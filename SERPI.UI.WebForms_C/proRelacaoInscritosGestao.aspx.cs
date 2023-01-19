using System;
using Aplicacao_C;
using SERPI.Dominio_C;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;
using System.IO;

namespace SERPI.UI.WebForms_C
{
    public partial class proRelacaoInscritosGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 5)) // Relatório de Inscritos - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                CarregarDados();

            }
            else
            {
                if (grdRelacaoInscritosGestao.Rows.Count != 0)
                {
                    grdRelacaoInscritosGestao.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        private void CarregarDados()
        {
            periodo_inscricao_curso item_periodo_inscricao_curso = (periodo_inscricao_curso)Session["periodo_inscricao_curso"];
            if (item_periodo_inscricao_curso != null)
            {
                InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
                var listaBoleto = item_periodo_inscricao_curso.fichas_inscricao.Select(x => x.boletos).ToList();

                boletos item_boleto;
                historico_inscricao item_historico;

                AreaAplicacao aplicacaoArea = new AreaAplicacao();
                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Configuracoes item_configuracoes;
                fichas_inscricao item_ficha;
                areas_concentracao item_area = new areas_concentracao();
                foreach (var lista2 in listaBoleto)
                {
                    foreach (var elemento in lista2)
                    {
                        if (elemento.data_pagamento == null)
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

                                elemento.data_pagamento = retorno.Item2;
                                elemento.data_alteracao = DateTime.Now;
                                elemento.usuario = "admin";

                                if (item_ficha != null)
                                {
                                    item_historico.id_inscricao = item_ficha.id_inscricao;
                                    item_historico.data = item_boleto.data_alteracao.Value;
                                    item_historico.status = "Inscrição Paga";
                                    item_historico.usuario = "admin";

                                    item_periodo_inscricao_curso.fichas_inscricao.Where(x => x.id_inscricao == item_ficha.id_inscricao).FirstOrDefault().historico_inscricao.Add(item_historico);

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
                                if (elemento.data_cancelamento == null)
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

                                        item_periodo_inscricao_curso.fichas_inscricao.Where(x => x.id_inscricao == item_ficha.id_inscricao).FirstOrDefault().historico_inscricao.Add(item_historico);

                                    }
                                    else
                                    {
                                        item_historico = null;
                                    }

                                    aplicacaoInscricao.IncluirDataPagamentoBoleto(item_boleto, item_historico);
                                }
                            }
                            else if (retorno.Item1 == "SemRegistro")
                            {
                                if (elemento.data_verificacao_sem_registro == null)
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

                                        item_periodo_inscricao_curso.fichas_inscricao.Where(x => x.id_inscricao == item_ficha.id_inscricao).FirstOrDefault().historico_inscricao.Add(item_historico);
                                    }
                                    else
                                    {
                                        item_historico = null;
                                    }
                                    aplicacaoInscricao.IncluirDataPagamentoBoleto(item_boleto, item_historico);
                                }
                            }
                            else
                            {
                                if (item_periodo_inscricao_curso.fichas_inscricao.Where(x => x.id_inscricao == elemento.fichas_inscricao.FirstOrDefault().id_inscricao).FirstOrDefault().historico_inscricao.Any(x=> x.status == "Sem Regsitro Gemini"))
                                {
                                    var historico = item_periodo_inscricao_curso.fichas_inscricao.Where(x => x.id_inscricao == elemento.fichas_inscricao.FirstOrDefault().id_inscricao).FirstOrDefault().historico_inscricao.Where(x => x.status == "Sem Regsitro Gemini").FirstOrDefault();
                                    item_periodo_inscricao_curso.fichas_inscricao.Where(x => x.id_inscricao == elemento.fichas_inscricao.FirstOrDefault().id_inscricao).FirstOrDefault().historico_inscricao.Remove(historico);
                                    aplicacaoInscricao.ExcluiHistorico_inscricao_Registro_Gemini(elemento.fichas_inscricao.FirstOrDefault().id_inscricao);
                                }
                            }
                        }

                    }
                }

                txtPeriodoInscricaoGestao.Value = item_periodo_inscricao_curso.periodo_inscricao.quadrimestre + " de " + String.Format("{0:dd/MM/yyyy}", item_periodo_inscricao_curso.periodo_inscricao.data_inicio) + " à " + String.Format("{0:dd/MM/yyyy}", item_periodo_inscricao_curso.periodo_inscricao.data_fim);
                txtCursoPeriodoInscricaoGestao.Value = item_periodo_inscricao_curso.cursos.nome;

                var lista = from item2 in item_periodo_inscricao_curso.cursos.turmas.Where(x=> x.ativo== true && x.data_limite_matricula != null && x.data_limite_matricula >= DateTime.Today).OrderByDescending(x => x.cod_turma)
                    select new
                    {
                        id_turma = item2.id_turma,
                        nome = Convert.ToString(item2.cod_turma) + " - " + Convert.ToString(item2.quadrimestre)
                    };

                ddlTurmaMatricula.Items.Clear();
                ddlTurmaMatricula.DataSource = lista;
                ddlTurmaMatricula.DataValueField = "id_turma";
                ddlTurmaMatricula.DataTextField = "Nome";
                ddlTurmaMatricula.DataBind();
                ddlTurmaMatricula.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione a Turma", ""));
                ddlTurmaMatricula.SelectedValue = "";

                subPreencheGrade(item_periodo_inscricao_curso);
            }
            
        }

        private void subPreencheGrade(periodo_inscricao_curso pItem)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            grdRelacaoInscritosGestao.DataSource = pItem.fichas_inscricao.ToList();
            grdRelacaoInscritosGestao.DataBind();

            if (pItem.fichas_inscricao.Count > 0)
            {
                grdRelacaoInscritosGestao.UseAccessibleHeader = true;
                grdRelacaoInscritosGestao.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultados.Visible = false;
                grdRelacaoInscritosGestao.Visible = true;

                if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 5).FirstOrDefault().modificacao == true)
                //Se não tiver direito desliga os botões de alteração/insersão
                {
                    grdRelacaoInscritosGestao.HeaderRow.Cells[11].CssClass = "hidden notexport";
                    grdRelacaoInscritosGestao.Columns[11].ItemStyle.CssClass = "hidden notexport";

                    grdRelacaoInscritosGestao.HeaderRow.Cells[12].CssClass = "hidden notexport";
                    grdRelacaoInscritosGestao.Columns[12].ItemStyle.CssClass = "hidden notexport";

                }

            }
            else
            {
                msgSemResultados.Visible = true;
            }
            divResultados.Visible = true;
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("proRelacaoInscritos.aspx", true);
        }

        protected void btnEditaInscritos_Click(object sender, EventArgs e)
        {
            int qIdCurso = Convert.ToInt32(HttpContext.Current.Request["hCodigo"]);
            List<periodo_inscricao_curso> lista = (List<periodo_inscricao_curso>)Session["lista_periodo_inscricao_curso"];
            periodo_inscricao_curso item_periodo_inscricao_curso = lista.Where(x => x.id_curso == qIdCurso).FirstOrDefault();
            Session["periodo_inscricao_curso"] = item_periodo_inscricao_curso;
            Response.Redirect("proRelacaoInscritosGestao.aspx", true);

        }

        //protected void grdResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Page")
        //    {
        //        return;
        //    }

        //    int linha = Convert.ToInt32(e.CommandArgument);
        //    int codigo = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0]);
        //    Professors_concentracao item = new Professors_concentracao();
        //    item.id_Professor_concentracao = codigo;
        //    switch (grdResultado.DataKeys[linha].Values[1].ToString())
        //    {
        //        case "Editar":
        //            ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
        //            item = aplicacaoProfessor.BuscaItem(item);
        //            Session.Add("Professors_concentracao", item);
        //            Session.Add("sNewProfessor", false);
        //            Response.Redirect("cadProfessorConcentracaoGestao.aspx", true);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        public void grdPeriodoConfirmacaoMatricula_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                int linha = Convert.ToInt32(e.CommandArgument);
                int codigo = Convert.ToInt32(grdRelacaoInscritosGestao.DataKeys[linha].Values[0]);
                pre_oferecimentos item = new pre_oferecimentos();
                MatriculaAplicacao aplicacaoMatricula = new MatriculaAplicacao();
                item.id_pre_oferecimento = codigo;
                item = aplicacaoMatricula.BuscaPreOferecimento(item);
                Session["pre_oferecimentos"] = item;
                Response.Redirect("matConfOferecimentoGestao.aspx", true);
            }
        }

        public string setRg(string qRg, string qIdInscricao)
        {
            string sAux = qRg;

            InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
            fichas_inscricao item_fichas_inscricao = new fichas_inscricao();
            item_fichas_inscricao.id_inscricao = Convert.ToInt32(qIdInscricao);
            item_fichas_inscricao = aplicacaoInscricao.BuscaItem_Inscricao(item_fichas_inscricao);

            if (item_fichas_inscricao.digito_rg != null && item_fichas_inscricao.digito_rg != "")
            {
                sAux = sAux + "-" + item_fichas_inscricao.digito_rg;
            }

            return sAux;
        }

        public string setAreaConcentracao(int qIdArea)
        {
            string sAux = "";

            if (qIdArea != null  && qIdArea !=0)
            {
                AreaAplicacao aplicacaoArea = new AreaAplicacao();
                areas_concentracao item_area = new areas_concentracao();
                item_area.id_area_concentracao = qIdArea;
                item_area = aplicacaoArea.BuscaItem(item_area);
                sAux = item_area.nome;

            }

            return sAux;
        }

        public string setStatusAtual(object qTabela)
        {

            string sAux = "";
            HashSet<historico_inscricao> lista = (HashSet<historico_inscricao>)qTabela;
            if (lista.Count > 0)
            {
                sAux = lista.Where(x => x.status != "Matriculado").OrderByDescending(x => x.data).FirstOrDefault().status;
            }
           
            return sAux;
        }

        public string setStatusMatriculado(object qTabela)
        {

            string sAux = "";
            HashSet<historico_inscricao> lista = (HashSet<historico_inscricao>)qTabela;
            if (lista.Any(x=> x.status== "Matriculado"))
            {
                sAux = "Sim";
            }
            else
            {
                sAux = "Não";
            }
            //sAux = lista.OrderByDescending(x => x.data).FirstOrDefault().status;
            return sAux;
        }

        public string setBotaoVisualizar(int qIdInscricao, string qNome)
        {
            string sAux = "";

            sAux = "<div title=\"Visualizar\"> <a class=\"btn btn-default btn-circle fa fa-search\" href=\'javascript:fVisualizarInscrito(\""
                        + qIdInscricao.ToString() + "\",\""
                        + qNome + "\")\'; ></a></div>";

            return sAux;
        }

        public string setBotaoMatricular(object qTabela, int qIdInscricao, string qNome, int qIdArea )
        {
            string sAux = "";

            HashSet<historico_inscricao> lista = (HashSet<historico_inscricao>)qTabela;
            //if (lista.OrderByDescending(x => x.data).FirstOrDefault().status != "Matriculado")
            if (!lista.Any(x => x.status == "Matriculado"))
                {
                if (qIdArea != null && qIdArea != 0)
                {
                    AreaAplicacao aplicacaoArea = new AreaAplicacao();
                    areas_concentracao item_area = new areas_concentracao();
                    item_area.id_area_concentracao = qIdArea;
                    item_area = aplicacaoArea.BuscaItem(item_area);
                    sAux = item_area.nome;

                }

                sAux = "<div title=\"Matricular na Turma\"> <a class=\"btn btn-primary btn-circle fa fa-edit\" href=\'javascript:fMatricularInscrito(\""
                        + qIdInscricao.ToString() + "\",\""
                        + qNome + "\",\""
                        + sAux + "\")\'; ></a></div>";
            };

            return sAux;
        }

        public string setBotaoExcluir(int qIdInscricao, string qNome)
        {
            string sAux = "";

            sAux = "<div title=\"Excluir Inscrição\"> <a class=\"btn btn-danger btn-circle fa fa-eraser\" href=\'javascript:fExcluirInscrito(\""
                        + qIdInscricao.ToString() + "\",\""
                        + qNome + "\")\'; ></a></div>";

            return sAux;
        }

        protected void btnMatricular_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            string sAux = "";
            if (ddlTurmaMatricula.SelectedValue == "")
            {
                sAux = "Deve-se selecionar uma Turma <br><br>";
            }

            if (ddlStatusMatricula.SelectedValue == "") {
                sAux = sAux + "Deve-se selecionar um Status <br><br>";
            }

            if (sAux != "")
            {
                lblMensagem.Text = sAux;
                lblTituloMensagem.Text = "Atenção";
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:AbreModalMensagem('alert-warning');", true);
                return;
            }

            int qIdInscricao = Convert.ToInt32(HttpContext.Current.Request.Form["hCodigo"]);

            InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
            fichas_inscricao item_fichas_inscricao = new fichas_inscricao();
            item_fichas_inscricao.id_inscricao = qIdInscricao;
            item_fichas_inscricao = aplicacaoInscricao.BuscaItem_Inscricao(item_fichas_inscricao);

            AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
            alunos item_aluno = new alunos();
            item_aluno.cpf = Convert.ToUInt64(item_fichas_inscricao.cpf).ToString(@"000\.000\.000\-00");
            item_aluno = aplicacaoAluno.BuscaItem(item_aluno.cpf);

            if (item_aluno == null)
            {
                //Incluir o aluno
                item_aluno = new alunos();
                item_aluno.cpf = Convert.ToUInt64(item_fichas_inscricao.cpf).ToString(@"000\.000\.000\-00");
                item_aluno.nome = item_fichas_inscricao.nome;
                item_aluno.data_nascimento = item_fichas_inscricao.data_nascimento;
                item_aluno.sexo = item_fichas_inscricao.sexo.ToUpper();
                item_aluno.cep_res = Convert.ToUInt64(item_fichas_inscricao.cep_res).ToString(@"00000\-000");
                item_aluno.logradouro_res = item_fichas_inscricao.endereco_res;
                item_aluno.numero_res = item_fichas_inscricao.numero_res;
                item_aluno.complemento_res = item_fichas_inscricao.complemento_res;
                item_aluno.bairro_res = item_fichas_inscricao.bairro_res;
                item_aluno.cidade_res = item_fichas_inscricao.cidade_res;
                item_aluno.uf_res = item_fichas_inscricao.estado_res;
                item_aluno.email = item_fichas_inscricao.email_res;
                item_aluno.numero_documento = item_fichas_inscricao.rg_rne;
                item_aluno.digito_num_documento = item_fichas_inscricao.digito_rg;
                if (item_fichas_inscricao.telefone_res != null & item_fichas_inscricao.telefone_res != "")
                {
                    item_aluno.telefone_res = Convert.ToUInt64(item_fichas_inscricao.telefone_res).ToString(@"00\-0000\-0000");
                }
                item_aluno.celular_res = Convert.ToUInt64(item_fichas_inscricao.celular_res).ToString(@"00\-00000\-0000");

                item_aluno.cidade_nasc = "";
                item_aluno.estado_nasc = "";
                item_aluno.pais_nasc = "";
                item_aluno.pais_empresa = "";
                item_aluno.cidade_empresa = "";
                item_aluno.data_cadastro = DateTime.Now;
                item_aluno.data_alteracao = DateTime.Now;
                item_aluno.usuario = usuario.usuario;

                item_aluno = aplicacaoAluno.CriarItem(item_aluno);

                //Incluir o usuario
                SHA1CryptoServiceProvider objSHA1 = new SHA1CryptoServiceProvider();
                ASCIIEncoding objEncoding = new ASCIIEncoding();
                UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                usuarios item_usuario = new usuarios();
                item_usuario.usuario = item_aluno.idaluno.ToString();
                item_usuario.nome = item_aluno.nome;
                item_usuario.un = "Acadêmico";
                item_usuario.email = item_aluno.email;
                item_usuario.id_grupo_acesso = 2;
                item_usuario.status = 1;
                item_usuario.avatar = "";
                if (item_aluno.nome.IndexOf (" ") != -1)
                {
                    item_usuario.nomeSocial = item_aluno.nome.Substring(0, item_aluno.nome.IndexOf(" "));
                }
                else
                {
                    item_usuario.nomeSocial = item_aluno.nome;
                }
                string sAuxSenha;
                sAuxSenha = item_aluno.cpf.Replace(".", "").Substring(0, 6);
                item_usuario.senha = Convert.ToBase64String(objSHA1.ComputeHash(objEncoding.GetBytes(sAuxSenha)));

                aplicacaoUsuario.CriarUsuario(item_usuario);

            }

            //Incluir o aluno na turma

            TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
            turmas item_turma = new turmas();
            item_turma.id_turma = Convert.ToInt32(ddlTurmaMatricula.SelectedValue);
            item_turma = aplicacaoTurma.BuscaItem(item_turma);
            matricula_turma pItemMatricula = new matricula_turma();
            historico_matricula_turma pItemHistorico = new historico_matricula_turma();

            pItemMatricula.id_turma = item_turma.id_turma;
            pItemMatricula.id_aluno = item_aluno.idaluno;
            if (item_fichas_inscricao.id_area_concentracao != 0 && item_fichas_inscricao.id_area_concentracao != null)
            {
                pItemMatricula.id_area_concentracao = item_fichas_inscricao.id_area_concentracao;
            }
            pItemMatricula.situacao = "Matriculado";
            pItemMatricula.data_termino = item_turma.data_fim;
            pItemMatricula.data_cadastro = DateTime.Now;
            pItemMatricula.data_alteracao = pItemMatricula.data_cadastro;
            pItemMatricula.usuario = usuario.usuario;
            pItemMatricula = aplicacaoTurma.IncluirAluno_Turma(pItemMatricula);

            //Incluir o historico da inclusão do aluno na turma
            pItemHistorico.data_inicio = item_turma.data_inicio;
            pItemHistorico.data_fim = item_turma.data_fim;
            pItemHistorico.data = pItemMatricula.data_cadastro;
            pItemHistorico.usuario = usuario.usuario;
            pItemHistorico.situacao = "Matriculado";
            pItemHistorico.status = ddlStatusMatricula.SelectedValue;
            pItemHistorico.ordem = 1;
            pItemHistorico.id_matricula_turma = pItemMatricula.id_matricula_turma;
            pItemHistorico = aplicacaoTurma.IncluirHistorico_Matricula(pItemHistorico);

            //Incluir no historico de inscrição
            historico_inscricao item_historico_inscricao = new historico_inscricao();
            item_historico_inscricao.id_inscricao = item_fichas_inscricao.id_inscricao;
            item_historico_inscricao.data = DateTime.Now;
            item_historico_inscricao.status = "Matriculado";
            item_historico_inscricao.usuario = usuario.usuario;
            item_historico_inscricao = aplicacaoInscricao.CriarHistorico(item_historico_inscricao);

            periodo_inscricao_curso item_periodo_inscricao_curso = (periodo_inscricao_curso)Session["periodo_inscricao_curso"];

            item_periodo_inscricao_curso.fichas_inscricao.Where(x => x.id_inscricao == item_fichas_inscricao.id_inscricao).FirstOrDefault().historico_inscricao.Add(item_historico_inscricao);

            Session["periodo_inscricao_curso"] = item_periodo_inscricao_curso;

            subPreencheGrade(item_periodo_inscricao_curso);

            if (item_aluno.sexo.ToUpper() == "M")
            {
                sAux = "O Candidato <strong>" + item_aluno.nome + "</strong> foi Matriculado com Sucesso.<br><br>Matrícula do Aluno: <span class=\"text-red\"><strong>" + item_aluno.idaluno.ToString() + "</strong></span>";
            }
            else
            {
                sAux = "A Candidata <strong>" + item_aluno.nome + "</strong> foi Matriculada com Sucesso.<br><br>Matrícula da Aluna: <span class=\"text-red\"><strong>" + item_aluno.idaluno.ToString() + "</strong></span>";
            }

            lblMensagem.Text = sAux;
            lblTituloMensagem.Text = "Matrícula Efetuada";
            
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fConfiguraGrade();javascript:$('#divModalMatricular').modal('hide');javascript:AbreModalMensagem('alert-success');", true);
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            string sAux = "";
            
            int qIdInscricao = Convert.ToInt32(HttpContext.Current.Request.Form["hCodigo"]);

            InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
            aplicacaoInscricao.ExcluiInscricao(qIdInscricao);

            periodo_inscricao_curso item_periodo_inscricao_curso = (periodo_inscricao_curso)Session["periodo_inscricao_curso"];

            item_periodo_inscricao_curso = aplicacaoInscricao.BuscaItem_periodo_inscricao_curso(item_periodo_inscricao_curso);

            Session["periodo_inscricao_curso"] = item_periodo_inscricao_curso;

            subPreencheGrade(item_periodo_inscricao_curso);

             sAux = "A Inscrição do Candidato foi excluída com sucesso.";

            lblMensagem.Text = sAux;
            lblTituloMensagem.Text = "Inscrição Excluída";

            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fConfiguraGrade();javascript:$('#divModalExcluirInscrito').modal('hide');javascript:AbreModalMensagem('alert-success');", true);
        }

        public class GridAux
        {

            private string _P1;
            private string _P2;
            private string _P3;
            private string _P4;
            private string _P5;
            private string _P6;
            private string _P7;
            private string _P8;
            private string _P9;
            private string _P10;
            private string _P11;
            private string _P12;
            private string _P13;
            private string _P14;
            private string _P15;

            public string P1
            {
                get
                {
                    return _P1;
                }
                set
                {
                    _P1 = value;
                }
            }

            public string P2
            {
                get
                {
                    return _P2;
                }
                set
                {
                    _P2 = value;
                }
            }

            public string P3
            {
                get
                {
                    return _P3;
                }
                set
                {
                    _P3 = value;
                }
            }

            public string P4
            {
                get
                {
                    return _P4;
                }
                set
                {
                    _P4 = value;
                }
            }

            public string P5
            {
                get
                {
                    return _P5;
                }
                set
                {
                    _P5 = value;
                }
            }

            public string P6
            {
                get
                {
                    return _P6;
                }
                set
                {
                    _P6 = value;
                }
            }

            public string P7
            {
                get
                {
                    return _P7;
                }
                set
                {
                    _P7 = value;
                }
            }

            public string P8
            {
                get
                {
                    return _P8;
                }
                set
                {
                    _P8 = value;
                }
            }

            public string P9
            {
                get
                {
                    return _P9;
                }
                set
                {
                    _P9 = value;
                }
            }

            public string P10
            {
                get
                {
                    return _P10;
                }
                set
                {
                    _P10 = value;
                }
            }

            public string P11
            {
                get
                {
                    return _P11;
                }
                set
                {
                    _P11 = value;
                }
            }

            public string P12
            {
                get
                {
                    return _P12;
                }
                set
                {
                    _P12 = value;
                }
            }

            public string P13
            {
                get
                {
                    return _P13;
                }
                set
                {
                    _P13 = value;
                }
            }

            public string P14
            {
                get
                {
                    return _P14;
                }
                set
                {
                    _P14 = value;
                }
            }

            public string P15
            {
                get
                {
                    return _P15;
                }
                set
                {
                    _P15 = value;
                }
            }
        }
    }
}