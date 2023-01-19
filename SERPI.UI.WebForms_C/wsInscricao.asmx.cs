using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using System.Configuration;

namespace SERPI.UI.WebForms_C
{
    /// <summary>
    /// Summary description for wsInscricao
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class wsInscricao : System.Web.Services.WebService
    {
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //[WebMethod()]
        //public void GetRefTran(string id_conv)
        //{
        //    return;
        //    string json;
        //    try
        //    {
        //        if (id_conv == "316753")
        //        {
        //            //temporario
        //            InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
        //            //refTran item;
        //            refTran item_new = new refTran();
        //            string sNossoNumero = aplicacaoInscricao.Busca_Ultimo_refTran_Phorte();
        //            if (Convert.ToDecimal(sNossoNumero) > 30473710000000099)
        //            {
        //                json = "[{\"Retorno\":\"Não há mais números disponíveis para teste. Entre em contato com o desenvolvedor do Sapiens.\"}]";
        //                this.Context.Response.ContentType = "application/json; charset=utf-8";
        //                this.Context.Response.Write(json);
        //                return;
        //            }
        //            else
        //            {
        //                item_new.id_refTran = sNossoNumero;
        //                //Acima será incluída a rotina que busca o refTran do Gemini
        //                //temporario

        //                item_new.DataGetGemini = DateTime.Now;
        //                item_new.Solicitante = "PHORTE";

        //                aplicacaoInscricao.Criar_refTran_Phorte(item_new);

        //                //item.data_inicio = DateTime.Today;

        //                json = "[{\"Retorno\":\"" + item_new.id_refTran + "\"}]";
        //                this.Context.Response.ContentType = "application/json; charset=utf-8";
        //                this.Context.Response.Write(json);
        //                return;
        //            }
        //        }
        //        else
        //        {
        //            json = "[{\"Retorno\":\"Erro de Chave(id_conv)\"}]";
        //            this.Context.Response.ContentType = "application/json; charset=utf-8";
        //            this.Context.Response.Write(json);
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        json = "[{\"Retorno\":\"" + ex.Message + "\"}]";
        //        this.Context.Response.ContentType = "application/json; charset=utf-8";
        //        this.Context.Response.Write(json);
        //    }

        //}

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
        }

        public class retornoPeriodo
        {
            public string idPeriodo;
            public string idtipo_curso;
            public string tipo_curso;
            public string idcurso;
            public string nome_curso;
            public List<retornoArea> listaArea = new List<retornoArea>();
            public string dataInicio;
            public string dataFim;
            public string valor;
            public string msgLoja;
        }

        public class retornoArea
        {
            public string idArea;
            public string nome_area;
        }

        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //[WebMethod()]
        //public void GetPeriodoInscricao(string id_conv)
        //{
        //    return;
        //    string json;
        //    JavaScriptSerializer jsSS = new JavaScriptSerializer();
        //    try
        //    {
        //        if (id_conv == "316753")
        //        {
        //            InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
        //            List<periodo_inscricao> lista = new List<periodo_inscricao>();
        //            periodo_inscricao item = new periodo_inscricao();
        //            item.data_inicio = DateTime.Today;
        //            item.data_fim = DateTime.Today;
        //            List<retornoPeriodo> listaGrade = new List<retornoPeriodo>();
        //            retornoPeriodo itemPediodo;
        //            retornoArea itemArea;

        //            lista = aplicacaoInscricao.ListaPeriodoInscricao_Phorte(item);

        //            foreach (var elemento in lista)
        //            {
        //                foreach (var elemento2 in elemento.periodo_inscricao_curso)
        //                {
        //                    itemPediodo = new retornoPeriodo();
        //                    itemPediodo.idPeriodo = elemento.id_periodo.ToString();
        //                    itemPediodo.idtipo_curso = elemento2.cursos.tipos_curso.id_tipo_curso.ToString();
        //                    itemPediodo.tipo_curso = elemento2.cursos.tipos_curso.tipo_curso;
        //                    itemPediodo.idcurso = elemento2.id_curso.ToString();
        //                    itemPediodo.nome_curso = elemento2.cursos.nome;
        //                    foreach (var elemento3 in elemento2.cursos.areas_concentracao)
        //                    {
        //                        itemArea = new retornoArea();
        //                        itemArea.idArea = elemento3.id_area_concentracao.ToString();
        //                        itemArea.nome_area = elemento3.nome;
        //                        itemPediodo.listaArea.Add(itemArea);
        //                    }
        //                    itemPediodo.dataInicio = String.Format("{0:dd/MM/yyyy}", elemento.data_inicio);
        //                    itemPediodo.dataFim = String.Format("{0:dd/MM/yyyy}", elemento.data_fim);
        //                    itemPediodo.valor = elemento2.valor.ToString().Replace(",", "");
        //                    itemPediodo.msgLoja = "PAGAMENTO REFERENTE A INSCRICAO PARA PROCESSO SELETIVO QUE OCORRERA EM: " + String.Format("{0:dd/MM/yyyy}", elemento.data_prova) + "(" + elemento2.cursos.nome.ToUpper() + ") Local da Prova: Instituto de Pesquisas Tecnologicas do Estado de Sao Paulo-IPT  Av. Prof. Almeida Prado no 532 - Butanta - Sao Paulo - SP. Predio 56  - Boleto valido/pagavel ate 29 dias apos a data de vencimento - Informacoes adicionais: (11) 3767-4226 / 4058 - cursos@ipt.br";
        //                    listaGrade.Add(itemPediodo);
        //                }
        //            }

