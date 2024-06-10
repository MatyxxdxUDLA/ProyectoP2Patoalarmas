using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ProyectoP2Patoalarmas;
using Microsoft.Extensions.Logging;  // Asegúrate de usar el namespace correcto para AppDbContext

namespace ProyectoP2Patoalarmas
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Configura el DbContext para usar SQLite
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Filename=MiAppDatabase.db"));

            // Crear e inicializar la base de datos al inicio de la app
            InitializeDatabase(builder.Services);

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void InitializeDatabase(IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.EnsureCreated();  // Asegura que la base de datos esté creada
        }
    }
}
