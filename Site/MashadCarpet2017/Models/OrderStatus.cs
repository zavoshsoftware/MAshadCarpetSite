//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MashadCarpet.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            this.Orders = new HashSet<Orders>();
        }
    
        public int OrderStatusID { get; set; }
        public string OrderStatusTitle { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public string EN_OrderStatus { get; set; }
        public string Rus_OrderStatus { get; set; }
        public string China_OrderStatus { get; set; }
    
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
