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
    public partial class finRelBoleto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 26)) // Sessõa Fianceiro - Relatório de Boletos - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
                int[] qIdCurso = new int[1];
                qIdCurso[0] = 0;
                List<periodo_inscricao> listaPeriodo = aplicacaoInscricao.ListaPeriodoInscricao(qIdCurso);
                var listaPeriodo2 = from elemento in listaPeriodo
                                    select new
                                    {
                                        id_periodo = elemento.id_periodo,
                                        periodo = elemento.quadrimestre + " de " + String.Format("{0:dd/MM/yyyy}", elemento.data_inicio) + " à " + String.Format("{0:dd/MM/yyyy}", elemento.data_fim)
                                    };

                ddlPeriodoBoleto.Items.Clear();
                ddlPeriodoBoleto.DataSource = listaPeriodo2;
                ddlPeriodoBoleto.DataValueField = "id_periodo";
                ddlPeriodoBoleto.DataTextField = "periodo";
                ddlPeriodoBoleto.DataBind();
                ddlPeriodoBoleto.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Período", ""));
                ddlPeriodoBoleto.SelectedValue = "";

            }
            else
            {
                //if (grdResultado.Rows.Count != 0)
                //{

                //    grdResultado.HeaderRow.TableSection = TableRowSection.TableHeader;
                //}
            }
        }

        public void ddlPeriodoBoleto_SelectedIndexChanged(Object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];
            //if (ddlTipoCursoAluno.SelectedValue != "")
            //{
            InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
            periodo_inscricao item = new periodo_inscricao();
            ddlCursoBoleto.Items.Clear();

            if (ddlPeriodoBoleto.SelectedValue != "")
            {
                item.id_periodo = Convert.ToInt32(ddlPeriodoBoleto.SelectedValue);
                List<cursos> listaCurso = new List<cursos>();


                if (usuario.id_grupo_acesso == 10) //Grupo Coordenador
                {
                    //É do grupo coordenador então pegar todos os cursos em que ele é coordenador
                    ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
                    professores item_professor = new professores();

                    if (usuario.usuario.Substring(usuario.usuario.Length - 1, 1) == "p")
                    {
                        item_professor.id_professor = Convert.ToInt32(usuario.usuario.Substring(0, usuario.usuario.Length - 1));
                        item_professor = aplicacaoProfessor.BuscaItem(item_professor);
                    }
                    else
                    {
                        item_professor.cpf = usuario.usuario;
                        item_professor = aplicacaoProfessor.BuscaItem_byCPF(item_professor);
                    }

                    if (item_professor != null)
                    {
                        var qIdCurso = item_professor.cursos_coordenadores.Select(x => x.id_curso).ToArray();

                        listaCurso = aplicacaoInscricao.ListaCursoPeriodo(item, qIdCurso);
                    }

                }
                else
                {
                    int[] qIdCurso = new int[1];
                    qIdCurso[0] = 0;

                    listaCurso = aplicacaoInscricao.ListaCursoPeriodo(item, qIdCurso);
                }

                var lista = from elemento in listaCurso
                            select new
                            {
                                id_curso = elemento.id_curso,
                                nome = elemento.tipos_curso.tipo_curso + " - " + elemento.sigla + " - " + elemento.nome
                            };

                ddlCursoBoleto.Items.Clear();
                ddlCursoBoleto.DataSource = lista.OrderBy(x => x.nome);
                ddlCursoBoleto.DataValueField = "id_curso";
                ddlCursoBoleto.DataTextField = "nome";
                ddlCursoBoleto.DataBind();
                ddlCursoBoleto.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os Cursos", ""));
                ddlCursoBoleto.SelectedValue = "";
           }
           
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fSelect2();", true);
        }

        private void CarregarDados()
        {
            string[] aFiltroProfessor = new string[4];

            professores item = new professores();

           

            if (aFiltroProfessor[3] != "" && aFiltroProfessor[3] != null)
            {
                item.status = aFiltroProfessor[3];
                optSituacaoBoletoPagos.Checked = false;
                optSituacaoBoletoNaoPagos.Checked = false;
                optSituacaoBoletoTodos.Checked = false;

              
            }

            //Session["aFiltroProfessor"] = aFiltroProfessor;
            ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
            List<professores> lista = new List<professores>();
            lista = aplicacaoProfessor.ListaItem(item);
            //grdResultado.DataSource = lista;
            //grdResultado.DataBind();

            //if (lista.Count > 0)
            //{
            //    grdResultado.UseAccessibleHeader = true;
            //    grdResultado.HeaderRow.TableSection = TableRowSection.TableHeader;
            //    msgSemResultados.Visible = false;
            //    grdResultado.Visible = true;
            //}
            //else
            //{
            //    msgSemResultados.Visible = true;
            //}
            //divResultados.Visible = true;
        }

        protected void btnPerquisaProfessor_Click(object sender, EventArgs e)
        {
            string[] aFiltroProfessor = new string[4];

            

            Session["aFiltroProfessor"] = aFiltroProfessor;

            CarregarDados();
        }

        public void grdResultado_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                int linha = Convert.ToInt32(e.CommandArgument);
                //int codigo = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0]);
                professores item = new professores();
                ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
                //item.id_professor = codigo;
                item = aplicacaoProfessor.BuscaItem(item);
                Session["professores"] = item;
                Session["sNewProfessor"] = false;
                Response.Redirect("cadProfessorGestao.aspx", true);
            }
        }

        protected void btnCriarProfessor_Click(object sender, EventArgs e)
        {
            Session["sNewProfessor"] = true;
            Session["professores"] = null;
            Response.Redirect("cadProfessorGestao.aspx", true);
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