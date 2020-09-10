using MashadCarpet.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spire.Pdf;
using Spire.Pdf.HtmlConverter;
using System.Text;
using System.Threading;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using MashadCarpet.Classes;
using GSD.Globalization;

namespace MashadCarpet
{
    public partial class Bank : System.Web.UI.Page
    {
        public static readonly string PgwSite = ConfigurationManager.AppSettings["PgwSite"];
        public static readonly string CallBackUrl = ConfigurationManager.AppSettings["CallBackUrl"];
        public static readonly string TerminalId = ConfigurationManager.AppSettings["TerminalId"];
        public static readonly string UserName = ConfigurationManager.AppSettings["UserName"];
        public static readonly string UserPassword = ConfigurationManager.AppSettings["UserPassword"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["OID"] != null && Request.QueryString["UID"] != null)
            {
                if (IdentifyCulture.cultureName().Contains("fa"))
                {
                    var persianCulture = new PersianCulture();
                    Thread.CurrentThread.CurrentCulture = persianCulture;
                    Thread.CurrentThread.CurrentUICulture = persianCulture;


                }
                Guid OrderID = new Guid(Request.QueryString["OID"]);
                Guid UserID = new Guid(Request.QueryString["UID"]);

                if (ValidateUser(UserID, OrderID) == true)
                {
                    //if (IsFinlizedOrder(OrderID) == false)
                    //{
                        //CreatePDFFactor();
                    //}
                    //Panel1.Visible = false;
                    FinlizeOrder(OrderID);
                    List<string> PriceAndOrderID = ReturnPaymentLogID(OrderID);
                    PayRequest(PriceAndOrderID[0], PriceAndOrderID[1]);

                }
                else
                {
                    pnlError.Visible = true;
                }

            }
            else
            {
                pnlError.Visible = true;
            }

        }


        #region Go To Bank
        public void FinlizeOrder(Guid OrderID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from a in db.Orders
                         where a.OrderID == OrderID && a.IsDelete == false
                         select a).FirstOrDefault();

                n.IsFinalized = true;
                n.FinalPrice = Convert.ToDecimal(OrderInfo.returnFinalPrice(n.OrderID));

                db.SaveChanges();
            }

        }
        public List<string> ReturnPaymentLogID(Guid OrderID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                List<string> returnList = new List<string>();
                Nullable<double> Price = 0;

                PaymentUniqeCodes puc = new PaymentUniqeCodes();
                puc.fk_OrderID = OrderID;

                db.PaymentUniqeCodes.Add(puc);
                db.SaveChanges();
                returnList.Add(puc.PayementUniqeCodeID.ToString());

                var p = (from a in db.Orders
                         where a.OrderID == OrderID && a.IsDelete == false
                         select a).FirstOrDefault();

                if (p != null)
                {
                    Price = Convert.ToDouble(p.FinalPrice);
                }


                //var od = (from a in db.OrderDetails
                //          where a.IsDelete == false && a.fk_OrderID == OrderID
                //          select a).ToList();
                //foreach (var item in od)
                //{
                //    var n = (from a in db.ProductColorSizes
                //             where a.ProductColorSizeID == item.fk_ProductColorSizeID
                //             && a.IsDelete == false
                //             select a).FirstOrDefault();

                //    Price = Price + Convert.ToDouble(n.ProductPrice) * item.Count;
                //}
                Price = Price * 10;
                returnList.Add(Price.ToString());
                return returnList;
            }
        }
        public Int64 returnPurePrice(string Price)
        {
            string[] priceParts = Price.Split('/');
            return Convert.ToInt64(priceParts[0]);
        }
        public void PayRequest(string OrderID, string Price)
        {
            //try
            //{
            string PayDateTextBox = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
            string PayTimeTextBox = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');

            string result;

            BypassCertificateError();

            MashadCarpet2017.ir.shaparak.bpm.PaymentGatewayImplService bpService = new MashadCarpet2017.ir.shaparak.bpm.PaymentGatewayImplService();
            //  MashadCarpet2017.ir.bankmellat.bpm.pgwstest.PaymentGatewayImplService bpService = new MashadCarpet2017.ir.bankmellat.bpm.pgwstest.PaymentGatewayImplService();
            Int64 newprice = returnPurePrice(Price);
            Int64 od = Int64.Parse(OrderID);

            result = bpService.bpPayRequest(Int64.Parse(TerminalId),
                              UserName,
                              UserPassword,
                              od,
                              newprice,
                              PayDateTextBox,
                              PayTimeTextBox,
                             null,
                           "http://www.mashadcarpet.com/callback.aspx",
                             0);

            String[] resultArray = result.Split(',');
            if (resultArray[0] == "0")
            {
                ClientScript.RegisterStartupScript(typeof(Page), "ClientScript", "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script> ", false);
            }
            else
            {
                int ResCode = Convert.ToInt32(resultArray[0]);
                Guid GOrderID = ConvertUniqeIDToRealOrderID(OrderID);
                InsertIntoPaymentLogs(GOrderID, ResCode, "");
                pnlError.Visible = true;
                lblResCode.Text = ResCode.ToString();
            }
            //}
            //catch (Exception exp)
            //{
            //    pnlError.Visible = true;
            //}
        }
       
        public void InsertIntoPaymentLogs(Guid OrderID, int ResCode, string ErrorrText)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                PaymentLogs a = new PaymentLogs();
                a.PaymentLogID = Guid.NewGuid();
                a.fk_OrderID = OrderID;

                a.ResCode_Request = ResCode;
                //a.PaymentDate = DateTime.Now;
                a.PaymentIP = Request.UserHostName;
                a.IsSuccess = false;

                db.PaymentLogs.Add(a);
                db.SaveChanges();
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
        public Boolean ValidateUser(Guid UserID, Guid OrderID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from a in db.Orders
                         where a.OrderID == OrderID && a.fk_UserID == UserID && a.IsDelete == false
                         select a).FirstOrDefault();

                if (n != null)
                    return true;
                else
                    return false;
            }
        }
        public Guid ConvertUniqeIDToRealOrderID(string UniqOrderID)
        {
            long id = Int64.Parse(UniqOrderID);
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from a in db.PaymentUniqeCodes
                         where a.PayementUniqeCodeID == id
                         select a).FirstOrDefault();

                return n.fk_OrderID;
            }
        }
        #endregion


       
    }
}