using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections;

namespace SERPI.UI.WebForms_C
{
    public partial class boletoBB : System.Web.UI.Page
    {
        string _tpPagamento = "";
        string _urlRetorno = "www.fipt.org.br";
        string _urlInforma = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            boletos boleto;
            boleto = (boletos)Session["boletos"];

            if (boleto != null)
            {

                string sAux = "";
                sAux = "<input type=\"hidden\" name=\"idConv\" value=\"" + boleto.id_conv + "\" />";
                sAux = sAux + "<input type=\"hidden\" name=\"refTran\" value=\"" + boleto.refTran + "\" />";
                sAux = sAux + "<input type=\"hidden\" name=\"valor\" value=\"" + boleto.valor + "\" />";
                //sAux = sAux + "<input type=\"hidden\" name=\"valor\" value=\"100\" />";
                sAux = sAux + "<input type=\"hidden\" name=\"qtdPontos\" value=\"0\" />";
                sAux = sAux + "<input type=\"hidden\" name=\"dtVenc\" value=\"" + String.Format("{0:ddMMyyyy}", boleto.data_vencimento) + "\" />";
                if (boleto.status != "Emitido")
                {
                    sAux = sAux + "<input type=\"hidden\" name=\"tpPagamento\" value=\"2\" />";
                    InscricaoAplicacao aplicacaoInscricao = new InscricaoAplicacao();
                    boleto.status = "Emitido";
                    boleto = aplicacaoInscricao.AlterarBoleto(boleto);
                }
                else
                {
                    sAux = sAux + "<input type=\"hidden\" name=\"tpPagamento\" value=\"21\" />";
                }
                sAux = sAux + "<input type=\"hidden\" name=\"cpfCnpj\" value=\"" + boleto.cpf + "\" />";
                sAux = sAux + "<input type=\"hidden\" name=\"indicadorPessoa\" value=\"1\" />";
                sAux = sAux + "<input type=\"hidden\" name=\"valorDesconto\" value=\"0\" />";
                sAux = sAux + "<input type=\"hidden\" name=\"dataLimiteDesconto\" value=\"\" />";
                sAux = sAux + "<input type=\"hidden\" name=\"tpDuplicata\" value=\"DS\" />";
                sAux = sAux + "<input type=\"hidden\" name=\"urlRetorno\" value=\"www.fipt.org.br\" />";
                sAux = sAux + "<input type=\"hidden\" name=\"urlInforma\" value=\"\" />";
                sAux = sAux + "<input type=\"hidden\" name=\"nome\" value=\"" + boleto.nome + "\" />";
                sAux = sAux + "<input type=\"hidden\" name=\"endereco\" value=\"" + boleto.endereco + "\" />";
                sAux = sAux + "<input type=\"hidden\" name=\"cidade\" value=\"" + boleto.cidade + "\" />";
                sAux = sAux + "<input type=\"hidden\" name=\"uf\" value=\"" + boleto.uf + "\" />";
                sAux = sAux + "<input type=\"hidden\" name=\"cep\" value=\"" + boleto.cep + "\" />";
                sAux = sAux + "<input type=\"hidden\" name=\"msgLoja\" value=\"" + boleto.msgLoja + "\" />";

                litInputs.Text = Server.HtmlDecode(sAux);

                Session["boletos"] = null;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ClientSideScript", "<script>window.close();</script>", false);
            }

        }
    }
}