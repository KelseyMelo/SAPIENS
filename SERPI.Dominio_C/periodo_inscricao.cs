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
    
    public partial class periodo_inscricao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public periodo_inscricao()
        {
            this.periodo_inscricao_curso = new HashSet<periodo_inscricao_curso>();
            this.fichas_inscricao = new HashSet<fichas_inscricao>();
        }
    
        public int id_periodo { get; set; }
        public string quadrimestre { get; set; }
        public System.DateTime data_inicio { get; set; }
        public Nullable<System.DateTime> data_fim { get; set; }
        public Nullable<System.DateTime> data_limite_pagamento { get; set; }
        public Nullable<System.DateTime> data_prova { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<periodo_inscricao_curso> periodo_inscricao_curso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<fichas_inscricao> fichas_inscricao { get; set; }
    }
}
