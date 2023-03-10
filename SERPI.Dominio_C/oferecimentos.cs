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
    
    public partial class oferecimentos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public oferecimentos()
        {
            this.datas_aulas = new HashSet<datas_aulas>();
            this.matricula_oferecimento = new HashSet<matricula_oferecimento>();
            this.notas = new HashSet<notas>();
            this.oferecimentos_professores = new HashSet<oferecimentos_professores>();
            this.presenca_professor = new HashSet<presenca_professor>();
            this.presenca = new HashSet<presenca>();
        }
    
        public decimal id_oferecimento { get; set; }
        public decimal id_disciplina { get; set; }
        public bool ativo { get; set; }
        public string quadrimestre { get; set; }
        public int num_oferecimento { get; set; }
        public string objetivo { get; set; }
        public string ementa { get; set; }
        public Nullable<int> carga_horaria { get; set; }
        public Nullable<int> num_max_alunos { get; set; }
        public Nullable<int> creditos { get; set; }
        public string justificativa { get; set; }
        public string forma_avaliacao { get; set; }
        public string material_utilizado { get; set; }
        public string metodologia { get; set; }
        public string conhecimentos_previos { get; set; }
        public string bibliografia_basica { get; set; }
        public string bibliografica_compl { get; set; }
        public string programa_disciplina { get; set; }
        public string observacao { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> data_cadastro { get; set; }
        public Nullable<System.DateTime> data_alteracao { get; set; }
        public string usuario { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<datas_aulas> datas_aulas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<matricula_oferecimento> matricula_oferecimento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<notas> notas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<oferecimentos_professores> oferecimentos_professores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<presenca_professor> presenca_professor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<presenca> presenca { get; set; }
        public virtual quadrimestres quadrimestres { get; set; }
        public virtual disciplinas disciplinas { get; set; }
    }
}
