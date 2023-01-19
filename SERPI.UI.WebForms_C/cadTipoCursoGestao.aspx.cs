using Aplicacao_C;
using SERPI.Dominio_C;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class cadTipoCursoGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 73)) // Tipo de Curso - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                if (Session["sNewtipos_curso"] != null && (Boolean)Session["sNewtipos_curso"] != true)
                {
                    CarregarPagina();
                }
                else
                {
                    //lblInativado.Style["display"] = "none";
                    btnAtivar.Style["display"] = "none";
                    btnInativar.Style["display"] = "none";

                    lblTituloPagina.Text = "(novo)";
                    txtIdTipoCurso.Value = "";
                    txtDataCadastro.Value = "";
                    txtDataAlteracao.Value = "";
                    txtStatus.Value = "";
                    txtResponsavel.Value = "";

                    txtTipoCurso.Value = "";
                    txtTipoCurso_en.Value = "";

                    //Preview
                    optStatusHomePageSim.Checked = false;
                    optStatusHomePageNao.Checked = false;

                    optStatusBotoesSim.Checked = false;
                    optStatusBotoesNao.Checked = false;
                    txtDescricaoHomePage.Value = "";
                    txtBotaoCalendario.Value = "";
                    txtBotaoProcesso.Value = "";
                    //==============
                    txtDescricaoHomePage_en.Value = "";
                    txtBotaoCalendario_en.Value = "";
                    txtBotaoProcesso_en.Value = "";
                    //==========
                    sectionBanner.Style["background-image"] = "url('./img/homepage/cursos/')";
                    lblMostarBotoeProcessoCalendario.Visible = false;
                    lblTextoHomeAlterado.Visible = false;
                    lblTextoBotaoProcesso.Visible = false;
                    lblTextoBotaoCalendario.Visible = false;
                    lblImagemAlterada.Visible = false;
                    divbtnAprovarHome.Visible = false;
                    txtObsAlteracao.Value = "";
                    //Preview

                    //Aprovada

                    lblMostrarHomeAprovado.InnerText = "(não definido)";
                    lblBotaoCorpoDocente.InnerText = "(não definido)";
                    txtDescricaoHomePageAprovado.Value = "";
                    txtBotaoCalendarioAprovado.Value = "";
                    txtBotaoProcessoAprovado.Value = "";
                    //==================
                    txtDescricaoHomePageAprovado_en.Value = "";
                    txtBotaoCalendarioAprovado_en.Value = "";
                    txtBotaoProcessoAprovado_en.Value = "";

                    sectionBannerAprovado.Style["background-image"] = "";
                    //Aprovada


                    //divCoordenadores.Visible = false;
                    //divDisciplinas.Visible = false;
                    //divCoordenadoresAdicionados.Visible = false;
                    //divDisciplinasAdicionadas.Visible = false;

                    btnCriarTipoCurso.Disabled = true;
                    divEdicao.Visible = false;

                    divQRCode.Visible = false;
                    txtEnderecoQRCode.Value = "";
                }
            }

        }

        protected void CarregarPagina()
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            tipos_curso item;
            item = (tipos_curso)Session["tipos_curso"];
            if (item.id_tipo_curso == 1)
            {
                divNomeEnglish.Visible = true;
                divEnglish.Visible = true;
                divEnglishProducao.Visible = true;
            }
            else
            {
                divNomeEnglish.Visible = false;
                divEnglish.Visible = false;
                divEnglishProducao.Visible = false;
            }

            lblTituloPagina.Text = "(Editar) - N.º " + item.id_tipo_curso.ToString();
            txtIdTipoCurso.Value = item.id_tipo_curso.ToString();

            //if (item.status == "inativado")
            //{
            //    lblInativado.Style["display"] = "block";
            //    btnAtivar.Style["display"] = "block";
            //    btnInativar.Style["display"] = "none";
            //}
            //else
            //{
            //    lblInativado.Style["display"] = "none";
            //    btnAtivar.Style["display"] = "none";
            //    btnInativar.Style["display"] = "block";
            //}

            txtDataCadastro.Value = String.Format("{0:dd/MM/yyyy}", item.data_cadastro);
            txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy}", item.data_alteracao);
            txtStatus.Value = item.status;
            txtResponsavel.Value = item.usuario;

            txtTipoCurso.Value = item.tipo_curso;
            txtTipoCurso_en.Value = item.tipo_curso_en;

            divQRCode.Visible = true;
            txtEnderecoQRCode.Value = "https://sapiens.ipt.br?TipoCurso=" + item.id_tipo_curso;


            //Homepage ===== Inicio ===

            //Preview
            if (item.statusHomepagePeview == 1)
            {
                optStatusHomePageSim.Checked = true;
                optStatusHomePageNao.Checked = false;
            }
            else
            {
                optStatusHomePageSim.Checked = false;
                optStatusHomePageNao.Checked = true;
            }
            if (item.status_BotoesPeview == 1)
            {
                optStatusBotoesSim.Checked = true;
                optStatusBotoesNao.Checked = false;
            }
            else
            {
                optStatusBotoesSim.Checked = false;
                optStatusBotoesNao.Checked = true;
            }

            txtDescricaoHomePage.Value = item.decricao_homepagePeview;
            txtBotaoCalendario.Value = item.calendarioPeview;
            txtBotaoProcesso.Value = item.processo_seletivoPeview;
            //===============
            txtDescricaoHomePage_en.Value = item.decricao_homepagePeview_en;
            txtBotaoCalendario_en.Value = item.calendarioPreview_en;
            txtBotaoProcesso_en.Value = item.processo_seletivoPreview_en;

            sectionBanner.Style["background-image"] = "url('./img/homepage/cursos/" + item.nome_imagemPeview + "?v=" + item.data_imagemPeview + "')";

            if (item.statusAprovado == 0)
            {
                lblSituacaoPaginaPreview.InnerText = "Aguardando aprovação";
                lblSituacaoPaginaPreview.Attributes.Add("class", "text-red piscante");

                if (usuario.grupos_acesso.id_grupo == 8 || usuario.grupos_acesso.id_grupo == 1) //1= TI e 8=Gerencia
                {
                    divbtnAprovarHome.Visible = true;
                }
                else
                {
                    divbtnAprovarHome.Visible = false;
                }

            }
            else if (item.statusAprovado == 2)
            {
                lblSituacaoPaginaPreview.InnerText = "Reprovado em: " + String.Format("{0:dd/MM/yyyy HH:mm}", item.data_reprovacao) + " por: " + item.usuario_aprovacao;
                lblSituacaoPaginaPreview.Attributes.Add("class", "text-red piscante");

                divbtnAprovarHome.Visible = false;

            }
            else
            {
                lblSituacaoPaginaPreview.InnerText = "Aprovado em: " + String.Format("{0:dd/MM/yyyy HH:mm}", item.data_aprovacao) + " por: " + item.usuario_aprovacao;
                lblSituacaoPaginaPreview.Attributes.Remove("class");

                divbtnAprovarHome.Visible = false;
            }

            lblSituacaoPaginaAprovada.InnerText = "Aprovado em: " + String.Format("{0:dd/MM/yyyy HH:mm}", item.data_aprovacao) + " por: " + item.usuario_aprovacao;

            if (item.statusHomepagePeview == item.statusHomepage)
            {
                lblAlteradoMostrarHome.Visible = false;
            }
            if (item.status_BotoesPeview == item.statusBotoes)
            {
                lblMostarBotoeProcessoCalendario.Visible = false;
            }
            if (item.decricao_homepagePeview == item.descricao_homepage)
            {
                lblTextoHomeAlterado.Visible = false;
            }
            if (item.processo_seletivoPeview == item.processo_seletivo)
            {
                lblTextoBotaoProcesso.Visible = false;
            }
            if (item.calendarioPeview == item.calendario)
            {
                lblTextoBotaoCalendario.Visible = false;
            }
            //===
            if (item.decricao_homepagePeview_en == item.descricao_homepage_en)
            {
                lblTextoHomeAlterado_en.Visible = false;
            }
            if (item.processo_seletivoPreview_en == item.processo_seletivo_en)
            {
                lblTextoBotaoProcesso_en.Visible = false;
            }
            if (item.calendarioPreview_en == item.calendario_en)
            {
                lblTextoBotaoCalendario_en.Visible = false;
            }

            if (item.nome_imagemPeview == item.nome_imagem && item.data_imagemPeview == item.data_imagem)
            {
                lblImagemAlterada.Visible = false;
            }

            txtObsAlteracao.Value = item.obs_preview;

            //Preview

            //Aprovada
            if (item.statusHomepage == 1)
            {
                lblMostrarHomeAprovado.InnerText = "Sim";
            }
            else if (item.statusHomepage == 0)
            {
                lblMostrarHomeAprovado.InnerText = "Não";
            }
            else
            {
                lblMostrarHomeAprovado.InnerText = "(não definido)";
            }
            if (item.statusBotoes == 1)
            {
                lblBotaoCorpoDocente.InnerText = "Sim";
            }
            else if (item.statusBotoes == 0)
            {
                lblBotaoCorpoDocente.InnerText = "Não";
            }
            else
            {
                lblBotaoCorpoDocente.InnerText = "(não definido)";
            }
            txtDescricaoHomePageAprovado.Value = item.descricao_homepage;
            txtBotaoProcessoAprovado.Value = item.processo_seletivo;
            txtBotaoCalendarioAprovado.Value = item.calendario;
            //====
            txtDescricaoHomePageAprovado_en.Value = item.descricao_homepage_en;
            txtBotaoProcessoAprovado_en.Value = item.processo_seletivo_en;
            txtBotaoCalendarioAprovado_en.Value = item.calendario_en;
            sectionBannerAprovado.Style["background-image"] = "url('./img/homepage/cursos/" + item.nome_imagem + "?v=" + item.data_imagem + "')";
            //Aprovada

            //Homepage ===== Fim ===

            btnCriarTipoCurso.Disabled = false;
            divEdicao.Visible = true;

            if (Session["AdiciondoSucesso"] != null)
            {
                if ((Boolean)Session["AdiciondoSucesso"])
                {
                    Session["AdiciondoSucesso"] = null;
                    lblMensagem.Text = "Novo Tipo de Curso adicionado com sucesso";
                    lblTituloMensagem.Text = "Novo Tipo de Curso";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
                }

            }
        }

        protected void btnCriarPeriodo_Click(object sender, EventArgs e)
        {
            Session["sNewtipos_curso"] = true;
            Session["tipos_curso"] = null;
            Session["AdiciondoSucesso"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("cadTipoCurso.aspx", true);
        }

        protected void btnAprovarHome_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];
            string sAux;

            if (usuario.grupos_acesso.id_grupo == 8 || usuario.grupos_acesso.id_grupo == 1)
            {
                TipoCursoAplicacao aplicacaoTipoCurso = new TipoCursoAplicacao();
                tipos_curso item = new tipos_curso();
                item = (tipos_curso)Session["tipos_curso"];

                item.statusAprovado = 1;
                item.data_aprovacao = DateTime.Now;
                item.usuario_aprovacao = usuario.usuario;

                aplicacaoTipoCurso.AlterarItem_Aprovacao(item);

                item.statusHomepage = item.statusHomepagePeview;
                item.statusBotoes = item.status_BotoesPeview;
                item.descricao_homepage = item.decricao_homepagePeview;
                item.calendario = item.calendarioPeview;
                item.processo_seletivo = item.processo_seletivoPeview;
                //
                item.descricao_homepage_en = item.decricao_homepagePeview_en;
                item.calendario_en = item.calendarioPreview_en;
                item.processo_seletivo_en = item.processo_seletivoPreview_en;

                item.nome_imagem = item.nome_imagemPeview;
                item.data_imagem = item.data_imagemPeview;
                item.data_aprovacao = item.data_aprovacao;
                item.usuario_aprovacao = item.usuario_aprovacao;
                item.statusAprovado = item.statusAprovado;
                item.obs_preview = "";

                Session["tipos_curso"] = item;

                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Configuracoes item_configuracoes;
                // 1 = email mestrado@ipt.br
                // 2 = email suporte@ipt.br
                item_configuracoes = aplicacaoGerais.BuscaConfiguracoes(1);

                string sFrom = item_configuracoes.remetente_email;
                string sFrom_Nome = item_configuracoes.nome_remetente_email;
                string sTo;
                string sAssunto = "Aprovação das alterações da Homepage do Tipo de curso " + item.tipo_curso;

                UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                usuarios usuario_remetente = new usuarios();
                usuario_remetente.usuario = item.usuario;
                usuario_remetente = aplicacaoUsuario.BuscaUsuario(usuario_remetente);

                sAux = "Suas alterações foram aprovadas.<br><br>";

                if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString == "Producao")
                {
                    sTo = usuario_remetente.email;
                }
                else
                {
                    sTo = "kelsey@ipt.br";
                }

                Utilizades.fEnviaEmail(sFrom, sFrom_Nome, sTo, "", "", sAssunto, sAux, item_configuracoes.servidor_email, item_configuracoes.conta_email, item_configuracoes.senha_email, item_configuracoes.porta_email.Value, 1, "");

                CarregarPagina();

                (this.Master as SERPI).PreencheSininho();

                sAux = "Aprovação executada com sucesso.";

                lblMensagem.Text = sAux;
                lblTituloMensagem.Text = "Atenção";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

                return;

            }
            else
            {
                sAux = "Você não tem permissão para executar essa atividade";

                lblMensagem.Text = sAux;
                lblTituloMensagem.Text = "Atenção";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                return;
            }
        }

        protected void btnReprovarHome_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];
            string sAux = "";

            if (usuario.grupos_acesso.id_grupo == 8 || usuario.grupos_acesso.id_grupo == 1)
            {
                TipoCursoAplicacao aplicacaoTipoCurso = new TipoCursoAplicacao();
                tipos_curso item = new tipos_curso();
                item = (tipos_curso)Session["tipos_curso"];

                item.statusAprovado = 2;
                item.data_reprovacao = DateTime.Now;
                item.usuario_aprovacao = usuario.usuario;
                item.obs_preview = txtObsAlteracao.Value;

                aplicacaoTipoCurso.AlterarItem_Reprovacao(item);

                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Configuracoes item_configuracoes;
                // 1 = email mestrado@ipt.br
                // 2 = email suporte@ipt.br
                item_configuracoes = aplicacaoGerais.BuscaConfiguracoes(1);

                string sFrom = item_configuracoes.remetente_email;
                string sFrom_Nome = item_configuracoes.nome_remetente_email;
                string sTo;
                string sAssunto = "Reprovação das alterações da Homepage do Tipo de curso " + item.tipo_curso;

                UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                usuarios usuario_remetente = new usuarios();
                usuario_remetente.usuario = item.usuario;
                usuario_remetente = aplicacaoUsuario.BuscaUsuario(usuario_remetente);

                sAux = "Suas alterações foram reprovadas.<br>Verifique os apontamentos abaixo e providencie as alterações.<br><br>";
                sAux = sAux + "<b>Observações sobre a alteração: </b>" + item.obs_preview;

                if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString == "Producao")
                {
                    sTo = usuario_remetente.email;
                }
                else
                {
                    sTo = "kelsey@ipt.br";
                }

                Utilizades.fEnviaEmail(sFrom, sFrom_Nome, sTo, "", "", sAssunto, sAux, item_configuracoes.servidor_email, item_configuracoes.conta_email, item_configuracoes.senha_email, item_configuracoes.porta_email.Value, 1, "");

                Session["cursos"] = item;

                CarregarPagina();

                (this.Master as SERPI).PreencheSininho();

                sAux = "Reprovação executada com sucesso.";

                lblMensagem.Text = sAux;
                lblTituloMensagem.Text = "Atenção";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

                return;

            }
            else
            {
                sAux = "Você não tem permissão para executar essa atividade";

                lblMensagem.Text = sAux;
                lblTituloMensagem.Text = "Atenção";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                return;
            }
        }

        protected void btnSalvar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";
                string sAuxDescricaoHomePage = HttpContext.Current.Request["hCodigotxtDescicaoHomePage"]; 
                string sAuxBotaoCalendario = HttpContext.Current.Request["hCodigotxtBotaoCalendario"]; 
                string sAuxBotaoProcesso = HttpContext.Current.Request["hCodigotxtBotaoProcesso"];

                //txtDescricaoHomePage.Value = sAuxDescricaoHomePage;
                //txtBotaoCalendario.Value = sAuxBotaoCalendario;
                //txtBotaoProcesso.Value = sAuxBotaoProcesso;
                tipos_curso item;
                item = (tipos_curso)Session["tipos_curso"];

                if (txtTipoCurso.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a descrição do Tipo de Curso. <br/><br/>";
                }

                if (txtTipoCurso_en.Value.Trim() == "" && item.id_tipo_curso == 1)
                {
                    sAux = sAux + "Preencher a descrição (em inglês) do Tipo de Curso. <br/><br/>";
                }

                if (item.statusHomepage == 1 && txtDescricaoHomePage.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Texto da HomePage pois se optou por mostrar esse Tipo de Curso na HomePage do Tipo de Curso. <br/><br/>";
                }

                if (optStatusBotoesSim.Checked && txtBotaoCalendario.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Texto do Botão Calendário pois se optou por mostrar os botões na HomePage do Tipo de Curso. <br/><br/>";
                }

                if (optStatusBotoesSim.Checked && txtBotaoProcesso.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Texto do Botão Processo Seletivo pois se optou por mostrar os botões na HomePage do Tipo de Curso. <br/><br/>";
                }

                if (item.statusHomepage == 1 && txtDescricaoHomePage_en.Value.Trim() == "" && item.id_tipo_curso == 1)
                {
                    sAux = sAux + "Preencher o Texto da HomePage (in english) pois se optou por mostrar esse Tipo de Curso na HomePage do Tipo de Curso. <br/><br/>";
                }

                if (optStatusBotoesSim.Checked && txtBotaoCalendario_en.Value.Trim() == "" && item.id_tipo_curso == 1)
                {
                    sAux = sAux + "Preencher o Texto do Botão Calendário (in english) pois se optou por mostrar os botões na HomePage do Tipo de Curso. <br/><br/>";
                }

                if (optStatusBotoesSim.Checked && txtBotaoProcesso_en.Value.Trim() == "" && item.id_tipo_curso == 1)
                {
                    sAux = sAux + "Preencher o Texto do Botão Processo Seletivo (in english) pois se optou por mostrar os botões na HomePage do Tipo de Curso. <br/><br/>";
                }

                if (sAux != "")
                {
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                    return;
                }

                if (Session["sNewtipos_curso"] != null && (Boolean)Session["sNewtipos_curso"] != true)
                {
                    //Alteração de registro
                    TipoCursoAplicacao aplicacaoTipoCurso = new TipoCursoAplicacao();
                    item = new tipos_curso();
                    tipos_curso itemRetorno = new tipos_curso();

                    item = (tipos_curso)Session["tipos_curso"];

                    itemRetorno.id_tipo_curso = item.id_tipo_curso;
                    itemRetorno.tipo_curso = txtTipoCurso.Value.Trim();
                    itemRetorno.tipo_curso_en = txtTipoCurso_en.Value.Trim();
                    itemRetorno = aplicacaoTipoCurso.VerificaItemMesmoTipoCurso_MesmoNome(itemRetorno);

                    if (itemRetorno != null)
                    {
                        if (itemRetorno.status != "inativado")
                        {
                            sAux = "Já existe um Tipo de Curso cadastrado com o mesmo Nome <br /><br />";
                            //sAux = sAux + "<strong>Código:</strong> " + itemRetorno.sigla + "<br />";
                            //sAux = sAux + "<strong>Nome:</strong> " + itemRetorno.nome + "<br />";

                            lblMensagem.Text = sAux;
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                            return;
                        }
                        else
                        {
                            sAux = "Já existe um Tipo de cadastrado com o mesmo Nome, porém ele está <strong>inativado</strong>.<br /><br />";
                            //sAux = sAux + "<strong>Código:</strong> " + itemRetorno.sigla + "<br />";
                            //sAux = sAux + "<strong>Nome:</strong> " + itemRetorno.nome + "<br />";

                            lblMensagem.Text = sAux;
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                            return;
                        }
                    }

                    item.status = "alterado";
                    //item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    item.tipo_curso = txtTipoCurso.Value.Trim();
                    item.tipo_curso_en = txtTipoCurso_en.Value.Trim();

                    item.obs_preview = txtObsAlteracao.Value.Trim();

                    //if (optStatusHomePageSim.Checked)
                    //{
                    //    item.statusHomepagePeview = 1;
                    //}
                    //else
                    //{
                    //    item.statusHomepagePeview = 0;
                    //}

                    item.statusHomepagePeview = item.statusHomepage;

                    if (optStatusBotoesSim.Checked)
                    {
                        item.status_BotoesPeview = 1;
                    }
                    else
                    {
                        item.status_BotoesPeview = 0;
                    }

                    item.decricao_homepagePeview = txtDescricaoHomePage.Value.Trim();
                    item.calendarioPeview = txtBotaoCalendario.Value.Trim();
                    item.processo_seletivoPeview = txtBotaoProcesso.Value.Trim();
                    //====
                    item.decricao_homepagePeview_en = txtDescricaoHomePage_en.Value.Trim();
                    item.calendarioPreview_en = txtBotaoCalendario_en.Value.Trim();
                    item.processo_seletivoPreview_en = txtBotaoProcesso_en.Value.Trim();

                    if (fileArquivoParaGravar.HasFile)
                    {
                        fileArquivoParaGravar.SaveAs(Server.MapPath("") + "\\img\\homepage\\cursos\\" + fileArquivoParaGravar.FileName);
                        item.nome_imagemPeview = fileArquivoParaGravar.FileName;
                        item.data_imagemPeview = DateTime.Now;
                        sectionBanner.Style["background-image"] = "url('./img/homepage/cursos/" + item.nome_imagemPeview + "?v=" + item.data_imagemPeview + "')";
                    }

                    lblAlteradoMostrarHome.Visible = false;
                    lblMostarBotoeProcessoCalendario.Visible = false;
                    lblTextoHomeAlterado.Visible = false;
                    lblTextoBotaoCalendario.Visible = false;
                    lblTextoBotaoProcesso.Visible = false;
                    lblImagemAlterada.Visible = false;
                    sAux = "";

                    if ((item.decricao_homepagePeview != item.descricao_homepage
                        || item.nome_imagemPeview != item.nome_imagem
                        || item.data_imagemPeview != item.data_imagem
                        || item.status_BotoesPeview != item.statusBotoes
                        || item.calendarioPeview != item.calendario
                        || item.processo_seletivoPeview != item.processo_seletivo
                        || item.decricao_homepagePeview_en != item.descricao_homepage_en
                        || item.calendarioPreview_en != item.calendario_en
                        || item.processo_seletivoPreview_en != item.processo_seletivo_en
                        && item.id_tipo_curso == 1)
                        || (item.decricao_homepagePeview != item.descricao_homepage
                        || item.nome_imagemPeview != item.nome_imagem
                        || item.data_imagemPeview != item.data_imagem
                        || item.status_BotoesPeview != item.statusBotoes
                        || item.calendarioPeview != item.calendario
                        || item.processo_seletivoPeview != item.processo_seletivo
                        && item.id_tipo_curso != 1)
                        )

                    {
                        
                        if (item.obs_preview == "")
                        {
                            lblMensagem.Text = "Deve-se descrever a alteração realizada na <strong>'Homepage'</strong> no campo <strong>'Observações sobre a alteração'</strong>";
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                            return;
                        }

                        item.statusAprovado = 0; // 0 = Aguardando aprovação (1 = Aprovado -- 2 = Reprovado -- 3 = Sem página)

                        //if (item.statusHomepagePreview != item.statusHomepage)
                        //{
                        //    lblAlteradoMostrarHome.Visible = true;
                        //    sAux = sAux + "<br> - Mostrar na HomePage";
                        //}
                        if (item.status_BotoesPeview != item.statusBotoes)
                        {
                            lblMostarBotoeProcessoCalendario.Visible = true;
                            sAux = sAux + "<br> - Mostrar botões 'Processo Seletivo e Calendário'";
                        }
                        if (item.decricao_homepagePeview!= item.descricao_homepage)
                        {
                            lblTextoHomeAlterado.Visible = true;
                            sAux = sAux + "<br> - Texto da HomePage";
                        }
                        if (item.calendarioPeview != item.calendario)
                        {
                            lblTextoBotaoCalendario.Visible = true;
                            sAux = sAux + "<br> - Texto Botão Calendário";
                        }
                        if (item.processo_seletivoPeview != item.processo_seletivo)
                        {
                            lblTextoBotaoProcesso.Visible = true;
                            sAux = sAux + "<br> - Texto Botão Processo Seletivo";
                        }
                        //
                        if (item.decricao_homepagePeview_en != item.descricao_homepage_en)
                        {
                            lblTextoHomeAlterado_en.Visible = true;
                            sAux = sAux + "<br> - Texto da HomePage (in english)";
                        }
                        if (item.calendarioPreview_en != item.calendario_en)
                        {
                            lblTextoBotaoCalendario.Visible = true;
                            sAux = sAux + "<br> - Texto Botão Calendário (in english)";
                        }
                        if (item.processo_seletivoPreview_en != item.processo_seletivo_en)
                        {
                            lblTextoBotaoProcesso.Visible = true;
                            sAux = sAux + "<br> - Texto Botão Processo Seletivo (in english)";
                        }

                        if (item.nome_imagemPeview != item.nome_imagem || item.data_imagemPeview != item.data_imagem)
                        {
                            lblImagemAlterada.Visible = true;
                            sAux = sAux + "<br> - Imagem da Página";
                        }
                        
                        sAux = sAux + "<br><br><b>Descrição da(s) alteração(ões):</b> " + item.obs_preview;

                        sAux = sAux + "<br><br><br><b>Alterado por: </b>" + item.usuario;
                        sAux = sAux + "<br><br><b>em: </b>" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", item.data_alteracao);

                        lblSituacaoPaginaPreview.InnerText = "Aguardando aprovação";
                        lblSituacaoPaginaPreview.Attributes.Add("class", "text-red piscante");
                        sAux = "<b>Os seguintes items abaixo foram alterados na HomePage do Tipo de curso " + item.tipo_curso + ":</b><br>" + sAux + "<br><br> Favor verificar e providenciar a aprovação.";

                        //É do grupo coordenador então pegar todos os cursos em que ele é coordenador
                        UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                        List<usuarios> lista_usuario;
                        usuarios item_usuario = new usuarios();
                        item_usuario.grupos_acesso = new grupos_acesso();
                        item_usuario.grupos_acesso.id_grupo = 8; //Gerência
                        lista_usuario = aplicacaoUsuario.ListaUsuario_porGrupoAcesso(item_usuario);

                        GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                        Configuracoes item_configuracoes;
                        // 1 = email mestrado@ipt.br
                        // 2 = email suporte@ipt.br
                        item_configuracoes = aplicacaoGerais.BuscaConfiguracoes(1);

                        string sFrom = item_configuracoes.remetente_email;
                        string sFrom_Nome = item_configuracoes.nome_remetente_email;
                        string sTo;
                        string sAssunto = "Alteração da Homepage do Tipo de curso " + item.tipo_curso;

                        foreach (var elemento in lista_usuario)
                        {
                            //ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
                            //professores item_professor = new professores();

                            //if (elemento.usuario.Substring(elemento.usuario.Length - 1, 1) == "p")
                            //{
                            //    item_professor.id_professor = Convert.ToInt32(elemento.usuario.Substring(0, elemento.usuario.Length - 1));
                            //    item_professor = aplicacaoProfessor.BuscaItem(item_professor);
                            //}
                            //else
                            //{
                            //    item_professor.cpf = elemento.usuario;
                            //    item_professor = aplicacaoProfessor.BuscaItem_byCPF(item_professor);
                            //}


                            if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString == "Producao")
                            {
                                sTo = elemento.email;
                            }
                            else
                            {
                                sTo = "kelsey@ipt.br";
                            }

                            //sTo = "kelsey@ipt.br";

                            Utilizades.fEnviaEmail(sFrom, sFrom_Nome, sTo, "", "", sAssunto, sAux + "<br><br>Obs.: Seu login é: " + elemento.usuario, item_configuracoes.servidor_email, item_configuracoes.conta_email, item_configuracoes.senha_email, item_configuracoes.porta_email.Value, 1, "");

                            //(this.Master as SERPI).PreencheSininho();

                        }

                    }

                    aplicacaoTipoCurso.AlterarItem(item);

                    item = aplicacaoTipoCurso.BuscaItem(item);

                    txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy}", item.data_alteracao);
                    txtStatus.Value = item.status;
                    txtResponsavel.Value = item.usuario;

                    lblMensagem.Text = "Tipo de Curso alterado com sucesso.";
                    lblTituloMensagem.Text = "Alteração de Tipo de Curso";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

                    Session["tipos_curso"] = item;

                    (this.Master as SERPI).PreencheSininho();

                    CarregarPagina();

                }
                else
                {
                    //Inclusão de registro
                    TipoCursoAplicacao aplicacaoTipoCurso = new TipoCursoAplicacao();

                    item = new tipos_curso();
                    tipos_curso itemRetorno = new tipos_curso();

                    itemRetorno.id_tipo_curso = 0;
                    itemRetorno.tipo_curso = txtTipoCurso.Value.Trim();
                    itemRetorno.tipo_curso_en = txtTipoCurso_en.Value.Trim();
                    itemRetorno = aplicacaoTipoCurso.VerificaItemMesmoTipoCurso_MesmoNome(itemRetorno);

                    if (itemRetorno != null)
                    {
                        if (itemRetorno.status != "inativado")
                        {
                            sAux = "Já existe um Tipo de Curso cadastrado com o mesmo Nome <br /><br />";
                            //sAux = sAux + "<strong>Código:</strong> " + itemRetorno.sigla + "<br />";
                            //sAux = sAux + "<strong>Nome:</strong> " + itemRetorno.nome + "<br />";

                            lblMensagem.Text = sAux;
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                            return;
                        }
                        else
                        {
                            sAux = "Já existe um Tipo de cadastrado com o mesmo Nome, porém ele está <strong>inativado</strong>.<br /><br />";
                            //sAux = sAux + "<strong>Código:</strong> " + itemRetorno.sigla + "<br />";
                            //sAux = sAux + "<strong>Nome:</strong> " + itemRetorno.nome + "<br />";

                            lblMensagem.Text = sAux;
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                            return;
                        }
                    }

                    item.status = "cadastrado";
                    item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    item.tipo_curso = txtTipoCurso.Value.Trim();
                    item.tipo_curso_en = txtTipoCurso_en.Value.Trim();

                    item = aplicacaoTipoCurso.CriarItem(item);

                    if (item != null)
                    {
                        Session["tipos_curso"] = item;
                        Session.Add("sNewtipos_curso", false);
                        Session["AdiciondoSucesso"] = true;
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na manutenção do Período. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Período";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }

    }
}