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
    
    public partial class matricula_turma_orientacao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public matricula_turma_orientacao()
        {
            this.relatorio_dissertacao = new HashSet<relatorio_dissertacao>();
        }
    
        public decimal id_orientacao { get; set; }
        public int id_matricula_turma { get; set; }
        public decimal id_professor { get; set; }
        public string tipo_orientacao { get; set; }
        public string titulo { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> data_cadastro { get; set; }
        public Nullable<System.DateTime> data_alteracao { get; set; }
        public string usuario { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<relatorio_dissertacao> relatorio_dissertacao { get; set; }
        public virtual professores professores { get; set; }
        public virtual matricula_turma matricula_turma { get; set; }
    }
}
