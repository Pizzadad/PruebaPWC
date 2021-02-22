using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PruebaPWC.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaPWC.Persistence
{
    /// <summary>
    /// Clase de la conexion a la BD
    /// </summary>
    public class PruebaBdContext : IdentityDbContext<Usuario>
    {
        /// <summary>
        /// Constructor de la clase de la conexion a la BD
        /// </summary>
        public PruebaBdContext(DbContextOptions<PruebaBdContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Login> Login { get; set; }

        public DbSet<Producto>  Producto { get; set; }
    }
}
