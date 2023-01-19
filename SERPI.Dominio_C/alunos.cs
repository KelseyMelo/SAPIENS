//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SERPI.Dominio_C
{
    using System;
    using System.Collections.Generic;
    
    public partial class alunos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public alunos()
        {
            this.atualizacaoEndereco = new HashSet<atualizacaoEndereco>();
            this.matricula = new HashSet<matricula>();
            this.matricula_disciplina_outra_instituicao = new HashSet<matricula_disciplina_outra_instituicao>();
            this.matricula_oferecimento = new HashSet<matricula_oferecimento>();
            this.notas = new HashSet<notas>();
            this.presenca = new HashSet<presenca>();
            this.prorrogacao = new HashSet<prorrogacao>();
            this.alunos_arquivos = new HashSet<alunos_arquivos>();
            this.alunos_dataLimite_documentos_pendentes = new HashSet<alunos_dataLimite_documentos_pendentes>();
            this.matricula_turma = new HashSet<matricula_turma>();
            this.alunos_inadimpentes_fipt = new HashSet<alunos_inadimpentes_fipt>();
            this.alunos_inadimplentes_emails_enviados = new HashSet<alunos_inadimplentes_emails_enviados>();
            this.alunos_boletos = new HashSet<alunos_boletos>();
            this.alunos_cadastro_automatico_det = new HashSet<alunos_cadastro_automatico_det>();
            this.boleto_email = new HashSet<boleto_email>();
        }
    
        public decimal idaluno { get; set; }
        public string nome { get; set; }
        public string estrangeiro { get; set; }
        public string cpf { get; set; }
        public string tipo_documento { get; set; }
        public string numero_documento { get; set; }
        public string orgao_expedidor { get; set; }
        public Nullable<System.DateTime> data_expedicao { get; set; }
        public Nullable<System.DateTime> data_validade { get; set; }
        public Nullable<System.DateTime> data_nascimento { get; set; }
        public string pais_nasc { get; set; }
        public string cidade_nasc { get; set; }
        public string sexo { get; set; }
        public string logradouro_res { get; set; }
        public string numero_res { get; set; }
        public string complemento_res { get; set; }
        public string bairro_res { get; set; }
        public string cidade_res { get; set; }
        public string uf_res { get; set; }
        public string pais_res { get; set; }
        public string cep_res { get; set; }
        public string telefone_res { get; set; }
        public string celular_res { get; set; }
        public string email { get; set; }
        public string formacao { get; set; }
        public string escola { get; set; }
        public Nullable<int> ano_graduacao { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> data_cadastro { get; set; }
        public Nullable<System.DateTime> data_alteracao { get; set; }
        public string usuario { get; set; }
        public string nome_fantasia { get; set; }
        public string empresa { get; set; }
        public string cnpj { get; set; }
        public string ie { get; set; }
        public string contato { get; set; }
        public string email_contato { get; set; }
        public string logradouro_empresa { get; set; }
        public string numero_empresa { get; set; }
        public string complemento_empresa { get; set; }
        public string bairro_empresa { get; set; }
        public string cidade_empresa { get; set; }
        public string uf_empresa { get; set; }
        public string pais_empresa { get; set; }
        public string cep_empresa { get; set; }
        public string telefone_empresa { get; set; }
        public string telefone_empresa_ramal { get; set; }
        public string cargo { get; set; }
        public string email2 { get; set; }
        public bool entregou_rg { get; set; }
        public bool entregou_cpf { get; set; }
        public bool entregou_historico { get; set; }
        public bool entregou_diploma { get; set; }
        public bool entregou_comprovante_end { get; set; }
        public bool entregou_fotos { get; set; }
        public bool entregou_certidao { get; set; }
        public bool entregou_contrato { get; set; }
        public string ocorrencias { get; set; }
        public string convenio { get; set; }
        public string linha_pesquisa { get; set; }
        public Nullable<int> id_estado_nasc { get; set; }
        public string digito_num_documento { get; set; }
        public Nullable<int> id_pais_nasc { get; set; }
        public Nullable<int> id_pais { get; set; }
        public string estado_nasc { get; set; }
        public Nullable<byte> RefazerProficienciaIngles { get; set; }
        public Nullable<byte> RefazerProvaPortugues { get; set; }
        public string palavra_chave { get; set; }
        public string profissao { get; set; }
        public string estado_civil { get; set; }
    
        public virtual inadimplentes inadimplentes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<atualizacaoEndereco> atualizacaoEndereco { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<matricula> matricula { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<matricula_disciplina_outra_instituicao> matricula_disciplina_outra_instituicao { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<matricula_oferecimento> matricula_oferecimento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<notas> notas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<presenca> presenca { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<prorrogacao> prorrogacao { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<alunos_arquivos> alunos_arquivos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<alunos_dataLimite_documentos_pendentes> alunos_dataLimite_documentos_pendentes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<matricula_turma> matricula_turma { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<alunos_inadimpentes_fipt> alunos_inadimpentes_fipt { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<alunos_inadimplentes_emails_enviados> alunos_inadimplentes_emails_enviados { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<alunos_boletos> alunos_boletos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<alunos_cadastro_automatico_det> alunos_cadastro_automatico_det { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<boleto_email> boleto_email { get; set; }
    }
}
