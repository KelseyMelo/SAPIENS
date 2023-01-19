using System;
using SERPI.Dominio_C;
using System.Linq;
using System.Collections.Generic;

namespace Repositorio_C
{
    public class UsuarioRepositorio:IDisposable 
    {

        private Entities contextoEF;

        public UsuarioRepositorio()
        {
            contextoEF = new Entities();
            // the terrible hack
            var ensureDLLIsCopied =
                    System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public usuarios BuscaUsuario(usuarios pItem)
        {
            usuarios usuario = new usuarios();
            usuario = contextoEF.usuarios.Include("grupos_acesso").Include("grupos_acesso.grupos_acesso_telas_sistema").Where(x => x.usuario == pItem.usuario).SingleOrDefault();
            //usuario.grupos_acesso.grupos_acesso_telas_sistema = usuario.grupos_acesso.grupos_acesso_telas_sistema;
            return usuario;
        }

        public usuarios VerificaLoginUsado(usuarios pItem)
        {
            usuarios usuario = new usuarios();
            usuario = contextoEF.usuarios.Include("grupos_acesso").Include("grupos_acesso.grupos_acesso_telas_sistema").Where(x => x.usuario == pItem.usuario).SingleOrDefault();
            //usuario.grupos_acesso.grupos_acesso_telas_sistema = usuario.grupos_acesso.grupos_acesso_telas_sistema;
            return usuario;
        }

        public List<usuarios> ListaUsuario_porGrupoAcesso(usuarios pItem)
        {
            var c = contextoEF.usuarios.AsQueryable();
            List<usuarios> lista = new List<usuarios>();

            c = c.Where(x => x.grupos_acesso.id_grupo == pItem.grupos_acesso.id_grupo);

            lista = c.OrderBy(x => x.nome).ToList();
            return lista;
        }

        public usuarios BuscaUsuarioPorEmail(usuarios pItem)
        {
            usuarios usuario = new usuarios();
            usuario = contextoEF.usuarios.Include("grupos_acesso").Include("grupos_acesso.grupos_acesso_telas_sistema").Where(x => x.email == pItem.email).FirstOrDefault();
            //usuario.grupos_acesso.grupos_acesso_telas_sistema = usuario.grupos_acesso.grupos_acesso_telas_sistema;
            return usuario;
        }

        public Boolean AlterarSenhaUsuario(usuarios pItem)
        {
            //usuarios usuario = new usuarios();
            usuarios usuario = contextoEF.usuarios.Where(x => x.usuario == pItem.usuario).SingleOrDefault();
            usuario.senha = pItem.senha;
            contextoEF.SaveChanges();
            return true;
        }

        public Boolean  CriarUsuario(usuarios pItem)
        {
            using (var contextoEF = new Entities())
            {
                contextoEF.usuarios.Add(pItem);
                contextoEF.SaveChanges();
                return true;
            }
        }

        public Boolean AlterarUsuario(usuarios pItem)
        {
            usuarios usuario = contextoEF.usuarios.Where(x => x.usuario == pItem.usuario).SingleOrDefault();
            usuario.nome = pItem.nome;
            usuario.un = pItem.un;
            usuario.email = pItem.email;
            usuario.id_grupo_acesso = pItem.id_grupo_acesso;
            usuario.avatar = pItem.avatar;
            usuario.nomeSocial = pItem.nomeSocial;
            contextoEF.SaveChanges();
            return true;
        }

        public Boolean AlterarStatus(usuarios pItem)
        {
            usuarios usuario = contextoEF.usuarios.Where(x => x.usuario == pItem.usuario).SingleOrDefault();
            usuario.status = pItem.status;
            contextoEF.SaveChanges();
            return true;
        }

        public Boolean setusuarioTema(usuarios pItem)
        {
            usuarios usuario = contextoEF.usuarios.Where(x => x.usuario == pItem.usuario).SingleOrDefault();
            usuario.TemaSistema = pItem.TemaSistema;
            contextoEF.SaveChanges();
            return true;
        }

        public List<usuarios> ListaUsuario(usuarios pItem) 
        {
            var c = contextoEF.usuarios.AsQueryable();
            List<usuarios> lista = new List<usuarios>();

            if (pItem.usuario != "" && pItem.usuario != null) {
                c = c.Where(x => x.usuario.Contains(pItem.usuario));
            }

            if (pItem.nome != "" && pItem.nome != null)
            {
                c = c.Where(x => x.nome.Contains(pItem.nome));
            }

            if (pItem.un != "" && pItem.un != null)
            {
                c = c.Where(x => x.un.Contains(pItem.un));
            }

            if (pItem.email != "" && pItem.email != null)
            {
                c = c.Where(x => x.nome.Contains(pItem.email));
            }

            if (pItem.id_grupo_acesso != null)
            {
                c = c.Where(x => x.id_grupo_acesso == pItem.id_grupo_acesso);
            }

            if (pItem.status != null)
            {
                c = c.Where(x => x.status == pItem.status);
            }

            lista = c.OrderBy(x => x.nome).ToList();

            return lista;
        }

        public usuarios_log CriarUsuario_Log(usuarios_log pItem)
        {
            contextoEF.usuarios_log.Add(pItem);
            contextoEF.SaveChanges();
            return pItem;
        }

        public usuarios_log AlteraUsuario_Log(usuarios_log pItem)
        {
            usuarios_log item = contextoEF.usuarios_log.Where(x => x.id_usuarios_log == pItem.id_usuarios_log).SingleOrDefault();
            item.qtd_bloqueio_parcial = pItem.qtd_bloqueio_parcial;
            item.qtd_bloqueio_total = pItem.qtd_bloqueio_total;
            item.status_bloqueio_total = pItem.status_bloqueio_total;
            item.data_bloqueio_parcial = pItem.data_bloqueio_parcial;
            item.data_bloqueio_total = pItem.data_bloqueio_total;
            item.data_ultimo_acesso = pItem.data_ultimo_acesso;
            contextoEF.SaveChanges();
            return item;
        }

        public void Dispose()
        {
            contextoEF.Dispose();
        }

    }
}
