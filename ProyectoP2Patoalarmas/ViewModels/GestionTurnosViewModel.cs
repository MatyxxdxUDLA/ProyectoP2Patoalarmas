using Microsoft.EntityFrameworkCore;
using ProyectoP2Patoalarmas.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace ProyectoP2Patoalarmas.ViewModels
{
    public class GestionTurnosViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context;
        public ObservableCollection<Servicio> Servicios { get; set; }
        public Usuario Usuario { get; set; }
        public bool UsuarioEncontrado { get; set; }
        public Servicio ServicioSeleccionado { get; set; }
        public DateTime FechaSeleccionada { get; set; }
        public TimeSpan HoraSeleccionada { get; set; }
        public ICommand BuscarUsuarioCommand { get; }
        public ICommand AgendarTurnoCommand { get; }
        public ICommand MostrarListaTurnosCommand { get; }

        public GestionTurnosViewModel()
        {
            _context = new AppDbContext();
            BuscarUsuarioCommand = new Command<string>(async (cedula) => await ExecuteBuscarUsuarioAsync(cedula));
            AgendarTurnoCommand = new Command(async () => await ExecuteAgendarTurno());
            MostrarListaTurnosCommand = new Command(async () => await MostrarListaTurnos());
            Servicios = new ObservableCollection<Servicio>();
            CargarServicios();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async Task ExecuteBuscarUsuarioAsync(string cedula)
        {
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
                    DisplayNotFoundMessage();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar usuario: " + ex.Message);
            }
        }

        private void DisplayNotFoundMessage()
        {
            Console.WriteLine("Usuario no encontrado.");
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task CargarServicios()
        {
            var servicios = await _context.Servicios.ToListAsync();
            Servicios.Clear();
            foreach (var servicio in servicios)
            {
                Servicios.Add(servicio);
            }
        }

        private async Task ExecuteAgendarTurno()
        {
            if (Usuario == null || ServicioSeleccionado == null || FechaSeleccionada == DateTime.MinValue)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
                return;
            }

            var nuevoTurno = new Turno
            {
                FechaHora = FechaSeleccionada.Add(HoraSeleccionada),
                UsuarioId = Usuario.Id,
                ServicioId = ServicioSeleccionado?.Id ?? 0,
                Estado = "Agendado"
            };

            try
            {
                _context.Turnos.Add(nuevoTurno);
                await _context.SaveChangesAsync();
                await Application.Current.MainPage.DisplayAlert("Éxito", "Turno agendado con éxito.", "OK");
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                await Application.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error al agendar el turno: {innerException}", "OK");
            }
        }

        private async Task MostrarListaTurnos()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProyectoP2Patoalarmas.Views.Admin.VistaTurnos());
        }
    }
}
