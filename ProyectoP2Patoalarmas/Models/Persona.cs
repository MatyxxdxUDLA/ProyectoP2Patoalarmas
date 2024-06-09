using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoP2Patoalarmas.Models
{
    public abstract class Persona
    {
        public int Id { get; set; }
        public string? Cedula { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
