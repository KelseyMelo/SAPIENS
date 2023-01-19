using System;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace SERPI.UI.WebForms_C
{
    public class Utilizades
    {
        //private readonly BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        public static bool fEnviaEmail(string sDe, string sDe_Nome, string sPara, string sCopia, string sCopiaOculta, string sSubject, string sCorpo, string sServidor, string sContaEmail, string sSenhaEmail, int iPorta, int iPrioridade, string sAnexo)
        {
            try
            {
                //===Trecho Elson - Início =================
                //bool platformSupportsTls12 = false;
                //foreach (System.Net.SecurityProtocolType protocol in Enum.GetValues(typeof(System.Net.SecurityProtocolType)))
                //{
                //    if (protocol.GetHashCode() == 3072)
                //    {
                //        platformSupportsTls12 = true;
                //    }
                //}
                //// enable Tls12, if possible
                //if (!System.Net.ServicePointManager.SecurityProtocol.HasFlag((System.Net.SecurityProtocolType)3072))
                //{
                //    if (platformSupportsTls12)
                //    {
                //        //System.Net.ServicePointManager.SecurityProtocol |= (System.Net.SecurityProtocolType)3072;
                //        System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
                //    }
                //}
                //===Trecho Elson - Fim =================

                if (!System.Net.ServicePointManager.SecurityProtocol.HasFlag((System.Net.SecurityProtocolType)3072))
                {
                    foreach (System.Net.SecurityProtocolType protocol in Enum.GetValues(typeof(System.Net.SecurityProtocolType)))
                    {
                        if (protocol.GetHashCode() == 3072)
                        {
                            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
                            continue;
                        }
                    }
                }

                //  cria uma inst�ncia do objeto MailMessage
                MailMessage mMailMessage = new MailMessage();
                //  Define o endere�o do remetente
                mMailMessage.From = new MailAddress(sDe, sDe_Nome, Encoding.Unicode);
                // Verifica se o valor para recepient � null ou uma string vazia
                var sAux = sPara.Split(';');
                foreach (var elemento in sAux)
                {
                    if ((!(elemento == null) && (elemento != String.Empty)))
                    {
                        mMailMessage.To.Add(new MailAddress(elemento));
                    }
                }

                sAux = sCopia.Split(';');
                foreach (var elemento in sAux)
                {
                    if ((!(elemento == null) && (elemento != String.Empty)))
                    {
                        mMailMessage.CC.Add(new MailAddress(elemento));
                    }
                }

                sAux = sCopiaOculta.Split(';');
                foreach (var elemento in sAux)
                {
                    if ((!(elemento == null) && (elemento != String.Empty)))
                    {
                        mMailMessage.Bcc.Add(new MailAddress(elemento));
                    }
                }

                // Define o assunto 
                mMailMessage.Subject = sSubject;

                // Define o corpo da mensagem
                mMailMessage.Body = sCorpo;

                Attachment attachment;
                sAux = sAnexo.Split(';');
                foreach (var elemento in sAux)
                {
                    if ((!(elemento == null) && (elemento != String.Empty)))
                    {
                        attachment = new Attachment(elemento);
                        mMailMessage.Attachments.Add(attachment);
                    }
                }

                // Define o formato do email como HTML
                mMailMessage.IsBodyHtml = true;

                if ((iPrioridade == 1))
                {
                    mMailMessage.Priority = MailPriority.High;
                }
                else
                {
                    mMailMessage.Priority = MailPriority.Normal;
                }

                //  Cria uma inst�ncia de SmtpClient
                SmtpClient mSmtpClient = new SmtpClient(sServidor);
                mSmtpClient.Port = iPorta;
                mSmtpClient.EnableSsl = true;
                mSmtpClient.UseDefaultCredentials = false;
                mSmtpClient.Credentials = new System.Net.NetworkCredential(sContaEmail, sSenhaEmail);

                int i = 0;
                bool bRetorno = false;
                do
                {
                    try
                    {
                        mSmtpClient.Send(mMailMessage);
                        bRetorno = true;
                        break;
                    }
                    catch (Exception)
                    {

                    }
                    i++;
                    System.Threading.Thread.Sleep(2000);
                } while (i < 5);

                
                return bRetorno;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // O método EscreverExtenso recebe um valor do tipo decimal
        public static string EscreverExtenso(decimal valor)
        {
            if (valor <= 0 | valor >= 1000000000000000)
                return "Valor não suportado pelo sistema.";
            else
            {
                string strValor = valor.ToString("000000000000000.00");
                string valor_por_extenso = string.Empty;
                for (int i = 0; i <= 15; i += 3)
                {
                    valor_por_extenso += Escrever_Valor_Extenso(Convert.ToDecimal(strValor.Substring(i, 3)));
                    if (i == 0 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
                            valor_por_extenso += " TRILHÃO" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                            valor_por_extenso += " TRILHÕES" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 3 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
                            valor_por_extenso += " BILHÃO" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                            valor_por_extenso += " BILHÕES" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 6 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                            valor_por_extenso += " MILHÃO" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
                            valor_por_extenso += " MILHÕES" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " E " : string.Empty);
                    }
                    else if (i == 9 & valor_por_extenso != string.Empty)
                        if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
                            valor_por_extenso += " MIL" + ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0) ? " E " : string.Empty);
                    if (i == 12)
                    {
                        if (valor_por_extenso.Length > 8)
                            if (valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "BILHÃO" | valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "MILHÃO")
                                valor_por_extenso += " DE";
                            else
                                if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "BILHÕES" | valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "MILHÕES"
| valor_por_extenso.Substring(valor_por_extenso.Length - 8, 7) == "TRILHÕES")
                                valor_por_extenso += " DE";
                            else
                                    if (valor_por_extenso.Substring(valor_por_extenso.Length - 8, 8) == "TRILHÕES")
                                valor_por_extenso += " DE";
                        if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
                            valor_por_extenso += " REAL";
                        else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
                            valor_por_extenso += " REAIS";
                        if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != string.Empty)
                            valor_por_extenso += " E ";
                    }
                    if (i == 15)
                        if (Convert.ToInt32(strValor.Substring(16, 2)) == 1)
                            valor_por_extenso += " CENTAVO";
                        else if (Convert.ToInt32(strValor.Substring(16, 2)) > 1)
                            valor_por_extenso += " CENTAVOS";
                }
                return valor_por_extenso;
            }
        }

        static string Escrever_Valor_Extenso(decimal valor)
        {
            if (valor <= 0)
                return string.Empty;
            else
            {
                string montagem = string.Empty;
                if (valor > 0 & valor < 1)
                {
                    valor *= 100;
                }
                string strValor = valor.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));
                if (a == 1) montagem += (b + c == 0) ? "CEM" : "CENTO";
                else if (a == 2) montagem += "DUZENTOS";
                else if (a == 3) montagem += "TREZENTOS";
                else if (a == 4) montagem += "QUATROCENTOS";
                else if (a == 5) montagem += "QUINHENTOS";
                else if (a == 6) montagem += "SEISCENTOS";
                else if (a == 7) montagem += "SETECENTOS";
                else if (a == 8) montagem += "OITOCENTOS";
                else if (a == 9) montagem += "NOVECENTOS";
                if (b == 1)
                {
                    if (c == 0) montagem += ((a > 0) ? " E " : string.Empty) + "DEZ";
                    else if (c == 1) montagem += ((a > 0) ? " E " : string.Empty) + "ONZE";
                    else if (c == 2) montagem += ((a > 0) ? " E " : string.Empty) + "DOZE";
                    else if (c == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TREZE";
                    else if (c == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUATORZE";
                    else if (c == 5) montagem += ((a > 0) ? " E " : string.Empty) + "QUINZE";
                    else if (c == 6) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSEIS";
                    else if (c == 7) montagem += ((a > 0) ? " E " : string.Empty) + "DEZESSETE";
                    else if (c == 8) montagem += ((a > 0) ? " E " : string.Empty) + "DEZOITO";
                    else if (c == 9) montagem += ((a > 0) ? " E " : string.Empty) + "DEZENOVE";
                }
                else if (b == 2) montagem += ((a > 0) ? " E " : string.Empty) + "VINTE";
                else if (b == 3) montagem += ((a > 0) ? " E " : string.Empty) + "TRINTA";
                else if (b == 4) montagem += ((a > 0) ? " E " : string.Empty) + "QUARENTA";
                else if (b == 5) montagem += ((a > 0) ? " E " : string.Empty) + "CINQUENTA";
                else if (b == 6) montagem += ((a > 0) ? " E " : string.Empty) + "SESSENTA";
                else if (b == 7) montagem += ((a > 0) ? " E " : string.Empty) + "SETENTA";
                else if (b == 8) montagem += ((a > 0) ? " E " : string.Empty) + "OITENTA";
                else if (b == 9) montagem += ((a > 0) ? " E " : string.Empty) + "NOVENTA";
                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) montagem += " E ";
                if (strValor.Substring(1, 1) != "1")
                    if (c == 1) montagem += "UM";
                    else if (c == 2) montagem += "DOIS";
                    else if (c == 3) montagem += "TRÊS";
                    else if (c == 4) montagem += "QUATRO";
                    else if (c == 5) montagem += "CINCO";
                    else if (c == 6) montagem += "SEIS";
                    else if (c == 7) montagem += "SETE";
                    else if (c == 8) montagem += "OITO";
                    else if (c == 9) montagem += "NOVE";
                return montagem;
            }
        }

        public static string GerarSenha()
        {
            Random r = new Random();
            String sCatacteres = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            var qArray = sCatacteres.Split(',');
            String qSenha = "";
            for (int i = 1; i < 7; i++)
            {
                qSenha = qSenha + qArray[r.Next(0,35)];
            }

            return qSenha;
        }

        public static bool fValidaCNPJ(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        public static bool fValidaCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }


        [DllImport("advapi32.DLL", SetLastError = true)]
        public static extern int LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        public static string SalvaArquivoComAutenticacao(HttpPostedFile qArquivo, string qCaminho, string qLogin, string qDominio, string qSenha)
        {
            try
            {
                AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

                IntPtr token = default(IntPtr);
                if (LogonUser(qLogin, qDominio, qSenha, 2, 0, ref token) != 0)
                {
                    WindowsIdentity identity = new WindowsIdentity(token);
                    WindowsImpersonationContext context = identity.Impersonate();
                    qArquivo.SaveAs(qCaminho);
                }

                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static DateTime[] CalculaFeriadosMoveis(int pAno)
        {
            try
            {
                int y = pAno;
                int d = (255 - 11 * (y % 19) - 21) % 30 + 21;
                var dRetorno = new DateTime[4];
                var dPascoa = new DateTime(y, 3, 1);
                dPascoa = dPascoa.AddDays(+d + Convert.ToInt32(d > 48) + 6 - (y + y / 4 + d + Convert.ToInt32(d > 48) + 1) % 7);
                dRetorno[0] = dPascoa.AddDays(-48); // Segunda de carnaval
                dRetorno[1] = dPascoa.AddDays(-47); // Terça de carnaval
                dRetorno[2] = dPascoa.AddDays(-2); // Sexta-Feira Santa
                dRetorno[3] = dPascoa.AddDays(60d); // Corpus Crhisty
                return dRetorno;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        //===== Boleto - Inicio===
        public static string GeraCodigoBancoComDigito(string qNumero)
        {
            string qParte1 = qNumero.Substring(0, 3);
            string qParte2 = CalcularDigitoModulo11(qParte1).ToString();
            return qParte1 + "-" + qParte2;
        }

        // C�lculo do d�gito verificador
        public static int CalcularDigitoModulo11(string numero)
        {
            int soma = 0;
            int peso = 2;
            int digito;

            for (int I = numero.Length - 1; I >= 0; I -= 1)
            {
                soma = (int)Math.Round(soma + Convert.ToDouble(numero.Substring(I, 1)) * peso);

                if (peso < 9)
                {
                    peso = peso + 1;
                }
                else
                {
                    peso = 2;
                }
            }

            digito = 11 - soma % 11;

            if (digito > 9)
            {
                digito = 0;
            }

            if (digito == 0)
            {
                digito = 1;
            }
            return digito;
        }
        // C�lculo do d�gito verificador

        protected static double DateToDays(int year, int month, int day)
        {
            string century = year.ToString().Substring(0, 2);
            year = Convert.ToInt32(year.ToString().Substring(2, 2));
            if (month > 2)
            {
                month -= 3;
            }
            else
            {
                month += 9;
                if (year == 0)
                {
                }
                else
                {
                    year = 99;
                    century = (Convert.ToInt32(century) - 1).ToString();
                }
            }
            double a = 146097d * Convert.ToDouble(century) / 4d;
            double b = 1461 * year / 4d;
            double c = (153 * month + 2) / 5d;
            int d = day + 1721119;
            return (a + b + c + d);
        }

        public static double fator_vencimento(string qData)
        {
            var qSplit = qData.Split('/');

            string qAno = qSplit[2];
            string qMes = qSplit[1];
            string qDia = qSplit[0];
            return Math.Round(Math.Abs(DateToDays(Convert.ToInt32("1997"), Convert.ToInt32("10"), Convert.ToInt32("07")) - DateToDays(Convert.ToInt32(qAno), Convert.ToInt32(qMes), Convert.ToInt32(qDia))));
        }

        public static string MontaLinhaDigitavel(string qLinha)
        {
            String linha = qLinha.Replace("[^0-9]", "");

            if (linha.Length != 44)
            {
                return null; // 'A linha do Código de Barras está incompleta!'
            }

            String campo1 = linha.Substring(0, 4) + linha.Substring(19, 1) + '.' + linha.Substring(20, 4);
            String campo2 = linha.Substring(24, 5) + '.' + linha.Substring(29, 5);
            String campo3 = linha.Substring(34, 5) + '.' + linha.Substring(39, 5);
            String campo4 = linha.Substring(4, 1); // Digito verificador
            String campo5 = linha.Substring(5, 14); // Vencimento + Valor

            if (CalcularDigitoModulo11(linha.Substring(0, 4) + linha.Substring(5, 39)) != Convert.ToInt32(campo4))
            {
                return null; //'Digito verificador '+campo4+', o correto é '+modulo11_banco(  linha.substr(0,4)+linha.substr(5,99)  )+'\nO sistema não altera automaticamente o dígito correto na quinta casa!'
            }
            string rRet = campo1 + modulo_10(campo1)
                    + ' '
                    + campo2 + modulo_10(campo2)
                    + ' '
                    + campo3 + modulo_10(campo3)
                    + ' '
                    + campo4
                    + ' '
                    + campo5
                    ;
            return rRet;
        }

        public static int modulo_10(string num)
        {
            num = num.Replace(".", "");
            //          int digito=0;
            int mult = 2;
            int sum = 0;

            for (int i = (num.Length - 1); i >= 0; i--)
            {
                char c = num[i];

                int res = Convert.ToInt32(c.ToString()) * mult;
                sum += res > 9 ? (res - 10) + 1 : res;
                mult = mult == 2 ? 1 : 2;
            }

            int ret;
            if (sum == 10 || 10 - (sum % 10) == 10)
            {
                ret = 0;
            }
            else
            {
                ret = 10 - (sum % 10);
            }

            return ret;
        }

        public static string FormataNumero(string qNumero, string qLoop, string qInsert, string qTipo = "geral")
        {
            if (qTipo == "geral")
            {
                qNumero = qNumero.Replace(",", "");
                while (qNumero.Length < Convert.ToInt32(qLoop))
                {
                    qNumero = (qInsert + qNumero).ToString();
                }
            }
            if (qTipo == "valor")
            {
                /*
                retira as virgulas
                formata o numero
                preenche com zeros
                */
                qNumero = qNumero.Replace(",", "");
                while (qNumero.Length < Convert.ToInt32(qLoop))
                {
                    qNumero = (qInsert + qNumero).ToString();
                }
            }
            if (qTipo == "convenio")
            {
                while (qNumero.Length < Convert.ToInt32(qLoop))
                {
                    qNumero = (qInsert + qNumero).ToString();
                }
            }
            return qNumero;
        }

        public static string CodigoBarra(string qLinha_Digitavel)
        {
            string sAux = "";
            string sAux2 = "";
            int f;
            int f1;
            int f2;
            int i;
            string texto = "";

            int fino = 1;
            int largo = 3;
            int altura = 50;

            string[] BarCodes = new string[100];
            if ((BarCodes[0]) == null)
            {
                BarCodes[0] = "00110";
                BarCodes[1] = "10001";
                BarCodes[2] = "01001";
                BarCodes[3] = "11000";
                BarCodes[4] = "00101";
                BarCodes[5] = "10100";
                BarCodes[6] = "01100";
                BarCodes[7] = "00011";
                BarCodes[8] = "10010";
                BarCodes[9] = "01010";
                for (f1 = 9; (f1 >= 0); f1--)
                {
                    for (f2 = 9; (f2 >= 0); f2--)
                    {
                        f = ((f1 * 10) + f2);
                        texto = "";
                        for (i = 0; (i < 5); i++)
                        {
                            texto = (texto + (BarCodes[f1].Substring((i), 1) + BarCodes[f2].Substring((i), 1)));
                        }
                        BarCodes[f] = texto;
                    }
                }
            }

            //int j = 0;
            //foreach (var item in BarCodes)
            //{

            //    sAux = sAux + "Barcode: " + j + ": " + item + "<br>";
            //    j++;
            //}

            //litCodigoBarra.Text = Server.HtmlDecode(sAux);
            //return;
            // Desenho da barra
            //  Guarda inicial
            sAux = "<img src=img/img-cabecalho/p.png width=" + fino + " height=" + altura + " border=0><img ";
            sAux = sAux + "src=img/img-cabecalho/b.png width =" + fino + " height =" + altura + " border = 0 ><img ";
            sAux = sAux + "src=img/img-cabecalho/p.png width =" + fino + " height =" + altura + " border = 0 ><img ";
            sAux = sAux + "src=img/img-cabecalho/b.png width =" + fino + " height =" + altura + " border = 0 ><img ";



            //texto = "00190.00009 03304.249000 32802.711179 7 90920000009913";  // 47

            // = "00191.91405 00000 012000 00000 282841 9 2828419100002702917";

            texto = qLinha_Digitavel;

            //texto = RetornaLinhaCodigoBarra(texto);

            //texto = "00197909200000099130000003304249003280271117";  // 44

            if ((texto.Length % 2) != 0)
            {
                texto = ("0" + texto);
            }

            //  Draw dos dados
            while ((texto.Length > 0))
            {
                i = int.Parse(texto.Substring(0, 2));
                texto = texto.Substring((texto.Length - (texto.Length - 2)));
                sAux2 = BarCodes[i];
                for (i = 0; (i < 10); i = (i + 2))
                {
                    if ((sAux2.Substring((i), 1) == "0"))
                    {
                        f1 = fino;
                    }
                    else
                    {
                        f1 = largo;
                    }

                    sAux = sAux + "src =img/img-cabecalho/p.png width =" + f1 + " height =" + altura + " border = 0 ><img ";

                    if ((sAux2.Substring(i + 1, 1) == "0"))
                    {
                        f2 = fino;
                    }
                    else
                    {
                        f2 = largo;
                    }

                    sAux = sAux + "src =img/img-cabecalho/b.png width =" + f2 + " height =" + altura + " border=0><img ";
                }

            }

            //  Draw guarda final
            sAux = sAux + "src =img/img-cabecalho/p.png width =" + largo + " height =" + altura + " border = 0 ><img ";
            sAux = sAux + "src =img/img-cabecalho/b.png width =" + fino + " height =" + altura + " border = 0 ><img ";
            sAux = sAux + "src =img/img-cabecalho/p.png width =1 height =" + altura + " border = 0 > ";

            return sAux;

        }

        //===== Boleto - Fim===
    }
}
