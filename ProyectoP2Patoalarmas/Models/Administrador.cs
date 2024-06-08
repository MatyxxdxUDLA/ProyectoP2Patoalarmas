using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoP2Patoalarmas.Models
{
    public class Administrador : Persona
    {
        public string? AreaResponsabilidad { get; set; }
        public List<Turno> TurnosAdministrados { get; set; }
        public List<Servicio> ServiciosGestionados { get; set; }
        public List<Usuario> UsuariosGestionados { get; set; }

        public Administrador()
        {
            TurnosAdministrados = new List<Turno>();
            ServiciosGestionados = new List<Servicio>();
            UsuariosGestionados = new List<Usuario>();
        }
    }
}
