using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class InadimplenteAplicacao
    {
        private readonly InadimplenteRepositorio Repositorio = new InadimplenteRepositorio();

        public inadimplentes BuscaItem(int qIdAluno)
        {
            return Repositorio.BuscaItem(qIdAluno);
        }

        public inadimplentes CriarItem(inadimplentes pItem)
        {
            return Repositorio.CriarItem(pItem);
        }

        public bool CriarTodos(IGrouping<decimal, decimal?>[] sAux, string usuario)
        {
            return Repositorio.CriarTodos(sAux, usuario);
        }

        public List<inadimplentes> ListaItem(inadimplentes pItem)
        {
            return Repositorio.ListaItem(pItem);
        }

        public List<alunos> ListaAlunosDisponiveis(alunos pItem)
        {
            return Repositorio.ListaAlunosDisponiveis(pItem);
        }

        public bool ApagarItem(int qIdAluno)
        {
            return Repositorio.ApagarItem(qIdAluno);
        }

        public bool ApagarTodos()
        {
            return Repositorio.ApagarTodos();
        }

    }
}


