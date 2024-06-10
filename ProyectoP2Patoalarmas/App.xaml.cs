using ProyectoP2Patoalarmas.Views;
using ProyectoP2Patoalarmas.Views.Admin;

namespace ProyectoP2Patoalarmas
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
            //MainPage = new NavigationPage(new HomeAdmin());

        }
    }
}
