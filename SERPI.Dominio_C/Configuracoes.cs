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
    
    public partial class Configuracoes
    {
        public int id_configuracao { get; set; }
        public string servidor_email { get; set; }
        public string conta_email { get; set; }
        public string senha_email { get; set; }
        public Nullable<int> porta_email { get; set; }
        public string remetente_email { get; set; }
        public string copia_email { get; set; }
        public string nome_remetente_email { get; set; }
    }
}
