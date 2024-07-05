using ProyectoP2Patoalarmas.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProyectoP2Patoalarmas.ViewModels
{
    public class GestionServiciosViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context;

        public ObservableCollection<Servicio> Servicios { get; set; }
        private Servicio _newServicio;
        public Servicio NewServicio
        {
            get => _newServicio;
            set
            {
                _newServicio = value;
                OnPropertyChanged(nameof(NewServicio));
            }
        }

        public ICommand AddServicioCommand { get; }
        public ICommand UpdateServicioCommand { get; }
        public ICommand DeleteServicioCommand { get; }

        public GestionServiciosViewModel()
        {
            _context = new AppDbContext();
            Servicios = new ObservableCollection<Servicio>(_context.Servicios.ToList());
            NewServicio = new Servicio();

            AddServicioCommand = new Command(async () => await OnAddServicio());
            UpdateServicioCommand = new Command<Servicio>(async (servicio) => await OnUpdateServicio(servicio));
            DeleteServicioCommand = new Command<Servicio>(OnDeleteServicio);
        }

        private async Task OnAddServicio()
        {
            var nuevoServicio = new Servicio
            {
                Nombre = NewServicio.Nombre,
                Descripcion = NewServicio.Descripcion,
                DuracionEstimada = NewServicio.DuracionEstimada,
                Costo = NewServicio.Costo
            };
            _context.Servicios.Add(nuevoServicio);
            _context.SaveChanges();

            Servicios.Add(nuevoServicio);

            // Limpiar el formulario después de agregar el servicio
            NewServicio = new Servicio();
            OnPropertyChanged(nameof(NewServicio));

            // Mostrar mensaje de confirmación
            await Application.Current.MainPage.DisplayAlert("Éxito", "Servicio agregado", "OK");
        }

        private async Task OnUpdateServicio(Servicio servicio)
        {
            var servicioExistente = _context.Servicios.FirstOrDefault(s => s.Id == servicio.Id);
            if (servicioExistente != null)
            {
                servicioExistente.Nombre = servicio.Nombre;
                servicioExistente.Descripcion = servicio.Descripcion;
                servicioExistente.DuracionEstimada = servicio.DuracionEstimada;
                servicioExistente.Costo = servicio.Costo;
                _context.SaveChanges();

                // Actualiza la lista de servicios para reflejar los cambios
                var index = Servicios.IndexOf(servicio);
                Servicios[index] = servicioExistente;
                OnPropertyChanged(nameof(Servicios));

                // Mostrar mensaje de confirmación
                await Application.Current.MainPage.DisplayAlert("Éxito", "Servicio actualizado", "OK");
            }
        }

        private void OnDeleteServicio(Servicio servicio)
        {
            var servicioExistente = _context.Servicios.FirstOrDefault(s => s.Id == servicio.Id);
            if (servicioExistente != null)
            {
                _context.Servicios.Remove(servicioExistente);
                _context.SaveChanges();

                Servicios.Remove(servicio);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
