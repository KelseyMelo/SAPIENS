using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Repositorio_C
{
    public class CertificadoRepositorio : IDisposable
    {
        private Entities contextoEF;

        public CertificadoRepositorio()
        {
            contextoEF = new Entities();
        }

        public certificados BuscaItem(certificados pItem)
        {
            certificados item = new certificados();
            item = contextoEF.certificados.Where(x => x.id_certificado == pItem.id_certificado).SingleOrDefault();
            //item = contextoEF.certificados.Include(x => x.tipos_curso).Include(x => x.turmas).Include(x => x.oferecimentos).Include(x => x.periodo_relatorio_dissertacao).Where(x => x.quadrimestre == pItem.quadrimestre).SingleOrDefault();
            return item;
        }

        public certificados CriarItem(certificados pItem)
        {
            contextoEF.certificados.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public Boolean AlterarItem(certificados pItem)
        {
            certificados item = new certificados();
            item = contextoEF.certificados.Where(x => x.id_certificado == pItem.id_certificado).SingleOrDefault();

            item.ano = pItem.ano;
            item.campo1a = pItem.campo1a;
            item.campo2a = pItem.campo2a;
            item.campo3a = pItem.campo3a;
            item.campo3b = pItem.campo3b;
            item.campo3c = pItem.campo3c;
            item.campo3d = pItem.campo3d;
            item.assinatura1_cargo = pItem.assinatura1_cargo;
            item.assinatura1_imagem = pItem.assinatura1_imagem;
            item.assinatura1_nome= pItem.assinatura1_nome;
            item.assinatura2_cargo = pItem.assinatura2_cargo;
            item.assinatura2_imagem = pItem.assinatura2_imagem;
            item.assinatura2_nome = pItem.assinatura2_nome;
            item.data_alteracao = pItem.data_alteracao;
            item.data_evento = pItem.data_evento;
            item.evento = pItem.evento;
            item.numero_seq_inicial = pItem.numero_seq_inicial;
            item.situacao = pItem.situacao;
            item.usuario = pItem.usuario;
            item.tipo_certificado = pItem.tipo_certificado;
            item.id_certificado_tipo_curso = pItem.id_certificado_tipo_curso;
            item.informacao_adicional = pItem.informacao_adicional;
            item.obs_folha2 = pItem.obs_folha2;
            item.palestrante = pItem.palestrante;
            contextoEF.SaveChanges();
            return true;
        }


        public List<certificados> ListaItem(certificados pItem)
        {
            var c = contextoEF.certificados.AsQueryable();
            List<certificados> lista = new List<certificados>();

            if (pItem.ano != null)
            {
                c = c.Where(x => x.ano == pItem.ano);
            }

            if (pItem.id_certificado != 0)
            {
                c = c.Where(x => x.id_certificado == pItem.id_certificado);
            }

            if (pItem.evento != "")
            {
                c = c.Where(x => x.evento.Contains(pItem.evento));
            }

            //if (pItem.status != "")
            //{
            //    if (pItem.status == "ativado")
            //    {
            //        c = c.Where(x => x.status != "inativado");
            //    }
            //    else if (pItem.status == "inativado")
            //    {
            //        c = c.Where(x => x.status == "inativado");
            //    }
            //}

            lista = c.OrderByDescending(x => x.data_evento).ThenBy(x => x.evento).ToList();
            //lista = c.ToList();
            return lista;
        }

        /// //////////////////////////////////////////

        public certificados_participantes BuscaItem_Participante(certificados_participantes pItem)
        {
            certificados_participantes item = new certificados_participantes();
            item = contextoEF.certificados_participantes.Where(x => x.id_certificado_participante == pItem.id_certificado_participante).SingleOrDefault();
            return item;
        }

        public int BuscaItem_NumeroMaximo()
        {
            List<certificados_participantes> lista = new List<certificados_participantes>();
            int iAux;

            iAux = contextoEF.certificados_participantes.Max(x => x.numero_seq).Value;

            return iAux;
        }

        public bool LimpaItem_Participante(certificados_participantes pItem)
        {
            contextoEF.certificados_participantes.RemoveRange(contextoEF.certificados_participantes.Where(x => x.id_certificado == pItem.id_certificado));
            contextoEF.SaveChanges();
            return true;
        }

        public certificados_participantes CriarItem_Participante(certificados_participantes pItem)
        {
            contextoEF.certificados_participantes.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public bool VerificaExisteNum_Seq(int iAux)
        {
            certificados_participantes item  = contextoEF.certificados_participantes.Where(x=> x.numero_seq == iAux).SingleOrDefault() ;
            if (item == null)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }

        public int MaxNum_Seq()
        {
            return contextoEF.certificados_participantes.Max(x=> x.numero_seq).Value;
        }

        public Boolean AlterarItem_participante(certificados_participantes pItem)
        {
            certificados_participantes item = new certificados_participantes();
            item = contextoEF.certificados_participantes.Where(x => x.id_certificado_participante == pItem.id_certificado_participante).SingleOrDefault();

            item.interno_externo = pItem.interno_externo;
            item.arquivo_pdf = pItem.arquivo_pdf;
            item.data_importacao = pItem.data_importacao;
            item.nome = pItem.nome;
            item.numero_seq = pItem.numero_seq;
            item.usuario = pItem.usuario;
            item.data_envio_email = pItem.data_envio_email;
            item.data_download = pItem.data_download;
            item.senha = pItem.senha;
            contextoEF.SaveChanges();
            return true;
        }

        public List<certificados_participantes> ListaItem_participante(certificados_participantes pItem)
        {
            var c = contextoEF.certificados_participantes.AsQueryable();
            List<certificados_participantes> lista = new List<certificados_participantes>();

            if (pItem.numero_seq != null)
            {
                c = c.Where(x => x.numero_seq == pItem.numero_seq);
            }

            if (pItem.id_certificado != null)
            {
                c = c.Where(x => x.id_certificado == pItem.id_certificado);
            }

            if (pItem.id_certificado_participante != 0)
            {
                c = c.Where(x => x.id_certificado_participante == pItem.id_certificado_participante);
            }

            if (pItem.nome != "")
            {
                c = c.Where(x => x.nome.Contains(pItem.nome));
            }

            //if (pItem.status != "")
            //{
            //    if (pItem.status == "ativado")
            //    {
            //        c = c.Where(x => x.status != "inativado");
            //    }
            //    else if (pItem.status == "inativado")
            //    {
            //        c = c.Where(x => x.status == "inativado");
            //    }
            //}

            lista = c.OrderBy(x => x.numero_seq).ToList();

            return lista;
        }

        public certificados_participantes Busca_Certificado_Participante_byChave(certificados_participantes pItem)
        {
            certificados_participantes item;
            item = contextoEF.certificados_participantes.Where(x => x.chave_participante == pItem.chave_participante).SingleOrDefault();
            return item;
        }

        public List<certificado_tipo_curso> Lista_certificado_tipo_curso()
        {
            List<certificado_tipo_curso> lista;
            lista = contextoEF.certificado_tipo_curso.OrderBy(x=> x.ordem).ToList();
            return lista;
        }
        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}
