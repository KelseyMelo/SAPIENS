
using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace Repositorio_C
{
    public class TipoCursoRepositorio : IDisposable
    {
        private Entities contextoEF;

        public TipoCursoRepositorio()
        {
            contextoEF = new Entities();
        }

        public tipos_curso BuscaItem(tipos_curso pItem)
        {
            tipos_curso item = new tipos_curso();
            item = contextoEF.tipos_curso.Where(x => x.id_tipo_curso == pItem.id_tipo_curso).SingleOrDefault();
            return item;
        }

        public tipos_curso VerificaItemMesmoTipoCurso_MesmoNome(tipos_curso pItem)
        {
            tipos_curso item = new tipos_curso();
            item = contextoEF.tipos_curso.Where(x => x.id_tipo_curso != pItem.id_tipo_curso && x.tipo_curso == pItem.tipo_curso).SingleOrDefault();
            return item;
        }

        public tipos_curso CriarItem(tipos_curso pItem)
        {
            contextoEF.tipos_curso.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public tipos_curso AlterarItem(tipos_curso pItem)
        {
            tipos_curso item = new tipos_curso();
            item = contextoEF.tipos_curso.Where(x => x.id_tipo_curso == pItem.id_tipo_curso).SingleOrDefault();
            item.tipo_curso = pItem.tipo_curso;
            //============================
            item.statusHomepagePeview = pItem.statusHomepagePeview;
            item.nome_imagemPeview = pItem.nome_imagemPeview;
            item.data_imagemPeview = pItem.data_imagemPeview;
            item.decricao_homepagePeview = pItem.decricao_homepagePeview;
            item.status_BotoesPeview = pItem.status_BotoesPeview;
            item.calendarioPeview = pItem.calendarioPeview;
            item.processo_seletivoPeview = pItem.processo_seletivoPeview;
            item.data_alteracao = DateTime.Now;
            item.usuario = pItem.usuario;
            item.status = pItem.status;
            item.statusAprovado = pItem.statusAprovado;
            item.obs_preview = pItem.obs_preview;
            //=====
            item.tipo_curso_en = pItem.tipo_curso_en;
            item.descricao_homepage_en = pItem.descricao_homepage_en;
            item.decricao_homepagePeview_en = pItem.decricao_homepagePeview_en;
            item.calendario_en = pItem.calendario_en;
            item.calendarioPreview_en = pItem.calendarioPreview_en;
            item.processo_seletivo_en = pItem.processo_seletivo_en;
            item.processo_seletivoPreview_en = pItem.processo_seletivoPreview_en;
            contextoEF.SaveChanges();
            return item;
        }

        public Boolean AlterarItem_Aprovacao(tipos_curso pItem)
        {
            tipos_curso item = new tipos_curso();
            item = contextoEF.tipos_curso.Where(x => x.id_tipo_curso == pItem.id_tipo_curso).SingleOrDefault();

            //===============
            item.statusHomepage = pItem.statusHomepagePeview;
            item.statusBotoes = pItem.status_BotoesPeview;
            item.descricao_homepage = pItem.decricao_homepagePeview;
            item.calendario = pItem.calendarioPeview;
            item.processo_seletivo = pItem.processo_seletivoPeview;
            item.descricao_homepage_en = pItem.decricao_homepagePeview_en;
            item.calendario_en = pItem.calendarioPreview_en;
            item.processo_seletivo_en = pItem.processo_seletivoPreview_en;
            item.nome_imagem = pItem.nome_imagemPeview;
            item.data_imagem = pItem.data_imagemPeview;
            item.data_aprovacao = pItem.data_aprovacao;
            item.usuario_aprovacao = pItem.usuario_aprovacao;
            item.statusAprovado = pItem.statusAprovado;
            item.obs_preview = "";

            contextoEF.SaveChanges();
            return true;
        }

        public Boolean AlterarItem_Reprovacao(tipos_curso pItem)
        {
            tipos_curso item = new tipos_curso();
            item = contextoEF.tipos_curso.Where(x => x.id_tipo_curso== pItem.id_tipo_curso).SingleOrDefault();

            //===============
            //item.data_aprovacao = pItem.data_aprovacao;
            item.data_reprovacao = pItem.data_reprovacao;
            item.usuario_aprovacao = pItem.usuario_aprovacao;
            item.statusAprovado = pItem.statusAprovado;
            item.obs_preview = pItem.obs_preview;

            contextoEF.SaveChanges();
            return true;
        }

        public List<tipos_curso> ListaItemAprovacaoHomePage()
        {
            var c = contextoEF.tipos_curso.AsQueryable();
            List<tipos_curso> lista = new List<tipos_curso>();

            c = c.Where(x => x.statusAprovado == 0);

            lista = c.OrderBy(x => x.tipo_curso).ToList();

            return lista;
        }

        public List<tipos_curso> ListaItemReprovacaoHomePage(string qUsuario)
        {
            var c = contextoEF.tipos_curso.AsQueryable();
            List<tipos_curso> lista = new List<tipos_curso>();

            c = c.Where(x => x.statusAprovado == 2 && x.usuario == qUsuario);

            lista = c.OrderBy(x => x.tipo_curso).ToList();

            return lista;
        }

        public List<tipos_curso> ListaItem(tipos_curso pItem)
        {
            var c = contextoEF.tipos_curso.AsQueryable();
            List<tipos_curso> lista = new List<tipos_curso>();

            if (pItem.id_tipo_curso != 0)
            {
                c = c.Where(x => x.id_tipo_curso == pItem.id_tipo_curso);
            }

            if (pItem.tipo_curso != null)
            {
                c = c.Where(x => x.tipo_curso.Contains(pItem.tipo_curso));
            }

            //if (pItem.status != "")
            //{
            //    if (pItem.status == "ativado")
            //    {
            //        c = c.Where(x => x.status != "inativado");
            //    }
            //    else if (pItem.status == "inativado")
            //    {
            //        c = c.Where(x => x.status == "inativado");
            //    }
            //}

            // 0 = Aguardando aprovação -- 1 = Aprovado-- 2 = Reprovado-- 3 = Sem página
            //(4 = não existe)
            if (pItem.statusAprovado == 0 || pItem.statusAprovado == 1 || pItem.statusAprovado == 2 || pItem.statusAprovado == 3)
            {
                c = c.Where(x => x.statusAprovado == pItem.statusAprovado);
            }


            lista = c.OrderBy(x => x.id_tipo_curso).ToList();

            return lista;
        }

        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}

