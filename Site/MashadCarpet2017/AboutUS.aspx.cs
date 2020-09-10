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
    public partial class AboutUS : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
                if (IdentifyCulture.cultureName().Contains("fa"))
                {
                  
                    imgSh.ImageUrl = "~/images/Shoar.png";

 
                }
                else
                {
                  
                    imgSh.ImageUrl = "~/images/Shor-Eng.png";

                }
                rptUnitsBind();
                FindMashadText();
                rptAccordionBind();
                rptService1Bind();
                //rptService2Bind();
                rpHonor1Bind();
                rpHonor2Bind();
                rptPortfolioBind();
                rptSlideBind();
                ChooseTitleAndDesc();
            }
        }
        public void ChooseTitleAndDesc()
        {
            if (IdentifyCulture.cultureName().Contains("fa"))
            {
                Page.Title = "درباره ما | وب سایت رسمی فرش مشهد";
                Page.MetaDescription = "شرکت فرش مشهد بزرگترین عضو گروه صنعتی مشهد و یکی از بزرگترین تولید کنندگان فرش ماشینی در ایران میباشد. این شرکت در سال ۱۳۵۶ در مشهد آغاز به کار کرد.";
            }
            else if (IdentifyCulture.cultureName().Contains("en"))
            {
                Page.Title = "about us | mashad carpet website";
                Page.MetaDescription = "mashad carpet website";
            }
            else if (IdentifyCulture.cultureName().Contains("ru"))
            {
                Page.Title = "about us | mashad carpet website";
                Page.MetaDescription = "mashad carpet website";
            }
            else if (IdentifyCulture.cultureName().Contains("zh"))
            {
                Page.Title = "about us | mashad carpet website";
                Page.MetaDescription = "mashad carpet website";
            }
            else
            {
                Page.Title = "درباره ما | وب سایت رسمی فرش مشهد";
                Page.MetaDescription = "شرکت فرش مشهد بزرگترین عضو گروه صنعتی مشهد و یکی از بزرگترین تولید کنندگان فرش ماشینی در ایران میباشد. این شرکت در سال ۱۳۵۶ در مشهد آغاز به کار کرد.";
            }
        }
        public void rptUnitsBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Texts.AsEnumerable()
                         where u.GroupID == 5 && u.IsDelete == false
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
                         }).ToList();
                rptUnits.DataSource = n;
                rptUnits.DataBind();
            }
        }

        public void FindMashadText()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Texts.AsEnumerable()
                         where u.GroupID == 5 && u.IsDelete == false
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
          //
          //      lblMashadDesc.Text = n.TextDescription;
          //      lblMashadTitle.Text = n.TextTitle;
            }
        }

        public void rptAccordionBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var nn = (from u in db.Texts.AsEnumerable()
                          where u.GroupID == 6 && u.IsDelete == false
                          select new
                          {

                              u.TextName,
                              u.TextID,
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
             //   lblAccordTitle.Text = nn.TextTitle;
             //   lblAccordDesc.Text = nn.TextDescription;
                var t = (from u in db.Texts.AsEnumerable()
                         where u.GroupID == 20 && u.IsDelete == false
                         select u).Count();
                var n = (from u in db.Texts.AsEnumerable()
                         where u.GroupID == 20 && u.IsDelete == false
                         select new
                         {

                             u.TextName,
                             u.TextID,
                             u.TextImage,
                             TextTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextTitle :
                     ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextTitle :
                     ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextTitle :
                     ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextTitle : u.TextTitle))),

                             TextDescription = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextDescription :
                     ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextDescription :
                     ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextDescription :
                     ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextDescription : u.TextDescription))),
                         }).ToList();
                  rptTextTabGroups.DataSource = n;
                  rptTextTabGroups.DataBind();

                  rptTextBody.DataSource = n;
                  rptTextBody.DataBind();
            }
        }

        public void rptService1Bind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Texts.AsEnumerable()
                         where u.GroupID == 8 && u.IsDelete == false
                         select new
                         {

                             u.TextName,
                             u.TextID,
                             u.TextImage,
                             TextTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextTitle :
                     ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextTitle :
                     ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextTitle :
                     ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextTitle : u.TextTitle))),

                             TextDescription = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextDescription :
                     ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextDescription :
                     ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextDescription :
                     ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextDescription : u.TextDescription))),
                         }).ToList();
                rptService1.DataSource = n;
                rptService1.DataBind();
            }
        }
        //public void rptService2Bind()
        //{
        //    using (MashadCarpetEntities db = new MashadCarpetEntities())
        //    {
        //        var n = (from u in db.Texts.AsEnumerable()
        //                 where u.GroupID == 8 && u.IsDelete == false
        //                 select new
        //                 {

        //                     u.TextName,
        //                     u.TextID,
        //                     u.TextImage,
        //                     TextTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextTitle :
        //             ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextTitle :
        //             ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextTitle :
        //             ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextTitle : u.TextTitle))),

        //                     TextDescription = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextDescription :
        //             ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextDescription :
        //             ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextDescription :
        //             ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextDescription : u.TextDescription))),
        //                 }).ToList();
        //        rptService2.DataSource = n;
        //        rptService2.DataBind();
        //    }
        //}

        public void rpHonor1Bind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Texts.AsEnumerable()
                         where u.GroupID == 9 && u.IsDelete == false
                         select new
                         {

                             u.TextName,
                             u.TextID,
                             u.TextImage,
                             TextTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextTitle :
                     ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextTitle :
                     ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextTitle :
                     ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextTitle : u.TextTitle))),

                             u.TextDescription
                         }).ToList().Take(3);
              // rpHonor1.DataSource = n;
              // rpHonor1.DataBind();
            }
        }

        public void rpHonor2Bind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Texts.AsEnumerable()
                         where u.GroupID == 9 && u.IsDelete == false
                         select new
                         {

                             u.TextName,
                             u.TextID,
                             u.TextImage,
                             TextTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextTitle :
                     ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextTitle :
                     ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextTitle :
                     ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextTitle : u.TextTitle))),

                             u.TextDescription
                         }).ToList().Skip(3).Take(3);
              //  rpHonor2.DataSource = n;
              //  rpHonor2.DataBind();
            }
        }
        public void rptPortfolioBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Texts.AsEnumerable()
                         where u.GroupID == 10 && u.IsDelete == false
                         select new
                         {

                             u.TextName,
                             u.TextID,
                             u.TextImage,
                             TextTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextTitle :
                     ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextTitle :
                     ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextTitle :
                     ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextTitle : u.TextTitle))),

                             u.TextDescription
                         }).ToList();
                rptPortfolio.DataSource = n;
                rptPortfolio.DataBind();
            }
        }

        public void rptSlideBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Texts.AsEnumerable()
                         where u.GroupID == 1 && u.IsDelete == false
                         select new
                         {

                            
                             TextDescription = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextDescription :
                   ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextDescription :
                   ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextDescription :
                   ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextDescription : u.TextDescription))),
                         }).ToList();
                rptSlide.DataSource = n;
                rptSlide.DataBind();
            }
        }
    }
}