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
    
    public partial class prorrogacao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public prorrogacao()
        {
            this.historico_matricula_turma = new HashSet<historico_matricula_turma>();
        }
    
        public decimal id_prorrogacao { get; set; }
        public int id_reuniao { get; set; }
        public decimal id_aluno { get; set; }
        public Nullable<System.DateTime> data_qualificacao { get; set; }
        public Nullable<System.DateTime> data_defesa { get; set; }
        public Nullable<System.DateTime> data_deposito { get; set; }
        public string parecer { get; set; }
        public string observacao { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> data_alteracao { get; set; }
        public Nullable<System.DateTime> data_cadastro { get; set; }
        public string usuario { get; set; }
        public Nullable<int> id_matricula_turma { get; set; }
        public Nullable<System.DateTime> data_inicio { get; set; }
        public Nullable<System.DateTime> data_fim { get; set; }
        public Nullable<int> id_tipo_reuniao_cpg { get; set; }
    
        public virtual tipo_reuniao_cpg tipo_reuniao_cpg { get; set; }
        public virtual alunos alunos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<historico_matricula_turma> historico_matricula_turma { get; set; }
        public virtual matricula_turma matricula_turma { get; set; }
    }
}