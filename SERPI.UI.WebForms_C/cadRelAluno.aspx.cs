using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections;

namespace SERPI.UI.WebForms_C
{
    public partial class cadRelAluno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null)
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 27)) //17. Relatório de Alunos
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                List<tipos_curso> listaTipoCurso = aplicacaoGerais.ListaTipoCurso();

                ddlTipoCursoAluno.Items.Clear();
                ddlTipoCursoAluno.DataSource = listaTipoCurso.OrderBy(x => x.id_tipo_curso);
                ddlTipoCursoAluno.DataValueField = "id_tipo_curso";
                ddlTipoCursoAluno.DataTextField = "tipo_curso";
                ddlTipoCursoAluno.DataBind();
                ddlTipoCursoAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos Tipos de Curso", ""));
                ddlTipoCursoAluno.SelectedValue = "";
                ddlTipoCursoAluno_SelectedIndexChanged(null, null);

                Configuracoes item_configuracoes;
                // 1 = email mestrado@ipt.br
                // 2 = email suporte@ipt.br
                item_configuracoes = aplicacaoGerais.BuscaConfiguracoes(1);

                txtCcEmailHidden.Value = item_configuracoes.copia_email;
                txtDeEmail.Value = item_configuracoes.remetente_email;
            }
            else
            {
                if (grdAluno.Rows.Count != 0)
                {
                    grdAluno.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        protected void grdAluno_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            grdAluno.PageIndex = e.NewPageIndex;
            grdAluno.SelectedIndex = -1;
        }

        private void CarregarDados()
        {
            alunos item = new alunos();

            int pIdTipoCurso = 0;
            int pIdCurso = 0;
            string qTurma = "";
            int pIdOfereciemnto = 0;
            string qArea = "";
            string qSituacao = "";
            string qTipoMatricula = "";

            if (txtMatriculaAluno.Value.Trim() != "")
            {
                item.idaluno = System.Convert.ToDecimal(txtMatriculaAluno.Value.Trim());
            }

            if (txtNomeAluno.Value.Trim() != "")
            {
                item.nome = txtNomeAluno.Value.Trim();
            }

            if (txtCPFAluno.Value.Trim() != "")
            {
                item.cpf = txtCPFAluno.Value.Trim();
            }

            if (txtRGAluno.Value.Trim() != "")
            {
                item.numero_documento = txtRGAluno.Value.Trim();
            }

            if (txtEmailAluno.Value .Trim()!= "")
            {
                item.email = txtEmailAluno.Value.Trim();
            }

            if (txtEnderecoAluno.Value.Trim() != "")
            {
                item.logradouro_res = txtEnderecoAluno.Value.Trim();
            }

            if (ddlTipoCursoAluno.SelectedValue != "")
            {
                pIdTipoCurso = Convert.ToInt32(ddlTipoCursoAluno.SelectedValue);
            }

            if (ddlNomeCursoAluno.SelectedValue != "")
            {
                pIdCurso = Convert.ToInt32(ddlNomeCursoAluno.SelectedValue);
            }

            if (ddlTurmaAluno.SelectedValue != "")
            {
                qTurma = ddlTurmaAluno.SelectedValue;
            }

            if (ddlOferecimentoAluno.SelectedValue != "")
            {
                pIdOfereciemnto = Convert.ToInt32(ddlOferecimentoAluno.SelectedValue);
            }

            if (txtFormacaoAluno.Value.Trim() != "")
            {
                item.formacao = txtFormacaoAluno.Value.Trim();
            }

            if (txtAnoFormacao.Value.Trim() != "")
            {
                item.ano_graduacao = Convert.ToInt32(txtAnoFormacao.Value.Trim());
            }

            if (txtEmpresaAluno.Value.Trim() != "")
            {
                item.empresa = txtEmpresaAluno.Value.Trim();
            }

            if (txtCargoAluno.Value.Trim() != "")
            {
                item.cargo = txtCargoAluno.Value.Trim();
            }

            if (txtAreaConcentracaoAluno.Value.Trim() != "")
            {
                qArea = txtAreaConcentracaoAluno.Value.Trim();
            }

            if (ddlSituacaoAluno.SelectedValue != "")
            {
                qSituacao = ddlSituacaoAluno.SelectedValue;
            }

            if (ddlTipoMatriculaAluno.SelectedValue != "")
            {
                qTipoMatricula = ddlTipoMatriculaAluno.SelectedValue;
            }

            if (optProficienciaInglesSim.Checked)
            {
                item.RefazerProficienciaIngles = 1;
            }
            else if (optProficienciaInglesNao.Checked)
            {
                item.RefazerProficienciaIngles = 0;
            }

            if (optProficienciaPortuguesSim.Checked)
            {
                item.RefazerProvaPortugues = 1;
            }
            else if (optProficienciaPortuguesNao.Checked)
            {
                item.RefazerProvaPortugues = 0;
            }

            if (txtPalavraChave.Value.Trim() != "")
            {
                item.palavra_chave = txtPalavraChave.Value.Trim();
            }

            //Session["arrayFiltroAluno"] = arrayFiltroAluno;
            AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
            List<alunos> listaAluno = new List<alunos>();
            listaAluno = aplicacaoAluno.ListaItemRelaroio(item,pIdTipoCurso,pIdCurso, qTurma, pIdOfereciemnto, qArea, qSituacao, qTipoMatricula);
            grdAluno.DataSource = listaAluno;
            grdAluno.DataBind();

            if (listaAluno.Count > 0)
            {
                grdAluno.UseAccessibleHeader = true;
                grdAluno.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultados.Visible = false;
                grdAluno.Visible = true;
                divBotaoPreparaEmail.Visible = true;
            }
            else
            {
                msgSemResultados.Visible = true;
                divBotaoPreparaEmail.Visible = false;
            }
            divResultados.Visible = true;
        }

        protected void bntPerquisaAluno_Click(object sender, EventArgs e)
        {
            CarregarDados();
        }

        protected void btnEnviarEmail_Click(object sender, EventArgs e)
        {
            try
            {
                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();

                Configuracoes item;
                // 1 = email mestrado@ipt.br
                // 2 = email suporte@ipt.br
                item = aplicacaoGerais.BuscaConfiguracoes(1);

                string qDe = item.remetente_email;
                string qDe_Nome = item.nome_remetente_email;
                string qPara = "mestrado@ipt.br";
                string qCopia = txtCcEmail.Value;
                string qCopiaOculta = txtParaEmail.Value;
                string qAssunto = txtAssuntoEmail.Value;
                string qCorpo = HttpContext.Current.Request["hCodigoAluno"];
                if (Utilizades.fEnviaEmail(qDe, qDe_Nome, qPara, qCopia, qCopiaOculta, qAssunto, qCorpo, item.servidor_email, item.conta_email, item.senha_email, item.porta_email.Value, 1, ""))
                {
                    lblTituloMensagem.Text = "Envio de Email";
                    lblMensagem.Text = "Email enviado com sucesso";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
                }
                else
                {
                    lblTituloMensagem.Text = "Envio de Email";
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

        protected void grdAluno_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            if (grdAluno.Rows.Count > 0)
            {
                if (!chkCpfAluno.Checked)
                {
                    grdAluno.HeaderRow.Cells[3].CssClass = "hidden notexport";
                    grdAluno.Columns[3].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdAluno.HeaderRow.Cells[3].CssClass = "centralizarTH";
                    grdAluno.Columns[3].ItemStyle.CssClass = "centralizarTH";
                }
                if (!chkRgAluno.Checked)
                {
                    grdAluno.HeaderRow.Cells[4].CssClass = "hidden notexport";
                    grdAluno.Columns[4].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdAluno.HeaderRow.Cells[4].CssClass = "centralizarTH";
                    grdAluno.Columns[4].ItemStyle.CssClass = "centralizarTH";
                }
                if (!chkTelefoneAluno.Checked)
                {
                    grdAluno.HeaderRow.Cells[5].CssClass = "hidden notexport";
                    grdAluno.Columns[5].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdAluno.HeaderRow.Cells[5].CssClass = "centralizarTH";
                    grdAluno.Columns[5].ItemStyle.CssClass = "centralizarTH";
                }
                if (!chkCelularAluno.Checked)
                {
                    grdAluno.HeaderRow.Cells[6].CssClass = "hidden notexport";
                    grdAluno.Columns[6].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdAluno.HeaderRow.Cells[6].CssClass = "centralizarTH";
                    grdAluno.Columns[6].ItemStyle.CssClass = "centralizarTH";
                }
                if (!chkEmailAluno.Checked)
                {
                    grdAluno.HeaderRow.Cells[7].CssClass = "hidden notexport";
                    grdAluno.Columns[7].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdAluno.HeaderRow.Cells[7].CssClass = "centralizarTH";
                    grdAluno.Columns[7].ItemStyle.CssClass = "centralizarTH";
                }
                if (!chkEnderecoAluno.Checked)
                {
                    grdAluno.HeaderRow.Cells[8].CssClass = "hidden notexport";
                    grdAluno.Columns[8].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdAluno.HeaderRow.Cells[8].CssClass = "centralizarTH";
                    grdAluno.Columns[8].ItemStyle.CssClass = "centralizarTH";
                }
                if (!chkTipoCursoAluno.Checked)
                {
                    grdAluno.HeaderRow.Cells[9].CssClass = "hidden notexport";
                    grdAluno.Columns[9].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdAluno.HeaderRow.Cells[9].CssClass = "centralizarTH";
                    grdAluno.Columns[9].ItemStyle.CssClass = "centralizarTH";
                }
                if (!chkCursoAluno.Checked)
                {
                    grdAluno.HeaderRow.Cells[10].CssClass = "hidden notexport";
                    grdAluno.Columns[10].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdAluno.HeaderRow.Cells[10].CssClass = "centralizarTH";
                    grdAluno.Columns[10].ItemStyle.CssClass = "centralizarTH";
                }
                if (!chkTurmaAluno.Checked)
                {
                    grdAluno.HeaderRow.Cells[11].CssClass = "hidden notexport";
                    grdAluno.Columns[11].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdAluno.HeaderRow.Cells[11].CssClass = "centralizarTH";
                    grdAluno.Columns[11].ItemStyle.CssClass = "centralizarTH";
                }
                if (!chkOferecimentoAluno.Checked)
                {
                    grdAluno.HeaderRow.Cells[12].CssClass = "hidden notexport";
                    grdAluno.Columns[12].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdAluno.HeaderRow.Cells[12].CssClass = "centralizarTH";
                    grdAluno.Columns[12].ItemStyle.CssClass = "centralizarTH";
                }
                if (!chkFormacaoAluno.Checked)
                {
                    grdAluno.HeaderRow.Cells[13].CssClass = "hidden notexport";
                    grdAluno.Columns[13].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdAluno.HeaderRow.Cells[13].CssClass = "centralizarTH";
                    grdAluno.Columns[13].ItemStyle.CssClass = "centralizarTH";
                }
                if (!chkAnoFormacaoAluno.Checked)
                {
                    grdAluno.HeaderRow.Cells[14].CssClass = "hidden notexport";
                    grdAluno.Columns[14].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdAluno.HeaderRow.Cells[14].CssClass = "centralizarTH";
                    grdAluno.Columns[14].ItemStyle.CssClass = "centralizarTH";
                }
                if (!chkEmpresaAluno.Checked)
                {
                    grdAluno.HeaderRow.Cells[15].CssClass = "hidden notexport";
                    grdAluno.Columns[15].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdAluno.HeaderRow.Cells[15].CssClass = "centralizarTH";
                    grdAluno.Columns[15].ItemStyle.CssClass = "centralizarTH";
                }
                if (!chkCargoAluno.Checked)
                {
                    grdAluno.HeaderRow.Cells[16].CssClass = "hidden notexport";
                    grdAluno.Columns[16].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdAluno.HeaderRow.Cells[16].CssClass = "centralizarTH";
                    grdAluno.Columns[16].ItemStyle.CssClass = "centralizarTH";
                }
                if (!chkAreaConcentracaoAluno.Checked)
                {
                    grdAluno.HeaderRow.Cells[17].CssClass = "hidden notexport";
                    grdAluno.Columns[17].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdAluno.HeaderRow.Cells[17].CssClass = "centralizarTH";
                    grdAluno.Columns[17].ItemStyle.CssClass = "centralizarTH";
                }
                if (!chkSituacaoAluno.Checked)
                {
                    grdAluno.HeaderRow.Cells[18].CssClass = "hidden notexport";
                    grdAluno.Columns[18].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdAluno.HeaderRow.Cells[18].CssClass = "centralizarTH";
                    grdAluno.Columns[18].ItemStyle.CssClass = "centralizarTH";
                }
                if (!chkTipoMatricula.Checked)
                {
                    grdAluno.HeaderRow.Cells[19].CssClass = "hidden notexport";
                    grdAluno.Columns[19].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdAluno.HeaderRow.Cells[19].CssClass = "centralizarTH";
                    grdAluno.Columns[19].ItemStyle.CssClass = "centralizarTH";
                }
                if (!chkOrientador.Checked)
                {
                    grdAluno.HeaderRow.Cells[20].CssClass = "hidden notexport";
                    grdAluno.Columns[20].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdAluno.HeaderRow.Cells[20].CssClass = "centralizarTH";
                    grdAluno.Columns[20].ItemStyle.CssClass = "centralizarTH";
                }
                if (!chkDatNascimento.Checked)
                {
                    grdAluno.HeaderRow.Cells[21].CssClass = "hidden notexport";
                    grdAluno.Columns[21].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdAluno.HeaderRow.Cells[21].CssClass = "centralizarTH";
                    grdAluno.Columns[21].ItemStyle.CssClass = "centralizarTH";
                }
                if (!chkPalavraChave.Checked)
                {
                    grdAluno.HeaderRow.Cells[22].CssClass = "hidden notexport";
                    grdAluno.Columns[22].ItemStyle.CssClass = "hidden notexport";
                }
                else
                {
                    grdAluno.HeaderRow.Cells[22].CssClass = "centralizarTH";
                    grdAluno.Columns[22].ItemStyle.CssClass = "centralizarTH";
                }
            }

        }

        public void ddlTipoCursoAluno_SelectedIndexChanged(Object sender, EventArgs e)
        {
            //if (ddlTipoCursoAluno.SelectedValue != "")
            //{
            CursoAplicacao aplicacaoCurso = new CursoAplicacao();
            cursos item = new cursos();
            if (ddlTipoCursoAluno.SelectedValue != "")
            {
                item.id_tipo_curso = Convert.ToInt32(ddlTipoCursoAluno.SelectedValue);
            }
            List<cursos> listaCurso = aplicacaoCurso.ListaItem(item);
            var lista = from item2 in listaCurso
                        select new
                        {
                            id_curso = item2.id_curso,
                            nome = item2.sigla + " - " + item2.nome
                        };

            ddlNomeCursoAluno.Items.Clear();
            ddlNomeCursoAluno.DataSource = lista.OrderBy(x => x.nome);
            ddlNomeCursoAluno.DataValueField = "id_curso";
            ddlNomeCursoAluno.DataTextField = "nome";
            ddlNomeCursoAluno.DataBind();
            ddlNomeCursoAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os Cursos", ""));
            ddlNomeCursoAluno.SelectedValue = "";

            TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
            turmas itemTurma = new turmas();
            List<turmas> listaTurma;

            listaTurma = aplicacaoTurma.ListaItemCombo(item);

            var q = (from h in listaTurma
                     group h by new { h.cod_turma } into hh
                     select new
                     {
                         hh.Key.cod_turma,
                     }).OrderByDescending(i => i.cod_turma);


            ddlTurmaAluno.Items.Clear();
            ddlTurmaAluno.DataSource = q;
            ddlTurmaAluno.DataValueField = "cod_turma";
            ddlTurmaAluno.DataTextField = "cod_turma";
            ddlTurmaAluno.DataBind();
            ddlTurmaAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todas Turmas", ""));
            ddlTurmaAluno.SelectedValue = "";

            //QuadrimestreAplicacao aplicacaoPeriodo = new QuadrimestreAplicacao();
            //quadrimestres itemPeriodo = new quadrimestres();
            //List<quadrimestres> listaPeriodo;
            //if (ddlTipoCursoAluno.SelectedValue != "")
            //{
            //    listaPeriodo = aplicacaoPeriodo.ListaItem(listaCurso);
            //}
            //else
            //{
            //    listaPeriodo = aplicacaoPeriodo.ListaItem(itemPeriodo);
            //}
            
            //ddlPeriodoAluno.Items.Clear();
            //ddlPeriodoAluno.DataSource = listaPeriodo.OrderByDescending(x => x.quadrimestre);
            //ddlPeriodoAluno.DataValueField = "quadrimestre";
            //ddlPeriodoAluno.DataTextField = "quadrimestre";
            //ddlPeriodoAluno.DataBind();
            //ddlPeriodoAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os Períodos", ""));
            //ddlPeriodoAluno.SelectedValue = "";

            ddlOferecimentoAluno.Items.Clear();


            //}
            //else
            //{
            //    ddlNomeCursoAluno.Items.Clear();
            //    ddlPeriodoAluno.Items.Clear();
            //}
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fSelect2();", true);
        }

        public void ddlNomeCursoAluno_SelectedIndexChanged(Object sender, EventArgs e)
        {
            //if (ddlTipoCursoAluno.SelectedValue != "")
            //{

            //QuadrimestreAplicacao aplicacaoPeriodo = new QuadrimestreAplicacao();
            cursos itemCurso = new cursos();
            if (ddlNomeCursoAluno.SelectedValue != "")
            {
                itemCurso.id_curso = Convert.ToInt32(ddlNomeCursoAluno.SelectedValue);
            }
            if (ddlTipoCursoAluno.SelectedValue != "")
            {
                itemCurso.id_tipo_curso = Convert.ToInt32(ddlTipoCursoAluno.SelectedValue);
            }
            //List<quadrimestres> listaPeriodo = aplicacaoPeriodo.ListaItem(itemCurso);
            //ddlPeriodoAluno.Items.Clear();
            //ddlPeriodoAluno.DataSource = listaPeriodo.OrderByDescending(x => x.quadrimestre);
            //ddlPeriodoAluno.DataValueField = "quadrimestre";
            //ddlPeriodoAluno.DataTextField = "quadrimestre";
            //ddlPeriodoAluno.DataBind();
            //ddlPeriodoAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os Períodos", ""));
            //ddlPeriodoAluno.SelectedValue = "";

            string sAux;
            sAux = ddlTurmaAluno.SelectedValue;

            TurmasAplicacao aplicacaoTurma = new TurmasAplicacao();
            turmas itemTurma = new turmas();
            List<turmas> listaTurma;

            listaTurma = aplicacaoTurma.ListaItemCombo(itemCurso);

            var q = (from h in listaTurma
                     group h by new { h.cod_turma } into hh
                     select new
                     {
                         hh.Key.cod_turma,
                     }).OrderByDescending(i => i.cod_turma);

            ddlTurmaAluno.Items.Clear();
            ddlTurmaAluno.DataSource = q;
            ddlTurmaAluno.DataValueField = "cod_turma";
            ddlTurmaAluno.DataTextField = "cod_turma";
            ddlTurmaAluno.DataBind();
            ddlTurmaAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todas Turmas", ""));

            if (q.Any(x=> x.cod_turma == sAux))
            {
                ddlTurmaAluno.SelectedValue = sAux;
            }
            else
            {
                ddlTurmaAluno.SelectedValue = "";
            }

            if (ddlNomeCursoAluno.SelectedValue != "")
            {
                
                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                List<oferecimentos> listaOferecimento;

                if (ddlTurmaAluno.SelectedValue != "")
                {
                    itemTurma.cod_turma = ddlTurmaAluno.SelectedValue;
                }
               
                listaOferecimento = aplicacaoOferecimento.ListaItemCombo(itemCurso, itemTurma);

                var lista2 = from item2 in listaOferecimento
                             select new
                             {
                                 id_oferecimento = item2.id_oferecimento,
                                 nome = item2.quadrimestre + " - " + item2.disciplinas.nome
                             };

                ddlOferecimentoAluno.Items.Clear();
                ddlOferecimentoAluno.DataSource = lista2.OrderByDescending(x => x.nome);
                ddlOferecimentoAluno.DataValueField = "id_oferecimento";
                ddlOferecimentoAluno.DataTextField = "nome";
                ddlOferecimentoAluno.DataBind();
                ddlOferecimentoAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os Oferecimentos", ""));
                ddlOferecimentoAluno.SelectedValue = "";
            }
            else
            {
                ddlOferecimentoAluno.Items.Clear();
            }
            //}
            //else
            //{
            //    ddlNomeCursoAluno.Items.Clear();
            //    ddlPeriodoAluno.Items.Clear();
            //}
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fSelect2();", true);
        }

        public void ddlTurmaAluno_SelectedIndexChanged(Object sender, EventArgs e)
        {
            CursoAplicacao aplicacaoCurso = new CursoAplicacao();
            cursos itemCurso = new cursos();
            turmas item = new turmas();
            //if (ddlTipoCursoAluno.SelectedValue != "")
            //{
            //    item.id_tipo_curso = Convert.ToInt32(ddlTipoCursoAluno.SelectedValue);
            //}
            if (ddlNomeCursoAluno.SelectedValue != "")
            {
                itemCurso.id_curso = Convert.ToInt32(ddlNomeCursoAluno.SelectedValue);
                OferecimentoAplicacao aplicacaoOferecimento = new OferecimentoAplicacao();
                List<oferecimentos> listaOferecimento;

                if (ddlTurmaAluno.SelectedValue != "")
                {
                    item.cod_turma = ddlTurmaAluno.SelectedValue;
                }

                listaOferecimento = aplicacaoOferecimento.ListaItemCombo(itemCurso, item);

                var lista2 = from item2 in listaOferecimento
                             select new
                             {
                                 id_oferecimento = item2.id_oferecimento,
                                 nome = item2.quadrimestre + " - " + item2.disciplinas.nome
                             };

                ddlOferecimentoAluno.Items.Clear();
                ddlOferecimentoAluno.DataSource = lista2.OrderByDescending(x => x.nome);
                ddlOferecimentoAluno.DataValueField = "id_oferecimento";
                ddlOferecimentoAluno.DataTextField = "nome";
                ddlOferecimentoAluno.DataBind();
                ddlOferecimentoAluno.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os Oferecimentos", ""));
                ddlOferecimentoAluno.SelectedValue = "";
            }
            else
            {
                ddlOferecimentoAluno.Items.Clear();
            }


            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fSelect2();", true);
        }

        public string setEndereco(string sLogradouro, string sNumero, string sComplemento, string sBairro, string sCidade, string sEstado, string sPais, string sCEP)
        {
            //HashSet<matricula_turma> lista = (HashSet<matricula_turma>)objeto;
            string sAux = "";

            if (chkEnderecoAluno.Checked)
            {
                sAux = sLogradouro + ", " + sNumero;
                if (sComplemento != "")
                {
                    sAux = sAux + ", " + sComplemento;
                }
                if (sBairro != "")
                {
                    sAux = sAux + " " + sBairro;
                }
                sAux = sAux + "<br> ";
                if (sCidade != "")
                {
                    sAux = sAux + " " + sCidade;
                }
                if (sEstado != "")
                {
                    sAux = sAux + "/" + sEstado;
                }
                if (sPais != "")
                {
                    sAux = sAux + " - " + sPais;
                }
                if (sCEP != "")
                {
                    sAux = sAux + " - CEP " + sCEP;
                }
            }


            return sAux;
        }

        public string setRG(string sNumero, string sDigito)
        {
            string sAux = "";
            if (chkRgAluno.Checked)
            {
                if (sDigito != "")
                {
                    sAux = sNumero + "-" + sDigito;
                }
                else
                {
                    sAux = sNumero;
                }
            }
            
            return sAux;
        }

        public string setTipoCurso(object objeto)
        {
            HashSet<matricula_turma> lista = (HashSet<matricula_turma>)objeto;
            string sAux = "";
            sAux = "";
            if (chkTipoCursoAluno.Checked)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<hr />";
                    }
                    sAux = sAux + "<span style=\"line-height: 2.2em;\">" + lista.ElementAt(i).turmas.cursos.tipos_curso.tipo_curso + "</span>";
                }
            }

            return sAux;
        }

        public string setCurso(object objeto)
        {
            HashSet<matricula_turma> lista = (HashSet<matricula_turma>)objeto;
            string sAux = "";
            sAux = "";
            if (chkCursoAluno.Checked)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<hr />";
                    }
                    sAux = sAux + "<span style=\"line-height: 2.2em;\">" + lista.ElementAt(i).turmas.cursos.nome + "</span>";
                }
            }

            return sAux;
        }

        public string setTurma(object objeto)
        {
            HashSet<matricula_turma> lista = (HashSet<matricula_turma>)objeto;
            string sAux = "";
            sAux = "";

            if (chkTurmaAluno.Checked)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<hr />";
                    }
                    if (chkInicioFimTurma.Checked)
                    {
                        sAux = sAux + "<span style=\"line-height: 2.2em;\">" + lista.ElementAt(i).turmas.cod_turma + " \n(" + String.Format("{0:dd/MM/yyyy}", lista.ElementAt(i).turmas.data_inicio) + " - " + String.Format("{0:dd/MM/yyyy}", lista.ElementAt(i).turmas.data_fim) + ") </span>";
                    }
                    else
                    {
                        sAux = sAux + "<span style=\"line-height: 2.2em;\">" + lista.ElementAt(i).turmas.cod_turma + " </span>";
                    }
                
                }
            }
            

            return sAux;
        }

        //public string setPeriodo(object objeto)
        //{
        //    HashSet<matricula_turma> lista = (HashSet<matricula_turma>)objeto;
        //    string sAux = "";
        //    sAux = "";
        //    for (int i = 0; i < lista.Count; i++)
        //    {
        //        if (sAux != "")
        //        {
        //            sAux = sAux + "<hr />";
        //        }
        //        sAux = sAux + "<span style=\"line-height: 2.2em;\">" + lista.ElementAt(i).turmas.quadrimestre + "</span>";
        //    }

        //    return sAux;
        //}

        public string setOferecimento(object objeto)
        {
            HashSet<matricula_turma> lista = (HashSet<matricula_turma>)objeto;
            string sAux = "";
            string qFreq = "";
            int IdOferecimento;

            if (chkOferecimentoAluno.Checked)
            {
                foreach (var elemento in lista)
                {
                    foreach (var elemento2 in elemento.turmas.matricula_oferecimento.Where(x => x.id_aluno == elemento.id_aluno).OrderByDescending(x => x.id_oferecimento).ToList())
                    {
                        if (ddlOferecimentoAluno.SelectedValue !="")
                        {
                            IdOferecimento = Convert.ToInt32(ddlOferecimentoAluno.SelectedValue);
                        }
                        else
                        {
                            IdOferecimento = 0;
                        }
                        if ((IdOferecimento == elemento2.id_oferecimento && !chkTodosOferecimentos.Checked) || (chkTodosOferecimentos.Checked))
                        {
                            if (sAux != "")
                            {
                                sAux = sAux + "\n<hr />";
                            }
                            if (lista.Count > 1)
                            {
                                qFreq = (elemento2.oferecimentos.presenca.Where(x => x.id_aluno == elemento2.id_aluno).Count() == 0) ? "0,00%" : ((elemento2.oferecimentos.presenca.Where(x => x.id_aluno == elemento2.id_aluno && x.presente == true).Count()) / (elemento2.oferecimentos.presenca.Where(x => x.id_aluno == elemento2.id_aluno).Count() * 0.01)).ToString("0.##") + "%";
                                qFreq = " - Freq: " + qFreq;
                                sAux = sAux + "<span style=\"line-height: 2.2em;\">" + elemento.turmas.cod_turma + " " + elemento2.oferecimentos.quadrimestre + " " + elemento2.oferecimentos.disciplinas.nome + " " + qFreq + "</span>";
                            }
                            else
                            {
                                qFreq = (elemento2.oferecimentos.presenca.Where(x => x.id_aluno == elemento2.id_aluno).Count() == 0) ? "0,00%" : ((elemento2.oferecimentos.presenca.Where(x => x.id_aluno == elemento2.id_aluno && x.presente == true).Count()) / (elemento2.oferecimentos.presenca.Where(x => x.id_aluno == elemento2.id_aluno).Count() * 0.01)).ToString("0.##") + "%";
                                qFreq = " - Freq: " + qFreq;
                                sAux = sAux + "<span style=\"line-height: 2.2em;\">" + elemento2.oferecimentos.quadrimestre + " " + elemento2.oferecimentos.disciplinas.nome + " " + qFreq + "</span>";
                            }

                        }
                    }
                }
            }
            
            return sAux;
        }

        public string setArea(object objeto)
        {
            HashSet<matricula_turma> lista = (HashSet<matricula_turma>)objeto;
            string sAux = "";
            sAux = "";

            if (chkAreaConcentracaoAluno.Checked)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<hr />";
                    }
                    if (lista.ElementAt(i).areas_concentracao != null)
                    {
                        sAux = sAux + "<span style=\"line-height: 2.2em;\">" + lista.ElementAt(i).areas_concentracao.nome + "</span>";
                    }

                }
            }

            return sAux;
        }

        public string setSituacao(object objeto)
        {
            HashSet<matricula_turma> lista = (HashSet<matricula_turma>)objeto;
            string sAux = "";
            sAux = "";
            DateTime dData;

            if (chkSituacaoAluno.Checked)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<hr />";
                    }
                    if (lista.ElementAt(i).historico_matricula_turma.Count > 0)
                    {
                        if (lista.ElementAt(i).historico_matricula_turma.Max(x => x.data_inicio) != null)
                        {
                            dData = lista.ElementAt(i).historico_matricula_turma.Max(x => x.data_inicio).Value;
                            if (lista.ElementAt(i).historico_matricula_turma.Where(x => x.data_inicio == dData).FirstOrDefault().situacao == "Titulado")
                            {
                                sAux = sAux + "<span style=\"line-height: 2.2em;\">" + lista.ElementAt(i).historico_matricula_turma.Where(x => x.data_inicio == dData).FirstOrDefault().situacao + " - " + lista.ElementAt(i).historico_matricula_turma.Where(x => x.data_inicio == dData).FirstOrDefault().data_fim.Value.Year + "</span>";
                            }
                            else
                            {
                                sAux = sAux + "<span style=\"line-height: 2.2em;\">" + lista.ElementAt(i).historico_matricula_turma.Where(x => x.data_inicio == dData).FirstOrDefault().situacao + "</span>";
                            }
                        }
                        else
                        {
                            dData = lista.ElementAt(i).historico_matricula_turma.Max(x => x.data_fim).Value;
                            sAux = sAux + "<span style=\"line-height: 2.2em;\">" + lista.ElementAt(i).historico_matricula_turma.Where(x => x.data_fim == dData).FirstOrDefault().situacao + "</span>";
                        }

                    }
                    else
                    {
                        sAux = sAux + "<span style=\"line-height: 2.2em;\">Sem situação</span>";
                    }

                }
            }

            return sAux;
        }

        public string setTipoMatricula(object objeto)
        {
            HashSet<matricula_turma> lista = (HashSet<matricula_turma>)objeto;
            string sAux = "";
            sAux = "";
            if (chkTipoMatricula.Checked)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<hr />";
                    }
                    if (lista.ElementAt(i).historico_matricula_turma.Count > 0)
                    {
                        if (lista.ElementAt(i).historico_matricula_turma.Any(x => x.situacao == "Matriculado"))
                        {
                            sAux = sAux + "<span style=\"line-height: 2.2em;\">" + lista.ElementAt(i).historico_matricula_turma.Where(x => x.situacao == "Matriculado").FirstOrDefault().status + "</span>";
                        }
                        else
                        {
                            sAux = sAux + "<span style=\"line-height: 2.2em;\">" + lista.ElementAt(i).historico_matricula_turma.FirstOrDefault().status + "</span>";
                        }
                    }
                    else
                    {
                        sAux = sAux + "<span style=\"line-height: 2.2em;\">Sem Tipo</span>";
                    }

                }
            }

            return sAux;
        }

        public string setOrientacao(object objeto)
        {
            HashSet<matricula_turma> lista = (HashSet<matricula_turma>)objeto;
            string sAux = "";
            sAux = "";
            
            DateTime qData;
            string qIdProfessor;

            if (chkOrientador.Checked)
            {
                foreach (var elemento in lista)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<hr />";
                    }

                    if (elemento.banca.Any(x => x.tipo_banca == "Defesa"))
                    {
                        if (elemento.banca.Where(x => x.tipo_banca == "Defesa").FirstOrDefault().banca_professores.Any(x => x.tipo_professor == "Orientador"))
                        {
                            qData = elemento.banca.Where(x => x.tipo_banca == "Defesa").FirstOrDefault().data_cadastro.Value;
                            qIdProfessor = elemento.banca.Where(x => x.tipo_banca == "Defesa").FirstOrDefault().banca_professores.Where(x => x.tipo_professor == "Orientador").FirstOrDefault().id_professor.ToString();
                            // Tem que verificar se tem (tipo_banca == "Qualificação"), pois cursos de especialização não tem banca de qualificação
                            if (elemento.banca.Any(x => x.tipo_banca == "Qualificação"))
                            {
                                if (elemento.banca.Where(x => x.tipo_banca == "Qualificação").FirstOrDefault().banca_professores.Any(x => x.tipo_professor == "Orientador"))
                                {
                                    if (qIdProfessor == elemento.banca.Where(x => x.tipo_banca == "Qualificação").FirstOrDefault().banca_professores.Where(x => x.tipo_professor == "Orientador").FirstOrDefault().id_professor.ToString())
                                    {
                                        qData = elemento.banca.Where(x => x.tipo_banca == "Qualificação").FirstOrDefault().data_cadastro.Value;
                                        if (elemento.matricula_turma_orientacao.Count != 0)
                                        {
                                            if (qIdProfessor == elemento.matricula_turma_orientacao.FirstOrDefault().id_professor.ToString())
                                            {
                                                qData = elemento.matricula_turma_orientacao.FirstOrDefault().data_cadastro.Value;
                                            }
                                        }
                                    }
                                }
                            }

                            sAux = sAux + "<span style=\"line-height: 2.2em;\">" + elemento.banca.Where(x => x.tipo_banca == "Defesa").FirstOrDefault().banca_professores.Where(x => x.tipo_professor == "Orientador").FirstOrDefault().professores.nome + " - " + String.Format("{0:dd/MM/yyyy}", qData) + " </span>";
                        }
                        else
                        {
                            sAux = sAux + "<span style=\"line-height: 2.2em;\"> Defesa Sem Orientador </span>";
                        }
                        
                    }
                    else if (elemento.banca.Any(x => x.tipo_banca == "Qualificação"))
                    {
                        if (elemento.banca.Where(x => x.tipo_banca == "Qualificação").FirstOrDefault().banca_professores.Any(x => x.tipo_professor == "Orientador"))
                        {
                            qData = elemento.banca.Where(x => x.tipo_banca == "Qualificação").FirstOrDefault().data_cadastro.Value;
                            qIdProfessor = elemento.banca.Where(x => x.tipo_banca == "Qualificação").FirstOrDefault().banca_professores.Where(x => x.tipo_professor == "Orientador").FirstOrDefault().id_professor.ToString();
                            if (elemento.matricula_turma_orientacao.Count != 0)
                            {
                                if (qIdProfessor == elemento.matricula_turma_orientacao.FirstOrDefault().id_professor.ToString())
                                {
                                    qData = elemento.matricula_turma_orientacao.FirstOrDefault().data_cadastro.Value;
                                }
                            }

                            sAux = sAux + "<span style=\"line-height: 2.2em;\">" + elemento.banca.Where(x => x.tipo_banca == "Qualificação").FirstOrDefault().banca_professores.Where(x => x.tipo_professor == "Orientador").FirstOrDefault().professores.nome + " - " + String.Format("{0:dd/MM/yyyy}", qData) + " </span>";
                        }
                        else
                        {
                            sAux = sAux + "<span style=\"line-height: 2.2em;\"> Qualificação Sem Orientador </span>";
                        }
                        
                    }
                    else if (elemento.matricula_turma_orientacao.Count != 0)
                    {
                        qData = elemento.matricula_turma_orientacao.FirstOrDefault().data_cadastro.Value;
                        sAux = sAux + "<span style=\"line-height: 2.2em;\">" + elemento.matricula_turma_orientacao.FirstOrDefault().professores.nome + " - " + String.Format("{0:dd/MM/yyyy}", qData) + " </span>";
                    }
                    else
                    {
                        sAux = sAux + "<span style=\"line-height: 2.2em;\">Sem Orientador</span>";
                    }
                }
            }

            return sAux;
        }

        //protected void grdAluno_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Page")
        //    {
        //        return;
        //    }

        //    int linha = Convert.ToInt32(e.CommandArgument);
        //    int codigo = Convert.ToInt32(grdAluno.DataKeys[linha].Values[0]);
        //    alunos item = new alunos();
        //    item.idaluno = codigo;
        //    switch (grdAluno.DataKeys[linha].Values[1].ToString())
        //    {
        //        case "Editar":
        //            AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
        //            item = aplicacaoAluno.BuscaItem(item);
        //            Session.Add("Aluno", item);
        //            Session.Add("sNovoAluno", false);
        //            Response.Redirect("cadAlunoGestao.aspx", true);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //public void grdAluno_Command(object sender, CommandEventArgs e)
        //{
        //    int index = Convert.ToInt32(e.CommandArgument);
        //    if (e.CommandName == "StartService")
        //    {
        //        int linha = Convert.ToInt32(e.CommandArgument);
        //        int codigo = Convert.ToInt32(grdAluno.DataKeys[linha].Values[0]);
        //        alunos item = new alunos();
        //        item.idaluno = codigo;
        //        AlunoAplicacao aplicacaoAluno = new AlunoAplicacao();
        //        item = aplicacaoAluno.BuscaItem(item);
        //        Session.Add("Aluno", item);
        //        Session.Add("sNovoAluno", false);
        //        Response.Redirect("cadAlunoGestao.aspx", true);
        //    }
        //}
    }
}