using Aplicacao_C;
using SERPI.Dominio_C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class aluDadosPessoais : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            //usuario = (usuarios)Session["UsuarioAlunoLogado"];
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 43) && !usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 47)) // 43 - Portal do Aluno e 47 - Dados Cadastrais - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                if ((string)Session["UsuarioClonado"] == "sim")
                {
                    btnSalvarDadosCadastrais.Visible = false;
                    btnSalvarDadosCadastraisClone.Visible = false;
                }

                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                List<Pais> listaPais = aplicacaoGerais.ListaPais();
                //var listaPais2 = from item2 in listaPais
                //                 select new
                //                 {
                //                     Id_Pais = item2.Id_Pais,
                //                     Nacionalidade = item2.Nacionalidade
                //                 };

                ddlPaisAluno.Items.Clear();
                ddlPaisAluno.DataSource = listaPais;
                ddlPaisAluno.DataValueField = "Nome";
                ddlPaisAluno.DataTextField = "Nome";
                ddlPaisAluno.DataBind();
                ddlPaisAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um País", ""));
                ddlPaisAluno.SelectedValue = "";

                ddlNacionalidadeAluno.Items.Clear();
                ddlNacionalidadeAluno.DataSource = listaPais;
                ddlNacionalidadeAluno.DataValueField = "Nacionalidade";
                ddlNacionalidadeAluno.DataTextField = "Nacionalidade";
                ddlNacionalidadeAluno.DataBind();
                ddlNacionalidadeAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione uma Nacionalidade", ""));
                ddlNacionalidadeAluno.SelectedValue = "";

                ddlPaisEmpresaAluno.Items.Clear();
                ddlPaisEmpresaAluno.DataSource = listaPais;
                ddlPaisEmpresaAluno.DataValueField = "Nome";
                ddlPaisEmpresaAluno.DataTextField = "Nome";
                ddlPaisEmpresaAluno.DataBind();
                ddlPaisEmpresaAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um País", ""));
                ddlPaisEmpresaAluno.SelectedValue = "";

                List<Estado> listaEstado = aplicacaoGerais.ListaEstado();
                ddlEstadoAluno.Items.Clear();
                ddlEstadoAluno.DataSource = listaEstado;
                ddlEstadoAluno.DataValueField = "Sigla";
                ddlEstadoAluno.DataTextField = "Nome";
                ddlEstadoAluno.DataBind();
                ddlEstadoAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Estado", ""));
                ddlEstadoAluno.SelectedValue = "";

                ddlEstadoEmpresaAluno.Items.Clear();
                ddlEstadoEmpresaAluno.DataSource = listaEstado;
                ddlEstadoEmpresaAluno.DataValueField = "Sigla";
                ddlEstadoEmpresaAluno.DataTextField = "Nome";
                ddlEstadoEmpresaAluno.DataBind();
                ddlEstadoEmpresaAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Estado", ""));
                ddlEstadoEmpresaAluno.SelectedValue = "";

                alunos item_aluno = (alunos)Session["AlunoLogado"];

                if (usuario.avatar != "")
                {
                    imgAluno.Src = "img/pessoas/" + usuario.avatar + "?" + DateTime.Now;
                }

                else
                {
                    imgAluno.Src = "img/pessoas/avatarunissex.jpg" + "?" + DateTime.Now;
                }

                lblNomeAluno.Text = item_aluno.nome;
                lblCargoAluno.Text = item_aluno.cargo;
                lblMatriculaAluno.Text = item_aluno.idaluno.ToString();
                lblCPFAluno.Text = item_aluno.cpf;

                txtCepAluno.Value = item_aluno.cep_res;
                txtLogradouroAluno.Value = item_aluno.logradouro_res;
                txtNumeroAluno.Value = item_aluno.numero_res;
                txtComplementoAluno.Value = item_aluno.complemento_res;
                txtBairroAluno.Value = item_aluno.bairro_res;
                
                if (item_aluno.pais_res == "Brasil")
                {
                    divddlEstadoAluno.Style.Add("display", "block");
                    divddlCidadeAluno.Style.Add("display", "block");
                    divtxtEstadoAluno.Style.Add("display", "none");
                    divtxtCidadeAluno.Style.Add("display", "none");
                }
                else
                {
                    divddlEstadoAluno.Style.Add("display", "none");
                    divddlCidadeAluno.Style.Add("display", "none");
                    divtxtEstadoAluno.Style.Add("display", "block");
                    divtxtCidadeAluno.Style.Add("display", "block");
                }

                ddlPaisAluno.SelectedValue = item_aluno.pais_res == null ? "" : item_aluno.pais_res.Trim();

                if (item_aluno.data_nascimento != null)
                {
                    txtDataNascimento.Value = String.Format("{0:yyyy-MM-dd}", item_aluno.data_nascimento);
                }

                if (item_aluno.sexo != null)
                {
                    ddlSexoAluno.SelectedValue = item_aluno.sexo;
                }

                //=======================

                ddlEstadoAluno.SelectedValue = item_aluno.uf_res;
                txtEstadoAluno.Value = item_aluno.uf_res;

                ddlEstadoAluno_SelectedIndexChanged(null, null);

                if (item_aluno.cidade_res != null)
                {
                    ddlCidadeAluno.SelectedValue = item_aluno.cidade_res.Trim();
                    txtCidadeAluno.Value = item_aluno.cidade_res.Trim();
                }

                txtDataNascimento.Value = String.Format("{0:yyyy-MM-dd}", item_aluno.data_nascimento);

                txtCelularAluno.Value = item_aluno.celular_res;
                txtTelefone.Value = item_aluno.telefone_res;
                txtEmailPrincipalAluno.Value = item_aluno.email;
                txtEmailSecundarioAluno.Value = item_aluno.email2;
                ddlNacionalidadeAluno.SelectedValue = item_aluno.pais_nasc;
                txtProfissaoAlunoAluno.Value = item_aluno.profissao;
                ddlEstadoCivilAluno.SelectedValue = item_aluno.estado_civil;

                //=======================
                ddlTipoDoctoAluno.SelectedValue = item_aluno.tipo_documento;
                txtNumeroDoctoAluno.Value = item_aluno.numero_documento;
                txtDigitoDoctoAluno.Value = item_aluno.digito_num_documento;
                txtOrgaoExpeditorAluno.Value = item_aluno.orgao_expedidor;
                txtDataExpedicaoAluno.Value = String.Format("{0:yyyy-MM-dd}", item_aluno.data_expedicao);
                txtDataValidadeDoctoAluno.Value = String.Format("{0:yyyy-MM-dd}", item_aluno.data_validade); 

                //=======================

                txtFormacaoAluno.Value = item_aluno.formacao;
                txtInstituicaoAluno.Value = item_aluno.escola;
                txtAnoFormacaoAluno.Value = item_aluno.ano_graduacao.ToString();

                //=======================

                txtEmpresaAluno.Value = item_aluno.empresa;
                txtCargoAluno.Value = item_aluno.cargo;
                txtCepEmpresaAluno.Value = item_aluno.cep_empresa;
                txtLogradouroEmpresaAluno.Value = item_aluno.logradouro_empresa;
                txtNumeroEmpresaAluno.Value = item_aluno.numero_empresa;
                txtComplementoEmpresaAluno.Value = item_aluno.complemento_empresa;
                txtBairroEmpresaAluno.Value = item_aluno.bairro_empresa;

                if (item_aluno.pais_empresa == "Brasil")
                {
                    divddlEstadoEmpresaAluno.Style.Add("display", "block");
                    divddlCidadeEmpresaAluno.Style.Add("display", "block");
                    divtxtEstadoEmpresaAluno.Style.Add("display", "none");
                    divtxtCidadeEmpresaAluno.Style.Add("display", "none");
                }
                else
                {
                    divddlEstadoEmpresaAluno.Style.Add("display", "none");
                    divddlCidadeEmpresaAluno.Style.Add("display", "none");
                    divtxtEstadoEmpresaAluno.Style.Add("display", "block");
                    divtxtCidadeEmpresaAluno.Style.Add("display", "block");
                }

                ddlPaisEmpresaAluno.SelectedValue = item_aluno.pais_empresa == null ? "" : item_aluno.pais_empresa.Trim();

                ddlEstadoEmpresaAluno.SelectedValue = item_aluno.uf_empresa;
                txtEstadoEmpresaAluno.Value = item_aluno.uf_empresa;

                ddlEstadoEmpresaAluno_SelectedIndexChanged(null, null);
                if (item_aluno.cidade_empresa != null)
                {
                    ddlCidadeEmpresaAluno.SelectedValue = item_aluno.cidade_empresa.Trim();
                    txtCidadeEmpresaAluno.Value = item_aluno.cidade_empresa.Trim();
                }
                
                txtTelefoneEmpresaAluno.Value = item_aluno.telefone_empresa;
                txtRamalEmpresaAluno.Value = item_aluno.telefone_empresa_ramal;
                txtPalavraChaveAluno.Value = item_aluno.palavra_chave;

            }

        }

        public void ddlEstadoAluno_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (ddlEstadoAluno.SelectedValue != "")
            {
                ddlCidadeAluno.Items.Clear();
                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Estado itemEstado = new Estado();
                itemEstado.Sigla = ddlEstadoAluno.SelectedValue;
                List<Cidade> listaCidade = aplicacaoGerais.ListaCidade(itemEstado);
                ddlCidadeAluno.Items.Clear();
                ddlCidadeAluno.DataSource = listaCidade;
                ddlCidadeAluno.DataValueField = "Nome";
                ddlCidadeAluno.DataTextField = "Nome";
                ddlCidadeAluno.DataBind();
                ddlCidadeAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione uma Cidade", ""));
                ddlCidadeAluno.SelectedValue = "";
            }
            else
            {
                ddlCidadeAluno.Items.Clear();
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fPaisResidencia();fPaisEmpresa();", true);
        }

        public void ddlEstadoEmpresaAluno_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (ddlEstadoEmpresaAluno.SelectedValue != "")
            {
                ddlCidadeEmpresaAluno.Items.Clear();
                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Estado itemEstado = new Estado();
                itemEstado.Sigla = ddlEstadoEmpresaAluno.SelectedValue;
                List<Cidade> listaCidade = aplicacaoGerais.ListaCidade(itemEstado);
                ddlCidadeEmpresaAluno.Items.Clear();
                ddlCidadeEmpresaAluno.DataSource = listaCidade;
                ddlCidadeEmpresaAluno.DataValueField = "Nome";
                ddlCidadeEmpresaAluno.DataTextField = "Nome";
                ddlCidadeEmpresaAluno.DataBind();
                ddlCidadeEmpresaAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione uma Cidade", ""));
                ddlCidadeEmpresaAluno.SelectedValue = "";
            }
            else
            {
                ddlCidadeEmpresaAluno.Items.Clear();
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", "javascript:fPaisResidencia();fPaisEmpresa();", true);
        }

        public void btnSalvarDadosCadastrais_Click(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";
                if (txtCepAluno.Value.Trim() == "" || txtCepAluno.Value.Trim() == "-")
                {
                    txtCepAluno.Value = "";
                    sAux = "Preencher o CEP residencial. <br/><br/>";
                }
                else if (txtCepAluno.Value.Trim().Length != 9)
                {
                    sAux = "O CEP residencial deve ter 8 posições. <br/><br/>";
                }
                
                if (txtLogradouroAluno.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Logradouro residencial. <br/><br/>";
                }
                if (txtNumeroAluno.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Número residencial. <br/><br/>";
                }
                if (txtBairroAluno.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Bairro residencial. <br/><br/>";
                }

                if (ddlPaisAluno.SelectedValue == "")
                {
                    sAux = sAux + "Selecione o País residencial. <br/><br/>";
                }
                else if (ddlPaisAluno.SelectedValue == "Brasil")
                {
                    if (ddlEstadoAluno.SelectedValue == "")
                    {
                        sAux = sAux + "Selecione o Estado residencial. <br/><br/>";
                    }
                    if (ddlCidadeAluno.SelectedValue == "")
                    {
                        sAux = sAux + "Selecione a Cidade residencial. <br/><br/>";
                    }
                }
                else
                {
                    if (txtEstadoAluno.Value.Trim() == "")
                    {
                        sAux = sAux + "Digite o Estado residencial. <br/><br/>";
                    }
                    if (txtCidadeAluno.Value.Trim() == "")
                    {
                        sAux = sAux + "Digite a Cidade residencial. <br/><br/>";
                    }
                }
                if (txtDataNascimento.Value == "")
                {
                    sAux = sAux + "Digite a Data de Nascimento. <br/><br/>";
                }
                if (ddlSexoAluno.SelectedValue == "")
                {
                    sAux = sAux + "Informar o Gênero. <br/><br/>";
                }
                if (txtCelularAluno.Value.Trim() == "" || txtCelularAluno.Value.Trim() == "--")
                {
                    txtCelularAluno.Value = "";
                    sAux = sAux + "Preencher o Celular. <br/><br/>";
                }
                if (txtEmailPrincipalAluno.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Email principal. <br/><br/>";
                }
                if (txtEmailPrincipalAluno.Value.IndexOf("@") == 0)
                {
                    sAux = sAux + "Preencher um email válido no Email principal. <br/><br/>";
                }

                if (ddlNacionalidadeAluno.SelectedValue == "")
                {
                    sAux = sAux + "Preencher a Nacionalidade. <br/><br/>";
                }
                if (txtProfissaoAlunoAluno.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a Profissão <br/><br/>";
                }
                if (ddlEstadoCivilAluno.SelectedValue == "")
                {
                    sAux = sAux + "Preencher o Estado Civil. <br/><br/>";
                }
                //==================
                if (ddlTipoDoctoAluno.SelectedValue.Trim() == "")
                {
                    sAux = sAux + "Selecione o Tipo de Documento. <br/><br/>";
                }
                if (txtNumeroDoctoAluno.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Número do documento. <br/><br/>";
                }
                if (txtOrgaoExpeditorAluno.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Órgão Expeditor do documento. <br/><br/>";
                }
                if (txtDataExpedicaoAluno.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a Data de Expedição do documento. <br/><br/>";
                }
                //if (txtDataValidadeDoctoAluno.Value.Trim() == "")
                //{
                //    sAux = sAux + "Preencher a Data de Validade do documento. <br/><br/>";
                //}
                //==================
                if (txtEmpresaAluno.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a Empresa. <br/><br/>";
                }
                if (txtCargoAluno.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Cargo. <br/><br/>";
                }
                if (txtCepEmpresaAluno.Value.Trim() == "" || txtCepEmpresaAluno.Value.Trim() == "-")
                {
                    txtCepEmpresaAluno.Value = "";
                    sAux = sAux + "Preencher o CEP comercial. <br/><br/>";
                }
                else if (txtCepEmpresaAluno.Value.Trim().Length != 9)
                {
                    sAux = sAux + "O CEP comercial deve ter 8 posições. <br/><br/>";
                }
                if (txtLogradouroEmpresaAluno.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Logradouro comercial. <br/><br/>";
                }
                if (txtNumeroEmpresaAluno.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Número comercial. <br/><br/>";
                }
                if (txtBairroEmpresaAluno.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Bairro comercial. <br/><br/>";
                }

                if (ddlPaisEmpresaAluno.SelectedValue == "")
                {
                    sAux = sAux + "Selecione o País comercial. <br/><br/>";
                }
                else if (ddlPaisEmpresaAluno.SelectedValue == "Brasil")
                {
                    if (ddlEstadoEmpresaAluno.SelectedValue == "")
                    {
                        sAux = sAux + "Selecione o Estado comercial. <br/><br/>";
                    }
                    if (ddlCidadeEmpresaAluno.SelectedValue == "")
                    {
                        sAux = sAux + "Selecione a Cidade comercial. <br/><br/>";
                    }
                }
                else
                {
                    if (txtEstadoEmpresaAluno.Value.Trim() == "")
                    {
                        sAux = sAux + "Digite o Estado comercial. <br/><br/>";
                    }
                    if (txtCidadeEmpresaAluno.Value.Trim() == "")
                    {
                        sAux = sAux + "Digite a Cidade comercial. <br/><br/>";
                    }
                }
                if (txtTelefoneEmpresaAluno.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Telefone comercial. <br/><br/>";
                }
               
                if (txtFormacaoAluno.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a Formação. <br/><br/>";
                }

                if (txtInstituicaoAluno.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a Instituição. <br/><br/>";
                }

                if (txtAnoFormacaoAluno.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Ano de Formação. <br/><br/>";
                }
                else if (Convert.ToInt32(txtAnoFormacaoAluno.Value.Trim()) < 1900)
                {
                    sAux = sAux + "O Ano de Formação deve ser superior a 1900. <br/><br/>";
                }


                if (sAux != "")
                {
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                    return;
                }

                alunos item_aluno = (alunos)Session["AlunoLogado"];

                item_aluno.cep_res = txtCepAluno.Value.Trim();
                item_aluno.logradouro_res = txtLogradouroAluno.Value.Trim();
                item_aluno.numero_res = txtNumeroAluno.Value.Trim();
                item_aluno.complemento_res = txtComplementoAluno.Value.Trim();
                item_aluno.bairro_res = txtBairroAluno.Value.Trim();
                item_aluno.pais_res = ddlPaisAluno.SelectedValue;

                if (ddlPaisAluno.SelectedValue == "Brasil")
                {
                    item_aluno.uf_res = ddlEstadoAluno.SelectedValue;
                    item_aluno.cidade_res = ddlCidadeAluno.SelectedValue;
                }
                else
                {
                    item_aluno.uf_res = txtEstadoAluno.Value.Trim();
                    item_aluno.cidade_res = txtCidadeAluno.Value.Trim();
                }
                item_aluno.data_nascimento = Convert.ToDateTime(txtDataNascimento.Value);

                item_aluno.sexo = ddlSexoAluno.SelectedValue;
                item_aluno.celular_res = txtCelularAluno.Value.Trim();
                item_aluno.telefone_res = txtTelefone.Value.Trim();
                item_aluno.email = txtEmailPrincipalAluno.Value.Trim();
                item_aluno.email2 = txtEmailSecundarioAluno.Value.Trim();
                item_aluno.pais_nasc = ddlNacionalidadeAluno.SelectedValue;
                if (item_aluno.pais_nasc == "Brasileira")
                {
                    item_aluno.estrangeiro = "Não";
                }
                else
                {
                    item_aluno.estrangeiro = "Sim";
                }
                item_aluno.profissao = txtProfissaoAlunoAluno.Value;
                item_aluno.estado_civil = ddlEstadoCivilAluno.SelectedValue;

                //======

                item_aluno.tipo_documento = ddlTipoDoctoAluno.SelectedValue;
                item_aluno.numero_documento = txtNumeroDoctoAluno.Value.Trim();
                item_aluno.digito_num_documento = txtDigitoDoctoAluno.Value.Trim();
                item_aluno.orgao_expedidor = txtOrgaoExpeditorAluno.Value.Trim();
                item_aluno.data_expedicao = Convert.ToDateTime(txtDataExpedicaoAluno.Value);
                if (txtDataValidadeDoctoAluno.Value != "")
                {
                    item_aluno.data_validade = Convert.ToDateTime(txtDataValidadeDoctoAluno.Value);
                }
                
                //======

                //======

                item_aluno.formacao = txtFormacaoAluno.Value.Trim();
                item_aluno.escola = txtInstituicaoAluno.Value.Trim();
                item_aluno.ano_graduacao = Convert.ToInt32(txtAnoFormacaoAluno.Value.Trim());

                //======
                item_aluno.empresa = txtEmpresaAluno.Value.Trim();
                item_aluno.cargo = txtCargoAluno.Value.Trim();
                item_aluno.cep_empresa = txtCepEmpresaAluno.Value.Trim();
                item_aluno.logradouro_empresa = txtLogradouroEmpresaAluno.Value.Trim();
                item_aluno.numero_empresa = txtNumeroEmpresaAluno.Value.Trim();
                item_aluno.complemento_empresa = txtComplementoEmpresaAluno.Value.Trim();
                item_aluno.bairro_empresa = txtBairroEmpresaAluno.Value.Trim();
                item_aluno.pais_empresa= ddlPaisEmpresaAluno.SelectedValue;
                item_aluno.palavra_chave = txtPalavraChaveAluno.Value.Trim().Replace(",",";");

                if (ddlPaisEmpresaAluno.SelectedValue == "Brasil")
                {
                    item_aluno.uf_empresa = ddlEstadoEmpresaAluno.SelectedValue;
                    item_aluno.cidade_empresa = ddlCidadeEmpresaAluno.SelectedValue;
                }
                else
                {
                    item_aluno.uf_empresa = txtEstadoEmpresaAluno.Value.Trim();
                    item_aluno.cidade_empresa = txtCidadeEmpresaAluno.Value.Trim();
                }
                item_aluno.telefone_empresa = txtTelefoneEmpresaAluno.Value.Trim();
                item_aluno.telefone_empresa_ramal = txtRamalEmpresaAluno.Value.Trim();

                item_aluno.usuario = usuario.usuario;
                item_aluno.status = "alterado";
                item_aluno.data_alteracao = DateTime.Now;

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();

                if (aplicacaoAluno.AlterarItem(item_aluno))
                {
                    UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                    usuarios item_usuario_aluno = new usuarios();
                    item_usuario_aluno.usuario = item_aluno.idaluno.ToString();
                    item_usuario_aluno = aplicacaoUsuario.BuscaUsuario(item_usuario_aluno);
                    item_usuario_aluno.email = item_aluno.email;
                    aplicacaoUsuario.AlterarUsuario(item_usuario_aluno);

                    Session["AlunoLogado"] = item_aluno;
                    lblMensagem.Text = "Dados alterados com sucesso.";
                    lblTituloMensagem.Text = "Alteração";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fAlteracao('Alteração realizada com sucesso!');", true);
                }
                else
                {
                    lblMensagem.Text = "Houve algum problema na alteração dos dados.";
                    lblTituloMensagem.Text = "Alteração";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    lblMensagem.Text = "Houve algum problema na alteração dos dados do Aluno. <br/> <br/>Descrição do erro: " + ex.Message;
                }
                else
                {
                    lblMensagem.Text = "Houve algum problema na alteração dos dados do Aluno. <br/> <br/>Descrição do erro: " + ex.InnerException.InnerException.Message;
                }
                lblTituloMensagem.Text = "Alteração";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }

    }
}