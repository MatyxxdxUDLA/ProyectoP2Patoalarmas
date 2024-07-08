using ProyectoP2Patoalarmas.Views;
using ProyectoP2Patoalarmas.Views.Admin;
using ProyectoP2Patoalarmas.API;

namespace ProyectoP2Patoalarmas
{
    public partial class App : Application
    {
        private readonly LocalApiServer _localApiServer;
        public App()
        {
            InitializeComponent();
            _localApiServer = new LocalApiServer();
            _localApiServer.Run();
            MainPage = new NavigationPage(new LoginPage());
            //MainPage = new NavigationPage(new HomeAdmin());

        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
