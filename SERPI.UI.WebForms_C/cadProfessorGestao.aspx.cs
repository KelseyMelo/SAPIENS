using Aplicacao_C;
using SERPI.Dominio_C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Diagnostics;
using System.Configuration;

namespace SERPI.UI.WebForms_C
{
    public partial class cadProfessorGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 8)) // 2. Professores - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                GeraisAplicacao AplicacaoGerias = new GeraisAplicacao();

                List<Pais> listaPais = AplicacaoGerias.ListaPais();
                ddlPaisEmpresaProfessor.Items.Clear();
                ddlPaisEmpresaProfessor.DataSource = listaPais.OrderBy(x => x.Id_Pais);
                ddlPaisEmpresaProfessor.DataValueField = "Nome";
                ddlPaisEmpresaProfessor.DataTextField = "Nome";
                ddlPaisEmpresaProfessor.DataBind();
                ddlPaisEmpresaProfessor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um País", ""));
                ddlPaisEmpresaProfessor.SelectedValue = "";

                List<Estado> listaEstaddo = AplicacaoGerias.ListaEstado();
                ddlEstadoResidenciaProfessor.Items.Clear();
                ddlEstadoResidenciaProfessor.DataSource = listaEstaddo.OrderBy(x => x.Nome);
                ddlEstadoResidenciaProfessor.DataValueField = "Sigla";
                ddlEstadoResidenciaProfessor.DataTextField = "Nome";
                ddlEstadoResidenciaProfessor.DataBind();
                ddlEstadoResidenciaProfessor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Estado", ""));
                ddlEstadoResidenciaProfessor.SelectedValue = "";

                ddlEstadoEmpresaProfessor.Items.Clear();
                ddlEstadoEmpresaProfessor.DataSource = listaEstaddo.OrderBy(x => x.Nome);
                ddlEstadoEmpresaProfessor.DataValueField = "Sigla";
                ddlEstadoEmpresaProfessor.DataTextField = "Nome";
                ddlEstadoEmpresaProfessor.DataBind();
                ddlEstadoEmpresaProfessor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Estado", ""));
                ddlEstadoEmpresaProfessor.SelectedValue = "";

                List<titulacao> listaTitulacao = AplicacaoGerias.ListaTitulacao();
                ddlTituloProfessor.Items.Clear();
                ddlTituloProfessor.DataSource = listaTitulacao;
                ddlTituloProfessor.DataValueField = "id_titulacao";
                ddlTituloProfessor.DataTextField = "nome";
                ddlTituloProfessor.DataBind();

                List<forma_recebimento> lista_Forma = AplicacaoGerias.ListaFormaRecebimento_banca();
                ddlBanca.Items.Clear();
                ddlBanca.DataSource = lista_Forma;
                ddlBanca.DataValueField = "id_forma_recebimento";
                ddlBanca.DataTextField = "nome";
                ddlBanca.DataBind();

                if (Session["sNewProfessor"] != null && (Boolean)Session["sNewProfessor"] != true)
                {

                    professores item;
                    item = (professores)Session["professores"];
                    lblTituloProfessor_a.Text = (item.sexo == "m") ? "Professor" : "Professora";
                    lblTituloNomeProfessor.Text = item.nome;
                    lblTituloCodigo.Text = "Código";
                    lblNumeroCodigo.Text = item.id_professor.ToString();
                    lblTituloAlteradoPor.Text = "Alterado por: ";
                    lblAlteradoPor.Text = item.usuario;
                    lblTituloAlteradoEm.Text = "em: ";
                    lblAlteradoEm.Text = String.Format("{0:dd/MM/yyyy}", item.data_alteracao);
                    divBotaoFoto.Visible = true;
                    divTextoBotaoFoto.Visible = false;
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

                    usuarios itemUsurioProfessor = new usuarios();
                    UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                    itemUsurioProfessor.usuario = item.id_professor.ToString() + "p";
                    itemUsurioProfessor = aplicacaoUsuario.BuscaUsuario(itemUsurioProfessor);
                    if (itemUsurioProfessor == null)
                    {
                        itemUsurioProfessor = new usuarios();
                        itemUsurioProfessor.usuario = Convert.ToString(item.id_professor) + "p";
                        itemUsurioProfessor = aplicacaoUsuario.BuscaUsuario(itemUsurioProfessor);
                        if (itemUsurioProfessor == null)
                        {
                            SHA1CryptoServiceProvider objSHA1 = new SHA1CryptoServiceProvider();
                            ASCIIEncoding objEncoding = new ASCIIEncoding();
                            usuarios usuarioProfessor = new usuarios();
                            usuarioProfessor.usuario = Convert.ToString(item.id_professor) + "p";
                            usuarioProfessor.nome = item.nome;
                            usuarioProfessor.un = "Acadêmico";
                            usuarioProfessor.email = item.email;
                            usuarioProfessor.id_grupo_acesso = 5;
                            usuarioProfessor.status = 1;
                            usuarioProfessor.avatar = "";
                            usuarioProfessor.nomeSocial = item.nome.Substring(0, item.nome.IndexOf(" "));
                            usuarioProfessor.senha = Convert.ToBase64String(objSHA1.ComputeHash(objEncoding.GetBytes(Convert.ToString(item.cpf))));

                            aplicacaoUsuario.CriarUsuario(usuarioProfessor);
                            itemUsurioProfessor = aplicacaoUsuario.BuscaUsuario(usuarioProfessor);
                        }
                    }
                        if (itemUsurioProfessor.avatar != "")
                    {
                        imgProfessor.Src = "img/pessoas/" + itemUsurioProfessor.avatar + "?" + DateTime.Now;
                    }

                    else
                    {
                        imgProfessor.Src = "img/pessoas/avatarunissex.jpg" + "?" + DateTime.Now;
                    }
                    imgFotoOriginal.Src = imgProfessor.Src;

                    //=======Dados Pessoal==========================
                    txtNomeProfessor.Value = item.nome;
                    txtCPFProfessor.Value = item.cpf;
                    string sComando = "";
                    if (item.cpf_passaporte == 0)
                    {
                        //lblCPF_Passaporte.InnerText = "CPF";
                        //iCPF_Passaporte.Attributes.Remove("class");
                        //iCPF_Passaporte.Attributes.Add("class", "fa fa-toggle-on");
                        optCPF.Checked = true;
                        optPassaporte.Checked = false;
                        sComando = "fSetMaskaraCPF();";
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fSetMaskaraCPF();", true);
                    }
                    else
                    {
                        //lblCPF_Passaporte.InnerText = "Passaporte";
                        //iCPF_Passaporte.Attributes.Remove("class");
                        //iCPF_Passaporte.Attributes.Add("class", "fa fa-toggle-off");
                        optCPF.Checked = false;
                        optPassaporte.Checked = true;
                        sComando = "fUnsetMaskaraCPF();";
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fUnsetMaskaraCPF();", true);
                    }
                    ddlSexoProfessor.SelectedValue = item.sexo;
                    txtDataNascimentoProfessor.Value = String.Format("{0:yyyy-MM-dd}", item.data_nasc);
                    txtDataCadastroProfessor.Value = String.Format("{0:dd/MM/yyyy}", item.data_cadastro);
                    ddlTipoDoctoProfessor.SelectedValue = item.tipo_documento;
                    txtNumeroDoctoProfessor.Value = item.numero_documento;
                    txtNacionalidadeProfessor.Value = item.nacionalidade;
                    txtUrlLatesProfessor.Value = item.url_lattes;
                    txtEmail1Professor.Value = item.email;
                    txtEmail2Professor.Value = item.email2;
                    txtTelefoneProfessor.Value = item.telefone_res;
                    txtCelularProfessor.Value = item.celular_res;

                    divConfirnacaoEmail.Visible = true;

                    if (item.email_confirmado == 1)
                    {
                        divEmailConfirmado.Visible = true;
                        divEmailNaoConfirmado.Visible = false;
                    }
                    else
                    {
                        divEmailConfirmado.Visible = false;
                        divEmailNaoConfirmado.Visible = true;
                    }

                    //=======Residencia==========================
                    txtCepResidenciaProfessor.Value = item.cep_res;
                    txtLogradouroResidenciaProfessor.Value = item.endereco_res;
                    txtNumeroResidenciaProfessor.Value = item.numero_res;
                    txtComplementoResidenciaProfessor.Value = item.complemento_res;
                    txtBairroResidenciaProfessor.Value = item.bairro_res;
                    if (item.uf_res != null)
                    {
                        ddlEstadoResidenciaProfessor.SelectedValue = item.uf_res;
                        if (ddlEstadoResidenciaProfessor.SelectedValue == "" && item.uf_res.Trim() != "")
                        {
                            txtEstadoResidenciaProfessorAnt.Value = item.uf_res;
                            txtEstadoResidenciaProfessorAnt.Style["display"] = "block";
                        }
                        else
                        {
                            txtEstadoResidenciaProfessorAnt.Style["display"] = "none";
                            ddlEstadoResidenciaProfessor_SelectedIndexChanged(null, null);
                        }
                    }

                    if (item.cidade_res != null)
                    {
                        ddlCidadeResidenciaProfessor.SelectedValue = item.cidade_res.Trim();
                        if (ddlCidadeResidenciaProfessor.SelectedValue == "" && item.cidade_res.Trim() != "")
                        {
                            txtCidadeResidenciaProfessorAnt.Value = item.cidade_res;
                            txtCidadeResidenciaProfessorAnt.Style["display"] = "block";
                        }
                        else
                        {
                            txtCidadeResidenciaProfessorAnt.Style["display"] = "none";
                        }
                    }
                    txtPlacaProfessor.Value = item.Placa;

                    //=======Título Acadêmico==========================
                    ddlTituloProfessor.SelectedValue = item.id_titulo.ToString();
                    txtAnoObtencaoProfessor.Value = item.ano_obtencao_titulo;
                    txtLocalTituloProfessor.Value = item.local_obtencao_titulo;

                    //=======Dados Bancários Pessoa Física==========================
                    txtNomeBancoProfessor.Value = item.nome_banco;
                    txtNumeroBancoProfessor.Value = item.numero_banco;
                    txtAgenciaProfessor.Value = item.agencia_numero;
                    txtNumeroContaProfessor.Value = item.conta_numero;

                    //=======Dados Comerciais==========================
                    txtEmpresaProfessor.Value = item.empresa;
                    txtCEPEmpresaProfessor.Value = item.cep_empresa;
                    txtLogradouroEmpresaProfessor.Value = item.logradouro_empresa;
                    txtNumeroEmpresaProfessor.Value = item.numero_empresa;
                    txtComplementoEmpresaProfessor.Value = item.complemento_empresa;
                    txtBairroEmpresaProfessor.Value = item.bairro_empresa;
                    ddlPaisEmpresaProfessor.SelectedValue = item.pais_empresa;
                    if (ddlPaisEmpresaProfessor.SelectedValue == "" && item.pais_empresa != "")
                    {
                        txtPaisEmpresaProfessorAnt.Value = item.pais_empresa;
                        txtPaisEmpresaProfessorAnt.Style["display"] = "block";
                    }
                    else
                    {
                        txtPaisEmpresaProfessorAnt.Style["display"] = "none";
                    }

                    ddlEstadoEmpresaProfessor.SelectedValue = item.uf_empresa;
                    txtEstadoEmpresaProfessor.Value = item.uf_empresa;
                    txtEstadoEmpresaProfessorAnt.Value = item.uf_empresa;
                    ddlEstadoEmpresaProfessor_SelectedIndexChanged(null, null);
                    ddlCidadeEmpresaProfessor.SelectedValue = item.cidade_empresa;
                    txtCidadeEmpresaProfessor.Value = item.cidade_empresa;
                    txtCidadeEmpresaProfessorAnt.Value = item.cidade_empresa;

                    if (item.pais_empresa == "Brasil")
                    {
                        divDDLEstadoEmpresaProfessor.Style["display"] = "block";
                        divDDLCidadeEmpresaProfessor.Style["display"] = "block";
                        divTXTEstadoEmpresaProfessor.Style["display"] = "none";
                        divTXTCidadeEmpresaProfessor.Style["display"] = "none";
                        if (ddlEstadoEmpresaProfessor.SelectedValue != "")
                        {
                            txtEstadoEmpresaProfessorAnt.Style["display"] = "none";
                        }
                        else
                        {
                            txtEstadoEmpresaProfessorAnt.Style["display"] = "block";
                        }
                        if (ddlCidadeEmpresaProfessor.SelectedValue != "")
                        {
                            txtCidadeEmpresaProfessorAnt.Style["display"] = "none";
                        }
                        else
                        {
                            txtCidadeEmpresaProfessorAnt.Style["display"] = "block";
                        }
                    }
                    else
                    {
                        divDDLEstadoEmpresaProfessor.Style["display"] = "none";
                        divDDLCidadeEmpresaProfessor.Style["display"] = "none";
                        divTXTEstadoEmpresaProfessor.Style["display"] = "block";
                        divTXTCidadeEmpresaProfessor.Style["display"] = "block";
                    }

                    txtCargoProfessor.Value = item.cargo;
                    txtTelefoneEmpresaProfessor.Value = item.telefone_empresa;
                    txtRamalEmpresaProfessor.Value = item.telefone_empresa_ramal;
                    bool bAcendeuEmpresa = false;

                    //=======Formas de Recebimento==========================
                    ddlHorasAula.SelectedValue = item.professores_forma_recebimento.horas_aula.ToString();
                    if (item.professores_forma_recebimento.horas_aula == 2)
                    {
                        divHorasPermitidas.Style["display"] = "block";
                        divDadosEmpresa.Style["display"] = "none";
                    }
                    else if (item.professores_forma_recebimento.horas_aula == 5 || item.professores_forma_recebimento.horas_aula == 12 || item.professores_forma_recebimento.horas_aula == 13 || item.professores_forma_recebimento.horas_aula == 14)
                    {
                        divHorasPermitidas.Style["display"] = "none";
                        divDadosEmpresa.Style["display"] = "block";
                        bAcendeuEmpresa = true;
                    }
                    else
                    {
                        //if (item.professores_forma_recebimento.orientacao != 5 && item.professores_forma_recebimento.orientacao != 12 && item.professores_forma_recebimento.orientacao != 13)
                         if (item.professores_forma_recebimento.orientacao != 5)
                        {
                            divDadosEmpresa.Style["display"] = "none";
                            bAcendeuEmpresa = true;
                        }
                        else if(!bAcendeuEmpresa)
                        {
                            divDadosEmpresa.Style["display"] = "block";
                        }
                        divHorasPermitidas.Style["display"] = "none";
                        
                    }

                    txtHorasAulaCLT.Value = item.professores_forma_recebimento.horas_clt.ToString();
                    ddlHorasAulaAdicional.SelectedValue = item.professores_forma_recebimento.horas_aula_adicional.ToString();

                    if (ddlHorasAulaAdicional.SelectedValue != "")
                    {
                        if (item.professores_forma_recebimento.horas_aula_adicional == 5 || item.professores_forma_recebimento.horas_aula == 5 || item.professores_forma_recebimento.orientacao == 5)
                        {
                            divDadosEmpresa.Style["display"] = "block";
                            bAcendeuEmpresa = true;
                        }
                        else if (!bAcendeuEmpresa)
                        {
                            divDadosEmpresa.Style["display"] = "none";
                        }
                    }

                    ddlOrientacao.SelectedValue =   item.professores_forma_recebimento.orientacao.ToString();
                    ddlBanca.SelectedValue =   item.professores_forma_recebimento.banca.ToString();

                    //=======Pessoa Jurídica Para Recebimento ==========================
                    if (item.professores_forma_recebimento.fornecedores != null)
                    {
                        txtIdEmpresa.Value = item.professores_forma_recebimento.fornecedores.id_fornecedor.ToString();
                        txtNomeEmpresa.Value = item.professores_forma_recebimento.fornecedores.nome;
                        txtCNPJEmpresa.Value = item.professores_forma_recebimento.fornecedores.cnpj;
                        txtInscricaoEstadualEmpresa.Value = item.professores_forma_recebimento.fornecedores.inscricao_estadual;
                        txtCargoEmpresa.Value = item.professores_forma_recebimento.fornecedores.cargo;
                        txtCEPEmpresa.Value = item.professores_forma_recebimento.fornecedores.cep_end;
                        txtLogradouroEmpresa.Value = item.professores_forma_recebimento.fornecedores.logradouro_end;
                        txtNumeroEmpresa.Value = item.professores_forma_recebimento.fornecedores.numero_end;
                        txtComplementoEmpresa.Value = item.professores_forma_recebimento.fornecedores.comp_end;
                        txtBairroEmpresa.Value = item.professores_forma_recebimento.fornecedores.bairro_end;
                        txtCidadeEmpresa.Value = item.professores_forma_recebimento.fornecedores.cidade_end;
                        txtEstadoEmpresa.Value = item.professores_forma_recebimento.fornecedores.uf_end;
                        txtTelefoneEmpresa.Value = item.professores_forma_recebimento.fornecedores.tel_contato;
                        txtCelularEmpresa.Value = item.professores_forma_recebimento.fornecedores.cel_contato;
                        txtFaxEmpresa.Value = item.professores_forma_recebimento.fornecedores.fax_contato;
                    }
                    

                    txtObservacaoProfessor.Value = item.Observacao;

                    btnNovoProfessor.Disabled = false;
                    btnImprimirCadastro.Disabled = false;

                    //ddlEstadoResidenciaProfessor_SelectedIndexChanged(null, null);

                    if (Session["AdiciondoSucesso"] != null)
                    {
                        if ((Boolean)Session["AdiciondoSucesso"])
                        {
                            subEnviaEmailConfirmacao(true);

                            Session["AdiciondoSucesso"] = null;
                            lblMensagem.Text = "Novo Professor adicionado com sucesso.<br><br> ";
                            lblMensagem.Text = lblMensagem.Text + "Um e-mail automático foi enviado para o professor <strong>\"" + item.nome + "\"</strong>, para que ele possa confirmar o endereço do <strong>e-mail principal</strong> cadastrado."; 
                            lblTituloMensagem.Text = "Novo Professor";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", sComando + "AbreMensagem('alert-success');", true);
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", sComando, true);
                        }

                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", sComando, true);
                    }

                }
                else
                {
                    PreparaNovoRegistro();
                }
                if
                    (usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 8))
                {
                    if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 8).FirstOrDefault().escrita != true)
                    {
                        btnNovoProfessor.Visible = false;
                        bntSalvarProfessor2.Visible = false;
                        btnInativar.Visible = false;
                        bntSalvarProfessor1.Visible = false;
                    }
                }
            }
        }


        private void PreparaNovoRegistro()
        {

            lblInativado.Style["display"] = "none";
            btnAtivar.Style["display"] = "none";
            btnInativar.Style["display"] = "none";

            lblTituloProfessor_a.Text = "Professor";
            lblTituloNomeProfessor.Text = "Novo";
            lblTituloCodigo.Text = "";
            lblNumeroCodigo.Text = "";
            lblTituloAlteradoPor.Text = "";
            lblAlteradoPor.Text = "";
            lblTituloAlteradoEm.Text = "";
            lblAlteradoEm.Text = "";
            divBotaoFoto.Visible = false;
            divTextoBotaoFoto.Visible = true;
            imgProfessor.Src = "img/pessoas/avatarunissex.jpg" + "?" + DateTime.Now;

            //=======Dados Pessoal==========================
            txtNomeProfessor.Value = "";
            txtCPFProfessor.Value = "";
            //lblCPF_Passaporte.InnerText = "CPF";
            //iCPF_Passaporte.Attributes.Remove("class");
            //iCPF_Passaporte.Attributes.Add("class", "fa fa-toggle-on");
            optCPF.Checked = true;
            optPassaporte.Checked = false;
            //hCPF_Passaporte.Value = "CPF";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fSetMaskaraCPF();", true);

            ddlSexoProfessor.SelectedValue = "";
            txtDataNascimentoProfessor.Value = "";
            txtDataCadastroProfessor.Value = String.Format("{0:dd/MM/yyyy}", DateTime.Today);
            ddlTipoDoctoProfessor.SelectedValue = "";
            txtNumeroDoctoProfessor.Value = "";
            txtNacionalidadeProfessor.Value = "";
            txtUrlLatesProfessor.Value = "";
            txtEmail1Professor.Value = "";
            txtEmail2Professor.Value = "";
            txtTelefoneProfessor.Value = "";
            txtCelularProfessor.Value = "";
            divConfirnacaoEmail.Visible = false;

            //=======Residencia==========================
            txtCepResidenciaProfessor.Value = "";
            txtLogradouroResidenciaProfessor.Value = "";
            txtNumeroResidenciaProfessor.Value = "";
            txtComplementoResidenciaProfessor.Value = "";
            txtBairroResidenciaProfessor.Value = "";
            txtEstadoResidenciaProfessorAnt.Value = "";
            txtEstadoResidenciaProfessorAnt.Style["display"] = "none";
            ddlEstadoResidenciaProfessor.SelectedValue = "";
            ddlCidadeResidenciaProfessor.Items.Clear();
            txtCidadeResidenciaProfessorAnt.Value = "";
            txtCidadeResidenciaProfessorAnt.Style["display"] = "none";
            txtPlacaProfessor.Value = "";

            //=======Título Acadêmico==========================
            ddlTituloProfessor.SelectedValue = "1";
            txtAnoObtencaoProfessor.Value = "";
            txtLocalTituloProfessor.Value = "";

            //=======Dados Bancários Pessoa Física==========================
            txtNomeBancoProfessor.Value = "";
            txtNumeroBancoProfessor.Value = "";
            txtAgenciaProfessor.Value = "";
            txtNumeroContaProfessor.Value = "";

            //=======Dados Comerciais==========================
            txtEmpresaProfessor.Value = "IPT - Instituto de Pesquisas Tecnológicas do Est. De SP.";
            txtCEPEmpresaProfessor.Value = "05508-901";
            txtLogradouroEmpresaProfessor.Value = "Av. Professor Almeida Prado";
            txtNumeroEmpresaProfessor.Value = "532";
            txtComplementoEmpresaProfessor.Value = "Prédio 56";
            txtBairroEmpresaProfessor.Value = "Butantã";
            ddlPaisEmpresaProfessor.SelectedValue = "Brasil";
            txtPaisEmpresaProfessorAnt.Value = "";
            txtPaisEmpresaProfessorAnt.Style["display"] = "none";
            ddlEstadoEmpresaProfessor.SelectedValue = "SP";
            ddlEstadoEmpresaProfessor_SelectedIndexChanged(null, null);
            txtEstadoEmpresaProfessor.Value = "";
            txtEstadoEmpresaProfessorAnt.Value = "";
            //ddlCidadeEmpresaProfessor.Items.Clear();
            ddlCidadeEmpresaProfessor.SelectedValue = "São Paulo";
            txtCidadeEmpresaProfessor.Value = "";
            txtCidadeEmpresaProfessorAnt.Value = "";
            divTXTEstadoEmpresaProfessor.Style["display"] = "none";
            divTXTCidadeEmpresaProfessor.Style["display"] = "none";
            divDDLEstadoEmpresaProfessor.Style["display"] = "block";
            divDDLCidadeEmpresaProfessor.Style["display"] = "block";
            txtCargoProfessor.Value = "Professor";
            txtTelefoneEmpresaProfessor.Value = "11";
            txtRamalEmpresaProfessor.Value = "";

            //=======Formas de Recebimento==========================
            ddlHorasAula.SelectedValue = "1";
            divHorasPermitidas.Style["display"] = "none";
            divDadosEmpresa.Style["display"] = "none";
            txtHorasAulaCLT.Value = "0";
            ddlHorasAulaAdicional.SelectedValue = "1";
            divDadosEmpresa.Style["display"] = "none";
            ddlOrientacao.SelectedValue = "1";
            ddlBanca.SelectedValue = "1";

            //=======Pessoa Jurídica Para Recebimento ==========================
            txtIdEmpresa.Value = "";
            txtNomeEmpresa.Value = "";
            txtCNPJEmpresa.Value = "";
            txtInscricaoEstadualEmpresa.Value = "";
            txtCargoEmpresa.Value = "";
            txtCEPEmpresa.Value = "";
            txtLogradouroEmpresa.Value = "";
            txtNumeroEmpresa.Value = "";
            txtComplementoEmpresa.Value = "";
            txtBairroEmpresa.Value = "";
            txtCidadeEmpresa.Value = "";
            txtEstadoEmpresa.Value = "";
            txtTelefoneEmpresa.Value = "";
            txtCelularEmpresa.Value = "";
            txtFaxEmpresa.Value = "";

            txtObservacaoProfessor.Value = "";

            Session["professores"] = null;
            Session["sNewProfessor"] = true;
            Session["AdiciondoSucesso"] = null;

            btnNovoProfessor.Disabled = true;
            btnImprimirCadastro.Disabled = true;
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("cadProfessor.aspx", true);
        }

        public void ddlEstadoResidenciaProfessor_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (ddlEstadoResidenciaProfessor.SelectedValue != "")
            {
                ddlCidadeResidenciaProfessor.Items.Clear();
                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Estado itemEstado = new Estado();
                itemEstado.Sigla = ddlEstadoResidenciaProfessor.SelectedValue;
                List<Cidade> listaCidade = aplicacaoGerais.ListaCidade(itemEstado);
                ddlCidadeResidenciaProfessor.Items.Clear();
                ddlCidadeResidenciaProfessor.DataSource = listaCidade;
                ddlCidadeResidenciaProfessor.DataValueField = "Nome";
                ddlCidadeResidenciaProfessor.DataTextField = "Nome";
                ddlCidadeResidenciaProfessor.DataBind();
                ddlCidadeResidenciaProfessor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione uma Cidade", ""));
                ddlCidadeResidenciaProfessor.SelectedValue = "";
            }
            else
            {
                ddlCidadeResidenciaProfessor.Items.Clear();
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", "javascript:fSelect2();", true);
        }

        public void ddlEstadoEmpresaProfessor_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (ddlEstadoEmpresaProfessor.SelectedValue != "")
            {
                ddlCidadeEmpresaProfessor.Items.Clear();
                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Estado itemEstado = new Estado();
                itemEstado.Sigla = ddlEstadoEmpresaProfessor.SelectedValue;
                List<Cidade> listaCidade = aplicacaoGerais.ListaCidade(itemEstado);
                ddlCidadeEmpresaProfessor.Items.Clear();
                ddlCidadeEmpresaProfessor.DataSource = listaCidade;
                ddlCidadeEmpresaProfessor.DataValueField = "Nome";
                ddlCidadeEmpresaProfessor.DataTextField = "Nome";
                ddlCidadeEmpresaProfessor.DataBind();
                ddlCidadeEmpresaProfessor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione uma Cidade", ""));
                ddlCidadeEmpresaProfessor.SelectedValue = "";
            }
            else
            {
                ddlCidadeEmpresaProfessor.Items.Clear();
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.UpdatePanel3.GetType(), "Script", "javascript:fSelect2();", true);
        }

        protected void btnNovoProfessor_Click(object sender, EventArgs e)
        {
            PreparaNovoRegistro();
        }

        protected void btnbtnEnvirConfirmacaoEmail_Click(object sender, EventArgs e)
        {
            string sAux = "";
            if (txtEmail1Professor.Value.Trim() == "")
            {
                sAux = sAux + "Preencher o e-mail principal do Professor. <br/><br/>";
            }
            else if (txtEmail1Professor.Value.Trim().IndexOf("@") == -1)
            {
                sAux = sAux + "Preencher um endereço válido para o e-mail principal do Professor. <br/><br/>";
            }

            if (sAux != "")
            {
                lblMensagem.Text = sAux;
                lblTituloMensagem.Text = "Atenção";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-warning');", true);
                return;
            }

            subEnviaEmailConfirmacao(false);
            professores item;

            item = (professores)Session["professores"];
            lblTituloMensagem.Text = "Atenção";
            lblMensagem.Text = "Um novo e-mail automático foi enviado para o professor <strong>\"" + item.nome + "\"</strong>, para que ele possa confirmar o endereço do <strong>e-mail principal</strong> cadastrado.";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-warning');", true);
        }

        private void subEnviaEmailConfirmacao(bool bNovoProfesor)
        {
            usuarios usuario = new usuarios();
            usuario = (usuarios)Session["UsuarioLogado"];

            professores item;
            item = (professores)Session["professores"];
            SHA1CryptoServiceProvider objSHA1 = new SHA1CryptoServiceProvider();
            ASCIIEncoding objEncoding = new ASCIIEncoding();
            string sChave = Convert.ToBase64String(objSHA1.ComputeHash(objEncoding.GetBytes(Convert.ToString(DateTime.Now))));
            sChave = sChave.Replace("/", "");
            sChave = sChave.Replace("=", "");
            sChave = sChave.Replace("?", "");
            sChave = sChave.Replace("&", "");
            sChave = sChave.Replace("+", "");
            ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
            item.chave_confirmacao_email = sChave;
            item.data_email_confirmacao = DateTime.Now;
            item.email_confirmado = 0;
            item.email = txtEmail1Professor.Value.Trim();
            aplicacaoProfessor.AlterarItem(item);

            GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();

            Configuracoes itemEmail;
            // 1 = email mestrado@ipt.br
            // 2 = email suporte@ipt.br
            itemEmail = aplicacaoGerais.BuscaConfiguracoes(1);

            string qDe = itemEmail.remetente_email;
            string qDe_Nome = itemEmail.nome_remetente_email;
            string qPara = "";
            string qAssunto = "";
            if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString == "Producao")
            {
                qPara = txtEmail1Professor.Value.Trim();
            }
            else
            {
                // qPara = "kelsey@ipt.br";
                qPara = usuario.email;
                qAssunto = "Teste-email iria para: " + txtEmail1Professor.Value.Trim() + " - ";
            }
            string qCopia = "";
            string qCopiaOculta = "";
            qAssunto = qAssunto + "IPT - Sistema SAPIENS - Confirmação de e-mail";
            string qCorpo = "";
            qCorpo = qCorpo + "<br>Olá " +  item.nome + "<br><br>";         
            qCorpo = qCorpo + "<br>Seu email foi cadastrado no sistema SAPIENS do IPT . <br><br>";
            qCorpo = qCorpo + "Por favor, clique <a title=\"Clique aqui\" href=\"https://sapiens.ipt.br/confirmacaoemail.aspx?chave=" + sChave + "\">aqui</a> para a validação desse endereço.<br><br>";
            qCorpo = qCorpo + "Ou então copie e cole o seguinte endereço em seu navegador (browser): https://sapiens.ipt.br/confirmacaoemail.aspx?chave=" + sChave + "<br><br>";
            qCorpo = qCorpo + "Atenciosamente. Sistema SAPIENS.<br><br>";
            qCorpo = qCorpo + "<span style=\"color:#535353;font-size:11px\">E-mail enviado automaticamente pelo sistema SAPIENS (não responder). </span>";

            if (Utilizades.fEnviaEmail(qDe, qDe_Nome, qPara, qCopia, qCopiaOculta, qAssunto, qCorpo, itemEmail.servidor_email, itemEmail.conta_email, itemEmail.senha_email, itemEmail.porta_email.Value, 1, ""))
            {
                
            }

            if (bNovoProfesor)
            {
                qDe = itemEmail.remetente_email;
                qDe_Nome = itemEmail.nome_remetente_email;
                qAssunto = "Novo Prof. cadastrado: " + item.nome;
                if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString == "Producao")
                {
                    qPara = "mareol@ipt.br";
                }
                else
                {
                    // qPara = "kelsey@ipt.br";
                    qPara = usuario.email;
                    qAssunto = qAssunto + " - Teste-email iria para: " + "mareol@ipt.br";
                }
                qCopia = "";
                qCopiaOculta = "";
                qCorpo = "";
                qCorpo = qCorpo + "<br>Olá Marisa<br><br>";
                qCorpo = qCorpo + "<br>Um novo prof. foi cadastrado no sistema SAPIENS. <br><br>";
                qCorpo = qCorpo + "Nome: " + item.nome + "<br>";
                qCorpo = qCorpo + "Matrícula: " + item.id_professor + "<br><br>";
                qCorpo = qCorpo + "Atenciosamente. Sistema SAPIENS.<br><br>";
                qCorpo = qCorpo + "<span style=\"color:#535353;font-size:11px\">E-mail enviado automaticamente pelo sistema SAPIENS (não responder). </span>";

                if (Utilizades.fEnviaEmail(qDe, qDe_Nome, qPara, qCopia, qCopiaOculta, qAssunto, qCorpo, itemEmail.servidor_email, itemEmail.conta_email, itemEmail.senha_email, itemEmail.porta_email.Value, 1, ""))
                {

                }
            }

        }
        protected void bntSalvarProfessor_Click(object sender, EventArgs e)
        {

            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            string sAux = "";
            if (txtNomeProfessor.Value.Trim() == "")
            {
                sAux = "Preencher o nome do Professor. <br/><br/>";
            }
            if (txtCPFProfessor.Value.Trim() == "")
            {
                sAux = sAux + "Preencher o cpf do Professor. <br/><br/>";
            }
            if (optCPF.Checked)
            {
                if (!Utilizades.fValidaCPF(txtCPFProfessor.Value.Trim()))
                {
                    sAux = sAux + "Preencher um CPF válido. <br/><br/>";
                }
            }
            if (txtEmail1Professor.Value.Trim() == "")
            {
                sAux = sAux + "Preencher o e-mail principal do Professor. <br/><br/>";
            }
            else if (txtEmail1Professor.Value.Trim().IndexOf("@") == -1|| txtEmail1Professor.Value.Trim().IndexOf(".") == -1)
            {
                sAux = sAux + "Preencher um e-mail principal válido para o Professor. <br/><br/>";
            }
            if (ddlTituloProfessor.SelectedValue == "1" &&  ddlOrientacao.SelectedValue == "1")
            {
                sAux = sAux + "Deve ser indicado uma '<b>Forma de Recebimento para Orientações</b>' para  professores com o Título Acadêmico de '<b>Doutor</b>'. <br/><br/>";
            }
            if (sAux != "")
            {
                lblMensagem.Text = sAux;
                lblTituloMensagem.Text = "Atenção";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-warning');", true);
                return;
            }
            if (Session["sNewProfessor"] != null && (bool)Session["sNewProfessor"] != false)
            {
                //CriarProfessor();
                ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
                professores item = new professores();
                item.professores_forma_recebimento = new professores_forma_recebimento();
                professores itemRetorno = new professores();
                itemRetorno.id_professor = 0;
                itemRetorno.cpf = txtCPFProfessor.Value;

                if (itemRetorno.cpf != "000.000.000-00")
                {
                    itemRetorno = aplicacaoProfessor.VerificaCPFJaExistente(itemRetorno);
                    if (itemRetorno != null)
                    {
                        lblTituloMensagem.Text = "Atenção";
                        if (optCPF.Checked)
                        {
                            lblMensagem.Text = "Já existe outro registro com o nome: <strong>\"" + itemRetorno.nome + "\"</strong> com o mesmo <strong>CPF</strong>.<br /><br /> Favor verificar.";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fSetMaskaraCPF();AbreMensagem('alert-warning');", true);
                        }
                        else
                        {
                            lblMensagem.Text = "Já existe outro registro com o nome: <strong>\"" + itemRetorno.nome + "\"</strong> com o mesmo <strong>Passaporte</strong>.<br /><br /> Favor verificar.";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fUnsetMaskaraCPF();AbreMensagem('alert-warning');", true);
                        }
                        
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-warning');", true);
                        return;
                    }
                }

                itemRetorno = new professores();
                itemRetorno.email = txtEmail1Professor.Value.Trim();
                itemRetorno.email2 = txtEmail2Professor.Value.Trim();
                itemRetorno = aplicacaoProfessor.VerificaEmailJaExistente(itemRetorno);
                if (itemRetorno != null)
                {
                    lblTituloMensagem.Text = "Atenção";
                   
                    lblMensagem.Text = "Já existe outro registro de professor com o nome: <strong>\"" + itemRetorno.nome + "\"</strong> com o mesmo <strong>e-mail principal</strong>.<br /><br /> Favor verificar.";
                    if (optCPF.Checked)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fSetMaskaraCPF();AbreMensagem('alert-warning');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fUnsetMaskaraCPF();AbreMensagem('alert-warning');", true);
                    }
                    return;
                }


                item.data_alteracao = Convert.ToDateTime(DateTime.Now);
                item.data_cadastro = Convert.ToDateTime(DateTime.Now);
                item.usuario = usuario.usuario;
                item.status = "cadastrado";

                item.nome = txtNomeProfessor.Value;
                item.cpf = txtCPFProfessor.Value;
                if (optCPF.Checked)
                {
                    item.cpf_passaporte = 0;
                }
                else
                {
                    item.cpf_passaporte = 1;
                }

                item.sexo = ddlSexoProfessor.SelectedValue;
                if (txtDataNascimentoProfessor.Value != "")
                {
                    item.data_nasc = Convert.ToDateTime(txtDataNascimentoProfessor.Value);
                }
                item.tipo_documento = ddlTipoDoctoProfessor.SelectedValue;
                item.numero_documento = txtNumeroDoctoProfessor.Value;
                item.nacionalidade = txtNacionalidadeProfessor.Value;
                item.url_lattes = txtUrlLatesProfessor.Value;
                item.email = txtEmail1Professor.Value;
                item.email2 = txtEmail2Professor.Value;
                item.telefone_res = txtTelefoneProfessor.Value;
                item.celular_res = txtCelularProfessor.Value;

                //=======Residencia==========================
                item.cep_res = txtCepResidenciaProfessor.Value;
                item.endereco_res = txtLogradouroResidenciaProfessor.Value;
                item.numero_res = txtNumeroResidenciaProfessor.Value;
                item.complemento_res = txtComplementoResidenciaProfessor.Value;
                item.bairro_res = txtBairroResidenciaProfessor.Value;
                item.uf_res = ddlEstadoResidenciaProfessor.SelectedValue;
                item.cidade_res = ddlCidadeResidenciaProfessor.SelectedValue;
                item.Placa = txtPlacaProfessor.Value;

                //=======Título Acadêmico==========================
                item.id_titulo = Convert.ToInt32(ddlTituloProfessor.SelectedValue);
                item.ano_obtencao_titulo = txtAnoObtencaoProfessor.Value;
                item.local_obtencao_titulo = txtLocalTituloProfessor.Value;

                //=======Dados Bancários Pessoa Física==========================
                item.nome_banco = txtNomeBancoProfessor.Value;
                item.numero_banco = txtNumeroBancoProfessor.Value;
                item.agencia_numero = txtAgenciaProfessor.Value;
                item.conta_numero = txtNumeroContaProfessor.Value;

                //=======Dados Comerciais==========================
                item.empresa = txtEmpresaProfessor.Value;
                item.cep_empresa = txtCEPEmpresaProfessor.Value;
                item.logradouro_empresa = txtLogradouroEmpresaProfessor.Value;
                item.numero_empresa = txtNumeroEmpresaProfessor.Value;
                item.complemento_empresa = txtComplementoEmpresaProfessor.Value;
                item.bairro_empresa = txtBairroEmpresaProfessor.Value;
                item.pais_empresa = ddlPaisEmpresaProfessor.SelectedValue;
                if (divTXTEstadoEmpresaProfessor.Attributes.CssStyle.Value != "display:none;")
                {
                    item.uf_empresa = txtEmpresaProfessor.Value;
                }
                else
                {
                    item.uf_empresa = ddlEstadoEmpresaProfessor.SelectedValue;
                }
                if (divTXTCidadeEmpresaProfessor.Attributes.CssStyle.Value != "display:none;")
                {
                    item.cidade_empresa = txtCidadeEmpresa.Value;
                }
                else
                {
                    item.cidade_empresa = ddlCidadeEmpresaProfessor.SelectedValue;
                }
                item.cargo = txtCargoProfessor.Value;
                item.telefone_empresa = txtTelefoneEmpresaProfessor.Value;
                item.telefone_empresa_ramal = txtRamalEmpresaProfessor.Value;

                //=======Formas de Recebimento==========================
                item.professores_forma_recebimento.horas_aula = Convert.ToInt32(ddlHorasAula.SelectedValue);
                if (ddlHorasAula.SelectedValue != "2") //CLT - único que tem Horas Permitidas (horas_clt) e Pagamento Horas Adicionais (horas_aula_adicional)
                {
                    item.professores_forma_recebimento.horas_clt = 0;
                    item.professores_forma_recebimento.horas_aula_adicional = 1;
                }
                else
                {
                    if (txtHorasAulaCLT.Value != "")
                    {
                        item.professores_forma_recebimento.horas_clt = Convert.ToInt32(txtHorasAulaCLT.Value);
                    }
                    else
                    {
                        item.professores_forma_recebimento.horas_clt = 0;
                    }
                    item.professores_forma_recebimento.horas_aula_adicional = Convert.ToInt32(ddlHorasAulaAdicional.SelectedValue);
                }
                item.professores_forma_recebimento.orientacao = Convert.ToInt32(ddlOrientacao.SelectedValue);
                item.professores_forma_recebimento.banca = Convert.ToInt32(ddlBanca.SelectedValue);

                if (txtIdEmpresa.Value != "")
                {
                    item.professores_forma_recebimento.id_fornecedor = Convert.ToInt32(txtIdEmpresa.Value);
                }
                
                item.Observacao = txtObservacaoProfessor.Value.Trim();

                item = aplicacaoProfessor.CriarItem(item);

                usuarios itemUsurioProfessor = new usuarios();
                UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                SHA1CryptoServiceProvider objSHA1 = new SHA1CryptoServiceProvider();
                ASCIIEncoding objEncoding = new ASCIIEncoding();
                usuarios usuarioProfessor = new usuarios();
                usuarioProfessor.usuario = Convert.ToString(item.id_professor) + "p";
                usuarioProfessor.nome = item.nome;
                usuarioProfessor.un = "Acadêmico";
                usuarioProfessor.email = item.email;
                usuarioProfessor.id_grupo_acesso = 5;
                usuarioProfessor.status = 1;
                usuarioProfessor.avatar = "";
                usuarioProfessor.nomeSocial = item.nome.Substring(0, item.nome.IndexOf(" "));
                usuarioProfessor.senha = Convert.ToBase64String(objSHA1.ComputeHash(objEncoding.GetBytes(Convert.ToString(item.cpf))));

                aplicacaoUsuario.CriarUsuario(usuarioProfessor);

                lblAlteradoPor.Text = usuario.usuario;
                lblAlteradoEm.Text = DateTime.Today.ToString("dd/MM/yyyy");

                Session["professores"] = item;
                Session["sNewProfessor"] = false;
                Session["AdiciondoSucesso"] = true;

                Response.Redirect(Request.RawUrl);

            }
            else
            { //Alterar Professor
                ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
                professores item = (professores) Session["professores"];
                professores itemRetorno = new professores();
                itemRetorno.id_professor = item.id_professor;
                itemRetorno.cpf = txtCPFProfessor.Value;

                string sEnviaEmail = "";

                if (itemRetorno.cpf != "000.000.000-00")
                {
                    itemRetorno = aplicacaoProfessor.VerificaCPFJaExistente(itemRetorno);
                    if (itemRetorno != null)
                    {
                        lblTituloMensagem.Text = "Atenção";
                        if (optCPF.Checked)
                        {
                            lblMensagem.Text = "Já existe outro registro com o nome: <strong>\"" + itemRetorno.nome + "\"</strong> com o mesmo <strong>CPF</strong>.<br /><br /> Favor verificar.";
                            //lblCPF_Passaporte.InnerText = "CPF";
                            //iCPF_Passaporte.Attributes.Remove("class");
                            //iCPF_Passaporte.Attributes.Add("class", "fa fa-toggle-on");
                            hCPF_Passaporte.Value = "CPF";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fSetMaskaraCPF();AbreMensagem('alert-warning');", true);
                        }
                        else
                        {
                            lblMensagem.Text = "Já existe outro registro com o nome: <strong>\"" + itemRetorno.nome + "\"</strong> com o mesmo <strong>Passaporte</strong>.<br /><br /> Favor verificar.";
                            //lblCPF_Passaporte.InnerText = "Passaporte";
                            //iCPF_Passaporte.Attributes.Remove("class");
                            //iCPF_Passaporte.Attributes.Add("class", "fa fa-toggle-off");
                            hCPF_Passaporte.Value = "Passaporte";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fUnsetMaskaraCPF();AbreMensagem('alert-warning');", true);
                        }
                        return;
                    }
                }

                if (item.email != txtEmail1Professor.Value.Trim())
                {
                    itemRetorno = new professores();
                    itemRetorno.email = txtEmail1Professor.Value.Trim();
                    itemRetorno.email2 = txtEmail2Professor.Value.Trim();
                    itemRetorno = aplicacaoProfessor.VerificaEmailJaExistente(itemRetorno);
                    

                    if (itemRetorno != null)
                    {
                        lblTituloMensagem.Text = "Atenção";

                        lblMensagem.Text = "Já existe outro registro de professor com o nome: <strong>\"" + itemRetorno.nome + "\"</strong> com o mesmo <strong>e-mail principal</strong>.<br /><br /> Favor verificar.";
                        if (optCPF.Checked)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fSetMaskaraCPF();AbreMensagem('alert-warning');", true);
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fUnsetMaskaraCPF();AbreMensagem('alert-warning');", true);
                        }
                        return;
                    }

                    sEnviaEmail = "Um e-mail automático foi enviado para o professor <strong>\"" + item.nome + "\"</strong>, para que ele possa confirmar o novo endereço do <strong>e-mail principal</strong> cadastrado.";

                    subEnviaEmailConfirmacao(false);

                }
                
                item.data_alteracao = Convert.ToDateTime(DateTime.Now);
                item.usuario = usuario.usuario;
                item.status = "alterado";
                //item.cep_res = "teste";

                item.nome = txtNomeProfessor.Value;
                item.cpf=txtCPFProfessor.Value ;
                string sComando = "";
                if (optCPF.Checked)
                {
                    item.cpf_passaporte = 0;
                    sComando = "fSetMaskaraCPF();";
                }
                else
                {
                    item.cpf_passaporte = 1;
                    sComando = "fUnsetMaskaraCPF();";
                }

                item.sexo = ddlSexoProfessor.SelectedValue;
                if (txtDataNascimentoProfessor.Value != "")
                {
                    item.data_nasc = Convert.ToDateTime(txtDataNascimentoProfessor.Value);
                }
                else
                {
                    item.data_nasc = null;
                }

                item.tipo_documento=ddlTipoDoctoProfessor.SelectedValue;
                item.numero_documento=txtNumeroDoctoProfessor.Value;
                item.nacionalidade=txtNacionalidadeProfessor.Value;
                item.url_lattes=txtUrlLatesProfessor.Value;
                item.email=txtEmail1Professor.Value;
                item.email2=txtEmail2Professor.Value;
                item.telefone_res=txtTelefoneProfessor.Value;
                item.celular_res=txtCelularProfessor.Value;

                //=======Residencia==========================
                item.cep_res=txtCepResidenciaProfessor.Value;
                item.endereco_res=txtLogradouroResidenciaProfessor.Value;
                item.numero_res=txtNumeroResidenciaProfessor.Value;
                item.complemento_res=txtComplementoResidenciaProfessor.Value;
                item.bairro_res = txtBairroResidenciaProfessor.Value;
                item.uf_res= ddlEstadoResidenciaProfessor.SelectedValue;
                item.cidade_res=ddlCidadeResidenciaProfessor.SelectedValue;
                item.Placa = txtPlacaProfessor.Value;

                //=======Título Acadêmico==========================
                item.id_titulo = Convert.ToInt32(ddlTituloProfessor.SelectedValue);
                item.ano_obtencao_titulo = txtAnoObtencaoProfessor.Value;
                item.local_obtencao_titulo = txtLocalTituloProfessor.Value;

                //=======Dados Bancários Pessoa Física==========================
                item.nome_banco = txtNomeBancoProfessor.Value;
                item.numero_banco = txtNumeroBancoProfessor.Value;
                item.agencia_numero = txtAgenciaProfessor.Value;
                item.conta_numero = txtNumeroContaProfessor.Value;

                //=======Dados Comerciais==========================
                item.empresa = txtEmpresaProfessor.Value;
                item.cep_empresa = txtCEPEmpresaProfessor.Value;
                item.logradouro_empresa = txtLogradouroEmpresaProfessor.Value;
                item.numero_empresa = txtNumeroEmpresaProfessor.Value;
                item.complemento_empresa = txtComplementoEmpresaProfessor.Value;
                item.bairro_empresa = txtBairroEmpresaProfessor.Value;
                item.pais_empresa = ddlPaisEmpresaProfessor.SelectedValue;
                if (divTXTEstadoEmpresaProfessor.Attributes.CssStyle.Value != "display:none;")
                {
                    item.uf_empresa = txtEstadoEmpresaProfessor.Value;
                }
                else
                {
                    item.uf_empresa = ddlEstadoEmpresaProfessor.SelectedValue;
                }
                if (divTXTCidadeEmpresaProfessor.Attributes.CssStyle.Value != "display:none;")
                {
                    item.cidade_empresa = txtCidadeEmpresaProfessor.Value;
                }
                else
                {
                    item.cidade_empresa = ddlCidadeEmpresaProfessor.SelectedValue;
                }
                item.cargo = txtCargoProfessor.Value;
                item.telefone_empresa = txtTelefoneEmpresaProfessor.Value;
                item.telefone_empresa_ramal = txtRamalEmpresaProfessor.Value;

                //=======Formas de Recebimento==========================
                item.professores_forma_recebimento.horas_aula = Convert.ToInt32(ddlHorasAula.SelectedValue);
                if (ddlHorasAula.SelectedValue != "2") //CLT - único que tem Horas Permitidas (horas_clt) e Pagamento Horas Adicionais (horas_aula_adicional)
                {
                    item.professores_forma_recebimento.horas_clt = 0;
                    item.professores_forma_recebimento.horas_aula_adicional = 1;
                }
                else
                {
                    if (txtHorasAulaCLT.Value != "")
                    {
                        item.professores_forma_recebimento.horas_clt = Convert.ToInt32(txtHorasAulaCLT.Value);
                    }
                    else
                    {
                        item.professores_forma_recebimento.horas_clt = 0;
                    }
                    item.professores_forma_recebimento.horas_aula_adicional = Convert.ToInt32(ddlHorasAulaAdicional.SelectedValue);
                }
                
                item.professores_forma_recebimento.orientacao = Convert.ToInt32(ddlOrientacao.SelectedValue);
                item.professores_forma_recebimento.banca = Convert.ToInt32(ddlBanca.SelectedValue);
                if (divDadosEmpresa.Attributes.CssStyle.Value != "display:none;" && txtIdEmpresa.Value.Trim() != "")
                {
                    item.professores_forma_recebimento.id_fornecedor = Convert.ToInt32(txtIdEmpresa.Value);
                }

                item.Observacao = txtObservacaoProfessor.Value.Trim();

                aplicacaoProfessor.AlterarItem(item);

                UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                usuarios item_usuario_professor = new usuarios();
                item_usuario_professor.usuario = item.cpf.ToString();
                item_usuario_professor = aplicacaoUsuario.BuscaUsuario(item_usuario_professor);

                if (item_usuario_professor == null)
                {
                    item_usuario_professor = new usuarios();
                    item_usuario_professor.usuario = Convert.ToString(item.id_professor) + "p";
                    item_usuario_professor = aplicacaoUsuario.BuscaUsuario(item_usuario_professor);

                    if (item_usuario_professor != null)
                    {
                        item_usuario_professor.email = item.email;
                        aplicacaoUsuario.AlterarUsuario(item_usuario_professor);
                    }
                }
                else
                {
                    item_usuario_professor.email = item.email;
                    aplicacaoUsuario.AlterarUsuario(item_usuario_professor);
                }

                lblAlteradoPor.Text = usuario.usuario;
                lblAlteradoEm.Text = DateTime.Today.ToString("dd/MM/yyyy");

                lblMensagem.Text = "Alteração efetuada com sucesso.<br><br>" + sEnviaEmail;
                lblTituloMensagem.Text = "Atenção";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", sComando + "AbreMensagem('alert-success');", true);

                Session["professores"] = item;
            }
        }

        protected void btnImprimirCadastro_Click(object sender, EventArgs e)
        {
            try
            {
                GeraisAplicacao AplicacaoGerais = new GeraisAplicacao();
                professores item;
                item = (professores)Session["professores"];

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 120, 40);//estibulando o espaçamento das margens que queremos ===Antigo===(45, 40, 40, 40)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/Professor.pdf"), FileMode.Create));
                PDF_Cabec_Rodape_Professor pageHeaderFooter = new PDF_Cabec_Rodape_Professor();
                pageHeaderFooter.Caminho = Server.MapPath("~");
                writer.PageEvent = pageHeaderFooter;
                doc.Open();

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                float[] widths;
                Paragraph p;
                PdfPCell cell;

                //Aqui é uma nova tabela
                PdfPTable table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 230f };
                table.SetWidths(widths);

                //Aqui se desenha uma Linha
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                //p.Add(new Chunk("CET - Coordenadoria de Ensino Tecnológico \r\n", font_Verdana_8_Normal));
                p.Add(new Chunk(" \r\n", font_Verdana_8_Normal));
                p.Add(new Chunk(" \r\n", font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha uma Linha
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Relatório de Professor", font_Verdana_14_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui se desenha uma linha fina
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(linefine));
                doc.Add(p);
                //doc.Add(new Chunk(" ", font_Arialn_6_Normal));


                //Aqui é uma nova tabela
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 230f };
                table.SetWidths(widths);

                //Aqui se desenha uma Linha
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("\r\nDados Pessoais", font_Verdana_12_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);
                doc.Add(table);

                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(4);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 40f, 100f, 40f, 50f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Nome:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.nome, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("CPF:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.cpf, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.tipo_documento + ":", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.numero_documento, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Nascimento:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(String.Format("{0:dd/MM/yyyy}", item.data_nasc), font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Nacionalidade:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.nacionalidade, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Url CV Lattes:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.url_lattes, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Sexo:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                if (item.sexo == "m")
                {
                    p.Add(new Chunk("Masculino", font_Verdana_9_Bold));
                }
                else
                {
                    p.Add(new Chunk("Feminino", font_Verdana_9_Bold));
                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);
                doc.Add(table);

                //Aqui é uma nova tabela de Uma Coluna ============================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 230f };
                table.SetWidths(widths);

                //Aqui se desenha uma Linha
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("\r\nResidência", font_Verdana_12_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);
                doc.Add(table);

                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(4);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 40f, 100f, 40f, 50f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("CEP:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.cep_res, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Logradouro:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.endereco_res, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Número:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.numero_res, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Complemento:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.complemento_res, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Bairro:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.bairro_res, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Cidade:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.cidade_res, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Estado:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.uf_res, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);
                doc.Add(table);


                //Aqui é uma nova tabela de Uma Coluna ============================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 230f };
                table.SetWidths(widths);

                //Aqui se desenha uma Linha
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("\r\nContatos", font_Verdana_12_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);
                doc.Add(table);

                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(4);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 40f, 100f, 40f, 50f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Email (1):", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.email, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Tel:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.telefone_res, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Email (2):", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.email2, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Cel:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.celular_res, font_Verdana_10_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);
                doc.Add(table);


                //Aqui é uma nova tabela de Uma Coluna ============================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 230f };
                table.SetWidths(widths);

                //Aqui se desenha uma Linha
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("\r\nTítulo Acadêmico", font_Verdana_12_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);
                doc.Add(table);

                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(4);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 40f, 100f, 40f, 50f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Título:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.titulacao.nome, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Ano:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.ano_obtencao_titulo, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Local:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.local_obtencao_titulo, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);
                doc.Add(table);


                //Aqui é uma nova tabela de Uma Coluna ============================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 230f };
                table.SetWidths(widths);

                //Aqui se desenha uma Linha
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("\r\nDados Bancário Pessoa Física", font_Verdana_12_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);
                doc.Add(table);

                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(4);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 40f, 100f, 40f, 50f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Banco:", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.nome_banco, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("N.º:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.numero_banco, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("N.º Agência:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.agencia_numero, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("N.º Conta:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.conta_numero, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);
                doc.Add(table);


                //Aqui é uma nova tabela de Uma Coluna ============================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 230f };
                table.SetWidths(widths);

                //Aqui se desenha uma Linha
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("\r\nDados Comerciais", font_Verdana_12_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);
                doc.Add(table);

                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(4);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 40f, 100f, 40f, 50f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Empresa:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.empresa, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("CEP:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.cep_empresa, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Logradouro:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.logradouro_empresa, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Número:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.numero_empresa, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Complemento:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.complemento_empresa, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Bairro:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.bairro_empresa, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Cidade:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.cidade_empresa, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Estado:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.uf_empresa, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("País:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.pais_empresa, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Tel:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.telefone_empresa, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);
                
                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Cargo:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.cargo, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Ramal:", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.telefone_empresa_ramal, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);
                doc.Add(table);

                if (item.professores_forma_recebimento.fornecedores != null)
                {
                    //Aqui é uma nova tabela de Uma Coluna ============================================
                    table = new PdfPTable(1);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 230f };
                    table.SetWidths(widths);

                    //Aqui se desenha uma Linha
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk("\r\nForma Recebimento", font_Verdana_12_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);
                    doc.Add(table);

                    //Aqui é uma nova tabela de 4 Colunas ========================================================
                    table = new PdfPTable(4);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 40f, 100f, 40f, 50f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk("Empresa:", font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk(item.professores_forma_recebimento.fornecedores.nome, font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 3
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk("CNPJ:", font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 4
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk(item.professores_forma_recebimento.fornecedores.cnpj, font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk("Inscrição Estadual:", font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk(item.professores_forma_recebimento.fornecedores.inscricao_estadual, font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 3
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk("Cargo:", font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 4
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk(item.professores_forma_recebimento.fornecedores.cargo, font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk("CEP:", font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk(item.professores_forma_recebimento.fornecedores.cep_end, font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 3
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk("", font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 4
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk("", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk("Logradouro:", font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk(item.professores_forma_recebimento.fornecedores.logradouro_end, font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 3
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk("Número:", font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 4
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk(item.professores_forma_recebimento.fornecedores.numero_end, font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk("Complemento:", font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk(item.professores_forma_recebimento.fornecedores.comp_end, font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 3
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk("Bairro:", font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 4
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk(item.professores_forma_recebimento.fornecedores.bairro_end, font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk("Cidade:", font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk(item.professores_forma_recebimento.fornecedores.cidade_end, font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 3
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk("Estado:", font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 4
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk(item.professores_forma_recebimento.fornecedores.uf_end, font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk("Telefone:", font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk(item.professores_forma_recebimento.fornecedores.tel_contato, font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 3
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk("Celular:", font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 4
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk(item.professores_forma_recebimento.fornecedores.cel_contato, font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk("Fax:", font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk(item.professores_forma_recebimento.fornecedores.fax_contato, font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 3
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk("", font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 4
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk("", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);
                    doc.Add(table);
                }
                

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

                if (File.Exists(Server.MapPath("~/doctos/Professor.pdf")))
                {
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/Professor_" + txtNomeProfessor.Value + ".pdf")));
                    Response.WriteFile(Server.MapPath("~/doctos/Professor.pdf"));
                    Response.Flush();
                    Response.End();
                }

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão do Cadastro do Professor";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }
        }

        public class PDF_Cabec_Rodape_Professor : PdfPageEventHelper
        {
            public string Caminho;
            //public string PortariaMEC; Aqui é uma comunicação com o meio externo
            //public string DataMEC;
            //public string DataDOU;

            // write on top of document
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                base.OnOpenDocument(writer, document);
            }

            // write on start of each page
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);


                //PdfPTable tabFot = new PdfPTable(new float[] { 1f });
                ////tabFot.SpacingAfter = 10.0!
                //PdfPCell cell = default(PdfPCell);
                //tabFot.TotalWidth = 530f; //Aqui se seta se a tabela irá ficar mais a esquerda ou a direita

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_8_Italic = new Font(_bfVerdanaNormal, 8, Font.ITALIC);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                var lineStrong = new iTextSharp.text.pdf.draw.LineSeparator(2.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                iTextSharp.text.Image imgIPT = iTextSharp.text.Image.GetInstance(Caminho + "/img/ipt.gif");
                Paragraph p = new Paragraph();
                PdfPCell cell = default(PdfPCell);

                //Aqui se determina que é uma tabela com duas colunas
                PdfPTable table = new PdfPTable(2);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                //Aqui se determina se determina o tamanho de cada coluna
                float[] widths = new float[] { 60f, 170f };
                table.SetWidths(widths);

                //Coluna 1
                p = new Paragraph();
                imgIPT.SetAbsolutePosition(20, 755);
                imgIPT.ScalePercent(65);
                p.Add(new Chunk(imgIPT, 0, 0, true));
                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(p);
                cell = new PdfPCell(new Paragraph(new Chunk(imgIPT, 0, 0, true)));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                //cell.Colspan = 2;
                table.AddCell(cell);

                //Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Instituto de Pesquisas Tecnológicas do Estado de São Paulo S/A - IPT \r\n", font_Verdana_8_Normal));
                p.Add(new Chunk(" \r\n", font_Verdana_8_Normal));
                p.Add(new Chunk("Av. Prof. Almeida Prado, 532 - Prédio 56 - Térreo \r\n", font_Verdana_8_Normal));
                p.Add(new Chunk("Cidade Universitária - Butantã - CEP 05508-901 - São Paulo - SP  \r\n", font_Verdana_8_Normal));
                p.Add(new Chunk("Caixa Postal 0141 - 01064-970 - São Paulo - SP  \r\n", font_Verdana_8_Normal));
                p.Add(new Chunk("Tel.: (11)3767-4068, (11)3767-4624 Fax.: (11)3719-2449 - Email: mestrado@ipt.br  \r\n", font_Verdana_8_Normal));
                cell= new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);
                //(não sei, não sei, esquerda-direita, altura,não sei)
                table.WriteSelectedRows(0, -1, 42, (document.PageSize.Height - 40), writer.DirectContent);


                //Aqui é uma nova tabela
                PdfPTable table2 = new PdfPTable(1);
                table2.TotalWidth = 520f;
                table2.LockedWidth = true;

                widths = new float[] { 230f };
                table2.SetWidths(widths);

                //Aqui se desenha uma Linha
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(lineStrong));
                cell.AddElement(p);

                table2.AddCell(cell);
                //(não sei, não sei, esquerda-direita, altura,não sei)
                table2.WriteSelectedRows(0, -1, 42, (document.PageSize.Height - 100), writer.DirectContent);

            }

            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);
                var _bfArialNarrowNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_8_Italic = new Font(_bfVerdanaNormal, 8, Font.ITALIC);
                PdfPTable tabFot = new PdfPTable(new float[] { 1f });
                PdfPCell cell = default(PdfPCell);
                Paragraph p = new Paragraph();
                tabFot.TotalWidth = 520f;
                //p.Add(new Chunk("Data da Impressão: " + Strings.Format(Today, "dd/MM/yyyy") + Strings.Space(60) + "Página " + writer.PageNumber, font_Arialn_10_Normal));
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Instituto de Pesquisas Tecnológicas do Estado de São Paulo S/A - IPT \r\n", font_Verdana_8_Italic));
                p.Add(new Chunk("Av. Prof. Almeida Prado, 532 - Cidade Universitária - Butantã - 05508-901 - São Paulo - SP  \r\n", font_Verdana_8_Italic));
                p.Add(new Chunk("Caixa Postal 0141 - 01064-970 - São Paulo - SP - Tel.: (11)3767-4084, 3767-4624 - sapiens.ipt.br  \r\n", font_Verdana_8_Italic));
                cell = new PdfPCell(p);
                //cell.AddElement(p);

                //p = new Paragraph();
                ////etipulando o alinhamneto
                //p.Alignment = Element.ALIGN_CENTER;
                //p.Clear();
                //p.Add(new Chunk("Av. Prof. Almeida Prado, 532 - Cidade Universitária - Butantã - 05508-901 - São Paulo - SP" , font_Verdana_8_Italic));
                //cell.AddElement(p);

                //p = new Paragraph();
                ////etipulando o alinhamneto
                //p.Alignment = Element.ALIGN_CENTER;
                //p.Clear();
                //p.Add(new Chunk("Caixa Postal 0141 - 01064-970 - São Paulo - SP Tel.: (11)3767-4084, 3767-4624 - sapiens.ipt.br", font_Verdana_8_Italic));
                //cell.AddElement(p);

                //cell = new PdfPCell(p);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tabFot.AddCell(cell);
                tabFot.WriteSelectedRows(0, -1, 30, document.Bottom, writer.DirectContent);
            }

            //write on close of document
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);
            }
        }
    } //End Class


}