
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using MashadCarpet.Models;
using System.Web.Routing;
using MashadCarpet.Classes;
using GSD.Globalization;

namespace MashadCarpet
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            string JQueryVer = "1.7.1";
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            {
                Path = "~/Scripts/jquery-" + JQueryVer + ".min.js",
                DebugPath = "~/Scripts/jquery-" + JQueryVer + ".js",
                CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-" + JQueryVer + ".min.js",
                CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-" + JQueryVer + ".js",
                CdnSupportsSecureConnection = true
                //  LoadSuccessExpression = "window.jQuery"
            });
            RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            var persianCulture = new PersianCulture();
            Thread.CurrentThread.CurrentCulture = persianCulture;
            Thread.CurrentThread.CurrentUICulture = persianCulture;

            string urlSite = "http://www.mashadcarpet.com";

            if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("http://mashadcarpet.com"))
            {
                HttpContext.Current.Response.Status = "301 Moved Permanently"; Response.StatusCode = 301;
                HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace("http://mashadcarpet.com", "http://www.mashadcarpet.com"));
            }

            RedirectingPages(urlSite+"/about.aspx", urlSite+"/about-us");


            RedirectingPages(urlSite+"/ProductsSearch.aspx", urlSite+"/carpet-online-shopping");
            RedirectingPages(urlSite+"/Products.aspx", urlSite+"/carpet-online-shopping");
            RedirectingPages(urlSite+"/ShopList.aspx", urlSite+"/carpet-online-shopping");
            RedirectingPages(urlSite+"/ProductsList.aspx", urlSite+"/carpet-online-shopping");
            RedirectingPages(urlSite+"/ShopDetail.aspx", urlSite+"/carpet-online-shopping");
            RedirectingPages(urlSite+"/ProductDetail.aspx", urlSite+"/carpet-online-shopping");
            RedirectingPages(urlSite+"/ShopDetail.aspx", urlSite+"/carpet-online-shopping");
            
            RedirectingPages(urlSite+"/Social.aspx", urlSite+"/corporate-social-responsibility");
           
            RedirectingPages(urlSite+"/News.aspx", urlSite+"/Blog/all");
            RedirectingPages(urlSite+"/NewsShow.aspx", urlSite+"/Blog/all");

            RedirectingPages(urlSite+"/Contact.aspx", urlSite+"/Contact-us");
            RedirectingPages(urlSite+"/FormOffer.aspx", urlSite+"/Contact-us");
            RedirectingPages(urlSite+"/FAQ.aspx", urlSite+"/Contact-us");
            //diffrent from babak files
            RedirectingPages(urlSite+"/ContactIran.aspx", urlSite+"/Sales-Representatives");

            RedirectingPages(urlSite+"/StoreList.aspx", urlSite+"/Sales-Representatives");
            RedirectingPages(urlSite+"/ShopUserPanel.aspx", urlSite+"/login");
            RedirectingPages(urlSite+"/ShopOrder.aspx", urlSite+"/login");
            RedirectingPages(urlSite+"/ShopBasket.aspx", urlSite+"/login");
            RedirectingPages(urlSite+"/login.aspx", urlSite+"/login");
            RedirectingPages(urlSite+"/LostPass.aspx", urlSite+"/login");

            RedirectingPages(urlSite+"/ShopOrder.aspx", urlSite+"/login");
            RedirectingPages(urlSite+"/ShopBasket.aspx", urlSite+"/login");
            RedirectingPages(urlSite+"/login.aspx", urlSite+"/login");
   
        }
        public void RedirectingPages(string OldPage, string NewPage)
        {
            string LasURL = HttpContext.Current.Request.Url.ToString().ToLower();
            if (LasURL.Contains(OldPage.ToString().ToLower()))
            {
                HttpContext.Current.Response.Status = "301 Moved Permanently";
                Response.StatusCode = 301;

                HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace(LasURL, NewPage));
            }
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{file}.js");
            routes.Ignore("{file}.txt");
            routes.Ignore("{file}.aspx");
            routes.Ignore("{file}.axd");
            routes.Ignore("{file}.css");

            routes.MapPageRoute("ProductListRout", "carpet-online-shopping/{ProductGroupName}", "~/ProductList.aspx");
            //routes.MapPageRoute("ProductRout", "Product/{ProductColorID}", "~/Product.aspx");
            routes.MapPageRoute("ProductRout", "carpet-online-shopping/{ProductGroupName}/{ProductName}", "~/Product.aspx");

            routes.MapPageRoute("BlogRout", "Blog/{BlogGroupName}", "~/Blog.aspx");
            routes.MapPageRoute("SingleBlogRout", "blog/{BlogGroupName}/{BlogName}", "~/SingleBlog.aspx");
            routes.MapPageRoute("BillRout", "Bill", "~/BillShow.aspx");
            routes.MapPageRoute("factorRout", "factor/{OrderID}", "~/Factor.aspx");
            routes.MapPageRoute("TextRout", "Text/{TextName}", "~/Text.aspx");
            routes.MapPageRoute("SearchRout", "Search/{ProductGroupName}/{PageID}/{ColorName}/{SizeTitle}/{Text}", "~/SearchResult.aspx");
            routes.MapPageRoute("AboutRout", "About-us", "~/AboutUS.aspx");
            routes.MapPageRoute("contactRout", "Contact-us", "~/ContactUS.aspx");
            routes.MapPageRoute("StoresRout", "Sales-Representatives", "~/storeList.aspx");
            routes.MapPageRoute("privacyRout", "Terms-Conditions", "~/Privacy.aspx");
            routes.MapPageRoute("loginRout", "login", "~/login.aspx");

            routes.MapPageRoute("socialRout", "corporate-social-responsibility", "~/Social.aspx");
            routes.MapPageRoute("ProductGroupListRout", "carpet-online-shopping", "~/ProductGroupList.aspx");


        }
        protected void Session_Start(object sender, EventArgs e)
        {
            //using (MashadCarpetEntities db = new MashadCarpetEntities())
            //{
            //    var n = (from v in db.VisitCounter
            //             where v.VisitDate == DateTime.Today
            //             select v).FirstOrDefault();

            //    int l = (from row in db.VisitCounter.OrderByDescending(row => row.VisitID)
            //             select row).FirstOrDefault().VisitID;

            //    int lastDay = l;
            //    DateTime Yesterday = DateTime.Today.AddDays(-1);
            //    //int Yesterday=n.VisitID--;
            //    if (n == null)
            //    {
            //        //var mm = n.VisitID;
            //        //int visitID = mm - 1;
            //        var m = (from yv in db.VisitCounter
            //                 where yv.VisitID == lastDay
            //                 select yv.TotalVisit).FirstOrDefault();

            //        int TodayTotal = Convert.ToInt32(m++);


            //        FindRefrall(TodayTotal);
            //        //VisitCounter vc = new VisitCounter()
            //        //{
            //        //    VisitDate = DateTime.Today,
            //        //    VisitCount = 1,
            //        //    TotalVisit = TodayTotal++
            //        //};
            //        //db.VisitCounter.Add(vc);
            //        //db.SaveChanges();
            //    }
            //    else
            //    {
            //        n.VisitCount++;
            //        n.TotalVisit++;
            //        db.SaveChanges();
            //    }
            //}
        }

        


        public void FindRefrall(int TodayTotal)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                VisitCounter c = new VisitCounter();
                TodayTotal = TodayTotal + 1;

                c.ClientIP = Request.UserHostAddress;
                c.VisitDate = DateTime.Now;
                c.VisitCount = 1;
                c.TotalVisit = TodayTotal;
                if (Request.UrlReferrer != null)
                {

                    c.RefrallPage = Request.UrlReferrer.ToString();


                }
                else
                {
                    c.RefrallPage = "ورود مستقیم";

                }
                System.Web.HttpBrowserCapabilities browser = Request.Browser;
                c.Browser = browser.Type;
                c.OS = ReturnOS();

                db.VisitCounter.Add(c);
                db.SaveChanges();
            }
        }
        public string ReturnOS()
        {

            if (LoadReq("Windows 95") > 0 ||
                LoadReq("Windows_95") > 0 ||
                LoadReq("Win95") > 0)
            {
                return "Windows 95";
            }
            else if (LoadReq("Windows 98") > 0 ||
               LoadReq("Win98") > 0)
            {
                return "Windows 98";
            }
            else if (LoadReq("Windows NT 5.0") > 0 ||
             LoadReq("Windows 2000") > 0)
            {
                return "Windows 2000";
            }
            else if (LoadReq("Windows NT 5.1") > 0 ||
        LoadReq("Windows XP") > 0)
            {
                return "Windows xp";
            }
            else if (LoadReq("Windows NT 5.2") > 0)
            {
                return "Windows server 2003";
            }
            else if (LoadReq("Windows NT 6.0") > 0)
            {
                return "Windows Vista";
            }
            else if (LoadReq("Windows NT 6.1") > 0
                || LoadReq("Windows 7") > 0
                )
            {
                return "Windows 7";
            }
            else if (LoadReq("Windows 8") > 0 ||
                LoadReq("Windows NT 6.2") > 0)
            {
                return "Windows 8";
            }
            else if (LoadReq("Windows ME") > 0)
            {
                return "Windows ME";
            }

            else if (LoadReq("Mac_PowerPC") > 0 ||
    LoadReq("Macintosh") > 0)
            {
                return "Mac OS";
            }
            else if (LoadReq("Android") > 0)
            {
                return "Android";
            }
            else if (LoadReq("iPod") > 0)
            {
                return "iPod";
            }
            else if (LoadReq("iPhone") > 0)
            {
                return "iPhone";
            }
            else if (LoadReq("iPad") > 0)
            {
                return "iPad";
            }
            else return "Other";
        }
        public int LoadReq(string OsName)
        {
            int osIndex = Request.UserAgent.IndexOf(OsName);
            return osIndex;
        }
    }
}