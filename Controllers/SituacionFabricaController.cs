using Microsoft.AspNetCore.Mvc;
using Planificador_Fabrica.Enums;
using Planificador_Fabrica.Filters;
using Planificador_Fabrica.Services;

namespace Planificador_Fabrica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SituacionFabricaController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        public SituacionFabricaController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSituacionFabrica([FromQuery] string? columnName, [FromQuery] string? filterValue)
        {
            var filter = new Filter();

            if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(filterValue))
            {
                filter.Add(columnName, FilterCondition.Contains, filterValue);
            }

            var data = await _databaseService.QueryViewAsync("vCtlCISituacionFabrica", filter);
            return Ok(data);
        }
    }
}
