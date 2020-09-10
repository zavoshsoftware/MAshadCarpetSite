using MashadCarpet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spire.Pdf;
using Spire.Pdf.HtmlConverter;
using System.Text;
using System.Threading;
using System.IO;

namespace MashadCarpet
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           Services.SmsService sms=new Services.SmsService();
            List<string> list=new List<string>();
            list.Add("09124806404");
            sms.Send(list, "messageBody", "");

        }
        
    }

      
}