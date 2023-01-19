using System;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class cadPeriodo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 14)) // 6. Quadrimestres - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                List<tipos_curso> listaTipoCurso = aplicacaoGerais.ListaTipoCurso();
                //var listaPais2 = from item2 in listaPais
                //                 select new
                //                 {
                //                     Id_Pais = item2.Id_Pais,
                //                     Nacionalidade = item2.Nacionalidade
                //                 };

                ddlTipoCursoPeriodo.Items.Clear();
                ddlTipoCursoPeriodo.DataSource = listaTipoCurso.OrderBy(x => x.id_tipo_curso);
                ddlTipoCursoPeriodo.DataValueField = "id_tipo_curso";
                ddlTipoCursoPeriodo.DataTextField = "tipo_curso";
                ddlTipoCursoPeriodo.DataBind();
                ddlTipoCursoPeriodo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os Tipos", ""));
                ddlTipoCursoPeriodo.SelectedValue = "";

                if (Session["arrayFiltroQuadrimestre"] != null)
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
            string[] arrayFiltroQuadrimestre = new string[2];

            quadrimestres item = new quadrimestres();

            arrayFiltroQuadrimestre = (string[])Session["arrayFiltroQuadrimestre"];

            if (arrayFiltroQuadrimestre[0] != "" && arrayFiltroQuadrimestre[0] != null)
            {
                item.ano = arrayFiltroQuadrimestre[0];
                txtAnoPeriodo.Value = arrayFiltroQuadrimestre[0];
            }

            if (arrayFiltroQuadrimestre[1] != "" && arrayFiltroQuadrimestre[1] != null)
            {
                item.id_tipo_curso = Convert.ToInt32(arrayFiltroQuadrimestre[1]);
                ddlTipoCursoPeriodo.SelectedValue = arrayFiltroQuadrimestre[1];
            }

            if (arrayFiltroQuadrimestre[2] != "" && arrayFiltroQuadrimestre[2] != null)
            {
                item.status = arrayFiltroQuadrimestre[2];
                optSituacaoSim.Checked = false;
                optSituacaoNao.Checked = false;
                optSituacaoTodos.Checked = false;

                if (arrayFiltroQuadrimestre[2] == "ativado")
                {
                    optSituacaoSim.Checked = true;
                }
                else if (arrayFiltroQuadrimestre[2] == "inativado")
                {
                    optSituacaoNao.Checked = true;
                }
                else
                {
                    optSituacaoTodos.Checked = true;
                }
            }

            //Session["arrayFiltroQuadrimestre"] = arrayFiltroQuadrimestre;
            QuadrimestreAplicacao aplicacaoQuadrimestre = new QuadrimestreAplicacao();
            List<quadrimestres> listaQuadrimestre = new List<quadrimestres>();
            listaQuadrimestre = aplicacaoQuadrimestre.ListaItem(item);
            grdResultado.DataSource = listaQuadrimestre;
            grdResultado.DataBind();

            if (listaQuadrimestre.Count > 0)
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

        protected void btnPerquisaQuadrimestre_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroQuadrimestre = new string[3];

            if (txtAnoPeriodo.Value.Trim() != "")
            {
                arrayFiltroQuadrimestre[0] = txtAnoPeriodo.Value.Trim();
            }

            if (ddlTipoCursoPeriodo.SelectedValue != "")
            {
                arrayFiltroQuadrimestre[1] = ddlTipoCursoPeriodo.SelectedValue;
            }

            if (optSituacaoSim.Checked)
            {
                arrayFiltroQuadrimestre[2] = "ativado";
            }
            else if (optSituacaoNao.Checked)
            {
                arrayFiltroQuadrimestre[2] = "inativado";
            }
            else
            {
                arrayFiltroQuadrimestre[2] = "todos";
            }

            Session["arrayFiltroQuadrimestre"] = arrayFiltroQuadrimestre;

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
        //    quadrimestres_concentracao item = new quadrimestres_concentracao();
        //    item.id_Quadrimestre_concentracao = codigo;
        //    switch (grdResultado.DataKeys[linha].Values[1].ToString())
        //    {
        //        case "Editar":
        //            QuadrimestreAplicacao aplicacaoQuadrimestre = new QuadrimestreAplicacao();
        //            item = aplicacaoQuadrimestre.BuscaItem(item);
        //            Session.Add("quadrimestres_concentracao", item);
        //            Session.Add("sNewQuadrimestre", false);
        //            Response.Redirect("cadQuadrimestreConcentracaoGestao.aspx", true);
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
                //int codigo = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0]);
                quadrimestres item = new quadrimestres();
                QuadrimestreAplicacao aplicacaoQuadrimestre = new QuadrimestreAplicacao();
                item.quadrimestre = grdResultado.DataKeys[linha].Values[0].ToString();
                item = aplicacaoQuadrimestre.BuscaItem(item);
                Session["quadrimestres"] = item;
                Session["sNewQuadrimestre"] = false;
                Response.Redirect("cadPeriodoGestao.aspx", true);
            }
        }

        protected void btnCriarQuadrimestre_Click(object sender, EventArgs e)
        {
            Session["sNewQuadrimestre"] = true;
            Session["quadrimestres"] = null;
            Response.Redirect("cadPeriodoGestao.aspx", true);
        }
    }
}