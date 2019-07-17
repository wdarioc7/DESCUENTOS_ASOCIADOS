using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DESCUENTOS_ASOCIADOS.Models;

namespace DESCUENTOS_ASOCIADOS.Controllers
{
    public class TBL_EQUIVALENCIASController : Controller
    {
        private DESCUENTOS_ASOCIADOSEntities db = new DESCUENTOS_ASOCIADOSEntities();

        // GET: TBL_EQUIVALENCIAS
        public ActionResult Index()
        {
            return View(db.TBL_EQUIVALENCIAS.ToList());
        }

        // GET: TBL_EQUIVALENCIAS/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_EQUIVALENCIAS tBL_EQUIVALENCIAS = db.TBL_EQUIVALENCIAS.Find(id);
            if (tBL_EQUIVALENCIAS == null)
            {
                return HttpNotFound();
            }
            return View(tBL_EQUIVALENCIAS);
        }

        // GET: TBL_EQUIVALENCIAS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TBL_EQUIVALENCIAS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_EQUIVALENCIAS,CODIGO_CONTABLE,TOTAL")] TBL_EQUIVALENCIAS tBL_EQUIVALENCIAS)
        {
            if (ModelState.IsValid)
            {
                db.TBL_EQUIVALENCIAS.Add(tBL_EQUIVALENCIAS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tBL_EQUIVALENCIAS);
        }

        // GET: TBL_EQUIVALENCIAS/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_EQUIVALENCIAS tBL_EQUIVALENCIAS = db.TBL_EQUIVALENCIAS.Find(id);
            if (tBL_EQUIVALENCIAS == null)
            {
                return HttpNotFound();
            }
            return View(tBL_EQUIVALENCIAS);
        }

        // POST: TBL_EQUIVALENCIAS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_EQUIVALENCIAS,CODIGO_CONTABLE,TOTAL")] TBL_EQUIVALENCIAS tBL_EQUIVALENCIAS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBL_EQUIVALENCIAS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tBL_EQUIVALENCIAS);
        }

        // GET: TBL_EQUIVALENCIAS/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_EQUIVALENCIAS tBL_EQUIVALENCIAS = db.TBL_EQUIVALENCIAS.Find(id);
            if (tBL_EQUIVALENCIAS == null)
            {
                return HttpNotFound();
            }
            return View(tBL_EQUIVALENCIAS);
        }

        // POST: TBL_EQUIVALENCIAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TBL_EQUIVALENCIAS tBL_EQUIVALENCIAS = db.TBL_EQUIVALENCIAS.Find(id);
            db.TBL_EQUIVALENCIAS.Remove(tBL_EQUIVALENCIAS);
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
