using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;

namespace ProyectoP2Patoalarmas.API
{
    public class LocalApiServer
    {
        private readonly WebServer _server;

        public LocalApiServer()
        {
            _server = new WebServer(o => o
                    .WithUrlPrefix("http://localhost:9696/")
                    .WithMode(HttpListenerMode.EmbedIO))
                .WithWebApi("/api", m => m
                    .WithController<ApiController>());
        }

        public void Run()
        {
            _server.RunAsync();
            Console.WriteLine("Server is running on http://localhost:9696/");
        }

        public void Stop()
        {
            _server.Dispose();
        }
    }

}
