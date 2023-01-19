using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class FornecedorAplicacao
    {
        private readonly FornecedoresRepositorio Repositorio = new FornecedoresRepositorio();

        public fornecedores BuscaItem(int qIdFornecedor)
        {
            return Repositorio.BuscaItem(qIdFornecedor);
        }

        public fornecedores BuscaItem(int qIdFornecedor, string qCNPJ)
        {
            return Repositorio.BuscaItem(qIdFornecedor, qCNPJ);
        }

        public fornecedores CriarItem(fornecedores pItem)
        {
            return Repositorio.CriarItem(pItem);
        }

        public Boolean AlterarItem(fornecedores pItem)
        {
            return Repositorio.AlterarItem(pItem);
        }

        public List<fornecedores> ListaItem(fornecedores pItem)
        {
            return Repositorio.ListaItem(pItem);
        }

    }
}