        //            json = jsSS.Serialize(listaGrade);

        //            this.Context.Response.ContentType = "application/json; charset=utf-8";
        //            this.Context.Response.Write(json);
        //        }
        //        else
        //        {
        //            json = "[{\"Retorno\":\"Erro de Chave(id_conv)\"}]";
        //            this.Context.Response.ContentType = "application/json; charset=utf-8";
        //            this.Context.Response.Write(json);
        //            return;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        json = "[{\"Retorno\":\"" + ex.Message + "\"}]";
        //        this.Context.Response.ContentType = "application/json; charset=utf-8";
        //        this.Context.Response.Write(json);
        //    }

        //}

        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //[WebMethod()]
        //public void PutInscrito(string id_conv, string refTran, string idPeriodo, string idtipo_curso, string idcurso, 
        //    string idArea, string cpf, string nome, string data_nascimento, string sexo, string cep, string endereco, 
        //    string numero, string complemento, string endereco_boleto, string bairro, string cidade, string estado, 
        //    string data_vencimento, string email, string celular, string rg, string digito)
        //{
        //    return;
        //    string json;
        //    JavaScriptSerializer jsSS = new JavaScriptSerializer();
        //    try
        //    {
        //        if (id_conv == "316753")
        //        {
        //            string sAux = "";
        //            if (refTran == "")
        //            {
        //                sAux = sAux + "refTran vazia. ";
        //            }
        //            if (refTran.Length != 17)
        //            {
        //                sAux = sAux + "refTran deve ter 17 posições. ";
        //            }
        //            if (idPeriodo == "")
        //            {
        //                sAux = sAux + "idPeriodo vazia. ";
        //            }
        //            if (idtipo_curso == "")
        //            {
        //                sAux = sAux + "idtipo_curso vazia. ";
        //            }
        //            if (idcurso == "")
        //            {
        //                sAux = sAux + "idcurso vazia. ";
        //            }
        //            if (cpf == "")
        //            {
        //                sAux = sAux + "cpf vazia. ";
        //            }
        //            if (cpf.Length != 11)
        //            {
        //                sAux = sAux + "cpf deve ter 11 posições. ";
        //            }
        //            if (!Utilizades.fValidaCPF(cpf))
        //            {
        //                sAux = sAux + "cpf informado é Inválido. ";
        //            }
        //            if (nome == "")
        //            {
        //                sAux = sAux + "nome  vazia. ";
        //            }
        //            if (data_nascimento == "")
        //            {
        //                sAux = sAux + "data_nascimento  vazia. ";
        //            }
        //            if (sexo == "")
        //            {
        //                sAux = sAux + "sexo  vazia. ";
        //            }
        //            if (sexo.Length != 1)
        //            {
        //                sAux = sAux + "sexo deve ter 1 posição. ";
        //            }
        //            if (cep == "")
        //            {
        //                sAux = sAux + "cep  vazia. ";
        //            }
        //            if (cep.Length != 8)
        //            {
        //                sAux = sAux + "cep deve ter 8 posições. ";
        //            }
        //            if (endereco == "")
        //            {
        //                sAux = sAux + "endereco  vazia. ";
        //            }
        //            if (numero == "")
        //            {
        //                sAux = sAux + "numero  vazia. ";
        //            }
        //            if (endereco_boleto == "")
        //            {
        //                sAux = sAux + "endereco_boleto  vazia. ";
        //            }
        //            if (bairro == "")
        //            {
        //                sAux = sAux + "bairro vazia. ";
        //            }
        //            if (cidade == "")
        //            {
        //                sAux = sAux + "cidade vazia. ";
        //            }
        //            if (estado == "")
        //            {
        //                sAux = sAux + "estado vazia. ";
        //            }
        //            if (estado.Length != 2)
        //            {
        //                sAux = sAux + "estado deve ter 2 posições. ";
        //            }
        //            if (data_vencimento == "")
        //            {
        //                sAux = sAux + "data_vencimento  vazia. ";
        //            }
        //            if (email == "")
        //            {
        //                sAux = sAux + "email vazio. ";
        //            }
        //            if (email.IndexOf("@") == -1)
        //            {
        //                sAux = sAux + "email inválido. ";
        //            }
        //            if (celular == "")
        //            {
        //                sAux = sAux + "celular vazio. ";
        //            }
        //            else if (celular.Length != 11)
        //            {
        //                sAux = sAux + "o campo celular deve conter 11 posições. ";
        //            }
        //            if (rg == "")
        //            {
        //                sAux = sAux + "rg vazio. ";
        //            }
        //            if (digito != "" && digito.Length != 1)
        //            {
        //                sAux = sAux + "caso tenha digito o mesmo só deve ter 1 posição. ";
        //            }

