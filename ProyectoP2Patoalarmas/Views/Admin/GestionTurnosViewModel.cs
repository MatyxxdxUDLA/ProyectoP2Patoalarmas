using Microsoft.EntityFrameworkCore;
using ProyectoP2Patoalarmas.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using Microsoft.Maui.Storage;

namespace ProyectoP2Patoalarmas.Views.Admin
{
    public class GestionTurnosViewModel : INotifyPropertyChanged
    {
        private AppDbContext _context;
        public ObservableCollection<Servicio> Servicios { get; set; }
        public Usuario Usuario { get; set; }
        public bool UsuarioEncontrado { get; set; }
        public Servicio ServicioSeleccionado { get; set; }
        public DateTime FechaSeleccionada { get; set; }
        public TimeSpan HoraSeleccionada { get; set; }

        public ICommand BuscarUsuarioCommand { get; set; }
        public ICommand AgendarTurnoCommand { get; set; }


        public GestionTurnosViewModel()
        {
            _context = new AppDbContext();
            BuscarUsuarioCommand = new Command<string>(async (cedula) => await ExecuteBuscarUsuarioAsync(cedula));
            AgendarTurnoCommand = new Command(async () => await ExecuteAgendarTurno());
            Servicios = new ObservableCollection<Servicio>();
            CargarServicios(); // Asumiendo que tienes un método para cargar servicios desde la base de datos
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private async Task ExecuteBuscarUsuarioAsync(string cedula)
        {
            // Implementación de búsqueda de usuario
            try
            {
                Usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Cedula == cedula);

                if (Usuario != null)
                {
                    UsuarioEncontrado = true;
                    OnPropertyChanged(nameof(UsuarioEncontrado));
                    OnPropertyChanged(nameof(Usuario));
                }
                else
                {
                    UsuarioEncontrado = false;
                    OnPropertyChanged(nameof(UsuarioEncontrado));
                    DisplayNotFoundMessage(); // Método para mostrar mensaje de no encontrado
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores en caso de problemas con la base de datos u otros errores
                Console.WriteLine("Error al buscar usuario: " + ex.Message);
            }
        }

        private async Task ExecuteAgendarTurno()
        {
            string folderPath = FileSystem.AppDataDirectory;  // Usar AppDataDirectory para almacenar archivos de forma segura en todas las plataformas
            string filename = Path.Combine(folderPath, "agendamientos.txt");

            if (Usuario == null || ServicioSeleccionado == null)
            {
                Console.WriteLine("Error: Usuario o servicio no seleccionado.");
                return;
            }

            string turnoInfo = $"Usuario: {Usuario.Nombre}, Servicio: {ServicioSeleccionado.Nombre}, Fecha: {FechaSeleccionada.ToShortDateString()}, Hora: {HoraSeleccionada}\n";

            try
            {
                if (!File.Exists(filename))
                {
                    using (var writer = File.CreateText(filename))
                    {
                        await writer.WriteLineAsync(turnoInfo);
                    }
                }
                else
                {
                    using (var writer = File.AppendText(filename))
                    {
                        await writer.WriteLineAsync(turnoInfo);
                    }
                }
                Console.WriteLine("Turno guardado exitosamente en: " + filename);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar el turno: {ex.Message}");
            }
        }


        private async Task TestWriteFile()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(folderPath, "test.txt");

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    await writer.WriteLineAsync("Test line");
                }
                Console.WriteLine("Archivo escrito con éxito en " + filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al escribir en el archivo: " + ex.Message);
            }
        }


        private void DisplayNotFoundMessage()
        {
            // Puedes implementar lógica aquí para mostrar una alerta o actualizar una propiedad que muestre un mensaje en la interfaz de usuario
            Console.WriteLine("Usuario no encontrado.");
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task CargarServicios()
        {
            // Cargar servicios de la base de datos y añadirlos a Servicios
            using var context = new AppDbContext();
            var servicios = await context.Servicios.ToListAsync();
            Servicios.Clear();
            foreach (var usuario in servicios)
            {
                Servicios.Add(usuario);
            }
        }

        public string FileInfoText { get; set; }

        private void UpdateFileInfoDisplay()
        {
            string folderPath = FileSystem.AppDataDirectory;
            string filename = Path.Combine(folderPath, "agendamientos.txt");

            if (File.Exists(filename))
            {
                FileInfoText = $"Archivo encontrado en: {filename}";
            }
            else
            {
                FileInfoText = "Archivo no encontrado.";
            }
            OnPropertyChanged(nameof(FileInfoText)); // Notificar cambios a la UI si estás usando Binding
        }

    }

}
