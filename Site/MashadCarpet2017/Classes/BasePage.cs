using GSD.Globalization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;

namespace MashadCarpet
{
    public class BasePage: Page
    {
        protected override void InitializeCulture()
        {
          
            string lang = Convert.ToString(Session["lang"]);
            string culture = string.Empty;
          
            if (lang.ToLower().CompareTo("fa") == 0 || string.IsNullOrEmpty(lang))
            {
                culture = "fa-IR";

                var persianCulture = new PersianCulture();
                Thread.CurrentThread.CurrentCulture = persianCulture;
                Thread.CurrentThread.CurrentUICulture = persianCulture;
            }
            if (lang.ToLower().CompareTo("en") == 0)
            {
                culture = "en-US";

            }
            if (lang.ToLower().CompareTo("ru") == 0)
            {
                culture = "ru-RU";

            }
            if (lang.ToLower().CompareTo("zh") == 0)
            {
                culture = "zh-CN";

            }
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

            base.InitializeCulture();
        }
    }
}