using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class TipoCursoAplicacao
    {
        private readonly TipoCursoRepositorio Repositorio = new TipoCursoRepositorio();

        public tipos_curso BuscaItem(tipos_curso pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }


        public tipos_curso VerificaItemMesmoTipoCurso_MesmoNome(tipos_curso pItem)
        {
            return Repositorio.VerificaItemMesmoTipoCurso_MesmoNome(pItem);
        }

        public tipos_curso CriarItem(tipos_curso pItem)
        {
            return Repositorio.CriarItem(pItem);
        }

        public tipos_curso AlterarItem(tipos_curso pItem)
        {
            return Repositorio.AlterarItem(pItem);
        }

        public Boolean AlterarItem_Aprovacao(tipos_curso pItem)
        {
            return Repositorio.AlterarItem_Aprovacao(pItem);
        }

        public Boolean AlterarItem_Reprovacao(tipos_curso pItem)
        {
            return Repositorio.AlterarItem_Reprovacao(pItem);
        }

        public List<tipos_curso> ListaItemAprovacaoHomePage()
        {
            return Repositorio.ListaItemAprovacaoHomePage();
        }

        public List<tipos_curso> ListaItemReprovacaoHomePage(string qUsuario)
        {
            return Repositorio.ListaItemReprovacaoHomePage(qUsuario);
        }

        public List<tipos_curso> ListaItem(tipos_curso pItem)
        {
            return Repositorio.ListaItem(pItem);
        }

    }
}

