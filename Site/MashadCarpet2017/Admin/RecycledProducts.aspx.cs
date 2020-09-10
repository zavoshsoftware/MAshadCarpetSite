using MashadCarpet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MashadCarpet.Admin
{
    public partial class RecycledProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadForm();
                DropDownBind();
            }
        }

        public void LoadForm()
        {
           
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.Products
                             where u.IsDelete == true
                             orderby u.submitDate descending
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
                Guid ProductID = new Guid(ViewState["ProductID"].ToString());
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var p = (from u in db.Products where u.ProductID == ProductID select u).FirstOrDefault();
                    p.IsDelete = false;
                    p.DeleteDate = null;

                    db.SaveChanges();
                }
               
                LoadForm();
            }

        }
       
        protected void btnReturn_Click(object sender, EventArgs e)
        {
           
                Response.Redirect("Default.aspx");
          

        }

      
        protected void grdTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            if (e.CommandName == "Details")
            {
                Guid ProductID = new Guid(e.CommandArgument.ToString());
                ViewState["ProductID"] = ProductID;
                ViewState["btn"] = "Update";
                DropDownBind();
                FillViewEdit(ProductID);
            }
            else if (e.CommandName == "Restore")
            {
                Guid ProductID = new Guid(e.CommandArgument.ToString());
                ViewState["ProductID"] = ProductID;
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.Products where u.ProductID == ProductID select u).FirstOrDefault();
                    n.IsDelete = false;
                    n.DeleteDate = null;
                    db.SaveChanges();

                    LoadForm();
                }
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

                txtENTitle.Text = n.EN_ProductTitle;

                txtDesignNO.Text = n.DesignNo.ToString();
                txtFrame.Text = n.Frame.ToString();
                txtCollection.Text = n.Collection;
                txtPileType.Text = n.PileType;
                txtEN_Collection.Text = n.EN_Collection;
                txtEN_PileType.Text = n.EN_PileType;
                txtReeds.Text = n.Reeds.ToString();
                txtShots.Text = n.Shots.ToString();
                txtPoints.Text = n.Points.ToString();
                txtKnots.Text = n.Knots.ToString();
                cbIsEspecial.Checked = Convert.ToBoolean(n.IsEspecial);
                chkMostSell.Checked = Convert.ToBoolean(n.IsMostSell);

                txtRusTitle.Text = n.Rus_ProductTitle;
                txtRus_Collection.Text = n.Rus_Collection;
                txtRus_PileType.Text = n.Rus_PileType;

                txtChinaTitle.Text = n.China_ProductTitle;
                txtChina_PileType.Text = n.China_PileType;
                txtChina_Collection.Text = n.China_Collection;
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
            LoadForm();
        }

        protected void btnCancel_Click1(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
        }
    }
}