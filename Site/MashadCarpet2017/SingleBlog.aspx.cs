using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MashadCarpet.Models;
using MashadCarpet.Classes;
using GSD.Globalization;
using System.Threading;

namespace MashadCarpet
{
    public partial class SingleBlog : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IdentifyCulture.cultureName().Contains("fa"))
            {
                var persianCulture = new PersianCulture();
                Thread.CurrentThread.CurrentCulture = persianCulture;
                Thread.CurrentThread.CurrentUICulture = persianCulture;
                ScriptManager.RegisterStartupScript(this, GetType(), "PageScriptfa",
                 "$('.newfa').removeClass('fa-arrow-right'); $('.newfa').addClass('fa-arrow-left');", true);

            }


            if (!Page.IsPostBack)
            {
                FindBlog();
                rptPopularBlogsBind();
                rptLatestBlogsBind();
                rptBlogGroupBind();
                rptRelatedBlogsBind();
                ChooseTitleAndDesc();
            }
        }
        public void ChooseTitleAndDesc()
        {
            if (Page.RouteData.Values["BlogName"] != null)
            {
                string BlogName = Page.RouteData.Values["BlogName"].ToString().ToLower();

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.Blogs.AsEnumerable()
                             where u.BlogName.ToLower() == BlogName
                             select new
                             {
                                 u.BlogTitle,
                                 u.EN_BlogTitle
                                 ,
                                 u.China_BlogTitle,
                                 u.Rus_BlogTitle
                                 ,
                                 BlogText = (IdentifyCulture.cultureName().Contains("fa")) ? (u.BlogText.Length > 200 ? u.BlogText.Substring(0, 200) : u.BlogText) :
                                ((IdentifyCulture.cultureName().Contains("en")) ? (u.EN_BlogText.Length > 200 ? u.EN_BlogText.Substring(0, 200) : u.EN_BlogText) :
                                ((IdentifyCulture.cultureName().Contains("ru")) ? (u.Rus_BlogText.Length > 200 ? u.Rus_BlogText.Substring(0, 200) : u.Rus_BlogText) :
                                ((IdentifyCulture.cultureName().Contains("zh")) ? (u.China_BlogText.Length > 200 ? u.China_BlogText.Substring(0, 200) : u.China_BlogText) : (u.BlogText.Length > 200 ? u.BlogText.Substring(0, 200) : u.BlogText)))),
                             }).FirstOrDefault();


                    if (IdentifyCulture.cultureName().Contains("fa"))
                    {
                        Page.Title = n.BlogTitle + " | وب سایت رسمی فرش مشهد";

                    }
                    else if (IdentifyCulture.cultureName().Contains("en"))
                    {
                        Page.Title = n.EN_BlogTitle + " | mashad carpet website";


                    }
                    else if (IdentifyCulture.cultureName().Contains("ru"))
                    {
                        Page.Title = n.Rus_BlogTitle + " | mashad carpet website";

                    }
                    else if (IdentifyCulture.cultureName().Contains("zh"))
                    {
                        Page.Title = n.China_BlogTitle + " | mashad carpet website";
                    }
                    else
                    {
                        Page.Title = n.BlogTitle + " | وب سایت رسمی فرش مشهد";
                    }
                    Page.MetaDescription = n.BlogText;
                }
            }
        }
        public void FindBlog()
        {
            if (Page.RouteData.Values["BlogName"] != null && Page.RouteData.Values["BlogGroupName"] != null)
            {
                string BlogName = Page.RouteData.Values["BlogName"].ToString().ToLower();
                string BlogGroupName = Page.RouteData.Values["BlogGroupName"].ToString();

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var o = (from i in db.BlogGroups.AsEnumerable()
                             where i.BlogGroupName.ToLower() == BlogGroupName
                             select new
                             {
                                 BlogGroupTitle = (IdentifyCulture.cultureName().Contains("fa")) ? i.BlogGroupTitle :
                        ((IdentifyCulture.cultureName().Contains("en")) ? i.EN_BlogGroupTitle :
                        ((IdentifyCulture.cultureName().Contains("ru")) ? i.Rus_BlogGroupTitle :
                        ((IdentifyCulture.cultureName().Contains("zh")) ? i.China_BlogGroupTitle : i.BlogGroupTitle))),

                             }).FirstOrDefault();

                    var n = (from u in db.Blogs.AsEnumerable()
                             where u.BlogName.ToLower() == BlogName
                             select new
                             {
                                 BlogTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.BlogTitle :
                                     ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_BlogTitle :
                                     ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_BlogTitle :
                                     ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_BlogTitle : u.BlogTitle))),

                                 BlogText = (IdentifyCulture.cultureName().Contains("fa")) ? (u.BlogText) :
                     ((IdentifyCulture.cultureName().Contains("en")) ? (u.EN_BlogText) :
                     ((IdentifyCulture.cultureName().Contains("ru")) ? (u.Rus_BlogText) :
                     ((IdentifyCulture.cultureName().Contains("zh")) ? (u.China_BlogText) : (u.BlogText)))),


                                 u.BlogName,
                                 u.BlogImage,
                                 u.VisitCounts,
                                 u.SubmitDate,

                             }).FirstOrDefault();
                    if(n!=null)
                    { 
                    //lblBlogGroupTitle.Text = n.BlogGroupTitle;
                    hlBlogGroupTitle.Text = o.BlogGroupTitle;
                    hlBlogGroupTitle.NavigateUrl = "/Blog/" + BlogGroupName + "?PageID=1";
                    lblBlogTitle.Text = n.BlogTitle;
                    BlogImg.ImageUrl = "/Uploads/Blogs/" + n.BlogImage;
                    BlogImg.AlternateText = n.BlogTitle;
                    lblDate.Text = string.Format("{0:d}", n.SubmitDate);
                    lblTitle.Text = n.BlogTitle;
                    lblBlogText.Text = n.BlogText;
                    }

                    var m = (from u in db.Blogs where u.BlogName.ToLower() == BlogName select u).FirstOrDefault();
                    if (m != null) { 
                    m.VisitCounts = m.VisitCounts + 1;

                    lblVisitCount.Text = m.VisitCounts.ToString();
                    db.SaveChanges();
                    }
                }
            }
        }
        public void rptLatestBlogsBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Blogs.AsEnumerable()
                         join aa in db.BlogGroups on u.fk_BlogGroupID equals aa.BlogGroupID
                         where u.IsDelete == false
                         orderby u.SubmitDate descending
                         select new
                         {
                             aa.BlogGroupName,
                             u.BlogImage,
                             u.SubmitDate,
                             //u.BlogTitle,
                             //BlogText = (u.BlogText.Length > 100) ? u.BlogText.Substring(0, 100) : u.BlogText,
                             BlogTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.BlogTitle :
                                    ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_BlogTitle :
                                    ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_BlogTitle :
                                    ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_BlogTitle : u.BlogTitle))),
                             BlogText = (IdentifyCulture.cultureName().Contains("fa")) ? (u.BlogSummery) :
                             ((IdentifyCulture.cultureName().Contains("en")) ? (u.EN_BlogSummery) :
                             ((IdentifyCulture.cultureName().Contains("ru")) ? (u.Rus_BlogSummery) :
                             ((IdentifyCulture.cultureName().Contains("zh")) ? (u.China_BlogSummery) : (u.BlogSummery)))),
                             u.BlogName
                         }).ToList().Take(3);
                rptLatestBlogs.DataSource = n;
                rptLatestBlogs.DataBind();
            }
        }

        public void rptPopularBlogsBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Blogs.AsEnumerable()
                         join aa in db.BlogGroups on u.fk_BlogGroupID equals aa.BlogGroupID
                         where u.IsDelete == false
                         orderby u.VisitCounts descending
                         select new
                         {
                             aa.BlogGroupName,
                             u.BlogImage,
                             u.SubmitDate,
                             //u.BlogTitle,
                             BlogTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.BlogTitle :
                                  ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_BlogTitle :
                                  ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_BlogTitle :
                                  ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_BlogTitle : u.BlogTitle))),
                             u.BlogName

                         }).ToList().Take(3);
                rptPopularBlogs.DataSource = n;
                rptPopularBlogs.DataBind();
            }
        }
        public void rptBlogGroupBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.BlogGroups.AsEnumerable()
                         where u.IsDelete == false
                         select new
                         {
                             u.BlogGroupName,
                             BlogGroupTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.BlogGroupTitle :
                             ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_BlogGroupTitle :
                             ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_BlogGroupTitle :
                             ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_BlogGroupTitle : u.BlogGroupTitle))),
                             u.BlogGroupID
                         }).ToList();
                rptBlogGroup.DataSource = n;
                rptBlogGroup.DataBind();
            }
        }

        protected void rptBlogGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hfBlogGroupID = (HiddenField)e.Item.FindControl("hfBlogGroupID");
                Guid BlogGroupID = new Guid(hfBlogGroupID.Value.ToString());
                Repeater rptBlogs = (Repeater)e.Item.FindControl("rptBlogs");

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.Blogs.AsEnumerable()
                             join aa in db.BlogGroups on u.fk_BlogGroupID equals aa.BlogGroupID
                             where u.fk_BlogGroupID == BlogGroupID && u.IsDelete == false
                             select new
                             {
                                 BlogTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.BlogTitle :
                                     ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_BlogTitle :
                                     ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_BlogTitle :
                                     ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_BlogTitle : u.BlogTitle))),
                                 u.BlogName,
                                 aa.BlogGroupName
                             }).ToList();
                    rptBlogs.DataSource = n;
                    rptBlogs.DataBind();
                }
            }
        }

        public void rptRelatedBlogsBind()
        {
            if (Page.RouteData.Values["BlogName"] != null)
            {
                string BlogGroupName = Page.RouteData.Values["BlogGroupName"].ToString();

                string BlogName = Page.RouteData.Values["BlogName"].ToString().ToLower();
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    //var m = (from u in db.Blogs.AsEnumerable() where u.BlogName == BlogName select u).FirstOrDefault();
                    var n = (from u in db.Blogs.AsEnumerable()
                             join aa in db.BlogGroups
                             on u.fk_BlogGroupID equals aa.BlogGroupID
                             where u.IsDelete == false && u.BlogName.ToLower() != BlogName.ToLower() &&
                             aa.BlogGroupName == BlogGroupName
                             select new
                                 {
                                     u.BlogImage,
                                     u.SubmitDate,
                                     BlogTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.BlogTitle :
                                 ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_BlogTitle :
                                 ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_BlogTitle :
                                 ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_BlogTitle : u.BlogTitle))),
                                     BlogText = (IdentifyCulture.cultureName().Contains("fa")) ? (u.BlogSummery) :
                                      ((IdentifyCulture.cultureName().Contains("en")) ? (u.EN_BlogSummery) :
                                      ((IdentifyCulture.cultureName().Contains("ru")) ? (u.Rus_BlogSummery) :
                                      ((IdentifyCulture.cultureName().Contains("zh")) ? (u.China_BlogSummery) : (u.BlogSummery)))),

                                     u.BlogName,
                                     aa.BlogGroupName
                                 }).ToList();

                    rptRelatedBlogs.DataSource = n;
                    rptRelatedBlogs.DataBind();
                }
            }
        }
    }
}