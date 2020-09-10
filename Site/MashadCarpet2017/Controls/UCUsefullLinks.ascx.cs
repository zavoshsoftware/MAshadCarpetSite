using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MashadCarpet.Controls
{
    public partial class UCUsefullLinks : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if(HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    pnlLinks.Visible = false;
                    pnlLoginBox.Visible = true;
                  
                }
                else
                {
                    pnlLinks.Visible = true;
                    pnlLoginBox.Visible = false;
                }
            }
        }
    }
}