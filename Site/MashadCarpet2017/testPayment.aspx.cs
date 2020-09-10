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

namespace MashadCarpet
{
    public partial class testPayment : System.Web.UI.Page
    {
        public static readonly string PgwSite = ConfigurationManager.AppSettings["PgwSite"];
        public static readonly string CallBackUrl = ConfigurationManager.AppSettings["CallBackUrl"];
        public static readonly string TerminalId = ConfigurationManager.AppSettings["TerminalId"];
        public static readonly string UserName = ConfigurationManager.AppSettings["UserName"];
        public static readonly string UserPassword = ConfigurationManager.AppSettings["UserPassword"];
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Guid OrderID = new Guid("7DE0C9F1-ACE2-45DA-A530-F17D354E96B1");
            List<string> PriceAndOrderID = ReturnPaymentLogID(OrderID);
         
          
            PayRequest(PriceAndOrderID[0], PriceAndOrderID[1]);

        }
        public List<string> ReturnPaymentLogID(Guid OrderID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                List<string> returnList = new List<string>();

                PaymentUniqeCodes puc = new PaymentUniqeCodes();
                puc.fk_OrderID = OrderID;

                db.PaymentUniqeCodes.Add(puc);
                db.SaveChanges();
                returnList.Add(puc.PayementUniqeCodeID.ToString());

                //var m = (from a in db.Orders
                //         where a.IsDelete == false && a.OrderID == OrderID
                //         select a).FirstOrDefault();

                Nullable<double> Price = 0;
                var od = (from a in db.OrderDetails
                          where a.IsDelete == false && a.fk_OrderID == OrderID
                          select a).ToList();
                foreach (var item in od)
                {
                    var n = (from a in db.ProductColorSizes
                             where a.ProductColorSizeID == item.fk_ProductColorSizeID
                             && a.IsDelete == false
                             select a).FirstOrDefault();

                    Price = Price + Convert.ToDouble(n.ProductPrice) * item.Count;
                }
                returnList.Add(Price.ToString());
                return returnList;
            }
        }
        public void PayRequest(string OrderID, string Price)
        {
            try
            {
                string PayDateTextBox = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
                string PayTimeTextBox = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');

                string result;

                BypassCertificateError();

                MashadCarpet2017.ir.shaparak.bpm.PaymentGatewayImplService bpService = new MashadCarpet2017.ir.shaparak.bpm.PaymentGatewayImplService();

                result = bpService.bpPayRequest(Int64.Parse(TerminalId),
                                  UserName,
                                  UserPassword,
                                  Int64.Parse(OrderID),
                                  Int64.Parse(Price),
                                  PayDateTextBox,
                                  PayTimeTextBox,
                                 null,
                               "http://www.mashadcarpet.com/callback.aspx",
                                 0);
                Label1.Text = result;


                //lblPayOutput.Visible = true;
                //lblPayOutput.Text = result;

                String[] resultArray = result.Split(',');
                if (resultArray[0] == "0")
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "ClientScript", "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script> ", false);
                }
                else
                {
                    int ResCode=Convert.ToInt32(resultArray[0]);
                    Guid GOrderID = new Guid(OrderID);
                    InsertIntoPaymentLogs(resultArray[1], GOrderID, ResCode, "");
                }
            }
            catch (Exception exp)
            {

            }
        }
        public void InsertIntoPaymentLogs(string RefID, Guid OrderID, int ResCode, string ErrorrText)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                PaymentLogs a = new PaymentLogs();
                a.PaymentLogID = Guid.NewGuid();
                a.fk_OrderID = OrderID;
                a.RefID = RefID;
                a.ResCode_Request = ResCode;
                a.PaymentDate = DateTime.Now;
                a.PaymentIP = Request.UserHostName;
                a.IsSuccess = false;

                db.PaymentLogs.Add(a);
                db.SaveChanges();
            }
        }
        void BypassCertificateError()
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                delegate(
                    Object sender1,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
        }

     //  protected void btncalculate_Click(object sender, EventArgs e)
     //  {
     //      Guid OrderID = new Guid(TextBox1.Text);
     //      List<string> PriceAndOrderID = ReturnPaymentLogID(OrderID);
     //      Label1.Text = PriceAndOrderID[0] + "   " + PriceAndOrderID[1];
     //  }
    }
}