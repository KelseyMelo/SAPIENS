using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class InscricaoAplicacao
    {
        private readonly InscricaoRepositorio Repositorio = new InscricaoRepositorio();

        public fichas_inscricao BuscaItem_Inscricao(fichas_inscricao pItem)
        {
            return Repositorio.BuscaItem_Inscricao(pItem);
        }

        public periodo_inscricao BuscaItem_periodo_inscricao(periodo_inscricao pItem)
        {
            return Repositorio.BuscaItem_periodo_inscricao(pItem);
        }

        public periodo_inscricao_curso BuscaItem_periodo_inscricao_curso(periodo_inscricao_curso pItem)
        {
            return Repositorio.BuscaItem_periodo_inscricao_curso(pItem);
        }

        //public periodo_inscricao_curso BuscaItem_periodo_inscricao_curso_Phorte(periodo_inscricao_curso pItem)
        //{
        //    return Repositorio.BuscaItem_periodo_inscricao_curso_Phorte(pItem);
        //}

        public periodo_inscricao CriarPeriodoInscricao(periodo_inscricao pItem)
        {
            return Repositorio.CriarPeriodoInscricao(pItem);
        }

        public bool ExcluirPeriodoInscricao(periodo_inscricao pItem)
        {
            return Repositorio.ExcluirPeriodoInscricao(pItem);
        }

        public List<cursos> ListaCursosDisponiveis(periodo_inscricao pItem, cursos pItemCurso)
        {
            return Repositorio.ListaCursosDisponiveis(pItem, pItemCurso);
        }

        public periodo_inscricao_curso CriarCursoPeriodoInscricao(periodo_inscricao_curso pItem)
        {
            return Repositorio.CriarCursoPeriodoInscricao(pItem);
        }

        public bool AlterarCursoPeriodoInscricao(periodo_inscricao_curso pItem)
        {
            return Repositorio.AlterarCursoPeriodoInscricao(pItem);
        }

        public bool ExcluirCursoPeriodoInscricao(periodo_inscricao_curso pItem)
        {
            return Repositorio.ExcluirCursoPeriodoInscricao(pItem);
        }

        public fichas_inscricao CriarInscricao(fichas_inscricao pItem)
        {
            return Repositorio.CriarInscricao(pItem);
        }

        //public fichas_inscricao CriarInscricao_Phorte(fichas_inscricao pItem)
        //{
        //    return Repositorio.CriarInscricao_Phorte(pItem);
        //}

        public historico_inscricao CriarHistorico(historico_inscricao pItem)
        {
            return Repositorio.CriarHistorico(pItem);
        }

        //public historico_inscricao CriarHistorico_Phorte(historico_inscricao pItem)
        //{
        //    return Repositorio.CriarHistorico_Phorte(pItem);
        //}

        public boletos CriarBoleto(boletos pItem, fichas_inscricao pItem_Inscricao)
        {
            return Repositorio.CriarBoleto(pItem, pItem_Inscricao);
        }

        //public boletos CriarBoleto_Phorte(boletos pItem, fichas_inscricao pItem_Inscricao)
        //{
        //    return Repositorio.CriarBoleto_Phorte(pItem, pItem_Inscricao);
        //}

        public bool Criar_inscricao_boleto(boletos pItem, fichas_inscricao pItem_Inscricao)
        {
            return Repositorio.Criar_inscricao_boleto(pItem, pItem_Inscricao);
        }

        //public bool Criar_inscricao_boleto_Phorte(boletos pItem, fichas_inscricao pItem_Inscricao)
        //{
        //    return Repositorio.Criar_inscricao_boleto_Phorte(pItem, pItem_Inscricao);
        //}

        public boletos AlterarBoleto(boletos pItem)
        {
            return Repositorio.AlterarBoleto(pItem);
        }

        public Boolean AlterarInscricao(fichas_inscricao pItem)
        {
            return Repositorio.AlterarInscricao(pItem);
        }

        public Boolean AlterarPeriodoInscricao(periodo_inscricao pItem)
        {
            return Repositorio.AlterarPeriodoInscricao(pItem);
        }

        public List<fichas_inscricao> ListaInscrisao(fichas_inscricao pItem, DateTime qDataFim, string qUsuario)
        {
            return Repositorio.ListaInscricao(pItem, qDataFim, qUsuario);
        }

        public List<periodo_inscricao> ListaPeriodoInscricao(periodo_inscricao pItem)
        {
            return Repositorio.ListaPeriodoInscricao(pItem);
        }

        public List<periodo_inscricao_curso> ListaPeriodoInscricaoCurso(periodo_inscricao_curso pItem)
        {
            return Repositorio.ListaPeriodoInscricaoCurso(pItem);
        }

        public List<periodo_inscricao> ListaPeriodoInscricao(int[] qIdCurso)
        {
            return Repositorio.ListaPeriodoInscricao(qIdCurso);
        }

        public List<periodo_inscricao> ListaPeriodoInscricaoAdmin(periodo_inscricao pItem)
        {
            return Repositorio.ListaPeriodoInscricaoAdmin(pItem);
        }

        public List<cursos> ListaCursoPeriodo(periodo_inscricao pItem, int[] qIdCurso)
        {
            return Repositorio.ListaCursoPeriodo(pItem, qIdCurso);
        }

        //public List<periodo_inscricao> ListaPeriodoInscricao_Phorte(periodo_inscricao pItem)
        //{
        //    return Repositorio.ListaPeriodoInscricao_Phorte(pItem);
        //}

        public bool Criar_refTran(refTran pItem)
        {
            return Repositorio.Criar_refTran(pItem);
        }

        //public bool Criar_refTran_Phorte(refTran pItem)
        //{
        //    return Repositorio.Criar_refTran_Phorte(pItem);
        //}

        //public refTran Busca_refTran_Phorte(refTran pItem)
        //{
        //    return Repositorio.Busca_refTran_Phorte(pItem);
        //}

        //public refTran Altera_refTran_Phorte(refTran pItem)
        //{
        //    return Repositorio.Altera_refTran_Phorte(pItem);
        //}

        public string Busca_Ultimo_refTran()
        {
            return Repositorio.Busca_Ultimo_refTran();
        }

        public string Busca_Ultimo_refTran_Phorte()
        {
            return Repositorio.Busca_Ultimo_refTran_Phorte();
        }

        public Boolean Insere_Gemini(string CPF, string Nome, int Sexo, DateTime DataNascimento, string CEP, string Endereco, string Numero, string Complemento, string Bairro, string Cidade, string UF, string Email, string Valor, string NossoNumero, int qIdTipoCurso, int qIdCurso, string qNomeCurso, DateTime DataVencimento)
        {
            return Repositorio.Insere_Gemini( CPF,  Nome,  Sexo,  DataNascimento,  CEP,  Endereco,  Numero,  Complemento,  Bairro,  Cidade,  UF,  Email,  Valor,  NossoNumero, qIdTipoCurso, qIdCurso, qNomeCurso, DataVencimento);
        }

        public Boolean Altera_NossoNumeto_Boleto_Gemini(int IDLancamento, string NossoNumero_IPT, string Complemento_IPT)
        {
            return Repositorio.Altera_NossoNumeto_Boleto_Gemini(IDLancamento, NossoNumero_IPT, Complemento_IPT);
        }

        public Tuple<string, DateTime> VarificaBoletoPago_Gemini(string NossoNumero)
        {
            return Repositorio.VarificaBoletoPago_Gemini(NossoNumero);
        }

        public Boolean Insere_Gemini_Phorte(string CPF, string Nome, int Sexo, DateTime DataNascimento, string CEP, string Endereco, string Numero, string Complemento, string Bairro, string Cidade, string UF, string Email, string Valor, string NossoNumero, int qIdTipoCurso, int qIdCurso, string qNomeCurso, DateTime DataVencimento)
        {
            return Repositorio.Insere_Gemini_Phorte(CPF, Nome, Sexo, DataNascimento, CEP, Endereco, Numero, Complemento, Bairro, Cidade, UF, Email, Valor, NossoNumero, qIdTipoCurso, qIdCurso, qNomeCurso, DataVencimento);
        }

        public List<fichas_inscricao> ListaCandidatosBoletos(int qIdPeriodo, int qIdCurso)
        {
            return Repositorio.ListaCandidatosBoletos(qIdPeriodo, qIdCurso);
        }

        public bool ExcluiDataPagamentoBoleto(boletos pItem, int qIdInscricao)
        {
            return Repositorio.ExcluiDataPagamentoBoleto(pItem, qIdInscricao);
        }

        public bool IncluirDataPagamentoBoleto(boletos pItem_boleto, historico_inscricao pItem_historico)
        {
            return Repositorio.IncluirDataPagamentoBoleto(pItem_boleto, pItem_historico);
        }

        public bool ExcluiInscricao(int qIdInscricao)
        {
            return Repositorio.ExcluiInscricao(qIdInscricao);
        }

        public List<boletos> ListaBoletosAbertos()
        {
            return Repositorio.ListaBoletosAbertos();
        }

        public List<boletos> ListaBoletosAbertos_byFichaInscricao(List<fichas_inscricao> qLista)
        {
            return Repositorio.ListaBoletosAbertos_byFichaInscricao(qLista);
        }

        public bool ExcluiHistorico_inscricao_Registro_Gemini(int id_inscricao)
        {
            return Repositorio.ExcluiHistorico_inscricao_Registro_Gemini(id_inscricao);
        }


    }
}
