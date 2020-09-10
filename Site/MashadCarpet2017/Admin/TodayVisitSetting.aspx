<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="TodayVisitSetting.aspx.cs" Inherits="MashadCarpet.Admin.TodayVisitSetting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:Label ID="lblTotalCount" runat="server" Text="" CssClass="Text-Msg"></asp:Label>
        <asp:Button ID="btnReturn" runat="server" Text="بازگشت" Visible="false" CssClass="btn btn-warning pull-left" OnClick="btnReturn_Click" CausesValidation="false"  /><br /><br />

        <asp:GridView ID="GrdTable" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="GrdTable_PageIndexChanging" PageSize="20">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="ردیف">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ClientIP" HeaderText="IP" />
                <asp:BoundField DataField="OS" HeaderText="OS" />
                <asp:BoundField DataField="Browser" HeaderText="Browser" />
                <asp:BoundField DataField="RefrallPage" HeaderText="RefrallPage" />
                <asp:BoundField DataField="VisitDate" DataFormatString="{0:t}" HeaderText="ساعت" />
                <%--<asp:BoundField DataField="VisitCount" HeaderText="تعداد بازدید" />--%>
                <%--<asp:BoundField DataField="TotalVisit" HeaderText="بازدید کل" />--%>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
</asp:Content>
