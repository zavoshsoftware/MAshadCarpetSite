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
    public partial class linksSetting : System.Web.UI.Page
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
                    var n = (from u in db.Links                           
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

            mvSetting.SetActiveView(vwEdit);
            ViewState["btn"] = "Insert";
            ResetForm();
        } 
        public void ResetForm()
        {
            txtTitle.Text = string.Empty;
          
            txtEN_Title.Text = string.Empty;
            txtLink.Text = string.Empty;
         
            txtChina_Title.Text = string.Empty;
            txtRus_Title.Text = string.Empty;
            
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
            string new_filename = string.Empty;

            if (fuImg.PostedFile.ContentLength != 0)
            {
                string original_filename = Path.GetFileName(fuImg.PostedFile.FileName);

                new_filename =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_filename);

                string new_filepath = Server.MapPath("~/Uploads/Blogs/" + new_filename);
                fuImg.PostedFile.SaveAs(new_filepath);
                ViewState["NewImg"] = new_filename;
            }

            Guid id = new Guid(ViewState["id"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var p = (from u in db.Links where u.id == id select u).FirstOrDefault();

                p.title = txtTitle.Text;
                p.LinkAddres = txtLink.Text;
                p.imgFile = ViewState["NewImg"].ToString();
                p.title_En = txtEN_Title.Text; 
                p.title_Ru = txtRus_Title.Text; 
                p.title_Ch = txtChina_Title.Text;
               
                db.SaveChanges();
            }
        }
        public void InsertForm()
        {

            string new_filename = string.Empty;

            if (fuImg.PostedFile.ContentLength != 0)
            {
                string original_filename = Path.GetFileName(fuImg.PostedFile.FileName);

                new_filename =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(original_filename);

                string new_filepath = Server.MapPath("~/Uploads/Blogs/" + new_filename);
                fuImg.PostedFile.SaveAs(new_filepath);
                ViewState["NewImg"] = new_filename;
            }
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                Links p = new Links();

                p.id = Guid.NewGuid();
               
                p.IsDelete = false;
                p.title = txtTitle.Text;
                p.LinkAddres = txtLink.Text;

                p.imgFile = ViewState["NewImg"].ToString();
                p.title_En = txtEN_Title.Text;
                p.title_Ru = txtRus_Title.Text;
                p.title_Ch = txtChina_Title.Text;

                db.Links.Add(p);
                db.SaveChanges(); 

            }
        }

     

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] == null)
                Response.Redirect("Default.aspx");
            else
                Response.Redirect("BlogsGroupSetting.aspx");
        }
        protected void btnYes_Click(object sender, EventArgs e)
        {
            Guid id = new Guid(ViewState["id"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Links where u.id == id select u).FirstOrDefault();

                n.IsDelete = true;
                n.DeleteDate = DateTime.Now;
                db.SaveChanges();
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
            Guid id = new Guid(e.CommandArgument.ToString());
            ViewState["id"] = id;

            switch (e.CommandName)
            {
                case "DoEdit":
                    {
                        ViewState["btn"] = "Update";
                     
                        FillViewEdit(id);
                        break;
                    }
                case "DoDelete":
                    {
                        using (MashadCarpetEntities db = new MashadCarpetEntities())
                        {
                            var n = (from u in db.Links where u.id == id select u).FirstOrDefault();
                            lblDelete.Text = n.title;
                            mvSetting.SetActiveView(vwDelete);
                        }
                        break;
                    }
            }
        }

        public void FillViewEdit(Guid id)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Links where u.id == id select u).FirstOrDefault();
                txtTitle.Text = n.title;
            
                // ddlUser.SelectedValue = n.fk_UserID.ToString();
                ViewState["NewImg"] = n.imgFile;
                imgEditImages.ImageUrl = "~/Uploads/Blogs/" + n.imgFile; 
                txtEN_Title.Text = n.title_En; 
                txtRus_Title.Text = n.title_Ru;
                txtChina_Title.Text = n.title_Ch;
                mvSetting.SetActiveView(vwEdit);
                txtLink.Text = n.LinkAddres;
            }
        }

      
        
    }
}