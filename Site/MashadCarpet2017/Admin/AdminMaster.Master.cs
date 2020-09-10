using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using MashadCarpet.Models;
using System.Data.Objects;
using System.Text.RegularExpressions;

namespace MashadCarpet.Admin
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
                FindUserInfo();
                AdminActivities();
                FindPaymentNumber();
            }
            FindActiveMenu();
        }

        public void AdminActivities()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Users where u.IsDelete == false && u.fk_RoleID == 2 select u).Count();
                lblCountUsers.Text = n.ToString();

                var m = (from u in db.VisitCounter.AsEnumerable() where u.VisitDate.Value.Date == DateTime.Now.Date select u).Count();
                lblTodayVisitCount.Text = m.ToString();

                var t = (from u in db.Orders.AsEnumerable() where u.SubmitDate.Value.Date == DateTime.Now.Date && u.IsDelete == false select u).Count();
                lblTodayOrders.Text = t.ToString();
                lblTodayOrders2.Text = t.ToString();

                //var p = (from u in db.Users.AsEnumerable() where u.RegisterDate.Value.Date == DateTime.Now.Date && u.IsDelete == false select u).Count();
                //lblTodayUsers.Text = p.ToString();

                //lblNotification.Text = (p + t).ToString();
            }
        }

        public void FindPaymentNumber()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                lblAll.Text = (from u in db.Orders
                               where u.IsDelete == false
                               select new { u.OrderID }).Count().ToString();
                lblpay.Text = (from u in db.Orders
                               where u.IsDelete == false && u.IsPaid == true
                               select new { u.OrderID }).Count().ToString();
                lblFinal.Text = (from u in db.Orders
                                 where u.IsDelete == false && u.IsPaid == false && u.IsFinalized == true
                                 select new { u.OrderID }).Count().ToString();
                lblNotFinal.Text = (from u in db.Orders
                                    where u.IsDelete == false && u.IsFinalized == false
                                    select new { u.OrderID }).Count().ToString();

                //var n = (from u in db.Orders
                //    where u.IsDelete == false && u.IsNewOrder == false
                //    select new {u.OrderID});
                //if (n != null)
                //{
                //    int a = n.Count();
                //}
                string notCheckedOrderCount = (from u in db.Orders
                                               where u.IsDelete == false && (u.IsNewOrder == true || u.IsNewOrder == null)
                                               select new { u.OrderID }).Count().ToString();
                lblNotChecked.Text = notCheckedOrderCount;
                lblNotification.Text = notCheckedOrderCount;
                lblNotiNotCheck.Text = notCheckedOrderCount;
                lblCounttotal.Text = notCheckedOrderCount;


                DateTime yesterday = DateTime.Today.AddDays(-1).Date;
                DateTime today = DateTime.Now.Date;
                //var yesterdayPaidOrders =db.Orders.Where(x => DateTime.Compare(x.PaymentDate.Value.Date, yesterday.Date) == 0).Count();
                var yesterdayPaidOrders = (from u in db.Orders
                                           where u.IsPaid == true && u.IsDelete == false
                                           && EntityFunctions.TruncateTime(u.PaymentDate) == yesterday
                                           select u).Count();
                lblYesterdayPay.Text = yesterdayPaidOrders.ToString();

                var todayPaidOrders = (from u in db.Orders
                                       where u.IsPaid == true && u.IsDelete == false
                                       && EntityFunctions.TruncateTime(u.PaymentDate) == today
                                       select u).Count();

                lblTodayPay.Text = todayPaidOrders.ToString();
            }
        }
        public void FindActiveMenu()
        {
            string strPage = Page.AppRelativeVirtualPath;

            if (strPage == "~/Admin/Default.aspx")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "PageScript",
                "AddCurrent('#default')", true);
            }

            if (strPage == "~/Admin/ColorSetting.aspx" || strPage == "~/Admin/ProductSetting.aspx" || strPage == "~/Admin/RecycledProducts.aspx" || strPage == "~/Admin/ProductGroupSetting.aspx" || strPage == "~/Admin/mainProductGroupSetting.aspx" || strPage == "~/Admin/SizeSettting.aspx" || strPage == "~/Admin/ProductColorSetting.aspx" || strPage == "~/Admin/ProductSizeSetting.aspx")
            {
                if (strPage == "~/Admin/CommentSetting.aspx")
                {
                    if (Request.QueryString["commentType"] == "Blog")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "PageScript",
          "AddCurrent('#Blog')", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "PageScript",
            "AddCurrent('#product')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "PageScript",
                    "AddCurrent('#product')", true);
                }
            }
            else if (strPage == "~/Admin/UsersSetting.aspx")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "PageScript",
                "AddCurrent('#userli')", true);

            }
            else if (strPage == "~/Admin/TodayVisitSetting.aspx" || strPage == "~/Admin/DailyVisit.aspx")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "PageScript",
                "AddCurrent('#Visit')", true);

            }
            else if (strPage == "~/Admin/TicketSetting.aspx" || strPage == "~/Admin/TicketResponseSetting.aspx")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "PageScript",
                "AddCurrent('#Ticket')", true);

            }

            else if (strPage == "~/Admin/OrderListSetting.aspx" || strPage == "~/Admin/OrderStatusSetting.aspx")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "PageScript",
                "AddCurrent('#Order')", true);

            }

            else if (strPage == "~/Admin/SliderSetting.aspx")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "PageScript",
                "AddCurrent('#slid')", true);

            }

            else if (strPage == "~/Admin/TextSetting.aspx")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "PageScript",
                "AddCurrent('#txt')", true);

            }
            else if (strPage == "~/Admin/ContactusFormSetting.aspx")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "PageScript",
                "AddCurrent('#cuf')", true);

            }


            else if (strPage == "~/Admin/BlogsGroupSetting.aspx" || strPage == "~/Admin/BlogsSetting.aspx")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "PageScript",
                "AddCurrent('#Blog')", true);

            }
            else if (strPage == "~/Admin/reqList.aspx" || strPage == "~/Admin/StoreSetting.aspx")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "PageScript",
                "AddCurrent('#Store')", true);

            }
            else if (strPage == "~/Admin/NewsLetterSetting.aspx")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "PageScript",
                "AddCurrent('#NL')", true);

            }

        }
        public void FindUserInfo()
        {
            //using (zavoshOfficeEntities db = new zavoshOfficeEntities())
            //{
            //    if (HttpContext.Current.User.Identity.IsAuthenticated)
            //    {
            //        int UserID = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

            //        var n = (from a in db.Users
            //                 where a.UserID == UserID
            //                 select a).FirstOrDefault();

            //        lblUserName.Text = n.Name + " " + n.Family;
            //    }
            //}
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/login");
        }
    }
}