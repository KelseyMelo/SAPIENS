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
    
    public partial class periodo_inscricao_curso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public periodo_inscricao_curso()
        {
            this.fichas_inscricao = new HashSet<fichas_inscricao>();
        }
    
        public int id_periodo { get; set; }
        public int id_curso { get; set; }
        public Nullable<decimal> valor { get; set; }
        public string mensagem_boleto { get; set; }
    
        public virtual periodo_inscricao periodo_inscricao { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<fichas_inscricao> fichas_inscricao { get; set; }
        public virtual cursos cursos { get; set; }
    }
}
