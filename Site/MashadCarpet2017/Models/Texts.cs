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
    
    public partial class Texts
    {
        public int TextID { get; set; }
        public string TextTitle { get; set; }
        public string EN_TextTitle { get; set; }
        public string TextDescription { get; set; }
        public string EN_TextDescription { get; set; }
        public string TextImage { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public Nullable<int> GroupID { get; set; }
        public string TextName { get; set; }
        public string Rus_TextTitle { get; set; }
        public string China_TextTitle { get; set; }
        public string Rus_TextDescription { get; set; }
        public string China_TextDescription { get; set; }
    }
}
