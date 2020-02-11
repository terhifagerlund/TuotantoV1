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
    public class AsiakasluokitteluController : Controller
    {
        private tuotantoEntities db = new tuotantoEntities();

        // GET: Asiakasluokittelu
        public ActionResult Index()
        {
            var asiakasluokittelu = db.Asiakasluokittelu.Include(a => a.Asiakkaanperustiedot);
            return View(asiakasluokittelu.ToList());
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
            ViewBag.Asiakasnumero = new SelectList(db.Asiakkaanperustiedot, "Asiakasnumero", "Asiakasnumero");
            return View();
        }

        // POST: Asiakasluokittelu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Luokitteluid,Asiakasnumero,Eläkeläisalennus,Tv,Pöytäkone,Kannettava,Matkapuhelin,Tabletti,Mokkula,Wlan")] Asiakasluokittelu asiakasluokittelu)
        {
            if (ModelState.IsValid)
            {
                db.Asiakasluokittelu.Add(asiakasluokittelu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Asiakasnumero = new SelectList(db.Asiakkaanperustiedot, "Asiakasnumero", "Asiakasnumero", asiakasluokittelu.Asiakasnumero);
            return View(asiakasluokittelu);
        }

        // GET: Asiakasluokittelu/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.Asiakasnumero = new SelectList(db.Asiakkaanperustiedot, "Asiakasnumero", "Asiakasnumero", asiakasluokittelu.Asiakasnumero);
            return View(asiakasluokittelu);
        }

        // POST: Asiakasluokittelu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewBag.Asiakasnumero = new SelectList(db.Asiakkaanperustiedot, "Asiakasnumero", "Asiakasnumero", asiakasluokittelu.Asiakasnumero);
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
