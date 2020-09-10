<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="DiscountSetting.aspx.cs" Inherits="MashadCarpet.Admin.DiscountSetting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
       #ContentPlaceHolder1_chlSizes tr{
            display:inline;
            margin-left:40px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:MultiView ID="mvSetting" runat="server">
        <asp:View ID="vwList" runat="server">
            <asp:Button ID="btnAdd" runat="server" Text="افزودن" CssClass="btn btn-success" OnClick="btnAdd_Click" />
            <asp:Button ID="btnReturn" runat="server" Text="بازگشت" CssClass="btn btn-warning" OnClick="btnReturn_Click" CausesValidation="false" />
        
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
                    <asp:BoundField DataField="Title" HeaderText="عنوان" />
                    <asp:BoundField DataField="DiscountPercent" HeaderText="درصد تخفیف" />

                    
                    <asp:BoundField DataField="SubmitDate" HeaderText="تاریخ" DataFormatString="{0:d}" />
                 
                    <asp:TemplateField HeaderText="توضیحات">

                        <ItemTemplate>
                            <i class="fa fa-edit"></i>
                            <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("DiscountID") %>' CommandName="DoEdit" CssClass="lb">ویرایش</asp:LinkButton>
                            <br />
                            <i class="fa fa-times"></i>
                            <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%# Eval("DiscountID") %>' CommandName="DoDelete" CssClass="lb">حذف</asp:LinkButton>
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
                        <td>درصد:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPercent" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic" 
                                       ErrorMessage="مقدار عددی وارد نمایید" ControlToValidate="txtPercent" ValidationExpression="^[0-9]*$">*</asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" runat="server" ErrorMessage="درصد تخفیف را وارد نمایید." ControlToValidate="txtPercent" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
 

                   
                    <tr>
                        <td>سایزها :
                        </td>
                        <td>
                            <asp:CheckBoxList ID="chlSizes" runat="server"></asp:CheckBoxList>
                                <asp:CustomValidator ID="cvSizes" runat="server" ErrorMessage="یکی از سایزهای انتخابی شما در تخفیف فعال دیگری مورد استفاده قرار گرفته است. (هر سایز فقط می تواند در یک تخفیف لحاظ گردد)." Display="Dynamic" ForeColor="Red" OnServerValidate="cvSizes_ServerValidate">*</asp:CustomValidator>

                        </td>
                    </tr>
                    

                    <tr>
                        <td>تخفیف فعال می باشد</td>
                        <td>
                            <asp:CheckBox ID="chbActive" runat="server" />
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
