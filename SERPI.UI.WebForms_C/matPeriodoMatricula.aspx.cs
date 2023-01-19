using Aplicacao_C;
using SERPI.Dominio_C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class matPeriodoMatricula : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 32)) // 1. Período de Matrícula - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                //CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                //cursos itemCurso = new cursos();
                //List<cursos> listaCurso = aplicacaoCurso.ListaItem(itemCurso);
                ////var listaPais2 = from item2 in listaPais
                ////                 select new
                ////                 {
                ////                     Id_Pais = item2.Id_Pais,
                ////                     Nacionalidade = item2.Nacionalidade
                ////                 };

                //ddlCodigoCursoProfessor.Items.Clear();
                //ddlCodigoCursoProfessor.DataSource = listaCurso.OrderBy(x => x.sigla);
                //ddlCodigoCursoProfessor.DataValueField = "id_curso";
                //ddlCodigoCursoProfessor.DataTextField = "sigla";
                //ddlCodigoCursoProfessor.DataBind();
                //ddlCodigoCursoProfessor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione uma Sigla do Curso", ""));
                //ddlCodigoCursoProfessor.SelectedValue = "";

                GeraisAplicacao aplicacaoGarais = new GeraisAplicacao();
                List<tipos_curso> listaTipoCurso = aplicacaoGarais.ListaTipoCurso();

                ddlTipoCursoPeriodoMatricula.Items.Clear();
                ddlTipoCursoPeriodoMatricula.DataSource = listaTipoCurso.OrderBy(x => x.id_tipo_curso);
                ddlTipoCursoPeriodoMatricula.DataValueField = "id_tipo_curso";
                ddlTipoCursoPeriodoMatricula.DataTextField = "tipo_curso";
                ddlTipoCursoPeriodoMatricula.DataBind();
                ddlTipoCursoPeriodoMatricula.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos Tipos de Curso", ""));
                ddlTipoCursoPeriodoMatricula.SelectedValue = "";

                if (Session["aFiltroMatricula"] != null)
                {
                    CarregarDados();
                }
                else
                {
                    ddlTipoCursoPeriodoMatricula_SelectedIndexChanged(null, null);
                }

                if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 32))
                {
                    if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 32).FirstOrDefault().escrita != true)
                    {
                        btnCriarPeriodo.Visible = false;
                    }
                }

            }
            else
            {
                if (grdPeriodoMatricula.Rows.Count != 0)
                {
                    grdPeriodoMatricula.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        private void CarregarDados()
        {
            string[] aFiltroMatricula = new string[4];

            periodo_matricula item = new periodo_matricula();

            aFiltroMatricula = (string[])Session["aFiltroMatricula"];

            if (aFiltroMatricula[0] != "" && aFiltroMatricula[0] != null)
            {
                item.id_periodo = Convert.ToInt32(aFiltroMatricula[0]);
                ddlTipoCursoPeriodoMatricula.SelectedValue = aFiltroMatricula[0];
                ddlTipoCursoPeriodoMatricula_SelectedIndexChanged(null, null);
            }

            if (aFiltroMatricula[1] != "" && aFiltroMatricula[1] != null)
            {
                item.quadrimestre = aFiltroMatricula[1];
                ddlPeriodoPeriodoMatricula.SelectedValue = aFiltroMatricula[1];
            }

            if (aFiltroMatricula[2] != "" && aFiltroMatricula[2] != null)
            {
                item.data_inicio = Convert.ToDateTime(aFiltroMatricula[2]);
                txtDataInicioPeriodoMatricula.Value = aFiltroMatricula[2];
            }

            if (aFiltroMatricula[3] != "" && aFiltroMatricula[3] != null)
            {
                item.data_termino = Convert.ToDateTime(aFiltroMatricula[3]);
                txtDataFimPeriodoMatricula.Value = aFiltroMatricula[3];
            }
           
            //Session["aFiltroProfessor"] = aFiltroProfessor;
            MatriculaAplicacao aplicacaoMatricula = new MatriculaAplicacao();
            List<periodo_matricula> lista = new List<periodo_matricula>();
            lista = aplicacaoMatricula.ListaItem(item);
            grdPeriodoMatricula.DataSource = lista;
            grdPeriodoMatricula.DataBind();

            if (lista.Count > 0)
            {
                grdPeriodoMatricula.UseAccessibleHeader = true;
                grdPeriodoMatricula.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultados.Visible = false;
                grdPeriodoMatricula.Visible = true;
            }
            else
            {
                msgSemResultados.Visible = true;
            }
            divResultados.Visible = true;
        }

        public void ddlTipoCursoPeriodoMatricula_SelectedIndexChanged(Object sender, EventArgs e)
        {
            //if (ddlTipoCursoAluno.SelectedValue != "")
            //{
            QuadrimestreAplicacao aplicacaoPeriodo = new QuadrimestreAplicacao();
            tipos_curso item = new tipos_curso();

            if (ddlTipoCursoPeriodoMatricula.SelectedValue != "")
            {
                item.id_tipo_curso = Convert.ToInt32(ddlTipoCursoPeriodoMatricula.SelectedValue);
            }

            List<quadrimestres> lista = new List<quadrimestres>();
            lista = aplicacaoPeriodo.ListaItem(item);

            ddlPeriodoPeriodoMatricula.Items.Clear();
            ddlPeriodoPeriodoMatricula.DataSource = lista.OrderByDescending(x=> x.ano).ThenByDescending(x=> x.quadrimestre).ToList();
            ddlPeriodoPeriodoMatricula.DataValueField = "quadrimestre";
            ddlPeriodoPeriodoMatricula.DataTextField = "quadrimestre";
            ddlPeriodoPeriodoMatricula.DataBind();
            ddlPeriodoPeriodoMatricula.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos Períodos", ""));
            ddlPeriodoPeriodoMatricula.SelectedValue = "";

            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fSelect2();", true);
        }

        protected void bntPerquisaMatricula_Click(object sender, EventArgs e)
        {
            string[] aFiltroMatricula = new string[4];

            if (ddlTipoCursoPeriodoMatricula.SelectedValue != "")
            {
                aFiltroMatricula[0] = ddlTipoCursoPeriodoMatricula.SelectedValue;
            }

            if (ddlPeriodoPeriodoMatricula.SelectedValue != "")
            {
                aFiltroMatricula[1] = ddlPeriodoPeriodoMatricula.SelectedValue;
            }

            if (txtDataInicioPeriodoMatricula.Value.Trim() != "")
            {
                aFiltroMatricula[2] = txtDataInicioPeriodoMatricula.Value.Trim();
            }

            if (txtDataFimPeriodoMatricula.Value.Trim() != "")
            {
                aFiltroMatricula[3] = txtDataFimPeriodoMatricula.Value.Trim();
            }

            Session["aFiltroMatricula"] = aFiltroMatricula;

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
        //    Professors_concentracao item = new Professors_concentracao();
        //    item.id_Professor_concentracao = codigo;
        //    switch (grdResultado.DataKeys[linha].Values[1].ToString())
        //    {
        //        case "Editar":
        //            ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
        //            item = aplicacaoProfessor.BuscaItem(item);
        //            Session.Add("Professors_concentracao", item);
        //            Session.Add("sNewProfessor", false);
        //            Response.Redirect("cadProfessorConcentracaoGestao.aspx", true);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        public void grdPeriodoMatricula_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                int linha = Convert.ToInt32(e.CommandArgument);
                int codigo = Convert.ToInt32(grdPeriodoMatricula.DataKeys[linha].Values[0]);
                periodo_matricula item = new periodo_matricula();
                MatriculaAplicacao aplicacaoPeriodo = new MatriculaAplicacao();
                item.id_periodo = codigo;
                item = aplicacaoPeriodo.BuscaPeriodoMatricula(item);
                Session["periodo_matricula"] = item;
                Session["sNewPeriodoMatricula"] = false;
                Response.Redirect("matPeriodoMatriculaGestao.aspx", true);
            }
        }

        protected void btnCriarPeridoMatricula_Click(object sender, EventArgs e)
        {
            Session["sNewPeriodoMatricula"] = true;
            Session["periodo_matricula"] = null;
            Response.Redirect("matPeriodoMatriculaGestao.aspx", true);
        }

        public string setLinkImagem(string cpf, string Nome)
        {
            UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
            usuarios item = new usuarios();
            item.usuario = cpf;
            item = aplicacaoUsuario.BuscaUsuario(item);
            string sAux;

            if (item == null)
            {
                sAux = "<div title=\"Sem foto\"> <a class=\"fa fa-ban\" href=\'#\'; style= color:#428bca;></a></div>";
            }
            else
            {
                if ((item.avatar == null || (item.avatar == "")))
                {
                    sAux = "<div title=\"Sem foto\"> <a class=\"fa fa-ban\" href=\'#\'; style= color:#428bca;></a></div>";
                }
                else
                {
                    sAux = ("<div title=\"Ver foto\"> <a href=\'javascript:fExibeImagem(\"" + (item.avatar + ("\",\"" + (Nome + "\")\' >"))));
                    sAux = (sAux + ("<img class=\"img-circle\" id=\"imageresource\" src=\"" + ("img\\pessoas\\" + (item.avatar + ("?" + (DateTime.Now.ToString() + "\" style=\"width: 35px; height: 35px;\">"))))));
                    sAux = (sAux + "</a></div>");
                }
            }

            return sAux;
        }
    }
}