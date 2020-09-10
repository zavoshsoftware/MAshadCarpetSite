using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MashadCarpet.Models;

namespace MashadCarpet
{
    public partial class SendTicket : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                FindUser();
            }
        }

        public void FindUser()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string strPage = HttpContext.Current.Request.RawUrl;
                Response.Redirect("/login?RetUrl=" + strPage);
            }
                
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Guid UserID = new Guid(HttpContext.Current.User.Identity.Name.ToString());

                using(MashadCarpetEntities db=new MashadCarpetEntities())
                {
                    Tickets t = new Tickets();

                    t.TicketID = Guid.NewGuid();
                    t.fk_UserID = UserID;
                    t.TicketSubject = txtTicketSubject.Text;
                    t.TicketMessage = txtTicketMessage.Text;
                    t.TicketDate = DateTime.Now;
                    t.IsDelete = false;
                    t.IsValid = false;
                    t.Status = 1;

                    db.Tickets.Add(t);
                    db.SaveChanges();

                    txtTicketMessage.Text = string.Empty;
                    txtTicketSubject.Text = string.Empty;
                }
                pnlSuccess.Visible = true;
            }
        }
    }
}