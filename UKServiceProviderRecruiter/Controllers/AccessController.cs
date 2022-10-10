using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using UKServiceProviderRecruiter.Helpers;

namespace UKServiceProviderRecruiter.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        SPRdbEntities db = null;
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Login()

        {

            return View();
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult LogOut()
        {

            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login", "Access");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User objUser)
        {
            try
            {
                /// ADD IS DELETED 
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(objUser.Name) || !string.IsNullOrEmpty(objUser.Password))
                    {

                        using (db = new SPRdbEntities())
                        {
                            User obj = null;
                            obj = new User();
                            var pass = Cryptography.Encryptor.MD5Hash(objUser.Password);
                            Cryptography.Encryptor.MD5Hash(objUser.Password);
                            obj = db.Users.Where(l => l.Name == objUser.Name && l.Password == pass.ToString()).FirstOrDefault();

                            if (obj != null)
                            {
                                Session["UserID"] = obj.UserID.ToString();
                                Session["Name"] = obj.Name.ToString();
                                Session["Email"] = obj.Email.ToString();

                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {

                                TempData["Message"] = "Login User name or password supplied doesn't exist.";

                            }
                        }
                        ViewBag.currentyear = DateTime.Now.Year;
                        return View(objUser);
                    }
                    else
                    {
                        TempData["Message"] = "Login failed.User name or password supplied doesn't exist.";
                    }
                }




            }
            catch (Exception ex)
            {
                TempData["Message"] = "Login failed.Error - " + ex.InnerException.ToString();
            }
            return RedirectToAction("lOGIN");

        }
    }
}
