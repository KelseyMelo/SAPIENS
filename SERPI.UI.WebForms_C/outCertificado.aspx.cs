using System;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class outCertificado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 75)) // 75. Outros Certificados - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                if (Session["arrayFiltroCertificado"] != null)
                {
                    CarregarDados();
                }
            }
            else
            {
                if (grdResultado.Rows.Count != 0)
                {
                    grdResultado.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
        }

        private void CarregarDados()
        {
            string[] arrayFiltroCertificado = new string[1];

            certificados item = new certificados();
            item.evento = "";

            arrayFiltroCertificado = (string[])Session["arrayFiltroCertificado"];

            if (arrayFiltroCertificado[0] != "" && arrayFiltroCertificado[0] != null)
            {
                item.evento = arrayFiltroCertificado[0];
                txtNomeCertificado.Value = arrayFiltroCertificado[0];
            }

            //Session["arrayFiltroGrupo"] = arrayFiltroGrupo;
            CertificadoAplicacao aplicacaoCertificado = new CertificadoAplicacao();
            List<certificados> lista = new List<certificados>();
            lista = aplicacaoCertificado.ListaItem(item);
            grdResultado.DataSource = lista;
            grdResultado.DataBind();

            if (lista.Count > 0)
            {
                grdResultado.UseAccessibleHeader = true;
                grdResultado.HeaderRow.TableSection = TableRowSection.TableHeader;
                msgSemResultados.Visible = false;
                grdResultado.Visible = true;
            }
            else
            {
                msgSemResultados.Visible = true;
            }
            divResultados.Visible = true;
        }

        protected void btnPerquisaCertificado_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroCertificado = new string[1];

            if (txtNomeCertificado.Value.Trim() != "")
            {
                arrayFiltroCertificado[0] = txtNomeCertificado.Value.Trim();
            }

            Session["arrayFiltroCertificado"] = arrayFiltroCertificado;

            CarregarDados();
        }

        public string setTipoCurso(object pItem, string pInfo)
        {
            certificado_tipo_curso item = (certificado_tipo_curso)pItem;

            string sAux;
            sAux = "";
            if (item != null)
            {
                if (item.id_certificado_tipo_curso != 2)
                {
                    sAux = item.descricao;
                }
                else
                {
                    sAux = item.descricao + "<hr>" + pInfo;
                }
            }

            return sAux;
        }

        public string setParticipantes(object objeto)
        {
            HashSet<certificados_participantes> lista = (HashSet<certificados_participantes>)objeto;
            string sAux;
            sAux = "";

            //if (lista.Count == 0)
            //{
            //    sAux = lista.Count.ToString();
            //}

            sAux = lista.Count.ToString();
            return sAux;
        }

        //protected void grdResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Page")
        //    {
        //        return;
        //    }

        //    int linha = Convert.ToInt32(e.CommandArgument);
        //    int codigo = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0]);
        //    usuarios_concentracao item = new usuarios_concentracao();
        //    item.id_Quadrimestre_concentracao = codigo;
        //    switch (grdResultado.DataKeys[linha].Values[1].ToString())
        //    {
        //        case "Editar":
        //            QuadrimestreAplicacao aplicacaoQuadrimestre = new QuadrimestreAplicacao();
        //            item = aplicacaoQuadrimestre.BuscaItem(item);
        //            Session.Add("usuarios_concentracao", item);
        //            Session.Add("sNewQuadrimestre", false);
        //            Response.Redirect("cadQuadrimestreConcentracaoGestao.aspx", true);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        public void grdResultado_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "StartService")
            {
                int linha = Convert.ToInt32(e.CommandArgument);
                //int codigo = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0]);
                certificados item = new certificados();
                CertificadoAplicacao aplicacao = new CertificadoAplicacao();
                item.id_certificado = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0]);
                item = aplicacao.BuscaItem(item);
                Session["certificados"] = item;
                Session["sNewcertificados"] = false;
                Response.Redirect("outCertificadoGestao.aspx", true);
            }
        }

        protected void btnCriarCertificado_Click(object sender, EventArgs e)
        {
            Session["sNewcertificados"] = true;
            Session["certificados"] = null;
            Response.Redirect("outCertificadoGestao.aspx", true);
        }
    }
}