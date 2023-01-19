using Aplicacao_C;
using SERPI.Dominio_C;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class index_curso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos item = new cursos();
                item.id_curso = Convert.ToInt32(Page.Request["qIdCurso"]);
                item = aplicacaoCurso.BuscaItem(item);

                sectionBanner.Style["background-image"] = "url('img/homepage/cursos/" + item.nome_imagem + "?v=" + item.data_imagem + "')";

                lblNomeCurso.Text = item.nome;
                lblNomeCurso2.Text = item.nome;
                //==
                lblNomeCurso_en.Text = item.nome_en;
                lblNomeCurso2_en.Text = item.nome_en;

                litDescricao.Text = Server.HtmlDecode("<div class =\"cssCurso_pt\">" + item.descricao_homepage + "</div> <div class =\"cssCurso_en\">" + item.descricao_homepage_en + "</div>");

                btnVoltar.Attributes.Add("onclick", "fVolta('" + item.id_tipo_curso + "')");

                lblCorpoDocente.Text = item.corpo_docente;
                lblCorpoDocente_en.Text = item.corpo_docente_en;

                if (item.statusBotao == 1)
                {
                    divBotoes.Visible = true;
                }
                else
                {
                    divBotoes.Visible = false;
                }

                if (item.statusBotao_en == 1)
                {
                    divBotoes_en.Visible = true;
                }
                else
                {
                    divBotoes_en.Visible = false;
                }

                hTipoCursoRunat.Value = item.id_tipo_curso.ToString();

                if (item.id_tipo_curso == 5)
                {
                    ainscricoesDesk.Visible = false;
                    ainscricoesmobile.Visible = false;
                    aFormulario.Visible = true;
                    aFormularioMobile.Visible = true;
                    lblInscricao.Text = " Encontre a solução ideal para você";
                    lblInscricao_en.Text = " Find the right solution for you";
                    aFormulario.HRef = "./index_FormularioEducacaoCorporativa.aspx";
                    aFormularioMobile.HRef = "./index_FormularioEducacaoCorporativa.aspx";
                }
                else
                {
                    ainscricoesDesk.Visible = true;
                    ainscricoesmobile.Visible = true;
                    aFormulario.Visible = false;
                    aFormularioMobile.Visible = false;
                    ainscricoesDesk.HRef = "./homeInscricoes.aspx?curso=" + item.sigla;
                    ainscricoesmobile.HRef = "./homeInscricoes.aspx?curso=" + item.sigla;
                    lblIncrevaseDesktop.InnerText = "Inscreva-se";
                    lblIncrevaseMobile.InnerText = "Inscreva-se";

                    if (item.sigla == "MTAC")
                    {
                        //lblIncrevaseDesktop.InnerText = "Manifestação de interesse";
                        //lblIncrevaseMobile.InnerText = "Manifestação de interesse";
                        //lblInscricao.Text = " Pós IPT - Faça sua manifestação de interesse nesse curso";
                        //lblInscricao_en.Text = " Post IPT - Make your expression of interest for this course";
                        ainscricoesDesk.HRef = "https://ipt.fatvestibulares.com.br";
                        ainscricoesmobile.HRef = "https://ipt.fatvestibulares.com.br";

                        ainscricoesDesk.HRef = "javascript: fAviso('ATENÇÃO','Início previsto para maio de 2023. <br><br>Aguarde abertura das inscrições.');";
                        ainscricoesmobile.HRef = "javascript: fAviso('ATENÇÃO','Início previsto para maio de 2023. <br><br>Aguarde abertura das inscrições.');";
                        ainscricoesDesk.Attributes.Remove("target");
                        ainscricoesmobile.Attributes.Remove("target");
                    }
                    else
                    {
                        lblInscricao.Text = " Pós IPT - Faça sua Inscrição nesse curso";
                        lblInscricao_en.Text = " Post IPT - Register for this course";
                    }
                    
                }

                if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString != "Producao")
                {
                    return;
                }

                string sAux;
                sAux = "<script async src=\"https://www.googletagmanager.com/gtag/js?id=UA-154434342-1\"></script>";
                sAux = sAux + "<script>";
                sAux = sAux + "window.dataLayer = window.dataLayer || [];";
                sAux = sAux + "          function gtag(){ dataLayer.push(arguments); }";
                sAux = sAux + "          gtag('js', new Date());";
                sAux = sAux + "          gtag('config', 'UA-154434342-1', {";
                sAux = sAux + "              'page_title': 'Página Curso:" + item.sigla + " Tipo: " + item.tipos_curso.tipo_curso.Substring(0, 3) + "',";
                sAux = sAux + "    'page_path': '/curso.aspx?Tipo=" + item.tipos_curso.tipo_curso.Substring(0,3) + "&Curso=" + item.sigla + "'";
                sAux = sAux + "          });";
                sAux = sAux + "</script> ";
                litGoogle.Text = Server.HtmlDecode(sAux);

            }
        }
    }
}