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
    
    public partial class grupos_acesso_telas_sistema
    {
        public int id_grupo { get; set; }
        public int id_tela { get; set; }
        public Nullable<bool> leitura { get; set; }
        public Nullable<bool> escrita { get; set; }
        public Nullable<bool> modificacao { get; set; }
        public Nullable<bool> exclusao { get; set; }
    
        public virtual grupos_acesso grupos_acesso { get; set; }
        public virtual telas_sistema telas_sistema { get; set; }
    }
}
