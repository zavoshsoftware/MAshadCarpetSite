using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MashadCarpet.Classes
{
    public static class OSIdentify
    {
        public static string OSName()
        {

            if (LoadReq("Windows 95") > 0 ||
                LoadReq("Windows_95") > 0 ||
                LoadReq("Win95") > 0)
            {
                return "Windows 95";
            }
            else if (LoadReq("Windows 98") > 0 ||
               LoadReq("Win98") > 0)
            {
                return "Windows 98";
            }
            else if (LoadReq("Windows NT 5.0") > 0 ||
             LoadReq("Windows 2000") > 0)
            {
                return "Windows 2000";
            }
            else if (LoadReq("Windows NT 5.1") > 0 ||
        LoadReq("Windows XP") > 0)
            {
                return "Windows xp";
            }
            else if (LoadReq("Windows NT 5.2") > 0)
            {
                return "Windows server 2003";
            }
            else if (LoadReq("Windows NT 6.0") > 0)
            {
                return "Windows Vista";
            }
            else if (LoadReq("Windows NT 6.1") > 0
                || LoadReq("Windows 7") > 0
                )
            {
                return "Windows 7";
            }
            else if (LoadReq("Windows 8") > 0 ||
                LoadReq("Windows NT 6.2") > 0)
            {
                return "Windows 8";
            }
            else if (LoadReq("Windows ME") > 0)
            {
                return "Windows ME";
            }

            else if (LoadReq("Mac_PowerPC") > 0 ||
    LoadReq("Macintosh") > 0)
            {
                return "Mac OS";
            }
            else if (LoadReq("Android") > 0)
            {
                return "Android";
            }
            else if (LoadReq("iPod") > 0)
            {
                return "iPod";
            }
            else if (LoadReq("iPhone") > 0)
            {
                return "iPhone";
            }
            else if (LoadReq("iPad") > 0)
            {
                return "iPad";
            }
            else return "Other";
        }
        public static int LoadReq(string OsName)
        {
            int osIndex =HttpContext.Current.Request.UserAgent.IndexOf(OsName);
            return osIndex;
        }
  
    }
}