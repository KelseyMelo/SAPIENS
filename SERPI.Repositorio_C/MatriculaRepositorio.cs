using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Repositorio_C
{
    public class MatriculaRepositorio : IDisposable
    {
        private Entities contextoEF;

        public MatriculaRepositorio()
        {
            contextoEF = new Entities();
        }

        public periodo_matricula BuscaPeriodoMatricula(periodo_matricula pItem)
        {
            return contextoEF.periodo_matricula.Where(x => x.id_periodo == pItem.id_periodo).SingleOrDefault();
        }

        public matricula BuscaMatricula(matricula pItem)
        {
            return contextoEF.matricula.Where(x => x.id_matricula == pItem.id_matricula).SingleOrDefault();
        }
        public matricula BuscaMatriculaSemId(matricula pItem)
        {
            return contextoEF.matricula.Where(x => x.id_aluno == pItem.id_aluno && x.id_turma == pItem.id_turma && x.id_periodo_matricula == pItem.id_periodo_matricula).SingleOrDefault();
        }

        public periodo_matricula CriaPeriodoMatricula(periodo_matricula pItem)
        {
            contextoEF.periodo_matricula.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public matricula CriaMatricula(matricula pItem)
        {
            contextoEF.matricula.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public bool CriaMatricula_PreOferecimento(matricula pItem_matricula, pre_oferecimentos pItem_pre_oferecimento)
        {
            contextoEF.Database.ExecuteSqlCommand("INSERT INTO matricula_pre_oferecimentos (id_matricula, id_pre_oferecimento) VALUES (" + pItem_matricula.id_matricula + "," + pItem_pre_oferecimento.id_pre_oferecimento + ")");
            contextoEF.SaveChanges();
            pItem_matricula = contextoEF.matricula.Include(x => x.pre_oferecimentos).Where(x => x.id_matricula == pItem_matricula.id_matricula).SingleOrDefault();
            return true;
        }

        public bool ApagarMatricula_PreOferecimento(matricula pItem_matricula, pre_oferecimentos pItem_pre_oferecimento)
        {
            contextoEF.Database.ExecuteSqlCommand("DELETE matricula_pre_oferecimentos where id_matricula = " + pItem_matricula.id_matricula + " and id_pre_oferecimento = " + pItem_pre_oferecimento.id_pre_oferecimento );
            contextoEF.SaveChanges();
            pItem_matricula = contextoEF.matricula.Include(x => x.pre_oferecimentos).Where(x => x.id_matricula == pItem_matricula.id_matricula).SingleOrDefault();
            if (pItem_matricula.pre_oferecimentos.Count == 0)
            {
                contextoEF.matricula.Remove(pItem_matricula);
                contextoEF.SaveChanges();
            }

            return true;
        }

        public periodo_matricula AlteraPeriodoMatricula(periodo_matricula pItem)
        {
            periodo_matricula item;
            item = contextoEF.periodo_matricula.Where(x => x.id_periodo == pItem.id_periodo).SingleOrDefault();
            item.quadrimestre = pItem.quadrimestre;
            item.data_inicio = pItem.data_inicio;
            item.data_termino = pItem.data_termino;
            item.data_alteracao = pItem.data_alteracao;
            item.usuario = pItem.usuario;

            contextoEF.SaveChanges();
            return item;
        }

        public bool ExcluiPeriodoMatricula(periodo_matricula pItem)
        {
            periodo_matricula item = contextoEF.periodo_matricula.Where(x => x.id_periodo == pItem.id_periodo).SingleOrDefault();
            contextoEF.periodo_matricula.Remove(item);
            contextoEF.SaveChanges();
            return true;
        }

        public List<periodo_matricula> ListaItem(periodo_matricula pItem)
        {
            var c = contextoEF.periodo_matricula.AsQueryable();
            List<periodo_matricula> lista = new List<periodo_matricula>();

            DateTime dDataDefault = new DateTime();

            if (pItem.quadrimestre != "" && pItem.quadrimestre != null)
            {
                c = c.Where(x => x.quadrimestre == pItem.quadrimestre);
            }
            else if (pItem.id_periodo != 0)
            {
                //o pItem.id_periodo está fazendo as vezes do id_tipo_curso
                var qQuadrimentre = contextoEF.quadrimestres.Where(x => x.id_tipo_curso == pItem.id_periodo).Select(x => x.quadrimestre).ToArray();
                c = c.Where(x => qQuadrimentre.Contains(x.quadrimestre));

            }

            if (pItem.data_inicio != null && pItem.data_inicio != dDataDefault)
            {
                c = c.Where(x => x.data_inicio >= pItem.data_inicio);
            }

            if (pItem.data_termino != null && pItem.data_termino != dDataDefault)
            {
                c = c.Where(x => x.data_termino <= pItem.data_termino);
            }

            lista = c.OrderByDescending(x => x.quadrimestre).ToList();

            return lista;
        }

        //public List<cursos> ListaCursosPeriodo(int qIdPeriodo)
        //{
        //    var c = contextoEF.periodo_matricula.AsQueryable();
        //    List<periodo_matricula> lista = new List<periodo_matricula>();

        //    DateTime dDataDefault = new DateTime();

        //    if (pItem.quadrimestre != "" && pItem.quadrimestre != null)
        //    {
        //        c = c.Where(x => x.quadrimestre == pItem.quadrimestre);
        //    }

        //    if (pItem.data_inicio != null && pItem.data_inicio != dDataDefault)
        //    {
        //        c = c.Where(x => x.data_inicio >= pItem.data_inicio);
        //    }

        //    if (pItem.data_termino != null && pItem.data_termino != dDataDefault)
        //    {
        //        c = c.Where(x => x.data_termino <= pItem.data_termino);
        //    }

        //    lista = c.OrderByDescending(x => x.quadrimestre).ToList();

        //    return lista;
        //}

        public List<disciplinas> ListaDisciplinasDisponiveis(periodo_matricula pItem, disciplinas pItemDisciplina)
        {
            var c = contextoEF.disciplinas.AsQueryable();
            List<disciplinas> lista = new List<disciplinas>();

            var qIdTipoCurso = contextoEF.quadrimestres.Where(x => x.quadrimestre == pItem.quadrimestre).FirstOrDefault().id_tipo_curso;

            if (qIdTipoCurso != null)
            {

                var qIdCurso = contextoEF.cursos.Where(x => x.id_tipo_curso == qIdTipoCurso).Select(x => x.id_curso).ToArray();

                var qIdDisciplina = contextoEF.cursos_disciplinas.Where(x => qIdCurso.Contains(x.id_curso)).Select(x => x.id_disciplina).ToArray();

                c = c.Where(x => qIdDisciplina.Contains(x.id_disciplina));
            }

            //Inibido para que procure por qualquer disciplina que esteja já associada a qualquer curso. Cso esteja não é pra mostrar a discuplina.
            var sAux = contextoEF.pre_oferecimentos.Where(x=> x.id_periodo_matricula == pItem.id_periodo).Select(x => x.id_disciplina).ToArray();
            //lista = contextoEF.professores.Where(x => !sAux.Contains(x.id_professor)).ToList();

            c = c.Where(x => !sAux.Contains(x.id_disciplina) && x.status != "inativado");

            if (pItemDisciplina.codigo != "")
            {
                c = c.Where(x => x.codigo == pItemDisciplina.codigo);
            }

            if (pItemDisciplina.nome != "")
            {
                c = c.Where(x => x.nome.Contains(pItemDisciplina.nome));
            }

            lista = c.OrderBy(x => x.nome).ToList();

            return lista;
        }

        public bool CriaPreOferecimento(pre_oferecimentos pItem)
        {
            contextoEF.pre_oferecimentos.Add(pItem);
            disciplinas item_disciplina = contextoEF.disciplinas.Where(x => x.id_disciplina == pItem.id_disciplina).SingleOrDefault();
            pItem.disciplinas = item_disciplina;
            contextoEF.SaveChanges();

            return true;
        }

        public bool AlteraPreOferecimento(pre_oferecimentos pItem)
        {
            pre_oferecimentos item = contextoEF.pre_oferecimentos.Where(x => x.id_pre_oferecimento == pItem.id_pre_oferecimento).SingleOrDefault();
            item.vagas = pItem.vagas;
            item.dia_semana = pItem.dia_semana;
            item.estado = pItem.estado;
            item.data_alteracao = pItem.data_alteracao;
            item.usuario = pItem.usuario;
            contextoEF.SaveChanges();
            return true;
        }

        public bool ExcluiPreOferecimento(pre_oferecimentos pItem)
        {
            pre_oferecimentos item = contextoEF.pre_oferecimentos.Where(x => x.id_pre_oferecimento == pItem.id_pre_oferecimento).SingleOrDefault();
            contextoEF.pre_oferecimentos.Remove(item);
            contextoEF.SaveChanges();
            return true;
        }

        public List<pre_oferecimentos> ListaPreOferecimento(periodo_matricula pItem)
        {
            var c = contextoEF.pre_oferecimentos.AsQueryable();
            List<pre_oferecimentos> lista = new List<pre_oferecimentos>();

            //DateTime dDataDefault = new DateTime();

            if (pItem.id_periodo != 0)
            {
                c = c.Where(x => x.id_periodo_matricula == pItem.id_periodo);
            }

            lista = c.ToList();

            return lista;
        }

        public pre_oferecimentos BuscaPreOferecimento(pre_oferecimentos pItem)
        {
            return contextoEF.pre_oferecimentos.Where(x => x.id_pre_oferecimento == pItem.id_pre_oferecimento).SingleOrDefault();
        }

        public List<pre_oferecimentos> ListaPreOferecimentoValidos(int qIdCurso)
        {
            var c = contextoEF.pre_oferecimentos.AsQueryable();
            List<pre_oferecimentos> lista = new List<pre_oferecimentos>();

            //DateTime dDataDefault = new DateTime();
            var qIdDisciplina = contextoEF.cursos_disciplinas.Where(x => x.id_curso == qIdCurso).Select(x => x.id_disciplina).ToArray();

            c = c.Where(x => x.periodo_matricula.data_inicio <= DateTime.Today && x.periodo_matricula.data_termino >= DateTime.Today && qIdDisciplina.Contains(x.id_disciplina));

            lista = c.ToList();

            return lista;
        }

        public bool VerificaAlunoCursouDisciplina(int qIdDisciplina, int qIdAluno, int qIdTurma, string qPeriodo)
        {
            //DateTime dDataDefault = new DateTime();

            var qOferecimento = contextoEF.oferecimentos.Where(x => x.id_disciplina == qIdDisciplina && x.quadrimestre != qPeriodo).Select(x => x.id_oferecimento).ToArray();

            var qlistaOfereciemntos = contextoEF.matricula_oferecimento.Where(x => qOferecimento.Contains(x.id_oferecimento) && x.id_aluno == qIdAluno && x.id_turma == qIdTurma).ToList();

            foreach (var elemento in qlistaOfereciemntos)
            {
                if (elemento.oferecimentos.disciplinas.nome.IndexOf("Acompanhamento da Dissertação") == -1)
                {
                    if (!elemento.oferecimentos.notas.Any(x => x.id_aluno == qIdAluno))
                    {
                        //não tem nota nenhuma, então retorna true pra não mostrar a disciplina para o aluno
                        return true;
                    }

                    string qNota = elemento.oferecimentos.notas.Where(x => x.id_aluno == qIdAluno).FirstOrDefault().conceito;

                    if (qNota == null)
                    {
                        return true;
                    }

                    if (qNota.ToUpper() != "D" && qNota.ToUpper() != "E" && qNota.ToUpper() != "I")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}
