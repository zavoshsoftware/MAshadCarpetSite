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
    
    public partial class SliderTexts
    {
        public int SliderTextID { get; set; }
        public int fk_SliderID { get; set; }
        public string datax { get; set; }
        public string datay { get; set; }
        public string speed { get; set; }
        public string start { get; set; }
        public bool IsLink { get; set; }
        public string LinkAddress { get; set; }
        public string Text { get; set; }
        public string InAndOutClass { get; set; }
        public string textColor { get; set; }
        public string EN_Text { get; set; }
        public string Rus_Text { get; set; }
        public string China_Text { get; set; }
        public Nullable<int> fontsize { get; set; }
    
        public virtual Slider Slider { get; set; }
    }
}
