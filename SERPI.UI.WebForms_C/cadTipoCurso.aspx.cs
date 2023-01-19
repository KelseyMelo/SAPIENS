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
    public partial class cadTipoCurso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 73)) // Tipo de Curso - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

               
                //var listaPais2 = from item2 in listaPais
                //                 select new
                //                 {
                //                     Id_Pais = item2.Id_Pais,
                //                     Nacionalidade = item2.Nacionalidade
                //                 };


                if (Session["arrayFiltroTipoCurso"] != null)
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
            string[] arrayFiltroTipoCurso = new string[3];

            tipos_curso item = new tipos_curso();

            arrayFiltroTipoCurso = (string[])Session["arrayFiltroTipoCurso"];

            if (arrayFiltroTipoCurso[0] != "" && arrayFiltroTipoCurso[0] != null)
            {
                item.id_tipo_curso = Convert.ToInt32(arrayFiltroTipoCurso[0]);
                txtCodigoTipoCurso.Value = arrayFiltroTipoCurso[0];
            }

            if (arrayFiltroTipoCurso[1] != "" && arrayFiltroTipoCurso[1] != null)
            {
                item.tipo_curso = arrayFiltroTipoCurso[1];
                txtNomeTipoCurso.Value = arrayFiltroTipoCurso[1];
            }

            if (arrayFiltroTipoCurso[2] != "" && arrayFiltroTipoCurso[2] != null)
            {
                item.statusAprovado = Convert.ToInt16(arrayFiltroTipoCurso[2]);
                optStatusHomeTodos.Checked = false;
                optStatusHomeAprovado.Checked = false;
                optStatusHomeAguardando.Checked = false;
                optStatusHomeReprovado.Checked = false;

                // 0 = Aguardando aprovação -- 1 = Aprovado-- 2 = Reprovado-- 3 = Sem página

                if (arrayFiltroTipoCurso[2] == "2")
                {
                    optStatusHomeReprovado.Checked = true;
                }
                else if (arrayFiltroTipoCurso[2] == "1")
                {
                    optStatusHomeAprovado.Checked = true;
                }
                else if (arrayFiltroTipoCurso[2] == "0")
                {
                    optStatusHomeAguardando.Checked = true;
                }
                else
                {
                    optStatusHomeTodos.Checked = true;
                }
            }

            //if (arrayFiltroTipoCurso[2] != "" && arrayFiltroTipoCurso[2] != null)
            //{
            //    item.statusHomepage = Convert.ToInt16(arrayFiltroTipoCurso[2]);
            //    optSituacaoSim.Checked = false;
            //    optSituacaoNao.Checked = false;
            //    optSituacaoTodos.Checked = false;

            //    if (arrayFiltroTipoCurso[2] == "ativado")
            //    {
            //        optSituacaoSim.Checked = true;
            //    }
            //    else if (arrayFiltroTipoCurso[2] == "inativado")
            //    {
            //        optSituacaoNao.Checked = true;
            //    }
            //    else
            //    {
            //        optSituacaoTodos.Checked = true;
            //    }
            //}

            //Session["arrayFiltroTipoCurso"] = arrayFiltroTipoCurso;
            TipoCursoAplicacao aplicacaoTipoCurso = new TipoCursoAplicacao();
            List<tipos_curso> listaTipoCurso = new List<tipos_curso>();
            listaTipoCurso = aplicacaoTipoCurso.ListaItem(item);
            grdResultado.DataSource = listaTipoCurso;
            grdResultado.DataBind();

            if (listaTipoCurso.Count > 0)
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

        protected void btnPerquisaTipoCurso_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroTipoCurso = new string[3];

            if (txtCodigoTipoCurso.Value.Trim() != "")
            {
                arrayFiltroTipoCurso[0] = txtCodigoTipoCurso.Value.Trim();
            }

            if (txtNomeTipoCurso.Value.Trim() != "")
            {
                arrayFiltroTipoCurso[1] = txtNomeTipoCurso.Value.Trim();
            }


            //if (optSituacaoSim.Checked)
            //{
            //    arrayFiltroTipoCurso[2] = "ativado";
            //}
            //else if (optSituacaoNao.Checked)
            //{
            //    arrayFiltroTipoCurso[2] = "inativado";
            //}
            //else
            //{
            //    arrayFiltroTipoCurso[2] = "todos";
            //}

            // 0 = Aguardando aprovação -- 1 = Aprovado-- 2 = Reprovado-- 3 = Sem página
            if (optStatusHomeAprovado.Checked)
            {
                arrayFiltroTipoCurso[2] = "1";
            }
            else if (optStatusHomeAguardando.Checked)
            {
                arrayFiltroTipoCurso[2] = "0";
            }
            else if (optStatusHomeReprovado.Checked)
            {
                arrayFiltroTipoCurso[2] = "2";
            }
            else
            {
                arrayFiltroTipoCurso[2] = "4";
            }

            Session["arrayFiltroTipoCurso"] = arrayFiltroTipoCurso;

            CarregarDados();
        }

        public void grdResultado_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                int linha = Convert.ToInt32(e.CommandArgument);
                int codigo = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0]);
                tipos_curso item = new tipos_curso();
                TipoCursoAplicacao aplicacaoTipoCurso = new TipoCursoAplicacao();
                item.id_tipo_curso = codigo;
                item = aplicacaoTipoCurso.BuscaItem(item);
                Session["tipos_curso"] = item;
                Session["sNewtipos_curso"] = false;
                Response.Redirect("cadTipoCursoGestao.aspx", true);
            }
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

        protected void btnCriarTipoCurso_Click(object sender, EventArgs e)
        {
            Session["sNewtipos_curso"] = true;
            Session["tipos_curso"] = null;
            Response.Redirect("cadTipoCursoGestao.aspx", true);
        }
    }
}