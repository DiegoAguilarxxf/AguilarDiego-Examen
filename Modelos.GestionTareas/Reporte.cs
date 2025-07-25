using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.GestionTareas
{
    public class Reporte
    {
        [Key] public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int TareaId { get; set; }
        public int ProyectoId { get; set; }      
        public string Estado{ get; set; }
        public string Prioridad { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public Usuario? Usuario { get; set; }
        public Tarea? Tarea { get; set; }
        public Proyecto? Proyecto { get; set; }
    }
}
