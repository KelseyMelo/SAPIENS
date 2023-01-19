using Aplicacao_C;
using SERPI.Dominio_C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C.ProcessoSeletivo
{
    public partial class proPeriodoInscricao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 10)) // Periodos de Inscrição - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                if (Session["aFiltroInscricao"] != null)
                {
                    CarregarDados();
                }

                if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 10)) //Periodos de Inscrição
                {
                    if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 10).FirstOrDefault().escrita != true)
                    {
                        btnCriarPeriodoInscricao.Visible = false;
                    }
                }
            }
            else
            {
                if (grdPeriodoInscricao.Rows.Count != 0)
                {
                    grdPeriodoInscricao.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        private void CarregarDados()
        {
            string[] aFiltroInscricao = new string[3];

            periodo_inscricao item = new periodo_inscricao();

            aFiltroInscricao = (string[])Session["aFiltroInscricao"];

            if (aFiltroInscricao[0] != "" && aFiltroInscricao[0] != null)
            {
                item.quadrimestre = aFiltroInscricao[0];
                txtPeriodoInscricao.Value = aFiltroInscricao[0];
            }

            if (aFiltroInscricao[1] != "" && aFiltroInscricao[1] != null)
            {
                item.data_inicio = Convert.ToDateTime(aFiltroInscricao[1]);
                txtDataInicioPeriodoInscricao.Value = aFiltroInscricao[1];
            }

            if (aFiltroInscricao[2] != "" && aFiltroInscricao[2] != null)
            {
                item.data_fim = Convert.ToDateTime(aFiltroInscricao[2]);
                txtDataFimPeriodoInscricao.Value = aFiltroInscricao[2];
            }

            //Session["aFiltroProfessor"] = aFiltroProfessor;
            InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
            List<periodo_inscricao> lista = new List<periodo_inscricao>();
            lista = aplicacaoInscricao.ListaPeriodoInscricaoAdmin(item);
            grdPeriodoInscricao.DataSource = lista;
            grdPeriodoInscricao.DataBind();

            if (lista.Count > 0)
            {
                grdPeriodoInscricao.UseAccessibleHeader = true;
                grdPeriodoInscricao.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultados.Visible = false;
                grdPeriodoInscricao.Visible = true;
            }
            else
            {
                msgSemResultados.Visible = true;
            }
            divResultados.Visible = true;
        }

        protected void bntPerquisaPeriodoInscricao_Click(object sender, EventArgs e)
        {
            string[] aFiltroInscricao = new string[3];

            if (txtPeriodoInscricao.Value.Trim() != "")
            {
                aFiltroInscricao[0] = txtPeriodoInscricao.Value.Trim();
            }

            if (txtDataInicioPeriodoInscricao.Value.Trim() != "")
            {
                aFiltroInscricao[1] = txtDataInicioPeriodoInscricao.Value.Trim();
            }

            if (txtDataFimPeriodoInscricao.Value.Trim() != "")
            {
                aFiltroInscricao[2] = txtDataFimPeriodoInscricao.Value.Trim();
            }

            Session["aFiltroInscricao"] = aFiltroInscricao;

            CarregarDados();
        }

        public void grdPeriodoInscricao_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                int linha = Convert.ToInt32(e.CommandArgument);
                int codigo = Convert.ToInt32(grdPeriodoInscricao.DataKeys[linha].Values[0]);
                periodo_inscricao item = new periodo_inscricao();
                InscricaoAplicacao aplicacaoPeriodo = new InscricaoAplicacao();
                item.id_periodo = codigo;
                item = aplicacaoPeriodo.BuscaItem_periodo_inscricao(item);
                Session["periodo_inscricao"] = item;
                Session["sNewPeriodoInscricao"] = false;
                Response.Redirect("proPeriodoInscricaoGestao.aspx", true);
            }
        }

        protected void btnCriarPeriodoInscricao_Click(object sender, EventArgs e)
        {
            Session["sNewPeriodoInscricao"] = true;
            Session["periodo_inscricao"] = null;
            Response.Redirect("proPeriodoInscricaoGestao.aspx", true);
        }

    }
}