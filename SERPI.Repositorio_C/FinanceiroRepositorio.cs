using System;
using SERPI.Dominio_C;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;

namespace Repositorio_C
{
    public class FinanceiroRepositorio : IDisposable
    {
        private Entities contextoEF;

        public FinanceiroRepositorio()
        {
            contextoEF = new Entities();
        }

        //Utilizado na tela de Relatório Pagto de Docentes (finCalculoCustoCurso) Início
        public List<datas_aulas> ListaCustoHoraAula_old(int qIdCurso, int qMes, int qAno)
        {
            var c = contextoEF.datas_aulas.AsQueryable();
            List<datas_aulas> lista = new List<datas_aulas>();

            c = c.Where(x => x.data_aula.Value.Month == qMes && x.data_aula.Value.Year == qAno && x.presenca_professor.Any(y=> y.presente == true) );

            if (qIdCurso != 0)
            {
                c = c.Where(x => x.oferecimentos.disciplinas.cursos_disciplinas.FirstOrDefault().cursos.id_curso == qIdCurso);
            }

            lista = c.ToList();

            return lista;
        }

        public List<geral_custo_hora_aula> ListaCustoHoraAula(string qIdCurso, DateTime qData, string qOrdenacao)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string strConnString;
            string sSql;

            List<geral_custo_hora_aula> lista_geral_custo_hora_aula = new List<geral_custo_hora_aula>();
            geral_custo_hora_aula item_geral_custo_hora_aula = new geral_custo_hora_aula();

            strConnString = ConfigurationManager.ConnectionStrings["SisProConnectionString"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "SELECT (select sum(hora_aula) ";
            sSql = sSql + "from datas_aulas_professor dap_x ";
            sSql = sSql + "inner join presenca_professor pp_x on pp_x.id_aula = dap_x.id_aula and pp_x.id_professor = dap_x.id_professor and pp_x.presente = 1 ";
            sSql = sSql + "where dap_x.id_aula in (select da.id_aula from datas_aulas da where id_oferecimento = dt.id_oferecimento) and dap_x.id_professor = dt.id_professor ";
            sSql = sSql + ") as TotalHorasProfessor, ";
            sSql = sSql + "o.carga_horaria, fo.nome nomeEmpresa,  dt.*, d.codigo, disciplina=d.nome, p.nome as professor, p.status statusProfessor, fr.id_forma_recebimento as recebe_como, p.id_titulo, t.nome as titulo_academico, ";
            sSql = sSql + "t.reduzido as titulo_reduzido, fr.nome as forma_recebimento, fr.valor_fixo, valor_hora = isnull(cvha.valor, 0.0), ";
            sSql = sSql + "total_horas = 0.0, pfr.horas_aula_adicional, valor_hora_adicional = isnull(cvhaa.valor, 0.0), ";
            sSql = sSql + "horas_extras = CASE WHEN isnull(cvhaa.valor, 0.0)> 0 AND dt.total_horas_mes_atual > dt.total_horas_mes_anterior AND dt.total_horas_mes_atual > pfr.horas_clt ";
            sSql = sSql + "THEN CASE WHEN dt.total_horas_mes_anterior > pfr.horas_clt ";
            sSql = sSql + "               THEN dt.total_horas_mes_atual - dt.total_horas_mes_anterior ";
            sSql = sSql + "               ELSE dt.total_horas_mes_atual - pfr.horas_clt ";
            sSql = sSql + "               END ";
            sSql = sSql + "          ELSE 0.0 ";
            sSql = sSql + "          END ";
            sSql = sSql + "FROM( ";
            sSql = sSql + "SELECT cur.nome, dap.id_professor, count(*) as datas, da.id_oferecimento, cd.id_curso, o.quadrimestre, SUM(hora_aula) as total_horas, ";
            sSql = sSql + "  total_horas_mes_anterior = (SELECT ISNULL(SUM(hora_aula), 0.0) ";
            sSql = sSql + "                     FROM datas_aulas_professor dapi ";
            sSql = sSql + "                     INNER JOIN datas_aulas dai ON dapi.id_aula = dai.id_aula ";
            sSql = sSql + "                     INNER JOIN oferecimentos oi ON dai.id_oferecimento = oi.id_oferecimento ";
            sSql = sSql + "                     INNER JOIN presenca_professor pp ON dapi.id_aula = pp.id_aula AND dapi.id_professor = pp.id_professor ";
            sSql = sSql + "                     WHERE oi.quadrimestre = o.quadrimestre AND dapi.id_professor = dap.id_professor AND pp.presente = 1 AND ";
            //sSql = sSql + "                           MONTH(dai.data_aula) <= (CASE WHEN YEAR(dai.data_aula) < YEAR(DATEADD(Month, -1, cast('" + qData + "' as datetime))) THEN 12 ELSE MONTH(DATEADD(Month, -1, cast('" + qData + "' as datetime))) END) ), ";
            sSql = sSql + "                           MONTH(dai.data_aula) <= (CASE WHEN YEAR(dai.data_aula) < " + (qData.AddMonths(-1).Year.ToString()) + " THEN 12 ELSE " + (qData.AddMonths(-1).Month.ToString()) + " END) ), ";
            sSql = sSql + "  total_horas_mes_atual = (SELECT ISNULL(SUM(hora_aula), 0.0) ";
            sSql = sSql + "                  FROM datas_aulas_professor dapi ";
            sSql = sSql + "                  INNER JOIN datas_aulas dai ON dapi.id_aula = dai.id_aula ";
            sSql = sSql + "                  INNER JOIN oferecimentos oi ON dai.id_oferecimento = oi.id_oferecimento ";
            sSql = sSql + "                  INNER JOIN presenca_professor pp ON dapi.id_aula = pp.id_aula AND dapi.id_professor = pp.id_professor ";
            sSql = sSql + "                  WHERE oi.quadrimestre = o.quadrimestre AND dapi.id_professor = dap.id_professor AND pp.presente = 1 AND ";
            //sSql = sSql + "                           MONTH(dai.data_aula) <= (CASE WHEN YEAR(dai.data_aula) < YEAR(cast('" + qData + "' as datetime)) THEN 12 ELSE MONTH(cast('" + qData + "' as datetime)) END) ) ";
            sSql = sSql + "                           MONTH(dai.data_aula) <= (CASE WHEN YEAR(dai.data_aula) < " + qData.Year.ToString() + " THEN 12 ELSE " + qData.Month.ToString() + " END) ) ";
            sSql = sSql + "FROM datas_aulas_professor dap ";
            sSql = sSql + "INNER JOIN datas_aulas da ON dap.id_aula = da.id_aula ";
            sSql = sSql + "INNER JOIN oferecimentos o ON da.id_oferecimento = o.id_oferecimento ";
            sSql = sSql + "INNER JOIN cursos_disciplinas cd ON o.id_disciplina = cd.id_disciplina ";
            sSql = sSql + "INNER JOIN cursos cur ON cur.id_curso = cd.id_curso ";
            sSql = sSql + "INNER JOIN presenca_professor pp ON dap.id_aula = pp.id_aula AND dap.id_professor = pp.id_professor ";
            sSql = sSql + "WHERE ";
            if (qIdCurso != "")
            {
                sSql = sSql + "cd.id_curso in (" + qIdCurso + ") AND ";
            }
            sSql = sSql + "YEAR(da.data_aula) = " + qData.Year.ToString() + " AND MONTH(da.data_aula) = " + qData.Month.ToString() + " AND pp.presente = 1 ";
            sSql = sSql + "  GROUP BY cur.nome, o.quadrimestre, cd.id_curso, dap.id_professor, da.id_oferecimento) as dt ";
            // sSql = sSql + " and p.id_professor = 2212 "
            sSql = sSql + "INNER JOIN professores as p ON dt.id_professor = p.id_professor ";
            sSql = sSql + "INNER JOIN oferecimentos o ON o.status <> 'inativado' and  dt.id_oferecimento = o.id_oferecimento ";
            sSql = sSql + "INNER JOIN disciplinas d ON o.id_disciplina = d.id_disciplina ";
            sSql = sSql + "LEFT JOIN professores_forma_recebimento pfr ON p.id_professor = pfr.id_professor ";
            sSql = sSql + "LEFT JOIN forma_recebimento as fr ON pfr.horas_aula = fr.id_forma_recebimento ";
            sSql = sSql + "LEFT JOIN fornecedores as fo ON fo.id_fornecedor = pfr.id_fornecedor ";
            sSql = sSql + "LEFT JOIN titulacao as t ON p.id_titulo = t.id_titulacao ";
            sSql = sSql + "LEFT JOIN curso_valor_hora_aula cvha ON dt.id_curso = cvha.id_curso AND pfr.horas_aula = cvha.id_forma_recebimento AND p.id_titulo = cvha.id_titulacao ";
            sSql = sSql + "LEFT JOIN curso_valor_hora_aula cvhaa ON dt.id_curso = cvhaa.id_curso AND pfr.horas_aula_adicional = cvhaa.id_forma_recebimento AND p.id_titulo = cvhaa.id_titulacao  ";
            sSql = sSql + "ORDER BY ";
            if (qOrdenacao != "professor")
            {
                sSql = sSql + " nomeEmpresa, ";
            }
            //sSql = sSql + " professor, id_oferecimento, quadrimestre ";
            sSql = sSql + " professor, quadrimestre ";

            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            SqlDataReader retorno = cmd.ExecuteReader();

            decimal dTotalHoraAula = 0;

            if (retorno.HasRows)
            {
                while (retorno.Read())
                {
                    if (!(item_geral_custo_hora_aula.id_professor == Convert.ToInt32(retorno["id_professor"]) && item_geral_custo_hora_aula.id_curso == Convert.ToInt32(retorno["id_curso"])))
                    //if ((item_geral_custo_hora_aula.id_professor != Convert.ToInt32(retorno["id_professor"]) && item_geral_custo_hora_aula.id_oferecimento != Convert.ToInt32(retorno["id_oferecimento"])))
                        {
                        if (item_geral_custo_hora_aula.id_professor != 0)
                        {
                            dTotalHoraAula = dTotalHoraAula + item_geral_custo_hora_aula.col_Total;
                            lista_geral_custo_hora_aula.Add(item_geral_custo_hora_aula);
                        }
                        item_geral_custo_hora_aula = new geral_custo_hora_aula();
                        item_geral_custo_hora_aula.NomeEmpresa = retorno["nomeEmpresa"].ToString();
                        item_geral_custo_hora_aula.NomeCurso = retorno["nome"].ToString();
                        //item_geral_custo_hora_aula.BotaoDetalhe = "<div title=\"Detalhe\"> <a class=\"btn btn-primary  btn-circle fa fa-search\" href=\'javascript:fPreencheDetalheHoraAula(\"" + retorno["professor"].ToString() + "\",\"" + retorno["nome"].ToString() + "\",\"" + dtfi.GetMonthName(Convert.ToDateTime(qData).Month) + "\",\"" + Convert.ToDateTime(qData).Year + "\",\"" + retorno["id_curso"].ToString() + "\",\"" + qData + "\",\"" + retorno["id_professor"].ToString() + "\")\'; ></a></div>";
                        item_geral_custo_hora_aula.BotaoDetalhe = "<div title=\"Detalhe\"> <a class=\"btn btn-primary  btn-circle fa fa-search\" href=\'javascript:fPreencheDetalheHoraAula(\"" + retorno["id_curso"].ToString() + "\",\"" + retorno["id_professor"].ToString() + "\",\"" + qData.ToString("dd/MM/yyyy") + "\",\"" + dtfi.GetMonthName(qData.Month) + "/" + qData.Year + "\")\'; ></a></div>";
                        item_geral_custo_hora_aula.id_professor = Convert.ToInt32(retorno["id_professor"]);
                        item_geral_custo_hora_aula.datas = Convert.ToInt32(retorno["datas"]);
                        item_geral_custo_hora_aula.id_oferecimento = Convert.ToInt32(retorno["id_oferecimento"]);
                        item_geral_custo_hora_aula.id_curso = Convert.ToInt32(retorno["id_curso"]);
                        item_geral_custo_hora_aula.quadrimestre = retorno["quadrimestre"].ToString();
                        item_geral_custo_hora_aula.total_horas = Convert.ToDecimal(retorno["total_horas"]);
                        item_geral_custo_hora_aula.total_horas_mes_anterior = Convert.ToDecimal(retorno["total_horas_mes_anterior"]);
                        item_geral_custo_hora_aula.total_horas_mes_atual = Convert.ToDecimal(retorno["total_horas_mes_atual"]);
                        item_geral_custo_hora_aula.codigo_oferecimento = retorno["codigo"].ToString();
                        item_geral_custo_hora_aula.disciplina = retorno["disciplina"].ToString();
                        item_geral_custo_hora_aula.professor = retorno["professor"].ToString();
                        if (retorno["statusProfessor"].ToString() == "inativado")
                        {
                            item_geral_custo_hora_aula.professor = item_geral_custo_hora_aula.professor + " (Inativado)";
                        }
                        item_geral_custo_hora_aula.recebe_como = Convert.ToInt32(retorno["recebe_como"]);
                        item_geral_custo_hora_aula.titulo_academico = retorno["titulo_academico"].ToString();
                        item_geral_custo_hora_aula.titulo_reduzido = retorno["titulo_reduzido"].ToString();
                        item_geral_custo_hora_aula.forma_recebimento = retorno["forma_recebimento"].ToString();
                        item_geral_custo_hora_aula.valor_fixo = Convert.ToInt32(retorno["valor_fixo"]);
                        item_geral_custo_hora_aula.valor_hora = Convert.ToDecimal(retorno["valor_hora"]);
                        item_geral_custo_hora_aula.horas_aula_adicional = Convert.ToInt32(retorno["horas_aula_adicional"]);
                        item_geral_custo_hora_aula.valor_hora_adicional = Convert.ToDecimal(retorno["valor_hora_adicional"]);
                        item_geral_custo_hora_aula.horas_extras = Convert.ToDecimal(retorno["horas_extras"]);

                        //Alterado em 15/10/2020 para incluir a palavra  (Inativo)
                        //item_geral_custo_hora_aula.col_Professor = retorno["titulo_reduzido"].ToString() + " <strong>" + retorno["professor"].ToString() + "</strong><br>Forma Recebimento: " + retorno["forma_recebimento"].ToString();
                        item_geral_custo_hora_aula.col_Professor = retorno["titulo_reduzido"].ToString() + " <strong>" + item_geral_custo_hora_aula.professor + "</strong><br>Forma Recebimento: " + retorno["forma_recebimento"].ToString();
                        if (item_geral_custo_hora_aula.recebe_como == 5 || item_geral_custo_hora_aula.recebe_como == 12 || item_geral_custo_hora_aula.recebe_como == 13 || item_geral_custo_hora_aula.recebe_como == 14)
                        {
                            item_geral_custo_hora_aula.col_Professor = item_geral_custo_hora_aula.col_Professor + "<br>" + retorno["nomeEmpresa"].ToString();

                        }
                        if (item_geral_custo_hora_aula.recebe_como == 2) //CTL 
                        {
                            //if (item_geral_custo_hora_aula.horas_aula_adicional != 1) // Recebe hora adicional (1= não recebe)
                            //{
                            //    item_geral_custo_hora_aula.col_TotalHoras = item_geral_custo_hora_aula.total_horas;
                            //    item_geral_custo_hora_aula.col_Total = item_geral_custo_hora_aula.col_TotalHoras * item_geral_custo_hora_aula.valor_hora;
                            //}
                            //else
                            //{
                            item_geral_custo_hora_aula.valor_hora = 0;
                            item_geral_custo_hora_aula.col_TotalHoras = 0;
                            item_geral_custo_hora_aula.col_Total = 0;
                            //}
                        }
                        else
                        {
                            item_geral_custo_hora_aula.col_TotalHoras = item_geral_custo_hora_aula.total_horas;
                            item_geral_custo_hora_aula.col_Total = item_geral_custo_hora_aula.col_TotalHoras * item_geral_custo_hora_aula.valor_hora;
                        }

                    }
                    else
                    {
                        item_geral_custo_hora_aula.datas = item_geral_custo_hora_aula.datas + Convert.ToInt32(retorno["datas"]);
                        item_geral_custo_hora_aula.id_curso = Convert.ToInt32(retorno["id_curso"]);
                        item_geral_custo_hora_aula.total_horas = item_geral_custo_hora_aula.total_horas + Convert.ToDecimal(retorno["total_horas"]);
                        item_geral_custo_hora_aula.horas_extras = item_geral_custo_hora_aula.horas_extras + Convert.ToDecimal(retorno["horas_extras"]);
                        item_geral_custo_hora_aula.col_TotalHoras = item_geral_custo_hora_aula.total_horas;
                        item_geral_custo_hora_aula.col_Total = item_geral_custo_hora_aula.col_TotalHoras * item_geral_custo_hora_aula.valor_hora;
                        item_geral_custo_hora_aula.id_oferecimento = Convert.ToInt32(retorno["id_oferecimento"]);

                        //if (item_geral_custo_hora_aula.id_oferecimento == Convert.ToInt32(retorno["id_oferecimento"]))
                        //{
                        //    if (item_geral_custo_hora_aula.NomeCurso.IndexOf(retorno["nome"].ToString()) == -1 )
                        //    {
                        //        item_geral_custo_hora_aula.NomeCurso = item_geral_custo_hora_aula.NomeCurso + "<br>" + retorno["nome"].ToString();
                        //    }
                        //}
                        //else
                        //{
                        //    item_geral_custo_hora_aula.datas = item_geral_custo_hora_aula.datas + Convert.ToInt32(retorno["datas"]);
                        //    item_geral_custo_hora_aula.id_curso = Convert.ToInt32(retorno["id_curso"]);
                        //    item_geral_custo_hora_aula.total_horas = item_geral_custo_hora_aula.total_horas + Convert.ToDecimal(retorno["total_horas"]);
                        //    item_geral_custo_hora_aula.horas_extras = item_geral_custo_hora_aula.horas_extras + Convert.ToDecimal(retorno["horas_extras"]);
                        //    item_geral_custo_hora_aula.col_TotalHoras = item_geral_custo_hora_aula.total_horas;
                        //    item_geral_custo_hora_aula.col_Total = item_geral_custo_hora_aula.col_TotalHoras * item_geral_custo_hora_aula.valor_hora;
                        //    item_geral_custo_hora_aula.id_oferecimento = Convert.ToInt32(retorno["id_oferecimento"]);
                        //}

                    }

                }

                dTotalHoraAula = dTotalHoraAula + item_geral_custo_hora_aula.col_Total;
                lista_geral_custo_hora_aula.Add(item_geral_custo_hora_aula);

                item_geral_custo_hora_aula = new geral_custo_hora_aula();
                item_geral_custo_hora_aula.col_Professor = "<strong>TOTAL:</strong>";
                item_geral_custo_hora_aula.col_Total = dTotalHoraAula;

                lista_geral_custo_hora_aula.Add(item_geral_custo_hora_aula);
            }
            
            return lista_geral_custo_hora_aula;
        }

