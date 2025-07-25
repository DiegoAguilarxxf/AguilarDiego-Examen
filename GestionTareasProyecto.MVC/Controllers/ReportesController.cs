using System.Threading.Tasks;
using GestionTareas.Consumer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos.GestionTareas;

namespace GestionTareasProyecto.MVC.Controllers
{
    public class ReportesController : Controller
    {
        // GET: ReportesController
        public async Task<ActionResult> Index(string buscar, string priori)
        {
            var reportes = await Crud<Reporte>.GetAllAsync();
            var tareas = await Crud<Tarea>.GetAllAsync();

            if (!string.IsNullOrEmpty(buscar))
            {
                var tareasFiltradasIds = tareas
                    .Where(t => t.Nombre != null && t.Nombre.ToLower().Contains(buscar.ToLower()))
                    .Select(t => t.Id)
                    .ToList();

                reportes = reportes
                    .Where(r => tareasFiltradasIds.Contains(r.TareaId))
                    .ToList();
            }

            if (priori == "prioridad_desc")
                reportes = reportes.OrderByDescending(r => r.Prioridad).ToList();
            else if (priori == "prioridad_asc")
                reportes = reportes.OrderBy(r => r.Prioridad).ToList();

            ViewBag.Buscar = buscar;
            ViewBag.Priori = priori;

            ViewBag.Tareas = tareas;
            ViewBag.Proyectos = await Crud<Proyecto>.GetAllAsync();
            ViewBag.Usuarios = await Crud<Usuario>.GetAllAsync();

            return View(reportes);
        }




        // GET: ReportesController/Details/5
        public async Task <ActionResult> Details(int id)
        {
            var reporte = await Crud<Reporte>.GetByIdAsync(id);
            return View(reporte);
        }

        // GET: ReportesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReportesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReportesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReportesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReportesController/Delete/5
        public async Task<ActionResult> Delete(int id)
        { var reporte=await Crud<Reporte>.DeleteAsync(id);
            return View(reporte);
        }

        // POST: ReportesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Reporte reporte)
        {
            try
            {
                await Crud<Reporte>.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(reporte);
            }
        }
    }
}
