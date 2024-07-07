using ProyectoP2Patoalarmas.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using ProyectoP2Patoalarmas.Views.Admin;

namespace ProyectoP2Patoalarmas.ViewModels
{
    public class VehiculoViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context;

        public ObservableCollection<Vehiculo> Vehiculos { get; set; }
        public ObservableCollection<Usuario> Usuarios { get; set; } // Colección de usuarios
        public ICommand EditarVehiculoCommand { get; private set; }
        public ICommand EliminarVehiculoCommand { get; private set; }
        public ICommand GuardarCambiosCommand { get; private set; }

        private Vehiculo _selectedVehiculo;
        public Vehiculo SelectedVehiculo
        {
            get => _selectedVehiculo;
            set
            {
                _selectedVehiculo = value;
                OnPropertyChanged(nameof(SelectedVehiculo));
            }
        }

        public VehiculoViewModel()
        {
            _context = new AppDbContext();
            Vehiculos = new ObservableCollection<Vehiculo>();
            Usuarios = new ObservableCollection<Usuario>(); // Inicializa la colección de usuarios
            EditarVehiculoCommand = new Command<Vehiculo>(OnEditVehiculo);
            EliminarVehiculoCommand = new Command<Vehiculo>(OnDeleteVehiculo);
            GuardarCambiosCommand = new Command(OnGuardarCambios);
            CargarUsuarios();
            CargarVehiculos(); // Asegúrate de llamar a este método si necesitas cargar los vehículos al iniciar.
        }


        public async Task CargarVehiculos()
        {
            Vehiculos.Clear();
            var vehiculos = _context.Vehiculos.ToList();
            foreach (var vehiculo in vehiculos)
            {
                Vehiculos.Add(vehiculo);
            }
        }

        public void CargarUsuarios()
        {
            Usuarios.Clear();
            var usuarios = _context.Usuarios.ToList();
            foreach (var usuario in usuarios)
            {
                Usuarios.Add(usuario);
            }
        }

        public async Task AddVehiculo(Vehiculo vehiculo)
        {
            _context.Vehiculos.Add(vehiculo);
            await _context.SaveChangesAsync();
            Vehiculos.Add(vehiculo);
        }

        private async void OnEditVehiculo(Vehiculo vehiculo)
        {
            SelectedVehiculo = vehiculo;
            await Application.Current.MainPage.Navigation.PushAsync(new EditarVehiculoPage(this));
        }


        private void OnDeleteVehiculo(Vehiculo vehiculo)
        {
            var vehiculoExistente = _context.Vehiculos.FirstOrDefault(v => v.Id == vehiculo.Id);
            if (vehiculoExistente != null)
            {
                _context.Vehiculos.Remove(vehiculoExistente);
                _context.SaveChanges();

                Vehiculos.Remove(vehiculo);
            }
        }

        private async void OnGuardarCambios()
        {
            var vehiculoExistente = _context.Vehiculos.FirstOrDefault(v => v.Id == SelectedVehiculo.Id);
            if (vehiculoExistente != null)
            {
                vehiculoExistente.Marca = SelectedVehiculo.Marca;
                vehiculoExistente.Modelo = SelectedVehiculo.Modelo;
                vehiculoExistente.Anio = SelectedVehiculo.Anio;
                vehiculoExistente.UsuarioId = SelectedVehiculo.UsuarioId;
                await _context.SaveChangesAsync();
                OnPropertyChanged(nameof(Vehiculos));
            }

            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
