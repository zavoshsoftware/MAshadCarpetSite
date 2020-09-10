<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="SliderSetting.aspx.cs" Inherits="MashadCarpet.Admin.SliderSetting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:MultiView ID="mvSetting" runat="server">
        <asp:View ID="vwList" runat="server">
            <asp:Button ID="btnAdd" runat="server" Text="افزودن" OnClick="btnAdd_Click" CssClass="btn btn-success" />
            <asp:Button ID="btnReturn" runat="server" Text="بازگشت" OnClick="btnReturn_Click" CssClass="btn btn-warning" />
            <br />
            <asp:GridView ID="grdTable" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" OnRowCommand="grdTable_RowCommand">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="ردیف">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SliderTitle" HeaderText="عنوان" />
                   
                    <asp:TemplateField HeaderText="تصویر">
                        <ItemTemplate>
                            <img src='<%# Eval("SliderImage","/Uploads/Sliders/{0}") %>' width="100px" height="100px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="تنظیمات">
                        <ItemTemplate>
                             <i class="fa fa-edit"></i>
                             <asp:LinkButton ID="lbEdit" runat="server" CommandName="DoEdit" CommandArgument='<%# Eval("SliderID") %>'>ویرایش</asp:LinkButton><br />
                            <i class="fa fa-trash"></i>
                            <asp:LinkButton ID="lbDelete" runat="server" CommandName="DoDelete" CommandArgument='<%# Eval("SliderID") %>'>حذف</asp:LinkButton>
                           <br />
                              <i class="fa fa-text-height"></i>
                            <a href='<%# Eval("SliderID","SliderTextSetting.aspx?id={0}") %>'>ویرایش متن ها</a>
                          
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
        <asp:View ID="vwEdit" runat="server"><div class="ad-div">
            <table>
                <tr>
                    <td>عنوان</td>
                    <td>

                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                    </td>
                </tr>

<%--                <tr>
                    <td>متن لایه اول</td>
                    <td>
                        <asp:TextBox ID="txtLayer1" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>متن لایه دوم</td>
                    <td>

                        <asp:TextBox ID="txtLayer2" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>متن لایه سوم</td>
                    <td>

                        <asp:TextBox ID="txtLayer3" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>متن لایه چهارم</td>
                    <td>

                        <asp:TextBox ID="txtLayer4" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                    </td>
                </tr>--%>

                <tr>
                    <td>تصویر</td>
                    <td>
                        <asp:FileUpload ID="fuImg" runat="server" />
                        <asp:Image ID="ImgSlider" runat="server" Visible="false" Width="200px" Height="200px" />
                    </td>
                </tr>

                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="ثبت" OnClick="btnSave_Click" CssClass="btn btn-success" />
                        <asp:Button ID="btnCancel" runat="server" Text="انصراف" OnClick="btnCancel_Click" CssClass="btn btn-danger" />
                    </td>
                </tr>
            </table></div>
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
