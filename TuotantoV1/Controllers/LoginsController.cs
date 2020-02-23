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
        public ActionResult Authorize(Logins LoginModel)
        {
            tuotantoEntities db = new tuotantoEntities();
            //Tarkistaa tietokannasta käyttäjätunnuksen ja salasanan
            var LoggedUser = db.Logins.SingleOrDefault(x => x.Käyttäjätunnus == LoginModel.Käyttäjätunnus && x.Salasana == LoginModel.Salasana);
            if (LoggedUser != null)
            {
                Logins käyttäjä = new Logins();
                int Loginid = LoggedUser.Loginid;
                käyttäjä = db.Logins.Where(o => o.Loginid == Loginid).FirstOrDefault(); //Käyttäjätunnus = tietokannasta löytyvä käyttäjätunnus
                Session["Käyttäjätunnus"] = LoggedUser.Käyttäjätunnus; 
                Session["KirjautunutKayttajaNimi"] = käyttäjä.Käyttäjätunnus; //Käyttäjätunnus näkyy sivun alareunassa(lisättiin layout-sivulle)
                return RedirectToAction("Index", "Etusivu"); //Onnistuneen kirjautumisen jälkeen etusivulle
            }
            else
            {
                LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana."; //Tämä lisättiin index-sivulle
                return View("Index", LoginModel );
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Logins"); //Uloskirjautumisen jälkeen kirjautumissivulle
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
