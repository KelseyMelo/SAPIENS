using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class OferecimentoAplicacao
    {
        private readonly OferecimentoRepositorio Repositorio = new OferecimentoRepositorio();

        public oferecimentos BuscaItem(oferecimentos pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public matricula_oferecimento BuscaMatriculaOferecimento(matricula_oferecimento pItem)
        {
            return Repositorio.BuscaMatriculaOferecimento(pItem);
        }

        public int BuscaNumeroOferecimento(oferecimentos pItem)
        {
            return Repositorio.BuscaNumeroOferecimento(pItem);
        }

        public oferecimentos CriarItem(oferecimentos pItem)
        {
            return Repositorio.CriarItem(pItem);
        }

        public oferecimentos AlterarStatus(oferecimentos pItem)
        {
            return Repositorio.AlterarStatus(pItem);
        }

        public Boolean AlterarItem(oferecimentos pItem)
        {
            return Repositorio.AlterarItem(pItem);
        }

        public List<oferecimentos> ListaItem(oferecimentos pItem, decimal idProfessor, DateTime sDataInicio, DateTime sDataFim)
        {
            return Repositorio.ListaItem(pItem, idProfessor, sDataInicio, sDataFim);
        }

        public List<oferecimentos_professores> ListaProfessores(oferecimentos pItem)
        {
            return Repositorio.ListaProfessores(pItem);
        }

        public List<oferecimentos_professores> ListaTecnicos(oferecimentos pItem)
        {
            return Repositorio.ListaTecnicos(pItem);
        }

        public List<oferecimentos> Oferecimentos_nObrigatorios_cursados_aluno(matricula_oferecimento pItem, int qIdCurso)
        {
            return Repositorio.Oferecimentos_nObrigatorios_cursados_aluno(pItem, qIdCurso);
        }

        public List<oferecimentos> Disciplina_cursada_aluno(matricula_oferecimento pItem, int qIdDisciplina)
        {
            return Repositorio.Disciplina_cursada_aluno(pItem, qIdDisciplina);
        }

        public List<professores> ListaProfessoresDisponiveis(oferecimentos_professores pItem, professores pItemProfessor)
        {
            return Repositorio.ListaProfessoresDisponiveis(pItem, pItemProfessor);
        }

        public bool IncluirProfessor_Tecnico_Oferecimento(oferecimentos_professores pItem)
        {
            return Repositorio.IncluirProfessor_Tecnico_Oferecimento(pItem);
        }

        public bool ExcluirProfessor_Tecnico_Oferecimento(oferecimentos_professores pItem)
        {
            return Repositorio.ExcluirProfessor_Tecnico_Oferecimento(pItem);
        }

        public bool AlterarProfessor_Tecnico_Oferecimento(oferecimentos_professores pItem)
        {
            return Repositorio.AlterarProfessor_Tecnico_Oferecimento(pItem);
        }

        public bool AlterarResponsavelProfessor_Tecnico_Oferecimento(oferecimentos_professores pItem)
        {
            return Repositorio.AlterarResponsavelProfessor_Tecnico_Oferecimento(pItem);
        }

        public datas_aulas Criar_datas_aulas(datas_aulas pItem)
        {
            return Repositorio.Criar_datas_aulas(pItem);
        }

        public bool Criar_datas_aulas_professor(datas_aulas_professor pItem)
        {
            return Repositorio.Criar_datas_aulas_professor(pItem);
        }

        public bool Criar_presenca_professor(presenca_professor pItem)
        {
            return Repositorio.Criar_presenca_professor(pItem);
        }

        public bool Alterar_datas_aulas(datas_aulas pItem)
        {
            return Repositorio.Alterar_datas_aulas(pItem);
        }

        public bool Excluir_presenca_professor(presenca_professor pItem)
        {
            return Repositorio.Excluir_presenca_professor(pItem);
        }

        public bool Excluir_datas_aulas_professor(datas_aulas_professor pItem)
        {
            return Repositorio.Excluir_datas_aulas_professor(pItem);
        }

        public bool Excluir_datas_aulas(datas_aulas pItem)
        {
            return Repositorio.Excluir_datas_aulas(pItem);
        }

        public bool Excluir_presenca(presenca pItem)
        {
            return Repositorio.Excluir_presenca(pItem);
        }

        public bool Excluir_Aluno_matricula_oferecimento(int qIdAluno, int qIdMatricula, int qIdOferecimento)
        {
            return Repositorio.Excluir_Aluno_matricula_oferecimento(qIdAluno, qIdMatricula, qIdOferecimento);
        }

        public bool Alterar_PresencaProfessor(presenca_professor pItem)
        {
            return Repositorio.Alterar_PresencaProfessor(pItem);
        }

        public bool Excluir_Equipe(datas_aulas_professor pItem)
        {
            return Repositorio.Excluir_Equipe(pItem);
        }

        public bool Alterar_HoraEquipe(datas_aulas_professor pItem)
        {
            return Repositorio.Alterar_HoraEquipe(pItem);
        }

        public List<professores> ListaEquipeDisponiveis_Aula(datas_aulas_professor pItem, oferecimentos_professores pItem2)
        {
            return Repositorio.ListaEquipeDisponiveis_Aula(pItem, pItem2);
        }

        public List<alunos> ListaAlunosDisponiveis(oferecimentos pItem_Oferecimento, alunos pItem_Aluno, int qIdTipoCurso, int qIdCurso, int qIdTurma)
        {
            return Repositorio.ListaAlunosDisponiveis(pItem_Oferecimento, pItem_Aluno, qIdTipoCurso, qIdCurso, qIdTurma);
        }

        public Boolean MatricularAluno(matricula_oferecimento pItem, notas pItem_Nota)
        {
            return Repositorio.MatricularAluno(pItem, pItem_Nota);
        }

        public Boolean ExcluirMatriculaAluno(matricula_oferecimento pItem)
        {
            return Repositorio.ExcluirMatriculaAluno(pItem);
        }

        public Boolean EditarNota(notas pItem)
        {
            return Repositorio.EditarNota(pItem);
        }

        public Boolean CriarNota(notas pItem)
        {
            return Repositorio.CriarNota(pItem);
        }

        public Boolean CriarPresenca(presenca pItem)
        {
            return Repositorio.CriarPresenca(pItem);
        }

        public Boolean AlterarPresenca(presenca pItem)
        {
            return Repositorio.AlterarPresenca(pItem);
        }

        public List<oferecimentos> ListaItemCombo()
        {
            return Repositorio.ListaItemCombo();
        }

        public List<oferecimentos> ListaItemCombo(List<cursos> lista_curso)
        {
            return Repositorio.ListaItemCombo(lista_curso);
        }

        public List<oferecimentos> ListaItemCombo(cursos pItem, turmas pItem_turma)
        {
            return Repositorio.ListaItemCombo(pItem, pItem_turma);
        }

    }
}

