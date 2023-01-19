using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class ArquivoAplicacao
    {
        private readonly ArquivoRepositorio Repositorio = new ArquivoRepositorio();

        public arquivos BuscaItem(arquivos pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public arquivos CriarItem(arquivos pItem)
        {
            return Repositorio.CriarItem(pItem);
        }

        public Boolean AlterarItem(arquivos pItem)
        {
            return Repositorio.AlterarItem(pItem);
        }

        public arquivos AlterarStatus(arquivos pItem)
        {
            return Repositorio.AlterarStatus(pItem);
        }

        public List<arquivos> ListaItem(arquivos pItem)
        {
            return Repositorio.ListaItem(pItem);
        }

        public List<tipos_curso> ListaTipoCurso_comArquivo(arquivos pItem)
        {
            return Repositorio.ListaTipoCurso_comArquivo(pItem);
        }

        public List<cursos> ListaCurso_comArquivo(arquivos pItem)
        {
            return Repositorio.ListaCurso_comArquivo(pItem);
        }
    }
}
