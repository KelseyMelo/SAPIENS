
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class FIPTAplicacao
    {
        private readonly FIPTRepositorio Repositorio = new FIPTRepositorio();

        public alunos_boletos BuscaItem(alunos_boletos pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public boleto_email BuscaItem(boleto_email pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public alunos_boletos_parcelas BuscaItem_BoletoParcela(alunos_boletos_parcelas pItem)
        {
            return Repositorio.BuscaItem_BoletoParcela(pItem);
        }

        public List<alunos_boletos> Lista_BuscaItem_idaluno(alunos_boletos pItem)
        {
            return Repositorio.Lista_BuscaItem_idaluno(pItem);
        }

        public alunos_inadimpentes_fipt BuscaItem(alunos_inadimpentes_fipt pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public alunos_curso_inadimplente BuscaItem(alunos_curso_inadimplente pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public alunos_inadimplentes_emails_enviados CriarItem_alunos_inadimplentes_emails_enviados(alunos_inadimplentes_emails_enviados pItem)
        {
            return Repositorio.CriarItem_alunos_inadimplentes_emails_enviados(pItem);
        }

        public bool Criar_emails_enviados_alunos_Inadimplentes_Gemini(int pIdAlunoFIPT, DateTime pDataEnvioEmail, int pId_aluno_curso_inadimplente)
        {
            return Repositorio.Criar_emails_enviados_alunos_Inadimplentes_Gemini(pIdAlunoFIPT, pDataEnvioEmail, pId_aluno_curso_inadimplente);
        }

        public alunos_inadimpentes_fipt Alterar_alunos_inadimpentes_fipt(alunos_inadimpentes_fipt pItem)
        {
            return Repositorio.Alterar_alunos_inadimpentes_fipt(pItem);
        }

        public alunos_curso_inadimplente Alterar_alunos_curso_inadimplente(alunos_curso_inadimplente pItem)
        {
            return Repositorio.Alterar_alunos_curso_inadimplente(pItem);
        }

        public bool Excluir_unico_alunos_boletos(alunos_boletos pItem)
        {
            return Repositorio.Excluir_unico_alunos_boletos(pItem);
        }

        public bool Excluir_unico_alunos_inadimpentes_fipt(alunos_inadimpentes_fipt pItem)
        {
            return Repositorio.Excluir_unico_alunos_inadimpentes_fipt(pItem);
        }

        public bool Excluir_todos_alunos_inadimpentes_fipt(alunos_inadimpentes_fipt pItem)
        {
            return Repositorio.Excluir_todos_alunos_inadimpentes_fipt(pItem);
        }

        public bool Excluir_Lote_unico_alunos_boletos_curso(alunos_boletos pItem)
        {
            return Repositorio.Excluir_Lote_unico_alunos_boletos_curso(pItem);
        }

        public bool Excluir_Lote_unico_alunos_curso_inadimplente(alunos_curso_inadimplente pItem)
        {
            return Repositorio.Excluir_Lote_unico_alunos_curso_inadimplente(pItem);
        }

        public bool Excluir_Lote_todos_alunos_curso_inadimplente(alunos_curso_inadimplente pItem)
        {
            return Repositorio.Excluir_Lote_todos_alunos_curso_inadimplente(pItem);
        }

        public bool Excluir_Lote_unico_alunos_boletos_parcelas(alunos_boletos_curso pItem)
        {
            return Repositorio.Excluir_Lote_unico_alunos_boletos_parcelas(pItem);
        }

        public bool Excluir_Lote_unico_alunos_parcelas_inadimplente(alunos_curso_inadimplente pItem)
        {
            return Repositorio.Excluir_Lote_unico_alunos_parcelas_inadimplente(pItem);
        }

        public bool Excluir_Lote_todos_alunos_parcelas_inadimplente(alunos_parcelas_inadimplente pItem)
        {
            return Repositorio.Excluir_Lote_todos_alunos_parcelas_inadimplente(pItem);
        }

        public List<alunos_inadimpentes_fipt> ListaAlunosInadimpelntes(alunos_inadimpentes_fipt pItem)
        {
            return Repositorio.ListaAlunosInadimpelntes(pItem);
        }

        public List<alunos_curso_inadimplente> ListaAlunosInadimpelntes2(alunos_inadimpentes_fipt pItem)
        {
            return Repositorio.ListaAlunosInadimpelntes2(pItem);
        }

        public List<alunos_boletos_curso> ListaAlunosBoletos(alunos_boletos pItem)
        {
            return Repositorio.ListaAlunosBoletos(pItem);
        }

        public bool ConsultaAlunosFipt(alunos_inadimpentes_fipt pItem)
        {
            return Repositorio.ConsultaAlunosFipt(pItem);
        }

        public Boolean Insere_Gemini_OcorrenciaAlunoInadimplente(int pIdAluno_fipt, int pIdCurso, string pOcorrencia, DateTime pDataOcorrencia)
        {
            return Repositorio.Insere_Gemini_OcorrenciaAlunoInadimplente(pIdAluno_fipt, pIdCurso, pOcorrencia, pDataOcorrencia);
        }

        public bool ConsultaBoletosFipt(alunos_boletos pItem)
        {
            return Repositorio.ConsultaBoletosFipt(pItem);
        }

        public geral_Boleto ConsultaDadosBoletosFipt(geral_Boleto pItem)
        {
            return Repositorio.ConsultaDadosBoletosFipt(pItem);
        }

        public bool ProcessaBoletosMesFipt(int iMes, int iAno)
        {
            return Repositorio.ProcessaBoletosMesFipt(iMes, iAno);
        }

        public List<boleto_email> ListaBoletosMesFipt(int pMatricula, string pNome, int pMes, int pAno)
        {
            return Repositorio.ListaBoletosMesFipt(pMatricula, pNome, pMes, pAno);
        }

        public bool AlteraBoletosMesFipt(boleto_email pItem)
        {
            return Repositorio.AlteraBoletosMesFipt(pItem);
        }

        public bool AlteraBoletosMesFipt_dataEnvio(boleto_email pItem)
        {
            return Repositorio.AlteraBoletosMesFipt_dataEnvio(pItem);
        }

    }
}

