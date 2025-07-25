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
    public class ProyectosController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProyectosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/Proyectos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proyecto>>> GetProyecto()
        {
            using var connection=new SqlConnection(_configuration.GetConnectionString("AppDbContext"));
            connection.Open();
            var proyecto=  connection.Query<Proyecto>("SELECT * FROM Proyecto"). ToList();
            return proyecto;
        }

        // GET: api/Proyectos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proyecto>> GetProyecto(int id)
        {   using var connection = new SqlConnection(_configuration.GetConnectionString("AppDbContext"));
            connection.Open();
            var proyecto = connection.QuerySingle<Proyecto>("SELECT * FROM Proyecto WHERE Id = @Id", new { Id = id });
         

            if (proyecto == null)
            {
                return NotFound();
            }

            return proyecto;
        }

        // PUT: api/Proyectos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProyecto(int id,[FromBody] Proyecto proyecto)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("AppDbContext"));
            connection.Open();
            connection.Execute("UPDATE Proyecto SET Nombre = @Nombre, Descripcion = @Descripcion WHERE Id = @Id",
                new { Nombre=proyecto.Nombre,
                    Descripcion= proyecto.Descripcion, 
                    Id = id });

            return NoContent();
        }

        // POST: api/Proyectos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public Proyecto PostProyecto([FromBody]Proyecto proyecto)
        {    using var connection = new SqlConnection(_configuration.GetConnectionString("AppDbContext"));
            connection.Open();
            connection.Execute("INSERT INTO Proyecto (Nombre, Descripcion) VALUES (@Nombre, @Descripcion)",
                new { Nombre = proyecto.Nombre, 
                    Descripcion = proyecto.Descripcion });
            return proyecto;
        }

        // DELETE: api/Proyectos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProyecto(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("AppDbContext"));
            connection.Open();
            connection.Execute("DELETE FROM Proyecto WHERE Id = @Id", new { Id = id });

            return NoContent();
        }

        
    }
}
