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
    public partial class finListaInadimplenteFIPT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null)
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 76)) //Lista de Inadimplentes FITP
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                if (Session["arrayFiltroListaInadimplentesFIPT"] != null)
                {
                    CarregarDados();
                }
            }
            else
            {
                if (grdAluno_InadimplenteFIPT.Rows.Count != 0)
                {

                    grdAluno_InadimplenteFIPT.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }

        }

        protected void grdAluno_InadimplenteFIPT_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            grdAluno_InadimplenteFIPT.PageIndex = e.NewPageIndex;
            grdAluno_InadimplenteFIPT.SelectedIndex = -1;
        }

        private void CarregarDados()
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            string[] arrayFiltroListaInadimplentesFIPT = new string[4];

            alunos_inadimpentes_fipt item_alunos_inadimpentes_fipt = new alunos_inadimpentes_fipt();
            alunos_curso_inadimplente item_alunos_curso_inadimplente = new alunos_curso_inadimplente();

            item_alunos_inadimpentes_fipt.alunos_curso_inadimplente.Add(item_alunos_curso_inadimplente);

            arrayFiltroListaInadimplentesFIPT = (string[])Session["arrayFiltroListaInadimplentesFIPT"];

            if (arrayFiltroListaInadimplentesFIPT[0] != "" && arrayFiltroListaInadimplentesFIPT[0] != null)
            {
                item_alunos_inadimpentes_fipt.idaluno = System.Convert.ToDecimal(arrayFiltroListaInadimplentesFIPT[0]);
                txtMatriculaAluno.Value = arrayFiltroListaInadimplentesFIPT[0];
            }

            if (arrayFiltroListaInadimplentesFIPT[1] != "" && arrayFiltroListaInadimplentesFIPT[1] != null)
            {
                item_alunos_inadimpentes_fipt.nome = arrayFiltroListaInadimplentesFIPT[1];
                txtNomeAluno.Value = arrayFiltroListaInadimplentesFIPT[1];
            }

            if (arrayFiltroListaInadimplentesFIPT[2] != "" && arrayFiltroListaInadimplentesFIPT[2] != null)
            {
                item_alunos_inadimpentes_fipt.alunos_curso_inadimplente.ElementAt(0).NomeCurso = arrayFiltroListaInadimplentesFIPT[2];
                txtCursoAluno.Value = arrayFiltroListaInadimplentesFIPT[2];
            }

            if (arrayFiltroListaInadimplentesFIPT[3] != "" && arrayFiltroListaInadimplentesFIPT[3] != null)
            {
                item_alunos_inadimpentes_fipt.data_pesquisa_fipt = Convert.ToDateTime(arrayFiltroListaInadimplentesFIPT[3]);
                txtDataAPartirDe.Value = arrayFiltroListaInadimplentesFIPT[3];
            }

            //Session["arrayFiltroListaInadimplentesFIPT"] = arrayFiltroListaInadimplentesFIPT;
            FIPTAplicacao aplicacaoFIPT = new FIPTAplicacao();
            List<alunos_curso_inadimplente> listaAluno = new List<alunos_curso_inadimplente>();

            listaAluno = aplicacaoFIPT.ListaAlunosInadimpelntes2(item_alunos_inadimpentes_fipt);

            grdAluno_InadimplenteFIPT.DataSource = listaAluno;
            grdAluno_InadimplenteFIPT.DataBind();

            if (listaAluno.Count > 0)
            {
                grdAluno_InadimplenteFIPT.UseAccessibleHeader = true;
                grdAluno_InadimplenteFIPT.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultados.Visible = false;
                grdAluno_InadimplenteFIPT.Visible = true;
                btnLocalizaEmailsLote.Visible = true;
            }
            else
            {
                msgSemResultados.Visible = true;
                btnLocalizaEmailsLote.Visible = false;
            }
            divResultados.Visible = true;
        }

        protected void btnPesquisaListaInadimplenteFIPT_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroListaInadimplentesFIPT = new string[4];

            if (txtMatriculaAluno.Value.Trim() != "")
            {
                arrayFiltroListaInadimplentesFIPT[0] = txtMatriculaAluno.Value.Trim();
            }

            if (txtNomeAluno.Value.Trim() != "")
            {
                arrayFiltroListaInadimplentesFIPT[1] = txtNomeAluno.Value.Trim();
            }

            if (txtCursoAluno.Value.Trim() != "")
            {
                arrayFiltroListaInadimplentesFIPT[2] = txtCursoAluno.Value.Trim();
            }

            if (txtDataAPartirDe.Value.Trim() != "")
            {
                arrayFiltroListaInadimplentesFIPT[3] = txtDataAPartirDe.Value.Trim();
            }

            Session["arrayFiltroListaInadimplentesFIPT"] = arrayFiltroListaInadimplentesFIPT;

            CarregarDados();

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

                btnPesquisaListaInadimplenteFIPT_Click(null, null);
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

            btnPesquisaListaInadimplenteFIPT_Click(null, null);
        }

        private string PreparaCorpoEmail (int qId_aluno_Curso)
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


        private bool EnviaEmail(string pTo, string pCc, string pAssunto,string pCorpo)
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

        public string setBotaoEmail(object objeto, int pIdAlunoCurso)
        {
            alunos_inadimpentes_fipt item = (alunos_inadimpentes_fipt)objeto;
            alunos_curso_inadimplente item_alunos_curso_inadimplente = item.alunos_curso_inadimplente.Where(x => x.id_aluno_curso_inadimplente == pIdAlunoCurso).SingleOrDefault();
            string sAux;
            sAux = "";
            if (item.observacao == "" || item.observacao == null)
            {
                if (Convert.ToDateTime(item.data_pesquisa_fipt.Value.ToString("dd/MM/yyyy")) < Convert.ToDateTime (DateTime.Today.ToString("dd/MM/yyyy")))
                {
                    sAux = "<div title=\"Fora de data\"> <label class=\"text-danger\">Fora de Data</label></div>";
                }
                else
                {

                    if (!item.alunos.alunos_inadimplentes_emails_enviados.Any(x=> x.IDCurso == item_alunos_curso_inadimplente.IDCurso && x.data_envio.Value.ToString("dd/MM/yyyy") == DateTime.Today.ToString("dd/MM/yyyy")))
                    {
                        sAux = "<div title=\"Enviar e-mail individual para o aluno\"> <a class=\"btn btn-primary";
                    }
                    else
                    {
                        sAux = "<div title=\"Já enviado e-mail hoje.\"> <a class=\"btn btn-purple";
                    }

                    sAux = sAux + " btn-circle fa fa-envelope\" href=\'javascript:fEnviarEmailIndividual(\""
                           + item.email + "\",\""
                           + item_alunos_curso_inadimplente.NomeCurso + "\",\""
                           + item.idaluno + "\",\""
                           + item.idaluno_fipt + "\",\""
                           + item_alunos_curso_inadimplente.id_aluno_curso_inadimplente + "\",\""
                           + item_alunos_curso_inadimplente.IDCurso + "\")\'; ></a></div>";
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
            alunos_inadimpentes_fipt item = (alunos_inadimpentes_fipt)objeto;
            alunos_curso_inadimplente item_alunos_curso_inadimplente = item.alunos_curso_inadimplente.Where(x => x.id_aluno_curso_inadimplente == pIdAlunoCurso).SingleOrDefault();
            string sAux;
            sAux = "";
            if (( item.observacao == null) && Convert.ToDateTime(item.data_pesquisa_fipt.Value.ToString("dd/MM/yyyy")) == Convert.ToDateTime(DateTime.Today.ToString("dd/MM/yyyy")))
            {
                if (!item.alunos.alunos_inadimplentes_emails_enviados.Any(x => x.IDCurso == item_alunos_curso_inadimplente.IDCurso && x.data_envio.Value.ToString("dd/MM/yyyy") == DateTime.Today.ToString("dd/MM/yyyy")))
                {
                    sAux = "checked";
                }
                else
                {
                    sAux = "";
                }

                sAux = "<label class=\"checkbox\"><input id = \"chkAlunoInadimplente_" + item_alunos_curso_inadimplente.id_aluno_curso_inadimplente.ToString() + "_" + item.nome.ToString() + " (" + item_alunos_curso_inadimplente.NomeCurso.ToString() + ")\" type=\"checkbox\" name=\"chkAlunoInadimplente_" + item_alunos_curso_inadimplente.id_aluno_curso_inadimplente.ToString() + "_" + item.nome.ToString() + " (" + item_alunos_curso_inadimplente.NomeCurso.ToString() + ")\" "+ sAux +"><span></span></label>";
            }
            else
            {
                sAux = "";
                //sAux = "<label class=\"checkbox\"><input id = \"chkAlunoPresenca_" + item.id_aluno_inadimplente.ToString() + "\" type=\"checkbox\" name=\"chkAlunoPresenca_" + item.id_aluno_inadimplente.ToString() + "\" checked><span></span></label>";
            }



            return sAux;
        }

        public string setRecalcularAluno(object objeto)
        {
            alunos_inadimpentes_fipt item = (alunos_inadimpentes_fipt)objeto;
            string sAux;
            sAux = "";

            sAux = "<div title=\"Recalcular Aluno\"> <a class=\"btn btn-success btn-circle fa fa-calculator\" href=\'javascript:fCalcularAluno(\""
                           + item.nome + "\",\""
                           + item.id_aluno_inadimplente + "\")\'; ></a></div>";

            return sAux;
        }

        //protected void grdAluno_InadimplenteFIPT_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Page")
        //    {
        //        return;
        //    }

        //    int linha = Convert.ToInt32(e.CommandArgument);
        //    int codigo = Convert.ToInt32(grdAluno_InadimplenteFIPT.DataKeys[linha].Values[0]);
        //    alunos item = new alunos();
        //    item.idaluno = codigo;
        //    switch (grdAluno_InadimplenteFIPT.DataKeys[linha].Values[1].ToString())
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

        //public void grdAluno_InadimplenteFIPT_Command(object sender, CommandEventArgs e)
        //{
        //    int index = Convert.ToInt32(e.CommandArgument);
        //    if (e.CommandName == "StartService")
        //    {
        //        int linha = Convert.ToInt32(e.CommandArgument);
        //        int codigo = Convert.ToInt32(grdAluno_InadimplenteFIPT.DataKeys[linha].Values[0]);
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

            var pIdChave = item.alunos_curso_inadimplente.Select(x=> x.id_aluno_curso_inadimplente).ToArray() ;
            foreach (var elemento in pIdChave)
            {
                item_alunoCurso.id_aluno_curso_inadimplente = elemento;
                aplicacaoFIPT.Excluir_Lote_unico_alunos_parcelas_inadimplente(item_alunoCurso);
                aplicacaoFIPT.Excluir_Lote_unico_alunos_curso_inadimplente(item_alunoCurso);
            }

            aplicacaoFIPT.Excluir_unico_alunos_inadimpentes_fipt(item);

            aplicacaoFIPT.ConsultaAlunosFipt(item);

            btnPesquisaListaInadimplenteFIPT_Click(null, null);
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

            aplicacaoFIPT.ConsultaAlunosFipt(item);

            List<alunos_inadimpentes_fipt> listaAluno = new List<alunos_inadimpentes_fipt>();

            //listaAluno = aplicacaoFIPT.ListaAlunosInadimpelntes(item).Where(x=> x.idaluno != null).ToList();

            var sAux2 = aplicacaoFIPT.ListaAlunosInadimpelntes(item).Where(x => x.idaluno != null).Select(x=> x.idaluno).GroupBy(x => x.Value).ToArray();

            IGrouping<decimal,decimal?>[] sAux = aplicacaoFIPT.ListaAlunosInadimpelntes(item).Where(x => x.idaluno != null).Select(x => x.idaluno).GroupBy(x => x.Value).ToArray();

            aplicacaoInscricao.CriarTodos(sAux, item.usuario);

            btnPesquisaListaInadimplenteFIPT_Click(null, null);
        }
    }
}