using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FPTCourse_ASP.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;
using System.Web.UI;
using System.Web.Security;

namespace FPTCourse_ASP.Controllers
{
    public class HomeController : Controller
    {
        ManageCourseEntities db = new ManageCourseEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //[HttpGet]
        //public ActionResult Dangky()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Dangky(User users,FormCollection f)
        //{
        //    //check captcha//
        //    if (this.IsCaptchaValid("Captcha isn't Valid"))
        //    {              
        //        ViewBag.thongbao = "Captcha Successfully";
        //        //add to database//
        //        db.User.Add(users);
        //        db.SaveChanges();
               
        //    }    
        //        Response.Write("<script>alert('Wrong Captcha,please try again');</script>");            
        //    //ViewBag.thongbao = "Wrong Captcha,please try again";
        //    //return View("~/Views/Home/Index.cshtml");
        //    return RedirectToAction("Index");
            
        //}
        [HttpPost]
        public ActionResult Login( FormCollection f) 
        {
            //Check username and password//
            string usernamec = f["User_Username"].ToString();
            string passwordc = f["User_Password"].ToString();
            User users = db.User.SingleOrDefault(n => n.User_Username == usernamec && n.User_Password == passwordc);   
                if (users != null)
                {
                var lstQuyen = db.UserPer_Permission.Where(n => n.User_Permission == users.User_Permission);
                //IEnumberable <UserPer_Permission> 1stQuyen = db.UserPer_Permission.Where(n => n.User_Permission == users.User_Permission);
                string Permission = "";
                if (lstQuyen.Count() != 0) { 
                    foreach (var item in lstQuyen)
                    {
                        Permission += item.Permission.Permission_ID + ",";
                    }
                    Permission = Permission.Substring(0, Permission.Length - 1);
                    PhanQuyen(users.User_Username, Permission);
                    Session["User_Username"] = users;
                    return RedirectToAction("Index");
                }
            }
            return Content("Username and password incorrect!");          
        }
        public void PhanQuyen(string User_Username, string Permission)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1,
               User_Username, DateTime.Now, DateTime.Now.AddHours(3), false, Permission, FormsAuthentication.FormsCookiePath);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);
        }
       public ActionResult ErrorPage()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["User_Username"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }   
    }
}