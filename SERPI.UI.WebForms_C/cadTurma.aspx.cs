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
    public partial class cadTurma : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 15)) //7.Turmas - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
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

                ddlCursoTurma.Items.Clear();
                ddlCursoTurma.DataSource = lista.OrderBy(x=> x.nome);
                ddlCursoTurma.DataValueField = "id_curso";
                ddlCursoTurma.DataTextField = "nome";
                ddlCursoTurma.DataBind();
                ddlCursoTurma.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", ""));
                ddlCursoTurma.SelectedValue = "";

                if (Session["arrayFiltroTurma"] != null)
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
            string[] arrayFiltroTurma = new string[3];

            turmas item = new turmas();

            arrayFiltroTurma = (string[])Session["arrayFiltroTurma"];

            if (arrayFiltroTurma[0] != "" && arrayFiltroTurma[0] != null)
            {
                item.cod_turma = arrayFiltroTurma[0];
                txtNumeroTurma.Value = arrayFiltroTurma[0];
            }

            if (arrayFiltroTurma[1] != "" && arrayFiltroTurma[1] != null)
            {
                item.id_curso = System.Convert.ToInt32(arrayFiltroTurma[1]);
                ddlCursoTurma.SelectedValue = arrayFiltroTurma[1];
            }

            if (arrayFiltroTurma[2] != "" && arrayFiltroTurma[2] != null)
            {
                item.status = arrayFiltroTurma[2];
                optSituacaoSim.Checked = false;
                optSituacaoNao.Checked = false;
                optSituacaoTodos.Checked = false;

                if (arrayFiltroTurma[2] == "ativado")
                {
                    optSituacaoSim.Checked = true;
                }
                else if (arrayFiltroTurma[2] == "inativado")
                {
                    optSituacaoNao.Checked = true;
                }
                else
                {
                    optSituacaoTodos.Checked = true;
                }
            }

            //Session["arrayFiltroTurma"] = arrayFiltroTurma;
            TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
            List<turmas> listaTurma = new List<turmas>();
            item.cursos = new cursos();
            listaTurma = aplicacaoTurma.ListaItem(item);
            grdResultado.DataSource = listaTurma;
            grdResultado.DataBind();

            if (listaTurma.Count > 0)
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

        protected void btnPerquisaTurma_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroTurma = new string[3];

            if (txtNumeroTurma.Value.Trim() != "")
            {
                arrayFiltroTurma[0] = txtNumeroTurma.Value.Trim();
            }

            if (ddlCursoTurma.SelectedValue != "")
            {
                arrayFiltroTurma[1] = ddlCursoTurma.SelectedValue;
            }

            if (optSituacaoSim.Checked)
            {
                arrayFiltroTurma[2] = "ativado";
            }
            else if (optSituacaoNao.Checked)
            {
                arrayFiltroTurma[2] = "inativado";
            }
            else
            {
                arrayFiltroTurma[2] = "todos";
            }

            Session["arrayFiltroTurma"] = arrayFiltroTurma;

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
        //    Cursos_concentracao item = new Cursos_concentracao();
        //    item.id_Curso_concentracao = codigo;
        //    switch (grdResultado.DataKeys[linha].Values[1].ToString())
        //    {
        //        case "Editar":
        //            CursoAplicacao aplicacaoCurso = new CursoAplicacao();
        //            item = aplicacaoCurso.BuscaItem(item);
        //            Session.Add("Cursos_concentracao", item);
        //            Session.Add("sNewCurso", false);
        //            Response.Redirect("cadCursoConcentracaoGestao.aspx", true);
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
                int codigo = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0]);
                turmas item = new turmas();
                TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                item.id_turma = codigo;
                item = aplicacaoTurma.BuscaItem(item);
                Session["turmas"] = item;
                Session["sNewTurma"] = false;
                Response.Redirect("cadTurmaGestao.aspx", true);
            }
        }

        protected void btnCriarTurma_Click(object sender, EventArgs e)
        {
            Session["sNewTurma"] = true;
            Session["turmas"] = null;
            Response.Redirect("cadTurmaGestao.aspx", true);
        }
    }
}