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
    public partial class cadRelProfessorQualificacaoDefesa : System.Web.UI.Page
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

            }
            else
            {
                if (grdProfessorQualificacaoDefesa.Rows.Count != 0)
                {
                    grdProfessorQualificacaoDefesa.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        private void CarregarDados()
        {
            professores item = new professores();

            //int pIdTipoCurso = 0;
            //int pIdCurso = 0;
            //string qTurma = "";
            //int pIdOfereciemnto = 0;
            //string qArea = "";
            //string qSituacao = "";
            //string qTipoMatricula = "";

            if (txtCpfProfessor.Value.Trim() != "")
            {
                item.cpf = txtCpfProfessor.Value.Trim();
            }

            if (txtNomeProfessor.Value.Trim() != "")
            {
                item.nome = txtNomeProfessor.Value.Trim();
            }

            string qTipoBanca;

            qTipoBanca = ddlTipoQualificacaoDefesa.SelectedValue;

            ////Session["arrayFiltroAluno"] = arrayFiltroAluno;
            ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
            List<banca_professores> listaBanca = new List<banca_professores>();
            listaBanca = aplicacaoProfessor.ListaQualificacaoDefesa(item, qTipoBanca);
            grdProfessorQualificacaoDefesa.DataSource = listaBanca;
            grdProfessorQualificacaoDefesa.DataBind();

            if (listaBanca.Count > 0)
            {
                grdProfessorQualificacaoDefesa.UseAccessibleHeader = true;
                grdProfessorQualificacaoDefesa.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultados.Visible = false;
                grdProfessorQualificacaoDefesa.Visible = true;
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

        protected void grdProfessorQualificacaoDefesa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            if (grdProfessorQualificacaoDefesa.Rows.Count > 0)
            {
                bool a = true;
                //!chkCpfAluno.Checked
                if (!a)
                {
                    grdProfessorQualificacaoDefesa.HeaderRow.Cells[3].CssClass = "hidden notexport";
                    grdProfessorQualificacaoDefesa.Columns[3].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdProfessorQualificacaoDefesa.HeaderRow.Cells[3].CssClass = "centralizarTH";
                    grdProfessorQualificacaoDefesa.Columns[3].ItemStyle.CssClass = "centralizarTH";
                }

            }

        }

    }
}