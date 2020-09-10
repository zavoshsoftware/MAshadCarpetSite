<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCUsefullLinks.ascx.cs" Inherits="MashadCarpet.Controls.UCUsefullLinks" %>
<asp:Panel ID="pnlLoginBox" runat="server" Visible="false">
    <h4 style="text-align: right;">
        <asp:Literal ID="ltServices" runat="server" Text="<%$Resources:Resource,CustomerServicesTitle%>"></asp:Literal>
    </h4>

    <ul class="links" style="text-align: right;">
        <li><a href="/SendTicket.aspx">
            <asp:Literal ID="ltSupportKnowledgebase" runat="server" Text="<%$Resources:Resource,SupportKnowledgebase%>"></asp:Literal></a></li>
        <li><a href="/MyTickets.aspx">
            <asp:Literal ID="ltTicketsList" runat="server" Text="<%$Resources:Resource,TicketsList%>"></asp:Literal></a></li>
        <li><a href="/bill/">
            <asp:Literal ID="ltOrderHistory" runat="server" Text="<%$Resources:Resource,OrderHistory%>"></asp:Literal></a></li>
        <li><a href="#">
            <asp:Literal ID="ltPaymentsreport" runat="server" Text="<%$Resources:Resource,Paymentsreport%>"></asp:Literal></a></li>
        <li><a href="/Terms-Conditions">
            <asp:Literal ID="ltTermsAndConditions" runat="server" Text="<%$Resources:Resource,TermsAndConditions%>"></asp:Literal></a></li>
    </ul>
    <div class="floatright">
        <img id='drftxlapgwmdgwmddrft' style='cursor: pointer' onclick='window.open("http://trustseal.enamad.ir/Verify.aspx?id=13991&p=nbpdfuixjzpgjzpgnbpd", "Popup","toolbar=no, location=no, statusbar=no, menubar=no, scrollbars=1, resizable=0, width=580, height=600, top=30")' alt='' src='http://trustseal.enamad.ir/logo.aspx?id=13991&p=lznbvjymzpfvzpfvlznb' />
    </div>
</asp:Panel>


<asp:Panel ID="pnlLinks" runat="server">
    <h4 style="text-align: right;">
        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Resource,Links%>"></asp:Literal>
    </h4>

    <ul class="links" style="text-align: right;">
        <li><a href="/default.aspx">
            <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Resource,MainPageTitle%>"></asp:Literal></a></li>
        <li><a href="/carpet-online-shopping">
            <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Resource,machinecarpetmodel%>"></asp:Literal></a></li>
        <li><a href="/corporate-social-responsibility">
            <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Resource,SocialResponsibility%>"></asp:Literal></a></li>
        <li><a href="/Sales-Representatives">
            <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Resource,Markets%>"></asp:Literal></a></li>
        <li><a href="/Terms-Conditions">
            <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Resource,TermsAndConditions%>"></asp:Literal></a></li>

        <li><a href="/Contact-us">
            <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Resource,Contacts%>"></asp:Literal></a></li>

    </ul>

    <div class="floatright">
        <img id='drftxlapgwmdgwmddrft' style='cursor: pointer' onclick='window.open("http://trustseal.enamad.ir/Verify.aspx?id=13991&p=nbpdfuixjzpgjzpgnbpd", "Popup","toolbar=no, location=no, statusbar=no, menubar=no, scrollbars=1, resizable=0, width=580, height=600, top=30")' alt='' src='http://trustseal.enamad.ir/logo.aspx?id=13991&p=lznbvjymzpfvzpfvlznb' />
    </div>
</asp:Panel>
