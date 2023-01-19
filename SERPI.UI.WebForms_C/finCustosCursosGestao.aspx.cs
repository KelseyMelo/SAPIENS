
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

namespace SERPI.UI.WebForms_C
{
    public partial class finCustosCursosGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 44)) // 1. Custos por Curso - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                cursos item_fin_cursos = (cursos)Session["fin_cursos"];

                if (item_fin_cursos != null)
                {
                    txtTipoCursoGestao.Value = item_fin_cursos.tipos_curso.tipo_curso;
                    txtCodigoCursoGestao.Value = item_fin_cursos.sigla;
                    txtNomeCursoGestao.Value = item_fin_cursos.nome;

                    CarregarDados();
                }
                else
                {
                    Response.Redirect("finCustosCursos.aspx", true);
                }
                
            }
            else
            {
                if (grdValoresHoraAula.Rows.Count != 0)
                {
                    grdValoresHoraAula.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

                if (grdValoresBancas.Rows.Count != 0)
                {
                    grdValoresBancas.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

                if (grdValoresOrientacao.Rows.Count != 0)
                {
                    grdValoresOrientacao.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

                if (grdValoresCoordenacao.Rows.Count != 0)
                {
                    grdValoresCoordenacao.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        private void CarregarDados()
        {
            cursos item_fin_cursos = (cursos)Session["fin_cursos"];

            GeraisAplicacao aplicacaoGeral = new GeraisAplicacao();
            List<forma_recebimento> lista_forma = aplicacaoGeral.ListaFormaRecebimento_HoraAula();
            List<GridAux> lista_grade = new List<GridAux>();
            GridAux itemGrade;
            foreach (var elemento in lista_forma.Where(x=> x.id_forma_recebimento > 1))
            {
                itemGrade = new GridAux();

                itemGrade.P1 = elemento.id_forma_recebimento.ToString();
                itemGrade.P2 = elemento.nome;
                if (item_fin_cursos.curso_valor_hora_aula.Any(x=> x.id_forma_recebimento == elemento.id_forma_recebimento && x.id_titulacao == 1))
                {
                    itemGrade.P3 = string.Format("{0:C}", item_fin_cursos.curso_valor_hora_aula.Where(x => x.id_forma_recebimento == elemento.id_forma_recebimento && x.id_titulacao == 1).FirstOrDefault().valor).Replace("R$", "");
                    //itemGrade.P3 = string.Format("{0:C}", itemGrade.P3).Replace("R$ ", "");
                }
                else
                {
                    itemGrade.P3 = "0,00";
                }
                if (item_fin_cursos.curso_valor_hora_aula.Any(x => x.id_forma_recebimento == elemento.id_forma_recebimento && x.id_titulacao == 2))
                {
                    itemGrade.P4 = string.Format("{0:C}", item_fin_cursos.curso_valor_hora_aula.Where(x => x.id_forma_recebimento == elemento.id_forma_recebimento && x.id_titulacao == 2).FirstOrDefault().valor).Replace("R$", "");
                    //itemGrade.P4 = string.Format("{0:C}", itemGrade.P4).Replace("R$ ", "");
                }
                else
                {
                    itemGrade.P4 = "0,00";
                }
                if (item_fin_cursos.curso_valor_hora_aula.Any(x => x.id_forma_recebimento == elemento.id_forma_recebimento && x.id_titulacao == 3))
                {
                    itemGrade.P5 = string.Format("{0:C}", item_fin_cursos.curso_valor_hora_aula.Where(x => x.id_forma_recebimento == elemento.id_forma_recebimento && x.id_titulacao == 3).FirstOrDefault().valor).Replace("R$", "");
                    //itemGrade.P5 = string.Format("{0:C}", itemGrade.P5).Replace("R$ ", "");
                }
                else
                {
                    itemGrade.P5 = "0,00";
                }
                if (item_fin_cursos.curso_valor_hora_aula.Any(x => x.id_forma_recebimento == elemento.id_forma_recebimento && x.id_titulacao == 4))
                {
                    itemGrade.P6 = string.Format("{0:C}", item_fin_cursos.curso_valor_hora_aula.Where(x => x.id_forma_recebimento == elemento.id_forma_recebimento && x.id_titulacao == 4).FirstOrDefault().valor).Replace("R$", "");
                    //itemGrade.P6 = string.Format("{0:C}", itemGrade.P6).Replace("R$ ", "");
                }
                else
                {
                    itemGrade.P6 = "0,00";
                }

                if (item_fin_cursos.curso_valor_hora_aula.Any(x => x.id_forma_recebimento == elemento.id_forma_recebimento && x.id_titulacao == 5))
                {
                    itemGrade.P8 = string.Format("{0:C}", item_fin_cursos.curso_valor_hora_aula.Where(x => x.id_forma_recebimento == elemento.id_forma_recebimento && x.id_titulacao == 5).FirstOrDefault().valor).Replace("R$", "");
                    //itemGrade.P6 = string.Format("{0:C}", itemGrade.P6).Replace("R$ ", "");
                }
                else
                {
                    itemGrade.P8 = "0,00";
                }

                itemGrade.P7 = "<div title=\"Editar valores\"> <a class=\"fa fa-edit btn btn-circle btn-primary classEditaHoraAula\" type=\"button\"></a></div>";

                lista_grade.Add(itemGrade);
            }

            grdValoresHoraAula.DataSource = lista_grade;
            grdValoresHoraAula.DataBind();

            if (lista_grade.Count > 0)
            {
                grdValoresHoraAula.UseAccessibleHeader = true;
                grdValoresHoraAula.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdValoresHoraAula.Visible = true;
            }

            //=================================================

            List<curso_valor_banca> lista_curso_valor_banca = aplicacaoGeral.Lista_curso_valor_banca(item_fin_cursos.id_curso);
            itemGrade = new GridAux();
            lista_grade = new List<GridAux>();
            if (lista_curso_valor_banca.Count > 0)
            {
                itemGrade.P1 = lista_curso_valor_banca.ElementAt(0).id_curso.ToString();
                itemGrade.P2 = "Grande São Paulo";
                itemGrade.P3 = string.Format("{0:C}", lista_curso_valor_banca.ElementAt(0).valor_sao_paulo).Replace("R$", "");
                itemGrade.P4 = "<div title=\"Editar valores\"> <a class=\"fa fa-edit btn btn-circle btn-primary classEditaValorBanca\" type=\"button\"></a></div>";
                lista_grade.Add(itemGrade);

                itemGrade = new GridAux();
                itemGrade.P1 = lista_curso_valor_banca.ElementAt(0).id_curso.ToString();
                itemGrade.P2 = "Interior e Outros Estados";
                itemGrade.P3 = string.Format("{0:C}", lista_curso_valor_banca.ElementAt(0).valor_fora_sao_paulo).Replace("R$", "");
                itemGrade.P4 = "<div title=\"Editar valores\"> <a class=\"fa fa-edit btn btn-circle btn-primary classEditaValorBanca\" type=\"button\"></a></div>";
                lista_grade.Add(itemGrade);

            }
            else
            {
                itemGrade.P1 = "";
                itemGrade.P2 = "Grande São Paulo";
                itemGrade.P3 = "0,00";
                itemGrade.P4 = "<div title=\"Editar valores\"> <a class=\"fa fa-edit btn btn-circle btn-primary classEditaValorBanca\" type=\"button\"></a></div>";
                lista_grade.Add(itemGrade);

                itemGrade = new GridAux();
                itemGrade.P1 = "";
                itemGrade.P2 = "Interior e Outros Estados";
                itemGrade.P3 = "0,00";
                itemGrade.P4 = "<div title=\"Editar valores\"> <a class=\"fa fa-edit btn btn-circle btn-primary classEditaValorBanca\" type=\"button\"></a></div>";
                lista_grade.Add(itemGrade);
            }    
            
            grdValoresBancas.DataSource = lista_grade;
            grdValoresBancas.DataBind();

            if (lista_grade.Count > 0)
            {
                grdValoresHoraAula.UseAccessibleHeader = true;
                grdValoresHoraAula.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdValoresHoraAula.Visible = true;
            }

            //========================================================================================

            lista_forma = aplicacaoGeral.ListaFormaRecebimento_ValoresOrientacao();
            lista_grade = new List<GridAux>();

            foreach (var elemento in lista_forma.Where(x => x.id_forma_recebimento > 1))
            {
                itemGrade = new GridAux();

                itemGrade.P1 = elemento.id_forma_recebimento.ToString();
                itemGrade.P2 = elemento.nome;
                if (item_fin_cursos.curso_valor_orientacao.Any(x => x.id_forma_recebimento == elemento.id_forma_recebimento))
                {
                    itemGrade.P3 = string.Format("{0:C}", item_fin_cursos.curso_valor_orientacao.Where(x => x.id_forma_recebimento == elemento.id_forma_recebimento).FirstOrDefault().valor_qualificacao).Replace("R$", "");
                    itemGrade.P4 = string.Format("{0:C}", item_fin_cursos.curso_valor_orientacao.Where(x => x.id_forma_recebimento == elemento.id_forma_recebimento).FirstOrDefault().valor_defesa).Replace("R$", "");
                    //itemGrade.P3 = string.Format("{0:C}", itemGrade.P3).Replace("R$ ", "");
                }
                else
                {
                    itemGrade.P3 = "0,00";
                    itemGrade.P4 = "0,00";
                }

                itemGrade.P5 = "<div title=\"Editar valores\"> <a class=\"fa fa-edit btn btn-circle btn-primary classEditaValoresOrientacao\" type=\"button\"></a></div>";

                lista_grade.Add(itemGrade);
            }

            grdValoresOrientacao.DataSource = lista_grade;
            grdValoresOrientacao.DataBind();

            if (lista_grade.Count > 0)
            {
                grdValoresOrientacao.UseAccessibleHeader = true;
                grdValoresOrientacao.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdValoresOrientacao.Visible = true;
            }

            //=================================================
            //=================================================

            List<curso_valor_coordenacao> lista_curso_valor_coordenador = aplicacaoGeral.Lista_curso_valor_coordenacao(item_fin_cursos.id_curso);
            itemGrade = new GridAux();
            lista_grade = new List<GridAux>();

            List<curso_tipo_coordenador> lista_curso_tipo_coordenador = aplicacaoGeral.Lista_curso_tipo_coordenador();
            foreach (var elemento in lista_curso_tipo_coordenador)
            {
                itemGrade = new GridAux();
                itemGrade.P1 = elemento.id_tipo_coordenador.ToString();
                itemGrade.P2 = elemento.descricao;
                if (lista_curso_valor_coordenador.Any(x => x.id_tipo_coordenador == elemento.id_tipo_coordenador))
                {
                    var sAux = lista_curso_valor_coordenador.Where(x => x.id_tipo_coordenador == elemento.id_tipo_coordenador).FirstOrDefault().valor;
                    itemGrade.P3 = string.Format("{0:C}", sAux).Replace("R$", "");
                }
                else
                {
                    itemGrade.P3 = "0,00";
                }
                itemGrade.P4 = "<div title=\"Editar valores\"> <a class=\"fa fa-edit btn btn-circle btn-primary classEditaValorCoordenacao\" type=\"button\"></a></div>";
                lista_grade.Add(itemGrade);
            }

            //if (lista_curso_valor_coordenador.Count > 0)
            //{
            //    foreach (var elemento in lista_curso_valor_coordenador)
            //    {
            //        itemGrade = new GridAux();
            //        itemGrade.P1 = elemento.id_tipo_coordenador.ToString();
            //        itemGrade.P2 = elemento.curso_tipo_coordenador.descricao;
            //        itemGrade.P3 = string.Format("{0:C}", elemento.valor).Replace("R$", "");
            //        itemGrade.P4 = "<div title=\"Editar valores\"> <a class=\"fa fa-edit btn btn-circle btn-primary classEditaValorCoordenacao\" type=\"button\"></a></div>";
            //        lista_grade.Add(itemGrade);
            //    }
            //}
            //else
            //{
            //    lista_curso_tipo_coordenador = aplicacaoGeral.Lista_curso_tipo_coordenador();
            //    foreach (var elemento in lista_curso_tipo_coordenador)
            //    {
            //        itemGrade = new GridAux();
            //        itemGrade.P1 = elemento.id_tipo_coordenador.ToString();
            //        itemGrade.P2 = elemento.descricao;
            //        if (lista_curso_valor_coordenador.Any(x=> x.id_tipo_coordenador == elemento.id_tipo_coordenador))
            //        {
            //            var sAux = lista_curso_valor_coordenador.Where(x => x.id_tipo_coordenador == elemento.id_tipo_coordenador).Select(x => x.valor);
            //            itemGrade.P3 = string.Format("{0:C}", sAux).Replace("R$", "");
            //        }
            //        else
            //        {
            //            itemGrade.P3 = "0,00";
            //        }
            //        itemGrade.P4 = "<div title=\"Editar valores\"> <a class=\"fa fa-edit btn btn-circle btn-primary classEditaValorCoordenacao\" type=\"button\"></a></div>";
            //        lista_grade.Add(itemGrade);
            //    }

            //}

            grdValoresCoordenacao.DataSource = lista_grade;
            grdValoresCoordenacao.DataBind();

            if (lista_grade.Count > 0)
            {
                grdValoresCoordenacao.UseAccessibleHeader = true;
                grdValoresCoordenacao.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdValoresCoordenacao.Visible = true;
            }

            //========================================================================================
        }



        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("finCustosCursos.aspx", true);
        }

        protected void btnSalvarDados_Click(object sender, EventArgs e)
        {
            var qRows = HttpContext.Current.Request["h_grdValoresHoraAula"];
            var qRowsValoresBancas = HttpContext.Current.Request["h_grdValoresBancas"];
            var qRowsValoresOrientacao = HttpContext.Current.Request["h_grdValoresOrientacao"];
            var qRowsValoresCoordenacao = HttpContext.Current.Request["h_grdValoresCoordenacao"];

            cursos item_fin_cursos = (cursos)Session["fin_cursos"];

            GeraisAplicacao aplicacaoGeral = new GeraisAplicacao();
            curso_valor_hora_aula item_curso_valor;

            if (qRows != "")
            {
                var qRow = qRows.Split(new string[] { "||" }, StringSplitOptions.None);

                foreach (var elemento in qRow)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        var qCol = elemento.Split(new string[] { "~~" }, StringSplitOptions.None);
                        item_curso_valor = new curso_valor_hora_aula();
                        item_curso_valor.id_curso = item_fin_cursos.id_curso;
                        item_curso_valor.id_forma_recebimento = Convert.ToInt32(qCol[0]);
                        item_curso_valor.id_titulacao = i + 1;
                        item_curso_valor.valor = Convert.ToDecimal(qCol[i+1]);
                        aplicacaoGeral.Altera_curso_valor_hora_aula(item_curso_valor);

                    }
                }

            }

            //===============================================================

            curso_valor_banca item_curso_valor_banca;
            item_curso_valor_banca = new curso_valor_banca();
            if (qRowsValoresBancas != "")
            {
                var qRowValoresBancas = qRowsValoresBancas.Split(new string[] { "||" }, StringSplitOptions.None);

                int k = 0;
                foreach (var elemento in qRowValoresBancas)
                {
                    k++;
                    var qCol = elemento.Split(new string[] { "~~" }, StringSplitOptions.None);
                    item_curso_valor_banca.id_curso = item_fin_cursos.id_curso;
                    if (k==1)
                    {
                        item_curso_valor_banca.valor_sao_paulo = Convert.ToDecimal(qCol[1]);
                    }
                    else
                    {
                        item_curso_valor_banca.valor_fora_sao_paulo = Convert.ToDecimal(qCol[1]);
                    }
                }
                aplicacaoGeral.Altera_curso_valor_banca(item_curso_valor_banca);
            }

            //===============================================================

            curso_valor_orientacao item_curso_valor_orientacao;

            if (qRowsValoresOrientacao != "")
            {
                var qRow = qRowsValoresOrientacao.Split(new string[] { "||" }, StringSplitOptions.None);

                foreach (var elemento in qRow)
                {
                    var qCol = elemento.Split(new string[] { "~~" }, StringSplitOptions.None);
                    item_curso_valor_orientacao = new curso_valor_orientacao();
                    item_curso_valor_orientacao.id_curso = item_fin_cursos.id_curso;
                    item_curso_valor_orientacao.id_forma_recebimento = Convert.ToInt32(qCol[0]);
                    item_curso_valor_orientacao.valor_qualificacao = Convert.ToDecimal(qCol[1]);
                    item_curso_valor_orientacao.valor_defesa = Convert.ToDecimal(qCol[2]);
                    aplicacaoGeral.Altera_curso_valor_orientacao(item_curso_valor_orientacao);
                }
            }

            //===============================================================

            curso_valor_coordenacao item_curso_valor_coordenacao;
            item_curso_valor_coordenacao = new curso_valor_coordenacao();
            if (qRowsValoresCoordenacao != "")
            {
                var qRowValoresCoordenacao = qRowsValoresCoordenacao.Split(new string[] { "||" }, StringSplitOptions.None);

                foreach (var elemento in qRowValoresCoordenacao)
                {
                    item_curso_valor_coordenacao = new curso_valor_coordenacao();
                    var qCol = elemento.Split(new string[] { "~~" }, StringSplitOptions.None);
                    item_curso_valor_coordenacao.id_curso = item_fin_cursos.id_curso;
                    item_curso_valor_coordenacao.id_tipo_coordenador = Convert.ToInt32(qCol[0].Replace("\r\n", "").ToString().Trim());
                    item_curso_valor_coordenacao.valor = Convert.ToDecimal(qCol[1]);
                    aplicacaoGeral.Altera_Cria_curso_valor_coordenacao(item_curso_valor_coordenacao);
                }
            }

            //===============================================================

            CursoAplicacao aplicacaoCurso = new CursoAplicacao();
            item_fin_cursos = aplicacaoCurso.BuscaItem(item_fin_cursos);
            Session["fin_cursos"] = item_fin_cursos;

            CarregarDados();

            lblMensagem.Text = "Dados de Custo salvos com sucesso";
            lblTituloMensagem.Text = "Dados de Custo";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

        }

        //protected void btnEditaInscritos_Click(object sender, EventArgs e)
        //{
        //    int qIdCurso = Convert.ToInt32(HttpContext.Current.Request["hCodigo"]);
        //    List<periodo_inscricao_curso> lista = (List<periodo_inscricao_curso>)Session["lista_periodo_inscricao_curso"];
        //    periodo_inscricao_curso item_periodo_inscricao_curso = lista.Where(x => x.id_curso == qIdCurso).FirstOrDefault();
        //    Session["periodo_inscricao_curso"] = item_periodo_inscricao_curso;
        //    Response.Redirect("proRelacaoInscritosGestao.aspx", true);

        //}

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

            if (qIdArea != null && qIdArea != 0)
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
            sAux = lista.OrderByDescending(x => x.data).FirstOrDefault().status;
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

        public string setBotaoMatricular(object qTabela, int qIdInscricao, string qNome, int qIdArea)
        {
            string sAux = "";

            HashSet<historico_inscricao> lista = (HashSet<historico_inscricao>)qTabela;
            if (lista.OrderByDescending(x => x.data).FirstOrDefault().status != "Matriculado")
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
            //if (ddlTurmaMatricula.SelectedValue == "")
            //{
            //    sAux = "Deve-se selecionar uma Turma <br><br>";
            //}

            //if (ddlStatusMatricula.SelectedValue == "")
            //{
            //    sAux = sAux + "Deve-se selecionar um Status <br><br>";
            //}

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
                item_usuario.nomeSocial = item_aluno.nome.Substring(0, item_aluno.nome.IndexOf(" "));
                string sAuxSenha;
                sAuxSenha = item_aluno.cpf.Replace(".", "").Substring(0, 6);
                item_usuario.senha = Convert.ToBase64String(objSHA1.ComputeHash(objEncoding.GetBytes(sAuxSenha)));

                aplicacaoUsuario.CriarUsuario(item_usuario);

            }


            //Incluir o aluno na turma

            TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
            turmas item_turma = new turmas();
            //item_turma.id_turma = Convert.ToInt32(ddlTurmaMatricula.SelectedValue);
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
            //pItemHistorico.status = ddlStatusMatricula.SelectedValue;
            pItemHistorico.id_matricula_turma = pItemMatricula.id_matricula_turma;
            pItemHistorico = aplicacaoTurma.IncluirHistorico_Matricula(pItemHistorico);

            //Incluir no historico de incrição
            historico_inscricao item_historico_inscricao = new historico_inscricao();
            item_historico_inscricao.id_inscricao = item_fichas_inscricao.id_inscricao;
            item_historico_inscricao.data = DateTime.Now;
            item_historico_inscricao.status = "Matriculado";
            item_historico_inscricao.usuario = usuario.usuario;
            item_historico_inscricao = aplicacaoInscricao.CriarHistorico(item_historico_inscricao);

            periodo_inscricao_curso item_periodo_inscricao_curso = (periodo_inscricao_curso)Session["periodo_inscricao_curso"];

            item_periodo_inscricao_curso.fichas_inscricao.Where(x => x.id_inscricao == item_fichas_inscricao.id_inscricao).FirstOrDefault().historico_inscricao.Add(item_historico_inscricao);

            Session["periodo_inscricao_curso"] = item_periodo_inscricao_curso;

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