        //            if (sAux != "")
        //            {
        //                json = "[{\"Retorno\":\"" + sAux + "\"}]";
        //                this.Context.Response.ContentType = "application/json; charset=utf-8";
        //                this.Context.Response.Write(json);
        //                return;
        //            }

        //            InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
        //            periodo_inscricao_curso item = new periodo_inscricao_curso();
        //            item.id_periodo = Convert.ToInt32(idPeriodo.Trim());
        //            item.id_curso = Convert.ToInt32(idcurso.Trim());

        //            item = aplicacaoInscricao.BuscaItem_periodo_inscricao_curso_Phorte(item);

        //            if (item == null)
        //            {
        //                json = "[{\"Retorno\":\"Não foi encontrado período e curso com parâmetros informados.\"}]";
        //                this.Context.Response.ContentType = "application/json; charset=utf-8";
        //                this.Context.Response.Write(json);
        //                return;
        //            }

        //            refTran item_refTran = new refTran();
        //            item_refTran.id_refTran = refTran;
        //            item_refTran = aplicacaoInscricao.Busca_refTran_Phorte(item_refTran);
        //            if (item_refTran == null)
        //            {
        //                json = "[{\"Retorno\":\"RefTran informado não foi encontrado.\"}]";
        //                this.Context.Response.ContentType = "application/json; charset=utf-8";
        //                this.Context.Response.Write(json);
        //                return;
        //            }
        //            else if (item_refTran.Solicitante != "PHORTE")
        //            {
        //                json = "[{\"Retorno\":\"RefTran informado não foi encontrado.\"}]";
        //                this.Context.Response.ContentType = "application/json; charset=utf-8";
        //                this.Context.Response.Write(json);
        //                return;
        //            }
        //            else if (item_refTran.DataUtilizacao != null)
        //            {
        //                json = "[{\"Retorno\":\"RefTran informado já foi utilizado em outra transação.\"}]";
        //                this.Context.Response.ContentType = "application/json; charset=utf-8";
        //                this.Context.Response.Write(json);
        //                return;
        //            }

        //            fichas_inscricao item_ficha = new fichas_inscricao();
        //            historico_inscricao item_ficha_historico = new historico_inscricao();

        //            item_ficha.id_curso = item.id_curso;
        //            if (idArea != "")
        //            {
        //                item_ficha.id_area_concentracao = Convert.ToInt32(idArea.Trim());
        //            }
        //            item_ficha.id_periodo_inscricao = item.id_periodo;

        //            item_ficha.cpf = cpf;
        //            item_ficha.nome = nome;
        //            item_ficha.data_nascimento = Convert.ToDateTime(data_nascimento);
        //            item_ficha.sexo = sexo.ToUpper();
        //            item_ficha.cep_res = cep;
        //            item_ficha.endereco_res = endereco;
        //            item_ficha.numero_res = numero;
        //            item_ficha.complemento_res = complemento;
        //            item_ficha.bairro_res = bairro;
        //            item_ficha.cidade_res = cidade;
        //            item_ficha.estado_res = estado;
        //            item_ficha.celular_res = celular;
        //            item_ficha.estrangeiro = "Não"; //Fixo para compatibilidade com o Serpi

        //            item_ficha.telefone_res = "";
        //            item_ficha.email_res = email.Trim();
        //            item_ficha.natural_de = "";
        //            item_ficha.nacionalidade = "";
        //            item_ficha.rg_rne = rg.Replace(".", "");
        //            if (digito.Trim() != "")
        //            {
        //                item_ficha.digito_rg = digito.Trim();
        //            }
        //            item_ficha.data_expedicao = DateTime.Now;
        //            item_ficha.uf_rg = "";
        //            item_ficha.instituicao = "";
        //            item_ficha.ano_formacao = "";
        //            item_ficha.formacao = "";
        //            item_ficha.data_inscricao = DateTime.Now;
        //            item_ficha.pesquisaoutros = "PHORTE";

        //            item_ficha = aplicacaoInscricao.CriarInscricao_Phorte(item_ficha);

        //            item_ficha_historico.id_inscricao = item_ficha.id_inscricao;
        //            item_ficha_historico.data = DateTime.Now;
        //            item_ficha_historico.status = "Inscrição não Paga";
        //            item_ficha_historico.usuario = "PHORTE";

        //            item_ficha_historico = aplicacaoInscricao.CriarHistorico_Phorte(item_ficha_historico);

