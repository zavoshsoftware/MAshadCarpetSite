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
    public partial class Privacy : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                FindPrivacy();
                ChooseTitleAndDesc();
            }
        }
        public void ChooseTitleAndDesc()
        {
            if (IdentifyCulture.cultureName().Contains("fa"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "PageScriptfa",
                 "$('.newfa').removeClass('fa-arrow-right'); $('.newfa').addClass('fa-arrow-left');", true);
              
                Page.Title = "قوانین و مقررات | وب سایت رسمی فرش مشهد";
                Page.MetaDescription = "وب سایت رسمی فرش مشهد.";
            }
            else if (IdentifyCulture.cultureName().Contains("en"))
            {
                Page.Title = "Terms and Conditions | mashad carpet website";
                Page.MetaDescription = "mashad carpet website";
            }
            else if (IdentifyCulture.cultureName().Contains("ru"))
            {
                Page.Title = "Terms and Conditions | mashad carpet website";
                Page.MetaDescription = "mashad carpet website";
            }
            else if (IdentifyCulture.cultureName().Contains("zh"))
            {
                Page.Title = "Terms and Conditions | mashad carpet website";
                Page.MetaDescription = "mashad carpet website";
            }
            else
            {
                Page.Title = "قوانین و مقررات | وب سایت رسمی فرش مشهد";
                Page.MetaDescription = "وب سایت رسمی فرش مشهد.";
            }
        }
        public void FindPrivacy()
        {
            using(MashadCarpetEntities db=new MashadCarpetEntities())
            {
                //var n = (from u in db.Texts where u.TextID == 1016 select u).FirstOrDefault();
                var n = (from u in db.Texts.AsEnumerable()
                         where u.TextID == 1016
                         select new
                         {

                             u.TextName,
                             u.TextImage,
                             TextTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextTitle :
                     ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextTitle :
                     ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextTitle :
                     ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextTitle : u.TextTitle))),

                             TextDescription = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextDescription :
                     ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextDescription :
                     ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextDescription :
                     ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextDescription : u.TextDescription))),
                         }).FirstOrDefault();

                lblText.Text = n.TextDescription;
                PrivacyImage.ImageUrl = "/Uploads/TextImages/"+n.TextImage;
                lblTitle.Text = n.TextTitle;
            }
        }
    }
}