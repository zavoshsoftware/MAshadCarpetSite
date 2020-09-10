using MashadCarpet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MashadCarpet.Admin
{
    public partial class TicketResponseSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadForm();
                RptAnswersBind();
            }
        }

        public void LoadForm()
        {
            Guid TicketID = new Guid(Request.QueryString["TicketID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Tickets.AsEnumerable()
                         join i in db.Users.AsEnumerable() on u.fk_UserID equals i.UserID
                         //join p in db.TicketGroup on u.fk_TicketGroupID equals p.TicketGroupID
                         where u.IsDelete == false && u.TicketID == TicketID
                         select new
                         {
                             Name=i.UserName+" "+i.UserFamily,
                             //p.TicketGroupTitle,
                             u.TicketSubject,
                             TicketDate = string.Format("{0:d}", u.TicketDate),
                             u.TicketMessage,
                             u.Status

                         }).FirstOrDefault();


                lblTicketDate.Text = n.TicketDate;
                //lblTicketGroup.Text = n.TicketGroupTitle;
                lblTicketMessage.Text = n.TicketMessage;
                lblTicketSubject.Text = n.TicketSubject;
                lblUserName.Text = n.Name;

                if (n.Status == 1)
                    lblStatus.Text = "درانتظار پاسخ";
                else if (n.Status == 2)
                    lblStatus.Text = "پاسخ داده شده";
                else if (n.Status == 3)
                    lblStatus.Text = "پاسخ مشتری";
                else if (n.Status == 4)
                    lblStatus.Text = "بسته شده";
            }


        }


        public void RptAnswersBind()
        {
            Guid TicketID = new Guid(Request.QueryString["TicketID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.TicketResponse.AsEnumerable()
                         join i in db.Users on u.fk_UserID equals i.UserID
                         where u.fk_TicketID == TicketID && u.IsDelete == false
                         orderby u.ResponseDate descending
                         select new
                         {
                             u.TicketResponseID,
                             ResponseDate = string.Format("{0:d}", u.ResponseDate),
                             Name=i.UserName+" "+i.UserFamily,
                             ResponseText = u.ResponseText.Length > 250 ? u.ResponseText.Substring(0, 250) + "..." : u.ResponseText,

                             //(u.ArticleSummery.Length > 100) ? u.ArticleSummery.Substring(0, 100) : u.ArticleSummery,
                         }).ToList();

                rptAnswers.DataSource = n;
                rptAnswers.DataBind();
                if (n.Count == 0)
                    pnlEmptyForm.Visible = true;
                else
                    pnlEmptyForm.Visible = false;
            }
            mvSetting.SetActiveView(vwList);
        }

        protected void btnAnswer_Click(object sender, EventArgs e)
        {
            reResponseText.Content = string.Empty;
            pnlSubmitAnswer.Visible = true;
            ViewState["btn"] = "Insert";
            mvSetting.SetActiveView(vwEdit);

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ViewState["btn"].ToString() == "Insert")
                InsertForm();

            else if (ViewState["btn"].ToString() == "Update")
                UpdateForm();

            pnlSubmitAnswer.Visible = false;
            RptAnswersBind();
            mvSetting.SetActiveView(vwList);
        }

        public void InsertForm()
        {
            Guid TicketID = new Guid(Request.QueryString["TicketID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                TicketResponse t = new TicketResponse();

                t.TicketResponseID = Guid.NewGuid();
                t.fk_TicketID = TicketID;
                t.ResponseText = reResponseText.Content;
                t.ResponseDate = DateTime.Now;

                //t.fk_UserID = new Guid(HttpContext.Current.User.Identity.Name);

                t.fk_UserID = new Guid("ee4edaf0-aad0-4140-a9fe-8b9bcaab6bd3");
                t.IsDelete = false;

                db.TicketResponse.Add(t);
                db.SaveChanges();

                var m = (from u in db.Tickets where u.TicketID == TicketID select u).FirstOrDefault();
                m.Status = 2;
                db.SaveChanges();

                //SendMail(t.TicketResponseID);
            }
        }

//        public void SendMail(Guid ID)
//        {

//            using (MashadCarpetEntities db = new MashadCarpetEntities())
//            {
//                var n = (from u in db.TicketResponse
//                         join i in db.Tickets on u.fk_TicketID equals i.TicketID
//                         join p in db.Users on i.fk_UserID equals p.UserID
//                         where u.TicketResponseID == ID
//                         select new
//                         {
//                             p.Email,
//                             u.ResponseText,
//                             i.TicketMessage
//                         }).FirstOrDefault();

//                MailMessage mail = new MailMessage();

//                mail.From = new MailAddress("info@drjart.ir");

//                mail.To.Add(n.Email);

//                mail.Subject = "پاسخ سوال شما";

//                DateTime today = DateTime.Today;
//                mail.Body =
//                    @" <div style ='width:500px; min-height:600px; background-color:#706c6c; padding-top:20px; margin:0 auto; direction:rtl; '>
//        <div style='width:400px; margin:0 auto; min-height:300px; background-color:#fff;color:#000; padding:5px;'>
//           <p style='color:#cf1c1c; '>با سلام</p>
//            <p style='color:#000; border-bottom:1px dotted #000; '>
//              " + n.TicketMessage + @"<br />
//                        <p style='color:#000; border-bottom:1px dotted #000; '>پاسخ:  
//              " + n.ResponseText + @"<br />
//            </p>
//             
//            
//        </div>
//
//       </div>";


//                mail.IsBodyHtml = true;
//                SmtpClient smtp = new SmtpClient("195.154.169.92");

//                System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("info@drjart.ir",
//                    "123qwe!@#QWE");
//                mail.Headers.Add("Message-Id", String.Concat("<", DateTime.Now.ToString("yyMMdd"), ".", DateTime.Now.ToString("HHmmss"), "info@drjart.ir>"));

//                smtp.UseDefaultCredentials = false;

//                smtp.Credentials = basicAuthenticationInfo;

//                mail.Priority = MailPriority.Normal;

//                smtp.Send(mail);



//            }
//        }


        public void UpdateForm()
        {
            Guid TicketResponseID = new Guid(ViewState["TicketResponseID"].ToString());
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.TicketResponse where u.TicketResponseID == TicketResponseID select u).FirstOrDefault();

                n.ResponseText = reResponseText.Content;

                db.SaveChanges();
            }
        }

        protected void rptAnswers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Guid TicketResponseID = new Guid(e.CommandArgument.ToString());

            ViewState["TicketResponseID"] = TicketResponseID;
            switch (e.CommandName)
            {
                case "DoEdit":
                    {

                        using (MashadCarpetEntities db = new MashadCarpetEntities())
                        {
                            var n = (from u in db.TicketResponse where u.TicketResponseID == TicketResponseID select u).FirstOrDefault();

                            reResponseText.Content = n.ResponseText;
                        }
                        mvSetting.SetActiveView(vwEdit);
                        ViewState["btn"] = "Update";
                        pnlSubmitAnswer.Visible = true;
                        break;
                    }

                case "DoDelete":
                    {
                        using (MashadCarpetEntities db = new MashadCarpetEntities())
                        {
                            var n = (from u in db.TicketResponse where u.TicketResponseID == TicketResponseID select u).FirstOrDefault();

                            n.IsDelete = true;
                            n.DeleteDate = DateTime.Now;

                            db.SaveChanges();

                        }
                        RptAnswersBind();
                        break;
                    }

                case "Show":
                    {
                        using (MashadCarpetEntities db = new MashadCarpetEntities())
                        {
                            var n = (from u in db.TicketResponse
                                     join i in db.Users.AsEnumerable() on u.fk_UserID equals i.UserID
                                     where u.TicketResponseID == TicketResponseID
                                     select new
                                     {
                                         u.ResponseText,
                                         Name=i.UserName+" "+i.UserFamily,
                                         u.ResponseDate
                                     }).FirstOrDefault();
                            lblAnswer.Text = n.ResponseText;
                            lblUserAnswerName.Text = n.Name;
                            lblAnswerDate.Text = string.Format("{0:d}", n.ResponseDate);
                        }

                        mvSetting.SetActiveView(vwShow);
                        break;
                    }
            }
        }

        protected void lbEdit_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Guid TicketID = new Guid(Request.QueryString["TicketID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Tickets where u.TicketID == TicketID select u).FirstOrDefault();

                Response.Redirect("TicketSetting.aspx?StatusID=" + n.Status);
            }
        }

    }
}