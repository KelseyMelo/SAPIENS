using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Security.Cryptography;
using System.Web.Security;
using System.Text;
using System.Configuration;

namespace SERPI.UI.WebForms_C
{
    public partial class SERPI : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            PreencheSininho();

            if (!Page.IsPostBack)
            {
                if ((string)Session["UsuarioClonado"] == "sim")
                {
                    aBtnAltearSenha.Visible = false;
                    bntSalvarDadosFotoOffLine.Visible = false;
                }
                //if (Session["ClassBody"] != null)
                //{
                //    Body.Attributes["Class"] = (string) Session["ClassBody"];
                //}

                if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString != "Producao")
                {
                    lblHomolog.Visible = true;
                }

                if (usuario == null)
                {
                    Response.Redirect("index.html", true);
                }

                if (usuario.TemaSistema != null)
                {
                    Body.Attributes["Class"] = usuario.TemaSistema;
                    if (usuario.TemaSistema.IndexOf("yellow") != -1)
                    {
                        id_EtiquetaNotificacao.Attributes.Remove("class");
                        id_EtiquetaNotificacao.Attributes.Add("class", "label label-primary");
                    }
                }

                if (usuario.avatar != "")
                {
                    imgLogin1.Src = "img/pessoas/" + usuario.avatar + "?" + DateTime.Now;
                }
                else
                {
                    imgLogin1.Src = "img/pessoas/avatarunissex.jpg" + "?" + DateTime.Now;
                }

                //======================

                imgLogin2.Src = imgLogin1.Src;
                imgLogin3.Src = imgLogin1.Src;

                lblNomeUsuario1.Text = usuario.nomeSocial;
                lblNomeUsuario2.Text = usuario.nome;
                lblNomeUsuario3.Text = usuario.nomeSocial;

                //for (i = 0; i > usuario.grupos_acesso.grupos_acesso_telas_sistema.Count;i++)
                //{

                //}
                Session["PerfilAluno"] = "não";

