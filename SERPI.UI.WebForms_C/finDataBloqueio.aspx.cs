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
    public partial class finDataBloqueio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 63)) // Data de Bloqueio - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
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

                CarregarDados();

            }
        }

        private void CarregarDados()
        {
            ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
            professor_data_recalculo item_data_recalculo;
            item_data_recalculo = aplicacaoProfessor.BuscaDataRecalculo();

            txtDataBloqueio.Value = item_data_recalculo.data_recalculo.ToString("yyyy-MM-dd");
        }

        protected void btnSalvaDataBloqueio_Click(object sender, EventArgs e)
        {
            string sAux ="";
            DateTime temp;
            if (!DateTime.TryParse(txtDataBloqueio.Value.Trim(), out temp))
            {
                sAux = "Digite uma Data de Bloqueio válida. <br/><br/>";
            }

            if (sAux != "")
            {
                lblMensagem.Text = sAux;
                lblTituloMensagem.Text = "Atenção";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                return;
            }

            ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
            professor_data_recalculo item_data_recalculo = new professor_data_recalculo();
            item_data_recalculo.data_recalculo = Convert.ToDateTime(txtDataBloqueio.Value);
            item_data_recalculo = aplicacaoProfessor.AlteraDataRecalculo(item_data_recalculo);

            lblMensagem.Text = "Alteração de Data de Bloqueio realizada com sucesso.";
            lblTituloMensagem.Text = "Alteração de Data de Bloqueio";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

        }
    }
}