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
using System.Text;

namespace DESCUENTOS_ASOCIADOS.Controllers
{
    public class TBL_CONCEPTO_ASOCIADOController : Controller
    {
        private DESCUENTOS_ASOCIADOSEntities db = new DESCUENTOS_ASOCIADOSEntities();

        // GET: TBL_CONCEPTO_ASOCIADO
        public async Task<ViewResult> Index(string sortOrder, string searchString1, string searchString2, string searchString3, string searchString4)
        {    //HACER UNA TABLA QUE CARGUE TODOS LOS ASOCIADOS Y LUEGO SELECCIONAR EL ASOCIADO Y COLOCARLE EL CONCEPTO LUEGO
             //LUEGO DEPENDIENDO DEL CONCEPTO SE LE APLICA LA PERIODICIDAD FECHA INICIAL DE LA CUENTA Y FECHA FINAL
             //1. el registro del concepto va hasta concepto tope
             // 2.cuentas contables: HAY CONCEPTOS QUE LES APLICO LA FECHA FINAL O DE VENCIMIENTO
             //           APORTE NO DEBE TENER VENCIMIENTO
             //AHORRO VOLUNTARIO SI DEBE TENER FECHA FINAL PERO CON PERIODICIDAD A SEIS MESES O CONCEPTOS CON FINAL
             //SI ES APORTE
             //3.FINALIZACION DEL ASOCIADO ACTIVO CANCELADO SUSPENDIDO
             //4.DETALLE CONCEPTOS POR ACTIVOS DE LISTADO DE INFORMACION PARA EL ASOCIADO COBRARLE TODOS LOS FILTROS POR LIBRANZA
             //5.TODOS LOS QUE CUMPLAN CON LA FECHA Y PERIODICIDAD LOS ACTIVO PARA ESE MES
             //6.ASOCIADOS EXISTENTES PUEDEN AGREGAR CONCEPTOS EXISTENTES + CONCEPTOS A SU CUENTA
             //7.SE AFILIA NUEVA APORTE DE 90000 SE ACTIVA EL MES ACTUAL
             //8.LIQUIDACION DE DESCUENTOS SE DEBE PODER MODIFICAR LA FORMA DE PAGO
             //9.DETALLE CONCEPTO
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "ProductName desc" : "";
            ViewBag.UnitPriceSortParm = sortOrder == "UnitPrice" ? "UnitPrice desc" : "UnitPrice";

            ViewBag.SearchString = searchString1;
            ViewBag.SearchString = searchString2;
            ViewBag.SearchString = searchString3;
            ViewBag.SearchString = searchString4;


            var products = db.TBL_CONCEPTO_ASOCIADO.Include(t => t.TBL_ASOCIADO).Include(t => t.TBL_CONCEPTOS);

            if (string.IsNullOrEmpty(searchString1) && string.IsNullOrEmpty(searchString2) && string.IsNullOrEmpty(searchString3) && string.IsNullOrEmpty(searchString4))
            {
                products = db.TBL_CONCEPTO_ASOCIADO.Include(t => t.TBL_ASOCIADO).Include(t => t.TBL_CONCEPTOS);
                return View(await products.ToListAsync());
            }
               
            if (searchString1 == null && searchString2 == null && searchString3 == null && searchString4 == null)
            {
                 products = db.TBL_CONCEPTO_ASOCIADO.Include(t => t.TBL_ASOCIADO).Include(t => t.TBL_CONCEPTOS);
                return View(await products.ToListAsync());
            }

           
            products = from a in products
                                   where (a.PERIODICIDAD.ToUpper().Contains(searchString1.ToUpper())
                                   || a.FORMA_PAGO.ToUpper().Contains(searchString2.ToUpper())
                                   || a.ESTADO_CUENTA.ToString() == searchString3
                                   || a.ID_CONCEPTO.ToString() == searchString4)
                                   select a;
          
           

            switch (sortOrder)
            {
                case "ProductName desc":
                    products = products.OrderByDescending(s => s.PERIODICIDAD);
                    break;
                case "UnitPrice":
                    products = products.OrderBy(s => s.PERIODICIDAD);
                    break;
                case "UnitPrice desc":
                    products = products.OrderByDescending(s => s.VALOR);
                    break;
                default:
                    products = products.OrderBy(s => s.ID_ASOCIADO);
                    break;
            }
         
            return View(await products.ToListAsync());
        }
        public ActionResult GenerarExcel()
        {
            var CONCEPTOS = (from p in db.TBL_CONCEPTO_ASOCIADO
                               
                                 select p).ToList();



            StringBuilder builder = new System.Text.StringBuilder();

            //Agregamos las cabezeras 
            builder.Append("PERIODICIDAD").Append(";")
            .Append("ESTADO_CUENTA").Append(";")
            .Append("FECHA INICIAL").Append(";")
            .Append("FECHA FINAL").Append(";")
            .Append("FECHA VENCIMIENTO").Append(";")
            .Append("ID_ASOCIADO").Append(";")
            .Append("ID_CONCEPTO").Append(";")
            .Append("VALOR").Append(";")
            .Append("FORMA DE PAGO");
            builder.Append("\n");

            foreach (var item in CONCEPTOS)
            {

                

                builder.Append(item.PERIODICIDAD).Append(";");
                builder.Append(item.ESTADO_CUENTA).Append(";");
                builder.Append(item.FECHA_INICIAL).Append(";");
                builder.Append(item.FECHA_FINAL).Append(";");
                builder.Append(item.FECHA_VENC).Append(";");
                builder.Append(item.ID_ASOCIADO).Append(";");
                builder.Append(item.ID_CONCEPTO).Append(";");
                builder.Append(item.VALOR).Append(";");
                builder.Append(item.FORMA_PAGO).Append(";");

                builder.Append("\n");// agregamos una nueva fila 
            }


            // Lo encodeamos con UTF8 para mostrar los acentos correctamente.
            var excelBytes = Encoding.UTF8.GetBytes(builder.ToString());
            var excelConUT8Encoding = Encoding.UTF8.GetPreamble().Concat(excelBytes).ToArray();

            // guardamos el contenido del archivo en la ruta especificada
            var rutaExcel = Server.MapPath("~/App_Data/excel.csv");
            System.IO.File.WriteAllBytes(rutaExcel, excelConUT8Encoding);

            return File(rutaExcel, "text/csv", "Calificaciones.csv");
        }
        // GET: TBL_CONCEPTO_ASOCIADO/Details/5
        public async Task<ActionResult> Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_CONCEPTO_ASOCIADO tBL_CONCEPTO_ASOCIADO = await db.TBL_CONCEPTO_ASOCIADO.FindAsync(id);
            if (tBL_CONCEPTO_ASOCIADO == null)
            {
                return HttpNotFound();
            }
            return View(tBL_CONCEPTO_ASOCIADO);
        }