        public List<geral_custo_banca_orientacao> ListaCustoBanca(string qIdCurso, DateTime qData)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string strConnString;
            string sSql;

            List<geral_custo_banca_orientacao> lista_geral_custo_banca = new List<geral_custo_banca_orientacao>();
            geral_custo_banca_orientacao item_geral_custo_banca = new geral_custo_banca_orientacao();

            strConnString = ConfigurationManager.ConnectionStrings["SisProConnectionString"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "SELECT bp.id_professor, b.tipo_banca, tr.id_curso, b.horario, p.nome as professor, a.nome as aluno, ";       
            sSql = sSql + "fr.id_forma_recebimento as recebe_como, p.id_titulo, t.nome as titulo_academico, t.reduzido as titulo_reduzido,    "; 
            sSql = sSql + "fr.nome as forma_recebimento, valor_hora = (CASE isNull(fr.id_forma_recebimento, 1) WHEN 8 THEN cvb.valor_sao_paulo WHEN 9 THEN cvb.valor_fora_sao_paulo ELSE 0.0 END)  ";
            sSql = sSql + "FROM banca_professores bp ";
            sSql = sSql + "INNER JOIN banca b ON bp.id_banca = b.id_banca ";
            sSql = sSql + "INNER JOIN matricula_turma mt ON b.id_matricula_turma = mt.id_matricula_turma ";
            sSql = sSql + "INNER JOIN turmas tr ON mt.id_turma = tr.id_turma ";
            sSql = sSql + "INNER JOIN alunos a ON mt.id_aluno = a.idaluno ";
            sSql = sSql + "INNER JOIN professores p ON bp.id_professor = p.id_professor ";
            sSql = sSql + "LEFT JOIN professores_forma_recebimento pfr ON p.id_professor = pfr.id_professor ";
            sSql = sSql + "LEFT JOIN forma_recebimento as fr ON pfr.banca = fr.id_forma_recebimento ";
            sSql = sSql + "LEFT JOIN titulacao as t ON p.id_titulo = t.id_titulacao ";
            sSql = sSql + "LEFT JOIN curso_valor_banca cvb ON tr.id_curso = cvb.id_curso ";
            sSql = sSql + "WHERE bp.tipo_professor = 'membro' AND isNull(pfr.banca, 1) > 1 AND ";
            if (qIdCurso != "")
            {
                sSql = sSql + "tr.id_curso in (" + qIdCurso + ") AND ";
            }

            sSql = sSql + "YEAR(b.horario) = " + qData.Year.ToString() + " AND MONTH(b.horario) = " + qData.Month.ToString() + " ";
            sSql = sSql + "ORDER BY professor ";

            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            SqlDataReader retorno = cmd.ExecuteReader();

            decimal dTotalHoraAula = 0;

            if (retorno.HasRows)
            {
                while (retorno.Read())
                {
                    
                    item_geral_custo_banca = new geral_custo_banca_orientacao();
                    item_geral_custo_banca.col_Id_Professor = Convert.ToInt32(retorno["id_professor"]);
                    item_geral_custo_banca.col_Professor = retorno["titulo_reduzido"].ToString() + " <strong>" + retorno["professor"].ToString() + "</strong>";
                    item_geral_custo_banca.col_FormaRecebimento = retorno["forma_recebimento"].ToString();
                    item_geral_custo_banca.col_TipoBanca = retorno["tipo_banca"].ToString();
                    item_geral_custo_banca.col_DataHora = String.Format("{0:dd/MM/yyyy}", retorno["horario"]) + " " + String.Format("{0:HH:mm}", retorno["horario"]);
                    item_geral_custo_banca.col_Aluno = retorno["aluno"].ToString();
                    item_geral_custo_banca.col_Total = Convert.ToDecimal(retorno["valor_hora"]);

                    dTotalHoraAula = dTotalHoraAula + item_geral_custo_banca.col_Total;
                    lista_geral_custo_banca.Add(item_geral_custo_banca);
                }

                item_geral_custo_banca = new geral_custo_banca_orientacao();
                item_geral_custo_banca.col_Professor = "<strong>TOTAL:</strong>";
                item_geral_custo_banca.col_Total = dTotalHoraAula;

                lista_geral_custo_banca.Add(item_geral_custo_banca);
            }

            return lista_geral_custo_banca;
        }

        public List<geral_custo_banca_orientacao> ListaCustoOrientacao(string qIdCurso, DateTime qData)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string strConnString;
            string sSql;

            List<geral_custo_banca_orientacao> lista_geral_custo_orientacao = new List<geral_custo_banca_orientacao>();
            geral_custo_banca_orientacao item_geral_custo_banca = new geral_custo_banca_orientacao();

