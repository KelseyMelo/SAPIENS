using System;
using SERPI.Dominio_C;
using System.Collections.Generic;
using System.Linq;


namespace Repositorio_C
{
    public class matricula_turma_orientacaoRepositorio : IDisposable

    {
        private Entities contextoEF;

        public matricula_turma_orientacaoRepositorio()
        {
            contextoEF = new Entities();
        }

        public matricula_turma_orientacao BuscaItem(matricula_turma_orientacao pItem)
        {
            return contextoEF.matricula_turma_orientacao.Where(x => x.id_orientacao == pItem.id_orientacao).SingleOrDefault();
        }

        public matricula_turma_orientacao CriarItem(matricula_turma_orientacao pItem)
        {
            contextoEF.matricula_turma_orientacao.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public Boolean DeletaItem(int pid_orientacao)
        {
            //List<matricula_turma_orientacao> Lista = new List<matricula_turma_orientacao>();
            //Lista = contextoEF.matricula_turma_orientacao.Where(x => x.id_orientacao == pItem.id_orientacao).ToList();
            //contextoEF.matricula_turma_orientacao.RemoveRange(Lista);
            matricula_turma_orientacao item = new matricula_turma_orientacao();
            item = contextoEF.matricula_turma_orientacao.Where(x => x.id_orientacao == pid_orientacao).SingleOrDefault();
            contextoEF.matricula_turma_orientacao.Remove(item);
            contextoEF.SaveChanges();
            return true; 
        }

        public List<matricula_turma_orientacao> ListaItem(matricula_turma pItem)
        {
            var c = contextoEF.matricula_turma_orientacao.Include("professores").AsQueryable();
            List<matricula_turma_orientacao> lista = new List<matricula_turma_orientacao>();

            if (pItem.id_matricula_turma != 0)
            {
                c = c.Where(x => x.id_matricula_turma == pItem.id_matricula_turma);
            }

            lista = c.OrderBy(x => x.tipo_orientacao).ToList();

            return lista;
        }

        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}
