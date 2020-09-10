<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="reqList.aspx.cs" Inherits="MashadCarpet.Admin.reqList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlEmptyForm" runat="server" Visible="false">
        <p class="alert alert-danger pnlEmptyForm" >اطلاعاتی جهت نمایش موجود نمی باشد.</p>
    </asp:Panel>
     <asp:Panel ID="pnlDetails" runat="server" Visible="false">
        <table class="auto-style1">
            <tr>
                <td class="tdRight">نام</td>
                <td>
                    <asp:Label ID="lblName" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="tdRight">نام خانوادگی</td>
                <td>
                    <asp:Label ID="lblLastName" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="tdRight">نام پدر</td>
                <td>
                    <asp:Label ID="lblFathername" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="tdRight">شماره ملی</td>
                <td>
                    <asp:Label ID="lblNational" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="tdRight">تاریخ تولد</td>
                <td>
                    <asp:Label ID="lblBirthday" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="tdRight">محل تولد</td>
                <td>
                    <asp:Label ID="lblBirthPlace" runat="server" Text=""></asp:Label></td>
            </tr>
           
            <tr>
                <td class="tdRight">نام فروشگاه</td>
                <td>
                    <asp:Label ID="lblStoreName" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="tdRight">تاریخ شروع فعالیت</td>
                <td>
                    <asp:Label ID="lblStartDate" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="tdRight">ایمیل</td>
                <td>
                    <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="tdRight">وب سایت</td>
                <td>
                    <asp:Label ID="lblWebsite" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="tdRight">کد پستی</td>
                <td>
                    <asp:Label ID="lblPostalCode" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="tdRight">تلفن تماس</td>
                <td>
                    <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="tdRight">فکس</td>
                <td>
                    <asp:Label ID="lblFax" runat="server" Text=""></asp:Label></td>
            </tr>
          
            <tr>
                <td class="tdRight">شهر</td>
                <td>
                    <asp:Label ID="lblCity" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="tdRight">آدرس</td>
                <td>
                    <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label></td>
            </tr>

            <tr>
                <td class="tdRight">توضیحات</td>
                <td>
                    <asp:Label ID="lblDesc" runat="server" Text=""></asp:Label></td>
            </tr>

        </table>


    </asp:Panel>
    <asp:Panel ID="pnlDelete" runat="server" Visible="false">
        آیا از حذف مطمئن می باشید؟
        <br />
        <asp:Button ID="btnYes" OnClick="btnYes_Click" runat="server" Text="بلی" CssClass="btn btn-success" /> &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnNo" OnClick="btnNo_Click" runat="server" Text="خیر" CssClass="btn btn-danger" />
    </asp:Panel>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
        OnRowCommand="GridView1_RowCommand1" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="FirstName" HeaderText="نام"></asp:BoundField>
            <asp:BoundField DataField="LastName" HeaderText="نام خانوادگی"></asp:BoundField>
            <asp:BoundField DataField="phone" HeaderText="تلفن"></asp:BoundField>
       
            <asp:BoundField DataField="CompanyName" HeaderText="نام شرکت"></asp:BoundField>
            <asp:BoundField DataField="City" HeaderText="شهر"></asp:BoundField>
            <asp:TemplateField HeaderText="جزییات">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Det" CommandArgument='<%# Eval("AgentID") %>'>مشاهده جزییات</asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Del" CommandArgument='<%# Eval("AgentID") %>'>حذف</asp:LinkButton>
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
