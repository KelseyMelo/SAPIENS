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
    public partial class proRelacaoInscritos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 5)) // Relatório de Inscritos - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
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

                InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
                List<periodo_inscricao> listaPeriodo = new List<periodo_inscricao>();

                if (usuario.id_grupo_acesso == 10) //Grupo Coordenador
                {
                    //É do grupo coordenador então pegar todos os cursos em que ele é coordenador
                    ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
                    professores item_professor = new professores();

                    if (usuario.usuario.Substring(usuario.usuario.Length - 1, 1) == "p")
                    {
                        item_professor.id_professor = Convert.ToInt32(usuario.usuario.Substring(0, usuario.usuario.Length - 1));
                        item_professor = aplicacaoProfessor.BuscaItem(item_professor);
                    }
                    else
                    {
                        item_professor.cpf = usuario.usuario;
                        item_professor = aplicacaoProfessor.BuscaItem_byCPF(item_professor);
                    }

                    if (item_professor != null)
                    {
                        var qIdCurso = item_professor.cursos_coordenadores.Select(x => x.id_curso).ToArray();

                        listaPeriodo = aplicacaoInscricao.ListaPeriodoInscricao(qIdCurso);
                    }

                }
                else
                {
                    int[] qIdCurso = new int[1];
                    qIdCurso[0] = 0;

                    listaPeriodo = aplicacaoInscricao.ListaPeriodoInscricao(qIdCurso);
                }

                var listaPeriodo2 = from elemento in listaPeriodo
                                    select new
                                    {
                                        id_periodo = elemento.id_periodo,
                                        periodo = elemento.quadrimestre + " de " + String.Format("{0:dd/MM/yyyy}", elemento.data_inicio) + " à " + String.Format("{0:dd/MM/yyyy}", elemento.data_fim)
                                    };

                ddlPeriodoInscritos.Items.Clear();
                ddlPeriodoInscritos.DataSource = listaPeriodo2;
                ddlPeriodoInscritos.DataValueField = "id_periodo";
                ddlPeriodoInscritos.DataTextField = "periodo";
                ddlPeriodoInscritos.DataBind();
                ddlPeriodoInscritos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecione um Período", ""));
                ddlPeriodoInscritos.SelectedValue = "";

                if (Session["aFiltroRelacaoInscritos"] != null)
                {
                    CarregarDados();
                }

            }
            else
            {
                if (grdRelacaoInscrito.Rows.Count != 0)
                {
                    grdRelacaoInscrito.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (grdPesquisa.Rows.Count != 0)
                {
                    grdPesquisa.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        private void CarregarDados()
        {
            string[] aFiltroRelacaoInscritos = new string[2];

            periodo_inscricao_curso item = new periodo_inscricao_curso();

            aFiltroRelacaoInscritos = (string[])Session["aFiltroRelacaoInscritos"];

            if (aFiltroRelacaoInscritos[0] != "" && aFiltroRelacaoInscritos[0] != null)
            {
                item.id_periodo = Convert.ToInt32(aFiltroRelacaoInscritos[0]);
                ddlPeriodoInscritos.SelectedValue = aFiltroRelacaoInscritos[0];
                ddlPeriodoInscritos_SelectedIndexChanged(null, null);
            }

            if (aFiltroRelacaoInscritos[1] != "" && aFiltroRelacaoInscritos[1] != null)
            {
                item.id_curso = Convert.ToInt32(aFiltroRelacaoInscritos[1]);
                ddlCursoInscritos.SelectedValue = aFiltroRelacaoInscritos[1];
            }


            //Session["aFiltroProfessor"] = aFiltroProfessor;
            InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
            List<periodo_inscricao_curso> lista =  aplicacaoInscricao.ListaPeriodoInscricaoCurso(item);
            grdRelacaoInscrito.DataSource = lista;
            grdRelacaoInscrito.DataBind();

            if (lista.Count > 0)
            {
                grdRelacaoInscrito.UseAccessibleHeader = true;
                grdRelacaoInscrito.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultados.Visible = false;
                grdRelacaoInscrito.Visible = true;

                Session["lista_periodo_inscricao_curso"] = lista;

                if (lista.Any(x=> x.fichas_inscricao.Count > 0))
                {
                    divPesquisa.Visible = true;
                    GridAux item_grid;
                    List<GridAux> lista_grid = new List<GridAux>();
                    fichas_inscricao item_ficha = new fichas_inscricao();
                    item_ficha.id_periodo_inscricao = item.id_periodo;
                    item_ficha.id_curso = item.id_curso;
                    DateTime d =  new DateTime();
                    string sAux_0="";
                    int iAux = 1;

                    List<fichas_inscricao> lista_ficha = aplicacaoInscricao.ListaInscrisao(item_ficha,d,"").OrderBy(x=> x.pesquisamala).ThenBy(x=> x.pesquisaoutros).ToList();

                    foreach (var elemento in lista_ficha)
                    {
                        if (sAux_0 != (elemento.pesquisamala + " " + elemento.pesquisaoutros))
                        {
                            if (sAux_0 == "")
                            {
                                iAux = 1;
                                sAux_0 = (elemento.pesquisamala + " " + elemento.pesquisaoutros);
                            }
                            else
                            {
                                item_grid = new GridAux();
                                item_grid.P1 = sAux_0;
                                item_grid.P2 = iAux.ToString();
                                lista_grid.Add(item_grid);
                                sAux_0 = (elemento.pesquisamala + " " + elemento.pesquisaoutros);
                                iAux = 1;
                            }
                        }
                        else
                        {
                            iAux = iAux + 1;
                        }
                    }

                    item_grid = new GridAux();
                    item_grid.P1 = sAux_0;
                    item_grid.P2 = iAux.ToString();
                    lista_grid.Add(item_grid);

                    grdPesquisa.DataSource = lista_grid;
                    grdPesquisa.DataBind();

                    grdPesquisa.UseAccessibleHeader = true;
                    grdPesquisa.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grdPesquisa.Visible = true;
                }
                else
                {
                    divPesquisa.Visible = false;
                }
            }
            else
            {
                msgSemResultados.Visible = true;
            }
            divResultados.Visible = true;
        }

        public void ddlPeriodoInscritos_SelectedIndexChanged(Object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            //if (ddlTipoCursoAluno.SelectedValue != "")
            //{
            InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
            periodo_inscricao item = new periodo_inscricao();
            ddlCursoInscritos.Items.Clear();

            if (ddlPeriodoInscritos.SelectedValue != "")
            {
                item.id_periodo = Convert.ToInt32(ddlPeriodoInscritos.SelectedValue);

                List<cursos> listaCurso = new List<cursos>();


                if (usuario.id_grupo_acesso == 10) //Grupo Coordenador
                {
                    //É do grupo coordenador então pegar todos os cursos em que ele é coordenador
                    ProfessorAplicacao aplicacaoProfessor = new ProfessorAplicacao();
                    professores item_professor = new professores();

                    if (usuario.usuario.Substring(usuario.usuario.Length - 1, 1) == "p")
                    {
                        item_professor.id_professor = Convert.ToInt32(usuario.usuario.Substring(0, usuario.usuario.Length - 1));
                        item_professor = aplicacaoProfessor.BuscaItem(item_professor);
                    }
                    else
                    {
                        item_professor.cpf = usuario.usuario;
                        item_professor = aplicacaoProfessor.BuscaItem_byCPF(item_professor);
                    }

                    if (item_professor != null)
                    {
                        int[] qIdCurso = new int[1];
                        qIdCurso[0] = 0;

                        listaCurso = aplicacaoInscricao.ListaCursoPeriodo(item, qIdCurso);

                        qIdCurso = item_professor.cursos_coordenadores.Select(x => x.id_curso).ToArray();

                        listaCurso = listaCurso.Where(x => qIdCurso.Contains(x.id_curso)).ToList();
                    }

                }
                else
                {
                    int[] qIdCurso = new int[1];
                    qIdCurso[0] = 0;

                    listaCurso = aplicacaoInscricao.ListaCursoPeriodo(item, qIdCurso);
                }
                
                var lista = from elemento in listaCurso
                            select new
                            {
                                id_curso = elemento.id_curso,
                                nome = elemento.tipos_curso.tipo_curso + " - " + elemento.sigla + " - " + elemento.nome
                            };

                ddlCursoInscritos.Items.Clear();
                ddlCursoInscritos.DataSource = lista.OrderBy(x => x.nome);
                ddlCursoInscritos.DataValueField = "id_curso";
                ddlCursoInscritos.DataTextField = "nome";
                ddlCursoInscritos.DataBind();
                if (usuario.id_grupo_acesso != 10) //Grupo Coordenador
                {
                    ddlCursoInscritos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos os Cursos", ""));
                    ddlCursoInscritos.SelectedValue = "";
                }
            }

            ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", "javascript:fSelect2();", true);
        }

        protected void bntPerquisaInscritos_Click(object sender, EventArgs e)
        {
            string[] aFiltroRelacaoInscritos = new string[2];

            if (ddlPeriodoInscritos.SelectedValue != "")
            {
                aFiltroRelacaoInscritos[0] = ddlPeriodoInscritos.SelectedValue;
            }
            else
            {
                return;
            }

            if (ddlCursoInscritos.SelectedValue != "")
            {
                aFiltroRelacaoInscritos[1] = ddlCursoInscritos.SelectedValue;
            }

            Session["aFiltroRelacaoInscritos"] = aFiltroRelacaoInscritos;

            CarregarDados();
        }


        protected void btnEditaInscritos_Click(object sender, EventArgs e)
        {
            int qIdCurso = Convert.ToInt32(HttpContext.Current.Request["hCodigo"]);
            List<periodo_inscricao_curso> lista = (List<periodo_inscricao_curso>)Session["lista_periodo_inscricao_curso"];
            periodo_inscricao_curso item_periodo_inscricao_curso = lista.Where(x => x.id_curso == qIdCurso).FirstOrDefault();
            Session["periodo_inscricao_curso"] = item_periodo_inscricao_curso;
            Response.Redirect("proRelacaoInscritosGestao.aspx", true);

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

        public void grdPeriodoConfirmacaoMatricula_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                int linha = Convert.ToInt32(e.CommandArgument);
                int codigo = Convert.ToInt32(grdRelacaoInscrito.DataKeys[linha].Values[0]);
                pre_oferecimentos item = new pre_oferecimentos();
                MatriculaAplicacao aplicacaoMatricula = new MatriculaAplicacao();
                item.id_pre_oferecimento = codigo;
                item = aplicacaoMatricula.BuscaPreOferecimento(item);
                Session["pre_oferecimentos"] = item;
                Response.Redirect("matConfOferecimentoGestao.aspx", true);
            }
        }

        public string setAreaConcentracao(object qTabela)
        {
            
            string sAux="";
            HashSet<fichas_inscricao> lista = (HashSet<fichas_inscricao>)qTabela;

            var query = from c in lista
                        group c by c.id_area_concentracao into g
                        select new
                        {
                            id = g.Key,
                            Sum = g.Count(),
                        };

            AreaAplicacao aplicacaoArea = new AreaAplicacao();
            areas_concentracao item_area =  new areas_concentracao();

            sAux="";

            foreach (var elemento in query.ToList())
            {
                if (elemento.id != null)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<hr>";
                    }
                    item_area.id_area_concentracao = elemento.id.Value;
                    item_area = aplicacaoArea.BuscaItem(item_area);

                    sAux = sAux + item_area.nome;
                }
                
            }

            return sAux;
        }

        public string setAreaConcentracaoTotal(object qTabela)
        {

            string sAux = "";
            HashSet<fichas_inscricao> lista = (HashSet<fichas_inscricao>)qTabela;

            var query = from c in lista
                        group c by c.id_area_concentracao into g
                        select new
                        {
                            id = g.Key,
                            Sum = g.Count(),
                        };

            AreaAplicacao aplicacaoArea = new AreaAplicacao();
            areas_concentracao item_area = new areas_concentracao();

            sAux = "";

            foreach (var elemento in query.ToList())
            {
                if (elemento.id != null)
                {
                    if (sAux != "")
                    {
                        sAux = sAux + "<hr>";
                    }
                    item_area.id_area_concentracao = elemento.id.Value;
                    item_area = aplicacaoArea.BuscaItem(item_area);

                    sAux = sAux + elemento.Sum;
                }
            }

            return sAux;
        }

        public string setBotao(int qIdPeriodo, int qIdCurso)
        {
            string sAux = "";

            sAux = "<div title=\"Editar\"> <a class=\"btn btn-primary btn-circle fa fa-edit\" href=\'javascript:fEditarInscritos(\""
                        + qIdPeriodo.ToString() + "\",\""
                        + qIdCurso.ToString() + "\")\'; ></a></div>";

            return sAux;
        }

        public class GridAux
        {

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