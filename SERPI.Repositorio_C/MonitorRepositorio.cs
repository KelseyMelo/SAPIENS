
using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Repositorio_C
{
    public class MonitorRepositorio : IDisposable
    {
        private Entities contextoEF;

        public MonitorRepositorio()
        {
            contextoEF = new Entities();
        }

        public monitor BuscaItem(monitor pItem)
        {
            monitor item = new monitor();
            item = contextoEF.monitor.Where(x => x.id_monitor == pItem.id_monitor).SingleOrDefault();
            return item;
        }

        public monitor_letreiro BuscaItem(monitor_letreiro pItem)
        {
            monitor_letreiro item = new monitor_letreiro();
            item = contextoEF.monitor_letreiro.FirstOrDefault();
            return item;
        }

        public monitor_video BuscaItem(monitor_video pItem)
        {
            monitor_video item = new monitor_video();
            item = contextoEF.monitor_video.Where(x => x.id_monitor_video == pItem.id_monitor_video).SingleOrDefault();
            return item;
        }

        public monitor CriarItem(monitor pItem)
        {
            contextoEF.monitor.Add(pItem);
            contextoEF.SaveChanges();
            pItem = contextoEF.monitor.Where(x => x.id_monitor == pItem.id_monitor).SingleOrDefault();
            return pItem;
        }

        public monitor_letreiro CriarItem(monitor_letreiro pItem)
        {
            contextoEF.monitor_letreiro.Add(pItem);
            contextoEF.SaveChanges();
            pItem = contextoEF.monitor_letreiro.Where(x => x.id_monitor_letreiro == pItem.id_monitor_letreiro).SingleOrDefault();
            return pItem;
        }

        public monitor_video CriarItem(monitor_video pItem)
        {
            contextoEF.monitor_video.Add(pItem);
            contextoEF.SaveChanges();
            pItem = contextoEF.monitor_video.Where(x => x.id_monitor_video == pItem.id_monitor_video).SingleOrDefault();
            return pItem;
        }

        public monitor AlterarStatus(monitor pItem)
        {
            monitor item = new monitor();
            item = contextoEF.monitor.Where(x => x.id_monitor == pItem.id_monitor).SingleOrDefault();
            item.status = "alterado";
            item.ativo = pItem.ativo;
            item.DataAlteracao = DateTime.Now;
            item.usuario = pItem.usuario;
            contextoEF.SaveChanges();
            return item;
        }

        public monitor_video AlterarStatus(monitor_video pItem)
        {
            monitor_video item = new monitor_video();
            item = contextoEF.monitor_video.Where(x => x.id_monitor_video == pItem.id_monitor_video).SingleOrDefault();
            item.status = "alterado";
            item.ativo = pItem.ativo;
            item.data_alteracao = DateTime.Now;
            item.usuario = pItem.usuario;
            contextoEF.SaveChanges();
            return item;
        }

        public Boolean AlterarItem(monitor pItem)
        {
            monitor item = new monitor();
            item = contextoEF.monitor.Where(x => x.id_monitor == pItem.id_monitor).SingleOrDefault();

            item.DescEventoMonitor = pItem.DescEventoMonitor;
            item.LocalEventoMonitor = pItem.LocalEventoMonitor;
            item.DataEventoMonitor = pItem.DataEventoMonitor;
            item.HorarioEventoMonitor = pItem.HorarioEventoMonitor;
            item.ResponsavelEventoMonitor = pItem.ResponsavelEventoMonitor;
            item.DataInicio = pItem.DataInicio;
            item.DataFim = pItem.DataFim;
            item.status = pItem.status;
            item.DataCadastro = pItem.DataCadastro;
            item.DataAlteracao = pItem.DataAlteracao;
            item.usuario = pItem.usuario;
            item.DataEventoInicio = pItem.DataEventoInicio;
            item.DataEventoFim = pItem.DataEventoFim;
            item.Coffee = pItem.Coffee;
            item.observacao = pItem.observacao;

            contextoEF.SaveChanges();
            return true;
        }

        public Boolean AlterarItem(monitor_letreiro pItem)
        {
            monitor_letreiro item = new monitor_letreiro();
            item = contextoEF.monitor_letreiro.Where(x => x.id_monitor_letreiro == pItem.id_monitor_letreiro).SingleOrDefault();

            item.descricao = pItem.descricao;
            item.data_cadastro = pItem.data_cadastro;
            item.data_alteracao = pItem.data_alteracao;
            item.usuario = pItem.usuario;
            contextoEF.SaveChanges();
            return true;
        }

        public Boolean AlterarItem(monitor_video pItem)
        {
            monitor_video item = new monitor_video();
            item = contextoEF.monitor_video.Where(x => x.id_monitor_video == pItem.id_monitor_video).SingleOrDefault();

            item.descricao = pItem.descricao;
            item.nome_arquivo = pItem.nome_arquivo;
            item.status = pItem.status;
            item.ordem = pItem.ordem;
            item.data_cadastro = pItem.data_cadastro;
            item.data_alteracao = pItem.data_alteracao;
            item.usuario = pItem.usuario;
            item.data_inicio = pItem.data_inicio;
            item.data_fim = pItem.data_fim;
            contextoEF.SaveChanges();
            return true;
        }

        public List<monitor> ListaItem_Ativos()
        {
            var c = contextoEF.monitor.AsQueryable();
            List<monitor> lista = new List<monitor>();

            DateTime qDataAgora = DateTime.Now;

            lista = c.Where(x => x.DataInicio <= qDataAgora && x.DataFim >= qDataAgora && x.ativo == 1).OrderBy(x => x.DataEventoInicio).ToList();

            return lista;
        }

        public List<monitor_video> ListaItem_Videos()
        {
            var c = contextoEF.monitor_video.AsQueryable();
            List<monitor_video> lista = new List<monitor_video>();

            DateTime qDataAgora = DateTime.Now;

            lista = c.Where(x => x.data_inicio <= qDataAgora && x.data_fim >= qDataAgora && x.ativo == 1).OrderByDescending(x => x.ordem).ToList();

            return lista;
        }

        public List<monitor> ListaItem(monitor pItem)
        {
            var c = contextoEF.monitor.AsQueryable();
            List<monitor> lista = new List<monitor>();

            DateTime qDataAgora = new DateTime();


            if (pItem.DescEventoMonitor != "" && pItem.DescEventoMonitor != null)
            {
                c = c.Where(x => x.DescEventoMonitor.Contains(pItem.DescEventoMonitor));
            }

            if (pItem.DataEventoInicio != qDataAgora && pItem.DataEventoInicio != null)
            {
                DateTime qDatAux = Convert.ToDateTime(String.Format("{0:dd/MM/yyyy}", pItem.DataEventoInicio) + " 23:59:59");
                c = c.Where(x => x.DataEventoInicio >= pItem.DataEventoInicio && x.DataEventoInicio <= qDatAux);
            }

            if (pItem.DataEventoFim != qDataAgora && pItem.DataEventoFim != null)
            {
                c = c.Where(x => x.DataEventoFim <= pItem.DataEventoFim);
            }

            if (pItem.Coffee != "")
            {
                if (pItem.Coffee == "sim")
                {
                    c = c.Where(x => x.Coffee != "" && x.Coffee != null);
                }
                else if (pItem.Coffee == "nao")
                {
                    c = c.Where(x => x.Coffee == "" || x.Coffee == null);
                }
            }

            if (pItem.ativo != null)
            {
                //if (pItem.ativo == 1)
                //{
                //    c = c.Where(x => x.ativo != "inativado");
                //}
                //else if (pItem.ativo == 0)
                //{
                    c = c.Where(x => x.ativo == pItem.ativo);
                //}
            }

            lista = c.ToList();

            return lista;
        }

        public List<monitor> ListaItemQuadro(monitor pItem)
        {
            var c = contextoEF.monitor.AsQueryable();
            List<monitor> lista = new List<monitor>();

            DateTime qDatafim = Convert.ToDateTime(String.Format("{0:dd/MM/yyyy}", pItem.DataEventoFim) + " 23:59:59");
                
            c = c.Where(x => x.DataEventoInicio >= pItem.DataEventoInicio && x.DataEventoInicio <= qDatafim);

            if (pItem.ativo != null)
            {
                //if (pItem.ativo == 1)
                //{
                //    c = c.Where(x => x.ativo != "inativado");
                //}
                //else if (pItem.ativo == 0)
                //{
                c = c.Where(x => x.ativo == pItem.ativo);
                //}
            }

            lista = c.ToList();

            return lista;
        }

        public List<monitor_video> ListaItem(monitor_video pItem)
        {
            var c = contextoEF.monitor_video.AsQueryable();
            List<monitor_video> lista = new List<monitor_video>();

            DateTime qDataAgora = new DateTime();

            if (pItem.descricao != "" && pItem.descricao != null)
            {
                c = c.Where(x => x.descricao.Contains(pItem.descricao));
            }

            if (pItem.data_cadastro != qDataAgora && pItem.data_cadastro != null)
            {
                c = c.Where(x => x.data_cadastro >= pItem.data_cadastro);
            }

            if (pItem.ativo != null)
            {
                c = c.Where(x => x.ativo == pItem.ativo);
            }

            lista = c.ToList();

            return lista;
        }

        public void Dispose()
        {
            contextoEF.Dispose();
        }
    }
}
