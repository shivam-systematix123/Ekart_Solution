using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ekart_mvc.Models.order
{
    public class combo
    {
        public BasketItem BasketItem {get; set;}
        public IEnumerable<ProductList> productLists { get; set; }
    }
}