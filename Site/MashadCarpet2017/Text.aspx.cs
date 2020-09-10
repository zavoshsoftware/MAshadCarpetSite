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
    public partial class Text : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FindPrivacy();
                LoadBannerImages();
            }
        }
        public void LoadBannerImages()
        {
             
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from a in db.ProductGroup.AsEnumerable()
                             where a.IsDelete == false  
                             && a.fk_ProductGroupID == null
                             select new
                             {
                                 a.imgSliderImage,
                                 a.ProductGroupID,
                                 ProductGroupTitle = (IdentifyCulture.cultureName().Contains("fa")) ? a.ProductGroupTitle :
                             ((IdentifyCulture.cultureName().Contains("en")) ? a.EN_ProductGroupTitle :
                             ((IdentifyCulture.cultureName().Contains("ru")) ? a.Rus_ProductGroupTitle :
                             ((IdentifyCulture.cultureName().Contains("zh")) ? a.China_ProductGroupTitle : a.ProductGroupTitle))),
                                 a.ProductGroupName
                             }).ToList();

                    rptbannerImages.DataSource = n;
                    rptbannerImages.DataBind();
                }
            
        }
        public void FindPrivacy()
        {
            if (Page.RouteData.Values["TextName"]!=null)
            {
                string TextName = Page.RouteData.Values["TextName"].ToString();
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.Texts.AsEnumerable()
                             where u.TextName == TextName
                             select new
                                 {
                                     u.TextName,
                                     u.TextImage,
                                     TextTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextTitle :
                             ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextTitle :
                             ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextTitle :
                             ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextTitle : u.TextTitle))),

                                     TextDescription =(IdentifyCulture.cultureName().Contains("fa")) ?u.TextDescription :
                             ((IdentifyCulture.cultureName().Contains("en")) ?u.EN_TextDescription:
                             ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextDescription :
                             ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextDescription : u.TextDescription))),
                                 }).FirstOrDefault();
                    if(n!=null)
                    {
                        lblText.Text = n.TextDescription;
                      
                        lblTitle.Text = n.TextTitle;
                    }
                 
                }
            }
         
        }
    }
}