            strConnString = ConfigurationManager.ConnectionStrings["SisProConnectionString"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "";
            sSql = sSql + "SELECT id_professor, tipo_banca, tipo_professor, id_curso, horario, professor, aluno, recebe_como, id_titulo, titulo_academico, titulo_reduzido, forma_recebimento, resultado, valor_hora,  ";
            sSql = sSql + "       data_entrega_trabalho, data_calculo, id_banca, derivedTbl.fornecedor, nomecurso ";
            sSql = sSql + "FROM  (SELECT bp.tipo_professor,fo.nome fornecedor, bp.id_professor, b.tipo_banca, tr.id_curso, b.horario, p.nome AS professor, a.nome AS aluno, fr.id_forma_recebimento AS recebe_como, p.id_titulo,  ";
            sSql = sSql + "              t.nome AS titulo_academico, t.reduzido AS titulo_reduzido, fr.nome AS forma_recebimento, b.resultado,  ";
            sSql = sSql + "              (CASE WHEN b.tipo_banca = 'defesa' THEN cvo.valor_defesa ELSE cvo.valor_qualificacao END) AS valor_hora,  ";
            sSql = sSql + "			  b.data_entrega_trabalho, '" + qData.ToString("dd/MM/yyyy") + "' AS data_calculo, b.id_banca, cs.nome as nomecurso  ";
            sSql = sSql + "       FROM banca_professores AS bp  ";
            sSql = sSql + "	   INNER JOIN banca AS b ON bp.id_banca = b.id_banca  ";
            sSql = sSql + "	   INNER JOIN matricula_turma AS mt ON b.id_matricula_turma = mt.id_matricula_turma  ";
            sSql = sSql + "	   INNER JOIN turmas AS tr ON mt.id_turma = tr.id_turma  ";
            sSql = sSql + "	   INNER JOIN cursos AS cs ON cs.id_curso = tr.id_curso  ";
            sSql = sSql + "	   INNER JOIN alunos AS a ON mt.id_aluno = a.idaluno  ";
            sSql = sSql + "	   INNER JOIN professores AS p ON bp.id_professor = p.id_professor  ";
            sSql = sSql + "	   LEFT OUTER JOIN professores_forma_recebimento AS pfr ON p.id_professor = pfr.id_professor  ";
            sSql = sSql + "	   LEFT OUTER JOIN fornecedores AS fo ON fo.id_fornecedor = pfr.id_fornecedor ";
            sSql = sSql + "	   LEFT OUTER JOIN forma_recebimento AS fr ON pfr.orientacao = fr.id_forma_recebimento  ";
            sSql = sSql + "	   LEFT OUTER JOIN titulacao AS t ON p.id_titulo = t.id_titulacao  ";
            sSql = sSql + "	   LEFT OUTER JOIN curso_valor_orientacao AS cvo ON cvo.id_curso = tr.id_curso AND fr.id_forma_recebimento = cvo.id_forma_recebimento ";
            sSql = sSql + "       WHERE (bp.tipo_professor='orientador')  ";
            //== Tirado o Co-orientador segundo email do prof Eduardo em 08/04/2021 as 10:42
            //sSql = sSql + "       WHERE (bp.tipo_professor='orientador' OR bp.tipo_professor='co-orientador')  "; 
            sSql = sSql + "	   AND (ISNULL(pfr.orientacao, 1) > 1)  ";
            if (qIdCurso != "")
            {
                sSql = sSql + "	   AND (tr.id_curso in (" + qIdCurso + "))";
            }
            //sSql = sSql + "	   AND (tr.id_curso = 1))  "; 
            sSql = sSql + ") AS derivedTbl ";
            sSql = sSql + "WHERE ((CASE tipo_banca WHEN 'Defesa'  ";
            sSql = sSql + "		THEN CASE WHEN (YEAR(data_entrega_trabalho) = " + qData.Year.ToString() + " AND MONTH(data_entrega_trabalho) = " + qData.Month.ToString() + " ";
            sSql = sSql + "			) THEN 1 ELSE 0 END  ";
            sSql = sSql + "		ELSE CASE WHEN (YEAR(horario) = " + qData.Year.ToString() + " AND MONTH(horario) = " + qData.Month.ToString() + "  ";
            sSql = sSql + "  AND (resultado = 'Aprovado') ";
            sSql = sSql + "         )    THEN 1 ELSE 0 END  ";
            sSql = sSql + "		END) = 1) ";
            sSql = sSql + "ORDER BY professor ";

            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            SqlDataReader retorno = cmd.ExecuteReader();

            decimal dTotalHoraAula = 0;

            if (retorno.HasRows)
            {
                while (retorno.Read())
                {

                    item_geral_custo_banca = new geral_custo_banca_orientacao();
                    item_geral_custo_banca.col_Id_Professor = Convert.ToInt32(retorno["id_professor"]);
                    //item_geral_custo_banca.col_Professor = retorno["titulo_reduzido"].ToString() + " <strong>" + retorno["professor"].ToString() + "</strong>";
                    item_geral_custo_banca.col_Professor = retorno["titulo_reduzido"].ToString() + " <strong>" + retorno["professor"].ToString() + "</strong><br>Forma Recebimento: " + retorno["forma_recebimento"].ToString() + "<br>" + retorno["fornecedor"].ToString();
                    item_geral_custo_banca.col_FormaRecebimento = retorno["forma_recebimento"].ToString();
                    item_geral_custo_banca.col_Empresa = retorno["fornecedor"].ToString();
                    item_geral_custo_banca.col_Curso = retorno["nomecurso"].ToString();
                    item_geral_custo_banca.col_TipoBanca = retorno["tipo_banca"].ToString();
                    item_geral_custo_banca.col_DataHora = String.Format("{0:dd/MM/yyyy}", retorno["horario"]) + " " + String.Format("{0:HH:mm}", retorno["horario"]);
                    item_geral_custo_banca.col_Aluno = retorno["aluno"].ToString();
                    item_geral_custo_banca.col_Total = Convert.ToDecimal(retorno["valor_hora"]);

                    dTotalHoraAula = dTotalHoraAula + item_geral_custo_banca.col_Total;
                    lista_geral_custo_orientacao.Add(item_geral_custo_banca);
                }

                item_geral_custo_banca = new geral_custo_banca_orientacao();
                item_geral_custo_banca.col_Professor = "<strong>TOTAL:</strong>";
                item_geral_custo_banca.col_Total = dTotalHoraAula;

                lista_geral_custo_orientacao.Add(item_geral_custo_banca);
            }

            return lista_geral_custo_orientacao;
        }

        public List<geral_custo_coordenacao> ListaCustoCoordenacao(string qIdCurso, decimal qIdProfessor, DateTime qData)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            List<geral_custo_coordenacao> lista_geral_coordenacao = new List<geral_custo_coordenacao>();
            geral_custo_coordenacao item_geral_custo_coordenacao = new geral_custo_coordenacao();

            //Encontrar na tabela "valor_custo_coordenacao" se há valoes > 0

            List<curso_valor_coordenacao> lista;
            decimal dTotalCoordenacao = 0;

            var array_qIdCurso = qIdCurso.Split(',');

            foreach (var elemento_IdCurso in array_qIdCurso)
            {



                if (qIdCurso != "")
                {
                    int idCurso = Convert.ToInt32(elemento_IdCurso);
                    lista = contextoEF.curso_valor_coordenacao.Where(x => x.id_curso == idCurso && x.valor > 0).ToList();
                }
                else
                {
                    lista = contextoEF.curso_valor_coordenacao.Where(x => x.valor > 0).ToList();
                }

                DateTime qDataInicio;
                DateTime qDataFim;
                //dTotalCoordenacao = 0;

                //qData.Day = DateTime.DaysInMonth(qData.Year, qData.Month);
                qDataInicio = Convert.ToDateTime(DateTime.DaysInMonth(qData.Year, qData.Month) + "/" + qData.Month + "/" + qData.Year);
                qDataFim = Convert.ToDateTime("01/" + qData.Month + "/" + qData.Year);


                foreach (var elemento in lista)
                {
                    //Verificar se tem turma(s) aberta para esse curso
                    if (elemento.cursos.turmas.Any(x => x.data_inicio.Value <= qDataInicio && x.data_fim.Value >= qDataFim))
                    {
                        //estou com um elemento (coordenador de Curso ou de Área)
                        List<turmas> lista_turma;
                        lista_turma = elemento.cursos.turmas.Where(x => x.data_inicio.Value <= qDataInicio && x.data_fim.Value >= qDataFim).ToList();
                        string sTurma = "";
                        foreach (var elemento2 in lista_turma)
                        {
                            if (sTurma != "")
                            {
                                sTurma = sTurma + " - ";
                            }
                            sTurma = sTurma + elemento2.cod_turma;
                        }

                        List<cursos_coordenadores> lista_coordenadores;
                        if (qIdProfessor != 0)
                        {
                            lista_coordenadores = contextoEF.cursos_coordenadores.Where(x => x.id_tipo_coordenador == elemento.id_tipo_coordenador && x.id_curso == elemento.id_curso && x.id_professor == qIdProfessor).ToList();
                        }
                        else
                        {
                            lista_coordenadores = contextoEF.cursos_coordenadores.Where(x => x.id_tipo_coordenador == elemento.id_tipo_coordenador && x.id_curso == elemento.id_curso).ToList();
                        }

                        foreach (var elemento3 in lista_coordenadores)
                        {
                            item_geral_custo_coordenacao = new geral_custo_coordenacao();
                            item_geral_custo_coordenacao.col_Id_Professor = Convert.ToInt32(elemento3.id_professor);
                            item_geral_custo_coordenacao.col_Professor = elemento3.professores.titulacao.reduzido + " <strong>" + elemento3.professores.nome + "</strong>";
                            item_geral_custo_coordenacao.col_Id_Curso = elemento3.cursos.id_curso;
                            item_geral_custo_coordenacao.col_Id_TipoCurso = elemento3.cursos.id_tipo_curso;
                            item_geral_custo_coordenacao.col_Curso = elemento3.cursos.nome;
                            item_geral_custo_coordenacao.col_TipoCoordenacao = elemento.curso_tipo_coordenador.descricao;
                            item_geral_custo_coordenacao.col_Turma = sTurma;
                            item_geral_custo_coordenacao.col_MesReferencia = qDataInicio.Month.ToString("00") + "/" + qDataInicio.Year.ToString();
                            item_geral_custo_coordenacao.col_Total = elemento.valor.Value;
                            dTotalCoordenacao = dTotalCoordenacao + item_geral_custo_coordenacao.col_Total;
                            lista_geral_coordenacao.Add(item_geral_custo_coordenacao);
                        }
                    }
                }

            }
            if (lista_geral_coordenacao.Count > 0 )
            {
                item_geral_custo_coordenacao = new geral_custo_coordenacao();
                item_geral_custo_coordenacao.col_Professor = "<strong>TOTAL:</strong>";
                item_geral_custo_coordenacao.col_Total = dTotalCoordenacao;

                lista_geral_coordenacao.Add(item_geral_custo_coordenacao);
            }

            return lista_geral_coordenacao;

        }

        public List<geral_detalhe_hora_aula> ListaDetalheHoraAula(int qIdCurso, int qIdProfessor, string qAno, string qMes)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string strConnString;
            string sSql;

            List<geral_detalhe_hora_aula> lista_detalhe_hora_aula = new List<geral_detalhe_hora_aula>();
            geral_detalhe_hora_aula item_detalhe_hora_aula = new geral_detalhe_hora_aula();

            strConnString = ConfigurationManager.ConnectionStrings["SisProConnectionString"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "";
            sSql = sSql + "SELECT da.data_aula, isnull(sum(dap.hora_aula), 0.0) as hora_aula, d.codigo, o.quadrimestre, o.id_oferecimento, c.nome curso, p.nome professor ";
            sSql = sSql + "FROM datas_aulas_professor dap "; 
            sSql = sSql + "INNER JOIN datas_aulas da ON dap.id_aula = da.id_aula ";
            sSql = sSql + "INNER JOIN oferecimentos o ON da.id_oferecimento = o.id_oferecimento and o.status <> 'inativado' ";
            sSql = sSql + "INNER JOIN disciplinas d ON d.id_disciplina=o.id_disciplina  ";
            sSql = sSql + "INNER JOIN cursos_disciplinas cd ON o.id_disciplina = cd.id_disciplina ";
            sSql = sSql + "INNER JOIN cursos c ON c.id_curso = cd.id_curso ";
            sSql = sSql + "INNER JOIN professores p ON p.id_professor = dap.id_professor ";
            sSql = sSql + "INNER JOIN presenca_professor pp ON dap.id_aula = pp.id_aula AND dap.id_professor = pp.id_professor ";
            sSql = sSql + "WHERE(cd.id_curso = '" + qIdCurso + "') AND(YEAR(da.data_aula) = '" + qAno + "') AND(MONTH(da.data_aula) = '" + qMes + "') AND(pp.presente = '1') ";
            //sSql = sSql + "WHERE (YEAR(da.data_aula) = '" + qAno + "') AND(MONTH(da.data_aula) = '" + qMes + "') AND(pp.presente = '1') ";
            sSql = sSql + "AND dap.id_professor = " + qIdProfessor + " ";
            //sSql = sSql + "AND o.id_oferecimento = " + qIdOferecimento + " ";
            sSql = sSql + "AND cd.id_curso = " + qIdCurso + " ";
            sSql = sSql + "GROUP BY da.data_aula, d.codigo, o.quadrimestre, o.id_oferecimento, c.nome, p.nome ";
            sSql = sSql + "ORDER BY d.codigo, da.data_aula ";

            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            SqlDataReader retorno = cmd.ExecuteReader();

            if (retorno.HasRows)
            {
                while (retorno.Read())
                {
                    item_detalhe_hora_aula = new geral_detalhe_hora_aula();
                    item_detalhe_hora_aula.data_aula = String.Format("{0:dd/MM/yyyy}", retorno["data_aula"]);
                    item_detalhe_hora_aula.hora_aula = Convert.ToDecimal(retorno["hora_aula"]);
                    item_detalhe_hora_aula.codigo_disciplina = retorno["codigo"].ToString();
                    item_detalhe_hora_aula.periodo = retorno["quadrimestre"].ToString();
                    item_detalhe_hora_aula.id_oferecimento = Convert.ToInt32(retorno["id_oferecimento"]);
                    item_detalhe_hora_aula.curso = retorno["curso"].ToString();
                    item_detalhe_hora_aula.professor = retorno["professor"].ToString();
                    lista_detalhe_hora_aula.Add(item_detalhe_hora_aula);
                }
            }

            return lista_detalhe_hora_aula;
        }

        //Utilizado na tela de Relatório Pagto de Docentes (finCalculoCustoCurso) Fim
        
        //Utilizado na tela de Extrato Professor (finCalculoCustoCurso) Início
        public List<geral_extrato_professor> ListaProfessores(professores pItem)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string strConnString;
            string sSql;
            string sWhere = "";

            List<geral_extrato_professor> lista_extrato_professor = new List<geral_extrato_professor>();
            geral_extrato_professor item_extrato_professor;