        // GET: TBL_CONCEPTO_ASOCIADO/Create
        public ActionResult Create()
        {
            ViewBag.ASOCIADOS = db.TBL_ASOCIADO.ToList();
            List<SelectListItem> Periodicidad = new List<SelectListItem>();
            Periodicidad.Add(new SelectListItem
            {
                Text = "Mensual",
                Value = "Mensual"
            });
            Periodicidad.Add(new SelectListItem
            {
                Text = "Trimestral",
                Value = "Trimestral"
            });
            Periodicidad.Add(new SelectListItem
            {
                Text = "Semestral",
                Value = "Semestral"
            });
            Periodicidad.Add(new SelectListItem
            {
                Text = "Anual",
                Value = "Anual"
            });
            @ViewBag.Periodicidad = Periodicidad;
            ViewBag.ID_ASOCIADO = new SelectList(db.TBL_ASOCIADO, "ID_ASOCIADO", "NOMBRE");
            ViewBag.ID_CONCEPTO = new SelectList(db.TBL_CONCEPTOS, "ID_CONCEPTO", "CODIGO_CONCEPTO");
            return View();
        }

        // POST: TBL_CONCEPTO_ASOCIADO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_CONASC,ID_CONCEPTO,ID_ASOCIADO,FECHA_INICIAL,FECHA_FINAL,ESTADO_CUENTA,VALOR,PERIODICIDAD,FORMA_PAGO,FECHA_VENC")] TBL_CONCEPTO_ASOCIADO tBL_CONCEPTO_ASOCIADO)
        {
            
            if (ModelState.IsValid)
            {
                db.TBL_CONCEPTO_ASOCIADO.Add(tBL_CONCEPTO_ASOCIADO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ID_ASOCIADO = new SelectList(db.TBL_ASOCIADO, "ID_ASOCIADO", "NOMBRE", tBL_CONCEPTO_ASOCIADO.ID_ASOCIADO);
            ViewBag.ID_CONCEPTO = new SelectList(db.TBL_CONCEPTOS, "ID_CONCEPTO", "CODIGO_CONCEPTO", tBL_CONCEPTO_ASOCIADO.ID_CONCEPTO);
            return View(tBL_CONCEPTO_ASOCIADO);
        }

        // GET: TBL_CONCEPTO_ASOCIADO/Edit/5
        public async Task<ActionResult> Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_CONCEPTO_ASOCIADO tBL_CONCEPTO_ASOCIADO = await db.TBL_CONCEPTO_ASOCIADO.FindAsync(id);
            if (tBL_CONCEPTO_ASOCIADO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_ASOCIADO = new SelectList(db.TBL_ASOCIADO, "ID_ASOCIADO", "NOMBRE", tBL_CONCEPTO_ASOCIADO.ID_ASOCIADO);
            ViewBag.ID_CONCEPTO = new SelectList(db.TBL_CONCEPTOS, "ID_CONCEPTO", "CODIGO_CONCEPTO", tBL_CONCEPTO_ASOCIADO.ID_CONCEPTO);
            return View(tBL_CONCEPTO_ASOCIADO);
        }

        // POST: TBL_CONCEPTO_ASOCIADO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_CONASC,ID_CONCEPTO,ID_ASOCIADO,FECHA_INICIAL,FECHA_FINAL,ESTADO_CUENTA,VALOR,PERIODICIDAD,FORMA_PAGO,FECHA_VENC")] TBL_CONCEPTO_ASOCIADO tBL_CONCEPTO_ASOCIADO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBL_CONCEPTO_ASOCIADO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_ASOCIADO = new SelectList(db.TBL_ASOCIADO, "ID_ASOCIADO", "NOMBRE", tBL_CONCEPTO_ASOCIADO.ID_ASOCIADO);
            ViewBag.ID_CONCEPTO = new SelectList(db.TBL_CONCEPTOS, "ID_CONCEPTO", "CODIGO_CONCEPTO", tBL_CONCEPTO_ASOCIADO.ID_CONCEPTO);
            return View(tBL_CONCEPTO_ASOCIADO);
        }

        // GET: TBL_CONCEPTO_ASOCIADO/Delete/5
        public async Task<ActionResult> Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_CONCEPTO_ASOCIADO tBL_CONCEPTO_ASOCIADO = await db.TBL_CONCEPTO_ASOCIADO.FindAsync(id);
            if (tBL_CONCEPTO_ASOCIADO == null)
            {
                return HttpNotFound();
            }
            return View(tBL_CONCEPTO_ASOCIADO);
        }

        // POST: TBL_CONCEPTO_ASOCIADO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(decimal id)
        {
            TBL_CONCEPTO_ASOCIADO tBL_CONCEPTO_ASOCIADO = await db.TBL_CONCEPTO_ASOCIADO.FindAsync(id);
            db.TBL_CONCEPTO_ASOCIADO.Remove(tBL_CONCEPTO_ASOCIADO);
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
