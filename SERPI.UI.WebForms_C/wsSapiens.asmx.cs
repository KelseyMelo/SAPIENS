using Aplicacao_C;
using Ionic.Zip;
using SERPI.Dominio_C;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;

namespace SERPI.UI.WebForms_C
{
    /// <summary>
    /// Summary description for wsSapiens
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class wsSapiens : System.Web.Services.WebService
    {
        //==========================================================
        //Geral Início
        //==========================================================
        public class retornoGeral
        {
            public string P0;
            public string P1;
            public string P2;
            public string P3;
            public string P4;
            public string P5;
            public string P6;
            public string P7;
            public string P8;
            public string P9;
            public string P10;
            public string P11;
            public string P12;
            public string P13;
            public string P14;
            public string P15;
            public string P16;
            public string P17;
            public string P18;
            public string P19;
            public string P20;
            public string P21;
            public string P22;
            public string P23;
            public string P24;
            public string P25;
        }

        public class retornoCombo
        {
            public string id;
            public string text;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fSalvaImagem()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();

                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 27))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                HttpPostedFile qArquivo = HttpContext.Current.Request.Files["qArquivo"];

                if (qArquivo.ContentLength > 0)
                {
                    string qNomeArquivo = qArquivo.FileName;
                    int qTamanhoArquivo = qArquivo.ContentLength;
                    byte[] arrayByte = new byte[qTamanhoArquivo];

                    qArquivo.InputStream.Read(arrayByte, 0, qTamanhoArquivo);


                    qArquivo.SaveAs(Server.MapPath("") + "\\Templates\\emails\\imagens\\" + qNomeArquivo);
                }
                
                json = "[{\"P0\":\"ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //================================

        [ScriptMethod()]
        [WebMethod(EnableSession = true)]
        public void fSalvaImagemSummer()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();

                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qTipo = HttpContext.Current.Request.Form["qTipo"];
                string qId = HttpContext.Current.Request.Form["qId"];
                HttpPostedFile qArquivo = HttpContext.Current.Request.Files["file"];
                string sAux = "";

                if (qArquivo == null)
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Sem imagem\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (qArquivo.ContentLength > 0)
                {
                    string qExt = Path.GetExtension(qArquivo.FileName);
                    if (qArquivo.ContentLength > 3145728)
                    {
                        json = "[{\"P0\":\"Erro\",\"P1\":\"A foto deve ter, no máximo, 3 Megabyte\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                    }
                    else if (qExt.ToUpper() != ".JPG" && qExt.ToUpper() != ".JPEG" && qExt.ToUpper() != ".PNG")
                    {
                        json = "[{\"P0\":\"Erro\",\"P1\":\"A foto deve ter a extenção JPG ou JPEG ou PNG\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                    }
                    else
                    {

                        int qTamanhoArquivo = qArquivo.ContentLength;
                        byte[] arrayByte = new byte[qTamanhoArquivo];
                        qArquivo.InputStream.Read(arrayByte, 0, qTamanhoArquivo);

                        qArquivo.SaveAs(Server.MapPath("") + "\\img\\homepage\\" + qTipo + "\\" + qId + "_" + qArquivo.FileName);

                        if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString == "Homolog")
                        {
                            //sAux = "/Sapiens";
                        }


                        json = "[{\"P0\":\"ok\",\"P1\":\"trocar\",\"P2\":\"" + sAux + "/img/homepage/" + qTipo + "/" + qId + "_" + qArquivo.FileName + "?" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now) + "\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                        //}
                        //else
                        //{
                        //    json = "[{\"P0\":\"ok\",\"P1\":\"nao\",\"P2\":\"/img/pessoas/" + qNomeArquivo + "?" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now) + "\",\"P3\":\"\"}]";
                        //    this.Context.Response.ContentType = "application/json; charset=utf-8";
                        //    this.Context.Response.Write(json);
                        //}

                    }

                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Sem imagem\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                }

            }
            catch (Exception ex)
            {

            }
        }
        //==========================================================
        //SERPI.MASTER Início
        //==========================================================

        //SERPI.MASTER
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public string setTema(string Tema)
        {
            Session.Timeout = 60;
            try
            {
                UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                usuarios itemUsuario = new usuarios();
                itemUsuario = (usuarios)Session["UsuarioLogado"];

                if ((string)Session["UsuarioClonado"] == "sim")
                {
                    return "[{\"Retorno\":\"ok\"}]";
                }

                if (itemUsuario != null)
                {
                    //itemUsuario.TemaSistema = Tema;
                    if (Tema != "")
                    {
                        if ((itemUsuario.TemaSistema).IndexOf("sidebar-collapse") == -1)
                        {
                            itemUsuario.TemaSistema = "hold-transition " + Tema + " sidebar-mini";
                        }
                        else
                        {
                            itemUsuario.TemaSistema = "hold-transition " + Tema + " sidebar-mini sidebar-collapse";
                        }
                        
                    }
                    else if (itemUsuario.TemaSistema == null)
                    {
                        itemUsuario.TemaSistema = itemUsuario.TemaSistema + " sidebar-collapse";
                    }
                    else if ((itemUsuario.TemaSistema).IndexOf("sidebar-collapse") == -1)
                    {
                        itemUsuario.TemaSistema = itemUsuario.TemaSistema + " sidebar-collapse";
                    }
                    else
                    {
                        itemUsuario.TemaSistema = itemUsuario.TemaSistema.Replace(" sidebar-collapse", "");
                    }
                    

                    if (aplicacaoUsuario.setusuarioTema(itemUsuario))
                    {
                        Session["UsuarioLogado"] = itemUsuario;
                        return "[{\"Retorno\":\"ok\"}]";
                    }
                    else
                    {
                        return "[{\"Retorno\":\"nok\"}]";
                    }
                }
                else
                {
                    return "[{\"Retorno\":\"nok\"}]";
                }

            }
            catch (Exception)
            {
                //throw new Exception(ex.Message);
                return "[{\"Retorno\":\"nok\"}]";
            }

        }


        //==========================================================
        //proPeriodoInscricaoGestao - Início
        //==========================================================

        //proPeriodoInscricaoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheCursoPeriodoInscricao()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                InscricaoAplicacao aplicacaoPeriodo = new InscricaoAplicacao();
                periodo_inscricao item;
                item = (periodo_inscricao)Session["periodo_inscricao"];
                List<periodo_inscricao> lista = new List<periodo_inscricao>();
                //lista = aplicacaoCurso.ListaDisciplinas(item);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (item.periodo_inscricao_curso.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in item.periodo_inscricao_curso)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_curso.ToString();
                        retorno.P1 = elemento.cursos.sigla;
                        retorno.P2 = elemento.cursos.nome;
                        retorno.P3 = elemento.valor.ToString();
                        retorno.P4 = "<div title=\"Editar Curso\"> <a class=\"btn btn-primary  btn-circle  fa fa-edit\" href=\'javascript:AbreModalEditarCurso(\""
                        + elemento.id_curso.ToString() + "\",\""
                        + elemento.cursos.sigla + "\",\""
                        + elemento.cursos.nome + "\",\""
                        + elemento.valor + "\")\'; ></a></div>";

                       
                        retorno.P5 = "<div title=\"Excluir Curso\"> <a class=\"btn btn-danger  btn-circle  fa fa-eraser\" href=\'javascript:AbreModalApagarCurso(\""
                        + elemento.id_curso.ToString() + "\",\""
                        + elemento.cursos.sigla + "\",\""
                        + elemento.cursos.nome + "\",\""
                        + elemento.valor + "\")\'; ></a></div>";

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //proPeriodoInscricaoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPerquisaCursoDisponivelPeridodoInscricao()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qSigla = HttpContext.Current.Request["qSigla"];
                string sNome = HttpContext.Current.Request["qNome"];

                InscricaoAplicacao aplicacaoPeriodo = new InscricaoAplicacao();
                periodo_inscricao item;
                item = (periodo_inscricao)Session["periodo_inscricao"];
                cursos pItemCurso = new cursos();
                pItemCurso.sigla = qSigla.Trim();
                pItemCurso.nome = sNome.Trim();
                List<cursos> lista = new List<cursos>();
                lista = aplicacaoPeriodo.ListaCursosDisponiveis(item, pItemCurso);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_curso.ToString();
                        retorno.P1 = elemento.sigla;
                        retorno.P2 = elemento.nome;
                        retorno.P3 = ("<div title=\"Incluir Curso\"> <a class=\"btn btn-success  btn-circle  fa fa-plus\" href=\'javascript:AbreModalIncluirCurso(\""
                        + (elemento.id_curso.ToString() + ("\",\""
                        + (elemento.sigla + ("\",\""
                        + (elemento.nome + "\")\'; ></a></div>"))))));
                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //proPeriodoInscricaoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fIncluiCursoPeriodoInscricao()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];
                string qValor = HttpContext.Current.Request["qValor"];

                InscricaoAplicacao aplicacaoPeriodo = new InscricaoAplicacao();
                periodo_inscricao item;
                item = (periodo_inscricao)Session["periodo_inscricao"];

                periodo_inscricao_curso pItem = new periodo_inscricao_curso();

                pItem.id_periodo = item.id_periodo;
                pItem.id_curso = Convert.ToInt32(qId);
                pItem.valor = Convert.ToDecimal(qValor);

                pItem = aplicacaoPeriodo.CriarCursoPeriodoInscricao(pItem);
                if (pItem != null)
                {
                    item = aplicacaoPeriodo.BuscaItem_periodo_inscricao(item);
                    item.periodo_inscricao_curso.Add(pItem);
                    Session["periodo_inscricao"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na inclusão do Curso. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //proPeriodoInscricaoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fAlteraCursoPeriodoInscricao()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];
                string qValor = HttpContext.Current.Request["qValor"];
                string qNome = HttpContext.Current.Request["qNome"];

                InscricaoAplicacao aplicacaoMatricula = new InscricaoAplicacao();
                periodo_inscricao item;
                item = (periodo_inscricao)Session["periodo_inscricao"];

                periodo_inscricao_curso pItem = new periodo_inscricao_curso();

                pItem.id_periodo = item.id_periodo;
                pItem.id_curso = Convert.ToInt32(qId);
                pItem.valor = Convert.ToDecimal(qValor);

                if (aplicacaoMatricula.AlterarCursoPeriodoInscricao(pItem))
                {
                    item = aplicacaoMatricula.BuscaItem_periodo_inscricao(item);
                    Session["periodo_inscricao"] = item;
                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na alteração dos dados do Curso. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //proPeriodoInscricaoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fExcluiCursoPeriodoInscricao()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];
                string qNome = HttpContext.Current.Request["qNome"];

                InscricaoAplicacao aplicacaoPeriodo = new InscricaoAplicacao();
                periodo_inscricao item;
                item = (periodo_inscricao)Session["periodo_inscricao"];

                periodo_inscricao_curso pItem = new periodo_inscricao_curso();

                pItem.id_periodo = item.id_periodo;
                pItem.id_curso = Convert.ToInt32(qId);

                if (aplicacaoPeriodo.ExcluirCursoPeriodoInscricao(pItem))
                {
                    item = aplicacaoPeriodo.BuscaItem_periodo_inscricao(item);
                    Session["periodo_inscricao"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na exclusão do Curso. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //==========================================================
        //proPeriodoInscricaoGestao - Fim
        //==========================================================


        //==========================================================
        //cadAlunoGestao Início
        //==========================================================

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public string DetalheDisciplina(int idOferecimento)
        {
            Session.Timeout = 60;
            try
            {
                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos itemOferecimento = new oferecimentos();
                itemOferecimento.id_oferecimento = idOferecimento;
                itemOferecimento = aplicacaoOferecimento.BuscaItem(itemOferecimento);
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                List<oferecimentos> lista = new List<oferecimentos>();
                lista.Add(itemOferecimento);

                var listaDisciplina = from item in lista
                                      select new
                                      {
                                          NomeDisciplina = item.disciplinas.nome,
                                          CodigoDisciplina = item.disciplinas.codigo,
                                          Quadrimestre = item.quadrimestre,
                                          NumeroOferecimento = item.num_oferecimento,
                                          Objetivo = item.objetivo,
                                          Ementa = item.ementa,
                                          BibliografiaBasica = item.bibliografia_basica,
                                          BibliografiaComplementar = item.bibliografica_compl,
                                          FormaAvaliacao = item.forma_avaliacao,
                                          Observacao = item.observacao
                                      };

                string json = jsSerializer.Serialize(listaDisciplina);
                return json;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public string ListaPresenca(int idOferecimento, int idAluno)
        {
            Session.Timeout = 60;
            try
            {
                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                presenca itemPresenca = new presenca();
                itemPresenca.id_oferecimento = idOferecimento;
                itemPresenca.id_aluno = idAluno;
                List<presenca> lista = new List<presenca>();
                lista = aplicacaoGerais.ListaPresenca(itemPresenca);
                int i = 1;
                lista = lista.OrderBy(x => x.datas_aulas.data_aula).ToList();
                var listaPresenca = from item in lista
                                    select new
                                    {
                                        NomeDisciplina = item.oferecimentos.disciplinas.nome,
                                        CodigoDisciplina = item.oferecimentos.disciplinas.codigo,
                                        NumeroAula = i++,
                                        DataAula = String.Format("{0:dd/MM/yyyy}", item.datas_aulas.data_aula),
                                        HoraInicio = String.Format("{0:HH:mm}", item.datas_aulas.hora_inicio),
                                        HoraFim = String.Format("{0:HH:mm}", item.datas_aulas.hora_fim),
                                        Presente = (item.presente == true) ? "Presente" : "Ausente"
                                    };
                JavaScriptSerializer jsSS = new JavaScriptSerializer();
                string json = jsSS.Serialize(listaPresenca);
                return json;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public string ListaOrientadores(int idOferecimento)
        {
            Session.Timeout = 60;
            try
            {
                Matricula_turma_orientacaoAplicacao aplicacaoMatricula_turma_orientacao = new Matricula_turma_orientacaoAplicacao();
                matricula_turma_orientacao itemMatricula_turma_orientacao = new matricula_turma_orientacao();
                itemMatricula_turma_orientacao.id_orientacao = idOferecimento;
                itemMatricula_turma_orientacao = aplicacaoMatricula_turma_orientacao.BuscaItem(itemMatricula_turma_orientacao);

                ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
                professores professor = new professores();
                List<professores> listaProfessores = new List<professores>();
                professor.status = "cadastrado";
                listaProfessores = aplicacaoProfessor.ListaItem(professor).Where(x => x.id_professor != itemMatricula_turma_orientacao.id_professor).OrderBy(x => x.nome).ToList();

                int i = 1;
                var listaProfessoresOrientacao = from item in listaProfessores
                                                 select new
                                                 {
                                                     IdProfessor = item.id_professor,
                                                     NomeProfessor = item.nome,
                                                     CPFProfessor = item.cpf
                                                 };
                JavaScriptSerializer jsSS = new JavaScriptSerializer();
                string json = jsSS.Serialize(listaProfessoresOrientacao);
                return json;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public string preencheCidade(string sigla)
        {
            Session.Timeout = 60;
            try
            {
                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Estado itemEstado = new Estado();
                itemEstado.Sigla = sigla;
                List<Cidade> listaCidade = aplicacaoGerais.ListaCidade(itemEstado);
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

                var listaCidadeSimples = from item in listaCidade
                                         select new
                                         {
                                             Codigo = item.Nome,
                                             Nome = item.Nome
                                         };

                string json2 = jsSerializer.Serialize(listaCidadeSimples);
                return json2;

                ////Lista de palavras
                //List<string> list = new List<string>();

                ////Criando a lista de palavras
                //for (int i = 1; i <= Convert.ToInt32(n); i++)
                //{
                //    list.Add(sigla + i);
                //}
                ////O JavaScriptSerializer vai fazer o web service retornar JSON
                //JavaScriptSerializer js = new JavaScriptSerializer();

                ////Converte e retorna os dados em JSON
                //string jSon = js.Serialize(list);
                //return jSon;



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void AlteraFoto()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();

                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                HttpPostedFile qArquivo = HttpContext.Current.Request.Files["qArquivo"];
                string qOrigem = HttpContext.Current.Request.Form["qOrigem"];

                if (qArquivo == null)
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Sem imagem\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (qArquivo.ContentLength > 0)
                {
                    string qExt = Path.GetExtension(qArquivo.FileName);
                    if (qOrigem == "2")
                    {
                        qExt = ".JPEG";
                    }

                    if (qArquivo.ContentLength > 1048576)
                    {
                        json = "[{\"P0\":\"Erro\",\"P1\":\"A foto deve ter, no máximo, 1 Megabyte\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                    }
                    else if (qExt.ToUpper() != ".JPG" && qExt.ToUpper() != ".JPEG" && qExt.ToUpper() != ".PNG")
                    {
                        json = "[{\"P0\":\"Erro\",\"P1\":\"A foto deve ter a extenção JPG ou JPEG ou PNG\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                    }
                    else
                    {
                        UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                        usuarios UsuarioAluno = new usuarios();

                        alunos aluno;
                        string qTab = HttpContext.Current.Request.Form["qTab"];
                        aluno = (alunos)Session[qTab + "Aluno"];

                        UsuarioAluno.usuario = aluno.idaluno.ToString();
                        UsuarioAluno = aplicacaoUsuario.BuscaUsuario(UsuarioAluno);

                        string qNomeArquivo = UsuarioAluno.usuario + qExt;

                        int qTamanhoArquivo = qArquivo.ContentLength;
                        byte[] arrayByte = new byte[qTamanhoArquivo];
                        qArquivo.InputStream.Read(arrayByte, 0, qTamanhoArquivo);

                        qArquivo.SaveAs(Server.MapPath("") + "\\img\\pessoas\\" + qNomeArquivo);
                        UsuarioAluno.avatar = qNomeArquivo;

                        aplicacaoUsuario.AlterarUsuario(UsuarioAluno);

                        //Session["UsuarioLogado"] = usuario;


                        //if (usuario.usuario == item.idaluno.ToString())
                        //{

                        json = "[{\"P0\":\"ok\",\"P1\":\"trocar\",\"P2\":\"/img/pessoas/" + qNomeArquivo + "?" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now) + "\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                        //}
                        //else
                        //{
                        //    json = "[{\"P0\":\"ok\",\"P1\":\"nao\",\"P2\":\"/img/pessoas/" + qNomeArquivo + "?" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now) + "\",\"P3\":\"\"}]";
                        //    this.Context.Response.ContentType = "application/json; charset=utf-8";
                        //    this.Context.Response.Write(json);
                        //}

                    }

                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Sem imagem\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                }

                //UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                //usuarios itemUsuario = new usuarios();
                //usuarios UsuarioAlteracao = new usuarios();
                //itemUsuario = (usuarios)Session["UsuarioLogado"];
                //alunos aluno = (alunos)Session["Aluno"];
                //UsuarioAlteracao.usuario = aluno.idaluno.ToString();
                //UsuarioAlteracao = aplicacaoUsuario.BuscaUsuario(UsuarioAlteracao);
                //string sCaminho;

                //if (itemUsuario != null)
                //{
                //    Byte[] imageBytes;
                //    imageBytes = Convert.FromBase64String( iFoto.Replace ("data:image/jpeg;base64,",""));
                //    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                //    ms.Write(imageBytes, 0, imageBytes.Length);
                //    Image Foto = Image.FromStream(ms, true);

                //    sCaminho = (Server.MapPath("img") + ("\\pessoas\\"));

                //    if (Foto != null)
                //    {
                //        if ((System.IO.Directory.Exists((Server.MapPath("img") + ("\\pessoas"))) == false))
                //        {
                //            System.IO.Directory.CreateDirectory((Server.MapPath("img") + ("\\pessoas")));
                //        }

                //        sCaminho = (Server.MapPath("img") + ("\\pessoas\\"));

                //        // Validate the uploaded image(optional)

                //        // Get the complete file path
                //        var fileSavePath = Path.Combine(sCaminho, aluno.idaluno.ToString() + ".jpg");

                //        // Save the uploaded file to "UploadedFiles" folder
                //        Foto.Save(fileSavePath);

                //        UsuarioAlteracao.avatar = aluno.idaluno.ToString() + ".jpg";
                //        aplicacaoUsuario.AlterarUsuario(UsuarioAlteracao);
                //    }


                //    return "[{\"Retorno\":\"ok\"}]";

                //}
                //else
                //{
                //    return "[{\"Retorno\":\"A sessão do aluno está vazia.\"}]";
                //}


            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheDocumentosObrigatorios()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                                
                List<matricula_oferecimento> lista = new List<matricula_oferecimento>();
                alunos item;
                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                List<alunos_arquivos_tipo> lista_alunoArquivoTipo = aplicacaoGerais.ListaAlunoArquivoTipo();

                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                retornoGeral retorno;
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                foreach (var elemento in lista_alunoArquivoTipo.Where(x=> x.id_alunos_arquivos_tipo != 4))
                {
                    retorno = new retornoGeral();

                    retorno.P0 = elemento.id_alunos_arquivos_tipo.ToString();
                    if (elemento.descricao == "Histórico Escolar")
                    {
                        retorno.P1 = elemento.descricao + " (Não Obrigatório)";
                    }
                    else
                    {
                        retorno.P1 = elemento.descricao;
                    }
                    //if ((item.entregou_rg && elemento.id_alunos_arquivos_tipo == 2) || (item.entregou_cpf && elemento.id_alunos_arquivos_tipo == 3) || (item.entregou_historico && elemento.id_alunos_arquivos_tipo == 4) || (item.entregou_diploma && elemento.id_alunos_arquivos_tipo == 5) || (item.entregou_comprovante_end && elemento.id_alunos_arquivos_tipo == 6) || (item.entregou_fotos && elemento.id_alunos_arquivos_tipo == 7) || (item.entregou_certidao && elemento.id_alunos_arquivos_tipo == 8))
                    if ((item.entregou_rg && elemento.id_alunos_arquivos_tipo == 2) || (item.entregou_cpf && elemento.id_alunos_arquivos_tipo == 3) || (item.entregou_diploma && elemento.id_alunos_arquivos_tipo == 5) || (item.entregou_comprovante_end && elemento.id_alunos_arquivos_tipo == 6) || (item.entregou_fotos && elemento.id_alunos_arquivos_tipo == 7) || (item.entregou_certidao && elemento.id_alunos_arquivos_tipo == 8))
                        {
                        retorno.P2 = "Entregou";
                    }
                    else if ((elemento.id_alunos_arquivos_tipo > 1 || elemento.id_alunos_arquivos_tipo < 9) && item.alunos_arquivos.Any(x => x.id_alunos_arquivos_tipo == elemento.id_alunos_arquivos_tipo))
                    {
                        switch (elemento.id_alunos_arquivos_tipo)
                        {
                            case 2:
                                item.entregou_rg = true;
                                break;
                            case 3:
                                item.entregou_cpf = true;
                                break;
                            case 4:
                                item.entregou_historico = true;
                                break;
                            case 5:
                                item.entregou_diploma = true;
                                break;
                            case 6:
                                item.entregou_comprovante_end = true;
                                break;
                            case 7:
                                item.entregou_fotos = true;
                                break;
                            case 8:
                                item.entregou_certidao = true;
                                break;
                        }
                        aplicacaoAluno.TrouxeDocumento(item);
                        retorno.P2 = "Entregou";
                    }
                    else
                    {
                        retorno.P2 = "<span style=\"color:red\">Não Entregou</span>";
                    }
                    if (item.alunos_arquivos.Any(x => x.id_alunos_arquivos_tipo == elemento.id_alunos_arquivos_tipo))
                    {
                        alunos_arquivos itemArquivo = item.alunos_arquivos.Where(x => x.id_alunos_arquivos_tipo == elemento.id_alunos_arquivos_tipo).SingleOrDefault();
                        retorno.P3 = String.Format("{0:dd/MM/yyyy}", itemArquivo.data_alteracao);
                        retorno.P4 = itemArquivo.usuario;
                        retorno.P5 = "<div title=\"download\"><a class=\"btn btn-purple btn-circle fa fa-cloud-download\" download target=\"_blank\" href=\"Arquivo\\" + item.idaluno + "\\" + itemArquivo.nome_arquivo + "\" ></a></div>";
                        retorno.P6 = "<div title=\"Editar\"> <a class=\"btn btn-primary btn-circle fa fa-edit\" href=\'javascript:fEditarDocumentoObrigatorio(\""
                            + itemArquivo.id_alunos_arquivos + "\",\"" + itemArquivo.id_alunos_arquivos_tipo + "\",\"" + itemArquivo.alunos_arquivos_tipo.descricao + "\",\"" + itemArquivo.nome_arquivo + "\")\'; ></a></div>";
                    }
                    else
                    {
                        retorno.P3 = "";
                        retorno.P4 = "";
                        retorno.P5 = "";
                        retorno.P6 = "<div title=\"Editar\"> <a class=\"btn btn-primary btn-circle fa fa-edit\" href=\'javascript:fEditarDocumentoObrigatorio(\"0\",\"" + retorno.P0 + "\",\"" + retorno.P1 + "\",\"\")\'; ></a></div>";
                    }

                    listaRetorno.Add(retorno);
                }

                Session[qTab + "Aluno"] = item;

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheDocumentosNaoObrigatorios()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                List<matricula_oferecimento> lista = new List<matricula_oferecimento>();
                string qTab = HttpContext.Current.Request["qTab"];
                alunos item = (alunos)Session[qTab + "Aluno"];

                retornoGeral retorno;
                List<alunos_arquivos> lista_itemArquivo = item.alunos_arquivos.Where(x => x.id_alunos_arquivos_tipo == 1).ToList();

                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                foreach (var elemento in lista_itemArquivo)
                {
                    retorno = new retornoGeral();

                    retorno.P0 = elemento.id_alunos_arquivos_tipo.ToString();
                    retorno.P1 = elemento.descricao;
                    retorno.P2 = String.Format("{0:dd/MM/yyyy}", elemento.data_alteracao);
                    retorno.P3 = elemento.usuario;
                    retorno.P4 = "<div title=\"download\"><a class=\"btn btn-purple btn-circle fa fa-cloud-download\" download target=\"_blank\" href=\"Arquivo\\" + item.idaluno + "\\" + elemento.nome_arquivo + "\" ></a></div>";
                    retorno.P5 = "<div title=\"Editar\"> <a class=\"btn btn-primary btn-circle fa fa-edit\" href=\'javascript:fEditarDocumentoNaoObrigatorio(\""
                        + elemento.id_alunos_arquivos + "\",\"1\",\"" + elemento.descricao + "\",\"" + elemento.nome_arquivo + "\")\'; ></a></div>";

                    listaRetorno.Add(retorno);
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fSalvarDocumento()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();

                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                HttpPostedFile qArquivo = HttpContext.Current.Request.Files["qArquivo"];
                string qIdDocumento = HttpContext.Current.Request.Form["qIdDocumento"];
                string qIdTipoDocumento = HttpContext.Current.Request.Form["qIdTipoDocumento"];
                string qDescricao = HttpContext.Current.Request.Form["qDescricao"];
                string qOrigem = HttpContext.Current.Request.Form["qOrigem"];
                string qNomeArquivo = HttpContext.Current.Request.Form["qNomeArquivo"];

                alunos aluno;
                string qTab = HttpContext.Current.Request.Form["qTab"];
                aluno = (alunos)Session[qTab + "Aluno"];

                //if (qArquivo == null)
                //{
                //    json = "[{\"P0\":\"Erro\",\"P1\":\"Sem imagem\",\"P2\":\"\",\"P3\":\"\"}]";
                //    this.Context.Response.ContentType = "application/json; charset=utf-8";
                //    this.Context.Response.Write(json);
                //    return;
                //}

                if (qArquivo != null)
                {
                    string qExt = Path.GetExtension(qArquivo.FileName);
                    if (qExt.ToUpper() != ".PDF" && qExt.ToUpper() != ".JPG" && qExt.ToUpper() != ".JPEG" && qExt.ToUpper() != ".PNG")
                    {
                        json = "[{\"P0\":\"Erro\",\"P1\":\"O arquivo deve ter a extenção PDF\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                    }
                    else
                    {
                        if (!System.IO.Directory.Exists(Server.MapPath("") + "\\Arquivo\\" + aluno.idaluno.ToString()))
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath("") + "\\Arquivo\\" + aluno.idaluno.ToString());
                        }

                        int qTamanhoArquivo = qArquivo.ContentLength;
                        byte[] arrayByte = new byte[qTamanhoArquivo];
                        qArquivo.InputStream.Read(arrayByte, 0, qTamanhoArquivo);

                        qArquivo.SaveAs(Server.MapPath("") + "\\Arquivo\\" + aluno.idaluno.ToString() + "\\" + qNomeArquivo);
                    }
                }
                alunos_arquivos item_aluno_arquivo = new alunos_arquivos();
                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();

                if (Convert.ToInt32(qIdTipoDocumento) > 1 && Convert.ToInt32(qIdTipoDocumento) < 9 )
                {
                    switch (Convert.ToInt32(qIdTipoDocumento))
                    {
                        case 2:
                            aluno.entregou_rg = true;
                            break;
                        case 3:
                            aluno.entregou_cpf = true;
                            break;
                        case 4:
                            aluno.entregou_historico = true;
                            break;
                        case 5:
                            aluno.entregou_diploma = true;
                            break;
                        case 6:
                            aluno.entregou_comprovante_end = true;
                            break;
                        case 7:
                            aluno.entregou_fotos = true;
                            break;
                        case 8:
                            aluno.entregou_certidao = true;
                            break;
                    }
                    aplicacaoAluno.TrouxeDocumento(aluno);
                }

                if (qIdDocumento == "0")
                {
                    if (!((Convert.ToInt32(qIdTipoDocumento) > 1 && Convert.ToInt32(qIdTipoDocumento) < 9) && aluno.alunos_arquivos.Any(x => x.id_alunos_arquivos_tipo == Convert.ToInt32(qIdTipoDocumento))))
                    {
                        //Novo
                        item_aluno_arquivo.descricao = qDescricao;
                        item_aluno_arquivo.nome_arquivo = qNomeArquivo;
                        item_aluno_arquivo.data_cadastro = DateTime.Now;
                        item_aluno_arquivo.data_alteracao = DateTime.Now;
                        item_aluno_arquivo.usuario = usuario.usuario;
                        item_aluno_arquivo.tipo_arquivo = "pdf";
                        item_aluno_arquivo.status = "cadastrado";
                        item_aluno_arquivo.ativo = 1;
                        item_aluno_arquivo.idaluno = aluno.idaluno;
                        item_aluno_arquivo.id_alunos_arquivos_tipo = Convert.ToInt32(qIdTipoDocumento);
                        aplicacaoAluno.CriarArquivo(item_aluno_arquivo);
                        aluno.alunos_arquivos.Add(item_aluno_arquivo);
                    }
                }
                else
                {
                    item_aluno_arquivo = aluno.alunos_arquivos.Where(x => x.id_alunos_arquivos == Convert.ToInt32(qIdDocumento)).SingleOrDefault();
                    item_aluno_arquivo.descricao = qDescricao;
                    item_aluno_arquivo.nome_arquivo = qNomeArquivo;
                    item_aluno_arquivo.data_alteracao = DateTime.Now;
                    item_aluno_arquivo.usuario = usuario.usuario;
                    item_aluno_arquivo.tipo_arquivo = "pdf";
                    item_aluno_arquivo.status = "alterado";
                    item_aluno_arquivo.ativo = 1;
                    item_aluno_arquivo.idaluno = aluno.idaluno;
                    item_aluno_arquivo.id_alunos_arquivos_tipo = Convert.ToInt32(qIdTipoDocumento);
                    aplicacaoAluno.AlterarArquivo(item_aluno_arquivo);
                }

                Session[qTab + "Aluno"] = aluno;
                json = "[{\"P0\":\"ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
                return;

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fSalvarContrato()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();

                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                HttpPostedFile qArquivo = HttpContext.Current.Request.Files["qArquivo"];
                string qIdAlunoArquivo = HttpContext.Current.Request.Form["qIdAlunoArquivo"];
                string qIdTurmaAluno = HttpContext.Current.Request.Form["qIdTurmaAluno"];

                alunos item;
                string qTab = HttpContext.Current.Request.Form["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma itemMatricula = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurmaAluno)).SingleOrDefault();
                //if (qArquivo == null)
                //{
                //    json = "[{\"P0\":\"Erro\",\"P1\":\"Sem imagem\",\"P2\":\"\",\"P3\":\"\"}]";
                //    this.Context.Response.ContentType = "application/json; charset=utf-8";
                //    this.Context.Response.Write(json);
                //    return;
                //}

                if (qArquivo != null)
                {
                    string qExt = Path.GetExtension(qArquivo.FileName);
                    if (qExt.ToUpper() != ".PDF" && qExt.ToUpper() != ".JPG" && qExt.ToUpper() != ".JPEG" && qExt.ToUpper() != ".PNG")
                    {
                        json = "[{\"P0\":\"Erro\",\"P1\":\"O arquivo deve ter a extenção PDF ou JPG ou JPEG ou PNG\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                    }
                    else
                    {
                        if (!System.IO.Directory.Exists(Server.MapPath("") + "\\Arquivo\\" + item.idaluno.ToString()))
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath("") + "\\Arquivo\\" + item.idaluno.ToString());
                        }

                        int qTamanhoArquivo = qArquivo.ContentLength;
                        byte[] arrayByte = new byte[qTamanhoArquivo];
                        qArquivo.InputStream.Read(arrayByte, 0, qTamanhoArquivo);

                        qArquivo.SaveAs(Server.MapPath("") + "\\Arquivo\\" + item.idaluno.ToString() + "\\" + qArquivo.FileName);
                    }
                }
                alunos_arquivos item_aluno_arquivo = new alunos_arquivos();
                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();

                if (qIdAlunoArquivo == "")
                {
                    //Novo
                    item_aluno_arquivo.descricao = "Contrato Assinado";
                    item_aluno_arquivo.nome_arquivo = qArquivo.FileName;
                    item_aluno_arquivo.data_cadastro = DateTime.Now;
                    item_aluno_arquivo.data_alteracao = DateTime.Now;
                    item_aluno_arquivo.usuario = usuario.usuario;
                    item_aluno_arquivo.tipo_arquivo = "pdf";
                    item_aluno_arquivo.status = "cadastrado";
                    item_aluno_arquivo.ativo = 1;
                    item_aluno_arquivo.idaluno = item.idaluno;
                    item_aluno_arquivo.id_alunos_arquivos_tipo = 9;
                    item_aluno_arquivo.id_matricula_turma = itemMatricula.id_matricula_turma;
                    item_aluno_arquivo = aplicacaoAluno.CriarArquivo(item_aluno_arquivo);
                    if (!item.alunos_arquivos.Any(x=> x.id_alunos_arquivos_tipo == 9 && x.id_matricula_turma == itemMatricula.id_matricula_turma))
                    {
                        item.alunos_arquivos.Add(item_aluno_arquivo);
                    }
                }
                else
                {
                    item_aluno_arquivo = item.alunos_arquivos.Where(x => x.id_alunos_arquivos == Convert.ToInt32(qIdAlunoArquivo)).SingleOrDefault();
                    item_aluno_arquivo.descricao = "Contrato Assinado";
                    item_aluno_arquivo.nome_arquivo = qArquivo.FileName;
                    item_aluno_arquivo.data_alteracao = DateTime.Now;
                    item_aluno_arquivo.usuario = usuario.usuario;
                    item_aluno_arquivo.tipo_arquivo = "pdf";
                    item_aluno_arquivo.status = "alterado";
                    item_aluno_arquivo.ativo = 1;
                    item_aluno_arquivo.idaluno = item.idaluno;
                    item_aluno_arquivo.id_matricula_turma = itemMatricula.id_matricula_turma;
                    item_aluno_arquivo.id_alunos_arquivos_tipo = 9;
                    aplicacaoAluno.AlterarArquivo(item_aluno_arquivo);
                }

                //item.entregou_contrato = true;
                //aplicacaoAluno.TrouxeDocumento(item);

                List<retornoGeral> listaRetorno = new List<retornoGeral>();
                retornoGeral retorno = new retornoGeral();

                retorno.P0 = "ok";
                retorno.P1 = item_aluno_arquivo.id_alunos_arquivos.ToString();
                retorno.P2 = String.Format("{0:dd/MM/yyyy}", item_aluno_arquivo.data_alteracao);
                retorno.P3 = item_aluno_arquivo.usuario;
                retorno.P4 = "\\Arquivo\\" + item.idaluno + "\\" + item_aluno_arquivo.nome_arquivo;
                listaRetorno.Add(retorno);

                Session[qTab + "Aluno"] = item;
                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
                return;

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }


        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fSalvarDadosArtigo()
        {
            Session.Timeout = 60;
            try
            {
                string json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_grupo == 3 || x.id_grupo == 1))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                HttpPostedFile qArquivo = HttpContext.Current.Request.Files["qArquivo"];
                string qIdAlunoArtigo = HttpContext.Current.Request.Form["qIdAlunoArtigo"];
                string qIdTurmaAluno = HttpContext.Current.Request.Form["qIdTurmaAluno"];
                string qDataArtigo = HttpContext.Current.Request["qDataArtigo"];
                string qNomeArtigo = HttpContext.Current.Request["qNomeArtigo"];
                string qDataAprovacaoArtigo = HttpContext.Current.Request["qDataAprovacaoArtigo"];
                string qOrientadorArtigo = HttpContext.Current.Request["qOrientadorArtigo"];

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request.Form["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma itemMatricula = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurmaAluno)).SingleOrDefault();
                //itemMatricula.id_aluno = item.idaluno;
                //itemMatricula.id_turma = Convert.ToInt32(qIdTurmaAluno);

                if (qDataArtigo == "")
                {
                    itemMatricula.data_artigo = null;
                }
                else
                {
                    itemMatricula.data_artigo = Convert.ToDateTime(HttpContext.Current.Request["qDataArtigo"]);
                }

                if (qDataAprovacaoArtigo == "")
                {
                    itemMatricula.data_aprovacao_artigo = null;
                }
                else
                {
                    itemMatricula.data_aprovacao_artigo = Convert.ToDateTime(HttpContext.Current.Request["qDataAprovacaoArtigo"]);
                }

                itemMatricula.nome_artigo = qNomeArtigo;
                itemMatricula.orientador_artigo = qOrientadorArtigo;

                aplicacaoAluno.AlterarDadosTurmaAluno(itemMatricula);

                item = aplicacaoAluno.BuscaItem(item);

                if (qArquivo != null)
                {
                    string qExt = Path.GetExtension(qArquivo.FileName);
                    if (qExt.ToUpper() != ".PDF" && qExt.ToUpper() != ".JPG" && qExt.ToUpper() != ".JPEG" && qExt.ToUpper() != ".PNG")
                    {
                        json = "[{\"P0\":\"Erro\",\"P1\":\"O arquivo deve ter a extenção PDF ou JPG ou JPEG ou PNG\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                    }
                    else
                    {
                        if (!System.IO.Directory.Exists(Server.MapPath("") + "\\Arquivo\\" + item.idaluno.ToString()))
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath("") + "\\Arquivo\\" + item.idaluno.ToString());
                        }

                        int qTamanhoArquivo = qArquivo.ContentLength;
                        byte[] arrayByte = new byte[qTamanhoArquivo];
                        qArquivo.InputStream.Read(arrayByte, 0, qTamanhoArquivo);

                        qArquivo.SaveAs(Server.MapPath("") + "\\Arquivo\\" + item.idaluno.ToString() + "\\" + qArquivo.FileName);
                    }
                }
                alunos_arquivos item_aluno_arquivo = new alunos_arquivos();

                if (qIdAlunoArtigo == "")
                {
                    //Novo
                    item_aluno_arquivo.descricao = "Artigo";
                    item_aluno_arquivo.nome_arquivo = qArquivo.FileName;
                    item_aluno_arquivo.data_cadastro = DateTime.Now;
                    item_aluno_arquivo.data_alteracao = DateTime.Now;
                    item_aluno_arquivo.usuario = usuario.usuario;
                    item_aluno_arquivo.tipo_arquivo = "pdf";
                    item_aluno_arquivo.status = "cadastrado";
                    item_aluno_arquivo.ativo = 1;
                    item_aluno_arquivo.idaluno = item.idaluno;
                    item_aluno_arquivo.id_alunos_arquivos_tipo = 10;
                    item_aluno_arquivo.id_matricula_turma = itemMatricula.id_matricula_turma;
                    item_aluno_arquivo = aplicacaoAluno.CriarArquivo(item_aluno_arquivo);
                    if (!item.alunos_arquivos.Any(x => x.id_alunos_arquivos_tipo == 10 && x.id_matricula_turma == itemMatricula.id_matricula_turma))
                    {
                        item.alunos_arquivos.Add(item_aluno_arquivo);
                    }
                }
                else
                {
                    item_aluno_arquivo = item.alunos_arquivos.Where(x => x.id_alunos_arquivos == Convert.ToInt32(qIdAlunoArtigo)).SingleOrDefault();
                    item_aluno_arquivo.descricao = "Artigo";
                    if (qArquivo != null)
                    {
                        item_aluno_arquivo.nome_arquivo = qArquivo.FileName;
                    }
                    item_aluno_arquivo.data_alteracao = DateTime.Now;
                    item_aluno_arquivo.usuario = usuario.usuario;
                    item_aluno_arquivo.tipo_arquivo = "pdf";
                    item_aluno_arquivo.status = "alterado";
                    item_aluno_arquivo.ativo = 1;
                    item_aluno_arquivo.idaluno = item.idaluno;
                    item_aluno_arquivo.id_matricula_turma = itemMatricula.id_matricula_turma;
                    item_aluno_arquivo.id_alunos_arquivos_tipo = 10;
                    aplicacaoAluno.AlterarArquivo(item_aluno_arquivo);
                }

                List<retornoGeral> listaRetorno = new List<retornoGeral>();
                retornoGeral retorno = new retornoGeral();

                retorno.P0 = "ok";
                retorno.P1 = item_aluno_arquivo.id_alunos_arquivos.ToString();
                retorno.P2 = String.Format("{0:dd/MM/yyyy}", item_aluno_arquivo.data_alteracao);
                retorno.P3 = item_aluno_arquivo.usuario;
                retorno.P4 = "\\Arquivo\\" + item.idaluno + "\\" + item_aluno_arquivo.nome_arquivo;
                listaRetorno.Add(retorno);

                Session[qTab + "Aluno"] = item;

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
                return;


            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheGrupoTurmaAluno()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                string qTab = HttpContext.Current.Request["qTab"];
                alunos item;
                if ((string)Session["PerfilAluno"] != "sim")
                {
                    item = (alunos)Session[qTab + "Aluno"];
                }
                else
                {
                    item = (alunos)Session["AlunoLogado"];
                }

                List<retornoCombo> listaRetorno = new List<retornoCombo>();

                if (item.matricula_turma.Count > 0)
                {
                    retornoCombo retorno;
                    foreach (var elemento in item.matricula_turma.OrderByDescending(x=> x.id_turma))
                    {
                        retorno = new retornoCombo();
                        retorno.id = elemento.id_turma.ToString();
                        retorno.text = elemento.turmas.cod_turma + " - " + elemento.turmas.cursos.nome;
                        listaRetorno.Add(retorno);
                    }
                }
                else
                {
                    retornoCombo retorno;
                    retorno = new retornoCombo();
                    retorno.id = "Nada";
                    retorno.text = "Nada";
                    listaRetorno.Add(retorno);
                }

                json = jsSS.Serialize(listaRetorno);

                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheTurmaAluno()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                int qIdTurma = Convert.ToInt32(HttpContext.Current.Request["qIdTurma"]);

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;

                string qTab = HttpContext.Current.Request["qTab"];
                if ((string)Session["PerfilAluno"] != "sim")
                {
                    item = (alunos)Session[qTab + "Aluno"];
                }
                else
                {
                    item = (alunos)Session["AlunoLogado"];
                }

                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                matricula_turma item_matricula;

                item_matricula = item.matricula_turma.Where(x => x.id_turma == qIdTurma).SingleOrDefault();

                int qTipoCurso;
                qTipoCurso = item_matricula.turmas.cursos.id_tipo_curso;

                retornoGeral retorno;
                retorno = new retornoGeral();

                retorno.P0 = item_matricula.turmas.cod_turma;
                retorno.P1 = item_matricula.turmas.quadrimestre;
                retorno.P2 = item_matricula.turmas.cursos.tipos_curso.tipo_curso;
                retorno.P3 = String.Format("{0:dd/MM/yyyy}", item_matricula.turmas.data_inicio);
                retorno.P4 = String.Format("{0:dd/MM/yyyy}", item_matricula.turmas.data_fim);

                var listaHistorico = item_matricula.historico_matricula_turma.Where(x => x.situacao.ToUpper() == "TRANCADO" || x.situacao.ToUpper() == "PRORROGAÇÃO CPG").ToList();
                int iDias = 0;
                foreach (var elemento in listaHistorico)
                {
                    if (elemento.data_inicio.HasValue && elemento.data_fim.HasValue)
                    {
                        iDias = iDias + elemento.data_fim.Value.Subtract(elemento.data_inicio.Value).Days;
                    }
                }

                retorno.P5 = String.Format("{0:dd/MM/yyyy}", item_matricula.turmas.data_fim.Value.AddDays(iDias));
                retorno.P6= item_matricula.turmas.cursos.sigla + " - " + item_matricula.turmas.cursos.nome;
                retorno.P7 = (item_matricula.areas_concentracao == null) ? "" : item_matricula.areas_concentracao.nome;

                string dataSituacao;
                if (item_matricula.historico_matricula_turma.Count > 0)
                {
                    dataSituacao = item_matricula.historico_matricula_turma.OrderByDescending(x => x.data_inicio).ThenByDescending(x => x.ordem).FirstOrDefault().data_fim == null ? " - " : item_matricula.historico_matricula_turma.OrderByDescending(x => x.data_inicio).ThenByDescending(x => x.ordem).FirstOrDefault().data_fim.ToString().Substring(0, 10);
                    retorno.P8 = item_matricula.historico_matricula_turma.OrderByDescending(x => x.data_inicio).ThenByDescending(x => x.ordem).FirstOrDefault().situacao + " (" + dataSituacao + ")";
                }
                else
                {
                    //retorno.P8 = "Sem registro na historico_matricula_turma";
                    retorno.P8 = "Sem Situação";
                }

                retorno.P9 = (item_matricula.data_artigo == null) ? "YYYY-MM-DD" : String.Format("{0:yyyy-MM-dd}", item_matricula.data_artigo);
                //== Novos campos do Artigo
                retorno.P21 = item_matricula.nome_artigo;
                retorno.P22 = (item_matricula.data_aprovacao_artigo == null) ? "YYYY-MM-DD" : String.Format("{0:yyyy-MM-dd}", item_matricula.data_aprovacao_artigo);
                retorno.P23 = item_matricula.orientador_artigo;
                //==================

                retorno.P10 = qTipoCurso.ToString();

                retorno.P11 = "";
                retorno.P12 = "";
                retorno.P13 = "";
                retorno.P14 = "";
                retorno.P15 = "";
                retorno.P16 = "";
                retorno.P17 = "";
                retorno.P18 = "";
                retorno.P19 = "";
                retorno.P20 = "";

                //Contrato
                alunos_arquivos item_arquvo_aluno = item_matricula.alunos_arquivos.Where(x => x.idaluno == item.idaluno  && x.id_alunos_arquivos_tipo == 9).SingleOrDefault();
                if (item_arquvo_aluno != null)
                {
                    retorno.P11 = item_arquvo_aluno.nome_arquivo;
                    retorno.P12 = String.Format("{0:dd/MM/yyyy}", item_arquvo_aluno.data_alteracao);
                    retorno.P13 = item_arquvo_aluno.usuario;
                    retorno.P14 = item_arquvo_aluno.id_alunos_arquivos.ToString();
                    retorno.P15 = "Arquivo\\" + item.idaluno + "\\" + item_arquvo_aluno.nome_arquivo;
                }

                item_arquvo_aluno = item_matricula.alunos_arquivos.Where(x => x.idaluno == item.idaluno && x.id_alunos_arquivos_tipo == 10).SingleOrDefault();
                if (item_arquvo_aluno != null)
                {
                    retorno.P16 = item_arquvo_aluno.nome_arquivo;
                    retorno.P17 = String.Format("{0:dd/MM/yyyy}", item_arquvo_aluno.data_alteracao);
                    retorno.P18 = item_arquvo_aluno.usuario;
                    retorno.P19 = item_arquvo_aluno.id_alunos_arquivos.ToString();
                    retorno.P20 = "Arquivo\\" + item.idaluno + "\\" + item_arquvo_aluno.nome_arquivo;
                }


                listaRetorno.Add(retorno);

                json = jsSS.Serialize(listaRetorno);

                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheHistoricoMatricula()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdTurma = HttpContext.Current.Request["qIdTurma"];

                List<matricula_turma> lista = new List<matricula_turma>();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                lista = item.matricula_turma.Where(x=> x.id_turma == Convert.ToInt32(qIdTurma)).ToList() ;
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    int i = 0;
                    retornoGeral retorno;
                    foreach (var elemento in lista.ToList())
                    {
                        foreach (var elemento2 in elemento.historico_matricula_turma.OrderBy(x=> x.data_inicio).ThenBy(x => x.ordem).ThenBy(x => x.data_fim))
                        {
                            i++;
                            retorno = new retornoGeral();
                            if (elemento.historico_matricula_turma.Any(x => x.situacao == "Titulado"))
                            {
                                retorno.P7 = "Titulado";
                                if (elemento.turmas.cursos.id_tipo_curso != 4) //Curso de Curta Duração = Ignorar pois não tem Banca
                                {
                                    if (elemento.banca.Count > 0)
                                    {
                                        retorno.P8 = elemento.banca.Where(x => x.tipo_banca == "Defesa").FirstOrDefault().portaria_mec;
                                        retorno.P9 = String.Format("{0:dd/MM/yyyy}", elemento.banca.Where(x => x.tipo_banca == "Defesa").FirstOrDefault().data_portaria_mec);
                                        retorno.P10 = String.Format("{0:dd/MM/yyyy}", elemento.banca.Where(x => x.tipo_banca == "Defesa").FirstOrDefault().data_diario_oficial);
                                    }
                                }
                                retorno.P12 = elemento.turmas.cursos.id_tipo_curso.ToString();
                            }
                            else if (elemento.turmas.cursos.id_tipo_curso == 2 || elemento.turmas.cursos.id_tipo_curso == 3 || elemento.turmas.cursos.id_tipo_curso == 4) //A pedido da Andrea liberar Botão Certificado para Especialização e Curta Duração e MBA Internacional
                            {
                                retorno.P7 = "Titulado";
                                retorno.P12 = elemento.turmas.cursos.id_tipo_curso.ToString();
                            }

                            retorno.P0 = i.ToString();
                            retorno.P1 = elemento2.status;
                            retorno.P2 = elemento2.situacao;
                            retorno.P3 = String.Format("{0:dd/MM/yyyy}", elemento2.data_inicio);
                            retorno.P4 = String.Format("{0:dd/MM/yyyy}", elemento2.data_fim);

                            if (elemento2.situacao != "Prorrogação CPG" && elemento2.situacao != "Trancamento Especial")
                            {
                                retorno.P5 = "<div title=\"Editar\"> <a class=\"btn btn-primary btn-circle fa fa-edit\" href=\'javascript:fModalEditarSituacao(\""
                                + elemento2.id_historico + "\",\"" + elemento2.situacao + "\",\"" + String.Format("{0:yyyy-MM-dd}", elemento2.data_inicio) + "\",\"" + String.Format("{0:yyyy-MM-dd}", elemento2.data_fim) + "\")\'; ></a></div>";

                                if (elemento2.situacao != "Matriculado")
                                {
                                    retorno.P6 = "<div title=\"Apagar\"> <a class=\"btn btn-danger btn-circle fa fa-eraser\" href=\'javascript:fModalApagarSituacao(\""
                                    + elemento2.id_historico + "\",\"" + elemento2.situacao + "\",\"" + String.Format("{0:yyyy-MM-dd}", elemento2.data_inicio) + "\",\"" + String.Format("{0:yyyy-MM-dd}", elemento2.data_fim) + "\")\'; ></a></div>";
                                }
                                else
                                {
                                    retorno.P6 = "<div title=\"Cuidado ao Apagar um registro 'Matriculado'\"> <a class=\"btn btn-vinho btn-circle fa fa-eraser\" href=\'javascript:fModalApagarSituacao(\""
                                    + elemento2.id_historico + "\",\"" + elemento2.situacao + "\",\"" + String.Format("{0:yyyy-MM-dd}", elemento2.data_inicio) + "\",\"" + String.Format("{0:yyyy-MM-dd}", elemento2.data_fim) + "\")\'; ></a></div>";
                                    //retorno.P6 = "<div title=\"Não é permitido apagar o registro Matriculado\"> <i class=\"fa fa-ban text-danger\"></i></div>";
                                }
                            }
                            else
                            {
                                retorno.P5 = "<div title=\"Não é permitido editar esse registro\"> <i class=\"fa fa-ban fa-2x text-muted\"></i></div>";
                                retorno.P6 = "<div title=\"Não é permitido apagar esse registro\"> <i class=\"fa fa-ban fa-2x text-muted\"></i></div>";
                            }

                            if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 19).FirstOrDefault().modificacao == true)
                                //Se não tiver direito de modificação, então avisa ao ajax solicitante
                            {
                                retorno.P11 = "1";
                            }
                            else
                            {
                                retorno.P11 = "0";
                            }

                            listaRetorno.Add(retorno);
                        }
                        
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheHistoricoAluno()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdTurma = HttpContext.Current.Request["qIdTurma"];

                List<matricula_oferecimento> lista = new List<matricula_oferecimento>();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];

                if ((string)Session["PerfilAluno"] != "sim")
                {
                    item = (alunos)Session[qTab + "Aluno"];
                }
                else
                {
                    item = (alunos)Session["AlunoLogado"];
                }

                lista = item.matricula_oferecimento.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).ToList();
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = String.Format("{0:dd/MM/yyyy}", elemento.oferecimentos.datas_aulas.Min(x => x.data_aula));
                        retorno.P1 = elemento.oferecimentos.quadrimestre;
                        retorno.P2 = elemento.oferecimentos.disciplinas.codigo;
                        retorno.P3 = elemento.oferecimentos.disciplinas.nome;
                        retorno.P4 = elemento.oferecimentos.carga_horaria.ToString() + " h";
                        retorno.P5 = (elemento.oferecimentos.presenca.Where(x => x.id_aluno == item.idaluno).Count() == 0) ? "0,00%" : ((elemento.oferecimentos.presenca.Where(x => x.id_aluno == item.idaluno && x.presente == true).Count()) / (elemento.oferecimentos.presenca.Where(x => x.id_aluno == item.idaluno).Count() * 0.01)).ToString("0.##") + "%";
                        retorno.P6 = (elemento.oferecimentos.notas.Where(x => x.id_aluno == item.idaluno).FirstOrDefault() == null) ? "" : (elemento.oferecimentos.notas.Where(x => x.id_aluno == item.idaluno).Select(x => x.conceito).FirstOrDefault() == null) ? "" : elemento.oferecimentos.notas.Where(x => x.id_aluno == item.idaluno).Select(x => x.conceito).FirstOrDefault();
                        retorno.P7 = (elemento.oferecimentos.notas.Where(x => x.id_aluno == item.idaluno).FirstOrDefault() == null) ? "" : (elemento.oferecimentos.notas.Where(x => x.id_aluno == item.idaluno).FirstOrDefault().conceitos_de_aprovacao == null) ? "" : elemento.oferecimentos.notas.Where(x => x.id_aluno == item.idaluno).FirstOrDefault().conceitos_de_aprovacao.descricao;
                        retorno.P8 = "<div title=\"Visualizar detalhes da Disciplina\"> <a class=\"btn btn-default btn-circle fa fa-search-plus text-blue\" href=\'javascript:fDetalheDisciplina(\""
                        + elemento.oferecimentos.id_oferecimento + "\")\'; ></a></div>";
                        retorno.P9 = "<div title=\"Visualizar lista de presença na Disciplina\"> <a class=\"btn btn-default btn-circle fa fa-calendar-check-o text-blue\" href=\'javascript:fDetalhePresenca(\""
                        + elemento.oferecimentos.id_oferecimento + "\",\""
                        + item.idaluno + "\")\'; ></a></div>";

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fInserirSituacao()
        {
            Session.Timeout = 60;
            try
            {
                string json = "";
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!(usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_grupo == 3 || x.id_grupo == 1)))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdTurma = HttpContext.Current.Request["qIdTurma"];
                string qStatus = HttpContext.Current.Request["qStatus"];
                string qSituacao = HttpContext.Current.Request["qSituacao"];
                string qDataInicio = HttpContext.Current.Request["qDataInicio"];
                string qDataFim = HttpContext.Current.Request["qDataFim"];

                if (qSituacao != "Desligado" && qSituacao != "Abandonou" && qSituacao != "Qualificação" && qSituacao != "Titulado" && qDataInicio == "")
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Deve-se digitar uma Data Início <br><br>\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                historico_matricula_turma itemHistoricoMatricula = new historico_matricula_turma();
                itemHistoricoMatricula.id_matricula_turma = item.matricula_turma.Where(x=> x.id_turma == Convert.ToInt32(qIdTurma)).FirstOrDefault().id_matricula_turma;
                if (qSituacao == "Desligado" || qSituacao == "Abandonou" || qSituacao == "Qualificação" || qSituacao == "Titulado" || qDataInicio == "")
                {
                    itemHistoricoMatricula.data_inicio = Convert.ToDateTime(HttpContext.Current.Request["qDataFim"]);
                    if (qSituacao == "Titulado")
                    {
                        itemHistoricoMatricula.ordem = 3;
                    }
                    else if (qSituacao == "Matriculado")
                    {
                        itemHistoricoMatricula.ordem = 1;
                    }
                    else
                    {
                        itemHistoricoMatricula.ordem = 2;
                    }
                }
                else
                {
                    itemHistoricoMatricula.data_inicio = Convert.ToDateTime(HttpContext.Current.Request["qDataInicio"]);
                    itemHistoricoMatricula.ordem = 1;
                }

                itemHistoricoMatricula.data_fim = Convert.ToDateTime(HttpContext.Current.Request["qDataFim"]);
                //itemHistoricoMatricula.data_previsao_termino = Convert.ToDateTime(HttpContext.Current.Request["qDataFim"]);
                itemHistoricoMatricula.status = qStatus;
                itemHistoricoMatricula.situacao = qSituacao;
                itemHistoricoMatricula.data = DateTime.Now;
                itemHistoricoMatricula.usuario = usuario.usuario;

                aplicacaoAluno.CriarSituacaoHistorico(itemHistoricoMatricula);

                item = aplicacaoAluno.BuscaItem(item);

                Session[qTab + "Aluno"] = item;

                json = "[{\"P0\":\"ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fEditarSituacao()
        {
            Session.Timeout = 60;
            try
            {
                string json = "";
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 19).FirstOrDefault().modificacao == true)
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdHistorico = HttpContext.Current.Request["qIdHistorico"];
                string qStatus = HttpContext.Current.Request["qStatus"];
                string qSituacao = HttpContext.Current.Request["qSituacao"];
                string qDataInicio = HttpContext.Current.Request["qDataInicio"];
                string qDataFim = HttpContext.Current.Request["qDataFim"];

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                historico_matricula_turma itemHistoricoMatricula = new historico_matricula_turma();
                itemHistoricoMatricula.id_historico = Convert.ToInt32(qIdHistorico);
                if (qSituacao == "Desligado" || qSituacao == "Abandonou" || qSituacao == "Qualificação" || qSituacao == "Titulado" || qDataInicio == "")
                {
                    itemHistoricoMatricula.data_inicio = Convert.ToDateTime(HttpContext.Current.Request["qDataFim"]);
                }
                else
                {
                    itemHistoricoMatricula.data_inicio = Convert.ToDateTime(HttpContext.Current.Request["qDataInicio"]);
                }

                itemHistoricoMatricula.data_fim = Convert.ToDateTime(HttpContext.Current.Request["qDataFim"]);
                //itemHistoricoMatricula.data_previsao_termino = Convert.ToDateTime(HttpContext.Current.Request["qDataFim"]);
                itemHistoricoMatricula.status = qStatus;
                //itemHistoricoMatricula.situacao = qSituacao;
                itemHistoricoMatricula.data = DateTime.Now;
                itemHistoricoMatricula.usuario = usuario.usuario;

                aplicacaoAluno.EditarSituacaoHistorico(itemHistoricoMatricula);

                item = aplicacaoAluno.BuscaItem(item);

                Session[qTab + "Aluno"] = item;

                json = "[{\"P0\":\"ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fApagarSituacao()
        {
            Session.Timeout = 60;
            try
            {
                string json = "";
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!(usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_grupo == 3 || x.id_grupo == 1)))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdHistorico = HttpContext.Current.Request["qIdHistorico"];

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                historico_matricula_turma itemHistoricoMatricula = new historico_matricula_turma();
                itemHistoricoMatricula.id_historico = Convert.ToInt32(qIdHistorico);
                
                aplicacaoAluno.ApagarSituacaoHistorico(itemHistoricoMatricula);

                item = aplicacaoAluno.BuscaItem(item);

                Session[qTab + "Aluno"] = item;

                json = "[{\"P0\":\"ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheOrietacaoAluno()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdTurma = HttpContext.Current.Request["qIdTurma"];

                List<matricula_turma> lista = new List<matricula_turma>();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                lista = item.matricula_turma.Where(x=> x.id_turma == Convert.ToInt32(qIdTurma)).ToList() ;
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    int i = 0;
                    retornoGeral retorno;
                    string sCPF ="";
                    string sNome ="";
                    string sTitulo ="";
                    foreach (var elemento in lista.ToList())
                    {
                        if (elemento.matricula_turma_orientacao.Count == 1)
                        {
                            foreach (var elemento2 in elemento.matricula_turma_orientacao.OrderByDescending(x => x.tipo_orientacao))
                            {
                                retorno = new retornoGeral();
                                retorno.P0 = elemento2.professores.cpf;
                                retorno.P1 = elemento2.professores.nome;
                                retorno.P2 = "<div title=\"Excluir Co-orientador\"> <a class=\"btn btn-danger btn-circle fa fa-eraser\" href=\'javascript:fModalExcluirCoorientador(\""
                                + elemento2.id_professor + "\",\"" + elemento2.professores.nome + "\",\"" + qIdTurma + "\")\'; ></a></div>";
                                    //}
                                retorno.P3 = elemento2.professores.cpf;
                                retorno.P4 = elemento2.professores.nome;
                                retorno.P5 = elemento2.tipo_orientacao;
                                retorno.P6 = elemento2.titulo;
                                retorno.P7 = "nao";
                                retorno.P8 = elemento2.id_professor.ToString();

                                //====================
                                if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 30).FirstOrDefault().modificacao == true)
                                //Se não tiver direito avida ao ajax
                                {
                                    retorno.P9 = "1";
                                }
                                else
                                {
                                    retorno.P9 = "0";
                                }

                                listaRetorno.Add(retorno);
                                i++;
                            }
                        }
                        else if (elemento.matricula_turma_orientacao.Count > 1)
                        {
                            foreach (var elemento2 in elemento.matricula_turma_orientacao.OrderByDescending(x => x.tipo_orientacao))
                            {
                                if (i == 0)
                                {
                                    sCPF = elemento2.professores.cpf;
                                    sNome = elemento2.professores.nome;
                                    sTitulo = elemento2.titulo;
                                }
                                else
                                {
                                    retorno = new retornoGeral();
                                    retorno.P0 = elemento2.professores.cpf;
                                    retorno.P1 = elemento2.professores.nome;
                                    retorno.P2 = "<div title=\"Excluir Co-orientador\"> <a class=\"btn btn-danger btn-circle fa fa-eraser\" href=\'javascript:fModalExcluirCoorientador(\""
                                + elemento2.id_professor + "\",\"" + elemento2.professores.nome + "\",\"" + qIdTurma + "\")\'; ></a></div>";
                                    //}
                                    retorno.P3 = sCPF;
                                    retorno.P4 = sNome;
                                    retorno.P5 = elemento2.tipo_orientacao;
                                    retorno.P6 = sTitulo;
                                    retorno.P7 = "sim";
                                    retorno.P8 = elemento2.id_professor.ToString();

                                    //====================
                                    if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 30).FirstOrDefault().modificacao == true)
                                    //Se não tiver direito avida ao ajax
                                    {
                                        retorno.P9 = "1";
                                    }
                                    else
                                    {
                                        retorno.P9 = "0";
                                    }

                                    listaRetorno.Add(retorno);
                                }

                                i++;
                            }
                        }

                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPerquisaOrientadorDisponivel()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdTurma = HttpContext.Current.Request["qIdTurma"];
                string sCPF = HttpContext.Current.Request["qCPF"];
                string sNome = HttpContext.Current.Request["qNome"];

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                matricula_turma_orientacao item = new matricula_turma_orientacao();
                professores pItemProfessor = new professores();
                alunos itemAluno;
                string qTab = HttpContext.Current.Request["qTab"];
                itemAluno = (alunos)Session[qTab + "Aluno"];

                item.id_matricula_turma = itemAluno.matricula_turma.Where(x=> x.id_turma == Convert.ToInt32(qIdTurma)).FirstOrDefault().id_matricula_turma;

                pItemProfessor.cpf = sCPF;
                pItemProfessor.nome = sNome;

                List<professores> lista = new List<professores>();
                lista = aplicacaoAluno.ListOrientadoresDisponiveis(item, pItemProfessor);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_professor.ToString();
                        retorno.P1 = elemento.nome;
                        retorno.P2 = elemento.cpf;
                        retorno.P3 = ("<div title=\"Incluir/Alterar Orientador\"> <a class=\"btn btn-success  btn-circle  fa fa-plus\" href=\'javascript:fIncluiAlteraOrientadorAluno(\""
                        + (elemento.id_professor.ToString() + ("\",\""
                        + (elemento.cpf + ("\",\""
                        + (elemento.nome + "\")\'; ></a></div>"))))));

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPerquisaCoorientadorDisponivel()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdTurma = HttpContext.Current.Request["qIdTurma"];
                string sCPF = HttpContext.Current.Request["qCPF"];
                string sNome = HttpContext.Current.Request["qNome"];

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                matricula_turma_orientacao item = new matricula_turma_orientacao();
                professores pItemProfessor = new professores();
                alunos itemAluno;
                string qTab = HttpContext.Current.Request["qTab"];
                itemAluno = (alunos)Session[qTab + "Aluno"];

                item.id_matricula_turma = itemAluno.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).FirstOrDefault().id_matricula_turma;

                pItemProfessor.cpf = sCPF;
                pItemProfessor.nome = sNome;

                List<professores> lista = new List<professores>();
                lista = aplicacaoAluno.ListOrientadoresDisponiveis(item, pItemProfessor);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_professor.ToString();
                        retorno.P1 = elemento.nome;
                        retorno.P2 = elemento.cpf;
                        retorno.P3 = ("<div title=\"Adicionar Co-orientador\"> <a class=\"btn btn-success  btn-circle  fa fa-plus\" href=\'javascript:fIncluiCoorientadorAluno(\""
                        + (elemento.id_professor.ToString() + ("\",\""
                        + (elemento.cpf + ("\",\""
                        + (elemento.nome + "\")\'; ></a></div>"))))));

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fIncluiAlteraOrientadorAluno()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];
                string qIdOrientadorAnterior = HttpContext.Current.Request["qIdOrientadorAnterior"];
                string qIdTurma = HttpContext.Current.Request["qIdTurma"];

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma_orientacao pItem = new matricula_turma_orientacao();

                pItem.id_matricula_turma = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).FirstOrDefault().id_matricula_turma;
                pItem.id_professor = Convert.ToInt32(qId);
                pItem.tipo_orientacao = "Orientador";
                pItem.data_cadastro = DateTime.Now;
                pItem.data_alteracao = pItem.data_cadastro;
                pItem.usuario = usuario.usuario;
                if (aplicacaoAluno.IncluirAlterarOrientador(pItem, qIdOrientadorAnterior))
                {
                    item = aplicacaoAluno.BuscaItem(item);
                    Session[qTab + "Aluno"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na inclusão/alteração do Orientador. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fIncluiCoorientadorAluno()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];
                string qIdOrientadorAnterior = "";
                string qIdTurma = HttpContext.Current.Request["qIdTurma"];

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma_orientacao pItem = new matricula_turma_orientacao();

                pItem.id_matricula_turma = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).FirstOrDefault().id_matricula_turma;
                pItem.id_professor = Convert.ToInt32(qId);
                pItem.tipo_orientacao = "Co-orientador";
                pItem.titulo = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).FirstOrDefault().matricula_turma_orientacao.FirstOrDefault().titulo;
                pItem.data_cadastro = DateTime.Now;
                pItem.data_alteracao = pItem.data_cadastro;
                pItem.usuario = usuario.usuario;
                if (aplicacaoAluno.IncluirAlterarOrientador(pItem, qIdOrientadorAnterior))
                {
                    item = aplicacaoAluno.BuscaItem(item);
                    Session[qTab + "Aluno"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na inclusão/alteração do Orientador. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fSalvarDadosOrientacao()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qTitulo = HttpContext.Current.Request["qTitulo"];
                string qIdTurma = HttpContext.Current.Request["qIdTurma"];

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma_orientacao pItem = new matricula_turma_orientacao();

                pItem.id_matricula_turma = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).FirstOrDefault().id_matricula_turma;
                pItem.titulo = qTitulo;
                pItem.data_cadastro = DateTime.Now;
                pItem.data_alteracao = pItem.data_cadastro;
                pItem.usuario = usuario.usuario;
                if (aplicacaoAluno.AlterarTituloOrientacao(pItem))
                {
                    item = aplicacaoAluno.BuscaItem(item);
                    Session[qTab + "Aluno"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na inclusão/alteração do Orientador. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fApagarDadosOrientacao()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qTitulo = HttpContext.Current.Request["qTitulo"];
                string qIdTurma = HttpContext.Current.Request["qIdTurma"];

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma_orientacao pItem = new matricula_turma_orientacao();

                pItem.id_matricula_turma = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).FirstOrDefault().id_matricula_turma;
                pItem.titulo = qTitulo;
                pItem.data_cadastro = DateTime.Now;
                pItem.data_alteracao = pItem.data_cadastro;
                pItem.usuario = usuario.usuario;
                if (aplicacaoAluno.ApagarDadosOrientacao(pItem))
                {
                    item = aplicacaoAluno.BuscaItem(item);
                    Session[qTab + "Aluno"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na inclusão/alteração do Orientador. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fExcluirCoorientador()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdCoorientador = HttpContext.Current.Request["qIdCoorientador"];
                string qIdTurma = HttpContext.Current.Request["qIdTurma"];

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma_orientacao pItem = new matricula_turma_orientacao();

                pItem.id_matricula_turma = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).FirstOrDefault().id_matricula_turma;
                pItem.id_professor = Convert.ToInt32(qIdCoorientador);

                if (aplicacaoAluno.ApagarCoorientador(pItem))
                {
                    item = aplicacaoAluno.BuscaItem(item);
                    Session[qTab + "Aluno"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na exclusão do Co-orientador. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheBancaQualificacaoAlunoDetalhes()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                int qIdTurma = Convert.ToInt32(HttpContext.Current.Request["qIdTurma"]);

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                matricula_turma item_matricula;
                retornoGeral retorno = new retornoGeral();

                item_matricula = item.matricula_turma.Where(x => x.id_turma == qIdTurma).SingleOrDefault();

                List<banca> listaBanca = new List<banca>();
                List<banca_professores> listaBancaProfessores = new List<banca_professores>();

                listaBanca = item_matricula.banca.Where(x => x.tipo_banca == "Qualificação").ToList();

                List<matricula_turma_orientacao> listaOrientacao = new List<matricula_turma_orientacao>();

                List<matricula_turma_orientacao> listaCo_Orientacao = new List<matricula_turma_orientacao>();

                listaOrientacao = item_matricula.matricula_turma_orientacao.Where(x => x.tipo_orientacao == "Orientador").ToList();

                listaCo_Orientacao = item_matricula.matricula_turma_orientacao.Where(x => x.tipo_orientacao == "Co-orientador").OrderBy(x=> x.professores.nome).ToList();


                if (listaBanca.Count != 0)
                {
                    retorno = new retornoGeral();

                    retorno.P0 = listaBanca.ElementAt(0).id_banca.ToString();
                    retorno.P1 = String.Format("{0:yyyy-MM-dd}", listaBanca.ElementAt(0).horario); 
                    retorno.P2 = String.Format("{0:HH:mm}", listaBanca.ElementAt(0).horario); 
                    retorno.P3 = listaBanca.ElementAt(0).resultado;
                    retorno.P4 = listaBanca.ElementAt(0).titulo;
                    retorno.P5 = listaBanca.ElementAt(0).observacao;

                    if (listaBanca.ElementAt(0).banca_professores.Any(x => x.tipo_professor == "Orientador"))
                    {
                        listaBancaProfessores = listaBanca.ElementAt(0).banca_professores.Where(x => x.tipo_professor == "Orientador").ToList();

                        retorno.P6 = listaBancaProfessores.ElementAt(0).professores.id_professor.ToString();
                        retorno.P7 = listaBancaProfessores.ElementAt(0).professores.cpf;
                        retorno.P8 = listaBancaProfessores.ElementAt(0).professores.nome;
                    }
                    
                    retorno.P9 = "existe";
                    retorno.P10 = "";
                    retorno.P11 = String.Format("{0:dd/MM/yyyy}", listaBanca.ElementAt(0).data_cadastro);
                    retorno.P12 = String.Format("{0:dd/MM/yyyy}", listaBanca.ElementAt(0).data_alteracao);
                    retorno.P13 = listaBanca.ElementAt(0).usuario;
                    retorno.P14 = listaBanca.ElementAt(0).remoto.ToString();
                }
                else if(listaOrientacao.Count !=0)
                {
                    retorno = new retornoGeral();

                    retorno.P0 = "";
                    retorno.P1 = "";
                    retorno.P2 = "";
                    retorno.P3 = "";
                    retorno.P4 = listaOrientacao.ElementAt(0).titulo;
                    retorno.P5 = "";
                    retorno.P6 = listaOrientacao.ElementAt(0).professores.id_professor.ToString();
                    retorno.P7 = listaOrientacao.ElementAt(0).professores.cpf;
                    retorno.P8 = listaOrientacao.ElementAt(0).professores.nome;
                    retorno.P9 = "novo";
                    retorno.P10 = "";
                    foreach (var elemento in listaCo_Orientacao)
                    {
                        if (retorno.P10 != "")
                        {
                            retorno.P10 = retorno.P10 + "<br>";
                        }
                        retorno.P10 = retorno.P10 + elemento.professores.nome;
                    }
                    retorno.P11 = "";
                    retorno.P12 = "";
                    retorno.P13 = "";
                    retorno.P14 = "";
                }
                else
                {
                    retorno = new retornoGeral();

                    retorno.P0 = "";
                    retorno.P1 = "";
                    retorno.P2 = "";
                    retorno.P3 = "";
                    retorno.P4 = "";
                    retorno.P5 = "";
                    retorno.P6 = "";
                    retorno.P7 = "";
                    retorno.P8 = "";
                    retorno.P9 = "sem_Orientação";
                    retorno.P10 = "";
                    retorno.P11 = "";
                    retorno.P12 = "";
                    retorno.P13 = "";
                    retorno.P14 = "";
                }
                
                listaRetorno.Add(retorno);

                json = jsSS.Serialize(listaRetorno);

                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheBancaDefesaAluno()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                int qIdTurma = Convert.ToInt32(HttpContext.Current.Request["qIdTurma"]);

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                string qTab = HttpContext.Current.Request["qTab"];
                alunos item = (alunos)Session[qTab + "Aluno"];

                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                matricula_turma item_matricula;
                retornoGeral retorno = new retornoGeral();

                item_matricula = item.matricula_turma.Where(x => x.id_turma == qIdTurma).SingleOrDefault();

                List<banca> listaBanca = new List<banca>();
                List<banca> listaBancaQualificacao = new List<banca>();
                List<banca_professores> listaBancaProfessores = new List<banca_professores>();

                //===============
                List<matricula_turma_orientacao> listaOrientacao = new List<matricula_turma_orientacao>();

                List<matricula_turma_orientacao> listaCo_Orientacao = new List<matricula_turma_orientacao>();

                listaOrientacao = item_matricula.matricula_turma_orientacao.Where(x => x.tipo_orientacao == "Orientador").ToList();

                listaCo_Orientacao = item_matricula.matricula_turma_orientacao.Where(x => x.tipo_orientacao == "Co-orientador").OrderBy(x => x.professores.nome).ToList();

                //===============

                listaBanca = item_matricula.banca.Where(x => x.tipo_banca == "Defesa").ToList();

                listaBancaQualificacao = item_matricula.banca.Where(x => x.tipo_banca == "Qualificação").ToList();


                if (listaBanca.Count != 0)
                {
                    retorno = new retornoGeral();

                    retorno.P0 = listaBanca.ElementAt(0).id_banca.ToString();
                    retorno.P1 = String.Format("{0:yyyy-MM-dd}", listaBanca.ElementAt(0).horario);
                    retorno.P2 = String.Format("{0:HH:mm}", listaBanca.ElementAt(0).horario);
                    retorno.P3 = listaBanca.ElementAt(0).resultado;
                    retorno.P4 = listaBanca.ElementAt(0).titulo;
                    retorno.P5 = listaBanca.ElementAt(0).observacao;

                    if (listaBanca.ElementAt(0).banca_professores.Any(x => x.tipo_professor == "Orientador"))
                    {
                        listaBancaProfessores = listaBanca.ElementAt(0).banca_professores.Where(x => x.tipo_professor == "Orientador").ToList();

                        retorno.P6 = listaBancaProfessores.ElementAt(0).professores.id_professor.ToString();
                        retorno.P7 = listaBancaProfessores.ElementAt(0).professores.cpf;
                        retorno.P8 = listaBancaProfessores.ElementAt(0).professores.nome;
                    }

                    retorno.P9 = "existe";
                    retorno.P10 = "";
                    retorno.P11 = "";
                    retorno.P12 = String.Format("{0:yyyy-MM-dd}", listaBanca.ElementAt(0).data_entrega_trabalho);
                    retorno.P13 = listaBanca.ElementAt(0).portaria_mec;
                    retorno.P14 = String.Format("{0:yyyy-MM-dd}", listaBanca.ElementAt(0).data_portaria_mec);
                    retorno.P15 = String.Format("{0:yyyy-MM-dd}", listaBanca.ElementAt(0).data_diario_oficial);
                    retorno.P16 = String.Format("{0:dd/MM/yyyy}", listaBanca.ElementAt(0).data_cadastro);
                    retorno.P17 = String.Format("{0:dd/MM/yyyy}", listaBanca.ElementAt(0).data_alteracao);
                    retorno.P18 = listaBanca.ElementAt(0).usuario;
                    retorno.P19 = listaBanca.ElementAt(0).remoto.ToString();

                }
                else if (listaBancaQualificacao.Count != 0)
                {
                    retorno = new retornoGeral();

                    retorno.P0 = "";
                    retorno.P1 = "";
                    retorno.P2 = "";
                    retorno.P3 = "";
                    retorno.P4 = listaBancaQualificacao.ElementAt(0).titulo;
                    retorno.P5 = "";

                    listaBancaProfessores = listaBancaQualificacao.ElementAt(0).banca_professores.Where(x => x.tipo_professor == "Orientador").ToList();

                    retorno.P6 = listaBancaProfessores.ElementAt(0).professores.id_professor.ToString();
                    retorno.P7 = listaBancaProfessores.ElementAt(0).professores.cpf;
                    retorno.P8 = listaBancaProfessores.ElementAt(0).professores.nome;
                    retorno.P9 = "novo";
                    retorno.P10 = "";
                    listaBancaProfessores = listaBancaQualificacao.ElementAt(0).banca_professores.Where(x => x.tipo_professor == "Co-orientador").ToList();
                    foreach (var elemento in listaBancaProfessores)
                    {
                        if (retorno.P10 != "")
                        {
                            retorno.P10 = retorno.P10 + "<br>";
                        }
                        retorno.P10 = retorno.P10 + elemento.professores.nome;
                    }

                    retorno.P11 = "";
                    listaBancaProfessores = listaBancaQualificacao.ElementAt(0).banca_professores.Where(x => x.tipo_professor == "Membro").ToList();
                    foreach (var elemento in listaBancaProfessores)
                    {
                        if (retorno.P11 != "")
                        {
                            retorno.P11 = retorno.P11 + "<br>";
                        }
                        retorno.P11 = retorno.P11 + elemento.professores.nome;
                    }

                    listaBancaProfessores = listaBancaQualificacao.ElementAt(0).banca_professores.Where(x => x.tipo_professor == "Membro Suplente").ToList();
                    foreach (var elemento in listaBancaProfessores)
                    {
                        if (retorno.P11 != "")
                        {
                            retorno.P11 = retorno.P11 + "<br>";
                        }
                        retorno.P11 = retorno.P11 + elemento.professores.nome + " <strong>Suplente</strong>";
                    }

                    retorno.P12 = "";
                    retorno.P13 = "";
                    retorno.P14 = "";
                    retorno.P15 = "";
                    retorno.P16 = "";
                    retorno.P17 = "";
                    retorno.P18 = "";
                    retorno.P19 = "";

                }
                else if (listaOrientacao.Count != 0 && item_matricula.turmas.cursos.id_tipo_curso == 3)
                {
                    //Só para tipo de curso Especialização, pois esse tipo de curso NÃO tem Qualificação, então "pega" o nome do orientador direto da tabela de Orientação
                    retorno = new retornoGeral();

                    retorno.P0 = "";
                    retorno.P1 = "";
                    retorno.P2 = "";
                    retorno.P3 = "";
                    retorno.P4 = listaOrientacao.ElementAt(0).titulo;
                    retorno.P5 = "";
                    retorno.P6 = listaOrientacao.ElementAt(0).professores.id_professor.ToString();
                    retorno.P7 = listaOrientacao.ElementAt(0).professores.cpf;
                    retorno.P8 = listaOrientacao.ElementAt(0).professores.nome;
                    retorno.P9 = "novo";
                    retorno.P10 = "";
                    foreach (var elemento in listaCo_Orientacao)
                    {
                        if (retorno.P10 != "")
                        {
                            retorno.P10 = retorno.P10 + "<br>";
                        }
                        retorno.P10 = retorno.P10 + elemento.professores.nome;
                    }
                    retorno.P11 = "";
                    retorno.P12 = "";
                    retorno.P13 = "";
                    retorno.P14 = "";
                }
                else
                {
                    retorno = new retornoGeral();

                    retorno.P0 = "";
                    retorno.P1 = "";
                    retorno.P2 = "";
                    retorno.P3 = "";
                    retorno.P4 = "";
                    retorno.P5 = "";
                    retorno.P6 = "";
                    retorno.P7 = "";
                    retorno.P8 = "";
                    retorno.P9 = "sem_qualificação";
                    retorno.P10 = "";
                    retorno.P11 = "";
                    retorno.P12 = "";
                    retorno.P13 = "";
                    retorno.P14 = "";
                    retorno.P15 = "";
                    retorno.P16 = "";
                    retorno.P17 = "";
                    retorno.P18 = "";
                    retorno.P19 = "";
                }

                listaRetorno.Add(retorno);

                json = jsSS.Serialize(listaRetorno);

                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheDissertacaoBancaDefesaAluno()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                int qIdTurma = Convert.ToInt32(HttpContext.Current.Request["qIdTurma"]);

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                matricula_turma item_matricula;
                retornoGeral retorno = new retornoGeral();

                item_matricula = item.matricula_turma.Where(x => x.id_turma == qIdTurma).SingleOrDefault();

                List<banca> listaBancaQualificacao = new List<banca>();
                List<banca_professores> listaBancaProfessores = new List<banca_professores>();

                banca itemBanca = item_matricula.banca.Where(x => x.tipo_banca == "Defesa").SingleOrDefault();

                //listaBancaQualificacao = item_matricula.banca.Where(x => x.tipo_banca == "Qualificação").ToList();


                if (itemBanca != null)
                {
                    if (usuario.grupos_acesso.id_grupo != 8 && usuario.grupos_acesso.id_grupo != 1) //1= TI e 8=Gerencia
                    {
                        retorno.P14 = "n_Gerente";
                    }
                    else
                    {
                        retorno.P14 = "Gerente";
                    }

                    if (itemBanca.banca_dissertacao.Count >0)
                    {
                        //retorno = new retornoGeral();

                        retorno.P0 = "nNovo";
                        //====== log de quem alterou
                        retorno.P1 = String.Format("{0:dd/MM/yyy}", itemBanca.banca_dissertacao.ElementAt(0).data_cadastro);
                        retorno.P2 = String.Format("{0:dd/MM/yyy}", itemBanca.banca_dissertacao.ElementAt(0).data_alteracao);
                        retorno.P3 = itemBanca.banca_dissertacao.ElementAt(0).usuario;

                        //====== Visita e download são fixos
                        retorno.P4 = itemBanca.banca_dissertacao.ElementAt(0).visitas.ToString();
                        retorno.P5 = itemBanca.banca_dissertacao.ElementAt(0).downloads.ToString();

                        //===== Publicado
                        retorno.P6 = itemBanca.banca_dissertacao.ElementAt(0).palavras_chave;
                        retorno.P7 = itemBanca.banca_dissertacao.ElementAt(0).resumo;
                        retorno.P8 = itemBanca.banca_dissertacao.ElementAt(0).arquivo;
                        retorno.P9 = itemBanca.banca_dissertacao.ElementAt(0).cod_ipt;

                        //======
                        retorno.P10 = itemBanca.banca_dissertacao.ElementAt(0).palavras_chavePreview;
                        retorno.P11 = itemBanca.banca_dissertacao.ElementAt(0).cod_iptPreview;
                        retorno.P12 = itemBanca.banca_dissertacao.ElementAt(0).resumoPreview;
                        retorno.P13 = itemBanca.banca_dissertacao.ElementAt(0).arquivoPreview;
                        //======
                        retorno.P15 = itemBanca.banca_dissertacao.ElementAt(0).statusAprovacao.ToString();
                        
                        //====== Igual para Previwe e Publicado
                        retorno.P16 = item_matricula.turmas.cursos.nome;
                        retorno.P17 = itemBanca.titulo;
                        List<string> names = item.nome.Split(' ').ToList();
                        retorno.P18 = names.ElementAt(names.Count - 1).ToUpper();
                        names.RemoveAt(names.Count - 1);
                        retorno.P18 = retorno.P18 + ", " + string.Join(" ", names.ToArray());
                        names = itemBanca.banca_professores.Where(x => x.tipo_professor == "Orientador").SingleOrDefault().professores.nome.Split(' ').ToList();
                        retorno.P19 = names.ElementAt(names.Count - 1).ToUpper();
                        names.RemoveAt(names.Count - 1);
                        retorno.P19 = retorno.P19 + ", " + string.Join(" ", names.ToArray());
                        retorno.P20 = itemBanca.horario.Value.Year.ToString();
                        retorno.P21 = String.Format("{0:dd/MM/yyy}", itemBanca.banca_dissertacao.ElementAt(0).data_aprovacao);
                        retorno.P22 = itemBanca.banca_dissertacao.ElementAt(0).usuarioAprovacao;
                    }
                }
                
                listaRetorno.Add(retorno);

                json = jsSS.Serialize(listaRetorno);

                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheHistoricoDissertacao()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                int qIdTurma = Convert.ToInt32(HttpContext.Current.Request["qIdTurma"]);

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                matricula_turma item_matricula;
                retornoGeral retorno = new retornoGeral();

                item_matricula = item.matricula_turma.Where(x => x.id_turma == qIdTurma).SingleOrDefault();

                List<banca> listaBanca = new List<banca>();
                List<banca> listaBancaQualificacao = new List<banca>();
                List<banca_professores> listaBancaProfessores = new List<banca_professores>();

                banca itemBanca = item_matricula.banca.Where(x => x.tipo_banca == "Defesa").SingleOrDefault();

                //listaBancaQualificacao = item_matricula.banca.Where(x => x.tipo_banca == "Qualificação").ToList();
                if (itemBanca.banca_dissertacao.Count > 0)
                {
                    foreach (var elemento in itemBanca.banca_dissertacao.ElementAt(0).banca_dissertacao_obs)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = String.Format("{0:dd/MM/yyy}", elemento.dataObs);
                        retorno.P1 = elemento.observacao;
                        retorno.P2 = elemento.usuario;
                        listaRetorno.Add(retorno);
                    }
                } 
                
                json = jsSS.Serialize(listaRetorno);

                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fEnviarAprovacaoDissertacao()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                int qIdTurma = Convert.ToInt32(HttpContext.Current.Request["qIdTurma"]);
                string qObs = HttpContext.Current.Request["qObs"];

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                matricula_turma item_matricula;
                retornoGeral retorno = new retornoGeral();

                item_matricula = item.matricula_turma.Where(x => x.id_turma == qIdTurma).SingleOrDefault();

                string sTipoDissertacao;
                if (item_matricula.turmas.cursos.id_tipo_curso == 3)
                {
                    sTipoDissertacao = "Monografia";
                }
                else
                {
                    sTipoDissertacao = "Dissertação";
                }

                List<banca> listaBanca = new List<banca>();
                List<banca> listaBancaQualificacao = new List<banca>();
                List<banca_professores> listaBancaProfessores = new List<banca_professores>();

                listaBanca = item_matricula.banca.Where(x => x.tipo_banca == "Defesa").ToList();

                banca item_banca = item_matricula.banca.Where(x => x.tipo_banca == "Defesa").SingleOrDefault();

                //listaBancaQualificacao = item_matricula.banca.Where(x => x.tipo_banca == "Qualificação").ToList();

                banca_dissertacao item_dissertacao = item_banca.banca_dissertacao.ElementAt(0);
                item_dissertacao.data_alteracao = DateTime.Now;
                item_dissertacao.usuario = usuario.usuario;
                item_dissertacao.statusAprovacao = 0; //Aguardando aprovação
                item_dissertacao = aplicacaoAluno.AlteraStatusDissertacao(item_dissertacao);


                banca_dissertacao_obs itemObs = new banca_dissertacao_obs();
                itemObs.dataObs = DateTime.Now;
                itemObs.observacao = qObs;
                itemObs.id_banca_dissertacao = item_dissertacao.id_banca_dissertacao;
                itemObs.usuario = usuario.usuario;

                item_dissertacao.banca_dissertacao_obs.Add(aplicacaoAluno.CriarItem_Obs(itemObs));
                item_banca.banca_dissertacao.ElementAt(0).banca_dissertacao_obs.Add(itemObs);

                item = aplicacaoAluno.BuscaItem(item);
                //item.matricula_turma.Where(x => x.id_turma == qIdTurma).SingleOrDefault().banca.Where(x => x.tipo_banca == "Defesa").SingleOrDefault().banca_dissertacao.ElementAt(0).banca_dissertacao_obs.Add(itemObs);
                Session[qTab + "Aluno"] = item;

                //====================

                string sAux;

                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Configuracoes item_configuracoes;
                // 1 = email mestrado@ipt.br
                // 2 = email suporte@ipt.br
                item_configuracoes = aplicacaoGerais.BuscaConfiguracoes(1);

                string sFrom = item_configuracoes.remetente_email;
                string sFrom_Nome = item_configuracoes.nome_remetente_email;
                string sTo;
                string sAssunto = "Inclusão/Alteração de " + sTipoDissertacao + " do Aluno: " + item_dissertacao.banca.matricula_turma.alunos.nome;

                UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                usuarios usuario_remetente = new usuarios();
                usuario_remetente.usuario = item.usuario;
                usuario_remetente = aplicacaoUsuario.BuscaUsuario(usuario_remetente);

                if (item_matricula.turmas.cursos.id_tipo_curso == 3)
                {
                    sAux = sTipoDissertacao + " Incluído/Alterado<br><br>";
                }
                else
                {
                    sAux = sTipoDissertacao + " Incluída/Alterada<br><br>";
                }
                sAux = sAux + "Título: <strong>" + item_dissertacao.banca.titulo + "</strong><br>";
                sAux = sAux + "Palavras-chave: <strong>" + item_dissertacao.palavras_chavePreview + "</strong><br>";
                sAux = sAux + "Cod IPT: <strong>" + item_dissertacao.cod_iptPreview + "</strong><br>";
                sAux = sAux + "Resumo: <strong>" + item_dissertacao.resumoPreview + "</strong><br>";
                sAux = sAux + "Nome do Documento: <strong>" + item_dissertacao.arquivoPreview + "</strong><br>";
                sAux = sAux + "Responsável: <strong>" + usuario.nomeSocial.Trim() + "</strong><br>";
                sAux = sAux + "Obs: <strong>" + qObs + "</strong><br>";

                //É do grupo coordenador então pegar todos os cursos em que ele é coordenador
                List<usuarios> lista_usuario;
                usuarios item_usuario = new usuarios();
                item_usuario.grupos_acesso = new grupos_acesso();
                item_usuario.grupos_acesso.id_grupo = 8; //Gerência
                lista_usuario = aplicacaoUsuario.ListaUsuario_porGrupoAcesso(item_usuario);
                foreach (var elemento in lista_usuario)
                {

                    if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString == "Producao")
                    {
                        sTo = elemento.email;
                    }
                    else
                    {
                        sTo = "kelsey@ipt.br"; // usuario.email;
                        sAux = sAux + "<br><br> <strong>Esse email seria enviado para:</strong>" + elemento.email;
                    }

                    //sTo = "kelsey@ipt.br";

                    Utilizades.fEnviaEmail(sFrom, sFrom_Nome, sTo, "", "", sAssunto, sAux, item_configuracoes.servidor_email, item_configuracoes.conta_email, item_configuracoes.senha_email, item_configuracoes.porta_email.Value, 1, "");

                    //(this.Master as SERPI).PreencheSininho();

                }

                Session[qTab + "Aluno"] = item;
                //(this.Master as SERPI).PreencheSininho();

                json = "[{\"P0\":\"ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";

                //json = jsSS.Serialize(listaRetorno);

                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fAprovarDissertacao()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                int qIdTurma = Convert.ToInt32(HttpContext.Current.Request["qIdTurma"]);

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                matricula_turma item_matricula;
                retornoGeral retorno = new retornoGeral();

                item_matricula = item.matricula_turma.Where(x => x.id_turma == qIdTurma).SingleOrDefault();

                string sTipoDissertacao;
                if (item_matricula.turmas.cursos.id_tipo_curso == 3)
                {
                    sTipoDissertacao = "Monografia";
                }
                else
                {
                    sTipoDissertacao = "Dissertação";
                }

                List<banca> listaBanca = new List<banca>();
                List<banca> listaBancaQualificacao = new List<banca>();
                List<banca_professores> listaBancaProfessores = new List<banca_professores>();

                listaBanca = item_matricula.banca.Where(x => x.tipo_banca == "Defesa").ToList();

                banca item_banca = item_matricula.banca.Where(x => x.tipo_banca == "Defesa").SingleOrDefault();

                //listaBancaQualificacao = item_matricula.banca.Where(x => x.tipo_banca == "Qualificação").ToList();

                banca_dissertacao item_dissertacao = item_banca.banca_dissertacao.ElementAt(0);
                item_dissertacao.usuario = usuario.usuario;
                item_dissertacao.statusAprovacao = 1; //Aprovado
                bool bEnviarEmailDarci;

                //Em conversa com a Longuinho, definiu-se que a Darci receberá email SEMPRE que houver um alteração. 30/05/2022 - contábil 01/06/2022
                bEnviarEmailDarci = true;
                //if (item_dissertacao.data_aprovacao == null)
                //{
                //    bEnviarEmailDarci = true;
                //}
                //else
                //{
                //    bEnviarEmailDarci = false;
                //}

                item_dissertacao = aplicacaoAluno.AprovarDissertacao(item_dissertacao);

                item = aplicacaoAluno.BuscaItem(item);
                //item.matricula_turma.Where(x => x.id_turma == qIdTurma).SingleOrDefault().banca.Where(x => x.tipo_banca == "Defesa").SingleOrDefault().banca_dissertacao.ElementAt(0).banca_dissertacao_obs.Add(itemObs);
                Session[qTab + "Aluno"] = item;

                //====================

                string sAux;

                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Configuracoes item_configuracoes;
                // 1 = email mestrado@ipt.br
                // 2 = email suporte@ipt.br
                item_configuracoes = aplicacaoGerais.BuscaConfiguracoes(1);

                string sFrom = item_configuracoes.remetente_email;
                string sFrom_Nome = item_configuracoes.nome_remetente_email;
                string sTo;
                string sAssunto = "Aprovado " + sTipoDissertacao + " do Aluno: " + item_dissertacao.banca.matricula_turma.alunos.nome;

                UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                usuarios usuario_remetente = new usuarios();
                usuario_remetente.usuario = item_dissertacao.usuario;
                usuario_remetente = aplicacaoUsuario.BuscaUsuario(usuario_remetente);

                sAux = sTipoDissertacao + " Aprovado<br><br>";
                sAux = sAux + "Título: <strong>" + item_dissertacao.banca.titulo + "</strong><br>";
                sAux = sAux + "Palavras-chave: <strong>" + item_dissertacao.palavras_chavePreview + "</strong><br>";
                sAux = sAux + "Cod IPT: <strong>" + item_dissertacao.cod_iptPreview + "</strong><br>";
                sAux = sAux + "Resumo: <strong>" + item_dissertacao.resumoPreview + "</strong><br>";
                sAux = sAux + "Nome do Documento: <strong>" + item_dissertacao.arquivoPreview + "</strong><br>";
                sAux = sAux + "Responsável: <strong>" + usuario_remetente.nomeSocial.Trim() + "</strong><br>";

                if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString == "Producao")
                {
                    sTo = usuario_remetente.email;
                }
                else
                {
                    sTo = "kelsey@ipt.br"; // usuario.email;
                    sAux = sAux + "<br><br> <strong>Esse email seria enviado para:</strong>" + usuario_remetente.email;
                }

                //sTo = "kelsey@ipt.br";

                Utilizades.fEnviaEmail(sFrom, sFrom_Nome, sTo, "", "", sAssunto, sAux, item_configuracoes.servidor_email, item_configuracoes.conta_email, item_configuracoes.senha_email, item_configuracoes.porta_email.Value, 1, "");

                //======================================================
                //Se Dissertação nova, então enviar email para a Darci
                //Em conversa com a Longuinho, definiu-se que a Darci receberá email SEMPRE que houver um alteração. 30/05/2022 - contábil 01/06/2022
                if (bEnviarEmailDarci)
                {

                    Configuracoes itemConfig;
                    // 1 = email mestrado@ipt.br
                    // 2 = email suporte@ipt.br
                    itemConfig = aplicacaoGerais.BuscaConfiguracoes(1);

                    string qDe = itemConfig.remetente_email;
                    string qDe_Nome = itemConfig.nome_remetente_email;
                    string qPara;
                    string qCopia = "";
                    string qCopiaOculta = "";
                    string qAssunto = "";

                    if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString == "Producao")
                    {
                        qPara = itemConfig.remetente_email + ";mdarci@ipt.br";
                    }
                    else
                    {
                        qPara = "kelsey@ipt.br";
                        //qPara = usuario.email;
                        qAssunto = "Teste - email iria para: " + itemConfig.remetente_email + ";mdarci@ipt.br" + " - ";
                    }

                    if (item_dissertacao.data_aprovacao != null)
                    {
                        qAssunto = qAssunto + "(atualização)";
                    }

                    qAssunto = qAssunto + sTipoDissertacao + " " + listaBanca.ElementAt(0).horario.Value.Year + " - " + listaBanca.ElementAt(0).matricula_turma.alunos.nome;
                    string qCorpo = "";
                    qCorpo = qCorpo + "Olá. <br><br>";
                    qCorpo = qCorpo + "Segue dados de " + sTipoDissertacao + " publicado hoje - " + DateTime.Today.ToString("dd/MM/yyyy") + ".<br><br><br>";
                    qCorpo = qCorpo + "Aluno: <strong>" + item_dissertacao.banca.matricula_turma.alunos.nome + "</strong><br><br>";
                    qCorpo = qCorpo + "Título " + sTipoDissertacao + ": <strong>" + item_dissertacao.banca.titulo + "</strong><br><br>";
                    qCorpo = qCorpo + "Resumo: <strong>" + item_dissertacao.resumoPreview + "</strong><br><br>";
                    qCorpo = qCorpo + "Ano: <strong>" + item_dissertacao.banca.horario.Value.Year.ToString() + "</strong><br><br>";
                    qCorpo = qCorpo + "Curso: <strong>" + item_dissertacao.banca.matricula_turma.turmas.cursos.sigla + "</strong><br><br>";
                    qCorpo = qCorpo + "Orientador: <strong>" + item_dissertacao.banca.banca_professores.Where(x => x.tipo_professor == "Orientador").FirstOrDefault().professores.nome + "</strong><br><br>";
                    qCorpo = qCorpo + "PDF: <strong>" + item_dissertacao.arquivoPreview + "</strong><br><br><br>";
                    qCorpo = qCorpo + "<span style=\"color:#535353;font-size:11px\">E-mail enviado automaticamente pelo sistema SAPIENS (não responder). </span>";

                    if (!Utilizades.fEnviaEmail(qDe, qDe_Nome, qPara, qCopia, qCopiaOculta, qAssunto, qCorpo, itemConfig.servidor_email, itemConfig.conta_email, itemConfig.senha_email, itemConfig.porta_email.Value, 1, ""))
                    {
                        json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na emissão de email com informações de " + sTipoDissertacao + "\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                    }


                    //== - Enviar e-mail para o aluno ================================

                    if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString == "Producao")
                    {
                        qPara = item_dissertacao.banca.matricula_turma.alunos.email;
                        qAssunto = "SAPIENS - Publicação de " + sTipoDissertacao;
                    }
                    else
                    {
                        qPara = "kelsey@ipt.br";
                        //qPara = usuario.email;
                        qAssunto = "Teste(email aluno) - email iria para: " + item_dissertacao.banca.matricula_turma.alunos.email + " - SAPIENS - Publicação de " + sTipoDissertacao;
                    }

                    qCorpo = "";
                    qCorpo = qCorpo + "Olá " + item_dissertacao.banca.matricula_turma.alunos.nome + "<br><br>";
                    qCorpo = qCorpo + "Segue dados de " + sTipoDissertacao + " publicado hoje - " + DateTime.Today.ToString("dd/MM/yyyy") + " no <a href=\"https://sapiens.ipt.br\"> SAPIENS</a>.<br><br><br>";
                    qCorpo = qCorpo + "Título " + sTipoDissertacao + ": <strong>" + item_dissertacao.banca.titulo + "</strong><br><br>";
                    qCorpo = qCorpo + "Resumo: <strong>" + item_dissertacao.resumoPreview + "</strong><br><br>";
                    qCorpo = qCorpo + "Ano: <strong>" + item_dissertacao.banca.horario.Value.Year.ToString() + "</strong><br><br>";
                    qCorpo = qCorpo + "Curso: <strong>" + item_dissertacao.banca.matricula_turma.turmas.cursos.sigla + "</strong><br><br>";
                    qCorpo = qCorpo + "Orientador: <strong>" + item_dissertacao.banca.banca_professores.Where(x => x.tipo_professor == "Orientador").FirstOrDefault().professores.nome + "</strong><br><br>";
                    qCorpo = qCorpo + "PDF: <strong><a href=\"https://sapiens.ipt.br/Teses/" + item_dissertacao.arquivoPreview + "\"> Clique aqui</a> </strong><br><br><br>";
                    if (sTipoDissertacao == "Dissertação")
                    {
                        qCorpo = qCorpo + "URL: <strong><a href=\"https://sapiens.ipt.br?p=index_dissertacoes.aspx\"> SAPIENS - Dissertações</a> </strong><br><br><br>";
                    }
                    else
                    {
                        qCorpo = qCorpo + "URL: <strong><a href=\"https://sapiens.ipt.br?p=index_tcc.aspx\"> SAPIENS - Monografias</a> </strong><br><br><br>";
                    }


                    qCorpo = qCorpo + "<span style=\"color:#535353;font-size:11px\">E-mail enviado automaticamente pelo sistema SAPIENS (não responder). </span>";

                    if (!Utilizades.fEnviaEmail(qDe, qDe_Nome, qPara, qCopia, qCopiaOculta, qAssunto, qCorpo, itemConfig.servidor_email, itemConfig.conta_email, itemConfig.senha_email, itemConfig.porta_email.Value, 1, ""))
                    {
                        json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na emissão de email com informações de " + sTipoDissertacao + "\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                    }



                }
                //================

                Session[qTab + "Aluno"] = item;
                //(this.Master as SERPI).PreencheSininho();

                json = "[{\"P0\":\"ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";

                //json = jsSS.Serialize(listaRetorno);

                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fReprovarDissertacao()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                int qIdTurma = Convert.ToInt32(HttpContext.Current.Request["qIdTurma"]);
                string qObs = HttpContext.Current.Request["qObs"];

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                matricula_turma item_matricula;
                retornoGeral retorno = new retornoGeral();

                item_matricula = item.matricula_turma.Where(x => x.id_turma == qIdTurma).SingleOrDefault();

                string sTipoDissertacao;
                if (item_matricula.turmas.cursos.id_tipo_curso == 3)
                {
                    sTipoDissertacao = "Monografia";
                }
                else
                {
                    sTipoDissertacao = "Dissertação";
                }

                List<banca> listaBanca = new List<banca>();
                List<banca> listaBancaQualificacao = new List<banca>();
                List<banca_professores> listaBancaProfessores = new List<banca_professores>();

                listaBanca = item_matricula.banca.Where(x => x.tipo_banca == "Defesa").ToList();

                banca item_banca = item_matricula.banca.Where(x => x.tipo_banca == "Defesa").SingleOrDefault();

                //listaBancaQualificacao = item_matricula.banca.Where(x => x.tipo_banca == "Qualificação").ToList();

                banca_dissertacao item_dissertacao = item_banca.banca_dissertacao.ElementAt(0);
                item_dissertacao.data_alteracao = DateTime.Now;
                item_dissertacao.data_reprovacao = DateTime.Now;
                //item_dissertacao.usuario = "Mary";
                item_dissertacao.usuarioAprovacao = usuario.usuario;
                item_dissertacao.statusAprovacao = 2; //Reprovação
                item_dissertacao = aplicacaoAluno.AlteraStatusDissertacao(item_dissertacao);


                banca_dissertacao_obs itemObs = new banca_dissertacao_obs();
                itemObs.dataObs = DateTime.Now;
                itemObs.observacao = qObs;
                itemObs.id_banca_dissertacao = item_dissertacao.id_banca_dissertacao;
                itemObs.usuario = usuario.usuario;

                item_dissertacao.banca_dissertacao_obs.Add(aplicacaoAluno.CriarItem_Obs(itemObs));
                item_banca.banca_dissertacao.ElementAt(0).banca_dissertacao_obs.Add(itemObs);

                item = aplicacaoAluno.BuscaItem(item);
                //item.matricula_turma.Where(x => x.id_turma == qIdTurma).SingleOrDefault().banca.Where(x => x.tipo_banca == "Defesa").SingleOrDefault().banca_dissertacao.ElementAt(0).banca_dissertacao_obs.Add(itemObs);
                Session[qTab + "Aluno"] = item;

                //====================

                string sAux;

                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Configuracoes item_configuracoes;
                // 1 = email mestrado@ipt.br
                // 2 = email suporte@ipt.br
                item_configuracoes = aplicacaoGerais.BuscaConfiguracoes(1);

                string sFrom = item_configuracoes.remetente_email;
                string sFrom_Nome = item_configuracoes.nome_remetente_email;
                string sTo;
                string sAssunto = "Reprovação de " + sTipoDissertacao + " do Aluno: " + item_dissertacao.banca.matricula_turma.alunos.nome;

                UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                usuarios usuario_remetente = new usuarios();
                usuario_remetente.usuario = item_dissertacao.usuario;
                usuario_remetente = aplicacaoUsuario.BuscaUsuario(usuario_remetente);

                sAux = sTipoDissertacao + " Reprovado<br><br>";
                sAux = sAux + "Título: <strong>" + item_dissertacao.banca.titulo + "</strong><br>";
                sAux = sAux + "Palavras-chave: <strong>" + item_dissertacao.palavras_chavePreview + "</strong><br>";
                sAux = sAux + "Cod IPT: <strong>" + item_dissertacao.cod_iptPreview + "</strong><br>";
                sAux = sAux + "Resumo: <strong>" + item_dissertacao.resumoPreview + "</strong><br>";
                sAux = sAux + "Nome do Documento: <strong>" + item_dissertacao.arquivoPreview + "</strong><br>";
                sAux = sAux + "Responsável: <strong>" + usuario_remetente.nomeSocial.Trim() + "</strong><br><br>";
                sAux = sAux + "Reprovado por: <strong>" + usuario.nomeSocial.Trim() + "</strong><br>";
                sAux = sAux + "Obs: <strong>" + qObs + "</strong><br>";

 

                    if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString == "Producao")
                    {
                        sTo = usuario_remetente.email;
                    }
                    else
                    {
                        sTo = "kelsey@ipt.br"; // usuario.email;
                        sAux = sAux + "<br><br> <strong>Esse email seria enviado para:</strong>" + usuario_remetente.email;
                    }

                    //sTo = "kelsey@ipt.br";

                    Utilizades.fEnviaEmail(sFrom, sFrom_Nome, sTo, "", "", sAssunto, sAux, item_configuracoes.servidor_email, item_configuracoes.conta_email, item_configuracoes.senha_email, item_configuracoes.porta_email.Value, 1, "");

                //(this.Master as SERPI).PreencheSininho();


                Session[qTab + "Aluno"] = item;
                //(this.Master as SERPI).PreencheSininho();

                json = "[{\"P0\":\"ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";

                //json = jsSS.Serialize(listaRetorno);

                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fSalvarDissertacaoBancaDefesaAluno()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                int qVititas = Convert.ToInt32(HttpContext.Current.Request.Form["qVititas"]);
                int qDownloads = Convert.ToInt32(HttpContext.Current.Request.Form["qDownloads"]);
                string qPalavras = HttpContext.Current.Request.Form["qPalavras"];
                string qResumo = HttpContext.Current.Request.Form["qResumo"];
                string qNomeArquivo = HttpContext.Current.Request.Form["qNomeArquivo"];
                int qIdTurma = Convert.ToInt32(HttpContext.Current.Request.Form["qTurma"]);
                int qBanca = Convert.ToInt32(HttpContext.Current.Request.Form["qBanca"]);
                string qCodIPT = HttpContext.Current.Request.Form["qCodIPT"];
                string qDissertacao = HttpContext.Current.Request.Form["qDissertacao"];
                HttpPostedFile qArquivo = HttpContext.Current.Request.Files["qArquivo"];

                if (qDissertacao.IndexOf("Monografia") != -1)
                {
                    qDissertacao = "2";
                }
                else
                {
                    qDissertacao = "1";
                }

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();

                alunos item;
                string qTab = HttpContext.Current.Request.Form["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                List <retornoGeral> listaRetorno = new List<retornoGeral>();

                matricula_turma item_matricula;
                retornoGeral retorno = new retornoGeral();

                item_matricula = item.matricula_turma.Where(x => x.id_turma == qIdTurma).SingleOrDefault();

                List<banca> listaBanca = new List<banca>();
                List<banca> listaBancaQualificacao = new List<banca>();
                List<banca_professores> listaBancaProfessores = new List<banca_professores>();

                listaBanca = item_matricula.banca.Where(x => x.tipo_banca == "Defesa").ToList();

                banca itemBanca = item_matricula.banca.Where(x => x.tipo_banca == "Defesa").SingleOrDefault();

                if (itemBanca.banca_dissertacao.Count == 0)
                {
                    banca_dissertacao item_dissertacao = new banca_dissertacao();

                    item_dissertacao.id_banca = qBanca;
                    item_dissertacao.status = "1";
                    item_dissertacao.visitas = qVititas;
                    item_dissertacao.downloads = qDownloads;
                    //item_dissertacao.palavras_chave = qPalavras;
                    //item_dissertacao.resumo = qResumo;
                    //item_dissertacao.arquivo = qNomeArquivo;
                    //item_dissertacao.cod_ipt = qCodIPT;
                    //item_dissertacao.usuario = usuario.usuario;
                    item_dissertacao.palavras_chavePreview = qPalavras;
                    item_dissertacao.resumoPreview = qResumo;
                    item_dissertacao.arquivoPreview = qNomeArquivo;
                    item_dissertacao.cod_iptPreview = qCodIPT;
                    item_dissertacao.usuario = usuario.usuario;
                    item_dissertacao.statusAprovacao = 3; //3 = Alterado
                    item_dissertacao.data_alteracao = DateTime.Now;
                    item_dissertacao.id_tipo_dissertacao = Convert.ToInt32(qDissertacao);

                    item_dissertacao = aplicacaoAluno.salvarDissertacao(item_dissertacao);

                    itemBanca.banca_dissertacao.Add(item_dissertacao);
                }
                else
                {
                    //item_dissertacao.palavras_chave = qPalavras;
                    //item_dissertacao.resumo = qResumo;
                    //item_dissertacao.arquivo = qNomeArquivo;
                    //item_dissertacao.cod_ipt = qCodIPT;
                    //item_dissertacao.usuario = usuario.usuario;
                    itemBanca.banca_dissertacao.ElementAt(0).palavras_chavePreview = qPalavras;
                    itemBanca.banca_dissertacao.ElementAt(0).resumoPreview = qResumo;
                    itemBanca.banca_dissertacao.ElementAt(0).arquivoPreview = qNomeArquivo;
                    itemBanca.banca_dissertacao.ElementAt(0).cod_iptPreview = qCodIPT;
                    itemBanca.banca_dissertacao.ElementAt(0).usuario = usuario.usuario;
                    itemBanca.banca_dissertacao.ElementAt(0).statusAprovacao = 3; //3 = Alterado
                    itemBanca.banca_dissertacao.ElementAt(0).data_alteracao = DateTime.Now;
                    itemBanca.banca_dissertacao.ElementAt(0).id_tipo_dissertacao = Convert.ToInt32(qDissertacao);

                    aplicacaoAluno.salvarDissertacao(itemBanca.banca_dissertacao.ElementAt(0));
                }
                
                if (qArquivo != null)
                {

                    //int qTamanhoArquivo = qArquivo.ContentLength;
                    //byte[] arrayByte = new byte[qTamanhoArquivo];
                    //qArquivo.InputStream.Read(arrayByte, 0, qTamanhoArquivo);
                    string filenamepath; //= System.IO.Path.Combine("S:" + qNomeArquivo);
                    string sLogin;
                    string sSenha;
                    string sAd;

                    if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString == "Producao")
                    {
                        filenamepath = "\\\\fileserveript\\SISTEMAS\\CASSIOPEA\\teses\\" + qNomeArquivo;
                        sLogin = "sapiens";
                        sSenha = "Sae082020";
                        sAd = "ipt-ad";
                        Utilizades.SalvaArquivoComAutenticacao(qArquivo, filenamepath, sLogin, sAd, sSenha);

                    }
                    else
                    {
                        qArquivo.SaveAs(Server.MapPath("") + "\\teses\\" + qNomeArquivo);
                    }

                    //qArquivo.SaveAs(filenamepath);

                }

                retorno = new retornoGeral();
                retorno.P0 = "ok";
                retorno.P1 = itemBanca.banca_dissertacao.ElementAt(0).data_cadastro.Value.ToString("dd/MM/yyyy");
                retorno.P2 = itemBanca.banca_dissertacao.ElementAt(0).data_alteracao.Value.ToString("dd/MM/yyyy");
                retorno.P3 = itemBanca.banca_dissertacao.ElementAt(0).usuario;

                Session[qTab + "Aluno"] = item;

                listaRetorno.Add(retorno);

                json = jsSS.Serialize(listaRetorno);

                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheBancaCoorientador()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                int qIdTurma = Convert.ToInt32(HttpContext.Current.Request["qIdTurma"]);
                string qBanca = HttpContext.Current.Request["qBanca"];

                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma item_matricula = new matricula_turma();
                item_matricula = item.matricula_turma.Where(x => x.id_turma == qIdTurma).SingleOrDefault();
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                item_matricula = item.matricula_turma.Where(x => x.id_turma == qIdTurma).SingleOrDefault();

                List<banca> listaBanca = new List<banca>();
                List<banca_professores> listaBancaProfessores = new List<banca_professores>();

                listaBanca = item_matricula.banca.Where(x => x.tipo_banca == qBanca).ToList();

                listaBancaProfessores = listaBanca.ElementAt(0).banca_professores.Where(x => x.tipo_professor == "Co-orientador").ToList();

                retornoGeral retorno;
                string sAux;

                if (listaBancaProfessores.Count > 0)
                {
                    foreach (var elemento in listaBancaProfessores.ToList())
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.professores.cpf;
                        retorno.P1 = elemento.professores.nome;

                        if (elemento.imprimir)
                        {
                            sAux = "checked";

                            retorno.P3 = "<div title=\"Atestado do Co-orientador\"> <a class=\"btn btn-info btn-circle fa fa-file-pdf-o\" href=\'javascript:fImprimirAtestado(\""
                            + elemento.id_professor + "\",\"" + elemento.professores.nome + "\",\"" + elemento.id_banca + "\",\"" + qBanca + "\")\'; ></a></div>";

                            retorno.P4 = "<div title=\"Recido do Co-orientador\"> <a class=\"btn btn-success btn-circle fa fa fa-money\" href=\'javascript:fImprimirRecibo(\""
                            + elemento.id_professor + "\",\"" + elemento.professores.nome + "\",\"" + elemento.id_banca + "\",\"" + qBanca + "\")\'; ></a></div>";

                            retorno.P5 = "<div title=\"Convite do Co-orientador\"> <a class=\"btn btn-purple btn-circle fa fa-envelope-o\" href=\'javascript:fImprimirConvite(\""
                            + elemento.id_professor + "\",\"" + elemento.professores.nome + "\",\"" + elemento.id_banca + "\",\"" + qBanca + "\")\'; ></a></div>";
                        }
                        else
                        {
                            sAux = "";
                        }

                        if (qBanca == "Qualificação")
                        {
                            retorno.P2 = ("<label class=\"checkbox\"><input onclick=\"fImprimirCoorientadorBanca(this, 'Qualificação');\" id = \"chkImprimirQualificacaoCoorientador_" + elemento.professores.id_professor.ToString() + "_" + elemento.id_banca + "\" type=\"checkbox\" name=\"chkImprimirQualificacaoCoorientador_" + elemento.professores.id_professor.ToString() + "_" + elemento.id_banca + "\" " + sAux + " ><span></span></label>");
                        }
                        else
                        {
                            retorno.P2 = ("<label class=\"checkbox\"><input onclick=\"fImprimirCoorientadorBanca(this, 'Defesa');\" id = \"chkImprimirDefesaCoorientador_" + elemento.professores.id_professor.ToString() + "_" + elemento.id_banca + "\" type=\"checkbox\" name=\"chkImprimirDefesaCoorientador_" + elemento.professores.id_professor.ToString() + "_" + elemento.id_banca + "\" " + sAux + " ><span></span></label>");
                        }

                        retorno.P6 = "<div title=\"Excluir Co-orientador\"> <a class=\"btn btn-danger btn-circle fa fa-eraser\" href=\'javascript:fModalExcluirMembroBanca(\""
                        + elemento.id_banca + "\",\"" + elemento.id_professor + "\",\"" + elemento.professores.nome + "\",\"Co-orientador\",\"" + qBanca + "\")\'; ></a></div>";

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheBancaMembro()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                int qIdTurma = Convert.ToInt32(HttpContext.Current.Request["qIdTurma"]);
                string qBanca = HttpContext.Current.Request["qBanca"];


                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma item_matricula = new matricula_turma();
                item_matricula = item.matricula_turma.Where(x => x.id_turma == qIdTurma).SingleOrDefault();
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                item_matricula = item.matricula_turma.Where(x => x.id_turma == qIdTurma).SingleOrDefault();

                List<banca> listaBanca = new List<banca>();
                List<banca_professores> listaBancaProfessores = new List<banca_professores>();

                listaBanca = item_matricula.banca.Where(x => x.tipo_banca == qBanca).ToList();

                listaBancaProfessores = listaBanca.ElementAt(0).banca_professores.Where(x => x.tipo_professor == "Membro" || x.tipo_professor == "Membro Suplente").OrderBy(x=> x.tipo_professor == "Membro Suplente").ThenBy(x=> x.tipo_professor == "Membro").ThenBy(x=> x.professores.nome).ToList();

                retornoGeral retorno;

                if (listaBancaProfessores.Count > 0)
                {
                    foreach (var elemento in listaBancaProfessores.ToList())
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.professores.cpf;
                        retorno.P1 = elemento.professores.nome;

                       
                        retorno.P3 = "<div title=\"Atestado do " + elemento.tipo_professor + "\"> <a class=\"btn btn-info btn-circle fa fa-file-pdf-o\" href=\'javascript:fImprimirAtestado(\""
                        + elemento.id_professor + "\",\"" + elemento.professores.nome + "\",\"" + elemento.id_banca + "\",\"" + qBanca + "\")\'; ></a></div>";

                        if (elemento.professores.professores_forma_recebimento.banca != 1)
                        {
                            retorno.P4 = "<div title=\"Recido do " + elemento.tipo_professor + "\"> <a class=\"btn btn-success btn-circle fa fa fa-money\" href=\'javascript:fImprimirRecibo(\""
                            + elemento.id_professor + "\",\"" + elemento.professores.nome + "\",\"" + elemento.id_banca + "\",\"" + qBanca + "\")\'; ></a></div>";
                        }

                        retorno.P5 = "<div title=\"Convite do " + elemento.tipo_professor + "\"> <a class=\"btn btn-purple btn-circle fa fa-envelope-o\" href=\'javascript:fImprimirConvite(\""
                        + elemento.id_professor + "\",\"" + elemento.professores.nome + "\",\"" + elemento.id_banca + "\",\"" + qBanca + "\")\'; ></a></div>";


                        if (elemento.tipo_professor == "Membro Suplente")
                        {
                            retorno.P2 = "Suplente";
                        }

                        retorno.P6 = "<div title=\"Excluir " + elemento.tipo_professor + "\"> <a class=\"btn btn-danger btn-circle fa fa-eraser\" href=\'javascript:fModalExcluirMembroBanca(\""
                        + elemento.id_banca + "\",\"" + elemento.id_professor + "\",\"" + elemento.professores.nome + "\",\"" + elemento.tipo_professor + "\",\"" + qBanca + "\")\'; ></a></div>";

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fSalvarDadosQualificacao()
        {
            Session.Timeout = 60;
            try
            {
                string json = "";
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 31))
                {
                    json = "[{\"P0\":\"Aviso\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                string sAux;
                sAux = "";

                if (item.inadimplentes != null)
                {
                    sAux = "O aluno está Inadimplente... favor instruí-lo à procurar o setor Financerio para regularização.";
                }

                if (item.RefazerProficienciaIngles == 1)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<br><br>";
                    }
                    sAux = sAux + "O aluno precisa Refazer prova de Proficiência em Inglês";
                }
                if (item.RefazerProvaPortugues == 1)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<br><br>";
                    }
                    sAux = sAux + "O aluno precisa Refazer prova de Português";
                }

                if (sAux != "")
                {
                    json = "[{\"P0\":\"Aviso\",\"P1\":\"" + sAux + "\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!item.entregou_rg)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<br><br>";
                    }
                    sAux = sAux + "O aluno precisa entregar o RG.";
                }
                if (!item.entregou_cpf)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<br><br>";
                    }
                    sAux = sAux + "O aluno precisa entregar o CPF.";
                }
                
                //Suprimido no dia 06/01/22 solicitado pela Andreia Longuinho autorizada pelo prof Eduardo
                //if (!item.entregou_historico)
                //{
                //    if (sAux != "")
                //    {
                //        sAux = sAux + "<br><br>";
                //    }
                //    sAux = sAux + "O aluno precisa entregar o Histórico Escolar.";
                //}
                if (!item.entregou_diploma)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<br><br>";
                    }
                    sAux = sAux + "O aluno precisa entregar o Diploma.";
                }
                if (!item.entregou_comprovante_end)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<br><br>";
                    }
                    sAux = sAux + "O aluno precisa entregar o Comprovante de Endereço.";
                }
                if (!item.entregou_certidao)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<br><br>";
                    }
                    sAux = sAux + "O aluno precisa entregar a Certidão de Nascimento/Casamento.";
                }
                //if (!item.entregou_contrato)
                //{
                //    if (sAux != "")
                //    {
                //        sAux = sAux + "<br><br>";
                //    }
                //    sAux = sAux + "O aluno precisa entregar o Contrato Assinado.";
                //}

                int qIdTurma = Convert.ToInt32(HttpContext.Current.Request["qIdTurma"]);
                matricula_turma item_matricula = new matricula_turma();
                item_matricula = item.matricula_turma.Where(x => x.id_turma == qIdTurma).SingleOrDefault();

                if (!item.alunos_arquivos.Any(x=> x.id_alunos_arquivos_tipo == 9 && x.id_matricula_turma == item_matricula.id_matricula_turma))
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<br><br>";
                    }
                    sAux = sAux + "O aluno precisa entregar o Contrato Assinado.";
                }

                string qIdBanca = HttpContext.Current.Request["qIdBanca"];
                string qData = HttpContext.Current.Request["qData"];
                string qHora = HttpContext.Current.Request["qHora"];
                string qRemota = HttpContext.Current.Request["qRemota"];
                string qResultado = HttpContext.Current.Request["qResultado"];
                string qTitulo = HttpContext.Current.Request["qTitulo"];
                string qObservacao = HttpContext.Current.Request["qObservacao"];

                List<banca> listaBanca = new List<banca>();
                banca item_banca = new banca();

                if (qIdBanca != "")
                {
                    listaBanca = item_matricula.banca.Where(x => x.id_banca == Convert.ToInt32(HttpContext.Current.Request["qIdBanca"])).ToList();
                    listaBanca.ElementAt(0).horario = Convert.ToDateTime(qData + " " + qHora);

                    if (qResultado!="")
                    {
                        if (qResultado == "Aprovado")
                        {
                            if (item_matricula.historico_matricula_turma.Any(x => x.situacao == "Qualificação"))
                            {
                                historico_matricula_turma itemHistoricoMatricula = new historico_matricula_turma();
                                itemHistoricoMatricula = item_matricula.historico_matricula_turma.Where(x => x.situacao == "Qualificação").FirstOrDefault();
                                //itemHistoricoMatricula.id_historico = Convert.ToInt32(qIdHistorico);
                                itemHistoricoMatricula.data_inicio = Convert.ToDateTime(qData);
                                itemHistoricoMatricula.data_fim = Convert.ToDateTime(qData);
                                itemHistoricoMatricula.data = DateTime.Now;
                                itemHistoricoMatricula.usuario = usuario.usuario;
                                aplicacaoAluno.EditarSituacaoHistorico(itemHistoricoMatricula);
                                
                            }
                            else
                            {
                                historico_matricula_turma itemHistoricoMatricula = new historico_matricula_turma();
                                itemHistoricoMatricula.id_matricula_turma = item_matricula.id_matricula_turma;
                                itemHistoricoMatricula.data_inicio = Convert.ToDateTime(qData);
                                itemHistoricoMatricula.data_fim = Convert.ToDateTime(qData);
                                //itemHistoricoMatricula.data_previsao_termino = Convert.ToDateTime(HttpContext.Current.Request["qDataFim"]);
                                itemHistoricoMatricula.status = "Regular";
                                itemHistoricoMatricula.situacao = "Qualificação";
                                itemHistoricoMatricula.ordem = 2;
                                itemHistoricoMatricula.data = DateTime.Now;
                                itemHistoricoMatricula.usuario = usuario.usuario;

                                aplicacaoAluno.CriarSituacaoHistorico(itemHistoricoMatricula);
                            }
                           
                        }
                        else
                        {
                            if (item_matricula.historico_matricula_turma.Any(x => x.situacao == "Qualificação"))
                            {
                                aplicacaoAluno.ApagarSituacaoHistorico(item_matricula.historico_matricula_turma.Where(x => x.situacao == "Qualificação").FirstOrDefault());
                            }
                        }


                    }
                    else if (item_matricula.historico_matricula_turma.Any(x=> x.situacao == "Qualificação"))
                    {
                        if (item_matricula.historico_matricula_turma.Any(x => x.situacao == "Qualificação"))
                        {
                            aplicacaoAluno.ApagarSituacaoHistorico(item_matricula.historico_matricula_turma.Where(x => x.situacao == "Qualificação").FirstOrDefault());
                        }
                    }

                    listaBanca.ElementAt(0).remoto = Convert.ToInt16(qRemota);
                    listaBanca.ElementAt(0).resultado = qResultado;
                    listaBanca.ElementAt(0).titulo = qTitulo;
                    listaBanca.ElementAt(0).observacao = qObservacao;
                    listaBanca.ElementAt(0).status = "alterado";
                    listaBanca.ElementAt(0).data_alteracao = DateTime.Now;
                    listaBanca.ElementAt(0).usuario = usuario.usuario;

                    aplicacaoAluno.AlterarBanca(listaBanca.ElementAt(0));
                }
                else
                {
                    item_banca.id_matricula_turma = item_matricula.id_matricula_turma;
                    item_banca.tipo_banca = "Qualificação";
                    item_banca.horario = Convert.ToDateTime(qData + " " + qHora);
                    item_banca.remoto = Convert.ToInt16(qRemota);
                    item_banca.resultado = qResultado;
                    item_banca.titulo = qTitulo;
                    item_banca.observacao = qObservacao;
                    item_banca.status = "cadastrado";
                    item_banca.data_cadastro = DateTime.Now;
                    item_banca.data_alteracao = item_banca.data_cadastro;
                    item_banca.usuario = usuario.usuario;

                    item_banca = aplicacaoAluno.IncluirBanca(item_banca);

                    banca_professores pItem;
                    if (item_banca.id_banca != 0)
                    {
                        foreach (var elemento in item_matricula.matricula_turma_orientacao)
                        {
                            pItem = new banca_professores();
                            pItem.id_banca = item_banca.id_banca;
                            pItem.id_professor = elemento.id_professor;
                            pItem.tipo_professor = elemento.tipo_orientacao;
                            pItem.status = "cadastrado";
                            pItem.data_cadastro = DateTime.Now;
                            pItem.data_alteracao = pItem.data_cadastro;
                            pItem.usuario = usuario.usuario;
                            pItem.imprimir = true;

                            aplicacaoAluno.IncluirProfessorBanca(pItem);
                        }
                    }
                }

                item = aplicacaoAluno.BuscaItem(item);

                Session[qTab + "Aluno"] = item;

                json = "[{\"P0\":\"ok\",\"P1\":\"" + item_banca.id_banca + "\",\"P2\":\"" + sAux + "\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fSalvarDadosDefesa()
        {
            Session.Timeout = 60;
            try
            {
                string json = "";
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 31))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                string sAux;
                sAux = "";
                if (item.inadimplentes != null)
                {
                    sAux = "O aluno está Inadimplente... favor instruí-lo à procurar o setor Financerio para regularização.";
                }

                if (item.RefazerProficienciaIngles == 1)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<br><br>";
                    }
                    sAux = sAux + "O aluno precisa Refazer prova de Proficiência em Inglês";
                }
                if (item.RefazerProvaPortugues == 1)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<br><br>";
                    }
                    sAux = sAux + "O aluno precisa Refazer prova de Português";
                }

                if (!item.entregou_rg)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<br><br>";
                    }
                    sAux = sAux + "O aluno precisa entregar o RG.";
                }
                if (!item.entregou_cpf)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<br><br>";
                    }
                    sAux = sAux + "O aluno precisa entregar o CPF.";
                }
                //Suprimido no dia 06/01/22 solicitado pela Andreia Longuinho autorizada pelo prof Eduardo
                //if (!item.entregou_historico)
                //{
                //    if (sAux != "")
                //    {
                //        sAux = sAux + "<br><br>";
                //    }
                //    sAux = sAux + "O aluno precisa entregar o Histórico Escolar.";
                //}
                if (!item.entregou_diploma)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<br><br>";
                    }
                    sAux = sAux + "O aluno precisa entregar o Diploma.";
                }
                if (!item.entregou_comprovante_end)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<br><br>";
                    }
                    sAux = sAux + "O aluno precisa entregar o Comprovante de Endereço.";
                }
                if (!item.entregou_certidao)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<br><br>";
                    }
                    sAux = sAux + "O aluno precisa entregar a Certidão de Nascimento/Casamento.";
                }

                //if (!item.entregou_contrato)
                //{
                //    if (sAux != "")
                //    {
                //        sAux = sAux + "<br><br>";
                //    }
                //    sAux = sAux + "O aluno precisa entregar o Contrato Assinado.";
                //}

                int qIdTurma = Convert.ToInt32(HttpContext.Current.Request["qIdTurma"]);
                matricula_turma item_matricula = new matricula_turma();
                item_matricula = item.matricula_turma.Where(x => x.id_turma == qIdTurma).SingleOrDefault();

                string qData = HttpContext.Current.Request["qData"];
                string qDataEntrega = HttpContext.Current.Request["qDataEntrega"];
                
                //Aqui deixa passar ausencia de contrato anterior à 24/02/2021
                if (!item.alunos_arquivos.Any(x => x.id_alunos_arquivos_tipo == 9 && x.id_matricula_turma == item_matricula.id_matricula_turma)
                    && Convert.ToDateTime(qData) > Convert.ToDateTime("2021-02-24"))
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<br><br>";
                    }
                    sAux = sAux + "O aluno precisa entregar o Contrato Assinado.";
                }

                if (qDataEntrega != "")
                {
                    if (Convert.ToDateTime(qDataEntrega) < Convert.ToDateTime(qData))
                    {
                        if (sAux != "")
                        {
                            sAux = sAux + "<br><br>";
                        }
                        sAux = sAux + "A Data Aprovação Orientador não pode ser menor que a Data da Defesa.";
                    }
                }

                if (sAux != "")
                {
                    json = "[{\"P0\":\"Aviso\",\"P1\":\"" + sAux + "\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdBanca = HttpContext.Current.Request["qIdBanca"];
                
                string qHora = HttpContext.Current.Request["qHora"];
                string qRemota = HttpContext.Current.Request["qRemota"];
                string qResultado = HttpContext.Current.Request["qResultado"];
                string qTitulo = HttpContext.Current.Request["qTitulo"];
                string qObservacao = HttpContext.Current.Request["qObservacao"];
                string qNumeroPortariaMecBancaDefesa = HttpContext.Current.Request["qNumeroPortariaMecBancaDefesa"];
                string qDataPortariaMecBancaDefesa = HttpContext.Current.Request["qDataPortariaMecBancaDefesa"];
                string qDataDOUBancaDefesa = HttpContext.Current.Request["qDataDOUBancaDefesa"];

                DateTime temp;
                sAux = "";
                if (DateTime.TryParse(qDataEntrega, out temp))
                {
                    //if (!DateTime.TryParse(item_matricula.data_artigo.ToString(), out temp))
                    //{
                    //    sAux = sAux + "Para se incluir uma 'Data Aprovação Orientador' é necessário que antes se inclua a 'Data entrega Artigo'.<br><br>";
                    //}
                    if (qResultado != "Aprovado")
                    {
                        sAux = sAux + "Para se incluir uma 'Data Aprovação Orientador' é necessário que o Resultado seja 'Aprovado'.<br><br>";
                    }
                }

                if (sAux != "")
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"" + sAux + "\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                List<banca> listaBanca = new List<banca>();
                banca item_banca = new banca();
                //=====

                if (qResultado != "")
                {
                    if (qResultado == "Aprovado")
                    {
                        if (item_matricula.historico_matricula_turma.Any(x => x.situacao == "Titulado"))
                        {
                            historico_matricula_turma itemHistoricoMatricula = new historico_matricula_turma();
                            itemHistoricoMatricula = item_matricula.historico_matricula_turma.Where(x => x.situacao == "Titulado").FirstOrDefault();
                            //itemHistoricoMatricula.id_historico = Convert.ToInt32(qIdHistorico);
                            itemHistoricoMatricula.data_inicio = Convert.ToDateTime(qData);
                            itemHistoricoMatricula.data_fim = Convert.ToDateTime(qData);
                            itemHistoricoMatricula.data = DateTime.Now;
                            itemHistoricoMatricula.usuario = usuario.usuario;
                            aplicacaoAluno.EditarSituacaoHistorico(itemHistoricoMatricula);

                        }
                        else
                        {
                            historico_matricula_turma itemHistoricoMatricula = new historico_matricula_turma();
                            itemHistoricoMatricula.id_matricula_turma = item_matricula.id_matricula_turma;
                            itemHistoricoMatricula.data_inicio = Convert.ToDateTime(qData);
                            itemHistoricoMatricula.data_fim = Convert.ToDateTime(qData);
                            //itemHistoricoMatricula.data_previsao_termino = Convert.ToDateTime(HttpContext.Current.Request["qDataFim"]);
                            itemHistoricoMatricula.status = "Regular";
                            itemHistoricoMatricula.situacao = "Titulado";
                            itemHistoricoMatricula.ordem = 3;
                            itemHistoricoMatricula.data = DateTime.Now;
                            itemHistoricoMatricula.usuario = usuario.usuario;

                            aplicacaoAluno.CriarSituacaoHistorico(itemHistoricoMatricula);
                        }
                    }
                    else
                    {
                        if (item_matricula.historico_matricula_turma.Any(x => x.situacao == "Titulado"))
                        {
                            aplicacaoAluno.ApagarSituacaoHistorico(item_matricula.historico_matricula_turma.Where(x => x.situacao == "Titulado").FirstOrDefault());
                        }
                    }
                }
                else if (item_matricula.historico_matricula_turma.Any(x => x.situacao == "Titulado"))
                {
                    if (item_matricula.historico_matricula_turma.Any(x => x.situacao == "Titulado"))
                    {
                        aplicacaoAluno.ApagarSituacaoHistorico(item_matricula.historico_matricula_turma.Where(x => x.situacao == "Titulado").FirstOrDefault());
                    }
                }

                //====


                if (qIdBanca != "")
                {
                    listaBanca = item_matricula.banca.Where(x => x.id_banca == Convert.ToInt32(HttpContext.Current.Request["qIdBanca"])).ToList();
                    listaBanca.ElementAt(0).horario = Convert.ToDateTime(qData + " " + qHora);
                    listaBanca.ElementAt(0).remoto = Convert.ToInt16(qRemota);
                    listaBanca.ElementAt(0).resultado = qResultado;
                    listaBanca.ElementAt(0).titulo = qTitulo;
                    listaBanca.ElementAt(0).observacao = qObservacao;
                    if (qDataEntrega != "")
                    {
                        listaBanca.ElementAt(0).data_entrega_trabalho = Convert.ToDateTime(qDataEntrega);
                    }
                    else
                    {
                        listaBanca.ElementAt(0).data_entrega_trabalho = null;
                    }
                    if (qNumeroPortariaMecBancaDefesa.Trim() != "")
                    {
                        listaBanca.ElementAt(0).portaria_mec = qNumeroPortariaMecBancaDefesa.Trim();
                    }
                    else
                    {
                        listaBanca.ElementAt(0).portaria_mec = null;
                    }
                    if (qDataPortariaMecBancaDefesa != "")
                    {
                        listaBanca.ElementAt(0).data_portaria_mec = Convert.ToDateTime(qDataPortariaMecBancaDefesa);
                    }
                    else
                    {
                        listaBanca.ElementAt(0).data_portaria_mec = null;
                    }
                    if (qDataDOUBancaDefesa != "")
                    {
                        listaBanca.ElementAt(0).data_diario_oficial = Convert.ToDateTime(qDataDOUBancaDefesa);
                    }
                    else
                    {
                        listaBanca.ElementAt(0).data_diario_oficial = null;
                    }

                    listaBanca.ElementAt(0).status = "alterado";
                    listaBanca.ElementAt(0).data_alteracao = DateTime.Now;
                    listaBanca.ElementAt(0).usuario = usuario.usuario;

                    aplicacaoAluno.AlterarBanca(listaBanca.ElementAt(0));
                }
                else
                {
                    item_banca.id_matricula_turma = item_matricula.id_matricula_turma;
                    item_banca.tipo_banca = "Defesa";
                    item_banca.horario = Convert.ToDateTime(qData + " " + qHora);
                    item_banca.remoto = Convert.ToInt16(qRemota);
                    item_banca.resultado = qResultado;
                    item_banca.titulo = qTitulo;
                    item_banca.observacao = qObservacao;
                    if (qDataEntrega != "")
                    {
                        item_banca.data_entrega_trabalho = Convert.ToDateTime(qDataEntrega);
                    }
                    item_banca.status = "cadastrado";
                    item_banca.data_cadastro = DateTime.Now;
                    item_banca.data_alteracao = item_banca.data_cadastro;
                    item_banca.usuario = usuario.usuario;

                    item_banca = aplicacaoAluno.IncluirBanca(item_banca);

                    banca_professores pItem;
                    //======
                    if (item_matricula.turmas.cursos.id_tipo_curso != 3)
                    {
                        listaBanca = item_matricula.banca.Where(x => x.tipo_banca == "Qualificação").ToList();
                        foreach (var elemento in listaBanca.ElementAt(0).banca_professores)
                        {
                            pItem = new banca_professores();
                            pItem.id_banca = item_banca.id_banca;
                            pItem.id_professor = elemento.id_professor;
                            pItem.tipo_professor = elemento.tipo_professor;
                            pItem.status = "cadastrado";
                            pItem.data_cadastro = DateTime.Now;
                            pItem.data_alteracao = pItem.data_cadastro;
                            pItem.usuario = usuario.usuario;
                            pItem.imprimir = true;

                            aplicacaoAluno.IncluirProfessorBanca(pItem);
                        }
                    }
                    else
                    {
                        List<matricula_turma_orientacao> listaOrientacao = new List<matricula_turma_orientacao>();

                        List<matricula_turma_orientacao> listaCo_Orientacao = new List<matricula_turma_orientacao>();

                        listaOrientacao = item_matricula.matricula_turma_orientacao.Where(x => x.tipo_orientacao == "Orientador").ToList();

                        listaCo_Orientacao = item_matricula.matricula_turma_orientacao.Where(x => x.tipo_orientacao == "Co-orientador").OrderBy(x => x.professores.nome).ToList();

                        foreach (var elemento in listaOrientacao)
                        {
                            pItem = new banca_professores();
                            pItem.id_banca = item_banca.id_banca;
                            pItem.id_professor = elemento.id_professor;
                            pItem.tipo_professor = "Orientador";
                            pItem.status = "cadastrado";
                            pItem.data_cadastro = DateTime.Now;
                            pItem.data_alteracao = pItem.data_cadastro;
                            pItem.usuario = usuario.usuario;
                            pItem.imprimir = true;

                            aplicacaoAluno.IncluirProfessorBanca(pItem);
                        }

                        foreach (var elemento in listaCo_Orientacao)
                        {
                            pItem = new banca_professores();
                            pItem.id_banca = item_banca.id_banca;
                            pItem.id_professor = elemento.id_professor;
                            pItem.tipo_professor = "Co-orientador";
                            pItem.status = "cadastrado";
                            pItem.data_cadastro = DateTime.Now;
                            pItem.data_alteracao = pItem.data_cadastro;
                            pItem.usuario = usuario.usuario;
                            pItem.imprimir = true;

                            aplicacaoAluno.IncluirProfessorBanca(pItem);
                        }
                    }
                    //=====

                }


                item = aplicacaoAluno.BuscaItem(item);

                Session[qTab + "Aluno"] = item;

                json = "[{\"P0\":\"ok\",\"P1\":\"" + item_banca.id_banca + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPesquisaBancaDisponivel()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdTurma = HttpContext.Current.Request["qIdTurma"];
                string sCPF = HttpContext.Current.Request["qCPF"];
                string sNome = HttpContext.Current.Request["qNome"];
                string qBanca = HttpContext.Current.Request["qBanca"];
                string qTipoProfessor = HttpContext.Current.Request["qTipoProfessor"];

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                banca_professores item_Banca_Professores = new banca_professores();
                professores pItemProfessor = new professores();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma item_matricula = new matricula_turma();
                item_matricula = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).SingleOrDefault();

                List<banca> listaBanca = new List<banca>();
                if (qBanca == "Qualificação")
                {
                    listaBanca = item_matricula.banca.Where(x => x.tipo_banca == "Qualificação").ToList();
                }
                else
                {
                    listaBanca = item_matricula.banca.Where(x => x.tipo_banca == "Defesa").ToList();
                }

                pItemProfessor.cpf = sCPF;
                pItemProfessor.nome = sNome;

                List<professores> lista = new List<professores>();
                lista = aplicacaoAluno.ListBancaDisponiveis(listaBanca.ElementAt(0), pItemProfessor);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_professor.ToString();
                        retorno.P1 = elemento.nome;
                        retorno.P2 = elemento.cpf;
                        if (qTipoProfessor == "Orientador")
                        {
                            retorno.P3 = "<div title=\"Alterar Orientador\"> <a class=\"btn btn-success  btn-circle fa fa-refresh\" href=\'javascript:fAlterarOrientadorBanca(\""
                            + listaBanca.ElementAt(0).id_banca.ToString() + "\",\""
                            + elemento.id_professor.ToString() + "\",\""
                            + elemento.cpf + "\",\""
                            + elemento.nome + "\",\""
                            + qBanca + "\""
                            + ")\'; ></a></div>";
                        }
                        else 
                        {
                            retorno.P3 = "<div title=\"Incluir " + qTipoProfessor + "\"> <a class=\"btn btn-success  btn-circle  fa fa-plus\" href=\'javascript:fIncluirProfessorBanca(\""
                            + listaBanca.ElementAt(0).id_banca.ToString() + "\",\""
                            + elemento.id_professor.ToString() + "\",\""
                            + elemento.cpf + "\",\""
                            + elemento.nome + "\",\""
                            + qTipoProfessor + "\",\""
                            + qBanca + "\""
                            + ")\'; ></a></div>";
                        }
                        
                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fAlterarOrientadorBanca()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdBanca = HttpContext.Current.Request["qIdBanca"];
                string qIdProfessor = HttpContext.Current.Request["qIdProfessor"];
                string qCpf = HttpContext.Current.Request["qCpf"];
                string qNome = HttpContext.Current.Request["qNome"];
                string qBanca = HttpContext.Current.Request["qBanca"];

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                banca_professores pItem = new banca_professores();

                pItem.id_banca = Convert.ToInt32(qIdBanca);
                pItem.id_professor = Convert.ToInt32(qIdProfessor);
                pItem.tipo_professor = "Orientador";
                pItem.status = "alterado";
                pItem.data_alteracao = DateTime.Now;
                pItem.usuario = usuario.usuario;
                if (aplicacaoAluno.AlterarOrientadorBanca(pItem))
                {
                    item = aplicacaoAluno.BuscaItem(item);
                    Session[qTab + "Aluno"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na alteração do Orientador da banca de " + qBanca + ". Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fIncluirProfessorBanca()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdBanca = HttpContext.Current.Request["qIdBanca"];
                string qIdProfessor = HttpContext.Current.Request["qIdProfessor"];
                string qCpf = HttpContext.Current.Request["qCpf"];
                string qNome = HttpContext.Current.Request["qNome"];
                string qTipoProfessor = HttpContext.Current.Request["qTipoProfessor"];
                string qBanca = HttpContext.Current.Request["qBanca"];

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                banca_professores pItem = new banca_professores();

                pItem.id_banca = Convert.ToInt32(qIdBanca);
                pItem.id_professor = Convert.ToInt32(qIdProfessor);
                pItem.tipo_professor = qTipoProfessor;
                pItem.status = "cadastrado";
                pItem.data_cadastro = DateTime.Now;
                pItem.data_alteracao = pItem.data_cadastro;
                pItem.usuario = usuario.usuario;
                pItem.imprimir = true;

                if (aplicacaoAluno.IncluirProfessorBanca(pItem))
                {
                    item = aplicacaoAluno.BuscaItem(item);
                    Session[qTab + "Aluno"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na alteração do Orientador da banca de " + qBanca + ". Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fExcluirProfessorBanca()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdBanca = HttpContext.Current.Request["qIdBanca"];
                string qIdProfessor = HttpContext.Current.Request["qIdProfessor"];
                string qNomeProfessor = HttpContext.Current.Request["qNome"];
                string qTipoProfessor = HttpContext.Current.Request["qTipoProfessor"];
                string qBanca = HttpContext.Current.Request["qBanca"];

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                banca_professores pItem = new banca_professores();

                pItem.id_banca = Convert.ToInt32(qIdBanca);
                pItem.id_professor = Convert.ToInt32(qIdProfessor);
                //pItem.tipo_professor = qTipoProfessor;
                //pItem.status = "cadastrado";
                //pItem.data_cadastro = DateTime.Now;
                //pItem.data_alteracao = pItem.data_cadastro;
                //pItem.usuario = usuario.usuario;
                //pItem.imprimir = true;

                if (aplicacaoAluno.ExcluirProfessorBanca(pItem))
                {
                    item = aplicacaoAluno.BuscaItem(item);
                    Session[qTab + "Aluno"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na alteração do " + qTipoProfessor + " da banca de " + qBanca + ". Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fImprimirCoorientadorBanca()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdBanca = HttpContext.Current.Request["qIdBanca"];
                string qIdProfessor = HttpContext.Current.Request["qIdProfessor"];
                string qImprimir = HttpContext.Current.Request["qImprimir"];
                string qBanca = HttpContext.Current.Request["qBanca"];

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                banca_professores pItem = new banca_professores();

                pItem.id_banca = Convert.ToInt32(qIdBanca);
                pItem.id_professor = Convert.ToInt32(qIdProfessor);
                //pItem.tipo_professor = qTipoProfessor;
                //pItem.status = "cadastrado";
                //pItem.data_cadastro = DateTime.Now;
                //pItem.data_alteracao = pItem.data_cadastro;
                //pItem.usuario = usuario.usuario;
                if (qImprimir == "true")
                {
                    pItem.imprimir = true;
                }
                else
                {
                    pItem.imprimir = false;
                }

                if (aplicacaoAluno.ImprimirProfessorBanca(pItem))
                {
                    item = aplicacaoAluno.BuscaItem(item);
                    Session[qTab + "Aluno"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na alteração do status 'imprimir'. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheComboContrato()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdTurma = HttpContext.Current.Request["qIdTurma"];

                matricula_turma item_Matricula = new matricula_turma();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];


                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                List<dados_contratos> ListaContrato;

                item_Matricula = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).FirstOrDefault();

                if (item_Matricula.turmas.cursos.id_tipo_curso == 1 || item_Matricula.turmas.cursos.id_tipo_curso == 2 || item_Matricula.turmas.id_curso == 5 || item_Matricula.turmas.cursos.id_tipo_curso == 3 || item_Matricula.turmas.cursos.id_tipo_curso == 4 )
                {
                    ListaContrato = aplicacaoGerais.ListaContrato(item_Matricula.turmas.id_curso);
                }
                else
                {
                    ListaContrato = aplicacaoGerais.ListaContrato(0);
                }

                List<retornoCombo> listaRetorno = new List<retornoCombo>();

                if (ListaContrato.Count > 0)
                {
                    retornoCombo retorno;
                    foreach (var elemento in ListaContrato)
                    {
                       
                        retorno = new retornoCombo();
                        retorno.id = elemento.nome_contrato;
                        retorno.text = elemento.nome_contrato;

                        listaRetorno.Add(retorno);

                    }
                }
                else
                {
                    retornoCombo retorno;
                    retorno = new retornoCombo();
                    retorno.id = item_Matricula.turmas.cursos.nome;
                    retorno.text = retorno.id;

                    listaRetorno.Add(retorno);
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheContrato()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdTurma = HttpContext.Current.Request["qIdTurma"];
                string qContrato = HttpContext.Current.Request["qContrato"];

                matricula_turma item_Matricula = new matricula_turma();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                dados_contratos Item_Contrato = new dados_contratos();

                item_Matricula = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).FirstOrDefault();
                Item_Contrato.nome_contrato = qContrato;

                Item_Contrato = aplicacaoGerais.BuscaContrato(Item_Contrato);
                

                List<retornoGeral> listaRetorno = new List<retornoGeral>();


                retornoGeral retorno = new retornoGeral();
                if (Item_Contrato != null)
                {
                    retorno.P0 = String.Format("{0:yyyy-MM-dd}", DateTime.Today);
                    retorno.P1 = Item_Contrato.valor_total.ToString();
                    retorno.P2 = Item_Contrato.valor_disciplina.ToString();
                    retorno.P3 = Item_Contrato.num_parcelas.ToString();
                    retorno.P4 = Item_Contrato.valor_parcela.ToString();
                    retorno.P5 = String.Format("{0:yyyy-MM-dd}", item_Matricula.turmas.data_inicio);
                    retorno.P6 = Item_Contrato.prazo.ToString();
                    retorno.P7 = Item_Contrato.coordenador;
                    retorno.P8 = Item_Contrato.secretaria;
                    retorno.P9 = Item_Contrato.testemunha_1;
                    retorno.P10 = Item_Contrato.rg_testemunha_1;
                    retorno.P11 = Item_Contrato.testemunha_2;
                    retorno.P12 = Item_Contrato.rg_testemunha_2;
                    retorno.P13 = Item_Contrato.diretor;
                }
                else
                {
                    retorno.P0 = "";
                    retorno.P1 = "";
                    retorno.P2 = "";
                    retorno.P3 = "";
                    retorno.P4 = "";
                    retorno.P5 = "";
                    retorno.P6 = "";
                    retorno.P7 = "";
                    retorno.P8 = "";
                    retorno.P9 = "";
                    retorno.P10 = "";
                    retorno.P11 = "";
                    retorno.P12 = "";
                    retorno.P13 = "";
                }

                listaRetorno.Add(retorno);

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }
        //== Fim Aba Contrato==========================================================

        //== Início Aba Reunião CPG==========================================================
        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheProrrogacaoCPG()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdTurma = HttpContext.Current.Request["qIdTurma"];

                List<matricula_turma> lista = new List<matricula_turma>();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                lista = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).ToList();
                List<retornoGeral> listaRetorno = new List<retornoGeral>();
                string qTipoReuniao;

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista.ToList())
                    {
                        foreach (var elemento2 in elemento.prorrogacao.OrderBy(x => x.data_inicio).ThenBy(x => x.data_fim))
                        {
                            retorno = new retornoGeral();
                            retorno.P0 = elemento2.id_reuniao.ToString();
                            retorno.P1 = String.Format("{0:dd/MM/yyyy}", elemento2.data_inicio);
                            retorno.P2 = String.Format("{0:dd/MM/yyyy}", elemento2.data_fim);
                            retorno.P3 = elemento2.parecer;
                            retorno.P4 = String.Format("{0:dd/MM/yyyy}", elemento2.data_deposito);
                            retorno.P5 = elemento2.observacao;

                            if (elemento2.id_tipo_reuniao_cpg == null)
                            {
                                qTipoReuniao = "";
                            }
                            else
                            {
                                qTipoReuniao = elemento2.tipo_reuniao_cpg.tipo_reuniao_cpg1;
                            }

                            retorno.P6 = "<div title=\"Editar\"> <a class=\"btn btn-primary btn-circle fa fa-edit\" href=\'javascript:fModalEditarProrrogacaoCPG(\""
                            + elemento2.id_prorrogacao + "\",\"" + elemento2.id_reuniao + "\",\"" + String.Format("{0:yyyy-MM-dd}", elemento2.data_inicio) + "\","
                            + "\"" + String.Format("{0:yyyy-MM-dd}", elemento2.data_fim) + "\","
                            + "\"" + elemento2.parecer + "\","
                            + "\"" + String.Format("{0:yyyy-MM-dd}", elemento2.data_deposito) + "\","
                            + "\"" + elemento2.observacao + "\","
                            + "\"" + elemento2.id_tipo_reuniao_cpg + "\""
                            + ")\'; ></a></div>";

                            retorno.P7 = "<div title=\"Apagar\"> <a class=\"btn btn-danger btn-circle fa fa-eraser\" href=\'javascript:fModalApagarProrrogacaoCPG(\""
                            + elemento2.id_prorrogacao + "\",\"" + elemento2.id_reuniao + "\",\"" + String.Format("{0:yyyy-MM-dd}", elemento2.data_inicio) + "\","
                            + "\"" + String.Format("{0:yyyy-MM-dd}", elemento2.data_fim) + "\","
                            + "\"" + elemento2.parecer + "\","
                            + "\"" + String.Format("{0:yyyy-MM-dd}", elemento2.data_deposito) + "\","
                            + "\"" + elemento2.observacao + "\","
                            + "\"" + elemento2.id_tipo_reuniao_cpg + "\""
                            + ")\'; ></a></div>";

                            retorno.P8 = qTipoReuniao;
                            retorno.P9 = String.Format("{0:dd/MM/yyyy}", elemento2.data_cadastro);

                            //====================
                            if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 52).FirstOrDefault().modificacao == true)
                            //Se não tiver direito avida ao ajax
                            {
                                retorno.P10 = "1";
                            }
                            else
                            {
                                retorno.P10 = "0";
                            }

                            listaRetorno.Add(retorno);
                        }

                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fInserirReuniaoCPG()
        {
            Session.Timeout = 60;
            try
            {
                string json = "";
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!(usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 52)))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdTurma = HttpContext.Current.Request["qIdTurma"];
                string qIdReuniao = HttpContext.Current.Request["qIdReuniao"];
                string qidTipoReuniao = HttpContext.Current.Request["qidTipoReuniao"];
                string qParecer = HttpContext.Current.Request["qParecer"];
                string qDataInicio = HttpContext.Current.Request["qDataInicio"];
                string qDataFim = HttpContext.Current.Request["qDataFim"];
                string qDataDeposito = HttpContext.Current.Request["qDataDeposito"];
                string qObs = HttpContext.Current.Request["qObs"];
                string qTipoReuniao;
                if (qidTipoReuniao == "1") //Prorrogação ou trancamento
                {
                    qTipoReuniao = "Prorrogação CPG";
                }
                else if (qidTipoReuniao == "4") //Prorrogação ou trancamento
                {
                    qTipoReuniao = "Trancamento Especial";
                }
                else
                {
                    qTipoReuniao = "";
                }

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma item_matricula;
                item_matricula = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).FirstOrDefault();

                prorrogacao item_prorrogacao = new prorrogacao();

                item_prorrogacao.id_reuniao = Convert.ToInt32(qIdReuniao);
                item_prorrogacao.id_tipo_reuniao_cpg = Convert.ToInt32(qidTipoReuniao);
                item_prorrogacao.id_aluno = item.idaluno;
                item_prorrogacao.parecer = qParecer;
                if (qidTipoReuniao == "1" || qidTipoReuniao == "4") //Prorrogação ou trancamento
                {
                    item_prorrogacao.data_inicio = Convert.ToDateTime(qDataInicio);
                    item_prorrogacao.data_fim = Convert.ToDateTime(qDataFim);
                    if (qDataDeposito != "")
                    {
                        item_prorrogacao.data_deposito = Convert.ToDateTime(qDataDeposito);
                    }
                }
                item_prorrogacao.observacao = qObs;
                item_prorrogacao.status = "cadastrado";
                item_prorrogacao.data_cadastro = DateTime.Now;
                item_prorrogacao.data_alteracao = item_prorrogacao.data_cadastro;
                item_prorrogacao.usuario = usuario.usuario;
                item_prorrogacao.id_matricula_turma = item_matricula.id_matricula_turma;

                item_prorrogacao = aplicacaoAluno.CriarReuniaoCPG(item_prorrogacao);

                if ((qidTipoReuniao == "1" || qidTipoReuniao == "4") && qParecer == "Aceito") //Prorrogação ou Trancamento
                {
                    historico_matricula_turma itemHistoricoMatricula = new historico_matricula_turma();

                    bool bAchou;

                   if (item_matricula.historico_matricula_turma.Any(x => x.data_inicio == item_prorrogacao.data_inicio && x.data_fim == item_prorrogacao.data_fim))
                    {
                        itemHistoricoMatricula = item_matricula.historico_matricula_turma.Where(x => x.data_inicio == item_prorrogacao.data_inicio && x.data_fim == item_prorrogacao.data_fim && x.situacao == qTipoReuniao).SingleOrDefault();
                        bAchou = true;
                    }
                    else
                    {
                        bAchou = false;
                    }

                    itemHistoricoMatricula.id_matricula_turma = item_matricula.id_matricula_turma;
                    itemHistoricoMatricula.data_inicio = Convert.ToDateTime(qDataInicio);
                    itemHistoricoMatricula.data_fim = Convert.ToDateTime(qDataFim);
                    //itemHistoricoMatricula.data_previsao_termino = Convert.ToDateTime(HttpContext.Current.Request["qDataFim"]);
                    itemHistoricoMatricula.status = "Regular";
                    if (qidTipoReuniao == "1")
                    {
                        itemHistoricoMatricula.situacao = "Prorrogação CPG";
                    }
                    else
                    {
                        itemHistoricoMatricula.situacao = "Trancamento Especial";
                    }
                    itemHistoricoMatricula.ordem = 2;
                    itemHistoricoMatricula.data = DateTime.Now;
                    itemHistoricoMatricula.usuario = usuario.usuario;
                    itemHistoricoMatricula.id_prorrogacao = item_prorrogacao.id_prorrogacao;

                    if (bAchou)
                    {
                        aplicacaoAluno.EditarSituacaoHistorico(itemHistoricoMatricula);
                    }
                    else
                    {
                        aplicacaoAluno.CriarSituacaoHistorico(itemHistoricoMatricula);
                    }
                }

                item = aplicacaoAluno.BuscaItem(item);

                Session[qTab + "Aluno"] = item;

                json = "[{\"P0\":\"ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fEditarReuniaoCPG()
        {
            Session.Timeout = 60;
            try
            {
                string json = "";
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 52).FirstOrDefault().modificacao == true)
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdProrrogacao = HttpContext.Current.Request["qIdProrrogacao"];
                string qIdTurma = HttpContext.Current.Request["qIdTurma"];
                string qIdReuniao = HttpContext.Current.Request["qIdReuniao"];
                string qidTipoReuniao = HttpContext.Current.Request["qidTipoReuniao"];
                string qParecer = HttpContext.Current.Request["qParecer"];
                string qDataInicio = HttpContext.Current.Request["qDataInicio"];
                string qDataFim = HttpContext.Current.Request["qDataFim"];
                string qDataDeposito = HttpContext.Current.Request["qDataDeposito"];
                string qObs = HttpContext.Current.Request["qObs"];
                string qTipoReuniao;
                if (qidTipoReuniao == "1") //Prorrogação ou trancamento
                {
                    qTipoReuniao = "Prorrogação CPG";
                }
                else if (qidTipoReuniao == "4") //Prorrogação ou trancamento
                {
                    qTipoReuniao = "Trancamento Especial";
                }
                else
                {
                    qTipoReuniao = "";
                }

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;

                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma item_matricula;
                item_matricula = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).FirstOrDefault();

                prorrogacao item_prorrogacao = new prorrogacao();
                historico_matricula_turma itemHistoricoMatricula = new historico_matricula_turma();

                item_prorrogacao.id_prorrogacao = Convert.ToInt32(qIdProrrogacao);
                item_prorrogacao.id_reuniao = Convert.ToInt32(qIdReuniao);
                item_prorrogacao.id_tipo_reuniao_cpg = Convert.ToInt32(qidTipoReuniao);
                item_prorrogacao.id_aluno = item.idaluno;
                item_prorrogacao.parecer = qParecer;
                if (qidTipoReuniao == "1" || qidTipoReuniao == "4") //Prorrogação ou trancamento
                {
                    item_prorrogacao.data_inicio = Convert.ToDateTime(qDataInicio);
                    item_prorrogacao.data_fim = Convert.ToDateTime(qDataFim);
                    if (qDataDeposito != "")
                    {
                        item_prorrogacao.data_deposito = Convert.ToDateTime(qDataDeposito);
                    }
                }
                item_prorrogacao.observacao = qObs;
                item_prorrogacao.status = "alterado";
                //item_prorrogacao.data_cadastro = DateTime.Now;
                item_prorrogacao.data_alteracao = DateTime.Now;
                item_prorrogacao.usuario = usuario.usuario;
                item_prorrogacao.id_matricula_turma = item_matricula.id_matricula_turma;

                item_prorrogacao = aplicacaoAluno.EditarReuniaoCPG(item_prorrogacao);

                if ((qidTipoReuniao == "1" || qidTipoReuniao == "4")) //Prorrogação ou Trancamento
                {
                    bool bAchou;

                    if (item_matricula.historico_matricula_turma.Any(x => x.id_prorrogacao == item_prorrogacao.id_prorrogacao))
                    {
                        itemHistoricoMatricula = item_matricula.historico_matricula_turma.Where(x => x.id_prorrogacao == item_prorrogacao.id_prorrogacao).SingleOrDefault();
                        bAchou = true;
                    }
                    else if (item_matricula.historico_matricula_turma.Any(x => x.data_inicio == item_prorrogacao.data_inicio && x.data_fim == item_prorrogacao.data_fim && x.situacao == qTipoReuniao))
                    {
                        itemHistoricoMatricula = item_matricula.historico_matricula_turma.Where(x => x.data_inicio == item_prorrogacao.data_inicio && x.data_fim == item_prorrogacao.data_fim && x.situacao == qTipoReuniao).SingleOrDefault();
                        bAchou = true;
                    }
                    else
                    {
                        bAchou = false;
                    }

                    itemHistoricoMatricula.id_matricula_turma = item_matricula.id_matricula_turma;
                    itemHistoricoMatricula.data_inicio = Convert.ToDateTime(qDataInicio);
                    itemHistoricoMatricula.data_fim = Convert.ToDateTime(qDataFim);
                    //itemHistoricoMatricula.data_previsao_termino = Convert.ToDateTime(HttpContext.Current.Request["qDataFim"]);
                    itemHistoricoMatricula.status = "Regular";
                    if (qidTipoReuniao == "1")
                    {
                        itemHistoricoMatricula.situacao = "Prorrogação CPG";
                    }
                    else
                    {
                        itemHistoricoMatricula.situacao = "Trancamento Especial";
                    }
                    itemHistoricoMatricula.ordem = 2;
                    itemHistoricoMatricula.data = DateTime.Now;
                    itemHistoricoMatricula.usuario = usuario.usuario;
                    itemHistoricoMatricula.id_prorrogacao = item_prorrogacao.id_prorrogacao;

                    if (qParecer == "Aceito")
                    {
                        if (bAchou)
                        {
                            aplicacaoAluno.EditarSituacaoHistorico(itemHistoricoMatricula);
                        }
                        else
                        {
                            aplicacaoAluno.CriarSituacaoHistorico(itemHistoricoMatricula);
                        }
                    }
                    else
                    {
                        if (bAchou)
                        {
                            aplicacaoAluno.ApagarSituacaoHistorico(itemHistoricoMatricula);
                        }
                    }
                }

                item = aplicacaoAluno.BuscaItem(item);

                Session[qTab + "Aluno"] = item;

                json = "[{\"P0\":\"ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fApagarReuniaoCPG()
        {
            Session.Timeout = 60;
            try
            {
                string json = "";
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 52).FirstOrDefault().modificacao == true)
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdProrrogacao = HttpContext.Current.Request["qIdProrrogacao"];
                string qIdTurma = HttpContext.Current.Request["qIdTurma"];
                string qIdReuniao = HttpContext.Current.Request["qIdReuniao"];
                string qidTipoReuniao = HttpContext.Current.Request["qidTipoReuniao"];
                string qParecer = HttpContext.Current.Request["qParecer"];
                string qDataInicio = HttpContext.Current.Request["qDataInicio"];
                string qDataFim = HttpContext.Current.Request["qDataFim"];
                string qDataDeposito = HttpContext.Current.Request["qDataDeposito"];
                string qObs = HttpContext.Current.Request["qObs"];
                string qTipoReuniao;
                if (qidTipoReuniao == "1") //Prorrogação ou trancamento
                {
                    qTipoReuniao = "Prorrogação CPG";
                }
                else if (qidTipoReuniao == "4") //Prorrogação ou trancamento
                {
                    qTipoReuniao = "Trancamento Especial";
                }
                else
                {
                    qTipoReuniao = "";
                }

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma item_matricula;
                item_matricula = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).FirstOrDefault();

                prorrogacao item_prorrogacao = new prorrogacao();
                historico_matricula_turma itemHistoricoMatricula = new historico_matricula_turma();



                item_prorrogacao.id_prorrogacao = Convert.ToInt32(qIdProrrogacao);
                item_prorrogacao.id_reuniao = Convert.ToInt32(qIdReuniao);
                item_prorrogacao.id_tipo_reuniao_cpg = Convert.ToInt32(qidTipoReuniao);
                item_prorrogacao.id_aluno = item.idaluno;
                item_prorrogacao.parecer = qParecer;
                if (qidTipoReuniao == "1" || qidTipoReuniao == "4") //Prorrogação ou trancamento
                {
                    item_prorrogacao.data_inicio = Convert.ToDateTime(qDataInicio);
                    item_prorrogacao.data_fim = Convert.ToDateTime(qDataFim);
                    if (qDataDeposito != "")
                    {
                        item_prorrogacao.data_deposito = Convert.ToDateTime(qDataDeposito);
                    }
                }
                item_prorrogacao.observacao = qObs;
                item_prorrogacao.status = "alterado";
                //item_prorrogacao.data_cadastro = DateTime.Now;
                item_prorrogacao.data_alteracao = DateTime.Now;
                item_prorrogacao.usuario = usuario.usuario;
                item_prorrogacao.id_matricula_turma = item_matricula.id_matricula_turma;


                if ((qidTipoReuniao == "1" || qidTipoReuniao == "4")) //Prorrogação ou Trancamento
                {

                    if (item_matricula.historico_matricula_turma.Any(x => x.id_prorrogacao == item_prorrogacao.id_prorrogacao))
                    {
                        itemHistoricoMatricula = item_matricula.historico_matricula_turma.Where(x => x.id_prorrogacao == item_prorrogacao.id_prorrogacao).SingleOrDefault();
                        aplicacaoAluno.ApagarSituacaoHistorico(itemHistoricoMatricula);
                    }
                    else if (item_matricula.historico_matricula_turma.Any(x => x.data_inicio == item_prorrogacao.data_inicio && x.data_fim == item_prorrogacao.data_fim && x.situacao == qTipoReuniao))
                    {
                        itemHistoricoMatricula = item_matricula.historico_matricula_turma.Where(x => x.data_inicio == item_prorrogacao.data_inicio && x.data_fim == item_prorrogacao.data_fim && x.situacao == qTipoReuniao).SingleOrDefault();
                        aplicacaoAluno.ApagarSituacaoHistorico(itemHistoricoMatricula);
                    }
                    
                }

                aplicacaoAluno.ApagarReuniaoCPG(item_prorrogacao);

                item = aplicacaoAluno.BuscaItem(item);

                Session[qTab + "Aluno"] = item;

                json = "[{\"P0\":\"ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }
        //== Fim Aba Reunião CPG==========================================================

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheDataLimiteDocumentacao()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                //string qIdTurma = HttpContext.Current.Request["qIdTurma"];

                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (item != null)
                {
                    if (item.alunos_dataLimite_documentos_pendentes.Count > 0)
                    {
                        retornoGeral retorno;
                        foreach (var elemento in item.alunos_dataLimite_documentos_pendentes.OrderByDescending(x => x.data_limite))
                        {
                            retorno = new retornoGeral();
                            retorno.P0 = String.Format("{0:dd/MM/yyyy}", elemento.data_cadastro);
                            retorno.P1 = elemento.observacao;
                            retorno.P2 = elemento.usuario;
                            retorno.P3 = String.Format("{0:dd/MM/yyyy}", elemento.data_limite);
                            if (elemento.data_limite < DateTime.Today && !item.alunos_dataLimite_documentos_pendentes.Any(x => x.tipo_registro == 1))
                            {
                                retorno.P4 = "true";
                            }
                            else
                            {
                                retorno.P4 = "false";
                            }

                            listaRetorno.Add(retorno);
                        }
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fIncluirDataLimiteDocumentacao()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qDataLimite = HttpContext.Current.Request["qDataLimite"];
                string qObsDataLimite = HttpContext.Current.Request["qObsDataLimite"];

                TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request["qTab"];
                item = (alunos)Session[qTab + "Aluno"];

                alunos_dataLimite_documentos_pendentes pItem = new alunos_dataLimite_documentos_pendentes();

                pItem.data_limite = Convert.ToDateTime(qDataLimite);
                pItem.observacao = qObsDataLimite;
                pItem.data_cadastro = DateTime.Now;
                pItem.usuario = usuario.usuario;
                pItem.idaluno = item.idaluno;
                pItem.tipo_registro = 1;

                pItem = aplicacaoTurma.IncluirAluno_DataLimiteDocumento(pItem);


                if (pItem != null)
                {
                    item.alunos_dataLimite_documentos_pendentes.Add(pItem);
                    Session[qTab + "Aluno"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na inclusão da Nova Data Limite para entrega da documentação. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //==========================================================
        //cadAreaConcentracaoGestao
        //==========================================================

        //cadAreaConcentracaoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheCoordenador()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                AreaAplicacao aplicacaoArea = new AreaAplicacao();
                areas_concentracao item;
                item = (areas_concentracao)Session["areas_concentracao"];
                List<areas_concentracao_coordenadores> lista = new List<areas_concentracao_coordenadores>();
                lista = aplicacaoArea.ListaCoordenadores(item);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.professores.id_professor.ToString();
                        retorno.P1 = elemento.professores.cpf;
                        retorno.P2 = elemento.professores.nome;
                        retorno.P3 = ("<div title=\"Excluir Coordenador\"> <a class=\"btn btn-danger  btn-circle  fa fa-eraser\" href=\'javascript:AbreModalApagarCoordenador(\""
                        + (elemento.professores.id_professor.ToString() + ("\",\""
                        + (elemento.professores.cpf + ("\",\""
                        + (elemento.professores.nome + "\")\'; ></a></div>"))))));

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAreaConcentracaoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPerquisaCoordenador()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string sCPF = HttpContext.Current.Request["qCPF"];
                string sNome = HttpContext.Current.Request["qNome"];

                AreaAplicacao aplicacaoArea = new AreaAplicacao();
                areas_concentracao item;
                professores pItemProfessor = new professores();
                item = (areas_concentracao)Session["areas_concentracao"];
                pItemProfessor.cpf = sCPF.Trim();
                pItemProfessor.nome = sNome.Trim();
                List<professores> lista = new List<professores>();
                lista = aplicacaoArea.ListaCoordenadoresDisponiveis(item, pItemProfessor);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_professor.ToString();
                        retorno.P1 = elemento.nome;
                        retorno.P2 = elemento.cpf;
                        retorno.P3 = ("<div title=\"Incluir Coordenador\"> <a class=\"btn btn-success  btn-circle  fa fa-plus\" href=\'javascript:fIncluiCoordenador(\""
                        + (elemento.id_professor.ToString() + ("\",\""
                        + (elemento.cpf + ("\",\""
                        + (elemento.nome + "\")\'; ></a></div>"))))));

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAreaConcentracaoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fIncluiCoordenador()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];

                AreaAplicacao aplicacaoArea = new AreaAplicacao();
                areas_concentracao item;
                areas_concentracao_coordenadores pItem = new areas_concentracao_coordenadores();
                item = (areas_concentracao)Session["areas_concentracao"];
                pItem.id_area_concentracao = item.id_area_concentracao;
                pItem.id_professor = Convert.ToInt32(qId);
                pItem.status = "cadastrado";
                pItem.data_cadastro = DateTime.Now;
                pItem.data_alteracao = pItem.data_cadastro;
                pItem.usuario = usuario.usuario;
                if (aplicacaoArea.IncluirCoordenador(pItem))
                {
                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na inclusão do Coordenador. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAreaConcentracaoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fExcluiCoordenador()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];

                AreaAplicacao aplicacaoArea = new AreaAplicacao();
                areas_concentracao item;
                areas_concentracao_coordenadores pItem = new areas_concentracao_coordenadores();
                item = (areas_concentracao)Session["areas_concentracao"];
                pItem.id_area_concentracao = item.id_area_concentracao;
                pItem.id_professor = Convert.ToInt32(qId);
                if (aplicacaoArea.ExcluirCoordenador(pItem))
                {
                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na exclusão do Coordenador. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadAreaConcentracaoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public string fAtivarInativarArea(string qOperacao)
        {
            Session.Timeout = 60;
            try
            {
                string json = "";
                string sAux = "";
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"Retorno\":\"deslogado\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    return json;
                }

                AreaAplicacao aplicacaoArea = new AreaAplicacao();
                areas_concentracao item = (areas_concentracao)Session["areas_concentracao"];

                foreach (var item2 in item.disciplinas)
                {
                    if (item2.status != "inativado")
                    {
                        if (sAux == "")
                        {
                            sAux = "Não é possível inativar essa Área de Concentração pois há disciplinas ativas associadas: <br><br>";
                        }
                        sAux = sAux + "<strong>" + item2.nome + "</strong><br>";
                    }
                }
                if (sAux == "")
                {
                    if (qOperacao == "Ativar")
                    {
                        item.status = "alterado";
                    }
                    else
                    {
                        item.status = "inativado";
                    }
                    item.usuario = usuario.usuario;

                    item = aplicacaoArea.AlterarStatus(item);

                    Session["areas_concentracao"] = item;

                    json = "[{\"Retorno\":\"ok\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    return json;
                }
                else
                {
                    json = "[{\"Retorno\":\"erro\",\"Resposta\":\"" + sAux + "\",\"P2\":\"\",\"P3\":\"\"}]";
                    return json;
                } 
            }
            catch (Exception ex)
            {
                string json = "[{\"Retorno\":\"erro\",\"Resposta\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                return json;
            }

        }

        //==========================================================

        //cadProfessorGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPerquisaEmpresa()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string sCNPJ = HttpContext.Current.Request["qCNPJ"];
                string sNome = HttpContext.Current.Request["qNome"];

                FornecedorAplicacao aplicacaoFornecedor = new FornecedorAplicacao();
                fornecedores item = new fornecedores();
                item.cnpj = sCNPJ.Trim();
                item.nome = sNome.Trim();
                List<fornecedores> lista = new List<fornecedores>();
                lista = aplicacaoFornecedor.ListaItem(item);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_fornecedor.ToString();
                        retorno.P1 = elemento.nome;
                        retorno.P2 = elemento.cnpj;
                        retorno.P3 = ("<div title=\"Associar Empresa\"> <a class=\"btn btn-success  btn-circle  fa fa-plus\" href=\'javascript:fAssociarEmpresa(\""
                        + (elemento.id_fornecedor.ToString() + ("\",\""
                        + (elemento.cnpj + ("\",\""
                        + (elemento.nome + "\")\'; ></a></div>"))))));

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadProfessorGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public string fAssociarEmpresa(int qIdEmpresa)
        {
            Session.Timeout = 60;
            try
            {
                FornecedorAplicacao aplicacaoFornecedor = new FornecedorAplicacao();
                fornecedores item = new fornecedores();
                item = aplicacaoFornecedor.BuscaItem(qIdEmpresa);
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                List<fornecedores> lista = new List<fornecedores>();
                lista.Add(item);

                var listaDisciplina = from item2 in lista
                                      select new
                                      {
                                          IdEmpresa = item2.id_fornecedor,
                                          NomeEmpresa = item2.nome,
                                          CNPJEmpresa = item2.cnpj,
                                          IEEmpresa = item2.inscricao_estadual,
                                          CargoEmpresa = item2.cargo,
                                          CEPEmpresa = item2.cep_end,
                                          LogradouroEmpresa = item2.logradouro_end,
                                          NumeroEmpresa = item2.numero_end,
                                          ComplementoEmpresa = item2.comp_end,
                                          BairroEmpresa = item2.bairro_end,
                                          CidadeEmpresa = item2.cidade_end,
                                          EstadoEmpresa = item2.uf_end,
                                          TelefoneEmpresa = item2.tel_contato,
                                          CelularEmpresa = item2.cel_contato,
                                          FaxEmpresa = item2.fax_contato
                                      };

                string json = jsSerializer.Serialize(listaDisciplina);
                return json;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //cadProfessorGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public string fAlteraFotoProfessor(string iFoto)
        {
            Session.Timeout = 60;
            try
            {
                UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                usuarios itemUsuario_Logado = new usuarios();
                usuarios item_Usuario_Professor = new usuarios();
                itemUsuario_Logado = (usuarios)Session["UsuarioLogado"];
                professores item = (professores)Session["professores"];
                item_Usuario_Professor.usuario = item.cpf.ToString();
                item_Usuario_Professor = aplicacaoUsuario.BuscaUsuario(item_Usuario_Professor);
                string sCaminho;

                if (item_Usuario_Professor != null)
                {
                    Byte[] imageBytes;
                    imageBytes = Convert.FromBase64String(iFoto.Replace("data:image/jpeg;base64,", ""));
                    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                    ms.Write(imageBytes, 0, imageBytes.Length);
                    Image Foto = Image.FromStream(ms, true);

                    sCaminho = (Server.MapPath("img") + ("\\pessoas\\"));

                    if (Foto != null)
                    {
                        if ((System.IO.Directory.Exists((Server.MapPath("img") + ("\\pessoas"))) == false))
                        {
                            System.IO.Directory.CreateDirectory((Server.MapPath("img") + ("\\pessoas")));
                        }

                        sCaminho = (Server.MapPath("img") + ("\\pessoas\\"));


                        // Validate the uploaded image(optional)

                        // Get the complete file path
                        var fileSavePath = Path.Combine(sCaminho, item.cpf.ToString() + ".jpg");

                        // Save the uploaded file to "UploadedFiles" folder
                        Foto.Save(fileSavePath);

                        item_Usuario_Professor.avatar = item.cpf.ToString() + ".jpg";
                        aplicacaoUsuario.AlterarUsuario(item_Usuario_Professor);
                    }

                    //int i;
                    //string[] arr1;
                    //HttpFileCollection Files;
                    //HttpPostedFile memFile;

                    //Files = HttpContext.Current.Request.Files;
                    ////  Load File collection into HttpFileCollection variable.



                    //if (HttpContext.Current.Request.Files.AllKeys.Any())
                    //{
                    //    // Get the uploaded image from the Files collection
                    //    var httpPostedFile = HttpContext.Current.Request.Files["UploadedFile"];

                    //    if (httpPostedFile != null)
                    //    {
                    //        if ((System.IO.Directory.Exists((Server.MapPath("img") + ("\\pessoas"))) == false))
                    //        {
                    //            System.IO.Directory.CreateDirectory((Server.MapPath("img") + ("\\pessoas")));
                    //        }

                    //        sCaminho = (Server.MapPath("img") + ("\\pessoas\\"));


                    //        // Validate the uploaded image(optional)

                    //        // Get the complete file path
                    //        var fileSavePath = Path.Combine(sCaminho, aluno.idaluno.ToString() + ".jpg");

                    //        // Save the uploaded file to "UploadedFiles" folder
                    //        httpPostedFile.SaveAs(fileSavePath);

                    //        UsuarioAlteracao.avatar = aluno.idaluno.ToString() + ".jpg";
                    //        aplicacaoUsuario.AlterarUsuario(UsuarioAlteracao);
                    //    }
                    //}





                    //if ((Files.Count > 0))
                    //{
                    //    if ((System.IO.Directory.Exists((Server.MapPath("img") + ("\\pessoas"))) == false))
                    //    {
                    //        System.IO.Directory.CreateDirectory((Server.MapPath("img") + ("\\pessoas")));
                    //    }

                    //    sCaminho = (Server.MapPath("img") + ("\\pessoas\\"));
                    //    arr1 = Files.AllKeys;
                    //    //  This will get keys (not file names as incorrectlystated in MSDN article) of all files into a string array.
                    //    string strFname;
                    //    for (i = 0; (i <= arr1.GetUpperBound(0)); i++)
                    //    {
                    //        memFile = HttpContext.Current.Request.Files[arr1[i]];
                    //        strFname = memFile.FileName;
                    //        memFile.SaveAs((sCaminho + System.IO.Path.GetFileName(aluno.idaluno.ToString() + ".jpg")));//HttpContext.Current.Items["DescricaoArquivo"].ToString()
                    //        UsuarioAlteracao.avatar = aluno.idaluno.ToString() + ".jpg";
                    //        aplicacaoUsuario.AlterarUsuario(UsuarioAlteracao);
                    //    }
                    //}

                    //return Json({isValid: true}, JsonRequestBehavior.AllowGet);
                    //return new AjaxReturnObject(0, "");

                    return "[{\"Retorno\":\"ok\"}]";

                }
                else
                {
                    return "[{\"Retorno\":\"A sessão do professor está vazia.\"}]";
                }

                //return "[{\"Retorno\":\"ok\"}]";

            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                return "[{\"Retorno\":\"" + ex.Message + "\"}]";
            }

        }

        //cadProfessorGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public string fAtivarInativarProfessor(string qOperacao)
        {
            Session.Timeout = 60;
            try
            {
                string json = "";
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"Retorno\":\"deslogado\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    return json;
                }

                ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
                professores item = (professores)Session["professores"];
                if (qOperacao == "Ativar")
                {
                    item.status = "alterado";
                }
                else
                {
                    item.status = "inativado";
                    if (item.presenca_professor.Any(x => x.presente == false))
                    {
                        var qDatas = item.presenca_professor.Where(x => x.presente == false).Select(x => x.datas_aulas.data_aula).ToArray();
                        string qData = "";
                        foreach (var elemento in qDatas)
                        {
                            qData = qData + elemento.Value.ToString("dd/MM/yyyy") + "<br>";
                        }
                        json = "[{\"Retorno\":\"erro\",\"Resposta\":\"Há aulas marcadas para esse professor, portanto não é possível realizar sua inativação.<br>" + qData  + "\",\"P2\":\"\",\"P3\":\"\"}]";
                        return json;
                    }
                }
                item.usuario = usuario.usuario;

                item = aplicacaoProfessor.AlterarStatus(item);

                Session["professores"] = item;

                json = "[{\"Retorno\":\"ok\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                return json;

            }
            catch (Exception ex)
            {
                string json = "[{\"Retorno\":\"erro\",\"Resposta\":\"Há aulas marcadas para esse professor, portanto não é possível realizar sua inativação.\",\"P2\":\"\",\"P3\":\"\"}]";
                return json;
            }

        }

        //cadDisciplinaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public string fAtivarInativarDisciplina(string qOperacao)
        {
            Session.Timeout = 60;
            try
            {
                string json = "";
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"Retorno\":\"deslogado\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    return json;
                }

                DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                disciplinas item = (disciplinas)Session["disciplinas"];
                if (qOperacao == "Ativar")
                {
                    item.status = "alterado";
                }
                else
                {
                    item.status = "inativado";
                }
                item.usuario = usuario.usuario;

                item = aplicacaoDisciplina.AlterarStatus(item);

                Session["professores"] = item;

                json = "[{\"Retorno\":\"ok\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                return json;

            }
            catch (Exception ex)
            {
                string json = "[{\"Retorno\":\"erro\",\"Resposta\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                return json;
            }

        }

        //=================================== cadDisciplinaGestao ==== FIM ====

        //=================================== cadCursoGestao ==== INÍCIO ====

        //cadCursoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public string fAtivarInativarCurso(string qOperacao)
        {
            Session.Timeout = 60;
            try
            {
                string json = "";
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"Retorno\":\"deslogado\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    return json;
                }

                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos item = (cursos)Session["cursos"];
                if (qOperacao == "Ativar")
                {
                    item.status = "alterado";
                }
                else
                {
                    item.status = "inativado";
                }
                item.usuario = usuario.usuario;

                item = aplicacaoCurso.AlterarStatus(item);

                Session["cursos"] = item;

                json = "[{\"Retorno\":\"ok\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                return json;

            }
            catch (Exception ex)
            {
                string json = "[{\"Retorno\":\"erro\",\"Resposta\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                return json;
            }

        }

        //cadCursoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheCoordenadorCurso()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos item;
                item = (cursos)Session["cursos"];
                List<cursos_coordenadores> lista = new List<cursos_coordenadores>();
                lista = aplicacaoCurso.ListaCoordenadores(item);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                int i = 0;
                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.professores.id_professor.ToString();
                        retorno.P1 = elemento.professores.cpf;
                        retorno.P2 = elemento.professores.nome;
                        if (elemento.curso_tipo_coordenador != null)
                        {
                            retorno.P3 = elemento.curso_tipo_coordenador.descricao;
                        }
                        else
                        {
                            retorno.P3 = "";
                        }
                        retorno.P4 = ("<div title=\"Excluir Coordenador\"> <a class=\"btn btn-danger  btn-circle  fa fa-eraser\" href=\'javascript:AbreModalApagarCoordenador(\""
                        + (elemento.professores.id_professor.ToString() + ("\",\""
                        + (elemento.professores.cpf + ("\",\""
                        + (elemento.professores.nome + "\")\'; ></a></div>"))))));

                        retorno.P5 = i.ToString();
                        i++;
                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadCursoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPerquisaCoordenadorDisponivelCurso()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string sCPF = HttpContext.Current.Request["qCPF"];
                string sNome = HttpContext.Current.Request["qNome"];

                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos item;
                professores pItemProfessor = new professores();
                item = (cursos)Session["cursos"];
                pItemProfessor.cpf = sCPF.Trim();
                pItemProfessor.nome = sNome.Trim();
                List<professores> lista = new List<professores>();
                lista = aplicacaoCurso.ListaProfessoresDisponiveis(item, pItemProfessor);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_professor.ToString();
                        retorno.P1 = elemento.nome;
                        retorno.P2 = elemento.cpf;
                        retorno.P3 = ("<div title=\"Incluir Coordenador\"> <a class=\"btn btn-success  btn-circle  fa fa-plus\" href=\'javascript:fIncluiCoordenadorCurso(\""
                        + (elemento.id_professor.ToString() + ("\",\""
                        + (elemento.cpf + ("\",\""
                        + (elemento.nome + "\")\'; ></a></div>"))))));

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadCursoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fIncluiCoordenadorCurso()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];

                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos item;
                cursos_coordenadores pItem = new cursos_coordenadores();
                item = (cursos)Session["cursos"];
                pItem.id_curso = item.id_curso;
                pItem.id_professor = Convert.ToInt32(qId);
                pItem.status = "cadastrado";
                pItem.data_cadastro = DateTime.Now;
                pItem.data_alteracao = pItem.data_cadastro;
                pItem.usuario = usuario.usuario;
                pItem.id_tipo_coordenador = Convert.ToInt32(HttpContext.Current.Request["qTipo"]);
                if (aplicacaoCurso.IncluirCoordenador_Curso(pItem))
                {
                    Session["cursos"] = item;
                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na inclusão do Coordenador. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadCursoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fExcluiCoordenadorCurso()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];

                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos item;
                cursos_coordenadores pItem = new cursos_coordenadores();
                item = (cursos)Session["cursos"];
                pItem.id_curso = item.id_curso;
                pItem.id_professor = Convert.ToInt32(qId);
                if (aplicacaoCurso.ExcluirCoordenador(pItem) != null)
                {
                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na exclusão do Coordenador. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadCursoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheDisciplinaCurso()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos item;
                item = (cursos)Session["cursos"];
                List<cursos_disciplinas> lista = new List<cursos_disciplinas>();
                lista = aplicacaoCurso.ListaDisciplinasCursos(item);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    string sChecado;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_disciplina.ToString();
                        retorno.P1 = elemento.disciplinas.codigo;
                        retorno.P2 = elemento.disciplinas.nome;
                        
                        if (elemento.obrigatoria == 1)
                        {
                            sChecado = "checked";
                        }
                        else 
                        {
                            sChecado = "";
                        }

                        if (item.id_tipo_curso ==1)
                        {
                            retorno.P4 = "<span style=\"line-height: 2.2em;\"><label class=\"checkbox\"><input onclick =\"fCheckObrigatoria(this);\"  id = \"chkObrigatoria_" + elemento.id_curso.ToString() + "_" + elemento.id_disciplina.ToString() + "\" type=\"checkbox\" name=\"chkObrigatoria_" + elemento.id_curso.ToString() + "_" + elemento.id_disciplina.ToString() + "\" " + sChecado + " ><span></span></label></span>";
                        }
                        else
                        {
                            retorno.P4 = "nMestrado";
                        }

                        retorno.P3 = ("<div title=\"Excluir Disciplina\"> <a class=\"btn btn-danger  btn-circle  fa fa-eraser\" href=\'javascript:AbreModalApagarDisciplina(\""
                        + (elemento.id_disciplina.ToString() + ("\",\""
                        + (elemento.disciplinas.codigo + ("\",\""
                        + (elemento.disciplinas.nome + "\")\'; ></a></div>"))))));

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadCursoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPerquisaDisciplinaDisponivelCurso()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string sCodigo = HttpContext.Current.Request["qCodigo"];
                string sNome = HttpContext.Current.Request["qNome"];

                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos item;
                disciplinas pItemDisciplina = new disciplinas();
                item = (cursos)Session["cursos"];
                pItemDisciplina.codigo = sCodigo.Trim();
                pItemDisciplina.nome = sNome.Trim();
                List<disciplinas> lista = new List<disciplinas>();
                lista = aplicacaoCurso.ListaDisciplinasDisponiveis(item, pItemDisciplina);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_disciplina.ToString();
                        retorno.P1 = elemento.nome;
                        retorno.P2 = elemento.codigo;
                        retorno.P3 = ("<div title=\"Incluir Disciplina\"> <a class=\"btn btn-success  btn-circle  fa fa-plus\" href=\'javascript:fIncluiDisciplinaCurso(\""
                        + (elemento.id_disciplina.ToString() + ("\",\""
                        + (elemento.codigo + ("\",\""
                        + (elemento.nome + "\")\'; ></a></div>"))))));

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadCursoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fIncluiDisciplinaCurso()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];

                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos item;
                cursos_disciplinas pItem = new cursos_disciplinas();
                item = (cursos)Session["cursos"];
                pItem.id_curso = item.id_curso;
                pItem.id_disciplina = Convert.ToInt32(qId);
                pItem.status = "cadastrado";
                pItem.data_cadastro = DateTime.Now;
                pItem.data_alteracao = pItem.data_cadastro;
                pItem.usuario = usuario.usuario;
                if (aplicacaoCurso.IncluirDisciplina_Curso(pItem))
                {
                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na inclusão da Disciplina. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadCursoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fExcluiDisciplinaCurso()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];

                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos item;
                cursos_disciplinas pItem = new cursos_disciplinas();
                item = (cursos)Session["cursos"];
                pItem.id_curso = item.id_curso;
                pItem.id_disciplina = Convert.ToInt32(qId);
                if (aplicacaoCurso.ExcluirDisciplina(pItem) != null)
                {
                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na exclusão da Disciplina. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadCursoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fCheckObrigatoria()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdCurso = HttpContext.Current.Request["qIdCurso"];
                string qIdDiscuplina = HttpContext.Current.Request["qIdDiscuplina"];
                string qSituacao = HttpContext.Current.Request["qSituacao"];

                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos item = (cursos)Session["cursos"];
                cursos_disciplinas pItem = new cursos_disciplinas();
                pItem.id_curso = Convert.ToInt32(qIdCurso);
                pItem.id_disciplina = Convert.ToInt32(qIdDiscuplina);
                if (qSituacao == "true")
                {
                    pItem.obrigatoria = 1;
                }
                else
                {
                    pItem.obrigatoria = 0;
                }

                if (aplicacaoCurso.AlterarObrigatoriedade_Curso(pItem))
                {
                    item.cursos_disciplinas.Where(x => x.id_disciplina == pItem.id_disciplina).SingleOrDefault().obrigatoria = pItem.obrigatoria;
                    Session["oferecimentos"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na alteração da obrigatoriedade da disciplina. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //=========================================================================

        //cadDisciplinaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheProfessorDisciplina()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                disciplinas item;
                item = (disciplinas)Session["disciplinas"];
                List<disciplinas_professores> lista = new List<disciplinas_professores>();
                lista = aplicacaoDisciplina.ListaProfessores(item);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    int i = 0;
                    string sAux;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.professores.id_professor.ToString();
                        retorno.P1 = elemento.professores.cpf;
                        retorno.P2 = elemento.professores.nome;
                        if (elemento.responsavel.Value)
                        {
                            sAux = "checked= \"checked\"";
                        }
                        else
                        {
                            sAux = "";
                        }
                        retorno.P3 = ("<label class=\"checkbox\"><input id = \"chkResponsavel_" + elemento.professores.id_professor.ToString() + "\" type=\"checkbox\" name=\"chkResponsavel_" + elemento.professores.id_professor.ToString() + "\" " + sAux + " ><span></span></label>");
                        //retorno.P3 = ("<input id = \"chkResponsavel_" + i + "\" type=\"checkbox\" name=\"chkResponsavel_" + i + "\" " + sAux + " onclick = \"fCheckResponsavel(this);\">");
                        retorno.P4 = ("<div title=\"Excluir Professor\"> <a class=\"btn btn-danger  btn-circle  fa fa-eraser\" href=\'javascript:AbreModalApagarProfessor(\""
                        + (elemento.professores.id_professor.ToString() + ("\",\""
                        + (elemento.professores.cpf + ("\",\""
                        + (elemento.professores.nome + "\")\'; ></a></div>"))))));

                        i++;

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadDisciplinaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPerquisaProfessorDisponivelDisciplina()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string sCPF = HttpContext.Current.Request["qCPF"];
                string sNome = HttpContext.Current.Request["qNome"];

                DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                disciplinas item;
                disciplinas_professores itemProfessor = new disciplinas_professores();
                professores pItemProfessor = new professores();
                item = (disciplinas)Session["disciplinas"];
                itemProfessor.id_disciplina = item.id_disciplina;
                itemProfessor.tipo_professor = "professor";

                pItemProfessor.cpf = sCPF.Trim();
                pItemProfessor.nome = sNome.Trim();
                List<professores> lista = new List<professores>();
                lista = aplicacaoDisciplina.ListaProfessoresDisponiveis(itemProfessor, pItemProfessor);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_professor.ToString();
                        retorno.P1 = elemento.nome;
                        retorno.P2 = elemento.cpf;
                        retorno.P3 = ("<div title=\"Incluir Professor\"> <a class=\"btn btn-success  btn-circle  fa fa-plus\" href=\'javascript:fIncluiProfessorDisciplina(\""
                        + (elemento.id_professor.ToString() + ("\",\""
                        + (elemento.cpf + ("\",\""
                        + (elemento.nome + "\")\'; ></a></div>"))))));

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadDisciplinaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fIncluiProfessorDisciplina()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];

                DisciplinaAplicacao aplicacaoCurso = new DisciplinaAplicacao();
                disciplinas item;
                disciplinas_professores pItem = new disciplinas_professores();
                item = (disciplinas)Session["disciplinas"];
                pItem.id_disciplina = item.id_disciplina;
                pItem.id_professor = Convert.ToInt32(qId);
                pItem.responsavel = false;
                pItem.tipo_professor = "professor";
                pItem.status = "cadastrado";
                pItem.data_cadastro = DateTime.Now;
                pItem.data_alteracao = pItem.data_cadastro;
                pItem.usuario = usuario.usuario;
                if (aplicacaoCurso.IncluirProfessor_Tecnico_Disciplina(pItem))
                {
                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na inclusão do Professor. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadDisciplinaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fExcluiProfessorDisciplina()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];

                DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                disciplinas item;
                disciplinas_professores  pItem = new disciplinas_professores();
                item = (disciplinas)Session["disciplinas"];
                pItem.id_disciplina = item.id_disciplina;
                pItem.id_professor = Convert.ToInt32(qId);
                pItem.tipo_professor = "professor";

                if (aplicacaoDisciplina.ExcluirProfessor_Tecnico_Disciplina(pItem) != null)
                {
                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na exclusão do Professor. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadDisciplinaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheTecnicoDisciplina()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                disciplinas item;
                item = (disciplinas)Session["disciplinas"];
                List<disciplinas_professores> lista = new List<disciplinas_professores>();
                lista = aplicacaoDisciplina.ListaTecnicos(item);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.professores.id_professor.ToString();
                        retorno.P1 = elemento.professores.cpf;
                        retorno.P2 = elemento.professores.nome;
                        retorno.P3 = ("<div title=\"Excluir Técnico\"> <a class=\"btn btn-danger  btn-circle  fa fa-eraser\" href=\'javascript:AbreModalApagarTecnico(\""
                        + (elemento.professores.id_professor.ToString() + ("\",\""
                        + (elemento.professores.cpf + ("\",\""
                        + (elemento.professores.nome + "\")\'; ></a></div>"))))));
                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadDisciplinaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPerquisaTecnicoDisponivelDisciplina()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string sCPF = HttpContext.Current.Request["qCPF"];
                string sNome = HttpContext.Current.Request["qNome"];

                DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                disciplinas item;
                disciplinas_professores itemProfessor = new disciplinas_professores();
                professores pItemProfessor = new professores();
                item = (disciplinas)Session["disciplinas"];
                itemProfessor.id_disciplina = item.id_disciplina;
                itemProfessor.tipo_professor = "tecnico";

                pItemProfessor.cpf = sCPF.Trim();
                pItemProfessor.nome = sNome.Trim();
                List<professores> lista = new List<professores>();
                lista = aplicacaoDisciplina.ListaProfessoresDisponiveis(itemProfessor, pItemProfessor);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_professor.ToString();
                        retorno.P1 = elemento.nome;
                        retorno.P2 = elemento.cpf;
                        retorno.P3 = ("<div title=\"Incluir Técnico\"> <a class=\"btn btn-success  btn-circle  fa fa-plus\" href=\'javascript:fIncluiTecnicoDisciplina(\""
                        + (elemento.id_professor.ToString() + ("\",\""
                        + (elemento.cpf + ("\",\""
                        + (elemento.nome + "\")\'; ></a></div>"))))));

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadDisciplinaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fIncluiTecnicoDisciplina()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];

                DisciplinaAplicacao aplicacaoCurso = new DisciplinaAplicacao();
                disciplinas item;
                disciplinas_professores pItem = new disciplinas_professores();
                item = (disciplinas)Session["disciplinas"];
                pItem.id_disciplina = item.id_disciplina;
                pItem.id_professor = Convert.ToInt32(qId);
                pItem.responsavel = false;
                pItem.tipo_professor = "tecnico";
                pItem.status = "cadastrado";
                pItem.data_cadastro = DateTime.Now;
                pItem.data_alteracao = pItem.data_cadastro;
                pItem.usuario = usuario.usuario;
                if (aplicacaoCurso.IncluirProfessor_Tecnico_Disciplina(pItem))
                {
                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na inclusão do Técnico. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadDisciplinaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fExcluiTecnicoDisciplina()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];

                DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                disciplinas item;
                disciplinas_professores pItem = new disciplinas_professores();
                item = (disciplinas)Session["disciplinas"];
                pItem.id_disciplina = item.id_disciplina;
                pItem.id_professor = Convert.ToInt32(qId);
                pItem.tipo_professor = "tecnico";

                if (aplicacaoDisciplina.ExcluirProfessor_Tecnico_Disciplina(pItem) != null)
                {
                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na exclusão do Técnico. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadDisciplinaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreenchePrerequisitoDisciplina()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                disciplinas item;
                item = (disciplinas)Session["disciplinas"];
                List<disciplinas_professores> lista = new List<disciplinas_professores>();
                lista = aplicacaoDisciplina.ListaTecnicos(item);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (item.disciplinas_requisitos.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in item.disciplinas_requisitos)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.disciplinas1.id_disciplina.ToString();
                        retorno.P1 = elemento.disciplinas1.codigo;
                        retorno.P2 = elemento.disciplinas1.nome;
                        retorno.P3 = ("<div title=\"Excluir Pré-requisito\"> <a class=\"btn btn-danger  btn-circle  fa fa-eraser\" href=\'javascript:AbreModalApagarPrerequisito(\""
                        + (elemento.disciplinas1.id_disciplina.ToString() + ("\",\""
                        + (elemento.disciplinas1.codigo + ("\",\""
                        + (elemento.disciplinas1.nome + "\")\'; ></a></div>"))))));
                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadDisciplinaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPerquisaPrerequisitoDisponivelDisciplina()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qCodigo = HttpContext.Current.Request["qCodigo"];
                string sNome = HttpContext.Current.Request["qNome"];

                DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                disciplinas item;
                disciplinas pItem = new disciplinas();

                item = (disciplinas)Session["disciplinas"];

                decimal[] aIdDisciplina;

                aIdDisciplina = item.disciplinas_requisitos.Select(x => x.id_disciplina_req).ToArray();

                aIdDisciplina=aIdDisciplina.Concat(item.disciplinas_requisitos.Select(x=> x.id_disciplina)).ToArray();

                pItem.codigo = qCodigo.Trim();
                pItem.nome = sNome.Trim();
                List<disciplinas> lista = new List<disciplinas>();
                lista = aplicacaoDisciplina.ListaPrerequisitoDisponiveis(pItem, aIdDisciplina);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_disciplina.ToString();
                        retorno.P1 = elemento.codigo;
                        retorno.P2 = elemento.nome;
                        retorno.P3 = ("<div title=\"Incluir Pré-requisito\"> <a class=\"btn btn-success  btn-circle  fa fa-plus\" href=\'javascript:fIncluiPrerequisitoDisciplina(\""
                        + (elemento.id_disciplina.ToString() + ("\",\""
                        + (elemento.codigo + ("\",\""
                        + (elemento.nome + "\")\'; ></a></div>"))))));

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadDisciplinaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fIncluiPrerequisitoDisciplina()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];

                DisciplinaAplicacao aplicacaoCurso = new DisciplinaAplicacao();
                disciplinas item;
                disciplinas_requisitos pItem = new disciplinas_requisitos();
                item = (disciplinas)Session["disciplinas"];
                pItem.id_disciplina = item.id_disciplina;
                pItem.id_disciplina_req = Convert.ToInt32(qId);
                pItem.status = "cadastrado";
                pItem.data_cadastro = DateTime.Now;
                pItem.data_alteracao = pItem.data_cadastro;
                pItem.usuario = usuario.usuario;
                if (aplicacaoCurso.IncluirPrerequisito_Disciplina(pItem))
                {
                    item = aplicacaoCurso.BuscaItem(item);
                    Session["disciplinas"] = item;
                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na inclusão do Pré-requisito. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadDisciplinaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fExcluiPrerequisitoDisciplina()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];

                DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                disciplinas item;
                disciplinas_requisitos pItem = new disciplinas_requisitos();
                item = (disciplinas)Session["disciplinas"];
                pItem.id_disciplina = item.id_disciplina;
                pItem.id_disciplina_req = Convert.ToInt32(qId);

                if (aplicacaoDisciplina.ExcluirPrerequisito_Disciplina(pItem))
                {
                    item = aplicacaoDisciplina.BuscaItem(item);
                    Session["disciplinas"] = item;
                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na exclusão do Pré-requisito. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadPeriodoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public string fCalculaNumeroPeriodo(string qTipoCurso, string qAno)
        {
            Session.Timeout = 60;
            try
            {
                string json = "";
                string sAux = "";
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"Retorno\":\"deslogado\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    return json;
                }

                QuadrimestreAplicacao aplicacaoPeriodo = new QuadrimestreAplicacao();
                quadrimestres item = new quadrimestres();

                item.id_tipo_curso = Convert.ToInt32(qTipoCurso); //temporariamente inabilitado Convert.ToInt32(qTipoCurso);
                item.ano = qAno;
                item.numero = aplicacaoPeriodo.BuscaItem_NumeroMaximo(item) + 1;
                if (item.id_tipo_curso == 1 && item.numero == 4)
                {
                    sAux = "Para curso do tipo <strong>\'MESTRADO\'</strong> não é permitido a crição de mais que 3 períodos que representam 3 quadrimestres.";
                    json = "[{\"Retorno\":\"erro\",\"Resposta\":\"" + sAux + "\",\"P2\":\"\",\"P3\":\"\"}]";
                    return json;
                }
                else
                {
                    json = "[{\"Retorno\":\"ok\",\"Resposta\":\"" + item.numero + "\",\"P2\":\"\",\"P3\":\"\"}]";
                    return json;
                }
            }
            catch (Exception ex)
            {
                string json = "[{\"Retorno\":\"erro\",\"Resposta\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                return json;
            }

        }

        //cadPeriodoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public string fAtivarInativarPeriodo(string qOperacao)
        {
            Session.Timeout = 60;
            try
            {
                string json = "";
                string sAux = "";
                string sAux2 = "";
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"Retorno\":\"deslogado\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    return json;
                }

                QuadrimestreAplicacao aplicacaoPerido = new QuadrimestreAplicacao();
                quadrimestres item = (quadrimestres)Session["quadrimestres"];

                foreach (var item2 in item.oferecimentos)
                {
                    if (item2.status != "inativado")
                    {
                        if (sAux == "")
                        {
                            sAux = "Não é possível inativar esse Período pois há ofereciemntos ativos associados: <br><br>";
                        }
                        sAux = sAux + "Disciplina: <strong>" + item2.disciplinas.nome + "</strong><br>";
                        sAux = sAux + "Oferecimento: <strong>" + item2.num_oferecimento + "</strong><br><br>";
                    }
                }

                foreach (var item2 in item.turmas)
                {
                    if (item2.status != "inativado")
                    {
                        if (sAux2 == "")
                        {
                            sAux2 = "Não é possível inativar esse Período pois há turmas ativas associadas: <br><br>";
                        }
                        sAux2 = sAux2 + "N.º: <strong>" + item2.cod_turma + "</strong><br>";
                        sAux2 = sAux2 + "Curso: <strong>" + item2.cursos.nome + "</strong><br><br>";
                    }
                }

                sAux = sAux + sAux2;
                if (sAux == "")
                {
                    if (qOperacao == "Ativar")
                    {
                        item.status = "alterado";
                    }
                    else
                    {
                        item.status = "inativado";
                    }
                    item.usuario = usuario.usuario;

                    item = aplicacaoPerido.AlterarStatus(item);

                    Session["quadrimestres"] = item;

                    json = "[{\"Retorno\":\"ok\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    return json;
                }
                else
                {
                    json = "[{\"Retorno\":\"erro\",\"Resposta\":\"" + sAux + "\",\"P2\":\"\",\"P3\":\"\"}]";
                    return json;
                }
            }
            catch (Exception ex)
            {
                string json = "[{\"Retorno\":\"erro\",\"Resposta\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                return json;
            }

        }

        //=========================================================================

        //cadTurmaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheCoordenadorTurma()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                turmas item;
                item = (turmas)Session["turmas"];
                List<turmas_coordenadores> lista = new List<turmas_coordenadores>();
                lista = aplicacaoTurma.ListaCoordenadores(item);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.professores.id_professor.ToString();
                        retorno.P1 = elemento.professores.cpf;
                        retorno.P2 = elemento.professores.nome;
                        retorno.P3 = ("<div title=\"Excluir Coordenador\"> <a class=\"btn btn-danger  btn-circle  fa fa-eraser\" href=\'javascript:AbreModalApagarCoordenador(\""
                        + (elemento.professores.id_professor.ToString() + ("\",\""
                        + (elemento.professores.cpf + ("\",\""
                        + (elemento.professores.nome + "\")\'; ></a></div>"))))));

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadTurmaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPerquisaCoordenadorDisponivelTurma()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string sCPF = HttpContext.Current.Request["qCPF"];
                string sNome = HttpContext.Current.Request["qNome"];

                TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                turmas item;
                professores pItemProfessor = new professores();
                item = (turmas)Session["turmas"];
                pItemProfessor.cpf = sCPF.Trim();
                pItemProfessor.nome = sNome.Trim();
                List<professores> lista = new List<professores>();
                lista = aplicacaoTurma.ListaProfessoresDisponiveis(item, pItemProfessor);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_professor.ToString();
                        retorno.P1 = elemento.nome;
                        retorno.P2 = elemento.cpf;
                        retorno.P3 = ("<div title=\"Incluir Coordenador\"> <a class=\"btn btn-success  btn-circle  fa fa-plus\" href=\'javascript:fIncluiCoordenadorTurma(\""
                        + (elemento.id_professor.ToString() + ("\",\""
                        + (elemento.cpf + ("\",\""
                        + (elemento.nome + "\")\'; ></a></div>"))))));

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadTurmaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fIncluiCoordenadorTurma()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];

                TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                turmas item;
                turmas_coordenadores pItem = new turmas_coordenadores();
                item = (turmas)Session["turmas"];
                pItem.id_turma = item.id_turma;
                pItem.id_professor = Convert.ToInt32(qId);
                pItem.status = "cadastrado";
                pItem.data_cadastro = DateTime.Now;
                pItem.data_alteracao = pItem.data_cadastro;
                pItem.usuario = usuario.usuario;
                if (aplicacaoTurma.IncluirCoordenador_Turma(pItem))
                {
                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na inclusão do Coordenador. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadTurmaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fExcluiCoordenadorTurma()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];

                TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                turmas item;
                turmas_coordenadores pItem = new turmas_coordenadores();
                item = (turmas)Session["turmas"];
                pItem.id_turma= item.id_turma;
                pItem.id_professor = Convert.ToInt32(qId);
                if (aplicacaoTurma.ExcluirCoordenador(pItem) != null)
                {
                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na exclusão do Coordenador. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadTurmaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheDisciplinaTurma()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                turmas item;
                item = (turmas)Session["turmas"];
                List<disciplinas> lista = new List<disciplinas>();
                lista = aplicacaoTurma.ListaDisciplinas(item);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();
                turmas_disciplinas item_turma_disciplina = new turmas_disciplinas();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        item_turma_disciplina = item.turmas_disciplinas.Where(x => x.id_disciplina == elemento.id_disciplina).SingleOrDefault();
                        retorno.P0 = elemento.id_disciplina.ToString();
                        retorno.P1 = elemento.codigo;
                        retorno.P2 = elemento.nome;
                        retorno.P3 = ("<div title=\"Excluir Disciplina\"> <a class=\"btn btn-danger  btn-circle  fa fa-eraser\" href=\'javascript:AbreModalApagarDisciplina(\""
                        + (elemento.id_disciplina.ToString() + ("\",\""
                        + (elemento.codigo + ("\",\""
                        + (elemento.nome + "\")\'; ></a></div>"))))));
                        retorno.P4 = item_turma_disciplina.data_cadastro.Value.ToString("dd/MM/yyyy");
                        retorno.P5 = item_turma_disciplina.usuario;

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadTurmaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPerquisaDisciplinaDisponivelTurma()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string sCodigo = HttpContext.Current.Request["qCodigo"];
                string sNome = HttpContext.Current.Request["qNome"];

                TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                turmas item;
                disciplinas pItemDisciplina = new disciplinas();
                item = (turmas)Session["turmas"];
                pItemDisciplina.codigo = sCodigo.Trim();
                pItemDisciplina.nome = sNome.Trim();
                List<disciplinas> lista = new List<disciplinas>();
                lista = aplicacaoTurma.ListaDisciplinasDisponiveis(item, pItemDisciplina);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_disciplina.ToString();
                        retorno.P1 = elemento.nome;
                        retorno.P2 = elemento.codigo;
                        retorno.P3 = ("<div title=\"Incluir Disciplina\"> <a class=\"btn btn-success  btn-circle  fa fa-plus\" href=\'javascript:fIncluiDisciplinaTurma(\""
                        + (elemento.id_disciplina.ToString() + ("\",\""
                        + (elemento.codigo + ("\",\""
                        + (elemento.nome + "\")\'; ></a></div>"))))));

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadTurmaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fIncluiDisciplinaTurma()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];

                TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                turmas item;
                turmas_disciplinas pItem = new turmas_disciplinas();
                item = (turmas)Session["turmas"];
                pItem.id_turma = item.id_turma;
                pItem.id_disciplina = Convert.ToInt32(qId);
                pItem.status = "cadastrado";
                pItem.data_cadastro = DateTime.Now;
                pItem.data_alteracao = pItem.data_cadastro;
                pItem.usuario = usuario.usuario;
                if (aplicacaoTurma.IncluirDisciplina_Turma(pItem))
                {
                    item.turmas_disciplinas.Add(pItem);
                    Session["turmas"] = item;
                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na inclusão da Disciplina. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadTurmaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fExcluiDisciplinaTurma()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];

                TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                turmas item;
                turmas_disciplinas pItem = new turmas_disciplinas();
                item = (turmas)Session["turmas"];
                pItem.id_turma= item.id_turma;
                pItem.id_disciplina = Convert.ToInt32(qId);
                if (aplicacaoTurma.ExcluirDisciplina(pItem) != null)
                {
                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na exclusão da Disciplina. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadTurmaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheMatriculaTurma()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdArea = HttpContext.Current.Request["qIdArea"];

                TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                turmas item;
                turmas item2 = new turmas();
                item = (turmas)Session["turmas"];
                List<turmas_coordenadores> lista = new List<turmas_coordenadores>();
                lista = aplicacaoTurma.ListaCoordenadores(item);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (qIdArea == "")
                {
                    item2.matricula_turma = item.matricula_turma.Where(x => x.id_area_concentracao == null).OrderBy(x => x.alunos.nome).ToList();
                }
                else
                {
                    item2.matricula_turma = item.matricula_turma.Where(x => x.id_area_concentracao == Convert.ToInt32(qIdArea)).OrderBy(x => x.alunos.nome).ToList();
                }

                if (item2.matricula_turma.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in item2.matricula_turma)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_matricula_turma.ToString();
                        retorno.P1 = elemento.alunos.idaluno.ToString();
                        retorno.P2 = elemento.alunos.nome;
                        retorno.P3 = String.Format("{0:dd/MM/yyyy}", elemento.data_cadastro);
                        if (elemento.historico_matricula_turma.Count > 0)
                        {
                            retorno.P4 = elemento.historico_matricula_turma.OrderByDescending(x => x.data_inicio).ThenByDescending(x => x.ordem).FirstOrDefault().status;
                            retorno.P5 = elemento.historico_matricula_turma.OrderByDescending(x => x.data_inicio).ThenByDescending(x => x.ordem).FirstOrDefault().situacao;
                        }
                        else
                        {
                            retorno.P4 = "<span class=\"text-danger\">sem status</span>";
                            retorno.P5 = "<span class=\"text-danger\">sem situação</span>";
                        }
                        retorno.P6 = "<div title=\"Comprovante de Matrícula\"> <a class=\"btn btn-warning  btn-circle  fa fa-print\" href=\'javascript:fComprovanteMatricula(\""
                        + elemento.id_matricula_turma.ToString() + "\",\""
                        + elemento.id_area_concentracao + "\",\""
                        + elemento.alunos.idaluno + "\",\""
                        + elemento.alunos.nome + "\")\'; ></a></div>";
                        if (item.cursos.areas_concentracao.Count > 1)
                        {
                            retorno.P7 = "<div title=\"Mudar de Área de Concentração\"> <a class=\"btn btn-primary  btn-circle  fa fa-exchange\" href=\'javascript:fAbreMudarAreaTurma(\""
                            + elemento.id_matricula_turma.ToString() + "\",\""
                            + elemento.id_area_concentracao + "\",\""
                            + elemento.alunos.idaluno + "\",\""
                            + elemento.alunos.nome + "\")\'; ></a></div>";
                        }
                        else
                        {
                            retorno.P7 = "";
                        }

                        if (retorno.P5 != "Matriculado")
                        {
                            retorno.P8 = "<div title=\"Não é permitido excluir matrícula de aluno com situação diferente de Matriculado.\"> <i class=\"fa fa-ban\"></i></div>";
                        }
                        else
                        {
                            retorno.P8 = "<div title=\"Excluir aluno da Turma\"> <a class=\"btn btn-danger btn-circle fa fa-eraser\" href=\'javascript:fExcluirAluno(\""
                            + elemento.id_matricula_turma.ToString() + "\",\""
                            + elemento.id_area_concentracao + "\",\""
                            + elemento.alunos.idaluno + "\",\""
                            + elemento.alunos.nome + "\")\'; ></a></div>";
                        }
                        
                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadTurmaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public string fMudarAreaTurma(string qMatricula, string qAreaAntiga, string qAreaNova, string qAluno)
        {
            Session.Timeout = 60;
            try
            {
                string json = "";
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"Retorno\":\"deslogado\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    return json;
                }

                TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                turmas item;
                matricula_turma item_matricula = new matricula_turma();
                item_matricula.id_matricula_turma = Convert.ToInt32(qMatricula);
                item_matricula.id_area_concentracao = Convert.ToInt32(qAreaNova);
                item_matricula.data_alteracao = DateTime.Now;
                item_matricula.usuario = usuario.usuario;

                aplicacaoTurma.AlterarAreaMatriculaTurma(item_matricula);

                item = (turmas)Session["turmas"];
                item = aplicacaoTurma.BuscaItem(item);
                Session["turmas"] = item;
                
                json = "[{\"Retorno\":\"ok\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                return json;
               
            }
            catch (Exception ex)
            {
                string json = "[{\"Retorno\":\"erro\",\"Resposta\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                return json;
            }

        }

        //cadTurmaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public string fExcluirAlunoTurma(string qMatricula)
        {
            Session.Timeout = 60;
            try
            {
                string json = "";
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"Retorno\":\"deslogado\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    return json;
                }

                TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                turmas item;
                matricula_turma item_matricula = new matricula_turma();
                item_matricula.id_matricula_turma = Convert.ToInt32(qMatricula);

                List<matricula_oferecimento> lista_matricula_oferecimento = aplicacaoTurma.Lista_VerificaAlunoMatriculadoOferecimento(item_matricula);

                if (lista_matricula_oferecimento.Count == 0)
                {
                    aplicacaoTurma.ExcluirMatriculaAlunoTurma(item_matricula);

                    item = (turmas)Session["turmas"];
                    item = aplicacaoTurma.BuscaItem(item);
                    Session["turmas"] = item;

                    json = "[{\"Retorno\":\"ok\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                }
                else
                {
                    string sAux = "";
                    foreach (var elemento in lista_matricula_oferecimento)
                    {
                        if (sAux != "")
                        {
                            sAux = sAux + "; ";
                        }
                        sAux = sAux + elemento.id_oferecimento.ToString();
                    }
                    sAux = "Não é possível excluir esse aluno, pois ele está matriculado no(s) oferecimento(s) n.º(s): " + sAux;
                    json = "[{\"Retorno\":\"erro\",\"Resposta\":\"" + sAux + "\",\"P2\":\"\",\"P3\":\"\"}]";
                }
               
                return json;

            }
            catch (Exception ex)
            {
                string json = "[{\"Retorno\":\"erro\",\"Resposta\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                return json;
            }

        }

        //cadTurmaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public string fAtivarInativarTurma(string qOperacao)
        {
            Session.Timeout = 60;
            try
            {
                string json = "";
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"Retorno\":\"deslogado\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    return json;
                }

                TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                turmas item;
                item = (turmas)Session["turmas"];

                if (qOperacao == "Ativar")
                {
                    item.status = "alterado";
                    item.ativo = true;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;
                    item = aplicacaoTurma.AlterarStatus(item);
                    Session["turmas"] = item;
                    json = "[{\"Retorno\":\"ok\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    return json;
                }
                else
                {
                    if (item.matricula_turma.Count() > 0)
                    {
                        foreach (var item2 in item.matricula_turma)
                        {
                            if (json == "")
                            {
                                json = "Não é possível inativar essa turma, pois os seguintes alunos estão matriculados nela:<br/><br/>";
                            }
                            json = json + "Matrícula: <strong>" + item2.id_aluno + "</strong> - Nome: <strong>" + item2.alunos.nome + "</strong><br/>";
                        }
                        json = json + "<br/> Para inativar a turma deve-se desmatricular esses alunos.";
                        json = "[{\"Retorno\":\"erro\",\"Resposta\":\"" + json + "\",\"P2\":\"\",\"P3\":\"\"}]";
                        return json;
                    }
                    else
                    {
                        item.ativo = false;
                        item.status = "inativado";
                        item.data_alteracao = DateTime.Now;
                        item.usuario = usuario.usuario;
                        item = aplicacaoTurma.AlterarStatus(item);
                        Session["turmas"] = item;
                        json = "[{\"Retorno\":\"ok\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                        return json;
                    }
                }

            }
            catch (Exception ex)
            {
                string json = "[{\"Retorno\":\"erro\",\"Resposta\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                return json;
            }

        }

        //cadTurmaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPerquisaAlunoDisponivelTurma()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qMatricula = HttpContext.Current.Request["qMatricula"];
                string sNome = HttpContext.Current.Request["qNome"];
                string sArea = HttpContext.Current.Request["qArea"];

                TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                turmas item;
                matricula_turma pItem = new matricula_turma();
                alunos pAluno = new alunos();
                item = (turmas)Session["turmas"];
                pItem.id_turma = item.id_turma;
                if (sArea != "")
                {
                    pItem.id_area_concentracao = Convert.ToInt32(sArea);
                }

                if (qMatricula == "")
                {
                    qMatricula = "0";
                }
                pAluno.idaluno = Convert.ToInt32(qMatricula);
                pAluno.nome = sNome;
                
                List<alunos> lista = new List<alunos>();
                lista = aplicacaoTurma.ListaAlunosDisponiveis(pItem, pAluno);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.idaluno.ToString();
                        retorno.P1 = elemento.idaluno.ToString();
                        retorno.P2 = elemento.nome;
                        retorno.P3 = "<select id=\"ddlStatusAluno_" + elemento.idaluno.ToString() + "\" class=\"form-control input-sm select2 SemPesquisa\"><option selected value = \"0\">Selecione um status</option><option value = \"1\">Regular</option><option value = \"2\">Especial</option></select>";
                        retorno.P4 = "<div title=\"Matricular Aluno\"> <a class=\"btn btn-success btn-circle fa fa-plus\" href=\'javascript:fIncluiAlunoTurma(\""
                        + elemento.idaluno.ToString() + "\",\""
                        + elemento.idaluno + "\",\""
                        + elemento.nome + "\")\'; ></a></div>";

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadTurmaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fIncluiAlunoTurma()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdAluno = HttpContext.Current.Request["qIdAluno"];
                string qArea = HttpContext.Current.Request["qArea"];
                string qStatus = HttpContext.Current.Request["qStatus"];

                TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                turmas item;
                matricula_turma pItemMatricula = new matricula_turma();
                historico_matricula_turma pItemHistorico = new historico_matricula_turma();

                item = (turmas)Session["turmas"];
                pItemMatricula.id_turma = item.id_turma;
                pItemMatricula.id_aluno = Convert.ToInt32(qIdAluno);
                if (qArea != "")
                {
                    pItemMatricula.id_area_concentracao = Convert.ToInt32(qArea);
                }
                pItemMatricula.situacao = "Matriculado";
                pItemMatricula.data_termino = item.data_fim;
                pItemMatricula.data_cadastro = DateTime.Now;
                pItemMatricula.data_alteracao = pItemMatricula.data_cadastro;
                pItemMatricula.usuario = usuario.usuario;

                pItemHistorico.data_inicio = item.data_inicio;
                pItemHistorico.data_fim = item.data_fim;
                pItemHistorico.data = pItemMatricula.data_cadastro;
                pItemHistorico.usuario = usuario.usuario;
                pItemHistorico.situacao = "Matriculado";
                pItemHistorico.ordem = 1;
                if (qStatus == "1")
                {
                    pItemHistorico.status = "Regular";
                }
                else
                {
                    pItemHistorico.status = "Especial";
                }
                pItemMatricula = aplicacaoTurma.IncluirAluno_Turma(pItemMatricula);
                if (pItemMatricula.id_matricula_turma != 0)
                {
                    pItemHistorico.id_matricula_turma = pItemMatricula.id_matricula_turma;
                    pItemHistorico = aplicacaoTurma.IncluirHistorico_Matricula(pItemHistorico);
                    if (pItemHistorico.id_historico != 0)
                    {
                        item = aplicacaoTurma.BuscaItem(item);
                        Session["turmas"] = item;

                        json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                        return;
                    }
                    else
                    {
                        json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na Matrícula do Aluno. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                        return;
                    }
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na Matrícula do Aluno. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadTurmaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fAbreMudarAreaTurma()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string sArea = HttpContext.Current.Request["qArea"];

                TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                turmas item;
                List<areas_concentracao> lista = new List<areas_concentracao>();

                item = (turmas)Session["turmas"];

                lista = item.cursos.areas_concentracao.Where(x => x.id_area_concentracao != Convert.ToInt32(sArea)).ToList();

                List<retornoCombo> listaRetorno = new List<retornoCombo>();

                if (lista.Count > 0)
                {
                    retornoCombo retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoCombo();
                        retorno.id = elemento.id_area_concentracao.ToString();
                        retorno.text = elemento.nome;
                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //=========================================================================

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheProfessorOferecimento()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                item = (oferecimentos)Session["oferecimentos"];
                List<oferecimentos_professores> lista = new List<oferecimentos_professores>();
                lista = aplicacaoOferecimento.ListaProfessores(item);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                List<decimal> sListaProfessores = new List<decimal>();

                foreach (var elemento in item.datas_aulas)
                {
                    var sAux2 = elemento.datas_aulas_professor.Select(x => x.id_professor).ToArray();
                    sListaProfessores.AddRange(sAux2);
                }

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    int i = 0;
                    string sAux;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.professores.id_professor.ToString();
                        retorno.P1 = elemento.professores.cpf;
                        retorno.P2 = elemento.professores.nome;
                        retorno.P3 = (elemento.professores.email_confirmado == 1)?"Sim": "<span class=\"text-danger\">Não</span>";
                        if (elemento.responsavel.Value)
                        {
                            sAux = "checked= \"checked\"";
                        }
                        else
                        {
                            sAux = "";
                        }
                        retorno.P4 = ("<label class=\"checkbox\"><input onclick=\"fCheckAssociar(this);\"  id = \"chkResponsavel_" + elemento.professores.id_professor.ToString() + "\" type=\"checkbox\" name=\"chkResponsavel_" + elemento.professores.id_professor.ToString() + "\" " + sAux + " ><span></span></label>");
                        //retorno.P3 = ("<input id = \"chkResponsavel_" + i + "\" type=\"checkbox\" name=\"chkResponsavel_" + i + "\" " + sAux + " onclick = \"fCheckResponsavel(this);\">");
                        if (sListaProfessores.Find(x => x == elemento.professores.id_professor) == 0)
                        {
                            retorno.P5 = ("<div title=\"Excluir Professor\"> <a class=\"btn btn-danger  btn-circle  fa fa-eraser\" href=\'javascript:AbreModalApagarProfessor(\""
                            + (elemento.professores.id_professor.ToString() + ("\",\""
                            + (elemento.professores.cpf + ("\",\""
                            + (elemento.professores.nome + "\")\'; ></a></div>"))))));
                        }
                        else
                        {
                            retorno.P5 = "<div title=\"Professor associado a alguma aula\"> <i class=\"fa fa-ban\"></i>";
                        }
                            
                        i++;

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPerquisaProfessorDisponivelOferecimento()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string sCPF = HttpContext.Current.Request["qCPF"];
                string sNome = HttpContext.Current.Request["qNome"];

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                oferecimentos_professores itemProfessor = new oferecimentos_professores();
                professores pItemProfessor = new professores();
                item = (oferecimentos)Session["oferecimentos"];
                itemProfessor.id_oferecimento = item.id_oferecimento;
                itemProfessor.tipo_professor = "professor";

                pItemProfessor.cpf = sCPF.Trim();
                pItemProfessor.nome = sNome.Trim();
                List<professores> lista = new List<professores>();
                lista = aplicacaoOferecimento.ListaProfessoresDisponiveis(itemProfessor, pItemProfessor);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_professor.ToString();
                        retorno.P1 = elemento.nome;
                        retorno.P2 = elemento.cpf;
                        retorno.P3 = ("<div title=\"Incluir Professor\"> <a class=\"btn btn-success  btn-circle  fa fa-plus\" href=\'javascript:fIncluiProfessorOferecimento(\""
                        + (elemento.id_professor.ToString() + ("\",\""
                        + (elemento.cpf + ("\",\""
                        + (elemento.nome + "\")\'; ></a></div>"))))));

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fIncluiProfessorOferecimento()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                oferecimentos_professores pItem = new oferecimentos_professores();
                item = (oferecimentos)Session["oferecimentos"];
                pItem.id_oferecimento = item.id_oferecimento;
                pItem.id_professor = Convert.ToInt32(qId);
                pItem.responsavel = false;
                pItem.tipo_professor = "professor";
                pItem.status = "cadastrado";
                pItem.data_cadastro = DateTime.Now;
                pItem.data_alteracao = pItem.data_cadastro;
                pItem.usuario = usuario.usuario;
                if (aplicacaoOferecimento.IncluirProfessor_Tecnico_Oferecimento(pItem))
                {
                    item = aplicacaoOferecimento.BuscaItem(item);
                    item.oferecimentos_professores.Add(pItem);
                    Session["oferecimentos"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na inclusão do Professor. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fExcluiProfessorOferecimento()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                oferecimentos_professores pItem = new oferecimentos_professores();
                item = (oferecimentos)Session["oferecimentos"];
                pItem.id_oferecimento = item.id_oferecimento;
                pItem.id_professor = Convert.ToInt32(qId);
                pItem.tipo_professor = "professor";

                if (aplicacaoOferecimento.ExcluirProfessor_Tecnico_Oferecimento(pItem))
                {
                    item = aplicacaoOferecimento.BuscaItem(item);
                    Session["oferecimentos"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na exclusão do Professor. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheTecnicoOferecimento()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                item = (oferecimentos)Session["oferecimentos"];
                List<oferecimentos_professores> lista = new List<oferecimentos_professores>();
                lista = aplicacaoOferecimento.ListaTecnicos(item);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                List<decimal> sAux =  new List<decimal>();

                foreach (var elemento in item.datas_aulas)
                {
                    var sAux2 = elemento.datas_aulas_professor.Select(x => x.id_professor).ToArray();
                    sAux.AddRange(sAux2);
                }


                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.professores.id_professor.ToString();
                        retorno.P1 = elemento.professores.cpf;
                        retorno.P2 = elemento.professores.nome;
                        retorno.P3 = (elemento.professores.email_confirmado == 1) ? "Sim" : "<span class=\"text-danger\">Não</span>";
                        if (sAux.Find(x=> x == elemento.professores.id_professor)  == 0)
                        {
                            retorno.P4 = ("<div title=\"Excluir Técnico\"> <a class=\"btn btn-danger btn-circle fa fa-eraser\" href=\'javascript:AbreModalApagarTecnico(\""
                            + (elemento.professores.id_professor.ToString() + ("\",\""
                            + (elemento.professores.cpf + ("\",\""
                            + (elemento.professores.nome + "\")\'; ></a></div>"))))));
                        }
                        else
                        {
                            retorno.P4 = "<div title=\"Técnico associado a alguma aula\"> <i class=\"fa fa-ban\"></i>";
                        }
                        
                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPerquisaTecnicoDisponivelOferecimento()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string sCPF = HttpContext.Current.Request["qCPF"];
                string sNome = HttpContext.Current.Request["qNome"];

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                oferecimentos_professores itemProfessor = new oferecimentos_professores();
                professores pItemProfessor = new professores();
                item = (oferecimentos)Session["oferecimentos"];
                itemProfessor.id_oferecimento = item.id_oferecimento;
                itemProfessor.tipo_professor = "tecnico";

                pItemProfessor.cpf = sCPF.Trim();
                pItemProfessor.nome = sNome.Trim();
                List<professores> lista = new List<professores>();
                lista = aplicacaoOferecimento.ListaProfessoresDisponiveis(itemProfessor, pItemProfessor);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_professor.ToString();
                        retorno.P1 = elemento.nome;
                        retorno.P2 = elemento.cpf;
                        retorno.P3 = ("<div title=\"Incluir Técnico\"> <a class=\"btn btn-success  btn-circle  fa fa-plus\" href=\'javascript:fIncluiTecnicoOferecimento(\""
                        + (elemento.id_professor.ToString() + ("\",\""
                        + (elemento.cpf + ("\",\""
                        + (elemento.nome + "\")\'; ></a></div>"))))));

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fIncluiTecnicoOferecimento()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                oferecimentos_professores pItem = new oferecimentos_professores();
                item = (oferecimentos)Session["oferecimentos"];
                pItem.id_oferecimento = item.id_oferecimento;
                pItem.id_professor = Convert.ToInt32(qId);
                pItem.responsavel = false;
                pItem.tipo_professor = "tecnico";
                pItem.status = "cadastrado";
                pItem.data_cadastro = DateTime.Now;
                pItem.data_alteracao = pItem.data_cadastro;
                pItem.usuario = usuario.usuario;
                if (aplicacaoOferecimento.IncluirProfessor_Tecnico_Oferecimento(pItem))
                {
                    item = aplicacaoOferecimento.BuscaItem(item);
                    Session["oferecimentos"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na inclusão do Técnico. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fExcluiTecnicoOferecimento()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                oferecimentos_professores pItem = new oferecimentos_professores();
                item = (oferecimentos)Session["oferecimentos"];
                pItem.id_oferecimento = item.id_oferecimento;
                pItem.id_professor = Convert.ToInt32(qId);
                pItem.tipo_professor = "tecnico";

                if (aplicacaoOferecimento.ExcluirProfessor_Tecnico_Oferecimento(pItem))
                {
                    item = aplicacaoOferecimento.BuscaItem(item);
                    Session["oferecimentos"] = item;
                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na exclusão do Técnico. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPerquisaDisciplinaDisponivelOferecimento()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string sCodigo = HttpContext.Current.Request["qCodigo"];
                string sNome = HttpContext.Current.Request["qNome"];
                string sCurso = HttpContext.Current.Request["qCurso"];

                DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                disciplinas pItemDisciplina = new disciplinas();
                cursos_disciplinas pItemCursoDisciplina = new cursos_disciplinas();
                pItemDisciplina.codigo = sCodigo.Trim();
                pItemDisciplina.nome = sNome.Trim();
                pItemDisciplina.status = "ativado";
                pItemCursoDisciplina.id_curso = Convert.ToInt32(sCurso);
                pItemDisciplina.cursos_disciplinas.Add(pItemCursoDisciplina);
                List<disciplinas> lista = new List<disciplinas>();
                lista = aplicacaoDisciplina.ListaItem(pItemDisciplina);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_disciplina.ToString();
                        retorno.P1 = elemento.nome;
                        retorno.P2 = elemento.codigo;
                        if (elemento.cursos_disciplinas.Count == 0)
                        {
                            retorno.P3 = "<div title=\"Disciplina sem curso associado.\"><i class=\"fa fa-ban text-danger\"></i></div>";
                        }
                        //else if (elemento.cursos_disciplinas.FirstOrDefault().cursos.cursos_coordenadores.Count == 0)
                        //{

                        //    retorno.P3 = "<div title=\"O curso associado a essa disciplina está sem coordenador.\"><i class=\"fa fa-window-close text-danger\"></i></div>";
                        //}
                        else
                        {
                            if (elemento.cursos_disciplinas.Any(x=> x.cursos.cursos_coordenadores.Count ==0))
                            {
                                cursos_disciplinas sAux = elemento.cursos_disciplinas.Where(x => x.cursos.cursos_coordenadores.Count == 0).FirstOrDefault();
                                retorno.P3 = "<div title=\"O curso '" + sAux.cursos.nome + "' associado a essa disciplina está sem coordenador.\"><i class=\"fa fa-window-close text-danger\"></i></div>";
                            }
                            else
                            {
                                retorno.P3 = ("<div title=\"Incluir Disciplina\"> <a class=\"btn btn-success  btn-circle  fa fa-plus\" href=\'javascript:fIncluiDisciplinaOferecimento(\""
                               + (elemento.id_disciplina.ToString() + ("\",\""
                               + (elemento.codigo + ("\",\""
                               + (elemento.nome + "\")\'; ></a></div>"))))));
                            }                        
                        }
                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public string fAtivarInativarOferecimento(string qOperacao)
        {
            Session.Timeout = 60;
            try
            {
                string json = "";
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"Retorno\":\"deslogado\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    return json;
                }

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                oferecimentos_professores pItem = new oferecimentos_professores();
                item = (oferecimentos)Session["oferecimentos"];

                if (qOperacao == "Ativar")
                {
                    item.status = "alterado";
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;
                    item = aplicacaoOferecimento.AlterarStatus(item);
                    Session["oferecimentos"] = item;
                    json = "[{\"Retorno\":\"ok\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    return json;
                }
                else
                {
                    if (item.matricula_oferecimento.Count() > 0)
                    {
                        foreach (var item2 in item.matricula_oferecimento)
                        {
                            if (json == "")
                            {
                                json = "Não é possível inativar esse Oferecimento, pois os seguintes alunos estão matriculados nesse oferecimento:<br/><br/>";
                            }
                            json = json + "Matrícula: <strong>" + item2.id_aluno + "</strong> - Nome: <strong>" + item2.alunos.nome + "</strong><br/>";
                        }
                        json = json + "<br/> Para inativar esse Oferecimento deve-se desmatricular esses alunos e excluir as Datas de Aula.";
                        json = "[{\"Retorno\":\"erro\",\"Resposta\":\"" + json + "\",\"P2\":\"\",\"P3\":\"\"}]";
                        return json;
                    }
                    else
                    {
                        item.status = "inativado";
                        item.data_alteracao = DateTime.Now;
                        item.usuario = usuario.usuario;
                        item = aplicacaoOferecimento.AlterarStatus(item);
                        Session["oferecimentos"] = item;
                        json = "[{\"Retorno\":\"ok\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                        return json;
                    }
                }

            }
            catch (Exception ex)
            {
                string json = "[{\"Retorno\":\"erro\",\"Resposta\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                return json;
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheDataAulaOferecimento()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 16)) // 8. Oferecimentos - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Você não tem permissão de acsso a essa operação.\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
                professor_data_recalculo item_data_recalculo;
                item_data_recalculo = aplicacaoProfessor.BuscaDataRecalculo();
                //item_data_recalculo.data_recalculo = Convert.ToDateTime("2020/04/07");

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                item = (oferecimentos)Session["oferecimentos"];
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                decimal iHoraP = 0;
                decimal iHoraT = 0;
                TimeSpan dHoraA = TimeSpan.Parse("00:00");
                TimeSpan dHoraP = TimeSpan.Parse("00:00");
                TimeSpan dHoraT = TimeSpan.Parse("00:00");
                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;

                if (item.datas_aulas.Count > 0)
                {
                    retornoGeral retorno;
                    int i = 0;
                    string sAux = "";
                    string sAux2 = "";
                    string sP8 = "";
                    string sP8a = "";
                    string sChecado = "";
                    int iQtdProfessor = 0;
                    //string sMinutos = "";
                    
                    foreach (var elemento in item.datas_aulas.OrderBy(x=> x.data_aula))
                    {
                        dHoraA = dHoraA + (Convert.ToDateTime(elemento.hora_fim) - Convert.ToDateTime(elemento.hora_inicio));

                        sP8 = "<table border = \"0\" cellpadding = \"2\" cellspacing = \"2\" style=\"width: 100% \">";
                        sP8a = "";

                        retorno = new retornoGeral();
                        retorno.P15 = item_data_recalculo.data_recalculo.AddDays(1).ToString("yyyy-MM-dd");

                        retorno.P0 = elemento.id_aula.ToString();
                        retorno.P1 = (i + 1).ToString();
                        retorno.P1 = "<a class=\"btn btn-circle btn-default\" href=\'javascript:fLogDataAula(\"" + String.Format("{0:dd/MM/yyyy}", elemento.data_alteracao) + "\",\"" + String.Format("{0:HH:mm}", elemento.data_alteracao) + "\",\"" + elemento.status + "\",\"" + elemento.usuario + "\")\';><span style=\"font-size:medium\">" + (i + 1).ToString() + "</span></a>";
                        //retorno.P1 = "<a class=\"btn btn-circle btn-default\" href=\'javascript:fLogDataAula(\"" + String.Format("{0:dd/MM/yyyy}", elemento.data_alteracao) + "\")\';><span style=\"font-size:medium\">" + (i + 1).ToString() + "</span></a>";

                        sAux2 = dtfi.GetDayName(elemento.data_aula.Value.DayOfWeek);
                        if (sAux2.IndexOf("-") != -1)
                        {
                            retorno.P2 = String.Format("{0:dd/MM/yyyy}", elemento.data_aula) + "<br>" + (sAux2).Substring(0, sAux2.IndexOf("-"));
                        }
                        else
                        {
                            retorno.P2 = String.Format("{0:dd/MM/yyyy}", elemento.data_aula) + "<br>" + sAux2;
                        }
                        
                        sAux2 = "";

                        retorno.P3 = String.Format("{0:HH:mm}", elemento.hora_inicio);
                        retorno.P4 = String.Format("{0:HH:mm}", elemento.hora_fim);
                        retorno.P5 = elemento.salas_aula.sala;

                        if (elemento.data_aula <=  item_data_recalculo.data_recalculo )
                        {
                            retorno.P6 = ("<div title=\"Bloqueado pelo Financeiro\"> <a class=\"btn btn-default  btn-circle fa fa-eraser\" ; ></a></div>");
                            retorno.P7 = ("<div title=\"Bloqueado pelo Financeiro\"> <a class=\"btn btn-default  btn-circle fa fa-edit\" ; ></a></div>");
                        }
                        else
                        {
                            if (elemento.presenca.Count == 0)
                            {
                                retorno.P6 = ("<div title=\"Excluir Aula\"> <a class=\"btn btn-danger  btn-circle fa fa-eraser\" href=\'javascript:fAbreApagarAula(\"" + (i + 1).ToString() + "\",\"" + elemento.id_aula + "\",\"" + String.Format("{0:yyyy-MM-dd}", elemento.data_aula) + "\",\"" + String.Format("{0:HH:mm}", elemento.hora_inicio) + "\",\"" + String.Format("{0:HH:mm}", elemento.hora_fim) + "\",\"" + elemento.id_sala_aula + "\",\"SemPresenca\")\'; ></a></div>");
                            }
                            else
                            {
                                retorno.P6 = ("<div title=\"Excluir Aula (há presenças de alunos nessa aula)\"> <a class=\"btn btn-warning  btn-circle fa fa-eraser\" href=\'javascript:fAbreApagarAula(\"" + (i + 1).ToString() + "\",\"" + elemento.id_aula + "\",\"" + String.Format("{0:yyyy-MM-dd}", elemento.data_aula) + "\",\"" + String.Format("{0:HH:mm}", elemento.hora_inicio) + "\",\"" + String.Format("{0:HH:mm}", elemento.hora_fim) + "\",\"" + elemento.id_sala_aula + "\",\"ComPresenca\")\'; ></a></div>");
                            }

                            retorno.P7 = ("<div title=\"Editar Aula\"> <a class=\"btn btn-primary  btn-circle fa fa-edit\" href=\'javascript:fAbreEditarAula(\"" + (i + 1).ToString() + "\",\"" + elemento.id_aula + "\",\"" + String.Format("{0:yyyy-MM-dd}", elemento.data_aula) + "\",\"" + String.Format("{0:HH:mm}", elemento.hora_inicio) + "\",\"" + String.Format("{0:HH:mm}", elemento.hora_fim) + "\",\"" + elemento.id_sala_aula + "\")\'; ></a></div>");

                        }

                        foreach (var elemento2 in elemento.datas_aulas_professor.OrderBy(x=> x.tipo_professor).ThenBy(x=> x.professores.nome))
                        {
                            var timeSpan = TimeSpan.FromHours(Convert.ToDouble(elemento2.hora_aula.Value));
                            int dd = timeSpan.Days;
                            int hh = timeSpan.Hours;
                            int mm = timeSpan.Minutes;
                            int ss = timeSpan.Seconds;

                            if (dd > 0)
                            {
                                hh = hh + (dd * 24);
                            }

                            var hours = Math.Floor(elemento2.hora_aula.Value);
                            //var mins = 60 * (elemento.col_TotalHoras - hours);

                            if (hh == 0 && hours != 0)
                            {
                                hh = Convert.ToInt32(hours);
                            }
                            //switch ((elemento2.hora_aula.Value - Math.Truncate(elemento2.hora_aula.Value)).ToString())
                            //{
                            //    case "0,08":
                            //        sMinutos = "05";
                            //        break;
                            //    case "0,17":
                            //        sMinutos = "10";
                            //        break;
                            //    case "0,25":
                            //        sMinutos = "15";
                            //        break;
                            //    case "0,33":
                            //        sMinutos = "20";
                            //        break;
                            //    case "0,42":
                            //        sMinutos = "25";
                            //        break;
                            //    case "0,50":
                            //        sMinutos = "30";
                            //        break;
                            //    case "0,58":
                            //        sMinutos = "35";
                            //        break;
                            //    case "0,67":
                            //        sMinutos = "40";
                            //        break;
                            //    case "0,75":
                            //        sMinutos = "45";
                            //        break;
                            //    case "0,83":
                            //        sMinutos = "50";
                            //        break;
                            //    case "0,92":
                            //        sMinutos = "55";
                            //        break;
                            //    default:
                            //        sMinutos = "00";
                            //        break;
                            //}

                            iQtdProfessor = elemento.datas_aulas_professor.Where(x => x.tipo_professor == "professor").ToList().Count;
                            if (elemento2.tipo_professor == "professor")
                            {
                                iHoraP = iHoraP + elemento2.hora_aula.Value;
                                //dHoraP = dHoraP.Add(Convert.ToDateTime(Math.Truncate(elemento2.hora_aula.Value).ToString("00") + ":" + sMinutos).TimeOfDay);
                                dHoraP = dHoraP.Add(timeSpan);
                            }
                            else
                            {
                                iHoraT = iHoraT + elemento2.hora_aula.Value;
                                //dHoraT = dHoraT.Add(Convert.ToDateTime(Math.Truncate(elemento2.hora_aula.Value).ToString("00") + ":" + sMinutos).TimeOfDay);
                                dHoraT = dHoraT.Add(timeSpan);
                            }

                            if (sAux2 == "")
                            {
                                sP8a = sP8a + "<tr> <td align=\"left\" colspan = \"2\"><b>Professores</b></td><td><b>Presença</b></td></tr>";
                                sAux2 = "professor";
                                sAux = "Professor";
                            }
                            else if (sAux2 != elemento2.tipo_professor)
                            {
                                sP8a = sP8a + "<tr> <td align=\"left\" colspan = \"4\"><hr><b>Técnicos/Monitores</b></td></tr>";
                                sAux2 = "tecnico";
                                sAux = "Técnico/Monitor";
                            }
                            sP8a = sP8a + "<tr >";
                            sP8a = sP8a + "<td width=\"50%\" align=\"left\">";
                            sP8a = sP8a + elemento2.professores.nome;
                            sP8a = sP8a + "</td><td width=\"20%\">";

                            //sP8a = sP8a + "Hora aula (" + Math.Truncate(elemento2.hora_aula.Value).ToString("00") + ":" + sMinutos;
                            sP8a = sP8a + "Hora aula (" + hh.ToString("00") + ":" + mm.ToString("00");
                            sP8a = sP8a + ")</td><td align=\"left\" width=\"10%\">";
                            if (elemento.presenca_professor.Count > 0)
                            {
                                if (elemento.presenca_professor.Where(x => x.id_aula == elemento2.id_aula && x.id_professor == elemento2.id_professor && x.id_oferecimento == elemento.id_oferecimento).SingleOrDefault() == null)
                                {
                                    sChecado = "";
                                }
                                else
                                {
                                    if (elemento.presenca_professor.Where(x => x.id_aula == elemento2.id_aula && x.id_professor == elemento2.id_professor && x.id_oferecimento == elemento.id_oferecimento).SingleOrDefault().presente.Value)
                                    {
                                        sChecado = "checked";
                                    }
                                    else
                                    {
                                        sChecado = "";
                                    }
                                }
                            }
                            else
                            {
                                sChecado = "";
                            }

                            //Aqui seria o local pra colocar um tooltip para mostrar quem e data de quem fez a alteração na presença do professor
                            //exemplo: <label data-toggle="tooltip" data-placement="top" title="This is the text of the tooltip" clas=....
                            string sDisabled;
                            if (elemento.data_aula <= item_data_recalculo.data_recalculo)
                            {
                                sDisabled = "disabled";
                            }
                            else
                            {
                                sDisabled = "";
                            }
                            sP8a = sP8a + "<span style=\"line-height: 2.2em;\"><label class=\"checkbox\"><input " + sDisabled + " onclick =\"fCheckPresenca(this);\"  id = \"chkPresenca_" + elemento2.id_aula.ToString() + "_" + elemento2.id_professor.ToString() + "_" + sAux2 + "\" type=\"checkbox\" name=\"chkPresenca_" + elemento2.id_aula.ToString() + "_" + elemento2.id_professor.ToString() + "_" + sAux2 + "\" " + sChecado + " ><span></span></label></span>";

                            sP8a = sP8a + "</td><td align=\"center\" width=\"10%\">";
                            if ((iQtdProfessor > 1 && sAux == "Professor") || sAux == "Técnico/Monitor")
                            {
                                if (elemento.data_aula <= item_data_recalculo.data_recalculo)
                                {
                                    sP8a = sP8a + "<span style =\"line-height: 2.2em;display:inline-block\"><div style =\"display:inline-block\" title=\"Bloqueado pelo Financeiro\"> <a class=\"btn btn-default  btn-circle-xs fa fa-eraser\"; ></a></div></span>";
                                    sP8a = sP8a + "</td><td align=\"center\" width=\"10%\">";
                                }
                                else
                                {
                                    sP8a = sP8a + "<span style =\"line-height: 2.2em;display:inline-block\"><div style =\"display:inline-block\" title=\"Excluir " + sAux + "\"> <a class=\"btn btn-danger  btn-circle-xs fa fa-eraser\" href=\'javascript:fAbreModalExcluirEquipe(\""
                                    + (i + 1).ToString() + "\",\""
                                    + elemento2.id_professor.ToString() + "\",\""
                                    + elemento2.id_aula + "\",\""
                                    + sAux + "\",\""
                                    //+ Math.Truncate(elemento2.hora_aula.Value).ToString("00") + "\",\""
                                    //+ sMinutos + "\",\""
                                    + hh.ToString("00") + "\",\""
                                    + mm.ToString("00") + "\",\""
                                    + elemento2.professores.nome + "\")\'; ></a></div></span>";
                                    sP8a = sP8a + "</td><td align=\"center\" width=\"10%\">";
                                }
                            }
                            else
                            {
                                sP8a = sP8a + "</td><td align=\"center\" width=\"10%\">";
                            }

                            if (elemento.data_aula <= item_data_recalculo.data_recalculo)
                            {
                                sP8a = sP8a + "<span style =\"line-height: 2.2em;display:inline-block\"><div style =\"display:inline-block\" title=\"Bloqueado pelo Financeiro\"> <a class=\"btn btn-default  btn-circle-xs fa fa-clock-o\"; ></a></div></span>";
                            }
                            else
                            {
                                sP8a = sP8a + "<span style =\"line-height: 2.2em;display:inline-block\"><div style =\"display:inline-block\" title=\"Editar hora do " + sAux + "\"> <a class=\"btn btn-primary  btn-circle-xs fa fa-clock-o\" href=\'javascript:fAbreModalAlterarEquipe(\""
                                    + (i + 1).ToString() + "\",\""
                                    + elemento2.id_professor.ToString() + "\",\""
                                    + elemento2.id_aula + "\",\""
                                    + sAux + "\",\""
                                    //+ Math.Truncate(elemento2.hora_aula.Value).ToString("00") + "\",\""
                                    //+ sMinutos + "\",\""
                                    + hh.ToString("00") + "\",\""
                                    + mm.ToString("00") + "\",\""
                                    + elemento2.professores.nome + "\")\'; ></a></div></span>";
                            }

                            sP8a = sP8a + "</td><tr>";


                            //if (sP6 != "")
                            //{
                            //    sP6 = sP6 + "<hr>";
                            //    sP7 = sP7 + "<hr>";
                            //    sP8 = sP8 + "<hr>";
                            //    sP9 = sP9 + "<hr>";
                            //    sP10 = sP10 + "<hr>";

                            //}
                            //if (elemento2.tipo_professor == "professor")
                            //{
                            //    //sP6 = sP6 + "<span style=\"line-height: 2.2em;\">Prof: <strong>";
                            //    sAux = "Professor";

                            //    sP6 = sP6 + "<span style =\"line-height: 2.2em;display:inline-block\"><div style =\"display:inline-block\" title=\"Excluir Professor\"> <a class=\"fa fa-close\" href=\'javascript:fAbreModalAlterarHoraEquipe(\""
                            //    + elemento2.id_professor.ToString() + "\",\""
                            //    + elemento2.id_aula + "\",\""
                            //    + sAux + "\",\""
                            //    + elemento2.hora_aula + "\",\""
                            //    + elemento2.professores.nome + "\")\'; ></a></div> Prof:<strong>" + elemento2.professores.nome + "</strong></span>";

                            //}
                            //else
                            //{
                            //    sP6 = sP6 + "<span style =\"line-height: 2.2em;\">Téc: <strong>";
                            //    sAux = "Técnico";
                            //}

                            //sP6 = sP6 + elemento2.professores.nome + "</strong></span>";

                            //sP7 = sP7 + "<span style =\"line-height: 2.2em;\">" + Math.Truncate(elemento2.hora_aula.Value).ToString("00") + ":" + (elemento2.hora_aula.Value - Math.Truncate(elemento2.hora_aula.Value)).ToString("00") + "</span>";

                            //sP7 = sP7 +  ("<span style =\"line-height: 2.2em;display:inline-block\">" + Math.Truncate(elemento2.hora_aula.Value).ToString("00") + ":" + (elemento2.hora_aula.Value - Math.Truncate(elemento2.hora_aula.Value)).ToString("00") + " <div style =\"display:inline-block\" title=\"Editar hora do " + sAux + "\"> <a class=\"fa fa-clock-o\" href=\'javascript:fAbreModalAlterarHoraEquipe(\""
                            //+ (elemento2.id_professor.ToString() + ("\",\""
                            //+ (elemento2.id_aula + ("\",\""
                            //+ (sAux + ("\",\""
                            //+ (elemento2.hora_aula + ("\",\""
                            //+ (elemento2.professores.nome + "\")\'; ></a></div></span>"))))))))));

                            //if (elemento.presenca_professor.Where(x => x.id_aula == elemento2.id_aula && x.id_professor == elemento2.id_professor).SingleOrDefault().presente.Value)
                            //{
                            //    sChecado = "checked";
                            //}
                            //else
                            //{
                            //    sChecado = "";
                            //}

                            //sP8 = sP8 + "<span style=\"line-height: 2.2em;\"><input onclick=\"fCheckPresenca(this);\"  id = \"chkPresenca_" + elemento2.id_aula.ToString() + "_" + elemento2.id_professor.ToString() + "\" type=\"checkbox\" name=\"chkPresenca_" + elemento2.id_aula.ToString() + "_" + elemento2.id_professor.ToString() + "\" " + sChecado + " ></span>";

                            //sP9 = sP9 + ("<div title=\"Editar hora do " + sAux + "\"> <a class=\"btn btn-primary  btn-circle fa fa-clock-o\" href=\'javascript:fAbreModalAlterarHoraEquipe(\""
                            //+ (elemento2.id_professor.ToString() + ("\",\""
                            //+ (elemento2.id_aula + ("\",\""
                            //+ (sAux + ("\",\""
                            //+ (elemento2.hora_aula + ("\",\""
                            //+ (elemento2.professores.nome + "\")\'; ></a></div>"))))))))));

                            //sP6 = sP6 + ("<div title=\"Excluir " + sAux + "\"> <a class=\"btn btn-danger  btn-circle  fa fa-eraser\" href=\'javascript:fAbreModalExcluirEquipe(\""
                            //+ (elemento2.id_professor.ToString() + ("\",\""
                            //+ (elemento2.id_aula + ("\",\""
                            //+ (sAux + ("\",\""
                            //+ (elemento2.professores.nome + "\")\'; ></a></div>"))))))));

                        }


                        sP8 = sP8 + sP8a + "</table>";
                        retorno.P8 = sP8;
                        if (elemento.data_aula <= item_data_recalculo.data_recalculo)
                        {
                            retorno.P9 = ("<div title=\"Bloqueado pelo Financeiro\"> <a class=\"btn btn-default  btn-circle fa fa-user-plus\" ; ></a></div>");
                            retorno.P10 = ("<div title=\"Bloqueado pelo Financeiro\"> <a class=\"btn btn-default  btn-circle fa fa-user-plus\" ; ></a></div>");
                        }
                        else
                        {
                            if (item.oferecimentos_professores.Where(x=> x.tipo_professor == "professor").ToList().Count > elemento.datas_aulas_professor.Where(x => x.tipo_professor == "professor").ToList().Count)
                            {
                                retorno.P9 = ("<div title=\"Adicionar Professor\"> <a class=\"btn btn-success  btn-circle fa fa-user-plus\" href=\'javascript:fAbreModalIncluirEquipe(\"" + (i + 1).ToString() + "\",\"" + elemento.id_aula + "\",\"Professor\")\'; ></a></div>");
                            }
                            else
                            {
                                retorno.P9 = "<div title=\"Professor indisponível\"><i class=\"fa fa-ban\"></i></div>";
                            }

                            if (item.oferecimentos_professores.Where(x => x.tipo_professor == "tecnico").ToList().Count > elemento.datas_aulas_professor.Where(x => x.tipo_professor == "tecnico").ToList().Count)
                            {
                                retorno.P10 = ("<div title=\"Adicionar Técnico/Monitor\"> <a class=\"btn btn-warning  btn-circle fa fa-user-plus\" href=\'javascript:fAbreModalIncluirEquipe(\"" + (i + 1).ToString() + "\",\"" + elemento.id_aula + "\",\"Técnico/Monitor\")\'; ></a></div>");
                            }
                            else
                            {
                                retorno.P10 = "<div title=\"Técnico/Monitor indisponível\"><i class=\"fa fa-ban\"></i></div>";
                            }
                        }

                        retorno.P11 = (int)dHoraP.TotalHours + dHoraP.ToString(@"\:mm");
                        retorno.P12 = (int)dHoraT.TotalHours + dHoraT.ToString(@"\:mm");
                        retorno.P15 = (int)dHoraA.TotalHours + dHoraA.ToString(@"\:mm");

                        if (item.carga_horaria < iHoraP)
                        {
                            retorno.P13 = "red";
                        }
                        else if (item.carga_horaria == iHoraP)
                        {
                            retorno.P13 = "";
                        }
                        else
                        {
                            retorno.P13 = "blue";
                        }

                        //if (elemento.data_aula <= item_data_recalculo.data_recalculo)
                        //{
                        //    retorno.P14 = ("<div title=\"Bloqueado pelo Financeiro\"> <a class=\"btn btn-default  btn-circle fa fa-calendar-check-o\" ; ></a></div>");
                        //}
                        //else
                        //{
                            if (elemento.presenca.Count == 0)
                            {
                                retorno.P14 = ("<div title=\"Editar presença dos Alunos / Gerar Lista de Presença (não cadastrado)\"> <a id=\"a_" + String.Format("{0:ddMMyyyy}", elemento.data_aula) + "\" class=\"btn btn-danger btn-circle fa fa-calendar-check-o\" href=\'javascript:fAbrePresencaAlunos(\"" + String.Format("{0:dd/MM/yyyy}", elemento.data_aula) + " " + dtfi.GetDayName(elemento.data_aula.Value.DayOfWeek) + "\",\"" + elemento.id_aula + "\",\"a_" + String.Format("{0:ddMMyyyy}", elemento.data_aula) + "\")\'; ></a></div>");
                            }
                            else if (elemento.presenca.Any(x=> x.presente == null))
                            {
                                retorno.P14 = ("<div title=\"Editar presença dos Alunos / Gerar Lista de Presença (não cadastrado)\"> <a id=\"a_" + String.Format("{0:ddMMyyyy}", elemento.data_aula) + "\" class=\"btn btn-danger btn-circle fa fa-calendar-check-o\" href=\'javascript:fAbrePresencaAlunos(\"" + String.Format("{0:dd/MM/yyyy}", elemento.data_aula) + " " + dtfi.GetDayName(elemento.data_aula.Value.DayOfWeek) + "\",\"" + elemento.id_aula + "\",\"a_" + String.Format("{0:ddMMyyyy}", elemento.data_aula) + "\")\'; ></a></div>");
                            }
                            else
                            {
                                retorno.P14 = ("<div title=\"Editar presença dos Alunos / Gerar Lista de Presença\"> <a id=\"a_" + String.Format("{0:ddMMyyyy}", elemento.data_aula) + "\" class=\"btn btn-purple btn-circle fa fa-calendar-check-o\" href=\'javascript:fAbrePresencaAlunos(\"" + String.Format("{0:dd/MM/yyyy}", elemento.data_aula) + " " + dtfi.GetDayName(elemento.data_aula.Value.DayOfWeek) + "\",\"" + elemento.id_aula + "\",\"a_" + String.Format("{0:ddMMyyyy}", elemento.data_aula) + "\")\'; ></a></div>");
                            }
                        //}

                        i++;

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fIncluirAula()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 16)) // 8. Oferecimentos - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Você não tem permissão de acsso a essa operação.\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qData = HttpContext.Current.Request["qData"]; 
                string qHoraAulaInicio = HttpContext.Current.Request["qHoraAulaInicio"];
                string qMinutoAulaInicio = HttpContext.Current.Request["qMinutoAulaInicio"];
                string qHoraAulaFim = HttpContext.Current.Request["qHoraAulaFim"];
                string qMinutoAulaFim = HttpContext.Current.Request["qMinutoAulaFim"];
                string qIdSalaAula = HttpContext.Current.Request["qIdSalaAula"];
                string qIdProfessor = HttpContext.Current.Request["qIdProfessor"];
                string qHoraGasta = HttpContext.Current.Request["qHoraGasta"];
                string qMinutoGasta = HttpContext.Current.Request["qMinutoGasta"];

                ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
                professor_data_recalculo item_data_recalculo;
                item_data_recalculo = aplicacaoProfessor.BuscaDataRecalculo();
                //item_data_recalculo.data_recalculo = Convert.ToDateTime("2020/04/07");

                if (Convert.ToDateTime(qData) <= item_data_recalculo.data_recalculo)
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Há um Bloqueio financeiro de Datas.<br><br>A data da aula tem que ser superior a " + item_data_recalculo.data_recalculo.ToString("dd/MM/yyyy") + ".\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                datas_aulas pItem_datas_aulas = new datas_aulas();
                datas_aulas_professor pItem_datas_aulas_professor = new datas_aulas_professor();
                presenca_professor pItem_presenca_professor = new presenca_professor();
                item = (oferecimentos)Session["oferecimentos"];

                pItem_datas_aulas.id_oferecimento = item.id_oferecimento;
                pItem_datas_aulas.id_sala_aula = Convert.ToInt32(qIdSalaAula);
                pItem_datas_aulas.data_aula = Convert.ToDateTime(qData);
                pItem_datas_aulas.hora_inicio = Convert.ToDateTime(qHoraAulaInicio + ":" + qMinutoAulaInicio);
                pItem_datas_aulas.hora_fim = Convert.ToDateTime(qHoraAulaFim + ":" + qMinutoAulaFim);
                pItem_datas_aulas.status = "cadastrado";
                pItem_datas_aulas.data_cadastro = DateTime.Now;
                pItem_datas_aulas.data_alteracao = pItem_datas_aulas.data_cadastro;
                pItem_datas_aulas.usuario = usuario.usuario;

                pItem_datas_aulas = aplicacaoOferecimento.Criar_datas_aulas(pItem_datas_aulas);

                if (pItem_datas_aulas != null)
                {
                    pItem_datas_aulas_professor.id_professor = Convert.ToInt32(qIdProfessor);
                    pItem_datas_aulas_professor.id_aula = pItem_datas_aulas.id_aula;
                    pItem_datas_aulas_professor.tipo_professor = "professor";
                    pItem_datas_aulas_professor.data_cadastro = DateTime.Now;
                    pItem_datas_aulas_professor.usuario = usuario.usuario;
                    decimal dHoraAula;

                    dHoraAula = Convert.ToDecimal(TimeSpan.Parse(qHoraGasta + ":" + qMinutoGasta + ":00").TotalHours);

                    //switch (qMinutoGasta)
                    //{
                    //    case "05":
                    //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,08");
                    //        break;
                    //    case "10":
                    //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,17");
                    //        break;
                    //    case "15":
                    //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,25");
                    //        break;
                    //    case "20":
                    //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,33");
                    //        break;
                    //    case "25":
                    //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,42");
                    //        break;
                    //    case "30":
                    //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,50");
                    //        break;
                    //    case "35":
                    //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,58");
                    //        break;
                    //    case "40":
                    //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,67");
                    //        break;
                    //    case "45":
                    //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,75");
                    //        break;
                    //    case "50":
                    //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,83");
                    //        break;
                    //    case "55":
                    //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,92");
                    //        break;
                    //    default:
                    //        dHoraAula = Convert.ToDecimal(qHoraGasta);
                    //        break;
                    //}

                    pItem_datas_aulas_professor.hora_aula = dHoraAula;

                    if (aplicacaoOferecimento.Criar_datas_aulas_professor(pItem_datas_aulas_professor))
                    {
                        pItem_presenca_professor.id_aula = pItem_datas_aulas.id_aula;
                        pItem_presenca_professor.id_oferecimento = item.id_oferecimento;
                        pItem_presenca_professor.id_professor = Convert.ToInt32(qIdProfessor);
                        pItem_presenca_professor.tipo_professor = "professor";
                        pItem_presenca_professor.presente = false;
                        pItem_presenca_professor.data_cadastro = DateTime.Now;
                        pItem_presenca_professor.data_alteracao = pItem_datas_aulas.data_cadastro;
                        pItem_presenca_professor.usuario = usuario.usuario;

                        if (aplicacaoOferecimento.Criar_presenca_professor(pItem_presenca_professor))
                        {
                            item = aplicacaoOferecimento.BuscaItem(item);
                            Session["oferecimentos"] = item;

                            json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                            this.Context.Response.ContentType = "application/json; charset=utf-8";
                            this.Context.Response.Write(json);
                            return;
                        }
                        else
                        {
                            json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na Inclusão da Aula. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                            this.Context.Response.ContentType = "application/json; charset=utf-8";
                            this.Context.Response.Write(json);
                            return;
                        }
                    }
                    else
                    {
                        json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na Inclusão da Aula. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                        return;
                    }
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na Inclusão da Aula. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fEditarAula()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 16)) // 8. Oferecimentos - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Você não tem permissão de acsso a essa operação.\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];
                string qData = HttpContext.Current.Request["qData"];
                string qHoraAulaInicio = HttpContext.Current.Request["qHoraAulaInicio"];
                string qMinutoAulaInicio = HttpContext.Current.Request["qMinutoAulaInicio"];
                string qHoraAulaFim = HttpContext.Current.Request["qHoraAulaFim"];
                string qMinutoAulaFim = HttpContext.Current.Request["qMinutoAulaFim"];
                string qIdSalaAula = HttpContext.Current.Request["qIdSalaAula"];

                ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
                professor_data_recalculo item_data_recalculo;
                item_data_recalculo = aplicacaoProfessor.BuscaDataRecalculo();
                //item_data_recalculo.data_recalculo = Convert.ToDateTime("2020/04/07");

                if (Convert.ToDateTime(qData) <= item_data_recalculo.data_recalculo)
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Há um Bloqueio financeiro de Datas.<br><br>A data da aula tem que ser superior a " + item_data_recalculo.data_recalculo.ToString("dd/MM/yyyy") + ".\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                datas_aulas pItem_datas_aulas = new datas_aulas();
                datas_aulas_professor pItem_datas_aulas_professor = new datas_aulas_professor();
                presenca_professor pItem_presenca_professor = new presenca_professor();
                item = (oferecimentos)Session["oferecimentos"];

                pItem_datas_aulas.id_aula = Convert.ToInt32(qId);
                pItem_datas_aulas.id_oferecimento = item.id_oferecimento;
                pItem_datas_aulas.id_sala_aula = Convert.ToInt32(qIdSalaAula);
                pItem_datas_aulas.data_aula = Convert.ToDateTime(qData);
                pItem_datas_aulas.hora_inicio = Convert.ToDateTime(qHoraAulaInicio + ":" + qMinutoAulaInicio);
                pItem_datas_aulas.hora_fim = Convert.ToDateTime(qHoraAulaFim + ":" + qMinutoAulaFim);
                pItem_datas_aulas.status = "alterado";
                pItem_datas_aulas.data_alteracao = DateTime.Now;
                pItem_datas_aulas.usuario = usuario.usuario;

                if (aplicacaoOferecimento.Alterar_datas_aulas(pItem_datas_aulas))
                {
                    item = aplicacaoOferecimento.BuscaItem(item);
                    Session["oferecimentos"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na alteração da Aula. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fExcluirAula()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                datas_aulas pItem_datas_aulas = new datas_aulas();
                datas_aulas_professor pItem_datas_aulas_professor = new datas_aulas_professor();
                presenca_professor pItem_presenca_professor = new presenca_professor();
                presenca pItem_presenca = new presenca();

                item = (oferecimentos)Session["oferecimentos"];

                pItem_presenca_professor.id_aula = Convert.ToInt32(qId);
                pItem_presenca_professor.id_oferecimento = item.id_oferecimento;

                pItem_datas_aulas_professor.id_aula = Convert.ToInt32(qId);
                pItem_presenca.id_aula = Convert.ToInt32(qId);

                pItem_datas_aulas.id_aula = Convert.ToInt32(qId);

                if (aplicacaoOferecimento.Excluir_presenca_professor(pItem_presenca_professor))
                {
                    if (aplicacaoOferecimento.Excluir_datas_aulas_professor(pItem_datas_aulas_professor))
                    {
                        if (aplicacaoOferecimento.Excluir_presenca(pItem_presenca))
                        {
                            if (aplicacaoOferecimento.Excluir_datas_aulas(pItem_datas_aulas))
                            {
                                item = aplicacaoOferecimento.BuscaItem(item);
                                Session["oferecimentos"] = item;

                                json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                                this.Context.Response.ContentType = "application/json; charset=utf-8";
                                this.Context.Response.Write(json);
                                return;
                            }
                            else
                            {
                                json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na exclusão da Aula. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                                this.Context.Response.ContentType = "application/json; charset=utf-8";
                                this.Context.Response.Write(json);
                                return;
                            }
                        }
                        else
                        {
                            json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na exclusão da Aula. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                            this.Context.Response.ContentType = "application/json; charset=utf-8";
                            this.Context.Response.Write(json);
                            return;
                        }

                    }
                    else
                    {
                        json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na exclusão da Aula. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                        return;
                    }
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na exclusão da Aula. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheMatriculaOferecimento()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                notas pItemNota;
                item = (oferecimentos)Session["oferecimentos"];
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (item.matricula_oferecimento.Count > 0)
                {
                    retornoGeral retorno;

                    foreach (var elemento in item.matricula_oferecimento)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_matricula_oferecimento.ToString();
                        retorno.P1 = elemento.id_aluno.ToString();
                        retorno.P2 = elemento.alunos.nome;
                        retorno.P3 = elemento.turmas.cod_turma + " - " + elemento.turmas.cursos.nome;
                        retorno.P4 = "frequencia";
                        retorno.P4 = (item.presenca.Where(x => x.id_aluno == elemento.id_aluno).Count() == 0) ? "0,00%" : ((item.presenca.Where(x => x.id_aluno == elemento.id_aluno && x.presente == true).Count()) / (item.datas_aulas.Count() * 0.01)).ToString("0.##") + "%";
                        //retorno.P5 = item.notas.Where(x => x.id_aluno == elemento.id_aluno).SingleOrDefault().conceito;
                        if (item.notas.Where(x => x.id_aluno == elemento.id_aluno).FirstOrDefault() == null)
                        {
                            pItemNota = new notas();
                            pItemNota.id_aluno = elemento.id_aluno;
                            pItemNota.id_oferecimento = item.id_oferecimento;
                            pItemNota.conceito = null;
                            pItemNota.autorizado = "N";
                            pItemNota.data_cadastro = DateTime.Now;
                            pItemNota.data_alteracao = pItemNota.data_cadastro;
                            pItemNota.usuario = usuario.usuario;
                            aplicacaoOferecimento.CriarNota(pItemNota);
                            item = aplicacaoOferecimento.BuscaItem(item);
                            Session["oferecimentos"] = item;
                        }

                        retorno.P5 = (item.notas.Where(x => x.id_aluno == elemento.id_aluno).FirstOrDefault() == null) ? "" : (item.notas.Where(x => x.id_aluno == elemento.id_aluno).Select(x => x.conceito).FirstOrDefault() == null) ? "" : item.notas.Where(x => x.id_aluno == elemento.id_aluno).Select(x => x.conceito).FirstOrDefault();
                        retorno.P6 = (item.notas.Where(x => x.id_aluno == elemento.id_aluno).FirstOrDefault() == null) ? "" : (item.notas.Where(x => x.id_aluno == elemento.id_aluno).FirstOrDefault().conceitos_de_aprovacao == null) ? "" : item.notas.Where(x => x.id_aluno == elemento.id_aluno).FirstOrDefault().conceitos_de_aprovacao.descricao;
                        retorno.P9 = (item.notas.Where(x => x.id_aluno == elemento.id_aluno).FirstOrDefault() == null) ? "" : (item.notas.Where(x => x.id_aluno == elemento.id_aluno).Select(x => x.autorizado).FirstOrDefault().Trim() == "") ? "" : (item.notas.Where(x => x.id_aluno == elemento.id_aluno).Select(x => x.autorizado).FirstOrDefault().ToUpper() == "S") ? "Sim" : "Não";

                        if (retorno.P6 == "Reprovado")
                        {
                            retorno.P6 = "<div class=\"text-danger\"><strong>" + retorno.P6  + "</strong></div>";
                        }

                        retorno.P7 = ("<div title=\"Editar Nota\"> <a class=\"btn btn-primary  btn-circle fa fa-edit\" href=\'javascript:fAbreEditarNota(\"" + elemento.turmas.cod_turma + " - " + elemento.turmas.cursos.nome + "\",\"" + item.notas.Where(x => x.id_aluno == elemento.id_aluno).FirstOrDefault().conceito + "\",\"" + item.notas.Where(x => x.id_aluno == elemento.id_aluno).FirstOrDefault().autorizado + "\",\"" + elemento.id_matricula_oferecimento + "\",\"" + elemento.id_aluno.ToString() + "\",\"" + elemento.id_turma.ToString() + "\",\"" + item.notas.Where(x => x.id_aluno == elemento.id_aluno).FirstOrDefault().id_nota.ToString() + "\",\"" + elemento.alunos.nome + "\")\'; ></a></div>");
                        retorno.P8 = ("<div title=\"Excluir Matrícula\"> <a class=\"btn btn-danger  btn-circle fa fa-eraser\" href=\'javascript:fAbreExcluirMatricula(\"" + elemento.id_matricula_oferecimento + "\",\"" + elemento.id_aluno.ToString() + "\",\"" + elemento.id_turma.ToString() + "\",\"" + item.notas.Where(x => x.id_aluno == elemento.id_aluno).FirstOrDefault().id_nota.ToString() + "\",\"" + elemento.alunos.nome + "\")\'; ></a></div>");

                        listaRetorno.Add(retorno);
                    }                    
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fCheckPresenca()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdAula = HttpContext.Current.Request["qIdAula"];
                string qIdProfessor = HttpContext.Current.Request["qIdProfessor"];
                string qTipoProfessor = HttpContext.Current.Request["qTipoProfessor"];
                string qSituacao = HttpContext.Current.Request["qSituacao"];

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                presenca_professor pItem_presenca_professor = new presenca_professor();

                item = (oferecimentos)Session["oferecimentos"];

                pItem_presenca_professor.id_aula = Convert.ToInt32(qIdAula);
                pItem_presenca_professor.id_oferecimento = item.id_oferecimento;
                pItem_presenca_professor.id_professor = Convert.ToInt32(qIdProfessor);
                pItem_presenca_professor.tipo_professor = qTipoProfessor;
                if (qSituacao == "true")
                {
                    pItem_presenca_professor.presente = true;
                }
                else
                {
                    pItem_presenca_professor.presente = false;
                }
                pItem_presenca_professor.data_alteracao = DateTime.Now;
                pItem_presenca_professor.data_cadastro = pItem_presenca_professor.data_alteracao;
                pItem_presenca_professor.usuario = usuario.usuario;
                
                if (aplicacaoOferecimento.Alterar_PresencaProfessor(pItem_presenca_professor))
                {
                    item = aplicacaoOferecimento.BuscaItem(item);
                    Session["oferecimentos"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na alteração da presença do " + qTipoProfessor + ". Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fExcluirEquipe()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdAula = HttpContext.Current.Request["qIdAula"];
                string qIdProfessor = HttpContext.Current.Request["qIdProfessor"];
                string qTipoProfessor = HttpContext.Current.Request["qTipoProfessor"];

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                datas_aulas_professor pItem_datas_aulas_professor = new datas_aulas_professor();

                item = (oferecimentos)Session["oferecimentos"];

                pItem_datas_aulas_professor.id_aula = Convert.ToInt32(qIdAula);
                pItem_datas_aulas_professor.id_professor = Convert.ToInt32(qIdProfessor);
                if (qTipoProfessor == "Professor")
                {
                    pItem_datas_aulas_professor.tipo_professor = "professor";
                }
                else
                {
                    pItem_datas_aulas_professor.tipo_professor = "tecnico";
                }
                
                if (aplicacaoOferecimento.Excluir_Equipe(pItem_datas_aulas_professor))
                {
                    item = aplicacaoOferecimento.BuscaItem(item);
                    Session["oferecimentos"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na Exclusão do " + qTipoProfessor + ". Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fAlterarEquipe()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdAula = HttpContext.Current.Request["qIdAula"];
                string qIdProfessor = HttpContext.Current.Request["qIdProfessor"];
                string qTipoProfessor = HttpContext.Current.Request["qTipoProfessor"];
                string qHora = HttpContext.Current.Request["qHora"];
                string qMinuto = HttpContext.Current.Request["qMinuto"];
                decimal dHoraAula;

                dHoraAula = Convert.ToDecimal(TimeSpan.Parse(qHora + ":" + qMinuto + ":00").TotalHours);

                //switch (qMinuto)
                //{
                //    case "05":
                //        dHoraAula = Convert.ToDecimal(qHora) + Convert.ToDecimal("0,08");
                //        break;
                //    case "10":
                //        dHoraAula = Convert.ToDecimal(qHora) + Convert.ToDecimal("0,17");
                //        break;
                //    case "15":
                //        dHoraAula = Convert.ToDecimal(qHora) + Convert.ToDecimal("0,25");
                //        break;
                //    case "20":
                //        dHoraAula = Convert.ToDecimal(qHora) + Convert.ToDecimal("0,33");
                //        break;
                //    case "25":
                //        dHoraAula = Convert.ToDecimal(qHora) + Convert.ToDecimal("0,42");
                //        break;
                //    case "30":
                //        dHoraAula = Convert.ToDecimal(qHora) + Convert.ToDecimal("0,50");
                //        break;
                //    case "35":
                //        dHoraAula = Convert.ToDecimal(qHora) + Convert.ToDecimal("0,58");
                //        break;
                //    case "40":
                //        dHoraAula = Convert.ToDecimal(qHora) + Convert.ToDecimal("0,67");
                //        break;
                //    case "45":
                //        dHoraAula = Convert.ToDecimal(qHora) + Convert.ToDecimal("0,75");
                //        break;
                //    case "50":
                //        dHoraAula = Convert.ToDecimal(qHora) + Convert.ToDecimal("0,83");
                //        break;
                //    case "55":
                //        dHoraAula = Convert.ToDecimal(qHora) + Convert.ToDecimal("0,92");
                //        break;
                //    default:
                //        dHoraAula = Convert.ToDecimal(qHora);
                //        break;
                //}


                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                datas_aulas_professor pItem_datas_aulas_professor = new datas_aulas_professor();

                item = (oferecimentos)Session["oferecimentos"];

                pItem_datas_aulas_professor.id_aula = Convert.ToInt32(qIdAula);
                pItem_datas_aulas_professor.id_professor = Convert.ToInt32(qIdProfessor);
                if (qTipoProfessor == "Professor")
                {
                    pItem_datas_aulas_professor.tipo_professor = "professor";
                }
                else
                {
                    pItem_datas_aulas_professor.tipo_professor = "tecnico";
                }
                pItem_datas_aulas_professor.hora_aula = dHoraAula;
                pItem_datas_aulas_professor.usuario = usuario.usuario;

                if (aplicacaoOferecimento.Alterar_HoraEquipe(pItem_datas_aulas_professor))
                {
                    item = aplicacaoOferecimento.BuscaItem(item);
                    Session["oferecimentos"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na alteração da hora do " + qTipoProfessor + ". Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheEquipeDisponiveisOferecimento()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qAula = HttpContext.Current.Request["qAula"];
                string qEquipe = HttpContext.Current.Request["qEquipe"];

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                oferecimentos_professores pitemProfessor = new oferecimentos_professores();
                datas_aulas_professor pItemdatas_aulas_professor = new datas_aulas_professor();
                item = (oferecimentos)Session["oferecimentos"];
                pitemProfessor.id_oferecimento = item.id_oferecimento;
                if (qEquipe == "Professor")
                {
                    pitemProfessor.tipo_professor = "professor";
                    pItemdatas_aulas_professor.tipo_professor = "professor";
                }
                else
                {
                    pitemProfessor.tipo_professor = "tecnico";
                    pItemdatas_aulas_professor.tipo_professor = "tecnico";
                }

                pItemdatas_aulas_professor.id_aula = Convert.ToInt32(qAula);

                List<professores> lista = new List<professores>();
                lista = aplicacaoOferecimento.ListaEquipeDisponiveis_Aula(pItemdatas_aulas_professor, pitemProfessor);
                List<retornoCombo> listaRetorno = new List<retornoCombo>();
                retornoCombo retorno;
                retorno = new retornoCombo();
                retorno.id = "0";
                retorno.text = "Selecione um " + qEquipe;
                listaRetorno.Add(retorno);

                if (lista.Count > 0)
                {
                    
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoCombo();
                        retorno.id = elemento.id_professor.ToString();
                        retorno.text = elemento.nome;
                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fIncluirEquipeOferecimento()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdAula = HttpContext.Current.Request["qIdAula"];
                string qIdProfessor = HttpContext.Current.Request["qIdProfessor"];
                string qTipoProfessor = HttpContext.Current.Request["qTipoProfessor"];
                string qHoraGasta = HttpContext.Current.Request["qHora"];
                string qMinutoGasta = HttpContext.Current.Request["qMinuto"];
                decimal dHoraAula;

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;

                datas_aulas_professor pItem_datas_aulas_professor = new datas_aulas_professor();
                presenca_professor pItem_presenca_professor = new presenca_professor();
                item = (oferecimentos)Session["oferecimentos"];
                
                pItem_datas_aulas_professor.id_professor = Convert.ToInt32(qIdProfessor);
                pItem_datas_aulas_professor.id_aula = Convert.ToInt32(qIdAula);
                if (qTipoProfessor == "Professor")
                {
                    pItem_datas_aulas_professor.tipo_professor = "professor";
                }
                else
                {
                    pItem_datas_aulas_professor.tipo_professor = "tecnico";
                }
                pItem_datas_aulas_professor.data_cadastro = DateTime.Now;
                pItem_datas_aulas_professor.usuario = usuario.usuario;

                dHoraAula = Convert.ToDecimal(TimeSpan.Parse(qHoraGasta + ":" + qMinutoGasta + ":00").TotalHours);

                //switch (qMinutoGasta)
                //{
                //    case "05":
                //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,08");
                //        break;
                //    case "10":
                //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,17");
                //        break;
                //    case "15":
                //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,25");
                //        break;
                //    case "20":
                //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,33");
                //        break;
                //    case "25":
                //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,42");
                //        break;
                //    case "30":
                //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,50");
                //        break;
                //    case "35":
                //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,58");
                //        break;
                //    case "40":
                //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,67");
                //        break;
                //    case "45":
                //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,75");
                //        break;
                //    case "50":
                //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,83");
                //        break;
                //    case "55":
                //        dHoraAula = Convert.ToDecimal(qHoraGasta) + Convert.ToDecimal("0,92");
                //        break;
                //    default:
                //        dHoraAula = Convert.ToDecimal(qHoraGasta);
                //        break;
                //}

                pItem_datas_aulas_professor.hora_aula = dHoraAula;

                if (aplicacaoOferecimento.Criar_datas_aulas_professor(pItem_datas_aulas_professor))
                {
                    pItem_presenca_professor.id_aula = Convert.ToInt32(qIdAula);
                    pItem_presenca_professor.id_oferecimento = item.id_oferecimento;
                    pItem_presenca_professor.id_professor = Convert.ToInt32(qIdProfessor);
                    if (qTipoProfessor == "Professor")
                    {
                        pItem_presenca_professor.tipo_professor = "professor";
                    }
                    else
                    {
                        pItem_presenca_professor.tipo_professor = "tecnico";
                    }
                    pItem_presenca_professor.presente = false;
                    pItem_presenca_professor.data_cadastro = DateTime.Now;
                    pItem_presenca_professor.data_alteracao = pItem_presenca_professor.data_cadastro;
                    pItem_presenca_professor.usuario = usuario.usuario;

                    if (aplicacaoOferecimento.Criar_presenca_professor(pItem_presenca_professor))
                    {
                        item = aplicacaoOferecimento.BuscaItem(item);
                        Session["oferecimentos"] = item;

                        json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                        return;
                    }
                    else
                    {
                        json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na Inclusão do " + qTipoProfessor + ". Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                        return;
                    }
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na Inclusão do " + qTipoProfessor + ". Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                
            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPerquisaAlunoDisponivelOferecimento()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qMatricula = HttpContext.Current.Request["qMatricula"];
                string sNome = HttpContext.Current.Request["qNome"];
                int iTipoCurso = 0;
                int iCurso = 0;
                int iTurma = 0;

                if (HttpContext.Current.Request["qTipoCurso"].Trim() != "")
                {
                    iTipoCurso = Convert.ToInt32(HttpContext.Current.Request["qTipoCurso"]);
                }
                if (HttpContext.Current.Request["qCurso"].Trim() != "")
                {
                    iCurso = Convert.ToInt32(HttpContext.Current.Request["qCurso"]);
                }
                if (HttpContext.Current.Request["qTurma"].Trim() != "" && HttpContext.Current.Request["qTurma"].Trim() != "undefined")
                {
                    iTurma = Convert.ToInt32(HttpContext.Current.Request["qTurma"]);
                }

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                oferecimentos pItem = new oferecimentos();
                alunos pAluno = new alunos();
                item = (oferecimentos)Session["oferecimentos"];
                if (qMatricula != "")
                {
                    pAluno.idaluno = Convert.ToInt32(qMatricula);
                }
                pAluno.nome = sNome;

                List<alunos> lista = new List<alunos>();
                lista = aplicacaoOferecimento.ListaAlunosDisponiveis(item, pAluno, iTipoCurso, iCurso, iTurma);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                bool bFaltaRequisito;
                string qDisciplinas;
                int qTipoCurso = 0;
                int qNumMaxinoDisciplinaCurso = 0;
                int qNumQuantasDisciplinasObrigatorias = 0;
                int qEssa_e_Obrigatoria = 0;
                int qQdtnObrigatorriaAlunoFez;
                int qFez = 0;
                int qPassou = 0;
                int qQtdDiciplinaAlunoPodeFazer = 0;
                string qAlerta = "";
                int qLigaAlerta = 0;
                int qLigaRequisito = 0;
                int qLigaSituacao = 0;
                bool bAtingiuDataLimiteDocumentacao = false;
                bool bDocumentosPendentes = false;
                bool bInadimplente;
                bool bContratoPendente = false;
                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.idaluno.ToString();
                        retorno.P1 = elemento.idaluno.ToString();
                        retorno.P2 = elemento.nome;
                        retorno.P3 = "";
                        bDocumentosPendentes = false;
                        bInadimplente = false;
                        bAtingiuDataLimiteDocumentacao = elemento.alunos_dataLimite_documentos_pendentes.OrderByDescending(x => x.data_limite).FirstOrDefault().data_limite < DateTime.Today;
                        //Retirado a obrigatoriedade do Histórico Escolar solicitado pela Longuinho com autorização do prof. Eduardo - em 11/01/2022)
                        //if (!elemento.entregou_certidao || !elemento.entregou_comprovante_end || !elemento.entregou_cpf || !elemento.entregou_diploma || !elemento.entregou_fotos || !elemento.entregou_historico || !elemento.entregou_rg )
                        if (!elemento.entregou_certidao || !elemento.entregou_comprovante_end || !elemento.entregou_cpf || !elemento.entregou_diploma || !elemento.entregou_fotos || !elemento.entregou_rg )
                        {
                             bDocumentosPendentes = true;
                        }

                        if (elemento.inadimplentes != null)
                        {
                            bInadimplente = true;
                        }

                        foreach (var elemento2 in elemento.matricula_turma)
                        {
                            bContratoPendente = false;
                            qQdtnObrigatorriaAlunoFez = 0;
                            // Aqui verifica se tem contrato pendente pra esse curso
                            if (!elemento.alunos_arquivos.Any(x => x.id_alunos_arquivos_tipo == 9 && x.id_matricula_turma == elemento2.id_matricula_turma))
                            {
                                bContratoPendente = true;
                            }

                            //Aqui o elemento 2 é cada turma em que o aluno está cadastrado
                            //Verificar aqui se é um curso de Mestrado
                            qTipoCurso = elemento2.turmas.cursos.id_tipo_curso;
                            if (elemento2.turmas.cursos.id_tipo_curso == 1)
                            {
                                qTipoCurso = 1;
                                //Verificar aqui qual é o número máximo de disciplinas do curso, se null então deixa zero e não conta nada
                                if (elemento2.turmas.cursos.num_max_disciplinas == null)
                                {
                                    qNumMaxinoDisciplinaCurso = 0;
                                }
                                else
                                {
                                    qNumMaxinoDisciplinaCurso = elemento2.turmas.cursos.num_max_disciplinas.Value;

                                    qNumQuantasDisciplinasObrigatorias = elemento2.turmas.cursos.cursos_disciplinas.Where(x => x.obrigatoria == 1).Count();

                                    //Verificar se é uma disciplina obrigatória para esse curso
                                    if (elemento2.turmas.cursos.cursos_disciplinas.Any(x => x.id_disciplina == item.id_disciplina && x.obrigatoria == 1))
                                    {
                                        qEssa_e_Obrigatoria = 1;
                                    }

                                    //
                                    matricula_oferecimento item_matricula_oferecimento = new matricula_oferecimento();
                                    item_matricula_oferecimento.id_aluno = elemento.idaluno;
                                    item_matricula_oferecimento.id_turma = elemento2.id_turma;
                                    List<oferecimentos> lista_oferecimento = aplicacaoOferecimento.Disciplina_cursada_aluno(item_matricula_oferecimento, Convert.ToInt32(item.id_disciplina));
                                    qFez = lista_oferecimento.Count();
                                    if (qFez> 0)
                                    {
                                        foreach (var itemoferecimento in lista_oferecimento)
                                        {
                                            foreach (var nota in itemoferecimento.notas.Where(x => x.id_aluno == elemento.idaluno))
                                            {
                                                if (nota.conceito == "A" || nota.conceito == "B" || nota.conceito == "C")
                                                {
                                                    qPassou = 1;
                                                }
                                            }
                                        }

                                    }

                                    int idOferecimentoAnterior = 0;
                                    List<oferecimentos> lista_oferecimento_nObriatorio = aplicacaoOferecimento.Oferecimentos_nObrigatorios_cursados_aluno(item_matricula_oferecimento, elemento2.turmas.cursos.id_curso);
                                    if (lista_oferecimento_nObriatorio.Count() > 0)
                                    {
                                        foreach (var itemoferecimento in lista_oferecimento_nObriatorio)
                                        {
                                            foreach (var nota in itemoferecimento.notas.Where(x=> x.id_aluno == elemento.idaluno))
                                            {
                                                if ((nota.conceito == "A" || nota.conceito == "B" || nota.conceito == "C") && ((Convert.ToInt32(nota.id_oferecimento) != idOferecimentoAnterior)  || idOferecimentoAnterior == 0))
                                                {
                                                    qQdtnObrigatorriaAlunoFez++;
                                                }
                                                idOferecimentoAnterior = Convert.ToInt32(nota.id_oferecimento);
                                            }
                                        }
                                    }
                                }
                            }

                            qQtdDiciplinaAlunoPodeFazer = (qNumMaxinoDisciplinaCurso - qNumQuantasDisciplinasObrigatorias) - qQdtnObrigatorriaAlunoFez;

                            //O curso dele "não" é mestrado;
                            //A disciplina é obrigatória para o curso dele, mas o Aluno ainda não fez;
                            //A disciplina não" é obrigatória, o Aluno ainda não fez, e o Aluno não atingiu o número máximo de discipinas não obrigatórias;
                            if (qTipoCurso != 1 || (qEssa_e_Obrigatoria == 1 && qFez == 0) || (qEssa_e_Obrigatoria == 0 && qFez == 0 && qQtdDiciplinaAlunoPodeFazer > 0))
                            {
                                qLigaAlerta = 0;
                                qAlerta = "";
                            }
                            //A disciplina é obrigatória para o curso dele e o Aluno já fez;
                            else if (qEssa_e_Obrigatoria == 1 && qFez > 0)
                            {
                                qLigaAlerta = 1;
                                qAlerta = "Essa disciplina é obrigatória para o curso da turma desse aluno.<br>";
                                qAlerta = qAlerta + "O aluno já fez essa disciplina <strong>" + qFez.ToString() + "</strong> vez(es).<br>";
                                if (qPassou == 1)
                                {
                                    qAlerta = qAlerta + "O aluno <strong>passou</strong> nessa disciplina.";
                                }
                                else
                                {
                                    qAlerta = qAlerta + "O aluno <strong>NÃO passou</strong> nessa disciplina.";
                                }
                            }
                            //A disciplina não é obrigatória para o curso dele e o Aluno já fez;
                            else if (qEssa_e_Obrigatoria == 0 && (qFez > 0 || qQtdDiciplinaAlunoPodeFazer <= 0))
                            {
                                qLigaAlerta = 1;
                                qAlerta = "Essa disciplina <string>NÃO</string> é obrigatória para o curso da turma desse aluno.<br>";
                                if (qFez == 1)
                                {
                                    qAlerta = qAlerta + "O aluno já fez essa disciplina <strong>" + qFez.ToString() + "</strong> vez(es).<br>";
                                    if (qPassou == 1)
                                    {
                                        qAlerta = qAlerta + "O aluno <strong>passou</strong> nessa disciplina.<br>";
                                    }
                                    else
                                    {
                                        qAlerta = qAlerta + "O aluno <strong>NÃO passou</strong> nessa disciplina.<br>";
                                    }
                                }
                                else
                                {
                                    qAlerta = qAlerta + "O aluno ainda <strong>NÃO</strong> fez essa disciplina.<br>";
                                }
                               
                                if (qQtdDiciplinaAlunoPodeFazer > 0)
                                {
                                    qAlerta = qAlerta + "O aluno ainda <strong>pode</strong> fazer " + qQtdDiciplinaAlunoPodeFazer.ToString() + " disciplina(s) não obrigatória.<br>";
                                }
                                else
                                {
                                    qAlerta = qAlerta + "O aluno já atingiu o número máximo de disciplinas não obrigatória.<br>";
                                }
                            }


                            bFaltaRequisito = false;
                            qDisciplinas = "";
                            if (retorno.P3 != "")
                            {
                                retorno.P3 = retorno.P3 + "<hr>";
                                retorno.P4 = retorno.P4 + "<hr>";
                            }
                            retorno.P3 = retorno.P3 + "<span style =\"line-height: 2.2em\">" + elemento2.turmas.cod_turma + " - " + elemento2.turmas.cursos.nome + "</span>";
                            //Verifica se o aluno tem alguma situação de matrícula cadastrada. Caso não tenha então não disponibilizá-lo.

                            bool bDisponibilizar = true;
                            if (elemento2.historico_matricula_turma.Count == 0)
                            {
                                bDisponibilizar = false;
                                retorno.P4 = retorno.P4 + "<span style =\"line-height: 2.2em\">" + "<div title=\"O aluno sem nenhuma Situação cadastrada.\"><i class=\"fa fa-2x fa-window-close text-danger\"></i></div>" + "</span>";
                            }
                            else if (bAtingiuDataLimiteDocumentacao && (bDocumentosPendentes || bContratoPendente))
                            {
                                if (bDocumentosPendentes && bContratoPendente)
                                {
                                    if (qTipoCurso == 5)
                                    {
                                        if (!elemento.entregou_rg || !elemento.entregou_cpf)
                                        {
                                            bDisponibilizar = false;
                                            retorno.P4 = retorno.P4 + "<span style =\"line-height: 2.2em\">" + "<div title=\"O aluno tem Documentos Pendentes e Não entregou o Contrato Assinado.\"><i class=\"fa fa-2x fa-window-close text-danger\"></i></div>" + "</span>";
                                        }
                                        else
                                        {
                                            retorno.P4 = retorno.P4 + "<span style =\"line-height: 2.2em\">" + "<div title=\"O aluno Não entregou o Contrato Assinado.\"><i class=\"fa fa-2x fa-window-close text-danger\"></i></div>" + "</span>";
                                        }
                                    }
                                    else
                                    {
                                        bDisponibilizar = false;
                                        retorno.P4 = retorno.P4 + "<span style =\"line-height: 2.2em\">" + "<div title=\"O aluno tem Documentos Pendentes e Não entregou o Contrato Assinado.\"><i class=\"fa fa-2x fa-window-close text-danger\"></i></div>" + "</span>";
                                    }

                                }
                                else if (bDocumentosPendentes)
                                {
                                    if (qTipoCurso == 5)
                                    {
                                        if (!elemento.entregou_rg || !elemento.entregou_cpf)
                                        {
                                            bDisponibilizar = false;
                                            retorno.P4 = retorno.P4 + "<span style =\"line-height: 2.2em\">" + "<div title=\"O aluno tem Documentos Pendentes.\"><i class=\"fa fa-2x fa-window-close text-danger\"></i></div>" + "</span>";
                                        }
                                    }
                                    else
                                    {
                                        bDisponibilizar = false;
                                        retorno.P4 = retorno.P4 + "<span style =\"line-height: 2.2em\">" + "<div title=\"O aluno tem Documentos Pendentes.\"><i class=\"fa fa-2x fa-window-close text-danger\"></i></div>" + "</span>";
                                    }
                                }
                                else
                                {
                                    bDisponibilizar = false;
                                    retorno.P4 = retorno.P4 + "<span style =\"line-height: 2.2em\">" + "<div title=\"O aluno Não entregou o Contrato Assinado.\"><i class=\"fa fa-2x fa-window-close text-danger\"></i></div>" + "</span>";
                                }
                            }

                            if (bDisponibilizar)
                            {
                                //Verifica se a última situação do aluno é "Matriculado" ou "Cursando"
                                if (elemento2.historico_matricula_turma.OrderByDescending(x => x.data_inicio).ThenByDescending(x => x.ordem).FirstOrDefault().situacao == "Matriculado") //|| elemento2.historico_matricula_turma.OrderByDescending(x => x.data_inicio).FirstOrDefault().situacao == "Cursando"
                                {
                                    qLigaSituacao = 0; //Ou seja, a situação da Matrícula é normal... não apresentar nenhuma mensagem a esse respeito
                                }
                                else
                                {
                                    qLigaSituacao = 1; //A situação da Matrícula NÃO é normal... então apresentar mensagem a esse respeito
                                }

                                //Verifica se o aluno está inadimplente
                                if (bInadimplente)
                                {
                                    retorno.P4 = retorno.P4 + "<span style =\"line-height: 2.2em\">" + "<div title=\"O aluno está INADIMPLENTE. Orientá-lo para regularizar a situação financeira.\"><i class=\"fa fa-2x fa-thumbs-down text-danger\"></i></div>" + "</span>";
                                }

                                //Verifica se o aluno já está matriculado nesse oferecimento
                                if (elemento.matricula_oferecimento.Any(x => x.id_oferecimento == item.id_oferecimento))
                                {

                                    if (elemento.matricula_oferecimento.Where(x => x.id_oferecimento == item.id_oferecimento).Any (x=> x.id_turma == elemento2.id_turma))
                                    {
                                        retorno.P4 = retorno.P4 + "<span style =\"line-height: 2.2em\">" + "<div title=\"O aluno já está matriculado nesse oferecimento com essa Turma.\"><i class=\"fa fa-2x fa-thumbs-down text-purple\"></i></div>" + "</span>";
                                    }
                                    else
                                    {
                                        qLigaAlerta = 1;
                                        qAlerta = " O aluno já está matriculado nesse oferecimento, mas em OUTRA Turma (Verifique).<br>" + qAlerta;
                                        retorno.P4 = retorno.P4 + "<div title=\"Matricular Aluno (Aluno com alerta. Verificar.)\"> <a class=\"btn btn-warning btn-circle fa fa-plus\" href=\'javascript:fIncluiAlunoOferecimentoConfirmacao(\""
                                            + qDisciplinas + "\",\"" //Disciplinas requsitadas
                                            + elemento2.turmas.cod_turma + " - " + elemento2.turmas.cursos.nome + "\",\"" //Turma e curso do Aluno
                                            + elemento2.historico_matricula_turma.OrderByDescending(x => x.data_inicio).ThenByDescending(x => x.ordem).FirstOrDefault().situacao + "\",\"" //Situação da matrícula
                                            + elemento2.id_turma.ToString() + "\",\"" //Id da turma
                                            + elemento.idaluno.ToString() + "\",\"" //Id do aluno
                                            + elemento.nome + "\",\"" //Nome do Aluno
                                            + qAlerta + "\",\"" //Alerta
                                            + qLigaSituacao.ToString() + "\",\"" //Liga Situação
                                            + qLigaRequisito.ToString() + "\",\"" //Liga Requerimento
                                            + qLigaAlerta.ToString() + "\")\'; ></a></div>";  //Liga Alerta
                                    }
                                }

                                else
                                {
                                    //Verifica se essa disciplina tem pre-requisito
                                    if (item.disciplinas.disciplinas_requisitos.Count > 0)
                                    {
                                        foreach (var elemento3 in item.disciplinas.disciplinas_requisitos)
                                        {
                                            qDisciplinas = qDisciplinas + elemento3.disciplinas1.codigo + " - " + elemento3.disciplinas1.nome + "<br>";
                                            if (!elemento2.alunos.matricula_oferecimento.Any(x => x.id_aluno == elemento2.id_aluno && x.oferecimentos.disciplinas.id_disciplina == elemento3.id_disciplina_req))
                                            {
                                                bFaltaRequisito = true;
                                                qLigaRequisito = 1;
                                            }
                                        }
                                    }

                                    if (qLigaSituacao == 1 || qLigaRequisito == 1 || qLigaAlerta == 1)
                                    {
                                        retorno.P4 = retorno.P4 + "<div title=\"Matricular Aluno (Aluno com alerta. Verificar.)\"> <a class=\"btn btn-warning btn-circle fa fa-plus\" href=\'javascript:fIncluiAlunoOferecimentoConfirmacao(\""
                                            + qDisciplinas + "\",\"" //Disciplinas requsitadas
                                            + elemento2.turmas.cod_turma + " - " + elemento2.turmas.cursos.nome + "\",\"" //Turma e curso do Aluno
                                            + elemento2.historico_matricula_turma.OrderByDescending(x => x.data_inicio).ThenByDescending(x => x.ordem).FirstOrDefault().situacao + "\",\"" //Situação da matrícula
                                            + elemento2.id_turma.ToString() + "\",\"" //Id da turma
                                            + elemento.idaluno.ToString() + "\",\"" //Id do aluno
                                            + elemento.nome + "\",\"" //Nome do Aluno
                                            + qAlerta + "\",\"" //Alerta
                                            + qLigaSituacao.ToString() + "\",\"" //Liga Situação
                                            + qLigaRequisito.ToString() + "\",\"" //Liga Requerimento
                                            + qLigaAlerta.ToString() + "\")\'; ></a></div>";  //Liga Alerta
                                    }
                                    else
                                    {
                                        retorno.P4 = retorno.P4 + "<div title=\"Matricular Aluno\"> <a class=\"btn btn-success btn-circle fa fa-plus\" href=\'javascript:fIncluiAlunoOferecimento(\""
                                            + elemento2.id_turma.ToString() + "\",\"" //Id da turma
                                            + elemento.idaluno.ToString() + "\",\"" //Id do aluno
                                            + elemento.nome + "\")\'; ></a></div>";  //Liga Alerta
                                    }

                                }

                            }
                        }
                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }
        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fIncluiAlunoOferecimento()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdTurma = HttpContext.Current.Request["qIdTurma"];
                string qIdAluno = HttpContext.Current.Request["qIdAluno"];
                string qNome = HttpContext.Current.Request["qNome"];

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                matricula_oferecimento pItem_matricula_oferecimento = new matricula_oferecimento();
                notas pItem_notas = new notas();

                item = (oferecimentos)Session["oferecimentos"];

                pItem_matricula_oferecimento.id_oferecimento = item.id_oferecimento;
                pItem_matricula_oferecimento.id_aluno = Convert.ToInt32(qIdAluno);
                pItem_matricula_oferecimento.id_turma = Convert.ToInt32(qIdTurma);
                pItem_matricula_oferecimento.status = "cadastrado";
                pItem_matricula_oferecimento.data_matricula = DateTime.Now;
                pItem_matricula_oferecimento.usuario = usuario.usuario;

                pItem_notas.id_oferecimento = item.id_oferecimento;
                pItem_notas.id_aluno = Convert.ToInt32(qIdAluno);
                pItem_notas.conceito = null;
                pItem_notas.autorizado = "";
                pItem_notas.data_cadastro = DateTime.Now;
                pItem_notas.data_alteracao = pItem_notas.data_cadastro;
                pItem_notas.usuario = usuario.usuario;

                if (aplicacaoOferecimento.MatricularAluno(pItem_matricula_oferecimento, pItem_notas))
                {
                    item = aplicacaoOferecimento.BuscaItem(item);
                    Session["oferecimentos"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na matrícula do Aluno " + qNome + " nesse oferecimento. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
               

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fExcluirMatriculaAluno()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId_matricula_oferecimento = HttpContext.Current.Request["qId_matricula_oferecimento"];
                string qId_aluno = HttpContext.Current.Request["qId_aluno"];
                string qId_turma = HttpContext.Current.Request["qId_turma"];
                string qId_Nota = HttpContext.Current.Request["qId_Nota"];
                string qNome = HttpContext.Current.Request["qNome"];

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                //presenca pItem_presenca = new presenca();
                //historico_notas pItem_historico_notas = new historico_notas();
                //notas pItem_notas = new notas();
                matricula_oferecimento pItem_matricula_oferecimento = new matricula_oferecimento();
                
                item = (oferecimentos)Session["oferecimentos"];

                pItem_matricula_oferecimento.id_matricula_oferecimento = Convert.ToInt32(qId_matricula_oferecimento);
                pItem_matricula_oferecimento.id_aluno = Convert.ToInt32(qId_aluno);
                pItem_matricula_oferecimento.id_oferecimento = item.id_oferecimento;

                if (aplicacaoOferecimento.ExcluirMatriculaAluno(pItem_matricula_oferecimento))
                {
                    item = aplicacaoOferecimento.BuscaItem(item);
                    Session["oferecimentos"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na Exclusão da matrícula do Aluno " + qNome + ". Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fEditarNota()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qid_aluno = HttpContext.Current.Request["qid_aluno"];
                string qid_nota = HttpContext.Current.Request["qid_nota"];
                string qNome = HttpContext.Current.Request["qNome"];
                string qConceito = HttpContext.Current.Request["qConceito"];
                string qAutorizado = HttpContext.Current.Request["qAutorizado"];

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;
                notas pItem_Nota = new notas();

                item = (oferecimentos)Session["oferecimentos"];

                pItem_Nota.id_nota = Convert.ToInt32(qid_nota);
                if (qConceito == "")
                {
                    pItem_Nota.conceito = null;
                }
                else
                {
                    pItem_Nota.conceito = qConceito;
                }
                pItem_Nota.autorizado = qAutorizado;
                pItem_Nota.status = "alterado";
                pItem_Nota.data_alteracao = DateTime.Now;
                pItem_Nota.usuario = usuario.usuario;

                if (aplicacaoOferecimento.EditarNota(pItem_Nota))
                {
                    item = aplicacaoOferecimento.BuscaItem(item);
                    Session["oferecimentos"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na Alteração da nota do Aluno " + qNome + ". Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreenchePresencaAlunos()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdAula = HttpContext.Current.Request["qIdAula"];
                var vData = HttpContext.Current.Request["qData"].Split(' ');
                DateTime dData = Convert.ToDateTime(vData.ElementAt(0));

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item;

                item = (oferecimentos)Session["oferecimentos"];

                List<retornoGeral> listaRetorno = new List<retornoGeral>();
                retornoGeral retorno;
                List<presenca> listaPresenca;
                presenca item_presenca;

                foreach (var elemento in item.matricula_oferecimento.OrderBy(x=> x.alunos.nome))
                {
                    
                    retorno = new retornoGeral();
                    retorno.P0 = elemento.id_aluno.ToString();
                    retorno.P1 = elemento.id_aluno.ToString();
                    retorno.P2 = elemento.alunos.nome;

                    listaPresenca = item.datas_aulas.Where(x => x.id_aula == Convert.ToInt32(qIdAula)).FirstOrDefault().presenca.ToList();

                    string sDesligado = "";
                    if (elemento.alunos.matricula_turma.Where(x => x.id_turma == elemento.id_turma).FirstOrDefault().historico_matricula_turma.Any(x => x.situacao == "Abandonou" && dData >= x.data_inicio))
                    {
                        sDesligado = "Abandonou em " + elemento.alunos.matricula_turma.Where(x => x.id_turma == elemento.id_turma).FirstOrDefault().historico_matricula_turma.Where(x => x.situacao == "Abandonou").SingleOrDefault().data_inicio.Value.ToString("dd/MM/yyyy");
                    }
                    else if (elemento.alunos.matricula_turma.Where(x => x.id_turma == elemento.id_turma).FirstOrDefault().historico_matricula_turma.Any(x => x.situacao == "Desligado" && dData >= x.data_inicio))
                    {
                        sDesligado = "Desligado em " + elemento.alunos.matricula_turma.Where(x => x.id_turma == elemento.id_turma).FirstOrDefault().historico_matricula_turma.Where(x => x.situacao == "Desligado").SingleOrDefault().data_inicio.Value.ToString("dd/MM/yyyy");
                    }

                    if (listaPresenca.Any(x => x.id_aluno == elemento.id_aluno))
                    {
                        if (listaPresenca.Where(x => x.id_aluno == elemento.id_aluno).FirstOrDefault().presente == true)
                        {
                            retorno.P3 = ("<label class=\"checkbox\"><input id = \"chkAlunoPresenca_" + elemento.id_aluno.ToString() + "\" type=\"checkbox\" name=\"chkAlunoPresenca_" + elemento.id_aluno.ToString() + "\" checked><span></span></label>");
                        }
                        else if (sDesligado != "")
                        {
                            retorno.P3 = "<span><strong class=\"text-danger\">" + sDesligado + "</strong></span>";
                        }
                        else
                        {
                            retorno.P3 = ("<label class=\"checkbox\"><input id = \"chkAlunoPresenca_" + elemento.id_aluno.ToString() + "\" type=\"checkbox\" name=\"chkAlunoPresenca_" + elemento.id_aluno.ToString() + "\"><span></span></label>");
                        }
                    }
                    else
                    {
                        item_presenca = new presenca();
                        item_presenca.id_aluno = elemento.id_aluno;
                        item_presenca.id_oferecimento = elemento.id_oferecimento;
                        item_presenca.id_aula = Convert.ToInt32(qIdAula);
                        item_presenca.presente = null;
                        item_presenca.data_cadastro = DateTime.Now;
                        item_presenca.data_alteracao = item_presenca.data_cadastro;
                        item_presenca.usuario = usuario.usuario;
                        //item_presenca.alunos = elemento.alunos;
                        aplicacaoOferecimento.CriarPresenca(item_presenca);
                        //retorno.P3 = ("<label class=\"checkbox\"><input id = \"chkAlunoPresenca_" + qIdAula + "_" + elemento.id_aluno.ToString() + "\" type=\"checkbox\" name=\"chkAlunoPresenca_" + qIdAula + "_" + elemento.id_aluno.ToString() + "\"><span></span></label>");
                        retorno.P3 = ("<label class=\"checkbox\"><input id = \"chkAlunoPresenca_" + elemento.id_aluno.ToString() + "\" type=\"checkbox\" name=\"chkAlunoPresenca_" + elemento.id_aluno.ToString() + "\"><span></span></label>");
                        item = aplicacaoOferecimento.BuscaItem(item);
                        Session["oferecimentos"] = item;
                    }
                    retorno.P4 = "";
                    listaRetorno.Add(retorno);
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //cadOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fConfirmaPresencaAluno()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                oferecimentos item;

                string qPresencas = HttpContext.Current.Request["qPresencas"];
                string qIdAula = HttpContext.Current.Request["qIdAula"];

                var aAux = qPresencas.Split(';');

                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                item = (oferecimentos)Session["oferecimentos"];

                presenca item_presenca;

                foreach (var elemento in item.matricula_oferecimento.OrderBy(x => x.alunos.nome))
                {

                    item_presenca = new presenca();
                    item_presenca.id_oferecimento = item.id_oferecimento;
                    item_presenca.id_aula = Convert.ToInt32(qIdAula);
                    item_presenca.id_aluno = elemento.id_aluno;
                    item_presenca.presente = Array.IndexOf(aAux, elemento.id_aluno.ToString()) >= 0;
                    item_presenca.data_alteracao = DateTime.Now;
                    item_presenca.usuario = usuario.usuario;

                    aplicacaoOferecimento.AlterarPresenca(item_presenca);

                }

                item = aplicacaoOferecimento.BuscaItem(item);
                Session["oferecimentos"] = item;

                json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
                return;

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //====================================================================


        //=== Período Matrícula - Início =================================================================
        //matPeriodoMatriculaGestão
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheDisciplinaPeriodoMatricula()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                MatriculaAplicacao aplicacaoMatricula = new MatriculaAplicacao();
                periodo_matricula item;
                item = (periodo_matricula)Session["periodo_matricula"];
                List<periodo_matricula> lista = new List<periodo_matricula>();
                //lista = aplicacaoCurso.ListaDisciplinas(item);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (item.pre_oferecimentos.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in item.pre_oferecimentos)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_disciplina.ToString();
                        retorno.P1 = elemento.disciplinas.codigo;
                        retorno.P2 = elemento.disciplinas.nome;
                        retorno.P3 = elemento.vagas.ToString();
                        retorno.P4 = elemento.dia_semana;
                        retorno.P5 = "<div title=\"Editar Disciplina\"> <a class=\"btn btn-primary  btn-circle  fa fa-edit\" href=\'javascript:AbreModalEditarDisciplina(\""
                        + elemento.id_pre_oferecimento.ToString() + "\",\""
                        + elemento.disciplinas.id_disciplina + "\",\""
                        + elemento.disciplinas.codigo + "\",\""
                        + elemento.disciplinas.nome + "\",\""
                        + elemento.vagas + "\",\""
                        + elemento.dia_semana + "\")\'; ></a></div>";

                        if (elemento.matricula.Count ==0)
                        {
                            retorno.P6 = "<div title=\"Excluir Disciplina\"> <a class=\"btn btn-danger  btn-circle  fa fa-eraser\" href=\'javascript:AbreModalApagarDisciplina(\""
                            + elemento.id_pre_oferecimento.ToString() + "\",\""
                            + elemento.disciplinas.id_disciplina + "\",\""
                            + elemento.disciplinas.codigo + "\",\""
                            + elemento.disciplinas.nome + "\",\""
                            + elemento.vagas + "\",\""
                            + elemento.dia_semana + "\")\'; ></a></div>";
                        }
                        else
                        {
                            retorno.P6 = "<div title=\"Já há alunos Matriculados.\"> <i class=\"fa fa-ban\"></i></div>";
                        }

                        retorno.P7 = elemento.disciplinas.cursos_disciplinas.FirstOrDefault().cursos.nome;

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //matPeriodoMatriculaGestão
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPerquisaDisciplinaDisponivelPeridodoMatricula()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string sCodigo = HttpContext.Current.Request["qCodigo"];
                string sNome = HttpContext.Current.Request["qNome"];

                MatriculaAplicacao aplicacaoMatricula = new MatriculaAplicacao();
                periodo_matricula item;
                item = (periodo_matricula)Session["periodo_matricula"];
                disciplinas pItemDisciplina = new disciplinas();
                pItemDisciplina.codigo = sCodigo.Trim();
                pItemDisciplina.nome = sNome.Trim();
                List<disciplinas> lista = new List<disciplinas>();
                lista = aplicacaoMatricula.ListaDisciplinasDisponiveis(item, pItemDisciplina);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_disciplina.ToString();
                        retorno.P1 = elemento.nome;
                        retorno.P2 = elemento.codigo;
                        retorno.P3 = ("<div title=\"Incluir Disciplina\"> <a class=\"btn btn-success  btn-circle  fa fa-plus\" href=\'javascript:AbreModalIncluirDisciplina(\""
                        + (elemento.id_disciplina.ToString() + ("\",\""
                        + (elemento.codigo + ("\",\""
                        + (elemento.nome + "\")\'; ></a></div>"))))));
                        retorno.P4 = elemento.cursos_disciplinas.FirstOrDefault().cursos.nome;
                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //matPeriodoMatriculaGestão
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fIncluiDisciplinaPeriodoMatricula()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];
                string qVaga = HttpContext.Current.Request["qVaga"];
                string qDia = HttpContext.Current.Request["qDia"];

                MatriculaAplicacao aplicacaoMatricula = new MatriculaAplicacao();
                periodo_matricula item;
                item = (periodo_matricula)Session["periodo_matricula"];

                pre_oferecimentos pItem = new pre_oferecimentos();

                pItem.id_periodo_matricula = item.id_periodo;
                pItem.id_disciplina = Convert.ToInt32(qId);
                pItem.vagas = Convert.ToInt32(qVaga);
                pItem.dia_semana = qDia;
                pItem.estado = "Pendente";
                pItem.data_cadastro = DateTime.Now;
                pItem.data_alteracao = pItem.data_cadastro;
                pItem.usuario = usuario.usuario;
                if (aplicacaoMatricula.CriaPreOferecimento(pItem))
                {
                    item = aplicacaoMatricula.BuscaPeriodoMatricula(item);
                    Session["periodo_matricula"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na inclusão da Disciplina. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //matPeriodoMatriculaGestão
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fAlteraDisciplinaPeriodoMatricula()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];
                string qVaga = HttpContext.Current.Request["qVaga"];
                string qDia = HttpContext.Current.Request["qDia"];

                MatriculaAplicacao aplicacaoMatricula = new MatriculaAplicacao();
                periodo_matricula item;
                item = (periodo_matricula)Session["periodo_matricula"];

                pre_oferecimentos pItem = new pre_oferecimentos();

                pItem.id_pre_oferecimento = Convert.ToInt32(qId);
                pItem.vagas = Convert.ToInt32(qVaga);
                pItem.dia_semana = qDia;
                pItem.estado = "Pendente";
                pItem.data_alteracao = DateTime.Now;
                pItem.usuario = usuario.usuario;

                if (aplicacaoMatricula.AlteraPreOferecimento(pItem))
                {
                    item = aplicacaoMatricula.BuscaPeriodoMatricula(item);
                    Session["periodo_matricula"] = item;
                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na alteração dos dados da Disciplina. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //matPeriodoMatriculaGestão
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fExcluiDisciplinaPeriodoMatricula()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId = HttpContext.Current.Request["qId"];
                string qVaga = HttpContext.Current.Request["qVaga"];
                string qDia = HttpContext.Current.Request["qDia"];

                MatriculaAplicacao aplicacaoMatricula = new MatriculaAplicacao();
                periodo_matricula item;
                item = (periodo_matricula)Session["periodo_matricula"];

                pre_oferecimentos pItem = new pre_oferecimentos();

                pItem.id_pre_oferecimento = Convert.ToInt32(qId);

                if (aplicacaoMatricula.ExcluiPreOferecimento(pItem))
                {
                    item = aplicacaoMatricula.BuscaPeriodoMatricula(item);
                    Session["periodo_matricula"] = item;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na exclusão da Disciplina. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }
        //=== matPeriodoMatriculaGestão - Fim =================================================================

        //=== matConfOferecimentoGestao - Início =================================================================

        //matConfOferecimentoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheAlunoConfirmacaoMatricula()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 55))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Você não tem permissão para executar essa tarefa.\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                pre_oferecimentos item = (pre_oferecimentos)Session["pre_oferecimentos"];

                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (item.matricula.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in item.matricula)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.alunos.idaluno.ToString(); 
                        retorno.P1 = elemento.alunos.nome;
                        OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                        matricula_oferecimento item_matriculaOferecimento = new matricula_oferecimento();
                        item_matriculaOferecimento.id_aluno = elemento.id_aluno;
                        item_matriculaOferecimento.id_turma = elemento.id_turma;
                        oferecimentos item_ofereciemnto = new oferecimentos();
                        item_matriculaOferecimento.oferecimentos = new oferecimentos();
                        item_matriculaOferecimento.oferecimentos.id_disciplina = item.id_disciplina;
                        item_matriculaOferecimento.oferecimentos.quadrimestre = item.periodo_matricula.quadrimestre;
                        item_matriculaOferecimento = aplicacaoOferecimento.BuscaMatriculaOferecimento(item_matriculaOferecimento);

                        if (item_matriculaOferecimento != null)
                        {
                            retorno.P2 = ("<label class=\"checkbox\"><input class=\"sim\" id = \"chkAlunoConfMatricula_" + elemento.id_aluno.ToString() + "_" + elemento.id_turma.ToString() + "_" + item_matriculaOferecimento.id_matricula_oferecimento + "_" + item_matriculaOferecimento.id_oferecimento + "\" type=\"checkbox\" name=\"chkAlunoConfMatricula_" + elemento.id_aluno.ToString() + "_" + elemento.id_turma.ToString() + "_" + item_matriculaOferecimento.id_matricula_oferecimento + "_" + item_matriculaOferecimento.id_oferecimento + "\" checked><span></span></label>");
                            retorno.P3 = "<div title=\"Aluno Matriculado\"> <a class=\"text-bold btn fa fa-thumbs-o-up\" href=\'#\'; ></a></div>";
                        }
                        else
                        {
                            retorno.P2 = ("<label class=\"checkbox\"><input class=\"nao\" id = \"chkAlunoConfMatricula_" + elemento.id_aluno.ToString() + "_" + elemento.id_turma.ToString() + "\" type=\"checkbox\" name=\"chkAlunoConfMatricula_" + elemento.id_aluno.ToString() + "_" + elemento.id_turma.ToString() + "\"><span></span></label>");
                            retorno.P3 = "<div title=\"Excluir inscrição\"> <a class=\"btn btn-danger btn-circle fa fa-eraser\" href=\'javascript:fModalExcluirInscricao(\""
                            + elemento.id_matricula + "\",\"" + elemento.alunos.nome + "\")\'; ></a></div>";
                        }

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }


        //matConfOferecimentoGestao =================
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fConfirmaConfMatricula()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 55))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Você não tem permissão para executar essa tarefa.\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qMatricular = HttpContext.Current.Request["qMatricular"];
                string qDesmatricular = HttpContext.Current.Request["qDesmatricular"];

                pre_oferecimentos item = (pre_oferecimentos)Session["pre_oferecimentos"];

                if (qMatricular != "")
                {
                    var qAluno = qMatricular.Split(';');
                    //Verificar se tem o oferecimento
                    OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                    oferecimentos item_ofereciemnto = new oferecimentos();
                    item_ofereciemnto.id_disciplina = item.id_disciplina;
                    item_ofereciemnto.quadrimestre = item.periodo_matricula.quadrimestre;
                    item_ofereciemnto.num_oferecimento = 1;

                    item_ofereciemnto = aplicacaoOferecimento.BuscaItem(item_ofereciemnto);

                    if (item_ofereciemnto == null)
                    {
                        //Não tem o oferecimento... então vai criar
                        item_ofereciemnto = new oferecimentos();

                        item_ofereciemnto.id_disciplina = item.disciplinas.id_disciplina;
                        item_ofereciemnto.ativo = true;
                        item_ofereciemnto.num_oferecimento = 1;
                        item_ofereciemnto.quadrimestre = item.periodo_matricula.quadrimestre;
                        item_ofereciemnto.num_max_alunos = item.vagas;

                        item_ofereciemnto.creditos = item.disciplinas.creditos;
                        item_ofereciemnto.carga_horaria = item.disciplinas.carga_horaria;

                        item_ofereciemnto.objetivo = item.disciplinas.objetivo;
                        item_ofereciemnto.justificativa = item.disciplinas.justificativa;
                        item_ofereciemnto.ementa = item.disciplinas.ementa;
                        item_ofereciemnto.forma_avaliacao = item.disciplinas.forma_avaliacao;
                        item_ofereciemnto.material_utilizado = item.disciplinas.material_utilizado;
                        item_ofereciemnto.metodologia = item.disciplinas.metodologia;
                        item_ofereciemnto.conhecimentos_previos = item.disciplinas.conhecimentos_previos;
                        item_ofereciemnto.programa_disciplina = item.disciplinas.programa_disciplina;
                        item_ofereciemnto.bibliografia_basica = item.disciplinas.bibliografia_basica;
                        item_ofereciemnto.bibliografica_compl = item.disciplinas.bibliografica_compl;
                        item_ofereciemnto.observacao = item.disciplinas.observacao;

                        item_ofereciemnto.status = "cadastrado";
                        item_ofereciemnto.data_cadastro = DateTime.Now;
                        item_ofereciemnto.data_alteracao = DateTime.Now;
                        item_ofereciemnto.usuario = usuario.usuario;

                        item_ofereciemnto = aplicacaoOferecimento.CriarItem(item_ofereciemnto);

                        DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                        disciplinas itemDisciplina = new disciplinas();
                        oferecimentos_professores itemOferecimentoProfessor;

                        itemDisciplina.id_disciplina = item.id_disciplina;
                        itemDisciplina = aplicacaoDisciplina.BuscaItem(itemDisciplina);

                        foreach (var itemCada in itemDisciplina.disciplinas_professores)
                        {
                            itemOferecimentoProfessor = new oferecimentos_professores();
                            itemOferecimentoProfessor.id_oferecimento = item_ofereciemnto.id_oferecimento;
                            itemOferecimentoProfessor.id_professor = itemCada.id_professor;
                            itemOferecimentoProfessor.tipo_professor = itemCada.tipo_professor;
                            itemOferecimentoProfessor.responsavel = itemCada.responsavel;
                            itemOferecimentoProfessor.status = "cadastrado";
                            itemOferecimentoProfessor.data_cadastro = DateTime.Now;
                            itemOferecimentoProfessor.data_alteracao = DateTime.Now;
                            itemOferecimentoProfessor.usuario = usuario.usuario;
                            aplicacaoOferecimento.IncluirProfessor_Tecnico_Oferecimento(itemOferecimentoProfessor);
                        }
                    }

                    //Pronto, agora já tem Oferecimento.
                    //Agora é criar a Matricula_oferecimento
                    matricula_oferecimento pItem_matricula_oferecimento;
                    notas pItem_notas;
                    int qIdAluno;
                    int qIdTurma;
                    int qPosicao;

                    foreach (var elemento in qAluno)
                    {
                        pItem_matricula_oferecimento = new matricula_oferecimento();
                        pItem_notas = new notas();
                        qPosicao = elemento.IndexOf("_") +1;
                        qIdAluno = Convert.ToInt32(elemento.Substring(0, qPosicao-1));
                        qPosicao = elemento.Length - qPosicao;
                        qIdTurma = Convert.ToInt32(elemento.Substring(elemento.IndexOf("_") +1 , qPosicao));

                        pItem_matricula_oferecimento.id_oferecimento = item_ofereciemnto.id_oferecimento;

                        pItem_matricula_oferecimento.id_aluno = qIdAluno;
                        pItem_matricula_oferecimento.id_turma = qIdTurma;
                        pItem_matricula_oferecimento.status = "cadastrado";
                        pItem_matricula_oferecimento.data_matricula = DateTime.Now;
                        pItem_matricula_oferecimento.usuario = usuario.usuario;

                        pItem_notas.id_oferecimento = item_ofereciemnto.id_oferecimento;
                        pItem_notas.id_aluno = Convert.ToInt32(qIdAluno);
                        pItem_notas.conceito = null;
                        pItem_notas.autorizado = "";
                        pItem_notas.data_cadastro = DateTime.Now;
                        pItem_notas.data_alteracao = pItem_notas.data_cadastro;
                        pItem_notas.usuario = usuario.usuario;

                        aplicacaoOferecimento.MatricularAluno(pItem_matricula_oferecimento, pItem_notas);
                        
                    }

                }

                if (qDesmatricular != "")
                {
                    var qAluno = qDesmatricular.Split(';');
                    OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                    foreach (var elemento in qAluno)
                    {
                        var qElementos = elemento.Split('_');
                        aplicacaoOferecimento.Excluir_Aluno_matricula_oferecimento(Convert.ToInt32(qElementos[0]), Convert.ToInt32(qElementos[2]), Convert.ToInt32(qElementos[3]));
                    }
                }

                MatriculaAplicacao aplicacaoMatricula = new MatriculaAplicacao();
                item.estado = "Confirmado";
                aplicacaoMatricula.AlteraPreOferecimento(item);

                item = aplicacaoMatricula.BuscaPreOferecimento(item);
                Session["pre_oferecimentos"] = item;

                json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
                return;

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //=== matConfOferecimentoGestao - Fim =================================================================

        //=== finInadimplente - Início =================================================================
        //finInadimplente
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheGradeInadimplente()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 49))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Você não tem permissão para executar essa tarefa.\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                InadimplenteAplicacao aplicacaoinadimplentes = new InadimplenteAplicacao();
                List<inadimplentes> lista = new List<inadimplentes>();
                inadimplentes item = new inadimplentes();
                item.id_aluno = 0;
                if (HttpContext.Current.Request["qMatricula"].Trim() != "")
                {
                    item.id_aluno = Convert.ToInt32(HttpContext.Current.Request["qMatricula"]);
                }
                item.alunos = new alunos();
                item.alunos.nome = HttpContext.Current.Request["qNome"].Trim();

                lista = aplicacaoinadimplentes.ListaItem(item);

                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_aluno.ToString();
                        retorno.P1 = elemento.alunos.nome;
                        retorno.P2 = String.Format("{0:dd/MM/yyyy}", elemento.data);
                        retorno.P3 = "<div title=\"Excluir Aluno da lista de inadimplentes\"> <a class=\"btn btn-danger btn-circle fa fa-eraser\" href=\'javascript:fModalExcluirAluno(\""
                                + elemento.id_aluno + "\",\"" + elemento.alunos.nome + "\")\'; ></a></div>";
                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //finInadimplente
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fExcluirAlunoListaInadimplente()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 49))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Você não tem permissão para executar essa tarefa.\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qMatricula = HttpContext.Current.Request["qMatricula"];
                string qNome = HttpContext.Current.Request["qNome"];

                InadimplenteAplicacao aplicacaoInscricao = new InadimplenteAplicacao();

                if (aplicacaoInscricao.ApagarItem(Convert.ToInt32(qMatricula)))
                {

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na Exclusão de Aluno " + qNome + " da lista de inadimplentes. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //finInadimplente
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPesquisaAlunoDisponivelInadimplente()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 49))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Você não tem permissão para executar essa tarefa.\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qMatricula = HttpContext.Current.Request["qMatricula"];
                string qNome = HttpContext.Current.Request["qNome"];

                InadimplenteAplicacao aplicacao = new InadimplenteAplicacao();
                alunos item = new alunos();

                item.idaluno = 0;
                if (qMatricula.Trim() != "")
                {
                    item.idaluno = Convert.ToInt32(qMatricula);
                }
                item.nome = qNome.Trim();

                List<alunos> lista = new List<alunos>();
                lista = aplicacao.ListaAlunosDisponiveis(item);
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.idaluno.ToString();
                        retorno.P1 = elemento.nome;
                        retorno.P2 = "<div title=\"Inclui aluno na lista de inadimplentes\"> <a class=\"btn btn-success  btn-circle fa fa-plus\" href=\'javascript:fAdicionarAlunoInadimplente(\""
                            + retorno.P0 + "\",\""
                            + retorno.P1 + "\")\'; ></a></div>";
                        
                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //finInadimplente
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fAdicionarAlunoInadimplente()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 49))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Você não tem permissão para executar essa tarefa.\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qMatricula = HttpContext.Current.Request["qMatricula"];
                string qNome = HttpContext.Current.Request["qNome"];

                InadimplenteAplicacao aplicacao = new InadimplenteAplicacao();
                inadimplentes item = new inadimplentes();
                item.id_aluno = Convert.ToInt32(qMatricula);
                item.data = DateTime.Now;
                item.usuario = usuario.usuario;

                item = aplicacao.CriarItem(item);

                if (item != null)
                {
                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na Inclusão de aluno na lista de inadimplentes. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //=== finInadimplente - Fim =================================================================

        //=== finRelBoleto - Início =================================================================

        //finRelBoleto
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheGradeBoleto()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 26))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Você não tem permissão para executar essa tarefa.\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                int qIdPeriodo = Convert.ToInt32(HttpContext.Current.Request["qIdPeriodo"]);
                int qIdCurso;
                if (HttpContext.Current.Request["qIdCurso"] == "")
                {
                    qIdCurso = 0;
                }
                else
                {
                    qIdCurso = Convert.ToInt32(HttpContext.Current.Request["qIdCurso"]);
                }
                
                int qSituacao = Convert.ToInt32(HttpContext.Current.Request["qSituacao"]);

                InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
                List<fichas_inscricao> lista = new List<fichas_inscricao>();

                List<Int32> qId = new List<Int32>();


                lista = aplicacaoInscricao.ListaCandidatosBoletos(qIdPeriodo, qIdCurso);

                if (qSituacao == 1)
                {
                    foreach (var elemento in lista)
                    {
                        if (elemento.boletos.Count != 0)
                        {
                            if (elemento.boletos.FirstOrDefault().data_pagamento == null)
                            {
                                qId.Add(elemento.id_inscricao);
                                //lista_aux.Add(new fichas_inscricao());
                            }
                        }
                    }
                }
                else if (qSituacao == 2)
                {
                    foreach (var elemento in lista)
                    {
                        if (elemento.boletos.Count != 0)
                        {
                            if (elemento.boletos.FirstOrDefault().data_pagamento != null)
                            {
                                qId.Add(elemento.id_inscricao);
                            }
                        }
                    }
                }

                lista = lista.Where(x => !qId.Contains(x.id_inscricao)).ToList();
                //lista = lista_aux;

                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.nome; 
                        retorno.P1 = elemento.cursos.nome;
                        if (elemento.boletos.Count == 0)
                        {
                            retorno.P2 = "Não tem Boleto";
                        }
                        else
                        {
                            retorno.P0 = elemento.boletos.FirstOrDefault().nome;
                            retorno.P2 = elemento.boletos.FirstOrDefault().refTran;
                            if (elemento.boletos.FirstOrDefault().data_pagamento != null)
                            {
                                retorno.P3 = String.Format("{0:dd/MM/yyyy}", elemento.boletos.FirstOrDefault().data_pagamento);
                                retorno.P4 = "<div title=\"Excluir data de Pagamento\"> <a class=\"btn btn-danger btn-circle fa fa-close\" href=\'javascript:fModalExcluirPagamento(\""
                                + elemento.id_inscricao + "\",\"" + elemento.nome + "\",\"" + elemento.cursos.nome + "\",\"" + elemento.boletos.FirstOrDefault().refTran + "\",\"" + String.Format("{0:yyyy-MM-dd}", elemento.boletos.FirstOrDefault().data_pagamento) + "\",\"" + elemento.boletos.FirstOrDefault().id_boleto + "\")\'; ></a></div>";

                            }
                            else
                            {
                                retorno.P3 = "";
                                retorno.P4 = "<div title=\"Pagar Boleto\"> <a class=\"btn btn-primary btn-circle fa fa-money\" href=\'javascript:fModalConfirmarPagamento(\""
                                + elemento.email_res + "\",\"" + elemento.id_inscricao + "\",\"" + elemento.nome + "\",\"" + elemento.cursos.nome + "\",\"" + elemento.boletos.FirstOrDefault().refTran + "\",\"" + String.Format("{0:yyyy-MM-dd}", DateTime.Today) + "\",\"" + elemento.boletos.FirstOrDefault().id_boleto + "\")\'; ></a></div>";
                            }
                        }
                        
                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //finRelBoleto
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fExcluirDataPagamento()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 26))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Você não tem permissão para executar essa tarefa.\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdInscricao = HttpContext.Current.Request["qIdInscricao"];
                string qNome = HttpContext.Current.Request["qNome"];
                string qRefTran = HttpContext.Current.Request["qRefTran"];
                string qData = HttpContext.Current.Request["qData"];
                string qIdBoleto = HttpContext.Current.Request["qIdBoleto"];

                InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
                boletos item_boleto = new boletos();

                item_boleto.id_boleto = Convert.ToInt32(qIdBoleto);

                if (aplicacaoInscricao.ExcluiDataPagamentoBoleto(item_boleto, Convert.ToInt32(qIdInscricao)))
                {

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na Exclusão da data de pagamento do candidato " + qNome + ". Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //finRelBoleto
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fConfirmarDataPagamento()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 26))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Você não tem permissão para executar essa tarefa.\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdInscricao = HttpContext.Current.Request["qIdInscricao"];
                string qNome = HttpContext.Current.Request["qNome"];
                string qRefTran = HttpContext.Current.Request["qRefTran"];
                string qData = HttpContext.Current.Request["qData"];
                string qIdBoleto = HttpContext.Current.Request["qIdBoleto"];
                string qEmail = HttpContext.Current.Request["qEmail"];

                InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
                boletos item_boleto = new boletos();
                historico_inscricao item_historico = new historico_inscricao();

                item_boleto.id_boleto = Convert.ToInt32(qIdBoleto);
                item_boleto.data_pagamento = Convert.ToDateTime(qData);
                item_boleto.data_alteracao = DateTime.Now;
                item_boleto.usuario = usuario.usuario;

                item_historico.id_inscricao = Convert.ToInt32(qIdInscricao);
                item_historico.data = item_boleto.data_alteracao.Value;
                item_historico.status = "Inscrição Paga";
                item_historico.usuario = usuario.usuario;

                if (aplicacaoInscricao.IncluirDataPagamentoBoleto(item_boleto, item_historico))
                {
                    AreaAplicacao aplicacaoArea = new AreaAplicacao();
                    GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                    Configuracoes item_configuracoes;
                    fichas_inscricao item_ficha = new fichas_inscricao();
                    areas_concentracao item_area = new areas_concentracao();
                    item_ficha.id_inscricao = Convert.ToInt32(qIdInscricao);
                    item_ficha = aplicacaoInscricao.BuscaItem_Inscricao(item_ficha);

                    // 1 = email mestrado@ipt.br
                    // 2 = email suporte@ipt.br
                    item_configuracoes = aplicacaoGerais.BuscaConfiguracoes(1);

                    string sFrom = item_configuracoes.remetente_email;
                    string sFrom_Nome = item_configuracoes.nome_remetente_email;
                    string sTo = qEmail;
                    string sAssunto = "Confirmação de Recebimento de Pagamento";
                    string sCorpo;

                    StreamReader objReader;

                    if (item_ficha.cursos.id_tipo_curso == 1)
                    {
                        objReader = new StreamReader(HttpRuntime.AppDomainAppPath + "\\Templates\\emails\\ConfirmacaoPagamentoInscricao.html");
                    }
                    else
                    {
                        objReader = new StreamReader(HttpRuntime.AppDomainAppPath + "\\Templates\\emails\\ConfirmacaoPagamentoInscricaoSemProva.html");
                    }

                    sCorpo = objReader.ReadToEnd();
                    objReader.Close();

                    sCorpo = sCorpo.Replace("{nome}", item_ficha.nome);
                    sCorpo = sCorpo.Replace("{valor_inscricao}", item_ficha.periodo_inscricao_curso.valor.ToString());
                    sCorpo = sCorpo.Replace("{inscricao_numero}", item_ficha.id_inscricao + "/" + item_ficha.periodo_inscricao.quadrimestre);
                    sCorpo = sCorpo.Replace("{curso}", item_ficha.periodo_inscricao_curso.cursos.nome);
                    if (item_ficha.id_area_concentracao != null)
                    {
                        item_area.id_area_concentracao = item_ficha.id_area_concentracao.Value;
                        item_area = aplicacaoArea.BuscaItem(item_area);
                        sCorpo = sCorpo.Replace("{area_concentracao}", item_area.nome);
                    }
                    else
                    {
                        sCorpo = sCorpo.Replace("{area_concentracao}", "");
                    }
                    
                    sCorpo = sCorpo.Replace("{data_inscricao}", String.Format("{0:dd/MM/yyyy HH:mm}", item_ficha.data_inscricao));

                    Utilizades.fEnviaEmail(sFrom, sFrom_Nome, sTo, "", "", sAssunto, sCorpo, item_configuracoes.servidor_email, item_configuracoes.conta_email, item_configuracoes.senha_email, item_configuracoes.porta_email.Value, 1, "");

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na Exclusão da data de pagamento do candidato " + qNome + ". Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //=== finRelBoleto - Fim =================================================================

        //=== aluDadosPessoais - Inicio =================================================================
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fSalvaFoto()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();

                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                //if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 43) && !usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 47)) // 43 - Portal do Aluno e 47 - Dados Cadastrais - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
                //{
                //    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                //    this.Context.Response.ContentType = "application/json; charset=utf-8";
                //    this.Context.Response.Write(json);
                //    return;
                //}

                HttpPostedFile qArquivo = HttpContext.Current.Request.Files["qArquivo"];

                if (qArquivo == null)
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Sem imagem\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (qArquivo.ContentLength > 0)
                {
                    string qExt = Path.GetExtension(qArquivo.FileName);
                    if (qArquivo.ContentLength > 1048576)
                    {
                        json = "[{\"P0\":\"Erro\",\"P1\":\"A foto deve ter, no máximo, 1 Megabyte\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                    }
                    else if (qExt.ToUpper() != ".JPG" && qExt.ToUpper() != ".JPEG" && qExt.ToUpper() != ".PNG" )
                    {
                        json = "[{\"P0\":\"Erro\",\"P1\":\"A foto deve ter a extenção JPG ou JPEG ou PNG\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                    }
                    else
                    {
                        UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();

                        string qNomeArquivo = usuario.usuario + qExt;

                        //string qNomeArquivo = item.idaluno.ToString() + qExt;

                        int qTamanhoArquivo = qArquivo.ContentLength;
                        byte[] arrayByte = new byte[qTamanhoArquivo];
                        qArquivo.InputStream.Read(arrayByte, 0, qTamanhoArquivo);

                        qArquivo.SaveAs(Server.MapPath("") + "\\img\\pessoas\\" + qNomeArquivo);
                        usuario.avatar = qNomeArquivo;

                        aplicacaoUsuario.AlterarUsuario(usuario);

                        Session["UsuarioLogado"] = usuario;


                        //if (usuario.usuario == item.idaluno.ToString())
                        //{

                        json = "[{\"P0\":\"ok\",\"P1\":\"trocar\",\"P2\":\"/img/pessoas/" + qNomeArquivo + "?" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now) + "\",\"P3\":\"\"}]";
                            this.Context.Response.ContentType = "application/json; charset=utf-8";
                            this.Context.Response.Write(json);
                        //}
                        //else
                        //{
                        //    json = "[{\"P0\":\"ok\",\"P1\":\"nao\",\"P2\":\"/img/pessoas/" + qNomeArquivo + "?" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now) + "\",\"P3\":\"\"}]";
                        //    this.Context.Response.ContentType = "application/json; charset=utf-8";
                        //    this.Context.Response.Write(json);
                        //}
                        
                    }
                    
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Sem imagem\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                }

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //=== aluDadosPessoais - Fim =================================================================

        //=== aluSitAcademica - Inicio =================================================================
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreenchePreMatricula()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 43) && !usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 47)) // 43 - Portal do Aluno e 47 - Dados Cadastrais - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                alunos item_aluno = new alunos();
                //string qTab = HttpContext.Current.Request["qTab"];
                //item_aluno = (alunos)Session[qTab + "Aluno"];
                item_aluno = (alunos)Session["AlunoLogado"];

                int qIdTurma = Convert.ToInt32(HttpContext.Current.Request["qIdTurma"]);

                //Verificar se está inadinplente
                if (item_aluno.inadimplentes != null)
                {
                    json = ("[{\"P0\":\"Aviso\",\"P1\":\""
                        + "<span class=\'text-danger\'><strong>Aluno Inadimplente</strong></span><br> <br>Por favor entre em contato com o departamento financeiro.<br>(sr. Lindomar T.: (11) 3769-6918 email: financeiro@fipt.org.br)" + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\"}]");
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                matricula_turma item_matricula = item_aluno.matricula_turma.Where(x => x.id_turma == qIdTurma).FirstOrDefault();

                //Verificar se está Abandocou
                if (item_matricula.historico_matricula_turma.Any(x => x.situacao == "Abandonou"))
                {
                    json = ("[{\"P0\":\"Aviso\",\"P1\":\""
                        + "<span><strong>Situação: Abandonou</strong></span><br> <br>Para mais informações entre em contato com a secretaria. <br> T.: (11) 3767-4068 - email: mestrado@ipt.br" + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\"}]");
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                //Verificar se está Desligado
                if (item_matricula.historico_matricula_turma.Any(x => x.situacao == "Desligado"))
                {
                    json = ("[{\"P0\":\"Aviso\",\"P1\":\""
                        + "<span><strong>Situação: Desligado</strong></span><br> <br>Para mais informações entre em contato com a secretaria. <br> T.: (11) 3767-4068 - email: mestrado@ipt.br" + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\"}]");
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                //Verificar se está Titulado
                if (item_matricula.historico_matricula_turma.Any(x => x.situacao == "Titulado"))
                {
                    json = ("[{\"P0\":\"Aviso\",\"P1\":\""
                        + "<span><strong>Situação: Titulado</strong></span><br> <br>Para mais informações entre em contato com a secretaria. <br> T.: (11) 3767-4068 - email: mestrado@ipt.br" + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\"}]");
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                //Verificar se está Trancado
                if (item_matricula.historico_matricula_turma.Any(x => x.situacao == "Trancado"))
                {
                    if (item_matricula.historico_matricula_turma.Where(x => x.situacao == "Trancado").FirstOrDefault().data_fim >= DateTime.Today)
                    {
                        json = ("[{\"P0\":\"Aviso\",\"P1\":\""
                        + "<span><strong>Situação: Trancado</strong></span><br> <br>Para mais informações entre em contato com a secretaria. <br> T.: (11) 3767-4068 - email: mestrado@ipt.br" + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\"}]");
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                        return;
                    }
                }

                int qIdCurso = item_aluno.matricula_turma.Where(x => x.id_turma == qIdTurma).FirstOrDefault().turmas.id_curso;

                MatriculaAplicacao aplicacaoMatricula = new MatriculaAplicacao();
                List<pre_oferecimentos> lista_pre_oferecimento = new List<pre_oferecimentos>();
                lista_pre_oferecimento = aplicacaoMatricula.ListaPreOferecimentoValidos(qIdCurso);

                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                bool bAberto;

                if (lista_pre_oferecimento.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista_pre_oferecimento)
                    {
                        //Verificar se o aluno já fez essa disciplina antes.
                        //Caso tenha feito, se a nota for "C" ou "B" ou "A" então não mostrar essa disciplina de novo
                        if (aplicacaoMatricula.VerificaAlunoCursouDisciplina(Convert.ToInt32(elemento.id_disciplina), Convert.ToInt32(item_aluno.idaluno), Convert.ToInt32(item_matricula.id_turma),elemento.periodo_matricula.quadrimestre))
                        {
                            continue;
                        }

                        int qNumMaxinoDisciplinaCurso = 0;
                        int qNumQuantasDisciplinasObrigatorias = 0;
                        int qEssa_e_Obrigatoria = 0;
                        int qQdtnObrigatorriaAlunoFez = 0;
                        int qQtdDiciplinaAlunoPodeFazer = 0;

                        OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();

                        //Verificar aqui se é um curso de Mestrado
                        if (item_matricula.turmas.cursos.id_tipo_curso == 1)
                        {
                            //Verificar aqui qual é o número máximo de disciplinas do curso, se null então deixa zero e não conta nada
                            if (item_matricula.turmas.cursos.num_max_disciplinas == null)
                            {
                                qNumMaxinoDisciplinaCurso = 0;
                            }
                            else
                            {
                                qNumMaxinoDisciplinaCurso = item_matricula.turmas.cursos.num_max_disciplinas.Value;

                                qNumQuantasDisciplinasObrigatorias = item_matricula.turmas.cursos.cursos_disciplinas.Where(x => x.obrigatoria == 1).Count();

                                //Verificar se é uma disciplina obrigatória para esse curso
                                if (item_matricula.turmas.cursos.cursos_disciplinas.Any(x => x.id_disciplina == elemento.id_disciplina && x.obrigatoria == 1))
                                {
                                    qEssa_e_Obrigatoria = 1;
                                }
                                else
                                {
                                    int idOferecimentoAnterior = 0;
                                    matricula_oferecimento item_matricula_oferecimento = new matricula_oferecimento();
                                    item_matricula_oferecimento.id_aluno = item_matricula.id_aluno;
                                    item_matricula_oferecimento.id_turma = item_matricula.id_turma;
                                    List<oferecimentos> lista_oferecimento_nObriatorio = aplicacaoOferecimento.Oferecimentos_nObrigatorios_cursados_aluno(item_matricula_oferecimento, item_matricula.turmas.cursos.id_curso);
                                    if (lista_oferecimento_nObriatorio.Count() > 0)
                                    {
                                        foreach (var itemoferecimento in lista_oferecimento_nObriatorio)
                                        {
                                            if (itemoferecimento.disciplinas.nome.ToLower().IndexOf("acompanhamento da dissertação") == -1)
                                            {
                                                //Colocado esse trecha para NÃO considerar a disciplina de "Acompnhamento da Dissertação" caso o Aluno tenha feito
                                                foreach (var nota in itemoferecimento.notas.Where(x => x.id_aluno == item_matricula.id_aluno))
                                                {
                                                    if ((nota.conceito == "A" || nota.conceito == "B" || nota.conceito == "C") && ((Convert.ToInt32(nota.id_oferecimento) != idOferecimentoAnterior) || idOferecimentoAnterior == 0))
                                                    {
                                                        qQdtnObrigatorriaAlunoFez++;
                                                    }
                                                    idOferecimentoAnterior = Convert.ToInt32(nota.id_oferecimento);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        qQtdDiciplinaAlunoPodeFazer = (qNumMaxinoDisciplinaCurso - qNumQuantasDisciplinasObrigatorias) - qQdtnObrigatorriaAlunoFez;

                        if (qQtdDiciplinaAlunoPodeFazer <= 0 && qEssa_e_Obrigatoria == 0 && elemento.disciplinas.nome.ToLower().IndexOf("acompanhamento da dissertação") == -1)
                        {
                            //Colocado esse trecha para NÃO considerar a disciplina de "Acompnhamento da Dissertação" caso seja essa a oferecida
                            continue;
                        }

                        retorno = new retornoGeral();
                        retorno.P0 = elemento.disciplinas.codigo;
                        retorno.P1 = elemento.disciplinas.nome;
                        retorno.P2 = (elemento.vagas - elemento.matricula.Count).ToString() + "/" + elemento.vagas.ToString();
                        if ((elemento.vagas - elemento.matricula.Count) != 0)
                        {
                            bAberto = true;
                        }
                        else
                        {
                            bAberto = false;
                        }
                        retorno.P3 = elemento.dia_semana;

                        if (elemento.matricula.Any(x=> x.id_aluno == item_aluno.idaluno && x.pre_oferecimentos.Any(y=> y.id_pre_oferecimento == elemento.id_pre_oferecimento)))
                        {
                            //Está pré-matriculado
                            oferecimentos item_ofereciemnto = new oferecimentos();
                            item_ofereciemnto.id_disciplina = elemento.id_disciplina;
                            item_ofereciemnto.quadrimestre = elemento.periodo_matricula.quadrimestre;
                            item_ofereciemnto.num_oferecimento = 1;

                            item_ofereciemnto = aplicacaoOferecimento.BuscaItem(item_ofereciemnto);
                            if (item_ofereciemnto != null)
                            {
                                //Tem o oferecimento
                                //Verificar se o aluno está matriculado nele
                                if (item_ofereciemnto.matricula_oferecimento.Any(x=> x.id_aluno == item_aluno.idaluno))
                                {
                                    //Está matriculado no Oferecimento, então não pode desmatricular
                                    retorno.P4 = "<div title=\"Você já está matriculado.\"><i class=\"fa fa-thumbs-o-up text-primary text-bold\"></i></div>";
                                }
                                else
                                {
                                    //Não está matriculado no Oferecimento, então pode sim desmatricular
                                    retorno.P4 = ("<label class=\"checkbox\"><input class=\"sim\" id = \"chkAlunoMatricula_" + qIdTurma + "_" + elemento.periodo_matricula.id_periodo + "_" + elemento.matricula.FirstOrDefault().id_matricula + "_" + elemento.id_pre_oferecimento + "\" type=\"checkbox\" name=\"chkAlunoMatricula_" + qIdTurma + "_" + elemento.periodo_matricula.id_periodo + "_" + elemento.matricula.FirstOrDefault().id_matricula + "_" + elemento.id_pre_oferecimento + "\" checked><span></span></label>");
                                }
                            }
                            else
                            {
                                //Não tem o oferecimento ainda, então pode sim desmatricular
                                retorno.P4 = ("<label class=\"checkbox\"><input class=\"sim\" id = \"chkAlunoMatricula_" + qIdTurma + "_" + elemento.periodo_matricula.id_periodo + "_" + elemento.matricula.FirstOrDefault().id_matricula + "_" + elemento.id_pre_oferecimento + "\" type=\"checkbox\" name=\"chkAlunoMatricula_" + qIdTurma + "_" + elemento.periodo_matricula.id_periodo + "_" + elemento.matricula.FirstOrDefault().id_matricula + "_" + elemento.id_pre_oferecimento + "\" checked><span></span></label>");    
                            }
                        }
                        else if (elemento.matricula.Any(x => x.id_aluno == item_aluno.idaluno))
                        {
                            //Não está matriculado mas já tem regsitro de outra disciplina matriculada (Tabela matricula)
                            if (bAberto)
                            {
                                retorno.P4 = ("<label class=\"checkbox\"><input class=\"nao\" id = \"chkAlunoMatricula_" + qIdTurma + "_" + elemento.periodo_matricula.id_periodo + "_" + elemento.id_pre_oferecimento + "_" + elemento.matricula.FirstOrDefault() + "\" type=\"checkbox\" name=\"chkAlunoMatricula_" + qIdTurma + "_" + elemento.periodo_matricula.id_periodo + "_" + elemento.id_pre_oferecimento + "_" + elemento.matricula.FirstOrDefault() + "\"><span></span></label>");
                            }
                            else
                            {
                                retorno.P4 = "<div title=\"Essa disciplina já está lotada.\"><i class=\"fa fa-lock text-danger text-bold\"></i></div>";
                            }
                        }
                        else
                        {
                            //Não está matriculado
                            if (bAberto)
                            {
                                retorno.P4 = ("<label class=\"checkbox\"><input class=\"nao\" id = \"chkAlunoMatricula_" + qIdTurma + "_" + elemento.periodo_matricula.id_periodo + "_" + elemento.id_pre_oferecimento + "\" type=\"checkbox\" name=\"chkAlunoMatricula_" + qIdTurma + "_" + elemento.periodo_matricula.id_periodo + "_" + elemento.id_pre_oferecimento + "\"><span></span></label>");
                            }
                            else
                            {
                                retorno.P4 = "<div title=\"Essa disciplina já está lotada.\"><i class=\"fa fa-lock text-danger text-bold\"></i></div>";
                            }
                        }

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fConfirmaMatricula()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 43) && !usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 47)) // 43 - Portal do Aluno e 47 - Dados Cadastrais - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                alunos item_aluno;
                string qTab = HttpContext.Current.Request["qTab"];
                //item_aluno = (alunos)Session[qTab + "Aluno"];
                item_aluno = (alunos)Session["AlunoLogado"];

                string qMatricular = HttpContext.Current.Request["qMatricular"];
                string qDesmatricular = HttpContext.Current.Request["qDesmatricular"];

                MatriculaAplicacao aplicacaoMatricula = new MatriculaAplicacao();

                if (qMatricular != "")
                {
                    var qDisciplinas = qMatricular.Split(';');
                    matricula item_matricula;
                    pre_oferecimentos item_pre_ofereciemnto;

                    foreach (var elemento in qDisciplinas)
                    {
                        var qElementos = elemento.Split('_');

                        item_matricula = new matricula();
                        item_matricula.id_aluno = item_aluno.idaluno;
                        item_matricula.id_turma = Convert.ToInt32(qElementos[0]);
                        item_matricula.id_periodo_matricula = Convert.ToInt32(qElementos[1]);
                        item_matricula.data = DateTime.Now;

                        item_matricula = aplicacaoMatricula.BuscaMatriculaSemId(item_matricula);

                        if (item_matricula == null)
                        {
                            item_matricula = new matricula();
                            item_matricula.id_aluno = item_aluno.idaluno;
                            item_matricula.id_turma = Convert.ToInt32(qElementos[0]);
                            item_matricula.id_periodo_matricula = Convert.ToInt32(qElementos[1]);
                            item_matricula.data = DateTime.Now;
                            item_matricula = aplicacaoMatricula.CriaMatricula(item_matricula);
                        }

                        item_pre_ofereciemnto = new pre_oferecimentos();
                        item_pre_ofereciemnto.id_pre_oferecimento = Convert.ToInt32(qElementos[2]);

                        aplicacaoMatricula.CriaMatricula_PreOferecimento(item_matricula, item_pre_ofereciemnto);

                    }

                }

                if (qDesmatricular != "")
                {
                    var qDisciplinas = qDesmatricular.Split(';');
                    matricula item_matricula = new matricula();
                    pre_oferecimentos item_pre_ofereciemnto;

                    foreach (var elemento in qDisciplinas)
                    {
                        var qElementos = elemento.Split('_');

                        item_matricula = new matricula();
                        item_matricula.id_aluno = item_aluno.idaluno;
                        item_matricula.id_turma = Convert.ToInt32(qElementos[0]);
                        item_matricula.id_periodo_matricula = Convert.ToInt32(qElementos[1]);
                        item_matricula.data = DateTime.Now;

                        item_matricula = aplicacaoMatricula.BuscaMatriculaSemId(item_matricula);
                        
                        item_pre_ofereciemnto = new pre_oferecimentos();
                        item_pre_ofereciemnto.id_pre_oferecimento = Convert.ToInt32(qElementos[3]);

                        aplicacaoMatricula.ApagarMatricula_PreOferecimento(item_matricula, item_pre_ofereciemnto);

                    }
                }

                //MatriculaAplicacao aplicacaoMatricula = new MatriculaAplicacao();
                //item.estado = "Confirmado";
                //aplicacaoMatricula.AlteraPreOferecimento(item);

                //item = aplicacaoMatricula.BuscaPreOferecimento(item);
                //Session["pre_oferecimentos"] = item;

                json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
                return;


            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //=== aluDadosPessoais - Fim =================================================================

        //=== aluDadosPessoais - Início =================================================================

        //proRelacaoInscritosGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fVisualizarInscrito()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 5))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Você não tem permissão para executar essa tarefa.\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                int qIdPeriodo = Convert.ToInt32(HttpContext.Current.Request.Form["qIdInscricao"]);

                InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
                fichas_inscricao item_fichas_inscricao = new fichas_inscricao();
                item_fichas_inscricao.id_inscricao = qIdPeriodo;

                item_fichas_inscricao = aplicacaoInscricao.BuscaItem_Inscricao(item_fichas_inscricao);
                
                List<retornoGeral> listaRetorno = new List<retornoGeral>();
               
                retornoGeral retorno;
                
                retorno = new retornoGeral();
                if (item_fichas_inscricao != null)
                {
                    retorno.P0 = item_fichas_inscricao.cursos.nome;
                    if (item_fichas_inscricao.id_area_concentracao != null && item_fichas_inscricao.id_area_concentracao != 0)
                    {
                        AreaAplicacao aplicacaoArea = new AreaAplicacao();
                        areas_concentracao item_area = new areas_concentracao();
                        item_area.id_area_concentracao = item_fichas_inscricao.id_area_concentracao.Value;
                        item_area = aplicacaoArea.BuscaItem(item_area);
                        retorno.P1 = item_area.nome;
                    }
                    else
                    {
                        retorno.P1 = "";
                    }

                    retorno.P2 = Convert.ToUInt64(item_fichas_inscricao.cpf).ToString(@"000\.000\.000\-00");
                    retorno.P3 = item_fichas_inscricao.nome;
                    retorno.P4 = String.Format("{0:dd/MM/yyyy}", item_fichas_inscricao.data_nascimento);
                    if (item_fichas_inscricao.sexo.ToUpper() == "M")
                    {
                        retorno.P5 = "Masculino";
                    }
                    else
                    {
                        retorno.P5 = "Feminino";
                    }
                    retorno.P6 = Convert.ToUInt64(item_fichas_inscricao.cep_res).ToString(@"00000\-000");
                    retorno.P7 = item_fichas_inscricao.endereco_res;
                    retorno.P8 = item_fichas_inscricao.numero_res;
                    retorno.P9 = item_fichas_inscricao.complemento_res;
                    retorno.P10 = item_fichas_inscricao.bairro_res;
                    retorno.P11 = item_fichas_inscricao.cidade_res;
                    retorno.P12 = item_fichas_inscricao.estado_res;
                    retorno.P13 = item_fichas_inscricao.email_res;
                    retorno.P14 = item_fichas_inscricao.rg_rne;
                    retorno.P15 = item_fichas_inscricao.digito_rg;
                    if (item_fichas_inscricao.telefone_res != null & item_fichas_inscricao.telefone_res != "")
                    {
                        retorno.P16 = Convert.ToUInt64(item_fichas_inscricao.telefone_res).ToString(@"\(00\) 0000\-0000");
                    }
                    else
                    {
                        retorno.P16 = "";
                    }
                    retorno.P17 = Convert.ToUInt64(item_fichas_inscricao.celular_res).ToString(@"\(00\) 0\.0000\-0000");
                    retorno.P18 = item_fichas_inscricao.pesquisamala;
                    retorno.P19 = item_fichas_inscricao.pesquisaoutros;
                }
                
                listaRetorno.Add(retorno);

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //=== aluDadosPessoais - Fim =================================================================

        //=== Monitor - Início =================================================================

        //Monitor
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheMonitor()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 69))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Você não tem permissão para executar essa tarefa.\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                //int qIdPeriodo = Convert.ToInt32(HttpContext.Current.Request.Form["qIdInscricao"]);

                MonitorAplicacao aplicacaoMonitor = new MonitorAplicacao();

                monitor_letreiro item_letreiro = new monitor_letreiro();

                item_letreiro = aplicacaoMonitor.BuscaItem(item_letreiro);

                string sAux_letreiro = "";

                if (item_letreiro != null)
                {
                    if (item_letreiro.descricao != "")
                    {
                        sAux_letreiro = item_letreiro.descricao;
                    }
                }

                List<monitor> lista = aplicacaoMonitor.ListaItem_Ativos();

                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                retornoGeral retorno;

                int i = 1;
                if (lista.Count > 0)
                {
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = "avisos";
                        retorno.P1 = i + " de " + lista.Count().ToString();
                        retorno.P2 = elemento.DescEventoMonitor;
                        retorno.P3 = elemento.LocalEventoMonitor;
                        retorno.P4 = elemento.DataEventoMonitor;
                        retorno.P5 = elemento.HorarioEventoMonitor;
                        retorno.P6 = elemento.ResponsavelEventoMonitor;
                        retorno.P7 = sAux_letreiro; //'"A T E N Ç Ã O - Dia 26/07/2019 (sexta-feira) não haverá expediente na secretaria devido ao feriado prolongado."
                        listaRetorno.Add(retorno);
                        i++;
                    }

                }
                else
                {
                    List<monitor_video> lista_videos = aplicacaoMonitor.ListaItem_Videos();

                    if (lista_videos.Count > 0)
                    {
                        foreach (var elemento in lista_videos)
                        {
                            retorno = new retornoGeral();
                            retorno.P0 = "sem_avisos";
                            retorno.P1 = "./videos/" + elemento.nome_arquivo; //ConfigurationManager.ConnectionStrings["qCaminhoVideo"].ConnectionString
                            retorno.P2 = String.Format("{0:dd/MM/yyyy}", elemento.data_alteracao);
                            retorno.P7 = sAux_letreiro; //'"A T E N Ç Ã O - Dia 26/07/2019 (sexta-feira) não haverá expediente na secretaria devido ao feriado prolongado."
                            listaRetorno.Add(retorno);
                        }
                    }
                    else
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = "sem_avisos";
                        retorno.P1 = "./videos/" + "video_default.mp4?data=" + DateTime.Now.ToString("dd/MM/yyyy HH");
                        retorno.P2 = "1";
                        retorno.P7 = sAux_letreiro; //'"A T E N Ç Ã O - Dia 26/07/2019 (sexta-feira) não haverá expediente na secretaria devido ao feriado prolongado."
                        listaRetorno.Add(retorno);
                    }

                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //Monitor
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheDetalheHoraAula()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 48))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Você não tem permissão para executar essa tarefa.\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                //int qIdPeriodo = Convert.ToInt32(HttpContext.Current.Request.Form["qIdInscricao"]);

                FinanceiroAplicacao aplicacaoFinanceiro = new FinanceiroAplicacao();

                List<geral_detalhe_hora_aula> lista_detalhe_hora_aula = new List<geral_detalhe_hora_aula>();

                int qIdCurso = Convert.ToInt32(HttpContext.Current.Request["qIdCurso"]);
                int qIdProfessor = Convert.ToInt32(HttpContext.Current.Request["qIdProfessor"]);
                //int qIdOferecimento = Convert.ToInt32(HttpContext.Current.Request.Form["qIdOferecimento"]);
                DateTime qData = Convert.ToDateTime(HttpContext.Current.Request["qData"]);

                lista_detalhe_hora_aula = aplicacaoFinanceiro.ListaDetalheHoraAula(qIdCurso,qIdProfessor,qData.Year.ToString(), qData.Month.ToString());

                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                retornoGeral retorno;
                string sAux = "";
                //string sMinutos ="";

                retorno = new retornoGeral();

                if (lista_detalhe_hora_aula.Count > 0)
                {
                    foreach (var elemento in lista_detalhe_hora_aula)
                    {
                        if (sAux != elemento.codigo_disciplina && sAux != "")
                        {
                            listaRetorno.Add(retorno);
                            retorno = new retornoGeral();
                        }

                        retorno.P0 = elemento.professor;
                        retorno.P1 = elemento.curso;
                        retorno.P2 = elemento.codigo_disciplina;
                        retorno.P3 = elemento.periodo;

                        var timeSpan = TimeSpan.FromHours(Convert.ToDouble(elemento.hora_aula));
                        int hh = timeSpan.Hours;
                        int mm = timeSpan.Minutes;
                        int ss = timeSpan.Seconds;

                        var hours = Math.Floor(elemento.hora_aula);
                        //var mins = 60 * (elemento.col_TotalHoras - hours);

                        if (hh == 0 && hours != 0)
                        {
                            hh = Convert.ToInt32(hours);
                        }
                        //switch ((elemento.hora_aula - Math.Truncate(elemento.hora_aula)).ToString())
                        //{
                        //    case "0,08":
                        //        sMinutos = "05";
                        //        break;
                        //    case "0,17":
                        //        sMinutos = "10";
                        //        break;
                        //    case "0,25":
                        //        sMinutos = "15";
                        //        break;
                        //    case "0,33":
                        //        sMinutos = "20";
                        //        break;
                        //    case "0,42":
                        //        sMinutos = "25";
                        //        break;
                        //    case "0,50":
                        //        sMinutos = "30";
                        //        break;
                        //    case "0,58":
                        //        sMinutos = "35";
                        //        break;
                        //    case "0,67":
                        //        sMinutos = "40";
                        //        break;
                        //    case "0,75":
                        //        sMinutos = "45";
                        //        break;
                        //    case "0,83":
                        //        sMinutos = "50";
                        //        break;
                        //    case "0,92":
                        //        sMinutos = "55";
                        //        break;
                        //    default:
                        //        sMinutos = "00";
                        //        break;
                        //}


                        if (retorno.P4 == null)
                        {
                            retorno.P4 = elemento.data_aula;
                            //retorno.P5 = Math.Truncate(elemento.hora_aula).ToString("00") + ":" + sMinutos;
                            retorno.P5 = hh.ToString("00") + ":" + mm.ToString("00");
                        }
                        else
                        {
                            retorno.P4 = retorno.P4 + "<hr>" + elemento.data_aula;
                            //retorno.P5 = retorno.P5 + "<hr>" + Math.Truncate(elemento.hora_aula).ToString("00") + ":" + sMinutos;
                            retorno.P5 = retorno.P5 + "<hr>" + hh.ToString("00") + ":" + mm.ToString("00");
                        }

                        sAux = elemento.codigo_disciplina;
                    }

                }

                listaRetorno.Add(retorno);

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //=== Monitor - Fim =================================================================

        //=== Sininho - Início ==============================================================
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fAprovarHPCurso()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();

            try
            {

                var qIdCurso = HttpContext.Current.Request.Form["qIdCurso"];

                int n;
                var isNumeric = int.TryParse(qIdCurso, out n);

                if (!isNumeric)
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"O argumento enviado não é válido<br><br>\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                cursos item = new cursos();
                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                item.id_curso = n;
                item = aplicacaoCurso.BuscaItem(item);
                Session["cursos"] = item;
                Session["sNewCurso"] = false;
                json = "[{\"P0\":\"ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
                return;

            }
            catch (Exception ex)
            {
                json = "[{\"P0\":\"Erro\",\"P1\":\"Falha ao estabelecer a conexão. <br><br> Erro: " + ex.Message + "<br><br>Por favor, tente novamente mais tarde.\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
                return;
            }
        }

        //=============================================================================

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fAprovarHPTipoCurso()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();

            try
            {

                var qIdTipoCurso = HttpContext.Current.Request.Form["qIdCurso"];

                int n;
                var isNumeric = int.TryParse(qIdTipoCurso, out n);

                if (!isNumeric)
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"O argumento enviado não é válido<br><br>\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                tipos_curso item = new tipos_curso();
                TipoCursoAplicacao aplicacaoTipoCurso = new TipoCursoAplicacao();
                item.id_tipo_curso = n;
                item = aplicacaoTipoCurso.BuscaItem(item);
                Session["tipos_curso"] = item;
                Session["sNewtipos_curso"] = false;
                json = "[{\"P0\":\"ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
                return;

            }
            catch (Exception ex)
            {
                json = "[{\"P0\":\"Erro\",\"P1\":\"Falha ao estabelecer a conexão. <br><br> Erro: " + ex.Message + "<br><br>Por favor, tente novamente mais tarde.\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
                return;
            }
        }

        //=============================================================================

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fAprovarHPDocumentoAcademico()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();

            try
            {

                var qIdDocumento = HttpContext.Current.Request.Form["qIdDocumento"];

                int n;
                var isNumeric = int.TryParse(qIdDocumento, out n);

                if (!isNumeric)
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"O argumento enviado não é válido<br><br>\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                documentos_academicos item = new documentos_academicos();
                DocumentosAcademicosAplicacao aplicacaoDocumento = new DocumentosAcademicosAplicacao();
                item.id_documentos_academicos = n;
                item = aplicacaoDocumento.BuscaItem(item);
                Session["documentos_academicos"] = item;
                Session["sNewdocumentos_academicos"] = false;
                json = "[{\"P0\":\"ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
                return;

            }
            catch (Exception ex)
            {
                json = "[{\"P0\":\"Erro\",\"P1\":\"Falha ao estabelecer a conexão. <br><br> Erro: " + ex.Message + "<br><br>Por favor, tente novamente mais tarde.\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
                return;
            }
        }

        //=============================================================================

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fAprovarHPDissertacao()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();

            try
            {

                var qIdAluno = HttpContext.Current.Request.Form["qIdAluno"];
                var qIdTurma = HttpContext.Current.Request.Form["qIdTurma"];
                string qTab = HttpContext.Current.Request.Form["qTab"];

                int n;
                var isNumeric = int.TryParse(qIdAluno, out n);

                if (!isNumeric)
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"O argumento enviado não é válido<br><br>\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                isNumeric = int.TryParse(qIdTurma, out n);

                if (!isNumeric)
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"O argumento enviado não é válido<br><br>\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                alunos item = new alunos();
                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                item.idaluno = Convert.ToInt32(qIdAluno);
                item = aplicacaoAluno.BuscaItem(item);
                Session[qTab + "Aluno"] = item;
                Session[qTab + "sNovoAluno"] = false;
                json = "[{\"P0\":\"ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
                return;

            }
            catch (Exception ex)
            {
                json = "[{\"P0\":\"Erro\",\"P1\":\"Falha ao estabelecer a conexão. <br><br> Erro: " + ex.Message + "<br><br>Por favor, tente novamente mais tarde.\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
                return;
            }
        }

        //=== Sininho - Fim ==============================================================

        //=== cadUsuario - Início ==============================================================
        //cadAreaConcentracaoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public string fAtivarInativarUsuario(string qOperacao)
        {
            Session.Timeout = 60;
            try
            {
                string json = "";
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"Retorno\":\"deslogado\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    return json;
                }

                UsuarioAplicacao aplicacao = new UsuarioAplicacao();
                usuarios item = (usuarios)Session["usuarios"];

                if (qOperacao == "Ativar")
                {
                    item.status = 1;
                }
                else
                {
                    item.status = 0;
                }

                aplicacao.AlterarStatus(item);
                Session["usuarios"] = item;

                json = "[{\"Retorno\":\"ok\",\"Resposta\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                return json;
                
            }
            catch (Exception ex)
            {
                string json = "[{\"Retorno\":\"erro\",\"Resposta\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                return json;
            }

        }
        //=== cadUsuario - Fim ==============================================================

        //=== Solicitação Pagamento Professor - Início ==============================================================

        //finBotaoSolicitacaoPagto
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fSolicitarPagto()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                int qIdProfessor = Convert.ToInt32(HttpContext.Current.Request["qIdProfessor"]);

                FinanceiroAplicacao aplicacao = new FinanceiroAplicacao();
                List<geral_SolicitacaoPagto> listaSolicitacao;
                List<retornoGeral> listaRetorno = new List<retornoGeral>(); ;
                retornoGeral retorno;

                listaSolicitacao = aplicacao.ListaSolicitaoPagto(qIdProfessor);

                foreach (var elemento in listaSolicitacao)
                {
                    retorno = new retornoGeral();
                    retorno.P0 = elemento.mes.ToString();
                    retorno.P1 = elemento.mes_string.ToString();
                    retorno.P2 = elemento.motivo.ToString();

                    if (elemento.valor_pagar > 0)
                    {
                        retorno.P3 = elemento.valor_pagar.ToString("#,###,###,##0.00");
                        retorno.P4 = ("<label class=\"checkbox classCheck\"><input onclick=\"fSomaSolicitacaoPagto(this);\" id = \"chkSomaValor_" + elemento.id_plano.ToString() + "_" + elemento.valor_pagar.ToString("#############0.00") + "\" type=\"checkbox\" name=\"chkSomaValor_" + elemento.id_plano.ToString() + "_" + elemento.valor_pagar.ToString("#############0.00") + "\"><span></span></label>");
                    }
                    else
                    {
                        retorno.P3 = "<div class=\"text-danger\">" + elemento.valor_pagar.ToString("#,###,###,##0.00") + "</div>";
                        retorno.P4 = "";
                    }
                   
                    listaRetorno.Add(retorno);
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //finBotaoMontaEmail
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fMontaEmailSolicitacaoPagamento()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 58))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Você não tem permissão para executar essa tarefa.\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdProfessor = HttpContext.Current.Request["qIdProfessor"];
                
                FinanceiroAplicacao aplicacaoFinanceiro = new FinanceiroAplicacao();
                professor_solicitacao_pagamento item_psp = new professor_solicitacao_pagamento();
                item_psp.id_professorOld = Convert.ToInt32(qIdProfessor);
                item_psp.status = "Solicitado";
                List<geral_solicitado_professor> lista = aplicacaoFinanceiro.ListaSolicitacoesPagto(item_psp);
                string qIdCurso = "";

                if (lista.Count > 0)
                {
                    AreaAplicacao aplicacaoArea = new AreaAplicacao();
                    GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                    Enderecos item_endereco = new Enderecos();
                    item_endereco.id_endereco = 1; //1=FIPT

                    item_endereco = aplicacaoGerais.BuscaEndereco(item_endereco);

                    StreamReader objReader;
                  
                    objReader = new StreamReader(HttpRuntime.AppDomainAppPath + "\\Templates\\emails\\SolicitacaoNFPagamentoProfessor.html");
                    

                    string sCorpo = objReader.ReadToEnd();
                    objReader.Close();

                    sCorpo = sCorpo.Replace("{nome}", lista.ElementAt(0).professor);

                    int qIdSolicitacao = 0;
                    decimal qTotalValorSolicitacao = 0;
                    foreach (var elemento in lista.OrderBy(x=> x.id_solicitacao))
                    {
                        if (qIdSolicitacao == 0 || qIdSolicitacao != elemento.id_solicitacao)
                        {
                            qTotalValorSolicitacao = qTotalValorSolicitacao + elemento.valor_t_SolicitacaoPagto;
                        }
                        qIdSolicitacao = elemento.id_solicitacao;
                    }

                    sCorpo = sCorpo.Replace("{valor_nota}", string.Format("{0:C}", qTotalValorSolicitacao));

                    string qMes = "";
                    string qMesAux = "";
                    string qReferente = "";
                    foreach (var elemento in lista.OrderBy(x=> x.mes_plano))
                    {
                        qMesAux = elemento.mes_plano.Month.ToString("00") + "/" + elemento.mes_plano.Year.ToString();
                        if (qMes.IndexOf(qMesAux) == -1)
                        {
                            if (qMes != "")
                            {
                                qMes = qMes + ", ";
                            }
                            qMes = qMes + qMesAux;
                        }
                    }

                    if (lista.Any(x => x.motivo == "Horas Aula") && lista.Any(x => x.motivo == "Orientação") && lista.Any(x => x.motivo == "Coordenação"))
                    {
                        qReferente = "aulas ministradas, orientação, coordenação";
                    }
                    else if (lista.Any(x => x.motivo == "Horas Aula") && lista.Any(x => x.motivo == "Orientação"))
                    {
                        qReferente = "aulas ministradas, orientação";
                    }
                    else if (lista.Any(x => x.motivo == "Horas Aula") && lista.Any(x => x.motivo == "Coordenação"))
                    {
                        qReferente = "aulas ministradas, coordenação";
                    }
                    else if (lista.Any(x => x.motivo == "orientação") && lista.Any(x => x.motivo == "Coordenação"))
                    {
                        qReferente = "orientação, coordenação";
                    }
                    else if (lista.Any(x => x.motivo == "Horas Aula"))
                    {
                        qReferente = "aulas ministradas";
                    }
                    else if (lista.Any(x => x.motivo == "orientação"))
                    {
                        qReferente = "orientação";
                    }
                    else
                    {
                        qReferente = "coordenação";
                    }
                    sCorpo = sCorpo.Replace("{mes}", qMes);
                    sCorpo = sCorpo.Replace("{referente_a}", qReferente);

                    sCorpo = sCorpo.Replace("{Endereço}", item_endereco.endereco);
                    sCorpo = sCorpo.Replace("{Numero}", item_endereco.numero);
                    sCorpo = sCorpo.Replace("{Complemento}", item_endereco.complemento);
                    sCorpo = sCorpo.Replace("{Bairro}", item_endereco.bairro);
                    sCorpo = sCorpo.Replace("{Cidade}", item_endereco.cidade);
                    sCorpo = sCorpo.Replace("{Estado}", item_endereco.estado);
                    sCorpo = sCorpo.Replace("{Cep}", item_endereco.cep);
                    sCorpo = sCorpo.Replace("{Cnpj}", item_endereco.cnpj);
                    sCorpo = sCorpo.Replace("{Ie}", item_endereco.ie);

                    string sAux = "";
                    string sAux_orientacao = "";
                    foreach (var elemento in lista.Where(x => x.motivo == "Horas Aula").ToList())
                    {
                        item_psp.data_solicitacao = elemento.mes_plano;
                       
                        List<geral_horas_aulas_dadas> lista_horas = new List<geral_horas_aulas_dadas>();
                        lista_horas = aplicacaoFinanceiro.ListaHorasAulasDadas(item_psp);

                        string qIdCurso_Horas = "";
                        TimeSpan qTtimespan = TimeSpan.FromHours(0);
                        int dd;
                        int hh;
                        int mm;
                        int ss;
                        
                        //Horas Aula
                        if (lista_horas.Count > 0)
                        {
                            foreach (var elemento2 in lista_horas)
                            {
                                if (qIdCurso_Horas == "")
                                {
                                    if (elemento2.id_tipo_curso == 1)
                                    {
                                        sAux = sAux + "<b>CURSO:</b> &nbsp; Mestrado Profissional em " + elemento2.nome_curso;
                                    }
                                    else
                                    {
                                        sAux = sAux + "<b>CURSO:</b> &nbsp; " + elemento2.nome_curso;
                                    }
                                    sAux = sAux + "<table style = \"width: 100%\"> <tbody> <tr> <td style = \"width: 250px\" ><b> Oferecimento </b></td>";
                                    sAux = sAux + "<td><b> Datas de Aula</b ></td> <td><b> Total Hrs.</b></td><td><b> Valor Hr.</b ></td ><td style = \"width: 80px; text-align: right\" ><b> sub-total </b ></td ></tr >";
                                }
                                else if (qIdCurso_Horas != elemento2.id_curso.ToString())
                                {
                                    sAux = sAux + "</tbody ></table ><br><br><br>";
                                    if (elemento2.id_tipo_curso == 1)
                                    {
                                        sAux = sAux + "<b>CURSO:</b> &nbsp; Mestrado Profissional em " + elemento2.nome_curso;
                                    }
                                    else
                                    {
                                        sAux = sAux + "<b>CURSO:</b> &nbsp; " + elemento2.nome_curso;
                                    }
                                    sAux = sAux + "<table style = \"width: 100%\"> <tbody> <tr> <td style = \"width: 250px\" ><b> Oferecimento </b></td>";
                                    sAux = sAux + "<td><b> Datas de Aula</b ></td> <td><b> Total Hrs.</b></td><td><b> Valor Hr.</b ></td ><td style = \"width: 80px; text-align: right\" ><b> sub-total </b ></td ></tr >";
                                }

                                sAux = sAux + "<tr style = \"vertical-align: top\">";
                                sAux = sAux + "<td> " + elemento2.nome_disciplina + "</td>";
                                sAux = sAux + "<td>" + elemento2.datas_aula + "</td>";

                                qTtimespan = TimeSpan.FromHours(Convert.ToDouble(elemento2.total_hora_aula));
                                dd = qTtimespan.Days;
                                hh = qTtimespan.Hours;
                                mm = qTtimespan.Minutes;
                                ss = qTtimespan.Seconds;
                                if (dd > 0)
                                {
                                    hh = hh + (dd * 24);
                                }

                                sAux = sAux + "<td style = \"text-align:center\">" + hh.ToString("00") + ":" + mm.ToString("00") + "</td >";
                                sAux = sAux + "<td style = \"text-align:center\">" + String.Format("{0:N}", elemento2.valor_hora) + "</td >";
                                sAux = sAux + "<td style = \"text-align:right\">" + String.Format("{0:N}", elemento2.sub_total) + "</td ></tr> ";

                                qIdCurso_Horas = elemento2.id_curso.ToString();
                            }
                        }
                        sAux = sAux + "</tbody></table><br><br><br>";

                    }

                    //Troquei motivo de (x.motivo != "Orientação") para (x.motivo == "Orientação")
                    foreach (var elemento in lista.Where(x=> x.motivo == "Orientação").ToList())
                    {
                        item_psp.data_solicitacao = elemento.mes_plano;
                        List<geral_orientacoes_dadas> lista_orientacoes = new List<geral_orientacoes_dadas>();
                        lista_orientacoes = aplicacaoFinanceiro.ListaOrientacoesDadas(item_psp);

                        //string qIdCurso_Horas = "";

                        //Horas Aula
                        if (lista_orientacoes.Count > 0)
                        {
                            foreach (var elemento2 in lista_orientacoes)
                            {
                                if (qIdCurso == "")
                                {
                                    if (elemento2.id_tipo_curso == 1)
                                    {
                                        sAux_orientacao = sAux_orientacao + "<b>CURSO:</b> &nbsp; Mestrado Profissional em " + elemento2.nome_curso;
                                    }
                                    else
                                    {
                                        sAux_orientacao = sAux_orientacao + "<b>CURSO:</b> &nbsp; " + elemento2.nome_curso;
                                    }
                                    sAux_orientacao = sAux_orientacao + "<table style = \"width: 100%\"> <tbody> <tr> <td><b> Orientação </b></td>";
                                    sAux_orientacao = sAux_orientacao + "<td style = \"width: 80px; text-align: right\" ><b> sub-total </b ></td ></tr >";
                                }
                                else if (qIdCurso != elemento2.id_curso.ToString())
                                {
                                    sAux_orientacao = sAux_orientacao + "</tbody ></table ><br><br><br>";
                                    if (elemento2.id_tipo_curso == 1)
                                    {
                                        sAux_orientacao = sAux_orientacao + "<b>CURSO:</b> &nbsp; Mestrado Profissional em " + elemento2.nome_curso;
                                    }
                                    else
                                    {
                                        sAux_orientacao = sAux_orientacao + "<b>CURSO:</b> &nbsp; " + elemento2.nome_curso;
                                    }
                                    sAux_orientacao = sAux_orientacao + "<table style = \"width: 100%\"> <tbody> <tr> <td><b> Orientação </b></td>";
                                    sAux_orientacao = sAux_orientacao + "<td style = \"width: 80px; text-align: right\" ><b> sub-total </b ></td ></tr >";
                                }

                                sAux_orientacao = sAux_orientacao + "<tr style = \"vertical-align:top\">";
                                sAux_orientacao = sAux_orientacao + "<td> " + elemento2.orientacao + "</td>";
                                sAux_orientacao = sAux_orientacao + "<td style = \"text-align:right\">" + String.Format("{0:N}", elemento2.sub_total) + "</td ></tr>";

                                qIdCurso = elemento2.id_curso.ToString();
                            }
                        }

                    }

                    foreach (var elemento in lista.Where(x => x.motivo == "Coordenação").ToList().OrderBy(x => x.mes_plano))
                    {
                        item_psp.data_solicitacao = elemento.mes_plano;
                        List<geral_custo_coordenacao> lista_coordenacao = new List<geral_custo_coordenacao>();
                        professor_solicitacao_pagamento item_psp_cover = new professor_solicitacao_pagamento();
                        item_psp_cover.data_alteracao = item_psp.data_alteracao;
                        elemento.id_professor = Convert.ToInt32(item_psp.id_professorOld.Value);
                        lista_coordenacao = aplicacaoFinanceiro.ListaCoordenacaoDadas(elemento);

                        //string qIdCurso_Horas = "";

                        //Horas Aula
                        if (lista_coordenacao.Count > 0)
                        {
                            foreach (var elemento2 in lista_coordenacao)
                            {
                                if (qIdCurso == "")
                                {
                                    if (elemento2.col_Id_TipoCurso == 1)
                                    {
                                        sAux_orientacao = sAux_orientacao + "<b>CURSO:</b> &nbsp; Mestrado Profissional em " + elemento2.col_Curso;
                                    }
                                    else
                                    {
                                        sAux_orientacao = sAux_orientacao + "<b>CURSO:</b> &nbsp; " + elemento2.col_Curso;
                                    }
                                    sAux_orientacao = sAux_orientacao + "<table style = \"width: 100%\"> <tbody> <tr> <td><b> Tipo Coordenação </b></td> <td><b> Mês Ref. </b></td>";
                                    sAux_orientacao = sAux_orientacao + "<td style = \"width: 80px; text-align: right\" ><b> sub-total </b ></td ></tr >";
                                }
                                else if (qIdCurso != elemento2.col_Id_Curso.ToString())
                                {
                                    sAux_orientacao = sAux_orientacao + "</tbody ></table ><br><br><br>";
                                    if (elemento2.col_Id_TipoCurso == 1)
                                    {
                                        sAux_orientacao = sAux_orientacao + "<b>CURSO:</b> &nbsp; Mestrado Profissional em " + elemento2.col_Curso;
                                    }
                                    else
                                    {
                                        sAux_orientacao = sAux_orientacao + "<b>CURSO:</b> &nbsp; " + elemento2.col_Curso;
                                    }
                                    sAux_orientacao = sAux_orientacao + "<table style = \"width: 100%\"> <tbody> <tr> <td><b> Tipo Coordenação </b></td> <td><b> Mês Ref. </b></td>";
                                    sAux_orientacao = sAux_orientacao + "<td style = \"width: 80px; text-align: right\" ><b> sub-total </b ></td ></tr >";
                                }

                                sAux_orientacao = sAux_orientacao + "<tr style = \"vertical-align:top\">";
                                sAux_orientacao = sAux_orientacao + "<td> " + elemento2.col_TipoCoordenacao + "</td>";
                                sAux_orientacao = sAux_orientacao + "<td> " + elemento2.col_MesReferencia + "</td>";
                                sAux_orientacao = sAux_orientacao + "<td style = \"text-align:right\">" + String.Format("{0:N}", elemento2.col_Total) + "</td ></tr>";

                                qIdCurso = elemento2.col_Id_Curso.ToString();
                            }
                        }

                    }

                    if (sAux_orientacao != "")
                    {
                        sAux_orientacao = sAux_orientacao + "</tbody></table><br><br><br>";
                        sAux = sAux + sAux_orientacao;
                    }

                    sCorpo = sCorpo.Replace("{tabela_aulas_dissertacao}", sAux);

                    sCorpo = sCorpo.Replace("\"", "'");

                    retornoGeral retorno = new retornoGeral();
                    List<retornoGeral> listaRetorno = new List<retornoGeral>();

                    retorno.P0 = "ok";
                    retorno.P1 = sCorpo;
                    retorno.P2 = "suporte@ipt.br";
                    retorno.P3 = lista.ElementAt(0).email + ";" + lista.ElementAt(0).email2;
                    retorno.P4 = "mareol@ipt.br";
                    retorno.P5 = "Prof. " + lista.ElementAt(0).professor + " – PAGAMENTO DE SERVIÇOS EDUCACIONAIS – IPT/FIPT";
                    retorno.P6 = String.Format("{0:yyyy-MM-dd}", DateTime.Today);

                    listaRetorno.Add(retorno);

                    json = jsSS.Serialize(listaRetorno);
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);


                    //json = "[{\"P0\":\"ok\",\"P1\":\"" + sCorpo + "\",\"P2\":\"\",\"P3\":\"\"}]";
                    //this.Context.Response.ContentType = "application/json; charset=utf-8";
                    //this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //finBotaoRecebimentoNF
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fRecebimentoNF()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                int qIdProfessor = Convert.ToInt32(HttpContext.Current.Request["qIdProfessor"]);
                bool qSolicitado = Convert.ToBoolean(HttpContext.Current.Request["qSolicitado"]);

                FinanceiroAplicacao aplicacao = new FinanceiroAplicacao();
                List<geral_Solicitacao> listaSolicitacao;
                List<retornoGeral> listaRetorno = new List<retornoGeral>(); ;
                retornoGeral retorno = new retornoGeral();

                listaSolicitacao = aplicacao.ListaSolicitaoEfetuada(qIdProfessor, qSolicitado);
                int qIdSolicitacao = 0;
                decimal qTotalSolicitacao = listaSolicitacao.Sum(x => x.valor_solicitado);
                string arrayIdsolicitacao ="";

                foreach (var elemento in listaSolicitacao)
                {
                    if (qIdSolicitacao != elemento.id_solicitacao && qIdSolicitacao != 0)
                    {
                        retorno.P5 = qTotalSolicitacao.ToString("#,###,###,##0.00");
                        listaRetorno.Add(retorno);
                        retorno = new retornoGeral();
                        
                        retorno.P0 = String.Format("{0:dd/MM/yyyy}", elemento.data_solicitacao);
                        retorno.P1 = elemento.mes_string.ToString();
                        retorno.P2 = elemento.motivo.ToString();
                        retorno.P3 = elemento.valor_solicitado.ToString("#,###,###,##0.00");
                        retorno.P4 = elemento.valor_total_solicitado.ToString("#,###,###,##0.00");

                        if (elemento.status == "Solicitado")
                        {
                            retorno.P7 = "";
                            retorno.P8 = "<div title=\"Excluir Solicitação\"> <a class=\"btn btn-danger btn-circle fa fa-eraser\" href=\'javascript:fExcluirSolicitacao(\""
        + elemento.id_solicitacao + "\",\"" + retorno.P0 + "\",\"" + retorno.P4 + "\")\'; ></a></div>";
                        }
                        else
                        {
                            retorno.P7 = "<div title=\"Mais informações\"> <a class=\"btn btn-warning btn-circle fa fa-search\" href=\'javascript:fMaisInformacoes(\""
        + elemento.nota_fiscal + "\",\"" + String.Format("{0:dd/MM/yyyy}", elemento.data_recebimento) + "\",\"" + String.Format("{0:dd/MM/yyyy}", elemento.data_pagamento) + "\")\'; ></a></div>";
                            retorno.P8 = "";
                        }

                        arrayIdsolicitacao = arrayIdsolicitacao + ";" + elemento.id_solicitacao.ToString();
                    }
                    else
                    {
                        if (retorno.P0 == null)
                        {
                            arrayIdsolicitacao = elemento.id_solicitacao.ToString();
                            retorno.P0 = String.Format("{0:dd/MM/yyyy}", elemento.data_solicitacao);
                            retorno.P4 = elemento.valor_total_solicitado.ToString("#,###,###,##0.00");

                            if (elemento.status == "Solicitado")
                            {
                                retorno.P7 = "";
                                retorno.P8 = "<div title=\"Excluir Solicitação\"> <a class=\"btn btn-danger btn-circle fa fa-eraser\" href=\'javascript:fExcluirSolicitacao(\""
        + elemento.id_solicitacao + "\",\"" + retorno.P0 + "\",\"" + retorno.P4 + "\")\'; ></a></div>";
                            }
                            else
                            {
                                retorno.P7 = "<div title=\"Mais informações\"> <a class=\"btn btn-warning btn-circle fa fa-search\" href=\'javascript:fMaisInformacoes(\""
            + elemento.nota_fiscal + "\",\"" + String.Format("{0:dd/MM/yyyy}", elemento.data_recebimento) + "\",\"" + String.Format("{0:dd/MM/yyyy}", elemento.data_pagamento) + "\")\'; ></a></div>";
                                retorno.P8 = "";
                            }
                        }
                        
                        if (retorno.P1 != null)
                        {
                            retorno.P1 = retorno.P1 + "<hr>";
                            retorno.P2 = retorno.P2 + "<hr>";
                            retorno.P3 = retorno.P3 + "<hr>";
                        }
                        retorno.P1 = retorno.P1 + elemento.mes_string.ToString();
                        retorno.P2 = retorno.P2 + elemento.motivo.ToString();
                        retorno.P3 = retorno.P3 + elemento.valor_solicitado.ToString("#,###,###,##0.00");
                        
                    }

                    qIdSolicitacao = elemento.id_solicitacao;
                }

                retorno.P5 = qTotalSolicitacao.ToString("#,###,###,##0.00");
                retorno.P6 = arrayIdsolicitacao;
                listaRetorno.Add(retorno);

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //=== Solicitação Pagamento Professor - Fim ==============================================================

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fBoletoFipt()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();

            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 43) && !usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 47)) // 43 - Portal do Aluno e 47 - Dados Cadastrais - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Você não tem permissão para executar essa tarefa.\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }



                alunos item_aluno = new alunos();
                item_aluno = (alunos)Session["AlunoLogado"];

                string URI = "http://200.18.106.49:8080/login/aluno/";
                var reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("login", item_aluno.cpf);//"112.742.388-62"
                reqparm.Add("senha", null);
                reqparm.Add("tipo_user", "aluno");
                reqparm.Add("dtnasc", null);

                string responsebody;

                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    var HtmlResult = wc.UploadValues(URI, reqparm);

                    responsebody = Encoding.UTF8.GetString(HtmlResult);

                    //string myParameters = "login=112.742.388-62&senha=&tipo_user=alunovalue3&dtnasc=";

                    //var HtmlResult2 = wc.UploadString(URI, myParameters);

                }

                json = "[{\"P0\":\"ok\",\"P1\":\"" + responsebody + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
                return;

            }
            catch (Exception ex)
            {
                json = "[{\"P0\":\"Erro\",\"P1\":\"Falha ao estabelecer a conexão. <br><br> Erro: " + ex.Message + "<br><br>Por favor, tente novamente mais tarde.\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
                return;
            }
        }


        //==========================================================
        //Tela_Sistema - Início
        //==========================================================

        //admTelaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheGrupoTela()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 3)) // 3. Cadastro Tela - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                TelaAplicacao aplicacaoTela = new TelaAplicacao();
                telas_sistema item = (telas_sistema)Session["telas_sistema"];

                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (item.grupos_acesso_telas_sistema.Count > 0)
                {
                    retornoGeral retorno;

                    foreach (var elemento in item.grupos_acesso_telas_sistema.OrderByDescending(x => x.grupos_acesso.grupo))
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.grupos_acesso.grupo + "<br>" + elemento.grupos_acesso.descricao;
                        if (elemento.escrita==true)
                        {
                            retorno.P1 = "<span class=\"text-info\"><strong>Escrita</strong></span>";
                            retorno.P2 = "<div title=\"Editar\"> <a class=\"btn btn-primary btn-circle fa fa-edit\" href=\'javascript:fModalEditarGrupo(\""
                                + elemento.grupos_acesso.grupo + "\",\"" + elemento.id_grupo + "\",\"Escrita\")\'; ></a></div>";
                        }
                        else
                        {
                            retorno.P1 = "<span><strong>Leitura</strong></span>";
                            retorno.P2 = "<div title=\"Editar\"> <a class=\"btn btn-primary btn-circle fa fa-edit\" href=\'javascript:fModalEditarGrupo(\""
                                + elemento.grupos_acesso.grupo + "\",\"" + elemento.id_grupo + "\",\"Leitura\")\'; ></a></div>";
                        }

                        retorno.P3 = "<div title=\"Excluir Associação\"> <a class=\"btn btn-danger btn-circle fa fa-eraser\" href=\'javascript:fModalExcluirGrupo(\""
                                + elemento.grupos_acesso.grupo + "\",\"" + elemento.id_grupo + "\")\'; ></a></div>";

                        listaRetorno.Add(retorno);
                    }
                }
                
                json = jsSS.Serialize(listaRetorno);

                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //admTelaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fEditarGrupo()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 3)) // 3. Cadastro Tela - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                }

                string qIdGrupo = HttpContext.Current.Request["qIdGrupo"];
                string qPermissao = HttpContext.Current.Request["qPermissao"];


                GrupoAplicacao aplicacaoGrupo = new GrupoAplicacao();
                TelaAplicacao aplicacaoTela = new TelaAplicacao();
                grupos_acesso_telas_sistema item = new grupos_acesso_telas_sistema();
                telas_sistema pItem_Tela = new telas_sistema();

                pItem_Tela = (telas_sistema)Session["telas_sistema"];

                item.id_grupo  = Convert.ToInt32(qIdGrupo);
                item.id_tela = pItem_Tela.id_tela;
                if (qPermissao == "escrita")
                {
                    item.escrita = true;
                    item.leitura = false;
                }
                else
                {
                    item.escrita = false;
                    item.leitura = true;
                }

                aplicacaoGrupo.AlterarItem_grupo_tela_sistema(item);
                Session["telas_sistema"] = aplicacaoTela.BuscaItem(pItem_Tela);

                json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
                return;
            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }
        }

        //admTelaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPerquisaGrupoDisponivel()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 3)) // 3. Cadastro Tela - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
                {
                    json = "[{\"P0\":\"Você não tem acesso a esse módulo.\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdGrupo = HttpContext.Current.Request["qIdGrupo"];
                string qNome = HttpContext.Current.Request["qNome"];

                TelaAplicacao aplicacaoTela = new TelaAplicacao();
                grupos_acesso_telas_sistema item = new grupos_acesso_telas_sistema();
                item.grupos_acesso = new grupos_acesso();
                List<grupos_acesso> lista = new List<grupos_acesso>();
                telas_sistema pItem_Tela = new telas_sistema();
                pItem_Tela = (telas_sistema)Session["telas_sistema"];

                item.id_tela = pItem_Tela.id_tela;
                if (qIdGrupo != "")
                {
                    item.id_grupo = Convert.ToInt32(qIdGrupo);
                }
                if (qNome != "")
                {
                    item.grupos_acesso.grupo = qNome;
                }
                
                lista = aplicacaoTela.ListaItemDisponiveis(item);
                
                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_grupo.ToString();
                        retorno.P1 = elemento.grupo + " (" + elemento.descricao + ")";
                        retorno.P2 = ("<div title=\"Incluir Grupo\"> <a class=\"btn btn-success  btn-circle  fa fa-plus\" href=\'javascript:fAbreModalIncluiGrupo(\""
                        + (elemento.id_grupo.ToString() + ("\",\""
                        + (elemento.grupo + "\")\'; ></a></div>"))));

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //admTelaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fAssociarGrupo()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 3)) // 3. Cadastro Tela - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
                {
                    json = "[{\"P0\":\"Você não tem acesso à esse módulo.\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdGrupo = HttpContext.Current.Request["qIdGrupo"];
                string qPermissao = HttpContext.Current.Request["qPermissao"];

                TelaAplicacao aplicacaoTela = new TelaAplicacao();
                grupos_acesso_telas_sistema item = new grupos_acesso_telas_sistema();
                telas_sistema pItem_Tela = new telas_sistema();
                pItem_Tela = (telas_sistema)Session["telas_sistema"];

                item.id_tela = pItem_Tela.id_tela;
                item.id_grupo = Convert.ToInt32(qIdGrupo);
                if (qPermissao == "escrita")
                {
                    item.escrita = true;
                    item.leitura = false;
                }
                else
                {
                    item.escrita = false;
                    item.leitura = true;
                }

                item.modificacao = false;
                item.exclusao = false;

                if (aplicacaoTela.CriarAssociacao(item))
                {
                    pItem_Tela = aplicacaoTela.BuscaItem(pItem_Tela);
                    GrupoAplicacao aplicacaoGrupo = new GrupoAplicacao();
                    grupos_acesso item_grupo = new grupos_acesso();
                    item_grupo.id_grupo = item.id_grupo;
                    item_grupo = aplicacaoGrupo.BuscaItem(item_grupo);
                    item.grupos_acesso = item_grupo;
                    pItem_Tela.grupos_acesso_telas_sistema.Add(item);

                    Session["telas_sistema"] = pItem_Tela;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na Associação do Grupo. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //admTelaGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fApagarGrupo()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 3)) // 3. Cadastro Tela - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
                {
                    json = "[{\"P0\":\"Você não acesso à esse módulo.\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdGrupo = HttpContext.Current.Request["qIdGrupo"];

                TelaAplicacao aplicacaoTela = new TelaAplicacao();
                grupos_acesso_telas_sistema item = new grupos_acesso_telas_sistema();
                telas_sistema pItem_Tela = new telas_sistema();
                pItem_Tela = (telas_sistema)Session["telas_sistema"];

                item.id_tela = pItem_Tela.id_tela;
                item.id_grupo = Convert.ToInt32(qIdGrupo);

                if (aplicacaoTela.ApagarAssociacao(item))
                {
                    pItem_Tela = aplicacaoTela.BuscaItem(pItem_Tela);

                    Session["telas_sistema"] = pItem_Tela;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na desassociação do Grupo. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //==========================================================
        //Tela_Sistema - Fim
        //==========================================================

        //==========================================================
        //Grupo_Usuario - Início
        //==========================================================

        //admGrupoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheTelaGrupo()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 2)) // 2. Cadastro Grupo - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                GrupoAplicacao aplicacaoGrupo = new GrupoAplicacao();
                grupos_acesso item = (grupos_acesso)Session["grupos_acesso"];

                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (item.grupos_acesso_telas_sistema.Count > 0)
                {
                    retornoGeral retorno;

                    foreach (var elemento in item.grupos_acesso_telas_sistema.OrderByDescending(x => x.telas_sistema.modulo_sapiens).OrderByDescending(x => x.telas_sistema.descricao_sapiens))
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.telas_sistema.modulo_sapiens;
                        retorno.P1 = elemento.telas_sistema.descricao_sapiens;
                        if (elemento.escrita == true)
                        {
                            retorno.P2 = "<span class=\"text-info\"><strong>Escrita</strong></span>";
                            retorno.P3 = "<div title=\"Editar Tipo Acesso\"> <a class=\"btn btn-primary btn-circle fa fa-edit\" href=\'javascript:fModalEditarTipoAcesso(\""
                                + elemento.telas_sistema.descricao_sapiens + "\",\"" + elemento.id_tela + "\",\"Escrita\")\'; ></a></div>";
                        }
                        else
                        {
                            retorno.P2 = "<span><strong>Leitura</strong></span>";
                            retorno.P3 = "<div title=\"Editar Tipo Acesso\"> <a class=\"btn btn-primary btn-circle fa fa-edit\" href=\'javascript:fModalEditarTipoAcesso(\""
                                + elemento.telas_sistema.descricao_sapiens + "\",\"" + elemento.id_tela + "\",\"Leitura\")\'; ></a></div>";
                        }

                        retorno.P4 = "<div title=\"Excluir Associação\"> <a class=\"btn btn-danger btn-circle fa fa-eraser\" href=\'javascript:fModalExcluirTela(\""
                                + elemento.telas_sistema.descricao_sapiens + "\",\"" + elemento.id_tela + "\")\'; ></a></div>";

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);

                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //admGrupoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fEditarTela()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 2)) // 2. Cadastro Tela - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                }

                string qIdTela = HttpContext.Current.Request["qIdTela"];
                string qPermissao = HttpContext.Current.Request["qPermissao"];


                GrupoAplicacao aplicacaoGrupo = new GrupoAplicacao();
                TelaAplicacao aplicacaoTela = new TelaAplicacao();
                grupos_acesso_telas_sistema item = new grupos_acesso_telas_sistema();
                grupos_acesso pItem_Grupo = new grupos_acesso();

                pItem_Grupo = (grupos_acesso)Session["grupos_acesso"];

                item.id_tela = Convert.ToInt32(qIdTela);
                item.id_grupo = pItem_Grupo.id_grupo;
                if (qPermissao == "escrita")
                {
                    item.escrita = true;
                    item.leitura = false;
                }
                else
                {
                    item.escrita = false;
                    item.leitura = true;
                }

                aplicacaoGrupo.AlterarItem_grupo_tela_sistema(item);
                Session["grupos_acesso"] = aplicacaoGrupo.BuscaItem(pItem_Grupo);

                json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
                return;
            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }
        }

        //admGrupoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPerquisaTelaDisponivel()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 2)) // 2. Cadastro Tela - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
                {
                    json = "[{\"P0\":\"Você não tem acesso à esse módulo.\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qIdTela = HttpContext.Current.Request["qIdTela"];
                string qModulo = HttpContext.Current.Request["qModulo"];
                string qNomeTela = HttpContext.Current.Request["qNomeTela"];

                GrupoAplicacao aplicacaoGrupo = new GrupoAplicacao();
                grupos_acesso_telas_sistema item = new grupos_acesso_telas_sistema();
                item.telas_sistema = new telas_sistema();
                List<telas_sistema> lista = new List<telas_sistema>();
                grupos_acesso pItem_Grupo = new grupos_acesso();
                pItem_Grupo = (grupos_acesso)Session["grupos_acesso"];

                item.id_grupo = pItem_Grupo.id_grupo;
                if (qIdTela != "")
                {
                    item.id_tela = Convert.ToInt32(qIdTela);
                }

                item.telas_sistema.modulo_sapiens = qModulo;
                item.telas_sistema.descricao_sapiens = qNomeTela;

                lista = aplicacaoGrupo.ListaItemDisponiveis(item);

                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (lista.Count > 0)
                {
                    retornoGeral retorno;
                    foreach (var elemento in lista)
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.id_tela.ToString();
                        retorno.P1 = "(" + elemento.modulo_sapiens + ") " + elemento.descricao_sapiens;
                        retorno.P2 = ("<div title=\"Incluir Tela\"> <a class=\"btn btn-success  btn-circle  fa fa-plus\" href=\'javascript:fAbreModalIncluiTela(\""
                        + (elemento.id_tela.ToString() + ("\",\""
                        + (elemento.descricao_sapiens + "\")\'; ></a></div>"))));

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //admGrupoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fAssociarTela()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 2)) // 2. Cadastro Tela - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                }

                string qIdTela = HttpContext.Current.Request["qIdTela"];
                string qPermissao = HttpContext.Current.Request["qPermissao"];

                GrupoAplicacao aplicacaoGrupo = new GrupoAplicacao();
                grupos_acesso_telas_sistema item = new grupos_acesso_telas_sistema();
                grupos_acesso pItem_Grupo = new grupos_acesso();
                pItem_Grupo = (grupos_acesso)Session["grupos_acesso"];

                item.id_grupo = pItem_Grupo.id_grupo;
                item.id_tela = Convert.ToInt32(qIdTela);
                if (qPermissao == "escrita")
                {
                    item.escrita = true;
                    item.leitura = false;
                }
                else
                {
                    item.escrita = false;
                    item.leitura = true;
                }

                item.modificacao = false;
                item.exclusao = false;

                if (aplicacaoGrupo.CriarAssociacao(item))
                {
                    pItem_Grupo = aplicacaoGrupo.BuscaItem(pItem_Grupo);
                    TelaAplicacao aplicacaoTela = new TelaAplicacao();
                    telas_sistema item_Tela = new telas_sistema();
                    item_Tela.id_tela = item.id_tela;
                    item_Tela = aplicacaoTela.BuscaItem(item_Tela);
                    item.telas_sistema = item_Tela;
                    pItem_Grupo.grupos_acesso_telas_sistema.Add(item);

                    Session["grupos_acesso"] = pItem_Grupo;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na Associação do Grupo. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //admGrupoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fApagarTela()
        {
            Session.Timeout = 60;
            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();
            try
            {
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"deslogado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 2)) // 2. Cadastro Tela - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                }

                string qIdTela = HttpContext.Current.Request["qIdTela"];

                GrupoAplicacao aplicacaoGrupo = new GrupoAplicacao();
                grupos_acesso_telas_sistema item = new grupos_acesso_telas_sistema();
                grupos_acesso pItem_Grupo = new grupos_acesso();
                pItem_Grupo = (grupos_acesso)Session["grupos_acesso"];

                item.id_grupo = pItem_Grupo.id_grupo;
                item.id_tela = Convert.ToInt32(qIdTela);

                if (aplicacaoGrupo.ApagarAssociacao(item))
                {
                    pItem_Grupo = aplicacaoGrupo.BuscaItem(pItem_Grupo);

                    Session["grupos_acesso"] = pItem_Grupo;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na desassociação doa Tela. Tente novamente\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

            }
            catch (Exception ex)
            {
                json = ("[{\"P0\":\"Erro\",\"P1\":\""
                        + (ex.Message + "\",\"P2\":\"\",\"P3\":\"\",\"P4\":\"\",\"P5\":\"\",\"P6\":\"\"}]"));
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //==========================================================
        //Grupo_Usuario - Fim
        //==========================================================

        //==========================================================
        //Certificado - Início
        //==========================================================
        //outCertificadoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheTurmaCertificado()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 75)) // 3. Cadastro Tela - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                CertificadoAplicacao aplicacaoTela = new CertificadoAplicacao();
                certificados item = (certificados)Session["certificados"];

                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (item.certificados_participantes.Count > 0)
                {
                    retornoGeral retorno;

                    foreach (var elemento in item.certificados_participantes.OrderBy(x => x.numero_seq))
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.numero_seq.ToString();
                        retorno.P1 = elemento.nome;
                        retorno.P2 = elemento.interno_externo.ToUpper();
                        if (elemento.arquivo_pdf != null)
                        {
                            retorno.P3 = "<div title=\"Download do certificado\"> <a  target=\"_blank\" class=\"btn btn-purple btn-circle fa fa-download\" download href=\'Certificados\\" + item.id_certificado + "\\pdfs\\" + elemento.arquivo_pdf + "\'; ></a></div>";
                        }
                        else
                        {
                            retorno.P3 = "";
                        }

                        retorno.P4 = "<div title=\"Editar\"> <a class=\"btn btn-primary btn-circle fa fa-edit\" href=\'javascript:fModalEditarParticipante(\""
                                + elemento.id_certificado_participante + "\",\"" + elemento.nome + "\",\"" + elemento.interno_externo + "\")\'; ></a></div>";


                        retorno.P5 = "<div title=\"Excluir Associação\"> <a class=\"btn btn-danger btn-circle fa fa-eraser\" href=\'javascript:fModalExcluirParticipante(\""
                                + elemento.id_certificado_participante + "\",\"" + elemento.nome + "\",\"" + elemento.interno_externo + "\")\'; ></a></div>";

                        retorno.P6 = elemento.email;
                        if (elemento.email != "" && retorno.P3 != "")
                        {
                            retorno.P7 = "<div title=\"Enviar e-mail\"> <a class=\"btn btn-info btn-circle fa fa-envelope-o\" href=\'javascript:fAbrirModalEnviarEmailLote(\""
                                + elemento.email + "\")\'; ></a></div>";

                        }

                        if (elemento.data_envio_email != null)
                        {
                            retorno.P8 = elemento.data_envio_email.Value.ToString("dd/MM/yyyy");

                        }


                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);

                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //outCertificadoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fSelecionouExcelCertificado()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();

                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 75))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                HttpPostedFile qArquivo = HttpContext.Current.Request.Files["qArquivo"];
                certificados item = (certificados)Session["certificados"];

                if (qArquivo.ContentLength > 0)
                {
                    string qNomeArquivo = qArquivo.FileName;
                    int qTamanhoArquivo = qArquivo.ContentLength;
                    byte[] arrayByte = new byte[qTamanhoArquivo];

                    qArquivo.InputStream.Read(arrayByte, 0, qTamanhoArquivo);

                    if (!System.IO.Directory.Exists(Server.MapPath("") + "\\Certificados\\"))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("") + "\\Certificados\\");
                    }

                    if (!System.IO.Directory.Exists(Server.MapPath("") + "\\Certificados\\" + item.id_certificado + "\\" ))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("") + "\\Certificados\\" + item.id_certificado + "\\");
                    }

                    if (!System.IO.Directory.Exists(Server.MapPath("") + "\\Certificados\\" + item.id_certificado + "\\Lista\\"))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("") + "\\Certificados\\" + item.id_certificado + "\\Lista\\");
                    }

                    qArquivo.SaveAs(Server.MapPath("") + "\\Certificados\\" + item.id_certificado + "\\Lista\\" + qNomeArquivo);

                    string sCaminho = Server.MapPath("") + "\\Certificados\\" + item.id_certificado + "\\Lista\\" + qNomeArquivo;
                    string qExt = Path.GetExtension(qArquivo.FileName).ToUpper();

                    string con;

                    if (qExt.CompareTo(".XLS") == 0)
                        con = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sCaminho + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
                    else
                        con = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sCaminho + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  

                    OleDbConnection connection = new OleDbConnection(con);
                    
                    connection.Open();
                    OleDbCommand command = new OleDbCommand("select * from [Plan1$]", connection);
                    certificados_participantes item_participantes;
                    int iNum_seq = item.numero_seq_inicial.Value;
                    bool bTemNum_Seq = false;
                    int i = 0;

                    CertificadoAplicacao aplicacaoCertificado = new CertificadoAplicacao();
                    item_participantes = new certificados_participantes();
                    item_participantes.id_certificado = item.id_certificado;
                    aplicacaoCertificado.LimpaItem_Participante(item_participantes);

                    using (OleDbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            i++;
                            item_participantes = new certificados_participantes();
                            item_participantes.id_certificado = item.id_certificado;
                            if (dr[0].ToString() == "")
                            {
                                aplicacaoCertificado.LimpaItem_Participante(item_participantes);
                                item = aplicacaoCertificado.BuscaItem(item);
                                Session["certificados"] = item;
                                connection.Close();
                                connection.Dispose();
                                qArquivo = null;
                                json = "[{\"P0\":\"Erro\",\"P1\":\"Falta a informação na linha: " + i + "\",\"P2\":\"\",\"P3\":\"\"}]";
                                this.Context.Response.ContentType = "application/json; charset=utf-8";
                                this.Context.Response.Write(json);
                                return;
                            }
                            if (dr[1].ToString() == "")
                            {
                                aplicacaoCertificado.LimpaItem_Participante(item_participantes);
                                item = aplicacaoCertificado.BuscaItem(item);
                                Session["certificados"] = item;
                                connection.Close();
                                connection.Dispose();
                                qArquivo = null;
                                json = "[{\"P0\":\"Erro\",\"P1\":\"Falta a informação na linha: " + i + "\",\"P2\":\"\",\"P3\":\"\"}]";
                                this.Context.Response.ContentType = "application/json; charset=utf-8";
                                this.Context.Response.Write(json);
                                return;
                            }
                            item_participantes.nome = dr[0].ToString();
                            item_participantes.interno_externo = dr[1].ToString();

                            if (dr.FieldCount >2)
                            {
                                item_participantes.email = dr[2].ToString();

                                SHA1CryptoServiceProvider objSHA1 = new SHA1CryptoServiceProvider();
                                ASCIIEncoding objEncoding = new ASCIIEncoding();
                                string sChave = Convert.ToBase64String(objSHA1.ComputeHash(objEncoding.GetBytes(iNum_seq.ToString() + i.ToString() + Convert.ToString(DateTime.Now))));
                                sChave = sChave.Replace("/", "");
                                sChave = sChave.Replace("=", "");
                                sChave = sChave.Replace("?", "");
                                sChave = sChave.Replace("&", "");
                                sChave = sChave.Replace("+", "");
                                item_participantes.chave_participante = sChave;
                            }
                            else
                            {
                                item_participantes.email = "";
                            }
                            item_participantes.data_importacao = DateTime.Today;
                            item_participantes.usuario = usuario.usuario;
                            
                            do
                            {
                                bTemNum_Seq = aplicacaoCertificado.VerificaExisteNum_Seq(iNum_seq);
                                if (bTemNum_Seq)
                                {
                                    iNum_seq++;
                                }
                            } while (bTemNum_Seq);
                            item_participantes.numero_seq = iNum_seq;

                            aplicacaoCertificado.CriarItem_Participante(item_participantes);

                            iNum_seq++;
                        }
                    }

                    item_participantes = new certificados_participantes();
                    item_participantes.id_certificado = item.id_certificado;
                    item_participantes.nome = "";
                    List<certificados_participantes> lista = aplicacaoCertificado.ListaItem_participante(item_participantes);

                    item.certificados_participantes = lista;

                    Session["certificados"] = item;

                    connection.Close();
                    connection.Dispose();

                    qArquivo = null;



                }
                

                json = "[{\"P0\":\"ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //outCertificadoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fBaixarPdfCertificado()
        {
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();

                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 75))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                certificados item = (certificados)Session["certificados"];
                string sAux;
                ZipFile zip = new ZipFile();
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("Certificados");
                foreach (var elemento in item.certificados_participantes)
                {
                    sAux = Server.MapPath("") + "\\Certificados\\" + item.id_certificado.ToString() + "\\PDFs\\" + elemento.arquivo_pdf;
                    zip.AddFile(sAux, "Certificados");
                }

                string zipName = String.Format("Certificados_{0}.zip", item.id_certificado.ToString() + "_" + DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                zipName = String.Format("Certificados_{0}.zip", item.id_certificado.ToString());
                this.Context.Response.ContentType = "application/zip";
                this.Context.Response.AddHeader("content-disposition", ("attachment; filename=" + zipName));
                zip.Save(Server.MapPath("") + "\\Certificados\\" + item.id_certificado.ToString() + "\\Lista\\" + zipName);

                sAux = "\\Certificados\\" + item.id_certificado.ToString() + "\\Lista\\" + zipName;

                //this.Context.Response.Clear();
                //this.Context.Response.BufferOutput = false;
                //string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                //this.Context.Response.ContentType = "application/zip";
                //this.Context.Response.AddHeader("content-disposition", ("attachment; filename=" + zipName));
                //zip.Save(Server.MapPath("") + "\\Certificados\\1\\Lista\\" + zipName);
                //zip.Save(this.Context.Response.OutputStream);
                //this.Context.ApplicationInstance.CompleteRequest();
                //this.Context.Response.End();
                //this.Context.Response.Flush();
                //this.Context.Response.SuppressContent = true;
                //this.Context.ApplicationInstance.CompleteRequest();

                retornoGeral ret = new retornoGeral();
                List<retornoGeral> listaRetorno = new List<retornoGeral>();
                ret.P0 = "ok";
                ret.P1 = sAux;
                listaRetorno.Add(ret);

                json = jsSS.Serialize(listaRetorno);

                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

                //json = "[{\"P0\":\"ok\",\"P1\":\"" + sAux + "\",\"P2\":\"\",\"P3\":\"\"}]";
                //this.Context.Response.ContentType = "application/json; charset=utf-8";
                //this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //outCertificadoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fEnviaEmailCertificado()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();

                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 75))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                certificados item = (certificados)Session["certificados"];

                string qDestinatario = HttpContext.Current.Request.Form["qDestinatario"] ;
                string qAssunto = HttpContext.Current.Request.Form["qAssunto"];
                string qCorpo = HttpContext.Current.Request.Form["qCorpo"];

                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Configuracoes item_configuracoes;
                // 1 = email mestrado@ipt.br
                // 2 = email suporte@ipt.br
                item_configuracoes = aplicacaoGerais.BuscaConfiguracoes(2);

                string sFrom = item_configuracoes.remetente_email;
                string sFrom_Nome = item_configuracoes.nome_remetente_email;
                string sTo;
                string sAssunto = qAssunto;
                string sAux = "";
                string sRetorno = "";

                CertificadoAplicacao apliccaoCertificado = new CertificadoAplicacao();
                List<certificados_participantes> listaParticipantes;

                if (qDestinatario == "Todos")
                {
                    listaParticipantes = item.certificados_participantes.Where(x => x.email != "" && x.email != null).ToList();
                }
                else
                {
                    listaParticipantes = item.certificados_participantes.Where(x=> x.email.Trim() == qDestinatario).ToList();

                }

                foreach (var elemento in listaParticipantes.Where(x=> x.email != "" && x.email != null))
                {
                    sAux = qCorpo;

                    if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString == "Producao")
                    {
                        sTo = elemento.email;
                    }
                    else
                    {
                        sTo = "kelsey@ipt.br"; // usuario.email;
                        sAux = sAux + "<br><br> <strong>Esse email seria enviado para:</strong>" + elemento.email;
                    }

                    sAux = sAux.Replace("{participante}", elemento.nome);

                    string qNovaSenha = Utilizades.GerarSenha();

                    sAux = sAux.Replace("{senha}", qNovaSenha);

                    sAux = sAux.Replace("{href_certificado}", "https://sapiens.ipt.br/outDownloadCertificado.aspx?chave=" + elemento.chave_participante);

                    //sTo = "kelsey@ipt.br";
                    if (Utilizades.fEnviaEmail(sFrom, sFrom_Nome, sTo, "", "", sAssunto, sAux, item_configuracoes.servidor_email, item_configuracoes.conta_email, item_configuracoes.senha_email, item_configuracoes.porta_email.Value, 1, ""))
                    {
                        elemento.senha = qNovaSenha;
                        elemento.data_envio_email = DateTime.Today;
                        apliccaoCertificado.AlterarItem_participante(elemento);
                    }
                    else
                    {
                        if (sRetorno != "")
                        {
                            sRetorno = sRetorno + "<br>";
                        }
                        sRetorno = sRetorno + elemento.nome;
                    }
                    
                    //(this.Master as SERPI).PreencheSininho();
                }

                Session["certificados"] = item;

                retornoGeral ret = new retornoGeral();
                List<retornoGeral> listaRetorno = new List<retornoGeral>();
                if (sRetorno == "")
                {
                    ret.P0 = "ok";
                }
                else
                {
                    ret.P0 = "nok";
                    ret.P1 = sRetorno;
                }

                
                listaRetorno.Add(ret);

                json = jsSS.Serialize(listaRetorno);

                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

                //json = "[{\"P0\":\"ok\",\"P1\":\"" + sAux + "\",\"P2\":\"\",\"P3\":\"\"}]";
                //this.Context.Response.ContentType = "application/json; charset=utf-8";
                //this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }
        //==========================================================
        //Certificado - Fim
        //==========================================================

        //==========================================================
        //Cadastro Automático de Aluno - Início
        //==========================================================

        //outCadastroAutomáticoAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fPreencheTurmaCadAutAlunosAlunos()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 78)) // 78. Cadastro Automático de Alunos - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                CadastroAutomaticoAlunoAplicacao aplicacaoTela = new CadastroAutomaticoAlunoAplicacao();
                alunos_cadastro_automatico item = (alunos_cadastro_automatico)Session["alunos_cadastro_automatico"];

                List<retornoGeral> listaRetorno = new List<retornoGeral>();

                if (item.alunos_cadastro_automatico_det.Count > 0)
                {
                    retornoGeral retorno;

                    foreach (var elemento in item.alunos_cadastro_automatico_det.OrderBy(x => x.nome))
                    {
                        retorno = new retornoGeral();
                        retorno.P0 = elemento.idaluno.ToString();
                        retorno.P1 = elemento.nome;
                        retorno.P2 = elemento.cpf;
                        retorno.P3 = elemento.email;
                        if (elemento.data_envio_email != null)
                        {
                            retorno.P4 = elemento.data_envio_email.Value.ToString("dd/MM/yyyy");

                        }

                        if (elemento.email != "" && elemento.idaluno != null)
                        {
                            retorno.P5 = "<div title=\"Enviar e-mail\"> <a class=\"btn btn-info btn-circle fa fa-envelope-o\" href=\'javascript:fAbrirModalEnviarEmailLote(\""
                                + elemento.email + "\")\'; ></a></div>";

                        }

                        listaRetorno.Add(retorno);
                    }
                }

                json = jsSS.Serialize(listaRetorno);

                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //outCadastroAutomáticoAlunoGestao
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fSelecionouExcelCadAutAlunosAlunos()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();

                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 78))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                HttpPostedFile qArquivo = HttpContext.Current.Request.Files["qArquivo"];
                alunos_cadastro_automatico item = (alunos_cadastro_automatico)Session["alunos_cadastro_automatico"];

                if (qArquivo.ContentLength > 0)
                {
                    string qNomeArquivo = qArquivo.FileName;
                    int qTamanhoArquivo = qArquivo.ContentLength;
                    byte[] arrayByte = new byte[qTamanhoArquivo];

                    qArquivo.InputStream.Read(arrayByte, 0, qTamanhoArquivo);

                    if (!System.IO.Directory.Exists(Server.MapPath("") + "\\CadastroAutomaticoAlunos\\"))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("") + "\\CadastroAutomaticoAlunos\\");
                    }

                    if (!System.IO.Directory.Exists(Server.MapPath("") + "\\CadastroAutomaticoAlunos\\" + item.id_cadastro_automatico + "\\"))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("") + "\\CadastroAutomaticoAlunos\\" + item.id_cadastro_automatico + "\\");
                    }

                    if (!System.IO.Directory.Exists(Server.MapPath("") + "\\CadastroAutomaticoAlunos\\" + item.id_cadastro_automatico + "\\Lista\\"))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("") + "\\CadastroAutomaticoAlunos\\" + item.id_cadastro_automatico + "\\Lista\\");
                    }

                    qArquivo.SaveAs(Server.MapPath("") + "\\CadastroAutomaticoAlunos\\" + item.id_cadastro_automatico + "\\Lista\\" + qNomeArquivo);

                    string sCaminho = Server.MapPath("") + "\\CadastroAutomaticoAlunos\\" + item.id_cadastro_automatico + "\\Lista\\" + qNomeArquivo;
                    string qExt = Path.GetExtension(qArquivo.FileName).ToUpper();

                    string con;

                    if (qExt.CompareTo(".XLS") == 0)
                        con = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sCaminho + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
                    else
                        con = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sCaminho + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  

                    OleDbConnection connection = new OleDbConnection(con);

                    connection.Open();
                    OleDbCommand command = new OleDbCommand("select * from [Planilha1$]", connection);
                    alunos_cadastro_automatico_det item_participantes;
                    int i = 0;

                    CadastroAutomaticoAlunoAplicacao aplicacaoCadastro = new CadastroAutomaticoAlunoAplicacao();
                    item_participantes = new alunos_cadastro_automatico_det();
                    item_participantes.id_cadastro_automatico = item.id_cadastro_automatico;
                    aplicacaoCadastro.LimpaItem_Participante(item_participantes);
                    string sAux = "";

                    // Aqui só irá validar planilha
                    using (OleDbDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            if (dr.FieldCount >= 3)
                            {
                                while (dr.Read())
                                {
                                    i++;
                                    //item_participantes = new alunos_cadastro_automatico_det();
                                    //item_participantes.id_cadastro_automatico = item.id_cadastro_automatico;
                                    if (dr[0].ToString().Trim() == "")
                                    {
                                        sAux = sAux + "Falta a informação na linha: " + i + " e coluna 'Nome'.<br>";
                                        //aplicacaoCadastro.LimpaItem_Participante(item_participantes);
                                        //connection.Close();
                                        //connection.Dispose();
                                        //qArquivo = null;
                                        //json = "[{\"P0\":\"Erro\",\"P1\":\"Falta a informação na linha: " + i + "\",\"P2\":\"\",\"P3\":\"\"}]";
                                        //this.Context.Response.ContentType = "application/json; charset=utf-8";
                                        //this.Context.Response.Write(json);
                                        //item.alunos_cadastro_automatico_det.Clear();
                                        //return;
                                    }
                                    if (dr[1].ToString().Trim() == "")
                                    {
                                        sAux = sAux + "Falta a informação na linha: " + i + " e coluna 'CPF'.<br>";
                                        //aplicacaoCadastro.LimpaItem_Participante(item_participantes);
                                        //connection.Close();
                                        //connection.Dispose();
                                        //qArquivo = null;
                                        //json = "[{\"P0\":\"Erro\",\"P1\":\"Falta a informação na linha: " + i + "\",\"P2\":\"\",\"P3\":\"\"}]";
                                        //this.Context.Response.ContentType = "application/json; charset=utf-8";
                                        //this.Context.Response.Write(json);
                                        //item.alunos_cadastro_automatico_det.Clear();
                                        //return;
                                    }
                                    else if (!Utilizades.fValidaCPF(dr[1].ToString().Trim()))
                                    {
                                        sAux = sAux + "CPF inválido na linha: " + i + ".<br>";
                                        //aplicacaoCadastro.LimpaItem_Participante(item_participantes);
                                        //connection.Close();
                                        //connection.Dispose();
                                        //qArquivo = null;
                                        //json = "[{\"P0\":\"Erro\",\"P1\":\"CPF inválido na linha: " + i + "\",\"P2\":\"\",\"P3\":\"\"}]";
                                        //this.Context.Response.ContentType = "application/json; charset=utf-8";
                                        //this.Context.Response.Write(json);
                                        ////alunos_cadastro_automatico_det alunosZerados = new alunos_cadastro_automatico_det();
                                        //item.alunos_cadastro_automatico_det.Clear();
                                        //return;
                                    }
                                    if (dr[2].ToString().Trim() == "")
                                    {
                                        sAux = sAux + "Falta a informação na linha: " + i + " e coluna 'E-mail'.<br>";
                                        //aplicacaoCadastro.LimpaItem_Participante(item_participantes);
                                        //connection.Close();
                                        //connection.Dispose();
                                        //qArquivo = null;
                                        //json = "[{\"P0\":\"Erro\",\"P1\":\"Falta a informação na linha: " + i + "\",\"P2\":\"\",\"P3\":\"\"}]";
                                        //this.Context.Response.ContentType = "application/json; charset=utf-8";
                                        //this.Context.Response.Write(json);
                                        //item.alunos_cadastro_automatico_det.Clear();
                                        //return;
                                    }
                                    else if (dr[2].ToString().Trim().IndexOf("@") == -1)
                                    {
                                        sAux = sAux + "E-mail inválido na linha:: " + i + ".<br>";
                                        //aplicacaoCadastro.LimpaItem_Participante(item_participantes);
                                        //connection.Close();
                                        //connection.Dispose();
                                        //qArquivo = null;
                                        //json = "[{\"P0\":\"Erro\",\"P1\":\"E-mail inválido na linha: " + i + "\",\"P2\":\"\",\"P3\":\"\"}]";
                                        //this.Context.Response.ContentType = "application/json; charset=utf-8";
                                        //this.Context.Response.Write(json);
                                        //item.alunos_cadastro_automatico_det.Clear();
                                        //return;
                                    }
                                    //item_participantes.nome = dr[0].ToString();
                                    //item_participantes.cpf = dr[1].ToString();
                                    //item_participantes.email = dr[2].ToString();

                                    //do
                                    //{
                                    //    bTemNum_Seq = aplicacaoCadastro.VerificaExisteNum_Seq(iNum_seq);
                                    //    if (bTemNum_Seq)
                                    //    {
                                    //        iNum_seq++;
                                    //    }
                                    //} while (bTemNum_Seq);
                                    //item_participantes.numero_seq = iNum_seq;

                                    //item_participantes = aplicacaoCadastro.CriarItem_Participante(item_participantes);
                                    //item.alunos_cadastro_automatico_det.Add(item_participantes);
                                }
                            }
                            else
                            {
                                aplicacaoCadastro.LimpaItem_Participante(item_participantes);
                                connection.Close();
                                connection.Dispose();
                                qArquivo = null;
                                json = "[{\"P0\":\"Erro\",\"P1\":\"A Planilha tem que ter, pelo menos, 3 colunas.\",\"P2\":\"\",\"P3\":\"\"}]";
                                this.Context.Response.ContentType = "application/json; charset=utf-8";
                                this.Context.Response.Write(json);
                                item.alunos_cadastro_automatico_det.Clear();
                                return;
                            }
                        }
                    }

                    if (sAux != "")
                    {
                        aplicacaoCadastro.LimpaItem_Participante(item_participantes);
                        connection.Close();
                        connection.Dispose();
                        qArquivo = null;
                        json = "[{\"P0\":\"Erro\",\"P1\":\"" + sAux + "\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                        item.alunos_cadastro_automatico_det.Clear();
                        return;
                    }
                   
                    using (OleDbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            item_participantes = new alunos_cadastro_automatico_det();
                            item_participantes.id_cadastro_automatico = item.id_cadastro_automatico;
                                    
                            item_participantes.nome = dr[0].ToString();
                            item_participantes.cpf = dr[1].ToString();
                            item_participantes.email = dr[2].ToString();

                            item_participantes = aplicacaoCadastro.CriarItem_Participante(item_participantes);
                            item.alunos_cadastro_automatico_det.Add(item_participantes);
                        }
                    }

                    //item_participantes = new certificados_participantes();
                    //item_participantes.id_certificado = item.id_certificado;
                    //item_participantes.nome = "";
                    //List<certificados_participantes> lista = aplicacaoCadastro.ListaItem_participante(item_participantes);

                    //item.alunos_cadastro_automatico_det.Add(lista);

                    Session["certificados"] = item;

                    connection.Close();
                    connection.Dispose();

                    qArquivo = null;
                }


                json = "[{\"P0\":\"ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fEnviaEmailCadAutAlunosAlunos()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();

                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 78))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                alunos_cadastro_automatico item = (alunos_cadastro_automatico)Session["alunos_cadastro_automatico"];

                string qDestinatario = HttpContext.Current.Request.Form["qDestinatario"];
                string qAssunto = HttpContext.Current.Request.Form["qAssunto"];
                string qCorpo = HttpContext.Current.Request.Form["qCorpo"];

                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Configuracoes item_configuracoes;
                // 1 = email mestrado@ipt.br
                // 2 = email suporte@ipt.br
                item_configuracoes = aplicacaoGerais.BuscaConfiguracoes(2);

                string sFrom = item_configuracoes.remetente_email;
                string sFrom_Nome = item_configuracoes.nome_remetente_email;
                string sTo;
                string sAssunto = qAssunto;
                string sAux = "";
                string sRetorno = "";

                CadastroAutomaticoAlunoAplicacao p_Aplicacao = new CadastroAutomaticoAlunoAplicacao();
                List<alunos_cadastro_automatico_det> listaParticipantes;

                if (qDestinatario == "Todos")
                {
                    listaParticipantes = item.alunos_cadastro_automatico_det.Where(x => x.email != "" && x.email != null && x.data_envio_email == null).ToList();
                }
                else
                {
                    listaParticipantes = item.alunos_cadastro_automatico_det.Where(x => x.email.Trim() == qDestinatario).ToList();

                }

                foreach (var elemento in listaParticipantes.Where(x => x.email != "" && x.email != null))
                {
                    sAux = qCorpo;

                    if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString == "Producao")
                    {
                        sTo = elemento.email;
                    }
                    else
                    {
                        sTo = "kelsey@ipt.br"; // usuario.email;
                        sAux = sAux + "<br><br> <strong>Esse email seria enviado para:</strong>" + elemento.email;
                    }

                    sAux = sAux.Replace("{aluno}", elemento.nome);
                    sAux = sAux.Replace("{login}", elemento.idaluno.ToString());
                    sAux = sAux.Replace("{senha}", elemento.cpf.Replace(".", "").Substring(0, 6));

                    //sTo = "kelsey@ipt.br";
                    if (Utilizades.fEnviaEmail(sFrom, sFrom_Nome, sTo, "", "", sAssunto, sAux, item_configuracoes.servidor_email, item_configuracoes.conta_email, item_configuracoes.senha_email, item_configuracoes.porta_email.Value, 1, ""))
                    {
                        elemento.data_envio_email = DateTime.Today;
                        p_Aplicacao.AlterarItem_participante(elemento);
                    }
                    else
                    {
                        if (sRetorno != "")
                        {
                            sRetorno = sRetorno + "<br>";
                        }
                        sRetorno = sRetorno + elemento.nome;
                    }

                    //(this.Master as SERPI).PreencheSininho();
                }

                Session["alunos_cadastro_automatico"] = item;

                retornoGeral ret = new retornoGeral();
                List<retornoGeral> listaRetorno = new List<retornoGeral>();
                if (sRetorno == "")
                {
                    ret.P0 = "ok";
                }
                else
                {
                    ret.P0 = "nok";
                    ret.P1 = sRetorno;
                }


                listaRetorno.Add(ret);

                json = jsSS.Serialize(listaRetorno);

                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

                //json = "[{\"P0\":\"ok\",\"P1\":\"" + sAux + "\",\"P2\":\"\",\"P3\":\"\"}]";
                //this.Context.Response.ContentType = "application/json; charset=utf-8";
                //this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }
        //==========================================================
        //Cadastro Automático de Aluno - Fim
        //==========================================================

        //==========================================================
        //FIPT - Início
        //==========================================================
        //finListaInadimplenteFIPT
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fBuscaCorpoEmailInadimplentes()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();

                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 76))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId_aluno_Curso = HttpContext.Current.Request.Form["qId_aluno_Curso"];

                if (qId_aluno_Curso.Trim() == "")
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Parâmetro não enviado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                FIPTAplicacao aplicacaoFIPT = new FIPTAplicacao();
                alunos_curso_inadimplente item = new alunos_curso_inadimplente();
                item.id_aluno_curso_inadimplente = Convert.ToInt32(qId_aluno_Curso);
                item = aplicacaoFIPT.BuscaItem(item);

                if (item == null)
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Curso do Aluno Não Encontrado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                StreamReader objReader;
                objReader = new StreamReader(HttpRuntime.AppDomainAppPath + "\\Templates\\emails\\EmailCobrancaInadimplentes.html");
                string sCorpo = objReader.ReadToEnd();
                objReader.Close();
                sCorpo = sCorpo.Replace("{nome_aluno}", item.alunos_inadimpentes_fipt.nome);
                sCorpo = sCorpo.Replace("{nome_curso}", item.NomeCurso);
                sCorpo = sCorpo.Replace("{data_calculada}", DateTime.Today.ToString("dd/MM/yyyy"));
                sCorpo = sCorpo.Replace("{nome_usuario}", usuario.nome);
                sCorpo = sCorpo.Replace("{email_usuario}", usuario.email);

                string sAux = "";

                foreach (var elemento in item.alunos_parcelas_inadimplente)
                {
                    sAux = sAux + "<tr style=\"height: 24pt;\">";

                    sAux = sAux + "    <td nowrap=\"\" style=\"width: 105pt; height: 24pt; padding: 0 3.5pt; border-width: 1pt; border-style: none solid solid solid; border-color: windowtext;\">";
                    sAux = sAux + "         <p align=\"center\" style=\"font-size: 11pt; font-family: Calibri,sans-serif; text-align: center; margin: 0;\">";
                    sAux = sAux + "             <span style=\"color: black; font-size: 12pt;\">" + elemento.DataVencimento.Value.ToString("dd/MM/yyyy") + "</span>";
                    sAux = sAux + "         </p>";
                    sAux = sAux + "     </td>";

                    sAux = sAux + "    <td nowrap=\"\" style=\"width: 87.75pt; height: 24pt; padding: 0 3.5pt; border-style: none solid solid none; border-right-width: 1pt; border-bottom-width: 1pt; border-right-color: windowtext; border-bottom-color: windowtext;\">";
                    sAux = sAux + "         <p align=\"center\" style=\"font-size: 11pt; font-family: Calibri,sans-serif; text-align: right; margin: 0; \">";
                    sAux = sAux + "             <span style=\"color:black; font-size: 12pt;\">" + string.Format("{0:N}", elemento.ValorOriginal) + " </span>";
                    sAux = sAux + "         </p>";
                    sAux = sAux + "     </td>";

                    sAux = sAux + "    <td nowrap=\"\" style=\"width: 94.45pt; height: 24pt; padding: 0 3.5pt; border-style: none solid solid none; border-right-width: 1pt; border-bottom-width: 1pt; border-right-color: windowtext; border-bottom-color: windowtext;\">";
                    sAux = sAux + "         <p align=\"center\" style=\"font-size: 11pt; font-family: Calibri,sans-serif; text-align: center; margin: 0; \">";
                    sAux = sAux + "             <span style=\"color:black; font-size: 12pt;\">" + elemento.dias_atraso.ToString() + " </span>";
                    sAux = sAux + "         </p>";
                    sAux = sAux + "     </td>";

                    sAux = sAux + "    <td nowrap=\"\" style=\"width: 94.9pt; height: 24pt; padding: 0 3.5pt; border-style: none solid solid none; border-right-width: 1pt; border-bottom-width: 1pt; border-right-color: windowtext; border-bottom-color: windowtext;\">";
                    sAux = sAux + "         <p align=\"center\" style=\"font-size: 11pt; font-family: Calibri,sans-serif; text-align: right; margin: 0; \">";
                    sAux = sAux + "             <span style=\"color:black; font-size: 12pt;\">" + string.Format("{0:N}", elemento.multa) + " </span>";
                    sAux = sAux + "         </p>";
                    sAux = sAux + "     </td>";

                    sAux = sAux + "    <td nowrap=\"\" style=\"width: 109pt; height: 24pt; padding: 0 3.5pt; border-style: none solid solid none; border-right-width: 1pt; border-bottom-width: 1pt; border-right-color: windowtext; border-bottom-color: windowtext;\">";
                    sAux = sAux + "         <p align=\"center\" style=\"font-size: 11pt; font-family: Calibri,sans-serif; text-align: right; margin: 0; \">";
                    sAux = sAux + "             <span style=\"color:black; font-size: 12pt;\">" + string.Format("{0:N}", elemento.juros) + " </span>";
                    sAux = sAux + "         </p>";
                    sAux = sAux + "     </td>";

                    sAux = sAux + "    <td nowrap=\"\" style=\"width: 92.9pt; height: 24pt; padding: 0 3.5pt; border-style: none solid solid none; border-right-width: 1pt; border-bottom-width: 1pt; border-right-color: windowtext; border-bottom-color: windowtext;\">";
                    sAux = sAux + "         <p align=\"center\" style=\"font-size: 11pt; font-family: Calibri,sans-serif; text-align: right; margin: 0; \">";
                    sAux = sAux + "             <span style=\"color:black; font-size: 12pt;\">" + string.Format("{0:N}", elemento.valor_corrigido) + " </span>";
                    sAux = sAux + "         </p>";
                    sAux = sAux + "     </td>";

                    sAux = sAux + " </tr>";
                }

                sAux = sAux + "<tr style=\"height: 24pt;\">";

                sAux = sAux + "    <td nowrap=\"\" style=\"width: 105pt; height: 24pt; padding: 0 3.5pt; border-width: 1pt; border-style: none solid solid solid; border-color: windowtext;\">";
                sAux = sAux + "         <p align=\"center\" style=\"font-size: 11pt; font-family: Calibri,sans-serif; text-align: center; margin: 0;\">";
                sAux = sAux + "             <span style=\"color: black; font-size: 14pt;\">TOTAL</span>";
                sAux = sAux + "         </p>";
                sAux = sAux + "     </td>";

                sAux = sAux + "    <td nowrap=\"\" style=\"width: 87.75pt; height: 24pt; padding: 0 3.5pt; border-style: none solid solid none; border-right-width: 1pt; border-bottom-width: 1pt; border-right-color: windowtext; border-bottom-color: windowtext;\">";
                sAux = sAux + "         <p align=\"center\" style=\"font-size: 11pt; font-family: Calibri,sans-serif; text-align: right; margin: 0; \">";
                sAux = sAux + "             <span style=\"color:black; font-size: 14pt; \">" + string.Format("{0:N}", item.alunos_parcelas_inadimplente.Sum(x=> x.ValorOriginal)) + " </span>";
                sAux = sAux + "         </p>";
                sAux = sAux + "     </td>";

                sAux = sAux + "    <td nowrap=\"\" style=\"width: 94.45pt; height: 24pt; padding: 0 3.5pt; border-style: none solid solid none; border-right-width: 1pt; border-bottom-width: 1pt; border-right-color: windowtext; border-bottom-color: windowtext;\">";
                sAux = sAux + "         <p align=\"center\" style=\"font-size: 11pt; font-family: Calibri,sans-serif; text-align: center; margin: 0; \">";
                sAux = sAux + "             <span style=\"color:black; font-size: 14pt;\">&nbsp;- </span>";
                sAux = sAux + "         </p>";
                sAux = sAux + "     </td>";

                sAux = sAux + "    <td nowrap=\"\" style=\"width: 94.9pt; height: 24pt; padding: 0 3.5pt; border-style: none solid solid none; border-right-width: 1pt; border-bottom-width: 1pt; border-right-color: windowtext; border-bottom-color: windowtext;\">";
                sAux = sAux + "         <p align=\"center\" style=\"font-size: 11pt; font-family: Calibri,sans-serif; text-align: right; margin: 0; \">";
                sAux = sAux + "             <span style=\"color:black; font-size: 14pt;\">" + string.Format("{0:N}", item.alunos_parcelas_inadimplente.Sum(x => x.multa)) + " </span>";
                sAux = sAux + "         </p>";
                sAux = sAux + "     </td>";

                sAux = sAux + "    <td nowrap=\"\" style=\"width: 109pt; height: 24pt; padding: 0 3.5pt; border-style: none solid solid none; border-right-width: 1pt; border-bottom-width: 1pt; border-right-color: windowtext; border-bottom-color: windowtext;\">";
                sAux = sAux + "         <p align=\"center\" style=\"font-size: 11pt; font-family: Calibri,sans-serif; text-align: right; margin: 0; \">";
                sAux = sAux + "             <span style=\"color:black; font-size: 14pt;\">" + string.Format("{0:N}", item.alunos_parcelas_inadimplente.Sum(x => x.juros)) + " </span>";
                sAux = sAux + "         </p>";
                sAux = sAux + "     </td>";

                sAux = sAux + "    <td nowrap=\"\" style=\"width: 92.9pt; height: 24pt; padding: 0 3.5pt; border-style: none solid solid none; border-right-width: 1pt; border-bottom-width: 1pt; border-right-color: windowtext; border-bottom-color: windowtext;\">";
                sAux = sAux + "         <p align=\"center\" style=\"font-size: 11pt; font-family: Calibri,sans-serif; text-align: right; margin: 0; \">";
                sAux = sAux + "             <span style=\"color:black; font-size: 14pt;\">" + string.Format("{0:N}", item.alunos_parcelas_inadimplente.Sum(x => x.valor_corrigido)) + " </span>";
                sAux = sAux + "         </p>";
                sAux = sAux + "     </td>";

                sAux = sAux + " </tr>";

                sCorpo = sCorpo.Replace("{corpo}", sAux);

                retornoGeral ret = new retornoGeral();
                List<retornoGeral> listaRetorno = new List<retornoGeral>();
                ret.P0 = "ok";
                ret.P1 = sCorpo;
                listaRetorno.Add(ret);

                json = jsSS.Serialize(listaRetorno);

                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

                //json = "[{\"P0\":\"ok\",\"P1\":\"" + sAux + "\",\"P2\":\"\",\"P3\":\"\"}]";
                //this.Context.Response.ContentType = "application/json; charset=utf-8";
                //this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //Menu do Aluno
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fEmiteBoletoAluno()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();
                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];
                string sAux = "";
                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 77)) // 3. Cadastro Tela - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                //int qId_alunos_boletos_parcelas = Convert.ToInt32(HttpContext.Current.Request.Form["qId_alunos_boletos_parcelas"]);
                int qIDLancamento = Convert.ToInt32(HttpContext.Current.Request.Form["qIDLancamento"]);
                string qTipoBoleto = HttpContext.Current.Request.Form["qTipoBoleto"];
                string qNossoNumero = HttpContext.Current.Request.Form["qNossoNumero"];

                alunos item_aluno = (alunos)Session["AlunoLogado"];
                boletos item_boleto = new boletos();
                InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
                FIPTAplicacao aplicacaoFipt = new FIPTAplicacao();
                alunos_boletos_parcelas item_aluno_boleto_parcela = new alunos_boletos_parcelas();
                geral_Boleto item_geral_boleto = new geral_Boleto();
                item_aluno_boleto_parcela.IDLancamento = qIDLancamento;
                item_geral_boleto.IDLancamento = qIDLancamento.ToString();


                if (qTipoBoleto == "0")
                {
                    item_geral_boleto = aplicacaoFipt.ConsultaDadosBoletosFipt(item_geral_boleto);

                    Session["geral_Boleto"] = item_geral_boleto;

                    json = "[{\"P0\":\"Ok\",\"P1\":\"Ok\",\"P2\":\"\",\"P3\":\"\"}]";

                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;

                }
                else
                {
                    item_aluno_boleto_parcela = aplicacaoFipt.BuscaItem_BoletoParcela(item_aluno_boleto_parcela);
                }
                

                // Verificar se é um Boleto Vencito (terá um novo NossoNumero) ou se é a reimpressão de um Boleto a vencer
                //0 = A vencer ; 1 = Vencido
                if (qTipoBoleto == "0")
                {
                    // A vencer
                    item_boleto.status = "Emitido";
                    item_boleto.id_conv = item_aluno_boleto_parcela.numero_convenio; // "Qual é";//303250
                    item_boleto.refTran = item_aluno_boleto_parcela.nossonumero;
                    item_boleto.valor = item_aluno_boleto_parcela.valor_original.ToString().Replace(",", "");
                    item_boleto.data_vencimento = item_aluno_boleto_parcela.data_venc.Value;
                    sAux = "PAGAMENTO REFERENTE A PARC. DE VENCIMENTO EM " + String.Format("{0:dd/MM/yyyy}", item_aluno_boleto_parcela.data_venc);
                    sAux = sAux + " **APÓS O VENCIMENTO SERÁ COBRADO JUROS E MULTA SOBRE O VALOR DA PARCELA**";
                    item_boleto.msgLoja = sAux;
                }
                else
                {
                    // Vencido
                    item_boleto.status = "Cadastrado";
                    item_boleto.id_conv = "316753";//303250
                    item_boleto.refTran = aplicacaoInscricao.Busca_Ultimo_refTran() ;
                    decimal step = (decimal)Math.Pow(10, 2);
                    decimal tmp = Math.Truncate(step * item_aluno_boleto_parcela.valor_corrigido.Value);
                    item_boleto.valor = tmp.ToString(); //  (tmp / step).ToString();
                    //item_boleto.valor = item_aluno_boleto_parcela.valor_corrigido.ToString().Replace(",", "");
                    item_boleto.data_vencimento = DateTime.Today;
                    sAux = "PAGAMENTO REFERENTE A PARC. VENCIDA EM: " + String.Format("{0:dd/MM/yyyy}", item_aluno_boleto_parcela.data_venc);
                    sAux = sAux + " - COM VLR ORIG.: " + String.Format("{0:C}", item_aluno_boleto_parcela.valor_original);
                    sAux = sAux + " - DIAS ATRASO: " + item_aluno_boleto_parcela.dias_atraso.ToString();
                    sAux = sAux + " - MULTA: " + String.Format("{0:C}", item_aluno_boleto_parcela.multa);
                    sAux = sAux + " - JUROS: " + String.Format("{0:C}", item_aluno_boleto_parcela.juros);
                    sAux = sAux + " **valor válido até a data do vencimento**";
                    item_boleto.msgLoja = sAux;
                }

                
                item_boleto.cpf = item_aluno.cpf.Replace(".","").Replace("-","");
                if (item_aluno.nome.Length > 60)
                {
                    item_boleto.nome = item_aluno.nome.Substring(0, 60);
                }
                else
                {
                    item_boleto.nome = item_aluno.nome;
                }
               
                string sEnderecoTemp = item_aluno.logradouro_res + ", " + item_aluno.numero_res + " " + item_aluno.complemento_res;
                if (sEnderecoTemp.Length > 60)
                {
                    sEnderecoTemp = sEnderecoTemp.Substring(0, 60);
                }
                item_boleto.endereco = sEnderecoTemp;

                if (item_aluno.cidade_res.Length > 18)
                {
                    item_boleto.cidade = item_aluno.cidade_res.Substring(0, 18);
                }
                else
                {
                    item_boleto.cidade = item_aluno.cidade_res;
                }
                item_boleto.uf = item_aluno.uf_res;
                item_boleto.cep = item_aluno.cep_res.Replace("-","");
                
                item_boleto.data_cadastro = DateTime.Now;
                item_boleto.data_alteracao = DateTime.Now;
                item_boleto.usuario = "web";
                //item_boleto.usuario = "web";

                item_boleto.fichas_inscricao = null;

                item_boleto = aplicacaoInscricao.CriarBoleto(item_boleto, null);

                //item_boleto = aplicacaoInscricao.AlterarBoleto(item_boleto);

                //aplicacaoInscricao.Criar_inscricao_boleto(item_boleto, item_ficha);
                if (qTipoBoleto != "0")
                {
                    // Vencido

                    refTran item_refTran = new refTran();
                    item_refTran.id_refTran = item_boleto.refTran;
                    item_refTran.DataGetGemini = DateTime.Now;
                    item_refTran.Solicitante = "Sapiens";
                    //item_refTran.Solicitante = "Sapiens";
                    item_refTran.DataUtilizacao = item_refTran.DataGetGemini;
                    item_refTran.id_boleto = item_boleto.id_boleto;
                    aplicacaoInscricao.Criar_refTran(item_refTran);

                    aplicacaoInscricao.Altera_NossoNumeto_Boleto_Gemini(item_aluno_boleto_parcela.IDLancamento.Value, item_boleto.refTran, item_boleto.msgLoja);
                }

                Session["boletos"] = item_boleto;

                json = "[{\"P0\":\"Ok\",\"P1\":\"Ok\",\"P2\":\"\",\"P3\":\"\"}]";

                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }

        //finListaInadimplenteFIPT
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fBuscaCorpoEmailIAlunoBoleto()
        {
            Session.Timeout = 60;
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();

                usuarios usuario = new usuarios();
                usuario = (usuarios)Session["UsuarioLogado"];

                if (usuario == null)
                {
                    json = "[{\"P0\":\"deslogado\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 79))
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário sem permissão para essa operação\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                string qId_Boleto = HttpContext.Current.Request.Form["qId_Boleto"];

                if (qId_Boleto.Trim() == "")
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Parâmetro não enviado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                FIPTAplicacao aplicacaoFIPT = new FIPTAplicacao();
                boleto_email item = new boleto_email();
                item.id_boleto_email = Convert.ToInt32(qId_Boleto);
                item = aplicacaoFIPT.BuscaItem(item);

                if (item == null)
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Curso do Aluno Não Encontrado\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                StreamReader objReader;
                objReader = new StreamReader(HttpRuntime.AppDomainAppPath + "\\Templates\\emails\\EmailBoletoMesCorrente.html");
                string sCorpo = objReader.ReadToEnd();
                objReader.Close();
                sCorpo = sCorpo.Replace("{nome_aluno}", item.nome);
                sCorpo = sCorpo.Replace("{mes_ano}", item.data_mes_ano.Value.ToString("MM/yyyy"));

                sCorpo = sCorpo.Replace("{vencimento}", item.data_vencimento.Value.ToString("dd/MM/yyyy"));
                sCorpo = sCorpo.Replace("{valor}", String.Format("{0:C2}", item.valor));
                sCorpo = sCorpo.Replace("{linha_digitavel}", item.linha_digitavel);
                sCorpo = sCorpo.Replace("{codigo_barra}", item.codigo_barra);

                retornoGeral ret = new retornoGeral();
                List<retornoGeral> listaRetorno = new List<retornoGeral>();
                ret.P0 = "ok";
                ret.P1 = sCorpo;
                listaRetorno.Add(ret);

                json = jsSS.Serialize(listaRetorno);

                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);

                //json = "[{\"P0\":\"ok\",\"P1\":\"" + sAux + "\",\"P2\":\"\",\"P3\":\"\"}]";
                //this.Context.Response.ContentType = "application/json; charset=utf-8";
                //this.Context.Response.Write(json);

            }
            catch (Exception ex)
            {
                string json = "[{\"P0\":\"Erro\",\"P1\":\"" + ex.Message + "\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
            }

        }
        //==========================================================
        //FIPT - Fim
        //==========================================================

    }
}
