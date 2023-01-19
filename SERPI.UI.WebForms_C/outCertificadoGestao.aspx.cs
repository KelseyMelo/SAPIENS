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

namespace SERPI.UI.WebForms_C
{
    public partial class outCertificadoGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 75)) // 3. Cadastro Tela - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                CertificadoAplicacao aplicacaoCertificado = new CertificadoAplicacao();
                List<certificado_tipo_curso> lista = aplicacaoCertificado.Lista_certificado_tipo_curso();
                ddlTipoCursoCertificado.Items.Clear();
                ddlTipoCursoCertificado.DataSource = lista;
                ddlTipoCursoCertificado.DataValueField = "id_certificado_tipo_curso";
                ddlTipoCursoCertificado.DataTextField = "descricao";
                ddlTipoCursoCertificado.DataBind();
                ddlTipoCursoCertificado.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Tipo", ""));
                ddlTipoCursoCertificado.SelectedValue = "";

                if (Session["sNewcertificados"] != null && (Boolean)Session["sNewcertificados"] != true)
                {
                    certificados item;
                    item = (certificados)Session["certificados"];
                    lblTituloPagina.Text = "(Editar) - N.º " + item.id_certificado;

                    txtDataEvento.Value = Convert.ToDateTime(item.data_evento).ToString("yyyy-MM-dd");
                    txtNumeroSequencial.Value = item.numero_seq_inicial.ToString();
                    txtAnoReferencia.Value = item.ano.ToString();
                    txtCampo1a.Value = item.campo1a;
                    txtCampo2a.Value = item.campo2a;
                    txtNomeEvento.Value = item.evento;
                    txtCampo3a.Value = item.campo3a;
                    txtCampo3b.Value = item.campo3b;
                    txtCampo3c.Value = item.campo3c;
                    txtCampo3d.Value = item.campo3d;
                    imgAssinatura1.Src = "AssinaturasDiversas\\" + item.assinatura1_imagem;
                    txtImagemAssinatura1.Value = item.assinatura1_imagem;
                    txtNomeAssinatura1.Value = item.assinatura1_nome;
                    txtCargoAssinatura1.Value = item.assinatura1_cargo;
                    txtObsFolha2.Value = item.obs_folha2;
                    if (item.id_certificado_tipo_curso != null)
                    {
                        ddlTipoCursoCertificado.SelectedValue = item.id_certificado_tipo_curso.ToString();
                        if (item.id_certificado_tipo_curso == 2)
                        {
                            txtInformacoesAdicionais.Value = item.informacao_adicional;
                            divColInformacoesAdicionais.Style.Add("display", "block");
                        }
                        else
                        {
                            txtInformacoesAdicionais.Value = "";
                        }
                    }
                    else
                    {
                        ddlTipoCursoCertificado.SelectedValue = "";
                        txtInformacoesAdicionais.Value = "";
                        divColInformacoesAdicionais.Style.Add("display", "none");
                    }

                    divLog.Visible = true;
                    txtDataCadastro.Value = Convert.ToDateTime(item.data_cadastro).ToString("dd/MM/yyyy");
                    txtDataAlteracao.Value = Convert.ToDateTime(item.data_alteracao).ToString("dd/MM/yyyy");
                    txtResponsavel.Value = item.usuario;

                    optTipoCertificado_1.Checked = false;
                    optTipoCertificado_2.Checked = false;
                    optTipoCertificado_3.Checked = false;
                    optTipoCertificado_4.Checked = false;
                    optTipoCertificado_5.Checked = false;

                    if (item.tipo_certificado == 1)
                    {
                        optTipoCertificado_1.Checked = true;
                    }
                    else if (item.tipo_certificado == 2)
                    {
                        optTipoCertificado_2.Checked = true;
                    }
                    else if (item.tipo_certificado == 3)
                    {
                        optTipoCertificado_3.Checked = true;
                    }
                    else if (item.tipo_certificado == 4)
                    {
                        optTipoCertificado_4.Checked = true;
                    }
                    else 
                    {
                        optTipoCertificado_5.Checked = true;
                    }

                    divTurma.Visible = true;
                    divPreview.Visible = true;

                    if (item.palestrante == null || item.palestrante == 0)
                    {
                        divAssinatura.Style.Add("display", "block");
                        optPalestranteNao.Checked = true;
                        optPalestranteSim.Checked = false;
                    }
                    else
                    {
                        divAssinatura.Style.Add("display", "none");
                        optPalestranteNao.Checked = false;
                        optPalestranteSim.Checked = true;
                    }

                    if (Session["AdiciondoSucesso"] != null)
                    {
                        if ((Boolean)Session["AdiciondoSucesso"])
                        {
                            Session["AdiciondoSucesso"] = null;
                            lblMensagem.Text = "Novo Certificado adicionado com sucesso";
                            lblTituloMensagem.Text = "Novo Certificado adicionado";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');", true);
                        }
                    }

                    StreamReader objReader;

                    objReader = new StreamReader(HttpRuntime.AppDomainAppPath + "\\Templates\\emails\\EmailLoteCertificado.html");

                    string sCorpo = objReader.ReadToEnd();
                    objReader.Close();

                    byte[] imageArray = System.IO.File.ReadAllBytes(Server.MapPath("~/img/pessoas/" + usuario.avatar));
                    string ext = Path.GetExtension(Server.MapPath("~/img/pessoas/" + usuario.avatar));
                    string base64ImageRepresentation = "data:image/" + ext.Replace(".","") + ";base64," + Convert.ToBase64String(imageArray);
                    //txtCorpoEmail.Value = txtCorpoEmail.Value;

                    sCorpo = sCorpo.Replace("{imagem_usuario}", base64ImageRepresentation);

                    sCorpo = sCorpo.Replace("{usuario_remetente}", usuario.nomeSocial);

                    sCorpo = sCorpo.Replace("{evento}", item.evento);

                    sCorpo = sCorpo.Replace("{data_limite}", DateTime.Today.AddMonths(2).ToString("dd/MM/yyyy"));

                    txtCorpoEmail.Value = sCorpo;

                }
                else
                {
                    lblTituloPagina.Text = "(novo)";
                    txtDataEvento.Value = "";
                    txtNumeroSequencial.Value = "";
                    txtAnoReferencia.Value = "";
                    txtCampo1a.Value = "PELO PRESENTE CERTIFICAMOS QUE:";
                    txtCampo2a.Value = "PARTICIPOU DO CURSO:";
                    txtNomeEvento.Value = "";
                    txtCampo3a.Value = " DATA:";
                    txtCampo3b.Value = "";
                    txtCampo3c.Value = "Carga horária:";
                    txtCampo3d.Value = "";
                    imgAssinatura1.Src = "#";
                    txtImagemAssinatura1.Value = "";
                    txtNomeAssinatura1.Value = "";
                    txtCargoAssinatura1.Value = "";
                    txtObsFolha2.Value = "";
                    optTipoCertificado_1.Checked = false;
                    optTipoCertificado_2.Checked = false;
                    optTipoCertificado_3.Checked = false;
                    optTipoCertificado_4.Checked = false;
                    ddlTipoCursoCertificado.SelectedValue = "";
                    divColInformacoesAdicionais.Style.Add("display", "none");

                    divTurma.Visible = false;
                    divPreview.Visible = false;
                    divAssinatura.Style.Add("display", "block");
                    optPalestranteNao.Checked = true;
                    optPalestranteSim.Checked = false;
                    divLog.Visible = false;
                }
            }

        }

        protected void btnCriarCertificado_Click(object sender, EventArgs e)
        {
            Session["sNewcertificados"] = true;
            Session["certificados"] = null;
            Session["AdiciondoSucesso"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnPreviewCertificado_Click(object sender, EventArgs e)
        {
            certificados item;
            item = (certificados)Session["certificados"];
            string sArquivo = "";

            certificados_participantes elemento = new certificados_participantes();
            elemento.nome = "Fulano Ciclano Souza e Silva";
            elemento.numero_seq = item.numero_seq_inicial;
            elemento.id_certificado = item.id_certificado;
            elemento.interno_externo = "I";
            elemento.certificados = item;

            //item.certificados_participantes.Add(elemento);


            if (item.tipo_certificado == 1)
            {
                sArquivo = ImprimeCertificado(elemento, "Normal");
            }
            else if (item.tipo_certificado == 2)
            {
                sArquivo = ImprimeCertificado_2(elemento);
            }
            else if (item.tipo_certificado == 3)
            {
                sArquivo = ImprimeCertificado_3(elemento);
            }
            else if (item.tipo_certificado == 4)
            {
                sArquivo = ImprimeCertificado_4(elemento);
            }
            else
            {
                sArquivo = ImprimeCertificado_5(elemento);
            }

            if (optPalestranteSim.Checked)
            {
                divAssinatura.Style.Add("display", "none");
            }
            else
            {
                divAssinatura.Style.Add("display", "block");
            }

            lblMensagem.Text = "Certificado Preview gerado com sucesso!";
            lblTituloMensagem.Text = "Certificado Preview Gerado";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "fDownloadPreview('/certificados/" + item.id_certificado + "/PDFs/" + sArquivo + "');AbreModalMensagem('alert-success');fAncora();", true);

        }
        protected void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            certificados item;
            item = (certificados)Session["certificados"];
            CertificadoAplicacao aplicacaoCertificado = new CertificadoAplicacao();
            string sArquivo;

            foreach (var elemento in item.certificados_participantes)
            {
                elemento.certificados = item;
                if (item.tipo_certificado == 1)
                {
                    sArquivo = ImprimeCertificado(elemento, "Normal");
                }
                else if (item.tipo_certificado == 2)
                {
                    sArquivo = ImprimeCertificado_2(elemento);
                }
                else if (item.tipo_certificado == 3)
                {
                    sArquivo = ImprimeCertificado_3(elemento);
                }
                else if (item.tipo_certificado == 4)
                {
                    sArquivo = ImprimeCertificado_4(elemento);
                }
                else
                {
                    sArquivo = ImprimeCertificado_5(elemento);
                }

                if (sArquivo != "")
                {
                    elemento.arquivo_pdf = sArquivo;
                    aplicacaoCertificado.AlterarItem_participante(elemento);
                }
            }

            GeraisAplicacao aplicacaoGerais = new GeraisAplicacao();
            Configuracoes item_configuracoes;
            // 1 = email mestrado@ipt.br
            // 2 = email suporte@ipt.br
            item_configuracoes = aplicacaoGerais.BuscaConfiguracoes(2);

            string sFrom = item_configuracoes.remetente_email;
            string sFrom_Nome = item_configuracoes.nome_remetente_email;
            string sTo;
            string sAssunto = "Gerado Certificados para o evento: " + item.evento;
            string sAux = "";

            UsuarioAplicacao aplicacaoUsuario = new UsuarioAplicacao();
            usuarios item_usuario = new usuarios();
            item_usuario.grupos_acesso = new grupos_acesso();
            item_usuario.grupos_acesso.id_grupo= 8; //Gerência	Depto Gerência - Mestrado
            List<usuarios> lista_usuarios = aplicacaoUsuario.ListaUsuario_porGrupoAcesso(item_usuario);

            foreach (var elemento in lista_usuarios)
            {
                sAux = "Caro " + elemento.nome;

                sAux = sAux + "<br><br>Foram criados <b>" + item.certificados_participantes.Count().ToString() + "</b> certificados para o evento: <b>" + item.evento + "</b>";

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



            item = aplicacaoCertificado.BuscaItem(item);
            Session["certificados"] = item;

            lblMensagem.Text = "Certificados gerados com sucesso!";
            lblTituloMensagem.Text = "Certificados Gerados";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');fAncora();", true);

        }

        private string ImprimeCertificado(certificados_participantes item, string sTipo)
        {
            try
            {
                Document doc = new Document(PageSize.A4.Rotate());//criando e estipulando o tipo da folha usada
                doc.SetMargins(70, 130, 95, 10);//estibulando o espaçamento das margens que queremos ===Antigo===(70, 130, 200, 10) //Alterado pedido andreia 17/09/2020
                doc.AddCreationDate();//adicionando as configuracoes

                string sArquivo;

                if (!System.IO.Directory.Exists(Server.MapPath("") + "\\Certificados\\" + item.id_certificado + "\\PDFs\\"))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("") + "\\Certificados\\" + item.id_certificado + "\\PDFs\\");
                }

                sArquivo = item.nome.Trim().Replace("'"," ") + "_" + item.id_certificado_participante + ".pdf";

                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("") + "\\Certificados\\" + item.id_certificado + "\\PDFs\\" + sArquivo, FileMode.Create));
                doc.Open();

                iTextSharp.text.Image imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/img/Homepage/logo.jpg"));
                //imgCabecalho.SetAbsolutePosition(0, 430);
                imgCabecalho.SetAbsolutePosition(560, 480);
                //imgCabecalho.ScalePercent(97);
                imgCabecalho.ScalePercent(70);
                doc.Add(imgCabecalho);

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
                Font font_Arialn_24_Bold = new Font(_bfArialNarrowNormal, 24, Font.BOLD);
                Font font_Arialn_24_Normal = new Font(_bfArialNarrowNormal, 24, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_14_Normal = new Font(_bfArialNarrowNormal, 14, Font.NORMAL);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_12_Bold = new Font(_bfArialNarrowNormal, 12, Font.BOLD);
                Font font_Arialn_12_Normal = new Font(_bfArialNarrowNormal, 12, Font.NORMAL);
                Font font_Arialn_11_Bold = new Font(_bfArialNarrowNormal, 11, Font.BOLD);
                Font font_Arialn_11_Normal = new Font(_bfArialNarrowNormal, 11, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_7_Bold = new Font(_bfArialNarrowNormal, 7, Font.BOLD);
                Font font_Arialn_7_Normal = new Font(_bfArialNarrowNormal, 7, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);

                Font font_Tahoma_40_Bold = new Font(_bfTahoma, 40, Font.BOLD);
                Font font_Tahoma_40_Normal = new Font(_bfTahoma, 40, Font.NORMAL);
                Font font_Tahoma_30_Bold = new Font(_bfTahoma, 30, Font.BOLD);
                Font font_Tahoma_25_Bold = new Font(_bfTahoma, 25, Font.BOLD);
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



                //criando a variavel para paragrafo
                Paragraph paragrafo = new Paragraph();
                //estipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_LEFT;

                paragrafo.Add(new Chunk("Certificado\n\n\n ", font_Tahoma_40_Normal));

                doc.Add(paragrafo);
                //Alterado pedido andreia 17/09/2020

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_RIGHT;

                string sAux = item.certificados.ano.ToString();
                paragrafo.Add(new Chunk(item.numero_seq + "-" + item.interno_externo + "/" + sAux.Substring(sAux.Length-2,2), font_Tahoma_15_Normal));
                paragrafo.Add(new Chunk("\n\n\n\n", font_Arialn_14_Bold));
                doc.Add(paragrafo);

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_LEFT;

                paragrafo.Add(new Chunk(item.certificados.campo1a, font_Arialn_12_Normal));
                paragrafo.Add(new Chunk(" " + item.nome.Trim().ToUpper() + "\n\n", font_Arialn_24_Bold));
                
                paragrafo.SetLeading(25, 0); //Aumenta o espaço entre as linhas.

                //paragrafo.IndentationLeft = 20;
                //paragrafo.IndentationRight = 20;

                doc.Add(paragrafo);

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_JUSTIFIED;

                paragrafo.Add(new Chunk(item.certificados.campo2a, font_Arialn_12_Normal));
                paragrafo.Add(new Chunk(" " + item.certificados.evento + "\n\n", font_Arialn_12_Bold));

                paragrafo.Add(new Chunk(item.certificados.campo3a, font_Arialn_12_Normal));
                paragrafo.Add(new Chunk(" " + item.certificados.campo3b + "\n\n", font_Arialn_12_Bold));

                paragrafo.SetLeading(18, 0); //Aumenta o espaço entre as linhas.

                //paragrafo.IndentationLeft = 20;
                //paragrafo.IndentationRight = 20;

                doc.Add(paragrafo);


                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_RIGHT;

                paragrafo.Add(new Chunk(item.certificados.campo3c, font_Arialn_12_Normal));
                paragrafo.Add(new Chunk(" " + item.certificados.campo3d + "\n\n", font_Arialn_12_Bold));

                paragrafo.Add(new Chunk("São Paulo, " + DateTime.Today.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("PT-pt")) + ".\n\n\n", font_Arialn_12_Normal));
                doc.Add(paragrafo);

                //=== Assinaturas ========================================================

                if (item.certificados.palestrante != 1)
                {
                    imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/AssinaturasDiversas/" + item.certificados.assinatura1_imagem));
                    //imgCabecalho.SetAbsolutePosition(0, 430);
                    imgCabecalho.SetAbsolutePosition(145, 110);
                    //imgCabecalho.ScalePercent(97);
                    //imgCabecalho.ScalePercent(70);
                    imgCabecalho.ScaleAbsoluteHeight(90);
                    imgCabecalho.ScaleAbsoluteWidth(160);
                    doc.Add(imgCabecalho);

                    imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/AssinaturasDiversas/" + item.certificados.assinatura2_imagem));
                    //imgCabecalho.SetAbsolutePosition(0, 430);
                    imgCabecalho.SetAbsolutePosition(500, 110);
                    //imgCabecalho.ScalePercent(97);
                    imgCabecalho.ScaleAbsoluteHeight(90);
                    imgCabecalho.ScaleAbsoluteWidth(160);
                    doc.Add(imgCabecalho);
                }
                else
                {
                    imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/AssinaturasDiversas/" + item.certificados.assinatura2_imagem));
                    //imgCabecalho.SetAbsolutePosition(0, 430);
                    imgCabecalho.SetAbsolutePosition(310, 110);
                    //imgCabecalho.ScalePercent(97);
                    imgCabecalho.ScaleAbsoluteHeight(90);
                    imgCabecalho.ScaleAbsoluteWidth(160);
                    doc.Add(imgCabecalho);
                }


                //Aqui é uma nova tabela de 3 Colunas ========================================================
                int iSpace = 0;

                PdfPTable table;
                float[] widths;
                PdfPCell cell;
                Paragraph p;
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);

                if (item.certificados.palestrante != 1)
                {
                    table = new PdfPTable(3);
                    table.TotalWidth = 620f;
                    table.LockedWidth = true;
                    widths = new float[] { 170f, 50f, 165f };
                    table.SetWidths(widths);

                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk("\n\n", font_Arialn_11_Bold));
                    p.Add(new Chunk(linefine));
                    p.Add(new Chunk("\n" + item.certificados.assinatura1_nome, font_Arialn_11_Normal));
                    iSpace = 70 - (item.certificados.assinatura1_cargo.Length);
                    p.Add(new Chunk("\n" + item.certificados.assinatura1_cargo + string.Empty.PadLeft(iSpace), font_Arialn_11_Normal));
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
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk(" ", font_Verdana_10_Bold));
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
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk("\n\n", font_Arialn_11_Bold));
                    p.Add(new Chunk(linefine));
                    p.Add(new Chunk("\n" + item.certificados.assinatura2_nome, font_Arialn_11_Normal));
                    iSpace = 70 - (item.certificados.assinatura2_cargo.Length);
                    p.Add(new Chunk("\n" + item.certificados.assinatura2_cargo + string.Empty.PadLeft(iSpace), font_Arialn_11_Normal));
                    p.SpacingAfter = 20f;
                    cell = new PdfPCell();
                    cell.AddElement(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);
                    doc.Add(table);
                }
                else
                {
                    table = new PdfPTable(3);
                    table.TotalWidth = 620f;
                    table.LockedWidth = true;
                    widths = new float[] { 130f, 150f, 105f };
                    table.SetWidths(widths);

                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk(" ", font_Verdana_10_Bold));
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
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk("\n\n", font_Arialn_11_Bold));
                    p.Add(new Chunk(linefine));
                    p.Add(new Chunk("\n" + item.certificados.assinatura2_nome, font_Arialn_11_Normal));
                    iSpace = 70 - (item.certificados.assinatura2_cargo.Length);
                    p.Add(new Chunk("\n" + item.certificados.assinatura2_cargo + string.Empty.PadLeft(iSpace), font_Arialn_11_Normal));
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
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk(" ", font_Verdana_10_Bold));
                    p.SpacingAfter = 20f;
                    cell = new PdfPCell();
                    cell.AddElement(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);


                    doc.Add(table);
                }

                //=======================================================================

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_LEFT;

                paragrafo.Add(new Chunk("\nInstituto de Pesquisas Tecnológicas do Estado de São Paulo - IPT", font_Arialn_10_Normal));
                paragrafo.Add(new Chunk("\nwww.ipt.br", font_Arialn_10_Normal));

                paragrafo.SetLeading(10, 0); //Aumenta o espaço entre as linhas.

                //paragrafo.IndentationLeft = 20;
                //paragrafo.IndentationRight = 20;

                doc.Add(paragrafo);

                ////Alterado pedido andreia 17/09/2020
                //paragrafo = new Paragraph();
                ////estipulando o alinhamneto
                //paragrafo.Clear();
                //paragrafo.Alignment = Element.ALIGN_CENTER;

                //paragrafo.Add(new Chunk("\n\n______________________________________\nProf. Dr. Eduardo Luiz Machado\nDiretor Técnico em Ensino Tecnológico", font_Tahoma_11_Normal));
                //doc.Add(paragrafo);





                //Alterado pedido andreia 17/09/2020

                //Alterado pedido andreia 17/09/2020
                //imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/img/img-cabecalho/assinaturaEduardo.png"));
                //imgCabecalho.SetAbsolutePosition(280, 40);
                //imgCabecalho.ScalePercent(77);
                //doc.Add(imgCabecalho);
                //Alterado pedido andreia 17/09/2020


                //===================================================
                //==Nova Página
                if (item.certificados.obs_folha2 != "" && item.certificados.obs_folha2 != null)
                {
                    
                    paragrafo = new Paragraph();

                    //etipulando o alinhamneto
                    paragrafo.Clear();
                    paragrafo.Alignment = Element.ALIGN_JUSTIFIED;

                    //Retirado o espaçamento abaixo por solicitação da Longuinho no dia 21/09/2022
                    //paragrafo.Add(new Chunk("\n\n\n\n\n\n\n\n\n", font_Tahoma_10_Normal));
                    paragrafo.Add(new Chunk("\n\n", font_Tahoma_10_Normal));

                    ArrayList htmlarraylist;
                    htmlarraylist = HTMLWorker.ParseToList(new StringReader(item.certificados.obs_folha2), null);
                    for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                    {
                        paragrafo.Add(htmlarraylist[k]);
                    }

                    //paragrafo.Add(new Chunk(item.certificados.obs_folha2, font_Tahoma_10_Normal));

                    paragrafo.SetLeading(12, 0); //Aumenta o espaço entre as linhas.

                    //paragrafo.IndentationLeft = 20;
                    //paragrafo.IndentationRight = 20;

                    doc.Add(paragrafo);
                }

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

                return sArquivo;
                //if (File.Exists(Server.MapPath("~/doctos/Certificado.pdf")))
                //{
                //    Response.Clear();
                //    Response.BufferOutput = true;
                //    Response.ContentType = "application/pdf";
                //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/Certificado.pdf")));
                //    Response.WriteFile(Server.MapPath("~/doctos/Certificado.pdf"));
                //    Response.Flush();
                //    Response.End();
                //}

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão do Certificado";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-danger');", true);
                return "";
            }
        }

        private string ImprimeCertificado_2(certificados_participantes item)
        {
            try
            {
                Document doc = new Document(PageSize.A4.Rotate());//criando e estipulando o tipo da folha usada
                doc.SetMargins(70, 130, 95, 10);//estibulando o espaçamento das margens que queremos ===Antigo===(70, 130, 200, 10) //Alterado pedido andreia 17/09/2020
                doc.AddCreationDate();//adicionando as configuracoes

                string sArquivo;

                if (!System.IO.Directory.Exists(Server.MapPath("") + "\\Certificados\\" + item.id_certificado + "\\PDFs\\"))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("") + "\\Certificados\\" + item.id_certificado + "\\PDFs\\");
                }

                sArquivo = item.nome.Trim().Replace("'", " ") + "_" + item.id_certificado_participante + ".pdf";

                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("") + "\\Certificados\\" + item.id_certificado + "\\PDFs\\" + sArquivo, FileMode.Create));
                doc.Open();

                //iTextSharp.text.Image imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/img/Homepage/logo.jpg"));
                iTextSharp.text.Image imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/Certificados/barra_azul.png"));
                imgCabecalho.SetAbsolutePosition(300, 480);
                imgCabecalho.ScalePercent(70);
                doc.Add(imgCabecalho);

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
                Font font_Arialn_24_Bold = new Font(_bfArialNarrowNormal, 24, Font.BOLD);
                Font font_Arialn_24_Normal = new Font(_bfArialNarrowNormal, 24, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_14_Normal = new Font(_bfArialNarrowNormal, 14, Font.NORMAL);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_12_Bold = new Font(_bfArialNarrowNormal, 12, Font.BOLD);
                Font font_Arialn_12_Normal = new Font(_bfArialNarrowNormal, 12, Font.NORMAL);
                Font font_Arialn_11_Bold = new Font(_bfArialNarrowNormal, 11, Font.BOLD);
                Font font_Arialn_11_Normal = new Font(_bfArialNarrowNormal, 11, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_7_Bold = new Font(_bfArialNarrowNormal, 7, Font.BOLD);
                Font font_Arialn_7_Normal = new Font(_bfArialNarrowNormal, 7, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);

                Font font_Tahoma_40_Bold = new Font(_bfTahoma, 40, Font.BOLD);
                Font font_Tahoma_40_Normal = new Font(_bfTahoma, 40, Font.NORMAL);
                Color azul_claro_ipt = new Color(2, 188, 232);
                Font font_Tahoma_40_Normal_Azul = new Font(_bfTahoma, 40, Font.NORMAL, azul_claro_ipt);
                Font font_Tahoma_30_Bold = new Font(_bfTahoma, 30, Font.BOLD);
                Font font_Tahoma_25_Bold = new Font(_bfTahoma, 25, Font.BOLD);
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



                //criando a variavel para paragrafo
                Paragraph paragrafo = new Paragraph();
                //estipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_LEFT;

                paragrafo.Add(new Chunk("Certificado\n\n\n ", font_Tahoma_40_Normal_Azul));

                doc.Add(paragrafo);
                //Alterado pedido andreia 17/09/2020

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_RIGHT;

                string sAux = item.certificados.ano.ToString();
                paragrafo.Add(new Chunk(item.numero_seq + "-" + item.interno_externo + "/" + sAux.Substring(sAux.Length - 2, 2), font_Tahoma_15_Normal));
                paragrafo.Add(new Chunk("\n\n", font_Arialn_14_Bold));
                doc.Add(paragrafo);

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_LEFT;

                paragrafo.Add(new Chunk(item.certificados.campo1a, font_Arialn_12_Normal));
                paragrafo.Add(new Chunk(" " + item.nome.Trim().ToUpper() + "\n\n", font_Arialn_24_Bold));

                paragrafo.SetLeading(25, 0); //Aumenta o espaço entre as linhas.

                //paragrafo.IndentationLeft = 20;
                //paragrafo.IndentationRight = 20;

                doc.Add(paragrafo);

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_JUSTIFIED;

                paragrafo.Add(new Chunk(item.certificados.campo2a, font_Arialn_12_Normal));
                paragrafo.Add(new Chunk(" " + item.certificados.evento + "\n\n", font_Arialn_12_Bold));

                paragrafo.Add(new Chunk(item.certificados.campo3a, font_Arialn_12_Normal));
                paragrafo.Add(new Chunk(" " + item.certificados.campo3b + "\n\n", font_Arialn_12_Bold));

                paragrafo.SetLeading(18, 0); //Aumenta o espaço entre as linhas.

                //paragrafo.IndentationLeft = 20;
                //paragrafo.IndentationRight = 20;

                doc.Add(paragrafo);


                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_RIGHT;

                paragrafo.Add(new Chunk(item.certificados.campo3c, font_Arialn_12_Normal));
                paragrafo.Add(new Chunk(" " + item.certificados.campo3d + "\n\n", font_Arialn_12_Bold));

                paragrafo.Add(new Chunk("São Paulo, " + DateTime.Today.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("PT-pt")) + ".\n\n\n", font_Arialn_12_Normal));
                doc.Add(paragrafo);

                //=== Assinaturas ========================================================

                if (item.certificados.palestrante != 1)
                {
                    imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/AssinaturasDiversas/" + item.certificados.assinatura1_imagem));
                    //imgCabecalho.SetAbsolutePosition(0, 430);
                    imgCabecalho.SetAbsolutePosition(145, 150);
                    //imgCabecalho.ScalePercent(97);
                    //imgCabecalho.ScalePercent(70);
                    imgCabecalho.ScaleAbsoluteHeight(90);
                    imgCabecalho.ScaleAbsoluteWidth(160);
                    doc.Add(imgCabecalho);

                    imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/AssinaturasDiversas/" + item.certificados.assinatura2_imagem));
                    //imgCabecalho.SetAbsolutePosition(0, 430);
                    imgCabecalho.SetAbsolutePosition(500, 150);
                    //imgCabecalho.ScalePercent(97);
                    imgCabecalho.ScaleAbsoluteHeight(90);
                    imgCabecalho.ScaleAbsoluteWidth(160);
                    doc.Add(imgCabecalho);
                }
                else
                {
                    imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/AssinaturasDiversas/" + item.certificados.assinatura2_imagem));
                    //imgCabecalho.SetAbsolutePosition(0, 430);
                    imgCabecalho.SetAbsolutePosition(450, 150);
                    //imgCabecalho.ScalePercent(97);
                    imgCabecalho.ScaleAbsoluteHeight(90);
                    imgCabecalho.ScaleAbsoluteWidth(160);
                    doc.Add(imgCabecalho);
                }


                //Aqui é uma nova tabela de 3 Colunas ========================================================
                int iSpace = 0;

                PdfPTable table;
                float[] widths;
                PdfPCell cell;
                Paragraph p;
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);

                if (item.certificados.palestrante != 1)
                {
                    table = new PdfPTable(3);
                    table.TotalWidth = 620f;
                    table.LockedWidth = true;
                    widths = new float[] { 170f, 50f, 165f };
                    table.SetWidths(widths);

                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk("\n\n", font_Arialn_11_Bold));
                    p.Add(new Chunk(linefine));
                    p.Add(new Chunk("\n" + item.certificados.assinatura1_nome, font_Arialn_11_Normal));
                    iSpace = 70 - (item.certificados.assinatura1_cargo.Length);
                    p.Add(new Chunk("\n" + item.certificados.assinatura1_cargo + string.Empty.PadLeft(iSpace), font_Arialn_11_Normal));
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
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk(" ", font_Verdana_10_Bold));
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
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk("\n\n", font_Arialn_11_Bold));
                    p.Add(new Chunk(linefine));
                    p.Add(new Chunk("\n" + item.certificados.assinatura2_nome, font_Arialn_11_Normal));
                    iSpace = 70 - (item.certificados.assinatura2_cargo.Length);
                    p.Add(new Chunk("\n" + item.certificados.assinatura2_cargo + string.Empty.PadLeft(iSpace), font_Arialn_11_Normal));
                    p.SpacingAfter = 20f;
                    cell = new PdfPCell();
                    cell.AddElement(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);
                    doc.Add(table);
                }
                else
                {
                    table = new PdfPTable(3);
                    table.TotalWidth = 620f;
                    table.LockedWidth = true;
                    widths = new float[] { 330f, 150f, 55f };
                    table.SetWidths(widths);

                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk(" ", font_Verdana_10_Bold));
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
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk("\n\n", font_Arialn_11_Bold));
                    p.Add(new Chunk(linefine));
                    p.Add(new Chunk("\n" + item.certificados.assinatura2_nome, font_Arialn_11_Normal));
                    iSpace = 70 - (item.certificados.assinatura2_cargo.Length);
                    p.Add(new Chunk("\n" + item.certificados.assinatura2_cargo + string.Empty.PadLeft(iSpace), font_Arialn_11_Normal));
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
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk(" ", font_Verdana_10_Bold));
                    p.SpacingAfter = 20f;
                    cell = new PdfPCell();
                    cell.AddElement(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);


                    doc.Add(table);
                }

                //=======================================================================


                iTextSharp.text.Image imgAux = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/Certificados/barra_azul_ipt.png"));
                imgAux.SetAbsolutePosition(0, 40);
                imgAux.ScalePercent(52);
                doc.Add(imgAux);

                imgAux = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/Certificados/plataforma_municipio.png"));
                imgAux.SetAbsolutePosition(330, 40);
                imgAux.ScalePercent(30);
                doc.Add(imgAux);

                imgAux = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/img/Homepage/logo.jpg"));
                imgAux.SetAbsolutePosition(500, 45);
                imgAux.ScalePercent(45);
                doc.Add(imgAux);

                imgAux = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/Certificados/estado_sao_paulo.png"));
                imgAux.SetAbsolutePosition(650, 40);
                imgAux.ScalePercent(20);
                doc.Add(imgAux);


                //paragrafo = new Paragraph();
                ////etipulando o alinhamneto
                //paragrafo.Clear();
                //paragrafo.Alignment = Element.ALIGN_LEFT;

                //paragrafo.Add(new Chunk("\nInstituto de Pesquisas Tecnológicas do Estado de São Paulo - IPT", font_Arialn_10_Normal));
                //paragrafo.Add(new Chunk("\nwww.ipt.br", font_Arialn_10_Normal));

                //paragrafo.SetLeading(10, 0); //Aumenta o espaço entre as linhas.

                //paragrafo.IndentationLeft = 20;
                //paragrafo.IndentationRight = 20;

                //doc.Add(paragrafo);

                ////Alterado pedido andreia 17/09/2020
                //paragrafo = new Paragraph();
                ////estipulando o alinhamneto
                //paragrafo.Clear();
                //paragrafo.Alignment = Element.ALIGN_CENTER;

                //paragrafo.Add(new Chunk("\n\n______________________________________\nProf. Dr. Eduardo Luiz Machado\nDiretor Técnico em Ensino Tecnológico", font_Tahoma_11_Normal));
                //doc.Add(paragrafo);





                //Alterado pedido andreia 17/09/2020

                //Alterado pedido andreia 17/09/2020
                //imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/img/img-cabecalho/assinaturaEduardo.png"));
                //imgCabecalho.SetAbsolutePosition(280, 40);
                //imgCabecalho.ScalePercent(77);
                //doc.Add(imgCabecalho);
                //Alterado pedido andreia 17/09/2020


                //===================================================
                //==Nova Página
                if (item.certificados.obs_folha2 != "" && item.certificados.obs_folha2 != null)
                {
                    paragrafo = new Paragraph();
                    //etipulando o alinhamneto
                    paragrafo.Clear();
                    paragrafo.Alignment = Element.ALIGN_JUSTIFIED;

                    paragrafo.Add(new Chunk("\n\n\n\n\n\n\n\n\n", font_Tahoma_10_Normal));

                    ArrayList htmlarraylist;
                    htmlarraylist = HTMLWorker.ParseToList(new StringReader(item.certificados.obs_folha2), null);
                    for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                    {
                        paragrafo.Add(htmlarraylist[k]);
                    }

                    //paragrafo.Add(new Chunk(item.certificados.obs_folha2, font_Tahoma_10_Normal));

                    paragrafo.SetLeading(12, 0); //Aumenta o espaço entre as linhas.

                    //paragrafo.IndentationLeft = 20;
                    //paragrafo.IndentationRight = 20;

                    doc.Add(paragrafo);
                }


                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

                return sArquivo;
                //if (File.Exists(Server.MapPath("~/doctos/Certificado.pdf")))
                //{
                //    Response.Clear();
                //    Response.BufferOutput = true;
                //    Response.ContentType = "application/pdf";
                //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/Certificado.pdf")));
                //    Response.WriteFile(Server.MapPath("~/doctos/Certificado.pdf"));
                //    Response.Flush();
                //    Response.End();
                //}

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão do Certificado";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-danger');", true);
                return "";
            }
        }

        private string ImprimeCertificado_3(certificados_participantes item)
        {
            try
            {
                Document doc = new Document(PageSize.A4.Rotate());//criando e estipulando o tipo da folha usada
                doc.SetMargins(70, 130, 95, 10);//estibulando o espaçamento das margens que queremos ===Antigo===(70, 130, 200, 10) //Alterado pedido andreia 17/09/2020
                doc.AddCreationDate();//adicionando as configuracoes

                string sArquivo;

                if (!System.IO.Directory.Exists(Server.MapPath("") + "\\Certificados\\" + item.id_certificado + "\\PDFs\\"))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("") + "\\Certificados\\" + item.id_certificado + "\\PDFs\\");
                }

                sArquivo = item.nome.Trim().Replace("'", " ") + "_" + item.id_certificado_participante + ".pdf";

                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("") + "\\Certificados\\" + item.id_certificado + "\\PDFs\\" + sArquivo, FileMode.Create));
                doc.Open();

                iTextSharp.text.Image imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/Certificados/cabecalho certificado t3.png"));
                //imgCabecalho.SetAbsolutePosition(0, 430);
                imgCabecalho.SetAbsolutePosition(0, 442);
                //imgCabecalho.ScalePercent(97);
                imgCabecalho.ScalePercent(70);
                doc.Add(imgCabecalho);

                imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/Certificados/rodape certificado t3.png"));
                //imgCabecalho.SetAbsolutePosition(0, 430);
                imgCabecalho.SetAbsolutePosition(0, 0);
                //imgCabecalho.ScalePercent(97);
                imgCabecalho.ScalePercent(70);
                doc.Add(imgCabecalho);

                iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
                var _bfArialNarrowNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\Arialn.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfVerdanaNormal = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\verdana.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfTahoma = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\tahoma.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var _bfAgenda = BaseFont.CreateFont(Server.MapPath("~") + "\\Content\\fonts\\agenda-light.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
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
                Font font_Arialn_24_Bold = new Font(_bfArialNarrowNormal, 24, Font.BOLD);
                Font font_Arialn_24_Normal = new Font(_bfArialNarrowNormal, 24, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_14_Normal = new Font(_bfArialNarrowNormal, 14, Font.NORMAL);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_12_Bold = new Font(_bfArialNarrowNormal, 12, Font.BOLD);
                Font font_Arialn_12_Normal = new Font(_bfArialNarrowNormal, 12, Font.NORMAL);
                Font font_Arialn_11_Bold = new Font(_bfArialNarrowNormal, 11, Font.BOLD);
                Font font_Arialn_11_Normal = new Font(_bfArialNarrowNormal, 11, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_7_Bold = new Font(_bfArialNarrowNormal, 7, Font.BOLD);
                Font font_Arialn_7_Normal = new Font(_bfArialNarrowNormal, 7, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);

                Font font_Tahoma_40_Bold = new Font(_bfTahoma, 40, Font.BOLD);
                Font font_Tahoma_40_Normal = new Font(_bfTahoma, 40, Font.NORMAL);
                Font font_Tahoma_30_Bold = new Font(_bfTahoma, 30, Font.BOLD);
                Font font_Tahoma_25_Bold = new Font(_bfTahoma, 25, Font.BOLD);
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

                Font font_Agenda_24_Bold = new Font(_bfAgenda, 24, Font.BOLD);
                Font font_Agenda_24_Normal = new Font(_bfAgenda, 24, Font.NORMAL);
                Font font_Agenda_14_Bold = new Font(_bfAgenda, 14, Font.BOLD);
                Font font_Agenda_14_Normal = new Font(_bfAgenda, 14, Font.NORMAL);
                Font font_Agenda_10_Bold = new Font(_bfAgenda, 10, Font.BOLD);
                Font font_Agenda_10_Normal = new Font(_bfAgenda, 10, Font.NORMAL);
                Font font_Agenda_12_Bold = new Font(_bfAgenda, 12, Font.BOLD);
                Font font_Agenda_12_Normal = new Font(_bfAgenda, 12, Font.NORMAL);
                Font font_Agenda_11_Bold = new Font(_bfAgenda, 11, Font.BOLD);
                Font font_Agenda_11_Normal = new Font(_bfAgenda, 11, Font.NORMAL);
                Font font_Agenda_9_Bold = new Font(_bfAgenda, 9, Font.BOLD);
                Font font_Agenda_9_Normal = new Font(_bfAgenda, 9, Font.NORMAL);
                Font font_Agenda_8_Bold = new Font(_bfAgenda, 8, Font.BOLD);
                Font font_Agenda_8_Normal = new Font(_bfAgenda, 8, Font.NORMAL);
                Font font_Agenda_7_Bold = new Font(_bfAgenda, 7, Font.BOLD);
                Font font_Agenda_7_Normal = new Font(_bfAgenda, 7, Font.NORMAL);
                Font font_Agenda_6_Normal = new Font(_bfAgenda, 6, Font.NORMAL);

                Color FontColor_Cinza = new Color(204, 204, 204);
                Color FontColor_CinzaClaro = new Color(229, 229, 229);



                //criando a variavel para paragrafo
                Paragraph paragrafo = new Paragraph();
                //estipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_LEFT;

                //paragrafo.Add(new Chunk("Certificado\n\n\n ", font_Tahoma_40_Normal));
                paragrafo.Add(new Chunk(" \n\n\n ", font_Tahoma_40_Normal));

                doc.Add(paragrafo);
                //Alterado pedido andreia 17/09/2020

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_RIGHT;

                string sAux = item.certificados.ano.ToString();
                paragrafo.Add(new Chunk(item.numero_seq + "-" + item.interno_externo + "/" + sAux.Substring(sAux.Length - 2, 2), font_Tahoma_15_Normal));
                paragrafo.Add(new Chunk("\n\n\n\n", font_Agenda_14_Bold));
                doc.Add(paragrafo);

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_LEFT;

                paragrafo.Add(new Chunk(item.certificados.campo1a, font_Agenda_12_Normal));
                paragrafo.Add(new Chunk(" " + item.nome.Trim().ToUpper() + "\n\n", font_Agenda_24_Bold));

                paragrafo.SetLeading(25, 0); //Aumenta o espaço entre as linhas.

                //paragrafo.IndentationLeft = 20;
                //paragrafo.IndentationRight = 20;

                doc.Add(paragrafo);

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_JUSTIFIED;

                paragrafo.Add(new Chunk(item.certificados.campo2a, font_Agenda_12_Normal));
                paragrafo.Add(new Chunk(" " + item.certificados.evento + "\n\n", font_Agenda_12_Bold));

                paragrafo.Add(new Chunk(item.certificados.campo3a, font_Agenda_12_Normal));
                paragrafo.Add(new Chunk(" " + item.certificados.campo3b + "\n\n", font_Agenda_12_Bold));

                paragrafo.SetLeading(18, 0); //Aumenta o espaço entre as linhas.

                //paragrafo.IndentationLeft = 20;
                //paragrafo.IndentationRight = 20;

                doc.Add(paragrafo);


                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_RIGHT;

                paragrafo.Add(new Chunk(item.certificados.campo3c, font_Agenda_12_Normal));
                paragrafo.Add(new Chunk(" " + item.certificados.campo3d + "\n\n", font_Agenda_12_Bold));

                paragrafo.Add(new Chunk("São Paulo, " + DateTime.Today.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("PT-pt")) + ".\n\n\n", font_Agenda_12_Normal));
                doc.Add(paragrafo);

                //=== Assinaturas ========================================================
                if (item.certificados.palestrante != 1)
                {
                    imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/AssinaturasDiversas/" + item.certificados.assinatura1_imagem));
                    //imgCabecalho.SetAbsolutePosition(0, 430);
                    imgCabecalho.SetAbsolutePosition(145, 110);
                    //imgCabecalho.ScalePercent(97);
                    //imgCabecalho.ScalePercent(70);
                    imgCabecalho.ScaleAbsoluteHeight(90);
                    imgCabecalho.ScaleAbsoluteWidth(160);
                    doc.Add(imgCabecalho);

                    imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/AssinaturasDiversas/" + item.certificados.assinatura2_imagem));
                    //imgCabecalho.SetAbsolutePosition(0, 430);
                    imgCabecalho.SetAbsolutePosition(500, 110);
                    //imgCabecalho.ScalePercent(97);
                    imgCabecalho.ScaleAbsoluteHeight(90);
                    imgCabecalho.ScaleAbsoluteWidth(160);
                    doc.Add(imgCabecalho);
                }
                else
                {
                    imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/AssinaturasDiversas/" + item.certificados.assinatura2_imagem));
                    //imgCabecalho.SetAbsolutePosition(0, 430);
                    imgCabecalho.SetAbsolutePosition(310, 110);
                    //imgCabecalho.ScalePercent(97);
                    imgCabecalho.ScaleAbsoluteHeight(90);
                    imgCabecalho.ScaleAbsoluteWidth(160);
                    doc.Add(imgCabecalho);
                }

                //Aqui é uma nova tabela de 3 Colunas ========================================================
                int iSpace = 0;

                PdfPTable table;
                float[] widths;
                PdfPCell cell;
                Paragraph p;
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);

                if (item.certificados.palestrante != 1)
                {
                    table = new PdfPTable(3);
                    table.TotalWidth = 620f;
                    table.LockedWidth = true;
                    widths = new float[] { 170f, 50f, 165f };
                    table.SetWidths(widths);

                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk("\n\n", font_Arialn_11_Bold));
                    p.Add(new Chunk(linefine));
                    p.Add(new Chunk("\n" + item.certificados.assinatura1_nome, font_Arialn_11_Normal));
                    iSpace = 70 - (item.certificados.assinatura1_cargo.Length);
                    p.Add(new Chunk("\n" + item.certificados.assinatura1_cargo + string.Empty.PadLeft(iSpace), font_Arialn_11_Normal));
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
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk(" ", font_Verdana_10_Bold));
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
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk("\n\n", font_Arialn_11_Bold));
                    p.Add(new Chunk(linefine));
                    p.Add(new Chunk("\n" + item.certificados.assinatura2_nome, font_Arialn_11_Normal));
                    iSpace = 70 - (item.certificados.assinatura2_cargo.Length);
                    p.Add(new Chunk("\n" + item.certificados.assinatura2_cargo + string.Empty.PadLeft(iSpace), font_Arialn_11_Normal));
                    p.SpacingAfter = 20f;
                    cell = new PdfPCell();
                    cell.AddElement(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);
                    doc.Add(table);
                }
                else
                {
                    table = new PdfPTable(3);
                    table.TotalWidth = 620f;
                    table.LockedWidth = true;
                    widths = new float[] { 130f, 150f, 105f };
                    table.SetWidths(widths);

                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk(" ", font_Verdana_10_Bold));
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
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk("\n\n", font_Arialn_11_Bold));
                    p.Add(new Chunk(linefine));
                    p.Add(new Chunk("\n" + item.certificados.assinatura2_nome, font_Arialn_11_Normal));
                    iSpace = 70 - (item.certificados.assinatura2_cargo.Length);
                    p.Add(new Chunk("\n" + item.certificados.assinatura2_cargo + string.Empty.PadLeft(iSpace), font_Arialn_11_Normal));
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
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk(" ", font_Verdana_10_Bold));
                    p.SpacingAfter = 20f;
                    cell = new PdfPCell();
                    cell.AddElement(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);


                    doc.Add(table);
                }

                //=======================================================================

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_LEFT;

                paragrafo.Add(new Chunk("\nInstituto de Pesquisas Tecnológicas do Estado de São Paulo - IPT", font_Agenda_10_Normal));
                paragrafo.Add(new Chunk("\nwww.ipt.br", font_Agenda_10_Normal));

                paragrafo.SetLeading(10, 0); //Aumenta o espaço entre as linhas.

                //paragrafo.IndentationLeft = 20;
                //paragrafo.IndentationRight = 20;

                doc.Add(paragrafo);

                ////Alterado pedido andreia 17/09/2020
                //paragrafo = new Paragraph();
                ////estipulando o alinhamneto
                //paragrafo.Clear();
                //paragrafo.Alignment = Element.ALIGN_CENTER;

                //paragrafo.Add(new Chunk("\n\n______________________________________\nProf. Dr. Eduardo Luiz Machado\nDiretor Técnico em Ensino Tecnológico", font_Tahoma_11_Normal));
                //doc.Add(paragrafo);





                //Alterado pedido andreia 17/09/2020

                //Alterado pedido andreia 17/09/2020
                //imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/img/img-cabecalho/assinaturaEduardo.png"));
                //imgCabecalho.SetAbsolutePosition(280, 40);
                //imgCabecalho.ScalePercent(77);
                //doc.Add(imgCabecalho);
                //Alterado pedido andreia 17/09/2020

                //===================================================
                //==Nova Página
                if (item.certificados.obs_folha2 != "" && item.certificados.obs_folha2 != null)
                {
                    paragrafo = new Paragraph();
                    //etipulando o alinhamneto
                    paragrafo.Clear();
                    paragrafo.Alignment = Element.ALIGN_JUSTIFIED;

                    paragrafo.Add(new Chunk("\n\n\n\n\n\n\n\n\n", font_Tahoma_10_Normal));

                    ArrayList htmlarraylist;
                    htmlarraylist = HTMLWorker.ParseToList(new StringReader(item.certificados.obs_folha2), null);
                    for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                    {
                        paragrafo.Add(htmlarraylist[k]);
                    }

                    //paragrafo.Add(new Chunk(item.certificados.obs_folha2, font_Tahoma_10_Normal));

                    paragrafo.SetLeading(12, 0); //Aumenta o espaço entre as linhas.

                    //paragrafo.IndentationLeft = 20;
                    //paragrafo.IndentationRight = 20;

                    doc.Add(paragrafo);
                }


                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

                return sArquivo;
                //if (File.Exists(Server.MapPath("~/doctos/Certificado.pdf")))
                //{
                //    Response.Clear();
                //    Response.BufferOutput = true;
                //    Response.ContentType = "application/pdf";
                //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/Certificado.pdf")));
                //    Response.WriteFile(Server.MapPath("~/doctos/Certificado.pdf"));
                //    Response.Flush();
                //    Response.End();
                //}

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão do Certificado";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-danger');", true);
                return "";
            }
        }

        private string ImprimeCertificado_4(certificados_participantes item)
        {
            try
            {
                Document doc = new Document(PageSize.A4.Rotate());//criando e estipulando o tipo da folha usada
                doc.SetMargins(70, 130, 95, 10);//estibulando o espaçamento das margens que queremos ===Antigo===(70, 130, 200, 10) //Alterado pedido andreia 17/09/2020
                doc.AddCreationDate();//adicionando as configuracoes

                string sArquivo;

                if (!System.IO.Directory.Exists(Server.MapPath("") + "\\Certificados\\" + item.id_certificado + "\\PDFs\\"))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("") + "\\Certificados\\" + item.id_certificado + "\\PDFs\\");
                }

                sArquivo = item.nome.Trim().Replace("'", " ") + "_" + item.id_certificado_participante + ".pdf";

                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("") + "\\Certificados\\" + item.id_certificado + "\\PDFs\\" + sArquivo, FileMode.Create));
                doc.Open();

                iTextSharp.text.Image imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/Certificados/cafe_com_tecnologia.png"));
                //imgCabecalho.SetAbsolutePosition(0, 430);
                imgCabecalho.SetAbsolutePosition(340, 480);
                //imgCabecalho.ScalePercent(97);
                imgCabecalho.ScalePercent(10);
                doc.Add(imgCabecalho);

                imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/img/Homepage/logo.jpg"));
                //imgCabecalho.SetAbsolutePosition(0, 430);
                imgCabecalho.SetAbsolutePosition(560, 480);
                //imgCabecalho.ScalePercent(97);
                imgCabecalho.ScalePercent(70);
                doc.Add(imgCabecalho);

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
                Font font_Arialn_24_Bold = new Font(_bfArialNarrowNormal, 24, Font.BOLD);
                Font font_Arialn_24_Normal = new Font(_bfArialNarrowNormal, 24, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_14_Normal = new Font(_bfArialNarrowNormal, 14, Font.NORMAL);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_12_Bold = new Font(_bfArialNarrowNormal, 12, Font.BOLD);
                Font font_Arialn_12_Normal = new Font(_bfArialNarrowNormal, 12, Font.NORMAL);
                Font font_Arialn_11_Bold = new Font(_bfArialNarrowNormal, 11, Font.BOLD);
                Font font_Arialn_11_Normal = new Font(_bfArialNarrowNormal, 11, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_7_Bold = new Font(_bfArialNarrowNormal, 7, Font.BOLD);
                Font font_Arialn_7_Normal = new Font(_bfArialNarrowNormal, 7, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);

                Font font_Tahoma_40_Bold = new Font(_bfTahoma, 40, Font.BOLD);
                Font font_Tahoma_40_Normal = new Font(_bfTahoma, 40, Font.NORMAL);
                Font font_Tahoma_30_Bold = new Font(_bfTahoma, 30, Font.BOLD);
                Font font_Tahoma_25_Bold = new Font(_bfTahoma, 25, Font.BOLD);
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



                //criando a variavel para paragrafo
                Paragraph paragrafo = new Paragraph();
                //estipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_LEFT;

                paragrafo.Add(new Chunk("Certificado\n\n\n ", font_Tahoma_40_Normal));

                doc.Add(paragrafo);
                //Alterado pedido andreia 17/09/2020

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_RIGHT;

                string sAux = item.certificados.ano.ToString();
                paragrafo.Add(new Chunk(item.numero_seq + "-" + item.interno_externo + "/" + sAux.Substring(sAux.Length - 2, 2), font_Tahoma_15_Normal));
                paragrafo.Add(new Chunk("\n\n\n\n", font_Arialn_14_Bold));
                doc.Add(paragrafo);

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_LEFT;

                paragrafo.Add(new Chunk(item.certificados.campo1a, font_Arialn_12_Normal));
                paragrafo.Add(new Chunk(" " + item.nome.Trim().ToUpper() + "\n\n", font_Arialn_24_Bold));

                paragrafo.SetLeading(25, 0); //Aumenta o espaço entre as linhas.

                //paragrafo.IndentationLeft = 20;
                //paragrafo.IndentationRight = 20;

                doc.Add(paragrafo);

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_JUSTIFIED;

                paragrafo.Add(new Chunk(item.certificados.campo2a, font_Arialn_12_Normal));
                paragrafo.Add(new Chunk(" " + item.certificados.evento + "\n\n", font_Arialn_12_Bold));

                paragrafo.Add(new Chunk(item.certificados.campo3a, font_Arialn_12_Normal));
                paragrafo.Add(new Chunk(" " + item.certificados.campo3b + "\n\n", font_Arialn_12_Bold));

                paragrafo.SetLeading(18, 0); //Aumenta o espaço entre as linhas.

                //paragrafo.IndentationLeft = 20;
                //paragrafo.IndentationRight = 20;

                doc.Add(paragrafo);


                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_RIGHT;

                paragrafo.Add(new Chunk(item.certificados.campo3c, font_Arialn_12_Normal));
                paragrafo.Add(new Chunk(" " + item.certificados.campo3d + "\n\n", font_Arialn_12_Bold));

                paragrafo.Add(new Chunk("São Paulo, " + DateTime.Today.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("PT-pt")) + ".\n\n\n", font_Arialn_12_Normal));
                doc.Add(paragrafo);

                //=== Assinaturas ========================================================
                if (item.certificados.palestrante != 1)
                {
                    imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/AssinaturasDiversas/" + item.certificados.assinatura1_imagem));
                    //imgCabecalho.SetAbsolutePosition(0, 430);
                    imgCabecalho.SetAbsolutePosition(145, 110);
                    //imgCabecalho.ScalePercent(97);
                    //imgCabecalho.ScalePercent(70);
                    imgCabecalho.ScaleAbsoluteHeight(90);
                    imgCabecalho.ScaleAbsoluteWidth(160);
                    doc.Add(imgCabecalho);

                    imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/AssinaturasDiversas/" + item.certificados.assinatura2_imagem));
                    //imgCabecalho.SetAbsolutePosition(0, 430);
                    imgCabecalho.SetAbsolutePosition(500, 110);
                    //imgCabecalho.ScalePercent(97);
                    imgCabecalho.ScaleAbsoluteHeight(90);
                    imgCabecalho.ScaleAbsoluteWidth(160);
                    doc.Add(imgCabecalho);
                }
                else
                {
                    imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/AssinaturasDiversas/" + item.certificados.assinatura2_imagem));
                    //imgCabecalho.SetAbsolutePosition(0, 430);
                    imgCabecalho.SetAbsolutePosition(310, 110);
                    //imgCabecalho.ScalePercent(97);
                    imgCabecalho.ScaleAbsoluteHeight(90);
                    imgCabecalho.ScaleAbsoluteWidth(160);
                    doc.Add(imgCabecalho);
                }


                //Aqui é uma nova tabela de 3 Colunas ========================================================
                int iSpace = 0;

                PdfPTable table;
                float[] widths;
                PdfPCell cell;
                Paragraph p;
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);
                if (item.certificados.palestrante != 1)
                {
                    table = new PdfPTable(3);
                    table.TotalWidth = 620f;
                    table.LockedWidth = true;
                    widths = new float[] { 170f, 50f, 165f };
                    table.SetWidths(widths);

                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;
                
                    p.Add(new Chunk("\n\n", font_Arialn_11_Bold));
                    p.Add(new Chunk(linefine));
                    p.Add(new Chunk("\n" + item.certificados.assinatura1_nome, font_Arialn_11_Normal));
                    iSpace = 70 - (item.certificados.assinatura1_cargo.Length);
                    p.Add(new Chunk("\n" + item.certificados.assinatura1_cargo + string.Empty.PadLeft(iSpace), font_Arialn_11_Normal));
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
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk(" ", font_Verdana_10_Bold));
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
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk("\n\n", font_Arialn_11_Bold));
                    p.Add(new Chunk(linefine));
                    p.Add(new Chunk("\n" + item.certificados.assinatura2_nome, font_Arialn_11_Normal));
                    iSpace = 70 - (item.certificados.assinatura2_cargo.Length);
                    p.Add(new Chunk("\n" + item.certificados.assinatura2_cargo + string.Empty.PadLeft(iSpace), font_Arialn_11_Normal));
                    p.SpacingAfter = 20f;
                    cell = new PdfPCell();
                    cell.AddElement(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);
                    doc.Add(table);
                }
                else
                {
                    table = new PdfPTable(3);
                    table.TotalWidth = 620f;
                    table.LockedWidth = true;
                    widths = new float[] { 130f, 150f, 105f };
                    table.SetWidths(widths);

                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk(" ", font_Verdana_10_Bold));
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
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk("\n\n", font_Arialn_11_Bold));
                    p.Add(new Chunk(linefine));
                    p.Add(new Chunk("\n" + item.certificados.assinatura2_nome, font_Arialn_11_Normal));
                    iSpace = 70 - (item.certificados.assinatura2_cargo.Length);
                    p.Add(new Chunk("\n" + item.certificados.assinatura2_cargo + string.Empty.PadLeft(iSpace), font_Arialn_11_Normal));
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
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk(" ", font_Verdana_10_Bold));
                    p.SpacingAfter = 20f;
                    cell = new PdfPCell();
                    cell.AddElement(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);


                    doc.Add(table);
                }

                

                //=======================================================================

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_LEFT;

                paragrafo.Add(new Chunk("\nInstituto de Pesquisas Tecnológicas do Estado de São Paulo - IPT", font_Arialn_10_Normal));
                paragrafo.Add(new Chunk("\nwww.ipt.br", font_Arialn_10_Normal));

                paragrafo.SetLeading(10, 0); //Aumenta o espaço entre as linhas.

                //paragrafo.IndentationLeft = 20;
                //paragrafo.IndentationRight = 20;

                doc.Add(paragrafo);

                ////Alterado pedido andreia 17/09/2020
                //paragrafo = new Paragraph();
                ////estipulando o alinhamneto
                //paragrafo.Clear();
                //paragrafo.Alignment = Element.ALIGN_CENTER;

                //paragrafo.Add(new Chunk("\n\n______________________________________\nProf. Dr. Eduardo Luiz Machado\nDiretor Técnico em Ensino Tecnológico", font_Tahoma_11_Normal));
                //doc.Add(paragrafo);





                //Alterado pedido andreia 17/09/2020

                //Alterado pedido andreia 17/09/2020
                //imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/img/img-cabecalho/assinaturaEduardo.png"));
                //imgCabecalho.SetAbsolutePosition(280, 40);
                //imgCabecalho.ScalePercent(77);
                //doc.Add(imgCabecalho);
                //Alterado pedido andreia 17/09/2020


                //===================================================
                //==Nova Página
                if (item.certificados.obs_folha2 != "" && item.certificados.obs_folha2 != null)
                {

                    paragrafo = new Paragraph();

                    //etipulando o alinhamneto
                    paragrafo.Clear();
                    paragrafo.Alignment = Element.ALIGN_JUSTIFIED;

                    paragrafo.Add(new Chunk("\n\n\n\n\n\n\n\n\n", font_Tahoma_10_Normal));

                    ArrayList htmlarraylist;
                    htmlarraylist = HTMLWorker.ParseToList(new StringReader(item.certificados.obs_folha2), null);
                    for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                    {
                        paragrafo.Add(htmlarraylist[k]);
                    }

                    //paragrafo.Add(new Chunk(item.certificados.obs_folha2, font_Tahoma_10_Normal));

                    paragrafo.SetLeading(12, 0); //Aumenta o espaço entre as linhas.

                    //paragrafo.IndentationLeft = 20;
                    //paragrafo.IndentationRight = 20;

                    doc.Add(paragrafo);
                }

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

                return sArquivo;
                //if (File.Exists(Server.MapPath("~/doctos/Certificado.pdf")))
                //{
                //    Response.Clear();
                //    Response.BufferOutput = true;
                //    Response.ContentType = "application/pdf";
                //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/Certificado.pdf")));
                //    Response.WriteFile(Server.MapPath("~/doctos/Certificado.pdf"));
                //    Response.Flush();
                //    Response.End();
                //}

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão do Certificado";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-danger');", true);
                return "";
            }
        }

        private string ImprimeCertificado_5(certificados_participantes item)
        {
            try
            {
                Document doc = new Document(PageSize.A4.Rotate());//criando e estipulando o tipo da folha usada
                doc.SetMargins(70, 130, 95, 10);//estibulando o espaçamento das margens que queremos ===Antigo===(70, 130, 200, 10) //Alterado pedido andreia 17/09/2020
                doc.AddCreationDate();//adicionando as configuracoes

                string sArquivo;

                if (!System.IO.Directory.Exists(Server.MapPath("") + "\\Certificados\\" + item.id_certificado + "\\PDFs\\"))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("") + "\\Certificados\\" + item.id_certificado + "\\PDFs\\");
                }

                sArquivo = item.nome.Trim().Replace("'", " ") + "_" + item.id_certificado_participante + ".pdf";

                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("") + "\\Certificados\\" + item.id_certificado + "\\PDFs\\" + sArquivo, FileMode.Create));
                doc.Open();

                iTextSharp.text.Image imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/img/Homepage/Marca Dagua Bionanomanufatura.png"));
                //imgCabecalho.SetAbsolutePosition(0, 430);
                imgCabecalho.SetAbsolutePosition(130, 150);
                //imgCabecalho.ScalePercent(97);
                imgCabecalho.ScalePercent(70);
                doc.Add(imgCabecalho);

                imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/img/Homepage/logo.jpg"));
                //imgCabecalho.SetAbsolutePosition(0, 430);
                imgCabecalho.SetAbsolutePosition(560, 480);
                //imgCabecalho.ScalePercent(97);
                imgCabecalho.ScalePercent(70);
                doc.Add(imgCabecalho);

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
                Font font_Arialn_24_Bold = new Font(_bfArialNarrowNormal, 24, Font.BOLD);
                Font font_Arialn_24_Normal = new Font(_bfArialNarrowNormal, 24, Font.NORMAL);
                Font font_Arialn_14_Bold = new Font(_bfArialNarrowNormal, 14, Font.BOLD);
                Font font_Arialn_14_Normal = new Font(_bfArialNarrowNormal, 14, Font.NORMAL);
                Font font_Arialn_10_Bold = new Font(_bfArialNarrowNormal, 10, Font.BOLD);
                Font font_Arialn_10_Normal = new Font(_bfArialNarrowNormal, 10, Font.NORMAL);
                Font font_Arialn_12_Bold = new Font(_bfArialNarrowNormal, 12, Font.BOLD);
                Font font_Arialn_12_Normal = new Font(_bfArialNarrowNormal, 12, Font.NORMAL);
                Font font_Arialn_11_Bold = new Font(_bfArialNarrowNormal, 11, Font.BOLD);
                Font font_Arialn_11_Normal = new Font(_bfArialNarrowNormal, 11, Font.NORMAL);
                Font font_Arialn_9_Bold = new Font(_bfArialNarrowNormal, 9, Font.BOLD);
                Font font_Arialn_9_Normal = new Font(_bfArialNarrowNormal, 9, Font.NORMAL);
                Font font_Arialn_8_Bold = new Font(_bfArialNarrowNormal, 8, Font.BOLD);
                Font font_Arialn_8_Normal = new Font(_bfArialNarrowNormal, 8, Font.NORMAL);
                Font font_Arialn_7_Bold = new Font(_bfArialNarrowNormal, 7, Font.BOLD);
                Font font_Arialn_7_Normal = new Font(_bfArialNarrowNormal, 7, Font.NORMAL);
                Font font_Arialn_6_Normal = new Font(_bfArialNarrowNormal, 6, Font.NORMAL);

                Font font_Tahoma_40_Bold = new Font(_bfTahoma, 40, Font.BOLD);
                Font font_Tahoma_40_Normal = new Font(_bfTahoma, 40, Font.NORMAL);
                Font font_Tahoma_30_Bold = new Font(_bfTahoma, 30, Font.BOLD);
                Font font_Tahoma_25_Bold = new Font(_bfTahoma, 25, Font.BOLD);
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



                //criando a variavel para paragrafo
                Paragraph paragrafo = new Paragraph();
                //estipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_LEFT;

                paragrafo.Add(new Chunk("Certificado\n\n\n ", font_Tahoma_40_Normal));

                doc.Add(paragrafo);
                //Alterado pedido andreia 17/09/2020

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_RIGHT;

                string sAux = item.certificados.ano.ToString();
                paragrafo.Add(new Chunk(item.numero_seq + "-" + item.interno_externo + "/" + sAux.Substring(sAux.Length - 2, 2), font_Tahoma_15_Normal));
                paragrafo.Add(new Chunk("\n\n\n\n", font_Arialn_14_Bold));
                doc.Add(paragrafo);

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_LEFT;

                paragrafo.Add(new Chunk(item.certificados.campo1a, font_Arialn_12_Normal));
                paragrafo.Add(new Chunk(" " + item.nome.Trim().ToUpper() + "\n\n", font_Arialn_24_Bold));

                paragrafo.SetLeading(25, 0); //Aumenta o espaço entre as linhas.

                //paragrafo.IndentationLeft = 20;
                //paragrafo.IndentationRight = 20;

                doc.Add(paragrafo);

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_JUSTIFIED;

                paragrafo.Add(new Chunk(item.certificados.campo2a, font_Arialn_12_Normal));
                paragrafo.Add(new Chunk(" " + item.certificados.evento + "\n\n", font_Arialn_12_Bold));

                paragrafo.Add(new Chunk(item.certificados.campo3a, font_Arialn_12_Normal));
                paragrafo.Add(new Chunk(" " + item.certificados.campo3b + "\n\n", font_Arialn_12_Bold));

                paragrafo.SetLeading(18, 0); //Aumenta o espaço entre as linhas.

                //paragrafo.IndentationLeft = 20;
                //paragrafo.IndentationRight = 20;

                doc.Add(paragrafo);


                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_RIGHT;

                paragrafo.Add(new Chunk(item.certificados.campo3c, font_Arialn_12_Normal));
                paragrafo.Add(new Chunk(" " + item.certificados.campo3d + "\n\n", font_Arialn_12_Bold));

                paragrafo.Add(new Chunk("São Paulo, " + DateTime.Today.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("PT-pt")) + ".\n\n\n", font_Arialn_12_Normal));
                doc.Add(paragrafo);

                //=== Assinaturas ========================================================

                if (item.certificados.palestrante != 1)
                {
                    imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/AssinaturasDiversas/" + item.certificados.assinatura1_imagem));
                    //imgCabecalho.SetAbsolutePosition(0, 430);
                    imgCabecalho.SetAbsolutePosition(145, 110);
                    //imgCabecalho.ScalePercent(97);
                    //imgCabecalho.ScalePercent(70);
                    imgCabecalho.ScaleAbsoluteHeight(90);
                    imgCabecalho.ScaleAbsoluteWidth(160);
                    doc.Add(imgCabecalho);

                    imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/AssinaturasDiversas/" + item.certificados.assinatura2_imagem));
                    //imgCabecalho.SetAbsolutePosition(0, 430);
                    imgCabecalho.SetAbsolutePosition(500, 110);
                    //imgCabecalho.ScalePercent(97);
                    imgCabecalho.ScaleAbsoluteHeight(90);
                    imgCabecalho.ScaleAbsoluteWidth(160);
                    doc.Add(imgCabecalho);
                }
                else
                {
                    imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/AssinaturasDiversas/" + item.certificados.assinatura2_imagem));
                    //imgCabecalho.SetAbsolutePosition(0, 430);
                    imgCabecalho.SetAbsolutePosition(310, 110);
                    //imgCabecalho.ScalePercent(97);
                    imgCabecalho.ScaleAbsoluteHeight(90);
                    imgCabecalho.ScaleAbsoluteWidth(160);
                    doc.Add(imgCabecalho);
                }


                //Aqui é uma nova tabela de 3 Colunas ========================================================
                int iSpace = 0;

                PdfPTable table;
                float[] widths;
                PdfPCell cell;
                Paragraph p;
                var linefine = new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1);

                if (item.certificados.palestrante != 1)
                {
                    table = new PdfPTable(3);
                    table.TotalWidth = 620f;
                    table.LockedWidth = true;
                    widths = new float[] { 170f, 50f, 165f };
                    table.SetWidths(widths);

                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk("\n\n", font_Arialn_11_Bold));
                    p.Add(new Chunk(linefine));
                    p.Add(new Chunk("\n" + item.certificados.assinatura1_nome, font_Arialn_11_Normal));
                    iSpace = 70 - (item.certificados.assinatura1_cargo.Length);
                    p.Add(new Chunk("\n" + item.certificados.assinatura1_cargo + string.Empty.PadLeft(iSpace), font_Arialn_11_Normal));
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
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk(" ", font_Verdana_10_Bold));
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
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk("\n\n", font_Arialn_11_Bold));
                    p.Add(new Chunk(linefine));
                    p.Add(new Chunk("\n" + item.certificados.assinatura2_nome, font_Arialn_11_Normal));
                    iSpace = 70 - (item.certificados.assinatura2_cargo.Length);
                    p.Add(new Chunk("\n" + item.certificados.assinatura2_cargo + string.Empty.PadLeft(iSpace), font_Arialn_11_Normal));
                    p.SpacingAfter = 20f;
                    cell = new PdfPCell();
                    cell.AddElement(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);
                    doc.Add(table);
                }
                else
                {
                    table = new PdfPTable(3);
                    table.TotalWidth = 620f;
                    table.LockedWidth = true;
                    widths = new float[] { 130f, 150f, 105f };
                    table.SetWidths(widths);

                    cell = new PdfPCell();
                    p = new Paragraph();
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk(" ", font_Verdana_10_Bold));
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
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk("\n\n", font_Arialn_11_Bold));
                    p.Add(new Chunk(linefine));
                    p.Add(new Chunk("\n" + item.certificados.assinatura2_nome, font_Arialn_11_Normal));
                    iSpace = 70 - (item.certificados.assinatura2_cargo.Length);
                    p.Add(new Chunk("\n" + item.certificados.assinatura2_cargo + string.Empty.PadLeft(iSpace), font_Arialn_11_Normal));
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
                    p.Alignment = Element.ALIGN_CENTER;

                    p.Add(new Chunk(" ", font_Verdana_10_Bold));
                    p.SpacingAfter = 20f;
                    cell = new PdfPCell();
                    cell.AddElement(p);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    //cell.FixedHeight = 25f;
                    table.AddCell(cell);


                    doc.Add(table);
                }

                //=======================================================================

                paragrafo = new Paragraph();
                //etipulando o alinhamneto
                paragrafo.Clear();
                paragrafo.Alignment = Element.ALIGN_LEFT;

                paragrafo.Add(new Chunk("\nInstituto de Pesquisas Tecnológicas do Estado de São Paulo - IPT", font_Arialn_10_Normal));
                paragrafo.Add(new Chunk("\nwww.ipt.br", font_Arialn_10_Normal));

                paragrafo.SetLeading(10, 0); //Aumenta o espaço entre as linhas.

                //paragrafo.IndentationLeft = 20;
                //paragrafo.IndentationRight = 20;

                doc.Add(paragrafo);

                ////Alterado pedido andreia 17/09/2020
                //paragrafo = new Paragraph();
                ////estipulando o alinhamneto
                //paragrafo.Clear();
                //paragrafo.Alignment = Element.ALIGN_CENTER;

                //paragrafo.Add(new Chunk("\n\n______________________________________\nProf. Dr. Eduardo Luiz Machado\nDiretor Técnico em Ensino Tecnológico", font_Tahoma_11_Normal));
                //doc.Add(paragrafo);





                //Alterado pedido andreia 17/09/2020

                //Alterado pedido andreia 17/09/2020
                //imgCabecalho = iTextSharp.text.Image.GetInstance(Context.Server.MapPath("~/img/img-cabecalho/assinaturaEduardo.png"));
                //imgCabecalho.SetAbsolutePosition(280, 40);
                //imgCabecalho.ScalePercent(77);
                //doc.Add(imgCabecalho);
                //Alterado pedido andreia 17/09/2020


                //===================================================
                //==Nova Página
                if (item.certificados.obs_folha2 != "" && item.certificados.obs_folha2 != null)
                {

                    paragrafo = new Paragraph();

                    //etipulando o alinhamneto
                    paragrafo.Clear();
                    paragrafo.Alignment = Element.ALIGN_JUSTIFIED;

                    //Retirado o espaçamento abaixo por solicitação da Longuinho no dia 21/09/2022
                    //paragrafo.Add(new Chunk("\n\n\n\n\n\n\n\n\n", font_Tahoma_10_Normal));
                    paragrafo.Add(new Chunk("\n\n", font_Tahoma_10_Normal));

                    ArrayList htmlarraylist;
                    htmlarraylist = HTMLWorker.ParseToList(new StringReader(item.certificados.obs_folha2), null);
                    for (int k = 0; (k <= (htmlarraylist.Count - 1)); k++)
                    {
                        paragrafo.Add(htmlarraylist[k]);
                    }

                    //paragrafo.Add(new Chunk(item.certificados.obs_folha2, font_Tahoma_10_Normal));

                    paragrafo.SetLeading(12, 0); //Aumenta o espaço entre as linhas.

                    //paragrafo.IndentationLeft = 20;
                    //paragrafo.IndentationRight = 20;

                    doc.Add(paragrafo);
                }

                PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1.0F);
                PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
                writer.SetOpenAction(action);
                doc.Close();

                return sArquivo;
                //if (File.Exists(Server.MapPath("~/doctos/Certificado.pdf")))
                //{
                //    Response.Clear();
                //    Response.BufferOutput = true;
                //    Response.ContentType = "application/pdf";
                //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Server.MapPath("~/doctos/Certificado.pdf")));
                //    Response.WriteFile(Server.MapPath("~/doctos/Certificado.pdf"));
                //    Response.Flush();
                //    Response.End();
                //}

            }
            catch (Exception ex)
            {
                lblTituloMensagem.Text = "Erro";
                lblMensagem.Text = "Erro na rotina de impressão do Certificado";
                lblMensagem.Text = lblMensagem.Text + "<br/><br/>Descrição: " + ex.Message;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreMensagem('alert-danger');", true);
                return "";
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("outCertificado.aspx", true);
        }

        protected void btnSalvarCertificado_Click(object sender, EventArgs e)
        {
            try
            {
                usuarios usuario;
                usuario = (usuarios)Session["UsuarioLogado"];

                string sAux = "";


                if (txtDataEvento.Value.Trim() == "")
                {
                    sAux = sAux + "Digite uma Data do Evento. <br/><br/>";
                }

                if (txtNumeroSequencial.Value.Trim() == "")
                {
                    sAux = sAux + "Digite um número sequencial inicial. <br/><br/>";
                }

                if (txtAnoReferencia.Value.Trim() == "")
                {
                    sAux = sAux + "Digite um Ano do Certificado. <br/><br/>";
                }
                else if (Convert.ToInt32(txtAnoReferencia.Value.Trim()) < 2000)
                {
                    sAux = sAux + "O Ano do Certificado deve ser maior que 2000. <br/><br/>";
                }

                if (txtCampo1a.Value == "")
                {
                    sAux = sAux + "Preencher o campo da Linha 2. <br/><br/>";
                }

                if (txtCampo2a.Value == "")
                {
                    sAux = sAux + "Preencher o campo da Linha 3. <br/><br/>";
                }

                if (txtNomeEvento.Value == "")
                {
                    sAux = sAux + "Preencher o nome do Evento (curso) na Linha 3. <br/><br/>";
                }

                if (txtCampo3a.Value == "")
                {
                    sAux = sAux + "Preencher o campo 1 da Linha 4. <br/><br/>";
                }

                if (txtCampo3b.Value == "")
                {
                    sAux = sAux + "Preencher o campo 2 da Linha 4. <br/><br/>";
                }

                if (txtCampo3c.Value == "")
                {
                    sAux = sAux + "Preencher o campo 3 da Linha 4. <br/><br/>";
                }

                if (txtCampo3d.Value == "")
                {
                    sAux = sAux + "Preencher o campo 4 da Linha 4. <br/><br/>";
                }

                if (optPalestranteNao.Checked)
                {
                    if (txtImagemAssinatura1.Value.Trim() == "")
                    {
                        sAux = sAux + "Selecione a imagem da Assinatura 1. <br/><br/>";
                    }

                    if (txtNomeAssinatura1.Value == "")
                    {
                        sAux = sAux + "Preencher o nome da Assinatura 1. <br/><br/>";
                    }

                    if (txtCargoAssinatura1.Value == "")
                    {
                        sAux = sAux + "Preencher o cargo da Assinatura 1. <br/><br/>";
                    }
                }

                

                if (ddlTipoCursoCertificado.SelectedValue == "")
                {
                    sAux = sAux + "Selecione o Tipo de Curso/Palestra. <br/><br/>";
                    divColInformacoesAdicionais.Style.Add("display", "none");
                }
                else if (ddlTipoCursoCertificado.SelectedValue == "2" && txtInformacoesAdicionais.Value.Trim() == "")
                {
                    divColInformacoesAdicionais.Style.Add("display", "block");
                    sAux = sAux + "Preencher as Informações Adicionais. <br/><br/>";
                }

                if (!optTipoCertificado_1.Checked && !optTipoCertificado_2.Checked && !optTipoCertificado_3.Checked && !optTipoCertificado_4.Checked && !optTipoCertificado_5.Checked)
                {
                    sAux = sAux + "Selecionar um Tipo de Certificado. <br/><br/>";
                }

                if (sAux != "")
                {
                    if (optPalestranteSim.Checked)
                    {
                        divAssinatura.Style.Add("display", "none");
                    }
                    else
                    {
                        divAssinatura.Style.Add("display", "block");
                    }
                    lblMensagem.Text = sAux;
                    lblTituloMensagem.Text = "Atenção";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                    return;
                }

                CertificadoAplicacao aplicacaoCertificado = new CertificadoAplicacao();

                if (Session["sNewcertificados"] != null && (Boolean)Session["sNewcertificados"] != true)
                {
                    //Alteração
                    certificados item = new certificados();

                    item = (certificados)Session["certificados"];

                    if (fileArquivoParaGravar.HasFile)
                    {
                        if (fileArquivoParaGravar.FileName.IndexOf("+") != -1)
                        {
                            lblMensagem.Text = "O nome do arquivo <strong>NÃO</strong> pode conter o caracter \"+\". <br/> ";
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                            return;

                        }

                        if (!System.IO.Directory.Exists(Server.MapPath("") + "\\AssinaturasDiversas\\"))
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath("") + "\\AssinaturasDiversas\\");
                        }

                        fileArquivoParaGravar.SaveAs(Server.MapPath("") + "\\AssinaturasDiversas\\" + fileArquivoParaGravar.FileName);
                        item.assinatura1_imagem = fileArquivoParaGravar.FileName;
                    }

                    item.data_evento = Convert.ToDateTime(txtDataEvento.Value);
                    item.numero_seq_inicial = Convert.ToInt32(txtNumeroSequencial.Value);
                    item.ano = Convert.ToInt32(txtAnoReferencia.Value);
                    item.campo1a = txtCampo1a.Value;
                    item.campo2a = txtCampo2a.Value;
                    item.evento = txtNomeEvento.Value;
                    item.campo3a = txtCampo3a.Value;
                    item.campo3b = txtCampo3b.Value;
                    item.campo3c = txtCampo3c.Value;
                    item.campo3d = txtCampo3d.Value;
                    item.assinatura1_nome = txtNomeAssinatura1.Value;
                    item.assinatura1_cargo = txtCargoAssinatura1.Value;
                    item.id_certificado_tipo_curso = Convert.ToInt32(ddlTipoCursoCertificado.SelectedValue);
                    if (item.id_certificado_tipo_curso == 2)
                    {
                        item.informacao_adicional = txtInformacoesAdicionais.Value.Trim();
                    }
                    else
                    {
                        item.informacao_adicional = null;
                    }
                    item.obs_folha2 = txtObsFolha2.Value.Trim();
                    if (optTipoCertificado_1.Checked)
                    {
                        item.tipo_certificado = 1;
                    }
                    else if (optTipoCertificado_2.Checked)
                    {
                        item.tipo_certificado = 2;
                    }
                    else if (optTipoCertificado_3.Checked)
                    {
                        item.tipo_certificado = 3;
                    }
                    else if (optTipoCertificado_4.Checked)
                    {
                        item.tipo_certificado = 4;
                    }
                    else
                    {
                        item.tipo_certificado = 5;
                    }

                    if (optPalestranteSim.Checked)
                    {
                        item.palestrante = 1;
                    }
                    else
                    {
                        item.palestrante = 0;
                    }

                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario.usuario;

                    aplicacaoCertificado.AlterarItem(item);

                    if (optPalestranteSim.Checked)
                    {
                        divAssinatura.Style.Add("display", "none");
                    }
                    else
                    {
                        divAssinatura.Style.Add("display", "block");
                    }

                    lblMensagem.Text = "Certificado alterado com sucesso.";
                    lblTituloMensagem.Text = "Alteração de Certificado";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-success');fAtiva_grdDisciplina();", true);

                    Session["certificados"] = item;

                }
                else
                {
                    //Inclusão

                    certificados item = new certificados();

                    if (fileArquivoParaGravar.HasFile)
                    {
                        if (fileArquivoParaGravar.FileName.IndexOf("+") != -1)
                        {
                            lblMensagem.Text = "O nome do arquivo <strong>NÃO</strong> pode conter o caracter \"+\". <br/> ";
                            lblTituloMensagem.Text = "Atenção";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                            return;

                        }

                        if (!System.IO.Directory.Exists(Server.MapPath("") + "\\AssinaturasDiversas\\"))
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath("") + "\\AssinaturasDiversas\\");
                        }

                        fileArquivoParaGravar.SaveAs(Server.MapPath("") + "\\AssinaturasDiversas\\" + fileArquivoParaGravar.FileName);
                        item.assinatura1_imagem = fileArquivoParaGravar.FileName;
                    }
                    else if (optPalestranteNao.Checked)
                    {
                        lblMensagem.Text = "Deve-se selecionar um arquivo da Assinatura 1<br/> ";
                        lblTituloMensagem.Text = "Atenção";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "AbreModalMensagem('alert-warning');", true);
                        return;
                    }
                    
                    item.data_evento = Convert.ToDateTime(txtDataEvento.Value);
                    item.numero_seq_inicial = Convert.ToInt32(txtNumeroSequencial.Value);
                    item.ano = Convert.ToInt32(txtAnoReferencia.Value);
                    item.campo1a = txtCampo1a.Value;
                    item.campo2a = txtCampo2a.Value;
                    item.evento = txtNomeEvento.Value;
                    item.campo3a = txtCampo3a.Value;
                    item.campo3b = txtCampo3b.Value;
                    item.campo3c = txtCampo3c.Value;
                    item.campo3d = txtCampo3d.Value;
                    item.assinatura1_nome = txtNomeAssinatura1.Value;
                    item.assinatura1_cargo = txtCargoAssinatura1.Value;
                    item.id_certificado_tipo_curso = Convert.ToInt32(ddlTipoCursoCertificado.SelectedValue);
                    if (item.id_certificado_tipo_curso == 2)
                    {
                        item.informacao_adicional = txtInformacoesAdicionais.Value.Trim();
                    }
                    else
                    {
                        item.informacao_adicional = null;
                    }
                    item.obs_folha2 = txtObsFolha2.Value.Trim();
                    imgAssinatura1.Src = "AssinaturasDiversas\\" + item.assinatura1_imagem;

                    ////======================
                    item.assinatura2_imagem = "Eduardo Machado.PNG";
                    item.assinatura2_nome = "Dr. Eduardo Luiz Machado";
                    item.assinatura2_cargo = "Diretor Técnico em Ensino Tecnológico";
                    item.usuario = usuario.usuario;

                    item.data_cadastro = DateTime.Now;
                    item.data_alteracao = item.data_cadastro;
                    item.situacao = 1;

                    if (optTipoCertificado_1.Checked)
                    {
                        item.tipo_certificado = 1;
                    }
                    else if (optTipoCertificado_2.Checked)
                    {
                        item.tipo_certificado = 2;
                    }
                    else if (optTipoCertificado_3.Checked)
                    {
                        item.tipo_certificado = 3;
                    }
                    else
                    {
                        item.tipo_certificado = 4;
                    }

                    if (optPalestranteSim.Checked)
                    {
                        item.palestrante = 1;
                        divAssinatura.Style.Add("display", "none");
                    }
                    else
                    {
                        item.palestrante = 0;
                        divAssinatura.Style.Add("display", "block");
                    }

                    item = aplicacaoCertificado.CriarItem(item);

                    

                    if (item != null)
                    {
                        Session["certificados"] = item;
                        Session.Add("sNewcertificados", false);
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