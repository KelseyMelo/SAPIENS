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
    
    public partial class professores_forma_recebimento
    {
        public decimal id_professor { get; set; }
        public Nullable<int> horas_aula { get; set; }
        public Nullable<int> orientacao { get; set; }
        public Nullable<int> banca { get; set; }
        public Nullable<int> id_fornecedor { get; set; }
        public Nullable<int> horas_clt { get; set; }
        public Nullable<int> horas_aula_adicional { get; set; }
    
        public virtual forma_recebimento forma_recebimento { get; set; }
        public virtual forma_recebimento forma_recebimento1 { get; set; }
        public virtual forma_recebimento forma_recebimento2 { get; set; }
        public virtual fornecedores fornecedores { get; set; }
        public virtual professores professores { get; set; }
    }
}
