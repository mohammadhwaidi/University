using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using University.Models;

namespace University.Controllers
{
    public class StudentExamGradesController : Controller
    {
        private UniversityEntities db = new UniversityEntities();

        // GET: StudentExamGrades
        public ActionResult Index()
        {
            var studentExamGrades = db.StudentExamGrades.Include(s => s.Exam).Include(s => s.Grade).Include(s => s.Student);
            return View(studentExamGrades.ToList());
        }

        // GET: StudentExamGrades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentExamGrade studentExamGrade = db.StudentExamGrades.Find(id);
            if (studentExamGrade == null)
            {
                return HttpNotFound();
            }
            return View(studentExamGrade);
        }

        // GET: StudentExamGrades/Create
        public ActionResult Create()
        {
            ViewBag.ExamID = new SelectList(db.Exams, "ExamID", "ExamID");
            ViewBag.GradeID = new SelectList(db.Grades, "GradeID", "GradeValue");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName");
            return View();
        }

        // POST: StudentExamGrades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentExamGradeID,StudentID,ExamID,GradeID")] StudentExamGrade studentExamGrade)
        {
            if (ModelState.IsValid)
            {
                db.StudentExamGrades.Add(studentExamGrade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExamID = new SelectList(db.Exams, "ExamID", "ExamID", studentExamGrade.ExamID);
            ViewBag.GradeID = new SelectList(db.Grades, "GradeID", "GradeValue", studentExamGrade.GradeID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", studentExamGrade.StudentID);
            return View(studentExamGrade);
        }

        // GET: StudentExamGrades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentExamGrade studentExamGrade = db.StudentExamGrades.Find(id);
            if (studentExamGrade == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExamID = new SelectList(db.Exams, "ExamID", "ExamID", studentExamGrade.ExamID);
            ViewBag.GradeID = new SelectList(db.Grades, "GradeID", "GradeValue", studentExamGrade.GradeID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", studentExamGrade.StudentID);
            return View(studentExamGrade);
        }

        // POST: StudentExamGrades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentExamGradeID,StudentID,ExamID,GradeID")] StudentExamGrade studentExamGrade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentExamGrade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExamID = new SelectList(db.Exams, "ExamID", "ExamID", studentExamGrade.ExamID);
            ViewBag.GradeID = new SelectList(db.Grades, "GradeID", "GradeValue", studentExamGrade.GradeID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", studentExamGrade.StudentID);
            return View(studentExamGrade);
        }

        // GET: StudentExamGrades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentExamGrade studentExamGrade = db.StudentExamGrades.Find(id);
            if (studentExamGrade == null)
            {
                return HttpNotFound();
            }
            return View(studentExamGrade);
        }

        // POST: StudentExamGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentExamGrade studentExamGrade = db.StudentExamGrades.Find(id);
            db.StudentExamGrades.Remove(studentExamGrade);
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
