
using PruebaBlueSoft_parte3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Collections.Generic;
using System.IO;

namespace PruebaBlueSoft_parte3.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Index()
        {
            return View();
        }

        // GET: Reporte/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reporte/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reporte/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reporte/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reporte/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reporte/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reporte/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult GenerarReporte()
        {
            var clientes = ObtenerClientesConTransacciones();

            using (var ms = new MemoryStream())
            {
                // Crear el escritor PDF con el flujo de memoria
                var writer = new PdfWriter(ms);

                // Crear el documento PDF
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                // Agregar un título
                document.Add(new Paragraph("Reporte de Transacciones")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(20));

                // Crear una tabla con 3 columnas
                var table = new iText.Layout.Element.Table(3); // Especifica el namespace completo para la clase Table

                // Agregar encabezados
                table.AddCell(new Cell().Add(new Paragraph("Cliente").SetBold()));
                table.AddCell(new Cell().Add(new Paragraph("Número de Transacciones").SetBold()));
                table.AddCell(new Cell().Add(new Paragraph("Monto Total").SetBold()));

                // Agregar filas a la tabla
                foreach (var cliente in clientes)
                {
                    table.AddCell(cliente.Nombre);
                    table.AddCell(cliente.NumeroTransacciones.ToString());
                    table.AddCell(cliente.MontoTotal.ToString());
                }

                document.Add(table);

                // Cerrar el documento
                document.Close();

                // Devolver el PDF como un archivo descargable
                return File(ms.ToArray(), "application/pdf", "ReporteTransacciones.pdf");
            }
        }
        private List<ClienteReporte> ObtenerClientesConTransacciones()
        {
            using (var context = new PruebaBluesoftEntities1())
            {
                // Consulta usando tipos anónimos
                var query = from cliente in context.Clientes
                            join transaccion in context.Transacciones
                            on cliente.Id equals transaccion.CuentaId
                            group new { cliente, transaccion } by cliente.Nombre into g
                            select new
                            {
                                Nombre = g.Key,
                                NumeroTransacciones = g.Count(),
                                MontoTotal = g.Sum(x => x.transaccion.Monto)
                            };

                // Convertir el resultado a una lista de ClienteReporte
                var resultados = query.ToList().Select(x => new ClienteReporte
                {
                    Nombre = x.Nombre,
                    NumeroTransacciones = x.NumeroTransacciones,
                    MontoTotal = x.MontoTotal
                }).ToList();

                return resultados;
            }
        }
    }
}
