using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class DocumentosAcademicosAplicacao
    {
        private readonly DocumentosAcademicosRepositorio Repositorio = new DocumentosAcademicosRepositorio();

        public documentos_academicos BuscaItem(documentos_academicos pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public documentos_academicos CriarItem(documentos_academicos pItem)
        {
            return Repositorio.CriarItem(pItem);
        }

        public Boolean AlterarItem(documentos_academicos pItem)
        {
            return Repositorio.AlterarItem(pItem);
        }

        public List<documentos_academicos> ListaItemAguardandoAprovacao()
        {
            return Repositorio.ListaItemAguardandoAprovacao();
        }

        public List<documentos_academicos> ListaItemReprovado(string qUsuario)
        {
            return Repositorio.ListaItemReprovado(qUsuario);
        }

        public documentos_academicos_obs CriarItem_Obs(documentos_academicos_obs pItem)
        {
            return Repositorio.CriarItem_Obs(pItem);
        }

        public Boolean AlterarItem_Obs(documentos_academicos_obs pItem)
        {
            return Repositorio.AlterarItem_Obs(pItem);
        }

        public Boolean AlterarItem_Aprovacao(documentos_academicos pItem)
        {
            return Repositorio.AlterarItem_Aprovacao(pItem);
        }

        public Boolean AlterarItem_Reprovacao(documentos_academicos pItem)
        {
            return Repositorio.AlterarItem_Reprovacao(pItem);
        }

        public List<documentos_academicos> ListaItemAprovacaoHomePage()
        {
            return Repositorio.ListaItemAprovacaoHomePage();
        }

        public List<documentos_academicos> ListaItemReprovacaoHomePage(string qUsuario)
        {
            return Repositorio.ListaItemReprovacaoHomePage(qUsuario);
        }

        public documentos_academicos AlterarStatus(documentos_academicos pItem)
        {
            return Repositorio.AlterarStatus(pItem);
        }

        public List<documentos_academicos> ListaItem(documentos_academicos pItem)
        {
            return Repositorio.ListaItem(pItem);
        }
    }
}
