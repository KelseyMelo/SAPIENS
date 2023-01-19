using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections;
using System.Configuration;
using System.IO;

namespace SERPI.UI.WebForms_C
{
    public partial class aluBoletos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null)
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 77)) //Tela de Boletos dos Alunos
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                    CarregarDados();
            }
            else
            {
                if (grdAluno_Boletos.Rows.Count != 0)
                {

                    grdAluno_Boletos.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }

        }

        protected void grdAluno_Boletos_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            grdAluno_Boletos.PageIndex = e.NewPageIndex;
            grdAluno_Boletos.SelectedIndex = -1;
        }

        private void CarregarDados()
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            alunos item_aluno = (alunos)Session["AlunoLogado"];

            CalcularAluno(item_aluno);



            //alunos_inadimpentes_fipt item_alunos_inadimpentes_fipt = new alunos_inadimpentes_fipt();
            //alunos_curso_inadimplente item_alunos_curso_inadimplente = new alunos_curso_inadimplente();

            //item_alunos_inadimpentes_fipt.alunos_curso_inadimplente.Add(item_alunos_curso_inadimplente);

            //Session["arrayFiltroListaInadimplentesFIPT"] = arrayFiltroListaInadimplentesFIPT;
            FIPTAplicacao aplicacaoFIPT = new FIPTAplicacao();
            List<alunos_boletos_curso> listaAluno = new List<alunos_boletos_curso>();

            List<alunos_boletos_parcelas> listaparcelas = new List<alunos_boletos_parcelas>();

            alunos_boletos item = new alunos_boletos();
            item.idaluno = item_aluno.idaluno;

            //listaAluno = aplicacaoFIPT.ListaAlunosBoletos(item);

            var lista = aplicacaoFIPT.ListaAlunosBoletos(item);

            foreach (var elemento in lista)
            {

                listaparcelas.AddRange(elemento.alunos_boletos_parcelas);
            }

            grdAluno_Boletos.DataSource = listaparcelas;
            grdAluno_Boletos.DataBind();

            if (listaparcelas.Count > 0)
            {
                grdAluno_Boletos.UseAccessibleHeader = true;
                grdAluno_Boletos.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultados.Visible = false;
                grdAluno_Boletos.Visible = true;
                //btnLocalizaEmailsLote.Visible = true;
            }
            else
            {
                msgSemResultados.Visible = true;
                //btnLocalizaEmailsLote.Visible = false;
            }
        }


        protected void btnEnviarEmailUnitario_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (EnviaEmail(txtParaEmail.Value.Trim(), txtCcEmail.Value.Trim(), txtAssuntoEmail.Value.Trim(), txtCorpoEmail.Value.Trim()))
            {
                FIPTAplicacao aplicacaoFIPT = new FIPTAplicacao();
                alunos_inadimplentes_emails_enviados item = new alunos_inadimplentes_emails_enviados();
                item.idaluno = Convert.ToInt32(txtIdAluno.Value.Trim());
                item.IDCurso = Convert.ToInt32(txtIdCurso.Value.Trim());
                item.data_envio = DateTime.Now;
                item.usuario = usuario.usuario;

                aplicacaoFIPT.CriarItem_alunos_inadimplentes_emails_enviados(item);

                aplicacaoFIPT.Criar_emails_enviados_alunos_Inadimplentes_Gemini(Convert.ToInt32(txtIdAluno_FIPT.Value.Trim()), item.data_envio.Value, Convert.ToInt32(txtIdAlunoCurso.Value.Trim()));

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
            alunos_curso_inadimplente item_alunos_curso_inadimplente = new alunos_curso_inadimplente();
            string sAlunosNaoEnviados = "";

            string qEmailsLote = HttpContext.Current.Request.Form["hCodigoAluno"];

            foreach (var elemento in qEmailsLote.Split(';'))
            {
                if (elemento == "")
                {
                    continue;
                }
                item_alunos_curso_inadimplente.id_aluno_curso_inadimplente = Convert.ToInt32(elemento);
                item_alunos_curso_inadimplente = aplicacaoFIPT.BuscaItem(item_alunos_curso_inadimplente);

                if (EnviaEmail(item_alunos_curso_inadimplente.alunos_inadimpentes_fipt.email,
                    "financeiroensino@ipt.br",
                    "Pendência - Curso: " + item_alunos_curso_inadimplente.NomeCurso,
                    PreparaCorpoEmail(item_alunos_curso_inadimplente.id_aluno_curso_inadimplente)))
                {
                    alunos_inadimplentes_emails_enviados item = new alunos_inadimplentes_emails_enviados();
                    item.idaluno = item_alunos_curso_inadimplente.alunos_inadimpentes_fipt.idaluno;
                    item.IDCurso = item_alunos_curso_inadimplente.IDCurso;
                    item.data_envio = DateTime.Now;
                    item.usuario = usuario.usuario;

                    aplicacaoFIPT.CriarItem_alunos_inadimplentes_emails_enviados(item);

                    aplicacaoFIPT.Criar_emails_enviados_alunos_Inadimplentes_Gemini(item_alunos_curso_inadimplente.alunos_inadimpentes_fipt.idaluno_fipt.Value, item.data_envio.Value, item_alunos_curso_inadimplente.id_aluno_curso_inadimplente);
                }
                else
                {
                    if (sAlunosNaoEnviados != "")
                    {
                        sAlunosNaoEnviados = sAlunosNaoEnviados + ";<br> ";
                    }

                    sAlunosNaoEnviados = sAlunosNaoEnviados + item_alunos_curso_inadimplente.alunos_inadimpentes_fipt.nome;
                }
            }

            if (sAlunosNaoEnviados != "")
            {
                lblMensagem.Text = "Houve um erro no envio do e-mail para o(s) Aluno(s):<br> <strong>" + sAlunosNaoEnviados + "</strong>.<br><br>Por favor, tente novamente.";
                lblTituloMensagem.Text = "Envio de E-mail";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
            }

        }

        private string PreparaCorpoEmail(int qId_aluno_Curso)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            FIPTAplicacao aplicacaoFIPT = new FIPTAplicacao();
            alunos_curso_inadimplente item = new alunos_curso_inadimplente();
            item.id_aluno_curso_inadimplente = qId_aluno_Curso;
            item = aplicacaoFIPT.BuscaItem(item);

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
            sAux = sAux + "             <span style=\"color:black; font-size: 14pt; \">" + string.Format("{0:N}", item.alunos_parcelas_inadimplente.Sum(x => x.ValorOriginal)) + " </span>";
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

        public string setImprimirBoleto(DateTime data_venc, DateTime data_pagto, decimal pValorCorrigido, decimal pValorOriginal, int IDLanc, string NossoNumero)
        {
            //HashSet<alunos_boletos_parcelas> lista = (HashSet<alunos_boletos_parcelas>)objeto;
            string sAux = "";
            DateTime dData_vazia = new DateTime();

            DateTime dData_atual = DateTime.Today;
            bool bVencido = true;

            if (data_pagto != dData_vazia)
            {
                sAux = "<div title=\"Pagamento já efetuado.\"><i class=\"fa fa-2x fa-thumbs-o-up text-success\"></i></div>";
                return sAux;
            }
            else if (dData_atual <= data_venc)
            {
                bVencido = false;
                //if (NossoNumero != "")
                //{
                //    sAux = "<div title=\"Imprimir Boleto Sem Juros\"> <a class=\"btn btn-primary btn-circle fa fa fa-barcode fa-2x\" href=\'javascript:fEmitirBoleto(\""
                //           + IDLanc + "\",\""
                //           + String.Format("{0:C}", pValorOriginal) + "\",\""
                //           + String.Format("{0:C}", pValorCorrigido) + "\",\""
                //           + String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(data_venc)) + "\",\""
                //           + 0 + "\")\'; ></a></div>";
                //}
                //else
                //{
                //    sAux = "<div title=\"Boleto ainda não foi Emitido pela FIPT\"> <a class=\"btn btn-warning btn-circle fa fa-2x fa-info-circle\" href=\'javascript:fInformacao(\""
                //           + "Boleto ainda não foi Emitido pela FIPT"
                //            + "\")\'; ></a></div>";
                //}
                
            }
            //else if ((dData_atual - data_venc).TotalDays <= 28)
            //{
            //    sAux = "<div title=\"Imprimir Boleto com Juros\"> <a class=\"btn btn-purple btn-circle fa fa fa-barcode fa-2x\" href=\'javascript:fInformacao(\""
            //               + "" + "\",\""
            //               + "" + "\")\'; ></a></div>";
            //}
            //else
            //{
                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                feriado item_feriado;
                
                DateTime dData_Aux = data_venc;
                if (dData_atual.Subtract(data_venc).Days < 6 && dData_atual.Subtract(data_venc).Days > 0)
                {
                    bVencido = false;
                    do
                    {
                        item_feriado = aplicacaoGerais.ListaFeriado_porData(dData_Aux);
                        if (item_feriado == null && ((dData_Aux.DayOfWeek != DayOfWeek.Saturday) && (dData_Aux.DayOfWeek != DayOfWeek.Sunday)))
                        {
                            bVencido = true;
                            break;
                        }
                        dData_Aux = dData_Aux.AddDays(1);
                    } while (dData_atual != dData_Aux);
                }


                if (bVencido)
                {
                    if (IDLanc.ToString() != "")
                    {
                        sAux = "<div title=\"Imprimir Boleto com Juros\"> <a class=\"btn btn-danger btn-circle fa fa fa-barcode fa-2x\" href=\'javascript:fEmitirBoleto(\""
                               + IDLanc + "\",\""
                               + String.Format("{0:C}", pValorOriginal) + "\",\""
                               + String.Format("{0:C}", pValorCorrigido) + "\",\""
                               + String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(data_venc)) + "\",\""
                               + 1 + "\")\'; ></a></div>";
                    }
                    else
                    {
                        sAux = "<div title=\"Sem correspondente na Base de Dados\"> <a class=\"btn btn-danger btn-circle fa fa-2x fa-info-circle\" href=\'javascript:fInformacao(\""
                               + "O valor corrigido dessa parcela é de: <b>" + String.Format("{0:C}", pValorCorrigido)
                               + "</b><br><br>O registro está Sem correspondente na Base de Dados, portanto o pagamento só poderá ser feito através de PIX ou TED com os seguintes dados bancários: <br><br> banco: <b>001</b> <br>agência: <b>1897-X</b> <br> CC: <b>78600-4</b> <br> CNPJ: <b>05.505.390/0001-75</b> <br><br> Logo após o pagametno informar à FIPT sobre o acerto." + "\",\""
                               + "\")\'; ></a></div>";
                    }
                }

                else
                {
                    if (NossoNumero != "")
                    {
                        sAux = "<div title=\"Imprimir Boleto Sem Juros\"> <a class=\"btn btn-primary btn-circle fa fa fa-barcode fa-2x\" href=\'javascript:fEmitirBoleto(\""
                               + IDLanc + "\",\""
                               + String.Format("{0:C}", pValorOriginal) + "\",\""
                               + String.Format("{0:C}", pValorCorrigido) + "\",\""
                               + String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(data_venc)) + "\",\""
                               + 0 + "\")\'; ></a></div>";
                    }
                    else
                    {
                        sAux = "<div title=\"Boleto ainda não foi Emitido pela FIPT\"> <a class=\"btn btn-warning btn-circle fa fa-2x fa-info-circle\" href=\'javascript:fInformacao(\""
                               + "Boleto ainda não foi Emitido pela FIPT"
                                + "\")\'; ></a></div>";
                    }
                }
                
            //}
            return sAux;
        }

        public string setParcelasAtrazadas(object objeto)
        {
            HashSet<alunos_boletos_parcelas> lista = (HashSet<alunos_boletos_parcelas>)objeto;
            string sAux;

            sAux = lista.Count().ToString();

            return sAux;
        }

        public string setValorTotal(object objeto)
        {
            HashSet<alunos_boletos_parcelas> lista = (HashSet<alunos_boletos_parcelas>)objeto;
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

        public string setBotaoEmail(object objeto, int pIdAlunoCurso)
        {
            alunos_boletos item = (alunos_boletos)objeto;
            alunos_boletos_curso item_alunos_curso = item.alunos_boletos_curso.Where(x => x.id_alunos_boletos_curso == pIdAlunoCurso).SingleOrDefault();
            string sAux;
            sAux = "";
            if (item.observacao == "" || item.observacao == null)
            {
                if (Convert.ToDateTime(item.data_pesquisa_fipt.Value.ToString("dd/MM/yyyy")) < Convert.ToDateTime(DateTime.Today.ToString("dd/MM/yyyy")))
                {
                    sAux = "<div title=\"Fora de data\"> <label class=\"text-danger\">Fora de Data</label></div>";
                }
                else
                {

                    if (!item.alunos.alunos_inadimplentes_emails_enviados.Any(x => x.IDCurso == item_alunos_curso.IDCurso && x.data_envio.Value.ToString("dd/MM/yyyy") == DateTime.Today.ToString("dd/MM/yyyy")))
                    {
                        sAux = "<div title=\"Enviar e-mail individual para o aluno\"> <a class=\"btn btn-primary";
                    }
                    else
                    {
                        sAux = "<div title=\"Já enviado e-mail hoje.\"> <a class=\"btn btn-purple";
                    }

                    sAux = sAux + " btn-circle fa fa-envelope\" href=\'javascript:fEnviarEmailIndividual(\""
                           + item.email + "\",\""
                           + item_alunos_curso.nome_curso + "\",\""
                           + item.idaluno + "\",\""
                           + item.idaluno_fipt + "\",\""
                           + item_alunos_curso.id_alunos_boletos_curso + "\",\""
                           + item_alunos_curso.IDCurso + "\")\'; ></a></div>";
                }
            }
            else
            {
                sAux = "<div title=\"Observação de processamento\"> <a class=\"btn btn-danger btn-circle fa fa-info-circle\" href=\'javascript:fInformacao(\""
                           + item.nome + "\",\""
                           + item.observacao + "\")\'; ></a></div>";
            }

            return sAux;
        }

        public string setCheckEmail(object objeto, int pIdAlunoCurso)
        {
            //alunos_inadimpentes_fipt item = (alunos_inadimpentes_fipt)objeto;
            //alunos_curso_inadimplente item_alunos_curso_inadimplente = item.alunos_curso_inadimplente.Where(x => x.id_aluno_curso_inadimplente == pIdAlunoCurso).SingleOrDefault();
            string sAux;
            sAux = "";
            return sAux;
        }

        public string setRecalcularAluno(object objeto)
        {
            alunos_boletos item = (alunos_boletos)objeto;
            string sAux;
            sAux = "";

            sAux = "<div title=\"Recalcular Aluno\"> <a class=\"btn btn-success btn-circle fa fa-calculator\" href=\'javascript:fCalcularAluno(\""
                           + item.nome + "\",\""
                           + item.id_alunos_boletos + "\")\'; ></a></div>";

            return sAux;
        }

        //protected void grdAluno_Boletos_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Page")
        //    {
        //        return;
        //    }

        //    int linha = Convert.ToInt32(e.CommandArgument);
        //    int codigo = Convert.ToInt32(grdAluno_Boletos.DataKeys[linha].Values[0]);
        //    alunos item = new alunos();
        //    item.idaluno = codigo;
        //    switch (grdAluno_Boletos.DataKeys[linha].Values[1].ToString())
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

        //public void grdAluno_Boletos_Command(object sender, CommandEventArgs e)
        //{
        //    int index = Convert.ToInt32(e.CommandArgument);
        //    if (e.CommandName == "StartService")
        //    {
        //        int linha = Convert.ToInt32(e.CommandArgument);
        //        int codigo = Convert.ToInt32(grdAluno_Boletos.DataKeys[linha].Values[0]);
        //        alunos item = new alunos();
        //        item.idaluno = codigo;
        //        FIPTAplicacao aplicacaoAluno = new FIPTAplicacao();
        //        item = aplicacaoAluno.BuscaItem(item);
        //        Session.Add("Aluno", item);
        //        Session.Add("sNovoAluno", false);
        //        Response.Redirect("cadAlunoGestao.aspx", true);
        //    }
        //}

        private void CalcularAluno (alunos pItem)
        {
            FIPTAplicacao aplicacaoFIPT = new FIPTAplicacao();
            alunos_boletos item = new alunos_boletos();
            //List<aluBoletos> lista = new List<aluBoletos>();
            alunos_boletos_curso item_alunoCurso = new alunos_boletos_curso();


            item.idaluno = Convert.ToInt32(pItem.idaluno);

            // utiliza o cpf para buscar na base de dados da FIPT
            item.cpf = pItem.cpf;

            var lista = aplicacaoFIPT.Lista_BuscaItem_idaluno(item);
            item.data_pesquisa_fipt = DateTime.Now;
            //Apagar todos os regsitros
            foreach (var elemento in lista)
            {
                var pIdChave = elemento.alunos_boletos_curso.Select(x => x.id_alunos_boletos_curso).ToArray();
                foreach (var elemento2 in pIdChave)
                {
                    item_alunoCurso.id_alunos_boletos_curso = elemento2;
                    aplicacaoFIPT.Excluir_Lote_unico_alunos_boletos_parcelas(item_alunoCurso);
                    aplicacaoFIPT.Excluir_Lote_unico_alunos_boletos_curso(elemento);
                }

                aplicacaoFIPT.Excluir_unico_alunos_boletos(elemento);
            }
            
            aplicacaoFIPT.ConsultaBoletosFipt(item);
        }

        protected void btnNovaConsultaFIPT_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            FIPTAplicacao aplicacaoFIPT = new FIPTAplicacao();
            alunos_inadimpentes_fipt item = new alunos_inadimpentes_fipt();
            item.data_pesquisa_fipt = DateTime.Now;
            item.usuario = usuario.usuario;

            //Apagar todos os regsitros
            aplicacaoFIPT.Excluir_Lote_todos_alunos_parcelas_inadimplente(null);
            aplicacaoFIPT.Excluir_Lote_todos_alunos_curso_inadimplente(null);
            aplicacaoFIPT.Excluir_todos_alunos_inadimpentes_fipt(null);

            //Apagar Tabela Inadimplenstes
            InadimplenteAplicacao aplicacaoInscricao = new InadimplenteAplicacao();
            aplicacaoInscricao.ApagarTodos();

            //aplicacaoFIPT.ConsultaBoletosFipt(item);

            List<alunos_inadimpentes_fipt> listaAluno = new List<alunos_inadimpentes_fipt>();

            //listaAluno = aplicacaoFIPT.ListaAlunosInadimpelntes(item).Where(x=> x.idaluno != null).ToList();

            var sAux2 = aplicacaoFIPT.ListaAlunosInadimpelntes(item).Where(x => x.idaluno != null).Select(x => x.idaluno).GroupBy(x => x.Value).ToArray();

            IGrouping<decimal, decimal?>[] sAux = aplicacaoFIPT.ListaAlunosInadimpelntes(item).Where(x => x.idaluno != null).Select(x => x.idaluno).GroupBy(x => x.Value).ToArray();

            aplicacaoInscricao.CriarTodos(sAux, item.usuario);

        }
    }
}