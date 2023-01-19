using Aplicacao_C;
using SERPI.Dominio_C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class cadPeriodoGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 14)) // 6. Quadrimestres - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
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
                //ddlTipoCurso.SelectedValue = "1"; //Travado no Mestrado por enquanto
                //ddlTipoCurso.Enabled = false;

                if (Session["sNewQuadrimestre"] != null && (Boolean)Session["sNewQuadrimestre"] != true)
                {
                    quadrimestres item;
                    item = (quadrimestres)Session["quadrimestres"];
                    lblTituloPagina.Text = "(Editar) - N.º " + item.quadrimestre;

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

                    if (item.id_tipo_curso == null)
                    {
                        ddlTipoCurso.SelectedValue = "1";
                    }
                    else
                    {
                        ddlTipoCurso.SelectedValue = item.id_tipo_curso.ToString();
                    }
                    ddlTipoCurso.Attributes.Add("disabled", "disabled");
                    txtAnoPeriodo.Attributes.Add("disabled", "disabled");
                    txtAnoPeriodo.Value = item.ano;
                    txtNumeroPeriodo.Value = item.numero.ToString();

                    txtDataInicioPeriodo.Value = String.Format("{0:yyyy-MM-dd}", item.data_inicio);
                    txtDataFimPeriodo.Value = String.Format("{0:yyyy-MM-dd}", item.data_fim);

                    btnCriarPeriodo.Disabled = false;
                    divEdicao.Visible = true;

                    if (Session["AdiciondoSucesso"] != null)
                    {
                        if ((Boolean)Session["AdiciondoSucesso"])
                        {
                            Session["AdiciondoSucesso"] = null;
                            lblMensagem.Text = "Novo Período adicionado com sucesso";
                            lblTituloMensagem.Text = "Novo Período";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
                        }

                    }

                }
                else
                {
                    lblInativado.Style["display"] = "none";
                    btnAtivar.Style["display"] = "none";
                    btnInativar.Style["display"] = "none";

                    lblTituloPagina.Text = "(novo)";
                    txtDataCadastro.Value = "";
                    txtDataAlteracao.Value = "";
                    txtStatus.Value = "";
                    txtResponsavel.Value = "";

                    ddlTipoCurso.Attributes.Remove("disabled");
                    txtAnoPeriodo.Attributes.Remove("disabled");
                    ddlTipoCurso.SelectedValue = "";
                    txtAnoPeriodo.Value = "";
                    txtNumeroPeriodo.Value = "";

                    txtDataInicioPeriodo.Value = "";
                    txtDataFimPeriodo.Value = "";

                    btnCriarPeriodo.Disabled = true;
                    divEdicao.Visible = false;
                }
            }

        }

        protected void btnCriarPeriodo_Click(object sender, EventArgs e)
        {
            Session["sNewQuadrimestre"] = true;
            Session["quadrimestres"] = null;
            Session["AdiciondoSucesso"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("cadPeriodo.aspx", true);
        }

        protected void btnSalvar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";
                //if (ddlTipoCurso.SelectedValue == "" && (Session["sNewQuadrimestre"] == null || (Boolean)Session["sNewQuadrimestre"] == true))
                //{
                //    sAux = "Selecionar um Tipo do Curso. <br/><br/>";
                //}
                if (txtAnoPeriodo.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o Ano do Período. <br/><br/>";
                }

                else if (Convert.ToInt32(txtAnoPeriodo.Value.Trim()) < 1900 || Convert.ToInt32(txtAnoPeriodo.Value.Trim()) > 5000)
                {
                    sAux = sAux + "Digite um ano válido. <br/><br/>";
                }

                DateTime temp;
                if (!DateTime.TryParse(txtDataInicioPeriodo.Value.Trim(), out temp))
                {
                    sAux = sAux + "Digite uma Data Início válida. <br/><br/>";
                }
                if (!DateTime.TryParse(txtDataFimPeriodo.Value.Trim(), out temp))
                {
                    sAux = sAux + "Digite uma Data Fim válida. <br/><br/>";
                }

                if (sAux != "")
                {
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                    return;
                }

                if (Session["sNewQuadrimestre"] != null && (Boolean)Session["sNewQuadrimestre"] != true)
                {
                    //Alteração de registro
                    QuadrimestreAplicacao aplicacaoPeriodo = new QuadrimestreAplicacao();
                    quadrimestres item = new quadrimestres();

                    item = (quadrimestres)Session["quadrimestres"];

                    if (txtDataInicioPeriodo.Value != "")
                    {
                        item.data_inicio = Convert.ToDateTime(txtDataInicioPeriodo.Value);
                    }

                    if (txtDataFimPeriodo.Value != "")
                    {
                        item.data_fim = Convert.ToDateTime(txtDataFimPeriodo.Value);
                    }

                    item.status = "alterado";
                    //item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;


                    aplicacaoPeriodo.AlterarItem(item);

                    item = aplicacaoPeriodo.BuscaItem(item);

                    txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy}", item.data_alteracao);
                    txtStatus.Value = item.status;
                    txtResponsavel.Value = item.usuario;

                    lblMensagem.Text = "Período alterado com sucesso.";
                    lblTituloMensagem.Text = "Alteração de Período";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

                    Session["quadrimestres"] = item;

                }
                else
                {
                    //Inclusão de registro
                    QuadrimestreAplicacao aplicacaoPeriodo = new QuadrimestreAplicacao();

                    quadrimestres item = new quadrimestres();

                    item.id_tipo_curso = Convert.ToInt32(ddlTipoCurso.SelectedValue);// item.id_tipo_curso = 1; //temporariamente inabilitado Convert.ToInt32(ddlTipoCurso.SelectedValue);
                    item.ano = txtAnoPeriodo.Value.Trim();
                    item.numero = aplicacaoPeriodo.BuscaItem_NumeroMaximo(item) + 1;
                    if (item.id_tipo_curso == 1 && item.numero == 4)
                    {
                        lblMensagem.Text = "Para curso do tipo <strong>\"MESTRADO\"</strong> não é permitido a crição de mais que 3 períodos que representam 3 quadrimestres.";
                        lblTituloMensagem.Text = "Período Tipo Mestrado";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                        return;
                    }

                    if (item.id_tipo_curso == 2) //MBA
                    {
                        //item.quadrimestre = "M" + item.ano + "-" + item.numero.ToString();
                        item.quadrimestre = "M" + item.ano + item.numero.ToString();
                    }
                    else if (item.id_tipo_curso == 3) //Especialização
                    {
                        //item.quadrimestre = "E" + item.ano + "-" + item.numero.ToString();
                        item.quadrimestre = "E" + item.ano + item.numero.ToString();
                    }
                    else if (item.id_tipo_curso == 4) //Curta Duração
                    {
                        //item.quadrimestre = "C" + item.ano + "-" + item.numero.ToString();
                        item.quadrimestre = "C" + item.ano + item.numero.ToString();
                    }
                    else if (item.id_tipo_curso == 5) //In Company
                    {
                        //item.quadrimestre = "I" + item.ano + "-" + item.numero.ToString();
                        item.quadrimestre = "I" + item.ano + item.numero.ToString();
                    }
                    else if (item.id_tipo_curso == 6) //Lato Sensu
                    {
                        //item.quadrimestre = "L" + item.ano + "-" + item.numero.ToString();
                        item.quadrimestre = "L" + item.ano + item.numero.ToString();
                    }
                    else //Mestrado
                    {
                        item.quadrimestre = item.ano + "-" + item.numero.ToString();
                    }

                    if (txtDataInicioPeriodo.Value != "")
                    {
                        item.data_inicio = Convert.ToDateTime(txtDataInicioPeriodo.Value);
                    }

                    if (txtDataFimPeriodo.Value != "")
                    {
                        item.data_fim = Convert.ToDateTime(txtDataFimPeriodo.Value);
                    }

                    item.status = "cadastrado";
                    item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;


                    item = aplicacaoPeriodo.CriarItem(item);

                    if (item != null)
                    {
                        Session["quadrimestres"] = item;
                        Session.Add("sNewQuadrimestre", false);
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