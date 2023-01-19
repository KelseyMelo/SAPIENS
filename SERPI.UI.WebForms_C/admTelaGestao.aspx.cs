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
    public partial class admTelaGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 3)) // 3. Cadastro Tela - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                
                if (Session["sNewtelas_sistema"] != null && (Boolean)Session["sNewtelas_sistema"] != true)
                {
                    telas_sistema item;
                    item = (telas_sistema)Session["telas_sistema"];
                    lblTituloPagina.Text = "(Editar) - N.º " + item.id_tela;

                    txtTelaSerpi.Value = item.tela;
                    txtDescricaoSerpi.Value = item.descricao;
                    ddlModuloSerpi.SelectedValue = item.modulo;
                    ddlModuloSapiens.SelectedValue = item.modulo_sapiens;
                    txtDescricaoSapiens.Value = item.descricao_sapiens;
                    if (item.status == 1)
                    {
                        optSituacaoSim.Checked = true;
                    }
                    else
                    {
                        optSituacaoNao.Checked = true;
                    }

                    divGrupos.Visible = true;

                    if (Session["AdiciondoSucesso"] != null)
                    {
                        if ((Boolean)Session["AdiciondoSucesso"])
                        {
                            Session["AdiciondoSucesso"] = null;
                            lblMensagem.Text = "Novo Tela do Sistema adicionado com sucesso";
                            lblTituloMensagem.Text = "Novo Tela do Sistema";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
                        }
                    }
                }
                else
                {
                    lblTituloPagina.Text = "(novo)";
                    txtTelaSerpi.Value = "";
                    txtDescricaoSerpi.Value = "";
                    ddlModuloSerpi.SelectedValue = "Selecione um módulo";
                    ddlModuloSapiens.SelectedValue = "Selecione um módulo";
                    txtDescricaoSapiens.Value = "";
                    optSituacaoSim.Checked = true;
                    optSituacaoNao.Checked = false;

                    divGrupos.Visible = false;
                }
            }

        }

        
        protected void btnCriarTela_Click(object sender, EventArgs e)
        {
            Session["sNewtelas_sistema"] = true;
            Session["telas_sistema"] = null;
            Session["AdiciondoSucesso"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("admTela.aspx", true);
        }

        protected void btnSalvarTela_Click(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";

                if (txtTelaSerpi.Value.Trim() == "")
                {
                    sAux = sAux + "Digite uma Tela (SERPI). <br/><br/>";
                }

                if (txtDescricaoSerpi.Value.Trim () == "")
                {
                    sAux = sAux + "Digite uma Descrição (SERPI). <br/><br/>";
                }

                if (ddlModuloSerpi.SelectedValue == "Selecione um módulo")
                {
                    sAux = "Selecionar um módulo (SERPI). <br/><br/>";
                }

                if (ddlModuloSapiens.SelectedValue == "Selecione um módulo")
                {
                    sAux = "Selecionar um módulo (SAPIENS). <br/><br/>";
                }

                if (txtDescricaoSapiens.Value.Trim() == "")
                {
                    sAux = sAux + "Digite uma Descrição (SAPIENS). <br/><br/>";
                }

                if (sAux != "")
                {
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                    return;
                }

                if (Session["sNewtelas_sistema"] != null && (Boolean)Session["sNewtelas_sistema"] != true)
                {

                    TelaAplicacao aplicacaoTela = new TelaAplicacao();
                    telas_sistema item = new telas_sistema();

                    item = (telas_sistema)Session["telas_sistema"];
                    //item.quadrimestre  = ddlPeriodoPeriodoMatriculaGestao.SelectedValue;
                    item.tela = txtTelaSerpi.Value.Trim();
                    item.descricao = txtDescricaoSerpi.Value.Trim();
                    item.modulo = ddlModuloSerpi.SelectedValue;
                    item.modulo_sapiens = ddlModuloSapiens.SelectedValue;
                    item.descricao_sapiens = txtDescricaoSapiens.Value.Trim();
                    if (optSituacaoSim.Checked)
                    {
                        item.status = 1;
                    }
                    else
                    {
                        item.status = 0;
                    }
                    
                    
                    aplicacaoTela.AlterarItem(item);

                    lblMensagem.Text = "Tela do Sistema alterada com sucesso.";
                    lblTituloMensagem.Text = "Alteração de Tela do Sistema";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');fAtiva_grdDisciplina();", true);

                    Session["telas_sistema"] = item;

                }
                else
                {
                    TelaAplicacao aplicacaoTela = new TelaAplicacao();
                    telas_sistema item = new telas_sistema();

                    item.tela = txtTelaSerpi.Value.Trim();
                    item.descricao = txtDescricaoSerpi.Value.Trim();
                    item.modulo = ddlModuloSerpi.SelectedValue;
                    item.modulo_sapiens = ddlModuloSapiens.SelectedValue;
                    item.descricao_sapiens = txtDescricaoSapiens.Value.Trim();
                    if (optSituacaoSim.Checked)
                    {
                        item.status = 1;
                    }
                    else
                    {
                        item.status = 0;
                    }

                    item = aplicacaoTela.CriarItem(item);

                    if (item != null)
                    {
                        Session["telas_sistema"] = item;
                        Session.Add("sNewtelas_sistema", false);
                        Session["AdiciondoSucesso"] = true;
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na manutenção da Tela do Sistema. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Tela do Sistema";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }

        protected void btnExcluirPeriodoMatricula_ServerClick(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                TelaAplicacao aplicacaoTela = new TelaAplicacao();
                telas_sistema item = new telas_sistema();

                item = (telas_sistema)Session["telas_sistema"];

                //aplicacaoTela.ExcluiPeriodoMatricula(item);

                lblMensagem.Text = "Tela do Sistema Excluído com sucesso.";
                lblTituloMensagem.Text = "Exclusão de Tela do Sistema";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');fAtiva_grdDisciplina();", true);

                lblTituloPagina.Text = "(novo)";

                if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 32))
                {
                    if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 32).FirstOrDefault().escrita != true)
                    {
                        

                    }
                }

            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na exclusão do Tela do Sistema. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Tela do Sistema";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }
    }
}