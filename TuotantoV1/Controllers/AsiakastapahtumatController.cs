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
    public class AsiakastapahtumatController : Controller
    {
        private tuotantoEntities db = new tuotantoEntities();

        // GET: Asiakastapahtumat
        public ActionResult Index(string sortOrder, string searchString)
        {
            if (Session["Käyttäjätunnus"] == null)
            {
                return RedirectToAction("Index", "Logins"); //Palauttaa kirjautumisnäkymän, jos kirjatuminen ei onnistunut
            }
            else
            {
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : ""; //Hakukenttä
            var seniorit = from s in db.Asiakastapahtumat
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                seniorit = seniorit.Where(s => s.Asiakkaanperustiedot.Sukunimi.Contains(searchString) //Määritellään mistä ja mitä tietoa haetaan
                                       || s.Asiakkaanperustiedot.Etunimi.Contains(searchString)
                                       || s.Asiakkaanperustiedot.Asiakasnumero.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    seniorit = seniorit.OrderByDescending(s => s.Asiakkaanperustiedot.Sukunimi); //Mitä tietoja näytetään
                        break;
                default:
                    seniorit = seniorit.OrderBy(s => s.Asiakkaanperustiedot.Etunimi);
                    seniorit = seniorit.OrderBy(s => s.Asiakkaanperustiedot.Asiakasnumero);
                    break;
            }
            return View(seniorit.ToList());
            }
        }

        // GET: Asiakastapahtumat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiakastapahtumat asiakastapahtumat = db.Asiakastapahtumat.Find(id);
            if (asiakastapahtumat == null)
            {
                return HttpNotFound();
            }
            return View(asiakastapahtumat);
        }

        // GET: Asiakastapahtumat/Create
        public ActionResult Create()
        
        {
            var multihaku = db.Asiakkaanperustiedot.Include(k => k.Asiakastapahtumat); //Lisätään dropdowniin asiakasnumeron lisäksi nimi
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

        // POST: Asiakastapahtumat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Tapahtumaid,Asiakasnumero,Asiakas,Päivämäärä,Yhteydenotto,Kuvaus,Ratkaisu")] Asiakastapahtumat asiakastapahtumat)
        {
            if (ModelState.IsValid)
            {
                db.Asiakastapahtumat.Add(asiakastapahtumat); //Lisää asiakastapahtuman
                db.SaveChanges();
                return RedirectToAction("Index"); //Palaa Indexiin
            }

            ViewBag.Asiakasnumero = new SelectList(db.Asiakkaanperustiedot, "Asiakasnumero", "Asiakas", asiakastapahtumat.Asiakasnumero);
            return View(asiakastapahtumat);
        }

        // GET: Asiakastapahtumat/Edit/5
        public ActionResult Edit(int? id)
        {
            {
                var multihaku = db.Asiakastapahtumat.Include(k => k.Asiakkaanperustiedot); //Lisätään dropdowniin asiakasnumeron lisäksi nimi
                List<SelectListItem> Asiakas = new List<SelectListItem>();
                foreach (var Asiakkas in multihaku.ToList())
                {
                    Asiakas.Add(new SelectListItem
                    {
                        Value = Asiakkas.Asiakasnumero.ToString(),
                        Text = Asiakkas.Asiakasnumero.ToString() + " - " + Asiakkas.Asiakkaanperustiedot.Asiakas
                    });
                }
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Asiakastapahtumat asiakastapahtumat = db.Asiakastapahtumat.Find(id); 
                if (asiakastapahtumat == null)
                {
                    return HttpNotFound();
                }

                ViewBag.Asiakasnumero = new SelectList(Asiakas, "Value", "Text", asiakastapahtumat.Asiakasnumero);
                return View(asiakastapahtumat);
            }
        }

        // POST: Asiakastapahtumat/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Tapahtumaid,Asiakasnumero,Päivämäärä,Yhteydenotto,Kuvaus,Ratkaisu")] Asiakastapahtumat asiakastapahtumat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asiakastapahtumat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Asiakasnumero = new SelectList(db.Asiakkaanperustiedot, "Asiakasnumero", "Asiakas", asiakastapahtumat.Asiakasnumero);
            return View(asiakastapahtumat);
        }

        // GET: Asiakastapahtumat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiakastapahtumat asiakastapahtumat = db.Asiakastapahtumat.Find(id); //Hakee id:llä oikean asiakkaan
            if (asiakastapahtumat == null)
            {
                return HttpNotFound();
            }
            return View(asiakastapahtumat);
        }

        // POST: Asiakastapahtumat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asiakastapahtumat asiakastapahtumat = db.Asiakastapahtumat.Find(id); //Hakee id:llä oikean asiakkaan
            db.Asiakastapahtumat.Remove(asiakastapahtumat); //Poistaa asiakastapahtuman
            db.SaveChanges(); //Tallentaa muutokset
            return RedirectToAction("Index"); //Palaa Indexiin
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
