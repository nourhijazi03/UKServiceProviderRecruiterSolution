using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.Mvc;
using UKServiceProviderRecruiter.Models;

namespace UKServiceProviderRecruiter.Controllers
{
    public class HomeController : Controller
    {

        SPRdbEntities db = null;

        public ActionResult Index()
        {

            DashboardModel dm = new DashboardModel();
            using (db = new SPRdbEntities()) {
                dm.CandidatesCount = db.Candidates.Count();
                dm.HotelsCount = db.Hotels.Count();
                dm.Income = 10.33 ;
                dm.Payments = 8.33;
               
                dm.PendingHotelRequests = db.HotelRequests.Where(x => x.Status.Name == "pending").ToList();
                dm.ActiveHotelRequests = db.HotelRequests.Where(x => x.Status.Name == "active").ToList();
                dm.PendingHotelRequestsString = "There are "+ dm.PendingHotelRequests.Count+" pending hotel requests ";
                dm.ActiveHotelRequestsString = "There are " + dm.ActiveHotelRequests.Count + " active hotel requests ";
            }
    

                return View(dm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}