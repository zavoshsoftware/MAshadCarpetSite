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
    
    public partial class ProductColors
    {
        public ProductColors()
        {
            this.ProductColorSizes = new HashSet<ProductColorSizes>();
        }
    
        public System.Guid ProductColorID { get; set; }
        public System.Guid fk_ProductID { get; set; }
        public int fk_ColorID { get; set; }
        public string ProductImage { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public Nullable<int> Priority { get; set; }
    
        public virtual Colors Colors { get; set; }
        public virtual Products Products { get; set; }
        public virtual ICollection<ProductColorSizes> ProductColorSizes { get; set; }
    }
}
