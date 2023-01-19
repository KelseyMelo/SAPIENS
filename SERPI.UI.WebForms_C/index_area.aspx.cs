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
    public partial class index_area : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                TipoCursoAplicacao aplicacaoTipoCurso = new TipoCursoAplicacao();
                //cursos item = new cursos();
                tipos_curso item_tipo = new tipos_curso();
                item_tipo.id_tipo_curso = Convert.ToInt32(Page.Request["qIdTipo"]);
                //item.id_tipo_curso = Convert.ToInt32(Page.Request["qIdTipo"]);
                //List<cursos> listaTipoCurso = aplicacaoCurso.ListaCursosHomepage(item);


                item_tipo = aplicacaoTipoCurso.BuscaItem(item_tipo);

                string sAux = "";
                if (item_tipo != null)
                {
                    foreach (var elemento in item_tipo.cursos.Where(x=> x.statusHomepage == 1).OrderBy(x => x.sigla).ToList())
                    {
                        if (sAux != "")
                        {
                            sAux = sAux + "<div class=\"hidden-lg hidden-md\">";
                            sAux = sAux + "     <br />";
                            sAux = sAux + "</div>";
                        }

                        sAux = sAux + "<div class=\"col-md-4\">";
                        sAux = sAux + "  <div class=\"thumbnail\" onclick=\"fGoToCurso(" + elemento.id_curso + ")\">";
                        sAux = sAux + "      <br />";
                        sAux = sAux + "      <div class=\"caption text-center\">";
                        sAux = sAux + "          <img style = \"width:280px;height:200px\" class=\"center-block img-rounded img-responsive\" src=\"img/Homepage/cursos/" + elemento.nome_imagem + "?v=" + elemento.data_imagem.ToString() + "\" title=\"" + elemento.nome + "\" alt=\"" + elemento.nome + "\" />";
                        sAux = sAux + "          <br />";
                        sAux = sAux + "          <h4 style = \"color:#016699\"> <span class=\"cssCurso_pt\">" + elemento.nome + "</span><span class=\"cssCurso_en\">" + elemento.nome_en + "</span></h4>";
                        sAux = sAux + "      </div>";
                        sAux = sAux + "      <div class=\"caption card-footer text-center\">";
                        sAux = sAux + "          <ul class=\"list-inline\">";
                        sAux = sAux + "              <li><span class=\"cssCurso_pt\">Saiba mais</span><span class=\"cssCurso_en\">Learn more</span>...</li>";
                        sAux = sAux + "          </ul>";
                        sAux = sAux + "      </div>";
                        sAux = sAux + "  </div>";
                        sAux = sAux + "</div>";
                    }

                    if (sAux == "")
                    {
                        sAux = "<div class=\"text-center \">Aguarde, logo teremos cursos disponíveis para essa modalidade.</div>";
                    }

                    litCursos.Text = Server.HtmlDecode(sAux);
                    
                    sectionBanner.Style["background-image"] = "url('img/homepage/cursos/" + item_tipo.nome_imagem + "?v=" + item_tipo.data_imagem + "')";

                    //sAux = "";
                    //sAux = sAux + "<style>";
                    //sAux = sAux + "     .bannerRosto_interno {";
                    //sAux = sAux + "     background: url('img/homepage/cursos/" + listaTipoCurso.ElementAt(0).tipos_curso.nome_imagem + "?v=" + listaTipoCurso.ElementAt(0).tipos_curso.data_imagem + "');";

                    if (item_tipo.id_tipo_curso == 1)
                    {
                        lblTipoCurso.Text = "MESTRADO PROFISSIONAL";
                    }
                    else
                    {
                        lblTipoCurso.Text = item_tipo.tipo_curso.ToUpper();
                    }
                    lblTipoCurso_en.Text = item_tipo.tipo_curso_en;

                    //sAux = sAux + "     background - repeat: no - repeat;";
                    //sAux = sAux + "     background - position: right - 80px;";
                    //sAux = sAux + "     overflow: hidden;";
                    //sAux = sAux + "     background - size: cover;";
                    //sAux = sAux + "     height: 50vh;";
                    //sAux = sAux + "     background - attachment: fixed;";
                    //sAux = sAux + "     animation - name:img - ani;";
                    //sAux = sAux + "     animation - duration: 1s;";
                    //sAux = sAux + "     animation - timing - function: ease -in;";
                    //sAux = sAux + "</style>";

                    //litBanner.Text = Server.HtmlDecode(sAux);

                    litDescricao.Text = Server.HtmlDecode("<div id=\"idDescricao\">" + item_tipo.descricao_homepage + "</div> <div id=\"idDescricao_en\">" + item_tipo.descricao_homepage_en + "</div>");

                    if (item_tipo.statusBotoes == 1)
                    {
                        divBotoes.Visible = true;
                        lblProcessoSeletivo.Text = item_tipo.processo_seletivo;
                        lblCalendario.Text = item_tipo.calendario;
                        lblProcessoSeletivo_en.Text = item_tipo.processo_seletivo_en;
                        lblCalendario_en.Text = item_tipo.calendario_en;
                    }
                    else
                    {
                        divBotoes.Visible = false;
                    }

                    if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString != "Producao")
                    {
                        return;
                    }

                    sAux = "<script async src=\"https://www.googletagmanager.com/gtag/js?id=UA-154434342-1\"></script>";
                    sAux = sAux + "<script>";
                    sAux = sAux + "window.dataLayer = window.dataLayer || [];";
                    sAux = sAux + "          function gtag(){ dataLayer.push(arguments); }";
                    sAux = sAux + "          gtag('js', new Date());";
                    sAux = sAux + "          gtag('config', 'UA-154434342-1', {";
                    sAux = sAux + "              'page_title': 'Página Tipo Curso:" + item_tipo.tipo_curso + "',";
                    sAux = sAux + "    'page_path': '/area.aspx?Tipo=" + item_tipo.tipo_curso + "'";
                    sAux = sAux + "          });";
                    sAux = sAux + "</script> ";
                    litGoogle.Text = Server.HtmlDecode(sAux);

                }

            }
        }
    }

}