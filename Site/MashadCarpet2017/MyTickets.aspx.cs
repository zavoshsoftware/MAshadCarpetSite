using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MashadCarpet.Models;
using MashadCarpet.Classes;

namespace MashadCarpet
{
    public partial class MyTickets : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                rptTicketsBind();
            }
        }
        public void rptTicketsBind()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Guid UserID = new Guid(HttpContext.Current.User.Identity.Name);

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.Tickets
                             where u.IsDelete == false && u.fk_UserID == UserID
                             orderby u.TicketDate descending
                             select u).ToList();
                    rptTickets.DataSource = n;
                    rptTickets.DataBind();
                }

            }
            else
            {
                string strPage = HttpContext.Current.Request.RawUrl;
                Response.Redirect("/login?RetUrl=" + strPage);
            }
        }

        protected void rptTickets_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hfTicketID = (HiddenField)e.Item.FindControl("hfTicketID");
                Guid TicketID = new Guid(hfTicketID.Value.ToString());
                Label lblStatus = (Label)e.Item.FindControl("lblStatus");

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.Tickets where u.TicketID == TicketID select u).FirstOrDefault();
                    if (n.Status == 1)
                    {
                        lblStatus.Text = (IdentifyCulture.cultureName().Contains("fa")) ? "در انتظار پاسخ" :
                              ((IdentifyCulture.cultureName().Contains("en")) ? "waiting for an answer" :
                              ((IdentifyCulture.cultureName().Contains("ru")) ? "В ожидании ответа" :
                              ((IdentifyCulture.cultureName().Contains("zh")) ? "等待回复" : "در انتظار پاسخ")));
                    }
                    //lblStatus.Text="در انتظار پاسخ";
                    else if (n.Status == 2)
                    {
                        lblStatus.Text = (IdentifyCulture.cultureName().Contains("fa")) ? "پاسخ داده شده" :
                              ((IdentifyCulture.cultureName().Contains("en")) ? "has been answered" :
                              ((IdentifyCulture.cultureName().Contains("ru")) ? "Ответил" :
                              ((IdentifyCulture.cultureName().Contains("zh")) ? "回答" : "پاسخ داده شده")));
                    }
                    //lblStatus.Text = "پاسخ داده شده";
                    else if (n.Status == 3)
                          {
                              lblStatus.Text = (IdentifyCulture.cultureName().Contains("fa")) ? "پاسخ مشتری" :
                              ((IdentifyCulture.cultureName().Contains("en")) ? "Customer response" :
                              ((IdentifyCulture.cultureName().Contains("ru")) ? "Клиент ответ" :
                              ((IdentifyCulture.cultureName().Contains("zh")) ? "客户响应" : "پاسخ مشتری")));
                    }
                        //lblStatus.Text = "پاسخ مشتری";
                    else if (n.Status == 4)
                    {
                        lblStatus.Text = (IdentifyCulture.cultureName().Contains("fa")) ? "بسته شده" :
                        ((IdentifyCulture.cultureName().Contains("en")) ? "closed" :
                        ((IdentifyCulture.cultureName().Contains("ru")) ? "запертый" :
                        ((IdentifyCulture.cultureName().Contains("zh")) ? "锁定" : "بسته شده")));
                    }
                        //lblStatus.Text = "بسته شده";
                }
            }
        }

        protected void rptTickets_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Guid TicketID = new Guid(e.CommandArgument.ToString());
            ViewState["TicketID"] = TicketID;
            if (e.CommandName == "Show")
            {
                pnlTickets.Visible = false;
                pnlAswers.Visible = true;
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.Tickets
                             join i in db.Users on u.fk_UserID equals i.UserID
                             where u.TicketID == TicketID
                             select new
                                 {
                                     u.TicketDate,
                                     i.UserName,
                                     i.UserFamily,
                                     u.TicketMessage
                                 }).FirstOrDefault();
                    //lblDate.Text = "تاریخ: " + string.Format("{0:d}", n.TicketDate);
                    lblDate.Text = (IdentifyCulture.cultureName().Contains("fa")) ? "تاریخ:" + string.Format("{0:d}", n.TicketDate) :
                        ((IdentifyCulture.cultureName().Contains("en")) ? "Date:" + string.Format("{0:d}", n.TicketDate) :
                        ((IdentifyCulture.cultureName().Contains("ru")) ? "история:" + string.Format("{0:d}", n.TicketDate) :
                        ((IdentifyCulture.cultureName().Contains("zh")) ? "历史:" + string.Format("{0:d}", n.TicketDate) : "تاریخ" + string.Format("{0:d}", n.TicketDate))));
                    lblUserName.Text = "نام کاربر: " + n.UserName + " " + n.UserFamily;
                    lblUserName.Text = (IdentifyCulture.cultureName().Contains("fa")) ? "نام کاربر: " + n.UserName + " " + n.UserFamily :
                        ((IdentifyCulture.cultureName().Contains("en")) ? "user name: " + n.UserName + " " + n.UserFamily :
                        ((IdentifyCulture.cultureName().Contains("ru")) ? "имя пользователя: " + n.UserName + " " + n.UserFamily :
                        ((IdentifyCulture.cultureName().Contains("zh")) ? "用户名: " + n.UserName + " " + n.UserFamily : "نام کاربر: " + n.UserName + " " + n.UserFamily)));
                    lblText.Text = n.TicketMessage;

                    rptAnswersBind(TicketID);


                }
            }
        }


        public void rptAnswersBind(Guid TicketID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var m = (from u in db.TicketResponse
                         join i in db.Users.AsEnumerable() on u.fk_UserID equals i.UserID
                         where u.fk_TicketID == TicketID && u.IsDelete == false
                         orderby u.ResponseDate descending
                         select new
                         {
                             u.ResponseDate,
                             u.ResponseText,
                             UserName = i.UserName + " " + i.UserFamily
                         }).ToList();
                rptAnswers.DataSource = m;
                rptAnswers.DataBind();
            }
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            Guid TicketID = new Guid(ViewState["TicketID"].ToString());
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Guid UserID = new Guid(HttpContext.Current.User.Identity.Name);
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    TicketResponse t = new TicketResponse();

                    t.TicketResponseID = Guid.NewGuid();
                    t.fk_TicketID = TicketID;
                    t.fk_UserID = UserID;
                    t.ResponseText = txtAnswerText.Text;
                    t.ResponseDate = DateTime.Now;
                    t.IsDelete = false;

                    db.TicketResponse.Add(t);
                    db.SaveChanges();
                }
                rptAnswersBind(TicketID);
                txtAnswerText.Text = string.Empty;
            }
        }

        protected void lbBack_Click(object sender, EventArgs e)
        {
            pnlAswers.Visible = false;
            pnlTickets.Visible = true;
        }
    }
}