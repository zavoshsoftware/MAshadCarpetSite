<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="FinlizingFactor.aspx.cs" Inherits="MashadCarpet.FinlizingFactor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        th, td
        {
            text-align:center !important;

        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content" role="main">
        <div class="breadcrumb-container">

            <div class="container">
                <div class="row">
                    <div class="col-sm-12">
                        <ul class="breadcrumb">
                            <li><a href="/default.aspx" title="Home">
                                <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Resource,MainPageTitle%>"></asp:Literal></a> <i class="fa fa-arrow-right newfa"></i></li>
                            <li class="active">
                                <asp:Literal ID="ltContactTitle" runat="server" Text="<%$Resources:Resource,YourShoppingCart%>"></asp:Literal></li>
                        </ul>
                    </div>
                    <!-- End .col-md-12 -->
                </div>
                <!-- End .row -->
            </div>
            <!-- End .container -->
        </div>
        <!-- End .breadcrumb-container -->

        <div class="container">
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
                        <asp:Label ID="lblCity" runat="server" Text=""></asp:Label>
                        <br />
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
                   با تشکر از خرید شما، لطفا به نکات زیر توجه فرمایید :
                        <br />
1. جهت پاسخگویی به سوالات خود با شماره 22012612 (021) واحد فروش تماس حاصل فرمایید. زمان کاری واحد فروش روزهای شنبه تا چهارشنبه از ساعت 8 الی 16:30 می باشد. 
همچنین می توانید به بخش سوالات متداول در فروشگاه اینترنتی فرش مشهد به آدرس www.MashadCarpet.com مراجعه فرمایید.
                         <br />
2. لطفا درصورت مشاهده هرگونه ایراد در محصول دریافتی و یا مغایرت محصول با تصویر و مشخصات آن، موضوع را سریعاً به واحد فروش (جهت تعویض و یا بازگشت وجه) اطلاع رسانی کنید. بدیهی است در صورت بروز هرگونه مغایرت در محصول خریداری شده کلیه هزینه های تعویض، ارجاع و ارسال مجدد با شرکت فرش مشهد خواهد بود.
                         <br />
3. درصورت انصراف از خرید (در صورت عدم استفاده از محصول و نیز همراه داشتن فاکتور) می توانید طی مدت یک هفته پس از خرید کالا جهت بازگشت وجه به حساب خود با واحد فروش تماس حاصل فرمایید. بدیهی است در صورت انصراف از خرید هزینه ارجاع محصول به عهده مشتری محترم بوده و بازپرداخت وجه نیز طی 48 ساعت پس از دریافت کالا توسط شرکت فرش مشهد انجام خواهد گرفت.
                         <br />
4. قیمت ها با احتساب 9% مالیات بر ارزش افزوده محاسبه شده است.

                    </div>
                    
                          <div style="background-color: gray; color: #fff; text-align: center; direction: rtl;">
                        sales@mashadcarpet.com
                              <br />
                              <a href="http://www.mashadcarpet.com">www.mashadcarpet.com</a>
                    </div>
                </asp:Panel>


                <asp:Panel ID="pnlIsFinilized" runat="server">
                    <p class="alert-succeed directionRTL">
                        <asp:Literal ID="ltDearUder" runat="server" Text="<%$Resources:Resource,Dearuser%>"></asp:Literal>
                        <br />
                        <asp:Literal ID="ltFinilizeFactor" runat="server" Text="<%$Resources:Resource,FinalizedFactor%>"></asp:Literal>
                        <br />
                        <span class="directionRTL">
                            <asp:Literal ID="ltTrackingNum" runat="server" Text="<%$Resources:Resource,TrackingNumber%>"></asp:Literal>

                            <asp:Label ID="lblTrackingCode" runat="server" Text="" ForeColor="#1694D6"></asp:Label>
                        </span>

                        <br />
                        <asp:Literal ID="ltShowOrderList" runat="server" Text="<%$Resources:Resource,ShowOrderList%>"></asp:Literal>
                        <a href="/Bill/">
                            <asp:Literal ID="ltHere" runat="server" Text="<%$Resources:Resource,Here%>"></asp:Literal></a>
                        را کلیک کنید.
                    </p>
                </asp:Panel>
            </div>
        </div>
    </section>
</asp:Content>
