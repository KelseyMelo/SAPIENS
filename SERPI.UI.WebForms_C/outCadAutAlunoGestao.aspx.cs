
using Aplicacao_C;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using SERPI.Dominio_C;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

namespace SERPI.UI.WebForms_C
{
    public partial class outCadAutAlunoGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 78)) // 78. Diversos - Cadastro Automático de Alunos - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                if (Session["sNewalunos_cadastro_automatico"] != null && (Boolean)Session["sNewalunos_cadastro_automatico"] != true)
                {
                    alunos_cadastro_automatico item;
                    item = (alunos_cadastro_automatico)Session["alunos_cadastro_automatico"];
                    lblTituloPagina.Text = "(Editar) - N.º " + item.id_cadastro_automatico;

                    //txtDescricao.Value = Convert.ToDateTime(item.data_evento).ToString("yyyy-MM-dd");
                    //txtNumeroSequencial.Value = item.numero_seq_inicial.ToString();
                    txtDescricao.Value = item.descricao;
                    txtDescricao_curso.Value = item.descricao_curso;
                    
                    divLog.Visible = true;
                    //txtDataCadastro.Value = Convert.ToDateTime(item.data_cadastro).ToString("dd/MM/yyyy");
                    //txtDataAlteracao.Value = Convert.ToDateTime(item.data_alteracao).ToString("dd/MM/yyyy");
                    //txtResponsavel.Value = item.usuario;

                    txtAssuntoEmail.Value = "Cadastro Plataforma SAPIENS (" + item.descricao_curso + ")";

                    divTurma.Visible = true;

                    if (Session["AdiciondoSucesso"] != null)
                    {
                        if ((Boolean)Session["AdiciondoSucesso"])
                        {
                            Session["AdiciondoSucesso"] = null;
                            lblMensagem.Text = "Novo Lote de Importação adicionado com sucesso";
                            lblTituloMensagem.Text = "Novo Lote de Cadastro";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
                        }
                    }

                    StreamReader objReader;

                    objReader = new StreamReader(HttpRuntime.AppDomainAppPath + "\\Templates\\emails\\CadastroAutomaticoAluno.html");

                    string sCorpo = objReader.ReadToEnd();
                    objReader.Close();

                    byte[] imageArray = System.IO.File.ReadAllBytes(Server.MapPath("~/img/pessoas/" + usuario.avatar));
                    string ext = Path.GetExtension(Server.MapPath("~/img/pessoas/" + usuario.avatar));
                    string base64ImageRepresentation = "data:image/" + ext.Replace(".", "") + ";base64," + Convert.ToBase64String(imageArray);
                    //txtCorpoEmail.Value = txtCorpoEmail.Value;

                    //sCorpo = sCorpo.Replace("{imagem_usuario}", base64ImageRepresentation);

                    sCorpo = sCorpo.Replace("{curso}", item.descricao_curso);

                    //sCorpo = sCorpo.Replace("{evento}", item.descricao);

                    //sCorpo = sCorpo.Replace("{data_limite}", DateTime.Today.AddMonths(2).ToString("dd/MM/yyyy"));

                    txtCorpoEmail.Value = sCorpo;

                }
                else
                {
                    lblTituloPagina.Text = "(novo)";
                    txtDescricao.Value = "";
                    txtDescricao_curso.Value = "";

                    divTurma.Visible = false;
                    divLog.Visible = false;
                }
            }

        }

        protected void btnCriarCadAutAlunosAlunos_Click(object sender, EventArgs e)
        {
            Session["sNewalunos_cadastro_automatico"] = true;
            Session["alunos_cadastro_automatico"] = null;
            Session["AdiciondoSucesso"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnGerarMatriculaAlunos_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            alunos_cadastro_automatico item;
            item = (alunos_cadastro_automatico)Session["alunos_cadastro_automatico"];
            CadastroAutomaticoAlunoAplicacao aplicacaoCadAutAlunos = new CadastroAutomaticoAlunoAplicacao();

            AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();

            alunos itemAluno = new alunos();

            foreach (var elemento in item.alunos_cadastro_automatico_det)
            {

                if (elemento.idaluno == null)
                {
                    itemAluno.cpf = elemento.cpf;

                    itemAluno = aplicacaoAluno.BuscaItem(itemAluno.cpf);

                    if (itemAluno == null)
                    {
                        itemAluno = new alunos();
                        itemAluno.usuario = usuario.usuario;
                        itemAluno.data_cadastro = DateTime.Now;
                        itemAluno.status = "cadastrado";
                        itemAluno.nome = elemento.nome;
                        itemAluno.cpf = elemento.cpf;
                        itemAluno.email = elemento.email;
                        itemAluno = aplicacaoAluno.CriarItem(itemAluno);
                    }

                    SHA1CryptoServiceProvider objSHA1 = new SHA1CryptoServiceProvider();
                    ASCIIEncoding objEncoding = new ASCIIEncoding();
                    usuarios usuarioAluno = new usuarios();
                    UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();

                    usuarioAluno.usuario = itemAluno.idaluno.ToString();

                    usuarioAluno = aplicacaoUsuario.BuscaUsuario(usuarioAluno);

                    if (usuarioAluno == null)
                    {
                        usuarioAluno = new usuarios();
                        usuarioAluno.usuario = Convert.ToString(itemAluno.idaluno);
                        usuarioAluno.nome = itemAluno.nome;
                        usuarioAluno.un = "Acadêmico";
                        usuarioAluno.email = itemAluno.email;
                        usuarioAluno.id_grupo_acesso = 2;
                        usuarioAluno.status = 1;
                        usuarioAluno.avatar = "";
                        usuarioAluno.nomeSocial = itemAluno.nome.Substring(0, itemAluno.nome.IndexOf(" "));
                        string sAuxSenha;
                        sAuxSenha = itemAluno.cpf.Replace(".", "").Substring(0, 6);
                        usuarioAluno.senha = Convert.ToBase64String(objSHA1.ComputeHash(objEncoding.GetBytes(sAuxSenha)));

                        aplicacaoUsuario.CriarUsuario(usuarioAluno);
                    }

                    elemento.idaluno = itemAluno.idaluno;
                    aplicacaoCadAutAlunos.AlterarItem_participante(elemento);
                }
                
            }

            //item = aplicacaoCadAutAlunos.BuscaItem(item);
            Session["alunos_cadastro_automatico"] = item;

            lblMensagem.Text = "Matrículas geradas com sucesso!";
            lblTituloMensagem.Text = "Geração de Matrículas";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
        }

        protected void btnImportarAlunos_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            alunos_cadastro_automatico item;
            item = (alunos_cadastro_automatico)Session["alunos_cadastro_automatico"];
            CadastroAutomaticoAlunoAplicacao aplicacaoCadAutAlunos = new CadastroAutomaticoAlunoAplicacao();
            string sArquivo;

            foreach (var elemento in item.alunos_cadastro_automatico_det)
            {
                elemento.alunos_cadastro_automatico = item;
                
                aplicacaoCadAutAlunos.AlterarItem_participante(elemento);
            }

            GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
            Configuracoes item_configuracoes;
            // 1 = email mestrado@ipt.br
            // 2 = email suporte@ipt.br
            item_configuracoes = aplicacaoGerais.BuscaConfiguracoes(2);

            string sFrom = item_configuracoes.remetente_email;
            string sFrom_Nome = item_configuracoes.nome_remetente_email;
            string sTo;
            string sAssunto = "Gerado Cadastro Aulotático de Alunos para o Lote: " + item.descricao;
            string sAux = "";

            UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
            usuarios item_usuario = new usuarios();
            item_usuario.grupos_acesso = new grupos_acesso();
            item_usuario.grupos_acesso.id_grupo = 8; //Gerência	Depto Gerência - Mestrado
            List<usuarios> lista_usuarios = aplicacaoUsuario.ListaUsuario_porGrupoAcesso(item_usuario);

            foreach (var elemento in lista_usuarios)
            {
                sAux = "Caro " + elemento.nome;

                sAux = sAux + "<br><br>Foram criados <b>" + item.alunos_cadastro_automatico_det.Count().ToString() + "</b> alunos_cadastro_automatico para o Lote: <b>" + item.descricao + "</b>";

                sAux = sAux + "<br> <br>Criados por: <b>" + usuario.nome + "</b>";
                sAux = sAux + "<br> <br>Data/hora criação: <b>" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "</b>";


                if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString == "Producao")
                {
                    sTo = elemento.email;
                }
                else
                {
                    sTo = "kelsey@ipt.br"; // usuario.email;
                    sAux = sAux + "<br><br> <strong>Esse email seria enviado para:</strong>" + elemento.email;
                }

                //sTo = "kelsey@ipt.br";
                Utilizades.fEnviaEmail(sFrom, sFrom_Nome, sTo, "", "", sAssunto, sAux, item_configuracoes.servidor_email, item_configuracoes.conta_email, item_configuracoes.senha_email, item_configuracoes.porta_email.Value, 1, "");

            }



            item = aplicacaoCadAutAlunos.BuscaItem(item);
            Session["alunos_cadastro_automatico"] = item;

            lblMensagem.Text = "alunos_cadastro_automatico gerados com sucesso!";
            lblTituloMensagem.Text = "alunos_cadastro_automatico Gerados";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');fAncora();", true);

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("outCadAutAluno.aspx", true);
        }

        protected void btnSalvarCadAutAlunosAlunos_Click(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";


                if (txtDescricao.Value.Trim() == "")
                {
                    sAux = sAux + "Digite uma Descrição para esse Lote. <br/><br/>";
                }

                if (txtDescricao_curso.Value.Trim() == "")
                {
                    sAux = sAux + "Digite o Curso Alvo. <br/><br/>";
                }

                if (sAux != "")
                {
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                    return;
                }

                CadastroAutomaticoAlunoAplicacao aplicacaoCadAutAlunos = new CadastroAutomaticoAlunoAplicacao();

                if (Session["sNewalunos_cadastro_automatico"] != null && (Boolean)Session["sNewalunos_cadastro_automatico"] != true)
                {
                    //Alteração
                    alunos_cadastro_automatico item = new alunos_cadastro_automatico();

                    item = (alunos_cadastro_automatico)Session["alunos_cadastro_automatico"];

                    //item.data_evento = Convert.ToDateTime(txtDataEvento.Value);
                    //item.numero_seq_inicial = Convert.ToInt32(txtNumeroSequencial.Value);
                    //item.ano = Convert.ToInt32(txtAnoReferencia.Value);

                    item.descricao = txtDescricao.Value.Trim();
                    item.descricao_curso = txtDescricao_curso.Value.Trim();
                    item.data_importacao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    aplicacaoCadAutAlunos.AlterarItem(item);

                    lblMensagem.Text = "Lote de Cadastro Automático de Alunos alterado com sucesso.";
                    lblTituloMensagem.Text = "Alteração de Lote de Cadastro Automático de Aluno";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');fAtiva_grdDisciplina();", true);

                    Session["alunos_cadastro_automatico"] = item;

                }
                else
                {
                    //Inclusão

                    alunos_cadastro_automatico item = new alunos_cadastro_automatico();

                    item.data_importacao = DateTime.Today;
                    item.descricao_curso = txtDescricao_curso.Value.Trim();
                    item.descricao = txtDescricao.Value;
                    item.usuario = usuario.usuario;

                    item = aplicacaoCadAutAlunos.CriarItem(item);

                    if (item != null)
                    {
                        Session["alunos_cadastro_automatico"] = item;
                        Session.Add("sNewalunos_cadastro_automatico", false);
                        Session["AdiciondoSucesso"] = true;
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na manutenção do Certificado. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Certificado";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }

    }
}