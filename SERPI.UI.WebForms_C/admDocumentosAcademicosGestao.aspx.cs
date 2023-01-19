using Aplicacao_C;
using SERPI.Dominio_C;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class admDocumentosAcademicosGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 74)) // Documentos Acadêmicos - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {


                if (Session["sNewdocumentos_academicos"] != null && (Boolean)Session["sNewdocumentos_academicos"] != true)
                {
                    CarregarPagina();

                }
                else
                {
                    lblTextoHomeAlterado.Visible = false;
                    lblTituloPagina.Text = "(novo)";
                    txtDataCadastro.Value = "";
                    txtDataAlteracao.Value = "";
                    txtStatus.Value = "";
                    txtResponsavel.Value = "";

                    txtTituloDocumento.Value = "";
                    txtDescricaoDocumento.Value = "";
                    txtNomeArquivo.Value = "";
                    //idVideo.Src = "&nbsp;";

                    btnCriarDocumentoAcademico.Disabled = true;
                    divEdicao.Visible = false;
                    tabPublicado.Visible = false;
                    btnAprovarOffLine.Visible = false;
                    btnReprovarOffLine.Visible = false;
                    btnEnviarAprovacaoOffLine.Visible = false;
                    divObservacao.Visible = false;
                }
            }

        }

        protected void btnCriarDocumentoAcademico_Click(object sender, EventArgs e)
        {
            Session["sNewdocumentos_academicos"] = true;
            Session["documentos_academicos"] = null;
            Session["AdiciondoSucesso"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void CarregarPagina()
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            documentos_academicos item;
            item = (documentos_academicos)Session["documentos_academicos"];
            lblTituloPagina.Text = "(Editar) - N.º " + item.id_documentos_academicos;

            //Carrega Preview
            txtDataCadastro.Value = String.Format("{0:dd/MM/yyyy HH:mm:ss}", item.data_cadastro);
            txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy HH:mm:ss}", item.data_alteracao);
            txtStatus.Value = item.status;
            txtResponsavel.Value = item.usuario;
            divEdicao.Visible = true;

            txtTituloDocumentoPreview.Value = item.nomePreview;
            txtDescricaoDocumentoPreview.Value = item.descricaoPreview;
            txtNomeArquivoPreview.Value = item.nome_arquivoPreview;

            //Carrega Exemplo Preview
            string sAux = "";
            int i = 0;
            sAux = sAux + "<br><hr><br><div class=\"row\"><div class=\"col-md-6\">";
            sAux = sAux + "            <h5>Exemplo <em>Preview</em></h5>";
            sAux = sAux + "            <div class=\"panel panel-success\">";
            sAux = sAux + "                <div class=\"panel-heading\" role=\"tab\">";
            sAux = sAux + "                    <h5 class=\"panel-title\"><a class=\"collapsed a_faq\" id=\"cab_" + i + "\" data-toggle=\"collapse\" href=\"#res_" + i + "\" aria-expanded=\"false\">";
            if (item.tipo_arquivoPreview.ToUpper() != "PDF")
            {
                sAux = sAux + "                        <h6><i class=\"fa fa-file-word-o\" style=\"color: #3588CC\"></i> Documento Word</h6>";
            }
            else
            {
                sAux = sAux + "                        <h6><i class=\"fa fa-file-pdf-o\" style=\"color: #3588CC\"></i> Documento PDF</h6>";
            }
            sAux = sAux + "                        <h5 style = \"line -height: 1.5em\" ><strong>" + item.nomePreview + "</strong></h5>";
            sAux = sAux + "                        <i id = \"icab_" + i + "\" style = \"margin-top: -25px; color: #3588CC\" class=\"fa fa-chevron-left pull-right rotate\"></i>";
            sAux = sAux + "                    </a></h5>";
            sAux = sAux + "                </div>";
            sAux = sAux + "                <div id = \"res_" + i + "\" class=\"panel-collapse collapse\" role=\"tabpanel\" aria-labelledby=\"cab_" + i + "\" >";
            sAux = sAux + "                    <div class=\"panel-body\">";
            sAux = sAux + "                        " + item.descricaoPreview.Replace("\n", "<br>");
            sAux = sAux + "                        <br><br>";
            sAux = sAux + "                       <a class=\"btn btn-primary\" download target=\"_blank\" href=\"DocumentosAcademicos\\" + item.nome_arquivoPreview + "\" onclick=\"javascript:fAguarde()\"><b>Faça aqui o<em> download</em> do documento</b></a>";
            sAux = sAux + "                   </div>";
            sAux = sAux + "                </div>";
            sAux = sAux + "            </div>";
            sAux = sAux + "        </div></div>";

            litExemploPreview.Text = Server.HtmlDecode(sAux);

            lblTextoHomeAlterado.Visible = false;
            btnEnviarAprovacaoOffLine.Visible = false;
            btnSalvar.Visible = false;
            btnLocalizarArquivo.Visible = false;
            btnAprovarOffLine.Visible = false;
            btnReprovarOffLine.Visible = false;
            divEdicaoAprovado.Visible = false;

            if (item.statusAprovacao==0)
            {
                //statusAprovacao = 0 (Aguardando Aprovação)
                lblTextoHomeAlterado.Visible = true;
                lblTextoHomeAlterado.InnerText = "Aguardando Aprovação";
                if (usuario.grupos_acesso.id_grupo == 8 || usuario.grupos_acesso.id_grupo == 1) //1= TI e 8=Gerencia
                {
                    btnAprovarOffLine.Visible = true;
                    btnReprovarOffLine.Visible = true;
                }
                
            }
            else if (item.statusAprovacao == 1)
            {
                //statusAprovacao = 1 (Aprovado)
                if (usuario.grupos_acesso.id_grupo != 8 && usuario.grupos_acesso.id_grupo != 1) //1= TI e 8=Gerencia
                {
                    btnSalvar.Visible = true;
                    btnEnviarAprovacaoOffLine.Visible = false;
                    btnLocalizarArquivo.Visible = true;
                }
            }
            else if (item.statusAprovacao == 2)
            {
                //statusAprovacao = 2 (Reprovado)
                lblTextoHomeAlterado.Visible = true;
                lblTextoHomeAlterado.InnerText = "Reprovado";
                if (usuario.grupos_acesso.id_grupo != 8 && usuario.grupos_acesso.id_grupo != 1) //1= TI e 8=Gerencia
                {
                    btnSalvar.Visible = true;
                    btnLocalizarArquivo.Visible = true;
                }
            }
            else if (item.statusAprovacao == 3)
            {
                //statusAprovacao = 3 (Alterado)
                lblTextoHomeAlterado.Visible = true;
                lblTextoHomeAlterado.InnerText = "Alterado (enviar para aprovação)";
                if (usuario.grupos_acesso.id_grupo != 8 && usuario.grupos_acesso.id_grupo != 1) //1= TI e 8=Gerencia
                {
                    btnEnviarAprovacaoOffLine.Visible = true;
                    btnSalvar.Visible = true;
                    btnLocalizarArquivo.Visible = true;
                }
            }

            grdResultado.DataSource = item.documentos_academicos_obs.ToList();
            grdResultado.DataBind();

            if (item.documentos_academicos_obs.Count > 0)
            {
                grdResultado.UseAccessibleHeader = true;
                grdResultado.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultados.Visible = false;
                grdResultado.Visible = true;
            }
            else
            {
                msgSemResultados.Visible = true;
            }
            divObservacao.Visible = true;

            if (item.data_aprovacao != null)
            {
                //Publicado
                divEdicaoAprovado.Visible = true;
                txtDataAprovacao.Value = item.data_aprovacao.Value.ToString("dd/MM/yyyy");
                txtusuarioAprovacao.Value = item.usuarioAprovacao;
                txtTituloDocumento.Value = item.nome;
                txtDescricaoDocumento.Value = item.descricao;
                txtNomeArquivo.Value = item.nome_arquivo;

                //Carrega Exemplo Publicado
                sAux = "";
                i = 1;
                sAux = sAux + "<br><hr><br><div class=\"row\"><div class=\"col-md-6\">";
                sAux = sAux + "            <h4>Exemplo Publicado</h4>";
                sAux = sAux + "            <div class=\"panel panel-success\">";
                sAux = sAux + "                <div class=\"panel-heading\" role=\"tab\">";
                sAux = sAux + "                    <h5 class=\"panel-title\"><a class=\"collapsed a_faq\" id=\"cab_" + i + "\" data-toggle=\"collapse\" href=\"#res_" + i + "\" aria-expanded=\"false\">";
                if (item.tipo_arquivo.ToUpper() != "PDF")
                {
                    sAux = sAux + "                        <h6><i class=\"fa fa-file-word-o\" style=\"color: #3588CC\"></i> Documento Word</h6>";
                }
                else
                {
                    sAux = sAux + "                        <h6><i class=\"fa fa-file-pdf-o\" style=\"color: #3588CC\"></i> Documento PDF</h6>";
                }
                sAux = sAux + "                        <h5 style = \"line -height: 1.5em\" ><strong>" + item.nome + "</strong></h5>";
                sAux = sAux + "                        <i id = \"icab_" + i + "\" style = \"margin-top: -25px; color: #3588CC\" class=\"fa fa-chevron-left pull-right rotate\"></i>";
                sAux = sAux + "                    </a></h5>";
                sAux = sAux + "                </div>";
                sAux = sAux + "                <div id = \"res_" + i + "\" class=\"panel-collapse collapse\" role=\"tabpanel\" aria-labelledby=\"cab_" + i + "\" >";
                sAux = sAux + "                    <div class=\"panel-body\">";
                sAux = sAux + "                        " + item.descricao.Replace("\n", "<br>");
                sAux = sAux + "                        <br><br>";
                sAux = sAux + "                       <a class=\"btn btn-primary\" download target=\"_blank\" href=\"DocumentosAcademicos\\" + item.nome_arquivo + "\" onclick=\"javascript:fAguarde()\"><b>Faça aqui o<em> download</em> do documento</b></a>";
                sAux = sAux + "                   </div>";
                sAux = sAux + "                </div>";
                sAux = sAux + "            </div>";
                sAux = sAux + "        </div></div>";

                litExemploPublicado.Text = Server.HtmlDecode(sAux);

                tabPublicado.Visible = true;
            }
            else
            {
                tabPublicado.Visible = false;
            }

            btnCriarDocumentoAcademico.Disabled = false;

            if (Session["AdiciondoSucesso"] != null)
            {
                if ((Boolean)Session["AdiciondoSucesso"])
                {
                    Session["AdiciondoSucesso"] = null;
                    lblMensagem.Text = "Novo Documento Acadêmico cadastrado com sucesso";
                    lblTituloMensagem.Text = "Novo Documento Acadêmico";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
                }

            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("admDocumentosAcademicos.aspx", true);
        }

        protected void btnSalvar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";

               if (txtTituloDocumentoPreview.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Título do Documento Acadêmico. <br/><br/>";
                } 

                if (txtDescricaoDocumentoPreview.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a Descrição do Documento Acadêmico. <br/><br/>";
                }

                if (sAux != "")
                {
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                    return;
                }

                if (Session["sNewdocumentos_academicos"] != null && (Boolean)Session["sNewdocumentos_academicos"] != true)
                {
                    //Alteração'
                    DocumentosAcademicosAplicacao aplicacaoDocumentosAcademicos = new DocumentosAcademicosAplicacao();
                    documentos_academicos item = new documentos_academicos();

                    item = (documentos_academicos)Session["documentos_academicos"];

                    item.status = "alterado";
                    //item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;
                    item.statusAprovacao = 3; //3 = Alterado

                    item.nomePreview = txtTituloDocumentoPreview.Value.Trim();
                    item.descricaoPreview = txtDescricaoDocumentoPreview.Value.Trim();
                    item.id_tipo_cursoPreview = 1; //Fixado mestrado

                    if (fileArquivoParaGravar.HasFile)
                    {
                        if (fileArquivoParaGravar.FileName.IndexOf("+") != -1)
                        {
                            lblMensagem.Text = "O nome do arquivo <strong>NÃO</strong> pode conter o caracter \"+\". <br/> ";
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                            return;

                        }
                        fileArquivoParaGravar.SaveAs(Server.MapPath("") + "\\DocumentosAcademicos\\" + fileArquivoParaGravar.FileName);
                        item.nome_arquivoPreview = fileArquivoParaGravar.FileName;
                        item.tipo_arquivoPreview = ((Path.GetExtension(fileArquivoParaGravar.FileName)).ToLower()).Replace(".", "");
                    }

                    aplicacaoDocumentosAcademicos.AlterarItem(item);

                    item = aplicacaoDocumentosAcademicos.BuscaItem(item);

                    txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy HH:mm:ss}", item.data_alteracao);
                    txtStatus.Value = item.status;
                    txtResponsavel.Value = item.usuario;

                    lblMensagem.Text = "Dados do Documento Acadêmico alterado com sucesso.";
                    lblTituloMensagem.Text = "Alteração de Documento Acadêmico";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

                    Session["documentos_academicos"] = item;

                    CarregarPagina();

                }
                else
                {
                    //Inclusão

                    if (!fileArquivoParaGravar.HasFile)
                    {
                        lblMensagem.Text = "Deve-se localizar e selecionar um Documento Acadêmico para o cadastramento. <br/> ";
                        lblTituloMensagem.Text = "Atenção";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                        return;
                    }

                    if (fileArquivoParaGravar.FileName.IndexOf("+") != -1)
                    {
                        lblMensagem.Text = "O nome do arquivo <strong>NÃO</strong> pode conter o caracter \"+\". <br/> ";
                        lblTituloMensagem.Text = "Atenção";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                        return;

                    }

                    fileArquivoParaGravar.SaveAs(Server.MapPath("") + "\\DocumentosAcademicos\\" + fileArquivoParaGravar.FileName);

                    DocumentosAcademicosAplicacao aplicacaoDocumentosAcademicos = new DocumentosAcademicosAplicacao();

                    documentos_academicos item = new documentos_academicos();
                    item.ativo = 1;
                    item.status = "cadastrado";
                    item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    item.nomePreview = txtTituloDocumentoPreview.Value.Trim();
                    item.descricaoPreview = txtDescricaoDocumentoPreview.Value.Trim();
                    item.nome_arquivoPreview = fileArquivoParaGravar.FileName;
                    item.tipo_arquivoPreview = ((Path.GetExtension(fileArquivoParaGravar.FileName)).ToLower()).Replace(".", "");
                    item.id_tipo_cursoPreview = 1; //Fixado mestrado
                    item.statusAprovacao = 3; //3 = Alterado

                    item = aplicacaoDocumentosAcademicos.CriarItem(item);

                    if (item != null)
                    {
                        Session["documentos_academicos"] = item;
                        Session.Add("sNewdocumentos_academicos", false);
                        Session["AdiciondoSucesso"] = true;
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na manutenção do Documento Acadêmico. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Documento Acadêmico";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }

        protected void btnEnviarAprovacao_ServerClick(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";

                if (txtObsAprovacao.Value.Trim() == "")
                {
                    sAux = sAux + "Deve-se preencher campo 'Observação' para descrever a alteração realizada. <br/><br/>";
                }

                if (sAux != "")
                {
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                    return;
                }

                //Alteração'
                DocumentosAcademicosAplicacao aplicacaoDocumentosAcademicos = new DocumentosAcademicosAplicacao();
                documentos_academicos item = (documentos_academicos)Session["documentos_academicos"];

                item.status = "alterado";
                item.data_alteracao = DateTime.Now;
                item.usuario = usuario.usuario;

                item.statusAprovacao = 0; // Aguardando aprovação

                aplicacaoDocumentosAcademicos.AlterarItem(item);

                documentos_academicos_obs itemObs = new documentos_academicos_obs();
                itemObs.DataObs = DateTime.Now;
                itemObs.Observacao = txtObsAprovacao.Value.Trim();
                itemObs.id_documentos_academicos = item.id_documentos_academicos;
                itemObs.usuario = usuario.usuario;

                item.documentos_academicos_obs.Add(aplicacaoDocumentosAcademicos.CriarItem_Obs(itemObs));

                Session["documentos_academicos"] = item;

                //=======================
                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Configuracoes item_configuracoes;
                // 1 = email mestrado@ipt.br
                // 2 = email suporte@ipt.br
                item_configuracoes = aplicacaoGerais.BuscaConfiguracoes(1);

                string sFrom = item_configuracoes.remetente_email;
                string sFrom_Nome = item_configuracoes.nome_remetente_email;
                string sTo;
                string sAssunto = "Inclusão/Alteração de Documento Acadêmico: " + item.nomePreview;

                UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                usuarios usuario_remetente = new usuarios();
                usuario_remetente.usuario = item.usuario;
                usuario_remetente = aplicacaoUsuario.BuscaUsuario(usuario_remetente);

                sAux = "O Documento Acadêmico Incluído/Alterado<br><br>";
                sAux = sAux + "Título: <strong>" + item.nomePreview + "</strong><br>";
                sAux = sAux + "Descrição: <strong>" + item.descricaoPreview + "</strong><br>";
                sAux = sAux + "Nome do Documento: <strong>" + item.nome_arquivoPreview + "</strong><br>";
                sAux = sAux + "Responsável: <strong>" + usuario.nomeSocial.Trim() + "</strong><br>";
                sAux = sAux + "Obs: <strong>" + txtObsAprovacao.Value.Trim() + "</strong><br>";

                //É do grupo coordenador então pegar todos os cursos em que ele é coordenador
                List<usuarios> lista_usuario;
                usuarios item_usuario = new usuarios();
                item_usuario.grupos_acesso = new grupos_acesso();
                item_usuario.grupos_acesso.id_grupo = 8; //Gerência
                lista_usuario = aplicacaoUsuario.ListaUsuario_porGrupoAcesso(item_usuario);
                foreach (var elemento in lista_usuario)
                {

                    if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString == "Producao")
                    {
                        sTo = elemento.email;
                    }
                    else
                    {
                        sTo = usuario.email;
                        sAux = sAux + "<br><br> <strong>Esse email seria enviado para:</strong>" + elemento.email;
                    }

                    //sTo = "kelsey@ipt.br";

                    Utilizades.fEnviaEmail(sFrom, sFrom_Nome, sTo, "", "", sAssunto, sAux, item_configuracoes.servidor_email, item_configuracoes.conta_email, item_configuracoes.senha_email, item_configuracoes.porta_email.Value, 1, "");

                    //(this.Master as SERPI).PreencheSininho();

                }
                CarregarPagina();

                (this.Master as SERPI).PreencheSininho();

                //=================

                item = aplicacaoDocumentosAcademicos.BuscaItem(item);

                lblMensagem.Text = "Documento Acadêmico enviado para aprovação com sucesso.";
                lblTituloMensagem.Text = "Documento Acadêmico enviado para aprovação";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na manutenção do Documento Acadêmico. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Documento Acadêmico";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }

        protected void btnAprovar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";

                //Aprovar'
                DocumentosAcademicosAplicacao aplicacaoDocumentosAcademicos = new DocumentosAcademicosAplicacao();
                documentos_academicos item = (documentos_academicos)Session["documentos_academicos"];

                //item.status = "alterado";
                //item.data_alteracao = DateTime.Now;
                //item.usuario = usuario.usuario;

                item.statusAprovacao = 1; // Aguardando aprovação
                item.nome = item.nomePreview;
                item.nome_arquivo = item.nome_arquivoPreview;
                item.id_tipo_curso = item.id_tipo_cursoPreview;
                item.descricao = item.descricaoPreview;
                item.data_aprovacao = DateTime.Now;
                item.usuarioAprovacao = usuario.usuario;
                item.tipo_arquivo = item.tipo_arquivoPreview;

                aplicacaoDocumentosAcademicos.AlterarItem_Aprovacao(item);

                Session["documentos_academicos"] = item;

                //=======================
                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Configuracoes item_configuracoes;
                // 1 = email mestrado@ipt.br
                // 2 = email suporte@ipt.br
                item_configuracoes = aplicacaoGerais.BuscaConfiguracoes(1);

                string sFrom = item_configuracoes.remetente_email;
                string sFrom_Nome = item_configuracoes.nome_remetente_email;
                string sTo;
                string sAssunto = "Aprovação de Documento Acadêmico: " + item.nomePreview;

                UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                usuarios usuario_remetente = new usuarios();
                usuario_remetente.usuario = item.usuario;
                usuario_remetente = aplicacaoUsuario.BuscaUsuario(usuario_remetente);

                sAux = "O Documento Acadêmico foi Aprovado<br><br>";
                sAux = sAux + "Título: <strong>" + item.nomePreview + "</strong><br>";
                sAux = sAux + "Descrição: <strong>" + item.descricaoPreview + "</strong><br>";
                sAux = sAux + "Nome do Documento: <strong>" + item.nome_arquivoPreview + "</strong><br>";
                sAux = sAux + "Responsável: <strong>" + usuario.nomeSocial.Trim() + "</strong><br>";
                
                if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString == "Producao")
                {
                    sTo = usuario_remetente.email;
                }
                else
                {
                    sTo = usuario.email;
                    sAux = sAux + "<br><br> <strong>Esse email seria enviado para:</strong>" + usuario_remetente.email;
                }

                    //sTo = "kelsey@ipt.br";

                    Utilizades.fEnviaEmail(sFrom, sFrom_Nome, sTo, "", "", sAssunto, sAux, item_configuracoes.servidor_email, item_configuracoes.conta_email, item_configuracoes.senha_email, item_configuracoes.porta_email.Value, 1, "");

                    //(this.Master as SERPI).PreencheSininho();

                CarregarPagina();

                (this.Master as SERPI).PreencheSininho();

                //=================

                item = aplicacaoDocumentosAcademicos.BuscaItem(item);

                lblMensagem.Text = "Documento Acadêmico Aprovado com sucesso.";
                lblTituloMensagem.Text = "Documento Acadêmico Aprovado";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na manutenção do Documento Acadêmico. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Documento Acadêmico";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }

        protected void btnReprovar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";

                if (txtObsAprovacao.Value.Trim() == "")
                {
                    sAux = sAux + "Deve-se preencher campo 'Observação' para descrever o porquê da Reprovação. <br/><br/>";
                }

                if (sAux != "")
                {
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                    return;
                }

                //Reprovar'
                DocumentosAcademicosAplicacao aplicacaoDocumentosAcademicos = new DocumentosAcademicosAplicacao();
                documentos_academicos item = (documentos_academicos)Session["documentos_academicos"];

                //item.status = "alterado";
                //item.data_alteracao = DateTime.Now;
                //item.usuario = usuario.usuario;

                item.statusAprovacao = 2; // Reprovado
                item.data_reprovacao = DateTime.Now;
                item.usuarioAprovacao = usuario.usuario;

                aplicacaoDocumentosAcademicos.AlterarItem_Reprovacao(item);

                documentos_academicos_obs itemObs = new documentos_academicos_obs();
                itemObs.DataObs = DateTime.Now;
                itemObs.Observacao = txtObsAprovacao.Value.Trim();
                itemObs.id_documentos_academicos = item.id_documentos_academicos;
                itemObs.usuario = usuario.usuario;

                item.documentos_academicos_obs.Add(aplicacaoDocumentosAcademicos.CriarItem_Obs(itemObs));

                Session["documentos_academicos"] = item;

                //=======================
                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Configuracoes item_configuracoes;
                // 1 = email mestrado@ipt.br
                // 2 = email suporte@ipt.br
                item_configuracoes = aplicacaoGerais.BuscaConfiguracoes(1);

                string sFrom = item_configuracoes.remetente_email;
                string sFrom_Nome = item_configuracoes.nome_remetente_email;
                string sTo;
                string sAssunto = "REPROVAÇÃO de Documento Acadêmico: " + item.nomePreview;

                UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                usuarios usuario_remetente = new usuarios();
                usuario_remetente.usuario = item.usuario;
                usuario_remetente = aplicacaoUsuario.BuscaUsuario(usuario_remetente);

                sAux = "O Documento Acadêmico foi Reprovado<br><br>";
                sAux = sAux + "Título: <strong>" + item.nomePreview + "</strong><br>";
                sAux = sAux + "Descrição: <strong>" + item.descricaoPreview + "</strong><br>";
                sAux = sAux + "Nome do Documento: <strong>" + item.nome_arquivoPreview + "</strong><br>";
                sAux = sAux + "Responsável: <strong>" + usuario.nomeSocial.Trim() + "</strong><br>";
                sAux = sAux + "Obs: <strong>" + txtObsAprovacao.Value.Trim() + "</strong><br>";

                if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString == "Producao")
                {
                    sTo = usuario_remetente.email;
                }
                else
                {
                    sTo = usuario.email;
                    sAux = sAux + "<br><br> <strong>Esse email seria enviado para:</strong>" + usuario_remetente.email;
                }

                //sTo = "kelsey@ipt.br";

                Utilizades.fEnviaEmail(sFrom, sFrom_Nome, sTo, "", "", sAssunto, sAux, item_configuracoes.servidor_email, item_configuracoes.conta_email, item_configuracoes.senha_email, item_configuracoes.porta_email.Value, 1, "");

                CarregarPagina();

                (this.Master as SERPI).PreencheSininho();

                //=================

                item = aplicacaoDocumentosAcademicos.BuscaItem(item);

                lblMensagem.Text = "Documento Acadêmico REPROVADO com sucesso.";
                lblTituloMensagem.Text = "Documento Acadêmico REPROVADO";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na manutenção do Documento Acadêmico. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Documento Acadêmico";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }

    }
}