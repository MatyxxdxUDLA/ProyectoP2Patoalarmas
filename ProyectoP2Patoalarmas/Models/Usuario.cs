using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoP2Patoalarmas.Models
{
    public class Usuario : Persona
    {
        [Key]
        public int IdUsuario { get; set; }
        public List<Vehiculo> Vehiculos { get; set; }
        public Usuario()
        {
            Vehiculos = new List<Vehiculo>();
        }
    }
}
