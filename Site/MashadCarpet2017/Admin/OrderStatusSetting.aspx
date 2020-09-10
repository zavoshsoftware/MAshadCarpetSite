<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="OrderStatusSetting.aspx.cs" Inherits="MashadCarpet.Admin.OrderStatusSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvSetting" runat="server">

        <asp:View ID="vwList" runat="server">
            <asp:Button ID="btnAdd" runat="server" Text="افزودن" CssClass="btn btn-success" OnClick="btnAdd_Click" />
            <asp:Button ID="btnBack" runat="server" Text="برگشت" CssClass="btn btn-warning" OnClick="btnBack_Click" /><br />
            <asp:GridView ID="grdTable" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" OnRowCommand="grdTable_RowCommand">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="OrderStatusTitle" HeaderText="وضعیت سفارش" />
                    <asp:BoundField DataField="EN_OrderStatus" HeaderText="وضعیت سفارش انگلیسی" />
                    <asp:BoundField DataField="Rus_OrderStatus" HeaderText="وضعیت سفارش روسی" />
                    <asp:BoundField DataField="China_OrderStatus" HeaderText="وضعیت سفارش چینی" />

                    <asp:TemplateField HeaderText="توضیحات">

                        <ItemTemplate>
                            <i class="fa fa-edit"></i>
                            <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("OrderStatusID") %>' CommandName="DoEdit" CssClass="lb">ویرایش</asp:LinkButton>
                            <br />
                            <i class="fa fa-edit"></i>
                            <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%# Eval("OrderStatusID") %>' CommandName="DoDelete" CssClass="lb">حذف</asp:LinkButton>

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
        <asp:View ID="vwEdit" runat="server">
            <asp:Button ID="btnReturn" runat="server" Text="برگشت" CssClass="btn btn-warning" OnClick="btnReturn_Click" /><br />
            <table>


                <tr>
                    <td>وضعیت سفارش
                    </td>
                    <td>
                        <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>وضعیت سفارش انگلیسی
                    </td>
                    <td>
                        <asp:TextBox ID="txtEN_Status" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>وضعیت سفارش روسی
                    </td>
                    <td>
                        <asp:TextBox ID="txtRus_Status" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>وضعیت سفارش چینی
                    </td>
                    <td>
                        <asp:TextBox ID="txtChina_Status" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="ثبت" OnClick="btnSave_Click" CssClass="btn btn-success" />
                        <asp:Button ID="btnCancel" runat="server" Text="انصراف" OnClick="btnCancel_Click" CssClass="btn btn-danger" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwDelete" runat="server">
            آیا مطمئن به حذف 
        <asp:Label ID="lblDelete" runat="server" Text=""></asp:Label>
            هستید؟

        <asp:Button ID="btnAgree" runat="server" Text="بلی" OnClick="btnAgree_Click" CssClass="btn btn-success" />
            <asp:Button ID="btnDeny" runat="server" Text="خیر" OnClick="btnDeny_Click" CssClass="btn btn-danger" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
