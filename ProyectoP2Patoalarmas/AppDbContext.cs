using Microsoft.EntityFrameworkCore;
using ProyectoP2Patoalarmas.Models;

namespace ProyectoP2Patoalarmas
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Turno> Turnos { get; set; }

        // Constructor que acepta DbContextOptions
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Asegúrate de no sobreescribir las opciones proporcionadas externamente a menos que no se proporcionen
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Filename=MiAppDatabase.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehiculo>()
                .HasOne(v => v.Usuario)
                .WithMany(u => u.Vehiculos)
                .HasForeignKey(v => v.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Turno>()
                .HasOne(t => t.Usuario)
                .WithMany(u => u.Turnos)
                .HasForeignKey(t => t.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Turno>()
                .HasOne(t => t.Servicio)
                .WithMany(s => s.Turnos)
                .HasForeignKey(t => t.ServicioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Turno>()
                .HasOne(t => t.Vehiculo)
                .WithMany(v => v.Turnos)
                .HasForeignKey(t => t.VehiculoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
