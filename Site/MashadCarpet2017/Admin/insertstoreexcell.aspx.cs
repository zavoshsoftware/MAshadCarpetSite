using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MashadCarpet.Classes;
using MashadCarpet.Models;

namespace MashadCarpet.Admin
{
    public partial class insertstoreexcell : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);

            DataTable datasource = package.ToDataTable();

            // List<string> newproList = new List<string>();

            foreach (DataRow dr in datasource.Rows)
            {
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    string city = dr["شهر"].ToString();
                    string address = dr["آدرس"].ToString();
                    string phone = dr["تلفن"].ToString();

                    Stores sss = new Stores();
                    sss.StoreID = Guid.NewGuid();
                    sss.StorePhone = phone;
                    sss.StoreAddress = address;
                    sss.IsDelete = false;
                    sss.StoreCity = city;
                    sss.IsStore = true;

                    db.Stores.Add(sss);
                    db.SaveChanges();

                }
            }
        }
    }
}

     