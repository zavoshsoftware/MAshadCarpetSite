using MashadCarpet.Classes;
using MashadCarpet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MashadCarpet
{
    public partial class ProductGroupList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                ChooseTitleAndDesc();
                rptProductsBind();
                callInfo();
            }
        }
        public void callInfo()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from a in db.Texts
                         where a.TextID == 1067
                         select a).FirstOrDefault();

                lblCall.Text = (IdentifyCulture.cultureName().Contains("fa")) ? n.TextDescription :
                        ((IdentifyCulture.cultureName().Contains("en")) ? n.EN_TextDescription :
                        ((IdentifyCulture.cultureName().Contains("ru")) ? n.Rus_TextDescription :
                        ((IdentifyCulture.cultureName().Contains("zh")) ? n.China_TextDescription : n.TextDescription)));

                var n2 = (from a in db.Texts
                         where a.TextID == 1071
                         select a).FirstOrDefault();

                ltPageTitle.Text = (IdentifyCulture.cultureName().Contains("fa")) ? n2.TextTitle :
                            ((IdentifyCulture.cultureName().Contains("en")) ? n2.EN_TextTitle :
                            ((IdentifyCulture.cultureName().Contains("ru")) ? n2.Rus_TextTitle :
                            ((IdentifyCulture.cultureName().Contains("zh")) ? n2.China_TextDescription : n2.TextTitle)));

                ltPageDesc.Text = (IdentifyCulture.cultureName().Contains("fa")) ? n2.TextDescription :
                        ((IdentifyCulture.cultureName().Contains("en")) ? n2.EN_TextDescription :
                        ((IdentifyCulture.cultureName().Contains("ru")) ? n2.Rus_TextDescription :
                        ((IdentifyCulture.cultureName().Contains("zh")) ? n2.China_TextDescription : n.TextDescription)));

            
            }
        }
        public void ChooseTitleAndDesc()
        {
            if (IdentifyCulture.cultureName().Contains("fa"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "PageScriptfa",
               "$('.newfa').removeClass('fa-arrow-right'); $('.newfa').addClass('fa-arrow-left');", true);
                Page.Title = "فروش اینترنتی فرش ماشینی | وب سایت رسمی فرش مشهد";
                Page.MetaDescription = "خرید فرش در فروشگاه آنلاین فرش مشهد، ارائه دهنده انواع فرش های ماشینی با ارزان ترین قیمت و ارسال رایگان به سراسر کشور. ";
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
                Page.Title = "فروشگاه اینترنتی فرش | وب سایت رسمی فرش مشهد";
                Page.MetaDescription = "خرید فرش در فروشگاه آنلاین فرش مشهد، ارائه دهنده انواع فرش های ماشینی با ارزان ترین قیمت و ارسال رایگان به سراسر کشور. ";
            }
        }
        public void rptProductsBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                if (!IdentifyCulture.cultureName().Contains("fa"))
                {
                    var n = (from a in db.ProductGroup.AsEnumerable()
                             where a.fk_ProductGroupID == null && a.IsDelete == false&&a.IsAlienCulture==true
                             orderby a.Priority
                             select new

                             {
                                 a.ProductGroupName,
                                 a.ProductGroupImage,
                                 ProductGroupTitle = (IdentifyCulture.cultureName().Contains("fa")) ? a.ProductGroupTitle :
                                 ((IdentifyCulture.cultureName().Contains("en")) ? a.EN_ProductGroupTitle :
                                 ((IdentifyCulture.cultureName().Contains("ru")) ? a.Rus_ProductGroupTitle :
                                 ((IdentifyCulture.cultureName().Contains("zh")) ? a.China_ProductGroupTitle : a.ProductGroupTitle))),
                             }).ToList();

                    rptProductgroups.DataSource = n;
                    rptProductgroups.DataBind();
                }
                else
                {
                    var n = (from a in db.ProductGroup.AsEnumerable()
                             where a.fk_ProductGroupID == null && a.IsDelete == false && a.IsAlienCulture != true
                             orderby a.Priority
                             select new

                             {
                                 a.ProductGroupName,
                                 a.ProductGroupImage,
                                 ProductGroupTitle = a.ProductGroupTitle,
                             }).ToList();

                    rptProductgroups.DataSource = n;
                    rptProductgroups.DataBind();
                }
            }
        }
    }
}