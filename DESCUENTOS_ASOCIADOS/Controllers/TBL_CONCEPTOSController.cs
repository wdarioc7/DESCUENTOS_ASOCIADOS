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
    public class TBL_CONCEPTOSController : Controller
    {
        private DESCUENTOS_ASOCIADOSEntities db = new DESCUENTOS_ASOCIADOSEntities();

        // GET: TBL_CONCEPTOS
        public ActionResult Index()
        {

           
            return View(db.TBL_CONCEPTOS.ToList());
        }

        // GET: TBL_CONCEPTOS/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_CONCEPTOS tBL_CONCEPTOS = db.TBL_CONCEPTOS.Find(id);
            if (tBL_CONCEPTOS == null)
            {
                return HttpNotFound();
            }
            return View(tBL_CONCEPTOS);
        }

        // GET: TBL_CONCEPTOS/Create
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

        // POST: TBL_CONCEPTOS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_CONCEPTO,CODIGO_CONCEPTO,DESCRIPCION,ABREVIATURA,NATURALEZA,MODO,TIPO_CUOTA,APLICACION_CUOTA,CONCEPTO_TOPE,PERIODICIDAD_CUOTA,PORCENTAJE_CUOTA,AGRUP_CONCEPTOS,PROP_TIEMPO,AGRUP_CALC_TIEMPO,VAL_EXCLUD,VALOR,FECHA_INICIO_DESC,FECHA_FIN_DESC,ESTADO_DESC,FORMA_PAGO,ESTADO_CONCEPTO")] TBL_CONCEPTOS tBL_CONCEPTOS)
        {
            if (ModelState.IsValid)
            {
                db.TBL_CONCEPTOS.Add(tBL_CONCEPTOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tBL_CONCEPTOS);
        }

        // GET: TBL_CONCEPTOS/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_CONCEPTOS tBL_CONCEPTOS = db.TBL_CONCEPTOS.Find(id);
            if (tBL_CONCEPTOS == null)
            {
                return HttpNotFound();
            }
            return View(tBL_CONCEPTOS);
        }

        // POST: TBL_CONCEPTOS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_CONCEPTO,CODIGO_CONCEPTO,DESCRIPCION,ABREVIATURA,NATURALEZA,MODO,TIPO_CUOTA,APLICACION_CUOTA,CONCEPTO_TOPE,PERIODICIDAD_CUOTA,PORCENTAJE_CUOTA,AGRUP_CONCEPTOS,PROP_TIEMPO,AGRUP_CALC_TIEMPO,VAL_EXCLUD,VALOR,FECHA_INICIO_DESC,FECHA_FIN_DESC,ESTADO_DESC,FORMA_PAGO,ESTADO_CONCEPTO")] TBL_CONCEPTOS tBL_CONCEPTOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBL_CONCEPTOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tBL_CONCEPTOS);
        }

        // GET: TBL_CONCEPTOS/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_CONCEPTOS tBL_CONCEPTOS = db.TBL_CONCEPTOS.Find(id);
            if (tBL_CONCEPTOS == null)
            {
                return HttpNotFound();
            }
            return View(tBL_CONCEPTOS);
        }

        // POST: TBL_CONCEPTOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TBL_CONCEPTOS tBL_CONCEPTOS = db.TBL_CONCEPTOS.Find(id);
            db.TBL_CONCEPTOS.Remove(tBL_CONCEPTOS);
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
