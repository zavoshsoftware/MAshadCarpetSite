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
    
    public partial class Province
    {
        public Province()
        {
            this.City = new HashSet<City>();
            this.Users = new HashSet<Users>();
        }
    
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public string EN_ProvinceName { get; set; }
        public string Rus_ProvinceName { get; set; }
        public string China_ProvinceName { get; set; }
    
        public virtual ICollection<City> City { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
