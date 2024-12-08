using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Planificador_Fabrica.Data;
using Planificador_Fabrica.Enums;
using Planificador_Fabrica.Filters;
using Planificador_Fabrica.Models;
using Planificador_Fabrica.Repositories;

namespace Planificador_Fabrica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenFabricacionController : ControllerBase
    {
        private readonly IOrdenFabricacionRepository _ordenFabricacionRepository;

        public OrdenFabricacionController(IOrdenFabricacionRepository ordenFabricacionRepository)
        {
            _ordenFabricacionRepository = ordenFabricacionRepository;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var ordenes = await _ordenFabricacionRepository.GetAllAsync();
            return Ok(ordenes);
        }

        [HttpGet("IDOrden")]
        public async Task<IActionResult> GetById(int id)
        {
            var orden = await _ordenFabricacionRepository.GetByIdAsync(id,"IDOrden");
            if (orden == null)
                return NotFound();

            return Ok(orden);
        }

        [HttpGet("byEstado")]
        public async Task<IActionResult> GetByEstado([FromQuery] int estado)
        {
            var ordenes = await _ordenFabricacionRepository.GetOrdenesByEstadoAsync(estado);
            return Ok(ordenes);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdenesFabricacion(
    [FromQuery] int? id,
    [FromQuery] string? norden,
    [FromQuery] bool? activas,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("El número de páginas y el tamaño deben ser mayor que cero.");
            }

            // Crear el filtro
            var filter = new Filter();
            if (id.HasValue)
            {
                filter.Add(nameof(OrdenFabricacion.IDOrden), FilterCondition.Equals, id.Value);
            }
            if (!string.IsNullOrEmpty(norden))
            {
                filter.Add(nameof(OrdenFabricacion.NOrden), FilterCondition.Equals, norden);
            }
            if (activas.HasValue && activas.Value)
            {
                filter.Add(nameof(OrdenFabricacion.Estado), FilterCondition.GreaterThanOrEqual, 2);
                filter.Add(nameof(OrdenFabricacion.Estado), FilterCondition.LessThanOrEqual, 3);
            }

            // Usa el repositorio para obtener datos paginados y filtrados
            var result = await _ordenFabricacionRepository.GetFilteredAsync(filter, pageNumber, pageSize);

            return Ok(result);
        }
    }
}
