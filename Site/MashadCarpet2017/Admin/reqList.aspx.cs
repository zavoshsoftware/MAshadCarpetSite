using MashadCarpet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MashadCarpet.Admin
{
    public partial class reqList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }
        public void LoadData()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from a in db.AgentReq
                         where a.IsDelete == false
                         select a).ToList();

                GridView1.DataSource = n;
                GridView1.DataBind();
                if (n.Count == 0)
                    pnlEmptyForm.Visible = true;
                else
                    pnlEmptyForm.Visible = false;
            }
        }
        protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Det")
            {
                int AgentID = Convert.ToInt32(e.CommandArgument);
                pnlDetails.Visible = true;
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from a in db.AgentReq
                             where a.AgentID == AgentID
                             select a).FirstOrDefault();

                    if (n != null)
                    {
                        lblAddress.Text = n.Address;
                        lblBirthday.Text = n.Birthday;
                        lblBirthPlace.Text = n.BirthPlace;
                        lblCity.Text = n.city;
                        lblDesc.Text = n.description;
                        lblEmail.Text = n.email;
                        lblFathername.Text = n.FatherName;
                        lblFax.Text = n.fax;
                        //if (n.IsMale != null)
                        //{
                        //    if (n.IsMale == true)
                        //        lblGender.Text = "مذکر";
                        //    else if (n.IsMale == false)
                        //        lblGender.Text = "مونث";
                        //}
                        lblLastName.Text = n.LastName;
                        //    lblMobile.Text = n.mobile;
                        lblName.Text = n.FirstName;
                        lblNational.Text = n.NationalID;
                        lblPhone.Text = n.phone;
                        lblPostalCode.Text = n.PostalCode;
                        lblStartDate.Text = n.startDate;
                        lblStoreName.Text = n.CompanyName;
                        lblWebsite.Text = n.website;
                    }
                }
            }
            if (e.CommandName == "Del")
            {
                int AgentID = Convert.ToInt32(e.CommandArgument);
                ViewState["AgentID"] = AgentID;

                pnlDetails.Visible = false;
                pnlDelete.Visible = true;

            }

        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            int AgentID = Convert.ToInt32(ViewState["AgentID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from a in db.AgentReq
                         where a.AgentID == AgentID
                         select a).FirstOrDefault();

                n.IsDelete = true;

                n.DeleteDate = DateTime.Now;

                db.SaveChanges();
                pnlDetails.Visible = false;
                pnlDelete.Visible = false;
                LoadData();
            }

        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            pnlDetails.Visible = false;
            pnlDelete.Visible = false;
            LoadData();
        }
    }
}