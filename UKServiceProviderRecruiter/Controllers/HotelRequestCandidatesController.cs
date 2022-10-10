using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UKServiceProviderRecruiter;

namespace UKServiceProviderRecruiter.Controllers
{
    public class HotelRequestCandidatesController : Controller
    {
        private SPRdbEntities db = new SPRdbEntities();

        // GET: HotelRequestCandidates
        public ActionResult Index()
        {
            var hotelRequestCandidates = db.HotelRequestCandidates.Include(h => h.HotelRequest).Include(h => h.Candidate);
            return View(hotelRequestCandidates.ToList());
        }

        // GET: HotelRequestCandidates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelRequestCandidate hotelRequestCandidate = db.HotelRequestCandidates.Find(id);
            if (hotelRequestCandidate == null)
            {
                return HttpNotFound();
            }
            return View(hotelRequestCandidate);
        }

        // GET: HotelRequestCandidates/Create
        public ActionResult Create()
        {
            ViewBag.hotelRequestID = new SelectList(db.HotelRequests, "HotelRequestID", "HoursNeeded");
            ViewBag.CandidateID = new SelectList(db.Candidates, "CandidateID", "Name");
            return View();
        }

        // POST: HotelRequestCandidates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HotelRequestCandidateID,hotelRequestID,CandidateID")] HotelRequestCandidate hotelRequestCandidate)
        {
            if (ModelState.IsValid)
            {
                db.HotelRequestCandidates.Add(hotelRequestCandidate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.hotelRequestID = new SelectList(db.HotelRequests, "HotelRequestID", "HoursNeeded", hotelRequestCandidate.hotelRequestID);
            ViewBag.CandidateID = new SelectList(db.Candidates, "CandidateID", "Name", hotelRequestCandidate.CandidateID);
            return View(hotelRequestCandidate);
        }

        // GET: HotelRequestCandidates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelRequestCandidate hotelRequestCandidate = db.HotelRequestCandidates.Find(id);
            if (hotelRequestCandidate == null)
            {
                return HttpNotFound();
            }
            ViewBag.hotelRequestID = new SelectList(db.HotelRequests, "HotelRequestID", "HoursNeeded", hotelRequestCandidate.hotelRequestID);
            ViewBag.CandidateID = new SelectList(db.Candidates, "CandidateID", "Name", hotelRequestCandidate.CandidateID);
            return View(hotelRequestCandidate);
        }

        // POST: HotelRequestCandidates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HotelRequestCandidateID,hotelRequestID,CandidateID")] HotelRequestCandidate hotelRequestCandidate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotelRequestCandidate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.hotelRequestID = new SelectList(db.HotelRequests, "HotelRequestID", "HoursNeeded", hotelRequestCandidate.hotelRequestID);
            ViewBag.CandidateID = new SelectList(db.Candidates, "CandidateID", "Name", hotelRequestCandidate.CandidateID);
            return View(hotelRequestCandidate);
        }

        // GET: HotelRequestCandidates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelRequestCandidate hotelRequestCandidate = db.HotelRequestCandidates.Find(id);
            if (hotelRequestCandidate == null)
            {
                return HttpNotFound();
            }
            return View(hotelRequestCandidate);
        }

        // POST: HotelRequestCandidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HotelRequestCandidate hotelRequestCandidate = db.HotelRequestCandidates.Find(id);
            db.HotelRequestCandidates.Remove(hotelRequestCandidate);
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
