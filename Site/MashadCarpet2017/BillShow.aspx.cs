using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MashadCarpet.Models;
using System.Web.UI.HtmlControls;
using MashadCarpet.Classes;
using GSD.Globalization;
using System.Threading;
using System.IO;
using System.Web.Configuration;

namespace MashadCarpet
{
    public partial class BillShow : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IdentifyCulture.cultureName().Contains("fa"))
            {
                var persianCulture = new PersianCulture();
                Thread.CurrentThread.CurrentCulture = persianCulture;
                Thread.CurrentThread.CurrentUICulture = persianCulture;
            }
            if (!Page.IsPostBack)
            {
                rptOrdersBind();

                if (Request.QueryString["Fail"] != null)
                {
                    pnlError.Visible = true;
                }
                else
                    pnlError.Visible = false;

                if (Request.QueryString["Confirm"] != null)
                {
                    pnlSuccess.Visible = true;
                    lblSaleRefID.Text = Request.QueryString["Confirm"];
                }
                else
                    pnlSuccess.Visible = false;
            }
        }

        public void rptOrdersBind()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Guid UserID = new Guid(HttpContext.Current.User.Identity.Name);

            

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    //Services.SmsService sms = new Services.SmsService();
                    //List<string> list = new List<string>();
                    //string mobile = db.Users.Find(UserID).Mobile;
                    //if (!string.IsNullOrEmpty(mobile))
                    //{
                    //    list.Add(mobile);
                    //    sms.Send(list, "مشتری گرامی سفارش شما با موفقیت ثبت گردید. کارشناسان ما با شما تماس خواهند گرفت.", "");
                    //}

                    //list.Clear();
                    //string number = WebConfigurationManager.AppSettings["CompanyNumber"];
                    //list.Add(number);
                    //sms.Send(list, "سفارش جدیدی در وب سایت ثبت گردید.", "");

                    var n = (from u in db.Orders where u.fk_UserID == UserID && u.IsDelete == false && u.IsFinalized == true orderby u.SubmitDate descending select u).ToList();
                    rptOrders.DataSource = n;
                    rptOrders.DataBind();
                }
            }
        }

        protected void rptOrders_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hfOrderID = (HiddenField)e.Item.FindControl("hfOrderID");

                Guid OrderID = new Guid(hfOrderID.Value.ToString());

                Label lblPrice = (Label)e.Item.FindControl("lblPrice");

                HyperLink hlPayment = (HyperLink)e.Item.FindControl("hlPayment");

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.OrderDetails
                             join i in db.ProductColorSizes
                                      on u.fk_ProductColorSizeID equals i.ProductColorSizeID
                             where u.fk_OrderID == OrderID && u.IsDelete == false && i.IsDelete == false
                             select new
                                 {
                                     u.Count,
                                     i.ProductPrice
                                 }).ToList();
                    int price = 0;
                    foreach (var item in n)
                    {
                        for (int i = 0; i < item.Count; i++)
                        {
                            price = price + Convert.ToInt32(item.ProductPrice);
                        }

                    }
                    lblPrice.Text = string.Format("{0:N0}", price) + "   تومان";


                    var m = (from u in db.Orders where u.OrderID == OrderID select u).FirstOrDefault();

                    hlPayment.NavigateUrl = "/Bank.aspx?OID=" + OrderID + "&&UID=" + HttpContext.Current.User.Identity.Name;

                    if (m.IsPaid == true)
                        hlPayment.Visible = false;

                    else
                        hlPayment.Visible = true;


                }
            }
        }


        //public void rptOrderDetailsBind(Guid OrderID)
        //{

        //    using (MashadCarpetEntities db = new MashadCarpetEntities())
        //    {



        //        var m = (from u in db.OrderDetails
        //                 join i in db.Rel_Product_Color_Size
        //                     on u.fk_Rel_Product_Color_size_ID equals i.Rel_Product_Color_Size_ID
        //                 join p in db.Products on i.fk_ProductID equals p.ProductID
        //                 join c in db.Colors on i.fk_ColorID equals c.ColorID
        //                 join s in db.SIzes on i.fk_SizeID equals s.SizeID
        //                 where u.fk_OrderID == OrderID && i.IsDelete == false && u.IsDelete == false && p.IsDelete == false
        //                 select new
        //                 {
        //                     p.ProductTitle,
        //                     i.ProductImage,
        //                     u.Count,
        //                     i.ProductPrice,
        //                     p.ProductID,
        //                     u.OrderDetailID,
        //                     c.ColorTitle,
        //                     s.SizeTitle,
        //                     p.ProductName
        //                 }).ToList();

        //        rptOrderDetails.DataSource = m;
        //        rptOrderDetails.DataBind();




        //    }
        //    if (pnlOrderDetails.Visible == false)
        //        pnlOrderDetails.Visible = true;
        //    else
        //        pnlOrderDetails.Visible = false;
        //}

        protected void rptOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "factorDetail")
            {
                Guid OrderID = new Guid(e.CommandArgument.ToString());

                //HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("row");
                //tr.Attributes.Add("style", "background-color:#999999;color:#FFFFFF;");

                Panel pnlOrderDetails = e.Item.FindControl("pnlOrderDetails") as Panel;
                Repeater rptOrderDetails = e.Item.FindControl("rptOrderDetails") as Repeater;

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var m = (from u in db.OrderDetails
                             join i in db.ProductColorSizes
                           on u.fk_ProductColorSizeID equals i.ProductColorSizeID
                             join aa in db.ProductColors on i.fk_ProductColorID equals aa.ProductColorID
                             join p in db.Products on aa.fk_ProductID equals p.ProductID
                             join c in db.Colors on aa.fk_ColorID equals c.ColorID
                             join s in db.SIzes on i.fk_SizeID equals s.SizeID
                             join pg in db.ProductGroup
                             on p.fk_ProductGroupID equals pg.ProductGroupID

                             where u.fk_OrderID == OrderID && i.IsDelete == false && u.IsDelete == false && p.IsDelete == false
                             select new
                             {
                                 p.ProductTitle,
                                 aa.ProductImage,
                                 u.Count,
                                 pg.ProductGroupName,
                                 aa.ProductColorID,
                                 i.ProductPrice,
                                 p.ProductID,
                                 u.OrderDetailID,
                                 c.ColorTitle,
                                 s.SizeTitle,
                                 p.ProductName
                             }).ToList();

                    rptOrderDetails.DataSource = m;
                    rptOrderDetails.DataBind();

                }
                if (pnlOrderDetails.Visible == false)
                    pnlOrderDetails.Visible = true;
                else
                    pnlOrderDetails.Visible = false;

                HtmlTableRow newRow = e.Item.FindControl("row") as HtmlTableRow;
                if (newRow.BgColor == "#BAB49B")
                    newRow.BgColor = "#FBFAF4";
                else
                    newRow.BgColor = "#BAB49B";
                //rptOrderDetailsBind(OrderID);
            }
            else if (e.CommandName == "factorDL")
            {
                Guid OrderID = new Guid(e.CommandArgument.ToString());
                Response.Redirect("/GeneratePDFPage.aspx?orderId=" + OrderID);
                //Guid OrderID = new Guid(e.CommandArgument.ToString());

                //using (MashadCarpetEntities db = new MashadCarpetEntities())
                //{
                //    var n = (from a in db.Orders
                //             where a.OrderID == OrderID
                //             select a).FirstOrDefault();

                //    string FilePath = Server.MapPath("~/Uploads/Factors/" + n.CustomerOrderID + ".pdf");
                //    if (File.Exists(FilePath))
                //    {
                //      //  Response.Redirect(FilePath);


                //        string path = (FilePath); //get physical file path from server
                //        string name = Path.GetFileName(path); //get file name


                //         string type = "Application/pdf";
                //        Response.AppendHeader("content-disposition", "attachment; filename=" + name);

                //        if (type != "")
                //            Response.ContentType = type;
                //        Response.WriteFile(path);

                //        Response.End(); //give POP to user for file downlaod 
                //    }  
                //}
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
                    lblTotalPrice.Text = (IdentifyCulture.cultureName().Contains("fa")) ? string.Format("{0:N0}", (n.Count * (n.ProductPrice))) + "   تومان" :
                             ((IdentifyCulture.cultureName().Contains("en")) ? string.Format("{0:N0}", (n.Count * (n.ProductPrice))) + "   Toman" :
                             ((IdentifyCulture.cultureName().Contains("ru")) ? string.Format("{0:N0}", (n.Count * (n.ProductPrice))) + "   RS" :
                             ((IdentifyCulture.cultureName().Contains("zh")) ? string.Format("{0:N0}", (n.Count * (n.ProductPrice))) + "   RS" : string.Format("{0:N0}", (n.Count * (n.ProductPrice))) + "   تومان")));
                }
            }
        }
    }
}