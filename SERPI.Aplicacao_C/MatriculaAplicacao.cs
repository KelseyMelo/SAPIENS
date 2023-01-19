using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class MatriculaAplicacao
    {
        private readonly MatriculaRepositorio Repositorio = new MatriculaRepositorio();

        public periodo_matricula BuscaPeriodoMatricula(periodo_matricula pItem)
        {
            return Repositorio.BuscaPeriodoMatricula(pItem);
        }

        public matricula BuscaMatricula(matricula pItem)
        {
            return Repositorio.BuscaMatricula(pItem);
        }

        public matricula BuscaMatriculaSemId(matricula pItem)
        {
            return Repositorio.BuscaMatriculaSemId(pItem);
        }

        public periodo_matricula CriaPeriodoMatricula(periodo_matricula pItem)
        {
            return Repositorio.CriaPeriodoMatricula(pItem);
        }

        public matricula CriaMatricula(matricula pItem)
        {
            return Repositorio.CriaMatricula(pItem);
        }

        public bool CriaMatricula_PreOferecimento(matricula pItem_matricula, pre_oferecimentos pItem_pre_oferecimento)
        {
            return Repositorio.CriaMatricula_PreOferecimento(pItem_matricula, pItem_pre_oferecimento);
        }

        public bool ApagarMatricula_PreOferecimento(matricula pItem_matricula, pre_oferecimentos pItem_pre_oferecimento)
        {
            return Repositorio.ApagarMatricula_PreOferecimento(pItem_matricula, pItem_pre_oferecimento);
        }

        public periodo_matricula AlteraPeriodoMatricula(periodo_matricula pItem)
        {
            return Repositorio.AlteraPeriodoMatricula(pItem);
        }

        public bool ExcluiPeriodoMatricula(periodo_matricula pItem)
        {
            return Repositorio.ExcluiPeriodoMatricula(pItem);
        }

        public List<periodo_matricula> ListaItem(periodo_matricula pItem)
        {
            return Repositorio.ListaItem(pItem);
        }

        public List<disciplinas> ListaDisciplinasDisponiveis(periodo_matricula pItem, disciplinas pItemDisciplina)
        {
            return Repositorio.ListaDisciplinasDisponiveis(pItem, pItemDisciplina);
        }

        public bool CriaPreOferecimento(pre_oferecimentos pItem)
        {
            return Repositorio.CriaPreOferecimento(pItem);
        }

        public bool AlteraPreOferecimento(pre_oferecimentos pItem)
        {
            return Repositorio.AlteraPreOferecimento(pItem);
        }

        public bool ExcluiPreOferecimento(pre_oferecimentos pItem)
        {
            return Repositorio.ExcluiPreOferecimento(pItem);
        }

        public List<pre_oferecimentos> ListaPreOferecimento(periodo_matricula pItem)
        {
            return Repositorio.ListaPreOferecimento(pItem);
        }

        public pre_oferecimentos BuscaPreOferecimento(pre_oferecimentos pItem)
        {
            return Repositorio.BuscaPreOferecimento(pItem);
        }

        public List<pre_oferecimentos> ListaPreOferecimentoValidos(int qIdCurso)
        {
            return Repositorio.ListaPreOferecimentoValidos(qIdCurso);
        }

        public bool VerificaAlunoCursouDisciplina(int qIdDisciplina, int qIdAluno, int qIdTurma, string qPeriodo)
        {
            return Repositorio.VerificaAlunoCursouDisciplina(qIdDisciplina, qIdAluno, qIdTurma, qPeriodo);
        }
    }
}