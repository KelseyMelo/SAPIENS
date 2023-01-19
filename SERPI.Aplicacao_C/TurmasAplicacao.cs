using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class TurmasAplicacao
    {
        private readonly TurmasRepositorio Repositorio = new TurmasRepositorio();

        public turmas BuscaItem(turmas pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public turmas VerificaItemMesmoCodigo(turmas pItem)
        {
            return Repositorio.VerificaItemMesmoCodigo(pItem);
        }

        public string BuscaItem_NumeroMaximo(turmas pItem)
        {
            return Repositorio.BuscaItem_NumeroMaximo(pItem);
        }

        public turmas CriarItem(turmas pItem)
        {
            return Repositorio.CriarItem(pItem);
        }

        public Boolean AlterarItem(turmas pItem)
        {
            return Repositorio.AlterarItem(pItem);
        }

        public turmas AlterarStatus(turmas pItem)
        {
            return Repositorio.AlterarStatus(pItem);
        }

        public bool IncluirCoordenador_Turma(turmas_coordenadores pItem)
        {
            return Repositorio.IncluirCoordenador_Turma(pItem);
        }

        public bool IncluirDisciplina_Turma(turmas_disciplinas pItem)
        {
            return Repositorio.IncluirDisciplina_Turma(pItem);
        }
        public turmas ExcluirCoordenador(turmas_coordenadores pItem)
        {
            return Repositorio.ExcluirCoordenador_Turma(pItem);
        }

        public turmas ExcluirDisciplina(turmas_disciplinas pItem)
        {
            return Repositorio.ExcluirDisciplina_Turma(pItem);
        }

        public List<turmas> ListaItem(turmas pItem)
        {
            return Repositorio.ListaItem(pItem);
        }

        public List<turmas> ListaItemCombo(cursos pItem)
        {
            return Repositorio.ListaItemCombo(pItem);
        }

        public List<disciplinas> ListaDisciplinas(turmas pItem)
        {
            return Repositorio.ListaDisciplinas(pItem);
        }

        public List<turmas_coordenadores> ListaCoordenadores(turmas pItem)
        {
            return Repositorio.ListaCoordenadores(pItem);
        }

        public List<professores> ListaProfessoresDisponiveis(turmas pItem, professores pItemProfessor)
        {
            return Repositorio.ListaProfessoresDisponiveis(pItem, pItemProfessor);
        }

        public List<disciplinas> ListaDisciplinasDisponiveis(turmas pItem, disciplinas pItemDisciplina)
        {
            return Repositorio.ListaDisciplinasDisponiveis(pItem, pItemDisciplina);
        }

        public List<alunos> ListaAlunosDisponiveis(matricula_turma pItem_Matricula, alunos pItem_Aluno)
        {
            return Repositorio.ListaAlunosDisponiveis(pItem_Matricula, pItem_Aluno);
        }

        public Boolean AlterarAreaMatriculaTurma(matricula_turma pItem)
        {
            return Repositorio.AlterarAreaMatriculaTurma(pItem);
        }

        public List<matricula_oferecimento> Lista_VerificaAlunoMatriculadoOferecimento(matricula_turma pItem)
        {
            return Repositorio.Lista_VerificaAlunoMatriculadoOferecimento(pItem);
        }

        public Boolean ExcluirMatriculaAlunoTurma(matricula_turma pItem)
        {
            return Repositorio.ExcluirMatriculaAlunoTurma(pItem);
        }
         
        public matricula_turma IncluirAluno_Turma(matricula_turma pItem)
        {
            return Repositorio.IncluirAluno_Turma(pItem);
        }

        public alunos_dataLimite_documentos_pendentes IncluirAluno_DataLimiteDocumento(alunos_dataLimite_documentos_pendentes pItem)
        {
            return Repositorio.IncluirAluno_DataLimiteDocumento(pItem);
        }

        public historico_matricula_turma IncluirHistorico_Matricula(historico_matricula_turma pItem)
        {
            return Repositorio.IncluirHistorico_Matricula(pItem);
        }

    }
}


