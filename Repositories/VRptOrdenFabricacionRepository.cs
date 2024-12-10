using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Planificador_Fabrica.Data;
using Planificador_Fabrica.Filters;
using Planificador_Fabrica.Models;
using System.Dynamic;

namespace Planificador_Fabrica.Repositories
{
    public class VRptOrdenFabricacionRepository
    {
        private readonly ApplicationDbContext _context;
        public VRptOrdenFabricacionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PaginatedResultVista<ExpandoObject>> GetFilteredAsync(Filter filter, int pageNumber, int pageSize)
        {
            var query = _context.VRptOrdenFabricacion.AsQueryable();
            query = query.ApplyFilters(filter);

            var totalItems = await query.CountAsync();
            var paginatedData = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var dynamicData = paginatedData.Select(item =>
            {
                var newItem = new VRptOrdenFabricacion
                {
                    IdOrden = item.IdOrden,
                    NOrden = item.NOrden,
                    IDArticulo = item.IDArticulo,
                    FechaReq = item.FechaReq,
                    ExtraFields = new Dictionary<string, object>()
                };

                // Agregar dinámicamente todas las columnas que no están en la clase modelo
                foreach (var property in item.GetType().GetProperties())
                {
                    if (!newItem.GetType().GetProperties().Any(p => p.Name == property.Name))
                    {
                        newItem.ExtraFields.Add(property.Name, property.GetValue(item));
                    }
                }

                return newItem;
            }).ToList();

            return new PaginatedResultVista<ExpandoObject>
            {
                TotalItems = totalItems,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Data = dynamicData
            };
        }
    }

    public class PaginatedResultVista<T>
    {
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public List<VRptOrdenFabricacion> Data { get; set; } = new List<VRptOrdenFabricacion>();
    }
}
