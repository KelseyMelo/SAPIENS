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
    
    public partial class banca_professores
    {
        public decimal id_banca { get; set; }
        public decimal id_professor { get; set; }
        public string tipo_professor { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> data_alteracao { get; set; }
        public Nullable<System.DateTime> data_cadastro { get; set; }
        public string usuario { get; set; }
        public bool imprimir { get; set; }
    
        public virtual professores professores { get; set; }
        public virtual banca banca { get; set; }
    }
}