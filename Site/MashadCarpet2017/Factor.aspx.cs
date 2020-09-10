using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MashadCarpet.Models;

namespace MashadCarpet
{
    public partial class Factor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                FindOrder();
                RptOrdersBind();
            }
        }

        public void FindOrder()
        {
            if (Page.RouteData.Values["OrderID"] != null)
            {
                Guid OrderID = new Guid(Page.RouteData.Values["OrderID"].ToString());
                using(MashadCarpetEntities db=new MashadCarpetEntities())
                {
                    var n = (from u in db.Orders where u.OrderID == OrderID select u).FirstOrDefault();

                    lblOrderDate.Text = string.Format("{0:d}", n.SubmitDate);
                    customerOrderID.Text = n.CustomerOrderID.ToString();
                    lblRecipientAddress.Text = n.RecipientAddress;
                    lblRecipientName.Text = n.RecipientName;
                    lblRecipientPhone.Text = n.RecipientPhone;
                    lblOrderDesc.Text = n.OrderDesc;
                    FindUser(n.fk_UserID.ToString());
                }
            }
        }

        public void FindUser(string UserID)
        {
            Guid Userid = new Guid(UserID);
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Users where u.UserID == Userid select u).FirstOrDefault();
                lblUserName.Text = n.UserName + " " + n.UserFamily;
                lblPhone.Text = n.Phone;
                lblMobile.Text = n.Mobile;
                lblEmail.Text = n.Email;
                lblAddress.Text = n.Address1;

                
            }

        }
        public void RptOrdersBind()
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
                        //hlOrder.NavigateUrl = "/Bill/"+n.OrderID;
                        var m = (from u in db.OrderDetails
                                 join i in db.ProductColorSizes
                                     on u.fk_ProductColorSizeID equals i.ProductColorSizeID
                                 join aa in db.ProductColors on i.fk_ProductColorID equals aa.ProductColorID
                                 join p in db.Products on aa.fk_ProductID equals p.ProductID
                                 join c in db.Colors on aa.fk_ColorID equals c.ColorID
                                 join s in db.SIzes on i.fk_SizeID equals s.SizeID
                                 where u.fk_OrderID == n.OrderID && i.IsDelete == false && u.IsDelete == false && p.IsDelete == false
                                 select new
                                 {
                                     p.ProductTitle,
                                     p.Collection,
                                     p.Reeds,
                                     p.Shots,
                                    aa.ProductImage,
                                     u.Count,
                                     i.ProductPrice,
                                     p.ProductID,
                                     u.OrderDetailID,
                                     c.ColorTitle,
                                     s.SizeTitle,
                                     p.ProductName
                                 }).ToList();

                        rptOrders.DataSource = m;
                        rptOrders.DataBind();


                        decimal price = 0;
                        foreach (var item in m)
                        {
                            price = Convert.ToDecimal(item.Count * (item.ProductPrice));
                        }

                        //lblTotalPrice.Text = string.Format("{0:N0}", price) + " تومان";

                        //if (m.Count == 0)
                        //{
                        //    pnlOrderDetails.Visible = false;
                        //    pnlEmpry.Visible = true;
                        //}
                    }
                    else
                    {
                        pnlOrderDetails.Visible = false;
                        //pnlEmpry.Visible = true;
                    }

                }
            }
        }

        protected void rptOrders_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hfOrderDetailID = (HiddenField)e.Item.FindControl("hfOrderDetailID");
                Guid OrderDetailID = new Guid(hfOrderDetailID.Value.ToString());

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.OrderDetails
                             join i in db.ProductColorSizes
                                    on u.fk_ProductColorSizeID equals i.ProductColorSizeID
                             where u.OrderDetailID == OrderDetailID
                             select new
                             {
                                 u.Count,
                                 i.ProductPrice
                             }).FirstOrDefault();
                    Label lblTotalPrice = (Label)e.Item.FindControl("lblTotalPrice");
                    lblTotalPrice.Text = string.Format("{0:N0}", (n.Count * (n.ProductPrice))) + " تومان";
                }
            }
        }
         
    }
}