using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ekart_mvc.Models;
using Ekart_mvc.Models.order;
using Ekart_mvc.Models.Class2;
using RestSharp.Extensions;

namespace Ekart_mvc.Controllers
{
    public class ProductListsController : Controller
    {
        private EkartEntities7 db = new EkartEntities7();

        // GET: ProductLists

        public ActionResult RecIndex()
        {
            var records = db.Records.Include(r => r.ProductList);
            return View(records.ToList());
        }

        public ActionResult Index()
        {
            var productLists = db.ProductLists.Include(p => p.CategoryList).Where(p => p.CategoryList.IsActive.Equals("Y"));
            return View(productLists.ToList());
        }

        // GET: ProductLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductList productList = db.ProductLists.Find(id);
            if (productList == null)
            {
                return HttpNotFound();
            }
            return View(productList);
        }

        // GET: ProductLists/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.CategoryLists.Where(p => p.IsActive.Equals("Y")), "CategoryId", "CategoryName");
            return View();
        }
        
        // POST: ProductLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
/*        [Obsolete]
*/       
        public ActionResult Create([Bind(Include = "ProductId,Unit,ProductName,Price,CreatedAt,CreatedBy,TotalQun,CurrentQun,CategoryId,IsActive,Image,Image1,ImageFile")] ProductList productList , HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
         
                using (EkartEntities7 db = new EkartEntities7())
                { 
                    var v = db.ProductLists.Where(a => a.ProductName.Equals(productList.ProductName)).FirstOrDefault();
                    if (v == null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(productList.Image1);
                        if (file != null)
                        {
                            productList.Image1 = /*"~/Content/ProductImages/" +*/ DateTime.Now.ToString("yymmssfff") + productList.ProductId + Path.GetExtension(file.FileName);
                            file.SaveAs(Server.MapPath("//Content//ProductImages//") + productList.Image1);
                      
                            productList.IsActive = "Y";
                        productList.CreatedBy = Session["userName"].ToString();
                        productList.CreatedAt = DateTime.Now;
                        productList.TotalQun = productList.CurrentQun;
                        /*  db.ProductLists.Add(productList);
                          db.SaveChanges();
                          return RedirectToAction("Index");*/

                        /* if (file != null)
                         {
                             productList.Image1 = productList.Image1+Path.GetExtension(file.FileName);
                             file.SaveAs(Server.MapPath("//Content//ProductImages//") + productList.Image1);
                             db.ProductLists.Add(productList);
                             db.SaveChanges();
                             return RedirectToAction("Index");
                         }*/
                     

                        db.ProductLists.Add(productList);
                        db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert('Product Is Already Peresent'); window.location.replace('Create');</script>");
                    }
                }
            else
            {
               
            }

            ViewBag.CategoryId = new SelectList(db.CategoryLists, "CategoryId", "CategoryName", productList.CategoryId);
            return View(productList);
        }

        // GET: ProductLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductList productList = db.ProductLists.Find(id);
            if (productList == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.CategoryLists.Where(p => p.IsActive.Equals("Y")), "CategoryId", "CategoryName", productList.CategoryId);
            return View(productList);
        }

        // POST: ProductLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Unit,ProductName,Price,CreatedAt,CreatedBy,TotalQun,CurrentQun,CategoryId,IsActive,Image,Image1")] ProductList productList)
        {
            if (ModelState.IsValid)
            {

                /*   productList.CreatedAt = DateTime.Now;
                   productList.CreatedBy = Session["userName"].ToString();
                   *//*  ProductList product = new ProductList();


                     product = db.ProductLists.Where(u => u.ProductName == productList.ProductName).SingleOrDefault();
                     product.TotalQun = (productList.CurrentQun + product.TotalQun);
                     product.CurrentQun = (productList.CurrentQun + product.CurrentQun);

     */
                /* var productList1 = db.ProductLists.Where(u => u.ProductName == productList.ProductName).SingleOrDefault();
                  productList1.TotalQun = productList1.TotalQun + productList.CurrentQun;
                  productList1.CurrentQun = productList1.CurrentQun + productList.CurrentQun;

                  db.ProductLists.Add(productList);
                  db.SaveChanges();
                  return RedirectToAction("Index");*//*
                db.Entry(productList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.CategoryLists, "CategoryId", "CategoryName", productList.CategoryId);
            return View(productList);*/
                using (EkartEntities7 db = new EkartEntities7())
                {
                    var v = db.ProductLists.Where(a => a.ProductName.Equals(productList.ProductName)).FirstOrDefault();
                    if (v == null)
                    {
                        productList.CreatedAt = DateTime.Now;
                        productList.CreatedBy = Session["userName"].ToString();
                        /*  categoryList.IsActive = "Y";*/

                        db.Entry(productList).State = EntityState.Modified;

                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        /*                      CategoryList c1=  db.CategoryLists.Where(a => a.CategoryId == categoryList.CategoryId).SingleOrDefault();
                         *                      db.CategoryLists.Find(id);
                         *                      
                         *                      
                        */
                        ProductList c1 = db.ProductLists.Find(productList.ProductId);
                        if (c1.ProductName == productList.ProductName)
                        {
                            /*                            db.Entry(categoryList).State = EntityState.Modified;
                            */
                            c1.CreatedAt = productList.CreatedAt = DateTime.Now;
                            c1.CreatedBy = productList.CreatedBy = Session["userName"].ToString();
                            c1.IsActive = productList.IsActive;
                            c1.CategoryId = productList.CategoryId;
                            c1.Price = productList.Price;
                            c1.Unit = productList.Unit;
                            /* categoryList.CreatedAt = DateTime.Now;
                             db.Entry(categoryList).State = EntityState.Modified;*/
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                            return Content("<script language='javascript' type='text/javascript'>alert('Product Is Already Peresent'); window.location.replace('Edit');</script>");
                    }



                }
            }
            return View(productList);

        }

        public ActionResult Quantity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductList productList = db.ProductLists.Find(id);
            if (productList == null)
            {
                return HttpNotFound();
            }
            return View(productList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Quantity([Bind(Include = "ProductId,ProductName,Price,CreatedAt,CreatedBy,TotalQun,CurrentQun,CategoryId,IsActive,Image,Image1")] ProductList productList)
        {
            if (ModelState.IsValid)
            {

               
                /*  ProductList product = new ProductList();


                  product = db.ProductLists.Where(u => u.ProductName == productList.ProductName).SingleOrDefault();
                  product.TotalQun = (productList.CurrentQun + product.TotalQun);
                  product.CurrentQun = (productList.CurrentQun + product.CurrentQun);

  */
                ProductList p1 = db.ProductLists.Find(productList.ProductId);
                p1.CreatedAt = DateTime.Now;
                p1.CreatedBy = Session["userName"].ToString();
                p1.TotalQun = p1.TotalQun;
                p1.CurrentQun = p1.CurrentQun + productList.CurrentQun;

                Record rc = new Record();
                rc.AddAt = DateTime.Now.Date;
                rc.AddBy = Session["userName"].ToString();
                rc.ProductId = productList.ProductId;
                rc.Quantity = productList.CurrentQun;
                rc.CurrentQuantity = p1.CurrentQun;
                db.Records.Add(rc);
                /*  db.ProductLists.Add(productList);
                  db.SaveChanges();
                  return RedirectToAction("Index");*/
                /*                db.Entry(productList).State = EntityState.Modified;
                */

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.CategoryLists, "CategoryId", "CategoryName", productList.CategoryId);
            return View(productList);
        }
        // GET: ProductLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductList productList = db.ProductLists.Find(id);
            if (productList == null)
            {
                return HttpNotFound();
            }
            return View(productList);
        }

        // POST: ProductLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductList productList = db.ProductLists.Find(id);
            db.ProductLists.Remove(productList);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Logout()
        {
            Session["userName"] = null;
            return RedirectToAction("Login", "Home");
            /*    return View();*/
        }

        public ActionResult Search()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(ProductList p1)
        {
            Session["Product"] = p1.ProductName;
            using (EkartEntities7 dc = new EkartEntities7())
            {
                var v = dc.ProductLists.Where(a => a.ProductName.Equals(p1.ProductName)).FirstOrDefault();
                if (v != null)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Product is Found'); window.location.replace('AddQun');</script>");
                }
                else
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Product Not Found'); window.location.replace('Search');</script>");
                }
            }
        }
        public ActionResult AddQun()
        {
            return View();
        }
        public ActionResult AddQun(Class2 p1)
        {

           
                ProductList product = new ProductList();

                string s1 = Session["Product"].ToString();
                product = db.ProductLists.Where(u => u.ProductName == s1).SingleOrDefault();
                product.TotalQun = (p1.Quantity + product.TotalQun);
                product.CurrentQun = (p1.Quantity + product.CurrentQun);
                db.SaveChanges();

                if (product != null)
                    return Content("<script language='javascript' type='text/javascript'>alert('Quantity Update Successfully'); window.location.replace('Index');</script>");
                else
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Some Error occure Please Try again after some time'); window.location.replace('Logout');</script>");
                }
            
        }
        
        public ActionResult ProductHome()
        { return View(); }

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
