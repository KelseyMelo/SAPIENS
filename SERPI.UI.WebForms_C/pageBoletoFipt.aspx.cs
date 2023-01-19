using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SERPI.UI.WebForms_C
{
    public partial class pageBoletoFipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string URI = "http://200.18.106.49:8080/login/aluno/";
            var reqparm = new System.Collections.Specialized.NameValueCollection();
            reqparm.Add("login", "112.742.388-62");
            reqparm.Add("senha", null);
            reqparm.Add("tipo_user", "aluno");
            reqparm.Add("dtnasc", null);

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var HtmlResult = wc.UploadValues(URI, reqparm);

                string responsebody = Encoding.UTF8.GetString(HtmlResult);

                //string myParameters = "login=112.742.388-62&senha=&tipo_user=alunovalue3&dtnasc=";

                //var HtmlResult2 = wc.UploadString(URI, myParameters);

            }
        }
    }
}