                if (usuario.grupos_acesso.descricao == "Alunos")
                {
                    alunos item = new alunos();
                    item.idaluno = Convert.ToInt32 (usuario.usuario);
                    AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                    item = aplicacaoAluno.BuscaItem(item);

                    Session["AlunoLogado"] = item;
                    Session["PerfilAluno"] = "sim";
                    //Session["UsuarioAlunoLogado"] = (usuarios)Session["UsuarioLogado"];

                    liAlunoDadosCadastrais.Visible = true;
                    aAlunoDadosCadastrais.Attributes["href"] = "aluDadosPessoais.aspx";

                    liAlunoSitAcademica.Visible = true;
                    aAlunoSitAcademica.Attributes["href"] = "javascript:fGoToPagina('aluSitAcademica.aspx');";

                    if (item.matricula_turma.Any(x=> x.turmas.cursos.id_tipo_curso == 1))
                    {
                        liAlunoDocumentosAcademicos.Visible = true;
                        aAlunoDocumentosAcademicos.Attributes["href"] = "aluDocumentosAcademicos.aspx";
                    }

                    liAlunoBoletoFipt.Visible = true;
                    aAlunoBoletoFipt.Attributes["href"] = "aluBoletos.aspx"; //"javascript:fBoletoFipt();";

                }
                else if (usuario.grupos_acesso.descricao == "Professores")
                {
                    //Definir aqui as regras para professores

                }
                else
                {
                    foreach (grupos_acesso_telas_sistema item in usuario.grupos_acesso.grupos_acesso_telas_sistema)
                    {
                        //Administração do Sistema
                        if (item.id_tela == 1)
                        { //Cadastro Usuários
                            liAdmSistema.Visible = true; 
                            liCadUsuario.Visible = true;
                            aCadUsuario.Attributes["href"] = "cadUsuario.aspx";
                        }
                        else if (item.id_tela == 74)
                        { //Cadastro de Documentos Acadêmicos
                            liAdmSistema.Visible = true;
                            liCadDocumentosAcademicos.Visible = true;
                            aCadDocumentosAcademicos.Attributes["href"] = "admDocumentosAcademicos.aspx";
                        }
                        else if (item.id_tela == 2)
                        { //Cadastro de Grupos
                            liAdmSistema.Visible = true;
                            liCadGrupo.Visible = true;
                            aCadGrupo.Attributes["href"] = "admGrupo.aspx";
                        }
                        else if (item.id_tela == 3)
                        { //Cadastro de Telas
                            liAdmSistema.Visible = true;  //"treeview"
                            liCadTela.Visible = true;
                            aCadTela.Attributes["href"] = "admTela.aspx";
                        }
                        //else if (item.id_tela == 43)
                        //{ //Portal do Aluno
                        //    liAdmSistema.Attributes["Class"] = "hidden";  //"treeview"
                        //    liPortalAluno.Attributes["Class"] = "hidden";
                        //    aPortalAluno.Attributes["href"] = "#";
                        //}
                        //else if (item.id_tela == 36)
                        //{ //Portal do Professor
                        //    liAdmSistema.Attributes["Class"] = "hidden";  //"treeview"
                        //    liPortalProfessor.Attributes["Class"] = "hidden";
                        //    aPortalProfessor.Attributes["href"] = "#";
                        //}


                        //Processo Seletivo
                        else if (item.id_tela == 10)
                        { //Periodos de Inscrição
                            liProcessoSeletivo.Visible = true;
                            liPeriodosIncricao.Visible = true;
                            aPeriodosIncricao.Attributes["href"] = "proPeriodoInscricao.aspx";
                        }
                        else if (item.id_tela == 11)
                        { //Relatório de Datas<br/>para Entrevista
                            liProcessoSeletivo.Visible = true;
                            liRelDatasEntervista.Visible = false;
                            aRelDatasEntervista.Attributes["href"] = "#";
                        }
                        else if (item.id_tela == 5)
                        { //Relatório de Inscritos
                          // liProcessoSeletivo.Attributes["Class"] = "treeview";  //"treeview"
                            liProcessoSeletivo.Visible = true;
                            //liRelInscritos.Attributes["Class"] = "";
                            liRelInscritos.Visible = true;
                            aRelInscritos.Attributes["href"] = "proRelacaoInscritos.aspx";
                        }

                        //Acadêmico
                        else if (item.id_tela == 6)
                        { //1. Alunos
                            liAcademico.Visible = true;
                            li1Alunos.Visible = true;
                            a1Alunos.Attributes["href"] = "cadAluno.aspx";
                        }
                        else if (item.id_tela == 8)
                        { //2. Professores
                            liAcademico.Visible = true;
                            li2Professores.Visible = true;
                            a2Professores.Attributes["href"] = "cadProfessor.aspx";
                        }
                        else if (item.id_tela == 9)
                        { //3. Disciplinas
                            liAcademico.Visible = true;
                            li3Disciplinas.Visible = true;
                            a3Disciplinas.Attributes["href"] = "cadDisciplina.aspx";
                        }
                        else if (item.id_tela == 73)
                        { //4. Tipo Curso
                            liAcademico.Visible = true;
                            li4TipoCursos.Visible = true;
                            a4TipoCursos.Attributes["href"] = "cadTipoCurso.aspx";

                            liAdmSistema.Visible = true;
                            liCadArquivos.Visible = true;
                            aCadArquivos.Attributes["href"] = "admArquivo.aspx";

                            liDicasBootstrap.Visible = true;

                        }
                        else if (item.id_tela == 12)
                        { //4. Cursos
                            liAcademico.Visible = true;
                            li4Cursos.Visible = true;
                            a4Cursos.Attributes["href"] = "cadCurso.aspx";

                            //aqui é liberado o menu de Arquivos para que o usuário possa faer o upload de arquivos de imagens para customizar a homepage dos cursos
                            liAdmSistema.Visible = true;
                            liCadArquivos.Visible = true;
                            aCadArquivos.Attributes["href"] = "admArquivo.aspx";

                            liDicasBootstrap.Visible = true;
                        }
                        else if (item.id_tela == 13)
                        { //5. Áreas de Concentração
                            liAcademico.Visible = true;
                            li5AreaConcentracao.Visible = true;
                            a5AreaConcentracao.Attributes["href"] = "cadAreaConcentracao.aspx";
                        }
                        else if (item.id_tela == 14)
                        { //6. Quadrimestres
                            liAcademico.Visible = true;
                            li6Quadrimestres.Visible = true;
                            a6Quadrimestres.Attributes["href"] = "cadPeriodo.aspx";
                        }
                        else if (item.id_tela == 15)
                        { //7. Turmas
                            liAcademico.Visible = true;
                            li7Turmas.Visible = true;
                            a7Turmas.Attributes["href"] = "cadTurma.aspx";
                        }
                        else if (item.id_tela == 16)
                        { //8. Oferecimentos
                            liAcademico.Visible = true;
                            li8Oferecimentos.Visible = true;
                            a8Oferecimentos.Attributes["href"] = "cadOferecimento.aspx";
                        }
                        //else if (item.id_tela == 17)
                        //{ //9. Datas de Aula
                        //    liAcademico.Visible = true;
                        //    li9DatasdeAula.Attributes["Class"] = "hidden";
                        //    a9DatasdeAula.Attributes["href"] = "#";
                        //}
                        //else if (item.id_tela == 18)
                        //{ //10. Matrícula na Turma
                        //    liAcademico.Visible = true;
                        //    li10MatriculaTurma.Attributes["Class"] = "hidden";
                        //    a10MatriculaTurma.Attributes["href"] = "#";
                        //}
                        //else if (item.id_tela == 21)
                        //{ //11. Matrícula no Oferecimento
                        //    liAcademico.Visible = true;
                        //    li11MatriculaOferecimento.Attributes["Class"] = "hidden";
                        //    a11MatriculaOferecimento.Attributes["href"] = "#";
                        //}
                        //else if (item.id_tela == 19)      //Menu retirado pois agora pertence a uma "aba" do menu Aluno
                        //{ //12. Situação da Matrícula
                        //    liAcademico.Visible = true;
                        //    li12SituacaoMatricula.Attributes["Class"] = "hidden";
                        //    a12SituacaoMatricula.Attributes["href"] = "#";
                        //}
                        //else if (item.id_tela == 23)
                        //{ //13. Cadastro de Notas
                        //    liAcademico.Visible = true;
                        //    li13CadastroNotas.Attributes["Class"] = "hidden";
                        //    a13CadastroNotas.Attributes["href"] = "#";
                        //}
                        //else if (item.id_tela == 25)
                        //{ //14. Lista de Presença
                        //    liAcademico.Visible = true;
                        //    li14ListaPresenca.Attributes["Class"] = "hidden";
                        //    a14ListaPresenca.Attributes["href"] = "#";
                        //}
                        //else if (item.id_tela == 24)
                        //{ //15. Cad. Presença Aluno
                        //    liAcademico.Visible = true;
                        //    li15CadPresencaAluno.Attributes["Class"] = "hidden";
                        //    a15CadPresencaAluno.Attributes["href"] = "#";
                        //}
                        //else if (item.id_tela == 34)
                        //{ //16. Cad. Presença Professor
                        //    liAcademico.Visible = true;
                        //    li16CadPresencaProfessor.Attributes["Class"] = "hidden";
                        //    a16CadPresencaProfessor.Attributes["href"] = "#";
                        //}
                        else if (item.id_tela == 27)
                        { //17. Relatório de Alunos
                            divHrRelatorioAcademico.Visible = true;
                            liAcademico.Visible = true;
                            li9RelatorioAlunos.Visible = true;
                            a9RelatorioAlunos.Attributes["href"] = "cadRelAluno.aspx";
                        }
                        //else if (item.id_tela == 28)           //Menu retirado pois agora pertence a uma "aba" do menu Aluno
                        //{ //18. Histórico Escolar
                        //    liAcademico.Visible = true;
                        //    li18HistoricoEscolar.Attributes["Class"] = "";
                        //    a18HistoricoEscolar.Attributes["href"] = "#";
                        //}
                        //else if (item.id_tela == 30)          //Menu retirado pois agora pertence a uma "aba" do menu Aluno
                        //{ //19. Cadastro de Orientação
                        //    liAcademico.Visible = true;
                        //    li19CadastroOrientacao.Attributes["Class"] = "";
                        //    a19CadastroOrientacao.Attributes["href"] = "#";
                        //}
                        //else if (item.id_tela == 31)        //Menu retirado pois agora pertence a uma "aba" do menu Aluno
                        //{ //20. Cadastro de Bancas
                        //    liAcademico.Visible = true;
                        //    li20CadastroBancas.Attributes["Class"] = "hidden";
                        //    a20CadastroBancas.Attributes["href"] = "#";
                        //}
                        //else if (item.id_tela == 52)    //Menu retirado pois agora pertence a uma "aba" do menu Aluno
                        //{ //21. Reuniões CPG
                        //    liAcademico.Visible = true;
                        //    li21ReunioesCPG.Attributes["Class"] = "hidden";
                        //    a21ReunioesCPG.Attributes["href"] = "#";
                        //}
                        //else if (item.id_tela == 51)     //Menu retirado pois agora pertence a uma "aba" do menu Aluno
                        //{ //22. Contratos
                        //    liAcademico.Visible = true;
                        //    li22Contratos.Attributes["Class"] = "hidden";
                        //    a22Contratos.Attributes["href"] = "#";
                        //}
                        else if (item.id_tela == 56)
                        { //23. Outros Relatórios
                            divHrRelatorioAcademico.Visible = true;

                            liAcademico.Visible = true;
                            liCadRelProfessor.Visible = true;
                            aCadRelProfessorOferecimento.Attributes["href"] = "cadRelProfessorOferecimento.aspx";
                            aCadRelProfessorOrientacoes.Attributes["href"] = "cadRelProfessorOrientacao.aspx";
                            aCadRelProfessorQualificacaoDefesa.Attributes["href"] = "cadRelProfessorQualificacaoDefesa.aspx";
                            aCadRelProfessorAulasMarcadas.Attributes["href"] = "cadRelProfessorAulasMarcadas.aspx";


                            liCadRelBanca.Visible = true;
                            aCadRelBancaQualificacaoDefesa.Attributes["href"] = "cadRelBancaQualificacaoDefesa.aspx";
                            aCadRelBancaMembros.Attributes["href"] = "cadRelBancaMembro.aspx";

                        }
                        //else if (item.id_tela == 57)      //Menu retirado pois agora pertence a uma "aba" do menu Aluno
                        //{ //24. Certificado Titulação
                        //    liAcademico.Visible = true;
                        //    li24CertificadoTitulacao.Attributes["Class"] = "hidden";
                        //    a24CertificadoTitulacao.Attributes["href"] = "#";
                        //}
                        //else if (item.id_tela == 60)
                        //{ //Período Relatórios Dissertação
                        //    liAcademico.Visible = true;
                        //    liPeriodoRelDissertacao.Attributes["Class"] = "hidden";
                        //    aPeriodoRelDissertacao.Attributes["href"] = "#";
                        //}
                        //else if (item.id_tela == 47)
                        //{ //Dados Cadastrais
                        //    liAcademico.Visible = true;
                        //    liDadosCadastrais.Attributes["Class"] = "hidden";
                        //    aDadosCadastrais.Attributes["href"] = "#";
                        //}
                        //else if (item.id_tela == 35)
                        //{ //Histórico Escolar
                        //    liAcademico.Visible = true;
                        //    liHistoricoEscolar.Attributes["Class"] = "hidden";
                        //    aHistoricoEscolar.Attributes["href"] = "#";
                        //}
                        ////else if (item.id_tela == 59) { //Matrícula on-line
                        ////    liAcademico.Visible = true;
                        ////    liMatriculaOnLine.Attributes["Class"] = "";
                        ////    aMatriculaOnLine.Attributes["href"] = "#";
                        ////}
                        //else if (item.id_tela == 60)
                        //{ //Período Relatórios Dissertação
                        //    liAcademico.Visible = true;
                        //    liPeriodoRelDissertacao.Attributes["Class"] = "hidden";
                        //    aPeriodoRelDissertacao.Attributes["href"] = "#";
                        //}
                        //else if (item.id_tela == 61)
                        //{ //Relatório<br/>Acompanhamento Dissertação
                        //    liAcademico.Visible = true;
                        //    liRelAcompanhamentoDissertacao.Attributes["Class"] = "hidden";
                        //    aRelAcompanhamentoDissertacao.Attributes["href"] = "#";
                        //}
                        //else if (item.id_tela == 53)
                        //{ //Requerimentos
                        //    liAcademico.Visible = true;
                        //    liRequerimentos.Attributes["Class"] = "hidden";
                        //    aRequerimentos.Attributes["href"] = "#";
                        //}
                        //else if (item.id_tela == 39)
                        //{ //Aprovação de Notas
                        //    liAcademico.Visible = true;
                        //    liAprovacaoNotas.Attributes["Class"] = "hidden";
                        //    aAprovacaoNotas.Attributes["href"] = "#";
                        //}
                        //else if (item.id_tela == 64)
                        //{ //Aprovação<br>Relatório Dissertação
                        //    liAcademico.Visible = true;
                        //    liAprovacaoRelDissertacao.Attributes["Class"] = "hidden";
                        //    aAprovacaoRelDissertacao.Attributes["href"] = "#";
                        //}
                        //else if (item.id_tela == 38)
                        //{ //Atribuição de Notas
                        //    liAcademico.Visible = true;
                        //    liAtribuicaoNotas.Attributes["Class"] = "hidden";
                        //    aAtribuicaoNotas.Attributes["href"] = "#";
                        //}
                        //else if (item.id_tela == 40)
                        //{ //Dados Cadastrais
                        //    liAcademico.Visible = true;
                        //    liDadosCadastraisProfessor.Attributes["Class"] = "hidden";
                        //    aDadosCadastraisProfessor.Attributes["href"] = "#";
                        //}

                        //Matrícula
                        else if (item.id_tela == 32)
                        { //1. Período de Matrícula
                            liMatricula.Visible = true;
                            liPeriodoMatricula.Visible = true;
                            aPeriodoMatricula.Attributes["href"] = "matPeriodoMatricula.aspx";
                        }
                        else if (item.id_tela == 55)
                        { //2. Confirmação de Oferecimento
                            liMatricula.Visible = true;
                            liConfirmacaoOferecimento.Visible = true;
                            aConfirmacaoOferecimento.Attributes["href"] = "matConfOferecimento.aspx";
                        }

                        //Financeiro =======================================================
                        else if (item.id_tela == 44)
                        { //1. Custos por Curso
                            liFinanceiro.Visible = true;
                            liCustosCurso.Visible = true;
                            aCustosCurso.Attributes["href"] = "finCustosCursos.aspx";
                        }
                        else if (item.id_tela == 7)   

                        { //2. Pessoa Jurídica
                            liFinanceiro.Visible = true;
                            liPessoaJuridica.Visible = true;
                            aPessoaJuridica.Attributes["href"] = "finPessoaJuridica.aspx";
                        }
                        else if (item.id_tela == 48)
                        { //3. Cálculo de Custos
                            liFinanceiro.Visible = true;
                            liCalculoCustos.Visible = true;
                            aCalculoCustos.Attributes["href"] = "finCalculoCustoCurso.aspx";
                        }
                        else if (item.id_tela == 54)
                        { //4. Extrato do Professor
                            liFinanceiro.Visible = true;
                            liExtratoProfessor.Visible = true;
                            aExtratoProfessor.Attributes["href"] = "finExtratoProfessor.aspx";
                        }
                        else if (item.id_tela == 58)
                        { //5. Solicitação Pagamento Professor
                            liFinanceiro.Visible = true;
                            liSolicitacaoPagamentoProfessor.Visible = true;
                            aSolicitacaoPagamentoProfessor.Attributes["href"] = "finSolicitacaoPagtoProfessor.aspx";
                        }
                        else if (item.id_tela == 63)
                        { //6. Data para Bloqueio 
                            liFinanceiro.Visible = true;
                            liDataBloqueio.Visible = true;
                            aDataBloqueio.Attributes["href"] = "finDataBloqueio.aspx";
                        }
                        else if (item.id_tela == 76)
                        { //Lista de Inadimplentes FITP
                            liFinanceiro.Visible = true;
                            liListaInadimplentesFIPT.Visible = true;
                            aListaInadimplentesFIPT.Attributes["href"] = "finListaInadimplenteFIPT.aspx";
                        }
                        else if (item.id_tela == 49)
                        { //Controle de Inadimplentes
                            liFinanceiro.Visible = true;
                            liControleInadimplentes.Visible = true;
                            aControleInadimplentes.Attributes["href"] = "finInadimplente.aspx";
                        }
                        else if (item.id_tela == 26)
                        { //Relatório de Boletos
                            liFinanceiro.Visible = true;
                            liRelatorioBoletos.Visible = true;
                            aRelatorioBoletos.Attributes["href"] = "finRelBoleto.aspx";
                        }

                        //Relatórios Gerenciais =======================================================
                        //else if (item.id_tela == 68)
                        //{ //Relatórios Gerenciais
                        //    liRelatoriosGerenciaisGrupo.Attributes["Class"] = "hidden";  //"treeview"
                        //    liRelatoriosGerenciais.Attributes["Class"] = "hidden";
                        //    aRelatoriosGerenciais.Attributes["href"] = "#";
                        //}

                        //Monitor =======================================================
                        else if (item.id_tela == 69)
                        { //Monitor Exibir
                            liMenuMonitorGrupo.Visible = true;
                            liMonitorExibir.Visible = true;
                            aMonitorExibir.Attributes["href"] = "tela.aspx";
                        }
                        else if (item.id_tela == 70)
                        { //Monitor Cadastro
                            liMenuMonitorGrupo.Visible = true;
                            liMonitorCadastro.Visible = true;
                            aMonitorCadastro.Attributes["href"] = "telaCadastro.aspx";
                        }
                        else if (item.id_tela == 71)
                        { //Monitor Cadastro
                            liMenuMonitorGrupo.Visible = true;
                            liMonitorVideo.Visible = true;
                            aMonitorVideo.Attributes["href"] = "telaVideo.aspx";
                        }
                        else if (item.id_tela == 72)
                        { //Monitor Cadastro
                            liMenuMonitorGrupo.Visible = true;
                            liMonitorLetreiro.Visible = true;
                            aMonitorLetreiro.Attributes["href"] = "telaLetreiro.aspx";
                        }
                        else if (item.id_tela == 79)
                        { //Monitor Cadastro
                            liBoletoMesCorrente.Visible = true;
                            liBoletoMesCorrente.Visible = true;
                            aBoletoMesCorrente.Attributes["href"] = "finBoletoMesCorrente.aspx";
                        }

                        //Diversos =======================================================
                        else if (item.id_tela == 75)
                        { //Diversos Certificados
                            liMenuOutrosGrupo.Visible = true;
                            liOutrosCertificado.Visible = true;
                            aOutrosCertificado.Attributes["href"] = "outCertificado.aspx";
                        }
                        else if (item.id_tela == 78)
                        { //Diversos Casdastro Automático de Alunos
                            liMenuOutrosGrupo.Visible = true;
                            liOutrosCadAutAluno.Visible = true;
                            aOutrosCadAutAluno.Attributes["href"] = "outCadAutAluno.aspx";
                        }
                    }

                }

