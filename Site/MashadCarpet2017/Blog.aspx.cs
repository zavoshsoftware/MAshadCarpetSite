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
    public partial class Blog : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (IdentifyCulture.cultureName().Contains("fa"))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "PageScriptfa",
                 "$('.newfa').removeClass('fa-arrow-right'); $('.newfa').addClass('fa-arrow-left');", true);
                }
                rptBlogsBind();
                rptPageCountBind();
                rptBlogGroupBind();
                rptLatestBlogsBind();
                rptPopularBlogsBind();
                ChooseTitleAndDesc();
            }
        }
        public void ChooseTitleAndDesc()
        {
            if (Page.RouteData.Values["BlogGroupName"] != null)
            {
                string BlogGroupName = Page.RouteData.Values["BlogGroupName"].ToString();

                if (BlogGroupName != "all")
                {
                    using (MashadCarpetEntities db = new MashadCarpetEntities())
                    {
                        var n = (from a in db.BlogGroups
                                 where a.BlogGroupName == BlogGroupName
                                 select a).FirstOrDefault();

                        ltBlog.Text = n.BlogGroupTitle;

                        if (IdentifyCulture.cultureName().Contains("fa"))
                        {
                            Page.Title = n.BlogGroupTitle + " | وب سایت رسمی فرش مشهد";
                            Page.MetaDescription = "اخبار مجموعه فرش مشهد و نمایشگاه ها و همچنین اخبار قرعه کشی و جوایز فرش مشهد را به همراه مطالب خواندنی و آموزنده می توانید در این قسمت مشاهده کنید.";
                        }
                        else if (IdentifyCulture.cultureName().Contains("en"))
                        {
                            Page.Title = n.EN_BlogGroupTitle + " | mashad carpet website";
                            Page.MetaDescription = "mashad carpet website";
                        }
                        else if (IdentifyCulture.cultureName().Contains("ru"))
                        {
                            Page.Title = n.Rus_BlogGroupTitle + " | mashad carpet website";
                            Page.MetaDescription = "mashad carpet website";
                        }
                        else if (IdentifyCulture.cultureName().Contains("zh"))
                        {
                            Page.Title = n.China_BlogGroupTitle + " | mashad carpet website";
                            Page.MetaDescription = "mashad carpet website";
                        }
                        else
                        {
                            Page.Title = n.BlogGroupTitle + " | وب سایت رسمی فرش مشهد";
                            Page.MetaDescription = "اخبار مجموعه فرش مشهد و نمایشگاه ها و همچنین اخبار قرعه کشی و جوایز فرش مشهد را به همراه مطالب خواندنی و آموزنده می توانید در این قسمت مشاهده کنید.";
                        }
                    }
                }
                else
                {
                    ltBlog.Text = "مطالب و اخبار";

                    if (IdentifyCulture.cultureName().Contains("fa"))
                    {
                        Page.Title = "مطالب و اخبار | وب سایت رسمی فرش مشهد";
                        Page.MetaDescription = "اخبار مجموعه فرش مشهد و نمایشگاه ها و همچنین اخبار قرعه کشی و جوایز فرش مشهد را به همراه مطالب خواندنی و آموزنده می توانید در این قسمت مشاهده کنید.";
                    }
                    else if (IdentifyCulture.cultureName().Contains("en"))
                    {
                        Page.Title = "news and blog | mashad carpet website";
                        Page.MetaDescription = "mashad carpet website";
                    }
                    else if (IdentifyCulture.cultureName().Contains("ru"))
                    {
                        Page.Title = "news and blog | mashad carpet website";
                        Page.MetaDescription = "mashad carpet website";
                    }
                    else if (IdentifyCulture.cultureName().Contains("zh"))
                    {
                        Page.Title = "news and blog | mashad carpet website";
                        Page.MetaDescription = "mashad carpet website";
                    }
                    else
                    {
                        Page.Title = "مطالب و اخبار | وب سایت رسمی فرش مشهد";
                        Page.MetaDescription = "اخبار مجموعه فرش مشهد و نمایشگاه ها و همچنین اخبار قرعه کشی و جوایز فرش مشهد را به همراه مطالب خواندنی و آموزنده می توانید در این قسمت مشاهده کنید.";
                    }
                }

            }
        }
        public int ReturnPageID()
        {
            if (Request.QueryString["PageID"] != null)
            {
                int pID = Convert.ToInt32(Request.QueryString["PageID"].ToString());
                return pID;
            }
            else
                return 1;
        }
        public void rptBlogsBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                if (Page.RouteData.Values["BlogGroupName"] != null)
                {
                    string BlogGroupName = Page.RouteData.Values["BlogGroupName"].ToString();
                    if (BlogGroupName != "all")
                    {

                        int pID = ReturnPageID();
                        var n = (from u in db.Blogs.AsEnumerable()
                                 join i in db.BlogGroups.AsEnumerable() on u.fk_BlogGroupID equals i.BlogGroupID

                                 where u.IsDelete == false && i.IsDelete == false && i.BlogGroupName == BlogGroupName
                                 select new
                                     {
                                         u.BlogImage,
                                         u.SubmitDate,
                                         u.VisitCounts,
                                         BlogTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.BlogTitle :
                           ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_BlogTitle :
                           ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_BlogTitle :
                           ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_BlogTitle : u.BlogTitle))),
                                         BlogText = (IdentifyCulture.cultureName().Contains("fa")) ? (u.BlogSummery) :
                                                                    ((IdentifyCulture.cultureName().Contains("en")) ? (u.EN_BlogSummery) :
                                                                    ((IdentifyCulture.cultureName().Contains("ru")) ? (u.Rus_BlogSummery) :
                                                                    ((IdentifyCulture.cultureName().Contains("zh")) ? (u.China_BlogSummery) : (u.BlogSummery)))),
                                         BlogGroupTitle = (IdentifyCulture.cultureName().Contains("fa")) ? i.BlogGroupTitle :
                             ((IdentifyCulture.cultureName().Contains("en")) ? i.EN_BlogGroupTitle :
                             ((IdentifyCulture.cultureName().Contains("ru")) ? i.Rus_BlogGroupTitle :
                             ((IdentifyCulture.cultureName().Contains("zh")) ? i.China_BlogGroupTitle : i.BlogGroupTitle))),
                                         u.BlogName,
                                         i.BlogGroupName
                                     }).ToList().Skip((pID - 1) * 9).Take(9);
                        rptBlogs.DataSource = n;
                        rptBlogs.DataBind();
                    }
                    else
                    {
                        int pID = ReturnPageID();
                        var n = (from u in db.Blogs.AsEnumerable()
                                 join i in db.BlogGroups.AsEnumerable() on u.fk_BlogGroupID equals i.BlogGroupID
                                 where u.IsDelete == false && i.IsDelete == false
                                 select new
                                 {

                                     u.BlogImage,
                                     u.SubmitDate,
                                     i.BlogGroupName,
                                     u.VisitCounts,
                                     BlogTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.BlogTitle :
                           ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_BlogTitle :
                           ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_BlogTitle :
                           ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_BlogTitle : u.BlogTitle))),

                                     BlogText = (IdentifyCulture.cultureName().Contains("fa")) ? (u.BlogSummery) :
                             ((IdentifyCulture.cultureName().Contains("en")) ? (u.EN_BlogSummery) :
                             ((IdentifyCulture.cultureName().Contains("ru")) ? (u.Rus_BlogSummery) :
                             ((IdentifyCulture.cultureName().Contains("zh")) ? (u.China_BlogSummery) : (u.BlogSummery)))),

                                     BlogGroupTitle = (IdentifyCulture.cultureName().Contains("fa")) ? i.BlogGroupTitle :
                                      ((IdentifyCulture.cultureName().Contains("en")) ? i.EN_BlogGroupTitle :
                                      ((IdentifyCulture.cultureName().Contains("ru")) ? i.Rus_BlogGroupTitle :
                                      ((IdentifyCulture.cultureName().Contains("zh")) ? i.China_BlogGroupTitle : i.BlogGroupTitle))),

                                     u.BlogName
                                 }).ToList().Skip((pID - 1) * 9).Take(9);
                        rptBlogs.DataSource = n;
                        rptBlogs.DataBind();

                    }
                }
            }
        }
        public void rptPageCountBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Blogs
                         join i in db.BlogGroups on u.fk_BlogGroupID equals i.BlogGroupID
                         join p in db.Users on u.fk_UserID equals p.UserID
                         select u).ToList();
                int ocjCount = n.Count();
                if ((ocjCount % 9) >= 1)
                {
                    int PageCount = Convert.ToInt32(Math.Round(Convert.ToDecimal(ocjCount / 9)));
                    List<int> p = new List<int>();

                    for (int i = 1; i <= PageCount + 1; i++)
                    {
                        p.Add(i);
                    }
                    var m = (from a in p
                             select new
                             {
                                 pageid = a.ToString()
                             }).ToList();
                    rptPageNum.DataSource = m;
                    rptPageNum.DataBind();
                }
                else
                {
                    int PageCount = Convert.ToInt32(Math.Round(Convert.ToDecimal(ocjCount / 9)));
                    List<int> p = new List<int>();

                    for (int i = 1; i < PageCount + 1; i++)
                    {
                        p.Add(i);
                    }
                    var m = (from a in p
                             select new
                             {
                                 pageid = a.ToString()
                             }).ToList();
                    rptPageNum.DataSource = m;
                    rptPageNum.DataBind();
                }
            }
        }

        protected void rptPageNum_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int pID = ReturnPageID();

                Label lblPageNum = (Label)e.Item.FindControl("lblPageNum");

                HiddenField hfPageID = (HiddenField)e.Item.FindControl("hfPageID");

                int PageID = Convert.ToInt32(hfPageID.Value);

                if (PageID == pID)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "PageScript",
                        "AddCurrent('#" + PageID + "')", true);
                    //lblPageNum.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBC6B5");
                    //lblPageNum.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                    //ScriptManager.RegisterStartupScript(this, GetType(), "PageScript",
                    // "AddCurrent('#c')", true);
                }
            }
        }
        protected void rptPageNum_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string pageid = e.CommandArgument.ToString();
            if (Page.RouteData.Values["BlogGroupName"] != null)
            {
                string BlogGroupName = Page.RouteData.Values["BlogGroupName"].ToString();
                Response.Redirect("/Blog/" + pageid + "/" + BlogGroupName);
            }
            else
            {

                Response.Redirect("/Blog/" + pageid + "/all");
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
                             join aa in db.BlogGroups
                             on u.fk_BlogGroupID equals aa.BlogGroupID
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

        public void rptLatestBlogsBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Blogs.AsEnumerable()
                         join aa in db.BlogGroups on u.fk_BlogGroupID equals aa.BlogGroupID
                         where u.IsDelete == false && aa.IsDelete == false
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
    }
}