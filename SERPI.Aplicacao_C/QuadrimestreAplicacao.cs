using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class QuadrimestreAplicacao
    {
        private readonly QuadrimestreRepositorio Repositorio = new QuadrimestreRepositorio();

        public quadrimestres BuscaItem(quadrimestres pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public int BuscaItem_NumeroMaximo(quadrimestres pItem)
        {
            return Repositorio.BuscaItem_NumeroMaximo(pItem);
        }

        public quadrimestres CriarItem(quadrimestres pItem)
        {
            return Repositorio.CriarItem(pItem);
        }

        public quadrimestres AlterarStatus(quadrimestres pItem)
        {
            return Repositorio.AlterarStatus(pItem);
        }

        public Boolean AlterarItem(quadrimestres pItem)
        {
            return Repositorio.AlterarItem(pItem);
        }

        public List<quadrimestres> ListaItem(quadrimestres pItem)
        {
            return Repositorio.ListaItem(pItem);
        }

        public List<quadrimestres> ListaItem()
        {
            return Repositorio.ListaItem();
        }

        ///<summary>
        ///Esa é a descrição do método.
        ///</summary>
        public List<quadrimestres> ListaItem(cursos pItem)
        {
            return Repositorio.ListaItem(pItem);
        }

        public List<quadrimestres> ListaItem(List<cursos> lista_curso)
        {
            return Repositorio.ListaItem(lista_curso);
        }

        ///<summary>
        ///Listar todos os períodos de um determinado Tipo de Curso
        ///</summary>
        public List<quadrimestres> ListaItem(tipos_curso pItem)
        {
            return Repositorio.ListaItem(pItem);
        }
    }
}

