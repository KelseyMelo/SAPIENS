
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
    public partial class telaCadastroGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 70)) // Monitor Cadastro - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {


                if (Session["sNewMonitor"] != null && (Boolean)Session["sNewMonitor"] != true)
                {
                    monitor item;
                    item = (monitor)Session["monitor"];
                    lblTituloPagina.Text = "(Editar) - N.º " + item.id_monitor;

                    if (item.ativo == 0)
                    {
                        lblInativado.Visible = true;
                    }
                    else
                    {
                        lblInativado.Visible = false;
                    }
                    txtDataCadastro.Value = String.Format("{0:dd/MM/yyyy}", item.DataCadastro);
                    txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy}", item.DataAlteracao);
                    txtStatus.Value = item.status;
                    txtResponsavel.Value = item.usuario;

                    txtDescEventoMonitor.Value = item.DescEventoMonitor;
                    txtLocalEventoMonitor.Value = item.LocalEventoMonitor;
                    txtDataEventoMonitor.Value = item.DataEventoMonitor;
                    txtHoraEventoMonitor.Value = item.HorarioEventoMonitor;
                    txtResponsavelEvento.Value = item.ResponsavelEventoMonitor;

                    txtCoffee.Value = item.Coffee;

                    txtDataInicioMonitor.Value = String.Format("{0:yyyy-MM-dd}", item.DataInicio);
                    txtDataFimMonitor.Value = String.Format("{0:yyyy-MM-dd}", item.DataFim);
                    txtHoraInicioMonitor.Value = String.Format("{0:HH:mm}", item.DataInicio);
                    txtHoraFimMonitor.Value = String.Format("{0:HH:mm}", item.DataFim);

                    txtDataInicioEvento.Value = String.Format("{0:yyyy-MM-dd}", item.DataEventoInicio);
                    txtHoraInicioEvento.Value = String.Format("{0:HH:mm}", item.DataEventoInicio);
                    txtHoraFimEvento.Value = String.Format("{0:HH:mm}", item.DataEventoFim);

                    btnCriarEvento.Disabled = false;
                    divEdicao.Visible = true;

                    if (Session["AdiciondoSucesso"] != null)
                    {
                        if ((Boolean)Session["AdiciondoSucesso"])
                        {
                            Session["AdiciondoSucesso"] = null;
                            lblMensagem.Text = "Novo Evento adicionado com sucesso";
                            lblTituloMensagem.Text = "Novo Evento";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
                        }

                    }

                }
                else
                {
                    lblInativado.Visible = false;
                    lblTituloPagina.Text = "(novo)";
                    txtDataCadastro.Value = "";
                    txtDataAlteracao.Value = "";
                    txtStatus.Value = "";
                    txtResponsavel.Value = "";

                    txtDescEventoMonitor.Value = "";
                    txtLocalEventoMonitor.Value = "";
                    txtDataEventoMonitor.Value = "";
                    txtHoraEventoMonitor.Value = "";
                    txtResponsavelEvento.Value = "";

                    txtCoffee.Value = "";

                    txtDataInicioMonitor.Value = "";
                    txtDataFimMonitor.Value = "";
                    txtHoraInicioMonitor.Value = "";
                    txtHoraFimMonitor.Value = "";

                    txtDataInicioEvento.Value = "";
                    txtHoraInicioEvento.Value = "";
                    txtHoraFimEvento.Value = "";

                    btnCriarEvento.Disabled = true;
                    divEdicao.Visible = false;
                }
            }

        }

        protected void btnCriarEvento_Click(object sender, EventArgs e)
        {
            Session["sNewMonitor"] = true;
            Session["monitor"] = null;
            Session["AdiciondoSucesso"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("telaCadastro.aspx", true);
        }

        protected void btnSalvar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";

                if (txtDescEventoMonitor.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a Descrição do Evento. <br/><br/>";
                }

                if (txtLocalEventoMonitor.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Local do Evento. <br/><br/>";
                }

                if (txtResponsavelEvento.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Responsável do Evento. <br/><br/>";
                }

                DateTime temp;
                if (!DateTime.TryParse(txtDataInicioEvento.Value.Trim(), out temp))
                {
                    sAux = sAux + "Digite uma Data Início do Evento válida. <br/><br/>";
                }
                if (!DateTime.TryParse(txtDataInicioMonitor.Value.Trim(), out temp))
                {
                    sAux = sAux + "Digite uma Data Início de apresentação no Monitor válida. <br/><br/>";
                }
                if (!DateTime.TryParse(txtDataFimMonitor.Value.Trim(), out temp))
                {
                    sAux = sAux + "Digite uma Data Fim de apresentação no Monitor válida. <br/><br/>";
                }

                if (txtHoraInicioMonitor.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a Hora Início de apresentação no Monitor. <br/><br/>";
                }

                if (txtHoraFimMonitor.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a Hora Fim de apresentação no Monitor. <br/><br/>";
                }

                if (txtHoraInicioEvento.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a Hora Início do Evento. <br/><br/>";
                }

                if (txtHoraFimEvento.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a Hora Fim do Evento. <br/><br/>";
                }

                if (sAux != "")
                {
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                    return;
                }

                if (Session["sNewMonitor"] != null && (Boolean)Session["sNewMonitor"] != true)
                {

                    MonitorAplicacao aplicacaoEvento = new MonitorAplicacao();
                    monitor item = new monitor();

                    item = (monitor)Session["monitor"];

                    item.status = "alterado";
                    //item.data_cadastro = DateTime.Now;
                    item.DataAlteracao = DateTime.Today;
                    item.usuario = usuario.usuario;

                    item.DescEventoMonitor = txtDescEventoMonitor.Value;
                    item.LocalEventoMonitor = txtLocalEventoMonitor.Value;
                    item.DataEventoMonitor = txtDataEventoMonitor.Value;
                    item.HorarioEventoMonitor = txtHoraEventoMonitor.Value;
                    item.ResponsavelEventoMonitor = txtResponsavelEvento.Value;

                    item.Coffee = txtCoffee.Value;

                    item.DataInicio = Convert.ToDateTime(txtDataInicioMonitor.Value + " " + txtHoraInicioMonitor.Value);
                    item.DataFim = Convert.ToDateTime(txtDataFimMonitor.Value + " " + txtHoraFimMonitor.Value);

                    item.DataEventoInicio = Convert.ToDateTime(txtDataInicioEvento.Value + " " + txtHoraInicioEvento.Value);
                    item.DataEventoFim = Convert.ToDateTime(txtDataInicioEvento.Value + " " + txtHoraFimEvento.Value);

                    aplicacaoEvento.AlterarItem(item);

                    item = aplicacaoEvento.BuscaItem(item);

                    txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy}", item.DataAlteracao);
                    txtStatus.Value = item.status;
                    txtResponsavel.Value = item.usuario;

                    lblMensagem.Text = "Evento alterado com sucesso.";
                    lblTituloMensagem.Text = "Alteração de Evento";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

                    Session["monitor"] = item;

                }
                else
                {
                    MonitorAplicacao aplicacaoEvento = new MonitorAplicacao();

                    monitor item = new monitor();
                    item.ativo = 1;
                    item.status = "cadastrado";
                    item.DataCadastro = DateTime.Today;
                    item.DataAlteracao = DateTime.Today;
                    item.usuario = usuario.usuario;

                    item.DescEventoMonitor = txtDescEventoMonitor.Value;
                    item.LocalEventoMonitor = txtLocalEventoMonitor.Value;
                    item.DataEventoMonitor = txtDataEventoMonitor.Value;
                    item.HorarioEventoMonitor = txtHoraEventoMonitor.Value;
                    item.ResponsavelEventoMonitor = txtResponsavelEvento.Value;

                    item.Coffee = txtCoffee.Value;

                    item.DataInicio = Convert.ToDateTime(txtDataInicioMonitor.Value + " " + txtHoraInicioMonitor.Value);
                    item.DataFim = Convert.ToDateTime(txtDataFimMonitor.Value + " " + txtHoraFimMonitor.Value);

                    item.DataEventoInicio = Convert.ToDateTime(txtDataInicioEvento.Value + " " + txtHoraInicioEvento.Value);
                    item.DataEventoFim = Convert.ToDateTime(txtDataInicioEvento.Value + " " + txtHoraFimEvento.Value);

                    item = aplicacaoEvento.CriarItem(item);

                    if (item != null)
                    {
                        Session["monitor"] = item;
                        Session.Add("sNewMonitor", false);
                        Session["AdiciondoSucesso"] = true;
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na manutenção do Evento. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Evento";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }

    }
}