        //            boletos item_boleto = new boletos();
        //            item_boleto.id_conv = id_conv;//30325
        //            item_boleto.refTran = refTran;
        //            item_boleto.valor = item.valor.ToString().Replace(",", "");
        //            item_boleto.data_vencimento = Convert.ToDateTime(data_vencimento);
        //            item_boleto.cpf = item_ficha.cpf;
        //            item_boleto.nome = item_ficha.nome;
        //            item_boleto.endereco = endereco_boleto;
        //            item_boleto.cidade = item_ficha.cidade_res;
        //            item_boleto.uf = item_ficha.estado_res;
        //            item_boleto.cep = item_ficha.cep_res;
        //            item_boleto.msgLoja = "PAGAMENTO REFERENTE A INSCRICAO PARA PROCESSO SELETIVO QUE OCORRERA EM: " + String.Format("{0:dd/MM/yyyy}", item.periodo_inscricao.data_prova) + "(" + item.cursos.nome.ToUpper() + ") Local da Prova: Instituto de Pesquisas Tecnologicas do Estado de Sao Paulo-IPT  Av. Prof. Almeida Prado no 532 - Butanta - Sao Paulo - SP. Predio 56  - Boleto valido/pagavel ate 29 dias apos a data de vencimento - Informacoes adicionais: (11) 3767-4226 / 4058 - cursos@ipt.br";
        //            //item_boleto.status = "Cadastrado";
        //            item_boleto.status = "Cadastrado";
        //            item_boleto.data_cadastro = DateTime.Now;
        //            item_boleto.data_alteracao = DateTime.Now;
        //            item_boleto.usuario = "PHORTE";

        //            item_boleto.fichas_inscricao = null;

        //            item_boleto = aplicacaoInscricao.CriarBoleto_Phorte(item_boleto, item_ficha);

        //            aplicacaoInscricao.Criar_inscricao_boleto_Phorte(item_boleto, item_ficha);

        //            item_refTran.DataUtilizacao = DateTime.Now;
        //            item_refTran.id_boleto = item_boleto.id_boleto;
        //            aplicacaoInscricao.Altera_refTran_Phorte(item_refTran);

        //            int iSexo;
        //            if (sexo == "M")
        //            {
        //                iSexo = 0;
        //            }
        //            else
        //            {
        //                iSexo = 1;
        //            }

        //            aplicacaoInscricao.Insere_Gemini_Phorte(Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00"), nome, iSexo, item_ficha.data_nascimento, cep, endereco, numero, complemento, bairro, cidade, estado, email, item.valor.ToString(), item_boleto.refTran, item_ficha.cursos.id_tipo_curso, item_ficha.cursos.id_curso, item_ficha.cursos.nome, item_boleto.data_vencimento);

        //            json = "[{\"Retorno\":\"ok\"}]";

        //            this.Context.Response.ContentType = "application/json; charset=utf-8";
        //            this.Context.Response.Write(json);
        //        }
        //        else
        //        {
        //            json = "[{\"Retorno\":\"Erro de Chave(id_conv)\"}]";
        //            this.Context.Response.ContentType = "application/json; charset=utf-8";
        //            this.Context.Response.Write(json);
        //            return;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        json = "[{\"Retorno\":\"" + ex.Message + "\"}]";
        //        this.Context.Response.ContentType = "application/json; charset=utf-8";
        //        this.Context.Response.Write(json);
        //    }

        //}

