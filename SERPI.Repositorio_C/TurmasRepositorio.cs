using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace Repositorio_C
{
    public class TurmasRepositorio : IDisposable
    {
        private Entities contextoEF;

        public TurmasRepositorio()
        {
            contextoEF = new Entities();
        }

        public turmas BuscaItem(turmas pItem)
        {
            turmas item = new turmas();
            //item = contextoEF.turmas.Include(x => x.turmas_coordenadores).Include(x => x.turmas_disciplinas).Include(x => x.cursos.tipos_curso).Include(x => x.quadrimestres).Where(x => x.id_turma == pItem.id_turma).SingleOrDefault();
            item = contextoEF.turmas.Where(x => x.id_turma == pItem.id_turma).SingleOrDefault();
            return item;
        }

        public turmas VerificaItemMesmoCodigo(turmas pItem)
        {
            turmas item = new turmas();
            item = contextoEF.turmas.Include(x => x.turmas_disciplinas).Include(x => x.turmas_coordenadores).Include(x => x.cursos.tipos_curso).Include(x => x.quadrimestres).Where(x => x.id_turma != pItem.id_turma && x.cod_turma == pItem.cod_turma).SingleOrDefault();
            return item;
        }
        public string BuscaItem_NumeroMaximo(turmas pItem)
        {
            List<turmas> lista = new List<turmas>();
            string sAux;
            int iAux;
            lista = contextoEF.turmas.Where(x => x.id_curso == pItem.id_curso).ToList();
            //lista = contextoEF.turmas.Include(x => x.cursos).Include(x => x.turmas_disciplinas).Include(x => x.turmas_coordenadores).Include(x => x.quadrimestres).Include(x => x.cursos.tipos_curso).Where(x => x.id_curso == pItem.id_curso).ToList();

            if (lista.Count != 0)
            {

                sAux = lista.OrderByDescending(x=> x.cod_turma).FirstOrDefault().cod_turma;
                iAux = Convert.ToInt32(sAux.Substring(1, sAux.Length - 1)) + 1;
                if (iAux <10)
                {
                    sAux = "T0" + iAux.ToString();
                }
                else
                {
                    sAux = "T" + iAux.ToString();
                }
            }
            else
            {
                sAux = "T01";
            }
            return sAux;
        }

        public turmas CriarItem(turmas pItem)
        {
            contextoEF.turmas.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public Boolean AlterarItem(turmas pItem)
        {
            turmas item = new turmas();
            item = contextoEF.turmas.Include(x => x.cursos).Include(x => x.turmas_disciplinas).Include(x => x.turmas_coordenadores).Include(x => x.cursos.tipos_curso).Include(x => x.quadrimestres).Where(x => x.id_turma == pItem.id_turma).SingleOrDefault();

            //item.ativo = pItem.ativo;
            //item.cod_turma = pItem.cod_turma;
            item.data_inicio = pItem.data_inicio;
            item.data_fim = pItem.data_fim;
            item.id_curso = pItem.id_curso;
            item.carga_horaria = pItem.carga_horaria;
            item.observacao = pItem.observacao;
            item.portaria_mec = pItem.portaria_mec;
            item.data_portaria_mec = pItem.data_portaria_mec;
            item.data_diario_oficial = pItem.data_diario_oficial;
            item.conceito_capes = pItem.conceito_capes;
            item.numero_capes = pItem.numero_capes;
            item.creditos = pItem.creditos;
            item.num_max_disciplinas = pItem.num_max_disciplinas;
            //item.status  = pItem.status ;
            item.data_cadastro = pItem.data_cadastro;
            item.data_alteracao = pItem.data_alteracao;
            item.usuario = pItem.usuario;
            item.quadrimestre = pItem.quadrimestre;
            item.data_limite_matricula = pItem.data_limite_matricula;

            contextoEF.SaveChanges();
            return true;
        }

        public turmas AlterarStatus(turmas pItem)
        {
            turmas item = new turmas();
            item = contextoEF.turmas.Where(x => x.id_turma == pItem.id_turma).SingleOrDefault();
            item.ativo = pItem.ativo;
            item.status = pItem.status;
            item.data_alteracao = DateTime.Now;
            item.usuario = pItem.usuario;
            contextoEF.SaveChanges();
            item = contextoEF.turmas.Include(x => x.cursos).Include(x => x.turmas_disciplinas).Include(x => x.turmas_coordenadores).Include(x => x.cursos.tipos_curso).Where(x => x.id_turma == pItem.id_turma).SingleOrDefault();
            return item;
        }

        public bool IncluirCoordenador_Turma(turmas_coordenadores pItem)
        {
            contextoEF.turmas_coordenadores.Add(pItem);
            contextoEF.SaveChanges();

            List<turmas_coordenadores> lista;
            lista = contextoEF.turmas_coordenadores.Include(x => x.professores).Where(x => x.id_turma == pItem.id_turma && x.id_professor == pItem.id_professor).ToList();
            return true;
        }

        public bool IncluirDisciplina_Turma(turmas_disciplinas pItem)
        {
            contextoEF.turmas_disciplinas.Add(pItem);
            contextoEF.SaveChanges();

            List<turmas_disciplinas> lista;
            lista = contextoEF.turmas_disciplinas.Include(x => x.disciplinas).Where(x => x.id_turma == pItem.id_turma && x.id_disciplina == pItem.id_disciplina).ToList();
            return true;
        }

        public turmas ExcluirCoordenador_Turma(turmas_coordenadores pItem)
        {
            turmas_coordenadores item = new turmas_coordenadores();
            turmas itemTurma = new turmas();
            item = contextoEF.turmas_coordenadores.Where(x => x.id_turma == pItem.id_turma && x.id_professor == pItem.id_professor).SingleOrDefault();
            contextoEF.turmas_coordenadores.Remove(item);
            contextoEF.SaveChanges();
            itemTurma = contextoEF.turmas.Include(x => x.cursos).Include(x => x.turmas_disciplinas).Include(x => x.turmas_coordenadores).Include(x => x.cursos.tipos_curso).Include(x => x.quadrimestres).Where(x => x.id_turma == pItem.id_turma).SingleOrDefault();
            return itemTurma;
        }

        public turmas ExcluirDisciplina_Turma(turmas_disciplinas pItem)
        {
            turmas_disciplinas item = new turmas_disciplinas();
            turmas itemTurma = new turmas();
            item = contextoEF.turmas_disciplinas.Where(x => x.id_turma == pItem.id_turma && x.id_disciplina == pItem.id_disciplina).SingleOrDefault();
            contextoEF.turmas_disciplinas.Remove(item);
            contextoEF.SaveChanges();
            itemTurma = contextoEF.turmas.Include(x => x.cursos).Include(x => x.turmas_disciplinas).Include(x => x.turmas_coordenadores).Include(x => x.cursos.tipos_curso).Include(x => x.quadrimestres).Where(x => x.id_turma == pItem.id_turma).SingleOrDefault();
            return itemTurma;
        }

        public List<turmas> ListaItem(turmas pItem)
        {
            var c = contextoEF.turmas.AsQueryable();
            List<turmas> lista = new List<turmas>();

            if (pItem.id_turma != 0)
            {
                c = c.Where(x => x.id_turma == pItem.id_turma);
            }


            if (pItem.cod_turma != null)
            {
                c = c.Where(x => x.cod_turma == pItem.cod_turma);
            }

            if (pItem.cursos.id_tipo_curso != 0)
            {
                c = c.Where(x => x.cursos.id_tipo_curso == pItem.cursos.id_tipo_curso);
            }

            if (pItem.id_curso != 0)
            {
                c = c.Where(x => x.id_curso == pItem.id_curso);
            }

            if (pItem.status != "")
            {
                if (pItem.status == "ativado")
                {
                    c = c.Where(x => x.ativo == true);
                }
                else if (pItem.status == "inativado")
                {
                    c = c.Where(x => x.ativo == false);
                }
            }


            //lista = c.Include(x => x.cursos).Include(x => x.turmas_disciplinas).Include(x => x.turmas_coordenadores).Include(x => x.cursos.tipos_curso).Include(x => x.quadrimestres).OrderByDescending(x => x.cod_turma).ToList();

            lista = c.OrderByDescending(x => x.cod_turma).ToList();

            return lista;
        }

        public List<turmas> ListaItemCombo(cursos pItem)
        {
            var c = contextoEF.turmas.AsQueryable();
            List<turmas> lista = new List<turmas>();

            if (pItem.id_curso != 0)
            {
                c = c.Where(x => x.id_curso == pItem.id_curso);
            }
            else if (pItem.id_tipo_curso != 0)
            {
                var sAux = contextoEF.cursos.Where(x => x.id_tipo_curso == pItem.id_tipo_curso).Select(x => x.id_curso).ToArray();

                c = c.Where(x => sAux.Contains(x.id_curso));
            }

            c = c.Where(x => x.ativo == true);

            lista = c.Include(x => x.turmas_disciplinas).Include(x => x.turmas_coordenadores).OrderByDescending(x => x.cod_turma).ToList();

            //lista = c.Include(x => x.cursos).Include(x => x.turmas_disciplinas).Include(x => x.turmas_coordenadores).Include(x => x.cursos.tipos_curso).Include(x => x.quadrimestres).OrderByDescending(x => x.cod_turma).ToList();

            return lista;
        }

        public List<disciplinas> ListaDisciplinas(turmas pItem)
        {
            List<turmas_disciplinas> lista_TurmaDisciplina;
            lista_TurmaDisciplina = contextoEF.turmas_disciplinas.Where(x => x.id_turma == pItem.id_turma).ToList();

            var sAux = lista_TurmaDisciplina.Select(x => x.id_disciplina).ToArray();

            var c = contextoEF.disciplinas.AsQueryable();
            List<disciplinas> lista = new List<disciplinas>();

            c = c.Where(x => sAux.Contains(x.id_disciplina));

            lista = c.OrderBy(x => x.nome).ToList();

            return lista;
        }

        public List<turmas_coordenadores> ListaCoordenadores(turmas pItem)
        {
            List<turmas_coordenadores> lista = new List<turmas_coordenadores>();
            lista = contextoEF.turmas_coordenadores.Include(x => x.professores).Where(x => x.id_turma == pItem.id_turma).ToList();
            return lista;
        }

        public List<professores> ListaProfessoresDisponiveis(turmas pItem, professores pItemProfessor)
        {
            var c = contextoEF.professores.AsQueryable();
            List<professores> lista = new List<professores>();
            var sAux = contextoEF.turmas_coordenadores.Where(x => x.id_turma == pItem.id_turma).Select(x => x.id_professor).ToArray();
            //lista = contextoEF.professores.Where(x => !sAux.Contains(x.id_professor)).ToList();

            c = c.Where(x => !sAux.Contains(x.id_professor) && x.status != "inativado");

            if (pItemProfessor.cpf != "")
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

        public List<disciplinas> ListaDisciplinasDisponiveis(turmas pItem, disciplinas pItemDisciplina)
        {
            var c = contextoEF.disciplinas.AsQueryable();
            List<disciplinas> lista = new List<disciplinas>();
            var sAux = contextoEF.turmas_disciplinas.Where(x => x.id_turma == pItem.id_turma).Select(x => x.id_disciplina).ToArray();
            //lista = contextoEF.professores.Where(x => !sAux.Contains(x.id_professor)).ToList();

            c = c.Where(x => !sAux.Contains(x.id_disciplina) && x.status != "inativado");

            if (pItemDisciplina.codigo != "")
            {
                c = c.Where(x => x.codigo == pItemDisciplina.codigo);
            }

            if (pItemDisciplina.nome != "")
            {
                c = c.Where(x => x.nome.Contains(pItemDisciplina.nome));
            }

            lista = c.OrderBy(x => x.nome).ToList();

            return lista;
        }

        public List<alunos> ListaAlunosDisponiveis(matricula_turma pItem_Matricula, alunos pItem_Aluno)
        {
            var c = contextoEF.alunos.AsQueryable();
            List<alunos> lista = new List<alunos>();
            //contextoEF.matricula_turma.Where(x => x.id_turma == pItem_Matricula.id_turma && x.id_area_concentracao == pItem_Matricula.id_area_concentracao).Select(x => x.id_aluno).ToArray();
            var sAux = contextoEF.matricula_turma.Where(x => x.id_turma == pItem_Matricula.id_turma).Select(x => x.id_aluno).ToArray();
            //lista = contextoEF.professores.Where(x => !sAux.Contains(x.id_professor)).ToList();

            c = c.Where(x => !sAux.Contains(x.idaluno) && x.status != "inativado");

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

        public Boolean AlterarAreaMatriculaTurma(matricula_turma pItem)
        {
            matricula_turma item;
            item = contextoEF.matricula_turma.Where(x => x.id_matricula_turma == pItem.id_matricula_turma).SingleOrDefault();
            item.id_area_concentracao = pItem.id_area_concentracao;
            item.data_alteracao = pItem.data_alteracao;
            item.usuario = pItem.usuario;

            contextoEF.SaveChanges();
            return true;
        }

        public List<matricula_oferecimento> Lista_VerificaAlunoMatriculadoOferecimento(matricula_turma pItem)
        {
            matricula_turma item_matricula = contextoEF.matricula_turma.Where(x => x.id_matricula_turma == pItem.id_matricula_turma).SingleOrDefault();
            List<matricula_oferecimento> lista = contextoEF.matricula_oferecimento.Where(x => x.id_aluno == item_matricula.id_aluno && x.id_turma == item_matricula.id_turma).ToList();
            return lista;
        }

        public Boolean ExcluirMatriculaAlunoTurma(matricula_turma pItem)
        {
            int id_Inscricao = 0;
            List<fichas_inscricao> lista_fichas_inscricao;
            lista_fichas_inscricao = contextoEF.fichas_inscricao.ToList();
            bool bMudou = false;


            foreach (var elemento in lista_fichas_inscricao)
            {
                foreach (var elemento2 in elemento.matricula_turma)
                {
                    if (elemento2.id_matricula_turma == pItem.id_matricula_turma)
                    {
                        id_Inscricao = elemento.id_inscricao;
                        elemento2.fichas_inscricao.Remove(elemento);
                        bMudou = true;
                        break;
                    }
                }
            }
            if (bMudou)
            {
                contextoEF.SaveChanges();
            }
            

            if (id_Inscricao != 0)
            {
                List<historico_inscricao> lista_historico_inscricao;
                lista_historico_inscricao = contextoEF.historico_inscricao.Where(x => x.id_inscricao == id_Inscricao && x.status == "Matriculado").ToList();
                foreach (var elemento in lista_historico_inscricao)
                {
                    contextoEF.historico_inscricao.Remove(elemento);
                }
            }

            List<historico_matricula_turma> lista_historico_matricula_turma;
            lista_historico_matricula_turma = contextoEF.historico_matricula_turma.Where(x => x.id_matricula_turma == pItem.id_matricula_turma && x.situacao == "Matriculado").ToList();

            foreach (var elemento in lista_historico_matricula_turma)
            {
                contextoEF.historico_matricula_turma.Remove(elemento);
                contextoEF.SaveChanges();
            }

            matricula_turma item;
            item = contextoEF.matricula_turma.Where(x => x.id_matricula_turma == pItem.id_matricula_turma).SingleOrDefault();
            contextoEF.matricula_turma.Remove(item);
            contextoEF.SaveChanges();
            return true;
        }



        public matricula_turma IncluirAluno_Turma(matricula_turma pItem)
        {
            matricula_turma pItemNew;
            pItemNew = contextoEF.matricula_turma.Where(x => x.id_aluno == pItem.id_aluno && x.id_turma == pItem.id_turma).SingleOrDefault();

            if (pItemNew == null)
            {
                alunos item;
                item = contextoEF.alunos.Where(x => x.idaluno == pItem.id_aluno).SingleOrDefault();
                pItem.alunos = item;

                contextoEF.matricula_turma.Add(pItem);
                contextoEF.SaveChanges();

                turmas itemTurma = contextoEF.turmas.Where(x => x.id_turma == pItem.id_turma).SingleOrDefault();

                alunos_dataLimite_documentos_pendentes item_alunoDocumento = new alunos_dataLimite_documentos_pendentes();
                item_alunoDocumento.idaluno = pItem.id_aluno;
                item_alunoDocumento.data_limite = DateTime.Today.AddMonths(6);
                item_alunoDocumento.data_cadastro = DateTime.Today;
                item_alunoDocumento.usuario = pItem.usuario;
                item_alunoDocumento.observacao = "Inclusão na turma: " + itemTurma.cod_turma + " do curso: " + itemTurma.cursos.nome;
                item_alunoDocumento.tipo_registro = 0;
                IncluirAluno_DataLimiteDocumento(item_alunoDocumento);

            }
            else
            {
                pItem = pItemNew;
            }
            
            return pItem;
        }

        public alunos_dataLimite_documentos_pendentes IncluirAluno_DataLimiteDocumento(alunos_dataLimite_documentos_pendentes pItem)
        {
            contextoEF.alunos_dataLimite_documentos_pendentes.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public historico_matricula_turma IncluirHistorico_Matricula(historico_matricula_turma pItem)
        {
            historico_matricula_turma pItemNew = null;

            if (pItem.situacao == "Matriculado")
            {
                pItemNew = contextoEF.historico_matricula_turma.Where(x => x.id_matricula_turma == pItem.id_matricula_turma && x.situacao == "Matriculado").SingleOrDefault();
            }
            

            if (pItemNew == null)
            {
                contextoEF.historico_matricula_turma.Add(pItem);
                contextoEF.SaveChanges();
            }
            else
            {
                pItem = pItemNew;
            }
            
            return pItem;
        }

        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}


