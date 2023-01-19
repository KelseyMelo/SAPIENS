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

namespace SERPI.UI.WebForms_C
{
    public partial class cadTurmaGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 15)) //7.Turmas - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                GeraisAplicacao aplicacaoGarais = new GeraisAplicacao();
                TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                List<tipos_curso> listaTipoCurso = aplicacaoGarais.ListaTipoCurso();

                ddlTipoCurso.Items.Clear();
                ddlTipoCurso.DataSource = listaTipoCurso.OrderBy(x => x.id_tipo_curso);
                ddlTipoCurso.DataValueField = "id_tipo_curso";
                ddlTipoCurso.DataTextField = "tipo_curso";
                ddlTipoCurso.DataBind();
                ddlTipoCurso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Tipo de Curso", ""));
                ddlTipoCurso.SelectedValue = "";

                //ddlNomeCursoArea.Attributes.Add("onselectedindexchanged", "javascript: ShowProgress();");
                //ddlCodigoCursoArea.Attributes.Add("onselectedindexchanged", "javascript: ShowProgress();");

                if (Session["sNewTurma"] != null && (Boolean)Session["sNewTurma"] != true)
                {
                    turmas item;
                    item = (turmas)Session["turmas"];
                    lblTituloPagina.Text = "(Editar) - N.º " + item.id_turma;
                    lblId.Text = item.id_turma.ToString();

                    if (!item.ativo && item.status != "inativado")
                    {
                        item.status = "inativado";
                        item = aplicacaoTurma.AlterarStatus(item);
                        Session["turmas"] = item;
                    }

                    if (item.status == "inativado")
                    {
                        lblInativado.Style["display"] = "block";
                        btnAtivar.Style["display"] = "block";
                        btnInativar.Style["display"] = "none";
                    }
                    else
                    {
                        lblInativado.Style["display"] = "none";
                        btnAtivar.Style["display"] = "none";
                        btnInativar.Style["display"] = "block";
                    }

                    txtDataCadastro.Value = String.Format("{0:dd/MM/yyyy}", item.data_cadastro);
                    txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy}", item.data_alteracao);
                    txtStatus.Value = item.status;
                    txtResponsavel.Value = item.usuario;

                    ddlTipoCurso.SelectedValue = item.cursos.id_tipo_curso.ToString();
                    ddlTipoCurso_SelectedIndexChanged(null, null);
                    txtNumeroTurma.Value = item.cod_turma;
                    ddlCursoTurma.SelectedValue = item.id_curso.ToString();
                    ddlTipoCurso.Attributes.Add("disabled", "disabled");
                    ddlCursoTurma.Attributes.Add("disabled", "disabled");
                    txtDataTerminoInscricaoTurma.Value = String.Format("{0:yyyy-MM-dd}", item.data_limite_matricula);

                    ddlPeriodoTurma.SelectedValue = item.quadrimestre;
                    txtDataInicioPeriodoTurma.Value = String.Format("{0:dd/MM/yyyy}", item.quadrimestres.data_inicio);
                    txtDataFimPeriodoTurma.Value = String.Format("{0:dd/MM/yyyy}", item.quadrimestres.data_fim);
                    //ddlPeriodoTurma.Attributes.Add("disabled", "disabled");
                    //inibido a pedido do Bruno no  dia 13/12/2018

                    txtDataInicioTurma.Value = String.Format("{0:yyyy-MM-dd}", item.data_inicio);
                    txtDataFimTurma.Value = String.Format("{0:yyyy-MM-dd}", item.data_fim);
                    //chkAtivoTurma.Checked = item.ativo;

                    txtCargaHorariaTurma.Value = item.carga_horaria.ToString();
                    txtCreditosTurma.Value = item.creditos.ToString();
                    txtNumeroMaxDisciplinaTurma.Value = item.num_max_disciplinas.ToString();

                    txtPortatiaMEC_Turma.Value = item.portaria_mec;
                    txtDataPortatiaMEC_Turma.Value = String.Format("{0:yyyy-MM-dd}", item.data_portaria_mec);
                    txtDataDiarioOficialTurma.Value = String.Format("{0:yyyy-MM-dd}", item.data_diario_oficial);
                    txtConceitoCapesTurma.Value = item.conceito_capes;
                    txtNumeroCapesTurma.Value = item.numero_capes;

                    ddlAreaConcentracaoTurma.Items.Clear();
                    if (item.cursos.areas_concentracao.Count >0)
                    {
                        ddlAreaConcentracaoTurma.DataSource = item.cursos.areas_concentracao.OrderBy(x=> x.nome);
                        ddlAreaConcentracaoTurma.DataValueField = "id_area_concentracao";
                        ddlAreaConcentracaoTurma.DataTextField = "nome";
                        ddlAreaConcentracaoTurma.DataBind();
                        ddlAreaConcentracaoTurma.SelectedValue = item.cursos.areas_concentracao.OrderBy(x => x.nome).FirstOrDefault().id_area_concentracao.ToString();
                    }
                    else
                    {
                        ddlAreaConcentracaoTurma.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Sem áreas de concentração", ""));
                        ddlAreaConcentracaoTurma.SelectedValue = "";
                    }
                    

                    if (item.cursos.id_tipo_curso == 1) //Mestrado
                    {
                        spanAsterisco_txtConceitoCapesTurma.Style.Add("display", "inline-block");
                        spanAsterisco_txtDataDiarioOficialTurma.Style.Add("display", "inline-block");
                        spanAsterisco_txtDataPortatiaMEC_Turma.Style.Add("display", "inline-block");
                        spanAsterisco_txtNumeroCapesTurma.Style.Add("display", "inline-block");
                        spanAsterisco_txtPortatiaMEC_Turma.Style.Add("display", "inline-block");
                    }
                    else
                    {
                        spanAsterisco_txtConceitoCapesTurma.Style.Add("display", "none");
                        spanAsterisco_txtDataDiarioOficialTurma.Style.Add("display", "none");
                        spanAsterisco_txtDataPortatiaMEC_Turma.Style.Add("display", "none");
                        spanAsterisco_txtNumeroCapesTurma.Style.Add("display", "none");
                        spanAsterisco_txtPortatiaMEC_Turma.Style.Add("display", "none");
                    }

                    txtObservacaoTurma.Value = item.observacao;

                    //PreencheCoordenadorAdicionado(item.turmas_coordenadores.ToList());

                    //PreencheDisciplinaAdicionada(item.turmas_disciplinas.ToList());

                    divCoordenadores.Visible = true;
                    divDisciplinas.Visible = true;
                    btnCriarTurma.Disabled = false;
                    divEdicao.Visible = true;

                    tabCoordenadoresTurma.Style["display"] = "block";
                    tabDisciplinaTurma.Style["display"] = "block";
                    tabMatriculaTurma.Style["display"] = "block";

                    if (Session["AdiciondoSucesso"] != null)
                    {
                        if ((Boolean)Session["AdiciondoSucesso"])
                        {
                            Session["AdiciondoSucesso"] = null;
                            lblMensagem.Text = "Nova Turma <strong>" + item.cod_turma + "</strong> adicionada com sucesso";
                            lblTituloMensagem.Text = "Nova Turma";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
                        }

                    }

                }
                else
                {
                    lblInativado.Style["display"] = "none";
                    btnAtivar.Style["display"] = "none";
                    btnInativar.Style["display"] = "none";

                    lblTituloPagina.Text = "(novo)";
                    txtDataCadastro.Value = "";
                    txtDataAlteracao.Value = "";
                    txtStatus.Value = "";
                    txtResponsavel.Value = "";

                    txtNumeroTurma.Value = "";
                    ddlTipoCurso.SelectedValue = "";
                    ddlCursoTurma.Items.Clear();
                    ddlTipoCurso.Attributes.Remove("disabled");
                    ddlCursoTurma.Attributes.Remove("disabled");

                    ddlPeriodoTurma.Items.Clear();
                    txtDataInicioPeriodoTurma.Value = "";
                    txtDataFimPeriodoTurma.Value = "";
                    ddlTipoCurso.Attributes.Remove("disabled");

                    txtDataInicioTurma.Value = "";
                    txtDataFimTurma.Value = "";
                    //chkAtivoTurma.Checked = false;

                    txtCargaHorariaTurma.Value = "";
                    txtCreditosTurma.Value = "";
                    txtNumeroMaxDisciplinaTurma.Value = "";

                    txtObservacaoTurma.Value = "";

                    txtPortatiaMEC_Turma.Value = "";
                    txtDataPortatiaMEC_Turma.Value = "";
                    txtDataDiarioOficialTurma.Value = "";
                    txtConceitoCapesTurma.Value = "";
                    txtNumeroCapesTurma.Value = "";

                    spanAsterisco_txtConceitoCapesTurma.Style.Add("display", "none");
                    spanAsterisco_txtDataDiarioOficialTurma.Style.Add("display", "none");
                    spanAsterisco_txtDataPortatiaMEC_Turma.Style.Add("display", "none");
                    spanAsterisco_txtNumeroCapesTurma.Style.Add("display", "none");
                    spanAsterisco_txtPortatiaMEC_Turma.Style.Add("display", "none");

                    divCoordenadores.Visible = false;
                    divDisciplinas.Visible = false;
                    btnCriarTurma.Disabled = true;
                    divEdicao.Visible = false;

                    tabCoordenadoresTurma.Style["display"] = "none";
                    tabDisciplinaTurma.Style["display"] = "none";
                    tabMatriculaTurma.Style["display"] = "none";
                }
            }

        }

        public void ddlTipoCurso_SelectedIndexChanged(Object sender, EventArgs e)
        {
            txtNumeroTurma.Value = "";

            txtCargaHorariaTurma.Value = "";
            txtCreditosTurma.Value = "";
            txtNumeroMaxDisciplinaTurma.Value = "";

            txtPortatiaMEC_Turma.Value = "";
            txtDataPortatiaMEC_Turma.Value = "";
            txtDataDiarioOficialTurma.Value = "";
            txtConceitoCapesTurma.Value = "";
            txtNumeroCapesTurma.Value = "";

            txtObservacaoTurma.Value = "";

            if (ddlTipoCurso.SelectedValue != "")
            {
                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos item = new cursos();
                item.id_tipo_curso = Convert.ToInt32(ddlTipoCurso.SelectedValue);
                List<cursos> listaCurso = aplicacaoCurso.ListaItem(item);
                var lista = from item2 in listaCurso
                            select new
                            {
                                id_curso = item2.id_curso,
                                nome = item2.sigla + " - " + item2.nome
                            };

                ddlCursoTurma.Items.Clear();
                ddlCursoTurma.DataSource = lista.OrderBy(x => x.nome);
                ddlCursoTurma.DataValueField = "id_curso";
                ddlCursoTurma.DataTextField = "nome";
                ddlCursoTurma.DataBind();
                ddlCursoTurma.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Curso", ""));
                ddlCursoTurma.SelectedValue = "";

                QuadrimestreAplicacao aplicacaoPeriodo = new QuadrimestreAplicacao();
                quadrimestres itemPeriodo = new quadrimestres();
                itemPeriodo.id_tipo_curso = Convert.ToInt32(ddlTipoCurso.SelectedValue);
                List<quadrimestres> listaPeriodo = aplicacaoPeriodo.ListaItem(itemPeriodo);
                ddlPeriodoTurma.Items.Clear();
                ddlPeriodoTurma.DataSource = listaPeriodo.OrderByDescending(x => x.quadrimestre);
                ddlPeriodoTurma.DataValueField = "quadrimestre";
                ddlPeriodoTurma.DataTextField = "quadrimestre";
                ddlPeriodoTurma.DataBind();
                ddlPeriodoTurma.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Período", ""));
                ddlPeriodoTurma.SelectedValue = "";

                if (ddlTipoCurso.SelectedValue == "1") //Mestrado
                {
                    spanAsterisco_txtConceitoCapesTurma.Style.Add("display", "inline-block");
                    spanAsterisco_txtDataDiarioOficialTurma.Style.Add("display", "inline-block");
                    spanAsterisco_txtDataPortatiaMEC_Turma.Style.Add("display", "inline-block");
                    spanAsterisco_txtNumeroCapesTurma.Style.Add("display", "inline-block");
                    spanAsterisco_txtPortatiaMEC_Turma.Style.Add("display", "inline-block");
                }
                else
                {
                    spanAsterisco_txtConceitoCapesTurma.Style.Add("display", "none");
                    spanAsterisco_txtDataDiarioOficialTurma.Style.Add("display", "none");
                    spanAsterisco_txtDataPortatiaMEC_Turma.Style.Add("display", "none");
                    spanAsterisco_txtNumeroCapesTurma.Style.Add("display", "none");
                    spanAsterisco_txtPortatiaMEC_Turma.Style.Add("display", "none");
                }
            }
            else
            {
                ddlCursoTurma.Items.Clear();
                ddlPeriodoTurma.Items.Clear();
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fSelect2();", true);
        }

        public void ddlPeriodoTurma_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (ddlPeriodoTurma.SelectedValue != "")
            {
                QuadrimestreAplicacao aplicacaoPeriodo = new QuadrimestreAplicacao();
                quadrimestres item = new quadrimestres();
                item.quadrimestre = ddlPeriodoTurma.SelectedValue;
                item = aplicacaoPeriodo.BuscaItem(item);
                txtDataInicioPeriodoTurma.Value = String.Format("{0:dd/MM/yyyy}", item.data_inicio);
                txtDataFimPeriodoTurma.Value = String.Format("{0:dd/MM/yyyy}", item.data_fim);
            }
            else
            {
                txtDataInicioPeriodoTurma.Value = "";
                txtDataFimPeriodoTurma.Value = "";
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fSelect2();", true);
        }

        public void ddlCursoTurma_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (ddlCursoTurma.SelectedValue != "")
            {
                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                cursos item = new cursos();
                turmas itemTurma = new turmas();
                item.id_curso = Convert.ToInt32(ddlCursoTurma.SelectedValue);
                item = aplicacaoCurso.BuscaItem(item);

                itemTurma.id_curso = item.id_curso;
                txtNumeroTurma.Value = aplicacaoTurma.BuscaItem_NumeroMaximo(itemTurma);

                txtCargaHorariaTurma.Value = item.carga_horaria.ToString();
                txtCreditosTurma.Value = item.creditos.ToString();
                txtNumeroMaxDisciplinaTurma.Value = item.num_max_disciplinas.ToString();

                txtPortatiaMEC_Turma.Value = item.portaria_mec;
                txtDataPortatiaMEC_Turma.Value = String.Format("{0:yyyy-MM-dd}", item.data_portaria_mec);
                txtDataDiarioOficialTurma.Value = String.Format("{0:yyyy-MM-dd}", item.data_diario_oficial);
                txtConceitoCapesTurma.Value = item.conceito_capes;
                txtNumeroCapesTurma.Value = item.numero_capes;
                txtObservacaoTurma.Value = item.observacao;
            }
            else
            {
                txtNumeroTurma.Value = "";

                txtCargaHorariaTurma.Value = "";
                txtCreditosTurma.Value = "";
                txtNumeroMaxDisciplinaTurma.Value = "";

                txtPortatiaMEC_Turma.Value = "";
                txtDataPortatiaMEC_Turma.Value = "";
                txtDataDiarioOficialTurma.Value = "";
                txtConceitoCapesTurma.Value = "";
                txtNumeroCapesTurma.Value = "";

                txtObservacaoTurma.Value = "";
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fSelect2();", true);
        }

        protected void btnCriarTurma_Click(object sender, EventArgs e)
        {
            Session["sNewTurma"] = true;
            Session["turmas"] = null;
            Session["AdiciondoSucesso"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("cadTurma.aspx", true);
        }

        protected void btnSalvar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";
                if (ddlTipoCurso.SelectedValue == "")
                {
                    sAux = "Selecione o Tipo do Curso. <br/><br/>";
                }
                if (ddlCursoTurma.SelectedValue == "")
                {
                    sAux = sAux + "Selecione o Curso da Turma. <br/><br/>";
                }
                if (ddlPeriodoTurma.SelectedValue == "")
                {
                    sAux = sAux + "Selecione o Período de início da Turma. <br/><br/>";
                }
                if (txtDataInicioTurma.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a Data de Início da Turma. <br/><br/>";
                }
                if (txtDataFimTurma.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a Data Fim da Turma. <br/><br/>";
                }
                if (txtDataTerminoInscricaoTurma.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a Data Limite Matrícula Candidatos. <br/><br/>";
                }

                if (ddlTipoCurso.SelectedValue == "1")
                {
                    //if (txtPortatiaMEC_Turma.Value.Trim() == "")
                    //{
                    //    sAux = sAux + "Preencher a Portaria MEC. <br/><br/>";
                    //}

                    if (txtDataPortatiaMEC_Turma.Value.Trim() == "")
                    {
                        sAux = sAux + "Preencher a Data Portaria MEC. <br/><br/>";
                    }

                    if (txtDataDiarioOficialTurma.Value.Trim() == "")
                    {
                        sAux = sAux + "Preencher a Data Diário Oficial. <br/><br/>";
                    }

                    if (txtConceitoCapesTurma.Value.Trim() == "")
                    {
                        sAux = sAux + "Preencher o Conceito na CAPES. <br/><br/>";
                    }

                    if (txtNumeroCapesTurma.Value.Trim() == "")
                    {
                        sAux = sAux + "Preencher o Número na CAPES. <br/><br/>";
                    }
                }

                if (sAux != "")
                {
                    if (ddlTipoCurso.SelectedValue == "1") //Mestrado
                    {
                        spanAsterisco_txtConceitoCapesTurma.Style.Add("display", "inline-block");
                        spanAsterisco_txtDataDiarioOficialTurma.Style.Add("display", "inline-block");
                        spanAsterisco_txtDataPortatiaMEC_Turma.Style.Add("display", "inline-block");
                        spanAsterisco_txtNumeroCapesTurma.Style.Add("display", "inline-block");
                        spanAsterisco_txtPortatiaMEC_Turma.Style.Add("display", "inline-block");
                    }
                    else
                    {
                        spanAsterisco_txtConceitoCapesTurma.Style.Add("display", "none");
                        spanAsterisco_txtDataDiarioOficialTurma.Style.Add("display", "none");
                        spanAsterisco_txtDataPortatiaMEC_Turma.Style.Add("display", "none");
                        spanAsterisco_txtNumeroCapesTurma.Style.Add("display", "none");
                        spanAsterisco_txtPortatiaMEC_Turma.Style.Add("display", "none");
                    }

                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                    return;
                }

                if (Session["sNewTurma"] != null && (Boolean)Session["sNewTurma"] != true)
                {
                    //alterar turma
                    TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                    turmas item = new turmas();

                    item = (turmas)Session["turmas"];

                    //item.ativo = chkAtivoTurma.Checked;

                    item.data_inicio = Convert.ToDateTime(txtDataInicioTurma.Value);
                    item.data_fim = Convert.ToDateTime(txtDataFimTurma.Value);

                    if (txtCargaHorariaTurma.Value.Trim() != "")
                    {
                        item.carga_horaria = Convert.ToInt32(txtCargaHorariaTurma.Value);
                    }

                    if (txtCreditosTurma.Value.Trim() != "")
                    {
                        item.creditos = Convert.ToInt32(txtCreditosTurma.Value);
                    }

                    if (txtNumeroMaxDisciplinaTurma.Value.Trim() != "")
                    {
                        item.num_max_disciplinas = Convert.ToInt32(txtNumeroMaxDisciplinaTurma.Value);
                    }

                    item.portaria_mec = txtPortatiaMEC_Turma.Value.Trim();

                    if (txtDataPortatiaMEC_Turma.Value != "")
                    {
                        item.data_portaria_mec = Convert.ToDateTime(txtDataPortatiaMEC_Turma.Value);
                    }

                    if (txtDataDiarioOficialTurma.Value != "")
                    {
                        item.data_diario_oficial = Convert.ToDateTime(txtDataDiarioOficialTurma.Value);
                    }

                    item.data_limite_matricula = Convert.ToDateTime(txtDataTerminoInscricaoTurma.Value);

                    item.conceito_capes = txtConceitoCapesTurma.Value.Trim();
                    item.numero_capes = txtNumeroCapesTurma.Value.Trim();

                    item.observacao = txtObservacaoTurma.Value.Trim();

                    item.status = "alterado";
                    //item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    item.quadrimestre = ddlPeriodoTurma.SelectedValue;

                    aplicacaoTurma.AlterarItem(item);

                    item = aplicacaoTurma.BuscaItem(item);

                    lblMensagem.Text = "Turma alterada com sucesso.";
                    lblTituloMensagem.Text = "Alteração de Turma";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

                    Session["turmas"] = item;

                }
                else
                {
                    //cria nova turma
                    TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
                    turmas item = new turmas();

                    item.ativo = true;
                    item.id_curso = Convert.ToInt32(ddlCursoTurma.SelectedValue);
                    item.cod_turma = aplicacaoTurma.BuscaItem_NumeroMaximo(item);
                    item.data_inicio = Convert.ToDateTime(txtDataInicioTurma.Value);
                    item.data_fim = Convert.ToDateTime(txtDataFimTurma.Value);
                    item.quadrimestre = ddlPeriodoTurma.SelectedValue;

                    item.data_limite_matricula = Convert.ToDateTime(txtDataTerminoInscricaoTurma.Value);

                    if (txtCargaHorariaTurma.Value.Trim() != "")
                    {
                        item.carga_horaria = Convert.ToInt32(txtCargaHorariaTurma.Value);
                    }

                    if (txtCreditosTurma.Value.Trim() != "")
                    {
                        item.creditos = Convert.ToInt32(txtCreditosTurma.Value);
                    }

                    if (txtNumeroMaxDisciplinaTurma.Value.Trim() != "")
                    {
                        item.num_max_disciplinas = Convert.ToInt32(txtNumeroMaxDisciplinaTurma.Value);
                    }

                    item.portaria_mec = txtPortatiaMEC_Turma.Value.Trim();

                    if (txtDataPortatiaMEC_Turma.Value != "")
                    {
                        item.data_portaria_mec = Convert.ToDateTime(txtDataPortatiaMEC_Turma.Value);
                    }

                    if (txtDataDiarioOficialTurma.Value != "")
                    {
                        item.data_diario_oficial = Convert.ToDateTime(txtDataDiarioOficialTurma.Value);
                    }
                    item.conceito_capes = txtConceitoCapesTurma.Value.Trim();
                    item.numero_capes = txtNumeroCapesTurma.Value.Trim();

                    item.observacao = txtObservacaoTurma.Value.Trim();

                    item.status = "cadastrado";
                    item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    item = aplicacaoTurma.CriarItem(item);

                    CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                    cursos itemCurso = new cursos();
                    List<cursos_coordenadores> listaCooordenadores = new List<cursos_coordenadores>();
                    List<disciplinas> listaDisciplinas = new List<disciplinas>();

                    turmas_coordenadores pItem_coordenadores;
                    turmas_disciplinas pItem_disciplinas;

                    itemCurso.id_curso = item.id_curso;

                    listaCooordenadores = aplicacaoCurso.ListaCoordenadores(itemCurso);
                    listaDisciplinas = aplicacaoCurso.ListaDisciplinas(itemCurso);

                    foreach (var itemCoordenador in listaCooordenadores)
                    {
                        pItem_coordenadores = new turmas_coordenadores();
                        pItem_coordenadores.id_turma = item.id_turma;
                        pItem_coordenadores.id_professor = itemCoordenador.id_professor;
                        pItem_coordenadores.status = "cadastrado";
                        pItem_coordenadores.data_cadastro = DateTime.Now;
                        pItem_coordenadores.data_alteracao = DateTime.Now;
                        pItem_coordenadores.usuario = usuario.usuario;

                        aplicacaoTurma.IncluirCoordenador_Turma(pItem_coordenadores);
                    }

                    foreach (var itemDisciplina in listaDisciplinas)
                    {
                        pItem_disciplinas = new turmas_disciplinas();
                        pItem_disciplinas.id_turma = item.id_turma;
                        pItem_disciplinas.id_disciplina = itemDisciplina.id_disciplina;
                        pItem_disciplinas.status = "cadastrado";
                        pItem_disciplinas.data_cadastro = DateTime.Now;
                        pItem_disciplinas.data_alteracao = DateTime.Now;
                        pItem_disciplinas.usuario = usuario.usuario;

                        aplicacaoTurma.IncluirDisciplina_Turma(pItem_disciplinas);
                    }

                    if (item != null)
                    {
                        QuadrimestreAplicacao aplicacaoQuadrimestre = new QuadrimestreAplicacao();
                        quadrimestres itemQuadrimestre = new quadrimestres();
                        itemQuadrimestre.quadrimestre = ddlPeriodoTurma.SelectedValue;
                        itemQuadrimestre = aplicacaoQuadrimestre.BuscaItem(itemQuadrimestre);

                        item.quadrimestres = new quadrimestres();
                        item.quadrimestres = itemQuadrimestre;


                        cursos itemCurso2 = new cursos();
                        itemCurso2.id_curso = item.id_curso;
                        itemCurso2 = aplicacaoCurso.BuscaItem(itemCurso2);

                        item.cursos = new cursos();

                        item.cursos = itemCurso2;

                        //item = aplicacaoTurma.BuscaItem(item);

                        Session["turmas"] = item;
                        Session.Add("sNewTurma", false);
                        Session["AdiciondoSucesso"] = true;
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na manutenção da Turma. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Curso";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }

        public class PDF_Cabec_Rodape : PdfPageEventHelper
        {
            public string Caminho;
            public string qTipoCurso;
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
                p.Add(new Chunk("  \r\n", font_Verdana_8_Normal));
                p.Add(new Chunk(qTipoCurso.ToUpper() + "  \r\n", font_Verdana_8_Normal));
                //p.Add(new Chunk("MESTRADO PROFISSIONAL  \r\n", font_Verdana_8_Normal));
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

        protected void btnImprimirComprovanteMatricula_Click(object sender, EventArgs e)
        {
            try
            {
                string qId = HttpContext.Current.Request["hCodigo"];

                turmas item_Turma;
                item_Turma = (turmas)Session["turmas"];

                matricula_turma item;
                item = item_Turma.matricula_turma.Where(x => x.id_matricula_turma == Convert.ToInt32(qId)).SingleOrDefault();

                GeraRelatorioComprovanteMatricula();

                if (File.Exists(Server.MapPath("~/doctos/Comprovante_Matricula_" + item.id_aluno.ToString() + "_" + item.turmas.cod_turma + ".pdf")))
                {
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName("~/doctos/Comprovante_Matricula_" + item.id_aluno.ToString() + "_" + item.turmas.cod_turma + ".pdf"));
                    Response.WriteFile(Server.MapPath("~/doctos/Comprovante_Matricula_" + item.id_aluno.ToString() + "_" + item.turmas.cod_turma + ".pdf"));
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão de comprovante de matrícula do aluno";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }
        }

        protected void GeraRelatorioComprovanteMatricula()
        {
            try
            {
                string qId = HttpContext.Current.Request["hCodigo"];

                GeraisAplicacao AplicacaoGerais = new GeraisAplicacao();
                turmas item_Turma;
                item_Turma = (turmas)Session["turmas"];

                matricula_turma item;
                item = item_Turma.matricula_turma.Where(x=> x.id_matricula_turma == Convert.ToInt32(qId)).SingleOrDefault();


                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 120, 40);//estibulando o espaçamento das margens que queremos ===Antigo===(45, 40, 40, 40)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/Comprovante_Matricula_" + item.id_aluno.ToString() + "_" + item.turmas.cod_turma + ".pdf"), FileMode.Create));
                PDF_Cabec_Rodape pageHeaderFooter = new PDF_Cabec_Rodape();
                pageHeaderFooter.Caminho = Server.MapPath("~");
                pageHeaderFooter.qTipoCurso = item.turmas.cursos.tipos_curso.tipo_curso;
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
                var linefine_white = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.WHITE, Element.ALIGN_LEFT, 1);
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
                p.Add(new Chunk("COMPROVANTE DE MATRÍCULA", font_Verdana_14_Bold));
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
                p.Add(new Chunk(linefine_white));
                doc.Add(p);



                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(4);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 26f, 60f, 30f, 60f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Matrícula:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.id_aluno.ToString(), font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Turma:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item_Turma.cod_turma, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //======================

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Nome:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.alunos.nome, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //==================================

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Curso:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item_Turma.cursos.nome, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Área de Concentração:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                if (item.areas_concentracao != null)
                {
                    p.Add(new Chunk(item.areas_concentracao.nome, font_Verdana_9_Normal));
                }
                else
                {
                    p.Add(new Chunk("sem área de concentração", font_Verdana_9_Normal));
                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //================================

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Data de Matrícula:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(String.Format("{0:dd/MM/yyyy}", item.data_cadastro), font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Início das Aulas:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(String.Format("{0:dd/MM/yyyy}", item_Turma.quadrimestres.data_inicio), font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui se desenha uma linha fina
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(linefine_white));
                doc.Add(p);

                //===============================================


                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
                List<oferecimentos> listaOferecimento = new List<oferecimentos>();
                listaOferecimento = aplicacaoAluno.ListaOferecimentosAluno(Convert.ToInt32(item.id_aluno), Convert.ToInt32(item.id_turma));

                if (listaOferecimento.Count == 0)
                {
                    //Aqui é uma nova tabela
                    table = new PdfPTable(1);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;

                    widths = new float[] { 230f };
                    table.SetWidths(widths);

                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk("Não há disciplinas matriculadas para esse aluno", font_Verdana_10_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);
                    
                }
                else
                {
                    //Aqui é uma nova tabela
                    table = new PdfPTable(1);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;

                    widths = new float[] { 230f };
                    table.SetWidths(widths);

                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Add(new Chunk("Disciplinas Matriculadas", font_Verdana_10_Bold));
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
                    p.Add(new Chunk(linefine_white));
                    doc.Add(p);

                    //===============================================

                    //Aqui é uma nova tabela de 6 Colunas ========================================================
                    table = new PdfPTable(5);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 17f, 17f, 47f, 11f, 11f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Quadr.", font_Verdana_8_Bold));
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
                    p.Add(new Chunk("Disciplina", font_Verdana_8_Bold));
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
                    p.Add(new Chunk("Duração", font_Verdana_8_Bold));
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
                    p.Add(new Chunk("Resultado", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    int i = 0;

                    foreach (var elemento in listaOferecimento)
                    {
                        i++;
                        //Aqui se desenha a Coluna 1
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(elemento.quadrimestre, font_Arialn_8_Normal));
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

                        //Aqui se desenha a Coluna 2
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(elemento.disciplinas.codigo, font_Arialn_8_Normal));
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
                        p.Add(new Chunk(elemento.disciplinas.nome, font_Arialn_8_Normal));
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
                        p.Add(new Chunk(elemento.carga_horaria.ToString() + " h", font_Arialn_8_Normal));
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

                        //Aqui se desenha a Coluna 5
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk((elemento.notas.Where(x => x.id_aluno == item.id_aluno).FirstOrDefault() == null) ? "" : (elemento.notas.Where(x => x.id_aluno == item.id_aluno).FirstOrDefault().conceitos_de_aprovacao == null) ? "" : elemento.notas.Where(x => x.id_aluno == item.id_aluno).FirstOrDefault().conceitos_de_aprovacao.descricao, font_Arialn_8_Normal));
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

    }
}