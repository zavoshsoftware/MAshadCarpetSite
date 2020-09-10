using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MashadCarpet.Models;
using System.Web.Security;
using System.Net.Mail;
using MashadCarpet.Classes;

namespace MashadCarpet
{
    public partial class Register : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlProvinceBind();
                ChooseTitleAndDesc();

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "PageScriptfa",
              "$('.newfa').removeClass('fa-arrow-right'); $('.newfa').addClass('fa-arrow-left');", true);
                 
        }
        public void ChooseTitleAndDesc()
        {
            if (IdentifyCulture.cultureName().Contains("fa"))
            {
                Page.Title = "ثبت نام | فرش مشهد";
               
                 
            }
            else if (IdentifyCulture.cultureName().Contains("en"))
            {
                Page.Title = "Register";
            }
            else if (IdentifyCulture.cultureName().Contains("ru"))
            {
                Page.Title = "Register";

            }
            else if (IdentifyCulture.cultureName().Contains("zh"))
            {
                Page.Title = "Register";

            }
            else
            {
                Page.Title = "ثبت نام";
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
                //ddlCity.Items.Add(new ListItem("شهر ", "-1"));
                ddlCity.Items.Add(new ListItem((IdentifyCulture.cultureName().Contains("fa") ? "شهر" :
                           ((IdentifyCulture.cultureName().Contains("en")) ? "City" :
                           ((IdentifyCulture.cultureName().Contains("ru")) ? "город" :
                           ((IdentifyCulture.cultureName().Contains("zh")) ? "城市" : "شهر")))), "-1"));
                foreach (var t in pg)
                    //ddlCity.Items.Add(new ListItem(t.CityName, t.CityID.ToString()));
                    ddlCity.Items.Add(new ListItem((IdentifyCulture.cultureName().Contains("fa") ? t.CityName :
                             ((IdentifyCulture.cultureName().Contains("en")) ? t.EN_CityName :
                             ((IdentifyCulture.cultureName().Contains("ru")) ? t.Rus_CityName :
                             ((IdentifyCulture.cultureName().Contains("zh")) ? t.China_CityName : t.CityName)))), t.CityID.ToString()));
            }
        }

        public void ddlProvinceBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var pg = (from u in db.Province
                          select u).ToList();
                ddlProvince.Items.Clear();
                //ddlProvince.Items.Add(new ListItem("استان ", "-1"));
                ddlProvince.Items.Add(new ListItem((IdentifyCulture.cultureName().Contains("fa") ? "استان" :
                              ((IdentifyCulture.cultureName().Contains("en")) ? "Province" :
                              ((IdentifyCulture.cultureName().Contains("ru")) ? "провинция" :
                              ((IdentifyCulture.cultureName().Contains("zh")) ? "省" : "استان")))),"-1"));
                foreach (var t in pg)
                    //ddlProvince.Items.Add(new ListItem(t.ProvinceName, t.ProvinceID.ToString()));
                    ddlProvince.Items.Add(new ListItem((IdentifyCulture.cultureName().Contains("fa") ? t.ProvinceName:
                              ((IdentifyCulture.cultureName().Contains("en")) ? t.EN_ProvinceName :
                              ((IdentifyCulture.cultureName().Contains("ru")) ? t.Rus_ProvinceName :
                              ((IdentifyCulture.cultureName().Contains("zh")) ? t.China_ProvinceName : t.ProvinceName)))),t.ProvinceID.ToString()));
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    Users u = new Users();

                    u.UserID = Guid.NewGuid();
                    u.UserName = txtUserName.Text;

                    u.Address1 = txtAddress1.Text;
                    u.Address2 = txtAddress2.Text;
                    if (ddlCity.SelectedIndex != -1)
                        u.CityID = int.Parse(ddlCity.SelectedValue.ToString());
                    if (ddlProvince.SelectedIndex != -1)
                        u.ProvinceID = int.Parse(ddlProvince.SelectedValue.ToString());
                    u.PostalCode = txtPostalCode.Text;
                    u.Phone = txtPhone.Text;
                    u.Mobile = txtMobile.Text;
                    u.Email = txtEmail.Text;
                    u.Password = txtPass.Text;
                    u.fk_RoleID = 2;

                    u.IsDelete = false;
                    u.RegisterIP = Request.UserHostAddress;
                    u.RegisterDate = DateTime.Now;

                    db.Users.Add(u);
                    db.SaveChanges();
                     SendMail(u.UserID);
                    FormsAuthentication.SetAuthCookie(u.UserID.ToString(), false);

                    if (Request.QueryString["RetUrl"] != null)
                        Response.Redirect(Request.QueryString["RetUrl"]);
                    else
                        pnlSuccess.Visible = true;
                }
                EmptyForm();
            }
        }

        public void EmptyForm()
        {
            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtEmail.Text = string.Empty;

            txtMobile.Text = string.Empty;
            txtPass.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtPostalCode.Text = string.Empty;
            txtUserName.Text = string.Empty;
            ddlCity.SelectedValue = "-1";
            ddlProvince.SelectedValue = "-1";
        }

        protected void cvEmail_ServerValidate(object source, ServerValidateEventArgs args)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Users where u.Email == txtEmail.Text &&u.IsDelete==false select u).FirstOrDefault();

                if (n == null)
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
        }

        protected void cvCity_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ddlProvince.SelectedValue == "-1")
            {
                args.IsValid = false;
            }
            else
                if (ddlCity.SelectedValue == "-1")
                {
                    args.IsValid = false;
                }
        }


        public void SendMail(Guid ID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {

                var m = (from u in db.Users where u.IsDelete == false
                         &&u.UserID==ID
                         select u).FirstOrDefault();
                //foreach (var m in n)
                //{


                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("register@mashadcarpet.com");

                mail.To.Add(m.Email);


                mail.Subject = "فرش مشهد";
                DateTime today = DateTime.Today;
                //if (p.BlogDescription.Length > 200)
                //{
                mail.Body =
                     @"
  <style>
        #content.no-content form input::-webkit-input-placeholder {
            color: #fff;
            text-transform: uppercase;
        }

        .no-content-box {
            width: 771px;
            height: 514px;
            background-image: url('../images/404-contentbg.png');
            background-repeat: no-repeat;
            text-align: center;
            margin: 55px auto 60px;
            padding: 30px;
        }

            .no-content-box h2 {
                color: #2f4497;
                margin-bottom: 20px;
            }

            .no-content-box h3 {
                font-size: 35px;
                color: #807e78;
                margin-bottom: 23px;
            }

            .no-content-box p {
                line-height: 34px;
                color: #9c978d;
                margin-bottom: 36px;
            }

            .no-content-box form input {
                background-color: #f7f5f2;
                border-color: #e2e1d9;
                color: #8b8475;
                box-shadow: inset 0 0 0 1px rgba(255, 255, 255, 0.5);
            }

            .no-content-box .submit-btn {
                background-position: -299px -536px;
            }

            .no-content-box form input:-moz-placeholder {
                color: #8b8475;
                text-transform: uppercase;
            }

            .no-content-box form input::-moz-placeholder {
                color: #8b8475;
                text-transform: uppercase;
            }

            .no-content-box form input:-ms-input-placeholder {
                color: #8b8475;
                text-transform: uppercase;
            }

            .no-content-box form input::-webkit-input-placeholder {
                color: #8b8475;
                text-transform: uppercase;
            }

        .vcenter-container {
            display: table;
            height: 100%;
            table-layout: fixed;
            width: 100%;
        }
    </style>
<div style='padding: 5px; direction: rtl;'>
            <section id='content' role='main'>
                <div style='width: 1200px; margin-left: auto; margin-right: auto; padding-left: 15px; padding-right: 15px;'>
                    <div class='no-content-box wow fadeInRightBig'>
                        <div class='vcenter-container'>
                            <div class='vcenter'>
                                <h2>فرش مشهد</h2>
                                <h3>ثبت نام شما با موفقیت انجام شد.</h3>
                                <p>
                                    شما این ایمیل را دریافت کرده اید چون در وب سایت فرش مشهد عضو شده اید
                                 <br />
                                    جهت پاسخگویی به سوالات احتمالی با شماره  22012612 (021) تماس حاصل فرمایید و یا به بخش سوالات متداول در فروشگاه اینترنتی فرش مشهد به آدرس <a href='http://mashadcarpet.com'>mashadcarpet.com</a> مراجعه فرمائید.
                                    <br />

                                </p>
                            </div>

                        </div>

                    </div>

                </div>

            </section>
        </div>";

              

                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("185.129.169.25");

                System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("register@mashadcarpet.com",
               "k70kjX5%");
                mail.Headers.Add("Message-Id", String.Concat("<", DateTime.Now.ToString("yyMMdd"), ".", DateTime.Now.ToString("HHmmss"), "register@mashadcarpet.com>"));

                smtp.UseDefaultCredentials = false;

                smtp.Credentials = basicAuthenticationInfo;

                mail.Priority = MailPriority.Normal;

                smtp.Send(mail);
                //}


            }
        }
    }
}