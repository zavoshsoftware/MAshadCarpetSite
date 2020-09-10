using MashadCarpet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MashadCarpet.Admin
{
    public partial class NewsLetterSetting : System.Web.UI.Page
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
                var n = (from u in db.NewsLetters
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
            ViewState["btn"] = "Insert";
            EmptyForm();
            mvSetting.SetActiveView(vwEdit);
        }
        public void EmptyForm()
        {
            txtEmail.Text = string.Empty;
            cbIsValid.Checked = false;
        }
        protected void btnRet_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void grdProductGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid NewsLetterID = new Guid(e.CommandArgument.ToString());
            ViewState["NewsLetterID"] = NewsLetterID;

            switch (e.CommandName)
            {
                case "DoEdit":
                    {
                        ViewState["btn"] = "Update";

                        FillViewEdit(NewsLetterID);
                        break;
                    }
                case "DoDelete":
                    {
                        using (MashadCarpetEntities db = new MashadCarpetEntities())
                        {
                            var n = (from u in db.NewsLetters where u.NewsLetterID == NewsLetterID select u).FirstOrDefault();
                            lblDelete.Text = n.NewsLetterEmail;

                            mvSetting.SetActiveView(vwDelete);
                        }
                        break;
                    }



            }
        }


        public void FillViewEdit(Guid NewsLetterID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.NewsLetters where u.NewsLetterID == NewsLetterID select u).FirstOrDefault();
                txtEmail.Text = n.NewsLetterEmail;
                cbIsValid.Checked = Convert.ToBoolean(n.IsValid);
                mvSetting.SetActiveView(vwEdit);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ViewState["btn"].ToString() == "Update")
                Update();
            else if (ViewState["btn"].ToString() == "Insert")
                Insert();

            LoadForm();
        }

        public void Insert()
        {

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                NewsLetters u = new NewsLetters();

                u.NewsLetterID = Guid.NewGuid();
                u.NewsLetterEmail = txtEmail.Text;
                u.SubmitDate = DateTime.Now;
                u.SubmitIP = Request.UserHostAddress;
                u.IsDelete = false;
                u.IsValid = cbIsValid.Checked;

                db.NewsLetters.Add(u);
                db.SaveChanges();
            }

        }

        public void Update()
        {
            Guid NewsLetterID = new Guid(ViewState["NewsLetterID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var u = (from n in db.NewsLetters where n.NewsLetterID == NewsLetterID select n).FirstOrDefault();

                u.NewsLetterEmail = txtEmail.Text;
                u.IsValid = cbIsValid.Checked;

                db.SaveChanges();
            }
        }

        protected void btnAgree_Click(object sender, EventArgs e)
        {

            Guid NewsLetterID = new Guid(ViewState["NewsLetterID"].ToString());
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.NewsLetters where u.NewsLetterID == NewsLetterID select u).FirstOrDefault();

                n.IsDelete = true;
                n.DeleteDate = DateTime.Now;

                db.SaveChanges();

                LoadForm();
            }
        }

        protected void btnDeny_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
        }
    }
}