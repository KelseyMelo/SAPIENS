

using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Configuration;
using System.Data.SqlClient;

namespace Repositorio_C
{
    public class FIPTRepositorio : IDisposable
    {
        private Entities contextoEF;

        public FIPTRepositorio()
        {
            contextoEF = new Entities();
        }

        public alunos_boletos BuscaItem(alunos_boletos pItem)
        {
            alunos_boletos item = new alunos_boletos();
            item = contextoEF.alunos_boletos.Where(x => x.id_alunos_boletos == pItem.id_alunos_boletos).SingleOrDefault();
            //item = contextoEF.cursos.Include(x => x.cursos_coordenadores).Include(x => x.cursos_disciplinas).Where(x => x.id_curso == pItem.id_curso).SingleOrDefault();
            return item;
        }

        public boleto_email BuscaItem(boleto_email pItem)
        {
            boleto_email item = contextoEF.boleto_email.Where(x => x.id_boleto_email == pItem.id_boleto_email).SingleOrDefault();
            //item = contextoEF.cursos.Include(x => x.cursos_coordenadores).Include(x => x.cursos_disciplinas).Where(x => x.id_curso == pItem.id_curso).SingleOrDefault();
            return item;
        }

        public alunos_boletos_parcelas BuscaItem_BoletoParcela(alunos_boletos_parcelas pItem)
        {
            alunos_boletos_parcelas item = new alunos_boletos_parcelas();
            if (pItem.id_alunos_boletos_parcelas != 0)
            {
                item = contextoEF.alunos_boletos_parcelas.Where(x => x.id_alunos_boletos_parcelas == pItem.id_alunos_boletos_parcelas).SingleOrDefault();
            }
            else
            {
                item = contextoEF.alunos_boletos_parcelas.Where(x => x.IDLancamento == pItem.IDLancamento).SingleOrDefault();
            }
            //item = contextoEF.cursos.Include(x => x.cursos_coordenadores).Include(x => x.cursos_disciplinas).Where(x => x.id_curso == pItem.id_curso).SingleOrDefault();
            return item;
        }

        public List<alunos_boletos> Lista_BuscaItem_idaluno(alunos_boletos pItem)
        {
            List<alunos_boletos> lista = new List<alunos_boletos>();
            lista = contextoEF.alunos_boletos.Where(x => x.idaluno == pItem.idaluno).ToList();
            //item = contextoEF.cursos.Include(x => x.cursos_coordenadores).Include(x => x.cursos_disciplinas).Where(x => x.id_curso == pItem.id_curso).SingleOrDefault();
            return lista;
        }

        public alunos_inadimpentes_fipt BuscaItem(alunos_inadimpentes_fipt pItem)
        {
            alunos_inadimpentes_fipt item = new alunos_inadimpentes_fipt();
            item = contextoEF.alunos_inadimpentes_fipt.Where(x => x.id_aluno_inadimplente == pItem.id_aluno_inadimplente).SingleOrDefault();
            //item = contextoEF.cursos.Include(x => x.cursos_coordenadores).Include(x => x.cursos_disciplinas).Where(x => x.id_curso == pItem.id_curso).SingleOrDefault();
            return item;
        }

        public alunos_curso_inadimplente BuscaItem(alunos_curso_inadimplente pItem)
        {
            alunos_curso_inadimplente item = new alunos_curso_inadimplente();
            item = contextoEF.alunos_curso_inadimplente.Where(x => x.id_aluno_curso_inadimplente == pItem.id_aluno_curso_inadimplente).SingleOrDefault();
            //item = contextoEF.cursos.Include(x => x.cursos_coordenadores).Include(x => x.cursos_disciplinas).Where(x => x.id_curso == pItem.id_curso).SingleOrDefault();
            return item;
        }

        public alunos_inadimpentes_fipt BuscaItem_idSapiens(alunos_inadimpentes_fipt pItem)
        {
            alunos_inadimpentes_fipt item = new alunos_inadimpentes_fipt();
            item = contextoEF.alunos_inadimpentes_fipt.Where(x => x.idaluno == pItem.idaluno).SingleOrDefault();
            //item = contextoEF.cursos.Include(x => x.cursos_coordenadores).Include(x => x.cursos_disciplinas).Where(x => x.id_curso == pItem.id_curso).SingleOrDefault();
            return item;
        }
        
