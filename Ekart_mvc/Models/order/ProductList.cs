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
    
    public partial class ProductList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductList()
        {
            this.BasketItems = new HashSet<BasketItem>();
            this.Records = new HashSet<Record>();
        }
    
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<System.DateTimeOffset> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> TotalQun { get; set; }
        public Nullable<int> CurrentQun { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string IsActive { get; set; }
        public byte[] Image { get; set; }
        public string Image1 { get; set; }
        public string Unit { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BasketItem> BasketItems { get; set; }
        public virtual CategoryList CategoryList { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Record> Records { get; set; }
    }
}
