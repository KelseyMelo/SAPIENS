using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class AreaAplicacao
    {
        private readonly AreaRepositorio Repositorio = new AreaRepositorio();

        public areas_concentracao BuscaItem(areas_concentracao pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public areas_concentracao CriarItem(areas_concentracao pItem)
        {
            return Repositorio.CriarItem(pItem);
        }

        public Boolean AlterarItem(areas_concentracao pItem)
        {
            return Repositorio.AlterarItem(pItem);
        }

        public areas_concentracao AlterarStatus(areas_concentracao pItem)
        {
            return Repositorio.AlterarStatus(pItem);
        }

        public List<areas_concentracao> ListaItem(areas_concentracao pItem)
        {
            return Repositorio.ListaItem(pItem);
        }

        public List<areas_concentracao_coordenadores> ListaCoordenadores(areas_concentracao pItem)
        {
            return Repositorio.ListaCoordenadores(pItem);
        }

        public List<professores> ListaCoordenadoresDisponiveis(areas_concentracao pItem, professores pItemProfessor)
        {
            return Repositorio.ListaCoordenadoresDisponiveis(pItem,pItemProfessor);
        }

        public bool IncluirCoordenador(areas_concentracao_coordenadores pItem)
        {
            return Repositorio.IncluirCoordenador(pItem);
        }

        public bool ExcluirCoordenador(areas_concentracao_coordenadores pItem)
        {
            return Repositorio.ExcluirCoordenador(pItem);
        }

        public Boolean CriarAreaConcentracaoDisciplina(areas_concentracao_disciplinas pItem)
        {
            return Repositorio.CriarAreaConcentracaoDisciplina(pItem);
        }

        public bool ExcluirAreaConcentracaoDisciplina(areas_concentracao pItem)
        {
            return Repositorio.ExcluirAreaConcentracaoDisciplina(pItem);
        }

    }
}
