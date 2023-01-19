using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aplicacao_C;
using SERPI.Dominio_C;

namespace SERPI.UI.WebForms_C
{
    public partial class confirmacaoemail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
                professores item = new professores();
                string sChave = HttpContext.Current.Request["chave"];
                item.chave_confirmacao_email = sChave;
                item = aplicacaoProfessor.VerificaConfirmacaoEmail(item);
                if (item != null)
                {
                    lblMensagemEmail.Text = "Olá " + item.nome + ", seu e-mail foi confirmado com sucesso.";
                }
                else
                {
                    lblMensagemEmail.Text = "Infelizmente a chave informada não foi encontrada.<br><br>Por favor, ente em contato com a secretaria do mestrado.";
                }
                
            }
        }
    }
}