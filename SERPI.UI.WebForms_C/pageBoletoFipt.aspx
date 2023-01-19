<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pageBoletoFipt.aspx.cs" Inherits="SERPI.UI.WebForms_C.pageBoletoFipt" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/AdminLTE/jQuery/jquery-2.2.3.min.js"></script>
</head>
<body onload="JavaScript:document.forms[0].submit();">
  <form action="http://200.18.106.49:8080/login/aluno/" method="post" runat="server">
      <input type="hidden" name="login" value="112.742.388-62" />
	  <input type="hidden" name="senha"/>";
	  <input type="hidden" name="tipo_user" value="aluno" />";
	  <input type="hidden" name="dtnasc" />";
  </form>
</body>
    <script>
        $(document).ready(function () {
            document.forms[0].submit();
            alert('oi1');

            alert('oi2');
            var bodyHtml = $(entirePageHTML).find('body').html();

            alert('oi');

		    });
    </script>
</html>