            strConnString = ConfigurationManager.ConnectionStrings["SisProConnectionString"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "";
            sSql = sSql + "SELECT        p.id_professor, t.nome AS titulo, t.reduzido, p.nome AS professor, p.email, ";
            sSql = sSql + "saldo_a_solicitar=ISNULL((SELECT SUM(valor-valor_solicitado) ";
            sSql = sSql + "                         FROM(SELECT pp.*, valor_solicitado = ISNULL(( ";
            sSql = sSql + "                              SELECT SUM(psp.valor_solicitado) FROM professor_solicitacao_plano psp WHERE pp.id_plano = psp.id_plano), 0) ";
            sSql = sSql + "                              FROM professor_plano pp WHERE pp.id_professor = p.id_professor and pp.motivo <> 'Banca') derivedTbl), 0), ";
            sSql = sSql + "ISNULL((SELECT        SUM(valor) AS Expr1 ";
            sSql = sSql + "         FROM            professor_plano AS pp ";
            sSql = sSql + "         WHERE(id_professor = p.id_professor and pp.motivo <> 'Banca')), 0) AS plano, ";
            sSql = sSql + "ISNULL ((select sum(pSolPagto.valor) ";
            sSql = sSql + " from professor_solicitacao_pagamento pSolPagto ";
            sSql = sSql + " where pSolPagto.id_solicitacao in (select pSolPlano.id_solicitacao ";
            sSql = sSql + "                                    from professor_plano pplano ";
            sSql = sSql + "                                    inner join professores p1 on p1.id_professor = pplano.id_professor ";
            sSql = sSql + "                                    inner join professor_solicitacao_plano pSolPlano on pSolPlano.id_plano = pplano.id_plano ";
            sSql = sSql + "                                    where p1.id_professor = p.id_professor ";
            sSql = sSql + "                                    group by pSolPlano.id_solicitacao) ";
            sSql = sSql + "        and pSolPagto.status = 'Pago'), 0) AS pagamento, ";
            sSql = sSql + "ISNULL ((SELECT        SUM(psp.valor_solicitado) AS Expr1 ";
            sSql = sSql + "         FROM            professor_solicitacao_plano AS psp INNER JOIN ";
            sSql = sSql + "                                professor_plano AS pp ON psp.id_plano = pp.id_plano INNER JOIN ";
            sSql = sSql + "                                professor_solicitacao_pagamento AS pspg ON psp.id_solicitacao = pspg.id_solicitacao ";
            sSql = sSql + "         WHERE(pp.id_professor = p.id_professor) AND(pspg.status = 'Solicitado')), 0) AS solicitado, ";
            sSql = sSql + "p.cpf, p.numero_documento, p.status ";
            sSql = sSql + "FROM            professores AS p INNER JOIN ";
            sSql = sSql + "                titulacao AS t ON p.id_titulo = t.id_titulacao INNER JOIN ";
            sSql = sSql + "                         professores_forma_recebimento AS pfr ON p.id_professor = pfr.id_professor LEFT OUTER JOIN ";
            sSql = sSql + "                         forma_recebimento AS frha ON pfr.horas_aula = frha.id_forma_recebimento LEFT OUTER JOIN ";
            sSql = sSql + "                         forma_recebimento AS fro ON pfr.horas_aula = fro.id_forma_recebimento LEFT OUTER JOIN ";
            sSql = sSql + "                         forma_recebimento AS frb ON pfr.horas_aula = frb.id_forma_recebimento ";
            if (pItem.nome != null)
            {
                sWhere = "p.nome like '%" + pItem.nome + "%' ";
            }
            if (pItem.id_professor != 0)
            {
                if (sWhere != "")
                {
                    sWhere = sWhere + " and ";
                }
                sWhere = sWhere + "p.id_professor = " + pItem.id_professor + " ";
            }
            if (pItem.cpf != null)
            {
                if (sWhere != "")
                {
                    sWhere = sWhere + " and ";
                }
                sWhere = sWhere + "p.cpf = " + pItem.cpf + " ";
            }
            if (pItem.status != "")
            {
                if (pItem.status == "ativado")
                {
                    if (sWhere != "")
                    {
                        sWhere = sWhere + " and ";
                    }
                    sWhere = sWhere + "p.status <> 'inativado' ";
                }
                else if (pItem.status == "inativado")
                {
                    if (sWhere != "")
                    {
                        sWhere = sWhere + " and ";
                    }
                    sWhere = sWhere + "p.status = 'inativado' ";
                }
            }

            if (sWhere != "")
            {
                sSql = sSql + " where " + sWhere;
            }

            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            SqlDataReader retorno = cmd.ExecuteReader();

            if (retorno.HasRows)
            {
                while (retorno.Read())
                {
                    item_extrato_professor = new geral_extrato_professor();

                    item_extrato_professor.id_professor = Convert.ToInt32(retorno["id_professor"]);
                    item_extrato_professor.professor = retorno["professor"].ToString();
                    item_extrato_professor.cpf = retorno["cpf"].ToString();
                    item_extrato_professor.saldo_a_solicitar = Convert.ToDecimal(retorno["saldo_a_solicitar"]);
                    item_extrato_professor.plano = Convert.ToDecimal(retorno["plano"]);
                    item_extrato_professor.pagamento = Convert.ToDecimal(retorno["pagamento"]);
                    item_extrato_professor.solicitado = Convert.ToDecimal(retorno["solicitado"]);
                    item_extrato_professor.status = retorno["status"].ToString();
                    lista_extrato_professor.Add(item_extrato_professor);
                }
            }
            return lista_extrato_professor;
        }

        public List<geral_extrato_ocorrencia> ListaExtratoOcorrencia(int qIdProfessor, int iTemBanca)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string strConnString;
            string sSql;

            List<geral_extrato_ocorrencia> lista_extrato_realizado = new List<geral_extrato_ocorrencia>();
            geral_extrato_ocorrencia item_extrato_realizado = new geral_extrato_ocorrencia();

            strConnString = ConfigurationManager.ConnectionStrings["SisProConnectionString"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "";
            sSql = sSql + "select p.id_professor, p.nome, ";
            sSql = sSql + "pplano.id_plano, pplano.data_cadastro, pplano.mes, pplano.motivo ocorrencia, pplano.valor, pplano.usuario, ";
            sSql = sSql + "psolicitacao_plano.valor_solicitado, ";
            sSql = sSql + "psolicitacao_pagamento.data_solicitacao ";
            sSql = sSql + "from professor_plano pplano ";
            sSql = sSql + "inner join professores p on p.id_professor = pplano.id_professor ";
            sSql = sSql + "left join professor_solicitacao_plano psolicitacao_plano on psolicitacao_plano.id_plano = pplano.id_plano ";
            sSql = sSql + "left join professor_solicitacao_pagamento psolicitacao_pagamento on psolicitacao_pagamento.id_solicitacao = psolicitacao_plano.id_solicitacao ";
            sSql = sSql + "where p.id_professor = " + qIdProfessor ;
            if (iTemBanca == 0)
            {
                sSql = sSql + " and pplano.motivo <> 'Banca' ";
            }
            else if (iTemBanca == 2)
            {
                sSql = sSql + " and pplano.motivo = 'Banca' ";
            }
            sSql = sSql + " order by pplano.mes, pplano.id_plano, psolicitacao_pagamento.data_solicitacao, pplano.motivo ";

            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            SqlDataReader retorno = cmd.ExecuteReader();

            int sAux = 0;
            item_extrato_realizado = new geral_extrato_ocorrencia();

            if (retorno.HasRows)
            {
                while (retorno.Read())
                {
                    if (sAux == 0 || sAux == Convert.ToInt32(retorno["id_plano"]))
                    {
                        item_extrato_realizado.id_professor = Convert.ToInt32(retorno["id_professor"]);
                        item_extrato_realizado.professor = retorno["nome"].ToString();
                        item_extrato_realizado.id_plano = Convert.ToInt32(retorno["id_plano"]);
                        item_extrato_realizado.data_cadastro = Convert.ToDateTime(retorno["data_cadastro"].ToString());
                        item_extrato_realizado.mes = String.Format("{0:MM/yyyy}", retorno["mes"]);
                        item_extrato_realizado.motivo = retorno["ocorrencia"].ToString();
                        item_extrato_realizado.valor_atual = Convert.ToDecimal(retorno["valor"]);// string.Format("{0:C}", Convert.ToDecimal(retorno["valor"])); 
                        item_extrato_realizado.usuario = retorno["usuario"].ToString();

                        if (retorno["valor_solicitado"].ToString() != "")
                        {
                            if (item_extrato_realizado.valor_solicitado == null)
                            {
                                item_extrato_realizado.valor_solicitado = Convert.ToDecimal(retorno["valor_solicitado"]).ToString("#,###,###,##0.00"); //string.Format("{0:C}", Convert.ToDecimal(retorno["valor_solicitado"]));
                                item_extrato_realizado.data_solicitacao = String.Format("{0:dd/MM/yyyy}", retorno["data_solicitacao"]);
                            }
                            else
                            {
                                item_extrato_realizado.valor_solicitado = item_extrato_realizado.valor_solicitado + "<hr>" + Convert.ToDecimal(retorno["valor_solicitado"]).ToString("#,###,###,##0.00"); //string.Format("{0:C}", Convert.ToDecimal(retorno["valor_solicitado"]));
                                item_extrato_realizado.data_solicitacao = item_extrato_realizado.data_solicitacao + "<hr>" + String.Format("{0:dd/MM/yyyy}", retorno["data_solicitacao"]);
                            }
                        }
                        
                        
                    }
                    else
                    {
                        lista_extrato_realizado.Add(item_extrato_realizado);
                        item_extrato_realizado = new geral_extrato_ocorrencia();

                        item_extrato_realizado.id_professor = Convert.ToInt32(retorno["id_professor"]);
                        item_extrato_realizado.professor = retorno["nome"].ToString();
                        item_extrato_realizado.id_plano = Convert.ToInt32(retorno["id_plano"]);
                        item_extrato_realizado.data_cadastro = Convert.ToDateTime(retorno["data_cadastro"].ToString());
                        item_extrato_realizado.mes = String.Format("{0:MM/yyyy}", retorno["mes"]);
                        item_extrato_realizado.motivo = retorno["ocorrencia"].ToString();
                        item_extrato_realizado.valor_atual = Convert.ToDecimal(retorno["valor"]);// string.Format("{0:C}", Convert.ToDecimal(retorno["valor"])); 
                        item_extrato_realizado.usuario = retorno["usuario"].ToString();
                        if (retorno["valor_solicitado"].ToString() != "")
                        {
                            item_extrato_realizado.valor_solicitado = Convert.ToDecimal(retorno["valor_solicitado"]).ToString("#,###,###,##0.00"); //string.Format("{0:C}", Convert.ToDecimal(retorno["valor_solicitado"]));
                            item_extrato_realizado.data_solicitacao = String.Format("{0:dd/MM/yyyy}", retorno["data_solicitacao"]);
                        }
                    }
                    sAux = Convert.ToInt32(retorno["id_plano"]);
                }

                lista_extrato_realizado.Add(item_extrato_realizado);
            }

            return lista_extrato_realizado;
        }

        public List<geral_extrato_solicitado_pago> ListaExtratoSolicitadoPago(int qIdProfessor)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string strConnString;
            string sSql;

            List<geral_extrato_solicitado_pago> lista_extrato_solicitado_pago = new List<geral_extrato_solicitado_pago>();
            geral_extrato_solicitado_pago item_extrato_solicitado_pago = new geral_extrato_solicitado_pago();

            strConnString = ConfigurationManager.ConnectionStrings["SisProConnectionString"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "";
            sSql = sSql + "select p.id_professor, p.nome, pSolPagto.*, pplano.id_plano, pplano.motivo ";
            sSql = sSql + "from professor_plano pplano ";
            sSql = sSql + "inner join professores p on p.id_professor = pplano.id_professor ";
            sSql = sSql + "inner join professor_solicitacao_plano pSolPlano on pSolPlano.id_plano = pplano.id_plano ";
            sSql = sSql + "inner join professor_solicitacao_pagamento pSolPagto on pSolPagto.id_solicitacao = pSolPlano.id_solicitacao ";
            sSql = sSql + "where p.id_professor = " + qIdProfessor;
            sSql = sSql + "order by pSolPagto.data_solicitacao ";

            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            SqlDataReader retorno = cmd.ExecuteReader();

            int sAux = 0;
            item_extrato_solicitado_pago = new geral_extrato_solicitado_pago();

            if (retorno.HasRows)
            {
                while (retorno.Read())
                {
                    if (sAux == 0 || sAux == Convert.ToInt32(retorno["id_solicitacao"]))
                    {
                        item_extrato_solicitado_pago.id_professor = Convert.ToInt32(retorno["id_professor"]);
                        item_extrato_solicitado_pago.professor = retorno["nome"].ToString();
                        item_extrato_solicitado_pago.id_solicitacao = Convert.ToInt32(retorno["id_solicitacao"]);
                        item_extrato_solicitado_pago.data_solicitacao = Convert.ToDateTime(retorno["data_solicitacao"]); // String.Format("{0:dd/MM/yyyy}", retorno["data_solicitacao"]);
                        item_extrato_solicitado_pago.valor = Convert.ToDecimal(retorno["valor"]);  //string.Format("{0:C}", Convert.ToDecimal(retorno["valor"]));
                        item_extrato_solicitado_pago.nota_fiscal = retorno["nota_fiscal"].ToString();
                        item_extrato_solicitado_pago.usuario = retorno["usuario"].ToString();
                        item_extrato_solicitado_pago.status = retorno["status"].ToString();

                        if (item_extrato_solicitado_pago.id_plano == null)
                        {
                            item_extrato_solicitado_pago.id_plano = retorno["id_plano"].ToString();
                            item_extrato_solicitado_pago.motivo = retorno["motivo"].ToString();
                        }
                        else
                        {
                            item_extrato_solicitado_pago.id_plano = item_extrato_solicitado_pago.id_plano + "<hr>" + retorno["id_plano"].ToString(); ;
                            item_extrato_solicitado_pago.motivo = item_extrato_solicitado_pago.motivo + "<hr>" + retorno["motivo"].ToString();
                        }
                    }
                    else
                    {
                        lista_extrato_solicitado_pago.Add(item_extrato_solicitado_pago);
                        item_extrato_solicitado_pago = new geral_extrato_solicitado_pago();

                        item_extrato_solicitado_pago.id_professor = Convert.ToInt32(retorno["id_professor"]);
                        item_extrato_solicitado_pago.professor = retorno["nome"].ToString();
                        item_extrato_solicitado_pago.id_solicitacao = Convert.ToInt32(retorno["id_solicitacao"]);
                        item_extrato_solicitado_pago.data_solicitacao = Convert.ToDateTime(retorno["data_solicitacao"]); // String.Format("{0:dd/MM/yyyy}", retorno["data_solicitacao"]);
                        item_extrato_solicitado_pago.valor = Convert.ToDecimal(retorno["valor"]);  //string.Format("{0:C}", Convert.ToDecimal(retorno["valor"]));
                        item_extrato_solicitado_pago.nota_fiscal = retorno["nota_fiscal"].ToString();
                        item_extrato_solicitado_pago.usuario = retorno["usuario"].ToString();
                        item_extrato_solicitado_pago.status = retorno["status"].ToString();
                        item_extrato_solicitado_pago.id_plano = retorno["id_plano"].ToString();
                        item_extrato_solicitado_pago.motivo = retorno["motivo"].ToString();

                    }
                    sAux = Convert.ToInt32(retorno["id_solicitacao"]);
                }

                lista_extrato_solicitado_pago.Add(item_extrato_solicitado_pago);
            }

            return lista_extrato_solicitado_pago;
        }

