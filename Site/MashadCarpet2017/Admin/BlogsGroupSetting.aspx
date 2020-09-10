<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="BlogsGroupSetting.aspx.cs" Inherits="MashadCarpet.Admin.BlogsGroupSetting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:MultiView ID="mvSetting" runat="server">

        <asp:View ID="vwList" runat="server">
            <asp:Button ID="btnAdd" runat="server" Text="افزودن" OnClick="btnAdd_Click" CssClass="btn btn-success" />
            <asp:Button ID="btnReturn" runat="server" Text="بازگشت" OnClick="btnReturn_Click" CssClass="btn btn-warning" CausesValidation="false" />
            <br />
            <br />
             <asp:Panel ID="pnlEmptyForm" runat="server" Visible="false">
                <p class="alert alert-danger pnlEmptyForm">اطلاعاتی جهت نمایش موجود نمی باشد.</p>
            </asp:Panel>
            <asp:GridView ID="grdTable" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="grdProductGroup_RowCommand">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="BlogGroupTitle" HeaderText="عنوان"></asp:BoundField>
                      <asp:BoundField DataField="EN_BlogGroupTitle" HeaderText="عنوان انگلیسی" />
                      <asp:BoundField DataField="Rus_BlogGroupTitle" HeaderText="عنوان روسی" />
                      <asp:BoundField DataField="China_BlogGroupTitle" HeaderText="عنوان چینی" />
                    <asp:BoundField DataField="BlogGroupName" HeaderText="عنوان سیستمی" />
                    

                      <%--<asp:BoundField DataField="SubmitDate" HeaderText="تاریخ ثبت" DataFormatString="{0:d}" />--%>

                  <%--  <asp:TemplateField HeaderText="تصویر">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ProductGroupImage","~/Uploads/ProductGroups/{0}") %>' Width="100px" Height="100px" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="توضیحات">
                        <ItemTemplate>
                              <i class="fa fa-eye"></i>
                            <asp:LinkButton ID="lbBlogs" runat="server" CommandArgument='<%# Eval("BlogGroupID") %>' CommandName="Blogs" CssClass="lb">بلاگ ها</asp:LinkButton>
                            <br />
                            <i class="fa fa-edit"></i>
                            <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("BlogGroupID") %>' CommandName="DoEdit" CssClass="lb">ویرایش</asp:LinkButton>
                            <br />
                            <i class="fa fa-times"></i>
                            <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%# Eval("BlogGroupID") %>' CommandName="DoDelete" CssClass="lb">حذف</asp:LinkButton>
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
        </asp:View>
        <asp:View ID="vwEdit" runat="server"><div class="ad-div">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <table>

                <tr>
                    <td>عنوان</td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control txt"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ErrorMessage="عنوان را وارد نمایید." ControlToValidate="txtTitle" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                   <tr>
                    <td>عنوان انگلیسی</td>
                    <td>
                        <asp:TextBox ID="txtEN_Title" runat="server" CssClass="form-control txt"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="عنوان انگلیسی را وارد نمایید." ControlToValidate="txtEN_Title" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                 <tr>
                    <td>عنوان روسی</td>
                    <td>
                        <asp:TextBox ID="txtRus_Title" runat="server" CssClass="form-control txt"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="عنوان روسی را وارد نمایید." ControlToValidate="txtRus_Title" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                 <tr>
                    <td>عنوان چینی</td>
                    <td>
                        <asp:TextBox ID="txtChina_Title" runat="server" CssClass="form-control txt"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="عنوان چینی را وارد نمایید." ControlToValidate="txtChina_Title" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td>نام سیستمی</td>
                    <td>

                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control txt"></asp:TextBox>
                        <dfn style="color:#4C8BF5;">در نام سیستمی از space استفاده نکنید.</dfn>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="نام سیستمی را وارد نمایید." ControlToValidate="txtName" ForeColor="Red"></asp:RequiredFieldValidator>
                        <br />
                        <asp:CustomValidator ID="cvName" runat="server" ErrorMessage="عنوان سیستمی معتبر نمی باشد." Display="Dynamic" ControlToValidate="txtName" ForeColor="Red" OnServerValidate="cvName_ServerValidate"></asp:CustomValidator>
                    </td>
                </tr>

              
               

                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="ثبت" OnClick="btnSave_Click" CssClass="btn btn-success" />
                        <asp:Button ID="btnCancel" runat="server" Text="بازگشت" OnClick="btnCancel_Click" CssClass="btn btn-warning" CausesValidation="false" />
                    </td>
                </tr>

            </table>
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
