
using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace Repositorio_C
{
    public class TelaRepositorio : IDisposable
    {
        private Entities contextoEF;

        public TelaRepositorio()
        {
            contextoEF = new Entities();
        }

        public telas_sistema BuscaItem(telas_sistema pItem)
        {
            telas_sistema item = new telas_sistema();
            item = contextoEF.telas_sistema.Where(x => x.id_tela == pItem.id_tela).SingleOrDefault();
            return item;
        }

        public telas_sistema CriarItem(telas_sistema pItem)
        {
            contextoEF.telas_sistema.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public Boolean AlterarItem(telas_sistema pItem)
        {
            telas_sistema item = new telas_sistema();
            item = contextoEF.telas_sistema.Include(x => x.grupos_acesso_telas_sistema).Where(x => x.id_tela == pItem.id_tela).SingleOrDefault();

            item.tela = pItem.tela;
            item.descricao = pItem.descricao;
            item.modulo = pItem.modulo;
            item.modulo_sapiens = pItem.modulo_sapiens;
            item.descricao_sapiens = pItem.descricao_sapiens;
            item.status = pItem.status;

            contextoEF.SaveChanges();
            return true;
        }

        public grupos_acesso_telas_sistema AlterarPermissao(grupos_acesso_telas_sistema pItem)
        {
            grupos_acesso_telas_sistema item = new grupos_acesso_telas_sistema();
            item = contextoEF.grupos_acesso_telas_sistema.Where(x => x.id_tela == pItem.id_tela && x.id_grupo == pItem.id_grupo).SingleOrDefault();
            item.leitura = pItem.leitura;
            item.escrita = pItem.escrita;
            item.modificacao = pItem.modificacao;
            item.exclusao = pItem.exclusao;
            contextoEF.SaveChanges();
            return item;
        }

        public List<telas_sistema> ListaItem(telas_sistema pItem, int qIdGrupo)
        {
            var c = contextoEF.telas_sistema.AsQueryable();
            List<telas_sistema> lista = new List<telas_sistema>();

            if (pItem.id_tela != 0)
            {
                c = c.Where(x => x.id_tela == pItem.id_tela);
            }

            if (pItem.tela != null)
            {
                c = c.Where(x => x.tela.Contains(pItem.tela));
            }

            if (pItem.descricao != null)
            {
                c = c.Where(x => x.descricao.Contains(pItem.descricao));
            }

            if (qIdGrupo != 0)
            {
                var qAux = contextoEF.grupos_acesso_telas_sistema.Where(x => x.id_grupo == qIdGrupo).Select(x => x.id_tela).ToArray();
                c = c.Where(x => qAux.Contains(x.id_tela));
            }

            if (pItem.modulo_sapiens != null)
            {
                c = c.Where(x => x.modulo_sapiens == pItem.modulo_sapiens);
            }

            if (pItem.descricao_sapiens != null)
            {
                c = c.Where(x => x.descricao_sapiens.Contains(pItem.descricao_sapiens));
            }

            if (pItem.status != 2)
            {
                c = c.Where(x => x.status == pItem.status);
            }

            lista = c.Include(x => x.grupos_acesso_telas_sistema).OrderBy(x => x.tela).ToList();

            return lista;
        }

        public bool ExcluirTela(telas_sistema pItem)
        {
            telas_sistema item = new telas_sistema();
            item = contextoEF.telas_sistema.Where(x => x.id_tela == pItem.id_tela).SingleOrDefault();
            contextoEF.telas_sistema.Remove(pItem);
            contextoEF.SaveChanges();
            return true;
        }

        public List<grupos_acesso> ListaItemDisponiveis(grupos_acesso_telas_sistema pItem)
        {
            var c = contextoEF.grupos_acesso.AsQueryable();
            List<grupos_acesso> lista = new List<grupos_acesso>();

            if (pItem.id_grupo != 0)
            {
                c = c.Where(x => x.id_grupo == pItem.id_grupo);
            }
            if (pItem.grupos_acesso.grupo != null)
            {
                c = c.Where(x => x.grupo.Contains(pItem.grupos_acesso.grupo));
                c = c.Where(x => x.descricao.Contains(pItem.grupos_acesso.grupo));
            }

            var sAux = contextoEF.grupos_acesso_telas_sistema.Where(x => x.id_tela == pItem.id_tela).Select(x=> x.id_grupo).ToArray();

            c = c.Where(x => !sAux.Contains(x.id_grupo));

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

