﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<areas_concentracao_coordenadores> areas_concentracao_coordenadores { get; set; }
        public virtual DbSet<areas_concentracao_disciplinas> areas_concentracao_disciplinas { get; set; }
        public virtual DbSet<atualizacaoEndereco> atualizacaoEndereco { get; set; }
        public virtual DbSet<banca_professores> banca_professores { get; set; }
        public virtual DbSet<conceitos_de_aprovacao> conceitos_de_aprovacao { get; set; }
        public virtual DbSet<curso_valor_banca> curso_valor_banca { get; set; }
        public virtual DbSet<curso_valor_hora_aula> curso_valor_hora_aula { get; set; }
        public virtual DbSet<curso_valor_orientacao> curso_valor_orientacao { get; set; }
        public virtual DbSet<datas_aulas> datas_aulas { get; set; }
        public virtual DbSet<disciplinas_professores> disciplinas_professores { get; set; }
        public virtual DbSet<disciplinas_requisitos> disciplinas_requisitos { get; set; }
        public virtual DbSet<estados_registros> estados_registros { get; set; }
        public virtual DbSet<fornecedores> fornecedores { get; set; }
        public virtual DbSet<grupos_acesso> grupos_acesso { get; set; }
        public virtual DbSet<grupos_acesso_telas_sistema> grupos_acesso_telas_sistema { get; set; }
        public virtual DbSet<historico_inscricao> historico_inscricao { get; set; }
        public virtual DbSet<inadimplentes> inadimplentes { get; set; }
        public virtual DbSet<matricula> matricula { get; set; }
        public virtual DbSet<matricula_disciplina_outra_instituicao> matricula_disciplina_outra_instituicao { get; set; }
        public virtual DbSet<matricula_oferecimento> matricula_oferecimento { get; set; }
        public virtual DbSet<matricula_oferecimento_outro_curso> matricula_oferecimento_outro_curso { get; set; }
        public virtual DbSet<matricula_turma_orientacao> matricula_turma_orientacao { get; set; }
        public virtual DbSet<notas> notas { get; set; }
        public virtual DbSet<oferecimentos_professores> oferecimentos_professores { get; set; }
        public virtual DbSet<periodo_inscricao> periodo_inscricao { get; set; }
        public virtual DbSet<periodo_inscricao_curso> periodo_inscricao_curso { get; set; }
        public virtual DbSet<periodo_matricula> periodo_matricula { get; set; }
        public virtual DbSet<periodo_relatorio_dissertacao> periodo_relatorio_dissertacao { get; set; }
        public virtual DbSet<pre_oferecimentos> pre_oferecimentos { get; set; }
        public virtual DbSet<presenca> presenca { get; set; }
        public virtual DbSet<presenca_professor> presenca_professor { get; set; }
        public virtual DbSet<professor_data_recalculo> professor_data_recalculo { get; set; }
        public virtual DbSet<professor_plano> professor_plano { get; set; }
        public virtual DbSet<professor_planoOld> professor_planoOld { get; set; }
        public virtual DbSet<professor_solicitacao_pagamento> professor_solicitacao_pagamento { get; set; }
        public virtual DbSet<quadrimestres> quadrimestres { get; set; }
        public virtual DbSet<relatorio_dissertacao> relatorio_dissertacao { get; set; }
        public virtual DbSet<salas_aula> salas_aula { get; set; }
        public virtual DbSet<titulacao> titulacao { get; set; }
        public virtual DbSet<turmas_coordenadores> turmas_coordenadores { get; set; }
        public virtual DbSet<turmas_disciplinas> turmas_disciplinas { get; set; }
        public virtual DbSet<Cidade> Cidade { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<historico_notas> historico_notas { get; set; }
        public virtual DbSet<menu_config> menu_config { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<professor_fornecedor> professor_fornecedor { get; set; }
        public virtual DbSet<professor_solicitacao_planoOld> professor_solicitacao_planoOld { get; set; }
        public virtual DbSet<vw_disciplinas_historico> vw_disciplinas_historico { get; set; }
        public virtual DbSet<oferecimentos> oferecimentos { get; set; }
        public virtual DbSet<refTran> refTran { get; set; }
        public virtual DbSet<Enderecos> Enderecos { get; set; }
        public virtual DbSet<forma_recebimento> forma_recebimento { get; set; }
        public virtual DbSet<professores_forma_recebimento> professores_forma_recebimento { get; set; }
        public virtual DbSet<dados_contratos> dados_contratos { get; set; }
        public virtual DbSet<prorrogacao> prorrogacao { get; set; }
        public virtual DbSet<tipo_reuniao_cpg> tipo_reuniao_cpg { get; set; }
        public virtual DbSet<areas_concentracao> areas_concentracao { get; set; }
        public virtual DbSet<boletos> boletos { get; set; }
        public virtual DbSet<novidadesSistema> novidadesSistema { get; set; }
        public virtual DbSet<monitor_letreiro> monitor_letreiro { get; set; }
        public virtual DbSet<monitor> monitor { get; set; }
        public virtual DbSet<professor_solicitacao_plano> professor_solicitacao_plano { get; set; }
        public virtual DbSet<professor_observacoes_plano> professor_observacoes_plano { get; set; }
        public virtual DbSet<arquivos> arquivos { get; set; }
        public virtual DbSet<fichas_inscricao> fichas_inscricao { get; set; }
        public virtual DbSet<disciplinas> disciplinas { get; set; }
        public virtual DbSet<vw_historico> vw_historico { get; set; }
        public virtual DbSet<Configuracoes> Configuracoes { get; set; }
        public virtual DbSet<telas_sistema> telas_sistema { get; set; }
        public virtual DbSet<documentos_academicos_obs> documentos_academicos_obs { get; set; }
        public virtual DbSet<documentos_academicos> documentos_academicos { get; set; }
        public virtual DbSet<usuarios> usuarios { get; set; }
        public virtual DbSet<professores> professores { get; set; }
        public virtual DbSet<alunos> alunos { get; set; }
        public virtual DbSet<datas_aulas_professor> datas_aulas_professor { get; set; }
        public virtual DbSet<cursos_disciplinas> cursos_disciplinas { get; set; }
        public virtual DbSet<historico_matricula_turma> historico_matricula_turma { get; set; }
        public virtual DbSet<alunos_arquivos> alunos_arquivos { get; set; }
        public virtual DbSet<alunos_arquivos_tipo> alunos_arquivos_tipo { get; set; }
        public virtual DbSet<tipos_curso> tipos_curso { get; set; }
        public virtual DbSet<banca_dissertacao_obs> banca_dissertacao_obs { get; set; }
        public virtual DbSet<usuarios_log> usuarios_log { get; set; }
        public virtual DbSet<cursos> cursos { get; set; }
        public virtual DbSet<alunos_dataLimite_documentos_pendentes> alunos_dataLimite_documentos_pendentes { get; set; }
        public virtual DbSet<certificados_participantes> certificados_participantes { get; set; }
        public virtual DbSet<turmas> turmas { get; set; }
        public virtual DbSet<matricula_turma> matricula_turma { get; set; }
        public virtual DbSet<banca_dissertacao> banca_dissertacao { get; set; }
        public virtual DbSet<tipo_dissertacao> tipo_dissertacao { get; set; }
        public virtual DbSet<alunos_curso_inadimplente> alunos_curso_inadimplente { get; set; }
        public virtual DbSet<alunos_parcelas_inadimplente> alunos_parcelas_inadimplente { get; set; }
        public virtual DbSet<alunos_inadimpentes_fipt> alunos_inadimpentes_fipt { get; set; }
        public virtual DbSet<alunos_inadimplentes_emails_enviados> alunos_inadimplentes_emails_enviados { get; set; }
        public virtual DbSet<certificado_tipo_curso> certificado_tipo_curso { get; set; }
        public virtual DbSet<cursos_coordenadores> cursos_coordenadores { get; set; }
        public virtual DbSet<curso_valor_coordenacao> curso_valor_coordenacao { get; set; }
        public virtual DbSet<curso_tipo_coordenador> curso_tipo_coordenador { get; set; }
        public virtual DbSet<certificados> certificados { get; set; }
        public virtual DbSet<banca> banca { get; set; }
        public virtual DbSet<alunos_boletos> alunos_boletos { get; set; }
        public virtual DbSet<alunos_boletos_curso> alunos_boletos_curso { get; set; }
        public virtual DbSet<alunos_boletos_parcelas> alunos_boletos_parcelas { get; set; }
        public virtual DbSet<alunos_cadastro_automatico_det> alunos_cadastro_automatico_det { get; set; }
        public virtual DbSet<alunos_cadastro_automatico> alunos_cadastro_automatico { get; set; }
        public virtual DbSet<monitor_video> monitor_video { get; set; }
        public virtual DbSet<feriado> feriado { get; set; }
        public virtual DbSet<boleto_email> boleto_email { get; set; }
    }
}