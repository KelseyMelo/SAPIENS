
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
    public partial class admGrupoGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 2)) // 3. Cadastro Grupo - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                if (Session["sNewgrupos_acesso"] != null && (Boolean)Session["sNewgrupos_acesso"] != true)
                {
                    grupos_acesso item;
                    item = (grupos_acesso)Session["grupos_acesso"];
                    lblTituloPagina.Text = "(Editar) - N.º " + item.id_grupo;

                    txtNomeGrupo.Value = item.grupo;
                    txtDescricaoGrupo.Value = item.descricao;
                    
                    divGrupos.Visible = true;

                    if (Session["AdiciondoSucesso"] != null)
                    {
                        if ((Boolean)Session["AdiciondoSucesso"])
                        {
                            Session["AdiciondoSucesso"] = null;
                            lblMensagem.Text = "Novo Grupo do Sistema adicionado com sucesso";
                            lblTituloMensagem.Text = "Novo Grupo do Sistema";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
                        }
                    }
                }
                else
                {
                    lblTituloPagina.Text = "(novo)";
                    txtNomeGrupo.Value = "";
                    txtDescricaoGrupo.Value = "";
                    divGrupos.Visible = false;
                }
            }

        }


        protected void btnCriarGrupo_Click(object sender, EventArgs e)
        {
            Session["sNewgrupos_acesso"] = true;
            Session["grupos_acesso"] = null;
            Session["AdiciondoSucesso"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("admGrupo.aspx", true);
        }

        protected void btnSalvarGrupo_Click(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";

                if (txtNomeGrupo.Value.Trim() == "")
                {
                    sAux = sAux + "Digite um nome de Grupo. <br/><br/>";
                }

                if (txtDescricaoGrupo.Value.Trim() == "")
                {
                    sAux = sAux + "Digite uma Descrição do Grupo. <br/><br/>";
                }

                if (sAux != "")
                {
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                    return;
                }

                if (Session["sNewgrupos_acesso"] != null && (Boolean)Session["sNewgrupos_acesso"] != true)
                {

                    GrupoAplicacao aplicacaoGrupo = new GrupoAplicacao();
                    grupos_acesso item = new grupos_acesso();

                    item = (grupos_acesso)Session["grupos_acesso"];
                    item.grupo = txtNomeGrupo.Value.Trim();
                    item.descricao = txtDescricaoGrupo.Value.Trim();

                    aplicacaoGrupo.AlterarItem(item);

                    lblMensagem.Text = "Grupo do Sistema alterado com sucesso.";
                    lblTituloMensagem.Text = "Alteração de Grupo do Sistema";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');fAtiva_grdDisciplina();", true);

                    Session["grupos_acesso"] = item;

                }
                else
                {
                    GrupoAplicacao aplicacaoGrupo = new GrupoAplicacao();
                    grupos_acesso item = new grupos_acesso();

                    item.grupo = txtNomeGrupo.Value.Trim();
                    item.descricao = txtDescricaoGrupo.Value.Trim();

                    item = aplicacaoGrupo.CriarItem(item);

                    if (item != null)
                    {
                        Session["grupos_acesso"] = item;
                        Session.Add("sNewgrupos_acesso", false);
                        Session["AdiciondoSucesso"] = true;
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na manutenção do Grupo do Sistema. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Tela do Sistema";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }
    }
}