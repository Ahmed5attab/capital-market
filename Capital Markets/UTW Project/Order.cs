//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UTW_Project
{
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Order
    {
        public int ClientID { get; set; }
        [Display(Name = "OrderID", ResourceType = typeof(MyTexts))]
        public int OrderID { get; set; }
        public int OrderQuantity { get; set; }
        public int StockID { get; set; }
        [Display(Name = "OrderDate", ResourceType = typeof(MyTexts))]
        public System.DateTime OrderDate { get; set; }
        public int OrderType { get; set; }
        public double OrderPrice { get; set; }
    
        public virtual Person Person { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
