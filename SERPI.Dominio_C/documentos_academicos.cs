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
    
    public partial class documentos_academicos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public documentos_academicos()
        {
            this.documentos_academicos_obs = new HashSet<documentos_academicos_obs>();
        }
    
        public int id_documentos_academicos { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string tipo_arquivo { get; set; }
        public Nullable<System.DateTime> data_cadastro { get; set; }
        public Nullable<System.DateTime> data_alteracao { get; set; }
        public string usuario { get; set; }
        public string status { get; set; }
        public Nullable<short> ativo { get; set; }
        public Nullable<int> id_tipo_curso { get; set; }
        public string nome_arquivo { get; set; }
        public string nome_arquivoPreview { get; set; }
        public string nomePreview { get; set; }
        public string descricaoPreview { get; set; }
        public string tipo_arquivoPreview { get; set; }
        public Nullable<int> id_tipo_cursoPreview { get; set; }
        public Nullable<System.DateTime> data_aprovacao { get; set; }
        public Nullable<System.DateTime> data_reprovacao { get; set; }
        public Nullable<short> statusAprovacao { get; set; }
        public string usuarioAprovacao { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<documentos_academicos_obs> documentos_academicos_obs { get; set; }
        public virtual tipos_curso tipos_curso { get; set; }
        public virtual tipos_curso tipos_curso1 { get; set; }
    }
}
