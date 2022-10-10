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
    public class CandidateContractInvoicesController : Controller
    {
        private SPRdbEntities db = new SPRdbEntities();

        // GET: CandidateContractInvoices
        public ActionResult Index()
        {
            var candidateContractInvoices = db.CandidateContractInvoices.Include(c => c.CandidateInvoice);
            return View(candidateContractInvoices.ToList());
        }

        // GET: CandidateContractInvoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateContractInvoice candidateContractInvoice = db.CandidateContractInvoices.Find(id);
            if (candidateContractInvoice == null)
            {
                return HttpNotFound();
            }
            return View(candidateContractInvoice);
        }

        // GET: CandidateContractInvoices/Create
        public ActionResult Create()
        {
            ViewBag.InvoiceID = new SelectList(db.CandidateInvoices, "InvoiceID", "PaymentMethod");
            return View();
        }

        // POST: CandidateContractInvoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CCID,InvoiceID,CandidateContractID")] CandidateContractInvoice candidateContractInvoice)
        {
            if (ModelState.IsValid)
            {
                db.CandidateContractInvoices.Add(candidateContractInvoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvoiceID = new SelectList(db.CandidateInvoices, "InvoiceID", "PaymentMethod", candidateContractInvoice.InvoiceID);
            return View(candidateContractInvoice);
        }

        // GET: CandidateContractInvoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateContractInvoice candidateContractInvoice = db.CandidateContractInvoices.Find(id);
            if (candidateContractInvoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceID = new SelectList(db.CandidateInvoices, "InvoiceID", "PaymentMethod", candidateContractInvoice.InvoiceID);
            return View(candidateContractInvoice);
        }

        // POST: CandidateContractInvoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CCID,InvoiceID,CandidateContractID")] CandidateContractInvoice candidateContractInvoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidateContractInvoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceID = new SelectList(db.CandidateInvoices, "InvoiceID", "PaymentMethod", candidateContractInvoice.InvoiceID);
            return View(candidateContractInvoice);
        }

        // GET: CandidateContractInvoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateContractInvoice candidateContractInvoice = db.CandidateContractInvoices.Find(id);
            if (candidateContractInvoice == null)
            {
                return HttpNotFound();
            }
            return View(candidateContractInvoice);
        }

        // POST: CandidateContractInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CandidateContractInvoice candidateContractInvoice = db.CandidateContractInvoices.Find(id);
            db.CandidateContractInvoices.Remove(candidateContractInvoice);
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
