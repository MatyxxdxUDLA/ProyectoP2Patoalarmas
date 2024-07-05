using ProyectoP2Patoalarmas.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProyectoP2Patoalarmas.ViewModels
{
    public class VehiculoViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context;

        public ObservableCollection<Vehiculo> Vehiculos { get; set; }
        public ObservableCollection<Usuario> Usuarios { get; set; } // Colección de usuarios
        public ICommand UpdateVehiculoCommand { get; }
        public ICommand DeleteVehiculoCommand { get; }

        public VehiculoViewModel()
        {
            _context = new AppDbContext();
            Vehiculos = new ObservableCollection<Vehiculo>();
            Usuarios = new ObservableCollection<Usuario>(); // Inicializa la colección de usuarios
            UpdateVehiculoCommand = new Command<Vehiculo>(OnUpdateVehiculo);
            DeleteVehiculoCommand = new Command<Vehiculo>(OnDeleteVehiculo);
            CargarUsuarios();
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

        private void OnUpdateVehiculo(Vehiculo vehiculo)
        {
            var vehiculoExistente = _context.Vehiculos.FirstOrDefault(v => v.Id == vehiculo.Id);
            if (vehiculoExistente != null)
            {
                vehiculoExistente.Marca = vehiculo.Marca;
                vehiculoExistente.Modelo = vehiculo.Modelo;
                vehiculoExistente.Anio = vehiculo.Anio;
                vehiculoExistente.UsuarioId = vehiculo.UsuarioId;
                _context.SaveChanges();
                var index = Vehiculos.IndexOf(vehiculo);
                Vehiculos[index] = vehiculoExistente;
                OnPropertyChanged(nameof(Vehiculos));
            }
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}