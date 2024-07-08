using ProyectoP2Patoalarmas.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.EntityFrameworkCore;

namespace ProyectoP2Patoalarmas.ViewModels
{
    public class VehiculoViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context;

        private string _marca;
        private string _modelo;
        private int _anio;
        private Usuario _selectedUsuario;
        private Vehiculo _selectedVehiculo;

        public ObservableCollection<Vehiculo> Vehiculos { get; set; }
        public ObservableCollection<Usuario> Usuarios { get; set; }

        public string Marca
        {
            get => _marca;
            set
            {
                _marca = value;
                OnPropertyChanged();
            }
        }

        public string Modelo
        {
            get => _modelo;
            set
            {
                _modelo = value;
                OnPropertyChanged();
            }
        }

        public int Anio
        {
            get => _anio;
            set
            {
                _anio = value;
                OnPropertyChanged();
            }
        }

        public Usuario SelectedUsuario
        {
            get => _selectedUsuario;
            set
            {
                _selectedUsuario = value;
                OnPropertyChanged();
            }
        }

        public Vehiculo SelectedVehiculo
        {
            get => _selectedVehiculo;
            set
            {
                _selectedVehiculo = value;
                if (value != null)
                {
                    Marca = value.Marca;
                    Modelo = value.Modelo;
                    Anio = value.Anio;
                    SelectedUsuario = Usuarios.FirstOrDefault(u => u.IdUsuario == value.UsuarioId);
                }
                OnPropertyChanged();
            }
        }

        public ICommand AddVehiculoCommand { get; }
        public ICommand UpdateVehiculoCommand { get; }
        public ICommand DeleteVehiculoCommand { get; }

        public VehiculoViewModel()
        {
            _context = new AppDbContext();
            Vehiculos = new ObservableCollection<Vehiculo>(_context.Vehiculos.ToList());
            Usuarios = new ObservableCollection<Usuario>(_context.Usuarios.ToList());

            AddVehiculoCommand = new Command(async () => await AddVehiculo());
            UpdateVehiculoCommand = new Command<Vehiculo>(async (vehiculo) => await UpdateVehiculo(vehiculo));
            DeleteVehiculoCommand = new Command<Vehiculo>(async (vehiculo) => await DeleteVehiculo(vehiculo));

            CargarVehiculos();
            CargarUsuarios();
        }

        public async Task CargarVehiculos()
        {
            Vehiculos.Clear();
            var vehiculos = await _context.Vehiculos.ToListAsync();
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

        public async Task AddVehiculo()
        {
            var nuevoVehiculo = new Vehiculo
            {
                Marca = Marca,
                Modelo = Modelo,
                Anio = Anio,
                UsuarioId = SelectedUsuario.IdUsuario
            };
            _context.Vehiculos.Add(nuevoVehiculo);
            await _context.SaveChangesAsync();

            Vehiculos.Add(nuevoVehiculo);

            // Limpiar el formulario después de agregar el vehículo
            Marca = string.Empty;
            Modelo = string.Empty;
            Anio = 0;
            SelectedUsuario = null;

            // Mostrar mensaje de confirmación
            await Application.Current.MainPage.DisplayAlert("Éxito", "Vehículo agregado", "OK");
        }

        public async Task UpdateVehiculo(Vehiculo vehiculo)
        {
            var vehiculoExistente = _context.Vehiculos.FirstOrDefault(v => v.Id == vehiculo.Id);
            if (vehiculoExistente != null)
            {
                vehiculoExistente.Marca = vehiculo.Marca;
                vehiculoExistente.Modelo = vehiculo.Modelo;
                vehiculoExistente.Anio = vehiculo.Anio;
                vehiculoExistente.UsuarioId = vehiculo.UsuarioId;
                await _context.SaveChangesAsync();

                // Actualiza la lista de vehículos para reflejar los cambios
                var index = Vehiculos.IndexOf(vehiculo);
                Vehiculos[index] = vehiculoExistente;
                OnPropertyChanged(nameof(Vehiculos));

                // Mostrar mensaje de confirmación
                await Application.Current.MainPage.DisplayAlert("Éxito", "Vehículo actualizado", "OK");
            }
        }

        public async Task DeleteVehiculo(Vehiculo vehiculo)
        {
            var vehiculoExistente = _context.Vehiculos.FirstOrDefault(v => v.Id == vehiculo.Id);
            if (vehiculoExistente != null)
            {
                _context.Vehiculos.Remove(vehiculoExistente);
                await _context.SaveChangesAsync();

                Vehiculos.Remove(vehiculo);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
