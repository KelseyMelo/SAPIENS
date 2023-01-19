using System;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class telaVideo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 71)) // monitor_video Video - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                List<tipos_curso> listaTipoCurso = aplicacaoGerais.ListaTipoCurso();
                //var listaPais2 = from item2 in listaPais
                //                 select new
                //                 {
                //                     Id_Pais = item2.Id_Pais,
                //                     Nacionalidade = item2.Nacionalidade
                //                 };

                //ddlTipoCursoPeriodo.Items.Clear();
                //ddlTipoCursoPeriodo.DataSource = listaTipoCurso.OrderBy(x => x.id_tipo_curso);
                //ddlTipoCursoPeriodo.DataValueField = "id_tipo_curso";
                //ddlTipoCursoPeriodo.DataTextField = "tipo_curso";
                //ddlTipoCursoPeriodo.DataBind();
                //ddlTipoCursoPeriodo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os Tipos", ""));
                //ddlTipoCursoPeriodo.SelectedValue = "";

                if (Session["arrayFiltroMonitorVideo"] != null)
                {
                    CarregarDados();
                }
            }
            else
            {
                if (grdResultado.Rows.Count != 0)
                {
                    grdResultado.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        private void CarregarDados()
        {
            string[] arrayFiltroMonitorVideo = new string[2];

            monitor_video item = new monitor_video();

            arrayFiltroMonitorVideo = (string[])Session["arrayFiltroMonitorVideo"];

            if (arrayFiltroMonitorVideo[0] != "" && arrayFiltroMonitorVideo[0] != null)
            {
                item.descricao = arrayFiltroMonitorVideo[0];
                txtDescricaoVideo.Value = arrayFiltroMonitorVideo[0];
            }

            if (arrayFiltroMonitorVideo[1] != "" && arrayFiltroMonitorVideo[1] != null)
            {
                item.ativo = Convert.ToInt16(arrayFiltroMonitorVideo[1]);
                optSituacaoSim.Checked = false;
                optSituacaoNao.Checked = false;
                optSituacaoTodos.Checked = false;

                if (arrayFiltroMonitorVideo[1] == "1")
                {
                    optSituacaoSim.Checked = true;
                }
                else if (arrayFiltroMonitorVideo[1] == "0")
                {
                    optSituacaoNao.Checked = true;
                }
                else
                {
                    optSituacaoTodos.Checked = true;
                }
            }

            //Session["arrayFiltroMonitorVideo"] = arrayFiltroMonitorVideo;
            MonitorAplicacao aplicacaoMonitor = new MonitorAplicacao();
            List<monitor_video> listaMonitor = new List<monitor_video>();
            listaMonitor = aplicacaoMonitor.ListaItem(item);
            grdResultado.DataSource = listaMonitor;
            grdResultado.DataBind();

            if (listaMonitor.Count > 0)
            {
                grdResultado.UseAccessibleHeader = true;
                grdResultado.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultados.Visible = false;
                grdResultado.Visible = true;
            }
            else
            {
                msgSemResultados.Visible = true;
            }
            divResultados.Visible = true;
        }

        protected void btnPerquisaVideo_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroMonitorVideo = new string[2];

            if (txtDescricaoVideo.Value.Trim() != "")
            {
                arrayFiltroMonitorVideo[0] = txtDescricaoVideo.Value.Trim();
            }

            if (optSituacaoSim.Checked)
            {
                arrayFiltroMonitorVideo[1] = "1";
            }
            else if (optSituacaoNao.Checked)
            {
                arrayFiltroMonitorVideo[1] = "0";
            }
            else
            {
                arrayFiltroMonitorVideo[1] = "";
            }

            Session["arrayFiltroMonitorVideo"] = arrayFiltroMonitorVideo;

            CarregarDados();
        }

        //protected void grdResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Page")
        //    {
        //        return;
        //    }

        //    int linha = Convert.ToInt32(e.CommandArgument);
        //    int codigo = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0]);
        //    monitor_concentracao item = new monitor_concentracao();
        //    item.id_Monitor_concentracao = codigo;
        //    switch (grdResultado.DataKeys[linha].Values[1].ToString())
        //    {
        //        case "Editar":
        //            MonitorAplicacao aplicacaoMonitor = new MonitorAplicacao();
        //            item = aplicacaoMonitor.BuscaItem(item);
        //            Session.Add("monitor_concentracao", item);
        //            Session.Add("sNewMonitor", false);
        //            Response.Redirect("cadMonitorConcentracaoGestao.aspx", true);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        public void grdResultado_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                int linha = Convert.ToInt32(e.CommandArgument);
                //int codigo = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0]);
                monitor_video item = new monitor_video();
                MonitorAplicacao aplicacaoMonitor = new MonitorAplicacao();
                item.id_monitor_video = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0].ToString());
                item = aplicacaoMonitor.BuscaItem(item);
                Session["monitor_video"] = item;
                Session["sNewMonitorVideo"] = false;
                Response.Redirect("telaVideoGestao.aspx", true);
            }
        }

        public string setLinkAtivacao(int qId, string qDescricao, string qStatusAtivo)
        {
            if ((qStatusAtivo == "1"))
            {
                return ("<div title=\"Inativar\"> <a class=\"fa fa-toggle-on btn btn-circle btn-default\" href=\'javascript:AbreModalDesativar(\""
                            + (qId + ("\",\""
                            + (qDescricao + "\")\'; ></a></div>"))));
            }
            else
            {
                return ("<div title=\"Ativar\"> <a class=\"fa fa-toggle-off btn btn-circle btn-default\" href=\'javascript:AbreModalAtivar(\""
                            + (qId + ("\",\""
                            + (qDescricao + "\")\'; ></a></div>"))));
            }

        }

        protected void btnInativar_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            MonitorAplicacao aplicacaoMonitor = new MonitorAplicacao();
            monitor_video item = new monitor_video();
            item.id_monitor_video = Convert.ToInt32(Request["hCodigo"]);
            item.ativo = 0;
            item.data_alteracao = DateTime.Now;
            item.usuario = usuario.usuario;
            string sAux;

            try
            {
                if (aplicacaoMonitor.AlterarStatus(item) != null)
                {
                    sAux = "Vídeo inativado com sucesso!";
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Vídeo Inativado";
                    btnPerquisaVideo_Click(null, null);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem(\'alert-success\');", true);
                }
                else
                {
                    sAux = "Prezado Usuário,";
                    sAux = (sAux + "<br><br>");
                    sAux = (sAux + "Houve um erro ao Inativar o Vídeo selecionado.");
                    sAux = (sAux + "<br><br>");
                    sAux = (sAux + "Por favor, tente novamente.");
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "ERRO.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "javascript:AbreModalMensagem(\'alert-danger\');", true);
                }

            }
            catch (Exception ex)
            {
                sAux = "Prezado Usuário,";
                sAux = (sAux + "<br><br>");
                sAux = (sAux + "Houve um erro ao Inativar o Vídeo selecionado.");
                sAux = (sAux + "<br><br>");
                sAux = (sAux + "<strong>Erro: <strong>" + ex.Message);
                sAux = (sAux + "<br><br>");
                sAux = (sAux + "Por favor, tente novamente.");
                lblMensagem.Text = sAux;
                lblTituloMensagem.Text = "ERRO.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "javascript:AbreModalMensagem(\'alert-danger\');", true);
            }

        }

        protected void btnAtivar_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            MonitorAplicacao aplicacaoMonitor = new MonitorAplicacao();
            monitor_video item = new monitor_video();
            item.id_monitor_video = Convert.ToInt32(Request["hCodigo"]);
            item.ativo = 1;
            item.data_alteracao = DateTime.Now;
            item.usuario = usuario.usuario;
            string sAux;
            try
            {
                if (aplicacaoMonitor.AlterarStatus(item) != null)
                {
                    sAux = "Vídeo Ativado com sucesso!";
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Vídeo Ativado";
                    btnPerquisaVideo_Click(null, null);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem(\'alert-success\');", true);
                }
                else
                {
                    sAux = "Prezado Usuário,";
                    sAux = (sAux + "<br><br>");
                    sAux = (sAux + "Houve um erro ao Ativar o Vídeo selecionado.");
                    sAux = (sAux + "<br><br>");
                    sAux = (sAux + "Por favor, tente novamente.");
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "ERRO.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "javascript:AbreModalMensagem(\'alert-danger\');", true);
                }

            }
            catch (Exception ex)
            {
                sAux = "Prezado Usuário,";
                sAux = (sAux + "<br><br>");
                sAux = (sAux + "Houve um erro ao Ativar o Vídeo selecionado.");
                sAux = (sAux + "<br><br>");
                sAux = (sAux + "<strong>Erro: <strong>" + ex.Message);
                sAux = (sAux + "<br><br>");
                sAux = (sAux + "Por favor, tente novamente.");
                lblMensagem.Text = sAux;
                lblTituloMensagem.Text = "ERRO.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "javascript:AbreModalMensagem(\'alert-danger\');", true);
            }

        }

        protected void btnCriarVideo_Click(object sender, EventArgs e)
        {
            Session["sNewMonitorVideo"] = true;
            Session["monitor_video"] = null;
            Response.Redirect("telaVideoGestao.aspx", true);
        }
    }
}