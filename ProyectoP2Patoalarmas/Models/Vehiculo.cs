using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoP2Patoalarmas.Models
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public int Anio { get; set; }
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
