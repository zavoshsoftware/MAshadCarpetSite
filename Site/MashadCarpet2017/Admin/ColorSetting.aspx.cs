using MashadCarpet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MashadCarpet.Admin
{
    public partial class ColorSetting : System.Web.UI.Page
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
                var n = (from u in db.Colors
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
            txtName.Text = string.Empty;
            txtENTitle.Text = string.Empty;
            txtColorNo.Text = string.Empty;
            txtChinaTitle.Text = string.Empty;
            txtRusTitle.Text = string.Empty;

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

            int ColorID = int.Parse(ViewState["ColorID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var p = (from u in db.Colors where u.ColorID == ColorID select u).FirstOrDefault();

                p.ColorEN_Title = txtENTitle.Text;
                p.ColorTitle = txtTitle.Text;
                p.ColorName = txtName.Text;
                p.ColorNo = "#" + txtColorNo.Text;
                p.Rus_ColorTitle = txtRusTitle.Text;
                p.China_ColorTitle = txtChinaTitle.Text;

                db.SaveChanges();
            }
        }
        public void InsertForm()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                Colors p = new Colors();
                p.ColorEN_Title = txtENTitle.Text;
                p.ColorTitle = txtTitle.Text;
                p.ColorName = txtName.Text;
                p.IsDelete = false;
                p.ColorNo = "#" + txtColorNo.Text;
                p.Rus_ColorTitle = txtRusTitle.Text;
                p.China_ColorTitle = txtChinaTitle.Text;

                db.Colors.Add(p);
                db.SaveChanges();




            }
        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {

            Response.Redirect("Default.aspx");


        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            int ColorID = int.Parse(ViewState["ColorID"].ToString());


            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Colors where u.ColorID == ColorID select u).FirstOrDefault();

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
                int ColorID = int.Parse(e.CommandArgument.ToString());
                ViewState["ColorID"] = ColorID;
                ViewState["btn"] = "Update";

                FillViewEdit(ColorID);
            }
            else if (e.CommandName == "DoDelete")
            {
                int ColorID = int.Parse(e.CommandArgument.ToString());
                ViewState["ColorID"] = ColorID;
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.Colors where u.ColorID == ColorID select u).FirstOrDefault();
                    lblDelete.Text = n.ColorTitle;
                    mvSetting.SetActiveView(vwDelete);
                }
            }

        }

        public void FillViewEdit(int ColorID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Colors where u.ColorID == ColorID select u).FirstOrDefault();
                txtTitle.Text = n.ColorTitle;
                txtName.Text = n.ColorName;
                txtENTitle.Text = n.ColorEN_Title;
                txtColorNo.Text = n.ColorNo;
                txtChinaTitle.Text = n.China_ColorTitle;
                txtRusTitle.Text = n.Rus_ColorTitle;

                mvSetting.SetActiveView(vwEdit);
            }
        }

        protected void cvName_ServerValidate(object source, ServerValidateEventArgs args)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Colors where u.ColorName == txtName.Text && u.IsDelete == false select u).FirstOrDefault();

                if (ViewState["btn"].ToString() == "Insert")
                {
                    args.IsValid = n == null;
                }
                else if (ViewState["btn"].ToString() == "Update")
                {
                    int ColorID = int.Parse(ViewState["ColorID"].ToString());

                    var m = (from u in db.Colors where u.ColorID == ColorID select u).FirstOrDefault();

                    if (m.ColorName == txtName.Text)
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