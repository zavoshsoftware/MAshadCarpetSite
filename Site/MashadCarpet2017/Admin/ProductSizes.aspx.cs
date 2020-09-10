using MashadCarpet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MashadCarpet.Admin
{
    public partial class ProductSizes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadForm();
                ddlSizeBind();
                //  ddlSizeBind();
                //DropDownBind();
            }
        }



        public void ddlSizeBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {


                var pg = (from u in db.SIzes
                          where u.IsDelete == false
                          select u).ToList();
                ddlSizes.Items.Clear();
                ddlSizes.Items.Add(new ListItem("سایز محصول ", "-1"));
                foreach (var t in pg)
                    ddlSizes.Items.Add(new ListItem(t.SizeTitle, t.SizeID.ToString()));
            }
        }
        public void LoadForm()
        {
            Guid ID = new Guid(Request.QueryString["ID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {

                var n = (from u in db.ProductColorSizes.AsEnumerable()
                         where u.fk_ProductColorID == ID && u.IsDelete == false
                         join i in db.SIzes on u.fk_SizeID equals i.SizeID
                         orderby i.SizeTitle ascending
                         select new
                         {
                             i.SizeTitle,
                             u.ProductPrice,
                             u.Stock,
                             u.ProductColorSizeID,
                             u.IsAvailable

                         }).ToList();

                grdTable.DataSource = n;
                grdTable.DataBind();

                if (n.Count == 0)
                    pnlEmptyForm.Visible = true;
                else
                    pnlEmptyForm.Visible = false;


                var m = (from u in db.ProductColors
                         where u.ProductColorID == ID
                         select u).FirstOrDefault();

                var o = (from a in db.Colors
                         where a.ColorID == m.fk_ColorID
                         select a).FirstOrDefault();

                var p = (from a in db.Products
                         where a.ProductID == m.fk_ProductID
                         select a).FirstOrDefault();

                lblDesignNo.Text = p.DesignNo.ToString();
                lblProductTitle.Text = p.ProductTitle;
                ImgProduct.ImageUrl = "~/Uploads/Products/" + m.ProductImage;
                lblColorTitle.Text = o.ColorTitle;
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
            ddlSizes.SelectedValue = "-1";
            txtPrice.Text = string.Empty;
            txtStock.Text = string.Empty;
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

                LoadForm();
            }
           
        }
        public void UpdateForm()
        {

            Guid ID = new Guid(ViewState["ProductColorSizeID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var p = (from u in db.ProductColorSizes where u.ProductColorSizeID == ID select u).FirstOrDefault();

                p.fk_SizeID = int.Parse(ddlSizes.SelectedValue);
                p.ProductPrice = Convert.ToDecimal(txtPrice.Text);
                p.Stock = int.Parse(txtStock.Text);

                db.SaveChanges();
            }
        }

        public void InsertForm()
        {
            Guid ProductColorID = new Guid(Request.QueryString["ID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                ProductColorSizes p = new ProductColorSizes();

                p.ProductColorSizeID = Guid.NewGuid();
                p.IsDelete = false;
                p.fk_SizeID = int.Parse(ddlSizes.SelectedValue);
                p.ProductPrice = Convert.ToDecimal(txtPrice.Text);
                p.Stock = int.Parse(txtStock.Text);
                p.fk_ProductColorID = ProductColorID;
                p.IsAvailable = true;

                db.ProductColorSizes.Add(p);
                db.SaveChanges();
            }
        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductSetting.aspx");
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            Guid ID = new Guid(ViewState["ProductColorSizeID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.ProductColorSizes where u.ProductColorSizeID == ID select u).FirstOrDefault();

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
                Guid ProductColorSizeID = new Guid(e.CommandArgument.ToString());
                ViewState["ProductColorSizeID"] = ProductColorSizeID;
                ViewState["btn"] = "Update";

                FillViewEdit(ProductColorSizeID);
            }
            else if (e.CommandName == "DoDelete")
            {
                Guid ProductColorSizeID = new Guid(e.CommandArgument.ToString());
                ViewState["ProductColorSizeID"] = ProductColorSizeID;
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.ProductColorSizes
                             join i in db.SIzes on u.fk_SizeID equals i.SizeID
                             where u.ProductColorSizeID == ProductColorSizeID
                             select i).FirstOrDefault();
                    lblDelete.Text = n.SizeTitle;
                    mvSetting.SetActiveView(vwDelete);
                }
            }


        }

        public void FillViewEdit(Guid ProductColorSizeID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.ProductColorSizes where u.ProductColorSizeID == ProductColorSizeID select u).FirstOrDefault();
                ddlSizes.SelectedValue = n.fk_SizeID.ToString();

                txtPrice.Text = n.ProductPrice.ToString();
                txtStock.Text = n.Stock.ToString();


                mvSetting.SetActiveView(vwEdit);
            }
        }


        protected void grdTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdTable.PageIndex = e.NewPageIndex;
            LoadForm();
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ddlSizes.SelectedValue == "-1")
            {
                args.IsValid = false;
            }
        }
    }
}