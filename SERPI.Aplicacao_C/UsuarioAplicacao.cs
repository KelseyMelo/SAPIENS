using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class UsuarioAplicacao
    {
        private readonly UsuarioRepositorio Repositorio = new UsuarioRepositorio();

        public usuarios BuscaUsuario(usuarios pItem)
        {
            return Repositorio.BuscaUsuario(pItem);
        }

        public usuarios VerificaLoginUsado(usuarios pItem)
        {
            return Repositorio.VerificaLoginUsado(pItem);
        }

        public List<usuarios> ListaUsuario_porGrupoAcesso(usuarios pItem)
        {
            return Repositorio.ListaUsuario_porGrupoAcesso(pItem);
        }

        public usuarios BuscaUsuarioPorEmail(usuarios pItem)
        {
            return Repositorio.BuscaUsuarioPorEmail(pItem);
        }

        public Boolean AlterarSenhaUsuario(usuarios pItem)
        {
            return Repositorio.AlterarSenhaUsuario(pItem);
        }

        public Boolean CriarUsuario(usuarios pItem)
        {
            return Repositorio.CriarUsuario(pItem);
        }

        public Boolean AlterarUsuario(usuarios pItem)
        {
            return Repositorio.AlterarUsuario(pItem);
        }

        public Boolean AlterarStatus(usuarios pItem)
        {
            return Repositorio.AlterarStatus(pItem);
        }


        public Boolean setusuarioTema(usuarios pItem)
        {
            return Repositorio.setusuarioTema(pItem);
        }

        public List<usuarios> ListaUsuario(usuarios pItem)
        {
            return Repositorio.ListaUsuario(pItem);
        }

        public usuarios_log CriarUsuario_Log(usuarios_log pItem)
        {
            return Repositorio.CriarUsuario_Log(pItem);
        }

        public usuarios_log AlteraUsuario_Log(usuarios_log pItem)
        {
            return Repositorio.AlteraUsuario_Log(pItem);
        }

    }
}
