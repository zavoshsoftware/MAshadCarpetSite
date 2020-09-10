using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MashadCarpet.Models;

namespace MashadCarpet.Admin
{
    public partial class TicketSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadForm();
            }
        }

        public void LoadForm()
        {
            if (Request.QueryString["StatusID"] != null)
            {
                int StatusID = int.Parse(Request.QueryString["StatusID"].ToString());

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.Tickets
                             join i in db.Users.AsEnumerable()
                             on u.fk_UserID equals i.UserID
                             //join p in db.TicketGroup on u.fk_TicketGroupID equals p.TicketGroupID
                             where u.IsDelete == false && u.Status == StatusID
                             orderby u.TicketDate descending
                             select new
                             {
                                 UserName = i.UserName + " " + i.UserFamily,
                                 u.TicketSubject,
                                 //u.TicketPriority,
                                 u.TicketDate,
                                 u.TicketID,
                                 i.UserID,
                                 u.Status,
                                 u.fk_UserID,
                                 //p.TicketGroupTitle
                             }).ToList();

                    GrdTable.DataSource = n;
                    GrdTable.DataBind();
                    if (n.Count == 0)
                        pnlEmptyForm.Visible = true;
                    else
                        pnlEmptyForm.Visible = false;
                }
            }
            else
            {
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.Tickets
                             join i in db.Users.AsEnumerable()
                             on u.fk_UserID equals i.UserID
                             where u.IsDelete == false
                             orderby u.TicketDate descending
                             select new
                             {
                                 UserName = i.UserName + " " + i.UserFamily,
                                 u.TicketSubject,
                                 //u.TicketPriority,
                                 u.TicketDate,
                                 u.TicketID,
                                 i.UserID,
                                 u.Status,
                                 u.fk_UserID,
                                 //p.TicketGroupTitle
                             }).ToList();

                    GrdTable.DataSource = n;
                    GrdTable.DataBind();
                    if (n.Count == 0)
                        pnlEmptyForm.Visible = true;
                    else
                        pnlEmptyForm.Visible = false;
                }
            }
            mvSetting.SetActiveView(vwList);
        }

        public void DropDownBind()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var m = (from u in db.Users

                         select u).ToList();
                ddlUser.Items.Clear();
                ddlUser.Items.Add(new ListItem("انتخاب کاربر ", "-1"));
                foreach (var i in m)
                    ddlUser.Items.Add(new ListItem(i.UserName + " " + i.UserFamily, i.UserID.ToString()));


                //var n = (from u in db.TicketGroup

                //         select u).ToList();
                //ddlTicketGroup.Items.Clear();
                //ddlTicketGroup.Items.Add(new ListItem("گروه تیکت ", "-1"));
                //foreach (var r in n)
                //    ddlTicketGroup.Items.Add(new ListItem(r.TicketGroupTitle, r.TicketGroupID.ToString()));
            }
        }

        protected void GrdTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            Guid TicketID = new Guid(e.CommandArgument.ToString());

            ViewState["TicketID"] = TicketID;

            switch (e.CommandName)
            {
                case "DoEdit":
                    {
                        ViewState["btn"] = "Update";
                        DropDownBind();
                        FillViewEdit(TicketID);

                        mvSetting.SetActiveView(vwEdit);
                        break;
                    }

                case "DoDelete":
                    {
                        using (MashadCarpetEntities db = new MashadCarpetEntities())
                        {
                            var n = (from u in db.Tickets where u.TicketID == TicketID select u).FirstOrDefault();

                            lblDelete.Text = n.TicketSubject;

                        }
                        mvSetting.SetActiveView(vwDelete);

                        break;
                    }

                case "ShowAnswer":
                    {
                        Response.Redirect("TicketResponseSetting.aspx?TicketID=" + TicketID);
                        break;
                    }
                case "DoClose":
                    {
                        using (MashadCarpetEntities db = new MashadCarpetEntities())
                        {
                            var n = (from u in db.Tickets where u.TicketID == TicketID select u).FirstOrDefault();

                            n.Status = 4;
                            db.SaveChanges();
                        }
                        LoadForm();
                        break;

                    }
                case "UserInfo":
                    {
                        using (MashadCarpetEntities db = new MashadCarpetEntities())
                        {
                            var n = (from u in db.Tickets
                                     join i in db.Users on u.fk_UserID equals i.UserID
                                     join c in db.City on i.CityID equals c.CityID
                                     join p in db.Province on i.ProvinceID equals p.ProvinceID
                                     where u.TicketID == TicketID
                                     select new
                                         {i.UserName,
                                             i.UserFamily,
                                             i.Address1,
                                             i.Address2,
                                             p.ProvinceName,
                                             c.CityName,
                                             i.PostalCode,
                                             i.Phone,
                                             i.Mobile,
                                             i.Email
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
                        }
                        if (pnlUserInfo.Visible == true)
                            pnlUserInfo.Visible = false;
                        else
                            pnlUserInfo.Visible = true;
                        break;
                    }
            }
        }


        public void FillViewEdit(Guid TicketID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Tickets where u.TicketID == TicketID select u).FirstOrDefault();

                txtSubject.Text = n.TicketSubject;
                //ddlPriority.SelectedValue = n.TicketPriority.ToString();
                txtMessage.Text = n.TicketMessage;
                ddlUser.SelectedValue = n.fk_UserID.ToString();
                //ddlTicketGroup.SelectedValue = n.fk_TicketGroupID.ToString();


            }
        }


        protected void btnReturn_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
        }


        protected void btnYes_Click(object sender, EventArgs e)
        {
            Guid TicketID = new Guid(ViewState["TicketID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Tickets
                         where u.TicketID == TicketID
                         select u).FirstOrDefault();

                n.IsDelete = true;
                n.DeleteDate = DateTime.Now;
                db.SaveChanges();
            }
            LoadForm();
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
        }


        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (ViewState["btn"].ToString() == "Insert")
                    InserForm();
                else if (ViewState["btn"].ToString() == "Update")
                    UpdateForm();

                LoadForm();
            }
        }

        public void InserForm()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                Tickets p = new Tickets();

                p.TicketID = Guid.NewGuid();
                //p.fk_TicketGroupID = new Guid(ddlTicketGroup.SelectedValue);
                p.TicketSubject = txtSubject.Text;
                //p.TicketPriority = int.Parse(ddlPriority.SelectedValue);
                p.TicketMessage = txtMessage.Text;
                p.TicketDate = DateTime.Now;
                p.IsDelete = false;
                p.fk_UserID = new Guid(ddlUser.SelectedValue);
                if (Request.QueryString["StatusID"] != null)
                {
                    int StatusID = int.Parse(Request.QueryString["StatusID"].ToString());
                    p.Status = Convert.ToByte(StatusID);
                }
                else
                    p.Status = 1;

                db.Tickets.Add(p);
                db.SaveChanges();
            }
        }

        public void UpdateForm()
        {


            Guid TicketID = new Guid(ViewState["TicketID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.Tickets where u.TicketID == TicketID select u).FirstOrDefault();

                n.TicketSubject = txtSubject.Text;
                //n.TicketPriority = int.Parse(ddlPriority.SelectedValue);
                n.TicketMessage = txtMessage.Text;
                n.fk_UserID = new Guid(ddlUser.SelectedValue);



                db.SaveChanges();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ViewState["btn"] = "Insert";
            //ddlPriority.SelectedValue = "0";
            txtSubject.Text = string.Empty;
            txtMessage.Text = string.Empty;
            DropDownBind();

            mvSetting.SetActiveView(vwEdit);
        }

        protected void GrdTable_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow r in GrdTable.Rows)
            {
                HiddenField hfTicketID = (HiddenField)r.FindControl("hfTicketID");

                Guid TicketID = new Guid(hfTicketID.Value);

                Label lblStatus = (Label)r.FindControl("lblStatus");

                Label lblPriority = (Label)r.FindControl("lblPriority");

                Panel pnlClose = (Panel)r.FindControl("pnlClose");

                if (Request.QueryString["StatusID"] != null)
                {
                    int StatusID = int.Parse(Request.QueryString["StatusID"].ToString());

                    if (StatusID == 1)
                        pnlClose.Visible = true;
                }

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.Tickets where u.TicketID == TicketID select u).FirstOrDefault();
                    if (n.Status == 1)
                        lblStatus.Text = "درانتظار پاسخ";
                    else if (n.Status == 2)
                        lblStatus.Text = "پاسخ داده شده";
                    else if (n.Status == 3)
                        lblStatus.Text = "پاسخ مشتری";
                    else if (n.Status == 4)
                        lblStatus.Text = "بسته شده";


                    //if (n.Status == 0)
                    //    lblPriority.Text = "none";
                    //else if (n.Status == 1)
                    //    lblPriority.Text = "کم";
                    //else if (n.Status == 2)
                    //    lblPriority.Text = "متوسط";
                    //else if (n.Status == 3)
                    //    lblPriority.Text = "زیاد";

                }
            }
            //else
            //     lblStatus.Text = "درانتظار پاسخ";

        }



    }
}