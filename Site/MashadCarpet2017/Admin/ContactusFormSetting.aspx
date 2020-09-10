<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ContactusFormSetting.aspx.cs" Inherits="MashadCarpet.Admin.ContactusFormSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvSetting" runat="server">
        <asp:View ID="vwList" runat="server">

            <asp:Button ID="btnReturn" runat="server" Text="بازگشت" OnClick="btnReturn_Click" CssClass="btn btn-warning" />
            <br />
            <br />
            <asp:GridView ID="grdTable" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" OnRowCommand="grdTable_RowCommand">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="ردیف">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ContactusName" HeaderText="نام" />
                    <asp:BoundField DataField="ContactusEmail" HeaderText="ایمیل" />
                    <asp:TemplateField HeaderText="تنظیمات">
                        <ItemTemplate>
                            <i class="fa fa-eye"></i>
                            <asp:LinkButton ID="lbShow" runat="server" CommandName="ShowMsg" CommandArgument='<%# Eval("ContactusID") %>'>مشاهده</asp:LinkButton>
                            <br />
                            <i class="fa fa-trash"></i>
                            <asp:LinkButton ID="lbDelete" runat="server" CommandName="DoDelete" CommandArgument='<%# Eval("ContactusID") %>'>حذف</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
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

        </asp:View>
        <asp:View ID="vwShow" runat="server">

            <div class="container">
                <div class="row">
                    <asp:Button ID="btnBack" runat="server" Text="بازگشت" OnClick="btnBack_Click" CssClass="btn btn-warning" />
                    <br />
                    <br />
                    <div style="border-bottom: 1px double #AEA995;padding:10px;">
                        نام:  
                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  ایمیل: 
                        <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                    </div>

                    <br />
                    <br />
                 
                   <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
            </div>

        </asp:View>
        <asp:View ID="vwDelete" runat="server">
            آیا مایل به حذف
                <asp:Label ID="lblDelete" runat="server" Text=""></asp:Label>
            هستید؟
                <br />
            <asp:Button ID="btnAgree" runat="server" Text="بلی" OnClick="btnAgree_Click" CssClass="btn btn-success" />
            <asp:Button ID="btnDeny" runat="server" Text="خیر" OnClick="btnDeny_Click" CssClass="btn btn-warning" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
