using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace Repositorio_C
{
    public class DocumentosAcademicosRepositorio : IDisposable
    {
        private Entities contextoEF;

        public DocumentosAcademicosRepositorio()
        {
            contextoEF = new Entities();
        }

        public documentos_academicos BuscaItem(documentos_academicos pItem)
        {
            documentos_academicos item = new documentos_academicos();
            item = contextoEF.documentos_academicos.Where(x => x.id_documentos_academicos == pItem.id_documentos_academicos).SingleOrDefault();
            return item;
        }

        public documentos_academicos CriarItem(documentos_academicos pItem)
        {
            contextoEF.documentos_academicos.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public Boolean AlterarItem(documentos_academicos pItem)
        {
            documentos_academicos item = new documentos_academicos();
            item = contextoEF.documentos_academicos.Where(x => x.id_documentos_academicos == pItem.id_documentos_academicos).SingleOrDefault();

            item.nome_arquivoPreview = pItem.nome_arquivoPreview;
            item.nomePreview = pItem.nomePreview;
            item.descricaoPreview = pItem.descricaoPreview;
            item.tipo_arquivoPreview = pItem.tipo_arquivoPreview;
            item.id_tipo_cursoPreview = pItem.id_tipo_cursoPreview;
            item.data_alteracao = pItem.data_alteracao;
            item.status = pItem.status;
            item.ativo = pItem.ativo;
            item.usuario = pItem.usuario;
            item.statusAprovacao = pItem.statusAprovacao;
            item.usuarioAprovacao = pItem.usuarioAprovacao;
            item.data_aprovacao = pItem.data_aprovacao;
            item.data_reprovacao = pItem.data_reprovacao;
            contextoEF.SaveChanges();

            return true;
        }

        public List<documentos_academicos> ListaItemAguardandoAprovacao()
        {
            var c = contextoEF.documentos_academicos.AsQueryable();
            List<documentos_academicos> lista = new List<documentos_academicos>();

            c = c.Where(x => x.statusAprovacao == 0);

            lista = c.OrderBy(x => x.nomePreview).ToList();

            return lista;
        }

        public List<documentos_academicos> ListaItemReprovado(string qUsuario)
        {
            var c = contextoEF.documentos_academicos.AsQueryable();
            List<documentos_academicos> lista = new List<documentos_academicos>();

            c = c.Where(x => x.statusAprovacao == 2 && x.usuario == qUsuario);

            lista = c.OrderBy(x => x.nomePreview).ToList();

            return lista;
        }

        public documentos_academicos_obs CriarItem_Obs(documentos_academicos_obs pItem)
        {
            contextoEF.documentos_academicos_obs.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public Boolean AlterarItem_Obs(documentos_academicos_obs pItem)
        {
            documentos_academicos_obs item = new documentos_academicos_obs();
            item = contextoEF.documentos_academicos_obs.Where(x => x.id_documentos_academicos_obs == pItem.id_documentos_academicos_obs).SingleOrDefault();

            item.Observacao = pItem.Observacao;
            contextoEF.SaveChanges();
            return true;
        }

        public Boolean AlterarItem_Aprovacao(documentos_academicos pItem)
        {
            documentos_academicos item = new documentos_academicos();
            item = contextoEF.documentos_academicos.Where(x => x.id_documentos_academicos == pItem.id_documentos_academicos).SingleOrDefault();

            item.nome_arquivo = pItem.nome_arquivo;
            item.nome = pItem.nome;
            item.descricao = pItem.descricao;
            item.tipo_arquivo = pItem.tipo_arquivo;
            item.data_aprovacao = pItem.data_aprovacao;
            item.statusAprovacao = pItem.statusAprovacao;
            item.id_tipo_curso = pItem.id_tipo_curso;
            item.usuarioAprovacao = pItem.usuarioAprovacao;
            item.tipo_arquivo = pItem.tipo_arquivo;

            contextoEF.SaveChanges();
            return true;
        }

        public Boolean AlterarItem_Reprovacao(documentos_academicos pItem)
        {
            documentos_academicos item = new documentos_academicos();
            item = contextoEF.documentos_academicos.Where(x => x.id_documentos_academicos == pItem.id_documentos_academicos).SingleOrDefault();

            //===============
            //item.data_aprovacao = pItem.data_aprovacao;
            item.data_reprovacao = pItem.data_reprovacao;
            item.usuarioAprovacao = pItem.usuarioAprovacao;
            item.statusAprovacao = pItem.statusAprovacao;
            contextoEF.SaveChanges();
            return true;
        }

        public List<documentos_academicos> ListaItemAprovacaoHomePage()
        {
            var c = contextoEF.documentos_academicos.AsQueryable();
            List<documentos_academicos> lista = new List<documentos_academicos>();

            c = c.Where(x => x.statusAprovacao == 0);

            lista = c.OrderBy(x => x.nome).ToList();

            return lista;
        }

        public List<documentos_academicos> ListaItemReprovacaoHomePage(string qUsuario)
        {
            var c = contextoEF.documentos_academicos.AsQueryable();
            List<documentos_academicos> lista = new List<documentos_academicos>();

            c = c.Where(x => x.statusAprovacao == 2 && x.usuario == qUsuario && x.ativo != 0);

            lista = c.OrderBy(x => x.nome).ToList();

            return lista;
        }

        public documentos_academicos AlterarStatus(documentos_academicos pItem)
        {
            documentos_academicos item = new documentos_academicos();
            item = contextoEF.documentos_academicos.Where(x => x.id_documentos_academicos == pItem.id_documentos_academicos).SingleOrDefault();
            item.ativo = pItem.ativo;
            item.data_alteracao = DateTime.Now;
            item.usuario = pItem.usuario;
            contextoEF.SaveChanges();
            return item;
        }

        public List<documentos_academicos> ListaItem(documentos_academicos pItem)
        {
            var c = contextoEF.documentos_academicos.AsQueryable();
            List<documentos_academicos> lista = new List<documentos_academicos>();

            if (pItem.id_documentos_academicos != 0)
            {
                c = c.Where(x => x.id_documentos_academicos == pItem.id_documentos_academicos);
            }

            if (pItem.nome != null)
            {
                c = c.Where(x => x.nome.Contains(pItem.nome));
            }

            if (pItem.descricao != null)
            {
                c = c.Where(x => x.descricao.Contains(pItem.descricao));
            }

            if (pItem.tipo_arquivo != null)
            {
                c = c.Where(x => x.tipo_arquivo.Contains(pItem.tipo_arquivo));
            }

            if (pItem.ativo != null)
            {
                c = c.Where(x => x.ativo == pItem.ativo);
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
