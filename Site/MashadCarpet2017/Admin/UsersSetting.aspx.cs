using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MashadCarpet.Models;

namespace MashadCarpet.Admin
{
    public partial class UsersSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadForm();
                ddlRolesFilterBind();
                ddlProvinceBind();
                ddlCityBind();
            }
        }

        public void LoadForm()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Users
                         where u.IsDelete==false
                         orderby u.RegisterDate descending
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

        public void FillDDLRoles()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var m = (from u in db.Roles
                         select u).ToList();
                ddlRoles.Items.Clear();
                ddlRoles.Items.Add(new ListItem("نقش کاربر ", "-1"));
                foreach (var i in m)
                    ddlRoles.Items.Add(new ListItem(i.RoleTitle, i.RoleID.ToString()));
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ViewState["btn"] = "Insert";
            EmptyForm();
            FillDDLRoles();
            mvSetting.SetActiveView(vwEdit);
        }
        public void EmptyForm()
        {
            txtEmail.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtpass.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress1.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtFamily.Text = string.Empty;
            txtAddress1.Text = string.Empty;
            ddlProvince.SelectedValue = "-1";
            ddlCity.SelectedValue = "-1";
            txtPostalCode.Text = string.Empty;
            txtMobile.Text = string.Empty;
            ddlRoles.SelectedValue = "-1";
            
        }
        protected void btnRet_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void grdProductGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid UserID = new Guid(e.CommandArgument.ToString());
            ViewState["UserID"] = UserID;

            switch (e.CommandName)
            {
                case "DoEdit":
                    {
                        ViewState["btn"] = "Update";

                        FillViewEdit(UserID);
                        break;
                    }
                case "DoDelete":
                    {
                        using (MashadCarpetEntities db = new MashadCarpetEntities())
                        {
                            var n = (from u in db.Users where u.UserID == UserID select u).FirstOrDefault();
                            lblDelete.Text = n.UserName+" "+n.UserFamily;

                            mvSetting.SetActiveView(vwDelete);
                        }
                        break;
                    }



            }
        }


        public void FillViewEdit(Guid UserID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Users where u.UserID == UserID select u).FirstOrDefault();
                if(n.fk_RoleID==1)
                {
                    txtpass.Text ="*****";
                    txtpass.Enabled = false;
                }
                else
                {
                    txtpass.Text = n.Password;
                }
                txtEmail.Text = n.Email;
                txtFirstName.Text = n.UserName;
                txtAddress1.Text = n.Address1;
               
                txtPhone.Text = n.Phone;
                FillDDLRoles();
                ddlRoles.SelectedValue = n.fk_RoleID.ToString();
                txtFamily.Text = n.UserFamily;
                txtAddress2.Text = n.Address2;
                ddlProvince.SelectedValue = n.ProvinceID.ToString();
                ddlCity.SelectedValue = n.CityID.ToString();
                txtPostalCode.Text = n.PostalCode;
                txtMobile.Text = n.Mobile;

                mvSetting.SetActiveView(vwEdit);

            }
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (ViewState["btn"].ToString() == "Update")
                    Update();
                else if (ViewState["btn"].ToString() == "Insert")
                    Insert();

                LoadForm();
                pnlSuccess.Visible = true;
            }
        }
        public void Insert()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                Users u = new Users();
                u.UserID = Guid.NewGuid();
                u.UserName = txtFirstName.Text;
                u.Email = txtEmail.Text;
                u.Phone = txtPhone.Text;
                u.RegisterDate = DateTime.Now;
                u.Password = txtpass.Text;
                u.fk_RoleID = Convert.ToInt32(ddlRoles.SelectedValue);
                u.RegisterIP = Request.UserHostAddress;
                u.Address1 = txtAddress1.Text;
                u.Address2 = txtAddress2.Text;
                u.CityID = int.Parse(ddlCity.SelectedValue);
                u.ProvinceID = int.Parse(ddlProvince.SelectedValue);
                u.PostalCode = txtPostalCode.Text;
                u.Mobile = txtMobile.Text;
                u.IsDelete = false;
                u.UserFamily = txtFamily.Text;


                db.Users.Add(u);
                db.SaveChanges();
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
        public void Update()
        {
            Guid UserID = new Guid(ViewState["UserID"].ToString());
          
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var u = (from n in db.Users where n.UserID == UserID select n).FirstOrDefault();
                if (u.fk_RoleID == 2)
                {
                    u.Password = txtpass.Text;
                }
                u.UserName = txtFirstName.Text;
                u.Email = txtEmail.Text;
                u.Phone = txtPhone.Text;               
                u.Address1 = txtAddress1.Text;
                u.fk_RoleID = Convert.ToInt32(ddlRoles.SelectedValue);
                u.Address2 = txtAddress2.Text;
                u.CityID = int.Parse(ddlCity.SelectedValue);
                u.ProvinceID = int.Parse(ddlProvince.SelectedValue);
                u.PostalCode = txtPostalCode.Text;
                u.Mobile = txtMobile.Text;
                u.UserFamily = txtFamily.Text;

                db.SaveChanges();
            }
        }

        protected void btnAgree_Click(object sender, EventArgs e)
        {

            Guid UserID = new Guid(ViewState["UserID"].ToString());
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Users where u.UserID == UserID select u).FirstOrDefault();

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

        protected void grdTable_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow r in grdTable.Rows)
            {
                HiddenField hfRoleID = (HiddenField)r.FindControl("hfRoleID");
                int RoleID = int.Parse(hfRoleID.Value);

                Label lblRole = (Label)r.FindControl("lblRole");

                HiddenField hfUserID = (HiddenField)r.FindControl("hfUserID");
                Label lblpass = (Label)r.FindControl("lblpass");
                Guid UserID = new Guid(hfUserID.Value);
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var m = (from a in db.Users
                             where a.UserID == UserID && a.IsDelete == false
                             select a).FirstOrDefault();
                    if (RoleID == 1)
                        lblpass.Text = "******";
                    else
                        lblpass.Text = m.Password;


                    var n = (from u in db.Roles where u.RoleID == RoleID select u).FirstOrDefault();

                    lblRole.Text = n.RoleTitle;
                }

            }
        }

        protected void cvRole_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = ddlRoles.SelectedIndex != 0;
        }

        protected void cvEmail_ServerValidate(object source, ServerValidateEventArgs args)
        {

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Users where u.Email == txtEmail.Text && u.IsDelete!=true select u).FirstOrDefault();

                if (ViewState["btn"].ToString() == "Insert")
                {
                    args.IsValid = n == null;
                }
                else if (ViewState["btn"].ToString() == "Update")
                {
                    Guid UserID = new Guid(ViewState["UserID"].ToString());

                    var m = (from u in db.Users where u.UserID == UserID && u.IsDelete!=true select u).FirstOrDefault();

                    if (m.Email == txtEmail.Text)
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
        public void ddlRolesFilterBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var m = (from u in db.Roles
                         select u).ToList();
                ddlRolesFilter.Items.Clear();
                ddlRolesFilter.Items.Add(new ListItem("نقش کاربر ", "-1"));
                foreach (var i in m)
                    ddlRolesFilter.Items.Add(new ListItem(i.RoleTitle, i.RoleID.ToString()));
            }
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            if (ddlRolesFilter.SelectedValue != "-1" && JQDatePicker1.Date != null)
            {
                int RoleID = int.Parse(ddlRolesFilter.SelectedValue.ToString());
                DateTime SubmitDate = Convert.ToDateTime(JQDatePicker1.Date.ToString());
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.Users.AsEnumerable()
                             join i in db.Roles on u.fk_RoleID equals i.RoleID
                             where u.RegisterDate.Value.Date == SubmitDate.Date && u.fk_RoleID == RoleID
                             select u).ToList();
                    grdTable.DataSource = n;
                    grdTable.DataBind();
                    if (n.Count == 0)
                        pnlEmptyForm.Visible = true;
                }
            }
            else if (ddlRolesFilter.SelectedValue != "-1")
            {
                int RoleID = int.Parse(ddlRolesFilter.SelectedValue.ToString());
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.Users
                             join i in db.Roles on u.fk_RoleID equals i.RoleID
                             where u.fk_RoleID == RoleID
                             select u).ToList();
                    grdTable.DataSource = n;
                    grdTable.DataBind();
                    if (n.Count == 0)
                        pnlEmptyForm.Visible = true;
                }
            }
            else if (ddlRolesFilter.SelectedValue == "-1" && JQDatePicker1.Date != null)
            {
                DateTime SubmitDate = Convert.ToDateTime(JQDatePicker1.Date.ToString());
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.Users.AsEnumerable()                           
                             where u.RegisterDate.Value.Date == SubmitDate.Date
                             select u).ToList();
                    grdTable.DataSource = n;
                    grdTable.DataBind();
                    if (n.Count == 0)
                        pnlEmptyForm.Visible = true;
                }

            }
            else
            {
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.Users
                             select u).ToList();
                    grdTable.DataSource = n;
                    grdTable.DataBind();
                    if (n.Count == 0)
                        pnlEmptyForm.Visible = true;
                }
            }

        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ddlCity.SelectedValue != "-1")
            {
                args.IsValid = true;
            }
            else
                args.IsValid = false;
        }
    }
}