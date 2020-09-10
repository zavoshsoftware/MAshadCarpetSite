<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4_lang.aspx.cs" Inherits="MashadCarpet.WebForm4_lang" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register src="Controls/UCLanguage.ascx" tagname="UCLanguage" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ddd</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" meta:resourcekey="Label1Resource1"></asp:Label>
    
                              <uc1:UCLanguage ID="UCLanguage1" runat="server" />
    </div>
    </form>
</body>
</html>
