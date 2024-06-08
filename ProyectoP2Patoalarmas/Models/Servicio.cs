using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoP2Patoalarmas.Models
{
    public class Servicio
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? DuracionEstimada { get; set; }
        public decimal? Costo { get; set; }

        // Turnos que utilizan este servicio
        public List<Turno> Turnos { get; set; }

        public Servicio()
        {
            Turnos = new List<Turno>();
        }
    }
}
