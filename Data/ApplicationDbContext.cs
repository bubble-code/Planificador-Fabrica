using Microsoft.EntityFrameworkCore;
using Planificador_Fabrica.Constants;
using Planificador_Fabrica.Models;

namespace Planificador_Fabrica.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<OrdenFabricacion> OrdenFabricacion { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrdenFabricacion>()
        .HasKey(o => o.IDOrden);  // Establece IDOrden como clave primaria
            modelBuilder.Entity<OrdenFabricacion>()
                .Property(o => o.IDOrden)
                .HasColumnName("IDOrden");
        }
    }
}
