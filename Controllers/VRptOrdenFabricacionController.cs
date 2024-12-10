using Microsoft.AspNetCore.Mvc;
using Planificador_Fabrica.Enums;
using Planificador_Fabrica.Filters;
using Planificador_Fabrica.Models;
using Planificador_Fabrica.Repositories;

namespace Planificador_Fabrica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VRptOrdenFabricacionController: ControllerBase
    {
        private readonly VRptOrdenFabricacionRepository _repository;
        public VRptOrdenFabricacionController(VRptOrdenFabricacionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetOrdenesFabricacion([FromQuery] int? IdOrden,
    [FromQuery] string? nOrden,
    [FromQuery] string? idArticulo,
    [FromQuery] string? idCliente,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("El número de páginas y el tamaño deben ser mayor que cero.");
            }
            var filter = new Filter();
            if (IdOrden.HasValue)
            {
                filter.Add(nameof(VRptOrdenFabricacion.IdOrden), FilterCondition.Equals, IdOrden.Value);
            }
            if (!string.IsNullOrEmpty(nOrden))
            {
                filter.Add(nameof(VRptOrdenFabricacion.NOrden), FilterCondition.Equals, nOrden);
            }
            if (!string.IsNullOrEmpty(idArticulo))
            {
                filter.Add(nameof(VRptOrdenFabricacion.IDArticulo), FilterCondition.Equals, idArticulo);
            }
            //if (!string.IsNullOrEmpty(idCliente))
            //{
            //    filter.Add(nameof(VRptOrdenFabricacion.IDCliente), FilterCondition.Equals, idCliente);
            //}
            try
            {
                // Usa el repositorio para obtener datos paginados y filtrados
                var result = await _repository.GetFilteredAsync(filter, pageNumber, pageSize);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, new { message = "Error al obtener los datos", details = ex.Message });
            }
        }
    }
}
