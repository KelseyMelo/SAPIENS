using Aplicacao_C;
using SERPI.Dominio_C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class proPeriodoInscricaoGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 10)) // Período de Inscricao - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                QuadrimestreAplicacao aplicacaoPeriodo = new QuadrimestreAplicacao();
                cursos itemCurso = new cursos();
                List<quadrimestres> listaCurso = aplicacaoPeriodo.ListaItem();

                if (Session["sNewPeriodoInscricao"] != null && (Boolean)Session["sNewPeriodoInscricao"] != true)
                {
                    periodo_inscricao item;
                    item = (periodo_inscricao)Session["periodo_inscricao"];
                    lblTituloPagina.Text = "(Editar) - N.º " + item.id_periodo;

                    txtIdPeriodoInscricao.Value = item.id_periodo.ToString();
                    txtPeriodoInscricao.Value = item.quadrimestre;
                    txtDataInicioPeriodoInscricaoGestao.Value = String.Format("{0:yyyy-MM-dd}", item.data_inicio);
                    txtDataFimPeriodoInscricaoGestao.Value = String.Format("{0:yyyy-MM-dd}", item.data_fim);

                    txtDataLimitePagtoPeriodoInscricaoGestao.Value = String.Format("{0:yyyy-MM-dd}", item.data_limite_pagamento);
                    txtDataProvaPeriodoInscricaoGestao.Value = String.Format("{0:yyyy-MM-dd}", item.data_prova);

                    divCursos.Visible = true;
                    btnCriarPeriodoInscricao.Disabled = false;
                    btnExcluirPeriodoInscricao.Disabled = false;

                    if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 10))
                    {
                        if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 10).FirstOrDefault().escrita != true)
                        {
                            btnCriarPeriodoInscricao.Visible = false;
                            btnExcluirPeriodoInscricao.Visible = false;
                            btnSalvarPeriodoInscricao.Visible = false;
                        }
                    }

                    if (Session["AdiciondoSucesso"] != null)
                    {
                        if ((Boolean)Session["AdiciondoSucesso"])
                        {
                            Session["AdiciondoSucesso"] = null;
                            lblMensagem.Text = "Novo Período de Inscrição adicionado com sucesso";
                            lblTituloMensagem.Text = "Novo Período de Inscrição";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
                        }

                    }

                }
                else
                {

                    lblTituloPagina.Text = "(novo)";

                    txtIdPeriodoInscricao.Value = "";

                    txtPeriodoInscricao.Value = "";
                    txtDataInicioPeriodoInscricaoGestao.Value = "";
                    txtDataFimPeriodoInscricaoGestao.Value = "";
                    txtDataLimitePagtoPeriodoInscricaoGestao.Value = "";
                    txtDataProvaPeriodoInscricaoGestao.Value = "";

                    divCursos.Visible = false;
                    btnCriarPeriodoInscricao.Disabled = true;
                    btnExcluirPeriodoInscricao.Disabled = true;

                    if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 10))
                    {
                        if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 10).FirstOrDefault().escrita != true)
                        {
                            btnCriarPeriodoInscricao.Visible = false;
                            btnSalvarPeriodoInscricao.Visible = false;
                        }
                    }
                }
            }

        }

        protected void btnCriarPeriodoInscricao_Click(object sender, EventArgs e)
        {
            Session["sNewPeriodoInscricao"] = true;
            Session["periodo_inscricao"] = null;
            Session["AdiciondoSucesso"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("proPeriodoInscricao.aspx", true);
        }

        protected void btnSalvarPeriodoInscricao_ServerClick(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";
                if (txtPeriodoInscricao.Value.Trim() == "")
                {
                    sAux = sAux + "Digite uma Descrição do Período de INscrição. <br/><br/>";
                }

                if (txtDataInicioPeriodoInscricaoGestao.Value == "")
                {
                    sAux = sAux + "Digite uma Data Início. <br/><br/>";
                }

                if (txtDataFimPeriodoInscricaoGestao.Value == "")
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

                if (Session["sNewPeriodoInscricao"] != null && (Boolean)Session["sNewPeriodoInscricao"] != true)
                {

                    InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
                    periodo_inscricao item = new periodo_inscricao();

                    item = (periodo_inscricao)Session["periodo_inscricao"];
                    item.quadrimestre  = txtPeriodoInscricao.Value.Trim();
                    item.data_inicio = Convert.ToDateTime(txtDataInicioPeriodoInscricaoGestao.Value);
                    item.data_fim = Convert.ToDateTime(txtDataFimPeriodoInscricaoGestao.Value);
                    if (txtDataLimitePagtoPeriodoInscricaoGestao.Value != "")
                    {
                        item.data_limite_pagamento = Convert.ToDateTime(txtDataLimitePagtoPeriodoInscricaoGestao.Value);
                    }

                    if (txtDataProvaPeriodoInscricaoGestao.Value != "")
                    {
                        item.data_prova = Convert.ToDateTime(txtDataProvaPeriodoInscricaoGestao.Value);
                    }

                    aplicacaoInscricao.AlterarPeriodoInscricao(item);

                    item = aplicacaoInscricao.BuscaItem_periodo_inscricao(item);

                    lblMensagem.Text = "Período de Inscrição alterado com sucesso.";
                    lblTituloMensagem.Text = "Alteração de Período de Inscrição";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

                    Session["periodo_inscricao"] = item;

                }
                else
                {
                    InscricaoAplicacao aplicacaoPeriodo = new InscricaoAplicacao();
                    periodo_inscricao item = new periodo_inscricao();

                    item.quadrimestre = txtPeriodoInscricao.Value.Trim();
                    item.data_inicio = Convert.ToDateTime(txtDataInicioPeriodoInscricaoGestao.Value);
                    item.data_fim = Convert.ToDateTime(txtDataFimPeriodoInscricaoGestao.Value);
                    if (txtDataLimitePagtoPeriodoInscricaoGestao.Value.Trim() != "")
                    {
                        item.data_limite_pagamento = Convert.ToDateTime(txtDataLimitePagtoPeriodoInscricaoGestao.Value);
                    }
                    if (txtDataProvaPeriodoInscricaoGestao.Value.Trim() != "")
                    {
                        item.data_prova = Convert.ToDateTime(txtDataProvaPeriodoInscricaoGestao.Value);
                    }

                    item = aplicacaoPeriodo.CriarPeriodoInscricao(item);

                    if (item != null)
                    {
                        Session["periodo_inscricao"] = item;
                        Session.Add("sNewPeriodoInscricao", false);
                        Session["AdiciondoSucesso"] = true;
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na manutenção do Período de Inscrição. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Período de Inscrição";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }

        protected void btnExcluirPeriodoInscricao_ServerClick(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                InscricaoAplicacao aplicacaoPeriodo = new InscricaoAplicacao();
                periodo_inscricao item = new periodo_inscricao();

                item = (periodo_inscricao)Session["periodo_inscricao"];

                aplicacaoPeriodo.ExcluirPeriodoInscricao(item);

                lblMensagem.Text = "Período de Inscrição Excluído com sucesso.";
                lblTituloMensagem.Text = "Exclusão de Período de Inscrição";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

                txtIdPeriodoInscricao.Value = "";

                txtPeriodoInscricao.Value = "";
                txtDataInicioPeriodoInscricaoGestao.Value = "";
                txtDataFimPeriodoInscricaoGestao.Value = "";

                divCursos.Visible = false;
                btnCriarPeriodoInscricao.Disabled = true;
                btnExcluirPeriodoInscricao.Disabled = true;

                lblTituloPagina.Text = "(novo)";
                Session["sNewPeriodoInscricao"] = true;
                Session["periodo_inscricao"] = null;
                Session["AdiciondoSucesso"] = null;

                if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 10))
                {
                    if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 10).FirstOrDefault().escrita != true)
                    {
                        btnCriarPeriodoInscricao.Visible = false;
                        btnSalvarPeriodoInscricao.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na exclusão do Período de Inscrição. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Período de Inscrição";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }
    }
}