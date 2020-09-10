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
    public partial class BlogsSetting : System.Web.UI.Page
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
            if (Request.QueryString["ID"] == null)
            {
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    if (Request.QueryString["type"] == null)
                    {
                        var n = (from u in db.Blogs
                                 join i in db.BlogGroups on u.fk_BlogGroupID equals i.BlogGroupID
                                 
                                 where u.IsDelete == false && i.IsDelete == false
                                 select new
                                 {
                                     u.BlogID,
                                     u.BlogName,
                                     
                                     u.BlogTitle,
                                     u.SubmitDate,
                                     u.BlogImage,
                                     i.BlogGroupTitle,
                                     
                                     u.VisitCounts
                                 }).ToList();

                        grdTable.DataSource = n;
                        grdTable.DataBind();
                        if (n.Count == 0)
                            pnlEmptyForm.Visible = true;
                        else
                            pnlEmptyForm.Visible = false;
                    }
                    else
                    {
                        var n = (from u in db.Blogs
                                 join i in db.BlogGroups on u.fk_BlogGroupID equals i.BlogGroupID
                                
                                 where u.IsDelete == true && i.IsDelete == false
                                 select new
                                 {
                                     u.BlogID,
                                     u.BlogName,
                                    
                                     u.BlogTitle,
                                     u.SubmitDate,
                                     u.BlogImage,
                                     i.BlogGroupTitle,
                                   
                                     u.VisitCounts
                                 }).ToList();

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
                if (Request.QueryString["type"] == null)
                {
                    pnlGroup.Visible = false;
                    Guid BlogGroupID = new Guid(Request.QueryString["ID"].ToString());
                    using (MashadCarpetEntities db = new MashadCarpetEntities())
                    {
                        var n = (from u in db.Blogs
                                 join i in db.BlogGroups on u.fk_BlogGroupID equals i.BlogGroupID
                                 join p in db.Users on u.fk_UserID equals p.UserID
                                 where u.IsDelete == false && i.IsDelete == false && i.BlogGroupID == BlogGroupID
                                 select new
                                 {
                                     u.BlogID,
                                     u.BlogName,
                                     u.EN_BlogTitle,
                                     u.BlogTitle,
                                     u.SubmitDate,
                                     u.BlogImage,
                                     i.BlogGroupTitle,
                                     Name = p.UserName + " " + p.UserFamily,
                                     u.Rus_BlogTitle,
                                     u.China_BlogTitle,
                                     u.VisitCounts
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
                    pnlGroup.Visible = false;
                    Guid BlogGroupID = new Guid(Request.QueryString["ID"].ToString());
                    using (MashadCarpetEntities db = new MashadCarpetEntities())
                    {
                        var n = (from u in db.Blogs
                                 join i in db.BlogGroups on u.fk_BlogGroupID equals i.BlogGroupID
                                 join p in db.Users on u.fk_UserID equals p.UserID
                                 where u.IsDelete == true && i.IsDelete == false && i.BlogGroupID == BlogGroupID
                                 select new
                                 {
                                     u.BlogID,
                                     u.BlogName,
                                     u.EN_BlogTitle,
                                     u.BlogTitle,
                                     u.SubmitDate,
                                     u.BlogImage,
                                     i.BlogGroupTitle,
                                     Name = p.UserName + " " + p.UserFamily,
                                     u.Rus_BlogTitle,
                                     u.China_BlogTitle,
                                     u.VisitCounts
                                 }).ToList();

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

            mvSetting.SetActiveView(vwEdit);
            ViewState["btn"] = "Insert";
            ResetForm();
        }


        public void ResetForm()
        {
            txtTitle.Text = string.Empty;
            txtName.Text = string.Empty;
            reDesc.Content = string.Empty;
            ddlGroup.SelectedValue = "-1";
            txtEN_Title.Text = string.Empty;
            reEN_Desc.Content = null;
            txtChina_Title.Text = string.Empty;
            txtRus_Title.Text = string.Empty;
            reRus_Desc.Content = null;
            reChina_Desc.Content = null;
            txtSummery.Text = string.Empty;
            txtSummery_Chine.Text = string.Empty;
            txtSummery_En.Text = string.Empty;
            txtSummery_Rus.Text = string.Empty;

        }

        public void DropDownBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var m = (from u in db.BlogGroups
                         where u.IsDelete == false
                         select u).ToList();
                ddlGroup.Items.Clear();
                ddlGroup.Items.Add(new ListItem("گروه ", "-1"));
                foreach (var i in m)
                    ddlGroup.Items.Add(new ListItem(i.BlogGroupTitle, i.BlogGroupID.ToString()));


                //var pg = (from u in db.Users
                //          select u).ToList();
                //ddlUser.Items.Clear();
                //ddlUser.Items.Add(new ListItem("کاربر ", "-1"));
                //foreach (var t in pg)
                //    ddlUser.Items.Add(new ListItem(t.Name, t.UserID.ToString()));
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

            Guid BlogID = new Guid(ViewState["BlogID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var p = (from u in db.Blogs where u.BlogID == BlogID select u).FirstOrDefault();

                p.BlogTitle = txtTitle.Text;
                p.BlogName = txtName.Text;
                p.BlogText = reDesc.Content;
                p.fk_BlogGroupID = new Guid(ddlGroup.SelectedValue.ToString());
                //p.fk_UserID = new Guid(ddlUser.SelectedValue);
                p.BlogImage = ViewState["NewImg"].ToString();
                p.EN_BlogTitle = txtEN_Title.Text;
                p.EN_BlogText = reEN_Desc.Content;
                p.Rus_BlogTitle = txtRus_Title.Text;
                p.Rus_BlogText = reRus_Desc.Content;
                p.China_BlogTitle = txtChina_Title.Text;
                p.China_BlogText = reChina_Desc.Content;
                p.BlogSummery = txtSummery.Text;
                p.China_BlogSummery = txtSummery_Chine.Text;
                p.EN_BlogSummery = txtSummery_En.Text;
                p.Rus_BlogSummery = txtSummery_Rus.Text;

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
                Blogs p = new Blogs();

                p.BlogID = Guid.NewGuid();
                p.BlogTitle = txtTitle.Text;
                p.BlogName = txtName.Text;
                p.BlogText = reDesc.Content;
                if (Request.QueryString["ID"] == null)
                    p.fk_BlogGroupID = new Guid(ddlGroup.SelectedValue.ToString());
                else
                    p.fk_BlogGroupID = new Guid(Request.QueryString["ID"].ToString());
                p.fk_UserID = new Guid(HttpContext.Current.User.Identity.Name);
                p.BlogImage = new_filename;
                p.IsDelete = false;
                p.VisitCounts = 0;
                p.EN_BlogText = reEN_Desc.Content;
                p.EN_BlogTitle = txtEN_Title.Text;
                p.SubmitDate = DateTime.Now;
                p.Rus_BlogTitle = txtRus_Title.Text;
                p.Rus_BlogText = reRus_Desc.Content;
                p.China_BlogTitle = txtChina_Title.Text;
                p.China_BlogText = reChina_Desc.Content;
                p.BlogSummery = txtSummery.Text;
                p.China_BlogSummery = txtSummery_Chine.Text;
                p.EN_BlogSummery = txtSummery_En.Text;
                p.Rus_BlogSummery = txtSummery_Rus.Text;

                db.Blogs.Add(p);
                db.SaveChanges();

                //SendMail(p.BlogID);
            }
        }

        //        public void SendMail(Guid ID)
        //        {
        //            using (MashadCarpetEntities db = new MashadCarpetEntities())
        //            {
        //                var p = (from u in db.Blogs where u.BlogID == ID select u).FirstOrDefault();
        //                var n = (from u in db.NewsLetters where u.IsDelete == false && u.IsValid == true select u).ToList();
        //                foreach (var m in n)
        //                {


        //                    MailMessage mail = new MailMessage();

        //                    mail.From = new MailAddress("blog@drjart.ir");

        //                    mail.To.Add(m.NewsLetterEmail);

        //                    mail.Subject = p.BlogTitle;

        //                    DateTime today = DateTime.Today;
        //                    if (p.BlogDescription.Length > 200)
        //                    {
        //                        mail.Body =
        //                             @" <div style='width: 400px; margin: 0 auto; min-height: 300px; background-color: #fff; color: #000; padding: 5px;direction:rtl;'>
        //                <p style='border-bottom: 1px dotted #000; padding: 5px;text-align:justify;direction:rtl;padding-bottom:15px;margin-bottom:15px;'>
        //                    سلام
        // <br />
        //                    ممنون که بلاگ‌ دکتر جارت رو دنبال می‌کنین، اگر پیشنهادی در رابطه با مطالبمون دارین، خوشحال میشیم که نظرات خودتون رو برامون در قسمت نظرات مطالب ارسال کنید
        //                </p>
        //                <p style='color: #00AFEC; text-align: center;'>" + p.BlogTitle + @"</p>
        //                <div style='height: auto;'>
        //                    <p style='color: #000; width: 55%; float: right;padding:5px;'>
        //                    " + p.BlogDescription.Substring(0, 200) + "..." + @"<br /><br />
        //
        //    <a style='background-color: #198FFF;color:#fff;text-decoration:none;padding:10px;' href='http://www.drjart.ir/Blog/" + p.BlogName + @"'>ادامه مطلب</a>
        //                      
        //                    </p>
        //                    <p style='color: #000; width: 40%; height:100%; float: right;'>
        //                        <img src='http://www.drjart.ir/Uploads/Blogs/" + p.BlogImage + @"' style='width:100%;height:100%;' />
        //                    </p>
        //  <p style='clear:both;'></p>
        //                </div><br /><br />
        //                <div style='height: auto;padding:5px;margin-top:10px;color:#7C7F82;'>
        //                     <p style='padding:5px;text-align:center;'>
        //                         شما این ایمیل رو دریافت کردید چون در خبرنامه وبلاگ دکتر جارت عضو شدید
        //                         <br />
        // ایران - تهران - خیابان ولیعصر- برج سپهر ساعی 
        //<br />
        //کلیه حقوق برای شرکت دکتر جارت محفوظ است
        //                     </p>
        //                 </div>
        //            </div>";

        //                    }
        //                    else
        //                    {
        //                        mail.Body =
        //                        @" <div style='width: 400px; margin: 0 auto; min-height: 300px; background-color: #fff; color: #000; padding: 5px;direction:rtl;'>
        //                <p style='border-bottom: 1px dotted #000; padding: 5px;text-align:justify;direction:rtl;padding-bottom:15px;margin-bottom:15px;'>
        //                    سلام
        // <br />
        //                    ممنون که بلاگ‌ دکتر جارت رو دنبال می‌کنین، اگر پیشنهادی در رابطه با مطالبمون دارین، خوشحال میشیم که نظرات خودتون رو برامون در قسمت نظرات مطالب ارسال کنید
        //                </p>
        //                <p style='color: #00AFEC; text-align: center;'>" + p.BlogTitle + @"</p>
        //                <div style='height: auto;'>
        //                    <p style='color: #000; width: 55%; float: right;padding:5px;'>
        //                    " + p.BlogDescription + "..." + @"<br /><br />
        //
        //  <a style='background-color: #198FFF;color:#fff;text-decoration:none;padding:10px;' href='http://www.drjart.ir/Blog/" + p.BlogName + @"'>ادامه مطلب</a>
        //                      
        //                    </p>
        //                    <p style='color: #000; width: 40%; height:100%; float: right;'>
        //                        <img src='http://www.drjart.ir/Uploads/Blogs/" + p.BlogImage + @"' style='width:100%;height:100%;' />
        //                    </p>   <p style='clear:both;'></p> </div><br /><br />
        // <div style='height: auto;padding:5px;margin-top:10px;color:#7C7F82;'>
        //                     <p style='padding:5px;text-align:center;'>
        //                         شما این ایمیل رو دریافت کردید چون در خبرنامه وبلاگ دکتر جارت عضو شدید
        //                         <br />
        // ایران - تهران - خیابان ولیعصر- برج سپهر ساعی 
        //<br />
        //کلیه حقوق برای شرکت دکتر جارت محفوظ است
        //                     </p>
        //                 </div>
        //                </div>
        //                
        //            </div>";

        //                    }

        //                    mail.IsBodyHtml = true;
        //                    SmtpClient smtp = new SmtpClient("185.10.72.134");

        //                    System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("blog@drjart.ir",
        //                   "111qqq!!!QQQ");
        //                    mail.Headers.Add("Message-Id", String.Concat("<", DateTime.Now.ToString("yyMMdd"), ".", DateTime.Now.ToString("HHmmss"), "blog@drjart.ir>"));

        //                    smtp.UseDefaultCredentials = false;

        //                    smtp.Credentials = basicAuthenticationInfo;

        //                    mail.Priority = MailPriority.Normal;

        //                    smtp.Send(mail);
        //                }


        //            }
        //        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] == null)
                Response.Redirect("Default.aspx");
            else
                Response.Redirect("BlogsGroupSetting.aspx");
        }
        protected void btnYes_Click(object sender, EventArgs e)
        {
            Guid BlogID = new Guid(ViewState["BlogID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Blogs where u.BlogID == BlogID select u).FirstOrDefault();

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
            Guid BlogID = new Guid(e.CommandArgument.ToString());
            ViewState["BlogID"] = BlogID;

            switch (e.CommandName)
            {
                case "DoEdit":
                    {
                        ViewState["btn"] = "Update";
                        DropDownBind();
                        FillViewEdit(BlogID);
                        break;
                    }
                case "DoDelete":
                    {
                        using (MashadCarpetEntities db = new MashadCarpetEntities())
                        {
                            var n = (from u in db.Blogs where u.BlogID == BlogID select u).FirstOrDefault();
                            lblDelete.Text = n.BlogTitle;
                            mvSetting.SetActiveView(vwDelete);
                        }
                        break;
                    }
            }
        }

        public void FillViewEdit(Guid BlogID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Blogs where u.BlogID == BlogID select u).FirstOrDefault();
                txtTitle.Text = n.BlogTitle;
                txtName.Text = n.BlogName;
                ddlGroup.SelectedValue = n.fk_BlogGroupID.ToString();
                // ddlUser.SelectedValue = n.fk_UserID.ToString();
                ViewState["NewImg"] = n.BlogImage;
                imgEditImages.ImageUrl = "~/Uploads/Blogs/" + n.BlogImage;
                reDesc.Content = n.BlogText;
                txtEN_Title.Text = n.EN_BlogTitle;
                reEN_Desc.Content = n.EN_BlogText;

                txtRus_Title.Text = n.Rus_BlogTitle;
                reRus_Desc.Content = n.Rus_BlogText;
                txtChina_Title.Text = n.China_BlogTitle;
                reChina_Desc.Content = n.China_BlogText;
                mvSetting.SetActiveView(vwEdit);
            }
        }

        protected void cvName_ServerValidate(object source, ServerValidateEventArgs args)
        {

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Blogs where u.BlogName == txtName.Text && u.IsDelete == false select u).FirstOrDefault();

                if (ViewState["btn"].ToString() == "Insert")
                {
                    args.IsValid = n == null;
                }
                else if (ViewState["btn"].ToString() == "Update")
                {
                    Guid BlogID = new Guid(ViewState["BlogID"].ToString());

                    var m = (from u in db.Blogs where u.BlogID == BlogID select u).FirstOrDefault();

                    if (m.BlogName == txtName.Text)
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

        protected void cvGroup_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = ddlGroup.SelectedIndex != 0;
        }
    }
}