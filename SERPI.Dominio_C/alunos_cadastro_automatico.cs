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
    
    public partial class alunos_cadastro_automatico
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public alunos_cadastro_automatico()
        {
            this.alunos_cadastro_automatico_det = new HashSet<alunos_cadastro_automatico_det>();
        }
    
        public int id_cadastro_automatico { get; set; }
        public string descricao { get; set; }
        public Nullable<System.DateTime> data_importacao { get; set; }
        public string usuario { get; set; }
        public string descricao_curso { get; set; }
        public Nullable<System.DateTime> data_alteracao { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<alunos_cadastro_automatico_det> alunos_cadastro_automatico_det { get; set; }
    }
}
