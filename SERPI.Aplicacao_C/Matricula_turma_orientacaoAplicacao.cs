using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class Matricula_turma_orientacaoAplicacao
    {
        private readonly matricula_turma_orientacaoRepositorio Repositorio = new matricula_turma_orientacaoRepositorio();

        public matricula_turma_orientacao BuscaItem(matricula_turma_orientacao pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public matricula_turma_orientacao CriarItem(matricula_turma_orientacao pItem)
        {
            return Repositorio.CriarItem(pItem);
        }

        //public Boolean AlterarItem(matricula_turma_orientacao pItem)
        //{
        //    return Repositorio.al(pItem);
        //}

        public Boolean DeletaItem(int pid_orientacao)
        {
            return Repositorio.DeletaItem(pid_orientacao);
        }
            
        public List<matricula_turma_orientacao> ListaItem(matricula_turma pItem)
        {
            return Repositorio.ListaItem(pItem);
        } 
    }
}
