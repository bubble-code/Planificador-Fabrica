using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Planificador_Fabrica.Constants;
using Planificador_Fabrica.Models;

namespace Planificador_Fabrica.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<OrdenFabricacion> OrdenFabricacion { get; set; } = null!;
        public DbSet<VRptOrdenFabricacion> VRptOrdenFabricacion { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrdenFabricacion>()
        .HasKey(o => o.IDOrden);  // Establece IDOrden como clave primaria
            modelBuilder.Entity<OrdenFabricacion>()
                .Property(o => o.IDOrden)
                .HasColumnName("IDOrden");

            modelBuilder.Entity<VRptOrdenFabricacion>()
        .HasNoKey() // Marca esta entidad como keyless
        .ToView("vRptOrdenFabricacionRutaEstructura_Favram");
            //modelBuilder.Entity<VRptOrdenFabricacion>()
            //.Ignore(v => v.DatosExtras);

            //    modelBuilder.Entity<VRptOrdenFabricacion>()
            //.Property(v => v.DatosExtras)
            //.HasConversion(
            //    v => JsonConvert.SerializeObject(v), // Convierte a JSON para guardar
            //    v => JsonConvert.DeserializeObject<Dictionary<string, object>>(v) // Convierte de JSON al cargar
            //);
        }
    }
}
