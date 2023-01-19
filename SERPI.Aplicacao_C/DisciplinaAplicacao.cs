
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class DisciplinaAplicacao
    {
        private readonly DisciplinaRepositorio Repositorio = new DisciplinaRepositorio();

        public disciplinas BuscaItem(disciplinas pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public disciplinas VerificaItemMesmoCodigo(disciplinas pItem)
        {
            return Repositorio.VerificaItemMesmoCodigo(pItem);
        }

        public disciplinas CriarItem(disciplinas pItem)
        {
            return Repositorio.CriarItem(pItem);
        }

        public disciplinas AlterarStatus(disciplinas pItem)
        {
            return Repositorio.AlterarStatus(pItem);
        }

        public Boolean AlterarItem(disciplinas pItem)
        {
            return Repositorio.AlterarItem(pItem);
        }

        public List<disciplinas> ListaItem(disciplinas pItem)
        {
            return Repositorio.ListaItem(pItem);
        }

        public List<disciplinas_professores> ListaProfessores(disciplinas pItem)
        {
            return Repositorio.ListaProfessores(pItem);
        }

        public List<disciplinas_professores> ListaTecnicos(disciplinas pItem)
        {
            return Repositorio.ListaTecnicos(pItem);
        }

        public List<professores> ListaProfessoresDisponiveis(disciplinas_professores pItem, professores pItemProfessor)
        {
            return Repositorio.ListaProfessoresDisponiveis(pItem, pItemProfessor);
        }

        public bool IncluirProfessor_Tecnico_Disciplina(disciplinas_professores pItem)
        {
            return Repositorio.IncluirProfessor_Tecnico_Disciplina(pItem);
        }

        public bool ExcluirProfessor_Tecnico_Disciplina(disciplinas_professores pItem)
        {
            return Repositorio.ExcluirProfessor_Tecnico_Disciplina(pItem);
        }

        public bool AlterarProfessor_Tecnico_Disciplina(disciplinas_professores pItem)
        {
            return Repositorio.AlterarProfessor_Tecnico_Disciplina(pItem);
        }

        public bool AlterarResponsavelProfessor_Tecnico_Disciplina(disciplinas_professores pItem)
        {
            return Repositorio.AlterarResponsavelProfessor_Tecnico_Disciplina(pItem);
        }

        public Boolean CriarDisciplina_Requisito(disciplinas_requisitos pItem)
        {
            return Repositorio.CriarDisciplina_Requisito(pItem);
        }

        public bool ExcluirDisciplinas_requisitos(disciplinas pItem)
        {
            return Repositorio.ExcluirDisciplinas_requisitos(pItem);
        }

        public List<disciplinas> ListaPrerequisitoDisponiveis(disciplinas pItem, decimal[] aIdDisciplina)
        {
            return Repositorio.ListaPrerequisitoDisponiveis(pItem, aIdDisciplina);
        }

        public bool IncluirPrerequisito_Disciplina(disciplinas_requisitos pItem)
        {
            return Repositorio.IncluirPrerequisito_Disciplina(pItem);
        }

        public bool ExcluirPrerequisito_Disciplina(disciplinas_requisitos pItem)
        {
            return Repositorio.ExcluirPrerequisito_Disciplina(pItem);
        }

    }
}
