using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PruebaBlueSoft_parte3.Models;

namespace PruebaBlueSoft_parte3.Controllers
{
    public class CuentasController : Controller
    {
        private PruebaBluesoftEntities1 db = new PruebaBluesoftEntities1();

        // GET: Cuentas
        public ActionResult Index()
        {
            var cuentas = db.Cuentas.Include(c => c.Clientes);
            ViewBag.TiposCuenta = GetTiposCuentaSelectList();
            return View(cuentas.ToList());
        }
        private SelectList GetTiposCuentaSelectList()
        {
            var tiposCuenta = db.TiposCuenta.ToList();
            return new SelectList(tiposCuenta, "Id", "Nombre");
        }
        // GET: Cuentas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuentas cuentas = db.Cuentas.Find(id);
            if (cuentas == null)
            {
                return HttpNotFound();
            }
            return View(cuentas);
        }

        // GET: Cuentas/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre");
            ViewBag.TiposCuenta = new SelectList(db.TiposCuenta, "Id", "Nombre");
            return View();
        }

        // POST: Cuentas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClienteId,TipoId,Saldo,CiudadOrigen")] Cuentas cuentas)
        {
            if (ModelState.IsValid)
            {
                db.Cuentas.Add(cuentas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre", cuentas.ClienteId);
            ViewBag.TiposCuenta = new SelectList(db.TiposCuenta, "Id", "Nombre", cuentas.TipoId);
            return View(cuentas);
        }

        // GET: Cuentas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Buscar la cuenta por su Id incluyendo el objeto TiposCuenta
            Cuentas cuentas = db.Cuentas.Include(c => c.TiposCuenta).SingleOrDefault(c => c.Id == id);

            if (cuentas == null)
            {
                return HttpNotFound();
            }

            // Cargar las opciones de tipos de cuenta para el DropDownList
            ViewBag.TiposCuenta = new SelectList(db.TiposCuenta, "Id", "Nombre", cuentas.TipoId);

            return View(cuentas);
        }

        // POST: Cuentas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClienteId,Tipo,Saldo,CiudadOrigen")] Cuentas cuentas)
        {
            if (ModelState.IsValid)
            {
                // Actualizar el objeto TiposCuenta basado en el TipoId seleccionado
                cuentas.TiposCuenta = db.TiposCuenta.Find(cuentas.TipoId);
                db.Entry(cuentas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // Recargar las opciones de tipos de cuenta en caso de error
            ViewBag.TiposCuenta = new SelectList(db.TiposCuenta, "Id", "Nombre", cuentas.TipoId);
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nombre", cuentas.ClienteId);
            return View(cuentas);
        }

        // GET: Cuentas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuentas cuentas = db.Cuentas.Find(id);
            if (cuentas == null)
            {
                return HttpNotFound();
            }
            return View(cuentas);
        }

        // POST: Cuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cuentas cuentas = db.Cuentas.Find(id);
            db.Cuentas.Remove(cuentas);
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
