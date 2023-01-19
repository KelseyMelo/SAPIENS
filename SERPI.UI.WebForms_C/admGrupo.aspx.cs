using System;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class admGrupo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 2)) // 2. Cadastro Grupo - Verifica se o usuário tem acesso à essa página, caso não redireciona para a Grupo Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                if (Session["arrayFiltroGrupo_Acesso"] != null)
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
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            string[] arrayFiltroGrupo_Acesso = new string[2];

            grupos_acesso item = new grupos_acesso();
            int qIdGrupo = 0;

            arrayFiltroGrupo_Acesso = (string[])Session["arrayFiltroGrupo_Acesso"];

            if (arrayFiltroGrupo_Acesso[0] != "" && arrayFiltroGrupo_Acesso[0] != null)
            {
                item.grupo = arrayFiltroGrupo_Acesso[0];
                txtNomeGrupo.Value = arrayFiltroGrupo_Acesso[0];
            }


            if (arrayFiltroGrupo_Acesso[1] != "" && arrayFiltroGrupo_Acesso[1] != null)
            {
                item.descricao = arrayFiltroGrupo_Acesso[1];
                txtDescricaoGrupo.Value = arrayFiltroGrupo_Acesso[1];
            }

            //Session["arrayFiltroGrupo_Acesso"] = arrayFiltroGrupo_Acesso;
            GrupoAplicacao aplicacaoGrupo = new GrupoAplicacao();
            List<grupos_acesso> lista = new List<grupos_acesso>();
            lista = aplicacaoGrupo.ListaItem(item);
            grdResultado.DataSource = lista;
            grdResultado.DataBind();

            if (lista.Count > 0)
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

        protected void btnPerquisaGrupo_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroGrupo_Acesso = new string[2];

            if (txtNomeGrupo.Value.Trim() != "")
            {
                arrayFiltroGrupo_Acesso[0] = txtNomeGrupo.Value.Trim();
            }

            if (txtDescricaoGrupo.Value.Trim() != "")
            {
                arrayFiltroGrupo_Acesso[1] = txtDescricaoGrupo.Value.Trim();
            }

            Session["arrayFiltroGrupo_Acesso"] = arrayFiltroGrupo_Acesso;

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
        //    usuarios_concentracao item = new usuarios_concentracao();
        //    item.id_Quadrimestre_concentracao = codigo;
        //    switch (grdResultado.DataKeys[linha].Values[1].ToString())
        //    {
        //        case "Editar":
        //            QuadrimestreAplicacao aplicacaoQuadrimestre = new QuadrimestreAplicacao();
        //            item = aplicacaoQuadrimestre.BuscaItem(item);
        //            Session.Add("usuarios_concentracao", item);
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
                grupos_acesso item = new grupos_acesso();
                GrupoAplicacao aplicacao = new GrupoAplicacao();
                item.id_grupo = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0]);
                item = aplicacao.BuscaItem(item);
                Session["grupos_acesso"] = item;
                Session["sNewgrupos_acesso"] = false;
                Response.Redirect("admGrupoGestao.aspx", true);
            }
        }

        protected void btnCriarGrupo_Click(object sender, EventArgs e)
        {
            Session["sNewgrupos_acesso"] = true;
            Session["usuarios"] = null;
            Response.Redirect("cadUsuarioGestao.aspx", true);
        }

    }
}