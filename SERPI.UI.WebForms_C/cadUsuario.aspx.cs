using System;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class cadUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 1)) // 1. Cadastro usuário - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                GrupoAplicacao aplicacaoGrupo = new GrupoAplicacao();
                grupos_acesso item = new grupos_acesso();
                List<grupos_acesso> lista = aplicacaoGrupo.ListaItem(item);
                //var listaPais2 = from item2 in listaPais
                //                 select new
                //                 {
                //                     Id_Pais = item2.Id_Pais,
                //                     Nacionalidade = item2.Nacionalidade
                //                 };

                ddlGrupo.Items.Clear();
                ddlGrupo.DataSource = lista.OrderBy(x => x.id_grupo);
                ddlGrupo.DataValueField = "id_grupo";
                ddlGrupo.DataTextField = "grupo";
                ddlGrupo.DataBind();
                ddlGrupo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os Grupos", ""));
                ddlGrupo.SelectedValue = "";

                if (Session["arrayFiltroGrupo"] != null)
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
            string[] arrayFiltroGrupo = new string[3];

            usuarios item = new usuarios();
            item.grupos_acesso = new grupos_acesso();

            arrayFiltroGrupo = (string[])Session["arrayFiltroGrupo"];

            if (arrayFiltroGrupo[0] != "" && arrayFiltroGrupo[0] != null)
            {
                item.nome  = arrayFiltroGrupo[0];
                txtNomeUsuario.Value = arrayFiltroGrupo[0];
            }

            if (arrayFiltroGrupo[1] != "" && arrayFiltroGrupo[1] != null)
            {
                item.id_grupo_acesso = Convert.ToInt32(arrayFiltroGrupo[1]);
                ddlGrupo.SelectedValue = arrayFiltroGrupo[1];
            }

            if (arrayFiltroGrupo[2] != "" && arrayFiltroGrupo[2] != null)
            {
                if (arrayFiltroGrupo[2] != "todos")
                {
                    item.status = Convert.ToInt16(arrayFiltroGrupo[2]);
                }
                optSituacaoSim.Checked = false;
                optSituacaoNao.Checked = false;
                optSituacaoTodos.Checked = false;

                if (arrayFiltroGrupo[2] == "1")
                {
                    optSituacaoSim.Checked = true;
                }
                else if (arrayFiltroGrupo[2] == "0")
                {
                    optSituacaoNao.Checked = true;
                }
                else
                {
                    optSituacaoTodos.Checked = true;
                }
            }

            //Session["arrayFiltroGrupo"] = arrayFiltroGrupo;
            UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
            List<usuarios> lista = new List<usuarios>();
            lista = aplicacaoUsuario.ListaUsuario(item);
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

        protected void btnPerquisaUsuario_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroGrupo = new string[3];

            if (txtNomeUsuario.Value.Trim() != "")
            {
                arrayFiltroGrupo[0] = txtNomeUsuario.Value.Trim();
            }

            if (ddlGrupo.SelectedValue != "")
            {
                arrayFiltroGrupo[1] = ddlGrupo.SelectedValue;
            }

            if (optSituacaoSim.Checked)
            {
                arrayFiltroGrupo[2] = "1";
            }
            else if (optSituacaoNao.Checked)
            {
                arrayFiltroGrupo[2] = "0";
            }
            else
            {
                arrayFiltroGrupo[2] = "todos";
            }

            Session["arrayFiltroGrupo"] = arrayFiltroGrupo;

            CarregarDados();
        }

        public string setBloqueio(object objeto)
        {
            HashSet<usuarios_log> lista = (HashSet<usuarios_log>)objeto;
            string sAux;
            sAux = "";

            if (lista.Count == 0)
            {
                sAux = "Não";
            }
            else if (lista.ElementAt(0).status_bloqueio_total == 0)
            {
                sAux = "Não";
            }
            else
            {
                sAux = "<strong class=\"text-danger\">Sim</span>";
            }
            return sAux;
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
                usuarios item = new usuarios();
                UsuarioAplicacao aplicacao = new UsuarioAplicacao();
                item.usuario = grdResultado.DataKeys[linha].Values[0].ToString();
                item = aplicacao.BuscaUsuario(item);
                Session["usuarios"] = item;
                Session["sNewUsuario"] = false;
                Response.Redirect("cadUsuarioGestao.aspx", true);
            }
        }

        protected void btnCriarUsuario_Click(object sender, EventArgs e)
        {
            Session["sNewUsuario"] = true;
            Session["usuarios"] = null;
            Response.Redirect("cadUsuarioGestao.aspx", true);
        }
    }
}