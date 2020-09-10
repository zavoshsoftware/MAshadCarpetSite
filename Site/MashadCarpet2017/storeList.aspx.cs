using MashadCarpet.Classes;
using MashadCarpet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MashadCarpet
{
    public partial class storeList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                using (MashadCarpetEntities db = new MashadCarpetEntities())
                {
                    var n = (from a in db.Stores
                             where a.IsDelete == false
                             orderby a.StoreCity
                             select new
                             {
                                 a.StoreName,
                                 a.StorePhone,
                                 a.StoreCity,
                                 a.StoreAddress
                             }).ToList();

                    rptStores.DataSource = n;
                    rptStores.DataBind();
                }
                ChooseTitleAndDesc();
            }
        }
        public void ChooseTitleAndDesc()
        {
            if (IdentifyCulture.cultureName().Contains("fa"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "PageScriptfa",
            "$('.newfa').removeClass('fa-arrow-right'); $('.newfa').addClass('fa-arrow-left');", true);
                Page.Title = "فروشگاه ها و نمایندگی ها | وب سایت رسمی فرش مشهد";
                Page.MetaDescription = "لیست فروشگاه ها و نمایندگی های فرش مشهد در سراسر ایران. شما عزیزان می توانید با مراجعه حضوری به این مغازه ها از محصولات ما دیدن فرمایید.";
            }
            else if (IdentifyCulture.cultureName().Contains("en"))
            {
                Page.Title = "Representatives | mashad carpet website";
                Page.MetaDescription = "mashad carpet website";
            }
            else if (IdentifyCulture.cultureName().Contains("ru"))
            {
                Page.Title = "Representatives | mashad carpet website";
                Page.MetaDescription = "mashad carpet website";
            }
            else if (IdentifyCulture.cultureName().Contains("zh"))
            {
                Page.Title = "Representatives | mashad carpet website";
                Page.MetaDescription = "mashad carpet website";
            }
            else
            {
                Page.Title = "فروشگاه ها و نمایندگی ها | وب سایت رسمی فرش مشهد";
                Page.MetaDescription = "لیست فروشگاه ها و نمایندگی های فرش مشهد در سراسر ایران. شما عزیزان می توانید با مراجعه حضوری به این مغازه ها از محصولات ما دیدن فرمایید.";
            }
        }
    }
}