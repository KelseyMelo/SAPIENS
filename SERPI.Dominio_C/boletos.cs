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
    
    public partial class boletos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public boletos()
        {
            this.refTran1 = new HashSet<refTran>();
            this.fichas_inscricao = new HashSet<fichas_inscricao>();
        }
    
        public int id_boleto { get; set; }
        public string id_conv { get; set; }
        public string refTran { get; set; }
        public string valor { get; set; }
        public System.DateTime data_vencimento { get; set; }
        public string cpf { get; set; }
        public string nome { get; set; }
        public string endereco { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string cep { get; set; }
        public string msgLoja { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> data_pagamento { get; set; }
        public Nullable<System.DateTime> data_cadastro { get; set; }
        public Nullable<System.DateTime> data_alteracao { get; set; }
        public string usuario { get; set; }
        public Nullable<System.DateTime> data_cancelamento { get; set; }
        public Nullable<System.DateTime> data_verificacao_sem_registro { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<refTran> refTran1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<fichas_inscricao> fichas_inscricao { get; set; }
    }
}