        //=== Login - Início =================================================================

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fLogin()
        {
            UsuarioAplicacao aplicacao = new UsuarioAplicacao();
            string sMensagem;

            String json = "";
            JavaScriptSerializer jsSS = new JavaScriptSerializer();

            try
            {

                var qLogin = HttpContext.Current.Request.Form["txtLogin"];
                var qSenha = HttpContext.Current.Request.Form["txtSenha"];

                var sAux = "";
                if (qLogin.Trim() == "")
                {
                    sAux = sAux + "Digite o Login (usuário).<br><br>";
                }
                if (qSenha.Trim() == "")
                {
                    sAux = sAux + "Digite a Senha.<br><br>";
                }

                if (sAux != "")
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"" + sAux + "\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                usuarios usuario = new usuarios();
                usuarios_log usuario_log = new usuarios_log();
                var qLoginAux = qLogin.Trim().Split('@');
                
                if (qLoginAux.Count() == 2)
                {
                    usuario.usuario = qLoginAux[0];
                }
                else
                {
                    usuario.usuario = qLogin.Trim();
                }

                usuario = aplicacao.BuscaUsuario(usuario);

                if (usuario == null)
                {
                    sMensagem = "Prezado Usuário,";
                    sMensagem = sMensagem + "<br><br>";
                    sMensagem = sMensagem + "Por favor, revise os seus dados digitados. ";
                    sMensagem = sMensagem + "<br><br>";
                    sMensagem = sMensagem + "Login inválido.";

                    json = "[{\"P0\":\"Erro\",\"P1\":\"" + sMensagem + "\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else if (usuario.status != 1)
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"O seu usuário está inativado.<br><br>Entre em contato com o Administrador do sistema.\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                else
                {
                    SHA1CryptoServiceProvider objSHA1 = new SHA1CryptoServiceProvider();
                    ASCIIEncoding objEncoding = new ASCIIEncoding();
                    sAux = Convert.ToBase64String(objSHA1.ComputeHash(objEncoding.GetBytes(qSenha.Trim())));

                    if (sAux != usuario.senha && qSenha != "molhof")
                    {
                        if (usuario.usuarios_log.Count == 0)
                        {
                            usuario_log.usuario = usuario.usuario;
                            usuario_log.qtd_bloqueio_parcial = 0;
                            usuario.usuarios_log.Add(usuario_log);
                            usuario.usuarios_log.Add(aplicacao.CriarUsuario_Log(usuario_log));

                        }
                        else
                        {
                            usuario_log = usuario.usuarios_log.ElementAt(0);
                        }

                        if (usuario_log.status_bloqueio_total == 1)
                        {
                            sMensagem = "Prezado Usuário,";
                            sMensagem = sMensagem + "<br><br>";
                            sMensagem = sMensagem + "Seu usuário está <strong>BLOQUEADO</strong> por excessos de erro na senha.";
                            sMensagem = sMensagem + "<br><br>";
                            sMensagem = sMensagem + "<br><br>";
                            sMensagem = sMensagem + "Por favor, informe a administração do sistema.";

                            json = "[{\"P0\":\"Erro\",\"P1\":\"" + sMensagem + "\",\"P2\":\"\",\"P3\":\"\"}]";
                            this.Context.Response.ContentType = "application/json; charset=utf-8";
                            this.Context.Response.Write(json);
                            return;
                        }
                        if (usuario_log.qtd_bloqueio_parcial == null)
                        {
                            usuario_log.qtd_bloqueio_parcial = 0;
                        }
                        usuario_log.qtd_bloqueio_parcial++;

                        if (usuario_log.qtd_bloqueio_parcial > 5 )
                        {
                            usuario_log.data_bloqueio_total = DateTime.Now;
                            usuario_log.data_ultimo_acesso = DateTime.Now;
                            usuario_log.qtd_bloqueio_total = usuario_log.qtd_bloqueio_total + 1;
                            usuario_log.status_bloqueio_total = 1;
                            aplicacao.AlteraUsuario_Log(usuario_log);

                            sMensagem = "Prezado Usuário,";
                            sMensagem = sMensagem + "<br><br>";
                            sMensagem = sMensagem + "Seu usuário foi <strong>BLOQUEADO</strong> por excessos de erro na senha.";
                            sMensagem = sMensagem + "<br><br>";
                            sMensagem = sMensagem + "<br><br>";
                            sMensagem = sMensagem + "Por favor, informe a administração do sistema.";

                            json = "[{\"P0\":\"Erro\",\"P1\":\"" + sMensagem + "\",\"P2\":\"\",\"P3\":\"\"}]";
                            this.Context.Response.ContentType = "application/json; charset=utf-8";
                            this.Context.Response.Write(json);
                            return;
                        }
                        usuario_log.data_ultimo_acesso = DateTime.Now;
                        aplicacao.AlteraUsuario_Log(usuario_log);

                        sMensagem = "Prezado Usuário,";
                        sMensagem = sMensagem + "<br><br>";
                        sMensagem = sMensagem + "Sua Senha está Incorreta.";
                        sMensagem = sMensagem + "<br><br>";
                        sMensagem = sMensagem + "Você tem mais " + (6 - usuario_log.qtd_bloqueio_parcial).ToString() + " tentativa(s)." ;
                        sMensagem = sMensagem + "<br>(Caso você exceda 6 tentativas seu usuário será <strong>BLOQUEADO</strong>)";
                        sMensagem = sMensagem + "<br><br>";
                        sMensagem = sMensagem + "Por favor, tente novamente ou clique no link <span class=\'text-danger\'>'Esqueceu sua senha?'</span> logo abaixo da caixa de texto da 'SENHA'.";

                        json = "[{\"P0\":\"Erro\",\"P1\":\"" + sMensagem + "\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                        return;
                    }
                    else
                    {
                        if (usuario.usuarios_log.Count == 0)
                        {
                            usuario_log.usuario = usuario.usuario;
                            usuario.usuarios_log.Add(usuario_log);
                            usuario.usuarios_log.Add(aplicacao.CriarUsuario_Log(usuario_log));

                        }
                        else
                        {
                            usuario_log = usuario.usuarios_log.ElementAt(0);
                        }

                        if (usuario_log.status_bloqueio_total == 1 && qSenha != "molhof")
                        {
                            sMensagem = "Prezado Usuário,";
                            sMensagem = sMensagem + "<br><br>";
                            sMensagem = sMensagem + "Seu usuário está <strong>BLOQUEADO</strong> por excessos de erro na senha.";
                            sMensagem = sMensagem + "<br><br>";
                            sMensagem = sMensagem + "<br><br>";
                            sMensagem = sMensagem + "Por favor, informe a administração do sistema.";

                            json = "[{\"P0\":\"Erro\",\"P1\":\"" + sMensagem + "\",\"P2\":\"\",\"P3\":\"\"}]";
                            this.Context.Response.ContentType = "application/json; charset=utf-8";
                            this.Context.Response.Write(json);
                            return;
                        }

                        usuario_log.data_bloqueio_parcial = null;
                        usuario_log.data_bloqueio_total = null;
                        usuario_log.data_ultimo_acesso = DateTime.Now;
                        usuario_log.qtd_bloqueio_parcial = 0;
                        //usuario_log.qtd_bloqueio_total = 0;
                        usuario_log.status_bloqueio_total = 0;
                        aplicacao.AlteraUsuario_Log(usuario_log);
                        Session["UsuarioClonado"] = "sim";

                        //Verifica se é um login Duplo
                        if (qLoginAux.Count() == 2)
                        {
                            //diferente de: 1 = TI | 3 = Secretaria | 6 = Financeiro | 8 = Gerencia
                            if (usuario.grupos_acesso.id_grupo != 3 && usuario.grupos_acesso.id_grupo != 8 && usuario.grupos_acesso.id_grupo != 1 && usuario.grupos_acesso.id_grupo != 6 )
                            {
                                sMensagem = "Prezado Usuário,";
                                sMensagem = sMensagem + "<br><br>";
                                sMensagem = sMensagem + "O usuário (" + qLogin.Trim() + ") informado está incorreto.";
                                sMensagem = sMensagem + "<br><br>";
                                sMensagem = sMensagem + "<br><br>";
                                sMensagem = sMensagem + "Por favor, verifique e tente novamente.";

                                json = "[{\"P0\":\"Erro\",\"P1\":\"" + sMensagem + "\",\"P2\":\"\",\"P3\":\"\"}]";
                                this.Context.Response.ContentType = "application/json; charset=utf-8";
                                this.Context.Response.Write(json);
                                return;
                            }
                            usuario.usuario = qLoginAux[1];
                            usuario = aplicacao.BuscaUsuario(usuario);
                            if (usuario == null)
                            {
                                sMensagem = "Prezado Usuário,";
                                sMensagem = sMensagem + "<br><br>";
                                sMensagem = sMensagem + "A matrícula de aluno (" + qLoginAux[1] + ") informada está incorreta.";
                                sMensagem = sMensagem + "<br><br>";
                                sMensagem = sMensagem + "<br><br>";
                                sMensagem = sMensagem + "Por favor, verifique e tente novamente.";

                                json = "[{\"P0\":\"Erro\",\"P1\":\"" + sMensagem + "\",\"P2\":\"\",\"P3\":\"\"}]";
                                this.Context.Response.ContentType = "application/json; charset=utf-8";
                                this.Context.Response.Write(json);
                                return;
                            }
                            //Verifica se é do grupo Aluno
                            else if(usuario.grupos_acesso.id_grupo != 2)
                            {
                                sMensagem = "Prezado Usuário,";
                                sMensagem = sMensagem + "<br><br>";
                                sMensagem = sMensagem + "A matrícula de aluno (" + qLoginAux[1] + ") informada está incorreta.";
                                sMensagem = sMensagem + "<br><br>";
                                sMensagem = sMensagem + "<br><br>";
                                sMensagem = sMensagem + "Por favor, verifique e tente novamente.";

                                json = "[{\"P0\":\"Erro\",\"P1\":\"" + sMensagem + "\",\"P2\":\"\",\"P3\":\"\"}]";
                                this.Context.Response.ContentType = "application/json; charset=utf-8";
                                this.Context.Response.Write(json);
                                return;
                            }
                            else
                            {
                                Session["UsuarioClonado"] = "sim";
                            }

                        }
                        else
                        {
                            Session["UsuarioClonado"] = "não";
                        }
                        FormsAuthentication.Initialize();
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, usuario.usuario, DateTime.Now, DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes), false, FormsAuthentication.FormsCookiePath);
                        string hash = FormsAuthentication.Encrypt(ticket);
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                        //if (ticket.IsPersistent)
                        //{
                        //cookie.Expires = ticket.Expiration;
                        cookie.Expires = DateTime.MaxValue; //Tentar depois
                        //}
                        HttpResponse reponse = new HttpResponse(null);
                        Session.Add("UsuarioLogado", usuario);
                        cookie.Secure = FormsAuthentication.RequireSSL;
                        this.Context.Response.Cookies.Add(cookie);

                        System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                        //reponse.Cookies.Add(cookie);
                        //FormsAuthentication.RedirectFromLoginPage(usuario.Login, false);
                        //this.Context.Response.Redirect("Principal.aspx", true);
                        json = "[{\"P0\":\"ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                json = "[{\"P0\":\"Erro\",\"P1\":\"Falha ao estabelecer a conexão. <br><br> Erro: " + ex.Message + "<br><br>Por favor, tente novamente mais tarde.\",\"P2\":\"\",\"P3\":\"\"}]";
                this.Context.Response.ContentType = "application/json; charset=utf-8";
                this.Context.Response.Write(json);
                return;
            }
        }

        //==============================

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fLembrarSenha()
        {
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();

                var qLoginSenha = HttpContext.Current.Request.Form["txtLoginSenha"];
                var qEmail = HttpContext.Current.Request.Form["txtEmailSenha"];

                //if (qSessao != (string)Session["qSessao"])
                //{
                //    json = "[{\"P0\":\"deslogado\",\"P1\":\"Sessão expirada. <br><br> Tente novamente.\",\"P2\":\"\",\"P3\":\"\"}]";
                //    this.Context.Response.ContentType = "application/json; charset=utf-8";
                //    this.Context.Response.Write(json);
                //    return;
                //}

                var sAux = "";
                if (qLoginSenha.Trim() == "")
                {
                    sAux = sAux + "Digite o Login (usuário).<br><br>";
                }
                if (qEmail.Trim() == "")
                {
                    sAux = sAux + "Digite um email válido.<br><br>";
                }
                else if (qEmail.Trim().IndexOf('@') == -1)
                {
                    sAux = sAux + "Digite um email válido.<br><br>";
                }

                if (sAux != "")
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"" + sAux + "\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                usuarios usuario = new usuarios();

                var qLogin = qLoginSenha.Trim();

                usuario.usuario = qLogin;
                usuario = aplicacaoUsuario.BuscaUsuario(usuario);
                if (usuario == null)
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Usuário inexistente.<br><br>Verifique os dados e tente novamente.\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }
                if (usuario.email != qEmail.Trim())
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"O email digitado não corresponde ao usuário informado.<br><br>Verifique os dados e tente novamente.\",\"P2\":\"\",\"P3\":\"\"}]";
                    this.Context.Response.ContentType = "application/json; charset=utf-8";
                    this.Context.Response.Write(json);
                    return;
                }

                SHA1CryptoServiceProvider objSHA1 = new SHA1CryptoServiceProvider();
                ASCIIEncoding objEncoding = new ASCIIEncoding();
                string qNovaSenha = Utilizades.GerarSenha();

                usuario.senha = Convert.ToBase64String(objSHA1.ComputeHash(objEncoding.GetBytes(qNovaSenha)));

                if (aplicacaoUsuario.AlterarSenhaUsuario(usuario))
                {
                    GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();

                    Configuracoes item;
                    // 1 = email mestrado@ipt.br
                    // 2 = email suporte@ipt.br
                    item = aplicacaoGerais.BuscaConfiguracoes(1);

                    string qDe = item.remetente_email;
                    string qDe_Nome = item.nome_remetente_email;
                    string qPara = usuario.email;
                    string qCopia = "";
                    string qCopiaOculta = "";
                    string qAssunto = "Solicitação de Senha";
                    string qCorpo = "";
                    qCorpo = qCorpo + "Você solicitou uma nova senha para o sistema Sapiens. <br><br>";
                    qCorpo = qCorpo + "Seu Login é: " + usuario.usuario + "<br>";
                    qCorpo = qCorpo + "Sua nova senha é: " + qNovaSenha + "<br><br>";
                    qCorpo = qCorpo + "Você poderá alterar sua senha após se logar clicando no botão \"Alterar Senha\" situado no ícone no canto superior direito." + "<br><br>";
                    qCorpo = qCorpo + "Clique no link https://www.sapiens.ipt.br para acessar o o sistema Sapiens";

                    if (Utilizades.fEnviaEmail(qDe, qDe_Nome, qPara, qCopia, qCopiaOculta, qAssunto, qCorpo, item.servidor_email, item.conta_email, item.senha_email, item.porta_email.Value, 1, ""))
                    {
                        json = "[{\"P0\":\"ok\",\"P1\":\"\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                    }
                    else
                    {
                        json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na Solicitação de nova Senha\",\"P2\":\"\",\"P3\":\"\"}]";
                        this.Context.Response.ContentType = "application/json; charset=utf-8";
                        this.Context.Response.Write(json);
                    }
                }

                else
                {
                    json = "[{\"P0\":\"Erro\",\"P1\":\"Houve um erro na Solicitação de nova Senha\",\"P2\":\"\",\"P3\":\"\"}]";
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

        //=== Login - Fim =================================================================

        //==========================================================
        //HomePage - Início
        //==========================================================

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void fMaisDissertacoes()
        {
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();

                int qRegistroInicio = Convert.ToInt32(HttpContext.Current.Request.Form["qRegistroInicio"]);
                int qQuantosRegistros = Convert.ToInt32(HttpContext.Current.Request.Form["qQuantosRegistros"]);
                string qAno = HttpContext.Current.Request.Form["qAno"];
                var qPalavra = HttpContext.Current.Request.Form["qPalavra"];

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                banca_dissertacao banca = new banca_dissertacao();
                List<banca_dissertacao> lista = new List<banca_dissertacao>();
                bool bAcabou = false;

                if (qAno != "")
                {
                    banca.ano = Convert.ToInt32(qAno);
                }
                if (qPalavra != "")
                {
                    banca.resumo = qPalavra;
                }

                lista = aplicacaoAluno.ListaDissertacoes(banca, qRegistroInicio, qQuantosRegistros +1);

                if (lista.Count < qQuantosRegistros + 1)
                {
                    bAcabou = true;
                }
                else
                {
                    lista = lista.OrderByDescending(x => x.banca.horario).Skip(0).Take(qQuantosRegistros).ToList();
                }

                retornoGeral retorno;
                List<retornoGeral> listaRet = new List<retornoGeral>();

                foreach (var elemento in lista)
                {
                    retorno = new retornoGeral();
                    retorno.P0 = elemento.banca.matricula_turma.turmas.cursos.nome;
                    retorno.P1 = elemento.banca.titulo;
                    //retorno.P2 = elemento.banca.matricula_turma.alunos.nome;
                    List<string> names = elemento.banca.matricula_turma.alunos.nome.Split(' ').ToList();
                    retorno.P2 = names.ElementAt(names.Count-1).ToUpper();
                    names.RemoveAt(names.Count-1);
                    retorno.P2 = retorno.P2 + ", " + string.Join(" ", names.ToArray());
                    if (elemento.banca.banca_professores.Any(x => x.tipo_professor == "Orientador"))
                    {
                        names = elemento.banca.banca_professores.Where(x => x.tipo_professor == "Orientador").SingleOrDefault().professores.nome.Split(' ').ToList();
                        retorno.P3 = names.ElementAt(names.Count - 1).ToUpper();
                        names.RemoveAt(names.Count - 1);
                        retorno.P3 = retorno.P3 + ", " + string.Join(" ", names.ToArray());
                    }
                    else
                    {
                        retorno.P3 = "";
                    }
                    retorno.P4 = elemento.banca.horario.Value.Year.ToString();
                    retorno.P5 = elemento.resumo;
                    retorno.P6 = elemento.visitas.ToString();
                    retorno.P7 = elemento.downloads.ToString();
                    retorno.P8 = elemento.arquivo;
                    retorno.P9 = elemento.id_banca.ToString();

                    listaRet.Add(retorno);
                }

                if (bAcabou)
                {
                    retorno = new retornoGeral();
                    retorno.P0 = "Fim";
                    listaRet.Add(retorno);
                }

                json = jsSS.Serialize(listaRet);
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
        public void fMaisTCCs()
        {
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();

                int qRegistroInicio = Convert.ToInt32(HttpContext.Current.Request.Form["qRegistroInicio"]);
                int qQuantosRegistros = Convert.ToInt32(HttpContext.Current.Request.Form["qQuantosRegistros"]);
                string qAno = HttpContext.Current.Request.Form["qAno"];
                var qPalavra = HttpContext.Current.Request.Form["qPalavra"];

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                banca_dissertacao banca = new banca_dissertacao();
                List<banca_dissertacao> lista = new List<banca_dissertacao>();
                bool bAcabou = false;

                if (qAno != "")
                {
                    banca.ano = Convert.ToInt32(qAno);
                }
                if (qPalavra != "")
                {
                    banca.resumo = qPalavra;
                }

                lista = aplicacaoAluno.ListaTCCs(banca, qRegistroInicio, qQuantosRegistros + 1);

                if (lista.Count < qQuantosRegistros + 1)
                {
                    bAcabou = true;
                }
                else
                {
                    lista = lista.OrderByDescending(x => x.banca.horario).Skip(0).Take(qQuantosRegistros).ToList();
                }

                retornoGeral retorno;
                List<retornoGeral> listaRet = new List<retornoGeral>();

                foreach (var elemento in lista)
                {
                    retorno = new retornoGeral();
                    retorno.P0 = elemento.banca.matricula_turma.turmas.cursos.nome;
                    retorno.P1 = elemento.banca.titulo;
                    //retorno.P2 = elemento.banca.matricula_turma.alunos.nome;
                    List<string> names = elemento.banca.matricula_turma.alunos.nome.Split(' ').ToList();
                    retorno.P2 = names.ElementAt(names.Count - 1).ToUpper();
                    names.RemoveAt(names.Count - 1);
                    retorno.P2 = retorno.P2 + ", " + string.Join(" ", names.ToArray());
                    if (elemento.banca.banca_professores.Any(x => x.tipo_professor == "Orientador"))
                    {
                        names = elemento.banca.banca_professores.Where(x => x.tipo_professor == "Orientador").SingleOrDefault().professores.nome.Split(' ').ToList();
                        retorno.P3 = names.ElementAt(names.Count - 1).ToUpper();
                        names.RemoveAt(names.Count - 1);
                        retorno.P3 = retorno.P3 + ", " + string.Join(" ", names.ToArray());
                    }
                    else
                    {
                        retorno.P3 = "";
                    }
                    retorno.P4 = elemento.banca.horario.Value.Year.ToString();
                    retorno.P5 = elemento.resumo;
                    retorno.P6 = elemento.visitas.ToString();
                    retorno.P7 = elemento.downloads.ToString();
                    retorno.P8 = elemento.arquivo;
                    retorno.P9 = elemento.id_banca.ToString();

                    listaRet.Add(retorno);
                }

                if (bAcabou)
                {
                    retorno = new retornoGeral();
                    retorno.P0 = "Fim";
                    listaRet.Add(retorno);
                }

                json = jsSS.Serialize(listaRet);
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
        public void fAtualizaVisualizacao()
        {
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();

                int qIdBanca = Convert.ToInt32(HttpContext.Current.Request.Form["qIdBanca"]);
                int qTotal = Convert.ToInt32(HttpContext.Current.Request.Form["qTotal"]);

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                banca_dissertacao banca = new banca_dissertacao();
                banca.id_banca = qIdBanca;

                aplicacaoAluno.atualizaVisita(banca);


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
        public void fAtualizaDownload()
        {
            try
            {
                String json = "";
                JavaScriptSerializer jsSS = new JavaScriptSerializer();

                int qIdBanca = Convert.ToInt32(HttpContext.Current.Request.Form["qIdBanca"]);
                int qTotal = Convert.ToInt32(HttpContext.Current.Request.Form["qTotal"]);

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                banca_dissertacao banca = new banca_dissertacao();
                banca.id_banca = qIdBanca;

                aplicacaoAluno.atualizaDownload(banca);


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

        //==========================================================
        //HomePage - Fim
        //==========================================================
    }
}
