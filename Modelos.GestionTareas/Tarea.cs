using System.ComponentModel.DataAnnotations;

namespace Modelos.GestionTareas
{
    public class Tarea
    {
        [Key]public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public string Prioridad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int UsuarioId { get; set; }
        public int ProyectoId { get; set; }
        public Proyecto? Proyecto { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
