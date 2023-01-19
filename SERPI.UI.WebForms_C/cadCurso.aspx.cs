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
    public partial class cadCurso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 12)) // Curso - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                List<tipos_curso> listaTipoCurso = aplicacaoCurso.ListaTipoCurso();
                //var listaPais2 = from item2 in listaPais
                //                 select new
                //                 {
                //                     Id_Pais = item2.Id_Pais,
                //                     Nacionalidade = item2.Nacionalidade
                //                 };

                ddlTipoCurso.Items.Clear();
                ddlTipoCurso.DataSource = listaTipoCurso;
                ddlTipoCurso.DataValueField = "id_tipo_curso";
                ddlTipoCurso.DataTextField = "tipo_curso";
                ddlTipoCurso.DataBind();
                ddlTipoCurso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", ""));
                ddlTipoCurso.SelectedValue = "";

                if (Session["arrayFiltroCurso"] != null)
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
            string[] arrayFiltroCurso = new string[5];

            cursos item = new cursos();

            arrayFiltroCurso = (string[])Session["arrayFiltroCurso"];

            if (arrayFiltroCurso[0] != "" && arrayFiltroCurso[0] != null)
            {
                item.sigla = arrayFiltroCurso[0];
                txtCodigoCurso.Value = arrayFiltroCurso[0];
            }

            if (arrayFiltroCurso[1] != "" && arrayFiltroCurso[1] != null)
            {
                item.nome = arrayFiltroCurso[1];
                txtNomeCurso.Value = arrayFiltroCurso[1];
            }

            if (arrayFiltroCurso[2] != "" && arrayFiltroCurso[2] != null)
            {
                item.id_tipo_curso = System.Convert.ToInt32(arrayFiltroCurso[2]);
                ddlTipoCurso.SelectedValue = arrayFiltroCurso[2];
            }

            if (arrayFiltroCurso[3] != "" && arrayFiltroCurso[3] != null)
            {
                item.status = arrayFiltroCurso[3];
                optSituacaoSim.Checked = false;
                optSituacaoNao.Checked = false;
                optSituacaoTodos.Checked = false;

                if (arrayFiltroCurso[3] == "ativado")
                {
                    optSituacaoSim.Checked = true;
                }
                else if (arrayFiltroCurso[3] == "inativado")
                {
                    optSituacaoNao.Checked = true;
                }
                else
                {
                    optSituacaoTodos.Checked = true;
                }
            }

            if (arrayFiltroCurso[4] != "" && arrayFiltroCurso[4] != null)
            {
                item.statusAprovado = Convert.ToInt16(arrayFiltroCurso[4]);
                optStatusHomeTodos.Checked = false;
                optStatusHomeAprovado.Checked = false;
                optStatusHomeAguardando.Checked = false;
                optStatusHomeReprovado.Checked = false;

                // 0 = Aguardando aprovação -- 1 = Aprovado-- 2 = Reprovado-- 3 = Sem página

                if (arrayFiltroCurso[4] == "2")
                {
                    optStatusHomeReprovado.Checked = true;
                }
                else if (arrayFiltroCurso[4] == "1")
                {
                    optStatusHomeAprovado.Checked = true;
                }
                else if (arrayFiltroCurso[4] == "0")
                {
                    optStatusHomeAguardando.Checked = true;
                }
                else
                {
                    optStatusHomeTodos.Checked = true;
                }
            }

            //Session["arrayFiltroCurso"] = arrayFiltroCurso;
            CursoAplicacao aplicacaoCurso = new CursoAplicacao();
            List<cursos> listaCurso = new List<cursos>();
            listaCurso = aplicacaoCurso.ListaItem(item);
            grdResultado.DataSource = listaCurso;
            grdResultado.DataBind();

            if (listaCurso.Count > 0)
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

        protected void btnPerquisaCurso_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroCurso = new string[5];

            if (txtCodigoCurso.Value.Trim() != "")
            {
                arrayFiltroCurso[0] = txtCodigoCurso.Value.Trim();
            }

            if (txtNomeCurso.Value.Trim() != "")
            {
                arrayFiltroCurso[1] = txtNomeCurso.Value.Trim();
            }

            if (ddlTipoCurso.SelectedValue != "")
            {
                arrayFiltroCurso[2] = ddlTipoCurso.SelectedValue;
            }

            if (optSituacaoSim.Checked)
            {
                arrayFiltroCurso[3] = "ativado";
            }
            else if (optSituacaoNao.Checked)
            {
                arrayFiltroCurso[3] = "inativado";
            }
            else
            {
                arrayFiltroCurso[3] = "todos";
            }

            // 0 = Aguardando aprovação -- 1 = Aprovado-- 2 = Reprovado-- 3 = Sem página
            if (optStatusHomeAprovado.Checked)
            {
                arrayFiltroCurso[4] = "1";
            }
            else if (optStatusHomeAguardando.Checked)
            {
                arrayFiltroCurso[4] = "0";
            }
            else if (optStatusHomeReprovado.Checked)
            {
                arrayFiltroCurso[4] = "2";
            }
            else
            {
                arrayFiltroCurso[4] = "4";
            }

            Session["arrayFiltroCurso"] = arrayFiltroCurso;

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
        //    Cursos_concentracao item = new Cursos_concentracao();
        //    item.id_Curso_concentracao = codigo;
        //    switch (grdResultado.DataKeys[linha].Values[1].ToString())
        //    {
        //        case "Editar":
        //            CursoAplicacao aplicacaoCurso = new CursoAplicacao();
        //            item = aplicacaoCurso.BuscaItem(item);
        //            Session.Add("Cursos_concentracao", item);
        //            Session.Add("sNewCurso", false);
        //            Response.Redirect("cadCursoConcentracaoGestao.aspx", true);
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
                cursos item = new cursos();
                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                item.id_curso = codigo;
                item = aplicacaoCurso.BuscaItem(item);
                Session["cursos"] = item;
                Session["sNewCurso"] = false;
                Response.Redirect("cadCursoGestao.aspx", true);
            }
        }

        protected void btnCriarCurso_Click(object sender, EventArgs e)
        {
            Session["sNewCurso"] = true;
            Session["cursos"] = null;
            Response.Redirect("cadCursoGestao.aspx", true);
        }

        public string set_Sim_Nao(int qTipo)
        {
            string sAux;

            if (qTipo == 1)
            {
                sAux = "<div class='text-primary'><strong>Sim</strong></div>";
            }
            else
            {
                sAux = "<div class='text-danger'><strong>Não</strong></div>";
            }
            return sAux;
        }

        public string set_Status_Homepage(int qStatusAprovado)
        {
            string sAux;

            // 0 = Aguardando aprovação -- 1 = Aprovado-- 2 = Reprovado-- 3 = Sem página
            if (qStatusAprovado == 0)
            {
                sAux = "<div class='text-primary'><strong class='piscante'>Aguardando Aprovação</strong></div>";
            }
            else if (qStatusAprovado == 2)
            {
                sAux = "<div class='text-danger'><strong class='piscante'>Reprovado</strong></div>";
            }
            else if (qStatusAprovado == 1)
            {
                sAux = "<div class='text-success'><strong>Aprovado</strong></div>";
            }
            else 
            {
                sAux = "<div class='text-grey'><strong> - </strong></div>";
            }
            
           return sAux;
        }
    }
}