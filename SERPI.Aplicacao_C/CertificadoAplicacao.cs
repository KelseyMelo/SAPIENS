
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class CertificadoAplicacao
    {
        private readonly CertificadoRepositorio Repositorio = new CertificadoRepositorio();

        public certificados BuscaItem(certificados pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public certificados CriarItem(certificados pItem)
        {
            return Repositorio.CriarItem(pItem);
        }

        public Boolean AlterarItem(certificados pItem)
        {
            return Repositorio.AlterarItem(pItem);
        }

        public List<certificados> ListaItem(certificados pItem)
        {
            return Repositorio.ListaItem(pItem);
        }

        public certificados_participantes BuscaItem_Participante(certificados_participantes pItem)
        {
            return Repositorio.BuscaItem_Participante(pItem);
        }

        public int BuscaItem_NumeroMaximo()
        {
            return Repositorio.BuscaItem_NumeroMaximo();
        }

        public bool LimpaItem_Participante(certificados_participantes pItem)
        {
            return Repositorio.LimpaItem_Participante(pItem);
        }

        public certificados_participantes CriarItem_Participante(certificados_participantes pItem)
        {
            return Repositorio.CriarItem_Participante(pItem);
        }

        public bool VerificaExisteNum_Seq(int iAux)
        {
            return Repositorio.VerificaExisteNum_Seq(iAux);
        }

        public int MaxNum_Seq()
        {
            return Repositorio.MaxNum_Seq();
        }

        public Boolean AlterarItem_participante(certificados_participantes pItem)
        {
            return Repositorio.AlterarItem_participante(pItem);
        }

        public List<certificados_participantes> ListaItem_participante(certificados_participantes pItem)
        {
            return Repositorio.ListaItem_participante(pItem);
        }

        public certificados_participantes Busca_Certificado_Participante_byChave(certificados_participantes pItem)
        {
            return Repositorio.Busca_Certificado_Participante_byChave(pItem);
        }

        public List<certificado_tipo_curso> Lista_certificado_tipo_curso()
        {
            return Repositorio.Lista_certificado_tipo_curso();
        }
    }
}
