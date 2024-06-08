using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoP2Patoalarmas.Models
{
    public class Usuario : Persona
    {
        public List<Vehiculo> Vehiculos { get; set; }
        public Usuario()
        {
            Vehiculos = new List<Vehiculo>();
        }
    }
}
