
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio_C;
using SERPI.Dominio_C;

namespace Aplicacao_C
{
    public class CadastroAutomaticoAlunoAplicacao
    {
        private readonly CadastroAutomaticoAlunoRepositorio Repositorio = new CadastroAutomaticoAlunoRepositorio();

        public alunos_cadastro_automatico BuscaItem(alunos_cadastro_automatico pItem)
        {
            return Repositorio.BuscaItem(pItem);
        }

        public alunos_cadastro_automatico CriarItem(alunos_cadastro_automatico pItem)
        {
            return Repositorio.CriarItem(pItem);
        }

        public Boolean AlterarItem(alunos_cadastro_automatico pItem)
        {
            return Repositorio.AlterarItem(pItem);
        }

        public List<alunos_cadastro_automatico> ListaItem(alunos_cadastro_automatico pItem)
        {
            return Repositorio.ListaItem(pItem);
        }

        public alunos_cadastro_automatico_det BuscaItem_Participante(alunos_cadastro_automatico_det pItem)
        {
            return Repositorio.BuscaItem_Participante(pItem);
        }

        public bool LimpaItem_Participante(alunos_cadastro_automatico_det pItem)
        {
            return Repositorio.LimpaItem_Participante(pItem);
        }

        public alunos_cadastro_automatico_det CriarItem_Participante(alunos_cadastro_automatico_det pItem)
        {
            return Repositorio.CriarItem_Participante(pItem);
        }

        public Boolean AlterarItem_participante(alunos_cadastro_automatico_det pItem)
        {
            return Repositorio.AlterarItem_participante(pItem);
        }

        public List<alunos_cadastro_automatico_det> ListaItem_participante(alunos_cadastro_automatico_det pItem)
        {
            return Repositorio.ListaItem_participante(pItem);
        }

    }
}
