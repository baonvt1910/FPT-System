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
    public class COURSE_Controller : Controller
    {
        private System_CategoryEntities db = new System_CategoryEntities();

        // GET: COURSE_

        [AuthLog(Roles = "Admin,User, Staff, Student, Trainee")]
        public ActionResult Index()
        {
            var cOURSEs = db.COURSEs.Include(c => c.CATEGORY);
            return View(cOURSEs.ToList());
        }

        // GET: COURSE_/Details/5
        [AuthLog(Roles = "Admin,User, Staff, Student, Trainee")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COURSE cOURSE = db.COURSEs.Find(id);
            if (cOURSE == null)
            {
                return HttpNotFound();
            }
            return View(cOURSE);
        }

        // GET: COURSE_/Create
        [AuthLog(Roles = "Admin, Staff")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.CATEGORies, "CategoryId", "CategoryName");
            return View();
        }

        // POST: COURSE_/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,CategoryId,CourseName,Description")] COURSE cOURSE)
        {
            if (ModelState.IsValid)
            {
                db.COURSEs.Add(cOURSE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.CATEGORies, "CategoryId", "CategoryName", cOURSE.CategoryId);
            return View(cOURSE);
        }

        // GET: COURSE_/Edit/5
        [AuthLog(Roles = "Admin, Staff")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COURSE cOURSE = db.COURSEs.Find(id);
            if (cOURSE == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.CATEGORies, "CategoryId", "CategoryName", cOURSE.CategoryId);
            return View(cOURSE);
        }

        // POST: COURSE_/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,CategoryId,CourseName,Description")] COURSE cOURSE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOURSE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.CATEGORies, "CategoryId", "CategoryName", cOURSE.CategoryId);
            return View(cOURSE);
        }

        // GET: COURSE_/Delete/5
        [AuthLog(Roles = "Admin, Staff")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COURSE cOURSE = db.COURSEs.Find(id);
            if (cOURSE == null)
            {
                return HttpNotFound();
            }
            return View(cOURSE);
        }

        // POST: COURSE_/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            COURSE cOURSE = db.COURSEs.Find(id);
            db.COURSEs.Remove(cOURSE);
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
