using Microsoft.Maui.Controls;
using ProyectoP2Patoalarmas.ViewModels;

namespace ProyectoP2Patoalarmas.Views.Admin
{
    public partial class EditarVehiculoPage : ContentPage
    {
        public EditarVehiculoPage(VehiculoViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnGuardarCambiosClicked(object sender, EventArgs e)
        {
            if (BindingContext is VehiculoViewModel viewModel)
            {
                viewModel.GuardarCambiosCommand.Execute(null);
            }
        }

        private async void OnVolverClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
