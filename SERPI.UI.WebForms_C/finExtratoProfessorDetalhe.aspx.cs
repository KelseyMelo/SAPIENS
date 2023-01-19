using Aplicacao_C;
using SERPI.Dominio_C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class finExtratoProfessorDetalhe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 54)) // 4. Extrato do Professor - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                professores item;
                item = (professores)Session["professores"];
                lblTituloProfessor_a.Text = (item.sexo == "m") ? "Professor" : "Professora";
                lblTituloNomeProfessor.Text = item.nome;
                lblTituloCodigo.Text = "Código";
                lblNumeroCodigo.Text = item.id_professor.ToString();
                if (item.professor_observacoes_plano != null)
                {
                    txtObservacaoExtratoProfessor.Value = item.professor_observacoes_plano.observacoes;
                }
                else
                {
                    txtObservacaoExtratoProfessor.Value = "";
                }
                
                if (item.status == "inativado")
                {
                    lblInativado.Style["display"] = "block";
                }
                else
                {
                    lblInativado.Style["display"] = "none";
                }

                usuarios itemUsurioProfessor = new usuarios();
                UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
                itemUsurioProfessor.usuario = item.cpf.ToString();
                itemUsurioProfessor = aplicacaoUsuario.BuscaUsuario(itemUsurioProfessor);
                if (itemUsurioProfessor == null)
                {
                    itemUsurioProfessor = new usuarios();
                    itemUsurioProfessor.usuario = Convert.ToString(item.id_professor) + "p";
                    itemUsurioProfessor = aplicacaoUsuario.BuscaUsuario(itemUsurioProfessor);
                    if (itemUsurioProfessor == null)
                    {
                        SHA1CryptoServiceProvider objSHA1 = new SHA1CryptoServiceProvider();
                        ASCIIEncoding objEncoding = new ASCIIEncoding();
                        usuarios usuarioProfessor = new usuarios();
                        usuarioProfessor.usuario = Convert.ToString(item.id_professor) + "p";
                        usuarioProfessor.nome = item.nome;
                        usuarioProfessor.un = "Acadêmico";
                        usuarioProfessor.email = item.email;
                        usuarioProfessor.id_grupo_acesso = 5;
                        usuarioProfessor.status = 1;
                        usuarioProfessor.avatar = "";
                        usuarioProfessor.nomeSocial = item.nome.Substring(0, item.nome.IndexOf(" "));
                        usuarioProfessor.senha = Convert.ToBase64String(objSHA1.ComputeHash(objEncoding.GetBytes(Convert.ToString(item.cpf))));

                        aplicacaoUsuario.CriarUsuario(usuarioProfessor);
                        itemUsurioProfessor = aplicacaoUsuario.BuscaUsuario(usuarioProfessor);
                    }
                }
                if (itemUsurioProfessor.avatar != "")
                {
                    imgProfessor.Src = "img/pessoas/" + itemUsurioProfessor.avatar + "?" + DateTime.Now;
                }

                else
                {
                    imgProfessor.Src = "img/pessoas/avatarunissex.jpg" + "?" + DateTime.Now;
                }
                //imgFotoOriginal.Src = imgProfessor.Src;

                CarregarDados(Convert.ToInt32(item.id_professor), 1);
            }
            else
            {
                if (grdResultadoOcorrencia.Rows.Count != 0)
                {

                    grdResultadoOcorrencia.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

                if (grdResultadoSolicitacao.Rows.Count != 0)
                {

                    grdResultadoSolicitacao.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        private void CarregarDados(int qIdProfessor, int qTemBanca)
        {

            FinanceiroAplicacao aplicacaoFinanceiro = new FinanceiroAplicacao();
            List<geral_extrato_ocorrencia> listaOcorrencia = new List<geral_extrato_ocorrencia>();
            listaOcorrencia = aplicacaoFinanceiro.ListaExtratoOcorrencia(qIdProfessor, qTemBanca);
            grdResultadoOcorrencia.DataSource = listaOcorrencia;
            grdResultadoOcorrencia.DataBind();

            decimal qTotalOcorrencia = 0;

            if (listaOcorrencia.Count > 0)
            {
                qTotalOcorrencia = listaOcorrencia.Where(x => x.motivo != "Banca").Sum(x => x.valor_atual);
                grdResultadoOcorrencia.UseAccessibleHeader = true;
                grdResultadoOcorrencia.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultadosOcorrencia.Visible = false;
                grdResultadoOcorrencia.Visible = true;
            }
            else
            {
                msgSemResultadosOcorrencia.Visible = true;
            }

            List<geral_extrato_solicitado_pago> listaSolicitacao = new List<geral_extrato_solicitado_pago>();
            listaSolicitacao = aplicacaoFinanceiro.ListaExtratoSolicitadoPago(qIdProfessor);
            grdResultadoSolicitacao.DataSource = listaSolicitacao;//.OrderBy(x=> x.data_solicitacao);
            grdResultadoSolicitacao.DataBind();

            decimal qTotalSolicitacao = 0;

            if (listaSolicitacao.Count > 0)
            {
                qTotalSolicitacao = listaSolicitacao.Where(x => x.status == "Pago").Sum(x => x.valor);

                grdResultadoSolicitacao.UseAccessibleHeader = true;
                grdResultadoSolicitacao.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultadosSolicitacao.Visible = false;
                grdResultadoSolicitacao.Visible = true;
            }
            else
            {
                msgSemResultadosSolicitacao.Visible = true;
            }

            lblTotalOcorrencia.InnerHtml = qTotalOcorrencia.ToString("#,###,###,##0.00");
            lblTotalPagamento.InnerHtml = qTotalSolicitacao.ToString("#,###,###,##0.00");
            lblTotalGeral.InnerHtml = (qTotalOcorrencia - qTotalSolicitacao).ToString("#,###,###,##0.00");
            if ((qTotalOcorrencia - qTotalSolicitacao) > 0)
            {
                lblTotalGeral.Style.Add("color", "darkolivegreen");
            }
            else if ((qTotalOcorrencia - qTotalSolicitacao) > 0)
            {
                lblTotalGeral.Style.Add("color", "darkred");
            }

            divResultados.Visible = true;
        }

        protected void btnOkBanca_Click(object sender, EventArgs e)
        {
            professores item;
            item = (professores)Session["professores"];

            int qTemBanca;
            if (optComBanca.Checked)
            {
                qTemBanca = 1;
            }
            else if (optSemBanca.Checked)
            {
                qTemBanca = 0;
            }
            else
            {
                qTemBanca = 2;
            }
            CarregarDados(Convert.ToInt32(item.id_professor), qTemBanca);
        }

        protected void btnObservacoesExtratoProfessor_Click(object sender, EventArgs e)
        {
            professores item;
            item = (professores)Session["professores"];

            string sAux = "";
            if (txtObservacaoExtratoProfessor.Value.Trim() == "")
            {
                sAux = "Preencher a descrição da Observação. <br/><br/>";
            }
            
            if (sAux != "")
            {
                lblMensagem.Text = sAux;
                lblTituloMensagem.Text = "Atenção";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                return;
            }

            professor_observacoes_plano item_observacao = new professor_observacoes_plano();
            item_observacao.id_professor = item.id_professor;
            item_observacao.observacoes = txtObservacaoExtratoProfessor.Value.Trim();

            FinanceiroAplicacao aplicacaoFinanceiro = new FinanceiroAplicacao();

            item = aplicacaoFinanceiro.Cria_altera_Observacoes_Plano(item_observacao);

            Session["professores"] = item;

            lblMensagem.Text = "Alteração da Observação realizada com sucesso.";
            lblTituloMensagem.Text = "Observação";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("finExtratoProfessor.aspx", true);
        }


    }
}