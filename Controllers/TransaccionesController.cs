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
    public class TransaccionesController : Controller
    {
        private PruebaBluesoftEntities1 db = new PruebaBluesoftEntities1();

        // GET: Transacciones
        public ActionResult Index()
        {
            ViewBag.transac= db.Transacciones.ToList();
            var transacciones = db.Transacciones.Include(t => t.Cuentas);
            //var transacciones = db.Transacciones.Include(t => t.Cuentas);
            return View(transacciones.ToList());
        }

        // GET: Transacciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transacciones transacciones = db.Transacciones.Find(id);
            if (transacciones == null)
            {
                return HttpNotFound();
            }
            return View(transacciones);
        }

        // GET: Transacciones/Create
        public ActionResult Create()
        {
            // Asignar las listas de selección a ViewBag
            ViewBag.CuentaId = new SelectList(db.Cuentas, "Id", "Id");
            ViewBag.TipoTransaccionId = new SelectList(db.Transacciones, "Id", "CuentaId");

            return View();
        }

        // POST: Transacciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CuentaId,TipoTransaccion,Monto,FechaTransaccion")] Transacciones transacciones)
        {
            if (ModelState.IsValid)
            {
                // Verifica si la CuentaId y TipoTransaccionId existen en la base de datos
                var cuenta = db.Cuentas.Find(transacciones.CuentaId);
                //var tipoTransaccion = db.Transacciones.Find(transacciones.TipoTransaccion);

                if (cuenta == null)
                {
                    ModelState.AddModelError("", "La cuenta o el tipo de transacción no existen.");
                    ViewBag.CuentaId = new SelectList(db.Cuentas, "Id", "ClienteId", transacciones.CuentaId);
                    ViewBag.TipoTransaccionId = new SelectList(db.Transacciones, "Id", "CuentaId", transacciones.Id);
                    return View(transacciones);
                }

                db.Transacciones.Add(transacciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Si el modelo no es válido, vuelve a cargar las listas desplegables
            ViewBag.CuentaId = new SelectList(db.Cuentas, "Id", "NombreCuenta", transacciones.CuentaId);
            ViewBag.TipoTransaccionId = new SelectList(db.Transacciones, "Id", "Nombre", transacciones.Id);
            return View(transacciones);
        }

        // GET: Transacciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transacciones transacciones = db.Transacciones.Find(id);
            if (transacciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.CuentaId = new SelectList(db.Cuentas, "Id", "NombreCuenta", transacciones.CuentaId);
            ViewBag.TipoTransaccionId = new SelectList(db.Transacciones, "Id", "Nombre", transacciones.Id);
            return View(transacciones);
        }

        // POST: Transacciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CuentaId,TipoTransaccion,Monto,FechaTransaccion")] Transacciones transacciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transacciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CuentaId = new SelectList(db.Cuentas, "Id", "NombreCuenta", transacciones.CuentaId);
            ViewBag.TipoTransaccionId = new SelectList(db.Transacciones, "Id", "Nombre", transacciones.Id);
            return View(transacciones);
        }

        // GET: Transacciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transacciones transacciones = db.Transacciones.Find(id);
            if (transacciones == null)
            {
                return HttpNotFound();
            }
            return View(transacciones);
        }

        // POST: Transacciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transacciones transacciones = db.Transacciones.Find(id);
            db.Transacciones.Remove(transacciones);
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
