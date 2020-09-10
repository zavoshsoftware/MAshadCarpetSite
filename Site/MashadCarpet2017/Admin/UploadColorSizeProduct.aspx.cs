using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MashadCarpet.Models;
using System.IO;
using OfficeOpenXml;
using System.Data;
using MashadCarpet.Classes;
using System.Text.RegularExpressions;

namespace MashadCarpet.Admin
{
    public partial class UploadColorSizeProduct : System.Web.UI.Page
    {
        public string MessageResult;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                if (ExcellIsValid() == true)
                {
                    string extenstion = Path.GetExtension(FileUpload1.FileName);
                    if (extenstion == ".xlsx" || extenstion == ".xls")
                    {
                        string FileName = Log_ExcelFile(true);
                        UpdateWithExcell(FileName);
                        ResetProductStocks(FileName);

                        //insert into AlienProducts
                        UpdateAlienWithExcell(FileName);
                        //reset Alien stocks
                        ResetAlienProductStocks(FileName);
                        UpdateAlienImages();
                        pnlSuccess.Visible = true;
                    }
                    else
                    {
                        MessageResult = "Invalid Extension - " + extenstion;
                        string FileName = Log_ExcelFile(false);
                    }
                }
                else
                {
                    string FileName = Log_ExcelFile(false);

                }
            }
        }

        // Deactive Products where not Updated with this list
        public void ResetProductStocks(string FileName)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
               // var m=db.ProductColorSizes.Where(current=>current.IsDelete==false&&current.ProductColors.Products.IsAlienCulture)
                var n = (from a in db.ProductColorSizes
                         join i in db.ProductColors
                         on a.fk_ProductColorID equals i.ProductColorID
                         join p in db.Products
                         on i.fk_ProductID equals p.ProductID
                         where a.IsDelete == false && (p.IsAlienCulture == false||p.IsAlienCulture==null)
                         select a).ToList();

                foreach (var item in n)
                {
                    if (item.FileName != FileName && item.IsAvailable == true)
                    {
                        item.Stock = 0;
                        item.IsAvailable = false;
                        db.SaveChanges();
                    }
                }
            }

            
        }
        public void ResetAlienProductStocks(string FileName)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from a in db.ProductColorSizes
                         join i in db.ProductColors
                         on a.fk_ProductColorID equals i.ProductColorID
                         join p in db.Products
                         on i.fk_ProductID equals p.ProductID
                         where a.IsDelete == false && p.IsAlienCulture == true
                         select a).ToList();

                foreach (var item in n)
                {
                    if (item.FileName != FileName && item.IsAvailable == true)
                    {
                        item.Stock = 0;
                        item.IsAvailable = false;
                        db.SaveChanges();
                    }
                }
            }


        }
        public string Log_ExcelFile(bool isSuccess)
        {
            string new_Thumb1 = string.Empty;

            if (isSuccess == true)
            {
                if (FileUpload1.PostedFile.ContentLength != 0)
                {
                    string original_Thumb1 = Path.GetFileName(FileUpload1.PostedFile.FileName);

                    new_Thumb1 =
                        Guid.NewGuid().ToString() +
                        Path.GetExtension(original_Thumb1);

                    string new_filepathThumb1 = Server.MapPath("~/Uploads/Excells/" + new_Thumb1);
                    FileUpload1.PostedFile.SaveAs(new_filepathThumb1);
                }
            }

            Guid userid = new Guid(HttpContext.Current.User.Identity.Name);
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                Log_ProductExcells enter = new Log_ProductExcells();

                enter.id = Guid.NewGuid();
                enter.excelFilename = new_Thumb1;
                enter.fk_UserIDSubmited = userid;
                enter.SubmitDate = DateTime.Now;
                enter.SubmitIP = Request.UserHostAddress;
                enter.IsSuccess = isSuccess;
                enter.ErrorMsg = MessageResult;
                db.Log_ProductExcells.Add(enter);
                db.SaveChanges();

            }
            return new_Thumb1;

        }
        public void UpdateWithExcell(string FileName)
        {
            ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);

            DataTable datasource = package.ToDataTable();

            // List<string> newproList = new List<string>();

            foreach (DataRow dr in datasource.Rows)
            {
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    string ProductName = dr["FldNaghshe"].ToString();
                    string colortoret = dr["FldZamine"].ToString();
                    string sizetoret = ReturnOrginalSize(dr["FldSiz"].ToString());
                    string tarakom = (dr["FldTarakom"].ToString());
                    string rang = (dr["FldTedadRang"].ToString());
                    string shane = (dr["FldShane"].ToString());
                    int fldNaghshei=0;

                    String[] elements = Regex.Split(dr["FldNaghshe"].ToString(), @"\D+");
                    foreach (var element in elements)
                    {
                        if (!string.IsNullOrEmpty(element))
                        {
                             fldNaghshei = int.Parse(element);
                        }
                    }

                    if (returnOrginalshane(shane) != null)
                        shane = returnOrginalshane(shane);

                    string systemname = shane + "-" + rang + "-" + tarakom;

                    var n = (from a in db.ProductGroup
                             where a.ProductGroupName == systemname && a.IsDelete == false &&( a.IsAlienCulture ==null || a.IsAlienCulture==false)
                             select a).FirstOrDefault();

                    var o = (from a in db.Products
                             where a.ProductName == ProductName && a.IsDelete == false && a.fk_ProductGroupID == n.ProductGroupID && (a.IsAlienCulture == null || a.IsAlienCulture == false)
                             select a).FirstOrDefault();

                    if (o == null)
                    {
                        //Insert into All 3 Tables

                        ////////////////////////////////
                        Products et = new Products();
                        et.ProductID = Guid.NewGuid();
                        et.fk_ProductGroupID = n.ProductGroupID;
                        et.IsDelete = false;
                        et.ProductTitle = dr["FldNaghshe"].ToString();
                        et.ProductName = dr["FldNaghshe"].ToString();
                        et.ProductUniqeCode = fldNaghshei;
                        et.DesignNo = fldNaghshei;
                        et.Frame = Convert.ToInt32(dr["FldTedadRang"].ToString());
                        et.Reeds = shane;
                        et.Shots = tarakom;
                        et.IsActive = true;
                        et.IsAlienCulture = false;
                        db.Products.Add(et);

                        ProductColors pcEnter = new ProductColors();
                        pcEnter.ProductColorID = Guid.NewGuid();
                        pcEnter.fk_ProductID = et.ProductID;
                        pcEnter.IsDelete = false;
                        pcEnter.fk_ColorID = returnColorID(colortoret);
                        db.ProductColors.Add(pcEnter);

                        ProductColorSizes pcsEnter = new ProductColorSizes();
                        pcsEnter.ProductColorSizeID = Guid.NewGuid();
                        pcsEnter.fk_ProductColorID = pcEnter.ProductColorID;
                        pcsEnter.ProductPrice = Convert.ToDecimal(dr["FldGheymat"].ToString());
                        pcsEnter.Stock = Convert.ToInt32(dr["FldMojoodi"].ToString());
                        pcsEnter.fk_SizeID = returnSizeID(sizetoret);
                        pcsEnter.IsDelete = false;
                        pcsEnter.IsAvailable = true;

                        pcsEnter.FileName = FileName;
                        db.ProductColorSizes.Add(pcsEnter);

                        db.SaveChanges();

                       }
                    else
                    {

                        string productName = dr["FldNaghshe"].ToString();

                        var ProductID = o.ProductID;

                        o.IsActive = true;

                        //  string colortoret = dr["FldZamine"].ToString();
                        //string sizetoret = returnOrginalSize(dr["FldSiz"].ToString());

                        int colorid = returnColorID(colortoret);
                        int sizeid = returnSizeID(sizetoret);

                        var ProductColorID = (from a in db.ProductColors
                                              where a.fk_ColorID == colorid && a.fk_ProductID == ProductID
                                              && a.IsDelete == false
                                              select new { a.ProductColorID }).FirstOrDefault();
                        //////////////////////////////////////
                        if (ProductColorID == null)
                        {
                            ProductColors pcEnter = new ProductColors();
                            pcEnter.ProductColorID = Guid.NewGuid();
                            pcEnter.fk_ProductID = ProductID;
                            pcEnter.IsDelete = false;
                            pcEnter.fk_ColorID = returnColorID(colortoret);
                            db.ProductColors.Add(pcEnter);


                            ProductColorSizes pcsEnter = new ProductColorSizes();
                            pcsEnter.ProductColorSizeID = Guid.NewGuid();
                            pcsEnter.fk_ProductColorID = pcEnter.ProductColorID;
                            pcsEnter.ProductPrice = Convert.ToDecimal(dr["FldGheymat"].ToString());
                            pcsEnter.Stock = Convert.ToInt32(dr["FldMojoodi"].ToString());
                            pcsEnter.fk_SizeID = returnSizeID(sizetoret);
                            pcsEnter.IsDelete = false;
                            pcsEnter.FileName = FileName;
                            pcsEnter.IsAvailable = true;

                            db.ProductColorSizes.Add(pcsEnter);

                            db.SaveChanges();
                        }
                        else
                        {
                            var pcs = (from a in db.ProductColorSizes
                                       where a.fk_ProductColorID == ProductColorID.ProductColorID &&
                                       a.fk_SizeID == sizeid && a.IsDelete == false
                                       select a).FirstOrDefault();

                            if (pcs == null)
                            {
                                ProductColorSizes pcsEnter = new ProductColorSizes();
                                pcsEnter.ProductColorSizeID = Guid.NewGuid();
                                pcsEnter.fk_ProductColorID = ProductColorID.ProductColorID;
                                pcsEnter.ProductPrice = Convert.ToDecimal(dr["FldGheymat"].ToString());
                                pcsEnter.Stock = Convert.ToInt32(dr["FldMojoodi"].ToString());
                                pcsEnter.fk_SizeID = returnSizeID(sizetoret);
                                pcsEnter.IsDelete = false;
                                pcsEnter.FileName = FileName;
                                pcsEnter.IsAvailable = true;

                                db.ProductColorSizes.Add(pcsEnter);
                                db.SaveChanges();
                            }
                            else
                            {
                                pcs.Stock = Convert.ToInt32(dr["FldMojoodi"].ToString());
                                pcs.ProductPrice = Convert.ToDecimal(dr["FldGheymat"].ToString());
                                pcs.FileName = FileName;
                                pcs.IsAvailable = true;

                                db.SaveChanges();
                            }
                        }

                    }





                }
            }
        }

        public int returnColorID(string colortitle)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from a in db.Colors
                         where a.ColorTitle == colortitle && a.IsDelete == false
                         select a).FirstOrDefault();

                return n.ColorID;
            }
        }
        public int returnSizeID(string sizetitle)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                var n = (from a in db.SIzes
                         where a.SizeTitle == sizetitle
                         && a.IsDelete == false
                         select a).FirstOrDefault();

                return n.SizeID;
            }
        }
        public string ReturnOrginalSize(string size)
        {
            string[] newsize = size.Split('*');

            return (Convert.ToDouble(newsize[0]) / 100).ToString() + "*" + (Convert.ToDouble(newsize[1]) / 100).ToString();

        }
        public string ReturnOrginalSize_Reverse(string size)
        {
            string[] newsize = size.Split('*');

            return (Convert.ToDouble(newsize[0]) * 100).ToString() + "*" + (Convert.ToDouble(newsize[1]) * 100).ToString();

        }
        public string returnOrginalTarakom(string tarakom)
        {
            if (tarakom.Contains("9"))
            {
                return "900";
            }
            else if (tarakom.Contains("10"))
            {
                return "1000";
            }
            //else if (tarakom.Contains("18"))
            //{
            //    return "1800";
            //}
            else if (tarakom.Contains("12"))
            {
                return "1200";
            }
            else if (tarakom.Contains("14"))
            {
                return "1400";
            }
            //else if (tarakom.Contains("15"))
            //{
            //    return "1500";
            //}
            //else if (tarakom.Contains("8"))
            //{
            //    return "800";
            //}
            else if (tarakom.Contains("27"))
            {
                return "2700";
            }
            else if (tarakom.Contains("25"))
            {
                return "2550";
            }
            else if (tarakom.Contains("30"))
            {
                return "3000";
            }
            else
                return null;
        }
        public string returnOrginalshane(string shane)
        {
            if (shane.Contains("350"))
            {
                return "350";
            }
            else if (shane.Contains("1500"))
            {
                return "1500";
            }
            else if (shane.Contains("500"))
            {
                return "500";
            }
            else if (shane.Contains("700"))
            {
                return "700";
            }
            else if (shane.Contains("1000"))
            {
                return "1000";
            }
            else if (shane.Contains("1200"))
            {
                return "1200";
            }


            else
                return null;
        }

        #region  ExcellValidationMethods


        public Boolean ExcellIsValid()
        {
            if (CheckProductGroup() && CheckExcellColumnNumberIsValid() == true && CheckCoumnName() == true
                && CheckColors() == true)
            {
                checkSizes();
                MessageResult = "true";
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean CheckColors()
        {
            ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);

            DataTable datasource = package.ToDataTable();

            List<string> colorList = new List<string>();
            int counter = 1;
            foreach (DataRow dr in datasource.Rows)
            {
                counter = counter + 1;
                string colortitle = dr["FldZamine"].ToString();
                if (!colorList.Contains(colortitle))
                {
                    using (MashadCarpetEntities db = new MashadCarpetEntities())
                    {
                        var n = (from a in db.Colors
                                 where a.ColorTitle == colortitle
                                 && a.IsDelete == false
                                 select new
                                 {
                                     a.ColorTitle
                                 }).FirstOrDefault();

                        if (n == null)
                        {
                            pnlColorandsizeError.Visible = true;
                            lblColor.Visible = true;
                            lblColor.Text = "رنگ " + dr["FldZamine"].ToString() + "  در رنگ ها به ثبت نرسیده است.  ردیف:" + counter.ToString();
                            MessageResult = "رنگ " + dr["FldZamine"].ToString() + "  در رنگ ها به ثبت نرسیده است.  ردیف:" + counter.ToString();
                            return false;
                        }
                        else
                        {
                            colorList.Add(dr["FldZamine"].ToString());
                        }
                    }
                }
            }
            return true;
        }

        public Boolean checkSizes()
        {
            ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);

            DataTable datasource = package.ToDataTable();

            List<string> colorList = new List<string>();
            int counter = 1;
            foreach (DataRow dr in datasource.Rows)
            {
                counter = counter + 1;
                string orGinalSize = ReturnOrginalSize(dr["FldSiz"].ToString());
                if (!colorList.Contains(orGinalSize))
                {
                    using (MashadCarpetEntities db = new MashadCarpetEntities())
                    {
                        var n = (from a in db.SIzes
                                 where a.SizeTitle == orGinalSize
                                 && a.IsDelete == false
                                 select a.SizeTitle).FirstOrDefault();

                        if (n == null)
                        {
                            Insert_Sizes(orGinalSize);
                            //pnlColorandsizeError.Visible = true;
                            //lblsize.Text = "سایز " + ORGsize + "  در سایزها به ثبت نرسیده است.   ردیف:" + counter.ToString();
                            //lblsize.Visible = true;
                            //MessageResult = "سایز " + ORGsize + "  در سایزها به ثبت نرسیده است.   ردیف:" + counter.ToString();
                            //return false;
                        }
                        else
                        {
                            colorList.Add(orGinalSize);
                        }
                    }
                }
            }
            return true;
        }

        public void Insert_Sizes(string title)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                SIzes sizes = new SIzes
                {
                    SizeTitle = title,
                    IsDelete = false
                };

                db.SIzes.Add(sizes);
                db.SaveChanges();
                Insert_SizeDiscount(sizes.SizeID);
            }
        }

        public void Insert_SizeDiscount(int sizeId)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                if (db.Discounts.Any(current => current.IsActive == true && current.IsDelete == false))
                {
                    Rel_Discounts_Sizes discountsSizes = new Rel_Discounts_Sizes();
                    discountsSizes.Rel_Discount_SizesID = Guid.NewGuid();
                    discountsSizes.fk_SizeID = sizeId;
                    discountsSizes.fk_DiscountID = db.Discounts.FirstOrDefault(current => current.IsActive == true && current.IsDelete == false).DiscountID;


                    db.Rel_Discounts_Sizes.Add(discountsSizes);
                    db.SaveChanges();
                }
            }
        }
        public Boolean CheckExcellColumnNumberIsValid()
        {
            ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);

            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            int DimationCount = int.Parse(workSheet.Dimension.Columns.ToString());

            if (DimationCount == 8)
            {
                return true;
            }
            else
            {
                MessageResult = "Excell Column Number invalid";
                return false;
            }
        }

        public Boolean CheckCoumnName()
        {
            ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);

            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            int DimationCount = int.Parse(workSheet.Dimension.Columns.ToString());

            if (workSheet.Cells[1, 1, 1, 1].Text != "FldNaghshe")
            {
                MessageResult = "Coloumn Name Invalid - wrong=" + workSheet.Cells[1, 1, 1, 1].Text;
                return false;
            }

            if (workSheet.Cells[1, 2, 1, 2].Text != "FldZamine")
            {
                MessageResult = "Coloumn Name Invalid - wrong=" + workSheet.Cells[1, 2, 1, 2].Text;
                return false;
            }

            if (workSheet.Cells[1, 3, 1, 3].Text != "FldSiz")
            {
                MessageResult = "Coloumn Name Invalid - wrong=" + workSheet.Cells[1, 3, 1, 3].Text;
                return false;
            }

            if (workSheet.Cells[1, 4, 1, 4].Text != "FldShane")
            {
                MessageResult = "Coloumn Name Invalid - wrong=" + workSheet.Cells[1, 4, 1, 4].Text;
                return false;
            }

            if (workSheet.Cells[1, 5, 1, 5].Text != "FldTedadRang")
            {
                MessageResult = "Coloumn Name Invalid - wrong=" + workSheet.Cells[1, 5, 1, 5].Text;
                return false;
            }

            if (workSheet.Cells[1, 6, 1, 6].Text != "FldTarakom")
            {
                MessageResult = "Coloumn Name Invalid - wrong=" + workSheet.Cells[1, 6, 1, 6].Text;
                return false;
            }

            if (workSheet.Cells[1, 7, 1, 7].Text != "FldGheymat")
            {
                MessageResult = "Coloumn Name Invalid - wrong=" + workSheet.Cells[1, 7, 1, 7].Text;
                return false;
            }

            if (workSheet.Cells[1, 8, 1, 8].Text != "FldMojoodi")
            {
                MessageResult = "Coloumn Name Invalid - wrong=" + workSheet.Cells[1, 8, 1, 8].Text;
                return false;
            }

            else
                return true;
        }

        public Boolean CheckProductGroup()
        {
            ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);
            DataTable datasource = package.ToDataTable();

            List<string> PGList = new List<string>();
            int counter = 1;
            foreach (DataRow dr in datasource.Rows)
            {
                counter++;
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    string tarakom = (dr["FldTarakom"].ToString());
                    // if (returnOrginalTarakom(tarakom) != null)
                    //     tarakom = returnOrginalTarakom(tarakom);

                    string rang = (dr["FldTedadRang"].ToString());

                    string shane = (dr["FldShane"].ToString());
                    if (returnOrginalshane(shane) != null)
                        shane = returnOrginalshane(shane);

                    string systemname = shane + "-" + rang + "-" + tarakom;

                    if (!PGList.Contains(systemname))
                    {
                        ProductGroup n = (from a in db.ProductGroup
                                          where a.ProductGroupName == systemname && a.IsDelete == false
                                          select a).FirstOrDefault();

                        if (n != null)
                        {
                            PGList.Add(systemname);
                        }
                        else
                        {
                            pnlNoProductGroupError.Visible = true;
                            lblProGroup.Text = shane + " شانه " + rang + " رنگ تراکم " + tarakom;
                            MessageResult = shane + " شانه " + rang + " رنگ تراکم " + tarakom;
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //public void deActiveAllProducts(Guid ProductColorSizeId)
        //{
        //    using (MashadCarpetEntities db = new MashadCarpetEntities())
        //    {
        //        Guid pcs = db.ProductColorSizes.Where(a => a.ProductColorSizeID == ProductColorSizeId).Select(a => a.fk_ProductColorID).FirstOrDefault();
        //        var pc = db.ProductColors.Where(a => a.ProductColorID == pcs).Select(a => a.fk_ProductID).FirstOrDefault();


        //        var product = db.Products.Where(a => a.IsActive == true && a.IsDelete == false).ToList();
        //        foreach (var item in product)
        //        {
        //            item.IsActive = false;
        //        }
        //    }
        //}
        #endregion

        public void UpdateAlienWithExcell(string fileName)
        {
            ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);
            DataTable datasource = package.ToDataTable();
            foreach (DataRow dr in datasource.Rows)
            {
                
                string productName = dr["FldNaghshe"].ToString();
                string colortoret = dr["FldZamine"].ToString();
                string sizetoret = ReturnOrginalSize(dr["FldSiz"].ToString());
                string tarakom = (dr["FldTarakom"].ToString());
                string rang = (dr["FldTedadRang"].ToString());
                string shane = (dr["FldShane"].ToString());
                string gheymat = dr["FldGheymat"].ToString();
                string mojoodi=dr["FldMojoodi"].ToString();
                int fldNaghshei = 0;

                String[] elements = Regex.Split(dr["FldNaghshe"].ToString(), @"\D+");
                foreach (var element in elements)
                {
                    if (!string.IsNullOrEmpty(element))
                    {
                        fldNaghshei = int.Parse(element);
                    }
                }
                if (returnOrginalshane(shane) != null)
                    shane = returnOrginalshane(shane);

                string systemname = shane + "-" + rang + "-" + tarakom;
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from a in db.ProductGroup
                             where a.ProductGroupName == systemname && a.IsDelete == false && a.IsAlienCulture == true
                             select a).FirstOrDefault();

                    var o = (from a in db.Products
                             where a.ProductName == productName && a.IsDelete == false && a.fk_ProductGroupID == n.ProductGroupID && a.IsAlienCulture == true
                             select a).FirstOrDefault();

                    if (o == null)
                    {
                        //Insert into All 3 Tables

                        ////////////////////////////////
                        Products et = new Products();
                        et.ProductID = Guid.NewGuid();
                        et.fk_ProductGroupID = n.ProductGroupID;
                        et.IsDelete = false;
                        et.ProductTitle = productName;
                        et.ProductName = productName;
                        et.ProductUniqeCode = fldNaghshei;
                        et.DesignNo = fldNaghshei;
                        et.Frame = Convert.ToInt32(rang);
                        et.Reeds = shane;
                        et.Shots = tarakom;
                        et.IsActive = true;
                        et.IsAlienCulture = true;
                        et.EN_ProductTitle = productName;
                        et.Rus_ProductTitle = productName;
                        et.China_ProductTitle = productName;

                        db.Products.Add(et);

                        ProductColors pcEnter = new ProductColors();
                        pcEnter.ProductColorID = Guid.NewGuid();
                        pcEnter.fk_ProductID = et.ProductID;
                        pcEnter.IsDelete = false;
                        pcEnter.fk_ColorID = returnColorID(colortoret);
                        db.ProductColors.Add(pcEnter);

                        ProductColorSizes pcsEnter = new ProductColorSizes();
                        pcsEnter.ProductColorSizeID = Guid.NewGuid();
                        pcsEnter.fk_ProductColorID = pcEnter.ProductColorID;
                        pcsEnter.ProductPrice = Convert.ToDecimal(gheymat);
                        pcsEnter.Stock = Convert.ToInt32(mojoodi);
                        pcsEnter.fk_SizeID = returnSizeID(sizetoret);
                        pcsEnter.IsDelete = false;
                        pcsEnter.IsAvailable = true;

                        pcsEnter.FileName = fileName;
                        db.ProductColorSizes.Add(pcsEnter);

                        db.SaveChanges();
                    }

                    else
                    {

                        var ProductID = o.ProductID;

                        o.IsActive = true;

                        //  string colortoret = dr["FldZamine"].ToString();
                        //string sizetoret = returnOrginalSize(dr["FldSiz"].ToString());

                        int colorid = returnColorID(colortoret);
                        int sizeid = returnSizeID(sizetoret);

                        var ProductColorID = (from a in db.ProductColors
                                              where a.fk_ColorID == colorid && a.fk_ProductID == ProductID
                                              && a.IsDelete == false
                                              select new { a.ProductColorID }).FirstOrDefault();
                        //////////////////////////////////////
                        if (ProductColorID == null)
                        {
                            ProductColors pcEnter = new ProductColors();
                            pcEnter.ProductColorID = Guid.NewGuid();
                            pcEnter.fk_ProductID = ProductID;
                            pcEnter.IsDelete = false;
                            pcEnter.fk_ColorID = returnColorID(colortoret);
                            db.ProductColors.Add(pcEnter);


                            ProductColorSizes pcsEnter = new ProductColorSizes();
                            pcsEnter.ProductColorSizeID = Guid.NewGuid();
                            pcsEnter.fk_ProductColorID = pcEnter.ProductColorID;
                            pcsEnter.ProductPrice = Convert.ToDecimal(dr["FldGheymat"].ToString());
                            pcsEnter.Stock = Convert.ToInt32(dr["FldMojoodi"].ToString());
                            pcsEnter.fk_SizeID = returnSizeID(sizetoret);
                            pcsEnter.IsDelete = false;
                            pcsEnter.FileName = fileName;
                            pcsEnter.IsAvailable = true;

                            db.ProductColorSizes.Add(pcsEnter);

                            db.SaveChanges();
                        }
                        else
                        {
                            var pcs = (from a in db.ProductColorSizes
                                       where a.fk_ProductColorID == ProductColorID.ProductColorID &&
                                       a.fk_SizeID == sizeid && a.IsDelete == false
                                       select a).FirstOrDefault();

                            if (pcs == null)
                            {
                                ProductColorSizes pcsEnter = new ProductColorSizes();
                                pcsEnter.ProductColorSizeID = Guid.NewGuid();
                                pcsEnter.fk_ProductColorID = ProductColorID.ProductColorID;
                                pcsEnter.ProductPrice = Convert.ToDecimal(dr["FldGheymat"].ToString());
                                pcsEnter.Stock = Convert.ToInt32(dr["FldMojoodi"].ToString());
                                pcsEnter.fk_SizeID = returnSizeID(sizetoret);
                                pcsEnter.IsDelete = false;
                                pcsEnter.FileName = fileName;
                                pcsEnter.IsAvailable = true;

                                db.ProductColorSizes.Add(pcsEnter);
                                db.SaveChanges();
                            }
                            else
                            {
                                pcs.Stock = Convert.ToInt32(dr["FldMojoodi"].ToString());
                                pcs.ProductPrice = Convert.ToDecimal(dr["FldGheymat"].ToString());
                                pcs.FileName = fileName;
                                pcs.IsAvailable = true;

                                db.SaveChanges();
                            }
                        }

                    }
                }
            }
        }
        public void UpdateAlienImages()
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
                List<ProductColors> alienproductColors = db.ProductColors.Where(current => current.IsDelete == false
                && current.Products.IsAlienCulture == true && current.Products.IsActive == true && current.ProductImage == null).ToList();

                List<ProductColors> productColors = db.ProductColors.Where(current => current.IsDelete == false
                && (current.Products.IsAlienCulture == false || current.Products.IsAlienCulture == null) && current.Products.IsActive == true && current.ProductImage != "").ToList();

                foreach (var item in alienproductColors)
                {
                    foreach (var item2 in productColors)
                    {
                        if (item.Products.ProductTitle == item2.Products.ProductTitle && item.fk_ColorID == item2.fk_ColorID)
                        {
                            item.ProductImage = item2.ProductImage;

                        }
                    }
                }
                db.SaveChanges();

            }

        }
    }
}