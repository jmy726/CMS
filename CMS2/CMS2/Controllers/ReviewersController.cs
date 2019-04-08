using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS2.Models;

namespace CMS2.Controllers
{
    public class ReviewersController : Controller
    {
        private CMS db = new CMS();

        // GET: Reviewers
        public ActionResult Index()
        {
            var reviewers = db.Reviewers.Include(r => r.User);
            return View(reviewers.ToList());
        }

        // GET: Reviewers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reviewer reviewer = db.Reviewers.Find(id);
            if (reviewer == null)
            {
                return HttpNotFound();
            }
            return View(reviewer);
        }

        // GET: Reviewers/Create
        public ActionResult Create()
        {
            ViewBag.keywordId = new SelectList(db.Keywords, "keywordId", "keyword1");
            ViewBag.reviewerId = new SelectList(db.Users, "userId", "name");
            return View();
        }

        // POST: Reviewers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "reviewerId,keywordId")] Reviewer reviewer)
        {
            if (ModelState.IsValid)
            {
                db.Reviewers.Add(reviewer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            ViewBag.reviewerId = new SelectList(db.Users, "userId", "name", reviewer.reviewerId);
            return View(reviewer);
        }

        // GET: Reviewers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reviewer reviewer = db.Reviewers.Find(id);
            if (reviewer == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.reviewerId = new SelectList(db.Users, "userId", "name", reviewer.reviewerId);
            return View(reviewer);
        }

        // POST: Reviewers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "reviewerId,keywordId")] Reviewer reviewer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reviewer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.reviewerId = new SelectList(db.Users, "userId", "name", reviewer.reviewerId);
            return View(reviewer);
        }

        // GET: Reviewers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reviewer reviewer = db.Reviewers.Find(id);
            if (reviewer == null)
            {
                return HttpNotFound();
            }
            return View(reviewer);
        }

        // POST: Reviewers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Reviewer reviewer = db.Reviewers.Find(id);
            db.Reviewers.Remove(reviewer);
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
