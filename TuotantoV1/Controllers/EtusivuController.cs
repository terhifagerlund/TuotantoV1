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
    public class EtusivuController : Controller
    {
        private tuotantoEntities db = new tuotantoEntities();

        // GET: Etusivu
        public ActionResult Index(string sortOrder, string searchString)
        {
            if (Session["Käyttäjätunnus"] == null)
            {
                return RedirectToAction("Index", "Logins");
            }
            else
            {

           
           
            ViewBag.SukunimiNameSortParm = String.IsNullOrEmpty(sortOrder) ? "SukunimiName_desc" : "";
            ViewBag.AsiakasnumeroNameSortParm = sortOrder == "AsiakasnumeroName" ? "AsiakasnumeroName_desc" : "AsiakasnumeroName";
            ViewBag.PostitoimipaikkaNameSortParm = sortOrder == "PostitoimipaikkaName" ? "PostitoimipaikkaName_desc" : "PostitoimipaikkaName";
            ViewBag.PostinumeroNameSortParm = sortOrder == "PostinumeroName" ? "PostinumeroName_desc" : "PostinumeroName";
            var seniorit = from s in db.Asiakkaanperustiedot
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                seniorit = seniorit.Where(s => s.Sukunimi.Contains(searchString)
                                       || s.Etunimi.Contains(searchString)
                                       || s.Asiakasnumero.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "SukunimiName_desc":
                    seniorit = seniorit.OrderByDescending(s => s.Sukunimi);
                    break;
                case "PostitoimipaikkaName":
                    seniorit = seniorit.OrderBy(s => s.Postitoimipaikka);
                    break;
                case "PostitoimipaikkaName_desc":
                    seniorit = seniorit.OrderByDescending(s => s.Postitoimipaikka);
                    break;
                case "AsiakasnumeroName":
                    seniorit = seniorit.OrderBy(s => s.Asiakasnumero);
                    break;
                case "AsiakasnumeroName_desc":
                    seniorit = seniorit.OrderByDescending(s => s.Asiakasnumero);
                    break;
                case "PostinumeroName":
                    seniorit = seniorit.OrderBy(s => s.Postinumero);
                    break;
                case "PostinumeroName_desc":
                    seniorit = seniorit.OrderByDescending(s => s.Postinumero);
                    break;
                default:
                    seniorit = seniorit.OrderBy(s => s.Sukunimi);
                    break;
            }
            return View(seniorit.ToList());
            }
        }
        // GET: Etusivu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiakkaanperustiedot asiakkaanperustiedot = db.Asiakkaanperustiedot.Find(id);
            if (asiakkaanperustiedot == null)
            {
                return HttpNotFound();
            }
            return View(asiakkaanperustiedot);
        }

        // GET: Etusivu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Etusivu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Asiakasnumero,Etunimi,Sukunimi,Osoite,Postinumero,Postitoimipaikka,Puhelin,Sähköposti,Laskutusnimi,Laskutusosoite,Laskutuspostinumero,Laskutuspostitoimipaikka")] Asiakkaanperustiedot asiakkaanperustiedot)
        {
            if (ModelState.IsValid)
            {
                db.Asiakkaanperustiedot.Add(asiakkaanperustiedot);
                db.SaveChanges();
                return RedirectToAction("Create", "Asiakasluokittelu");
            }

            return View(asiakkaanperustiedot);
        }

        // GET: Etusivu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiakkaanperustiedot asiakkaanperustiedot = db.Asiakkaanperustiedot.Find(id);
            if (asiakkaanperustiedot == null)
            {
                return HttpNotFound();
            }
            return View(asiakkaanperustiedot);
        }

        // POST: Etusivu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Asiakasnumero,Etunimi,Sukunimi,Osoite,Postinumero,Postitoimipaikka,Puhelin,Sähköposti,Laskutusnimi,Laskutusosoite,Laskutuspostinumero,Laskutuspostitoimipaikka")] Asiakkaanperustiedot asiakkaanperustiedot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asiakkaanperustiedot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asiakkaanperustiedot);
        }

        // GET: Etusivu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiakkaanperustiedot asiakkaanperustiedot = db.Asiakkaanperustiedot.Find(id);
            if (asiakkaanperustiedot == null)
            {
                return HttpNotFound();
            }
            return View(asiakkaanperustiedot);
        }

        // POST: Etusivu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Asiakkaanperustiedot asiakkaanperustiedot = db.Asiakkaanperustiedot.Find(id);
                db.Asiakkaanperustiedot.Remove(asiakkaanperustiedot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                string message = string.Format("Tapahtui virhe! Poista asiakkaalta asiakasluokittelu ja asiakastapahtuma ennen asiakastietojen poistamista.", ex.Message);
                ModelState.AddModelError(string.Empty, message);
            }
            return View();
            
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
