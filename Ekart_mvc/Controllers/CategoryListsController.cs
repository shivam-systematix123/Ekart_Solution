using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ekart_mvc.Models.order;
using Ekart_mvc.Models;

namespace Ekart_mvc.Controllers
{
    public class CategoryListsController : Controller
    {
        private EkartEntities7 db = new EkartEntities7();

        public ActionResult CategoryHome()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session["userName"] = null;
            return RedirectToAction("Login", "Home");
            /*    return View();*/
        }
       
        // GET: CategoryLists
        public ActionResult Index()
        {
            return View(db.CategoryLists.ToList());
        }

        // GET: CategoryLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryList categoryList = db.CategoryLists.Find(id);
            if (categoryList == null)
            {
                return HttpNotFound();
            }
            return View(categoryList);
        }

        // GET: CategoryLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryName,CreatedAt,CreatedBy,IsActive")] CategoryList categoryList)
        {
            if (ModelState.IsValid)
            {
                using (EkartEntities7 db = new EkartEntities7() )
                {
                    var v = db.CategoryLists.Where(a => a.CategoryName.Equals(categoryList.CategoryName)).FirstOrDefault();
                    if (v == null)
                    {
                        categoryList.CreatedAt = DateTime.Now;
                        categoryList.CreatedBy = Session["userName"].ToString();
                        categoryList.IsActive = "Y";
                        db.CategoryLists.Add(categoryList);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert('Category Is Already Peresent'); window.location.replace('Create');</script>");
                    }


                   
                }
               
            }
            return View(categoryList);
        }

        // GET: CategoryLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryList categoryList = db.CategoryLists.Find(id);
            if (categoryList == null)
            {
                return HttpNotFound();
            }
            return View(categoryList);
        }

        // POST: CategoryLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName,CreatedAt,CreatedBy,IsActive")] CategoryList categoryList)
        {
            if (ModelState.IsValid)
            {

                using (EkartEntities7 db = new EkartEntities7())
                {
                    var v = db.CategoryLists.Where(a => a.CategoryName.Equals(categoryList.CategoryName)).FirstOrDefault();
                    if (v == null)
                    {
                        categoryList.CreatedAt = DateTime.Now;
                        categoryList.CreatedBy = Session["userName"].ToString();
                    /*  categoryList.IsActive = "Y";*/
              
                    db.Entry(categoryList).State = EntityState.Modified;
                 
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
                      CategoryList c1=  db.CategoryLists.Find(categoryList.CategoryId);
                        if (c1.CategoryName == categoryList.CategoryName)
                        {
                            /*                            db.Entry(categoryList).State = EntityState.Modified;
                            */
                            c1.CreatedAt = categoryList.CreatedAt = DateTime.Now;
                            c1.CreatedBy = categoryList.CreatedBy = Session["userName"].ToString();
                            c1.IsActive = categoryList.IsActive;
                            /* categoryList.CreatedAt = DateTime.Now;
                             db.Entry(categoryList).State = EntityState.Modified;*/
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        return Content("<script language='javascript' type='text/javascript'>alert('Category Is Already Peresent'); window.location.replace('Edit');</script>");
                }



            }
            }
            return View(categoryList);
        }
       
        // GET: CategoryLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryList categoryList = db.CategoryLists.Find(id);
            if (categoryList == null)
            {
                return HttpNotFound();
            }
            return View(categoryList);
        }

        // POST: CategoryLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryList categoryList = db.CategoryLists.Find(id);
           
           
                db.CategoryLists.Remove(categoryList);
                db.SaveChanges();
                return RedirectToAction("Index");
            
        }
        public ActionResult Conflict()
        {
            return View();
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
