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
    
    public partial class matricula_disciplina_outra_instituicao
    {
        public decimal id_matricula_disciplina { get; set; }
        public Nullable<decimal> id_disciplina_substituindo { get; set; }
        public int id_reuniao { get; set; }
        public decimal id_aluno { get; set; }
        public string instituicao { get; set; }
        public string nome { get; set; }
        public string conceito { get; set; }
        public Nullable<int> creditos { get; set; }
        public Nullable<int> carga_horaria { get; set; }
        public Nullable<decimal> frequencia { get; set; }
        public Nullable<System.DateTime> inicio { get; set; }
        public Nullable<System.DateTime> fim { get; set; }
        public string parecer { get; set; }
        public string observacao { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> data_cadastro { get; set; }
        public Nullable<System.DateTime> data_alteracao { get; set; }
        public string usuario { get; set; }
    
        public virtual conceitos_de_aprovacao conceitos_de_aprovacao { get; set; }
        public virtual disciplinas disciplinas { get; set; }
        public virtual alunos alunos { get; set; }
    }
}
