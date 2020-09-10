<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testPayment.aspx.cs" Inherits="MashadCarpet.testPayment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script language="javascript" type="text/javascript">
        function postRefId(refIdValue) {
            var form = document.createElement("form");
            form.setAttribute("method", "POST");
            form.setAttribute("action", "<%= PgwSite %>");
            form.setAttribute("target", "_self");
            var hiddenField = document.createElement("input");
            hiddenField.setAttribute("name", "RefId");
            hiddenField.setAttribute("value", refIdValue);
            form.appendChild(hiddenField);
            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
      <%--  <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="btncalculate" OnClick="btncalculate_Click" runat="server" Text="calculate" />--%>
        <asp:Button ID="Button1" runat="server" Text="پرداخت" OnClick="Button1_Click" />
    </div>
    </form>
</body>
</html>
