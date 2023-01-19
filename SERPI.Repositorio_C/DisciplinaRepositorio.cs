using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Repositorio_C
{
    public class DisciplinaRepositorio : IDisposable
    {
        private Entities contextoEF;

        public DisciplinaRepositorio()
        {
            contextoEF = new Entities();
        }

        public disciplinas BuscaItem(disciplinas pItem)
        {
            disciplinas item = new disciplinas();
            item = contextoEF.disciplinas.Include(x => x.cursos_disciplinas).Include(x => x.disciplinas_professores).Where(x => x.id_disciplina == pItem.id_disciplina).SingleOrDefault();
            return item;
        }

        public disciplinas VerificaItemMesmoCodigo(disciplinas pItem)
        {
            disciplinas item = new disciplinas();
            item = contextoEF.disciplinas.Include(x => x.cursos_disciplinas).Include(x => x.disciplinas_professores).Where(x => x.id_disciplina != pItem.id_disciplina  && x.codigo == pItem.codigo ).SingleOrDefault();
            return item;
        }

        public disciplinas CriarItem(disciplinas pItem)
        {
            contextoEF.disciplinas.Add(pItem);
            contextoEF.SaveChanges();
            pItem = contextoEF.disciplinas.Include(x => x.cursos_disciplinas).Where(x => x.id_disciplina == pItem.id_disciplina).SingleOrDefault();
            return pItem;
        }

        public disciplinas AlterarStatus(disciplinas pItem)
        {
            disciplinas item = new disciplinas();
            item = contextoEF.disciplinas.Where(x => x.id_disciplina == pItem.id_disciplina).SingleOrDefault();
            item.status = pItem.status;
            item.data_alteracao = DateTime.Now;
            item.usuario = pItem.usuario;
            contextoEF.SaveChanges();
            item = contextoEF.disciplinas.Where(x => x.id_disciplina == pItem.id_disciplina).SingleOrDefault();
            return item;
        }

        public Boolean AlterarItem(disciplinas pItem)
        {
            disciplinas item = new disciplinas();
            item = contextoEF.disciplinas.Where(x => x.id_disciplina == pItem.id_disciplina).SingleOrDefault();

            item.codigo = pItem.codigo;
            item.nome = pItem.nome;
            item.id_area_concentracao = pItem.id_area_concentracao;
            item.obrigatorio = pItem.obrigatorio;
            item.data_criacao = pItem.data_criacao;
            item.data_ultima_alteracao = pItem.data_ultima_alteracao;
            item.objetivo = pItem.objetivo;
            item.ementa = pItem.ementa;
            item.substituindo = pItem.substituindo;
            item.carga_horaria = pItem.carga_horaria;
            item.num_max_alunos = pItem.num_max_alunos;
            item.creditos = pItem.creditos ;
            item.justificativa = pItem.justificativa;
            item.forma_avaliacao = pItem.forma_avaliacao;
            item.material_utilizado = pItem.material_utilizado;
            item.metodologia = pItem.metodologia;
            item.conhecimentos_previos = pItem.conhecimentos_previos;
            item.bibliografia_basica = pItem.bibliografia_basica;
            item.bibliografica_compl = pItem.bibliografica_compl;
            item.programa_disciplina = pItem.programa_disciplina;
            item.observacao = pItem.observacao;
            item.status = pItem.status;
            item.data_cadastro = pItem.data_cadastro;
            item.data_alteracao = pItem.data_alteracao;
            item.usuario = pItem.usuario;
            item.acompanhamento_dissertacao = pItem.acompanhamento_dissertacao;

            contextoEF.SaveChanges();
            return true;
        }

        public List<disciplinas> ListaItem(disciplinas pItem)
        {
            var c = contextoEF.disciplinas.AsQueryable();
            List<disciplinas> lista = new List<disciplinas>();

            if (pItem.id_disciplina != 0)
            {
                c = c.Where(x => x.id_disciplina == pItem.id_disciplina);
            }

            if (pItem.codigo != null)
            {
                c = c.Where(x => x.codigo.Contains(pItem.codigo));
            }

            if (pItem.nome != null)
            {
                c = c.Where(x => x.nome.Contains(pItem.nome));
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

            if (pItem.cursos_disciplinas.Count() > 0)
            {
                if (pItem.cursos_disciplinas.ElementAt(0).id_curso != 0)
                {
                    int iAux = pItem.cursos_disciplinas.ElementAt(0).id_curso;
                    var sAux = contextoEF.cursos_disciplinas.Where(x => x.id_curso == iAux).Select(x => x.id_disciplina).ToArray();
                    c = c.Where(x => sAux.Contains (x.id_disciplina ));
                }
            }
            
            lista = c.Include(x => x.cursos_disciplinas).OrderBy(x => x.nome).ToList();

            return lista;
        }

        public List<disciplinas_professores> ListaProfessores(disciplinas pItem)
        {
            List<disciplinas_professores> lista = new List<disciplinas_professores>();
            lista = contextoEF.disciplinas_professores.Include(x => x.professores).Where(x => x.id_disciplina == pItem.id_disciplina && x.tipo_professor == "professor").ToList();
            return lista;
        }

        public List<disciplinas_professores> ListaTecnicos(disciplinas pItem)
        {
            List<disciplinas_professores> lista = new List<disciplinas_professores>();
            lista = contextoEF.disciplinas_professores.Include(x => x.professores).Where(x => x.id_disciplina == pItem.id_disciplina && x.tipo_professor == "tecnico").ToList();
            return lista;
        }

        public List<professores> ListaProfessoresDisponiveis(disciplinas_professores pItem, professores pItemProfessor)
        {
            var c = contextoEF.professores.AsQueryable();
            List<professores> lista = new List<professores>();
            var sAux = contextoEF.disciplinas_professores.Where(x => x.id_disciplina == pItem.id_disciplina && x.tipo_professor == pItem.tipo_professor).Select(x => x.id_professor).ToArray();
            //lista = contextoEF.professores.Where(x => !sAux.Contains(x.id_professor)).ToList();

            c = c.Where(x => !sAux.Contains(x.id_professor) && x.status != "inativado");

            if (pItemProfessor.cpf != null && pItemProfessor.cpf != "")
            {
                c = c.Where(x => x.cpf == pItemProfessor.cpf);
            }

            if (pItemProfessor.nome != null && pItemProfessor.nome != "")
            {
                c = c.Where(x => x.nome.Contains(pItemProfessor.nome));
            }

            lista = c.OrderBy(x => x.nome).ToList();

            return lista;
        }

        public bool IncluirProfessor_Tecnico_Disciplina(disciplinas_professores pItem)
        {
            contextoEF.disciplinas_professores.Add(pItem);
            contextoEF.SaveChanges();

            List<disciplinas_professores> lista;
            lista = contextoEF.disciplinas_professores.Include(x => x.professores).Where(x => x.id_disciplina == pItem.id_disciplina && x.id_professor == pItem.id_professor).ToList();
            return true;
        }

        public bool ExcluirProfessor_Tecnico_Disciplina(disciplinas_professores pItem)
        {
            disciplinas_professores item;
            item = contextoEF.disciplinas_professores.Where(x => x.id_disciplina == pItem.id_disciplina && x.id_professor == pItem.id_professor && x.tipo_professor == pItem.tipo_professor).SingleOrDefault();
            contextoEF.disciplinas_professores.Remove(item);
            contextoEF.SaveChanges();
            return true;
        }

        public bool AlterarProfessor_Tecnico_Disciplina(disciplinas_professores pItem)
        {
            disciplinas_professores item;
            item = contextoEF.disciplinas_professores.Where(x => x.id_disciplina == pItem.id_disciplina && x.id_professor == pItem.id_professor && x.tipo_professor == pItem.tipo_professor).SingleOrDefault();

            item.responsavel = pItem.responsavel;
            item.status = pItem.status;
            item.data_alteracao = pItem.data_alteracao;
            item.usuario = pItem.usuario;
            contextoEF.SaveChanges();
            return true;
        }

        public bool AlterarResponsavelProfessor_Tecnico_Disciplina(disciplinas_professores pItem)
        {
            List< disciplinas_professores> lista;
            lista = contextoEF.disciplinas_professores.Where(x => x.id_disciplina == pItem.id_disciplina && x.tipo_professor == pItem.tipo_professor && x.responsavel == true).ToList();
            lista.ForEach(x => { x.data_alteracao = DateTime.Now; x.responsavel = false; x.status = "alterado"; });
            contextoEF.SaveChanges();
            return true;
        }

        public Boolean CriarDisciplina_Requisito(disciplinas_requisitos pItem)
        {
            contextoEF.disciplinas_requisitos.Add(pItem);
            contextoEF.SaveChanges();

            return true;
        }

        public bool ExcluirDisciplinas_requisitos(disciplinas pItem)
        {
            contextoEF.disciplinas_requisitos.RemoveRange(contextoEF.disciplinas_requisitos.Where(x => x.id_disciplina == pItem.id_disciplina));
            contextoEF.SaveChanges();
            return true;
        }

        public List<disciplinas> ListaPrerequisitoDisponiveis(disciplinas pItem, decimal[] aIdDisciplina)
        {
            var c = contextoEF.disciplinas.AsQueryable();
            List<disciplinas> lista = new List<disciplinas>();
            //var sAux = contextoEF.disciplinas_professores.Where(x => x.id_disciplina == pItem.id_disciplina).Select(x => x.id_disciplina).ToArray();
            //lista = contextoEF.professores.Where(x => !sAux.Contains(x.id_professor)).ToList();

            c = c.Where(x => !aIdDisciplina.Contains(x.id_disciplina) && x.status != "inativado");

            if (pItem.codigo != null && pItem.codigo != "")
            {
                c = c.Where(x => x.codigo == pItem.codigo);
            }

            if (pItem.nome != null && pItem.nome != "")
            {
                c = c.Where(x => x.nome.Contains(pItem.nome));
            }

            lista = c.OrderBy(x => x.nome).ToList();

            return lista;
        }

        public bool IncluirPrerequisito_Disciplina(disciplinas_requisitos pItem)
        {
            contextoEF.disciplinas_requisitos.Add(pItem);
            pItem.disciplinas1 = contextoEF.disciplinas.Where(x => x.id_disciplina == pItem.id_disciplina_req).SingleOrDefault();

            contextoEF.SaveChanges();
            return true;
        }

        public bool ExcluirPrerequisito_Disciplina(disciplinas_requisitos pItem)
        {
            disciplinas_requisitos item;
            item = contextoEF.disciplinas_requisitos.Where(x => x.id_disciplina == pItem.id_disciplina && x.id_disciplina_req == pItem.id_disciplina_req).SingleOrDefault();
            contextoEF.disciplinas_requisitos.Remove(item);
            contextoEF.SaveChanges();
            return true;
        }

        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}
