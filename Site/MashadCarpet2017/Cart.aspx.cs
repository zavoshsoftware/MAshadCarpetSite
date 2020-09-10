using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MashadCarpet.Models;
using System.Net.Mail;
using MashadCarpet.Classes;
using GSD.Globalization;
using System.Threading;

namespace MashadCarpet
{
    public partial class Cart : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IdentifyCulture.cultureName().Contains("fa"))
            {
                var persianCulture = new PersianCulture();
                Thread.CurrentThread.CurrentCulture = persianCulture;
                Thread.CurrentThread.CurrentUICulture = persianCulture;

                ScriptManager.RegisterStartupScript(this, GetType(), "PageScriptfa",
             "$('.newfa').removeClass('fa-arrow-right'); $('.newfa').addClass('fa-arrow-left');", true);

            }
            if (!Page.IsPostBack)
            {
                RptOrdersBind();
                callInfo();
            }
        }
        public void callInfo()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from a in db.Texts
                         where a.TextID == 1067
                         select a).FirstOrDefault();

                lblCall.Text = (IdentifyCulture.cultureName().Contains("fa")) ? n.TextDescription :
                        ((IdentifyCulture.cultureName().Contains("en")) ? n.EN_TextDescription :
                        ((IdentifyCulture.cultureName().Contains("ru")) ? n.Rus_TextDescription :
                        ((IdentifyCulture.cultureName().Contains("zh")) ? n.China_TextDescription : n.TextDescription)));
            }
        }
        public void RptOrdersBind()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Guid UserID = new Guid(HttpContext.Current.User.Identity.Name);

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from a in db.Orders
                             where a.fk_UserID == UserID && a.IsFinalized == false && a.IsDelete == false
                             select a).FirstOrDefault();

                    if (n != null)
                    {
                        //hlOrder.NavigateUrl = "/Bill/"+n.OrderID;
                        var m = (from u in db.OrderDetails.AsEnumerable()
                                 join i in db.ProductColorSizes
                                 on u.fk_ProductColorSizeID equals i.ProductColorSizeID
                                 join aa in db.ProductColors on i.fk_ProductColorID equals aa.ProductColorID
                                 join p in db.Products.AsEnumerable() on aa.fk_ProductID equals p.ProductID
                                 join c in db.Colors on aa.fk_ColorID equals c.ColorID
                                 join s in db.SIzes on i.fk_SizeID equals s.SizeID
                                 join pg in db.ProductGroup on p.fk_ProductGroupID equals pg.ProductGroupID
                                 where u.fk_OrderID == n.OrderID && i.IsDelete == false && u.IsDelete == false && p.IsDelete == false
                                 select new
                                 {
                                     //         ProductTitle = (IdentifyCulture.cultureName().Contains("fa")) ? p.ProductTitle :
                                     //((IdentifyCulture.cultureName().Contains("en")) ? p.EN_ProductTitle :
                                     //((IdentifyCulture.cultureName().Contains("ru")) ? p.Rus_ProductTitle :
                                     //((IdentifyCulture.cultureName().Contains("zh")) ? p.China_ProductTitle : p.ProductTitle))),

                                   //  price = ReturnPercent() == 1 ? i.ProductPrice : 2,
                                     p.ProductTitle,
                                     aa.ProductImage,
                                     u.Count,
                                     i.ProductPrice,
                                     p.ProductID,
                                     u.OrderDetailID,
                                     c.ColorTitle,
                                     s.SizeTitle,
                                     p.ProductName,
                                     pg.ProductGroupName,
                                     aa.ProductColorID
                                 }).ToList();

                        rptOrders.DataSource = m;
                        rptOrders.DataBind();



                        lblTotalAllPrice.Text = string.Format("{0:N0}", (OrderInfo.returnFinalPrice(n.OrderID)));
                        //foreach (var item in m)
                        //{
                        //    price =price+ Convert.ToDecimal(item.Count * (item.ProductPrice));
                        //}

                        //lblTotalPrice.Text = (IdentifyCulture.cultureName().Contains("fa")) ? string.Format("{0:N0}", price) + "   تومان" :
                        //      ((IdentifyCulture.cultureName().Contains("en")) ? string.Format("{0:N0}", price) + "   Toman" :
                        //      ((IdentifyCulture.cultureName().Contains("ru")) ? string.Format("{0:N0}", price) + "   RS" :
                        //      ((IdentifyCulture.cultureName().Contains("zh")) ? string.Format("{0:N0}", price) + "   RS" : string.Format("{0:N0}", price) + "   تومان")));

                        if (m.Count == 0)
                        {
                            pnlOrderDetails.Visible = false;
                            pnlEmpry.Visible = true;
                        }
                    }
                    else
                    {
                        pnlOrderDetails.Visible = false;
                        pnlEmpry.Visible = true;
                    }

                }
            }
        }

        public int ReturnPercent()
        {
            return 1;
        }
        protected void rptOrders_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Nullable<double> TotalPrice = 0;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hfOrderDetailID = (HiddenField)e.Item.FindControl("hfOrderDetailID");
                Guid OrderDetailID = new Guid(hfOrderDetailID.Value.ToString());

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    Label lblTotalPrice = (Label)e.Item.FindControl("lblTotalPrice");
                    Label lblPriceWithDiscount = (Label)e.Item.FindControl("lblPriceWithDiscount");

                    var n = (from u in db.OrderDetails
                             where u.OrderDetailID == OrderDetailID
                             select new
                             {
                                 u.Count,
                                 u.fk_ProductColorSizeID
                             }).FirstOrDefault();


                    if (n != null)
                    {
                        var m = (from a in db.ProductColorSizes
                                 where a.ProductColorSizeID == n.fk_ProductColorSizeID
                                 && a.IsDelete == false
                                 select a).FirstOrDefault();
                        if (m != null)
                        {
                            var o = (from a in db.Rel_Discounts_Sizes
                                     join aa in db.Discounts
                                     on a.fk_DiscountID equals aa.DiscountID
                                     where a.fk_SizeID == m.fk_SizeID && aa.IsActive == true && aa.IsDelete == false
                                     select new { aa.DiscountPercent }).FirstOrDefault();

                            if (o != null)
                            {


                                double pricenew = CalculateNewPrice((double)o.DiscountPercent, (decimal)m.ProductPrice);
                                lblPriceWithDiscount.Text = returnPurePrice(pricenew);


                                lblTotalPrice.Text = returnPurePrice((double)(pricenew * n.Count));

                                TotalPrice = TotalPrice + (pricenew * n.Count);
                            }
                            else
                            {


                                lblPriceWithDiscount.Text = returnPurePrice((double)m.ProductPrice);

                                lblTotalPrice.Text = returnPurePrice((double)(m.ProductPrice * n.Count));
                                TotalPrice = TotalPrice + (double)(m.ProductPrice * n.Count);
                            }
                        }
                    }

                }
            }
            // lblTotalAllPrice.Text =(Convert.ToDouble(lblTotalAllPrice.Text)+ TotalPrice).ToString();
        }
        public string returnPurePrice(double Price)
        {
            string[] priceParts = Price.ToString().Split('/');
            return priceParts[0];
        }
        public double CalculateNewPrice(double DiscountPercent, decimal price)
        {
            double NewPercent = 100 - DiscountPercent;
            double pricenew = ((Convert.ToDouble(price) * NewPercent) / 100);

            return pricenew;
        }
        protected void rptOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Guid OrderDetailID = new Guid(e.CommandArgument.ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.OrderDetails where u.OrderDetailID == OrderDetailID select u).FirstOrDefault();
                n.IsDelete = true;
                n.DeleteDate = DateTime.Now;
                db.SaveChanges();

                Response.Redirect("/cart.aspx");
            }
        }

        protected void btnUpdateBasket_Click(object sender, EventArgs e)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                foreach (RepeaterItem item in rptOrders.Items)
                {
                    HiddenField hfOrderDetailID = (HiddenField)item.FindControl("hfOrderDetailID");

                    TextBox txtCount = (TextBox)item.FindControl("txtCount");

                    Guid OrderDetailID = new Guid(hfOrderDetailID.Value);


                    var n = (from a in db.OrderDetails
                             where a.OrderDetailID == OrderDetailID
                             select a).FirstOrDefault();

                    n.Count = Convert.ToInt32(txtCount.Text);

                    var m = (from u in db.Orders where u.OrderID == n.fk_OrderID select u).FirstOrDefault();
                    m.OrderDesc = txtOrderDesc.Text;

                }
                db.SaveChanges();
                RptOrdersBind();

            }
        }

        //protected void btnIsFinilzed_Click(object sender, EventArgs e)
        //{
        //    if (HttpContext.Current.User.Identity.IsAuthenticated)
        //    {
        //        Guid UserID = new Guid(HttpContext.Current.User.Identity.Name);

        //        using (MashadCarpetEntities db = new MashadCarpetEntities())
        //        {
        //            var n = (from a in db.Orders
        //                     where a.fk_UserID == UserID && a.IsFinalized == false && a.IsDelete == false
        //                     select a).FirstOrDefault();

        //            n.IsFinalized = true;
        //            n.OrderDesc = txtOrderDesc.Text;
        //            n.FinalPrice = Convert.ToDecimal(OrderInfo.returnFinalPrice(n.OrderID));
        //            db.SaveChanges();

        //            //lblTrackingCode.Text = n.CustomerOrderID;
        //            Response.Redirect("~/FinlizingFactor.aspx?Code=" + n.CustomerOrderID);
        //        }
        //        pnlOrderDetails.Visible = false;

        //        // pnlIsFinilized.Visible = true;
        //        //SendMail(UserID);

        //    }
        //}

        protected void btnpay_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Guid UserID = new Guid(HttpContext.Current.User.Identity.Name);

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from a in db.Orders
                             where a.fk_UserID == UserID && a.IsFinalized == false && a.IsDelete == false
                             select a).FirstOrDefault();

                    n.OrderDesc = txtOrderDesc.Text;
                   

                    //int finalPrice = Convert.ToInt32(n.FinalPrice);
                    //int newProductPrice=0 ;
                    //List<OrderDetails> orderDetails = db.OrderDetails.Where(current => current.fk_OrderID == n.OrderID && current.IsDelete==false).ToList();
                    //foreach(var detail in orderDetails)
                    //{
                    //    ProductColorSizes productColorSizes = db.ProductColorSizes.Where(current => current.IsDelete == false && current.ProductColorSizeID == detail.fk_ProductColorSizeID).FirstOrDefault();
                    //    newProductPrice = newProductPrice + (Convert.ToInt32(productColorSizes.ProductPrice)*detail.Count.Value);
                    //}
                    //if(finalPrice!=newProductPrice)
                    //{
                    //    n.FinalPrice = newProductPrice;
                    //}
                    db.SaveChanges();
                    Response.Redirect("~/bank.aspx?OID=" + n.OrderID.ToString() + "&&UID=" + UserID.ToString());

                }
            }
        }


        public void SendMail(Guid UserID)
        {


            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from a in db.Orders
                         where a.fk_UserID == UserID && a.IsDelete == false
                         select a).FirstOrDefault();

                var o = (from u in db.OrderDetails
                         join i in db.ProductColorSizes
                             on u.fk_ProductColorSizeID equals i.ProductColorSizeID
                         join aaa in db.ProductColors on i.fk_ProductColorID equals aaa.ProductColorID
                         join p in db.Products on aaa.fk_ProductID equals p.ProductID
                         join c in db.Colors on aaa.fk_ColorID equals c.ColorID
                         join s in db.SIzes on i.fk_SizeID equals s.SizeID
                         where u.fk_OrderID == n.OrderID && i.IsDelete == false && u.IsDelete == false && p.IsDelete == false
                         select new
                         {
                             p.ProductTitle,
                             p.Collection,
                             p.Reeds,
                             p.Shots,
                             aaa.ProductImage,
                             u.Count,
                             i.ProductPrice,
                             p.ProductID,
                             u.OrderDetailID,
                             c.ColorTitle,
                             s.SizeTitle,
                             p.ProductName
                         }).ToList();

                string[] rpt = new string[o.Count];
                int count = o.Count;
                for (int y = 0; y < count; y++)
                {
                    foreach (var item in o)
                    {

                        rpt[y] = @"   <td><%# Container.ItemIndex + 1 %></td><td class='product-price-col'><span class='product-price-special fontBRoya'>" + item.ProductTitle + @"</span></td>
                                                        <td class='product-price-col'><span class='product-price-special fontBRoya'>" + item.Collection + @" </span></td>
                                                        <td class='product-price-col'><span class='product-price-special fontBRoya'>" + item.Reeds + @" </span></td>
                                                        <td class='product-price-col'><span class='product-price-special fontBRoya'>" + item.Shots + @" </span></td>
                                                        <td class='product-price-col'><span class='product-price-special fontBRoya'><" + item.SizeTitle + @" </span></td>
                                                        <td class='product-price-col'><span class='product-price-special fontBRoya'>" + item.ColorTitle + @" </span></td>

                                                        <td>
                                                            <div class='custom-quantity-input'>
                                                               " + item.Count + @"
                                                              
                                                            </div>
                                                        </td>
                                                        <td class='product-total-col'><span class='product-price-special'>
                                                            <asp:Label ID='lblTotalPrice' runat='server' Text=" + item.ProductPrice + @" CssClass='fontBRoya'></asp:Label></span>
                                                        </td>
                                                        <td class='product-price-col'><span class='product-price-special fontBRoya'><%# string.Format('{0:N0}'," + item.ProductPrice + @") %> تومان</span></td>";
                    }
                }
                var m = (from u in db.Users where u.IsDelete == false && u.UserID == UserID select u).FirstOrDefault();
                //foreach (var m in n)
                //{


                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("blog@drjart.ir");

                mail.To.Add(m.Email);


                mail.Subject = "فرش مشهد";
                DateTime today = DateTime.Today;
                //if (p.BlogDescription.Length > 200)
                //{
                mail.Body =
                     @"
 <style>
        .btn-group-vertical > .btn-group::after, .btn-group-vertical > .btn-group::before, .btn-toolbar::after, .btn-toolbar::before, .clearfix::after, .clearfix::before, .container-fluid::after, .container-fluid::before, .container::after, .container::before, .dl-horizontal dd::after, .dl-horizontal dd::before, .form-horizontal .form-group::after, .form-horizontal .form-group::before, .modal-footer::after, .modal-footer::before, .nav::after, .nav::before, .navbar-collapse::after, .navbar-collapse::before, .navbar-header::after, .navbar-header::before, .navbar::after, .navbar::before, .pager::after, .pager::before, .panel-body::after, .panel-body::before, .row::after, .row::before {
            content: ' ';
            display: table;
        }

        *::after, *::before {
            box-sizing: border-box;
        }

        .btn-group-vertical > .btn-group::after, .btn-toolbar::after, .clearfix::after, .container-fluid::after, .container::after, .dl-horizontal dd::after, .form-horizontal .form-group::after, .modal-footer::after, .nav::after, .navbar-collapse::after, .navbar-header::after, .navbar::after, .pager::after, .panel-body::after, .row::after {
            clear: both;
        }

        .btn-group-vertical > .btn-group::after, .btn-group-vertical > .btn-group::before, .btn-toolbar::after, .btn-toolbar::before, .clearfix::after, .clearfix::before, .container-fluid::after, .container-fluid::before, .container::after, .container::before, .dl-horizontal dd::after, .dl-horizontal dd::before, .form-horizontal .form-group::after, .form-horizontal .form-group::before, .modal-footer::after, .modal-footer::before, .nav::after, .nav::before, .navbar-collapse::after, .navbar-collapse::before, .navbar-header::after, .navbar-header::before, .navbar::after, .navbar::before, .pager::after, .pager::before, .panel-body::after, .panel-body::before, .row::after, .row::before {
            content: ' ';
            display: table;
        }

        *::after, *::before {
            box-sizing: border-box;
        }

        .container {
            width: 1170px;
        }

        .container {
            width: 970px;
        }

        .container {
            width: 750px;
        }

        .container {
            margin-left: auto;
            margin-right: auto;
            padding-left: 15px;
            padding-right: 15px;
        }

        * {
            box-sizing: border-box;
        }

            *::-moz-selection {
                background-color: #cbc6b2;
                color: #fff;
            }

        .row {
            margin-left: -15px;
            margin-right: -15px;
        }

        .graydiv {
            background-color: #cccccc;
            direction: rtl;
            font-family: BYekan;
            font-size: 20px;
            font-weight: bolder;
            line-height: 1.8em;
            padding: 5px;
            text-align: center;
        }

        .whitediv {
            direction: rtl;
            font-family: BYekan;
            font-size: 16px;
            line-height: 1.8em;
            padding: 10px;
        }

        .col-md-12 {
            width: 100%;
        }

        .table {
            margin-bottom: 20px;
            max-width: 100%;
            width: 100%;
        }

        .text-right {
            text-align: right;
        }

        .table {
            border: 1px solid #e7e2d1;
            color: #9e988a;
            margin-bottom: 0;
        }

        table {
            background-color: transparent;
        }

        table {
            border-collapse: collapse;
            border-spacing: 0;
        }

        *::-moz-selection {
            background-color: #cbc6b2;
            color: #fff;
        }

        .table .table-title {
            color: #a19988;
            font: 16px/20px 'open_sansregular',sans-serif;
            text-transform: uppercase;
        }

        .table > thead > tr > th {
            border-bottom: 2px solid #ddd;
            vertical-align: bottom;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            border-top: 1px solid #ddd;
            line-height: 1.42857;
            padding: 8px;
            vertical-align: top;
        }

        .cart-table thead tr th {
            background: #e7e2d1 none repeat scroll 0 0;
            border-bottom: medium none;
            border-color: transparent transparent -moz-use-text-color;
            padding: 17px 25px;
        }

        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
            border-top: medium none;
            line-height: 1.5;
            padding: 10px;
            text-align: center;
        }

        .text-right {
            text-align: right;
        }

        th {
            text-align: left;
        }

        td, th {
            padding: 0;
        }

        * {
            box-sizing: border-box;
        }

            *::-moz-selection {
                background-color: #cbc6b2;
                color: #fff;
            }
    </style>
   <div class='container'>
                <div class='row'>
                    <div class='graydiv'>
                        <p>
                            شرکت فرش مشهد (سهامی خاص) 
                    <br />
                            فاکتور فروش اینترنتی
                        </p>
                    </div>
                    <div class='whitediv'>
                        شماره فاکتور: 
                <asp:Label ID='customerOrderID' runat='server' Text=" + n.CustomerOrderID.ToString() + @"></asp:Label>
                        <br />
                        تاریخ فاکتور: 
                <asp:Label ID='lblOrderDate' runat='server' Text=" + string.Format("{0:d}", n.SubmitDate) + @"></asp:Label>
                    </div>
                    <div class='graydiv'>
                        <p>
                            مشخصات خریدار
                        </p>
                    </div>
                    <div class='whitediv'>
                        نام و نام خانوادگی: 
                <asp:Label ID='lblUserName' runat='server' Text=" + m.UserName + ' ' + m.UserFamily + @"></asp:Label>
                        <br />
                        شماره تلفن: 
                <asp:Label ID='lblPhone' runat='server' Text=" + m.Phone + @"></asp:Label>
                        <br />
                        شماره موبایل: 
                <asp:Label ID='lblMobile' runat='server' Text=" + m.Mobile + @"></asp:Label>
                        <br />
                        ایمیل: 
                <asp:Label ID='lblEmail' runat='server' Text=" + m.Email + @"></asp:Label>
                        <br />
                        آدرس: 
                <asp:Label ID='lblAddress' runat='server' Text=" + m.Address1 + @"></asp:Label>
                    </div>


                    <div class='graydiv'>
                        <p>
                            مشخصات دریافت کننده
                        </p>
                    </div>
                    <div class='whitediv'>
                        نام و نام خانوادگی: 
                <asp:Label ID='lblRecipientName' runat='server' Text=" + n.RecipientName + @"></asp:Label>
                        <br />
                        شماره تلفن: 
                <asp:Label ID='lblRecipientPhone' runat='server' Text=" + n.RecipientPhone + @"></asp:Label>
                        <br />
                        آدرس: 
                <asp:Label ID='lblRecipientAddress' runat='server' Text=" + n.RecipientAddress + @"></asp:Label>
                    </div>


                    <div class='graydiv'>
                        <p>
                            توضیحات خرید
                        </p>
                    </div>
                    <div class='whitediv'>
                        <asp:Label ID='lblOrderDesc' runat='server' Text=" + n.OrderDesc + @"></asp:Label>
                    </div>

                    <div class='graydiv'>
                        <p>
                            اجناس خریداری شده
                        </p>
                    </div>
                    <div class='whitediv'>
                        <asp:Panel ID='pnlOrderDetails' runat='server'>
                            <div class='col-md-12'>
                                <asp:Panel ID='pnlOrders' runat='server'>
                                    <table class='table cart-table directionRTL text-right'>
                                        <thead>
                                            <tr>
                                                <th class='table-title text-right'>ردیف</th>
                                                <th class='table-title text-right'>نام کالا</th>
                                                <th class='table-title text-right'>جنس</th>
                                                <th class='table-title text-right'>شانه</th>
                                                <th class='table-title text-right'>تراکم</th>
                                                <th class='table-title text-right'>سایز</th>
                                                <th class='table-title text-right'>رنگ زمینه</th>
                                                <th class='table-title text-right'>تعداد</th>
                                                <th class='table-title text-right'>مبلغ فروش</th>
                                                <th class='table-title text-right'>جمع کل</th>


                                            </tr>
                                        </thead>
                                      

                                                                       
                                                   <tbody>
                                                    <tr style='border-bottom: 1px solid #DDDDDD;'>
                                                      
                                                        " +
                                                       rpt + @"

                                                    </tr>

                                                </tbody>
                                                            

                                    </table>
                                </asp:Panel>
                                <div class='md-margin'></div>
                                <!-- space -->

                                <!-- End .row -->
                            </div>
                            <!-- End .col-md-12 -->
                        </asp:Panel>
                    </div>

                </div>
                <div class='graydiv'>
                    <p>
                        توضیحات
                    </p>
                </div>
                <div class='whitediv'>
                    ازخرید شما متشکریم.
                    <br />


                    • جهت پاسخگویی به سوالات احتمالی با شماره 22012612 (021)  تماس حاصل فرمایید و یا به بخش سوالات متداول در فروشگاه اینترنتی فرش مشهد به آدرس mashadcarpet.com مراجعه فرمائید.
                <br />
                    • درصورت مشاهده هر گونه نقص یا ایراد در فرش دریافتی و یا مغایرت فرش ارسال شده با عکس و مشخصات آن به محض دریافت با شماره 02122012612 جهت تعویض یا مرجوع کردن کالا و بازگشت وجه ، تماس حاصل فرمایید. در این حالت کلیه هزینه های بازپس فرستادن و ارسال مجدد کالا برعهده شرکت فرش مشهد خواهد بود و همچنین در صورت انصراف از خرید ( در صورت عدم استفاده و حفظ فاکتور )می توانید طی مدت زمان یک هفته پس از خرید کالا جهت بازگشت وجه به حساب خود با شماره 02122012612 تماس حاصل فرمایید. در این حالت هزینه باز پس فرستادن کالا بر عهده مشتریان عزیز بوده و بازپرداخت وجه نیز طی 48 ساعت پس از دریافت کالا توسط فروشگاه فرش مشهد انجام خواهد گرفت.
                <br />
                    • قیمت ها با احتساب 9% مالیات بر ارزش افزوده می باشند.

                </div>

                <div class='graydiv'>
                    <p>
                        <a href='#'>sales@mashadcarpet.com</a><br />
                        <a href='http://www.mashadcarpet.com'>www.mashadcarpet.com</a>


                    </p>
                </div>
            </div>";



                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("185.10.72.134");

                System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("blog@drjart.ir",
               "111qqq!!!QQQ");
                mail.Headers.Add("Message-Id", String.Concat("<", DateTime.Now.ToString("yyMMdd"), ".", DateTime.Now.ToString("HHmmss"), "blog@drjart.ir>"));

                // SmtpClient smtp = new SmtpClient("195.154.169.92");

                // System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("test@dooris.ir",
                //"123qwe!@#QWE");
                // mail.Headers.Add("Message-Id", String.Concat("<", DateTime.Now.ToString("yyMMdd"), ".", DateTime.Now.ToString("HHmmss"), "test@dooris.ir>"));




                smtp.UseDefaultCredentials = false;

                smtp.Credentials = basicAuthenticationInfo;

                mail.Priority = MailPriority.Normal;

                smtp.Send(mail);


            }
        }
    }
}