using System;
using SERPI.Dominio_C;
using System.Collections.Generic;
using System.Linq;

namespace Repositorio_C
{
    public class GeraisRepositorio:IDisposable
    {
        private Entities contextoEF;

        public GeraisRepositorio()
        {
            contextoEF = new Entities();
        }

        public matricula_turma BuscaMatricula_turma(int idaluno, int id_turma)
        {
            return contextoEF.matricula_turma.Where(x=> x.id_aluno == idaluno && x.id_turma == id_turma).SingleOrDefault();
        }

        public List<Pais> ListaPais()
        {
            return contextoEF.Pais.OrderBy(x=> x.Id_Pais).ToList(); ;
        }

        public List<Estado> ListaEstado()
        {
            return contextoEF.Estado.OrderBy(x => x.Nome).ToList(); ;
        }

        public List<Cidade> ListaCidade(Estado pItem)
        {
            Estado itemEstado;
            itemEstado = contextoEF.Estado.Where(x => x.Sigla == pItem.Sigla).SingleOrDefault();
            return contextoEF.Cidade.Where (x=> x.Id_Estado == itemEstado.Id_Estado).OrderBy(x => x.Nome).ToList();
        }

        public List<presenca> ListaPresenca(presenca pItem)
        {
            List<presenca> lista;
            lista = contextoEF.presenca.Where(x => x.id_aluno == pItem.id_aluno && x.id_oferecimento == pItem.id_oferecimento).ToList();
            return lista;
        }

        public vw_historico vw_historico(int idAluno, int idTurma)
        {
            return contextoEF.vw_historico.Where(x => x.idaluno == idAluno && x.id_turma == idTurma).SingleOrDefault();
        }
        public List<vw_disciplinas_historico> ListaDisciplinas_historico(int idAluno, int idTurma)
        {
            List<vw_disciplinas_historico> lista = new List<vw_disciplinas_historico>();
            lista = contextoEF.vw_disciplinas_historico.Where(x => x.id_aluno == idAluno && x.id_turma == idTurma).OrderBy(x => x.inicio_aula).ToList();
            return lista;
        }

        public List<titulacao> ListaTitulacao()
        {
            List<titulacao> lista;
            lista = contextoEF.titulacao.ToList();
            return lista;
        }

        public List<tipos_curso> ListaTipoCurso()
        {
            List<tipos_curso> lista;
            lista = contextoEF.tipos_curso.ToList();
            return lista;
        }

        public List<curso_tipo_coordenador> ListaTipoCoordenador()
        {
            List<curso_tipo_coordenador> lista;
            lista = contextoEF.curso_tipo_coordenador.OrderBy(x=> x.ordem).ToList();
            return lista;
        }

        public List<salas_aula> ListaSalaAula()
        {
            List<salas_aula> lista;
            lista = contextoEF.salas_aula.Where(x=> x.status != "inativado").OrderBy(x=> x.sala).ToList();
            return lista;
        }

        public Enderecos BuscaEndereco(Enderecos pItem)
        {
            pItem = contextoEF.Enderecos.Where(x => x.id_endereco == pItem.id_endereco).SingleOrDefault();
            return pItem;
        }

        public List<forma_recebimento> ListaFormaRecebimento_banca()
        {
            List<forma_recebimento> lista;
            lista = contextoEF.forma_recebimento.Where(x => x.banca == true).OrderBy(x => x.ordem).ToList();
            return lista;
        }

        public List<forma_recebimento> ListaFormaRecebimento_HoraAula()
        {
            List<forma_recebimento> lista;
            lista = contextoEF.forma_recebimento.Where(x => x.horas_aula == true).OrderBy(x => x.ordem).ToList();
            return lista;
        }

        public List<curso_valor_banca> Lista_curso_valor_banca(int qIdCurso)
        {
            List<curso_valor_banca> lista;
            lista = contextoEF.curso_valor_banca.Where(x => x.id_curso == qIdCurso).ToList();
            return lista;
        }

        public List<curso_valor_coordenacao> Lista_curso_valor_coordenacao(int qIdCurso)
        {
            List<curso_valor_coordenacao> lista;
            lista = contextoEF.curso_valor_coordenacao.Where(x => x.id_curso == qIdCurso).ToList();
            return lista;
        }

        public List<curso_tipo_coordenador> Lista_curso_tipo_coordenador()
        {
            List<curso_tipo_coordenador> lista;
            lista = contextoEF.curso_tipo_coordenador.OrderBy(x=> x.ordem).ToList();
            return lista;
        }

        public List<forma_recebimento> ListaFormaRecebimento_ValoresOrientacao()
        {
            List<forma_recebimento> lista;
            lista = contextoEF.forma_recebimento.Where(x => x.orientacao == true).OrderBy(x => x.id_forma_recebimento).ToList();
            return lista;
        }

