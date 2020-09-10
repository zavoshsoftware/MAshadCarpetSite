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
    public partial class DailyVisit : System.Web.UI.Page
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
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.VisitCounter
                         group u by EntityFunctions.TruncateTime(u.VisitDate) into g
                         orderby g.FirstOrDefault().VisitDate descending
                         select new
                         {
                             g.FirstOrDefault().VisitDate,

                             g.FirstOrDefault().VisitCount,
                             g.FirstOrDefault().TotalVisit,

                             g.FirstOrDefault().VisitID

                         }).ToList();

                GrdTable.DataSource = n;
                GrdTable.DataBind();
            }
        }

        protected void GrdTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int VisitID = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "DoEdit":
                    {
                        Response.Redirect("TodayVisitSetting.aspx?VisitID=" + VisitID);
                        break;
                    }
            }
        }

        protected void GrdTable_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow r in GrdTable.Rows)
            {
                HiddenField hfVisitDate = (HiddenField)r.FindControl("hfVisitDate");

                DateTime VisitDate = Convert.ToDateTime(hfVisitDate.Value).Date;

                Label lblCount = (Label)r.FindControl("lblCount");

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.VisitCounter where EntityFunctions.TruncateTime(u.VisitDate) == VisitDate select u).Count();

                    lblCount.Text = n.ToString();

                }
            }
        }
    }
}