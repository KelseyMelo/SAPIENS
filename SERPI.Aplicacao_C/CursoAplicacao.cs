
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class CursoAplicacao
    {
        private readonly CursoRepositorio Repositorio = new CursoRepositorio();

        public cursos BuscaItem(cursos pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public cursos VerificaItemMesmaSigla(cursos pItem)
        {
            return Repositorio.VerificaItemMesmaSigla(pItem);
        }

        public cursos VerificaItemMesmoTipoCurso_MesmoNome(cursos pItem)
        {
            return Repositorio.VerificaItemMesmoTipoCurso_MesmoNome(pItem);
        }

        public cursos CriarItem(cursos pItem)
        {
            return Repositorio.CriarItem(pItem);
        }

        public bool AlterarObrigatoriedade_Curso(cursos_disciplinas pItem)
        {
            return Repositorio.AlterarObrigatoriedade_Curso(pItem);
        }

        public Boolean AlterarItem(cursos pItem)
        {
            return Repositorio.AlterarItem(pItem);
        }

        public cursos AlterarStatus(cursos pItem)
        {
            return Repositorio.AlterarStatus(pItem);
        }

        public bool IncluirCoordenador_Curso(cursos_coordenadores pItem)
        {
            return Repositorio.IncluirCoordenador_Curso(pItem);
        }

        public bool IncluirDisciplina_Curso(cursos_disciplinas pItem)
        {
            return Repositorio.IncluirDisciplina_Curso(pItem);
        }
        public cursos ExcluirCoordenador(cursos_coordenadores pItem)
        {
            return Repositorio.ExcluirCoordenador_Curso(pItem);
        }

        public cursos ExcluirDisciplina(cursos_disciplinas pItem)
        {
            return Repositorio.ExcluirDisciplina_Curso(pItem);
        }

        public Boolean AlterarItem_Aprovacao(cursos pItem)
        {
            return Repositorio.AlterarItem_Aprovacao(pItem);
        }

        public Boolean AlterarItem_Reprovacao(cursos pItem)
        {
            return Repositorio.AlterarItem_Reprovacao(pItem);
        }

        public List<cursos> ListaItemAprovacaoHomePage()
        {
            return Repositorio.ListaItemAprovacaoHomePage();
        }

        public List<cursos> ListaItemReprovacaoHomePage(string qUsuario)
        {
            return Repositorio.ListaItemReprovacaoHomePage(qUsuario);
        }

        public List<tipos_curso> ListaTipoCurso()
        {
            return Repositorio.ListaTipoCurso();
        }

        public List<cursos> ListaItem(cursos pItem)
        {
            return Repositorio.ListaItem(pItem);
        }

        public List<cursos> ListaItem(turmas pItem)
        {
            return Repositorio.ListaItem(pItem);
        }

        public List<disciplinas> ListaDisciplinas(cursos pItem)
        {
            return Repositorio.ListaDisciplinas(pItem);
        }

        public List<cursos_disciplinas> ListaDisciplinasCursos(cursos pItem)
        {
            return Repositorio.ListaDisciplinasCursos(pItem);
        }

        public List<cursos_coordenadores> ListaCoordenadores(cursos pItem)
        {
            return Repositorio.ListaCoordenadores(pItem);
        }

        public List<professores> ListaProfessoresDisponiveis(cursos pItem, professores pItemProfessor)
        {
            return Repositorio.ListaProfessoresDisponiveis(pItem, pItemProfessor);
        }

        public List<disciplinas> ListaDisciplinasDisponiveis(cursos pItem, disciplinas pItemDisciplina)
        {
            return Repositorio.ListaDisciplinasDisponiveis(pItem, pItemDisciplina);
        }

        public List<cursos> ListaCursosHomepage(cursos pItem)
        {
            return Repositorio.ListaCursosHomepage(pItem);
        }

        public List<tipos_curso> ListaTipoCurso(tipos_curso pItem)
        {
            return Repositorio.ListaTipoCurso(pItem);
        }
       
    }
}

