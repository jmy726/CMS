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
    public class ReviewsController : Controller
    {
        private CMS db = new CMS();

        // GET: Reviews
        public ActionResult Index()
        {
            var reviews = db.Reviews.Include(r => r.Paper).Include(r => r.Reviewer);
            return View(reviews.ToList());
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            ViewBag.paperId = new SelectList(db.Papers, "paperId", "title");
            ViewBag.reviewerId = new SelectList(db.Reviewers, "reviewerId", "reviewerId");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "reviewId,reviewDate,rating,reviewDesc,reviewerId,paperId")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.paperId = new SelectList(db.Papers, "paperId", "title", review.paperId);
            ViewBag.reviewerId = new SelectList(db.Reviewers, "reviewerId", "reviewerId", review.reviewerId);
            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.paperId = new SelectList(db.Papers, "paperId", "title", review.paperId);
            ViewBag.reviewerId = new SelectList(db.Reviewers, "reviewerId", "reviewerId", review.reviewerId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "reviewId,reviewDate,rating,reviewDesc,reviewerId,paperId")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.paperId = new SelectList(db.Papers, "paperId", "title", review.paperId);
            ViewBag.reviewerId = new SelectList(db.Reviewers, "reviewerId", "reviewerId", review.reviewerId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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

        public ActionResult AssignReviewer(int? paperId)
        {
            if(paperId==null)
            {
                return HttpNotFound();
            }
            var reviewers = db.Reviewers.ToList();//list all reviewers
            var paper = db.Papers.Find(paperId);//list paper
            Checkboxlist checklist = new Checkboxlist
            {
                paper = paper,//first is checkboxlist's paper, second is "var paper" get paper
                checklist = new List<Checkbox>(),
            };

            foreach(var reviewer in reviewers)//show all reviewers find in "var reviewers"
            {
                var checkbox = new Checkbox//build checkbox
                {
                    IsSelected = false,//intial state isn't be selected
                    reviewerId = reviewer.reviewerId,
                };
                User user1 = db.Users.Find(reviewer.reviewerId);//ba shang mian de reviewer de id qu user li mian zhao
                if (user1!=null)
                {
                    checkbox.name = user1.name;                    
                }
                LogEntities db2 = new LogEntities();
                AspNetUser user2 = db2.AspNetUsers.Find(reviewer.reviewerId);
                if(user2!=null)
                {
                    checkbox.Email = user2.Email;
                }
                var keywordbrgs = db.Reviewers.Where(m => m.reviewerId == reviewer.reviewerId).ToList();
                foreach(var keywordbrg in keywordbrgs)
                {
                    var keywordEntity = db.Keywords.Find(keywordbrg.keywords);
                    checkbox.keyword += keywordEntity.keyword1 + ", ";
                }
                checklist.checklist.Add(checkbox);//1 is "Checkboxlist checklist = new Checkboxlist"
            }//2 is checklist class's attribute

            return View(checklist);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignReviewer(Checkboxlist model)
        {           
            var selected = model.GetSelectedId();//huo de bei xuan de reviewer
            var paper = db.Papers.Find(model.paper.paperId);
            if(selected.Count() == 4 || selected.Count() == 3)
            {
                foreach (string reviewerId in selected)
                {
                    Review review = new Review
                    {
                        paperId = paper.paperId,
                        reviewerId = reviewerId,//1 is reviewer class's reviewer id, 2 is for loop's                        
                    };
                    db.Reviews.Add(review);                  
                }
                paper.reviewstatus = "Assigned";
                db.Entry(paper).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

    }
}
