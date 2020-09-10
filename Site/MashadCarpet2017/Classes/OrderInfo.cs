using MashadCarpet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MashadCarpet.Classes
{
    public static class OrderInfo
    {
        public static double returnFinalPrice(Guid OrderID)
        {
            using (MashadCarpetEntities db = new MashadCarpetEntities())
            {
               double TotalPrice = 0;

                var n = (from a in db.OrderDetails
                         where a.fk_OrderID == OrderID && a.IsDelete == false
                         select a).ToList();

                foreach (var item in n)
                {
                     var m = (from a in db.ProductColorSizes
                                 where a.ProductColorSizeID == item.fk_ProductColorSizeID
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
                             double pricenew = CalculateNewPrice((double)o.DiscountPercent, (decimal)m.ProductPrice);
                             TotalPrice = TotalPrice + (double)(pricenew * item.Count);
                         }
                         else
                         {
                             TotalPrice = TotalPrice + (double)(m.ProductPrice * item.Count);
                         }

                     }
                }
                return TotalPrice;
            }
        }

        public static double CalculateNewPrice(double DiscountPercent, decimal price)
        {
            double NewPercent = 100 - DiscountPercent;
            double pricenew = ((Convert.ToDouble(price) * NewPercent) / 100);

            return pricenew;
        }
    }
}