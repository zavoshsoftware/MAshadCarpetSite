using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MashadCarpet.Classes
{
    public static class IdentifyCulture
    {
        public static string cultureName()
        {
            string lang = Convert.ToString(HttpContext.Current.Session["lang"]);
            string culture = string.Empty;

            if (lang.ToLower().CompareTo("fa") == 0 || string.IsNullOrEmpty(lang))
            {
                return "fa-IR";
            }
          else  if (lang.ToLower().CompareTo("en") == 0)
            {
                return "en-US";

            }
            else if (lang.ToLower().CompareTo("ru") == 0)
            {
                return "ru-RU";

            }
            else if (lang.ToLower().CompareTo("zh") == 0)
            {
                return "zh-CN";
            }
            else
                return "fa-IR";
            
        }
    }
}