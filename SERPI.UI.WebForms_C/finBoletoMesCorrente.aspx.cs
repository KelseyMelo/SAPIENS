using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Configuration;
using System.IO;

namespace SERPI.UI.WebForms_C
{
    public partial class finBoletoMesCorrente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null)
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 79)) //Emissão de Boletos Mês Corrente
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                txtAnoBoleto.Value = DateTime.Today.Year.ToString();
                ddlMesBoleto.SelectedValue = DateTime.Today.Month.ToString();

                if (Session["arrayFiltroBoletoMesCorrente"] != null)
                {
                    CarregarDados();
                }
            }
            else
            {
                if (grdBoletosMesCorrente.Rows.Count != 0)
                {

                    grdBoletosMesCorrente.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }

        }

        protected void grdBoletosMesCorrente_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            grdBoletosMesCorrente.PageIndex = e.NewPageIndex;
            grdBoletosMesCorrente.SelectedIndex = -1;
        }

        private void CarregarDados()
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            int pMatricula = 0;
            string pNome = "";
            int pMes = 0;
            int pAno = 0;

            string[] arrayFiltroBoletoMesCorrente = new string[4];

            arrayFiltroBoletoMesCorrente = (string[])Session["arrayFiltroBoletoMesCorrente"];

            if (arrayFiltroBoletoMesCorrente[0] != "" && arrayFiltroBoletoMesCorrente[0] != null)
            {
                pMatricula = System.Convert.ToInt32(arrayFiltroBoletoMesCorrente[0]);
                txtMatriculaAluno.Value = arrayFiltroBoletoMesCorrente[0];
            }

            if (arrayFiltroBoletoMesCorrente[1] != "" && arrayFiltroBoletoMesCorrente[1] != null)
            {
                pNome = arrayFiltroBoletoMesCorrente[1];
                txtNomeAluno.Value = arrayFiltroBoletoMesCorrente[1];
            }

            if (arrayFiltroBoletoMesCorrente[2] != "" && arrayFiltroBoletoMesCorrente[2] != null)
            {
                pMes = Convert.ToInt32(arrayFiltroBoletoMesCorrente[2]);
                ddlMesBoleto.SelectedValue = arrayFiltroBoletoMesCorrente[2];
            }

            if (arrayFiltroBoletoMesCorrente[3] != "" && arrayFiltroBoletoMesCorrente[3] != null)
            {
                pAno = Convert.ToInt32(arrayFiltroBoletoMesCorrente[3]);
                txtAnoBoleto.Value = arrayFiltroBoletoMesCorrente[3];
            }

            //Session["arrayFiltroBoletoMesCorrente"] = arrayFiltroBoletoMesCorrente;
            FIPTAplicacao aplicacaoFIPT = new FIPTAplicacao();
            List<boleto_email> listaAluno = new List<boleto_email>();

            listaAluno = aplicacaoFIPT.ListaBoletosMesFipt(pMatricula, pNome, pMes, pAno);

            grdBoletosMesCorrente.DataSource = listaAluno;
            grdBoletosMesCorrente.DataBind();

            if (listaAluno.Count > 0)
            {
                grdBoletosMesCorrente.UseAccessibleHeader = true;
                grdBoletosMesCorrente.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultados.Visible = false;
                grdBoletosMesCorrente.Visible = true;
                btnLocalizaEmailsLote.Visible = true;
            }
            else
            {
                msgSemResultados.Visible = true;
                btnLocalizaEmailsLote.Visible = false;
            }
            divResultados.Visible = true;
        }

        protected void btnPesquisaBoletoMesCorrente_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroBoletoMesCorrente = new string[4];

            if (txtMatriculaAluno.Value.Trim() != "")
            {
                arrayFiltroBoletoMesCorrente[0] = txtMatriculaAluno.Value.Trim();
            }

            if (txtNomeAluno.Value.Trim() != "")
            {
                arrayFiltroBoletoMesCorrente[1] = txtNomeAluno.Value.Trim();
            }

            if (ddlMesBoleto.SelectedValue.Trim() != "")
            {
                arrayFiltroBoletoMesCorrente[2] = ddlMesBoleto.SelectedValue;
            }

            if (txtAnoBoleto.Value.Trim() != "")
            {
                arrayFiltroBoletoMesCorrente[3] = txtAnoBoleto.Value.Trim();
            }

            Session["arrayFiltroBoletoMesCorrente"] = arrayFiltroBoletoMesCorrente;

            CarregarDados();

        }

        protected void btnEnviarEmailUnitario_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            string sCorpo = txtCorpoEmail.Value.Trim().Replace("img/", "https://sapiens.ipt.br/img/");

            if (EnviaEmail(txtParaEmail.Value.Trim(), txtCcEmail.Value.Trim(), txtAssuntoEmail.Value.Trim(), sCorpo))
            {
                FIPTAplicacao aplicacaoFIPT = new FIPTAplicacao();
                boleto_email item = new boleto_email();
                item.id_boleto_email = Convert.ToInt32(txtIdAluno_FIPT.Value.Trim());
                item.data_envio = DateTime.Now;
                item.usuario = usuario.usuario;

                aplicacaoFIPT.AlteraBoletosMesFipt_dataEnvio(item);

                btnPesquisaBoletoMesCorrente_Click(null, null);
            }
            else
            {
                lblMensagem.Text = "Houve um erro no envio do e-mail para o endereço: <strong>" + txtParaEmail.Value.Trim() + "</strong><br><br>Por favor, tente novamente.";
                lblTituloMensagem.Text = "Envio de E-mail";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
            }

        }

        protected void btnEnviarEmailLote_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            FIPTAplicacao aplicacaoFIPT = new FIPTAplicacao();
            boleto_email item_boleto_email = new boleto_email();
            string sAlunosNaoEnviados = "";

            string qEmailsLote = HttpContext.Current.Request.Form["hCodigoAluno"];

            foreach (var elemento in qEmailsLote.Split(';'))
            {
                if (elemento == "")
                {
                    continue;
                }
                item_boleto_email.id_boleto_email = Convert.ToInt32(elemento);
                item_boleto_email = aplicacaoFIPT.BuscaItem(item_boleto_email);

                if (EnviaEmail(item_boleto_email.email.Trim(),
                    "financeiroensino@ipt.br",
                    "SAPIENS/IPT - Mensalidade ref.: " + item_boleto_email.data_mes_ano.Value.Month + "/" + item_boleto_email.data_mes_ano.Value.Year,
                    PreparaCorpoEmail(item_boleto_email.id_boleto_email)))
                {
                    item_boleto_email.data_envio = DateTime.Now;
                    item_boleto_email.usuario = usuario.usuario;

                    aplicacaoFIPT.AlteraBoletosMesFipt_dataEnvio(item_boleto_email);

                }
                else
                {
                    if (sAlunosNaoEnviados != "")
                    {
                        sAlunosNaoEnviados = sAlunosNaoEnviados + ";<br> ";
                    }

                    sAlunosNaoEnviados = sAlunosNaoEnviados + item_boleto_email.nome;
                }
            }

            if (sAlunosNaoEnviados != "")
            {
                lblMensagem.Text = "Houve um erro no envio do e-mail para o(s) Aluno(s):<br> <strong>" + sAlunosNaoEnviados + "</strong>.<br><br>Por favor, tente novamente.";
                lblTituloMensagem.Text = "Envio de E-mail";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
            }

            btnPesquisaBoletoMesCorrente_Click(null, null);
        }

        private string PreparaCorpoEmail(int qId_aluno_Curso)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            FIPTAplicacao aplicacaoFIPT = new FIPTAplicacao();
            boleto_email item = new boleto_email();
            item.id_boleto_email = qId_aluno_Curso;
            item = aplicacaoFIPT.BuscaItem(item);

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

            sCorpo = sCorpo.Replace("img/", "https://sapiens.ipt.br/img/");

            return sCorpo;
        }


        private bool EnviaEmail(string pTo, string pCc, string pAssunto, string pCorpo)
        {
            GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
            Configuracoes item_configuracoes;
            // 1 = email mestrado@ipt.br
            // 2 = email suporte@ipt.br
            // 3 = email financeiroensino@ipt.br
            item_configuracoes = aplicacaoGerais.BuscaConfiguracoes(3);

            string sFrom = item_configuracoes.remetente_email;
            string sFrom_Nome = item_configuracoes.nome_remetente_email;
            string sTo;
            string sAssunto = pAssunto;
            string sCopia = "";
            string sAux = "";
            if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString == "Producao")
            {
                sTo = pTo;
                sCopia = pCc;
            }
            else
            {
                sTo = "kelsey@ipt.br"; // usuario.email;
                sAux = "<br> <strong>Esse email seria enviado para:</strong>" + pTo + "<br> <strong>E com cópia para para:</strong>" + pCc;
            }

            sAux = pCorpo + sAux;

            return Utilizades.fEnviaEmail(sFrom, sFrom_Nome, sTo, sCopia, "", sAssunto, sAux, item_configuracoes.servidor_email, item_configuracoes.conta_email, item_configuracoes.senha_email, item_configuracoes.porta_email.Value, 1, "");

            //(this.Master as SERPI).PreencheSininho();

        }
        public string setParcelasAtrazadas(object objeto)
        {
            HashSet<alunos_parcelas_inadimplente> lista = (HashSet<alunos_parcelas_inadimplente>)objeto;
            string sAux;

            sAux = lista.Count().ToString();

            return sAux;
        }


        public string setValorTotal(object objeto)
        {
            HashSet<alunos_parcelas_inadimplente> lista = (HashSet<alunos_parcelas_inadimplente>)objeto;
            string sAux;
            sAux = string.Format("{0:C}", lista.Sum(x => x.valor_corrigido));

            return sAux;
        }

        public string setDataEmailsEnviados(object objeto, int pIDCURSO)
        {
            alunos_inadimpentes_fipt item = (alunos_inadimpentes_fipt)objeto;

            string sAux = " - ";
            if (item.alunos != null)
            {
                foreach (var elemento in item.alunos.alunos_inadimplentes_emails_enviados.Where(x => x.IDCurso == pIDCURSO).OrderBy(x => x.data_envio))
                {
                    if (sAux == " - ")
                    {
                        sAux = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(elemento.data_envio));
                    }
                    else
                    {
                        sAux = sAux + "<hr>" + String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(elemento.data_envio));
                    }
                }
            }

            return sAux;
        }

        public string setOcorrenciasFIPT(string pAluno, string pCurso, string pOcorrencia)
        {
            string sAux = "";
            if (pOcorrencia.Trim() != "")
            {
                pOcorrencia = pOcorrencia.Replace("'", "aspasimples");
                pOcorrencia = pOcorrencia.Replace("\"", "aspadupla");

                sAux = "<div title=\"Ocorrências FIPT\"> <a class=\"btn btn-primary btn-circle fa fa-newspaper-o\" href=\'javascript:fOcorrenciasFIPT(\""
                           + pAluno + "\",\""
                           + pCurso + "\",\""
                           + pOcorrencia + "\")\'; ></a></div>";
            }
            return sAux;
        }

        public string setDatasSerasa(string pDataInclusao, string pDataExclusao)
        {

            string sAux;
            if (pDataInclusao != "01/01/1900 00:00:00")
            {
                sAux = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(pDataInclusao));
            }
            else
            {
                sAux = " - ";
            }
            sAux = sAux + "<hr>";
            if (pDataExclusao != "01/01/1900 00:00:00")
            {
                sAux = sAux + String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(pDataExclusao));
            }
            else
            {
                sAux = sAux + " - ";
            }

            return sAux;
        }

        public string setBotaoEmail(int pId_Boleto, string pEmail, string pNossoNumero, DateTime pDataEnvio)
        {
            //alunos_inadimpentes_fipt item = (alunos_inadimpentes_fipt)objeto;
            string sAux;
            sAux = "";



            if (pEmail == "")
            {
                sAux = "<div title=\"Aluno/E-mail não encontrado\"> <label class=\"text-danger\">Registro não encontrado</label></div>";
            }
            else if (pNossoNumero == "")
            {
                sAux = "<div title=\"Sem Nosso Numero\"> <label class=\"text-purple\">NossoNumero não encontrado</label></div>";
            }
            else if (pDataEnvio != null)
            {
                sAux = sAux + "<div title=\"Envia E-mail individual\"> <a class=\"btn btn-primary btn-circle fa fa-envelope\" href=\'javascript:fEnviarEmailIndividual(\""
                        + pEmail + "\",\""
                        + pId_Boleto + "\")\'; ></a></div>";
            }
            else
            {
                sAux = "<div title=\"Fora de data\"> <label class=\"text-danger\">Fora de Data</label></div>";
            }

            return sAux;
        }

        public string setCheckEmail(int pId_Boleto, string pNome, string pEmail, string pNossoNumero, DateTime pDataEnvio)
        {
            string sAux;
            sAux = "";

            DateTime dAux = new DateTime();

            if (pDataEnvio == dAux && pEmail != "" && pNossoNumero != "")
            {
                //if (!item.alunos.alunos_inadimplentes_emails_enviados.Any(x => x.IDCurso == item_alunos_curso_inadimplente.IDCurso && x.data_envio.Value.ToString("dd/MM/yyyy") == DateTime.Today.ToString("dd/MM/yyyy")))
                //{
                //    sAux = "checked";
                //}
                //else
                //{
                //    sAux = "";
                //}
                sAux = "checked";
                sAux = "<label class=\"checkbox\"><input id = \"chkAlunoBoleto_" + pId_Boleto.ToString() + "__" + pNome + " - " + pEmail + "\" type=\"checkbox\" name=\"chkAlunoBoleto_" + pId_Boleto.ToString() + "__" + pNome + " - " + pEmail + "\" " + sAux + "><span></span></label>";

            }
            else if (pEmail != "" && pNossoNumero != "")
            {
                sAux = "<label class=\"checkbox\"><input id = \"chkAlunoBoleto_" + pId_Boleto.ToString() + "__" + pNome + " - " + pEmail + "\" type=\"checkbox\" name=\"chkAlunoBoleto_" + pId_Boleto.ToString() + "__" + pNome + " - " + pEmail + "\" " + sAux + "><span></span></label>";
            }
            else
            {
                sAux = "";
                //sAux = "<label class=\"checkbox\"><input id = \"chkAlunoPresenca_" + item.id_aluno_inadimplente.ToString() + "\" type=\"checkbox\" name=\"chkAlunoPresenca_" + item.id_aluno_inadimplente.ToString() + "\" checked><span></span></label>";
            }



            return sAux;
        }

        public string setRecalcularAluno(int pId_Boleto, string pNome)
        {
            string sAux;
            sAux = "";

            sAux = "<div title=\"Recalcular Aluno\"> <a class=\"btn btn-success btn-circle fa fa-calculator\" href=\'javascript:fCalcularAluno(\""
                           + pNome + "\",\""
                           + pId_Boleto + "\")\'; ></a></div>";

            return sAux;
        }

        //protected void grdBoletosMesCorrente_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Page")
        //    {
        //        return;
        //    }

        //    int linha = Convert.ToInt32(e.CommandArgument);
        //    int codigo = Convert.ToInt32(grdBoletosMesCorrente.DataKeys[linha].Values[0]);
        //    alunos item = new alunos();
        //    item.idaluno = codigo;
        //    switch (grdBoletosMesCorrente.DataKeys[linha].Values[1].ToString())
        //    {
        //        case "Editar":
        //            FIPTAplicacao aplicacaoAluno = new FIPTAplicacao();
        //            item = aplicacaoAluno.BuscaItem(item);
        //            Session.Add("Aluno", item);
        //            Session.Add("sNovoAluno", false);
        //            Response.Redirect("cadAlunoGestao.aspx", true);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //public void grdBoletosMesCorrente_Command(object sender, CommandEventArgs e)
        //{
        //    int index = Convert.ToInt32(e.CommandArgument);
        //    if (e.CommandName == "StartService")
        //    {
        //        int linha = Convert.ToInt32(e.CommandArgument);
        //        int codigo = Convert.ToInt32(grdBoletosMesCorrente.DataKeys[linha].Values[0]);
        //        alunos item = new alunos();
        //        item.idaluno = codigo;
        //        FIPTAplicacao aplicacaoAluno = new FIPTAplicacao();
        //        item = aplicacaoAluno.BuscaItem(item);
        //        Session.Add("Aluno", item);
        //        Session.Add("sNovoAluno", false);
        //        Response.Redirect("cadAlunoGestao.aspx", true);
        //    }
        //}

        protected void btnCalcularAluno_Click(object sender, EventArgs e)
        {
            FIPTAplicacao aplicacaoFIPT = new FIPTAplicacao();
            alunos_inadimpentes_fipt item = new alunos_inadimpentes_fipt();
            alunos_curso_inadimplente item_alunoCurso = new alunos_curso_inadimplente();


            item.id_aluno_inadimplente = Convert.ToInt32(HttpContext.Current.Request["hCodigoAluno"]);

            item = aplicacaoFIPT.BuscaItem(item);
            item.data_pesquisa_fipt = DateTime.Now;
            //Apagar todos os regsitros

            var pIdChave = item.alunos_curso_inadimplente.Select(x => x.id_aluno_curso_inadimplente).ToArray();
            foreach (var elemento in pIdChave)
            {
                item_alunoCurso.id_aluno_curso_inadimplente = elemento;
                aplicacaoFIPT.Excluir_Lote_unico_alunos_parcelas_inadimplente(item_alunoCurso);
                aplicacaoFIPT.Excluir_Lote_unico_alunos_curso_inadimplente(item_alunoCurso);
            }

            aplicacaoFIPT.Excluir_unico_alunos_inadimpentes_fipt(item);

            aplicacaoFIPT.ConsultaAlunosFipt(item);

            btnPesquisaBoletoMesCorrente_Click(null, null);
        }

        protected void btnNovaConsultaFIPT_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            FIPTAplicacao aplicacaoFIPT = new FIPTAplicacao();

            //Extrai dados FIPT e insere no SAPIENS
            aplicacaoFIPT.ProcessaBoletosMesFipt(DateTime.Today.Month, DateTime.Today.Year);

            //Agora percorre dados SAPIENS e calcula Código de barras e Linha digitável
            List<boleto_email> lista_Boleto = new List<boleto_email>();
            lista_Boleto = aplicacaoFIPT.ListaBoletosMesFipt(0, "", DateTime.Today.Month, DateTime.Today.Year);

            foreach (var elemento in lista_Boleto)
            {
                if (elemento.linha_digitavel != "")
                {
                    string qLinha = "";
                    string qDV;

                    string qCodigoBanco = "001";
                    string qCodigo_banco_com_dv = Utilizades.GeraCodigoBancoComDigito(elemento.numero_banco);
                    string qNummoeda = "9";
                    string qFator_vencimento = Utilizades.fator_vencimento(elemento.data_vencimento.Value.ToString("dd/MM/yyyy")).ToString();

                    if (qFator_vencimento.Length < 4)
                    {
                        int qLoop = qFator_vencimento.Length;
                        for (int j = qLoop; j < 4; j++)
                        {
                            qFator_vencimento = qFator_vencimento + "0";
                        }
                    }

                    qFator_vencimento = Utilizades.FormataNumero(qFator_vencimento, "4", "0");
                    //valor tem 10 digitos, sem virgula
                    string qValor = string.Format("{0:C2}",elemento.valor);

                    qValor = qValor.Replace("R$", "").Replace(",", "").Replace(".", "").Trim();

                    qValor = Utilizades.FormataNumero(qValor, "10", "0", "valor");

                    //agencia é sempre 4 digitos
                    string qAgencia = Utilizades.FormataNumero(elemento.numero_agencia, "4", "0");
                    
                    //conta é sempre 8 digitos ****não é usado para calcular a linha digitável
                    //string qConta = Utilizades.FormataNumero(elemento.nu, "8", "0");
                    
                    //carteira 18
                    string qCarteira = elemento.carteira;
                    //agencia e conta
                    //string qAgencia_codigo = item.NumeroAgencia + "-" + CalcularDigitoModulo11(item.NumeroAgencia) + " / " + item.NumeroConta + "-" + CalcularDigitoModulo11(item.NumeroConta);
                    //Zeros: usado quando convenio de 7 digitos
                    string qLivre_zeros = "000000";

                    string qConvenio = "";
                    string qNossoNumero = "";

                    string qLinhaDigitavel = "";
                    int nOk;

                    if (int.TryParse(qCarteira, out nOk))
                    {
                        // x is an int
                        // Do something
                        // Carteira 18 com Convênio de 8 dígitos
                        if (elemento.numero_convenio.Length == 8)
                        {
                            qConvenio = Utilizades.FormataNumero(elemento.numero_convenio, "8", "0", "convenio");
                            // Nosso número de até 9 dígitos
                            qNossoNumero = Utilizades.FormataNumero(elemento.nosso_numero, "9", "0");
                            qDV = Utilizades.CalcularDigitoModulo11(qCodigoBanco + qNummoeda + qFator_vencimento + qValor + qLivre_zeros + qConvenio + qNossoNumero + qCarteira).ToString(); //modulo_11("$codigobanco$nummoeda$fator_vencimento$valor$livre_zeros$convenio$nossonumero$carteira");
                            qLinha = qCodigoBanco + qNummoeda + qDV + qFator_vencimento + qValor + qLivre_zeros + qConvenio + qNossoNumero + qCarteira; //"$codigobanco$nummoeda$dv$fator_vencimento$valor$livre_zeros$convenio$nossonumero$carteira";
                                                                                                                                                        //$nossonumero = $convenio. $nossonumero."-".modulo_11($convenio.$nossonumero);
                            qLinhaDigitavel = Utilizades.MontaLinhaDigitavel(qLinha);
                        }

                        // Carteira 18 com Convênio de 7 dígitos
                        if (elemento.numero_convenio.Length == 7)
                        {
                            qConvenio = Utilizades.FormataNumero(elemento.numero_convenio, "7", "0", "convenio");
                            // Nosso número de até 9 dígitos
                            qNossoNumero = Utilizades.FormataNumero(elemento.nosso_numero, "10", "0");
                            qDV = Utilizades.CalcularDigitoModulo11(qCodigoBanco + qNummoeda + qFator_vencimento + qValor + qLivre_zeros + qNossoNumero + qCarteira).ToString(); //modulo_11("$codigobanco$nummoeda$fator_vencimento$valor$livre_zeros$convenio$nossonumero$carteira");
                            qLinha = qCodigoBanco + qNummoeda + qDV + qFator_vencimento + qValor + qLivre_zeros + qNossoNumero + qCarteira; //"$codigobanco$nummoeda$dv$fator_vencimento$valor$livre_zeros$convenio$nossonumero$carteira";

                            qLinhaDigitavel = Utilizades.MontaLinhaDigitavel(qLinha);

                            //Não existe DV na composição do nosso-número para convênios de sete posições//montando o nosso numero que aparecerá no boleto
                        }
                    }
                    
                    elemento.linha_digitavel = qLinhaDigitavel;

                    elemento.linha = qLinha;

                    //==================================
                    // Calcula Codigo de Barra

                    elemento.codigo_barra = Utilizades.CodigoBarra(qLinha);

                    aplicacaoFIPT.AlteraBoletosMesFipt(elemento);
                }
            }

            txtAnoBoleto.Value = DateTime.Today.Year.ToString();
            ddlMesBoleto.SelectedValue = DateTime.Today.Month.ToString();

            btnPesquisaBoletoMesCorrente_Click(null, null);
        }
    }
}