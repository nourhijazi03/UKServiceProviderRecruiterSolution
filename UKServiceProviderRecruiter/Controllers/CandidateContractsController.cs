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
    public class CandidateContractsController : Controller
    {
        private SPRdbEntities db = new SPRdbEntities();

        // GET: CandidateContracts
        public ActionResult Index()
        {
            var candidateContracts = db.CandidateContracts.Include(c => c.CandidateContractInvoice);
            return View(candidateContracts.ToList());
        }

        // GET: CandidateContracts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateContract candidateContract = db.CandidateContracts.Find(id);
            if (candidateContract == null)
            {
                return HttpNotFound();
            }
            return View(candidateContract);
        }

        // GET: CandidateContracts/Create
        public ActionResult Create()
        {
            ViewBag.CCID = new SelectList(db.CandidateContractInvoices, "CCID", "CCID");
            return View();
        }

        // POST: CandidateContracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CandidateContractID,CCID,TotalAmount,StatementDate")] CandidateContract candidateContract)
        {
            if (ModelState.IsValid)
            {
                db.CandidateContracts.Add(candidateContract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CCID = new SelectList(db.CandidateContractInvoices, "CCID", "CCID", candidateContract.CCID);
            return View(candidateContract);
        }

        // GET: CandidateContracts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateContract candidateContract = db.CandidateContracts.Find(id);
            if (candidateContract == null)
            {
                return HttpNotFound();
            }
            ViewBag.CCID = new SelectList(db.CandidateContractInvoices, "CCID", "CCID", candidateContract.CCID);
            return View(candidateContract);
        }

        // POST: CandidateContracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CandidateContractID,CCID,TotalAmount,StatementDate")] CandidateContract candidateContract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidateContract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CCID = new SelectList(db.CandidateContractInvoices, "CCID", "CCID", candidateContract.CCID);
            return View(candidateContract);
        }

        // GET: CandidateContracts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateContract candidateContract = db.CandidateContracts.Find(id);
            if (candidateContract == null)
            {
                return HttpNotFound();
            }
            return View(candidateContract);
        }

        // POST: CandidateContracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CandidateContract candidateContract = db.CandidateContracts.Find(id);
            db.CandidateContracts.Remove(candidateContract);
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
