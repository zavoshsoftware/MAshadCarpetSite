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
    
    public partial class Orders
    {
        public Orders()
        {
            this.OrderDetails = new HashSet<OrderDetails>();
            this.PaymentLogs = new HashSet<PaymentLogs>();
            this.PaymentUniqeCodes = new HashSet<PaymentUniqeCodes>();
        }
    
        public System.Guid OrderID { get; set; }
        public Nullable<System.Guid> fk_UserID { get; set; }
        public Nullable<System.DateTime> SubmitDate { get; set; }
        public Nullable<bool> IsFinalized { get; set; }
        public Nullable<bool> IsPaid { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPhone { get; set; }
        public string RecipientAddress { get; set; }
        public Nullable<int> PaymentWay { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public string CustomerOrderID { get; set; }
        public Nullable<int> fk_OrderStatusID { get; set; }
        public string OrderDesc { get; set; }
        public Nullable<decimal> FinalPrice { get; set; }
        public int OrderShowID { get; set; }
        public Nullable<bool> IsNewOrder { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
    
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual Users Users { get; set; }
        public virtual ICollection<PaymentLogs> PaymentLogs { get; set; }
        public virtual ICollection<PaymentUniqeCodes> PaymentUniqeCodes { get; set; }
    }
}
