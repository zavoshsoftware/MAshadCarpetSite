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
    public partial class ContactUS : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if (IdentifyCulture.cultureName().Contains("fa"))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "PageScriptfa",
                 "$('.newfa').removeClass('fa-arrow-right'); $('.newfa').addClass('fa-arrow-left');", true);
                }
                rptFactoryInfoBind();
                rptheadquartersBind();
                ChooseTitleAndDesc();
            }
        }
        public void ChooseTitleAndDesc()
        {
            if (IdentifyCulture.cultureName().Contains("fa"))
            {
                Page.Title = "تماس با ما | وب سایت رسمی فرش مشهد";
                Page.MetaDescription = "شما عزیزان می توانید از طریق فرم مربوطه و یا شماره تماس های موجود با ما در ارتباط باشید. آدرس کارخانه و دفتر مرکزی فرش مشهد قابل مشاهده است.";
            }
            else if (IdentifyCulture.cultureName().Contains("en"))
            {
                Page.Title = "contact us | mashad carpet website";
                Page.MetaDescription = "mashad carpet website";
            }
            else if (IdentifyCulture.cultureName().Contains("ru"))
            {
                Page.Title = "contact us | mashad carpet website";
                Page.MetaDescription = "mashad carpet website";
            }
            else if (IdentifyCulture.cultureName().Contains("zh"))
            {
                Page.Title = "contact us | mashad carpet website";
                Page.MetaDescription = "mashad carpet website";
            }
            else
            {
                Page.Title = "تماس با ما | وب سایت رسمی فرش مشهد";
                Page.MetaDescription = "شما عزیزان می توانید از طریق فرم مربوطه و یا شماره تماس های موجود با ما در ارتباط باشید. آدرس کارخانه و دفتر مرکزی فرش مشهد قابل مشاهده است.";

            }
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    ContactUSForm cu = new ContactUSForm();
                    cu.ContactusID = Guid.NewGuid();
                    cu.ContactusName = txtName.Text;
                    cu.ContactusEmail = txtEmail.Text;
                    cu.ContactusMsg = txtMsg.Text;

                    db.ContactUSForm.Add(cu);
                    db.SaveChanges();
                    EmptyForm();
                }
                pnlSuccess.Visible = true;
            }
           
        }

        public void EmptyForm()
        {
            txtMsg.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtName.Text = string.Empty;
        }

        public void rptFactoryInfoBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Texts.AsEnumerable()
                         where u.GroupID == 12 && u.IsDelete == false
                         select new
                         {

                             
                             TextTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextTitle :
                     ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextTitle :
                     ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextTitle :
                     ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextTitle : u.TextTitle))),

                             TextDescription = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextDescription :
                     ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextDescription :
                     ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextDescription :
                     ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextDescription : u.TextDescription))),
                         }).ToList();
                rptFactoryInfo.DataSource = n;
                rptFactoryInfo.DataBind();
            }
        }

        public void rptheadquartersBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Texts.AsEnumerable()
                         where u.GroupID == 13 && u.IsDelete == false
                         select new
                         {

                            
                             TextTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextTitle :
                     ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextTitle :
                     ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextTitle :
                     ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextTitle : u.TextTitle))),

                             TextDescription = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextDescription :
                     ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextDescription :
                     ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextDescription :
                     ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextDescription : u.TextDescription))),
                         }).ToList();
                rptheadquarters.DataSource = n;
                rptheadquarters.DataBind();
            }
        }
    }
}