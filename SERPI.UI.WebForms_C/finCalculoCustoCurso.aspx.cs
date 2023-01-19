using System;
using Aplicacao_C;
using SERPI.Dominio_C;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;
using System.Collections;
using iTextSharp.text.html.simpleparser;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace SERPI.UI.WebForms_C
{
    public partial class finCalculoCustoCurso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 48)) // 3. Cálculo de Custos - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                CursoAplicacao aplicacaoCurso = new CursoAplicacao();
                List<tipos_curso> listaTipoCurso = aplicacaoCurso.ListaTipoCurso();
                //var listaPais2 = from item2 in listaPais
                //                 select new
                //                 {
                //                     Id_Pais = item2.Id_Pais,
                //                     Nacionalidade = item2.Nacionalidade
                //                 };

                ddlTipoCursoCalculoCusto.Items.Clear();
                ddlTipoCursoCalculoCusto.DataSource = listaTipoCurso;
                ddlTipoCursoCalculoCusto.DataValueField = "id_tipo_curso";
                ddlTipoCursoCalculoCusto.DataTextField = "tipo_curso";
                ddlTipoCursoCalculoCusto.DataBind();
                ddlTipoCursoCalculoCusto.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos", ""));
                ddlTipoCursoCalculoCusto.SelectedValue = "";


                cursos item = new cursos();
                List<cursos> listaCurso = aplicacaoCurso.ListaItem(item);
                var lista = from item2 in listaCurso
                            select new
                            {
                                id_curso = item2.id_curso,
                                nome = item2.sigla + " - " + item2.nome
                            };

                ddlCursoCalculoCusto.Items.Clear();
                ddlCursoCalculoCusto.DataSource = lista.OrderBy(x => x.nome);
                ddlCursoCalculoCusto.DataValueField = "id_curso";
                ddlCursoCalculoCusto.DataTextField = "nome";
                ddlCursoCalculoCusto.DataBind();
                ddlCursoCalculoCusto.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os Cursos", ""));
                ddlCursoCalculoCusto.SelectedValue = "";

                ddlMesCalculoCusto.SelectedValue = DateTime.Today.Month.ToString();
                txtAnoCalculoCusto.Value = DateTime.Today.Year.ToString();

                divgrdHoraAula.Style["display"] = "none";
                divgrdBanca.Style["display"] = "none";
                divgrdOrientacao.Style["display"] = "none";

            }
            else
            {
                if (grdValorHoraAula.Rows.Count != 0)
                {
                    grdValorHoraAula.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        private void CarregarDados(string qIdCurso, int qMes, int qAno, string qOrdenacao)
        {
            GeraisAplicacao aplicacaoGeral = new GeraisAplicacao();
            List<forma_recebimento> lista_forma = aplicacaoGeral.ListaFormaRecebimento_HoraAula();

            FinanceiroAplicacao aplicacaoFinanceiro = new FinanceiroAplicacao();
            DateTime qData = Convert.ToDateTime("01/" + qMes + "/" + qAno);

            //===Custo Hora Aula ==
            List<geral_custo_hora_aula> lista_data_aula = aplicacaoFinanceiro.ListaCustoHoraAula(qIdCurso, qData, qOrdenacao);

            grdValorHoraAula.DataSource = lista_data_aula;
            grdValorHoraAula.DataBind();
            divgrdHoraAula.Style["display"] = "block";
            decimal dTotal = 0;
            
            if (lista_data_aula.Count > 0)
            {
                dTotal =  lista_data_aula.Where(x => x.col_Professor == "<strong>TOTAL:</strong>").FirstOrDefault().col_Total;

                grdValorHoraAula.UseAccessibleHeader = true;
                grdValorHoraAula.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultadosHoraAula.Visible = false;
                grdValorHoraAula.Visible = true;
            }
            else
            {
                //divgrdHoraAula.Visible = false;
                msgSemResultadosHoraAula.Visible = true;
            }


            //===Custo Hora Aula ==

            //===Custo Banca ==
            List<geral_custo_banca_orientacao> lista_banca = aplicacaoFinanceiro.ListaCustoBanca(qIdCurso, qData);

            grdValorBanca.DataSource = lista_banca;
            grdValorBanca.DataBind();
            divgrdBanca.Style["display"] = "block";

            if (lista_banca.Count > 0)
            {
                dTotal = dTotal + lista_banca.Where(x => x.col_Professor == "<strong>TOTAL:</strong>").FirstOrDefault().col_Total;

                grdValorBanca.UseAccessibleHeader = true;
                grdValorBanca.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultadosBanca.Visible = false;
                grdValorBanca.Visible = true;
                //divgrdBanca.Visible = true;
            }
            else
            {
                //divgrdBanca.Visible = false;
                msgSemResultadosBanca.Visible = true;
            }
            //===Custo Banca ==

            //===Custo Orientacao ==
            List<geral_custo_banca_orientacao> lista_orientacao = aplicacaoFinanceiro.ListaCustoOrientacao(qIdCurso, qData);

            grdValorOrientacao.DataSource = lista_orientacao;
            grdValorOrientacao.DataBind();
            divgrdOrientacao.Style["display"] = "block";

            if (lista_orientacao.Count > 0)
            {
                dTotal = dTotal + lista_orientacao.Where(x => x.col_Professor == "<strong>TOTAL:</strong>").FirstOrDefault().col_Total;

                grdValorOrientacao.UseAccessibleHeader = true;
                grdValorOrientacao.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultadosOrientacao.Visible = false;
                grdValorOrientacao.Visible = true;
                //divgrdBanca.Visible = true;
            }
            else
            {
                //divgrdBanca.Visible = false;
                msgSemResultadosOrientacao.Visible = true;
            }

            //===Custo Coordenação ==
            List<geral_custo_coordenacao> lista_coordenacao = aplicacaoFinanceiro.ListaCustoCoordenacao(qIdCurso,0, qData);

            grdCoordenacao.DataSource = lista_coordenacao;
            grdCoordenacao.DataBind();
            divgrdCoordenacao.Style["display"] = "block";

            if (lista_coordenacao.Count > 0)
            {
                dTotal = dTotal + lista_coordenacao.Where(x => x.col_Professor == "<strong>TOTAL:</strong>").FirstOrDefault().col_Total;

                grdCoordenacao.UseAccessibleHeader = true;
                grdCoordenacao.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultadosCoordenacao.Visible = false;
                grdCoordenacao.Visible = true;
                //divgrdBanca.Visible = true;
            }
            else
            {
                //divgrdBanca.Visible = false;
                msgSemResultadosCoordenacao.Visible = true;
            }

            //=================================================

            //===Custo TOTAL ==
            if (msgSemResultadosHoraAula.Visible && msgSemResultadosBanca.Visible && msgSemResultadosOrientacao.Visible && msgSemResultadosCoordenacao.Visible)
            {
                divgrdTotal.Style["display"] = "none";
                divSemValores.Style["display"] = "block";
            }
            else
            {
                divgrdTotal.Style["display"] = "block";
                divSemValores.Style["display"] = "none";
                lblMesAno.Text = ddlMesCalculoCusto.SelectedItem.Text + "/" + txtAnoCalculoCusto.Value.Trim() + ":";
                lblTotalCusto.Text = string.Format("{0:C}", dTotal) ;
                //    qMes = Convert.ToInt32(ddlMesCalculoCusto.SelectedValue);
                //qAno = Convert.ToInt32(txtAnoCalculoCusto.Value);
            }

            
        }

        

        public void ddlTipoCursoCalculoCusto_SelectedIndexChanged(Object sender, EventArgs e)
        {
            CursoAplicacao aplicacaoCurso = new CursoAplicacao();
            cursos item = new cursos();

            if (ddlTipoCursoCalculoCusto.SelectedValue != "")
            {
                item.id_tipo_curso = Convert.ToInt32(ddlTipoCursoCalculoCusto.SelectedValue);
            }

            List<cursos> listaCurso = aplicacaoCurso.ListaItem(item);

            var lista = from item2 in listaCurso
                        select new
                        {
                            id_curso = item2.id_curso,
                            nome = item2.sigla + " - " + item2.nome
                        };

            ddlCursoCalculoCusto.Items.Clear();
            ddlCursoCalculoCusto.DataSource = lista.OrderBy(x => x.nome);
            ddlCursoCalculoCusto.DataValueField = "id_curso";
            ddlCursoCalculoCusto.DataTextField = "nome";
            ddlCursoCalculoCusto.DataBind();
            ddlCursoCalculoCusto.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os Cursos", ""));
            ddlCursoCalculoCusto.SelectedValue = "";


            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fSelect2();", true);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("finCustosCursos.aspx", true);
        }

        protected void bntPerquisaCalculoCusto_Click(object sender, EventArgs e)
        {
            string qIdCurso = "";
            int qMes;
            int qAno;
            string qOrdenacao = "";

            if (txtAnoCalculoCusto.Value.Trim() == "")
            {
                lblMensagem.Text = "Deve-se digitar um ano.";
                lblTituloMensagem.Text = "Atenção";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                divgrdHoraAula.Style["display"] = "none";
                divgrdBanca.Style["display"] = "none";
                divgrdOrientacao.Style["display"] = "none";
                divgrdTotal.Style["display"] = "none";
                divSemValores.Style["display"] = "none";
                return;
            }

            var isNumeric = int.TryParse(txtAnoCalculoCusto.Value.Trim(), out qAno);

            if (!isNumeric)
            {
                lblMensagem.Text = "Deve-se digitar um ano numérico.";
                lblTituloMensagem.Text = "Atenção";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                divgrdHoraAula.Style["display"] = "none";
                divgrdBanca.Style["display"] = "none";
                divgrdOrientacao.Style["display"] = "none";
                divgrdTotal.Style["display"] = "none";
                divSemValores.Style["display"] = "none";
                return;
            }
            else if (qAno < 1900)
            {
                lblMensagem.Text = "Deve-se digitar um ano igual ou maior que 1900.";
                lblTituloMensagem.Text = "Atenção";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                divgrdHoraAula.Style["display"] = "none";
                divgrdBanca.Style["display"] = "none";
                divgrdOrientacao.Style["display"] = "none";
                divgrdTotal.Style["display"] = "none";
                divSemValores.Style["display"] = "none";
                return;
            }

            if (ddlCursoCalculoCusto.SelectedValue != "")
            {
                qIdCurso = ddlCursoCalculoCusto.SelectedValue;
            }
            else if (ddlTipoCursoCalculoCusto.SelectedValue == "")
            {
                qIdCurso = "";
            }
            else
            {
                for (int i = 1; i < ddlCursoCalculoCusto.Items.Count; i++)
                {
                    if (qIdCurso != "")
                    {
                        qIdCurso = qIdCurso + ",";
                    }
                    qIdCurso = qIdCurso + ddlCursoCalculoCusto.Items[i].Value;
                }
                //foreach (var elemento in ddlCursoCalculoCusto.Items.Count)
                //{
                //    qIdCurso = qIdCurso + elemento.
                //}
            }

            qMes = Convert.ToInt32(ddlMesCalculoCusto.SelectedValue);

            if (optOrdenarProfessor.Checked)
            {
                qOrdenacao = "professor";
            }
            else
            {
                qOrdenacao = "empresa";
            }
            

            CarregarDados(qIdCurso, qMes, qAno, qOrdenacao);

        }

        public class PDF_Cabec_Rodape_GeraEmentaPDF : PdfPageEventHelper
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
                p.Add(new Chunk(" \r\n", font_Verdana_8_Normal));
                //p.Add(new Chunk("MESTRADO PROFISSIONAL  \r\n", font_Verdana_8_Normal));
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
                p.Alignment = Element.ALIGN_RIGHT;
                //p.Add(new Chunk("Instituto de Pesquisas Tecnológicas do Estado de São Paulo S/A - IPT \r\n", font_Verdana_8_Italic));
                //p.Add(new Chunk("Av. Prof. Almeida Prado, 532 - Cidade Universitária - Butantã - 05508-901 - São Paulo - SP  \r\n", font_Verdana_8_Italic));
                //p.Add(new Chunk("Caixa Postal 0141 - 01064-970 - São Paulo - SP - Tel.: (11)3767-4084, 3767-4624 - página " + qPagina.ToString() + "\r\n", font_Verdana_8_Italic));
                p.Add(new Chunk("\n\n Impresso em " + String.Format("{0:dd/MM/yyyy}", DateTime.Today) + " - Página " + qPagina.ToString() + "\r\n", font_Verdana_8_Italic));
                cell = new PdfPCell(p);
                qPagina++;
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

        protected void btnImprimirCustos_Click(object sender, EventArgs e)
        {
            try
            {

                GeraCustoPDF();

                if (File.Exists(Server.MapPath("~/doctos/PagamentoDocentes_" + ddlMesCalculoCusto.SelectedValue + "-" + txtAnoCalculoCusto.Value.Trim() + ".pdf")))
                {
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/PagamentoDocentes_" + ddlMesCalculoCusto.SelectedValue + "-" + txtAnoCalculoCusto.Value.Trim() + ".pdf")));
                    Response.WriteFile(Server.MapPath("~/doctos/PagamentoDocentes_" + ddlMesCalculoCusto.SelectedValue + "-" + txtAnoCalculoCusto.Value.Trim() + ".pdf"));
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão de Relatório Pagamento de Docentes";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }
        }

        protected void GeraCustoPDF()
        {
            try
            {
                FinanceiroAplicacao aplicacaoFinanceiro = new FinanceiroAplicacao();

                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(45, 40, 120, 40);//estibulando o espaçamento das margens que queremos ===Antigo===(45, 40, 40, 40)
                doc.AddCreationDate();//adicionando as configuracoes

                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/doctos/PagamentoDocentes_" + ddlMesCalculoCusto.SelectedValue + "-" + txtAnoCalculoCusto.Value.Trim() + ".pdf"), FileMode.Create));
                PDF_Cabec_Rodape_GeraEmentaPDF pageHeaderFooter = new PDF_Cabec_Rodape_GeraEmentaPDF();
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
                //string sMinutos;

                //Aqui é uma nova tabela
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 230f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("RELATÓRIO PAGAMENTO DE DOCENTES", font_Verdana_14_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui se desenha uma linha fina
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(linefine));
                doc.Add(p);



                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(4);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 15f, 35f, 15f, 15f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Tipo Curso\n\n", font_Verdana_9_Bold));
                p.Add(new Chunk(ddlTipoCursoCalculoCusto.SelectedItem.Text, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Curso\n\n", font_Verdana_9_Bold));
                p.Add(new Chunk(ddlCursoCalculoCusto.SelectedItem.Text, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Mês/Ano\n\n", font_Verdana_9_Bold));
                p.Add(new Chunk(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dtfi.GetMonthName(Convert.ToInt32(ddlMesCalculoCusto.SelectedValue)).ToLower()) + "/" + txtAnoCalculoCusto.Value, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Ordenado por\n\n", font_Verdana_9_Bold));
                if (optOrdenarProfessor.Checked)
                {
                    p.Add(new Chunk("nome professor", font_Verdana_9_Normal));
                }
                else
                {
                    p.Add(new Chunk("nome empresa", font_Verdana_9_Normal));
                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);
                doc.Add(table);

                decimal dTotal = 0;
                ArrayList htmlarraylist;
                string sAux = "";
                int qIdOferecimento;
                string qIdCurso ="";
                //string qData = "01/" + ddlMesCalculoCusto.SelectedValue + "/" + txtAnoCalculoCusto.Value;
                DateTime qData = Convert.ToDateTime("01/" + ddlMesCalculoCusto.SelectedValue + "/" + txtAnoCalculoCusto.Value);
                string qOrdenacao;

                if (ddlCursoCalculoCusto.SelectedValue != "")
                {
                    qIdCurso = ddlCursoCalculoCusto.SelectedValue;
                }
                else if (ddlTipoCursoCalculoCusto.SelectedValue == "")
                {
                    qIdCurso = "";
                }
                else
                {
                    for (int i = 1; i < ddlCursoCalculoCusto.Items.Count; i++)
                    {
                        if (qIdCurso != "")
                        {
                            qIdCurso = qIdCurso + ",";
                        }
                        qIdCurso = qIdCurso + ddlCursoCalculoCusto.Items[i].Value;
                    }

                }

                if (optOrdenarProfessor.Checked)
                {
                    qOrdenacao = "professor";
                }
                else
                {
                    qOrdenacao = "empresa";
                }

                //Cabeçalho da Tabela-CUSTO HORA-AULA-Início =================
                //Aqui é uma nova tabela de 1 Colunas ========================================================
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 102f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("CUSTOS HORA-AULA", font_Verdana_10_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderColor = FontColor_Cinza;
                cell.BackgroundColor = FontColor_Cinza;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);
                doc.Add(table);

                List<geral_detalhe_hora_aula> lista_detalhe_hora_aula;

                //===Custo Hora Aula ==
                List<geral_custo_hora_aula> lista_data_aula = aplicacaoFinanceiro.ListaCustoHoraAula(qIdCurso, qData, qOrdenacao);

                if (lista_data_aula.Count != 0)
                {
                    //Aqui é uma nova tabela de 5 Colunas ========================================================
                    table = new PdfPTable(5);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 55f, 23f, 7f, 7f, 10f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Professor", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Curso", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 3
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Total Horas", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 4
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Valor Hora", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 5
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Total", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Cabeçalho da Tabela--Fim =================

                    int i = 0;

                    dTotal = dTotal + lista_data_aula.Where(x => x.col_Professor == "<strong>TOTAL:</strong>").FirstOrDefault().col_Total;

                    foreach (var elemento in lista_data_aula)
                    {
                        i++;

                        //Aqui se desenha a Coluna 1
                        cell = new PdfPCell();
                        p = new Paragraph();
                        htmlarraylist = HTMLWorker.ParseToList(new StringReader("<span style='font-size:7px'>" + elemento.col_Professor + "</span>"), null);
                        for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                        {
                            p.Add(htmlarraylist[k]);
                        }

                        //sAux = "";
                        //sAux = elemento.col_Professor.Replace("<strong>", "");
                        //sAux = sAux.Replace("</strong>", "");
                        //sAux = sAux.Replace("<br>", "\n");

                        //if (elemento.col_Professor != "<strong>TOTAL:</strong>")
                        //{
                        //    p.Add(new Chunk(sAux, font_Arialn_8_Normal));
                        //}
                        //else
                        //{
                        //    p.Add(new Chunk(sAux, font_Arialn_8_Bold));
                        //}

                        lista_detalhe_hora_aula = aplicacaoFinanceiro.ListaDetalheHoraAula(elemento.id_curso, elemento.id_professor, qData.Year.ToString(), qData.Month.ToString());
                        sAux = "";
                        qIdOferecimento = 0;
                        int qQuebraLinha = 0;
                        foreach (var elemento2 in lista_detalhe_hora_aula)
                        {
                            var timeSpan = TimeSpan.FromHours(Convert.ToDouble(elemento2.hora_aula));
                            int hh = timeSpan.Hours;
                            int mm = timeSpan.Minutes;
                            int ss = timeSpan.Seconds;

                            var hours = Math.Floor(elemento2.hora_aula);
                            //var mins = 60 * (elemento2.hora_aula - hours);

                            if (hh == 0 && hours != 0)
                            {
                                hh = Convert.ToInt32(hours);
                            }
                            //switch ((elemento2.hora_aula - Math.Truncate(elemento2.hora_aula)).ToString())
                            //{
                            //    case "0,08":
                            //        sMinutos = "05";
                            //        break;
                            //    case "0,17":
                            //        sMinutos = "10";
                            //        break;
                            //    case "0,25":
                            //        sMinutos = "15";
                            //        break;
                            //    case "0,33":
                            //        sMinutos = "20";
                            //        break;
                            //    case "0,42":
                            //        sMinutos = "25";
                            //        break;
                            //    case "0,50":
                            //        sMinutos = "30";
                            //        break;
                            //    case "0,58":
                            //        sMinutos = "35";
                            //        break;
                            //    case "0,67":
                            //        sMinutos = "40";
                            //        break;
                            //    case "0,75":
                            //        sMinutos = "45";
                            //        break;
                            //    case "0,83":
                            //        sMinutos = "50";
                            //        break;
                            //    case "0,92":
                            //        sMinutos = "55";
                            //        break;
                            //    default:
                            //        sMinutos = "00";
                            //        break;
                            //}

                            if (qIdOferecimento == 0 || qIdOferecimento != elemento2.id_oferecimento)
                            {
                                if (sAux != "")
                                {
                                    sAux = sAux + "<br/>";
                                }
                                sAux = sAux + "&nbsp;&nbsp;&nbsp;" + elemento2.codigo_disciplina + "&nbsp;&nbsp;&nbsp;" + elemento2.periodo;
                                qQuebraLinha = 0;
                            }
                            if (qQuebraLinha == 5)
                            {
                                sAux = sAux + "<br/>";
                                qQuebraLinha = -1;
                            }
                            //sAux = sAux + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>" + Convert.ToDateTime(elemento2.data_aula).Day + "</strong>&nbsp;&nbsp;" + Math.Truncate(elemento2.hora_aula).ToString("00") + ":" + sMinutos;
                            sAux = sAux + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>" + Convert.ToDateTime(elemento2.data_aula).Day + "</strong>&nbsp;&nbsp;" + hh.ToString("00") + ":" + mm.ToString("00");
                            qIdOferecimento = elemento2.id_oferecimento;
                            qQuebraLinha++;
                        }

                        htmlarraylist = HTMLWorker.ParseToList(new StringReader("<span style='font-size:7px'>" + sAux + "</span>"), null);
                        for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                        {
                            p.Add(htmlarraylist[k]);
                        }

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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 2
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(elemento.NomeCurso, font_Arialn_8_Normal));
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 3
                        cell = new PdfPCell();
                        p = new Paragraph();
                        if (elemento.col_Professor != "<strong>TOTAL:</strong>")
                        {

                            var timeSpan = TimeSpan.FromHours(Convert.ToDouble(elemento.col_TotalHoras));
                            int hh = timeSpan.Hours;
                            int mm = timeSpan.Minutes;
                            int ss = timeSpan.Seconds;

                            var hours = Math.Floor(elemento.col_TotalHoras);
                            //var mins = 60 * (elemento.col_TotalHoras - hours);

                            if (hh == 0 && hours != 0)
                            {
                                hh = Convert.ToInt32(hours);
                            }

                            //rever horas
                            hh = Convert.ToInt32(hours);

                            //switch ((elemento.col_TotalHoras - Math.Truncate(elemento.col_TotalHoras)).ToString())
                            //{
                            //    case "0,08":
                            //        sMinutos = "05";
                            //        break;
                            //    case "0,17":
                            //        sMinutos = "10";
                            //        break;
                            //    case "0,25":
                            //        sMinutos = "15";
                            //        break;
                            //    case "0,33":
                            //        sMinutos = "20";
                            //        break;
                            //    case "0,42":
                            //        sMinutos = "25";
                            //        break;
                            //    case "0,50":
                            //        sMinutos = "30";
                            //        break;
                            //    case "0,58":
                            //        sMinutos = "35";
                            //        break;
                            //    case "0,67":
                            //        sMinutos = "40";
                            //        break;
                            //    case "0,75":
                            //        sMinutos = "45";
                            //        break;
                            //    case "0,83":
                            //        sMinutos = "50";
                            //        break;
                            //    case "0,92":
                            //        sMinutos = "55";
                            //        break;
                            //    default:
                            //        sMinutos = "00";
                            //        break;
                            //}
                            //p.Add(new Chunk(Math.Truncate(elemento.col_TotalHoras).ToString("00") + ":" + sMinutos, font_Arialn_8_Normal));
                            p.Add(new Chunk(hh.ToString("00") + ":" + mm.ToString("00"), font_Arialn_8_Normal));
                        }
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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 4
                        cell = new PdfPCell();
                        p = new Paragraph();
                        if (elemento.col_Professor != "<strong>TOTAL:</strong>")
                        {
                            p.Add(new Chunk(string.Format("{0:C}", elemento.valor_hora), font_Arialn_8_Normal));
                        }
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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 5
                        cell = new PdfPCell();
                        p = new Paragraph();
                        if (elemento.col_Professor != "<strong>TOTAL:</strong>")
                        {
                            p.Add(new Chunk(string.Format("{0:C}", elemento.col_Total), font_Arialn_8_Normal));
                        }
                        else
                        {
                            p.Add(new Chunk(string.Format("{0:C}", elemento.col_Total), font_Arialn_8_Bold));
                        }

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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);
                    }
                }
                else
                {
                    table = new PdfPTable(1);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 102f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Nenhum resultado encontrado", font_Verdana_10_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    //cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);
                    //doc.Add(table);
                }
                doc.Add(table);


                //Cabeçalho da Tabela-CUSTO BANCA-Início =================
                //Aqui é uma nova tabela de 1 Colunas ========================================================
                doc.NewPage();
                //Aqui é uma nova tabela
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 230f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("RELATÓRIO PAGAMENTO DE DOCENTES", font_Verdana_14_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui se desenha uma linha fina
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(linefine));
                doc.Add(p);



                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(4);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 15f, 35f, 15f, 15f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Tipo Curso\n\n", font_Verdana_9_Bold));
                p.Add(new Chunk(ddlTipoCursoCalculoCusto.SelectedItem.Text, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Curso\n\n", font_Verdana_9_Bold));
                p.Add(new Chunk(ddlCursoCalculoCusto.SelectedItem.Text, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Mês/Ano\n\n", font_Verdana_9_Bold));
                p.Add(new Chunk(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dtfi.GetMonthName(Convert.ToInt32(ddlMesCalculoCusto.SelectedValue)).ToLower()) + "/" + txtAnoCalculoCusto.Value, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("\n\n", font_Verdana_9_Bold));
                if (optOrdenarProfessor.Checked)
                {
                    p.Add(new Chunk("", font_Verdana_9_Normal));
                }
                else
                {
                    p.Add(new Chunk("", font_Verdana_9_Normal));
                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);
                doc.Add(table);
                //==================


                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 102f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("CUSTOS BANCA", font_Verdana_10_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderColor = FontColor_Cinza;
                cell.BackgroundColor = FontColor_Cinza;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);
                doc.Add(table);

                //===Custo Banca ==
                List<geral_custo_banca_orientacao> lista_custo_banca = aplicacaoFinanceiro.ListaCustoBanca(qIdCurso, qData);

                if (lista_custo_banca.Count != 0)
                {
                    dTotal = dTotal + lista_custo_banca.Where(x => x.col_Professor == "<strong>TOTAL:</strong>").FirstOrDefault().col_Total;

                    //Aqui é uma nova tabela de 5 Colunas ========================================================
                    table = new PdfPTable(6);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 30f, 12f, 10f, 10f, 30f, 10f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Professor", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Forma Recebimento", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 3
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Tipo Banca", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 4
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Data/Hora", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 5
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Aluno", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 6
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Total", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Cabeçalho da Tabela--Fim =================

                    int i = 0;

                    foreach (var elemento in lista_custo_banca)
                    {
                        i++;

                        //Aqui se desenha a Coluna 1
                        cell = new PdfPCell();
                        p = new Paragraph();
                        htmlarraylist = HTMLWorker.ParseToList(new StringReader("<span style='font-size:7px'>" + elemento.col_Professor + "</span>"), null);
                        for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                        {
                            p.Add(htmlarraylist[k]);
                        }

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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 2
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(elemento.col_FormaRecebimento, font_Arialn_8_Normal));
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 3
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(elemento.col_TipoBanca, font_Arialn_8_Normal));
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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 4
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(string.Format("{0:C}", elemento.col_DataHora), font_Arialn_8_Normal));
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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 5
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(string.Format("{0:C}", elemento.col_Aluno), font_Arialn_8_Normal));
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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 6
                        cell = new PdfPCell();
                        p = new Paragraph();
                        if (elemento.col_Professor != "<strong>TOTAL:</strong>")
                        {
                            p.Add(new Chunk(string.Format("{0:C}", elemento.col_Total), font_Arialn_8_Normal));
                        }
                        else
                        {
                            p.Add(new Chunk(string.Format("{0:C}", elemento.col_Total), font_Arialn_8_Bold));
                        }

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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);
                    }
                }
                else
                {
                    table = new PdfPTable(1);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 102f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Nenhum resultado encontrado", font_Verdana_10_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    //cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);
                    //doc.Add(table);
                }
                doc.Add(table);

                //Cabeçalho da Tabela-CUSTO BANCA-Início =================
                //Aqui é uma nova tabela de 1 Colunas ========================================================
                doc.NewPage();
                //Aqui é uma nova tabela
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 230f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("RELATÓRIO PAGAMENTO DE DOCENTES", font_Verdana_14_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui se desenha uma linha fina
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(linefine));
                doc.Add(p);



                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(4);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 15f, 35f, 15f, 15f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Tipo Curso\n\n", font_Verdana_9_Bold));
                p.Add(new Chunk(ddlTipoCursoCalculoCusto.SelectedItem.Text, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Curso\n\n", font_Verdana_9_Bold));
                p.Add(new Chunk(ddlCursoCalculoCusto.SelectedItem.Text, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Mês/Ano\n\n", font_Verdana_9_Bold));
                p.Add(new Chunk(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dtfi.GetMonthName(Convert.ToInt32(ddlMesCalculoCusto.SelectedValue)).ToLower()) + "/" + txtAnoCalculoCusto.Value, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("\n\n", font_Verdana_9_Bold));
                if (optOrdenarProfessor.Checked)
                {
                    p.Add(new Chunk("", font_Verdana_9_Normal));
                }
                else
                {
                    p.Add(new Chunk("", font_Verdana_9_Normal));
                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);
                doc.Add(table);
                //==================

                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 102f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("CUSTOS ORIENTAÇÃO", font_Verdana_10_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderColor = FontColor_Cinza;
                cell.BackgroundColor = FontColor_Cinza;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);
                doc.Add(table);

                //===Custo Orientação ==
                List<geral_custo_banca_orientacao> lista_custo_orientacao = aplicacaoFinanceiro.ListaCustoOrientacao(qIdCurso, qData);

                if (lista_custo_orientacao.Count != 0)
                {
                    dTotal = dTotal + lista_custo_orientacao.Where(x => x.col_Professor == "<strong>TOTAL:</strong>").FirstOrDefault().col_Total;

                    //Aqui é uma nova tabela de 7 Colunas ========================================================
                    table = new PdfPTable(6);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 25f, 24f, 9f, 9f, 25f, 10f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Professor", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Curso", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 3
                    //cell = new PdfPCell();
                    //p = new Paragraph();
                    //p.Add(new Chunk("Empresa PJ", font_Verdana_8_Bold));
                    //cell = new PdfPCell(p);
                    //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    //cell.Border = Rectangle.BOX;
                    //cell.BorderColor = FontColor_Cinza;
                    //cell.BackgroundColor = FontColor_Cinza;
                    //cell.PaddingBottom = 12f;
                    //table.AddCell(cell);

                    //Aqui se desenha a Coluna 3
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Tipo Banca", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 4
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Data/Hora", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 5
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Aluno", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 6
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Total", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Cabeçalho da Tabela--Fim =================

                    int i = 0;

                    foreach (var elemento in lista_custo_orientacao)
                    {
                        i++;

                        //Aqui se desenha a Coluna 1
                        cell = new PdfPCell();
                        p = new Paragraph();
                        htmlarraylist = HTMLWorker.ParseToList(new StringReader("<span style='font-size:7px'>" + elemento.col_Professor + "</span>"), null);
                        for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                        {
                            p.Add(htmlarraylist[k]);
                        }

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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 2
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(elemento.col_Curso, font_Arialn_8_Normal));
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 3
                        //cell = new PdfPCell();
                        //p = new Paragraph();
                        //p.Add(new Chunk(elemento.col_Empresa, font_Arialn_8_Normal));
                        //cell = new PdfPCell(p);
                        //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        //cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        //cell.Border = Rectangle.BOX;
                        //if (i % 2 != 0)
                        //{
                        //    cell.BorderColor = Color.WHITE;
                        //    cell.BackgroundColor = Color.WHITE;
                        //}
                        //else
                        //{
                        //    cell.BorderColor = FontColor_CinzaClaro;
                        //    cell.BackgroundColor = FontColor_CinzaClaro;
                        //}
                        //cell.PaddingBottom = 12f;
                        //table.AddCell(cell);

                        //Aqui se desenha a Coluna 4
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(elemento.col_TipoBanca, font_Arialn_8_Normal));
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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 5
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(string.Format("{0:C}", elemento.col_DataHora), font_Arialn_8_Normal));
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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 6
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(string.Format("{0:C}", elemento.col_Aluno), font_Arialn_8_Normal));
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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 7
                        cell = new PdfPCell();
                        p = new Paragraph();
                        if (elemento.col_Professor != "<strong>TOTAL:</strong>")
                        {
                            p.Add(new Chunk(string.Format("{0:C}", elemento.col_Total), font_Arialn_8_Normal));
                        }
                        else
                        {
                            p.Add(new Chunk(string.Format("{0:C}", elemento.col_Total), font_Arialn_8_Bold));
                        }

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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);
                    }
                }
                else
                {
                    table = new PdfPTable(1);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 102f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Nenhum resultado encontrado", font_Verdana_10_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    //cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);
                    //doc.Add(table);
                }
                doc.Add(table);

                //==Final====

                //Cabeçalho da Tabela-CUSTO COORDENAÇÃO-Início =================
                //Aqui é uma nova tabela de 1 Colunas ========================================================
                doc.NewPage();
                //Aqui é uma nova tabela
                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;

                widths = new float[] { 230f };
                table.SetWidths(widths);

                cell = new PdfPCell();
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Add(new Chunk("RELATÓRIO PAGAMENTO DE DOCENTES", font_Verdana_14_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                //cell.FixedHeight = 25f;
                table.AddCell(cell);

                doc.Add(table);

                //Aqui se desenha uma linha fina
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(linefine));
                doc.Add(p);



                //Aqui é uma nova tabela de 4 Colunas ========================================================
                table = new PdfPTable(4);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 15f, 35f, 15f, 15f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Tipo Curso\n\n", font_Verdana_9_Bold));
                p.Add(new Chunk(ddlTipoCursoCalculoCusto.SelectedItem.Text, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 2
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Curso\n\n", font_Verdana_9_Bold));
                p.Add(new Chunk(ddlCursoCalculoCusto.SelectedItem.Text, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 3
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("Mês/Ano\n\n", font_Verdana_9_Bold));
                p.Add(new Chunk(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dtfi.GetMonthName(Convert.ToInt32(ddlMesCalculoCusto.SelectedValue)).ToLower()) + "/" + txtAnoCalculoCusto.Value, font_Verdana_9_Normal));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);

                //Aqui se desenha a Coluna 4
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("\n\n", font_Verdana_9_Bold));
                if (optOrdenarProfessor.Checked)
                {
                    p.Add(new Chunk("", font_Verdana_9_Normal));
                }
                else
                {
                    p.Add(new Chunk("", font_Verdana_9_Normal));
                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);
                doc.Add(table);
                //==================

                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 102f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                p.Add(new Chunk("CUSTOS COORDENAÇÃO", font_Verdana_10_Bold));
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderColor = FontColor_Cinza;
                cell.BackgroundColor = FontColor_Cinza;
                cell.PaddingBottom = 12f;
                table.AddCell(cell);
                doc.Add(table);

                //===Custo Orientação ==
                List<geral_custo_coordenacao> lista_custo_coordenacao = aplicacaoFinanceiro.ListaCustoCoordenacao(qIdCurso, 0, qData);

                if (lista_custo_coordenacao.Count != 0)
                {
                    dTotal = dTotal + lista_custo_coordenacao.Where(x => x.col_Professor == "<strong>TOTAL:</strong>").FirstOrDefault().col_Total;

                    //Aqui é uma nova tabela de 7 Colunas ========================================================
                    table = new PdfPTable(5);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 27f, 27f, 12f, 27f, 10f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Professor", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 2
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Curso", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 3
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Tipo Coordenação", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 4
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Turma(s) Aberta(s)", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Aqui se desenha a Coluna 5
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Total", font_Verdana_8_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);

                    //Cabeçalho da Tabela--Fim =================

                    int i = 0;

                    foreach (var elemento in lista_custo_coordenacao)
                    {
                        i++;

                        //Aqui se desenha a Coluna 1
                        cell = new PdfPCell();
                        p = new Paragraph();
                        htmlarraylist = HTMLWorker.ParseToList(new StringReader("<span style='font-size:7px'>" + elemento.col_Professor + "</span>"), null);
                        for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                        {
                            p.Add(htmlarraylist[k]);
                        }

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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 2
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(elemento.col_Curso, font_Arialn_8_Normal));
                        cell = new PdfPCell(p);
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 3
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(elemento.col_TipoCoordenacao, font_Arialn_8_Normal));
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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);

                        //Aqui se desenha a Coluna 4
                        cell = new PdfPCell();
                        p = new Paragraph();
                        p.Add(new Chunk(elemento.col_Turma, font_Arialn_8_Normal));
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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);


                        //Aqui se desenha a Coluna 5
                        cell = new PdfPCell();
                        p = new Paragraph();
                        if (elemento.col_Professor != "<strong>TOTAL:</strong>")
                        {
                            p.Add(new Chunk(string.Format("{0:C}", elemento.col_Total), font_Arialn_8_Normal));
                        }
                        else
                        {
                            p.Add(new Chunk(string.Format("{0:C}", elemento.col_Total), font_Arialn_8_Bold));
                        }

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
                        cell.PaddingBottom = 12f;
                        table.AddCell(cell);
                    }
                }
                else
                {
                    table = new PdfPTable(1);
                    table.TotalWidth = 520f;
                    table.LockedWidth = true;
                    widths = new float[] { 102f };
                    table.SetWidths(widths);

                    //Aqui se desenha a Coluna 1
                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Add(new Chunk("Nenhum resultado encontrado", font_Verdana_10_Bold));
                    cell = new PdfPCell(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = FontColor_Cinza;
                    //cell.BackgroundColor = FontColor_Cinza;
                    cell.PaddingBottom = 12f;
                    table.AddCell(cell);
                    //doc.Add(table);
                }
                doc.Add(table);

                //==Final====

                //Aqui começa o Total Geral
                //Aqui se desenha uma linha fina
                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk("\n\n\n"));
                doc.Add(p);

                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk(linefine));
                doc.Add(p);

                p = new Paragraph();
                p.Alignment = Element.ALIGN_CENTER;
                p.Clear();
                p.Add(new Chunk("\n"));
                doc.Add(p);

                //===============

                table = new PdfPTable(1);
                table.TotalWidth = 520f;
                table.LockedWidth = true;
                widths = new float[] { 102f };
                table.SetWidths(widths);

                //Aqui se desenha a Coluna 1
                cell = new PdfPCell();
                p = new Paragraph();
                if (dTotal != 0)
                {
                    p.Add(new Chunk("Total Custos para o mês de ", font_Verdana_10_Normal));
                    p.Add(new Chunk(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dtfi.GetMonthName(Convert.ToInt32(ddlMesCalculoCusto.SelectedValue)).ToLower()) + "/" + txtAnoCalculoCusto.Value + "  ", font_Verdana_10_Bold));
                    p.Add(new Chunk(string.Format("{0:C}", dTotal), font_Verdana_10_Bold));
                }
                else
                {
                    p.Add(new Chunk("Não há movimentação financeira para o período solicitado. ", font_Verdana_10_Normal));

                }
                cell = new PdfPCell(p);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Border = Rectangle.BOX;
                cell.BorderColor = FontColor_Cinza;
                //cell.BackgroundColor = FontColor_Cinza;
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
                lblMensagem.Text = "Erro na rotina que gera o relatório de Relatório Pagamento de Docentes";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }

        }

        protected void btnImprimirCustosExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string sMesReferencia = ddlMesCalculoCusto.SelectedItem.Text.ToUpper() + "-" + txtAnoCalculoCusto.Value;

                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet(sMesReferencia);
                sheet.DisplayGridlines = true;



                //==Fontes
                IFont font_10_Negrito = workbook.CreateFont();
                font_10_Negrito.IsBold = true;
                font_10_Negrito.FontHeightInPoints = 10;

                IFont font_10_Negrito_Azul = workbook.CreateFont();
                font_10_Negrito_Azul.Color = IndexedColors.LightBlue.Index;
                font_10_Negrito_Azul.IsBold = true;
                font_10_Negrito_Azul.FontHeightInPoints = 10;

                IFont font_10_Negrito_Verde = workbook.CreateFont();
                font_10_Negrito_Verde.Color = IndexedColors.SeaGreen.Index;
                font_10_Negrito_Verde.IsBold = true;
                font_10_Negrito_Verde.FontHeightInPoints = 10;

                IFont font_10_Normal = workbook.CreateFont();
                font_10_Normal.IsBold = false;
                font_10_Normal.FontHeightInPoints = 10;
                //==Fontes

                //==Styles
                ICellStyle style_10_Negrito_Center = workbook.CreateCellStyle();
                style_10_Negrito_Center.Alignment = HorizontalAlignment.Center;
                style_10_Negrito_Center.VerticalAlignment = VerticalAlignment.Center;
                style_10_Negrito_Center.SetFont(font_10_Negrito);

                ICellStyle style_10_Negrito_Esquerda = workbook.CreateCellStyle();
                style_10_Negrito_Esquerda.Alignment = HorizontalAlignment.Left;
                style_10_Negrito_Esquerda.VerticalAlignment = VerticalAlignment.Center;
                style_10_Negrito_Esquerda.SetFont(font_10_Negrito);

                ICellStyle style_10_Normal_Center = workbook.CreateCellStyle();
                style_10_Normal_Center.Alignment = HorizontalAlignment.Center;
                style_10_Normal_Center.VerticalAlignment = VerticalAlignment.Center;
                style_10_Normal_Center.SetFont(font_10_Normal);

                ICellStyle style_10_Normal_Esquerda = workbook.CreateCellStyle();
                style_10_Normal_Esquerda.Alignment = HorizontalAlignment.Left;
                style_10_Normal_Esquerda.VerticalAlignment = VerticalAlignment.Center;
                style_10_Normal_Esquerda.SetFont(font_10_Normal);

                ICellStyle style_10_Normal_Direita = workbook.CreateCellStyle();
                style_10_Normal_Direita.Alignment = HorizontalAlignment.Right;
                style_10_Normal_Direita.VerticalAlignment = VerticalAlignment.Center;
                style_10_Normal_Direita.SetFont(font_10_Normal);

                ICellStyle style_10_Negrito_Direita = workbook.CreateCellStyle();
                style_10_Negrito_Direita.Alignment = HorizontalAlignment.Right;
                style_10_Negrito_Direita.VerticalAlignment = VerticalAlignment.Center;
                style_10_Negrito_Direita.SetFont(font_10_Negrito);

                ICellStyle style_10_Negrito_Direita_Azul = workbook.CreateCellStyle();
                style_10_Negrito_Direita_Azul.Alignment = HorizontalAlignment.Right;
                style_10_Negrito_Direita_Azul.VerticalAlignment = VerticalAlignment.Center;
                style_10_Negrito_Direita_Azul.SetFont(font_10_Negrito_Azul);

                ICellStyle style_10_Negrito_Direita_Verde = workbook.CreateCellStyle();
                style_10_Negrito_Direita_Verde.Alignment = HorizontalAlignment.Right;
                style_10_Negrito_Direita_Verde.VerticalAlignment = VerticalAlignment.Center;
                style_10_Negrito_Direita_Verde.SetFont(font_10_Negrito_Verde);

                ICellStyle style_10_Negrito_Center_Rosa = workbook.CreateCellStyle();
                style_10_Negrito_Center_Rosa.FillPattern = FillPattern.SolidForeground;
                style_10_Negrito_Center_Rosa.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Rose.Index;
                style_10_Negrito_Center_Rosa.Alignment = HorizontalAlignment.Center;
                style_10_Negrito_Center_Rosa.VerticalAlignment = VerticalAlignment.Center;
                style_10_Negrito_Center_Rosa.SetFont(font_10_Negrito);


                //==Styles



                //Linha 1 - Inicio
                int iRowNumer = 0;
                IRow row = sheet.CreateRow(iRowNumer);
                ICell cell;

                cell = row.CreateCell(0);
                cell.SetCellValue("CUSTOS DOS DOCENTES - " + sMesReferencia );
                cell.CellStyle = style_10_Negrito_Center;
                cell.CellStyle.WrapText = false;

                var cra = new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 9);
                sheet.AddMergedRegion(cra);

                //==== AULAS - INÍCIO ========================

                //Linha 2 - Inicio
                iRowNumer = 1;
                row = sheet.CreateRow(iRowNumer);

                cell = row.CreateCell(0);
                cell.SetCellValue("AULAS");
                cell.CellStyle = style_10_Negrito_Esquerda;
                cell.CellStyle.WrapText = false;

                //Linha 3 - Inicio
                iRowNumer = 2;
                row = sheet.CreateRow(iRowNumer);

                cell = row.CreateCell(0);
                cell.SetCellValue("ORD");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                sheet.SetColumnWidth(0, 1500);

                cell = row.CreateCell(1);
                cell.SetCellValue("PROFESSOR");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;
                sheet.SetColumnWidth(1, 10000);

                cell = row.CreateCell(2);
                cell.SetCellValue("CURSO");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;
                sheet.SetColumnWidth(2, 10000);

                cell = row.CreateCell(3);
                cell.SetCellValue("VALOR");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;
                sheet.SetColumnWidth(3, 4000);

                cell = row.CreateCell(4);
                cell.SetCellValue("VALOR PAGO");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;
                sheet.SetColumnWidth(4, 4000);

                cell = row.CreateCell(5);
                cell.SetCellValue("DATA PAGTO");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;
                sheet.SetColumnWidth(5, 4000);

                cell = row.CreateCell(6);
                cell.SetCellValue("SALDO");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;
                sheet.SetColumnWidth(6, 4000);

                cell = row.CreateCell(7);
                cell.SetCellValue("EMPRESA/NF/DATA");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;
                sheet.SetColumnWidth(7, 10000);

                cell = row.CreateCell(8);
                cell.SetCellValue("NF FORA DA CIDADE E DO ESTADO");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;
                sheet.SetColumnWidth(8, 10000);

                cell = row.CreateCell(9);
                cell.SetCellValue("BAIXA NO SAPIENS");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;
                sheet.SetColumnWidth(9, 10000);

                //Linha 1 - FIM ===========================

                string qIdCurso = "";
                string qOrdenacao = "";
                DateTime qData = Convert.ToDateTime("01/" + ddlMesCalculoCusto.SelectedValue + "/" + txtAnoCalculoCusto.Value);
                decimal dTotal = 0;

                if (ddlCursoCalculoCusto.SelectedValue != "")
                {
                    qIdCurso = ddlCursoCalculoCusto.SelectedValue;
                }
                else if (ddlTipoCursoCalculoCusto.SelectedValue == "")
                {
                    qIdCurso = "";
                }
                else
                {
                    for (int k = 1; k < ddlCursoCalculoCusto.Items.Count; k++)
                    {
                        if (qIdCurso != "")
                        {
                            qIdCurso = qIdCurso + ",";
                        }
                        qIdCurso = qIdCurso + ddlCursoCalculoCusto.Items[k].Value;
                    }

                }

                if (optOrdenarProfessor.Checked)
                {
                    qOrdenacao = "professor";
                }
                else
                {
                    qOrdenacao = "empresa";
                }

                FinanceiroAplicacao aplicacaoFinanceiro = new FinanceiroAplicacao();
                List<geral_custo_hora_aula> lista_data_aula = aplicacaoFinanceiro.ListaCustoHoraAula(qIdCurso, qData, qOrdenacao);
                IDataFormat dataFormatCustom = workbook.CreateDataFormat();
                bool bTemRegistro = false;

                int j = 0;
                string sAux = "";
                foreach (var elemento in lista_data_aula)
                {
                    if (lista_data_aula.Count == j+1)
                    {
                        bTemRegistro = true;
                        break;
                    }

                    iRowNumer++;
                    row = sheet.CreateRow(iRowNumer);
                    j++;

                    cell = row.CreateCell(0);
                    cell.SetCellValue(j.ToString());
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(1);
                    sAux = elemento.titulo_reduzido + " " + elemento.professor;
                    cell.SetCellValue(sAux);
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(2);
                    sAux = elemento.NomeCurso;
                    cell.SetCellValue(sAux);
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(3);
                    cell.SetCellType(CellType.Numeric);
                    cell.SetCellValue(Convert.ToDouble(elemento.col_Total));
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(4);
                    cell.SetCellType(CellType.Numeric);
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(5);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(6);
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.SetCellType(CellType.Formula);
                    sAux = "SUM(D" + (j + 3).ToString() + "-E" + (j + 3).ToString() + ")";
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.SetCellFormula(sAux);

                    cell = row.CreateCell(7);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(8);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(9);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    dTotal = dTotal + elemento.col_Total;

                }
                //sheet.SetColumnWidth(0, 10000);
                //sheet.SetColumnWidth(1, 10000);

                if (!bTemRegistro)
                {
                    iRowNumer++;
                    row = sheet.CreateRow(iRowNumer);
                    j++;

                    cell = row.CreateCell(0);
                    cell.SetCellValue(j.ToString());
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(1);
                    sAux = "";
                    cell.SetCellValue(sAux);
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(2);
                    sAux = "";
                    cell.SetCellValue(sAux);
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(3);
                    cell.SetCellType(CellType.Numeric);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(4);
                    cell.SetCellType(CellType.Numeric);
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(5);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(6);
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.SetCellType(CellType.Formula);
                    sAux = "SUM(D" + (j + 3).ToString() + ":E" + (j + 3).ToString() + ")";
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.SetCellFormula(sAux);

                    cell = row.CreateCell(7);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(8);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(9);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                }

                // Linha com os TOTAIS
                iRowNumer++;
                row = sheet.CreateRow(iRowNumer);
                
                cell = row.CreateCell(0);
                cell.SetCellValue(" ");
                cell.CellStyle = style_10_Normal_Esquerda;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(1);
                cell.SetCellValue("TOTAL");
                cell.CellStyle = style_10_Negrito_Center;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(2);
                cell.SetCellValue("");
                cell.CellStyle = style_10_Normal_Esquerda;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(3);
                cell.CellStyle = style_10_Negrito_Direita_Azul;
                cell.SetCellType(CellType.Formula);
                sAux = "SUM(D4:D" + (iRowNumer).ToString() + ")";
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                cell.SetCellFormula(sAux);

                cell = row.CreateCell(4);
                cell.CellStyle = style_10_Negrito_Direita_Verde;
                cell.SetCellType(CellType.Formula);
                sAux = "SUM(E4:E" + (iRowNumer).ToString() + ")";
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                cell.SetCellFormula(sAux);

                cell = row.CreateCell(5);
                cell.SetCellValue("");
                cell.CellStyle = style_10_Normal_Esquerda;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(6);
                cell.CellStyle = style_10_Negrito_Direita;
                cell.SetCellType(CellType.Formula);
                sAux = "SUM(G4:G" + (iRowNumer).ToString() + ")";
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                cell.SetCellFormula(sAux);

                cell = row.CreateCell(7);
                cell.SetCellValue("");
                cell.CellStyle = style_10_Normal_Center;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(8);
                cell.SetCellValue("");
                cell.CellStyle = style_10_Normal_Center;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(9);
                cell.SetCellValue("");
                cell.CellStyle = style_10_Normal_Esquerda;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                // Linha em branco

                iRowNumer++;
                row = sheet.CreateRow(iRowNumer);

                cell = row.CreateCell(0);
                cell.SetCellValue("");

                cell = row.CreateCell(1);
                cell.SetCellValue("");

                cell = row.CreateCell(2);
                cell.SetCellValue("");

                cell = row.CreateCell(3);
                cell.SetCellValue("");

                cell = row.CreateCell(4);
                cell.SetCellValue("");

                cell = row.CreateCell(5);
                cell.SetCellValue("");

                cell = row.CreateCell(6);
                cell.SetCellValue("");

                cell = row.CreateCell(7);
                cell.SetCellValue("");

                cell = row.CreateCell(8);
                cell.SetCellValue("");

                cell = row.CreateCell(9);
                cell.SetCellValue("");


                //==== AULAS - FIM ========================

                //==========================================

                //==== BANCAS - INÍCIO ========================

                //Inicio
                iRowNumer++;
                row = sheet.CreateRow(iRowNumer);
                cell = row.CreateCell(0);
                cell.SetCellValue("BANCAS");
                cell.CellStyle = style_10_Negrito_Esquerda;
                cell.CellStyle.WrapText = false;

                iRowNumer++;
                row = sheet.CreateRow(iRowNumer);

                cell = row.CreateCell(0);
                cell.SetCellValue("ORD");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;

                cell = row.CreateCell(1);
                cell.SetCellValue("PROFESSOR");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(2);
                cell.SetCellValue("CURSO");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(3);
                cell.SetCellValue("VALOR");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(4);
                cell.SetCellValue("VALOR PAGO");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(5);
                cell.SetCellValue("DATA PAGTO");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(6);
                cell.SetCellValue("SALDO");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;
                sheet.SetColumnWidth(6, 4000);

                cell = row.CreateCell(7);
                cell.SetCellValue("EMPRESA/NF/DATA");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(8);
                cell.SetCellValue("NF FORA DA CIDADE E DO ESTADO");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(9);
                cell.SetCellValue("BAIXA NO SAPIENS");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                //Linha 1 - FIM ===========================

                List<geral_custo_banca_orientacao> lista_banca_orientacao = aplicacaoFinanceiro.ListaCustoBanca(qIdCurso, qData);
                bTemRegistro = false;

                j = 0;
                sAux = "";
                foreach (var elemento in lista_banca_orientacao)
                {
                    if (lista_banca_orientacao.Count == j + 1)
                    {
                        bTemRegistro = true;
                        break;
                    }

                    iRowNumer++;
                    row = sheet.CreateRow(iRowNumer);
                    j++;

                    cell = row.CreateCell(0);
                    cell.SetCellValue(j.ToString());
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(1);
                    sAux = elemento.col_Professor.Replace("<strong>", "").Replace("</strong>", "");
                    cell.SetCellValue(sAux);
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(2);
                    sAux = "";
                    cell.SetCellValue(sAux);
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(3);
                    cell.SetCellType(CellType.Numeric);
                    cell.SetCellValue(Convert.ToDouble(elemento.col_Total));
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(4);
                    cell.SetCellType(CellType.Numeric);
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(5);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(6);
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.SetCellType(CellType.Formula);
                    sAux = "SUM(D" + (iRowNumer + 1).ToString() + "-E" + (iRowNumer + 1).ToString() + ")";
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.SetCellFormula(sAux);

                    cell = row.CreateCell(7);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(8);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(9);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    dTotal = dTotal + elemento.col_Total;

                }
                //sheet.SetColumnWidth(0, 10000);
                //sheet.SetColumnWidth(1, 10000);

                if (!bTemRegistro)
                {
                    iRowNumer++;
                    row = sheet.CreateRow(iRowNumer);
                    j++;

                    cell = row.CreateCell(0);
                    cell.SetCellValue(j.ToString());
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(1);
                    sAux = "";
                    cell.SetCellValue(sAux);
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(2);
                    sAux = "";
                    cell.SetCellValue(sAux);
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(3);
                    cell.SetCellType(CellType.Numeric);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(4);
                    cell.SetCellType(CellType.Numeric);
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(5);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(6);
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.SetCellType(CellType.Formula);
                    sAux = "SUM(D" + (iRowNumer + 1).ToString() + ":E" + (iRowNumer + 1).ToString() + ")";
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.SetCellFormula(sAux);

                    cell = row.CreateCell(7);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(8);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(9);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                }

                // Linha com os TOTAIS

                iRowNumer++;
                row = sheet.CreateRow(iRowNumer);

                cell = row.CreateCell(0);
                cell.SetCellValue(" ");
                cell.CellStyle = style_10_Normal_Esquerda;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(1);
                cell.SetCellValue("TOTAL");
                cell.CellStyle = style_10_Negrito_Center;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(2);
                cell.SetCellValue("");
                cell.CellStyle = style_10_Normal_Esquerda;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(3);
                cell.CellStyle = style_10_Negrito_Direita_Azul;
                cell.SetCellType(CellType.Formula);
                sAux = "SUM(D" + ((iRowNumer + 1) - j).ToString() + ":D" + (iRowNumer).ToString() + ")";
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                cell.SetCellFormula(sAux);

                cell = row.CreateCell(4);
                cell.CellStyle = style_10_Negrito_Direita_Verde;
                cell.SetCellType(CellType.Formula);
                sAux = "SUM(E" + ((iRowNumer + 1) - j).ToString() + ":E" + (iRowNumer).ToString() + ")";
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                cell.SetCellFormula(sAux);

                cell = row.CreateCell(5);
                cell.SetCellValue("");
                cell.CellStyle = style_10_Normal_Esquerda;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(6);
                cell.CellStyle = style_10_Negrito_Direita;
                cell.SetCellType(CellType.Formula);
                sAux = "SUM(G" + ((iRowNumer + 1) - j).ToString() + ":G" + (iRowNumer).ToString() + ")";
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                cell.SetCellFormula(sAux);

                cell = row.CreateCell(7);
                cell.SetCellValue("");
                cell.CellStyle = style_10_Normal_Center;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(8);
                cell.SetCellValue("");
                cell.CellStyle = style_10_Normal_Center;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(9);
                cell.SetCellValue("");
                cell.CellStyle = style_10_Normal_Esquerda;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                // Linha em branco

                iRowNumer++;
                row = sheet.CreateRow(iRowNumer);

                cell = row.CreateCell(0);
                cell.SetCellValue("");

                cell = row.CreateCell(1);
                cell.SetCellValue("");

                cell = row.CreateCell(2);
                cell.SetCellValue("");

                cell = row.CreateCell(3);
                cell.SetCellValue("");

                cell = row.CreateCell(4);
                cell.SetCellValue("");

                cell = row.CreateCell(5);
                cell.SetCellValue("");

                cell = row.CreateCell(6);
                cell.SetCellValue("");

                cell = row.CreateCell(7);
                cell.SetCellValue("");

                cell = row.CreateCell(8);
                cell.SetCellValue("");

                cell = row.CreateCell(9);
                cell.SetCellValue("");


                //==== BANCAS - FIM ========================

                //==========================================

                //==== ORIENTAÇÃO - INÍCIO ========================

                //Inicio
                iRowNumer++;
                row = sheet.CreateRow(iRowNumer);
                cell = row.CreateCell(0);
                cell.SetCellValue("ORIENTAÇÃO");
                cell.CellStyle = style_10_Negrito_Esquerda;
                cell.CellStyle.WrapText = false;

                iRowNumer++;
                row = sheet.CreateRow(iRowNumer);

                cell = row.CreateCell(0);
                cell.SetCellValue("ORD");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;

                cell = row.CreateCell(1);
                cell.SetCellValue("PROFESSOR");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(2);
                cell.SetCellValue("CURSO");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(3);
                cell.SetCellValue("VALOR");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(4);
                cell.SetCellValue("VALOR PAGO");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(5);
                cell.SetCellValue("DATA PAGTO");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(6);
                cell.SetCellValue("SALDO");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;
                sheet.SetColumnWidth(6, 4000);

                cell = row.CreateCell(7);
                cell.SetCellValue("EMPRESA/NF/DATA");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(8);
                cell.SetCellValue("NF FORA DA CIDADE E DO ESTADO");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(9);
                cell.SetCellValue("BAIXA NO SAPIENS");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                //Linha 1 - FIM ===========================

                List<geral_custo_banca_orientacao> lista_orientacao = aplicacaoFinanceiro.ListaCustoOrientacao(qIdCurso, qData);
                bTemRegistro = false;

                j = 0;
                sAux = "";
                foreach (var elemento in lista_orientacao)
                {
                    if (lista_orientacao.Count == j + 1)
                    {
                        bTemRegistro = true;
                        break;
                    }

                    iRowNumer++;
                    row = sheet.CreateRow(iRowNumer);
                    j++;

                    cell = row.CreateCell(0);
                    cell.SetCellValue(j.ToString());
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(1);
                    //sAux = elemento.col_Professor.Replace("<strong>", "").Replace("</strong>", "");
                    sAux = elemento.col_Professor.Replace("<strong>", "").Replace("</strong>", "");
                    sAux = sAux.Substring(0, sAux.IndexOf("<br>"));
                    cell.SetCellValue(sAux);
                    cell.SetCellValue(sAux);
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.WrapText = false;

                    //"a".IndexOf()
                    cell = row.CreateCell(2);
                    sAux = elemento.col_Curso;
                    cell.SetCellValue(sAux);
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(3);
                    cell.SetCellType(CellType.Numeric);
                    cell.SetCellValue(Convert.ToDouble(elemento.col_Total));
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(4);
                    cell.SetCellType(CellType.Numeric);
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(5);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(6);
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.SetCellType(CellType.Formula);
                    sAux = "SUM(D" + (iRowNumer + 1).ToString() + "-E" + (iRowNumer + 1).ToString() + ")";
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.SetCellFormula(sAux);

                    cell = row.CreateCell(7);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(8);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(9);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    dTotal = dTotal + elemento.col_Total;

                }
                //sheet.SetColumnWidth(0, 10000);
                //sheet.SetColumnWidth(1, 10000);

                if (!bTemRegistro)
                {
                    iRowNumer++;
                    row = sheet.CreateRow(iRowNumer);
                    j++;

                    cell = row.CreateCell(0);
                    cell.SetCellValue(j.ToString());
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(1);
                    sAux = "";
                    cell.SetCellValue(sAux);
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(2);
                    sAux = "";
                    cell.SetCellValue(sAux);
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(3);
                    cell.SetCellType(CellType.Numeric);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(4);
                    cell.SetCellType(CellType.Numeric);
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(5);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(6);
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.SetCellType(CellType.Formula);
                    sAux = "SUM(D" + (iRowNumer + 1).ToString() + ":E" + (iRowNumer + 1).ToString() + ")";
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.SetCellFormula(sAux);

                    cell = row.CreateCell(7);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(8);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(9);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                }

                // Linha com os TOTAIS

                iRowNumer++;
                row = sheet.CreateRow(iRowNumer);

                cell = row.CreateCell(0);
                cell.SetCellValue(" ");
                cell.CellStyle = style_10_Normal_Esquerda;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(1);
                cell.SetCellValue("TOTAL");
                cell.CellStyle = style_10_Negrito_Center;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(2);
                cell.SetCellValue("");
                cell.CellStyle = style_10_Normal_Esquerda;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(3);
                cell.CellStyle = style_10_Negrito_Direita_Azul;
                cell.SetCellType(CellType.Formula);
                sAux = "SUM(D" + ((iRowNumer + 1) - j).ToString() + ":D" + (iRowNumer).ToString() + ")";
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                cell.SetCellFormula(sAux);

                cell = row.CreateCell(4);
                cell.CellStyle = style_10_Negrito_Direita_Verde;
                cell.SetCellType(CellType.Formula);
                sAux = "SUM(E" + ((iRowNumer + 1) - j).ToString() + ":E" + (iRowNumer).ToString() + ")";
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                cell.SetCellFormula(sAux);

                cell = row.CreateCell(5);
                cell.SetCellValue("");
                cell.CellStyle = style_10_Normal_Esquerda;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(6);
                cell.CellStyle = style_10_Negrito_Direita;
                cell.SetCellType(CellType.Formula);
                sAux = "SUM(G" + ((iRowNumer + 1) - j).ToString() + ":G" + (iRowNumer).ToString() + ")";
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                cell.SetCellFormula(sAux);

                cell = row.CreateCell(7);
                cell.SetCellValue("");
                cell.CellStyle = style_10_Normal_Center;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(8);
                cell.SetCellValue("");
                cell.CellStyle = style_10_Normal_Center;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(9);
                cell.SetCellValue("");
                cell.CellStyle = style_10_Normal_Esquerda;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                // Linha em branco

                iRowNumer++;
                row = sheet.CreateRow(iRowNumer);

                cell = row.CreateCell(0);
                cell.SetCellValue("");

                cell = row.CreateCell(1);
                cell.SetCellValue("");

                cell = row.CreateCell(2);
                cell.SetCellValue("");

                cell = row.CreateCell(3);
                cell.SetCellValue("");

                cell = row.CreateCell(4);
                cell.SetCellValue("");

                cell = row.CreateCell(5);
                cell.SetCellValue("");

                cell = row.CreateCell(6);
                cell.SetCellValue("");

                cell = row.CreateCell(7);
                cell.SetCellValue("");

                cell = row.CreateCell(8);
                cell.SetCellValue("");

                cell = row.CreateCell(9);
                cell.SetCellValue("");


                //==== ORIENTAÇÃO - FIM ========================

                //==========================================

                //==== COORDENAÇÃO - INÍCIO ========================

                //Inicio
                iRowNumer++;
                row = sheet.CreateRow(iRowNumer);
                cell = row.CreateCell(0);
                cell.SetCellValue("COORDENAÇÃO");
                cell.CellStyle = style_10_Negrito_Esquerda;
                cell.CellStyle.WrapText = false;

                iRowNumer++;
                row = sheet.CreateRow(iRowNumer);

                cell = row.CreateCell(0);
                cell.SetCellValue("ORD");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;

                cell = row.CreateCell(1);
                cell.SetCellValue("PROFESSOR");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(2);
                cell.SetCellValue("CURSO");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(3);
                cell.SetCellValue("VALOR");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(4);
                cell.SetCellValue("VALOR PAGO");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(5);
                cell.SetCellValue("DATA PAGTO");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(6);
                cell.SetCellValue("SALDO");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;
                sheet.SetColumnWidth(6, 4000);

                cell = row.CreateCell(7);
                cell.SetCellValue("EMPRESA/NF/DATA");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(8);
                cell.SetCellValue("NF FORA DA CIDADE E DO ESTADO");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(9);
                cell.SetCellValue("BAIXA NO SAPIENS");
                cell.CellStyle = style_10_Negrito_Center_Rosa;
                cell.CellStyle.WrapText = false;

                //Linha 1 - FIM ===========================

                List<geral_custo_coordenacao> lista_Coordenacao = aplicacaoFinanceiro.ListaCustoCoordenacao(qIdCurso, 0, qData);
                bTemRegistro = false;

                j = 0;
                sAux = "";
                foreach (var elemento in lista_Coordenacao)
                {
                    if (lista_Coordenacao.Count == j + 1)
                    {
                        bTemRegistro = true;
                        break;
                    }

                    iRowNumer++;
                    row = sheet.CreateRow(iRowNumer);
                    j++;

                    cell = row.CreateCell(0);
                    cell.SetCellValue(j.ToString());
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(1);
                    sAux = elemento.col_Professor.Replace("<strong>", "").Replace("</strong>", "");
                    cell.SetCellValue(sAux);
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(2);
                    sAux = elemento.col_Curso;
                    cell.SetCellValue(sAux);
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(3);
                    cell.SetCellType(CellType.Numeric);
                    cell.SetCellValue(Convert.ToDouble(elemento.col_Total));
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(4);
                    cell.SetCellType(CellType.Numeric);
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(5);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(6);
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.SetCellType(CellType.Formula);
                    sAux = "SUM(D" + (iRowNumer + 1).ToString() + "-E" + (iRowNumer + 1).ToString() + ")";
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.SetCellFormula(sAux);

                    cell = row.CreateCell(7);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(8);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(9);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    dTotal = dTotal + elemento.col_Total;

                }
                //sheet.SetColumnWidth(0, 10000);
                //sheet.SetColumnWidth(1, 10000);

                if (!bTemRegistro)
                {
                    iRowNumer++;
                    row = sheet.CreateRow(iRowNumer);
                    j++;

                    cell = row.CreateCell(0);
                    cell.SetCellValue(j.ToString());
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(1);
                    sAux = "";
                    cell.SetCellValue(sAux);
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(2);
                    sAux = "";
                    cell.SetCellValue(sAux);
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(3);
                    cell.SetCellType(CellType.Numeric);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(4);
                    cell.SetCellType(CellType.Numeric);
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(5);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(6);
                    cell.CellStyle = style_10_Normal_Direita;
                    cell.SetCellType(CellType.Formula);
                    sAux = "SUM(D" + (iRowNumer + 1).ToString() + ":E" + (iRowNumer + 1).ToString() + ")";
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                    cell.SetCellFormula(sAux);

                    cell = row.CreateCell(7);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(8);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Center;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                    cell = row.CreateCell(9);
                    cell.SetCellValue("");
                    cell.CellStyle = style_10_Normal_Esquerda;
                    cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    cell.CellStyle.WrapText = false;

                }

                // Linha com os TOTAIS

                iRowNumer++;
                row = sheet.CreateRow(iRowNumer);

                cell = row.CreateCell(0);
                cell.SetCellValue(" ");
                cell.CellStyle = style_10_Normal_Esquerda;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(1);
                cell.SetCellValue("TOTAL");
                cell.CellStyle = style_10_Negrito_Center;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(2);
                cell.SetCellValue("");
                cell.CellStyle = style_10_Normal_Esquerda;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(3);
                cell.CellStyle = style_10_Negrito_Direita_Azul;
                cell.SetCellType(CellType.Formula);
                sAux = "SUM(D" + ((iRowNumer + 1) - j).ToString() + ":D" + (iRowNumer).ToString() + ")";
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                cell.SetCellFormula(sAux);

                cell = row.CreateCell(4);
                cell.CellStyle = style_10_Negrito_Direita_Verde;
                cell.SetCellType(CellType.Formula);
                sAux = "SUM(E" + ((iRowNumer + 1) - j).ToString() + ":E" + (iRowNumer).ToString() + ")";
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                cell.SetCellFormula(sAux);

                cell = row.CreateCell(5);
                cell.SetCellValue("");
                cell.CellStyle = style_10_Normal_Esquerda;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(6);
                cell.CellStyle = style_10_Negrito_Direita;
                cell.SetCellType(CellType.Formula);
                sAux = "SUM(G" + ((iRowNumer + 1) - j).ToString() + ":G" + (iRowNumer).ToString() + ")";
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("#,###,###,##0.00");
                cell.SetCellFormula(sAux);

                cell = row.CreateCell(7);
                cell.SetCellValue("");
                cell.CellStyle = style_10_Normal_Center;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(8);
                cell.SetCellValue("");
                cell.CellStyle = style_10_Normal_Center;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                cell = row.CreateCell(9);
                cell.SetCellValue("");
                cell.CellStyle = style_10_Normal_Esquerda;
                cell.CellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                cell.CellStyle.WrapText = false;

                // Linha em branco

                iRowNumer++;
                row = sheet.CreateRow(iRowNumer);

                cell = row.CreateCell(0);
                cell.SetCellValue("");

                cell = row.CreateCell(1);
                cell.SetCellValue("");

                cell = row.CreateCell(2);
                cell.SetCellValue("");

                cell = row.CreateCell(3);
                cell.SetCellValue("");

                cell = row.CreateCell(4);
                cell.SetCellValue("");

                cell = row.CreateCell(5);
                cell.SetCellValue("");

                cell = row.CreateCell(6);
                cell.SetCellValue("");

                cell = row.CreateCell(7);
                cell.SetCellValue("");

                cell = row.CreateCell(8);
                cell.SetCellValue("");

                cell = row.CreateCell(9);
                cell.SetCellValue("");


                //==== COORDENAÇÃO - FIM ========================

                MemoryStream stream = new MemoryStream();
                workbook.Write(stream);

                Response.ContentType = "application/vnd.ms-excel";
                Response.BinaryWrite(stream.ToArray());
                Response.AppendHeader("Content-Disposition", "inline; filename=SAPIENS_" + DateTime.Today.ToString("dd-MM-yyyy") + ".xlsx");

                Response.End();


                if (File.Exists(Server.MapPath("~/doctos/PagamentoDocentes_" + ddlMesCalculoCusto.SelectedValue + "-" + txtAnoCalculoCusto.Value.Trim() + ".pdf")))
                {
                    Response.Clear();
                    Response.BufferOutput = true;
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/PagamentoDocentes_" + ddlMesCalculoCusto.SelectedValue + "-" + txtAnoCalculoCusto.Value.Trim() + ".pdf")));
                    Response.WriteFile(Server.MapPath("~/doctos/PagamentoDocentes_" + ddlMesCalculoCusto.SelectedValue + "-" + txtAnoCalculoCusto.Value.Trim() + ".pdf"));
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão de Relatório Pagamento de Docentes";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                if (ex.InnerException != null)
                {
                    lblMensagem.Text = lblMensagem.Text + "<br/>" + ex.InnerException.Message;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('modal-header alert-success');", true);
            }
        }

        public string set_ValorHora(decimal qValor)
        {
            string sAux;
            //string sMinutos;

            //switch ((qValor - Math.Truncate(qValor)).ToString())
            //{
            //    case "0,08":
            //        sMinutos = "05";
            //        break;
            //    case "0,17":
            //        sMinutos = "10";
            //        break;
            //    case "0,25":
            //        sMinutos = "15";
            //        break;
            //    case "0,33":
            //        sMinutos = "20";
            //        break;
            //    case "0,42":
            //        sMinutos = "25";
            //        break;
            //    case "0,50":
            //        sMinutos = "30";
            //        break;
            //    case "0,58":
            //        sMinutos = "35";
            //        break;
            //    case "0,67":
            //        sMinutos = "40";
            //        break;
            //    case "0,75":
            //        sMinutos = "45";
            //        break;
            //    case "0,83":
            //        sMinutos = "50";
            //        break;
            //    case "0,92":
            //        sMinutos = "55";
            //        break;
            //    default:
            //        sMinutos = "00";
            //        break;
            //}
            //sAux = Math.Truncate(qValor).ToString("00") + ":" + sMinutos;

            var timeSpan = TimeSpan.FromHours(Convert.ToDouble(qValor));

            int hh = timeSpan.Hours;
            int mm = timeSpan.Minutes;
            int ss = timeSpan.Seconds;

            var hours = Math.Floor(qValor);
            //var mins = 60 * (elemento.col_TotalHoras - hours);

            if (hh == 0 && hours != 0)
            {
                hh = Convert.ToInt32(hours);
            }

            sAux = hh.ToString("00") + ":" + mm.ToString("00");

            //rever
            sAux = hours.ToString("00") + ":" + mm.ToString("00");

            return sAux;
        }

    }
}