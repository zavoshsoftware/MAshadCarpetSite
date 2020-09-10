<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bank.aspx.cs" EnableEventValidation="false" Inherits="MashadCarpet.Bank" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="DefaultCss/bootstrap.min.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
        function postRefId(refIdValue) {
            var form = document.createElement("form");
            form.setAttribute("method", "POST");
            form.setAttribute("action", "<%= PgwSite %>");
            form.setAttribute("target", "_self");
            var hiddenField = document.createElement("input");
            hiddenField.setAttribute("name", "RefId");
            hiddenField.setAttribute("value", refIdValue);
            form.appendChild(hiddenField);
            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="display:none">
            <asp:Label ID="lblResCode" runat="server" Text="Label"></asp:Label>
        </div>
        <%--<div class="container">
            <div class="row">
                <asp:Panel ID="Panel1" runat="server"> 
                    <div style="background-color: gray; color: #fff; text-align: center; direction: rtl;">
                        شرکت فرش مشهد (سهامی خاص)
                        <br />
                        فاکتور فروش اینترنتی
                            <br />
                        
                    </div>
                    <div style="direction: rtl; text-align: right; padding:5px 0;">

                        شماره فاکتور:
                        <asp:Label ID="lblFactorID" runat="server" Text="Label"></asp:Label>
                        <br />
                        تاریخ فاکتور:
                        <asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label>
                        <br />
                        کد اقتصادی:۵۷۱۱ـ۹۱۸۷ـ۴۱۱

                    </div>
                    <div style="background-color: gray; color: #fff; text-align: center; direction: rtl;">
                        <b>مشخصات خریدار:
                        </b>
                    </div>

                    <div style="direction: rtl; text-align: right; padding:5px 0;">
                        نام:
                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                        <br />
                        شماره تلفن:
                        <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>
                        <br />
                        شماره موبایل:
                        <asp:Label ID="lblMobile" runat="server" Text=""></asp:Label>
                        <br />
                        ایمیل:
                        <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                        <br />
                         استان:
                        <asp:Label ID="lblProv" runat="server" Text=""></asp:Label>
                        <br /> شهر:
                        <asp:Label ID="lblCity" runat="server" Text=""></asp:Label><br />
                        آدرس 1:
                        <asp:Label ID="lblAddress1" runat="server" Text=""></asp:Label>
                        <br />
                        آدرس 2:
                        <asp:Label ID="lblAddress2" runat="server" Text=""></asp:Label>
                        <br />

                    </div>
                    <div style="background-color: gray; color: #fff; text-align: center; direction: rtl;">
                        <b>توضیحات خرید:
                        </b>
                    </div>
                    <div style="direction: rtl; text-align: right; padding:5px 0;">
                        <asp:Label ID="lblDesc" runat="server" Text=""></asp:Label>
                    </div>

                    <div style="background-color: gray; color: #fff; text-align: center; direction: rtl;">
                        <b>اجناس خریداری شده:
                        </b>
                    </div>
                    <div style="direction: rtl; text-align: right; padding:5px 0;">
                        <asp:GridView ID="grdOrderDetails" Width="100%" runat="server" OnDataBound="grdOrderDetails_DataBound" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="ردیف">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ProductTitle" HeaderText="نام کالا" />
                                <asp:BoundField DataField="ProductGroupTitle" HeaderText="گروه کالا" />
                                <asp:BoundField DataField="Reeds" HeaderText="شانه کالا" />
                                <asp:BoundField DataField="Shots" HeaderText="تراکم کالا" />
                                <asp:BoundField DataField="SizeTitle" HeaderText="سایز کالا" />
                                <asp:BoundField DataField="ColorTitle" HeaderText="رنگ کالا" />

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

                                <asp:BoundField DataField="Count" HeaderText="تعداد کالا" />

                                <asp:TemplateField HeaderText="جمع مبلغ (تومان)">
                                    <ItemTemplate>
                                        <asp:Literal ID="ltTotalPrice" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                       
                        <br /><br />
                        <b>مبلغ قابل پرداخت:</b>
                        <asp:Literal ID="ltPaymentPrice" runat="server"></asp:Literal>
                    </div>
                          <div style="background-color: gray; color: #fff; text-align: center; direction: rtl;">
                        <b>توضیحات:
                        </b>
                    </div>
                    <div style="direction: rtl; text-align: right; padding:5px 0;">
                      
  جهت پاسخگویی به سوالات احتمالی با شماره 22012612 (021)  تماس حاصل فرمایید و یا به بخش سوالات متداول در فروشگاه اینترنتی فرش مشهد به آدرس mashadcarpet.com مراجعه فرمائید.
  درصورت مشاهده هر گونه نقص یا ایراد در فرش دریافتی و یا مغایرت فرش ارسال شده با عکس و مشخصات آن به محض دریافت با شماره 02122012612 جهت تعویض یا مرجوع کردن کالا و بازگشت وجه ، تماس حاصل فرمایید. در این حالت کلیه هزینه های بازپس فرستادن و ارسال مجدد کالا برعهده شرکت فرش مشهد خواهد بود و همچنین در صورت انصراف از خرید ( در صورت عدم استفاده و حفظ فاکتور )می توانید طی مدت زمان یک هفته پس از خرید کالا جهت بازگشت وجه به حساب خود با شماره 02122012612 تماس حاصل فرمایید. در این حالت هزینه باز پس فرستادن کالا بر عهده مشتریان عزیز بوده و بازپرداخت وجه نیز طی 48 ساعت پس از دریافت کالا توسط فروشگاه فرش مشهد انجام خواهد گرفت.
  قیمت ها با احتساب 9% مالیات بر ارزش افزوده می باشند.


                    </div>
                    
                          <div style="background-color: gray; color: #fff; text-align: center; direction: rtl;">
                        sales@mashadcarpet.com
                              <br />
                              <a href="http://www.mashadcarpet.com">www.mashadcarpet.com</a>
                    </div>
                </asp:Panel>
        --%>
        <div class="col-md-12">
            <asp:Panel ID="pnlError" runat="server" CssClass="alert alert-danger" Visible="false">
                خطایی در عملیات پرداخت رخ داده است.
            لطفا مجداداً وارد شوید

            <br />
                <a href="Default.aspx">صفحه اصلی سایت</a>
            </asp:Panel>
        </div>
        <%--</div>
        </div>--%>

        <%--  <div style="margin:0 auto;text-align:center;color:#AAA399;width:500px;height:auto;margin-top:50px;font-size:18px;">
   
         درحال اتصال به صفحه بانک
        <br />
        <img src="images/loading_spinner.gif" />
    </div>--%>
    </form>
</body>
</html>
