<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Factor.aspx.cs" Inherits="MashadCarpet.Factor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="DefaultCss/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="/DefaultCss/bootstrap.min.css" />
    <link href="css/MyStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="graydiv">
                    <p>
                        شرکت فرش مشهد (سهامی خاص) 
                    <br />
                        فاکتور فروش اینترنتی
                    </p>
                </div>
                <div class="whitediv">
                    شماره فاکتور: 
                <asp:Label ID="customerOrderID" runat="server" Text=""></asp:Label>
                    <br />
                    تاریخ فاکتور: 
                <asp:Label ID="lblOrderDate" runat="server" Text=""></asp:Label>
                </div>
                <div class="graydiv">
                    <p>
                        مشخصات خریدار
                    </p>
                </div>
                <div class="whitediv">
                    نام و نام خانوادگی: 
                <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
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
                    آدرس: 
                <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
                </div>


                <div class="graydiv">
                    <p>
                        مشخصات دریافت کننده
                    </p>
                </div>
                <div class="whitediv">
                    نام و نام خانوادگی: 
                <asp:Label ID="lblRecipientName" runat="server" Text=""></asp:Label>
                    <br />
                    شماره تلفن: 
                <asp:Label ID="lblRecipientPhone" runat="server" Text=""></asp:Label>
                    <br />
                    آدرس: 
                <asp:Label ID="lblRecipientAddress" runat="server" Text=""></asp:Label>
                </div>


                <div class="graydiv">
                    <p>
                        توضیحات خرید
                    </p>
                </div>
                <div class="whitediv">
                    <asp:Label ID="lblOrderDesc" runat="server" Text=""></asp:Label>
                </div>

                <div class="graydiv">
                    <p>
                        اجناس خریداری شده
                    </p>
                </div>
                <div class="whitediv">
                    <asp:Panel ID="pnlOrderDetails" runat="server">
                        <div class="col-md-12">
                            <asp:Panel ID="pnlOrders" runat="server">
                                <table class="table cart-table directionRTL text-right">
                                    <thead>
                                        <tr>
                                            <th class="table-title text-right">ردیف</th>
                                            <th class="table-title text-right">نام کالا</th>
                                            <th class="table-title text-right">جنس</th>
                                            <th class="table-title text-right">شانه</th>
                                            <th class="table-title text-right">تراکم</th>
                                            <th class="table-title text-right">سایز</th>
                                            <th class="table-title text-right">رنگ زمینه</th>
                                            <%--<th class="table-title text-right">درجه</th>--%>
                                            <th class="table-title text-right">تعداد</th>
                                            <th class="table-title text-right">مبلغ فروش</th>
                                            <th class="table-title text-right">جمع کل</th>


                                        </tr>
                                    </thead>
                                    <asp:Repeater ID="rptOrders" runat="server" OnItemDataBound="rptOrders_ItemDataBound">
                                        <ItemTemplate>


                                            <tbody>
                                                <tr>
                                                    <td><%# Container.ItemIndex + 1 %></td>

                                                    <td class="product-price-col"><span class="product-price-special fontBRoya"><%# Eval("ProductTitle") %> </span></td>
                                                    <td class="product-price-col"><span class="product-price-special fontBRoya"><%# Eval("Collection") %> </span></td>
                                                    <td class="product-price-col"><span class="product-price-special fontBRoya"><%# Eval("Reeds") %> </span></td>
                                                    <td class="product-price-col"><span class="product-price-special fontBRoya"><%# Eval("Shots") %> </span></td>
                                                    <td class="product-price-col"><span class="product-price-special fontBRoya"><%# Eval("SizeTitle") %> </span></td>
                                                    <td class="product-price-col"><span class="product-price-special fontBRoya"><%# Eval("ColorTitle") %> </span></td>
                                                    
                                                     <td>
                                                        <div class="custom-quantity-input">
                                                       
                                                            <asp:TextBox ID="txtCount" runat="server" Text='<%# Eval("Count") %>' Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                     <td class="product-total-col"><span class="product-price-special">
                                                        <asp:HiddenField ID="hfOrderDetailID" runat="server" Value='<%# Eval("OrderDetailID") %>' />
                                                        <asp:Label ID="lblTotalPrice" runat="server" Text="" CssClass="fontBRoya"></asp:Label></span>
                                                    </td>
                                                    <td class="product-price-col"><span class="product-price-special fontBRoya"><%# string.Format("{0:N0}",Eval("ProductPrice")) %> تومان</span></td>
                                                  
                                                </tr>

                                            </tbody>



                                        </ItemTemplate>
                                    </asp:Repeater>

                                </table>
                            </asp:Panel>
                            <div class="md-margin"></div>
                            <!-- space -->

                            <!-- End .row -->
                        </div>
                        <!-- End .col-md-12 -->
                    </asp:Panel>
                </div>

                </div>
             <div class="graydiv">
                 <p>
                     توضیحات
                 </p>
             </div>
                <div class="whitediv">
                    
ازخرید شما متشکریم.
                    <br />
                    

• جهت پاسخگویی به سوالات احتمالی با شماره 02122012612 تماس حاصل فرمایید و یا به بخش سوالات متداول در فروشگاه اینترنتی فرش مشهد به آدرس mashadcarpet.com مراجعه فرمائید.
<br />
• درصورت مشاهده هر گونه نقص یا ایراد در فرش دریافتی و یا مغایرت فرش ارسال شده با عکس و مشخصات آن به محض دریافت با شماره 02122012612 جهت تعویض یا مرجوع کردن کالا و بازگشت وجه ، تماس حاصل فرمایید. در این حالت کلیه هزینه های بازپس فرستادن و ارسال مجدد کالا برعهده شرکت فرش مشهد خواهد بود و همچنین در صورت انصراف از خرید ( در صورت عدم استفاده و حفظ فاکتور )می توانید طی مدت زمان یک هفته پس از خرید کالا جهت بازگشت وجه به حساب خود با شماره 02122012612 تماس حاصل فرمایید. در این حالت هزینه باز پس فرستادن کالا بر عهده مشتریان عزیز بوده و بازپرداخت وجه نیز طی 48 ساعت پس از دریافت کالا توسط فروشگاه فرش مشهد انجام خواهد گرفت.
<br />
• قیمت ها با احتساب 9% مالیات بر ارزش افزوده می باشند.

                </div>

                 <div class="graydiv">
                    <p>
                        <a href="#"> sales@mashadcarpet.com</a><br />
                     <a href="http://www.mashadcarpet.com">www.mashadcarpet.com</a>
                    

                    </p>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
