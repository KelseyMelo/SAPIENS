
using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace Repositorio_C
{
    public class FornecedoresRepositorio : IDisposable
    {
        private Entities contextoEF;

        public FornecedoresRepositorio()
        {
            contextoEF = new Entities();
        }

        public fornecedores BuscaItem(int qIdFornecedor)
        {
            fornecedores item = new fornecedores();
            item = contextoEF.fornecedores.Where(x => x.id_fornecedor == qIdFornecedor).SingleOrDefault();
            return item;
        }

        public fornecedores BuscaItem(int qIdFornecedor, string qCNPJ)
        {
            fornecedores item = new fornecedores();
            item = contextoEF.fornecedores.Where(x => x.cnpj == qCNPJ  && x.id_fornecedor != qIdFornecedor).SingleOrDefault();
            return item;
        }

        public fornecedores CriarItem(fornecedores pItem)
        {
            contextoEF.fornecedores.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public Boolean AlterarItem(fornecedores pItem)
        {
            fornecedores item = new fornecedores();
            item = contextoEF.fornecedores.Where(x => x.id_fornecedor == pItem.id_fornecedor).SingleOrDefault();
            item.nome = pItem.nome;
            item.cnpj = pItem.cnpj;
            item.inscricao_estadual = pItem.inscricao_estadual;
            item.logradouro_end = pItem.logradouro_end;
            item.numero_end = pItem.numero_end;
            item.comp_end = pItem.comp_end;
            item.bairro_end = pItem.bairro_end;
            item.cidade_end = pItem.cidade_end;
            item.uf_end = pItem.uf_end;
            item.cep_end = pItem.cep_end;
            item.nome_contato = pItem.nome_contato;
            item.cargo = pItem.cargo;
            item.tel_contato = pItem.tel_contato;
            item.fax_contato = pItem.fax_contato;
            item.status = "alterado";
            //item.data_cadastro = pItem.data_cadastro;
            item.data_alteracao = DateTime.Now;
            item.usuario = pItem.usuario;
            item.cel_contato = pItem.cel_contato;
            item.email_contato = pItem.email_contato;
            item.numero_banco = pItem.numero_banco;
            item.nome_banco = pItem.nome_banco;
            item.agencia_numero = pItem.agencia_numero;
            item.conta_numero = pItem.conta_numero;
            item.endereco_agencia = pItem.endereco_agencia;
            contextoEF.SaveChanges();
            return true;
        }

        public List<fornecedores> ListaItem(fornecedores pItem)
        {
            var c = contextoEF.fornecedores.AsQueryable();
            List<fornecedores> lista = new List<fornecedores>();

            if (pItem.id_fornecedor != 0)
            {
                c = c.Where(x => x.id_fornecedor == pItem.id_fornecedor);
            }

            if (pItem.nome != "" && pItem.nome != null)
            {
                c = c.Where(x => x.nome.Contains(pItem.nome));
            }

            if (pItem.cnpj != "" && pItem.cnpj != null)
            {
                c = c.Where(x => x.cnpj == pItem.cnpj);
            }

            lista = c.OrderBy(x => x.nome).ToList();

            return lista;
        }


        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}


