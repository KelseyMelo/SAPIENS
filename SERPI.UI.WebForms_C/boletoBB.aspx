<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="boletoBB.aspx.cs" Inherits="SERPI.UI.WebForms_C.boletoBB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Emissão de Boleto Banco Do Brasil</title>
</head>
<body onload="JavaScript:document.forms[0].submit();">
  <form action="https://mpag.bb.com.br/site/mpag/" method="post" name="pagamento" runat="server">
      <asp:Literal ID="litInputs" runat="server"></asp:Literal>

  </form>
</body>
</html>
