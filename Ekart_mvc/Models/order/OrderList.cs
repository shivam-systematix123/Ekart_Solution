//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ekart_mvc.Models.order
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderList
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Nullable<int> BasketId { get; set; }
        public string ProductId { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string UserId { get; set; }
    
        public virtual Basket Basket { get; set; }
        public virtual Order Order { get; set; }
    }
}
