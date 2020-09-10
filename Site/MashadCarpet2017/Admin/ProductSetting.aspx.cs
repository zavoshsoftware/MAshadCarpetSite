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
    public partial class ProductSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadForm(null);
                DropDownBind();
            }
        }
        public void LoadForm(string searchItem)
        {
            if (searchItem == null)
            {
                if (Request.QueryString["ID"] == null)
                {
                    using (MashadCarpetEntities db = new MashadCarpetEntities())
                    {
                        var n = (from u in db.Products
                                 join a in db.ProductGroup
                                 on u.fk_ProductGroupID equals a.ProductGroupID
                                 where u.IsDelete == false
                                 orderby u.submitDate descending
                                 select
                                 new
                                 {
                                     u.ProductTitle,
                                     u.ProductName,
                                     u.Frame,
                                     u.DesignNo,
                                     u.ProductUniqeCode,
                                     u.Collection,
                                     u.PileType,
                                     u.ProductID,
                                     a.ProductGroupTitle,
                                     a.IsAlienCulture
                                 }).ToList();

                        grdTable.DataSource = n;
                        grdTable.DataBind();

                        if (n.Count == 0)
                            pnlEmptyForm.Visible = true;
                        else
                            pnlEmptyForm.Visible = false;
                    }
                }

                else
                {
                    Guid ID = new Guid(Request.QueryString["ID"].ToString());
                    using (MashadCarpetEntities db = new MashadCarpetEntities())
                    {
                        var n = (from u in db.Products
                                 where u.fk_ProductGroupID == ID && u.IsDelete == false
                                 select u).ToList();


                        grdTable.DataSource = n;
                        grdTable.DataBind();
                        if (n.Count == 0)
                            pnlEmptyForm.Visible = true;
                        else
                            pnlEmptyForm.Visible = false;
                    }
                }
            }
            else
            {
                if (Request.QueryString["ID"] == null)
                {
                    using (MashadCarpetEntities db = new MashadCarpetEntities())
                    {
                        var n = (from u in db.Products
                                 join a in db.ProductGroup
                                 on u.fk_ProductGroupID equals a.ProductGroupID
                                 where u.IsDelete == false && u.ProductTitle == searchItem && a.IsDelete==false
                                 orderby u.submitDate descending
                                 select
                                 new
                                 {
                                     u.ProductTitle,
                                     u.ProductName,
                                     u.Frame,
                                     u.DesignNo,
                                     u.ProductUniqeCode,
                                     u.Collection,
                                     u.PileType,
                                     u.ProductID,
                                     a.ProductGroupTitle,
                                     a.IsAlienCulture
                                 }).ToList();

                        grdTable.DataSource = n;
                        grdTable.DataBind();

                        if (n.Count == 0)
                            pnlEmptyForm.Visible = true;
                        else
                            pnlEmptyForm.Visible = false;
                    }
                }

                else
                {
                    Guid ID = new Guid(Request.QueryString["ID"].ToString());
                    using (MashadCarpetEntities db = new MashadCarpetEntities())
                    {
                        var n = (from u in db.Products
                                 where u.fk_ProductGroupID == ID && u.IsDelete == false && u.ProductTitle == searchItem
                                 select u).ToList();


                        grdTable.DataSource = n;
                        grdTable.DataBind();
                        if (n.Count == 0)
                            pnlEmptyForm.Visible = true;
                        else
                            pnlEmptyForm.Visible = false;
                    }
                }
            }
            mvSetting.SetActiveView(vwList);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //imgEditImages.Visible = false;
            ImgThumb1.Visible = false;
            ImgThumb2.Visible = false;
            mvSetting.SetActiveView(vwEdit);
            ViewState["btn"] = "Insert";
            ResetForm();
        }
        public void ResetForm()
        {
            txtTitle.Text = string.Empty;
            txtName.Text = string.Empty;
            ddlProductGroup.SelectedValue = "-1";
            reDesc.Content = string.Empty;
            txtENTitle.Text = string.Empty;
            txtDesignNO.Text = string.Empty;
            txtFrame.Text = string.Empty;
            txtCollection.Text = string.Empty;
            txtEN_Collection.Text = string.Empty;
            txtPileType.Text = string.Empty;
            txtEN_PileType.Text = string.Empty;
            txtReeds.Text = string.Empty;
            txtShots.Text = string.Empty;
            txtPoints.Text = string.Empty;
            txtKnots.Text = string.Empty;
            cbIsEspecial.Checked = false;
            chkMostSell.Checked = false;
            txtChina_Collection.Text = string.Empty;
            txtChina_PileType.Text = string.Empty;
            txtChinaTitle.Text = string.Empty;
            txtRus_Collection.Text = string.Empty;
            txtRus_PileType.Text = string.Empty;
            txtRusTitle.Text = string.Empty;
            chbIsAlien.Checked = false;

        }
        public void DropDownBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var pg = (from u in db.ProductGroup
                          where u.IsDelete == false && u.fk_ProductGroupID == null
                          select u).ToList();
                ddlProductGroup.Items.Clear();
                ddlProductGroup.Items.Add(new ListItem("گروه محصول ", "-1"));
                foreach (var t in pg)
                    ddlProductGroup.Items.Add(new ListItem(t.ProductGroupTitle, t.ProductGroupID.ToString()));
            }
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

                LoadForm(null);
            }

        }
        public void UpdateForm()
        {
            string new_Thumb1 = string.Empty;

            if (fuThumb1.PostedFile.ContentLength != 0)
            {
                string original_Thumb1 = Path.GetFileName(fuThumb1.PostedFile.FileName);

                new_Thumb1 =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_Thumb1);

                string new_filepathThumb1 = Server.MapPath("~/Uploads/Products/" + new_Thumb1);
                fuThumb1.PostedFile.SaveAs(new_filepathThumb1);
                ViewState["Thumb1"] = new_Thumb1;
            }
            string new_Thumb2 = string.Empty;

            if (fuThumb2.PostedFile.ContentLength != 0)
            {
                string original_Thumb2 = Path.GetFileName(fuThumb2.PostedFile.FileName);

                new_Thumb2 =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_Thumb2);

                string new_filepathThumb2 = Server.MapPath("~/Uploads/Products/" + new_Thumb2);
                fuThumb2.PostedFile.SaveAs(new_filepathThumb2);
                ViewState["Thumb2"] = new_Thumb2;
            }

            Guid ProductID = new Guid(ViewState["ProductID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var p = (from u in db.Products where u.ProductID == ProductID select u).FirstOrDefault();

                p.ProductUniqeCode = Convert.ToInt32(txtUniqCode.Text);
                p.IsAlienCulture = chbIsAlien.Checked;
                p.ProductTitle = txtTitle.Text;
                p.ProductName = txtName.Text;
                p.EN_ProductTitle = txtENTitle.Text;
                //p.ProductImage = ViewState["NewImg"].ToString();
                //p.IsAvailable = cbIsValid.Checked;
                p.DesignNo = int.Parse(txtDesignNO.Text);
                p.Frame = int.Parse(txtFrame.Text);
                p.Collection = txtCollection.Text;
                p.EN_Collection = txtEN_Collection.Text;
                p.PileType = txtPileType.Text;
                p.EN_PileType = txtEN_PileType.Text;
                p.Reeds = txtReeds.Text;
                p.Shots = txtShots.Text;
                p.Points = txtPoints.Text;
                p.Knots = txtKnots.Text;
                p.IsEspecial = cbIsEspecial.Checked;
                p.fk_ProductGroupID = new Guid(ddlProductGroup.SelectedValue);
                p.LastUpdateDate = DateTime.Now;
                p.IsMostSell = chkMostSell.Checked;
                p.ProDesc = reDesc.Content;
                p.China_Collection = txtChina_Collection.Text;
                p.China_PileType = txtChina_PileType.Text;
                p.China_ProductTitle = txtChinaTitle.Text;
                p.Rus_Collection = txtRus_Collection.Text;
                p.Rus_PileType = txtRus_PileType.Text;
                p.Rus_ProductTitle = txtRusTitle.Text;
                if (ViewState["Thumb1"] != null)
                    p.ThumbImage1 = ViewState["Thumb1"].ToString();
                else
                    p.ThumbImage1 = "no_photo.gif";
                if (ViewState["Thumb2"] != null)
                    p.ThumbImage2 = ViewState["Thumb2"].ToString();
                else
                    p.ThumbImage2 = "no_photo.gif";


                db.SaveChanges();
            }
        }
        public void InsertForm()
        {
            string new_Thumb1 = string.Empty;

            if (fuThumb1.PostedFile.ContentLength != 0)
            {
                string original_Thumb1 = Path.GetFileName(fuThumb1.PostedFile.FileName);

                new_Thumb1 =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_Thumb1);

                string new_filepathThumb1 = Server.MapPath("~/Uploads/Products/" + new_Thumb1);
                fuThumb1.PostedFile.SaveAs(new_filepathThumb1);
                ViewState["Thumb1"] = new_Thumb1;
            }
            string new_Thumb2 = string.Empty;

            if (fuThumb2.PostedFile.ContentLength != 0)
            {
                string original_Thumb2 = Path.GetFileName(fuThumb2.PostedFile.FileName);

                new_Thumb2 =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_Thumb2);

                string new_filepathThumb2 = Server.MapPath("~/Uploads/Products/" + new_Thumb2);
                fuThumb2.PostedFile.SaveAs(new_filepathThumb2);
                ViewState["Thumb2"] = new_Thumb2;
            }
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                Products p = new Products();

                p.ProductUniqeCode = Convert.ToInt32(txtUniqCode.Text);
                p.IsAlienCulture = chbIsAlien.Checked;
                p.ProductID = Guid.NewGuid();
                p.ProductTitle = txtTitle.Text;
                p.ProductName = txtName.Text;
                p.fk_ProductGroupID = new Guid(ddlProductGroup.SelectedValue);
                p.IsDelete = false;
                p.ProDesc = reDesc.Content;
                p.DesignNo = int.Parse(txtDesignNO.Text);
                p.Frame = int.Parse(txtFrame.Text);
                p.Collection = txtCollection.Text;
                p.EN_Collection = txtEN_Collection.Text;
                p.PileType = txtPileType.Text;
                p.EN_PileType = txtEN_PileType.Text;
                p.Reeds = txtReeds.Text;
                p.Shots = txtShots.Text;
                p.Points = txtPoints.Text;
                p.Knots = txtKnots.Text;
                p.IsEspecial = cbIsEspecial.Checked;
                p.EN_ProductTitle = txtENTitle.Text;
                p.submitDate = DateTime.Now;
                p.IsMostSell = chkMostSell.Checked;
                p.China_Collection = txtChina_Collection.Text;
                p.China_PileType = txtChina_PileType.Text;
                p.China_ProductTitle = txtChinaTitle.Text;
                p.Rus_Collection = txtRus_Collection.Text;
                p.Rus_PileType = txtRus_PileType.Text;
                p.Rus_ProductTitle = txtRusTitle.Text;

                if (fuThumb1.PostedFile.ContentLength != 0)
                    p.ThumbImage1 = new_Thumb1;
                else
                    p.ThumbImage1 = "no_photo.gif";
                if (fuThumb2.PostedFile.ContentLength != 0)
                    p.ThumbImage2 = new_Thumb2;
                else
                    p.ThumbImage2 = "no_photo.gif";

                db.Products.Add(p);
                db.SaveChanges();
            }
        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] == null)
                Response.Redirect("Default.aspx");
            else
            {
                Guid ID = new Guid(Request.QueryString["ID"].ToString());
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.ProductGroup where u.ProductGroupID == ID select u).FirstOrDefault();
                    Response.Redirect("ProductGroupSetting.aspx?ID=" + n.fk_ProductGroupID);
                }
            }
        }
        protected void btnYes_Click(object sender, EventArgs e)
        {
            Guid ProductID = new Guid(ViewState["ProductID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Products where u.ProductID == ProductID select u).FirstOrDefault();

                n.IsDelete = true;
                n.DeleteDate = DateTime.Now;
                db.SaveChanges();

                //delete colors
                var m = (from a in db.ProductColors
                         where a.fk_ProductID == ProductID && a.IsDelete == false
                         select a).ToList();

                foreach (var item in m)
                {
                    item.IsDelete = true;
                    item.DeleteDate = DateTime.Now;

                    //delete Sizes
                    var o = (from a in db.ProductColorSizes
                             where a.fk_ProductColorID == item.ProductColorID
                             && a.IsDelete == false
                             select a).ToList();
                    foreach (var itemSize in o)
                    {
                        itemSize.IsDelete = true;
                        itemSize.DeleteDate = DateTime.Now;
                    }
                }
                db.SaveChanges();
            }
            LoadForm(null);
        }
        protected void btnNo_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
        }
        protected void grdTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DoEdit")
            {
                Guid ProductID = new Guid(e.CommandArgument.ToString());
                ViewState["ProductID"] = ProductID;
                ViewState["btn"] = "Update";
                ImgThumb1.Visible = true;
                ImgThumb2.Visible = true;
                DropDownBind();
                FillViewEdit(ProductID);
            }
            else if (e.CommandName == "DoDelete")
            {
                Guid ProductID = new Guid(e.CommandArgument.ToString());
                ViewState["ProductID"] = ProductID;
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.Products where u.ProductID == ProductID select u).FirstOrDefault();
                    lblDelete.Text = n.ProductTitle;
                    mvSetting.SetActiveView(vwDelete);
                }
            }
            else if (e.CommandName == "Colors")
            {
                Guid ProductID = new Guid(e.CommandArgument.ToString());
                Response.Redirect("ProductColorSetting.aspx?ID=" + ProductID);
            }
        }
        public void FillViewEdit(Guid ProductID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Products where u.ProductID == ProductID select u).FirstOrDefault();
                txtTitle.Text = n.ProductTitle;
                txtName.Text = n.ProductName;
                ddlProductGroup.SelectedValue = n.fk_ProductGroupID.ToString();
                if (n.IsAlienCulture != null)
                    chbIsAlien.Checked = (Boolean)n.IsAlienCulture;
                else
                    chbIsAlien.Checked = false;
                txtENTitle.Text = n.EN_ProductTitle;

                txtDesignNO.Text = n.DesignNo.ToString();
                txtFrame.Text = n.Frame.ToString();
                txtCollection.Text = n.Collection;
                txtPileType.Text = n.PileType;
                txtEN_Collection.Text = n.EN_Collection;
                txtEN_PileType.Text = n.EN_PileType;
                txtReeds.Text = n.Reeds.ToString();
                txtShots.Text = n.Shots.ToString();
                reDesc.Content = n.ProDesc;

                cbIsEspecial.Checked = Convert.ToBoolean(n.IsEspecial);
                chkMostSell.Checked = Convert.ToBoolean(n.IsMostSell);

                txtRusTitle.Text = n.Rus_ProductTitle;
                txtRus_Collection.Text = n.Rus_Collection;
                txtRus_PileType.Text = n.Rus_PileType;

                txtChinaTitle.Text = n.China_ProductTitle;
                txtChina_PileType.Text = n.China_PileType;
                txtChina_Collection.Text = n.China_Collection;

                ImgThumb1.ImageUrl = "/Uploads/Products/" + n.ThumbImage1;
                ImgThumb2.ImageUrl = "/Uploads/Products/" + n.ThumbImage2;
                ViewState["Thumb1"] = n.ThumbImage1;
                ViewState["Thumb2"] = n.ThumbImage2;
                ddlProductGroup.SelectedValue = n.fk_ProductGroupID.ToString();
                mvSetting.SetActiveView(vwEdit);
            }
        }
        protected void cvName_ServerValidate(object source, ServerValidateEventArgs args)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Products where u.ProductName == txtName.Text && u.IsDelete == false select u).FirstOrDefault();
                if (ViewState["btn"].ToString() == "Insert")
                {
                    args.IsValid = n == null;
                }
                else if (ViewState["btn"].ToString() == "Update")
                {
                    Guid ProductID = new Guid(ViewState["ProductID"].ToString());

                    var m = (from u in db.Products where u.ProductID == ProductID select u).FirstOrDefault();

                    if (m.ProductName == txtName.Text)
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
        protected void cvProGroup_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = ddlProductGroup.SelectedIndex != 0;
        }
        protected void grdTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdTable.PageIndex = e.NewPageIndex;
            LoadForm(null);
        }
        protected void dvDesignNo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int DesignNo = int.Parse(txtDesignNO.Text);
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Products where u.DesignNo == DesignNo && u.IsDelete == false select u).FirstOrDefault();

                if (ViewState["btn"].ToString() == "Insert")
                {
                    args.IsValid = n == null;
                }
                else if (ViewState["btn"].ToString() == "Update")
                {
                    Guid ProductID = new Guid(ViewState["ProductID"].ToString());

                    var m = (from u in db.Products where u.ProductID == ProductID select u).FirstOrDefault();

                    if (m.DesignNo == DesignNo)
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
        protected void cvUniqCode_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int ProductUniqeCode = int.Parse(txtUniqCode.Text);
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Products where u.ProductUniqeCode == ProductUniqeCode && u.IsDelete == false 
                         && (u.IsAlienCulture== chbIsAlien.Checked || u.IsAlienCulture==null) select u).FirstOrDefault();

                if (ViewState["btn"].ToString() == "Insert")
                {
                    args.IsValid = n == null;
                }
                else if (ViewState["btn"].ToString() == "Update")
                {
                    Guid ProductID = new Guid(ViewState["ProductID"].ToString());

                    var m = (from u in db.Products where u.ProductID == ProductID
                             && (u.IsAlienCulture == chbIsAlien.Checked || u.IsAlienCulture == null) select u).FirstOrDefault();

                    if (m.ProductUniqeCode == ProductUniqeCode)
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
                LoadForm(txtSearch.Text);
            else
                LoadForm(null);

        }
    }
}