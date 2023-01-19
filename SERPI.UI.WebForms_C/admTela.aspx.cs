
using System;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class admTela : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 3)) // 3. Cadastro Tela - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
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

                ddlGrupoTela.Items.Clear();
                ddlGrupoTela.DataSource = lista.OrderBy(x => x.grupo);
                ddlGrupoTela.DataValueField = "id_grupo";
                ddlGrupoTela.DataTextField = "grupo";
                ddlGrupoTela.DataBind();
                ddlGrupoTela.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os Grupos", ""));
                ddlGrupoTela.SelectedValue = "";

                if (Session["arrayFiltroTela"] != null)
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

            string[] arrayFiltroTela = new string[6];

            telas_sistema item = new telas_sistema();
            int qIdGrupo = 0;

            arrayFiltroTela = (string[])Session["arrayFiltroTela"];

            if (arrayFiltroTela[0] != "" && arrayFiltroTela[0] != null)
            {
                item.tela = arrayFiltroTela[0];
                txtNomeTela.Value = arrayFiltroTela[0];
            }


            if (arrayFiltroTela[1] != "" && arrayFiltroTela[1] != null)
            {
                item.tela = arrayFiltroTela[1];
                txtDescricaoTela.Value = arrayFiltroTela[1];
            }

            if (arrayFiltroTela[2] != "" && arrayFiltroTela[2] != null)
            {
                qIdGrupo = Convert.ToInt32(arrayFiltroTela[2]);
                ddlGrupoTela.SelectedValue = arrayFiltroTela[2];
            }

            if (arrayFiltroTela[3] != "" && arrayFiltroTela[3] != null)
            {
                item.modulo_sapiens = arrayFiltroTela[3];
                ddlModuloSapiens.SelectedValue = arrayFiltroTela[3];
            }

            if (arrayFiltroTela[4] != "" && arrayFiltroTela[4] != null)
            {
                item.descricao_sapiens = arrayFiltroTela[4];
                txtDescricaoSapiens.Value = arrayFiltroTela[4];
            }

            if (arrayFiltroTela[5] != "" && arrayFiltroTela[5] != null)
            {
                optSituacaoSim.Checked = false;
                optSituacaoNao.Checked = false;
                optSituacaoTodos.Checked = false;

                if (arrayFiltroTela[5] == "Ativo")
                {
                    optSituacaoSim.Checked = true;
                    item.status = 1;
                }
                else if (arrayFiltroTela[5] == "Inativo")
                {
                    optSituacaoNao.Checked = true;
                    item.status = 0;
                }
                else
                {
                    optSituacaoTodos.Checked = true;
                    item.status = 2;
                }
            }

            //Session["arrayFiltroTela"] = arrayFiltroTela;
            TelaAplicacao aplicacaoTela = new TelaAplicacao();
            List<telas_sistema> lista = new List<telas_sistema>();
            lista = aplicacaoTela.ListaItem(item,qIdGrupo);
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

        protected void btnPerquisaTela_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroTela = new string[6];

            if (txtNomeTela.Value.Trim() != "")
            {
                arrayFiltroTela[0] = txtNomeTela.Value.Trim();
            }

            if (txtDescricaoTela.Value.Trim() != "")
            {
                arrayFiltroTela[1] = txtDescricaoTela.Value.Trim();
            }

            if (ddlGrupoTela.SelectedValue != "")
            {
                arrayFiltroTela[2] = ddlGrupoTela.SelectedValue;
            }

            if (ddlModuloSapiens.SelectedValue != "")
            {
                arrayFiltroTela[3] = ddlModuloSapiens.SelectedValue;
            }

            if (txtDescricaoSapiens.Value.Trim() != "")
            {
                arrayFiltroTela[4] = txtDescricaoSapiens.Value.Trim();
            }

            // 0 = Aguardando aprovação -- 1 = Aprovado-- 2 = Reprovado-- 3 = Sem página
            if (optSituacaoSim.Checked)
            {
                arrayFiltroTela[5] = "Ativo";
            }
            else if (optSituacaoNao.Checked)
            {
                arrayFiltroTela[5] = "Inativo";
            }
            else
            {
                arrayFiltroTela[5] = "Todos";
            }

            Session["arrayFiltroTela"] = arrayFiltroTela;

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
                telas_sistema item = new telas_sistema();
                TelaAplicacao aplicacao = new TelaAplicacao();
                item.id_tela = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0]);
                item = aplicacao.BuscaItem(item);
                Session["telas_sistema"] = item;
                Session["sNewtelas_sistema"] = false;
                Response.Redirect("admTelaGestao.aspx", true);
            }
        }

        protected void btnCriarTela_Click(object sender, EventArgs e)
        {
            Session["sNewtelas_sistema"] = true;
            Session["usuarios"] = null;
            Response.Redirect("admTelaGestao.aspx", true);
        }

        public string setGrupoTela(object objeto)
        {
            HashSet<grupos_acesso_telas_sistema> lista = (HashSet<grupos_acesso_telas_sistema>)objeto;
            string sAux;
            int iSpace = 22;

            sAux = "";
            foreach (var item in lista.OrderBy(x=> x.grupos_acesso.grupo))
            {
                if (sAux != "")
                {
                    sAux = sAux + "<br>";
                }
                //sAux = sAux + "<span >" + lista.ElementAt(i).grupos_acesso.grupo + "</span>";
                
                if (item.escrita==true)
                {
                    //sAux = new String(' ', (iSpace - item.grupos_acesso.grupo.Length));
                    sAux = sAux + item.grupos_acesso.grupo + new String(' ', (iSpace - item.grupos_acesso.grupo.Length)) + " -  <strong>Escrita</strong>";
                }
                else if (item.leitura == true)
                {
                    sAux = sAux + item.grupos_acesso.grupo + new String(' ', iSpace - item.grupos_acesso.grupo.Length) + " -  <strong><em>Leitura</em></strong>";
                }
                else
                {
                    sAux = sAux + item.grupos_acesso.grupo + new String(' ', iSpace - item.grupos_acesso.grupo.Length) + " -  Nada";
                }
            }

            //for (int i = 0; i < lista.Count; i++)
            //{
            //    if (sAux != "")
            //    {
            //        sAux = sAux + "<br>";
            //    }
            //    //sAux = sAux + "<span >" + lista.ElementAt(i).grupos_acesso.grupo + "</span>";
            //    sAux = sAux + lista.ElementAt(i).grupos_acesso.grupo;
            //}

            sAux = "<div title=\"Visualizar Grupos Associados\"> <a class=\"btn btn-success btn-circle fa fa-search\" href=\'javascript:fAbreGrupos(\""
                            + sAux + "\")\'; ></a></div>";

            return sAux;
        }
    }
}