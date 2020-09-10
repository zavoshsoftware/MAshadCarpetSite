<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="TextSetting.aspx.cs" Inherits="MashadCarpet.Admin.TextSetting" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvSetting" runat="server">
        <asp:View ID="vwList" runat="server">
            <asp:Button ID="btnAdd" runat="server" Text="افزودن" OnClick="btnAdd_Click" CssClass="btn btn-success" Visible="false" />
            <asp:Button ID="btnReturn" runat="server" Text="بازگشت" OnClick="btnReturn_Click" CssClass="btn btn-warning" />
            <br />
            <br />
            <asp:GridView ID="grdTable" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" OnRowCommand="grdTable_RowCommand" OnRowDataBound="grdTable_RowDataBound">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="ردیف">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TextTitle" HeaderText="عنوان" />
                    <asp:BoundField DataField="TextName" HeaderText="عنوان سیستمی" />
                    <asp:BoundField DataField="EN_TextTitle" HeaderText="عنوان انگلیسی" />
                    <asp:BoundField DataField="Rus_TextTitle" HeaderText="عنوان روسی" />
                    <asp:BoundField DataField="China_TextTitle" HeaderText="عنوان چینی" />
                    <asp:TemplateField HeaderText="تنظیمات">
                        <ItemTemplate>
                            <i class="fa fa-edit"></i>
                            <asp:LinkButton ID="lbEdit" runat="server" CommandName="DoEdit" CommandArgument='<%# Eval("TextID") %>'>ویرایش</asp:LinkButton>
                            <br />
                              <asp:Panel ID="pnlAbout" runat="server" Visible="false">
                                <i class="fa fa-trash"></i>
                                <asp:LinkButton ID="lbAbout" runat="server" CommandName="ManageAbout" CommandArgument='<%# Eval("GroupID") %>'>مدیریت متن ها</asp:LinkButton>
                            </asp:Panel>
                            <asp:Panel ID="pnlDelete" runat="server" Visible="false">
                                <i class="fa fa-trash"></i>
                                <asp:LinkButton ID="lbDelete" runat="server" CommandName="DoDelete" CommandArgument='<%# Eval("TextID") %>'>حذف</asp:LinkButton>
                            </asp:Panel>
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
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            </telerik:RadAjaxManager>
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server" EnableTheming="True">
                <Scripts>
                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
                </Scripts>
            </telerik:RadScriptManager>
            <div class="ad-div">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" />
                <table>
                    <tr>
                        <td>عنوان</td>
                        <td>

                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form_control" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle" ForeColor="Red" ErrorMessage="عنوان را وارد نمایید.">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>عنوان سیستمی</td>
                        <td>

                            <asp:TextBox ID="txtName" runat="server" CssClass="form_control" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName" ForeColor="Red" ErrorMessage="عنوان سیستمی را وارد نمایید.">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="عنوان سیستمی معتبر نمی باشد." Display="Dynamic" ForeColor="Red" ControlToValidate="txtName" OnServerValidate="CustomValidator1_ServerValidate">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>عنوان انگلیسی</td>
                        <td>

                            <asp:TextBox ID="txtEN_Title" runat="server" CssClass="form_control" Width="300px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>عنوان روسی</td>
                        <td>

                            <asp:TextBox ID="txtRusTitle" runat="server" CssClass="form_control" Width="300px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>عنوان چینی</td>
                        <td>

                            <asp:TextBox ID="txtChinaTitle" runat="server" CssClass="form_control" Width="300px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>تصویر:
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:FileUpload ID="fuImg" runat="server" /></td>
                                    <td>
                                        <asp:Image ID="imgEditImages" runat="server" Height="150px" />

                                    </td>

                                </tr>
                            </table>

                        </td>
                    </tr>
                    <tr>
                        <td>متن</td>
                        <td>

                            <telerik:RadEditor ID="reDesc" runat="server" 
                                ImageManager-EnableImageEditor="true"
                                ImageManager-UploadPaths="~/Uploads/TextImages/"
                                ImageManager-ViewPaths="~/Uploads/TextImages/"></telerik:RadEditor>
                        </td>
                    </tr>
                    <tr>
                        <td>متن انگلیسی</td>
                        <td>

                            <telerik:RadEditor ID="reEN_Desc" runat="server" 
                                ImageManager-EnableImageEditor="true"
                                ImageManager-UploadPaths="~/Uploads/TextImages/"
                                ImageManager-ViewPaths="~/Uploads/TextImages/"></telerik:RadEditor>
                        </td>
                    </tr>

                      <tr>
                        <td>متن روسی</td>
                        <td>

                            <telerik:RadEditor ID="reRus_Desc" runat="server"
                                ImageManager-EnableImageEditor="true"
                                ImageManager-UploadPaths="~/Uploads/TextImages/"
                                ImageManager-ViewPaths="~/Uploads/TextImages/"></telerik:RadEditor>
                        </td>
                    </tr>

                      <tr>
                        <td>متن چینی</td>
                        <td>

                            <telerik:RadEditor ID="reChina_Text" runat="server"
                                ImageManager-EnableImageEditor="true"
                                ImageManager-UploadPaths="~/Uploads/TextImages/"
                                ImageManager-ViewPaths="~/Uploads/TextImages/"></telerik:RadEditor>
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
            </div>
        </asp:View>
        <asp:View ID="vwDelete" runat="server">
            آیا مایل به حذف 
            <asp:Label ID="lblDelete" runat="server" Text="Label"></asp:Label>
            هستید؟
            <asp:Button ID="btnYes" runat="server" Text="بلی" OnClick="btnAgree_Click" CssClass="btn btn-success" />
            <asp:Button ID="btnNo" runat="server" Text="خیر" OnClick="btnDeny_Click" CssClass="btn btn-warning" />

        </asp:View>
    </asp:MultiView>
</asp:Content>
