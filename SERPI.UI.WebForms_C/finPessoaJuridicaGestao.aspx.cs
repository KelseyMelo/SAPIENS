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
    public partial class finPessoaJuridicaGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 7)) // 2. Pessoa Jurídica - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();

                List<Estado> listaEstado = aplicacaoGerais.ListaEstado();
                ddlEstadoFornecedor.Items.Clear();
                ddlEstadoFornecedor.DataSource = listaEstado;
                ddlEstadoFornecedor.DataValueField = "Sigla";
                ddlEstadoFornecedor.DataTextField = "Nome";
                ddlEstadoFornecedor.DataBind();
                ddlEstadoFornecedor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Estado", ""));
                ddlEstadoFornecedor.SelectedValue = "";

                if (Session["sNewFornecedores"] != null && (Boolean)Session["sNewFornecedores"] != true)
                {
                    fornecedores item;
                    item = (fornecedores)Session["fornecedores"];
                    lblTituloPagina.Text = "(Editar) - N.º " + item.id_fornecedor;

                    if (item.status == "inativado")
                    {
                        lblInativado.Style["display"] = "block";
                        btnAtivar.Style["display"] = "block";
                        btnInativar.Style["display"] = "none";
                    }
                    else
                    {
                        lblInativado.Style["display"] = "none";
                        btnAtivar.Style["display"] = "none";
                        btnInativar.Style["display"] = "block";
                    }

                    txtDataCadastro.Value = String.Format("{0:dd/MM/yyyy}", item.data_cadastro);
                    txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy}", item.data_alteracao);
                    txtStatus.Value = item.status;
                    txtResponsavel.Value = item.usuario;

                    txtNomeFornecedor.Value = item.nome;
                    txtCNPJFornecedor.Value = item.cnpj;
                    txtIEFornecedor.Value = item.inscricao_estadual;
                    txtLogradouroFornecedor.Value = item.logradouro_end;
                    txtNumeroFornecedor.Value = item.numero_end;
                    txtComplementoFornecedor.Value = item.comp_end;
                    txtBairroFornecedor.Value = item.bairro_end;
                    txtCidadeFornecedor.Value = item.cidade_end;
                    ddlEstadoFornecedor.SelectedValue = item.uf_end;
                    txtCepFornecedor.Value = item.cep_end;
                    txtNomeContatoFornecedor.Value = item.nome_contato;
                    txtCargoFornecedor.Value = item.cargo;
                    txtTelefoneFornecedor.Value = item.tel_contato;
                    txtCepFornecedor.Value = item.cel_contato;
                    txtFaxFornecedor.Value = item.fax_contato;
                    txtEmailFornecedor.Value = item.email_contato;
                    txtNomeBancoFornecedor.Value = item.nome_banco;
                    txtNumeroBancoFornecedor.Value = item.numero_banco;
                    txtAgenciaFornecedor.Value = item.agencia_numero;
                    txtNumeroContaFornecedor.Value = item.conta_numero;


                    //ddlTipoCurso.Attributes.Add("disabled", "disabled");
                    //txtAnoFornecedor.Attributes.Add("disabled", "disabled");
                    //txtAnoFornecedor.Value = item.ano;
                    //txtNumeroFornecedor.Value = item.numero.ToString();

                    //txtDataInicioFornecedor.Value = String.Format("{0:yyyy-MM-dd}", item.data_inicio);
                    //txtDataFimFornecedor.Value = String.Format("{0:yyyy-MM-dd}", item.data_fim);

                    btnCriarFornecedor.Disabled = false;
                    divEdicao.Visible = true;

                    if (Session["AdiciondoSucesso"] != null)
                    {
                        if ((Boolean)Session["AdiciondoSucesso"])
                        {
                            Session["AdiciondoSucesso"] = null;
                            lblMensagem.Text = "Novo Fornecedor adicionado com sucesso";
                            lblTituloMensagem.Text = "Novo Fornecedor";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
                        }

                    }

                }
                else
                {
                    lblInativado.Style["display"] = "none";
                    btnAtivar.Style["display"] = "none";
                    btnInativar.Style["display"] = "none";

                    lblTituloPagina.Text = "(novo)";
                    txtDataCadastro.Value = "";
                    txtDataAlteracao.Value = "";
                    txtStatus.Value = "";
                    txtResponsavel.Value = "";

                    txtNomeFornecedor.Value = "";
                    txtCNPJFornecedor.Value = "";
                    txtIEFornecedor.Value = "";
                    txtLogradouroFornecedor.Value = "";
                    txtNumeroFornecedor.Value = "";
                    txtComplementoFornecedor.Value = "";
                    txtBairroFornecedor.Value = "";
                    txtCidadeFornecedor.Value = "";
                    ddlEstadoFornecedor.SelectedValue = "";
                    txtCepFornecedor.Value = "";
                    txtNomeContatoFornecedor.Value = "";
                    txtCargoFornecedor.Value = "";
                    txtTelefoneFornecedor.Value = "";
                    txtCepFornecedor.Value = "";
                    txtFaxFornecedor.Value = "";
                    txtEmailFornecedor.Value = "";
                    txtNomeBancoFornecedor.Value = "";
                    txtNumeroBancoFornecedor.Value = "";
                    txtAgenciaFornecedor.Value = "";
                    txtNumeroContaFornecedor.Value = "";

                    btnCriarFornecedor.Disabled = true;
                    divEdicao.Visible = false;
                }
            }

        }

        protected void btnCriarFornecedor_Click(object sender, EventArgs e)
        {
            Session["sNewFornecedores"] = true;
            Session["fornecedores"] = null;
            Session["AdiciondoSucesso"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("finPessoaJuridica.aspx", true);
        }

        protected void btnSalvar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";
                //if (ddlTipoCurso.SelectedValue == "" && (Session["sNewQuadrimestre"] == null || (Boolean)Session["sNewQuadrimestre"] == true))
                //{
                //    sAux = "Selecionar um Tipo do Curso. <br/><br/>";
                //}
                if (txtNomeFornecedor.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o nome do Fornecedor. <br/><br/>";
                }
                if (txtCNPJFornecedor.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o CNPJ do Fornecedor. <br/><br/>";
                }
                else if (!Utilizades.fValidaCNPJ(txtCNPJFornecedor.Value.Trim()))
                {
                    sAux = sAux + "Preencher um CNPJ válido do Fornecedor. <br/><br/>";
                }
                if (txtCepFornecedor.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o CEP do Fornecedor. <br/><br/>";
                }

                if (sAux != "")
                {
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                    return;
                }

                if (Session["sNewFornecedores"] != null && (Boolean)Session["sNewFornecedores"] != true)
                {
                    //Alterar Registro

                    FornecedorAplicacao aplicacaoFornecedor = new FornecedorAplicacao();
                    fornecedores item;
                    fornecedores item_aux;

                    item = (fornecedores)Session["fornecedores"];

                    item_aux = aplicacaoFornecedor.BuscaItem(item.id_fornecedor, item.cnpj);
                    if (item_aux != null)
                    {
                        lblMensagem.Text = "O CNPJ: <strong>" + item_aux.cnpj + "</strong> já está cadastrado no nome da empresa: <strong>" + item_aux.nome + "</strong><br><br>Favor verificar.";
                        lblTituloMensagem.Text = "Cadastro de Fornecedor";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                        return;
                    }

                    item.nome = txtNomeFornecedor.Value;
                    item.cnpj = txtCNPJFornecedor.Value;
                    item.inscricao_estadual = txtIEFornecedor.Value;
                    item.logradouro_end = txtLogradouroFornecedor.Value;
                    item.numero_end = txtNumeroFornecedor.Value;
                    item.comp_end = txtComplementoFornecedor.Value;
                    item.bairro_end = txtBairroFornecedor.Value;
                    item.cidade_end = txtCidadeFornecedor.Value;
                    item.uf_end = ddlEstadoFornecedor.SelectedValue;
                    item.cep_end = txtCepFornecedor.Value;
                    item.nome_contato = txtNomeContatoFornecedor.Value;
                    txtCargoFornecedor.Value = item.cargo;
                    item.tel_contato = txtTelefoneFornecedor.Value;
                    item.cel_contato = txtCelularFornecedor.Value;
                    item.fax_contato = txtFaxFornecedor.Value;
                    item.email_contato = txtEmailFornecedor.Value;
                    item.nome_banco = txtNomeBancoFornecedor.Value;
                    item.numero_banco = txtNumeroBancoFornecedor.Value;
                    item.agencia_numero = txtAgenciaFornecedor.Value;
                    item.conta_numero = txtNumeroContaFornecedor.Value;

                    item.status = "alterado";
                    //item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    aplicacaoFornecedor.AlterarItem(item);

                    item = aplicacaoFornecedor.BuscaItem(item.id_fornecedor);

                    txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy}", item.data_alteracao);
                    txtStatus.Value = item.status;
                    txtResponsavel.Value = item.usuario;

                    lblMensagem.Text = "Fornecedor alterado com sucesso.";
                    lblTituloMensagem.Text = "Alteração de Fornecedor";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

                    Session["fornecedores"] = item;

                }
                else
                {
                    //Inserir Registro
                    FornecedorAplicacao aplicacaoFornecedor = new FornecedorAplicacao();
                    fornecedores item = new fornecedores();

                    item.cnpj = txtCNPJFornecedor.Value.Trim();
                    item = aplicacaoFornecedor.BuscaItem(0, item.cnpj);

                    if (item != null)
                    {
                        lblMensagem.Text = "O CNPJ: <strong>" + item.cnpj + "</strong> já está cadastrado no nome da empresa: <strong>" + item.nome + "</strong><br><br>Favor verificar.";
                        lblTituloMensagem.Text = "Cadastro de Fornecedor";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                        return;
                    }

                    item = new fornecedores();
                    item.nome = txtNomeFornecedor.Value;
                    item.cnpj = txtCNPJFornecedor.Value.Trim();
                    item.inscricao_estadual = txtIEFornecedor.Value;
                    item.logradouro_end = txtLogradouroFornecedor.Value;
                    item.numero_end = txtNumeroFornecedor.Value;
                    item.comp_end = txtComplementoFornecedor.Value;
                    item.bairro_end = txtBairroFornecedor.Value;
                    item.cidade_end = txtCidadeFornecedor.Value;
                    item.uf_end = ddlEstadoFornecedor.SelectedValue;
                    item.cep_end = txtCepFornecedor.Value;
                    item.nome_contato = txtNomeContatoFornecedor.Value;
                    txtCargoFornecedor.Value = item.cargo;
                    item.tel_contato = txtTelefoneFornecedor.Value;
                    item.cel_contato = txtCelularFornecedor.Value;
                    item.fax_contato = txtFaxFornecedor.Value;
                    item.email_contato = txtEmailFornecedor.Value;
                    item.nome_banco = txtNomeBancoFornecedor.Value;
                    item.numero_banco = txtNumeroBancoFornecedor.Value;
                    item.agencia_numero = txtAgenciaFornecedor.Value;
                    item.conta_numero = txtNumeroContaFornecedor.Value;

                    item.status = "cadastrado";
                    item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;


                    item = aplicacaoFornecedor.CriarItem(item);

                    if (item != null)
                    {
                        Session["fornecedores"] = item;
                        Session.Add("sNewFornecedores", false);
                        Session["AdiciondoSucesso"] = true;
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na manutenção do Fornecedor. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Cadastro de Fornecedor";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }

    }
}