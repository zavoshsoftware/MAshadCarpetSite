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
    
    public partial class Log_LoginAttemps
    {
        public int LoginLogID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public System.DateTime VisitDate { get; set; }
        public string IP { get; set; }
        public string Browser { get; set; }
        public string OS { get; set; }
        public string RefrallPage { get; set; }
        public Nullable<bool> IsSuccess { get; set; }
    }
}