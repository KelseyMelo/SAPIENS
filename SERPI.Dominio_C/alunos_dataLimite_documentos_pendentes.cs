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
    
    public partial class alunos_dataLimite_documentos_pendentes
    {
        public int id_alunos_dataLimite_documentos_pendentes { get; set; }
        public Nullable<System.DateTime> data_limite { get; set; }
        public string observacao { get; set; }
        public Nullable<System.DateTime> data_cadastro { get; set; }
        public string usuario { get; set; }
        public Nullable<decimal> idaluno { get; set; }
        public Nullable<short> tipo_registro { get; set; }
    
        public virtual alunos alunos { get; set; }
    }
}