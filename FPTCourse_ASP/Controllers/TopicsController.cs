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
    public class TopicsController : Controller
    {   
        private ManageCourseEntities db = new ManageCourseEntities();

        // GET: Topics
        [Authorize(Roles = "IndexStaff,ManageTopic,Quantri")]
        public ActionResult Index()
        {
            return View(db.Topic.ToList());
        }

        [Authorize(Roles = "ManageAccount,ManageTopic,Quantri")]
        public ActionResult TraineIndex()
        {
            return View(db.Topic.ToList());
        }
        [Authorize(Roles = "ManageAccount,ManageTopic,Quantri")]
        // GET: Topics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topic.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }
        [Authorize(Roles = "ManageTopic,Quantri")]
        // GET: Topics/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "ManageTopic,Quantri")]
        // POST: Topics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Topic_ID,Topic_Name")] Topic topic,string Topic_Name)
        {
            if (ModelState.IsValid)
            {
                Topic topic_detail = new Topic();
                topic_detail = db.Topic.Where(n => n.Topic_Name.ToLower() == Topic_Name.ToLower()).FirstOrDefault();

                if (topic_detail != null)
                {
                    ViewBag.thongbao = "Topic name is exist";
                    return View(topic);
                }
                else
                {
                    ViewBag.thongbao = "Insert successfully";
                    db.Topic.Add(topic);
                    db.SaveChanges();
                    ViewBag.thongbao = "Create successfully";
                    return View();
                }
                //db.Topic.Add(topic);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }

            return View(topic);
        }
        [Authorize(Roles = "ManageTopic,Quantri")]
        // GET: Topics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topic.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }
        [Authorize(Roles = "ManageTopic,Quantri")]
        // POST: Topics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Topic_ID,Topic_Name")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.thongbao = "Update successfully";
                return View();
            }
            return View(topic);
        }
        [Authorize(Roles = "ManageTopic,Quantri")]
        // GET: Topics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topic.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }
        [Authorize(Roles = "ManageTopic,Quantri")]
        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Topic topic = db.Topic.Find(id);
            db.Topic.Remove(topic);
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
