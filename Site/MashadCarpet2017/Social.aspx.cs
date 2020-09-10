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
    public partial class Social : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                rptTabsBind();
                ChooseTitleAndDesc();
            }
        }
        public void ChooseTitleAndDesc()
        {
            if (IdentifyCulture.cultureName().Contains("fa"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "PageScriptfa",
               "$('.newfa').removeClass('fa-arrow-right'); $('.newfa').addClass('fa-arrow-left');", true);
                Page.Title = "مسئولیت اجتماعی | وب سایت رسمی فرش مشهد";
                Page.MetaDescription = "آسایشگاه فیاض بخش، بنیاد دانشگاهی فردوسی و مرکز درمانی رضا به عنوان نمونه ای از فعالیت های کارخانجات گروه صنعتی مشهد در خدمات اجتماعی و نیکوکاری است.";
            }
            else if (IdentifyCulture.cultureName().Contains("en"))
            {
                Page.Title = "carpet online shopping | mashad carpet website";
                Page.MetaDescription = "mashad carpet website";
            }
            else if (IdentifyCulture.cultureName().Contains("ru"))
            {
                Page.Title = "carpet online shopping | mashad carpet website";
                Page.MetaDescription = "mashad carpet website";
            }
            else if (IdentifyCulture.cultureName().Contains("zh"))
            {
                Page.Title = "carpet online shopping | mashad carpet website";
                Page.MetaDescription = "mashad carpet website";
            }
            else
            {
                Page.Title = "مسئولیت اجتماعی | وب سایت رسمی فرش مشهد";
                Page.MetaDescription = "آسایشگاه فیاض بخش، بنیاد دانشگاهی فردوسی و مرکز درمانی رضا به عنوان نمونه ای از فعالیت های کارخانجات گروه صنعتی مشهد در خدمات اجتماعی و نیکوکاری است.";
            }
        }
        public void rptTabsBind()
        {
            using(MashadCarpetEntities db=new MashadCarpetEntities())
            {
                var n = (from u in db.Texts.AsEnumerable()
                         where u.GroupID == 3 && u.IsDelete == false && u.TextID != 3
                         select new
                             {
                                 TextTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextTitle :
                           ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextTitle :
                           ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextTitle :
                           ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextTitle : u.TextTitle))),
                           u.TextID,
                           u.TextImage,
                                 TextDescription = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextDescription :
                              ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextDescription :
                              ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextDescription :
                              ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextDescription : u.TextDescription))),
                             }).ToList();
                rptTabs.DataSource = n;
                rptTabs.DataBind();

                rptTabPane.DataSource = n;
                rptTabPane.DataBind();

                var m = (from u in db.Texts.AsEnumerable()
                         where u.TextID == 3
                         select new
                             {
                                 u.TextImage,
                                 TextDescription = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextDescription :
                         ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextDescription :
                         ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextDescription :
                         ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextDescription : u.TextDescription))),
                             }).FirstOrDefault();
                lblDesc.Text = m.TextDescription;
                TextImage.ImageUrl = "/Uploads/TextImages/"+m.TextImage;
            }
        }
    }
}