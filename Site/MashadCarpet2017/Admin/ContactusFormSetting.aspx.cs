using MashadCarpet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MashadCarpet.Admin
{
    public partial class ContactusFormSetting : System.Web.UI.Page
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

                    var n = (from u in db.ContactUSForm where u.IsDelete==false select u).ToList();

                    grdTable.DataSource = n;
                    grdTable.DataBind();

                }
        

            mvSetting.SetActiveView(vwList);
        }

       


        protected void btnAgree_Click(object sender, EventArgs e)
        {
            Guid ID = new Guid(ViewState["ID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.ContactUSForm where u.ContactusID == ID select u).FirstOrDefault();
                db.ContactUSForm.Remove(n);
                db.SaveChanges();
                LoadForm();


            }
        }

        protected void btnDeny_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            //UpdateForm();
            LoadForm();
        }



     
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
        }

        protected void grdTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid ID = new Guid(e.CommandArgument.ToString());

            ViewState["ID"] = ID;

            switch (e.CommandName)
            {


                case "ShowMsg":
                    {
                        //ViewState["btnMode"] = "Edit";
                        FillForm();

                        break;
                    }
                case"DoDelete":
                    {
                        using (MashadCarpetEntities db = new MashadCarpetEntities())
                        {
                            var n = (from u in db.ContactUSForm where u.ContactusID == ID select u).FirstOrDefault();
                            lblDelete.Text = n.ContactusName;
                            mvSetting.SetActiveView(vwDelete);
                        }

                        break;
                    }
            }
        }

        public void FillForm()
        {

            Guid ID = new Guid(ViewState["ID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.ContactUSForm where u.ContactusID == ID select u).FirstOrDefault();

                lblEmail.Text = n.ContactusEmail;
                lblName.Text = n.ContactusName;
                lblMsg.Text = n.ContactusMsg;
            }
            mvSetting.SetActiveView(vwShow);
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
        }
    }
}