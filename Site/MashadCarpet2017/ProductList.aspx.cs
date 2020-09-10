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
    public partial class ProductList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ProductBind();
                rptCategoryGroupBind();
                rptColorBind();
                rprSizeBind();
                LoadBannerImages();
                ChooseTitleAndDesc();
            }
        }
        public void ChooseTitleAndDesc()
        {
            string ProductGroupName = Page.RouteData.Values["ProductGroupName"].ToString();
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from a in db.ProductGroup.AsEnumerable()
                         where a.IsDelete == false && a.ProductGroupName == ProductGroupName
                         select new
                         {
                             ProductGroupTitle = (IdentifyCulture.cultureName().Contains("fa")) ? a.ProductGroupTitle :
                                 ((IdentifyCulture.cultureName().Contains("en")) ? a.EN_ProductGroupTitle :
                                 ((IdentifyCulture.cultureName().Contains("ru")) ? a.Rus_ProductGroupTitle :
                                 ((IdentifyCulture.cultureName().Contains("zh")) ? a.China_ProductGroupTitle : a.ProductGroupTitle))),
                             desc = (IdentifyCulture.cultureName().Contains("fa")) ? a.ProductGroupDesc :
                                 ((IdentifyCulture.cultureName().Contains("en")) ? a.EN_ProductGroupDesc :
                                 ((IdentifyCulture.cultureName().Contains("ru")) ? a.Rus_ProductGroupDesc :
                                 ((IdentifyCulture.cultureName().Contains("zh")) ? a.China_ProductGroupDesc : a.ProductGroupDesc))),

                         }).FirstOrDefault();
                if (string.IsNullOrEmpty(n.desc))
                {
                    pnlDesc.Visible = false;

                }
                else
                {
                    pnlDesc.Visible = true;
                    ltPageDesc.Text = n.desc;

                }
                if (IdentifyCulture.cultureName().Contains("fa"))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "PageScriptfa",
                   "$('.newfa').removeClass('fa-arrow-right'); $('.newfa').addClass('fa-arrow-left');", true);
                    Page.Title = "فرش های " + n.ProductGroupTitle + " | وب سایت رسمی فرش مشهد";
                    Page.MetaDescription = "خرید فرش " + n.ProductGroupTitle + " در فروشگاه آنلاین فرش مشهد، ارائه دهنده بهترین فرش های ماشینی با ارزانترین قیمت و ارسال رایگان به سراسر کشور. ";
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
                    Page.Title = "فرش های " + n.ProductGroupTitle + " | وب سایت رسمی فرش مشهد";
                    Page.MetaDescription = "خرید فرش " + n.ProductGroupTitle + " در فروشگاه آنلاین فرش مشهد، ارائه دهنده بهترین فرش های ماشینی با ارزانترین قیمت و ارسال رایگان به سراسر کشور. ";
                }


            }

        }
        public void LoadBannerImages()
        {
            if (Page.RouteData.Values["ProductGroupName"].ToString() != null)
            {
                string ProductGroupName = Page.RouteData.Values["ProductGroupName"].ToString();
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from a in db.ProductGroup.AsEnumerable()
                             where a.IsDelete == false && a.ProductGroupName == ProductGroupName
                             && a.fk_ProductGroupID == null
                             orderby a.Priority
                             select new
                             {
                                 a.imgSliderImage,
                                 a.ProductGroupID,
                                 ProductGroupTitle = (IdentifyCulture.cultureName().Contains("fa")) ? a.ProductGroupTitle :
                             ((IdentifyCulture.cultureName().Contains("en")) ? a.EN_ProductGroupTitle :
                             ((IdentifyCulture.cultureName().Contains("ru")) ? a.Rus_ProductGroupTitle :
                             ((IdentifyCulture.cultureName().Contains("zh")) ? a.China_ProductGroupTitle : a.ProductGroupTitle))),
                                 a.ProductGroupName
                             }).ToList().Take(1);

                    rptbannerImages.DataSource = n;
                    rptbannerImages.DataBind();
                }
            }
        }
        public void ProductBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                if (Page.RouteData.Values["ProductGroupName"].ToString() != null)
                {
                    string ProductGroupName = Page.RouteData.Values["ProductGroupName"].ToString();
                    if (IdentifyCulture.cultureName().Contains("fa"))
                    {

                        var n = (from a in db.ProductGroup
                                 where a.ProductGroupName == ProductGroupName && (a.IsAlienCulture == false || a.IsAlienCulture == null)
                                 && a.IsDelete == false
                                 select a).FirstOrDefault();

                        if (n != null)
                        {

                            progroupList.Text = (IdentifyCulture.cultureName().Contains("fa")) ? n.ProductGroupTitle :
                                 ((IdentifyCulture.cultureName().Contains("en")) ? n.EN_ProductGroupTitle :
                                 ((IdentifyCulture.cultureName().Contains("ru")) ? n.Rus_ProductGroupTitle :
                                 ((IdentifyCulture.cultureName().Contains("zh")) ? n.China_ProductGroupTitle : n.ProductGroupTitle)));

                            var nm = (from a in db.ProductColors.AsEnumerable()
                                      join aa in db.Products.AsEnumerable()
                                      on a.fk_ProductID equals aa.ProductID
                                      join aaa in db.ProductGroup
                                      on aa.fk_ProductGroupID equals aaa.ProductGroupID
                                      join aaaa in db.Colors on a.fk_ColorID equals aaaa.ColorID
                                      where a.IsDelete == false && aa.IsDelete == false
                                      && aa.fk_ProductGroupID == n.ProductGroupID && (aa.IsAlienCulture == null || aa.IsAlienCulture == false)
                                      orderby aa.DesignNo
                                      select new
                                      {
                                          aa.ProductID,
                                          a.ProductImage,
                                          aa.ProductName,
                                          aaa.ProductGroupName,
                                          ProductTitle = aa.ProductTitle + "-" + aaaa.ColorTitle,
                                          a.fk_ColorID,
                                          a.ProductColorID
                                      }).ToList();

                            foreach (var item in nm.ToList())
                            {
                                var pcs = db.ProductColorSizes.Where(a => a.IsAvailable == true && a.IsDelete == false &&
                                                                     a.fk_ProductColorID == item.ProductColorID).FirstOrDefault();
                                if (pcs == null)
                                    nm.Remove(item);
                            }

                            if (Request.QueryString["Size"] == null && Request.QueryString["Color"] == null)
                            {
                                VisibleEmptyBox(nm.Count());
                                rptProducts.DataSource = nm;
                                rptProducts.DataBind();
                            }
                            else
                            {
                                if (Request.QueryString["Color"] != null && Request.QueryString["Size"] == null)
                                {
                                    int ColorID = Convert.ToInt32(Request.QueryString["Color"]);

                                    var nnn = (from a in nm
                                               where a.fk_ColorID == ColorID
                                               select a).ToList();

                                    VisibleEmptyBox(nnn.Count());
                                    rptProducts.DataSource = nnn;
                                    rptProducts.DataBind();

                                }
                                else if (Request.QueryString["Size"] != null && Request.QueryString["Color"] == null)
                                {
                                    int SizeID = Convert.ToInt32(Request.QueryString["Size"]);

                                    var nnn = (from a in nm
                                               join aa in db.ProductColors
                                               on a.fk_ColorID equals aa.fk_ColorID
                                               join aaa in db.ProductColorSizes
                                               on aa.ProductColorID equals aaa.fk_ProductColorID
                                               where aaa.fk_SizeID == SizeID
                                               select a).ToList();

                                    List<Guid> ProductIDLists = new List<Guid>();


                                    foreach (var item in nnn.ToList())
                                    {
                                        if (!ProductIDLists.Contains(item.ProductID))
                                        {
                                            ProductIDLists.Add(item.ProductID);
                                        }
                                        else
                                        {
                                            nnn.Remove(item);
                                        }
                                    }

                                    VisibleEmptyBox(nnn.Count());
                                    rptProducts.DataSource = nnn;
                                    rptProducts.DataBind();

                                }
                                else if (Request.QueryString["Size"] != null
                                       && Request.QueryString["Color"] != null)
                                {
                                    int SizeID = Convert.ToInt32(Request.QueryString["Size"]);
                                    int ColorID = Convert.ToInt32(Request.QueryString["Color"]);

                                    var nnn = (from a in nm
                                               join aa in db.ProductColors
                                               on a.fk_ColorID equals aa.fk_ColorID
                                               join aaa in db.ProductColorSizes
                                               on aa.ProductColorID equals aaa.fk_ProductColorID
                                               where aaa.fk_SizeID == SizeID && a.fk_ColorID == ColorID
                                               select a).ToList();

                                    List<Guid> ProductIDLists = new List<Guid>();


                                    foreach (var item in nnn.ToList())
                                    {
                                        if (!ProductIDLists.Contains(item.ProductID))
                                        {
                                            ProductIDLists.Add(item.ProductID);
                                        }
                                        else
                                        {
                                            nnn.Remove(item);
                                        }
                                    }

                                    VisibleEmptyBox(nnn.Count());
                                    rptProducts.DataSource = nnn;
                                    rptProducts.DataBind();
                                }
                            }
                        }
                        else
                            Response.Redirect("/default.aspx");
                    }
                    else
                    {
                        var n = (from a in db.ProductGroup
                                 where a.ProductGroupName == ProductGroupName && a.IsAlienCulture == true
                                 && a.IsDelete == false
                                 select a).FirstOrDefault();

                        if (n != null)
                        {
                            var nm = (from a in db.ProductColors.AsEnumerable()
                                      join aa in db.Products.AsEnumerable()
                                      on a.fk_ProductID equals aa.ProductID
                                      join aaa in db.ProductGroup
                                      on aa.fk_ProductGroupID equals aaa.ProductGroupID
                                      join aaaa in db.Colors on a.fk_ColorID equals aaaa.ColorID
                                      where a.IsDelete == false && aa.IsDelete == false
                                      && aa.fk_ProductGroupID == n.ProductGroupID && aa.IsAlienCulture == true
                                      orderby aa.DesignNo
                                      select new
                                      {
                                          aa.ProductID,
                                          a.ProductImage,
                                          aa.ProductName,
                                          aaa.ProductGroupName,
                                          ProductTitle = aaaa.ColorEN_Title + "-" + ((IdentifyCulture.cultureName().Contains("en")) ? aa.EN_ProductTitle :
                                 ((IdentifyCulture.cultureName().Contains("ru")) ? aa.Rus_ProductTitle :
                                 ((IdentifyCulture.cultureName().Contains("zh")) ? aa.China_ProductTitle : aa.ProductTitle))),
                                          a.fk_ColorID,
                                          a.ProductColorID
                                      }).ToList();

                            foreach (var item in nm.ToList())
                            {
                                var pcs = db.ProductColorSizes.Where(a => a.IsAvailable == true && a.IsDelete == false &&
                                                                     a.fk_ProductColorID == item.ProductColorID).FirstOrDefault();
                                if (pcs == null)
                                    nm.Remove(item);
                            }

                            if (Request.QueryString["Size"] == null && Request.QueryString["Color"] == null)
                            {
                                VisibleEmptyBox(nm.Count());
                                rptProducts.DataSource = nm;
                                rptProducts.DataBind();
                            }
                            else
                            {
                                if (Request.QueryString["Color"] != null && Request.QueryString["Size"] == null)
                                {
                                    int ColorID = Convert.ToInt32(Request.QueryString["Color"]);

                                    var nnn = (from a in nm
                                               where a.fk_ColorID == ColorID
                                               select a).ToList();

                                    VisibleEmptyBox(nnn.Count());
                                    rptProducts.DataSource = nnn;
                                    rptProducts.DataBind();

                                }
                                else if (Request.QueryString["Size"] != null && Request.QueryString["Color"] == null)
                                {
                                    int SizeID = Convert.ToInt32(Request.QueryString["Size"]);

                                    var nnn = (from a in nm
                                               join aa in db.ProductColors
                                               on a.fk_ColorID equals aa.fk_ColorID
                                               join aaa in db.ProductColorSizes
                                               on aa.ProductColorID equals aaa.fk_ProductColorID
                                               where aaa.fk_SizeID == SizeID
                                               select a).ToList();

                                    List<Guid> ProductIDLists = new List<Guid>();


                                    foreach (var item in nnn.ToList())
                                    {
                                        if (!ProductIDLists.Contains(item.ProductID))
                                        {
                                            ProductIDLists.Add(item.ProductID);
                                        }
                                        else
                                        {
                                            nnn.Remove(item);
                                        }
                                    }

                                    VisibleEmptyBox(nnn.Count());
                                    rptProducts.DataSource = nnn;
                                    rptProducts.DataBind();

                                }
                                else if (Request.QueryString["Size"] != null
                                       && Request.QueryString["Color"] != null)
                                {
                                    int SizeID = Convert.ToInt32(Request.QueryString["Size"]);
                                    int ColorID = Convert.ToInt32(Request.QueryString["Color"]);

                                    var nnn = (from a in nm
                                               join aa in db.ProductColors
                                               on a.fk_ColorID equals aa.fk_ColorID
                                               join aaa in db.ProductColorSizes
                                               on aa.ProductColorID equals aaa.fk_ProductColorID
                                               where aaa.fk_SizeID == SizeID && a.fk_ColorID == ColorID
                                               select a).ToList();

                                    List<Guid> ProductIDLists = new List<Guid>();


                                    foreach (var item in nnn.ToList())
                                    {
                                        if (!ProductIDLists.Contains(item.ProductID))
                                        {
                                            ProductIDLists.Add(item.ProductID);
                                        }
                                        else
                                        {
                                            nnn.Remove(item);
                                        }
                                    }

                                    VisibleEmptyBox(nnn.Count());
                                    rptProducts.DataSource = nnn;
                                    rptProducts.DataBind();
                                }
                            }
                        }
                        else
                            Response.Redirect("/default.aspx");
                    }
                }

            }
        }
        public void rptCategoryGroupBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                if (IdentifyCulture.cultureName().Contains("fa"))
                {
                    var n = (from u in db.ProductGroup.AsEnumerable()
                             where u.fk_ProductGroupID == null && u.IsDelete == false && u.IsAlienCulture != true
                             orderby u.ProductGroupID
                             select new
                             {
                                 u.ProductGroupName,
                                 u.ProductGroupID,
                                 ProductGroupTitle = u.ProductGroupTitle
                             }).ToList();
                    rptCategoryGroup.DataSource = n;
                    rptCategoryGroup.DataBind();
                }
                else
                {
                    var n = (from u in db.ProductGroup.AsEnumerable()
                             where u.fk_ProductGroupID == null && u.IsDelete == false && u.IsAlienCulture == true
                             orderby u.ProductGroupID
                             select new
                             {
                                 u.ProductGroupName,
                                 u.ProductGroupID,
                                 ProductGroupTitle = ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_ProductGroupTitle :
                                 ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_ProductGroupTitle :
                                 ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_ProductGroupTitle : u.ProductGroupTitle)))

                             }).ToList();
                    rptCategoryGroup.DataSource = n;
                    rptCategoryGroup.DataBind();
                }
            }
        }
        public void rptColorBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Colors.AsEnumerable()
                         where u.IsDelete == false
                         select new
                         {
                             u.ColorID,
                             u.ColorNo,
                             ColorTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.ColorTitle :
                                ((IdentifyCulture.cultureName().Contains("en")) ? u.ColorEN_Title :
                                ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_ColorTitle :
                                ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_ColorTitle : u.ColorTitle))),
                         }).ToList();
                rptColors.DataSource = n;
                rptColors.DataBind();
            }
        }
        public void rprSizeBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.SIzes where u.IsDelete == false select u).ToList();
                rptSize.DataSource = n;
                rptSize.DataBind();
            }
        }
        protected void lbAllProductGroup_Click(object sender, EventArgs e)
        {

        }



        protected void rptColors_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string absouluteUrladdress = HttpContext.Current.Request.Url.AbsolutePath;
            string Urladdress = HttpContext.Current.Request.Url.ToString();
            string ColorID = e.CommandArgument.ToString();

            if (Request.QueryString["Size"] != null)
            {
                if (Request.QueryString["Color"] == null)
                    Response.Redirect(Urladdress + "&&Color=" + ColorID);
                else
                {
                    string OldColor = Request.QueryString["Color"];
                    Urladdress = Urladdress.Replace(OldColor, ColorID);
                    Response.Redirect(Urladdress);
                }
            }
            else
            {
                if (Request.QueryString["Color"] == null)
                    Response.Redirect(absouluteUrladdress + "?Color=" + ColorID);
                else
                {
                    string OldColor = Request.QueryString["Color"];
                    Urladdress = Urladdress.Replace(OldColor, ColorID);

                    Response.Redirect(Urladdress);
                }
            }


        }



        protected void rptSize_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string absouluteUrladdress = HttpContext.Current.Request.Url.AbsolutePath;
            string Urladdress = HttpContext.Current.Request.Url.ToString();
            string SizeID = e.CommandArgument.ToString();

            if (Request.QueryString["Color"] != null)
            {
                if (Request.QueryString["Size"] == null)
                    Response.Redirect(Urladdress + "&&Size=" + SizeID);
                else
                {
                    string OldSize = Request.QueryString["Size"];
                    Urladdress = Urladdress.Replace(OldSize, SizeID);
                    Response.Redirect(Urladdress);
                }
            }
            else
            {
                if (Request.QueryString["Size"] == null)
                    Response.Redirect(absouluteUrladdress + "?Size=" + SizeID);
                else
                {
                    string OldSize = Request.QueryString["Size"];
                    Urladdress = Urladdress.Replace(OldSize, SizeID);
                    Response.Redirect(Urladdress);
                }
            }




        }

        protected void rptCategoryGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                HiddenField hfCatID = (HiddenField)e.Item.FindControl("hfCatID");
                string HFProductGroupName = hfCatID.Value.ToString();

                if (Page.RouteData.Values["ProductGroupName"].ToString() != null)
                {
                    string ProductGroupName = Page.RouteData.Values["ProductGroupName"].ToString();

                    if (ProductGroupName == HFProductGroupName)
                    {

                        HyperLink hlcatID = (HyperLink)e.Item.FindControl("hlprogroupLink");

                        hlcatID.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2f4497");
                    }
                }
            }
        }

        protected void rptSize_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (Request.QueryString["Size"] != null)
                {
                    string ReqSizeID = (Request.QueryString["Size"].ToString());
                    HiddenField hfSizeID = (HiddenField)e.Item.FindControl("hfSizeID");
                    string sizeid = hfSizeID.Value.ToString();
                    if (ReqSizeID == sizeid)
                    {
                        string classID = ".class" + sizeid;
                        ScriptManager.RegisterStartupScript(this, GetType(), "PageScript",
                           "$('" + classID + "').css('color','#fff');", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "PageScriptaa",
                     "$('" + classID + "').css('background-color','#2f4497');", true);
                    }
                }
            }
        }

        protected void rptColors_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (Request.QueryString["Color"] != null)
                {
                    string ReqColorID = (Request.QueryString["Color"].ToString());
                    HiddenField hfColorID = (HiddenField)e.Item.FindControl("hfColorID");
                    string colorid = hfColorID.Value.ToString();
                    if (ReqColorID == colorid)
                    {
                        string classID = ".class" + colorid;
                        ScriptManager.RegisterStartupScript(this, GetType(), "PageScriptasd",
                           "$('" + classID + "').css('border','3px solid #2f4497');", true);

                    }
                }
            }
        }
        internal void VisibleEmptyBox(int productCount)
        {
            if (productCount == 0)
                pnlEmpty.Visible = true;
            else
                pnlEmpty.Visible = false;

        }
    }
}