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
    public partial class cadUsuarioGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 1)) // 1. Cad Usuário - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                GrupoAplicacao aplicacaoGrupo = new GrupoAplicacao();
                grupos_acesso itemGrupo = new grupos_acesso();
                List<grupos_acesso> lista = aplicacaoGrupo.ListaItem(itemGrupo);

                ddlGrupo.Items.Clear();
                ddlGrupo.DataSource = lista.OrderBy(x => x.id_grupo);
                ddlGrupo.DataValueField = "id_grupo";
                ddlGrupo.DataTextField = "grupo";
                ddlGrupo.DataBind();
                ddlGrupo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Grupo", ""));
                ddlGrupo.SelectedValue = "";
                //ddlGrupo.SelectedValue = "1"; //Travado no Mestrado por enquanto
                //ddlGrupo.Enabled = false;

                if (Session["sNewUsuario"] != null && (Boolean)Session["sNewUsuario"] != true)
                {
                    usuarios item;
                    item = (usuarios)Session["usuarios"];
                    lblTituloPagina.Text = "(Editar) - " + item.usuario;

                    if (item.status.ToString() != "1")
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

                    divBloqueio.Visible = true;
                    //txtDataCadastro.Value = String.Format("{0:dd/MM/yyyy}", item.data_cadastro);
                    //txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy}", item.data_alteracao);
                    //txtStatus.Value = item.status;
                    //txtResponsavel.Value = item.usuario;


                    ddlGrupo.SelectedValue = item.id_grupo_acesso.ToString();
                    //ddlGrupo.Attributes.Add("disabled", "disabled");
                    //txtAnoUsuario.Attributes.Add("disabled", "disabled");
                    txtLoginUsuario.Value = item.usuario;
                    txtLoginUsuario.Attributes.Add("disabled", "disabled");
                    txtNomeUsuario.Value = item.nome;
                    txtEmailUsuario.Value = item.email;
                    txtNomeSocial.Value = item.nomeSocial;

                    if (item.usuarios_log.Count == 0)
                    {
                        optBloqueadoSim.Checked = false;
                        optBloqueadoNao.Checked = true;
                        txtDataBloqueio.Value = "";
                    }
                    else if (item.usuarios_log.ElementAt(0).status_bloqueio_total == 0)
                    {
                        optBloqueadoSim.Checked = false;
                        optBloqueadoNao.Checked = true;
                        txtDataBloqueio.Value = "";
                    }
                    else
                    {
                        optBloqueadoSim.Checked = true;
                        optBloqueadoNao.Checked = false;
                        txtDataBloqueio.Value = item.usuarios_log.ElementAt(0).data_bloqueio_total.Value.ToString("dd/MM/yyyy");
;                    }

                    //txtDataInicioUsuario.Value = String.Format("{0:yyyy-MM-dd}", item.data_inicio);
                    //txtDataFimUsuario.Value = String.Format("{0:yyyy-MM-dd}", item.data_fim);

                    btnCriarUsuario.Disabled = false;
                    //divEdicao.Visible = true;

                    if (Session["AdiciondoSucesso"] != null)
                    {
                        if ((Boolean)Session["AdiciondoSucesso"])
                        {
                            Session["AdiciondoSucesso"] = null;
                            lblMensagem.Text = "Novo Usuário adicionado com sucesso";
                            lblTituloMensagem.Text = "Novo Usuário";
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
                    divBloqueio.Visible = false;
                    //txtDataCadastro.Value = "";
                    //txtDataAlteracao.Value = "";
                    //txtStatus.Value = "";
                    //txtResponsavel.Value = "";

                    ddlGrupo.Attributes.Remove("disabled");
                    txtLoginUsuario.Attributes.Remove("disabled");
                    ddlGrupo.SelectedValue = "";
                    txtLoginUsuario.Value = "";
                    txtNomeUsuario.Value = "";

                    txtEmailUsuario.Value = "";
                    txtNomeSocial.Value = "";

                    btnCriarUsuario.Disabled = true;
                    //divEdicao.Visible = false;
                }
            }

        }

        protected void btnCriarUsuario_Click(object sender, EventArgs e)
        {
            Session["sNewUsuario"] = true;
            Session["usuarios"] = null;
            Session["AdiciondoSucesso"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("cadUsuario.aspx", true);
        }

        protected void btnSalvar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";

                if (ddlGrupo.SelectedValue == "")
                {
                    sAux = "Selecionar um Grupo. <br/><br/>";
                }

                if (txtNomeUsuario.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Nome do Usuário. <br/><br/>";
                }

                if (txtLoginUsuario.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Login do Usuário. <br/><br/>";
                }

                if (txtEmailUsuario.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o email do Usuário. <br/><br/>";
                }

                if (txtEmailUsuario.Value.Trim().IndexOf('@') == -1)
                {
                    sAux = sAux + "Preencher um email válido. <br/><br/>";
                }

                if (txtNomeSocial.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Nome Social do Usuário. <br/><br/>";
                }

                //DateTime temp;
                //if (!DateTime.TryParse(txtDataInicioUsuario.Value.Trim(), out temp))
                //{
                //    sAux = sAux + "Digite uma Data Início válida. <br/><br/>";
                //}
                //if (!DateTime.TryParse(txtDataFimUsuario.Value.Trim(), out temp))
                //{
                //    sAux = sAux + "Digite uma Data Fim válida. <br/><br/>";
                //}

                if (sAux != "")
                {
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                    return;
                }

                if (Session["sNewUsuario"] != null && (Boolean)Session["sNewUsuario"] != true)
                {
                    //Alteração de registro
                    UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                    usuarios item = new usuarios();

                    item = (usuarios)Session["usuarios"];

                    //item.status = "alterado";
                    ////item.data_cadastro = DateTime.Now;
                    //item.data_alteracao = DateTime.Now;
                    //item.usuario = usuario.usuario;

                    item.nome = txtNomeUsuario.Value.Trim();
                    item.email = txtEmailUsuario.Value.Trim();
                    item.nomeSocial = txtNomeSocial.Value.Trim();
                    item.id_grupo_acesso = Convert.ToInt32(ddlGrupo.SelectedValue);

                    aplicacaoUsuario.AlterarUsuario(item);

                    usuarios_log usuario_log = new usuarios_log();

                    if (item.usuarios_log.Count == 0)
                    {
                        usuario_log.usuario = item.usuario;
                        item.usuarios_log.Add(usuario_log);
                        item.usuarios_log.Add(aplicacaoUsuario.CriarUsuario_Log(usuario_log));

                    }
                    else
                    {
                        usuario_log = item.usuarios_log.ElementAt(0);
                    }

                    if (optBloqueadoSim.Checked)
                    {
                        usuario_log.status_bloqueio_total = 1;
                        usuario_log.data_bloqueio_total = DateTime.Now;
                        txtDataBloqueio.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        usuario_log.status_bloqueio_total = 0;
                        usuario_log.data_bloqueio_total = null;
                    }

                    usuario_log = aplicacaoUsuario.AlteraUsuario_Log(usuario_log);

                    //item = aplicacaoUsuario.BuscaItem(item);

                    //txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy}", item.data_alteracao);
                    //txtStatus.Value = item.status;
                    //txtResponsavel.Value = item.usuario;

                    lblMensagem.Text = "Usuário alterado com sucesso.";
                    lblTituloMensagem.Text = "Alteração de Usuário";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

                    Session["usuarios"] = item;

                }
                else
                {
                    //Inclusão de registro

                    if (ddlGrupo.SelectedValue == "")
                    {
                        sAux = "Selecionar um Grupo. <br/><br/>";
                    }

                    if (sAux != "")
                    {
                        lblMensagem.Text = sAux;
                        lblTituloMensagem.Text = "Atenção";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                        return;
                    }

                    UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();

                    usuarios item = new usuarios();

                    item.id_grupo_acesso = Convert.ToInt32(ddlGrupo.SelectedValue);
                    item.usuario = txtLoginUsuario.Value.Trim();
                    item.nome = txtNomeUsuario.Value.Trim();
                    item.email = txtEmailUsuario.Value.Trim();
                    item.nomeSocial = txtNomeSocial.Value.Trim();
                    item.status = 1;

                    if (aplicacaoUsuario.VerificaLoginUsado(item) != null)
                    {
                        lblMensagem.Text = "Já existe um usuário com esse Login. <br><br> Por favor, tente outro.";
                        lblTituloMensagem.Text = "Login já existente";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                        return;
                    }


                    aplicacaoUsuario.CriarUsuario(item);

                    if (item != null)
                    {
                        Session["usuarios"] = item;
                        Session.Add("sNewUsuario", false);
                        Session["AdiciondoSucesso"] = true;
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na manutenção do Usuário. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Usuário";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }

    }
}