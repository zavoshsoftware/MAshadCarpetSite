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
    
    public partial class Tickets
    {
        public Tickets()
        {
            this.TicketResponse = new HashSet<TicketResponse>();
        }
    
        public System.Guid TicketID { get; set; }
        public string TicketSubject { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.Guid> fk_UserID { get; set; }
        public Nullable<System.DateTime> TicketDate { get; set; }
        public Nullable<bool> IsValid { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public string TicketMessage { get; set; }
    
        public virtual ICollection<TicketResponse> TicketResponse { get; set; }
        public virtual Users Users { get; set; }
    }
}