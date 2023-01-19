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
    public partial class finCustosCursos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 44)) // 1. Custos por Curso - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                List<tipos_curso> listaTipoCurso = aplicacaoCurso.ListaTipoCurso();
                //var listaPais2 = from item2 in listaPais
                //                 select new
                //                 {
                //                     Id_Pais = item2.Id_Pais,
                //                     Nacionalidade = item2.Nacionalidade
                //                 };

                ddlTipoCurso.Items.Clear();
                ddlTipoCurso.DataSource = listaTipoCurso;
                ddlTipoCurso.DataValueField = "id_tipo_curso";
                ddlTipoCurso.DataTextField = "tipo_curso";
                ddlTipoCurso.DataBind();
                ddlTipoCurso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", ""));
                ddlTipoCurso.SelectedValue = "";

                if (Session["arrayFiltroCursoCusto"] != null)
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
            string[] arrayFiltroCursoCusto = new string[4];

            cursos item = new cursos();

            arrayFiltroCursoCusto = (string[])Session["arrayFiltroCursoCusto"];

            if (arrayFiltroCursoCusto[0] != "" && arrayFiltroCursoCusto[0] != null)
            {
                item.sigla = arrayFiltroCursoCusto[0];
                txtCodigoCurso.Value = arrayFiltroCursoCusto[0];
            }

            if (arrayFiltroCursoCusto[1] != "" && arrayFiltroCursoCusto[1] != null)
            {
                item.nome = arrayFiltroCursoCusto[1];
                txtNomeCurso.Value = arrayFiltroCursoCusto[1];
            }

            if (arrayFiltroCursoCusto[2] != "" && arrayFiltroCursoCusto[2] != null)
            {
                item.id_tipo_curso = System.Convert.ToInt32(arrayFiltroCursoCusto[2]);
                ddlTipoCurso.SelectedValue = arrayFiltroCursoCusto[2];
            }

            if (arrayFiltroCursoCusto[3] != "" && arrayFiltroCursoCusto[3] != null)
            {
                item.status = arrayFiltroCursoCusto[3];
                optSituacaoSim.Checked = false;
                optSituacaoNao.Checked = false;
                optSituacaoTodos.Checked = false;

                if (arrayFiltroCursoCusto[3] == "ativado")
                {
                    optSituacaoSim.Checked = true;
                }
                else if (arrayFiltroCursoCusto[3] == "inativado")
                {
                    optSituacaoNao.Checked = true;
                }
                else
                {
                    optSituacaoTodos.Checked = true;
                }
            }

            //Session["arrayFiltroCursoCusto"] = arrayFiltroCursoCusto;
            CursoAplicacao aplicacaoCurso = new CursoAplicacao();
            List<cursos> listaCurso = new List<cursos>();
            listaCurso = aplicacaoCurso.ListaItem(item);
            grdResultado.DataSource = listaCurso;
            grdResultado.DataBind();

            if (listaCurso.Count > 0)
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

        protected void btnPerquisaCurso_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroCursoCusto = new string[4];

            if (txtCodigoCurso.Value.Trim() != "")
            {
                arrayFiltroCursoCusto[0] = txtCodigoCurso.Value.Trim();
            }

            if (txtNomeCurso.Value.Trim() != "")
            {
                arrayFiltroCursoCusto[1] = txtNomeCurso.Value.Trim();
            }

            if (ddlTipoCurso.SelectedValue != "")
            {
                arrayFiltroCursoCusto[2] = ddlTipoCurso.SelectedValue;
            }

            if (optSituacaoSim.Checked)
            {
                arrayFiltroCursoCusto[3] = "ativado";
            }
            else if (optSituacaoNao.Checked)
            {
                arrayFiltroCursoCusto[3] = "inativado";
            }
            else
            {
                arrayFiltroCursoCusto[3] = "todos";
            }

            Session["arrayFiltroCursoCusto"] = arrayFiltroCursoCusto;

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
                cursos item = new cursos();
                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                item.id_curso = codigo;
                item = aplicacaoCurso.BuscaItem(item);
                Session["fin_cursos"] = item;
                Response.Redirect("finCustosCursosGestao.aspx", true);
            }
        }

        protected void btnCriarCurso_Click(object sender, EventArgs e)
        {
            Session["sNewCurso"] = true;
            Session["cursos"] = null;
            Response.Redirect("cadCursoGestao.aspx", true);
        }
    }
}