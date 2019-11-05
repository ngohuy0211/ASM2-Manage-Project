using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManageProject.Models;

namespace ManageProject.Controllers
{
    public class CoursesController : Controller
    {
        private ManageASMEntities1 db = new ManageASMEntities1();

        // GET: Courses
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.CourseCategory).Include(c => c.Topic).Include(c => c.Trainer);
            return View(courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Create()
        {
            ViewBag.CourseCategoryID = new SelectList(db.CourseCategories, "CourseCategoryID", "CourseCategoryName");
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName");
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "TrainerName");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,CourseCategoryID,TrainerID,TopicID,CourseName,CourseDetail")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseCategoryID = new SelectList(db.CourseCategories, "CourseCategoryID", "CourseCategoryName", course.CourseCategoryID);
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName", course.TopicID);
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "TrainerName", course.TrainerID);
            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseCategoryID = new SelectList(db.CourseCategories, "CourseCategoryID", "CourseCategoryName", course.CourseCategoryID);
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName", course.TopicID);
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "TrainerName", course.TrainerID);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseID,CourseCategoryID,TrainerID,TopicID,CourseName,CourseDetail")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseCategoryID = new SelectList(db.CourseCategories, "CourseCategoryID", "CourseCategoryName", course.CourseCategoryID);
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName", course.TopicID);
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "TrainerName", course.TrainerID);
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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
        public ActionResult Search(string huy)
        {
            var courses = db.Courses.Where(c => c.CourseID.Contains(huy));
            return View("Index", courses);
        }
    }
}
