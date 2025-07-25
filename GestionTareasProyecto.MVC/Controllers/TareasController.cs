using System.Data;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GestionTareas.Consumer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Modelos.GestionTareas;
using System.Linq;

namespace GestionTareasProyecto.MVC.Controllers
{
    [Authorize(Roles = "usuario")]
    public class TareasController : Controller
    {
        // GET: TareasController
        public async Task<ActionResult> Index(string buscar, string priori)
        {
            var tareas = await Crud<Tarea>.GetAllAsync();

            if (!string.IsNullOrEmpty(buscar))
            {
                tareas = tareas
                    .Where(t => t.Nombre != null && t.Nombre.ToLower().Contains(buscar.ToLower()))
                    .ToList();
            }

            if (priori == "prioridad_desc")
                tareas = tareas.OrderByDescending(t => t.Prioridad).ToList();
            else if (priori == "prioridad_asc")
                tareas = tareas.OrderBy(t => t.Prioridad).ToList();

            ViewBag.Buscar = buscar;
            ViewBag.Priori = priori;

            return View(tareas);
        }


        // GET: TareasController/Details/5
        public async Task<ActionResult> Details(int id)
        {   var tarea = await Crud<Tarea>.GetByIdAsync(id);
            var tareaid = await Crud<Tarea>.GetAllAsync();
            ViewBag.UsuarioId = new SelectList(tareaid, "Id", "UsuarioId");
            return View(tarea);
        }

        // GET: TareasController/Create
        public async Task<ActionResult> Create()
        {
            var usuario = await Crud<Usuario>.GetAllAsync();
            ViewBag.UsuarioId = new SelectList(usuario, "Id", "Nombre");
            var proyecto = await Crud<Proyecto>.GetAllAsync();
            ViewBag.ProyectoId = new SelectList(proyecto, "Id", "Nombre");
            return View();
        }

        // POST: TareasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Tarea tarea)
        {
            try
            {
                await Crud<Tarea>.CreateAsync(tarea);
                var tareaid = await Crud<Tarea>.GetByIdAsync(tarea.Id);
               
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var usuario = await Crud<Usuario>.GetAllAsync();
                ViewBag.UsuarioId = new SelectList(usuario, "Id", "Nombre");

                var proyecto = await Crud<Proyecto>.GetAllAsync();
                ViewBag.ProyectoId = new SelectList(proyecto, "Id", "Nombre");
                return View();
            }
        }

        // GET: TareasController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var tarea = await Crud<Tarea>.GetByIdAsync(id);
            var usuario = await Crud<Usuario>.GetAllAsync();
            var proyecto = await Crud<Proyecto>.GetAllAsync();
            ViewBag.UsuarioId = new SelectList(usuario, "Id", "Nombre", tarea.UsuarioId);
            ViewBag.ProyectoId = new SelectList(proyecto, "Id", "Nombre", tarea.ProyectoId);
            return View(tarea);
        }

        // POST: TareasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Tarea tareas)
        {
            try
            {
                await Crud<Tarea>.UpdateAsync(id, tareas);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var usuario = await Crud<Usuario>.GetAllAsync();
                ViewBag.UsuarioId = new SelectList(usuario, "Id", "Nombre");
                var proyecto = await Crud<Proyecto>.GetAllAsync();
                ViewBag.ProyectoId = new SelectList(proyecto, "Id", "Nombre");
                return View(tareas);
            }
        }

        // GET: TareasController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var tareas = await Crud<Tarea>.GetByIdAsync(id);
            return View(tareas);
        }

        // POST: TareasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Tarea tarea)
        {
            try
            {
               await Crud<Tarea>.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(tarea);
            }
        }
    }
}
