using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using ProyectoP2Patoalarmas.Models;

namespace ProyectoP2Patoalarmas.API
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            var response = await _httpClient.GetAsync("api/usuarios");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Usuario>>(content);
        }

        public async Task<List<Servicio>> GetServiciosAsync()
        {
            var response = await _httpClient.GetAsync("api/servicios");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Servicio>>(content);
        }

        public async Task CreateUsuarioAsync(Usuario usuario)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(usuario), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/usuarios", jsonContent);
            response.EnsureSuccessStatusCode();
        }
    }
}
