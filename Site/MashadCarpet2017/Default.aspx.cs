using GSD.Globalization;
using MashadCarpet.Classes;
using MashadCarpet.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MashadCarpet
{
    public partial class Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //UpdateAlienImages();
            //  HttpCookie cookie = Request.Cookies["CurrentLanguage"];
            if (IdentifyCulture.cultureName().Contains("fa"))
            {
                var persianCulture = new PersianCulture();
                Thread.CurrentThread.CurrentCulture = persianCulture;
                Thread.CurrentThread.CurrentUICulture = persianCulture;
                imglogo.ImageUrl = "~/images/Logo2.png";
                imgSh.ImageUrl = "~/images/Shoar.png";

                const string styleFormat =
            "<LINK href=\"{0}\" type=\"text/css\" rel=\"stylesheet\">";

                string linkText;
                linkText = String.Format(styleFormat, StyleSheetPathFa);
                StyleSheet.Text = linkText;
            }
            else
            {
                imglogo.ImageUrl = "~/images/Logo-eng.png";
                imgSh.ImageUrl = "~/images/Shor-Eng.png";

                const string styleFormat =
               "<LINK href=\"{0}\" type=\"text/css\" rel=\"stylesheet\">";

                string linkText;
                linkText = String.Format(styleFormat, StyleSheetPath);
                StyleSheet.Text = linkText;

            }
            if (!Page.IsPostBack)
            {
                ChooseTitleAndDesc();
                rptProductGroupBind();
                OrderLoad();
                rptRecentProductsBind();
                rptLinksBind();

                rptBlogsBind();
                rptSliderBind();
                rptMiddelTextBind();
                rptBuyingManualBind();
                rptMainProductGroupBind();
                rptTextBind();
                PaymentBtnAddress();

                //UpdatePaymentDate();
            }
        }
        public void PaymentBtnAddress()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Guid UserID = new Guid(HttpContext.Current.User.Identity.Name);
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from a in db.Orders
                             where a.fk_UserID == UserID && a.IsFinalized == false && a.IsDelete == false
                             select a).FirstOrDefault();
                    if (n != null)
                    {
                        hlPayment.NavigateUrl = "~/bank.aspx?OID=" + n.OrderID.ToString() + "&&UID=" + UserID.ToString();
                        pnlIsActiveFactor.Visible = true;
                    }
                    else
                        pnlIsActiveFactor.Visible = false;
                }
            }
            else
                pnlIsActiveFactor.Visible = false;
        }
        public void ChooseTitleAndDesc()
        {
            if (IdentifyCulture.cultureName().Contains("fa"))
            {
                Page.Title = "وب سایت رسمی فرش مشهد";
                Page.MetaDescription = "وب سایت رسمی فرش مشهد";
            }
            else if (IdentifyCulture.cultureName().Contains("en"))
            {
                Page.Title = "mashad carpet website";
                Page.MetaDescription = "mashad carpet website";
            }
            else if (IdentifyCulture.cultureName().Contains("ru"))
            {
                Page.Title = "mashad carpet website";
                Page.MetaDescription = "mashad carpet website";
            }
            else if (IdentifyCulture.cultureName().Contains("zh"))
            {
                Page.Title = "mashad carpet website";
                Page.MetaDescription = "mashad carpet website";
            }
            else
            {
                Page.Title = "وب سایت رسمی فرش مشهد";
                Page.MetaDescription = "وب سایت رسمی فرش مشهد";
            }
        }
        protected string StyleSheetPathFa
        {
            get
            {
                // pull the stylesheet name from a database or xml file...
                return ApplicationPath + "css/rightStyle.css";
            }
        }
        protected string StyleSheetPath
        {
            get
            {
                // pull the stylesheet name from a database or xml file...
                return ApplicationPath + "css/leftStyle.css";
            }
        }

        private string ApplicationPath
        {
            get
            {
                string result = Request.ApplicationPath;
                if (!result.EndsWith("/"))
                {
                    result += "/";
                }
                return result;
            }
        }


        public void LinkbuttonEnable(LinkButton lbID)
        {
            lbID.Enabled = true;
        }
        public void rptTextBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Texts.AsEnumerable()
                         where u.GroupID == 4
                         select new
                         {

                             u.TextName,
                             u.TextImage,
                             TextTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextTitle :
                         ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextTitle :
                         ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextTitle :
                         ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextTitle : u.TextTitle))),

                             TextDescription = (IdentifyCulture.cultureName().Contains("fa")) ? (u.TextDescription.Length > 100 ? u.TextDescription.Substring(0, 100) : u.TextDescription) :
                         ((IdentifyCulture.cultureName().Contains("en")) ? (u.EN_TextDescription.Length > 100 ? u.EN_TextDescription.Substring(0, 100) : u.EN_TextDescription) :
                         ((IdentifyCulture.cultureName().Contains("ru")) ? (u.Rus_TextDescription.Length > 100 ? u.Rus_TextDescription.Substring(0, 100) : u.Rus_TextDescription) :
                         ((IdentifyCulture.cultureName().Contains("zh")) ? (u.China_TextDescription.Length > 100 ? u.China_TextDescription.Substring(0, 100) : u.China_TextDescription) : (u.TextDescription.Length > 100 ? u.TextDescription.Substring(0, 100) : u.TextDescription)))),
                         }).ToList().Take(3);
                //   rptText.DataSource = n;
                //   rptText.DataBind();              

            }
        }

        public void rptSliderBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Slider select u).ToList();
                rptSlider.DataSource = n;
                rptSlider.DataBind();
            }
        }

        public void rptMainProductGroupBind()
        {
            //    using (MashadCarpetEntities db = new MashadCarpetEntities())
            //    {
            //        var n = (from u in db.ProductGroup where u.fk_ProductGroupID == null && u.IsDelete == false select u).ToList().Take(2);
            //        rptMainProductGroup.DataSource = n;
            //        rptMainProductGroup.DataBind();
            //    }
        }
        public void rptMiddelTextBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Texts.AsEnumerable()
                         where u.GroupID == 1 && u.IsDelete == false
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
                rptMiddelText.DataSource = n;
                rptMiddelText.DataBind();
            }
        }

        public void rptBuyingManualBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Texts.AsEnumerable()
                         where u.GroupID == 2 && u.IsDelete == false
                         select new
                         {
                             TextTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextTitle :
                            ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextTitle :
                            ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextTitle :
                            ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextTitle : u.TextTitle))),
                             u.TextName,
                             TextDescription = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextDescription :
                         ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextDescription :
                         ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextDescription :
                         ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextDescription : u.TextDescription))),

                         }).ToList();
                rptBuyingManual.DataSource = n;
                rptBuyingManual.DataBind();
            }
        }
        public void rptProductGroupBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                //AlienLang
               
                if (!IdentifyCulture.cultureName().Contains("fa"))
                {

                    //Right Line
                    var n = (from u in db.ProductGroup.AsEnumerable()
                             where u.IsDelete == false && u.fk_ProductGroupID == null && u.IsAlienCulture == true
                             orderby u.Priority
                             select new
                             {
                                 u.ProductGroupID,
                                 ProductGroupTitle =
                                 (IdentifyCulture.cultureName().Contains("fa")) ? u.ProductGroupTitle :
                                 ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_ProductGroupTitle :
                                 ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_ProductGroupTitle :
                                 ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_ProductGroupTitle : u.ProductGroupTitle))),
                                 u.ProductGroupName

                             }).ToList().Take(6);
                    rptProductGroup.DataSource = n;
                    rptProductGroup.DataBind();

                    //Left Line
                    var n2 = (from u in db.ProductGroup.AsEnumerable()
                              where u.IsDelete == false && u.fk_ProductGroupID == null && u.IsAlienCulture == true
                              orderby u.Priority
                              select new
                              {
                                  u.ProductGroupID,
                                  ProductGroupTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.ProductGroupTitle :
                                  ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_ProductGroupTitle :
                                  ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_ProductGroupTitle :
                                  ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_ProductGroupTitle : u.ProductGroupTitle))),
                                  u.ProductGroupName

                              }).Skip(6).ToList().Take(5);
                    rptProductGroup2.DataSource = n2;
                    rptProductGroup2.DataBind();

                }
                else
                {
                    //Right Line
                    var n = (from u in db.ProductGroup.AsEnumerable()
                             where u.IsDelete == false && u.fk_ProductGroupID == null && u.IsAlienCulture != true
                             orderby u.Priority
                             select new
                             {
                                 u.ProductGroupID,
                                 ProductGroupTitle = u.ProductGroupTitle,
                                 u.ProductGroupName

                             }).ToList().Take(6);
                    rptProductGroup.DataSource = n;
                    rptProductGroup.DataBind();

                    //Left Line
                    var n2 = (from u in db.ProductGroup.AsEnumerable()
                              where u.IsDelete == false && u.fk_ProductGroupID == null && u.IsAlienCulture != true
                              orderby u.Priority
                              select new
                              {
                                  u.ProductGroupID,
                                  ProductGroupTitle = u.ProductGroupTitle,
                                  u.ProductGroupName

                              }).Skip(6).ToList().Take(5);
                    rptProductGroup2.DataSource = n2;
                    rptProductGroup2.DataBind();
                }
            }
        }

        public void rptRecentProductsBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                //AlienLang
                if (!IdentifyCulture.cultureName().Contains("fa"))
                {
                    var n = (from aaa in db.ProductGroup.AsEnumerable()
                             where aaa.IsDelete == false && aaa.fk_ProductGroupID == null && aaa.IsAlienCulture == true
                             orderby aaa.Priority
                             select new
                             {
                                 aaa.ProductGroupImage,
                                 aaa.ProductGroupName,
                                 ProductGroupTitle = (IdentifyCulture.cultureName().Contains("fa")) ? aaa.ProductGroupTitle :
                        ((IdentifyCulture.cultureName().Contains("en")) ? aaa.EN_ProductGroupTitle :
                        ((IdentifyCulture.cultureName().Contains("ru")) ? aaa.Rus_ProductGroupTitle :
                        ((IdentifyCulture.cultureName().Contains("zh")) ? aaa.China_ProductGroupTitle : aaa.ProductGroupTitle))),
                             }).ToList();

                    rptRecentProducts.DataSource = n;
                    rptRecentProducts.DataBind();
                }
                else
                {
                    var n = (from aaa in db.ProductGroup.AsEnumerable()
                             where aaa.IsDelete == false && aaa.fk_ProductGroupID == null && aaa.IsAlienCulture != true
                             orderby aaa.Priority
                             select new
                             {
                                 aaa.ProductGroupImage,
                                 aaa.ProductGroupName,
                                 ProductGroupTitle = aaa.ProductGroupTitle,
                             }).ToList();

                    rptRecentProducts.DataSource = n;
                    rptRecentProducts.DataBind();
                }
            }
        }



        //protected void rptProductGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        HiddenField hfProductGroupID = (HiddenField)e.Item.FindControl("hfProductGroupID");
        //        Guid ProductGroupID = new Guid(hfProductGroupID.Value.ToString());

        //        Repeater rptProduct = (Repeater)e.Item.FindControl("rptProduct");

        //        using (MashadCarpetEntities db = new MashadCarpetEntities())
        //        {
        //            var n = (from u in db.Products 
        //                     where u.IsDelete == false && u.fk_ProductGroupID == ProductGroupID 
        //                     select u).Take(5).ToList();

        //            rptProduct.DataSource = n;
        //            rptProduct.DataBind();
        //        }
        //    }
        //}

        public void OrderLoad()
        {
            //  FormsAuthentication.SignOut();
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Guid UserID = new Guid(HttpContext.Current.User.Identity.Name);
                //Guid UserID = new Guid("ee4edaf0-aad0-4140-a9fe-8b9bcaab6bd3");
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from a in db.Orders
                             where a.fk_UserID == UserID && a.IsFinalized == false
                             select a).FirstOrDefault();

                    if (n != null)
                    {
                        var m = (from u in db.OrderDetails
                                 join i in db.ProductColorSizes
                                 on u.fk_ProductColorSizeID equals i.ProductColorSizeID
                                 join aa in db.ProductColors on i.fk_ProductColorID equals aa.ProductColorID
                                 join p in db.Products on aa.fk_ProductID equals p.ProductID
                                 join aaa in db.ProductGroup on p.fk_ProductGroupID equals aaa.ProductGroupID
                                 where u.fk_OrderID == n.OrderID && i.IsDelete == false && u.IsDelete == false && p.IsDelete == false
                                 select new
                                 {
                                     p.ProductTitle,
                                     p.ThumbImage1,
                                     aa.ProductImage,
                                     aa.ProductColorID,
                                     u.Count,
                                     i.ProductPrice,
                                     p.ProductID,
                                     u.OrderDetailID,
                                     p.ProductName,
                                     aaa.ProductGroupName
                                 }).ToList();

                        rptBasketItems.DataSource = m;
                        rptBasketItems.DataBind();

                        int price = 0;
                        foreach (var item in m)
                        {
                            for (int i = 0; i < item.Count; i++)
                            {
                                price = price + Convert.ToInt32(item.ProductPrice);
                            }

                        }

                        //lblTotalPrice.Text = string.Format("{0:N0}", price) + "   تومان";
                        //lblOrderTotal.Text = string.Format("{0:N0}", price) + "   تومان";
                        //lblTotal.Text = string.Format("{0:N0}", price) + "   تومان";
                        lblOrderCount2.Text = m.Count().ToString();
                        lblOrderCount.Text = m.Count().ToString();
                        lblOrderTotal.Text = (IdentifyCulture.cultureName().Contains("fa")) ? string.Format("{0:N0}", price) + "   تومان" :
                              ((IdentifyCulture.cultureName().Contains("en")) ? string.Format("{0:N0}", price) + "   Toman" :
                              ((IdentifyCulture.cultureName().Contains("ru")) ? string.Format("{0:N0}", price) + "   RS" :
                              ((IdentifyCulture.cultureName().Contains("zh")) ? string.Format("{0:N0}", price) + "   RS" : string.Format("{0:N0}", price) + "   تومان")));

                        lblTotalPrice.Text = (IdentifyCulture.cultureName().Contains("fa")) ? string.Format("{0:N0}", price) + "   تومان" :
                              ((IdentifyCulture.cultureName().Contains("en")) ? string.Format("{0:N0}", price) + "   Toman" :
                              ((IdentifyCulture.cultureName().Contains("ru")) ? string.Format("{0:N0}", price) + "   RS" :
                              ((IdentifyCulture.cultureName().Contains("zh")) ? string.Format("{0:N0}", price) + "   RS" : string.Format("{0:N0}", price) + "   تومان")));

                        lblTotal.Text = (IdentifyCulture.cultureName().Contains("fa")) ? string.Format("{0:N0}", price) + "   تومان" :
                              ((IdentifyCulture.cultureName().Contains("en")) ? string.Format("{0:N0}", price) + "   Toman" :
                              ((IdentifyCulture.cultureName().Contains("ru")) ? string.Format("{0:N0}", price) + "   RS" :
                              ((IdentifyCulture.cultureName().Contains("zh")) ? string.Format("{0:N0}", price) + "   RS" : string.Format("{0:N0}", price) + "   تومان")));
                    }
                }
            }
            else
            {
                lblTotal.Text = "0";
                lblOrderCount2.Text = "0";
                lblOrderCount.Text = "0";
                //pnlEmpty.Visible = true;
                //pnltotal.Visible = false;
            }
        }

        protected void lbExit_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }

        protected void rptBasketItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hfOrderDetailID = (HiddenField)e.Item.FindControl("hfOrderDetailID");
                Guid OrderDetailID = new Guid(hfOrderDetailID.Value);
                Label lblPrice = (Label)e.Item.FindControl("lblPrice");
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var m = (from u in db.OrderDetails
                             join i in db.ProductColorSizes
                                   on u.fk_ProductColorSizeID equals i.ProductColorSizeID

                             where u.OrderDetailID == OrderDetailID
                             select new
                             {
                                 u.Count,
                                 i.ProductPrice
                             }).FirstOrDefault();
                    lblPrice.Text = string.Format("{0:N0}", (m.Count * (m.ProductPrice))) + " تومان";
                }
            }
        }

        protected void rptBasketItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Guid OrderDetailID = new Guid(e.CommandArgument.ToString());
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.OrderDetails where u.OrderDetailID == OrderDetailID select u).FirstOrDefault();
                n.IsDelete = true;
                n.DeleteDate = DateTime.Now;

                db.SaveChanges();

                OrderLoad();
            }
        }

        public void rptBlogsBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Blogs.AsEnumerable()
                         join aa in db.BlogGroups on u.fk_BlogGroupID equals aa.BlogGroupID
                         where u.IsDelete == false
                         select new
                         {
                             u.BlogImage,
                             aa.BlogGroupName,
                             u.VisitCounts,
                             //u.BlogTitle,
                             BlogTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.BlogTitle :
                            ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_BlogTitle :
                            ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_BlogTitle :
                            ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_BlogTitle : u.BlogTitle))),
                             //BlogText = (u.BlogText.Length > 100) ? u.BlogText.Substring(0, 100) + "..." : u.BlogText,



                             //     BlogText = (IdentifyCulture.cultureName().Contains("fa")) ? (u.BlogText.Length>100?u.BlogText.Substring(0,100):u.BlogText) :
                             //((IdentifyCulture.cultureName().Contains("en")) ? (u.EN_BlogText.Length>100?u.EN_BlogText.Substring(0,100):u.EN_BlogText) :
                             //((IdentifyCulture.cultureName().Contains("ru")) ? (u.Rus_BlogText.Length>100?u.Rus_BlogText.Substring(0,100):u.Rus_BlogText):
                             //((IdentifyCulture.cultureName().Contains("zh")) ? (u.China_BlogText.Length>100?u.China_BlogText.Substring(0,100):u.China_BlogText) : (u.BlogText.Length>100?u.BlogText.Substring(0,100):u.BlogText)))),
                             u.SubmitDate,
                             u.BlogName
                         }).ToList();
                rptBlogs.DataSource = n;
                rptBlogs.DataBind();
            }
        }
        public void rptLinksBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Links.AsEnumerable()
                         orderby u.RegisterDate
                         where u.IsDelete == false
                         select new
                         {
                             u.imgFile,
                             u.LinkAddres,
                             linktitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.title :
                        ((IdentifyCulture.cultureName().Contains("en")) ? u.title_En :
                        ((IdentifyCulture.cultureName().Contains("ru")) ? u.title_Ru :
                        ((IdentifyCulture.cultureName().Contains("zh")) ? u.title_Ch : u.title)))
                         });


                rptLinks.DataSource = n;
                rptLinks.DataBind();
            }
        }
        //protected void rptRecentProducts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        HiddenField hfRel_Product_Color_Size_ID = (HiddenField)e.Item.FindControl("hfRel_Product_Color_Size_ID");
        //        Guid Rel_Product_Color_Size_ID = new Guid(hfRel_Product_Color_Size_ID.Value.ToString());
        //        Image ProductHoverImage = (Image)e.Item.FindControl("ProductHoverImage");

        //        using(MashadCarpetEntities db=new MashadCarpetEntities())
        //        {
        //            var n = (from u in db.Rel_Product_Color_Size where u.Rel_Product_Color_Size_ID == Rel_Product_Color_Size_ID select u).FirstOrDefault();

        //            var m = (from u in db.Rel_Product_Color_Size where u.Rel_Product_Color_Size_ID != Rel_Product_Color_Size_ID && u.fk_ProductID == n.fk_ProductID && u.IsDelete==false select u).FirstOrDefault();

        //            ProductHoverImage.ImageUrl = "/Uploads/Products/" + m.ProductImage;
        //        }

        //    }
        //}



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SearchResult.aspx?Text=" + txtSearch.Text);
        }

        protected void rptSlider_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                HiddenField hfID = (HiddenField)e.Item.FindControl("hfID");

                Repeater rptSliderText = (Repeater)e.Item.FindControl("rptSliderText");

                int ID = Convert.ToInt32(hfID.Value);

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from a in db.SliderTexts.AsEnumerable()
                             where a.fk_SliderID == ID
                             orderby a.datay descending
                             select new
                             {
                                 a.SliderTextID,
                                 a.datax,
                                 a.datay,
                                 a.fk_SliderID,
                                 a.InAndOutClass,
                                 a.IsLink,
                                 a.LinkAddress,
                                 a.Slider,
                                 a.fontsize,
                                 a.speed,
                                 a.start,
                                 a.textColor,
                                 Text = (IdentifyCulture.cultureName().Contains("fa")) ? a.Text :
                            ((IdentifyCulture.cultureName().Contains("en")) ? a.EN_Text :
                            ((IdentifyCulture.cultureName().Contains("ru")) ? a.Rus_Text :
                            ((IdentifyCulture.cultureName().Contains("zh")) ? a.China_Text : a.Text))),
                             }).ToList();

                    rptSliderText.DataSource = n;
                    rptSliderText.DataBind();
                }

            }
        }


        //public void UpdateAlienImages()
        //{
        //    using (MashadCarpetEntities db = new MashadCarpetEntities())
        //    {
        //        List<ProductColors> alienproductColors = db.ProductColors.Where(current => current.IsDelete == false
        //        && current.Products.IsAlienCulture == true && current.Products.IsActive == true && current.ProductImage == null).ToList();

        //        List<ProductColors> productColors = db.ProductColors.Where(current => current.IsDelete == false
        //        && (current.Products.IsAlienCulture == false || current.Products.IsAlienCulture == null) && current.Products.IsActive == true && current.ProductImage != "").ToList();

        //        foreach (var item in alienproductColors)
        //        {
        //            foreach (var item2 in productColors)
        //            {
        //                if (item.Products.ProductTitle == item2.Products.ProductTitle && item.fk_ColorID == item2.fk_ColorID)
        //                {
        //                    item.ProductImage = item2.ProductImage;

        //                }
        //            }
        //        }
        //        db.SaveChanges();

        //    }

        //}
        //protected void rptSliderText_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        HiddenField hfTextID = (HiddenField)e.Item.FindControl("hfTextID");

        //        int TextID = Convert.ToInt32(hfTextID.Value);

        //        Control textDiv = e.Item.FindControl("textDiv");

        //        textDiv.st
        //    }
        //}

        //private void UpdatePaymentDate()
        //{
        //    using (MashadCarpetEntities db = new MashadCarpetEntities())
        //    {
        //        var n = (from u in db.Orders where u.IsDelete == false && u.IsPaid == true && u.PaymentDate == null select u).ToList();

        //        foreach (var item in n)
        //        {
        //            var m = (from u in db.PaymentLogs where u.fk_OrderID == item.OrderID select u).FirstOrDefault();
        //            item.PaymentDate = m.PaymentDate;

        //        }
        //        db.SaveChanges();
        //    }
        //}

    }
}