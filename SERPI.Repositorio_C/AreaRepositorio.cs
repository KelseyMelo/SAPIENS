using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace Repositorio_C
{
    public class AreaRepositorio : IDisposable
    {
        private Entities contextoEF;

        public AreaRepositorio()
        {
            contextoEF = new Entities();
        }

        public areas_concentracao BuscaItem(areas_concentracao pItem)
        {
            areas_concentracao item = new areas_concentracao();
            item = contextoEF.areas_concentracao.Where(x => x.id_area_concentracao == pItem.id_area_concentracao).SingleOrDefault();
            return item;
        }

        public areas_concentracao CriarItem(areas_concentracao pItem)
        {
            contextoEF.areas_concentracao.Add(pItem);
            contextoEF.SaveChanges();
            pItem = contextoEF.areas_concentracao.Include(x=> x.cursos).Where(x => x.id_area_concentracao == pItem.id_area_concentracao).SingleOrDefault();
            return pItem;
        }

        public Boolean AlterarItem(areas_concentracao pItem)
        {
            areas_concentracao item = new areas_concentracao();
            item = contextoEF.areas_concentracao.Include(x => x.cursos).Include(x => x.cursos.fichas_inscricao).Where(x => x.id_area_concentracao == pItem.id_area_concentracao).SingleOrDefault();

            item.nome = pItem.nome;
            item.id_curso = pItem.id_curso;
            item.num_eletivas = pItem.num_eletivas;
            item.disponivel = pItem.disponivel;
            item.status  = pItem.status;
            item.data_cadastro = pItem.data_cadastro;
            item.data_alteracao = pItem.data_alteracao;
            item.usuario  = pItem.usuario;

            contextoEF.SaveChanges();
            return true;
        }

        public areas_concentracao AlterarStatus(areas_concentracao pItem)
        {
            areas_concentracao item = new areas_concentracao();
            item = contextoEF.areas_concentracao.Where(x => x.id_area_concentracao == pItem.id_area_concentracao).SingleOrDefault();
            item.status = pItem.status;
            item.data_alteracao = DateTime.Now;
            item.usuario = pItem.usuario;
            contextoEF.SaveChanges();
            item = contextoEF.areas_concentracao.Where(x => x.id_area_concentracao == pItem.id_area_concentracao).SingleOrDefault();
            return item;
        }

        public List<areas_concentracao> ListaItem(areas_concentracao pItem)
        {
            var c = contextoEF.areas_concentracao.AsQueryable();
            List<areas_concentracao> lista = new List<areas_concentracao>();

            if (pItem.id_area_concentracao != 0)
            {
                c = c.Where(x => x.id_area_concentracao == pItem.id_area_concentracao);
            }

            if (pItem.nome != null)
            {
                c = c.Where(x => x.nome.Contains(pItem.nome));
            }

            if (pItem.id_curso != 0)
            {
                c = c.Where(x => x.id_curso == pItem.id_curso);
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

            lista = c.Include(x=> x.cursos).OrderBy(x => x.nome).ToList();

            return lista;
        }

        public List<areas_concentracao_coordenadores> ListaCoordenadores(areas_concentracao pItem)
        {
            List<areas_concentracao_coordenadores> lista = new List<areas_concentracao_coordenadores>();
            lista = contextoEF.areas_concentracao_coordenadores.Include(x=> x.professores).Where(x => x.id_area_concentracao == pItem.id_area_concentracao).ToList();
            return lista;
        }

        public List<professores> ListaCoordenadoresDisponiveis(areas_concentracao pItem, professores pItemProfessor)
        {
            var c = contextoEF.professores.AsQueryable();
            List<professores> lista = new List<professores>();
            var sAux = contextoEF.areas_concentracao_coordenadores.Where(x => x.id_area_concentracao == pItem.id_area_concentracao).Select(x=> x.id_professor).ToArray();
            lista = contextoEF.professores.Where(x => !sAux.Contains(x.id_professor)).ToList();

            c = c.Where(x => !sAux.Contains(x.id_professor));

            if (pItemProfessor.cpf  != "")
            {
                c = c.Where(x => x.cpf == pItemProfessor.cpf);
            }

            if (pItemProfessor.nome != "")
            {
                c = c.Where(x => x.nome.Contains(pItemProfessor.nome));
            }

            lista = c.OrderBy(x => x.nome).ToList();
            
            return lista;
        }

        public bool IncluirCoordenador(areas_concentracao_coordenadores pItem)
        {
            contextoEF.areas_concentracao_coordenadores.Add(pItem);
            contextoEF.SaveChanges();
            return true;
        }

        public bool ExcluirCoordenador(areas_concentracao_coordenadores pItem)
        {
            areas_concentracao_coordenadores item;
            item = contextoEF.areas_concentracao_coordenadores.Where(x => x.id_area_concentracao == pItem.id_area_concentracao && x.id_professor == pItem.id_professor).SingleOrDefault();
            contextoEF.areas_concentracao_coordenadores.Remove(item);
            contextoEF.SaveChanges();
            return true;
        }

        public Boolean CriarAreaConcentracaoDisciplina(areas_concentracao_disciplinas pItem)
        {
            contextoEF.areas_concentracao_disciplinas.Add(pItem);
            contextoEF.SaveChanges();

            return true;
        }

        public bool ExcluirAreaConcentracaoDisciplina(areas_concentracao pItem)
        {
            contextoEF.areas_concentracao_disciplinas.RemoveRange(contextoEF.areas_concentracao_disciplinas.Where(x=> x.id_area_concentracao == pItem.id_area_concentracao));
            contextoEF.SaveChanges();
            return true;
        }

        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}
