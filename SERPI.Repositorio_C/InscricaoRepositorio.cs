using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Repositorio_C
{
    public class InscricaoRepositorio : IDisposable
    {
        private Entities contextoEF;
        //private EntitiesHomolog contextoEF_Homolog;

        public InscricaoRepositorio()
        {
            contextoEF = new Entities();
            //contextoEF_Homolog = new EntitiesHomolog();
        }

        public fichas_inscricao BuscaItem_Inscricao(fichas_inscricao pItem)
        {
            fichas_inscricao item = new fichas_inscricao();
            item = contextoEF.fichas_inscricao.Where(x => x.id_inscricao == pItem.id_inscricao).SingleOrDefault();
            return item;
        }

        public periodo_inscricao BuscaItem_periodo_inscricao(periodo_inscricao pItem)
        {
            periodo_inscricao item = new periodo_inscricao();
            item = contextoEF.periodo_inscricao.Where(x => x.id_periodo == pItem.id_periodo).SingleOrDefault();
            return item;
        }

        public periodo_inscricao_curso BuscaItem_periodo_inscricao_curso(periodo_inscricao_curso pItem)
        {
            periodo_inscricao_curso item = new periodo_inscricao_curso();
            item = contextoEF.periodo_inscricao_curso.Include(x=> x.periodo_inscricao).Include(x => x.cursos).Where(x => x.id_periodo == pItem.id_periodo && x.id_curso == pItem.id_curso).SingleOrDefault();
            return item;
        }

        //public periodo_inscricao_curso BuscaItem_periodo_inscricao_curso_Phorte(periodo_inscricao_curso pItem)
        //{
        //    periodo_inscricao_curso item = new periodo_inscricao_curso();
        //    item = contextoEF_Homolog.periodo_inscricao_curso.Include(x => x.periodo_inscricao).Include(x => x.cursos).Where(x => x.id_periodo == pItem.id_periodo && x.id_curso == pItem.id_curso).SingleOrDefault();
        //    return item;
        //}

        public periodo_inscricao CriarPeriodoInscricao(periodo_inscricao pItem)
        {
            contextoEF.periodo_inscricao.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public bool ExcluirPeriodoInscricao(periodo_inscricao pItem)
        {
            periodo_inscricao item = new periodo_inscricao();
            item = contextoEF.periodo_inscricao.Where(x => x.id_periodo == pItem.id_periodo).SingleOrDefault();
            contextoEF.periodo_inscricao.Remove(item);
            contextoEF.SaveChanges();
            return true;
        }

        public List<cursos> ListaCursosDisponiveis(periodo_inscricao pItem, cursos pItemCurso)
        {
            var c = contextoEF.cursos.AsQueryable();
            List<cursos> lista = new List<cursos>();

            var qIdCurso = contextoEF.periodo_inscricao_curso.Where(x => x.id_periodo == pItem.id_periodo).Select(x=> x.id_curso).ToArray();

            c = c.Where(x => !qIdCurso.Contains(x.id_curso) && x.status != "inativado");

            lista = c.OrderBy(x => x.nome).ToList();

            return lista;
        }

        public periodo_inscricao_curso CriarCursoPeriodoInscricao(periodo_inscricao_curso pItem)
        {
            contextoEF.periodo_inscricao_curso.Add(pItem);
            contextoEF.SaveChanges();
            cursos item = contextoEF.cursos.Where(x => x.id_curso == pItem.id_curso).SingleOrDefault();
            pItem.cursos = item;
            return pItem;
        }

        public bool AlterarCursoPeriodoInscricao(periodo_inscricao_curso pItem)
        {
            periodo_inscricao_curso item = new periodo_inscricao_curso();
            item = contextoEF.periodo_inscricao_curso.Where(x => x.id_periodo == pItem.id_periodo && x.id_curso == pItem.id_curso).SingleOrDefault();
            item.valor = pItem.valor;
            contextoEF.SaveChanges();
            return true;
        }

        public bool ExcluirCursoPeriodoInscricao(periodo_inscricao_curso pItem)
        {
            periodo_inscricao_curso item = new periodo_inscricao_curso();
            item = contextoEF.periodo_inscricao_curso.Where(x => x.id_periodo == pItem.id_periodo && x.id_curso == pItem.id_curso).SingleOrDefault();
            contextoEF.periodo_inscricao_curso.Remove(item);
            contextoEF.SaveChanges();
            return true;
        }

        public fichas_inscricao CriarInscricao(fichas_inscricao pItem)
        {
            contextoEF.fichas_inscricao.Add(pItem);
            contextoEF.SaveChanges();
            pItem.cursos = contextoEF.cursos.Where(x => x.id_curso == pItem.id_curso).FirstOrDefault();
            return pItem;
        }

        //public fichas_inscricao CriarInscricao_Phorte(fichas_inscricao pItem)
        //{
        //    contextoEF_Homolog.fichas_inscricao.Add(pItem);
        //    contextoEF_Homolog.SaveChanges();
        //    return pItem;
        //}

        public historico_inscricao CriarHistorico(historico_inscricao pItem)
        {
            historico_inscricao pItemNew = null;

            if (pItem.status == "Matriculado")
            {
                pItemNew = contextoEF.historico_inscricao.Where(x => x.id_inscricao == pItem.id_inscricao && x.status == "Matriculado").SingleOrDefault();
            }

            if (pItemNew == null)
            {
                contextoEF.historico_inscricao.Add(pItem);
                contextoEF.SaveChanges();
            }
            else
            {
                pItem = pItemNew;
            }
            
            return pItem;
        }

        //public historico_inscricao CriarHistorico_Phorte(historico_inscricao pItem)
        //{
        //    contextoEF_Homolog.historico_inscricao.Add(pItem);
        //    contextoEF_Homolog.SaveChanges();
        //    return pItem;
        //}

        public boletos CriarBoleto(boletos pItem, fichas_inscricao pItem_Inscricao)
        {
            contextoEF.boletos.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        //public boletos CriarBoleto_Phorte(boletos pItem, fichas_inscricao pItem_Inscricao)
        //{
        //    contextoEF_Homolog.boletos.Add(pItem);
        //    contextoEF_Homolog.SaveChanges();
        //    return pItem;
        //}

        public bool Criar_inscricao_boleto(boletos pItem, fichas_inscricao pItem_Inscricao)
        {
            contextoEF.Database.ExecuteSqlCommand("INSERT INTO inscricao_boleto (id_inscricao, id_boleto) VALUES (" + pItem_Inscricao.id_inscricao + "," + pItem.id_boleto + ")");
            return true;
        }

        //public bool Criar_inscricao_boleto_Phorte(boletos pItem, fichas_inscricao pItem_Inscricao)
        //{
        //    contextoEF_Homolog.Database.ExecuteSqlCommand("INSERT INTO inscricao_boleto (id_inscricao, id_boleto) VALUES (" + pItem_Inscricao.id_inscricao + "," + pItem.id_boleto + ")");
        //    return true;
        //}

        public boletos AlterarBoleto(boletos pItem)
        {
            boletos item = new boletos();
            item = contextoEF.boletos.Include(x => x.fichas_inscricao).Where(x => x.id_boleto == pItem.id_boleto).SingleOrDefault();
            //item.refTran = pItem.refTran;
            item.status = pItem.status;
            contextoEF.SaveChanges();
            return item;
        }

        public Boolean AlterarInscricao(fichas_inscricao pItem)
        {
            fichas_inscricao item = new fichas_inscricao();
            item = contextoEF.fichas_inscricao.Include(x => x.boletos).Include(x => x.cursos).Include(x => x.periodo_inscricao).Include(x => x.historico_inscricao).Where(x => x.id_inscricao == pItem.id_inscricao).SingleOrDefault();

            item.nome = pItem.nome;

            contextoEF.SaveChanges();
            return true;
        }

        public Boolean AlterarPeriodoInscricao(periodo_inscricao pItem)
        {
            periodo_inscricao item = new periodo_inscricao();
            item = contextoEF.periodo_inscricao.Where(x => x.id_periodo == pItem.id_periodo).SingleOrDefault();

            item.quadrimestre = pItem.quadrimestre;
            item.data_inicio = pItem.data_inicio;
            item.data_fim = pItem.data_fim;
            item.data_limite_pagamento = pItem.data_limite_pagamento;
            item.data_prova = pItem.data_prova;

            contextoEF.SaveChanges();
            return true;
        }

        public List<fichas_inscricao> ListaInscricao(fichas_inscricao pItem, DateTime qDataFim, string qUsuario)
        {
            var c = contextoEF.fichas_inscricao.AsQueryable();
            List<fichas_inscricao> lista = new List<fichas_inscricao>();

            DateTime dtDefault = new DateTime();

            if (pItem.id_inscricao != 0)
            {
                c = c.Where(x => x.id_inscricao == pItem.id_inscricao);
            }

            if (pItem.id_periodo_inscricao != null)
            {
                c = c.Where(x => x.id_periodo_inscricao == pItem.id_periodo_inscricao);
            }

            if (pItem.nome != null)
            {
                c = c.Where(x => x.nome.Contains(pItem.nome));
            }

            if (pItem.id_curso != 0)
            {
                c = c.Where(x => x.id_curso == pItem.id_curso);
            }

            if (pItem.cpf != null)
            {
                c = c.Where(x => x.cpf == pItem.cpf);
            }

            if (qUsuario != "")
            {
                if (qUsuario == "PHORTE")
                {
                    c = c.Where(x => x.boletos.Any(y => y.usuario == "PHORTE"));
                }
                else
                {
                    c = c.Where(x => x.boletos.Any(y => y.usuario != "PHORTE"));
                }
                
            }

            if (pItem.data_inscricao != dtDefault)
            {
                c = c.Where(x => x.data_inscricao >=  pItem.data_inscricao);
            }

            if (qDataFim != dtDefault)
            {
                c = c.Where(x => x.data_inscricao <= qDataFim);
            }

            lista = c.Include(x => x.cursos).OrderBy(x => x.nome).ToList();

            return lista;
        }

        public List<periodo_inscricao> ListaPeriodoInscricao(periodo_inscricao pItem)
        {
            var c = contextoEF.periodo_inscricao.AsQueryable();
            List<periodo_inscricao> lista = new List<periodo_inscricao>();

            c = c.Where(x => x.data_inicio <= pItem.data_inicio && x.data_fim >= pItem.data_fim);

            lista = c.Include(x => x.fichas_inscricao).Include(x => x.periodo_inscricao_curso).Include(x => x.periodo_inscricao_curso).OrderBy(x => x.data_inicio).ToList();

            return lista;
        }

        public List<periodo_inscricao_curso> ListaPeriodoInscricaoCurso(periodo_inscricao_curso pItem)
        {
            var c = contextoEF.periodo_inscricao_curso.AsQueryable();
            List<periodo_inscricao_curso> lista = new List<periodo_inscricao_curso>();
            if (pItem.id_periodo != 0)
            {
                c = c.Where(x => x.id_periodo == pItem.id_periodo);
            }
            if (pItem.id_curso != 0)
            {
                c = c.Where(x => x.id_curso == pItem.id_curso);
            }

            lista = c.Include(x => x.fichas_inscricao).Include(x => x.fichas_inscricao.Select(y=>y.historico_inscricao)).Include(x => x.periodo_inscricao).Include(x => x.cursos).OrderBy(x => x.periodo_inscricao.data_inicio).ToList();

            return lista;
        }

        public List<periodo_inscricao> ListaPeriodoInscricaoAdmin(periodo_inscricao pItem)
        {
            var c = contextoEF.periodo_inscricao.AsQueryable();
            List<periodo_inscricao> lista = new List<periodo_inscricao>();

            DateTime dDataDefault = new DateTime();


            if (pItem.quadrimestre != "" && pItem.quadrimestre != null)
            {
                c = c.Where(x => x.quadrimestre.Contains(pItem.quadrimestre));
            }

            if (pItem.data_inicio != dDataDefault && pItem.data_inicio != null)
            {
                c = c.Where(x => x.data_inicio >= pItem.data_inicio);
            }

            if (pItem.data_fim != dDataDefault && pItem.data_fim != null)
            {
                c = c.Where(x => x.data_fim <= pItem.data_fim);
            }
            
            lista = c.Include(x => x.fichas_inscricao).Include(x => x.periodo_inscricao_curso).Include(x => x.periodo_inscricao_curso).OrderBy(x => x.data_inicio).ToList();

            return lista;
        }

        public List<periodo_inscricao> ListaPeriodoInscricao(int[] qIdCurso)
        {
            var c = contextoEF.periodo_inscricao.AsQueryable();
            List<periodo_inscricao> lista = new List<periodo_inscricao>();

            if (qIdCurso[0] != 0)
            {
                var qIdPeriodo = contextoEF.periodo_inscricao_curso.Where(x => qIdCurso.Contains(x.id_curso)).Select(x => x.id_periodo).ToArray();

                c = c.Where(x => qIdPeriodo.Contains(x.id_periodo));

                //foreach (var elemento in qIdCurso)
                //{
                //    qIdPeriodo = contextoEF.periodo_inscricao_curso.Where(x => x.id_curso == elemento).Select(x => x.id_periodo).ToArray();
                //    qIdPeriodo = contextoEF.periodo_inscricao_curso.Where(x => qIdCurso.Contains(x.id_curso)).Select(x => x.id_periodo).ToArray();

                //    c = c.Where(x => qIdPeriodo.Contains(x.id_periodo));
                //}
            }

            //lista = c.Include(x => x.fichas_inscricao).Include(x => x.periodo_inscricao_curso).Include(x => x.periodo_inscricao_curso).OrderByDescending(x => x.data_inicio).ToList();

            lista = c.OrderByDescending(x => x.data_inicio).ToList();
            return lista;
        }

        public List<cursos> ListaCursoPeriodo(periodo_inscricao pItem, int[] qIdCurso)
        {
            var c = contextoEF.cursos.AsQueryable();
            List<cursos> lista = new List<cursos>();
            //if (qIdCurso[0] != 0)
            //{

            //    var qIdPeriodo = contextoEF.periodo_inscricao_curso.Where(x => qIdCurso.Contains(x.id_curso)).Select(x => x.id_periodo).ToArray();
            //    var idCurso = contextoEF.periodo_inscricao_curso.Where(x => x.id_periodo == pItem.id_periodo && qIdPeriodo.Contains(x.id_periodo)).Select(x => x.id_curso).ToArray();

            //    c = c.Where(x => qIdCurso.Contains(x.id_curso));
            //}
            //else
            //{
                var idCurso = contextoEF.periodo_inscricao_curso.Where(x => x.id_periodo == pItem.id_periodo).Select(x => x.id_curso).ToArray();
                c = c.Where(x => idCurso.Contains(x.id_curso));
            //}
            
            //lista = c.Include(x => x.fichas_inscricao).Include(x => x.periodo_inscricao_curso).Include(x => x.periodo_inscricao_curso).ToList();
            lista = c.ToList();
            return lista;
        }

        //public List<periodo_inscricao> ListaPeriodoInscricao_Phorte(periodo_inscricao pItem)
        //{
        //    var c = contextoEF_Homolog.periodo_inscricao.AsQueryable();
        //    List<periodo_inscricao> lista = new List<periodo_inscricao>();

        //    c = c.Where(x => x.data_inicio <= pItem.data_inicio && x.data_fim >= pItem.data_fim);

        //    lista = c.Include(x => x.fichas_inscricao).Include(x => x.periodo_inscricao_curso).Include(x => x.periodo_inscricao_curso).OrderBy(x => x.data_inicio).ToList();

        //    return lista;
        //}

        public bool Criar_refTran(refTran pItem)
        {
            contextoEF.refTran.Add(pItem);
            contextoEF.SaveChanges();
            return true;
        }

        //public bool Criar_refTran_Phorte(refTran pItem)
        //{
        //    contextoEF_Homolog.refTran.Add(pItem);
        //    contextoEF_Homolog.SaveChanges();
        //    return true;
        //}

        //public refTran Busca_refTran_Phorte(refTran pItem)
        //{
        //    refTran item = new refTran();
        //    item = contextoEF_Homolog.refTran.Include(x=> x.boletos).Where(x => x.id_refTran == pItem.id_refTran).SingleOrDefault();
        //    return item;
        //}

        //public refTran Altera_refTran_Phorte(refTran pItem)
        //{
        //    refTran item = new refTran();
        //    item = contextoEF_Homolog.refTran.Where(x => x.id_refTran == pItem.id_refTran).SingleOrDefault();
        //    item.DataGetGemini = pItem.DataGetGemini;
        //    item.DataUtilizacao = pItem.DataUtilizacao;
        //    item.id_boleto = pItem.id_boleto;
        //    contextoEF_Homolog.SaveChanges();
        //    return item;
        //}

        public string Busca_Ultimo_refTran()
        {

            string strConnString;
            string sSql;
            string sNossoNumero ="";
            strConnString  = ConfigurationManager.ConnectionStrings["FIPTEntities"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "select * from TBTipoBoleto where IDBoleto = 256";

            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            cmd.ExecuteNonQuery();

            //sda.SelectCommand = cmd;
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //if ((dt.Rows.Count > 0))
            //{
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        sNossoNumero = (Convert.ToDecimal(row["NossoNumero"].ToString()) + 1).ToString();
            //    }
            //}

            SqlDataReader reader = cmd.ExecuteReader();
            DataTable datatable = new DataTable();
            datatable.Load(reader);
            if ((datatable.Rows.Count > 0))
            {
                foreach (DataRow row in datatable.Rows)
                {
                    sNossoNumero = (Convert.ToDecimal(row["NossoNumero"].ToString()) + 1).ToString();
                }
            }

            sSql = "update TBTipoBoleto set NossoNumero = '" + sNossoNumero + "'  where IDBoleto = 256";
            cmd = new SqlCommand(sSql, cCon);
            var retorno = cmd.ExecuteNonQuery();
            cCon.Close();

            return sNossoNumero;
        }

        public string Busca_Ultimo_refTran_Phorte()
        {
            string strConnString;
            string sSql;
            string sNossoNumero = "";
            strConnString = ConfigurationManager.ConnectionStrings["FIPTEntitiesTeste"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "select * from TBTipoBoleto where IDBoleto = 256";

            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            cmd.ExecuteNonQuery();

            //sda.SelectCommand = cmd;
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //if ((dt.Rows.Count > 0))
            //{
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        sNossoNumero = (Convert.ToDecimal(row["NossoNumero"].ToString()) + 1).ToString();
            //    }
            //}

            SqlDataReader reader = cmd.ExecuteReader();
            DataTable datatable = new DataTable();
            datatable.Load(reader);
            if ((datatable.Rows.Count > 0))
            {
                foreach (DataRow row in datatable.Rows)
                {
                    sNossoNumero = (Convert.ToDecimal(row["NossoNumero"].ToString()) + 1).ToString();
                }
            }

            sSql = "update TBTipoBoleto set NossoNumero = '" + sNossoNumero + "'  where IDBoleto = 256";
            cmd = new SqlCommand(sSql, cCon);
            var retorno = cmd.ExecuteNonQuery();
            cCon.Close();

            return sNossoNumero;
        }

        public Boolean Insere_Gemini(string CPF, string Nome, int Sexo, DateTime DataNascimento, string CEP, string Endereco, string Numero, string Complemento, string Bairro, string Cidade, string UF, string Email, string Valor, string NossoNumero, int qIdTipoCurso, int qIdCurso, string qNomeCurso, DateTime DataVencimento)
        {

            string strConnString;
            string sSql;

            strConnString = ConfigurationManager.ConnectionStrings["FIPTEntities"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "Insert into TBEducacional (CPF,Nome,Sexo,DataNascimento,CEP,Endereco,Numero,Complemento,Bairro,Cidade,UF,Email,Valor,NossoNumero,id_tipo_curso,id_curso,nome_curso,DataVencimento) Values (";
            sSql = sSql + "'" + CPF + "',";
            sSql = sSql + "'" + Nome.Replace("'", "''") + "',";
            sSql = sSql + "" + Sexo + ",";
            //round(cast(dateadd(DAY, 2, cast('2019/01/11' as datetime)) as float), 0, 1)
            sSql = sSql + "(select round(cast(dateadd(DAY, 2, cast('" + String.Format("{0:yyyy/MM/dd}", DataNascimento) + "' as datetime)) as float),0,1)),";
            sSql = sSql + "'" + CEP + "',";
            sSql = sSql + "'" + Endereco.Replace("'","''") + "',";
            sSql = sSql + "'" + Numero + "',";
            sSql = sSql + "'" + Complemento + "',";
            sSql = sSql + "'" + Bairro.Replace("'", "''") + "',";
            sSql = sSql + "'" + Cidade.Replace("'", "''") + "',";
            sSql = sSql + "'" + UF + "',";
            sSql = sSql + "'" + Email + "',";
            sSql = sSql + "'" + Valor.Replace(".","").Replace(",",".") + "',";
            sSql = sSql + "'" + NossoNumero + "',";
            sSql = sSql + "" + qIdTipoCurso + ",";
            sSql = sSql + "" + qIdCurso + ",";
            sSql = sSql + "'" + qNomeCurso + "',";
            sSql = sSql + "" + "(Select round(cast(dateadd(DAY, 2, cast('" + String.Format("{0:yyyy/MM/dd}", DataVencimento) + "' as datetime)) as float),0,1)))";

            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            var retorno = cmd.ExecuteNonQuery();
            cCon.Close();

            return true;
        }

        public Boolean Altera_NossoNumeto_Boleto_Gemini(int IDLancamento, string NossoNumero_IPT, string Complemento_IPT)
        {

            string strConnString;
            string sSql;

            strConnString = ConfigurationManager.ConnectionStrings["FIPTEntities"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "update TBLancamento set NossoNumero_IPT='" + NossoNumero_IPT + "', Complemento_IPT='" + Complemento_IPT + "'";
            sSql = sSql + " where IDLancamento=" + IDLancamento.ToString();

            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            var retorno = cmd.ExecuteNonQuery();
            cCon.Close();

            return true;
        }

        public Tuple<string,DateTime> VarificaBoletoPago_Gemini(string NossoNumero)
        {

            string strConnString;
            string sSql;

            strConnString = ConfigurationManager.ConnectionStrings["FIPTEntities"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "select DATEADD(DAY,-2,cast(t.data as datetime)) dataPagto , ISNULL(DATEADD(DAY,-2,cast(t.DataCancelamento as datetime)),0) dataCancela ";
            sSql = sSql + " from TBLancamento t";
            sSql = sSql + " where t.NossoNumero = '" + NossoNumero + "'";
            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            SqlDataReader retorno = cmd.ExecuteReader();

            string sRetorno = "SemRegistro";
            DateTime dRetorno = Convert.ToDateTime("01/01/1900");

            while (retorno.Read())
            {
                if (Convert.ToDateTime(retorno["dataCancela"]) == dRetorno)
                {
                    dRetorno = Convert.ToDateTime(retorno["dataPagto"]);
                    if (dRetorno.Year > 2000)
                    {
                        sRetorno = "Pago";
                    }
                    else
                    {
                        sRetorno = "NaoPago";
                    }
                }
                else
                {
                    sRetorno = "Cancelado";
                    dRetorno = Convert.ToDateTime(retorno["dataCancela"]);
                }
            }

            if (sRetorno == "SemRegistro")
            {
                cCon.Close();

                cCon = new SqlConnection(strConnString);
                cmd = new SqlCommand();
                sda = new SqlDataAdapter();
                sSql = "select * ";
                sSql = sSql + " from TBEducacional t";
                sSql = sSql + " where t.NossoNumero = '" + NossoNumero + "'";
                cCon.Open();
                cmd.Connection = cCon;
                cmd.CommandText = sSql;
                retorno = cmd.ExecuteReader();

                while (retorno.Read())
                {
                    sRetorno = "NaoPago";
                }
            }
            cCon.Close();
            return Tuple.Create(sRetorno, dRetorno);
        }

        public Boolean Insere_Gemini_Phorte(string CPF, string Nome, int Sexo, DateTime DataNascimento, string CEP, string Endereco, string Numero, string Complemento, string Bairro, string Cidade, string UF, string Email, string Valor, string NossoNumero, int qIdTipoCurso, int qIdCurso, string qNomeCurso, DateTime DataVencimento)
        {

            string strConnString;
            string sSql;

            strConnString = ConfigurationManager.ConnectionStrings["FIPTEntitiesTeste"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "Insert into TBEducacional (CPF,Nome,Sexo,DataNascimento,CEP,Endereco,Numero,Complemento,Bairro,Cidade,UF,Email,Valor,NossoNumero,DataVencimento) Values (";
            sSql = sSql + "'" + CPF + "',";
            sSql = sSql + "'" + Nome + "',";
            sSql = sSql + "" + Sexo + ",";
            //round(cast(dateadd(DAY, 2, cast('2019/01/11' as datetime)) as float), 0, 1)
            sSql = sSql + "(select round(cast(dateadd(DAY, 2, cast('" + String.Format("{0:yyyy/MM/dd}", DataNascimento) + "' as datetime)) as float),0,1)),";
            sSql = sSql + "'" + CEP + "',";
            sSql = sSql + "'" + Endereco + "',";
            sSql = sSql + "'" + Numero + "',";
            sSql = sSql + "'" + Complemento + "',";
            sSql = sSql + "'" + Bairro + "',";
            sSql = sSql + "'" + Cidade + "',";
            sSql = sSql + "'" + UF + "',";
            sSql = sSql + "'" + Email + "',";
            sSql = sSql + "'" + Valor.Replace(".", "").Replace(",", ".") + "',";
            sSql = sSql + "'" + NossoNumero + "',";
            sSql = sSql + "" + qIdTipoCurso + ",";
            sSql = sSql + "" + qIdCurso + ",";
            sSql = sSql + "'" + qNomeCurso + "',";
            sSql = sSql + "" + "(Select round(cast(dateadd(DAY, 2, cast('" + String.Format("{0:yyyy/MM/dd}", DataVencimento) + "' as datetime)) as float),0,1)))";

            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            var retorno = cmd.ExecuteNonQuery();
            cCon.Close();

            return true;
        }

        public List<fichas_inscricao> ListaCandidatosBoletos(int qIdPeriodo, int qIdCurso)
        {
            var c = contextoEF.fichas_inscricao.AsQueryable();
            List<fichas_inscricao> lista = new List<fichas_inscricao>();

            if (qIdPeriodo != 0)
            {
                c = c.Where(x => x.periodo_inscricao.id_periodo == qIdPeriodo);
            }

            if (qIdCurso != 0)
            {
                c = c.Where(x => x.id_curso == qIdCurso);
            }

            lista = c.OrderBy(x => x.nome).ToList();

            return lista;
        }

        public bool ExcluiDataPagamentoBoleto(boletos pItem, int qIdInscricao)
        {
            boletos item;

            contextoEF.historico_inscricao.RemoveRange(contextoEF.historico_inscricao.Where(x => x.id_inscricao == qIdInscricao && x.status == "Inscrição Paga"));

            item = contextoEF.boletos.Where(x => x.id_boleto == pItem.id_boleto).SingleOrDefault();
            item.data_pagamento = null;
            contextoEF.SaveChanges();
            return true;
        }

        public bool IncluirDataPagamentoBoleto(boletos pItem_boleto, historico_inscricao pItem_historico)
        {
            boletos item;
            if (pItem_historico != null)
            {
                contextoEF.historico_inscricao.Add(pItem_historico);
                contextoEF.Database.ExecuteSqlCommand("DELETE FROM historico_inscricao WHERE status = 'Sem Regsitro Gemini' and id_inscricao=" + pItem_historico.id_inscricao);
            }
            item = contextoEF.boletos.Where(x => x.id_boleto == pItem_boleto.id_boleto).SingleOrDefault();
            item.data_pagamento = pItem_boleto.data_pagamento;
            item.data_cancelamento = pItem_boleto.data_cancelamento;
            item.data_verificacao_sem_registro = pItem_boleto.data_verificacao_sem_registro;
            item.data_alteracao = pItem_boleto.data_alteracao;
            item.usuario = pItem_boleto.usuario;
            contextoEF.SaveChanges();
            return true;
        }

        public bool ExcluiInscricao(int qIdInscricao)
        {
            contextoEF.Database.ExecuteSqlCommand("DELETE FROM inscricao_boleto WHERE id_inscricao=" + qIdInscricao);
            contextoEF.Database.ExecuteSqlCommand("DELETE FROM historico_inscricao WHERE id_inscricao=" + qIdInscricao);
            contextoEF.Database.ExecuteSqlCommand("DELETE FROM ficha_inscricao_matricula WHERE id_inscricao=" + qIdInscricao);
            contextoEF.Database.ExecuteSqlCommand("DELETE FROM fichas_inscricao WHERE id_inscricao=" + qIdInscricao);
            contextoEF.SaveChanges();
            return true;
        }

        public List<boletos> ListaBoletosAbertos()
        {
            var c = contextoEF.boletos.AsQueryable();
            List<boletos> lista = new List<boletos>();

            c = c.Where(x => x.status== "Emitido" && x.data_pagamento == null && x.data_cancelamento == null && x.data_verificacao_sem_registro == null);

            lista = c.OrderByDescending(x => x.data_cadastro).ToList();
            return lista;
        }

        public List<boletos> ListaBoletosAbertos_byFichaInscricao( List<fichas_inscricao> qLista)
        {
            var c = contextoEF.boletos.AsQueryable();
            List<boletos> lista = new List<boletos>();


            var a = qLista.Select(x => x.boletos.Select(y => y.id_boleto)).ToArray();

            c = c.Where(x => x.status == "Emitido" && x.data_pagamento == null && x.data_cancelamento == null && x.data_verificacao_sem_registro == null);

            lista = c.OrderByDescending(x => x.data_cadastro).ToList();
            return lista;
        }

        public bool ExcluiHistorico_inscricao_Registro_Gemini(int id_inscricao)
        {
            contextoEF.Database.ExecuteSqlCommand("DELETE FROM historico_inscricao WHERE status = 'Sem Regsitro Gemini' and id_inscricao=" + id_inscricao);

            return true;
        }

        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}

