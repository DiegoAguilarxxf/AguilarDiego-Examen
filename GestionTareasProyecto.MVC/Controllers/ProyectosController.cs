using System.Data;
using System.Threading.Tasks;
using GestionTareas.Consumer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos.GestionTareas;

namespace GestionTareasProyecto.MVC.Controllers
{
   [ Authorize(Roles = "usuario")]
    public class ProyectosController : Controller
    {
        // GET: ProyectosController
        public async Task<ActionResult> Index()
        {
            var proyecto = await Crud<Proyecto>.GetAllAsync();
            ViewBag.Proyectos = proyecto;
            return View(proyecto);
        }

        // GET: ProyectosController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var proyecto = await Crud<Proyecto>.GetByIdAsync(id);
            var usuarios = await Crud<Usuario>.GetAllAsync();
            var tareas = await Crud<Tarea>.GetAllAsync();

            proyecto.Tareas = tareas
                .Where(t => t.ProyectoId == proyecto.Id)
                .ToList();

            foreach (var tarea in proyecto.Tareas)
            {
                tarea.Usuario = usuarios.FirstOrDefault(u => u.Id == tarea.UsuarioId); 
            }

            return View(proyecto);
        }


        // GET: ProyectosController/Create
        public ActionResult Create()
        { 
            return View();
        }

        // POST: ProyectosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Proyecto proyecto)
        {
            try
            { await Crud<Proyecto>.CreateAsync(proyecto);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(proyecto);
            }
        }

        // GET: ProyectosController/Edit/5
        public async Task<ActionResult> Edit(int id)
        { var proyecto = await Crud<Proyecto>.GetByIdAsync(id);
            return View(proyecto);
        }

        // POST: ProyectosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Proyecto proyecto)
        {
            try
            { await Crud<Proyecto>.UpdateAsync(id, proyecto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(proyecto);
            }
        }

        // GET: ProyectosController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var proyecto = await Crud<Proyecto>.GetByIdAsync(id);
            return View(proyecto);
        }

        // POST: ProyectosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Proyecto proyecto)
        {
            try
            {   await Crud <Proyecto>.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(proyecto);
            }
        }
    }
}
