
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SERPI.Dominio_C;
using Aplicacao_C;
using System.Collections;
using System.Text.RegularExpressions;

namespace SERPI.UI.WebForms_C
{
    public partial class BoletoBB_print : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            geral_Boleto item;
            item = (geral_Boleto)Session["geral_Boleto"];
            string qLinha = "";
            string qDV;

            string qCodigoBanco = "001";
            string qCodigo_banco_com_dv = Utilizades.GeraCodigoBancoComDigito(item.NumeroBanco);
            string qNummoeda = "9";
            string qFator_vencimento = Utilizades.fator_vencimento(Convert.ToDateTime(item.DATAVENCIMENTO).ToString("dd/MM/yyyy")).ToString();

            if (qFator_vencimento.Length < 4)
            {
                int qLoop = qFator_vencimento.Length;
                for (int j = qLoop; j < 4; j++)
                {
                    qFator_vencimento = qFator_vencimento + "0";
                }
            }

            qFator_vencimento = Utilizades.FormataNumero(qFator_vencimento, "4", "0");
            //valor tem 10 digitos, sem virgula
            string qValor = string.Format("{0:C}", Convert.ToDecimal(item.ValorReceberPagar));

            qValor = qValor.Replace("R$", "").Replace(",", "").Replace(".", "").Trim();

            qValor = Utilizades.FormataNumero(qValor, "10", "0", "valor");

            //agencia é sempre 4 digitos
            string qAgencia = Utilizades.FormataNumero(item.NumeroAgencia, "4", "0");
            //conta é sempre 8 digitos
            string qConta = Utilizades.FormataNumero(item.NumeroConta, "8", "0");
            //carteira 18
            string qCarteira = item.Carteira;
            //agencia e conta
            //string qAgencia_codigo = item.NumeroAgencia + "-" + CalcularDigitoModulo11(item.NumeroAgencia) + " / " + item.NumeroConta + "-" + CalcularDigitoModulo11(item.NumeroConta);
            //Zeros: usado quando convenio de 7 digitos
            string qLivre_zeros = "000000";

            string qConvenio = "";
            string qNossoNumero = "";

            string qLinhaDigitavel = "";

            //1º Campo -Composto por: código do banco(posições 1 a 3 do código de barras), código da moeda(posição 4 do código de barras), 
            //as cinco primeiras posições do campo livre(posições 20 a 24 do código de barras) e digito verificador deste campo;

            //2º Campo -Composto pelas posições 6 a 15 do campo livre(posições 25 a 34 do código de barras) e digito verificador deste campo;

            //3º Campo -Composto pelas posições 16 a 25 do campo livre(posições 35 a 44 do código de barras) e digito verificador deste campo;

            //4º Campo -Dígito verificador geral do código de barras(posição 5 do código de barras);

            //5º Campo -Composto pelo "fator de vencimento"(posições 6 a 9 do código de barras) e pelo valor nominal do documento(posições 10 a 19 do código de barras), com a inclusão de zeros entre eles até compor as 14 posições do campo e sem edição(sem ponto e sem vírgula).Não obstante existam 10 posições, o valor nominal do documento não poderá exceder R$ 9.999.999,99.

            //qLinha = qCodigoBanco + qNummoeda + qDV + qFator_vencimento + qValor + qLivre_zeros + qConvenio + qNossoNumero + qCarteira;


