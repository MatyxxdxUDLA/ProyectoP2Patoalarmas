using ProyectoP2Patoalarmas.ViewModels;

namespace ProyectoP2Patoalarmas.Views.Admin
{
    public partial class GestionVehiculoPage : ContentPage
    {
        private VehiculoViewModel viewModel;

        public GestionVehiculoPage()
        {
            InitializeComponent();
            viewModel = new VehiculoViewModel();
            BindingContext = viewModel;
        }

        private async void OnAppearing(object sender, EventArgs e)
        {
            await viewModel.CargarVehiculos();
        }
    }
}
