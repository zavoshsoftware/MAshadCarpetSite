using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MashadCarpet.Controls
{
    public partial class UCLanguage : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Color Beige = ColorTranslator.FromHtml("#E6DFD1");
            Color NoColor = ColorTranslator.FromHtml("#FBFAF4");
            if (!string.IsNullOrEmpty(Convert.ToString(Session["lang"])))
            {
               
                if (Convert.ToString(Session["lang"]) == "en")
                {
                   
                    lbFa.Enabled = true;
                    lbChine.Enabled = true;
                    lbRus.Enabled = true;
                    lbEng.Enabled = false;
                  //  liCurrentLangLong.Text = "English";
                    imgCurrentLang.ImageUrl = "~/images/flags/eng.jpg";
                    lbEng.BackColor = Beige;
                    lbRus.BackColor = NoColor;
                    lbChine.BackColor = NoColor;
                    lbFa.BackColor = NoColor;
                }
                else if (Convert.ToString(Session["lang"]) == "ru")
                {
                    lbFa.Enabled = true;
                    lbChine.Enabled = true;
                    lbRus.Enabled = false;
                    lbEng.Enabled = true;
                    imgCurrentLang.ImageUrl = "~/images/flags/RussiaFlag.jpg";
                  //  liCurrentLangLong.Text = "русский";
                    lbRus.BackColor = Beige;

                    lbEng.BackColor = NoColor;
                    lbChine.BackColor = NoColor;
                    lbFa.BackColor = NoColor;

                }
                else if (Convert.ToString(Session["lang"]) == "zh")
                {
                    lbFa.Enabled = true;
                    lbChine.Enabled = false;
                    lbRus.Enabled = true;
                    lbEng.Enabled = true;
                    imgCurrentLang.ImageUrl = "~/images/flags/chnFlag.jpg";

                  //  liCurrentLangLong.Text = "中文";
                    lbChine.BackColor = Beige;

                    lbEng.BackColor = NoColor;
                    lbRus.BackColor = NoColor;
                    lbFa.BackColor = NoColor;

                }
                else
                {
                    lbEng.Enabled = true;
                    lbFa.Enabled = false;
                    lbChine.Enabled = true;
                    lbRus.Enabled = true;
                    imgCurrentLang.ImageUrl = "~/images/flags/ir.jpg";
                //    liCurrentLangLong.Text = "فارسی";
                    lbFa.BackColor = Beige;

                    lbRus.BackColor = NoColor;
                    lbEng.BackColor = NoColor;
                    lbChine.BackColor = NoColor;

                }
            }
            else
            {
                lbEng.Enabled =true ;
                lbFa.Enabled = false;
                imgCurrentLang.ImageUrl = "~/images/flags/ir.jpg";

             //   liCurrentLangLong.Text = "فارسی";
                lbFa.BackColor = Beige;

               
                lbRus.BackColor = NoColor;
                lbEng.BackColor = NoColor;
                lbChine.BackColor = NoColor;

            }
            
        }

        protected void lbEnglish_Click(object sender, EventArgs e)
        {
            Session["lang"] = "en";
            Response.Redirect(Request.RawUrl);

        }

        protected void lbFa_Click(object sender, EventArgs e)
        {
            Session["lang"] = "fa";
            Response.Redirect(Request.RawUrl);


        }

        protected void lbChine_Click(object sender, EventArgs e)
        {
            Session["lang"] = "zh";
            Response.Redirect(Request.RawUrl);

        }

        protected void lbRus_Click(object sender, EventArgs e)
        {
            Session["lang"] = "ru";
            Response.Redirect(Request.RawUrl);

        }
    }
}