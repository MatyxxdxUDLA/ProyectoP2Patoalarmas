using ProyectoP2Patoalarmas.Models;

namespace ProyectoP2Patoalarmas.Views.Admin;

public partial class GestionUsuario : ContentPage
{
    private UsuarioViewModel viewModel;

    public GestionUsuario()
    {
        InitializeComponent();
        viewModel = new UsuarioViewModel();
        this.BindingContext = viewModel;
        // Asignar el manejador de eventos
        this.Appearing += OnAppearing;
    }

    private async void OnAppearing(object sender, EventArgs e)
    {
        await viewModel.CargarUsuarios();
    }

    private async void OnGuardarUsuarioClicked(object sender, EventArgs e)
    {
        var cedula = CedulaEntry.Text;
        var nombre = NombreEntry.Text;
        var email = EmailEntry.Text;
        var password = PasswordEntry.Text;

        if (!string.IsNullOrWhiteSpace(cedula) && !string.IsNullOrWhiteSpace(nombre) && !string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
        {
            Usuario nuevoUsuario = new Usuario
            {
                Cedula = cedula,
                Nombre = nombre,
                Email = email,
                Password = password
            };

            await viewModel.AddUser(nuevoUsuario);

            // Limpiar los campos después de agregar el usuario
            CedulaEntry.Text = "";
            NombreEntry.Text = "";
            EmailEntry.Text = "";
            PasswordEntry.Text = "";

            // Opcional: Recargar la lista de usuarios
            await viewModel.CargarUsuarios();
        }
        else
        {
            // Mostrar un mensaje de error al usuario indicando que todos los campos son requeridos
            await DisplayAlert("Error de Validación", "Por favor, complete todos los campos requeridos para continuar.", "OK");
        }
    }
}
