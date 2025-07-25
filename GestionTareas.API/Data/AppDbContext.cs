using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Modelos.GestionTareas;

    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Modelos.GestionTareas.Tarea> Tarea { get; set; } = default!;

public DbSet<Modelos.GestionTareas.Usuario> Usuario { get; set; } = default!;

public DbSet<Modelos.GestionTareas.Proyecto> Proyecto { get; set; } = default!;
    }
