using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DESCUENTOS_ASOCIADOS.Models;

namespace DESCUENTOS_ASOCIADOS.Controllers
{
    public class TBL_EQUIVALENCIAS1Controller : Controller
    {
        private DESCUENTOS_ASOCIADOSEntities db = new DESCUENTOS_ASOCIADOSEntities();

        // GET: TBL_EQUIVALENCIAS1
        public async Task<ActionResult> Index()
        {
            var tBL_EQUIVALENCIAS = db.TBL_EQUIVALENCIAS.Include(t => t.TBL_CONCEPTOS);
            return View(await tBL_EQUIVALENCIAS.ToListAsync());
        }

        // GET: TBL_EQUIVALENCIAS1/Details/5
        public async Task<ActionResult> Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_EQUIVALENCIAS tBL_EQUIVALENCIAS = await db.TBL_EQUIVALENCIAS.FindAsync(id);
            if (tBL_EQUIVALENCIAS == null)
            {
                return HttpNotFound();
            }
            return View(tBL_EQUIVALENCIAS);
        }

        // GET: TBL_EQUIVALENCIAS1/Create
        public ActionResult Create()
        {
            ViewBag.ID_CONCEPTO = new SelectList(db.TBL_CONCEPTOS, "ID_CONCEPTO", "CODIGO_CONCEPTO");
            return View();
        }

        // POST: TBL_EQUIVALENCIAS1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_EQUIVALENCIAS,CODIGO_CONTABLE,ID_CONCEPTO,TOTAL")] TBL_EQUIVALENCIAS tBL_EQUIVALENCIAS)
        {
            if (ModelState.IsValid)
            {
                db.TBL_EQUIVALENCIAS.Add(tBL_EQUIVALENCIAS);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CONCEPTO = new SelectList(db.TBL_CONCEPTOS, "ID_CONCEPTO", "CODIGO_CONCEPTO", tBL_EQUIVALENCIAS.ID_CONCEPTO);
            return View(tBL_EQUIVALENCIAS);
        }

        // GET: TBL_EQUIVALENCIAS1/Edit/5
        public async Task<ActionResult> Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_EQUIVALENCIAS tBL_EQUIVALENCIAS = await db.TBL_EQUIVALENCIAS.FindAsync(id);
            if (tBL_EQUIVALENCIAS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CONCEPTO = new SelectList(db.TBL_CONCEPTOS, "ID_CONCEPTO", "CODIGO_CONCEPTO", tBL_EQUIVALENCIAS.ID_CONCEPTO);
            return View(tBL_EQUIVALENCIAS);
        }

        // POST: TBL_EQUIVALENCIAS1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_EQUIVALENCIAS,CODIGO_CONTABLE,ID_CONCEPTO,TOTAL")] TBL_EQUIVALENCIAS tBL_EQUIVALENCIAS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBL_EQUIVALENCIAS).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CONCEPTO = new SelectList(db.TBL_CONCEPTOS, "ID_CONCEPTO", "CODIGO_CONCEPTO", tBL_EQUIVALENCIAS.ID_CONCEPTO);
            return View(tBL_EQUIVALENCIAS);
        }

        // GET: TBL_EQUIVALENCIAS1/Delete/5
        public async Task<ActionResult> Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_EQUIVALENCIAS tBL_EQUIVALENCIAS = await db.TBL_EQUIVALENCIAS.FindAsync(id);
            if (tBL_EQUIVALENCIAS == null)
            {
                return HttpNotFound();
            }
            return View(tBL_EQUIVALENCIAS);
        }

        // POST: TBL_EQUIVALENCIAS1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(decimal id)
        {
            TBL_EQUIVALENCIAS tBL_EQUIVALENCIAS = await db.TBL_EQUIVALENCIAS.FindAsync(id);
            db.TBL_EQUIVALENCIAS.Remove(tBL_EQUIVALENCIAS);
            await db.SaveChangesAsync();
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
