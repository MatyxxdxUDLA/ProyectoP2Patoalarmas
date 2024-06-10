namespace ProyectoP2Patoalarmas.Views.Admin;

public partial class HomeAdmin : FlyoutPage
{
	public HomeAdmin()
	{
		InitializeComponent();
	}

    private async void OnGestionUsuariosClicked(object sender, EventArgs e)
    {
        var gestionUsuarioPage = new GestionUsuario();
        var navigationPage = new NavigationPage(gestionUsuarioPage);

        // Asignar el Detail
        Detail = navigationPage;

        /*
        // Esperar a que la p�gina se muestre
        gestionUsuarioPage.Appearing += (s, args) => {
            Device.BeginInvokeOnMainThread(() => {
                IsPresented = false;  // Intenta cerrar el Flyout una vez que la p�gina est� completamente cargada
            });
        };
        */
    }
    private async void OnGestionServiciosClicked(object sender, EventArgs e)
    {
        var gestionServicioPage = new GestionServiciosPage();
        var navigationPage = new NavigationPage(gestionServicioPage);

        // Asignar el Detail
        Detail = navigationPage; // Cambia la p�gina principal a la de login
    }
    private async void OnGestionTurnosClicked(object sender, EventArgs e)
    {
        var gestionTurnosPage = new GestionTurnosPage();
        var navigationPage = new NavigationPage(gestionTurnosPage);

        // Asignar el Detail
        Detail = navigationPage; // Cambia la p�gina principal a la de login
    }
    private async void OnGestionVehiculoClicked(object sender, EventArgs e)
    {
        var gestionVehiculoPage = new GestionVehiculoPage();
        var navigationPage = new NavigationPage(gestionVehiculoPage);

        // Asignar el Detail
        Detail = navigationPage; // Cambia la p�gina principal a la de login
    }
    private async void OnCerrarSesionClicked(object sender, EventArgs e)
    {
        // Aqu� puedes agregar cualquier l�gica necesaria para "cerrar sesi�n", como limpiar datos
        await Application.Current.MainPage.Navigation.PopToRootAsync(); // Esto asume que quieres volver a la p�gina ra�z
        Application.Current.MainPage = new NavigationPage(new LoginPage()); // Cambia la p�gina principal a la de login
    }



}