using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MashadCarpet.Models;
using System.Web.Security;
using MashadCarpet.Classes;
using GSD.Globalization;
using System.Threading;

namespace MashadCarpet
{
    public partial class MainMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
       
            if(!Page.IsPostBack)
            {
                if (IdentifyCulture.cultureName().Contains("fa"))
                {
                    const string styleFormat =
                           "<LINK href=\"{0}\" type=\"text/css\" rel=\"stylesheet\">";

                    string linkText;
                    linkText = String.Format(styleFormat, StyleSheetPathFa);
                    StyleSheet.Text = linkText;        
                    imglogo.ImageUrl = "~/images/Logo2.png";
                  
                }
                else
                {
                    imglogo.ImageUrl = "~/images/Logo-eng.png";
                    const string styleFormat =
            "<LINK href=\"{0}\" type=\"text/css\" rel=\"stylesheet\">";

                    string linkText;
                    linkText = String.Format(styleFormat, StyleSheetPath);
                    StyleSheet.Text = linkText;   
                }
                OrderLoad();
                rptProductGroupBind();
                rptBuyingManualBind();
            }
            PaymentBtnAddress();
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
                        hlPayment2.NavigateUrl = "~/bank.aspx?OID=" + n.OrderID.ToString() + "&&UID=" + UserID.ToString();
                        pnlIsActiveFactor2.Visible = true;
                    }
                    else
                    {
                        pnlIsActiveFactor.Visible = false;
                        pnlIsActiveFactor2.Visible = false;

                    }
                }
            }
            else
            {
                pnlIsActiveFactor.Visible = false;
                pnlIsActiveFactor2.Visible = false;

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
                                 ProductGroupTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.ProductGroupTitle :
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
        //public void rptProductGroupBind()
        //{
        //    using (MashadCarpetEntities db = new MashadCarpetEntities())
        //    {
        //        var n = (from u in db.ProductGroup.AsEnumerable()
        //                 where u.IsDelete == false && u.fk_ProductGroupID == null
        //                 select new
        //                 {
        //                     u.ProductGroupID,
        //                     u.ProductGroupName,
        //                     ProductGroupTitle = (IdentifyCulture.cultureName().Contains("fa")) ? u.ProductGroupTitle :
        //                     ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_ProductGroupTitle :
        //                     ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_ProductGroupTitle :
        //                     ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_ProductGroupTitle : u.ProductGroupTitle))),
        //                 }).ToList().Take(3);
        //        rptProductGroup.DataSource = n;
        //        rptProductGroup.DataBind();
        //    }
        //}
        public void rptBuyingManualBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Texts.AsEnumerable()
                         where u.GroupID == 2 && u.IsDelete == false
                         select new
                         {
                             u.TextName,
                             TextDescription = (IdentifyCulture.cultureName().Contains("fa")) ? u.TextDescription :
                        ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextDescription :
                        ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextDescription :
                        ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextDescription : u.TextDescription))),
                             TextTitle=(IdentifyCulture.cultureName().Contains("fa")) ? u.TextTitle :
                             ((IdentifyCulture.cultureName().Contains("en")) ? u.EN_TextTitle :
                             ((IdentifyCulture.cultureName().Contains("ru")) ? u.Rus_TextTitle :
                             ((IdentifyCulture.cultureName().Contains("zh")) ? u.China_TextTitle : u.TextTitle))),
                         }).ToList();
                rptBuyingManual.DataSource = n;
                rptBuyingManual.DataBind();
            }
        }
        public void OrderLoad()
        {
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
                                 join pg in db.ProductGroup
                                 on p.fk_ProductGroupID equals pg.ProductGroupID
                                 
                                 where u.fk_OrderID == n.OrderID && i.IsDelete == false && u.IsDelete == false && p.IsDelete == false
                                 select new
                                 {
                                     p.ProductTitle,
                                     aa.ProductImage,
                                     u.Count,
                                     i.ProductPrice,
                                     p.ProductID,
                                     u.OrderDetailID,
                                     p.ProductName,
                                     aa.ProductColorID,
                                     pg.ProductGroupName
                                 }).ToList();

                        rptBasketItems.DataSource = m;
                        rptBasketItems.DataBind();

                        Repeater1.DataSource = m;
                        Repeater1.DataBind();


                        int price = 0;
                        foreach (var item in m)
                        {
                            for (int i = 0; i < item.Count; i++)
                            {
                                price = price + Convert.ToInt32(item.ProductPrice);
                            }

                        }

                        lblTotalPrice.Text = (IdentifyCulture.cultureName().Contains("fa")) ? string.Format("{0:N0}", price) + "   تومان" :
                              ((IdentifyCulture.cultureName().Contains("en")) ? string.Format("{0:N0}", price) + "   Toman" :
                              ((IdentifyCulture.cultureName().Contains("ru")) ? string.Format("{0:N0}", price) + "   RS" :
                              ((IdentifyCulture.cultureName().Contains("zh")) ? string.Format("{0:N0}", price) + "   RS" : string.Format("{0:N0}", price) + "   تومان")));

                        lblOrderTotal.Text = (IdentifyCulture.cultureName().Contains("fa")) ? string.Format("{0:N0}", price) + "   تومان" :
                              ((IdentifyCulture.cultureName().Contains("en")) ? string.Format("{0:N0}", price) + "   Toman" :
                              ((IdentifyCulture.cultureName().Contains("ru")) ? string.Format("{0:N0}", price) + "   RS" :
                              ((IdentifyCulture.cultureName().Contains("zh")) ? string.Format("{0:N0}", price) + "   RS" : string.Format("{0:N0}", price) + "   تومان")));

                        lblTotal.Text = (IdentifyCulture.cultureName().Contains("fa")) ? string.Format("{0:N0}", price) + "   تومان" :
                              ((IdentifyCulture.cultureName().Contains("en")) ? string.Format("{0:N0}", price) + "   Toman" :
                              ((IdentifyCulture.cultureName().Contains("ru")) ? string.Format("{0:N0}", price) + "   RS" :
                              ((IdentifyCulture.cultureName().Contains("zh")) ? string.Format("{0:N0}", price) + "   RS" : string.Format("{0:N0}", price) + "   تومان")));

                        lblOrderCount2.Text = m.Count().ToString();
                        lblOrderCount.Text = m.Count().ToString();
                        lblCo.Text = m.Count().ToString();
                        
                        Label4.Text = lblTotalPrice.Text;
                        Label5.Text = lblTotalPrice.Text;
                        if(m.Count==0)
                        {
                            lblTotal.Text = "0";
                            lblOrderCount2.Text = "0";
                            lblOrderCount.Text = "0";
                            lblCo.Text = "0";
                            
                        }
                    }
                    else
                    {
                        lblTotal.Text = "0";
                        lblOrderCount2.Text = "0";
                        lblOrderCount.Text = "0";
                        lblCo.Text = "0";
                       
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

        //protected void rptProductGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        HiddenField hfProductGroupID = (HiddenField)e.Item.FindControl("hfProductGroupID");
        //        Guid ProductGroupID = new Guid(hfProductGroupID.Value.ToString());

        //        Repeater rptProduct = (Repeater)e.Item.FindControl("rptProduct");

        //        using(MashadCarpetEntities db=new MashadCarpetEntities())
        //        {
        //            var n = (from u in db.Products where u.IsDelete == false && u.fk_ProductGroupID == ProductGroupID select u).Take(5).ToList();

        //            rptProduct.DataSource = n;
        //            rptProduct.DataBind();
        //        }
        //    }
        //}

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
                    //lblPrice.Text = string.Format("{0:N0}", (m.Count * (m.ProductPrice))) + " تومان";
                    lblPrice.Text=(IdentifyCulture.cultureName().Contains("fa")) ? string.Format("{0:N0}", (m.Count * (m.ProductPrice))) + "   تومان" :
                              ((IdentifyCulture.cultureName().Contains("en")) ? string.Format("{0:N0}", (m.Count * (m.ProductPrice))) + "   Toman" :
                              ((IdentifyCulture.cultureName().Contains("ru")) ? string.Format("{0:N0}", (m.Count * (m.ProductPrice))) + "   RS" :
                              ((IdentifyCulture.cultureName().Contains("zh")) ? string.Format("{0:N0}", (m.Count * (m.ProductPrice))) + "   RS" : string.Format("{0:N0}", (m.Count * (m.ProductPrice))) + "   تومان")));
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

        protected void lbLogin_Click(object sender, EventArgs e)
        {
            string strPage = HttpContext.Current.Request.RawUrl;
            Response.Redirect("/login?RetUrl=" + strPage);
        }
        protected void lbExit_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SearchResult.aspx?Text=" + txtSearch.Text);
        }
        protected void lbEnglish_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("CurrentLanguage");
            cookie.Value = "en-US";
            Response.SetCookie(cookie);
            Response.Redirect(Request.RawUrl);
        }

        protected void lbFa_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("CurrentLanguage");
            cookie.Value = "fa-IR";
            Response.SetCookie(cookie);
            Response.Redirect(Request.RawUrl);
        }

        protected void lbChine_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("CurrentLanguage");
            cookie.Value = "zh-CN";
            Response.SetCookie(cookie);
            Response.Redirect(Request.RawUrl);
        }

        protected void lbRus_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("CurrentLanguage");
            cookie.Value = "ru-RU";
            Response.SetCookie(cookie);
            Response.Redirect(Request.RawUrl);
        }
    }
}