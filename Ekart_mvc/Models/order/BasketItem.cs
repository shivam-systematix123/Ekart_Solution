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
    
    public partial class BasketItem
    {
        public int Id { get; set; }
        public Nullable<int> BasketId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<System.DateTimeOffset> CreatedAt { get; set; }
        public Nullable<decimal> Price { get; set; }
    
        public virtual Basket Basket { get; set; }
        public virtual ProductList ProductList { get; set; }
    }
}
