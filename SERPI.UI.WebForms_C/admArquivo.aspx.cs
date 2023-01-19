using System;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class admArquivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 73 || x.id_tela == 12)) // Tipo Curso ou Curso - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
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

                if (Session["arrayFiltroArquivo"] != null)
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
            string[] arrayFiltroArquivo = new string[3];

            arquivos item = new arquivos();

            arrayFiltroArquivo = (string[])Session["arrayFiltroArquivo"];

            if (arrayFiltroArquivo[0] != "" && arrayFiltroArquivo[0] != null)
            {
                item.descricao = arrayFiltroArquivo[0];
                txtDescricaoArquivo.Value = arrayFiltroArquivo[0];
            }

            if (arrayFiltroArquivo[1] != "" && arrayFiltroArquivo[1] != null)
            {
                item.nome_arquivo = arrayFiltroArquivo[1];
                txtNomeArquivo.Value = arrayFiltroArquivo[1];
            }

            if (arrayFiltroArquivo[2] != "" && arrayFiltroArquivo[2] != null)
            {
                item.tipo_arquivo = arrayFiltroArquivo[2];
                ddlTipoArquivo.SelectedValue = arrayFiltroArquivo[2];
            }

            //if (arrayFiltroArquivo[1] != "" && arrayFiltroArquivo[1] != null)
            //{
            //    item.ativo = Convert.ToInt16(arrayFiltroArquivo[1]);
            //    optSituacaoSim.Checked = false;
            //    optSituacaoNao.Checked = false;
            //    optSituacaoTodos.Checked = false;

            //    if (arrayFiltroArquivo[1] == "1")
            //    {
            //        optSituacaoSim.Checked = true;
            //    }
            //    else if (arrayFiltroArquivo[1] == "0")
            //    {
            //        optSituacaoNao.Checked = true;
            //    }
            //    else
            //    {
            //        optSituacaoTodos.Checked = true;
            //    }
            //}

            //Session["arrayFiltroArquivo"] = arrayFiltroArquivo;
            ArquivoAplicacao aplicacaoAplicacao = new ArquivoAplicacao();
            List<arquivos> listaArquivo = new List<arquivos>();
            listaArquivo = aplicacaoAplicacao.ListaItem(item);
            grdResultado.DataSource = listaArquivo;
            grdResultado.DataBind();

            if (listaArquivo.Count > 0)
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

        protected void btnPerquisaArquivo_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroArquivo = new string[3];

            if (txtDescricaoArquivo.Value.Trim() != "")
            {
                arrayFiltroArquivo[0] = txtDescricaoArquivo.Value.Trim();
            }

            if (txtNomeArquivo.Value.Trim() != "")
            {
                arrayFiltroArquivo[1] = txtNomeArquivo.Value.Trim();
            }

            arrayFiltroArquivo[2] = ddlTipoArquivo.SelectedValue;

            //if (optSituacaoSim.Checked)
            //{
            //    arrayFiltroArquivo[1] = "1";
            //}
            //else if (optSituacaoNao.Checked)
            //{
            //    arrayFiltroArquivo[1] = "0";
            //}
            //else
            //{
            //    arrayFiltroArquivo[1] = "";
            //}

            Session["arrayFiltroArquivo"] = arrayFiltroArquivo;

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
        //            ArquivoAplicacao aplicacaoMonitor = new ArquivoAplicacao();
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
                arquivos item = new arquivos();
                ArquivoAplicacao aplicacaoArquivo = new ArquivoAplicacao();
                item.id_arquivo = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0].ToString());
                item = aplicacaoArquivo.BuscaItem(item);
                Session["arquivos"] = item;
                Session["sNewArquivos"] = false;
                Response.Redirect("admArquivoGestao.aspx", true);
            }
        }

        public string setNomeArquivo(string qLink, DateTime qData)
        {
           
            string sAux;

            sAux = "<div title=\"Download do arquivo\"> <a target=\"_blank\" href=\'./Anexos/" + qLink + "?v=" + String.Format("{0:yyyyMMddHHmmss}", qData) + "\'; >" + qLink + "</a></div>";

            return sAux;
        }

        public string setCopiarLink(string qLink, DateTime qData)
        {

            string sAux;

            sAux = "<div title=\"Copiar link\"> <a class=\"fa fa-copy btn btn-circle btn-default\" href=\'javascript:fCopyUrl(\"sapiens.ipt.br/Anexos/" + qLink + "?v=" + String.Format("{0:yyyyMMddHHmmss}", qData) + "\")\'; ></a></div>";

            return sAux;
        }

        public string setApagarArquivo(int qIdArquivo, string qNomeArquivo)
        {

            string sAux;

            sAux = "<div title=\"Apagar Arquivo\"> <a class=\"fa fa-eraser btn btn-circle btn-danger\" href=\'javascript:fAbreModalApagarArquivo(\"" + qIdArquivo + "\",\"" + qNomeArquivo + "\")\'; ></a></div>";

            return sAux;
        }

        protected void btnApagarArquivo_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            string sAux;
            ArquivoAplicacao aplicacaoAplicacao = new ArquivoAplicacao();
            arquivos item = new arquivos();
            item.id_arquivo = Convert.ToInt32(Request["hCodigo"]);
            item = aplicacaoAplicacao.BuscaItem(item);

            List<tipos_curso> listaTipoCurso;
            List<cursos> listaCurso;

            listaTipoCurso = aplicacaoAplicacao.ListaTipoCurso_comArquivo(item);

            if (listaTipoCurso.Count > 0)
            {
                sAux = "";
                foreach (var elemento in listaTipoCurso)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "; ";
                    }
                    sAux = sAux + elemento.tipo_curso;
                }

                sAux = "Esse arquivo está sendo utilizado na descrição do(s) tipo(s) de curso: " + sAux + ".<br><br>Para poder apagar esse arquivo deve-se excluí-lo dessa(s) descrição(ões).";

                lblMensagem.Text = sAux;
                lblTituloMensagem.Text = "Arquivo Utilizado";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem(\'alert-warning\');", true);
                return;

            }

            listaCurso = aplicacaoAplicacao.ListaCurso_comArquivo(item);

            if (listaCurso.Count > 0)
            {
                sAux = "";
                foreach (var elemento in listaCurso)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "; ";
                    }
                    sAux = sAux + elemento.nome;
                }

                sAux = "Esse arquivo está sendo utilizado na descrição do(s) curso(s): " + sAux + ".<br><br>Para poder apagar esse arquivo deve-se excluí-lo dessa(s) descrição(ões).";

                lblMensagem.Text = sAux;
                lblTituloMensagem.Text = "Arquivo Utilizado";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem(\'alert-warning\');", true);
                return;
            }

            try
            {
                item.ativo = 0;

                if (aplicacaoAplicacao.AlterarStatus(item) != null)
                {
                    sAux = "Arquivo Apagado com sucesso!";
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Arquivo Apagado";
                    btnPerquisaArquivo_Click(null, null);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem(\'alert-success\');", true);
                }
                else
                {
                    sAux = "Prezado Usuário,";
                    sAux = (sAux + "<br><br>");
                    sAux = (sAux + "Houve um erro ao Apagado o Arquivo selecionado.");
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
                sAux = (sAux + "Houve um erro ao Apagar o Arquivo selecionado.");
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

            ArquivoAplicacao aplicacaoMonitor = new ArquivoAplicacao();
            arquivos item = new arquivos();
            item.id_arquivo = Convert.ToInt32(Request["hCodigo"]);
            item.ativo = 1;
            item.data_alteracao = DateTime.Now;
            item.usuario = usuario.usuario;
            string sAux;
            try
            {
                if (aplicacaoMonitor.AlterarStatus(item) != null)
                {
                    sAux = "Arquivo Ativado com sucesso!";
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Arquivo Ativado";
                    btnPerquisaArquivo_Click(null, null);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem(\'alert-success\');", true);
                }
                else
                {
                    sAux = "Prezado Usuário,";
                    sAux = (sAux + "<br><br>");
                    sAux = (sAux + "Houve um erro ao Ativar o Arquivo selecionado.");
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
                sAux = (sAux + "Houve um erro ao Ativar o Arquivo selecionado.");
                sAux = (sAux + "<br><br>");
                sAux = (sAux + "<strong>Erro: <strong>" + ex.Message);
                sAux = (sAux + "<br><br>");
                sAux = (sAux + "Por favor, tente novamente.");
                lblMensagem.Text = sAux;
                lblTituloMensagem.Text = "ERRO.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "javascript:AbreModalMensagem(\'alert-danger\');", true);
            }

        }

        protected void btnCriarArquivo_Click(object sender, EventArgs e)
        {
            Session["sNewArquivos"] = true;
            Session["arquivos"] = null;
            Response.Redirect("admArquivoGestao.aspx", true);
        }
    }
}