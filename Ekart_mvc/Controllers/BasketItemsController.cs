using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ekart_mvc.Models;
using Ekart_mvc.Models.order;


namespace Ekart_mvc.Controllers
{
    public class BasketItemsController : Controller
    {
        private EkartEntities1 dc = new EkartEntities1();
        private EkartEntities7 db = new EkartEntities7();
        // GET: BasketItems
        public ActionResult Index()
        {
           /* var i;*/
            var basketItems = db.BasketItems/*.Include(a => a.BasketId).Where(a => a.Basket.UserId.Equals("Y"))*/;
            var productLists = db.ProductLists.Include(p => p.CategoryList).Where(p => p.CategoryList.IsActive.Equals("Y") && p.IsActive.Equals("Y"));
          /*  if (Session["bask"] == null)
            {
                Basket basket = new Basket();

                basket = new Basket();
                basket.CreatedAt = DateTime.Now;
                db.Baskets.Add(basket);
                db.SaveChanges();
                i = basket.BasketId;
            }*/
            return View(productLists.ToList().Where(a => a.CurrentQun >= 1));
        }
        public ActionResult Index1()
        {
            
            string s1 = Session["userName"].ToString();

            Register r = dc.Registers.Where(a => a.Email == s1).SingleOrDefault();
            int i = r.UserId;
            Basket basket = db.Baskets.Where(a => a.Uid == i.ToString()).SingleOrDefault();
            int k = basket.BasketId;
            var productLists = db.BasketItems.Include(p => p.Basket).Where(p => p.Basket.BasketId == basket.BasketId);
            BasketItem c = db.BasketItems.Where(a => a.BasketId == k).FirstOrDefault();
            if (c == null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Basket Is Empty Please Add Atleast 1 Item'); window.location.replace('Index');</script>");

            }
            else
            {
                return View(productLists.ToList());
            }
        }

        /* public ActionResult AddToBasket(int Id)
         {
             ProductList p1 = new ProductList();

             p1 = db.ProductLists.Find(Id);
 *//*            b1.ProductId = p1.ProductId;
 *//*            p1.Quantity = p1.Quantity + 1
 *//*
             return RedirectToAction("Index");
     }

     // GET: BasketItems/Details/5*/

        /*public ActionResult AddToBasket(int? productId) 
        {
            ViewBag.BasketId = new (db.Baskets, "BasketId", "BasketId");
            ViewBag.ProductId = new SelectList(db.ProductLists, "ProductId", "ProductName");
            return View();
        }
*/



