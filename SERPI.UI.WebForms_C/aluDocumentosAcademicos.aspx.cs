
using System;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class aluDocumentosAcademicos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (usuario.grupos_acesso.descricao != "Alunos") // Tipo Curso ou Curso - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();

                List<documentos_academicos> lista = aplicacaoGerais.ListaDocumentosAcademicos();
                string sAux = "";
                int i = 0;

                foreach (var item in lista)
                {
                    if (i % 2 == 0)
                    {
                        sAux = sAux + "<br/>";
                    }
                    else
                    {
                        sAux = sAux + "<div class=\"hidden-md hidden-lg\">";
                        sAux = sAux + "     <br/>";
                        sAux = sAux + "</div >";
                    }

                    sAux = sAux + "<div class=\"col-md-6\">";
                    sAux = sAux + "            <div class=\"panel panel-success\">";
                    sAux = sAux + "                <div class=\"panel-heading\" role=\"tab\">";
                    sAux = sAux + "                    <h5 class=\"panel-title\"><a class=\"collapsed a_faq\" id=\"cab_" + i + "\" data-toggle=\"collapse\" href=\"#res_" + i + "\" aria-expanded=\"false\">";
                    if (item.tipo_arquivo.ToUpper() != "PDF")
                    {
                        sAux = sAux + "                        <h6><i class=\"fa fa-file-word-o\" style=\"color: #3588CC\"></i> Documento Word</h6>";
                    }
                    else
                    {
                        sAux = sAux + "                        <h6><i class=\"fa fa-file-pdf-o\" style=\"color: #3588CC\"></i> Documento PDF</h6>";
                    }
                    sAux = sAux + "                        <h5 style = \"line -height: 1.5em\" ><strong>" + item.nome + "</strong></h5>";
                    sAux = sAux + "                        <i id = \"icab_" + i + "\" style = \"margin-top: -25px; color: #3588CC\" class=\"fa fa-chevron-left pull-right rotate\"></i>";
                    sAux = sAux + "                    </a></h5>";
                    sAux = sAux + "                </div>";
                    sAux = sAux + "                <div id = \"res_" + i + "\" class=\"panel-collapse collapse\" role=\"tabpanel\" aria-labelledby=\"cab_" + i + "\" >";
                    sAux = sAux + "                    <div class=\"panel-body\">";
                    sAux = sAux + "                        " + item.descricao.Replace("\n", "<br>");
                    sAux = sAux + "                        <br><br>";
                    sAux = sAux + "                       <a class=\"btn btn-primary\" download target=\"_blank\" href=\"DocumentosAcademicos\\" + item.nome_arquivo + "\" onclick=\"javascript:fAguarde()\"><b>Faça aqui o<em> download</em> do documento</b></a>";
                    sAux = sAux + "                   </div>";
                    sAux = sAux + "                </div>";
                    sAux = sAux + "            </div>";
                    sAux = sAux + "        </div>";
                    i++;
                }

                litDocumentos.Text = Server.HtmlDecode(sAux);

            }
            
        }
        
    }
}