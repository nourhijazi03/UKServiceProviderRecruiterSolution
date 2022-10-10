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
    public class CandidateInvoicesController : Controller
    {
        private SPRdbEntities db = new SPRdbEntities();

        // GET: CandidateInvoices
        public ActionResult Index()
        {
            var candidateInvoices = db.CandidateInvoices.Include(c => c.Candidate).Include(c => c.HotelRequest).Include(c => c.Hotel);
            return View(candidateInvoices.ToList());
        }

        // GET: CandidateInvoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateInvoice candidateInvoice = db.CandidateInvoices.Find(id);
            if (candidateInvoice == null)
            {
                return HttpNotFound();
            }
            return View(candidateInvoice);
        }

        // GET: CandidateInvoices/Create
        public ActionResult Create()
        {
            ViewBag.CandidateID = new SelectList(db.Candidates, "CandidateID", "Name");
            ViewBag.HotelRequestID = new SelectList(db.HotelRequests, "HotelRequestID", "HoursNeeded");
            ViewBag.HotelID = new SelectList(db.Hotels, "HotelID", "Name");
            return View();
        }

        // POST: CandidateInvoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceID,InvoiceNum,CandidateID,HotelID,CCID,InvoiceDate,TotalAmount,PaymentMethod,HotelRequestID,Summary")] CandidateInvoice candidateInvoice)
        {
            if (ModelState.IsValid)
            {
                db.CandidateInvoices.Add(candidateInvoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CandidateID = new SelectList(db.Candidates, "CandidateID", "Name", candidateInvoice.CandidateID);
            ViewBag.HotelRequestID = new SelectList(db.HotelRequests, "HotelRequestID", "HoursNeeded", candidateInvoice.HotelRequestID);
            ViewBag.HotelID = new SelectList(db.Hotels, "HotelID", "Name", candidateInvoice.HotelID);
            return View(candidateInvoice);
        }

        // GET: CandidateInvoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateInvoice candidateInvoice = db.CandidateInvoices.Find(id);
            if (candidateInvoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.CandidateID = new SelectList(db.Candidates, "CandidateID", "Name", candidateInvoice.CandidateID);
            ViewBag.HotelRequestID = new SelectList(db.HotelRequests, "HotelRequestID", "HoursNeeded", candidateInvoice.HotelRequestID);
            ViewBag.HotelID = new SelectList(db.Hotels, "HotelID", "Name", candidateInvoice.HotelID);
            return View(candidateInvoice);
        }

        // POST: CandidateInvoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceID,InvoiceNum,CandidateID,HotelID,CCID,InvoiceDate,TotalAmount,PaymentMethod,HotelRequestID,Summary")] CandidateInvoice candidateInvoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidateInvoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CandidateID = new SelectList(db.Candidates, "CandidateID", "Name", candidateInvoice.CandidateID);
            ViewBag.HotelRequestID = new SelectList(db.HotelRequests, "HotelRequestID", "HoursNeeded", candidateInvoice.HotelRequestID);
            ViewBag.HotelID = new SelectList(db.Hotels, "HotelID", "Name", candidateInvoice.HotelID);
            return View(candidateInvoice);
        }

        // GET: CandidateInvoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateInvoice candidateInvoice = db.CandidateInvoices.Find(id);
            if (candidateInvoice == null)
            {
                return HttpNotFound();
            }
            return View(candidateInvoice);
        }

        // POST: CandidateInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CandidateInvoice candidateInvoice = db.CandidateInvoices.Find(id);
            db.CandidateInvoices.Remove(candidateInvoice);
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
