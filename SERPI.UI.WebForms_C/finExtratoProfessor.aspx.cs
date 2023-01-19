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
    public partial class finExtratoProfessor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 54)) // 4. Extrato do Professor - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
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

                //ddlNomeCursoProfessor.Items.Clear();
                //ddlNomeCursoProfessor.DataSource = listaCurso;
                //ddlNomeCursoProfessor.DataValueField = "id_curso";
                //ddlNomeCursoProfessor.DataTextField = "nome";
                //ddlNomeCursoProfessor.DataBind();
                //ddlNomeCursoProfessor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Curso", ""));
                //ddlNomeCursoProfessor.SelectedValue = "";

                if (Session["aFiltroExtratoProfessor"] != null)
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
            string[] aFiltroExtratoProfessor = new string[4];

            professores item = new professores();

            aFiltroExtratoProfessor = (string[])Session["aFiltroExtratoProfessor"];

            if (aFiltroExtratoProfessor[0] != "" && aFiltroExtratoProfessor[0] != null)
            {
                item.id_professor = Convert.ToInt32(aFiltroExtratoProfessor[0]);
                txtIdProfessor.Value = aFiltroExtratoProfessor[0];
            }

            if (aFiltroExtratoProfessor[1] != "" && aFiltroExtratoProfessor[1] != null)
            {
                item.nome = aFiltroExtratoProfessor[1];
                txtNomeProfessor.Value = aFiltroExtratoProfessor[1];
            }

            if (aFiltroExtratoProfessor[2] != "" && aFiltroExtratoProfessor[2] != null)
            {
                item.cpf = aFiltroExtratoProfessor[2];
            }

            if (aFiltroExtratoProfessor[3] != "" && aFiltroExtratoProfessor[3] != null)
            {
                item.status = aFiltroExtratoProfessor[3];
                optSituacaoProfessorSim.Checked = false;
                optSituacaoProfessorNao.Checked = false;
                optSituacaoProfessorTodos.Checked = false;

                if (aFiltroExtratoProfessor[3] == "ativado")
                {
                    optSituacaoProfessorSim.Checked = true;
                }
                else if (aFiltroExtratoProfessor[3] == "inativado")
                {
                    optSituacaoProfessorNao.Checked = true;
                }
                else
                {
                    optSituacaoProfessorTodos.Checked = true;
                }
            }

            //Session["aFiltroExtratoProfessor"] = aFiltroExtratoProfessor;
            FinanceiroAplicacao aplicacaoFinanceiro = new FinanceiroAplicacao();
            List<geral_extrato_professor> lista = new List<geral_extrato_professor>();
            lista = aplicacaoFinanceiro.ListaProfessores(item);
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

        protected void btnPerquisaProfessor_Click(object sender, EventArgs e)
        {
            string[] aFiltroExtratoProfessor = new string[4];

            if (txtIdProfessor.Value.Trim() != "")
            {
                aFiltroExtratoProfessor[0] = txtIdProfessor.Value.Trim();
            }

            if (txtNomeProfessor.Value.Trim() != "")
            {
                aFiltroExtratoProfessor[1] = txtNomeProfessor.Value.Trim();
            }

            if (txtCPFProfessor.Value.Trim() != "")
            {
                aFiltroExtratoProfessor[2] = txtCPFProfessor.Value.Trim();
            }

            if (optSituacaoProfessorSim.Checked)
            {
                aFiltroExtratoProfessor[3] = "ativado";
            }
            else if (optSituacaoProfessorNao.Checked)
            {
                aFiltroExtratoProfessor[3] = "inativado";
            }
            else
            {
                aFiltroExtratoProfessor[3] = "todos";
            }

            Session["aFiltroExtratoProfessor"] = aFiltroExtratoProfessor;

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

        public void grdResultado_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                int linha = Convert.ToInt32(e.CommandArgument);
                int codigo = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0]);
                professores item = new professores();
                ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
                item.id_professor = codigo;
                item = aplicacaoProfessor.BuscaItem(item);
                Session["professores"] = item;
                Response.Redirect("finExtratoProfessorDetalhe.aspx", true);
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

        public string setSaldo(decimal plano, decimal pagamento)
        {
            return string.Format("{0:C}", plano - pagamento); ;
        }
    }
}