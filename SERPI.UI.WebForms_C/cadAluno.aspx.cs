using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections;
using System.Net;
using System.Text;
using System.IO;

namespace SERPI.UI.WebForms_C
{
    public partial class cadAluno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null)
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 6)) //1. Alunos
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                litMenu.Text = "<input type=\"hidden\" id =\"hGrupoMenu\"  name=\"hGrupoMenu\" value=\"liAcademico\" /> <input type=\"hidden\" id =\"hItemMenu\"  name=\"hGrupoMenu\" value=\"li1Alunos\" />";
                if (Session["arrayFiltroAluno"] != null)
                {
                    CarregarDados();
                }
            }
            else
            {
                if (grdAluno.Rows.Count != 0)
                {

                    grdAluno.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }

            if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 6))
            {
                if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 6).FirstOrDefault().escrita != true)
                {
                    btnCriarAluno.Visible = false;
                }
            }
        }

        protected void grdAluno_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            grdAluno.PageIndex = e.NewPageIndex;
            grdAluno.SelectedIndex = -1;
        }

        private void CarregarDados()
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            string[] arrayFiltroAluno = new string[8]; 

            alunos item = new alunos();

            arrayFiltroAluno = (string[]) Session["arrayFiltroAluno"];

            if (arrayFiltroAluno[0] != "" && arrayFiltroAluno[0] != null )
            {
                item.idaluno = System.Convert.ToDecimal(arrayFiltroAluno[0]);
                txtMatriculaAluno.Value = arrayFiltroAluno[0];
            }

            if (arrayFiltroAluno[1] != "" && arrayFiltroAluno[1] != null)
            {
                item.nome = arrayFiltroAluno[1];
                txtNomeAluno.Value = arrayFiltroAluno[1];
            }

            if (arrayFiltroAluno[2] != "" && arrayFiltroAluno[2] != null)
            {
                item.cpf = arrayFiltroAluno[2];
                txtCPFAluno.Value = arrayFiltroAluno[2];
            }

            if (arrayFiltroAluno[3] != "" && arrayFiltroAluno[3] != null)
            {
                item.numero_documento = arrayFiltroAluno[3];
                txtRGAluno.Value = arrayFiltroAluno[3];
            }

            if (arrayFiltroAluno[4] != "" && arrayFiltroAluno[4] != null)
            {
                item.email = arrayFiltroAluno[4];
                txtEmailAluno.Value = arrayFiltroAluno[4];
            }

            if (arrayFiltroAluno[5] != "" && arrayFiltroAluno[5] != null)
            {
                item.empresa = arrayFiltroAluno[5];
                txtEmpresaAluno.Value = arrayFiltroAluno[5];
            }

            optProficienciaInglesTodos.Checked = true;
            if (arrayFiltroAluno[6] != "" && arrayFiltroAluno[6] != null)
            {
                item.RefazerProficienciaIngles = System.Convert.ToByte(arrayFiltroAluno[6]);
                if (arrayFiltroAluno[6] == "1")
                {
                    optProficienciaInglesSim.Checked = true;
                }
                else
                {
                    optProficienciaInglesNao.Checked = true;
                }
            }

            optProficienciaPortuguesTodos.Checked = true;
            if (arrayFiltroAluno[7] != "" && arrayFiltroAluno[7] != null)
            {
                item.RefazerProvaPortugues = System.Convert.ToByte(arrayFiltroAluno[7]);
                if (arrayFiltroAluno[7] == "1")
                {
                    optProficienciaPortuguesSim.Checked = true;
                }
                else
                {
                    optProficienciaPortuguesNao.Checked = true;
                }
            }

            //Session["arrayFiltroAluno"] = arrayFiltroAluno;
            AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
            List<alunos> listaAluno = new List<alunos>();
            if (usuario.id_grupo_acesso == 10 ) //Grupo Coordenador
            {
                //É do grupo coordenador então pegar todos os cursos em que ele é coordenador
                ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
                professores item_professor = new professores();

                if (usuario.usuario.Substring(usuario.usuario.Length-1,1) =="p")
                {
                    item_professor.id_professor = Convert.ToInt32(usuario.usuario.Substring(0, usuario.usuario.Length - 1));
                    item_professor = aplicacaoProfessor.BuscaItem(item_professor);
                }
                else
                {
                    item_professor.cpf = usuario.usuario;
                    item_professor = aplicacaoProfessor.BuscaItem_byCPF(item_professor);
                }

                if (item_professor != null)
                {
                    var qIdCurso = item_professor.cursos_coordenadores.Select(x => x.id_curso).ToArray();

                    listaAluno = aplicacaoAluno.ListaItem(item, qIdCurso);
                }

            }
            else
            {
                int[] qIdCurso = new int[1];
                qIdCurso[0] = 0;

                listaAluno = aplicacaoAluno.ListaItem(item, qIdCurso);
            }

            grdAluno.DataSource = listaAluno;
            grdAluno.DataBind();

            if (listaAluno.Count > 0)
            {
                grdAluno.UseAccessibleHeader = true;
                grdAluno.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultados.Visible = false;
                grdAluno.Visible = true;
            }
            else
            { 
                msgSemResultados.Visible = true;
            }
            divResultados.Visible = true;
        }

        protected void bntPerquisaAluno_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroAluno = new string[8];

            if (txtMatriculaAluno.Value.Trim() != "")
            {
                arrayFiltroAluno[0] = txtMatriculaAluno.Value.Trim();
            }

            if (txtNomeAluno.Value.Trim() != "")
            {
                arrayFiltroAluno[1] = txtNomeAluno.Value.Trim();
            }

            if (txtCPFAluno.Value.Trim() != "")
            {
                arrayFiltroAluno[2] = txtCPFAluno.Value.Trim();
            }

            if (txtRGAluno.Value.Trim() != "")
            {
                arrayFiltroAluno[3] = txtRGAluno.Value.Trim();
            }

            if (txtEmailAluno.Value.Trim() != "")
            {
                arrayFiltroAluno[4] = txtEmailAluno.Value.Trim();
            }

            if (txtEmpresaAluno.Value.Trim() != "")
            {
                arrayFiltroAluno[5] = txtEmpresaAluno.Value.Trim();
            }

            if (optProficienciaInglesSim.Checked)
            {
                arrayFiltroAluno[6] = "1";
            }
            else if (optProficienciaInglesSim.Checked)
            {
                arrayFiltroAluno[6] = "0";
            }
            else
            {
                arrayFiltroAluno[6] = null;
            }

            if (optProficienciaPortuguesSim.Checked)
            {
                arrayFiltroAluno[7] = "1";
            }
            else if (optProficienciaPortuguesNao.Checked)
            {
                arrayFiltroAluno[7] = "0";
            }
            else
            {
                arrayFiltroAluno[7] = null;
            }

            Session["arrayFiltroAluno"] = arrayFiltroAluno;

            CarregarDados();

        }

        public string setTurmaCurso(object objeto)
        {
            HashSet<matricula_turma> lista = (HashSet<matricula_turma>)objeto;
            string sAux;
            sAux = "";
            for (int i = 0; i < lista.Count; i++)
            {
                if (sAux != "")
                {
                    sAux = sAux + "<hr />";
                }
                sAux = sAux + "<span style=\"line-height: 2.2em;\">" + lista.ElementAt(i).turmas.cod_turma + " - " + lista.ElementAt(i).turmas.cursos.nome + "</span>";
            }

            return sAux;
        }

        protected void grdAluno_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Page")
            {
                return;
            }
            
            int linha = Convert.ToInt32(e.CommandArgument);
            int codigo = Convert.ToInt32(grdAluno.DataKeys[linha].Values[0]);
            alunos item = new alunos();
            item.idaluno = codigo;
            switch (grdAluno.DataKeys[linha].Values[1].ToString())
            {
                case "Editar":
                    AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                    item = aplicacaoAluno.BuscaItem(item);
                    Session.Add("Aluno", item);
                    Session.Add("sNovoAluno", false);
                    Response.Redirect("cadAlunoGestao.aspx", true);
                    break;
                default:
                    break;
            }
        }

        public void grdAluno_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                int linha = Convert.ToInt32(e.CommandArgument);
                int codigo = Convert.ToInt32(grdAluno.DataKeys[linha].Values[0]);
                alunos item = new alunos();
                item.idaluno = codigo;
                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                item = aplicacaoAluno.BuscaItem(item);
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

                //Response.Redirect("cadAlunoGestao.aspx?hQTab=" + qTab, true);
            }
        }

        protected void btnCriarAluno_Click(object sender, EventArgs e)
        {
            string qTab = HttpContext.Current.Request.Form["hQTab"];
            Session[qTab + "sNovoAluno"] = true;
            Session[qTab + "Aluno"] = null;

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

            //Session.Add("sNovoAluno", true);
            //Response.Redirect("cadAlunoGestao.aspx", true);
        }
    }
}