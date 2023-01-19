using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class AlunoAplicacao
    {
        private readonly AlunoRepositorio Repositorio = new AlunoRepositorio();

        public alunos BuscaItem(alunos pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public alunos BuscaItem(string qCPF)
        {
            return Repositorio.BuscaItem(qCPF);
        }

        public alunos CriarItem(alunos pItem)
        {
            return Repositorio.CriarItem(pItem);
        }

        public Boolean AlterarItem(alunos pItem)
        {
            return Repositorio.AlterarItem(pItem);
        }

        public Boolean AlterarDadosTurmaAluno(matricula_turma pItem)
        {
            return Repositorio.AlterarDadosTurmaAluno(pItem);
        }
            
        public List<alunos> ListaItem(alunos pItem, int[] qIdCurso)
        {
            return Repositorio.ListaItem(pItem, qIdCurso);
        }

        public List<alunos> ListaItemRelaroio(alunos pItem, int pIdTipoCurso, int pIdCurso, string qTurma, int pIdOferecimento, string qArea, string qSituacao, string qTipoMatricula)
        {
            return Repositorio.ListaItemRelaroio(pItem, pIdTipoCurso, pIdCurso, qTurma, pIdOferecimento, qArea, qSituacao, qTipoMatricula);
        }

        public List<oferecimentos> ListaOferecimentosAluno(int id_aluno, int Id_Turma)
        {
            return Repositorio.ListaOferecimentosAluno(id_aluno, Id_Turma);
        }

        public historico_matricula_turma CriarSituacaoHistorico(historico_matricula_turma pItem)
        {
            return Repositorio.CriarSituacaoHistorico(pItem);
        }

        public bool EditarSituacaoHistorico(historico_matricula_turma pItem)
        {
            return Repositorio.EditarSituacaoHistorico(pItem);
        }

        public bool ApagarSituacaoHistorico(historico_matricula_turma pItem)
        {
            return Repositorio.ApagarSituacaoHistorico(pItem);
        }

        public prorrogacao CriarReuniaoCPG(prorrogacao pItem)
        {
            return Repositorio.CriarReuniaoCPG(pItem);
        }

        public prorrogacao EditarReuniaoCPG(prorrogacao pItem)
        {
            return Repositorio.EditarReuniaoCPG(pItem);
        }

        public bool ApagarReuniaoCPG(prorrogacao pItem)
        {
            return Repositorio.ApagarReuniaoCPG(pItem);
        }

        public List<professores> ListOrientadoresDisponiveis(matricula_turma_orientacao pItem, professores pItemProfessor)
        {
            return Repositorio.ListOrientadoresDisponiveis(pItem, pItemProfessor);
        }

        public List<professores> ListBancaDisponiveis(banca pItem, professores pItemProfessor)
        {
            return Repositorio.ListBancaDisponiveis(pItem, pItemProfessor);
        }

        public bool IncluirAlterarOrientador(matricula_turma_orientacao pItem, string idOrientadorAnterior)
        {
            return Repositorio.IncluirAlterarOrientador(pItem, idOrientadorAnterior);
        }

        public bool AlterarTituloOrientacao(matricula_turma_orientacao pItem)
        {
            return Repositorio.AlterarTituloOrientacao(pItem);
        }

        public bool ApagarDadosOrientacao(matricula_turma_orientacao pItem)
        {
            return Repositorio.ApagarDadosOrientacao(pItem);
        }

        public bool ApagarCoorientador(matricula_turma_orientacao pItem)
        {
            return Repositorio.ApagarCoorientador(pItem);
        }

        public bool AlterarBanca(banca pItem)
        {
            return Repositorio.AlterarBanca(pItem);
        }

        public banca IncluirBanca(banca pItem)
        {
            return Repositorio.IncluirBanca(pItem);
        }

        public bool AlterarOrientadorBanca(banca_professores pItem)
        {
            return Repositorio.AlterarOrientadorBanca(pItem);
        }

        public bool IncluirProfessorBanca(banca_professores pItem)
        {
            return Repositorio.IncluirProfessorBanca(pItem);
        }

        public bool ExcluirProfessorBanca(banca_professores pItem)
        {
            return Repositorio.ExcluirProfessorBanca(pItem);
        }

        public bool ImprimirProfessorBanca(banca_professores pItem)
        {
            return Repositorio.ImprimirProfessorBanca(pItem);
        }

        public banca_dissertacao salvarDissertacao(banca_dissertacao pItem)
        {
            return Repositorio.salvarDissertacao(pItem);
        }

        public banca_dissertacao AlteraStatusDissertacao(banca_dissertacao pItem)
        {
            return Repositorio.AlteraStatusDissertacao(pItem);
        }

        public banca_dissertacao AprovarDissertacao(banca_dissertacao pItem)
        {
            return Repositorio.AprovarDissertacao(pItem);
        }

        public banca_dissertacao_obs CriarItem_Obs(banca_dissertacao_obs pItem)
        {
            return Repositorio.CriarItem_Obs(pItem);
        }

        public IEnumerable<int> ListaAnosDissertacao()
        {
            return Repositorio.ListaAnosDissertacao();
        }

        public IEnumerable<int> ListaAnosTCC()
        {
            return Repositorio.ListaAnosTCC();
        }

        public List<banca_dissertacao> ListaDissertacoes(banca_dissertacao pItem, int iRegistroInicio, int iQuantosRegistros)
        {
            return Repositorio.ListaDissertacoes(pItem, iRegistroInicio, iQuantosRegistros);
        }

        public List<banca_dissertacao> ListaTCCs(banca_dissertacao pItem, int iRegistroInicio, int iQuantosRegistros)
        {
            return Repositorio.ListaTCCs(pItem, iRegistroInicio, iQuantosRegistros);
        }

        public bool atualizaVisita(banca_dissertacao pItem)
        {
            return Repositorio.atualizaVisita(pItem);
        }

        public bool atualizaDownload(banca_dissertacao pItem)
        {
            return Repositorio.atualizaDownload(pItem);
        }

        public alunos_arquivos CriarArquivo(alunos_arquivos pItem)
        {
            return Repositorio.CriarArquivo(pItem);
        }

        public alunos_arquivos AlterarArquivo(alunos_arquivos pItem)
        {
            return Repositorio.AlterarArquivo(pItem);
        }

        public bool TrouxeDocumento(alunos pItem)
        {
            return Repositorio.TrouxeDocumento(pItem);
        }

        public List<banca_dissertacao> ListaItemAprovacaoHomePage()
        {
            return Repositorio.ListaItemAprovacaoHomePage();
        }

        public List<banca_dissertacao> ListaItemReprovacaoHomePage(string qUsuario)
        {
            return Repositorio.ListaItemReprovacaoHomePage(qUsuario);
        }

        public List<matricula_turma> ListaItemAlunosSituacaoIndefinida(cursos pItem)
        {
            return Repositorio.ListaItemAlunosSituacaoIndefinida(pItem);
        }

        public List<matricula_turma> ListaItemAlunosDocumentacaoPendente(cursos pItem)
        {
            return Repositorio.ListaItemAlunosDocumentacaoPendente(pItem);
        }

        public List<matricula_turma> ListaItemAlunosAprovacaoOrientador(cursos pItem)
        {
            return Repositorio.ListaItemAlunosAprovacaoOrientador(pItem);
        }

        public List<matricula_turma> ListaItemAlunosEntregaArtigo(cursos pItem)
        {
            return Repositorio.ListaItemAlunosEntregaArtigo(pItem);
        }

    }
}
