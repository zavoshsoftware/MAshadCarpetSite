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
    
    public partial class Rel_Discounts_Sizes
    {
        public System.Guid Rel_Discount_SizesID { get; set; }
        public System.Guid fk_DiscountID { get; set; }
        public int fk_SizeID { get; set; }
    
        public virtual Discounts Discounts { get; set; }
        public virtual SIzes SIzes { get; set; }
    }
}
