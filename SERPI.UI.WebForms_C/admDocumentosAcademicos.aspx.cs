using System;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class admDocumentosAcademicos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 74)) // Tipo Documentos Acadêmicos - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
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

                if (Session["arrayFiltroDocumentosAcademicos"] != null)
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
            string[] arrayFiltroDocumentosAcademicos = new string[4];

            documentos_academicos item = new documentos_academicos();

            arrayFiltroDocumentosAcademicos = (string[])Session["arrayFiltroDocumentosAcademicos"];

            if (arrayFiltroDocumentosAcademicos[0] != "" && arrayFiltroDocumentosAcademicos[0] != null)
            {
                item.descricao = arrayFiltroDocumentosAcademicos[0];
                txtDescricaoDocumentoAcademico.Value = arrayFiltroDocumentosAcademicos[0];
            }

            if (arrayFiltroDocumentosAcademicos[1] != "" && arrayFiltroDocumentosAcademicos[1] != null)
            {
                item.nome = arrayFiltroDocumentosAcademicos[1];
                txtNomeDocumentoAcademico.Value = arrayFiltroDocumentosAcademicos[1];
            }

            if (arrayFiltroDocumentosAcademicos[2] != "" && arrayFiltroDocumentosAcademicos[2] != null)
            {
                item.tipo_arquivo = arrayFiltroDocumentosAcademicos[2];
                ddlTipoDocumentoAcademico.SelectedValue = arrayFiltroDocumentosAcademicos[2];
            }

            if (arrayFiltroDocumentosAcademicos[3] != "" && arrayFiltroDocumentosAcademicos[3] != null)
            {
                item.ativo = Convert.ToInt16(arrayFiltroDocumentosAcademicos[3]);
                optSituacaoSim.Checked = false;
                optSituacaoNao.Checked = false;
                optSituacaoTodos.Checked = false;

                if (arrayFiltroDocumentosAcademicos[3] == "1")
                {
                    optSituacaoSim.Checked = true;
                }
                else if (arrayFiltroDocumentosAcademicos[3] == "0")
                {
                    optSituacaoNao.Checked = true;
                }
                else
                {
                    optSituacaoTodos.Checked = true;
                }
            }

            //Session["arrayFiltroDocumentosAcademicos"] = arrayFiltroDocumentosAcademicos;
            DocumentosAcademicosAplicacao aplicacaoAplicacao = new DocumentosAcademicosAplicacao();
            List<documentos_academicos> listaDocumentoAcademico = new List<documentos_academicos>();
            listaDocumentoAcademico = aplicacaoAplicacao.ListaItem(item);
            grdResultado.DataSource = listaDocumentoAcademico;
            grdResultado.DataBind();

            if (listaDocumentoAcademico.Count > 0)
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

        protected void btnPerquisaDocumentoAcademico_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroDocumentosAcademicos = new string[4];

            if (txtDescricaoDocumentoAcademico.Value.Trim() != "")
            {
                arrayFiltroDocumentosAcademicos[0] = txtDescricaoDocumentoAcademico.Value.Trim();
            }

            if (txtNomeDocumentoAcademico.Value.Trim() != "")
            {
                arrayFiltroDocumentosAcademicos[1] = txtNomeDocumentoAcademico.Value.Trim();
            }

            arrayFiltroDocumentosAcademicos[2] = ddlTipoDocumentoAcademico.SelectedValue;

            if (optSituacaoSim.Checked)
            {
                arrayFiltroDocumentosAcademicos[3] = "1";
            }
            else if (optSituacaoNao.Checked)
            {
                arrayFiltroDocumentosAcademicos[3] = "0";
            }
            else
            {
                arrayFiltroDocumentosAcademicos[3] = "";
            }

            Session["arrayFiltroDocumentosAcademicos"] = arrayFiltroDocumentosAcademicos;

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
        //            DocumentosAcademicosAplicacao aplicacaoMonitor = new DocumentosAcademicosAplicacao();
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
                documentos_academicos item = new documentos_academicos();
                DocumentosAcademicosAplicacao aplicacaoDocumentoAcademico = new DocumentosAcademicosAplicacao();
                item.id_documentos_academicos = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0].ToString());
                item = aplicacaoDocumentoAcademico.BuscaItem(item);
                Session["documentos_academicos"] = item;
                Session["sNewdocumentos_academicos"] = false;
                Response.Redirect("admDocumentosAcademicosGestao.aspx", true);
            }
        }

        public string setNomeDocumentoAcademico(string qLink, DateTime qData)
        {

            string sAux;

            sAux = "<div title=\"Download do Documento Acadêmico\"> <a target=\"_blank\" href=\'./DocumentosAcademicos/" + qLink + "?v=" + String.Format("{0:yyyyMMddHHmmss}", qData) + "\'; >" + qLink + "</a></div>";

            return sAux;
        }

        public string setCopiarLink(string qLink, DateTime qData)
        {

            string sAux;

            sAux = "<div title=\"Copiar link\"> <a class=\"fa fa-copy btn btn-circle btn-default\" href=\'javascript:fCopyUrl(\"sapiens.ipt.br/DocumentosAcademicos/" + qLink + "?v=" + String.Format("{0:yyyyMMddHHmmss}", qData) + "\")\'; ></a></div>";

            return sAux;
        }

        public string setAtivaInativarDocumentoAcademico(int qIdDocumentoAcademico, string qNomeDocumentoAcademico, int Ativo)
        {

            string sAux;

            if (Ativo == 1)
            {
                sAux = "<div title=\"Inativar Documento Acadêmico\"> <a class=\"fa fa-toggle-on btn btn-circle btn-default\" href=\'javascript:fAbreModalInativarDocumentoAcademico(\"" + qIdDocumentoAcademico + "\",\"" + qNomeDocumentoAcademico + "\")\'; ></a></div>";
            }
            else
            {
                sAux = "<div title=\"Ativar Documento Acadêmico\"> <a class=\"fa fa-toggle-off btn btn-circle btn-default\" href=\'javascript:fAbreModalAtivarDocumentoAcademico(\"" + qIdDocumentoAcademico + "\",\"" + qNomeDocumentoAcademico + "\")\'; ></a></div>";
            }

            return sAux;
        }

        public string setStatusHomepage(int qStatusAprovado)
        {
            string sAux;

            // 0 = Aguardando aprovação -- 1 = Aprovado -- 2 = Reprovado -- 3 = Alterado
            if (qStatusAprovado == 0)
            {
                sAux = "<div class='text-primary'><strong class='piscante'>Aguardando Aprovação</strong></div>";
            }
            else if (qStatusAprovado == 2)
            {
                sAux = "<div class='text-danger'><strong class='piscante'>Reprovado</strong></div>";
            }
            else if (qStatusAprovado == 1)
            {
                sAux = "<div class='text-success'><strong>Aprovado</strong></div>";
            }
            else
            {
                sAux = "<div class='text-orange'><strong class='piscante'> Enviar para Aprovação </strong></div>";
            }
            return sAux;
        }

        protected void btnInativarDocumentoAcademico_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            string sAux;
            DocumentosAcademicosAplicacao aplicacaoAplicacao = new DocumentosAcademicosAplicacao();
            documentos_academicos item = new documentos_academicos();
            item.id_documentos_academicos = Convert.ToInt32(Request["hCodigo"]);
            item = aplicacaoAplicacao.BuscaItem(item);

            try
            {
                item.ativo = 0;

                if (aplicacaoAplicacao.AlterarStatus(item) != null)
                {
                    sAux = "Documento Acadêmico Inativado com sucesso!";
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Documento Acadêmico Inativado";
                    btnPerquisaDocumentoAcademico_Click(null, null);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem(\'alert-success\');", true);
                }
                else
                {
                    sAux = "Prezado Usuário,";
                    sAux = (sAux + "<br><br>");
                    sAux = (sAux + "Houve um erro ao Inativar o Documento Acadêmico selecionado.");
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
                sAux = (sAux + "Houve um erro ao Apagar o Documento Acadêmico selecionado.");
                sAux = (sAux + "<br><br>");
                sAux = (sAux + "<strong>Erro: <strong>" + ex.Message);
                sAux = (sAux + "<br><br>");
                sAux = (sAux + "Por favor, tente novamente.");
                lblMensagem.Text = sAux;
                lblTituloMensagem.Text = "ERRO.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "javascript:AbreModalMensagem(\'alert-danger\');", true);
            }

        }

        protected void btnAtivarDocumentoAcademico_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            string sAux;
            DocumentosAcademicosAplicacao aplicacaoAplicacao = new DocumentosAcademicosAplicacao();
            documentos_academicos item = new documentos_academicos();
            item.id_documentos_academicos = Convert.ToInt32(Request["hCodigo"]);
            item = aplicacaoAplicacao.BuscaItem(item);

            try
            {
                item.ativo = 1;

                if (aplicacaoAplicacao.AlterarStatus(item) != null)
                {
                    sAux = "Documento Acadêmico Ativado com sucesso!";
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Documento Acadêmico Ativado";
                    btnPerquisaDocumentoAcademico_Click(null, null);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem(\'alert-success\');", true);
                }
                else
                {
                    sAux = "Prezado Usuário,";
                    sAux = (sAux + "<br><br>");
                    sAux = (sAux + "Houve um erro ao Ativado o Documento Acadêmico selecionado.");
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
                sAux = (sAux + "Houve um erro ao Apagar o Documento Acadêmico selecionado.");
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

            DocumentosAcademicosAplicacao aplicacaoMonitor = new DocumentosAcademicosAplicacao();
            documentos_academicos item = new documentos_academicos();
            item.id_documentos_academicos = Convert.ToInt32(Request["hCodigo"]);
            item.ativo = 1;
            item.data_alteracao = DateTime.Now;
            item.usuario = usuario.usuario;
            string sAux;
            try
            {
                if (aplicacaoMonitor.AlterarStatus(item) != null)
                {
                    sAux = "Documento Acadêmico Ativado com sucesso!";
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Documento Acadêmico Ativado";
                    btnPerquisaDocumentoAcademico_Click(null, null);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem(\'alert-success\');", true);
                }
                else
                {
                    sAux = "Prezado Usuário,";
                    sAux = (sAux + "<br><br>");
                    sAux = (sAux + "Houve um erro ao Ativar o Documento Acadêmico selecionado.");
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
                sAux = (sAux + "Houve um erro ao Ativar o Documento Acadêmico selecionado.");
                sAux = (sAux + "<br><br>");
                sAux = (sAux + "<strong>Erro: <strong>" + ex.Message);
                sAux = (sAux + "<br><br>");
                sAux = (sAux + "Por favor, tente novamente.");
                lblMensagem.Text = sAux;
                lblTituloMensagem.Text = "ERRO.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "javascript:AbreModalMensagem(\'alert-danger\');", true);
            }

        }

        protected void btnCriarDocumentoAcademico_Click(object sender, EventArgs e)
        {
            Session["sNewdocumentos_academicos"] = true;
            Session["documentos_academicos"] = null;
            Response.Redirect("admDocumentosAcademicosGestao.aspx", true);
        }
    }
}