            // Carteira 18 com Convênio de 8 dígitos
            if (item.NumeroConvenio.Length == 8)
            {
                qConvenio = Utilizades.FormataNumero(item.NumeroConvenio, "8", "0", "convenio");
                // Nosso número de até 9 dígitos
                qNossoNumero = Utilizades.FormataNumero(item.NossoNumero, "9", "0");
                qDV = Utilizades.CalcularDigitoModulo11(qCodigoBanco + qNummoeda + qFator_vencimento + qValor + qLivre_zeros + qConvenio + qNossoNumero + qCarteira).ToString(); //modulo_11("$codigobanco$nummoeda$fator_vencimento$valor$livre_zeros$convenio$nossonumero$carteira");
                qLinha = qCodigoBanco + qNummoeda + qDV + qFator_vencimento + qValor + qLivre_zeros + qConvenio + qNossoNumero + qCarteira; //"$codigobanco$nummoeda$dv$fator_vencimento$valor$livre_zeros$convenio$nossonumero$carteira";
                                                                                                                                            //$nossonumero = $convenio. $nossonumero."-".modulo_11($convenio.$nossonumero);
                qLinhaDigitavel = Utilizades.MontaLinhaDigitavel(qLinha);
            }

            // Carteira 18 com Convênio de 7 dígitos
            if (item.NumeroConvenio.Length == 7)
            {
                qConvenio = Utilizades.FormataNumero(item.NumeroConvenio, "7", "0", "convenio");
                // Nosso número de até 9 dígitos
                qNossoNumero = Utilizades.FormataNumero(item.NossoNumero, "10", "0");
                qDV = Utilizades.CalcularDigitoModulo11(qCodigoBanco + qNummoeda + qFator_vencimento + qValor + qLivre_zeros + qNossoNumero + qCarteira).ToString(); //modulo_11("$codigobanco$nummoeda$fator_vencimento$valor$livre_zeros$convenio$nossonumero$carteira");
                qLinha = qCodigoBanco + qNummoeda + qDV + qFator_vencimento + qValor + qLivre_zeros + qNossoNumero + qCarteira; //"$codigobanco$nummoeda$dv$fator_vencimento$valor$livre_zeros$convenio$nossonumero$carteira";

                qLinhaDigitavel = Utilizades.MontaLinhaDigitavel(qLinha);

                //Não existe DV na composição do nosso-número para convênios de sete posições//montando o nosso numero que aparecerá no boleto
            }



            // Carteira 18 com Convênio de 6 dígitos
  //          if (item.NumeroConvenio == "6")
  //          {
  //              qConvenio = FormataNumero(item.NumeroConvenio, "6", "0", "convenio");

  //              if ($dadosboleto["formatacao_nosso_numero"] == "1") {

		//// Nosso número de até 5 dígitos
		//$nossonumero = formata_numero($dadosboleto["nosso_numero"], 5, 0);
		//$dv = modulo_11("$codigobanco$nummoeda$fator_vencimento$valor$convenio$nossonumero$agencia$conta$carteira");
		//$linha = "$codigobanco$nummoeda$dv$fator_vencimento$valor$convenio$nossonumero$agencia$conta$carteira";
		////montando o nosso numero que aparecerá no boleto
		//$nossonumero = $convenio. $nossonumero."-".modulo_11($convenio.$nossonumero);
  //              }

  //              if ($dadosboleto["formatacao_nosso_numero"] == "2") {

		//// Nosso número de até 17 dígitos
		//$nservico = "21";
		//$nossonumero = formata_numero($dadosboleto["nosso_numero"], 17, 0);
		//$dv = modulo_11("$codigobanco$nummoeda$fator_vencimento$valor$convenio$nossonumero$nservico");
		//$linha = "$codigobanco$nummoeda$dv$fator_vencimento$valor$convenio$nossonumero$nservico";
  //              }
  //          }



            //=== parte 1 - COMEÇO =======

            lbl_codigo_banco_com_dv.InnerText = qCodigo_banco_com_dv; // "001-9"; // não
            lbl_linha_digitavel.InnerText = qLinhaDigitavel; // "Linha Digitável (não tem informação)"; // não

            lbl_cedente_1.InnerText = "FUNDAÇÃO DE APOIO AO INSTITUTO DE PESQUISASTECNOLÓGICAS";// (não tem informação)"; // não
            lbl_agencia_codigo_1.InnerText = qAgencia + " / " + qConta; // "1897-X / 78604-0";
            lbl_especie_1.InnerText = "R$ "; // "R$ (não tem informação)"; // não
            lbl_quantidade_1.InnerText = ""; // "(não tem informação)"; // não
            lbl_nosso_numero_1.InnerText = qNossoNumero; // "28284191000027029";

