using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aplicacao_C;
using SERPI.Dominio_C;

namespace SERPI.UI.WebForms_C
{
    public partial class outDownloadCertificado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    CertificadoAplicacao aplicacaoCertificado = new CertificadoAplicacao();
                    certificados_participantes item = new certificados_participantes();
                    string sChave = HttpContext.Current.Request["chave"];
                    if (sChave != null)
                    {
                        item.chave_participante = sChave;
                        item = aplicacaoCertificado.Busca_Certificado_Participante_byChave(item);
                        if (item == null)
                        {
                            lblMensagemEmail.Text = "Caro usuário, infelizmente a chave informada não foi encontrada.<br><br>Por favor, entre em contato com a secretaria do mestrado IPT e informe o ocorrido.";
                            divRowSenha.Visible = false;
                        }
                        else if (item.data_envio_email.Value.AddMonths(2) < DateTime.Today)
                        {
                            lblMensagemEmail.Text = "Caro usuário, infelizmente a data limite para o <em>download</em>do Certificado expirou.<br><br>Por favor, entre em contato com a secretaria do mestrado IPT e informe o ocorrido.";
                            divRowSenha.Visible = false;
                        }

                        else
                        {
                            Session["chave_certificados_participantes"] = item;
                            lblMensagemEmail.Text = "Bem-vindo <b>" + item.nome + "</b>!<br>Digite abaixo a senha enviada por e-mail para fazer o <em>download</em>do seu Certificado.";
                            divRowSenha.Visible = true;
                        }
                    }
                    else
                    {
                        lblMensagemEmail.Text = "Caro usuário, infelizmente a chave informada não foi encontrada.<br><br>Por favor, entre em contato com a secretaria do mestrado IPT e informe o ocorrido.";
                        divRowSenha.Visible = false;
                    }
                    
                }
                catch (Exception ex)
                {
                    lblMensagemEmail.Text = "Caro usuário, infelizmente houve um erro interno.<br><br>Por favor, entre em contato com a secretaria do mestrado IPT e informe o ocorrido.<br><br>Erro:" + ex.Message;
                    divRowSenha.Visible = false; 
                }
                

            }
        }

        protected void btnBaixarCertificado_Click(object sender, EventArgs e)
        {
            certificados_participantes item;
            item = (certificados_participantes)Session["chave_certificados_participantes"];

            if (item.senha == txtSenhaEmail.Value.Trim())
            {
                if (File.Exists(Server.MapPath("~/Certificados/" + item.id_certificado + "/PDFs/" + item.arquivo_pdf)))
                {
                    CertificadoAplicacao aplicacaoCertificado = new CertificadoAplicacao();
                    item.data_download = DateTime.Now;
                    aplicacaoCertificado.AlterarItem_participante(item);

                    BaixaCertificado(item);

                    lblMensagemEmail.Text = "<b>Parabéns</>!!!<br><br>Seu Certificado foi baixado com sucesso.";
                    divRowSenha.Visible = false;
                }
                else
                {
                    lblMensagem.Text = "Caro usuário, infelizmente seu Certificado não foi encontrado.<br><br> Por favor, entre em contato com a secretaria do mestrado IPT e informe o ocorrido!";
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                }
            }
            else
            {
                lblMensagem.Text = "Caro usuário, a senha informada é inválida.<br><br> Por favor, tente novamente!";
                lblTituloMensagem.Text = "Atenção";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
            }
            
        }

        protected void BaixaCertificado(certificados_participantes item)
        {
            Response.Clear();
            Response.BufferOutput = true;
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/Certificados/" + item.id_certificado + "/PDFs/" + item.arquivo_pdf)));
            Response.WriteFile(Server.MapPath("~/Certificados/" + item.id_certificado + "/PDFs/" + item.arquivo_pdf));
            Response.Flush();
            Response.End();
        }
    }
}