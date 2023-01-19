using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class consultaInscricaoLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnConfirma_Click(object sender, EventArgs e)
        {
            if (txtSenha.Value.Trim() == "iptphorte2@19")
            {
                Session["logado"] = "phorte";
                Response.Redirect("consultaInscricao.aspx", true);
            }
            else if (txtSenha.Value.Trim() == "M@ster1!" || txtSenha.Value.Trim() == "molhof")
            {
                Session["logado"] = "master";
                Response.Redirect("consultaInscricao.aspx", true);
            }
            else
            {
                Session["logado"] = "";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fModalFalhaLogin();", true);
            }
        }
    }
}