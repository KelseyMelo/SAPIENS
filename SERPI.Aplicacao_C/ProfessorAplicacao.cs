using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class ProfessorAplicacao
    {
        private readonly ProfessorRepositorio Repositorio = new ProfessorRepositorio();

        public professores BuscaItem(professores pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public professores BuscaItem_byCPF(professores pItem)
        {
            return Repositorio.BuscaItem_byCPF(pItem);
        }


        public professores CriarItem(professores pItem)
        {
            return Repositorio.CriarItem(pItem);
        }

        public professores VerificaCPFJaExistente(professores pItem)
        {
            return Repositorio.VerificaCPFJaExistente(pItem);
        }

        public professores VerificaEmailJaExistente(professores pItem)
        {
            return Repositorio.VerificaEmailJaExistente(pItem);
        }

        public professores VerificaConfirmacaoEmail(professores pItem)
        {
            return Repositorio.VerificaConfirmacaoEmail(pItem);
        }

        public professores AlterarStatus(professores pItem)
        {
            return Repositorio.AlterarStatus(pItem);
        }

        public Boolean AlterarItem(professores pItem)
        {
            return Repositorio.AlterarItem(pItem);
        }

        public List<professores> ListaItem(professores pItem)
        {
            return Repositorio.ListaItem(pItem);
        }
        public List<oferecimentos_professores> ListaOferecimento(professores pItem)
        {
            return Repositorio.ListaOferecimento(pItem);
        }

        public List<presenca_professor> ListaAulasMarcadas(DateTime qData, string qPresente)
        {
            return Repositorio.ListaAulasMarcadas(qData, qPresente);
        }

        public List<matricula_turma_orientacao> ListaOrientacao(professores pItem)
        {
            return Repositorio.ListaOrientacao(pItem);
        }

        public List<banca_professores> ListaQualificacaoDefesa(professores pItem, string sTipoBanca)
        {
            return Repositorio.ListaQualificacaoDefesa(pItem, sTipoBanca);
        }

        public professor_data_recalculo BuscaDataRecalculo()
        {
            return Repositorio.BuscaDataRecalculo();
        }

        public professor_data_recalculo AlteraDataRecalculo(professor_data_recalculo pItem)
        {
            return Repositorio.AlteraDataRecalculo(pItem);
        }

    }
}