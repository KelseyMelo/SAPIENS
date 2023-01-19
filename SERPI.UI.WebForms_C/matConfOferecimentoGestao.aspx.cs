
using Aplicacao_C;
using SERPI.Dominio_C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class matConfOferecimentoGestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 55)) // 2. Confirmação de Oferecimento - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                pre_oferecimentos item = new pre_oferecimentos();
                MatriculaAplicacao aplicacaoMatricula = new MatriculaAplicacao();
                item = (pre_oferecimentos)Session["pre_oferecimentos"];

                txtPeriodoConfirmacaoMatricula.Value = item.periodo_matricula.quadrimestre + " (Início: " + String.Format("{0:dd/MM/yyyy}", item.periodo_matricula.data_inicio) + " - Término: " + String.Format("{0:dd/MM/yyyy}", item.periodo_matricula.data_termino) + ")";
                txtIdPreOferecimento.Value = item.id_pre_oferecimento.ToString();
                txtCodigoConfirmacaoMatricula.Value = item.disciplinas.codigo;
                txtNomeConfirmacaoMatricula.Value = item.disciplinas.nome;
            }

        }

        protected void btnCriarPeriodoMatricula_Click(object sender, EventArgs e)
        {
            Session["sNewPeriodoMatricula"] = true;
            Session["periodo_matricula"] = null;
            Session["AdiciondoSucesso"] = null;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("matConfOferecimento.aspx", true);
        }

    }
}