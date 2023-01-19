
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
    public partial class telaVideoGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 71)) // Video_video Video - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {


                if (Session["sNewMonitorVideo"] != null && (Boolean)Session["sNewMonitorVideo"] != true)
                {
                    monitor_video item;
                    item = (monitor_video)Session["monitor_video"];
                    lblTituloPagina.Text = "(Editar) - N.º " + item.id_monitor_video;

                    if (item.ativo == 0)
                    {
                        lblInativado.Visible = true;
                    }
                    else
                    {
                        lblInativado.Visible = false;
                    }
                    txtDataCadastro.Value = String.Format("{0:dd/MM/yyyy}", item.data_cadastro);
                    txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy}", item.data_alteracao);
                    txtStatus.Value = item.status;
                    txtResponsavel.Value = item.usuario;

                    txtDescVideoMonitor.Value = item.descricao;
                    txtNomeArquivoVideoMonitor.Value = item.nome_arquivo;
                    txtOrdemVideoMonitor.Value = item.ordem.ToString();
                    txtDataInicioVideo.Value = String.Format("{0:yyyy-MM-dd}", item.data_inicio);
                    txtHoraInicioVideo.Value = String.Format("{0:HH:mm}", item.data_inicio);
                    txtDataFimVideo.Value = String.Format("{0:yyyy-MM-dd}", item.data_fim);
                    txtHoraFimVideo.Value = String.Format("{0:HH:mm}", item.data_fim);

                    idVideo.Src = "./videos/" + item.nome_arquivo;


                    btnCriarVideo.Disabled = false;
                    divEdicao.Visible = true;

                    if (Session["AdiciondoSucesso"] != null)
                    {
                        if ((Boolean)Session["AdiciondoSucesso"])
                        {
                            Session["AdiciondoSucesso"] = null;
                            lblMensagem.Text = "Novo Vídeo cadastrado com sucesso";
                            lblTituloMensagem.Text = "Novo Vídeo";
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

                    txtDescVideoMonitor.Value = "";
                    txtNomeArquivoVideoMonitor.Value = "";
                    txtOrdemVideoMonitor.Value = "";
                    idVideo.Src = "&nbsp;";

                    btnCriarVideo.Disabled = true;
                    divEdicao.Visible = false;
                }
            }

        }

        protected void btnCriarVideo_Click(object sender, EventArgs e)
        {
            Session["sNewMonitorVideo"] = true;
            Session["monitor_video"] = null;
            Session["AdiciondoSucesso"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("telaVideo.aspx", true);
        }

        protected void btnSalvar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";

                if (txtDescVideoMonitor.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a Descrição do Vídeo. <br/><br/>";
                }

                if (txtNomeArquivoVideoMonitor.Value.Trim() == "")
                {
                    sAux = sAux + "O Nome do Arquivo do Vídeo deve estar preenchido. <br/><br/>";
                }

                if (txtOrdemVideoMonitor.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a Ordem em que o vídeo será apresentado (lembre que é em ordem decrescente). <br/><br/>";
                }

                DateTime temp;
                if (!DateTime.TryParse(txtDataInicioVideo.Value.Trim(), out temp))
                {
                    sAux = sAux + "Digite uma Data Início do Vídeo no Monitor válida. <br/><br/>";
                }
                
                if (!DateTime.TryParse(txtDataFimVideo.Value.Trim(), out temp))
                {
                    sAux = sAux + "Digite uma Data Fim de apresentação do Vídeo no Monitor válida. <br/><br/>";
                }

                if (txtHoraInicioVideo.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a Hora Início de apresentação do Vídeo no Monitor. <br/><br/>";
                }

                if (txtHoraFimVideo.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a Hora Fim de apresentação do Vídeo no Monitor. <br/><br/>";
                }

                if (sAux != "")
                {
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                    return;
                }

                if (Session["sNewMonitorVideo"] != null && (Boolean)Session["sNewMonitorVideo"] != true)
                {
                    //Alteração'
                    MonitorAplicacao aplicacaoMonitor = new MonitorAplicacao();
                    monitor_video item = new monitor_video();

                    item = (monitor_video)Session["monitor_video"];

                    item.status = "alterado";
                    //item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    item.descricao = txtDescVideoMonitor.Value.Trim();
                    item.nome_arquivo = txtNomeArquivoVideoMonitor.Value;
                    item.ordem = Convert.ToInt32(txtOrdemVideoMonitor.Value.Trim());

                    item.data_inicio = Convert.ToDateTime(txtDataInicioVideo.Value + " " + txtHoraInicioVideo.Value);
                    item.data_fim = Convert.ToDateTime(txtDataFimVideo.Value + " " + txtHoraFimVideo.Value);


                    aplicacaoMonitor.AlterarItem(item);

                    if (fileArquivoParaGravar.HasFile)
                    {
                        fileArquivoParaGravar.SaveAs(Server.MapPath("") + "\\videos\\" + fileArquivoParaGravar.FileName);
                        idVideo.Src = "\\videos\\" + item.nome_arquivo;
                    }

                    item = aplicacaoMonitor.BuscaItem(item);

                    txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy}", item.data_alteracao);
                    txtStatus.Value = item.status;
                    txtResponsavel.Value = item.usuario;

                    lblMensagem.Text = "Dados do Vídeo alterado com sucesso.";
                    lblTituloMensagem.Text = "Alteração de Vídeo";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

                    Session["monitor_video"] = item;

                }
                else
                {
                    //Inclusão

                    if (!fileArquivoParaGravar.HasFile)
                    {
                        lblMensagem.Text = "Deve-se selecionar um Vídeo para o cadastramento. <br/> ";
                        lblTituloMensagem.Text = "Atenção";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                        return;
                    }

                    fileArquivoParaGravar.SaveAs(Server.MapPath("") + "\\videos\\" + fileArquivoParaGravar.FileName);

                    MonitorAplicacao aplicacaoMonitor = new MonitorAplicacao();

                    monitor_video item = new monitor_video();
                    item.ativo = 1;
                    item.status = "cadastrado";
                    item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    item.descricao = txtDescVideoMonitor.Value.Trim();
                    item.nome_arquivo = txtNomeArquivoVideoMonitor.Value;
                    item.ordem = Convert.ToInt32(txtOrdemVideoMonitor.Value.Trim());

                    item.data_inicio = Convert.ToDateTime(txtDataInicioVideo.Value + " " + txtHoraInicioVideo.Value);
                    item.data_fim = Convert.ToDateTime(txtDataFimVideo.Value + " " + txtHoraFimVideo.Value);

                    item = aplicacaoMonitor.CriarItem(item);

                    if (item != null)
                    {
                        Session["monitor_video"] = item;
                        Session.Add("sNewMonitorVideo", false);
                        Session["AdiciondoSucesso"] = true;
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na manutenção do Vídeo. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Vídeo";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }

    }
}