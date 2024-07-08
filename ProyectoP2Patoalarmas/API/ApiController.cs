using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Microsoft.EntityFrameworkCore;
using ProyectoP2Patoalarmas.Models;

namespace ProyectoP2Patoalarmas.API
{
    public class ApiController : WebApiController
    {
        [Route(HttpVerbs.Get, "/usuarios")]
        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            // Aquí deberías utilizar tu contexto de base de datos para obtener los usuarios
            using var dbContext = new AppDbContext();
            return await dbContext.Usuarios.ToListAsync();
        }

        [Route(HttpVerbs.Get, "/servicios")]
        public async Task<IEnumerable<Servicio>> GetServicios()
        {
            using var dbContext = new AppDbContext();
            return await dbContext.Servicios.ToListAsync();
        }

        [Route(HttpVerbs.Post, "/usuarios")]
        public async Task CreateUsuario([JsonData] Usuario usuario)
        {
            using var dbContext = new AppDbContext();
            dbContext.Usuarios.Add(usuario);
            await dbContext.SaveChangesAsync();
        }
    }
}
