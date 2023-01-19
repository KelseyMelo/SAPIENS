using Aplicacao_C;
using SERPI.Dominio_C;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class fichaInscricao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                periodo_inscricao_curso item = new periodo_inscricao_curso();
                item = (periodo_inscricao_curso)Session["periodo_inscricao_curso"];

                if (item != null)
                {
                    txtTipoCurso.Value = item.cursos.tipos_curso.tipo_curso;
                    txtNomeCurso.Value = item.cursos.nome;

                    ddlAreaConcentracao.Items.Clear();
                    if (item.cursos.areas_concentracao.Count == 0)
                    {
                        divAreaConcentracao.Visible = false;
                    }
                    else if (item.cursos.areas_concentracao.Count == 1)
                    {
                        divAreaConcentracao.Visible = true;
                        ddlAreaConcentracao.DataSource = item.cursos.areas_concentracao;
                        ddlAreaConcentracao.DataValueField = "id_area_concentracao";
                        ddlAreaConcentracao.DataTextField = "nome";
                        ddlAreaConcentracao.DataBind();
                        ddlAreaConcentracao.SelectedValue = item.cursos.areas_concentracao.ElementAt(0).id_area_concentracao.ToString();
                    }
                    else
                    {
                        divAreaConcentracao.Visible = true;
                        ddlAreaConcentracao.DataSource = item.cursos.areas_concentracao;
                        ddlAreaConcentracao.DataValueField = "id_area_concentracao";
                        ddlAreaConcentracao.DataTextField = "nome";
                        ddlAreaConcentracao.DataBind();
                        ddlAreaConcentracao.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione uma área", ""));
                        ddlAreaConcentracao.SelectedValue = "";
                    }

                    GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                    List<Estado> listaEstado = aplicacaoGerais.ListaEstado();
                    ddlEstado.Items.Clear();
                    ddlEstado.DataSource = listaEstado;
                    ddlEstado.DataValueField = "Sigla";
                    ddlEstado.DataTextField = "Nome";
                    ddlEstado.DataBind();
                    ddlEstado.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Estado", ""));
                    ddlEstado.SelectedValue = "";

                    if (item.valor >0)
                    {
                        btnModalSegundaVia.Visible = true;
                    }
                    else
                    {
                        btnModalSegundaVia.Visible = false;
                    }

                    string qOrigem = (string)Session["origem"];
                    if (qOrigem != "phorte")
                    {
                        divPesquisa.Visible=true;
                    }
                    else
                    {
                        divPesquisa.Visible = false;
                    }

                    string sAux;

                    sAux = "<script async src=\"https://www.googletagmanager.com/gtag/js?id=UA-154434342-1\"></script>";
                    sAux = sAux + "<script>";
                    sAux = sAux + "window.dataLayer = window.dataLayer || [];";
                    sAux = sAux + "          function gtag(){ dataLayer.push(arguments); }";
                    sAux = sAux + "          gtag('js', new Date());";
                    sAux = sAux + "          gtag('config', 'UA-154434342-1', {";
                    sAux = sAux + "              'page_title': 'Página Inscricão:" + item.cursos.tipos_curso.tipo_curso + "',";
                    sAux = sAux + "    'page_path': '/inscricao.aspx?T=" + item.cursos.tipos_curso.tipo_curso.Substring(0,3) + "&C=" + item.cursos.sigla + "'";
                    sAux = sAux + "          });";
                    sAux = sAux + "</script> ";
                    litGoogle.Text = Server.HtmlDecode(sAux);

                }
                else
                {
                    Response.Redirect("homeInscricoes.aspx", true);
                }

            }
        }

        protected void btnEnviarInscricao_Click(object sender, EventArgs e)
        {
            string sAux = "";
            string sCel = "";

            if (ddlAreaConcentracao.Items.Count>1)
            {
                if (ddlAreaConcentracao.SelectedValue == "")
                {
                    sAux = sAux + "Selecionar uma área de concentração. <br/><br/>";
                }
            }
            if (txtCPFAluno.Value.Trim() == "" || txtCPFAluno.Value.Trim() == "..-")
            {
                txtCPFAluno.Value = "";
                sAux = sAux + "Preencher o CPF. <br/><br/>";
            }
            if (!Utilizades.fValidaCPF(txtCPFAluno.Value.Trim()))
            {
                //txtCPFAluno.Value = "";
                sAux = sAux + "O CPF digitado é Inválido. <br/><br/>";
            }
            if (txtNomeAluno.Value.Trim() == "")
            {
                sAux = sAux + "Preencher o Nome. <br/><br/>";
            }
            if (txtNascimentoAluno.Value.Trim() == "")
            {
                sAux = sAux + "Preencher a Data de Nascimento. <br/><br/>";
            }
            if (txtCEPAluno.Value.Trim() == "" || txtCEPAluno.Value.Trim() == "-")
            {
                sAux = sAux + "Preencher o CEP. <br/><br/>";
            }
            if (txtCEPAluno.Value.Length != 9)
            {
                sAux = sAux + "O CEP deve ter 8 posições. <br/><br/>";
            }
            if (txtNascimentoAluno.Value.Trim() == "")
            {
                sAux = sAux + "Preencher a Data de Nascimento. <br/><br/>";
            }
            DateTime temp;
            if (!DateTime.TryParse(txtNascimentoAluno.Value.Trim(), out temp))
            {
                sAux = sAux + "Digite uma Data de Nascimento válida. <br/><br/>";
            }
            if (txtEnderecoAluno.Value.Trim() == "")
            {
                sAux = sAux + "Preencher o Endereço. <br/><br/>";
            }
            if (txtEnderecoNumeroAluno.Value.Trim() == "")
            {
                sAux = sAux + "Preencher o Número. <br/><br/>";
            }
            if (txtBairroAluno.Value.Trim() == "")
            {
                sAux = sAux + "Preencher o bairro. <br/><br/>";
            }
            if (txtCidadeAluno.Value.Trim() == "")
            {
                sAux = sAux + "Preencher a Cidade. <br/><br/>";
            }
            if (ddlEstado.SelectedValue == "")
            {
                sAux = sAux + "Selecionar o Estado. <br/><br/>";
            }
            if (txtEmailAluno.Value.Trim() == "")
            {
                sAux = sAux + "Preencher o Email. <br/><br/>";
            }
            else if (!txtEmailAluno.Value.Trim().Contains("@"))
            {
                sAux = sAux + "Preencher um email válido. <br/><br/>";
            }
            sCel = txtCelular.Value.Trim().Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Replace(".", "");
            if (txtCelular.Value.Trim() == "" || txtCelular.Value.Trim() == "()-")
            {
                sAux = sAux + "Preencher o celular. <br/><br/>";
            }
            else if (txtCelular.Value.Trim().Length < 15)
            {
                sAux = sAux + "Preencher o celular com o formato (xx) xxxxx.xxxx <br/><br/>";
            }
            else if (sCel.Length < 11)
            {
                sAux = sAux + "Preencher o celular com o formato (xx) xxxxx.xxxx <br/><br/>";
            }
            if (txtRg.Value.Trim().Replace(".","") == "")
            {
                sAux = sAux + "Preencher o Rg. <br/><br/>";
            }

            InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
            periodo_inscricao_curso item = new periodo_inscricao_curso();
            item = (periodo_inscricao_curso)Session["periodo_inscricao_curso"];

            fichas_inscricao item_ficha = new fichas_inscricao();
            List<fichas_inscricao> lista_ficha = new List<fichas_inscricao>();
            item_ficha.id_periodo_inscricao = item.id_periodo;
            item_ficha.cpf = txtCPFAluno.Value.Trim().Replace(".", "").Replace("-", "");
            item_ficha.id_curso = item.id_curso;
            DateTime dt = new DateTime();
            lista_ficha = aplicacaoInscricao.ListaInscrisao(item_ficha, dt, "");

            if (sAux != "")
            {
                lblMensagem.Text = sAux;
                lblTituloMensagem.Text = "Atenção";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                return;
            }

            if (lista_ficha.Count != 0)
            {
                lblTituloMensagem.Text = "Atenção";
                if (item.valor > 0)
                {
                    lblMensagem.Text = "Já existe uma inscrição desse CPF para esse curso nesse período.<br/><br/>Portanto, clique no botão \"Segunda Via Boleto\" e informe o CPF para a emissão do boleto.";
                }
                else
                {
                    lblMensagem.Text = "Já existe uma inscrição desse CPF para esse curso nesse período.<br/><br/>Portanto, não é possível realizar outra inscrição.";
                }

                //lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                return;
            }

            historico_inscricao item_ficha_historico = new historico_inscricao();

            if (divAreaConcentracao.Visible)
            {
                item_ficha.id_area_concentracao = Convert.ToInt32(ddlAreaConcentracao.SelectedValue);
            }
            item_ficha.cpf = txtCPFAluno.Value.Trim().Replace(".","").Replace("-","");
            item_ficha.nome = txtNomeAluno.Value.Trim().Replace("'", "");
            item_ficha.data_nascimento = Convert.ToDateTime(txtNascimentoAluno.Value.Trim());
            item_ficha.sexo = ddlSexoAluno.SelectedValue;
            item_ficha.cep_res = txtCEPAluno.Value.Trim().Replace("-", "");
            item_ficha.endereco_res = txtEnderecoAluno.Value.Trim().Replace("'", "");
            item_ficha.numero_res = txtEnderecoNumeroAluno.Value.Trim().Replace("'", "");
            item_ficha.complemento_res = txtEnderecoComplementoAluno.Value.Trim().Replace("'", "");
            item_ficha.bairro_res = txtBairroAluno.Value.Trim().Replace("'", "");
            item_ficha.cidade_res = txtCidadeAluno.Value.Trim().Replace("'", "");
            item_ficha.estado_res = ddlEstado.SelectedValue;
            if (txtTelefone.Value.Trim() !="")
            {
                item_ficha.telefone_res = txtTelefone.Value.Trim().Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
            }
            else
            {
                item_ficha.telefone_res = "";
            }
            item_ficha.celular_res = txtCelular.Value.Trim().Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
            sAux = txtRg.Value.Trim().Replace(".", "");

            item_ficha.rg_rne = (sAux.Length <= 15 ? sAux : sAux.Substring(0,15));
            if (txtDigitoRg.Value.Trim() !="")
            {
                item_ficha.digito_rg = txtDigitoRg .Value.Trim();
            }
            item_ficha.estrangeiro = "Não"; //Fixo para compatibilidade com o Serpi

            item_ficha.email_res = txtEmailAluno.Value.Trim().Replace("'", "");
            item_ficha.natural_de = "";
            item_ficha.nacionalidade = " ";
            //item_ficha.rg_rne = "";
            item_ficha.data_expedicao = DateTime.Now;
            item_ficha.uf_rg = "";
            item_ficha.instituicao = "";
            item_ficha.ano_formacao = "";
            item_ficha.formacao = "";
            item_ficha.data_inscricao = DateTime.Now;

            string qOrigem = (string)Session["origem"];
            if (qOrigem != "phorte")
            {
                if (optIndEmpresa.Checked)
                {
                    item_ficha.pesquisamala = optIndEmpresa.Value;
                }
                else if (optIndProfessor.Checked)
                {
                    item_ficha.pesquisamala = optIndProfessor.Value;
                }
                else if (optIndAluno.Checked)
                {
                    item_ficha.pesquisamala = optIndAluno.Value;
                }
                else if (optIndExAluno.Checked)
                {
                    item_ficha.pesquisamala = optIndExAluno.Value;
                }
                else if (optBuscaINternet.Checked)
                {
                    item_ficha.pesquisamala = "Internet - SITE DE PESQUISA";// optBuscaINternet.Value;
                }
                else if (optFacebook.Checked)
                {
                    item_ficha.pesquisamala = optFacebook.Value;
                }
                else if (optTwitter.Checked)
                {
                    item_ficha.pesquisamala = optTwitter.Value;
                }
                else if (optLinkedin.Checked)
                {
                    item_ficha.pesquisamala = "Internet - SITE DE PESQUISA";//optLinkedin.Value;
                }
                else if (optOutros.Checked)
                {
                    item_ficha.pesquisamala = optOutros.Value;
                }

                item_ficha.pesquisaoutros = txtOutros.Value.Trim();
            }
            else
            {
                item_ficha.pesquisaoutros = "PHORTE";
                item_ficha.pesquisamala = "Outros";
            }


            item_ficha = aplicacaoInscricao.CriarInscricao(item_ficha);

            item_ficha_historico.id_inscricao = item_ficha.id_inscricao;
            item_ficha_historico.data = DateTime.Now;
            item_ficha_historico.status = "Inscrição não Paga";
            if (qOrigem != "phorte")
            {
                item_ficha_historico.usuario = "web";
            }
            else
            {
                item_ficha_historico.usuario = "PHORTE";
            }

            item_ficha_historico = aplicacaoInscricao.CriarHistorico(item_ficha_historico);

            boletos item_boleto = new boletos();
            if (item.valor > 0)
            {
                item_boleto.id_conv = "316753";//303250
                item_boleto.refTran = "";
                item_boleto.valor = item.valor.ToString().Replace(",", "");
                if ((item.periodo_inscricao.data_fim.Value - DateTime.Today).Days > 2)
                {
                    item_boleto.data_vencimento = item.periodo_inscricao.data_fim.Value.AddDays(-2);
                }
                else
                {
                    item_boleto.data_vencimento = DateTime.Today;
                }

                //item_boleto.data_vencimento = DateTime.Today.AddDays(7);
                item_boleto.cpf = item_ficha.cpf;
                item_boleto.nome = item_ficha.nome;
                if (item_ficha.nome.Length > 60)
                {
                    item_boleto.nome = item_ficha.nome.Substring(0, 60);
                }

                string sEnderecoTemp = item_ficha.endereco_res + ", " + item_ficha.numero_res + " " + item_ficha.complemento_res;
                if (sEnderecoTemp.Length > 60)
                {
                    sEnderecoTemp = sEnderecoTemp.Substring(0, 60);
                }
                item_boleto.endereco = sEnderecoTemp;
                item_boleto.cidade = item_ficha.cidade_res;
                if (item_ficha.cidade_res.Length > 18)
                {
                    item_boleto.cidade = item_ficha.cidade_res.Substring(0, 18);
                }
                item_boleto.uf = item_ficha.estado_res;
                item_boleto.cep = item_ficha.cep_res;
                if (item.cursos.id_tipo_curso == 1)
                {
                    item_boleto.msgLoja = "PAGAMENTO REFERENTE A INSCRICAO PARA PROCESSO SELETIVO QUE OCORRERA EM: " + String.Format("{0:dd/MM/yyyy}", item.periodo_inscricao.data_prova) + "(" + item.cursos.nome.ToUpper() + ") Local da Prova: Instituto de Pesquisas Tecnologicas do Estado de Sao Paulo-IPT  Av. Prof. Almeida Prado no 532 - Butanta - Sao Paulo - SP. Predio 56  - Boleto valido/pagavel ate 29 dias apos a data de vencimento - Informacoes adicionais: (11) 3767-4226 / 4058 - cursos@ipt.br";
                }
                else
                {
                    item_boleto.msgLoja = "PAGAMENTO REFERENTE A INSCRICAO PARA O CURSO DE (" + item.cursos.nome.ToUpper() + ")  - Boleto valido/pagavel ate 29 dias apos a data de vencimento - Informacoes adicionais: (11) 3767-4226 / 4058 - cursos@ipt.br";
                }
                item_boleto.status = "Cadastrado";
                item_boleto.data_cadastro = DateTime.Now;
                item_boleto.data_alteracao = DateTime.Now;
                if (qOrigem != "phorte")
                {
                    item_boleto.usuario = "web";
                }
                else
                {
                    item_boleto.usuario = "PHORTE";
                }
                //item_boleto.usuario = "web";

                item_boleto.fichas_inscricao = null;

                string sNossoNumero = aplicacaoInscricao.Busca_Ultimo_refTran();
                item_boleto.refTran = sNossoNumero;
                item_boleto = aplicacaoInscricao.CriarBoleto(item_boleto, item_ficha);

                refTran item_refTran = new refTran();
                item_refTran.id_refTran = sNossoNumero;
                item_refTran.DataGetGemini = DateTime.Now;
                if (qOrigem != "phorte")
                {
                    item_refTran.Solicitante = "Sapiens";
                }
                else
                {
                    item_refTran.Solicitante = "PHORTE";
                }
                //item_refTran.Solicitante = "Sapiens";
                item_refTran.DataUtilizacao = item_refTran.DataGetGemini;
                item_refTran.id_boleto = item_boleto.id_boleto;
                aplicacaoInscricao.Criar_refTran(item_refTran);

                //item_boleto = aplicacaoInscricao.AlterarBoleto(item_boleto);

                aplicacaoInscricao.Criar_inscricao_boleto(item_boleto, item_ficha);

                int iSexo;
                if (ddlSexoAluno.SelectedValue.ToUpper() == "M")
                {
                    iSexo = 0;
                }
                else
                {
                    iSexo = 1;
                }

                aplicacaoInscricao.Insere_Gemini(txtCPFAluno.Value, item_boleto.nome, iSexo, item_ficha.data_nascimento, txtCEPAluno.Value, item_ficha.endereco_res, item_ficha.numero_res, item_ficha.complemento_res, item_ficha.bairro_res, item_ficha.cidade_res, item_ficha.estado_res, item_ficha.email_res.Trim(), item.valor.ToString(), item_boleto.refTran, item_ficha.cursos.id_tipo_curso, item_ficha.id_curso, item_ficha.cursos.nome, item_boleto.data_vencimento);

                Session["boletos"] = item_boleto;
                btnEnviarInscricao.Visible = false;
            }

            GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
            Configuracoes item_configuracoes;
            // 1 = email mestrado@ipt.br
            // 2 = email suporte@ipt.br
            item_configuracoes = aplicacaoGerais.BuscaConfiguracoes(1);

            string sFrom = item_configuracoes.remetente_email;
            string sFrom_Nome = item_configuracoes.nome_remetente_email;
            string sTo = item_ficha.email_res.Trim();
            string sAssunto = "Processo Seletivo IPT - " + item.periodo_inscricao.quadrimestre;
            string sCorpo;

            StreamReader objReader;
            if (item.valor > 0)
            {
                objReader = new StreamReader(Server.MapPath("~/Templates/emails") + "/ConfirmacaoInscricao.html");
            }
            else
            {
                objReader = new StreamReader(Server.MapPath("~/Templates/emails") + "/ConfirmacaoInscricaoSemTaxa.html");
            }
            sCorpo = objReader.ReadToEnd();
            objReader.Close();

            sCorpo = sCorpo.Replace("{inscricao_numero}", item_ficha.id_inscricao + "/" + item.periodo_inscricao.quadrimestre);
            sCorpo = sCorpo.Replace("{curso}", txtNomeCurso.Value);
            if (ddlAreaConcentracao.SelectedItem != null)
            {
                sCorpo = sCorpo.Replace("{area_concentracao}", ddlAreaConcentracao.SelectedItem.Text);
            }
            else
            {
                sCorpo = sCorpo.Replace("{area_concentracao}", "");
            }
            sCorpo = sCorpo.Replace("{nome}", txtNomeAluno.Value.Trim());
            sCorpo = sCorpo.Replace("{data_inscricao}", String.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now));

            if (item.valor > 0)
            {
                sCorpo = sCorpo.Replace("{valor_inscricao}", item.valor.ToString());
                sCorpo = sCorpo.Replace("{vencimento_boleto}", String.Format("{0:dd/MM/yyyy HH:mm}", item_boleto.data_vencimento));
            }

            if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString != "Producao")
            {
                sAssunto = sAssunto + "<br><br> <strong>Esse email seria enviado para:</strong>" + sTo;
                sTo = "kelsey@ipt.br"; // usuario.email;
            }

            Utilizades.fEnviaEmail(sFrom, sFrom_Nome, sTo, "", "", sAssunto, sCorpo, item_configuracoes.servidor_email, item_configuracoes.conta_email, item_configuracoes.senha_email, item_configuracoes.porta_email.Value, 1, "");

            if (item.valor > 0)
            {
                if (item.periodo_inscricao.data_prova != null)
                {
                    divProvaComTaxa.Visible = true;
                    lblDiaProvaComTaxa.Text = String.Format("{0:dd/MM/yyyy}", item.periodo_inscricao.data_prova);
                }
                else
                {
                    divProvaComTaxa.Visible = false;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fAbreModalConfirmacaoInscricao();", true);
            }
            else
            {
                if (item.periodo_inscricao.data_prova != null)
                {
                    divProvaSemTaxa.Visible = true;
                    lblDiaProvaSemTaxa.Text = String.Format("{0:dd/MM/yyyy}", item.periodo_inscricao.data_prova);
                }
                else
                {
                    divProvaSemTaxa.Visible = false;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fAbreModalConfirmacaoInscricaoSemTaxa();", true);
            }
            
            //Response.Redirect("boletoBB.aspx", true);

        }

        protected void btnSegundaVia_Click(object sender, EventArgs e)
        {
            string sAux = "";

            if (txtCPFSegundaVia.Value.Trim() == "" || txtCPFSegundaVia.Value.Trim() == "..-")
            {
                txtCPFSegundaVia.Value = "";
                sAux = sAux + "Informe o CPF. <br/><br/>";
            }

            if (sAux != "")
            {
                lblMensagem.Text = sAux;
                lblTituloMensagem.Text = "Atenção";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                return;
            }

            InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
            periodo_inscricao_curso item = new periodo_inscricao_curso();
            item = (periodo_inscricao_curso)Session["periodo_inscricao_curso"];

            fichas_inscricao item_ficha = new fichas_inscricao();
            List<fichas_inscricao> lista_ficha = new List<fichas_inscricao>();


            item_ficha.id_periodo_inscricao = item.id_periodo;
            item_ficha.cpf = txtCPFSegundaVia.Value.Trim().Replace(".","").Replace("-","");
            item_ficha.id_curso = item.id_curso;

            DateTime dt = new DateTime();
            lista_ficha = aplicacaoInscricao.ListaInscrisao(item_ficha, dt, "");

            if (lista_ficha.Count == 0)
            {
                lblTituloMensagem.Text = "Atenção";
                lblMensagem.Text = "Não foi encontrada inscrição desse CPF para esse curso nesse período.";
                //lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                return;
            }

            int iSexo;
            if (ddlSexoAluno.SelectedValue.ToUpper() == "M")
            {
                iSexo = 0;
            }
            else
            {
                iSexo = 1;
            }

            //boletos item_boleto = new boletos();

            if (lista_ficha.ElementAt(0).boletos.Count == 0)
            {
                //Não encontrou Boleto, então irá gerar um boleto para essa inscrição
                item_ficha = lista_ficha.ElementAt(0);
                boletos item_boleto = new boletos();
                if (item.valor > 0)
                {
                    item_boleto.id_conv = "316753";//303250
                    item_boleto.refTran = "";
                    item_boleto.valor = item.valor.ToString().Replace(",", "");
                    if ((item.periodo_inscricao.data_fim.Value - DateTime.Today).Days > 2)
                    {
                        item_boleto.data_vencimento = item.periodo_inscricao.data_fim.Value.AddDays(-2);
                    }
                    else
                    {
                        item_boleto.data_vencimento = DateTime.Today;
                    }

                    //item_boleto.data_vencimento = DateTime.Today.AddDays(7);
                    item_boleto.cpf = item_ficha.cpf;
                    item_boleto.nome = item_ficha.nome;
                    if (item_ficha.nome.Length > 60)
                    {
                        item_boleto.nome = item_ficha.nome.Substring(0, 60);
                    }

                    string sEnderecoTemp = item_ficha.endereco_res + ", " + item_ficha.numero_res + " " + item_ficha.complemento_res;
                    if (sEnderecoTemp.Length > 60)
                    {
                        sEnderecoTemp = sEnderecoTemp.Substring(0, 60);
                    }
                    item_boleto.endereco = sEnderecoTemp;
                    item_boleto.cidade = item_ficha.cidade_res;
                    if (item_ficha.cidade_res.Length > 18)
                    {
                        item_boleto.cidade = item_ficha.cidade_res.Substring(0, 18);
                    }
                    item_boleto.uf = item_ficha.estado_res;
                    item_boleto.cep = item_ficha.cep_res;
                    if (item.cursos.id_tipo_curso == 1)
                    {
                        item_boleto.msgLoja = "PAGAMENTO REFERENTE A INSCRICAO PARA PROCESSO SELETIVO QUE OCORRERA EM: " + String.Format("{0:dd/MM/yyyy}", item.periodo_inscricao.data_prova) + "(" + item.cursos.nome.ToUpper() + ") Local da Prova: Instituto de Pesquisas Tecnologicas do Estado de Sao Paulo-IPT  Av. Prof. Almeida Prado no 532 - Butanta - Sao Paulo - SP. Predio 56 - Boleto valido/pagavel ate 29 dias apos a data de vencimento - Informacoes adicionais: (11) 3767-4226 / 4058 - cursos@ipt.br";
                    }
                    else
                    {
                        item_boleto.msgLoja = "PAGAMENTO REFERENTE A INSCRICAO PARA O CURSO DE (" + item.cursos.nome.ToUpper() + ")  - Boleto valido/pagavel ate 29 dias apos a data de vencimento - Informacoes adicionais: (11) 3767-4226 / 4058 - cursos@ipt.br";
                    }
                    item_boleto.status = "Cadastrado";
                    item_boleto.data_cadastro = DateTime.Now;
                    item_boleto.data_alteracao = DateTime.Now;
                    item_boleto.usuario = "web";
                    //item_boleto.usuario = "web";

                    item_boleto.fichas_inscricao = null;

                    string sNossoNumero = aplicacaoInscricao.Busca_Ultimo_refTran();
                    item_boleto.refTran = sNossoNumero;
                    item_boleto = aplicacaoInscricao.CriarBoleto(item_boleto, item_ficha);

                    refTran item_refTran = new refTran();
                    item_refTran.id_refTran = item_boleto.refTran;
                    item_refTran.DataGetGemini = DateTime.Now;
                    item_refTran.Solicitante = "Sapiens";
                    //item_refTran.Solicitante = "Sapiens";
                    item_refTran.DataUtilizacao = item_refTran.DataGetGemini;
                    item_refTran.id_boleto = item_boleto.id_boleto;
                    aplicacaoInscricao.Criar_refTran(item_refTran);

                    //item_boleto = aplicacaoInscricao.AlterarBoleto(item_boleto);

                    aplicacaoInscricao.Criar_inscricao_boleto(item_boleto, item_ficha);

                    aplicacaoInscricao.Insere_Gemini(txtCPFAluno.Value, item_boleto.nome, iSexo, item_ficha.data_nascimento, txtCEPAluno.Value, item_ficha.endereco_res, item_ficha.numero_res, item_ficha.complemento_res, item_ficha.bairro_res, item_ficha.cidade_res, item_ficha.estado_res, item_ficha.email_res.Trim(), item.valor.ToString(), item_boleto.refTran, item_ficha.cursos.tipos_curso.id_tipo_curso, item_ficha.id_curso, item_ficha.cursos.nome, item_boleto.data_vencimento);

                    Session["boletos"] = item_boleto;

                    GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                    Configuracoes item_configuracoes;
                    // 1 = email mestrado@ipt.br
                    // 2 = email suporte@ipt.br
                    item_configuracoes = aplicacaoGerais.BuscaConfiguracoes(1);

                    string sFrom = item_configuracoes.remetente_email;
                    string sFrom_Nome = item_configuracoes.nome_remetente_email;
                    string sTo = item_ficha.email_res.Trim();
                    string sAssunto = "Processo Seletivo IPT - " + item.periodo_inscricao.quadrimestre;
                    string sCorpo;

                    StreamReader objReader;
                    if (item.valor > 0)
                    {
                        objReader = new StreamReader(Server.MapPath("~/Templates/emails") + "/ConfirmacaoInscricao.html");
                    }
                    else
                    {
                        objReader = new StreamReader(Server.MapPath("~/Templates/emails") + "/ConfirmacaoInscricaoSemTaxa.html");
                    }
                    sCorpo = objReader.ReadToEnd();
                    objReader.Close();

                    sCorpo = sCorpo.Replace("{inscricao_numero}", item_ficha.id_inscricao + "/" + item.periodo_inscricao.quadrimestre);
                    sCorpo = sCorpo.Replace("{curso}", txtNomeCurso.Value);
                    if (ddlAreaConcentracao.SelectedItem != null)
                    {
                        sCorpo = sCorpo.Replace("{area_concentracao}", ddlAreaConcentracao.SelectedItem.Text);
                    }
                    else
                    {
                        sCorpo = sCorpo.Replace("{area_concentracao}", "");
                    }
                    sCorpo = sCorpo.Replace("{nome}", txtNomeAluno.Value.Trim());
                    sCorpo = sCorpo.Replace("{data_inscricao}", String.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now));

                    if (item.valor > 0)
                    {
                        sCorpo = sCorpo.Replace("{valor_inscricao}", item.valor.ToString());
                        sCorpo = sCorpo.Replace("{vencimento_boleto}", String.Format("{0:dd/MM/yyyy HH:mm}", item_boleto.data_vencimento));
                    }

                    if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString != "Producao")
                    {
                        sAssunto = sAssunto + "<br><br> <strong>Esse email seria enviado para:</strong>" + sTo;
                        sTo = "kelsey@ipt.br"; // usuario.email;
                    }

                    Utilizades.fEnviaEmail(sFrom, sFrom_Nome, sTo, "", "", sAssunto, sCorpo, item_configuracoes.servidor_email, item_configuracoes.conta_email, item_configuracoes.senha_email, item_configuracoes.porta_email.Value, 1, "");

                    if (item.valor > 0)
                    {
                        if (item.periodo_inscricao.data_prova != null)
                        {
                            divProvaComTaxa.Visible = true;
                            lblDiaProvaComTaxa.Text = String.Format("{0:dd/MM/yyyy}", item.periodo_inscricao.data_prova);
                        }
                        else
                        {
                            divProvaComTaxa.Visible = false;
                        }
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fAbreModalConfirmacaoInscricao();", true);
                    }
                    else
                    {
                        if (item.periodo_inscricao.data_prova != null)
                        {
                            divProvaSemTaxa.Visible = true;
                            lblDiaProvaSemTaxa.Text = String.Format("{0:dd/MM/yyyy}", item.periodo_inscricao.data_prova);
                        }
                        else
                        {
                            divProvaSemTaxa.Visible = false;
                        }
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fAbreModalConfirmacaoInscricaoSemTaxa();", true);
                    }

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fAbreModalConfirmacaoInscricaoSemTaxa();", true);
                }
            }
            else
            {
                var retorno = aplicacaoInscricao.VarificaBoletoPago_Gemini(lista_ficha.ElementAt(0).boletos.ElementAt(0).refTran);
                if (retorno.Item1 == "Pago")
                {
                    lblTituloMensagem.Text = "Atenção";
                    lblMensagem.Text = "O boleto nº: <strong>" + lista_ficha.ElementAt(0).boletos.ElementAt(0).refTran + "</strong> referente a essa inscrição foi pago no dia <strong>" + String.Format("{0:dd/MM/yyyy}", retorno.Item2) + "</strong>";
                    //lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
                    return;
                }
                else if (retorno.Item1 == "Cancelado")
                {
                    lblTituloMensagem.Text = "Atenção";
                    lblMensagem.Text = "O boleto nº: <strong>" + lista_ficha.ElementAt(0).boletos.ElementAt(0).refTran + "</strong> referente a essa inscrição foi CANCELADO no dia <strong>" + String.Format("{0:dd/MM/yyyy}", retorno.Item2) + "</strong> <br> <br> Envie um email para <strong>mestrado@ipt.br</strong> informando o ocorrido.";
                    //lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                    return;
                }
                else
                {
                    if (retorno.Item1 == "SemRegistro")
                    {
                        if (lista_ficha.ElementAt(0).sexo == "M")
                        {
                            iSexo = 0;
                        }
                        else
                        {
                            iSexo = 1;
                        }

                        aplicacaoInscricao.Insere_Gemini(Convert.ToUInt64(lista_ficha.ElementAt(0).boletos.ElementAt(0).cpf).ToString(@"000\.000\.000\-00"), lista_ficha.ElementAt(0).boletos.ElementAt(0).nome, iSexo, lista_ficha.ElementAt(0).data_nascimento, Convert.ToUInt64(lista_ficha.ElementAt(0).cep_res).ToString(@"00000\-000"), lista_ficha.ElementAt(0).endereco_res, lista_ficha.ElementAt(0).numero_res.Replace("'",""), lista_ficha.ElementAt(0).complemento_res, lista_ficha.ElementAt(0).bairro_res, lista_ficha.ElementAt(0).cidade_res, lista_ficha.ElementAt(0).estado_res, lista_ficha.ElementAt(0).email_res.Trim(), item.valor.ToString(), lista_ficha.ElementAt(0).boletos.ElementAt(0).refTran, lista_ficha.ElementAt(0).cursos.tipos_curso.id_tipo_curso, lista_ficha.ElementAt(0).id_curso, lista_ficha.ElementAt(0).cursos.nome, lista_ficha.ElementAt(0).boletos.ElementAt(0).data_vencimento);
                    }
                    Session["boletos"] = lista_ficha.ElementAt(0).boletos.ElementAt(0);
                    Response.Redirect("boletoBB.aspx", true);
                }

            }

        }
    }
}