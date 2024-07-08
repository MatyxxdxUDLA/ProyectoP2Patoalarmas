using ProyectoP2Patoalarmas.ViewModels;
using Microsoft.Maui.Controls;

namespace ProyectoP2Patoalarmas.Views.Admin
{
    public partial class VistaTurnos : ContentPage
    {
        public VistaTurnos()
        {
            InitializeComponent();
            BindingContext = new VistaTurnosViewModel();
        }
    }
}
