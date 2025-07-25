using GestionTareas.Consumer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos.GestionTareas;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestionTareasProyecto.MVC.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: UsuariosController
        public async Task< ActionResult> Index()
        {  var usuarios = await Crud<Usuario>.GetAllAsync();
            ViewBag.Usuarios = usuarios;
            return View(usuarios);
        }

        // GET: UsuariosController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var usuario = await Crud<Usuario>.GetByIdAsync(id);
            return View(usuario);
        }

        // GET: UsuariosController/Create
        public ActionResult Create()
        { 
            return View();
        }

        // POST: UsuariosController/Create
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
