using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace Repositorio_C
{
    public class ProfessorRepositorio : IDisposable
    {
        private Entities contextoEF;

        public ProfessorRepositorio()
        {
            contextoEF = new Entities();
        }

        public professores BuscaItem(professores pItem)
        {
            professores item = new professores();
            item = contextoEF.professores.Where(x => x.id_professor == pItem.id_professor).SingleOrDefault();
            return item;
        }

        public professores BuscaItem_byCPF(professores pItem)
        {
            professores item = new professores();
            item = contextoEF.professores.Where(x => x.cpf == pItem.cpf).SingleOrDefault();
            return item;
        }

        public professores CriarItem(professores pItem)
        {
            contextoEF.professores.Add(pItem);
            contextoEF.SaveChanges();
            pItem = contextoEF.professores.Include(x => x.professores_forma_recebimento).Include(x => x.professores_forma_recebimento.fornecedores).Where(x => x.id_professor == pItem.id_professor).SingleOrDefault();
            return pItem;
        }

        public professores VerificaCPFJaExistente(professores pItem)
        {
            professores item = new professores();
            item = contextoEF.professores.Where(x => x.cpf == pItem.cpf  && x.id_professor != pItem .id_professor).SingleOrDefault();
            return item;
        }

        public professores VerificaEmailJaExistente(professores pItem)
        {
            professores item = new professores();
            item = contextoEF.professores.Where(x => x.email == pItem.email && x.id_professor != pItem.id_professor).SingleOrDefault();
            return item;
        }

        public professores VerificaConfirmacaoEmail(professores pItem)
        {
            professores item = new professores();
            item = contextoEF.professores.Where(x => x.chave_confirmacao_email == pItem.chave_confirmacao_email).SingleOrDefault();
            if (item != null)
            {
                item.data_email_confirmado = DateTime.Now;
                item.email_confirmado = 1;
                contextoEF.SaveChanges();
            }
            return item;
        }

        public professores AlterarStatus(professores pItem)
        {
            professores item = new professores();
            item = contextoEF.professores.Where(x => x.id_professor == pItem.id_professor).SingleOrDefault();
            item.status = pItem.status;
            item.data_alteracao = DateTime.Now;
            item.usuario = pItem.usuario;
            contextoEF.SaveChanges();
            item = contextoEF.professores.Where(x => x.id_professor == pItem.id_professor).SingleOrDefault();
            return item;
        }

        public Boolean AlterarItem(professores pItem)
        {
            professores item = new professores();
            item = contextoEF.professores.Where(x => x.id_professor == pItem.id_professor).SingleOrDefault();
            item.nome = pItem.nome;
            item.cpf = pItem.cpf;
            item.cpf_passaporte = pItem.cpf_passaporte;
            item.sexo = pItem.sexo;
            item.data_nasc = pItem.data_nasc;
            item.tipo_documento = pItem.tipo_documento;
            item.numero_documento = pItem.numero_documento;
            item.nacionalidade = pItem.nacionalidade;
            item.url_lattes = pItem.url_lattes;
            item.email = pItem.email;
            item.email2 = pItem.email2;
            item.telefone_res = pItem.telefone_res;
            item.celular_res = pItem.celular_res;
            item.chave_confirmacao_email = pItem.chave_confirmacao_email;
            item.email_confirmado = pItem.email_confirmado;
            item.data_email_confirmacao = pItem.data_email_confirmacao;
            item.data_email_confirmado = pItem.data_email_confirmado;

            item.cep_res = pItem.cep_res;
            item.endereco_res = pItem.endereco_res;
            item.numero_res = pItem.numero_res;
            item.complemento_res = pItem.complemento_res;
            item.bairro_res = pItem.bairro_res;
            item.cidade_res = pItem.cidade_res;
            item.uf_res = pItem.uf_res;
            item.Placa = pItem.Placa;
            
            item.status = "alterado";
            item.data_alteracao = DateTime.Now;
            item.usuario = pItem.usuario;

            item.id_titulo = pItem.id_titulo;
            item.ano_obtencao_titulo = pItem.ano_obtencao_titulo;
            item.local_obtencao_titulo = pItem.local_obtencao_titulo;

            item.nome_banco = pItem.nome_banco;
            item.numero_banco = pItem.numero_banco;
            item.agencia_numero = pItem.agencia_numero;
            item.conta_numero = pItem.conta_numero;

            item.empresa = pItem.empresa;
            item.cep_empresa = pItem.cep_empresa;
            item.logradouro_empresa = pItem.logradouro_empresa;
            item.numero_empresa = pItem.numero_empresa;
            item.complemento_empresa = pItem.complemento_empresa;
            item.bairro_empresa = pItem.bairro_empresa;
            item.cidade_empresa = pItem.cidade_empresa;
            item.uf_empresa = pItem.uf_empresa;
            item.pais_empresa = pItem.pais_empresa;
            item.telefone_empresa = pItem.telefone_empresa;
            item.telefone_empresa_ramal = pItem.telefone_empresa_ramal;
            item.cargo = pItem.cargo;

            item.Observacao = pItem.Observacao;


            item.professores_forma_recebimento.horas_aula = pItem.professores_forma_recebimento.horas_aula;
            item.professores_forma_recebimento.horas_clt = pItem.professores_forma_recebimento.horas_clt;
            item.professores_forma_recebimento.horas_aula_adicional = pItem.professores_forma_recebimento.horas_aula_adicional;
            item.professores_forma_recebimento.orientacao = pItem.professores_forma_recebimento.orientacao;
            item.professores_forma_recebimento.banca = pItem.professores_forma_recebimento.banca;
            item.professores_forma_recebimento.id_fornecedor = pItem.professores_forma_recebimento.id_fornecedor;

            contextoEF.SaveChanges();
            return true;
        }

        public List<professores> ListaItem(professores pItem)
        {
            var c = contextoEF.professores.AsQueryable();
            List<professores> lista = new List<professores>();

            if (pItem.id_professor != 0)
            {
                c = c.Where(x => x.id_professor == pItem.id_professor);
            }

            if (pItem.nome != null)
            {
                c = c.Where(x => x.nome.Contains(pItem.nome));
            }

            if (pItem.cpf != null)
            {
                c = c.Where(x => x.cpf == pItem.cpf);
            }

            if (pItem.email != null)
            {
                c = c.Where(x => x.email.Contains(pItem.email));
            }

            if (pItem.numero_documento != null)
            {
                c = c.Where(x => x.numero_documento.Contains(pItem.numero_documento));
            }

            if (pItem.empresa != null)
            {
                c = c.Where(x => x.empresa.Contains(pItem.empresa));
            }

            if (pItem.status != "")
            {
                if (pItem.status == "ativado")
                {
                    c = c.Where(x => x.status != "inativado");
                }
                else if (pItem.status == "inativado")
                {
                    c = c.Where(x => x.status == "inativado");
                }
            }

            lista = c.OrderBy(x => x.nome).ToList();

            return lista;
        }

        public List<oferecimentos_professores> ListaOferecimento(professores pItem)
        {
            var c = contextoEF.oferecimentos_professores.AsQueryable();
            List<oferecimentos_professores> lista = new List<oferecimentos_professores>();

            if (pItem.id_professor != 0)
            {
                c = c.Where(x => x.id_professor == pItem.id_professor);
            }

            if (pItem.cpf != null)
            {
                var qIdProfessor = contextoEF.professores.Where(x => x.cpf.Contains(pItem.cpf)).Select(x => x.id_professor).ToArray();
                c = c.Where(x => qIdProfessor.Contains(x.id_professor));
            }

            if (pItem.nome != null)
            {
                var qIdProfessor2 = contextoEF.professores.Where(x => x.nome.Contains(pItem.nome)).Select(x => x.id_professor).ToArray();
                c = c.Where(x => qIdProfessor2.Contains(x.id_professor));
            }

            lista = c.OrderBy(x => x.professores.nome).ToList();

            return lista;
        }

        public List<presenca_professor> ListaAulasMarcadas(DateTime qData, string qPresente)
        {
            List<presenca_professor> lista;
            var iAux = contextoEF.datas_aulas.Where(x => x.data_aula.Value.Month == qData.Month && x.data_aula.Value.Year == qData.Year).Select(x => x.id_aula).ToArray();

            if (qPresente == "1" )
            {
                lista = contextoEF.presenca_professor.Where(x => iAux.Contains(x.id_aula) && x.presente==true).ToList();
            }
            else if (qPresente == "2")
            {
                lista = contextoEF.presenca_professor.Where(x => iAux.Contains(x.id_aula) && x.presente == false).ToList();
            }
            else
            {
                lista = contextoEF.presenca_professor.Where(x => iAux.Contains(x.id_aula)).ToList();
            }

            return lista;
        }

        public List<matricula_turma_orientacao> ListaOrientacao(professores pItem)
        {
            var c = contextoEF.matricula_turma_orientacao.AsQueryable();
            List<matricula_turma_orientacao> lista = new List<matricula_turma_orientacao>();

            if (pItem.id_professor != 0)
            {
                c = c.Where(x => x.id_professor == pItem.id_professor);
            }

            if (pItem.cpf != null)
            {
                var qIdProfessor = contextoEF.professores.Where(x => x.cpf.Contains(pItem.cpf)).Select(x => x.id_professor).ToArray();
                c = c.Where(x => qIdProfessor.Contains(x.id_professor));
            }

            if (pItem.nome != null)
            {
                var qIdProfessor2 = contextoEF.professores.Where(x => x.nome.Contains(pItem.nome)).Select(x => x.id_professor).ToArray();
                c = c.Where(x => qIdProfessor2.Contains(x.id_professor));
            }

            lista = c.OrderBy(x => x.professores.nome).ToList();

            return lista;
        }

        public List<banca_professores> ListaQualificacaoDefesa(professores pItem, string sTipoBanca)
        {
            var c = contextoEF.banca_professores.AsQueryable();
            List<banca_professores> lista = new List<banca_professores>();

            if (pItem.id_professor != 0)
            {
                c = c.Where(x => x.id_professor == pItem.id_professor);
            }

            if (pItem.cpf != null)
            {
                var qIdProfessor = contextoEF.professores.Where(x => x.cpf.Contains(pItem.cpf)).Select(x => x.id_professor).ToArray();
                c = c.Where(x => qIdProfessor.Contains(x.id_professor));
            }

            if (pItem.nome != null)
            {
                var qIdProfessor2 = contextoEF.professores.Where(x => x.nome.Contains(pItem.nome)).Select(x => x.id_professor).ToArray();
                c = c.Where(x => qIdProfessor2.Contains(x.id_professor));
            }

            lista = c.Where(x=> x.banca.tipo_banca == sTipoBanca).OrderBy(x => x.professores.nome).ToList();

            return lista;
        }

        public professor_data_recalculo BuscaDataRecalculo()
        {
            professor_data_recalculo item;
            item = contextoEF.professor_data_recalculo.Where(x => x.id_data_recalculo == 1).SingleOrDefault();
            return item;
        }

        public professor_data_recalculo AlteraDataRecalculo(professor_data_recalculo pItem)
        {
            professor_data_recalculo item;
            item = contextoEF.professor_data_recalculo.Where(x => x.id_data_recalculo == 1).SingleOrDefault();
            item.data_recalculo = pItem.data_recalculo;
            contextoEF.SaveChanges();
            return item;
        }

        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}

