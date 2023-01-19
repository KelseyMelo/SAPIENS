using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Repositorio_C
{
    public class OferecimentoRepositorio : IDisposable
    {
        private Entities contextoEF;

        public OferecimentoRepositorio()
        {
            contextoEF = new Entities();
        }

        public oferecimentos BuscaItem(oferecimentos pItem)
        {
            var c = contextoEF.oferecimentos.AsQueryable();

            if (pItem.id_oferecimento != 0)
            {
                c = c.Where(x => x.id_oferecimento == pItem.id_oferecimento);
            }

            if (pItem.id_disciplina != 0)
            {
                c = c.Where(x => x.id_disciplina == pItem.id_disciplina);
            }

            if (pItem.quadrimestre != "" && pItem.quadrimestre != null)
            {
                c = c.Where(x => x.quadrimestre == pItem.quadrimestre);
            }

            if (pItem.num_oferecimento != 0)
            {
                c = c.Where(x => x.num_oferecimento == pItem.num_oferecimento && x.ativo == true);
            }

            oferecimentos item = new oferecimentos();
            item =  c.Include(x => x.oferecimentos_professores).Include(x => x.quadrimestres).Include(x => x.datas_aulas).Include(x => x.matricula_oferecimento).SingleOrDefault();

            return item;
        }

        public matricula_oferecimento BuscaMatriculaOferecimento(matricula_oferecimento pItem)
        {
            matricula_oferecimento item = new matricula_oferecimento();
            item = contextoEF.matricula_oferecimento.Where(x => x.id_aluno == pItem.id_aluno && x.id_turma == pItem.id_turma && x.oferecimentos.id_disciplina == pItem.oferecimentos.id_disciplina && x.oferecimentos.quadrimestre == pItem.oferecimentos.quadrimestre).FirstOrDefault();
            return item;
        }

        public int BuscaNumeroOferecimento(oferecimentos pItem)
        {
            List<oferecimentos> lista = new List<oferecimentos>();
            int iAux;
            lista = contextoEF.oferecimentos.Include(x => x.oferecimentos_professores).Include(x => x.quadrimestres).Include(x => x.datas_aulas).Where(x => x.id_disciplina == pItem.id_disciplina && x.quadrimestre == pItem.quadrimestre).ToList();
            if (lista.Count == 0 )
            {
                iAux = 1;
            }
            else
            {
                iAux = lista.Max(x => x.num_oferecimento) + 1;
            }
            return iAux;
        }

        public oferecimentos CriarItem(oferecimentos pItem)
        {
            contextoEF.oferecimentos.Add(pItem);
            contextoEF.SaveChanges();
            pItem = contextoEF.oferecimentos.Include(x => x.quadrimestres).Include(x => x.oferecimentos_professores).Include(x => x.disciplinas).Include(x => x.datas_aulas).Where(x => x.id_oferecimento == pItem.id_oferecimento).SingleOrDefault();
            return pItem;
        }

        public oferecimentos AlterarStatus(oferecimentos pItem)
        {
            oferecimentos item = new oferecimentos();
            item = contextoEF.oferecimentos.Where(x => x.id_oferecimento == pItem.id_oferecimento).SingleOrDefault();
            item.status = pItem.status;
            item.data_alteracao = DateTime.Now;
            item.usuario = pItem.usuario;
            contextoEF.SaveChanges();
            item = contextoEF.oferecimentos.Include(x => x.quadrimestres).Include(x => x.disciplinas).Include(x => x.datas_aulas).Where(x => x.id_oferecimento == pItem.id_oferecimento).SingleOrDefault();
            return item;
        }

        public Boolean AlterarItem(oferecimentos pItem)
        {
            oferecimentos item = new oferecimentos();
            item = contextoEF.oferecimentos.Where(x => x.id_oferecimento == pItem.id_oferecimento).SingleOrDefault();

            //item.id_disciplina = pItem.id_disciplina;
            item.ativo = pItem.ativo;
            //item.num_oferecimento = pItem.num_oferecimento;
            item.objetivo = pItem.objetivo;
            item.ementa = pItem.ementa;
            item.carga_horaria = pItem.carga_horaria;
            item.num_max_alunos = pItem.num_max_alunos;
            item.creditos = pItem.creditos;
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
            item.quadrimestre = pItem.quadrimestre;

            contextoEF.SaveChanges();
            return true;
        }

        public List<oferecimentos> ListaItem(oferecimentos pItem, decimal idProfessor, DateTime sDataInicio, DateTime sDataFim )
        {
            DateTime sDataDefaut = new DateTime();
            var c = contextoEF.oferecimentos.AsQueryable();
            List<oferecimentos> lista = new List<oferecimentos>();
    
            if (idProfessor != 0)
            {
                List<datas_aulas_professor> lista_Data_aulas_professor = new List<datas_aulas_professor>();
                var sIdAula = contextoEF.datas_aulas_professor.Where(x => x.id_professor == idProfessor).Select(x => x.id_aula).ToArray();
                var sIdOferecimento = contextoEF.datas_aulas.Where(x => sIdAula.Contains(x.id_aula)).Select(x => x.id_oferecimento).ToArray();
                c = c.Where(x => sIdOferecimento.Contains(x.id_oferecimento));
            }

            if (pItem.id_oferecimento != 0)
            {
                c = c.Where(x => x.id_oferecimento == pItem.id_oferecimento);
            }

            if (pItem.id_disciplina != 0)
            {
                c = c.Where(x => x.id_disciplina == pItem.id_disciplina);
            }

            if (pItem.ativo != null)
            {
                c = c.Where(x => x.ativo == pItem.ativo);
            }

            if (pItem.disciplinas.codigo != null)
            {
                c = c.Where(x => x.disciplinas.codigo.Contains(pItem.disciplinas.codigo));
            }

            if (pItem.disciplinas.nome != null)
            {
                c = c.Where(x => x.disciplinas.nome.Contains(pItem.disciplinas.nome));
            }

            if (pItem.quadrimestre != null)
            {
                c = c.Where(x => x.quadrimestre == pItem.quadrimestre);
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

            if (pItem.disciplinas.cursos_disciplinas.ElementAt(0).id_curso != 0)
            {
                int iAux = pItem.disciplinas.cursos_disciplinas.ElementAt(0).id_curso;
                var sAux = contextoEF.cursos_disciplinas.Where(x => x.id_curso == iAux).Select(x => x.id_disciplina).ToArray();
                c = c.Where(x => sAux.Contains(x.id_disciplina));
            }
            else if (pItem.disciplinas.cursos_disciplinas.ElementAt(0).cursos.id_tipo_curso != 0)
            {
                int iAux = pItem.disciplinas.cursos_disciplinas.ElementAt(0).cursos.id_tipo_curso;
                var sAux2 = contextoEF.cursos.Where(x => x.id_tipo_curso == iAux).Select(x => x.id_curso).ToArray();
                var sAux = contextoEF.cursos_disciplinas.Where(x => sAux2.Contains( x.id_curso)).Select(x => x.id_disciplina).ToArray();
                c = c.Where(x => sAux.Contains(x.id_disciplina));
            }

            if (pItem.quadrimestre != null)
            {
                c = c.Where(x => x.quadrimestre == pItem.quadrimestre);
            }


            if (sDataInicio != sDataDefaut && sDataFim != sDataDefaut)
            {
                var sAux = contextoEF.datas_aulas.Where(x => x.data_aula >= sDataInicio && x.data_aula <= sDataFim).Select(x => x.id_oferecimento).ToArray();
                c = c.Where(x => sAux.Contains(x.id_oferecimento));
            }
            else if (sDataInicio != sDataDefaut && sDataFim == sDataDefaut)
            {
                var sAux = contextoEF.datas_aulas.Where(x => x.data_aula >= sDataInicio).Select(x => x.id_oferecimento).ToArray();
                c = c.Where(x => sAux.Contains(x.id_oferecimento));
            }
            else if (sDataInicio == sDataDefaut && sDataFim != sDataDefaut)
            {
                var sAux = contextoEF.datas_aulas.Where(x => x.data_aula <= sDataFim).Select(x => x.id_oferecimento).ToArray();
                c = c.Where(x => sAux.Contains(x.id_oferecimento));
            }

            //lista = c.Include(x => x.quadrimestres).Include(x => x.disciplinas).Include(x => x.datas_aulas).OrderByDescending(x => x.quadrimestre).OrderBy(x=> x.disciplinas.nome).ToList();

            lista = c.Include(x => x.quadrimestres).Include(x => x.disciplinas).OrderByDescending(x => x.quadrimestre).OrderBy(x => x.disciplinas.nome).ToList();

            return lista;
        }

        public List<oferecimentos_professores> ListaProfessores(oferecimentos pItem)
        {
            List<oferecimentos_professores> lista = new List<oferecimentos_professores>();
            lista = contextoEF.oferecimentos_professores.Include(x => x.professores).Where(x => x.id_oferecimento == pItem.id_oferecimento && x.tipo_professor == "professor").ToList();
            return lista;
        }

        public List<oferecimentos_professores> ListaTecnicos(oferecimentos pItem)
        {
            List<oferecimentos_professores> lista = new List<oferecimentos_professores>();
            lista = contextoEF.oferecimentos_professores.Include(x => x.professores).Where(x => x.id_oferecimento == pItem.id_oferecimento && x.tipo_professor == "tecnico").ToList();
            return lista;
        }

        public List<oferecimentos> Disciplina_cursada_aluno(matricula_oferecimento pItem, int qIdDisciplina)
        {
            List<oferecimentos> lista = new List<oferecimentos>();
            var sAux = contextoEF.matricula_oferecimento.Where(x => x.id_aluno == pItem.id_aluno && x.id_turma == pItem.id_turma).Select(x=> x.id_oferecimento).ToArray();
            lista = contextoEF.oferecimentos.Where(x => sAux.Contains(x.id_oferecimento) && x.id_disciplina == qIdDisciplina).ToList();
            return lista;
        }

        public List<oferecimentos> Oferecimentos_nObrigatorios_cursados_aluno(matricula_oferecimento pItem, int qIdCurso)
        {
            List<oferecimentos> lista = new List<oferecimentos>();
            var sAux = contextoEF.matricula_oferecimento.Where(x => x.id_aluno == pItem.id_aluno && x.id_turma == pItem.id_turma).Select(x => x.id_oferecimento).ToArray();
            var sAux1 = contextoEF.cursos_disciplinas.Where(x => x.id_curso == qIdCurso && x.obrigatoria != 1).Select(x => x.id_disciplina).ToArray();
            lista = contextoEF.oferecimentos.Where(x => sAux.Contains(x.id_oferecimento) && sAux1.Contains(x.id_disciplina)).ToList();
            return lista;
        }

        public List<professores> ListaProfessoresDisponiveis(oferecimentos_professores pItem, professores pItemProfessor)
        {
            var c = contextoEF.professores.AsQueryable();
            List<professores> lista = new List<professores>();
            var sAux = contextoEF.oferecimentos_professores.Where(x => x.id_oferecimento == pItem.id_oferecimento && x.tipo_professor == pItem.tipo_professor).Select(x => x.id_professor).ToArray();
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

        public bool IncluirProfessor_Tecnico_Oferecimento(oferecimentos_professores pItem)
        {
            contextoEF.oferecimentos_professores.Add(pItem);
            contextoEF.SaveChanges();

            List<oferecimentos_professores> lista;
            lista = contextoEF.oferecimentos_professores.Include(x => x.professores).Where(x => x.id_oferecimento == pItem.id_oferecimento && x.id_professor == pItem.id_professor).ToList();
            return true;
        }

        public bool ExcluirProfessor_Tecnico_Oferecimento(oferecimentos_professores pItem)
        {
            oferecimentos_professores item;
            item = contextoEF.oferecimentos_professores.Where(x => x.id_oferecimento == pItem.id_oferecimento && x.id_professor == pItem.id_professor && x.tipo_professor == pItem.tipo_professor).SingleOrDefault();
            contextoEF.oferecimentos_professores.Remove(item);
            contextoEF.SaveChanges();
            return true;
        }

        public bool AlterarProfessor_Tecnico_Oferecimento(oferecimentos_professores pItem)
        {
            oferecimentos_professores item;
            item = contextoEF.oferecimentos_professores.Where(x => x.id_oferecimento == pItem.id_oferecimento && x.id_professor == pItem.id_professor && x.tipo_professor == pItem.tipo_professor).SingleOrDefault();

            item.responsavel = pItem.responsavel;
            item.status = pItem.status;
            item.data_alteracao = pItem.data_alteracao;
            item.usuario = pItem.usuario;
            contextoEF.SaveChanges();
            return true;
        }

        public bool AlterarResponsavelProfessor_Tecnico_Oferecimento(oferecimentos_professores pItem)
        {
            List<oferecimentos_professores> lista;
            lista = contextoEF.oferecimentos_professores.Where(x => x.id_oferecimento == pItem.id_oferecimento && x.tipo_professor == pItem.tipo_professor && x.responsavel == true).ToList();
            lista.ForEach(x => { x.data_alteracao = DateTime.Now; x.responsavel = false; x.status = "alterado"; });
            contextoEF.SaveChanges();
            return true;
        }

        public datas_aulas Criar_datas_aulas(datas_aulas pItem)
        {
            contextoEF.datas_aulas.Add(pItem);
            contextoEF.SaveChanges();
            pItem = contextoEF.datas_aulas.Include(x => x.presenca_professor).Include(x => x.datas_aulas_professor).Include(x => x.presenca).Include(x => x.salas_aula).Where(x => x.id_aula == pItem.id_aula).SingleOrDefault();
            return pItem;
        }

        public bool Criar_datas_aulas_professor(datas_aulas_professor pItem)
        {
            professores item;
            item = contextoEF.professores.Where(x => x.id_professor == pItem.id_professor).SingleOrDefault();
            pItem.professores = item;
            contextoEF.datas_aulas_professor.Add(pItem);
            contextoEF.SaveChanges();
            return true;
        }

        public bool Criar_presenca_professor(presenca_professor pItem)
        {
            contextoEF.presenca_professor.Add(pItem);
            contextoEF.SaveChanges();
            return true;
        }

        public bool Alterar_datas_aulas(datas_aulas pItem)
        {
            datas_aulas item;
            item = contextoEF.datas_aulas.Where(x => x.id_aula == pItem.id_aula).SingleOrDefault();
            item.id_sala_aula = pItem.id_sala_aula;
            item.data_aula = pItem.data_aula;
            item.hora_inicio = pItem.hora_inicio;
            item.hora_fim = pItem.hora_fim;
            item.status = pItem.status;
            item.data_alteracao = pItem.data_alteracao;
            item.usuario = pItem.usuario;

            contextoEF.SaveChanges();
            return true;
        }

        public bool Excluir_presenca_professor(presenca_professor pItem)
        {
            var c = contextoEF.presenca_professor.AsQueryable();
            List<presenca_professor> lista = new List<presenca_professor>();

            if (pItem.id_aula != 0)
            {
                c = c.Where(x => x.id_aula == pItem.id_aula);
            }

            if (pItem.id_professor != 0)
            {
                c = c.Where(x => x.id_professor == pItem.id_professor);
            }

            if (pItem.id_oferecimento != 0)
            {
                c = c.Where(x => x.id_oferecimento == pItem.id_oferecimento);
            }

            lista = c.ToList();

            foreach (var item in lista)
            {
                contextoEF.presenca_professor.Remove(item);
            }
            contextoEF.SaveChanges();
            return true;
        }

        public bool Excluir_datas_aulas_professor(datas_aulas_professor pItem)
        {
            var c = contextoEF.datas_aulas_professor.AsQueryable();
            List<datas_aulas_professor> lista = new List<datas_aulas_professor>();

            if (pItem.id_aula != 0)
            {
                c = c.Where(x => x.id_aula == pItem.id_aula);
            }

            if (pItem.id_professor != 0)
            {
                c = c.Where(x => x.id_professor == pItem.id_professor);
            }

            lista = c.ToList();

            foreach (var item in lista)
            {
                contextoEF.datas_aulas_professor.Remove(item);
            }
            contextoEF.SaveChanges();
            return true;
        }

        public bool Excluir_datas_aulas(datas_aulas pItem)
        {
            var c = contextoEF.datas_aulas.AsQueryable();
            List<datas_aulas> lista = new List<datas_aulas>();

            if (pItem.id_aula != 0)
            {
                c = c.Where(x => x.id_aula == pItem.id_aula);
            }

            lista = c.ToList();

            foreach (var item in lista)
            {
                contextoEF.datas_aulas.Remove(item);
            }
            contextoEF.SaveChanges();
            return true;
        }

        public bool Excluir_presenca(presenca pItem)
        {
            var c = contextoEF.presenca.AsQueryable();
            List<presenca> lista = new List<presenca>();

            if (pItem.id_aula != 0)
            {
                c = c.Where(x => x.id_aula == pItem.id_aula);
            }

            lista = c.ToList();

            foreach (var item in lista)
            {
                contextoEF.presenca.Remove(item);
            }
            contextoEF.SaveChanges();
            return true;
        }

        public bool Excluir_Aluno_matricula_oferecimento(int qIdAluno, int qIdMatricula, int qIdOferecimento)
        {
            var c = contextoEF.presenca.AsQueryable();
            List<presenca> lista = new List<presenca>();

            c = c.Where(x => x.id_oferecimento == qIdOferecimento && x.id_aluno == qIdAluno);

            lista = c.ToList();

            foreach (var item in lista)
            {
                contextoEF.presenca.Remove(item);
            }

            contextoEF.Database.ExecuteSqlCommand("DELETE FROM historico_notas WHERE id_nota IN (SELECT id_nota FROM notas WHERE id_oferecimento=" + qIdOferecimento + " AND id_aluno=" + qIdAluno + ")");

            contextoEF.notas.RemoveRange(contextoEF.notas.Where(x => x.id_aluno == qIdAluno && x.id_oferecimento == qIdOferecimento));

            contextoEF.matricula_oferecimento_outro_curso.RemoveRange(contextoEF.matricula_oferecimento_outro_curso.Where(x => x.id_matricula_oferecimento == qIdMatricula));

            contextoEF.matricula_oferecimento.RemoveRange(contextoEF.matricula_oferecimento.Where(x => x.id_matricula_oferecimento == qIdMatricula));

            contextoEF.SaveChanges();
            return true;
        }



        public bool Alterar_PresencaProfessor(presenca_professor pItem)
        {
            presenca_professor item;
            item = contextoEF.presenca_professor.Where(x => x.id_aula == pItem.id_aula && x.id_professor == pItem.id_professor && x.id_oferecimento == pItem.id_oferecimento && x.tipo_professor == pItem.tipo_professor).SingleOrDefault();
            if (item == null)
            {
                Criar_presenca_professor(pItem);
            }
            else
            {
                item.presente = pItem.presente;
                item.data_alteracao = pItem.data_alteracao;
                item.usuario = pItem.usuario;
                contextoEF.SaveChanges();
            }
            return true;
        }

        public bool Excluir_Equipe(datas_aulas_professor pItem)
        {
            presenca_professor item_presenca_professor = new presenca_professor();
            item_presenca_professor.id_aula = pItem.id_aula;
            item_presenca_professor.id_professor = pItem.id_professor;
            item_presenca_professor.tipo_professor = pItem.tipo_professor;
            Excluir_presenca_professor(item_presenca_professor);

            datas_aulas_professor item;
            item = contextoEF.datas_aulas_professor.Where(x => x.id_aula == pItem.id_aula && x.id_professor == pItem.id_professor && x.tipo_professor == pItem.tipo_professor).SingleOrDefault();
            contextoEF.datas_aulas_professor.Remove(item);
            contextoEF.SaveChanges();
            return true;
        }

        public bool Alterar_HoraEquipe(datas_aulas_professor pItem)
        {
            datas_aulas_professor item;
            item = contextoEF.datas_aulas_professor.Where(x => x.id_aula == pItem.id_aula && x.id_professor == pItem.id_professor &&  x.tipo_professor == pItem.tipo_professor).SingleOrDefault();
            item.hora_aula = pItem.hora_aula;
            item.usuario = pItem.usuario;
            contextoEF.SaveChanges();

            return true;
        }

        public List<professores> ListaEquipeDisponiveis_Aula(datas_aulas_professor pItem, oferecimentos_professores pItem2)
        {
            var sAux = contextoEF.datas_aulas_professor.Where(x => x.id_aula == pItem.id_aula && x.tipo_professor == pItem.tipo_professor).Select(x => x.id_professor).ToArray();

            var sAux2 = contextoEF.oferecimentos_professores.Where(x => x.id_oferecimento == pItem2.id_oferecimento && x.tipo_professor == pItem2.tipo_professor && !sAux.Contains(x.id_professor)).Select(x => x.id_professor).ToArray();

            var c = contextoEF.professores.AsQueryable();
            List<professores> lista = new List<professores>();
            
            //lista = contextoEF.professores.Where(x => !sAux.Contains(x.id_professor)).ToList();

            c = c.Where(x => sAux2.Contains(x.id_professor) && x.status != "inativado" && x.email_confirmado == 1);

            lista = c.OrderBy(x => x.nome).ToList();

            return lista;
        }

        public List<alunos> ListaAlunosDisponiveis(oferecimentos pItem_Oferecimento, alunos pItem_Aluno, int qIdTipoCurso, int qIdCurso, int qIdTurma)
        {
            var c = contextoEF.alunos.AsQueryable();
            List<alunos> lista = new List<alunos>();
            //var sIdAlunosOferecimento = pItem_Oferecimento.matricula_oferecimento.ToList().Select(x => x.id_aluno).ToArray();
            //contextoEF.matricula_turma.Where(x => x.id_turma == pItem_Matricula.id_turma && x.id_area_concentracao == pItem_Matricula.id_area_concentracao).Select(x => x.id_aluno).ToArray();
            var sAux = contextoEF.matricula_turma.ToList().Select(x=> x.id_aluno).ToArray();
            //lista = contextoEF.professores.Where(x => !sAux.Contains(x.id_professor)).ToList();
            
            //c = c.Where(x => !sIdAlunosOferecimento.Contains(x.idaluno) && sAux.Contains(x.idaluno) && x.status != "inativado");

            if (qIdTurma != 0)
            {
                sAux = contextoEF.matricula_turma.Where(x => x.turmas.id_turma == qIdTurma).ToList().Select(x => x.id_aluno).ToArray();
            }
            else if (qIdCurso != 0)
            {
                sAux = contextoEF.matricula_turma.Where(x => x.turmas.id_curso == qIdCurso).ToList().Select(x => x.id_aluno).ToArray();
            }
            else if (qIdTipoCurso != 0)
            {
                sAux = contextoEF.matricula_turma.Where(x => x.turmas.cursos.id_tipo_curso == qIdTipoCurso).ToList().Select(x => x.id_aluno).ToArray();
            }

            c = c.Where(x => sAux.Contains(x.idaluno) && x.status != "inativado");

            if (pItem_Aluno.idaluno != 0)
            {
                c = c.Where(x => x.idaluno == pItem_Aluno.idaluno);
            }

            if (pItem_Aluno.nome != null && pItem_Aluno.nome != "")
            {
                c = c.Where(x => x.nome.Contains(pItem_Aluno.nome));
            }

            lista = c.OrderBy(x => x.nome).ToList();

            return lista;
        }

        public Boolean MatricularAluno(matricula_oferecimento pItem, notas pItem_Nota)
        {
            alunos item;
            item = contextoEF.alunos.Where(x => x.idaluno == pItem.id_aluno).SingleOrDefault();
            pItem.alunos = item;

            turmas item_turma;
            item_turma = contextoEF.turmas.Where(x => x.id_turma == pItem.id_turma).SingleOrDefault();
            pItem.turmas = item_turma;

            contextoEF.matricula_oferecimento.Add(pItem);
            contextoEF.notas.Add(pItem_Nota);
            contextoEF.SaveChanges();
            return true;
        }

        public Boolean ExcluirMatriculaAluno(matricula_oferecimento pItem)
        {
            //presenca pItem_presenca;
            //pItem_presenca = contextoEF.presenca.Where(x => x.id_aluno == pItem.id_aluno && x.id_oferecimento == pItem.id_oferecimento).SingleOrDefault();
            //if (pItem_presenca != null)
            //{
            //    contextoEF.presenca.Remove(pItem_presenca);
            //}

            contextoEF.presenca.RemoveRange(contextoEF.presenca.Where(x => x.id_aluno == pItem.id_aluno && x.id_oferecimento == pItem.id_oferecimento));
            contextoEF.Database.ExecuteSqlCommand("DELETE FROM historico_notas WHERE id_nota IN (SELECT id_nota FROM notas WHERE id_oferecimento=" + pItem.id_oferecimento + " AND id_aluno=" + pItem.id_aluno + ")");

            //List<notas> lista_nota;
            //historico_notas pItem_historico;
            //lista_nota = contextoEF.notas.Where(x => x.id_aluno == pItem.id_aluno && x.id_oferecimento == pItem.id_oferecimento).ToList();
            //foreach (var elemento in lista_nota)
            //{
            //    pItem_historico = contextoEF.historico_notas.Where(x => x.id_nota == elemento.id_nota).SingleOrDefault();
            //    if (pItem_historico != null)
            //    {

            //        contextoEF.historico_notas.RemoveRange(contextoEF.historico_notas.Where(x => x.id_nota == elemento.id_nota));
            //        contextoEF.historico_notas.Remove(pItem_historico);

            //        var studentList = contextoEF.historico_notas
            //            .SqlQuery("select * from historico_notas where id_nota =" + elemento.id_nota)
            //            .SingleOrDefault<historico_notas>();

            //        contextoEF.SaveChanges();
            //    }
            //}

            contextoEF.notas.RemoveRange(contextoEF.notas.Where(x => x.id_aluno == pItem.id_aluno && x.id_oferecimento == pItem.id_oferecimento));

            //notas pItem_Nota;
            //pItem_Nota = contextoEF.notas.Where(x => x.id_aluno == pItem.id_aluno && x.id_oferecimento == pItem.id_oferecimento).SingleOrDefault();
            //if (pItem_Nota != null)
            //{
            //    contextoEF.notas.Remove(pItem_Nota);
            //}
            
            matricula_oferecimento pItem_Matricula;
            pItem_Matricula = contextoEF.matricula_oferecimento.Where(x => x.id_matricula_oferecimento == pItem.id_matricula_oferecimento).SingleOrDefault();
            contextoEF.matricula_oferecimento.Remove(pItem_Matricula);

            contextoEF.SaveChanges();
            return true;
        }

        public Boolean EditarNota(notas pItem)
        {
            notas item;
            item = contextoEF.notas.Where(x => x.id_nota == pItem.id_nota).FirstOrDefault();

            contextoEF.Database.ExecuteSqlCommand("INSERT INTO historico_notas (id_nota, conceito, autorizado, data, usuario) VALUES(" + item.id_nota + ", '" + item.conceito + "', '" + item.autorizado + "', '" + String.Format("{0:yyyyMMdd HH:mm:ss}", item.data_alteracao) + "', '" + item.usuario + "'); ");

            item.conceito = pItem.conceito;
            item.autorizado = pItem.autorizado;
            item.data_alteracao = pItem.data_alteracao;
            item.usuario = pItem.usuario;

            contextoEF.SaveChanges();
            return true;
        }

        public Boolean CriarNota(notas pItem)
        {
            contextoEF.notas.Add(pItem);
            contextoEF.SaveChanges();
            return true;
        }

        public Boolean CriarPresenca(presenca pItem)
        {
            contextoEF.presenca.Add(pItem);
            contextoEF.SaveChanges();
            return true;
        }

        public Boolean AlterarPresenca(presenca pItem)
        {
            presenca item;
            item = contextoEF.presenca.Where(x => x.id_oferecimento == pItem.id_oferecimento && x.id_aula == pItem.id_aula && x.id_aluno == pItem.id_aluno).SingleOrDefault();
            if (item.presente != pItem.presente)
            {
                item.presente = pItem.presente;
                item.data_alteracao = pItem.data_alteracao;
                item.usuario = pItem.usuario;
                contextoEF.SaveChanges();
            }
            return true;
        }

        public List<oferecimentos> ListaItemCombo()
        {
            var c = contextoEF.oferecimentos.AsQueryable();
            List<oferecimentos> lista = new List<oferecimentos>();
            lista = c.Include(x => x.quadrimestres).Include(x => x.disciplinas).Include(x => x.datas_aulas).OrderByDescending(x => x.quadrimestre).OrderBy(x => x.disciplinas.nome).ToList();
            return lista;
        }

        public List<oferecimentos> ListaItemCombo(List<cursos> lista_curso)
        {
            var c = contextoEF.oferecimentos.AsQueryable();
            List<oferecimentos> lista = new List<oferecimentos>();

            var sAux = lista_curso.Select(x => x.id_curso).ToArray();

            var sAux2 = contextoEF.cursos_disciplinas.Where(x => sAux.Contains(x.id_curso)).Select(x => x.id_disciplina).ToArray();

            c = c.Where(x => sAux2.Contains(x.id_disciplina));

            lista = c.Include(x => x.quadrimestres).Include(x => x.disciplinas).Include(x => x.datas_aulas).OrderByDescending(x => x.quadrimestre).OrderBy(x => x.disciplinas.nome).ToList();

            return lista;
        }

        public List<oferecimentos> ListaItemCombo(cursos pItem, turmas pItem_turma)
        {
            var c = contextoEF.oferecimentos.AsQueryable();
            List<oferecimentos> lista = new List<oferecimentos>();

            if (pItem_turma.cod_turma != "" && pItem_turma.cod_turma != null)
            {
                var sIdTurma = contextoEF.turmas.Where(x => x.cod_turma == pItem_turma.cod_turma && x.id_curso == pItem.id_curso).FirstOrDefault().id_turma;

                var sAux = contextoEF.matricula_oferecimento.Where(x => x.id_turma == sIdTurma).Select(x=> x.id_oferecimento).ToArray();

                c = c.Where(x => sAux.Contains(x.id_oferecimento));
            }
            else
            {
                var sAux2 = contextoEF.cursos_disciplinas.Where(x => x.id_curso == pItem.id_curso).Select(x => x.id_disciplina).ToArray();

                c = c.Where(x => sAux2.Contains(x.id_disciplina));
            }

            lista = c.OrderByDescending(x => x.quadrimestre).OrderBy(x => x.disciplinas.nome).ToList();

            //lista = c.Include(x => x.quadrimestres).Include(x => x.disciplinas).Include(x => x.datas_aulas).OrderByDescending(x => x.quadrimestre).OrderBy(x => x.disciplinas.nome).ToList();

            return lista;
        }

        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}

