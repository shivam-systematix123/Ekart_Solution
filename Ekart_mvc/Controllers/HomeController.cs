using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Ekart_mvc.Models;
using RestSharp;
using System.Web.Services.Description;
using TechTalk.SpecFlow.CommonModels;
using Xceed.Wpf.Toolkit;
using System.Web.UI;
using DocumentFormat.OpenXml.Math;
using Ekart_mvc.Models.Otp;
using System.ServiceModel.Channels;
using Ekart_mvc.Models.changep;
using System.Net;
using System.Data.Entity;

namespace Ekart_mvc.Controllers
{
   
    public class HomeController : Controller 
    {

       private EkartEntities1 db = new EkartEntities1();
        public ActionResult Home()
        {
            return View();
        }
        public int otp1;
        public ActionResult Index()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]

    
        public ActionResult Index(Register obj)
        {
            if (ModelState.IsValid)
            {
                using (EkartEntities1 db = new EkartEntities1())
                {
                    var v = db.Registers.Where(a => a.Email.Equals(obj.Email) || a.Contact.Equals(obj.Contact)).FirstOrDefault();
                    if (v!=null)
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert('Email Or Contact Is already Register Please Enter Different Data !!!'); window.location.replace('Index');</script>");
                    }
                    else
                    {
                        obj.RegisterTime = DateTime.Now.Date;
                        obj.Roal = "User"; 
                        obj.IsActive = "Y";
                        db.Registers.Add(obj);
                        db.SaveChanges();

                        /*                        return Content("<script language='javascript' type='text/javascript'>alert('Your Registration Successful Please Login !!!'); window.location.replace('Login');</script>");
                        */
                        Session["check"] = "Yes";
                        return RedirectToActionPermanent("Login", "Home");
                    }
                }

            }
            else
            {
                return RedirectToAction("Index");

            }
            /* obj.RegisterTime = DateTime.Now;
             obj.Roal = "User";
             obj.IsActive = "Y";
             db.Registers.Add(obj);
             db.SaveChanges();
             obj.FirstName = null;
             return Content("<script language='javascript' type='text/javascript'>alert('Your Registration Successful Please Login !!!'); window.location.replace('ForgotPass');</script>");

         }
         else
         {
             return RedirectToAction("Index");

         }
*/
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Register register)
        {
           
                using (EkartEntities1 dc = new EkartEntities1())
                {
                    var v = dc.Registers.Where(a => a.Email.Equals(register.Email) && a.Password.Equals(register.Password)).FirstOrDefault();
                if (v != null)
                {
                    
                    Session["userName"] = register.Email;
                  
                    Register r = db.Registers.Where(a => a.Email == register.Email).SingleOrDefault();
                    if (r.IsActive == "Y")
                    {
                        Session["Userid"] = r.UserId;
                        if (r.Roal == "Admin")
                        {
                            TempData["message"] = "Save successfully";

                            return RedirectToActionPermanent("Index", "Admin");
                            /*                        return View("~/Views/CategoryLists/Index.cshtml");
                            */                        /*                        return Content("<script language='javascript' type='text/javascript'>alert('You Are Not Registered User Please Register First !!!'); window.location.replace('Login');</script>");
                                                    */
                        }
                        else
                        {
                            Session["Uid"] = register.UserId;

                            return RedirectToActionPermanent("Index", "BasketItems");
                        }
                    }
                    else
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert('You Cant Access The Site Beacuse You Are Not Active User'); window.location.replace('Index');</script>");
                    }
                }
                else
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('You Are Not Registered User Please Register First !!!'); window.location.replace('Login');</script>");
                }
            }
               
            }
        public ActionResult ForgotPass()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPass(Register register)
        {
            using (EkartEntities1 dc = new EkartEntities1())
            {
                var v = dc.Registers.Where(a => a.Contact.Equals(register.Contact)).FirstOrDefault();
                if (v != null)
                {
                     int _min = 0000;
                     int _max = 9999;
                     Random _rdm = new Random();
                     int otp = _rdm.Next(_min, _max);
                    Session["OTP"] = otp;
                    var client = new RestClient("https://www.fast2sms.com/dev/bulkV2");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddHeader("authorization", "WM7EV9OTgK1qoP2J4ltzfvNC0cL5rhmbdXSHjuZAU8IGFyYsQRyp5htjnO3BfMkuZiPFco1bS74NQrJU");
                    request.AddParameter("sender_id", "CHKSMS");
                    request.AddParameter("message", "2");
                    request.AddParameter("variables_values", otp);
                    request.AddParameter("route", "s");
                    request.AddParameter("numbers", register.Contact);
                    IRestResponse response = client.Execute(request);
                   
                    Session["Id"] = register.Contact;
                    return RedirectToAction("EnterOtp");
                }
                else
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Please Enter Corract Contact Number !!!'); window.location.replace('ForgotPass');</script>");
                }
            }

        }
          public ActionResult EnterOtp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnterOtp(OTP o1)
        {
            
            if(Session["OTP"].ToString().Equals(o1.Otp.ToString()))
            {
                return RedirectToAction("ChangePass");
            }
            else
           return Content("<script language='javascript' type='text/javascript'>alert('Please Enter Corract OTP !!!'); window.location.replace('EnterOtp');</script>");

        }

        public ActionResult ChangePass()
        {
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePass(changp p1)
        { 
            if (ModelState.IsValid)
            {
                Register register = new Register();
                
                long x = long.Parse(Session["Id"].ToString());
                register = db.Registers.Where(u => u.Contact == x.ToString()).SingleOrDefault();
                register.Password = p1.Password;
                db.SaveChanges();
              
                if (register != null)
                    return Content("<script language='javascript' type='text/javascript'>alert('PassWord Change Successfully'); window.location.replace('Login');</script>");
                else
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Some Error occure Please Try again after some time'); window.location.replace('Login');</script>");
                }
            }
            return View();
        }


    }



}
