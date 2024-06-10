using ProyectoP2Patoalarmas.Views.Admin;

namespace ProyectoP2Patoalarmas.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        // Aquí verificarías las credenciales del usuario
        var username = UsernameEntry.Text;
        var password = PasswordEntry.Text;

        if (username == "admin" && password == "1234")
        {
            OnLoginSuccess();
        }
        else
        {
            // Mostrar un mensaje de error
            await DisplayAlert("Error", "Credenciales incorrectas", "OK");
        }
    }

    private void OnLoginSuccess()
    {
        // Cambia la página principal a HomeAdmin dentro de una NavigationPage
        Application.Current.MainPage = new NavigationPage(new HomeAdmin());
    }
}