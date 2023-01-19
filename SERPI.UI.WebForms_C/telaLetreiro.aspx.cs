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
    public partial class telaLetreiro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 72)) // Monitor Letreiro - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                MonitorAplicacao aplicacaoMonitor = new MonitorAplicacao();
                monitor_letreiro item = new monitor_letreiro();

                item = aplicacaoMonitor.BuscaItem(item);

                if (item != null)
                {
                    txtDataCadastro.Value = String.Format("{0:dd/MM/yyyy}", item.data_cadastro);
                    txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy}", item.data_alteracao);
                    txtResponsavel.Value = item.usuario;

                    txtDescLetreiro.Value = item.descricao;
                }
                else
                {
                    txtDataCadastro.Value = String.Format("{0:dd/MM/yyyy}", DateTime.Now );
                    txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
                    txtResponsavel.Value = usuario.usuario;

                    txtDescLetreiro.Value = "";
                }
                
            }
        }

        protected void btnSalvar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";

                //if (txtDescLetreiro.Value.Trim() == "")
                //{
                //    sAux = sAux + "Preencher a Descrição do Letreiro. <br/><br/>";
                //}

                if (sAux != "")
                {
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                    return;
                }

                MonitorAplicacao aplicacaoMonitor = new MonitorAplicacao();
                monitor_letreiro item = new monitor_letreiro();

                item = aplicacaoMonitor.BuscaItem(item);

                if (item != null)
                {

                    //item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    item.descricao = txtDescLetreiro.Value.Trim();

                    aplicacaoMonitor.AlterarItem(item);

                    txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy}", item.data_alteracao);
                    txtResponsavel.Value = item.usuario;

                    lblMensagem.Text = "Letreiro alterado com sucesso.";
                    lblTituloMensagem.Text = "Alteração de Letreiro";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

                }
                else
                {
                    MonitorAplicacao aplicacaoEvento = new MonitorAplicacao();

                    item = new monitor_letreiro();

                    item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    item.descricao = txtDescLetreiro.Value.Trim();

                    aplicacaoMonitor.CriarItem(item);

                    txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy}", item.data_alteracao);
                    txtResponsavel.Value = item.usuario;

                    lblMensagem.Text = "Letreiro incluído com sucesso.";
                    lblTituloMensagem.Text = "Inclusão de Letreiro";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na manutenção do Letreiro. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Letreiro";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }

    }
}