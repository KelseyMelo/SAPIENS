using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;
using Aplicacao_C;
using SERPI.Dominio_C;

using System.Configuration;

namespace SERPI.UI.WebForms_C
{
    public class Global : System.Web.HttpApplication
    {
        private static CacheItemRemovedCallback OnCacheRemove = null;

        protected void Application_Start(object sender, EventArgs e)
        {
            //RegisterCacheEntry();
            AddTask("DoStuff", 14400); //3600 = 1h // 14400 = 4h  // 21600 = 6h
        }

        private void AddTask(string name, int seconds)
        {
            OnCacheRemove = new CacheItemRemovedCallback(CacheItemRemoved);
            HttpRuntime.Cache.Insert(name, seconds, null,
                DateTime.Now.AddSeconds(seconds), Cache.NoSlidingExpiration,
                CacheItemPriority.NotRemovable, OnCacheRemove);
            DoWork();
        }

        public void CacheItemRemoved(string k, object v, CacheItemRemovedReason r)
        {
            // do stuff here if it matches our taskname, like WebRequest
            // re-add our task so it recurs
            AddTask(k, Convert.ToInt32(v));
        }

        private void DoWork()
        {
            Debug.WriteLine("Begin DoWork...");
            Debug.WriteLine("Running as: " +  WindowsIdentity.GetCurrent().Name);

            DoSomeFileWritingStuff();

            Debug.WriteLine("End DoWork...");
        }

        private void DoSomeFileWritingStuff()
        {
            Debug.WriteLine("Writing to file...");

            try
            {
                InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
                List<boletos> lista = aplicacaoInscricao.ListaBoletosAbertos();

                boletos item_boleto;
                historico_inscricao item_historico;

                AreaAplicacao aplicacaoArea = new AreaAplicacao();
                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Configuracoes item_configuracoes;
                fichas_inscricao item_ficha;
                areas_concentracao item_area = new areas_concentracao();
                foreach (var elemento in lista)
                {
                    var retorno = aplicacaoInscricao.VarificaBoletoPago_Gemini(elemento.refTran);
                    if (retorno.Item1 == "Pago")
                    {
                        item_boleto = new boletos();
                        item_historico = new historico_inscricao();
                        item_ficha = elemento.fichas_inscricao.FirstOrDefault();

                        item_boleto.id_boleto = elemento.id_boleto;
                        item_boleto.data_pagamento = retorno.Item2;
                        item_boleto.data_alteracao = DateTime.Now;
                        item_boleto.usuario = "admin";
                        if (item_ficha != null)
                        {
                            item_historico.id_inscricao = item_ficha.id_inscricao;
                            item_historico.data = item_boleto.data_alteracao.Value;
                            item_historico.status = "Inscrição Paga";
                            item_historico.usuario = "admin";
                        }
                        else
                        {
                            item_historico = null;
                        }

                        if (aplicacaoInscricao.IncluirDataPagamentoBoleto(item_boleto, item_historico))
                        {
                            if (ConfigurationManager.ConnectionStrings["qAmbiente"].ConnectionString == "Producao")
                            {
                                // 1 = email mestrado@ipt.br
                                // 2 = email suporte@ipt.br
                                item_configuracoes = aplicacaoGerais.BuscaConfiguracoes(1);

                                string sFrom = item_configuracoes.remetente_email;
                                string sFrom_Nome = item_configuracoes.nome_remetente_email;
                                string sTo = item_ficha.email_res;
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
                            }
                        }
                    }
                    else if (retorno.Item1 == "Cancelado")
                    {
                        item_boleto = new boletos();
                        item_historico = new historico_inscricao();
                        item_ficha = elemento.fichas_inscricao.FirstOrDefault();

                        item_boleto.id_boleto = elemento.id_boleto;
                        item_boleto.data_cancelamento = retorno.Item2;
                        item_boleto.data_alteracao = DateTime.Now;
                        item_boleto.usuario = "admin";

                        if (item_ficha != null)
                        {
                            item_historico.id_inscricao = item_ficha.id_inscricao;
                            item_historico.data = item_boleto.data_alteracao.Value;
                            item_historico.status = "Boleto Cancelado";
                            item_historico.usuario = "admin";
                        }
                        else
                        {
                            item_historico = null;
                        }

                        aplicacaoInscricao.IncluirDataPagamentoBoleto(item_boleto, item_historico);
                    }
                    else if (retorno.Item1 == "SemRegistro")
                    {
                        item_boleto = new boletos();
                        item_historico = new historico_inscricao();
                        item_ficha = elemento.fichas_inscricao.FirstOrDefault();

                        item_boleto.id_boleto = elemento.id_boleto;
                        item_boleto.data_verificacao_sem_registro = DateTime.Now;
                        item_boleto.data_alteracao = DateTime.Now;
                        item_boleto.usuario = "admin";

                        if (item_ficha != null)
                        {
                            item_historico.id_inscricao = item_ficha.id_inscricao;
                            item_historico.data = item_boleto.data_alteracao.Value;
                            item_historico.status = "Sem Regsitro Gemini";
                            item_historico.usuario = "admin";
                        }
                        else
                        {
                            item_historico = null;
                        }
                        aplicacaoInscricao.IncluirDataPagamentoBoleto(item_boleto, item_historico);
                    }

                }

                if (!Directory.Exists(HttpRuntime.AppDomainAppPath + "\\Logs"))
                {
                    Directory.CreateDirectory(HttpRuntime.AppDomainAppPath + "\\Logs\\");
                }

                using (StreamWriter writer = new StreamWriter(HttpRuntime.AppDomainAppPath + "Logs\\logBoleto.txt", true))
                {
                    writer.WriteLine("Cache Callback: {0}", DateTime.Now);
                    writer.Close();
                }
            }
            catch (Exception x)
            {
                Debug.WriteLine(x);
            }

            Debug.WriteLine("File write successful");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}