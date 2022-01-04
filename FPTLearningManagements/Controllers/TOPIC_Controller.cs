using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FPTLearningManagement.CustomFilters;
using FPTLearningManagement.Models;

namespace FPTLearningManagement.Controllers
{
    public class TOPIC_Controller : Controller
    {
        private System_CategoryEntities db = new System_CategoryEntities();

        // GET: TOPIC_
        [AuthLog(Roles = "Admin,User, Staff, Student, Trainee")]
        public ActionResult Index()
        {
            var tOPICs = db.TOPICs.Include(t => t.COURSE).Include(t => t.Instructor);
            return View(tOPICs.ToList());
        }

        // GET: TOPIC_/Details/5
        [AuthLog(Roles = "Admin,User, Staff, Student, Trainee")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TOPIC tOPIC = db.TOPICs.Find(id);
            if (tOPIC == null)
            {
                return HttpNotFound();
            }
            return View(tOPIC);
        }

        // GET: TOPIC_/Create
        [AuthLog(Roles = "Admin, Staff")]
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.COURSEs, "CourseId", "CourseName");
            ViewBag.InstructorId = new SelectList(db.Instructors, "InstructorId", "InstructorName");
            return View();
        }

        // POST: TOPIC_/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TopicId,CourseId,TopicName,Class,InstructorId")] TOPIC tOPIC)
        {
            if (ModelState.IsValid)
            {
                db.TOPICs.Add(tOPIC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.COURSEs, "CourseId", "CourseName", tOPIC.CourseId);
            ViewBag.InstructorId = new SelectList(db.Instructors, "InstructorId", "InstructorName", tOPIC.InstructorId);
            return View(tOPIC);
        }

        // GET: TOPIC_/Edit/5
        [AuthLog(Roles = "Admin, Staff")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TOPIC tOPIC = db.TOPICs.Find(id);
            if (tOPIC == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.COURSEs, "CourseId", "CourseName", tOPIC.CourseId);
            ViewBag.InstructorId = new SelectList(db.Instructors, "InstructorId", "InstructorName", tOPIC.InstructorId);
            return View(tOPIC);
        }

        // POST: TOPIC_/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TopicId,CourseId,TopicName,Class,InstructorId")] TOPIC tOPIC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tOPIC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.COURSEs, "CourseId", "CourseName", tOPIC.CourseId);
            ViewBag.InstructorId = new SelectList(db.Instructors, "InstructorId", "InstructorName", tOPIC.InstructorId);
            return View(tOPIC);
        }

        // GET: TOPIC_/Delete/5
        [AuthLog(Roles = "Admin, Staff")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TOPIC tOPIC = db.TOPICs.Find(id);
            if (tOPIC == null)
            {
                return HttpNotFound();
            }
            return View(tOPIC);
        }

        // POST: TOPIC_/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TOPIC tOPIC = db.TOPICs.Find(id);
            db.TOPICs.Remove(tOPIC);
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
