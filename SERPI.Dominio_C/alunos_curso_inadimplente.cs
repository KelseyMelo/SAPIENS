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
    
    public partial class alunos_curso_inadimplente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public alunos_curso_inadimplente()
        {
            this.alunos_parcelas_inadimplente = new HashSet<alunos_parcelas_inadimplente>();
        }
    
        public Nullable<int> id_aluno_inadimplente { get; set; }
        public int id_aluno_curso_inadimplente { get; set; }
        public Nullable<int> IDCurso { get; set; }
        public string NomeCurso { get; set; }
        public string Ocorrencias { get; set; }
        public Nullable<System.DateTime> data_inclusao_serasa { get; set; }
        public Nullable<System.DateTime> data_exclusao_serasa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<alunos_parcelas_inadimplente> alunos_parcelas_inadimplente { get; set; }
        public virtual alunos_inadimpentes_fipt alunos_inadimpentes_fipt { get; set; }
    }
}
