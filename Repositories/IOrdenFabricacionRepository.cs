using Planificador_Fabrica.Filters;
using Planificador_Fabrica.Models;

namespace Planificador_Fabrica.Repositories
{
    public interface IOrdenFabricacionRepository : IRepositories<OrdenFabricacion>
    {
        Task<PaginatedResult<OrdenFabricacion>> GetFilteredAsync(Filter filter, int pageNumber, int pageSize);
        Task<IEnumerable<OrdenFabricacion>> GetOrdenesByEstadoAsync(int estado);
    }
}
