using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TuotantoV1.Models;

namespace TuotantoV1.Controllers
{
    public class LoginsController : Controller
    {
        private tuotantoEntities db = new tuotantoEntities();

        // GET: Logins
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(Logins LoginsModel)
        {
            tuotantoEntities db = new tuotantoEntities();

            var LoggedUser = db.Logins.SingleOrDefault(x => x.Käyttäjätunnus == LoginsModel.Käyttäjätunnus && x.Salasana == LoginsModel.Salasana);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Successfull login";
                ViewBag.LoginId = LoggedUser.Loginid;
                Session["UserName"] = LoggedUser.Käyttäjätunnus;
                Session["LoginId"] = LoggedUser.Loginid;
                return RedirectToAction("Index", "Etusivu");
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessfull";
                ViewBag.LoggedStatus = "Out";
                //LoginsModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";
                return View("Logins", LoginsModel);
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Out";
            return RedirectToAction("Index", "Logins"); //Uloskirjautumisen jälkeen pääsivulle
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