        public professores Cria_altera_Observacoes_Plano(professor_observacoes_plano pItem)
        {
            professor_observacoes_plano item = new professor_observacoes_plano();
            professores item_professor;

            item = contextoEF.professor_observacoes_plano.Where(x => x.id_professor == pItem.id_professor).FirstOrDefault();
            if (item == null)
            {
                item = new professor_observacoes_plano();

                item.id_professor = pItem.id_professor;
                item.observacoes = pItem.observacoes;

                contextoEF.professor_observacoes_plano.Add(item);

                contextoEF.SaveChanges();

                item_professor = contextoEF.professores.Where(x => x.id_professor == pItem.id_professor).FirstOrDefault();
                item_professor.professor_observacoes_plano = item;

            }
            else
            {
                //contextoEF.Database.ExecuteSqlCommand("UPDATE professor_observacoes_plano SET observacoes='" + pItem.observacoes + "' WHERE id_professor=" + pItem.id_professor );

                item.observacoes = pItem.observacoes;

                contextoEF.SaveChanges();

                item_professor = contextoEF.professores.Where(x => x.id_professor == pItem.id_professor).FirstOrDefault();
            }
            return item_professor;
        }

        //Utilizado na tela de Extrato Professor (finCalculoCustoCurso) Fim

        //Utilizado na tela de Solicitação Pagto Professor (finSolicitacaoPagtoProfessor) Início

        ////Botão Recalcular Plano - Início
        public void RecalcularHorasAulas(int idProfessor, DateTime dDataInicio, DateTime dDataFim, string usuario)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string strConnString;
            string sSql;
            DateTime dDataTemp = new DateTime();
            int idProfessorAux;
            decimal dValor;
            professor_plano item;

            dDataTemp = dDataInicio;

            strConnString = ConfigurationManager.ConnectionStrings["SisProConnectionString"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);

            do
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter sda = new SqlDataAdapter();
                sSql = "";
                sSql = sSql + "SELECT cd.id_curso, dap.id_professor, cvha.valor,  isnull(sum(dap.hora_aula), 0.0) as hora_aula, cvha.valor* isnull(sum(dap.hora_aula), 0.0) valor_total ";
                sSql = sSql + "FROM datas_aulas_professor dap ";
                sSql = sSql + "INNER JOIN datas_aulas da ON dap.id_aula = da.id_aula ";
                sSql = sSql + "INNER JOIN oferecimentos o ON da.id_oferecimento = o.id_oferecimento and o.status <> 'inativado' ";
                sSql = sSql + "INNER JOIN cursos_disciplinas cd ON o.id_disciplina = cd.id_disciplina ";
                sSql = sSql + "INNER JOIN presenca_professor pp ON dap.id_aula = pp.id_aula AND dap.id_professor = pp.id_professor ";
                sSql = sSql + " ";
                sSql = sSql + "inner join professores p on p.id_professor = dap.id_professor ";
                sSql = sSql + "inner join professores_forma_recebimento pf on pf.id_professor = dap.id_professor ";
                sSql = sSql + "inner join forma_recebimento fh on fh.id_forma_recebimento = pf.banca ";
                sSql = sSql + "inner join curso_valor_hora_aula cvha on cvha.id_forma_recebimento = pf.horas_aula and cvha.id_curso = cd.id_curso and cvha.id_titulacao = p.id_titulo ";
                sSql = sSql + "WHERE(YEAR(da.data_aula) = '" + dDataTemp.Year.ToString() + "') AND(MONTH(da.data_aula) = '" + dDataTemp.Month.ToString() + "') ";
                sSql = sSql + "      AND(pp.presente = '1') ";
                if (idProfessor != 0)
                {
                    sSql = sSql + "        AND dap.id_professor = " + idProfessor + " ";
                }
                sSql = sSql + "GROUP BY p.nome, cd.id_curso, dap.id_professor, cvha.valor order by dap.id_professor ";

                if (cCon.State.ToString()  != "Open")
                {
                    cCon.Open();
                }
                cmd.Connection = cCon;
                cmd.CommandText = sSql;
                SqlDataReader retorno = cmd.ExecuteReader();

                //Aqui será DELETADO todos os registros da tabela (professor_plano) com o mes/ano correspondente que NÃO foi feito nenhuma Solicitação (Tabela professor_solicitacao_plano)
                if (idProfessor != 0)
                {
                    contextoEF.professor_plano.RemoveRange(contextoEF.professor_plano.Where(x => x.id_professor == idProfessor && x.mes.Month == dDataTemp.Month && x.mes.Year == dDataTemp.Year && x.motivo == "Horas Aula" && x.professor_solicitacao_plano.Count == 0));
                    var sAux = contextoEF.professor_plano.Where(x => x.id_professor == idProfessor && x.mes.Month == dDataTemp.Month && x.mes.Year == dDataTemp.Year && x.motivo == "Horas Aula" && x.professor_solicitacao_plano.Count == 0).Select(x => x.id_plano).ToArray();
                }
                else
                {
                    contextoEF.professor_plano.RemoveRange(contextoEF.professor_plano.Where(x => x.mes.Month == dDataTemp.Month && x.mes.Year == dDataTemp.Year && x.motivo == "Horas Aula" && x.professor_solicitacao_plano.Count == 0));
                }
                
                contextoEF.SaveChanges();

                if (retorno.HasRows)
                {
                    idProfessorAux = 0;
                    dValor = 0;
                    while (retorno.Read())
                    {
                        if (idProfessorAux != Convert.ToInt32(retorno["id_professor"]) && idProfessorAux != 0)
                        {
                            item = contextoEF.professor_plano.Where(x => x.id_professor == idProfessorAux && x.mes.Month == dDataTemp.Month && x.mes.Year == dDataTemp.Year && x.motivo == "Horas Aula").SingleOrDefault();
                            if (item == null)
                            {
                                item = new professor_plano();
                                item.id_professor = idProfessorAux;
                                item.mes = dDataTemp;
                                item.valor = dValor;
                                item.motivo = "Horas Aula";
                                item.data_cadastro = DateTime.Now;
                                item.data_alteracao = item.data_alteracao;
                                item.usuario = usuario;
                                contextoEF.professor_plano.Add(item);
                            }
                            else
                            {
                                item.valor = dValor;
                                item.data_alteracao = DateTime.Now;
                                item.usuario = usuario;
                            }
                            contextoEF.SaveChanges();
                            dValor = 0;
                        }
                        dValor = dValor + Convert.ToDecimal(retorno["valor_total"]);
                        idProfessorAux = Convert.ToInt32(retorno["id_professor"]);
                    }

                    item = contextoEF.professor_plano.Where(x => x.id_professor == idProfessorAux && x.mes.Month == dDataTemp.Month && x.mes.Year == dDataTemp.Year && x.motivo == "Horas Aula").SingleOrDefault();
                    if (item == null)
                    {
                        item = new professor_plano();
                        item.id_professor = idProfessorAux;
                        item.mes = dDataTemp;
                        item.valor = dValor;
                        item.motivo = "Horas Aula";
                        item.data_cadastro = DateTime.Now;
                        item.data_alteracao = item.data_alteracao;
                        item.usuario = usuario;
                        contextoEF.professor_plano.Add(item);
                    }
                    else
                    {
                        item.valor = dValor;
                        item.data_alteracao = DateTime.Now;
                        item.usuario = usuario;
                    }
                    contextoEF.SaveChanges();
                }
                
