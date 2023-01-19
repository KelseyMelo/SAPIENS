using System;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SERPI.UI.WebForms_C
{
    public partial class cadAreaConcentracao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 13)) // Área de Concentração - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos itemCurso = new cursos();
                List<cursos> listaCurso = aplicacaoCurso.ListaItem(itemCurso);
                //var listaPais2 = from item2 in listaPais
                //                 select new
                //                 {
                //                     Id_Pais = item2.Id_Pais,
                //                     Nacionalidade = item2.Nacionalidade
                //                 };

                ddlCodigoCursoArea.Items.Clear();
                ddlCodigoCursoArea.DataSource = listaCurso.OrderBy(x => x.sigla);
                ddlCodigoCursoArea.DataValueField = "id_curso";
                ddlCodigoCursoArea.DataTextField = "sigla";
                ddlCodigoCursoArea.DataBind();
                ddlCodigoCursoArea.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione uma Sigla do Curso", ""));
                ddlCodigoCursoArea.SelectedValue = "";

                ddlNomeCursoArea.Items.Clear();
                ddlNomeCursoArea.DataSource = listaCurso;
                ddlNomeCursoArea.DataValueField = "id_curso";
                ddlNomeCursoArea.DataTextField = "nome";
                ddlNomeCursoArea.DataBind();
                ddlNomeCursoArea.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Curso", ""));
                ddlNomeCursoArea.SelectedValue = "";

                if (Session["arrayFiltroArea"] != null)
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
            string[] arrayFiltroArea = new string[3];

            areas_concentracao item = new areas_concentracao();

            arrayFiltroArea = (string[])Session["arrayFiltroArea"];

            if (arrayFiltroArea[0] != "" && arrayFiltroArea[0] != null)
            {
                item.nome = arrayFiltroArea[0];
                txtNomeArea.Value = arrayFiltroArea[0];
            }

            if (arrayFiltroArea[1] != "" && arrayFiltroArea[1] != null)
            {
                item.id_curso = System.Convert.ToInt32(arrayFiltroArea[1]);
                ddlCodigoCursoArea.SelectedValue = arrayFiltroArea[1];
                ddlNomeCursoArea.SelectedValue = arrayFiltroArea[1];
            }

            if (arrayFiltroArea[2] != "" && arrayFiltroArea[2] != null)
            {
                item.status = arrayFiltroArea[2];
                optSituacaoSim.Checked = false;
                optSituacaoNao.Checked = false;
                optSituacaoTodos.Checked = false;

                if (arrayFiltroArea[2] == "ativado")
                {
                    optSituacaoSim.Checked = true;
                }
                else if (arrayFiltroArea[2] == "inativado")
                {
                    optSituacaoNao.Checked = true;
                }
                else
                {
                    optSituacaoTodos.Checked = true;
                }

            }

            //Session["arrayFiltroArea"] = arrayFiltroArea;
            AreaAplicacao aplicacaoAluno = new AreaAplicacao();
            List<areas_concentracao> listaArea = new List<areas_concentracao>();
            listaArea = aplicacaoAluno.ListaItem(item);
            grdResultado.DataSource = listaArea;
            grdResultado.DataBind();

            if (listaArea.Count > 0)
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

        protected void btnPerquisaArea_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroArea = new string[3];

            if (txtNomeArea.Value.Trim() != "")
            {
                arrayFiltroArea[0] = txtNomeArea.Value.Trim();
            }

            if (ddlCodigoCursoArea.SelectedValue != "")
            {
                arrayFiltroArea[1] = ddlCodigoCursoArea.SelectedValue;
            }

            if (optSituacaoSim.Checked)
            {
                arrayFiltroArea[2] = "ativado";
            }
            else if (optSituacaoNao.Checked)
            {
                arrayFiltroArea[2] = "inativado";
            }
            else
            {
                arrayFiltroArea[2] = "todos";
            }

            Session["arrayFiltroArea"] = arrayFiltroArea;

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
        //    areas_concentracao item = new areas_concentracao();
        //    item.id_area_concentracao = codigo;
        //    switch (grdResultado.DataKeys[linha].Values[1].ToString())
        //    {
        //        case "Editar":
        //            AreaAplicacao aplicacaoArea = new AreaAplicacao();
        //            item = aplicacaoArea.BuscaItem(item);
        //            Session.Add("areas_concentracao", item);
        //            Session.Add("sNewArea", false);
        //            Response.Redirect("cadAreaConcentracaoGestao.aspx", true);
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
                areas_concentracao item = new areas_concentracao();
                AreaAplicacao aplicacaoArea = new AreaAplicacao();
                item.id_area_concentracao = codigo;
                item = aplicacaoArea.BuscaItem(item);
                Session["areas_concentracao"] = item;
                Session["sNewArea"] = false;
                Response.Redirect("cadAreaConcentracaoGestao.aspx", true);
            }
        }

        protected void btnCriarArea_Click(object sender, EventArgs e)
        {
            Session["sNewArea"] = true;
            Session["areas_concentracao"] = null;
            Response.Redirect("cadAreaConcentracaoGestao.aspx", true);
        }

    }
}