namespace ProyectoP2Patoalarmas.Views.Admin;

public partial class GestionTurnosPage : ContentPage
{
	public GestionTurnosPage()
	{
		InitializeComponent();
        BindingContext = new GestionTurnosViewModel();
    }
}