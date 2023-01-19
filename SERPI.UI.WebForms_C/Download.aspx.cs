using System;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;


namespace SERPI.UI.WebForms_C
{
    public partial class Download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios) Session["UsuarioLogado"];
            if (usuario==null)
            {
                Response.Redirect("index.html", true);
            }

            if (!Page.IsPostBack)
            {
                try
                {
                    if (Request.Form["skin"] != null)
                    {
                        UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                        usuarios itemUsuario = new usuarios();
                        itemUsuario = (usuarios)Session["UsuarioLogado"];
                        itemUsuario.TemaSistema = Request.Form["skin"];
                        if (aplicacaoUsuario.setusuarioTema(itemUsuario))
                        {
                            Session["UsuarioLogado"] = itemUsuario;
                            Response.Write("[{\"Retorno\":\"ok\"}]");
                        }
                        else
                        {
                            Response.Write("[{\"Retorno\":\"nok\"}]");
                        }
                        Response.Flush();
                        Response.End();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}