                Session.Timeout = 60;
            }

        }

        protected void btnSair_Click(Object sender, EventArgs e )
        {
            Session.RemoveAll();
            Session.Abandon();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
            Response.Redirect("index.html");
        }

        protected void btnAlteraSenha_click(Object sender, EventArgs e)
        {
            usuarios usuario;
            usuarios item = new usuarios();

            usuario = (usuarios) Session["UsuarioLogado"];
            item.usuario = usuario.usuario;
            UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
            string sSenhaAtual;
            string sNovaSenha;

            if (usuario==null)
            {
                Response.Redirect("index.html", true);
            }

            SHA1CryptoServiceProvider objSHA1 = new SHA1CryptoServiceProvider();
            ASCIIEncoding objEncoding = new ASCIIEncoding();
            sSenhaAtual = Convert.ToBase64String(objSHA1.ComputeHash(objEncoding.GetBytes(txtSenhaAtual.Text.Trim())));
            sNovaSenha = Convert.ToBase64String(objSHA1.ComputeHash(objEncoding.GetBytes(txtNovaSenha.Text.Trim())));
            item.senha = sNovaSenha;


            if (usuario.senha != sSenhaAtual)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "javascript:AbreErroSenha('Senha Atual não confere. <br /> <br /> Digite novamente e tente de novo.');", true);
            }
            else if (txtNovaSenha.Text != txtConfirmaSenha.Text)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "javascript:AbreErroSenha('Nova Senha não confere com a Confirmação da Nova Senha. <br /> <br /> Digite novamente e tente de novo.');", true);
            }
            else if (txtNovaSenha.Text.Length < 4)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "javascript:AbreErroSenha('Nova Senha deve ter 4 ou mais caracteres. <br /> <br /> Digite novamente e tente de novo.');", true);
            }
            else if (aplicacaoUsuario.AlterarSenhaUsuario(item))
            {
                usuario.senha = sNovaSenha;
                Session["UsuarioLogado"] = usuario;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "javascript:AbreSucessoSenha();", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "javascript:AbreErroSenha('Houve um erro na alteração de senha. <br /> <br /> Confira os dados e tente novamente.');", true);
            }
        }

        public void PreencheSininho()
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            string sAux = "";

            //Inicializando Sininho
            id_sino_notificacao.Attributes.Remove("class");
            id_sino_notificacao.Attributes.Add("class", "fa fa-bell-o");

            id_EtiquetaNotificacao.Visible = false;

            lblQtdNotificao.Text = "0";
            lblDescricaoQtdNotififcacao.Text = "Não há notificações.";

            litMenuNotificacao.Text = Server.HtmlDecode(sAux);
            int iQtdSininho = 0;

            //======================

            //Preenchendo Sininho
            CursoAplicacao aplicacaocurso = new CursoAplicacao();
            TipoCursoAplicacao aplicacaoTipoCurso = new TipoCursoAplicacao();
            DocumentosAcademicosAplicacao aplicacaoDocumentoAcademico = new DocumentosAcademicosAplicacao();
            AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
            List<cursos> lista_curso = new List<cursos>();
            List<tipos_curso> lista_tipocurso = new List<tipos_curso>();
            List<documentos_academicos> lista_documentos_academicos = new List<documentos_academicos>();
            List<banca_dissertacao> lista_banca_dissertacao = new List<banca_dissertacao>();
            int qItem = 0;

            if (usuario.grupos_acesso.id_grupo == 8 || usuario.grupos_acesso.id_grupo == 1) //1= TI e 8=Gerencia
            {
                //Verifica se tem cursos aguardando aprovação
                lista_curso = aplicacaocurso.ListaItemAprovacaoHomePage();

                if (lista_curso.Count > 0)
                {
                    iQtdSininho = lista_curso.Count;
                    id_sino_notificacao.Attributes.Add("class", "fa fa-bell-o faa-shake animated");
                    id_EtiquetaNotificacao.Visible = true;
                    lblQtdNotificao.Text = iQtdSininho.ToString();

                    if (iQtdSininho == 1)
                    {
                        lblDescricaoQtdNotififcacao.Text = "Você tem 1 notificação.";
                    }
                    else
                    {
                        lblDescricaoQtdNotififcacao.Text = "Você tem " + iQtdSininho + " notificações.";
                    }

                    foreach (var elemento in lista_curso)
                    {
                        qItem++;
                        sAux = sAux + "<li id=\"li_sininho_" + qItem + "\">";
                        sAux = sAux + "    <a title=\"Aprovar a homepage do curso " + elemento.nome + "\" class=\"faa-parent animated-hover\" href = \"javascript:fAprovarHPCurso('" + elemento.id_curso + "')\" >";
                        sAux = sAux + "        <i title=\"Aprovar a homepage do curso " + elemento.nome + "\" class=\"fa fa-envelope faa-shake text-purple\"></i>Aprovar a hp do curso " + elemento.nome;
                        sAux = sAux + "    </a>";
                        sAux = sAux + "</li>";
                    }
                }

                //Verifica se tem Tipo de cursos aguardando aprovação
                lista_tipocurso = aplicacaoTipoCurso.ListaItemAprovacaoHomePage();

                if (lista_tipocurso.Count > 0)
                {
                    iQtdSininho = iQtdSininho + lista_tipocurso.Count;
                    id_sino_notificacao.Attributes.Add("class", "fa fa-bell-o faa-shake animated");
                    id_EtiquetaNotificacao.Visible = true;
                    lblQtdNotificao.Text = iQtdSininho.ToString();

                    if (iQtdSininho == 1)
                    {
                        lblDescricaoQtdNotififcacao.Text = "Você tem 1 notificação.";
                    }
                    else
                    {
                        lblDescricaoQtdNotififcacao.Text = "Você tem " + iQtdSininho + " notificações.";
                    }

                    foreach (var elemento in lista_tipocurso)
                    {
                        qItem++;
                        sAux = sAux + "<li id=\"li_sininho_" + qItem + "\">";
                        sAux = sAux + "    <a title=\"Aprovar a homepage do Tipo de curso " + elemento.tipo_curso + "\" class=\"faa-parent animated-hover\" href = \"javascript:fAprovarHPTipoCurso('" + elemento.id_tipo_curso + "')\" >";
                        sAux = sAux + "        <i title=\"Aprovar a homepage do Tipo de curso " + elemento.tipo_curso + "\" class=\"fa fa-envelope-o faa-shake text-purple\"></i>Aprovar a hp do Tipo de curso " + elemento.tipo_curso;
                        sAux = sAux + "    </a>";
                        sAux = sAux + "</li>";
                    }
                }

                //Verifica se tem Documento Acadêmico aguardando aprovação
                lista_documentos_academicos = aplicacaoDocumentoAcademico.ListaItemAprovacaoHomePage();

                if (lista_documentos_academicos.Count > 0)
                {
                    iQtdSininho = iQtdSininho + lista_documentos_academicos.Count;
                    id_sino_notificacao.Attributes.Add("class", "fa fa-bell-o faa-shake animated");
                    id_EtiquetaNotificacao.Visible = true;
                    lblQtdNotificao.Text = iQtdSininho.ToString();

                    if (iQtdSininho == 1)
                    {
                        lblDescricaoQtdNotififcacao.Text = "Você tem 1 notificação.";
                    }
                    else
                    {
                        lblDescricaoQtdNotififcacao.Text = "Você tem " + iQtdSininho + " notificações.";
                    }

                    foreach (var elemento in lista_documentos_academicos)
                    {
                        qItem++;
                        sAux = sAux + "<li id=\"li_sininho_" + qItem + "\">";
                        sAux = sAux + "    <a title=\"Aprovar Documento Acadêmico " + elemento.nomePreview + "\" class=\"faa-parent animated-hover\" href = \"javascript:fAprovarHPDocumentoAcademico('" + elemento.id_documentos_academicos + "')\" >";
                        sAux = sAux + "        <i title=\"Aprovar Documento Acadêmico " + elemento.nomePreview + "\" class=\"fa fa-envelope-o faa-shake text-info\"></i>Aprovar o Documento Acadêmico " + elemento.nomePreview;
                        sAux = sAux + "    </a>";
                        sAux = sAux + "</li>";
                    }


                }

                //Verifica se tem Documento Dissertações aguardando aprovação
                lista_banca_dissertacao = aplicacaoAluno.ListaItemAprovacaoHomePage();

                if (lista_banca_dissertacao.Count > 0)
                {
                    iQtdSininho = iQtdSininho + lista_banca_dissertacao.Count;
                    id_sino_notificacao.Attributes.Add("class", "fa fa-bell-o faa-shake animated");
                    id_EtiquetaNotificacao.Visible = true;
                    lblQtdNotificao.Text = iQtdSininho.ToString();

                    if (iQtdSininho == 1)
                    {
                        lblDescricaoQtdNotififcacao.Text = "Você tem 1 notificação.";
                    }
                    else
                    {
                        lblDescricaoQtdNotififcacao.Text = "Você tem " + iQtdSininho + " notificações.";
                    }

                    foreach (var elemento in lista_banca_dissertacao)
                    {
                        string qTipoDissertacao;
                        if (elemento.id_tipo_dissertacao == 1)
                        {
                            qTipoDissertacao = "Dissertação";
                        }
                        else
                        {
                            qTipoDissertacao = "Monografia";
                        }

                        qItem++;
                        sAux = sAux + "<li id=\"li_sininho_" + qItem + "\">";
                        sAux = sAux + "    <a title=\"Aprovar " + qTipoDissertacao + " Aluno: " + elemento.banca.matricula_turma.alunos.nome + "\" class=\"faa-parent animated-hover\" href = \"javascript:fAprovarHPDissertacao('" + elemento.banca.matricula_turma.id_aluno + "','" + elemento.banca.matricula_turma.id_turma + "','" + elemento.id_tipo_dissertacao + "')\" >";
                        sAux = sAux + "        <i title=\"Aprovar " + qTipoDissertacao + " Aluno: " + elemento.banca.matricula_turma.alunos.nome + "\" class=\"fa fa-envelope faa-shake text-success\"></i>Aprovar " + qTipoDissertacao + " Aluno: " + elemento.banca.matricula_turma.alunos.nome;
                        sAux = sAux + "    </a>";
                        sAux = sAux + "</li>";
                    }

                }

            }

