<%@ Page Title="" Language="C#" MasterPageFile="~/MSadmin/adminMS.Master" AutoEventWireup="true" CodeBehind="Logs.aspx.cs" Inherits="MashadCarpet.MSadmin.Logs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:GridView ID="grdTable" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
            GridLines="None" OnRowCommand="grdTable_RowCommand" Width="100%">

                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="ColorID" HeaderText="کد رنگ" />
                    <asp:BoundField DataField="ColorTitle" HeaderText="عنوان" />
                    <asp:BoundField DataField="ColorEN_Title" HeaderText="عنوان انگلیسی" />
                    <asp:BoundField DataField="Rus_ColorTitle" HeaderText="عنوان روسی" />
                    <asp:BoundField DataField="China_ColorTitle" HeaderText="عنوان چینی" />
                    <asp:BoundField DataField="ColorName" HeaderText="عنوان سیستمی" />
                    <asp:BoundField DataField="ColorNo" HeaderText="کد رنگ" />

                    <asp:TemplateField HeaderText="توضیحات">

                        <ItemTemplate>

                            <i class="fa fa-edit"></i>
                            <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("ColorID") %>' CommandName="DoEdit" CssClass="lb">ویرایش</asp:LinkButton>
                            <br />
                            <i class="fa fa-times"></i>
                            <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%# Eval("ColorID") %>' CommandName="DoDelete" CssClass="lb">حذف</asp:LinkButton>
                            <br />
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
</asp:Content>
