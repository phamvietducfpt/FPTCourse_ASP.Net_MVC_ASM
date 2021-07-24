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
   
    public class CategoryCoursesController : Controller
    {
        private ManageCourseEntities db = new ManageCourseEntities();
        [Authorize(Roles = "IndexStaff,ManageCategory,Quantri")]
        // GET: CategoryCourses
        public ActionResult Index()
        {
            return View(db.CategoryCourse.ToList());
        }
        [Authorize(Roles = "ManageAccount,ManageCategory,Quantri")]
        public ActionResult TraineIndex()
        {
            return View(db.CategoryCourse.ToList());
        }
        [Authorize(Roles = "ManageAccount,ManageCategory,Quantri")]
        // GET: CategoryCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryCourse categoryCourse = db.CategoryCourse.Find(id);
            if (categoryCourse == null)
            {
                return HttpNotFound();
            }
            return View(categoryCourse);
        }
        [Authorize(Roles = "ManageCategory,Quantri")]
        // GET: CategoryCourses/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "ManageCategory,Quantri")]
        // POST: CategoryCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CatCourse_ID,Cat_Name")] CategoryCourse categoryCourse,string Cat_Name)
        {
            if (ModelState.IsValid)
            {
                CategoryCourse catcourse_detail = new CategoryCourse();
                catcourse_detail = db.CategoryCourse.Where(n => n.Cat_Name.ToLower() == Cat_Name.ToLower()).FirstOrDefault();

                if (catcourse_detail != null)
                {
                    ViewBag.thongbao = "Category name is exist";
                    return View(categoryCourse);
                }
                db.CategoryCourse.Add(categoryCourse);
                db.SaveChanges();
                ViewBag.thongbao = "Create successfully";
                return View();
            }

            return View(categoryCourse);
        }
        [Authorize(Roles = "ManageCategory,Quantri")]
        // GET: CategoryCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryCourse categoryCourse = db.CategoryCourse.Find(id);
            if (categoryCourse == null)
            {
                return HttpNotFound();
            }
            return View(categoryCourse);
        }
        [Authorize(Roles = "ManageCategory,Quantri")]
        // POST: CategoryCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CatCourse_ID,Cat_Name")] CategoryCourse categoryCourse,string Cat_Name)
        {
            if (ModelState.IsValid)
            {
                CategoryCourse catcourse_detail = new CategoryCourse();
                catcourse_detail = db.CategoryCourse.Where(n => n.Cat_Name.ToLower() == Cat_Name.ToLower()).FirstOrDefault();

                if (catcourse_detail != null)
                {
                    ViewBag.thongbao = "Category name is exist";
                    return View(categoryCourse);
                }
                db.Entry(categoryCourse).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.thongbao = "Update successfully";
                return View();
            }
            return View(categoryCourse);
        }
        [Authorize(Roles = "ManageCategory,Quantri")]
        // GET: CategoryCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryCourse categoryCourse = db.CategoryCourse.Find(id);
            if (categoryCourse == null)
            {
                return HttpNotFound();
            }
            return View(categoryCourse);
        }
        [Authorize(Roles = "ManageCategory,Quantri")]
        // POST: CategoryCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryCourse categoryCourse = db.CategoryCourse.Find(id);
            db.CategoryCourse.Remove(categoryCourse);
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
