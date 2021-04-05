using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ekart_mvc.Controllers;
using System.Text;
using System.Threading.Tasks;
using Ekart_mvc.Models;
using Ekart_mvc.Models.order;
using Ekart_mvc.Contracts;

namespace Ekart_mvc.Contracts
{
    public interface IBasketService
    {
        void AddToBasket(HttpContextBase httpContext, string productId);
        void RemoveFromBasket(HttpContextBase httpContext, string itemId);
        List<BasketItem> GetBasketItems(HttpContextBase httpContext);
/*        BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext);
*/
    }
}
