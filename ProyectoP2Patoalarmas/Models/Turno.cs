using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoP2Patoalarmas.Models
{
    public class Turno
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public int VehiculoId { get; set; }
        public int ServicioId { get; set; }
        public string? Estado { get; set; }

        // Relación con Vehículo
        public virtual Vehiculo? Vehiculo { get; set; }

        // Relación con Servicio
        public virtual Servicio? Servicio { get; set; }
    }
}
