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
        public ActionResult Index()
        {
            var asiakastapahtumat = db.Asiakastapahtumat.Include(a => a.Asiakkaanperustiedot);
            return View(asiakastapahtumat.ToList());
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
            ViewBag.Asiakasnumero = new SelectList(db.Asiakkaanperustiedot, "Asiakasnumero", "Asiakasnumero");
            return View();
        }

        // POST: Asiakastapahtumat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Tapahtumaid,Asiakasnumero,Päivämäärä,Yhteydenotto,Kuvaus,Ratkaisu")] Asiakastapahtumat asiakastapahtumat)
        {
            if (ModelState.IsValid)
            {
                db.Asiakastapahtumat.Add(asiakastapahtumat);
                db.SaveChanges();
                return RedirectToAction("Index", "Etusivu");
            }

            ViewBag.Asiakasnumero = new SelectList(db.Asiakkaanperustiedot, "Asiakasnumero", "Asiakasnumero", asiakastapahtumat.Asiakasnumero);
            return View(asiakastapahtumat);
        }

        // GET: Asiakastapahtumat/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.Asiakasnumero = new SelectList(db.Asiakkaanperustiedot, "Asiakasnumero", "Asiakasnumero", asiakastapahtumat.Asiakasnumero);
            return View(asiakastapahtumat);
        }

        // POST: Asiakastapahtumat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewBag.Asiakasnumero = new SelectList(db.Asiakkaanperustiedot, "Asiakasnumero", "Asiakasnumero", asiakastapahtumat.Asiakasnumero);
            return View(asiakastapahtumat);
        }

        // GET: Asiakastapahtumat/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Asiakastapahtumat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asiakastapahtumat asiakastapahtumat = db.Asiakastapahtumat.Find(id);
            db.Asiakastapahtumat.Remove(asiakastapahtumat);
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
