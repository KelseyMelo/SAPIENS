using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Repositorio_C
{
    public class CadastroAutomaticoAlunoRepositorio : IDisposable
    {
        private Entities contextoEF;

        public CadastroAutomaticoAlunoRepositorio()
        {
            contextoEF = new Entities();
        }

        public alunos_cadastro_automatico BuscaItem(alunos_cadastro_automatico pItem)
        {
            alunos_cadastro_automatico item = new alunos_cadastro_automatico();
            item = contextoEF.alunos_cadastro_automatico.Where(x => x.id_cadastro_automatico == pItem.id_cadastro_automatico).SingleOrDefault();
            //item = contextoEF.alunos_cadastro_automatico.Include(x => x.tipos_curso).Include(x => x.turmas).Include(x => x.oferecimentos).Include(x => x.periodo_relatorio_dissertacao).Where(x => x.quadrimestre == pItem.quadrimestre).SingleOrDefault();
            return item;
        }

        public alunos_cadastro_automatico CriarItem(alunos_cadastro_automatico pItem)
        {
            contextoEF.alunos_cadastro_automatico.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public Boolean AlterarItem(alunos_cadastro_automatico pItem)
        {
            alunos_cadastro_automatico item = new alunos_cadastro_automatico();
            item = contextoEF.alunos_cadastro_automatico.Where(x => x.id_cadastro_automatico == pItem.id_cadastro_automatico).SingleOrDefault();

            item.descricao = pItem.descricao;
            item.descricao_curso = pItem.descricao_curso;
            item.data_importacao = pItem.data_importacao;
            item.usuario = pItem.usuario;
            contextoEF.SaveChanges();
            return true;
        }


        public List<alunos_cadastro_automatico> ListaItem(alunos_cadastro_automatico pItem)
        {
            var c = contextoEF.alunos_cadastro_automatico.AsQueryable();
            List<alunos_cadastro_automatico> lista = new List<alunos_cadastro_automatico>();

            
            if (pItem.id_cadastro_automatico != 0)
            {
                c = c.Where(x => x.id_cadastro_automatico == pItem.id_cadastro_automatico);
            }

            if (pItem.descricao != "")
            {
                c = c.Where(x => x.descricao.Contains(pItem.descricao));
            }

            if (pItem.descricao_curso != "")
            {
                c = c.Where(x => x.descricao_curso.Contains(pItem.descricao_curso));
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

            lista = c.OrderByDescending(x => x.data_importacao).ThenBy(x => x.descricao).ThenBy(x => x.descricao_curso).ToList();
            //lista = c.ToList();
            return lista;
        }

        /// //////////////////////////////////////////

        public alunos_cadastro_automatico_det BuscaItem_Participante(alunos_cadastro_automatico_det pItem)
        {
            alunos_cadastro_automatico_det item = new alunos_cadastro_automatico_det();
            item = contextoEF.alunos_cadastro_automatico_det.Where(x => x.id_alunos_cadastro_automatico_det == pItem.id_alunos_cadastro_automatico_det).SingleOrDefault();
            return item;
        }

        public bool LimpaItem_Participante(alunos_cadastro_automatico_det pItem)
        {
            contextoEF.alunos_cadastro_automatico_det.RemoveRange(contextoEF.alunos_cadastro_automatico_det.Where(x => x.id_cadastro_automatico == pItem.id_cadastro_automatico));
            contextoEF.SaveChanges();
            return true;
        }

        public alunos_cadastro_automatico_det CriarItem_Participante(alunos_cadastro_automatico_det pItem)
        {
            using (var contextoEF = new Entities())
            {
                contextoEF.alunos_cadastro_automatico_det.Add(pItem);
                contextoEF.SaveChanges();
                return pItem;
            }
        }

        public Boolean AlterarItem_participante(alunos_cadastro_automatico_det pItem)
        {
            alunos_cadastro_automatico_det item = new alunos_cadastro_automatico_det();
            item = contextoEF.alunos_cadastro_automatico_det.Where(x => x.id_alunos_cadastro_automatico_det == pItem.id_alunos_cadastro_automatico_det).SingleOrDefault();

            item.nome = pItem.nome;
            item.cpf = pItem.cpf;
            item.data_envio_email = pItem.data_envio_email;
            item.obs = pItem.obs;
            item.idaluno = pItem.idaluno;
            contextoEF.SaveChanges();
            return true;
        }

        public List<alunos_cadastro_automatico_det> ListaItem_participante(alunos_cadastro_automatico_det pItem)
        {
            var c = contextoEF.alunos_cadastro_automatico_det.AsQueryable();
            List<alunos_cadastro_automatico_det> lista = new List<alunos_cadastro_automatico_det>();


            if (pItem.id_cadastro_automatico != null)
            {
                c = c.Where(x => x.id_cadastro_automatico == pItem.id_cadastro_automatico);
            }

            if (pItem.id_alunos_cadastro_automatico_det != 0)
            {
                c = c.Where(x => x.id_alunos_cadastro_automatico_det == pItem.id_alunos_cadastro_automatico_det);
            }

            if (pItem.nome != "")
            {
                c = c.Where(x => x.nome.Contains(pItem.nome));
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

            lista = c.OrderBy(x => x.nome).ToList();

            return lista;
        }

        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}
