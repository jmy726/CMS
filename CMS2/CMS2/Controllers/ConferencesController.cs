using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS2.Models;
using Microsoft.AspNet.Identity;

namespace CMS2.Controllers
{
    public class ConferencesController : Controller
    {
        
        private CMS db = new CMS();

        // GET: Conferences
        
        public ActionResult Index()
        {
            //string currentUserId = User.Identity.GetUserId();
            //var conferences = db.Conferences.Include(c => c.Chair).ToList();
            var conferences = db.Conferences.Include(c => c.Chair);
            return View(conferences.ToList());
        }

        
        public ActionResult MyConference()
        {
            string currentUserId = User.Identity.GetUserId();
            var conferences = db.Conferences.Include(c => c.Chair).ToList();
            //var conferences = db.Conferences.Include(c => c.Chair);
            return View(conferences.ToList());
        }

        // GET: Conferences/Details/5
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conference conference = db.Conferences.Find(id);
            if (conference == null)
            {
                return HttpNotFound();
            }
            return View(conference);
        }
        
        // GET: Conferences/Create
        public ActionResult Create()
        {
            ViewBag.chairId = new SelectList(db.Chairs, "chairId", "chairId");
            return View();
        }

        // POST: Conferences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include = "conferenceId,beginDate,endDate,kindOfPaperTask,numOfReviewers,topic,chairId")] Conference conference)
        {
            if (ModelState.IsValid)
            {
                db.Conferences.Add(conference);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.chairId = new SelectList(db.Chairs, "chairId", "chairId", conference.chairId);
            return View(conference);
        }

        // GET: Conferences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conference conference = db.Conferences.Find(id);
            if (conference == null)
            {
                return HttpNotFound();
            }
            ViewBag.chairId = new SelectList(db.Chairs, "chairId", "chairId", conference.chairId);
            return View(conference);
        }

        // POST: Conferences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Edit([Bind(Include = "conferenceId,beginDate,endDate,kindOfPaperTask,numOfReviewers,topic,chairId")] Conference conference)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conference).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.chairId = new SelectList(db.Chairs, "chairId", "chairId", conference.chairId);
            return View(conference);
        }

        // GET: Conferences/Delete/5
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conference conference = db.Conferences.Find(id);
            if (conference == null)
            {
                return HttpNotFound();
            }
            return View(conference);
        }

        // POST: Conferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Conference conference = db.Conferences.Find(id);
            db.Conferences.Remove(conference);
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

        public ActionResult IndexUserNames()
        {
            string currentUSerID = User.Identity.GetUserId();
            return View(db.Conferences.Where(m => m.chairId == currentUSerID).ToList());
        }
    }
}
