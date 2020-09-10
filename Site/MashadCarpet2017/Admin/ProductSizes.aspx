<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ProductSizes.aspx.cs" Inherits="MashadCarpet.Admin.ProductSizes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvSetting" runat="server">
        <asp:View ID="vwList" runat="server">
            <asp:Button ID="btnAdd" runat="server" Text="افزودن" CssClass="btn btn-success" OnClick="btnAdd_Click" />
            <asp:Button ID="btnReturn" runat="server" Text="بازگشت" CssClass="btn btn-warning" OnClick="btnReturn_Click" CausesValidation="false" />
            <br />
            <br />
            <asp:Panel ID="pnlEmptyForm" runat="server" Visible="false">
                <p class="alert alert-danger pnlEmptyForm">اطلاعاتی جهت نمایش موجود نمی باشد.</p>
            </asp:Panel>
            <div>
                <span>عنوان محصول: </span>
                <asp:Label ID="lblProductTitle" runat="server" Text=""></asp:Label>
             
                <span style="padding-right: 40px;">شماره نقشه: </span>
                <asp:Label ID="lblDesignNo" runat="server" Text=""></asp:Label>



                  <span style="padding-right: 40px;">رنگ: </span>
                <asp:Label ID="lblColorTitle" runat="server" Text=""></asp:Label>



                   <span style="padding-right: 40px;">تصویر:
                </span>
                <asp:Image ID="ImgProduct" runat="server" Width="100" Height="150" />


            </div>
            <br />
            <br />
            <asp:GridView ID="grdTable" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="grdTable_RowCommand" Width="100%" AllowPaging="True" PageSize="15" OnPageIndexChanging="grdTable_PageIndexChanging">

                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
              
                      <asp:BoundField DataField="SizeTitle" HeaderText="سایز" />
                        <asp:BoundField DataField="ProductPrice" HeaderText="قیمت" DataFormatString="{0:N0}"/> 
            <asp:CheckBoxField DataField="IsAvailable" HeaderText="نمایش" />
                        <asp:BoundField DataField="Stock" HeaderText="موجودی" /> 
                     

                    <asp:TemplateField HeaderText="توضیحات">

                        <ItemTemplate>
                           
                            <i class="fa fa-edit"></i>
                            <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("ProductColorSizeID") %>' CommandName="DoEdit" CssClass="lb">ویرایش</asp:LinkButton>
                            <br />
                            <i class="fa fa-times"></i>
                            <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%# Eval("ProductColorSizeID") %>' CommandName="DoDelete" CssClass="lb">حذف</asp:LinkButton>
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
                        <td>سایز:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSizes" runat="server" CssClass="form-control" Width="300px" Height="40"></asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator1" OnServerValidate="CustomValidator1_ServerValidate" runat="server" ErrorMessage="سایز را انتخاب کنید" ForeColor="red">*</asp:CustomValidator>

                        </td>
                    </tr>
                     
                    <tr>
                        <td>قیمت:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" Width="300px" Height="40" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="txtPrice" ForeColor="Red"
                            ErrorMessage="قیمت را وارد نمایید.">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
          
                    <tr>
                        <td>موجودی:
                        </td>
                        <td>
                            <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" Width="300px" Height="40"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ControlToValidate="txtStock" ForeColor="Red"
                            ErrorMessage="موجودی را وارد نمایید.">*</asp:RequiredFieldValidator>
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