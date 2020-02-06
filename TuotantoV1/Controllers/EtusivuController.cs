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
        public ActionResult Index()
        {
            return View(db.Asiakkaanperustiedot.ToList());
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
                return RedirectToAction("Index");
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
            Asiakkaanperustiedot asiakkaanperustiedot = db.Asiakkaanperustiedot.Find(id);
            db.Asiakkaanperustiedot.Remove(asiakkaanperustiedot);
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
