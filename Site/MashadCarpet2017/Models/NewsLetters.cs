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
    
    public partial class NewsLetters
    {
        public System.Guid NewsLetterID { get; set; }
        public string NewsLetterEmail { get; set; }
        public Nullable<System.DateTime> SubmitDate { get; set; }
        public string SubmitIP { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public Nullable<bool> IsValid { get; set; }
    }
}
