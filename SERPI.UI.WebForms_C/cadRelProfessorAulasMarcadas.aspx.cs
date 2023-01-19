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
    public partial class cadRelProfessorAulasMarcadas : System.Web.UI.Page
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
                if (DateTime.Today.Month == 1)
                {
                    ddlMesCalculoCusto.SelectedValue = "12";
                    txtAnoCalculoCusto.Value = (DateTime.Today.Year - 1).ToString();
                }
                else
                {
                    ddlMesCalculoCusto.SelectedValue = (DateTime.Today.Month -1 ).ToString();
                    txtAnoCalculoCusto.Value = DateTime.Today.Year.ToString();
                }
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
            string sAux = "";
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

            if (txtAnoCalculoCusto.Value.Trim() == "")
            {
                sAux = sAux + "Digite um Ano. <br/><br/>";
            }
            else if (Convert.ToInt32(txtAnoCalculoCusto.Value.Trim()) < 2000)
            {
                sAux = sAux + "O Ano deve ser maior que 2000. <br/><br/>";
            }

            if (sAux != "")
            {
                lblMensagem.Text = sAux;
                lblTituloMensagem.Text = "Atenção";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                return;
            }

            ////Session["arrayFiltroAluno"] = arrayFiltroAluno;
            ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
            List<presenca_professor> listaOferecimento = new List<presenca_professor>();

            DateTime qData = Convert.ToDateTime("01/" + ddlMesCalculoCusto.SelectedValue + "/" + txtAnoCalculoCusto.Value);

            string qPresenca;

            if (optPresenteSim.Checked)
            {
                qPresenca = "1";
            }
            else if (optPresenteNao.Checked)
            {
                qPresenca = "2";
            }
            else
            {
                qPresenca = "0";
            }

            listaOferecimento = aplicacaoProfessor.ListaAulasMarcadas(qData, qPresenca);
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

        public string SetCurso(object tabela)
        {
            HashSet<cursos_disciplinas> lista = (HashSet<cursos_disciplinas>)tabela;
            string sAux = "";
            sAux = "";

            foreach (var elemento in lista)
            {
                sAux = elemento.cursos.sigla;
                break;
            }
            return sAux;
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