using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class MonitorAplicacao
    {
        private readonly MonitorRepositorio Repositorio = new MonitorRepositorio();

        public monitor BuscaItem(monitor pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public monitor_letreiro BuscaItem(monitor_letreiro pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public monitor_video BuscaItem(monitor_video pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public monitor CriarItem(monitor pItem)
        {
            return Repositorio.CriarItem(pItem);
        }

        public monitor_letreiro CriarItem(monitor_letreiro pItem)
        {
            return Repositorio.CriarItem(pItem);
        }

        public monitor_video CriarItem(monitor_video pItem)
        {
            return Repositorio.CriarItem(pItem);
        }

        public monitor_video AlterarStatus(monitor_video pItem)
        {
            return Repositorio.AlterarStatus(pItem);
        }

        public monitor AlterarStatus(monitor pItem)
        {
            return Repositorio.AlterarStatus(pItem);
        }

        public Boolean AlterarItem(monitor pItem)
        {
            return Repositorio.AlterarItem(pItem);
        }

        public Boolean AlterarItem(monitor_letreiro pItem)
        {
            return Repositorio.AlterarItem(pItem);
        }

        public Boolean AlterarItem(monitor_video pItem)
        {
            return Repositorio.AlterarItem(pItem);
        }

        public List<monitor> ListaItem(monitor pItem)
        {
            return Repositorio.ListaItem(pItem);
        }

        public List<monitor> ListaItemQuadro(monitor pItem)
        {
            return Repositorio.ListaItemQuadro(pItem);
        }

        public List<monitor_video> ListaItem(monitor_video pItem)
        {
            return Repositorio.ListaItem(pItem);
        }

        public List<monitor> ListaItem_Ativos()
        {
            return Repositorio.ListaItem_Ativos();
        }

        public List<monitor_video> ListaItem_Videos()
        {
            return Repositorio.ListaItem_Videos();
        }

    }
}

