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
    public class TBL_CONCEPTOS1Controller : Controller
    {
        private DESCUENTOS_ASOCIADOSEntities db = new DESCUENTOS_ASOCIADOSEntities();

        // GET: TBL_CONCEPTOS1
        public async Task<ActionResult> Index()
        {
            return View(await db.TBL_CONCEPTOS.ToListAsync());
        }

        // GET: TBL_CONCEPTOS1/Details/5
        public async Task<ActionResult> Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_CONCEPTOS tBL_CONCEPTOS = await db.TBL_CONCEPTOS.FindAsync(id);
            if (tBL_CONCEPTOS == null)
            {
                return HttpNotFound();
            }
            return View(tBL_CONCEPTOS);
        }

        // GET: TBL_CONCEPTOS1/Create
        public ActionResult Create()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem
            {
                Text = "Devengo",
                Value = "Devengo"
            });
            listItems.Add(new SelectListItem
            {
                Text = "Valor fijo",
                Value = "Valor fijo"
            });
            @ViewBag.Naturaleza = listItems;
            List<SelectListItem> Modo = new List<SelectListItem>();
            Modo.Add(new SelectListItem
            {
                Text = "Individual",
                Value = "Individual"
            });
            Modo.Add(new SelectListItem
            {
                Text = "General",
                Value = "General"
            });
            @ViewBag.Modo = Modo;
            List<SelectListItem> TipodeCuota = new List<SelectListItem>();
            TipodeCuota.Add(new SelectListItem
            {
                Text = "En porcentaje sobre grupo de cuotas",
                Value = "En porcentaje sobre grupo de cuotas"
            });
            TipodeCuota.Add(new SelectListItem
            {
                Text = "Valor fijo",
                Value = "Valor fijo"
            });
            @ViewBag.TipodeCuota = TipodeCuota;
            List<SelectListItem> AplicacionCuota = new List<SelectListItem>();
            AplicacionCuota.Add(new SelectListItem
            {
                Text = "Individual",
                Value = "Individual"
            });
            AplicacionCuota.Add(new SelectListItem
            {
                Text = "General",
                Value = "General"
            });
            @ViewBag.AplicacionCuota = AplicacionCuota;
            List<SelectListItem> ManejotopeMax = new List<SelectListItem>();
            ManejotopeMax.Add(new SelectListItem
            {
                Text = "Si",
                Value = "Si"
            });
            ManejotopeMax.Add(new SelectListItem
            {
                Text = "No",
                Value = "No"
            });
            @ViewBag.ManejotopeMax = ManejotopeMax;
            return View();
        }

        // POST: TBL_CONCEPTOS1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_CONCEPTO,CODIGO_CONCEPTO,DESCRIPCION,ABREVIATURA,NATURALEZA,MODO,TIPO_CUOTA,APLICACION_CUOTA,CONCEPTO_TOPE,PERIODICIDAD_CUOTA,PORCENTAJE_CUOTA,AGRUP_CONCEPTOS,PROP_TIEMPO,AGRUP_CALC_TIEMPO,VAL_EXCLUD,VALOR,FECHA_INICIO_DESC,FECHA_FIN_DESC,ESTADO_DESC,FORMA_PAGO,ESTADO_CONCEPTO")] TBL_CONCEPTOS tBL_CONCEPTOS)
        {
            if (ModelState.IsValid)
            {
                db.TBL_CONCEPTOS.Add(tBL_CONCEPTOS);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tBL_CONCEPTOS);
        }

        // GET: TBL_CONCEPTOS1/Edit/5
        public async Task<ActionResult> Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_CONCEPTOS tBL_CONCEPTOS = await db.TBL_CONCEPTOS.FindAsync(id);
            if (tBL_CONCEPTOS == null)
            {
                return HttpNotFound();
            }
            return View(tBL_CONCEPTOS);
        }

        // POST: TBL_CONCEPTOS1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_CONCEPTO,CODIGO_CONCEPTO,DESCRIPCION,ABREVIATURA,NATURALEZA,MODO,TIPO_CUOTA,APLICACION_CUOTA,CONCEPTO_TOPE,PERIODICIDAD_CUOTA,PORCENTAJE_CUOTA,AGRUP_CONCEPTOS,PROP_TIEMPO,AGRUP_CALC_TIEMPO,VAL_EXCLUD,VALOR,FECHA_INICIO_DESC,FECHA_FIN_DESC,ESTADO_DESC,FORMA_PAGO,ESTADO_CONCEPTO")] TBL_CONCEPTOS tBL_CONCEPTOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBL_CONCEPTOS).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tBL_CONCEPTOS);
        }

        // GET: TBL_CONCEPTOS1/Delete/5
        public async Task<ActionResult> Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_CONCEPTOS tBL_CONCEPTOS = await db.TBL_CONCEPTOS.FindAsync(id);
            if (tBL_CONCEPTOS == null)
            {
                return HttpNotFound();
            }
            return View(tBL_CONCEPTOS);
        }

        // POST: TBL_CONCEPTOS1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(decimal id)
        {
            TBL_CONCEPTOS tBL_CONCEPTOS = await db.TBL_CONCEPTOS.FindAsync(id);
            db.TBL_CONCEPTOS.Remove(tBL_CONCEPTOS);
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
