using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MashadCarpet.Models;
using System.Data.Objects;

namespace MashadCarpet.Admin
{
    public partial class TodayVisitSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadForm();
            }
        }

        public void LoadForm()
        {
            if (Request.QueryString["VisitID"] != null)
            {
                int VisitID = Convert.ToInt32(Request.QueryString["VisitID"].ToString());
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.VisitCounter where u.VisitID == VisitID select u).FirstOrDefault();

                    var m = (from u in db.VisitCounter where EntityFunctions.TruncateTime(u.VisitDate) == EntityFunctions.TruncateTime(n.VisitDate) select u).ToList();

                    GrdTable.DataSource = m;
                    GrdTable.DataBind();


                    var t = (from u in db.VisitCounter where EntityFunctions.TruncateTime(u.VisitDate) == EntityFunctions.TruncateTime(n.VisitDate) select u).Count();

                    string dd = string.Format("{0:d}", n.VisitDate);
                    lblTotalCount.Text = " تعداد بازدید کل " + t.ToString() + " در تاریخ " + dd;
                }

                btnReturn.Visible = true;
            }
            else
            {
                DateTime d = DateTime.Now;
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.VisitCounter
                             where EntityFunctions.TruncateTime(u.VisitDate) == d.Date
                             select u).ToList();

                    GrdTable.DataSource = n;
                    GrdTable.DataBind();

                    var m = (from u in db.VisitCounter where EntityFunctions.TruncateTime(u.VisitDate) == d.Date select u).Count();
                    string dd = string.Format("{0:d}", d);

                    lblTotalCount.Text = " تعداد بازدید کل " + m.ToString() + " در تاریخ " + dd;
                }
            }


        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("DailyVisit.aspx");
        }

        protected void GrdTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdTable.PageIndex = e.NewPageIndex;
            LoadForm();
        }
    }
}