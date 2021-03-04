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



namespace Ekart_mvc.Controllers
{
   
    public class HomeController : Controller
    {

        public int otp1;
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Register obj)
        {
            if (ModelState.IsValid)
            {
                EkartEntities db = new EkartEntities();
                obj.RegisterTime = DateTime.Now;
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
            
}

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Register register)
        {
           
                using (EkartEntities dc = new EkartEntities())
                {
                    var v = dc.Registers.Where(a => a.Email.Equals(register.Email) && a.Password.Equals(register.Password)).FirstOrDefault();
                    if(v!=null)
                    return RedirectToAction("Index");
                    else
                    return RedirectToAction("Login");

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
            using (EkartEntities dc = new EkartEntities())
            {
                var v = dc.Registers.Where(a => a.Contact.Equals(register.Contact)).FirstOrDefault();
                if (v != null)
                {
                     int _min = 0000;
                     int _max = 9999;
                     Random _rdm = new Random();
                     int otp = _rdm.Next(_min, _max);
                    otp1 = otp;
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
                    Session["OTP"] = otp1;
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
            
            if(otp1.ToString().Equals(o1.Otp))
            {
                return RedirectToAction("ChangePass");
            }
            else
           return Content("<script language='javascript' type='text/javascript'>alert('Please Enter Corract OTP !!!'); window.location.replace('EnterOtp');</script>");

        }


    }

           

    }
