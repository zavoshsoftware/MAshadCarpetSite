<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="mainProductGroupSetting.aspx.cs" Inherits="MashadCarpet.Admin.mainProductGroupSetting" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
            <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="alert alert-success">
                اطلاعات با موفقیت تغییر یافت.

            </asp:Panel>
            <asp:GridView ID="grdTable" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="grdProductGroup_RowCommand">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="Priority" HeaderText="ردیف" />
                    <asp:BoundField DataField="ProductGroupTitle" HeaderText="عنوان" />
                    <asp:BoundField DataField="EN_ProductGroupTitle" HeaderText="عنوان انگلیسی" />
                    <asp:CheckBoxField DataField="IsAlienCulture" HeaderText="محصول صادراتی؟" />
                    <asp:BoundField DataField="ProductGroupName" HeaderText="عنوان سیستمی" />
                    <asp:TemplateField HeaderText="تصویر">
                        <ItemTemplate>
                            <img src='<%# Eval("ProductGroupImage","/Uploads/ProductGroup/{0}") %>' width="150" height="150" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="تنظیمات">
                        <ItemTemplate>
                            <i class="fa fa-edit"></i>
                            <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("ProductGroupID") %>' CommandName="DoEdit" CssClass="lb">ویرایش</asp:LinkButton>
                            <br />
                            <%--    <i class="fa fa-edit"></i>
                            <asp:LinkButton ID="lbPages" runat="server" CommandArgument='<%# Eval("ProductGroupID") %>' CommandName="ShowProducts" CssClass="lb">مشاهده زیر گروه محصولات</asp:LinkButton>
                            <br />--%>
                            <i class="fa fa-times"></i>
                            <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%# Eval("ProductGroupID") %>' CommandName="DoDelete" CssClass="lb">حذف</asp:LinkButton>
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

        <asp:View ID="vwEdit" runat="server">
            <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
            </telerik:RadStyleSheetManager>
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                <Scripts>
                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
                </Scripts>
            </telerik:RadScriptManager>
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            </telerik:RadAjaxManager>
            <div class="ad-div">

                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" />
                <table>
                    <tr>
                        <td>ردیف</td>
                        <td>
                            <asp:TextBox ID="txtPrio" runat="server" CssClass="form-control txt"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>محصول صادراتی؟</td>
                        <td>
                            <asp:CheckBox ID="chkAlienPro" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>عنوان</td>
                        <td>
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control txt"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ErrorMessage="عنوان را وارد نمایید." ControlToValidate="txtTitle" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>عنوان انگلیسی</td>
                        <td>
                            <asp:TextBox ID="txtEN_Title" runat="server" CssClass="form-control txt"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="عنوان انگلیسی را وارد نمایید." ControlToValidate="txtEN_Title" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>عنوان روسی</td>
                        <td>
                            <asp:TextBox ID="txtRus_Title" runat="server" CssClass="form-control txt"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="عنوان روسی را وارد نمایید." ControlToValidate="txtRus_Title" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td>عنوان چینی</td>
                        <td>
                            <asp:TextBox ID="txtChina_Title" runat="server" CssClass="form-control txt"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="عنوان چینی را وارد نمایید." ControlToValidate="txtChina_Title" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <%--  <tr>
                        <td>رنگ عنوان</td>
                        <td>
                            <asp:TextBox ID="txtcolor" runat="server" CssClass="form-control txt"></asp:TextBox>
                           
                             </td>
                    </tr>
                    <tr>
                        <td>رنگ عنوان2</td>
                        <td>
                            <asp:TextBox ID="txtcolor2" runat="server" CssClass="form-control txt"></asp:TextBox>
                                </td>
                    </tr>--%>
                    <tr>
                        <td>نام سیستمی</td>
                        <td>

                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control txt"></asp:TextBox>
                            <dfn style="color: #4C8BF5;">نام سیستمی باید طبق الگوی رو به رو باشد: شانه-رنگ-تراکم</dfn>



                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="نام سیستمی را وارد نمایید." ControlToValidate="txtName" ForeColor="Red">*</asp:RequiredFieldValidator>

                            <asp:CustomValidator ID="cvName" runat="server" ErrorMessage="عنوان سیستمی معتبر نمی باشد. عنوان سیستمی نباید تکراری باشد" Display="Dynamic" ControlToValidate="txtName" ForeColor="Red" OnServerValidate="cvName_ServerValidate">*</asp:CustomValidator>
                        </td>
                    </tr>

                    <tr>
                        <td>تصویر</td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:FileUpload ID="fuImage" runat="server" CssClass="form-control txt" /></td>
                                    <td>
                                        <asp:Image ID="imgEditImages" runat="server" Height="150px" />

                                    </td>

                                </tr>
                            </table>

                        </td>
                    </tr>
                    <tr>
                        <td>تصویر اسلایدر</td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:FileUpload ID="fuImage_Slider" runat="server" CssClass="form-control txt" /></td>
                                    <td>
                                        <asp:Image ID="imgEditImages_Slider" runat="server" Height="150px" />

                                    </td>

                                </tr>
                            </table>

                        </td>
                    </tr>
                    <tr>
                        <td>توضیحات</td>
                        <td>
                            <telerik:RadEditor runat="server" ID="reDesc"></telerik:RadEditor>
                        </td>
                    </tr>
                    <tr>
                        <td>توضیحات انگیسی</td>
                        <td>
                            <telerik:RadEditor runat="server" ID="reEN_Desc"></telerik:RadEditor>
                        </td>
                    </tr>
                    <tr>
                        <td>توضیحات روسی</td>
                        <td>
                            <telerik:RadEditor runat="server" ID="reRus_Desc"></telerik:RadEditor>
                        </td>
                    </tr>
                    <tr>
                        <td>توضیحات چینی</td>
                        <td>
                            <telerik:RadEditor runat="server" ID="reChina_Desc"></telerik:RadEditor>
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
