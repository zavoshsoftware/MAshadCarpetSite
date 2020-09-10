using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MashadCarpet.Models;

namespace MashadCarpet.Admin
{
    public partial class OrderStatusSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                LoadForm();
            }
        }

        public void LoadForm()
        {
            using(MashadCarpetEntities db=new MashadCarpetEntities())
            {
                var n = (from u in db.OrderStatus where u.IsDelete == false select u).ToList();
                grdTable.DataSource = n;
                grdTable.DataBind();
            }
            mvSetting.SetActiveView(vwList);
        }

        protected void grdTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int OrderStatusID = int.Parse(e.CommandArgument.ToString());
            ViewState["OrderStatusID"] = OrderStatusID;

            if (e.CommandName == "DoEdit")
            {
                ViewState["btn"] = "update";
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.OrderStatus where u.OrderStatusID == OrderStatusID select u).FirstOrDefault();

                    txtStatus.Text = n.OrderStatusTitle;
                    txtChina_Status.Text = n.China_OrderStatus;
                    txtEN_Status.Text = n.EN_OrderStatus;
                    txtRus_Status.Text = n.Rus_OrderStatus;

                    mvSetting.SetActiveView(vwEdit);
                }
            }

            else if (e.CommandName == "DoDelete")
            {
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.OrderStatus where u.OrderStatusID == OrderStatusID select u).FirstOrDefault();
                    lblDelete.Text = n.OrderStatusTitle;

                    mvSetting.SetActiveView(vwDelete);
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ViewState["btn"] = "Insert";
            txtStatus.Text = string.Empty;
            txtRus_Status.Text = string.Empty;
            txtEN_Status.Text = string.Empty;
            txtChina_Status.Text = string.Empty;
            mvSetting.SetActiveView(vwEdit);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Default.aspx");
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ViewState["btn"].ToString() == "update")
                Update();
            else if (ViewState["btn"].ToString() == "Insert")
                Insert();
            LoadForm();
        }

        public void Insert()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                OrderStatus o = new OrderStatus();
                o.OrderStatusTitle = txtStatus.Text;
                o.IsDelete = false;
                o.Rus_OrderStatus = txtRus_Status.Text;
                o.EN_OrderStatus = txtEN_Status.Text;
                o.China_OrderStatus = txtChina_Status.Text;

                db.OrderStatus.Add(o);
                db.SaveChanges();
            }
        }

        public void Update()
        {
            int OrderStatusID = int.Parse(ViewState["OrderStatusID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.OrderStatus where u.OrderStatusID == OrderStatusID select u).FirstOrDefault();
                n.OrderStatusTitle = txtStatus.Text;
                n.Rus_OrderStatus = txtRus_Status.Text;
                n.EN_OrderStatus = txtEN_Status.Text;
                n.China_OrderStatus = txtChina_Status.Text;
                db.SaveChanges();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
        }

        protected void btnAgree_Click(object sender, EventArgs e)
        {
            int OrderStatusID = int.Parse(ViewState["OrderStatusID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.OrderStatus where u.OrderStatusID == OrderStatusID select u).FirstOrDefault();
                n.IsDelete = true;
                n.DeleteDate = DateTime.Now;

                db.SaveChanges();
            }
            LoadForm();
        }

        protected void btnDeny_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
        }
    }
}