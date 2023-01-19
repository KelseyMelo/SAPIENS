using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace Repositorio_C
{
    public class GrupoRepositorio : IDisposable
    {
        private Entities contextoEF;

        public GrupoRepositorio()
        {
            contextoEF = new Entities();
        }

        public grupos_acesso BuscaItem(grupos_acesso pItem)
        {
            grupos_acesso item = new grupos_acesso();
            item = contextoEF.grupos_acesso.Where(x => x.id_grupo == pItem.id_grupo).SingleOrDefault();
            return item;
        }

        public grupos_acesso VerificaItem_MesmoNome(grupos_acesso pItem)
        {
            grupos_acesso item = new grupos_acesso();
            item = contextoEF.grupos_acesso.Where(x => x.id_grupo != pItem.id_grupo && x.grupo == pItem.grupo).SingleOrDefault();
            return item;
        }

        public grupos_acesso CriarItem(grupos_acesso pItem)
        {
            contextoEF.grupos_acesso.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public grupos_acesso AlterarItem(grupos_acesso pItem)
        {
            grupos_acesso item = new grupos_acesso();
            item = contextoEF.grupos_acesso.Where(x => x.id_grupo == pItem.id_grupo).SingleOrDefault();
            item.grupo = pItem.grupo;
            item.descricao = pItem.descricao;
            contextoEF.SaveChanges();
            return item;
        }

        public grupos_acesso_telas_sistema AlterarItem_grupo_tela_sistema(grupos_acesso_telas_sistema pItem)
        {
            grupos_acesso_telas_sistema item = new grupos_acesso_telas_sistema();
            item = contextoEF.grupos_acesso_telas_sistema.Where(x => x.id_grupo == pItem.id_grupo && x.id_tela == pItem.id_tela).SingleOrDefault();
            item.leitura = pItem.leitura;
            item.escrita = pItem.escrita;
            contextoEF.SaveChanges();
            return item;
        }

        public void Excluir_grupo_tela_sistema(grupos_acesso_telas_sistema pItem)
        {
            grupos_acesso_telas_sistema item = new grupos_acesso_telas_sistema();
            item = contextoEF.grupos_acesso_telas_sistema.Where(x => x.id_grupo == pItem.id_grupo && x.id_tela == pItem.id_tela).SingleOrDefault();
            contextoEF.grupos_acesso_telas_sistema.Remove(item);
            contextoEF.SaveChanges();
        }

        public List<grupos_acesso> ListaItem(grupos_acesso pItem)
        {
            var c = contextoEF.grupos_acesso.AsQueryable();
            List<grupos_acesso> lista = new List<grupos_acesso>();

            if (pItem.id_grupo != 0)
            {
                c = c.Where(x => x.id_grupo == pItem.id_grupo);
            }

            if (pItem.grupo != null)
            {
                c = c.Where(x => x.grupo.Contains(pItem.grupo));
            }

            if (pItem.descricao != null)
            {
                c = c.Where(x => x.descricao.Contains(pItem.descricao));
            }

            lista = c.OrderBy(x => x.descricao).ToList();

            return lista;
        }

        public List<telas_sistema> ListaItemDisponiveis(grupos_acesso_telas_sistema pItem)
        {
            var c = contextoEF.telas_sistema.AsQueryable();
            List<telas_sistema> lista = new List<telas_sistema>();

            if (pItem.id_tela != 0)
            {
                c = c.Where(x => x.id_tela == pItem.id_tela);
            }

            if (pItem.telas_sistema.descricao_sapiens != "")
            {
                c = c.Where(x => x.descricao_sapiens.Contains(pItem.telas_sistema.descricao_sapiens));
            }

            if (pItem.telas_sistema.modulo_sapiens != "")
            {
                c = c.Where(x => x.modulo_sapiens == pItem.telas_sistema.modulo_sapiens);
            }

            var sAux = contextoEF.grupos_acesso_telas_sistema.Where(x => x.id_grupo == pItem.id_grupo).Select(x => x.id_tela).ToArray();
            c = c.Where(x => !sAux.Contains(x.id_tela));

            lista = c.ToList();

            return lista;
        }

        public bool CriarAssociacao(grupos_acesso_telas_sistema pItem)
        {
            contextoEF.grupos_acesso_telas_sistema.Add(pItem);
            contextoEF.SaveChanges();
            return true;
        }

        public bool ApagarAssociacao(grupos_acesso_telas_sistema pItem)
        {
            grupos_acesso_telas_sistema item = contextoEF.grupos_acesso_telas_sistema.Where(x => x.id_grupo == pItem.id_grupo && x.id_tela == pItem.id_tela).SingleOrDefault();

            contextoEF.grupos_acesso_telas_sistema.Remove(item);
            contextoEF.SaveChanges();
            return true;
        }

        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}


