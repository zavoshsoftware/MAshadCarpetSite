using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MashadCarpet.Models;
using System.IO;

namespace MashadCarpet.Admin
{
    public partial class OrderListSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadForm(null);
            }
        }

        public void ddlOrderStatusBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var y = (from u in db.OrderStatus
                         where u.IsDelete == false
                         select u).ToList();

                ddlOrderStatus.Items.Clear();
                ddlOrderStatus.Items.Add(new ListItem("وضعیت سفارش ", "-1"));
                foreach (var i in y)
                    ddlOrderStatus.Items.Add(new ListItem(i.OrderStatusTitle, i.OrderStatusID.ToString()));

            }
        }
        public void LoadForm(string search)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                if (search == null)
                {
                    if (Request.QueryString["Status"] != null)
                    {
                        string status = Request.QueryString["Status"].ToString();
                        if (status == "pay")
                        {
                            var n = (from u in db.Orders
                                join i in db.Users.AsEnumerable() on u.fk_UserID equals i.UserID
                                where u.IsDelete == false && i.IsDelete == false && u.IsPaid == true
                                orderby u.PaymentDate descending
                                select new
                                {
                                    u.OrderID,
                                    u.SubmitDate,
                                    u.IsFinalized,
                                    u.IsPaid,
                                    i.UserName,
                                    u.PaymentWay,
                                    u.CustomerOrderID,
                                    u.OrderShowID,
                                    u.PaymentDate
                                }).ToList();

                            grdTable.DataSource = n;
                            grdTable.DataBind();
                            if (n.Count == 0)
                                pnlEmptyForm.Visible = true;
                            else
                                pnlEmptyForm.Visible = false;
                        }
                        else if (status == "final")
                        {
                            var n = (from u in db.Orders
                                join i in db.Users.AsEnumerable() on u.fk_UserID equals i.UserID
                                where u.IsDelete == false && i.IsDelete == false && u.IsPaid == false
                                      && u.IsFinalized == true
                                orderby u.SubmitDate descending
                                select new
                                {
                                    u.OrderID,
                                    u.SubmitDate,
                                    u.IsFinalized,
                                    u.IsPaid,
                                    i.UserName,
                                    u.PaymentWay,
                                    u.CustomerOrderID,
                                    u.OrderShowID,
                                    u.PaymentDate
                                }).ToList();

                            grdTable.DataSource = n;
                            grdTable.DataBind();
                            if (n.Count == 0)
                                pnlEmptyForm.Visible = true;
                            else
                                pnlEmptyForm.Visible = false;
                        }
                        else if (status == "notfinal")
                        {
                            var n = (from u in db.Orders
                                join i in db.Users.AsEnumerable() on u.fk_UserID equals i.UserID
                                where u.IsDelete == false && i.IsDelete == false && u.IsFinalized == false
                                orderby u.SubmitDate descending
                                select new
                                {
                                    u.OrderID,
                                    u.SubmitDate,
                                    u.IsFinalized,
                                    u.IsPaid,
                                    i.UserName,
                                    u.PaymentWay,
                                    u.CustomerOrderID,
                                    u.OrderShowID,
                                    u.PaymentDate
                                }).ToList();

                            grdTable.DataSource = n;
                            grdTable.DataBind();
                            if (n.Count == 0)
                                pnlEmptyForm.Visible = true;
                            else
                                pnlEmptyForm.Visible = false;
                        }
                        else if (status == "notchecked")
                        {
                            var n = (from u in db.Orders
                                join i in db.Users.AsEnumerable() on u.fk_UserID equals i.UserID
                                where u.IsDelete == false && i.IsDelete == false &&
                                      (u.IsNewOrder == true || u.IsNewOrder == null)
                                orderby u.SubmitDate descending
                                select new
                                {
                                    u.OrderID,
                                    u.SubmitDate,
                                    u.IsFinalized,
                                    u.IsPaid,
                                    i.UserName,
                                    u.PaymentWay,
                                    u.CustomerOrderID,
                                    u.OrderShowID,
                                    u.PaymentDate
                                }).ToList();

                            grdTable.DataSource = n;
                            grdTable.DataBind();
                            if (n.Count == 0)
                                pnlEmptyForm.Visible = true;
                            else
                                pnlEmptyForm.Visible = false;
                        }
                    }
                    else
                    {
                        var n = (from u in db.Orders
                            join i in db.Users.AsEnumerable() on u.fk_UserID equals i.UserID
                            where u.IsDelete == false && i.IsDelete == false
                            orderby u.SubmitDate descending
                            select new
                            {
                                u.OrderID,
                                u.SubmitDate,
                                u.IsFinalized,
                                u.IsPaid,
                                i.UserName,
                                u.PaymentWay,
                                u.CustomerOrderID,
                                u.OrderShowID,
                                u.PaymentDate
                            }).ToList();

                        grdTable.DataSource = n;
                        grdTable.DataBind();
                        if (n.Count == 0)
                            pnlEmptyForm.Visible = true;
                        else
                            pnlEmptyForm.Visible = false;
                    }
                }
                else
                {
                    int orderShowId = Convert.ToInt32(search);
                    if (Request.QueryString["Status"] != null)
                    {
                        string status = Request.QueryString["Status"].ToString();
                        if (status == "pay")
                        {
                            var n = (from u in db.Orders
                                     join i in db.Users.AsEnumerable() on u.fk_UserID equals i.UserID
                                     where u.IsDelete == false && i.IsDelete == false && u.IsPaid == true
                                     &&u.OrderShowID== orderShowId
                                     orderby u.SubmitDate descending
                                     select new
                                     {
                                         u.OrderID,
                                         u.SubmitDate,
                                         u.IsFinalized,
                                         u.IsPaid,
                                         i.UserName,
                                         u.PaymentWay,
                                         u.CustomerOrderID,
                                         u.OrderShowID,
                                         u.PaymentDate
                                     }).ToList();

                            grdTable.DataSource = n;
                            grdTable.DataBind();
                            if (n.Count == 0)
                                pnlEmptyForm.Visible = true;
                            else
                                pnlEmptyForm.Visible = false;
                        }
                        else if (status == "final")
                        {
                            var n = (from u in db.Orders
                                     join i in db.Users.AsEnumerable() on u.fk_UserID equals i.UserID
                                     where u.IsDelete == false && i.IsDelete == false && u.IsPaid == false
                                           && u.IsFinalized == true
                                           && u.OrderShowID == orderShowId
                                orderby u.SubmitDate descending
                                     select new
                                     {
                                         u.OrderID,
                                         u.SubmitDate,
                                         u.IsFinalized,
                                         u.IsPaid,
                                         i.UserName,
                                         u.PaymentWay,
                                         u.CustomerOrderID,
                                         u.OrderShowID,
                                         u.PaymentDate
                                     }).ToList();

                            grdTable.DataSource = n;
                            grdTable.DataBind();
                            if (n.Count == 0)
                                pnlEmptyForm.Visible = true;
                            else
                                pnlEmptyForm.Visible = false;
                        }
                        else if (status == "notfinal")
                        {
                            var n = (from u in db.Orders
                                     join i in db.Users.AsEnumerable() on u.fk_UserID equals i.UserID
                                     where u.IsDelete == false && i.IsDelete == false && u.IsFinalized == false
                                           && u.OrderShowID == orderShowId
                                orderby u.SubmitDate descending
                                     select new
                                     {
                                         u.OrderID,
                                         u.SubmitDate,
                                         u.IsFinalized,
                                         u.IsPaid,
                                         i.UserName,
                                         u.PaymentWay,
                                         u.CustomerOrderID,
                                         u.OrderShowID,
                                         u.PaymentDate
                                     }).ToList();

                            grdTable.DataSource = n;
                            grdTable.DataBind();
                            if (n.Count == 0)
                                pnlEmptyForm.Visible = true;
                            else
                                pnlEmptyForm.Visible = false;
                        }
                        else if (status == "notchecked")
                        {
                            var n = (from u in db.Orders
                                     join i in db.Users.AsEnumerable() on u.fk_UserID equals i.UserID
                                     where u.IsDelete == false && i.IsDelete == false &&
                                           (u.IsNewOrder == true || u.IsNewOrder == null)
                                           && u.OrderShowID == orderShowId
                                orderby u.SubmitDate descending
                                     select new
                                     {
                                         u.OrderID,
                                         u.SubmitDate,
                                         u.IsFinalized,
                                         u.IsPaid,
                                         i.UserName,
                                         u.PaymentWay,
                                         u.CustomerOrderID,
                                         u.OrderShowID,
                                         u.PaymentDate
                                     }).ToList();

                            grdTable.DataSource = n;
                            grdTable.DataBind();
                            if (n.Count == 0)
                                pnlEmptyForm.Visible = true;
                            else
                                pnlEmptyForm.Visible = false;
                        }
                    }
                    else
                    {
                        var n = (from u in db.Orders
                                 join i in db.Users.AsEnumerable() on u.fk_UserID equals i.UserID
                                 where u.IsDelete == false && i.IsDelete == false
                                       && u.OrderShowID == orderShowId
                            orderby u.SubmitDate descending
                                 select new
                                 {
                                     u.OrderID,
                                     u.SubmitDate,
                                     u.IsFinalized,
                                     u.IsPaid,
                                     i.UserName,
                                     u.PaymentWay,
                                     u.CustomerOrderID,
                                     u.OrderShowID,
                                     u.PaymentDate
                                 }).ToList();

                        grdTable.DataSource = n;
                        grdTable.DataBind();
                        if (n.Count == 0)
                            pnlEmptyForm.Visible = true;
                        else
                            pnlEmptyForm.Visible = false;
                    }
                }
            }
            mvSetting.SetActiveView(vwList);
        }

        protected void grdTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName != "Page")
            {
                Guid OrderID = new Guid(e.CommandArgument.ToString());
                ViewState["OrderID"] = OrderID;
                if (e.CommandName == "Show")
                {
                    FindOrderDetails(OrderID);
                }

                else if (e.CommandName == "DoEdit")
                {
                    using (MashadCarpetEntities db = new MashadCarpetEntities())
                    {
                        var n = (from u in db.Orders where u.OrderID == OrderID select u).FirstOrDefault();

                        cbISFinalize.Checked = Convert.ToBoolean(n.IsFinalized);
                        cbIsPaid.Checked = Convert.ToBoolean(n.IsPaid);
                        txtRecipientAddress.Text = n.RecipientAddress;
                        txtRecipientName.Text = n.RecipientName;
                        txtRecipientPhone.Text = n.RecipientPhone;
                        if (n.PaymentWay != null)
                            ddlPaymentWay.SelectedValue = n.PaymentWay.ToString();
                        ddlOrderStatusBind();
                        if (n.fk_OrderStatusID != null)
                            ddlOrderStatus.SelectedValue = n.fk_OrderStatusID.ToString();
                        mvSetting.SetActiveView(vwEditOrder);
                    }
                }
                else if (e.CommandName == "DoCheck")
                {
                    using (MashadCarpetEntities db = new MashadCarpetEntities())
                    {
                        Orders orders = db.Orders.FirstOrDefault(current => current.OrderID == OrderID);
                        if (orders != null)
                        {
                            orders.IsNewOrder = false;
                            db.SaveChanges();
                        }
                    }
                    Response.Redirect("~/Admin/OrderListSetting.aspx?Status=notchecked");
                }
                else if (e.CommandName == "downloadFactor")
                {
                    Response.Redirect("/GeneratePDFPage.aspx?orderId=" + OrderID);
                    //using (MashadCarpetEntities db = new MashadCarpetEntities())
                    //{
                    //    var n = (from a in db.Orders
                    //             where a.OrderID == OrderID
                    //             select a).FirstOrDefault();

                    //    string FilePath = Server.MapPath("~/Uploads/Factors/" + n.CustomerOrderID + ".pdf");
                    //    if (File.Exists(FilePath))
                    //    {
                    //        //  Response.Redirect(FilePath);
                    //        string path = (FilePath); //get physical file path from server
                    //        string name = Path.GetFileName(path); //get file name


                    //        string type = "Application/pdf";
                    //        Response.AppendHeader("content-disposition", "attachment; filename=" + name);

                    //        if (type != "")
                    //            Response.ContentType = type;
                    //        Response.WriteFile(path);

                    //        Response.End(); //give POP to user for file downlaod 
                    //    }
                    //}
                }
                else if (e.CommandName == "DoDelete")
                {
                    mvSetting.SetActiveView(vwDeleteOrder);

                }

                else if (e.CommandName == "UserInfo")
                {
                    using (MashadCarpetEntities db = new MashadCarpetEntities())
                    {
                        var n = (from u in db.Orders
                                 join i in db.Users on u.fk_UserID equals i.UserID
                                 join p in db.Province on i.ProvinceID equals p.ProvinceID
                                 join c in db.City on i.CityID equals c.CityID
                                 where u.OrderID == OrderID
                                 select new
                                 {
                                     u.RecipientAddress,
                                     u.RecipientName,
                                     u.RecipientPhone,
                                     i.UserName,
                                     i.UserFamily,
                                     i.Address1,
                                     i.Address2,
                                     i.Province,
                                     i.City,
                                     i.PostalCode,
                                     i.Phone,
                                     i.Email,
                                     i.Mobile,
                                     c.CityName,
                                     p.ProvinceName
                                 }).FirstOrDefault();
                        lblName.Text = n.UserName;
                        lblFamily.Text = n.UserFamily;
                        lblAddress1.Text = n.Address1;
                        lblAddress2.Text = n.Address2;
                        lblProvince.Text = n.ProvinceName;
                        lblCity.Text = n.CityName;
                        lblPostalcode.Text = n.PostalCode;
                        lblPhone.Text = n.Phone;
                        lblMobile.Text = n.Mobile;
                        lblEmail.Text = n.Email;
                        lblRecipientAddress.Text = n.RecipientAddress;
                        lblRecipientName.Text = n.RecipientName;
                        lblRecipientPhone.Text = n.RecipientPhone;
                    }
                    if (pnlUserInfo.Visible == true)
                        pnlUserInfo.Visible = false;
                    else
                        pnlUserInfo.Visible = true;

                }
            }
        }

        public void FindOrderDetails(Guid OrderID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {

                var n = (from u in db.OrderDetails
                         join i in db.ProductColorSizes
                                      on u.fk_ProductColorSizeID equals i.ProductColorSizeID
                         join aa in db.ProductColors on i.fk_ProductColorID equals aa.ProductColorID
                         join p in db.Products on aa.fk_ProductID equals p.ProductID
                         join c in db.Colors on aa.fk_ColorID equals c.ColorID
                         join s in db.SIzes on i.fk_SizeID equals s.SizeID
                         where u.IsDelete == false && u.fk_OrderID == OrderID && i.IsDelete == false && p.IsDelete == false && c.IsDelete == false && s.IsDelete == false
                         select new
                         {
                             u.Count,
                             p.ProductTitle,
                             u.OrderDetailID,
                             c.ColorTitle,
                             i.ProductPrice,
                             s.SizeTitle,

                         }).ToList();


                grdDetails.DataSource = n;
                grdDetails.DataBind();
                if (n.Count == 0)
                    pnlEmptyForm.Visible = true;
                var m = (from u in db.Orders
                         join i in db.Users on u.fk_UserID equals i.UserID
                         where u.OrderID == OrderID
                         select new
                         {
                             u.OrderID,
                             u.SubmitDate,
                             u.IsFinalized,
                             u.IsPaid,
                             UserName = i.UserName + " " + i.UserFamily

                         }).FirstOrDefault();
                lblUserName.Text = "نام کاربر: " + m.UserName;
                lblDate.Text = "تاریخ: " + string.Format("{0:d}", m.SubmitDate);
                mvSetting.SetActiveView(vwOrder);
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Guid OrderID = new Guid(ViewState["OrderID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Orders where u.OrderID == OrderID select u).FirstOrDefault();

                n.IsPaid = cbIsPaid.Checked;
                n.IsFinalized = cbISFinalize.Checked;
                n.RecipientPhone = txtRecipientPhone.Text;
                n.RecipientName = txtRecipientName.Text;
                n.RecipientAddress = txtRecipientAddress.Text;
                n.PaymentWay = int.Parse(ddlPaymentWay.SelectedValue);
                n.fk_OrderStatusID = int.Parse(ddlOrderStatus.SelectedValue);
                db.SaveChanges();
            }
            LoadForm(null);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            LoadForm(null);
        }

        protected void btnAgree_Click(object sender, EventArgs e)
        {
            Guid OrderID = new Guid(ViewState["OrderID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Orders where u.OrderID == OrderID select u).FirstOrDefault();

                n.IsDelete = true;
                n.DeleteDate = DateTime.Now;

                db.SaveChanges();
            }
            LoadForm(null);
        }

        protected void btnDeny_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
        }

        protected void grdDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid OrderDetailID = new Guid(e.CommandArgument.ToString());
            ViewState["OrderDetailID"] = OrderDetailID;

            if (e.CommandName == "DoEdit")
            {
                ViewState["btn"] = "update";
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    OrderDetails orderDetails = db.OrderDetails
                        .FirstOrDefault(current => current.OrderDetailID == OrderDetailID && current.IsDelete == false);

                    //var n = (from u in db.OrderDetails
                    //         join i in db.ProductColorSizes
                    //               on u.fk_ProductColorSizeID equals i.ProductColorSizeID
                    //         join aa in db.ProductColors on i.fk_ProductColorID equals aa.ProductColorID
                    //         where u.OrderDetailID == OrderDetailID && i.IsDelete == false
                    //         select
                    //             new
                    //             {
                    //                 u.Count,
                    //                 aa.fk_ColorID,
                    //                 aa.fk_ProductID,
                    //                 i.fk_SizeID
                    //             }).FirstOrDefault();

                    ddlProductBind();
                    Guid ProductID = orderDetails.ProductColorSizes.ProductColors.fk_ProductID;
                    ddlProducts.SelectedValue = ProductID.ToString();
                    txtCount.Text = orderDetails.Count.ToString();
                    int selectedColorId = orderDetails.ProductColorSizes.ProductColors.fk_ColorID;
                    FillDDLsUpdate(ProductID, selectedColorId);
                    ddlColors.SelectedValue = selectedColorId.ToString();
                    ddlSize.SelectedValue =orderDetails.ProductColorSizes.fk_SizeID.ToString();

                    mvSetting.SetActiveView(vwEditDetails);
                }
            }

            else if (e.CommandName == "DoDelete")
            {
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.OrderDetails
                             join i in db.ProductColorSizes
                               on u.fk_ProductColorSizeID equals i.ProductColorSizeID
                             join aa in db.ProductColors on i.fk_ProductColorID equals aa.ProductColorID
                             join p in db.Products on aa.fk_ProductID equals p.ProductID
                             select p).FirstOrDefault();
                    lblDelete.Text = n.ProductTitle;

                    mvSetting.SetActiveView(vwDeleteDetails);

                }
            }

        }

        public void FillDDLsUpdate(Guid productId,int selectedColorId)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                List<ProductColors> productColors =
                    db.ProductColors.Where(current => current.fk_ProductID == productId).ToList();

                ddlColors.Items.Clear();
                ddlColors.Items.Add(new ListItem("رنگ ", "-1"));
                foreach (var i in productColors)
                    ddlColors.Items.Add(new ListItem(i.Colors.ColorTitle, i.Colors.ColorID.ToString()));

                ProductColors selectedProductColor =
                    productColors.Where(current => current.fk_ColorID == selectedColorId).FirstOrDefault();

                List<ProductColorSizes> productColorSizes = db.ProductColorSizes
                    .Where(current => current.fk_ProductColorID == selectedProductColor.ProductColorID).ToList();


                ddlSize.Items.Clear();
                ddlSize.Items.Add(new ListItem("سایز ", "-1"));
                foreach (var i in productColorSizes)
                    ddlSize.Items.Add(new ListItem(i.SIzes.SizeTitle, i.SIzes.SizeID.ToString()));
                
            }
        }
        public void FillDDLs()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var y = (from u in db.Colors
                         where u.IsDelete == false
                         select u).ToList();

                ddlColors.Items.Clear();
                ddlColors.Items.Add(new ListItem("رنگ ", "-1"));
                foreach (var i in y)
                    ddlColors.Items.Add(new ListItem(i.ColorTitle, i.ColorID.ToString()));


                var t = (from u in db.SIzes where u.IsDelete == false select u).ToList();

                ddlSize.Items.Clear();
                ddlSize.Items.Add(new ListItem("سایز ", "-1"));
                foreach (var i in t)
                    ddlSize.Items.Add(new ListItem(i.SizeTitle, i.SizeID.ToString()));
            }
        }
        public void ddlProductBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var m = (from u in db.Products
                         where u.IsDelete == false
                         select u).ToList();
                ddlProducts.Items.Clear();
                ddlProducts.Items.Add(new ListItem("انتخاب محصول ", "-1"));
                foreach (var i in m)
                    ddlProducts.Items.Add(new ListItem(i.ProductTitle, i.ProductID.ToString()));
            }
        }


        protected void btnSaveDetails_Click(object sender, EventArgs e)
        {
            if (ViewState["btn"].ToString() == "update")
                update();

            else if (ViewState["btn"].ToString() == "Insert")
                Insert();
            Guid ID = new Guid(ViewState["OrderID"].ToString());
            FindOrderDetails(ID);
        }

        public void Insert()
        {
            //using (MashadCarpetEntities db = new MashadCarpetEntities())
            //{
            //    Guid ProductID = new Guid(ddlProducts.SelectedValue.ToString());
            //    int ColorID = int.Parse(ddlColors.SelectedValue.ToString());
            //    int SizeID = int.Parse(ddlSize.SelectedValue.ToString());

            //    var n = (from u in db.Rel_Product_Color_Size where u.fk_ProductID == ProductID && 
            //                 u.fk_ColorID == ColorID && u.fk_SizeID == SizeID select u).FirstOrDefault();


            //    OrderDetails od = new OrderDetails();
            //    od.OrderDetailID = Guid.NewGuid();
            //    od.fk_OrderID = new Guid(ViewState["OrderID"].ToString().ToString());
            //    od.fk_ProductColorSizeID = n.Rel_Product_Color_Size_ID;
            //    od.Count = int.Parse(txtCount.Text);
            //    od.IsDelete = false;

            //    db.OrderDetails.Add(od);
            //    db.SaveChanges();
            //}
        }


        public void update()
        {
            //Guid DetailID = new Guid(ViewState["OrderDetailID"].ToString());
            //Guid ProductID = new Guid(ddlProducts.SelectedValue.ToString());
            //int ColorID = int.Parse(ddlColors.SelectedValue.ToString());
            //int SizeID = int.Parse(ddlSize.SelectedValue.ToString());


            //using (MashadCarpetEntities db = new MashadCarpetEntities())
            //{
            //    var m = (from u in db.Rel_Product_Color_Size where u.fk_ProductID == ProductID && u.fk_ColorID == ColorID && u.fk_SizeID == SizeID select u).FirstOrDefault();
            //    var n = (from u in db.OrderDetails where u.OrderDetailID == DetailID select u).FirstOrDefault();

            //    n.Count = int.Parse(txtCount.Text);
            //    n.fk_Rel_Product_Color_size_ID = m.Rel_Product_Color_Size_ID;

            //    db.SaveChanges();
            //    Guid ID = new Guid(ViewState["OrderID"].ToString());
            //    FindOrderDetails(ID);

            //}
        }

        protected void btnCancelDetails_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwOrder);
        }

        protected void btnDelDetails_Click(object sender, EventArgs e)
        {
            Guid DetailID = new Guid(ViewState["OrderDetailID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.OrderDetails where u.OrderDetailID == DetailID select u).FirstOrDefault();

                n.IsDelete = true;
                n.DeleteDate = DateTime.Now;
                db.SaveChanges();

                Guid ID = new Guid(ViewState["OrderID"].ToString());
                FindOrderDetails(ID);
            }
        }

        protected void btnDisAgreeDel_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwOrder);

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ViewState["btn"] = "Insert";
            ddlProductBind();
            ddlProducts.SelectedValue = "-1";
            txtCount.Text = string.Empty;
            FillDDLs();
            ddlOrderStatusBind();
            ddlColors.SelectedValue = "-1";
            ddlSize.SelectedValue = "-1";
            mvSetting.SetActiveView(vwEditDetails);
        }

        //protected void grdTable_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    foreach (GridViewRow r in grdTable.Rows)
        //    {
        //        HiddenField hfOrderID = (HiddenField)r.FindControl("hfOrderID");
        //        Guid OrderID = new Guid(hfOrderID.Value.ToString());
        //        // Label lblOrderStatus = (Label)r.FindControl("lblOrderStatus");
        //        Label lblPaymentWay = (Label)r.FindControl("lblPaymentWay");
        //        LinkButton lbDownloadFactors = (LinkButton)r.FindControl("lbDownloadFactors");

        //        using (MashadCarpetEntities db = new MashadCarpetEntities())
        //        {
        //            var n = (from u in db.Orders where u.OrderID == OrderID select u).FirstOrDefault();

        //            if (n.PaymentWay == 1)
        //                lblPaymentWay.Text = "پرداخت آنلاین";
        //            else if (n.PaymentWay == 2)
        //                lblPaymentWay.Text = "پرداخت از طریق بانک";

        //            if (n.IsFinalized == false)
        //            {
        //                lbDownloadFactors.Visible = false;
        //            }
        //            else
        //                lbDownloadFactors.Visible = true;
        //            //  var m = (from u in db.OrderStatus where u.OrderStatusID == n.fk_OrderStatusID select u).FirstOrDefault();
        //            //  if (m != null)
        //            //      lblOrderStatus.Text = m.OrderStatusTitle;
        //        }
        //    }
        //}

        protected void ddlProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid productId = new Guid(ddlProducts.SelectedValue);
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                List<ProductColors> productColors =
                    db.ProductColors.Where(current => current.fk_ProductID == productId && current.IsDelete == false).ToList();

                ddlColors.Items.Clear();
                ddlColors.Items.Add(new ListItem("رنگ ", "-1"));
                foreach (var i in productColors)
                    ddlColors.Items.Add(new ListItem(i.Colors.ColorTitle, i.Colors.ColorID.ToString()));
                ddlSize.Items.Clear();
               
            }
        }

        protected void ddlColors_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int colorId = Convert.ToInt32(ddlColors.SelectedValue);
            Guid productId = new Guid(ddlProducts.SelectedValue);

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                ProductColors productColors = db.ProductColors.Where(current =>
                    current.fk_ColorID == colorId && current.fk_ProductID == productId
                    && current.IsDelete == false).FirstOrDefault();

                List<ProductColorSizes> productColorSizes = db.ProductColorSizes.Where(current =>
                    current.fk_ProductColorID == productColors.ProductColorID && current.IsDelete == false).ToList();

                ddlSize.Items.Clear();
                ddlSize.Items.Add(new ListItem("سایز ", "-1"));
                foreach (var i in productColorSizes)
                    ddlSize.Items.Add(new ListItem(i.SIzes.SizeTitle.ToString(), i.SIzes.SizeID.ToString()));
            }
        }

        protected void btnUserInfo_Click(object sender, EventArgs e)
        {
            Guid OrderID = new Guid(ViewState["OrderID"].ToString());
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Orders
                         join i in db.Users on u.fk_UserID equals i.UserID
                         join p in db.Province on i.ProvinceID equals p.ProvinceID
                         join c in db.City on i.CityID equals c.CityID
                         where u.OrderID == OrderID
                         select new
                             {
                                 u.RecipientAddress,
                                 u.RecipientName,
                                 u.RecipientPhone,
                                 i.UserName,
                                 i.UserFamily,
                                 i.Address1,
                                 i.Address2,
                                 i.Province,
                                 i.City,
                                 i.PostalCode,
                                 i.Phone,
                                 i.Email,
                                 i.Mobile,
                                 c.CityID,
                                 p.ProvinceID,
                                 c.CityName,
                                 p.ProvinceName

                             }).FirstOrDefault();
                lblName.Text = n.UserName;
                lblFamily.Text = n.UserFamily;
                lblAddress1.Text = n.Address1;
                lblAddress2.Text = n.Address2;
                lblProvince.Text = n.ProvinceName;
                lblCity.Text = n.CityName;
                lblPostalcode.Text = n.PostalCode;
                lblPhone.Text = n.Phone;
                lblMobile.Text = n.Mobile;
                lblEmail.Text = n.Email;
                lblRecipientAddress.Text = n.RecipientAddress;
                lblRecipientName.Text = n.RecipientName;
                lblRecipientPhone.Text = n.RecipientPhone;

            }
            if (pnlUserInfo.Visible == true)
                pnlUserInfo.Visible = false;
            else
                pnlUserInfo.Visible = true;
        }

        protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Nullable<double> TotalPrice = 0;
            foreach (GridViewRow r in grdDetails.Rows)
            {
                HiddenField hfOrderDetailID = (HiddenField)r.FindControl("hfOrderDetailID");

                Literal ltPrice = (Literal)r.FindControl("ltPrice");
                Literal ltTotalPrice = (Literal)r.FindControl("ltTotalPrice");
                Literal ltPriceBeforDescount = (Literal)r.FindControl("ltPriceBeforDescount");

                Guid OrderDetailID = new Guid(hfOrderDetailID.Value);
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from a in db.OrderDetails
                             where a.OrderDetailID == OrderDetailID
                             && a.IsDelete == false
                             select a).FirstOrDefault();

                    if (n != null)
                    {
                        var m = (from a in db.ProductColorSizes
                                 where a.ProductColorSizeID == n.fk_ProductColorSizeID
                                 && a.IsDelete == false
                                 select a).FirstOrDefault();
                        if (m != null)
                        {
                            var o = (from a in db.Rel_Discounts_Sizes
                                     join aa in db.Discounts
                                     on a.fk_DiscountID equals aa.DiscountID
                                     where a.fk_SizeID == m.fk_SizeID && aa.IsActive == true && aa.IsDelete == false
                                     select new { aa.DiscountPercent }).FirstOrDefault();

                            if (o != null)
                            {
                                ltPriceBeforDescount.Text = returnPurePrice((double)m.ProductPrice);

                                double pricenew = CalculateNewPrice((double)o.DiscountPercent, (decimal)m.ProductPrice);

                                ltPrice.Text = returnPurePrice(pricenew);

                                ltTotalPrice.Text = returnPurePrice((double)(pricenew * n.Count));

                                TotalPrice = TotalPrice + (pricenew * n.Count);
                            }
                            else
                            {

                                ltPrice.Text = returnPurePrice((double)m.ProductPrice);
                                ltPriceBeforDescount.Text = returnPurePrice((double)m.ProductPrice);

                                ltTotalPrice.Text = returnPurePrice((double)(m.ProductPrice * n.Count));
                                TotalPrice = TotalPrice + (double)(m.ProductPrice * n.Count);
                            }
                        }
                    }
                }
            }
        }
        public string returnPurePrice(double Price)
        {
            string[] priceParts = Price.ToString().Split('/');
            return priceParts[0];
        }
        public double CalculateNewPrice(double DiscountPercent, decimal price)
        {
            double NewPercent = 100 - DiscountPercent;
            double pricenew = ((Convert.ToDouble(price) * NewPercent) / 100);

            return pricenew;
        }

        protected void grdTable_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdTable.PageIndex = e.NewPageIndex;
            LoadForm(null);
            pnlUserInfo.Visible = false;
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            string search = txtSearch.Text;
            LoadForm(search);
        }
    }
}