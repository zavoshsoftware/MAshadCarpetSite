using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MashadCarpet.Models;
using System.IO;

namespace MashadCarpet.Admin
{
    public partial class SliderSetting : System.Web.UI.Page
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
                var n = (from u in db.Slider select u).ToList();

                grdTable.DataSource = n;
                grdTable.DataBind();
            }

            mvSetting.SetActiveView(vwList);
        }

        public void EmptyForm()
        {
         
            txtTitle.Text = string.Empty;
            ImgSlider.Visible = false;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            ViewState["btn"] = "Insert";
            EmptyForm();
            mvSetting.SetActiveView(vwEdit);
        }

        protected void btnAgree_Click(object sender, EventArgs e)
        {
            int SLiderID =int.Parse(ViewState["SliderID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Slider where u.SliderID == SLiderID select u).FirstOrDefault();
                db.Slider.Remove(n);
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
            if (ViewState["btn"].ToString() == "Insert")
                Insert();
            else if (ViewState["btn"].ToString() == "Edit")
                Update();
            LoadForm();
        }

        public void Update()
        {
            string new_filename = string.Empty;

            if (fuImg.PostedFile.ContentLength != 0)
            {
                string original_filename = Path.GetFileName(fuImg.PostedFile.FileName);

                new_filename =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_filename);

                string new_filepath = Server.MapPath("~/Uploads/Sliders/" + new_filename);
                fuImg.PostedFile.SaveAs(new_filepath);
                ViewState["NewImg"] = new_filename;
            }


            int SLiderID = int.Parse(ViewState["SliderID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Slider where u.SliderID == SLiderID select u).FirstOrDefault();

             
                n.SliderTitle = txtTitle.Text;
                n.SliderImage = ViewState["NewImg"].ToString();

                db.SaveChanges();
            }
        }

        public void Insert()
        {
            string new_filename = string.Empty;

            if (fuImg.PostedFile.ContentLength != 0)
            {
                string original_filename = Path.GetFileName(fuImg.PostedFile.FileName);

                new_filename =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_filename);

                string new_filepath = Server.MapPath("~/Uploads/Sliders/" + new_filename);
                fuImg.PostedFile.SaveAs(new_filepath);
                ViewState["NewImg"] = new_filename;
            }

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                Slider s = new Slider();

              
                s.SliderTitle = txtTitle.Text;
           
                s.SliderImage = new_filename;

                db.Slider.Add(s);
                db.SaveChanges();

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
        }

        protected void grdTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int SLiderID = int.Parse(e.CommandArgument.ToString());
            ViewState["SliderID"] = SLiderID;

            switch (e.CommandName)
            {
                case "DoEdit":
                    {
                        using (MashadCarpetEntities db = new MashadCarpetEntities())
                        {
                            var n = (from u in db.Slider where u.SliderID == SLiderID select u).FirstOrDefault();
                       
                            txtTitle.Text = n.SliderTitle;
                            ViewState["NewImg"] = n.SliderImage;
                            ImgSlider.ImageUrl = "~/Uploads/Sliders/" + n.SliderImage;
                            ImgSlider.Visible = true;
                        }

                        ViewState["btn"] = "Edit";
                        mvSetting.SetActiveView(vwEdit);
                        break;
                    }
                case "DoDelete":
                    {
                        using (MashadCarpetEntities db = new MashadCarpetEntities())
                        {
                            var n = (from u in db.Slider where u.SliderID == SLiderID select u).FirstOrDefault();
                            lblDelete.Text = n.SliderTitle;
                            mvSetting.SetActiveView(vwDelete);

                        }
                        break;
                    }
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}