//============================================================================================
            //Verifica se tem cursos Reprovado
            lista_curso = aplicacaocurso.ListaItemReprovacaoHomePage(usuario.usuario);

            if (lista_curso.Count > 0)
            {
                id_sino_notificacao.Attributes.Add("class", "fa fa-bell-o faa-shake animated");
                id_EtiquetaNotificacao.Visible = true;
                iQtdSininho = iQtdSininho + lista_curso.Count;
                lblQtdNotificao.Text = iQtdSininho.ToString();

                if (iQtdSininho == 1)
                {
                    lblDescricaoQtdNotififcacao.Text = "Você tem 1 notificação.";
                }
                else
                {
                    lblDescricaoQtdNotififcacao.Text = "Você tem " + iQtdSininho + " notificações.";
                }

                foreach (var elemento in lista_curso)
                {
                    qItem++;
                    sAux = sAux + "<li id=\"li_sininho_" + qItem + "\">";
                    sAux = sAux + "    <a title=\"Verificar a homepage do curso " + elemento.nome + "\" class=\"faa-parent animated-hover\" href = \"javascript:fAprovarHPCurso('" + elemento.id_curso + "')\" >";
                    sAux = sAux + "        <i title=\"Verificar a homepage do curso " + elemento.nome + "\" class=\"fa fa-envelope faa-shake text-red\"></i>Verificar a hp do curso " + elemento.nome;
                    sAux = sAux + "    </a>";
                    sAux = sAux + "</li>";
                }

            }

            //Verifica se tem Tipo de cursos Reprovado
            lista_tipocurso = aplicacaoTipoCurso.ListaItemReprovacaoHomePage(usuario.usuario);

            if (lista_tipocurso.Count > 0)
            {
                id_sino_notificacao.Attributes.Add("class", "fa fa-bell-o faa-shake animated");
                id_EtiquetaNotificacao.Visible = true;
                iQtdSininho = iQtdSininho + lista_tipocurso.Count;
                lblQtdNotificao.Text = iQtdSininho.ToString();

                if (iQtdSininho == 1)
                {
                    lblDescricaoQtdNotififcacao.Text = "Você tem 1 notificação.";
                }
                else
                {
                    lblDescricaoQtdNotififcacao.Text = "Você tem " + iQtdSininho + " notificações.";
                }

                foreach (var elemento in lista_tipocurso)
                {
                    qItem++;
                    sAux = sAux + "<li id=\"li_sininho_" + qItem + "\">";
                    sAux = sAux + "    <a title=\"Verificar a homepage do Tipo de curso " + elemento.tipo_curso + "\" class=\"faa-parent animated-hover\" href = \"javascript:fAprovarHPTipoCurso('" + elemento.id_tipo_curso + "')\" >";
                    sAux = sAux + "        <i title=\"Verificar a homepage do Tipo de curso " + elemento.tipo_curso + "\" class=\"fa fa-envelope-o faa-shake text-red\"></i>Verificar a hp do Tipo de curso " + elemento.tipo_curso;
                    sAux = sAux + "    </a>";
                    sAux = sAux + "</li>";
                }

            }

            //Verifica se tem Documento Acadêmico Reprovado
            lista_documentos_academicos = aplicacaoDocumentoAcademico.ListaItemReprovacaoHomePage(usuario.usuario);

            if (lista_documentos_academicos.Count > 0)
            {
                id_sino_notificacao.Attributes.Add("class", "fa fa-bell-o faa-shake animated");
                id_EtiquetaNotificacao.Visible = true;
                iQtdSininho = iQtdSininho + lista_documentos_academicos.Count;
                lblQtdNotificao.Text = iQtdSininho.ToString();

                if (iQtdSininho == 1)
                {
                    lblDescricaoQtdNotififcacao.Text = "Você tem 1 notificação.";
                }
                else
                {
                    lblDescricaoQtdNotififcacao.Text = "Você tem " + iQtdSininho + " notificações.";
                }

                foreach (var elemento in lista_documentos_academicos)
                {
                    qItem++;
                    sAux = sAux + "<li id=\"li_sininho_" + qItem + "\">";
                    sAux = sAux + "    <a title=\"Verificar Documento Acadêmico " + elemento.nomePreview + "\" class=\"faa-parent animated-hover\" href = \"javascript:fAprovarHPDocumentoAcademico('" + elemento.id_documentos_academicos + "')\" >";
                    sAux = sAux + "        <i title=\"Verificar o Documento Acadêmico " + elemento.nomePreview + "\" class=\"fa fa-envelope-o faa-shake text-red\"></i>Verificar o Documento Acadêmico " + elemento.nomePreview;
                    sAux = sAux + "    </a>";
                    sAux = sAux + "</li>";
                }

            }

            //Verifica se tem Dissertação Reprovada
            lista_banca_dissertacao = aplicacaoAluno.ListaItemReprovacaoHomePage(usuario.usuario);

            if (lista_banca_dissertacao.Count > 0)
            {
                id_sino_notificacao.Attributes.Add("class", "fa fa-bell-o faa-shake animated");
                id_EtiquetaNotificacao.Visible = true;
                iQtdSininho = iQtdSininho + lista_banca_dissertacao.Count;
                lblQtdNotificao.Text = iQtdSininho.ToString();

                if (iQtdSininho == 1)
                {
                    lblDescricaoQtdNotififcacao.Text = "Você tem 1 notificação.";
                }
                else
                {
                    lblDescricaoQtdNotififcacao.Text = "Você tem " + iQtdSininho + " notificações.";
                }

                foreach (var elemento in lista_banca_dissertacao)
                {
                    string qTipoDissertacao;
                    if (elemento.id_tipo_dissertacao == 1)
                    {
                        qTipoDissertacao = "Dissertação";
                    }
                    else
                    {
                        qTipoDissertacao = "Monografia";
                    }

                    qItem++;
                    sAux = sAux + "<li id=\"li_sininho_" + qItem + "\">";
                    sAux = sAux + "    <a title=\"Verificar " + qTipoDissertacao + " Aluno " + elemento.banca.matricula_turma.alunos.nome + "\" class=\"faa-parent animated-hover\" href = \"javascript:fAprovarHPDissertacao('" + elemento.banca.matricula_turma.id_aluno + "','" + elemento.banca.matricula_turma.id_turma + "','" + elemento.id_tipo_dissertacao + "')\" >";
                    sAux = sAux + "        <i title=\"Verificar " + qTipoDissertacao + " Aluno " + elemento.banca.matricula_turma.alunos.nome + "\" class=\"fa fa-envelope-o faa-shake text-success\"></i>Verificar " + qTipoDissertacao + " Aluno: " + elemento.banca.matricula_turma.alunos.nome;
                    sAux = sAux + "    </a>";
                    sAux = sAux + "</li>";
                }

            }

            litMenuNotificacao.Text = Server.HtmlDecode(sAux);
            //======================
        }
    }
}