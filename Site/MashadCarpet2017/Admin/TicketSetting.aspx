<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="TicketSetting.aspx.cs" Inherits="MashadCarpet.Admin.TicketSetting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:MultiView ID="mvSetting" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwList" runat="server">
            <asp:Button ID="btnAdd" runat="server" Text="افزودن" OnClick="btnAdd_Click" CssClass="btn btn-success" />
            <br />
            <br />
             <asp:Panel ID="pnlEmptyForm" runat="server" Visible="false">
                <p class="alert alert-danger pnlEmptyForm">اطلاعاتی جهت نمایش موجود نمی باشد.</p>
            </asp:Panel>
                 <asp:Panel ID="pnlUserInfo" runat="server" Visible="false">
                <div style="width: 100%; height: 460px; padding: 10px; color: #808080 !important;text-align:center ">
                    <table class="table" style="width:500px;margin:0 auto;font-weight:700;">
                        <tr>
                            <td colspan="2"><h3>اطلاعات کاربر</h3></td>
                        </tr>
                        <tr>
                            <td>نام</td>
                            <td>
                                <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>نام خانوادگی</td>
                            <td>
                                <asp:Label ID="lblFamily" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td>آدرس1</td>
                            <td>
                                <asp:Label ID="lblAddress1" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                          <tr>
                            <td>آدرس2</td>
                            <td>
                                <asp:Label ID="lblAddress2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                          <tr>
                            <td>استان</td>
                            <td>
                                <asp:Label ID="lblProvince" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td>شهر</td>
                            <td>
                                <asp:Label ID="lblCity" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                          <tr>
                            <td>کد پستی</td>
                            <td>
                                <asp:Label ID="lblPostalcode" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td>تلفن</td>
                            <td>
                                <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                          <tr>
                            <td>موبایل</td>
                            <td>
                                <asp:Label ID="lblMobile" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                          <tr>
                            <td>ایمیل</td>
                            <td>
                                <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:GridView ID="GrdTable" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GrdTable_RowCommand" OnDataBound="GrdTable_DataBound">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="ردیف">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="نام کاربر">

                        <ItemTemplate>
                            <asp:LinkButton ID="lbUserInfo" runat="server" CommandArgument='<%# Eval("TicketID") %>' CommandName="UserInfo" CssClass="lb">   <%# Eval("UserName") %></asp:LinkButton>
                       
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:BoundField DataField="TicketSubject" HeaderText="عنوان" />
                    <%--<asp:BoundField DataField="TicketPriority" HeaderText="اولویت" />--%>
                 <%--   <asp:TemplateField HeaderText="اولویت">
                        <ItemTemplate>
                             <asp:Label ID="lblPriority" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <%--<asp:BoundField DataField="TicketGroupTitle" HeaderText="گروه تیکت" />--%>
                    <asp:TemplateField HeaderText="حالت">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfTicketID" runat="server" Value='<%# Eval("TicketID") %>' />
                            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="TicketDate" HeaderText="تاریخ ثبت" DataFormatString="{0:d}" />
                    <asp:TemplateField HeaderText="تنظیمات">
                        <ItemTemplate>
                            <i class="fa fa-edit"></i>
                            <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("TicketID") %>' CommandName="DoEdit" CssClass="lb">ویرایش</asp:LinkButton>

                            <br />
                            <i class="fa fa-edit"></i>
                            <asp:LinkButton ID="lbAnswers" runat="server" CommandArgument='<%# Eval("TicketID") %>' CommandName="ShowAnswer" CssClass="lb">پاسخ ها</asp:LinkButton>

                            <br />
                            <i class="fa fa-times"></i>
                            <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%# Eval("TicketID") %>' CommandName="DoDelete" CssClass="lb">حذف</asp:LinkButton>
                            <br />
                            <asp:Panel ID="pnlClose" runat="server" Visible="false"> <i class="fa fa-times"></i>
                            <asp:LinkButton ID="lbClose" runat="server" CommandArgument='<%# Eval("TicketID") %>' CommandName="DoClose" CssClass="lb">بستن</asp:LinkButton></asp:Panel>
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
                <table>
                    <tr>
                        <td>کاربر:</td>
                        <td>
                            <asp:DropDownList ID="ddlUser" runat="server" CssClass="form-control"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td >عنوان:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="rfvTitle" runat="server" ErrorMessage="عنوان را وارد نمایید." ValidationGroup="aaa" ControlToValidate="txtSubject" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                <%--    <tr>
                        <td>گروه تیکت:</td>
                        <td>
                            <asp:DropDownList ID="ddlTicketGroup" runat="server" CssClass="form-control"></asp:DropDownList>
                        </td>
                    </tr>--%>
                   
                    <tr>
                        <td>متن:
                        </td>
                        <td>
                            <%--<asp:RequiredFieldValidator ID="rfvText" runat="server" ErrorMessage="متن را وارد نمایید." ValidationGroup="aaa" ControlToValidate="reMessage" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                            <asp:TextBox ID="txtMessage" Width="550px" Height="250px" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <%-- <tr>
                        <td>حالت:</td>
                        <td>
                            <asp:DropDownList ID="ddlStatus" runat="server">
                                <asp:ListItem Value="1">در انتظار پاسخ</asp:ListItem>
                                <asp:ListItem Value="2">پاسخ داده شده</asp:ListItem>
                                <asp:ListItem Value="3">پاسخ مشتری</asp:ListItem>
                                <asp:ListItem Value="4">بسته شده</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>--%>
                    <%--   <tr>
                        <td>تصویر:
                        </td>
                        <td>
                            <asp:FileUpload ID="fuImage" runat="server" />
                        </td>
                    </tr>--%>

                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnsave" runat="server" Text="ثبت" OnClick="btnsave_Click" CssClass="btn btn-success" ValidationGroup="aaa" />
                            <asp:Button ID="btnReturn" runat="server" Text="انصراف" OnClick="btnReturn_Click" CssClass="btn btn-danger" CausesValidation="false" />
                        </td>
                    </tr>
                </table>

            </div>
            <br />


        </asp:View>


        <asp:View ID="vwDelete" runat="server">
            آیا مطمئن به حذف 
            <asp:Label ID="lblDelete" runat="server" Text=""></asp:Label>

            هستید؟<br />
            <asp:Button ID="btnYes" runat="server" Text="بلی" OnClick="btnYes_Click" CssClass="btn btn-success" />
            <asp:Button ID="btnNo" runat="server" Text="خیر" OnClick="btnNo_Click" CssClass="btn btn-warning" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
