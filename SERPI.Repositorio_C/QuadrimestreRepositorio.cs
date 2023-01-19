using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Repositorio_C
{
    public class QuadrimestreRepositorio : IDisposable
    {
        private Entities contextoEF;

        public QuadrimestreRepositorio()
        {
            contextoEF = new Entities();
        }

        public quadrimestres BuscaItem(quadrimestres pItem)
        {
            quadrimestres item = new quadrimestres();
            item = contextoEF.quadrimestres.Where(x => x.quadrimestre == pItem.quadrimestre).SingleOrDefault();
            //item = contextoEF.quadrimestres.Include(x => x.tipos_curso).Include(x => x.turmas).Include(x => x.oferecimentos).Include(x => x.periodo_relatorio_dissertacao).Where(x => x.quadrimestre == pItem.quadrimestre).SingleOrDefault();
            return item;
        }

        public int BuscaItem_NumeroMaximo(quadrimestres pItem)
        {
            List<quadrimestres> lista = new List<quadrimestres>();
            int iAux;
            if (pItem.id_tipo_curso == 1)
            {
                lista = contextoEF.quadrimestres.Include(x => x.tipos_curso).Include(x => x.turmas).Include(x => x.oferecimentos).Include(x => x.periodo_relatorio_dissertacao).Where(x => x.ano == pItem.ano && (x.id_tipo_curso == pItem.id_tipo_curso || x.id_tipo_curso == null)).ToList();
            }
            else
            {
                lista = contextoEF.quadrimestres.Include(x => x.tipos_curso).Include(x => x.turmas).Include(x => x.oferecimentos).Include(x => x.periodo_relatorio_dissertacao).Where(x => x.ano == pItem.ano && x.id_tipo_curso == pItem.id_tipo_curso).ToList();
            }
            
            if (lista.Count != 0)
            {
                iAux = lista.Max(x => x.numero).Value;
            }
            else
            {
                iAux = 0;
            }
            return iAux;
        }

        public quadrimestres CriarItem(quadrimestres pItem)
        {
            contextoEF.quadrimestres.Add(pItem);
            contextoEF.SaveChanges();
            pItem = contextoEF.quadrimestres.Include(x => x.tipos_curso).Include(x => x.turmas).Include(x => x.oferecimentos).Include(x => x.periodo_relatorio_dissertacao).Where(x => x.quadrimestre == pItem.quadrimestre).SingleOrDefault();
            return pItem;
        }

        public quadrimestres AlterarStatus(quadrimestres pItem)
        {
            quadrimestres item = new quadrimestres();
            item = contextoEF.quadrimestres.Include(x => x.tipos_curso).Include(x => x.turmas).Include(x => x.oferecimentos).Include(x => x.periodo_relatorio_dissertacao).Where(x => x.quadrimestre == pItem.quadrimestre).SingleOrDefault();
            item.status = pItem.status;
            item.data_alteracao = DateTime.Now;
            item.usuario = pItem.usuario;
            contextoEF.SaveChanges();
            return item;
        }

        public Boolean AlterarItem(quadrimestres pItem)
        {
            quadrimestres item = new quadrimestres();
            item = contextoEF.quadrimestres.Include(x => x.tipos_curso).Include(x => x.turmas).Include(x => x.oferecimentos).Include(x => x.periodo_relatorio_dissertacao).Where(x => x.quadrimestre == pItem.quadrimestre).SingleOrDefault();

            item.ano = pItem.ano;
            item.numero = pItem.numero;
            item.data_inicio = pItem.data_inicio;
            item.data_fim = pItem.data_fim;
            item.status = pItem.status;
            item.data_cadastro = pItem.data_cadastro;
            item.data_alteracao = pItem.data_alteracao;
            item.usuario = pItem.usuario;
            item.id_tipo_curso = pItem.id_tipo_curso;

            contextoEF.SaveChanges();
            return true;
        }

        public List<quadrimestres> ListaItem()
        {
            var c = contextoEF.quadrimestres.AsQueryable();
            List<quadrimestres> lista = new List<quadrimestres>();
           
            lista = contextoEF.quadrimestres.OrderByDescending(x=> x.ano).ThenByDescending(x => x.quadrimestre).ToList();
            return lista;
        }

        public List<quadrimestres> ListaItem(quadrimestres pItem)
        {
            var c = contextoEF.quadrimestres.AsQueryable();
            List<quadrimestres> lista = new List<quadrimestres>();

            if (pItem.ano != null)
            {
                c = c.Where(x => x.ano == pItem.ano);
            }

            if (pItem.id_tipo_curso != null)
            {
                if (pItem.id_tipo_curso == 1)
                {
                    c = c.Where(x => x.id_tipo_curso == pItem.id_tipo_curso || x.id_tipo_curso == null);
                }
                else
                {
                    c = c.Where(x => x.id_tipo_curso == pItem.id_tipo_curso);
                }
            }

            if (pItem.status != "")
            {
                if (pItem.status == "ativado")
                {
                    c = c.Where(x => x.status != "inativado");
                }
                else if (pItem.status == "inativado")
                {
                    c = c.Where(x => x.status == "inativado");
                }
            }

            lista = c.OrderByDescending(x => x.ano).ThenBy(x => x.numero).ToList();

            //lista = c.Include(x => x.tipos_curso).Include(x => x.turmas).Include(x => x.oferecimentos).Include(x => x.periodo_relatorio_dissertacao).OrderByDescending(x => x.ano).ThenBy(x=> x.numero).ToList();

            return lista;
        }

        public List<quadrimestres> ListaItem(cursos pItem)
        {
            var c = contextoEF.quadrimestres.AsQueryable();
            List<quadrimestres> lista = new List<quadrimestres>();
            if (pItem.id_curso !=0)
            {
                lista = contextoEF.turmas.Where(x => x.id_curso == pItem.id_curso).Select(x => x.quadrimestres).ToList();
            }
            else
            {
                lista = contextoEF.turmas.Select(x => x.quadrimestres).ToList();
            }
            return lista;
        }

        public List<quadrimestres> ListaItem(List<cursos> lista_curso)
        {
            var c = contextoEF.quadrimestres.AsQueryable();
            List<quadrimestres> lista = new List<quadrimestres>();

            var sAux = lista_curso.ToArray().Select(x => x.id_curso);
                lista = contextoEF.turmas.Where(x => sAux.Contains(x.id_curso)).Select(x => x.quadrimestres).ToList();
            return lista;
        }

        ///<summary>
        ///Listar todos os períodos de um determinado Tipo de Curso
        ///</summary>
        public List<quadrimestres> ListaItem(tipos_curso pItem)
        {
            var c = contextoEF.quadrimestres.AsQueryable();
            List<quadrimestres> lista = new List<quadrimestres>();
            if (pItem.id_tipo_curso != 0)
            {
                lista = contextoEF.quadrimestres.Where(x => x.id_tipo_curso == pItem.id_tipo_curso).ToList();
            }
            else
            {
                lista = contextoEF.quadrimestres.ToList();
            }
            
            return lista;
        }

        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}
