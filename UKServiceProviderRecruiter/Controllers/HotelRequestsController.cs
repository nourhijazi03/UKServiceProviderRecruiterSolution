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
    public class HotelRequestsController : Controller
    {
        private SPRdbEntities db = new SPRdbEntities();

        // GET: HotelRequest
        public ActionResult Index()
        {
            var hotelRequests = db.HotelRequests.Include(h => h.Hotel);
            return View(hotelRequests.ToList());
        }

        // GET: HotelRequest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelRequest hotelRequest = db.HotelRequests.Find(id);
            if (hotelRequest == null)
            {
                return HttpNotFound();
            }
            return View(hotelRequest);
        }

        // GET: HotelRequest/Create
        public ActionResult Create()
        {
            ViewBag.HotelID = new SelectList(db.Hotels, "HotelID", "Name");
            return View();
        }

        // POST: HotelRequest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HotelRequestID,HotelID,NumberOfCandidates,StartDate,EndDate,HoursNeeded,RequestDate,Speciality")] HotelRequest hotelRequest)
        {
            if (ModelState.IsValid)
            {
                db.HotelRequests.Add(hotelRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HotelID = new SelectList(db.Hotels, "HotelID", "Name", hotelRequest.HotelID);
            return View(hotelRequest);
        }

        // GET: HotelRequest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelRequest hotelRequest = db.HotelRequests.Find(id);
            if (hotelRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelID = new SelectList(db.Hotels, "HotelID", "Name", hotelRequest.HotelID);
            return View(hotelRequest);
        }

        // POST: HotelRequest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HotelRequestID,HotelID,NumberOfCandidates,StartDate,EndDate,HoursNeeded,RequestDate,Speciality")] HotelRequest hotelRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotelRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HotelID = new SelectList(db.Hotels, "HotelID", "Name", hotelRequest.HotelID);
            return View(hotelRequest);
        }

        // GET: HotelRequest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelRequest hotelRequest = db.HotelRequests.Find(id);
            if (hotelRequest == null)
            {
                return HttpNotFound();
            }
            return View(hotelRequest);
        }

        // POST: HotelRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HotelRequest hotelRequest = db.HotelRequests.Find(id);
            db.HotelRequests.Remove(hotelRequest);
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
