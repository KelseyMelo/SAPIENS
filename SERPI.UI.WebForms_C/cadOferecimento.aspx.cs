using System;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class cadOferecimento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 16)) // 8. Oferecimentos - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                GeraisAplicacao aplicacaoGarais = new GeraisAplicacao();
                List<tipos_curso> listaTipoCurso = aplicacaoGarais.ListaTipoCurso();

                ddlTipoCursoOferecimento.Items.Clear();
                ddlTipoCursoOferecimento.DataSource = listaTipoCurso.OrderBy(x => x.id_tipo_curso);
                ddlTipoCursoOferecimento.DataValueField = "id_tipo_curso";
                ddlTipoCursoOferecimento.DataTextField = "tipo_curso";
                ddlTipoCursoOferecimento.DataBind();
                ddlTipoCursoOferecimento.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos Tipos de Curso", ""));
                ddlTipoCursoOferecimento.SelectedValue = "";

                ProfessorAplicacao aplicacaoProfessores = new ProfessorAplicacao();
                professores itemProfessor = new professores();
                List<professores> listaProfessores = aplicacaoProfessores.ListaItem(itemProfessor);

                ddlProfessorOferecimento.Items.Clear();
                ddlProfessorOferecimento.DataSource = listaProfessores.OrderBy(x => x.nome);
                ddlProfessorOferecimento.DataValueField = "id_professor";
                ddlProfessorOferecimento.DataTextField = "nome";
                ddlProfessorOferecimento.DataBind();
                ddlProfessorOferecimento.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos Professores", ""));
                ddlProfessorOferecimento.SelectedValue = "";

                if (Session["arrayFiltroOferecimento"] != null)
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

        public void ddlTipoCursoOferecimento_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (ddlTipoCursoOferecimento.SelectedValue != "")
            {
                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos item = new cursos();
                item.id_tipo_curso = Convert.ToInt32(ddlTipoCursoOferecimento.SelectedValue);
                List<cursos> listaCurso = aplicacaoCurso.ListaItem(item);
                var lista = from item2 in listaCurso
                            select new
                            {
                                id_curso = item2.id_curso,
                                nome = item2.sigla + " - " + item2.nome
                            };

                ddlNomeCursoOferecimento.Items.Clear();
                ddlNomeCursoOferecimento.DataSource = lista.OrderBy(x => x.nome);
                ddlNomeCursoOferecimento.DataValueField = "id_curso";
                ddlNomeCursoOferecimento.DataTextField = "nome";
                ddlNomeCursoOferecimento.DataBind();
                ddlNomeCursoOferecimento.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os Cursos", ""));
                ddlNomeCursoOferecimento.SelectedValue = "";

                QuadrimestreAplicacao aplicacaoPeriodo = new QuadrimestreAplicacao();
                quadrimestres itemPeriodo = new quadrimestres();
                itemPeriodo.id_tipo_curso = Convert.ToInt32(ddlTipoCursoOferecimento.SelectedValue);
                List<quadrimestres> listaPeriodo = aplicacaoPeriodo.ListaItem(itemPeriodo);
                ddlPeriodoOferecimento.Items.Clear();
                ddlPeriodoOferecimento.DataSource = listaPeriodo.OrderByDescending(x => x.quadrimestre);
                ddlPeriodoOferecimento.DataValueField = "quadrimestre";
                ddlPeriodoOferecimento.DataTextField = "quadrimestre";
                ddlPeriodoOferecimento.DataBind();
                ddlPeriodoOferecimento.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os Períodos", ""));
                ddlPeriodoOferecimento.SelectedValue = "";
            }
            else
            {
                ddlNomeCursoOferecimento.Items.Clear();
                ddlPeriodoOferecimento.Items.Clear();
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fSelect2();", true);
        }

        private void CarregarDados()
        {
            string[] arrayFiltroOferecimento = new string[11];

            oferecimentos item = new oferecimentos();

            decimal idProfessor = 0;

            DateTime sDataInicio = new DateTime();

            DateTime sDataFim = new DateTime();

            item.disciplinas = new disciplinas();

            item.disciplinas.cursos_disciplinas.Add(new cursos_disciplinas());

            item.disciplinas.cursos_disciplinas.ElementAt(0).cursos = new cursos();

            arrayFiltroOferecimento = (string[])Session["arrayFiltroOferecimento"];

            if (arrayFiltroOferecimento[0] != "" && arrayFiltroOferecimento[0] != null)
            {
                item.disciplinas.codigo = arrayFiltroOferecimento[0];
                txtCodigoDisciplinaOferecimento.Value = arrayFiltroOferecimento[0];
            }

            if (arrayFiltroOferecimento[1] != "" && arrayFiltroOferecimento[1] != null)
            {
                item.disciplinas.nome = arrayFiltroOferecimento[1];
                txtNomeDisciplinaOferecimento.Value = arrayFiltroOferecimento[1];
            }

            if (arrayFiltroOferecimento[2] != "" && arrayFiltroOferecimento[2] != null)
            {
                item.disciplinas.cursos_disciplinas.ElementAt(0).cursos.id_tipo_curso = System.Convert.ToInt32(arrayFiltroOferecimento[2]);
                ddlTipoCursoOferecimento.SelectedValue = arrayFiltroOferecimento[2];
                ddlTipoCursoOferecimento_SelectedIndexChanged(null, null);
            }

            if (arrayFiltroOferecimento[3] != "" && arrayFiltroOferecimento[3] != null)
            {
                item.disciplinas.cursos_disciplinas.ElementAt(0).id_curso = System.Convert.ToInt32(arrayFiltroOferecimento[3]);
                if (ddlTipoCursoOferecimento.SelectedValue != "" && ddlNomeCursoOferecimento.Items.Count == 0)
                {
                    ddlTipoCursoOferecimento_SelectedIndexChanged(null, null);
                }
                ddlNomeCursoOferecimento.SelectedValue = arrayFiltroOferecimento[3];
            }

            if (arrayFiltroOferecimento[4] != "" && arrayFiltroOferecimento[4] != null)
            {
                item.quadrimestre = arrayFiltroOferecimento[4];
                ddlPeriodoOferecimento.SelectedValue = arrayFiltroOferecimento[4];
            }

            if (arrayFiltroOferecimento[5] != "" && arrayFiltroOferecimento[5] != null)
            {
                item.ativo = Convert.ToBoolean(arrayFiltroOferecimento[5]);
                chkAtivoOferecimento.Checked = Convert.ToBoolean(arrayFiltroOferecimento[5]);
            }

            if (arrayFiltroOferecimento[6] != "" && arrayFiltroOferecimento[6] != null)
            {
                item.status = arrayFiltroOferecimento[6];
                optSituacaoSim.Checked = false;
                optSituacaoNao.Checked = false;
                optSituacaoTodos.Checked = false;

                if (arrayFiltroOferecimento[6] == "ativado")
                {
                    optSituacaoSim.Checked = true;
                }
                else if (arrayFiltroOferecimento[6] == "inativado")
                {
                    optSituacaoNao.Checked = true;
                }
                else
                {
                    optSituacaoTodos.Checked = true;
                }
            }

            if (arrayFiltroOferecimento[7] != "" && arrayFiltroOferecimento[7] != null)
            {
                idProfessor = Convert.ToDecimal(arrayFiltroOferecimento[7]);
                ddlProfessorOferecimento.SelectedValue = arrayFiltroOferecimento[7];
            }
            else
            {
                idProfessor = 0;
                ddlProfessorOferecimento.SelectedValue = "";
            }

            if (arrayFiltroOferecimento[8] != "" && arrayFiltroOferecimento[8] != null)
            {
                sDataInicio = Convert.ToDateTime(arrayFiltroOferecimento[8]);
                txtDataInicioAula.Value = arrayFiltroOferecimento[8];
            }

            if (arrayFiltroOferecimento[9] != "" && arrayFiltroOferecimento[9] != null)
            {
                sDataFim = Convert.ToDateTime(arrayFiltroOferecimento[9]);
                txtDataFimAula.Value = arrayFiltroOferecimento[9];
            }

            if (arrayFiltroOferecimento[10] != "" && arrayFiltroOferecimento[10] != null)
            {
                item.id_oferecimento = Convert.ToInt32(arrayFiltroOferecimento[10]);
                txtIdOferecimento.Value = arrayFiltroOferecimento[10];
            }

            //Session["arrayFiltroOferecimento"] = arrayFiltroOferecimento;
            OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
            List<oferecimentos> listaOferecimento = new List<oferecimentos>();
            listaOferecimento = aplicacaoOferecimento.ListaItem(item, idProfessor, sDataInicio, sDataFim);
            grdResultado.DataSource = listaOferecimento;
            grdResultado.DataBind();

            if (listaOferecimento.Count > 0)
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

        protected void btnPerquisaOferecimento_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroOferecimento = new string[11];

            if (txtCodigoDisciplinaOferecimento.Value.Trim() != "")
            {
                arrayFiltroOferecimento[0] = txtCodigoDisciplinaOferecimento.Value.Trim();
            }

            if (txtNomeDisciplinaOferecimento.Value.Trim() != "")
            {
                arrayFiltroOferecimento[1] = txtNomeDisciplinaOferecimento.Value.Trim();
            }

            if (ddlTipoCursoOferecimento.SelectedValue != "")
            {
                arrayFiltroOferecimento[2] = ddlTipoCursoOferecimento.SelectedValue;
            }

            if (ddlNomeCursoOferecimento.SelectedValue != "")
            {
                arrayFiltroOferecimento[3] = ddlNomeCursoOferecimento.SelectedValue;
            }

            if (ddlPeriodoOferecimento.SelectedValue != "")
            {
                arrayFiltroOferecimento[4] = ddlPeriodoOferecimento.SelectedValue;
            }

            if (chkAtivoOferecimento.Checked)
            {
                arrayFiltroOferecimento[5] = chkAtivoOferecimento.Checked.ToString();
            }
            else
            {
                arrayFiltroOferecimento[5] = "";
            }

            if (optSituacaoSim.Checked)
            {
                arrayFiltroOferecimento[6] = "ativado";
            }
            else if (optSituacaoNao.Checked)
            {
                arrayFiltroOferecimento[6] = "inativado";
            }
            else
            {
                arrayFiltroOferecimento[6] = "todos";
            }

            if (ddlProfessorOferecimento.SelectedValue != "")
            {
                arrayFiltroOferecimento[7] = ddlProfessorOferecimento.SelectedValue;
            }
            else
            {
                arrayFiltroOferecimento[7] = "";
            }

            if (txtDataInicioAula.Value.Trim() != "")
            {
                arrayFiltroOferecimento[8] = txtDataInicioAula.Value.Trim();
            }

            if (txtDataFimAula.Value.Trim() != "")
            {
                arrayFiltroOferecimento[9] = txtDataFimAula.Value.Trim();
            }

            if (txtIdOferecimento.Value.Trim() != "")
            {
                arrayFiltroOferecimento[10] = txtIdOferecimento.Value.Trim();
            }

            Session["arrayFiltroOferecimento"] = arrayFiltroOferecimento;

            CarregarDados();
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

        //protected void grdResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Page")
        //    {
        //        return;
        //    }

        //    int linha = Convert.ToInt32(e.CommandArgument);
        //    int codigo = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0]);
        //    Oferecimentos_concentracao item = new Oferecimentos_concentracao();
        //    item.id_Oferecimento_concentracao = codigo;
        //    switch (grdResultado.DataKeys[linha].Values[1].ToString())
        //    {
        //        case "Editar":
        //            OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
        //            item = aplicacaoOferecimento.BuscaItem(item);
        //            Session.Add("Oferecimentos_concentracao", item);
        //            Session.Add("sNewOferecimento", false);
        //            Response.Redirect("cadOferecimentoConcentracaoGestao.aspx", true);
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
                oferecimentos item = new oferecimentos();
                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                item.id_oferecimento = codigo;
                item = aplicacaoOferecimento.BuscaItem(item);
                Session["oferecimentos"] = item;
                Session["sNewOferecimento"] = false;
                Response.Redirect("cadOferecimentoGestao.aspx", true);
            }
        }

        protected void btnCriarOferecimento_Click(object sender, EventArgs e)
        {
            Session["sNewOferecimento"] = true;
            Session["oferecimentos"] = null;
            Response.Redirect("cadOferecimentoGestao.aspx", true);
        }
    }
}