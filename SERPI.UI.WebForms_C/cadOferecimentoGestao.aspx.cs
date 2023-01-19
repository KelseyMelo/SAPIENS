using Aplicacao_C;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SERPI.Dominio_C;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using iTextSharp.text.html.simpleparser;
using System.Globalization;

namespace SERPI.UI.WebForms_C
{
    public partial class cadOferecimentoGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 16)) // 8. Oferecimentos - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                GeraisAplicacao aplicacaoGarais = new GeraisAplicacao();
                List<tipos_curso> listaTipoCurso = aplicacaoGarais.ListaTipoCurso();
                List<salas_aula> listaSala = aplicacaoGarais.ListaSalaAula();

                ddlTipoCursoOferecimento.Items.Clear();
                ddlTipoCursoOferecimento.DataSource = listaTipoCurso.OrderBy(x => x.id_tipo_curso);
                ddlTipoCursoOferecimento.DataValueField = "id_tipo_curso";
                ddlTipoCursoOferecimento.DataTextField = "tipo_curso";
                ddlTipoCursoOferecimento.DataBind();
                ddlTipoCursoOferecimento.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Tipo de Curso", ""));
                ddlTipoCursoOferecimento.SelectedValue = "";

                ddlTipoCursoFiltro.Items.Clear();
                ddlTipoCursoFiltro.DataSource = listaTipoCurso.OrderBy(x => x.id_tipo_curso);
                ddlTipoCursoFiltro.DataValueField = "id_tipo_curso";
                ddlTipoCursoFiltro.DataTextField = "tipo_curso";
                ddlTipoCursoFiltro.DataBind();
                ddlTipoCursoFiltro.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos Tipos de Curso", ""));
                ddlTipoCursoFiltro.SelectedValue = "";
                ddlTipoCursoFiltro_SelectedIndexChanged(null, null);

                ddlSalaAula.Items.Clear();
                ddlSalaAula.DataSource = listaSala;
                ddlSalaAula.DataValueField = "id_sala_aula";
                ddlSalaAula.DataTextField = "sala";
                ddlSalaAula.DataBind();
                ddlSalaAula.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione uma Sala de Aula", ""));
                ddlSalaAula.SelectedValue = "";

                ddlSalaAulaIncluirAula.Items.Clear();
                ddlSalaAulaIncluirAula.DataSource = listaSala;
                ddlSalaAulaIncluirAula.DataValueField = "id_sala_aula";
                ddlSalaAulaIncluirAula.DataTextField = "sala";
                ddlSalaAulaIncluirAula.DataBind();
                ddlSalaAulaIncluirAula.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione uma Sala de Aula", ""));
                ddlSalaAulaIncluirAula.SelectedValue = "2";



                //ddlNomeCursoArea.Attributes.Add("onselectedindexchanged", "javascript: ShowProgress();");
                //ddlCodigoCursoArea.Attributes.Add("onselectedindexchanged", "javascript: ShowProgress();");

                if (Session["sNewOferecimento"] != null && (Boolean)Session["sNewOferecimento"] != true)
                {
                    oferecimentos item;
                    item = (oferecimentos)Session["oferecimentos"];

                    //&& x.professores.email_confirmado == 1
                    var listaProfessor = from item2 in item.oferecimentos_professores.Where(x => x.tipo_professor == "professor").OrderBy(x => x.professores.nome).ToList() 
                                         select new
                                           {
                                             id_professor = item2.id_professor,
                                             nome = (item2.professores.email_confirmado == 1) ? item2.professores.nome : item2.professores.nome + " (e-mail não confirmado)",
                                             confirmado = item2.professores.email_confirmado
                                         };

                    ddlProfessorIncluirAula.Items.Clear();
                    ddlProfessorIncluirAula.DataSource = listaProfessor;
                    ddlProfessorIncluirAula.DataValueField = "id_professor";
                    ddlProfessorIncluirAula.DataTextField = "nome";
                    ddlProfessorIncluirAula.DataBind();
                    if (item.oferecimentos_professores.Where(x => x.responsavel == true).FirstOrDefault() != null)
                    {
                        ddlProfessorIncluirAula.SelectedValue = item.oferecimentos_professores.Where(x => x.responsavel == true).FirstOrDefault().id_professor.ToString();
                    }
                    else
                    {
                        ddlProfessorIncluirAula.SelectedIndex = 1;
                    }

                    foreach (System.Web.UI.WebControls.ListItem itm in ddlProfessorIncluirAula.Items)
                    {
                        if (itm.Text.IndexOf("e-mail não confirmado") != -1)
                        {
                            itm.Attributes.Add("disabled", "disabled");
                        }
                    }

                    lblTituloPagina.Text = "(Editar) - N.º " + item.id_oferecimento;

                    if (item.status == "inativado")
                    {
                        lblInativadoOferecimento.Style["display"] = "block";
                        btnAtivar.Style["display"] = "block";
                        btnInativar.Style["display"] = "none";
                    }
                    else
                    {
                        lblInativadoOferecimento.Style["display"] = "none";
                        btnAtivar.Style["display"] = "none";
                        btnInativar.Style["display"] = "block";
                    }

                    txtDataCadastro.Value = String.Format("{0:dd/MM/yyyy}", item.data_cadastro);
                    txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy}", item.data_alteracao);
                    txtStatus.Value = item.status;
                    txtResponsavel.Value = item.usuario;

                    if (item.quadrimestres.id_tipo_curso == null)
                    {
                        ddlTipoCursoOferecimento.SelectedValue = "1"; //Mestrado
                    }
                    else
                    {
                        ddlTipoCursoOferecimento.SelectedValue = item.quadrimestres.id_tipo_curso.ToString();
                    }
                    ddlTipoCursoOferecimento_SelectedIndexChanged(null, null);
                    ddlPeriodoOferecimento.SelectedValue = item.quadrimestre;
                    ddlCursoOferecimento.SelectedValue = item.disciplinas.cursos_disciplinas.ElementAt(0).cursos.id_curso.ToString();
                    txtNumeroOferecimanto.Value = item.num_oferecimento.ToString();

                    ddlTipoCursoOferecimento.Attributes.Add("disabled", "disabled");
                    ddlCursoOferecimento.Attributes.Add("disabled", "disabled");
                    ddlPeriodoOferecimento.Attributes.Add("disabled", "disabled");


                    txtCodigoDisciplinaOferecimento.Value = item.disciplinas.codigo;
                    txtIdDisciplinaOferecimento.Value = item.id_disciplina.ToString();
                    txtNomeDisciplinaOferecimento.Value = item.disciplinas.nome;

                    txtNumeroMaxAlunosOferecimento.Value = item.num_max_alunos.ToString();
                    txtCreditosOferecimento.Value = item.creditos.ToString();
                    txtCargaHorariaOferecimento.Value = item.carga_horaria.ToString();
                    chkAtivoOferecimento_new.Checked = item.ativo;

                    txtObjetivoOferecimento.Value = item.objetivo;
                    txtJustificativaOferecimento.Value = item.justificativa;
                    txtEmentaOferecimento.Value = item.ementa;
                    txtFormaAvaliacaoOferecimento.Value = item.forma_avaliacao;
                    txtMaterialUtilizadoOferecimento.Value = item.material_utilizado;
                    txtMetodologiaOferecimento.Value = item.metodologia;
                    txtConhecimentosPreviosOferecimento.Value = item.conhecimentos_previos;
                    txtProgramaOferecimento.Value = item.programa_disciplina;
                    txtBibliografiaBasicaOferecimento.Value = item.bibliografia_basica;
                    txtBibliografiaComplementarOferecimento.Value = item.bibliografica_compl;
                    txtObservacaoOferecimento.Value = item.observacao;

                    //PreencheProfessorAdicionado(item.Oferecimentos_professores.ToList());

                    //PreencheTecnicoAdicionado(item.Oferecimentos_professores.ToList());

                    btnSelecionarDisciplina.Visible = false;
                    divProfessor.Visible = true;
                    divTecnico.Visible = true;
                    tabProfessoresOferecimento.Style["display"] = "block";
                    tabTecnicoOferecimento.Style["display"] = "block";
                    //divProfessoresAdicionados.Visible = false;
                    //divTecnicoAdicionados.Visible = false;
                    btnCriarOferecimento.Disabled = false;
                    btnImprimirEmenta.Disabled = false;
                    divEdicao.Visible = true;
                    txtUrlEmenta.Value = "https://sapiens.ipt.br/doctos/Oferecimento_" + item.id_oferecimento.ToString() + ".pdf";
                    divUrl.Visible = true;
                    tabDataAulaOferecimento.Style["display"] = "block";
                    tabMatriculaOferecimento.Style["display"] = "block";

                    if (item.oferecimentos_professores.Where(x => x.responsavel == true).FirstOrDefault() != null)
                    {
                        txtProfessorResponsavelOferecimento.Value = item.oferecimentos_professores.Where(x => x.responsavel == true).FirstOrDefault().professores.nome;
                    }
                    else
                    {
                        txtProfessorResponsavelOferecimento.Value = "";
                    }

                    txtDuracaoOferecimento.Value = item.carga_horaria.ToString();
                    //decimal iHoraP = 0;
                    //decimal iHoraT = 0;
                    //foreach (var elemento in item.datas_aulas)
                    //{
                    //    foreach (var elemento2 in elemento.datas_aulas_professor)
                    //    {
                    //        if (elemento2.tipo_professor == "professor")
                    //        {
                    //            iHoraP = iHoraP + elemento2.hora_aula.Value;
                    //        }
                    //        else
                    //        {
                    //            iHoraT = iHoraT + elemento2.hora_aula.Value;
                    //        }
                    //    }
                    //}
                    //txtHorasProfessor.Value = iHoraP.ToString();
                    //txtHorasTecnico.Value = iHoraT.ToString();
                    //if (item.carga_horaria < iHoraP)
                    //{
                    //    txtHorasProfessor.Style.Add("color","red");
                    //}
                    //else if (item.carga_horaria == iHoraP)
                    //{
                    //    txtHorasProfessor.Style.Add("color", "blue");
                    //}
                    //else
                    //{
                    //    txtHorasProfessor.Style.Remove("color");
                    //}

                    if (!File.Exists(Server.MapPath("~/doctos/Oferecimento_" + item.id_oferecimento.ToString() + ".pdf")))
                    {
                        GeraEmentaPDF();
                    }

                    if (Session["AdiciondoSucesso"] != null)
                    {
                        if ((Boolean)Session["AdiciondoSucesso"])
                        {
                            Session["AdiciondoSucesso"] = null;
                            lblMensagem.Text = "Novo Oferecimento adicionado com sucesso";
                            lblTituloMensagem.Text = "Novo Oferecimento";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
                        }

                    }

                }
                else
                {
                    lblInativadoOferecimento.Style["display"] = "none";
                    btnAtivar.Style["display"] = "none";
                    btnInativar.Style["display"] = "none";

                    lblTituloPagina.Text = "(nova)";
                    txtDataCadastro.Value = "";
                    txtDataAlteracao.Value = "";
                    txtStatus.Value = "";
                    txtResponsavel.Value = "";

                    ddlTipoCursoOferecimento.SelectedValue = "";
                    ddlTipoCursoOferecimento_SelectedIndexChanged(null, null);
                    txtNumeroOferecimanto.Value = "";

                    ddlTipoCursoOferecimento.Attributes.Remove("disabled");
                    ddlCursoOferecimento.Attributes.Remove("disabled");
                    ddlPeriodoOferecimento.Attributes.Remove("disabled");

                    txtCodigoDisciplinaOferecimento.Value = "";
                    txtIdDisciplinaOferecimento.Value = "";
                    txtNomeDisciplinaOferecimento.Value = "";

                    txtNumeroMaxAlunosOferecimento.Value = "";
                    txtCreditosOferecimento.Value = "";
                    txtCargaHorariaOferecimento.Value = "";
                    chkAtivoOferecimento_new.Checked = true;

                    txtObjetivoOferecimento.Value = "";
                    txtJustificativaOferecimento.Value = "";
                    txtEmentaOferecimento.Value = "";
                    txtFormaAvaliacaoOferecimento.Value = "";
                    txtMaterialUtilizadoOferecimento.Value = "";
                    txtMetodologiaOferecimento.Value = "";
                    txtConhecimentosPreviosOferecimento.Value = "";
                    txtProgramaOferecimento.Value = "";
                    txtBibliografiaBasicaOferecimento.Value = "";
                    txtBibliografiaComplementarOferecimento.Value = "";
                    txtObservacaoOferecimento.Value = "";

                    btnSelecionarDisciplina.Visible = false;
                    divProfessor.Visible = false;
                    divTecnico.Visible = false;

                    tabProfessoresOferecimento.Style["display"] = "none";
                    tabTecnicoOferecimento.Style["display"] = "none";

                    //divProfessoresAdicionados.Visible = false;
                    //divTecnicoAdicionados.Visible = false;
                    btnCriarOferecimento.Disabled = true;
                    btnImprimirEmenta.Disabled = true;
                    divEdicao.Visible = false;
                    divUrl.Visible = false;
                    tabDataAulaOferecimento.Style["display"] = "none";
                    tabMatriculaOferecimento.Style["display"] = "none";
                }
            }

        }

        public void ddlTipoCursoFiltro_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (ddlTipoCursoFiltro.SelectedValue != "")
            {
                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos itemCurso = new cursos();
                itemCurso.id_tipo_curso = Convert.ToInt32(ddlTipoCursoFiltro.SelectedValue);
                itemCurso.status = "ativado";
                List<cursos> listaCurso = aplicacaoCurso.ListaItem(itemCurso);
                ddlTurmaFiltro.Items.Clear();
                ddlNomeCursoFiltro.Items.Clear();
                ddlNomeCursoFiltro.DataSource = listaCurso.OrderByDescending(x => x.nome);
                ddlNomeCursoFiltro.DataValueField = "id_curso";
                ddlNomeCursoFiltro.DataTextField = "nome";
                ddlNomeCursoFiltro.DataBind();
                ddlNomeCursoFiltro.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os Cursos", ""));
                ddlNomeCursoFiltro.SelectedValue = "";
            }
            else
            {
                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos itemCurso = new cursos();
                itemCurso.status = "ativado";
                List<cursos> listaCurso = aplicacaoCurso.ListaItem(itemCurso);
                ddlTurmaFiltro.Items.Clear();
                ddlNomeCursoFiltro.Items.Clear();
                ddlNomeCursoFiltro.DataSource = listaCurso.OrderByDescending(x => x.nome);
                ddlNomeCursoFiltro.DataValueField = "id_curso";
                ddlNomeCursoFiltro.DataTextField = "nome";
                ddlNomeCursoFiltro.DataBind();
                ddlNomeCursoFiltro.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os Cursos", ""));
                ddlNomeCursoFiltro.SelectedValue = "";
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.UpdatePanel1.GetType(), "Script", "javascript:fSelect2();", true);
        }

        public void ddlNomeCursoFiltro_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (ddlNomeCursoFiltro.SelectedValue != "")
            {
                TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                turmas itemTurma = new turmas();
                itemTurma.id_curso = Convert.ToInt32(ddlNomeCursoFiltro.SelectedValue);
                itemTurma.status = "ativado";
                itemTurma.cursos = new cursos();
                List<turmas> listaTurma = aplicacaoTurma.ListaItem(itemTurma);
                ddlTurmaFiltro.Items.Clear();
                ddlTurmaFiltro.DataSource = listaTurma.OrderByDescending(x => x.cod_turma);
                ddlTurmaFiltro.DataValueField = "id_turma";
                ddlTurmaFiltro.DataTextField = "cod_turma";
                ddlTurmaFiltro.DataBind();
                ddlTurmaFiltro.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todas as Turmas", ""));
                ddlTurmaFiltro.SelectedValue = "";
            }
            else
            {
                ddlTurmaFiltro.Items.Clear();
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel5, this.UpdatePanel1.GetType(), "Script", "javascript:fSelect2();", true);
        }

        public void ddlTipoCursoOferecimento_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (ddlTipoCursoOferecimento.SelectedValue != "")
            {
                QuadrimestreAplicacao aplicacaoPeriodo = new QuadrimestreAplicacao();
                quadrimestres itemPeriodo = new quadrimestres();
                itemPeriodo.id_tipo_curso = Convert.ToInt32(ddlTipoCursoOferecimento.SelectedValue);
                List<quadrimestres> listaPeriodo = aplicacaoPeriodo.ListaItem(itemPeriodo);
                ddlPeriodoOferecimento.Items.Clear();
                ddlPeriodoOferecimento.DataSource = listaPeriodo.OrderByDescending(x => x.quadrimestre);
                ddlPeriodoOferecimento.DataValueField = "quadrimestre";
                ddlPeriodoOferecimento.DataTextField = "quadrimestre";
                ddlPeriodoOferecimento.DataBind();
                ddlPeriodoOferecimento.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Período", ""));
                ddlPeriodoOferecimento.SelectedValue = "";

                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos itemCurso = new cursos();
                itemCurso.id_tipo_curso = Convert.ToInt32(ddlTipoCursoOferecimento.SelectedValue);
                List<cursos> listaCursos = aplicacaoCurso.ListaItem(itemCurso);
                ddlCursoOferecimento.Items.Clear();
                ddlCursoOferecimento.DataSource = listaCursos.OrderByDescending(x => x.nome);
                ddlCursoOferecimento.DataValueField = "id_curso";
                ddlCursoOferecimento.DataTextField = "nome";
                ddlCursoOferecimento.DataBind();
                ddlCursoOferecimento.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Curso", ""));
                ddlCursoOferecimento.SelectedValue = "";
            }
            else
            {
                ddlCursoOferecimento.Items.Clear();
                ddlPeriodoOferecimento.Items.Clear();
            }

            btnSelecionarDisciplina.Visible = false;
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fAtivaTextAreas(),fSelect2();", true);
        }

        public void ddlCursoOferecimento_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (ddlCursoOferecimento.SelectedValue != "")
            {
                btnSelecionarDisciplina.Visible = true;
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fAtivaTextAreas();fSelect2();", true);
            }
            else
            {
                btnSelecionarDisciplina.Visible = false;
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fAtivaTextAreas(),fSelect2();", true);
            }
            
        }

        public void ddlPeriodoOferecimento_SelectedIndexChanged(Object sender, EventArgs e)
        {
            
            if (ddlPeriodoOferecimento.SelectedValue != "" && Page.Request["hCodigoDisciplina"] != "")
            {
                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                oferecimentos item = new oferecimentos();
                item.quadrimestre = ddlPeriodoOferecimento.SelectedValue;
                item.id_disciplina = Convert.ToInt32(Page.Request["hCodigoDisciplina"]);
                txtNumeroOferecimanto.Value = aplicacaoOferecimento.BuscaNumeroOferecimento(item).ToString();

            }
            else
            {
                txtNumeroOferecimanto.Value="";
            }
            if (txtNumeroOferecimanto.Value != "")
            {
                if (Convert.ToInt32(txtNumeroOferecimanto.Value) > 1)
                {
                    lblTituloMensagem.Text = "ANTENÇÂO";
                    lblMensagem.Text = "Já há outro(s) OFERECIMENTO dessa disciplina para esse Período.</br></br> Para essa disciplina você está prestes a criar o Oferecimento número: <strong class=\"text-danger\" style=\"font-size:18px\">" + Convert.ToInt32(txtNumeroOferecimanto.Value) + "</strong>";
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fSelect2(),fAtivaTextAreas(),AbreMensagem_com_parametros('alert-warning','" + lblTituloMensagem.Text + "','" + lblMensagem.Text + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fSelect2(),fAtivaTextAreas();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fSelect2(),fAtivaTextAreas();", true);
            }
            
        }

        //protected void PreencheProfessorAdicionado(List<Oferecimentos_professores> lista)
        //{
        //    GridProfessorAdicionado item;
        //    List<GridProfessorAdicionado> listaProfessorAdicionado = new List<GridProfessorAdicionado>();
        //    lista = lista.Where(x => x.tipo_professor == "professor").ToList();
        //    foreach (var elemento in lista)
        //    {
        //        item = new GridProfessorAdicionado();
        //        item.id_professor = Convert.ToInt32(elemento.id_professor);
        //        item.cpf = elemento.professores.cpf;
        //        item.nome = elemento.professores.nome;
        //        item.responsavel = (bool)elemento.responsavel;

        //        listaProfessorAdicionado.Add(item);
        //    }

        //    grdProfessorAdicionado.DataSource = listaProfessorAdicionado;
        //    grdProfessorAdicionado.DataBind();

        //    if (lista.Count > 0)
        //    {
        //        grdProfessorAdicionado.UseAccessibleHeader = true;
        //        grdProfessorAdicionado.HeaderRow.TableSection = TableRowSection.TableHeader;
        //        msgSemResultadogrdProfessorAdicionado.Visible = false;
        //        grdProfessorAdicionado.Visible = true;
        //    }
        //    else
        //    {
        //        msgSemResultadogrdProfessorAdicionado.Visible = true;
        //    }

        //}

        //protected void PreencheTecnicoAdicionado(List<Oferecimentos_professores> lista)
        //{
        //    GridProfessorAdicionado item;
        //    List<GridProfessorAdicionado> listaProfessorAdicionado = new List<GridProfessorAdicionado>();
        //    lista = lista.Where(x => x.tipo_professor == "tecnico").ToList();
        //    foreach (var elemento in lista)
        //    {
        //        item = new GridProfessorAdicionado();
        //        item.id_professor = Convert.ToInt32(elemento.id_professor);
        //        item.cpf = elemento.professores.cpf;
        //        item.nome = elemento.professores.nome;

        //        listaProfessorAdicionado.Add(item);
        //    }

        //    grdTecnicoAdicionado.DataSource = listaProfessorAdicionado;
        //    grdTecnicoAdicionado.DataBind();

        //    if (lista.Count > 0)
        //    {
        //        grdTecnicoAdicionado.UseAccessibleHeader = true;
        //        grdTecnicoAdicionado.HeaderRow.TableSection = TableRowSection.TableHeader;
        //        msgSemResultadogrdTecnicoAdicionado.Visible = false;
        //        grdTecnicoAdicionado.Visible = true;
        //    }
        //    else
        //    {
        //        msgSemResultadogrdTecnicoAdicionado.Visible = true;
        //    }

        //}

        protected void btnCriarOferecimento_Click(object sender, EventArgs e)
        {
            Session["sNewOferecimento"] = true;
            Session["oferecimentos"] = null;
            Session["AdiciondoSucesso"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnIncluiDisciplina_Click(object sender, EventArgs e)
        {
            ddlPeriodoOferecimento_SelectedIndexChanged(null, null);

            DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
            disciplinas itemDisciplina = new disciplinas();

            itemDisciplina.id_disciplina = Convert.ToInt32(Page.Request["hCodigoDisciplina"]);
            itemDisciplina = aplicacaoDisciplina.BuscaItem(itemDisciplina);

            txtCodigoDisciplinaOferecimento.Value = itemDisciplina.codigo;
            txtIdDisciplinaOferecimento.Value = itemDisciplina.id_disciplina.ToString();
            txtNomeDisciplinaOferecimento.Value = itemDisciplina.nome;

            txtNumeroMaxAlunosOferecimento.Value = itemDisciplina.num_max_alunos.ToString();
            txtCreditosOferecimento.Value = itemDisciplina.creditos.ToString();
            txtCargaHorariaOferecimento.Value = itemDisciplina.carga_horaria.ToString();

            txtObjetivoOferecimento.Value = itemDisciplina.objetivo;
            txtJustificativaOferecimento.Value = itemDisciplina.justificativa;
            txtEmentaOferecimento.Value = itemDisciplina.ementa;
            txtFormaAvaliacaoOferecimento.Value = itemDisciplina.forma_avaliacao;
            txtMaterialUtilizadoOferecimento.Value = itemDisciplina.material_utilizado;
            txtMetodologiaOferecimento.Value = itemDisciplina.metodologia;
            txtConhecimentosPreviosOferecimento.Value = itemDisciplina.conhecimentos_previos;
            txtProgramaOferecimento.Value = itemDisciplina.programa_disciplina;
            txtBibliografiaBasicaOferecimento.Value = itemDisciplina.bibliografia_basica;
            txtBibliografiaComplementarOferecimento.Value = itemDisciplina.bibliografica_compl;
            txtObservacaoOferecimento.Value = itemDisciplina.observacao;

        }

        protected void btnImprimirEmenta_Click(object sender, EventArgs e)
        {
            try
            {
                oferecimentos item;
                item = (oferecimentos)Session["oferecimentos"];

                if (File.Exists(Server.MapPath("~/doctos/Oferecimento_" + item.id_oferecimento.ToString() + ".pdf")))
                {
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/Oferecimento_" + item.id_oferecimento.ToString() + ".pdf")));
                    Response.WriteFile(Server.MapPath("~/doctos/Oferecimento_" + item.id_oferecimento.ToString() + ".pdf"));
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão da Ementa do Oferecimento";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }
        }

        protected void GeraEmentaPDF()
        {
                string sQtextArea = "";
            PdfWriter writer = null;
            try
            {
                GeraisAplicacao AplicacaoGerais = new GeraisAplicacao();
                oferecimentos item;
                item = (oferecimentos)Session["oferecimentos"];

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 120, 40);//estibulando o espaçamento das margens que queremos ===Antigo===(45, 40, 40, 40)
                doc.AddCreationDate();//adicionando as configuracoes

                using (FileStream fileStream = new FileStream(Server.MapPath("~/doctos/Oferecimento_" + item.id_oferecimento.ToString() + ".pdf"), FileMode.Create))
                {
                    writer = PdfWriter.GetInstance(doc, fileStream);

                    PDF_Cabec_Rodape_GeraEmentaPDF pageHeaderFooter = new PDF_Cabec_Rodape_GeraEmentaPDF();
                    pageHeaderFooter.Caminho = Server.MapPath("~");
                    pageHeaderFooter.TipoCurso = item.disciplinas.cursos_disciplinas.FirstOrDefault().cursos.tipos_curso.tipo_curso;
                    writer.PageEvent = pageHeaderFooter;
                    doc.Open();

                    iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                    var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                    Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                    Font font_Verdana_11_Bold = new Font(_bfVerdanaNormal, 11, Font.BOLD);
                    Font font_Verdana_11_Normal = new Font(_bfVerdanaNormal, 11, Font.NORMAL);
                    Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                    Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                    Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                    Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                    Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                    Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                    Font font_Verdana_9_Normal_vermelho = new Font(_bfVerdanaNormal, 9, Font.NORMAL, Color.RED);
                    Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                    Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                    Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                    Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                    Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);
                    float[] widths;
                    Paragraph p;
                    PdfPCell cell;
                    PdfPTable table;
                    ArrayList htmlarraylist;
                    string sAux = "";


                    //Aqui é uma nova tabela de 2 Colunas ========================================================
                    table = new PdfPTable(2);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 63f, 180f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Título:", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(item.disciplinas.codigo + " - " + item.disciplinas.nome, font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Ativa:", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(item.ativo == true ? "Sim" : "Não", font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);
                    doc.Add(table);


                    //Aqui é uma nova tabela de 4 Colunas ========================================================
                    table = new PdfPTable(4);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 46f, 30f, 40f, 60f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Carga Horária:", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(item.carga_horaria.ToString(), font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 3
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Crédito:", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 4
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(item.creditos.ToString(), font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);
                    doc.Add(table);


                    //Aqui é uma nova tabela de 2 Colunas ========================================================
                    table = new PdfPTable(2);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 63f, 180f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Responsável:", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();

                    foreach (var item2 in item.oferecimentos_professores.Where(x => x.tipo_professor == "professor" && x.responsavel == true).ToList())
                    {
                        p.Add(new Chunk(item2.professores.nome + "\r\n", font_Verdana_9_Normal));
                    }

                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    doc.Add(table);


                    //Aqui é uma nova tabela de 2 Colunas ========================================================
                    table = new PdfPTable(2);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 63f, 180f };
                    table.SetWidths(widths);


                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Observações:", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    sQtextArea = "Caixa de texto <strong>'Observação'</strong>";
                    p = new Paragraph();
                    htmlarraylist = HTMLWorker.ParseToList(new StringReader(item.observacao), null);
                    for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                    {
                        p.Add(htmlarraylist[k]);
                    }
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    doc.Add(table);
                    sQtextArea = "";

                    //Aqui é uma nova tabela de 2 Colunas ========================================================
                    table = new PdfPTable(2);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 63f, 180f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Objetivo:", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    sQtextArea = "Caixa de texto <strong>'Objetivo'</strong>";
                    p = new Paragraph();
                    sAux = item.objetivo.Replace("x-small;", "&quot;9px&quot;");
                    sAux = sAux.Replace("small;", "&quot;10px&quot;");
                    sAux = sAux.Replace("medium;", "&quot;12px&quot;");
                    sAux = sAux.Replace("large;", "&quot;14px&quot;");
                    sAux = sAux.Replace("x-large;", "&quot;16px&quot;");
                    htmlarraylist = HTMLWorker.ParseToList(new StringReader(sAux), null);
                    for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                    {
                        p.Add(htmlarraylist[k]);
                    }
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    doc.Add(table);


                    //Aqui é uma nova tabela de 2 Colunas ========================================================
                    table = new PdfPTable(2);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 63f, 180f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Justificativa:", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    sQtextArea = "Caixa de texto <strong>'Justificativa'</strong>";
                    p = new Paragraph();
                    sAux = item.ementa.Replace("x-small;", "&quot;9px&quot;");
                    sAux = sAux.Replace("small;", "&quot;10px&quot;");
                    sAux = sAux.Replace("medium;", "&quot;12px&quot;");
                    sAux = sAux.Replace("large;", "&quot;14px&quot;");
                    sAux = sAux.Replace("x-large;", "&quot;16px&quot;");
                    htmlarraylist = HTMLWorker.ParseToList(new StringReader(sAux), null);
                    for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                    {
                        p.Add(htmlarraylist[k]);
                    }
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    doc.Add(table);


                    //Aqui é uma nova tabela de 2 Colunas ========================================================
                    table = new PdfPTable(2);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 63f, 180f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Ementa:", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    sQtextArea = "Caixa de texto <strong>'Ementa'</strong>";
                    p = new Paragraph();
                    sAux = item.ementa.Replace("x-small;", "&quot;9px&quot;");
                    sAux = sAux.Replace("small;", "&quot;10px&quot;");
                    sAux = sAux.Replace("medium;", "&quot;12px&quot;");
                    sAux = sAux.Replace("large;", "&quot;14px&quot;");
                    sAux = sAux.Replace("x-large;", "&quot;16px&quot;");
                    htmlarraylist = HTMLWorker.ParseToList(new StringReader(sAux), null);
                    for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                    {
                        p.Add(htmlarraylist[k]);
                    }
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    doc.Add(table);


                    //Aqui é uma nova tabela de 2 Colunas ========================================================
                    table = new PdfPTable(2);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 63f, 180f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Forma de Avaliação:", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    sQtextArea = "Caixa de texto <strong>'Forma de Avaliação'</strong>";
                    p = new Paragraph();
                    sAux = item.forma_avaliacao.Replace("x-small;", "&quot;9px&quot;");
                    sAux = sAux.Replace("small;", "&quot;10px&quot;");
                    sAux = sAux.Replace("medium;", "&quot;12px&quot;");
                    sAux = sAux.Replace("large;", "&quot;14px&quot;");
                    sAux = sAux.Replace("x-large;", "&quot;16px&quot;");
                    htmlarraylist = HTMLWorker.ParseToList(new StringReader(sAux), null);
                    for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                    {
                        p.Add(htmlarraylist[k]);
                    }
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    doc.Add(table);


                    //Aqui é uma nova tabela de 2 Colunas ========================================================
                    table = new PdfPTable(2);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 63f, 180f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Material Utilizado:", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    sQtextArea = "Caixa de texto <strong>'Material Utilizado'</strong>";
                    p = new Paragraph();
                    sAux = item.material_utilizado.Replace("x-small;", "&quot;9px&quot;");
                    sAux = sAux.Replace("small;", "&quot;10px&quot;");
                    sAux = sAux.Replace("medium;", "&quot;12px&quot;");
                    sAux = sAux.Replace("large;", "&quot;14px&quot;");
                    sAux = sAux.Replace("x-large;", "&quot;16px&quot;");
                    htmlarraylist = HTMLWorker.ParseToList(new StringReader(sAux), null);
                    for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                    {
                        p.Add(htmlarraylist[k]);
                    }
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    doc.Add(table);


                    //Aqui é uma nova tabela de 2 Colunas ========================================================
                    table = new PdfPTable(2);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 63f, 180f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Metodologia:", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    sQtextArea = "Caixa de texto <strong>'Metodologia'</strong>";
                    p = new Paragraph();

                    sAux = item.metodologia.Replace("x-small;", "&quot;9px&quot;");
                    sAux = sAux.Replace("small;", "&quot;10px&quot;");
                    sAux = sAux.Replace("medium;", "&quot;12px&quot;");
                    sAux = sAux.Replace("large;", "&quot;14px&quot;");
                    sAux = sAux.Replace("x-large;", "&quot;16px&quot;");
                    htmlarraylist = HTMLWorker.ParseToList(new StringReader(sAux), null);
                    for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                    {
                        p.Add(htmlarraylist[k]);
                    }
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    doc.Add(table);


                    //Aqui é uma nova tabela de 2 Colunas ========================================================
                    table = new PdfPTable(2);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 63f, 180f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Conhecimentos Prévio:", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    sQtextArea = "Caixa de texto <strong>'Conhecimentos Prévios'</strong>";
                    p = new Paragraph();

                    sAux = item.conhecimentos_previos.Replace("x-small;", "&quot;9px&quot;");
                    sAux = sAux.Replace("small;", "&quot;10px&quot;");
                    sAux = sAux.Replace("medium;", "&quot;12px&quot;");
                    sAux = sAux.Replace("large;", "&quot;14px&quot;");
                    sAux = sAux.Replace("x-large;", "&quot;16px&quot;");
                    htmlarraylist = HTMLWorker.ParseToList(new StringReader(sAux), null);
                    for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                    {
                        p.Add(htmlarraylist[k]);
                    }
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    doc.Add(table);


                    //Aqui é uma nova tabela de 2 Colunas ========================================================
                    table = new PdfPTable(2);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 63f, 180f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Bibliografia Básica:", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    sQtextArea = "Caixa de texto <strong>'Bibliografia Básica'</strong>";
                    p = new Paragraph();

                    sAux = item.bibliografia_basica.Replace("x-small;", "&quot;9px&quot;");
                    sAux = sAux.Replace("small;", "&quot;10px&quot;");
                    sAux = sAux.Replace("medium;", "&quot;12px&quot;");
                    sAux = sAux.Replace("large;", "&quot;14px&quot;");
                    sAux = sAux.Replace("x-large;", "&quot;16px&quot;");
                    htmlarraylist = HTMLWorker.ParseToList(new StringReader(sAux), null);
                    for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                    {
                        p.Add(htmlarraylist[k]);
                    }
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    doc.Add(table);


                    //Aqui é uma nova tabela de 2 Colunas ========================================================
                    table = new PdfPTable(2);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 63f, 180f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Bibliografia Complementar:", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    sQtextArea = "Caixa de texto <strong>'Bibliografia Complementar'</strong>";
                    p = new Paragraph();

                    sAux = item.bibliografica_compl.Replace("x-small;", "&quot;9px&quot;");
                    sAux = sAux.Replace("small;", "&quot;10px&quot;");
                    sAux = sAux.Replace("medium;", "&quot;12px&quot;");
                    sAux = sAux.Replace("large;", "&quot;14px&quot;");
                    sAux = sAux.Replace("x-large;", "&quot;16px&quot;");
                    htmlarraylist = HTMLWorker.ParseToList(new StringReader(sAux), null);
                    for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                    {
                        p.Add(htmlarraylist[k]);
                    }
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    doc.Add(table);


                    //Aqui é uma nova tabela de 2 Colunas ========================================================
                    table = new PdfPTable(2);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 63f, 180f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Programa da Oferecimento:", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    sQtextArea = "Caixa de texto <strong>'Programa do Oferecimento'</strong>";
                    p = new Paragraph();

                    sAux = item.programa_disciplina.Replace("x-small;", "&quot;9px&quot;");
                    sAux = sAux.Replace("small;", "&quot;10px&quot;");
                    sAux = sAux.Replace("medium;", "&quot;12px&quot;");
                    sAux = sAux.Replace("large;", "&quot;14px&quot;");
                    sAux = sAux.Replace("x-large;", "&quot;16px&quot;");
                    htmlarraylist = HTMLWorker.ParseToList(new StringReader(sAux), null);
                    for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                    {
                        p.Add(htmlarraylist[k]);
                    }
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    doc.Add(table);

                    PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                    PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                    writer.SetOpenAction(action);
                    doc.Close();
                }
            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina que gera a Ementa do Oferecimento";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (sQtextArea != "")
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>Erro provavelmente na " + sQtextArea;
                }
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }
            finally
            {
                //writer.Close();
            }

        }

        public class PDF_Cabec_Rodape_GeraEmentaPDF : PdfPageEventHelper
        {
            public string Caminho;
            public string TipoCurso;
            //public string PortariaMEC; Aqui é uma comunicação com o meio externo
            //public string DataMEC;
            //public string DataDOU;

            // write on top of document
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                base.OnOpenDocument(writer, document);
            }

            // write on start of each page
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);


                //PdfPTable tabFot = new PdfPTable(new float[] { 1f });
                ////tabFot.SpacingAfter = 10.0!
                //PdfPCell cell = default(PdfPCell);
                //tabFot.TotalWidth = 530f; //Aqui se seta se a tabela irá ficar mais a esquerda ou a direita

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_8_Italic = new Font(_bfVerdanaNormal, 8, Font.ITALIC);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                var lineStrong = new iTextSharp.text.pdf.draw.LineSeparator(2.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                iTextSharp.text.Image imgIPT = iTextSharp.text.Image.GetInstance(Caminho + "/img/ipt.gif");
                Paragraph p = new Paragraph();
                PdfPCell cell = default(PdfPCell);

                //Aqui se determina que é uma tabela com duas colunas
                PdfPTable table = new PdfPTable(2);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                //Aqui se determina se determina o tamanho de cada coluna
                float[] widths = new float[] { 60f, 170f };
                table.SetWidths(widths);

                //Coluna 1
                p = new Paragraph();
                imgIPT.SetAbsolutePosition(20, 755);
                imgIPT.ScalePercent(65);
                p.Add(new Chunk(imgIPT, 0, 0, true));
                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(p);
                cell = new PdfPCell(new Paragraph(new Chunk(imgIPT, 0, 0, true)));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                //cell.Colspan = 2;
                table.AddCell(cell);

                //Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Instituto de Pesquisas Tecnológicas do Estado de São Paulo S/A - IPT \r\n", font_Verdana_8_Normal));
                p.Add(new Chunk(" \r\n", font_Verdana_8_Normal));
                //p.Add(new Chunk("CET - Coordenadoria de Ensino Tecnológico \r\n", font_Verdana_8_Normal));
                p.Add(new Chunk(" \r\n", font_Verdana_8_Normal));
                p.Add(new Chunk(TipoCurso.ToUpper() + "  \r\n", font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);
                //(não sei, não sei, esquerda-direita, altura,não sei)
                table.WriteSelectedRows(0, -1, 42, (document.PageSize.Height - 40), writer.DirectContent);

                //Aqui é uma nova tabela
                PdfPTable table2 = new PdfPTable(1);
                table2.TotalWidth = 520f;
                table2.LockedWidth = true;

                widths = new float[] { 230f };
                table2.SetWidths(widths);

                //Aqui se desenha uma Linha
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(lineStrong));
                cell.AddElement(p);

                table2.AddCell(cell);
                //(não sei, não sei, esquerda-direita, altura,não sei)
                table2.WriteSelectedRows(0, -1, 42, (document.PageSize.Height - 100), writer.DirectContent);

            }

            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);
                var _bfArialNarrowNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_8_Italic = new Font(_bfVerdanaNormal, 8, Font.ITALIC);
                PdfPTable tabFot = new PdfPTable(new float[] { 1f });
                PdfPCell cell = default(PdfPCell);
                Paragraph p = new Paragraph();
                tabFot.TotalWidth = 520f;
                //p.Add(new Chunk("Data da Impressão: " + Strings.Format(Today, "dd/MM/yyyy") + Strings.Space(60) + "Página " + writer.PageNumber, font_Arialn_10_Normal));
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Instituto de Pesquisas Tecnológicas do Estado de São Paulo S/A - IPT \r\n", font_Verdana_8_Italic));
                p.Add(new Chunk("Av. Prof. Almeida Prado, 532 - Cidade Universitária - Butantã - 05508-901 - São Paulo - SP  \r\n", font_Verdana_8_Italic));
                p.Add(new Chunk("Caixa Postal 0141 - 01064-970 - São Paulo - SP - Tel.: (11)3767-4084, 3767-4624 - sapiens.ipt.br  \r\n", font_Verdana_8_Italic));
                cell = new PdfPCell(p);
                //cell.AddElement(p);

                //cell = new PdfPCell(p);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tabFot.AddCell(cell);
                tabFot.WriteSelectedRows(0, -1, 30, document.Bottom, writer.DirectContent);
            }

            //write on close of document
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);
            }
        }

        public class PDF_Cabec_Rodape_Paisagem : PdfPageEventHelper
        {
            public string Caminho;
            //public string PortariaMEC; Aqui é uma comunicação com o meio externo
            //public string DataMEC;
            //public string DataDOU;

            // write on top of document
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                base.OnOpenDocument(writer, document);
            }

            // write on start of each page
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);


                //PdfPTable tabFot = new PdfPTable(new float[] { 1f });
                ////tabFot.SpacingAfter = 10.0!
                //PdfPCell cell = default(PdfPCell);
                //tabFot.TotalWidth = 530f; //Aqui se seta se a tabela irá ficar mais a esquerda ou a direita

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_8_Italic = new Font(_bfVerdanaNormal, 8, Font.ITALIC);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                var lineStrong = new iTextSharp.text.pdf.draw.LineSeparator(2.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                iTextSharp.text.Image imgIPT = iTextSharp.text.Image.GetInstance(Caminho + "/img/ipt.gif");
                Paragraph p = new Paragraph();
                PdfPCell cell = default(PdfPCell);

                //Aqui se determina que é uma tabela com duas colunas
                PdfPTable table = new PdfPTable(2);
                table.TotalWidth = 830f;
                table.LockedWidth = true;

                //Aqui se determina se determina o tamanho de cada coluna
                float[] widths = new float[] { 60f, 170f };
                table.SetWidths(widths);

                //Coluna 1
                p = new Paragraph();
                imgIPT.SetAbsolutePosition(20, 755);
                imgIPT.ScalePercent(65);
                p.Add(new Chunk(imgIPT, 0, 0, true));
                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(p);
                cell = new PdfPCell(new Paragraph(new Chunk(imgIPT, 0, 0, true)));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                //cell.Colspan = 2;
                table.AddCell(cell);

                //Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Instituto de Pesquisas Tecnológicas do Estado de São Paulo S/A - IPT \r\n", font_Verdana_8_Normal));
                p.Add(new Chunk(" \r\n", font_Verdana_8_Normal));
                //p.Add(new Chunk("CET - Coordenadoria de Ensino Tecnológico \r\n", font_Verdana_8_Normal));
                p.Add(new Chunk("  \r\n", font_Verdana_8_Normal));
                p.Add(new Chunk("MESTRADO PROFISSIONAL  \r\n", font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);
                //(não sei, não sei, esquerda-direita, altura,não sei)
                table.WriteSelectedRows(0, -1, 42, (document.PageSize.Height - 40), writer.DirectContent);

                //Aqui é uma nova tabela
                PdfPTable table2 = new PdfPTable(1);
                table2.TotalWidth = 760f;
                table2.LockedWidth = true;

                widths = new float[] { 230f };
                table2.SetWidths(widths);

                //Aqui se desenha uma Linha
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(lineStrong));
                cell.AddElement(p);

                table2.AddCell(cell);
                //(não sei, não sei, esquerda-direita, altura,não sei)
                table2.WriteSelectedRows(0, -1, 42, (document.PageSize.Height - 100), writer.DirectContent);

            }

            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);
                var _bfArialNarrowNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_8_Italic = new Font(_bfVerdanaNormal, 8, Font.ITALIC);
                PdfPTable tabFot = new PdfPTable(new float[] { 1f });
                PdfPCell cell = default(PdfPCell);
                Paragraph p = new Paragraph();
                tabFot.TotalWidth = 830f;
                //p.Add(new Chunk("Data da Impressão: " + Strings.Format(Today, "dd/MM/yyyy") + Strings.Space(60) + "Página " + writer.PageNumber, font_Arialn_10_Normal));
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Instituto de Pesquisas Tecnológicas do Estado de São Paulo S/A - IPT \r\n", font_Verdana_8_Italic));
                p.Add(new Chunk("Av. Prof. Almeida Prado, 532 - Cidade Universitária - Butantã - 05508-901 - São Paulo - SP  \r\n", font_Verdana_8_Italic));
                p.Add(new Chunk("Caixa Postal 0141 - 01064-970 - São Paulo - SP - Tel.: (11)3767-4084, 3767-4624 - sapiens.ipt.br  \r\n", font_Verdana_8_Italic));
                cell = new PdfPCell(p);
                //cell.AddElement(p);

                //cell = new PdfPCell(p);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tabFot.AddCell(cell);
                tabFot.WriteSelectedRows(0, -1, 30, document.Bottom, writer.DirectContent);
            }

            //write on close of document
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);
            }
        }

        protected void btnImprimirNotasAlunos_Click(object sender, EventArgs e)
        {
            try
            {
                oferecimentos item;
                item = (oferecimentos)Session["oferecimentos"];

                if (GeraNotasAlunos())
                {
                    if (File.Exists(Server.MapPath("~/doctos/NotasAlunos_" + item.id_oferecimento.ToString() + ".pdf")))
                    {
                        Response.Clear();
                        Response.BufferOutput = true;
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/NotasAlunos_" + item.id_oferecimento.ToString() + ".pdf")));
                        Response.WriteFile(Server.MapPath("~/doctos/NotasAlunos_" + item.id_oferecimento.ToString() + ".pdf"));
                        Response.Flush();
                        Response.End();
                    }
                }
                
            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão de Notas dos Alunos no Oferecimento";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }
        }

        protected bool GeraNotasAlunos()
        {
            try
            {
                GeraisAplicacao AplicacaoGerais = new GeraisAplicacao();
                oferecimentos item;
                item = (oferecimentos)Session["oferecimentos"];

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 120, 40);//estibulando o espaçamento das margens que queremos ===Antigo===(45, 40, 40, 40)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/NotasAlunos_" + item.id_oferecimento.ToString() + ".pdf"), FileMode.Create));
                PDF_Cabec_Rodape_GeraEmentaPDF pageHeaderFooter = new PDF_Cabec_Rodape_GeraEmentaPDF();
                pageHeaderFooter.Caminho = Server.MapPath("~");
                pageHeaderFooter.TipoCurso = item.disciplinas.cursos_disciplinas.FirstOrDefault().cursos.tipos_curso.tipo_curso;
                writer.PageEvent = pageHeaderFooter;
                doc.Open();

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_11_Bold = new Font(_bfVerdanaNormal, 11, Font.BOLD);
                Font font_Verdana_11_Normal = new Font(_bfVerdanaNormal, 11, Font.NORMAL);
                Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Verdana_8_Bold = new Font(_bfVerdanaNormal, 8, Font.BOLD);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Verdana_7_Bold = new Font(_bfVerdanaNormal, 7, Font.BOLD);
                Font font_Verdana_7_Normal = new Font(_bfVerdanaNormal, 7, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_12_Bold = new Font(_bfArialNarrowNormal, 12, Font.BOLD);
                Font font_Arialn_12_Normal = new Font(_bfArialNarrowNormal, 12, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);
                Color FontColor_Cinza = new Color(204, 204, 204);
                Color FontColor_CinzaClaro = new Color(229, 229, 229);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                float[] widths;
                Paragraph p;
                PdfPCell cell;
                PdfPTable table;

                //Aqui é uma nova tabela
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 230f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("BOLETIM DE NOTAS E FREQUÊNCIA", font_Verdana_14_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.disciplinas.cursos_disciplinas.FirstOrDefault().cursos.tipos_curso.tipo_curso.ToUpper() + " EM " + item.disciplinas.cursos_disciplinas.FirstOrDefault().cursos.nome.ToUpper(), font_Arialn_12_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(2);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 23f, 200f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Disciplina", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.disciplinas.codigo + " - " + item.disciplinas.nome, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(4);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 70f, 190f, 70f, 190f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Professor", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                if (item.oferecimentos_professores.Any(x => x.responsavel == true))
                {
                    p.Add(new Chunk(item.oferecimentos_professores.Where(x => x.responsavel == true).FirstOrDefault().professores.titulacao.reduzido + " " + item.oferecimentos_professores.Where(x => x.responsavel == true).FirstOrDefault().professores.nome, font_Verdana_9_Bold));
                }
                else
                {
                    p.Add(new Chunk("Nenhum professor responsável encontrado", font_Verdana_9_Bold));
                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);
                doc.Add(table);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Aulas Realizadas", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.datas_aulas.Count.ToString(), font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);
                doc.Add(table);

                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(4);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 70f, 190f, 70f, 190f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Período", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.quadrimestre, font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(" ", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                //p.Add(new Chunk("de " + String.Format("{0:dd/MM/yyyy}", item.quadrimestres.data_inicio) + " a " + String.Format("{0:dd/MM/yyyy}", item.quadrimestres.data_fim), font_Verdana_9_Bold));
                p.Add(new Chunk("de " + String.Format("{0:dd/MM/yyyy}", item.datas_aulas.Min(x => x.data_aula)) + " a " + String.Format("{0:dd/MM/yyyy}", item.datas_aulas.Max(x => x.data_aula)), font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);
                doc.Add(table);


                //Aqui é uma nova tabela de 5 Colunas ========================================================
                table = new PdfPTable(5);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] {10f, 56f, 13f, 8f, 11f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Matrícula", font_Verdana_8_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderColor = FontColor_Cinza;
                cell.BackgroundColor = FontColor_Cinza;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Nome", font_Verdana_8_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderColor = FontColor_Cinza;
                cell.BackgroundColor = FontColor_Cinza;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Freq.", font_Verdana_8_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderColor = FontColor_Cinza;
                cell.BackgroundColor = FontColor_Cinza;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 5
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Conceito", font_Verdana_8_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderColor = FontColor_Cinza;
                cell.BackgroundColor = FontColor_Cinza;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Situação", font_Verdana_8_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderColor = FontColor_Cinza;
                cell.BackgroundColor = FontColor_Cinza;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                int i = 0;
                int j = 0;

                foreach (var elemento in item.matricula_oferecimento.OrderBy(x=> x.alunos.nome))
                {
                    i++;
                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(elemento.id_aluno.ToString(), font_Arialn_8_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    if (i % 2 != 0)
                    {
                        cell.BorderColor = Color.WHITE;
                        cell.BackgroundColor = Color.WHITE;
                    }
                    else
                    {
                        cell.BorderColor = FontColor_CinzaClaro;
                        cell.BackgroundColor = FontColor_CinzaClaro;
                    }
                    cell.Border = Rectangle.BOX;
                    cell.PaddingBottom = 6f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(elemento.alunos.nome, font_Arialn_8_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    if (i % 2 != 0)
                    {
                        cell.BorderColor = Color.WHITE;
                        cell.BackgroundColor = Color.WHITE;
                    }
                    else
                    {
                        cell.BorderColor = FontColor_CinzaClaro;
                        cell.BackgroundColor = FontColor_CinzaClaro;
                    }
                    cell.PaddingBottom = 6f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 3
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk((item.presenca.Where(x => x.id_aluno == elemento.id_aluno).Count() == 0) ? "0,00%" : ((item.presenca.Where(x => x.id_aluno == elemento.id_aluno && x.presente == true).Count()) / (item.datas_aulas.Count() * 0.01)).ToString("0.##") + "%", font_Arialn_8_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    if (i % 2 != 0)
                    {
                        cell.BorderColor = Color.WHITE;
                        cell.BackgroundColor = Color.WHITE;
                    }
                    else
                    {
                        cell.BorderColor = FontColor_CinzaClaro;
                        cell.BackgroundColor = FontColor_CinzaClaro;
                    }
                    cell.PaddingBottom = 6f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 4
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk((item.notas.Where(x => x.id_aluno == elemento.id_aluno).FirstOrDefault() == null) ? "" : (item.notas.Where(x => x.id_aluno == elemento.id_aluno).Select(x => x.conceito).FirstOrDefault() == null) ? "" : item.notas.Where(x => x.id_aluno == elemento.id_aluno).Select(x => x.conceito).FirstOrDefault(), font_Arialn_8_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    if (i % 2 != 0)
                    {
                        cell.BorderColor = Color.WHITE;
                        cell.BackgroundColor = Color.WHITE;
                    }
                    else
                    {
                        cell.BorderColor = FontColor_CinzaClaro;
                        cell.BackgroundColor = FontColor_CinzaClaro;
                    }
                    cell.PaddingBottom = 6f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 5
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk((item.notas.Where(x => x.id_aluno == elemento.id_aluno).FirstOrDefault() == null) ? "" : (item.notas.Where(x => x.id_aluno == elemento.id_aluno).FirstOrDefault().conceitos_de_aprovacao == null) ? "" : item.notas.Where(x => x.id_aluno == elemento.id_aluno).FirstOrDefault().conceitos_de_aprovacao.descricao, font_Verdana_8_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    if (i % 2 != 0)
                    {
                        cell.BorderColor = Color.WHITE;
                        cell.BackgroundColor = Color.WHITE;
                    }
                    else
                    {
                        cell.BorderColor = FontColor_CinzaClaro;
                        cell.BackgroundColor = FontColor_CinzaClaro;
                    }
                    cell.PaddingBottom = 6f;
                    table.AddCell(cell);
                }

                doc.Add(table);

                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 520f };
                table.SetWidths(widths);

                //CultureInfo ci = CultureInfo.InvariantCulture;

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("São Paulo, " + DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy ", new CultureInfo("PT-pt")), font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 10f;
                cell.PaddingBottom = 30f;
                table.AddCell(cell);
                
                doc.Add(table);

                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(2);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 260f, 260f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                if (item.oferecimentos_professores.Any(x => x.responsavel == true))
                {
                    p.Add(new Chunk(item.oferecimentos_professores.Where(x => x.responsavel == true).FirstOrDefault().professores.titulacao.reduzido + " " + item.oferecimentos_professores.Where(x => x.responsavel == true).FirstOrDefault().professores.nome, font_Verdana_9_Bold));
                }
                else
                {
                    p.Add(new Chunk("Nenhum professor responsável encontrado", font_Verdana_9_Bold));
                }
                
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.disciplinas.cursos_disciplinas.FirstOrDefault().cursos.cursos_coordenadores.Where(x => x.id_tipo_coordenador == 1 && x.id_curso == item.disciplinas.cursos_disciplinas.FirstOrDefault().id_curso).FirstOrDefault().professores.titulacao.reduzido + " " + item.disciplinas.cursos_disciplinas.FirstOrDefault().cursos.cursos_coordenadores.Where(x=> x.id_tipo_coordenador == 1 && x.id_curso == item.disciplinas.cursos_disciplinas.FirstOrDefault().id_curso).FirstOrDefault().professores.nome, font_Verdana_9_Bold));
                //p.Add(new Chunk(item.disciplinas.cursos_disciplinas.FirstOrDefault().id_curso.ToString(), font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Professor", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Coordenador", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3f;
                table.AddCell(cell);

                doc.Add(table);

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

                return true;

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina que gera a Nota dos Alunos";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                return false;
            }

        }

        protected void btnImprimirPresencaProfessor_Click(object sender, EventArgs e)
        {
            try
            {
                oferecimentos item;
                item = (oferecimentos)Session["oferecimentos"];

                GeraPresencaProfessor();

                if (File.Exists(Server.MapPath("~/doctos/PresencaProfessor_" + item.id_oferecimento.ToString() + ".pdf")))
                {
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/PresencaProfessor_" + item.id_oferecimento.ToString() + ".pdf")));
                    Response.WriteFile(Server.MapPath("~/doctos/PresencaProfessor_" + item.id_oferecimento.ToString() + ".pdf"));
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão de Presença de Professor no Oferecimento";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }
        }

        protected void GeraPresencaProfessor()
        {
            try
            {
                GeraisAplicacao AplicacaoGerais = new GeraisAplicacao();
                oferecimentos item;
                item = (oferecimentos)Session["oferecimentos"];

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 120, 40);//estibulando o espaçamento das margens que queremos ===Antigo===(45, 40, 40, 40)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/PresencaProfessor_" + item.id_oferecimento.ToString() + ".pdf"), FileMode.Create));
                PDF_Cabec_Rodape_GeraEmentaPDF pageHeaderFooter = new PDF_Cabec_Rodape_GeraEmentaPDF();
                pageHeaderFooter.Caminho = Server.MapPath("~");
                pageHeaderFooter.TipoCurso = item.disciplinas.cursos_disciplinas.FirstOrDefault().cursos.tipos_curso.tipo_curso;
                writer.PageEvent = pageHeaderFooter;
                doc.Open();

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_11_Bold = new Font(_bfVerdanaNormal, 11, Font.BOLD);
                Font font_Verdana_11_Normal = new Font(_bfVerdanaNormal, 11, Font.NORMAL);
                Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Verdana_8_Bold = new Font(_bfVerdanaNormal, 8, Font.BOLD);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Verdana_7_Bold = new Font(_bfVerdanaNormal, 7, Font.BOLD);
                Font font_Verdana_7_Normal = new Font(_bfVerdanaNormal, 7, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);
                Color FontColor_Cinza = new Color(204, 204, 204);
                Color FontColor_CinzaClaro = new Color(229, 229, 229);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                float[] widths;
                Paragraph p;
                PdfPCell cell;
                PdfPTable table;

                //Aqui é uma nova tabela
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 230f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("PRESENÇA DE PROFESSORES", font_Verdana_14_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui se desenha uma linha fina
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(linefine));
                doc.Add(p);



                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(2);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 63f, 180f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Código", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.disciplinas.codigo, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("N.º Oferecimento", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.num_oferecimento.ToString(), font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);
                doc.Add(table);

                //Aqui se desenha uma linha fina
                //p = new Paragraph();
                //p.Alignment = Element.ALIGN_CENTER;
                //p.Clear();
                //p.Add(new Chunk(linefine));
                //doc.Add(p);

                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(2);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 63f, 180f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Período", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.quadrimestre, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Nome", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.disciplinas.nome, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);
                doc.Add(table);


                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(6);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 5f, 26f, 45f, 10f, 8f, 8f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("", font_Verdana_8_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderColor = FontColor_Cinza;
                cell.BackgroundColor = FontColor_Cinza;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Data de Aula", font_Verdana_8_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderColor = FontColor_Cinza;
                cell.BackgroundColor = FontColor_Cinza;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Nome", font_Verdana_8_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderColor = FontColor_Cinza;
                cell.BackgroundColor = FontColor_Cinza;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Sala", font_Verdana_8_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderColor = FontColor_Cinza;
                cell.BackgroundColor = FontColor_Cinza;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 5
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Tipo", font_Verdana_8_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderColor = FontColor_Cinza;
                cell.BackgroundColor = FontColor_Cinza;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Presente", font_Verdana_8_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderColor = FontColor_Cinza;
                cell.BackgroundColor = FontColor_Cinza;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                int i = 0;
                int j = 0;

                foreach (var elemento in item.datas_aulas)
                {
                    j++;
                    foreach (var elemento2 in elemento.datas_aulas_professor.OrderBy(x => x.tipo_professor).ThenBy(x => x.professores.nome))
                    {
                        i++;
                        //Aqui se desenha a Coluna 1
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(j.ToString(), font_Arialn_8_Normal));
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Border = Rectangle.BOX;
                        if (i % 2 != 0)
                        {
                            cell.BorderColor = Color.WHITE;
                            cell.BackgroundColor = Color.WHITE;
                        }
                        else
                        {
                            cell.BorderColor = FontColor_CinzaClaro;
                            cell.BackgroundColor = FontColor_CinzaClaro;
                        }
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 2
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(String.Format("{0:dd/MM/yyyy}", elemento.data_aula) + " das " + String.Format("{0:HH:mm}", elemento.hora_inicio) + " às " + String.Format("{0:HH:mm}", elemento.hora_fim), font_Arialn_8_Normal));
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        if (i % 2 != 0)
                        {
                            cell.BorderColor = Color.WHITE;
                            cell.BackgroundColor = Color.WHITE;
                        }
                        else
                        {
                            cell.BorderColor = FontColor_CinzaClaro;
                            cell.BackgroundColor = FontColor_CinzaClaro;
                        }
                        cell.Border = Rectangle.BOX;
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 3
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(elemento2.professores.nome, font_Arialn_8_Normal));
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Border = Rectangle.BOX;
                        if (i % 2 != 0)
                        {
                            cell.BorderColor = Color.WHITE;
                            cell.BackgroundColor = Color.WHITE;
                        }
                        else
                        {
                            cell.BorderColor = FontColor_CinzaClaro;
                            cell.BackgroundColor = FontColor_CinzaClaro;
                        }
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 4
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(elemento.salas_aula.sala, font_Arialn_8_Normal));
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Border = Rectangle.BOX;
                        if (i % 2 != 0)
                        {
                            cell.BorderColor = Color.WHITE;
                            cell.BackgroundColor = Color.WHITE;
                        }
                        else
                        {
                            cell.BorderColor = FontColor_CinzaClaro;
                            cell.BackgroundColor = FontColor_CinzaClaro;
                        }
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 5
                        cell = new PdfPCell();
                        p = new Paragraph();
                        if (elemento2.tipo_professor == "professor")
                        {
                            p.Add(new Chunk("Professor", font_Arialn_8_Normal));
                        }
                        else
                        {
                            p.Add(new Chunk("Técnico", font_Arialn_8_Normal));
                        }
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Border = Rectangle.BOX;
                        if (i % 2 != 0)
                        {
                            cell.BorderColor = Color.WHITE;
                            cell.BackgroundColor = Color.WHITE;
                        }
                        else
                        {
                            cell.BorderColor = FontColor_CinzaClaro;
                            cell.BackgroundColor = FontColor_CinzaClaro;
                        }
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 4
                        cell = new PdfPCell();
                        p = new Paragraph();
                        if (elemento.presenca_professor.Where(x => x.id_professor == elemento2.id_professor && x.tipo_professor == elemento2.tipo_professor).SingleOrDefault() != null)
                        {
                            if (elemento.presenca_professor.Where(x => x.id_professor == elemento2.id_professor && x.tipo_professor == elemento2.tipo_professor).SingleOrDefault().presente == true)
                            {
                                p.Add(new Chunk("X", font_Verdana_8_Normal));
                            }
                            else
                            {
                                p.Add(new Chunk("", font_Verdana_8_Normal));
                            }
                        }
                        else
                        {
                            p.Add(new Chunk("", font_Verdana_8_Normal));
                        }
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Border = Rectangle.BOX;
                        if (i % 2 != 0)
                        {
                            cell.BorderColor = Color.WHITE;
                            cell.BackgroundColor = Color.WHITE;
                        }
                        else
                        {
                            cell.BorderColor = FontColor_CinzaClaro;
                            cell.BackgroundColor = FontColor_CinzaClaro;
                        }
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);
                    }
                }

                doc.Add(table);

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina que gera a Lista de Presença dos Professores";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }

        }

        protected void btnImprimirPresencaAluno_Click(object sender, EventArgs e)
        {
            try
            {
                oferecimentos item;
                item = (oferecimentos)Session["oferecimentos"];

                GeraPresencaAlunos();

                if (File.Exists(Server.MapPath("~/doctos/PresencaAlunos_" + item.id_oferecimento.ToString() + ".pdf")))
                {
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/PresencaAlunos_" + item.id_oferecimento.ToString() + ".pdf")));
                    Response.WriteFile(Server.MapPath("~/doctos/PresencaAlunos_" + item.id_oferecimento.ToString() + ".pdf"));
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão de Presença de Alunos no Oferecimento";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }
        }

        protected void GeraPresencaAlunos()
        {
            try
            {
                GeraisAplicacao AplicacaoGerais = new GeraisAplicacao();
                oferecimentos item;
                item = (oferecimentos)Session["oferecimentos"];

                //Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                Document doc = new Document(PageSize.A4.Rotate());//Igal ao de cima, mas tipo Paisagem
                doc.SetMargins(45, 40, 120, 40);//estibulando o espaçamento das margens que queremos ===Antigo===(45, 40, 40, 40)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/PresencaAlunos_" + item.id_oferecimento.ToString() + ".pdf"), FileMode.Create));
                PDF_Cabec_Rodape_Paisagem pageHeaderFooter = new PDF_Cabec_Rodape_Paisagem();
                pageHeaderFooter.Caminho = Server.MapPath("~");
                writer.PageEvent = pageHeaderFooter;
                doc.Open();

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_11_Bold = new Font(_bfVerdanaNormal, 11, Font.BOLD);
                Font font_Verdana_11_Normal = new Font(_bfVerdanaNormal, 11, Font.NORMAL);
                Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Verdana_9_Normal_vermelho = new Font(_bfVerdanaNormal, 9, Font.NORMAL, Color.RED);
                Font font_Verdana_9_Normal_azul = new Font(_bfVerdanaNormal, 9, Font.NORMAL, Color.BLUE);
                Font font_Verdana_8_Bold = new Font(_bfVerdanaNormal, 8, Font.BOLD);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Verdana_7_Bold = new Font(_bfVerdanaNormal, 7, Font.BOLD);
                Font font_Verdana_7_Normal = new Font(_bfVerdanaNormal, 7, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);
                Font redListTextFont = FontFactory.GetFont("Arial", 28, Color.RED);
                Color FontColor_Cinza = new Color(204, 204, 204);
                Color FontColor_CinzaClaro = new Color(229, 229, 229);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                float[] widths;
                Paragraph p;
                PdfPCell cell;
                PdfPTable table;

                //Aqui é uma nova tabela
                table = new PdfPTable(1);
                table.TotalWidth = 750f;
                table.LockedWidth = true;

                widths = new float[] { 750f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item.disciplinas.codigo + " - " + item.disciplinas.nome + " - " + item.quadrimestre, font_Verdana_12_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui se desenha uma linha fina
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(linefine));
                doc.Add(p);

                int qTotalColunas = 4 + item.datas_aulas.Count;


                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(qTotalColunas);
                table.TotalWidth = 830f;
                table.LockedWidth = true;
                widths = new float[] { 25f, 90f, 25f, 12f };
                for (int y = 0; y < item.datas_aulas.Count; y++)
                {
                    widths = widths.Concat(new float[] { 12f }).ToArray();
                }
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Matrícula", font_Verdana_8_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Nome", font_Verdana_8_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Freq.(%)", font_Verdana_8_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Conc.", font_Verdana_8_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                foreach (var elemento in item.datas_aulas.OrderBy(x => x.data_aula))
                {
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(String.Format("{0:dd/MM}", elemento.data_aula), font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);
                }

                foreach (var elemento in item.matricula_oferecimento.OrderBy(x=> x.alunos.nome))
                {
                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(elemento.alunos.idaluno.ToString(), font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(elemento.alunos.nome, font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk((item.presenca.Where(x => x.id_aluno == elemento.id_aluno).Count() == 0) ? "0,00%" : ((item.presenca.Where(x => x.id_aluno == elemento.id_aluno && x.presente == true).Count()) / (item.datas_aulas.Count() * 0.01)).ToString("0.##") + "%", font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk((item.notas.Where(x => x.id_aluno == elemento.id_aluno).FirstOrDefault() == null) ? "" : (item.notas.Where(x => x.id_aluno == elemento.id_aluno).Select(x => x.conceito).FirstOrDefault() == null) ? "" : item.notas.Where(x => x.id_aluno == elemento.id_aluno).Select(x => x.conceito).FirstOrDefault(), font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    foreach (var elemento2 in item.datas_aulas.OrderBy(x => x.data_aula))
                    {

                        if (elemento2.presenca.Any(x=> x.id_aluno == elemento.id_aluno))
                        {
                            if (elemento2.presenca.Where(x => x.id_aluno == elemento.id_aluno).FirstOrDefault().presente == true)
                            {
                                cell = new PdfPCell();
                                p = new Paragraph();
                                p.Add(new Chunk("v", font_Verdana_9_Normal));
                                cell = new PdfPCell(p);
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell.Border = Rectangle.NO_BORDER;
                                cell.PaddingBottom = 12f;
                                table.AddCell(cell);
                            }
                            else if (elemento2.presenca.Where(x => x.id_aluno == elemento.id_aluno).FirstOrDefault().presente == false)
                            {
                                cell = new PdfPCell();
                                p = new Paragraph();
                                p.Add(new Chunk("x", font_Verdana_9_Normal_vermelho));
                                cell = new PdfPCell(p);
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell.Border = Rectangle.NO_BORDER;
                                cell.PaddingBottom = 12f;
                                table.AddCell(cell);
                            }
                            else
                            {
                                cell = new PdfPCell();
                                p = new Paragraph();
                                p.Add(new Chunk(" ", font_Verdana_9_Normal));
                                cell = new PdfPCell(p);
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell.Border = Rectangle.NO_BORDER;
                                cell.PaddingBottom = 12f;
                                table.AddCell(cell);
                            }
                        }
                        else
                        {
                            cell = new PdfPCell();
                            p = new Paragraph();
                            p.Add(new Chunk(" ", font_Verdana_9_Normal_vermelho));
                            cell = new PdfPCell(p);
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell.Border = Rectangle.NO_BORDER;
                            cell.PaddingBottom = 12f;
                            table.AddCell(cell);
                        }
                        
                    }

                }

                doc.Add(table);

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina que gera a Lista de Presença dos Alunos";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("cadOferecimento.aspx", true);
        }

        protected void btnSalvar_ServerClick1(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                txtObjetivoOferecimento.Value = Page.Request["htxtObjetivoOferecimento"];
                txtJustificativaOferecimento.Value = Page.Request["htxtJustificativaOferecimento"];
                txtEmentaOferecimento.Value = Page.Request["htxtEmentaOferecimento"];
                txtFormaAvaliacaoOferecimento.Value = Page.Request["htxtFormaAvaliacaoOferecimento"];
                txtMaterialUtilizadoOferecimento.Value = Page.Request["htxtMaterialUtilizadoOferecimento"];
                txtMetodologiaOferecimento.Value = Page.Request["htxtMetodologiaOferecimento"];
                txtConhecimentosPreviosOferecimento.Value = Page.Request["htxtConhecimentosPreviosOferecimento"];
                txtProgramaOferecimento.Value = Page.Request["htxtProgramaOferecimento"];
                txtBibliografiaBasicaOferecimento.Value = Page.Request["htxtBibliografiaBasicaOferecimento"];
                txtBibliografiaComplementarOferecimento.Value = Page.Request["htxtBibliografiaComplementarOferecimento"];
                txtObservacaoOferecimento.Value = Page.Request["htxtObservacaoOferecimento"];

                string sAux = "";

                if (ddlTipoCursoOferecimento.SelectedValue == "")
                {
                    sAux = sAux + "Selecione um Tipo de Curso. <br/><br/>";
                }

                if (ddlPeriodoOferecimento.SelectedValue == "")
                {
                    sAux = sAux + "Selecione um Período. <br/><br/>";
                }

                if (txtNumeroOferecimanto.Value.Trim() == "")
                {
                    sAux = sAux + "O <strong> número do oferecimento </strong> é obrigatório, porém é gerado automaticamente. Para tanto deve-se escolher um <strong>Tipode Curso</strong>, um <strong>Período</strong> e uma <strong>Disciplina</strong>. <br/><br/>";
                }

                if (txtIdDisciplinaOferecimento.Value.Trim() == "")
                {
                    sAux = "Deve-se selecionar uma disciplina. <br/><br/>";
                }
                
                if (txtCreditosOferecimento.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher os Créditos da Oferecimento. <br/><br/>";
                }

                if (txtCargaHorariaOferecimento.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a Carga Horária da Oferecimento. <br/><br/>";
                }

                if (txtEmentaOferecimento.Value.Trim() == "<br>")
                {
                    sAux = sAux + "Preencher a Ementa da Oferecimento. <br/><br/>";
                }

                if (txtBibliografiaBasicaOferecimento.Value.Trim() == "<br>")
                {
                    sAux = sAux + "Preencher a Bibliografia Básica da Oferecimento. <br/><br/>";
                }

                if (sAux != "")
                {
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                    return;
                }

                txtObjetivoOferecimento.Value = Page.Request["htxtObjetivoOferecimento"];
                txtJustificativaOferecimento.Value = Page.Request["htxtJustificativaOferecimento"];
                txtEmentaOferecimento.Value = Page.Request["htxtEmentaOferecimento"];
                txtFormaAvaliacaoOferecimento.Value = Page.Request["htxtFormaAvaliacaoOferecimento"];
                txtMaterialUtilizadoOferecimento.Value = Page.Request["htxtMaterialUtilizadoOferecimento"];
                txtMetodologiaOferecimento.Value = Page.Request["htxtMetodologiaOferecimento"];
                txtConhecimentosPreviosOferecimento.Value = Page.Request["htxtConhecimentosPreviosOferecimento"];
                txtProgramaOferecimento.Value = Page.Request["htxtProgramaOferecimento"];
                txtBibliografiaBasicaOferecimento.Value = Page.Request["htxtBibliografiaBasicaOferecimento"];
                txtBibliografiaComplementarOferecimento.Value = Page.Request["htxtBibliografiaComplementarOferecimento"];
                txtObservacaoOferecimento.Value = Page.Request["htxtObservacaoOferecimento"];

                if (Session["sNewOferecimento"] != null && (Boolean)Session["sNewOferecimento"] != true)
                {
                    //Update
                    OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                    oferecimentos item = new oferecimentos();

                    item = (oferecimentos)Session["oferecimentos"];
                    //item.id_disciplina = Convert.ToInt32(txtIdDisciplinaOferecimento.Value.Trim());
                    item.ativo = chkAtivoOferecimento_new.Checked;
                    item.num_oferecimento = Convert.ToInt32(txtNumeroOferecimanto.Value.Trim());
                    //item.quadrimestre = ddlPeriodoOferecimento.SelectedValue;

                    if (txtNumeroMaxAlunosOferecimento.Value.Trim() != "")
                    {
                        item.num_max_alunos = Convert.ToInt32(txtNumeroMaxAlunosOferecimento.Value);
                    }

                    item.creditos = Convert.ToInt32(txtCreditosOferecimento.Value);
                    item.carga_horaria = Convert.ToInt32(txtCargaHorariaOferecimento.Value);

                    item.objetivo = txtObjetivoOferecimento.Value.Trim();
                    item.justificativa = txtJustificativaOferecimento.Value.Trim();
                    item.ementa = txtEmentaOferecimento.Value.Trim();
                    item.forma_avaliacao = txtFormaAvaliacaoOferecimento.Value.Trim();
                    item.material_utilizado = txtMaterialUtilizadoOferecimento.Value.Trim();
                    item.metodologia = txtMetodologiaOferecimento.Value.Trim();
                    item.conhecimentos_previos = txtConhecimentosPreviosOferecimento.Value.Trim();
                    item.programa_disciplina = txtProgramaOferecimento.Value.Trim();
                    item.bibliografia_basica = txtBibliografiaBasicaOferecimento.Value.Trim();
                    item.bibliografica_compl = txtBibliografiaComplementarOferecimento.Value.Trim();
                    item.observacao = txtObservacaoOferecimento.Value.Trim();

                    item.status = "alterado";
                    //item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    aplicacaoOferecimento.AlterarItem(item);

                    oferecimentos_professores itemOferecimento_Professores = new oferecimentos_professores();
                    itemOferecimento_Professores.id_oferecimento = item.id_oferecimento;
                    itemOferecimento_Professores.tipo_professor = "professor";

                    aplicacaoOferecimento.AlterarResponsavelProfessor_Tecnico_Oferecimento(itemOferecimento_Professores);

                    sAux = Page.Request["hCodigo"].Replace("undefined;", "");

                    var aAux = sAux.Split(';');

                    if (aAux.ElementAt(0) != "")
                    {
                        for (int i = 0; i < aAux.Count(); i++)
                        {
                            itemOferecimento_Professores = new oferecimentos_professores();
                            itemOferecimento_Professores.id_professor = Convert.ToInt32(aAux.ElementAt(i));
                            itemOferecimento_Professores.id_oferecimento = item.id_oferecimento;
                            itemOferecimento_Professores.tipo_professor = "professor";
                            itemOferecimento_Professores.status = "alterado";
                            itemOferecimento_Professores.responsavel = true;
                            itemOferecimento_Professores.data_alteracao = DateTime.Now;
                            itemOferecimento_Professores.usuario = usuario.usuario;

                            aplicacaoOferecimento.AlterarProfessor_Tecnico_Oferecimento(itemOferecimento_Professores);
                        }
                    }

                    //foreach (GridViewRow row in grdProfessorAdicionado.Rows)
                    //{
                    //    if ((row.RowType == DataControlRowType.DataRow))
                    //    {
                    //        CheckBox chkResponsavel = row.Cells[3].FindControl("chkResponsavel") as CheckBox;
                    //        itemOferecimento_Professores = new Oferecimentos_professores();
                    //        itemOferecimento_Professores.id_professor= Convert.ToInt32(row.Cells[0].Text);
                    //        itemOferecimento_Professores.id_Oferecimento = item.id_Oferecimento;
                    //        itemOferecimento_Professores.tipo_professor = "professor";
                    //        if (chkResponsavel.Checked)
                    //        {
                    //            itemOferecimento_Professores.responsavel = true;
                    //        }
                    //        else
                    //        {
                    //            itemOferecimento_Professores.responsavel = false;
                    //        }
                    //        itemOferecimento_Professores.status = "alterado";
                    //        itemOferecimento_Professores.data_alteracao = DateTime.Now;
                    //        itemOferecimento_Professores.usuario = usuario.usuario;

                    //        aplicacaoOferecimento.AlterarProfessor_Tecnico_Oferecimento(itemOferecimento_Professores);
                    //    }

                    //}

                    item = aplicacaoOferecimento.BuscaItem(item);

                    if (item.oferecimentos_professores.Where(x => x.responsavel == true).FirstOrDefault() != null)
                    {
                        txtProfessorResponsavelOferecimento.Value = item.oferecimentos_professores.Where(x => x.responsavel == true).FirstOrDefault().professores.nome;
                    }
                    else
                    {
                        txtProfessorResponsavelOferecimento.Value = "";
                    }

                    lblMensagem.Text = "Oferecimento alterado com sucesso.";
                    lblTituloMensagem.Text = "Alteração do Oferecimento";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

                    Session["oferecimentos"] = item;

                    GeraEmentaPDF();

                }
                else
                {
                    //Insert
                    OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                    oferecimentos item = new oferecimentos();

                    item.id_disciplina = Convert.ToInt32(txtIdDisciplinaOferecimento.Value);
                    item.ativo = chkAtivoOferecimento_new.Checked;
                    item.num_oferecimento = Convert.ToInt32(txtNumeroOferecimanto.Value.Trim());
                    item.quadrimestre = ddlPeriodoOferecimento.SelectedValue;

                    if (txtNumeroMaxAlunosOferecimento.Value.Trim() != "")
                    {
                        item.num_max_alunos = Convert.ToInt32(txtNumeroMaxAlunosOferecimento.Value);
                    }

                    item.creditos = Convert.ToInt32(txtCreditosOferecimento.Value);
                    item.carga_horaria = Convert.ToInt32(txtCargaHorariaOferecimento.Value);

                    item.objetivo = txtObjetivoOferecimento.Value.Trim();
                    item.justificativa = txtJustificativaOferecimento.Value.Trim();
                    item.ementa = txtEmentaOferecimento.Value.Trim();
                    item.forma_avaliacao = txtFormaAvaliacaoOferecimento.Value.Trim();
                    item.material_utilizado = txtMaterialUtilizadoOferecimento.Value.Trim();
                    item.metodologia = txtMetodologiaOferecimento.Value.Trim();
                    item.conhecimentos_previos = txtConhecimentosPreviosOferecimento.Value.Trim();
                    item.programa_disciplina = txtProgramaOferecimento.Value.Trim();
                    item.bibliografia_basica = txtBibliografiaBasicaOferecimento.Value.Trim();
                    item.bibliografica_compl = txtBibliografiaComplementarOferecimento.Value.Trim();
                    item.observacao = txtObservacaoOferecimento.Value.Trim();

                    item.status = "cadastrado";
                    item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    item = aplicacaoOferecimento.CriarItem(item);

                    DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                    disciplinas itemDisciplina = new disciplinas();
                    oferecimentos_professores itemOferecimentoProfessor;

                    itemDisciplina.id_disciplina = item.id_disciplina;
                    itemDisciplina = aplicacaoDisciplina.BuscaItem(itemDisciplina);

                    foreach (var itemCada in itemDisciplina.disciplinas_professores)
                    {
                        itemOferecimentoProfessor = new oferecimentos_professores();
                        itemOferecimentoProfessor.id_oferecimento = item.id_oferecimento;
                        itemOferecimentoProfessor.id_professor = itemCada.id_professor;
                        itemOferecimentoProfessor.tipo_professor = itemCada.tipo_professor;
                        itemOferecimentoProfessor.responsavel = itemCada.responsavel;
                        itemOferecimentoProfessor.status = "cadastrado";
                        itemOferecimentoProfessor.data_cadastro = DateTime.Now;
                        itemOferecimentoProfessor.data_alteracao = DateTime.Now;
                        itemOferecimentoProfessor.usuario = usuario.usuario;
                        aplicacaoOferecimento.IncluirProfessor_Tecnico_Oferecimento(itemOferecimentoProfessor);
                    }


                    if (item != null)
                    {
                        Session["oferecimentos"] = item;
                        Session["sNewOferecimento"] = false;
                        Session["AdiciondoSucesso"] = true;
                        GeraEmentaPDF();
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na manutenção do Oferecimento. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Oferecimento";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }

        protected void btnGerarListaPresencaAluno_Click(object sender, EventArgs e)
        {
            try
            {
                oferecimentos item;
                item = (oferecimentos)Session["oferecimentos"];

                int qIdAula = Convert.ToInt32(Page.Request["hCodigo"]);

                string qData = String.Format("{0:dd_MM_yyyy}", item.datas_aulas.Where(x => x.id_aula == qIdAula).FirstOrDefault().data_aula);

                GerarListaPresencaAluno(qIdAula,qData);

                if (File.Exists(Server.MapPath("~/doctos/ListaAlunos_" + item.id_oferecimento.ToString() + "_" + qData + ".pdf")))
                {
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/ListaAlunos_" + item.id_oferecimento.ToString() + "_" + qData + ".pdf")));
                    Response.WriteFile(Server.MapPath("~/doctos/ListaAlunos_" + item.id_oferecimento.ToString() + "_" + qData + ".pdf"));
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão de Lista de Presença de Alunos no Oferecimento";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }
        }

        protected void GerarListaPresencaAluno(int qIdAula, string qData)
        {
            try
            {
                GeraisAplicacao AplicacaoGerais = new GeraisAplicacao();
                oferecimentos item;
                item = (oferecimentos)Session["oferecimentos"];
                DateTime dQDataAula = Convert.ToDateTime(qData.Replace("_", "/"));

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 120, 40);//estibulando o espaçamento das margens que queremos ===Antigo===(45, 40, 40, 40)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/ListaAlunos_" + item.id_oferecimento.ToString() + "_" + qData + ".pdf"), FileMode.Create));
                PDF_Cabec_Rodape_GeraEmentaPDF pageHeaderFooter = new PDF_Cabec_Rodape_GeraEmentaPDF();
                pageHeaderFooter.Caminho = Server.MapPath("~");
                pageHeaderFooter.TipoCurso = item.disciplinas.cursos_disciplinas.FirstOrDefault().cursos.tipos_curso.tipo_curso;
                writer.PageEvent = pageHeaderFooter;
                doc.Open();

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_11_Bold = new Font(_bfVerdanaNormal, 11, Font.BOLD);
                Font font_Verdana_11_Normal = new Font(_bfVerdanaNormal, 11, Font.NORMAL);
                Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Verdana_8_Bold = new Font(_bfVerdanaNormal, 8, Font.BOLD);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Verdana_7_Bold = new Font(_bfVerdanaNormal, 7, Font.BOLD);
                Font font_Verdana_7_Normal = new Font(_bfVerdanaNormal, 7, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);
                Color FontColor_Cinza = new Color(204, 204, 204);
                Color FontColor_CinzaClaro = new Color(229, 229, 229);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                float[] widths;
                Paragraph p;
                PdfPCell cell;
                PdfPTable table;

                //Aqui é uma nova tabela
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 230f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Lista de Presença", font_Verdana_14_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(String.Format("{0:dd/MM/yyyy}", item.datas_aulas.Where(x => x.id_aula == qIdAula).FirstOrDefault().data_aula) + " - " + dtfi.GetDayName(item.datas_aulas.Where(x => x.id_aula == qIdAula).FirstOrDefault().data_aula.Value.DayOfWeek) + " das " + String.Format("{0:HH:mm}", item.datas_aulas.Where(x => x.id_aula == qIdAula).FirstOrDefault().hora_inicio) + " às " + String.Format("{0:HH:mm}", item.datas_aulas.Where(x => x.id_aula == qIdAula).FirstOrDefault().hora_fim), font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui se desenha uma linha fina
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(linefine));
                doc.Add(p);



                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(2);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 63f, 280f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Curso", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.disciplinas.cursos_disciplinas.FirstOrDefault().cursos.sigla + " - " + item.disciplinas.cursos_disciplinas.FirstOrDefault().cursos.nome, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Disciplina", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.disciplinas.codigo + " - " + item.disciplinas.nome, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                doc.Add(table);

                //============================================================

                table = new PdfPTable(4);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 33f, 63f, 23f, 63f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Período", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.quadrimestre, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Alunos", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                int iAlunosFora = 0;
                foreach (var elemento in item.matricula_oferecimento.OrderBy(X => X.alunos.nome))
                {
                    if (elemento.alunos.matricula_turma.Where(x => x.id_turma == elemento.id_turma).SingleOrDefault().historico_matricula_turma.Any(x => (x.situacao == "Abandonou" || x.situacao == "Desligado") && dQDataAula >= x.data_inicio))
                    {
                        iAlunosFora++;
                    }
                }

                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk((item.matricula_oferecimento.Count - iAlunosFora).ToString(), font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                doc.Add(table);

                //====================

                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 63f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Professor(es)", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 1f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(2);
                table.TotalWidth = 400f;
                table.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.LockedWidth = true;
                widths = new float[] { 200f, 200f };
                table.SetWidths(widths);

                int i = 0;
                foreach (var elemento in item.datas_aulas.Where(x=> x.id_aula == qIdAula).FirstOrDefault().datas_aulas_professor.Where(x=> x.tipo_professor == "professor").OrderBy(x=> x.professores.nome))
                {
                    i++;
                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(linefine));
                    p.Add(new Chunk("\n" + elemento.professores.nome, font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 10f;
                    table.AddCell(cell);

                    ////Aqui se desenha a Coluna 1
                    //cell = new PdfPCell();
                    //p = new Paragraph();
                    //p.Add(new Chunk(elemento.professores.nome, font_Verdana_9_Bold));
                    //cell = new PdfPCell(p);
                    //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    //cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    //cell.Border = Rectangle.NO_BORDER;
                    //cell.PaddingBottom = 12f;
                    //table.AddCell(cell);

                }

                if (i % 2 != 0)
                {
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(" ", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 10f;
                    table.AddCell(cell);
                }
                doc.Add(table);

                List<datas_aulas_professor> lista_Tecnico;
                lista_Tecnico=item.datas_aulas.Where(x => x.id_aula == qIdAula).FirstOrDefault().datas_aulas_professor.Where(x => x.tipo_professor == "tecnico").ToList();
                if (lista_Tecnico.Count>0)
                {
                    table = new PdfPTable(1);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 63f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Técnico(s)", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 1f;
                    table.AddCell(cell);

                    doc.Add(table);

                    //Aqui é uma nova tabela de 2 Colunas ========================================================
                    table = new PdfPTable(2);
                    table.TotalWidth = 400f;
                    table.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.LockedWidth = true;
                    widths = new float[] { 200f, 200f };
                    table.SetWidths(widths);

                    i = 0;

                    foreach (var elemento in lista_Tecnico.OrderBy(x => x.professores.nome))
                    {
                        i++;
                        //Aqui se desenha a Coluna 1
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(linefine));
                        p.Add(new Chunk("\n" + elemento.professores.nome, font_Verdana_9_Bold));
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Border = Rectangle.NO_BORDER;
                        cell.PaddingBottom = 10f;
                        table.AddCell(cell);
                    }

                    if (i % 2 != 0)
                    {
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(" ", font_Verdana_9_Bold));
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Border = Rectangle.NO_BORDER;
                        cell.PaddingBottom = 10f;
                        table.AddCell(cell);
                    }
                    doc.Add(table);
                }

                if (txtMensagemLista.Value.Trim() != "")
                {
                    table = new PdfPTable(1);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 520f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Atenção:", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 1f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(txtMensagemLista.Value.Trim(), font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 1f;
                    table.AddCell(cell);

                    doc.Add(table);
                }

                //=====================

                //Aqui se desenha uma linha fina
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(linefine));
                doc.Add(p);

                //==================================

                //Aqui se desenha uma linha fina
                //p = new Paragraph();
                //p.Alignment = Element.ALIGN_CENTER;
                //p.Clear();
                //p.Add(new Chunk(linefine));
                //doc.Add(p);

                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(3);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 63f, 180f, 180f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Matrícula", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Nome", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Assinatura", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                doc.Add(table);

                table = new PdfPTable(3);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 63f, 180f, 180f };
                table.SetWidths(widths);

                foreach (var elemento in item.matricula_oferecimento.OrderBy(X=> X.alunos.nome))
                {
                    if (elemento.alunos.matricula_turma.Where(x => x.id_turma == elemento.id_turma).SingleOrDefault().historico_matricula_turma.Any(x => (x.situacao == "Abandonou" || x.situacao == "Desligado") && dQDataAula >= x.data_inicio))
                    {
                        continue;
                    }

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();

                    p.Add(new Chunk(elemento.alunos.idaluno.ToString(), font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 10f;
                    table.AddCell(cell);

                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(elemento.alunos.nome, font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 10f;
                    table.AddCell(cell);

                    cell = new PdfPCell();
                    p = new Paragraph();

                    //if (elemento.alunos.matricula_turma.Where(x => x.id_turma == elemento.id_turma).SingleOrDefault().historico_matricula_turma.Any(x => x.situacao == "Abandonou" && dQDataAula >= x.data_inicio))
                    //{
                    //    p.Add(new Chunk("Abandonou em " + elemento.alunos.matricula_turma.Where(x => x.id_turma == elemento.id_turma).FirstOrDefault().historico_matricula_turma.Where(x => x.situacao == "Abandonou").SingleOrDefault().data_inicio.Value.ToString("dd/MM/yyyy"), font_Verdana_9_Normal));
                    //}
                    //else if (elemento.alunos.matricula_turma.Where(x => x.id_turma == elemento.id_turma).SingleOrDefault().historico_matricula_turma.Any(x => x.situacao == "Desligado" && dQDataAula >= x.data_inicio))
                    //{
                    //    p.Add(new Chunk("Desligado em " + elemento.alunos.matricula_turma.Where(x => x.id_turma == elemento.id_turma).FirstOrDefault().historico_matricula_turma.Where(x => x.situacao == "Desligado").SingleOrDefault().data_inicio.Value.ToString("dd/MM/yyyy"), font_Verdana_9_Normal));
                    //}
                    //else
                    //{
                    //    p.Add(new Chunk(linefine));
                    //}

                    p.Add(new Chunk(linefine));

                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 10f;
                    table.AddCell(cell);
                }

                doc.Add(table);

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina que gera a Lista de Presença dos Professores";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }

        }

        protected void btnGerarListaPresencaProfessor_Click(object sender, EventArgs e)
        {
            try
            {
                oferecimentos item;
                item = (oferecimentos)Session["oferecimentos"];

                int qIdAula = Convert.ToInt32(Page.Request["hCodigo"]);

                string qData = String.Format("{0:dd_MM_yyyy}", item.datas_aulas.Where(x => x.id_aula == qIdAula).FirstOrDefault().data_aula);

                GerarListaPresencaProfessor(qIdAula, qData);

                if (File.Exists(Server.MapPath("~/doctos/ListaProfessores_" + item.id_oferecimento.ToString() + "_" + qData + ".pdf")))
                {
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/ListaProfessores_" + item.id_oferecimento.ToString() + "_" + qData + ".pdf")));
                    Response.WriteFile(Server.MapPath("~/doctos/ListaProfessores_" + item.id_oferecimento.ToString() + "_" + qData + ".pdf"));
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão da Lista de Presença dos Professores no Oferecimento";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }
        }

        protected void GerarListaPresencaProfessor(int qIdAula, string qData)
        {
            try
            {
                GeraisAplicacao AplicacaoGerais = new GeraisAplicacao();
                oferecimentos item;
                item = (oferecimentos)Session["oferecimentos"];

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 120, 40);//estibulando o espaçamento das margens que queremos ===Antigo===(45, 40, 40, 40)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/ListaProfessores_" + item.id_oferecimento.ToString() + "_" + qData + ".pdf"), FileMode.Create));
                PDF_Cabec_Rodape_GeraEmentaPDF pageHeaderFooter = new PDF_Cabec_Rodape_GeraEmentaPDF();
                pageHeaderFooter.Caminho = Server.MapPath("~");
                pageHeaderFooter.TipoCurso = item.disciplinas.cursos_disciplinas.FirstOrDefault().cursos.tipos_curso.tipo_curso;
                writer.PageEvent = pageHeaderFooter;
                doc.Open();

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_11_Bold = new Font(_bfVerdanaNormal, 11, Font.BOLD);
                Font font_Verdana_11_Normal = new Font(_bfVerdanaNormal, 11, Font.NORMAL);
                Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Verdana_8_Bold = new Font(_bfVerdanaNormal, 8, Font.BOLD);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Verdana_7_Bold = new Font(_bfVerdanaNormal, 7, Font.BOLD);
                Font font_Verdana_7_Normal = new Font(_bfVerdanaNormal, 7, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);
                Color FontColor_Cinza = new Color(204, 204, 204);
                Color FontColor_CinzaClaro = new Color(229, 229, 229);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                float[] widths;
                Paragraph p;
                PdfPCell cell;
                PdfPTable table;

                //Aqui é uma nova tabela
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 230f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Lista de Presença dos Professores", font_Verdana_14_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(String.Format("{0:dd/MM/yyyy}", item.datas_aulas.Where(x => x.id_aula == qIdAula).FirstOrDefault().data_aula) + " - " + dtfi.GetDayName(item.datas_aulas.Where(x => x.id_aula == qIdAula).FirstOrDefault().data_aula.Value.DayOfWeek) + " das " + String.Format("{0:HH:mm}", item.datas_aulas.Where(x => x.id_aula == qIdAula).FirstOrDefault().hora_inicio) + " às " + String.Format("{0:HH:mm}", item.datas_aulas.Where(x => x.id_aula == qIdAula).FirstOrDefault().hora_fim), font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui se desenha uma linha fina
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(linefine));
                doc.Add(p);



                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(2);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 63f, 280f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Curso", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.disciplinas.cursos_disciplinas.FirstOrDefault().cursos.sigla + " - " + item.disciplinas.cursos_disciplinas.FirstOrDefault().cursos.nome, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Disciplina", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.disciplinas.codigo + " - " + item.disciplinas.nome, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                doc.Add(table);

                //============================================================

                table = new PdfPTable(4);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 33f, 63f, 23f, 63f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Período", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.quadrimestre, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Alunos", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.matricula_oferecimento.Count.ToString(), font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                doc.Add(table);

                //====================

                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 63f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(" ", font_Verdana_12_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Professor(es)", font_Verdana_12_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(3);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 30f, 100f, 50f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("CPF", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Nome", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Assinatura", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(3);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 30f, 100f, 50f };
                table.SetWidths(widths);

                foreach (var elemento in item.datas_aulas.Where(x => x.id_aula == qIdAula).FirstOrDefault().datas_aulas_professor.Where(x => x.tipo_professor == "professor").OrderBy(x => x.professores.nome))
                {
                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(elemento.professores.cpf, font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 10f;
                    table.AddCell(cell);

                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(elemento.professores.nome, font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 10f;
                    table.AddCell(cell);

                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(linefine));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 10f;
                    table.AddCell(cell);
                }

                doc.Add(table);

                List<datas_aulas_professor> lista_Tecnico;
                lista_Tecnico = item.datas_aulas.Where(x => x.id_aula == qIdAula).FirstOrDefault().datas_aulas_professor.Where(x => x.tipo_professor == "tecnico").ToList();
                if (lista_Tecnico.Count > 0)
                {
                    //Aqui é uma nova tabela de 2 Colunas ========================================================
                    table = new PdfPTable(1);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 63f };
                    table.SetWidths(widths);

                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(" ", font_Verdana_12_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Técnico(s)", font_Verdana_12_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    doc.Add(table);

                    //Aqui é uma nova tabela de 2 Colunas ========================================================
                    table = new PdfPTable(3);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 30f, 100f, 50f };
                    table.SetWidths(widths);

                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("CPF", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 10f;
                    table.AddCell(cell);

                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Nome", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 10f;
                    table.AddCell(cell);

                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Assinatura", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 10f;
                    table.AddCell(cell);

                    doc.Add(table);

                    //Aqui é uma nova tabela de 2 Colunas ========================================================
                    table = new PdfPTable(3);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 30f, 100f, 50f };
                    table.SetWidths(widths);

                    foreach (var elemento in lista_Tecnico.OrderBy(x => x.professores.nome))
                    {
                        //Aqui se desenha a Coluna 1
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(elemento.professores.cpf, font_Verdana_9_Normal));
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Border = Rectangle.NO_BORDER;
                        cell.PaddingBottom = 10f;
                        table.AddCell(cell);

                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(elemento.professores.nome, font_Verdana_9_Normal));
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Border = Rectangle.NO_BORDER;
                        cell.PaddingBottom = 10f;
                        table.AddCell(cell);

                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(linefine));
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Border = Rectangle.NO_BORDER;
                        cell.PaddingBottom = 10f;
                        table.AddCell(cell);
                    }

                    doc.Add(table);
                }

                if (txtMensagemLista.Value.Trim() != "")
                {
                    //Aqui se desenha uma linha fina
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Clear();
                    p.Add(new Chunk(linefine));
                    doc.Add(p);

                    table = new PdfPTable(1);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 520f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Atenção:", font_Verdana_9_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 1f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(txtMensagemLista.Value.Trim(), font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 1f;
                    table.AddCell(cell);

                    doc.Add(table);
                }

                //=====================

                //Aqui se desenha uma linha fina
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(linefine));
                doc.Add(p);

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina que gera a Lista de Presença dos Professores";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }

        }

    }
}