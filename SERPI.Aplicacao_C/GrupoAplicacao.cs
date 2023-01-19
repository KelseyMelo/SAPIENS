
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class GrupoAplicacao
    {
        private readonly GrupoRepositorio Repositorio = new GrupoRepositorio();

        public grupos_acesso BuscaItem(grupos_acesso pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public grupos_acesso VerificaItem_MesmoNome(grupos_acesso pItem)
        {
            return Repositorio.VerificaItem_MesmoNome(pItem);
        }

        public grupos_acesso CriarItem(grupos_acesso pItem)
        {
            return Repositorio.CriarItem(pItem);
        }

        public grupos_acesso AlterarItem(grupos_acesso pItem)
        {
            return Repositorio.AlterarItem(pItem);
        }

        public grupos_acesso_telas_sistema AlterarItem_grupo_tela_sistema(grupos_acesso_telas_sistema pItem)
        {
            return Repositorio.AlterarItem_grupo_tela_sistema(pItem);
        }

        public void Excluir_grupo_tela_sistema(grupos_acesso_telas_sistema pItem)
        {
            Repositorio.Excluir_grupo_tela_sistema(pItem);
        }

        public List<grupos_acesso> ListaItem(grupos_acesso pItem)
        {
            return Repositorio.ListaItem(pItem);
        }

        public List<telas_sistema> ListaItemDisponiveis(grupos_acesso_telas_sistema pItem)
        {
            return Repositorio.ListaItemDisponiveis(pItem);
        }

        public bool CriarAssociacao(grupos_acesso_telas_sistema pItem)
        {
            return Repositorio.CriarAssociacao(pItem);
        }

        public bool ApagarAssociacao(grupos_acesso_telas_sistema pItem)
        {
            return Repositorio.ApagarAssociacao(pItem);
        }

    }
}

