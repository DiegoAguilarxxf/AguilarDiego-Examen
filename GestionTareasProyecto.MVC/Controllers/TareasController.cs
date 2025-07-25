using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GestionTareas.Consumer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Modelos.GestionTareas;
namespace GestionTareasProyecto.MVC.Controllers
{
    public class TareasController : Controller
    {
        // GET: TareasController
        public async Task<ActionResult> Index()
        {
            var tareas = await Crud<Tarea>.GetAllAsync();
            ViewBag.Tareas = tareas;
           
            return View(tareas);
        }

        // GET: TareasController/Details/5
        public async Task<ActionResult> Details(int id)
        { var tarea = await Crud<Tarea>.GetByIdAsync(id);
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
        public async Task<ActionResult> Create(Tarea collection)
        {
            try
            { await Crud<Tarea>.CreateAsync(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {var usuario=await Crud<Usuario>.GetAllAsync();
                ViewBag.UsuarioId = new SelectList(usuario, "Id", "Nombre");
               
                var proyecto = await Crud<Proyecto>.GetAllAsync();
                ViewBag.ProyectoId = new SelectList(proyecto, "Id", "Nombre");
                return View();
            }
        }

        // GET: TareasController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var usuario = await Crud<Usuario>.GetAllAsync();
            ViewBag.UsuarioId = new SelectList(usuario, "Id", "Nombre");
            var proyecto = await Crud<Proyecto>.GetAllAsync();
            ViewBag.ProyectoId = new SelectList(proyecto, "Id", "Nombre");
            return View();
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
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
