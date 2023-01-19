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
    public partial class matConfOferecimento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 55)) // 2. Confirmação de Oferecimento - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                //CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                //cursos itemCurso = new cursos();
                //List<cursos> listaCurso = aplicacaoCurso.ListaItem(itemCurso);
                ////var listaPais2 = from item2 in listaPais
                ////                 select new
                ////                 {
                ////                     Id_Pais = item2.Id_Pais,
                ////                     Nacionalidade = item2.Nacionalidade
                ////                 };

                //ddlCodigoCursoProfessor.Items.Clear();
                //ddlCodigoCursoProfessor.DataSource = listaCurso.OrderBy(x => x.sigla);
                //ddlCodigoCursoProfessor.DataValueField = "id_curso";
                //ddlCodigoCursoProfessor.DataTextField = "sigla";
                //ddlCodigoCursoProfessor.DataBind();
                //ddlCodigoCursoProfessor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione uma Sigla do Curso", ""));
                //ddlCodigoCursoProfessor.SelectedValue = "";

                MatriculaAplicacao aplicacaoMatricula = new MatriculaAplicacao();
                periodo_matricula item = new periodo_matricula();
                List<periodo_matricula> lista = aplicacaoMatricula.ListaItem(item);

                var lista2 = from item2 in lista
                    select new
                    {
                        id = item2.id_periodo,
                        descricao = item2.quadrimestre + " (Início: " + String.Format("{0:dd/MM/yyyy}", item2.data_inicio) + " - Término: " + String.Format("{0:dd/MM/yyyy}", item2.data_termino) + ")"
                    };

                ddlPeriodoConfirmacaoMatricula.Items.Clear();
                ddlPeriodoConfirmacaoMatricula.DataSource = lista2;
                ddlPeriodoConfirmacaoMatricula.DataValueField = "id";
                ddlPeriodoConfirmacaoMatricula.DataTextField = "descricao";
                ddlPeriodoConfirmacaoMatricula.DataBind();
                ddlPeriodoConfirmacaoMatricula.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um período", ""));
                ddlPeriodoConfirmacaoMatricula.SelectedValue = "";

                if (Session["aFiltroConfirmacaoMatricula"] != null)
                {
                    CarregarDados();
                }

            }
            else
            {
                if (grdPeriodoConfirmacaoMatricula.Rows.Count != 0)
                {
                    grdPeriodoConfirmacaoMatricula.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        private void CarregarDados()
        {
            string[] aFiltroConfirmacaoMatricula = new string[1];

            periodo_matricula item = new periodo_matricula();

            aFiltroConfirmacaoMatricula = (string[])Session["aFiltroConfirmacaoMatricula"];

            if (aFiltroConfirmacaoMatricula[0] != "" && aFiltroConfirmacaoMatricula[0] != null)
            {
                item.id_periodo = Convert.ToInt32(aFiltroConfirmacaoMatricula[0]);
                ddlPeriodoConfirmacaoMatricula.SelectedValue = aFiltroConfirmacaoMatricula[0];
            }


            //Session["aFiltroProfessor"] = aFiltroProfessor;
            MatriculaAplicacao aplicacaoMatricula = new MatriculaAplicacao();
            List<pre_oferecimentos> lista = aplicacaoMatricula.ListaPreOferecimento(item);
            grdPeriodoConfirmacaoMatricula.DataSource = lista;
            grdPeriodoConfirmacaoMatricula.DataBind();

            if (lista.Count > 0)
            {
                grdPeriodoConfirmacaoMatricula.UseAccessibleHeader = true;
                grdPeriodoConfirmacaoMatricula.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultados.Visible = false;
                grdPeriodoConfirmacaoMatricula.Visible = true;
            }
            else
            {
                msgSemResultados.Visible = true;
            }
            divResultados.Visible = true;
        }

        protected void bntPerquisaMatricula_Click(object sender, EventArgs e)
        {
            string[] aFiltroConfirmacaoMatricula = new string[1];

            if (ddlPeriodoConfirmacaoMatricula.SelectedValue != "")
            {
                aFiltroConfirmacaoMatricula[0] = ddlPeriodoConfirmacaoMatricula.SelectedValue;
            }

            Session["aFiltroConfirmacaoMatricula"] = aFiltroConfirmacaoMatricula;

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
        //    Professors_concentracao item = new Professors_concentracao();
        //    item.id_Professor_concentracao = codigo;
        //    switch (grdResultado.DataKeys[linha].Values[1].ToString())
        //    {
        //        case "Editar":
        //            ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
        //            item = aplicacaoProfessor.BuscaItem(item);
        //            Session.Add("Professors_concentracao", item);
        //            Session.Add("sNewProfessor", false);
        //            Response.Redirect("cadProfessorConcentracaoGestao.aspx", true);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        public void grdPeriodoConfirmacaoMatricula_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                int linha = Convert.ToInt32(e.CommandArgument);
                int codigo = Convert.ToInt32(grdPeriodoConfirmacaoMatricula.DataKeys[linha].Values[0]);
                pre_oferecimentos item = new pre_oferecimentos();
                MatriculaAplicacao aplicacaoMatricula = new MatriculaAplicacao();
                item.id_pre_oferecimento = codigo;
                item = aplicacaoMatricula.BuscaPreOferecimento(item);
                Session["pre_oferecimentos"] = item;
                Response.Redirect("matConfOferecimentoGestao.aspx", true);
            }
        }

        public string setLinkImagem(string cpf, string Nome)
        {
            UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
            usuarios item = new usuarios();
            item.usuario = cpf;
            item = aplicacaoUsuario.BuscaUsuario(item);
            string sAux;

            if (item == null)
            {
                sAux = "<div title=\"Sem foto\"> <a class=\"fa fa-ban\" href=\'#\'; style= color:#428bca;></a></div>";
            }
            else
            {
                if ((item.avatar == null || (item.avatar == "")))
                {
                    sAux = "<div title=\"Sem foto\"> <a class=\"fa fa-ban\" href=\'#\'; style= color:#428bca;></a></div>";
                }
                else
                {
                    sAux = ("<div title=\"Ver foto\"> <a href=\'javascript:fExibeImagem(\"" + (item.avatar + ("\",\"" + (Nome + "\")\' >"))));
                    sAux = (sAux + ("<img class=\"img-circle\" id=\"imageresource\" src=\"" + ("img\\pessoas\\" + (item.avatar + ("?" + (DateTime.Now.ToString() + "\" style=\"width: 35px; height: 35px;\">"))))));
                    sAux = (sAux + "</a></div>");
                }
            }

            return sAux;
        }
    }
}