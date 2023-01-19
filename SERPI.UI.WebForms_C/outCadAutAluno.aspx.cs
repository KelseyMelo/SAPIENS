
using System;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class outCadAutAluno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 78)) // 78. Diversos Cadastro Automático de Alunos - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                if (Session["arrayFiltroCadAutAlunos"] != null)
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
            string[] arrayFiltroCadAutAlunos = new string[2];

            alunos_cadastro_automatico item = new alunos_cadastro_automatico();
            item.descricao = "";
            item.descricao_curso = "";

            arrayFiltroCadAutAlunos = (string[])Session["arrayFiltroCadAutAlunos"];

            if (arrayFiltroCadAutAlunos[0] != "" && arrayFiltroCadAutAlunos[0] != null)
            {
                item.descricao = arrayFiltroCadAutAlunos[0];
                txtDescricao.Value = arrayFiltroCadAutAlunos[0];
            }

            if (arrayFiltroCadAutAlunos[1] != "" && arrayFiltroCadAutAlunos[1] != null)
            {
                item.descricao_curso = arrayFiltroCadAutAlunos[1];
                txtDescricao_Curso.Value = arrayFiltroCadAutAlunos[1];
            }

            //Session["arrayFiltroGrupo"] = arrayFiltroGrupo;
            CadastroAutomaticoAlunoAplicacao aplicacaoCadastro = new CadastroAutomaticoAlunoAplicacao();
            List<alunos_cadastro_automatico> lista = new List<alunos_cadastro_automatico>();
            lista = aplicacaoCadastro.ListaItem(item);
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

        protected void btnPerquisaCadAutAlunosAlunos_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroCadAutAlunos = new string[2];

            if (txtDescricao.Value.Trim() != "")
            {
                arrayFiltroCadAutAlunos[0] = txtDescricao.Value.Trim();
            }

            if (txtDescricao_Curso.Value.Trim() != "")
            {
                arrayFiltroCadAutAlunos[1] = txtDescricao_Curso.Value.Trim();
            }

            Session["arrayFiltroCadAutAlunos"] = arrayFiltroCadAutAlunos;

            CarregarDados();
        }

        public string setTipoCurso(object pItem, string pInfo)
        {
            certificado_tipo_curso item = (certificado_tipo_curso)pItem;

            string sAux;
            sAux = "";
            if (item != null)
            {
                if (item.id_certificado_tipo_curso != 2)
                {
                    sAux = item.descricao;
                }
                else
                {
                    sAux = item.descricao + "<hr>" + pInfo;
                }
            }

            return sAux;
        }

        //public string setParticipantes(object objeto)
        //{
        //    HashSet<alunos_cadastro_automatico_participantes> lista = (HashSet<alunos_cadastro_automatico_participantes>)objeto;
        //    string sAux;
        //    sAux = "";

        //    //if (lista.Count == 0)
        //    //{
        //    //    sAux = lista.Count.ToString();
        //    //}

        //    sAux = lista.Count.ToString();
        //    return sAux;
        //}

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
                alunos_cadastro_automatico item = new alunos_cadastro_automatico();
                CadastroAutomaticoAlunoAplicacao aplicacao = new CadastroAutomaticoAlunoAplicacao();
                item.id_cadastro_automatico = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0]);
                item = aplicacao.BuscaItem(item);
                Session["alunos_cadastro_automatico"] = item;
                Session["sNewalunos_cadastro_automatico"] = false;
                Response.Redirect("outCadAutAlunoGestao.aspx", true);
            }
        }

        protected void btnCriarCadAutAlunosAlunos_Click(object sender, EventArgs e)
        {
            Session["sNewalunos_cadastro_automatico"] = true;
            Session["alunos_cadastro_automatico"] = null;
            Response.Redirect("outCadAutAlunoGestao.aspx", true);
        }
    }
}