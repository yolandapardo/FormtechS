using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FormtechS.Models;

namespace FormtechS.Controllers
{
    public class TypeOfSurveysController : Controller
    {
        private FormtechDb db = new FormtechDb();

        // GET: TypeOfSurveys
        public ActionResult Index()
        {
            return View(db.TypeOfSurveys.ToList());
        }

        // GET: TypeOfSurveys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfSurvey typeOfSurvey = db.TypeOfSurveys.Find(id);
            if (typeOfSurvey == null)
            {
                return HttpNotFound();
            }
            return View(typeOfSurvey);
        }

        // GET: TypeOfSurveys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeOfSurveys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type")] TypeOfSurvey typeOfSurvey)
        {
            if (ModelState.IsValid)
            {
                bool exist = db.TypeOfSurveys.Any(t => t.Type == typeOfSurvey.Type);
                if (!exist)
                {
                    db.TypeOfSurveys.Add(typeOfSurvey);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError(string.Empty, "Type of Survey already exists.");
            }

            return View(typeOfSurvey);
        }

        // GET: TypeOfSurveys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfSurvey typeOfSurvey = db.TypeOfSurveys.Find(id);
            if (typeOfSurvey == null)
            {
                return HttpNotFound();
            }
            return View(typeOfSurvey);
        }

        // POST: TypeOfSurveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type")] TypeOfSurvey typeOfSurvey)
        {
            if (ModelState.IsValid)
            {
                bool exist = db.TypeOfSurveys.Any(t => (t.Type == typeOfSurvey.Type && t.Id!=typeOfSurvey.Id));
                if (!exist)
                {
                    db.Entry(typeOfSurvey).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError(string.Empty, "Type of Survey already exists.");
            }
            return View(typeOfSurvey);
        }

        // GET: TypeOfSurveys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfSurvey typeOfSurvey = db.TypeOfSurveys.Find(id);
            if (typeOfSurvey == null)
            {
                return HttpNotFound();
            }
            return View(typeOfSurvey);
        }

        // POST: TypeOfSurveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeOfSurvey typeOfSurvey = db.TypeOfSurveys.Find(id);
            db.TypeOfSurveys.Remove(typeOfSurvey);
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
