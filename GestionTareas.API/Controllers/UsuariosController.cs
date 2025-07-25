using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Modelos.GestionTareas;

namespace GestionTareas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UsuariosController(IConfiguration configuration)
        {
            _configuration=configuration;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {using var connection = new SqlConnection(_configuration.GetConnectionString("AppDbContext"));
            connection.Open();
            var usuarios= connection.Query<Usuario>("SELECT * FROM Usuario").ToList();
            return usuarios;
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("AppDbContext"));
            connection.Open();
            var usuario = connection.QuerySingle<Usuario>("SELECT * FROM Usuario WHERE Id = @Id", new { Id = id });
            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        { using var connection= new SqlConnection(_configuration.GetConnectionString("AppDbContext"));
            connection.Open();
            connection.Execute("UPDATE Usuario SET Nombre = @Nombre, CorreoElectronico = @CorreoElectronico WHERE Id = @Id",
                new { Nombre = usuario.Nombre, CorreoElectronico = usuario.CorreoElectronico, Id = id });

            return NoContent();
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public Usuario PostUsuario(Usuario usuario)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("AppDbContext"));
            connection.Open();
            connection.Execute("INSERT INTO Usuario (Nombre, CorreoElectronico) VALUES (@Nombre, @CorreoElectronico)",
                new { Nombre = usuario.Nombre, CorreoElectronico = usuario.CorreoElectronico });

            return usuario;
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
           using var connection = new SqlConnection(_configuration.GetConnectionString("AppDbContext"));
            connection.Open();
            connection.Execute("DELETE FROM Usuario WHERE Id = @Id", new { Id = id });

            return NoContent();
        }

        
    }
}
