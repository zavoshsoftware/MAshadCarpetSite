<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="BlogsSetting.aspx.cs" Inherits="MashadCarpet.Admin.BlogsSetting" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvSetting" runat="server">
        <asp:View ID="vwList" runat="server">
            <asp:Button ID="btnAdd" runat="server" Text="افزودن" CssClass="btn btn-success" OnClick="btnAdd_Click" />
            <asp:Button ID="btnReturn" runat="server" Text="بازگشت" CssClass="btn btn-warning" OnClick="btnReturn_Click" CausesValidation="false" />
         <a href="BlogsSetting.aspx?type=recycle" class="btn btn-default">مشاهده مطالب پاک شده</a>
               <br />
            <br />
               <asp:Panel ID="pnlEmptyForm" runat="server" Visible="false">
                <p class="alert alert-danger pnlEmptyForm">اطلاعاتی جهت نمایش موجود نمی باشد.</p>
            </asp:Panel>
            <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="alert alert-success pnlEmptyForm">
                اطلاعات با موفقیت تغییر یافت.

            </asp:Panel>

            <asp:GridView ID="grdTable" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="grdTable_RowCommand" Width="100%">

                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="BlogTitle" HeaderText="عنوان" />
                  

                    <asp:BoundField DataField="BlogName" HeaderText="عنوان سیستمی" />
                    <asp:TemplateField HeaderText="تصویر">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("BlogImage","~/Uploads/Blogs/{0}") %>' Width="100px" Height="100px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SubmitDate" HeaderText="تاریخ" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="BlogGroupTitle" HeaderText="گروه " />

                    <asp:BoundField DataField="VisitCounts" HeaderText="تعداد بازدید" />
                    <asp:TemplateField HeaderText="توضیحات">

                        <ItemTemplate>
                            <i class="fa fa-edit"></i>
                            <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("BlogID") %>' CommandName="DoEdit" CssClass="lb">ویرایش</asp:LinkButton>
                            <br />
                            <i class="fa fa-times"></i>
                            <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%# Eval("BlogID") %>' CommandName="DoDelete" CssClass="lb">حذف</asp:LinkButton>
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
            <div class="ad-div">
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
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" />
                <table>
                    <tr>
                        <td>عنوان:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>

                            <asp:RequiredFieldValidator Display="Dynamic" ID="rfvTitle" runat="server" ErrorMessage="عنوان را وارد نمایید." ControlToValidate="txtTitle" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>عنوان انگلیسی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtEN_Title" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>

                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" runat="server" ErrorMessage="عنوان انگلیسی را وارد نمایید." ControlToValidate="txtEN_Title" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>

                     <tr>
                        <td>عنوان روسی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRus_Title" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>

                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3" runat="server" ErrorMessage="عنوان روسی را وارد نمایید." ControlToValidate="txtRus_Title" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>

                     <tr>
                        <td>عنوان چینی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtChina_Title" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>

                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator4" runat="server" ErrorMessage="عنوان چینی را وارد نمایید." ControlToValidate="txtChina_Title" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                      <tr>
                        <td>عنوان کوتاه:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSummery" runat="server" CssClass="form-control" TextMode="MultiLine" Width="300px"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td>عنوان کوتاه انگلیسی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSummery_En" runat="server" CssClass="form-control" TextMode="MultiLine" Width="300px"></asp:TextBox>

                        </td>
                    </tr>

                     <tr>
                        <td>عنوان کوتاه روسی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSummery_Chine" runat="server" CssClass="form-control" TextMode="MultiLine" Width="300px"></asp:TextBox>

                        </td>
                    </tr>

                     <tr>
                        <td>عنوان کوتاه چینی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSummery_Rus" runat="server" CssClass="form-control" TextMode="MultiLine" Width="300px"></asp:TextBox>

                        </td>
                    </tr>
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    


                    <tr>
                        <td>عنوان سیستمی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server" ErrorMessage="عنوان سیستمی را وارد نمایید." ControlToValidate="txtName" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvName" runat="server" ErrorMessage="عنوان سیستمی معتبر نمی باشد." Display="Dynamic" ControlToValidate="txtName" ForeColor="Red" OnServerValidate="cvName_ServerValidate">*</asp:CustomValidator>
                        </td>
                    </tr>

                    <tr>
                        <td>تصویر:
                        </td>
                        <td>

                            <table>
                                <tr>
                                    <td>
                                        <asp:FileUpload ID="fuImg" runat="server" CssClass="form-control txt" /></td>
                                    <td>
                                        <asp:Image ID="imgEditImages" runat="server" Height="150px" />

                                    </td>

                                </tr>
                            </table>

                        </td>
                    </tr>

                    <asp:Panel ID="pnlGroup" runat="server">
                    <tr>
                        <td>گروه :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlGroup" runat="server"  CssClass="form-control txt" Height="40" ></asp:DropDownList>
                            <asp:CustomValidator ID="cvGroup" runat="server" ErrorMessage="گروه را وارد نمایید." Display="Dynamic" ControlToValidate="txtName" ForeColor="Red" OnServerValidate="cvGroup_ServerValidate">*</asp:CustomValidator>

                        </td>
                    </tr>
                    </asp:Panel>

                    <tr>
                        <td>توضیحات</td>
                        <td>
                            <telerik:RadEditor ID="reDesc" runat="server"></telerik:RadEditor>
                        </td>
                    </tr>
                     <tr>
                        <td>توضیحات انگلیسی</td>
                        <td>
                            <telerik:RadEditor ID="reEN_Desc" runat="server"></telerik:RadEditor>
                        </td>
                    </tr>
                     <tr>
                        <td>توضیحات روسی</td>
                        <td>
                            <telerik:RadEditor ID="reRus_Desc" runat="server"></telerik:RadEditor>
                        </td>
                    </tr>
                     <tr>
                        <td>توضیحات چینی</td>
                        <td>
                            <telerik:RadEditor ID="reChina_Desc" runat="server"></telerik:RadEditor>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="ثبت" CssClass="btn btn-success" OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="انصراف" CssClass="btn btn-danger" OnClick="btnCancel_Click" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>
        <asp:View ID="vwDelete" runat="server">
            آیا مایل به حذف 
            <asp:Label ID="lblDelete" runat="server" Text="Label"></asp:Label>
            هستید؟
            <asp:Button ID="btnYes" runat="server" Text="بلی" OnClick="btnYes_Click" CssClass="btn btn-success" />
            <asp:Button ID="btnNo" runat="server" Text="خیر" OnClick="btnNo_Click" CssClass="btn btn-warning" />

        </asp:View>
    </asp:MultiView>
</asp:Content>
