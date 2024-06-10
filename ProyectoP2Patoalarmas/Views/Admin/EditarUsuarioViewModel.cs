using ProyectoP2Patoalarmas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProyectoP2Patoalarmas.Views.Admin
{
    public class EditarUsuarioViewModel : INotifyPropertyChanged
    {
        private AppDbContext _context;
        public Usuario Usuario { get; set; }
        public ICommand GuardarCambiosCommand { get; private set; }
        public ICommand VolverCommand { get; private set; }

        public EditarUsuarioViewModel(Usuario usuario)
        {
            Usuario = usuario;
            _context = new AppDbContext();
            GuardarCambiosCommand = new Command(async () => await GuardarCambios());
            VolverCommand = new Command(async () => await Volver());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private async Task GuardarCambios()
        {
            try
            {
                // Asumiendo que el contexto de la base de datos se ha inyectado o está disponible globalmente
                _context.Usuarios.Update(Usuario);
                await _context.SaveChangesAsync();

                // Navegar de regreso a la lista de usuarios o a la pantalla anterior después de guardar
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo guardar los cambios: " + ex.Message, "OK");
            }
        }

        private async Task Volver()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
