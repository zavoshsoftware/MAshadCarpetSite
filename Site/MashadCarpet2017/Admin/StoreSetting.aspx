<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="StoreSetting.aspx.cs" Inherits="MashadCarpet.Admin.StoreSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:MultiView runat="server" ID="mvStores">
        <asp:View runat="server" ID="vwlist">
            <asp:Button runat="server" Text="افزودن" ID="btnadd" OnClick="btnadd_Click" CssClass="btn btn-success" />
            &nbsp;&nbsp;&nbsp
             <asp:Button runat="server" Text="بازگشت" ID="btnback" OnClick="btnback_Click" CssClass="btn btn-warning" />

            <br />
            <br />
              <asp:Panel ID="pnlEmptyForm" runat="server" Visible="false">
                    <p class="alert alert-danger pnlEmptyForm">اطلاعاتی جهت نمایش موجود نمی باشد.</p>
                </asp:Panel>
            <asp:GridView ID="grdStores" runat="server" AutoGenerateColumns="False" CellPadding="4" Width="100%"
                ForeColor="#333333" GridLines="None" OnRowCommand="grdStores_RowCommand">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="StoreName" HeaderText="نام فروشگاه" />

                    <asp:BoundField DataField="StorePhone" HeaderText="تلفن فروشگاه" />

                    <asp:BoundField DataField="Prov" HeaderText="استان فروشگاه" />

                    <asp:BoundField DataField="StoreCity" HeaderText="شهر فروشگاه" />



                    <asp:TemplateField HeaderText="توضیحات">
                        <ItemTemplate>
                            <i class="fa fa-edit"></i>
                            <asp:LinkButton runat="server" CommandArgument='<%# Eval("StoreID") %>' CommandName="DoEdit">ویرایش</asp:LinkButton><br />
                            <i class="fa fa-trash"></i>
                            <asp:LinkButton runat="server" CommandArgument='<%# Eval("StoreID") %>' CommandName="DoDelete">حذف</asp:LinkButton>
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
        <asp:View runat="server" ID="vwedit">
            <table class="style1">
                

                <tr>
                    <td class="style2">تلفن  :</td>
                    <td>
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Width="350px"></asp:TextBox>
                    </td>
                </tr>
               
                <tr>
                    <td class="style2">شهر  :</td>
                    <td>
                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" Width="350px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2">آدرس  :</td>
                    <td>
                        <asp:TextBox TextMode="MultiLine" ID="txtAddress" runat="server" CssClass="form-control" Width="350px"></asp:TextBox>
                    </td>
                </tr>

                 


                <tr>
                    <td class="style2">&nbsp;</td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="ثبت" OnClick="btnSave_Click" CssClass="btn btn-success" />
                        &nbsp; &nbsp; &nbsp;
                        <asp:Button ID="btnCanceleEdit" runat="server" Text="انصراف" OnClick="btnCanceleEdit_Click" CssClass="btn btn-danger" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View runat="server" ID="vwdelete">
            آیا مایل به حذف 
            <asp:Label ID="lblDelete" runat="server" Text=""></asp:Label>
            هستید؟
       <asp:Button ID="btnAgree" runat="server" Text="بلی" OnClick="btnAgree_Click" CssClass="btn btn-success" />
            &nbsp;&nbsp;
            <asp:Button ID="btnDisAgree" runat="server" Text="خیر" CssClass="btn btn-danger"
                OnClick="btnDisAgree_Click" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