            lbl_numero_documento_1.InnerText = item.DocumentoReceberPagar; // "-19/25";
            lbl_contrato_1.InnerText = ""; // "(não tem informação)"; // não
            lbl_cpf_cnpj_1.InnerText = item.CPFCNPJ; // "358.923.618-37";
            lbl_data_vencimento_1.InnerText = Convert.ToDateTime(item.DATAVENCIMENTO).ToString("dd/MM/yyyy"); // "17/10/2022";
            lvl_valor_boleto_1.InnerText = string.Format("{0:C}", Convert.ToDecimal(item.ValorReceberPagar)).Replace("R$", "").Trim(); // "1.200,00";

            lbl_sacado_1.InnerText = item.NomePessoaFisicaJuridica; // "ACHILES DANIEL ALBERTI";

            //=== parte 1 - FIM =======

            //=================

            //=== parte 2 - COMEÇO =======
            lbl_codigo_banco_com_dv_2.InnerText = Utilizades.GeraCodigoBancoComDigito(item.NumeroBanco); // "001-9"; // não
            lbl_linha_digitavel_2.InnerText = qLinhaDigitavel; // "Linha Digitável (não tem informação)"; // não

            lbl_data_vencimento_2.InnerText = Convert.ToDateTime(item.DATAVENCIMENTO).ToString("dd/MM/yyyy"); // "17/10/2022";

            lbl_cedente_2.InnerText = "FUNDAÇÃO DE APOIO AO INSTITUTO DE PESQUISASTECNOLÓGICAS"; // não
            lbl_agencia_codigo_2.InnerText = qAgencia + " / " + qConta; // "1897-X / 78604-0";

            lbl_data_documento_2.InnerText = DateTime.Today.ToString("dd/MM/yyyy"); // "17/10/2022"; "(não tem informação)"; // não
            lbl_numero_documento_2.InnerText = item.DocumentoReceberPagar; // "-19/25";
            lbl_especie_doc_2.InnerText = "DM "; // não
            lbl_aceite_2.InnerText = "N"; // não
            lbl_data_processamento_2.InnerText = Convert.ToDateTime(item.DATAVENCIMENTO).ToString("dd/MM/yyyy");  //"(não tem informação)"; // não
            lbl_nosso_numero_2.InnerText = qNossoNumero; // "28284191000027029";

            lbl_carteira_2.InnerText = qCarteira; // "17 -019";
            lbl_especie_2.InnerText = "R$ "; // "R$ (não tem informação)"; // não
            lbl_quantidade_2.InnerText = ""; // "(N I)"; // não
            lbl_valor_unitario_2.InnerText = ""; //  "(N I)"; // não
            lbl_valor_boleto_2.InnerText = string.Format("{0:C}", Convert.ToDecimal(item.ValorReceberPagar)).Replace("R$", "").Trim(); // "1.200,00";

            lbl_demonstrativo1_1.InnerText = item.Instrucoes + " E-MAIL: financeiro @fipt.org.br";

            lbl_Sacado_2.InnerText = item.NomePessoaFisicaJuridica; //"ACHILES DANIEL ALBERTI";
            lbl_Endereco.InnerText = item.Endereco + " " + item.Numero; //"Rua Tamandare, 655, apto 141 Aclimação";
            lbl_Cidade_Capital.InnerText = item.Cidade + "/" + item.Estado; // "São Paulo/SP - C,";
            lbl_Cep_Cidade_Capital.InnerText = item.CEP + " " + item.Bairro; // "01525-000 - SÃO PAULO / SP";

            //=== parte 2 - FIM =======

