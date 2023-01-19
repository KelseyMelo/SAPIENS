using Aplicacao_C;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SERPI.Dominio_C;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using iTextSharp.text.html.simpleparser;

namespace SERPI.UI.WebForms_C
{
    public partial class cadDisciplinaGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 9)) // Disciplina - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                AreaAplicacao aplicacaoArea = new AreaAplicacao();
                areas_concentracao itemArea = new areas_concentracao();
                itemArea.status = "ativado";
                List<areas_concentracao> listaArea = aplicacaoArea.ListaItem(itemArea);

                ddlAreaConcentracaoDisciplina.Items.Clear();
                ddlAreaConcentracaoDisciplina.DataSource = listaArea.OrderBy(x => x.nome);
                ddlAreaConcentracaoDisciplina.DataValueField = "id_area_concentracao";
                ddlAreaConcentracaoDisciplina.DataTextField = "nome";
                ddlAreaConcentracaoDisciplina.DataBind();
                ddlAreaConcentracaoDisciplina.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione uma Área de Concentração", ""));
                ddlAreaConcentracaoDisciplina.SelectedValue = "";

                //ddlNomeCursoArea.Attributes.Add("onselectedindexchanged", "javascript: ShowProgress();");
                //ddlCodigoCursoArea.Attributes.Add("onselectedindexchanged", "javascript: ShowProgress();");

                if (Session["sNewDisciplina"] != null && (Boolean)Session["sNewDisciplina"] != true)
                {
                    disciplinas item;
                    item = (disciplinas)Session["disciplinas"];
                    lblTituloPagina.Text = "(Editar) - N.º " + item.id_disciplina;

                    if (item.status == "inativado")
                    {
                        lblInativadoDisciplina.Style["display"] = "block";
                        btnAtivar.Style["display"] = "block";
                        btnInativar.Style["display"] = "none";
                    }
                    else
                    {
                        lblInativadoDisciplina.Style["display"] = "none";
                        btnAtivar.Style["display"] = "none";
                        btnInativar.Style["display"] = "block";
                    }

                    txtDataCadastro.Value = String.Format("{0:dd/MM/yyyy}", item.data_cadastro);
                    txtDataAlteracao.Value = String.Format("{0:dd/MM/yyyy}", item.data_alteracao);
                    txtStatus.Value = item.status;
                    txtResponsavel.Value = item.usuario;

                    txtCodigoDisciplina.Value = item.codigo;
                    txtNomeDisciplina.Value = item.nome;
                    txtNumeroMaxAlunosDisciplina.Value = item.num_max_alunos.ToString();
                    ddlObrigatorioDisciplina.SelectedValue = item.obrigatorio.ToString();

                    ddlAreaConcentracaoDisciplina.SelectedValue = item.id_area_concentracao.ToString();
                    if (item.data_criacao != null)
                    {
                        txtDataCriacaoDisciplina.Value = item.data_criacao.ToString();
                    }
                    if (item.data_ultima_alteracao != null)
                    {
                        txtDataUltimaAlteracaoDisciplina.Value = item.data_ultima_alteracao.ToString();
                    }
                    txtCreditosDisciplina.Value = item.creditos.ToString();
                    txtCargaHorariaDisciplina.Value = item.carga_horaria.ToString();

                    DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                    disciplinas itemDiscplina = new disciplinas();
                    itemDiscplina.cursos_disciplinas.Add(new cursos_disciplinas());
                    if (item.cursos_disciplinas.Count != 0)
                    {
                        itemDiscplina.cursos_disciplinas.ElementAt(0).id_curso = item.cursos_disciplinas.ElementAt(0).id_curso;
                    }
                    List<disciplinas> listaDisciplina = aplicacaoDisciplina.ListaItem(itemDiscplina);

                    var listaDisciplina2 = from item2 in listaDisciplina.OrderBy(x => x.nome)
                                           select new
                                     {
                                         id_disciplina = item2.id_disciplina,
                                         nome = Convert.ToString(item2.codigo) + " - " + Convert.ToString(item2.nome)
                                     };

                    ddlDisciplinaSubstituta.Items.Clear();
                    ddlDisciplinaSubstituta.DataSource = listaDisciplina2;
                    ddlDisciplinaSubstituta.DataValueField = "id_disciplina";
                    ddlDisciplinaSubstituta.DataTextField = "nome";
                    ddlDisciplinaSubstituta.DataBind();
                    ddlDisciplinaSubstituta.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Nenhuma disciplina", ""));
                    ddlDisciplinaSubstituta.SelectedValue = "";

                    if (item.substituindo != null)
                    {
                        ddlDisciplinaSubstituta.SelectedValue = item.substituindo.ToString();
                    }
                    System.Web.UI.WebControls.ListItem removeItem = ddlDisciplinaSubstituta.Items.FindByValue(item.id_disciplina.ToString());
                    ddlDisciplinaSubstituta.Items.Remove(removeItem);

                    txtObjetivoDisciplina.Value = item.objetivo;
                    txtJustificativaDisciplina.Value = item.justificativa;
                    txtEmentaDisciplina.Value = item.ementa;
                    txtFormaAvaliacaoDisciplina.Value = item.forma_avaliacao;
                    txtMaterialUtilizadoDisciplina.Value = item.material_utilizado;
                    txtMetodologiaDisciplina.Value = item.metodologia;
                    txtConhecimentosPreviosDisciplina.Value = item.conhecimentos_previos;
                    txtProgramaDisciplina.Value = item.programa_disciplina;
                    txtBibliografiaBasicaDisciplina.Value = item.bibliografia_basica;
                    txtBibliografiaComplementarDisciplina.Value = item.bibliografica_compl;
                    txtObservacaoDisciplina.Value = item.observacao;

                    //PreencheProfessorAdicionado(item.disciplinas_professores.ToList());

                    //PreencheTecnicoAdicionado(item.disciplinas_professores.ToList());


                    //divProfessor.Visible = true;
                    tabProfessores.Style["display"] = "block";
                    tabTecnicos.Style["display"] = "block";
                    tabRequisitadas.Style["display"] = "block";

                    divProfessor.Style.Add("display", "block");
                    divTecnico.Visible = true;
                    divPreRequisito.Visible = true;
                    divProfessoresAdicionados.Visible = false;
                    divTecnicoAdicionados.Visible = false;
                    btnCriarDisciplina.Disabled = false;
                    btnImprimirEmenta.Disabled = false;
                    divEdicao.Visible = true;
                    txtUrlEmenta.Value = "https://sapiens.ipt.br/doctos/Disciplina_" + item.codigo.Trim() + ".pdf";
                    divUrl.Visible = true;

                    if (!File.Exists(Server.MapPath("~/doctos/Disciplina_" + item.codigo.Trim() + ".pdf")))
                    {
                        GeraEmentaPDF();
                    }

                    if (Session["AdiciondoSucesso"] != null)
                    {
                        if ((Boolean)Session["AdiciondoSucesso"])
                        {
                            Session["AdiciondoSucesso"] = null;
                            lblMensagem.Text = "Nova disciplina adicionada com sucesso";
                            lblTituloMensagem.Text = "Nova disciplina";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
                        }

                    }

                }
                else
                {
                    lblInativadoDisciplina.Style["display"] = "none";
                    btnAtivar.Style["display"] = "none";
                    btnInativar.Style["display"] = "none";

                    lblTituloPagina.Text = "(nova)";
                    txtDataCadastro.Value = "";
                    txtDataAlteracao.Value = "";
                    txtStatus.Value = "";
                    txtResponsavel.Value = "";

                    txtCodigoDisciplina.Value = "";
                    txtNomeDisciplina.Value = "";
                    txtNumeroMaxAlunosDisciplina.Value = "";
                    ddlObrigatorioDisciplina.SelectedValue = "";

                    txtDataCriacaoDisciplina.Value = "";
                    txtDataUltimaAlteracaoDisciplina.Value = "";
                    txtCreditosDisciplina.Value = "";
                    txtCargaHorariaDisciplina.Value = "";

                    DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                    disciplinas itemDiscplina = new disciplinas();
                    itemDiscplina.cursos_disciplinas.Add(new cursos_disciplinas());
                    List<disciplinas> listaDisciplina = aplicacaoDisciplina.ListaItem(itemDiscplina);

                    ddlDisciplinaSubstituta.Items.Clear();
                    ddlDisciplinaSubstituta.DataSource = listaDisciplina.OrderBy(x => x.nome);
                    ddlDisciplinaSubstituta.DataValueField = "id_disciplina";
                    ddlDisciplinaSubstituta.DataTextField = "nome";
                    ddlDisciplinaSubstituta.DataBind();
                    ddlDisciplinaSubstituta.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Nenhuma disciplina", ""));
                    ddlDisciplinaSubstituta.SelectedValue = "";

                    ddlDisciplinaSubstituta.SelectedValue = "";

                    txtObjetivoDisciplina.Value = "";
                    txtJustificativaDisciplina.Value = "";
                    txtEmentaDisciplina.Value = "";
                    txtFormaAvaliacaoDisciplina.Value = "";
                    txtMaterialUtilizadoDisciplina.Value = "";
                    txtMaterialUtilizadoDisciplina.Value = "";
                    txtConhecimentosPreviosDisciplina.Value = "";
                    txtProgramaDisciplina.Value = "";
                    txtBibliografiaBasicaDisciplina.Value = "";
                    txtBibliografiaComplementarDisciplina.Value = "";
                    txtObservacaoDisciplina.Value = "";

                    //divProfessor.Visible = false;
                    tabProfessores.Style["display"] = "none";
                    tabTecnicos.Style["display"] = "none";
                    tabRequisitadas.Style["display"] = "none";

                    divProfessor.Style.Add("display","none");
                    divTecnico.Visible = false;
                    divPreRequisito.Visible = false;
                    divProfessoresAdicionados.Visible = false;
                    divTecnicoAdicionados.Visible = false;
                    btnCriarDisciplina.Disabled = true;
                    btnImprimirEmenta.Disabled = true;
                    divEdicao.Visible = false;
                    divUrl.Visible = false;
                }
            }

        }

        protected void PreencheProfessorAdicionado(List<disciplinas_professores> lista)
        {
            GridProfessorAdicionado item;
            List<GridProfessorAdicionado> listaProfessorAdicionado = new List<GridProfessorAdicionado>();
            lista = lista.Where(x => x.tipo_professor == "professor").ToList();
            foreach (var elemento in lista)
            {
                item = new GridProfessorAdicionado();
                item.id_professor = Convert.ToInt32(elemento.id_professor);
                item.cpf = elemento.professores.cpf;
                item.nome = elemento.professores.nome;
                item.responsavel = (bool) elemento.responsavel;

                listaProfessorAdicionado.Add(item);
            }

            grdProfessorAdicionado.DataSource = listaProfessorAdicionado;
            grdProfessorAdicionado.DataBind();

            if (lista.Count > 0)
            {
                grdProfessorAdicionado.UseAccessibleHeader = true;
                grdProfessorAdicionado.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultadogrdProfessorAdicionado.Visible = false;
                grdProfessorAdicionado.Visible = true;
            }
            else
            {
                msgSemResultadogrdProfessorAdicionado.Visible = true;
            }

        }

        protected void PreencheTecnicoAdicionado(List<disciplinas_professores> lista)
        {
            GridProfessorAdicionado item;
            List<GridProfessorAdicionado> listaProfessorAdicionado = new List<GridProfessorAdicionado>();
            lista = lista.Where(x => x.tipo_professor == "tecnico").ToList();
            foreach (var elemento in lista)
            {
                item = new GridProfessorAdicionado();
                item.id_professor = Convert.ToInt32(elemento.id_professor);
                item.cpf = elemento.professores.cpf;
                item.nome = elemento.professores.nome;

                listaProfessorAdicionado.Add(item);
            }

            grdTecnicoAdicionado.DataSource = listaProfessorAdicionado;
            grdTecnicoAdicionado.DataBind();

            if (lista.Count > 0)
            {
                grdTecnicoAdicionado.UseAccessibleHeader = true;
                grdTecnicoAdicionado.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultadogrdTecnicoAdicionado.Visible = false;
                grdTecnicoAdicionado.Visible = true;
            }
            else
            {
                msgSemResultadogrdTecnicoAdicionado.Visible = true;
            }

        }

        protected void btnCriarDisciplina_Click(object sender, EventArgs e)
        {
            Session["sNewDisciplina"] = true;
            Session["disciplinas"] = null;
            Session["AdiciondoSucesso"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnImprimirEmenta_Click(object sender, EventArgs e)
        {
            try
            {
                disciplinas item;
                item = (disciplinas)Session["disciplinas"];

                if (File.Exists(Server.MapPath("~/doctos/Disciplina_" + item.codigo.Trim() + ".pdf")))
                {
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/Disciplina_" + item.codigo.Trim() + ".pdf")));
                    Response.WriteFile(Server.MapPath("~/doctos/Disciplina_" + item.codigo.Trim() + ".pdf"));
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão da Ementa da disciplina";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }
        }

        protected void GeraEmentaPDF()
        {
            try
            {
                GeraisAplicacao AplicacaoGerais = new GeraisAplicacao();
                disciplinas item;
                item = (disciplinas)Session["disciplinas"];

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 120, 40);//estibulando o espaçamento das margens que queremos ===Antigo===(45, 40, 40, 40)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/Disciplina_" + item.codigo.Trim() + ".pdf"), FileMode.Create));
                PDF_Cabec_Rodape_Disciplina pageHeaderFooter = new PDF_Cabec_Rodape_Disciplina();
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
                Font font_Verdana_8_Normal = new Font(_bfVerdanaNormal, 8, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                float[] widths;
                Paragraph p;
                PdfPCell cell;
                PdfPTable table;
                ArrayList htmlarraylist;

                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(2);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 63f, 180f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Título:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.codigo + " - " + item.nome, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Área de Concentração:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                if (item.areas_concentracao == null)
                {
                    p.Add(new Chunk("", font_Verdana_9_Normal));
                }
                else
                {
                    p.Add(new Chunk(item.areas_concentracao.nome, font_Verdana_9_Normal));
                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Obrigatória:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.obrigatorio == true ? "Sim" : "Não", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);
                doc.Add(table);


                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(4);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 46f, 30f, 40f, 60f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Criação:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.data_criacao != null ? String.Format("{0:dd/MM/yyyy}", item.data_criacao) : "", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Alteração:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.data_alteracao != null ? String.Format("{0:dd/MM/yyyy}", item.data_ultima_alteracao) : "", font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Carga Horária:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.carga_horaria.ToString(), font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Crédito:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk(item.creditos.ToString(), font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);
                doc.Add(table);


                //Aqui é uma nova tabela de 2 Colunas ========================================================
                table = new PdfPTable(2);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 63f, 180f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Responsável:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();

                foreach (var item2 in item.disciplinas_professores.Where(x=> x.tipo_professor == "professor" && x.responsavel == true).ToList())
                {
                    p.Add(new Chunk(item2.professores.nome + "\r\n", font_Verdana_9_Normal));
                }

                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Observações:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                p = new Paragraph();
                htmlarraylist = HTMLWorker.ParseToList(new StringReader(item.observacao), null);
                for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                {
                    p.Add(htmlarraylist[k]);
                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);
                //doc.Add(table);


                ////Aqui é uma nova tabela de 4 Colunas ========================================================
                //table = new PdfPTable(4);
                //table.TotalWidth = 520f;
                //table.LockedWidth = true;
                //widths = new float[] { 46f, 30f, 40f, 60f };
                //table.SetWidths(widths);

                
                //doc.Add(table);


                ////Aqui é uma nova tabela de 2 Colunas ========================================================
                //table = new PdfPTable(2);
                //table.TotalWidth = 520f;
                //table.LockedWidth = true;
                //widths = new float[] { 63f, 180f };
                //table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Objetivo:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                p = new Paragraph();
                htmlarraylist = HTMLWorker.ParseToList(new StringReader(item.objetivo), null);
                for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                {
                    p.Add(htmlarraylist[k]);
                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Justificativa:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                p = new Paragraph();
                htmlarraylist = HTMLWorker.ParseToList(new StringReader(item.justificativa), null);
                for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                {
                    p.Add(htmlarraylist[k]);
                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Ementa:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                p = new Paragraph();
                htmlarraylist = HTMLWorker.ParseToList(new StringReader(item.ementa), null);
                for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                {
                    p.Add(htmlarraylist[k]);
                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Forma de Avaliação:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                p = new Paragraph();
                htmlarraylist = HTMLWorker.ParseToList(new StringReader(item.forma_avaliacao), null);
                for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                {
                    p.Add(htmlarraylist[k]);
                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Material Utilizado:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                p = new Paragraph();
                htmlarraylist = HTMLWorker.ParseToList(new StringReader(item.material_utilizado), null);
                for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                {
                    p.Add(htmlarraylist[k]);
                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Metodologia:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                p = new Paragraph();
                htmlarraylist = HTMLWorker.ParseToList(new StringReader(item.metodologia), null);
                for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                {
                    p.Add(htmlarraylist[k]);
                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Conhecimentos Prévio:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                p = new Paragraph();
                htmlarraylist = HTMLWorker.ParseToList(new StringReader(item.conhecimentos_previos), null);
                for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                {
                    p.Add(htmlarraylist[k]);
                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Bibliografia Básica:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                p = new Paragraph();
                htmlarraylist = HTMLWorker.ParseToList(new StringReader(item.bibliografia_basica), null);
                for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                {
                    p.Add(htmlarraylist[k]);
                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Bibliografia Complementar:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                p = new Paragraph();
                htmlarraylist = HTMLWorker.ParseToList(new StringReader(item.bibliografica_compl), null);
                for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                {
                    p.Add(htmlarraylist[k]);
                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Programa da Disciplina:", font_Verdana_9_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                p = new Paragraph();
                htmlarraylist = HTMLWorker.ParseToList(new StringReader(item.programa_disciplina), null);
                for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                {
                    p.Add(htmlarraylist[k]);
                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
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
                lblMensagem.Text = "Erro na rotina que gera a Ementa da disciplina";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }

        }

        public class PDF_Cabec_Rodape_Disciplina : PdfPageEventHelper
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

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("cadDisciplina.aspx", true);
        }

        public class GridProfessorAdicionado
        {
            private int _id_professor;

            private string _cpf;

            private string _foto;

            private string _nome;

            private bool _responsavel;

            public int id_professor
            {
                get
                {
                    return _id_professor;
                }
                set
                {
                    _id_professor = value;
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

        protected void btnSalvar_ServerClick1(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";
                if (txtNomeDisciplina.Value.Trim() == "")
                {
                    sAux = "Preencher o nome da Disciplina. <br/><br/>";
                }
                if (txtCodigoDisciplina.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher o código da Disciplina. <br/><br/>";
                }
                else
                {
                    bool isAlphaBet = Regex.IsMatch(txtCodigoDisciplina.Value.Trim().Substring(0,1), "[a-z]", RegexOptions.IgnoreCase);
                    if (!isAlphaBet)
                    {
                        sAux = sAux + "O Código da disciplina deve sempre começar com uma Letra. <br/><br/>";
                    }
                }

                txtObjetivoDisciplina.Value = Page.Request["htxtObjetivoDisciplina"];
                txtJustificativaDisciplina.Value = Page.Request["htxtJustificativaDisciplina"];
                txtEmentaDisciplina.Value = Page.Request["htxtEmentaDisciplina"];
                txtFormaAvaliacaoDisciplina.Value = Page.Request["htxtFormaAvaliacaoDisciplina"];
                txtMaterialUtilizadoDisciplina.Value = Page.Request["htxtMaterialUtilizadoDisciplina"];
                txtMetodologiaDisciplina.Value = Page.Request["htxtMetodologiaDisciplina"];
                txtConhecimentosPreviosDisciplina.Value = Page.Request["htxtConhecimentosPreviosDisciplina"];
                txtProgramaDisciplina.Value = Page.Request["htxtProgramaDisciplina"];
                txtBibliografiaBasicaDisciplina.Value = Page.Request["htxtBibliografiaBasicaDisciplina"];
                txtBibliografiaComplementarDisciplina.Value = Page.Request["htxtBibliografiaComplementarDisciplina"];
                txtObservacaoDisciplina.Value = Page.Request["htxtObservacaoDisciplina"];

                //if (ddlAreaConcentracaoDisciplina.SelectedValue == "")
                //{
                //    sAux = sAux + "Selecione uma Área de Concentração. <br/><br/>";
                //}

                if (txtCreditosDisciplina.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher os Créditos da Disciplina. <br/><br/>";
                }

                if (txtCargaHorariaDisciplina.Value.Trim() == "")
                {
                    sAux = sAux + "Preencher a Carga Horária da Disciplina. <br/><br/>";
                }

                if (txtEmentaDisciplina.Value.Trim() == "<br>")
                {
                    sAux = sAux + "Preencher a Ementa da Disciplina. <br/><br/>";
                }

                if (txtBibliografiaBasicaDisciplina.Value.Trim() == "<br>")
                {
                    sAux = sAux + "Preencher a Bibliografia Básica da Disciplina. <br/><br/>";
                }

                if (sAux != "")
                {
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                    return;
                }

                

                if (Session["sNewDisciplina"] != null && (Boolean)Session["sNewDisciplina"] != true)
                {

                    DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                    disciplinas item = new disciplinas();
                    disciplinas itemRetorno;

                    item = (disciplinas)Session["disciplinas"];
                    item.nome = txtNomeDisciplina.Value.Trim();
                    item.codigo = txtCodigoDisciplina.Value.Trim();
                    if (txtNumeroMaxAlunosDisciplina.Value.Trim() != "")
                    {
                        item.num_max_alunos = Convert.ToInt32(txtNumeroMaxAlunosDisciplina.Value);
                    }
                    
                    if (ddlObrigatorioDisciplina.SelectedValue == "Sim")
                    {
                        item.obrigatorio = true;
                    }
                    else
                    {
                        item.obrigatorio = false;
                    }

                    if (ddlAreaConcentracaoDisciplina.SelectedValue != "")
                    {
                        item.id_area_concentracao = Convert.ToInt32(ddlAreaConcentracaoDisciplina.SelectedValue);
                    }
                    if (txtDataCriacaoDisciplina.Value != "")
                    {
                        item.data_criacao = Convert.ToDateTime(txtDataCriacaoDisciplina.Value);
                    }
                    if (txtDataUltimaAlteracaoDisciplina.Value != "")
                    {
                        item.data_ultima_alteracao = Convert.ToDateTime(txtDataUltimaAlteracaoDisciplina.Value);
                    }
                    item.creditos = Convert.ToInt32(txtCreditosDisciplina.Value);
                    item.carga_horaria = Convert.ToInt32(txtCargaHorariaDisciplina.Value);

                    if (ddlDisciplinaSubstituta.SelectedValue != "")
                    {
                        item.substituindo = Convert.ToInt32(ddlDisciplinaSubstituta.SelectedValue);
                    }

                    item.objetivo = txtObjetivoDisciplina.Value.Trim();
                    item.justificativa = txtJustificativaDisciplina.Value.Trim();
                    item.ementa = txtEmentaDisciplina.Value.Trim();
                    item.forma_avaliacao = txtFormaAvaliacaoDisciplina.Value.Trim();
                    item.material_utilizado = txtMaterialUtilizadoDisciplina.Value.Trim();
                    item.metodologia = txtMetodologiaDisciplina.Value.Trim();
                    item.conhecimentos_previos = txtConhecimentosPreviosDisciplina.Value.Trim();
                    item.programa_disciplina = txtProgramaDisciplina.Value.Trim();
                    item.bibliografia_basica = txtBibliografiaBasicaDisciplina.Value.Trim();
                    item.bibliografica_compl = txtBibliografiaComplementarDisciplina.Value.Trim();
                    item.observacao = txtObservacaoDisciplina.Value.Trim();

                    item.status = "alterado";
                    //item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    itemRetorno = aplicacaoDisciplina.VerificaItemMesmoCodigo(item);

                    if (itemRetorno != null)
                    {
                        if (itemRetorno.status != "inativado")
                        {
                            sAux = "Já existe uma disciplina cadastrada com o mesmo Código <br /><br />";
                            sAux = sAux + "<strong>Código:</strong> " + itemRetorno.codigo + "<br />";
                            sAux = sAux + "<strong>Nome:</strong> " + itemRetorno.nome + "<br />";

                            lblMensagem.Text = sAux;
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                            return;
                        }
                        else
                        {
                            sAux = "Já existe uma disciplina cadastrada com o mesmo Código, porém ela está <strong>inativada</strong>.<br /><br />";
                            sAux = sAux + "<strong>Código:</strong> " + itemRetorno.codigo + "<br />";
                            sAux = sAux + "<strong>Nome:</strong> " + itemRetorno.nome + "<br />";

                            lblMensagem.Text = sAux;
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                            return;
                        }
                    }

                    aplicacaoDisciplina.AlterarItem(item);

                    disciplinas_professores itemDisciplina_Professores = new disciplinas_professores();
                    itemDisciplina_Professores.id_disciplina = item.id_disciplina;
                    itemDisciplina_Professores.tipo_professor = "professor";

                    aplicacaoDisciplina.AlterarResponsavelProfessor_Tecnico_Disciplina(itemDisciplina_Professores);

                    sAux = Page.Request["hCodigo"].Replace("undefined;", "");

                    var aAux = sAux.Split(';');

                    if (aAux.ElementAt(0) != "")
                    {
                        for (int i = 0; i < aAux.Count(); i++)
                        {
                            itemDisciplina_Professores = new disciplinas_professores();
                            itemDisciplina_Professores.id_professor = Convert.ToInt32(aAux.ElementAt(i));
                            itemDisciplina_Professores.id_disciplina = item.id_disciplina;
                            itemDisciplina_Professores.tipo_professor = "professor";
                            itemDisciplina_Professores.status = "alterado";
                            itemDisciplina_Professores.responsavel = true;
                            itemDisciplina_Professores.data_alteracao = DateTime.Now;
                            itemDisciplina_Professores.usuario = usuario.usuario;

                            aplicacaoDisciplina.AlterarProfessor_Tecnico_Disciplina(itemDisciplina_Professores);
                        }
                    }
                    
                    //foreach (GridViewRow row in grdProfessorAdicionado.Rows)
                    //{
                    //    if ((row.RowType == DataControlRowType.DataRow))
                    //    {
                    //        CheckBox chkResponsavel = row.Cells[3].FindControl("chkResponsavel") as CheckBox;
                    //        itemDisciplina_Professores = new disciplinas_professores();
                    //        itemDisciplina_Professores.id_professor= Convert.ToInt32(row.Cells[0].Text);
                    //        itemDisciplina_Professores.id_disciplina = item.id_disciplina;
                    //        itemDisciplina_Professores.tipo_professor = "professor";
                    //        if (chkResponsavel.Checked)
                    //        {
                    //            itemDisciplina_Professores.responsavel = true;
                    //        }
                    //        else
                    //        {
                    //            itemDisciplina_Professores.responsavel = false;
                    //        }
                    //        itemDisciplina_Professores.status = "alterado";
                    //        itemDisciplina_Professores.data_alteracao = DateTime.Now;
                    //        itemDisciplina_Professores.usuario = usuario.usuario;

                    //        aplicacaoDisciplina.AlterarProfessor_Tecnico_Disciplina(itemDisciplina_Professores);
                    //    }

                    //}

                    item=aplicacaoDisciplina.BuscaItem(item);

                    lblMensagem.Text = "Disciplina alterada com sucesso.";
                    lblTituloMensagem.Text = "Alteração de Disciplina";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);

                    Session["disciplinas"] = item;

                    GeraEmentaPDF();

                }
                else
                {
                    DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                    disciplinas item = new disciplinas();
                    disciplinas itemRetorno;

                    item.nome = txtNomeDisciplina.Value.Trim();
                    item.codigo = txtCodigoDisciplina.Value.Trim();
                    if (txtNumeroMaxAlunosDisciplina.Value != "")
                    {
                        item.num_max_alunos = Convert.ToInt32(txtNumeroMaxAlunosDisciplina.Value);
                    }
                    if (ddlObrigatorioDisciplina.SelectedValue == "Sim")
                    {
                        item.obrigatorio = true;
                    }
                    else
                    {
                        item.obrigatorio = false;
                    }

                    if (ddlAreaConcentracaoDisciplina.SelectedValue != "")
                    {
                        item.id_area_concentracao = Convert.ToInt32(ddlAreaConcentracaoDisciplina.SelectedValue);
                    }
                    if (txtDataCriacaoDisciplina.Value != "")
                    {
                        item.data_criacao = Convert.ToDateTime(txtDataCriacaoDisciplina.Value);
                    }
                    if (txtDataUltimaAlteracaoDisciplina.Value != "")
                    {
                        item.data_ultima_alteracao = Convert.ToDateTime(txtDataUltimaAlteracaoDisciplina.Value);
                    }
                    item.creditos = Convert.ToInt32(txtCreditosDisciplina.Value);
                    item.carga_horaria = Convert.ToInt32(txtCargaHorariaDisciplina.Value);

                    if (ddlDisciplinaSubstituta.SelectedValue != "")
                    {
                        item.substituindo = Convert.ToInt32(ddlDisciplinaSubstituta.SelectedValue);
                    }

                    item.objetivo = txtObjetivoDisciplina.Value.Trim();
                    item.justificativa = txtJustificativaDisciplina.Value.Trim();
                    item.ementa = txtEmentaDisciplina.Value.Trim();
                    item.forma_avaliacao = txtFormaAvaliacaoDisciplina.Value.Trim();
                    item.material_utilizado = txtMaterialUtilizadoDisciplina.Value.Trim();
                    item.metodologia = txtMetodologiaDisciplina.Value.Trim();
                    item.conhecimentos_previos = txtConhecimentosPreviosDisciplina.Value.Trim();
                    item.programa_disciplina = txtProgramaDisciplina.Value.Trim();
                    item.bibliografia_basica = txtBibliografiaBasicaDisciplina.Value.Trim();
                    item.bibliografica_compl = txtBibliografiaComplementarDisciplina.Value.Trim();
                    item.observacao = txtObservacaoDisciplina.Value.Trim();

                    item.status = "cadastrado";
                    item.data_cadastro = DateTime.Now;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    itemRetorno = aplicacaoDisciplina.VerificaItemMesmoCodigo(item);

                    if (itemRetorno != null)
                    {
                        if (itemRetorno.status != "inativado")
                        {
                            sAux = "Já existe uma disciplina cadastrada com o mesmo Código <br /><br />";
                            sAux = sAux + "<strong>Código:</strong> " + itemRetorno.codigo + "<br />";
                            sAux = sAux + "<strong>Nome:</strong> " + itemRetorno.nome + "<br />";

                            lblMensagem.Text = sAux;
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                            return;
                        }
                        else
                        {
                            sAux = "Já existe uma disciplina cadastrada com o mesmo Código, porém ela está <strong>inativada</strong>.<br /><br />";
                            sAux = sAux + "<strong>Código:</strong> " + itemRetorno.codigo + "<br />";
                            sAux = sAux + "<strong>Nome:</strong> " + itemRetorno.nome + "<br />";

                            lblMensagem.Text = sAux;
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
                            return;
                        }
                    }

                    item = aplicacaoDisciplina.CriarItem(item);

                    if (item != null)
                    {
                        Session["disciplinas"] = item;
                        Session.Add("sNewDisciplina", false);
                        Session["AdiciondoSucesso"] = true;
                        GeraEmentaPDF();
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Houve algum problema na manutenção da Disciplina. <br/> <br/>Descrição do erro: " + ex.Message;
                lblTituloMensagem.Text = "Disciplina";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
            }
        }

        public void grdProfessorAdicionado_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                int linha = Convert.ToInt32(e.CommandArgument);
                int codigo = Convert.ToInt32(grdProfessorAdicionado.DataKeys[linha].Values[0]);
                disciplinas item = new disciplinas();
                item = (disciplinas)Session["disciplinas"];
                disciplinas_professores itemProfessores = new disciplinas_professores();
                itemProfessores.id_disciplina = item.id_disciplina;
                itemProfessores.id_professor = codigo;
                itemProfessores.tipo_professor = "professor";

                DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                aplicacaoDisciplina.ExcluirProfessor_Tecnico_Disciplina(itemProfessores);
                item = aplicacaoDisciplina.BuscaItem(item);
                Session["disciplinas"] = item;
                PreencheProfessorAdicionado(item.disciplinas_professores.ToList());

            }
        }

        public void grdTecnicoAdicionado_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                int linha = Convert.ToInt32(e.CommandArgument);
                int codigo = Convert.ToInt32(grdTecnicoAdicionado.DataKeys[linha].Values[0]);
                disciplinas item = new disciplinas();
                item = (disciplinas)Session["disciplinas"];
                disciplinas_professores itemProfessores = new disciplinas_professores();
                itemProfessores.id_disciplina = item.id_disciplina;
                itemProfessores.id_professor = codigo;
                itemProfessores.tipo_professor = "tecnico";

                DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                aplicacaoDisciplina.ExcluirProfessor_Tecnico_Disciplina(itemProfessores);
                item = aplicacaoDisciplina.BuscaItem(item);
                Session["disciplinas"] = item;
                PreencheTecnicoAdicionado(item.disciplinas_professores.ToList());

            }
        }

        protected void bntPerquisaProfessor_Click(object sender, EventArgs e)
        {
            DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
            disciplinas item;
            item = (disciplinas)Session["disciplinas"];
            professores itemProfessor = new professores();
            List<professores> listaProfessor = new List<professores>();
            disciplinas_professores itemdisciplinas_Professor = new disciplinas_professores();

            itemdisciplinas_Professor.id_disciplina = item.id_disciplina;
            itemdisciplinas_Professor.tipo_professor = "professor";

            if (txtCPFPerquisaProfessor.Value.Trim() != "")
            {
                itemProfessor.cpf = txtCPFPerquisaProfessor.Value.Trim();
            }

            if (txtNomePerquisaProfessor.Value.Trim() != "")
            {
                itemProfessor.nome = txtNomePerquisaProfessor.Value.Trim();
            }

            listaProfessor = aplicacaoDisciplina.ListaProfessoresDisponiveis(itemdisciplinas_Professor, itemProfessor);


            GridProfessorAdicionado itemGrade;
            List<GridProfessorAdicionado> listaProfessorAdicionado = new List<GridProfessorAdicionado>();

            foreach (var elemento in listaProfessor)
            {
                itemGrade = new GridProfessorAdicionado();
                itemGrade.id_professor = Convert.ToInt32(elemento.id_professor);
                itemGrade.cpf = elemento.cpf;
                itemGrade.nome = elemento.nome;

                listaProfessorAdicionado.Add(itemGrade);
            }


            grdProfessoresDisponiveis.DataSource = listaProfessorAdicionado;
            grdProfessoresDisponiveis.DataBind();

            if (listaProfessorAdicionado.Count > 0)
            {
                grdProfessoresDisponiveis.UseAccessibleHeader = true;
                grdProfessoresDisponiveis.HeaderRow.TableSection = TableRowSection.TableHeader;
                divgrdProfessoresDisponiveis.Style.Add("display", "none");
                grdProfessoresDisponiveis.Visible = true;
                ScriptManager.RegisterStartupScript(this.UpdatePanel3, this.UpdatePanel3.GetType(), "Script", "javascript:fAtiva_grdProfessoresDisponiveis();", true);
            }
            else
            {
                divgrdProfessoresDisponiveis.Style.Add("display", "block");
            }

            divResultadoListaProfessorDisponivel.Style.Add("display", "block");
        }

        protected void bntPerquisaTecnico_Click(object sender, EventArgs e)
        {
            DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
            disciplinas item;
            item = (disciplinas)Session["disciplinas"];
            professores itemProfessor = new professores();
            List<professores> listaProfessor = new List<professores>();
            disciplinas_professores itemdisciplinas_Professor = new disciplinas_professores();

            itemdisciplinas_Professor.id_disciplina = item.id_disciplina;
            itemdisciplinas_Professor.tipo_professor = "tecnico";

            if (txtCPFPerquisaTecnico.Value.Trim() != "")
            {
                itemProfessor.cpf = txtCPFPerquisaTecnico.Value.Trim();
            }

            if (txtNomePerquisaTecnico.Value.Trim() != "")
            {
                itemProfessor.nome = txtNomePerquisaTecnico.Value.Trim();
            }

            listaProfessor = aplicacaoDisciplina.ListaProfessoresDisponiveis(itemdisciplinas_Professor, itemProfessor);


            GridProfessorAdicionado itemGrade;
            List<GridProfessorAdicionado> listaProfessorAdicionado = new List<GridProfessorAdicionado>();

            foreach (var elemento in listaProfessor)
            {
                itemGrade = new GridProfessorAdicionado();
                itemGrade.id_professor = Convert.ToInt32(elemento.id_professor);
                itemGrade.cpf = elemento.cpf;
                itemGrade.nome = elemento.nome;

                listaProfessorAdicionado.Add(itemGrade);
            }


            grdTecnicoDisponiveis.DataSource = listaProfessorAdicionado;
            grdTecnicoDisponiveis.DataBind();

            if (listaProfessorAdicionado.Count > 0)
            {
                grdTecnicoDisponiveis.UseAccessibleHeader = true;
                grdTecnicoDisponiveis.HeaderRow.TableSection = TableRowSection.TableHeader;
                divgrdTecnicoDisponiveis.Style.Add("display", "none");
                grdTecnicoDisponiveis.Visible = true;
                ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.UpdatePanel6.GetType(), "Script", "javascript:fAtiva_grdTecnicoDisponiveis();", true);
            }
            else
            {
                divgrdTecnicoDisponiveis.Style.Add("display", "block");
            }

            divResultadoListaTecnicoDisponivel.Style.Add("display", "block");
        }

        public void grdProfessoresDisponiveis_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                int linha = Convert.ToInt32(e.CommandArgument);
                int codigo = Convert.ToInt32(grdProfessoresDisponiveis.DataKeys[linha].Values[0]);
                disciplinas item = new disciplinas();
                item = (disciplinas)Session["disciplinas"];
                disciplinas_professores itemProfessores = new disciplinas_professores();
                itemProfessores.id_disciplina = item.id_disciplina;
                itemProfessores.id_professor = codigo;
                itemProfessores.tipo_professor = "professor";

                itemProfessores.responsavel = false;
                itemProfessores.status = "cadastrado";
                itemProfessores.data_cadastro = DateTime.Now;
                itemProfessores.data_alteracao = DateTime.Now;
                itemProfessores.usuario = usuario.usuario;

                DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                aplicacaoDisciplina.IncluirProfessor_Tecnico_Disciplina(itemProfessores);
                item = aplicacaoDisciplina.BuscaItem(item);
                Session["disciplinas"] = item;
                PreencheProfessorAdicionado(item.disciplinas_professores.ToList());
                bntPerquisaProfessor_Click(null, null);
                ScriptManager.RegisterStartupScript(this.UpdatePanel6, this.UpdatePanel6.GetType(), "Script", "javascript:fMensagemConfirmacao('Inclusão de Professor','Inclusão do Professor <strong>'x'</strong> realizada com sucesso.<br />','success');", true);


            }
        }

        public void grdTecnicoDisponiveis_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                int linha = Convert.ToInt32(e.CommandArgument);
                int codigo = Convert.ToInt32(grdTecnicoDisponiveis.DataKeys[linha].Values[0]);
                disciplinas item = new disciplinas();
                item = (disciplinas)Session["disciplinas"];
                disciplinas_professores itemProfessores = new disciplinas_professores();
                itemProfessores.id_disciplina = item.id_disciplina;
                itemProfessores.id_professor = codigo;
                itemProfessores.tipo_professor = "tecnico";

                itemProfessores.responsavel = false;
                itemProfessores.status = "cadastrado";
                itemProfessores.data_cadastro = DateTime.Now;
                itemProfessores.data_alteracao = DateTime.Now;
                itemProfessores.usuario = usuario.usuario;

                DisciplinaAplicacao aplicacaoDisciplina = new DisciplinaAplicacao();
                aplicacaoDisciplina.IncluirProfessor_Tecnico_Disciplina(itemProfessores);
                item = aplicacaoDisciplina.BuscaItem(item);
                Session["disciplinas"] = item;
                PreencheTecnicoAdicionado(item.disciplinas_professores.ToList());
                bntPerquisaTecnico_Click(null, null);
            }
        }

    }
}