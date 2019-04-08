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
    public class ChairsController : Controller
    {
        private CMS db = new CMS();

        // GET: Chairs
        public ActionResult Index()
        {
            var chairs = db.Chairs.Include(c => c.User);
            return View(chairs.ToList());
        }

        // GET: Chairs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chair chair = db.Chairs.Find(id);
            if (chair == null)
            {
                return HttpNotFound();
            }
            return View(chair);
        }

        // GET: Chairs/Create
        public ActionResult Create()
        {
            ViewBag.chairId = new SelectList(db.Users, "userId", "name");
            return View();
        }

        // POST: Chairs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "chairId")] Chair chair)
        {
            if (ModelState.IsValid)
            {
                db.Chairs.Add(chair);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.chairId = new SelectList(db.Users, "userId", "name", chair.chairId);
            return View(chair);
        }

        // GET: Chairs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chair chair = db.Chairs.Find(id);
            if (chair == null)
            {
                return HttpNotFound();
            }
            ViewBag.chairId = new SelectList(db.Users, "userId", "name", chair.chairId);
            return View(chair);
        }

        // POST: Chairs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "chairId")] Chair chair)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chair).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.chairId = new SelectList(db.Users, "userId", "name", chair.chairId);
            return View(chair);
        }

        // GET: Chairs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chair chair = db.Chairs.Find(id);
            if (chair == null)
            {
                return HttpNotFound();
            }
            return View(chair);
        }

        // POST: Chairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Chair chair = db.Chairs.Find(id);
            db.Chairs.Remove(chair);
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
