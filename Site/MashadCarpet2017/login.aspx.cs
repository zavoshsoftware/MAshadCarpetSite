using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MashadCarpet.Models;
using System.Web.Security;
using MashadCarpet.Classes;

namespace MashadCarpet
{
    public partial class login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ChooseTitleAndDesc();
           
            if (Request.QueryString["RetUrl"] != null)
                pnlRetURL.Visible = true;
            else
                pnlRetURL.Visible = false;
        }
        public void ChooseTitleAndDesc()
        {
            if (IdentifyCulture.cultureName().Contains("fa"))
            {
                Page.Title = "ورود به حساب کاربری";
               
            }
            else if (IdentifyCulture.cultureName().Contains("en"))
            {
                Page.Title = "Login"; 
            }
            else if (IdentifyCulture.cultureName().Contains("ru"))
            {
                Page.Title = "Login"; 
            }
            else if (IdentifyCulture.cultureName().Contains("zh"))
            {
                Page.Title = "Login"; 
            }
            else
            {
                Page.Title = "ورود به حساب کاربری"; 
            }
        }
        protected void cvLogin_ServerValidate(object source, ServerValidateEventArgs args)
        {
            using(MashadCarpetEntities db=new MashadCarpetEntities())
            {
                var n = (from us in db.Users
                         where us.Email == txtEmail.Text && us.Password == txtPass.Text
                         select us
                        ).FirstOrDefault();
                args.IsValid = n != null;

                if (args.IsValid)
                {
                    ViewState["UserID"] = n.UserID;
                }
                else
                {
                    Insert_Log_LoginAttemp(false);
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (Request.QueryString["RetUrl"] == null)
                {
                    FormsAuthentication.SetAuthCookie(ViewState["UserID"].ToString(), false);
                    Guid UserID = new Guid(ViewState["UserID"].ToString());
                    Insert_Log_LoginAttemp(true);
                    using (MashadCarpetEntities db = new MashadCarpetEntities())
                    {
                        var n = (from a in db.Users
                                 where a.UserID == UserID
                                 select a).FirstOrDefault();

                         
                            Response.Redirect("~/Default.aspx");
                         
                    }

                }
                else
                {
                    FormsAuthentication.SetAuthCookie(ViewState["UserID"].ToString(), false);
                    Insert_Log_LoginAttemp(true);
                    //Response.Redirect("Default.aspx");
                    Response.Redirect(Request.QueryString["RetUrl"]);
                }

               
            }
        }
        public void Insert_Log_LoginAttemp(Boolean IsSuccess)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                Log_LoginAttemps lmEnter = new Log_LoginAttemps();

                lmEnter.IP = Request.UserHostAddress;
                lmEnter.VisitDate = DateTime.Now;

                if (Request.UrlReferrer != null)
                {
                    lmEnter.RefrallPage = Request.UrlReferrer.ToString();
                }
                else
                {
                    lmEnter.RefrallPage = "ورود مستقیم";
                }
                System.Web.HttpBrowserCapabilities browser = Request.Browser;
                lmEnter.Browser = browser.Type;
                lmEnter.OS = OSIdentify.OSName();
                lmEnter.Username = txtEmail.Text;
                lmEnter.Password = txtPass.Text;
                lmEnter.IsSuccess = IsSuccess;

                db.Log_LoginAttemps.Add(lmEnter);
                db.SaveChanges();
            }
  
        }
        protected void lbRegister_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["RetUrl"] == null)
                Response.Redirect("/Register.aspx");
            else
            {
                Response.Redirect("/Register.aspx?RetUrl=" + Request.QueryString["RetUrl"]);
            }
        }
    }
}