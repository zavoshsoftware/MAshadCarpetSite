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
    
    public partial class Users
    {
        public Users()
        {
            this.Discounts = new HashSet<Discounts>();
            this.Log_ProductExcells = new HashSet<Log_ProductExcells>();
            this.TicketResponse = new HashSet<TicketResponse>();
            this.Tickets = new HashSet<Tickets>();
            this.Orders = new HashSet<Orders>();
        }
    
        public System.Guid UserID { get; set; }
        public string UserName { get; set; }
        public string UserFamily { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public Nullable<int> fk_RoleID { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public Nullable<System.DateTime> RegisterDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RegisterIP { get; set; }
        public Nullable<int> ProvinceID { get; set; }
        public Nullable<int> CityID { get; set; }
    
        public virtual City City { get; set; }
        public virtual ICollection<Discounts> Discounts { get; set; }
        public virtual ICollection<Log_ProductExcells> Log_ProductExcells { get; set; }
        public virtual Province Province { get; set; }
        public virtual Roles Roles { get; set; }
        public virtual ICollection<TicketResponse> TicketResponse { get; set; }
        public virtual ICollection<Tickets> Tickets { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}