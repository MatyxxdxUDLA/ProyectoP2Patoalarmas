using ProyectoP2Patoalarmas.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;

namespace ProyectoP2Patoalarmas.Views.Admin
{
    public class UsuarioViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context = new AppDbContext();
        public ObservableCollection<Usuario> Usuarios { get; set; }

        public ICommand AgregarUsuarioCommand { get; private set; }
        public ICommand EditarUsuarioCommand { get; private set; }
        public ICommand EliminarUsuarioCommand { get; private set; }

        public UsuarioViewModel()
        {
            Usuarios = new ObservableCollection<Usuario>();
            AgregarUsuarioCommand = new Command(async () => await ExecuteAgregarUsuario());
            EditarUsuarioCommand = new Command<Usuario>(async (usuario) => await EditarUsuario(usuario));
            EliminarUsuarioCommand = new Command<Usuario>(async (usuario) => await EliminarUsuario(usuario));
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private async Task ExecuteAgregarUsuario()
        {
            // Example logic to add a new user
            Usuario newUser = new Usuario { Cedula = "New User", Nombre = "Test", Email = "test@example.com", Password = "password" };
            await AddUser(newUser);
        }

        private async Task ExecuteEditarUsuario(Usuario usuario)
        {
            // Logic to edit an existing user
            usuario.Nombre = "Updated Name"; // Example change
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        private async Task ExecuteEliminarUsuario(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            Usuarios.Remove(usuario);
        }

        public async Task CargarUsuarios()
        {
            /*
            var usuariosFromDb = await _context.Usuarios.ToListAsync();
            Usuarios.Clear();
            foreach (var usuario in usuariosFromDb)
            {
                Usuarios.Add(usuario);
            }
            */

            using var context = new AppDbContext();
            var usuarios = await context.Usuarios.ToListAsync();
            Usuarios.Clear();
            foreach (var usuario in usuarios)
            {
                Usuarios.Add(usuario);
            }
        }


        public async Task AddUser(Usuario usuario)
        {
            /*
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            Usuarios.Add(usuario);
            */

            using var context = new AppDbContext();
            context.Add(usuario);
            await context.SaveChangesAsync();
            Usuarios.Add(usuario);
            OnPropertyChanged(nameof(Usuarios));
        }

        // Agregar usuario ya existente
        public async Task EditarUsuario(Usuario usuario)
        {
            var editPage = new EditarUsuarioPage();
            editPage.BindingContext = new EditarUsuarioViewModel(usuario);
            await Application.Current.MainPage.Navigation.PushAsync(editPage);
        }

        // Eliminar usuario
        public async Task EliminarUsuario(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            Usuarios.Remove(usuario);
            OnPropertyChanged(nameof(Usuarios));  // Notificar que la lista ha cambiado
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
