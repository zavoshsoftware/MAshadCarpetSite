using MashadCarpet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MashadCarpet
{
    public partial class Profile : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlProvinceBind();
                FindUser();
                
            }
        }

        public void FindUser()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Guid UserID = new Guid(HttpContext.Current.User.Identity.Name);

                    var u = (from i in db.Users where i.UserID == UserID select i).FirstOrDefault();

                    txtAddress1.Text = u.Address1;
                    txtAddress2.Text = u.Address2;
                    txtEmail.Text = u.Email;
                    txtFamily.Text = u.UserFamily;
                    txtMobile.Text = u.Mobile;
                    //txtPass.Text = u.Password;
                    txtPhone.Text = u.Phone;
                    txtPostalCode.Text = u.PostalCode;
                    txtUserName.Text = u.UserName;
                    ddlProvince.SelectedValue = u.ProvinceID.ToString();

                    var pg = (from i in db.City
                              where i.ProvinceID == u.ProvinceID
                              select i).ToList();
                    ddlCity.Items.Clear();
                    ddlCity.Items.Add(new ListItem("شهر ", "-1"));
                    foreach (var t in pg)
                        ddlCity.Items.Add(new ListItem(t.CityName, t.CityID.ToString()));
                    ddlCity.SelectedValue = u.CityID.ToString();
                }
                else
                {
                    string strPage = HttpContext.Current.Request.RawUrl;
                    Response.Redirect("/login?RetUrl=" + strPage);
                }
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

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Guid UserID = new Guid(HttpContext.Current.User.Identity.Name);

                    var u = (from i in db.Users where i.UserID == UserID select i).FirstOrDefault();


                    u.UserName = txtUserName.Text;
                    u.UserFamily = txtFamily.Text;
                    u.Address1 = txtAddress1.Text;
                    u.Address2 = txtAddress2.Text;
                    u.CityID =int.Parse(ddlCity.SelectedValue.ToString());
                    u.ProvinceID = int.Parse(ddlProvince.SelectedValue.ToString());
                    u.PostalCode = txtPostalCode.Text;
                    u.Phone = txtPhone.Text;
                    u.Mobile = txtMobile.Text;
                    u.Email = txtEmail.Text;
                    //u.Password = txtPass.Text;

                 
                    db.SaveChanges();
                   
                }
            }
         
        }

      

        protected void cvEmail_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Guid UserID = new Guid(HttpContext.Current.User.Identity.Name);
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Users where u.Email == txtEmail.Text && u.UserID != UserID select u).FirstOrDefault();

                if (n == null)
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
        }
    }
}