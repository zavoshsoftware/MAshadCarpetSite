using MashadCarpet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MashadCarpet.Admin
{
    public partial class SliderTextSetting : System.Web.UI.Page
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
            if (Request.QueryString["ID"] != null)
            {
                int SliderID = Convert.ToInt32(Request.QueryString["ID"]);

                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.SliderTexts
                             where u.fk_SliderID == SliderID
                             select u).ToList();

                    grdTable.DataSource = n;
                    grdTable.DataBind();
                    if (n.Count == 0)
                        pnlEmptyForm.Visible = true;
                    else
                        pnlEmptyForm.Visible = false;
                }
            }
            mvSetting.SetActiveView(vwList);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            mvSetting.SetActiveView(vwEdit);
            ViewState["btn"] = "Insert";
            ResetForm();
        }


        public void ResetForm()
        {
            txtX.Text= string.Empty;
            txtY.Text = string.Empty;
            txtFont.Text = string.Empty;
            //  txtLinkAddress.Text = string.Empty;
            txtSpeed.Text = string.Empty;
            txtStart.Text = string.Empty;
            txtText.Text = string.Empty;
            ddlEnterSide.SelectedValue = "0";
            ddlEnterSpeed.SelectedValue = "0";
            txtColor.Text = string.Empty;
            ddlExitSpeed.SelectedValue = "0";
            ddlExitSide.SelectedValue = "0";
            txtChina_Text.Text = string.Empty;
            txtEN_Text.Text = string.Empty;
            txtRus_Text.Text = string.Empty;
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
            pnlSuccess.Visible = false;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (ViewState["btn"].ToString() == "Update")
                    UpdateForm();
                else if (ViewState["btn"].ToString() == "Insert")
                    InsertForm();

                LoadForm();
                pnlSuccess.Visible = true;
            }
        }
        public void UpdateForm()
        {

            int SliderTextID = Convert.ToInt32(ViewState["SliderID"].ToString());
            int SliderID = Convert.ToInt32(Request.QueryString["ID"]);

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var p = (from u in db.SliderTexts where u.SliderTextID == SliderTextID select u).FirstOrDefault();

                string color = p.textColor;
                //string PerStyle = ".sliderTextColorClass{color:#" + color + " !important;}";
                //string NextStyle = ".sliderTextColorClass{color:#" + txtColor.Text + " !important;}";

                //string path = Server.MapPath("~/css/SliderText.css");
                //string text = File.ReadAllText(path);
                //text = text.Replace(PerStyle, NextStyle);
                //File.WriteAllText(path, text);

                string path = Server.MapPath("~/css/SliderText.css");
                string text = File.ReadAllText(path);
                string ClassName = ".class" + p.SliderTextID.ToString() + "{color:#" + color + " !important;}";

                string NewClass = ".class" + p.SliderTextID.ToString() + "{color:#" + txtColor.Text + " !important;}";
                text = text.Replace(ClassName, NewClass);

                File.WriteAllText(path, text);
                 
                p.datax = txtX.Text;
                p.datay = txtY.Text;

                p.fk_SliderID = SliderID;

                p.InAndOutClass = retrunEnter(ddlEnterSide.SelectedValue, ddlEnterSpeed.SelectedValue) + " " + retrunExit(ddlExitSide.SelectedValue, ddlExitSpeed.SelectedValue);
                //     p.IsLink = chkIsLink.Checked;
                //     if (chkIsLink.Checked == true)
                //     {
                //         p.LinkAddress = txtLinkAddress.Text;
                //     }
                p.speed = txtSpeed.Text;
                p.start = txtStart.Text;
                p.Text = txtText.Text;
                p.textColor = txtColor.Text;
                p.EN_Text = txtEN_Text.Text;
                p.Rus_Text = txtRus_Text.Text;
                p.China_Text = txtChina_Text.Text;
                p.fontsize =Convert.ToInt32(txtFont.Text);



                db.SaveChanges();
            }
        }

        public string retrunEnter(string side, string speed)
        {
            if (side == "1" && speed == "1")
            {
                return "sft";
            }
            else if (side == "1" && speed == "2")
            {
                return "lft";
            }
            else if (side == "2" && speed == "1")
            {
                return "sfr";
            }
            else if (side == "2" && speed == "2")
            {
                return "lfr";
            }
            else if (side == "3" && speed == "1")
            {
                return "sfl";
            }
            else if (side == "3" && speed == "2")
            {
                return "lfl";
            }
            else if (side == "4" && speed == "1")
            {
                return "sfb";
            }
            else if (side == "4" && speed == "2")
            {
                return "lfb";
            }
            else
            {
                return "";
            }
        }
        public string retrunExit(string side, string speed)
        {
            if (side == "1" && speed == "1")
            {
                return "stt";
            }
            else if (side == "1" && speed == "2")
            {
                return "ltt";
            }
            else if (side == "2" && speed == "1")
            {
                return "str";
            }
            else if (side == "2" && speed == "2")
            {
                return "ltr";
            }
            else if (side == "3" && speed == "1")
            {
                return "stl";
            }
            else if (side == "3" && speed == "2")
            {
                return "ltl";
            }
            else if (side == "4" && speed == "1")
            {
                return "stb";
            }
            else if (side == "4" && speed == "2")
            {
                return "ltb";
            }
            else
            {
                return "";
            }
        }



        //public string retrunX(string place)
        //{
        //    if (place == "1")
        //    {
        //        return "838";
        //    }
        //    else if (place == "2")
        //    {
        //        return "center";
        //    }
        //    else if (place == "3")
        //    {
        //        return "176";
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}
        //public string retrunY(string place)
        //{
        //    if (place == "1")
        //    {
        //        return "165";
        //    }
        //    else if (place == "2")
        //    {
        //        return "220";
        //    }
        //    else if (place == "3")
        //    {
        //        return "295";
        //    }
        //    else if (place == "4")
        //    {
        //        return "393";
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}
        public void InsertForm()
        {
            int SliderID = Convert.ToInt32(Request.QueryString["ID"]);

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                
     

                SliderTexts p = new SliderTexts();




                p.datax = txtX.Text;
                p.datay = txtY.Text;
                p.fk_SliderID = SliderID;
                //    p.IsLink = chkIsLink.Checked;
                //    if (chkIsLink.Checked == true)
                //    {
                //        p.LinkAddress = txtLinkAddress.Text;
                //    }
                p.InAndOutClass = retrunEnter(ddlEnterSide.SelectedValue, ddlEnterSpeed.SelectedValue) + " " + retrunExit(ddlExitSide.SelectedValue, ddlExitSpeed.SelectedValue);

                p.speed = txtSpeed.Text;
                p.start = txtStart.Text;
                p.Text = txtText.Text;
                p.textColor = txtColor.Text;
                p.EN_Text = txtEN_Text.Text;
                p.Rus_Text = txtRus_Text.Text;
                p.China_Text = txtChina_Text.Text;
                p.fontsize = Convert.ToInt32(txtFont.Text);


                db.SliderTexts.Add(p);
                db.SaveChanges();
                
                string path = Server.MapPath("~/css/SliderText.css");
                string text = File.ReadAllText(path);


                string NewClass = ".class" + p.SliderTextID.ToString() + "{color:#" + txtColor.Text + " !important;}";


                File.WriteAllText(path, text + NewClass);


            }
        }



        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
        protected void btnYes_Click(object sender, EventArgs e)
        {
            int SliderTextID = Convert.ToInt32(ViewState["SliderID"].ToString());

            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.SliderTexts where u.SliderTextID == SliderTextID select u).FirstOrDefault();

                db.SliderTexts.Remove(n);

                db.SaveChanges();
            }
            LoadForm();
            pnlSuccess.Visible = true;
        }
        protected void btnNo_Click(object sender, EventArgs e)
        {
            mvSetting.SetActiveView(vwList);
            pnlSuccess.Visible = false;
        }

        protected void grdTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int SliderTextID = Convert.ToInt32(e.CommandArgument.ToString());
            ViewState["SliderID"] = SliderTextID;

            switch (e.CommandName)
            {
                case "DoEdit":
                    {
                        ViewState["btn"] = "Update";
                        mvSetting.SetActiveView(vwEdit);
                        FillViewEdit(SliderTextID);

                        break;
                    }
                case "DoDelete":
                    {
                        using (MashadCarpetEntities db = new MashadCarpetEntities())
                        {
                            var n = (from u in db.SliderTexts where u.SliderTextID == SliderTextID select u).FirstOrDefault();
                            lblDelete.Text = n.Text;
                            mvSetting.SetActiveView(vwDelete);
                        }
                        break;
                    }
            }
        }

        public void FillViewEdit(int SliderTextID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from u in db.SliderTexts where u.SliderTextID == SliderTextID select u).FirstOrDefault();

                //    txtLinkAddress.Text = n.LinkAddress;
                txtSpeed.Text = n.speed;
                txtStart.Text = n.start;
                txtText.Text = n.Text;
                txtChina_Text.Text = n.China_Text;
                txtRus_Text.Text = n.Rus_Text;
                txtEN_Text.Text = n.EN_Text;
                //    chkIsLink.Checked = n.IsLink;
                txtColor.Text = n.textColor;
                txtFont.Text = n.fontsize.ToString();

                txtX.Text = n.datax;
                //if (n.datax == "center")
                //    ddlPlace.SelectedValue = "2";
                //else if (n.datax == "176")
                //    ddlPlace.SelectedValue = "3";
                //else if (n.datax == "838")
                //    ddlPlace.SelectedValue = "1";


                txtY.Text = n.datay;

                //if (n.datay == "165")
                //    ddlY.SelectedValue = "1";
                //else if (n.datay == "220")
                //    ddlY.SelectedValue = "2";

                //else if (n.datay == "295")
                //    ddlY.SelectedValue = "3";

                //else if (n.datay == "393")
                //    ddlY.SelectedValue = "4";

                if (n.InAndOutClass != null)
                {
                    string[] inout = n.InAndOutClass.Split(' ');

                    if (inout[0].Contains('s'))
                    {
                        ddlEnterSpeed.SelectedValue = "1";
                    }
                    else
                    {
                        ddlEnterSpeed.SelectedValue = "2";
                    }

                    if (inout[0].Contains('t'))
                    {
                        ddlEnterSide.SelectedValue = "1";
                    }
                    else if (inout[0].Contains('r'))
                    {
                        ddlEnterSide.SelectedValue = "2";
                    }
                    else if (inout[0].Contains('b'))
                    {
                        ddlEnterSide.SelectedValue = "4";
                    }
                    else
                    {
                        ddlEnterSide.SelectedValue = "3";
                    }

                    if (inout[1].Contains('s'))
                    {
                        ddlExitSpeed.SelectedValue = "1";
                    }
                    else
                    {
                        ddlExitSpeed.SelectedValue = "2";
                    }

                    if (inout[1].Contains("tt"))
                    {
                        ddlExitSide.SelectedValue = "1";
                    }
                    else if (inout[1].Contains('r'))
                    {
                        ddlExitSide.SelectedValue = "2";
                    }
                    else if (inout[1].Contains('b'))
                    {
                        ddlExitSide.SelectedValue = "4";
                    }
                    else
                    {
                        ddlExitSide.SelectedValue = "3";
                    }
                }
                else
                {
                    ddlEnterSide.SelectedValue = "0";
                    ddlEnterSpeed.SelectedValue = "0";

                    ddlExitSpeed.SelectedValue = "0";
                    ddlExitSide.SelectedValue = "0";
                }
            }
        }

        protected void grdTable_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow r in grdTable.Rows)
            {
                HiddenField hfID = (HiddenField)r.FindControl("hfID");

                Label lblx = (Label)r.FindControl("lblx");
                Label lbly = (Label)r.FindControl("lbly");

                int SliderTextID = Convert.ToInt32(hfID.Value);
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from u in db.SliderTexts where u.SliderTextID == SliderTextID select u).FirstOrDefault();

                    if (n.datax == "center")
                        lblx.Text = "مرکز";
                    else if (n.datax == "176")
                        lblx.Text = "سمت چپ";
                    else if (n.datax == "838")
                        lblx.Text = "سمت راست";
                    if (n.datay == "165")
                        lbly.Text = "اول";

                    else if (n.datay == "220")
                        lbly.Text = "دوم";

                    else if (n.datay == "295")
                        lbly.Text = "سوم";

                    else if (n.datay == "393")
                        lbly.Text = "چهارم";

                }

            }
        }

        protected void cvEnterSide_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ddlEnterSide.SelectedValue != "0")
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ddlEnterSpeed.SelectedValue != "0")
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ddlExitSide.SelectedValue != "0")
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ddlExitSpeed.SelectedValue != "0")
                args.IsValid = true;
            else
                args.IsValid = false;
        }
    }
}