using Aplicacao_C;
using SERPI.Dominio_C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class matPeriodoMatriculaGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 32)) // 1. Período de Matrícula - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                GeraisAplicacao aplicacaoGarais = new GeraisAplicacao();
                List<tipos_curso> listaTipoCurso = aplicacaoGarais.ListaTipoCurso();

                ddlTipoCursoPeriodoMatricula.Items.Clear();
                ddlTipoCursoPeriodoMatricula.DataSource = listaTipoCurso.OrderBy(x => x.id_tipo_curso);
                ddlTipoCursoPeriodoMatricula.DataValueField = "id_tipo_curso";
                ddlTipoCursoPeriodoMatricula.DataTextField = "tipo_curso";
                ddlTipoCursoPeriodoMatricula.DataBind();
                ddlTipoCursoPeriodoMatricula.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos Tipos de Curso", ""));
                ddlTipoCursoPeriodoMatricula.SelectedValue = "";

                if (Session["sNewPeriodoMatricula"] != null && (Boolean)Session["sNewPeriodoMatricula"] != true)
                {
                    periodo_matricula item;
                    item = (periodo_matricula)Session["periodo_matricula"];
                    lblTituloPagina.Text = "(Editar) - N.º " + item.id_periodo;
                    QuadrimestreAplicacao aplicacaoQuadrimestre = new QuadrimestreAplicacao();
                    quadrimestres item_quadrimestre = new quadrimestres();
                    item_quadrimestre.quadrimestre = item.quadrimestre;
                    item_quadrimestre = aplicacaoQuadrimestre.BuscaItem(item_quadrimestre);

                    txtDataCadastro.Value = String.Format("{0:dd/MM/yyyy}", item.data_cadastro);
                    txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy}", item.data_alteracao);
                    txtResponsavel.Value = item.usuario;

                    txtIdPeriodo.Value = item.id_periodo.ToString();
                    ddlTipoCursoPeriodoMatricula.SelectedValue = item_quadrimestre.id_tipo_curso.ToString();
                    ddlTipoCursoPeriodoMatricula_SelectedIndexChanged(null, null);
                    ddlPeriodoPeriodoMatriculaGestao.SelectedValue = item.quadrimestre;
                    txtPeriodo.Value = item.quadrimestre;
                    txtDataInicioPeriodoMatriculaGestao.Value = String.Format("{0:yyyy-MM-dd}", item.data_inicio);
                    txtDataFimPeriodoMatriculaGestao.Value = String.Format("{0:yyyy-MM-dd}", item.data_termino);

                    divDisciplinas.Visible = true;
                    btnCriarPeriodoMatricula.Disabled = false;
                    btnExcluirPeriodoMatricula.Disabled = false;
                    divEdicao.Visible = true;
                    ddlPeriodoPeriodoMatriculaGestao.Attributes.Add("disabled", "disabled");
                    ddlTipoCursoPeriodoMatricula.Attributes.Add("disabled", "disabled");

                    if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 32))
                    {
                        if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 32).FirstOrDefault().escrita != true)
                        {
                            btnCriarPeriodoMatricula.Visible = false;
                            btnExcluirPeriodoMatricula.Visible = false;
                            btnSalvarPeridodo.Visible = false;
                            btnSalvarPeridodo2.Visible = false;
                        }
                    }

                    if (Session["AdiciondoSucesso"] != null)
                    {
                        if ((Boolean)Session["AdiciondoSucesso"])
                        {
                            Session["AdiciondoSucesso"] = null;
                            lblMensagem.Text = "Novo Período de Matrícula adicionado com sucesso";
                            lblTituloMensagem.Text = "Novo Período de Matrícula";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
                        }

                    }

                }
                else
                {

                    lblTituloPagina.Text = "(novo)";
                    txtDataCadastro.Value = "";
                    txtDataAlteracao.Value = "";
                    txtResponsavel.Value = "";

                    txtIdPeriodo.Value = "";

                    ddlTipoCursoPeriodoMatricula.SelectedValue = "";
                    ddlPeriodoPeriodoMatriculaGestao.SelectedValue = "";
                    txtPeriodo.Value = "";
                    txtDataInicioPeriodoMatriculaGestao.Value = "";
                    txtDataFimPeriodoMatriculaGestao.Value = "";

                    divDisciplinas.Visible = false;
                    btnCriarPeriodoMatricula.Disabled = true;
                    btnExcluirPeriodoMatricula.Disabled = true;
                    divEdicao.Visible = false;
                    ddlPeriodoPeriodoMatriculaGestao.Attributes.Remove("disabled");
                    ddlTipoCursoPeriodoMatricula.Attributes.Remove("disabled");

                    if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 32))
                    {
                        if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 32).FirstOrDefault().escrita != true)
                        {
                            btnCriarPeriodoMatricula.Visible = false;
                            btnSalvarPeridodo.Visible = false;
                            btnSalvarPeridodo2.Visible = false;
                        }
                    }
                }
            }

        }

        public void ddlTipoCursoPeriodoMatricula_SelectedIndexChanged(Object sender, EventArgs e)
        {
            //if (ddlTipoCursoAluno.SelectedValue != "")
            //{
            QuadrimestreAplicacao aplicacaoPeriodo = new QuadrimestreAplicacao();
            tipos_curso item = new tipos_curso();

            if (ddlTipoCursoPeriodoMatricula.SelectedValue != "")
            {
                item.id_tipo_curso = Convert.ToInt32(ddlTipoCursoPeriodoMatricula.SelectedValue);
            }

            List<quadrimestres> lista = new List<quadrimestres>();
            lista = aplicacaoPeriodo.ListaItem(item);

            ddlPeriodoPeriodoMatriculaGestao.Items.Clear();
            ddlPeriodoPeriodoMatriculaGestao.DataSource = lista.OrderByDescending(x=> x.ano).ThenByDescending(x=> x.quadrimestre).ToList();
            ddlPeriodoPeriodoMatriculaGestao.DataValueField = "quadrimestre";
            ddlPeriodoPeriodoMatriculaGestao.DataTextField = "quadrimestre";
            ddlPeriodoPeriodoMatriculaGestao.DataBind();
            ddlPeriodoPeriodoMatriculaGestao.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Período", ""));
            ddlPeriodoPeriodoMatriculaGestao.SelectedValue = "";

            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fSelect2();", true);
        }

        protected void btnCriarPeriodoMatricula_Click(object sender, EventArgs e)
        {
            Session["sNewPeriodoMatricula"] = true;
            Session["periodo_matricula"] = null;
            Session["AdiciondoSucesso"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("matPeriodoMatricula.aspx", true);
        }

        protected void btnSalvarPeridodo_ServerClick(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";
                if (ddlPeriodoPeriodoMatriculaGestao.SelectedValue == "" && (Boolean)Session["sNewPeriodoMatricula"] != false)
                {
                    sAux = "Selecionar um Período. <br/><br/>";
                }
                

                if (txtDataInicioPeriodoMatriculaGestao.Value == "")
                {
                    sAux = sAux + "Digite uma Data Início. <br/><br/>";
                }

                if (txtDataFimPeriodoMatriculaGestao.Value == "")
                {
                    sAux = sAux + "Digite uma Data Fim. <br/><br/>";
                }

                if (sAux != "")
                {
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                    return;
                }

                if (Session["sNewPeriodoMatricula"] != null && (Boolean)Session["sNewPeriodoMatricula"] != true)
                {

                    MatriculaAplicacao aplicacaoMatricula = new MatriculaAplicacao();
                    periodo_matricula item = new periodo_matricula();

                    item = (periodo_matricula)Session["periodo_matricula"];
                    //item.quadrimestre  = ddlPeriodoPeriodoMatriculaGestao.SelectedValue;
                    item.data_inicio= Convert.ToDateTime(txtDataInicioPeriodoMatriculaGestao.Value);
                    item.data_termino = Convert.ToDateTime(txtDataFimPeriodoMatriculaGestao.Value);
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    aplicacaoMatricula.AlteraPeriodoMatricula(item);

                    item = aplicacaoMatricula.BuscaPeriodoMatricula(item);

                    lblMensagem.Text = "Período de Matrícula alterado com sucesso.";
                    lblTituloMensagem.Text = "Alteração de Período de Matrícula";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');fAtiva_grdDisciplina();", true);

                    Session["periodo_matricula"] = item;

                }
                else
                {
                    MatriculaAplicacao aplicacaoMatricula = new MatriculaAplicacao();
                    periodo_matricula item = new periodo_matricula();

                    item.quadrimestre = ddlPeriodoPeriodoMatriculaGestao.SelectedValue;
                    item.data_inicio = Convert.ToDateTime(txtDataInicioPeriodoMatriculaGestao.Value);
                    item.data_termino = Convert.ToDateTime(txtDataFimPeriodoMatriculaGestao.Value);
                    item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    item = aplicacaoMatricula.CriaPeriodoMatricula(item);

                    if (item != null)
                    {
                        Session["periodo_matricula"] = item;
                        Session.Add("sNewPeriodoMatricula", false);
                        Session["AdiciondoSucesso"] = true;
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na manutenção do Período de Matrícula. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Período de Matrícula";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }

        protected void btnExcluirPeriodoMatricula_ServerClick(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                MatriculaAplicacao aplicacaoMatricula = new MatriculaAplicacao();
                periodo_matricula item = new periodo_matricula();

                item = (periodo_matricula)Session["periodo_matricula"];
                //item.quadrimestre  = ddlPeriodoPeriodoMatriculaGestao.SelectedValue;
                item.data_inicio = Convert.ToDateTime(txtDataInicioPeriodoMatriculaGestao.Value);
                item.data_termino = Convert.ToDateTime(txtDataFimPeriodoMatriculaGestao.Value);
                item.data_alteracao = DateTime.Now;
                item.usuario = usuario.usuario;

                aplicacaoMatricula.ExcluiPeriodoMatricula(item);

                lblMensagem.Text = "Período de Matrícula Excluído com sucesso.";
                lblTituloMensagem.Text = "Exclusão de Período de Matrícula";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');fAtiva_grdDisciplina();", true);

                lblTituloPagina.Text = "(novo)";
                txtDataCadastro.Value = "";
                txtDataAlteracao.Value = "";
                txtResponsavel.Value = "";

                txtIdPeriodo.Value = "";

                ddlPeriodoPeriodoMatriculaGestao.SelectedValue = "";
                txtPeriodo.Value = "";
                txtDataInicioPeriodoMatriculaGestao.Value = "";
                txtDataFimPeriodoMatriculaGestao.Value = "";

                divDisciplinas.Visible = false;
                btnCriarPeriodoMatricula.Disabled = true;
                btnExcluirPeriodoMatricula.Disabled = true;
                divEdicao.Visible = false;
                ddlPeriodoPeriodoMatriculaGestao.Attributes.Remove("disabled");

                if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 32))
                {
                    if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 32).FirstOrDefault().escrita != true)
                    {
                        btnCriarPeriodoMatricula.Visible = false;
                        btnSalvarPeridodo.Visible = false;
                        btnSalvarPeridodo2.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na exclusão do Período de Matrícula. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Período de Matrícula";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }
    }
}