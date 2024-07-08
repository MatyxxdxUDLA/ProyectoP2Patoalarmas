using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoP2Patoalarmas.Models
{
    public class Vehiculo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Marca { get; set; }
        [Required]
        public string? Modelo { get; set; }
        [Required]
        public int Anio { get; set; }
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        // Relación con Usuario
        public virtual Usuario? Usuario { get; set; }

        // Lista de turnos reservados para este vehículo
        public List<Turno> Turnos { get; set; }

        public Vehiculo()
        {
            Turnos = new List<Turno>();
        }
    }
}
