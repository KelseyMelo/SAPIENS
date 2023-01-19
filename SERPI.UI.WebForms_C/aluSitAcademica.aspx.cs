using Aplicacao_C;
using SERPI.Dominio_C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace SERPI.UI.WebForms_C
{
    public partial class aluSitAcademica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            //usuario = (usuarios)Session["UsuarioAlunoLogado"];
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 43) && !usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 47)) // 43 - Portal do Aluno e 47 - Dados Cadastrais - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                if ((string)Session["UsuarioClonado"] == "sim")
                {
                    divBotaoConfirmarMatricula.Visible = false;
                }

                GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
                List<Pais> listaPais = aplicacaoGerais.ListaPais();
                //var listaPais2 = from item2 in listaPais
                //                 select new
                //                 {
                //                     Id_Pais = item2.Id_Pais,
                //                     Nacionalidade = item2.Nacionalidade
                //                 };


                alunos item_aluno = (alunos)Session["AlunoLogado"];

                if (usuario.avatar != "")
                {
                    imgAluno.Src = "img/pessoas/" + usuario.avatar + "?" + DateTime.Now;
                }

                else
                {
                    imgAluno.Src = "img/pessoas/avatarunissex.jpg" + "?" + DateTime.Now;
                }

                lblNomeAluno.Text = item_aluno.nome;
                lblCargoAluno.Text = item_aluno.cargo;
                lblMatriculaAluno.Text = item_aluno.idaluno.ToString();
                lblCPFAluno.Text = item_aluno.cpf;

                string qTab = HttpContext.Current.Request["hQTab"];
                //Session[qTab + "Aluno"] = item_aluno;

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
                alunos item;
                //string qTab = HttpContext.Current.Request.Form["hQTab"];
                
                item = (alunos)Session["AlunoLogado"];

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
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(item.idaluno.ToString()), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("Nome"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(item.nome), font_Arialn_10_Normal));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode("CPF"), font_Arialn_10_Bold));
                objTable.AddCell(objCell);

                objCell = new Cell();
                objCell.Add(new Phrase(HttpContext.Current.Server.HtmlDecode(item.cpf), font_Arialn_10_Normal));
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
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/Histórico Escolar_" + item.idaluno.ToString() + ".pdf")));
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-danger');", true);
            }
        }

    }
}