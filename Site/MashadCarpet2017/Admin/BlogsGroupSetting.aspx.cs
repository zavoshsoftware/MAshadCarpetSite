using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MashadCarpet.Models;

namespace MashadCarpet.Admin
{
    public partial class BlogsGroupSetting : System.Web.UI.Page
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
                var n = (from u in db.BlogGroups
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
            txtTitle.Text = string.Empty;
            txtName.Text = string.Empty;
            txtEN_Title.Text = string.Empty;
            txtRus_Title.Text = string.Empty;
            txtChina_Title.Text = string.Empty;

            mvSetting.SetActiveView(vwEdit);
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");

        }

        protected void grdProductGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid BlogsGroupID = new Guid(e.CommandArgument.ToString());
            ViewState["BlogsGroupID"] = BlogsGroupID;

            switch (e.CommandName)
            {
                case "DoEdit":
                    {
                        ViewState["btn"] = "Update";

                        FillViewEdit(BlogsGroupID);
                        break;
                    }
                case "DoDelete":
                    {
                        using (MashadCarpetEntities db = new MashadCarpetEntities())
                        {
                            var n = (from u in db.BlogGroups where u.BlogGroupID == BlogsGroupID select u).FirstOrDefault();
                            lblDelete.Text = n.BlogGroupTitle;

                            mvSetting.SetActiveView(vwDelete);
                        }
                        break;
                    }
                case "Blogs":
                    {
                        Response.Redirect("BlogsSetting.aspx?ID=" + BlogsGroupID);
                        break;
                    }

            }
        }

        public void FillViewEdit(Guid BlogsGroupID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.BlogGroups where u.BlogGroupID == BlogsGroupID select u).FirstOrDefault();
                txtTitle.Text = n.BlogGroupTitle;
                txtName.Text = n.BlogGroupName;
                txtEN_Title.Text = n.EN_BlogGroupTitle;
                txtRus_Title.Text = n.Rus_BlogGroupTitle;
                txtChina_Title.Text = n.China_BlogGroupTitle;

                mvSetting.SetActiveView(vwEdit);

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (ViewState["btn"].ToString() == "Update")
                    UpdateForm();
                else if (ViewState["btn"].ToString() == "Insert")
                    InsertForm();

                LoadForm();
            }
        }

        public void InsertForm()
        {


            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                BlogGroups pg = new BlogGroups();
                pg.BlogGroupID = Guid.NewGuid();
                pg.BlogGroupTitle = txtTitle.Text;
                pg.BlogGroupName = txtName.Text;
                pg.IsDelete = false;
                pg.EN_BlogGroupTitle = txtEN_Title.Text;
                pg.Rus_BlogGroupTitle = txtRus_Title.Text;
                pg.China_BlogGroupTitle = txtChina_Title.Text;

                db.BlogGroups.Add(pg);
                db.SaveChanges();
            }

        }

        public void UpdateForm()
        {
            Guid BlogsGroupID = new Guid(ViewState["BlogsGroupID"].ToString());


            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.BlogGroups where u.BlogGroupID == BlogsGroupID select u).FirstOrDefault();

                n.BlogGroupTitle = txtTitle.Text;
                n.BlogGroupName = txtName.Text;
                n.EN_BlogGroupTitle = txtEN_Title.Text;
                n.Rus_BlogGroupTitle = txtRus_Title.Text;
                n.China_BlogGroupTitle = txtChina_Title.Text;

                db.SaveChanges();
            }
        }

        protected void btnAgree_Click(object sender, EventArgs e)
        {

            Guid BlogsGroupID = new Guid(ViewState["BlogsGroupID"].ToString());
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.BlogGroups where u.BlogGroupID == BlogsGroupID select u).FirstOrDefault();

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

        protected void cvName_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Boolean UserNameExist = true;
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {

                var u = (from b in db.BlogGroups where b.IsDelete==false select b.BlogGroupName).ToList();

                foreach (var item in u)
                {
                    if (txtName.Text == item)
                        UserNameExist = false;
                }

            }
            if (ViewState["btn"].ToString() == "Update")
                args.IsValid = true;
            else
                args.IsValid = UserNameExist;
        }

    }
}