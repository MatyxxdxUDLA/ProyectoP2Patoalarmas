using Microsoft.Extensions.DependencyInjection;
using ProyectoP2Patoalarmas;
using Microsoft.EntityFrameworkCore;

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
            });

        // Configuración del servicio de DbContext
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Filename=MiAppDatabase.db"));

        var app = builder.Build();

        // Forzar la eliminación y recreación de la base de datos (para desarrollo)
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //dbContext.Database.EnsureDeleted();

            dbContext.Database.EnsureCreated(); // Asegurar la creación de la base de datos y las tablas
        }

        return app;
    }
}