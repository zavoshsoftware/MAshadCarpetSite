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
    public partial class ProductColorSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadForm();
                ddlColorsBind();
              //  ddlSizeBind();
                //DropDownBind();
            }
        }

        public void ddlColorsBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {


                var pg = (from u in db.Colors
                          where u.IsDelete == false
                          select u).ToList();
                ddlColors.Items.Clear();
                ddlColors.Items.Add(new ListItem("رنگ محصول ", "-1"));
                foreach (var t in pg)
                    ddlColors.Items.Add(new ListItem(t.ColorTitle, t.ColorID.ToString()));
            }
        }

        //public void ddlSizeBind()
        //{
        //    using (MashadCarpetEntities db = new MashadCarpetEntities())
        //    {


        //        var pg = (from u in db.SIzes
        //                  where u.IsDelete == false
        //                  select u).ToList();
        //        ddlSize.Items.Clear();
        //        ddlSize.Items.Add(new ListItem("سایز محصول ", "-1"));
        //        foreach (var t in pg)
        //            ddlSize.Items.Add(new ListItem(t.SizeTitle, t.SizeID.ToString()));
        //    }
        //}
        public void LoadForm()
        {
            Guid ID = new Guid(Request.QueryString["ID"].ToString());
            ViewState["ID"] = ID;
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                 
                var n = (from u in db.ProductColors.AsEnumerable()
                         where u.fk_ProductID == ID && u.IsDelete == false
                         join i in db.Colors on u.fk_ColorID equals i.ColorID                       
                         orderby i.ColorName ascending           
                         select new
                         {                            
                             u.ProductImage,                            
                             i.ColorTitle,                             
                             i.ColorNo,
                             u.ProductColorID
                          
                         }).ToList();

                grdTable.DataSource = n;
                grdTable.DataBind();

                if (n.Count == 0)
                    pnlEmptyForm.Visible = true;
                else
                    pnlEmptyForm.Visible = false;


                var m = (from u in db.Products
                         where u.ProductID == ID
                         select u).FirstOrDefault();

                lblDesignNo.Text = m.DesignNo.ToString();
                lblProductTitle.Text = m.ProductTitle;
                //ImgProduct.ImageUrl = "~/Uploads/Products/" + m.ProductImage;


            }
            mvSetting.SetActiveView(vwList);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            imgEditImages.Visible = false;
            mvSetting.SetActiveView(vwEdit);
            ViewState["btn"] = "Insert";
            ResetForm();
        }
        public void ResetForm()
        {
            ddlColors.SelectedValue = "-1";
       
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
            string new_filename = string.Empty;

            if (fuImg.PostedFile.ContentLength != 0)
            {
                string original_filename = Path.GetFileName(fuImg.PostedFile.FileName);

                new_filename =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_filename);

                string new_filepath = Server.MapPath("~/Uploads/Products/" + new_filename);
                fuImg.PostedFile.SaveAs(new_filepath);
                ViewState["NewImg"] = new_filename;
            }

            Guid ID = new Guid(ViewState["ProductColorID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var p = (from u in db.ProductColors where u.ProductColorID == ID select u).FirstOrDefault();

                p.ProductImage = ViewState["NewImg"].ToString();
                p.fk_ColorID = int.Parse(ddlColors.SelectedValue);
            

                db.SaveChanges();
            }
        }
     //  public Nullable<Boolean> ReturnThumb()
     //  {
     //      if (ddlThumbImg.SelectedValue == "0")
     //          return null;
     //      else if (ddlThumbImg.SelectedValue == "1")
     //          return true;
     //      else
     //          return false;
     //  }
        public void InsertForm()
        {
            Guid ProductID = new Guid(ViewState["ID"].ToString());
            string new_filename = string.Empty;

            if (fuImg.PostedFile.ContentLength != 0)
            {
                string original_filename = Path.GetFileName(fuImg.PostedFile.FileName);

                new_filename =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_filename);

                string new_filepath = Server.MapPath("~/Uploads/Products/" + new_filename);
                fuImg.PostedFile.SaveAs(new_filepath);
                ViewState["NewImg"] = new_filename;
            }
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                ProductColors p = new ProductColors();

                p.ProductColorID = Guid.NewGuid();
                p.IsDelete = false;
                p.ProductImage = new_filename;
                p.fk_ColorID = int.Parse(ddlColors.SelectedValue);
           
                p.fk_ProductID = ProductID;


                db.ProductColors.Add(p);
                db.SaveChanges();




            }
        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductSetting.aspx");
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            Guid ID = new Guid(ViewState["ProductColorID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.ProductColors where u.ProductColorID == ID select u).FirstOrDefault();

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
                Guid ProductColorID = new Guid(e.CommandArgument.ToString());
                ViewState["ProductColorID"] = ProductColorID;
                ViewState["btn"] = "Update";
                //DropDownBind();
                FillViewEdit(ProductColorID);
            }
            else if (e.CommandName == "DoDelete")
            {
                Guid ProductColorID = new Guid(e.CommandArgument.ToString());
                ViewState["ProductColorID"] = ProductColorID;
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.ProductColors
                             join i in db.Colors on u.fk_ColorID equals i.ColorID 
                             where u.ProductColorID == ProductColorID select i).FirstOrDefault();
                    lblDelete.Text = n.ColorTitle;
                    mvSetting.SetActiveView(vwDelete);
                }
            }
            //else if (e.CommandName == "Size")
            //{
            //    Guid Rel_Product_Color_Size_ID = new Guid(e.CommandArgument.ToString());
            //    //using (MashadCarpetEntities db = new MashadCarpetEntities())
            //    //{
            //    //    var n = (from u in db.Rel_Product_Color_Size where u.Rel_Product_Color_Size_ID == Rel_Product_Color_Size_ID select u).FirstOrDefault();
            //    Response.Redirect("ProductSizeSetting.aspx?ID=" + Rel_Product_Color_Size_ID);

            //    //}



            //}

        }

        public void FillViewEdit(Guid ProductColorID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.ProductColors where u.ProductColorID == ProductColorID select u).FirstOrDefault();
                ddlColors.SelectedValue = n.fk_ColorID.ToString();           
                imgEditImages.ImageUrl = "/Uploads/Products/" + n.ProductImage;           
                ViewState["NewImg"] = n.ProductImage;
                mvSetting.SetActiveView(vwEdit);        
            }
        }


        protected void grdTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdTable.PageIndex = e.NewPageIndex;
            LoadForm();
        }
    }
}