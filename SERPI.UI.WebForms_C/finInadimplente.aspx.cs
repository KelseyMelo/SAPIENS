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
    public partial class finInadimplente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 49)) // Controle de Inadimplentes - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                //InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
                //List<periodo_inscricao> listaPeriodo = aplicacaoInscricao.ListaPeriodoInscricao();
                //var listaPeriodo2 = from elemento in listaPeriodo
                //                    select new
                //                    {
                //                        id_periodo = elemento.id_periodo,
                //                        periodo = elemento.quadrimestre + " de " + String.Format("{0:dd/MM/yyyy}", elemento.data_inicio) + " à " + String.Format("{0:dd/MM/yyyy}", elemento.data_fim)
                //                    };

                //ddlPeriodoBoleto.Items.Clear();
                //ddlPeriodoBoleto.DataSource = listaPeriodo2;
                //ddlPeriodoBoleto.DataValueField = "id_periodo";
                //ddlPeriodoBoleto.DataTextField = "periodo";
                //ddlPeriodoBoleto.DataBind();
                //ddlPeriodoBoleto.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Período", ""));
                //ddlPeriodoBoleto.SelectedValue = "";

            }
            else
            {
                //if (grdResultado.Rows.Count != 0)
                //{

                //    grdResultado.HeaderRow.TableSection = TableRowSection.TableHeader;
                //}
            }
        }

        private void CarregarDados()
        {
            string[] aFiltroInadimplente = new string[2];

            inadimplentes item = new inadimplentes();

            if (aFiltroInadimplente[0] != "" && aFiltroInadimplente[0] != null)
            {
                item.alunos.idaluno = Convert.ToInt32(aFiltroInadimplente[0]);
            }
            if (aFiltroInadimplente[1] != "" && aFiltroInadimplente[1] != null)
            {
                item.alunos.nome = aFiltroInadimplente[0];
            }

        }

        protected void btnPerquisaInadimplente_Click(object sender, EventArgs e)
        {
            string[] aFiltroInadimplente = new string[4];



            Session["aFiltroInadimplente"] = aFiltroInadimplente;

            CarregarDados();
        }

        protected void btnAdicionarAluno_Click(object sender, EventArgs e)
        {
            Session["sNewInadimplente"] = true;
            Session["inadimplentes"] = null;
            Response.Redirect("cadInadimplenteGestao.aspx", true);
        }

    }
}