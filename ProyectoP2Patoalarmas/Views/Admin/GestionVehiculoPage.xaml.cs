using ProyectoP2Patoalarmas.Models;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace ProyectoP2Patoalarmas.Views.Admin
{
    public partial class GestionVehiculoPage : ContentPage
    {
        private VehiculoViewModel viewModel;

        public GestionVehiculoPage()
        {
            InitializeComponent();
            viewModel = new VehiculoViewModel();
            this.BindingContext = viewModel;
            this.Appearing += OnAppearing;
        }

        private async void OnAppearing(object sender, EventArgs e)
        {
            await viewModel.CargarVehiculos();
        }

        private async void OnGuardarVehiculoClicked(object sender, EventArgs e)
        {
            var marca = MarcaEntry.Text;
            var modelo = ModeloEntry.Text;
            var selectedUsuario = UsuarioPicker.SelectedItem as Usuario; // Casting seguro al tipo Usuario

            if (int.TryParse(AnioEntry.Text, out int anio) && selectedUsuario != null)
            {
                if (!string.IsNullOrWhiteSpace(marca) && !string.IsNullOrWhiteSpace(modelo) && anio > 0)
                {
                    Vehiculo nuevoVehiculo = new Vehiculo
                    {
                        Marca = marca,
                        Modelo = modelo,
                        Anio = anio,
                        UsuarioId = selectedUsuario.IdUsuario // Usar el ID del usuario seleccionado
                    };

                    await viewModel.AddVehiculo(nuevoVehiculo);

                    // Limpiar los campos despu�s de agregar el veh�culo
                    MarcaEntry.Text = "";
                    ModeloEntry.Text = "";
                    AnioEntry.Text = "";
                    UsuarioPicker.SelectedItem = null;

                    // Opcional: Recargar la lista de veh�culos
                    await viewModel.CargarVehiculos();
                }
                else
                {
                    await DisplayAlert("Error de Validaci�n", "Por favor, complete todos los campos requeridos para continuar.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error de Validaci�n", "Por favor, ingrese valores num�ricos v�lidos para A�o y seleccione un usuario.", "OK");
            }
        }
    }
}