        public ActionResult AddToBasket(int? productId)
        {
            string s1 = Session["userName"].ToString();   
            Basket basket = new Basket();
            ProductList p1 = new ProductList();

            p1 = db.ProductLists.Where(a => a.ProductId == productId).SingleOrDefault();
            int? f = p1.CurrentQun;
            Register r = dc.Registers.Where(a => a.Email == s1).SingleOrDefault();
            int i= r.UserId;
            TempData["q"] = 1;
            /*            basket = db.Baskets.Where(a => a.UserId == i).FirstOrDefault();
            */
            basket= db.Baskets.Where(a => a.Uid == i.ToString()).SingleOrDefault();
            if (basket == null)
                {
                basket = new Basket();
                basket.CreatedAt = DateTime.Now;
                    basket.Uid = i.ToString();
                    db.Baskets.Add(basket);
                    db.SaveChanges();
               
                BasketItem item = db.BasketItems.Where(a => a.BasketId == basket.BasketId && a.ProductId == productId).SingleOrDefault();
                if (item == null)
                {
                    if (f > 0)
                    {
                        item = new BasketItem();
                        ProductList p = db.ProductLists.Find(productId);
                        item.BasketId = basket.BasketId;
                        item.ProductId = productId;
                        item.Quantity = 1;
                        item.Price = p.Price;
                        item.CreatedAt = DateTime.Now;
                        p.CurrentQun = p.CurrentQun - 1;
                        db.BasketItems.Add(item);
                        db.SaveChanges();
                    }
                    else
                    {
                return Content("<script language='javascript' type='text/javascript'>alert('Basket Is Empty Please Add Atleast 1 Item'); window.location.replace('Index');</script>");

                    }
                }
                else
                {
                    if (f > 0)
                    {
                        ProductList p = db.ProductLists.Find(productId);
                        p.CurrentQun = p.CurrentQun - 1;
                        item.Quantity = item.Quantity + 1;
                        db.SaveChanges();
                    }
                    else
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert('Basket Is Empty Please Add Atleast 1 Item'); window.location.replace('Index');</script>");

                    }
                }
                }
                else
                {
                    BasketItem item = db.BasketItems.Where(a => a.ProductId == productId && a.BasketId == basket.BasketId).FirstOrDefault();

                if (item == null)
                {
                    if (f > 0)
                    {
                        item = new BasketItem();
                        ProductList p = db.ProductLists.Find(productId);
                        item.BasketId = basket.BasketId;
                        item.ProductId = productId;
                        item.Quantity = 1;
                        item.Price = p.Price;
                        item.CreatedAt = DateTime.Now;
                        p.CurrentQun = p.CurrentQun - 1;
                        db.BasketItems.Add(item);
                        db.SaveChanges();
                    }
                    else
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert('Basket Is Empty Please Add Atleast 1 Item'); window.location.replace('Index');</script>");

                    }
                }
                else
                    {
                    if (f > 0)
                    {
                        ProductList p = db.ProductLists.Find(productId);
                        p.CurrentQun = p.CurrentQun - 1;
                        item.Quantity = item.Quantity + 1;
                        db.SaveChanges();
                    }
                    else
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert('Basket Is Empty Please Add Atleast 1 Item'); window.location.replace('Index');</script>");

                    }
                }
                }
                return RedirectToAction("Index1");
        }

        public ActionResult Removeb(int? productId)
        {
            string s1 = Session["userName"].ToString();
           

            Register r = dc.Registers.Where(a => a.Email == s1).SingleOrDefault();
            int i = r.UserId;
            Basket basket = db.Baskets.Where(a => a.Uid == i.ToString()).FirstOrDefault();
           BasketItem item = db.BasketItems.Where(a => a.BasketId == basket.BasketId && a.ProductId == productId).SingleOrDefault();
                if (item.Quantity == 1)
                {
                
                ProductList p = db.ProductLists.Find(productId);
                p.CurrentQun = p.CurrentQun + 1 ;
                db.BasketItems.Remove(item);
                db.SaveChanges();
               }
            else
                {
                    ProductList p = db.ProductLists.Find(productId);
                p.CurrentQun = p.CurrentQun +1;
                item.Quantity = item.Quantity -1;
                    db.SaveChanges();
                }
            
           
            return RedirectToAction("Index1");
        }

        /* public void RemoveFromBasket(HttpContextBase httpContext, string itemId)
         {
             Basket basket = GetBasket(httpContext, true);
             BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId.ToString() == itemId);

             if (item != null)
             {
                 *//*basket.BasketItems.Remove(item);
                 basketContext.Commit();*//*
                 db.BasketItems.Remove(item);
                 db.SaveChanges();
             }
         }
 */

        public ActionResult Details()
        {

            return View(db.BasketItems.TakeWhile(a => a.BasketId == 1).ToList());
        }

        // GET: BasketItems/Create
        public ActionResult Create()
        {
            /**/
            ViewBag.BasketId = new SelectList(db.Baskets, "BasketId", "BasketId");
            
            ViewBag.ProductId = new SelectList(db.ProductLists, "ProductId", "ProductName");
            return View();
        }

        // POST: BasketItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BasketId,ProductId,Quantity,CreatedAt,Price")] BasketItem basketItem)
        {
            if (ModelState.IsValid)
            {
                db.BasketItems.Add(basketItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BasketId = new SelectList(db.Baskets, "BasketId", "BasketId", basketItem.BasketId);
            ViewBag.ProductId = new SelectList(db.ProductLists, "ProductId", "ProductName", basketItem.ProductId);
            return View(basketItem);
        }

        // GET: BasketItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasketItem basketItem = db.BasketItems.Find(id);
            if (basketItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.BasketId = new SelectList(db.Baskets, "BasketId", "BasketId", basketItem.BasketId);
            ViewBag.ProductId = new SelectList(db.ProductLists, "ProductId", "ProductName", basketItem.ProductId);
            return View(basketItem);
        }

        // POST: BasketItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BasketId,ProductId,Quantity,CreatedAt,Price")] BasketItem basketItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(basketItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BasketId = new SelectList(db.Baskets, "BasketId", "BasketId", basketItem.BasketId);
            ViewBag.ProductId = new SelectList(db.ProductLists, "ProductId", "ProductName", basketItem.ProductId);
            return View(basketItem);
        }

        // GET: BasketItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasketItem basketItem = db.BasketItems.Find(id);
            if (basketItem == null)
            {
                return HttpNotFound();
            }
            return View(basketItem);
        }

        // POST: BasketItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BasketItem basketItem = db.BasketItems.Find(id);
            db.BasketItems.Remove(basketItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
