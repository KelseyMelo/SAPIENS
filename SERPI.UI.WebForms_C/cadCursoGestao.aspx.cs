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
    public partial class cadCursoGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 12)) // Curso - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                GeraisAplicacao aplicacaoGarais = new GeraisAplicacao();
                List<tipos_curso> listaTipoCurso = aplicacaoGarais.ListaTipoCurso();

                ddlTipoCurso.Items.Clear();
                ddlTipoCurso.DataSource = listaTipoCurso.OrderBy(x => x.id_tipo_curso);
                ddlTipoCurso.DataValueField = "id_tipo_curso";
                ddlTipoCurso.DataTextField = "tipo_curso";
                ddlTipoCurso.DataBind();
                ddlTipoCurso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Tipo de Curso", ""));
                ddlTipoCurso.SelectedValue = "";

                List<curso_tipo_coordenador> listaTipoCoordenador = aplicacaoGarais.ListaTipoCoordenador();
                ddlTipoCoordenador.Items.Clear();
                ddlTipoCoordenador.DataSource = listaTipoCoordenador.OrderBy(x => x.id_tipo_coordenador);
                ddlTipoCoordenador.DataValueField = "id_tipo_coordenador";
                ddlTipoCoordenador.DataTextField = "descricao";
                ddlTipoCoordenador.DataBind();
                ddlTipoCoordenador.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Tipo de Coordenador", ""));
                ddlTipoCoordenador.SelectedValue = "";

                //ddlNomeCursoArea.Attributes.Add("onselectedindexchanged", "javascript: ShowProgress();");
                //ddlCodigoCursoArea.Attributes.Add("onselectedindexchanged", "javascript: ShowProgress();");

                if (Session["sNewCurso"] != null && (Boolean)Session["sNewCurso"] != true)
                {
                    CarregarPagina();

                }
                else
                {
                    lblInativado.Style["display"] = "none";
                    btnAtivar.Style["display"] = "none";
                    btnInativar.Style["display"] = "none";

                    lblTituloPagina.Text = "(novo)";
                    txtIdCurso.Value = "";
                    txtDataCadastro.Value = "";
                    txtDataAlteracao.Value = "";
                    txtStatus.Value = "";
                    txtResponsavel.Value = "";

                    txtCodigoCurso.Value = "";
                    txtNomeCurso.Value = "";
                    txtNomeCurso_en.Value = "";

                    ddlTipoCurso.SelectedValue = "";
                    txtCargaHorariaCurso.Value = "";
                    txtCreditosCurso.Value = "";
                    txtNumeroMaxDisciplinaCurso.Value = "";


                    txtPortatiaMEC_Curso.Value = "";
                    txtDataPortatiaMEC_Curso.Value = "";
                    txtDataDiarioOficialCurso.Value = "";
                    txtConceitoCapesCurso.Value = "";
                    txtNumeroCapesCurso.Value = "";

                    spanAsterisco_txtNumeroMaxDisciplinaCurso.Style.Add("display", "none");
                    spanAsterisco_txtConceitoCapesCurso.Style.Add("display", "none");
                    spanAsterisco_txtDataDiarioOficialCurso.Style.Add("display", "none");
                    spanAsterisco_txtDataPortatiaMEC_Curso.Style.Add("display", "none");
                    spanAsterisco_txtNumeroCapesCurso.Style.Add("display", "none");
                    spanAsterisco_txtPortatiaMEC_Curso.Style.Add("display", "none");

                    txtObservacaoCurso.Value = "";

                    divQRCode.Visible = false;
                    txtEnderecoQRCode.Value = "";

                    //Preview
                    optStatusHomePageSim.Checked = false;
                    optStatusHomePageNao.Checked = true;
                    optStatusBotoesSim.Checked = false;
                    optStatusBotoesNao.Checked = true;
                    txtDescricaoHomePage.Value = "";
                    txtBotaoCorpoDocente.Value = "";
                    //==
                    txtDescricaoHomePage_en.Value = "";
                    txtBotaoCorpoDocente_en.Value = "";
                    sectionBanner.Style["background-image"] = "";
                    lblAlteradoMostrarHome.Visible = false;
                    lblMostarBotaoCorpoAlterado.Visible = false;
                    lblTextoHomeAlterado.Visible = false;
                    lblTextoBotaoCorpoAlterado.Visible = false;
                    //==
                    lblTextoHomeAlterado_en.Visible = false;
                    lblTextoBotaoCorpoAlterado_en.Visible = false;
                    lblImagemAlterada.Visible = false;
                    divbtnAprovarHome.Visible = false;
                    txtObsAlteracao.Value = "";
                    //Preview

                    //Aprovada

                    lblMostrarHomeAprovado.InnerText = "(não definido)";
                    lblBotaoCorpoDocente.InnerText = "(não definido)";
                    txtDescricaoHomePageAprovado.Value = "";
                    txtBotaoCorpoDocenteAprovado.Value = "";
                    //==
                    txtDescricaoHomePageAprovado_en.Value = "";
                    txtBotaoCorpoDocenteAprovado_en.Value = "";
                    sectionBannerAprovado.Style["background-image"] = "";
                    //Aprovada


                    //divCoordenadores.Visible = false;
                    //divDisciplinas.Visible = false;
                    //divCoordenadoresAdicionados.Visible = false;
                    //divDisciplinasAdicionadas.Visible = false;

                    divComRegistro.Visible = false;
                    btnCriarCurso.Disabled = true;
                    divEdicao.Visible = false;
                }
            }

        }

        protected void CarregarPagina()
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            cursos item;
            item = (cursos)Session["cursos"];
            lblTituloPagina.Text = "(Editar) - N.º " + item.id_curso;
            txtIdCurso.Value = item.id_curso.ToString();

            if (item.status == "inativado")
            {
                lblInativado.Style["display"] = "block";
                btnAtivar.Style["display"] = "block";
                btnInativar.Style["display"] = "none";
            }
            else
            {
                lblInativado.Style["display"] = "none";
                btnAtivar.Style["display"] = "none";
                btnInativar.Style["display"] = "block";
            }

            txtDataCadastro.Value = String.Format("{0:dd/MM/yyyy}", item.data_cadastro);
            txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy}", item.data_alteracao);
            txtStatus.Value = item.status;
            txtResponsavel.Value = item.usuario;

            txtCodigoCurso.Value = item.sigla;
            txtNomeCurso.Value = item.nome;
            txtNomeCurso_en.Value = item.nome_en;

            if (item.id_tipo_curso == 1)
            {
                divNomeEnglish.Style.Add("display", "block");
                divEnglish.Style.Add("display", "block");
                divEnglishProducao.Style.Add("display", "block");
            }
            else
            {
                divNomeEnglish.Style.Add("display", "none");
                divEnglish.Style.Add("display", "none");
                divEnglishProducao.Style.Add("display", "none");
            }

            ddlTipoCurso.SelectedValue = item.id_tipo_curso.ToString();
            txtCargaHorariaCurso.Value = item.carga_horaria.ToString();
            txtCreditosCurso.Value = item.creditos.ToString();
            txtNumeroMaxDisciplinaCurso.Value = item.num_max_disciplinas.ToString();

            txtPortatiaMEC_Curso.Value = item.portaria_mec;
            txtDataPortatiaMEC_Curso.Value = String.Format("{0:yyyy-MM-dd}", item.data_portaria_mec);
            txtDataDiarioOficialCurso.Value = String.Format("{0:yyyy-MM-dd}", item.data_diario_oficial);
            txtConceitoCapesCurso.Value = item.conceito_capes;
            txtNumeroCapesCurso.Value = item.numero_capes;

            if (item.id_tipo_curso == 1) //Mestrado
            {
                spanAsterisco_txtNumeroMaxDisciplinaCurso.Style.Add("display", "inline-block");
                spanAsterisco_txtConceitoCapesCurso.Style.Add("display", "inline-block");
                spanAsterisco_txtDataDiarioOficialCurso.Style.Add("display", "inline-block");
                spanAsterisco_txtDataPortatiaMEC_Curso.Style.Add("display", "inline-block");
                spanAsterisco_txtNumeroCapesCurso.Style.Add("display", "inline-block");
                spanAsterisco_txtPortatiaMEC_Curso.Style.Add("display", "inline-block");
            }
            else
            {
                spanAsterisco_txtNumeroMaxDisciplinaCurso.Style.Add("display", "none");
                spanAsterisco_txtConceitoCapesCurso.Style.Add("display", "none");
                spanAsterisco_txtDataDiarioOficialCurso.Style.Add("display", "none");
                spanAsterisco_txtDataPortatiaMEC_Curso.Style.Add("display", "none");
                spanAsterisco_txtNumeroCapesCurso.Style.Add("display", "none");
                spanAsterisco_txtPortatiaMEC_Curso.Style.Add("display", "none");
            }

            txtObservacaoCurso.Value = item.observacao;

            divQRCode.Visible = true;
            txtEnderecoQRCode.Value = "https://sapiens.ipt.br?p=" + item.id_curso;

            //Homepage ===== Inicio ===

            //Preview
            if (item.statusHomepagePreview == 1)
            {
                optStatusHomePageSim.Checked = true;
                optStatusHomePageNao.Checked = false;
            }
            else
            {
                optStatusHomePageSim.Checked = false;
                optStatusHomePageNao.Checked = true;
            }
            if (item.statusBotaoPreview == 1)
            {
                optStatusBotoesSim.Checked = true;
                optStatusBotoesNao.Checked = false;
            }
            else
            {
                optStatusBotoesSim.Checked = false;
                optStatusBotoesNao.Checked = true;
            }
            if (item.statusBotaoPreview_en == 1)
            {
                optStatusBotoesSim_en.Checked = true;
                optStatusBotoesNao_en.Checked = false;
            }
            else
            {
                optStatusBotoesSim_en.Checked = false;
                optStatusBotoesNao_en.Checked = true;
            }
            txtDescricaoHomePage.Value = item.descricao_homepagePreview;
            txtBotaoCorpoDocente.Value = item.corpo_docentePreview;
            //===
            txtDescricaoHomePage_en.Value = item.descricao_homepagePreview_en;
            txtBotaoCorpoDocente_en.Value = item.corpo_docentePreview_en;

            sectionBanner.Style["background-image"] = "url('./img/homepage/cursos/" + item.nome_imagemPreview + "?v=" + item.data_imagemPreview + "')";

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

            if (item.statusHomepagePreview == item.statusHomepage)
            {
                lblAlteradoMostrarHome.Visible = false;
            }
            if (item.statusBotaoPreview == item.statusBotao)
            {
                lblMostarBotaoCorpoAlterado.Visible = false;
            }
            if (item.descricao_homepagePreview == item.descricao_homepage)
            {
                lblTextoHomeAlterado.Visible = false;
            }
            if (item.corpo_docentePreview == item.corpo_docente)
            {
                lblTextoBotaoCorpoAlterado.Visible = false;
            }
            //==
            if (item.descricao_homepagePreview_en == item.descricao_homepage_en)
            {
                lblTextoHomeAlterado_en.Visible = false;
            }
            if (item.statusBotaoPreview_en == item.statusBotao_en)
            {
                lblMostarBotaoCorpoAlterado_en.Visible = false;
            }
            if (item.corpo_docentePreview_en == item.corpo_docente_en)
            {
                lblTextoBotaoCorpoAlterado_en.Visible = false;
            }
            if (item.nome_imagemPreview == item.nome_imagem && item.data_imagemPreview == item.data_imagem)
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
            if (item.statusBotao == 1)
            {
                lblBotaoCorpoDocente.InnerText = "Sim";
            }
            else if (item.statusBotao == 0)
            {
                lblBotaoCorpoDocente.InnerText = "Não";
            }
            else
            {
                lblBotaoCorpoDocente.InnerText = "(não definido)";
            }
            txtDescricaoHomePageAprovado.Value = item.descricao_homepage;
            txtBotaoCorpoDocenteAprovado.Value = item.corpo_docente;
            //=====
            if (item.statusBotao_en == 1)
            {
                lblBotaoCorpoDocente_en.InnerText = "Sim";
            }
            else if (item.statusBotao_en == 0)
            {
                lblBotaoCorpoDocente_en.InnerText = "Não";
            }
            else
            {
                lblBotaoCorpoDocente_en.InnerText = "(não definido)";
            }

            txtDescricaoHomePageAprovado_en.Value = item.descricao_homepage_en;
            txtBotaoCorpoDocenteAprovado_en.Value = item.corpo_docente_en;
            sectionBannerAprovado.Style["background-image"] = "url('./img/homepage/cursos/" + item.nome_imagem + "?v=" + item.data_imagem + "')";
            //Aprovada

            //Homepage ===== Fim ===

            //PreencheCoordenadorAdicionado(item.cursos_coordenadores.ToList());

            //PreencheDisciplinaAdicionada(item.cursos_disciplinas.ToList());

            //divCoordenadores.Visible = true;
            //divDisciplinas.Visible = true;
            //divCoordenadoresAdicionados.Visible = false;
            //divDisciplinasAdicionadas.Visible = false;

            divComRegistro.Visible = true;

            btnCriarCurso.Disabled = false;
            divEdicao.Visible = true;


            if (Session["AdiciondoSucesso"] != null)
            {
                if ((Boolean)Session["AdiciondoSucesso"])
                {
                    Session["AdiciondoSucesso"] = null;
                    lblMensagem.Text = "Novo Curso adicionado com sucesso";
                    lblTituloMensagem.Text = "Novo Curso";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
                }

            }
        }

        protected void PreencheCoordenadorAdicionado(List<cursos_coordenadores> lista)
        {
            GridCoordenadorAdicionado item;
            List<GridCoordenadorAdicionado> listaCoordenadorAdicionado = new List<GridCoordenadorAdicionado>();
            foreach (var elemento in lista)
            {
                item = new GridCoordenadorAdicionado();
                item.id_Professor = Convert.ToInt32(elemento.id_professor);
                item.cpf = elemento.professores.cpf;
                item.nome = elemento.professores.nome;

                listaCoordenadorAdicionado.Add(item);
            }

            grdCoordenadorAdicionado.DataSource = listaCoordenadorAdicionado;
            grdCoordenadorAdicionado.DataBind();

            if (lista.Count > 0)
            {
                grdCoordenadorAdicionado.UseAccessibleHeader = true;
                grdCoordenadorAdicionado.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultadogrdCoordenadorAdicionado.Visible = false;
                grdCoordenadorAdicionado.Visible = true;
            }
            else
            {
                msgSemResultadogrdCoordenadorAdicionado.Visible = true;
            }

        }

        protected void PreencheDisciplinaAdicionada(List<cursos_disciplinas> lista)
        {
            grdDisciplinaAdicionada.DataSource = lista;
            grdDisciplinaAdicionada.DataBind();

            if (lista.Count > 0)
            {
                grdDisciplinaAdicionada.UseAccessibleHeader = true;
                grdDisciplinaAdicionada.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultadogrdDisciplinaAdicionada.Visible = false;
                grdDisciplinaAdicionada.Visible = true;
                ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.UpdatePanel4.GetType(), "Script", "javascript:fAtiva_grdDisciplina();", true);
            }
            else
            {
                msgSemResultadogrdDisciplinaAdicionada.Visible = true;
            }

        }

        protected void btnCriarCurso_Click(object sender, EventArgs e)
        {
            Session["sNewCurso"] = true;
            Session["cursos"] = null;
            Session["AdiciondoSucesso"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("cadCurso.aspx", true);
        }

        public class GridCoordenadorAdicionado
        {
            private int _id_Professor;

            private string _cpf;

            private string _foto;

            private string _nome;

            private bool _responsavel;

            public int id_Professor
            {
                get
                {
                    return _id_Professor;
                }
                set
                {
                    _id_Professor = value;
                }
            }

            public string cpf
            {
                get
                {
                    return _cpf;
                }
                set
                {
                    _cpf = value;
                }
            }

            public string nome
            {
                get
                {
                    return _nome;
                }
                set
                {
                    _nome = value;
                }
            }

            public bool responsavel
            {
                get
                {
                    return _responsavel;
                }
                set
                {
                    _responsavel = value;
                }
            }

        }

        protected void btnAprovarHome_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];
            string sAux;

            if (usuario.grupos_acesso.id_grupo == 8 || usuario.grupos_acesso.id_grupo == 1)
            {
                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos item = new cursos();
                item = (cursos)Session["cursos"];

                item.statusAprovado = 1;
                item.data_aprovacao = DateTime.Now;
                item.usuario_aprovacao = usuario.usuario;

                aplicacaoCurso.AlterarItem_Aprovacao(item);

                item.statusHomepage = item.statusHomepagePreview;
                item.statusBotao = item.statusBotaoPreview;
                item.descricao_homepage = item.descricao_homepagePreview;
                item.corpo_docente = item.corpo_docentePreview;
                //==
                item.statusBotao_en = item.statusBotaoPreview_en;
                item.descricao_homepage_en = item.descricao_homepagePreview_en;
                item.corpo_docente_en = item.corpo_docentePreview_en;
                item.nome_imagem = item.nome_imagemPreview;
                item.data_imagem = item.data_imagemPreview;
                item.data_aprovacao = item.data_aprovacao;
                item.usuario_aprovacao = item.usuario_aprovacao;
                item.statusAprovado = item.statusAprovado;
                item.obs_preview = "";

                Session["cursos"] = item;

                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Configuracoes item_configuracoes;

                // 1 = email mestrado@ipt.br
                // 2 = email suporte@ipt.br
                item_configuracoes = aplicacaoGerais.BuscaConfiguracoes(1);

                string sFrom = item_configuracoes.remetente_email;
                string sFrom_Nome = item_configuracoes.nome_remetente_email;
                string sTo;
                string sAssunto = "Aprovação das alterações da Homepage do curso " + item.nome;

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

                Utilizades.fEnviaEmail(sFrom,sFrom_Nome, sTo, "", "", sAssunto, sAux, item_configuracoes.servidor_email, item_configuracoes.conta_email, item_configuracoes.senha_email, item_configuracoes.porta_email.Value, 1, "");

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
                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos item = new cursos();
                item = (cursos)Session["cursos"];

                item.statusAprovado = 2;
                item.data_reprovacao = DateTime.Now;
                item.usuario_aprovacao = usuario.usuario;
                item.obs_preview = txtObsAlteracao.Value;

                aplicacaoCurso.AlterarItem_Reprovacao(item);

                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Configuracoes item_configuracoes;

                // 1 = email mestrado@ipt.br
                // 2 = email suporte@ipt.br
                item_configuracoes = aplicacaoGerais.BuscaConfiguracoes(1);

                string sFrom = item_configuracoes.remetente_email;
                string sFrom_Nome = item_configuracoes.nome_remetente_email;
                string sTo;
                string sAssunto = "Reprovação das alterações da Homepage do curso " + item.nome;

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

        protected void btnSalvar_ServerClick1(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";

                string sAuxDescricaoHomePage = txtDescricaoHomePage.Value; // HttpContext.Current.Request["hCodigotxtDescicaoHomePage"];
                string sAuxBotaoCorpoDocente = txtBotaoCorpoDocente.Value; // HttpContext.Current.Request["hCodigotxtCorpoDocente"];
                //====
                string sAuxDescricaoHomePage_en = txtDescricaoHomePage_en.Value; // HttpContext.Current.Request["hCodigotxtDescicaoHomePage"];
                string sAuxBotaoCorpoDocente_en = txtBotaoCorpoDocente_en.Value;

                //txtDescricaoHomePage.Value = sAuxDescricaoHomePage;
                //txtBotaoCorpoDocente.Value = sAuxBotaoCorpoDocente;


                if (txtNomeCurso.Value.Trim() == "")
                {
                    sAux = "Preencher o nome da Curso. <br/><br/>";
                }
                if (txtCodigoCurso.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o código do Curso. <br/><br/>";
                }
                else
                {
                    bool isAlphaBet = Regex.IsMatch(txtCodigoCurso.Value.Trim().Substring(0, 1), "[a-z]", RegexOptions.IgnoreCase);
                    if (!isAlphaBet)
                    {
                        sAux = sAux + "O Código do Curso deve sempre começar com uma Letra. <br/><br/>";
                    }
                }

                if (ddlTipoCurso.SelectedValue == "")
                {
                    sAux = sAux + "Selecione um Tipo do Curso. <br/><br/>";
                }

                if (ddlTipoCurso.SelectedValue == "1")
                {
                    if (txtNomeCurso_en.Value.Trim() == "")
                    {
                        sAux = "Preencher o nome do Curso (in english). <br/><br/>";
                    }
                    if (txtPortatiaMEC_Curso.Value.Trim() == "")
                    {
                        sAux = sAux + "Preencher a Portaria MEC. <br/><br/>";
                    }

                    if (txtDataPortatiaMEC_Curso.Value.Trim() == "")
                    {
                        sAux = sAux + "Preencher a Data Portaria MEC. <br/><br/>";
                    }

                    if (txtDataDiarioOficialCurso.Value.Trim() == "")
                    {
                        sAux = sAux + "Preencher a Data Diário Oficial. <br/><br/>";
                    }

                    if (txtConceitoCapesCurso.Value.Trim() == "")
                    {
                        sAux = sAux + "Preencher o Conceito na CAPES. <br/><br/>";
                    }

                    if (txtNumeroCapesCurso.Value.Trim() == "")
                    {
                        sAux = sAux + "Preencher o Número na CAPES. <br/><br/>";
                    }

                    if (txtNumeroMaxDisciplinaCurso.Value.Trim() == "")
                    {
                        sAux = sAux + "Preencher o Número Máximo de Disciplinas. <br/><br/>";
                    }
                    else if (Convert.ToInt32(txtNumeroMaxDisciplinaCurso.Value.Trim()) < 1)
                    {
                        sAux = sAux + "O Número Máximo de Disciplinas deve ser maior que '0'. <br/><br/>";
                    }

                }

                //=============================

                if (optStatusHomePageSim.Checked && sAuxDescricaoHomePage == "")
                {
                    sAux = sAux + "Preencher o Texto da HomePage pois se optou por mostrar esse Curso na HomePage do <strong>SAPIENS</strong>. <br/><br/>";
                }

                if (optStatusHomePageSim.Checked && sAuxDescricaoHomePage_en == "" && ddlTipoCurso.SelectedValue == "1")
                {
                    sAux = sAux + "Preencher o Texto da HomePage (in english) pois se optou por mostrar esse Curso na HomePage do <strong>SAPIENS</strong>. <br/><br/>";
                }

                if (optStatusBotoesSim.Checked && sAuxBotaoCorpoDocente == "")
                {
                    sAux = sAux + "Preencher o Texto do Botão Corpo Docente pois se optou por mostrar esse botão na HomePage desse Curso. <br/><br/>";
                }

                if (optStatusBotoesSim_en.Checked && sAuxBotaoCorpoDocente_en == "" && ddlTipoCurso.SelectedValue == "1")
                {
                    sAux = sAux + "Preencher o Texto do Botão Corpo Docente (in english) pois se optou por mostrar esse botão na HomePage desse Curso. <br/><br/>";
                }

                if (sAux != "")
                {
                    if (ddlTipoCurso.SelectedValue == "1") //Mestrado
                    {
                        spanAsterisco_txtNumeroMaxDisciplinaCurso.Style.Add("display", "inline-block");
                        spanAsterisco_txtConceitoCapesCurso.Style.Add("display", "inline-block");
                        spanAsterisco_txtDataDiarioOficialCurso.Style.Add("display", "inline-block");
                        spanAsterisco_txtDataPortatiaMEC_Curso.Style.Add("display", "inline-block");
                        spanAsterisco_txtNumeroCapesCurso.Style.Add("display", "inline-block");
                        spanAsterisco_txtPortatiaMEC_Curso.Style.Add("display", "inline-block");
                    }
                    else
                    {
                        spanAsterisco_txtNumeroMaxDisciplinaCurso.Style.Add("display", "none");
                        spanAsterisco_txtConceitoCapesCurso.Style.Add("display", "none");
                        spanAsterisco_txtDataDiarioOficialCurso.Style.Add("display", "none");
                        spanAsterisco_txtDataPortatiaMEC_Curso.Style.Add("display", "none");
                        spanAsterisco_txtNumeroCapesCurso.Style.Add("display", "none");
                        spanAsterisco_txtPortatiaMEC_Curso.Style.Add("display", "none");
                    }

                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                    return;
                }

                if (Session["sNewCurso"] != null && (Boolean)Session["sNewCurso"] != true)
                {
                    //Alteração
                    CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                    cursos item = new cursos();
                    cursos item_atual = (cursos)Session["cursos"];
                    cursos itemRetorno;

                    item = (cursos)Session["cursos"];
                    item.nome = txtNomeCurso.Value.Trim();
                    item.nome_en = txtNomeCurso_en.Value.Trim();
                    item.sigla = txtCodigoCurso.Value.Trim();

                    item.id_tipo_curso = Convert.ToInt32(ddlTipoCurso.SelectedValue);

                    if (txtCargaHorariaCurso.Value.Trim() != "")
                    {
                        item.carga_horaria = Convert.ToInt32(txtCargaHorariaCurso.Value);
                    }

                    if (txtCreditosCurso.Value.Trim() != "")
                    {
                        item.creditos = Convert.ToInt32(txtCreditosCurso.Value);
                    }

                    if (txtNumeroMaxDisciplinaCurso.Value.Trim() != "")
                    {
                        item.num_max_disciplinas = Convert.ToInt32(txtNumeroMaxDisciplinaCurso.Value);
                    }

                    item.portaria_mec = txtPortatiaMEC_Curso.Value.Trim();

                    if (txtDataPortatiaMEC_Curso.Value != "")
                    {
                        item.data_portaria_mec = Convert.ToDateTime(txtDataPortatiaMEC_Curso.Value);
                    }

                    if (txtDataDiarioOficialCurso.Value != "")
                    {
                        item.data_diario_oficial = Convert.ToDateTime(txtDataDiarioOficialCurso.Value);
                    }
                    item.conceito_capes = txtConceitoCapesCurso.Value.Trim();
                    item.numero_capes = txtNumeroCapesCurso.Value.Trim();

                    item.observacao = txtObservacaoCurso.Value.Trim();

                    item.obs_preview = txtObsAlteracao.Value.Trim();

                    item.status = "alterado";
                    //item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    if (optStatusHomePageSim.Checked)
                    {
                        item.statusHomepagePreview = 1;
                    }
                    else
                    {
                        item.statusHomepagePreview = 0;
                    }
                    if (optStatusBotoesSim.Checked)
                    {
                        item.statusBotaoPreview = 1;
                    }
                    else
                    {
                        item.statusBotaoPreview = 0;
                    }

                    if (optStatusBotoesSim_en.Checked)
                    {
                        item.statusBotaoPreview_en = 1;
                    }
                    else
                    {
                        item.statusBotaoPreview_en = 0;
                    }

                    item.descricao_homepagePreview = sAuxDescricaoHomePage;
                    item.corpo_docentePreview = txtBotaoCorpoDocente.Value.Trim();
                    //==
                    item.descricao_homepagePreview_en = sAuxDescricaoHomePage_en;
                    item.corpo_docentePreview_en = txtBotaoCorpoDocente_en.Value.Trim();
                    if (fileArquivoParaGravar.HasFile)
                    {
                        fileArquivoParaGravar.SaveAs(Server.MapPath("") + "\\img\\homepage\\cursos\\" + fileArquivoParaGravar.FileName);
                        item.nome_imagemPreview = fileArquivoParaGravar.FileName;
                        item.data_imagemPreview = DateTime.Today;
                        sectionBanner.Style["background-image"] = "url('./img/homepage/cursos/" + item.nome_imagemPreview + "?v=" + item.data_imagemPreview + "')";
                    }

                    itemRetorno = aplicacaoCurso.VerificaItemMesmaSigla(item);

                    if (itemRetorno != null)
                    {
                        if (itemRetorno.status != "inativado")
                        {
                            sAux = "Já existe um Curso cadastrado com o mesmo Código <br /><br />";
                            sAux = sAux + "<strong>Código:</strong> " + itemRetorno.sigla + "<br />";
                            sAux = sAux + "<strong>Nome:</strong> " + itemRetorno.nome + "<br />";

                            lblMensagem.Text = sAux;
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                            return;
                        }
                        else
                        {
                            sAux = "Já existe um Curso cadastrado com o mesmo Código, porém ele está <strong>inativado</strong>.<br /><br />";
                            sAux = sAux + "<strong>Código:</strong> " + itemRetorno.sigla + "<br />";
                            sAux = sAux + "<strong>Nome:</strong> " + itemRetorno.nome + "<br />";

                            lblMensagem.Text = sAux;
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                            return;
                        }
                    }

                    itemRetorno = aplicacaoCurso.VerificaItemMesmoTipoCurso_MesmoNome(item);

                    if (itemRetorno != null)
                    {
                        if (itemRetorno.status != "inativado")
                        {
                            sAux = "Já existe um Curso cadastrado com o mesmo Nome nesse Tipo de Curso <br /><br />";
                            sAux = sAux + "<strong>Código:</strong> " + itemRetorno.sigla + "<br />";
                            sAux = sAux + "<strong>Nome:</strong> " + itemRetorno.nome + "<br />";

                            lblMensagem.Text = sAux;
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                            return;
                        }
                        else
                        {
                            sAux = "Já existe um Curso cadastrado com o mesmo Nome nesse Tipo de Curso, porém ele está <strong>inativado</strong>.<br /><br />";
                            sAux = sAux + "<strong>Código:</strong> " + itemRetorno.sigla + "<br />";
                            sAux = sAux + "<strong>Nome:</strong> " + itemRetorno.nome + "<br />";

                            lblMensagem.Text = sAux;
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                            return;
                        }
                    }

                    lblAlteradoMostrarHome.Visible = false;
                    lblMostarBotaoCorpoAlterado.Visible = false;
                    lblMostarBotaoCorpoAlterado_en.Visible = false;
                    lblTextoHomeAlterado.Visible = false;
                    lblTextoBotaoCorpoAlterado.Visible = false;
                    //==
                    lblTextoHomeAlterado_en.Visible = false;
                    lblTextoBotaoCorpoAlterado_en.Visible = false;
                    lblImagemAlterada.Visible = false;
                    sAux = "";

                    if ((item.descricao_homepagePreview != item.descricao_homepage 
                        || item.statusHomepagePreview != item.statusHomepage
                        || item.nome_imagemPreview != item.nome_imagem
                        || item.data_imagemPreview != item.data_imagem
                        || item.statusBotaoPreview != item.statusBotao
                        || item.corpo_docentePreview != item.corpo_docente
                        && item.id_tipo_curso != 1)
                        ||
                        (item.descricao_homepagePreview != item.descricao_homepage
                        || item.statusHomepagePreview != item.statusHomepage
                        || item.nome_imagemPreview != item.nome_imagem
                        || item.data_imagemPreview != item.data_imagem
                        || item.statusBotaoPreview != item.statusBotao
                        || item.corpo_docentePreview != item.corpo_docente
                        || item.descricao_homepagePreview_en != item.descricao_homepage_en
                        || item.corpo_docentePreview_en != item.corpo_docente_en
                        || item.statusBotaoPreview_en != item.statusBotao_en
                        && item.id_tipo_curso == 1))
                    {

                        if (item.obs_preview == "")
                        {
                            lblMensagem.Text = "Deve-se descrever a alteração realizada na <strong>'Homepage'</strong> no campo <strong>'Observações sobre a alteração'</strong>";
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                            return;
                        }

                        item.statusAprovado = 0; // 0 = Aguardando aprovação (1 = Aprovado -- 2 = Reprovado -- 3 = Sem página)

                        if (item.statusHomepagePreview != item.statusHomepage)
                        {
                            lblAlteradoMostrarHome.Visible = true;
                            sAux = sAux + "<br> - Mostrar na HomePage";
                        }
                        if (item.statusBotaoPreview != item.statusBotao)
                        {
                            lblMostarBotaoCorpoAlterado.Visible = true;
                            sAux = sAux + "<br> - Mostrar botão 'Corpo Docente'";
                        }
                        if (item.statusBotaoPreview_en != item.statusBotao_en)
                        {
                            lblMostarBotaoCorpoAlterado_en.Visible = true;
                            sAux = sAux + "<br> - Mostrar botão 'Faculty' (in english)";
                        }
                        if (item.descricao_homepagePreview != item.descricao_homepage)
                        {
                            lblTextoHomeAlterado.Visible = true;
                            sAux = sAux + "<br> - Texto da HomePage";
                        }
                        if (item.corpo_docentePreview != item.corpo_docente)
                        {
                            lblTextoBotaoCorpoAlterado.Visible = true;
                            sAux = sAux + "<br> - Texto Botão Corpo docente";
                        }
                        //==
                        if (item.descricao_homepagePreview_en != item.descricao_homepage_en)
                        {
                            lblTextoHomeAlterado_en.Visible = true;
                            sAux = sAux + "<br> - Texto da HomePage (in english)";
                        }
                        if (item.corpo_docentePreview_en != item.corpo_docente_en)
                        {
                            lblTextoBotaoCorpoAlterado_en.Visible = true;
                            sAux = sAux + "<br> - Texto Botão Corpo docente (in english)";
                        }
                        if (item.nome_imagemPreview != item.nome_imagem || item.data_imagemPreview != item.data_imagem)
                        {
                            lblImagemAlterada.Visible = true;
                            sAux = sAux + "<br> - Imagem da Página";
                        }

                        sAux = sAux + "<br><br><b>Descrição da(s) alteração(ões):</b> " + item.obs_preview;

                        sAux = sAux + "<br><br><br><b>Alterado por: </b>" + item.usuario;
                        sAux = sAux + "<br><br><b>em: </b>" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", item.data_alteracao);

                        lblSituacaoPaginaPreview.InnerText = "Aguardando aprovação";
                        lblSituacaoPaginaPreview.Attributes.Add("class", "text-red piscante");
                        sAux = "<b>Os seguintes items abaixo foram alterados na HomePage do curso " + item.nome + ":</b><br>" + sAux + "<br><br> Favor verificar e providenciar a aprovação.";

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
                        string sAssunto = "Alteração da Homepage do curso " + item.nome;

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

                    aplicacaoCurso.AlterarItem(item);

                    item = aplicacaoCurso.BuscaItem(item);

                    lblMensagem.Text = "Curso alterado com sucesso.";
                    lblTituloMensagem.Text = "Alteração de Curso";
                    if (grdDisciplinaAdicionada.Rows.Count > 0)
                    {
                        grdDisciplinaAdicionada.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');fAtiva_grdDisciplina();", true);

                    Session["cursos"] = item;

                    (this.Master as SERPI).PreencheSininho();

                    CarregarPagina();

                }
                else
                {
                    //=== INCLUSÂO ==============
                    CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                    cursos item = new cursos();
                    cursos itemRetorno;

                    item.nome = txtNomeCurso.Value.Trim();
                    item.nome_en = txtNomeCurso_en.Value.Trim();
                    item.sigla = txtCodigoCurso.Value.Trim();

                    item.id_tipo_curso = Convert.ToInt32(ddlTipoCurso.SelectedValue);

                    if (txtCargaHorariaCurso.Value.Trim() != "")
                    {
                        item.carga_horaria = Convert.ToInt32(txtCargaHorariaCurso.Value);
                    }

                    if (txtCreditosCurso.Value.Trim() != "")
                    {
                        item.creditos = Convert.ToInt32(txtCreditosCurso.Value);
                    }

                    if (txtNumeroMaxDisciplinaCurso.Value.Trim() != "")
                    {
                        item.num_max_disciplinas = Convert.ToInt32(txtNumeroMaxDisciplinaCurso.Value);
                    }

                    item.portaria_mec = txtPortatiaMEC_Curso.Value.Trim();
                    if (txtDataPortatiaMEC_Curso.Value != "")
                    {
                        item.data_portaria_mec = Convert.ToDateTime(txtDataPortatiaMEC_Curso.Value);
                    }

                    if (txtDataDiarioOficialCurso.Value != "")
                    {
                        item.data_diario_oficial = Convert.ToDateTime(txtDataDiarioOficialCurso.Value);
                    }
                    item.conceito_capes = txtConceitoCapesCurso.Value.Trim();
                    item.numero_capes = txtNumeroCapesCurso.Value.Trim();

                    item.observacao = txtObservacaoCurso.Value.Trim();
                    item.obs_preview = txtObsAlteracao.Value.Trim();

                    item.status = "cadastrado";
                    item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    //==============================

                    if (optStatusHomePageSim.Checked)
                    {
                        item.statusHomepagePreview = 1;
                    }
                    else
                    {
                        item.statusHomepagePreview = 0;
                    }
                    if (optStatusBotoesSim.Checked)
                    {
                        item.statusBotaoPreview = 1;
                    }
                    else
                    {
                        item.statusBotaoPreview = 0;
                    }
                    if (optStatusBotoesSim_en.Checked)
                    {
                        item.statusBotaoPreview_en = 1;
                    }
                    else
                    {
                        item.statusBotaoPreview_en = 0;
                    }

                    item.descricao_homepagePreview = sAuxDescricaoHomePage;
                    item.corpo_docentePreview = txtBotaoCorpoDocente.Value.Trim();
                    //==
                    item.descricao_homepagePreview_en = sAuxDescricaoHomePage_en;
                    item.corpo_docentePreview_en = txtBotaoCorpoDocente_en.Value.Trim();

                    if (fileArquivoParaGravar.HasFile)
                    {
                        fileArquivoParaGravar.SaveAs(Server.MapPath("") + "\\img\\homepage\\cursos\\" + fileArquivoParaGravar.FileName);
                        item.nome_imagemPreview = fileArquivoParaGravar.FileName;
                        item.data_imagemPreview = DateTime.Today;
                        sectionBanner.Style["background-image"] = "url('./img/homepage/cursos/" + item.nome_imagemPreview + "?v=" + item.data_imagemPreview + "')";
                    }

                    itemRetorno = aplicacaoCurso.VerificaItemMesmaSigla(item);

                    if (itemRetorno != null)
                    {
                        if (itemRetorno.status != "inativado")
                        {
                            sAux = "Já existe um Curso cadastrado com o mesmo Código <br /><br />";
                            sAux = sAux + "<strong>Código:</strong> " + itemRetorno.sigla + "<br />";
                            sAux = sAux + "<strong>Nome:</strong> " + itemRetorno.nome + "<br />";

                            lblMensagem.Text = sAux;
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                            return;
                        }
                        else
                        {
                            sAux = "Já existe um Curso cadastrado com o mesmo Código, porém ele está <strong>inativado</strong>.<br /><br />";
                            sAux = sAux + "<strong>Código:</strong> " + itemRetorno.sigla + "<br />";
                            sAux = sAux + "<strong>Nome:</strong> " + itemRetorno.nome + "<br />";

                            lblMensagem.Text = sAux;
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                            return;
                        }
                    }

                    itemRetorno = aplicacaoCurso.VerificaItemMesmoTipoCurso_MesmoNome(item);

                    if (itemRetorno != null)
                    {
                        if (itemRetorno.status != "inativado")
                        {
                            sAux = "Já existe um Curso cadastrado com o mesmo Nome nesse Tipo de Curso <br /><br />";
                            sAux = sAux + "<strong>Código:</strong> " + itemRetorno.sigla + "<br />";
                            sAux = sAux + "<strong>Nome:</strong> " + itemRetorno.nome + "<br />";

                            lblMensagem.Text = sAux;
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                            return;
                        }
                        else
                        {
                            sAux = "Já existe um Curso cadastrado com o mesmo Nome nesse Tipo de Curso, porém ele está <strong>inativado</strong>.<br /><br />";
                            sAux = sAux + "<strong>Código:</strong> " + itemRetorno.sigla + "<br />";
                            sAux = sAux + "<strong>Nome:</strong> " + itemRetorno.nome + "<br />";

                            lblMensagem.Text = sAux;
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                            return;
                        }
                    }

                    lblAlteradoMostrarHome.Visible = false;
                    lblMostarBotaoCorpoAlterado.Visible = false;
                    lblMostarBotaoCorpoAlterado_en.Visible = false;
                    lblTextoHomeAlterado.Visible = false;
                    lblTextoBotaoCorpoAlterado.Visible = false;
                    //==
                    lblTextoHomeAlterado_en.Visible = false;
                    lblTextoBotaoCorpoAlterado_en.Visible = false;
                    lblImagemAlterada.Visible = false;
                    sAux = "";

                    item.statusAprovado = 3; // 0 = Aguardando aprovação -- 1 = Aprovado -- 2 = Reprovado -- 3 = Sem página  

                    if ((item.descricao_homepagePreview != ""
                        || item.statusHomepagePreview != 0
                        || item.nome_imagemPreview != null
                        || item.data_imagemPreview != null
                        || item.statusBotaoPreview != 0
                        || item.corpo_docentePreview != ""
                        && item.id_tipo_curso != 1)
                        ||
                        (item.descricao_homepagePreview != ""
                        || item.statusHomepagePreview != 0
                        || item.nome_imagemPreview != null
                        || item.data_imagemPreview != null
                        || item.statusBotaoPreview != 0
                        || item.corpo_docentePreview != ""
                        || item.descricao_homepagePreview_en != ""
                        || item.corpo_docentePreview_en != ""
                        || item.statusBotaoPreview_en != 0
                        && item.id_tipo_curso == 1))
                    {
                        if (item.obs_preview == "")
                        {
                            lblMensagem.Text = "Deve-se descrever a alteração realizada na <strong>'Homepage'</strong> no campo <strong>'Observações sobre a alteração'</strong>";
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                            return;
                        }

                        item.statusAprovado = 0;  // Foi incluído um NOVO curso com página aguardando para ser aprovada.

                        if (item.statusHomepagePreview != item.statusHomepage)
                        {
                            lblAlteradoMostrarHome.Visible = true;
                            sAux = sAux + "<br> - Mostrar na HomePage";
                        }
                        if (item.statusBotaoPreview != item.statusBotao)
                        {
                            lblMostarBotaoCorpoAlterado.Visible = true;
                            sAux = sAux + "<br> - Mostrar botão 'Corpo Docente'";
                        }
                        if (item.statusBotaoPreview_en != item.statusBotao)
                        {
                            lblMostarBotaoCorpoAlterado_en.Visible = true;
                            sAux = sAux + "<br> - Mostrar botão 'Faculty' (in English)";
                        }
                        if (item.descricao_homepagePreview != item.descricao_homepage)
                        {
                            lblTextoHomeAlterado.Visible = true;
                            sAux = sAux + "<br> - Texto da HomePage";
                        }
                        if (item.corpo_docentePreview != item.corpo_docente)
                        {
                            lblTextoBotaoCorpoAlterado.Visible = true;
                            sAux = sAux + "<br> - Texto Botão Corpo docente";
                        }
                        //==
                        if (item.descricao_homepagePreview_en != item.descricao_homepage_en)
                        {
                            lblTextoHomeAlterado_en.Visible = true;
                            sAux = sAux + "<br> - Texto da HomePage (in English)";
                        }
                        if (item.corpo_docentePreview_en != item.corpo_docente_en)
                        {
                            lblTextoBotaoCorpoAlterado_en.Visible = true;
                            sAux = sAux + "<br> - Texto Botão Faculty (in English)";
                        }
                        if (item.nome_imagemPreview != item.nome_imagem || item.data_imagemPreview != item.data_imagem)
                        {
                            lblImagemAlterada.Visible = true;
                            sAux = sAux + "<br> - Imagem da Página";
                        }
                        lblSituacaoPaginaPreview.InnerText = "Aguardando aprovação";
                        lblSituacaoPaginaPreview.Attributes.Add("class", "text-red piscante");
                        sAux = "Os seguintes items abaixo foram inseridos na HomePage do curso " + item.nome + "<br>" + sAux + "<br><br> Favor verificar e providenciar a aprovação.";


                        sAux = sAux + "<br><br>Descrição da(s) inclusão(ões): " + item.obs_preview;

                        sAux = sAux + "<br><br>Incluído por: " + item.usuario;
                        sAux = sAux + "<br><br>em: " + String.Format("{0:dd/MM/yyyy HH:mm:ss}", item.data_alteracao);

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
                        string sAssunto = "Criação da Homepage do curso " + item.nome;

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
                    else
                    {
                        item.descricao_homepage = "";
                        item.statusHomepage = 0;
                        item.nome_imagem = null;
                        item.data_imagem = null;
                        item.statusBotao = 0;
                        item.corpo_docente = "";
                        //==
                        item.statusBotaoPreview_en = 0;
                        item.descricao_homepage_en = "";
                        item.corpo_docente_en = "";
                    }

                    item = aplicacaoCurso.CriarItem(item);

                    lblMensagem.Text = "Curso criado com sucesso.";
                    lblTituloMensagem.Text = "Criação de Curso";
                    if (grdDisciplinaAdicionada.Rows.Count > 0)
                    {
                        grdDisciplinaAdicionada.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');fAtiva_grdDisciplina();", true);

                    Session["cursos"] = item;

                    (this.Master as SERPI).PreencheSininho();

                    CarregarPagina();
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na manutenção do Curso. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Curso";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }

        public void grdCoordenadorAdicionado_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                int linha = Convert.ToInt32(e.CommandArgument);
                int codigo = Convert.ToInt32(grdCoordenadorAdicionado.DataKeys[linha].Values[0]);
                cursos item = new cursos();
                item = (cursos)Session["cursos"];
                cursos_coordenadores itemCoordenadores = new cursos_coordenadores();
                itemCoordenadores.id_curso = item.id_curso;
                itemCoordenadores.id_professor = codigo;

                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                aplicacaoCurso.ExcluirCoordenador(itemCoordenadores);
                item = aplicacaoCurso.BuscaItem(item);
                Session["cursos"] = item;
                PreencheCoordenadorAdicionado(item.cursos_coordenadores.ToList());
                PreencheDisciplinaAdicionada(item.cursos_disciplinas.ToList());
            }
        }

        public void grdDisciplinaAdicionada_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                int linha = Convert.ToInt32(e.CommandArgument);
                int codigo = Convert.ToInt32(grdDisciplinaAdicionada.DataKeys[linha].Values[0]);
                cursos item = new cursos();
                item = (cursos)Session["cursos"];
                cursos_disciplinas itemDisciplina = new cursos_disciplinas();
                itemDisciplina.id_curso = item.id_curso;
                itemDisciplina.id_disciplina = codigo;

                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                aplicacaoCurso.ExcluirDisciplina(itemDisciplina);
                item = aplicacaoCurso.BuscaItem(item);
                Session["cursos"] = item;
                PreencheDisciplinaAdicionada(item.cursos_disciplinas.ToList());

            }
        }

        protected void bntPerquisaCoordenador_Click(object sender, EventArgs e)
        {
            CursoAplicacao aplicacaoCurso = new CursoAplicacao();
            cursos item;
            item = (cursos)Session["cursos"];
            professores itemCoordenador = new professores();
            List<professores> listaCoordenador = new List<professores>();

            if (txtCPFPerquisaCoordenador.Value.Trim() != "")
            {
                itemCoordenador.cpf = txtCPFPerquisaCoordenador.Value.Trim();
            }

            if (txtNomePerquisaCoordenador.Value.Trim() != "")
            {
                itemCoordenador.nome = txtNomePerquisaCoordenador.Value.Trim();
            }

            listaCoordenador = aplicacaoCurso.ListaProfessoresDisponiveis(item, itemCoordenador);


            GridCoordenadorAdicionado itemGrade;
            List<GridCoordenadorAdicionado> listaCoordenadorAdicionado = new List<GridCoordenadorAdicionado>();

            foreach (var elemento in listaCoordenador)
            {
                itemGrade = new GridCoordenadorAdicionado();
                itemGrade.id_Professor= Convert.ToInt32(elemento.id_professor);
                itemGrade.cpf = elemento.cpf;
                itemGrade.nome = elemento.nome;

                listaCoordenadorAdicionado.Add(itemGrade);
            }

            grdCoordenadoresDisponiveis.DataSource = listaCoordenadorAdicionado;
            grdCoordenadoresDisponiveis.DataBind();

            if (listaCoordenadorAdicionado.Count > 0)
            {
                grdCoordenadoresDisponiveis.UseAccessibleHeader = true;
                grdCoordenadoresDisponiveis.HeaderRow.TableSection = TableRowSection.TableHeader;
                divgrdCoordenadoresDisponiveis.Style.Add("display", "none");
                grdCoordenadoresDisponiveis.Visible = true;
                if (grdDisciplinaAdicionada.Rows.Count > 0)
                {
                    grdDisciplinaAdicionada.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fAtiva_grdCoordenadoresDisponiveis()", true);
            }
            else
            {
                divgrdCoordenadoresDisponiveis.Style.Add("display", "block");
            }

            divResultadoListaCoordenadorDisponivel.Style.Add("display", "block");

            
        }

        protected void bntPerquisaDisciplina_Click(object sender, EventArgs e)
        {
            CursoAplicacao aplicacaoCurso = new CursoAplicacao();
            cursos item;
            item = (cursos)Session["cursos"];
            disciplinas itemDisciplina = new disciplinas();
            List<disciplinas> listaDisciplinas = new List<disciplinas>();

            if (txtCodigoPerquisaDisciplina.Value.Trim() != "")
            {
                itemDisciplina.codigo = txtCodigoPerquisaDisciplina.Value.Trim();
            }

            if (txtNomePerquisaDisciplina.Value.Trim() != "")
            {
                itemDisciplina.nome = txtNomePerquisaDisciplina.Value.Trim();
            }

            listaDisciplinas = aplicacaoCurso.ListaDisciplinasDisponiveis(item, itemDisciplina);

            grdDisciplinaDisponiveis.DataSource = listaDisciplinas;
            grdDisciplinaDisponiveis.DataBind();

            if (listaDisciplinas.Count > 0)
            {
                grdDisciplinaDisponiveis.UseAccessibleHeader = true;
                grdDisciplinaDisponiveis.HeaderRow.TableSection = TableRowSection.TableHeader;
                divgrdDisciplinaDisponiveis.Style.Add("display", "none");
                grdDisciplinaDisponiveis.Visible = true;
                if (grdDisciplinaAdicionada.Rows.Count > 0)
                {
                    grdDisciplinaAdicionada.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.UpdatePanel6.GetType(), "Script", "javascript:fAtiva_grdDisciplinaDisponiveis()", true);
            }
            else
            {
                divgrdDisciplinaDisponiveis.Style.Add("display", "block");
            }
            
            divResultadoListaDisciplinaDisponivel.Style.Add("display", "block");
        }

        public void grdCoordenadoresDisponiveis_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                int linha = Convert.ToInt32(e.CommandArgument);
                int codigo = Convert.ToInt32(grdCoordenadoresDisponiveis.DataKeys[linha].Values[0]);
                cursos item = new cursos();
                item = (cursos)Session["cursos"];
                cursos_coordenadores itemCoordenadores = new cursos_coordenadores();
                itemCoordenadores.id_curso = item.id_curso;
                itemCoordenadores.id_professor = codigo;

                itemCoordenadores.status = "cadastrado";
                itemCoordenadores.data_cadastro = DateTime.Now;
                itemCoordenadores.data_alteracao = DateTime.Now;
                itemCoordenadores.usuario = usuario.usuario;

                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                aplicacaoCurso.IncluirCoordenador_Curso(itemCoordenadores);
                item = aplicacaoCurso.BuscaItem(item);
                Session["cursos"] = item;
                PreencheCoordenadorAdicionado(item.cursos_coordenadores.ToList());
                bntPerquisaCoordenador_Click(null, null);
            }
        }

        public void grdDisciplinaDisponiveis_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                int linha = Convert.ToInt32(e.CommandArgument);
                int codigo = Convert.ToInt32(grdDisciplinaDisponiveis.DataKeys[linha].Values[0]);
                cursos item = new cursos();
                item = (cursos)Session["cursos"];
                cursos_disciplinas itemDisciplina = new cursos_disciplinas();
                itemDisciplina.id_curso = item.id_curso;
                itemDisciplina.id_disciplina = codigo;

                itemDisciplina.status = "cadastrado";
                itemDisciplina.data_cadastro = DateTime.Now;
                itemDisciplina.data_alteracao = DateTime.Now;
                itemDisciplina.usuario = usuario.usuario;

                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                aplicacaoCurso.IncluirDisciplina_Curso(itemDisciplina);
                item = aplicacaoCurso.BuscaItem(item);
                Session["cursos"] = item;
                PreencheDisciplinaAdicionada(item.cursos_disciplinas.ToList());
                bntPerquisaDisciplina_Click(null, null);
                //ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.UpdatePanel6.GetType(), "Script", "javascript:fAtiva_grdDisciplinaDisponiveis()", true);
            }
        }
    }
}