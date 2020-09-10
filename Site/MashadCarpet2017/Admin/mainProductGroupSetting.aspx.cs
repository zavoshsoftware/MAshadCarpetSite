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
    public partial class mainProductGroupSetting : System.Web.UI.Page
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
                var n = (from u in db.ProductGroup
                         where u.IsDelete == false
                         && u.fk_ProductGroupID == null
                         orderby u.Priority
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
            reDesc.Content = string.Empty;
            txtEN_Title.Text = string.Empty;
            reEN_Desc.Content = null;
            txtPrio.Text = string.Empty;
            imgEditImages.Visible = false;
            txtChina_Title.Text = string.Empty;
            txtRus_Title.Text = string.Empty;
            reChina_Desc.Content = null;
            reRus_Desc.Content = null;
            chkAlienPro.Checked = false;
            mvSetting.SetActiveView(vwEdit);
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void grdProductGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid ProductGroupID = new Guid(e.CommandArgument.ToString());
            ViewState["ProductGroupID"] = ProductGroupID;

            switch (e.CommandName)
            {
                case "DoEdit":
                    {
                        ViewState["btn"] = "Update";
                        FillViewEdit(ProductGroupID);
                        break;
                    }
                case "DoDelete":
                    {
                        using (MashadCarpetEntities db = new MashadCarpetEntities())
                        {
                            var n = (from u in db.ProductGroup where u.ProductGroupID == ProductGroupID select u).FirstOrDefault();
                            lblDelete.Text = n.ProductGroupTitle;

                            mvSetting.SetActiveView(vwDelete);
                        }
                        break;
                    }
            }
        }

        public void FillViewEdit(Guid ProductGroupID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.ProductGroup where u.ProductGroupID == ProductGroupID select u).FirstOrDefault();
                txtTitle.Text = n.ProductGroupTitle;
                txtName.Text = n.ProductGroupName;
                ViewState["GImage"] = n.ProductGroupImage;
                reDesc.Content = n.ProductGroupDesc;
                txtEN_Title.Text = n.EN_ProductGroupTitle;
                reEN_Desc.Content = n.EN_ProductGroupDesc;
                txtPrio.Text = n.Priority.ToString();
                //  txtcolor2.Text = n.HomeTitleColor2;
                //    txtTitle2.Text = n.ProductGroupTitle2;

                //    txtEN_Title2.Text = n.EN_ProductGroupTitle2;
                mvSetting.SetActiveView(vwEdit);
                imgEditImages.ImageUrl = "~/Uploads/ProductGroup/" + n.ProductGroupImage;
                imgEditImages_Slider.ImageUrl = "~/Uploads/ProductGroup/" + n.imgSliderImage;
                ViewState["GImage_Slider"] = n.imgSliderImage;
                //  txtChina_Title2.Text = n.China_ProductGroupTitle2;
                txtChina_Title.Text = n.China_ProductGroupTitle;
                reChina_Desc.Content = n.China_ProductGroupDesc;

                txtRus_Title.Text = n.Rus_ProductGroupTitle;
                //  txtRus_Title2.Text = n.Rus_ProductGroupTitle2;
                reRus_Desc.Content = n.Rus_ProductGroupDesc;
                chkAlienPro.Checked = Convert.ToBoolean(n.IsAlienCulture);
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
                pnlSuccess.Visible = true;
            }
        }

        public void InsertForm()
        {
            string new_filename = string.Empty;

            if (fuImage.PostedFile.ContentLength != 0)
            {
                string original_filename = Path.GetFileName(fuImage.PostedFile.FileName);

                new_filename =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_filename);

                string new_filepath = Server.MapPath("~/Uploads/ProductGroup/" + new_filename);
                fuImage.PostedFile.SaveAs(new_filepath);
                ViewState["GImage"] = new_filename;
            }
            string new_filename_Slider = string.Empty;

            if (fuImage_Slider.PostedFile.ContentLength != 0)
            {
                string original_filename = Path.GetFileName(fuImage_Slider.PostedFile.FileName);

                new_filename_Slider =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_filename);

                string new_filepath = Server.MapPath("~/Uploads/ProductGroup/" + new_filename_Slider);
                fuImage_Slider.PostedFile.SaveAs(new_filepath);
                ViewState["GImage_Slider"] = new_filename_Slider;
            }

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                ProductGroup pg = new ProductGroup();
                pg.ProductGroupID = Guid.NewGuid();
                pg.ProductGroupTitle = txtTitle.Text;
                pg.ProductGroupName = txtName.Text;
                pg.ProductGroupImage = new_filename;
                pg.ProductGroupDesc = reDesc.Content;
                pg.IsDelete = false;
                pg.EN_ProductGroupTitle = txtEN_Title.Text;
                pg.EN_ProductGroupDesc = reEN_Desc.Content;
                pg.imgSliderImage = new_filename_Slider;
                pg.Priority = Convert.ToInt32(txtPrio.Text);
                pg.Rus_ProductGroupDesc = reRus_Desc.Content;
                pg.Rus_ProductGroupTitle = txtRus_Title.Text;
                pg.China_ProductGroupDesc = reChina_Desc.Content;
                pg.China_ProductGroupTitle = txtChina_Title.Text;
                pg.IsAlienCulture = chkAlienPro.Checked;

                db.ProductGroup.Add(pg);
                db.SaveChanges();
            }

        }

        public void UpdateForm()
        {
            Guid ProductGroupID = new Guid(ViewState["ProductGroupID"].ToString());
            string new_filename = string.Empty;

            if (fuImage.PostedFile.ContentLength != 0)
            {
                string original_filename = Path.GetFileName(fuImage.PostedFile.FileName);

                new_filename =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_filename);

                string new_filepath = Server.MapPath("~/Uploads/ProductGroup/" + new_filename);
                fuImage.PostedFile.SaveAs(new_filepath);
                ViewState["GImage"] = new_filename;
            }
            string new_filename_Slider = string.Empty;
            Boolean temp = false;
            if (fuImage_Slider.PostedFile.ContentLength != 0)
            {
                string original_filename = Path.GetFileName(fuImage_Slider.PostedFile.FileName);

                new_filename_Slider =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_filename);

                string new_filepath = Server.MapPath("~/Uploads/ProductGroup/" + new_filename_Slider);
                fuImage_Slider.PostedFile.SaveAs(new_filepath);
                ViewState["GImage_Slider"] = new_filename_Slider;
                temp = true;
            }
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.ProductGroup where u.ProductGroupID == ProductGroupID select u).FirstOrDefault();

                n.ProductGroupTitle = txtTitle.Text;
                n.ProductGroupName = txtName.Text;
                n.ProductGroupImage = ViewState["GImage"].ToString();
                n.ProductGroupDesc = reDesc.Content;
                n.EN_ProductGroupTitle = txtEN_Title.Text;
                n.EN_ProductGroupDesc = reEN_Desc.Content;
                if (temp == true)
                    n.imgSliderImage = ViewState["GImage_Slider"].ToString();
                n.Rus_ProductGroupDesc = reRus_Desc.Content;
                n.Rus_ProductGroupTitle = txtRus_Title.Text;
                n.Priority = Convert.ToInt32(txtPrio.Text);
                n.China_ProductGroupDesc = reChina_Desc.Content;
                n.China_ProductGroupTitle = txtChina_Title.Text;
                n.IsAlienCulture = chkAlienPro.Checked;

                db.SaveChanges();
            }
        }

        protected void btnAgree_Click(object sender, EventArgs e)
        {

            Guid ProductGroupID = new Guid(ViewState["ProductGroupID"].ToString());
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.ProductGroup where u.ProductGroupID == ProductGroupID select u).FirstOrDefault();

                n.IsDelete = true;
                n.DeleteDate = DateTime.Now;
                db.SaveChanges();

                LoadForm();
                pnlSuccess.Visible = true;
            }
        }

        protected void btnDeny_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
            pnlSuccess.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
            pnlSuccess.Visible = false;
        }

        protected void cvName_ServerValidate(object source, ServerValidateEventArgs args)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.ProductGroup where u.ProductGroupName == txtName.Text && u.IsDelete == false && u.IsAlienCulture== chkAlienPro.Checked select u).FirstOrDefault();

                if (ViewState["btn"].ToString() == "Insert")
                {
                    args.IsValid = n == null;
                }
                else if (ViewState["btn"].ToString() == "Update")
                {
                    Guid ProductGroupID = new Guid(ViewState["ProductGroupID"].ToString());

                    var m = (from u in db.ProductGroup where u.ProductGroupID == ProductGroupID select u).FirstOrDefault();

                    if (m.ProductGroupName == txtName.Text)
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