        public alunos_inadimpentes_fipt CriarItem_alunos_inadimplentes_fipt(alunos_inadimpentes_fipt pItem)
        {
            contextoEF.alunos_inadimpentes_fipt.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public alunos_curso_inadimplente CriarItem_alunos_curso_inadimplente(alunos_curso_inadimplente pItem)
        {
            contextoEF.alunos_curso_inadimplente.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public alunos_parcelas_inadimplente CriarItem_alunos_parcelas_inadimplente(alunos_parcelas_inadimplente pItem)
        {
            contextoEF.alunos_parcelas_inadimplente.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public alunos_inadimplentes_emails_enviados CriarItem_alunos_inadimplentes_emails_enviados(alunos_inadimplentes_emails_enviados pItem)
        {
            using (var contextoEF = new Entities())
            {
                contextoEF.alunos_inadimplentes_emails_enviados.Add(pItem);
                contextoEF.SaveChanges();
            }
            return pItem;
        }

        public bool Criar_emails_enviados_alunos_Inadimplentes_Gemini(int pIdAlunoFIPT, DateTime pDataEnvioEmail, int pId_aluno_curso_inadimplente)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string strConnString;
            string sSql;

            List<alunos_inadimpentes_fipt> lista_alunos_inadimpentes_fipt = new List<alunos_inadimpentes_fipt>();
            alunos_inadimpentes_fipt item_alunos_inadimpentes_fipt = new alunos_inadimpentes_fipt();
            alunos_curso_inadimplente item_alunos_curso_inadimplente = new alunos_curso_inadimplente(); 
            alunos_parcelas_inadimplente item_alunos_parcelas_inadimplente = new alunos_parcelas_inadimplente();

            strConnString = ConfigurationManager.ConnectionStrings["FIPTEntities"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "";
            //sSql = sSql + "update tbALUNO set ocorrencias_ipt = 'Email SAPIENS 06/12/2021 10:51:35' ";
            sSql = sSql + "update tbALUNO set ocorrencias_ipt = ' Email SAPIENS " + pDataEnvioEmail.ToString("dd/MM/yyyy HH:mm:ss") + " ' ";
            sSql = sSql + "where idaluno = " + pIdAlunoFIPT;


            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            var retorno = cmd.ExecuteNonQuery();
            cCon.Close();

            alunos_curso_inadimplente item;
            using (var contextoEF = new Entities())
            {
                item = contextoEF.alunos_curso_inadimplente.Where(x => x.id_aluno_curso_inadimplente == pId_aluno_curso_inadimplente).SingleOrDefault();
                item.Ocorrencias = item.Ocorrencias + "\n Email SAPIENS " + pDataEnvioEmail.ToString("dd/MM/yyyy HH:mm:ss") + " ";
                contextoEF.SaveChanges();
            }
            return true;
        }

        public alunos_inadimpentes_fipt Alterar_alunos_inadimpentes_fipt(alunos_inadimpentes_fipt pItem)
        {
            alunos_inadimpentes_fipt item = new alunos_inadimpentes_fipt();
            item = contextoEF.alunos_inadimpentes_fipt.Where(x => x.id_aluno_inadimplente == pItem.id_aluno_inadimplente).SingleOrDefault();
            item.cpf = pItem.cpf;
            item.email = pItem.email;
            item.nome = pItem.nome;
            item.idaluno = pItem.idaluno;
            item.idaluno_fipt = pItem.idaluno_fipt;
            item.data_pesquisa_fipt = pItem.data_pesquisa_fipt;
            item.usuario = pItem.usuario;
            contextoEF.SaveChanges();
            return item;
        }

        public alunos_curso_inadimplente Alterar_alunos_curso_inadimplente(alunos_curso_inadimplente pItem)
        {
            alunos_curso_inadimplente item = new alunos_curso_inadimplente();
            item = contextoEF.alunos_curso_inadimplente.Where(x => x.id_aluno_curso_inadimplente == pItem.id_aluno_curso_inadimplente).SingleOrDefault();
            item.id_aluno_inadimplente = pItem.id_aluno_inadimplente;
            item.IDCurso = pItem.IDCurso;
            item.NomeCurso = pItem.NomeCurso;
            item.Ocorrencias = pItem.Ocorrencias;
            item.data_exclusao_serasa = pItem.data_exclusao_serasa;
            item.data_inclusao_serasa = pItem.data_inclusao_serasa;
            contextoEF.SaveChanges();
            return item;
        }

        public alunos_parcelas_inadimplente Alterar_alunos_parcelas_inadimplente(alunos_parcelas_inadimplente pItem)
        {
            alunos_parcelas_inadimplente item = new alunos_parcelas_inadimplente();
            item = contextoEF.alunos_parcelas_inadimplente.Where(x => x.id_alunos_parcelas_inadimplente == pItem.id_alunos_parcelas_inadimplente).SingleOrDefault();
            item.id_aluno_curso_inadimplente = pItem.id_aluno_curso_inadimplente;
            item.DataVencimentoInt = pItem.DataVencimentoInt;
            item.DataVencimento = pItem.DataVencimento;
            item.ValorOriginal = pItem.ValorOriginal;
            item.IDLancamento = pItem.IDLancamento;
            item.dias_atraso = pItem.dias_atraso;
            item.multa = pItem.multa;
            item.juros = pItem.juros;
            item.valor_corrigido = pItem.valor_corrigido;
            contextoEF.SaveChanges();
            return item;
        }

        public bool Excluir_unico_alunos_boletos(alunos_boletos pItem)
        {
            alunos_boletos item = new alunos_boletos();
            item = contextoEF.alunos_boletos.Where(x => x.id_alunos_boletos == pItem.id_alunos_boletos).SingleOrDefault();
            contextoEF.alunos_boletos.Remove(item);
            contextoEF.SaveChanges();
            return true;
        }

        public bool Excluir_unico_alunos_inadimpentes_fipt(alunos_inadimpentes_fipt pItem)
        {
            alunos_inadimpentes_fipt item = new alunos_inadimpentes_fipt();
            item = contextoEF.alunos_inadimpentes_fipt.Where(x => x.id_aluno_inadimplente == pItem.id_aluno_inadimplente).SingleOrDefault();
            contextoEF.alunos_inadimpentes_fipt.Remove(item);
            contextoEF.SaveChanges();
            return true;
        }

        public bool Excluir_todos_alunos_inadimpentes_fipt(alunos_inadimpentes_fipt pItem)
        {
            contextoEF.alunos_inadimpentes_fipt.RemoveRange(contextoEF.alunos_inadimpentes_fipt);
            contextoEF.SaveChanges();
            return true;
        }

        public bool Excluir_Lote_unico_alunos_boletos_curso(alunos_boletos pItem)
        {
            contextoEF.alunos_boletos_curso.RemoveRange(contextoEF.alunos_boletos_curso.Where(x => x.id_alunos_boletos == pItem.id_alunos_boletos));
            contextoEF.SaveChanges();
            return true;
        }

        public bool Excluir_Lote_unico_alunos_curso_inadimplente(alunos_curso_inadimplente pItem)
        {
            contextoEF.alunos_curso_inadimplente.RemoveRange(contextoEF.alunos_curso_inadimplente.Where(x=> x.id_aluno_curso_inadimplente == pItem.id_aluno_curso_inadimplente));
            contextoEF.SaveChanges();
            return true;
        }

        public bool Excluir_Lote_todos_alunos_curso_inadimplente(alunos_curso_inadimplente pItem)
        {
            contextoEF.alunos_curso_inadimplente.RemoveRange(contextoEF.alunos_curso_inadimplente);
            contextoEF.SaveChanges();
            return true;
        }

        public bool Excluir_Lote_unico_alunos_boletos_parcelas(alunos_boletos_curso pItem)
        {
            contextoEF.alunos_boletos_parcelas.RemoveRange(contextoEF.alunos_boletos_parcelas.Where(x => x.id_alunos_boletos_curso == pItem.id_alunos_boletos_curso));
            contextoEF.SaveChanges();
            return true;
        }

        public bool Excluir_Lote_unico_alunos_parcelas_inadimplente(alunos_curso_inadimplente pItem)
        {
            contextoEF.alunos_parcelas_inadimplente.RemoveRange(contextoEF.alunos_parcelas_inadimplente.Where(x => x.id_aluno_curso_inadimplente == pItem.id_aluno_curso_inadimplente));
            contextoEF.SaveChanges();
            return true;
        }

        public bool Excluir_Lote_todos_alunos_parcelas_inadimplente(alunos_parcelas_inadimplente pItem)
        {
            contextoEF.alunos_parcelas_inadimplente.RemoveRange(contextoEF.alunos_parcelas_inadimplente);
            contextoEF.SaveChanges();
            return true;
        }

        public List<alunos_inadimpentes_fipt> ListaAlunosInadimpelntes(alunos_inadimpentes_fipt pItem)
        {
            var c = contextoEF.alunos_inadimpentes_fipt.AsQueryable();
            List<alunos_inadimpentes_fipt> lista = new List<alunos_inadimpentes_fipt>();

            if (pItem.nome != null)
            {
                c = c.Where(x => x.nome.Contains(pItem.nome));
            }

            lista = contextoEF.alunos_inadimpentes_fipt.OrderBy(x=> x.nome).ToList();
            return lista;
        }

        public List<alunos_curso_inadimplente> ListaAlunosInadimpelntes2(alunos_inadimpentes_fipt pItem)
        {
            var c = contextoEF.alunos_curso_inadimplente.AsQueryable();
            List<alunos_curso_inadimplente> lista = new List<alunos_curso_inadimplente>();

            //pItem.data_pesquisa_fipt = Convert.ToDateTime("01/01/2021");

            //c = c.Where(x => x.alunos_inadimpentes_fipt.cpf == "534.352.814-72" || x.alunos_inadimpentes_fipt.cpf == "291.497.588-02");
            if (pItem.data_pesquisa_fipt != null)
            {
                var sAux = contextoEF.alunos_parcelas_inadimplente.Where(x => x.DataVencimento >= pItem.data_pesquisa_fipt).Select(x => new { x.alunos_curso_inadimplente.alunos_inadimpentes_fipt.cpf, x.alunos_curso_inadimplente.IDCurso });

                string[] qDupla = new string[] { "" };

                foreach (var elemento in sAux)
                {
                    qDupla = qDupla.Concat(new string[] { elemento.cpf + elemento.IDCurso }).ToArray();
                }

                c = c.Where(x => qDupla.Contains(x.alunos_inadimpentes_fipt.cpf + x.IDCurso));
            }
            
            //c = c.Where(x => x.alunos_parcelas_inadimplente.);

            if (pItem.idaluno != null)
            {
                c = c.Where(x => x.alunos_inadimpentes_fipt.idaluno == pItem.idaluno);
            }

            if (pItem.nome != null)
            {
                c = c.Where(x => x.alunos_inadimpentes_fipt.nome.Contains(pItem.nome));
            }

            if (pItem.alunos_curso_inadimplente.ElementAt(0).NomeCurso != null)
            {
                string qCurso = pItem.alunos_curso_inadimplente.ElementAt(0).NomeCurso;
                c = c.Where(x => x.NomeCurso.Contains(qCurso));
            }

            lista = c.OrderBy(x => x.alunos_inadimpentes_fipt.nome).ThenBy(x=> x.NomeCurso).ToList();

            return lista;
        }

        public List<alunos_boletos_curso> ListaAlunosBoletos(alunos_boletos pItem)
        {
            var c = contextoEF.alunos_boletos_curso.AsQueryable();
            List<alunos_boletos_curso> lista = new List<alunos_boletos_curso>();

            //pItem.data_pesquisa_fipt = Convert.ToDateTime("01/01/2021");

            //c = c.Where(x => x.alunos_inadimpentes_fipt.cpf == "534.352.814-72" || x.alunos_inadimpentes_fipt.cpf == "291.497.588-02");
            if (pItem.data_pesquisa_fipt != null)
            {
                var sAux = contextoEF.alunos_boletos_parcelas.Where(x => x.data_venc >= pItem.data_pesquisa_fipt).Select(x => new { x.alunos_boletos_curso.alunos_boletos.cpf, x.alunos_boletos_curso.IDCurso });

                string[] qDupla = new string[] { "" };

                foreach (var elemento in sAux)
                {
                    qDupla = qDupla.Concat(new string[] { elemento.cpf + elemento.IDCurso }).ToArray();
                }

                c = c.Where(x => qDupla.Contains(x.alunos_boletos.cpf + x.IDCurso));
            }

            //c = c.Where(x => x.alunos_parcelas_inadimplente.);

            if (pItem.idaluno != null)
            {
                c = c.Where(x => x.alunos_boletos.idaluno == pItem.idaluno);
            }

            if (pItem.nome != null)
            {
                c = c.Where(x => x.alunos_boletos.nome.Contains(pItem.nome));
            }

            //if (pItem.alunos_boletos_curso.ElementAt(0).nome_curso != null)
            //{
            //    string qCurso = pItem.alunos_boletos_curso.ElementAt(0).nome_curso;
            //    c = c.Where(x => x.nome_curso.Contains(qCurso));
            //}

            lista = c.OrderBy(x => x.alunos_boletos.nome).ThenBy(x => x.nome_curso).ToList();

            return lista;
        }

        private alunos EncontraIdAluno (alunos_inadimpentes_fipt pItem)
        {
            alunos item;
            item = contextoEF.alunos.Where(x => x.cpf == pItem.cpf).SingleOrDefault();
            if (item != null)
            {
                return item;
            }
            if (pItem.email != "")
            {
                var arrayEmail = pItem.email.Split(';');
                foreach (var elemento in arrayEmail)
                {
                    item = contextoEF.alunos.Where(x => x.email == elemento.Trim()).SingleOrDefault();
                    if (item != null)
                    {
                        return item;
                    }
                    item = contextoEF.alunos.Where(x => x.email2 == elemento.Trim()).SingleOrDefault();
                    if (item != null)
                    {
                        return item;
                    }
                }
            }
            item = contextoEF.alunos.Where(x => x.nome == pItem.nome).SingleOrDefault();
            if (item != null)
            {
                return item;
            }
            return item;
        }

        private alunos EncontraIdAluno(alunos_boletos pItem)
        {
            alunos item;
            item = contextoEF.alunos.Where(x => x.cpf == pItem.cpf).SingleOrDefault();
            if (item != null)
            {
                return item;
            }
            if (pItem.email != "")
            {
                var arrayEmail = pItem.email.Split(';');
                foreach (var elemento in arrayEmail)
                {
                    item = contextoEF.alunos.Where(x => x.email == elemento.Trim()).SingleOrDefault();
                    if (item != null)
                    {
                        return item;
                    }
                    item = contextoEF.alunos.Where(x => x.email2 == elemento.Trim()).SingleOrDefault();
                    if (item != null)
                    {
                        return item;
                    }
                }
            }
            item = contextoEF.alunos.Where(x => x.nome == pItem.nome).SingleOrDefault();
            if (item != null)
            {
                return item;
            }
            return item;
        }

        public static string removerAcentos(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }

        public bool ConsultaAlunosFipt(alunos_inadimpentes_fipt pItem)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string strConnString;
            string sSql;

            List<alunos_inadimpentes_fipt> lista_alunos_inadimpentes_fipt = new List<alunos_inadimpentes_fipt>();
            alunos_inadimpentes_fipt item_alunos_inadimpentes_fipt = new alunos_inadimpentes_fipt();
            alunos_curso_inadimplente item_alunos_curso_inadimplente = new alunos_curso_inadimplente(); ;
            alunos_parcelas_inadimplente item_alunos_parcelas_inadimplente = new alunos_parcelas_inadimplente();

            strConnString = ConfigurationManager.ConnectionStrings["FIPTEntities"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "";
            sSql = sSql + "select * from ";
            sSql = sSql + "VW_ET_ALUNOS_INADIMPLENTES ";
            if (pItem.idaluno_fipt != 0 && pItem.idaluno_fipt != null)
            {
                sSql = sSql + "where idaluno = " + pItem.idaluno_fipt;
            }
            sSql = sSql + "order by idaluno, IDCurso, DataVencimentoInt";

            //sSql = sSql + "			  b.data_entrega_trabalho, '" + qData.ToString("dd/MM/yyyy") + "' AS data_calculo, b.id_banca ";
           
            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            SqlDataReader retorno = cmd.ExecuteReader();

            alunos item_aluno;

            if (retorno.HasRows)
            {
                while (retorno.Read())
                {
                    if (retorno["idaluno"].ToString() != "")
                    {
                        if (item_alunos_inadimpentes_fipt.idaluno_fipt != Convert.ToInt32(retorno["idaluno"]))
                        {
                            item_alunos_inadimpentes_fipt = new alunos_inadimpentes_fipt();
                            item_alunos_curso_inadimplente = new alunos_curso_inadimplente();

                            item_alunos_inadimpentes_fipt.idaluno_fipt = Convert.ToInt32(retorno["idaluno"]);
                            item_alunos_inadimpentes_fipt.cpf = retorno["cpf"].ToString();
                            item_alunos_inadimpentes_fipt.email = retorno["Email"].ToString();
                            item_alunos_inadimpentes_fipt.nome = retorno["Nome"].ToString();
                            item_alunos_inadimpentes_fipt.data_pesquisa_fipt = pItem.data_pesquisa_fipt;
                            item_alunos_inadimpentes_fipt.usuario = pItem.usuario;
                            item_aluno = EncontraIdAluno(item_alunos_inadimpentes_fipt);
                            if (item_aluno == null)
                            {
                                item_alunos_inadimpentes_fipt.idaluno = null;
                                item_alunos_inadimpentes_fipt.observacao = "Dados da Base FIPT:<br> IdAluno: " + item_alunos_inadimpentes_fipt.idaluno_fipt +
                                       "<br> CPF: " + item_alunos_inadimpentes_fipt.cpf +
                                       "<br> Nome: " + item_alunos_inadimpentes_fipt.nome +
                                       "<br> E-mail: " + item_alunos_inadimpentes_fipt.email + "<br><br>ALUNO NÃO ENCONTRADO";

                            }
                            else
                            {
                                item_alunos_inadimpentes_fipt.idaluno = item_aluno.idaluno;
                                string sAux = "";
                                if (removerAcentos(item_alunos_inadimpentes_fipt.nome.ToUpper()).Trim() != removerAcentos(item_aluno.nome.ToUpper()).Trim())
                                {
                                    sAux = "O nome do Aluno está diferente.<br> Base FIPT: " + item_alunos_inadimpentes_fipt.nome + "<br>Base SAPIENS: " + item_aluno.nome + "<br><br>";
                                }

                                var arrayEmailFipt = item_alunos_inadimpentes_fipt.email.ToLower().Split(';');
                                string[] arrayEmailSapiens1 = new string[0];
                                if (item_aluno.email != null)
                                {
                                    arrayEmailSapiens1 = item_aluno.email.ToLower().Split(';');
                                }
                                string [] arrayEmailSapiens2 = new string[0];
                                if (item_aluno.email2 != null)
                                {
                                    arrayEmailSapiens2 = item_aluno.email2.ToLower().Split(';');
                                }
                                
                                bool bAchouEmail = false;

                                foreach (var elementoEmailFipt in arrayEmailFipt)
                                {
                                    if (bAchouEmail)
                                    {
                                        continue;
                                    }
                                    if (!bAchouEmail)
                                    {
                                        foreach (var elementoEmailSapiens1 in arrayEmailSapiens1)
                                        {
                                            if (elementoEmailSapiens1.ToLower().Trim() == elementoEmailFipt.ToLower().Trim())
                                            {
                                                bAchouEmail = true;
                                                continue;
                                            }
                                        }
                                    }
                                    if (!bAchouEmail)
                                    {
                                        foreach (var elementoEmailSapiens2 in arrayEmailSapiens2)
                                        {
                                            if (elementoEmailSapiens2.ToLower().Trim() == elementoEmailFipt.ToLower().Trim())
                                            {
                                                bAchouEmail = true;
                                                continue;
                                            }
                                        }
                                    }
                                }

                                if (!bAchouEmail)
                                {
                                    sAux = sAux + "O email do Aluno está diferente.<br> Base FIPT: " + item_alunos_inadimpentes_fipt.email + "<br>Base SAPIENS: " + item_aluno.email + "<br><br>";
                                }

                                if (item_alunos_inadimpentes_fipt.cpf != item_aluno.cpf)
                                {
                                    sAux = sAux + "O CPF do Aluno está diferente.<br> Base FIPT: " + item_alunos_inadimpentes_fipt.cpf + "<br>Base SAPIENS: " + item_aluno.cpf + "<br><br>";
                                }

                                if (sAux != "")
                                {
                                    string vAux2;
                                    vAux2 = "Dados da Base FIPT:<br> IdAluno: " + item_alunos_inadimpentes_fipt.idaluno_fipt +
                                        "<br> CPF: " + item_alunos_inadimpentes_fipt.cpf +
                                        "<br> Nome: " + item_alunos_inadimpentes_fipt.nome +
                                        "<br> E-mail: " + item_alunos_inadimpentes_fipt.email + "<br><br>";
                                    item_alunos_inadimpentes_fipt.observacao = vAux2 + sAux;
                                }
                            }
                            item_alunos_inadimpentes_fipt = CriarItem_alunos_inadimplentes_fipt(item_alunos_inadimpentes_fipt);
                        }

                        if (item_alunos_curso_inadimplente.IDCurso != Convert.ToInt32(retorno["IDCurso"]))
                        {
                            item_alunos_curso_inadimplente = new alunos_curso_inadimplente();
                            //item_alunos_parcelas_inadimplente = new alunos_parcelas_inadimplente();

                            item_alunos_curso_inadimplente.id_aluno_inadimplente = item_alunos_inadimpentes_fipt.id_aluno_inadimplente;
                            item_alunos_curso_inadimplente.IDCurso = Convert.ToInt32(retorno["IDCurso"]);
                            item_alunos_curso_inadimplente.NomeCurso = retorno["NomeCurso"].ToString();
                            item_alunos_curso_inadimplente.Ocorrencias = retorno["Ocorrencias"].ToString();
                            item_alunos_curso_inadimplente.data_inclusao_serasa = Convert.ToDateTime(retorno["DataInclusaoSerasa"]);
                            item_alunos_curso_inadimplente.data_exclusao_serasa = Convert.ToDateTime(retorno["DataExclusaoSerasa"]);

                            item_alunos_curso_inadimplente = CriarItem_alunos_curso_inadimplente(item_alunos_curso_inadimplente);
                        }


                        item_alunos_parcelas_inadimplente = new alunos_parcelas_inadimplente();

                        item_alunos_parcelas_inadimplente.id_aluno_curso_inadimplente = item_alunos_curso_inadimplente.id_aluno_curso_inadimplente;
                        item_alunos_parcelas_inadimplente.DataVencimentoInt = Convert.ToDecimal(retorno["DataVencimentoInt"]);
                        item_alunos_parcelas_inadimplente.DataVencimento = Convert.ToDateTime(retorno["DataVencimento"]);
                        item_alunos_parcelas_inadimplente.ValorOriginal = Convert.ToDecimal(retorno["Valorreceber"]);
                        item_alunos_parcelas_inadimplente.IDLancamento = Convert.ToInt32(retorno["IDLancamento"]);

                        item_alunos_parcelas_inadimplente.dias_atraso = (pItem.data_pesquisa_fipt - item_alunos_parcelas_inadimplente.DataVencimento.Value).Value.Days;
                        item_alunos_parcelas_inadimplente.multa = item_alunos_parcelas_inadimplente.ValorOriginal * Convert.ToDecimal(0.02);
                        item_alunos_parcelas_inadimplente.juros = ((Convert.ToDecimal(0.01) / 30) * item_alunos_parcelas_inadimplente.ValorOriginal) * item_alunos_parcelas_inadimplente.dias_atraso;
                        item_alunos_parcelas_inadimplente.valor_corrigido = item_alunos_parcelas_inadimplente.ValorOriginal + item_alunos_parcelas_inadimplente.multa + item_alunos_parcelas_inadimplente.juros;

                        item_alunos_parcelas_inadimplente = CriarItem_alunos_parcelas_inadimplente(item_alunos_parcelas_inadimplente);

                        //dTotalHoraAula = dTotalHoraAula + item_geral_custo_banca.col_Total;
                        //lista_geral_custo_orientacao.Add(item_geral_custo_banca);
                    }
                }

            }

            return true;
        }

        public Boolean Insere_Gemini_OcorrenciaAlunoInadimplente(int pIdAluno_fipt, int pIdCurso, string pOcorrencia,  DateTime pDataOcorrencia)
        {
            string strConnString;
            string sSql;

            strConnString = ConfigurationManager.ConnectionStrings["FIPTEntities"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "Insert into TBOcorrencia (IDAluno, IDCurso, Ocorrencia, DataOcorrencia) Values (";
            sSql = sSql + "'" + pIdAluno_fipt + "',";
            sSql = sSql + "'" + pIdCurso + "',";
            sSql = sSql + "'" + pOcorrencia.Replace("'", "''") + "',";
            sSql = sSql + "getdate())";
            
            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            var retorno = cmd.ExecuteNonQuery();
            cCon.Close();

            return true;
        }

        public bool ConsultaBoletosFipt(alunos_boletos pItem)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string strConnString;
            string sSql;

            List<alunos_boletos> lista_alunos_boletos = new List<alunos_boletos>();
            alunos_boletos item_alunos_boletos = new alunos_boletos();
            alunos_boletos_curso item_alunos_boletos_curso = new alunos_boletos_curso(); ;
            alunos_boletos_parcelas item_alunos_boletos_parcelas = new alunos_boletos_parcelas();

            strConnString = ConfigurationManager.ConnectionStrings["FIPTEntities"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "";
            sSql = sSql + "select * from ";
            sSql = sSql + "VW_ET_ALUNOS_TODOS ";
            if (pItem.idaluno_fipt != 0 && pItem.idaluno_fipt != null)
            {
                sSql = sSql + "where idaluno = " + pItem.idaluno_fipt;
            }
            else if (pItem.cpf != "" && pItem.cpf != null)
            {
                sSql = sSql + "where cpf = '" + pItem.cpf + "' ";
            }
            sSql = sSql + "order by idaluno, IDCurso, DataVencimentoInt";

            //sSql = sSql + "			  b.data_entrega_trabalho, '" + qData.ToString("dd/MM/yyyy") + "' AS data_calculo, b.id_banca ";

            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            SqlDataReader retorno = cmd.ExecuteReader();

            alunos item_aluno;

            if (retorno.HasRows)
            {
                while (retorno.Read())
                {
                    if (retorno["idaluno"].ToString() != "")
                    {
                        if (item_alunos_boletos.idaluno_fipt != Convert.ToInt32(retorno["idaluno"]))
                        {
                            item_alunos_boletos = new alunos_boletos();
                            item_alunos_boletos_curso = new alunos_boletos_curso();

                            item_alunos_boletos.idaluno_fipt = Convert.ToInt32(retorno["idaluno"]);
                            item_alunos_boletos.cpf = retorno["cpf"].ToString();
                            item_alunos_boletos.email = retorno["Email"].ToString();
                            item_alunos_boletos.nome = retorno["Nome"].ToString();
                            item_alunos_boletos.data_pesquisa_fipt = pItem.data_pesquisa_fipt;
                            item_alunos_boletos.usuario = pItem.usuario;
                            item_aluno = EncontraIdAluno(item_alunos_boletos);
                            if (item_aluno == null)
                            {
                                item_alunos_boletos.idaluno = null;
                                item_alunos_boletos.observacao = "Dados da Base FIPT:<br> IdAluno: " + item_alunos_boletos.idaluno_fipt +
                                       "<br> CPF: " + item_alunos_boletos.cpf +
                                       "<br> Nome: " + item_alunos_boletos.nome +
                                       "<br> E-mail: " + item_alunos_boletos.email + "<br><br>ALUNO NÃO ENCONTRADO";

                            }
                            else
                            {
                                item_alunos_boletos.idaluno = item_aluno.idaluno;
                                string sAux = "";
                                if (removerAcentos(item_alunos_boletos.nome.ToUpper()) != removerAcentos(item_aluno.nome.ToUpper()))
                                {
                                    sAux = "O nome do Aluno está diferente.<br> Base FIPT: " + item_alunos_boletos.nome + "<br>Base SAPIENS: " + item_aluno.nome + "<br><br>";
                                }

                                var arrayEmailFipt = item_alunos_boletos.email.ToLower().Split(';');
                                string[] arrayEmailSapiens1 = new string[0];
                                if (item_aluno.email != null)
                                {
                                    arrayEmailSapiens1 = item_aluno.email.ToLower().Split(';');
                                }
                                string[] arrayEmailSapiens2 = new string[0];
                                if (item_aluno.email2 != null)
                                {
                                    arrayEmailSapiens2 = item_aluno.email2.ToLower().Split(';');
                                }

                                bool bAchouEmail = false;

                                foreach (var elementoEmailFipt in arrayEmailFipt)
                                {
                                    if (bAchouEmail)
                                    {
                                        continue;
                                    }
                                    if (!bAchouEmail)
                                    {
                                        foreach (var elementoEmailSapiens1 in arrayEmailSapiens1)
                                        {
                                            if (elementoEmailSapiens1.ToLower().Trim() == elementoEmailFipt.ToLower().Trim())
                                            {
                                                bAchouEmail = true;
                                                continue;
                                            }
                                        }
                                    }
                                    if (!bAchouEmail)
                                    {
                                        foreach (var elementoEmailSapiens2 in arrayEmailSapiens2)
                                        {
                                            if (elementoEmailSapiens2.ToLower().Trim() == elementoEmailFipt.ToLower().Trim())
                                            {
                                                bAchouEmail = true;
                                                continue;
                                            }
                                        }
                                    }
                                }

                                if (!bAchouEmail)
                                {
                                    sAux = sAux + "O email do Aluno está diferente.<br> Base FIPT: " + item_alunos_boletos.email + "<br>Base SAPIENS: " + item_aluno.email + "<br><br>";
                                }

                                if (item_alunos_boletos.cpf != item_aluno.cpf)
                                {
                                    sAux = sAux + "O CPF do Aluno está diferente.<br> Base FIPT: " + item_alunos_boletos.cpf + "<br>Base SAPIENS: " + item_aluno.cpf + "<br><br>";
                                }

                                if (sAux != "")
                                {
                                    string vAux2;
                                    vAux2 = "Dados da Base FIPT:<br> IdAluno: " + item_alunos_boletos.idaluno_fipt +
                                        "<br> CPF: " + item_alunos_boletos.cpf +
                                        "<br> Nome: " + item_alunos_boletos.nome +
                                        "<br> E-mail: " + item_alunos_boletos.email + "<br><br>";
                                    item_alunos_boletos.observacao = vAux2 + sAux;
                                }
                            }
                            item_alunos_boletos = CriarItem_alunos_boletos(item_alunos_boletos);
                        }

                        if (item_alunos_boletos_curso.IDCurso != Convert.ToInt32(retorno["IDCurso"]))
                        {
                            item_alunos_boletos_curso = new alunos_boletos_curso();
                            //item_alunos_boletos_parcelas = new alunos_parcelas_inadimplente();

                            item_alunos_boletos_curso.id_alunos_boletos = item_alunos_boletos.id_alunos_boletos;
                            item_alunos_boletos_curso.IDCurso = Convert.ToInt32(retorno["IDCurso"]);
                            item_alunos_boletos_curso.nome_curso = retorno["NomeCurso"].ToString();
                            //item_alunos_boletos_curso.Ocorrencias = retorno["Ocorrencias"].ToString();
                            //item_alunos_boletos_curso.data_inclusao_serasa = Convert.ToDateTime(retorno["DataInclusaoSerasa"]);
                            //item_alunos_boletos_curso.data_exclusao_serasa = Convert.ToDateTime(retorno["DataExclusaoSerasa"]);

                            item_alunos_boletos_curso = CriarItem_alunos_boletos_curso(item_alunos_boletos_curso);
                        }


                        item_alunos_boletos_parcelas = new alunos_boletos_parcelas();

                        item_alunos_boletos_parcelas.id_alunos_boletos_curso = item_alunos_boletos_curso.id_alunos_boletos_curso;
                        item_alunos_boletos_parcelas.data_venc_int = Convert.ToDecimal(retorno["DataVencimentoInt"]);
                        item_alunos_boletos_parcelas.data_venc = Convert.ToDateTime(retorno["DataVencimento"]);
                        item_alunos_boletos_parcelas.valor_original = Convert.ToDecimal(retorno["Valorreceber"]);
                        item_alunos_boletos_parcelas.IDLancamento = Convert.ToInt32(retorno["IDLancamento"]);
                        item_alunos_boletos_parcelas.nossonumero = retorno["nossonumero"].ToString();
                        item_alunos_boletos_parcelas.numero_convenio = retorno["NumeroConvenio"].ToString();
                        if (retorno["DataPagamento"].ToString() != "")
                        {
                            item_alunos_boletos_parcelas.data_pagto = Convert.ToDateTime(retorno["DataPagamento"]);
                            item_alunos_boletos_parcelas.valor_recebido = Convert.ToDecimal(retorno["ValorRecebido"]);
                        }

                        if (item_alunos_boletos_parcelas.data_pagto == null && item_alunos_boletos_parcelas.data_venc < DateTime.Today)
                        
                        {
                            //item_alunos_boletos_parcelas.dias_atraso = (pItem.data_pesquisa_fipt - item_alunos_boletos_parcelas.data_venc.Value).Value.Days;
                            item_alunos_boletos_parcelas.dias_atraso = (DateTime.Today - item_alunos_boletos_parcelas.data_venc.Value).Days;
                            item_alunos_boletos_parcelas.multa = item_alunos_boletos_parcelas.valor_original * Convert.ToDecimal(0.02);
                            item_alunos_boletos_parcelas.juros = ((Convert.ToDecimal(0.01) / 30) * item_alunos_boletos_parcelas.valor_original) * item_alunos_boletos_parcelas.dias_atraso;
                            item_alunos_boletos_parcelas.valor_corrigido = item_alunos_boletos_parcelas.valor_original + item_alunos_boletos_parcelas.multa + item_alunos_boletos_parcelas.juros;
                        }

                        item_alunos_boletos_parcelas = CriarItem_alunos_boletos_parcelas(item_alunos_boletos_parcelas);

                        //dTotalHoraAula = dTotalHoraAula + item_geral_custo_banca.col_Total;
                        //lista_geral_custo_orientacao.Add(item_geral_custo_banca);
                    }
                }

            }

            return true;
        }

        public geral_Boleto ConsultaDadosBoletosFipt(geral_Boleto pItem)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string strConnString;
            string sSql;

            geral_Boleto item = new geral_Boleto();

            strConnString = ConfigurationManager.ConnectionStrings["FIPTEntities"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "";
            sSql = sSql + "select * from ";
            sSql = sSql + "VW_EDUCACIONAL_LANCAMENTOS ";
            sSql = sSql + "where IDLANCAMENTO = " + pItem.IDLancamento;

            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            SqlDataReader retorno = cmd.ExecuteReader();


            if (retorno.HasRows)
            {
                while (retorno.Read())
                {
                    item.DocumentoReceberPagar = retorno["DocumentoReceberPagar"].ToString();
                    item.NotaFiscalRecibo = retorno["NotaFiscalRecibo"].ToString();
                    item.NumeroNota = retorno["NumeroNota"].ToString();
                    item.CustoBoleto = retorno["CustoBoleto"].ToString();
                    item.IDLancamento = retorno["IDLancamento"].ToString();
                    item.ValorReceberPagar = retorno["ValorReceberPagar"].ToString();
                    item.NossoNumero = retorno["NossoNumero"].ToString();
                    item.DATAVENCIMENTO = retorno["DATAVENCIMENTO"].ToString();
                    item.DATAVENCIMENTOINT = Convert.ToDecimal(retorno["DATAVENCIMENTOINT"].ToString());
                    item.NomePessoaFisicaJuridica = retorno["NomePessoaFisicaJuridica"].ToString();
                    item.CPFCNPJ = retorno["CPFCNPJ"].ToString();
                    item.Endereco = retorno["Endereco"].ToString();
                    item.Bairro = retorno["Bairro"].ToString();
                    item.Numero = retorno["Numero"].ToString();
                    item.Complemento = retorno["Complemento"].ToString();
                    item.Estado = retorno["Estado"].ToString();
                    item.Cidade = retorno["Cidade"].ToString();
                    item.CEP = retorno["CEP"].ToString();
                    item.NumeroBanco = retorno["NumeroBanco"].ToString();
                    item.NumeroAgencia = retorno["NumeroAgencia"].ToString();
                    item.NomeAgencia = retorno["NomeAgencia"].ToString();
                    item.NumeroConta = retorno["NumeroConta"].ToString();
                    item.NumeroConvenio = retorno["NumeroConvenio"].ToString();
                    item.Carteira = retorno["Carteira"].ToString();
                    item.Instrucoes = retorno["Instrucoes"].ToString();
                    item.Variacao = retorno["Variacao"].ToString();
                    item.CodigoCedente = retorno["CodigoCedente"].ToString();
                    item.datapgtoint = retorno["datapgtoint"].ToString();

                    }
                }

            return item;
        }

        public bool ProcessaBoletosMesFipt(int iMes, int iAno)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string strConnString;
            string sSql;

            boleto_email item = null;

            strConnString = ConfigurationManager.ConnectionStrings["FIPTEntities"].ConnectionString;
            SqlConnection cCon = new SqlConnection(strConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter sda = new SqlDataAdapter();
            sSql = "";
            sSql = sSql + "select * from ";
            sSql = sSql + "VW_EDUCACIONAL_LANCAMENTOS ";
            sSql = sSql + "where MONTH(DATAVENCIMENTO) = " + iMes +  " and YEAR(DATAVENCIMENTO) = " + iAno;

            cCon.Open();
            cmd.Connection = cCon;
            cmd.CommandText = sSql;
            SqlDataReader retorno = cmd.ExecuteReader();

            AlunoRepositorio alunoRepositorio = new AlunoRepositorio();
            alunos item_aluno; ;
            boleto_email item_retorno;

            if (retorno.HasRows)
            {
                while (retorno.Read())
                {
                    item = new boleto_email();
                    item_aluno = new alunos();
                    item_aluno.cpf = retorno["CPFCNPJ"].ToString();
                    item_aluno = alunoRepositorio.BuscaItem(item_aluno.cpf);
                    if (item_aluno != null)
                    {
                        item.idaluno = item_aluno.idaluno;
                        item.email = item_aluno.email;
                        item.cpf = item_aluno.cpf;
                        item.nome = item_aluno.nome;
                    }
                    else
                    {
                        item.cpf = retorno["CPFCNPJ"].ToString();
                        item.nome = retorno["NomePessoaFisicaJuridica"].ToString();
                    }
                   
                    item.data_mes_ano = Convert.ToDateTime("01/" + iMes + "/" + iAno);
                    item.data_vencimento = Convert.ToDateTime(retorno["DATAVENCIMENTO"].ToString());
                    //item.usuario =;
                    //item.valor = String.Format("{0:C2}", Convert.ToDecimal(retorno["ValorReceberPagar"].ToString()));
                    item.valor = Convert.ToDecimal(retorno["ValorReceberPagar"]);
                    item.data_cadastro = DateTime.Now;

                    item.numero_banco = retorno["NumeroBanco"].ToString();
                    item.numero_agencia = retorno["NumeroAgencia"].ToString();
                    item.conta = retorno["NumeroConta"].ToString();
                    item.carteira = retorno["Carteira"].ToString();
                    item.moeda = retorno["NumeroBanco"].ToString();
                    item.nosso_numero = retorno["NossoNumero"].ToString();
                    item.instrucoes = retorno["Instrucoes"].ToString();
                    item.rua_endereco = retorno["Endereco"].ToString();
                    item.numero_endereco = retorno["Numero"].ToString();
                    item.complemento_endereco = retorno["Complemento"].ToString();
                    item.bairro_endereco = retorno["Bairro"].ToString();
                    item.cidade_endereco = retorno["Cidade"].ToString();
                    item.estado_endereco = retorno["Estado"].ToString();
                    item.cep_endereco = retorno["CEP"].ToString();
                    item.IDLancamento = Convert.ToInt32(retorno["IDLancamento"].ToString());
                    item.numero_convenio = retorno["NumeroConvenio"].ToString();

                    //item_geral_boleto = new geral_Boleto();
                    //item_geral_boleto.IDLancamento = retorno["IDLancamento"].ToString();
                    //item_geral_boleto = ConsultaDadosBoletosFipt(item_geral_boleto);

                    //item.codigo_barra = item_geral_boleto.;
                    //item.linha_digitavel = retorno["CPFCNPJ"].ToString();

                    //Verifica se já tem registro, caso não tenha insere, caso tenha despresa
                    item_retorno = contextoEF.boleto_email.Where(x => x.cpf == item.cpf && x.data_mes_ano == item.data_mes_ano).SingleOrDefault();

                    if (item_retorno == null)
                    {
                        using (var contextoEF = new Entities())
                        {
                            contextoEF.boleto_email.Add(item);
                            contextoEF.SaveChanges();
                        }
                    }
                }
            }

            return true;
        }

        public List<boleto_email> ListaBoletosMesFipt(int pMatricula, string pNome, int pMes, int pAno)
        {
            var c = contextoEF.boleto_email.AsQueryable();
            List<boleto_email> lista = new List<boleto_email>();

            if (pMatricula != 0)
            {
                c = c.Where(x => x.idaluno == pMatricula);
            }

            if (pNome != "")
            {
                c = c.Where(x => x.nome.Contains(pNome));
            }
            if (pMes != 0)
            {
                c = c.Where(x => x.data_mes_ano.Value.Month == pMes);
            }
            if (pAno != 0)
            {
                c = c.Where(x => x.data_mes_ano.Value.Year == pAno);
            }

            lista = c.OrderBy(x => x.nome).ToList();

            return lista;
        }

        public bool AlteraBoletosMesFipt(boleto_email pItem)
        {
            using (var contextoEF = new Entities())
            {
                boleto_email item;
                item = contextoEF.boleto_email.Where(x => x.id_boleto_email == pItem.id_boleto_email).SingleOrDefault();

                item.linha_digitavel = pItem.linha_digitavel;
                item.codigo_barra = pItem.codigo_barra;
                item.linha = pItem.linha;
                contextoEF.SaveChanges();
            }
            return true;
        }

        public bool AlteraBoletosMesFipt_dataEnvio(boleto_email pItem)
        {
            using (var contextoEF = new Entities())
            {
                boleto_email item;
                item = contextoEF.boleto_email.Where(x => x.id_boleto_email == pItem.id_boleto_email).SingleOrDefault();

                item.data_envio = pItem.data_envio;
                item.usuario = pItem.usuario;
                contextoEF.SaveChanges();
            }
            return true;
        }

        public alunos_boletos CriarItem_alunos_boletos(alunos_boletos pItem)
        {
            contextoEF.alunos_boletos.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public alunos_boletos_curso CriarItem_alunos_boletos_curso(alunos_boletos_curso pItem)
        {
            contextoEF.alunos_boletos_curso.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public alunos_boletos_parcelas CriarItem_alunos_boletos_parcelas(alunos_boletos_parcelas pItem)
        {
            contextoEF.alunos_boletos_parcelas.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}


