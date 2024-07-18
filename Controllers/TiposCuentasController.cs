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
    public class TiposCuentasController : Controller
    {
        private PruebaBluesoftEntities1 db = new PruebaBluesoftEntities1();

        // GET: TiposCuentas
        public ActionResult Index()
        {
            return View(db.TiposCuenta.ToList());
        }

        // GET: TiposCuentas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposCuenta tiposCuenta = db.TiposCuenta.Find(id);
            if (tiposCuenta == null)
            {
                return HttpNotFound();
            }
            return View(tiposCuenta);
        }

        // GET: TiposCuentas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiposCuentas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Descripcion")] TiposCuenta tiposCuenta)
        {
            if (ModelState.IsValid)
            {
                db.TiposCuenta.Add(tiposCuenta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tiposCuenta);
        }

        // GET: TiposCuentas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposCuenta tiposCuenta = db.TiposCuenta.Find(id);
            if (tiposCuenta == null)
            {
                return HttpNotFound();
            }
            return View(tiposCuenta);
        }

        // POST: TiposCuentas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Descripcion")] TiposCuenta tiposCuenta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiposCuenta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tiposCuenta);
        }

        // GET: TiposCuentas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposCuenta tiposCuenta = db.TiposCuenta.Find(id);
            if (tiposCuenta == null)
            {
                return HttpNotFound();
            }
            return View(tiposCuenta);
        }

        // POST: TiposCuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TiposCuenta tiposCuenta = db.TiposCuenta.Find(id);
            db.TiposCuenta.Remove(tiposCuenta);
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
