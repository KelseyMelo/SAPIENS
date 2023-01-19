using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class FinanceiroAplicacao
    {
        private readonly FinanceiroRepositorio Repositorio = new FinanceiroRepositorio();

        public List<datas_aulas> ListaCustoHoraAula_old(int qIdCurso, int qMes, int qAno)
        {
            return Repositorio.ListaCustoHoraAula_old(qIdCurso, qMes, qAno);
        }

        public List<geral_custo_hora_aula> ListaCustoHoraAula(string qIdCurso, DateTime qData, string qOrdenacao)
        {
            return Repositorio.ListaCustoHoraAula(qIdCurso, qData, qOrdenacao);
        }

        public List<geral_custo_banca_orientacao> ListaCustoBanca(string qIdCurso, DateTime qData)
        {
            return Repositorio.ListaCustoBanca(qIdCurso, qData);
        }

        public List<geral_custo_banca_orientacao> ListaCustoOrientacao(string qIdCurso, DateTime qData)
        {
            return Repositorio.ListaCustoOrientacao(qIdCurso, qData);
        }

        public List<geral_custo_coordenacao> ListaCustoCoordenacao(string qIdCurso, decimal qIdProfessor, DateTime qData)
        {
            return Repositorio.ListaCustoCoordenacao(qIdCurso, qIdProfessor, qData);
        }

        public List<geral_detalhe_hora_aula> ListaDetalheHoraAula(int qIdCurso, int qIdProfessor, string qAno, string qMes)
        {
            return Repositorio.ListaDetalheHoraAula(qIdCurso, qIdProfessor, qAno, qMes);
        }

        public List<geral_extrato_professor> ListaProfessores(professores pItem)
        {
            return Repositorio.ListaProfessores(pItem);
        }

        public List<geral_extrato_ocorrencia> ListaExtratoOcorrencia(int qIdProfessor, int iTemBanca)
        {
            return Repositorio.ListaExtratoOcorrencia(qIdProfessor, iTemBanca);
        }

        public List<geral_extrato_solicitado_pago> ListaExtratoSolicitadoPago(int qIdProfessor)
        {
            return Repositorio.ListaExtratoSolicitadoPago(qIdProfessor);
        }

        public professores Cria_altera_Observacoes_Plano(professor_observacoes_plano pItem)
        {
            return Repositorio.Cria_altera_Observacoes_Plano(pItem);
        }

        public void RecalcularHorasAulas(int idProfessor, DateTime dDataInicio, DateTime dDataFim, string usuario)
        {
            Repositorio.RecalcularHorasAulas(idProfessor, dDataInicio, dDataFim, usuario);
        }

        public void RecalcularOrientacoes(int idProfessor, DateTime dDataInicio, DateTime dDataFim, string usuario)
        {
            Repositorio.RecalcularOrientacoes(idProfessor, dDataInicio, dDataFim, usuario);
        }

        public void RecalcularBancas(int idProfessor, DateTime dDataInicio, DateTime dDataFim, string usuario)
        {
            Repositorio.RecalcularBancas(idProfessor, dDataInicio, dDataFim, usuario);
        }

        public void RecalcularCoordenacao(int idProfessor, DateTime dDataInicio, DateTime dDataFim, string usuario)
        {
            Repositorio.RecalcularCoordenacao(idProfessor, dDataInicio, dDataFim, usuario);

        }

        public List<geral_SolicitacaoPagto> ListaSolicitaoPagto(int idProfessor)
        {
            return Repositorio.ListaSolicitaoPagto(idProfessor);
        }

        public string AdicionaSolicitacaoPagto(DateTime qData, decimal qValorTotal, string qPlanoValor, string qUsuario)
        {
            return Repositorio.AdicionaSolicitacaoPagto(qData, qValorTotal, qPlanoValor, qUsuario);
        }

        public List<geral_Solicitacao> ListaSolicitaoEfetuada(int idProfessor, bool bSolicitado)
        {
            return Repositorio.ListaSolicitaoEfetuada(idProfessor, bSolicitado);
        }

        public professor_solicitacao_pagamento AlteraSolicitacao(professor_solicitacao_pagamento pItem)
        {
            return Repositorio.AlteraSolicitacao(pItem);
        }

        public string ExcluirSolicitacao(int pItem)
        {
            return Repositorio.ExcluirSolicitacao(pItem);
        }

        public List<geral_solicitado_professor> ListaSolicitacoesPagto(professor_solicitacao_pagamento pItem)
        {
            return Repositorio.ListaSolicitacoesPagto(pItem);
        }

        public List<geral_horas_aulas_dadas> ListaHorasAulasDadas(professor_solicitacao_pagamento pItem)
        {
            return Repositorio.ListaHorasAulasDadas(pItem);
        }

        public List<geral_orientacoes_dadas> ListaOrientacoesDadas(professor_solicitacao_pagamento pItem)
        {
            return Repositorio.ListaOrientacoesDadas(pItem);
        }

        public List<geral_custo_coordenacao> ListaCoordenacaoDadas(geral_solicitado_professor pItem)
        {
            return Repositorio.ListaCoordenacaoDadas(pItem);
        }
    }
}

