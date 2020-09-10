using MashadCarpet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MashadCarpet.Admin
{
    public partial class StoreSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Loadgrd();
                mvStores.SetActiveView(vwlist);
            }
        }

        public void Loadgrd()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Stores
                         where u.IsDelete == false && u.IsStore == true
                         select u).ToList();
                grdStores.DataSource = n;
                grdStores.DataBind();
                if (n.Count == 0)
                    pnlEmptyForm.Visible = true;
                else
                    pnlEmptyForm.Visible = false;
            }
        }


        public void LoadDeleteLabel()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                Guid id = new Guid(ViewState["StoreID"].ToString());
                var n = (from u in db.Stores
                         where u.StoreID == id
                         select u).FirstOrDefault();
                lblDelete.Text = n.StoreName;
            };
        }

        protected void btnAgree_Click(object sender, EventArgs e)
        {
            Guid id = new Guid(ViewState["StoreID"].ToString());
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Stores
                         where u.StoreID == id
                         select u).FirstOrDefault();

                n.IsDelete = true;
                n.DeleteDate = DateTime.Now;

                db.SaveChanges();
                Loadgrd();
                mvStores.SetActiveView(vwlist);
            }
        }

        protected void btnDisAgree_Click(object sender, EventArgs e)
        {
            mvStores.SetActiveView(vwlist);
        }

        protected void grdStores_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            ViewState["StoreID"] = e.CommandArgument;
            switch (e.CommandName)
            {
                case "DoEdit":
                    {
                        Fillvwedit();
                        mvStores.SetActiveView(vwedit);
                        ViewState["btn"] = "edit";
                        break;
                    }

                case "DoDelete":
                    {
                        LoadDeleteLabel();
                        mvStores.SetActiveView(vwdelete);
                        break;
                    }

            }
        }

        public void Fillvwedit()
        {
            Guid id = new Guid(ViewState["StoreID"].ToString());
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Stores
                         where u.StoreID == id
                         select u).FirstOrDefault();

            //    txtName.Text = n.StoreName;
                txtPhone.Text = n.StorePhone;
                txtCity.Text = n.StoreCity;
                txtAddress.Text = n.StoreAddress;
              //  txtPov.Text = n.Prov;
              //  txtDesc.Text = n.StoreDesc;
               

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ViewState["btn"].ToString() == "edit")
                Update();
            else if (ViewState["btn"].ToString() == "Insert")
                Insert();
        }
        public void Update()
        {
            Guid id = new Guid(ViewState["StoreID"].ToString());
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Stores
                         where u.StoreID == id
                         select u).FirstOrDefault();



              //  n.StoreName = txtName.Text;
                n.StorePhone = txtPhone.Text;
                n.StoreCity = txtCity.Text;
                n.StoreAddress = txtAddress.Text;
             //   n.Prov = txtPov.Text;
              //  n.StoreDesc = txtDesc.Text;


                db.SaveChanges();
                Loadgrd();
                mvStores.SetActiveView(vwlist);
            }
        }

        public void Insert()
        {

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            { 
                Stores n = new Stores();
                n.StoreID = Guid.NewGuid();
                n.IsDelete = false;
             //   n.StoreName = txtName.Text;
                n.StorePhone = txtPhone.Text;
                n.StoreCity = txtCity.Text;
                n.StoreAddress = txtAddress.Text;
                n.IsStore = true;
           //     n.Prov = txtPov.Text;
            //    n.StoreDesc = txtDesc.Text;
                db.Stores.Add(n);
                db.SaveChanges();
            }
            Loadgrd();
            mvStores.SetActiveView(vwlist);
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            ViewState["btn"] = "Insert";
            Emptyform();
            mvStores.SetActiveView(vwedit);
        }

        public void Emptyform()
        {

          //  txtName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtAddress.Text = string.Empty;
          //  txtPov.Text = string.Empty;
         //   txtDesc.Text = string.Empty;
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Admin/Default.aspx");
        }

        protected void btnCanceleEdit_Click(object sender, EventArgs e)
        {
            mvStores.SetActiveView(vwlist);
        }
    }
}