using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.GestionTareas
{
    public class Proyecto
    {
        [Key]public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<Tarea>? Tareas { get; set; } 
        public List<Usuario>? Usuarios { get; set; } 
    }
}
