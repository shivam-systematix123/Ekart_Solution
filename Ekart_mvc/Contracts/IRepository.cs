using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ekart_mvc.Models;
using Ekart_mvc.Models.order;

namespace Ekart_mvc.Contracts
{
   

  
        public interface IRepository<T> where T : EkartEntities7
    {
            IQueryable<T> Collection();
            void Commit();
            void Delete(string Id);
            T Find(string Id);
            void Insert(T t);
            void Update(T t);
        }
    
}