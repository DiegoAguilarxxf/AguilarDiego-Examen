using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Modelos.GestionTareas;

namespace GestionTareasProyecto.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Modelos.GestionTareas.Usuario> Usuario { get; set; } = default!;
        public DbSet<Modelos.GestionTareas.Tarea> Tarea { get; set; } = default!;
        public DbSet<Modelos.GestionTareas.Proyecto> Proyecto { get; set; } = default!;
    }
}
