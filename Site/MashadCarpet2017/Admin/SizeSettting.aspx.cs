using MashadCarpet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MashadCarpet.Admin
{
    public partial class SizeSettting : System.Web.UI.Page
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
                var n = (from u in db.SIzes
                         where u.IsDelete == false
                         select u).ToList();

                grdTable.DataSource = n;
                grdTable.DataBind();
                if (n.Count == 0)
                    pnlEmptyForm.Visible = true;
                else
                    pnlEmptyForm.Visible = false;
            }

            mvSetting.SetActiveView(vwList);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            mvSetting.SetActiveView(vwEdit);
            ViewState["btn"] = "Insert";
            ResetForm();
        }
        public void ResetForm()
        {
            txtTitle.Text = string.Empty;
            //txtName.Text = string.Empty;
            //txtENTitle.Text = string.Empty;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (ViewState["btn"].ToString() == "Update")
                    UpdateForm();
                else if (ViewState["btn"].ToString() == "Insert")
                    InsertForm();


            }
            LoadForm();
        }
        public void UpdateForm()
        {

            int SizeID = int.Parse(ViewState["SizeID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var p = (from u in db.SIzes where u.SizeID == SizeID select u).FirstOrDefault();

                //p.SizeEN_Title = txtENTitle.Text;
                p.SizeTitle = txtTitle.Text;
                //p.SizeName = txtName.Text;
               
                db.SaveChanges();
            }
        }
        public void InsertForm()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                SIzes p = new SIzes();
                //p.SizeEN_Title = txtENTitle.Text;
                p.SizeTitle = txtTitle.Text;
                //p.SizeName = txtName.Text;
                p.IsDelete = false;

                db.SIzes.Add(p);
                db.SaveChanges();




            }
        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {

            Response.Redirect("Default.aspx");


        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            int SizeID = int.Parse(ViewState["SizeID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.SIzes where u.SizeID == SizeID select u).FirstOrDefault();

                n.IsDelete = true;
                n.DeleteDate = DateTime.Now;
                db.SaveChanges();
            }
            LoadForm();
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
        }

        protected void grdTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            if (e.CommandName == "DoEdit")
            {
                int SizeID = int.Parse(e.CommandArgument.ToString());
                ViewState["SizeID"] = SizeID;
                ViewState["btn"] = "Update";

                FillViewEdit(SizeID);
            }
            else if (e.CommandName == "DoDelete")
            {
                int SizeID = int.Parse(e.CommandArgument.ToString());
                ViewState["SizeID"] = SizeID;
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.SIzes where u.SizeID == SizeID select u).FirstOrDefault();
                    lblDelete.Text = n.SizeTitle;
                    mvSetting.SetActiveView(vwDelete);
                }
            }

        }

        public void FillViewEdit(int SizeID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.SIzes where u.SizeID == SizeID select u).FirstOrDefault();
                txtTitle.Text = n.SizeTitle;
                //txtName.Text = n.SizeName;
                //txtENTitle.Text = n.SizeEN_Title;


                mvSetting.SetActiveView(vwEdit);
            }
        }

        protected void cvName_ServerValidate(object source, ServerValidateEventArgs args)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.SIzes where u.SizeTitle == txtTitle.Text && u.IsDelete == false select u).FirstOrDefault();

                if (ViewState["btn"].ToString() == "Insert")
                {
                    args.IsValid = n == null;
                }
                else if (ViewState["btn"].ToString() == "Update")
                {
                    int SizeID = int.Parse(ViewState["SizeID"].ToString());

                    var m = (from u in db.SIzes where u.SizeID == SizeID select u).FirstOrDefault();

                    if (m.SizeTitle == txtTitle.Text)
                    {
                        args.IsValid = true;
                    }
                    else
                    {
                        args.IsValid = n == null;
                    }
                }
            }
        }


    }
}