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
    
    public partial class quadrimestres
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public quadrimestres()
        {
            this.periodo_relatorio_dissertacao = new HashSet<periodo_relatorio_dissertacao>();
            this.oferecimentos = new HashSet<oferecimentos>();
            this.turmas = new HashSet<turmas>();
        }
    
        public string ano { get; set; }
        public Nullable<int> numero { get; set; }
        public Nullable<System.DateTime> data_inicio { get; set; }
        public Nullable<System.DateTime> data_fim { get; set; }
        public string quadrimestre { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> data_cadastro { get; set; }
        public Nullable<System.DateTime> data_alteracao { get; set; }
        public string usuario { get; set; }
        public Nullable<int> id_tipo_curso { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<periodo_relatorio_dissertacao> periodo_relatorio_dissertacao { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<oferecimentos> oferecimentos { get; set; }
        public virtual tipos_curso tipos_curso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<turmas> turmas { get; set; }
    }
}
