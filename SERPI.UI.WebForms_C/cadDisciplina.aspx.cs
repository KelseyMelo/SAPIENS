using System;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class cadDisciplina : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 9)) // Disciplina - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
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

                ddlCodigoCursoDisciplina.Items.Clear();
                ddlCodigoCursoDisciplina.DataSource = listaCurso.OrderBy(x => x.sigla);
                ddlCodigoCursoDisciplina.DataValueField = "id_curso";
                ddlCodigoCursoDisciplina.DataTextField = "sigla";
                ddlCodigoCursoDisciplina.DataBind();
                ddlCodigoCursoDisciplina.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os cursos", ""));
                ddlCodigoCursoDisciplina.SelectedValue = "";

                ddlNomeCursoDisciplina.Items.Clear();
                ddlNomeCursoDisciplina.DataSource = listaCurso;
                ddlNomeCursoDisciplina.DataValueField = "id_curso";
                ddlNomeCursoDisciplina.DataTextField = "nome";
                ddlNomeCursoDisciplina.DataBind();
                ddlNomeCursoDisciplina.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os cursos", ""));
                ddlNomeCursoDisciplina.SelectedValue = "";

                if (Session["arrayFiltroDisciplina"] != null)
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
            string[] arrayFiltroDisciplina = new string[4];

            disciplinas item = new disciplinas();

            item.cursos_disciplinas.Add (new cursos_disciplinas());

            arrayFiltroDisciplina = (string[])Session["arrayFiltroDisciplina"];

            if (arrayFiltroDisciplina[0] != "" && arrayFiltroDisciplina[0] != null)
            {
                item.codigo  = arrayFiltroDisciplina[0];
                txtCodigoDisciplina.Value = arrayFiltroDisciplina[0];
            }

            if (arrayFiltroDisciplina[1] != "" && arrayFiltroDisciplina[1] != null)
            {
                item.nome = arrayFiltroDisciplina[1];
                txtNomeDisciplina.Value = arrayFiltroDisciplina[1];
            }

            if (arrayFiltroDisciplina[2] != "" && arrayFiltroDisciplina[2] != null)
            {
                item.cursos_disciplinas.ElementAt(0).id_curso = System.Convert.ToInt32(arrayFiltroDisciplina[2]);
                ddlCodigoCursoDisciplina.SelectedValue = arrayFiltroDisciplina[2];
                ddlNomeCursoDisciplina.SelectedValue = arrayFiltroDisciplina[2];
            }

            if (arrayFiltroDisciplina[3] != "" && arrayFiltroDisciplina[3] != null)
            {
                item.status = arrayFiltroDisciplina[3];
                optSituacaoSim.Checked = false;
                optSituacaoNao.Checked = false;
                optSituacaoTodos.Checked = false;

                if (arrayFiltroDisciplina[3] == "ativado")
                {
                    optSituacaoSim.Checked = true;
                }
                else if (arrayFiltroDisciplina[3] == "inativado")
                {
                    optSituacaoNao.Checked = true;
                }
                else
                {
                    optSituacaoTodos.Checked = true;
                }
            }

            //Session["arrayFiltroDisciplina"] = arrayFiltroDisciplina;
            DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
            List<disciplinas> listaDisciplina = new List<disciplinas>();
            listaDisciplina = aplicacaoDisciplina.ListaItem(item);
            grdResultado.DataSource = listaDisciplina;
            grdResultado.DataBind();

            if (listaDisciplina.Count > 0)
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

        protected void btnPerquisaDisciplina_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroDisciplina = new string[4];

            if (txtCodigoDisciplina.Value.Trim() != "")
            {
                arrayFiltroDisciplina[0] = txtCodigoDisciplina.Value.Trim();
            }

            if (txtNomeDisciplina.Value.Trim() != "")
            {
                arrayFiltroDisciplina[1] = txtNomeDisciplina.Value.Trim();
            }

            if (ddlCodigoCursoDisciplina.SelectedValue != "")
            {
                arrayFiltroDisciplina[2] = ddlCodigoCursoDisciplina.SelectedValue;
            }

            if (optSituacaoSim.Checked)
            {
                arrayFiltroDisciplina[3] = "ativado";
            }
            else if (optSituacaoNao.Checked)
            {
                arrayFiltroDisciplina[3] = "inativado";
            }
            else
            {
                arrayFiltroDisciplina[3] = "todos";
            }

            Session["arrayFiltroDisciplina"] = arrayFiltroDisciplina;

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
        //    Disciplinas_concentracao item = new Disciplinas_concentracao();
        //    item.id_Disciplina_concentracao = codigo;
        //    switch (grdResultado.DataKeys[linha].Values[1].ToString())
        //    {
        //        case "Editar":
        //            DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
        //            item = aplicacaoDisciplina.BuscaItem(item);
        //            Session.Add("Disciplinas_concentracao", item);
        //            Session.Add("sNewDisciplina", false);
        //            Response.Redirect("cadDisciplinaConcentracaoGestao.aspx", true);
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
                disciplinas item = new disciplinas();
                DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                item.id_disciplina = codigo;
                item = aplicacaoDisciplina.BuscaItem(item);
                Session["disciplinas"] = item;
                Session["sNewDisciplina"] = false;
                Response.Redirect("cadDisciplinaGestao.aspx", true);
            }
        }

        protected void btnCriarDisciplina_Click(object sender, EventArgs e)
        {
            Session["sNewDisciplina"] = true;
            Session["disciplinas"] = null;
            Response.Redirect("cadDisciplinaGestao.aspx", true);
        }

        public string setTipoMestrado(object objeto)
        {
            HashSet<cursos_disciplinas> lista = (HashSet<cursos_disciplinas>)objeto;
            string sAux;
            sAux = "";
            for (int i = 0; i < lista.Count; i++)
            {
                if (sAux != "")
                {
                    sAux = sAux + "<hr />";
                }
                sAux = sAux + "<span >" + lista.ElementAt(i).cursos.tipos_curso.tipo_curso + " - " + lista.ElementAt(i).cursos.sigla + "</span>";
            }

            return sAux;
        }
    }
}