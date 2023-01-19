
using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace Repositorio_C
{
    public class CursoRepositorio : IDisposable
    {
        private Entities contextoEF;

        public CursoRepositorio()
        {
            contextoEF = new Entities();
        }

        public cursos BuscaItem(cursos pItem)
        {
            cursos item = new cursos();
            item = contextoEF.cursos.Where(x => x.id_curso == pItem.id_curso).SingleOrDefault();
            //item = contextoEF.cursos.Include(x => x.cursos_coordenadores).Include(x => x.cursos_disciplinas).Where(x => x.id_curso == pItem.id_curso).SingleOrDefault();
            return item;
        }

        public cursos VerificaItemMesmaSigla(cursos pItem)
        {
            cursos item = new cursos();
            item = contextoEF.cursos.Include(x => x.cursos_disciplinas).Include(x => x.cursos_coordenadores).Where(x => x.id_curso != pItem.id_curso && x.sigla == pItem.sigla).SingleOrDefault();
            return item;
        }

        public cursos VerificaItemMesmoTipoCurso_MesmoNome(cursos pItem)
        {
            cursos item = new cursos();
            item = contextoEF.cursos.Include(x => x.cursos_disciplinas).Include(x => x.cursos_coordenadores).Where(x => x.id_curso != pItem.id_curso && x.id_tipo_curso == pItem.id_tipo_curso && x.nome == pItem.nome).SingleOrDefault();
            return item;
        }

        public cursos CriarItem(cursos pItem)
        {
            contextoEF.cursos.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public cursos AlterarStatus(cursos pItem)
        {
            cursos item = new cursos();
            item = contextoEF.cursos.Where(x => x.id_curso == pItem.id_curso).SingleOrDefault();
            item.status = pItem.status;
            item.data_alteracao = DateTime.Now;
            item.usuario = pItem.usuario;
            contextoEF.SaveChanges();
            item = contextoEF.cursos.Where(x => x.id_curso == pItem.id_curso).SingleOrDefault();
            return item;
        }

        public bool IncluirCoordenador_Curso(cursos_coordenadores pItem)
        {
            contextoEF.cursos_coordenadores.Add(pItem);
            contextoEF.SaveChanges();

            List<cursos_coordenadores> lista;
            lista = contextoEF.cursos_coordenadores.Include(x => x.professores).Where(x => x.id_curso == pItem.id_curso && x.id_professor == pItem.id_professor).ToList();
            return true;
        }

        public bool IncluirDisciplina_Curso(cursos_disciplinas pItem)
        {
            contextoEF.cursos_disciplinas.Add(pItem);
            contextoEF.SaveChanges();

            List<cursos_disciplinas> lista;
            lista = contextoEF.cursos_disciplinas.Include(x => x.disciplinas).Where(x => x.id_curso == pItem.id_curso && x.id_disciplina == pItem.id_disciplina).ToList();
            return true;
        }

        public cursos ExcluirCoordenador_Curso(cursos_coordenadores pItem)
        {
            cursos_coordenadores item = new cursos_coordenadores();
            cursos itemCurso = new cursos();
            item = contextoEF.cursos_coordenadores.Where(x => x.id_curso == pItem.id_curso && x.id_professor == pItem.id_professor).SingleOrDefault();
            contextoEF.cursos_coordenadores.Remove(item);
            contextoEF.SaveChanges();
            itemCurso = contextoEF.cursos.Where(x => x.id_curso == pItem.id_curso).SingleOrDefault();
            return itemCurso;
        }

        public cursos ExcluirDisciplina_Curso(cursos_disciplinas pItem)
        {
            cursos_disciplinas item = new cursos_disciplinas();
            cursos itemCurso = new cursos();
            item = contextoEF.cursos_disciplinas.Where(x => x.id_curso == pItem.id_curso && x.id_disciplina == pItem.id_disciplina).SingleOrDefault();
            contextoEF.cursos_disciplinas.Remove(item);
            contextoEF.SaveChanges();
            itemCurso = contextoEF.cursos.Where(x => x.id_curso == pItem.id_curso).SingleOrDefault();
            return itemCurso;
        }

        public bool AlterarObrigatoriedade_Curso(cursos_disciplinas pItem)
        {
            cursos_disciplinas item = new cursos_disciplinas();
            item = contextoEF.cursos_disciplinas.Where(x => x.id_curso == pItem.id_curso && x.id_disciplina == pItem.id_disciplina).SingleOrDefault();
            item.obrigatoria = pItem.obrigatoria;
            contextoEF.SaveChanges();
            return true;
        }

        public Boolean AlterarItem(cursos pItem)
        {
            cursos item = new cursos();
            item = contextoEF.cursos.Where(x => x.id_curso == pItem.id_curso).SingleOrDefault();

            item.id_tipo_curso = pItem.id_tipo_curso;
            item.sigla = pItem.sigla;
            item.nome = pItem.nome;
            item.carga_horaria  = pItem.carga_horaria ;
            item.observacao  = pItem.observacao ;
            item.portaria_mec  = pItem.portaria_mec ;
            item.data_portaria_mec = pItem.data_portaria_mec;
            item.data_diario_oficial = pItem.data_diario_oficial;
            item.conceito_capes = pItem.conceito_capes;
            item.numero_capes = pItem.numero_capes;
            item.creditos  = pItem.creditos ;
            item.num_max_disciplinas  = pItem.num_max_disciplinas ;
            //item.status  = pItem.status ;
            item.data_cadastro  = pItem.data_cadastro ;
            item.data_alteracao  = pItem.data_alteracao;
            item.usuario  = pItem.usuario ;
            //===============
            item.statusHomepagePreview = pItem.statusHomepagePreview;
            item.statusBotaoPreview = pItem.statusBotaoPreview;
            item.descricao_homepagePreview = pItem.descricao_homepagePreview;
            item.corpo_docentePreview = pItem.corpo_docentePreview;
            item.nome_imagemPreview = pItem.nome_imagemPreview;
            item.data_imagemPreview = pItem.data_imagemPreview;
            item.statusAprovado = pItem.statusAprovado;
            item.obs_preview = pItem.obs_preview;
            //=====================
            item.statusBotaoPreview_en = pItem.statusBotaoPreview_en;
            item.nome_en = pItem.nome_en;
            item.descricao_homepagePreview_en = pItem.descricao_homepagePreview_en;
            item.corpo_docentePreview_en = pItem.corpo_docentePreview_en;
            contextoEF.SaveChanges();
            return true;
        }

        public Boolean AlterarItem_Aprovacao(cursos pItem)
        {
            cursos item = new cursos();
            item = contextoEF.cursos.Where(x => x.id_curso == pItem.id_curso).SingleOrDefault();

            //===============
            item.statusHomepage = pItem.statusHomepagePreview;
            item.statusBotao = pItem.statusBotaoPreview;
            item.descricao_homepage = pItem.descricao_homepagePreview;
            item.corpo_docente = pItem.corpo_docentePreview;
            item.nome_imagem = pItem.nome_imagemPreview;
            item.data_imagem = pItem.data_imagemPreview;
            item.data_aprovacao = pItem.data_aprovacao;
            item.usuario_aprovacao = pItem.usuario_aprovacao;
            item.statusAprovado = pItem.statusAprovado;
            item.obs_preview = "";
            //==
            item.statusBotao_en = pItem.statusBotaoPreview_en;
            item.descricao_homepage_en = pItem.descricao_homepagePreview_en;
            item.corpo_docente_en = pItem.corpo_docentePreview_en;

            contextoEF.SaveChanges();
            return true;
        }

        public Boolean AlterarItem_Reprovacao(cursos pItem)
        {
            cursos item = new cursos();
            item = contextoEF.cursos.Where(x => x.id_curso == pItem.id_curso).SingleOrDefault();

            //===============
            item.data_reprovacao = pItem.data_reprovacao;
            item.usuario_aprovacao = pItem.usuario_aprovacao;
            item.statusAprovado = pItem.statusAprovado;
            item.obs_preview = pItem.obs_preview;

            contextoEF.SaveChanges();
            return true;
        }

        public List<tipos_curso> ListaTipoCurso()
        {
            List<tipos_curso> lista = new List<tipos_curso>();
            lista = contextoEF.tipos_curso.ToList();

            return lista;
        }

        public List<cursos> ListaItemAprovacaoHomePage()
        {
            var c = contextoEF.cursos.AsQueryable();
            List<cursos> lista = new List<cursos>();

            c = c.Where(x => x.statusAprovado == 0);
            
            lista = c.OrderBy(x => x.sigla).ToList();

            return lista;
        }

        public List<cursos> ListaItemReprovacaoHomePage(string qUsuario)
        {
            var c = contextoEF.cursos.AsQueryable();
            List<cursos> lista = new List<cursos>();

            c = c.Where(x => x.statusAprovado == 2 && x.usuario == qUsuario);

            lista = c.OrderBy(x => x.sigla).ToList();

            return lista;
        }

        public List<cursos> ListaItem(cursos pItem)
        {
            var c = contextoEF.cursos.AsQueryable();
            List<cursos> lista = new List<cursos>();

            if (pItem.id_curso != 0)
            {
                c = c.Where(x => x.id_curso == pItem.id_curso);
            }

            if (pItem.nome != null)
            {
                c = c.Where(x => x.nome.Contains(pItem.nome));
            }

            if (pItem.sigla != null)
            {
                c = c.Where(x => x.sigla.Contains(pItem.sigla));
            }

            if (pItem.id_tipo_curso != 0)
            {
                c = c.Where(x => x.id_tipo_curso == pItem.id_tipo_curso);
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

            // 0 = Aguardando aprovação -- 1 = Aprovado-- 2 = Reprovado-- 3 = Sem página
            //(4 = não existe)
            if (pItem.statusAprovado == 0 || pItem.statusAprovado == 1 || pItem.statusAprovado == 2 || pItem.statusAprovado == 3)
            {
                c = c.Where(x => x.statusAprovado == pItem.statusAprovado);
            }

            lista = c.OrderBy(x => x.sigla).ToList();

            return lista;
        }

        public List<cursos> ListaItem(turmas pItem)
        {
            var c = contextoEF.cursos.AsQueryable();
            List<cursos> lista = new List<cursos>();

            if (pItem.cod_turma != "")
            {
                var sAux = contextoEF.turmas.Where(x => x.cod_turma == pItem.cod_turma).Select(x => x.id_curso).ToArray();

                c = c.Where(x => sAux.Contains(x.id_curso));
            }
            c = c.Where(x => x.status != "inativado");
               
            lista = c.OrderBy(x => x.sigla).ToList();

            return lista;
        }

        public List<disciplinas> ListaDisciplinas(cursos pItem)
        {
            List<cursos_disciplinas> lista_CursoDisciplina;
            lista_CursoDisciplina = contextoEF.cursos_disciplinas.Where(x => x.id_curso == pItem.id_curso).ToList();

            var sAux = lista_CursoDisciplina.Select(x => x.id_disciplina).ToArray();

            var c = contextoEF.disciplinas.AsQueryable();
            List<disciplinas> lista = new List<disciplinas>();

            c = c.Where(x => sAux.Contains(x.id_disciplina));

            lista = c.OrderBy(x => x.nome).ToList();

            return lista;
        }

        public List<cursos_disciplinas> ListaDisciplinasCursos(cursos pItem)
        {
            var c = contextoEF.cursos_disciplinas.AsQueryable();
            List<cursos_disciplinas> lista = new List<cursos_disciplinas>();

            c = c.Where(x => x.id_curso == pItem.id_curso);

            lista = c.OrderBy(x => x.disciplinas.nome).ToList();

            return lista;
        }

        public List<cursos_coordenadores> ListaCoordenadores(cursos pItem)
        {
            List<cursos_coordenadores> lista = new List<cursos_coordenadores>();
            lista = contextoEF.cursos_coordenadores.Where(x => x.id_curso == pItem.id_curso).OrderBy(x=> x.curso_tipo_coordenador.ordem).ToList();
            //lista = contextoEF.cursos_coordenadores.Include(x => x.professores).Where(x => x.id_curso == pItem.id_curso).ToList();
            return lista;
        }

        public List<professores> ListaProfessoresDisponiveis(cursos pItem, professores pItemProfessor)
        {
            var c = contextoEF.professores.AsQueryable();
            List<professores> lista = new List<professores>();
            var sAux = contextoEF.cursos_coordenadores.Where(x => x.id_curso == pItem.id_curso).Select(x => x.id_professor).ToArray();
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

        public List<disciplinas> ListaDisciplinasDisponiveis(cursos pItem, disciplinas pItemDisciplina)
        {
            var c = contextoEF.disciplinas.AsQueryable();
            List<disciplinas> lista = new List<disciplinas>();
            var sAux = contextoEF.cursos_disciplinas.Where(x => x.id_curso == pItem.id_curso).Select(x => x.id_disciplina).ToArray();

            //Procurar por qualquer disciplina que esteja já associada a qualquer curso. Caso esteja não é pra mostrar a discuplina.
            //Isso pq não tem como o SAPIENS entender para qual curso aquela disciplina está sendo oferecida, 
            //e com isso poderia-se calcular valores diferentes de cursos para o oferecimento.
            var sAux2 = contextoEF.cursos_disciplinas.Select(x => x.id_disciplina).ToArray();
            c = c.Where(x => !sAux.Contains(x.id_disciplina) && !sAux2.Contains(x.id_disciplina) && x.status != "inativado");

            //c = c.Where(x => !sAux.Contains(x.id_disciplina) && x.status != "inativado");

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

        public List<cursos> ListaCursosHomepage(cursos pItem)
        {
            var c = contextoEF.cursos.AsQueryable();
            List<cursos> lista = new List<cursos>();

            c = c.Where(x => x.id_tipo_curso == pItem.id_tipo_curso);

            c = c.Where(x => x.status != "inativado");

            c = c.Where(x => x.statusHomepage == 1);

            lista = c.OrderBy(x => x.sigla).ToList();

            return lista;
        }

        public List<tipos_curso> ListaTipoCurso(tipos_curso pItem)
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


            lista = c.OrderBy(x => x.id_tipo_curso).ToList();

            return lista;
        }

        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}

