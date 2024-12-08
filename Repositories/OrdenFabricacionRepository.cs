using Microsoft.EntityFrameworkCore;
using Planificador_Fabrica.Data;
using Planificador_Fabrica.Filters;
using Planificador_Fabrica.Models;

namespace Planificador_Fabrica.Repositories
{
    public class OrdenFabricacionRepository : Repository<OrdenFabricacion>, IOrdenFabricacionRepository
    {
        private readonly ApplicationDbContext _context;

        public OrdenFabricacionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<OrdenFabricacion>> GetFilteredAsync(Filter filter, int pageNumber, int pageSize)
        {
            var query = _context.OrdenFabricacion.AsQueryable();
            query = query.ApplyFilters(filter);
            var totalItems = await query.CountAsync();
            var paginatedData = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PaginatedResult<OrdenFabricacion>
            {
                TotalItems = totalItems,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Data = paginatedData
            };
        }

        public async Task<IEnumerable<OrdenFabricacion>> GetOrdenesByEstadoAsync(int estado)
        {
            return await _context.OrdenFabricacion
                .Where(o => o.Estado == estado)
                .ToListAsync();
        }
    }

    public class PaginatedResult<T>
    {
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public List<T> Data { get; set; } = new List<T>();
    }
}
