//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace deneme.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class sepet
    {
        public string sepetId { get; set; }
        public string sepetUyeId { get; set; }
        public string sepetUrunId { get; set; }
    
        public virtual urunler urunler { get; set; }
        public virtual uyeler uyeler { get; set; }
    }
}