            litCodigoBarra.Text = Server.HtmlDecode(Utilizades.CodigoBarra(qLinha));

        }

        // passado para Utilizaddes
        // passado para Utilizaddes - RetornaLinhaCodigoBarra===
        //protected string RetornaLinhaCodigoBarra(string qLinha)
        //{
        //    // remove caracteres diferente de dígitos
        //    string barra = Regex.Replace(qLinha, "[^0-9]", "");

        //    // completa com zeros à direita da string
        //    if (barra.Length < 47)
        //    {
        //        barra = string.Concat(barra, new string('0', 47 - barra.Length));
        //    }

        //    barra = barra.Substring(0, 4) + barra.Substring(32, 15) + barra.Substring(4, 5) + barra.Substring(10, 10) + barra.Substring(21, 10);


        //    // calcula dígito verificador
        //    int digito = Utilizades.CalcularDigitoModulo11(barra.Substring(0, 4) + barra.Substring(5, 39));

        //    // verifica dígito
        //    if (digito != Convert.ToDouble(barra.Substring(4, 1)))
        //    {
        //        throw new ArgumentException(string.Format("Dígito verificador '{0}', o correto é '{1}'.", digito, barra.Substring(4, 1)));
        //    }

        //    return barra;
        //}
        // passado para Utilizaddes - RetornaLinhaCodigoBarra===

        // passado para Utilizaddes - CalcularDigitoModulo11===
        //public int CalcularDigitoModulo11(string numero)
        //{
        //    int soma = 0;
        //    int peso = 2;
        //    int digito;

        //    for (int I = numero.Length - 1; I >= 0; I -= 1)
        //    {
        //        soma = (int)Math.Round(soma + Convert.ToDouble(numero.Substring(I, 1)) * peso);

        //        if (peso < 9)
        //        {
        //            peso = peso + 1;
        //        }
        //        else
        //        {
        //            peso = 2;
        //        }
        //    }

        //    digito = 11 - soma % 11;

        //    if (digito > 9)
        //    {
        //        digito = 0;
        //    }

        //    if (digito == 0)
        //    {
        //        digito = 1;
        //    }
        //    return digito;
        //}
        // passado para Utilizaddes - CalcularDigitoModulo11===


        // passado para Utilizaddes - GeraCodigoBancoComDigito===
        //protected string GeraCodigoBancoComDigito (string qNumero)
        //{
        //    string qParte1 = qNumero.Substring(0, 3);
        //    string qParte2 = CalcularDigitoModulo11(qParte1).ToString();
        //    return qParte1 + "-" + qParte2;
        //}
        // passado para Utilizaddes ===

        // passado para Utilizaddes - FormataNumero===
    //    protected string FormataNumero (string qNumero, string qLoop, string qInsert, string qTipo = "geral" )
    //    {
           
    //        if (qTipo == "geral") 
    //        {
    //            qNumero = qNumero.Replace(",", "");
    //            while (qNumero.Length < Convert.ToInt32(qLoop))
    //            {
    //                qNumero = (qInsert + qNumero).ToString();
    //            }
    //        }
    //        if (qTipo == "valor") {
    //            /*
    //            retira as virgulas
    //            formata o numero
    //            preenche com zeros
    //            */
    //            qNumero = qNumero.Replace(",", "");
    //            while (qNumero.Length < Convert.ToInt32(qLoop))
    //            {
    //                qNumero = (qInsert +qNumero).ToString();
    //            }
    //        }
    //        if (qTipo == "convenio") {
    //            while (qNumero.Length < Convert.ToInt32(qLoop))
    //            {
    //                qNumero = (qInsert + qNumero).ToString();
    //            }
    //        }
    //        return qNumero;
    //}
        // passado para Utilizaddes - FormataNumero===

        // passado para Utilizaddes - fator_vencimento===
        //protected double fator_vencimento(string qData)
        //{
        //    var qSplit = qData.Split('/'); 

	       // string qAno  = qSplit[2];
        //    string qMes = qSplit[1];
        //    string qDia = qSplit[0];
        //    return Math.Round(Math.Abs(DateToDays(Convert.ToInt32("1997"), Convert.ToInt32("10"), Convert.ToInt32("07")) - DateToDays(Convert.ToInt32(qAno), Convert.ToInt32(qMes), Convert.ToInt32(qDia))));
        //}
        // passado para Utilizaddes - fator_vencimento===

        // passado para Utilizaddes - modulo_10===
        //public int modulo_10(string num)
        //{
        //    num = num.Replace(".", "");
        //    //          int digito=0;
        //    int mult = 2;
        //    int sum = 0;

        //    for (int i = (num.Length - 1); i >= 0; i--)
        //    {
        //        char c = num[i];

        //        int res = Convert.ToInt32(c.ToString()) * mult;
        //        sum += res > 9 ? (res - 10) + 1 : res;
        //        mult = mult == 2 ? 1 : 2;
        //    }

        //    int ret;
        //    if (sum == 10 || 10 - (sum % 10) == 10)
        //    {
        //        ret = 0;
        //    }
        //    else
        //    {
        //        ret = 10 - (sum % 10);
        //    }

        //    return ret;
        //}
        // passado para Utilizaddes - modulo_10===

        // passado para Utilizaddes - DateToDays===
        //protected double DateToDays (int year, int month, int day)
        //{
        //    string century = year.ToString().Substring(0, 2);
        //    year = Convert.ToInt32(year.ToString().Substring(2, 2));
        //    if (month > 2)
        //    {
        //        month -= 3;
        //    }
        //    else
        //    {
        //        month += 9;
        //        if (year == 0)
        //        {
        //        }
        //        else
        //        {
        //            year = 99;
        //            century = (Convert.ToInt32(century) - 1).ToString();
        //        }
        //    }
        //    double a = 146097d * Convert.ToDouble(century) / 4d;
        //    double b = 1461 * year / 4d;
        //    double c = (153 * month + 2) / 5d;
        //    int d = day + 1721119;
        //    return (a + b + c + d);
        //}
        // passado para Utilizaddes - DateToDays===

        // passado para Utilizaddes - MontaLinhaDigitavel===
        //protected string MontaLinhaDigitavel (string qLinha)
        //{
        //    String linha = qLinha.Replace("[^0-9]", "");

        //    if (linha.Length != 44)
        //    {
        //        return null; // 'A linha do Código de Barras está incompleta!'
        //    }

        //    String campo1 = linha.Substring(0, 4) + linha.Substring(19, 1) + '.' + linha.Substring(20, 4);
        //    String campo2 = linha.Substring(24, 5) + '.' + linha.Substring(29, 5);
        //    String campo3 = linha.Substring(34, 5) + '.' + linha.Substring(39, 5);
        //    String campo4 = linha.Substring(4, 1); // Digito verificador
        //    String campo5 = linha.Substring(5, 14); // Vencimento + Valor

        //    if (Utilizades.CalcularDigitoModulo11(linha.Substring(0, 4) + linha.Substring(5, 39)) != Convert.ToInt32(campo4))
        //    {
        //        return null; //'Digito verificador '+campo4+', o correto é '+modulo11_banco(  linha.substr(0,4)+linha.substr(5,99)  )+'\nO sistema não altera automaticamente o dígito correto na quinta casa!'
        //    }
        //    string rRet = campo1 + modulo_10(campo1)
        //            + ' '
        //            + campo2 + modulo_10(campo2)
        //            + ' '
        //            + campo3 + modulo_10(campo3)
        //            + ' '
        //            + campo4
        //            + ' '
        //            + campo5
        //            ;
        //    return rRet;

        //    //// Posição 	Conteúdo
        //    //// 1 a 3    Número do banco
        //    //// 4        Código da Moeda - 9 para Real
        //    //// 5        Digito verificador do Código de Barras
        //    //// 6 a 19   Valor (12 inteiros e 2 decimais)
        //    //// 20 a 44  Campo Livre definido por cada banco

        //    //// 1. Campo - composto pelo código do banco, código da moéda, as cinco primeiras posições
        //    //// do campo livre e DV (modulo10) deste campo
        //    //string qP1 = qLinha.Substring(0, 4); 
        //    //string qP2 = qLinha.Substring(5, 19);
        //    //string qP3 = modulo_10(Convert.ToDouble(qP1 + qP2)).ToString();
        //    //string qP4 = qP1 + qP2 +qP3;
        //    //string qP5 = qP4.Substring(0, 5); 
        //    //string qP6 = qP4.Substring(5);
        //    //string qCampo1 = qP5 + qP6;

        //    //// 2. Campo - composto pelas posiçoes 6 a 15 do campo livre
        //    //// e livre e DV (modulo10) deste campo
        //    //qP1 = qLinha.Substring(24, 10);
        //    //qP2 = modulo_10(Convert.ToInt64(qP1)).ToString();
        //    //qP3 = qP1 + qP2;
        //    //qP4 = qP3.Substring(0, 5);
        //    //qP5 = qP3.Substring(5);
        //    //string qCampo2 = qP4 + qP5;


        //    //// 3. Campo composto pelas posicoes 16 a 25 do campo livre
        //    //// e livre e DV (modulo10) deste campo
        //    //qP1 = qLinha.Substring(34, 10);
        //    //qP2 = modulo_10(Convert.ToInt32(qP1)).ToString();
        //    //qP3 = qP1 + qP2;
        //    //qP4 = qP3.Substring(0, 5);
        //    //qP5 = qP3.Substring(5);
        //    //string qCampo3 = qP4 + qP5;


        //    //// 4. Campo - digito verificador do codigo de barras
        //    //string qCampo4 = qLinha.Substring(4, 1);

        //    //// 5. Campo composto pelo valor nominal pelo valor nominal do documento, sem
        //    //// indicacao de zeros a esquerda e sem edicao (sem ponto e virgula). Quando se
        //    //// tratar de valor zerado, a representacao deve ser 000 (tres zeros).
        //    //string qCampo5 = qLinha.Substring(5, 14);

        //    //return qCampo1 + qCampo2 + qCampo3 + qCampo4 + qCampo5;
        //}
        // passado para Utilizaddes - MontaLinhaDigitavel===
    }





    // string Modulo11(string qNum)
    //{
    //    int qBase = 9;
    //    int qR = 0;
    //    int qSoma = 0;
    //    int qFator = 2;
    //    string qDigito = "";
    //    int qRetorno = 0;

    //    string[] qNumeros = new string[qNum.Length];
    //    int[] qParcial = new int[qNum.Length];

    //    for (int i = qNum.Length; i > 0; i--)
    //    {
    //        qNumeros[i] = qNum.Substring(i - 1, 1);
    //        qParcial[i] = Convert.ToInt32(qNumeros[i]) * qFator;
    //        qSoma = qSoma + qParcial[i];

    //        if (qFator == qBase)
    //        {
    //            qFator = 1;
    //        }
    //        qFator++;
    //    }

    //    if (qR == 0)
    //    {
    //        qSoma = qSoma * 10;
    //        qDigito = (qSoma % 11).ToString();

    //        //corrigido
    //        if (qDigito == "10")
    //        {
    //            qDigito = "X";
    //        }
    //        if (qNum == "43")
    //        {
    //            //então estamos checando a linha digitável
    //            if (qDigito == "0" || qDigito == "X")
    //            {
    //                qDigito = "1";
    //            }
    //            else if (int.TryParse(qDigito, out qRetorno))
    //            {
    //                if (Convert.ToInt32(qDigito) > 9)
    //                {
    //                    qDigito = "1";
    //                }
    //            }
    //        }
    //    else if (qR == 1)
    //        {
    //            qDigito = (qSoma % 11).ToString();
    //        }
    //    }
    //    return qDigito;
    //}

}