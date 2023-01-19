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
    public partial class cadRelBancaMembro : System.Web.UI.Page
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
                List<cursos> listaCurso = aplicacaoCurso.ListaItem(item);
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
                if (grdBancaMembros.Rows.Count != 0)
                {
                    grdBancaMembros.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        private void CarregarDados()
        {
            professores item = new professores();

            int qIdCurso = 0;
            int qMes = 0;
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
            List<banca> listaBanca = new List<banca>();
            listaBanca = aplicacaoGerais.ListaMembros(qIdCurso, qMes, qAno, qCondicao, qTipoBanca);
            grdBancaMembros.DataSource = listaBanca;
            grdBancaMembros.DataBind();

            if (listaBanca.Count > 0)
            {
                grdBancaMembros.UseAccessibleHeader = true;
                grdBancaMembros.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultados.Visible = false;
                grdBancaMembros.Visible = true;
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

        public string SetProfessores(object objeto)
        {
            HashSet<banca_professores> lista = (HashSet<banca_professores>)objeto;
            string sAux = "";
            sAux = "";

            foreach (var elemento in lista.OrderBy(x => x.tipo_professor == "Membro Suplente").ThenBy(x => x.tipo_professor == "Membro").ThenBy(x => x.tipo_professor == "Orientador"))
            {
                if (sAux != "")
                {
                    sAux = sAux + (char)10 + "<br>";
                }

                sAux = sAux + elemento.professores.nome;

            }
            return sAux;
        }

        public string SetPapel(object objeto)
        {
            HashSet<banca_professores> lista = (HashSet<banca_professores>)objeto;
            string sAux = "";
            sAux = "";

            foreach (var elemento in lista.OrderBy(x=> x.tipo_professor == "Membro Suplente").ThenBy(x => x.tipo_professor == "Membro").ThenBy(x => x.tipo_professor == "Orientador"))
            {
                if (sAux != "")
                {
                    sAux = sAux + (char)10 + "<br>";
                }

                if (elemento.tipo_professor == "Membro Suplente")
                {
                    sAux = sAux + "Suplente";
                }
                else
                {
                    sAux = sAux + elemento.tipo_professor;
                }
            }
            return sAux;
        }

        protected void grdBancaQualificacaoDefesa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            if (grdBancaMembros.Rows.Count > 0)
            {
                bool a = true;
                //!chkCpfAluno.Checked
                if (!a)
                {
                    grdBancaMembros.HeaderRow.Cells[3].CssClass = "hidden notexport";
                    grdBancaMembros.Columns[3].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdBancaMembros.HeaderRow.Cells[3].CssClass = "centralizarTH";
                    grdBancaMembros.Columns[3].ItemStyle.CssClass = "centralizarTH";
                }

            }

        }

    }
}