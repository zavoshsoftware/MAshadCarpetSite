using MashadCarpet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MashadCarpet.Admin
{
    public partial class TextSetting : System.Web.UI.Page
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
            if (Request.QueryString["ID"] != null)
            {
                int ID = int.Parse(Request.QueryString["ID"].ToString());

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.Texts where u.TextID == ID && u.IsDelete == false select u).ToList();

                    grdTable.DataSource = n;
                    grdTable.DataBind();

                }


            }
            else if (Request.QueryString["GroupID"] != null)
            {
                int ID = int.Parse(Request.QueryString["GroupID"].ToString());
               // if (ID == 1)
               // {
               //     using (MashadCarpetEntities db = new MashadCarpetEntities())
               //     {

               //         var n = (from u in db.Texts where (u.TextID == 1059 || u.TextID == 1060 || u.TextID == 1061 || u.TextID == 1062 || u.TextID == 1063) && u.IsDelete == false select u).ToList();

               //         grdTable.DataSource = n;
               //         grdTable.DataBind();

               //     }
               // }
               //else if (ID == 2)
               // {
               //     using (MashadCarpetEntities db = new MashadCarpetEntities())
               //     {
               //         var n = (from u in db.Texts where (u.TextID == 1064 || u.TextID == 1065) && u.IsDelete == false select u).ToList();

               //         grdTable.DataSource = n;
               //         grdTable.DataBind();

               //     }
               // }
               // else
               // {
                    using (MashadCarpetEntities db = new MashadCarpetEntities())
                    {
                        var n = (from u in db.Texts where u.GroupID == ID && u.IsDelete == false select u).ToList();

                        grdTable.DataSource = n;
                        grdTable.DataBind();
                        btnAdd.Visible = true;
                    }
               // }
            }
            mvSetting.SetActiveView(vwList);
        }

        public void EmptyForm()
        {

            txtTitle.Text = string.Empty;
            txtName.Text = string.Empty;
            txtEN_Title.Text = string.Empty;
            reDesc.Content = null;
            reEN_Desc.Content = null;
            txtChinaTitle.Text = string.Empty;
            txtRusTitle.Text = string.Empty;
            reChina_Text.Content = null;
            reRus_Desc.Content = null;

        }



        protected void btnAgree_Click(object sender, EventArgs e)
        {
            int TextID = int.Parse(ViewState["TextID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Texts where u.TextID == TextID select u).FirstOrDefault();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (ViewState["btnMode"].ToString() == "Edit")
                    UpdateForm();

                else if (ViewState["btnMode"].ToString() == "Insert")
                    InsertForm();
                LoadForm();
            }

        }



        public void UpdateForm()
        {
            int TextID = int.Parse(ViewState["TextID"].ToString());
            string new_filename = string.Empty;

            if (fuImg.PostedFile.ContentLength != 0)
            {
                string original_filename = Path.GetFileName(fuImg.PostedFile.FileName);

                new_filename =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_filename);

                string new_filepath = Server.MapPath("~/Uploads/TextImages/" + new_filename);
                fuImg.PostedFile.SaveAs(new_filepath);
                ViewState["NewImg"] = new_filename;
            }
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Texts where u.TextID == TextID select u).FirstOrDefault();

                n.TextTitle = txtTitle.Text;
                n.TextName = txtName.Text;
                n.TextDescription = reDesc.Content;
                n.EN_TextTitle = txtEN_Title.Text;
                n.EN_TextDescription = reEN_Desc.Content;
                n.Rus_TextTitle = txtRusTitle.Text;
                n.Rus_TextDescription = reRus_Desc.Content;
                n.China_TextTitle = txtChinaTitle.Text;
                n.China_TextDescription = reChina_Text.Content;
                if (ViewState["NewImg"] != null)
                    n.TextImage = ViewState["NewImg"].ToString();

                db.SaveChanges();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
        }

        protected void grdTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            switch (e.CommandName)
            {


                case "DoEdit":
                    {
                        int TextID = int.Parse(e.CommandArgument.ToString());
                        ViewState["TextID"] = TextID;
                        ViewState["btnMode"] = "Edit";
                        FillForm();

                        break;
                    }

                case "DoDelete":
                    {
                        int TextID = int.Parse(e.CommandArgument.ToString());
                        ViewState["TextID"] = TextID;
                        using (MashadCarpetEntities db = new MashadCarpetEntities())
                        {
                            var n = (from u in db.Texts where u.TextID == TextID select u).FirstOrDefault();
                            lblDelete.Text = n.TextTitle;
                            mvSetting.SetActiveView(vwDelete);
                        }
                        break;
                    }

                case "ManageAbout":
                    {
                        int GroupID = int.Parse(e.CommandArgument.ToString());
                        Response.Redirect("TextSetting.aspx?GroupID=" + GroupID);
                        break;
                    }
            }
        }

        public void FillForm()
        {

            int TextID = int.Parse(ViewState["TextID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Texts where u.TextID == TextID select u).FirstOrDefault();

                txtTitle.Text = n.TextTitle;
                txtName.Text = n.TextName;
                reDesc.Content = n.TextDescription;
                txtEN_Title.Text = n.EN_TextTitle;
                reEN_Desc.Content = n.EN_TextDescription;
                reChina_Text.Content = n.China_TextDescription;
                txtChinaTitle.Text = n.China_TextTitle;
                reRus_Desc.Content = n.Rus_TextDescription;
                txtRusTitle.Text = n.Rus_TextTitle;
                ViewState["NewImg"] = n.TextImage;
            }
            mvSetting.SetActiveView(vwEdit);
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        public void InsertForm()
        {
            int ID = int.Parse(Request.QueryString["GroupID"].ToString());
            string new_filename = string.Empty;

            if (fuImg.PostedFile.ContentLength != 0)
            {
                string original_filename = Path.GetFileName(fuImg.PostedFile.FileName);

                new_filename =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_filename);

                string new_filepath = Server.MapPath("~/Uploads/TextImages/" + new_filename);
                fuImg.PostedFile.SaveAs(new_filepath);
                ViewState["NewImg"] = new_filename;
            }
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                Texts n = new Texts();

                n.TextTitle = txtTitle.Text;
                n.TextName = txtName.Text;
                n.TextDescription = reDesc.Content;
                n.EN_TextTitle = txtEN_Title.Text;
                n.EN_TextDescription = reEN_Desc.Content;
                n.GroupID = ID;
                n.IsDelete = false;
                n.TextImage = new_filename;
                n.Rus_TextTitle = txtRusTitle.Text;
                n.Rus_TextDescription = reRus_Desc.Content;
                n.China_TextTitle = txtChinaTitle.Text;
                n.China_TextDescription = reChina_Text.Content;

                db.Texts.Add(n);
                db.SaveChanges();
            }
            LoadForm();
            EmptyForm();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ViewState["btnMode"] = "Insert";
            EmptyForm();
            //txtEN_Title.Text = string.Empty;
            //txtTitle.Text = string.Empty;
            //txtName.Text = string.Empty;
            //reDesc.Content = null;
            //reEN_Desc.Content = null;
            mvSetting.SetActiveView(vwEdit);

        }

        protected void grdTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Request.QueryString["GroupID"] != null)
            {
                int ID = int.Parse(Request.QueryString["GroupID"].ToString());
                if (ID == 1 || ID==2)
                {
                    foreach (GridViewRow r in grdTable.Rows)
                    {
                        Panel pnlAbout = (Panel)r.FindControl("pnlAbout");
                        pnlAbout.Visible = true;
                    }


                }
                foreach (GridViewRow r in grdTable.Rows)
                {
                    Panel pnlDelete = (Panel)r.FindControl("pnlDelete");
                    pnlDelete.Visible = true;
                }

            }
            //if (Request.QueryString["ID"] != null)
            //{
            //    int ID = int.Parse(Request.QueryString["ID"].ToString());

            //    if (ID == 1)
            //    {
            //        foreach (GridViewRow r in grdTable.Rows)
            //        {
            //            Panel pnlAbout = (Panel)r.FindControl("pnlAbout");
            //            pnlAbout.Visible = true;
            //        }


            //    }
            //}
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Texts where u.TextName == txtName.Text && u.IsDelete == false select u).FirstOrDefault();

                if (ViewState["btnMode"].ToString() == "Insert")
                {
                    args.IsValid = n == null;
                }
                else if (ViewState["btnMode"].ToString() == "Edit")
                {
                    int TextID = int.Parse(ViewState["TextID"].ToString());

                    var m = (from u in db.Texts where u.TextID == TextID select u).FirstOrDefault();

                    if (m.TextName == txtName.Text)
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