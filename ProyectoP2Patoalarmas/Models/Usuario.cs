using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoP2Patoalarmas.Models
{
    public class Usuario : Persona
    {
        [Key]
        public int IdUsuario { get; set; }
        public List<Vehiculo> Vehiculos { get; set; }
        public List<Turno> Turnos { get; set; } // Relación con Turno

        public Usuario()
        {
            Vehiculos = new List<Vehiculo>();
            Turnos = new List<Turno>();
        }
    }
}
