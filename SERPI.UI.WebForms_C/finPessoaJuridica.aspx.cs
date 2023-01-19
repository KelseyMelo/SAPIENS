
using System;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class finPessoaJuridica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarios usuario;
            usuario = (usuarios)Session["UsuarioLogado"];

            if (usuario == null) //verifica se o usuário está logado, caso não redireciona para a página de Login
            {
                Response.Redirect("index.html", true);
            }

            if (!usuario.grupos_acesso.grupos_acesso_telas_sistema.Any(x => x.id_tela == 7)) // 2. Pessoa Jurídica - Verifica se o usuário tem acesso à essa página, caso não redireciona para a tela Principal.
            {
                Response.Redirect("Principal.aspx", true);
            }

            if (!Page.IsPostBack)
            {

                if (Session["arrayFiltroFornecedor"] != null)
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
            string[] arrayFiltroFornecedor = new string[2];

            fornecedores item = new fornecedores();

            arrayFiltroFornecedor = (string[])Session["arrayFiltroFornecedor"];

            if (arrayFiltroFornecedor[0] != "" && arrayFiltroFornecedor[0] != null)
            {
                item.nome = arrayFiltroFornecedor[0];
                txtNomeFornecedor.Value = arrayFiltroFornecedor[0];
            }

            if (arrayFiltroFornecedor[1] != "" && arrayFiltroFornecedor[1] != null)
            {
                item.cnpj = arrayFiltroFornecedor[1];
                txtCNPJFornecedor.Value = arrayFiltroFornecedor[1];
            }

            //Session["arrayFiltroFornecedor"] = arrayFiltroFornecedor;
            FornecedorAplicacao aplicacaoFornecedor = new FornecedorAplicacao();
            List<fornecedores> listaFornecedor = new List<fornecedores>();
            listaFornecedor = aplicacaoFornecedor.ListaItem(item);
            grdResultado.DataSource = listaFornecedor;
            grdResultado.DataBind();

            if (listaFornecedor.Count > 0)
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

        protected void btnPerquisaFornecedor_Click(object sender, EventArgs e)
        {
            string[] arrayFiltroFornecedor = new string[2];

            if (txtNomeFornecedor.Value.Trim() != "")
            {
                arrayFiltroFornecedor[0] = txtNomeFornecedor.Value.Trim();
            }

            if (txtCNPJFornecedor.Value.Trim() != "")
            {
                arrayFiltroFornecedor[1] = txtCNPJFornecedor.Value.Trim();
            }
            
            Session["arrayFiltroFornecedor"] = arrayFiltroFornecedor;

            CarregarDados();
        }

        //protected void grdResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Page")
        //    {
        //        return;
        //    }

        //    int linha = Convert.ToInt32(e.CommandArgument);
        //    int codigo = Convert.ToInt32(grdResultado.DataKeys[linha].Values[0]);
        //    Fornecedors_concentracao item = new Fornecedors_concentracao();
        //    item.id_Fornecedor_concentracao = codigo;
        //    switch (grdResultado.DataKeys[linha].Values[1].ToString())
        //    {
        //        case "Editar":
        //            FornecedorAplicacao aplicacaoFornecedor = new FornecedorAplicacao();
        //            item = aplicacaoFornecedor.BuscaItem(item);
        //            Session.Add("Fornecedors_concentracao", item);
        //            Session.Add("sNewFornecedores", false);
        //            Response.Redirect("cadFornecedorConcentracaoGestao.aspx", true);
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
                fornecedores item = new fornecedores();
                FornecedorAplicacao aplicacaoFornecedor = new FornecedorAplicacao();
                //item.Fornecedor = grdResultado.DataKeys[linha].Values[0].ToString();
                item = aplicacaoFornecedor.BuscaItem(Convert.ToInt32(grdResultado.DataKeys[linha].Values[0]));
                Session["fornecedores"] = item;
                Session["sNewFornecedores"] = false;
                Response.Redirect("finPessoaJuridicaGestao.aspx", true);
            }
        }

        protected void btnCriarFornecedor_Click(object sender, EventArgs e)
        {
            Session["sNewFornecedores"] = true;
            Session["fornecedores"] = null;
            Response.Redirect("finPessoaJuridicaGestao.aspx", true);
        }
    }
}