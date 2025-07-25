using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Modelos.GestionTareas;
using Dapper;

namespace GestionTareas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TareasController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/Tareas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarea>>> GetTarea()
        {
           using var connection= new SqlConnection(_configuration.GetConnectionString("AppDbContext"));
            connection.Open();
            var tareas = connection.Query<Tarea>("SELECT * FROM Tarea").ToList();
            return tareas;
        }

        // GET: api/Tareas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> GetTarea(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("AppDbContext"));
            connection.Open();
            var tarea = connection.QuerySingle<Tarea>("SELECT * FROM Tarea WHERE Id = @Id", new { Id = id });

            if (tarea == null)
            {
                return NotFound();
            }

            return tarea;
        }

        // PUT: api/Tareas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarea(int id, [FromBody]Tarea tarea)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("AppDbContext"));
            connection.Open();
            connection.Execute("UPDATE Tarea SET Nombre = @Nombre, Descripcion = @Descripcion, Estado = @Estado, Prioridad = @Prioridad, FechaCreacion = @FechaCreacion, FechaVencimiento = @FechaVencimiento, UsuarioId = @UsuarioId, ProyectoId=@ProyectoId WHERE Id = @Id",
                new
                {
                    
                    Nombre = tarea.Nombre,
                    Descripcion = tarea.Descripcion,
                    Estado = tarea.Estado,
                    Prioridad = tarea.Prioridad,
                    FechaCreacion = tarea.FechaCreacion,
                    FechaVencimiento = tarea.FechaVencimiento,
                    UsuarioId = tarea.UsuarioId,
                    ProyectoId=tarea.ProyectoId
                    ,
                    Id = id
                });

            return NoContent();
        }

        // POST: api/Tareas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public Tarea PostTarea([FromBody]Tarea tarea)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("AppDbContext"));
            connection.Open();
            connection.Execute("INSERT INTO Tarea (Nombre, Descripcion, Estado, Prioridad, FechaCreacion, FechaVencimiento, UsuarioId, ProyectoId) VALUES (@Nombre, @Descripcion, @Estado, @Prioridad, @FechaCreacion, @FechaVencimiento, @UsuarioId, @ProyectoId)",
                new
                {
                    Nombre = tarea.Nombre,
                    Descripcion = tarea.Descripcion,
                    Estado = tarea.Estado,
                    Prioridad = tarea.Prioridad,
                    FechaCreacion = tarea.FechaCreacion,
                    FechaVencimiento = tarea.FechaVencimiento,
                    UsuarioId = tarea.UsuarioId,
                    ProyectoId=tarea.ProyectoId
                });

            return tarea;
        }

        // DELETE: api/Tareas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("AppDbContext"));
            connection.Open();
            connection.Execute("DELETE FROM Tarea WHERE Id = @Id", new { Id = id });

            return NoContent();
        }

        
    }
}
