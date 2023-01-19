using System;
using SERPI.Dominio_C;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aplicacao_C;
using System.Text;

namespace SERPI.UI.WebForms_C
{
    public partial class Principal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];
                if (usuario== null)
                {
                    Response.Redirect("index.html", true);
                }

                GeraisAplicacao aplicacaoGarais = new GeraisAplicacao();
                List<tipos_curso> listaTipoCurso = aplicacaoGarais.ListaTipoCurso();

                ddlTipoCursoPrincipal.Items.Clear();
                ddlTipoCursoPrincipal.DataSource = listaTipoCurso.Where(x=> x.id_tipo_curso == 1 || x.id_tipo_curso == 3).OrderBy(x => x.id_tipo_curso);
                ddlTipoCursoPrincipal.DataValueField = "id_tipo_curso";
                ddlTipoCursoPrincipal.DataTextField = "tipo_curso";
                ddlTipoCursoPrincipal.DataBind();
                ddlTipoCursoPrincipal.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos Tipos de Curso", ""));
                ddlTipoCursoPrincipal.SelectedValue = "";
                ddlTipoCursoPrincipal_SelectedIndexChanged(null, null);

                ddlTipoCursoDocumentos.Items.Clear();
                ddlTipoCursoDocumentos.DataSource = listaTipoCurso.Where(x => x.id_tipo_curso == 1 || x.id_tipo_curso == 3).OrderBy(x => x.id_tipo_curso);
                ddlTipoCursoDocumentos.DataValueField = "id_tipo_curso";
                ddlTipoCursoDocumentos.DataTextField = "tipo_curso";
                ddlTipoCursoDocumentos.DataBind();
                ddlTipoCursoDocumentos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos Tipos de Curso", ""));
                ddlTipoCursoDocumentos.SelectedValue = "";
                ddlTipoCursoDocumentos_SelectedIndexChanged(null, null);

                ddlTipoCursoAprovacaoOrientador.Items.Clear();
                ddlTipoCursoAprovacaoOrientador.DataSource = listaTipoCurso.Where(x => x.id_tipo_curso == 1 || x.id_tipo_curso == 3).OrderBy(x => x.id_tipo_curso);
                ddlTipoCursoAprovacaoOrientador.DataValueField = "id_tipo_curso";
                ddlTipoCursoAprovacaoOrientador.DataTextField = "tipo_curso";
                ddlTipoCursoAprovacaoOrientador.DataBind();
                ddlTipoCursoAprovacaoOrientador.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos Tipos de Curso", ""));
                ddlTipoCursoAprovacaoOrientador.SelectedValue = "";
                ddlTipoCursoAprovacaoOrientador_SelectedIndexChanged(null, null);

                ddlTipoCursoEntregaArtigo.Items.Clear();
                ddlTipoCursoEntregaArtigo.DataSource = listaTipoCurso.Where(x => x.id_tipo_curso == 1 || x.id_tipo_curso == 3).OrderBy(x => x.id_tipo_curso);
                ddlTipoCursoEntregaArtigo.DataValueField = "id_tipo_curso";
                ddlTipoCursoEntregaArtigo.DataTextField = "tipo_curso";
                ddlTipoCursoEntregaArtigo.DataBind();
                ddlTipoCursoEntregaArtigo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos Tipos de Curso", ""));
                ddlTipoCursoEntregaArtigo.SelectedValue = "";
                ddlTipoCursoEntregaArtigo_SelectedIndexChanged(null, null);

                divRowAluno.Visible = false;
                divRowSecretaria.Visible = false;

                if (usuario.grupos_acesso.id_grupo == 1 || usuario.grupos_acesso.id_grupo == 3 || usuario.grupos_acesso.id_grupo == 6 || usuario.grupos_acesso.id_grupo == 11) 
                    //1 = Administrador/ 3 = Secretaria/ 6 = Financeiro/ 11 = Limpeza
                {
                    divRowQuadro.Visible = true;
                    txtDataQuadro.Value = String.Format("{0:yyyy-MM-dd}", DateTime.Today);
                    txtDataQuadroFim.Value = String.Format("{0:yyyy-MM-dd}", DateTime.Today);
                    CarregaQuadro();
                    if (Session["arrayFiltroPrincipalMatricula"] != null)
                    {
                        CarregaSituacaoMatricula();
                    }
                    if (Session["arrayFiltroPrincipalDocumento"] != null)
                    {
                        CarregaSituacaoDocumento();
                    }
                    if (Session["arrayFiltroPrincipalAprovacaoOrientador"] != null)
                    {
                        CarregaSituacaoAprovacaoOrientador();
                    }
                    if (Session["arrayFiltroPrincipalEntregaArtigo"] != null)
                    {
                        CarregaSituacaoEntregaArtigo();
                    }
                }

                if (usuario.grupos_acesso.id_grupo == 2) //2 = Aluno
                {
                    divRowAluno.Visible = true;
                    divRowSecretaria.Visible = false;
                    lblTituloMensagem.Text = "Boletos aqui no SAPIENS";
                    lblMensagemClone.Text = "A partir da Parcela com vencimento em 15/11/2022, os Boletos não serão mais encaminhados via e-mail, estarão disponíveis no novo menu \"Boletos\" aqui no SAPIENS.<br><br>";
                    lblMensagemClone.Text = lblMensagemClone.Text + "Obs.: Ficarão disponíveis todos os boletos a vencer que foram gerados pela FIPT.<br><br>";
                    lblMensagemClone.Text = lblMensagemClone.Text + "Os boletos vencidos também poderão ser emitidos aqui no SAPIENS com os valores atualizados até a data de sua emissão.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "$('#divCabecalho').addClass('alert-success'); $('#divMensagemModal').modal();", true);
                }
                else if (usuario.grupos_acesso.id_grupo != 11) //2 = Aluno
                {
                    divRowAluno.Visible = false;
                    divRowSecretaria.Visible = true;

                    GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                    List<novidadesSistema> listaNovidades = new List<novidadesSistema>();
                    listaNovidades = aplicacaoGerais.ListaNovidadesSistema();
                    grdNovidadesSapiens.DataSource = listaNovidades;
                    grdNovidadesSapiens.DataBind();

                    if (listaNovidades.Count > 0)
                    {
                        if (listaNovidades.Any(x=> x.dataInicio <=DateTime.Today && x.dataFim >= DateTime.Today))
                        {
                            lblQtdNovidades.InnerHtml= listaNovidades.Where(x => x.dataInicio <= DateTime.Today && x.dataFim >= DateTime.Today).Count().ToString();
                            iIconeNovidades.Attributes.Add("class", "fa fa-newspaper-o piscante");
                        }
                        else
                        {
                            lblQtdNovidades.InnerHtml = "0";
                        }
                        grdNovidadesSapiens.UseAccessibleHeader = true;
                        grdNovidadesSapiens.HeaderRow.TableSection = TableRowSection.TableHeader;
                        msgSemResultadosgrdNovidadesSapiens.Visible = false;
                        grdNovidadesSapiens.Visible = true;
                    }
                    else
                    {
                        lblQtdNovidades.InnerHtml = "0";
                        msgSemResultadosgrdNovidadesSapiens.Visible = true;
                    }
                }

            }
            else
            {
                if (grdMatricula.Rows.Count != 0)
                {
                    grdMatricula.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (grdDocumento.Rows.Count != 0)
                {
                    grdDocumento.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (grdAprovacaoOrientador.Rows.Count != 0)
                {
                    grdAprovacaoOrientador.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (grdEntregaArtigo.Rows.Count != 0)
                {
                    grdEntregaArtigo.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

            }
        }

        public void CarregaSituacaoMatricula()
        {
            string[] arrayFiltroPrincipalMatricula = new string[2];

            arrayFiltroPrincipalMatricula = (string[])Session["arrayFiltroPrincipalMatricula"];

            cursos item = new cursos();
            item.id_tipo_curso = 0;
            item.id_curso = 0;

            if (arrayFiltroPrincipalMatricula[0] != "" && arrayFiltroPrincipalMatricula[0] != null)
            {
                item.id_tipo_curso = Convert.ToInt32(arrayFiltroPrincipalMatricula[0]);
                ddlTipoCursoPrincipal.SelectedValue = arrayFiltroPrincipalMatricula[0];
                
            }

            if (arrayFiltroPrincipalMatricula[1] != "" && arrayFiltroPrincipalMatricula[1] != null)
            {
                ddlTipoCursoPrincipal_SelectedIndexChanged(null, null);
                item.id_curso = Convert.ToInt32(arrayFiltroPrincipalMatricula[1]);
                ddlNomeCursoPrincipal.SelectedValue = arrayFiltroPrincipalMatricula[1];
            }

            AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
            List<matricula_turma> listaMatricula = new List<matricula_turma>();
            listaMatricula = aplicacaoAluno.ListaItemAlunosSituacaoIndefinida(item);

            grdMatricula.DataSource = listaMatricula;
            grdMatricula.DataBind();

            if (listaMatricula.Count > 0)
            {
                grdMatricula.UseAccessibleHeader = true;
                grdMatricula.HeaderRow.TableSection = TableRowSection.TableHeader;
                divMsgMatricula.Visible = false;
                grdMatricula.Visible = true;
            }
            else
            {
                divMsgMatricula.Visible = true;
            }

            divGradeMatricula.Style["display"] = "block";

        }

        public void CarregaSituacaoDocumento()
        {
            string[] arrayFiltroPrincipalDocumento = new string[2];

            arrayFiltroPrincipalDocumento = (string[])Session["arrayFiltroPrincipalDocumento"];

            cursos item = new cursos();
            item.id_tipo_curso = 0;
            item.id_curso = 0;

            if (arrayFiltroPrincipalDocumento[0] != "" && arrayFiltroPrincipalDocumento[0] != null)
            {
                item.id_tipo_curso = Convert.ToInt32(arrayFiltroPrincipalDocumento[0]);
                ddlTipoCursoDocumentos.SelectedValue = arrayFiltroPrincipalDocumento[0];

            }

            if (arrayFiltroPrincipalDocumento[1] != "" && arrayFiltroPrincipalDocumento[1] != null)
            {
                ddlTipoCursoDocumentos_SelectedIndexChanged(null, null);
                item.id_curso = Convert.ToInt32(arrayFiltroPrincipalDocumento[1]);
                ddlCursoDocumentos.SelectedValue = arrayFiltroPrincipalDocumento[1];
            }

            AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
            List<matricula_turma> listaMatricula = new List<matricula_turma>();
            listaMatricula = aplicacaoAluno.ListaItemAlunosDocumentacaoPendente(item);

            grdDocumento.DataSource = listaMatricula;
            grdDocumento.DataBind();

            if (listaMatricula.Count > 0)
            {
                grdDocumento.UseAccessibleHeader = true;
                grdDocumento.HeaderRow.TableSection = TableRowSection.TableHeader;
                divMsgDocumento.Visible = false;
                grdDocumento.Visible = true;
            }
            else
            {
                divMsgDocumento.Visible = true;
            }

            divGradeDocumento.Style["display"] = "block";

        }

        public void CarregaSituacaoAprovacaoOrientador()
        {
            string[] arrayFiltroPrincipalAprovacaoOrientador = new string[2];

            arrayFiltroPrincipalAprovacaoOrientador = (string[])Session["arrayFiltroPrincipalAprovacaoOrientador"];

            cursos item = new cursos();
            item.id_tipo_curso = 0;
            item.id_curso = 0;

            if (arrayFiltroPrincipalAprovacaoOrientador[0] != "" && arrayFiltroPrincipalAprovacaoOrientador[0] != null)
            {
                item.id_tipo_curso = Convert.ToInt32(arrayFiltroPrincipalAprovacaoOrientador[0]);
                ddlTipoCursoAprovacaoOrientador.SelectedValue = arrayFiltroPrincipalAprovacaoOrientador[0];

            }

            if (arrayFiltroPrincipalAprovacaoOrientador[1] != "" && arrayFiltroPrincipalAprovacaoOrientador[1] != null)
            {
                ddlTipoCursoAprovacaoOrientador_SelectedIndexChanged(null, null);
                item.id_curso = Convert.ToInt32(arrayFiltroPrincipalAprovacaoOrientador[1]);
                ddlCursoAprovacaoOrientador.SelectedValue = arrayFiltroPrincipalAprovacaoOrientador[1];
            }

            AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
            List<matricula_turma> listaMatricula = new List<matricula_turma>();
            listaMatricula = aplicacaoAluno.ListaItemAlunosAprovacaoOrientador(item);

            grdAprovacaoOrientador.DataSource = listaMatricula;
            grdAprovacaoOrientador.DataBind();

            if (listaMatricula.Count > 0)
            {
                grdAprovacaoOrientador.UseAccessibleHeader = true;
                grdAprovacaoOrientador.HeaderRow.TableSection = TableRowSection.TableHeader;
                divMsgAprovacaoOrientador.Visible = false;
                grdAprovacaoOrientador.Visible = true;
            }
            else
            {
                divMsgAprovacaoOrientador.Visible = true;
            }

            divGradeAprovacaoOrientador.Style["display"] = "block";

        }

        public void CarregaSituacaoEntregaArtigo()
        {
            string[] arrayFiltroPrincipalEntregaArtigo = new string[2];

            arrayFiltroPrincipalEntregaArtigo = (string[])Session["arrayFiltroPrincipalEntregaArtigo"];

            cursos item = new cursos();
            item.id_tipo_curso = 0;
            item.id_curso = 0;

            if (arrayFiltroPrincipalEntregaArtigo[0] != "" && arrayFiltroPrincipalEntregaArtigo[0] != null)
            {
                item.id_tipo_curso = Convert.ToInt32(arrayFiltroPrincipalEntregaArtigo[0]);
                ddlTipoCursoEntregaArtigo.SelectedValue = arrayFiltroPrincipalEntregaArtigo[0];

            }

            if (arrayFiltroPrincipalEntregaArtigo[1] != "" && arrayFiltroPrincipalEntregaArtigo[1] != null)
            {
                ddlTipoCursoEntregaArtigo_SelectedIndexChanged(null, null);
                item.id_curso = Convert.ToInt32(arrayFiltroPrincipalEntregaArtigo[1]);
                ddlCursoEntregaArtigo.SelectedValue = arrayFiltroPrincipalEntregaArtigo[1];
            }

            AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
            List<matricula_turma> listaMatricula = new List<matricula_turma>();
            listaMatricula = aplicacaoAluno.ListaItemAlunosEntregaArtigo(item);

            grdEntregaArtigo.DataSource = listaMatricula;
            grdEntregaArtigo.DataBind();

            if (listaMatricula.Count > 0)
            {
                grdEntregaArtigo.UseAccessibleHeader = true;
                grdEntregaArtigo.HeaderRow.TableSection = TableRowSection.TableHeader;
                divMsgEntregaArtigo.Visible = false;
                grdEntregaArtigo.Visible = true;
            }
            else
            {
                divMsgEntregaArtigo.Visible = true;
            }

            divGradeEntregaArtigo.Style["display"] = "block";

        }

        public void grdMatricula_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                int linha = Convert.ToInt32(e.CommandArgument);
                int codigo = Convert.ToInt32(grdMatricula.DataKeys[linha].Values[0]);
                alunos item = new alunos();
                item.idaluno = codigo;
                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                item = aplicacaoAluno.BuscaItem(item);
                //Session.Add("Aluno", item);
                //Session.Add("sNovoAluno", false);
                //Response.Redirect("cadAlunoGestao.aspx", true);

                string qTab = HttpContext.Current.Request["hQTab"];
                Session.Add(qTab + "Aluno", item);
                Session.Add(qTab + "sNovoAluno", false);

                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
                sb.AppendFormat("<form name='form' action='{0}' method='post'>", "cadAlunoGestao.aspx");
                sb.AppendFormat("<input type='hidden' name='hQTab' value='{0}'>", qTab);
                // Other params go here
                sb.Append("</form>");
                sb.Append("</body>");
                sb.Append("</html>");

                Response.Write(sb.ToString());

                Response.End();
            }
        }

        public void grdDocumento_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                int linha = Convert.ToInt32(e.CommandArgument);
                int codigo = Convert.ToInt32(grdDocumento.DataKeys[linha].Values[0]);
                alunos item = new alunos();
                item.idaluno = codigo;
                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                item = aplicacaoAluno.BuscaItem(item);
                //Session.Add("Aluno", item);
                //Session.Add("sNovoAluno", false);
                //Response.Redirect("cadAlunoGestao.aspx", true);

                string qTab = HttpContext.Current.Request["hQTab"];
                Session.Add(qTab + "Aluno", item);
                Session.Add(qTab + "sNovoAluno", false);

                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
                sb.AppendFormat("<form name='form' action='{0}' method='post'>", "cadAlunoGestao.aspx");
                sb.AppendFormat("<input type='hidden' name='hQTab' value='{0}'>", qTab);
                // Other params go here
                sb.Append("</form>");
                sb.Append("</body>");
                sb.Append("</html>");

                Response.Write(sb.ToString());

                Response.End();
            }
        }

        public void grdAprovacaoOrientador_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                int linha = Convert.ToInt32(e.CommandArgument);
                int codigo = Convert.ToInt32(grdAprovacaoOrientador.DataKeys[linha].Values[0]);
                alunos item = new alunos();
                item.idaluno = codigo;
                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                item = aplicacaoAluno.BuscaItem(item);
                //Session.Add("Aluno", item);
                //Session.Add("sNovoAluno", false);
                //Response.Redirect("cadAlunoGestao.aspx", true);

                string qTab = HttpContext.Current.Request["hQTab"];
                Session.Add(qTab + "Aluno", item);
                Session.Add(qTab + "sNovoAluno", false);

                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
                sb.AppendFormat("<form name='form' action='{0}' method='post'>", "cadAlunoGestao.aspx");
                sb.AppendFormat("<input type='hidden' name='hQTab' value='{0}'>", qTab);
                // Other params go here
                sb.Append("</form>");
                sb.Append("</body>");
                sb.Append("</html>");

                Response.Write(sb.ToString());

                Response.End();

            }
        }

        public void grdEntregaArtigo_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                int linha = Convert.ToInt32(e.CommandArgument);
                int codigo = Convert.ToInt32(grdEntregaArtigo.DataKeys[linha].Values[0]);
                alunos item = new alunos();
                item.idaluno = codigo;
                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                item = aplicacaoAluno.BuscaItem(item);
                //Session.Add("Aluno", item);
                //Session.Add("sNovoAluno", false);
                //Response.Redirect("cadAlunoGestao.aspx", true);

                string qTab = HttpContext.Current.Request["hQTab"];
                Session.Add(qTab + "Aluno", item);
                Session.Add(qTab + "sNovoAluno", false);

                StringBuilder sb = new StringBuilder();
                sb.Append("<html>");
                sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
                sb.AppendFormat("<form name='form' action='{0}' method='post'>", "cadAlunoGestao.aspx");
                sb.AppendFormat("<input type='hidden' name='hQTab' value='{0}'>", qTab);
                // Other params go here
                sb.Append("</form>");
                sb.Append("</body>");
                sb.Append("</html>");

                Response.Write(sb.ToString());

                Response.End();
            }
        }

        public void CarregaQuadro()
        {
            MonitorAplicacao aplicacaoMonitor = new MonitorAplicacao();
            List<monitor> listaMonitor = new List<monitor>();
            monitor item = new monitor();

            string sAux = "";

            if (txtDataQuadro.Value.Trim() == "")
            {
                sAux = "Deve-se selecionar uma data (início) válida. <br><br>";
            }
            if (txtDataQuadroFim.Value.Trim() == "")
            {
                sAux = sAux + "Deve-se selecionar uma data (fim) válida.";
            }

            if (sAux != "")
            {
                lblMensagemClone.Text = sAux;
                lblTituloMensagem.Text = "Atenção";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                return;
            }

            item.DataEventoInicio = Convert.ToDateTime(txtDataQuadro.Value.Trim());
            item.DataEventoFim = Convert.ToDateTime(txtDataQuadroFim.Value.Trim());
            item.ativo = 1;

            listaMonitor = aplicacaoMonitor.ListaItemQuadro(item);
            grdQuadro.DataSource = listaMonitor;
            grdQuadro.DataBind();

            if (listaMonitor.Count > 0)
            {
                grdQuadro.UseAccessibleHeader = true;
                grdQuadro.HeaderRow.TableSection = TableRowSection.TableHeader;
                divMsgQuadro.Visible = false;
                grdQuadro.Visible = true;
            }
            else
            {
                divMsgQuadro.Visible = true;
            }
        }

        public void ddlTipoCursoPrincipal_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (ddlTipoCursoPrincipal.SelectedValue != "")
            {
                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos item = new cursos();
                item.id_tipo_curso = Convert.ToInt32(ddlTipoCursoPrincipal.SelectedValue);
                List<cursos> listaCurso = aplicacaoCurso.ListaItem(item);
                var lista = from item2 in listaCurso
                            select new
                            {
                                id_curso = item2.id_curso,
                                nome = item2.sigla + " - " + item2.nome
                            };

                ddlNomeCursoPrincipal.Items.Clear();
                ddlNomeCursoPrincipal.DataSource = lista.OrderBy(x => x.nome);
                ddlNomeCursoPrincipal.DataValueField = "id_curso";
                ddlNomeCursoPrincipal.DataTextField = "nome";
                ddlNomeCursoPrincipal.DataBind();
                ddlNomeCursoPrincipal.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os Cursos", ""));
                ddlNomeCursoPrincipal.SelectedValue = "";

            }
            else
            {
                ddlNomeCursoPrincipal.Items.Clear();
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fSelect2();", true);
        }

        public void ddlTipoCursoDocumentos_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (ddlTipoCursoDocumentos.SelectedValue != "")
            {
                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos item = new cursos();
                item.id_tipo_curso = Convert.ToInt32(ddlTipoCursoDocumentos.SelectedValue);
                List<cursos> listaCurso = aplicacaoCurso.ListaItem(item);
                var lista = from item2 in listaCurso
                            select new
                            {
                                id_curso = item2.id_curso,
                                nome = item2.sigla + " - " + item2.nome
                            };

                ddlCursoDocumentos.Items.Clear();
                ddlCursoDocumentos.DataSource = lista.OrderBy(x => x.nome);
                ddlCursoDocumentos.DataValueField = "id_curso";
                ddlCursoDocumentos.DataTextField = "nome";
                ddlCursoDocumentos.DataBind();
                ddlCursoDocumentos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os Cursos", ""));
                ddlCursoDocumentos.SelectedValue = "";

            }
            else
            {
                ddlCursoDocumentos.Items.Clear();
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", "javascript:fSelect2();", true);
        }

        public void ddlTipoCursoAprovacaoOrientador_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (ddlTipoCursoAprovacaoOrientador.SelectedValue != "")
            {
                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos item = new cursos();
                item.id_tipo_curso = Convert.ToInt32(ddlTipoCursoAprovacaoOrientador.SelectedValue);
                List<cursos> listaCurso = aplicacaoCurso.ListaItem(item);
                var lista = from item2 in listaCurso
                            select new
                            {
                                id_curso = item2.id_curso,
                                nome = item2.sigla + " - " + item2.nome
                            };

                ddlCursoAprovacaoOrientador.Items.Clear();
                ddlCursoAprovacaoOrientador.DataSource = lista.OrderBy(x => x.nome);
                ddlCursoAprovacaoOrientador.DataValueField = "id_curso";
                ddlCursoAprovacaoOrientador.DataTextField = "nome";
                ddlCursoAprovacaoOrientador.DataBind();
                ddlCursoAprovacaoOrientador.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os Cursos", ""));
                ddlCursoAprovacaoOrientador.SelectedValue = "";

            }
            else
            {
                ddlCursoAprovacaoOrientador.Items.Clear();
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.UpdatePanel3.GetType(), "Script", "javascript:fSelect2();", true);
        }

        public void ddlTipoCursoEntregaArtigo_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (ddlTipoCursoEntregaArtigo.SelectedValue != "")
            {
                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos item = new cursos();
                item.id_tipo_curso = Convert.ToInt32(ddlTipoCursoEntregaArtigo.SelectedValue);
                List<cursos> listaCurso = aplicacaoCurso.ListaItem(item);
                var lista = from item2 in listaCurso
                            select new
                            {
                                id_curso = item2.id_curso,
                                nome = item2.sigla + " - " + item2.nome
                            };

                ddlCursoEntregaArtigo.Items.Clear();
                ddlCursoEntregaArtigo.DataSource = lista.OrderBy(x => x.nome);
                ddlCursoEntregaArtigo.DataValueField = "id_curso";
                ddlCursoEntregaArtigo.DataTextField = "nome";
                ddlCursoEntregaArtigo.DataBind();
                ddlCursoEntregaArtigo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os Cursos", ""));
                ddlCursoEntregaArtigo.SelectedValue = "";

            }
            else
            {
                ddlCursoEntregaArtigo.Items.Clear();
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.UpdatePanel4.GetType(), "Script", "javascript:fSelect2();", true);
        }

        protected void btnPesquisaMatricula_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroPrincipalMatricula = new string[2];

            if (ddlTipoCursoPrincipal.SelectedValue != "")
            {
                arrayFiltroPrincipalMatricula[0] = ddlTipoCursoPrincipal.SelectedValue;
            }

            if (ddlNomeCursoPrincipal.SelectedValue != "")
            {
                arrayFiltroPrincipalMatricula[1] = ddlNomeCursoPrincipal.SelectedValue;
            }

            Session["arrayFiltroPrincipalMatricula"] = arrayFiltroPrincipalMatricula;

            CarregaSituacaoMatricula();

            //li_Matricula

            li_Matricula.Attributes.Add("class","active");
            tab_Matricula.Attributes.Add("class", "tab-pane active");

            li_AprovacaoOrientador.Attributes.Add("class", "");
            tab_AprovacaoOrientador.Attributes.Add("class", "tab-pane");

            li_Documentos.Attributes.Add("class", "");
            tab_Documentos.Attributes.Add("class", "tab-pane");

            li_Quadro.Attributes.Add("class", "");
            tab_Quadro.Attributes.Add("class", "tab-pane");

            li_EntregaArtigo.Attributes.Add("class", "");
            tab_EntregaArtigo.Attributes.Add("class", "tab-pane");

        }

        protected void btnPesquisaDocumento_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroPrincipalDocumento = new string[2];

            if (ddlTipoCursoDocumentos.SelectedValue != "")
            {
                arrayFiltroPrincipalDocumento[0] = ddlTipoCursoDocumentos.SelectedValue;
            }

            if (ddlCursoDocumentos.SelectedValue != "")
            {
                arrayFiltroPrincipalDocumento[1] = ddlCursoDocumentos.SelectedValue;
            }

            Session["arrayFiltroPrincipalDocumento"] = arrayFiltroPrincipalDocumento;

            CarregaSituacaoDocumento();

            //li_Documentos
            li_Documentos.Attributes.Add("class", "active");
            tab_Documentos.Attributes.Add("class", "tab-pane active");

            li_AprovacaoOrientador.Attributes.Add("class", "");
            tab_AprovacaoOrientador.Attributes.Add("class", "tab-pane");

            li_Matricula.Attributes.Add("class", "");
            tab_Matricula.Attributes.Add("class", "tab-pane");

            li_Quadro.Attributes.Add("class", "");
            tab_Quadro.Attributes.Add("class", "tab-pane");

            li_EntregaArtigo.Attributes.Add("class", "");
            tab_EntregaArtigo.Attributes.Add("class", "tab-pane");

        }

        protected void btnPesquisaAprovacaoOrientador_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroPrincipalAprovacaoOrientador = new string[2];

            if (ddlTipoCursoAprovacaoOrientador.SelectedValue != "")
            {
                arrayFiltroPrincipalAprovacaoOrientador[0] = ddlTipoCursoAprovacaoOrientador.SelectedValue;
            }

            if (ddlCursoAprovacaoOrientador.SelectedValue != "")
            {
                arrayFiltroPrincipalAprovacaoOrientador[1] = ddlCursoAprovacaoOrientador.SelectedValue;
            }

            Session["arrayFiltroPrincipalAprovacaoOrientador"] = arrayFiltroPrincipalAprovacaoOrientador;

            CarregaSituacaoAprovacaoOrientador();

            //li_AprovacaoOrientador
            li_AprovacaoOrientador.Attributes.Add("class", "active");
            tab_AprovacaoOrientador.Attributes.Add("class", "tab-pane active");

            li_Documentos.Attributes.Add("class", "");
            tab_Documentos.Attributes.Add("class", "tab-pane");

            li_Matricula.Attributes.Add("class", "");
            tab_Matricula.Attributes.Add("class", "tab-pane");

            li_Quadro.Attributes.Add("class", "");
            tab_Quadro.Attributes.Add("class", "tab-pane");

            li_EntregaArtigo.Attributes.Add("class", "");
            tab_EntregaArtigo.Attributes.Add("class", "tab-pane");

        }

        protected void btnPesquisaEntregaArtigo_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroPrincipalEntregaArtigo = new string[2];

            if (ddlTipoCursoEntregaArtigo.SelectedValue != "")
            {
                arrayFiltroPrincipalEntregaArtigo[0] = ddlTipoCursoEntregaArtigo.SelectedValue;
            }

            if (ddlCursoEntregaArtigo.SelectedValue != "")
            {
                arrayFiltroPrincipalEntregaArtigo[1] = ddlCursoEntregaArtigo.SelectedValue;
            }

            Session["arrayFiltroPrincipalEntregaArtigo"] = arrayFiltroPrincipalEntregaArtigo;

            CarregaSituacaoEntregaArtigo();

            //li_EntregaArtigo
            li_EntregaArtigo.Attributes.Add("class", "active");
            tab_EntregaArtigo.Attributes.Add("class", "tab-pane active");

            li_AprovacaoOrientador.Attributes.Add("class", "");
            tab_AprovacaoOrientador.Attributes.Add("class", "tab-pane");

            li_Documentos.Attributes.Add("class", "");
            tab_Documentos.Attributes.Add("class", "tab-pane");

            li_Matricula.Attributes.Add("class", "");
            tab_Matricula.Attributes.Add("class", "tab-pane");

            li_Quadro.Attributes.Add("class", "");
            tab_Quadro.Attributes.Add("class", "tab-pane");

        }

        protected void btnImprimirPresencaAluno_Click(object sender, EventArgs e)
        {
            CarregaQuadro();
        }

        //public void grdNovidadesSapiens_Command(object sender, CommandEventArgs e)
        //{
        //    int index = Convert.ToInt32(e.CommandArgument);
        //    if (e.CommandName == "StartService")
        //    {
        //        int linha = Convert.ToInt32(e.CommandArgument);
        //        //int codigo = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0]);
        //        fornecedores item = new fornecedores();
        //        FornecedorAplicacao aplicacaoFornecedor = new FornecedorAplicacao();
        //        //item.Fornecedor = grdResultado.DataKeys[linha].Values[0].ToString();
        //        item = aplicacaoFornecedor.BuscaItem(Convert.ToInt32(grdNovidadesSapiens.DataKeys[linha].Values[0]));
        //        Session["fornecedores"] = item;
        //        Session["sNewFornecedores"] = false;
        //        Response.Redirect("finPessoaJuridicaGestao.aspx", true);
        //    }
        //}

        public string setVisualizarNovidades(string qTitulo, string qDetalhe, DateTime qDataInicio, DateTime qDataFim, DateTime dDataOcorrencia)
        {
            string sAux;
            if (qDataInicio <= DateTime.Today && qDataFim >= DateTime.Today)
            {
                sAux = "<div title=\"Visualizar Detalhe\"> <a class=\"btn btn-primary btn-circle fa fa-search piscante\" href=\'javascript:fExibeVisualizarNovidades(\""
                        + qTitulo + "\",\"" + qDetalhe + "\",\"" + String.Format("{0:dd/MM/yyyy}", dDataOcorrencia) + "\")\'; ></a></div>";
            }
            else
            {
                sAux = "<div title=\"Visualizar Detalhe\"> <a class=\"btn btn-primary btn-circle fa fa-search\" href=\'javascript:fExibeVisualizarNovidades(\""
                        + qTitulo + "\",\"" + qDetalhe + "\",\"" + String.Format("{0:dd/MM/yyyy}", dDataOcorrencia) + "\")\'; ></a></div>";
            }
            
            return sAux;
        }
    }
}