using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ekart_mvc.Models;
/*using Ekart_mvc.Models.@new;*/
using Ekart_mvc.Models.order;


namespace Ekart_mvc.Controllers
{
    public class OrdersController : Controller
    {
        private EkartEntities7 db = new EkartEntities7();
        private EkartEntities1 dc = new EkartEntities1();
        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Orders.ToList());

        }
        public ActionResult Summary(int? orderid)
        {
            var orderLists = db.OrderLists.Include(o => o.Order).Where(a => a.OrderId == orderid);

            return View(orderLists.ToList());
        }
        public ActionResult My()
        {    
            string s1 = Session["userName"].ToString();

            Register r = dc.Registers.Where(a => a.Email == s1).SingleOrDefault();
            int i = r.UserId;
            return View(db.Orders.ToList().Where(a => a.UserName == i.ToString()));

        }

        public ActionResult Order()
        {
            string s1 = Session["userName"].ToString();
            Register r = dc.Registers.Where(a => a.Email == s1).SingleOrDefault();
            int i = r.UserId;
            Basket basket = db.Baskets.Where(a => a.Uid == i.ToString()).SingleOrDefault();
            string b = basket.Uid;
            Order order = new Order();
            OrderList orderList = new OrderList();
            var z = db.Orders.Where(a => a.UserName == i.ToString()).ToList();
            int k = 0;
            foreach(var t in z)
            {
                k = t.OrderId;
            }
            /*int k = order.OrderId;*/
            Basket bask = db.Baskets.Where(a => a.Uid == b).SingleOrDefault();

            var x = db.BasketItems.Include(p => p.Basket).Where(p => p.Basket.BasketId.Equals(bask.BasketId));
            foreach (var temp in x.ToList())
            {
                 order = new Order();
                orderList.Price = temp.Price;
                orderList.ProductId = temp.ProductList.ProductName;
                orderList.Quantity = temp.Quantity;
                orderList.UserId = b;
                orderList.OrderId = k;
                db.OrderLists.Add(orderList);
                db.SaveChanges();
            }

            Basket ba = db.Baskets.Where(a => a.Uid == i.ToString()).FirstOrDefault();
            var y = db.BasketItems.Include(p => p.Basket).Where(p => p.Basket.BasketId.Equals(ba.BasketId));
            foreach (var temp in y.ToList())
            {
                db.BasketItems.Remove(temp);
                db.SaveChanges();
            }
            Order order1 = db.Orders.Find(k);


                return View(order1);

            
           

        }
        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            var orderLists = db.OrderLists.Include(o => o.Order).Where(a => a.OrderId == id);

            return View(orderLists.ToList());
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            string s1 = Session["userName"].ToString();
            Register r = dc.Registers.Where(a => a.Email == s1).SingleOrDefault();
            int i = r.UserId;
            Basket basket = db.Baskets.Where(a => a.Uid == i.ToString()).SingleOrDefault();
            string b = basket.Uid;
            Basket bask = db.Baskets.Where(a => a.Uid == b).SingleOrDefault();
            if (bask != null)
            {
                return View();
            }
            else { return RedirectToActionPermanent(""); }
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,CreatedAt,Name,Email,Contact,Address,UserName,Status,Remark,DeliveryDate,PinCode")] Order order)
        {
            if (ModelState.IsValid)
            {
                string s1 = Session["userName"].ToString();
                Register r = dc.Registers.Where(a => a.Email == s1).SingleOrDefault();
                int i = r.UserId;
                order.UserName = i.ToString();
                order.Status = "Pending";
                order.CreatedAt = DateTime.Now;
                order.Remark = "NA";
                order.DeliveryDate = null;
                db.Orders.Add(order);

                db.SaveChanges();

                return RedirectToAction("Order");
               /* Basket basket = db.Baskets.Where(a => a.Uid == i.ToString()).SingleOrDefault();
                
                
                var x = db.BasketItems.ToList().TakeWhile(a => a.BasketId == basket.BasketId);
                foreach (var temp in x)
                {
                    OrderList orderList = new OrderList();
                    orderList.Price = temp.Price;
                    orderList.ProductId = temp.ProductList.ProductName;
                    orderList.Quantity = temp.Quantity;
                    orderList.UserId = i.ToString();
                    orderList.OrderId = order.OrderId;
                    db.OrderLists.Add(orderList);
                    db.SaveChanges();

                    Order order1 = db.Orders.Find(order.OrderId);
                    return View(order1);*/
                }

            return View(order);
        }
          
        

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*[Bind(Include = "OrderId,CreatedAt")]*/ Order order)
        {
           
                Register r1 = new Register();
                string s1 = Session["userName"].ToString();

                r1 = dc.Registers.Where(a => a.Email == s1).SingleOrDefault();
                int z = r1.UserId;
                Order o1 = new Order();
                o1 = db.Orders.Where(a => a.OrderId == order.OrderId).SingleOrDefault();
                o1.Status = "ACCPECT";
                o1.DeliveryDate = order.DeliveryDate;
                o1.Remark = order.Remark;
                db.SaveChanges();
                return RedirectToAction("Index");
           
/*            return View(order);
*/        }


        public ActionResult Reject(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reject(/*[Bind(Include = "OrderId,CreatedAt")]*/ Order order)
        {
           /* if (ModelState.IsValid)
            {*/
                Register r1 = new Register();
                string s1 = Session["userName"].ToString();

                r1 = dc.Registers.Where(a => a.Email == s1).SingleOrDefault();
                int z = r1.UserId;
                Order o1 = new Order();
                o1 = db.Orders.Where(a => a.OrderId == order.OrderId).SingleOrDefault();
                o1.Status = "REJECT";
                o1.DeliveryDate = null;
                o1.Remark = order.Remark;
            var zz = db.OrderLists.Include(o => o.Order).Where(a => a.OrderId == o1.OrderId).ToList();

            foreach(var z2 in zz)
            {
                ProductList p2 = new ProductList();
                int? q = z2.Quantity;
                p2 = db.ProductLists.Where(a => a.ProductName == z2.ProductId).SingleOrDefault();
                p2.CurrentQun = p2.CurrentQun + q;
                
            }
                db.SaveChanges();
                return RedirectToAction("Index");
          /*  }
            return View(order);*/
        }



        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
