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
    public partial class cadRelProfessorOferecimento : System.Web.UI.Page
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
                if (grdProfessorOferecimento.Rows.Count != 0)
                {
                    grdProfessorOferecimento.HeaderRow.TableSection = TableRowSection.TableHeader;
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

            ////Session["arrayFiltroAluno"] = arrayFiltroAluno;
            ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
            List<oferecimentos_professores> listaOferecimento = new List<oferecimentos_professores>();
            listaOferecimento = aplicacaoProfessor.ListaOferecimento(item);
            grdProfessorOferecimento.DataSource = listaOferecimento;
            grdProfessorOferecimento.DataBind();

            if (listaOferecimento.Count > 0)
            {
                grdProfessorOferecimento.UseAccessibleHeader = true;
                grdProfessorOferecimento.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultados.Visible = false;
                grdProfessorOferecimento.Visible = true;
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

        protected void grdProfessorOferecimento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            if (grdProfessorOferecimento.Rows.Count > 0)
            {
                bool a = true;
                //!chkCpfAluno.Checked
                if (!a)
                {
                    grdProfessorOferecimento.HeaderRow.Cells[3].CssClass = "hidden notexport";
                    grdProfessorOferecimento.Columns[3].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdProfessorOferecimento.HeaderRow.Cells[3].CssClass = "centralizarTH";
                    grdProfessorOferecimento.Columns[3].ItemStyle.CssClass = "centralizarTH";
                }

            }

        }

    }
}