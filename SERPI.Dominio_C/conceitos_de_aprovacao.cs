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
    
    public partial class conceitos_de_aprovacao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public conceitos_de_aprovacao()
        {
            this.matricula_disciplina_outra_instituicao = new HashSet<matricula_disciplina_outra_instituicao>();
            this.notas = new HashSet<notas>();
        }
    
        public string conceito { get; set; }
        public string descricao { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> data_alteracao { get; set; }
        public Nullable<System.DateTime> data_cadastro { get; set; }
        public string usuario { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<matricula_disciplina_outra_instituicao> matricula_disciplina_outra_instituicao { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<notas> notas { get; set; }
    }
}
