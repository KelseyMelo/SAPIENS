using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace Repositorio_C
{
    public class AlunoRepositorio:IDisposable              
    {
        private Entities contextoEF;

        public AlunoRepositorio()
        {
            contextoEF = new Entities();
        }

        public alunos BuscaItem(alunos pItem)
        {
            alunos item = new alunos();
            item = contextoEF.alunos.Include(x => x.matricula_turma).Include(x=> x.matricula_turma.Select(y=> y.historico_matricula_turma)).Where(x => x.idaluno == pItem.idaluno).SingleOrDefault();
            return item;
        }

        public alunos BuscaItem(string qCPF)
        {
            alunos item = new alunos();
            item = contextoEF.alunos.Include(x => x.matricula_turma).Include(x => x.matricula_turma.Select(y => y.historico_matricula_turma)).Where(x => x.cpf == qCPF).FirstOrDefault();
            return item;
        }

        public alunos CriarItem(alunos pItem)
        {
            using (var contextoEF = new Entities())
            {
                contextoEF.alunos.Add(pItem);
                contextoEF.SaveChanges();
                return pItem;
            }
        }

        public Boolean AlterarItem(alunos pItem)
        {
            alunos item = new alunos();
            item = contextoEF.alunos.Where(x => x.idaluno == pItem.idaluno).SingleOrDefault();
            item.nome = pItem.nome;
            item.estrangeiro = pItem.estrangeiro;
            item.cpf = pItem.cpf;
            item.tipo_documento = pItem.tipo_documento;
            item.numero_documento = pItem.numero_documento;
            item.digito_num_documento = pItem.digito_num_documento;
            item.orgao_expedidor = pItem.orgao_expedidor;
            item.data_expedicao = pItem.data_expedicao;
            item.data_validade = pItem.data_validade;
            item.data_nascimento = pItem.data_nascimento;
            item.pais_nasc = pItem.pais_nasc;
            item.cidade_nasc = pItem.cidade_nasc;
            item.sexo = pItem.sexo;
            item.logradouro_res = pItem.logradouro_res;
            item.numero_res = pItem.numero_res;
            item.complemento_res = pItem.complemento_res;
            item.bairro_res = pItem.bairro_res;
            item.cidade_res = pItem.cidade_res;
            item.uf_res = pItem.uf_res;
            item.pais_res = pItem.pais_res;
            item.cep_res = pItem.cep_res;
            item.telefone_res = pItem.telefone_res;
            item.celular_res = pItem.celular_res;
            item.email = pItem.email;
            item.formacao = pItem.formacao;
            item.escola = pItem.escola;
            item.ano_graduacao = pItem.ano_graduacao;
            item.status = "alterado";
            //item.data_cadastro = pItem.data_cadastro;
            item.data_alteracao = DateTime.Now;
            item.usuario = pItem.usuario;
            item.nome_fantasia = pItem.nome_fantasia;
            item.empresa = pItem.empresa;
            item.cnpj = pItem.cnpj;
            item.ie = pItem.ie;
            item.contato = pItem.contato;
            item.email_contato = pItem.email_contato;
            item.logradouro_empresa = pItem.logradouro_empresa;
            item.numero_empresa = pItem.numero_empresa;
            item.complemento_empresa = pItem.complemento_empresa;
            item.bairro_empresa = pItem.bairro_empresa;
            item.cidade_empresa = pItem.cidade_empresa;
            item.uf_empresa = pItem.uf_empresa;
            item.pais_empresa = pItem.pais_empresa;
            item.cep_empresa = pItem.cep_empresa;
            item.telefone_empresa = pItem.telefone_empresa;
            item.telefone_empresa_ramal = pItem.telefone_empresa_ramal;
            item.cargo = pItem.cargo;
            item.email2 = pItem.email2;
            //item.entregou_rg = pItem.entregou_rg;
            //item.entregou_cpf = pItem.entregou_cpf;
            //item.entregou_historico = pItem.entregou_historico;
            //item.entregou_diploma = pItem.entregou_diploma;
            //item.entregou_comprovante_end = pItem.entregou_comprovante_end;
            //item.entregou_fotos = pItem.entregou_fotos;
            //item.entregou_certidao = pItem.entregou_certidao;
            //item.entregou_contrato = pItem.entregou_contrato;
            item.RefazerProficienciaIngles = pItem.RefazerProficienciaIngles;
            item.RefazerProvaPortugues = pItem.RefazerProvaPortugues;
            item.ocorrencias = pItem.ocorrencias;
            item.convenio = pItem.convenio;
            item.linha_pesquisa = pItem.linha_pesquisa;
            item.estado_nasc = pItem.estado_nasc;
            item.palavra_chave = pItem.palavra_chave;
            item.profissao = pItem.profissao;
            item.estado_civil = pItem.estado_civil;
            contextoEF.SaveChanges();
            return true;
        }

        public Boolean AlterarDadosTurmaAluno(matricula_turma pItem)
        {
            matricula_turma item = new matricula_turma();
            item = contextoEF.matricula_turma.Where(x => x.id_aluno == pItem.id_aluno && x.id_turma == pItem.id_turma).SingleOrDefault();
            item.data_artigo = pItem.data_artigo;
            item.data_aprovacao_artigo = pItem.data_aprovacao_artigo;
            item.nome_artigo = pItem.nome_artigo;
            item.orientador_artigo = pItem.orientador_artigo;
            contextoEF.SaveChanges();
            return true;
        }

        public List<alunos> ListaItem(alunos pItem, int[] qIdCurso)
        {
            var c = contextoEF.alunos.AsQueryable();
            List<alunos> lista = new List<alunos>();

            if (pItem.idaluno != 0)
            {
                c = c.Where(x => x.idaluno == pItem.idaluno);
            }

            if (pItem.nome != null)
            {
                c = c.Where(x => x.nome.Contains(pItem.nome));
            }

            if (pItem.cpf != null)
            {
                c = c.Where(x => x.cpf == pItem.cpf);
            }

            if  (pItem.email != null)
            {
                c = c.Where(x => x.email.Contains(pItem.email));
            }

            if (pItem.numero_documento != null)
            {
                c = c.Where(x => x.numero_documento.Contains(pItem.numero_documento));
            }

            if (pItem.empresa != null)
            {
                c = c.Where(x=> x.empresa.Contains(pItem.empresa));
            }

            if (pItem.RefazerProficienciaIngles != null)
            {
                c = c.Where(x => x.RefazerProficienciaIngles == pItem.RefazerProficienciaIngles);
            }

            if (pItem.RefazerProvaPortugues != null)
            {
                c = c.Where(x => x.RefazerProvaPortugues == pItem.RefazerProvaPortugues);
            }

            //Alunos de um deterninado curso
            if (qIdCurso[0] != 0)
            {
                var qIdTurma = contextoEF.turmas.Where(x => qIdCurso.Contains(x.id_curso)).Select(x => x.id_turma).ToArray();

                var qIdMatricula = contextoEF.matricula_turma.Where(x => qIdTurma.Contains(x.id_turma)).Select(x => x.id_aluno).ToArray();

                c = c.Where(x => qIdMatricula.Contains(x.idaluno));                
            }
            
            lista = c.Include(x=> x.matricula_turma).OrderBy(x => x.nome).ToList();

            return lista;
        }

        public List<alunos> ListaItemRelaroio(alunos pItem, int pIdTipoCurso, int pIdCurso, string qTurma, int pIdOfereciemnto, string qArea, string qSituacao, string qTipoMatricula)
        {
            var c = contextoEF.alunos.AsQueryable();
            List<alunos> lista = new List<alunos>();

            if (pItem.idaluno != 0)
            {
                c = c.Where(x => x.idaluno == pItem.idaluno);
            }

            if (pItem.nome != null)
            {
                c = c.Where(x => x.nome.Contains(pItem.nome));
            }

            if (pItem.cpf != null)
            {
                c = c.Where(x => x.cpf == pItem.cpf);
            }

            if (pItem.numero_documento != null)
            {
                c = c.Where(x => x.numero_documento.Contains(pItem.numero_documento));
            }

            if (pItem.telefone_res != null)
            {
                c = c.Where(x => x.telefone_res.Contains(pItem.telefone_res));
            }

            if (pItem.celular_res != null)
            {
                c = c.Where(x => x.celular_res.Contains(pItem.celular_res));
            }

            if (pItem.email != null)
            {
                c = c.Where(x => x.email.Contains(pItem.email));
            }

            if (pItem.logradouro_res != null)
            {
                c = c.Where(x => x.logradouro_res.Contains(pItem.logradouro_res));
            }

            if (pItem.palavra_chave != null)
            {
                c = c.Where(x => x.palavra_chave.Contains(pItem.palavra_chave));
            }

            if (pIdOfereciemnto !=0)
            {
                var sid_aluno = contextoEF.matricula_oferecimento.Where(x => x.id_oferecimento == pIdOfereciemnto).Select(x => x.id_aluno).ToArray();

                c = c.Where(x => sid_aluno.Contains(x.idaluno));
            }
            if (pIdCurso != 0 && qTurma == "")
            {
                var sid_turma = contextoEF.turmas.Where(x => x.id_curso == pIdCurso).Select(x => x.id_turma).ToArray();

                var sid_aluno = contextoEF.matricula_turma.Where(x => sid_turma.Contains(x.id_turma)).Select(x => x.id_aluno).ToArray();

                c = c.Where(x => sid_aluno.Contains(x.idaluno));
            }
            else if (pIdCurso != 0 && qTurma != "")
            {
                var sid_turma = contextoEF.turmas.Where(x => x.id_curso == pIdCurso && x.cod_turma == qTurma).Select(x => x.id_turma).ToArray();

                var sid_aluno = contextoEF.matricula_turma.Where(x => sid_turma.Contains(x.id_turma)).Select(x => x.id_aluno).ToArray();

                c = c.Where(x => sid_aluno.Contains(x.idaluno));
            }
            else if (pIdTipoCurso != 0 && qTurma == "")
            {
                var sid_curso = contextoEF.cursos.Where(x => x.id_tipo_curso == pIdTipoCurso).Select(x => x.id_curso).ToArray();

                var sid_turma = contextoEF.turmas.Where(x => sid_curso.Contains(x.id_curso)).Select(x => x.id_turma).ToArray();

                var sid_aluno = contextoEF.matricula_turma.Where(x => sid_turma.Contains(x.id_turma)).Select(x => x.id_aluno).ToArray();

                c = c.Where(x => sid_aluno.Contains(x.idaluno));
            }
            else if (pIdTipoCurso != 0 && qTurma != "")
            {
                var sid_curso = contextoEF.cursos.Where(x => x.id_tipo_curso == pIdTipoCurso).Select(x => x.id_curso).ToArray();

                var sid_turma = contextoEF.turmas.Where(x => sid_curso.Contains(x.id_curso) && x.cod_turma == qTurma).Select(x => x.id_turma).ToArray();

                var sid_aluno = contextoEF.matricula_turma.Where(x => sid_turma.Contains(x.id_turma)).Select(x => x.id_aluno).ToArray();

                c = c.Where(x => sid_aluno.Contains(x.idaluno));
            }
            else if (qTurma != "")
            {
                //var sid_curso = contextoEF.cursos.Where(x => x.id_tipo_curso == pIdTipoCurso).Select(x => x.id_curso).ToArray();

                var sid_turma = contextoEF.turmas.Where(x => x.cod_turma == qTurma).Select(x => x.id_turma).ToArray();

                var sid_aluno = contextoEF.matricula_turma.Where(x => sid_turma.Contains(x.id_turma)).Select(x => x.id_aluno).ToArray();

                c = c.Where(x => sid_aluno.Contains(x.idaluno));
            }

            //var sAux = contextoEF.matricula_turma.Where(x => x.id_turma == 120).Select(x => x.id_aluno).ToArray();
            //c = c.Where(x => sAux.Contains(x.idaluno));  // Turma

            if (pItem.formacao != null)
            {
                c = c.Where(x => x.formacao.Contains(pItem.formacao));
            }

            if (pItem.ano_graduacao != null)
            {
                c = c.Where(x => x.ano_graduacao == pItem.ano_graduacao);
            }

            if (pItem.empresa != null)
            {
                c = c.Where(x => x.empresa.Contains(pItem.empresa));
            }

            if (pItem.cargo != null)
            {
                c = c.Where(x => x.cargo.Contains(pItem.cargo));
            }

            if (qArea != "")
            {
                var sid_area = contextoEF.areas_concentracao.Where(x => x.nome.Contains(qArea)).Select(x=> x.id_area_concentracao).ToArray();
                var sid_aluno = contextoEF.matricula_turma.Where(x => sid_area.Contains(x.id_area_concentracao.Value)).Select(x => x.id_aluno);
                c = c.Where(x => sid_aluno.Contains(x.idaluno));
            }

            if (qSituacao != "")
            {
                List<historico_matricula_turma> lista_Historico;
                var q = contextoEF.historico_matricula_turma.GroupBy(x => x.id_matricula_turma).Select(x => x.OrderByDescending(y => y.data_inicio).FirstOrDefault());

                lista_Historico = q.ToList();
                int[] sid_matriculaturma;

                if (qSituacao == "Cursando")
                {
                    sid_matriculaturma = lista_Historico.Where(x => x.situacao != "Titulado" && x.situacao != "Desligado" && x.situacao != "Abandonou").Select(x => x.id_matricula_turma).ToArray();
                }
                else if (qSituacao == "Não cursando")
                {
                    sid_matriculaturma = lista_Historico.Where(x => x.situacao == "Titulado" || x.situacao == "Desligado" || x.situacao == "Abandonou").Select(x => x.id_matricula_turma).ToArray();
                }
                else
                {
                    sid_matriculaturma = lista_Historico.Where(x => x.situacao == qSituacao).Select(x => x.id_matricula_turma).ToArray();
                }

                if (pIdCurso != 0 && qTurma == "")
                {
                    var sid_aluno = contextoEF.matricula_turma.Where(x => sid_matriculaturma.Contains(x.id_matricula_turma) && x.turmas.cursos.id_curso == pIdCurso).Select(x => x.id_aluno);
                    c = c.Where(x => sid_aluno.Contains(x.idaluno));
                }
                else if (pIdCurso != 0 && qTurma != "")
                {
                    var sid_aluno = contextoEF.matricula_turma.Where(x => sid_matriculaturma.Contains(x.id_matricula_turma) && x.turmas.cursos.id_curso == pIdCurso && x.turmas.cod_turma == qTurma).Select(x => x.id_aluno);
                    c = c.Where(x => sid_aluno.Contains(x.idaluno));
                }
                else if (pIdTipoCurso != 0 && qTurma == "")
                {
                    var sid_aluno = contextoEF.matricula_turma.Where(x => sid_matriculaturma.Contains(x.id_matricula_turma) && x.turmas.cursos.id_tipo_curso == pIdTipoCurso).Select(x => x.id_aluno);
                    c = c.Where(x => sid_aluno.Contains(x.idaluno));
                }
                else if (pIdTipoCurso != 0 && qTurma != "")
                {
                    var sid_aluno = contextoEF.matricula_turma.Where(x => sid_matriculaturma.Contains(x.id_matricula_turma) && x.turmas.cursos.id_tipo_curso == pIdTipoCurso && x.turmas.cod_turma == qTurma).Select(x => x.id_aluno);
                    c = c.Where(x => sid_aluno.Contains(x.idaluno));
                }
                else
                {
                    var sid_aluno = contextoEF.matricula_turma.Where(x => sid_matriculaturma.Contains(x.id_matricula_turma)).Select(x => x.id_aluno);
                    c = c.Where(x => sid_aluno.Contains(x.idaluno));
                }

            }

            if (qTipoMatricula != "")
            {
                List<historico_matricula_turma> lista_Historico;
                var q = contextoEF.historico_matricula_turma.GroupBy(x => x.id_matricula_turma).Select(x => x.OrderBy(y => y.data_inicio).FirstOrDefault());

                lista_Historico = q.ToList();

                var sid_matriculaturma = lista_Historico.Where(x => x.status == qTipoMatricula).Select(x => x.id_matricula_turma).ToArray();
                var sid_aluno = contextoEF.matricula_turma.Where(x => sid_matriculaturma.Contains(x.id_matricula_turma)).Select(x => x.id_aluno);
                c = c.Where(x => sid_aluno.Contains(x.idaluno));
            }



            if (pItem.RefazerProficienciaIngles != null)
            {
                c = c.Where(x => x.RefazerProficienciaIngles == pItem.RefazerProficienciaIngles);
            }

            if (pItem.RefazerProvaPortugues != null)
            {
                c = c.Where(x => x.RefazerProvaPortugues == pItem.RefazerProvaPortugues);
            }

            lista = c.OrderBy(x => x.nome).ToList();

            //lista = lista.OrderBy(x=> x.matricula_turma).ToList();

            return lista;
        }

        public List<oferecimentos> ListaOferecimentosAluno(int id_aluno, int Id_Turma)
        {

            List<oferecimentos> Lista = new List<oferecimentos>();
            //Array[] iAux;

            var iAux = contextoEF.matricula_oferecimento.Where(x => x.id_aluno == id_aluno && x.id_turma == Id_Turma).Select(x => x.id_oferecimento).ToArray();
            Lista = contextoEF.oferecimentos.Where(p => iAux.Contains(p.id_oferecimento)).Include(p => p.datas_aulas).Include(p => p.disciplinas).Include(p => p.notas).ToList();
            //Lista = contextoEF.oferecimentos.Where(p => iAux.Contains(p.id_oferecimento)).ToList();
            //for (int i = 0; i < Lista.Count; i++)
            //{
            //    Lista[i].datas_aulas = Lista[i].datas_aulas;
            //    Lista[i].disciplinas = Lista[i].disciplinas;
            //    Lista[i].notas = Lista[i].notas;
            //}
            return Lista;
        }

        public historico_matricula_turma CriarSituacaoHistorico(historico_matricula_turma pItem)
        {
            contextoEF.historico_matricula_turma.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public bool EditarSituacaoHistorico(historico_matricula_turma pItem)
        {
            historico_matricula_turma item;
            item = contextoEF.historico_matricula_turma.Where(x => x.id_historico == pItem.id_historico).SingleOrDefault();
            item.status = pItem.status;
            item.data_inicio = pItem.data_inicio;
            item.data_fim = pItem.data_fim;
            //item.data_previsao_termino = pItem.data_previsao_termino;
            item.data = pItem.data;
            item.usuario = pItem.usuario;
            item.id_prorrogacao = pItem.id_prorrogacao;
            contextoEF.SaveChanges();
            return true;
        }

        public bool ApagarSituacaoHistorico(historico_matricula_turma pItem)
        {
            historico_matricula_turma item;
            item = contextoEF.historico_matricula_turma.Where(x => x.id_historico == pItem.id_historico).SingleOrDefault();
            contextoEF.historico_matricula_turma.Remove(item);
            contextoEF.SaveChanges();
            return true;
        }

        public prorrogacao CriarReuniaoCPG(prorrogacao pItem)
        {
            contextoEF.prorrogacao.Add(pItem);
            contextoEF.SaveChanges();
            pItem = contextoEF.prorrogacao.Include(x=> x.tipo_reuniao_cpg).Where(x => x.id_prorrogacao == pItem.id_prorrogacao).SingleOrDefault();
            return pItem;
        }

        public prorrogacao EditarReuniaoCPG(prorrogacao pItem)
        {
            prorrogacao item;
            item = contextoEF.prorrogacao.Where(x => x.id_prorrogacao == pItem.id_prorrogacao).SingleOrDefault();
            //DateTime qDataDefault = new DateTime();
            item.id_reuniao = pItem.id_reuniao;
            item.id_tipo_reuniao_cpg = pItem.id_tipo_reuniao_cpg;
            item.id_aluno = pItem.id_aluno;
            item.parecer = pItem.parecer;
            if (pItem.id_tipo_reuniao_cpg == 1) //Prorrogação
            {
                item.data_inicio = pItem.data_inicio;
                item.data_fim = pItem.data_fim;
                //if (pItem.data_deposito != qDataDefault)
                //{
                    item.data_deposito = pItem.data_deposito;
                //}
            }
            item.observacao = pItem.observacao;
            item.status = pItem.status;
            //item.data_cadastro = DateTime.Now;
            item.data_alteracao = pItem.data_alteracao;
            item.usuario = pItem.usuario;
            item.id_matricula_turma = pItem.id_matricula_turma;
            
            contextoEF.SaveChanges();
            return item;
        }

        public bool ApagarReuniaoCPG(prorrogacao pItem)
        {
            prorrogacao item;
            item = contextoEF.prorrogacao.Where(x => x.id_prorrogacao == pItem.id_prorrogacao).SingleOrDefault();
            contextoEF.prorrogacao.Remove(item);
            contextoEF.SaveChanges();
            return true;
        }

        public List<professores> ListOrientadoresDisponiveis(matricula_turma_orientacao pItem, professores pItemProfessor)
        {
            var c = contextoEF.professores.AsQueryable();
            List<professores> lista = new List<professores>();
            var sAux = contextoEF.matricula_turma_orientacao.Where(x => x.id_matricula_turma == pItem.id_matricula_turma).Select(x => x.id_professor).ToArray();
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

        public List<professores> ListBancaDisponiveis(banca pItem, professores pItemProfessor)
        {
            var c = contextoEF.professores.AsQueryable();
            List<professores> lista = new List<professores>();
            var sAux = contextoEF.banca_professores.Where(x => x.id_banca == pItem.id_banca).Select(x => x.id_professor).ToArray();
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

        public bool IncluirAlterarOrientador(matricula_turma_orientacao pItem, string idOrientadorAnterior)
        {
            matricula_turma_orientacao item;
            if (idOrientadorAnterior != "")
            {
                int i_idOrientadorAnterior = Convert.ToInt32(idOrientadorAnterior);

                item = contextoEF.matricula_turma_orientacao.Where(x => x.id_matricula_turma == pItem.id_matricula_turma && x.id_professor == i_idOrientadorAnterior).FirstOrDefault();
                item.id_professor = pItem.id_professor;
                item.status = "alterado";
                item.data_alteracao = pItem.data_alteracao;
                item.usuario = pItem.usuario;
            }
            else
            {
                pItem.status = "cadastrado";
                contextoEF.matricula_turma_orientacao.Add(pItem);
                professores itemProfessor;
                itemProfessor = contextoEF.professores.Where(x => x.id_professor == pItem.id_professor).SingleOrDefault();
                pItem.professores = itemProfessor;
            }
            
            contextoEF.SaveChanges();
            return true;
        }

        public bool AlterarTituloOrientacao(matricula_turma_orientacao pItem)
        {
            List<matricula_turma_orientacao> lista;
            lista = contextoEF.matricula_turma_orientacao.Where(x => x.id_matricula_turma == pItem.id_matricula_turma).ToList();
            lista.ForEach(x => { x.titulo = pItem.titulo; x.data_alteracao = pItem.data_alteracao; x.status = "alterado"; x.usuario = pItem.usuario; });
            
            contextoEF.SaveChanges();
            return true;
        }

        public bool ApagarDadosOrientacao(matricula_turma_orientacao pItem)
        {
            List<matricula_turma_orientacao> lista;
            lista = contextoEF.matricula_turma_orientacao.Where(x => x.id_matricula_turma == pItem.id_matricula_turma).ToList();
            lista.ForEach(x => { contextoEF.matricula_turma_orientacao.Remove(x);});

            contextoEF.SaveChanges();
            return true;
        }

        public bool ApagarCoorientador(matricula_turma_orientacao pItem)
        {
            matricula_turma_orientacao item;

            item = contextoEF.matricula_turma_orientacao.Where(x => x.id_matricula_turma == pItem.id_matricula_turma && x.id_professor == pItem.id_professor).FirstOrDefault();
            contextoEF.matricula_turma_orientacao.Remove(item);

            contextoEF.SaveChanges();
            return true;
        }

        public bool AlterarBanca(banca pItem)
        {
            banca item;

            item = contextoEF.banca.Where(x => x.id_banca == pItem.id_banca).FirstOrDefault();
            item.horario = pItem.horario;
            item.resultado = pItem.resultado;
            item.titulo = pItem.titulo;
            item.observacao = pItem.observacao;
            item.status = pItem.status;
            item.data_alteracao = pItem.data_alteracao;
            item.usuario = pItem.usuario;
            item.data_entrega_trabalho = pItem.data_entrega_trabalho;
            item.portaria_mec = pItem.portaria_mec;
            item.data_portaria_mec = pItem.data_portaria_mec;
            item.data_diario_oficial = pItem.data_diario_oficial;
            item.remoto = pItem.remoto;

            contextoEF.SaveChanges();
            return true;
        }

        public bool AlterarOrientadorBanca(banca_professores pItem)
        {
            banca_professores item;

            item = contextoEF.banca_professores.Where(x => x.id_banca == pItem.id_banca && x.tipo_professor == "Orientador").FirstOrDefault();
            if (item != null)
            {
                pItem.imprimir = item.imprimir;
                pItem.data_cadastro = item.data_cadastro;
                contextoEF.banca_professores.Remove(item);
            }
            else
            {
                pItem.imprimir = false;
                pItem.data_cadastro = DateTime.Today;
            }
            
            contextoEF.banca_professores.Add(pItem);

            professores itemProfessor;
            itemProfessor = contextoEF.professores.Where(x => x.id_professor == pItem.id_professor).SingleOrDefault();
            pItem.professores = itemProfessor;

            contextoEF.SaveChanges();
            return true;
        }

        public banca IncluirBanca(banca pItem)
        {
            banca item = contextoEF.banca.Where(x => x.id_matricula_turma == pItem.id_matricula_turma && x.tipo_banca == pItem.tipo_banca).SingleOrDefault();
            if (item == null)
            {
                contextoEF.banca.Add(pItem);
                contextoEF.SaveChanges();
            }
            return pItem;
        }

        public bool IncluirProfessorBanca(banca_professores pItem)
        {
            contextoEF.banca_professores.Add(pItem);
            contextoEF.SaveChanges();

            professores itemProfessor;
            itemProfessor = contextoEF.professores.Where(x => x.id_professor == pItem.id_professor).SingleOrDefault();
            pItem.professores = itemProfessor;

            return true;
        }

        public bool ExcluirProfessorBanca(banca_professores pItem)
        {
            banca_professores item;
            item = contextoEF.banca_professores.Where(x => x.id_banca == pItem.id_banca && x.id_professor == pItem.id_professor).FirstOrDefault();
            contextoEF.banca_professores.Remove(item);
            contextoEF.SaveChanges();

            return true;
        }

        public bool ImprimirProfessorBanca(banca_professores pItem)
        {
            banca_professores item;
            item = contextoEF.banca_professores.Where(x => x.id_banca == pItem.id_banca && x.id_professor == pItem.id_professor).FirstOrDefault();
            item.imprimir = pItem.imprimir;
            contextoEF.SaveChanges();

            return true;
        }

        public banca_dissertacao salvarDissertacao(banca_dissertacao pItem)
        {
            banca_dissertacao item;
            item = contextoEF.banca_dissertacao.Where(x => x.id_banca == pItem.id_banca).SingleOrDefault();
            if (item == null)
            {
                item = new banca_dissertacao();
                item.data_cadastro = DateTime.Now;
                item.id_banca = pItem.id_banca;
                item.visitas = pItem.visitas;
                item.downloads = pItem.downloads;
                item.palavras_chavePreview = pItem.palavras_chavePreview;
                item.resumoPreview = pItem.resumoPreview;
                item.arquivoPreview = pItem.arquivoPreview;
                item.cod_iptPreview = pItem.cod_iptPreview;
                item.data_alteracao = DateTime.Now;
                item.usuario = pItem.usuario;
                item.statusAprovacao = pItem.statusAprovacao;
                item.id_tipo_dissertacao = pItem.id_tipo_dissertacao;
                contextoEF.banca_dissertacao.Add(item);
            }
            else
            {
                item.id_banca = pItem.id_banca;
                //item.visitas = pItem.visitas;
                //item.downloads = pItem.downloads;
                item.palavras_chavePreview = pItem.palavras_chavePreview;
                item.resumoPreview = pItem.resumoPreview;
                item.arquivoPreview = pItem.arquivoPreview;
                item.cod_iptPreview = pItem.cod_iptPreview;
                item.data_alteracao = DateTime.Now;
                item.usuario = pItem.usuario;
                item.statusAprovacao = pItem.statusAprovacao;
                item.id_tipo_dissertacao = pItem.id_tipo_dissertacao;
            }

            contextoEF.SaveChanges();

            return item;
        }

        public banca_dissertacao AlteraStatusDissertacao(banca_dissertacao pItem)
        {
            banca_dissertacao item;
            item = contextoEF.banca_dissertacao.Where(x => x.id_banca == pItem.id_banca).SingleOrDefault();
            item.data_alteracao = DateTime.Now;
            item.data_reprovacao = pItem.data_reprovacao;
            item.usuario = pItem.usuario;
            item.usuarioAprovacao = pItem.usuarioAprovacao;
            item.statusAprovacao = pItem.statusAprovacao;

            contextoEF.SaveChanges();

            return item;
        }

        public banca_dissertacao AprovarDissertacao(banca_dissertacao pItem)
        {
            banca_dissertacao item;
            item = contextoEF.banca_dissertacao.Where(x => x.id_banca == pItem.id_banca).SingleOrDefault();
            item.data_alteracao = DateTime.Now;
            item.data_aprovacao = DateTime.Now;
            item.usuarioAprovacao = pItem.usuarioAprovacao;
            item.statusAprovacao = 1;
            item.palavras_chave = pItem.palavras_chavePreview;
            item.cod_ipt = pItem.cod_iptPreview;
            item.resumo = pItem.resumoPreview;
            item.arquivo = pItem.arquivoPreview;
            item.usuarioAprovacao = pItem.usuarioAprovacao;

            contextoEF.SaveChanges();

            return item;
        }

        public banca_dissertacao_obs CriarItem_Obs(banca_dissertacao_obs pItem)
        {
            contextoEF.banca_dissertacao_obs.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public IEnumerable<int> ListaAnosDissertacao()
        {
            var c = contextoEF.banca_dissertacao.AsQueryable();
            List<banca_dissertacao> lista = new List<banca_dissertacao>();
            c = c.Where(x => (x.id_tipo_dissertacao == 1 || x.id_tipo_dissertacao == null) && x.data_aprovacao != null);
            lista = c.OrderByDescending(x => x.banca.horario).ToList();
            var sAux = lista.Select(x => x.banca.horario.Value.Year).ToArray();

            var output = sAux
                        .GroupBy(word => word)
                        .Select(group => group.Key);

            return output;
        }

        public IEnumerable<int> ListaAnosTCC()
        {
            var c = contextoEF.banca_dissertacao.AsQueryable();
            List<banca_dissertacao> lista = new List<banca_dissertacao>();
            c = c.Where(x => x.id_tipo_dissertacao == 2 && x.data_aprovacao != null);
            lista = c.OrderByDescending(x => x.banca.horario).ToList();
            var sAux = lista.Select(x => x.banca.horario.Value.Year).ToArray();

            var output = sAux
                        .GroupBy(word => word)
                        .Select(group => group.Key);

            return output;
        }

        public List<banca_dissertacao> ListaDissertacoes(banca_dissertacao pItem, int iRegistroInicio, int iQuantosRegistros)
        {
            var c = contextoEF.banca_dissertacao.AsQueryable();
            List<banca_dissertacao> lista = new List<banca_dissertacao>();
            
            if (pItem.resumo != null)
            {
                c = c.Where(x => x.resumo.Contains(pItem.resumo) || x.banca.titulo.Contains(pItem.resumo) || x.palavras_chave.Contains(pItem.resumo)
                || x.banca.matricula_turma.alunos.nome.Contains(pItem.resumo) || x.banca.matricula_turma.alunos.palavra_chave.Contains(pItem.resumo));
            }

            if (pItem.ano != 0)
            {
                c = c.Where(x => x.banca.horario.Value.Year == pItem.ano);
            }

            c = c.Where(x => x.banca.tipo_banca == "Defesa" && x.data_aprovacao != null && (x.id_tipo_dissertacao == 1 || x.id_tipo_dissertacao == null));

            lista = c.OrderByDescending(x => x.banca.horario).Skip(iRegistroInicio).Take(iQuantosRegistros).ToList();
           
            return lista;
        }

        public List<banca_dissertacao> ListaTCCs(banca_dissertacao pItem, int iRegistroInicio, int iQuantosRegistros)
        {
            var c = contextoEF.banca_dissertacao.AsQueryable();
            List<banca_dissertacao> lista = new List<banca_dissertacao>();

            if (pItem.resumo != null)
            {
                c = c.Where(x => x.resumo.Contains(pItem.resumo) || x.banca.titulo.Contains(pItem.resumo) || x.palavras_chave.Contains(pItem.resumo)
                || x.banca.matricula_turma.alunos.nome.Contains(pItem.resumo) || x.banca.matricula_turma.alunos.palavra_chave.Contains(pItem.resumo));
            }

            if (pItem.ano != 0)
            {
                c = c.Where(x => x.banca.horario.Value.Year == pItem.ano);
            }

            c = c.Where(x => x.banca.tipo_banca == "Defesa" && x.data_aprovacao != null && x.id_tipo_dissertacao == 2);

            lista = c.OrderByDescending(x => x.banca.horario).Skip(iRegistroInicio).Take(iQuantosRegistros).ToList();

            return lista;
        }

        public bool atualizaVisita(banca_dissertacao pItem)
        {
            banca_dissertacao item;
            item = contextoEF.banca_dissertacao.Where(x => x.id_banca == pItem.id_banca).SingleOrDefault();
            item.visitas = item.visitas +1;
            contextoEF.SaveChanges();
            return true;
        }

        public bool atualizaDownload(banca_dissertacao pItem)
        {
            banca_dissertacao item;
            item = contextoEF.banca_dissertacao.Where(x => x.id_banca == pItem.id_banca).SingleOrDefault();
            item.downloads = item.downloads + 1;
            contextoEF.SaveChanges();
            return true;
        }

        public alunos_arquivos CriarArquivo(alunos_arquivos pItem)
        {
            if (pItem.id_alunos_arquivos_tipo == 9)
            {
                alunos_arquivos item_confere = contextoEF.alunos_arquivos.Where(x => x.idaluno == pItem.idaluno && x.id_alunos_arquivos_tipo == 9 && x.id_matricula_turma == pItem.id_matricula_turma).SingleOrDefault();
                if (item_confere == null)
                {
                    contextoEF.alunos_arquivos.Add(pItem);
                    contextoEF.SaveChanges();
                }
            }
            else if (pItem.id_alunos_arquivos_tipo == 10)
            {
                alunos_arquivos item_confere = contextoEF.alunos_arquivos.Where(x => x.idaluno == pItem.idaluno && x.id_alunos_arquivos_tipo == 10 && x.id_matricula_turma == pItem.id_matricula_turma).SingleOrDefault();
                if (item_confere == null)
                {
                    contextoEF.alunos_arquivos.Add(pItem);
                    contextoEF.SaveChanges();
                    }
            }
            else
            {
                contextoEF.alunos_arquivos.Add(pItem);
                contextoEF.SaveChanges();
            }
            //pItem = contextoEF.alunos_arquivos.Where(x => x.id_alunos_arquivos == pItem.id_alunos_arquivos).SingleOrDefault();
            alunos_arquivos_tipo item = contextoEF.alunos_arquivos_tipo.Where(x => x.id_alunos_arquivos_tipo == pItem.id_alunos_arquivos_tipo).SingleOrDefault();
            pItem.alunos_arquivos_tipo = item;
            return pItem;
        }

        public alunos_arquivos AlterarArquivo(alunos_arquivos pItem)
        {
            alunos_arquivos Item;
            Item = contextoEF.alunos_arquivos.Where(x => x.id_alunos_arquivos == pItem.id_alunos_arquivos).SingleOrDefault();
            Item.descricao = pItem.descricao;
            Item.nome_arquivo = pItem.nome_arquivo;
            Item.data_alteracao = pItem.data_alteracao;
            Item.usuario = pItem.usuario;
            Item.status = pItem.status;
            contextoEF.SaveChanges();
            return pItem;
        }

        public bool TrouxeDocumento(alunos pItem)
        {
            alunos item;
            item = contextoEF.alunos.Where(x => x.idaluno == pItem.idaluno).SingleOrDefault();
            item.entregou_rg = pItem.entregou_rg;
            item.entregou_cpf = pItem.entregou_cpf;
            item.entregou_historico = pItem.entregou_historico;
            item.entregou_diploma = pItem.entregou_diploma;
            item.entregou_comprovante_end = pItem.entregou_comprovante_end;
            item.entregou_fotos = pItem.entregou_fotos;
            item.entregou_certidao = pItem.entregou_certidao;
            //item.entregou_contrato = pItem.entregou_contrato;
            contextoEF.SaveChanges();
            return true;
        }

        public List<banca_dissertacao> ListaItemAprovacaoHomePage()
        {
            var c = contextoEF.banca_dissertacao.AsQueryable();
            List<banca_dissertacao> lista = new List<banca_dissertacao>();

            c = c.Where(x => x.statusAprovacao == 0);

            lista = c.OrderBy(x => x.banca.matricula_turma.alunos.nome).ToList();

            return lista;
        }

        public List<banca_dissertacao> ListaItemReprovacaoHomePage(string qUsuario)
        {
            var c = contextoEF.banca_dissertacao.AsQueryable();
            List<banca_dissertacao> lista = new List<banca_dissertacao>();

            c = c.Where(x => x.statusAprovacao == 2 && x.usuario == qUsuario);

            lista = c.OrderBy(x => x.banca.matricula_turma.alunos.nome).ToList();

            return lista;
        }

        public List<matricula_turma> ListaItemAlunosSituacaoIndefinida(cursos pItem)
        {
            DateTime daux = DateTime.Today.AddDays(-1);
            var c = contextoEF.matricula_turma.AsQueryable();
            List<matricula_turma> lista = new List<matricula_turma>();

            var iAux_Titulados = contextoEF.historico_matricula_turma.Where(x => x.situacao == "Titulado" || x.situacao == "Abandonou" || x.situacao == "Desligado").Select(x => x.id_matricula_turma).Distinct().ToArray();

            var iAux2_MaiorData = contextoEF.historico_matricula_turma.Where(x => !iAux_Titulados.Contains(x.id_matricula_turma) && x.data_fim >= DateTime.Today).Select(x => x.id_matricula_turma).Distinct().ToArray();

            var iAux = contextoEF.historico_matricula_turma.Where(x => !iAux_Titulados.Contains(x.id_matricula_turma) && !iAux2_MaiorData.Contains(x.id_matricula_turma)).Select(x => x.id_matricula_turma).Distinct().ToArray();


            if (pItem.id_curso != 0)
            {
                c = c.Where(x => iAux.Contains(x.id_matricula_turma) && (x.turmas.cursos.id_curso == pItem.id_curso));
            }
            else if (pItem.id_tipo_curso != 0)
            {
                c = c.Where(x => iAux.Contains(x.id_matricula_turma) && (x.turmas.cursos.id_tipo_curso == pItem.id_tipo_curso));
            }
            else
            {
                c = c.Where(x => iAux.Contains(x.id_matricula_turma) && (x.turmas.cursos.id_tipo_curso == 1 || x.turmas.cursos.id_tipo_curso == 3));
            }

            lista = c.OrderBy(x => x.alunos.nome).ToList();

            return lista;
        }

        public List<matricula_turma> ListaItemAlunosDocumentacaoPendente(cursos pItem)
        {
            DateTime daux = DateTime.Today.AddDays(-1);
            var c = contextoEF.matricula_turma.AsQueryable();
            List<matricula_turma> lista = new List<matricula_turma>();

            //var iAux_idAlunos_Pendentes = contextoEF.alunos.Where(x => x.entregou_contrato != true || x.entregou_rg != true || x.entregou_cpf != true || x.entregou_historico != true || x.entregou_comprovante_end != true || x.entregou_fotos != true || x.entregou_certidao != true).Select(x => x.idaluno).Distinct().ToArray();
            //aqui tira a obrigatoriedade de Hostórico Escolar (Solicitado pela Longuinho e autorizado pelo prof Eduardo - em 11/01/2022)
            //var iAux_idAlunos_Pendentes = contextoEF.alunos.Where(x => x.entregou_rg != true || x.entregou_cpf != true || x.entregou_historico != true || x.entregou_comprovante_end != true || x.entregou_fotos != true || x.entregou_certidao != true).Select(x => x.idaluno).Distinct().ToArray();
            var iAux_idAlunos_Pendentes = contextoEF.alunos.Where(x => x.entregou_rg != true || x.entregou_cpf != true || x.entregou_comprovante_end != true || x.entregou_fotos != true || x.entregou_certidao != true).Select(x => x.idaluno).Distinct().ToArray();

            //Pegar os alunos que estão sem contrato ====
            var iAux_idAlunos_Turmas_Mestrado_Especializacao = contextoEF.matricula_turma.Where(x => (x.turmas.cursos.id_tipo_curso == 1 || x.turmas.cursos.id_tipo_curso == 3)).Select(x => x.id_aluno).Distinct().ToArray();

            var iAux_idAlunos_ComContrato = contextoEF.alunos_arquivos.Where(x => x.id_alunos_arquivos_tipo == 9).Select(x => x.idaluno).Distinct().ToArray();

            var iAux_idAlunos_SemContrato = contextoEF.alunos.Where(x => iAux_idAlunos_Turmas_Mestrado_Especializacao.Contains(x.idaluno) && !iAux_idAlunos_ComContrato.Contains(x.idaluno)).Select(x => x.idaluno).Distinct().ToArray();
            //Pegar os alunos que estão sem contrato ====

            var iAux = contextoEF.matricula_turma.Where(x => iAux_idAlunos_Pendentes.Contains(x.id_aluno) || iAux_idAlunos_SemContrato.Contains(x.id_aluno) ).Select(x => x.id_matricula_turma).Distinct().ToArray();

            if (pItem.id_curso != 0)
            {
                c = c.Where(x => iAux.Contains(x.id_matricula_turma) && x.turmas.cursos.id_curso == pItem.id_curso);
            }
            else if (pItem.id_tipo_curso != 0)
            {
                c = c.Where(x => iAux.Contains(x.id_matricula_turma) && (x.turmas.cursos.id_tipo_curso == pItem.id_tipo_curso));
            }
            else
            {
                c = c.Where(x => iAux.Contains(x.id_matricula_turma) && (x.turmas.cursos.id_tipo_curso == 1 || x.turmas.cursos.id_tipo_curso == 3));
            }

            //c = c.Where(x => x.alunos.idaluno==1);
            lista = c.OrderBy(x => x.alunos.nome).ToList();

            return lista;
        }

        public List<matricula_turma> ListaItemAlunosAprovacaoOrientador(cursos pItem)
        {
            DateTime daux = DateTime.Today.AddDays(-1);
            var c = contextoEF.matricula_turma.AsQueryable();
            List<matricula_turma> lista = new List<matricula_turma>();

            var iAux_idMatriculaTurma = contextoEF.banca.Where(x => x.tipo_banca == "Defesa" && x.resultado == "Aprovado" && x.data_entrega_trabalho == null).Select(x=> x.id_matricula_turma).ToArray();

            //var iAux_idMatricula_Contratos = contextoEF.alunos_arquivos.Where(x => x.id_alunos_arquivos_tipo == 9).Select(x => x.id_matricula_turma).Distinct().ToArray();

            //var iAux = contextoEF.matricula_turma.Where(x => !iAux_idMatricula_Contratos.Contains(x.id_matricula_turma) || iAux_idAlunos_Pendentes.Contains(x.id_aluno)).Select(x => x.id_matricula_turma).Distinct().ToArray();

            //var iAux_idMatricula_Contratos = contextoEF.alunos_arquivos.Where(x => x.id_alunos_arquivos_tipo == 9).Select(x => x.id_matricula_turma).Distinct().ToArray();

            //var iAux = contextoEF.matricula_turma.Where(x => iAux_idAlunos_Pendentes.Contains(x.id_aluno)).Select(x => x.id_matricula_turma).Distinct().ToArray();


            if (pItem.id_curso != 0)
            {
                c = c.Where(x => iAux_idMatriculaTurma.Contains(x.id_matricula_turma) && x.turmas.cursos.id_curso == pItem.id_curso);
            }
            else if (pItem.id_tipo_curso != 0)
            {
                c = c.Where(x => iAux_idMatriculaTurma.Contains(x.id_matricula_turma) && (x.turmas.cursos.id_tipo_curso == pItem.id_tipo_curso));
            }
            else
            {
                c = c.Where(x => iAux_idMatriculaTurma.Contains(x.id_matricula_turma) && (x.turmas.cursos.id_tipo_curso == 1 || x.turmas.cursos.id_tipo_curso == 3));
            }

            lista = c.OrderBy(x => x.alunos.nome).ToList();

            return lista;
        }

        public List<matricula_turma> ListaItemAlunosEntregaArtigo(cursos pItem)
        {
            DateTime daux = DateTime.Today.AddDays(-1);
            var c = contextoEF.matricula_turma.AsQueryable();
            List<matricula_turma> lista = new List<matricula_turma>();

            var iAux_idMatriculaTurma = contextoEF.banca.Where(x => x.tipo_banca == "Defesa" && x.resultado == "Aprovado" && x.data_entrega_trabalho != null).Select(x => x.id_matricula_turma).ToArray();

            //var iAux_idMatricula_Contratos = contextoEF.alunos_arquivos.Where(x => x.id_alunos_arquivos_tipo == 9).Select(x => x.id_matricula_turma).Distinct().ToArray();

            //var iAux = contextoEF.matricula_turma.Where(x => !iAux_idMatricula_Contratos.Contains(x.id_matricula_turma) || iAux_idAlunos_Pendentes.Contains(x.id_aluno)).Select(x => x.id_matricula_turma).Distinct().ToArray();

            //var iAux_idMatricula_Contratos = contextoEF.alunos_arquivos.Where(x => x.id_alunos_arquivos_tipo == 9).Select(x => x.id_matricula_turma).Distinct().ToArray();

            //var iAux = contextoEF.matricula_turma.Where(x => iAux_idAlunos_Pendentes.Contains(x.id_aluno)).Select(x => x.id_matricula_turma).Distinct().ToArray();


            if (pItem.id_curso != 0)
            {
                c = c.Where(x => iAux_idMatriculaTurma.Contains(x.id_matricula_turma) && x.turmas.cursos.id_curso == pItem.id_curso);
            }
            else if (pItem.id_tipo_curso != 0)
            {
                c = c.Where(x => iAux_idMatriculaTurma.Contains(x.id_matricula_turma) && (x.turmas.cursos.id_tipo_curso == pItem.id_tipo_curso));
            }
            else
            {
                c = c.Where(x => iAux_idMatriculaTurma.Contains(x.id_matricula_turma) && (x.turmas.cursos.id_tipo_curso == 1 || x.turmas.cursos.id_tipo_curso == 3));
            }

            lista = c.Where(x=> x.data_artigo == null).OrderBy(x => x.alunos.nome).ToList();

            return lista;
        }

        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}
