using Microsoft.EntityFrameworkCore;

namespace Planificador_Fabrica.Models
{
    public class OFContext:DbContext
    {
        public OFContext(DbContextOptions<OFContext> options) : base(options) { }
        public DbSet<OrdenFabricacion> OrdenFabricaciones { get; set; } = null!;
    }
}
