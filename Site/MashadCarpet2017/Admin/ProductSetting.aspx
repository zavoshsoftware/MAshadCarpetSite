<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ProductSetting.aspx.cs" Inherits="MashadCarpet.Admin.ProductSetting" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvSetting" runat="server">
        <asp:View ID="vwList" runat="server">
            <asp:Button ID="btnAdd" runat="server" Text="افزودن" CssClass="btn btn-success" OnClick="btnAdd_Click"  CausesValidation="false"/>
            <asp:Button ID="btnReturn" runat="server" Text="بازگشت" CssClass="btn btn-warning" OnClick="btnReturn_Click" CausesValidation="false" />
            <a href="UploadColorSizeProduct.aspx" class="btn btn-primary">آپلود محصولات</a>
            <%--<a href="UploadAlienColorSizeProduct.aspx" class="btn btn-primary"> آپلود محصولات صادراتی</a>--%>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch" CssClass="inline">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="نام نقشه"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="جست و جو" CssClass="btn btn-primary" OnClick="btnSearch_Click" CausesValidation="false" />
          </asp:Panel>
                  <a href="/Admin/ProductSetting.aspx" class="btn btn-default">نمایش همه</a>
            <br />
            <br />
            <asp:Panel ID="pnlEmptyForm" runat="server" Visible="false">
                <p class="alert alert-danger pnlEmptyForm">اطلاعاتی جهت نمایش موجود نمی باشد.</p>
            </asp:Panel>
            <asp:GridView ID="grdTable" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="None"
                OnRowCommand="grdTable_RowCommand" Width="100%" AllowPaging="True"
                PageSize="15" OnPageIndexChanging="grdTable_PageIndexChanging">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="ProductUniqeCode" HeaderText="کد محصول" />
                    <asp:BoundField DataField="ProductTitle" HeaderText="عنوان" />
                    <asp:CheckBoxField DataField="IsAlienCulture" HeaderText="محصول صادراتی؟" />
                    <asp:BoundField DataField="ProductName" HeaderText="عنوان سیستمی" />
                    <asp:BoundField DataField="ProductGroupTitle" HeaderText="گروه" />
                    <asp:BoundField DataField="Frame" HeaderText="تعداد رنگ" />
                    <asp:BoundField DataField="DesignNo" HeaderText="شماره نقشه" />
                    <asp:BoundField DataField="Collection" HeaderText="خانواده" />
                    <asp:BoundField DataField="PileType" HeaderText="کوالیته" />
                    <asp:TemplateField HeaderText="توضیحات">
                        <ItemTemplate>
                            <i class="fa fa-edit"></i>
                            <asp:LinkButton ID="lbColors" runat="server" CommandArgument='<%# Eval("ProductID") %>' CommandName="Colors" CssClass="lb">رنگ ها</asp:LinkButton>
                            <br />
                            <i class="fa fa-edit"></i>
                            <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("ProductID") %>' CommandName="DoEdit" CssClass="lb">ویرایش</asp:LinkButton>
                            <br />
                            <i class="fa fa-times"></i>
                            <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%# Eval("ProductID") %>' CommandName="DoDelete" CssClass="lb">حذف</asp:LinkButton>
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
                <div>
                    تصویر1:
                    <asp:Image ID="ImgThumb1" runat="server" Width="100" Height="100" Visible="false" />
                    <asp:FileUpload ID="fuThumb1" runat="server" />
                </div>

                <div>
                    تصویر2:
                         <asp:Image ID="ImgThumb2" runat="server" Width="100" Height="100" Visible="false" />
                    <asp:FileUpload ID="fuThumb2" runat="server" />
                </div>

                <table>
                    <tr>
                        <td>کد محصول:
                        </td>
                        <td>
                            <asp:TextBox ID="txtUniqCode" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="عنوان را وارد نمایید." ControlToValidate="txtUniqCode" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvUniqCode" runat="server" ErrorMessage="کد محصول تکراری می باشد." Display="Dynamic" ControlToValidate="txtName" ForeColor="Red" OnServerValidate="cvUniqCode_ServerValidate">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>عنوان:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ErrorMessage="عنوان را وارد نمایید." ControlToValidate="txtTitle" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>محصول صادراتی می باشد؟:
                        </td>
                        <td>
                            <asp:CheckBox ID="chbIsAlien" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td>عنوان انگلیسی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtENTitle" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="عنوان انگلیسی را وارد نمایید." ControlToValidate="txtENTitle" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td>عنوان روسی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRusTitle" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="عنوان روسی را وارد نمایید." ControlToValidate="txtRusTitle" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td>عنوان چینی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtChinaTitle" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="عنوان چینی را وارد نمایید." ControlToValidate="txtChinaTitle" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>


                    <tr>
                        <td>عنوان سیستمی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="عنوان سیستمی را وارد نمایید." ControlToValidate="txtName" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvName" runat="server" ErrorMessage="عنوان سیستمی معتبر نمی باشد." Display="Dynamic" ControlToValidate="txtName" ForeColor="Red" OnServerValidate="cvName_ServerValidate">*</asp:CustomValidator>
                        </td>
                    </tr>


                    <tr>
                        <td>پیشنهاد ویژه؟:
                        </td>
                        <td>
                            <asp:CheckBox ID="cbIsEspecial" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>پرفروش ترین؟:
                        </td>
                        <td>
                            <asp:CheckBox ID="chkMostSell" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>گروه محصول:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProductGroup" runat="server" CssClass="form-control" Width="300px"></asp:DropDownList>

                            <asp:CustomValidator ID="cvProGroup" ForeColor="Red" OnServerValidate="cvProGroup_ServerValidate" runat="server" Display="Dynamic" ErrorMessage="گروه محصول را وارد نمایید">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>شماره نقشه:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDesignNO" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="شماره نقشه را وارد نمایید." ControlToValidate="txtDesignNO" ForeColor="Red">*</asp:RequiredFieldValidator>

                            <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer"
                                ControlToValidate="txtDesignNO" ErrorMessage="فیلد شماره نقشه باید مقدار عددی داشته باشد" ForeColor="Red">*</asp:CompareValidator>
                            <asp:CustomValidator ID="dvDesignNo" runat="server" ErrorMessage="شماره نقشه معتبر نمی باشد." Display="Dynamic" ControlToValidate="txtDesignNO" ForeColor="Red" OnServerValidate="dvDesignNo_ServerValidate">*</asp:CustomValidator>

                        </td>
                    </tr>

                    <tr>
                        <td>تعداد رنگ:
                        </td>
                        <td>
                            <asp:TextBox ID="txtFrame" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="تعداد رنگ را وارد نمایید." ControlToValidate="txtFrame" ForeColor="Red">*</asp:RequiredFieldValidator>


                            <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer"
                                ControlToValidate="txtFrame" ErrorMessage="فیلد تعداد رنگ باید مقدار عددی داشته باشد" ForeColor="Red">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>خانواده:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCollection" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="خانواده را وارد نمایید." ControlToValidate="txtCollection" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>خانواده انگلیسی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtEN_Collection" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="خانواده را وارد نمایید." ControlToValidate="txtCollection" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>

                    <tr>
                        <td>خانواده روسی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRus_Collection" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="خانواده را وارد نمایید." ControlToValidate="txtCollection" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>

                    <tr>
                        <td>خانواده چینی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtChina_Collection" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="خانواده را وارد نمایید." ControlToValidate="txtCollection" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>


                    <tr>
                        <td>کوالیته:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPileType" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="کوالیته را وارد نمایید." ControlToValidate="txtPileType" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>کوالیته انگلیسی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtEN_PileType" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="کوالیته را وارد نمایید." ControlToValidate="txtPileType" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>

                    <tr>
                        <td>کوالیته روسی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRus_PileType" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="کوالیته را وارد نمایید." ControlToValidate="txtPileType" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>

                    <tr>
                        <td>کوالیته چینی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtChina_PileType" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="کوالیته را وارد نمایید." ControlToValidate="txtPileType" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>شانه:
                        </td>
                        <td>
                            <asp:TextBox ID="txtReeds" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="شانه را وارد نمایید." ControlToValidate="txtReeds" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>تراکم:
                        </td>
                        <td>
                            <asp:TextBox ID="txtShots" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="تراکم را وارد نمایید." ControlToValidate="txtShots" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td>سرنخ:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPoints" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="سرنخ را وارد نمایید." ControlToValidate="txtShots" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>گره:
                        </td>
                        <td>
                            <asp:TextBox ID="txtKnots" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="سرنخ را وارد نمایید." ControlToValidate="txtShots" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>توضیحات:
                        </td>
                        <td>
                            <telerik:RadEditor ID="reDesc" runat="server"></telerik:RadEditor>
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
