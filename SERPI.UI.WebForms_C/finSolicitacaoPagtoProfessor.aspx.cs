using Aplicacao_C;
using SERPI.Dominio_C;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class finSolicitacaoPagtoProfessor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 58)) // 5. Solicitação Pagamento Professor - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                //CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                //cursos itemCurso = new cursos();
                //List<cursos> listaCurso = aplicacaoCurso.ListaItem(itemCurso);
                ////var listaPais2 = from item2 in listaPais
                ////                 select new
                ////                 {
                ////                     Id_Pais = item2.Id_Pais,
                ////                     Nacionalidade = item2.Nacionalidade
                ////                 };

                //ddlCodigoCursoProfessor.Items.Clear();
                //ddlCodigoCursoProfessor.DataSource = listaCurso.OrderBy(x => x.sigla);
                //ddlCodigoCursoProfessor.DataValueField = "id_curso";
                //ddlCodigoCursoProfessor.DataTextField = "sigla";
                //ddlCodigoCursoProfessor.DataBind();
                //ddlCodigoCursoProfessor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione uma Sigla do Curso", ""));
                //ddlCodigoCursoProfessor.SelectedValue = "";

                //ddlNomeCursoProfessor.Items.Clear();
                //ddlNomeCursoProfessor.DataSource = listaCurso;
                //ddlNomeCursoProfessor.DataValueField = "id_curso";
                //ddlNomeCursoProfessor.DataTextField = "nome";
                //ddlNomeCursoProfessor.DataBind();
                //ddlNomeCursoProfessor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Curso", ""));
                //ddlNomeCursoProfessor.SelectedValue = "";

                if (Session["aFiltroSolicitacaoPagtoProfessor"] != null)
                {
                    CarregarDados();
                }
            }
            else
            {
                if (grdResultado.Rows.Count != 0)
                {
                    grdResultado.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        private void CarregarDados()
        {
            string[] aFiltroSolicitacaoPagtoProfessor = new string[5];

            professores item = new professores();

            aFiltroSolicitacaoPagtoProfessor = (string[])Session["aFiltroSolicitacaoPagtoProfessor"];

            if (aFiltroSolicitacaoPagtoProfessor[0] != "" && aFiltroSolicitacaoPagtoProfessor[0] != null)
            {
                item.id_professor = Convert.ToInt32(aFiltroSolicitacaoPagtoProfessor[0]);
                txtIdProfessor.Value = aFiltroSolicitacaoPagtoProfessor[0];
            }

            if (aFiltroSolicitacaoPagtoProfessor[1] != "" && aFiltroSolicitacaoPagtoProfessor[1] != null)
            {
                item.nome = aFiltroSolicitacaoPagtoProfessor[1];
                txtNomeProfessor.Value = aFiltroSolicitacaoPagtoProfessor[1];
            }

            if (aFiltroSolicitacaoPagtoProfessor[2] != "" && aFiltroSolicitacaoPagtoProfessor[2] != null)
            {
                item.cpf = aFiltroSolicitacaoPagtoProfessor[2];
            }

            if (aFiltroSolicitacaoPagtoProfessor[3] != "" && aFiltroSolicitacaoPagtoProfessor[3] != null)
            {
                item.status = aFiltroSolicitacaoPagtoProfessor[3];
                optSituacaoProfessorSim.Checked = false;
                optSituacaoProfessorNao.Checked = false;
                optSituacaoProfessorTodos.Checked = false;

                if (aFiltroSolicitacaoPagtoProfessor[3] == "ativado")
                {
                    optSituacaoProfessorSim.Checked = true;
                }
                else if (aFiltroSolicitacaoPagtoProfessor[3] == "inativado")
                {
                    optSituacaoProfessorNao.Checked = true;
                }
                else
                {
                    optSituacaoProfessorTodos.Checked = true;
                }
            }

            if (aFiltroSolicitacaoPagtoProfessor[4] != "" && aFiltroSolicitacaoPagtoProfessor[4] != null)
            {
                //item.status = aFiltroSolicitacaoPagtoProfessor[3];
                optSaldoTodos.Checked = false;
                optSaldoCom.Checked = false;
                optSaldoSem.Checked = false;
                optSaldoSolicitacao.Checked = false;

                if (aFiltroSolicitacaoPagtoProfessor[4] == "comsaldo")
                {
                    optSaldoCom.Checked = true;
                }
                else if (aFiltroSolicitacaoPagtoProfessor[4] == "semsaldo")
                {
                    optSaldoSem.Checked = true;
                }
                else if (aFiltroSolicitacaoPagtoProfessor[4] == "comsolicitacao")
                {
                    optSaldoSolicitacao.Checked = true;
                }
                else
                {
                    optSaldoTodos.Checked = true;
                }
            }

            //Session["aFiltroSolicitacaoPagtoProfessor"] = aFiltroSolicitacaoPagtoProfessor;
            FinanceiroAplicacao aplicacaoFinanceiro = new FinanceiroAplicacao();
            List<geral_extrato_professor> lista = new List<geral_extrato_professor>();
            lista = aplicacaoFinanceiro.ListaProfessores(item);

            if (optSaldoCom.Checked)
            {
                grdResultado.DataSource = lista.Where(x=> x.saldo_a_solicitar > 0).ToList();
            }
            else if (optSaldoSem.Checked)
            {
                grdResultado.DataSource = lista.Where(x => x.saldo_a_solicitar == 0).ToList();
            }
            else if (optSaldoSolicitacao.Checked)
            {
                grdResultado.DataSource = lista.Where(x => x.solicitado > 0).ToList();
            }
            else
            {
                grdResultado.DataSource = lista;
            }

            grdResultado.DataBind();

            if (lista.Count > 0)
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
            divResultados.Visible = true;
        }

        protected void btnCalcularMes_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];
            FinanceiroAplicacao aplicacaoFinanceiro = new FinanceiroAplicacao();

            string sBotao = HttpContext.Current.Request["hBotaoCalculo"];
            int IdProfessor = 0;
            DateTime dDataInicio = new DateTime();
            DateTime dDataFim = new DateTime();

            if (sBotao == "mes")
            {
                IdProfessor = Convert.ToInt32(HttpContext.Current.Request["hIdProfessor"]);
                dDataInicio = Convert.ToDateTime("01/" + HttpContext.Current.Request["hMesInicio"]);
                dDataFim = Convert.ToDateTime("01/" + HttpContext.Current.Request["hMesInicio"]);
            }
            else if (sBotao == "periodo")
            {
                IdProfessor = Convert.ToInt32(HttpContext.Current.Request["hIdProfessor"]);
                dDataInicio = Convert.ToDateTime("01/" + HttpContext.Current.Request["hMesInicio"]);
                dDataFim = Convert.ToDateTime("01/" + HttpContext.Current.Request["hMesFim"]);
            }
            else
            {
                IdProfessor = Convert.ToInt32(HttpContext.Current.Request["hIdProfessor"]);
                dDataInicio = Convert.ToDateTime("01/2010");
                dDataFim = DateTime.Today;
            }

            aplicacaoFinanceiro.RecalcularHorasAulas(IdProfessor, dDataInicio, dDataFim, usuario.usuario);
            aplicacaoFinanceiro.RecalcularOrientacoes(IdProfessor, dDataInicio, dDataFim, usuario.usuario);
            aplicacaoFinanceiro.RecalcularBancas(IdProfessor, dDataInicio, dDataFim, usuario.usuario);
            aplicacaoFinanceiro.RecalcularCoordenacao(IdProfessor, dDataInicio, dDataFim, usuario.usuario);

            CarregarDados();

            lblTituloMensagem.Text = "Recálculo Realizado";
            lblMensagem.Text = "Recálculo realizado com sucesso. <br><br><strong><em>Verifique no botão 'Solicitar Pagamento' se há pagamentos para serem solicitados.</em></strong>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "$('#divCabecalho').addClass('alert-success'); $('#divMensagemModal').modal();", true);

        }

        protected void btnSalvarSolicitacaoPagto_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];
            FinanceiroAplicacao aplicacaoFinanceiro = new FinanceiroAplicacao();

            DateTime dData = Convert.ToDateTime(txtDataSolicitacao.Value);
            decimal qValorTotal = Convert.ToDecimal(HttpContext.Current.Request["hValorSolicitar"]);
            string qPlanoValor = HttpContext.Current.Request["hIdPlano"];


            aplicacaoFinanceiro.AdicionaSolicitacaoPagto(dData, qValorTotal, qPlanoValor, usuario.usuario);

            CarregarDados();

            lblTituloMensagem.Text = "Valor Solicitado Realizado";
            lblMensagem.Text = "Valor Solicitado realizado com sucesso.";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "$('#divCabecalho').addClass('alert-success'); $('#divMensagemModal').modal();", true);

        }

        protected void btnSalvarRecebimentoNF_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];
            FinanceiroAplicacao aplicacaoFinanceiro = new FinanceiroAplicacao();

            string sAux = "";

            if (txtNotaFiscal.Value.Trim() == "")
            {
                sAux = "Deve-se digitar o número da Nota Fiscal. <br><br>";
            }

            if (txtDataRecebimentoNF.Value.Trim()=="")
            {
                sAux = sAux + "Deve-se digitar a Data de Recebimento. <br><br>";
            }

            if (txtDataPagtoNF.Value.Trim()=="")
            {
                sAux = sAux + "Deve-se digitar a Data de Pagamento. <br><br>";
            }

            if (sAux != "")
            {
                lblTituloMensagem.Text = "Atenção";
                lblMensagem.Text = sAux;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "$('#divCabecalho').addClass('alert-warning'); $('#divMensagemModal').modal();", true);
                return;
            }
            professor_solicitacao_pagamento item;

            string sIdsolicitacao = HttpContext.Current.Request["hIdSolicitacao"];

            var arrayIdsolicitacao = sIdsolicitacao.Split(';');

            sAux = "";
            string sNomeProfessor = "";

            foreach (var elemento in arrayIdsolicitacao)
            {
                item = new professor_solicitacao_pagamento();
                item.id_solicitacao = Convert.ToInt32(elemento);
                item.nota_fiscal = txtNotaFiscal.Value.Trim();
                item.status = "Pago";
                //item.data_recebimento = DateTime.Today;
                item.data_recebimento = Convert.ToDateTime(txtDataRecebimentoNF.Value);
                item.data_pagamento = Convert.ToDateTime(txtDataPagtoNF.Value);
                item.data_alteracao = DateTime.Now;
                item.usuario = usuario.usuario;

                item = aplicacaoFinanceiro.AlteraSolicitacao(item);

                foreach (var elemento2 in item.professor_solicitacao_plano)
                {
                    sAux = sAux + "Data soliticação: <b>" + item.data_solicitacao.ToString("dd/MM/yyyy") + "</b><br />";
                    sAux = sAux + "Mês/Ano: <b>" + elemento2.professor_plano.mes.ToString("MM/yyyy") + "</b><br />";
                    sAux = sAux + "Grupo: <b>" + elemento2.professor_plano.motivo + "</b><br />";
                    sAux = sAux + "Valor do Grupo: <b>" + elemento2.valor_solicitado.ToString("#,###,###,##0.00") + "</b><br />---------------<br />";
                    sNomeProfessor = elemento2.professor_plano.professores.nome;
                }
                sAux = sAux + "Valor Solicitado: <b>" + item.valor.ToString("#,###,###,##0.00") + "</b><br /><br /><br />";


            }



            CarregarDados();

            GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
            Configuracoes itemEmail;
            // 1 = email mestrado@ipt.br
            // 2 = email suporte@ipt.br
            itemEmail = aplicacaoGerais.BuscaConfiguracoes(2);

            string qDe = itemEmail.remetente_email;
            string qDe_Nome = itemEmail.nome_remetente_email;
            string qPara = "";
            string qCopia = "suporte@ipt.br";
            string qCopiaOculta = "";
            string qAssunto = "Recebimento de NF n.º: " + txtNotaFiscal.Value.Trim() + " – " + sNomeProfessor;
            string qCorpo = "";
            qCorpo = qCorpo + "Atenção.<br / > Informação de cadastro de recebimento de NF<br / ><br / >";
            qCorpo = qCorpo + "Recebimento da NF n.º: <b>" + txtNotaFiscal.Value.Trim() + "</b><br / >";
            qCorpo = qCorpo + "Data do Recebimento da NF: <b>" + Convert.ToDateTime(txtDataRecebimentoNF.Value).ToString("dd/MM/yyyy") + "</b><br / >";
            qCorpo = qCorpo + "Data do Pagamento da NF: <b>" + Convert.ToDateTime(txtDataPagtoNF.Value).ToString("dd/MM/yyyy") + "</b><br / >";
            qCorpo = qCorpo + "Total do(s) Valor(es) da NF: <b>" + txtTotalSolicitado.Value.Trim() + "</b><br / ><br / >";
            

            qCorpo = qCorpo + "Referente à(s) seguinte(s) solicitação(ões): <br / >";
            qCorpo = qCorpo + sAux;

            qCorpo = qCorpo + "<br / >Data/hora do cadastro: <b>" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "</b><br / >";
            qCorpo = qCorpo + "Usuário: <b>" + usuario.usuario + "</b><br / ><br / >";
            qCorpo = qCorpo + "<span style=\"color:#535353;font-size:11px\">E-mail enviado automaticamente pelo sistema SAPIENS (não responder). </span>";

            if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString == "Producao")
            {
                qPara = usuario.email;
                qCopia = "";
            }
            else
            {
                qPara = "kelsey@ipt.br"; // usuario.email;
                qCorpo = qCorpo + "<br><br> <strong>Esse email seria enviado para:</strong>" + usuario.email;
            }

            if (Utilizades.fEnviaEmail(qDe, qDe_Nome, qPara, qCopia, qCopiaOculta, qAssunto, qCorpo, itemEmail.servidor_email, itemEmail.conta_email, itemEmail.senha_email, itemEmail.porta_email.Value, 1, ""))
            {
                
            }


            lblTituloMensagem.Text = "Recebimento de Nota Fiscal";
            lblMensagem.Text = "Recebimento de Nota Fiscal realizado com sucesso.<br /> <br /> <strong>Atenção.</strong> <br /><strong>Um e-mail com as informações desse cadastro foram enviadas para o seu email.<br /> Por favor, verifique.</strong>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "$('#divCabecalho').addClass('alert-success'); $('#divMensagemModal').modal();", true);

        }

        protected void btnExcluirSolicitacao_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];
            FinanceiroAplicacao aplicacaoFinanceiro = new FinanceiroAplicacao();

            int iIdsolicitacao = Convert.ToInt32(txtIdSolicitacao.Value);

            aplicacaoFinanceiro.ExcluirSolicitacao(iIdsolicitacao);

            CarregarDados();

            lblTituloMensagem.Text = "Exclusão de Solicitação";
            lblMensagem.Text = "Exclusão de Solicitação realizada com sucesso.";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "$('#divCabecalho').addClass('alert-success'); $('#divMensagemModal').modal();", true);

        }

        protected void btnPerquisaProfessor_Click(object sender, EventArgs e)
        {
            string[] aFiltroSolicitacaoPagtoProfessor = new string[5];

            if (txtIdProfessor.Value.Trim() != "")
            {
                aFiltroSolicitacaoPagtoProfessor[0] = txtIdProfessor.Value.Trim();
            }

            if (txtNomeProfessor.Value.Trim() != "")
            {
                aFiltroSolicitacaoPagtoProfessor[1] = txtNomeProfessor.Value.Trim();
            }

            if (txtCPFProfessor.Value.Trim() != "")
            {
                aFiltroSolicitacaoPagtoProfessor[2] = txtCPFProfessor.Value.Trim();
            }

            if (optSituacaoProfessorSim.Checked)
            {
                aFiltroSolicitacaoPagtoProfessor[3] = "ativado";
            }
            else if (optSituacaoProfessorNao.Checked)
            {
                aFiltroSolicitacaoPagtoProfessor[3] = "inativado";
            }
            else
            {
                aFiltroSolicitacaoPagtoProfessor[3] = "todos";
            }

            if (optSaldoCom.Checked)
            {
                aFiltroSolicitacaoPagtoProfessor[4] = "comsaldo";
            }
            else if (optSaldoSem.Checked)
            {
                aFiltroSolicitacaoPagtoProfessor[4] = "semsaldo";
            }
            else if (optSaldoSolicitacao.Checked)
            {
                aFiltroSolicitacaoPagtoProfessor[4] = "comsolicitacao";
            }
            else
            {
                aFiltroSolicitacaoPagtoProfessor[4] = "todos";
            }

            Session["aFiltroSolicitacaoPagtoProfessor"] = aFiltroSolicitacaoPagtoProfessor;

            CarregarDados();
        }

        //protected void grdResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Page")
        //    {
        //        return;
        //    }

        //    int linha = Convert.ToInt32(e.CommandArgument);
        //    int codigo = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0]);
        //    Professors_concentracao item = new Professors_concentracao();
        //    item.id_Professor_concentracao = codigo;
        //    switch (grdResultado.DataKeys[linha].Values[1].ToString())
        //    {
        //        case "Editar":
        //            ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
        //            item = aplicacaoProfessor.BuscaItem(item);
        //            Session.Add("Professors_concentracao", item);
        //            Session.Add("sNewProfessor", false);
        //            Response.Redirect("cadProfessorConcentracaoGestao.aspx", true);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //public void grdResultado_Command(object sender, CommandEventArgs e)
        //{
        //    int index = Convert.ToInt32(e.CommandArgument);
        //    if (e.CommandName == "StartService")
        //    {
        //        int linha = Convert.ToInt32(e.CommandArgument);
        //        int codigo = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0]);
        //        professores item = new professores();
        //        ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
        //        item.id_professor = codigo;
        //        item = aplicacaoProfessor.BuscaItem(item);
        //        Session["professores"] = item;
        //        Response.Redirect("finExtratoProfessorDetalhe.aspx", true);
        //    }
        //}

        public string setLinkImagem(string cpf, string Nome)
        {
            UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
            usuarios item = new usuarios();
            item.usuario = cpf;
            item = aplicacaoUsuario.BuscaUsuario(item);
            string sAux;

            if (item == null)
            {
                sAux = "<div title=\"Sem foto\"> <a class=\"fa fa-ban\" href=\'#\'; style= color:#428bca;></a></div>";
            }
            else
            {
                if ((item.avatar == null || (item.avatar == "")))
                {
                    sAux = "<div title=\"Sem foto\"> <a class=\"fa fa-ban\" href=\'#\'; style= color:#428bca;></a></div>";
                }
                else
                {
                    sAux = ("<div title=\"Ver foto\"> <a href=\'javascript:fExibeImagem(\"" + (item.avatar + ("\",\"" + (Nome + "\")\' >"))));
                    sAux = (sAux + ("<img class=\"img-circle\" id=\"imageresource\" src=\"" + ("img\\pessoas\\" + (item.avatar + ("?" + (DateTime.Now.ToString() + "\" style=\"width: 35px; height: 35px;\">"))))));
                    sAux = (sAux + "</a></div>");
                }
            }

            return sAux;
        }

        public string setSaldo(decimal plano, decimal pagamento)
        {
            //return string.Format("{0:C}", plano - pagamento);
            return string.Format("{0:C}", plano);
        }

        public string setRecalcular(int idProfessor, string nome)
        {
            string sAux;

            sAux = "<div title=\"Recalcular Plano\"> <a class=\"btn btn-primary btn-circle fa fa-refresh\" href=\'javascript:fExibeRecalcular(\""
                                            + idProfessor.ToString() + "\",\""
                                            + nome + "\")\'; ></a></div>";

            return sAux;
        }

        public string setExtrato(int idProfessor, string nome)
        {
            string sAux;

            sAux = "<div title=\"Visualizar Extrato\"> <a class=\"btn btn-warning btn-circle fa fa-search\" href=\'javascript:fExibeExtrato(\""
                                            + idProfessor.ToString() + "\",\""
                                            + nome + "\")\'; ></a></div>";

            return sAux;
        }

        public string setPagamento(int idProfessor, string nome)
        {
            string sAux;

            sAux = "<div title=\"Solicitar Pagamento\"> <a class=\"btn btn-success btn-circle fa fa-money\" href=\'javascript:fExibePagamento(\""
                                            + idProfessor.ToString() + "\",\""
                                            + nome + "\")\'; ></a></div>";

            return sAux;
        }

        public string setEmail(int idProfessor, string nome, decimal solicitado)
        {
            string sAux = "";

            if (solicitado > 0)
            {
                sAux = "<div title=\"Preparar Email\"> <a class=\"btn btn-danger btn-circle fa fa-envelope-o\" href=\'javascript:fExibeEmail(\""
                                            + idProfessor.ToString() + "\",\""
                                            + nome + "\")\'; ></a></div>";
            }
            return sAux;
        }

        public string setNotaFiscal(int idProfessor, string nome, decimal solicitado)
        {
            string sAux = "";

            if (solicitado > 0)
            {
                sAux = "<div title=\"Recebimento de Nota Fiscal\"> <a class=\"btn btn-purple btn-circle fa fa-edit\" href=\'javascript:fExibeNotaFiscal(\""
                                            + idProfessor.ToString() + "\",\""
                                            + nome + "\")\'; ></a></div>";
            }

            return sAux;
        }

        public string setVisualizarSolicicacao(int idProfessor, string nome)
        {
            string sAux;

            sAux = "<div title=\"Visualizar Solicitação de Pagamento\"> <a class=\"btn btn-warning btn-circle fa fa-search-plus\" href=\'javascript:fExibeVisualizarSolicicacao(\""
                                            + idProfessor.ToString() + "\",\""
                                            + nome + "\")\'; ></a></div>";

            return sAux;
        }

        protected void btnEnviarEmail_Click(object sender, EventArgs e)
        {
            try
            {
                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                Configuracoes item;
                // 1 = email mestrado@ipt.br
                // 2 = email suporte@ipt.br
                item = aplicacaoGerais.BuscaConfiguracoes(2);

                string qDe = item.remetente_email;
                string qDe_Nome = item.nome_remetente_email;
                string qPara = txtParaEmail.Value.Trim();
                string qCopia = txtCcEmail.Value;
                string qCopiaOculta ="";
                string qAssunto = txtAssuntoEmail.Value;
                string qCorpo = HttpContext.Current.Request["hTextoEmail"];
                qCorpo = qCorpo.Replace("{data_limite}", Convert.ToDateTime(txtDataLimite.Value).ToString("dd/MM/yyyy"));

                if (ConfigurationManager.ConnectionStrings["qServidor"].ConnectionString != "Producao")
                {
                    qCorpo = qCorpo + "<br><br> <strong>Esse email seria enviado para:</strong>" + qPara + "<br><br> <strong>Com cópia para:</strong>" + qCopia;
                    qPara = "kelsey@ipt.br"; // usuario.email;
                    qCopia = "";
                }
                
                if (Utilizades.fEnviaEmail(qDe, qDe_Nome, qPara, qCopia, qCopiaOculta, qAssunto, qCorpo, item.servidor_email, item.conta_email, item.senha_email, item.porta_email.Value, 1, ""))
                {
                    lblTituloMensagem.Text = "Envio de Email";
                    lblMensagem.Text = "Email enviado com sucesso";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
                }
                else
                {
                    lblTituloMensagem.Text = "Envio de Email - ERRO";
                    lblMensagem.Text = "Houve um erro no envio do Email";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                }
            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Envio de Email";
                lblMensagem.Text = "Houve um erro no envio do Email <br><br>" + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
            }

        }
    }
}