using Ekart_mvc.Models.order;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ekart_mvc.Controllers
{
    public class OrderListsController : Controller
    {
        private EkartEntities7 db = new EkartEntities7();

        // GET: OrderLists
        public ActionResult Index()
        {
            var orderLists = db.OrderLists.Include(o => o.Order);
            return View(orderLists.ToList());
        }

        // GET: OrderLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderList orderList = db.OrderLists.Find(id);
            if (orderList == null)
            {
                return HttpNotFound();
            }
            return View(orderList);
        }

        // GET: OrderLists/Create
        public ActionResult Create()
        {
            /*            ViewBag.BasketId = new SelectList(db.Baskets, "BasketId", "BasketId");
            */
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderId");
            return View();
        }

        // POST: OrderLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,BasketId,ProductId,Price,Quantity,CreatedAt,Status,Id")] OrderList orderList)
        {
            if (ModelState.IsValid)
            {
                db.OrderLists.Add(orderList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            /*            ViewBag.BasketId = new SelectList(db.Baskets, "BasketId", "BasketId", orderList.BasketId);
            */
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderId", orderList.OrderId);
            return View(orderList);
        }

        // GET: OrderLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderList orderList = db.OrderLists.Find(id);
            if (orderList == null)
            {
                return HttpNotFound();
            }
            /*            ViewBag.BasketId = new SelectList(db.Baskets, "BasketId", "BasketId", orderList.BasketId);
            */
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderId", orderList.OrderId);
            return View(orderList);
        }

        // POST: OrderLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,BasketId,ProductId,Price,Quantity,CreatedAt,Status,Id")] OrderList orderList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            /*            ViewBag.BasketId = new SelectList(db.Baskets, "BasketId", "BasketId", orderList.BasketId);
            */
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderId", orderList.OrderId);
            return View(orderList);
        }

        // GET: OrderLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderList orderList = db.OrderLists.Find(id);
            if (orderList == null)
            {
                return HttpNotFound();
            }
            return View(orderList);
        }

        // POST: OrderLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderList orderList = db.OrderLists.Find(id);
            db.OrderLists.Remove(orderList);
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
