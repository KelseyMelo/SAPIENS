
using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace Repositorio_C
{
    public class ArquivoRepositorio : IDisposable
    {
        private Entities contextoEF;

        public ArquivoRepositorio()
        {
            contextoEF = new Entities();
        }

        public arquivos BuscaItem(arquivos pItem)
        {
            arquivos item = new arquivos();
            item = contextoEF.arquivos.Where(x => x.id_arquivo == pItem.id_arquivo).SingleOrDefault();
            return item;
        }

        public arquivos CriarItem(arquivos pItem)
        {
            contextoEF.arquivos.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public Boolean AlterarItem(arquivos pItem)
        {
            arquivos item = new arquivos();
            item = contextoEF.arquivos.Where(x => x.id_arquivo == pItem.id_arquivo).SingleOrDefault();

            item.descricao = pItem.descricao;
            item.nome_arquivo = pItem.nome_arquivo;
            item.tipo_arquivo = pItem.tipo_arquivo;
            item.data_alteracao = pItem.data_alteracao;
            item.status = pItem.status;
            item.usuario = pItem.usuario;

            contextoEF.SaveChanges();
            return true;
        }

        public arquivos AlterarStatus(arquivos pItem)
        {
            arquivos item = new arquivos();
            item = contextoEF.arquivos.Where(x => x.id_arquivo == pItem.id_arquivo).SingleOrDefault();
            item.ativo = pItem.ativo;
            item.data_alteracao = DateTime.Now;
            item.usuario = pItem.usuario;
            contextoEF.SaveChanges();
            item = contextoEF.arquivos.Where(x => x.id_arquivo == pItem.id_arquivo).SingleOrDefault();
            return item;
        }

        public List<arquivos> ListaItem(arquivos pItem)
        {
            var c = contextoEF.arquivos.AsQueryable();
            List<arquivos> lista = new List<arquivos>();

            if (pItem.id_arquivo != 0)
            {
                c = c.Where(x => x.id_arquivo == pItem.id_arquivo);
            }

            if (pItem.nome_arquivo != null)
            {
                c = c.Where(x => x.nome_arquivo.Contains(pItem.nome_arquivo));
            }

            if (pItem.descricao != null)
            {
                c = c.Where(x => x.descricao.Contains(pItem.descricao));
            }

            if (pItem.tipo_arquivo != null)
            {
                c = c.Where(x => x.tipo_arquivo == pItem.tipo_arquivo);
            }

            c = c.Where(x => x.ativo == 1);

            lista = c.OrderBy(x => x.nome_arquivo).ToList();

            return lista;
        }

        public List<tipos_curso> ListaTipoCurso_comArquivo(arquivos pItem)
        {
            var c = contextoEF.tipos_curso.AsQueryable();
            List<tipos_curso> lista = new List<tipos_curso>();

            c = c.Where(x => x.descricao_homepage.Contains(pItem.nome_arquivo));
            
            lista = c.OrderBy(x => x.tipo_curso).ToList();

            return lista;
        }

        public List<cursos> ListaCurso_comArquivo(arquivos pItem)
        {
            var c = contextoEF.cursos.AsQueryable();
            List<cursos> lista = new List<cursos>();

            c = c.Where(x => x.descricao_homepage.Contains(pItem.nome_arquivo));

            lista = c.OrderBy(x => x.nome).ToList();

            return lista;
        }

        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}
