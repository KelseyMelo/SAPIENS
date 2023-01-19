using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class GeraisAplicacao
    {
        private readonly GeraisRepositorio Repositorio = new GeraisRepositorio();

        public matricula_turma BuscaMatricula_turma(int idaluno, int id_turma)
        {
            return Repositorio.BuscaMatricula_turma(idaluno, id_turma);
        }
        public List<Pais> ListaPais()
        {
            return Repositorio.ListaPais();
        }

        public List<Estado> ListaEstado()
        {
            return Repositorio.ListaEstado();
        }

        public List<Cidade> ListaCidade(Estado pItem)
        {
            return Repositorio.ListaCidade(pItem);
        }

        public List<presenca> ListaPresenca(presenca pItem)
        {
            return Repositorio.ListaPresenca(pItem);
        }

        public vw_historico vw_historico(int idAluno, int idTurma)
        {
            return Repositorio.vw_historico(idAluno, idTurma);
        }
        public List<vw_disciplinas_historico> ListaDisciplinas_historico(int idAluno, int idTurma)
        {
            return Repositorio.ListaDisciplinas_historico(idAluno, idTurma);
        }

        public List<titulacao> ListaTitulacao()
        {
            return Repositorio.ListaTitulacao();
        }

        public List<tipos_curso> ListaTipoCurso()
        {
            return Repositorio.ListaTipoCurso();
        }

        public List<curso_tipo_coordenador> ListaTipoCoordenador()
        {
            return Repositorio.ListaTipoCoordenador();
        }

        public List<salas_aula> ListaSalaAula()
        {
            return Repositorio.ListaSalaAula();
        }

        public Enderecos BuscaEndereco(Enderecos pItem)
        {
            return Repositorio.BuscaEndereco(pItem);
        }

        public List<forma_recebimento> ListaFormaRecebimento_banca()
        {
            return Repositorio.ListaFormaRecebimento_banca();
        }

        public List<forma_recebimento> ListaFormaRecebimento_HoraAula()
        {
            return Repositorio.ListaFormaRecebimento_HoraAula();
        }

        public List<curso_valor_banca> Lista_curso_valor_banca(int qIdCurso)
        {
            return Repositorio.Lista_curso_valor_banca(qIdCurso);
        }

        public List<curso_valor_coordenacao> Lista_curso_valor_coordenacao(int qIdCurso)
        {
            return Repositorio.Lista_curso_valor_coordenacao(qIdCurso);
        }

        public List<curso_tipo_coordenador> Lista_curso_tipo_coordenador()
        {
            return Repositorio.Lista_curso_tipo_coordenador();
        }

        public List<forma_recebimento> ListaFormaRecebimento_ValoresOrientacao()
        {
            return Repositorio.ListaFormaRecebimento_ValoresOrientacao();
        }

        public List<dados_contratos> ListaContrato(int idCurso)
        {
            return Repositorio.ListaContrato(idCurso);
        }

        public dados_contratos CriaContrato(dados_contratos pItem)
        {
            return Repositorio.CriaContrato(pItem);
        }

        public dados_contratos BuscaContrato(dados_contratos pItem)
        {
            return Repositorio.BuscaContrato(pItem);
        }

        public dados_contratos AlterarContrato(dados_contratos pItem)
        {
            return Repositorio.AlterarContrato(pItem);
        }

        public Configuracoes BuscaConfiguracoes(int qIdConficuracao)
        {
            return Repositorio.BuscaConfiguracoes(qIdConficuracao);
        }

        public List<banca_professores> ListaBancas(int qIdCurso, int qMes, int qAno, string qCondicao, string qTipoBanca)
        {
            return Repositorio.ListaBancas(qIdCurso, qMes, qAno, qCondicao, qTipoBanca);
        }

        public List<banca> ListaMembros(int qIdCurso, int qMes, int qAno, string qCondicao, string qTipoBanca)
        {
            return Repositorio.ListaMembros(qIdCurso, qMes, qAno, qCondicao, qTipoBanca);
        }

        public List<novidadesSistema> ListaNovidadesSistema()
        {
            return Repositorio.ListaNovidadesSistema();
        }

        public curso_valor_hora_aula Altera_curso_valor_hora_aula(curso_valor_hora_aula pItem)
        {
            return Repositorio.Altera_curso_valor_hora_aula(pItem);
        }

        public curso_valor_banca Altera_curso_valor_banca(curso_valor_banca pItem)
        {
            return Repositorio.Altera_curso_valor_banca(pItem);
        }

        public curso_valor_coordenacao Altera_Cria_curso_valor_coordenacao(curso_valor_coordenacao pItem)
        {
            return Repositorio.Altera_Cria_curso_valor_coordenacao(pItem);
        }

        public curso_valor_orientacao Altera_curso_valor_orientacao(curso_valor_orientacao pItem)
        {
            return Repositorio.Altera_curso_valor_orientacao(pItem);
        }

        public List<documentos_academicos> ListaDocumentosAcademicos()
        {
            return Repositorio.ListaDocumentosAcademicos();
        }

        public List<alunos_arquivos_tipo> ListaAlunoArquivoTipo()
        {
            return Repositorio.ListaAlunoArquivoTipo();
        }

        public List<feriado> ListaFeriado()
        {
            return Repositorio.ListaFeriado();
        }

        public feriado ListaFeriado_porData(DateTime pItem)
        {
            return Repositorio.ListaFeriado_porData(pItem);
        }
    }
}
