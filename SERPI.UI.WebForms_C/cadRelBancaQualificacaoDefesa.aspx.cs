
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SERPI.Dominio_C;
using Aplicacao_C;

namespace SERPI.UI.WebForms_C
{
    public partial class cadRelBancaQualificacaoDefesa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null)
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 56)) //23. Outros Relatórios
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos item = new cursos();
                List<cursos> listaCurso = aplicacaoCurso.ListaItem(item).Where(x=> x.id_tipo_curso == 1 || x.id_tipo_curso == 3).ToList() ;
                var lista = from item2 in listaCurso
                            select new
                            {
                                id_curso = item2.id_curso,
                                nome = item2.sigla + " - " + item2.nome
                            };

                ddlCurso.Items.Clear();
                ddlCurso.DataSource = lista.OrderBy(x => x.nome);
                ddlCurso.DataValueField = "id_curso";
                ddlCurso.DataTextField = "nome";
                ddlCurso.DataBind();
                ddlCurso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", "0"));
                ddlCurso.SelectedValue = "0";
            }
            else
            {
                if (grdBancaQualificacaoDefesa.Rows.Count != 0)
                {
                    grdBancaQualificacaoDefesa.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        private void CarregarDados()
        {
            professores item = new professores();

            int qIdCurso;
            int qMes;
            int qAno = 0;
            string qCondicao;
            string qTipoBanca;
            //string qSituacao = "";
            //string qTipoMatricula = "";
            qIdCurso = Convert.ToInt32(ddlCurso.SelectedValue);
            qMes = Convert.ToInt32(ddlMes.SelectedValue);
            if (txtAno.Value != "")
            {
                qAno = Convert.ToInt32(txtAno.Value);
            }
            qCondicao = ddlCondicao.SelectedValue;
            qTipoBanca = ddlTipoQualificacaoDefesa.SelectedValue;

            ////Session["arrayFiltroAluno"] = arrayFiltroAluno;
            GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
            List<banca_professores> listaBanca = new List<banca_professores>();
            listaBanca = aplicacaoGerais.ListaBancas(qIdCurso, qMes, qAno, qCondicao, qTipoBanca);
            grdBancaQualificacaoDefesa.DataSource = listaBanca;
            grdBancaQualificacaoDefesa.DataBind();

            if (listaBanca.Count > 0)
            {
                grdBancaQualificacaoDefesa.UseAccessibleHeader = true;
                grdBancaQualificacaoDefesa.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultados.Visible = false;
                grdBancaQualificacaoDefesa.Visible = true;
            }
            else
            {
                msgSemResultados.Visible = true;
            }
            divResultados.Visible = true;
        }

        protected void btnPerquisaProfessor_Click(object sender, EventArgs e)
        {
            CarregarDados();
        }

        public string SetDataQualificacao(object objeto)
        {
            HashSet<banca> lista = (HashSet<banca>)objeto;
            string sAux = "";
            sAux = "";

            if (lista.Any(x => x.tipo_banca == "Qualificação" && x.horario != null))
            {
                sAux = String.Format("{0:dd/MM/yyyy}", lista.Where(x => x.tipo_banca == "Qualificação").FirstOrDefault().horario);
            }
            return sAux;
        }

        public string SetDataDefesa(object objeto)
        {
            HashSet<banca> lista = (HashSet<banca>)objeto;
            string sAux = "";
            sAux = "";

            if (lista.Any(x => x.tipo_banca == "Defesa" && x.horario != null))
            {
                sAux = String.Format("{0:dd/MM/yyyy}", lista.Where(x => x.tipo_banca == "Defesa").FirstOrDefault().horario);
            }
            return sAux;
        }

        protected void grdBancaQualificacaoDefesa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            if (grdBancaQualificacaoDefesa.Rows.Count > 0)
            {
                bool a = true;
                //!chkCpfAluno.Checked
                if (!a)
                {
                    grdBancaQualificacaoDefesa.HeaderRow.Cells[3].CssClass = "hidden notexport";
                    grdBancaQualificacaoDefesa.Columns[3].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdBancaQualificacaoDefesa.HeaderRow.Cells[3].CssClass = "centralizarTH";
                    grdBancaQualificacaoDefesa.Columns[3].ItemStyle.CssClass = "centralizarTH";
                }

            }

        }

    }
}