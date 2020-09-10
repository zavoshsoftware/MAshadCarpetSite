using MashadCarpet.Models;
using Spire.Pdf;
using Spire.Pdf.HtmlConverter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MashadCarpet
{
    public partial class CallBack : System.Web.UI.Page
    {
        public static readonly string PgwSite = ConfigurationManager.AppSettings["PgwSite"];
        public static readonly string CallBackUrl = ConfigurationManager.AppSettings["CallBackUrl"];
        public static readonly string TerminalId = ConfigurationManager.AppSettings["TerminalId"];
        public static readonly string UserName = ConfigurationManager.AppSettings["UserName"];
        public static readonly string UserPassword = ConfigurationManager.AppSettings["UserPassword"];

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.Params["RefId"] != null&& Request.Params["ResCode"]!=null &&
            //    Request.Params["SaleOrderId"]!=null && Request.Params["SaleReferenceId"]!=null)
            //{
            string RefId = Request.Params["RefId"];
            //if 0 is succes
            string ResCode = Request.Params["ResCode"];
            //vakilmoshaver uniqe order id
            string SaleOrderId = Request.Params["SaleOrderId"];
            //Payment codeID
            string SaleReferenceId = Request.Params["SaleReferenceId"];

            if (ResCode == "0")
            {
                VerifyRequest(ResCode, SaleOrderId, SaleOrderId, SaleReferenceId);


            }
            else
            {
                //Response.Redirect("/Test.aspx?RefId=" + RefId + "/salesOrderId=" + SaleOrderId + "/SaleReferenceId=" + SaleReferenceId
                //    + "/ResCode=" + ResCode + "/ResCode?" + ResCode);
                long? saleId = null;
                if (!string.IsNullOrEmpty(SaleReferenceId))
                    saleId = Convert.ToInt64(SaleReferenceId);
                InsertIntoPaymentLogs(RefId, ReturnRealOrderID(SaleOrderId), saleId, 0,
                    Convert.ToInt32(ResCode), null, null, PaymentMessage(ResCode), false);
                Response.Redirect("~/bill?Fail=1");
            }
            //}
        }

        public Guid ReturnRealOrderID(string SaleOrderID)
        {
            int SaleID = Convert.ToInt32(SaleOrderID);
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from a in db.PaymentUniqeCodes
                         where a.PayementUniqeCodeID == SaleID
                         select a).FirstOrDefault();

                return n.fk_OrderID;
            }
        }
        public void InsertIntoPaymentLogs(string RefID, Guid OrderID, long? SaleRefrenceID, int ResCode_Request, Nullable<int> ResCode_Payment, Nullable<int> ResCode_Verify, Nullable<int> ResCode_Settle, string ErrorrText, Boolean IsSiccess)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                PaymentLogs a = new PaymentLogs();

                a.PaymentLogID = Guid.NewGuid();
                a.fk_OrderID = OrderID;
                a.RefID = RefID;
                a.SaleReferenceId = SaleRefrenceID;

                a.ResCode_Request = ResCode_Request;
                a.ResCode_Payment = ResCode_Payment;
                a.ResCode_Settle = ResCode_Settle;
                a.ResCode_Verify = ResCode_Verify;

                a.ErrorMessage = ErrorrText;
                a.PaymentDate = DateTime.Now;
                a.PaymentIP = Request.UserHostName;
                a.IsSuccess = false;

                db.PaymentLogs.Add(a);
                db.SaveChanges();
            }
        }
        public void VerifyRequest(string ResCode, string OrderID, string SaleOrderID, string SaleReferenceId)
        {

            try
            {
                string result;
                BypassCertificateError();
                MashadCarpet2017.ir.shaparak.bpm.PaymentGatewayImplService bpService = new MashadCarpet2017.ir.shaparak.bpm.PaymentGatewayImplService();
                // MashadCarpet2017.ir.bankmellat.bpm.pgwstest.PaymentGatewayImplService bpService = new MashadCarpet2017.ir.bankmellat.bpm.pgwstest.PaymentGatewayImplService();
                result = bpService.bpVerifyRequest(Int64.Parse(TerminalId),
                   UserName,
                    UserPassword,
                    Int64.Parse(OrderID),
                    Int64.Parse(SaleOrderID),
                    Int64.Parse(SaleReferenceId));

                if (result == "0")
                {
                    SettleRequest(OrderID, SaleOrderID, SaleReferenceId);
                }
                else
                {
                    string RefId = Request.Params["RefId"];
                    InsertIntoPaymentLogs(RefId, ReturnRealOrderID(SaleOrderID), Convert.ToInt64(SaleReferenceId), 0, 0, Convert.ToInt32(result), null, PaymentMessage(result), false);
                    Response.Redirect("~/bill?Fail=2", false);
                }

            }
            catch (Exception exp)
            {
                Response.Redirect("~/bill?Fail=3");
                //     lblMessage.Text = "خطا: " + "خطایی در انجام پرداخت اتفاق افتاده است. لطفا مجددا پرداخت را انجام دهید";
            }


        }
        public void SettleRequest(string OrderID, string SaleOrderID, string SaleReferenceId)
        {
            try
            {
                string result;

                BypassCertificateError();

                MashadCarpet2017.ir.shaparak.bpm.PaymentGatewayImplService bpService = new MashadCarpet2017.ir.shaparak.bpm.PaymentGatewayImplService();
                //  MashadCarpet2017.ir.bankmellat.bpm.pgwstest.PaymentGatewayImplService bpService = new MashadCarpet2017.ir.bankmellat.bpm.pgwstest.PaymentGatewayImplService();
                result = bpService.bpSettleRequest(Int64.Parse(TerminalId),
                   UserName,
                   UserPassword,
                   Int64.Parse(OrderID),
                   Int64.Parse(SaleOrderID),
                   Int64.Parse(SaleReferenceId));

                Guid orderId = ReturnRealOrderID(SaleOrderID);
                if (result == "0")
                {
                    string RefId = Request.Params["RefId"];
                    UpdateOrder(orderId);
                    InsertIntoPaymentLogs(RefId, orderId, Convert.ToInt64(SaleReferenceId), 0, 0, 0, 0, PaymentMessage(result), true);

                    //CreatePDFFactor(orderId);
                    Response.Redirect("~/bill?Confirm=" + SaleReferenceId, false);
                }
                else
                {
                    string RefId = Request.Params["RefId"];
                    InsertIntoPaymentLogs(RefId, orderId, Convert.ToInt64(SaleReferenceId), 0, 0, 0, Convert.ToInt32(result), PaymentMessage(result), false);
                    Response.Redirect("~/bill?Fail=4", false);
                }
            }
            catch (Exception exp)
            {
                Response.Redirect("~/bill?Fail=5");
                //      lblMessage.Text = "ss: " + "خطایی در انجام پرداخت اتفاق افتاده است. لطفا مجددا پرداخت را انجام دهید";
            }
        }

        void BypassCertificateError()
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                delegate (
                    Object sender1,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
        }
        public String PaymentMessage(string PaymentCode)
        {
            switch (PaymentCode)
            {
                case "0":
                    {
                        return "تراكنش با موفقيت انجام شد";
                    }
                case "11":
                    {
                        return "شماره كارت نامعتبر است";
                    }
                case "12":
                    {
                        return "موجودي كافي نيست";
                    }
                case "13":
                    {
                        return "رمز نادرست است";
                    }
                case "14":
                    {
                        return "تعداد دفعات وارد كردن رمز بيش از حد مجاز است";
                    }
                case "15":
                    {
                        return "كارت نامعتبر است";
                    }
                case "16":
                    {
                        return "دفعات برداشت وجه بيش از حد مجاز است";
                    }
                case "17":
                    {
                        return "كاربر از انجام تراكنش منصرف شده است";
                    }
                case "18":
                    {
                        return "تاريخ انقضاي كارت گذشته است";
                    }
                case "19":
                    {
                        return "مبلغ برداشت وجه بيش از حد مجاز است";
                    }
                case "111":
                    {
                        return "صادر كننده كارت نامعتبر است";
                    }
                case "112":
                    {
                        return "خطاي سوييچ صادر كننده كارت";
                    }
                case "113":
                    {
                        return "پاسخي از صادر كننده كارت دريافت نشد";

                    }
                case "114":
                    {
                        return "دارنده كارت مجاز به انجام اين تراكنش نيست";

                    }
                case "21":
                    {
                        return "پذيرنده نامعتبر است";

                    }
                case "23":
                    {
                        return "خطاي امنيتي رخ داده است";

                    }
                case "24":
                    {
                        return "اطلاعات كاربري پذيرنده نامعتبر است";

                    }
                case "25":
                    {
                        return "مبلغ نامعتبر است";

                    }
                case "31":
                    {
                        return "پاسخ نامعتبر است";

                    }
                case "32":
                    {
                        return "فرمت اطلاعات وارد شده صحيح نمي باشد";

                    }
                case "33":
                    {
                        return "حساب نامعتبر است";

                    }
                case "34":
                    {
                        return "خطاي سيستمي";

                    }
                case "35":
                    {
                        return "تاريخ نامعتبر است";

                    }
                case "41":
                    {
                        return "شماره درخواست تكراري است";

                    }
                case "42":
                    {
                        return "تراكنش Sale يافت نشد";

                    }
                case "43":
                    {
                        return "قبلا درخواست Verify داده شده است";

                    }
                case "44":
                    {
                        return "درخواست Verfiy يافت نشد";

                    }
                case "45":
                    {
                        return "تراكنش Settle شده است";

                    }
                case "46":
                    {
                        return "تراكنش Settle نشده است";

                    }
                case "47":
                    {
                        return "تراكنش Settle يافت نشد";

                    }
                case "48":
                    {
                        return "تراكنش Reverse شده است";

                    }
                case "49":
                    {
                        return "تراكنش Refund يافت نشد";

                    }
                case "415":
                    {
                        return "زمان جلسه كاري به پايان رسيده است";

                    }
                case "416":
                    {
                        return "خطا در ثبت اطلاعات";

                    }
                case "417":
                    {
                        return "شناسه پرداخت كننده نامعتبر است";

                    }
                case "418":
                    {
                        return "اشكال در تعريف اطلاعات مشتري";

                    }
                case "419":
                    {
                        return "تعداد دفعات ورود اطلاعات از حد مجاز گذشته است";

                    }
                case "421":
                    {
                        return "IP نامعتبر است";

                    }
                case "51":
                    {
                        return "تراكنش تكراري است";

                    }
                case "54":
                    {
                        return "تراكنش مرجع موجود نيست";

                    }
                case "55":
                    {
                        return "تراكنش نامعتبر است";

                    }
                case "61":
                    {
                        return "خطا در واريز";

                    }
                default:
                    {
                        return "";

                    }

            }
        }
        public void UpdateOrder(Guid OrderID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from a in db.Orders
                         where a.OrderID == OrderID && a.IsDelete == false
                         select a).FirstOrDefault();


                n.IsFinalized = true;
                n.IsPaid = true;
                n.PaymentDate = DateTime.Now;
                db.SaveChanges();

                sendSMS(n.fk_UserID);
            }
        }
        public void sendSMS(Guid? userId)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                Users user = db.Users.Find(userId);

                if (user != null)
                {
                    Services.SmsService sms = new Services.SmsService();
                    List<string> list = new List<string>();
                    string mobile = user.Mobile;
                    if (!string.IsNullOrEmpty(mobile))
                    {
                        list.Add(mobile);
                        sms.Send(list,
                            "مشتری گرامی سفارش شما با موفقیت ثبت گردید. کارشناسان ما با شما تماس خواهند گرفت.", "");
                    }

                    list.Clear();
                    string number = WebConfigurationManager.AppSettings["CompanyNumber"];
                    list.Add(number);
                    sms.Send(list, "سفارش جدیدی در وب سایت ثبت گردید.", "");
                }
            }
        }
        #region CreatePDF
        public void CreatePDFFactor(Guid orderId)
        {
            LoadFactorData(orderId);
            PdfDocument pdf = new PdfDocument();
            PdfHtmlLayoutFormat htmlLayoutFormat = new PdfHtmlLayoutFormat();
            //webBrowser load html whether Waiting
            htmlLayoutFormat.IsWaiting = false;
            //page setting
            PdfPageSettings setting = new PdfPageSettings();
            setting.Size = PdfPageSize.A4;

            //Get innerHtml of Panel Control 
            var sb_htmlCode = new StringBuilder();
            Panel1.RenderControl(new HtmlTextWriter(new StringWriter(sb_htmlCode)));
            string htmlCode = sb_htmlCode.ToString();

            //use single thread to generate the pdf from above html code
            Thread thread = new Thread(() =>
            { pdf.LoadFromHTML(htmlCode, false, setting, htmlLayoutFormat); });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            // Save the file to PDF and preview it.
            string path = Server.MapPath("~/output.pdf");
            pdf.SaveToFile(path);
            CopyAndRenameFile(orderId);

            // System.Diagnostics.Process.Start(path);
            SendMail(orderId);
            Panel1.Visible = false;
        }
        public void LoadFactorData(Guid Code)
        {
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from a in db.Orders
                             where a.OrderID == Code
                             select a).FirstOrDefault();
                    if (n != null)
                    {
                        lblDate.Text = n.PaymentDate.Value.ToShortDateString();
                        lblDesc.Text = n.OrderDesc;
                        lblFactorID.Text = n.OrderShowID.ToString();
                        LoadUserDate(n.fk_UserID);
                        LoadOrderDetailInFactor(n.OrderID);
                    }
                }
            
        }
        protected void grdOrderDetails_DataBound(object sender, EventArgs e)
        {
            Nullable<double> TotalPrice = 0;
            foreach (GridViewRow r in grdOrderDetails.Rows)
            {
                HiddenField hfOrderDetailID = (HiddenField)r.FindControl("hfOrderDetailID");

                Literal ltPrice = (Literal)r.FindControl("ltPrice");
                Literal ltTotalPrice = (Literal)r.FindControl("ltTotalPrice");
                Literal ltPriceBeforDescount = (Literal)r.FindControl("ltPriceBeforDescount");

                Guid OrderDetailID = new Guid(hfOrderDetailID.Value);
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from a in db.OrderDetails
                             where a.OrderDetailID == OrderDetailID
                             && a.IsDelete == false
                             select a).FirstOrDefault();

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
                                ltPriceBeforDescount.Text = returnPurePrice((double)m.ProductPrice);

                                double pricenew = CalculateNewPrice((double)o.DiscountPercent, (decimal)m.ProductPrice);

                                ltPrice.Text = returnPurePrice(pricenew);

                                ltTotalPrice.Text = returnPurePrice((double)(pricenew * n.Count));

                                TotalPrice = TotalPrice + (pricenew * n.Count);
                            }
                            else
                            {

                                ltPrice.Text = returnPurePrice((double)m.ProductPrice);
                                ltPriceBeforDescount.Text = returnPurePrice((double)m.ProductPrice);


                                ltTotalPrice.Text = returnPurePrice((double)(m.ProductPrice * n.Count));
                                TotalPrice = TotalPrice + (double)(m.ProductPrice * n.Count);
                            }
                        }
                    }
                }
            }
            ltPaymentPrice.Text = returnPurePrice((double)TotalPrice) + " تومان";
        }
        public Nullable<Boolean> IsFinlizedOrder(Guid OrderID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from a in db.Orders
                         where a.OrderID == OrderID && a.IsDelete == false
                         select a).FirstOrDefault();

                return n.IsFinalized;
            }
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
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        public void LoadUserDate(Nullable<Guid> UserID)
        {
          //  if (HttpContext.Current.User.Identity.IsAuthenticated)
            //{
                //Guid OnlineUserID = new Guid(HttpContext.Current.User.Identity.Name);
                //if (UserID == OnlineUserID)
                //{
                    using (MashadCarpetEntities db = new MashadCarpetEntities())
                    {
                        var n = (from a in db.Users
                                 where a.UserID == UserID && a.IsDelete == false
                                 select a).FirstOrDefault();
                        if (n != null)
                        {
                            lblName.Text = n.UserName;
                            lblPhone.Text = n.Phone;
                            lblMobile.Text = n.Mobile;
                            lblAddress1.Text = n.Address1;
                            lblAddress2.Text = n.Address2;
                            lblEmail.Text = n.Email;

                            var p = (from a in db.Province
                                     where a.ProvinceID == n.ProvinceID
                                     select a).FirstOrDefault();

                            if (p != null)
                                lblProv.Text = p.ProvinceName;

                            var c = (from a in db.City
                                     where a.CityID == n.CityID
                                     select a).FirstOrDefault();

                            if (c != null)
                                lblCity.Text = c.CityName;
                        }
                //    }
              //  }
            }
        }
        public void LoadOrderDetailInFactor(Guid OrderID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from a in db.OrderDetails
                         join aa in db.ProductColorSizes on a.fk_ProductColorSizeID equals aa.ProductColorSizeID
                         join aaa in db.ProductColors on aa.fk_ProductColorID equals aaa.ProductColorID
                         join aaaa in db.Products on aaa.fk_ProductID equals aaaa.ProductID
                         join pg in db.ProductGroup on aaaa.fk_ProductGroupID equals pg.ProductGroupID
                         join s in db.SIzes on aa.fk_SizeID equals s.SizeID
                         join c in db.Colors on aaa.fk_ColorID equals c.ColorID
                         where a.fk_OrderID == OrderID && a.IsDelete == false
                         select new
                         {
                             a.OrderDetailID,
                             aaaa.ProductTitle,
                             pg.ProductGroupTitle,
                             //shane
                             aaaa.Reeds,
                             //tarakom
                             aaaa.Shots,
                             s.SizeTitle,
                             c.ColorTitle,
                             a.Count,

                         }).ToList();

                grdOrderDetails.DataSource = n;
                grdOrderDetails.DataBind();
            }
        }
        public void CopyAndRenameFile(Guid OrderID)
        {
            //1.Prepare the name for renaming
           

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from a in db.Orders
                         where a.OrderID == OrderID
                         select a).FirstOrDefault();
                //3.Copy the file with the new name
                string NewFileAddress = Server.MapPath("~") + @"\Uploads\Factors\" + n.CustomerOrderID + ".pdf";
                if (File.Exists(NewFileAddress))
                {
                    File.Delete(NewFileAddress);
                }
                File.Copy(Server.MapPath("~/output.pdf"), NewFileAddress);

            }
        }
        public void SendMail(Guid OrderID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                Guid ID = new Guid(HttpContext.Current.User.Identity.Name);
                var m = (from u in db.Users
                         where u.IsDelete == false
                             && u.UserID == ID
                         select u).FirstOrDefault();


                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("sale@mashadcarpet.com");

                mail.To.Add(m.Email);

                //Attachment
                //Guid OrderID = new Guid(Request.QueryString["OID"]);

                var code = (from a in db.Orders
                            where a.OrderID == OrderID
                            select a).FirstOrDefault();
                string FileName = Server.MapPath("~/Uploads/Factors/" + code.CustomerOrderID + ".pdf");
                Attachment data = new Attachment(FileName, MediaTypeNames.Application.Octet);
                mail.Attachments.Add(data);


                mail.Subject = "فاکتور فرش مشهد";
                DateTime today = DateTime.Today;
                //if (p.BlogDescription.Length > 200)
                //{
                mail.Body =
                     @"
  <style>
        #content.no-content form input::-webkit-input-placeholder {
            color: #fff;
            text-transform: uppercase;
        }

        .no-content-box {
            width: 771px;
            height: 514px;
            background-image: url('../images/404-contentbg.png');
            background-repeat: no-repeat;
            text-align: center;
            margin: 55px auto 60px;
            padding: 30px;
        }

            .no-content-box h2 {
                color: #2f4497;
                margin-bottom: 20px;
            }

            .no-content-box h3 {
                font-size: 35px;
                color: #807e78;
                margin-bottom: 23px;
            }

            .no-content-box p {
                line-height: 34px;
                color: #9c978d;
                margin-bottom: 36px;
            }

            .no-content-box form input {
                background-color: #f7f5f2;
                border-color: #e2e1d9;
                color: #8b8475;
                box-shadow: inset 0 0 0 1px rgba(255, 255, 255, 0.5);
            }

            .no-content-box .submit-btn {
                background-position: -299px -536px;
            }

            .no-content-box form input:-moz-placeholder {
                color: #8b8475;
                text-transform: uppercase;
            }

            .no-content-box form input::-moz-placeholder {
                color: #8b8475;
                text-transform: uppercase;
            }

            .no-content-box form input:-ms-input-placeholder {
                color: #8b8475;
                text-transform: uppercase;
            }

            .no-content-box form input::-webkit-input-placeholder {
                color: #8b8475;
                text-transform: uppercase;
            }

        .vcenter-container {
            display: table;
            height: 100%;
            table-layout: fixed;
            width: 100%;
        }
    </style>
