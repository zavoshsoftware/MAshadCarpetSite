using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MashadCarpet.Models;

namespace MashadCarpet
{
    public partial class Track : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string strPage = HttpContext.Current.Request.RawUrl;
                Response.Redirect("/login?RetUrl=" + strPage);
            }
        }

        protected void btnTracking_Click(object sender, EventArgs e)
        {
            FindUser();
            pnlTrack.Visible = true;
            pnlCode.Visible = false;

        }

        public void FindUser()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Guid UserID = new Guid(HttpContext.Current.User.Identity.Name);
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.Users where u.UserID == UserID select u).FirstOrDefault();

                    lblEmail.Text = n.Email;
                    lblPhone.Text = n.Phone;
                    lblUserName.Text = n.UserName + " " + n.UserFamily;
                    lblAddress1.Text = n.Address1;
                    lblAddress2.Text = n.Address2;
                    lblPostalCode.Text = n.PostalCode;

                    var m = (from u in db.Orders where u.fk_UserID == UserID && u.CustomerOrderID == txtTrackCode.Text select u).FirstOrDefault();
                    if (m.RecipientAddress != null || m.RecipientAddress == "")
                    {
                        lblRecipientAddress.Text = m.RecipientAddress;
                        lblRecipientName.Text = m.RecipientName;
                        lblRecipientPhone.Text = m.RecipientPhone;
                    }
                    else
                    {
                        lblRecipientAddress.Text = n.Address1;
                        lblRecipientName.Text = n.UserName + " " + n.UserFamily;
                        lblRecipientPhone.Text = n.Phone;
                    }



                    var t = (from u in db.OrderDetails
                             join i in db.ProductColorSizes
                                      on u.fk_ProductColorSizeID equals i.ProductColorSizeID
                             join aa in db.ProductColors on i.fk_ProductColorID equals aa.ProductColorID
                             join p in db.Products on aa.fk_ProductID equals p.ProductID
                             join c in db.Colors on aa.fk_ColorID equals c.ColorID
                             join s in db.SIzes on i.fk_SizeID equals s.SizeID
                             where u.fk_OrderID == m.OrderID && i.IsDelete == false && u.IsDelete == false && p.IsDelete == false
                             select new
                             {
                                 p.ProductTitle,
                                 aa.ProductImage,
                                 u.Count,
                                 i.ProductPrice,
                                 p.ProductID,
                                 u.OrderDetailID,
                                 c.ColorTitle,
                                 s.SizeTitle,
                                 p.ProductName
                             }).ToList();

                    rptOrderDetails.DataSource = t;
                    rptOrderDetails.DataBind();


                    var v= (from u in db.Orders join i in db.OrderStatus on u.fk_OrderStatusID equals i.OrderStatusID where u.OrderID==m.OrderID select i).FirstOrDefault();
                    lblOrderStatus.Text = v.OrderStatusTitle;

                }
            }
        }

        protected void rptOrderDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Guid UserID = new Guid(HttpContext.Current.User.Identity.Name);

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.Orders where u.fk_UserID == UserID && u.CustomerOrderID == txtTrackCode.Text select u).FirstOrDefault();

                    if (n == null)
                        args.IsValid = false;
                    else
                        args.IsValid = true;
                }
            }
        }


    }
}