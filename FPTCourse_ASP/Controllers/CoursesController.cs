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

    public class CoursesController : Controller
    {
        private ManageCourseEntities db = new ManageCourseEntities();
        [Authorize(Roles = "IndexStaff,ManageCourse,Quantri")]
        // GET: Courses
        public ActionResult Index()
        {
            var course = db.Course.Include(c => c.CategoryCourse).Include(c => c.Topic);
            return View(course.ToList());
        }
        [Authorize(Roles = "ManageAccount,ManageCourse,Quantri")]
        // GET: Courses
        public ActionResult TraineIndex()
        {
            var course = db.Course.Include(c => c.CategoryCourse).Include(c => c.Topic);
            return View(course.ToList());
        }
        [Authorize(Roles = "ManageAccount,ManageCourse,Quantri")]
        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }
        [Authorize(Roles = "ManageCourse,Quantri")]
        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.CatCourse_ID = new SelectList(db.CategoryCourse, "CatCourse_ID", "Cat_Name");
            ViewBag.Topic_ID = new SelectList(db.Topic, "Topic_ID", "Topic_Name");
            return View();
        }
        [Authorize(Roles = "ManageCourse,Quantri")]
        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Course_ID,Course_Name,CatCourse_ID,Topic_ID")] Course course,string Course_Name)
        {
            if (ModelState.IsValid)
            {
                Course course_detail = new Course();
                course_detail = db.Course.Where(n => n.Course_Name.ToLower() == Course_Name.ToLower()).FirstOrDefault();

                if (course_detail != null)
                {
                    ViewBag.CatCourse_ID = new SelectList(db.CategoryCourse, "CatCourse_ID", "Cat_Name", course.CatCourse_ID);
                    ViewBag.Topic_ID = new SelectList(db.Topic, "Topic_ID", "Topic_Name", course.Topic_ID);
                    ViewBag.thongbao = "Course name is exist";
                    return View(course);
                }
                db.Course.Add(course);
                db.SaveChanges();
                ViewBag.CatCourse_ID = new SelectList(db.CategoryCourse, "CatCourse_ID", "Cat_Name", course.CatCourse_ID);
                ViewBag.Topic_ID = new SelectList(db.Topic, "Topic_ID", "Topic_Name", course.Topic_ID);
                ViewBag.thongbao = "Create successfully";
                return View();
            }

            ViewBag.CatCourse_ID = new SelectList(db.CategoryCourse, "CatCourse_ID", "Cat_Name", course.CatCourse_ID);
            ViewBag.Topic_ID = new SelectList(db.Topic, "Topic_ID", "Topic_Name", course.Topic_ID);
            return View(course);
        }
        [Authorize(Roles = "ManageCourse,Quantri")]
        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatCourse_ID = new SelectList(db.CategoryCourse, "CatCourse_ID", "Cat_Name", course.CatCourse_ID);
            ViewBag.Topic_ID = new SelectList(db.Topic, "Topic_ID", "Topic_Name", course.Topic_ID);
            return View(course);
        }
        [Authorize(Roles = "ManageCourse,Quantri")]
        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Course_ID,Course_Name,CatCourse_ID,Topic_ID")] Course course,string Course_Name)
        {
            if (ModelState.IsValid)
            {
                Course course_detail = new Course();
                course_detail = db.Course.Where(n => n.Course_Name.ToLower() == Course_Name.ToLower()).FirstOrDefault();

                if (course_detail != null)
                {
                    ViewBag.CatCourse_ID = new SelectList(db.CategoryCourse, "CatCourse_ID", "Cat_Name", course.CatCourse_ID);
                    ViewBag.Topic_ID = new SelectList(db.Topic, "Topic_ID", "Topic_Name", course.Topic_ID);
                    ViewBag.thongbao = "Course name is exist";
                    return View(course);
                }
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.CatCourse_ID = new SelectList(db.CategoryCourse, "CatCourse_ID", "Cat_Name", course.CatCourse_ID);
                ViewBag.Topic_ID = new SelectList(db.Topic, "Topic_ID", "Topic_Name", course.Topic_ID);
                ViewBag.thongbao = "Update successfully";
                return View(course);
            }
            ViewBag.CatCourse_ID = new SelectList(db.CategoryCourse, "CatCourse_ID", "Cat_Name", course.CatCourse_ID);
            ViewBag.Topic_ID = new SelectList(db.Topic, "Topic_ID", "Topic_Name", course.Topic_ID);
            return View(course);
        }
        [Authorize(Roles = "ManageCourse,Quantri")]
        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }
        [Authorize(Roles = "ManageCourse,Quantri")]
        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Course.Find(id);
            db.Course.Remove(course);
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
