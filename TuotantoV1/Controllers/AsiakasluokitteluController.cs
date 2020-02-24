using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TuotantoV1.Models;

namespace TuotantoV1.Controllers
{
    public class AsiakasluokitteluController : Controller
    {
        private tuotantoEntities db = new tuotantoEntities();

        // GET: Asiakasluokittelu
        public ActionResult Index(string sortOrder, string searchString)
        {
            if (Session["Käyttäjätunnus"] == null) //Tarkistetaan onko käyttäjätunnus syötetty
            {
                return RedirectToAction("Index", "Logins");
            }
            else
            {
                //Luodaan Asiakasluokittelun hakutoiminto, asiakasta voidaan hakea sukunimen, etunimen ja asiakasnumeron perusteella
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "haku" : ""; 
            var seniorit = from s in db.Asiakasluokittelu
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                    //Haetaan halutut tiedot tietokannasta
                seniorit = seniorit.Where(s => s.Asiakkaanperustiedot.Sukunimi.Contains(searchString)
                                       || s.Asiakkaanperustiedot.Etunimi.Contains(searchString)
                                       || s.Asiakkaanperustiedot.Asiakasnumero.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                    //Tiedot tuodaan näkymään
                case "haku":
                    seniorit = seniorit.OrderByDescending(s => s.Asiakkaanperustiedot.Sukunimi);
                    break;
                default:
                    seniorit = seniorit.OrderBy(s => s.Asiakkaanperustiedot.Etunimi);
                    seniorit = seniorit.OrderBy(s => s.Asiakkaanperustiedot.Asiakasnumero);
                    break;
            }

            return View(seniorit.ToList());
            }
        }

        // GET: Asiakasluokittelu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiakasluokittelu asiakasluokittelu = db.Asiakasluokittelu.Find(id);
            if (asiakasluokittelu == null)
            {
                return HttpNotFound();
            }
            return View(asiakasluokittelu);
        }

        // GET: Asiakasluokittelu/Create
        public ActionResult Create()
        {
            //Luodaan multihaku niminen lista dropdownia varten. Dropdown näyttää asiakasnumeron ja asiakkaan etu- ja sukunimen
            var multihaku = db.Asiakkaanperustiedot.Include(k => k.Asiakasluokittelu); 
            List<SelectListItem> Asiakas = new List<SelectListItem>();
            foreach (var Asiakkas in multihaku.ToList())
            {
                Asiakas.Add(new SelectListItem
                {
                    Value = Asiakkas.Asiakasnumero.ToString(),
                    Text = Asiakkas.Asiakasnumero.ToString() + " - " + Asiakkas.Asiakas
                });
            }
            ViewBag.Asiakasnumero = new SelectList(Asiakas, "Value", "Text");
            return View();
        }

        // POST: Asiakasluokittelu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Luokitteluid,Asiakasnumero,Eläkeläisalennus,Tv,Pöytäkone,Kannettava,Matkapuhelin,Tabletti,Mokkula,Wlan")] Asiakasluokittelu asiakasluokittelu)
        {
            if (ModelState.IsValid)
            {
                db.Asiakasluokittelu.Add(asiakasluokittelu);
                db.SaveChanges();
                return RedirectToAction("Create", "Asiakastapahtumat");
            }
            ViewBag.Asiakasnumero = new SelectList(db.Asiakkaanperustiedot, "Asiakasnumero", "Asiakas", asiakasluokittelu.Asiakasnumero);
            return View(asiakasluokittelu);
        }

        // GET: Asiakasluokittelu/Edit/5
        public ActionResult Edit(int? id)
        {
            //Luodaan multihaku niminen lista dropdownia varten. Dropdown näyttää asiakasnumeron ja asiakkaan etu- ja sukunimen
            var multihaku = db.Asiakkaanperustiedot.Include(k => k.Asiakasluokittelu);
            List<SelectListItem> Asiakas = new List<SelectListItem>();
            foreach (var Asiakkas in multihaku.ToList())
            {
                Asiakas.Add(new SelectListItem
                {
                    Value = Asiakkas.Asiakasnumero.ToString(),
                    Text = Asiakkas.Asiakasnumero.ToString() + " - " + Asiakkas.Asiakas
                });
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiakasluokittelu asiakasluokittelu = db.Asiakasluokittelu.Find(id);
            if (asiakasluokittelu == null)
            {
                return HttpNotFound();
            }
            ViewBag.Asiakasnumero = new SelectList(Asiakas, "Value", "Text", asiakasluokittelu.Asiakasnumero);
            return View(asiakasluokittelu);
        }

        // POST: Asiakasluokittelu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Luokitteluid,Asiakasnumero,Eläkeläisalennus,Tv,Pöytäkone,Kannettava,Matkapuhelin,Tabletti,Mokkula,Wlan")] Asiakasluokittelu asiakasluokittelu)
        {
            if (ModelState.IsValid)   
            {
                db.Entry(asiakasluokittelu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index"); 
            }
        
            ViewBag.Asiakasnumero = new SelectList(db.Asiakkaanperustiedot, "Asiakasnumero", "Asiakas", asiakasluokittelu.Asiakasnumero);
            return View(asiakasluokittelu);
        }

        // GET: Asiakasluokittelu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiakasluokittelu asiakasluokittelu = db.Asiakasluokittelu.Find(id);
            if (asiakasluokittelu == null)
            {
                return HttpNotFound();
            }
            return View(asiakasluokittelu);
        }

        // POST: Asiakasluokittelu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asiakasluokittelu asiakasluokittelu = db.Asiakasluokittelu.Find(id);
            db.Asiakasluokittelu.Remove(asiakasluokittelu);
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
