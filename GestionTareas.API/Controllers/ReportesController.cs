using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Blazor;
using Modelos.GestionTareas;

namespace GestionTareas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ReportesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/Reportes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reporte>>> GetReporte()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("AppDbContext"));
            connection.Open();
            var reportes = connection.Query<Reporte>("SELECT * FROM Reporte").ToList();
            return reportes;
        }

        // GET: api/Reportes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reporte>> GetReporte(int id)
        {
           using var reportes = new SqlConnection(_configuration.GetConnectionString("AppDbContext"));
            reportes.Open();
            var reporte = reportes.QuerySingle<Reporte>("SELECT * FROM Reporte WHERE Id = @Id", new { Id = id });

            if (reporte == null)
            {
                return NotFound();
            }

            return reporte;
        }

        // PUT: api/Reportes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReporte(int id,[FromBody] Reporte reporte)
        {   
           using var connection = new SqlConnection(_configuration.GetConnectionString("AppDbContext"));
            connection.Open();
            connection.Execute("UPDATE Reporte SET UsuarioId=@UsurarioId, TareaId = @TareaId, ProyectoId=@ProyectoId, Estado=@Estado, Prioridad=@Prioridad, FechaVencimiento=@FechaVencimiento WHERE Id = @Id",
                new
                {UsuarioId=reporte.UsuarioId, 
                    TareaId = reporte.TareaId, 
                    ProyectoId = reporte.ProyectoId, 
                    Estado = reporte.Estado, 
                    Prioridad = reporte.Prioridad, 
                    FechaVencimiento = reporte.FechaVencimiento, 
                    Id = id });
            return NoContent();
        }

        // POST: api/Reportes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public Reporte PostReporte([FromBody]Reporte reporte)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("AppDbContext"));
            connection.Open();
            connection.Execute("INSERT INTO Reporte (UsuarioId, TareaId, ProyectoId, Estado, Prioridad, FechaVencimiento) VALUES (@UsuarioId, @TareaId, @ProyectoId, @Estado, @Prioridad, @FechaVencimiento)",
                new
                {
                    UsuarioId = reporte.UsuarioId,
                    TareaId = reporte.TareaId,
                    ProyectoId = reporte.ProyectoId,
                    Estado = reporte.Estado,
                    Prioridad = reporte.Prioridad,
                    FechaVencimiento = reporte.FechaVencimiento
                });

            return reporte;
        }

        // DELETE: api/Reportes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReporte(int id)
        {
           using var connection = new SqlConnection(_configuration.GetConnectionString("AppDbContext"));
            connection.Open();
            connection.Execute("DELETE FROM Reporte WHERE Id = @Id", new { Id = id });

            return NoContent();
        }

       
    }
}
