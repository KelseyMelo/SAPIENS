using System;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects.SqlClient;
//using SERPI.VB;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Security.Cryptography;
using System.Web.Security;
using System.Text;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace SERPI.UI.WebForms_C
{
    public partial class cadAlunoGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)

        {
            string qTab = HttpContext.Current.Request.Form["hQTab"];
            if (qTab == null)
            {
                Response.Redirect("cadAluno.aspx", true);
                return;
            }
            
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 6)) //1. Alunos - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();

            if (!Page.IsPostBack)
            {
                litMenu.Text = "<input type=\"hidden\" id =\"hGrupoMenu\"  name=\"hGrupoMenu\" value=\"liAcademico\" /> <input type=\"hidden\" id =\"hItemMenu\"  name=\"hGrupoMenu\" value=\"li1Alunos\" />";

                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                List<Pais> listaPais = aplicacaoGerais.ListaPais();
                //var listaPais2 = from item2 in listaPais
                //                 select new
                //                 {
                //                     Id_Pais = item2.Id_Pais,
                //                     Nacionalidade = item2.Nacionalidade
                //                 };

                ddlNacionalidadeAluno.Items.Clear();
                ddlNacionalidadeAluno.DataSource = listaPais;
                ddlNacionalidadeAluno.DataValueField = "Nacionalidade";
                ddlNacionalidadeAluno.DataTextField = "Nacionalidade";
                ddlNacionalidadeAluno.DataBind();
                ddlNacionalidadeAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione uma Nacionalidade", ""));
                ddlNacionalidadeAluno.SelectedValue = "";

                ddlPaisResidenciaAluno.Items.Clear();
                ddlPaisResidenciaAluno.DataSource = listaPais;
                ddlPaisResidenciaAluno.DataValueField = "Nome";
                ddlPaisResidenciaAluno.DataTextField = "Nome";
                ddlPaisResidenciaAluno.DataBind();
                ddlPaisResidenciaAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um País", ""));
                ddlPaisResidenciaAluno.SelectedValue = "";

                ddlPaisEmpresaAluno.Items.Clear();
                ddlPaisEmpresaAluno.DataSource = listaPais;
                ddlPaisEmpresaAluno.DataValueField = "Nome";
                ddlPaisEmpresaAluno.DataTextField = "Nome";
                ddlPaisEmpresaAluno.DataBind();
                ddlPaisEmpresaAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um País", ""));
                ddlPaisEmpresaAluno.SelectedValue = "";

                List<Estado> listaEstado = aplicacaoGerais.ListaEstado();
                ddlEstadoNasctoAluno.Items.Clear();
                ddlEstadoNasctoAluno.DataSource = listaEstado;
                ddlEstadoNasctoAluno.DataValueField = "Sigla";
                ddlEstadoNasctoAluno.DataTextField = "Nome";
                ddlEstadoNasctoAluno.DataBind();
                ddlEstadoNasctoAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Estado", ""));
                ddlEstadoNasctoAluno.SelectedValue = "";

                ddlEstadoResidenciaAluno.Items.Clear();
                ddlEstadoResidenciaAluno.DataSource = listaEstado;
                ddlEstadoResidenciaAluno.DataValueField = "Sigla";
                ddlEstadoResidenciaAluno.DataTextField = "Nome";
                ddlEstadoResidenciaAluno.DataBind();
                ddlEstadoResidenciaAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Estado", ""));
                ddlEstadoResidenciaAluno.SelectedValue = "";

                ddlEstadoEmpresaAluno.Items.Clear();
                ddlEstadoEmpresaAluno.DataSource = listaEstado;
                ddlEstadoEmpresaAluno.DataValueField = "Sigla";
                ddlEstadoEmpresaAluno.DataTextField = "Nome";
                ddlEstadoEmpresaAluno.DataBind();
                ddlEstadoEmpresaAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Estado", ""));
                ddlEstadoEmpresaAluno.SelectedValue = "";

                divDDLEstadoNasctoAluno.Style.Add("display", "none");
                divDDLCidadeNasctoAluno.Style.Add("display", "none");
                divTXTEstadoNasctoAluno.Style.Add("display", "none");
                divTXTCidadeNasctoAluno.Style.Add("display", "none");

                divDDLEstadoResidenciaAluno.Style.Add("display", "none");
                divDDLCidadeResidenciaAluno.Style.Add("display", "none");
                divTXTEstadoResidenciaAluno.Style.Add("display", "none");
                divTXTCidadeResidenciaAluno.Style.Add("display", "none");

                divDDLEstadoEmpresaAluno.Style.Add("display", "none");
                divDDLCidadeEmpresaAluno.Style.Add("display", "none");
                divTXTEstadoEmpresaAluno.Style.Add("display", "none");
                divTXTCidadeEmpresaAluno.Style.Add("display", "none");

                txtNacionalidadeAlunoAnt.Style.Add("display", "none");
                txtCidadeNasctoAlunoAnt.Style.Add("display", "none");

                txtPaisResidenciaAlunoAnt.Style.Add("display", "none");
                txtEstadoResidenciaAlunoAnt.Style.Add("display", "none");
                txtCidadeResidenciaAlunoAnt.Style.Add("display", "none");

                txtPaisEmpresaAlunoAnt.Style.Add("display", "none");
                txtEstadoEmpresaAlunoAnt.Style.Add("display", "none");
                txtCidadeEmpresaAlunoAnt.Style.Add("display", "none");

                
                //qTab = HttpContext.Current.Request["hQTab"];

                if (Session[qTab + "sNovoAluno"] != null && (Boolean)Session[qTab + "sNovoAluno"] != true)
                {
                    CarregaAluno();
                }
                else
                {
                    PreparaTelaNovoAluno();
                }

                hEscrita.Value = "1";

                if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 6))
                {
                    if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 6).FirstOrDefault().escrita != true)
                    {
                        btnCriarUsuario.Visible = false;
                        bntSalvarAlunoAcima_2.Visible = false;
                        bntSalvarAluno_2.Visible = false;
                        hEscrita.Value = "0";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fInibeAlteracoes();", true);
                    }
                }
            }
        }

        private void CarregaAluno()
        {
            alunos aluno;
            string qTab = HttpContext.Current.Request.Form["hQTab"];
            aluno = (alunos)Session[qTab + "Aluno"];

            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            //if (aluno.matricula_turma.Count != 0)
            //{
            //    divTurmaTem.Visible = true;
            //    divTurmaNaoTem.Visible = false;
            //    int idTurma = 0;

            //    if (aluno.matricula_turma.Count > 1)
            //    {
            //        List<matricula_turma> ListaMatricula = new List<matricula_turma>();
            //        ListaMatricula = aluno.matricula_turma.OrderByDescending(X=> X.id_turma).ToList();

            //        var ListaMatricula2 = from itemMatricula in ListaMatricula
            //                              select new
            //                              {
            //                                  id_Turma = itemMatricula.id_turma,
            //                                  Cod_Turma_NomeCurso = itemMatricula.turmas.cod_turma + " - " + itemMatricula.turmas.cursos.nome,
            //                              };

            //        idTurma = Convert.ToInt32(ListaMatricula2.ElementAt(0).id_Turma);
            //        divTurmaDiversas.Visible = true;
            //        ddlTurmaAluno.Items.Clear();
            //        ddlTurmaAluno.DataSource = ListaMatricula2;
            //        ddlTurmaAluno.DataValueField = "id_Turma";
            //        ddlTurmaAluno.DataTextField = "Cod_Turma_NomeCurso";
            //        ddlTurmaAluno.DataBind();
            //        ddlTurmaAluno.SelectedIndex = 0;
            //    }
            //    else
            //    {
            //        idTurma = Convert.ToInt32(aluno.matricula_turma.ElementAt(0).id_turma);
            //        divTurmaDiversas.Visible = false;
            //    }

            //    PreencheTurma(idTurma, aluno);
            //}
            //else
            //{
            //    divTurmaTem.Visible = false;
            //    divTurmaNaoTem.Visible = true;
            //    divTurmaDiversas.Visible = false;
            //    msgSemResultadosHistorico.Visible = true;
            //    grdHistoricoAluno.Visible = false;
            //    divMensagemOrientador.Visible = true;
            //    grdOrientador.Visible = false;
            //    txtTituloOrientacao.Value = "";
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "funcApagaBotoesHistorico();", true);
            //}
            string sAtcive = "active";
            //Grupos
            // 1-TI 
            // 2-Alunos 
            // 3-Secretaria 
            // 4-FIPT - Financeiro (Lindomar) 
            // 5-Professores 
            // 6-Financeiro (marisa) 
            // 7-Biblioteca (natalina) 
            // 8-Gerencia (eduardo) 
            // 9-Estagiário 
            //10-Coordenadores (eduardo) 
            if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_grupo == 3 || x.id_grupo == 1))
            {
                bntSalvarDadosTurmaNew.Visible = true;
            }
            else
            {
                bntSalvarDadosTurmaNew.Visible = false;
            }

            if (usuario.id_grupo_acesso == 10) //Grupo Coordenador
            {
                divCoordenadores.Style["display"] = "none";
            }

            if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x=> x.id_tela == 19))
            {
                //12. Situação da Matrícula
                tabHistoricoMatriculaNew.Style["display"] = "block";
                tabHistoricoMatriculaNew.Attributes["class"] = sAtcive;
                tab_HistoricoMatriculaNew.Attributes["class"] = "tab-pane " + sAtcive;
                sAtcive = "";
                if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 19).FirstOrDefault().modificacao == true)
                {
                    btnIncluirHistoricoMatricula.Visible = true;
                }
                else
                {
                    btnIncluirHistoricoMatricula.Visible = false;
                }
            }
            else
            {
                tabHistoricoMatriculaNew.Style["display"] = "none";
            }

            if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x=> x.id_tela == 28))
            {
                //18. Histórico Escolar
                tabHistoricoAlunoNew.Style["display"] = "block";
                tabHistoricoAlunoNew.Attributes["class"] = sAtcive;
                tab_HistoricoAlunoNew.Attributes["class"] = "tab-pane " + sAtcive;
                sAtcive = "";
            }
            else
            {
                tabHistoricoAlunoNew.Style["display"] = "none";
            }

                if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 30))
            {
                //19. Cadastro de Orientação
                tabOrientacaoAlunoNew.Style["display"] = "block";
                tabOrientacaoAlunoNew.Attributes["class"] = sAtcive;
                tab_OrientacaoAlunoNew.Attributes["class"] = "tab-pane " + sAtcive;
                sAtcive = "";
                if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 30).FirstOrDefault().modificacao == true)
                //Se não tiver direito desliga os botões de alteração/insersão
                {
                    btnAlterarOrientadorOrientacao.Visible=true;
                    btnExcluirOrientacao.Visible = true;
                    btnSalvarOrientacao.Visible = true;
                    btnAdicionarCoOrientacao.Visible = true;
                }
                else
                {
                    btnAlterarOrientadorOrientacao.Visible = false;
                    btnExcluirOrientacao.Visible = false;
                    btnSalvarOrientacao.Visible = false;
                    btnAdicionarCoOrientacao.Visible = false;
                }
            }
            else
            {
                tabOrientacaoAlunoNew.Style["display"] = "none";
            }

            if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 31))
            {
                //20. Cadastro de Bancas
                tabBancaAlunoNew.Style["display"] = "block";
                tabBancaAlunoNew.Attributes["class"] = sAtcive;
                tab_Banca.Attributes["class"] = "tab-pane " + sAtcive;
                sAtcive = "";
            }
            else
            {
                tabBancaAlunoNew.Style["display"] = "none";
            }
            if (usuario.id_grupo_acesso == 10) //Grupo Coordenador
            {
                btnLocalizarDissertacaoBancaDefesa.Attributes["class"] = "hidden";
                btnSalvarDissertacaoBancaDefesa.Attributes["class"] = "hidden";
                btnLocalizarContrato.Attributes["class"] = "hidden";
                btnLocalizarArtigo.Attributes["class"] = "hidden";
            }

            if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 52))
            {
                //21.Reuniões CPG
                tabProrrogacaoCPG.Style["display"] = "block";
                tabProrrogacaoCPG.Attributes["class"] = sAtcive;
                tab_ProrrogacaoCPG.Attributes["class"] = "tab-pane " + sAtcive;
                sAtcive = "";
                //=======================
                if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Where(x => x.id_tela == 52).FirstOrDefault().modificacao == true)
                //Se não tiver direito desliga os botões de alteração/insersão
                {
                    btnIncluirProrrogacaoCPG.Visible = true;
                }
                else
                {
                    btnIncluirProrrogacaoCPG.Visible = false;
                }
                //tabProrrogacaoCPG.Style["display"] = "none";
            }
            else
            {
                tabProrrogacaoCPG.Style["display"] = "none";
            }

            if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 51))
            {
                //22.Contratos
                tabContrato.Style["display"] = "block";
                tabContrato.Attributes["class"] = sAtcive;
                tab_Contrato.Attributes["class"] = "tab-pane " + sAtcive;
                sAtcive = "";

                //tabContrato.Style["display"] = "none";
            }
            else
            {
                tabContrato.Style["display"] = "none";
            }

            if (usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 57))
            {
                //24. Certificado Titulação
                tabCertificado.Style["display"] = "block";
                tabCertificado.Attributes["class"] = sAtcive;
                tab_Certificado.Attributes["class"] = "tab-pane " + sAtcive;
                sAtcive = "";

                //tabContrato.Style["display"] = "none";
            }
            else
            {
                tabCertificado.Style["display"] = "none";
            }

            if (aluno.sexo != null)
            {
                lblTituloAlunoAluna.Text = (aluno.sexo.ToUpper() == "M") ? "Aluno" : "Aluna";
            }
            
            lblTituloNomeAluno.Text = aluno.nome;
            lblTituloMatricula.Text = "Matrícula";
            lblNumeroMatricula.Text = aluno.idaluno.ToString();
            lblTituloAlteradoPor.Text = "Alterado por: ";
            lblAlteradoPor.Text = aluno.usuario;
            lblTituloAlteradoEm.Text = "em: ";
            lblAlteradoEm.Text = String.Format("{0:dd/MM/yyyy}", aluno.data_alteracao);
            divBotaoFoto.Visible = true;
            divTextoBotaoFoto.Visible = false;

            usuarios itemUsurioAluno = new usuarios();
            UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
            itemUsurioAluno.usuario = aluno.idaluno.ToString();
            itemUsurioAluno = aplicacaoUsuario.BuscaUsuario(itemUsurioAluno);
            if (itemUsurioAluno == null)
            {
                SHA1CryptoServiceProvider objSHA1 = new SHA1CryptoServiceProvider();
                ASCIIEncoding objEncoding = new ASCIIEncoding();
                usuarios usuarioAluno = new usuarios();
                usuarioAluno.usuario = Convert.ToString(aluno.idaluno);
                usuarioAluno.nome = aluno.nome;
                usuarioAluno.un = "Acadêmico";
                usuarioAluno.email = aluno.email;
                usuarioAluno.id_grupo_acesso = 2;
                usuarioAluno.nomeSocial = aluno.nome.Substring(0, aluno.nome.IndexOf(" "));
                usuarioAluno.avatar = "";
                usuarioAluno.status = 1;
                string sAuxSenha;
                sAuxSenha = aluno.cpf.Replace(".","").Substring(0,6);
                usuarioAluno.senha = Convert.ToBase64String(objSHA1.ComputeHash(objEncoding.GetBytes(sAuxSenha)));

                aplicacaoUsuario.CriarUsuario(usuarioAluno);
                itemUsurioAluno = new usuarios();
                itemUsurioAluno.usuario = Convert.ToString(aluno.idaluno);
                itemUsurioAluno = aplicacaoUsuario.BuscaUsuario(itemUsurioAluno);

            }
            if (itemUsurioAluno.avatar != "")
            {
                imgAluno.Src = "img/pessoas/" + itemUsurioAluno.avatar + "?" + DateTime.Now;
            }

            else
            {
                imgAluno.Src = "img/pessoas/avatarunissex.jpg" + "?" + DateTime.Now;
            }
            imgFotoOriginal.Src = imgAluno.Src;
            txtNomeAluno.Value = aluno.nome;
            txtMatriculaAluno.Value = aluno.idaluno.ToString();
            txtDataCadastroAluno.Value = String.Format("{0:dd/MM/yyyy}", aluno.data_cadastro);//String.Format("{0:yyyy-MM-dd}", aluno.data_cadastro);
            txtCPFAluno.Value = aluno.cpf;

            if (aluno.estrangeiro == "Não" || aluno.estrangeiro == null)
            {
                ddlEstrangeiro.SelectedIndex = 0;
                divDDLEstadoNasctoAluno.Style.Add("display", "block");
                divDDLCidadeNasctoAluno.Style.Add("display", "block");
            }
            else
            {
                ddlEstrangeiro.SelectedIndex = 1;
                divTXTEstadoNasctoAluno.Style.Add("display", "block");
                divTXTCidadeNasctoAluno.Style.Add("display", "block");
            }
            if (aluno.sexo != null)
            {
                if (aluno.sexo.ToUpper() == "M")
                {
                    ddlSexoAluno.SelectedIndex = 0;
                }
                else
                {
                    ddlSexoAluno.SelectedIndex = 1;
                }
            }
            
            txtConvenioAluno.Value = aluno.convenio;
            txtLinhaPesquisaAluno.Value = aluno.linha_pesquisa;
            txtDataUltimaAlteracao.Value = String.Format("{0:dd/MM/yyyy}", aluno.data_alteracao);// String.Format("{0:yyyy-MM-dd}", aluno.data_alteracao);
            txtDataNascimentoAluno.Value = String.Format("{0:yyyy-MM-dd}", aluno.data_nascimento);

            if (aluno.pais_nasc != null)
            {
                ddlNacionalidadeAluno.SelectedValue = aluno.pais_nasc.Trim();
            }

            if (ddlNacionalidadeAluno.SelectedValue == "")
            {
                txtNacionalidadeAlunoAnt.Value = aluno.pais_nasc;
                txtNacionalidadeAlunoAnt.Style.Add("display", "block");
            }

            if (aluno.estado_nasc != null)
            {
                if (aluno.estrangeiro == "Não")
                {
                    ddlEstadoNasctoAluno.SelectedValue = aluno.estado_nasc;
                }
                else
                {
                    txtEstadoNasctoAluno.Value = aluno.estado_nasc;
                }
                
            }
            ddlEstadoNasctoAluno_SelectedIndexChanged(null, null);

            if (aluno.cidade_nasc != null)
            {
                ddlCidadeNasctoAluno.SelectedValue = aluno.cidade_nasc.Trim();
                txtCidadeNasctoAluno.Value = aluno.cidade_nasc.Trim();
            }
            
            if (ddlCidadeNasctoAluno.SelectedValue == "" && aluno.estrangeiro == "Não" && aluno.cidade_nasc != "")
            {
                txtCidadeNasctoAlunoAnt.Value = aluno.cidade_nasc;
                txtCidadeNasctoAlunoAnt.Style.Add("display", "block");
            }

            txtEmail1Aluno.Value = aluno.email;
            txtEmail2Aluno.Value = aluno.email2;
            txtTelefoneAluno.Value = aluno.telefone_res;
            txtCelularAluno.Value = aluno.celular_res;
            ddlTipoDoctoAluno.SelectedValue = aluno.tipo_documento;
            txtNumeroDoctoAluno.Value = aluno.numero_documento;
            txtDigitoDoctoAluno.Value = aluno.digito_num_documento;
            txtOrgaoExpeditorAluno.Value = aluno.orgao_expedidor;
            txtDataExpedicaoAluno.Value = String.Format("{0:yyyy-MM-dd}", aluno.data_expedicao);
            txtDataValidadeDoctoAluno.Value = String.Format("{0:yyyy-MM-dd}", aluno.data_validade);
            txtCepResidenciaAluno.Value = aluno.cep_res;
            txtLogradouroResidenciaAluno.Value = aluno.logradouro_res;
            txtNumeroResidenciaAluno.Value = aluno.numero_res;
            txtComplementoResidenciaAluno.Value = aluno.complemento_res;
            txtBairroResidenciaAluno.Value = aluno.bairro_res;

            if (aluno.pais_res == "Brasil")
            {
                divDDLEstadoResidenciaAluno.Style.Add("display", "block");
                divDDLCidadeResidenciaAluno.Style.Add("display", "block");
            }
            else
            {
                divTXTEstadoResidenciaAluno.Style.Add("display", "block");
                divTXTCidadeResidenciaAluno.Style.Add("display", "block");
            }

            ddlPaisResidenciaAluno.SelectedValue = aluno.pais_res == null ? "" : aluno.pais_res.Trim();
            if (ddlPaisResidenciaAluno.SelectedValue == "" && aluno.pais_res != null)
            {
                txtPaisResidenciaAlunoAnt.Value = aluno.pais_res;
                txtPaisResidenciaAlunoAnt.Style.Add("display", "block");
            }

            ddlEstadoResidenciaAluno.SelectedValue = aluno.uf_res;
            txtEstadoResidenciaAluno.Value = aluno.uf_res;

            if (ddlEstadoResidenciaAluno.SelectedValue == "" && aluno.pais_res == "Brasil" && aluno.uf_res.Trim() != "")
            {
                txtEstadoResidenciaAlunoAnt.Value = aluno.cidade_res;
                txtEstadoResidenciaAlunoAnt.Style.Add("display", "block");
            }
            ddlEstadoResidenciaAluno_SelectedIndexChanged(null, null);

            if (aluno.cidade_res != null)
            {
                ddlCidadeResidenciaAluno.SelectedValue = aluno.cidade_res.Trim();
                txtCidadeResidenciaAluno.Value = aluno.cidade_res.Trim();
            }
            
            if (ddlCidadeResidenciaAluno.SelectedValue == "" && aluno.pais_res == "Brasil" && aluno.cidade_res.Trim() != "")
            {
                txtCidadeResidenciaAlunoAnt.Value = aluno.cidade_res;
                txtCidadeResidenciaAlunoAnt.Style.Add("display", "block");
            }

            txtFormacaoAluno.Value = aluno.formacao;
            txtInstituicaoAluno.Value = aluno.escola;
            txtAnoFormacaoAluno.Value = aluno.ano_graduacao.ToString();

            txtEmpresaAluno.Value = aluno.empresa;
            txtNomeFantasiaAluno.Value = aluno.nome_fantasia;
            txtCNPJEmpresaAluno.Value = aluno.cnpj;
            txtIEEmpresaAluno.Value = aluno.ie;
            txtNomeContato.Value = aluno.contato;
            txtEmailContato.Value = aluno.email_contato;
            txtCEPEmpresaAluno.Value = aluno.cep_empresa;
            txtLogradouroEmpresaAluno.Value = aluno.logradouro_empresa;
            txtNumeroEmpresaAluno.Value = aluno.numero_empresa;
            txtComplementoEmpresaAluno.Value = aluno.complemento_empresa;
            txtBairroEmpresaAluno.Value = aluno.bairro_empresa;
            txtCargoAluno.Value = aluno.cargo;
            txtTelefoneEmpresaAluno.Value = aluno.telefone_empresa;
            txtRamalEmpresaAluno.Value = aluno.telefone_empresa_ramal;
            txtPalavraChaveAluno.Value = aluno.palavra_chave;
            txtProfissaoAluno.Value = aluno.profissao;
            ddlEstadoCivilAluno.SelectedValue = aluno.estado_civil;

            if (aluno.pais_empresa == "Brasil")
            {
                divDDLEstadoEmpresaAluno.Style.Add("display", "block");
                divDDLCidadeEmpresaAluno.Style.Add("display", "block");
            }
            else
            {
                divTXTEstadoEmpresaAluno.Style.Add("display", "block");
                divTXTCidadeEmpresaAluno.Style.Add("display", "block");
            }

            ddlPaisEmpresaAluno.SelectedValue = aluno.pais_empresa == null ? "" : aluno.pais_empresa.Trim();
            if (ddlPaisEmpresaAluno.SelectedValue == "" && aluno.pais_empresa != null)
            {
                txtPaisEmpresaAlunoAnt.Value = aluno.pais_empresa;
                txtPaisEmpresaAlunoAnt.Style.Add("display", "block");
            }

            ddlEstadoEmpresaAluno.SelectedValue = aluno.uf_empresa;
            txtEstadoEmpresaAluno.Value = aluno.uf_empresa;

            if (ddlEstadoEmpresaAluno.SelectedValue == "" && aluno.pais_empresa == "Brasil" && aluno.uf_empresa.Trim() != "")
            {
                txtEstadoEmpresaAlunoAnt.Value = aluno.cidade_empresa;
                txtEstadoEmpresaAlunoAnt.Style.Add("display", "block");
            }
            ddlEstadoEmpresaAluno_SelectedIndexChanged(null, null);

            if (aluno.cidade_empresa != null)
            {
                ddlCidadeEmpresaAluno.SelectedValue = aluno.cidade_empresa.Trim();
                txtCidadeEmpresaAluno.Value = aluno.cidade_empresa.Trim();
            }
            
            if (ddlCidadeResidenciaAluno.SelectedValue == "" && aluno.pais_empresa == "Brasil" && aluno.cidade_empresa.Trim() != "")
            {
                txtCidadeEmpresaAlunoAnt.Value = aluno.cidade_empresa;
                txtCidadeEmpresaAlunoAnt.Style.Add("display", "block");
            }

            if (aluno.entregou_rg)
                chkRG.Checked = true;

            if (aluno.entregou_cpf)
                chkCPF.Checked = true;

            if (aluno.entregou_historico)
                chkHistoricoEscolar.Checked = true;

            if (aluno.entregou_diploma)
                chkDiploma.Checked = true;

            if (aluno.entregou_comprovante_end)
                chkComprovanteEndereco.Checked = true;

            if (aluno.entregou_fotos)
                chkFoto.Checked = true;

            if (aluno.entregou_certidao)
                chkCertidaoNascimento.Checked = true;

            if (aluno.entregou_contrato)
                chkContratoAssinado.Checked = true;

            if (aluno.RefazerProficienciaIngles == 1)
                chkRefazerProvaProficienciaIngles.Checked = true;

            if (aluno.RefazerProvaPortugues == 1)
                chkRefazerProvaPortugues.Checked = true;

            txtOcorrenciaAluno.Value = aluno.ocorrencias;


            btnCriarUsuario.Attributes["class"] = "btn btn-primary";
            btnCriarUsuario.Disabled = false;
            if (Session["AdiciondoSucesso"] != null)
            {
                Session["AdiciondoSucesso"] = null;
                lblMensagem.Text = "Cadastro do Aluno criado com sucesso.";
                lblTituloMensagem.Text = "Novo Aluno";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-success');", true);
            }
        }

        private void PreparaTelaNovoAluno()
        {

            //SHA1CryptoServiceProvider objSHA1 = new SHA1CryptoServiceProvider();
            //ASCIIEncoding objEncoding = new ASCIIEncoding();

            //AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
            //UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
            //List<alunos> listaAluno = new List<alunos>();
            //int[] qIdCurso = new int[1];
            //qIdCurso[0] = 0;
            //alunos item = new alunos();

            //int iAlunosAlterados = 0;
            //int iAlunosSemUsuario = 0;


            //listaAluno = aplicacaoAluno.ListaItem(item, qIdCurso);

            //usuarios item_usuario;

            //foreach (var elemento in listaAluno)
            //{
            //    item_usuario = new usuarios();
            //    item_usuario.usuario = elemento.idaluno.ToString();
            //    item_usuario = aplicacaoUsuario.BuscaUsuario(item_usuario);
            //    string sAuxsenha;
            //    sAuxsenha = elemento.idaluno.ToString();

            //    sAuxsenha = Convert.ToBase64String(objSHA1.ComputeHash(objEncoding.GetBytes(sAuxsenha)));

            //    if (item_usuario == null)
            //    {
            //        iAlunosSemUsuario = iAlunosSemUsuario + 1;
            //        item_usuario = new usuarios();
            //        item_usuario.usuario = elemento.idaluno.ToString();
            //        item_usuario.nome = elemento.nome;
            //        item_usuario.un = "Acadêmico";
            //        item_usuario.email = elemento.email;
            //        item_usuario.id_grupo_acesso = 2;
            //        item_usuario.status = 1;
            //        item_usuario.avatar = "";
            //        item_usuario.nomeSocial = elemento.nome.Substring(0, elemento.nome.IndexOf(" "));
            //        string sAuxSenha;
            //        sAuxSenha = elemento.cpf.Replace(".", "").Substring(0, 6);
            //        item_usuario.senha = Convert.ToBase64String(objSHA1.ComputeHash(objEncoding.GetBytes(sAuxSenha)));

            //        aplicacaoUsuario.CriarUsuario(item_usuario);
            //    }

            //    else if (item_usuario.senha == sAuxsenha)
            //    {
            //        iAlunosAlterados = iAlunosAlterados + 1;

            //        sAuxsenha = elemento.cpf.Replace(".", "").Substring(0, 6);

            //        item_usuario.senha = Convert.ToBase64String(objSHA1.ComputeHash(objEncoding.GetBytes(sAuxsenha)));

            //        aplicacaoUsuario.AlterarSenhaUsuario(item_usuario);
            //    }


            //}


            //Aqui se for um novo aluno (novo cadastro)

            lblTituloAlunoAluna.Text = "Aluno";
            lblTituloNomeAluno.Text = "Novo";
            lblTituloMatricula.Text = "";
            lblNumeroMatricula.Text = "";
            lblTituloAlteradoPor.Text = "";
            lblAlteradoPor.Text = "";
            lblTituloAlteradoEm.Text = "";
            lblAlteradoEm.Text = "";

            txtNacionalidadeAlunoAnt.Style.Add("display", "none");
            txtCidadeNasctoAlunoAnt.Style.Add("display", "none");

            txtPaisResidenciaAlunoAnt.Style.Add("display", "none");
            txtEstadoResidenciaAlunoAnt.Style.Add("display", "none");
            txtCidadeResidenciaAlunoAnt.Style.Add("display", "none");

            txtPaisEmpresaAlunoAnt.Style.Add("display", "none");
            txtEstadoEmpresaAlunoAnt.Style.Add("display", "none");
            txtCidadeEmpresaAlunoAnt.Style.Add("display", "none");

            divTXTEstadoNasctoAluno.Style.Add("display", "none");
            divTXTCidadeNasctoAluno.Style.Add("display", "none");
            divDDLEstadoNasctoAluno.Style.Add("display", "block");
            divDDLCidadeNasctoAluno.Style.Add("display", "block");
            txtEstadoNasctoAluno.Value = "";
            txtCidadeNasctoAluno.Value = "";

            divTXTEstadoResidenciaAluno.Style.Add("display", "none");
            divTXTCidadeResidenciaAluno.Style.Add("display", "none");
            divDDLEstadoResidenciaAluno.Style.Add("display", "block");
            divDDLCidadeResidenciaAluno.Style.Add("display", "block");
            txtEstadoResidenciaAluno.Value = "";
            txtCidadeResidenciaAluno.Value = "";

            divTXTEstadoEmpresaAluno.Style.Add("display", "none");
            divTXTCidadeEmpresaAluno.Style.Add("display", "none");
            divDDLEstadoEmpresaAluno.Style.Add("display", "block");
            divDDLCidadeEmpresaAluno.Style.Add("display", "block");
            txtEstadoEmpresaAluno.Value = "";
            txtCidadeEmpresaAluno.Value = "";

            btnCriarUsuario.Attributes["class"] = "btn btn-primary";
            btnCriarUsuario.Disabled = true;

            divBotaoFoto.Visible = false;
            divTextoBotaoFoto.Visible = true;

            imgAluno.Src = "img/pessoas/avatarunissex.jpg";
            txtNomeAluno.Value = "";
            txtMatriculaAluno.Value = "";
            txtDataCadastroAluno.Value = "";
            txtCPFAluno.Value = "";
            ddlEstrangeiro.SelectedValue = "Não";
            ddlSexoAluno.SelectedValue = "";
            txtConvenioAluno.Value = "";
            txtLinhaPesquisaAluno.Value = "";
            txtDataUltimaAlteracao.Value = "";
            txtDataNascimentoAluno.Value = "";
            ddlNacionalidadeAluno.SelectedValue = "Brasileira";
            ddlEstadoNasctoAluno.SelectedValue = "";
            ddlCidadeNasctoAluno.SelectedValue = "";
            txtEmail1Aluno.Value = "";
            txtEmail2Aluno.Value = "";
            txtTelefoneAluno.Value = "";
            txtCelularAluno.Value = "";
            ddlTipoDoctoAluno.SelectedValue = "";
            txtNumeroDoctoAluno.Value = "";
            txtDigitoDoctoAluno.Value = "";
            txtOrgaoExpeditorAluno.Value = "";
            txtDataExpedicaoAluno.Value = "";
            txtDataValidadeDoctoAluno.Value = "";
            txtCepResidenciaAluno.Value = "";
            txtLogradouroResidenciaAluno.Value = "";
            txtNumeroResidenciaAluno.Value = "";
            txtComplementoResidenciaAluno.Value = "";
            txtBairroResidenciaAluno.Value = "";
            ddlPaisResidenciaAluno.SelectedValue = "Brasil";
            ddlEstadoResidenciaAluno.SelectedValue = "";
            ddlCidadeResidenciaAluno.SelectedValue = "";
            txtFormacaoAluno.Value = "";
            txtInstituicaoAluno.Value = "";
            txtAnoFormacaoAluno.Value = "";
            txtEmpresaAluno.Value = "";
            txtNomeFantasiaAluno.Value = "";
            txtCNPJEmpresaAluno.Value = "";
            txtIEEmpresaAluno.Value = "";
            txtNomeContato.Value = "";
            txtEmailContato.Value = "";
            txtCEPEmpresaAluno.Value = "";
            txtLogradouroEmpresaAluno.Value = "";
            txtNumeroEmpresaAluno.Value = "";
            txtComplementoEmpresaAluno.Value = "";
            txtBairroEmpresaAluno.Value = "";
            ddlPaisEmpresaAluno.SelectedValue = "";
            ddlEstadoEmpresaAluno.SelectedValue = "";
            ddlCidadeEmpresaAluno.SelectedValue = "";
            txtCargoAluno.Value = "";
            txtTelefoneEmpresaAluno.Value = "";
            txtRamalEmpresaAluno.Value = "";
            txtPalavraChaveAluno.Value = "";
            txtProfissaoAluno.Value = "";
            ddlEstadoCivilAluno.SelectedValue = "";
            chkRG.Checked = false;
            chkCPF.Checked = false;
            chkHistoricoEscolar.Checked = false;
            chkDiploma.Checked = false;
            chkComprovanteEndereco.Checked = false;
            chkFoto.Checked = false;
            chkCertidaoNascimento.Checked = false;
            chkContratoAssinado.Checked = false;
            txtOcorrenciaAluno.Value = "";
            string qTab = HttpContext.Current.Request.Form["hQTab"];
            Session[qTab + "sNovoAluno"] = null;
            Session[qTab + "Aluno"] = null;
            tabSituacaoAcademica.Attributes["class"] = "hidden";
            //tabSituacaoAcademicaNew.Attributes["class"] = "hidden";
            tabSituacaoAcademicaNew.Style["display"] = "none";

        }

        private void PreencheTurma(int qTurma, alunos aluno)
        {
            //txtIdTurmaAluno.Value = qTurma.ToString();
            int index = aluno.matricula_turma.ToList().FindIndex(x => x.id_turma == qTurma);
            txtIdTurma.Value = aluno.matricula_turma.ToList()[index].turmas.id_turma.ToString();
            //txtCodTurmaAluno.Value = aluno.matricula_turma.ToList()[index].turmas.cod_turma;
            //txtTipoCursoAluno.Value = aluno.matricula_turma.ToList()[index].turmas.cursos.tipos_curso.tipo_curso;
            //txtDataEntregaArtigo.Value = (aluno.matricula_turma.ToList()[index].data_artigo == null) ? "" : String.Format("{0:yyyy-MM-dd}", aluno.matricula_turma.ToList()[index].data_artigo);

            var listaHistorico = aluno.matricula_turma.ToList()[index].historico_matricula_turma.Where(x => x.situacao.ToUpper() == "TRANCADO" || x.situacao.ToUpper() == "PRORROGAÇÃO CPG").ToList();
            int iDias = 0;
            foreach (var itemHistorico in listaHistorico)
            {
                if (itemHistorico.data_inicio.HasValue && itemHistorico.data_fim.HasValue)
                {
                    iDias = iDias + itemHistorico.data_fim.Value.Subtract(itemHistorico.data_inicio.Value).Days;
                }
            }

            //txtDataTerminoCursoAluno.Value = String.Format("{0:dd/MM/yyyy}", aluno.matricula_turma.ToList()[index].turmas.data_fim.Value.AddDays(iDias));
            //txtQuadrimestreAluno.Value = aluno.matricula_turma.ToList()[index].turmas.quadrimestre;
            //txtCursoAluno.Value = aluno.matricula_turma.ToList()[index].turmas.cursos.sigla + " - " + aluno.matricula_turma.ToList()[index].turmas.cursos.nome;
            //txtDataInicioCursoAluno.Value = String.Format("{0:dd/MM/yyyy}", aluno.matricula_turma.ToList()[index].turmas.data_inicio);
            //txtDataFimCursoAluno.Value = String.Format("{0:dd/MM/yyyy}", aluno.matricula_turma.ToList()[index].turmas.data_fim);
            //txtAreaConcentracaoAluno.Value = (aluno.matricula_turma.ToList()[index].areas_concentracao == null) ? "" : aluno.matricula_turma.ToList()[index].areas_concentracao.nome;
            matricula_turma matriculaTurma = new matricula_turma();
            matriculaTurma = aluno.matricula_turma.ElementAt(index);
            string dataSituacao;
            if (matriculaTurma.historico_matricula_turma.Count >0)
            {
                dataSituacao = matriculaTurma.historico_matricula_turma.OrderByDescending(x => x.id_historico).FirstOrDefault().data_fim == null ? " - " : matriculaTurma.historico_matricula_turma.OrderByDescending(x => x.id_historico).FirstOrDefault().data_fim.ToString().Substring(0, 10);
                //txtSituacaoAluno.Value = matriculaTurma.historico_matricula_turma.OrderByDescending(x => x.id_historico).FirstOrDefault().situacao + " (" + dataSituacao + ")";
            }
            else
            {
                //txtSituacaoAluno.Value = "Sem registro na historico_matricula_turma";
            }
            

            PreencheHistorico(Convert.ToInt32(aluno.idaluno), qTurma);
            PreencheOrientacao(Convert.ToInt32(aluno.idaluno), qTurma);
        }

        private void PreencheHistorico(int Id_Aluno, int id_Turma)
        {
            AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
            List<oferecimentos> listaOferecimento = new List<oferecimentos>();
            listaOferecimento = aplicacaoAluno.ListaOferecimentosAluno(Id_Aluno, id_Turma);
            if (listaOferecimento.Count > 0)
            {
                var ListaHIstorico = from item in listaOferecimento
                                     select new
                                     {
                                         Inicio = String.Format("{0:dd/MM/yyyy}", item.datas_aulas.Min(x => x.data_aula)),
                                         datas_aulas = item.datas_aulas.Min(x => x.data_aula),
                                         Quadrimestre = item.quadrimestre,
                                         DisciplinaCodigo = item.disciplinas.codigo,
                                         DisciplinaNome = item.disciplinas.nome,
                                         Duracao = item.carga_horaria.ToString() + " h",
                                         Frequencia = (item.presenca.Where(x => x.id_aluno == Id_Aluno).Count() == 0) ? "0,00%" : ((item.presenca.Where(x => x.id_aluno == Id_Aluno && x.presente == true).Count()) / (item.presenca.Where(x => x.id_aluno == Id_Aluno).Count() * 0.01)).ToString("0.##") + "%",
                                         Conceito = (item.notas.Where(x => x.id_aluno == Id_Aluno).FirstOrDefault() == null) ? "" : (item.notas.Where(x => x.id_aluno == Id_Aluno).Select(x => x.conceito).FirstOrDefault() == null) ? "" : item.notas.Where(x => x.id_aluno == Id_Aluno).Select(x => x.conceito).FirstOrDefault(),
                                         //Conceito = (item.notas.Where(x => x.id_aluno == Id_Aluno && x.autorizado == "S").FirstOrDefault() == null) ? "" : item.notas.Where(x => x.id_aluno == Id_Aluno).Select(x => x.conceito).FirstOrDefault(),
                                         Resultado = (item.notas.Where(x => x.id_aluno == Id_Aluno).FirstOrDefault() == null) ? "" : (item.notas.Where(x => x.id_aluno == Id_Aluno).FirstOrDefault().conceitos_de_aprovacao == null) ? "" : item.notas.Where(x => x.id_aluno == Id_Aluno).FirstOrDefault().conceitos_de_aprovacao.descricao,
                                         //Resultado = (item.notas.Where(x => x.id_aluno == Id_Aluno && x.autorizado == "S").FirstOrDefault() == null) ? "" : item.notas.Where(x => x.id_aluno == Id_Aluno).FirstOrDefault().conceitos_de_aprovacao.descricao,
                                         Oferecimento = item.id_oferecimento,
                                         Aluno = Id_Aluno
                                     };

                //grdHistoricoAluno.DataSource = ListaHIstorico.OrderBy(x=> x.datas_aulas).ToList();
                //grdHistoricoAluno.DataBind();
                //grdHistoricoAluno.UseAccessibleHeader = true;
                //grdHistoricoAluno.HeaderRow.TableSection = TableRowSection.TableHeader;
                //msgSemResultadosHistorico.Visible = false;
                //grdHistoricoAluno.Visible = true;
            }
            else
            {
                //msgSemResultadosHistorico.Visible = true;
                //grdHistoricoAluno.DataSource = null;
                //grdHistoricoAluno.DataBind();
                //grdHistoricoAluno.Visible = false;
            }
        }

        private void PreencheOrientacao(int Id_Aluno, int id_Turma)
        {
            GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
            matricula_turma itemMatricula_turma = new matricula_turma();
            itemMatricula_turma = aplicacaoGerais.BuscaMatricula_turma(Id_Aluno, id_Turma);

            Matricula_turma_orientacaoAplicacao aplicacaoMatricula_turma_orientacao = new Matricula_turma_orientacaoAplicacao();
            List<matricula_turma_orientacao> ListaMatricula_turma_orientacao = new List<matricula_turma_orientacao>();
            List<matricula_turma_orientacao> ListaMatricula_turma_Coorientacao = new List<matricula_turma_orientacao>();
            ListaMatricula_turma_orientacao = aplicacaoMatricula_turma_orientacao.ListaItem(itemMatricula_turma).Where(x=> x.tipo_orientacao == "Orientador").ToList();

            if (ListaMatricula_turma_orientacao.Count > 0)
            {
                var ListaOrientador = from item in ListaMatricula_turma_orientacao
                                      select new
                                     {
                                         IDProfessor = item.professores.id_professor,
                                         CPF = item.professores.cpf,
                                         Nome = item.professores.nome,
                                         ApagaLinha = item.id_orientacao + "," + Id_Aluno + "," + id_Turma + ",'" + item.professores.nome + "'," + item.professores.id_professor + "," + "'Orientador'"
                                      };

                //grdOrientador.DataSource = ListaOrientador.ToList();
                //grdOrientador.DataBind();
                //grdOrientador.UseAccessibleHeader = true;
                //grdOrientador.HeaderRow.TableSection = TableRowSection.TableHeader;
                //divMensagemOrientador.Visible = false;
                //grdOrientador.Visible = true;

                //txtTituloOrientacao.Value = ListaMatricula_turma_orientacao[0].titulo;
            }
            else
            {
                //divMensagemOrientador.Visible = true;
                //grdOrientador.Visible = false;
                //txtTituloOrientacao.Value = "";
            }

            ListaMatricula_turma_Coorientacao = aplicacaoMatricula_turma_orientacao.ListaItem(itemMatricula_turma).Where(x => x.tipo_orientacao == "Co-orientador").ToList();

            if (ListaMatricula_turma_Coorientacao.Count > 0)
            {
                var ListaCoOrientador = from item in ListaMatricula_turma_Coorientacao
                                      select new
                                      {
                                          IDProfessor = item.professores.id_professor,
                                          CPF = item.professores.cpf,
                                          Nome = item.professores.nome,
                                          ApagaLinha = item.id_orientacao + "," + Id_Aluno + "," + id_Turma + ",'" + item.professores.nome + "'," + item.professores.id_professor + "," + "'Co-orientador'"
                                      };

                //grdCo_orientador.DataSource = ListaCoOrientador.ToList();
                //grdCo_orientador.DataBind();
                //grdCo_orientador.UseAccessibleHeader = true;
                //grdCo_orientador.HeaderRow.TableSection = TableRowSection.TableHeader;
                //divMensagemCo_orientador.Visible = false;
                //grdCo_orientador.Visible = true;
            }
            else
            {
                //divMensagemCo_orientador.Visible = true;
                //grdCo_orientador.Visible = false;
            }
        }

        public void ddlEstadoNasctoAluno_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (ddlEstadoNasctoAluno.SelectedValue != "")
            {
                ddlCidadeNasctoAluno.Items.Clear();
                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Estado itemEstado = new Estado();
                itemEstado.Sigla = ddlEstadoNasctoAluno.SelectedValue;
                List<Cidade> listaCidade = aplicacaoGerais.ListaCidade(itemEstado);
                ddlCidadeNasctoAluno.Items.Clear();
                ddlCidadeNasctoAluno.DataSource = listaCidade;
                ddlCidadeNasctoAluno.DataValueField = "Nome";
                ddlCidadeNasctoAluno.DataTextField = "Nome";
                ddlCidadeNasctoAluno.DataBind();
                ddlCidadeNasctoAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione uma Cidade", ""));
                ddlCidadeNasctoAluno.SelectedValue = "";
            }
            else
            {
                ddlCidadeNasctoAluno.Items.Clear();
            }
            divDDLEstadoNasctoAluno.Style.Add("display", "block");
            divDDLCidadeNasctoAluno.Style.Add("display", "block");
            divTXTEstadoNasctoAluno.Style.Add("display", "none");
            divTXTCidadeNasctoAluno.Style.Add("display", "none");
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fSelect2();", true);
        }

        public void ddlTurmaAluno_SelectedIndexChanged(Object sender, EventArgs e)
        {
            alunos aluno;
            string qTab = HttpContext.Current.Request.Form["hQTab"];
            aluno = (alunos)Session[qTab + "Aluno"];

            //PreencheTurma(Convert.ToInt32(ddlTurmaAluno.SelectedValue), aluno);

            //ScriptManager.RegisterStartupScript(this.UpdatePanel4, this.UpdatePanel4.GetType(), "Script", "javascript:fSelect2();", true);

        }

        public void ddlEstadoResidenciaAluno_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (ddlEstadoResidenciaAluno.SelectedValue != "")
            {
                ddlCidadeResidenciaAluno.Items.Clear();
                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Estado itemEstado = new Estado();
                itemEstado.Sigla = ddlEstadoResidenciaAluno.SelectedValue;
                List<Cidade> listaCidade = aplicacaoGerais.ListaCidade(itemEstado);
                ddlCidadeResidenciaAluno.Items.Clear();
                ddlCidadeResidenciaAluno.DataSource = listaCidade;
                ddlCidadeResidenciaAluno.DataValueField = "Nome";
                ddlCidadeResidenciaAluno.DataTextField = "Nome";
                ddlCidadeResidenciaAluno.DataBind();
                ddlCidadeResidenciaAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione uma Cidade", ""));
                ddlCidadeResidenciaAluno.SelectedValue = "";
            }
            else
            {
                ddlCidadeResidenciaAluno.Items.Clear();
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", "javascript:fPaisResidencia();fPaisEmpresa();", true);
        }

        public void ddlEstadoEmpresaAluno_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (ddlEstadoEmpresaAluno.SelectedValue != "")
            {
                ddlCidadeEmpresaAluno.Items.Clear();
                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Estado itemEstado = new Estado();
                itemEstado.Sigla = ddlEstadoEmpresaAluno.SelectedValue;
                List<Cidade> listaCidade = aplicacaoGerais.ListaCidade(itemEstado);
                ddlCidadeEmpresaAluno.Items.Clear();
                ddlCidadeEmpresaAluno.DataSource = listaCidade;
                ddlCidadeEmpresaAluno.DataValueField = "Nome";
                ddlCidadeEmpresaAluno.DataTextField = "Nome";
                ddlCidadeEmpresaAluno.DataBind();
                ddlCidadeEmpresaAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione uma Cidade", ""));
                ddlCidadeEmpresaAluno.SelectedValue = "";
            }
            else
            {
                ddlCidadeEmpresaAluno.Items.Clear();
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.UpdatePanel3.GetType(), "Script", "javascript:fPaisResidencia();fPaisEmpresa();", true);
        }

        protected void btnNovoAluno_Click(object sender, EventArgs e)
        {
            PreparaTelaNovoAluno();
        }

        protected void btnImprimirOficial_Click(object sender, EventArgs e)
        {
            ImprimeHistoricoOficial();
        }

        private void ImprimeHistoricoOficial()
        {
            try
            {
                GeraisAplicacao AplicacaoGerais = new GeraisAplicacao();
                vw_historico HistoricoAluno = new vw_historico();
                List<vw_disciplinas_historico> ListaHistoricoDisciplicas = new List<vw_disciplinas_historico>();
                alunos aluno;
                string qTab = HttpContext.Current.Request.Form["hQTab"];
                aluno = (alunos)Session[qTab + "Aluno"];

                HistoricoAluno = AplicacaoGerais.vw_historico(Convert.ToInt32(aluno.idaluno), Convert.ToInt32(txtIdTurmaAlunoNew.Value));
                ListaHistoricoDisciplicas = AplicacaoGerais.ListaDisciplinas_historico(Convert.ToInt32(aluno.idaluno), Convert.ToInt32(txtIdTurmaAlunoNew.Value));
                matricula_turma itemTurma = aluno.matricula_turma.Where(x => x.id_turma == HistoricoAluno.id_turma).SingleOrDefault();

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 260, 40);//estibulando o espaçamento das margens que queremos ===Antigo===(45, 40, 40, 40)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/Histórico Escolar Oficial.pdf"), FileMode.Create));
                PDFFooter pageHeaderFooter = new PDFFooter();
                pageHeaderFooter.Caminho = Server.MapPath("~");
                pageHeaderFooter.HistoricoAluno = HistoricoAluno;
                pageHeaderFooter.qIdTipoCurso = itemTurma.turmas.cursos.id_tipo_curso;
                writer.PageEvent = pageHeaderFooter;
                doc.Open();

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);

                //criando a variaveis
                PdfPTable table;
                float[] widths;
                PdfPCell cell;
                Phrase phrase;
                Paragraph p = new Paragraph();

                //estipulando o alinhamneto
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                var line1 = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                p.Add(new Chunk(line1));
                doc.Add(p);

                //Aqui é uma nova tabela de 2 Colunas ========================================================
                if (ListaHistoricoDisciplicas.Count < 3)
                {
                    table = new PdfPTable(2);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 260f, 260f };
                    table.SetWidths(widths);
                }
                else
                {
                    table = new PdfPTable(1);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 520f };
                    table.SetWidths(widths);
                }

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("RG: ", font_Verdana_10_Bold));
                p.Add(new Chunk(HistoricoAluno.numero_documento + " " + HistoricoAluno.orgao_expedidor, font_Arialn_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Border = Rectangle.NO_BORDER;
                //cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("DATA DE NASCIMENTO: ", font_Verdana_10_Bold));
                p.Add(new Chunk(String.Format("{0:dd/MM/yyyy}", HistoricoAluno.data_nascimento), font_Arialn_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Border = Rectangle.NO_BORDER;
                //cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("LOCAL DE NASCIMENTO: ", font_Verdana_10_Bold));
                p.Add(new Chunk(HistoricoAluno.cidade_nasc, font_Arialn_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Border = Rectangle.NO_BORDER;
                //cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("NACIONALIDADE: ", font_Verdana_10_Bold));
                p.Add(new Chunk(HistoricoAluno.pais_nasc, font_Arialn_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Border = Rectangle.NO_BORDER;
                //cell.PaddingBottom = 12f;
                table.AddCell(cell);

                doc.Add(table);


                //var phrase = new Phrase();
                //phrase.Add(new Chunk("RG: ", font_Verdana_10_Bold));
                //phrase.Add(new Chunk(HistoricoAluno.numero_documento + " " + HistoricoAluno.orgao_expedidor , font_Arialn_10_Normal));
                //doc.Add(phrase);
                //doc.Add(new Chunk("\n"));
                //doc.Add(new Chunk(" ", font_Arialn_6_Normal));

                //phrase = new Phrase();
                //phrase.Add(new Chunk("DATA DE NASCIMENTO: ", font_Verdana_10_Bold));
                //phrase.Add(new Chunk(String.Format("{0:dd/MM/yyyy}", HistoricoAluno.data_nascimento), font_Arialn_10_Normal));
                //doc.Add(phrase);
                //doc.Add(new Chunk("\n"));
                //doc.Add(new Chunk(" ", font_Arialn_6_Normal));

                //phrase = new Phrase();
                //phrase.Add(new Chunk("LOCAL DE NASCIMENTO: ", font_Verdana_10_Bold));
                //phrase.Add(new Chunk(HistoricoAluno.cidade_nasc, font_Arialn_10_Normal));
                //doc.Add(phrase);
                //doc.Add(new Chunk("\n"));
                //doc.Add(new Chunk(" ", font_Arialn_6_Normal));

                //phrase = new Phrase();
                //phrase.Add(new Chunk("NACIONALIDADE: ", font_Verdana_10_Bold));
                //phrase.Add(new Chunk(HistoricoAluno.pais_nasc, font_Arialn_10_Normal));
                //doc.Add(phrase);
                //doc.Add(new Chunk(" ", font_Arialn_6_Normal));

                p = new Paragraph();
                //estipulando o alinhamneto
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(line1));
                doc.Add(p);
                doc.Add(new Chunk(" ", font_Arialn_6_Normal));

                phrase = new Phrase();
                phrase.Add(new Chunk("DATA DE INGRESSO: ", font_Verdana_10_Bold));
                phrase.Add(new Chunk(String.Format("{0:dd/MM/yyyy}", HistoricoAluno.data_inicio), font_Arialn_10_Normal));
                if (pageHeaderFooter.qIdTipoCurso == 3)
                {
                    phrase.Add(new Chunk("                                   DATA DO TRABALHO FINAL: ", font_Verdana_10_Bold));
                }
                else
                {
                    phrase.Add(new Chunk("                                   DATA DA DEFESA DE DISSERTAÇÃO: ", font_Verdana_10_Bold));
                }
                if (HistoricoAluno.data_defesa != null)
                {
                    phrase.Add(new Chunk(String.Format("{0:dd/MM/yyyy}", HistoricoAluno.data_defesa), font_Arialn_10_Normal));
                }
                else
                {
                    phrase.Add(new Chunk("xx/xx/xxxx", font_Arialn_10_Normal));
                }
                doc.Add(phrase);
                doc.Add(new Chunk("\n"));
                doc.Add(new Chunk(" ", font_Arialn_6_Normal));

                if (pageHeaderFooter.qIdTipoCurso == 1)
                {
                    phrase = new Phrase();
                    phrase.Add(new Chunk("ÁREA DE CONCENTRAÇÃO: ", font_Verdana_10_Bold));
                    phrase.Add(new Chunk(HistoricoAluno.nome_area_concentracao, font_Arialn_10_Normal));
                    doc.Add(phrase);
                    doc.Add(new Chunk("\n"));
                    doc.Add(new Chunk(" ", font_Arialn_6_Normal));

                    phrase = new Phrase();
                    phrase.Add(new Chunk("PROFICIÊNCIA EM LÍNGUA ESTRANGEIRA: ", font_Verdana_10_Bold));
                    phrase.Add(new Chunk("Inglês - " + HistoricoAluno.Resultado_Proficiencia, font_Arialn_10_Normal));
                    doc.Add(phrase);
                    doc.Add(new Chunk("\n"));
                    doc.Add(new Chunk(" ", font_Arialn_6_Normal));
                }
                
                phrase = new Phrase();
                phrase.Add(new Chunk("ORIENTADOR: ", font_Verdana_10_Bold));
                if (HistoricoAluno.nome_orientador != null)
                {
                    phrase.Add(new Chunk(HistoricoAluno.reduzido + " " + HistoricoAluno.nome_orientador, font_Arialn_10_Normal));
                }
                else
                {
                    phrase.Add(new Chunk("xxxxxxxxxxxxxxxxxxxxx", font_Arialn_10_Normal));
                }
                doc.Add(phrase);
                doc.Add(new Chunk("\n"));
                doc.Add(new Chunk(" ", font_Arialn_6_Normal));

                phrase = new Phrase();
                if (pageHeaderFooter.qIdTipoCurso == 3)
                {
                    phrase.Add(new Chunk("TÍTULO DO TRABALHO FINAL: ", font_Verdana_10_Bold));
                }
                else
                {
                    phrase.Add(new Chunk("TÍTULO DA DISSERTAÇÃO: ", font_Verdana_10_Bold));
                }
                if (HistoricoAluno.titulo != null)
                {
                    phrase.Add(new Chunk(HistoricoAluno.titulo, font_Arialn_10_Normal));
                }
                else
                {
                    phrase.Add(new Chunk("xxxxxxxxxxxxxxxxxxxxx", font_Arialn_10_Normal));
                }
                doc.Add(phrase);
                doc.Add(new Chunk("\n"));
                doc.Add(new Chunk(" ", font_Arialn_6_Normal));

                table = new PdfPTable(6);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                widths = new float[] {25f, 90f, 30f, 30f, 30f, 25f };
                table.SetWidths(widths);

                cell = new PdfPCell(new Paragraph("CÓD", font_Verdana_9_Bold));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.FixedHeight = 25f;
                //cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph("DISCIPLINAS/ATIVIDADES", font_Verdana_9_Bold));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph("PERÍODO", font_Verdana_9_Bold));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph("CONCEITOS", font_Verdana_9_Bold));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.FixedHeight = 25f;
                table.AddCell(cell);

                if (pageHeaderFooter.qIdTipoCurso == 3)
                {
                    cell = new PdfPCell(new Paragraph("FREQ.", font_Verdana_9_Bold));
                }
                else
                {
                    cell = new PdfPCell(new Paragraph("CRÉDITOS", font_Verdana_9_Bold));
                }
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.FixedHeight = 25f;
                cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph("CARGA HORÁRIA", font_Verdana_9_Bold));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.FixedHeight = 25f;
                table.AddCell(cell);

                string dDataFim;

                List<matricula_oferecimento> listaOferecimento = new List<matricula_oferecimento>();
                listaOferecimento = aluno.matricula_oferecimento.Where(x => x.id_turma == Convert.ToInt32(itemTurma.id_turma)).ToList();

                matricula_oferecimento itemOferecimento = listaOferecimento.Where(x => x.oferecimentos.disciplinas.codigo == ListaHistoricoDisciplicas[0].codigo).FirstOrDefault();

                for (int i = 0; i < ListaHistoricoDisciplicas.Count; i++)
                {
                    //if (ListaHistoricoDisciplicas[i].Conceito.ToUpper() == "D" || ListaHistoricoDisciplicas[i].Conceito.ToUpper() == "E" || ListaHistoricoDisciplicas[i].Conceito.ToUpper() == "I")
                    //{
                    //    continue;
                    //}
                    //A view já não traz os conceitos D E e I
                    if (pageHeaderFooter.qIdTipoCurso == 3 && ListaHistoricoDisciplicas[i].codigo == "DIS 001")
                    {
                        cell = new PdfPCell(new Paragraph("MON 001", font_Verdana_9_Normal));
                        cell.SetLeading(11, 0); //espaçamento entre linhas
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Paragraph("Apresentação Monografia", font_Verdana_9_Normal));
                        cell.SetLeading(11, 0);
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.FixedHeight = 30f;
                        table.AddCell(cell);
                    }
                    else
                    {
                        cell = new PdfPCell(new Paragraph(ListaHistoricoDisciplicas[i].codigo, font_Verdana_9_Normal));
                        cell.SetLeading(11, 0); //espaçamento entre linhas
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Paragraph(ListaHistoricoDisciplicas[i].nome_disciplina, font_Verdana_9_Normal));
                        cell.SetLeading(11, 0);
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.FixedHeight = 30f;
                        table.AddCell(cell);
                    }
                    
                    if (String.Format("{0:dd/MM/yyyy}", ListaHistoricoDisciplicas[i].fim_aula) == "01/01/1900")
                    {
                        dDataFim = "";
                    }
                    else
                    {
                        dDataFim = String.Format("{0:dd/MM/yyyy}", ListaHistoricoDisciplicas[i].fim_aula);
                    }

                    cell = new PdfPCell(new Paragraph(String.Format("{0:dd/MM/yyyy}", ListaHistoricoDisciplicas[i].inicio_aula) + "\n" + dDataFim, font_Verdana_9_Normal));
                    cell.SetLeading(10, 0); //espaçamento entre linhas
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.FixedHeight = 30f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(ListaHistoricoDisciplicas[i].Conceito, font_Verdana_9_Normal));
                    cell.SetLeading(11, 0); //espaçamento entre linhas
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.FixedHeight = 30f;
                    table.AddCell(cell);

                    string qFreq;
                    if (pageHeaderFooter.qIdTipoCurso == 3)
                    {
                        qFreq = itemOferecimento.oferecimentos.presenca.Where(x => x.id_aluno == aluno.idaluno).Count() == 0 ? "0,00%" : ((itemOferecimento.oferecimentos.presenca.Where(x => x.id_aluno == aluno.idaluno && x.presente == true).Count()) / (itemOferecimento.oferecimentos.presenca.Where(x => x.id_aluno == aluno.idaluno).Count() * 0.01)).ToString("0.##") + "%";
                        cell = new PdfPCell(new Paragraph(qFreq, font_Verdana_9_Normal));
                    }
                    else
                    {
                        cell = new PdfPCell(new Paragraph(Convert.ToString(ListaHistoricoDisciplicas[i].creditos), font_Verdana_9_Normal));
                    }
                    
                    cell.SetLeading(11, 0); //espaçamento entre linhas
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.FixedHeight = 30f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(Convert.ToString(ListaHistoricoDisciplicas[i].carga_horaria), font_Verdana_9_Normal));
                    cell.SetLeading(11, 0); //espaçamento entre linhas
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.FixedHeight = 30f;
                    table.AddCell(cell);
                }

                cell = new PdfPCell(new Paragraph("TOTAL:...................................................................", font_Verdana_10_Normal));
                cell.Colspan = 4;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.FixedHeight = 25f;
                table.AddCell(cell);

                if (pageHeaderFooter.qIdTipoCurso == 3)
                {
                    cell = new PdfPCell(new Paragraph("", font_Verdana_10_Bold));
                }
                else
                {
                    cell = new PdfPCell(new Paragraph(Convert.ToString(ListaHistoricoDisciplicas.Sum(x => Convert.ToInt32(x.creditos))), font_Verdana_10_Bold));
                }
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(Convert.ToString(ListaHistoricoDisciplicas.Sum(x => x.carga_horaria)), font_Verdana_10_Bold));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.FixedHeight = 25f;
                table.AddCell(cell);

                if (pageHeaderFooter.qIdTipoCurso == 3)
                {
                    
                    string sAux="";
                    p = new Paragraph();
                    p.Add(new Chunk("Convenções:\n", font_Verdana_9_Bold));

                    //Solicitado alteração pela Andreia no dia 07/07/2022 com autorização do prof Eduardo.
                    //sAux = "A - Indica excelência de desempenho e concede o crédito – 8,5 a 10,0\n";
                    //sAux = sAux + "B - Indica desempenho superior e concede crédito – 7,0 a 8,4\n";
                    //sAux = sAux + "C - Indica satisfatoriedade mínima e concede o crédito-5,0 a 6,9\n";
                    //sAux = sAux + "D - Indica insuficiência de desempenho.Não concede o crédito e reprova o aluno – 3,0 a 4,9\n";
                    //sAux = sAux + "E - Indica insuficiência de desempenho.Não concede o crédito e reprova o aluno até 2,9\n";
                    //sAux = sAux + "I - Indica cumprimento incompleto das obrigações discentes. Deve ser resolvido no prazo máximo de 30 dias – 0\n\n";
                    //Solicitado alteração pela Andreia no dia 07/07/2022 com autorização do prof Eduardo.
                    sAux = "CONVENÇÕES: A= Excelente, B= Bom, C= Regular, D= Reprovado, 1= Incompleto, AP= Aprovado\nConceitos: A,B, C e AP com direito a crédito.\n\n";

                    p.Add(new Chunk(sAux, font_Verdana_9_Normal));

                    sAux = "Carga Horária:\n";
                    p.Add(new Chunk(sAux, font_Verdana_9_Bold));

                    //Solicitado alteração pela Andreia no dia 07/07/2022 com autorização do prof Eduardo.
                    //sAux = "400 horas/aula – Presencial\n160 horas – Exercícios extra-classe \n\n";
                    //Solicitado alteração pela Andreia no dia 07/07/2022 com autorização do prof Eduardo.
                    sAux = "400 horas/aula – Presencial\n\n";
                    p.Add(new Chunk(sAux, font_Verdana_9_Normal));

                    sAux = "Este curso cumpre todos os requisitos legais exigidos pela Resolução CNE/CES No. 01, de 03/04/2001.";
                    p.Add(new Chunk(sAux, font_Verdana_9_Bold));

                    cell.AddElement(p);
                    cell.Colspan = 6;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    //cell.FixedHeight = 255f;
                    cell.FixedHeight = 155f;
                    table.AddCell(cell);
                }
                else
                {
                    cell = new PdfPCell(new Paragraph("CONVENÇÕES: A= Excelente, B= Bom, C= Regular, D= Reprovado, I= Incompleto, AP= Aprovado\n\n                         Conceitos: A,B, C e AP com direito a crédito.\n\nCARGA HORÁRIA: 01 CRÉDITO = 10 horas", font_Verdana_9_Normal));
                    cell.Colspan = 6;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.FixedHeight = 55f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Nº MÍNIMO DE CRÉDITOS = 48\n\n                                          Disciplinas: 36\n                                          Exame de Qualificação: 06\n                                          Defesa de Qualificação: 06", font_Verdana_9_Normal));
                    cell.Colspan = 6;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.FixedHeight = 55f;
                    table.AddCell(cell);
                }
                
                doc.Add(table);

                phrase = new Phrase();
                
                if (txtDataDiploma.Value != "")
                {
                    if (pageHeaderFooter.qIdTipoCurso == 3)
                    {
                        phrase.Add(new Chunk("Data de expedição do CERTIFICADO: ", font_Verdana_9_Normal));
                    }
                    else
                    {
                        phrase.Add(new Chunk("Data de expedição do diploma: ", font_Verdana_9_Normal));
                    }
                    phrase.Add(new Chunk(String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(txtDataDiploma.Value)), font_Verdana_9_Normal));
                }
                else if (txtObsHistorico2.Value.Trim() == "")
                {
                    if (pageHeaderFooter.qIdTipoCurso == 3)
                    {
                        phrase.Add(new Chunk("Data de expedição do CERTIFICADO: ", font_Verdana_9_Normal));
                        phrase.Add(new Chunk("Aluno não concluinte.", font_Verdana_9_Normal));
                    }
                    else
                    {
                        phrase.Add(new Chunk("Data de expedição do diploma: ", font_Verdana_9_Normal));
                        phrase.Add(new Chunk("Aluno não concluinte.", font_Verdana_9_Normal));
                    }
                    
                }

                if (txtObsHistorico2.Value.Trim() != "")
                {
                    doc.Add(phrase);
                    doc.Add(new Chunk("\n"));
                    doc.Add(new Chunk(" ", font_Arialn_6_Normal));

                    phrase = new Phrase();

                    phrase.Add(new Chunk(txtObsHistorico2.Value.Trim(), font_Verdana_9_Normal));
                }

                doc.Add(phrase);
                doc.Add(new Chunk("\n\n"));
                doc.Add(new Chunk(" ", font_Arialn_6_Normal));

                phrase = new Phrase();
                phrase.Add(new Chunk("São Paulo, " + DateTime.Today.Day.ToString() + " de " + DateTime.Now.ToString("MMMM") + " de " + DateTime.Today.Year.ToString(), font_Verdana_9_Normal));
                p = new Paragraph();
                //estipulando o alinhamneto
                p.Alignment = Element.ALIGN_RIGHT;
                p.Clear();
                p.Add(phrase);
                doc.Add(p);
                doc.Add(new Chunk("\n"));

                phrase = new Phrase();
                phrase.Add(new Chunk("__________________________________________\n", font_Verdana_9_Normal));
                phrase.Add(new Chunk("Prof. Dr. Eduardo Luiz Machado\n", font_Verdana_9_Normal));
                //Alteração Longuinho 26/11/2021
                //phrase.Add(new Chunk("Coordenador de Ensino Tecnológico", font_Verdana_9_Normal));
                phrase.Add(new Chunk("Diretor Técnico em Ensino Tecnológico", font_Verdana_9_Normal));
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(phrase);
                doc.Add(p);

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

                if (File.Exists(Server.MapPath("~/doctos/Histórico Escolar.pdf")))
                {
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/Histórico Escolar Oficial_" + txtMatriculaAluno.Value + ".pdf")));
                    Response.WriteFile(Server.MapPath("~/doctos/Histórico Escolar Oficial.pdf"));
                    Response.Flush();
                    Response.End();
                }

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão do Histório Escolar";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-success');", true);
            }
        }

        public class PDFFooter : PdfPageEventHelper
        {

            public string Caminho;
            public int qIdTipoCurso;
            //public string DataMEC;
            //public string DataDOU;

            public vw_historico HistoricoAluno = new vw_historico();
            // write on top of document
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                base.OnOpenDocument(writer, document);
            }

            // write on start of each page
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);
                Paragraph p = new Paragraph();

                iTextSharp.text.Image imgCabecalho = iTextSharp.text.Image.GetInstance(Caminho + "/img/ipt.gif");
                imgCabecalho.SetAbsolutePosition(20, 755);
                imgCabecalho.ScalePercent(65);

                //imgCabecalho = iTextSharp.text.Image.GetInstance(Caminho + "/img/rodape_sapiens.png");
                //imgCabecalho.SetAbsolutePosition(60, 755);
                //imgCabecalho.ScalePercent(20);

                p.Add(new Chunk(imgCabecalho, 0, 0, true));

                PdfPTable tabFot = new PdfPTable(new float[] { 1f });
                //tabFot.SpacingAfter = 10.0!
                PdfPCell cell = default(PdfPCell);
                tabFot.TotalWidth = 530f; //Aqui se seta se a tabela irá ficar mais a esquerda ou a direita
                //cell = new PdfPCell(p)

                cell = new PdfPCell();
                cell.AddElement(p);

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_8_Bold = new Font(_bfVerdanaNormal, 8, Font.BOLD);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Verdana_7_Bold = new Font(_bfVerdanaNormal, 7, Font.BOLD);
                Font font_Verdana_7_Normal = new Font(_bfVerdanaNormal, 7, Font.NORMAL);
                //criando a variavel para paragrafo
                p = new Paragraph();
                //etipulando o alinhamneto
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                //p.Add(new Chunk("Coordenadoria de Ensino Tecnológico - CET", font_Verdana_10_Bold));
                p.Add(new Chunk(" ", font_Verdana_10_Bold));
                cell.AddElement(p);

                p = new Paragraph();
                //etipulando o alinhamneto
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                if (qIdTipoCurso == 5)//3=especialização
                {
                    p.Add(new Chunk("EDUCAÇÃO CORPORATIVA EM " + HistoricoAluno.nome_do_curso.ToUpper(), font_Verdana_10_Bold));
                }
                else if (qIdTipoCurso == 3)//3=especialização
                {
                    p.Add(new Chunk("ESPECIALIZAÇÃO EM " + HistoricoAluno.nome_do_curso.ToUpper(), font_Verdana_10_Bold));
                }
                else if (qIdTipoCurso == 2)//2=MBA INTERNACIONAL
                {
                    p.Add(new Chunk("MBA INTERNACIONAL EM " + HistoricoAluno.nome_do_curso.ToUpper(), font_Verdana_10_Bold));
                }
                else
                {
                    p.Add(new Chunk("MESTRADO PROFISSIONAL EM " + HistoricoAluno.nome_do_curso.ToUpper(), font_Verdana_10_Bold));
                }
                cell.AddElement(p);

                p = new Paragraph();
                //etipulando o alinhamneto
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(" ", font_Arialn_10_Bold));
                cell.AddElement(p);

                p = new Paragraph();
                //etipulando o alinhamneto
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                if (qIdTipoCurso == 3)//3=especialização
                {
                    p.Add(new Chunk("O Instituto de Pesquisas Tecnológicas do Estado de São Paulo S. A. – IPT, nos termos da legislação federal em vigor, de acordo com as normas do Conselho Nacional de Educação, Resolução CNE/CES Nº 01 de 03/04/2001, confere.\n Numero Capes: nº 33083010", font_Verdana_8_Normal));
                }
                else
                {
                    p.Add(new Chunk("Curso reconhecido de acordo com o disposto na portaria MEC nº " + HistoricoAluno.portaria_mec + " de", font_Verdana_10_Bold));
                }
                cell.AddElement(p);

                if (qIdTipoCurso != 3)//3=especialização
                {
                    p = new Paragraph();
                    //etipulando o alinhamneto
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Clear();
                    if (HistoricoAluno.portaria_mec == "656")
                    {
                        p.Add(new Chunk(String.Format("{0:dd/MM/yyyy}", HistoricoAluno.data_portaria_mec) + ", DOU de " + String.Format("{0:dd/MM/yyyy}", HistoricoAluno.data_diario_oficial) + " e republicada no DOU de 27/07/2017", font_Verdana_10_Bold));
                    }
                    else
                    {
                        p.Add(new Chunk(String.Format("{0:dd/MM/yyyy}", HistoricoAluno.data_portaria_mec) + ", DOU de " + String.Format("{0:dd/MM/yyyy}", HistoricoAluno.data_diario_oficial) + ".", font_Verdana_10_Bold));
                    }

                    cell.AddElement(p);

                }

                if (qIdTipoCurso != 3)//3=especialização
                {
                    p = new Paragraph();
                    //etipulando o alinhamneto
                    p.Alignment = Element.ALIGN_CENTER;
                    p.Clear();
                    p.Add(new Chunk(" ", font_Verdana_10_Bold));
                    cell.AddElement(p);
                }

                p = new Paragraph();
                //etipulando o alinhamneto
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk("HISTÓRICO ESCOLAR", font_Verdana_10_Bold));
                cell.AddElement(p);

                p = new Paragraph();
                //etipulando o alinhamneto
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                var line1 = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                p.Add(new Chunk(line1));
                cell.AddElement(p);


                //Alterado para que o nome e a matrícula possam ficar na mesma linha independente do tamanho do nome

                //string sPace;
                //if (HistoricoAluno.nome.Length < 93)
                //{
                //    sPace = " ".PadRight(93 - HistoricoAluno.nome.Length);
                //}
                //else
                //{
                //    sPace = " ";
                //}

                //p = new Paragraph();
                ////etipulando o alinhamneto
                //p.Alignment = Element.ALIGN_LEFT;
                //p.Clear();
                //p.Add(new Chunk("NOME: " + HistoricoAluno.nome + sPace + "MATRÍCULA: " + HistoricoAluno.idaluno, font_Verdana_10_Bold));
                //cell.AddElement(p);

                //Alterado para que o nome e a matrícula possam ficar na mesma linha independente do tamanho do nome





                cell.Border = Rectangle.NO_BORDER;
                //tabFot.AddCell(cell); tabFot.WriteSelectedRows(0, -1, 45, document.Top, writer.DirectContent);
                tabFot.AddCell(cell);
                tabFot.WriteSelectedRows(0, -1, 40, (document.PageSize.Height - 40), writer.DirectContent);


                //Novo trecho para que o nome e a matrícula possam ficar na mesma linha independente do tamanho do nome

                PdfPTable table = new PdfPTable(4);

                table.TotalWidth = 520f;
                table.LockedWidth = true;
                
                float[] widths = new float[] { 20f, 161f, 31f, 18f };
                table.SetWidths(widths);

                cell = new PdfPCell(new Paragraph("NOME: ", font_Verdana_10_Bold));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                //cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(HistoricoAluno.nome, font_Verdana_10_Bold));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph("MATRÍCULA: ", font_Verdana_10_Bold));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(HistoricoAluno.idaluno.ToString(), font_Verdana_10_Bold));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);
                //(não sei, não sei, esquerda-direita, altura,não sei)
                table.WriteSelectedRows(0, -1, 42, (document.PageSize.Height - 239), writer.DirectContent);

                //document.Add(table);


                //tabFot.WriteSelectedRows(0, -1, 150, document.Top, writer.DirectContent)

                //document.Add(cell);

                //PdfPTable table;
                //table = new PdfPTable(3);

                //cell = new PdfPCell(new Phrase("Cell with colspan 3"));
                //cell.Colspan = 3;
                //table.AddCell(cell);
                //// now we add a cell with rowspan 2
                //cell = new PdfPCell(new Phrase("Cell with rowspan 2"));
                //cell.Rowspan = 2;
                //table.AddCell(cell);
                //// we add the four remaining cells with addCell()
                //table.AddCell("row 1; cell 1");
                //table.AddCell("row 1; cell 2");
                //table.AddCell("row 2; cell 1");
                //table.AddCell("row 2; cell 2");






                //table = new PdfPTable(2);
                //table.SpacingAfter = 9F;
                //p = new Paragraph();
                ////etipulando o alinhamneto
                //p.Alignment = Element.ALIGN_CENTER;
                //p.Clear();
                //p.Add(new Chunk("NOME: " + HistoricoAluno.nome, font_Arialn_10_Bold));
                //cell.AddElement(p);
                //cell.Colspan = 2;
                //table.AddCell(cell);

                //p = new Paragraph();
                ////etipulando o alinhamneto
                //p.Alignment = Element.ALIGN_CENTER;
                //p.Clear();
                //p.Add(new Chunk("MATRÍCULA: " + HistoricoAluno.idaluno, font_Arialn_10_Bold));
                //cell.AddElement(p);
                //cell.Colspan = 1;


            }

            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);
                var _bfArialNarrowNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_8_Italic = new Font(_bfVerdanaNormal, 8, Font.ITALIC);
                PdfPTable tabFot = new PdfPTable(new float[] { 1f });
                PdfPCell cell = default(PdfPCell);
                Paragraph p = new Paragraph();
                tabFot.TotalWidth = 520f;
                //p.Add(new Chunk("Data da Impressão: " + Strings.Format(Today, "dd/MM/yyyy") + Strings.Space(60) + "Página " + writer.PageNumber, font_Arialn_10_Normal));
                iTextSharp.text.Image imgRodape = iTextSharp.text.Image.GetInstance(Caminho + "/img/rodape_sapiens.png");
                imgRodape.SetAbsolutePosition(100, 300);
                imgRodape.ScalePercent(18);

                //p.Alignment = Element.ALIGN_RIGHT;

                p.Add(new Chunk(imgRodape, 0, 0, true));

                //p.Add(new Chunk("Instituto de Pesquisas Tecnológicas do Estado de São Paulo S/A - IPT \r\n", font_Verdana_8_Italic));
                //p.Add(new Chunk("Av. Prof. Almeida Prado, 532 - Cidade Universitária - Butantã - 05508-901 - São Paulo - SP  \r\n", font_Verdana_8_Italic));
                //p.Add(new Chunk("Caixa Postal 0141 - 01064-970 - São Paulo - SP - Tel.: (11)3767-4084, 3767-4624 - sapiens.ipt.br  \r\n", font_Verdana_8_Italic));
                cell = new PdfPCell(p);
                //cell.AddElement(p);

                //p = new Paragraph();
                ////etipulando o alinhamneto
                //p.Alignment = Element.ALIGN_CENTER;
                //p.Clear();
                //p.Add(new Chunk("Av. Prof. Almeida Prado, 532 - Cidade Universitária - Butantã - 05508-901 - São Paulo - SP" , font_Verdana_8_Italic));
                //cell.AddElement(p);

                //p = new Paragraph();
                ////etipulando o alinhamneto
                //p.Alignment = Element.ALIGN_CENTER;
                //p.Clear();
                //p.Add(new Chunk("Caixa Postal 0141 - 01064-970 - São Paulo - SP Tel.: (11)3767-4084, 3767-4624 - sapiens.ipt.br", font_Verdana_8_Italic));
                //cell.AddElement(p);

                //cell = new PdfPCell(p);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabFot.AddCell(cell);
                tabFot.WriteSelectedRows(0, -1, 30, document.Bottom, writer.DirectContent);
            }

            //write on close of document
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);
            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimeHistorico();
        }

        private void ImprimeHistorico()
        {
            try
            {

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 80, 18);//estibulando o espaçamento das margens que queremos ===Antigo===(40, 40, 40, 80)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/Histórico Escolar.pdf"), FileMode.Create));
                doc.Open();

                iTextSharp.text.Image imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/img/ipt.gif"));
                imgCabecalho.SetAbsolutePosition(20, 755);
                imgCabecalho.ScalePercent(65);
                doc.Add(imgCabecalho);

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_11_Bold = new Font(_bfVerdanaNormal, 11, Font.BOLD);
                Font font_Verdana_11_Normal = new Font(_bfVerdanaNormal, 11, Font.NORMAL);
                Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Verdana_8_Bold = new Font(_bfVerdanaNormal, 8, Font.BOLD);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Verdana_7_Bold = new Font(_bfVerdanaNormal, 7, Font.BOLD);
                Font font_Verdana_7_Normal = new Font(_bfVerdanaNormal, 7, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_12_Bold = new Font(_bfArialNarrowNormal, 12, Font.BOLD);
                Font font_Arialn_12_Normal = new Font(_bfArialNarrowNormal, 12, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_7_Bold = new Font(_bfArialNarrowNormal, 7, Font.BOLD);
                Font font_Arialn_7_Normal = new Font(_bfArialNarrowNormal, 7, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);
                Color FontColor_Cinza = new Color(204, 204, 204);
                Color FontColor_CinzaClaro = new Color(229, 229, 229);

                //criando a variavel para paragrafo
                Paragraph paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Alignment = Element.ALIGN_CENTER;
                paragrafo.Clear();
                paragrafo.Add(new Chunk("HISTÓRICO ESCOLAR", font_Arialn_14_Bold));
                doc.Add(paragrafo);

                iTextSharp.text.Table objTable = new iTextSharp.text.Table(2);

                objTable.Padding = 0;
                objTable.Spacing = 0;
                objTable.Border = Rectangle.NO_BORDER;
                objTable.Width = 100;
                objTable.DeleteAllRows();
                Single[] ColumnWidth = new Single[] { 15, 85 };
                objTable.Widths = ColumnWidth;
                objTable.DefaultCellBorder = 0;

                ////////////////////////////////
                Cell objCell = new Cell();
                objCell.Border = Rectangle.TOP_BORDER;
                objCell.BorderWidth = 1;
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("Matrícula"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Border = Rectangle.TOP_BORDER;
                objCell.BorderWidth = 1;
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(lblNumeroMatricula.Text), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("Nome"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(lblTituloNomeAluno.Text), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("CPF"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(txtCPFAluno.Value), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                doc.Add(objTable);
                /////////////////////////////////////////

                /////////////////////////////////////////
                objTable = new iTextSharp.text.Table(4);
                ColumnWidth = new Single[] { 20, 30, 15, 35 };
                objTable.Widths = ColumnWidth;
                objTable.Padding = 0;
                objTable.Spacing = 0;
                objTable.Border = Rectangle.NO_BORDER;
                objTable.Width = 100;
                objTable.DeleteAllRows();
                objTable.DefaultCellBorder = 0;

                objCell = new Cell();
                objCell.Border = Rectangle.TOP_BORDER;
                objCell.BorderWidth = 1;
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("Código Turma"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Border = Rectangle.TOP_BORDER;
                objCell.BorderWidth = 1;
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(txtCodTurmaAlunoNew.Value), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Border = Rectangle.TOP_BORDER;
                objCell.BorderWidth = 1;
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("Tipo Curso"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Border = Rectangle.TOP_BORDER;
                objCell.BorderWidth = 1;
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(txtTipoCursoAlunoNew.Value), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("Período"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(txtQuadrimestreAlunoNew.Value), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("Curso"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(txtCursoAlunoNew.Value), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("Data de Início"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(txtDataInicioCursoAlunoNew.Value), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("Data de Fim"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(txtDataFimCursoAlunoNew.Value), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("Área de Concentração"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(txtAreaConcentracaoAlunoNew.Value), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("Data de Término"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(txtDataTerminoCursoAlunoNew.Value), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                doc.Add(objTable);


                ///////////////////////////////////////////////////////////////////////////////////
                alunos item;
                string qTab = HttpContext.Current.Request.Form["hQTab"];
                item = (alunos)Session[qTab + "Aluno"];
                List<matricula_oferecimento> lista = new List<matricula_oferecimento>();

                lista = item.matricula_oferecimento.Where(x => x.id_turma == Convert.ToInt32(txtIdTurmaAlunoNew.Value)).ToList();

                if (lista.Count > 0)
                {
                    
                    //PDFClass objPDF = new PDFClass();
                    //objPDF.GridViewFontSize = 7;
                    //objPDF.GridViewPaddingCell = 1;
                    //objPDF.GridViewSpacingCell = 1;
                    //objPDF.ColumnWidth = new Single[] { 8, 8, 8, 47, 7, 7, 7, 8, 0, 0 };

                    float[] widths;
                    Paragraph p;
                    PdfPCell cell;
                    PdfPTable table;

                    //Aqui é uma nova tabela de 1 Colunas ========================================================
                    table = new PdfPTable(1);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 520f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(" ", font_Verdana_7_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    doc.Add(table);

                    //Aqui é uma nova tabela de 1 Colunas ========================================================
                    table = new PdfPTable(1);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 520f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("RELAÇÂO DE DISCIPLINAS", font_Verdana_7_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 6f;
                    table.AddCell(cell);

                    doc.Add(table);

                    //Aqui é uma nova tabela de 8 Colunas ========================================================
                    table = new PdfPTable(8);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 9f, 8f, 8f, 46f, 7f, 7f, 7f, 8f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Início", font_Verdana_7_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 6f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 3
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Período", font_Verdana_7_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 8f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 4
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Disciplina", font_Verdana_7_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 8f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 5
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Nome", font_Verdana_7_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 8f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 4
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Duração", font_Verdana_7_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 8f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 4
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Freq.", font_Verdana_7_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 8f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 4
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Conceito", font_Verdana_7_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 8f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 4
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Resultado", font_Verdana_7_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 8f;
                    table.AddCell(cell);

                    int i = 0;

                    foreach (var elemento in lista)
                    {
                        i++;
                        //Aqui se desenha a Coluna 1
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(String.Format("{0:dd/MM/yyyy}", elemento.oferecimentos.datas_aulas.Min(x => x.data_aula)), font_Verdana_7_Normal));
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        if (i % 2 != 0)
                        {
                            cell.BorderColor = Color.WHITE;
                            cell.BackgroundColor = Color.WHITE;
                        }
                        else
                        {
                            cell.BorderColor = FontColor_CinzaClaro;
                            cell.BackgroundColor = FontColor_CinzaClaro;
                        }
                        cell.Border = Rectangle.BOX;
                        cell.PaddingBottom = 8f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 2
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(elemento.oferecimentos.quadrimestre, font_Verdana_7_Normal));
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Border = Rectangle.BOX;
                        if (i % 2 != 0)
                        {
                            cell.BorderColor = Color.WHITE;
                            cell.BackgroundColor = Color.WHITE;
                        }
                        else
                        {
                            cell.BorderColor = FontColor_CinzaClaro;
                            cell.BackgroundColor = FontColor_CinzaClaro;
                        }
                        cell.PaddingBottom = 8f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 3
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(elemento.oferecimentos.disciplinas.codigo, font_Verdana_7_Normal));
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Border = Rectangle.BOX;
                        if (i % 2 != 0)
                        {
                            cell.BorderColor = Color.WHITE;
                            cell.BackgroundColor = Color.WHITE;
                        }
                        else
                        {
                            cell.BorderColor = FontColor_CinzaClaro;
                            cell.BackgroundColor = FontColor_CinzaClaro;
                        }
                        cell.PaddingBottom = 8f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 4
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(elemento.oferecimentos.disciplinas.nome, font_Verdana_7_Normal));
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Border = Rectangle.BOX;
                        if (i % 2 != 0)
                        {
                            cell.BorderColor = Color.WHITE;
                            cell.BackgroundColor = Color.WHITE;
                        }
                        else
                        {
                            cell.BorderColor = FontColor_CinzaClaro;
                            cell.BackgroundColor = FontColor_CinzaClaro;
                        }
                        cell.PaddingBottom = 8f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 5
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(elemento.oferecimentos.carga_horaria.ToString() + " h", font_Verdana_7_Normal));
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Border = Rectangle.BOX;
                        if (i % 2 != 0)
                        {
                            cell.BorderColor = Color.WHITE;
                            cell.BackgroundColor = Color.WHITE;
                        }
                        else
                        {
                            cell.BorderColor = FontColor_CinzaClaro;
                            cell.BackgroundColor = FontColor_CinzaClaro;
                        }
                        cell.PaddingBottom = 8f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 6
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk((elemento.oferecimentos.presenca.Where(x => x.id_aluno == item.idaluno).Count() == 0) ? "0,00%" : ((elemento.oferecimentos.presenca.Where(x => x.id_aluno == item.idaluno && x.presente == true).Count()) / (elemento.oferecimentos.presenca.Where(x => x.id_aluno == item.idaluno).Count() * 0.01)).ToString("0.##") + "%", font_Verdana_7_Normal));
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Border = Rectangle.BOX;
                        if (i % 2 != 0)
                        {
                            cell.BorderColor = Color.WHITE;
                            cell.BackgroundColor = Color.WHITE;
                        }
                        else
                        {
                            cell.BorderColor = FontColor_CinzaClaro;
                            cell.BackgroundColor = FontColor_CinzaClaro;
                        }
                        cell.PaddingBottom = 8f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 7
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk((elemento.oferecimentos.notas.Where(x => x.id_aluno == item.idaluno).FirstOrDefault() == null) ? "" : (elemento.oferecimentos.notas.Where(x => x.id_aluno == item.idaluno).Select(x => x.conceito).FirstOrDefault() == null) ? "" : elemento.oferecimentos.notas.Where(x => x.id_aluno == item.idaluno).Select(x => x.conceito).FirstOrDefault(), font_Verdana_7_Normal));
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Border = Rectangle.BOX;
                        if (i % 2 != 0)
                        {
                            cell.BorderColor = Color.WHITE;
                            cell.BackgroundColor = Color.WHITE;
                        }
                        else
                        {
                            cell.BorderColor = FontColor_CinzaClaro;
                            cell.BackgroundColor = FontColor_CinzaClaro;
                        }
                        cell.PaddingBottom = 8f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 8
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk((elemento.oferecimentos.notas.Where(x => x.id_aluno == item.idaluno).FirstOrDefault() == null) ? "" : (elemento.oferecimentos.notas.Where(x => x.id_aluno == item.idaluno).FirstOrDefault().conceitos_de_aprovacao == null) ? "" : elemento.oferecimentos.notas.Where(x => x.id_aluno == item.idaluno).FirstOrDefault().conceitos_de_aprovacao.descricao, font_Verdana_7_Normal));
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Border = Rectangle.BOX;
                        if (i % 2 != 0)
                        {
                            cell.BorderColor = Color.WHITE;
                            cell.BackgroundColor = Color.WHITE;
                        }
                        else
                        {
                            cell.BorderColor = FontColor_CinzaClaro;
                            cell.BackgroundColor = FontColor_CinzaClaro;
                        }
                        cell.PaddingBottom = 8f;
                        table.AddCell(cell);
                    }

                    doc.Add(table);
                    
                }

                /////////////////////////////////////////////////////////////////////////////////
                //if (!msgSemResultadosHistorico.Visible)
                //{
                //    PDFClass objPDF = new PDFClass();
                //    objPDF.GridViewFontSize = 7;
                //    objPDF.GridViewPaddingCell = 1;
                //    objPDF.GridViewSpacingCell = 1;
                //    objPDF.ColumnWidth = new Single[] { 8, 8, 8, 47, 7, 7, 7, 8, 0, 0 };
                //    doc.Add(objPDF.FuncGridViewToPdfTable(grdHistoricoAluno));
                //}

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

                if (File.Exists(Server.MapPath("~/doctos/Histórico Escolar.pdf")))
                {
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/Histórico Escolar_" + txtMatriculaAluno.Value + ".pdf")));
                    Response.WriteFile(Server.MapPath("~/doctos/Histórico Escolar.pdf"));
                    Response.Flush();
                    Response.End();
                }

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão do Histório Escolar";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-danger');", true);
            }
        }

        protected void btnImprimirSituacaoMatricula_Click(object sender, EventArgs e)
        {
            ImprimeSituacaoMatricula();
        }

        private void ImprimeSituacaoMatricula()
        {
            try
            {

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 80, 18);//estibulando o espaçamento das margens que queremos ===Antigo===(40, 40, 40, 80)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/SituacaoMatricula.pdf"), FileMode.Create));
                doc.Open();

                iTextSharp.text.Image imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/img/ipt.gif"));
                imgCabecalho.SetAbsolutePosition(20, 755);
                imgCabecalho.ScalePercent(65);
                doc.Add(imgCabecalho);

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_11_Bold = new Font(_bfVerdanaNormal, 11, Font.BOLD);
                Font font_Verdana_11_Normal = new Font(_bfVerdanaNormal, 11, Font.NORMAL);
                Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Verdana_8_Bold = new Font(_bfVerdanaNormal, 8, Font.BOLD);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Verdana_7_Bold = new Font(_bfVerdanaNormal, 7, Font.BOLD);
                Font font_Verdana_7_Normal = new Font(_bfVerdanaNormal, 7, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_12_Bold = new Font(_bfArialNarrowNormal, 12, Font.BOLD);
                Font font_Arialn_12_Normal = new Font(_bfArialNarrowNormal, 12, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_7_Bold = new Font(_bfArialNarrowNormal, 7, Font.BOLD);
                Font font_Arialn_7_Normal = new Font(_bfArialNarrowNormal, 7, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);
                Color FontColor_Cinza = new Color(204, 204, 204);
                Color FontColor_CinzaClaro = new Color(229, 229, 229);

                //criando a variavel para paragrafo
                Paragraph paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Alignment = Element.ALIGN_CENTER;
                paragrafo.Clear();
                paragrafo.Add(new Chunk("SITUAÇÃO DA MATRÍCULA DO ALUNO", font_Arialn_14_Bold));
                doc.Add(paragrafo);

                iTextSharp.text.Table objTable = new iTextSharp.text.Table(2);

                objTable.Padding = 0;
                objTable.Spacing = 0;
                objTable.Border = Rectangle.NO_BORDER;
                objTable.Width = 100;
                objTable.DeleteAllRows();
                Single[] ColumnWidth = new Single[] { 15, 85 };
                objTable.Widths = ColumnWidth;
                objTable.DefaultCellBorder = 0;

                ////////////////////////////////
                Cell objCell = new Cell();
                objCell.Border = Rectangle.TOP_BORDER;
                objCell.BorderWidth = 1;
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("Matrícula"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Border = Rectangle.TOP_BORDER;
                objCell.BorderWidth = 1;
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(lblNumeroMatricula.Text), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("Nome"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(lblTituloNomeAluno.Text), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("CPF"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(txtCPFAluno.Value), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                doc.Add(objTable);
                /////////////////////////////////////////

                /////////////////////////////////////////
                objTable = new iTextSharp.text.Table(4);
                ColumnWidth = new Single[] { 20, 30, 15, 35 };
                objTable.Widths = ColumnWidth;
                objTable.Padding = 0;
                objTable.Spacing = 0;
                objTable.Border = Rectangle.NO_BORDER;
                objTable.Width = 100;
                objTable.DeleteAllRows();
                objTable.DefaultCellBorder = 0;

                objCell = new Cell();
                objCell.Border = Rectangle.TOP_BORDER;
                objCell.BorderWidth = 1;
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("Código Turma"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Border = Rectangle.TOP_BORDER;
                objCell.BorderWidth = 1;
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(txtCodTurmaAlunoNew.Value), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Border = Rectangle.TOP_BORDER;
                objCell.BorderWidth = 1;
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("Tipo Curso"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Border = Rectangle.TOP_BORDER;
                objCell.BorderWidth = 1;
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(txtTipoCursoAlunoNew.Value), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("Período"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(txtQuadrimestreAlunoNew.Value), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("Curso"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(txtCursoAlunoNew.Value), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("Data de Início"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(txtDataInicioCursoAlunoNew.Value), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("Data de Fim"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(txtDataFimCursoAlunoNew.Value), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("Data Término"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(txtDataTerminoCursoAlunoNew.Value), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("Área de Concentração"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(txtAreaConcentracaoAlunoNew.Value), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                doc.Add(objTable);

                ///////////////////////////////////////////////////////////////////////////////////
                alunos item;
                string qTab = HttpContext.Current.Request.Form["hQTab"];
                item = (alunos)Session[qTab + "Aluno"];
                List<matricula_turma> lista = new List<matricula_turma>();

                lista = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(txtIdTurmaAlunoNew.Value)).ToList();

                if (lista.Count > 0)
                {

                    //PDFClass objPDF = new PDFClass();
                    //objPDF.GridViewFontSize = 7;
                    //objPDF.GridViewPaddingCell = 1;
                    //objPDF.GridViewSpacingCell = 1;
                    //objPDF.ColumnWidth = new Single[] { 8, 8, 8, 47, 7, 7, 7, 8, 0, 0 };

                    float[] widths;
                    Paragraph p;
                    PdfPCell cell;
                    PdfPTable table;

                    //Aqui é uma nova tabela de 1 Colunas ========================================================
                    table = new PdfPTable(1);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 520f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(" ", font_Verdana_7_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    doc.Add(table);

                    //Aqui é uma nova tabela de 1 Colunas ========================================================
                    table = new PdfPTable(1);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 520f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("HISTÓRICO DA MATRÍCULA", font_Verdana_7_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 6f;
                    table.AddCell(cell);

                    doc.Add(table);

                    //Aqui é uma nova tabela de 4 Colunas ========================================================
                    table = new PdfPTable(4);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 130f, 130f, 130f, 130f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Status", font_Verdana_7_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 6f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Situação", font_Verdana_7_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 8f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 3
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Data Início", font_Verdana_7_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 8f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 4
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Data Fim", font_Verdana_7_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 8f;
                    table.AddCell(cell);

                    int i = 0;

                    foreach (var elemento in lista)
                    {
                        foreach (var elemento2 in elemento.historico_matricula_turma.OrderBy(x=> x.data_inicio).ThenBy(y=> y.data_fim))
                        {
                            i++;
                            //Aqui se desenha a Coluna 1
                            cell = new PdfPCell();
                            p = new Paragraph();
                            p.Add(new Chunk(elemento2.status, font_Verdana_7_Normal));
                            cell = new PdfPCell(p);
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            if (i % 2 != 0)
                            {
                                cell.BorderColor = Color.WHITE;
                                cell.BackgroundColor = Color.WHITE;
                            }
                            else
                            {
                                cell.BorderColor = FontColor_CinzaClaro;
                                cell.BackgroundColor = FontColor_CinzaClaro;
                            }
                            cell.Border = Rectangle.BOX;
                            cell.PaddingBottom = 8f;
                            table.AddCell(cell);

                            //Aqui se desenha a Coluna 2
                            cell = new PdfPCell();
                            p = new Paragraph();
                            p.Add(new Chunk(elemento2.situacao, font_Verdana_7_Normal));
                            cell = new PdfPCell(p);
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell.Border = Rectangle.BOX;
                            if (i % 2 != 0)
                            {
                                cell.BorderColor = Color.WHITE;
                                cell.BackgroundColor = Color.WHITE;
                            }
                            else
                            {
                                cell.BorderColor = FontColor_CinzaClaro;
                                cell.BackgroundColor = FontColor_CinzaClaro;
                            }
                            cell.PaddingBottom = 8f;
                            table.AddCell(cell);

                            //Aqui se desenha a Coluna 3
                            cell = new PdfPCell();
                            p = new Paragraph();
                            p.Add(new Chunk(String.Format("{0:dd/MM/yyyy}", elemento2.data_inicio), font_Verdana_7_Normal));
                            cell = new PdfPCell(p);
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell.Border = Rectangle.BOX;
                            if (i % 2 != 0)
                            {
                                cell.BorderColor = Color.WHITE;
                                cell.BackgroundColor = Color.WHITE;
                            }
                            else
                            {
                                cell.BorderColor = FontColor_CinzaClaro;
                                cell.BackgroundColor = FontColor_CinzaClaro;
                            }
                            cell.PaddingBottom = 8f;
                            table.AddCell(cell);

                            //Aqui se desenha a Coluna 4
                            cell = new PdfPCell();
                            p = new Paragraph();
                            p.Add(new Chunk(String.Format("{0:dd/MM/yyyy}", elemento2.data_fim), font_Verdana_7_Normal));
                            cell = new PdfPCell(p);
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell.Border = Rectangle.BOX;
                            if (i % 2 != 0)
                            {
                                cell.BorderColor = Color.WHITE;
                                cell.BackgroundColor = Color.WHITE;
                            }
                            else
                            {
                                cell.BorderColor = FontColor_CinzaClaro;
                                cell.BackgroundColor = FontColor_CinzaClaro;
                            }
                            cell.PaddingBottom = 8f;
                            table.AddCell(cell);

                        }
                    }

                    doc.Add(table);

                }

                /////////////////////////////////////////////////////////////////////////////////
                //if (!msgSemResultadosHistorico.Visible)
                //{
                //    PDFClass objPDF = new PDFClass();
                //    objPDF.GridViewFontSize = 7;
                //    objPDF.GridViewPaddingCell = 1;
                //    objPDF.GridViewSpacingCell = 1;
                //    objPDF.ColumnWidth = new Single[] { 8, 8, 8, 47, 7, 7, 7, 8, 0, 0 };
                //    doc.Add(objPDF.FuncGridViewToPdfTable(grdHistoricoAluno));
                //}

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

                if (File.Exists(Server.MapPath("~/doctos/SituacaoMatricula.pdf")))
                {
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/SituacaoMatricula.pdf")));
                    Response.WriteFile(Server.MapPath("~/doctos/SituacaoMatricula.pdf"));
                    Response.Flush();
                    Response.End();
                }

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão da Situação da Matrícula";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-danger');", true);
            }
        }

        protected void btnImprimirCertificado_Click(object sender, EventArgs e)
        {
            ImprimeCertificado();
        }

        private void ImprimeCertificado()
        {
            try
            {

                Document doc = new Document(PageSize.A4.Rotate());//criando e estipulando o tipo da folha usada
                doc.SetMargins(70, 130, 200, 10);//estibulando o espaçamento das margens que queremos ===Antigo===(70, 130, 230, 10) //Alterado pedido andreia 17/09/2020
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/Certificado.pdf"), FileMode.Create));
                doc.Open();

                //Alterado pedido andreia 17/09/2020
                iTextSharp.text.Image imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/img/img-cabecalho/cabecalho_ipt_tarja.png"));
                imgCabecalho.SetAbsolutePosition(0, 430);
                imgCabecalho.ScalePercent(97);
                doc.Add(imgCabecalho);
                //Alterado pedido andreia 17/09/2020

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfTahoma = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\tahoma.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_11_Bold = new Font(_bfVerdanaNormal, 11, Font.BOLD);
                Font font_Verdana_11_Normal = new Font(_bfVerdanaNormal, 11, Font.NORMAL);
                Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Verdana_8_Bold = new Font(_bfVerdanaNormal, 8, Font.BOLD);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Verdana_7_Bold = new Font(_bfVerdanaNormal, 7, Font.BOLD);
                Font font_Verdana_7_Normal = new Font(_bfVerdanaNormal, 7, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_14_Normal = new Font(_bfArialNarrowNormal, 14, Font.NORMAL);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_12_Bold = new Font(_bfArialNarrowNormal, 12, Font.BOLD);
                Font font_Arialn_12_Normal = new Font(_bfArialNarrowNormal, 12, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_7_Bold = new Font(_bfArialNarrowNormal, 7, Font.BOLD);
                Font font_Arialn_7_Normal = new Font(_bfArialNarrowNormal, 7, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);

                Font font_Tahoma_30_Bold = new Font(_bfTahoma, 30, Font.BOLD);
                Font font_Tahoma_15_Bold = new Font(_bfTahoma, 15, Font.BOLD);
                Font font_Tahoma_15_Normal = new Font(_bfTahoma, 15, Font.NORMAL);
                Font font_Tahoma_14_Bold = new Font(_bfTahoma, 14, Font.BOLD);
                Font font_Tahoma_14_Normal = new Font(_bfTahoma, 14, Font.NORMAL);
                Font font_Tahoma_13_Bold = new Font(_bfTahoma, 13, Font.BOLD);
                Font font_Tahoma_13_Normal = new Font(_bfTahoma, 13, Font.NORMAL);
                Font font_Tahoma_12_Bold = new Font(_bfTahoma, 12, Font.BOLD);
                Font font_Tahoma_12_Normal = new Font(_bfTahoma, 12, Font.NORMAL);
                Font font_Tahoma_11_Bold = new Font(_bfTahoma, 11, Font.BOLD);
                Font font_Tahoma_11_Normal = new Font(_bfTahoma, 11, Font.NORMAL);
                Font font_Tahoma_10_Bold = new Font(_bfTahoma, 10, Font.BOLD);
                Font font_Tahoma_10_Normal = new Font(_bfTahoma, 10, Font.NORMAL);

                Color FontColor_Cinza = new Color(204, 204, 204);
                Color FontColor_CinzaClaro = new Color(229, 229, 229);

                alunos item;
                string qTab = HttpContext.Current.Request.Form["hQTab"];
                item = (alunos)Session[qTab + "Aluno"];
                List<matricula_turma> lista = new List<matricula_turma>();

                lista = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(txtIdTurmaAlunoNew.Value)).ToList();

                //criando a variavel para paragrafo
                //Alterado pedido andreia 17/09/2020
                Paragraph paragrafo = new Paragraph();
                //estipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_CENTER;

                paragrafo.Add(new Chunk("CERTIFICADO\n\n ", font_Tahoma_30_Bold));

                doc.Add(paragrafo);
                //Alterado pedido andreia 17/09/2020

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_JUSTIFIED;

                paragrafo.Add(new Chunk("Certificamos que ", font_Tahoma_15_Normal));
                if (lista.ElementAt(0).alunos.sexo.ToUpper() == "M")
                {
                    paragrafo.Add(new Chunk("o ", font_Tahoma_14_Normal));
                    paragrafo.Add(new Chunk("Sr. " + item.nome.ToUpper(), font_Tahoma_15_Bold));
                }
                else
                {
                    paragrafo.Add(new Chunk("a ", font_Arialn_14_Normal));
                    paragrafo.Add(new Chunk("Sra. " + item.nome.ToUpper(), font_Tahoma_15_Bold));
                }

                string sRg;

                if (item.digito_num_documento != "" && item.digito_num_documento != null)
                {
                    sRg = item.numero_documento + "-" + item.digito_num_documento;
                }
                else
                {
                    sRg = item.numero_documento;
                }

                DateTime qData = lista.ElementAt(0).historico_matricula_turma.Where(x => x.situacao == "Titulado").FirstOrDefault().data_fim.Value;

                paragrafo.Add(new Chunk(", RG: " + sRg + " (" + item.orgao_expedidor + "), matrícula: " + item.idaluno + ", obteve o título de ", font_Tahoma_15_Normal));
                paragrafo.Add(new Chunk("MESTRE EM " + lista.ElementAt(0).turmas.cursos.nome.ToUpper(), font_Tahoma_15_Bold));
                if (lista.ElementAt(0).areas_concentracao != null)
                //if (lista.ElementAt(0).turmas.cursos.areas_concentracao.Count > 0)
                {
                    //paragrafo.Add(new Chunk(", com ênfase em " + lista.ElementAt(0).turmas.cursos.areas_concentracao.ElementAt(0).nome, font_Tahoma_15_Normal));
                    paragrafo.Add(new Chunk(", com ênfase em " + lista.ElementAt(0).areas_concentracao.nome, font_Tahoma_15_Normal));
                }
                paragrafo.Add(new Chunk(", em " + qData.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("PT-pt")) + ".\n\n", font_Tahoma_15_Normal));
                //paragrafo.Add(new Chunk("Informamos que o Mestrado Profissional em " + lista.ElementAt(0).turmas.cursos.nome + ", foi reconhecido, através da Portaria MEC n° " + lista.ElementAt(0).turmas.portaria_mec + " de " + String.Format("{0:dd/MM/yyyy}", lista.ElementAt(0).turmas.data_portaria_mec) + ", publicado no Diário Oficial da União de " + String.Format("{0:dd/MM/yyyy}", lista.ElementAt(0).turmas.data_diario_oficial) + ".\n\n\n", font_Tahoma_15_Normal));
                paragrafo.Add(new Chunk("Informamos que o Mestrado Profissional em " + lista.ElementAt(0).turmas.cursos.nome + ", foi reconhecido, através da Portaria MEC n° " + txtPortariaNumeroOficial.Value + " de " + txtPortariaDataOficial.Value + ", publicado no Diário Oficial da União de " + txtDouDataOficial.Value + ".\n\n\n", font_Tahoma_15_Normal));

                paragrafo.SetLeading(18, 0);

                //paragrafo.IndentationLeft = 20;
                //paragrafo.IndentationRight = 20;

                doc.Add(paragrafo);

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_RIGHT;

                paragrafo.Add(new Chunk("São Paulo, " + DateTime.Today.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("PT-pt")) + ".\n\n\n", font_Tahoma_13_Normal));
                doc.Add(paragrafo);

                //Alterado pedido andreia 17/09/2020
                paragrafo = new Paragraph();
                //estipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_CENTER;

                //paragrafo.Add(new Chunk("Coordenadoria de Ensino Tecnológico\n\n\n______________________________________\n" + lista.ElementAt(0).turmas.cursos.cursos_coordenadores.ElementAt(0).professores.nome + "\nCoordenador", font_Tahoma_11_Normal));
                //paragrafo.Add(new Chunk("Coordenadoria de Ensino Tecnológico\n\n\n______________________________________\nEduardo Luiz Machado\nCoordenador", font_Tahoma_11_Normal));
                paragrafo.Add(new Chunk("\n\n______________________________________\nProf. Dr. Eduardo Luiz Machado\nDiretor Técnico em Ensino Tecnológico", font_Tahoma_11_Normal));
                doc.Add(paragrafo);
                //Alterado pedido andreia 17/09/2020

                //Alterado pedido andreia 17/09/2020
                //imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/img/img-cabecalho/assinaturaEduardo.png"));
                //imgCabecalho.SetAbsolutePosition(280, 40);
                //imgCabecalho.ScalePercent(77);
                //doc.Add(imgCabecalho);
                //Alterado pedido andreia 17/09/2020

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

                if (File.Exists(Server.MapPath("~/doctos/Certificado.pdf")))
                {
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/Certificado.pdf")));
                    Response.WriteFile(Server.MapPath("~/doctos/Certificado.pdf"));
                    Response.Flush();
                    Response.End();
                }

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão do Certificado de Titulação";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-danger');", true);
            }
        }

        protected void btnImprimirCertificadoCurta_Click(object sender, EventArgs e)
        {
            ImprimeCertificadoCurta();
        }

        private void ImprimeCertificadoCurta()
        {
            try
            {

                Document doc = new Document(PageSize.A4.Rotate());//criando e estipulando o tipo da folha usada
                doc.SetMargins(70, 130, 200, 10);//estibulando o espaçamento das margens que queremos ===Antigo===(70, 130, 230, 10) //Alterado pedido andreia 17/09/2020
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/Certificado.pdf"), FileMode.Create));
                doc.Open();

                //Alterado pedido andreia 17/09/2020
                iTextSharp.text.Image imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/img/img-cabecalho/cabecalho_ipt_tarja.png"));
                imgCabecalho.SetAbsolutePosition(0, 430);
                imgCabecalho.ScalePercent(97);
                doc.Add(imgCabecalho);
                //Alterado pedido andreia 17/09/2020

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfTahoma = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\tahoma.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_11_Bold = new Font(_bfVerdanaNormal, 11, Font.BOLD);
                Font font_Verdana_11_Normal = new Font(_bfVerdanaNormal, 11, Font.NORMAL);
                Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Verdana_8_Bold = new Font(_bfVerdanaNormal, 8, Font.BOLD);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Verdana_7_Bold = new Font(_bfVerdanaNormal, 7, Font.BOLD);
                Font font_Verdana_7_Normal = new Font(_bfVerdanaNormal, 7, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_14_Normal = new Font(_bfArialNarrowNormal, 14, Font.NORMAL);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_12_Bold = new Font(_bfArialNarrowNormal, 12, Font.BOLD);
                Font font_Arialn_12_Normal = new Font(_bfArialNarrowNormal, 12, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_7_Bold = new Font(_bfArialNarrowNormal, 7, Font.BOLD);
                Font font_Arialn_7_Normal = new Font(_bfArialNarrowNormal, 7, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);

                Font font_Tahoma_30_Bold = new Font(_bfTahoma, 30, Font.BOLD);
                Font font_Tahoma_15_Bold = new Font(_bfTahoma, 15, Font.BOLD);
                Font font_Tahoma_15_Normal = new Font(_bfTahoma, 15, Font.NORMAL);
                Font font_Tahoma_14_Bold = new Font(_bfTahoma, 14, Font.BOLD);
                Font font_Tahoma_14_Normal = new Font(_bfTahoma, 14, Font.NORMAL);
                Font font_Tahoma_13_Bold = new Font(_bfTahoma, 13, Font.BOLD);
                Font font_Tahoma_13_Normal = new Font(_bfTahoma, 13, Font.NORMAL);
                Font font_Tahoma_12_Bold = new Font(_bfTahoma, 12, Font.BOLD);
                Font font_Tahoma_12_Normal = new Font(_bfTahoma, 12, Font.NORMAL);
                Font font_Tahoma_11_Bold = new Font(_bfTahoma, 11, Font.BOLD);
                Font font_Tahoma_11_Normal = new Font(_bfTahoma, 11, Font.NORMAL);
                Font font_Tahoma_10_Bold = new Font(_bfTahoma, 10, Font.BOLD);
                Font font_Tahoma_10_Normal = new Font(_bfTahoma, 10, Font.NORMAL);

                Color FontColor_Cinza = new Color(204, 204, 204);
                Color FontColor_CinzaClaro = new Color(229, 229, 229);

                alunos item;
                string qTab = HttpContext.Current.Request.Form["hQTab"];
                item = (alunos)Session[qTab + "Aluno"];
                List<matricula_turma> lista = new List<matricula_turma>();

                lista = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(txtIdTurmaAlunoNew.Value)).ToList();

                //criando a variavel para paragrafo
                Paragraph paragrafo = new Paragraph();
                //estipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_CENTER;

                paragrafo.Add(new Chunk("CERTIFICADO\n\n ", font_Tahoma_30_Bold));

                doc.Add(paragrafo);

                paragrafo = new Paragraph();
                //estipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_RIGHT;

                paragrafo.Add(new Chunk(txtNumeroRegistroCurta.Value + "\n", font_Tahoma_15_Normal));

                doc.Add(paragrafo);
                //Alterado pedido andreia 17/09/2020

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_LEFT;

                paragrafo.Add(new Chunk("PELO PRESENTE CERTIFICAMOS QUE: ", font_Tahoma_15_Normal));
                //if (lista.ElementAt(0).alunos.sexo.ToUpper() == "M")
                //{
                //    paragrafo.Add(new Chunk("o ", font_Tahoma_14_Normal));
                //    paragrafo.Add(new Chunk("Sr. " + item.nome.ToUpper(), font_Tahoma_15_Bold));
                //}
                //else
                //{
                //    paragrafo.Add(new Chunk("a ", font_Arialn_14_Normal));
                //    paragrafo.Add(new Chunk("Sra. " + item.nome.ToUpper(), font_Tahoma_15_Bold));
                //}

                paragrafo.Add(new Chunk(lista.ElementAt(0).alunos.nome.ToUpper() + "\n\n", font_Tahoma_15_Bold));

                paragrafo.Add(new Chunk("PARTICIPOU DO CURSO: ", font_Tahoma_15_Normal));

                paragrafo.Add(new Chunk(lista.ElementAt(0).turmas.cursos.nome.ToUpper() + "\n\n", font_Tahoma_15_Bold));

                paragrafo.Add(new Chunk("DATA: ", font_Tahoma_15_Normal));

                paragrafo.Add(new Chunk(lista.ElementAt(0).turmas.data_inicio.Value.ToString("dd/MM/yyyy") + " a " + lista.ElementAt(0).turmas.data_fim.Value.ToString("dd/MM/yyyy") + "\n", font_Tahoma_15_Bold));

                doc.Add(paragrafo);

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_RIGHT;

                paragrafo.Add(new Chunk("Carga horária: ", font_Tahoma_15_Normal));

                paragrafo.Add(new Chunk(lista.ElementAt(0).turmas.carga_horaria + "h\n\n", font_Tahoma_15_Bold));

                paragrafo.Add(new Chunk("São Paulo, " + DateTime.Today.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("PT-pt")) + ".\n\n\n\n\n\n\n\n", font_Tahoma_13_Normal));
                doc.Add(paragrafo);


                //Aqui é uma nova tabela de 4 Colunas ========================================================
                PdfPTable table = new PdfPTable(2);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                float[] widths = new float[] { 260f, 260f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                PdfPCell cell = new PdfPCell();
                Paragraph p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("____________________________________\n\n", font_Verdana_8_Bold));
                p.Add(new Chunk(lista.ElementAt(0).turmas.cursos.cursos_coordenadores.ElementAt(0).professores.titulacao.reduzido + " " + lista.ElementAt(0).turmas.cursos.cursos_coordenadores.ElementAt(0).professores.nome + "\n\n", font_Verdana_9_Bold));
                p.Add(new Chunk("Coordenador do Curso", font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderColor = FontColor_Cinza;
                //cell.BackgroundColor = FontColor_Cinza;
                //cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("____________________________________\n\n", font_Verdana_8_Bold));
                p.Add(new Chunk("Prof. Dr. Eduardo Luiz Machado\n\n", font_Verdana_9_Bold));
                p.Add(new Chunk("Diretor Técnico em Ensino Tecnológico", font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderColor = FontColor_Cinza;
                //cell.BackgroundColor = FontColor_Cinza;
                //cell.PaddingBottom = 6f;
                table.AddCell(cell);

                doc.Add(table);


                //paragrafo = new Paragraph();
                ////estipulando o alinhamneto
                //paragrafo.Clear();
                //paragrafo.Alignment = Element.ALIGN_CENTER;

                //paragrafo.Add(new Chunk("\n\n\n______________________________________                                                 ______________________________________\n", font_Tahoma_11_Normal));
                //paragrafo.Add(new Chunk("Coordenadoria de Ensino Tecnológico\n\n\n______________________________________\nEduardo Luiz Machado\nCoordenador", font_Tahoma_11_Normal));
                //doc.Add(paragrafo);
                //Alterado pedido andreia 17/09/2020

                //Alterado pedido andreia 17/09/2020
                //imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/img/img-cabecalho/assinaturaEduardo.png"));
                //imgCabecalho.SetAbsolutePosition(280, 40);
                //imgCabecalho.ScalePercent(77);
                //doc.Add(imgCabecalho);
                //Alterado pedido andreia 17/09/2020

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

                if (File.Exists(Server.MapPath("~/doctos/Certificado.pdf")))
                {
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/Certificado.pdf")));
                    Response.WriteFile(Server.MapPath("~/doctos/Certificado.pdf"));
                    Response.Flush();
                    Response.End();
                }

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão do Certificado de Titulação";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-danger');", true);
            }
        }

        protected void btnImprimirCertificadoEspecializacao_Click(object sender, EventArgs e)
        {
            string sAux = "";
            if (txtNumeroRegistroEspecializacao.Value.Trim() == "")
            {
                sAux = "Preencher o número do Certificado. <br/><br/>";
            }
            if (txtCargaHorariaEspecializacao.Value.Trim() == "")
            {
                sAux = sAux + "Preencher a Carga horária do curso. <br/><br/>";
            }
            if (txtDataEspecializacao.Value.Trim() == "")
            {
                sAux = sAux + "Preencher a Data de conclusão do curso. <br/><br/>";
            }
            if (txtCoordenadorEspecializacao.Value.Trim() == "")
            {
                sAux = sAux + "Preencher nome do Coordenador do curso. <br/><br/>";
            }
            if (!optCertificadoComMascara.Checked && !optCertificadoSemMascara.Checked)
            {
                sAux = sAux + "Selecionar o Tipo de Certificado. <br/><br/>";
            }

            if (sAux != "")
            {
                lblMensagem.Text = sAux;
                lblTituloMensagem.Text = "Atenção";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-warning');", true);
                return;
            }


            ImprimeCertificadoEspecializacao();
        }

        private void ImprimeCertificadoEspecializacao()
        {
            try
            {

                Document doc = new Document(PageSize.A4.Rotate());//criando e estipulando o tipo da folha usada
                doc.SetMargins(70, 130, 200, 10);//estibulando o espaçamento das margens que queremos ===Antigo===(70, 130, 230, 10) //Alterado pedido andreia 17/09/2020
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/Certificado.pdf"), FileMode.Create));
                doc.Open();

                //criando a variavel para paragrafo
                Paragraph paragrafo = new Paragraph();

                //Alterado pedido andreia 17/09/2020
                if (optCertificadoSemMascara.Checked)
                {
                    iTextSharp.text.Image imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/img/img-cabecalho/cabecalho_ipt_tarja.png"));
                    imgCabecalho.SetAbsolutePosition(0, 430);
                    imgCabecalho.ScalePercent(97);
                    doc.Add(imgCabecalho);
                }
                
                
                //Alterado pedido andreia 17/09/2020

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfTahoma = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\tahoma.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_11_Bold = new Font(_bfVerdanaNormal, 11, Font.BOLD);
                Font font_Verdana_11_Normal = new Font(_bfVerdanaNormal, 11, Font.NORMAL);
                Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Verdana_8_Bold = new Font(_bfVerdanaNormal, 8, Font.BOLD);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Verdana_7_Bold = new Font(_bfVerdanaNormal, 7, Font.BOLD);
                Font font_Verdana_7_Normal = new Font(_bfVerdanaNormal, 7, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_14_Normal = new Font(_bfArialNarrowNormal, 14, Font.NORMAL);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_12_Bold = new Font(_bfArialNarrowNormal, 12, Font.BOLD);
                Font font_Arialn_12_Normal = new Font(_bfArialNarrowNormal, 12, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_7_Bold = new Font(_bfArialNarrowNormal, 7, Font.BOLD);
                Font font_Arialn_7_Normal = new Font(_bfArialNarrowNormal, 7, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);

                Font font_Tahoma_30_Bold = new Font(_bfTahoma, 30, Font.BOLD);
                Font font_Tahoma_15_Bold = new Font(_bfTahoma, 15, Font.BOLD);
                Font font_Tahoma_15_Normal = new Font(_bfTahoma, 15, Font.NORMAL);
                Font font_Tahoma_14_Bold = new Font(_bfTahoma, 14, Font.BOLD);
                Font font_Tahoma_14_Normal = new Font(_bfTahoma, 14, Font.NORMAL);
                Font font_Tahoma_13_Bold = new Font(_bfTahoma, 13, Font.BOLD);
                Font font_Tahoma_13_Normal = new Font(_bfTahoma, 13, Font.NORMAL);
                Font font_Tahoma_12_Bold = new Font(_bfTahoma, 12, Font.BOLD);
                Font font_Tahoma_12_Normal = new Font(_bfTahoma, 12, Font.NORMAL);
                Font font_Tahoma_11_Bold = new Font(_bfTahoma, 11, Font.BOLD);
                Font font_Tahoma_11_Normal = new Font(_bfTahoma, 11, Font.NORMAL);
                Font font_Tahoma_10_Bold = new Font(_bfTahoma, 10, Font.BOLD);
                Font font_Tahoma_10_Normal = new Font(_bfTahoma, 10, Font.NORMAL);

                Color FontColor_Cinza = new Color(204, 204, 204);
                Color FontColor_CinzaClaro = new Color(229, 229, 229);

                alunos item;
                string qTab = HttpContext.Current.Request.Form["hQTab"];
                item = (alunos)Session[qTab + "Aluno"];
                List<matricula_turma> lista = new List<matricula_turma>();

                lista = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(txtIdTurmaAlunoNew.Value)).ToList();

                if (optCertificadoSemMascara.Checked)
                {
                    //estipulando o alinhamneto
                    paragrafo.Clear();
                    paragrafo.Alignment = Element.ALIGN_CENTER;

                    paragrafo.Add(new Chunk("Certificado ", font_Tahoma_30_Bold));

                    doc.Add(paragrafo);
                }
               
                paragrafo = new Paragraph();
                //estipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_RIGHT;

                paragrafo.Add(new Chunk(txtNumeroRegistroEspecializacao.Value + "\n\n", font_Tahoma_15_Normal));

                doc.Add(paragrafo);
                //Alterado pedido andreia 17/09/2020

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_JUSTIFIED_ALL;

                paragrafo.Add(new Chunk("O Instituto de Pesquisas Tecnológicas do Estado de São Paulo S. A. - IPT, nos termos da legislação federal em vigor, \nde acordo com as normas do Conselho Nacional de Educação, Resolução CNE/CES Nº 01 de 06/04/2018, confere a\n\n", font_Tahoma_12_Normal));
                //if (lista.ElementAt(0).alunos.sexo.ToUpper() == "M")
                //{
                //    paragrafo.Add(new Chunk("o ", font_Tahoma_14_Normal));
                //    paragrafo.Add(new Chunk("Sr. " + item.nome.ToUpper(), font_Tahoma_15_Bold));
                //}
                //else
                //{
                //    paragrafo.Add(new Chunk("a ", font_Arialn_14_Normal));
                //    paragrafo.Add(new Chunk("Sra. " + item.nome.ToUpper(), font_Tahoma_15_Bold));
                //}
                doc.Add(paragrafo);

                paragrafo = new Paragraph();
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_CENTER;

                paragrafo.Add(new Chunk(lista.ElementAt(0).alunos.nome.ToUpper() + "\n\n", font_Tahoma_15_Bold));

                doc.Add(paragrafo);

                paragrafo = new Paragraph();
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_LEFT;

                paragrafo.Add(new Chunk("o presente certificado de Pós-Graduação \"Lato Sensu\", no curso\n\n", font_Tahoma_12_Normal));

                doc.Add(paragrafo);

                paragrafo = new Paragraph();
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_CENTER;

                paragrafo.Add(new Chunk(lista.ElementAt(0).turmas.cursos.nome.ToUpper() + "\n\n", font_Tahoma_15_Bold));

                doc.Add(paragrafo);

                //=======================

                PdfPTable table = new PdfPTable(2);
                table.TotalWidth = 640f;
                table.LockedWidth = true;
                float[] widths = new float[] { 320f, 320f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                PdfPCell cell = new PdfPCell();
                Paragraph p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                p.Add(new Chunk("Carga horária: ", font_Tahoma_12_Normal));

                if (txtCargaHorariaEspecializacao.Value.Trim() != "")
                {
                    p.Add(new Chunk(txtCargaHorariaEspecializacao.Value.Trim() + "h\n\n", font_Tahoma_12_Normal));
                }
                else
                {
                    p.Add(new Chunk(lista.ElementAt(0).turmas.carga_horaria + "h\n\n", font_Tahoma_12_Normal));
                }

                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderColor = FontColor_Cinza;
                //cell.BackgroundColor = FontColor_Cinza;
                //cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2 =====================================
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_RIGHT;
                p.Add(new Chunk("Concluído em " + txtDataEspecializacao.Value.Trim(), font_Tahoma_12_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderColor = FontColor_Cinza;
                //cell.BackgroundColor = FontColor_Cinza;
                //cell.PaddingBottom = 6f;
                table.AddCell(cell);

                doc.Add(table);

                //==========================================================

                paragrafo = new Paragraph();
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_CENTER;

                paragrafo.Add(new Chunk("\n", font_Tahoma_12_Bold));

                doc.Add(paragrafo);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 640f;
                table.LockedWidth = true;

                widths = new float[] { 640f };
                table.SetWidths(widths);

                table.SpacingAfter = 60f;

                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_RIGHT;
                p.Add(new Chunk("São Paulo, " + DateTime.Today.Day + " de " + dtfi.GetMonthName(DateTime.Today.Month) + " de " + DateTime.Today.Year, font_Verdana_12_Normal));
                p.SpacingAfter = 70f;
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //if (txtCoordenadorEspecializacao.Value.Trim() != "")
                //{
                //    paragrafo.Add(new Chunk(txtCoordenadorEspecializacao.Value.Trim().ToUpper() + " ", font_Tahoma_15_Normal));
                //}

                //paragrafo.Add(new Chunk("DATA: ", font_Tahoma_15_Normal));

                //if (txtDataEspecializacao.Value.Trim() != "")
                //{
                //    paragrafo.Add(new Chunk(txtDataEspecializacao.Value.Trim() + "\n", font_Tahoma_15_Bold));
                //}
                //else
                //{
                //    paragrafo.Add(new Chunk(lista.ElementAt(0).turmas.data_inicio.Value.ToString("dd/MM/yyyy") + " a " + lista.ElementAt(0).turmas.data_fim.Value.ToString("dd/MM/yyyy") + "\n", font_Tahoma_15_Bold));
                //}

                //doc.Add(paragrafo);

                paragrafo = new Paragraph();
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_CENTER;

                paragrafo.Add(new Chunk("\n", font_Tahoma_15_Bold));

                doc.Add(paragrafo);


                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(2);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 260f, 260f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("____________________________________\n\n", font_Verdana_8_Bold));
                //p.Add(new Chunk(lista.ElementAt(0).turmas.cursos.cursos_coordenadores.ElementAt(0).professores.titulacao.reduzido + " " + lista.ElementAt(0).turmas.cursos.cursos_coordenadores.ElementAt(0).professores.nome + "\n\n", font_Verdana_9_Bold));
                p.Add(new Chunk(txtCoordenadorEspecializacao.Value.Trim() + "\n\n", font_Verdana_9_Bold));
                p.Add(new Chunk("Coordenador do Curso", font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderColor = FontColor_Cinza;
                //cell.BackgroundColor = FontColor_Cinza;
                //cell.PaddingBottom = 6f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("____________________________________\n\n", font_Verdana_8_Bold));
                p.Add(new Chunk("Prof. Dr. Eduardo Luiz Machado\n\n", font_Verdana_9_Bold));
                p.Add(new Chunk("Diretor Técnico em Ensino Tecnológico", font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderColor = FontColor_Cinza;
                //cell.BackgroundColor = FontColor_Cinza;
                //cell.PaddingBottom = 6f;
                table.AddCell(cell);

                doc.Add(table);


                //paragrafo = new Paragraph();
                ////estipulando o alinhamneto
                //paragrafo.Clear();
                //paragrafo.Alignment = Element.ALIGN_CENTER;

                //paragrafo.Add(new Chunk("\n\n\n______________________________________                                                 ______________________________________\n", font_Tahoma_11_Normal));
                //paragrafo.Add(new Chunk("Coordenadoria de Ensino Tecnológico\n\n\n______________________________________\nEduardo Luiz Machado\nCoordenador", font_Tahoma_11_Normal));
                //doc.Add(paragrafo);
                //Alterado pedido andreia 17/09/2020

                //Alterado pedido andreia 17/09/2020
                //imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/img/img-cabecalho/assinaturaEduardo.png"));
                //imgCabecalho.SetAbsolutePosition(280, 40);
                //imgCabecalho.ScalePercent(77);
                //doc.Add(imgCabecalho);
                //Alterado pedido andreia 17/09/2020

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

                if (File.Exists(Server.MapPath("~/doctos/Certificado.pdf")))
                {
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/Certificado.pdf")));
                    Response.WriteFile(Server.MapPath("~/doctos/Certificado.pdf"));
                    Response.Flush();
                    Response.End();
                }

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão do Certificado de Titulação";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-danger');", true);
            }
        }

        protected void bntSalvarAluno_ServerClick(object sender, EventArgs e)
        {
            string qTab = HttpContext.Current.Request.Form["hQTab"];

            if (Session[qTab + "sNovoAluno"] == null)
            {
                CriaAluno();
            }
            else
            {
                AlteraAluno();
            }
        }

        protected void btnVoltar_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("cadAluno.aspx", true);
        }

        public class PDF_Cabec_Rodape_GeraEmentaPDF : PdfPageEventHelper
        {
            public string Caminho;
            public string qTipoCurso;
            //public string PortariaMEC; Aqui é uma comunicação com o meio externo
            //public string DataMEC;
            //public string DataDOU;

            // write on top of document
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                base.OnOpenDocument(writer, document);
            }

            // write on start of each page
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);


                //PdfPTable tabFot = new PdfPTable(new float[] { 1f });
                ////tabFot.SpacingAfter = 10.0!
                //PdfPCell cell = default(PdfPCell);
                //tabFot.TotalWidth = 530f; //Aqui se seta se a tabela irá ficar mais a esquerda ou a direita

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_8_Italic = new Font(_bfVerdanaNormal, 8, Font.ITALIC);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                var lineStrong = new iTextSharp.text.pdf.draw.LineSeparator(2.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                iTextSharp.text.Image imgIPT = iTextSharp.text.Image.GetInstance(Caminho + "/img/ipt.gif");
                Paragraph p = new Paragraph();
                PdfPCell cell = default(PdfPCell);

                //Aqui se determina que é uma tabela com duas colunas
                PdfPTable table = new PdfPTable(2);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                //Aqui se determina se determina o tamanho de cada coluna
                float[] widths = new float[] { 60f, 170f };
                table.SetWidths(widths);

                //Coluna 1
                p = new Paragraph();
                imgIPT.SetAbsolutePosition(20, 755);
                imgIPT.ScalePercent(65);
                p.Add(new Chunk(imgIPT, 0, 0, true));
                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(p);
                cell = new PdfPCell(new Paragraph(new Chunk(imgIPT, 0, 0, true)));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                //cell.Colspan = 2;
                table.AddCell(cell);

                //Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                //==== suprimido conforme e-mail de 12/04/2022 =============
                //p.Add(new Chunk("Instituto de Pesquisas Tecnológicas do Estado de São Paulo S/A - IPT \r\n", font_Verdana_8_Normal));
                //p.Add(new Chunk(" \r\n", font_Verdana_8_Normal));
                //p.Add(new Chunk("  \r\n", font_Verdana_8_Normal));
                //p.Add(new Chunk(qTipoCurso.ToUpper() + "  \r\n", font_Verdana_8_Normal));
                //==== suprimido conforme e-mail de 12/04/2022 =============
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);
                //(não sei, não sei, esquerda-direita, altura,não sei)
                table.WriteSelectedRows(0, -1, 42, (document.PageSize.Height - 40), writer.DirectContent);

                //Aqui é uma nova tabela
                PdfPTable table2 = new PdfPTable(1);
                table2.TotalWidth = 520f;
                table2.LockedWidth = true;

                widths = new float[] { 230f };
                table2.SetWidths(widths);

                //Aqui se desenha uma Linha
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(lineStrong));
                cell.AddElement(p);

                table2.AddCell(cell);
                //(não sei, não sei, esquerda-direita, altura,não sei)
                table2.WriteSelectedRows(0, -1, 42, (document.PageSize.Height - 100), writer.DirectContent);

            }

            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);
                var _bfArialNarrowNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_8_Italic = new Font(_bfVerdanaNormal, 8, Font.ITALIC);
                PdfPTable tabFot = new PdfPTable(new float[] { 1f });
                PdfPCell cell = default(PdfPCell);
                Paragraph p = new Paragraph();
                tabFot.TotalWidth = 520f;
                //p.Add(new Chunk("Data da Impressão: " + Strings.Format(Today, "dd/MM/yyyy") + Strings.Space(60) + "Página " + writer.PageNumber, font_Arialn_10_Normal));
                iTextSharp.text.Image imgRodape = iTextSharp.text.Image.GetInstance(Caminho + "/img/rodape_sapiens.png");
                imgRodape.SetAbsolutePosition(100, 300);
                imgRodape.ScalePercent(18);
                p.Add(new Chunk(imgRodape, 0, 0, true));

                //p.Alignment = Element.ALIGN_CENTER;
                //p.Add(new Chunk("Instituto de Pesquisas Tecnológicas do Estado de São Paulo S/A - IPT \r\n", font_Verdana_8_Italic));
                //p.Add(new Chunk("Av. Prof. Almeida Prado, 532 - Cidade Universitária - Butantã - 05508-901 - São Paulo - SP  \r\n", font_Verdana_8_Italic));
                //p.Add(new Chunk("Caixa Postal 0141 - 01064-970 - São Paulo - SP - Tel.: (11)3767-4084, 3767-4624 - sapiens.ipt.br  \r\n", font_Verdana_8_Italic));
                cell = new PdfPCell(p);
                //cell.AddElement(p);

                //cell = new PdfPCell(p);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabFot.AddCell(cell);
                tabFot.WriteSelectedRows(0, -1, 30, document.Bottom, writer.DirectContent);
            }

            //write on close of document
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);
            }
        }

        public class PDF_Cabec_Rodape_Paisagem : PdfPageEventHelper
        {
            public string Caminho;
            //public string PortariaMEC; Aqui é uma comunicação com o meio externo
            //public string DataMEC;
            //public string DataDOU;

            // write on top of document
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                base.OnOpenDocument(writer, document);
            }

            // write on start of each page
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);


                //PdfPTable tabFot = new PdfPTable(new float[] { 1f });
                ////tabFot.SpacingAfter = 10.0!
                //PdfPCell cell = default(PdfPCell);
                //tabFot.TotalWidth = 530f; //Aqui se seta se a tabela irá ficar mais a esquerda ou a direita

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_8_Italic = new Font(_bfVerdanaNormal, 8, Font.ITALIC);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                var lineStrong = new iTextSharp.text.pdf.draw.LineSeparator(2.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                iTextSharp.text.Image imgIPT = iTextSharp.text.Image.GetInstance(Caminho + "/img/ipt.gif");
                Paragraph p = new Paragraph();
                PdfPCell cell = default(PdfPCell);

                //Aqui se determina que é uma tabela com duas colunas
                PdfPTable table = new PdfPTable(2);
                table.TotalWidth = 830f;
                table.LockedWidth = true;

                //Aqui se determina se determina o tamanho de cada coluna
                float[] widths = new float[] { 60f, 170f };
                table.SetWidths(widths);

                //Coluna 1
                p = new Paragraph();
                imgIPT.SetAbsolutePosition(20, 755);
                imgIPT.ScalePercent(65);
                p.Add(new Chunk(imgIPT, 0, 0, true));
                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(p);
                cell = new PdfPCell(new Paragraph(new Chunk(imgIPT, 0, 0, true)));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                //cell.Colspan = 2;
                table.AddCell(cell);

                //Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Instituto de Pesquisas Tecnológicas do Estado de São Paulo S/A - IPT \r\n", font_Verdana_8_Normal));
                p.Add(new Chunk(" \r\n", font_Verdana_8_Normal));
                //p.Add(new Chunk("CET - Coordenadoria de Ensino Tecnológico \r\n", font_Verdana_8_Normal));
                p.Add(new Chunk("  \r\n", font_Verdana_8_Normal));
                p.Add(new Chunk("MESTRADO PROFISSIONAL  \r\n", font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);
                //(não sei, não sei, esquerda-direita, altura,não sei)
                table.WriteSelectedRows(0, -1, 42, (document.PageSize.Height - 40), writer.DirectContent);

                //Aqui é uma nova tabela
                PdfPTable table2 = new PdfPTable(1);
                table2.TotalWidth = 760f;
                table2.LockedWidth = true;

                widths = new float[] { 230f };
                table2.SetWidths(widths);

                //Aqui se desenha uma Linha
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(lineStrong));
                cell.AddElement(p);

                table2.AddCell(cell);
                //(não sei, não sei, esquerda-direita, altura,não sei)
                table2.WriteSelectedRows(0, -1, 42, (document.PageSize.Height - 100), writer.DirectContent);

            }

            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);
                var _bfArialNarrowNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_8_Italic = new Font(_bfVerdanaNormal, 8, Font.ITALIC);
                PdfPTable tabFot = new PdfPTable(new float[] { 1f });
                PdfPCell cell = default(PdfPCell);
                Paragraph p = new Paragraph();
                tabFot.TotalWidth = 830f;
                //p.Add(new Chunk("Data da Impressão: " + Strings.Format(Today, "dd/MM/yyyy") + Strings.Space(60) + "Página " + writer.PageNumber, font_Arialn_10_Normal));
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Instituto de Pesquisas Tecnológicas do Estado de São Paulo S/A - IPT \r\n", font_Verdana_8_Italic));
                p.Add(new Chunk("Av. Prof. Almeida Prado, 532 - Cidade Universitária - Butantã - 05508-901 - São Paulo - SP  \r\n", font_Verdana_8_Italic));
                p.Add(new Chunk("Caixa Postal 0141 - 01064-970 - São Paulo - SP - Tel.: (11)3767-4084, 3767-4624 - sapiens.ipt.br  \r\n", font_Verdana_8_Italic));
                cell = new PdfPCell(p);
                //cell.AddElement(p);

                //cell = new PdfPCell(p);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tabFot.AddCell(cell);
                tabFot.WriteSelectedRows(0, -1, 30, document.Bottom, writer.DirectContent);
            }

            //write on close of document
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);
            }
        }

        public class PDF_Cabec_Rodape_Sem_IPT : PdfPageEventHelper
        {
            public string Caminho;
            //public string PortariaMEC; Aqui é uma comunicação com o meio externo
            //public string DataMEC;
            //public string DataDOU;

            // write on top of document
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                base.OnOpenDocument(writer, document);
            }

            // write on start of each page
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);


                //PdfPTable tabFot = new PdfPTable(new float[] { 1f });
                ////tabFot.SpacingAfter = 10.0!
                //PdfPCell cell = default(PdfPCell);
                //tabFot.TotalWidth = 530f; //Aqui se seta se a tabela irá ficar mais a esquerda ou a direita

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_8_Italic = new Font(_bfVerdanaNormal, 8, Font.ITALIC);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                var lineStrong = new iTextSharp.text.pdf.draw.LineSeparator(2.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                iTextSharp.text.Image imgIPT = iTextSharp.text.Image.GetInstance(Caminho + "/img/ipt.gif");
                Paragraph p = new Paragraph();
                PdfPCell cell = default(PdfPCell);

                //Aqui se determina que é uma tabela com duas colunas
                PdfPTable table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                //Aqui se determina se determina o tamanho de cada coluna
                float[] widths = new float[] { 520f };
                table.SetWidths(widths);

                ////Coluna 1
                //p = new Paragraph();
                //imgIPT.SetAbsolutePosition(20, 755);
                //imgIPT.ScalePercent(65);
                //p.Add(new Chunk(""));
                //cell = new PdfPCell();
                //cell.Border = Rectangle.NO_BORDER;
                //cell.AddElement(p);
                //cell = new PdfPCell(new Paragraph(new Chunk("")));
                //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //cell.Border = Rectangle.NO_BORDER;
                ////cell.FixedHeight = 25f;
                ////cell.Colspan = 2;
                //table.AddCell(cell);

                //Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                //p.Add(new Chunk(" \r\n", font_Verdana_8_Normal));
                //p.Add(new Chunk("CET - Coordenadoria de Ensino Tecnológico \r\n", font_Verdana_14_Normal));
                p.Add(new Chunk("  \r\n", font_Verdana_14_Normal));
                p.Add(new Chunk("MESTRADO PROFISSIONAL  \r\n", font_Verdana_14_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);
                //(não sei, não sei, esquerda-direita, altura,não sei)
                table.WriteSelectedRows(0, -1, 42, (document.PageSize.Height - 40), writer.DirectContent);

                //Aqui é uma nova tabela
                PdfPTable table2 = new PdfPTable(1);
                table2.TotalWidth = 520f;
                table2.LockedWidth = true;

                widths = new float[] { 230f };
                table2.SetWidths(widths);

                //Aqui se desenha uma Linha
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(lineStrong));
                cell.AddElement(p);

                table2.AddCell(cell);
                //(não sei, não sei, esquerda-direita, altura,não sei)
                table2.WriteSelectedRows(0, -1, 42, (document.PageSize.Height - 100), writer.DirectContent);

            }

            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);
                var _bfArialNarrowNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_8_Italic = new Font(_bfVerdanaNormal, 8, Font.ITALIC);
                PdfPTable tabFot = new PdfPTable(new float[] { 1f });
                PdfPCell cell = default(PdfPCell);
                Paragraph p = new Paragraph();
                tabFot.TotalWidth = 520f;
                //p.Add(new Chunk("Data da Impressão: " + Strings.Format(Today, "dd/MM/yyyy") + Strings.Space(60) + "Página " + writer.PageNumber, font_Arialn_10_Normal));
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("\r\n", font_Verdana_8_Italic));
                p.Add(new Chunk("\r\n", font_Verdana_8_Italic));
                p.Add(new Chunk("\r\n", font_Verdana_8_Italic));
                cell = new PdfPCell(p);
                //cell.AddElement(p);

                //cell = new PdfPCell(p);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                tabFot.AddCell(cell);
                tabFot.WriteSelectedRows(0, -1, 30, document.Bottom, writer.DirectContent);
            }

            //write on close of document
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);
            }
        }

        public class PDF_Cabec_Rodape_Contrato : PdfPageEventHelper
        {
            public string Caminho;
            //public string PortariaMEC; Aqui é uma comunicação com o meio externo
            //public string DataMEC;
            //public string DataDOU;
            public int qPagina;

            // write on top of document
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                base.OnOpenDocument(writer, document);
            }

            // write on start of each page
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);


                //PdfPTable tabFot = new PdfPTable(new float[] { 1f });
                ////tabFot.SpacingAfter = 10.0!
                //PdfPCell cell = default(PdfPCell);
                //tabFot.TotalWidth = 530f; //Aqui se seta se a tabela irá ficar mais a esquerda ou a direita

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_8_Italic = new Font(_bfVerdanaNormal, 8, Font.ITALIC);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                var lineStrong = new iTextSharp.text.pdf.draw.LineSeparator(2.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                iTextSharp.text.Image imgIPT = iTextSharp.text.Image.GetInstance(Caminho + "/img/ipt.gif");
                Paragraph p = new Paragraph();
                PdfPCell cell = default(PdfPCell);

                //Aqui se determina que é uma tabela com duas colunas
                PdfPTable table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                //Aqui se determina se determina o tamanho de cada coluna
                float[] widths = new float[] { 100 };
                table.SetWidths(widths);

                //Coluna 1
                p = new Paragraph();
                imgIPT.SetAbsolutePosition(20, 755);
                imgIPT.ScalePercent(65);
                p.Add(new Chunk(imgIPT, 0, 0, true));
                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(p);
                cell = new PdfPCell(new Paragraph(new Chunk(imgIPT, 0, 0, true)));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                //cell.Colspan = 2;
                table.AddCell(cell);

                //(não sei, não sei, esquerda-direita, altura,não sei)
                table.WriteSelectedRows(0, -1, 42, (document.PageSize.Height - 40), writer.DirectContent);

                //Aqui é uma nova tabela
                PdfPTable table2 = new PdfPTable(1);
                table2.TotalWidth = 520f;
                table2.LockedWidth = true;

                widths = new float[] { 230f };
                table2.SetWidths(widths);

                //Aqui se desenha uma Linha
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(""));
                cell.AddElement(p);

                table2.AddCell(cell);
                //(não sei, não sei, esquerda-direita, altura,não sei)
                table2.WriteSelectedRows(0, -1, 42, (document.PageSize.Height - 100), writer.DirectContent);

            }

            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);
                var _bfArialNarrowNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Caminho + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var lineStrong = new iTextSharp.text.pdf.draw.LineSeparator(2.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                Font font_Verdana_8_Italic = new Font(_bfVerdanaNormal, 8, Font.ITALIC);

                PdfPTable tabFot;
                float[] widths;
                PdfPCell cell = default(PdfPCell);
                Paragraph p = new Paragraph();

                //===Tabela de 2 colunas ================
                tabFot = new PdfPTable(2);
                tabFot.TotalWidth = 520f;
                tabFot.LockedWidth = true;
                widths = new float[] { 95, 5 };
                tabFot.SetWidths(widths);
                //tabFot.DefaultCell.Border = Rectangle.TOP_BORDER;
                //tabFot.DefaultCell.

                //PdfPTable tabFot = new PdfPTable(new float[] { 1f });
                //tabFot.TotalWidth = 520f;
                //p.Add(new Chunk("Data da Impressão: " + Strings.Format(Today, "dd/MM/yyyy") + Strings.Space(60) + "Página " + writer.PageNumber, font_Arialn_10_Normal));
                iTextSharp.text.Image imgRodape = iTextSharp.text.Image.GetInstance(Caminho + "/img/rodape_sapiens.png");
                imgRodape.SetAbsolutePosition(100, 350);
                imgRodape.ScalePercent(18);
                p.Add(new Chunk(imgRodape, 0, 0, true));

                //p.Alignment = Element.ALIGN_CENTER;
                ////p.Add(new Chunk(lineStrong));
                //p.Add(new Chunk("\nInstituto de Pesquisas Tecnológicas do Estado de São Paulo S/A - IPT \r\n", font_Verdana_8_Italic));
                //p.Add(new Chunk("Av. Prof. Almeida Prado, 532 - Cidade Universitária - Butantã - 05508-901 - São Paulo - SP  \r\n", font_Verdana_8_Italic));
                //p.Add(new Chunk("Caixa Postal 0141 - 01064-970 - São Paulo - SP - Tel.: (11)3767-4084, 3767-4624 - sapiens.ipt.br  \r\n", font_Verdana_8_Italic));
                cell = new PdfPCell(p);
                //cell.AddElement(p);

                //cell = new PdfPCell(p);
                cell.Border = Rectangle.TOP_BORDER;
                cell.BorderWidthTop = 2;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabFot.AddCell(cell);

                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
               // p.Add(new Chunk(lineStrong));
                p.Add(new Chunk("\n\n" + qPagina.ToString(), font_Verdana_8_Italic));
                cell = new PdfPCell(p);
                //cell.AddElement(p);

                //cell = new PdfPCell(p);
                cell.Border = Rectangle.TOP_BORDER;
                cell.BorderWidthTop = 2;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabFot.AddCell(cell);

                tabFot.WriteSelectedRows(0, -1, 40, document.Bottom, writer.DirectContent);
            }

            //write on close of document
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);
            }
        }

        protected void btnImprimirAtestado_Click(object sender, EventArgs e)
        {
            var sAux = HttpContext.Current.Request["hCodigo"].ToString().Split(',');

            try
            {

                GeraAtestado(sAux[0], sAux[1], sAux[2], sAux[3], sAux[4]);

                if (File.Exists(Server.MapPath("~/doctos/Atestado_" + sAux[3] + "_" + sAux[0] + ".pdf")))
                {
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/Atestado_" + sAux[3] + "_" + sAux[0] + ".pdf")));
                    Response.WriteFile(Server.MapPath("~/doctos/Atestado_" + sAux[3] + "_" + sAux[0] + ".pdf"));
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão do Atestado do Prof. " + sAux[1];
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }
        }

        protected void GeraAtestado(string idProfessor, string sNomeProfessor, string idBanca, string qBanca, string qIdTurma)
        {
            try
            {
                GeraisAplicacao AplicacaoGerais = new GeraisAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request.Form["hQTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma item_matricula = new matricula_turma();
                item_matricula = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).SingleOrDefault();
                List<banca> lista_banca;
                lista_banca = item_matricula.banca.Where(x => x.id_banca == Convert.ToInt32(idBanca)).ToList();

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 120, 40);//estibulando o espaçamento das margens que queremos ===Antigo===(45, 40, 40, 40)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/Atestado_" + qBanca + "_" + idProfessor + ".pdf"), FileMode.Create));
                PDF_Cabec_Rodape_GeraEmentaPDF pageHeaderFooter = new PDF_Cabec_Rodape_GeraEmentaPDF();
                pageHeaderFooter.Caminho = Server.MapPath("~");
                pageHeaderFooter.qTipoCurso = item_matricula.turmas.cursos.tipos_curso.tipo_curso;
                writer.PageEvent = pageHeaderFooter;
                doc.Open();

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_11_Bold = new Font(_bfVerdanaNormal, 11, Font.BOLD);
                Font font_Verdana_11_Normal = new Font(_bfVerdanaNormal, 11, Font.NORMAL);
                Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Verdana_8_Bold = new Font(_bfVerdanaNormal, 8, Font.BOLD);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Verdana_7_Bold = new Font(_bfVerdanaNormal, 7, Font.BOLD);
                Font font_Verdana_7_Normal = new Font(_bfVerdanaNormal, 7, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);
                Color FontColor_Cinza = new Color(204, 204, 204);
                Color FontColor_CinzaClaro = new Color(229, 229, 229);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                float[] widths;
                Paragraph p;
                PdfPCell cell;
                PdfPTable table;

                //Aqui é uma nova tabela
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 230f };
                table.SetWidths(widths);

                table.SpacingBefore = 170f;
                table.SpacingAfter = 70f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_RIGHT;
                p.Add(new Chunk("BANCA CET Nº ", font_Verdana_11_Normal));
                p.Add(new Chunk(idBanca, font_Verdana_11_Bold));
                p.SpacingBefore = 170f;
                p.SpacingAfter = 170f;
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);



                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 230f };
                table.SetWidths(widths);

                table.SpacingAfter = 70f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("ATESTADO", font_Verdana_14_Bold));
                p.SpacingAfter = 70f;
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 230f };
                table.SetWidths(widths);

                table.SpacingAfter = 30f;

                banca_professores banca_professor_aux = lista_banca.ElementAt(0).banca_professores.Where(x => x.id_professor == Convert.ToInt32(idProfessor)).FirstOrDefault();

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;
                p.Add(new Chunk("Atesto que ", font_Verdana_11_Normal));
                p.Add(new Chunk(banca_professor_aux.professores.titulacao.reduzido, font_Verdana_11_Bold));
                p.Add(new Chunk(" " + banca_professor_aux.professores.nome, font_Verdana_11_Bold));
                if (banca_professor_aux.tipo_professor != "Membro Suplente")
                {
                    p.Add(new Chunk(", participou como Membro Titular da Comissão Julgadora ", font_Verdana_11_Normal));
                }
                else
                {
                    p.Add(new Chunk(", participou como Membro Suplente da Comissão Julgadora ", font_Verdana_11_Normal));
                }
                //p.Add(new Chunk(lista_banca.ElementAt(0).banca_professores.Where(x => x.id_professor == Convert.ToInt32(idProfessor)).FirstOrDefault().professores.titulacao.reduzido, font_Verdana_11_Bold));
                //p.Add(new Chunk(" " + lista_banca.ElementAt(0).banca_professores.Where(x => x.id_professor == Convert.ToInt32(idProfessor)).FirstOrDefault().professores.nome, font_Verdana_11_Bold));
                
                if (qBanca == "Qualificação")
                {
                    p.Add(new Chunk("do ", font_Verdana_11_Normal));
                    p.Add(new Chunk("Exame de Qualificação ", font_Verdana_11_Bold));
                    p.Add(new Chunk("intitulado ", font_Verdana_11_Normal));
                }
                else
                {
                    p.Add(new Chunk("da ", font_Verdana_11_Normal));
                    p.Add(new Chunk("Defesa da Dissertação ", font_Verdana_11_Bold));
                    p.Add(new Chunk("intitulada ", font_Verdana_11_Normal));
                }

                p.Add(new Chunk("\"" + lista_banca.ElementAt(0).titulo + "\"", font_Verdana_11_Bold ));

                if (item.sexo == "m")
                {
                    p.Add(new Chunk(", do aluno " , font_Verdana_11_Normal));
                }
                else
                {
                    p.Add(new Chunk(", da aluna ", font_Verdana_11_Normal));
                }
                p.Add(new Chunk(item.nome, font_Verdana_11_Bold));
                p.Add(new Chunk(", para obtenção do título de ", font_Verdana_11_Normal));
                p.Add(new Chunk("Mestre ", font_Verdana_11_Bold));
                p.Add(new Chunk("em ", font_Verdana_11_Normal));
                p.Add(new Chunk(item_matricula.turmas.cursos.nome, font_Verdana_11_Bold));
                p.Add(new Chunk(", defendido no Instituto de Pesquisas Tecnológicas em ", font_Verdana_11_Normal));
                p.Add(new Chunk(String.Format("{0:dd/MM/yyyy}", lista_banca.ElementAt(0).horario), font_Verdana_11_Bold));
                p.Add(new Chunk(".", font_Verdana_11_Normal));

                //p.Leading = 0;
                //Set a font-relative leading
                //p.MultipliedLeading = 2;

                p.SetLeading(12f,0.4f);

                cell = new PdfPCell();
                cell.AddElement(p);

                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_JUSTIFIED;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 15f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(2);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 63f, 180f };
                table.SetWidths(widths);

                table.SpacingAfter = 30f;

                foreach (var elemento in lista_banca.ElementAt(0).banca_professores.OrderBy(x=> x.tipo_professor == "Membro Suplente").ThenBy(x => x.tipo_professor == "Membro").ThenBy(x => x.tipo_professor == "Co-orientador").ThenBy(x => x.tipo_professor == "Orientador").ThenBy(x => x.professores.nome))
                {
                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(elemento.tipo_professor + ": ", font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 6f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(elemento.professores.titulacao.reduzido + " " + elemento.professores.nome, font_Verdana_9_Bold));
                    if (elemento.professores.empresa != "" && elemento.professores.empresa != null)
                    {
                        p.Add(new Chunk(" - ", font_Verdana_8_Normal));
                        p.Add(new Chunk(elemento.professores.empresa, font_Verdana_8_Normal));
                    }
                    
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 6f;
                    table.AddCell(cell);
                }

                doc.Add(table);

                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 530f };
                table.SetWidths(widths);

                table.SpacingAfter = 60f;

                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_RIGHT;
                p.Add(new Chunk("São Paulo, " + lista_banca.ElementAt(0).horario.Value.Day + " de " + dtfi.GetMonthName(lista_banca.ElementAt(0).horario.Value.Month) + " de " + lista_banca.ElementAt(0).horario.Value.Year, font_Verdana_10_Normal));
                p.SpacingAfter = 70f;
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui se desenha uma linha fina
                //p = new Paragraph();
                //p.Alignment = Element.ALIGN_CENTER;
                //p.Clear();
                //p.Add(new Chunk(linefine));
                //doc.Add(p);

                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(3);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 63f, 180f, 63f  };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(" ", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\nMestrado Profissional\nSecretaria Acadêmica", font_Verdana_10_Normal));
                p.SpacingAfter = 20f;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(" ", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);

                doc.Add(table);

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão do Atestado do Prof. " + sNomeProfessor;
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }

        }

        protected void btnImprimirConvite_Click(object sender, EventArgs e)
        {
            var sAux = HttpContext.Current.Request["hCodigo"].ToString().Split(',');

            try
            {

                GeraConvite(sAux[0], sAux[1], sAux[2], sAux[3], sAux[4]);

                if (File.Exists(Server.MapPath("~/doctos/Convite_" + sAux[3] + "_" + sAux[0] + ".pdf")))
                {
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/Convite_" + sAux[3] + "_" + sAux[0] + ".pdf")));
                    Response.WriteFile(Server.MapPath("~/doctos/Convite_" + sAux[3] + "_" + sAux[0] + ".pdf"));
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão do Convite do Prof. " + sAux[1];
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }
        }

        protected void GeraConvite(string idProfessor, string sNomeProfessor, string idBanca, string qBanca, string qIdTurma)
        {
            try
            {
                GeraisAplicacao AplicacaoGerais = new GeraisAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request.Form["hQTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma item_matricula = new matricula_turma();
                item_matricula = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).SingleOrDefault();
                List<banca> lista_banca;
                lista_banca = item_matricula.banca.Where(x => x.id_banca == Convert.ToInt32(idBanca)).ToList();

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 120, 40);//estibulando o espaçamento das margens que queremos ===Antigo===(45, 40, 40, 40)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/Convite_" + qBanca + "_" + idProfessor + ".pdf"), FileMode.Create));
                PDF_Cabec_Rodape_GeraEmentaPDF pageHeaderFooter = new PDF_Cabec_Rodape_GeraEmentaPDF();
                pageHeaderFooter.Caminho = Server.MapPath("~");
                pageHeaderFooter.qTipoCurso = item_matricula.turmas.cursos.tipos_curso.tipo_curso;
                writer.PageEvent = pageHeaderFooter;
                doc.Open();

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_11_Bold = new Font(_bfVerdanaNormal, 11, Font.BOLD);
                Font font_Verdana_11_Normal = new Font(_bfVerdanaNormal, 11, Font.NORMAL);
                Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Verdana_8_Bold = new Font(_bfVerdanaNormal, 8, Font.BOLD);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Verdana_7_Bold = new Font(_bfVerdanaNormal, 7, Font.BOLD);
                Font font_Verdana_7_Normal = new Font(_bfVerdanaNormal, 7, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);
                Color FontColor_Cinza = new Color(204, 204, 204);
                Color FontColor_CinzaClaro = new Color(229, 229, 229);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                float[] widths;
                Paragraph p;
                PdfPCell cell;
                PdfPTable table;

                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 530f };
                table.SetWidths(widths);

                table.SpacingAfter = 30f;

                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                //Alterado a pedido da Mary 19/06/2019
                //p.Add(new Chunk("\n\nSão Paulo, " + DateTime.Today.Day + " de " + dtfi.GetMonthName(DateTime.Today.Month) + " de " + DateTime.Today.Year, font_Verdana_10_Normal));
                p.Add(new Chunk("\n\nSão Paulo, " + lista_banca.ElementAt(0).horario.Value.Day + " de " + dtfi.GetMonthName(lista_banca.ElementAt(0).horario.Value.Month) + " de " + lista_banca.ElementAt(0).horario.Value.Year, font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                table.SpacingAfter = 30f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                p.Add(new Chunk("BANCA CET Nº ", font_Verdana_11_Normal));
                p.Add(new Chunk(idBanca, font_Verdana_11_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                table.SpacingAfter = 30f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;

                if (lista_banca.ElementAt(0).banca_professores.Where(x => x.id_professor == Convert.ToInt32(idProfessor)).FirstOrDefault().professores.sexo == "m")
                {
                    p.Add(new Chunk("Ilmo Sr.\n", font_Verdana_11_Normal));
                }
                else
                {
                    p.Add(new Chunk("Ilma Sra.\n", font_Verdana_11_Normal));
                }

                p.Add(new Chunk(lista_banca.ElementAt(0).banca_professores.Where(x => x.id_professor == Convert.ToInt32(idProfessor)).FirstOrDefault().professores.titulacao.reduzido, font_Verdana_11_Bold));
                p.Add(new Chunk(" " + lista_banca.ElementAt(0).banca_professores.Where(x => x.id_professor == Convert.ToInt32(idProfessor)).FirstOrDefault().professores.nome, font_Verdana_11_Bold));
                p.SetLeading(12f, 0.4f);
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 230f };
                table.SetWidths(widths);

                table.SpacingAfter = 20f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;
                p.Add(new Chunk("Informamos que V. Sª. foi indicado(a) pela Comissão de Pós-Graduação do IPT como membro titular da Comissão Julgadora na apresentação ", font_Verdana_11_Normal));
                if (qBanca == "Qualificação")
                {
                    p.Add(new Chunk("do ", font_Verdana_11_Normal));
                    p.Add(new Chunk("Exame de Qualificação ", font_Verdana_11_Bold));
                    p.Add(new Chunk("intitulado ", font_Verdana_11_Normal));
                }
                else
                {
                    if (item_matricula.turmas.cursos.id_tipo_curso == 3)
                    {
                        p.Add(new Chunk("da ", font_Verdana_11_Normal));
                        p.Add(new Chunk("Defesa da Monografia ", font_Verdana_11_Bold));
                        p.Add(new Chunk("intitulada ", font_Verdana_11_Normal));
                    }
                    else
                    {
                        p.Add(new Chunk("da ", font_Verdana_11_Normal));
                        p.Add(new Chunk("Defesa da Dissertação ", font_Verdana_11_Bold));
                        p.Add(new Chunk("intitulada ", font_Verdana_11_Normal));
                    }
                }
                p.Add(new Chunk("\"" + lista_banca.ElementAt(0).titulo + "\"", font_Verdana_11_Bold));

                if (item.sexo == "m")
                {
                    p.Add(new Chunk(", do aluno ", font_Verdana_11_Normal));
                }
                else
                {
                    p.Add(new Chunk(", da aluna ", font_Verdana_11_Normal));
                }
                p.Add(new Chunk(item.nome, font_Verdana_11_Bold));
                p.Add(new Chunk(", para obtenção do título de ", font_Verdana_11_Normal));
                p.Add(new Chunk("Mestre ", font_Verdana_11_Bold));
                p.Add(new Chunk("em ", font_Verdana_11_Normal));
                p.Add(new Chunk(item_matricula.turmas.cursos.nome, font_Verdana_11_Bold));
                p.Add(new Chunk(".", font_Verdana_11_Normal));

                //p.Leading = 0;
                //Set a font-relative leading
                //p.MultipliedLeading = 2;

                p.SetLeading(12f, 0.4f);

                cell = new PdfPCell();
                cell.AddElement(p);

                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_JUSTIFIED;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 15f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(2);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 63f, 180f };
                table.SetWidths(widths);

                table.SpacingAfter = 30f;

                foreach (var elemento in lista_banca.ElementAt(0).banca_professores.OrderBy(x => x.tipo_professor == "Membro Suplente").ThenBy(x => x.tipo_professor == "Membro").ThenBy(x => x.tipo_professor == "Co-orientador").ThenBy(x => x.tipo_professor == "Orientador").ThenBy(x => x.professores.nome))
                {
                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(elemento.tipo_professor + ": ", font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 6f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(elemento.professores.titulacao.reduzido + " " + elemento.professores.nome, font_Verdana_9_Bold));
                    if (elemento.professores.empresa != "" && elemento.professores.empresa != null)
                    {
                        p.Add(new Chunk(" - ", font_Verdana_8_Normal));
                        p.Add(new Chunk(elemento.professores.empresa, font_Verdana_8_Normal));
                    }

                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 6f;
                    table.AddCell(cell);
                }

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("\nData: ", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("\n" + String.Format("{0:dd/MM/yyyy HH:mm}", lista_banca.ElementAt(0).horario), font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Local: ", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("CET - Prédio 56 - Térreo", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 530f };
                table.SetWidths(widths);

                table.SpacingAfter = 60f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_RIGHT;
                p.Add(new Chunk("Atenciosamente,               ", font_Verdana_10_Normal));
                p.SpacingAfter = 70f;
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui se desenha uma linha fina
                //p = new Paragraph();
                //p.Alignment = Element.ALIGN_CENTER;
                //p.Clear();
                //p.Add(new Chunk(linefine));
                //doc.Add(p);

                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(3);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 63f, 180f, 63f };
                table.SetWidths(widths);

                table.SpacingAfter = 10f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(" ", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\nMestrado Profissional\nSecretaria Acadêmica", font_Verdana_10_Normal));
                p.SpacingAfter = 20f;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(" ", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                p.Add(new Chunk("Caso ocorra cancelamento da banca, mudança de data ou horário, por gentileza, enviar e-mail para mestrado@ipt.br", font_Verdana_8_Bold));
                p.SetLeading(12f, 0.4f);
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão do Convite do Prof. " + sNomeProfessor;
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }

        }

        protected void btnImprimirDivulgacao_Click(object sender, EventArgs e)
        {
            var sAux = HttpContext.Current.Request["hCodigo"].ToString().Split(',');

            try
            {

                GeraDivulgacao(sAux[0], sAux[1]);

                if (File.Exists(Server.MapPath("~/doctos/" + sAux[0] + "_" + sAux[1] + ".pdf")))
                {
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/" + sAux[0] + "_" + sAux[1] + ".pdf")));
                    Response.WriteFile(Server.MapPath("~/doctos/" + sAux[0] + "_" + sAux[1] + ".pdf"));
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão da Divulgação da " + sAux[0];
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }
        }

        protected void GeraDivulgacao(string qBanca, string qIdTurma)
        {
            try
            {
                GeraisAplicacao AplicacaoGerais = new GeraisAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request.Form["hQTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma item_matricula = new matricula_turma();
                item_matricula = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).SingleOrDefault();
                List<banca> lista_banca;
                lista_banca = item_matricula.banca.Where(x => x.tipo_banca == qBanca).ToList();

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 120, 40);//estibulando o espaçamento das margens que queremos ===Antigo===(45, 40, 40, 40)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/" + qBanca + "_" + qIdTurma + ".pdf"), FileMode.Create));
                PDF_Cabec_Rodape_GeraEmentaPDF pageHeaderFooter = new PDF_Cabec_Rodape_GeraEmentaPDF();
                pageHeaderFooter.Caminho = Server.MapPath("~");
                pageHeaderFooter.qTipoCurso = item_matricula.turmas.cursos.tipos_curso.tipo_curso;
                writer.PageEvent = pageHeaderFooter;
                doc.Open();

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_11_Bold = new Font(_bfVerdanaNormal, 11, Font.BOLD);
                Font font_Verdana_11_Normal = new Font(_bfVerdanaNormal, 11, Font.NORMAL);
                Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Verdana_8_Bold = new Font(_bfVerdanaNormal, 8, Font.BOLD);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Verdana_7_Bold = new Font(_bfVerdanaNormal, 7, Font.BOLD);
                Font font_Verdana_7_Normal = new Font(_bfVerdanaNormal, 7, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);
                Color FontColor_Cinza = new Color(204, 204, 204);
                Color FontColor_CinzaClaro = new Color(229, 229, 229);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                float[] widths;
                Paragraph p;
                PdfPCell cell;
                PdfPTable table;

                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                table.SpacingAfter = 30f;

                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("\n\nDIVULGAÇÃO DE BANCA DE " + qBanca.ToUpper(), font_Verdana_10_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela
                table = new PdfPTable(2);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 133f, 350f };
                table.SetWidths(widths);

                table.SpacingAfter = 10f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_RIGHT;
                p.Add(new Chunk("Curso: ", font_Verdana_11_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 15f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                p.Add(new Chunk(item_matricula.turmas.cursos.nome, font_Verdana_11_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 15f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_RIGHT;
                p.Add(new Chunk("Área de Concentração: ", font_Verdana_11_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 15f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                p.Add(new Chunk((item_matricula.areas_concentracao == null) ? "" : item_matricula.areas_concentracao.nome, font_Verdana_11_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 15f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_RIGHT;
                p.Add(new Chunk("Aluno: ", font_Verdana_11_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 15f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                p.Add(new Chunk(item.nome, font_Verdana_11_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 15f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_RIGHT;
                p.Add(new Chunk("Título: ", font_Verdana_11_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 15f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_TOP;
                p.Add(new Chunk(lista_banca.ElementAt(0).titulo, font_Verdana_11_Bold));
                p.SetLeading(12f, 0f);
                //p.setFixedLeadin= 4f;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //cell.VerticalAlignment = Element.ALIGN_BOTTOM;
                //cell.ExtraParagraphSpace = 2;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 15f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                table.SpacingAfter = 5f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Comissão Julgadora ", font_Verdana_11_Normal));
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                
                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(2);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 133f, 350f };
                table.SetWidths(widths);

                table.SpacingAfter = 30f;

                foreach (var elemento in lista_banca.ElementAt(0).banca_professores.OrderBy(x => x.tipo_professor == "Membro Suplente").ThenBy(x => x.tipo_professor == "Membro").ThenBy(x => x.tipo_professor == "Co-orientador").ThenBy(x => x.tipo_professor == "Orientador").ThenBy(x => x.professores.nome))
                {
                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(elemento.tipo_professor + ": ", font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 6f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk(elemento.professores.titulacao.reduzido + " " + elemento.professores.nome, font_Verdana_9_Bold));
                    if (elemento.professores.empresa != "" && elemento.professores.empresa != null)
                    {
                        p.Add(new Chunk(" - ", font_Verdana_8_Normal));
                        p.Add(new Chunk(elemento.professores.empresa, font_Verdana_8_Normal));
                    }

                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingBottom = 6f;
                    table.AddCell(cell);
                }

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("\nData: ", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("\n" + String.Format("{0:dd/MM/yyyy HH:mm}", lista_banca.ElementAt(0).horario), font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Local: ", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("CET - Prédio 56 - Térreo", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 6f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 530f };
                table.SetWidths(widths);

                table.SpacingAfter = 60f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("PARA ASSISTIR BASTA COMPARECER NO DIA E HORA DA APRESENTAÇÃO", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão da Divulgação da banca de " + qBanca;
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }

        }

        protected void btnImprimirRecibo_Click(object sender, EventArgs e)
        {
            var sAux = HttpContext.Current.Request["hCodigo"].ToString().Split(',');

            try
            {

                GeraRecibo(sAux[0], sAux[1], sAux[2], sAux[3], sAux[4]);

                if (File.Exists(Server.MapPath("~/doctos/Recibo_" + sAux[0] + "_" + sAux[2] + ".pdf")))
                {
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/Recibo_" + sAux[0] + "_" + sAux[2] + ".pdf")));
                    Response.WriteFile(Server.MapPath("~/doctos/Recibo_" + sAux[0] + "_" + sAux[2] + ".pdf"));
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão do Recibo do Prof. " + sAux[1];
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }
        }

        protected void GeraRecibo(string idProfessor, string sNomeProfessor, string idBanca, string qBanca, string qIdTurma)
        {
            try
            {
                GeraisAplicacao AplicacaoGerais = new GeraisAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request.Form["hQTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma item_matricula = new matricula_turma();
                item_matricula = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).SingleOrDefault();

                List<banca> lista_banca;
                lista_banca = item_matricula.banca.Where(x => x.id_banca == Convert.ToInt32(idBanca)).ToList();

                banca_professores item_banca_professores;
                item_banca_professores = lista_banca.ElementAt(0).banca_professores.Where(x=> x.id_professor == Convert.ToInt32(idProfessor)).SingleOrDefault();

                Enderecos item_endereco = new Enderecos();
                item_endereco.id_endereco = 1; //FIPT
                item_endereco = AplicacaoGerais.BuscaEndereco(item_endereco);

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 120, 40);//estibulando o espaçamento das margens que queremos ===Antigo===(45, 40, 40, 40)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/Recibo_" + idProfessor + "_" + idBanca + ".pdf"), FileMode.Create));
                PDF_Cabec_Rodape_Sem_IPT pageHeaderFooter = new PDF_Cabec_Rodape_Sem_IPT();
                pageHeaderFooter.Caminho = Server.MapPath("~");
                writer.PageEvent = pageHeaderFooter;
                doc.Open();

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_11_Bold = new Font(_bfVerdanaNormal, 11, Font.BOLD);
                Font font_Verdana_11_Normal = new Font(_bfVerdanaNormal, 11, Font.NORMAL);
                Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Verdana_8_Bold = new Font(_bfVerdanaNormal, 8, Font.BOLD);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Verdana_7_Bold = new Font(_bfVerdanaNormal, 7, Font.BOLD);
                Font font_Verdana_7_Normal = new Font(_bfVerdanaNormal, 7, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);
                Color FontColor_Cinza = new Color(204, 204, 204);
                Color FontColor_CinzaClaro = new Color(229, 229, 229);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                float[] widths;
                Paragraph p;
                PdfPCell cell;
                PdfPTable table;

                //Aqui é uma nova tabela de 1 Coluna ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                table.SpacingAfter = 60f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("\n\n\n\n\n\n\n\nRECIBO DE RESSARCIMENTO", font_Verdana_11_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 1 Coluna ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                table.SpacingAfter = 30f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;
                p.Add(new Chunk("Eu, ", font_Verdana_10_Normal));
                p.Add(new Chunk(item_banca_professores.professores.nome, font_Verdana_10_Bold));
                p.Add(new Chunk(" do CPF Nº " + item_banca_professores.professores.cpf + " e RG Nº " + item_banca_professores.professores.numero_documento, font_Verdana_10_Normal));
                p.Add(new Chunk(" residente à ", font_Verdana_10_Normal));
                p.Add(new Chunk(item_banca_professores.professores.endereco_res + ", " + item_banca_professores.professores.numero_res + " " + item_banca_professores.professores.complemento_res + ", " + item_banca_professores.professores.bairro_res + ", " + item_banca_professores.professores.cidade_res + "/" + item_banca_professores.professores.uf_res + ", CEP: " + item_banca_professores.professores.cep_res , font_Verdana_10_Bold));
                p.Add(new Chunk(", declaro ter recebido da " + item_endereco.nome + ", sito à " + item_endereco.endereco + ", " + item_endereco.numero + " " + item_endereco.complemento + " - " + item_endereco.bairro + " - " + item_endereco.cidade + "/" + item_endereco.estado + ", CEP: " + item_endereco.cep + ", CNPJ: " + item_endereco.cnpj + ", IE: " + item_endereco.ie + ", o valor de ", font_Verdana_10_Normal));
                p.Add(new Chunk(string.Format("{0:C}", item_banca_professores.professores.professores_forma_recebimento.forma_recebimento.valor) + " (" + Utilizades.EscreverExtenso(Convert.ToDecimal(item_banca_professores.professores.professores_forma_recebimento.forma_recebimento.valor)), font_Verdana_10_Normal));
                p.Add(new Chunk(") referente ao ressarcimento dos gastos incorridos com locomoção e alimentação no dia ", font_Verdana_10_Normal));
                p.Add(new Chunk(String.Format("{0:dd/MM/yyyy}", lista_banca.ElementAt(0).horario), font_Verdana_10_Bold));
                p.Add(new Chunk(" na participação em ", font_Verdana_10_Normal));
                p.Add(new Chunk(" Exame de Qualificação/Defesa", font_Verdana_10_Bold));
                p.Add(new Chunk(".", font_Verdana_11_Normal));

                p.SetLeading(12f, 1.0f);
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_JUSTIFIED;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 530f };
                table.SetWidths(widths);

                table.SpacingAfter = 30f;

                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                p.Add(new Chunk("\n\n\n\nSão Paulo, " + String.Format("{0:dd}", lista_banca.ElementAt(0).horario) + " de " + dtfi.GetMonthName(Convert.ToDateTime(lista_banca.ElementAt(0).horario).Month) + " de " + Convert.ToDateTime(lista_banca.ElementAt(0).horario).Year, font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela de 3 Colunas ========================================================
                table = new PdfPTable(3);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 63f, 180f, 63f };
                table.SetWidths(widths);

                table.SpacingAfter = 10f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(" ", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("\n\n\n", font_Verdana_9_Normal));
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\n" + item_banca_professores.professores.nome, font_Verdana_10_Normal));
                p.SpacingAfter = 20f;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(" ", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);

                doc.Add(table);

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão do Recibo do Prof. " + sNomeProfessor;
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }

        }

        protected void btnImprimirAta_Click(object sender, EventArgs e)
        {
            var sAux = HttpContext.Current.Request["hCodigo"].ToString().Split(',');

            try
            {
                if (sAux[0] ==  "Qualificação")
                {
                    GeraAtaQualificacao(sAux[0], sAux[1]);

                    if (File.Exists(Server.MapPath("~/doctos/Ata_" + sAux[0] + "_" + sAux[1] + ".pdf")))
                    {
                        Response.Clear();
                        Response.BufferOutput = true;
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/Ata_" + sAux[0] + "_" + sAux[1] + ".pdf")));
                        Response.WriteFile(Server.MapPath("~/doctos/Ata_" + sAux[0] + "_" + sAux[1] + ".pdf"));
                        Response.Flush();
                        Response.End();
                    }
                }
                else
                {
                    GeraAtaDefesa(sAux[0], sAux[1]);

                    if (File.Exists(Server.MapPath("~/doctos/Ata_" + sAux[0] + "_" + sAux[1] + ".pdf")))
                    {
                        Response.Clear();
                        Response.BufferOutput = true;
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/Ata_" + sAux[0] + "_" + sAux[1] + ".pdf")));
                        Response.WriteFile(Server.MapPath("~/doctos/Ata_" + sAux[0] + "_" + sAux[1] + ".pdf"));
                        Response.Flush();
                        Response.End();
                    }
                }
                
            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão da Ata de " + sAux[0];
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }
        }

        protected void GeraAtaQualificacao(string qBanca, string qIdTurma)
        {
            try
            {
                GeraisAplicacao AplicacaoGerais = new GeraisAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request.Form["hQTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma item_matricula = new matricula_turma();
                item_matricula = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).SingleOrDefault();
                banca item_banca;
                item_banca = item_matricula.banca.Where(x => x.tipo_banca == qBanca).FirstOrDefault();

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 120, 40);//estibulando o espaçamento das margens que queremos ===Antigo===(45, 40, 40, 40)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/Ata_" + qBanca + "_" + qIdTurma + ".pdf"), FileMode.Create));
                PDF_Cabec_Rodape_GeraEmentaPDF pageHeaderFooter = new PDF_Cabec_Rodape_GeraEmentaPDF();
                pageHeaderFooter.Caminho = Server.MapPath("~");
                pageHeaderFooter.qTipoCurso = item_matricula.turmas.cursos.tipos_curso.tipo_curso;
                writer.PageEvent = pageHeaderFooter;
                doc.Open();

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_11_Bold = new Font(_bfVerdanaNormal, 11, Font.BOLD);
                Font font_Verdana_11_Normal = new Font(_bfVerdanaNormal, 11, Font.NORMAL);
                Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Verdana_8_Bold = new Font(_bfVerdanaNormal, 8, Font.BOLD);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Verdana_7_Bold = new Font(_bfVerdanaNormal, 7, Font.BOLD);
                Font font_Verdana_7_Normal = new Font(_bfVerdanaNormal, 7, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);
                Color FontColor_Cinza = new Color(204, 204, 204);
                Color FontColor_CinzaClaro = new Color(229, 229, 229);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                float[] widths;
                Paragraph p;
                PdfPCell cell;
                PdfPTable table;

                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                table.SpacingAfter = 10f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                
                p.Add(new Chunk(item_matricula.turmas.cursos.tipos_curso.tipo_curso + " em ", font_Verdana_12_Normal));
                p.Add(new Chunk(item_matricula.turmas.cursos.nome, font_Verdana_12_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                table.SpacingAfter = 10f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("EXAME DE QUALIFICAÇÃO", font_Verdana_12_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela de 2 Colunas ========================================================
                iTextSharp.text.Table table2 = new iTextSharp.text.Table(2);
                Cell cell2 = new Cell();
                table2.Alignment = Rectangle.ALIGN_CENTER;
                table2.Padding = 1;
                table2.Spacing = 0;
                table2.Border = Rectangle.NO_BORDER;
                table2.BorderWidth = 0;
                table2.Width = 90;
                table2.DeleteAllRows();
                table2.Widths = new Single[] {25, 75};
                table2.DefaultCellBorder = 0;

                if (item_matricula.areas_concentracao != null)
                {
                    cell2 = new Cell(new Phrase("Área de Concentração:", font_Verdana_9_Normal));
                    cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    table2.AddCell(cell2);
                    table2.AddCell(new Cell(new Phrase(item_matricula.areas_concentracao.nome, font_Verdana_9_Bold)));
                }
                
                cell2 = new Cell(new Phrase("Aluno:", font_Verdana_9_Normal));
                cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table2.AddCell(cell2);
                table2.AddCell(new Cell(new Phrase(item.nome, font_Verdana_9_Bold)));

                cell2 = new Cell(new Phrase("Título:", font_Verdana_9_Normal));
                cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table2.AddCell(cell2);
                table2.AddCell(new Cell(new Phrase(item_banca.titulo, font_Verdana_9_Bold)));

                cell2 = new Cell(new Phrase("Orientador:", font_Verdana_9_Normal));
                cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table2.AddCell(cell2);
                table2.AddCell(new Cell(new Phrase(item_banca.banca_professores.Where(x=> x.tipo_professor == "Orientador").FirstOrDefault().professores.nome , font_Verdana_9_Bold)));

                doc.Add(table2);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 530f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;

                //===
                string sRemoto = "";
                if (item_banca.remoto == 1)
                {
                    sRemoto = ", remotamente,";
                }
                //====

                p.Add(new Chunk("\n\nEm " + String.Format("{0:dd/MM/yyyy}", item_banca.horario) + " às " + String.Format("{0: HH:mm}", item_banca.horario) + ", foi realizado" + sRemoto + " exame de qualificação do aluno(a) acima, com o seguinte resultado", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 530f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Comissão Julgadora:", font_Verdana_9_Normal));
                p.SpacingAfter = 4f;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table2 = new iTextSharp.text.Table(4);

                table2.Alignment = Rectangle.ALIGN_CENTER;
                table2.Padding = 1;
                table2.Spacing = 0;
                table2.Border = Rectangle.BOX;
                table2.BorderWidth = 1;
                table2.Width = 100;
                table2.DeleteAllRows();
                table2.Widths = new Single[] { 15, 65, 10, 10};
                table2.DefaultCellBorder = 1;

                table2.AddCell(" ");
                cell2 = new Cell(new Phrase("Examinadores", font_Verdana_9_Bold));
                cell2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell2.Border = Rectangle.BOX;
                table2.AddCell(cell2);
                cell2 = new Cell(new Phrase("Aprovado", font_Verdana_9_Bold));
                cell2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell2.Border = Rectangle.BOX;
                table2.AddCell(cell2);
                cell2 = new Cell(new Phrase("Reprovado", font_Verdana_9_Bold));
                cell2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell2.Border = Rectangle.BOX;
                table2.AddCell(cell2);
                foreach (var elemento in item_banca.banca_professores.OrderBy(x => x.tipo_professor == "Membro Suplente").ThenBy(x => x.tipo_professor == "Membro").ThenBy(x => x.tipo_professor == "Co-orientador").ThenBy(x => x.tipo_professor == "Orientador").ThenBy(x => x.professores.nome))
                {
                    string qMenbro;
                    if (elemento.tipo_professor == "Membro Suplente")
                    {
                        qMenbro = "Suplente";
                    }
                    else
                    {
                        qMenbro = elemento.tipo_professor;
                    }
                    cell2 = new Cell(new Phrase(qMenbro + ": ", font_Verdana_9_Normal));
                    cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell2.Border = Rectangle.BOX;
                    table2.AddCell(cell2);
                    cell2 = new Cell(new Phrase(elemento.professores.titulacao.reduzido + " " + elemento.professores.nome , font_Verdana_9_Bold));
                    if (elemento.professores.empresa.Trim() != "")
                    {
                        cell2.Add(new Phrase(" - " + elemento.professores.empresa, font_Verdana_9_Normal));
                    }
                    
                    cell2.Border = Rectangle.BOX;
                    table2.AddCell(cell2);
                    cell2 = new Cell(" ");
                    cell2.Border = Rectangle.BOX;
                    table2.AddCell(cell2);
                    table2.AddCell(" ");
                }

                doc.Add(table2);


                //==== trecho alterado conforme e-mail de 12/04/2022 =============
                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 530f };
                table.SetWidths(widths);

                //cell = new PdfPCell();
                //p = new Paragraph();
                //p.Alignment = Element.ALIGN_LEFT;
                //p.Add(new Chunk("\nParecer da banca em anexo.", font_Verdana_9_Normal));
                //cell = new PdfPCell(p);
                //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //cell.VerticalAlignment = Element.ALIGN_LEFT;
                //cell.Border = Rectangle.NO_BORDER;
                //table.AddCell(cell);

                //cell = new PdfPCell();
                //p = new Paragraph();
                //p.Alignment = Element.ALIGN_CENTER;
                //p.Add(new Chunk("Assinatura dos Examinadores:", font_Verdana_9_Normal));
                //p.SpacingAfter = 4f;
                //cell = new PdfPCell();
                //cell.AddElement(p);
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.VerticalAlignment = Element.ALIGN_CENTER;
                //cell.Border = Rectangle.NO_BORDER;
                //table.AddCell(cell);


                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                p.Add(new Chunk("\nAs orientações foram repassadas ao aluno pelos membros da banca durante a argüição.", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);
                //==== trecho alterado conforme e-mail de 12/04/2022 =============

                //==== suprimido conforme e-mail de 12/04/2022 =============
                //Aqui é uma nova tabela de 2 Colunas ========================================================
                //table2 = new iTextSharp.text.Table(2);

                //table2.Alignment = Rectangle.ALIGN_CENTER;
                //table2.Padding = 1;
                //table2.Spacing = 0;
                //table2.Border = Rectangle.BOX;
                //table2.BorderWidth = 1;
                //table2.Width = 100;
                //table2.DeleteAllRows();
                //table2.Widths = new Single[] { 15, 85};
                //table2.DefaultCellBorder = 1;

                //foreach (var elemento in item_banca.banca_professores.OrderBy(x => x.tipo_professor == "Membro Suplente").ThenBy(x => x.tipo_professor == "Membro").ThenBy(x => x.tipo_professor == "Co-orientador").ThenBy(x => x.tipo_professor == "Orientador").ThenBy(x => x.professores.nome))
                //{
                //    string qMenbro;
                //    if (elemento.tipo_professor == "Membro Suplente")
                //    {
                //        qMenbro = "Suplente";
                //    }
                //    else
                //    {
                //        qMenbro = elemento.tipo_professor;
                //    }
                //    cell2 = new Cell(new Phrase(qMenbro + ": ", font_Verdana_9_Normal));
                //    cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                //    cell2.Border = Rectangle.BOX;
                //    table2.AddCell(cell2);
                //    table2.AddCell("\r\n");
                //}

                //doc.Add(table2);
                //==== suprimido conforme e-mail de 12/04/2022 =============

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 530f };
                table.SetWidths(widths);

                //table.SpacingAfter = 5f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                p.Add(new Chunk("\nSão Paulo, " + String.Format("{0:dd}", item_banca.horario) + " de " + dtfi.GetMonthName(Convert.ToDateTime(item_banca.horario).Month) + " de " + Convert.ToDateTime(item_banca.horario).Year, font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                if (txtObsAta.Value != "")
                {
                    //Aqui é uma nova tabela de 1 Colunas ========================================================
                    table = new PdfPTable(1);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;

                    widths = new float[] { 530f };
                    table.SetWidths(widths);

                    table.SpacingAfter = 30f;

                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_LEFT;
                    p.Add(new Chunk(txtObsAta.Value, font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    doc.Add(table);
                }

                //===trecho alterado conforme e-mail de 12/04/2022 =============
                //Aqui é uma nova tabela de 3 Colunas ========================================================
                table = new PdfPTable(3);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 50f, 180f, 50f };
                table.SetWidths(widths);

                //table.SpacingAfter = 10f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(" ", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                //p.Add(new Chunk(linefine));
                //p.Add(new Chunk("\n\nCoordenadoria de Ensino Tecnológico\n", font_Verdana_10_Normal));
                p.Add(new Chunk("\n\n", font_Verdana_10_Normal));
                p.Add(new Chunk(item_matricula.turmas.cursos.tipos_curso.tipo_curso + " Profissional em " + item_matricula.turmas.cursos.nome + "\n\n\n", font_Verdana_10_Normal));
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\nOrientador: " + item_banca.banca_professores.Where(x => x.tipo_professor == "Orientador").FirstOrDefault().professores.nome, font_Verdana_10_Normal));

                p.Add(new Chunk("\n\n \n", font_Verdana_10_Normal));
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\nCoordenador: " + item_matricula.turmas.cursos.cursos_coordenadores.Where(x => x.id_tipo_coordenador == 1).FirstOrDefault().professores.nome, font_Verdana_10_Normal));
                //p.SpacingAfter = 20f;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(" ", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);

                doc.Add(table);

                //==================================================
                //===trecho alterado conforme e-mail de 12/04/2022 =============


                //========= tudo suprimido conforme e-mail de 26/04/2022
                //doc.NewPage();

                ////==================================================

                ////Aqui é uma nova tabela de 1 Colunas ========================================================
                //table = new PdfPTable(1);
                //table.TotalWidth = 520f;
                //table.LockedWidth = true;

                //widths = new float[] { 520f };
                //table.SetWidths(widths);

                //table.SpacingAfter = 10f;

                //cell = new PdfPCell();
                //p = new Paragraph();
                //p.Alignment = Element.ALIGN_CENTER;
                //p.Add(new Chunk("Mestrado Profissional em ", font_Verdana_12_Normal));
                //p.Add(new Chunk(item_matricula.turmas.cursos.nome, font_Verdana_12_Bold));
                //cell = new PdfPCell(p);
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.VerticalAlignment = Element.ALIGN_CENTER;
                //cell.Border = Rectangle.NO_BORDER;
                ////cell.FixedHeight = 25f;
                //table.AddCell(cell);

                //doc.Add(table);

                ////Aqui é uma nova tabela de 1 Colunas ========================================================
                //table = new PdfPTable(1);
                //table.TotalWidth = 520f;
                //table.LockedWidth = true;

                //widths = new float[] { 520f };
                //table.SetWidths(widths);

                //table.SpacingAfter = 10f;

                //cell = new PdfPCell();
                //p = new Paragraph();
                //p.Alignment = Element.ALIGN_CENTER;
                //p.Add(new Chunk("EXAME DE QUALIFICAÇÃO", font_Verdana_12_Bold));
                //cell = new PdfPCell(p);
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.VerticalAlignment = Element.ALIGN_CENTER;
                //cell.Border = Rectangle.NO_BORDER;
                ////cell.FixedHeight = 25f;
                //table.AddCell(cell);

                //doc.Add(table);

                ////Aqui é uma nova tabela de 2 Colunas ========================================================
                //table2 = new iTextSharp.text.Table(2);
                //cell2 = new Cell();
                //table2.Alignment = Rectangle.ALIGN_CENTER;
                //table2.Padding = 1;
                //table2.Spacing = 0;
                //table2.Border = Rectangle.NO_BORDER;
                //table2.BorderWidth = 0;
                //table2.Width = 90;
                //table2.DeleteAllRows();
                //table2.Widths = new Single[] { 25, 75 };
                //table2.DefaultCellBorder = 0;

                //cell2 = new Cell(new Phrase("Área de Concentração:", font_Verdana_9_Normal));
                //cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                //table2.AddCell(cell2);
                //table2.AddCell(new Cell(new Phrase(item_matricula.areas_concentracao.nome, font_Verdana_9_Bold)));

                //cell2 = new Cell(new Phrase("Aluno:", font_Verdana_9_Normal));
                //cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                //table2.AddCell(cell2);
                //table2.AddCell(new Cell(new Phrase(item.nome, font_Verdana_9_Bold)));

                //cell2 = new Cell(new Phrase("Título:", font_Verdana_9_Normal));
                //cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                //table2.AddCell(cell2);
                //table2.AddCell(new Cell(new Phrase(item_banca.titulo, font_Verdana_9_Bold)));

                //cell2 = new Cell(new Phrase("Orientador:", font_Verdana_9_Normal));
                //cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                //table2.AddCell(cell2);
                //table2.AddCell(new Cell(new Phrase(item_banca.banca_professores.Where(x => x.tipo_professor == "Orientador").FirstOrDefault().professores.nome, font_Verdana_9_Bold)));

                //doc.Add(table2);


                ////Aqui é uma nova tabela de 1 Colunas ========================================================
                //table = new PdfPTable(1);
                //table.TotalWidth = 520f;
                //table.LockedWidth = true;

                //widths = new float[] { 530f };
                //table.SetWidths(widths);

                //cell = new PdfPCell();
                //p = new Paragraph();
                //p.Alignment = Element.ALIGN_LEFT;
                //p.Add(new Chunk("Parecer Geral dos Examinadores:", font_Verdana_9_Normal));
                //p.SpacingAfter = 5f;
                //cell = new PdfPCell();
                //cell.AddElement(p);
                //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //cell.VerticalAlignment = Element.ALIGN_LEFT;
                //cell.Border = Rectangle.NO_BORDER;
                ////cell.FixedHeight = 25f;
                //table.AddCell(cell);

                //doc.Add(table);

                ////Aqui é uma nova tabela de 2 Colunas ========================================================
                //table2 = new iTextSharp.text.Table(2);

                //table2.Alignment = Rectangle.ALIGN_CENTER;
                //table2.Padding = 1;
                //table2.Spacing = 0;
                //table2.Border = Rectangle.NO_BORDER;
                //table2.BorderWidth = 0;
                //table2.Width = 100;
                //table2.DeleteAllRows();
                //table2.Widths = new Single[] { 15, 85};
                //table2.DefaultCellBorder = 0;

                //foreach (var elemento in item_banca.banca_professores.OrderBy(x => x.tipo_professor == "Membro Suplente").ThenBy(x => x.tipo_professor == "Membro").ThenBy(x => x.tipo_professor == "Co-orientador").ThenBy(x => x.tipo_professor == "Orientador").ThenBy(x => x.professores.nome))
                //{
                //    string qMenbro;
                //    if (elemento.tipo_professor == "Membro Suplente")
                //    {
                //        qMenbro = "Suplente";
                //    }
                //    else
                //    {
                //        qMenbro = elemento.tipo_professor;
                //    }
                //    cell2 = new Cell(new Phrase(qMenbro + ": ", font_Verdana_9_Normal));
                //    cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                //    table2.AddCell(cell2);
                //    cell2 = new Cell(new Phrase(elemento.professores.titulacao.reduzido + " " + elemento.professores.nome, font_Verdana_9_Bold));
                //    if (elemento.professores.empresa .Trim() != "")
                //    {
                //        cell2.Add(new Phrase(" - " + elemento.professores.empresa, font_Verdana_9_Normal));
                //    }
                //    table2.AddCell(cell2); ;
                //}

                //cell2 = new Cell(new Phrase("", font_Verdana_9_Normal));
                //table2.AddCell(cell2);
                //doc.Add(table2);


                ////Aqui é uma nova tabela de 2 Colunas ========================================================
                //table2 = new iTextSharp.text.Table(1);
                //table2.Alignment = Rectangle.ALIGN_CENTER;
                //table2.Padding = 1;
                //table2.Spacing = 0;
                //table2.Border = Rectangle.BOX;
                //table2.BorderWidth = 1;
                //table2.Width = 100;
                //table2.DeleteAllRows();
                //table2.DefaultCellBorder = 1;

                //for (int i = 0; i < 9; i++)
                //{
                //    table2.AddCell("\r\n");
                //}

                //doc.Add(table2);


                ////Aqui é uma nova tabela de 1 Colunas ========================================================
                //table = new PdfPTable(1);
                //table.TotalWidth = 520f;
                //table.LockedWidth = true;

                //widths = new float[] { 530f };
                //table.SetWidths(widths);

                ////table.SpacingAfter = 5f;

                //cell = new PdfPCell();
                //p = new Paragraph();
                //p.Alignment = Element.ALIGN_LEFT;
                //p.Add(new Chunk("\nSão Paulo, " + String.Format("{0:dd}", item_banca.horario) + " de " + dtfi.GetMonthName(Convert.ToDateTime(item_banca.horario).Month) + " de " + Convert.ToDateTime(item_banca.horario).Year, font_Verdana_10_Normal));
                //cell = new PdfPCell(p);
                //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                //cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //cell.Border = Rectangle.NO_BORDER;
                ////cell.FixedHeight = 25f;
                //table.AddCell(cell);

                //doc.Add(table);


                ////Aqui é uma nova tabela de 1 Colunas ========================================================
                //table = new PdfPTable(1);
                //table.TotalWidth = 520f;
                //table.LockedWidth = true;

                //widths = new float[] { 530f };
                //table.SetWidths(widths);

                //cell = new PdfPCell();
                //p = new Paragraph();
                //p.Alignment = Element.ALIGN_CENTER;
                //p.Add(new Chunk("\n\nAssinatura dos Examinadores:", font_Verdana_9_Normal));
                //p.SpacingAfter = 4f;
                //cell = new PdfPCell();
                //cell.AddElement(p);
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.VerticalAlignment = Element.ALIGN_CENTER;
                //cell.Border = Rectangle.NO_BORDER;
                ////cell.FixedHeight = 25f;
                //table.AddCell(cell);

                //doc.Add(table);


                ////Aqui é uma nova tabela de 2 Colunas ========================================================
                //table2 = new iTextSharp.text.Table(2);

                //table2.Alignment = Rectangle.ALIGN_CENTER;
                //table2.Padding = 1;
                //table2.Spacing = 0;
                //table2.Border = Rectangle.BOX;
                //table2.BorderWidth = 1;
                //table2.Width = 100;
                //table2.DeleteAllRows();
                //table2.Widths = new Single[] { 15, 85 };
                //table2.DefaultCellBorder = 1;

                //foreach (var elemento in item_banca.banca_professores.OrderBy(x => x.tipo_professor == "Membro Suplente").ThenBy(x => x.tipo_professor == "Membro").ThenBy(x => x.tipo_professor == "Co-orientador").ThenBy(x => x.tipo_professor == "Orientador").ThenBy(x => x.professores.nome))
                //{
                //    string qMenbro;
                //    if (elemento.tipo_professor == "Membro Suplente")
                //    {
                //        qMenbro = "Suplente";
                //    }
                //    else
                //    {
                //        qMenbro = elemento.tipo_professor;
                //    }
                //    cell2 = new Cell(new Phrase(qMenbro + ": ", font_Verdana_9_Normal));
                //    cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                //    cell2.Border = Rectangle.BOX;
                //    table2.AddCell(cell2);
                //    table2.AddCell("\r\n");
                //}

                //doc.Add(table2);

                //========= tudo suprimido conforme e-mail de 26/04/2022

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão da Ata da Qualificação ";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }

        }

        protected void GeraAtaDefesa(string qBanca, string qIdTurma)
        {
            try
            {
                GeraisAplicacao AplicacaoGerais = new GeraisAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request.Form["hQTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma item_matricula = new matricula_turma();
                item_matricula = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).SingleOrDefault();
                banca item_banca;
                item_banca = item_matricula.banca.Where(x => x.tipo_banca == qBanca).FirstOrDefault();

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 120, 40);//estibulando o espaçamento das margens que queremos ===Antigo===(45, 40, 40, 40)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/Ata_" + qBanca + "_" + qIdTurma + ".pdf"), FileMode.Create));
                PDF_Cabec_Rodape_GeraEmentaPDF pageHeaderFooter = new PDF_Cabec_Rodape_GeraEmentaPDF();
                pageHeaderFooter.Caminho = Server.MapPath("~");
                pageHeaderFooter.qTipoCurso = item_matricula.turmas.cursos.tipos_curso.tipo_curso;

                writer.PageEvent = pageHeaderFooter;
                doc.Open();

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_11_Bold = new Font(_bfVerdanaNormal, 11, Font.BOLD);
                Font font_Verdana_11_Normal = new Font(_bfVerdanaNormal, 11, Font.NORMAL);
                Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Verdana_8_Bold = new Font(_bfVerdanaNormal, 8, Font.BOLD);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Verdana_7_Bold = new Font(_bfVerdanaNormal, 7, Font.BOLD);
                Font font_Verdana_7_Normal = new Font(_bfVerdanaNormal, 7, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);
                Color FontColor_Cinza = new Color(204, 204, 204);
                Color FontColor_CinzaClaro = new Color(229, 229, 229);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                float[] widths;
                Paragraph p;
                PdfPCell cell;
                PdfPTable table;

                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                table.SpacingAfter = 10f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(item_matricula.turmas.cursos.tipos_curso.tipo_curso + " em ", font_Verdana_12_Normal));
                p.Add(new Chunk(item_matricula.turmas.cursos.nome, font_Verdana_12_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                table.SpacingAfter = 10f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;

                if (item_matricula.turmas.cursos.id_tipo_curso == 1)
                {
                    p.Add(new Chunk("DEFESA DE DISSERTAÇÃO", font_Verdana_12_Bold));
                }
                else if (item_matricula.turmas.cursos.id_tipo_curso == 3)
                {
                    p.Add(new Chunk("DEFESA DE MONOGRAFIA", font_Verdana_12_Bold));
                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 2 Colunas ========================================================
                iTextSharp.text.Table table2 = new iTextSharp.text.Table(2);
                Cell cell2 = new Cell();
                table2.Alignment = Rectangle.ALIGN_CENTER;
                table2.Padding = 1;
                table2.Spacing = 0;
                table2.Border = Rectangle.NO_BORDER;
                table2.BorderWidth = 0;
                table2.Width = 90;
                table2.DeleteAllRows();
                table2.Widths = new Single[] { 25, 75 };
                table2.DefaultCellBorder = 0;

                
                if (item_matricula.areas_concentracao != null)
                {
                    cell2 = new Cell(new Phrase("Área de Concentração:", font_Verdana_9_Normal));
                    cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    table2.AddCell(cell2);
                    table2.AddCell(new Cell(new Phrase(item_matricula.areas_concentracao.nome, font_Verdana_9_Bold)));
                }
                else
                {
                    cell2 = new Cell(new Phrase("", font_Verdana_9_Normal));
                    cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    table2.AddCell(cell2);
                    table2.AddCell(new Cell(new Phrase("", font_Verdana_9_Bold)));
                }
                

                cell2 = new Cell(new Phrase("Aluno:", font_Verdana_9_Normal));
                cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table2.AddCell(cell2);
                table2.AddCell(new Cell(new Phrase(item.nome, font_Verdana_9_Bold)));

                cell2 = new Cell(new Phrase("Título:", font_Verdana_9_Normal));
                cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table2.AddCell(cell2);
                table2.AddCell(new Cell(new Phrase(item_banca.titulo, font_Verdana_9_Bold)));

                cell2 = new Cell(new Phrase("Orientador:", font_Verdana_9_Normal));
                cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table2.AddCell(cell2);
                table2.AddCell(new Cell(new Phrase(item_banca.banca_professores.Where(x => x.tipo_professor == "Orientador").FirstOrDefault().professores.nome, font_Verdana_9_Bold)));

                doc.Add(table2);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 530f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;

                //===
                string sRemoto = "";
                if (item_banca.remoto == 1)
                {
                    sRemoto = ", remotamente,";
                }
               //====

                if (item_matricula.turmas.cursos.id_tipo_curso == 1)
                {
                    p.Add(new Chunk("\n\nEm " + String.Format("{0:dd/MM/yyyy}", item_banca.horario) + " às " + String.Format("{0: HH:mm}", item_banca.horario) + ", foi realizado" + sRemoto + " a defesa de dissertação do aluno(a) acima, com o seguinte resultado", font_Verdana_9_Normal));

                }
                else if (item_matricula.turmas.cursos.id_tipo_curso == 3)
                {
                    p.Add(new Chunk("\n\nEm " + String.Format("{0:dd/MM/yyyy}", item_banca.horario) + " às " + String.Format("{0: HH:mm}", item_banca.horario) + ", foi realizado" + sRemoto + " a defesa de monografia do aluno(a) acima, com o seguinte resultado", font_Verdana_9_Normal));
                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 530f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("Comissão Julgadora:", font_Verdana_9_Normal));
                p.SpacingAfter = 4f;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table2 = new iTextSharp.text.Table(4);

                table2.Alignment = Rectangle.ALIGN_CENTER;
                table2.Padding = 1;
                table2.Spacing = 0;
                table2.Border = Rectangle.BOX;
                table2.BorderWidth = 1;
                table2.Width = 100;
                table2.DeleteAllRows();
                table2.Widths = new Single[] { 15, 65, 10, 10 };
                table2.DefaultCellBorder = 1;

                table2.AddCell(" ");
                cell2 = new Cell(new Phrase("Examinadores", font_Verdana_9_Bold));
                cell2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell2.Border = Rectangle.BOX;
                table2.AddCell(cell2);
                cell2 = new Cell(new Phrase("Aprovado", font_Verdana_9_Bold));
                cell2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell2.Border = Rectangle.BOX;
                table2.AddCell(cell2);
                cell2 = new Cell(new Phrase("Reprovado", font_Verdana_9_Bold));
                cell2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell2.Border = Rectangle.BOX;
                table2.AddCell(cell2);
                foreach (var elemento in item_banca.banca_professores.OrderBy(x => x.tipo_professor == "Membro Suplente").ThenBy(x => x.tipo_professor == "Membro").ThenBy(x => x.tipo_professor == "Co-orientador").ThenBy(x => x.tipo_professor == "Orientador").ThenBy(x => x.professores.nome))
                {
                    string qMenbro;
                    if (elemento.tipo_professor == "Membro Suplente")
                    {
                        qMenbro = "Suplente";
                    }
                    else
                    {
                        qMenbro = elemento.tipo_professor;
                    }
                    cell2 = new Cell(new Phrase(qMenbro + ": ", font_Verdana_9_Normal));
                    cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell2.Border = Rectangle.BOX;
                    table2.AddCell(cell2);
                    cell2 = new Cell(new Phrase(elemento.professores.titulacao.reduzido + " " + elemento.professores.nome, font_Verdana_9_Bold));
                    if (elemento.professores.empresa.Trim() != "")
                    {
                        cell2.Add(new Phrase(" - " + elemento.professores.empresa, font_Verdana_9_Normal));
                    }

                    cell2.Border = Rectangle.BOX;
                    table2.AddCell(cell2);
                    cell2 = new Cell(" ");
                    cell2.Border = Rectangle.BOX;
                    table2.AddCell(cell2);
                    table2.AddCell(" ");
                }

                doc.Add(table2);

                //==== trecho alterado conforme e-mail de 12/04/2022 =============
                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 530f };
                table.SetWidths(widths);

                //cell = new PdfPCell();
                //p = new Paragraph();
                //p.Alignment = Element.ALIGN_LEFT;
                //p.Add(new Chunk("\n\nParecer da banca em anexo.", font_Verdana_9_Normal));
                //cell = new PdfPCell(p);
                //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //cell.VerticalAlignment = Element.ALIGN_LEFT;
                //cell.Border = Rectangle.NO_BORDER;
                ////cell.FixedHeight = 25f;
                //table.AddCell(cell);

                //cell = new PdfPCell();
                //p = new Paragraph();
                //p.Alignment = Element.ALIGN_CENTER;
                //p.Add(new Chunk("Assinatura dos Examinadores:", font_Verdana_9_Normal));
                //p.SpacingAfter = 4f;
                //cell = new PdfPCell();
                //cell.AddElement(p);
                //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //cell.VerticalAlignment = Element.ALIGN_CENTER;
                //cell.Border = Rectangle.NO_BORDER;
                ////cell.FixedHeight = 25f;
                //table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                p.Add(new Chunk("\nAs orientações foram repassadas ao aluno pelos membros da banca durante a argüição.", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);
                //==== trecho alterado conforme e-mail de 12/04/2022 =============

                //==== suprimido conforme e-mail de 12/04/2022 =============
                //Aqui é uma nova tabela de 2 Colunas ========================================================
                //table2 = new iTextSharp.text.Table(2);

                //table2.Alignment = Rectangle.ALIGN_CENTER;
                //table2.Padding = 1;
                //table2.Spacing = 0;
                //table2.Border = Rectangle.BOX;
                //table2.BorderWidth = 1;
                //table2.Width = 100;
                //table2.DeleteAllRows();
                //table2.Widths = new Single[] { 15, 85 };
                //table2.DefaultCellBorder = 1;

                //foreach (var elemento in item_banca.banca_professores.OrderBy(x => x.tipo_professor == "Membro Suplente").ThenBy(x => x.tipo_professor == "Membro").ThenBy(x => x.tipo_professor == "Co-orientador").ThenBy(x => x.tipo_professor == "Orientador").ThenBy(x => x.professores.nome))
                //{
                //    string qMenbro;
                //    if (elemento.tipo_professor == "Membro Suplente")
                //    {
                //        qMenbro = "Suplente";
                //    }
                //    else
                //    {
                //        qMenbro = elemento.tipo_professor;
                //    }

                //    cell2 = new Cell(new Phrase(qMenbro, font_Verdana_9_Normal));
                //    cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                //    cell2.Border = Rectangle.BOX;
                //    table2.AddCell(cell2);
                //    table2.AddCell("\r\n");
                //}

                //doc.Add(table2);
                //==== suprimido conforme e-mail de 12/04/2022 =============

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 530f };
                table.SetWidths(widths);

                table.SpacingAfter = 30f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                p.Add(new Chunk("\nSão Paulo, " + String.Format("{0:dd}", item_banca.horario) + " de " + dtfi.GetMonthName(Convert.ToDateTime(item_banca.horario).Month) + " de " + Convert.ToDateTime(item_banca.horario).Year, font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                if (txtObsAta.Value != "")
                {
                    //Aqui é uma nova tabela de 1 Colunas ========================================================
                    table = new PdfPTable(1);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;

                    widths = new float[] { 530f };
                    table.SetWidths(widths);

                    table.SpacingAfter = 30f;

                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_LEFT;
                    p.Add(new Chunk(txtObsAta.Value, font_Verdana_9_Normal));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    doc.Add(table);
                }
                
                //Aqui é uma nova tabela de 3 Colunas ========================================================
                table = new PdfPTable(3);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 50f, 180f, 50f };
                table.SetWidths(widths);

                table.SpacingAfter = 10f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(" ", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;

                //===trecho alterado conforme e-mail de 12/04/2022 =============
                p.Add(new Chunk("\n\n", font_Verdana_10_Normal));
                p.Add(new Chunk(item_matricula.turmas.cursos.tipos_curso.tipo_curso + " Profissional em " + item_matricula.turmas.cursos.nome + "\n\n\n", font_Verdana_10_Normal));
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\nOrientador: " + item_banca.banca_professores.Where(x => x.tipo_professor == "Orientador").FirstOrDefault().professores.nome, font_Verdana_10_Normal));

                p.Add(new Chunk("\n\n \n", font_Verdana_10_Normal));
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\nCoordenador: " + item_matricula.turmas.cursos.cursos_coordenadores.Where(x => x.id_tipo_coordenador == 1).FirstOrDefault().professores.nome, font_Verdana_10_Normal));
                //===trecho alterado conforme e-mail de 12/04/2022 =============

                //p.SpacingAfter = 20f;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(" ", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);

                doc.Add(table);

                //==================================================



                //==================================================
                //========= tudo suprimido conforme e-mail de 26/04/2022

                //foreach (var elemento in item_banca.banca_professores.OrderBy(x => x.tipo_professor == "Membro Suplente").ThenBy(x => x.tipo_professor == "Membro").ThenBy(x => x.tipo_professor == "Co-orientador").ThenBy(x => x.tipo_professor == "Orientador").ThenBy(x => x.professores.nome))
                //{
                //    doc.NewPage();

                //    //Aqui é uma nova tabela de 1 Colunas ========================================================
                //    table = new PdfPTable(1);
                //    table.TotalWidth = 520f;
                //    table.LockedWidth = true;

                //    widths = new float[] { 520f };
                //    table.SetWidths(widths);

                //    table.SpacingAfter = 10f;

                //    cell = new PdfPCell();
                //    p = new Paragraph();
                //    p.Alignment = Element.ALIGN_CENTER;
                //    p.Add(new Chunk(item_matricula.turmas.cursos.tipos_curso.tipo_curso + " em ", font_Verdana_12_Normal));
                //    p.Add(new Chunk(item_matricula.turmas.cursos.nome, font_Verdana_12_Bold));
                //    cell = new PdfPCell(p);
                //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //    cell.VerticalAlignment = Element.ALIGN_CENTER;
                //    cell.Border = Rectangle.NO_BORDER;
                //    //cell.FixedHeight = 25f;
                //    table.AddCell(cell);

                //    doc.Add(table);

                //    //Aqui é uma nova tabela de 1 Colunas ========================================================
                //    table = new PdfPTable(1);
                //    table.TotalWidth = 520f;
                //    table.LockedWidth = true;

                //    widths = new float[] { 520f };
                //    table.SetWidths(widths);

                //    table.SpacingAfter = 10f;

                //    cell = new PdfPCell();
                //    p = new Paragraph();
                //    p.Alignment = Element.ALIGN_CENTER;
                //    if (item_matricula.turmas.cursos.id_tipo_curso == 1)
                //    {
                //        p.Add(new Chunk("DEFESA DE DISSERTAÇÃO", font_Verdana_12_Bold));
                //    }
                //    else if (item_matricula.turmas.cursos.id_tipo_curso == 3)
                //    {
                //        p.Add(new Chunk("DEFESA DE MONOGRAFIA", font_Verdana_12_Bold));
                //    }
                //    cell = new PdfPCell(p);
                //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //    cell.VerticalAlignment = Element.ALIGN_CENTER;
                //    cell.Border = Rectangle.NO_BORDER;
                //    //cell.FixedHeight = 25f;
                //    table.AddCell(cell);

                //    doc.Add(table);

                //    //Aqui é uma nova tabela de 2 Colunas ========================================================
                //    table2 = new iTextSharp.text.Table(2);
                //    cell2 = new Cell();
                //    table2.Alignment = Rectangle.ALIGN_CENTER;
                //    table2.Padding = 1;
                //    table2.Spacing = 0;
                //    table2.Border = Rectangle.NO_BORDER;
                //    table2.BorderWidth = 0;
                //    table2.Width = 90;
                //    table2.DeleteAllRows();
                //    table2.Widths = new Single[] { 25, 75 };
                //    table2.DefaultCellBorder = 0;


                //    if (item_matricula.areas_concentracao != null)
                //    {
                //        cell2 = new Cell(new Phrase("Área de Concentração:", font_Verdana_9_Normal));
                //        cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                //        table2.AddCell(cell2);
                //        table2.AddCell(new Cell(new Phrase(item_matricula.areas_concentracao.nome, font_Verdana_9_Bold)));
                //    }
                //    else
                //    {
                //        cell2 = new Cell(new Phrase("", font_Verdana_9_Normal));
                //        cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                //        table2.AddCell(cell2);
                //        table2.AddCell(new Cell(new Phrase("", font_Verdana_9_Bold)));
                //    }

                //    cell2 = new Cell(new Phrase("Aluno:", font_Verdana_9_Normal));
                //    cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                //    table2.AddCell(cell2);
                //    table2.AddCell(new Cell(new Phrase(item.nome, font_Verdana_9_Bold)));

                //    cell2 = new Cell(new Phrase("Título:", font_Verdana_9_Normal));
                //    cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                //    table2.AddCell(cell2);
                //    table2.AddCell(new Cell(new Phrase(item_banca.titulo, font_Verdana_9_Bold)));

                //    cell2 = new Cell(new Phrase("Orientador:", font_Verdana_9_Normal));
                //    cell2.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                //    table2.AddCell(cell2);
                //    table2.AddCell(new Cell(new Phrase(item_banca.banca_professores.Where(x => x.tipo_professor == "Orientador").FirstOrDefault().professores.titulacao.reduzido + " " + item_banca.banca_professores.Where(x => x.tipo_professor == "Orientador").FirstOrDefault().professores.nome, font_Verdana_9_Bold)));

                //    doc.Add(table2);


                //    //Aqui é uma nova tabela de 1 Colunas ========================================================
                //    table = new PdfPTable(1);
                //    table.TotalWidth = 520f;
                //    table.LockedWidth = true;

                //    widths = new float[] { 530f };
                //    table.SetWidths(widths);

                //    cell = new PdfPCell();
                //    p = new Paragraph();
                //    p.Alignment = Element.ALIGN_LEFT;
                //    p.Add(new Chunk("\nParecer do Examinador: ", font_Verdana_9_Normal));
                //    p.Add(new Chunk(elemento.professores.titulacao.reduzido + " " + elemento.professores.nome, font_Verdana_9_Bold));
                //    p.SpacingAfter = 5f;
                //    cell = new PdfPCell();
                //    cell.AddElement(p);
                //    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //    cell.VerticalAlignment = Element.ALIGN_LEFT;
                //    cell.Border = Rectangle.NO_BORDER;
                //    //cell.FixedHeight = 25f;
                //    table.AddCell(cell);

                //    doc.Add(table);


                //    //Aqui é uma nova tabela de 1 Colunas ========================================================
                //    table2 = new iTextSharp.text.Table(1);
                //    table2.Alignment = Rectangle.ALIGN_CENTER;
                //    table2.Padding = 1;
                //    table2.Spacing = 0;
                //    table2.Border = Rectangle.BOX;
                //    table2.BorderWidth = 1;
                //    table2.Width = 100;
                //    table2.DeleteAllRows();
                //    table2.DefaultCellBorder = 1;

                //    for (int i = 0; i < 16; i++)
                //    {
                //        table2.AddCell("\r\n");
                //    }

                //    doc.Add(table2);


                //    //Aqui é uma nova tabela de 1 Colunas ========================================================
                //    table = new PdfPTable(1);
                //    table.TotalWidth = 520f;
                //    table.LockedWidth = true;

                //    widths = new float[] { 530f };
                //    table.SetWidths(widths);

                //    table.SpacingAfter = 30f;

                //    cell = new PdfPCell();
                //    p = new Paragraph();
                //    p.Alignment = Element.ALIGN_LEFT;
                //    p.Add(new Chunk("\nSão Paulo, " + String.Format("{0:dd}", item_banca.horario) + " de " + dtfi.GetMonthName(Convert.ToDateTime(item_banca.horario).Month) + " de " + Convert.ToDateTime(item_banca.horario).Year, font_Verdana_10_Normal));
                //    cell = new PdfPCell(p);
                //    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                //    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //    cell.Border = Rectangle.NO_BORDER;
                //    //cell.FixedHeight = 25f;
                //    table.AddCell(cell);

                //    doc.Add(table);


                //    //Aqui é uma nova tabela de 3 Colunas ========================================================
                //    table = new PdfPTable(3);
                //    table.TotalWidth = 520f;
                //    table.LockedWidth = true;
                //    widths = new float[] { 63f, 180f, 63f };
                //    table.SetWidths(widths);

                //    table.SpacingAfter = 10f;

                //    cell = new PdfPCell();
                //    p = new Paragraph();
                //    p.Add(new Chunk(" ", font_Verdana_9_Normal));
                //    cell = new PdfPCell(p);
                //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //    cell.Border = Rectangle.NO_BORDER;
                //    table.AddCell(cell);

                //    cell = new PdfPCell();
                //    p = new Paragraph();
                //    p.Alignment = Element.ALIGN_CENTER;
                //    p.Add(new Chunk(linefine));
                //    p.Add(new Chunk("\nExaminador\n", font_Verdana_10_Normal));
                //    p.Add(new Chunk(elemento.professores.titulacao.reduzido + " " + elemento.professores.nome, font_Verdana_9_Bold));
                //    //p.SpacingAfter = 20f;
                //    cell = new PdfPCell();
                //    cell.AddElement(p);
                //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //    cell.Border = Rectangle.NO_BORDER;
                //    //cell.FixedHeight = 25f;
                //    table.AddCell(cell);

                //    cell = new PdfPCell();
                //    p = new Paragraph();
                //    p.Add(new Chunk(" ", font_Verdana_9_Normal));
                //    cell = new PdfPCell(p);
                //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                //    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //    cell.Border = Rectangle.NO_BORDER;
                //    table.AddCell(cell);

                //    doc.Add(table);

                //}
                //========= tudo suprimido conforme e-mail de 26/04/2022


                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão da Ata da Qualificação ";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }

        }

        protected void btnImprimirContrato_Click(object sender, EventArgs e)
        {
            var qTipoContrato = HttpContext.Current.Request["ddlTipoContrato"];
            var qDataContrato = HttpContext.Current.Request["txtDataContrato"];
            var qValorTotal = HttpContext.Current.Request["txtValorTotal"];
            var qValorDisciplina = HttpContext.Current.Request["txtValorDisciplina"];
            var qNumeroParcela = HttpContext.Current.Request["txtNumeroParcela"];
            var qValorParcela = HttpContext.Current.Request["txtValorParcela"];
            var qDataInicioCurso = HttpContext.Current.Request["txtDataInicioCurso"];
            var qPrazo = HttpContext.Current.Request["txtPrazo"];
            var qCoordenador = HttpContext.Current.Request["txtCoordenador"];
            var qSecretaria = HttpContext.Current.Request["txtSecretaria"];
            var qTextemunha1 = HttpContext.Current.Request["txtTextemunha1"];
            var qRGTextemunha1 = HttpContext.Current.Request["txtRGTextemunha1"];
            var qTextemunha2 = HttpContext.Current.Request["txtTextemunha2"];
            var qRGTextemunha2 = HttpContext.Current.Request["txtRGTextemunha2"];
            var qParagrafoDiretor = HttpContext.Current.Request["txtParagrafoDiretor"];

            var sAux = HttpContext.Current.Request["hCodigo"].ToString().Split(',');

            var qIdTurma = sAux[0];
            var qNome = sAux[1].Replace(" ", "_");


            string sAux2 = "";

            if (qTipoContrato.Trim() == "")
            {
                sAux2 = sAux2 + "Deve-se selecionar o Tipo de Contrato. <br/><br/>";
            }

            if (qDataContrato.Trim() == "")
            {
                sAux2 = sAux2 + "Deve-se digitar a Data do Contrato. <br/><br/>";
            }

            if (qValorTotal.Trim() == "")
            {
                sAux2 = sAux2 + "Deve-se digitar o Valor Total do Contrato. <br/><br/>";
            }

            if (qValorDisciplina.Trim() == "")
            {
                sAux2 = sAux2 + "Deve-se digitar o Valor da Disiplina do Contrato. <br/><br/>";
            }

            if (qNumeroParcela.Trim() == "")
            {
                sAux2 = sAux2 + "Deve-se digitar o Número de Parcelas do Contrato. <br/><br/>";
            }

            if (qValorParcela.Trim() == "")
            {
                sAux2 = sAux2 + "Deve-se digitar o Valor da Parcela do Contrato. <br/><br/>";
            }

            if (qDataInicioCurso.Trim() == "")
            {
                sAux2 = sAux2 + "Deve-se digitar a Data de Início do Curso. <br/><br/>";
            }
            if (qPrazo.Trim() == "")
            {
                sAux2 = sAux2 + "Deve-se digitar o Prazo. <br/><br/>";
            }

            if (qCoordenador.Trim() == "")
            {
                sAux2 = sAux2 + "Deve-se digitar o Coordenador do Curso. <br/><br/>";
            }

            if (qSecretaria.Trim() == "")
            {
                sAux2 = sAux2 + "Deve-se digitar a Secretária. <br/><br/>";
            }

            if (qTextemunha1.Trim() == "")
            {
                sAux2 = sAux2 + "Deve-se digitar a Testemunha #1. <br/><br/>";
            }

            if (qRGTextemunha1.Trim() == "")
            {
                sAux2 = sAux2 + "Deve-se digitar o RG da Testemunha #1. <br/><br/>";
            }

            if (qTextemunha2.Trim() == "")
            {
                sAux2 = sAux2 + "Deve-se digitar a Testemunha #2. <br/><br/>";
            }

            if (qRGTextemunha2.Trim() == "")
            {
                sAux2 = sAux2 + "Deve-se digitar o RG da Testemunha #2. <br/><br/>";
            }

            if (qParagrafoDiretor.Trim() == "")
            {
                sAux2 = sAux2 + "Deve-se digitar o Parágrafo do Diretor. <br/><br/>";
            }
            
            if (sAux2 != "")
            {
                lblMensagem.Text = sAux2;
                lblTituloMensagem.Text = "Atenção";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-warning');", true);
                return;
            }

            try
            {

                alunos item;
                string qTab = HttpContext.Current.Request.Form["hQTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma item_matricula = new matricula_turma();
                item_matricula = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).SingleOrDefault();

                if (qTipoContrato != "Tecnologia de Fundição")
                {
                    if (item_matricula.turmas.cursos.id_tipo_curso == 2 || item_matricula.turmas.cursos.id_tipo_curso == 3 || item_matricula.turmas.cursos.id_tipo_curso == 4)
                    {
                        GeraContratoEspc_Curta(qTipoContrato, qDataContrato, qValorTotal, qValorDisciplina, qNumeroParcela,
                 qValorParcela, qDataInicioCurso, qPrazo, qCoordenador, qSecretaria, qTextemunha1, qRGTextemunha1,
                 qTextemunha2, qRGTextemunha2, qParagrafoDiretor, qIdTurma, qNome, item_matricula.turmas.cursos.id_tipo_curso);
                    }
                    else
                    {
                        if (qTipoContrato.IndexOf("Convênio") == -1)
                        {
                            GeraContratoGeral(qTipoContrato, qDataContrato, qValorTotal, qValorDisciplina, qNumeroParcela,
                 qValorParcela, qDataInicioCurso, qPrazo, qCoordenador, qSecretaria, qTextemunha1, qRGTextemunha1,
                 qTextemunha2, qRGTextemunha2, qParagrafoDiretor, qIdTurma, qNome);
                        }
                        else
                        {
                            GeraContratoGeralConvenio(qTipoContrato, qDataContrato, qValorTotal, qValorDisciplina, qNumeroParcela,
                 qValorParcela, qDataInicioCurso, qPrazo, qCoordenador, qSecretaria, qTextemunha1, qRGTextemunha1,
                 qTextemunha2, qRGTextemunha2, qParagrafoDiretor, qIdTurma, qNome);
                        }
                        
                    }
                    
                }
                else
                {
                    GeraContratoFundicao(qTipoContrato, qDataContrato, qValorTotal, qValorDisciplina, qNumeroParcela,
                 qValorParcela, qDataInicioCurso, qPrazo, qCoordenador, qSecretaria, qTextemunha1, qRGTextemunha1,
                 qTextemunha2, qRGTextemunha2, qParagrafoDiretor, qIdTurma, qNome);
                    
                    
                }

                if (File.Exists(Server.MapPath("~/doctos/Contrato_" + qNome + ".pdf")))
                {
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/Contrato_" + qNome + ".pdf")));
                    Response.WriteFile(Server.MapPath("~/doctos/Contrato_" + qNome + ".pdf"));
                    Response.Flush();
                    Response.End();
                }

                
            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão da Ata de " + "";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }
        }

        protected void GeraContratoEspc_Curta(string qTipoContrato, string qDataContrato, string qValorTotal, string qValorDisciplina, string qNumeroParcela,
            string qValorParcela, string qDataInicioCurso, string qPrazo, string qCoordenador, string qSecretaria, string qTextemunha1, string qRGTextemunha1,
            string qTextemunha2, string qRGTextemunha2, string qParagrafoDiretor, string qIdTurma, string qNome, int qIdTipoCurso)
        {
            try
            {
                bool bNovo = false;
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                GeraisAplicacao AplicacaoGerais = new GeraisAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request.Form["hQTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma item_matricula = new matricula_turma();
                item_matricula = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).SingleOrDefault();

                dados_contratos item_contato = new dados_contratos();
                item_contato.nome_contrato = qTipoContrato;
                item_contato = AplicacaoGerais.BuscaContrato(item_contato);

                if (item_contato == null)
                {
                    bNovo = true;
                    item_contato = new dados_contratos();
                    item_contato.nome_contrato = item_matricula.turmas.cursos.nome;
                    item_contato.id_curso = item_matricula.turmas.cursos.id_curso;
                    item_contato.convenio = false;
                    item_contato.data_cadastro = DateTime.Now;
                }

                Enderecos item_endereco = new Enderecos();
                item_endereco.id_endereco = 1; // FIPT
                item_endereco = AplicacaoGerais.BuscaEndereco(item_endereco);

                item_contato.valor_total = Convert.ToDecimal(qValorTotal);
                item_contato.num_parcelas = Convert.ToInt32(qNumeroParcela);
                //if (qValorParcela == null)
                //{
                //    item_contato.valor_parcela = Convert.ToDecimal(qValorParcela);
                //}
                //else
                //{
                //    item_contato.valor_parcela = 0;
                //}
                item_contato.valor_parcela = Convert.ToDecimal(qValorParcela);
                item_contato.valor_disciplina = Convert.ToDecimal(qValorDisciplina);
                item_contato.prazo = Convert.ToInt32(qPrazo);
                item_contato.inicio = Convert.ToDateTime(qDataInicioCurso);
                item_contato.coordenador = qCoordenador;
                item_contato.secretaria = qSecretaria;
                item_contato.testemunha_1 = qTextemunha1;
                item_contato.testemunha_2 = qTextemunha2;
                item_contato.rg_testemunha_1 = qRGTextemunha1;
                item_contato.rg_testemunha_2 = qRGTextemunha2;
                item_contato.diretor = qParagrafoDiretor;
                item_contato.status = "alterado";
                item_contato.data_alteracao = DateTime.Now;
                item_contato.usuario = usuario.usuario;

                if (bNovo)
                {
                    item_contato.status = "cadastrado";
                    item_contato = AplicacaoGerais.CriaContrato(item_contato);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fPreencheComboContrato('" + qIdTurma + "');", true);
                    
                }
                else
                {
                    item_contato = AplicacaoGerais.AlterarContrato(item_contato);
                }

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 120, 50);//estibulando o espaçamento das margens que queremos ===Antigo===(45, 40, 40, 40)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/Contrato_" + qNome + ".pdf"), FileMode.Create));
                PDF_Cabec_Rodape_Contrato pageHeaderFooter = new PDF_Cabec_Rodape_Contrato();
                pageHeaderFooter.Caminho = Server.MapPath("~");
                pageHeaderFooter.qPagina = 1;
                writer.PageEvent = pageHeaderFooter;
                doc.Open();

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_11_Bold = new Font(_bfVerdanaNormal, 11, Font.BOLD);
                Font font_Verdana_11_Normal = new Font(_bfVerdanaNormal, 11, Font.NORMAL);
                Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Verdana_8_Bold = new Font(_bfVerdanaNormal, 8, Font.BOLD);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Verdana_7_Bold = new Font(_bfVerdanaNormal, 7, Font.BOLD);
                Font font_Verdana_7_Normal = new Font(_bfVerdanaNormal, 7, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);
                Color FontColor_Cinza = new Color(204, 204, 204);
                Color FontColor_CinzaClaro = new Color(229, 229, 229);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                float[] widths;
                Paragraph p;
                PdfPCell cell;
                PdfPTable table;

                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;

                //Aqui é uma nova tabela de 2 Colunas ========================================================
                //table2 = new iTextSharp.text.Table(1);
                //cell2 = new Cell();
                //table2.Alignment = Rectangle.ALIGN_CENTER;
                //table2.Padding = 1;
                //table2.Spacing = 0;
                //table2.Border = Rectangle.BOX;
                //table2.BorderWidth = 1;
                //table2.Width = 100;
                //table2.DeleteAllRows();
                //table2.Widths = new Single[] { 100 };
                //table2.DefaultCellBorder = 1;

                //ph = new Phrase();
                //p = new Paragraph();
                //p.Add(new Chunk("Coordenadoria de Ensino Tecnológico - CET\n", font_Verdana_12_Bold));
                //p.Add(new Chunk("Contrato de Prestação de Serviços Educacionais de Mestrado\n", font_Verdana_12_Normal));
                //p.Add(new Chunk("Profissional em " + item_matricula.turmas.cursos.nome, font_Verdana_12_Normal));
                //p.SpacingAfter = 100;
                //p.SetLeading(12, 10);
                //cell2 = new Cell();
                //cell2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                ////cell2.Add(p);
                //cell2.AddElement(p);
                //table2.AddCell(cell2);

                //doc.Add(table2);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 530f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                //p.Add(new Chunk("Coordenadoria de Ensino Tecnológico - CET\n", font_Verdana_11_Bold));
                p.Add(new Chunk(" \n", font_Verdana_11_Bold));
                if (qIdTipoCurso == 3) //3=Especialização
                {
                    p.Add(new Chunk("Contrato de Prestação de Serviços Educacionais de Especialização\n", font_Verdana_11_Normal));
                    p.Add(new Chunk(" em " + item_matricula.turmas.cursos.nome, font_Verdana_11_Normal));
                }
                else if (qIdTipoCurso == 4) //3=Curta Duração
                {
                    p.Add(new Chunk("Contrato de Prestação de Serviços Educacionais de Curta Duração\n", font_Verdana_11_Normal));
                    p.Add(new Chunk(" em " + item_matricula.turmas.cursos.nome, font_Verdana_11_Normal));
                }
                else
                {
                    p.Add(new Chunk("Contrato de Prestação de Serviços Educacionais de MBA Internacional\n", font_Verdana_11_Normal));
                    p.Add(new Chunk(" em " + item_matricula.turmas.cursos.nome, font_Verdana_11_Normal));
                }
                
                //p.SpacingAfter = 100;
                //p.SetLeading(12, 10);
                p.SpacingAfter = 4f;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 2;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;
                p.Add(new Chunk("O ", font_Verdana_8_Normal));
                p.Add(new Chunk("INSTITUTO DE PESQUISAS TECNOLÓGICAS DO ESTADO DE SÃO PAULO S/A - IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", empresa pública constituída nos termos da Lei Estadual no. 896/75, com sede na Capital do Estado de São Paulo, na Cidade Universitária “Armando de Salles Oliveira”, inscrito CNPJ/MF nº. 60.633.674/0001-55 e Inscrição Estadual nº. 105.933.432.110, neste ato representado por seus Procuradores abaixo assinados, doravante designados simplesmente ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", e Aluno:", font_Verdana_8_Normal));
                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 3 Colunas ========================================================
                table = new PdfPTable(3);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 84 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Nome:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.nome, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 7 Colunas ========================================================
                table = new PdfPTable(7);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 39, 1, 14, 1, 30 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("RG:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                string sAux;
                if (item.digito_num_documento != "" && item.digito_num_documento != null)
                {
                    sAux = item.numero_documento + "-" + item.digito_num_documento;
                }
                else
                {
                    sAux = item.numero_documento;
                }

                p.Add(new Chunk(sAux, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("CPF:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.cpf, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 7 Colunas ========================================================
                table = new PdfPTable(7);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 39, 1, 14, 1, 30 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Formação:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.formacao, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Nacionalidade:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.pais_nasc, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 7 Colunas ========================================================
                table = new PdfPTable(7);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 39, 1, 14, 1, 30 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Endereço:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.logradouro_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Nº:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.numero_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 7 Colunas ========================================================
                table = new PdfPTable(7);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 39, 1, 14, 1, 30 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Complemento:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.complemento_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Bairro:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.bairro_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 7 Colunas ========================================================
                table = new PdfPTable(7);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 39, 1, 14, 1, 30 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("CEP:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.cep_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("UF:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.uf_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 3 Colunas ========================================================
                table = new PdfPTable(3);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 84 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Cidade:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.cidade_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                //table.SpacingAfter = 15f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;
                p.Add(new Chunk("doravante designado simplesmente ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", celebram o presente ", font_Verdana_8_Normal));
                if (qIdTipoCurso == 3)
                {
                    p.Add(new Chunk("CONTRATO DE PRESTAÇÃO DE SERVIÇOS EDUCACIONAIS DE ESPECIALIZAÇÃO EM ", font_Verdana_8_Bold));
                    p.Add(new Chunk(item_matricula.turmas.cursos.nome.ToUpper(), font_Verdana_8_Bold));
                }
                else if (qIdTipoCurso == 4)
                {
                    p.Add(new Chunk("CONTRATO DE PRESTAÇÃO DE SERVIÇOS EDUCACIONAIS DE CURTA DURAÇÃO EM ", font_Verdana_8_Bold));
                    p.Add(new Chunk(item_matricula.turmas.cursos.nome.ToUpper(), font_Verdana_8_Bold));
                }
                else 
                {
                    p.Add(new Chunk("CONTRATO DE PRESTAÇÃO DE SERVIÇOS EDUCACIONAIS DE MBA INTERNACIONAL EM ", font_Verdana_8_Bold));
                    p.Add(new Chunk(item_matricula.turmas.cursos.nome.ToUpper(), font_Verdana_8_Bold));
                }
                p.Add(new Chunk(", doravante designado simplesmente ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(", com a interveniência administrativa e financeira da ", font_Verdana_8_Normal));
                p.Add(new Chunk("FUNDAÇÃO DE APOIO AO INSTITUTO DE PESQUISAS TECNOLÓGICAS - FIPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", pessoa jurídica de direito privado, sem fins lucrativos, com sede na cidade São Paulo - SP, na ", font_Verdana_8_Normal));
                p.Add(new Chunk(item_endereco.endereco + ", " + item_endereco.numero, font_Verdana_8_Normal));
                if (item_endereco.complemento != "" && item_endereco.complemento != null)
                {
                    p.Add(new Chunk(" - " + item_endereco.complemento, font_Verdana_8_Normal));
                }
                p.Add(new Chunk(" - " + item_endereco.bairro + ", " + item_endereco.cidade + " - " + item_endereco.estado + " - CEP " + item_endereco.cep, font_Verdana_8_Normal));
                p.Add(new Chunk(", inscrita no CNPJ sob nº. " + item_endereco.cnpj + ", neste ato, representada por ", font_Verdana_8_Normal));
                p.Add(new Chunk(item_contato.diretor, font_Verdana_8_Normal));
                p.Add(new Chunk(", doravante designada simplesmente ", font_Verdana_8_Normal));
                p.Add(new Chunk("FIPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", na forma, cláusulas e condições abaixo:\n\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("CLÁUSULA PRIMEIRA - OBJETO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("1.1 - O presente contrato tem por objeto a prestação de serviços educacionais no âmbito do ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(", pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" ao ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("CLÁUSULA SEGUNDA - ORGANIZAÇÃO E OBRIGAÇÕES\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("2.1 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(" é mantido pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", sendo que o conteúdo da sua programação, a emissão do respectivo certificado de conclusão e as atividades curriculares e extracurriculares são de sua inteira responsabilidade, sendo o curso ministrado em conformidade com o previsto na legislação de ensino em vigor, e em seu planejamento pedagógico.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("2.2 - As aulas serão ministradas em salas, laboratórios, ambiente virtual  ou em locais que o ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" indicar, podendo ser síncronas ou assíncronas considerada a natureza de conteúdo, característica, peculiaridade e demais atividades que o ensino exigir, buscando, inclusive, otimizar a relação número de alunos por classe", font_Verdana_8_Normal));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("      Parágrafo Primeiro", font_Verdana_8_Bold));
                p.Add(new Chunk(". A matrícula, ato indispensável que estabelece o vínculo do aluno com o ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", dar-se à formalmente, através do preenchimento do formulário próprio, fornecido pelo IPT, com o pagamento da primeira parcela da mensalidade, com a assinatura deste Contrato e observância da Cláusula quarta deste instrumento", font_Verdana_8_Normal));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("2.3 - A ", font_Verdana_8_Normal));
                p.Add(new Chunk("FIPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" será a gestora administrativa e financeira do presente contrato, cabendo-lhe emitir boletos de cobrança, recibos e adotar todas as providências de caráter administrativo e financeiro em nome do ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("2.4 - É de exclusiva competência do ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" o planejamento, escolha de professores, orientação didática, pedagógica e educacional, fixação da carga horária e plano pedagógico, marcação de datas de provas e atividades de verificação de aproveitamento e demais providências de ensino.\n\n\n", font_Verdana_8_Normal));


                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                doc.NewPage();

                pageHeaderFooter.qPagina = 2;

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;


                p.Add(new Chunk("CLÁUSULA TERCEIRA - DAS OBRIGAÇÕES DO ALUNO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("3.1 - Cumprir todas as obrigações acadêmicas e disciplinares do ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("3.2 - Cumprir com as obrigações financeiras sob sua responsabilidade, e seguir as regras do ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO ", font_Verdana_8_Bold));
                p.Add(new Chunk("além de responsabilizar-se pelos prejuízos que venha a causar às instalações do ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT ", font_Verdana_8_Bold));
                p.Add(new Chunk("ou a terceiros, em decorrência da utilização da estrutura física disponibilizada pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n\n", font_Verdana_8_Normal));

                
                p.Add(new Chunk("CLÁUSULA QUARTA - PRAZO DE VIGÊNCIA\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("4.1 - O prazo de vigência do presente contrato, para efeito das obrigações financeiras, é de " + item_contato.prazo + " meses, a partir da data de início do curso.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("4.2 - O prazo de validade do presente contrato inicia-se na data de sua assinatura pelos participantes deste contrato e resolve-se de pleno acordo ao término do curso.\n\n\n", font_Verdana_8_Normal));


                p.Add(new Chunk("CLÁUSULA QUINTA - DO VALOR DO INVESTIMENTO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("5.1 - O valor do investimento no ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                if (qIdTipoCurso == 3) //3=Especialização
                {
                    p.Add(new Chunk(" será de " + string.Format("{0:C}", item_contato.valor_total) + " (" + Utilizades.EscreverExtenso(Convert.ToDecimal(item_contato.valor_total)) + "). Com reajuste anual pelo IPC-FIPE.\n\n\n", font_Verdana_8_Normal));
                }
                else
                {
                    p.Add(new Chunk(" será de " + string.Format("{0:C}", item_contato.valor_total) + " (" + Utilizades.EscreverExtenso(Convert.ToDecimal(item_contato.valor_total)) + "). Podendo ter reajuste anual pelo IPC-FIPE dependendo do curso.\n\n\n", font_Verdana_8_Normal));
                }


                p.Add(new Chunk("CLÁUSULA SEXTA - FORMA DE PAGAMENTO\n\n", font_Verdana_8_Bold));

                if (qIdTipoCurso == 3) //3=Especialização
                {
                    p.Add(new Chunk("6.1 - O ", font_Verdana_8_Normal));
                    p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                    p.Add(new Chunk(" pagará " + item_contato.num_parcelas + " parcelas mensais e sucessivas no valor de " + string.Format("{0:C}", item_contato.valor_parcela) + " (" + Utilizades.EscreverExtenso(Convert.ToDecimal(item_contato.valor_parcela)) + ") com vencimento no 15º dia de cada mês.\n\n", font_Verdana_8_Normal));

                    p.Add(new Chunk("6.2 - Outras atividades que não as especificadas no item 3.2 da cláusula terceira não integram o valor total do investimento e deverão ser ressarcidas à parte, pelo ", font_Verdana_8_Normal));
                    p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                    p.Add(new Chunk(", ao ", font_Verdana_8_Normal));
                    p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                    p.Add(new Chunk(", por meio da ", font_Verdana_8_Normal));
                    p.Add(new Chunk("FIPT", font_Verdana_8_Bold));
                    p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));
                }
                else
                {
                    p.Add(new Chunk("6.1 - O ", font_Verdana_8_Normal));
                    p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                    p.Add(new Chunk(" pagará " + item_contato.num_parcelas + " parcelas mensais e sucessivas no valor de " + string.Format("{0:C}", item_contato.valor_parcela) + " (" + Utilizades.EscreverExtenso(Convert.ToDecimal(item_contato.valor_parcela)) + ") com vencimento no 15º dia de cada mês. Em alguns cursos o valor do presente contrato será reajustado, respeitada a periodicidade mínima legal de 12 (doze) meses, de acordo com a variação do IPC - FIPE.\n\n", font_Verdana_8_Normal));

                    p.Add(new Chunk("6.2 - Outras atividades que não as especificadas no item 3.1 da cláusula terceira não integram o valor total do investimento e deverão ser ressarcidas à parte, pelo ", font_Verdana_8_Normal));
                    p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                    p.Add(new Chunk(", ao ", font_Verdana_8_Normal));
                    p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                    p.Add(new Chunk(", por meio da ", font_Verdana_8_Normal));
                    p.Add(new Chunk("FIPT", font_Verdana_8_Bold));
                    p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));
                }
                
                p.Add(new Chunk("6.3 - Com o pagamento dos valores referidos nesta cláusula à ", font_Verdana_8_Normal));
                p.Add(new Chunk("FIPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", o ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" dá quitação ao ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", relativas às parcelas pagas.\n\n\n", font_Verdana_8_Normal));

               
                p.Add(new Chunk("CLÁUSULA SÉTIMA - DO REAJUSTE\n\n", font_Verdana_8_Bold));

                if (qIdTipoCurso == 3) //3=Especialização
                {
                    p.Add(new Chunk("7.1 - O valor do presente contrato será reajustado, respeitada a periodicidade mínima legal de 12 (doze) meses, de acordo com a variação do IPC - FIPE.", font_Verdana_8_Normal));
                    p.Add(new Chunk("\n\n", font_Verdana_8_Normal));

                    p.Add(new Chunk("7.2 - As parcelas que compõem o valor do presente contrato serão, para o primeiro ano de sua vigência, fixas e irreajustáveis, enquanto que, para os anos subsequentes de sua vigência, serão reajustadas pela variação do IPC - FIPE.Caso remanesçam parcelas a serem pagas após o término da vigência do contrato, estas também serão reajustadas pela variação do IPC - FIPE.", font_Verdana_8_Normal));
                    p.Add(new Chunk("\n\n\n", font_Verdana_8_Normal));
                }
                else
                {
                    p.Add(new Chunk("7.1 - O valor do presente contrato poderá sofrer reajuste durante o período de vigência do ", font_Verdana_8_Normal));
                    p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                    p.Add(new Chunk(".\n\n\n", font_Verdana_8_Normal));
                }
                


                p.Add(new Chunk("CLÁUSULA OITAVA - PENALIDADES\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("8.1 - O atraso nos pagamentos devidos pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" implicará na aplicação de multa de 2% (dois por cento) sobre o valor da parcela em atraso e juros de mora de 1% (um por cento) ao mês, sem prejuízo da correção monetária e encargos financeiros, tendo como base o IGPM da FGV.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("8.2 - Para todos os efeitos, será considerada inadimplência o atraso por mais de 60 (sessenta) dias no pagamento das mensalidades, situação na qual o ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", por meio da ", font_Verdana_8_Normal));
                p.Add(new Chunk("FIPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", poderá optar, cumulativamente ou não.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("     a) pela emissão de duplicata de serviços, pelo valor da (s) parcela (s) vencida (s) e não paga (s) acrescida (s) da multa e juros supra mencionados, além de custas e honorários advocatícios;\n", font_Verdana_8_Normal));
                p.Add(new Chunk("     b) pelo protesto do título executivo extrajudicial;\n", font_Verdana_8_Normal));
                p.Add(new Chunk("     c) pela cobrança extrajudicial ou judicial, arcando o ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", além de todas as despesas que recaírem sobre o débito, com os honorários advocatícios;\n", font_Verdana_8_Normal));

                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                doc.NewPage();
                pageHeaderFooter.qPagina = 3;

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;

                p.Add(new Chunk("     d) pela inscrição do ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" no Cadastro de Proteção ao Crédito como inadimplente, após notificação, na falta de pagamento de duas parcelas, consecutivas ou não;\n", font_Verdana_8_Normal));
                p.Add(new Chunk("     e) pela suspensão da prestação dos serviços educacionais, após notificação ao ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

               

                p.Add(new Chunk("\nCLÁUSULA NONA - RESCISÃO, E CANCELAMENTO DE MATRÍCULA\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("9.1 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" poderá solicitar o cancelamento de matrícula do curso no qual se encontra regularmente matriculado, a qualquer tempo, com a expressa ciência e anuência da Coordenação do curso, porém:\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("     9.1.1 - No caso de solicitação de cancelamento de matrícula, o requerente terá direito à devolução integral do valor da matrícula, se a solicitação for realizada até 07 (sete) dias antes do início do curso;\n", font_Verdana_8_Normal));
                p.Add(new Chunk("     9.1.2 - Solicitações feitas após o período explicitado no item 9.1.1 e até o primeiro dia de início do curso, darão ao requerente direito à devolução de 20% do valor da matrícula;\n", font_Verdana_8_Normal));
                p.Add(new Chunk("     9.1.3 - Solicitações feitas após o início do curso, não darão direito à devolução do valor da matrícula ao requerente, mas isentam-no de pagamento da mensalidade relativa ao mês em curso, sempre que o cancelamento for solicitado antes do 15º dia do referido mês.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("9.2 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" não devolverá as quantias já pagas, na hipótese do ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" não obter resultado acadêmico suficiente para receber o certificado de conclusão de curso.\n\n\n", font_Verdana_8_Normal));

                    
                p.Add(new Chunk("CLÁUSULA DÉCIMA - NOVAÇÃO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("10.1 - A tolerância relativamente ao descumprimento de quaisquer das obrigações por parte do ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" não configurará novação, descabendo, portanto, as eventuais alegações de direito adquirido.\n\n\n", font_Verdana_8_Normal));
 

                p.Add(new Chunk("\nCLÁUSULA DÉCIMA PRIMEIRA – DAS DISPOSIÇÕES GERAIS\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("11.1 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" será responsável pelos prejuízos que venha a causar às instalações de propriedade do ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" ou a terceiros, em decorrência da utilização da estrutura física do ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("11.2 - Para todas as intimações e comunicações são válidos os endereços fornecidos pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" constantes no requerimento de matrícula, sendo consideradas entregues todas as remessas para o referido endereço, salvo alterações devidamente comunicadas.\n\n\n", font_Verdana_8_Normal));

               p.Add(new Chunk("\nCLÁUSULA DÉCIMA SEGUNDA - FORO DE ELEIÇÃO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("12.1 - Para dirimir quaisquer questões oriundas deste Contrato ou da prestação de serviços nele contratadas, fica eleito o Foro da Comarca da Capital do Estado de São Paulo, com exclusão de qualquer outro, por mais privilegiado que seja ou venha a ser.\n\n", font_Verdana_8_Normal));

                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                doc.NewPage();
                pageHeaderFooter.qPagina = 4;
                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;

                p.Add(new Chunk("\n\nE por estarem acordadas, assinam as ", font_Verdana_8_Normal));
                p.Add(new Chunk("PARTES", font_Verdana_8_Bold));
                p.Add(new Chunk(" o presente instrumento em 02 (duas) vias idênticas, rubricando todas as suas páginas, na presença das testemunhas abaixo identificadas.\n\n", font_Verdana_8_Normal));

                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 530f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                p.Add(new Chunk("\n\nSão Paulo, " + String.Format("{0:dd}", DateTime.Today) + " de " + dtfi.GetMonthName(Convert.ToDateTime(DateTime.Today).Month) + " de " + Convert.ToDateTime(DateTime.Today).Year, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 3 Colunas ========================================================
                table = new PdfPTable(3);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 30, 40, 30 };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("\n\n\n", font_Verdana_8_Normal));
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\n" + item.idaluno + " - " + item.nome, font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 100 };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("\n\nFUNDAÇÃO DE APOIO AO INSTITUTO DE PESQUISAS TECNOLÓGICAS - FIPT\n", font_Verdana_8_Bold));
                p.Add(new Chunk("INSTITUTO DE PESQUISAS TECNOLÓGICAS DO ESTADO DE SÃO PAULO S/A - IPT\n\n\n\n\n\n\n\n", font_Verdana_8_Bold));

                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela de 5 Colunas ========================================================
                table = new PdfPTable(5);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 5, 40, 10, 40, 5 };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\n" + item_contato.secretaria + "\nSecretária", font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\n" + item_contato.coordenador + "\nCoordenador", font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 100 };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                p.Add(new Chunk("\n\n\n\nTestemunhas:\n\n\n\n", font_Verdana_8_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela de 5 Colunas ========================================================
                table = new PdfPTable(5);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 5, 40, 10, 40, 5 };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\n" + item_contato.testemunha_1 + "\nRG: " + item_contato.rg_testemunha_1, font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\n" + item_contato.testemunha_2 + "\nRG: " + item_contato.rg_testemunha_2, font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão da Ata da Qualificação ";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }

        }

        protected void GeraContratoGeral(string qTipoContrato, string qDataContrato, string qValorTotal, string qValorDisciplina, string qNumeroParcela, 
            string qValorParcela, string qDataInicioCurso, string qPrazo, string qCoordenador, string qSecretaria, string qTextemunha1, string qRGTextemunha1, 
            string qTextemunha2, string qRGTextemunha2, string qParagrafoDiretor, string qIdTurma, string qNome)
        {
            try
            {
                bool bNovo = false;
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                GeraisAplicacao AplicacaoGerais = new GeraisAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request.Form["hQTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma item_matricula = new matricula_turma();
                item_matricula = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).SingleOrDefault();

                dados_contratos item_contato = new dados_contratos();
                item_contato.nome_contrato = qTipoContrato;
                item_contato = AplicacaoGerais.BuscaContrato(item_contato);

                if (item_contato == null)
                {
                    bNovo = true;
                    item_contato = new dados_contratos();
                    item_contato.nome_contrato = item_matricula.turmas.cursos.nome;
                    item_contato.id_curso = item_matricula.turmas.cursos.id_curso;
                    item_contato.convenio = false;
                    item_contato.data_cadastro = DateTime.Now;
                }

                Enderecos item_endereco = new Enderecos();
                item_endereco.id_endereco = 1; // FIPT
                item_endereco = AplicacaoGerais.BuscaEndereco(item_endereco);

                //qValorTotal = Repla
                item_contato.valor_total = Convert.ToDecimal(qValorTotal);
                item_contato.num_parcelas = Convert.ToInt32(qNumeroParcela);
                item_contato.valor_parcela = Convert.ToDecimal(qValorParcela);
                item_contato.valor_disciplina = Convert.ToDecimal(qValorDisciplina);
                item_contato.prazo = Convert.ToInt32(qPrazo);
                item_contato.inicio = Convert.ToDateTime(qDataInicioCurso);
                item_contato.coordenador = qCoordenador;
                item_contato.secretaria = qSecretaria;
                item_contato.testemunha_1 = qTextemunha1;
                item_contato.testemunha_2 = qTextemunha2;
                item_contato.rg_testemunha_1 = qRGTextemunha1;
                item_contato.rg_testemunha_2 = qRGTextemunha2;
                item_contato.diretor = qParagrafoDiretor;
                item_contato.status = "alterado";
                item_contato.data_alteracao = DateTime.Now;
                item_contato.usuario = usuario.usuario;

                if (bNovo)
                {
                    item_contato.status = "cadastrado";
                    item_contato = AplicacaoGerais.CriaContrato(item_contato);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fPreencheComboContrato('" + qIdTurma + "');", true);
                }
                else
                {
                    item_contato = AplicacaoGerais.AlterarContrato(item_contato);
                }

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 120, 50);//estibulando o espaçamento das margens que queremos ===Antigo===(45, 40, 40, 40)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/Contrato_" + qNome + ".pdf"), FileMode.Create));
                PDF_Cabec_Rodape_Contrato pageHeaderFooter = new PDF_Cabec_Rodape_Contrato();
                pageHeaderFooter.Caminho = Server.MapPath("~");
                pageHeaderFooter.qPagina = 1;
                writer.PageEvent = pageHeaderFooter;
                doc.Open();

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_11_Bold = new Font(_bfVerdanaNormal, 11, Font.BOLD);
                Font font_Verdana_11_Normal = new Font(_bfVerdanaNormal, 11, Font.NORMAL);
                Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Verdana_8_Bold = new Font(_bfVerdanaNormal, 8, Font.BOLD);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Verdana_7_Bold = new Font(_bfVerdanaNormal, 7, Font.BOLD);
                Font font_Verdana_7_Normal = new Font(_bfVerdanaNormal, 7, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);
                Color FontColor_Cinza = new Color(204, 204, 204);
                Color FontColor_CinzaClaro = new Color(229, 229, 229);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                float[] widths;
                Paragraph p;
                PdfPCell cell;
                PdfPTable table;

                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;

                //Aqui é uma nova tabela de 2 Colunas ========================================================
                //table2 = new iTextSharp.text.Table(1);
                //cell2 = new Cell();
                //table2.Alignment = Rectangle.ALIGN_CENTER;
                //table2.Padding = 1;
                //table2.Spacing = 0;
                //table2.Border = Rectangle.BOX;
                //table2.BorderWidth = 1;
                //table2.Width = 100;
                //table2.DeleteAllRows();
                //table2.Widths = new Single[] { 100 };
                //table2.DefaultCellBorder = 1;

                //ph = new Phrase();
                //p = new Paragraph();
                //p.Add(new Chunk("Coordenadoria de Ensino Tecnológico - CET\n", font_Verdana_12_Bold));
                //p.Add(new Chunk("Contrato de Prestação de Serviços Educacionais de Mestrado\n", font_Verdana_12_Normal));
                //p.Add(new Chunk("Profissional em " + item_matricula.turmas.cursos.nome, font_Verdana_12_Normal));
                //p.SpacingAfter = 100;
                //p.SetLeading(12, 10);
                //cell2 = new Cell();
                //cell2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                ////cell2.Add(p);
                //cell2.AddElement(p);
                //table2.AddCell(cell2);
              
                //doc.Add(table2);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 530f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                //p.Add(new Chunk("Coordenadoria de Ensino Tecnológico - CET\n", font_Verdana_11_Bold));
                p.Add(new Chunk(" \n", font_Verdana_11_Bold));
                p.Add(new Chunk("Contrato de Prestação de Serviços Educacionais de Mestrado\n", font_Verdana_11_Normal));
                p.Add(new Chunk("Profissional em " + item_matricula.turmas.cursos.nome, font_Verdana_11_Normal));
                //p.SpacingAfter = 100;
                //p.SetLeading(12, 10);
                p.SpacingAfter = 4f;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 2;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;
                p.Add(new Chunk("O ", font_Verdana_8_Normal));
                p.Add(new Chunk("INSTITUTO DE PESQUISAS TECNOLÓGICAS DO ESTADO DE SÃO PAULO S/A - IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", empresa pública constituída nos termos da Lei Estadual no. 896/75, com sede na Capital do Estado de São Paulo, na Cidade Universitária “Armando de Salles Oliveira”, inscrito CNPJ/MF nº. 60.633.674/0001-55 e Inscrição Estadual nº. 105.933.432.110, neste ato representado por seus Procuradores abaixo assinados, doravante designados simplesmente ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", e o aluno:", font_Verdana_8_Normal));
                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 3 Colunas ========================================================
                table = new PdfPTable(3);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15,1,84 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Nome:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.nome, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 7 Colunas ========================================================
                table = new PdfPTable(7);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 39, 1, 14, 1, 30 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("RG:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                string sAux;
                if (item.digito_num_documento != "" && item.digito_num_documento != null)
                {
                    sAux = item.numero_documento + "-" + item.digito_num_documento;
                }
                else
                {
                    sAux = item.numero_documento;
                }

                p.Add(new Chunk(sAux, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("CPF:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.cpf, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 7 Colunas ========================================================
                table = new PdfPTable(7);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 39, 1, 14, 1, 30 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Formação:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.formacao, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Nacionalidade:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.pais_nasc, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 7 Colunas ========================================================
                table = new PdfPTable(7);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 39, 1, 14, 1, 30 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Endereço:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.logradouro_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Nº:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.numero_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 7 Colunas ========================================================
                table = new PdfPTable(7);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 39, 1, 14, 1, 30 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Complemento:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.complemento_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Bairro:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.bairro_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 7 Colunas ========================================================
                table = new PdfPTable(7);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 39, 1, 14, 1, 30 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("CEP:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.cep_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("UF:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.uf_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 3 Colunas ========================================================
                table = new PdfPTable(3);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 84 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Cidade:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.cidade_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                //table.SpacingAfter = 15f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;
                p.Add(new Chunk("doravante designado simplesmente ALUNO, celebram o presente CONTRATO DE PRESTAÇÃO DE SERVIÇOS EDUCACIONAIS DE MESTRADO PROFISSIONAL EM ", font_Verdana_8_Normal));
                p.Add(new Chunk(item_matricula.turmas.cursos.nome.ToUpper(), font_Verdana_8_Normal));
                p.Add(new Chunk(", doravante designado simplesmente CURSO de Mestrado Profissional, com a interveniência e anuência da FUNDAÇÃO DE APOIO AO INSTITUTO DE PESQUISAS TECNOLÓGICAS - FIPT, pessoa jurídica de direito privado, sem fins lucrativos, com sede na cidade São Paulo - SP, na ", font_Verdana_8_Normal));
                p.Add(new Chunk(item_endereco.endereco + ", " + item_endereco.numero, font_Verdana_8_Normal));
                if (item_endereco.complemento != "" && item_endereco.complemento != null)
                {
                    p.Add(new Chunk(" - " + item_endereco.complemento, font_Verdana_8_Normal));
                }
                p.Add(new Chunk(" - " + item_endereco.bairro + ", " + item_endereco.cidade + " - " + item_endereco.estado + " - CEP " + item_endereco.cep, font_Verdana_8_Normal));
                p.Add(new Chunk(", inscrita no CNPJ sob nº. " + item_endereco.cnpj + ", neste ato, representada por ", font_Verdana_8_Normal));
                p.Add(new Chunk(item_contato.diretor, font_Verdana_8_Normal));
                p.Add(new Chunk(", doravante designada simplesmente FIPT, na forma, cláusulas e condições abaixo:\n\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("CLÁUSULA PRIMEIRA - OBJETO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("1.1 - O presente contrato tem por objeto a prestação de serviços educacionais no âmbito do ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(" de Mestrado Profissional em " + item_matricula.turmas.cursos.nome + ", pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" ao ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", desde que satisfeitos os requisitos estabelecidos no Regimento de Pós - Graduação Mestrado Profissional, que é sua parte integrante.\n\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("CLÁUSULA SEGUNDA - ORGANIZAÇÃO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("2.1 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(" é mantido pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", sendo que o conteúdo da sua programação, a emissão do respectivo certificado de conclusão e as atividades extracurriculares são de sua inteira responsabilidade.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("2.2 - A ", font_Verdana_8_Normal));
                p.Add(new Chunk("FIPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" será a gestora administrativa e financeira do presente contrato, cabendo-lhe emitir boletos de cobrança, recibos e adotar todas as providências de caráter administrativo e financeiro em nome do ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("CLÁUSULA TERCEIRA - PRAZO DE VIGÊNCIA\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("3.1 - O prazo de vigência do presente contrato é de " + item_contato.prazo + " meses computados a partir de São Paulo, " + String.Format("{0:dd}", item_contato.inicio) + " de " + dtfi.GetMonthName(Convert.ToDateTime(item_contato.inicio).Month) + " de " + Convert.ToDateTime(item_contato.inicio).Year + ", até o final do curso.\n\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("CLÁUSULA QUARTA - DO VALOR DO INVESTIMENTO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("4.1 - O valor do investimento no ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(" corresponde ao valor referente à " + item_contato.qtd_disciplina + " disciplinas a serem cursadas e a Orientação para a Dissertação, totalizando " + string.Format("{0:C}", item_contato.valor_total) + " (" + Utilizades.EscreverExtenso(Convert.ToDecimal(item_contato.valor_total)) + "), que serão pagas em " + item_contato.num_parcelas + " mensalidades.\n\n", font_Verdana_8_Normal));

                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                doc.NewPage();

                pageHeaderFooter.qPagina = 2;

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;

                p.Add(new Chunk("\nCLÁUSULA QUINTA - FORMA DE PAGAMENTO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("5.1 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" pagará, no primeiro ano de curso, 12 (Doze) parcelas mensais e sucessivas no valor de " + string.Format("{0:C}", item_contato.valor_parcela) + " (" + Utilizades.EscreverExtenso(Convert.ToDecimal(item_contato.valor_parcela)) + ") com vencimento no 15º dia de cada mês.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("5.2 - Para o pagamento do valor correspondente aos anos subsequentes do curso, será observado o disposto na cláusula sexta deste ajuste.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("5.3 - Para cada disciplina adicional às " + item_contato.qtd_disciplina + " disciplinas previstas, o ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" pagará um valor suplementar, que poderá parcelado em 4 vezes no período em que for cursada.\n\n", font_Verdana_8_Normal));

                //if (item_contato.valor_disciplina == 0)
                //{
                //    p.Add(new Chunk("5.3 - Para cada disciplina adicional as " + item_contato.qtd_disciplina + " disciplinas previstas, o aluno pagará o valor adicional de R$ 0,00 (Zero), parcelado em 4 vezes, no período em que for cursada.\n\n", font_Verdana_8_Normal));
                //}
                //else
                //{
                //    p.Add(new Chunk("5.3 - Para cada disciplina adicional as " + item_contato.qtd_disciplina + " disciplinas previstas, o aluno pagará o valor adicional de " + string.Format("{ 0:C }", item_contato.valor_disciplina) + " (" + Utilizades.EscreverExtenso(Convert.ToDecimal(item_contato.valor_disciplina)) + "), parcelado em 4 vezes, no período em que for cursada.\n\n", font_Verdana_8_Normal));
                //}

                //if (item_contato.valor_disciplina == 0)
                //{
                //    p.Add(new Chunk("   5.3.1 - Caso o aluno repita uma disciplina, ou a substitua por outra, por reprovação ou cancelamento após a metade do período letivo, o aluno pagará o valor adicional de R$ 0,00 (Zero), parcelado em 4 vezes, no período em que for cursada.\n\n", font_Verdana_8_Normal));
                //}
                //else
                //{
                //    p.Add(new Chunk("   5.3.1 - Caso o aluno repita uma disciplina, ou a substitua por outra, por reprovação ou cancelamento após a metade do período letivo, o aluno pagará o valor adicional de" + string.Format("{ 0:C }", item_contato.valor_disciplina) + " (" + Utilizades.EscreverExtenso(Convert.ToDecimal(item_contato.valor_disciplina)) + "), parcelado em 4 vezes, no período em que for cursada.\n\n", font_Verdana_8_Normal));
                //}

                p.Add(new Chunk("   5.3.1 - Outras atividades que não as especificadas em 4.1 , inclusive as extracurriculares e administrativas, terão seus valores fixados pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", ressarcidas à parte, pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", ao ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", por meio da ", font_Verdana_8_Normal));
                p.Add(new Chunk("FIPT", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("5.4 - Com o pagamento dos valores referidos nesta cláusula à ", font_Verdana_8_Normal));
                p.Add(new Chunk("FIPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", o ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" dá quitação ao ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", relativas às parcelas pagas.\n\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("5.5 - DEVOLUÇÃO DE VALORES PAGOS.\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("   5.5.1 - O cancelamento da matricula poderá ser solicitado a qualquer tempo, valendo as regras a seguir para devolução da taxa de matricula;\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("       a) No caso da solicitação ter sido protocolada na secretaria acadêmica da CET /IPT até 7 dias corridos antes do início das aulas, o requerente terá direito à devolução integral do valor da matricula;\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("       b) Solicitações feitas após o período explicitado na letra (a) deste artigo e até o primeiro dia letivo darão ao requerente direito à devolução de 50 % do valor da taxa de matricula;\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("   5.5.2 - Pedido de cancelamento do curso, deverá ser formalizado através de requerimento protocolado na secretaria acadêmica da CET / IPT até o dia 5 de cada mês, dará ao requerente suspensão do pagamento de mensalidades futuras, a partir do mês em  curso.Caso o pedido seja feito após o dia 5 do correspondente mês, será cobrada a mensalidade do mês.O pedido de cancelamento do curso não isenta o requerente de pagamento de débitos anteriores à data do pedido.\n\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("CLÁUSULA SEXTA - DO REAJUSTE\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("6.1 - O valor do presente contrato será reajustado, respeitada a periodicidade mínima legal de 12 (doze) meses, de acordo com a variação do IPC - FIPE.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("6.2 - As parcelas que compõem o valor do presente contrato serão, para o primeiro ano de sua vigência, fixas e irreajustáveis, enquanto que, para os anos subsequentes de sua vigência, serão reajustadas pela variação do IPC - FIPE. Caso remanesçam parcelas a serem pagas após o término da vigência do contrato, estas também serão reajustadas pela variação do IPC - FIPE.\n\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("CLÁUSULA SÉTIMA - PENALIDADES\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("7.1 - O atraso nos pagamentos devidos pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" implica na aplicação de multa moratória correspondente a 2% (dois por cento) sobre o valor da parcela em atraso e juros de mora de 1 % (um por cento) ao mês, sem prejuízo da correção monetária e encargos financeiros, tendo como base o IGPM da FGV.\n\n", font_Verdana_8_Normal));

                //p.Add(new Chunk("7.2 - O ", font_Verdana_8_Normal));
                //p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                //p.Add(new Chunk(" tem ciência neste ato, que em caso de inadimplência das parcelas ou de qualquer obrigação de pagamento decorrente deste contrato, por mais de 90(noventa) dias, a FIPT poderá comunicar / Inserir o debito em órgão(s) e / ou cadastro(s) de devedor(es) legalmente existente para registro(p.ex.SPC / SERASA), nos termos do artigo 43 da Lei. 8.078 / 90(Código de Defesa do Consumidor), além de exigir judicialmente a divida.\n\n", font_Verdana_8_Normal));

                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                doc.NewPage();
                pageHeaderFooter.qPagina = 3;

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;

                p.Add(new Chunk("7.2 - Para todos os efeitos, será considerada inadimplência o atraso por mais de 60(sessenta) dias no pagamento das mensalidades, situação na qual o ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", por meio da ", font_Verdana_8_Normal));
                p.Add(new Chunk("FIPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", poderá optar, cumulativamente ou não.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("       a) pela emissão de duplicata de serviços, pelo valor da(s) parcela(s) vencida(s) e não paga(s) acrescida(s) da multa e juros supra mencionados, além de custas e honorários advocatícios;\n\n", font_Verdana_8_Normal));
                p.Add(new Chunk("       b) pelo protesto do título executivo extrajudicial;\n\n", font_Verdana_8_Normal));
                p.Add(new Chunk("       c) pela cobrança extrajudicial ou judicial, arcando o ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", além de todas as despesas que recaírem sobre o débito, com os honorários advocatícios;\n\n", font_Verdana_8_Normal));
                p.Add(new Chunk("      d) pela inscrição do ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" no Cadastro de Proteção ao Crédito como inadimplente, após notificação, na falta de pagamento de duas parcelas, consecutivas ou não;\n\n", font_Verdana_8_Normal));
                p.Add(new Chunk("       e) pela suspensão da prestação dos serviços educacionais, após notificação ao ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                

                p.Add(new Chunk("\n   Paragrafo único", font_Verdana_8_Bold));
                p.Add(new Chunk(": Em caso de cobrança judicial respondera ainda o(a) devedor(a) pelas custas judicias e honorários advocatícios de 20 % (vinte por cento).\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("\nCLÁUSULA OITAVA - RESCISÃO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("8.1 - O presente ajuste poderá ser rescindido injustificadamente por iniciativa do ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", a ser comunicada formalmente ao ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" por meio da ", font_Verdana_8_Normal));
                p.Add(new Chunk("FIPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", ou por iniciativa do ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", na hipótese de resultado acadêmico insuficiente ou falta grave do ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", a critério da Comissão de Pós - Graduação(CPG) do Programa de Mestrado do ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("CLÁUSULA NONA - DISPOSIÇÕES GERAIS\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("9.1 - A vida acadêmica do ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" será regulada pelo Regimento da Pós-Graduação do ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" vigente na data de assinatura deste contrato.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("9.2 - As aulas do programa de Mestrado são presenciais, mas eventualmente poderá haver aulas de modo remoto em plataforma acessível a todos, de comum acordo entre o docente e o ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("9.3 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" deverá cumprir também, as regras específicas do curso estabelecidas no Caderno do Aluno, que é parte integrante deste contrato.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("9.4 - A conclusão dos créditos de disciplinas não confere, ao ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", qualquer título acadêmico ou profissional.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("9.5 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" está ciente que, sem a autorização expressa do professor responsável, não será permitida a gravação de imagens, conteúdos, voz, edição, reprodução de nenhum material utilizado em aulas, por nenhum meio, bem como, a utilização do conteúdo para quaisquer finalidades estranhas ao estudo e aprendizado das disciplinas. O descumprimento caracterizará infração disciplinar (violação do Código de Ética e Conduta do IPT) e poderá caracterizar, ainda, infração cível e eventualmente criminal.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("9.6 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", neste ato, autoriza a utilização de sua imagem em todos os veículos de comunicação e propaganda, para fim exclusivo de divulgação dos resultados obtidos em decorrência do Curso hora contratado.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("   Paragrafo único", font_Verdana_8_Bold));
                p.Add(new Chunk(": Em nenhuma hipótese poderá a imagem do ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" ser utilizada de maneira contraria à moral, aos bons costumes e à ordem pública.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("9.7 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" será desligado do programa de MP caso não efetue a matricula em todos os quadrimestres, inclusive após conseguir todos os créditos em disciplina.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("9.8 - A emissão do Certificado de Conclusão do Curso fica condicionada à entrega pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" de toda a documentação exigida pelo Regimento da Pós Graduação do IPT, ao pagamento integral do valor do Curso, bem como ao cumprimento de todos os requisitos acadêmicos do mesmo.\n\n", font_Verdana_8_Normal));

                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                doc.NewPage();
                pageHeaderFooter.qPagina = 4;
                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;
                p.Add(new Chunk("\nCLÁUSULA DÉCIMA - SUSPENSÃO (TRANCAMENTO)\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("10.1 - O presente ajuste poderá ser suspenso uma única vez por um período máximo de 4(quatro) meses, por iniciativa do ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" a ser comunicada formalmente ao ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", com o aproveitamento dos créditos já cursados, bem como dos valores pagos, quando da retomada do curso.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("\nCLÁUSULA DÉCIMA PRIMEIRA - NOVAÇÃO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("11.1 - A tolerância relativamente ao descumprimento de quaisquer das obrigações por parte do ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" não configurará novação, descabendo, portanto, as eventuais alegações de direito adquirido.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("\nCLÁUSULA DÉCIMA SEGUNDA - FORO DE ELEIÇÃO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("12.1 - Para dirimir quaisquer questões oriundas deste Contrato ou da prestação de serviços nele contratadas, fica eleito o Foro da Comarca da Capital do Estado de São Paulo, com exclusão de qualquer outro, por mais privilegiado que seja ou venha a ser.\n\n", font_Verdana_8_Normal));


                p.Add(new Chunk("\n\nE por estarem acordadas, assinam as ", font_Verdana_8_Normal));
                p.Add(new Chunk("PARTES", font_Verdana_8_Bold));
                p.Add(new Chunk(" o presente instrumento em 02 (duas) vias idênticas, rubricando todas as suas páginas, na presença das testemunhas abaixo identificadas.\n\n", font_Verdana_8_Normal));

                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 530f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                p.Add(new Chunk("\n\nSão Paulo, " + String.Format("{0:dd}", DateTime.Today) + " de " + dtfi.GetMonthName(Convert.ToDateTime(DateTime.Today).Month) + " de " + Convert.ToDateTime(DateTime.Today).Year, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 3 Colunas ========================================================
                table = new PdfPTable(3);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 30, 40, 30 };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("\n\n\n", font_Verdana_8_Normal));
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\n" + item.idaluno + " - " + item.nome, font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 100 };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("\n\nFUNDAÇÃO DE APOIO AO INSTITUTO DE PESQUISAS TECNOLÓGICAS - FIPT\n", font_Verdana_8_Bold));
                p.Add(new Chunk("INSTITUTO DE PESQUISAS TECNOLÓGICAS DO ESTADO DE SÃO PAULO S/A - IPT\n\n\n\n\n\n\n\n", font_Verdana_8_Bold));

                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela de 5 Colunas ========================================================
                table = new PdfPTable(5);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 5, 40, 10, 40, 5 };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\n" + item_contato.secretaria + "\nSecretária" , font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\n" + item_contato.coordenador + "\nCoordenador" , font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 100 };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                p.Add(new Chunk("\n\n\n\nTestemunhas:\n\n\n\n", font_Verdana_8_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela de 5 Colunas ========================================================
                table = new PdfPTable(5);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 5, 40, 10, 40, 5 };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\n" + item_contato.testemunha_1 + "\nRG: " + item_contato.rg_testemunha_1, font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\n" + item_contato.testemunha_2 + "\nRG: " + item_contato.rg_testemunha_2, font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão do Contrato";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }

        }

        protected void GeraContratoGeralConvenio(string qTipoContrato, string qDataContrato, string qValorTotal, string qValorDisciplina, string qNumeroParcela,
            string qValorParcela, string qDataInicioCurso, string qPrazo, string qCoordenador, string qSecretaria, string qTextemunha1, string qRGTextemunha1,
            string qTextemunha2, string qRGTextemunha2, string qParagrafoDiretor, string qIdTurma, string qNome)
        {
            try
            {
                bool bNovo = false;
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                GeraisAplicacao AplicacaoGerais = new GeraisAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request.Form["hQTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma item_matricula = new matricula_turma();
                item_matricula = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).SingleOrDefault();

                dados_contratos item_contato = new dados_contratos();
                item_contato.nome_contrato = qTipoContrato;
                item_contato = AplicacaoGerais.BuscaContrato(item_contato);

                if (item_contato == null)
                {
                    bNovo = true;
                    item_contato = new dados_contratos();
                    item_contato.nome_contrato = item_matricula.turmas.cursos.nome;
                    item_contato.id_curso = item_matricula.turmas.cursos.id_curso;
                    item_contato.convenio = false;
                    item_contato.data_cadastro = DateTime.Now;
                }

                Enderecos item_endereco = new Enderecos();
                item_endereco.id_endereco = 1; // FIPT
                item_endereco = AplicacaoGerais.BuscaEndereco(item_endereco);

                //qValorTotal = Repla
                item_contato.valor_total = Convert.ToDecimal(qValorTotal);
                item_contato.num_parcelas = Convert.ToInt32(qNumeroParcela);
                item_contato.valor_parcela = Convert.ToDecimal(qValorParcela);
                item_contato.valor_disciplina = Convert.ToDecimal(qValorDisciplina);
                item_contato.prazo = Convert.ToInt32(qPrazo);
                item_contato.inicio = Convert.ToDateTime(qDataInicioCurso);
                item_contato.coordenador = qCoordenador;
                item_contato.secretaria = qSecretaria;
                item_contato.testemunha_1 = qTextemunha1;
                item_contato.testemunha_2 = qTextemunha2;
                item_contato.rg_testemunha_1 = qRGTextemunha1;
                item_contato.rg_testemunha_2 = qRGTextemunha2;
                item_contato.diretor = qParagrafoDiretor;
                item_contato.status = "alterado";
                item_contato.data_alteracao = DateTime.Now;
                item_contato.usuario = usuario.usuario;

                if (bNovo)
                {
                    item_contato.status = "cadastrado";
                    item_contato = AplicacaoGerais.CriaContrato(item_contato);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fPreencheComboContrato('" + qIdTurma + "');", true);
                }
                else
                {
                    item_contato = AplicacaoGerais.AlterarContrato(item_contato);
                }

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 120, 50);//estibulando o espaçamento das margens que queremos ===Antigo===(45, 40, 40, 40)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/Contrato_" + qNome + ".pdf"), FileMode.Create));
                PDF_Cabec_Rodape_Contrato pageHeaderFooter = new PDF_Cabec_Rodape_Contrato();
                pageHeaderFooter.Caminho = Server.MapPath("~");
                pageHeaderFooter.qPagina = 1;
                writer.PageEvent = pageHeaderFooter;
                doc.Open();

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_11_Bold = new Font(_bfVerdanaNormal, 11, Font.BOLD);
                Font font_Verdana_11_Normal = new Font(_bfVerdanaNormal, 11, Font.NORMAL);
                Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Verdana_8_Bold = new Font(_bfVerdanaNormal, 8, Font.BOLD);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Verdana_7_Bold = new Font(_bfVerdanaNormal, 7, Font.BOLD);
                Font font_Verdana_7_Normal = new Font(_bfVerdanaNormal, 7, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);
                Color FontColor_Cinza = new Color(204, 204, 204);
                Color FontColor_CinzaClaro = new Color(229, 229, 229);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                float[] widths;
                Paragraph p;
                PdfPCell cell;
                PdfPTable table;

                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;

                //Aqui é uma nova tabela de 2 Colunas ========================================================
                //table2 = new iTextSharp.text.Table(1);
                //cell2 = new Cell();
                //table2.Alignment = Rectangle.ALIGN_CENTER;
                //table2.Padding = 1;
                //table2.Spacing = 0;
                //table2.Border = Rectangle.BOX;
                //table2.BorderWidth = 1;
                //table2.Width = 100;
                //table2.DeleteAllRows();
                //table2.Widths = new Single[] { 100 };
                //table2.DefaultCellBorder = 1;

                //ph = new Phrase();
                //p = new Paragraph();
                //p.Add(new Chunk("Coordenadoria de Ensino Tecnológico - CET\n", font_Verdana_12_Bold));
                //p.Add(new Chunk("Contrato de Prestação de Serviços Educacionais de Mestrado\n", font_Verdana_12_Normal));
                //p.Add(new Chunk("Profissional em " + item_matricula.turmas.cursos.nome, font_Verdana_12_Normal));
                //p.SpacingAfter = 100;
                //p.SetLeading(12, 10);
                //cell2 = new Cell();
                //cell2.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                ////cell2.Add(p);
                //cell2.AddElement(p);
                //table2.AddCell(cell2);

                //doc.Add(table2);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 530f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                //p.Add(new Chunk("Coordenadoria de Ensino Tecnológico - CET\n", font_Verdana_11_Bold));
                p.Add(new Chunk(" \n", font_Verdana_11_Bold));
                p.Add(new Chunk("Contrato de Prestação de Serviços Educacionais de Mestrado\n", font_Verdana_11_Normal));
                p.Add(new Chunk("Profissional em " + item_matricula.turmas.cursos.nome, font_Verdana_11_Normal));
                //p.SpacingAfter = 100;
                //p.SetLeading(12, 10);
                p.SpacingAfter = 4f;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 2;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;
                p.Add(new Chunk("O ", font_Verdana_8_Normal));
                p.Add(new Chunk("INSTITUTO DE PESQUISAS TECNOLÓGICAS DO ESTADO DE SÃO PAULO S/A - IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", empresa pública constituída nos termos da Lei Estadual no. 896/75, com sede na Capital do Estado de São Paulo, na Cidade Universitária “Armando de Salles Oliveira”, inscrito CNPJ/MF nº. 60.633.674/0001-55 e Inscrição Estadual nº. 105.933.432.110, neste ato representado por seus Procuradores abaixo assinados, doravante designados simplesmente ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", e o aluno:", font_Verdana_8_Normal));
                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 3 Colunas ========================================================
                table = new PdfPTable(3);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 84 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Nome:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.nome, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 7 Colunas ========================================================
                table = new PdfPTable(7);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 39, 1, 14, 1, 30 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("RG:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                string sAux;
                if (item.digito_num_documento != "" && item.digito_num_documento != null)
                {
                    sAux = item.numero_documento + "-" + item.digito_num_documento;
                }
                else
                {
                    sAux = item.numero_documento;
                }

                p.Add(new Chunk(sAux, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("CPF:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.cpf, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 7 Colunas ========================================================
                table = new PdfPTable(7);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 39, 1, 14, 1, 30 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Formação:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.formacao, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Nacionalidade:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.pais_nasc, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 7 Colunas ========================================================
                table = new PdfPTable(7);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 39, 1, 14, 1, 30 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Endereço:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.logradouro_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Nº:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.numero_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 7 Colunas ========================================================
                table = new PdfPTable(7);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 39, 1, 14, 1, 30 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Complemento:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.complemento_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Bairro:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.bairro_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 7 Colunas ========================================================
                table = new PdfPTable(7);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 39, 1, 14, 1, 30 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("CEP:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.cep_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("UF:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.uf_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 3 Colunas ========================================================
                table = new PdfPTable(3);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 84 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Cidade:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.cidade_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                //table.SpacingAfter = 15f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;
                p.Add(new Chunk("doravante designado simplesmente ALUNO, celebram o presente CONTRATO DE PRESTAÇÃO DE SERVIÇOS EDUCACIONAIS DE MESTRADO PROFISSIONAL EM ", font_Verdana_8_Normal));
                p.Add(new Chunk(item_matricula.turmas.cursos.nome.ToUpper(), font_Verdana_8_Normal));
                p.Add(new Chunk(", doravante designado simplesmente CURSO de Mestrado Profissional, com a interveniência e anuência da FUNDAÇÃO DE APOIO AO INSTITUTO DE PESQUISAS TECNOLÓGICAS - FIPT, pessoa jurídica de direito privado, sem fins lucrativos, com sede na cidade São Paulo - SP, na ", font_Verdana_8_Normal));
                p.Add(new Chunk(item_endereco.endereco + ", " + item_endereco.numero, font_Verdana_8_Normal));
                if (item_endereco.complemento != "" && item_endereco.complemento != null)
                {
                    p.Add(new Chunk(" - " + item_endereco.complemento, font_Verdana_8_Normal));
                }
                p.Add(new Chunk(" - " + item_endereco.bairro + ", " + item_endereco.cidade + " - " + item_endereco.estado + " - CEP " + item_endereco.cep, font_Verdana_8_Normal));
                p.Add(new Chunk(", inscrita no CNPJ sob nº. " + item_endereco.cnpj + ", neste ato, representada por ", font_Verdana_8_Normal));
                p.Add(new Chunk(item_contato.diretor, font_Verdana_8_Normal));
                p.Add(new Chunk(", doravante designada simplesmente FIPT, na forma, cláusulas e condições abaixo:\n\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("CLÁUSULA PRIMEIRA - OBJETO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("1.1 - O presente contrato tem por objeto a prestação de serviços educacionais no âmbito do ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(" de Mestrado Profissional em " + item_matricula.turmas.cursos.nome + ", pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" ao ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", desde que satisfeitos os requisitos estabelecidos no Regimento de Pós - Graduação Mestrado Profissional, que é sua parte integrante.\n\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("CLÁUSULA SEGUNDA - ORGANIZAÇÃO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("2.1 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(" é mantido pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", sendo que o conteúdo da sua programação, a emissão do respectivo certificado de conclusão e as atividades extracurriculares são de sua inteira responsabilidade.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("2.2 - A ", font_Verdana_8_Normal));
                p.Add(new Chunk("FIPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" será a gestora administrativa e financeira do presente contrato, cabendo-lhe emitir boletos de cobrança, recibos e adotar todas as providências de caráter administrativo e financeiro em nome do ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("CLÁUSULA TERCEIRA - PRAZO DE VIGÊNCIA\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("3.1 - O prazo de vigência do presente contrato é de " + item_contato.prazo + " meses computados a partir de São Paulo, " + String.Format("{0:dd}", item_contato.inicio) + " de " + dtfi.GetMonthName(Convert.ToDateTime(item_contato.inicio).Month) + " de " + Convert.ToDateTime(item_contato.inicio).Year + ", até o final do curso.\n\n\n", font_Verdana_8_Normal));

                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                doc.NewPage();

                pageHeaderFooter.qPagina = 2;

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;

                p.Add(new Chunk("CLÁUSULA QUARTA - DO VALOR DO INVESTIMENTO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("4.1 - O valor do investimento no ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(" corresponde ao valor referente à " + item_contato.qtd_disciplina + " disciplinas a serem cursadas e a Orientação para a Dissertação, totalizando " + string.Format("{0:C}", item_contato.valor_total) + " (" + Utilizades.EscreverExtenso(Convert.ToDecimal(item_contato.valor_total)) + "), que serão pagas em " + item_contato.num_parcelas + " mensalidades.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("\nCLÁUSULA QUINTA - FORMA DE PAGAMENTO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("5.1 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" pagará, no primeiro ano de curso, 12 (Doze) parcelas mensais e sucessivas no valor de " + string.Format("{0:C}", item_contato.valor_parcela) + " (" + Utilizades.EscreverExtenso(Convert.ToDecimal(item_contato.valor_parcela)) + ") com vencimento no 15º dia de cada mês.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("5.2 - Para o pagamento do valor correspondente aos anos subsequentes do curso, será observado o disposto na cláusula sexta deste ajuste.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("5.3 - Para cada disciplina adicional às " + item_contato.qtd_disciplina + " disciplinas previstas, o ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" pagará um valor suplementar, que poderá parcelado em 4 vezes no período em que for cursada.\n\n", font_Verdana_8_Normal));

                //if (item_contato.valor_disciplina == 0)
                //{
                //    p.Add(new Chunk("5.3 - Para cada disciplina adicional as " + item_contato.qtd_disciplina + " disciplinas previstas, o aluno pagará o valor adicional de R$ 0,00 (Zero), parcelado em 4 vezes, no período em que for cursada.\n\n", font_Verdana_8_Normal));
                //}
                //else
                //{
                //    p.Add(new Chunk("5.3 - Para cada disciplina adicional as " + item_contato.qtd_disciplina + " disciplinas previstas, o aluno pagará o valor adicional de " + string.Format("{ 0:C }", item_contato.valor_disciplina) + " (" + Utilizades.EscreverExtenso(Convert.ToDecimal(item_contato.valor_disciplina)) + "), parcelado em 4 vezes, no período em que for cursada.\n\n", font_Verdana_8_Normal));
                //}

                //if (item_contato.valor_disciplina == 0)
                //{
                //    p.Add(new Chunk("   5.3.1 - Caso o aluno repita uma disciplina, ou a substitua por outra, por reprovação ou cancelamento após a metade do período letivo, o aluno pagará o valor adicional de R$ 0,00 (Zero), parcelado em 4 vezes, no período em que for cursada.\n\n", font_Verdana_8_Normal));
                //}
                //else
                //{
                //    p.Add(new Chunk("   5.3.1 - Caso o aluno repita uma disciplina, ou a substitua por outra, por reprovação ou cancelamento após a metade do período letivo, o aluno pagará o valor adicional de" + string.Format("{ 0:C }", item_contato.valor_disciplina) + " (" + Utilizades.EscreverExtenso(Convert.ToDecimal(item_contato.valor_disciplina)) + "), parcelado em 4 vezes, no período em que for cursada.\n\n", font_Verdana_8_Normal));
                //}

                p.Add(new Chunk("   5.3.1 - Outras atividades que não as especificadas em 4.1 , inclusive as extracurriculares e administrativas, terão seus valores fixados pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", ressarcidas à parte, pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", ao ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", por meio da ", font_Verdana_8_Normal));
                p.Add(new Chunk("FIPT", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("5.4 - Com o pagamento dos valores referidos nesta cláusula à ", font_Verdana_8_Normal));
                p.Add(new Chunk("FIPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", o ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" dá quitação ao ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", relativas às parcelas pagas.\n\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("5.5 - DEVOLUÇÃO DE VALORES PAGOS.\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("   5.5.1 - O cancelamento da matricula poderá ser solicitado a qualquer tempo, valendo as regras a seguir para devolução da taxa de matricula;\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("       a) No caso da solicitação ter sido protocolada na secretaria acadêmica da CET /IPT até 7 dias corridos antes do início das aulas, o requerente terá direito à devolução integral do valor da matricula;\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("       b) Solicitações feitas após o período explicitado na letra (a) deste artigo e até o primeiro dia letivo darão ao requerente direito à devolução de 50 % do valor da taxa de matricula;\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("   5.5.2 - Pedido de cancelamento do curso, deverá ser formalizado através de requerimento protocolado na secretaria acadêmica da CET / IPT até o dia 5 de cada mês, dará ao requerente suspensão do pagamento de mensalidades futuras, a partir do mês em  curso.Caso o pedido seja feito após o dia 5 do correspondente mês, será cobrada a mensalidade do mês.O pedido de cancelamento do curso não isenta o requerente de pagamento de débitos anteriores à data do pedido.\n\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("CLÁUSULA SEXTA - DO REAJUSTE\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("6.1 - O valor do presente contrato será reajustado, respeitada a periodicidade mínima legal de 12 (doze) meses, de acordo com a variação do IPC - FIPE.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("6.2 - As parcelas que compõem o valor do presente contrato serão, para o primeiro ano de sua vigência, fixas e irreajustáveis, enquanto que, para os anos subsequentes de sua vigência, serão reajustadas pela variação do IPC - FIPE. Caso remanesçam parcelas a serem pagas após o término da vigência do contrato, estas também serão reajustadas pela variação do IPC - FIPE.\n\n\n", font_Verdana_8_Normal));

                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                doc.NewPage();
                pageHeaderFooter.qPagina = 3;

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;

                p.Add(new Chunk("CLÁUSULA SÉTIMA - PENALIDADES\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("7.1 - O atraso nos pagamentos devidos pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" implica na aplicação de multa moratória correspondente a 2% (dois por cento) sobre o valor da parcela em atraso e juros de mora de 1 % (um por cento) ao mês, sem prejuízo da correção monetária e encargos financeiros, tendo como base o IGPM da FGV.\n\n", font_Verdana_8_Normal));

                //p.Add(new Chunk("7.2 - O ", font_Verdana_8_Normal));
                //p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                //p.Add(new Chunk(" tem ciência neste ato, que em caso de inadimplência das parcelas ou de qualquer obrigação de pagamento decorrente deste contrato, por mais de 90(noventa) dias, a FIPT poderá comunicar / Inserir o debito em órgão(s) e / ou cadastro(s) de devedor(es) legalmente existente para registro(p.ex.SPC / SERASA), nos termos do artigo 43 da Lei. 8.078 / 90(Código de Defesa do Consumidor), além de exigir judicialmente a divida.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("7.2 - Para todos os efeitos, será considerada inadimplência o atraso por mais de 60(sessenta) dias no pagamento das mensalidades, situação na qual o ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", por meio da ", font_Verdana_8_Normal));
                p.Add(new Chunk("FIPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", poderá optar, cumulativamente ou não.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("       a) pela emissão de duplicata de serviços, pelo valor da(s) parcela(s) vencida(s) e não paga(s) acrescida(s) da multa e juros supra mencionados, além de custas e honorários advocatícios;\n\n", font_Verdana_8_Normal));
                p.Add(new Chunk("       b) pelo protesto do título executivo extrajudicial;\n\n", font_Verdana_8_Normal));
                p.Add(new Chunk("       c) pela cobrança extrajudicial ou judicial, arcando o ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", além de todas as despesas que recaírem sobre o débito, com os honorários advocatícios;\n\n", font_Verdana_8_Normal));
                p.Add(new Chunk("      d) pela inscrição do ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" no Cadastro de Proteção ao Crédito como inadimplente, após notificação, na falta de pagamento de duas parcelas, consecutivas ou não;\n\n", font_Verdana_8_Normal));
                p.Add(new Chunk("       e) pela suspensão da prestação dos serviços educacionais, após notificação ao ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));



                p.Add(new Chunk("\n   Paragrafo único", font_Verdana_8_Bold));
                p.Add(new Chunk(": Em caso de cobrança judicial respondera ainda o(a) devedor(a) pelas custas judicias e honorários advocatícios de 20 % (vinte por cento).\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("\nCLÁUSULA OITAVA - RESCISÃO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("8.1 - O presente ajuste poderá ser rescindido injustificadamente por iniciativa do ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", a ser comunicada formalmente ao ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" por meio da ", font_Verdana_8_Normal));
                p.Add(new Chunk("FIPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", ou por iniciativa do ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", na hipótese de resultado acadêmico insuficiente ou falta grave do ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", a critério da Comissão de Pós - Graduação(CPG) do Programa de Mestrado do ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("CLÁUSULA NONA - DISPOSIÇÕES GERAIS\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("9.1 - A vida acadêmica do ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" será regulada pelo Regimento da Pós-Graduação do ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" vigente na data de assinatura deste contrato.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("9.2 - As aulas do programa de Mestrado são presenciais, mas eventualmente poderá haver aulas de modo remoto em plataforma acessível a todos, de comum acordo entre o docente e o ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("9.3 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" deverá cumprir também, as regras específicas do curso estabelecidas no Caderno do Aluno, que é parte integrante deste contrato.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("9.4 - A conclusão dos créditos de disciplinas não confere, ao ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", qualquer título acadêmico ou profissional.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("9.5 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" está ciente que, sem a autorização expressa do professor responsável, não será permitida a gravação de imagens, conteúdos, voz, edição, reprodução de nenhum material utilizado em aulas, por nenhum meio, bem como, a utilização do conteúdo para quaisquer finalidades estranhas ao estudo e aprendizado das disciplinas. O descumprimento caracterizará infração disciplinar (violação do Código de Ética e Conduta do IPT) e poderá caracterizar, ainda, infração cível e eventualmente criminal.\n\n", font_Verdana_8_Normal));

                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                doc.NewPage();
                pageHeaderFooter.qPagina = 4;
                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;

                p.Add(new Chunk("\n ", font_Verdana_8_Normal));
                p.Add(new Chunk("9.6 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", neste ato, autoriza a utilização de sua imagem em todos os veículos de comunicação e propaganda, para fim exclusivo de divulgação dos resultados obtidos em decorrência do Curso hora contratado.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("   Paragrafo único", font_Verdana_8_Bold));
                p.Add(new Chunk(": Em nenhuma hipótese poderá a imagem do ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" ser utilizada de maneira contraria à moral, aos bons costumes e à ordem pública.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("9.7 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" será desligado do programa de MP caso não efetue a matricula em todos os quadrimestres, inclusive após conseguir todos os créditos em disciplina.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("9.8 - A emissão do Certificado de Conclusão do Curso fica condicionada à entrega pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" de toda a documentação exigida pelo Regimento da Pós Graduação do IPT, ao pagamento integral do valor do Curso, bem como ao cumprimento de todos os requisitos acadêmicos do mesmo.\n\n", font_Verdana_8_Normal));
                
                p.Add(new Chunk("\nCLÁUSULA DÉCIMA - SUSPENSÃO (TRANCAMENTO)\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("10.1 - O presente ajuste poderá ser suspenso uma única vez por um período máximo de 4(quatro) meses, por iniciativa do ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" a ser comunicada formalmente ao ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", com o aproveitamento dos créditos já cursados, bem como dos valores pagos, quando da retomada do curso.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("\nCLÁUSULA DÉCIMA PRIMEIRA - NOVAÇÃO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("11.1 - A tolerância relativamente ao descumprimento de quaisquer das obrigações por parte do ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" não configurará novação, descabendo, portanto, as eventuais alegações de direito adquirido.\n\n", font_Verdana_8_Normal));


                p.Add(new Chunk("\nCLÁUSULA DÉCIMA SEGUNDA - DA PERDA DA CONDIÇÃO DE BOLSISTA\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("12.1 - Perderá a condição de bolsista o ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" que tiver rescindido seu contrato de trabalho com o ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" ou que sua participação em projetos de interesse do ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" seja encerrada.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("12.2 - Perdendo a condição de bolsista, o ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" passará a pagar o valor total do investimento, sem a redução concedida a título de bolsa.\n\n", font_Verdana_8_Normal));


                p.Add(new Chunk("\nCLÁUSULA DÉCIMA TERCEIRA - FORO DE ELEIÇÃO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("13.1 - Para dirimir quaisquer questões oriundas deste Contrato ou da prestação de serviços nele contratadas, fica eleito o Foro da Comarca da Capital do Estado de São Paulo, com exclusão de qualquer outro, por mais privilegiado que seja ou venha a ser.\n\n", font_Verdana_8_Normal));

                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                doc.NewPage();
                pageHeaderFooter.qPagina = 5;
                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;

                p.Add(new Chunk("\n\nE por estarem acordadas, assinam as ", font_Verdana_8_Normal));
                p.Add(new Chunk("PARTES", font_Verdana_8_Bold));
                p.Add(new Chunk(" o presente instrumento em 02 (duas) vias idênticas, rubricando todas as suas páginas, na presença das testemunhas abaixo identificadas.\n\n", font_Verdana_8_Normal));

                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 530f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                p.Add(new Chunk("\n\nSão Paulo, " + String.Format("{0:dd}", DateTime.Today) + " de " + dtfi.GetMonthName(Convert.ToDateTime(DateTime.Today).Month) + " de " + Convert.ToDateTime(DateTime.Today).Year, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 3 Colunas ========================================================
                table = new PdfPTable(3);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 30, 40, 30 };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("\n\n\n", font_Verdana_8_Normal));
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\n" + item.idaluno + " - " + item.nome, font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 100 };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("\n\nFUNDAÇÃO DE APOIO AO INSTITUTO DE PESQUISAS TECNOLÓGICAS - FIPT\n", font_Verdana_8_Bold));
                p.Add(new Chunk("INSTITUTO DE PESQUISAS TECNOLÓGICAS DO ESTADO DE SÃO PAULO S/A - IPT\n\n\n\n\n\n\n\n", font_Verdana_8_Bold));

                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela de 5 Colunas ========================================================
                table = new PdfPTable(5);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 5, 40, 10, 40, 5 };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\n" + item_contato.secretaria + "\nSecretária", font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\n" + item_contato.coordenador + "\nCoordenador", font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 100 };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                p.Add(new Chunk("\n\n\n\nTestemunhas:\n\n\n\n", font_Verdana_8_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela de 5 Colunas ========================================================
                table = new PdfPTable(5);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 5, 40, 10, 40, 5 };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\n" + item_contato.testemunha_1 + "\nRG: " + item_contato.rg_testemunha_1, font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\n" + item_contato.testemunha_2 + "\nRG: " + item_contato.rg_testemunha_2, font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão do Contrato";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }

        }

        protected void GeraContratoFundicao(string qTipoContrato, string qDataContrato, string qValorTotal, string qValorDisciplina, string qNumeroParcela,
            string qValorParcela, string qDataInicioCurso, string qPrazo, string qCoordenador, string qSecretaria, string qTextemunha1, string qRGTextemunha1,
            string qTextemunha2, string qRGTextemunha2, string qParagrafoDiretor, string qIdTurma, string qNome)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                GeraisAplicacao AplicacaoGerais = new GeraisAplicacao();
                alunos item;
                string qTab = HttpContext.Current.Request.Form["hQTab"];
                item = (alunos)Session[qTab + "Aluno"];

                matricula_turma item_matricula = new matricula_turma();
                item_matricula = item.matricula_turma.Where(x => x.id_turma == Convert.ToInt32(qIdTurma)).SingleOrDefault();

                Enderecos item_endereco = new Enderecos();
                item_endereco.id_endereco = 1; //FIPT
                item_endereco = AplicacaoGerais.BuscaEndereco(item_endereco);

                dados_contratos item_contato = new dados_contratos();
                item_contato.nome_contrato = qTipoContrato;
                item_contato = AplicacaoGerais.BuscaContrato(item_contato);

                item_contato.valor_total = Convert.ToDecimal(qValorTotal);
                item_contato.num_parcelas = Convert.ToInt32(qNumeroParcela);
                item_contato.valor_parcela = Convert.ToDecimal(qValorParcela);
                item_contato.valor_disciplina = Convert.ToDecimal(qValorDisciplina);
                item_contato.prazo = Convert.ToInt32(qPrazo);
                item_contato.inicio = Convert.ToDateTime(qDataInicioCurso);
                item_contato.coordenador = qCoordenador;
                item_contato.secretaria = qSecretaria;
                item_contato.testemunha_1 = qTextemunha1;
                item_contato.testemunha_2 = qTextemunha2;
                item_contato.rg_testemunha_1 = qRGTextemunha1;
                item_contato.rg_testemunha_2 = qRGTextemunha2;
                item_contato.diretor = qParagrafoDiretor;
                item_contato.status = "alterado";
                item_contato.data_alteracao = DateTime.Now;
                item_contato.usuario = usuario.usuario;

                item_contato = AplicacaoGerais.AlterarContrato(item_contato);

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 120, 40);//estibulando o espaçamento das margens que queremos ===Antigo===(45, 40, 40, 40)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/Contrato_" + qNome + ".pdf"), FileMode.Create));
                PDF_Cabec_Rodape_Contrato pageHeaderFooter = new PDF_Cabec_Rodape_Contrato();
                pageHeaderFooter.Caminho = Server.MapPath("~");
                pageHeaderFooter.qPagina = 1;
                writer.PageEvent = pageHeaderFooter;
                doc.Open();

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font font_Verdana_10_Bold = new Font(_bfVerdanaNormal, 10, Font.BOLD);
                Font font_Verdana_10_Normal = new Font(_bfVerdanaNormal, 10, Font.NORMAL);
                Font font_Verdana_11_Bold = new Font(_bfVerdanaNormal, 11, Font.BOLD);
                Font font_Verdana_11_Normal = new Font(_bfVerdanaNormal, 11, Font.NORMAL);
                Font font_Verdana_14_Bold = new Font(_bfVerdanaNormal, 14, Font.BOLD);
                Font font_Verdana_14_Normal = new Font(_bfVerdanaNormal, 14, Font.NORMAL);
                Font font_Verdana_12_Bold = new Font(_bfVerdanaNormal, 12, Font.BOLD);
                Font font_Verdana_12_Normal = new Font(_bfVerdanaNormal, 12, Font.NORMAL);
                Font font_Verdana_9_Bold = new Font(_bfVerdanaNormal, 9, Font.BOLD);
                Font font_Verdana_9_Normal = new Font(_bfVerdanaNormal, 9, Font.NORMAL);
                Font font_Verdana_8_Bold = new Font(_bfVerdanaNormal, 8, Font.BOLD);
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Verdana_7_Bold = new Font(_bfVerdanaNormal, 7, Font.BOLD);
                Font font_Verdana_7_Normal = new Font(_bfVerdanaNormal, 7, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);
                Color FontColor_Cinza = new Color(204, 204, 204);
                Color FontColor_CinzaClaro = new Color(229, 229, 229);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                float[] widths;
                Paragraph p;
                PdfPCell cell;
                PdfPTable table;

                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 530f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                //p.Add(new Chunk("Coordenadoria de Ensino Tecnológico - CET\n", font_Verdana_11_Bold));
                p.Add(new Chunk(" \n", font_Verdana_11_Bold));
                p.Add(new Chunk("Contrato de Prestação de Serviços Educacionais de " + item_matricula.turmas.cursos.tipos_curso.tipo_curso + "\n", font_Verdana_11_Normal));
                p.Add(new Chunk("em " + item_matricula.turmas.cursos.nome, font_Verdana_11_Normal));
                //p.SpacingAfter = 100;
                //p.SetLeading(12, 10);
                p.SpacingAfter = 4f;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 2;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;
                p.Add(new Chunk("O ", font_Verdana_8_Normal));
                p.Add(new Chunk("INSTITUTO DE PESQUISAS TECNOLÓGICAS DO ESTADO DE SÃO PAULO S/A - IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", empresa pública constituída nos termos da Lei Estadual no. 896/75, com sede na Capital do Estado de São Paulo, na Cidade Universitária “Armando de Salles Oliveira”, inscrito CNPJ / MF nº. 60.633.674 / 0001 - 55 e Inscrição Estadual nº. 105.933.432.110, neste ato representado por seus Procuradores abaixo assinados, doravante designados simplesmente ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", e o aluno:", font_Verdana_8_Normal));
                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 3 Colunas ========================================================
                table = new PdfPTable(3);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 84 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Nome:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.nome, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 7 Colunas ========================================================
                table = new PdfPTable(7);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 39, 1, 14, 1, 30 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("RG:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                string sAux;
                if (item.digito_num_documento != "" && item.digito_num_documento != null)
                {
                    sAux = item.numero_documento + "-" + item.digito_num_documento;
                }
                else
                {
                    sAux = item.numero_documento;
                }

                p.Add(new Chunk(sAux, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("CPF:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.cpf, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 7 Colunas ========================================================
                table = new PdfPTable(7);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 39, 1, 14, 1, 30 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Formação:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.formacao, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Nacionalidade:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.pais_nasc, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 7 Colunas ========================================================
                table = new PdfPTable(7);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 39, 1, 14, 1, 30 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Endereço:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.logradouro_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Nº:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.numero_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 7 Colunas ========================================================
                table = new PdfPTable(7);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 39, 1, 14, 1, 30 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Complemento:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.complemento_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Bairro:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.bairro_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 7 Colunas ========================================================
                table = new PdfPTable(7);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 39, 1, 14, 1, 30 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("CEP:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.cep_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("UF:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.uf_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 3 Colunas ========================================================
                table = new PdfPTable(3);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 15, 1, 84 };
                table.SetWidths(widths);

                table.SpacingAfter = 3f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("Cidade:", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk(item.cidade_res, font_Verdana_9_Normal));
                p.SetLeading(4f, 0.5f);
                p.SpacingAfter = 2;
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                //table.SpacingAfter = 15f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;
                p.Add(new Chunk("doravante designado simplesmente ALUNO, celebram o presente CONTRATO DE PRESTAÇÃO DE SERVIÇOS EDUCACIONAIS DE " + item_matricula.turmas.cursos.tipos_curso.tipo_curso.ToUpper() + " EM ", font_Verdana_8_Normal));
                p.Add(new Chunk(item_matricula.turmas.cursos.nome.ToUpper(), font_Verdana_8_Normal));
                p.Add(new Chunk(", doravante designado simplesmente CURSO de " + item_matricula.turmas.cursos.tipos_curso.tipo_curso + ", com a interveniência e anuência da FUNDAÇÃO DE APOIO AO INSTITUTO DE PESQUISAS TECNOLÓGICAS - FIPT, pessoa jurídica de direito privado, sem fins lucrativos, com sede na cidade São Paulo - SP, na ", font_Verdana_8_Normal));
                p.Add(new Chunk(item_endereco.endereco + ", " + item_endereco.numero, font_Verdana_8_Normal));
                if (item_endereco.complemento != "" && item_endereco.complemento != null)
                {
                    p.Add(new Chunk(" - " + item_endereco.complemento, font_Verdana_8_Normal));
                }
                p.Add(new Chunk(" - " + item_endereco.bairro + ", " + item_endereco.cidade + " - " + item_endereco.estado + " - CEP " + item_endereco.cep, font_Verdana_8_Normal));
                p.Add(new Chunk(", inscrita no CNPJ sob nº. " + item_endereco.cnpj + ", neste ato, representada por ", font_Verdana_8_Normal));
                p.Add(new Chunk(item_contato.diretor, font_Verdana_8_Normal));
                p.Add(new Chunk(", doravante designada simplesmente FIPT, na forma, cláusulas e condições abaixo:\n\n\n", font_Verdana_8_Normal));


                p.Add(new Chunk("CLÁUSULA PRIMEIRA - OBJETO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("1.1 - O presente contrato tem por objeto a prestação de serviços educacionais no âmbito do ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(", pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" ao ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("\nCLÁUSULA SEGUNDA - ORGANIZAÇÃO E OBRIGAÇÕES\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("2.1 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(" é mantido pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", sendo que o conteúdo da sua programação, a emissão do respectivo certificado de conclusão e as atividades curriculares e extracurriculares são de sua inteira responsabilidade, sendo o curso ministrado em conformidade com o previsto na legislação de ensino em vigor, e em seu planejamento pedagógico.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("2.2 - As aulas serão ministradas em salas, laboratórios ou em locais que o ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" indicar, considerada a natureza de conteúdo, característica, peculiaridade e demais atividades que o ensino exigir, buscando, inclusive, otimizar a relação número de alunos por classe.\n\n", font_Verdana_8_Normal));

                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(2);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 7, 93 };
                table.SetWidths(widths);

                //table.SpacingAfter = 15f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(11f, 0);
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;
                p.Add(new Chunk("Parágrafo Primeiro.", font_Verdana_8_Bold));
                p.Add(new Chunk(" A matrícula, ato indispensável que estabelece o vínculo do aluno com o ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", dar-se à formalmente, através do preenchimento do formulário próprio, fornecido pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", com o pagamento da primeira parcela da anuidade, com a assinatura deste Contrato e observância da Cláusula quarta deste instrumento.\n\n", font_Verdana_8_Normal));

                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                //table.SpacingAfter = 15f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;

                p.Add(new Chunk("2.3 - A ", font_Verdana_8_Normal));
                p.Add(new Chunk("FITP", font_Verdana_8_Bold));
                p.Add(new Chunk(" será a gestora administrativa e financeira do presente contrato, cabendo-lhe emitir boletos de cobrança, recibos e adotar todas as providências de caráter administrativo e financeiro em nome do ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("2.1 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(" é mantido pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", sendo que o conteúdo da sua programação, a emissão do respectivo certificado de conclusão e as atividades curriculares e extracurriculares são de sua inteira responsabilidade, sendo o curso ministrado em conformidade com o previsto na legislação de ensino em vigor, e em seu planejamento pedagógico.\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("2.4 - É de exclusiva competência do ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" o planejamento, escolha de professores, orientação didática, pedagógica e educacional, fixação da carga horária e plano pedagógico, marcação de datas de provas e atividades de verificação de aproveitamento e demais providências de ensino.\n\n", font_Verdana_8_Normal));

                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Nova pagina ========================================================

                doc.NewPage();
                pageHeaderFooter.qPagina = 2;


                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                //table.SpacingAfter = 15f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;

                p.Add(new Chunk("\n\nCLÁUSULA TERCEIRA - DAS OBRIGAÇÕES DO ALUNO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("3.1 - Cumprir todas as obrigações acadêmicas e disciplinares do ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("3.2 - Cumprir com as obrigações financeiras sob sua responsabilidade, e seguir as regras do ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(" além de responsabiliza-se pelos prejuízos que venha a causar às instalações do ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" ou a terceiros, em decorrência da utilização da estrutura física disponibilizada pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("\nCLÁUSULA QUARTA - PRAZO DE VIGÊNCIA\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("4.1 - O prazo de vigência do presente contrato, para efeito das obrigações financeiras, é de " + item_contato.prazo + " meses, a partir da data de inicio do ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("4.2 O prazo de validade do presente contrato inicia-se na data de sua assinatura pelos participantes deste contrato e resolve-se de pleno acordo ao término do ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("\nCLÁUSULA QUINTA - DO VALOR DO INVESTIMENTO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("5.1 - O valor do investimento no ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(" será de ", font_Verdana_8_Normal));
                p.Add(new Chunk(string.Format("{0:C}", item_contato.valor_total) , font_Verdana_8_Normal));
                p.Add(new Chunk(" (" + Utilizades.EscreverExtenso(Convert.ToDecimal(item_contato.valor_total)) + ")", font_Verdana_8_Normal));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("\nCLÁUSULA SEXTA - FORMA DE PAGAMENTO\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("6.1 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" pagará " + item_contato.num_parcelas + " parcelas mensais e sucessivas no valor de " , font_Verdana_8_Normal));
                p.Add(new Chunk(string.Format("{0:C}", item_contato.valor_parcela), font_Verdana_8_Normal));
                p.Add(new Chunk(" (" + Utilizades.EscreverExtenso(Convert.ToDecimal(item_contato.valor_parcela)) + ")", font_Verdana_8_Normal));
                p.Add(new Chunk(" com vencimento no 15° dia de cada mês", font_Verdana_8_Normal));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("6.2 - Outras atividades que não as especificadas no item 3.1 da cláusula terceira não integram o valor total do investimento e deverão ser ressarcidas à parte, pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", ao ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", por meio da ", font_Verdana_8_Normal));
                p.Add(new Chunk("FIPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", relativa às parcelas pagas", font_Verdana_8_Normal));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("6.3 - Com o pagamento dos valores referidos no item 6.1 desta cláusula à ", font_Verdana_8_Normal));
                p.Add(new Chunk("FITP", font_Verdana_8_Bold));
                p.Add(new Chunk(", o ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", dá quitação ao ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("\nCLÁUSULA SETIMA - DO REAJUSTE\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("7.1 - O valor do presente contrato não sofrerá reajuste durante o período de vigência do ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("\nCLÁUSULA OITAVA - DAS PENALIDADES\n\n", font_Verdana_8_Bold));

                p.Add(new Chunk("8.1 - O atraso nos pagamentos devidos pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" implicará na aplicação de multa de 2% (dois por cento) sobre o valor da parcela em atraso e juros de mora de 1 % (um por cento) ao mês, sem prejuízo da correção monetária e encargos financeiros, tendo como base o IGPM da FGV", font_Verdana_8_Normal));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("8.2 - Para todos os efeitos, será considerada inadimplência o atraso por mais de 60(sessenta) dias no pagamento das mensalidades, situação na qual o ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", por meio da ", font_Verdana_8_Normal));
                p.Add(new Chunk("FIPT", font_Verdana_8_Bold));
                p.Add(new Chunk(", poderá optar, cumulativamente ou não", font_Verdana_8_Normal));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //===============================================

                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(2);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 7, 93 };
                table.SetWidths(widths);

                //table.SpacingAfter = 15f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(11f, 0);
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;
                p.Add(new Chunk("a) pela emissão de duplicata de serviços, pelo valor da(s) parcela(s) vencida(s) e não paga(s) acrescida(s) da multa e juros supra mencionados, além de custas e honorários advocatícios;\n\n", font_Verdana_8_Normal));
                p.Add(new Chunk("b) pelo protesto do título executivo extrajudicial;\n\n", font_Verdana_8_Normal));
                p.Add(new Chunk("c) pela cobrança extrajudicial ou judicial, arcando o ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", além de todas as despesas que recaírem sobre o débito, com os honorários advocatícios;", font_Verdana_8_Normal));
                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Nova pagina ========================================================

                doc.NewPage();
                pageHeaderFooter.qPagina = 3;

                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(2);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 7, 93 };
                table.SetWidths(widths);

                //table.SpacingAfter = 15f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(11f, 0);
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;
                p.Add(new Chunk("\n\nd) pela inscrição do ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" no Cadastro de Proteção ao Crédito como inadimplente, após notificação, na falta de pagamento de duas parcelas, consecutivas ou não;\n\n", font_Verdana_8_Normal));
                p.Add(new Chunk("e) pela suspensão da prestação dos serviços educacionais, após notificação ao ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                //table.SpacingAfter = 15f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;

                p.Add(new Chunk("\nCLÁUSULA NONA - DA RESCISÃO, E CANCELAMENTO DE MATRÍCULA\n\n ", font_Verdana_8_Bold));

                p.Add(new Chunk("9.1 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" poderá solicitar o cancelamento de matrícula do curso no qual se encontra regularmente matrículado, a qualquer tempo, com a expressa ciência e anuência da Coordenação do curso, porém", font_Verdana_8_Normal));
                p.Add(new Chunk(":\n\n", font_Verdana_8_Normal));

                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(2);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 7, 93 };
                table.SetWidths(widths);

                //table.SpacingAfter = 15f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_MIDDLE;
                p.Add(new Chunk("", font_Verdana_9_Bold));
                p.SetLeading(11f, 0);
                cell = new PdfPCell();
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.BorderWidth = 1;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;
                p.Add(new Chunk("9.1.1 - No caso de solicitação de cancelamento de matrícula, o requerente terá direito à devolução integral do valor da matrícula, se a solicitação for realizada até 07(sete) dias antes do início do curso;\n\n", font_Verdana_8_Normal));
                p.Add(new Chunk("9.1.2 - Solicitações feitas após o período explícitado no item 9.1.1 e até o primeiro dia de início do curso, darão ao requerente direito à devolução de 20 % do valor da matrícula;\n\n", font_Verdana_8_Normal));
                p.Add(new Chunk("9.1.3 - Solicitações feitas após o início do curso, não darão direito à devolução do valor da matrícula ao requerente, mas isentam - no de pagamento da mensalidade relativa ao mês em curso, sempre que o cancelamento for solicitado antes do 15° dia do referido mês.\n\n", font_Verdana_8_Normal));

                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                //table.SpacingAfter = 15f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;

                p.Add(new Chunk("9.2 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" não devolverá as quantias já pagas, na hipótese do ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" não obter resultado acadêmico suficiente para receber o certificado de conclusão de curso", font_Verdana_8_Normal));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("\nCLÁUSULA DÉCIMA - NOVAÇÃO\n\n ", font_Verdana_8_Bold));

                p.Add(new Chunk("10.1 - A tolerância relativamente ao descumprimento de quaisquer das obrigações por parte do ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" não configurará novação, descabendo, portanto, as eventuais alegações de direito adquirido", font_Verdana_8_Normal));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("\nCLÁUSULA DÉCIMA PRIMEIRA - DAS DISPOSIÇÕES GERAIS\n\n ", font_Verdana_8_Bold));

                p.Add(new Chunk("11.1 - O ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(" será responsável pelos prejuízos que venha a causar às instalações de propriedade do ", font_Verdana_8_Normal));
                p.Add(new Chunk("IPT", font_Verdana_8_Bold));
                p.Add(new Chunk(" ou a terceiros, em decorrência da utilização da estrutura física do ", font_Verdana_8_Normal));
                p.Add(new Chunk("CURSO", font_Verdana_8_Bold));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("11.2 - Para todas as intimações e comunicações são válidos os endereços fornecidos pelo ", font_Verdana_8_Normal));
                p.Add(new Chunk("ALUNO", font_Verdana_8_Bold));
                p.Add(new Chunk(", constantes no requerimento de matrícula, sendo consideradas entregues todas as remessas para o referido endereço, salvo alterações devidamente comunicadas", font_Verdana_8_Normal));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                p.Add(new Chunk("\nCLÁUSULA DÉCIMA SEGUNDA - FORO DE ELEIÇÃO\n\n ", font_Verdana_8_Bold));

                p.Add(new Chunk("12.1 - Para dirimir quaisquer questões oriundas deste Contrato ou da prestação de serviços nele contratadas, fica eleito o Foro da Comarca da Capital do Estado de São Paulo, com exclusão de qualquer outro, por mais privilegiado que seja ou venha a ser", font_Verdana_8_Normal));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Nova pagina ========================================================

                doc.NewPage();
                pageHeaderFooter.qPagina = 4;

                //======================================================

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 520f };
                table.SetWidths(widths);

                //table.SpacingAfter = 15f;

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_JUSTIFIED;

                p.Add(new Chunk("E por estarem acordadas, assinam as ", font_Verdana_8_Normal));
                p.Add(new Chunk("PARTES", font_Verdana_8_Bold));
                p.Add(new Chunk(" o presente instrumento em 02 (duas) vias idênticas, rubricando todas as suas páginas, na presença das testemunhas abaixo identificadas", font_Verdana_8_Normal));
                p.Add(new Chunk(".\n\n", font_Verdana_8_Normal));

                cell = new PdfPCell();
                p.SetLeading(11f, 0);
                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //===========================================================

                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 530f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                p.Add(new Chunk("\n\nSão Paulo, " + String.Format("{0:dd}", DateTime.Today) + " de " + dtfi.GetMonthName(Convert.ToDateTime(DateTime.Today).Month) + " de " + Convert.ToDateTime(DateTime.Today).Year, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui é uma nova tabela de 3 Colunas ========================================================
                table = new PdfPTable(3);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 30, 40, 30 };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("\n\n\n", font_Verdana_8_Normal));
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\n" + item.idaluno + " - " + item.nome, font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 100 };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("\n\nFUNDAÇÃO DE APOIO AO INSTITUTO DE PESQUISAS TECNOLÓGICAS - FIPT\n", font_Verdana_8_Bold));
                p.Add(new Chunk("INSTITUTO DE PESQUISAS TECNOLÓGICAS DO ESTADO DE SÃO PAULO S/A - IPT\n\n\n\n\n\n\n\n", font_Verdana_8_Bold));

                cell.AddElement(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela de 5 Colunas ========================================================
                table = new PdfPTable(5);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 5, 40, 10, 40, 5 };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\n" + item_contato.secretaria + "\nSecretátia" + item.nome, font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\n" + item_contato.coordenador + "\nCoordenador" + item.nome, font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 100 };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_LEFT;
                p.Add(new Chunk("\n\n\n\nTestemunhas:\n\n\n\n", font_Verdana_8_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //Aqui é uma nova tabela de 5 Colunas ========================================================
                table = new PdfPTable(5);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 5, 40, 10, 40, 5 };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\n" + item_contato.testemunha_1 + "\nRG: " + item_contato.rg_testemunha_1, font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk(linefine));
                p.Add(new Chunk("\n" + item_contato.testemunha_2 + "\nRG: " + item_contato.rg_testemunha_2, font_Verdana_8_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("", font_Verdana_10_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);


                //==================================


                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão da Ata da Qualificação ";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }

        }

        private void AlteraAluno()
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                alunos aluno;
                string qTab = HttpContext.Current.Request.Form["hQTab"];
                aluno = (alunos)Session[qTab + "Aluno"];

                string sAux = "";
                if (txtNomeAluno.Value.Trim() == "" )
                {
                    sAux = "Preencher o nome do Aluno. <br/><br/>";
                }
                if (txtCPFAluno.Value.Trim() == "" || txtCPFAluno.Value.Trim() == "..-")
                {
                    txtCPFAluno.Value = "";
                    sAux = sAux + "Preencher o cpf do Aluno. <br/><br/>";
                }
                if (txtEmail1Aluno.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o email do Aluno. <br/><br/>";
                }
                if (txtCNPJEmpresaAluno.Value.Trim() == "../-")
                {
                    txtCNPJEmpresaAluno.Value = "";
                }

                if (sAux != "")
                {
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-warning');", true);
                    return;
                }

                aluno = CarregaDadosDaTela(aluno);

                aluno.usuario = usuario.usuario;
                aluno.status = "alterado";
                aluno.data_alteracao = DateTime.Now;

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();

                if (aplicacaoAluno.AlterarItem(aluno))
                {
                    UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                    usuarios UsuarioAlteracao = new usuarios();
                    UsuarioAlteracao.usuario = aluno.idaluno.ToString();
                    UsuarioAlteracao = aplicacaoUsuario.BuscaUsuario(UsuarioAlteracao);
                    if (UsuarioAlteracao.avatar != "")
                    {
                        imgAluno.Src = "img/pessoas/" + UsuarioAlteracao.avatar + "?" + DateTime.Now;
                    }

                    else
                    {
                        imgAluno.Src = "img/pessoas/avatarunissex.jpg" + "?" + DateTime.Now;
                    }
                    UsuarioAlteracao.email = aluno.email;
                    aplicacaoUsuario.AlterarUsuario(UsuarioAlteracao);
                    imgFotoOriginal.Src = imgAluno.Src;
                    Session[qTab + "Aluno"] = aluno;
                    lblTituloNomeAluno.Text = aluno.nome;
                    lblAlteradoPor.Text = aluno.usuario;
                    lblAlteradoEm.Text = String.Format("{0:dd/MM/yyyy}", aluno.data_alteracao);

                    lblMensagem.Text = "Dados do Aluno alterados com sucesso.";
                    lblTituloMensagem.Text = "Alteração";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-success');", true);
                }
                else
                {
                    lblMensagem.Text = "Houve algum problema na alteração dos dados do Aluno.";
                    lblTituloMensagem.Text = "Alteração";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-warning');", true);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    lblMensagem.Text = "Houve algum problema na alteração dos dados do Aluno. <br/> <br/>Descrição do erro: " + ex.Message;
                }
                else
                {
                    lblMensagem.Text = "Houve algum problema na alteração dos dados do Aluno. <br/> <br/>Descrição do erro: " + ex.InnerException.InnerException.Message;
                }
                lblTituloMensagem.Text = "Alteração";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-warning');", true);
            }
        }

        private void CriaAluno()
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string qTab = HttpContext.Current.Request.Form["hQTab"];
                alunos aluno = new alunos();

                string sAux = "";
                if (txtNomeAluno.Value.Trim() == "")
                {
                    sAux = "Preencher o nome do Aluno. <br/><br/>";
                }
                if (txtCPFAluno.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o cpf do Aluno. <br/><br/>";
                }
                if (txtEmail1Aluno.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o email do Aluno. <br/><br/>";
                }
                if (sAux != "")
                {
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-warning');", true);
                    return;
                }

                CarregaDadosDaTela(aluno);

                aluno.usuario = usuario.usuario;
                aluno.data_cadastro = DateTime.Now;
                aluno.status = "cadastrado";

                AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();

                aluno = aplicacaoAluno.CriarItem(aluno);

                if (aluno != null)
                {
                    Session[qTab + "Aluno"] = aluno;

                    lblTituloAlunoAluna.Text = (aluno.sexo.ToUpper() == "M") ? "Aluno" : "Aluna";
                    lblTituloNomeAluno.Text = aluno.nome;
                    lblTituloMatricula.Text = "Matrícula";
                    lblNumeroMatricula.Text = aluno.idaluno.ToString();
                    lblTituloAlteradoPor.Text = "Alterado por: ";
                    lblAlteradoPor.Text = aluno.usuario;
                    lblTituloAlteradoEm.Text = "em: ";
                    lblAlteradoEm.Text = String.Format("{0:dd/MM/yyyy}", aluno.data_alteracao);

                    SHA1CryptoServiceProvider objSHA1 = new SHA1CryptoServiceProvider();
                    ASCIIEncoding objEncoding = new ASCIIEncoding();
                    usuarios usuarioAluno = new usuarios();
                    UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                    usuarioAluno.usuario = Convert.ToString(aluno.idaluno);
                    usuarioAluno.nome = aluno.nome;
                    usuarioAluno.un = "Acadêmico";
                    usuarioAluno.email = aluno.email;
                    usuarioAluno.id_grupo_acesso = 2;
                    usuarioAluno.status = 1;
                    usuarioAluno.avatar = "";
                    usuarioAluno.nomeSocial = aluno.nome.Substring(0, aluno.nome.IndexOf(" "));
                    string sAuxSenha;
                    sAuxSenha = aluno.cpf.Replace(".", "").Substring(0, 6);
                    usuarioAluno.senha = Convert.ToBase64String(objSHA1.ComputeHash(objEncoding.GetBytes(sAuxSenha)));

                    aplicacaoUsuario.CriarUsuario(usuarioAluno);

                    Session.Add(qTab + "sNovoAluno", false);
                    Session["AdiciondoSucesso"] = true;

                    btnCriarUsuario.Attributes["class"] = "btn btn-primary";
                    btnCriarUsuario.Disabled = false;

                    divBotaoFoto.Visible = true;
                    divTextoBotaoFoto.Visible = false;
                    tabSituacaoAcademica.Attributes["class"] = "hidden";
                    //tabSituacaoAcademicaNew.Attributes["class"] = "";
                    //tabSituacaoAcademicaNew.Style["display"] = "block";
                    tabSituacaoAcademicaNew.Style.Add("display", "block");
                    CarregaAluno();

                    lblMensagem.Text = "Cadastro do Aluno criado com sucesso.";
                    lblTituloMensagem.Text = "Novo Aluno";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-success');", true);

                }
                else
                {
                    lblMensagem.Text = "Houve algum problema na criação do cadastro do Aluno.";
                    lblTituloMensagem.Text = "Novo Aluno";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-warning');", true);
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na criação do cadastro do Aluno. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Novo Aluno";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-warning');", true);
            }
        }

        private alunos CarregaDadosDaTela(alunos aluno)
        {
            DateTime? DataNula = null;
            int? IntNulo = null;
            aluno.nome = txtNomeAluno.Value.Trim();
            aluno.cpf = txtCPFAluno.Value.Trim();
            aluno.estrangeiro = ddlEstrangeiro.SelectedValue;
            aluno.sexo = ddlSexoAluno.SelectedValue;
            aluno.convenio = txtConvenioAluno.Value.Trim();
            aluno.linha_pesquisa = txtLinhaPesquisaAluno.Value.Trim();
            aluno.data_alteracao = DateTime.Now;
            aluno.data_nascimento = (txtDataNascimentoAluno.Value == "") ? DataNula : Convert.ToDateTime(txtDataNascimentoAluno.Value);
            aluno.pais_nasc = ddlNacionalidadeAluno.SelectedValue;
            if (divTXTEstadoNasctoAluno.Attributes.CssStyle.Value != "display:none;")
            {
                aluno.estado_nasc = txtEstadoNasctoAluno.Value.Trim();
            }
            else
            {
                aluno.estado_nasc = ddlEstadoNasctoAluno.SelectedValue;
            }
            if (divTXTCidadeNasctoAluno.Attributes.CssStyle.Value != "display:none;")
            {
                aluno.cidade_nasc = txtCidadeNasctoAluno.Value.Trim();
            }
            else
            {
                aluno.cidade_nasc = ddlCidadeNasctoAluno.SelectedValue;
            }
            aluno.email = txtEmail1Aluno.Value.Trim();
            aluno.email2 = txtEmail2Aluno.Value.Trim();
            aluno.telefone_res = txtTelefoneAluno.Value.Trim();
            aluno.celular_res = txtCelularAluno.Value.Trim();
            aluno.tipo_documento = ddlTipoDoctoAluno.SelectedValue;
            aluno.numero_documento = txtNumeroDoctoAluno.Value.Trim();
            aluno.digito_num_documento = txtDigitoDoctoAluno.Value.Trim();
            aluno.orgao_expedidor = txtOrgaoExpeditorAluno.Value.Trim();
            aluno.data_expedicao = (txtDataExpedicaoAluno.Value == "") ? DataNula : Convert.ToDateTime(txtDataExpedicaoAluno.Value);
            aluno.data_validade = (txtDataValidadeDoctoAluno.Value == "") ? DataNula : Convert.ToDateTime(txtDataValidadeDoctoAluno.Value);
            aluno.cep_res = txtCepResidenciaAluno.Value;
            aluno.logradouro_res = txtLogradouroResidenciaAluno.Value.Trim();
            aluno.numero_res = txtNumeroResidenciaAluno.Value;
            aluno.complemento_res = txtComplementoResidenciaAluno.Value.Trim();
            aluno.bairro_res = txtBairroResidenciaAluno.Value.Trim();
            aluno.pais_res = ddlPaisResidenciaAluno.SelectedValue;
            if (ddlPaisResidenciaAluno.SelectedValue != "Brasil")
            //if (divTXTEstadoResidenciaAluno.Attributes.CssStyle.Value != "display:none;")
            {
                aluno.uf_res = txtEstadoResidenciaAluno.Value.Trim();
                aluno.cidade_res = txtCidadeResidenciaAluno.Value.Trim();
            }
            else
            {
                aluno.uf_res = ddlEstadoResidenciaAluno.SelectedValue;
                aluno.cidade_res = ddlCidadeResidenciaAluno.SelectedValue;
            }
            //if (divTXTCidadeResidenciaAluno.Attributes.CssStyle.Value != "display:none;")
            //{
            //    aluno.cidade_res = txtCidadeResidenciaAluno.Value.Trim();
            //}
            //else
            //{
            //    aluno.cidade_res = ddlCidadeResidenciaAluno.SelectedValue;
            //}
            aluno.formacao = txtFormacaoAluno.Value.Trim();
            aluno.escola = txtInstituicaoAluno.Value.Trim();
            aluno.ano_graduacao = (txtAnoFormacaoAluno.Value == "") ? 0 : Convert.ToInt16(txtAnoFormacaoAluno.Value);
            aluno.empresa = txtEmpresaAluno.Value.Trim();
            aluno.nome_fantasia = txtNomeFantasiaAluno.Value.Trim();
            aluno.cnpj = txtCNPJEmpresaAluno.Value;
            aluno.ie = txtIEEmpresaAluno.Value;
            aluno.contato = txtNomeContato.Value.Trim();
            aluno.email_contato = txtEmailContato.Value.Trim();
            aluno.cep_empresa = txtCEPEmpresaAluno.Value;
            aluno.logradouro_empresa = txtLogradouroEmpresaAluno.Value.Trim();
            aluno.numero_empresa = txtNumeroEmpresaAluno.Value;
            aluno.complemento_empresa = txtComplementoEmpresaAluno.Value.Trim();
            aluno.bairro_empresa = txtBairroEmpresaAluno.Value.Trim();
            aluno.pais_empresa = ddlPaisEmpresaAluno.SelectedValue;
            if (ddlPaisEmpresaAluno.SelectedValue != "Brasil")
            {
                aluno.uf_empresa = txtEstadoEmpresaAluno.Value.Trim();
                aluno.cidade_empresa = txtCidadeEmpresaAluno.Value.Trim();
            }
            else
            {
                aluno.uf_empresa = ddlEstadoEmpresaAluno.SelectedValue;
                aluno.cidade_empresa = ddlCidadeEmpresaAluno.SelectedValue;
            }
            //if (divTXTCidadeEmpresaAluno.Attributes.CssStyle.Value != "display:none;")
            //{
            //    aluno.cidade_empresa = txtCidadeEmpresaAluno.Value.Trim();
            //}
            //else
            //{
            //    aluno.cidade_empresa = ddlCidadeEmpresaAluno.SelectedValue;
            //}
            aluno.cargo = txtCargoAluno.Value.Trim();
            aluno.telefone_empresa = txtTelefoneEmpresaAluno.Value;
            aluno.telefone_empresa_ramal = txtRamalEmpresaAluno.Value;
            aluno.palavra_chave = txtPalavraChaveAluno.Value.Trim().Replace(",",";");
            aluno.profissao = txtProfissaoAluno.Value.Trim();
            aluno.estado_civil = ddlEstadoCivilAluno.SelectedValue;
            aluno.entregou_rg = chkRG.Checked;
            aluno.entregou_cpf = chkCPF.Checked;
            aluno.entregou_historico = chkHistoricoEscolar.Checked;
            aluno.entregou_diploma = chkDiploma.Checked;
            aluno.entregou_comprovante_end = chkComprovanteEndereco.Checked;
            aluno.entregou_fotos = chkFoto.Checked;
            aluno.entregou_certidao = chkCertidaoNascimento.Checked;
            aluno.entregou_contrato = chkContratoAssinado.Checked;
            aluno.RefazerProficienciaIngles = (chkRefazerProvaProficienciaIngles.Checked) ? Convert.ToByte(1): Convert.ToByte(0);
            aluno.RefazerProvaPortugues = (chkRefazerProvaPortugues.Checked) ? Convert.ToByte(1) : Convert.ToByte(0);
            aluno.entregou_contrato = chkContratoAssinado.Checked;
            aluno.ocorrencias = txtOcorrenciaAluno.Value.Trim();
            return aluno;
        }

        public class PDFHeaderFooterHistoricoOficial : PdfPageEventHelper
        {
            alunos aluno;
            // write on top of document
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                base.OnOpenDocument(writer, document);
            }

            // write on start of each page
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);
                PdfPTable tabFot = new PdfPTable(new float[] { 1F });
                tabFot.SpacingAfter = 10F;
                PdfPCell cell;
                tabFot.TotalWidth = 300F;
                cell = new PdfPCell(new Phrase("Header"));
                tabFot.AddCell(cell);
                tabFot.WriteSelectedRows(0, -1, 150, document.Top, writer.DirectContent);
            }

            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);
                PdfPTable tabFot = new PdfPTable(new float[] { 1F });
                PdfPCell cell;
                tabFot.TotalWidth = 300F;
                cell = new PdfPCell(new Phrase("Footer"));
                tabFot.AddCell(cell);
                tabFot.WriteSelectedRows(0, -1, 150, document.Bottom, writer.DirectContent);
            }

            //write on close of document
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);
            }
        }

    }


}