using Aplicacao_C;
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
    public partial class admArquivoGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 73 || x.id_tela == 12)) // Tipo Curso ou Curso - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {


                if (Session["sNewArquivos"] != null && (Boolean)Session["sNewArquivos"] != true)
                {
                    arquivos item;
                    item = (arquivos)Session["arquivos"];
                    lblTituloPagina.Text = "(Editar) - N.º " + item.id_arquivo;

                    txtDataCadastro.Value = String.Format("{0:dd/MM/yyyy HH:mm:ss}", item.data_cadastro);
                    txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy HH:mm:ss}", item.data_alteracao);
                    txtStatus.Value = item.status;
                    txtResponsavel.Value = item.usuario;

                    txtDescArquivo.Value = item.descricao;
                    txtNomeArquivo.Value = item.nome_arquivo;
                    //idVideo.Src = "./videos/" + item.nome_arquivo;


                    btnCriarVideo.Disabled = false;
                    divEdicao.Visible = true;

                    if (Session["AdiciondoSucesso"] != null)
                    {
                        if ((Boolean)Session["AdiciondoSucesso"])
                        {
                            Session["AdiciondoSucesso"] = null;
                            lblMensagem.Text = "Novo Arquivo cadastrado com sucesso";
                            lblTituloMensagem.Text = "Novo Arquivo";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
                        }

                    }

                }
                else
                {
                    lblTituloPagina.Text = "(novo)";
                    txtDataCadastro.Value = "";
                    txtDataAlteracao.Value = "";
                    txtStatus.Value = "";
                    txtResponsavel.Value = "";

                    txtDescArquivo.Value = "";
                    txtNomeArquivo.Value = "";
                    //idVideo.Src = "&nbsp;";

                    btnCriarVideo.Disabled = true;
                    divEdicao.Visible = false;
                }
            }

        }

        protected void btnCriarVideo_Click(object sender, EventArgs e)
        {
            Session["sNewArquivos"] = true;
            Session["arquivos"] = null;
            Session["AdiciondoSucesso"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("admArquivo.aspx", true);
        }

        protected void btnSalvar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";

                if (txtDescArquivo.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a Descrição do Arquivo. <br/><br/>";
                }

                if (sAux != "")
                {
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                    return;
                }

                if (Session["sNewArquivos"] != null && (Boolean)Session["sNewArquivos"] != true)
                {
                    //Alteração'
                    ArquivoAplicacao aplicacaoArquivo = new ArquivoAplicacao();
                    arquivos item = new arquivos();

                    item = (arquivos)Session["arquivos"];

                    item.status = "alterado";
                    //item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    item.descricao = txtDescArquivo.Value.Trim();
                    //item.nome_arquivo = txtNomeArquivoArquivo.Value;

                    if (fileArquivoParaGravar.HasFile)
                    {
                        if (item.nome_arquivo != fileArquivoParaGravar.FileName)
                        {
                            lblMensagem.Text = "Para proceder a alteração desse arquivo o nome do 'Novo Arquivo' precisa ser igual ao que está cadastrado.";
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                            return;
                        }

                        fileArquivoParaGravar.SaveAs(Server.MapPath("") + "\\Anexos\\" + fileArquivoParaGravar.FileName);
                        idVideo.Src = "\\Anexos\\" + item.nome_arquivo;
                    }

                    aplicacaoArquivo.AlterarItem(item);

                    item = aplicacaoArquivo.BuscaItem(item);

                    txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy HH:mm:ss}", item.data_alteracao);
                    txtStatus.Value = item.status;
                    txtResponsavel.Value = item.usuario;

                    lblMensagem.Text = "Dados do Arquivo alterado com sucesso.";
                    lblTituloMensagem.Text = "Alteração de Arquivo";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

                    Session["arquivos"] = item;

                }
                else
                {
                    //Inclusão

                    if (!fileArquivoParaGravar.HasFile)
                    {
                        lblMensagem.Text = "Deve-se selecionar um Arquivo para o cadastramento. <br/> ";
                        lblTituloMensagem.Text = "Atenção";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                        return;
                    }

                    fileArquivoParaGravar.SaveAs(Server.MapPath("") + "\\Anexos\\" + fileArquivoParaGravar.FileName);

                    ArquivoAplicacao aplicacaoArquivo = new ArquivoAplicacao();

                    arquivos item = new arquivos();
                    item.ativo = 1;
                    item.status = "cadastrado";
                    item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    item.descricao = txtDescArquivo.Value.Trim();
                    item.nome_arquivo = fileArquivoParaGravar.FileName;
                    item.tipo_arquivo =  ((Path.GetExtension(fileArquivoParaGravar.FileName)).ToLower()).Replace(".","");

                    item = aplicacaoArquivo.CriarItem(item);

                    if (item != null)
                    {
                        Session["arquivos"] = item;
                        Session.Add("sNewArquivos", false);
                        Session["AdiciondoSucesso"] = true;
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na manutenção do Arquivo. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Arquivo";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }

    }
}