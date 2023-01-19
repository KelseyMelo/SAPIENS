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
    public partial class consultaInscricao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sLogado;
            sLogado = (string)Session["logado"];

            if (sLogado != "phorte" && sLogado != "master")
            {
                Response.Redirect("consultaInscricaoLogin.aspx", true);
            }
            
            if (!Page.IsPostBack)
            {
                if (sLogado == "master")
                {
                    divOrigem.Visible = true;
                }
                else
                {
                    divOrigem.Visible = false;
                }

                divOrigem.Visible = false;
                //Colocado para suprimir a escolha da opção de origem 10/05/2022

                
                optSituacaoIpt.Checked = false;
                optSituacaoPhorte.Checked = false;
                optSituacaoTodos.Checked = true;

            }
            else
            {
                if (grdCandidato.Rows.Count > 0)
                {
                    grdCandidato.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                
            }

        }

        protected void btnPerquisa_Click(object sender, EventArgs e)
        {
            string sLogado;
            sLogado = (string)Session["logado"];
            InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
            List<fichas_inscricao> lista = new List<fichas_inscricao>();
            fichas_inscricao item = new fichas_inscricao();
            DateTime dt = new DateTime();
            string qUsuario;
            if (txtDataInicio.Value.Trim() != "")
            {
                item.data_inscricao = Convert.ToDateTime(txtDataInicio.Value);
            }
            if (txtDataFim.Value.Trim() != "")
            {
                dt = Convert.ToDateTime(txtDataFim.Value.Trim() + " 23:59:59");
            }
            if (sLogado == "master")
            {
                if (optSituacaoTodos.Checked)
                {
                    qUsuario = "";
                }
                else if (optSituacaoPhorte.Checked)
                {
                    qUsuario = "PHORTE";
                }
                else
                {
                    qUsuario = "IPT";
                }

                //Colocado para suprimir a escolha da opção de origem 10/05/2022
                qUsuario = "";

            }
            else
            {
                qUsuario = "PHORTE";
            }


            List<grade> listaGrade = new List<grade>();
            grade itemGrade;
            
            lista = aplicacaoInscricao.ListaInscrisao(item, dt, qUsuario);

            foreach (var elemento in lista)
            {
                itemGrade = new grade();
                itemGrade.P0 = elemento.nome;
                itemGrade.P1 = System.Text.RegularExpressions.Regex.Replace(elemento.cpf, @"(\d{3})(\d{3})(\d{3})(\d{2})", "$1.$2.$3-$4");
                itemGrade.P2 = String.Format("{0:dd/MM/yyyy}", elemento.data_inscricao);
                itemGrade.P3 = elemento.endereco_res + ", " + elemento.numero_res + " " + elemento.complemento_res;
                itemGrade.P4 = elemento.bairro_res;
                itemGrade.P5 = elemento.cidade_res;
                itemGrade.P6 = elemento.estado_res;
                itemGrade.P7 = elemento.cep_res;
                itemGrade.P8 = System.Text.RegularExpressions.Regex.Replace(elemento.celular_res, @"(\d{2})(\d{1})(\d{4})(\d{4})", "($1) $2.$3-$4");
                if (elemento.boletos.Count !=0)
                {
                    if (elemento.historico_inscricao.Any(x => x.status == "Inscrição Paga"))
                    {
                        itemGrade.P9 = "Inscrição Paga";
                    }
                    else
                    {
                        itemGrade.P9 = "<span class=\"text-danger\">Inscrição não Paga</span>";
                    }

                    if (elemento.boletos.FirstOrDefault().usuario == "PHORTE")
                    {
                        itemGrade.P10 = elemento.boletos.FirstOrDefault().usuario;
                    }
                    else
                    {
                        itemGrade.P10 = "IPT";
                    }
                }
                else
                {
                    itemGrade.P9 = "Sem boleto";
                    itemGrade.P10 = "Sem boleto";
                }

                itemGrade.P11 = elemento.cursos.nome;

                itemGrade.P12 = elemento.email_res;

                listaGrade.Add(itemGrade);

                foreach (var elemento2 in elemento.boletos)
                {
                    
                }
            }

            grdCandidato.DataSource = listaGrade;
            grdCandidato.DataBind();

            if (lista.Count > 0)

            {
                grdCandidato.UseAccessibleHeader = true;
                grdCandidato.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultados.Visible = false;
                grdCandidato.Visible = true;
            }
            else
            {
                msgSemResultados.Visible = true;
            }
        }

        protected void btnSair_Click(object sender, EventArgs e)
        {

            Session["logado"] = "";
            Response.Redirect("consultaInscricaoLogin.aspx", true);
        }

        public void grdCandidato_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                int linha = Convert.ToInt32(e.CommandArgument);
                int idPeriodo = Convert.ToInt32(grdCandidato.DataKeys[linha].Values[0]);
                int idCurso = Convert.ToInt32(grdCandidato.DataKeys[linha].Values[1]);
                periodo_inscricao_curso item = new periodo_inscricao_curso();
                item.id_periodo = idPeriodo;
                item.id_curso = idCurso;
                InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();

                item = aplicacaoInscricao.BuscaItem_periodo_inscricao_curso(item);
                Session["periodo_inscricao_curso"] = item;
                Session["origem"] = hOrigem.Value;

                Response.Redirect("fichaInscricao.aspx", true);
            }
        }

        public class grade
        {
            private string _P0;
            private string _P1;
            private string _P2;
            private string _P3;
            private string _P4;
            private string _P5;
            private string _P6;
            private string _P7;
            private string _P8;
            private string _P9;
            private string _P10;
            private string _P11;
            private string _P12;
            private string _P13;
            private string _P14;
            private string _P15;

            public string P0
            {
                get
                {
                    return _P0;
                }
                set
                {
                    _P0 = value;
                }
            }

            public string P1
            {
                get
                {
                    return _P1;
                }
                set
                {
                    _P1 = value;
                }
            }

            public string P2
            {
                get
                {
                    return _P2;
                }
                set
                {
                    _P2 = value;
                }
            }

            public string P3
            {
                get
                {
                    return _P3;
                }
                set
                {
                    _P3 = value;
                }
            }

            public string P4
            {
                get
                {
                    return _P4;
                }
                set
                {
                    _P4 = value;
                }
            }

            public string P5
            {
                get
                {
                    return _P5;
                }
                set
                {
                    _P5 = value;
                }
            }

            public string P6
            {
                get
                {
                    return _P6;
                }
                set
                {
                    _P6 = value;
                }
            }

            public string P7
            {
                get
                {
                    return _P7;
                }
                set
                {
                    _P7 = value;
                }
            }

            public string P8
            {
                get
                {
                    return _P8;
                }
                set
                {
                    _P8 = value;
                }
            }

            public string P9
            {
                get
                {
                    return _P9;
                }
                set
                {
                    _P9 = value;
                }
            }

            public string P10
            {
                get
                {
                    return _P10;
                }
                set
                {
                    _P10 = value;
                }
            }

            public string P11
            {
                get
                {
                    return _P11;
                }
                set
                {
                    _P11 = value;
                }
            }

            public string P12
            {
                get
                {
                    return _P12;
                }
                set
                {
                    _P12 = value;
                }
            }

            public string P13
            {
                get
                {
                    return _P13;
                }
                set
                {
                    _P13 = value;
                }
            }

            public string P14
            {
                get
                {
                    return _P14;
                }
                set
                {
                    _P14 = value;
                }
            }

            public string P15
            {
                get
                {
                    return _P15;
                }
                set
                {
                    _P15 = value;
                }
            }
        }
    }
}