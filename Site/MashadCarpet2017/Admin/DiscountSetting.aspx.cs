using MashadCarpet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MashadCarpet.Admin
{
    public partial class DiscountSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadForm();
                chekboxlistBind();
            }
        }

        public void LoadForm()
        {

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Discounts
                         where u.IsDelete == false
                         select new
                         {
                             u.DiscountID,
                             u.Title,
                             u.SubmitDate,
                             u.DiscountPercent
                         }).ToList();

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
            chekboxlistBind();
            mvSetting.SetActiveView(vwEdit);
            ViewState["btn"] = "Insert";
            ResetForm();
        }


        public void ResetForm()
        {
            txtTitle.Text = string.Empty;
            txtPercent.Text = string.Empty;
            chbActive.Checked = false;
            foreach (ListItem item in chlSizes.Items)
            {
                item.Selected = false;
            }
        }

        public void chekboxlistBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var m = (from u in db.SIzes
                         where u.IsDelete == false
                         select u).ToList();

                chlSizes.Items.Clear();

                foreach (var i in m)
                    chlSizes.Items.Add(new ListItem(i.SizeTitle, i.SizeID.ToString()));
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
            pnlSuccess.Visible = false;
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
        public void UpdateForm()
        {

            Guid userid = new Guid(HttpContext.Current.User.Identity.Name);
            Guid DiscountID = new Guid(ViewState["DiscountID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var p = (from u in db.Discounts where u.DiscountID == DiscountID select u).FirstOrDefault();
                p.DiscountPercent = Convert.ToDouble(txtPercent.Text);
                p.fk_UserID = userid;
                p.IsActive = chbActive.Checked;
                p.Title = txtTitle.Text;



                var nn = (from a in db.Rel_Discounts_Sizes
                          where a.fk_DiscountID == DiscountID
                          select a).ToList();

                foreach (var NItem in nn)
                {
                    db.Rel_Discounts_Sizes.Remove(NItem);
                    db.SaveChanges();
                }

                foreach (ListItem chk in chlSizes.Items)
                {
                    if (chk.Selected)
                    {
                        int size = Convert.ToInt32(chk.Value);

                        Rel_Discounts_Sizes RelEnter = new Rel_Discounts_Sizes();

                        RelEnter.Rel_Discount_SizesID = Guid.NewGuid();

                        RelEnter.fk_DiscountID = DiscountID;

                        RelEnter.fk_SizeID = size;


                        db.Rel_Discounts_Sizes.Add(RelEnter);
                        db.SaveChanges();
                    }
                }

                db.SaveChanges();


            }
        }
        public void InsertForm()
        {
            Guid userid = new Guid(HttpContext.Current.User.Identity.Name);

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                Discounts p = new Discounts();

                p.DiscountID = Guid.NewGuid();
                p.DiscountPercent = Convert.ToDouble(txtPercent.Text);
                p.fk_UserID = userid;
                p.IsActive = chbActive.Checked;
                p.Title = txtTitle.Text;
                p.IsDelete = false;
                p.SubmitDate = DateTime.Now;
                db.Discounts.Add(p);
                db.SaveChanges();


                foreach (ListItem chk in chlSizes.Items)
                {
                    if (chk.Selected)
                    {
                        int size = Convert.ToInt32(chk.Value);

                        Rel_Discounts_Sizes RelEnter = new Rel_Discounts_Sizes();

                        RelEnter.Rel_Discount_SizesID = Guid.NewGuid();

                        RelEnter.fk_DiscountID = p.DiscountID;

                        RelEnter.fk_SizeID = size;


                        db.Rel_Discounts_Sizes.Add(RelEnter);
                        db.SaveChanges();
                    }
                }

            }
        }


        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
        protected void btnYes_Click(object sender, EventArgs e)
        {
            Guid DiscountID = new Guid(ViewState["DiscountID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Discounts where u.DiscountID == DiscountID select u).FirstOrDefault();

                n.IsDelete = true;
                n.DeleteDate = DateTime.Now;
                db.SaveChanges();

                var nn = (from a in db.Rel_Discounts_Sizes
                          where a.fk_DiscountID == DiscountID
                          select a).ToList();

                foreach (var NItem in nn)
                {
                    db.Rel_Discounts_Sizes.Remove(NItem);
                    db.SaveChanges();
                }
            }
            LoadForm();
            pnlSuccess.Visible = true;
        }
        protected void btnNo_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
            pnlSuccess.Visible = false;
        }

        protected void grdTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid DiscountID = new Guid(e.CommandArgument.ToString());
            ViewState["DiscountID"] = DiscountID;

            switch (e.CommandName)
            {
                case "DoEdit":
                    {
                        ViewState["btn"] = "Update";
                        chekboxlistBind();
                        FillViewEdit(DiscountID);
                        break;
                    }
                case "DoDelete":
                    {
                        using (MashadCarpetEntities db = new MashadCarpetEntities())
                        {
                            var n = (from u in db.Discounts where u.DiscountID == DiscountID select u).FirstOrDefault();
                            lblDelete.Text = n.Title;
                            mvSetting.SetActiveView(vwDelete);
                        }
                        break;
                    }
            }
        }

        public void FillViewEdit(Guid DiscountID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Discounts where u.DiscountID == DiscountID select u).FirstOrDefault();
                txtTitle.Text = n.Title;
                txtPercent.Text = n.DiscountPercent.ToString();



                foreach (ListItem item in chlSizes.Items)
                {
                    item.Selected = false;
                }
                var m = (from a in db.Rel_Discounts_Sizes
                         where a.fk_DiscountID == n.DiscountID
                         select a);

                foreach (var rel in m)
                {
                    foreach (ListItem tt in chlSizes.Items)
                    {
                        if (rel.fk_SizeID == Convert.ToInt32(tt.Value))
                        {
                            tt.Selected = true;
                        }
                    }
                }
                mvSetting.SetActiveView(vwEdit);
            }
        }



        protected void cvSizes_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (chbActive.Checked == true)
            {
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var m = (from a in db.Rel_Discounts_Sizes
                             join aa in db.Discounts on a.fk_DiscountID equals aa.DiscountID
                             where aa.IsActive == true && aa.IsDelete == false
                             select a);

                    foreach (var rel in m)
                    {
                        foreach (ListItem tt in chlSizes.Items)
                        {
                            if (tt.Selected)
                            {
                                if (rel.fk_SizeID == Convert.ToInt32(tt.Value))
                                {
                                    args.IsValid = false;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}