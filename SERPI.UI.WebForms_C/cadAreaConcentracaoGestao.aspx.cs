using Aplicacao_C;
using SERPI.Dominio_C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class cadAreaConcentracaoGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 13)) // Área de Concentração - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos itemCurso = new cursos();
                List<cursos> listaCurso = aplicacaoCurso.ListaItem(itemCurso);

                var listaCurso2 = from item2 in listaCurso.OrderBy(x => x.sigla)
                    select new
                    {
                        id_curso = item2.id_curso,
                        nome = Convert.ToString(item2.sigla) + " - " + Convert.ToString(item2.nome)
                    };

                ddlCodigoCursoArea.Items.Clear();
                ddlCodigoCursoArea.DataSource = listaCurso2;
                ddlCodigoCursoArea.DataValueField = "id_curso";
                ddlCodigoCursoArea.DataTextField = "nome";
                ddlCodigoCursoArea.DataBind();
                ddlCodigoCursoArea.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Curso", ""));
                ddlCodigoCursoArea.SelectedValue = "";

                //ddlNomeCursoArea.Items.Clear();
                //ddlNomeCursoArea.DataSource = listaCurso;
                //ddlNomeCursoArea.DataValueField = "id_curso";
                //ddlNomeCursoArea.DataTextField = "nome";
                //ddlNomeCursoArea.DataBind();
                //ddlNomeCursoArea.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Curso", ""));
                //ddlNomeCursoArea.SelectedValue = "";

                //ddlNomeCursoArea.Attributes.Add("onselectedindexchanged", "javascript: ShowProgress();");
                //ddlCodigoCursoArea.Attributes.Add("onselectedindexchanged", "javascript: ShowProgress();");

                if (Session["sNewArea"] != null && (Boolean)Session["sNewArea"] != true)
                {
                    areas_concentracao item;
                    item = (areas_concentracao)Session["areas_concentracao"];
                    lblTituloPagina.Text = "(Editar) - N.º " + item.id_area_concentracao;

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


                    txtNomeArea.Value = item.nome;
                    txtNumeroEletivasArea.Value = item.num_eletivas.ToString();
                    if (item.disponivel==true)
                    {
                        chkDisponivelArea.Checked = true;
                    }
                    else
                    {
                        chkDisponivelArea.Checked = false;
                    }
                    ddlCodigoCursoArea.SelectedValue = item.id_curso.ToString();

                    if (item.cursos.fichas_inscricao.Count() > 0)
                    {
                        ddlCodigoCursoArea.Attributes.Add("disabled", "disabled");
                    }
                    else
                    {
                        ddlCodigoCursoArea.Attributes.Remove("disabled");
                    }
                    //ddlNomeCursoArea.SelectedValue = item.id_curso.ToString();

                    disciplinas itemD;
                    List<disciplinas> listaD = new List<disciplinas>();
                    foreach (var itemInterno in item.cursos.cursos_disciplinas)
                    {
                        itemD = new disciplinas();
                        itemD = itemInterno.disciplinas;
                        listaD.Add(itemD);
                    }

                    PreencheDisciplina(listaD, item);

                    divCoordenadores.Visible = true;
                    btnCriarArea.Disabled = false;
                    divEdicao.Visible = true;

                    if (Session["AdiciondoSucesso"] != null)
                    {
                        if ((Boolean)Session["AdiciondoSucesso"])
                        {
                            Session["AdiciondoSucesso"] = null;
                            lblMensagem.Text = "Nova área de concentração adicionada com sucesso";
                            lblTituloMensagem.Text = "Nova Área de Concentração";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
                        }

                    }
                   
                }
                else
                {
                    lblInativado.Style["display"] = "none";
                    btnAtivar.Style["display"] = "none";
                    btnInativar.Style["display"] = "none";

                    lblTituloPagina.Text = "(nova)";
                    txtNomeArea.Value = "";
                    txtNumeroEletivasArea.Value = "";
                    chkDisponivelArea.Checked = false;
                    ddlCodigoCursoArea.SelectedValue = "";
                    ddlCodigoCursoArea.Attributes.Remove("disabled");
                    //ddlNomeCursoArea.SelectedValue = "";

                    divCoordenadores.Visible = false;
                    btnCriarArea.Disabled = true;
                    divEdicao.Visible = false;
                }
            }
            
        }

        protected void PreencheDisciplina(List<disciplinas> lista, areas_concentracao item_area)
        {
            GridDisciplina item;
            List<GridDisciplina> listaDisciplina = new List<GridDisciplina>();
            areas_concentracao_disciplinas item_areas_concentracao_disciplinas;

            foreach (var elemento in lista)
            {
                item = new GridDisciplina();
                item.id_disciplina = Convert.ToInt32(elemento.id_disciplina);
                item.codigo = elemento.codigo;
                item.nome = elemento.nome;
                if (item_area != null)
                {
                    item_areas_concentracao_disciplinas = item_area.areas_concentracao_disciplinas.Where(x => x.id_disciplina == elemento.id_disciplina).SingleOrDefault();
                    if (item_areas_concentracao_disciplinas != null)
                    {
                        item.associado = 1;
                        if (item_areas_concentracao_disciplinas.obrigatoria == true)
                        {
                            item.obrigatoria = 1;
                        }
                        else
                        {
                            item.obrigatoria = 0;
                        }
                    }
                    else
                    {
                        item.associado = 0;
                        item.obrigatoria = 0;
                    }
                }
                else
                {
                    item.associado = 0;
                    item.obrigatoria = 0;
                }
                
                listaDisciplina.Add(item);
            }


            grdDisciplinas.DataSource = listaDisciplina;
            grdDisciplinas.DataBind();

            if (lista.Count > 0)
            {
                grdDisciplinas.UseAccessibleHeader = true;
                grdDisciplinas.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultadosDisciplina.Visible = false;
                grdDisciplinas.Visible = true;
            }
            else
            {
                msgSemResultadosDisciplina.Visible = true;
            }

        }

        public void ddlCodigoCursoArea_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (ddlCodigoCursoArea.SelectedValue != "")
            {
                areas_concentracao itemArea;
                itemArea = (areas_concentracao)Session["areas_concentracao"];

                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                cursos item = new cursos();
                item.id_curso = Convert.ToInt32(ddlCodigoCursoArea.SelectedValue);
                List<disciplinas> lista = aplicacaoCurso.ListaDisciplinas(item);
                PreencheDisciplina(lista, itemArea);
            }
            //ddlNomeCursoArea.SelectedValue = ddlCodigoCursoArea.SelectedValue;
            if (grdDisciplinas.Rows.Count > 0)
            {
                grdDisciplinas.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "fAcendeCheck_Option();", true);


            ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", "javascript:fAtiva_grdDisciplina();fSelect2();", true);
        }

        //public void ddlNomeCursoArea_SelectedIndexChanged(Object sender, EventArgs e)
        //{
        //    if (ddlNomeCursoArea.SelectedValue != "")
        //    {
        //        areas_concentracao itemArea;
        //        itemArea = (areas_concentracao)Session["areas_concentracao"];

        //        CursoAplicacao aplicacaoCurso = new CursoAplicacao();
        //        cursos item = new cursos();
        //        item.id_curso = Convert.ToInt32(ddlNomeCursoArea.SelectedValue);
        //        List<disciplinas> lista = aplicacaoCurso.ListaDisciplinas(item);
        //        PreencheDisciplina(lista, itemArea);
        //    }
        //    ddlCodigoCursoArea.SelectedValue = ddlNomeCursoArea.SelectedValue;
        //    if (grdDisciplinas.Rows.Count > 0)
        //    {
        //        grdDisciplinas.HeaderRow.TableSection = TableRowSection.TableHeader;
        //    }
        //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "fAcendeCheck_Option();", true);


        //    ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", "javascript:fAtiva_grdDisciplina();", true);
        //}

        protected void btnCriarArea_Click(object sender, EventArgs e)
        {
            Session["sNewArea"] = true;
            Session["areas_concentracao"] = null;
            Session["AdiciondoSucesso"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("cadAreaConcentracao.aspx", true);
        }

        public class GridDisciplina
        {
            private int _id_disciplina;

            private string _codigo;

            private string _nome;

            private int _associado;

            private int _obrigatoria;

            public int id_disciplina
            {
                get
                {
                    return _id_disciplina;
                }
                set
                {
                    _id_disciplina = value;
                }
            }

            public string codigo
            {
                get
                {
                    return _codigo;
                }
                set
                {
                    _codigo = value;
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

            public int associado
            {
                get
                {
                    return _associado;
                }
                set
                {
                    _associado = value;
                }
            }

            public int obrigatoria
            {
                get
                {
                    return _obrigatoria;
                }
                set
                {
                    _obrigatoria = value;
                }
            }
        }

        protected void btnSalvar_ServerClick1(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";
                if (txtNomeArea.Value.Trim() == "")
                {
                    sAux = "Preencher o nome da Área de Concentração. <br/><br/>";
                }
                if (ddlCodigoCursoArea.SelectedValue == "")
                {
                    sAux = sAux + "Selecione o Curso associado à Área de Concentração.. <br/><br/>";
                }

                if (sAux != "")
                {
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);

                    if (grdDisciplinas.Rows.Count > 0)
                    {
                        grdDisciplinas.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", "javascript:fAtiva_grdDisciplina();", true);
                    return;
                }

                if (Session["sNewArea"] != null && (Boolean)Session["sNewArea"] != true)
                {

                    AreaAplicacao aplicacaoArea = new AreaAplicacao();
                    areas_concentracao item = new areas_concentracao();

                    item = (areas_concentracao)Session["areas_concentracao"];

                    if (item.id_curso != Convert.ToInt32(ddlCodigoCursoArea.SelectedValue))
                    {
                        item.id_curso = Convert.ToInt32(ddlCodigoCursoArea.SelectedValue);
                        
                    }
                    
                    item.nome = txtNomeArea.Value.Trim();
                    if (txtNumeroEletivasArea.Value.Trim() != "")
                    {
                        item.num_eletivas = Convert.ToInt32(txtNumeroEletivasArea.Value.Trim());
                    }
                    else
                    {
                        item.num_eletivas = null;
                    }
                    if (chkDisponivelArea.Checked)
                    {
                        item.disponivel = true;
                    }
                    else
                    {
                        item.disponivel = false;
                    }
                    item.status = "alterado";
                    item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    aplicacaoArea.AlterarItem(item);


                    aplicacaoArea.ExcluirAreaConcentracaoDisciplina(item);

                    areas_concentracao_disciplinas itemDisciplina;

                    sAux = Page.Request["hCodigo"].Replace("undefined;", "");

                    var aAux = sAux.Split(';');

                    if (aAux.ElementAt(0) != "")
                    {
                        for (int i = 0; i < aAux.Count(); i+=3)
                        {
                            itemDisciplina = new areas_concentracao_disciplinas();
                            itemDisciplina.id_disciplina = Convert.ToInt32(aAux.ElementAt(i));
                            itemDisciplina.id_area_concentracao = item.id_area_concentracao;
                            itemDisciplina.status = "cadastrado";
                            itemDisciplina.data_cadastro = DateTime.Now;
                            itemDisciplina.data_alteracao = itemDisciplina.data_cadastro;
                            itemDisciplina.usuario = usuario.usuario;

                            if (aAux.ElementAt(i+2) == "1")
                            {
                                itemDisciplina.obrigatoria = true;
                            }

                            aplicacaoArea.CriarAreaConcentracaoDisciplina(itemDisciplina);
                        }
                    }

                    lblMensagem.Text = "Área de Concentração alterada com sucesso.";
                    lblTituloMensagem.Text = "Alteração de Área de Concentração";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

                    item = aplicacaoArea.BuscaItem(item);

                    Session["areas_concentracao"] = item;

                    ddlCodigoCursoArea_SelectedIndexChanged(null, null);


                   }
                else
                {
                    areas_concentracao item = new areas_concentracao();

                    item.id_curso = Convert.ToInt32(ddlCodigoCursoArea.SelectedValue);
                    item.nome = txtNomeArea.Value.Trim();
                    if (txtNumeroEletivasArea.Value.Trim() != "")
                    {
                        item.num_eletivas = Convert.ToInt32(txtNumeroEletivasArea.Value.Trim());
                    }
                    if (chkDisponivelArea.Checked)
                    {
                        item.disponivel = true;
                    }
                    else
                    {
                        item.disponivel = false;
                    }
                    item.status = "cadastrado";
                    item.data_cadastro = DateTime.Now;
                    item.data_alteracao = item.data_cadastro;
                    item.usuario = usuario.usuario;

                    AreaAplicacao aplicacaoArea = new AreaAplicacao();

                    item = aplicacaoArea.CriarItem(item);

                    if (item != null)
                    {
                        areas_concentracao_disciplinas itemDisciplina;
                        foreach (GridViewRow row in grdDisciplinas.Rows)
                        {
                            if ((row.RowType == DataControlRowType.DataRow))
                            {
                                CheckBox chkAssociar = row.Cells[3].FindControl("chkAssociar") as CheckBox;
                                if (chkAssociar.Checked)
                                {
                                    itemDisciplina = new areas_concentracao_disciplinas();
                                    itemDisciplina.id_disciplina = Convert.ToInt32(row.Cells[0].Text);
                                    itemDisciplina.id_area_concentracao = item.id_area_concentracao;
                                    itemDisciplina.status = "caastrado";
                                    itemDisciplina.data_cadastro = DateTime.Now;
                                    itemDisciplina.data_alteracao = itemDisciplina.data_cadastro;
                                    itemDisciplina.usuario = usuario.usuario;

                                    CheckBox chkObrigatorio = row.Cells[4].FindControl("chkObrigatoria") as CheckBox;

                                    if (chkObrigatorio.Checked)
                                    {
                                        itemDisciplina.obrigatoria = true;
                                    }

                                    aplicacaoArea.CriarAreaConcentracaoDisciplina(itemDisciplina);

                                }
                            }
                        }

                        Session["areas_concentracao"] = item;
                        Session.Add("sNewArea", false);
                        Session["AdiciondoSucesso"] = true;
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na manutenção da Área de Concentração. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Área de Concentração";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }
    }
}