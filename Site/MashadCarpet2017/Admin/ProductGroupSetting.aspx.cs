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
    public partial class ProductGroupSetting : System.Web.UI.Page
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
            if(Request.QueryString["ID"]!=null)
            {
                Guid ID = new Guid(Request.QueryString["ID"].ToString());
                ViewState["ID"] = ID;
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.ProductGroup
                             where u.IsDelete == false && u.fk_ProductGroupID ==ID
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
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //FillDDLProductgroup();
            ViewState["btn"] = "Insert";
            txtTitle.Text = string.Empty;
        
            txtName.Text = string.Empty;
            reDesc.Content = string.Empty;
            txtEN_Title.Text = string.Empty;
           
            reEN_Desc.Content = null;
            imgEditImages.Visible = false;

            txtChina_Title.Text = string.Empty;
            txtRus_Title.Text = string.Empty;

            reChina_Desc.Content = null;
            reRus_Desc.Content = null;
           
            mvSetting.SetActiveView(vwEdit);
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("mainProductGroupSetting.aspx");

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
                        //     LoadDropDown();
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

                case "ShowProducts":
                    {
                        Response.Redirect("ProductSetting.aspx?ID=" + ProductGroupID);
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
                mvSetting.SetActiveView(vwEdit);
                imgEditImages.ImageUrl = "~/Uploads/ProductGroup/" + n.ProductGroupImage;
                imgEditImages_Slider.ImageUrl = "~/Uploads/ProductGroup/" + n.imgSliderImage;
                ViewState["GImage_Slider"] = n.imgSliderImage;
                txtRus_Title.Text = n.Rus_ProductGroupTitle;
                txtChina_Title.Text = n.China_ProductGroupTitle;

                reRus_Desc.Content = n.Rus_ProductGroupDesc;
                reChina_Desc.Content = n.China_ProductGroupDesc;
  
                //FillDDLProductgroup();
                //  if(n.fk_ProductGroupID!=null)
                //          ddlProductGroups.SelectedValue =n.fk_ProductGroupID.ToString();
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
            Guid ID = new Guid(ViewState["ID"].ToString());
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

                new_filename =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_filename);

                string new_filepath = Server.MapPath("~/Uploads/ProductGroup/" + new_filename);
                fuImage_Slider.PostedFile.SaveAs(new_filepath);
                ViewState["GImage_Slider"] = new_filename;
            }

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                ProductGroup pg = new ProductGroup();
                pg.ProductGroupID = Guid.NewGuid();
                pg.ProductGroupTitle = txtTitle.Text;
             
                pg.ProductGroupName = txtName.Text;
                pg.ProductGroupImage = new_filename;
                pg.imgSliderImage = new_filename_Slider;
                pg.ProductGroupDesc = reDesc.Content;
                pg.IsDelete = false;
                pg.EN_ProductGroupTitle = txtEN_Title.Text;
        
                pg.EN_ProductGroupDesc = reEN_Desc.Content;
                pg.fk_ProductGroupID = ID;
                pg.Rus_ProductGroupDesc = reRus_Desc.Content;
                pg.Rus_ProductGroupTitle = txtRus_Title.Text;
                pg.China_ProductGroupDesc = reChina_Desc.Content;
                pg.China_ProductGroupTitle = txtChina_Title.Text;
                //  if (ddlProductGroups.SelectedValue != "-1")
                //           pg.fk_ProductGroupID = new Guid(ddlProductGroups.SelectedValue);
    
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

            if (fuImage_Slider.PostedFile.ContentLength != 0)
            {
                string original_filename = Path.GetFileName(fuImage_Slider.PostedFile.FileName);

                new_filename =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_filename);

                string new_filepath = Server.MapPath("~/Uploads/ProductGroup/" + new_filename);
                fuImage_Slider.PostedFile.SaveAs(new_filepath);
                ViewState["GImage_Slider"] = new_filename;
            }

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.ProductGroup where u.ProductGroupID == ProductGroupID select u).FirstOrDefault();

                n.ProductGroupTitle = txtTitle.Text;
             
                n.ProductGroupName = txtName.Text;
                n.ProductGroupImage = ViewState["GImage"].ToString();
                n.imgSliderImage = ViewState["GImage_Slider"].ToString();

                n.ProductGroupDesc = reDesc.Content;
                n.EN_ProductGroupTitle = txtEN_Title.Text;
                n.EN_ProductGroupDesc = reEN_Desc.Content;
                n.Rus_ProductGroupDesc = reRus_Desc.Content;
                n.Rus_ProductGroupTitle = txtRus_Title.Text;
                n.China_ProductGroupDesc = reChina_Desc.Content;
                n.China_ProductGroupTitle = txtChina_Title.Text;
                //n.fk_ProductGroupID = new Guid(ddlProductGroup.SelectedValue);
                //  n.fk_ProductGroupID = new Guid(ddlProductGroups.SelectedValue);
          
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
                var n = (from u in db.ProductGroup where u.ProductGroupName == txtName.Text && u.IsDelete == false select u).FirstOrDefault();

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