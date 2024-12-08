using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Planificador_Fabrica.Data;
using Planificador_Fabrica.Enums;
using Planificador_Fabrica.Filters;
using Planificador_Fabrica.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Planificador_Fabrica.Services
{
    public class DatabaseService
    {
        private readonly ApplicationDbContext _context;

        public DatabaseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DynamicModels>> QueryViewAsync(string viewName, Filter? filters = null )
        {
            // Ejecución de SQL raw para seleccionar datos de la vista
            var query = $"SELECT * FROM {viewName}";
            if (filters != null && filters.Filters.Any())
            {
                // Construir la cláusula WHERE dinámicamente
                var whereClauses = filters.Filters.Select(f =>
                {
                    var condition = f.FilterCondition switch
                    {
                        FilterCondition.Equals => "=",
                        FilterCondition.NotEquals => "<>",
                        FilterCondition.GreaterThan => ">",
                        FilterCondition.LessThan => "<",
                        FilterCondition.GreaterThanOrEqual => ">=",
                        FilterCondition.LessThanOrEqual => "<=",
                        FilterCondition.Contains => "LIKE",
                        _ => throw new InvalidOperationException($"Condición no soportada: {f.FilterCondition}")
                    };

                    // Para LIKE, usamos comodines
                    var value = f.FilterCondition == FilterCondition.Contains ? $"'%{f.Value}%'" : $"'{f.Value}'";
                    return $"{f.FieldName} {condition} {value}";
                });

                query += " WHERE " + string.Join(" AND ", whereClauses);
            }

            // Ejecutar la consulta y obtener un DataReader
            var connection = _context.Database.GetDbConnection();
            await using (connection)
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                command.CommandText = query;

                using var reader = await command.ExecuteReaderAsync();
                var results = new List<DynamicModels>();

                while (await reader.ReadAsync())
                {
                    var dynamicModel = new DynamicModels();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var columnName = reader.GetName(i);
                        var value = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);
                        dynamicModel.Fields[columnName] = value;
                    }
                    results.Add(dynamicModel);
                }

                return results;
            }
        }
    }
}
