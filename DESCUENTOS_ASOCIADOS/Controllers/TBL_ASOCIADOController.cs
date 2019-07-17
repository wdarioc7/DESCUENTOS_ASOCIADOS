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
    public class TBL_ASOCIADOController : Controller
    {
        private DESCUENTOS_ASOCIADOSEntities db = new DESCUENTOS_ASOCIADOSEntities();

        // GET: TBL_ASOCIADO
        public ActionResult Index()
        {
            var tBL_ASOCIADO = db.TBL_ASOCIADO.Include(t => t.TBL_CONCEPTOS);
            return View(tBL_ASOCIADO.ToList());
        }

        // GET: TBL_ASOCIADO/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_ASOCIADO tBL_ASOCIADO = db.TBL_ASOCIADO.Find(id);
            if (tBL_ASOCIADO == null)
            {
                return HttpNotFound();
            }
            return View(tBL_ASOCIADO);
        }

        // GET: TBL_ASOCIADO/Create
        public ActionResult Create()
        {
            ViewBag.ID_CONCEPTO = new SelectList(db.TBL_CONCEPTOS, "ID_CONCEPTO", "DESCRIPCION");
            return View();
        }

        // POST: TBL_ASOCIADO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_ASOCIADO,NOMBRE,TELOFONO,ID_CONCEPTO")] TBL_ASOCIADO tBL_ASOCIADO)
        {
            if (ModelState.IsValid)
            {
                db.TBL_ASOCIADO.Add(tBL_ASOCIADO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CONCEPTO = new SelectList(db.TBL_CONCEPTOS, "ID_CONCEPTO", "DESCRIPCION", tBL_ASOCIADO.ID_CONCEPTO);
            return View(tBL_ASOCIADO);
        }

        // GET: TBL_ASOCIADO/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_ASOCIADO tBL_ASOCIADO = db.TBL_ASOCIADO.Find(id);
            if (tBL_ASOCIADO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CONCEPTO = new SelectList(db.TBL_CONCEPTOS, "ID_CONCEPTO", "CODIGO_CONCEPTO", tBL_ASOCIADO.ID_CONCEPTO);
            return View(tBL_ASOCIADO);
        }

        // POST: TBL_ASOCIADO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ASOCIADO,NOMBRE,TELOFONO,ID_CONCEPTO")] TBL_ASOCIADO tBL_ASOCIADO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBL_ASOCIADO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CONCEPTO = new SelectList(db.TBL_CONCEPTOS, "ID_CONCEPTO", "CODIGO_CONCEPTO", tBL_ASOCIADO.ID_CONCEPTO);
            return View(tBL_ASOCIADO);
        }

        // GET: TBL_ASOCIADO/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_ASOCIADO tBL_ASOCIADO = db.TBL_ASOCIADO.Find(id);
            if (tBL_ASOCIADO == null)
            {
                return HttpNotFound();
            }
            return View(tBL_ASOCIADO);
        }

        // POST: TBL_ASOCIADO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TBL_ASOCIADO tBL_ASOCIADO = db.TBL_ASOCIADO.Find(id);
            db.TBL_ASOCIADO.Remove(tBL_ASOCIADO);
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
