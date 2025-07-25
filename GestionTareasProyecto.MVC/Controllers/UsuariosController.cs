using System.Data;
using System.Threading.Tasks;
using GestionTareas.Consumer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Modelos.GestionTareas;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestionTareasProyecto.MVC.Controllers
{
    [Authorize(Roles = "usuario")]
    public class UsuariosController : Controller
    {
        // GET: UsuariosController
        public async Task< ActionResult> Index()
        {  var usuarios = await Crud<Usuario>.GetAllAsync();
            ViewBag.Usuarios = usuarios;
            return View(usuarios);
        }

        // GET: UsuariosController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var usuario = await Crud<Usuario>.GetByIdAsync(id);
            var tareas = await Crud<Tarea>.GetAllAsync();
            var proyectos = await Crud<Proyecto>.GetAllAsync(); 

            usuario.Tareas = tareas
                .Where(t => t.UsuarioId == usuario.Id)
                .ToList();

            foreach (var tarea in usuario.Tareas)
            {
                tarea.Proyecto = proyectos.FirstOrDefault(p => p.Id == tarea.ProyectoId); 
            }

            return View(usuario);
        }
        



        // GET: UsuariosController/Create
        public async Task<ActionResult> Create()
        {
            var tareas = await Crud<Tarea>.GetAllAsync();
            ViewBag.UsuarioId = new SelectList(tareas, "Id", "Nombre");
            var proyecto = await Crud<Proyecto>.GetAllAsync();
            ViewBag.ProyectoId = new SelectList(proyecto, "Id", "Nombre");
            return View();
        }

        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Usuario usuario)
        {
            try
            {    await Crud<Usuario>.CreateAsync(usuario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var tareas = await Crud<Tarea>.GetAllAsync();
                ViewBag.UsuarioId = new SelectList(tareas, "Id", "Nombre");
                var proyecto = await Crud<Proyecto>.GetAllAsync();
                ViewBag.ProyectoId = new SelectList(proyecto, "Id", "Nombre");
                return View();
            }
        }

        
        // GET: UsuariosController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var usuario = await Crud<Usuario>.GetByIdAsync(id);
            return View(usuario);
        }


        // POST: UsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id,Usuario usuario)
        {
            try
            {
                await Crud<Usuario>.UpdateAsync(id, usuario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(usuario);
            }
        }

        // GET: UsuariosController/Delete/5
        public async Task<ActionResult >Delete(int id)
        {
            var usuario = await Crud<Usuario>.GetByIdAsync(id);

            return View(usuario);
        }

        // POST: UsuariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Usuario usuario)
        {
            try
            {   await Crud<Usuario>.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(usuario);
            }
        }
    }
}
