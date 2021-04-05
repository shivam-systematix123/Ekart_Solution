using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ekart_mvc.Models;

namespace Ekart_mvc.Controllers
{
    public class UserController : Controller
    {
        private EkartEntities1 db = new EkartEntities1();

        // GET: User
        public ActionResult Index()
        {
            return View(db.Registers.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register register = db.Registers.Find(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Email,Contact,DOB,Password,SecurityQue,Answer,RegisterTime,IsActive,Roal,UserId")] Register register)
        {
            if (ModelState.IsValid)
            {
                /* register.RegisterTime = DateTime.Now;
                 db.Registers.Add(register);
                 db.SaveChanges();
                 return RedirectToAction("Index");*/


                using (EkartEntities1 db = new EkartEntities1())
                {
                    var v = db.Registers.Where(a => a.Email.Equals(register.Email) || a.Contact.Equals(register.Contact)).FirstOrDefault();
                    if (v != null)
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert('Email Or Contact Is already Register Please Enter Different Data !!!'); window.location.replace('Create');</script>");
                    }
                    else
                    {
                        register.RegisterTime = DateTimeOffset.Now;
                        db.Registers.Add(register);
                        db.SaveChanges();
                        return Content("<script language='javascript' type='text/javascript'>alert('Your Registration Successful Please Login !!!'); window.location.replace('Login');</script>");

                    }
                }
            }

            return View(register);
        }
        string s1;
        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register register = db.Registers.Find(id);
           
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FirstName,LastName,Email,Contact,DOB,Password,SecurityQue,Answer,RegisterTime,IsActive,Roal,UserId")] Register register)
        {
            if (ModelState.IsValid)
            {
                Register p1 = db.Registers.Find(register.UserId);

                /*                db.Entry(register).State = EntityState.Modified;
                */
                p1.FirstName = register.FirstName;
                p1.LastName = register.LastName;
                p1.RegisterTime = DateTime.Now.Date;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(register);
        }
           
        

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register register = db.Registers.Find(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Register register = db.Registers.Find(id);
            db.Registers.Remove(register);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UserHome()
        {
           
            return View();
        }
        public ActionResult Logout()
        {

            return RedirectToActionPermanent("Index","Home");
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
