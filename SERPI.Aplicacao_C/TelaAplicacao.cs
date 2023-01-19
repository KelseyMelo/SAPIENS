using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class TelaAplicacao
    {
        private readonly TelaRepositorio Repositorio = new TelaRepositorio();

        public telas_sistema BuscaItem(telas_sistema pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public telas_sistema CriarItem(telas_sistema pItem)
        {
            return Repositorio.CriarItem(pItem);
        }

        public bool AlterarItem(telas_sistema pItem)
        {
            return Repositorio.AlterarItem(pItem);
        }

        public grupos_acesso_telas_sistema AlterarPermissao(grupos_acesso_telas_sistema pItem)
        {
            return Repositorio.AlterarPermissao(pItem);
        }

        public List<telas_sistema> ListaItem(telas_sistema pItem, int qIdGrupo)
        {
            return Repositorio.ListaItem(pItem, qIdGrupo);
        }

        public bool ExcluirTela(telas_sistema pItem)
        {
            return Repositorio.ExcluirTela(pItem);
        }

        public List<grupos_acesso> ListaItemDisponiveis(grupos_acesso_telas_sistema pItem)
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


