
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
    public partial class index_tcc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                int qAno;
                //if (Page.Request["qAno"] == null)
                //{
                //    qAno = DateTime.Now.Year;
                //}
                //else
                //{
                //    qAno = Convert.ToInt32(Page.Request["qAno"]);
                //}

                qAno = DateTime.Now.Year;

                var listaAno = aplicacaoAluno.ListaAnosTCC();
                int i = 0;
                string sAux = "";

                foreach (var elemento in listaAno)
                {
                    if (i % 3 == 0 && i != 0)
                    {
                        if (i % 6 == 0)
                        {
                            sAux = sAux + "<br/><br/><br/>";
                        }
                        else
                        {
                            sAux = sAux + "<div class=\"hidden-md hidden-lg\">";
                            sAux = sAux + "     <br/><br/><br/>";
                            sAux = sAux + "</div >";
                        }
                    }

                    if (i == 0)
                    {
                        sAux = sAux + "<div class=\"col-md-2 col-xs-4\">";
                        sAux = sAux + "     <button type = \"button\" class=\"cqBotao btn btn-primary btn-block btn-lg\" onclick=\"fGoToAno(0)\" style=\"cursor:context-menu;\">Todos</button>";
                        sAux = sAux + "</div>";
                        i++;
                    }

                    //if (i == 1)
                    //{
                    //    sAux = sAux + "<div class=\"col-md-2 col-xs-4\">";
                    //    sAux = sAux + "     <button type = \"button\" class=\"cqBotao btn btn-primary btn-block btn-lg\" onclick=\"fGoToAno(" + elemento.ToString() + ")\" style=\"cursor:context-menu;\">" + elemento.ToString() + "</button>";
                    //    sAux = sAux + "</div>";
                    //}
                    //else
                    //{
                    sAux = sAux + "<div class=\"col-md-2 col-xs-4\">";
                    sAux = sAux + "     <button type = \"button\" class=\"cqBotao btn btn-primary btn-block btn-lg btn-outline\" onclick=\"fGoToAno(" + elemento.ToString() + ")\" style=\"cursor:pointer;\">" + elemento.ToString() + "</button>";
                    sAux = sAux + "</div>";
                    //}
                    i++;
                }

                litAno.Text = Server.HtmlDecode(sAux);

            }
        }
    }
}