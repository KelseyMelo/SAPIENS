using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Repositorio_C
{
    public class InadimplenteRepositorio : IDisposable
    {
        private Entities contextoEF;

        public InadimplenteRepositorio()
        {
            contextoEF = new Entities();
        }

        public inadimplentes BuscaItem(int qIdAluno)
        {
            inadimplentes item = new inadimplentes();
            item = contextoEF.inadimplentes.Where(x => x.id_aluno == qIdAluno).SingleOrDefault();
            return item;
        }

        public inadimplentes CriarItem(inadimplentes pItem)
        {
            contextoEF.inadimplentes.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public bool CriarTodos(IGrouping<decimal, decimal?>[] sAux, string usuario)
        {
            using (var contextoEF = new Entities())
            {
                inadimplentes item;
                foreach (var elemento in sAux)
                {
                    item = new inadimplentes();
                    item.id_aluno = Convert.ToDecimal(elemento.Key);
                    item.data = DateTime.Now;
                    item.usuario = usuario;
                    contextoEF.inadimplentes.Add(item);
                    contextoEF.SaveChanges();
                }
            }
            return true;
        }

        public List<inadimplentes> ListaItem(inadimplentes pItem)
        {
            var c = contextoEF.inadimplentes.AsQueryable();
            List<inadimplentes> lista = new List<inadimplentes>();

            if (pItem.id_aluno != 0)
            {
                c = c.Where(x => x.id_aluno == pItem.id_aluno);
            }

            if (pItem.alunos.nome != "")
            {
                c = c.Where(x => x.alunos.nome.Contains(pItem.alunos.nome));
            }

            lista = c.OrderBy(x => x.alunos.nome).ToList();

            return lista;
        }

        public List<alunos> ListaAlunosDisponiveis(alunos pItem)
        {
            var c = contextoEF.alunos.AsQueryable();
            List<alunos> lista = new List<alunos>();

            if (pItem.idaluno != 0)
            {
                c = c.Where(x => x.idaluno == pItem.idaluno);
            }

            if (pItem.nome != "")
            {
                c = c.Where(x => x.nome.Contains(pItem.nome));
            }

            var qIdAluno = contextoEF.inadimplentes.Select(x => x.id_aluno).ToArray();

            lista = c.Where(x=> !qIdAluno.Contains(x.idaluno)).OrderBy(x => x.nome).ToList();

            return lista;
        }

        public bool ApagarItem(int qIdAluno)
        {
            inadimplentes item = new inadimplentes();
            item = contextoEF.inadimplentes.Where(x => x.id_aluno == qIdAluno).SingleOrDefault();
            contextoEF.inadimplentes.Remove(item);
            contextoEF.SaveChanges();
            return true;
        }

        public bool ApagarTodos()
        {
            contextoEF.inadimplentes.RemoveRange(contextoEF.inadimplentes);
            contextoEF.SaveChanges();
            return true;
        }

        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}

