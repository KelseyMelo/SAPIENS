using System;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class telaCadastro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 70)) // Monitor Cadastro - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
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

                if (Session["arrayFiltroMonitor"] != null)
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
            string[] arrayFiltroMonitor = new string[4];

            monitor item = new monitor();

            arrayFiltroMonitor = (string[])Session["arrayFiltroMonitor"];

            if (arrayFiltroMonitor[0] != "" && arrayFiltroMonitor[0] != null)
            {
                item.DescEventoMonitor = arrayFiltroMonitor[0];
                txtDescricaoEvento.Value = arrayFiltroMonitor[0];
            }

            if (arrayFiltroMonitor[1] != "" && arrayFiltroMonitor[1] != null)
            {
                item.DataEventoInicio = Convert.ToDateTime(arrayFiltroMonitor[1]);
                txtDataEvento.Value = arrayFiltroMonitor[1];
            }

            if (arrayFiltroMonitor[2] != "" && arrayFiltroMonitor[2] != null)
            {
                item.Coffee = arrayFiltroMonitor[2];
                optCoffeeSim.Checked = false;
                optCoffeeNao.Checked = false;
                optCoffeeTodos.Checked = false;

                if (arrayFiltroMonitor[2] == "sim")
                {
                    optCoffeeSim.Checked = true;
                }
                else if (arrayFiltroMonitor[2] == "nao")
                {
                    optCoffeeNao.Checked = true;
                }
                else
                {
                    optCoffeeTodos.Checked = true;
                }
            }

            if (arrayFiltroMonitor[3] != "" && arrayFiltroMonitor[3] != null)
            {
                item.ativo = Convert.ToInt16(arrayFiltroMonitor[3]);
                optSituacaoSim.Checked = false;
                optSituacaoNao.Checked = false;
                optSituacaoTodos.Checked = false;

                if (arrayFiltroMonitor[3] == "1")
                {
                    optSituacaoSim.Checked = true;
                }
                else if (arrayFiltroMonitor[3] == "0")
                {
                    optSituacaoNao.Checked = true;
                }
                else
                {
                    optSituacaoTodos.Checked = true;
                }
            }

            //Session["arrayFiltroMonitor"] = arrayFiltroMonitor;
            MonitorAplicacao aplicacaoMonitor = new MonitorAplicacao();
            List<monitor> listaMonitor = new List<monitor>();
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

        protected void btnPerquisaMonitor_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroMonitor = new string[4];

            if (txtDescricaoEvento.Value.Trim() != "")
            {
                arrayFiltroMonitor[0] = txtDescricaoEvento.Value.Trim();
            }

            if (txtDataEvento.Value != "")
            {
                arrayFiltroMonitor[1] = txtDataEvento.Value;
            }

            if (optCoffeeSim.Checked)
            {
                arrayFiltroMonitor[2] = "sim";
            }
            else if (optCoffeeNao.Checked)
            {
                arrayFiltroMonitor[2] = "nao";
            }
            else
            {
                arrayFiltroMonitor[2] = "todos";
            }

            if (optSituacaoSim.Checked)
            {
                arrayFiltroMonitor[3] = "1";
            }
            else if (optSituacaoNao.Checked)
            {
                arrayFiltroMonitor[3] = "0";
            }
            else
            {
                arrayFiltroMonitor[3] = "";
            }

            Session["arrayFiltroMonitor"] = arrayFiltroMonitor;

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
                monitor item = new monitor();
                MonitorAplicacao aplicacaoMonitor = new MonitorAplicacao();
                item.id_monitor = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0].ToString());
                item = aplicacaoMonitor.BuscaItem(item);
                Session["monitor"] = item;
                Session["sNewMonitor"] = false;
                Response.Redirect("telaCadastroGestao.aspx", true);
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
            monitor item = new monitor();
            item.id_monitor = Convert.ToInt32(Request["hCodigo"]);
            item.ativo = 0;
            item.DataAlteracao = DateTime.Now;
            item.usuario = usuario.usuario;
            string sAux;
            
            try
            {
                if (aplicacaoMonitor.AlterarStatus(item) != null)
                {
                    sAux = "Evento inativado com sucesso!";
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Evento Inativado";
                    btnPerquisaMonitor_Click(null, null);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem(\'alert-success\');", true);
                }
                else
                {
                    sAux = "Prezado Usuário,";
                    sAux = (sAux + "<br><br>");
                    sAux = (sAux + "Houve um erro ao Inativar o Evento selecionado.");
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
                sAux = (sAux + "Houve um erro ao Inativar o Evento selecionado.");
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
            monitor item = new monitor();
            item.id_monitor = Convert.ToInt32(Request["hCodigo"]);
            item.ativo = 1;
            item.DataAlteracao = DateTime.Now;
            item.usuario = usuario.usuario;
            string sAux;
            try
            {
                if (aplicacaoMonitor.AlterarStatus(item) != null)
                {
                    sAux = "Evento Ativado com sucesso!";
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Evento Ativado";
                    btnPerquisaMonitor_Click(null, null);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem(\'alert-success\');", true);
                }
                else
                {
                    sAux = "Prezado Usuário,";
                    sAux = (sAux + "<br><br>");
                    sAux = (sAux + "Houve um erro ao Ativar o Evento selecionado.");
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
                sAux = (sAux + "Houve um erro ao Ativar o Evento selecionado.");
                sAux = (sAux + "<br><br>");
                sAux = (sAux + "<strong>Erro: <strong>" + ex.Message);
                sAux = (sAux + "<br><br>");
                sAux = (sAux + "Por favor, tente novamente.");
                lblMensagem.Text = sAux;
                lblTituloMensagem.Text = "ERRO.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "javascript:AbreModalMensagem(\'alert-danger\');", true);
            }

        }

        protected void btnCriarMonitor_Click(object sender, EventArgs e)
        {
            Session["sNewMonitor"] = true;
            Session["monitor"] = null;
            Response.Redirect("telaCadastroGestao.aspx", true);
        }
    }
}