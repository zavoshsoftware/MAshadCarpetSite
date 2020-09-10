<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="OrderListSetting.aspx.cs" Inherits="MashadCarpet.Admin.OrderListSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlUserInfo" runat="server" Visible="false">
        <div style="width: 100%; height: 460px; padding: 10px; color: #808080 !important; text-align: center" id="userinfo">
            <table class="table pull-right" style="width: 500px; margin: 0 auto; font-weight: 700;">
                <tr>
                    <td colspan="2">
                        <h3>اطلاعات کاربر</h3>
                    </td>
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

            <table class="table pull-left" style="width: 500px; margin: 0 auto; font-weight: 700;">
                <tr>
                    <td colspan="2">
                        <h3>اطلاعات گیرنده سفارش</h3>
                    </td>
                </tr>
                <tr>
                    <td>نام</td>
                    <td>
                        <asp:Label ID="lblRecipientName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>تلفن</td>
                    <td>
                        <asp:Label ID="lblRecipientPhone" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>آدرس</td>
                    <td>
                        <asp:Label ID="lblRecipientAddress" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:MultiView ID="mvSetting" runat="server">
        <asp:View ID="vwList" runat="server">
            <asp:Panel ID="pnlEmptyForm" runat="server" Visible="false">
                <p class="alert alert-danger pnlEmptyForm">اطلاعاتی جهت نمایش موجود نمی باشد.</p>
            </asp:Panel>
            
            
            <div class="row" style="padding-bottom: 10px;">
                <div class="col-md-9">
                    <asp:TextBox ID="txtSearch" placeholder="شماره فاکتور" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_OnClick" Text="جست و جو"  CssClass="btn btn-success"/>
                </div>
                <div class="col-md-3 text-left">شماره فاکتور:</div>
             
            </div>

            <asp:GridView ID="grdTable" runat="server" AutoGenerateColumns="False" CellPadding="4" 
                ForeColor="#333333" GridLines="None" Width="100%" OnRowCommand="grdTable_RowCommand" 
                  AllowPaging="True"
                 PageSize="15" OnPageIndexChanging="grdTable_OnPageIndexChanging">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
         <asp:BoundField DataField="OrderShowID" HeaderText="شماره فاکتور" />

                    <asp:TemplateField HeaderText="نام کاربر">

                        <ItemTemplate>
                            <asp:LinkButton ID="lbUserInfo" runat="server" CommandArgument='<%# Eval("OrderID") %>' CommandName="UserInfo" CssClass="lb">   <%# Eval("UserName") %></asp:LinkButton>

                        </ItemTemplate>

                    </asp:TemplateField>
               

                    <asp:BoundField DataField="SubmitDate" HeaderText="تاریخ فاکتور" DataFormatString="{0:d}" />
                    <asp:CheckBoxField DataField="IsFinalized" HeaderText="نهایی شده؟" />
                    <asp:CheckBoxField DataField="IsPaid" HeaderText="پرداخت شده؟" />
                     <asp:BoundField DataField="PaymentDate" HeaderText="تاریخ پرداخت" DataFormatString="{0:d}" />
                <%--    <asp:TemplateField HeaderText="توضیحات">

                        <ItemTemplate>
                            <asp:HiddenField ID="hfOrderID" runat="server" Value='<%# Eval("OrderID") %>' />
                            <asp:Label ID="lblPaymentWay" runat="server" Text=""></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="توضیحات">

                        <ItemTemplate>

                            <i class="fa fa-eye"></i>
                            <asp:LinkButton ID="lbDetails" runat="server" CommandArgument='<%# Eval("OrderID") %>' CommandName="Show" CssClass="lb">مشاهده</asp:LinkButton>
                            <br />
                            <i class="fa fa-edit"></i>
                            <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("OrderID") %>' CommandName="DoEdit" CssClass="lb">ویرایش</asp:LinkButton>
                            <br />
                            <i class="fa fa-edit"></i>
                            <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%# Eval("OrderID") %>' CommandName="DoDelete" CssClass="lb">حذف</asp:LinkButton>
                              <br />
                            <i class="fa fa-download"></i>
                            <asp:LinkButton ID="lbDownloadFactors" runat="server" CommandArgument='<%# Eval("OrderID") %>' CommandName="downloadFactor" CssClass="lb">دانلود فاکتور</asp:LinkButton>
                            <br />
                            <i class="fa fa-check"></i>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("OrderID") %>' CommandName="DoCheck" CssClass="lb">بررسی شد</asp:LinkButton>

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
        <asp:View ID="vwEditOrder" runat="server">
            <table>

                <tr>
                    <td>نام گیرنده سقارش
                    </td>
                    <td>
                        <asp:TextBox ID="txtRecipientName" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>تلفن گیرنده سقارش
                    </td>
                    <td>
                        <asp:TextBox ID="txtRecipientPhone" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>آدرس گیرنده سقارش
                    </td>
                    <td>
                        <asp:TextBox ID="txtRecipientAddress" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>روش پرداخت
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPaymentWay" runat="server" CssClass="form-control" Width="300px" Height="40">
                            <asp:ListItem Value="1">پرداخت آنلاین</asp:ListItem>
                            <asp:ListItem Value="2">پرداخت از طریق بانک</asp:ListItem>
                        </asp:DropDownList>

                    </td>
                </tr>
                <tr>
                    <td>نهایی شده؟
                    </td>
                    <td>
                        <asp:CheckBox ID="cbISFinalize" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>پرداخت شده؟
                    </td>
                    <td>
                        <asp:CheckBox ID="cbIsPaid" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td>وضعیت سفارش
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="form-control" Width="300px" Height="40"></asp:DropDownList>
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
        </asp:View>
        <asp:View ID="vwDeleteOrder" runat="server">
            آیا مطمئن به حذف 
      
            هستید؟

        <asp:Button ID="btnAgree" runat="server" Text="بلی" OnClick="btnAgree_Click" CssClass="btn btn-success" />
            <asp:Button ID="btnDeny" runat="server" Text="خیر" OnClick="btnDeny_Click" CssClass="btn btn-danger" />
        </asp:View>
        <asp:View ID="vwOrder" runat="server">
            <asp:Button ID="btnAdd" runat="server" Text="افزودن" CssClass="btn btn-success" OnClick="btnAdd_Click" />
            <asp:Button ID="btnBack" runat="server" Text="برگشت" CssClass="btn btn-warning" OnClick="btnBack_Click" /><br />
            <br />
            <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
              <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnUserInfo" runat="server" Text="مشاهده اطلاعات کاربر" CssClass="btn btn-info" OnClick="btnUserInfo_Click" />
            <br />
            <br />
            <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" OnRowCommand="grdDetails_RowCommand" OnRowDataBound="grdDetails_RowDataBound">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="ProductTitle" HeaderText="نام محصول" />
                    <asp:BoundField DataField="Count" HeaderText="تعداد" />
                    <asp:BoundField DataField="SizeTitle" HeaderText="اندازه" />
                    <asp:BoundField DataField="ColorTitle" HeaderText="رنگ" />
                    <asp:BoundField DataField="ProductPrice" HeaderText="قیمت فی" DataFormatString="{0:N0}" />
                    <%--<asp:BoundField DataField="TotalPrice" HeaderText="قیمت کل" DataFormatString="{0:N0}" />--%>
                       <asp:TemplateField HeaderText="مبلغ فی">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfOrderDetailID" Value='<%# Eval("OrderDetailID") %>' runat="server" />
                                        <asp:Literal ID="ltPriceBeforDescount" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="مبلغ فی (با تخفیف)">
                                    <ItemTemplate>
                                        <asp:Literal ID="ltPrice" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                     

                                <asp:TemplateField HeaderText="جمع مبلغ (تومان)">
                                    <ItemTemplate>
                                        <asp:Literal ID="ltTotalPrice" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>

                    <asp:TemplateField HeaderText="توضیحات">

                        <ItemTemplate>


                            <i class="fa fa-edit"></i>
                            <asp:LinkButton ID="lbEdit" runat="server" CommandArgument='<%# Eval("OrderDetailID") %>' CommandName="DoEdit" CssClass="lb">ویرایش</asp:LinkButton>
                            <br />
                            <i class="fa fa-edit"></i>
                            <asp:LinkButton ID="lbDelete" runat="server" CommandArgument='<%# Eval("OrderDetailID") %>' CommandName="DoDelete" CssClass="lb">حذف</asp:LinkButton>

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
        <asp:View ID="vwEditDetails" runat="server">
            <table>
                <tr>
                    <td>محصول
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProducts" runat="server" OnSelectedIndexChanged="ddlProducts_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" Width="300px" Height="40"></asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td>رنگ
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlColors" runat="server" CssClass="form-control" Width="300px" Height="40" AutoPostBack="true" OnSelectedIndexChanged="ddlColors_OnSelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>سایز
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSize" runat="server" CssClass="form-control" Width="300px" Height="40"></asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td>تعداد
                    </td>
                    <td>
                        <asp:TextBox ID="txtCount" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSaveDetails" runat="server" Text="ثبت" OnClick="btnSaveDetails_Click" CssClass="btn btn-success" />
                        <asp:Button ID="btnCancelDetails" runat="server" Text="انصراف" OnClick="btnCancelDetails_Click" CssClass="btn btn-danger" />
                    </td>
                </tr>

            </table>
        </asp:View>
        <asp:View ID="vwDeleteDetails" runat="server">
            آیا مطمئن به حذف 
        <asp:Label ID="lblDelete" runat="server" Text=""></asp:Label>
            هستید؟

        <asp:Button ID="btnDelDetails" runat="server" Text="بلی" OnClick="btnDelDetails_Click" CssClass="btn btn-success" />
            <asp:Button ID="btnDisAgreeDel" runat="server" Text="خیر" OnClick="btnDisAgreeDel_Click" CssClass="btn btn-danger" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
