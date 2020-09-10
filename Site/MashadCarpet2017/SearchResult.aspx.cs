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
    public partial class SearchResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadBannerImages();
                rptCategoryGroupBind();



                ChooseTitleAndDesc();
                //rptBestSellersBind();
                rptProductsBind();
                //rptPageCountBind();
            }
        }
        public void ChooseTitleAndDesc()
        {
            if (IdentifyCulture.cultureName().Contains("fa"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "PageScriptfa",
               "$('.newfa').removeClass('fa-arrow-right'); $('.newfa').addClass('fa-arrow-left');", true);
                Page.Title = "فروشگاه اینترنتی فرش | وب سایت رسمی فرش مشهد";
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


        public void LoadBannerImages()
        {

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from a in db.ProductGroup.AsEnumerable()
                         where a.IsDelete == false
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
                         }).ToList();

                rptbannerImages.DataSource = n;
                rptbannerImages.DataBind();

            }
        }
        public void rptCategoryGroupBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.ProductGroup
                         where u.fk_ProductGroupID == null && u.IsDelete == false
                         select
                             new
                             {
                                 u.ProductGroupID,
                                 u.ProductGroupName,
                                 u.ProductGroupTitle,
                             }).ToList();
                rptCategoryGroup.DataSource = n;
                rptCategoryGroup.DataBind();
            }
        }

        //public void rptBestSellersBind()
        //{
        //    using (MashadCarpetEntities db = new MashadCarpetEntities())
        //    {
        //        var n = (from u in db.ProductColors
        //                 join aa in db.Products
        //                 on u.fk_ProductID equals aa.ProductID
        //                 join aaa in db.ProductGroup on aa.fk_ProductGroupID equals aaa.ProductGroupID

        //                 where u.IsDelete==false&&aa.IsDelete==false
        //                 select new
        //                 {
        //                     u.ProductColorID,
        //                     u.ProductImage,
        //                     aa.ProductTitle,
        //                     aaa.ProductGroupName,
        //                     aa.ProductName,
        //                     aa.ProductID,

        //                 }).ToList();


        //        rptBestSellers.DataSource = n;
        //        rptBestSellers.DataBind();
        //    }
        //} 

        public void rptProductsBind()
        {
            if (Request.QueryString["Text"] != null)
            {
                string Text = Request.QueryString["Text"].ToString();
                txtSearch.Text = Text;

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.ProductColors
                             join aa in db.Products
                             on u.fk_ProductID equals aa.ProductID
                             join aaa in db.ProductGroup on aa.fk_ProductGroupID equals aaa.ProductGroupID
                             where u.IsDelete == false && aa.IsDelete == false && aa.ProductTitle.Contains(Text)
                             select new
                             {
                                 u.ProductColorID,
                                 u.ProductImage,
                                 aa.ProductTitle,
                                 aaa.ProductGroupName,
                                 aa.ProductName,
                                 aa.ProductID,

                             }).ToList();

                    var m = (from aaa in db.ProductGroup
                             where aaa.ProductGroupTitle.Contains(Text) && aaa.IsDelete == false
                             select new
                             {

                                 aaa.ProductGroupImage,
                                 aaa.ProductGroupName,
                                 aaa.ProductGroupTitle,
                                 aaa.ProductGroupID,

                             }).ToList();

                    if (n.Count == 0 && m.Count() == 0)
                    {
                        pnlEmpty.Visible = true;
                    }

                    else
                    {
                        pnlEmpty.Visible = false;
                        rptProducts.DataSource = n;
                        rptProducts.DataBind();
                        rptProductgroups.DataSource = m;
                        rptProductgroups.DataBind();
                    }
                }
            }

            //if (Request.QueryString["Text"] != null)
            //{
            //    string Text = Request.QueryString["Text"].ToString();
            //    txtSearch.Text = Text;
            //     char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            //     string[] a = Text.Split(delimiterChars);


            //    using (MashadCarpetEntities db = new MashadCarpetEntities())
            //    {
            //        List<Products> p = new List<Products>();


            //        Boolean b = false;
            //        foreach(var t in a)
            //        {
            //            var n = (from u in db.Products
            //                     join pg in db.ProductGroup on u.fk_ProductGroupID equals pg.ProductGroupID
            //                     where u.IsDelete == false && (u.ProductTitle.Contains(t))
            //                     select new
            //                         {
            //                             u.ProductColorID,
            //                             u.ProductImage,
            //                             u.ProductTitle,
            //                             pg.ProductGroupName,
            //                             u.ProductName,
            //                             u.ProductID,
            //                         }).ToList();
            //            foreach(var i in n)
            //            {
            //                if(p.Count>0)
            //                {
            //                    foreach (var k in p)
            //                    {
            //                        if (k.ProductID == i.ProductID)
            //                        {
            //                            b = true;
            //                            break;
            //                        }

            //                    }
            //                    if (b == false)
            //                        p.Add(i);
            //                }
            //                else
            //                {
            //                    p.Add(i);

            //                }

            //            }
            //        }





            //        rptProducts.DataSource = p;
            //        rptProducts.DataBind();

            //        if (p.Count == 0)
            //        {
            //            pnlEmpty.Visible = true;

            //        }

            //        else
            //        {
            //            pnlEmpty.Visible = false; 
            //        }

            //    }
            //}

        }

        protected void rptBestSellers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{
            //    HiddenField hfProductID = (HiddenField)e.Item.FindControl("hfProductID");
            //    Guid ProductID = new Guid(hfProductID.Value.ToString());

            //    Image ProductImage = (Image)e.Item.FindControl("ProductImage");
            //    Image ProductImageHover = (Image)e.Item.FindControl("ProductImageHover");

            //    Label lblPrice = (Label)e.Item.FindControl("lblPrice");

            //    using (MashadCarpetEntities db = new MashadCarpetEntities())
            //    {
            //        var n = (from u in db.Rel_Product_Color_Size where u.fk_ProductID == ProductID && u.IsDelete == false select u).FirstOrDefault();
            //        if (n != null)
            //        {
            //            lblPrice.Text = string.Format("{0:N0}", n.ProductPrice) + "تومان";
            //         //  ProductImage.ImageUrl = "/Uploads/Products/" + n.ProductImage;
            //         //  ProductImageHover.ImageUrl = "/Uploads/Products/" + n.ProductImage;
            //        }


            //    }
            //}
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchResult.aspx?Text=" + txtSearch.Text);
        }














    }
}