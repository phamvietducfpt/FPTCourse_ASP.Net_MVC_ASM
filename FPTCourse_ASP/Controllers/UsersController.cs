using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FPTCourse_ASP.Models;

namespace FPTCourse_ASP.Controllers
{
    
    public class UsersController : Controller
    {
        private ManageCourseEntities db = new ManageCourseEntities();
        //[Authorize(Roles ="StaffIndex")]
        [Authorize(Roles = "Quantri,IndexStaff")]
        // GET: Users
        public ActionResult Index()
        {
            var user = db.User.Include(u => u.User_Permission1);
            return View(user.ToList());
        }

        // GET: Users/Details/5
        [Authorize(Roles = "ManageAccount,IndexStaff,Quantri")]
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //User user = db.User.Find(id);
            //if (user == null)
            //{
            //    return HttpNotFound();
            //}
            User users = (User)Session["User_Username"];
            return View(users);
        }
        [Authorize(Roles = "Dangky,Quantri")]
        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.User_Permission = new SelectList(db.User_Permission, "User_Permission1", "User_Permission_Name");
            return View();
        }
        [Authorize(Roles = "Dangky,Quantri")]
        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_ID,User_Name,User_Email,User_Phone,User_Address,User_Permission,User_Username,User_Password")] User user,string User_Username)
        {
            if (ModelState.IsValid)
            {
                User user_detail = new User();
                user_detail = db.User.Where(n => n.User_Username.ToLower() == User_Username.ToLower()).FirstOrDefault();
                if (user_detail != null)
                {
                    ViewBag.thongbao = "User name is exist";
                    ViewBag.User_Permission = new SelectList(db.User_Permission, "User_Permission1", "User_Permission_Name");
                    return View(user);
                }             
                db.User.Add(user);
                db.SaveChanges();
                ViewBag.User_Permission = new SelectList(db.User_Permission, "User_Permission1", "User_Permission_Name");
                ViewBag.thongbao = "Create successfully";
                 return View();
            }

            ViewBag.User_Permission = new SelectList(db.User_Permission, "User_Permission1", "User_Permission_Name", user.User_Permission);
            return View(user);
        }
        [Authorize(Roles = "IndexStaff,Quantri")]
        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            //User user = (User)Session["User_Username"];
            ViewBag.User_Permission = new SelectList(db.User_Permission, "User_Permission1", "User_Permission_Name", user.User_Permission);         
            return View(user);
        }
        
        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "IndexStaff,Quantri")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_ID,User_Name,User_Email,User_Phone,User_Address,User_Permission,User_Username,User_Password")] User user,string User_Username,string User_ID,FormCollection f)
        {
            if (ModelState.IsValid)
            {
                User user_detail = new User();
                user_detail = db.User.Where(n => n.User_Username.ToLower() == User_Username.ToLower() && n.User_ID.ToString() != User_ID.ToString()).FirstOrDefault();
                if (user_detail != null)
                {

                    ViewBag.User_Permission = new SelectList(db.User_Permission, "User_Permission1", "User_Permission_Name", user.User_Permission);
                    ViewBag.thongbao = "User name is exist";
                    return View(user);

                }
                string passwordc = f["User_OldPassword"].ToString();
                User users = db.User.Where(n => n.User_Password == passwordc).FirstOrDefault();
                if (passwordc == "password" || users != null)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.User_Permission = new SelectList(db.User_Permission, "User_Permission1", "User_Permission_Name", user.User_Permission);
                    ViewBag.thongbao = "Update successfully";
                    return View();
                }
                else
                {
                    ViewBag.User_Permission = new SelectList(db.User_Permission, "User_Permission1", "User_Permission_Name", user.User_Permission);
                    ViewBag.thongbao = "Password is incorrect";
                    return View(user);
                }
            }
            ViewBag.User_Permission = new SelectList(db.User_Permission, "User_Permission1", "User_Permission_Name", user.User_Permission);
            return View(user);
        }
        [Authorize(Roles = "ManageAccount")]
        public ActionResult TraineEdit()
        {
            //string usernamec = f["User_Username"].ToString();
            //string passwordc = f["User_Password"].ToString();
            //User users = db.User.SingleOrDefault(n => n.User_Username == usernamec && n.User_Password == passwordc);
            //if (users != null)
            //{
            User users = (User)Session["User_Username"];
            return View(users);
        }
        [Authorize(Roles = "ManageAccount")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TraineEdit([Bind(Include = "User_ID,User_Name,User_Email,User_Phone,User_Address,User_Permission,User_Username,User_Password")] User user, string User_Username, string User_ID, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                User user_detail = new User();
                user_detail = db.User.Where(n => n.User_Username.ToLower() == User_Username.ToLower() && n.User_ID.ToString() != User_ID.ToString()).FirstOrDefault();
                if (user_detail != null)
                {

                    ViewBag.User_Permission = new SelectList(db.User_Permission, "User_Permission1", "User_Permission_Name", user.User_Permission);
                    ViewBag.thongbao = "User name is exist";
                    return View();
                }
                string passwordc = f["User_OldPassword"].ToString();
                User users = db.User.Where(n => n.User_Password == passwordc).FirstOrDefault();
                if (passwordc == "password" || users != null)
                {
                    ViewBag.thongbao = "Update successfully";
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return View(user);
                }
                else
                {
                    ViewBag.User_Permission = new SelectList(db.User_Permission, "User_Permission1", "User_Permission_Name", user.User_Permission);
                    ViewBag.thongbao = "Password is incorrect";
                    return View();
                }
            }
            ViewBag.thongbao = "Update failed";
            return View(user);
        }
        [Authorize(Roles = "Delete,Quantri,IndexStaff")]
        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [Authorize(Roles = "Delete,Quantri,IndexStaff")]
        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
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
