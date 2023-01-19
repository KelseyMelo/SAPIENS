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
    public partial class homeInscricoes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hOrigem.Value =  HttpContext.Current.Request["p"];

                string sAux = HttpContext.Current.Request["curso"];

                InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
                List<periodo_inscricao> lista = new List<periodo_inscricao>();
                periodo_inscricao item = new periodo_inscricao();
                item.data_inicio = DateTime.Today;
                item.data_fim = DateTime.Today;
                List<grade> listaGrade = new List<grade>();
                grade itemGrade;

                lista = aplicacaoInscricao.ListaPeriodoInscricao(item);

                foreach (var elemento in lista)
                {
                    foreach (var elemento2 in elemento.periodo_inscricao_curso)
                    {
                        if (elemento2.cursos.sigla == sAux || sAux == null)
                        {
                            itemGrade = new grade();
                            itemGrade.P0 = elemento.id_periodo.ToString();
                            itemGrade.P1 = elemento2.id_curso.ToString();
                            itemGrade.P2 = elemento2.cursos.tipos_curso.tipo_curso;
                            itemGrade.P3 = elemento2.cursos.nome;
                            itemGrade.P4 = "de " + String.Format("{0:dd/MM/yyyy}", elemento.data_inicio) + "<br /> à " + String.Format("{0:dd/MM/yyyy}", elemento.data_fim);
                            if (elemento2.valor > 0)
                            {
                                itemGrade.P5 = string.Format("{0:C}", elemento2.valor);
                            }
                            else
                            {
                                itemGrade.P5 = "Isento";
                            }

                            listaGrade.Add(itemGrade);
                        }
                        
                    }
                }

                if (listaGrade.Count > 0)

                {
                    grdCursoDisponivel.DataSource = listaGrade;
                    grdCursoDisponivel.DataBind();

                    grdCursoDisponivel.UseAccessibleHeader = true;
                    grdCursoDisponivel.HeaderRow.TableSection = TableRowSection.TableHeader;
                    msgSemResultados.Visible = false;
                    grdCursoDisponivel.Visible = true;
                }
                else
                {
                    msgSemResultados.Visible = true;
                }
            }
            else
            {
                grdCursoDisponivel.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }

        public void grdCursoDisponivel_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                int linha = Convert.ToInt32(e.CommandArgument);
                int idPeriodo = Convert.ToInt32(grdCursoDisponivel.DataKeys[linha].Values[0]);
                int idCurso = Convert.ToInt32(grdCursoDisponivel.DataKeys[linha].Values[1]);
                periodo_inscricao_curso item = new periodo_inscricao_curso();
                item.id_periodo = idPeriodo;
                item.id_curso = idCurso;
                InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();

                item = aplicacaoInscricao.BuscaItem_periodo_inscricao_curso(item);
                Session["periodo_inscricao_curso"]= item;
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