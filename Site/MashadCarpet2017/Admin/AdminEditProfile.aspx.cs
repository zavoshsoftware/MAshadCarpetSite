using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MashadCarpet.Models;

namespace MashadCarpet.Admin
{
    public partial class AdminEditProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlProvinceBind();
                LoadForm();
               
            }
        }

        public void LoadForm()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Guid UserID = new Guid(HttpContext.Current.User.Identity.Name);
                //Guid UserID = new Guid("ee4edaf0-aad0-4140-a9fe-8b9bcaab6bd3");
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.Users where u.UserID == UserID select u).FirstOrDefault();

                    txtAddress1.Text = n.Address1;
                    txtAddress2.Text = n.Address2;
                    ddlCity.SelectedValue = n.CityID.ToString();
                    txtEmail.Text = n.Email;
                    txtFamily.Text = n.UserFamily;
                    txtFirstName.Text = n.UserName;
                    txtMobile.Text = n.Mobile;
                    txtpass.Text = n.Password;
                    txtPhone.Text = n.Phone;
                    txtPostalCode.Text = n.PostalCode;
                    ddlProvince.SelectedValue = n.ProvinceID.ToString();
                    ddlCityBind();
                    ddlCity.SelectedValue = n.CityID.ToString();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Guid UserID = new Guid(HttpContext.Current.User.Identity.Name);
            //Guid UserID = new Guid("ee4edaf0-aad0-4140-a9fe-8b9bcaab6bd3"); 
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var u = (from n in db.Users where n.UserID == UserID select n).FirstOrDefault();

                
                u.UserName = txtFirstName.Text;
                u.Email = txtEmail.Text;
                u.Phone = txtPhone.Text;
                u.Password = txtpass.Text;
                u.Address1 = txtAddress1.Text;
                u.Address2 = txtAddress2.Text;
                u.CityID = int.Parse(ddlCity.SelectedValue);
                u.ProvinceID = int.Parse(ddlProvince.SelectedValue);
                u.PostalCode = txtPostalCode.Text;
                u.Mobile = txtMobile.Text;
                u.UserFamily = txtFamily.Text;

                db.SaveChanges();
            }
            LoadForm();
        }

        public void ddlCityBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var pg = (from u in db.City
                          select u).ToList();
                ddlCity.Items.Clear();
                ddlCity.Items.Add(new ListItem("شهر ", "-1"));
                foreach (var t in pg)
                    ddlCity.Items.Add(new ListItem(t.CityName, t.CityID.ToString()));
            }
        }
        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ProvinceID = int.Parse(ddlProvince.SelectedValue.ToString());
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var pg = (from u in db.City
                          where u.ProvinceID == ProvinceID
                          select u).ToList();
                ddlCity.Items.Clear();
                ddlCity.Items.Add(new ListItem("شهر ", "-1"));
                foreach (var t in pg)
                    ddlCity.Items.Add(new ListItem(t.CityName, t.CityID.ToString()));
            }
        }

        public void ddlProvinceBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var pg = (from u in db.Province
                          select u).ToList();
                ddlProvince.Items.Clear();
                ddlProvince.Items.Add(new ListItem("استان ", "-1"));
                foreach (var t in pg)
                    ddlProvince.Items.Add(new ListItem(t.ProvinceName, t.ProvinceID.ToString()));
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

    }
}