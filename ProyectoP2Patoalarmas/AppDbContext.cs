﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoP2Patoalarmas.Models;

namespace ProyectoP2Patoalarmas
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        // Constructor que acepta DbContextOptions
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Asegúrate de no sobreescribir las opciones proporcionadas externamente a menos que no se proporcionen
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Filename=MiAppDatabase.db");
            }
        }
    }
}
