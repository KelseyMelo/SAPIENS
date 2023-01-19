using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERPI.Dominio_C
{
    public class Gerais
    {
    }

    public class geral_custo_hora_aula
    {
        public string NomeEmpresa { get; set; }
        public string NomeCurso { get; set; }
        public string BotaoDetalhe { get; set; }
        public int id_professor { get; set; }
        public int datas { get; set; }
        public int id_oferecimento { get; set; }
        public int id_curso { get; set; }
        public string quadrimestre { get; set; }
        public decimal total_horas { get; set; }
        public decimal total_horas_mes_anterior { get; set; }
        public decimal total_horas_mes_atual { get; set; }
        public string codigo_oferecimento { get; set; }
        public string disciplina { get; set; }
        public string professor { get; set; }
        public int recebe_como { get; set; }
        public string titulo_academico { get; set; }
        public string titulo_reduzido { get; set; }
        public string forma_recebimento { get; set; }
        public int valor_fixo { get; set; }
        public decimal valor_hora { get; set; }
        public int horas_aula_adicional { get; set; }
        public decimal valor_hora_adicional { get; set; }
        public decimal horas_extras { get; set; }
        public string col_Professor { get; set; }
        public decimal col_TotalHoras { get; set; }
        public decimal col_Total { get; set; }
    }

    public class geral_custo_banca_orientacao
    {
        public int col_Id_Professor { get; set; }
        public string col_Professor { get; set; }
        public string col_FormaRecebimento { get; set; }
        public string col_Empresa { get; set; }
        public string col_Curso { get; set; }
        public string col_TipoBanca { get; set; }
        public string col_DataHora { get; set; }
        public string col_Aluno { get; set; }
        public decimal col_Total { get; set; }
    }

    public class geral_custo_coordenacao
    {
        public int col_Id_Professor { get; set; }
        public string col_Professor { get; set; }
        public int col_Id_TipoCurso { get; set; }
        public int col_Id_Curso { get; set; }
        public string col_Curso { get; set; }
        public string col_TipoCoordenacao { get; set; }
        public string col_Turma { get; set; }
        public string col_MesReferencia { get; set; }
        public decimal col_Total { get; set; }
    }

    public class geral_detalhe_hora_aula
    {
        public string professor { get; set; }
        public string curso { get; set; }
        public int id_oferecimento { get; set; }
        public string periodo { get; set; }
        public string codigo_disciplina { get; set; }
        public decimal hora_aula { get; set; }
        public string data_aula { get; set; }
    }

    public class geral_extrato_professor
    {
        public int id_professor { get; set; }
        public string professor { get; set; }
        public string cpf { get; set; }
        public decimal saldo_a_solicitar { get; set; }
        public decimal plano { get; set; }
        public decimal pagamento { get; set; }
        public decimal solicitado { get; set; }
        public string status { get; set; }
       
    }

    public class geral_extrato_ocorrencia
    {
        public int id_professor { get; set; }
        public string professor { get; set; }
        public int id_plano { get; set; }
        public string mes { get; set; }
        public string motivo { get; set; }
        public decimal valor_atual { get; set; }
        public string valor_solicitado { get; set; }
        public string data_solicitacao { get; set; }
        public DateTime data_cadastro { get; set; }
        public DateTime data_alteracao { get; set; }
        public string usuario { get; set; }
        
    }

    public class geral_extrato_solicitado_pago
    {
        public int id_professor { get; set; }
        public string professor { get; set; }
        public int id_solicitacao { get; set; }
        public DateTime data_solicitacao { get; set; }
        public decimal valor { get; set; }
        public string data_email { get; set; }
        public string nota_fiscal { get; set; }
        public string data_recebimento { get; set; }
        public string data_pagamento { get; set; }
        public string status { get; set; }
        public DateTime data_alteracao { get; set; }
        public string usuario { get; set; }
        public string id_plano { get; set; }
        public string motivo { get; set; }
    }

    public class geral_solicitado_professor
    {
        public int id_professor { get; set; }
        public string professor { get; set; }
        public string email { get; set; }
        public string email2 { get; set; }
        public decimal valor_t_SolicitacaoPagto { get; set; }
        public decimal valor_t_SolicitacaoPlano { get; set; }
        public decimal valor_t_ProfessorPlano { get; set; }
        public DateTime mes_plano { get; set; }
        public string motivo { get; set; }
        public string nota_fiscal { get; set; }
        public DateTime data_recebimento { get; set; }
        public string status { get; set; }
        public DateTime data_alteracao { get; set; }
        public string usuario { get; set; }
        public int id_solicitacao { get; set; }
        public DateTime data_email { get; set; }
        public DateTime data_pagamento { get; set; }
    }

    public class geral_horas_aulas_dadas
    {
        public DateTime data_aula { get; set; }
        public decimal hora_aula { get; set; }
        public int id_oferecimento { get; set; }
        public int id_curso { get; set; }
        public string nome_curso { get; set; }
        public int id_tipo_curso { get; set; }
        public string quadrimestre { get; set; }
        public string codigo_disciplina { get; set; }
        public string nome_disciplina { get; set; }
        public decimal valor_hora { get; set; }
        public string datas_aula { get; set; }
        public decimal total_hora_aula { get; set; }
        public decimal sub_total { get; set; }
    }

    public class geral_orientacoes_dadas
    {
        public string nome_curso { get; set; }
        public int id_tipo_curso { get; set; }
        public string tipo_banca { get; set; }
        public int id_curso { get; set; }
        public string orientacao { get; set; }
        public decimal sub_total { get; set; }
        public DateTime data_orientacao { get; set; }
    }

    public class geral_SolicitacaoPagto
    {
        public int id_professor { get; set; }
        public int id_plano { get; set; }
        public DateTime mes { get; set; }
        public string mes_string { get; set; }
        public decimal valor { get; set; }
        public string motivo { get; set; }
        public decimal valor_solicitado { get; set; }
        public decimal valor_pagar { get; set; }
    }

    public class geral_Solicitacao
    {
        public int id_professor { get; set; }
        public DateTime data_solicitacao { get; set; }
        public DateTime mes { get; set; }
        public string motivo { get; set; }
        public decimal valor { get; set; }
        public decimal valor_solicitado { get; set; }
        public decimal valor_total_solicitado { get; set; }
        public int id_plano { get; set; }
        public int id_solicitacao { get; set; }
        public string mes_string { get; set; }
        public string status { get; set; }
        public DateTime data_recebimento { get; set; }
        public DateTime data_pagamento { get; set; }
        public string nota_fiscal { get; set; }

    }

    public class geral_Boleto
    {
        public string DocumentoReceberPagar { get; set; }
        public string NotaFiscalRecibo { get; set; }
        public string NumeroNota { get; set; }
        public string CustoBoleto { get; set; }
        public string IDLancamento { get; set; }
        public string ValorReceberPagar { get; set; }
        public string NossoNumero { get; set; }
        public string DATAVENCIMENTO { get; set; }
        public decimal DATAVENCIMENTOINT { get; set; }
        public string NomePessoaFisicaJuridica { get; set; }
        public string CPFCNPJ { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string CEP { get; set; }
        public string NumeroBanco { get; set; }
        public string NumeroAgencia { get; set; }
        public string NomeAgencia { get; set; }
        public string NumeroConta { get; set; }
        public string NumeroConvenio { get; set; }
        public string Carteira { get; set; }
        public string Instrucoes { get; set; }
        public string Variacao { get; set; }
        public string CodigoCedente { get; set; }
        public string datapgtoint { get; set; }
    }

}
