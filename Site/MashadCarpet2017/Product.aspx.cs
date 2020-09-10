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
    public partial class Product : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FindProduct();
                rptColorsBind();
                rptSizeBind();
                rptRelatedProductsBind();
                ChooseTitleAndDesc();
                Title.Insert(0, "0");
            }
        }

        public void ChooseTitleAndDesc()
        {
            string ProductName = Page.RouteData.Values["ProductName"].ToString();

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from a in db.Products.AsEnumerable()
                         where a.IsDelete == false && a.ProductName != ProductName
                         select new
                         {
                             ProductTitle = (IdentifyCulture.cultureName().Contains("fa")) ? a.ProductTitle :
                                 ((IdentifyCulture.cultureName().Contains("en")) ? a.EN_ProductTitle :
                                 ((IdentifyCulture.cultureName().Contains("ru")) ? a.Rus_ProductTitle :
                                 ((IdentifyCulture.cultureName().Contains("zh")) ? a.China_ProductTitle : a.ProductTitle))),
                         }).FirstOrDefault();

                if (IdentifyCulture.cultureName().Contains("fa"))
                {
                    UpdatePanel1.Visible = true;
                    pnlAvailableSize.Visible = true;
                    Page.Title = ProductName + " | وب سایت رسمی فرش مشهد";
                    Page.MetaDescription = "خرید فرش " + n.ProductTitle + " در فروشگاه آنلاین فرش مشهد، ارائه دهنده بهترین فرش های ماشینی با ارزانترین قیمت و ارسال رایگان به سراسر کشور. ";
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
                    Page.Title = n.ProductTitle + " | وب سایت رسمی فرش مشهد";
                    Page.MetaDescription = "خرید فرش " + n.ProductTitle + " در فروشگاه آنلاین فرش مشهد، ارائه دهنده بهترین فرش های ماشینی با ارزانترین قیمت و ارسال رایگان به سراسر کشور. ";
                }


            }

        }
        public void disablePrice(Boolean NoPro)
        {
            if (NoPro == true)
            {
                btnAddToCart.Visible = false;
                txtCount.Visible = false;
                lblPrice.Visible = false;
                pnlNoStock.Visible = true;
            }
            else
            {
                btnAddToCart.Visible = true;
                txtCount.Visible = true;
                lblPrice.Visible = true;
                pnlNoStock.Visible = false;
            }
        }
        public void FindProduct()
        {
            if (Request.QueryString["ColorID"] != null)
            {
                Guid ProductColorID = new Guid(Request.QueryString["ColorID"].ToString());
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var o = (from a in db.ProductColors
                             where a.ProductColorID == ProductColorID
                             select a).FirstOrDefault();

                    var n = (from u in db.Products.AsEnumerable()
                             join i in db.ProductColors on u.ProductID equals i.fk_ProductID
                             join c in db.Colors on i.fk_ColorID equals c.ColorID
                             where u.ProductID == o.fk_ProductID && u.IsDelete == false
                             && i.IsDelete == false && i.ProductColorID == ProductColorID &&
                             c.IsDelete == false
                             select new
                                 {
                                     Collection = (IdentifyCulture.cultureName().Contains("fa")) ? u.Collection :
                             ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_Collection :
                             ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_Collection :
                             ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_Collection : u.Collection))),
                                     u.DesignNo,
                                     ProductTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.ProductTitle :
                             ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_ProductTitle :
                             ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_ProductTitle :
                             ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_ProductTitle : u.ProductTitle))),
                                     u.Reeds,
                                     u.Shots,
                                     ColorTitle = (IdentifyCulture.cultureName().Contains("fa")) ? c.ColorTitle :
                             ((IdentifyCulture.cultureName().Contains("en")) ? c.ColorEN_Title :
                             ((IdentifyCulture.cultureName().Contains("ru")) ? c.Rus_ColorTitle :
                             ((IdentifyCulture.cultureName().Contains("zh")) ? c.China_ColorTitle : c.ColorTitle))),
                                     i.ProductImage,
                                     u.ThumbImage1,
                                     i.ProductColorID,
                                     u.ProDesc
                                 }).FirstOrDefault();

                    if (n != null)
                    {
                        lblCollection.Text = n.Collection;
                        lblDesignNo.Text = n.DesignNo.ToString();
                        lblProductTitle.Text = n.ProductTitle;
                        lblProductTitle2.Text = n.ProductTitle;
                        lblReeds.Text = n.Reeds;
                        lblShots.Text = n.Shots;
                        lblColorTitle.Text = n.ColorTitle;
                        if (string.IsNullOrEmpty(n.ProDesc))
                            pnlDesc.Visible = false;
                        else
                        {
                            pnlDesc.Visible = true;

                            ltProDesc.Text = n.ProDesc;
                        }

                        var m = (from a in db.ProductColorSizes
                                 where a.fk_ProductColorID == ProductColorID && a.IsDelete == false && a.IsAvailable == true
                                 orderby a.fk_SizeID descending
                                 select a).FirstOrDefault();


                        if (m != null)
                        {
                            if (m.Stock != 0)
                            {
                                if (CheckDiscountAndReturnDisconuntPercent(m.fk_SizeID) == 0)
                                {
                                    lblPrice.CssClass = "fontBRoya floatLeft marginright20 directionRTL  pricestyle";
                                    disablePrice(false);
                                    pnlNewPrice.Visible = false;
                                    string[] pricestring = (String.Format("{0:n}", m.ProductPrice)).Split('/');
                                    //   lblPrice.Text = pricestring[0] + "تومان";
                                    lblPrice.Text = (IdentifyCulture.cultureName().Contains("fa")) ? string.Format("{0:N0}", pricestring[0]) + "   تومان" :
                                      ((IdentifyCulture.cultureName().Contains("en")) ? string.Format("{0:N0}", pricestring[0]) + "   Toman" :
                                      ((IdentifyCulture.cultureName().Contains("ru")) ? string.Format("{0:N0}", pricestring[0]) + "   RS" :
                                      ((IdentifyCulture.cultureName().Contains("zh")) ? string.Format("{0:N0}", pricestring[0]) + "   RS" : string.Format("{0:N0}", pricestring[0]) + "   تومان")));
                                }
                                else
                                {
                                    disablePrice(false);
                                    //new price
                                    double percent = CheckDiscountAndReturnDisconuntPercent(m.fk_SizeID);

                                    double pricenew = CalculateNewPrice(percent, (decimal)m.ProductPrice);

                                    pnlNewPrice.Visible = true;
                                    string[] pricestring_New = (String.Format("{0:n}", pricenew)).Split('/');
                                    ltNewPrice.Text = (IdentifyCulture.cultureName().Contains("fa")) ? string.Format("{0:N0}", pricestring_New[0]) + "   تومان" :
                                      ((IdentifyCulture.cultureName().Contains("en")) ? string.Format("{0:N0}", pricestring_New[0]) + "   Toman" :
                                      ((IdentifyCulture.cultureName().Contains("ru")) ? string.Format("{0:N0}", pricestring_New[0]) + "   RS" :
                                      ((IdentifyCulture.cultureName().Contains("zh")) ? string.Format("{0:N0}", pricestring_New[0]) + "   RS" : string.Format("{0:N0}", pricestring_New[0]) + "   تومان")));
                                    lblPrice.CssClass = "fontBRoya floatLeft marginright20 directionRTL  pricestyle oldPrice";
                                    //old price
                                    string[] pricestring = (String.Format("{0:n}", m.ProductPrice)).Split('/');
                                    lblPrice.Text = (IdentifyCulture.cultureName().Contains("fa")) ? string.Format("{0:N0}", pricestring[0]) + "   تومان" :
                                     ((IdentifyCulture.cultureName().Contains("en")) ? string.Format("{0:N0}", pricestring[0]) + "   Toman" :
                                     ((IdentifyCulture.cultureName().Contains("ru")) ? string.Format("{0:N0}", pricestring[0]) + "   RS" :
                                     ((IdentifyCulture.cultureName().Contains("zh")) ? string.Format("{0:N0}", pricestring[0]) + "   RS" : string.Format("{0:N0}", pricestring[0]) + "   تومان")));

                                }
                            }
                            else
                            {
                                disablePrice(true);
                            }
                            ViewState["SizeID"] = m.fk_SizeID;
                        }
                    }
                    LoadImage(ProductColorID);


                }
            }
        }
        public double CalculateNewPrice(double DiscountPercent, decimal price)
        {
            double NewPercent = 100 - DiscountPercent;
            double pricenew = ((Convert.ToDouble(price) * NewPercent) / 100);

            return pricenew;
        }
        public double CheckDiscountAndReturnDisconuntPercent(int sizeid)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from a in db.Rel_Discounts_Sizes
                         where a.fk_SizeID == sizeid
                         select a).FirstOrDefault();
                if (n != null)
                {
                    var m = (from a in db.Discounts
                             where a.IsDelete == false && a.IsActive == true
                             && a.DiscountID == n.fk_DiscountID
                             select a).FirstOrDefault();
                    if(m!=null)
                    return Convert.ToDouble(m.DiscountPercent);
                    else
                    {
                        return 0;
                    }
                }
                else
                    return 0;
            }
        }
        public void LoadImage(Guid ProductColorID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var m = (from a in db.ProductColors
                         where a.ProductColorID == ProductColorID
                         select a);

                if (m != null)
                {
                    rptProductImg.DataSource = m.Take(1).ToList();
                    rptProductImg.DataBind();
                }
            }
        }
        public void rptRelatedProductsBind()
        {
            if (Request.QueryString["ColorID"] != null)
            {
                Guid ProductColorID = new Guid(Request.QueryString["ColorID"].ToString());
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    ProductColors n = db.ProductColors.Find(ProductColorID);

                    Products o =db.Products.Find(n.fk_ProductID);



                    if (!IdentifyCulture.cultureName().Contains("fa"))
                    {
                        var m = (from u in db.ProductColors
                                 join aa in db.Products
                                 on u.fk_ProductID equals aa.ProductID
                                 join aaa in db.ProductGroup on aa.fk_ProductGroupID equals aaa.ProductGroupID
                                 where u.fk_ProductID != n.fk_ProductID && aa.fk_ProductGroupID == o.fk_ProductGroupID
                                 && aa.IsDelete == false && aaa.IsDelete == false && u.IsDelete == false && aa.IsAlienCulture == true
                                 &&aa.IsActive==true && u.ProductImage!=null
                                 select new
                                 {
                                     u.ProductColorID,
                                     u.ProductImage,
                                     aa.ProductTitle,
                                     aaa.ProductGroupName,
                                     aa.ProductName
                                 }).ToList();

                        rptRelatedProducts.DataSource = m;
                        rptRelatedProducts.DataBind();
                    }
                    else
                    {
                        var m = (from u in db.ProductColors
                                 join aa in db.Products
                                 on u.fk_ProductID equals aa.ProductID
                                 join aaa in db.ProductGroup on aa.fk_ProductGroupID equals aaa.ProductGroupID
                                 where u.fk_ProductID != n.fk_ProductID && aa.fk_ProductGroupID == o.fk_ProductGroupID
                                 && aa.IsDelete == false && aaa.IsDelete == false && u.IsDelete == false && aa.IsAlienCulture != true
                                       && aa.IsActive == true && u.ProductImage != null
                                 select new
                                 {
                                     u.ProductColorID,
                                     u.ProductImage,
                                     aa.ProductTitle,
                                     aaa.ProductGroupName,
                                     aa.ProductName
                                 }).ToList();

                        rptRelatedProducts.DataSource = m;
                        rptRelatedProducts.DataBind();
                    }
                }
            }
        }
        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (Page.IsValid && ValidateStockCount())
            {
                if (Request.QueryString["ColorID"] != null)
                {
                    Guid ProductColorID = new Guid(Request.QueryString["ColorID"].ToString());

                    int SizeID = Convert.ToInt32(ViewState["SizeID"].ToString());

                    using (MashadCarpetEntities db = new MashadCarpetEntities())
                    {
                        var n = (from a in db.ProductColorSizes
                                 where a.fk_SizeID == SizeID && a.fk_ProductColorID == ProductColorID && a.IsAvailable == true
                                 select a).FirstOrDefault();

                        if (HttpContext.Current.User.Identity.IsAuthenticated)
                        {
                            Guid UserID = new Guid(HttpContext.Current.User.Identity.Name);

                            var order = (from a in db.Orders
                                         where a.fk_UserID == UserID && a.IsFinalized == false
                                         select a).FirstOrDefault();

                            if (order != null)
                            {
                                var orderDetail = (from a in db.OrderDetails
                                                   where a.fk_OrderID == order.OrderID &&
                                                   a.fk_ProductColorSizeID == n.ProductColorSizeID &&
                                                   a.IsDelete == false
                                                   select a).FirstOrDefault();

                                if (orderDetail != null)
                                {
                                    orderDetail.Count = orderDetail.Count + int.Parse(txtCount.Text);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    OrderDetails odEnter = new OrderDetails();
                                    odEnter.OrderDetailID = Guid.NewGuid();
                                    odEnter.fk_OrderID = order.OrderID;
                                    odEnter.fk_ProductColorSizeID = n.ProductColorSizeID;
                                    odEnter.Count = int.Parse(txtCount.Text);
                                    odEnter.IsDelete = false;

                                    db.OrderDetails.Add(odEnter);
                                    db.SaveChanges();
                                }
                            }
                            else
                            {
                                Orders oEnter = new Orders();
                                oEnter.OrderID = Guid.NewGuid();
                                oEnter.fk_UserID = UserID;
                                oEnter.SubmitDate = DateTime.Now;
                                oEnter.IsFinalized = false;
                                oEnter.IsPaid = false;
                                oEnter.IsDelete = false;
                                oEnter.CustomerOrderID = GenerateURL();

                                db.Orders.Add(oEnter);

                                OrderDetails odEnter2 = new OrderDetails();
                                odEnter2.OrderDetailID = Guid.NewGuid();
                                odEnter2.fk_OrderID = oEnter.OrderID;
                                odEnter2.fk_ProductColorSizeID = n.ProductColorSizeID;
                                odEnter2.Count = int.Parse(txtCount.Text);
                                odEnter2.IsDelete = false;
                                db.OrderDetails.Add(odEnter2);

                                db.SaveChanges();
                            }

                        }
                        else
                        {
                            string strPage = HttpContext.Current.Request.RawUrl;
                            Response.Redirect("/Login?RetUrl=" + strPage);
                        }

                        // var pc = (from a in db.ProductColors
                        //           where a.ProductColorID == ProductColorID
                        //           select a).FirstOrDefault();
                        //
                        // var p = (from a in db.Products
                        //          where a.ProductID == pc.fk_ProductID
                        //          select a).FirstOrDefault();
                        //
                        // var pg = (from a in db.ProductGroup
                        //           where a.ProductGroupID == p.fk_ProductGroupID
                        //           select a).FirstOrDefault();
                        //
                        // Response.Redirect("~/carpet-online-shopping/" + pg.ProductGroupName + "/" + p.ProductName + "/?ColorID=" + ProductColorID);

                    }



                    //      pnlSuccess.Visible = true;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "PageScriptfa",
           "$('#successDiv').css('display','block');", true);

         ScriptManager.RegisterStartupScript(this, GetType(), "scrolScriptfa",
           "$('html,body').animate({ 'scrollTop': 0 }, 1000);", true);
                 
            }
        }

        #region Color code
        public void rptColorsBind()
        {
            if (Request.QueryString["ColorID"] != null)
            {
                Guid ProductColorID = new Guid(Request.QueryString["ColorID"].ToString());

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var m = (from u in db.ProductColors where u.ProductColorID == ProductColorID select u).FirstOrDefault();

                    var n = (from u in db.ProductColors
                             where u.fk_ProductID == m.fk_ProductID && u.IsDelete == false
                             select u).ToList();

                    rptColors.DataSource = n;
                    rptColors.DataBind();
                }
            }

        }

        protected void rptColors_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (Request.QueryString["ColorID"] != null)
            {
                Guid ProductColorID = new Guid(Request.QueryString["ColorID"].ToString());
                int colorID = Convert.ToInt32(e.CommandArgument.ToString());
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from a in db.ProductColors
                             where a.ProductColorID == ProductColorID
                             select a).FirstOrDefault();

                    var p = (from a in db.Products
                             where a.ProductID == n.fk_ProductID
                             select a).FirstOrDefault();

                    var pg = (from a in db.ProductGroup
                              where a.ProductGroupID == p.fk_ProductGroupID
                              select a).FirstOrDefault();

                    var m = (from a in db.ProductColors
                             where a.fk_ProductID == n.fk_ProductID && a.fk_ColorID == colorID
                             select a).FirstOrDefault();

                    Response.Redirect("~/carpet-online-shopping/" + pg.ProductGroupName + "/" + p.ProductName + "/?ColorID=" + m.ProductColorID);

                    var c = (from a in db.Colors
                             where a.ColorID == colorID && a.IsDelete == false
                             select a).FirstOrDefault();
                    if (c != null)
                    {
                        lblColorTitle.Text = (IdentifyCulture.cultureName().Contains("fa")) ? c.ColorTitle :
                             ((IdentifyCulture.cultureName().Contains("en")) ? c.ColorEN_Title :
                             ((IdentifyCulture.cultureName().Contains("ru")) ? c.Rus_ColorTitle :
                             ((IdentifyCulture.cultureName().Contains("zh")) ? c.China_ColorTitle : c.ColorTitle)));
                    }
                }

            }
        }
        protected void rptColors_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (ViewState["ColorID"].ToString() != "nu")
            //{
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Guid ProductColorID = new Guid(Request.QueryString["ColorID"].ToString());

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var m = (from u in db.ProductColors where u.ProductColorID == ProductColorID select u).FirstOrDefault();

                    HiddenField hfColorID = (HiddenField)e.Item.FindControl("hfColorID");
                    LinkButton lbColor = (LinkButton)e.Item.FindControl("lbColor");
                    int ID = int.Parse(hfColorID.Value.ToString());
                    //  Image imgColors = (Image)e.Item.FindControl("imgColors");

                    if (m.fk_ColorID == ID)
                    {
                        lbColor.CssClass = "filter-color-box withoutSize withoutSizeActive";
                    }
                }
            }
        }



        #endregion

        #region Size code
        public void rptSizeBind()
        {
            if (Request.QueryString["ColorID"] != null)
            {
                Guid ProductColorID = new Guid(Request.QueryString["ColorID"].ToString());

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.ProductColors
                             join i in db.ProductColorSizes on u.ProductColorID equals i.fk_ProductColorID
                             join c in db.SIzes on i.fk_SizeID equals c.SizeID
                             where i.IsDelete == false && c.IsDelete == false &&
                             u.ProductColorID == ProductColorID
                             && c.IsDelete == false&&i.IsAvailable==true
                             select new
                             {
                                 c.SizeTitle,
                                 c.SizeID,
                                 i.ProductColorSizeID
                             }).ToList();

                    rptSize.DataSource = n;
                    rptSize.DataBind();
                }
            }
        }
        protected void rptSize_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (Request.QueryString["ColorID"] != null)
            {

                Guid ProductColorID = new Guid(Request.QueryString["ColorID"].ToString());

                int SizeID = int.Parse(e.CommandArgument.ToString());
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from a in db.ProductColorSizes
                             where a.fk_ProductColorID == ProductColorID && a.fk_SizeID == SizeID && a.IsAvailable == true
                             select a).FirstOrDefault();

                    if (n.Stock != 0)
                    {
                        if (CheckDiscountAndReturnDisconuntPercent(SizeID) == 0)
                        {
                            string[] pricestring = (String.Format("{0:N}", n.ProductPrice)).Split('/');
                            lblPrice.Text = pricestring[0] + "تومان";
                            pnlNewPrice.Visible = false;
                            disablePrice(false);
                            lblPrice.CssClass = "fontBRoya floatLeft marginright20 directionRTL  pricestyle";

                        }
                        else
                        {
                            disablePrice(false);
                            //new price
                            double percent = CheckDiscountAndReturnDisconuntPercent(SizeID);

                            double pricenew = CalculateNewPrice(percent, (decimal)n.ProductPrice);
                            pnlNewPrice.Visible = true;
                            string[] pricestring_New = (String.Format("{0:n}", pricenew)).Split('/');

                            ltNewPrice.Text = (IdentifyCulture.cultureName().Contains("fa")) ? string.Format("{0:N0}", pricestring_New[0]) + "   تومان" :
                              ((IdentifyCulture.cultureName().Contains("en")) ? string.Format("{0:N0}", pricestring_New[0]) + "   Toman" :
                              ((IdentifyCulture.cultureName().Contains("ru")) ? string.Format("{0:N0}", pricestring_New[0]) + "   RS" :
                              ((IdentifyCulture.cultureName().Contains("zh")) ? string.Format("{0:N0}", pricestring_New[0]) + "   RS" : string.Format("{0:N0}", pricestring_New[0]) + "   تومان")));

                            //old price
                            lblPrice.CssClass = "fontBRoya floatLeft marginright20 directionRTL  pricestyle oldPrice";
                            string[] pricestring = (String.Format("{0:n}", n.ProductPrice)).Split('/');
                            lblPrice.Text = (IdentifyCulture.cultureName().Contains("fa")) ? string.Format("{0:N0}", pricestring[0]) + "   تومان" :
                             ((IdentifyCulture.cultureName().Contains("en")) ? string.Format("{0:N0}", pricestring[0]) + "   Toman" :
                             ((IdentifyCulture.cultureName().Contains("ru")) ? string.Format("{0:N0}", pricestring[0]) + "   RS" :
                             ((IdentifyCulture.cultureName().Contains("zh")) ? string.Format("{0:N0}", pricestring[0]) + "   RS" : string.Format("{0:N0}", pricestring[0]) + "   تومان")));

                        }
                    }
                    else
                    {
                        disablePrice(true);
                    }
                    ViewState["SizeID"] = SizeID;
                    rptSizeBind();
                }
            }
        }
        protected void rptSize_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int viewstatesizeid = Convert.ToInt32(ViewState["SizeID"].ToString());
                HiddenField hfProductColorSizeID = (HiddenField)e.Item.FindControl("hfProductColorSizeID");
                LinkButton lbColor = (LinkButton)e.Item.FindControl("lbColor");
                Guid ID = new Guid(hfProductColorSizeID.Value.ToString());

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from a in db.ProductColorSizes
                             where a.ProductColorSizeID == ID&&a.IsAvailable==true
                             select a).FirstOrDefault();



                    if (viewstatesizeid == n.fk_SizeID)
                    {
                        lbColor.BackColor = System.Drawing.ColorTranslator.FromHtml("#CBC6B5");
                        lbColor.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                    }
                }



            }

        }


        #endregion

        #region Generate code
        public string GenerateURL()
        {
            while (true)
            {
                if (urlReturnRandom() == false)
                    urlReturnRandom();

                else
                    break;
            }
            int randomID = Convert.ToInt32(ViewState["randomNumber"]);
            int temp1 = randomID / 60466176;

            String result = Coding(temp1);
            int temp = randomID % 60466176;
            if (temp == 0)
                result = result + "00000";
            else
            {
                temp1 = temp / 1679616;
                result += Coding(temp1);
                temp %= 1679616;
                if (temp == 0)
                    result = result + "0000";
                else
                {
                    temp1 = temp / 46656;
                    result += Coding(temp1);
                    temp %= 46656;
                    if (temp == 0)
                        result = result + "000";
                    else
                    {
                        temp1 = temp / 1296;
                        result += Coding(temp1);
                        temp %= 1296;
                        if (temp == 0)
                            result = result + "00";
                        else
                        {
                            temp1 = temp / 36;
                            result += Coding(temp1);
                            temp %= 36;
                            if (temp == 0)
                                result = result + "0";
                            else
                            {
                                result += Coding(temp);
                                temp %= 1;
                                if (temp == 0) return result;
                            }
                        }
                    }
                }
            }
            return result;
        }
        public string Coding(int randomParam)
        {

            switch (randomParam)
            {

                case 0: return "0"; break;
                case 1: return "1"; break;
                case 2: return "2"; break;
                case 3: return "3"; break;
                case 4: return "4"; break;
                case 5: return "5"; break;
                case 6: return "6"; break;
                case 7: return "7"; break;
                case 8: return "8"; break;
                case 9: return "9"; break;
                case 10: return "h"; break;
                case 11: return "f"; break;
                case 12: return "m"; break;
                case 13: return "z"; break;
                case 14: return "q"; break;
                case 15: return "r"; break;
                case 16: return "b"; break;
                case 17: return "j"; break;
                case 18: return "g"; break;
                case 19: return "n"; break;
                case 20: return "a"; break;
                case 21: return "c"; break;
                case 22: return "s"; break;
                case 23: return "e"; break;
                case 24: return "w"; break;
                case 25: return "d"; break;
                case 26: return "v"; break;
                case 27: return "y"; break;
                case 28: return "i"; break;
                case 29: return "k"; break;
                case 30: return "o"; break;
                case 31: return "l"; break;
                case 32: return "p"; break;
                case 33: return "x"; break;
                case 34: return "t"; break;
                case 35: return "u"; break;
                default: return "0";

            }
        }
        public Boolean urlReturnRandom()
        {
            int maxInt = 2147483647;
            Random rnd = new Random();
            int randomInt = rnd.Next(1000, maxInt);

            Boolean ReturnRes = true;

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = from a in db.Orders
                        where a.IsPaid == false
                        select a;

                foreach (var i in n)
                {
                    if (i.CustomerOrderID == randomInt.ToString())
                    {
                        ReturnRes = false;
                        break;
                    }

                }

                ViewState["randomNumber"] = randomInt;
                return ReturnRes;

            }

        }

        #endregion

        public Boolean ValidateStockCount()
        {
            if (Request.QueryString["ColorID"] != null)
            {
                Guid ProductColorID = new Guid(Request.QueryString["ColorID"].ToString());

                int SizeID = int.Parse(ViewState["SizeID"].ToString());
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from a in db.ProductColorSizes
                             where a.fk_ProductColorID == ProductColorID && a.fk_SizeID == SizeID&&a.IsAvailable==true
                             select a).FirstOrDefault();

                    int customerCount = Convert.ToInt32(txtCount.Text);
                    if (n.Stock < customerCount)
                    {  
                        pnlError.Visible = true;
                        return false;                      
                    }
                    else
                    {
                        pnlError.Visible = false;
                        return true;
                    }
                }
            }
            else
                return false;
        }


        //protected void cvColor_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    if (ViewState["ColorID"].ToString() != "nu")
        //    {
        //        args.IsValid = true;
        //    }
        //    else
        //        args.IsValid = false;
        //}

        //protected void cvSize_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    if (ViewState["SizeID"].ToString() != "nu")
        //    {
        //        args.IsValid = true;
        //    }
        //    else
        //        args.IsValid = false;
        //}





    }
}