using ProyectoP2Patoalarmas.Models;

namespace ProyectoP2Patoalarmas.Views.Admin;

public partial class GestionUsuario : ContentPage
{
    private UsuarioViewModel viewModel;
    public GestionUsuario()
	{
		InitializeComponent();
        // Crear una instancia del ViewModel
        viewModel = new UsuarioViewModel();

        // Establecer el contexto de datos de la página
        this.BindingContext = viewModel;

        // Cargar los datos de los usuarios, si necesario
        viewModel.CargarUsuarios();
    }

    private async void OnGuardarUsuarioClicked(object sender, EventArgs e)
    {
        var cedula = CedulaEntry.Text;
        var nombre = NombreEntry.Text;
        var email = EmailEntry.Text;
        var password = PasswordEntry.Text;

        if (!string.IsNullOrWhiteSpace(cedula) && !string.IsNullOrWhiteSpace(nombre) && !string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
        {
            // Asumiendo que tienes una función en tu ViewModel para agregar el usuario.
            viewModel.AgregarUsuario(new Usuario { Cedula = cedula, Nombre = nombre, Email = email, Password = password });

            // Limpiar los campos después de agregar el usuario
            CedulaEntry.Text = "";
            NombreEntry.Text = "";
            EmailEntry.Text = "";
            PasswordEntry.Text = "";
        }
        else
        {
            // Mostrar un mensaje de error al usuario indicando que todos los campos son requeridos
            await DisplayAlert("Error de Validación", "Por favor, complete todos los campos requeridos para continuar.", "OK");
        }
    }


}