        public List<dados_contratos> ListaContrato(int idCurso)
        {
            List<dados_contratos> lista;
            if (idCurso != 0)
            {
                lista = contextoEF.dados_contratos.Where(x => x.id_curso == idCurso).OrderBy(x => x.nome_contrato).ToList();
            }
            else
            {
                lista = contextoEF.dados_contratos.OrderBy(x => x.nome_contrato).ToList();
            }
            return lista;
        }

        public dados_contratos CriaContrato(dados_contratos pItem)
        {
            contextoEF.dados_contratos.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public dados_contratos BuscaContrato(dados_contratos pItem)
        {
            pItem = contextoEF.dados_contratos.Where(x => x.nome_contrato == pItem.nome_contrato).SingleOrDefault();
            return pItem;
        }

        public dados_contratos AlterarContrato(dados_contratos pItem)
        {
            dados_contratos item;
            item = contextoEF.dados_contratos.Where(x => x.nome_contrato == pItem.nome_contrato).SingleOrDefault();

            item.valor_total = pItem.valor_total;
            item.num_parcelas = pItem.num_parcelas;
            item.valor_parcela = pItem.valor_parcela;
            item.valor_disciplina = pItem.valor_disciplina;
            item.prazo = pItem.prazo;
            item.inicio = pItem.inicio;
            item.coordenador = pItem.coordenador;
            item.secretaria = pItem.secretaria;
            item.testemunha_1 = pItem.testemunha_1;
            item.testemunha_2 = pItem.testemunha_2;
            item.rg_testemunha_1 = pItem.rg_testemunha_1;
            item.rg_testemunha_2 = pItem.rg_testemunha_2;
            item.diretor = pItem.diretor;
            item.status = pItem.status;
            item.data_alteracao = pItem.data_alteracao;
            item.usuario = pItem.usuario;
            contextoEF.SaveChanges();

            return pItem;
        }

        public Configuracoes BuscaConfiguracoes(int qIdConficuracao)
        {
            Configuracoes pItem;
            pItem = contextoEF.Configuracoes.Where(x=> x.id_configuracao == qIdConficuracao).SingleOrDefault();
            return pItem;
        }

        public List<banca_professores> ListaBancas(int qIdCurso, int qMes, int qAno, string qCondicao, string qTipoBanca)
        {
            var c = contextoEF.banca_professores.AsQueryable();
            List<banca_professores> lista = new List<banca_professores>();

            if (qIdCurso != 0)
            {
                var qIdTurma = contextoEF.turmas.Where(x => x.id_curso == qIdCurso).Select(x => x.id_turma).ToArray();
                var qIdMatriculaTurma = contextoEF.matricula_turma.Where(x => qIdTurma.Contains(x.id_turma)).Select(x => x.id_matricula_turma).ToArray();
                var qIdBanca = contextoEF.banca.Where(x => qIdMatriculaTurma.Contains(x.id_matricula_turma)).Select(x => x.id_banca).ToArray();
                c = c.Where(x => qIdBanca.Contains(x.id_banca));
            }

            if (qMes != 0)
            {
                var qIdBanca = contextoEF.banca.Where(x => x.horario.Value.Month == qMes).Select(x => x.id_banca).ToArray();
                c = c.Where(x => qIdBanca.Contains(x.id_banca));
            }

            if (qAno != 0)
            {
                var qIdBanca = contextoEF.banca.Where(x => x.horario.Value.Year == qAno).Select(x => x.id_banca).ToArray();
                c = c.Where(x => qIdBanca.Contains(x.id_banca));
            }

            if (qCondicao != "")
            {
                var qIdBanca = contextoEF.banca.Where(x => x.resultado == qCondicao).Select(x => x.id_banca).ToArray();
                c = c.Where(x => qIdBanca.Contains(x.id_banca));
            }

            lista = c.Where(x => x.banca.tipo_banca == qTipoBanca && x.tipo_professor == "Orientador").OrderBy(x => x.professores.nome).ToList();

            //lista = c.Where(x => x.banca.tipo_banca == qTipoBanca).OrderBy(x => x.professores.nome).ToList();

            return lista;
        }

        public List<banca> ListaMembros(int qIdCurso, int qMes, int qAno, string qCondicao, string qTipoBanca)
        {
            var c = contextoEF.banca.AsQueryable();
            List<banca> lista = new List<banca>();

            if (qIdCurso != 0)
            {
                var qIdTurma = contextoEF.turmas.Where(x => x.id_curso == qIdCurso).Select(x => x.id_turma).ToArray();
                var qIdMatriculaTurma = contextoEF.matricula_turma.Where(x => qIdTurma.Contains(x.id_turma)).Select(x => x.id_matricula_turma).ToArray();
                //var qIdBanca = contextoEF.banca.Where(x => qIdMatriculaTurma.Contains(x.id_matricula_turma)).Select(x => x.id_banca).ToArray();
                c = c.Where(x => qIdMatriculaTurma.Contains(x.id_matricula_turma));
            }

            if (qMes != 0)
            {
                //var qIdBanca = contextoEF.banca.Where(x => x.horario.Value.Month == qMes).Select(x => x.id_banca).ToArray();
                c = c.Where(x => x.horario.Value.Month == qMes);
            }

            if (qAno != 0)
            {
                //var qIdBanca = contextoEF.banca.Where(x => x.horario.Value.Year == qAno).Select(x => x.id_banca).ToArray();
                c = c.Where(x => x.horario.Value.Year == qAno);
            }

            if (qCondicao != "")
            {
                //var qIdBanca = contextoEF.banca.Where(x => x.resultado == qCondicao).Select(x => x.id_banca).ToArray();
                c = c.Where(x => x.resultado == qCondicao);
            }

            lista = c.Where(x => x.tipo_banca == qTipoBanca).ToList();

            return lista;
        }

        public List<novidadesSistema> ListaNovidadesSistema()
        {
            List<novidadesSistema> lista;
            lista = contextoEF.novidadesSistema.Where(x => x.status == "ativo").OrderByDescending(x => x.dataOcorrencia).ToList();
            return lista;
        }

        public curso_valor_hora_aula Altera_curso_valor_hora_aula(curso_valor_hora_aula pItem)
        {
            curso_valor_hora_aula item;
            item = contextoEF.curso_valor_hora_aula.Where(x=> x.id_curso == pItem.id_curso && x.id_forma_recebimento == pItem.id_forma_recebimento && x.id_titulacao == pItem.id_titulacao).SingleOrDefault();
            if (item != null)
            {
                item.valor = pItem.valor;
            }
            else
            {
                contextoEF.curso_valor_hora_aula.Add(pItem);
            }
            contextoEF.SaveChanges();

            return pItem;
        }

        public curso_valor_banca Altera_curso_valor_banca(curso_valor_banca pItem)
        {
            curso_valor_banca item;
            item = contextoEF.curso_valor_banca.Where(x => x.id_curso == pItem.id_curso && x.id_curso == pItem.id_curso).SingleOrDefault();
            if (item != null)
            {
                item.valor_sao_paulo = pItem.valor_sao_paulo;
                item.valor_fora_sao_paulo = pItem.valor_fora_sao_paulo;
            }
            else
            {
                contextoEF.curso_valor_banca.Add(pItem);
            }
            contextoEF.SaveChanges();

            return pItem;
        }

        public curso_valor_coordenacao Altera_Cria_curso_valor_coordenacao(curso_valor_coordenacao pItem)
        {
            curso_valor_coordenacao item;
            item = contextoEF.curso_valor_coordenacao.Where(x => x.id_curso == pItem.id_curso && x.id_tipo_coordenador == pItem.id_tipo_coordenador).SingleOrDefault();
            if (item != null)
            {
                item.valor = pItem.valor;
            }
            else
            {
                contextoEF.curso_valor_coordenacao.Add(pItem);
            }
            contextoEF.SaveChanges();

            return pItem;
        }


        public curso_valor_orientacao Altera_curso_valor_orientacao(curso_valor_orientacao pItem)
        {
            curso_valor_orientacao item;
            item = contextoEF.curso_valor_orientacao.Where(x => x.id_curso == pItem.id_curso && x.id_forma_recebimento == pItem.id_forma_recebimento).SingleOrDefault();
            if (item != null)
            {
                item.valor_qualificacao = pItem.valor_qualificacao;
                item.valor_defesa = pItem.valor_defesa;
            }
            else
            {
                contextoEF.curso_valor_orientacao.Add(pItem);
            }
            contextoEF.SaveChanges();

            return pItem;
        }

        public List<documentos_academicos> ListaDocumentosAcademicos()
        {
            List<documentos_academicos> lista;
            lista = contextoEF.documentos_academicos.Where(x => x.ativo == 1 && x.data_aprovacao != null).OrderByDescending(x => x.nome).ToList();
            return lista;
        }

        public List<alunos_arquivos_tipo> ListaAlunoArquivoTipo()
        {
            List<alunos_arquivos_tipo> lista;
            // 1=Outros e 9=contrato e 10=artigo
            lista = contextoEF.alunos_arquivos_tipo.Where(x => x.id_alunos_arquivos_tipo != 1 && x.id_alunos_arquivos_tipo != 9 && x.id_alunos_arquivos_tipo != 10).OrderBy(x => x.id_alunos_arquivos_tipo).ToList();
            return lista;
        }

        public List<feriado> ListaFeriado()
        {
            List<feriado> lista;
            lista = contextoEF.feriado.OrderBy(x => x.data).ToList();
            return lista;
        }

        public feriado ListaFeriado_porData(DateTime pItem)
        {
            DateTime pAux = Convert.ToDateTime(pItem.Day + "/" + pItem.Month + "/1900");
            feriado item;
            item = contextoEF.feriado.Where(x => x.data == pAux).SingleOrDefault();
            return item;
        }

        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}
