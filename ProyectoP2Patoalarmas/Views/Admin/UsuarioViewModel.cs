using ProyectoP2Patoalarmas.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProyectoP2Patoalarmas.Views.Admin
{
    public class UsuarioViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Usuario> Usuarios { get; set; }
        public ICommand AgregarUsuarioCommand { get; private set; }
        public ICommand EditarUsuarioCommand { get; private set; }
        public ICommand EliminarUsuarioCommand { get; private set; }

        public UsuarioViewModel()
        {
            Usuarios = new ObservableCollection<Usuario>();
            AgregarUsuarioCommand = new Command(ExecuteAgregarUsuario);
            EditarUsuarioCommand = new Command<Usuario>(ExecuteEditarUsuario);
            EliminarUsuarioCommand = new Command<Usuario>(ExecuteEliminarUsuario);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void ExecuteAgregarUsuario()
        {
            // Lógica para agregar usuario
        }

        private void ExecuteEditarUsuario(Usuario usuario)
        {
            // Lógica para editar usuario
        }

        private void ExecuteEliminarUsuario(Usuario usuario)
        {
            // Lógica para eliminar usuario
        }

        public void CargarUsuarios()
        {
            // Cargar datos de usuarios desde la base de datos o servicio
        }
        public void AgregarUsuario(Usuario usuario)
        {
            // Aquí deberías agregar lógica para guardar el usuario en la base de datos
            Usuarios.Add(usuario);
        }
    }
}