                dDataTemp = dDataTemp.AddMonths(1);
            } while (dDataTemp <= dDataFim);

            cCon.Close();
        }

        public void RecalcularOrientacoes(int idProfessor, DateTime dDataInicio, DateTime dDataFim, string usuario)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string strConnString;
            string sSql;
            DateTime dDataTemp = new DateTime();
            int idProfessorAux;
            decimal dValor;
            professor_plano item;

            dDataTemp = dDataInicio;

            strConnString = ConfigurationManager.ConnectionStrings["SisProConnectionString"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);

            do
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter sda = new SqlDataAdapter();
                sSql = "";
                sSql = sSql + "SELECT p.id_professor, p.nome, ";

                sSql = sSql + "        isnull(sum((CASE WHEN b.tipo_banca = 'defesa' THEN cvo.valor_defesa ELSE cvo.valor_qualificacao END)), 0.0) as valor ";
                sSql = sSql + "FROM professores p ";
                sSql = sSql + "inner join professores_forma_recebimento pf on pf.id_professor = p.id_professor ";
                //== Tirado o Co-orientador segundo email do prof Eduardo em 08/04/2021 as 10:42
                //sSql = sSql + "inner join banca_professores bp on bp.id_professor = p.id_professor and(bp.tipo_professor = 'orientador' OR bp.tipo_professor = 'co-orientador') ";
                sSql = sSql + "inner join banca_professores bp on bp.id_professor = p.id_professor and(bp.tipo_professor = 'orientador') ";
                sSql = sSql + "inner join banca b on b.id_banca = bp.id_banca ";
                sSql = sSql + "inner join matricula_turma mt on b.id_matricula_turma = mt.id_matricula_turma ";
                sSql = sSql + "inner join turmas t on t.id_turma = mt.id_turma ";
                sSql = sSql + "inner join cursos c on c.id_curso = t.id_curso ";
                sSql = sSql + "inner join curso_valor_orientacao cvo on cvo.id_curso = c.id_curso and cvo.id_forma_recebimento = pf.orientacao ";
                sSql = sSql + " ";
                sSql = sSql + "WHERE(CASE b.tipo_banca ";
                sSql = sSql + " ";
                sSql = sSql + "        WHEN 'Defesa' ";
                sSql = sSql + " ";
                sSql = sSql + "            THEN CASE WHEN(YEAR(b.data_entrega_trabalho) = " + dDataTemp.Year.ToString() + " AND MONTH(b.data_entrega_trabalho) = " + dDataTemp.Month.ToString() + ") THEN 1 ELSE 0 END ";
                sSql = sSql + "        ELSE CASE WHEN(YEAR(b.horario) = " + dDataTemp.Year.ToString() + " AND MONTH(b.horario) = " + dDataTemp.Month.ToString() + " ";
                sSql = sSql + "      AND(resultado = 'Aprovado') ";
                sSql = sSql + "        ) THEN 1 ELSE 0 END END) = 1 ";
                if (idProfessor != 0) 
                {
                    sSql = sSql + "        AND p.id_professor = " + idProfessor + " ";
                }
                sSql = sSql + "GROUP BY p.id_professor, p.nome, cvo.valor_defesa, cvo.valor_qualificacao ";

                if (cCon.State.ToString() != "Open")
                {
                    cCon.Open();
                }
                cmd.Connection = cCon;
                cmd.CommandText = sSql;
                SqlDataReader retorno = cmd.ExecuteReader();

                if (retorno.HasRows)
                {
                    idProfessorAux = 0;
                    dValor = 0;
                    while (retorno.Read())
                    {
                        if (idProfessorAux != Convert.ToInt32(retorno["id_professor"]) && idProfessorAux != 0)
                        {
                            item = contextoEF.professor_plano.Where(x => x.id_professor == idProfessorAux && x.mes.Month == dDataTemp.Month && x.mes.Year == dDataTemp.Year && x.motivo == "Orientação").SingleOrDefault();
                            if (item == null)
                            {
                                item = new professor_plano();
                                item.id_professor = idProfessorAux;
                                item.mes = dDataTemp;
                                item.valor = dValor;
                                item.motivo = "Orientação";
                                item.data_cadastro = DateTime.Now;
                                item.data_alteracao = item.data_alteracao;
                                item.usuario = usuario;
                                contextoEF.professor_plano.Add(item);
                            }
                            else
                            {
                                item.valor = dValor;
                                item.data_alteracao = DateTime.Now;
                                item.usuario = usuario;
                            }
                            contextoEF.SaveChanges();
                            dValor = 0;
                        }
                        dValor = dValor + Convert.ToDecimal(retorno["valor"]);
                        idProfessorAux = Convert.ToInt32(retorno["id_professor"]);
                    }

                    item = contextoEF.professor_plano.Where(x => x.id_professor == idProfessorAux && x.mes.Month == dDataTemp.Month && x.mes.Year == dDataTemp.Year && x.motivo == "Orientação").SingleOrDefault();
                    if (item == null)
                    {
                        item = new professor_plano();
                        item.id_professor = idProfessorAux;
                        item.mes = dDataTemp;
                        item.valor = dValor;
                        item.motivo = "Orientação";
                        item.data_cadastro = DateTime.Now;
                        item.data_alteracao = item.data_alteracao;
                        item.usuario = usuario;
                        contextoEF.professor_plano.Add(item);
                    }
                    else
                    {
                        item.valor = dValor;
                        item.data_alteracao = DateTime.Now;
                        item.usuario = usuario;
                    }
                    contextoEF.SaveChanges();
                }

                dDataTemp = dDataTemp.AddMonths(1);
            } while (dDataTemp <= dDataFim);

            cCon.Close();
        }

        public void RecalcularBancas(int idProfessor, DateTime dDataInicio, DateTime dDataFim, string usuario)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string strConnString;
            string sSql;
            DateTime dDataTemp = new DateTime();
            int idProfessorAux;
            decimal dValor;
            professor_plano item;

            dDataTemp = dDataInicio;

            strConnString = ConfigurationManager.ConnectionStrings["SisProConnectionString"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);

            do
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter sda = new SqlDataAdapter();
                sSql = "";
                sSql = sSql + "SELECT p.id_professor, p.nome, ";
                sSql = sSql + "        isnull(sum((CASE isNull(pf.banca, 1) WHEN 8 THEN cvb.valor_sao_paulo WHEN 9 THEN cvb.valor_fora_sao_paulo ELSE 0.0 END)), 0.0) as valor ";
                sSql = sSql + "FROM professores p ";
                sSql = sSql + "inner join professores_forma_recebimento pf on pf.id_professor = p.id_professor AND isNull(pf.banca, 1) > 1 ";
                sSql = sSql + "inner join banca_professores bp on bp.id_professor = p.id_professor and(bp.tipo_professor = 'membro') ";
                sSql = sSql + "inner join banca b on b.id_banca = bp.id_banca ";
                sSql = sSql + "inner join matricula_turma mt on b.id_matricula_turma = mt.id_matricula_turma ";
                sSql = sSql + "inner join turmas t on t.id_turma = mt.id_turma ";
                sSql = sSql + "inner join cursos c on c.id_curso = t.id_curso ";
                sSql = sSql + "inner join curso_valor_banca cvb on cvb.id_curso = c.id_curso ";
                sSql = sSql + "WHERE YEAR(b.horario) = " + dDataTemp.Year.ToString() + " AND MONTH(b.horario) = " + dDataTemp.Month.ToString() + " ";
                //sSql = sSql + "        AND(resultado = 'Aprovado' or resultado = 'Reprovado') ";
                if (idProfessor != 0)
                {
                    sSql = sSql + "        AND p.id_professor = " + idProfessor + " ";
                }
                sSql = sSql + "GROUP BY p.id_professor, p.nome ";
                sSql = sSql + "order by p.id_professor ";

                if (cCon.State.ToString() != "Open")
                {
                    cCon.Open();
                }
                cmd.Connection = cCon;
                cmd.CommandText = sSql;
                SqlDataReader retorno = cmd.ExecuteReader();

                if (retorno.HasRows)
                {
                    idProfessorAux = 0;
                    dValor = 0;
                    while (retorno.Read())
                    {
                        if (idProfessorAux != Convert.ToInt32(retorno["id_professor"]) && idProfessorAux != 0)
                        {
                            item = contextoEF.professor_plano.Where(x => x.id_professor == idProfessorAux && x.mes.Month == dDataTemp.Month && x.mes.Year == dDataTemp.Year && x.motivo == "Banca").SingleOrDefault();
                            if (item == null)
                            {
                                item = new professor_plano();
                                item.id_professor = idProfessorAux;
                                item.mes = dDataTemp;
                                item.valor = dValor;
                                item.motivo = "Banca";
                                item.data_cadastro = DateTime.Now;
                                item.data_alteracao = item.data_alteracao;
                                item.usuario = usuario;
                                contextoEF.professor_plano.Add(item);
                            }
                            else
                            {
                                item.valor = dValor;
                                item.data_alteracao = DateTime.Now;
                                item.usuario = usuario;
                            }
                            contextoEF.SaveChanges();
                            dValor = 0;
                        }
                        dValor = dValor + Convert.ToDecimal(retorno["valor"]);
                        idProfessorAux = Convert.ToInt32(retorno["id_professor"]);
                    }

                    item = contextoEF.professor_plano.Where(x => x.id_professor == idProfessorAux && x.mes.Month == dDataTemp.Month && x.mes.Year == dDataTemp.Year && x.motivo == "Banca").SingleOrDefault();
                    if (item == null)
                    {
                        item = new professor_plano();
                        item.id_professor = idProfessorAux;
                        item.mes = dDataTemp;
                        item.valor = dValor;
                        item.motivo = "Banca";
                        item.data_cadastro = DateTime.Now;
                        item.data_alteracao = item.data_alteracao;
                        item.usuario = usuario;
                        contextoEF.professor_plano.Add(item);
                    }
                    else
                    {
                        item.valor = dValor;
                        item.data_alteracao = DateTime.Now;
                        item.usuario = usuario;
                    }
                    contextoEF.SaveChanges();
                }

                dDataTemp = dDataTemp.AddMonths(1);
            } while (dDataTemp <= dDataFim);

            cCon.Close();
        }

        public void RecalcularCoordenacao(int idProfessor, DateTime dDataInicio, DateTime dDataFim, string usuario)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            List<geral_custo_coordenacao> lista_geral_coordenacao = new List<geral_custo_coordenacao>();
            geral_custo_coordenacao item_geral_custo_coordenacao = new geral_custo_coordenacao();
            professor_plano item;


            //Encontrar na tabela "valor_custo_coordenacao" se há valoes > 0
            List<curso_valor_coordenacao> lista_valor_cordenacao = contextoEF.curso_valor_coordenacao.Where(x => x.valor > 0).ToList();

            List<turmas> lista_turmas_abertas;

            DateTime qDataInicio;
            DateTime qDataFim;
            DateTime qDataComparacao = dDataInicio;
            //decimal dTotalCoordenacao = 0;
            bool bCalcula = true;
            decimal dValor = 0;

            do
            {
                qDataInicio = (qDataComparacao.AddMonths(1)).AddDays(-1);
                qDataFim = qDataComparacao;

                var qIdCurso = lista_valor_cordenacao.Select(x => x.id_curso).ToArray();

                lista_turmas_abertas = contextoEF.turmas.Where(x => qIdCurso.Contains(x.id_curso) && x.data_inicio.Value <= qDataInicio && x.data_fim.Value >= qDataFim).ToList();




                //Estou com a lista da tabela "valor_custo_coordenacao" que tem valoes > 0
                foreach (var elemento in lista_valor_cordenacao)
                {
                    //Verificar se tem turma(s) abertas para os curso
                    if (elemento.cursos.turmas.Any(x => x.data_inicio.Value <= qDataInicio && x.data_fim.Value >= qDataFim))
                    {
                        //estou com um elemento (coordenador de Curso ou de Área)
                        List<turmas> lista_turma;
                        lista_turma = elemento.cursos.turmas.Where(x => x.data_inicio.Value <= qDataInicio && x.data_fim.Value >= dDataFim).ToList();
                        string sTurma = "";
                        foreach (var elemento2 in lista_turma)
                        {
                            if (sTurma != "")
                            {
                                sTurma = sTurma + " - ";
                            }
                            sTurma = sTurma + elemento2.cod_turma;
                        }

                        cursos_coordenadores item_coordenadores;
                        item_coordenadores = contextoEF.cursos_coordenadores.Where(x => x.id_tipo_coordenador == elemento.id_tipo_coordenador && x.id_curso == elemento.id_curso && x.id_professor == idProfessor).SingleOrDefault();

                        if (item_coordenadores != null)
                        {
                            dValor = dValor + elemento.valor.Value;
                        }
                    }
                }

                item = contextoEF.professor_plano.Where(x => x.id_professor == idProfessor && x.mes.Month == qDataInicio.Month && x.mes.Year == qDataInicio.Year && x.motivo == "Coordenação").SingleOrDefault();
                if (item == null)
                {
                    item = new professor_plano();
                    item.id_professor = idProfessor;
                    item.mes = qDataInicio;
                    item.valor = dValor;
                    item.motivo = "Coordenação";
                    item.data_cadastro = DateTime.Now;
                    item.data_alteracao = item.data_alteracao;
                    item.usuario = usuario;
                    contextoEF.professor_plano.Add(item);
                }
                else
                {
                    item.valor = dValor;
                    item.data_alteracao = DateTime.Now;
                    item.usuario = usuario;
                }
                contextoEF.SaveChanges();
                dValor = 0;

                qDataComparacao = qDataComparacao.AddMonths(1);
                if (qDataComparacao > dDataFim)
                {
                    bCalcula = false;
                    break;
                }

            } while (bCalcula);

        }

        ////Botão Recalcular Plano - Fim

        ////Botão Solicitação de Pagto - Início

        public List<geral_SolicitacaoPagto> ListaSolicitaoPagto(int idProfessor)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string strConnString;
            string sSql;
            DateTime dData;

            List<geral_SolicitacaoPagto> lista = new List<geral_SolicitacaoPagto>();
            geral_SolicitacaoPagto item = new geral_SolicitacaoPagto();

            strConnString = ConfigurationManager.ConnectionStrings["SisProConnectionString"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "";
            sSql = sSql + "SELECT t.*, ISNULL(t.valor-t.valor_solicitado, 0) valor_pagar  FROM  ";
            sSql = sSql + "    (SELECT p.nome as professor, pp.*, ";
            sSql = sSql + "    valor_solicitado = ISNULL((SELECT SUM(psp.valor_solicitado) ";
            sSql = sSql + "                     FROM professor_solicitacao_plano psp ";
            sSql = sSql + "                     WHERE pp.id_plano = psp.id_plano), 0) ";
            sSql = sSql + "    FROM professor_plano pp ";
            sSql = sSql + "    INNER JOIN professores p ON pp.id_professor = p.id_professor) t ";
            sSql = sSql + "WHERE(t.valor - t.valor_solicitado <> 0) ";
            if (idProfessor != 0)
            {
                sSql = sSql + "        AND(t.id_professor = " + idProfessor + ") ";
            }
            sSql = sSql + "AND(t.motivo <> 'Banca') ";
            sSql = sSql + "ORDER BY t.id_professor, t.mes, t.motivo ";

            if (cCon.State.ToString() != "Open")
            {
                cCon.Open();
            }
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            SqlDataReader retorno = cmd.ExecuteReader();

            if (retorno.HasRows)
            {
                while (retorno.Read())
                {
                    item = new geral_SolicitacaoPagto();
                    item.id_professor = Convert.ToInt32(retorno["id_professor"]);
                    item.id_plano = Convert.ToInt32(retorno["id_plano"]);
                    dData = Convert.ToDateTime(retorno["mes"]);
                    item.motivo = retorno["motivo"].ToString();
                    item.mes = Convert.ToDateTime(retorno["mes"]);
                    item.mes_string = dData.Month.ToString("00") + "/" + dData.Year.ToString();
                    item.valor = Convert.ToDecimal(retorno["valor"]);
                    item.valor_solicitado = Convert.ToDecimal(retorno["valor_solicitado"]);
                    item.valor_pagar = Convert.ToDecimal(retorno["valor_pagar"]);
                    lista.Add(item);
                }
            }

            cCon.Close();

            return lista;
        }

        public string AdicionaSolicitacaoPagto (DateTime qData, decimal qValorTotal, string qPlanoValor, string qUsuario)
        {
            professor_solicitacao_pagamento item = new professor_solicitacao_pagamento();
            professor_solicitacao_plano itemPlano = new professor_solicitacao_plano();

            item.data_solicitacao = qData;
            item.valor = qValorTotal;
            item.status = "Solicitado";
            item.data_alteracao = DateTime.Now;
            item.usuario = qUsuario;

            contextoEF.professor_solicitacao_pagamento.Add(item);
            contextoEF.SaveChanges();

            var qPlano = qPlanoValor.Split('-');
            
            foreach (var elemento in qPlano)
            {
                if (elemento != "")
                {
                    var elemento2 = elemento.Split('=');
                    itemPlano = new professor_solicitacao_plano();

                    itemPlano.id_solicitacao = item.id_solicitacao;
                    itemPlano.id_plano = Convert.ToInt32(elemento2[0]);
                    itemPlano.valor_solicitado = Convert.ToDecimal(elemento2[1]);

                    contextoEF.professor_solicitacao_plano.Add(itemPlano);
                    contextoEF.SaveChanges();
                }

            }
            return "ok";
        }

        ////Botão Solicitação de Pagto - Fim

        ////Botão Preparar Email - Início
        public List<geral_solicitado_professor> ListaSolicitacoesPagto(professor_solicitacao_pagamento pItem)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string strConnString;
            string sSql;

            List<geral_solicitado_professor> lista_extrato_professor = new List<geral_solicitado_professor>();
            geral_solicitado_professor item_extrato_professor;

            strConnString = ConfigurationManager.ConnectionStrings["SisProConnectionString"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "";
            sSql = sSql + "SELECT        pspagto.data_solicitacao, pspagto.valor tSolicitacaoPagto, psplano.valor_solicitado tSolicitacaoPlano, pp.valor tProfessorPlano, p.nome, p.email, p.email2,";
             sSql = sSql + "pp.mes, pp.motivo, pspagto.nota_fiscal, pspagto.data_recebimento, pspagto.status, pspagto.data_alteracao, pspagto.usuario,  ";
            sSql = sSql + "pspagto.id_solicitacao, pspagto.data_email, pspagto.data_pagamento ";
            sSql = sSql + "FROM          professor_solicitacao_pagamento pspagto ";
            sSql = sSql + "inner join    professor_solicitacao_plano psplano on pspagto.id_solicitacao = psplano.id_solicitacao ";
            sSql = sSql + "inner join    professor_plano pp on pp.id_plano = psplano.id_plano ";
            sSql = sSql + "inner join    professores p on p.id_professor = pp.id_professor ";
            sSql = sSql + "WHERE ";
            if (pItem.status  != "")
            {
                sSql = sSql + "pspagto.status = '" + pItem.status + "' and ";
            }
            sSql = sSql + "(pspagto.id_solicitacao IN ";
            sSql = sSql + "              (SELECT        psp.id_solicitacao ";
            sSql = sSql + "                FROM            professor_solicitacao_plano AS psp INNER JOIN ";
            sSql = sSql + "                                          professor_plano AS pp ON psp.id_plano = pp.id_plano ";
            sSql = sSql + "                WHERE pp.id_professor = " + pItem.id_professorOld + ")) ";

            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            SqlDataReader retorno = cmd.ExecuteReader();

            if (retorno.HasRows)
            {
                while (retorno.Read())
                {
                    item_extrato_professor = new geral_solicitado_professor();

                    //item_extrato_professor.id_professor = Convert.ToInt32(retorno["id_professor"]);
                    item_extrato_professor.professor = retorno["nome"].ToString();
                    item_extrato_professor.email = retorno["email"].ToString();
                    item_extrato_professor.email2 = retorno["email2"].ToString();
                    item_extrato_professor.valor_t_SolicitacaoPagto = Convert.ToDecimal(retorno["tSolicitacaoPagto"]);
                    item_extrato_professor.valor_t_SolicitacaoPlano = Convert.ToDecimal(retorno["tSolicitacaoPlano"]);
                    item_extrato_professor.valor_t_ProfessorPlano = Convert.ToDecimal(retorno["tProfessorPlano"]);
                    item_extrato_professor.mes_plano = Convert.ToDateTime(retorno["mes"]);
                    item_extrato_professor.motivo = retorno["motivo"].ToString();
                    item_extrato_professor.nota_fiscal = retorno["nota_fiscal"].ToString();
                    if (retorno["data_recebimento"].ToString() != "" )
                    {
                        item_extrato_professor.data_recebimento = Convert.ToDateTime(retorno["data_recebimento"]);
                    }
                    item_extrato_professor.status = retorno["status"].ToString();
                    item_extrato_professor.data_alteracao = Convert.ToDateTime(retorno["data_alteracao"]);
                    item_extrato_professor.usuario = retorno["usuario"].ToString();
                    item_extrato_professor.id_solicitacao = Convert.ToInt32(retorno["id_solicitacao"]);
                    if (retorno["data_email"].ToString() != "")
                    {
                        item_extrato_professor.data_recebimento = Convert.ToDateTime(retorno["data_email"]);
                    }
                    if (retorno["data_pagamento"].ToString() != "")
                    {
                        item_extrato_professor.data_recebimento = Convert.ToDateTime(retorno["data_pagamento"]);
                    }

                    lista_extrato_professor.Add(item_extrato_professor);
                }
            }
            return lista_extrato_professor;
        }

        public List<geral_horas_aulas_dadas> ListaHorasAulasDadas(professor_solicitacao_pagamento pItem)
        {
            //string sAux_data = "'" + pItem.data_solicitacao.ToString().Replace("/","-") + "'";
            DateTime sAux_data = pItem.data_solicitacao;

            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string strConnString;
            string sSql;

            List<geral_horas_aulas_dadas> lista = new List<geral_horas_aulas_dadas>();
            geral_horas_aulas_dadas item;

            strConnString = ConfigurationManager.ConnectionStrings["SisProConnectionString"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "";
            sSql = sSql + "SELECT dt.*, d.codigo, disciplina=d.nome, p.nome as professor, fr.id_forma_recebimento as recebe_como, p.id_titulo, t.nome as titulo_academico,  ";
            sSql = sSql + "t.reduzido as titulo_reduzido, fr.nome as forma_recebimento, fr.valor_fixo, valor_hora = isnull(cvha.valor, 0.0),  ";
            sSql = sSql + "total_horas = 0.0, pfr.horas_aula_adicional, valor_hora_adicional = isnull(cvhaa.valor, 0.0),  ";
            sSql = sSql + "horas_extras = CASE WHEN isnull(cvhaa.valor, 0.0)> 0 AND dt.total_horas_mes_atual > dt.total_horas_mes_anterior AND dt.total_horas_mes_atual > pfr.horas_clt ";
            sSql = sSql + "               THEN CASE WHEN dt.total_horas_mes_anterior > pfr.horas_clt ";
            sSql = sSql + "                    THEN dt.total_horas_mes_atual - dt.total_horas_mes_anterior ";
            sSql = sSql + "                    ELSE dt.total_horas_mes_atual - pfr.horas_clt ";
            sSql = sSql + "                    END ";
            sSql = sSql + "               ELSE 0.0 ";
            sSql = sSql + "               END ";
            sSql = sSql + "FROM( ";
            sSql = sSql + "SELECT da.data_aula, isnull(sum(dap.hora_aula), 0.0) as hora_aula, dap.id_professor, count(*) as datas, da.id_oferecimento, cd.id_curso, c.nome, c.id_tipo_curso, o.quadrimestre, ";
            sSql = sSql + "total_horas_mes_anterior = (SELECT ISNULL(SUM(hora_aula), 0.0) ";
            sSql = sSql + "                          FROM datas_aulas_professor dapi ";
            sSql = sSql + "                          INNER JOIN datas_aulas dai ON dapi.id_aula = dai.id_aula ";
            sSql = sSql + "                          INNER JOIN oferecimentos oi ON dai.id_oferecimento = oi.id_oferecimento ";
            sSql = sSql + "                          INNER JOIN presenca_professor pp ON dapi.id_aula = pp.id_aula AND dapi.id_professor = pp.id_professor ";
            sSql = sSql + "                          WHERE oi.quadrimestre = o.quadrimestre AND dapi.id_professor = dap.id_professor AND pp.presente = 1 AND ";
            //sSql = sSql + "                                MONTH(dai.data_aula) <= (CASE WHEN YEAR(dai.data_aula) < YEAR(DATEADD(Month, -1, " + sAux_data + " )) THEN 12 ELSE MONTH(DATEADD(Month, -1, " + sAux_data + ")) END) ), ";
            sSql = sSql + "                                MONTH(dai.data_aula) <= (CASE WHEN YEAR(dai.data_aula) < " + sAux_data.AddMonths(-1).Year.ToString() + " THEN 12 ELSE " + sAux_data.AddMonths(-1).Month.ToString() + " END) ), ";
            sSql = sSql + "total_horas_mes_atual = (SELECT ISNULL(SUM(hora_aula), 0.0) ";
            sSql = sSql + "                       FROM datas_aulas_professor dapi ";
            sSql = sSql + "                       INNER JOIN datas_aulas dai ON dapi.id_aula = dai.id_aula ";
            sSql = sSql + "                       INNER JOIN oferecimentos oi ON dai.id_oferecimento = oi.id_oferecimento ";
            sSql = sSql + "                       INNER JOIN presenca_professor pp ON dapi.id_aula = pp.id_aula AND dapi.id_professor = pp.id_professor ";
            sSql = sSql + "                       WHERE oi.quadrimestre = o.quadrimestre AND dapi.id_professor = dap.id_professor AND pp.presente = 1 AND ";
            sSql = sSql + "                             MONTH(dai.data_aula) <= (CASE WHEN YEAR(dai.data_aula) < " + sAux_data.Year.ToString() + " THEN 12 ELSE " + sAux_data.Month.ToString() + " END) )  ";
            sSql = sSql + "FROM datas_aulas_professor dap ";
            sSql = sSql + "INNER JOIN datas_aulas da ON dap.id_aula = da.id_aula ";
            sSql = sSql + "INNER JOIN oferecimentos o ON da.id_oferecimento = o.id_oferecimento ";
            sSql = sSql + "INNER JOIN cursos_disciplinas cd ON o.id_disciplina = cd.id_disciplina ";
            sSql = sSql + "INNER JOIN cursos c ON cd.id_curso = c.id_curso ";
            sSql = sSql + "INNER JOIN presenca_professor pp ON dap.id_aula = pp.id_aula AND dap.id_professor = pp.id_professor ";
            sSql = sSql + "WHERE dap.id_professor = " + pItem.id_professorOld + " AND YEAR(da.data_aula) = " + sAux_data.Year.ToString() + " AND MONTH(da.data_aula) = " + sAux_data.Month.ToString() + " AND pp.presente = 1 ";
            sSql = sSql + "  GROUP BY c.nome, c.id_tipo_curso, da.data_aula, o.quadrimestre, cd.id_curso, dap.id_professor, da.id_oferecimento) as dt ";
            sSql = sSql + "INNER JOIN professores as p ON dt.id_professor = p.id_professor ";
            sSql = sSql + "INNER JOIN oferecimentos o ON dt.id_oferecimento = o.id_oferecimento and o.status <> 'inativado' ";
            sSql = sSql + "INNER JOIN disciplinas d ON o.id_disciplina = d.id_disciplina ";
            sSql = sSql + "LEFT JOIN professores_forma_recebimento pfr ON p.id_professor = pfr.id_professor ";
            sSql = sSql + "LEFT JOIN forma_recebimento as fr ON pfr.horas_aula = fr.id_forma_recebimento ";
            sSql = sSql + "LEFT JOIN titulacao as t ON p.id_titulo = t.id_titulacao ";
            sSql = sSql + "LEFT JOIN curso_valor_hora_aula cvha ON dt.id_curso = cvha.id_curso AND pfr.horas_aula = cvha.id_forma_recebimento AND p.id_titulo = cvha.id_titulacao ";
            sSql = sSql + "LEFT JOIN curso_valor_hora_aula cvhaa ON dt.id_curso = cvhaa.id_curso AND pfr.horas_aula_adicional = cvhaa.id_forma_recebimento AND p.id_titulo = cvhaa.id_titulacao ";
            sSql = sSql + "ORDER BY id_curso, id_oferecimento, data_aula, quadrimestre ";

            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            SqlDataReader retorno = cmd.ExecuteReader();

            int qIdOferecimento = 0;
            decimal qTotalHoras = 0;
            TimeSpan qTtimespan = TimeSpan.FromHours(0);
            //decimal qSubTotal = 0;

            item = new geral_horas_aulas_dadas();

            if (retorno.HasRows)
            {
                while (retorno.Read())
                {
                    if (qIdOferecimento == 0 || qIdOferecimento != Convert.ToInt32(retorno["id_oferecimento"]))
                    {
                        if (qIdOferecimento != 0)
                        {
                            item.total_hora_aula = qTotalHoras;
                            item.sub_total = qTotalHoras * item.valor_hora;
                            qTotalHoras = 0;
                            lista.Add(item);
                        }
                        item = new geral_horas_aulas_dadas();
                        item.data_aula = Convert.ToDateTime(retorno["data_aula"]);
                        item.hora_aula = Convert.ToDecimal(retorno["hora_aula"]);
                        item.id_oferecimento = Convert.ToInt32(retorno["id_oferecimento"]);
                        item.id_curso = Convert.ToInt32(retorno["id_curso"]);
                        item.nome_curso = retorno["nome"].ToString();
                        item.id_tipo_curso = Convert.ToInt32(retorno["id_tipo_curso"]);
                        item.quadrimestre = retorno["quadrimestre"].ToString();
                        item.codigo_disciplina = retorno["codigo"].ToString();
                        item.nome_disciplina = retorno["disciplina"].ToString();
                        item.valor_hora = Convert.ToDecimal(retorno["valor_hora"]);
                        qTtimespan = TimeSpan.FromHours(Convert.ToDouble(item.hora_aula));
                        int dd = qTtimespan.Days;
                        int hh = qTtimespan.Hours;
                        int mm = qTtimespan.Minutes;
                        int ss = qTtimespan.Seconds;
                        if (dd > 0 )
                        {
                            hh = hh + (dd * 24);
                        }
                        item.datas_aula = Convert.ToDateTime(retorno["data_aula"]).ToString("dd/MM/yyyy") + " (" + hh.ToString("00") + ":" + mm.ToString("00") + ")";
                        qTotalHoras = qTotalHoras + Convert.ToDecimal(retorno["hora_aula"]);
                    }
                    else
                    {
                        qTtimespan = TimeSpan.FromHours(Convert.ToDouble(retorno["hora_aula"]));
                        int dd = qTtimespan.Days;
                        int hh = qTtimespan.Hours;
                        int mm = qTtimespan.Minutes;
                        int ss = qTtimespan.Seconds;
                        if (dd > 0)
                        {
                            hh = hh + (dd * 24);
                        }
                        item.datas_aula = item.datas_aula + "<br>" + Convert.ToDateTime(retorno["data_aula"]).ToString("dd/MM/yyyy") + " (" + hh.ToString("00") + ":" + mm.ToString("00") + ")";
                        qTotalHoras = qTotalHoras + Convert.ToDecimal(retorno["hora_aula"]);
                    }
                    qIdOferecimento = Convert.ToInt32(retorno["id_oferecimento"]);
                }

                item.total_hora_aula = qTotalHoras;
                item.sub_total = qTotalHoras * item.valor_hora;
                qTotalHoras = 0;
                lista.Add(item);
            }
            return lista;
        }

        public List<geral_orientacoes_dadas> ListaOrientacoesDadas(professor_solicitacao_pagamento pItem)
        {
            //string sAux_data = "'" + pItem.data_solicitacao.ToString().Replace("/", "-") + "'";
            DateTime sAux_data = pItem.data_solicitacao;

            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string strConnString;
            string sSql;

            List<geral_orientacoes_dadas> lista = new List<geral_orientacoes_dadas>();
            geral_orientacoes_dadas item;

            strConnString = ConfigurationManager.ConnectionStrings["SisProConnectionString"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "";
            sSql = sSql + "SELECT * FROM (  ";
            sSql = sSql + "SELECT c.nome, c.id_tipo_curso, bp.id_professor, b.tipo_banca, tr.id_curso, b.horario, p.nome as professor, a.nome as aluno,  ";
            sSql = sSql + "       fr.id_forma_recebimento as recebe_como, p.id_titulo, t.nome as titulo_academico, t.reduzido as titulo_reduzido, ";
            sSql = sSql + "       fr.nome as forma_recebimento, b.resultado, valor_hora = (CASE WHEN b.tipo_banca = 'defesa' THEN cvo.valor_defesa ELSE cvo.valor_qualificacao END),  ";
            sSql = sSql + "       b.data_entrega_trabalho, data_calculo = " + sAux_data.ToString("dd/MM/yyyy") + ", b.id_banca ";
            sSql = sSql + "  FROM banca_professores bp ";
            sSql = sSql + "INNER JOIN banca b ON bp.id_banca = b.id_banca ";
            sSql = sSql + "INNER JOIN matricula_turma mt ON b.id_matricula_turma = mt.id_matricula_turma ";
            sSql = sSql + "INNER JOIN turmas tr ON mt.id_turma = tr.id_turma ";
            sSql = sSql + "INNER JOIN cursos c ON c.id_curso = tr.id_curso ";
            sSql = sSql + "INNER JOIN alunos a ON mt.id_aluno = a.idaluno ";
            sSql = sSql + "INNER JOIN professores p ON bp.id_professor = p.id_professor ";
            sSql = sSql + "LEFT JOIN professores_forma_recebimento pfr ON p.id_professor = pfr.id_professor ";
            sSql = sSql + "LEFT JOIN forma_recebimento as fr ON pfr.orientacao = fr.id_forma_recebimento ";
            sSql = sSql + "LEFT JOIN titulacao as t ON p.id_titulo = t.id_titulacao ";
            sSql = sSql + "LEFT JOIN curso_valor_orientacao cvo ON cvo.id_curso = tr.id_curso AND fr.id_forma_recebimento = cvo.id_forma_recebimento ";
            //== Tirado o Co-orientador segundo email do prof Eduardo em 08/04/2021 as 10:42
            //sSql = sSql + "WHERE(bp.tipo_professor = 'orientador' OR bp.tipo_professor = 'co-orientador') AND isNull(pfr.orientacao, 1)> 1 AND bp.id_professor = " + pItem.id_professorOld + ") derivedTbl ";
            sSql = sSql + "WHERE(bp.tipo_professor = 'orientador') AND isNull(pfr.orientacao, 1)> 1 AND bp.id_professor = " + pItem.id_professorOld + ") derivedTbl ";
            sSql = sSql + "WHERE(CASE tipo_banca WHEN 'Defesa' ";
            sSql = sSql + "      THEN CASE WHEN(YEAR(data_entrega_trabalho) = " + sAux_data.Year.ToString() + " AND MONTH(data_entrega_trabalho) = " + sAux_data.Month.ToString() + ") THEN 1 ELSE 0 END ";
            sSql = sSql + "      ELSE CASE WHEN(YEAR(horario) = " + sAux_data.Year.ToString() + " AND MONTH(horario) = " + sAux_data.Month.ToString() + ") THEN 1 ELSE 0 END END) = 1 ";
            sSql = sSql + "ORDER BY id_curso, horario ";

            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            SqlDataReader retorno = cmd.ExecuteReader();

            item = new geral_orientacoes_dadas();

            if (retorno.HasRows)
            {
                while (retorno.Read())
                {
                    
                    item = new geral_orientacoes_dadas();
                    item.nome_curso = retorno["nome"].ToString();
                    item.id_tipo_curso = Convert.ToInt32(retorno["id_tipo_curso"]);
                    item.tipo_banca = retorno["tipo_banca"].ToString();
                    item.id_curso = Convert.ToInt32(retorno["id_curso"]);
                    item.data_orientacao = Convert.ToDateTime(retorno["horario"].ToString());
                    item.orientacao = item.tipo_banca + " " + item.data_orientacao.ToString("dd/MM/yyyy HH:mm") + " - " + retorno["aluno"].ToString(); 
                    item.sub_total = Convert.ToDecimal(retorno["valor_hora"]);
                    lista.Add(item);
                }
            }
            return lista;
        }

        public List<geral_custo_coordenacao> ListaCoordenacaoDadas(geral_solicitado_professor pItem)
        {
            //string sAux_data = "'" + pItem.data_solicitacao.ToString().Replace("/", "-") + "'";
            //DateTime sAux_data = pItem.data_solicitacao;

            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            List<geral_custo_coordenacao> lista = new List<geral_custo_coordenacao>();
            geral_custo_coordenacao item;

            var s_Id_Solicitacao = contextoEF.professor_solicitacao_pagamento.Where(x => x.status == "Solicitado").Select(x => x.id_solicitacao).ToArray();
            var s_Id_Plano = contextoEF.professor_plano.Where(x => x.id_professor == pItem.id_professor && x.motivo == "Coordenação" && x.mes == pItem.mes_plano).Select(x => x.id_plano).ToArray();

            List<professor_solicitacao_plano> lista_sp;

            lista_sp = contextoEF.professor_solicitacao_plano.Where(x => s_Id_Plano.Contains(x.id_plano) && s_Id_Solicitacao.Contains(x.id_solicitacao)).ToList();

            foreach (var elemento in lista_sp)
            {
                List<geral_custo_coordenacao> lista_custo_coordenacao;
                lista_custo_coordenacao = ListaCustoCoordenacao("", pItem.id_professor, elemento.professor_plano.mes).ToList();

                foreach (var elemento2 in lista_custo_coordenacao.Where(x=> x.col_Curso != null))
                {
                    item = new geral_custo_coordenacao();
                    item.col_Curso = elemento2.col_Curso;
                    item.col_Id_Curso = elemento2.col_Id_Curso;
                    item.col_Id_TipoCurso = elemento2.col_Id_TipoCurso;
                    item.col_Professor = elemento2.col_Professor;
                    item.col_TipoCoordenacao = elemento2.col_TipoCoordenacao;
                    item.col_Turma = elemento2.col_Turma;
                    item.col_MesReferencia = elemento2.col_MesReferencia;
                    item.col_Total = elemento2.col_Total;
                    lista.Add(item);
                }
            }
               
            return lista;
        }

        ////Botão Preparar Email - Fim

        ////Botão Recebimento Nota Fiscal - Início

        public List<geral_Solicitacao> ListaSolicitaoEfetuada(int idProfessor, bool bSolicitado)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string strConnString;
            string sSql;
            DateTime dData;

            List<geral_Solicitacao> lista = new List<geral_Solicitacao>();
            geral_Solicitacao item = new geral_Solicitacao();

            strConnString = ConfigurationManager.ConnectionStrings["SisProConnectionString"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "";
            sSql = sSql + "SELECT pp.id_professor, pspg.data_solicitacao, pp.mes, pp.motivo, pp.valor, psp.valor_solicitado, pspg.valor valor_total_solicitado,  ";
            sSql = sSql + "pp.data_cadastro, pp.data_alteracao, pp.usuario, p.nome AS professor, pp.id_plano, psp.id_solicitacao, ";
            sSql = sSql + "pspg.data_pagamento, pspg.data_recebimento, pspg.nota_fiscal, pspg.status ";
            sSql = sSql + "FROM professor_plano pp ";
            sSql = sSql + "INNER JOIN professores p ON pp.id_professor = p.id_professor ";
            sSql = sSql + "INNER JOIN professor_solicitacao_plano AS psp ON pp.id_plano = psp.id_plano ";
            sSql = sSql + "INNER JOIN professor_solicitacao_pagamento as pspg ON psp.id_solicitacao = pspg.id_solicitacao ";
            sSql = sSql + "WHERE pp.id_professor = '" + idProfessor + "' ";
            if (bSolicitado)
            {
                sSql = sSql + "and pspg.status = 'Solicitado' ";
            }
            sSql = sSql + "ORDER BY pp.id_professor, pspg.data_solicitacao, pp.mes ";

            if (cCon.State.ToString() != "Open")
            {
                cCon.Open();
            }
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            SqlDataReader retorno = cmd.ExecuteReader();

            if (retorno.HasRows)
            {
                while (retorno.Read())
                {
                    item = new geral_Solicitacao();
                    item.id_professor = Convert.ToInt32(retorno["id_professor"]);
                    item.data_solicitacao = Convert.ToDateTime(retorno["data_solicitacao"]);
                    dData = Convert.ToDateTime(retorno["mes"]);
                    item.mes = Convert.ToDateTime(retorno["mes"]);
                    item.mes_string = dData.Month.ToString("00") + "/" + dData.Year.ToString();
                    item.motivo = retorno["motivo"].ToString();
                    item.valor = Convert.ToDecimal(retorno["valor"]);
                    item.valor_solicitado = Convert.ToDecimal(retorno["valor_solicitado"]);
                    item.valor_total_solicitado = Convert.ToDecimal(retorno["valor_total_solicitado"]);
                    item.id_plano = Convert.ToInt32(retorno["id_plano"]);
                    item.id_solicitacao = Convert.ToInt32(retorno["id_solicitacao"]);
                    item.status = retorno["status"].ToString();
                    if (item.status != "Solicitado")
                    {
                        item.data_pagamento = Convert.ToDateTime(retorno["data_pagamento"]);
                        item.data_recebimento = Convert.ToDateTime(retorno["data_recebimento"]);
                        item.nota_fiscal = retorno["nota_fiscal"].ToString();
                    }
                    
                    lista.Add(item);
                }
            }

            cCon.Close();

            return lista;
        }

        public professor_solicitacao_pagamento AlteraSolicitacao(professor_solicitacao_pagamento pItem)
        {
            professor_solicitacao_pagamento item;

            item = contextoEF.professor_solicitacao_pagamento.Where(x => x.id_solicitacao == pItem.id_solicitacao).SingleOrDefault();

            if (item != null)
            {
                item.status = "Pago";
                item.nota_fiscal = pItem.nota_fiscal;
                item.data_recebimento = pItem.data_recebimento;
                item.data_pagamento = pItem.data_pagamento;
                item.data_alteracao = DateTime.Now;
                item.usuario = pItem.usuario;
                contextoEF.SaveChanges();
                return item;
            }
            else
            {
                return item;
            }   
            
        }

        public string ExcluirSolicitacao(int pItem)
        {
            professor_solicitacao_pagamento item;

            item = contextoEF.professor_solicitacao_pagamento.Where(x => x.id_solicitacao == pItem).SingleOrDefault();

            if (item != null)
            {
                contextoEF.professor_solicitacao_plano.RemoveRange(contextoEF.professor_solicitacao_plano.Where(x => x.id_solicitacao == pItem));
                contextoEF.professor_solicitacao_pagamento.Remove(item);
                contextoEF.SaveChanges();
                return "ok";
            }
            else
            {
                return "nok";
            }

        }

        ////Botão Recebimento Nota Fiscal - Fim

        //Utilizado na tela de Solicitação Pagto Professor (finSolicitacaoPagtoProfessor) Fim


        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}
    