<div style='padding: 5px; direction: rtl;'>
            <section id='content' role='main'>
                <div style='width: 1200px; margin-left: auto; margin-right: auto; padding-left: 15px; padding-right: 15px;'>
                    <div class='no-content-box wow fadeInRightBig'>
                        <div class='vcenter-container'>
                            <div class='vcenter'>
                                <h2>فرش مشهد</h2>
                                <h3>سفارش شما با موفقیت ثبت گردید.</h3>
                                <p>
                                   فاکتور سفارش شما به پیوست ارسال شده است
                                 <br />
                                    جهت پاسخگویی به سوالات احتمالی با شماره 22012612 (021) تماس حاصل فرمایید و یا به بخش سوالات متداول در فروشگاه اینترنتی فرش مشهد به آدرس <a href='http://mashadcarpet.com'>mashadcarpet.com</a> مراجعه فرمائید.
                                    <br />

                                </p>
                            </div>

                        </div>

                    </div>

                </div>

            </section>
        </div>";

                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("185.129.169.25");

                System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("sale@mashadcarpet.com",
               "oBuo92^4");
                mail.Headers.Add("Message-Id", String.Concat("<", DateTime.Now.ToString("yyMMdd"), ".", DateTime.Now.ToString("HHmmss"), "sale@mashadcarpet.com>"));

                smtp.UseDefaultCredentials = false;

                smtp.Credentials = basicAuthenticationInfo;

                mail.Priority = MailPriority.Normal;

                smtp.Send(mail);
                //}


            }
        }
        #endregion
    }
}