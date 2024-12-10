using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Planificador_Fabrica.Models
{
    public abstract class BaseEntity
    {
        // Opcional: Columnas adicionales dinámicas para flexibilidad
        [NotMapped] // EF ignorará esta propiedad
        public virtual Dictionary<string, object> ExtraFields { get; set; } = new();

        public virtual object? GetField(string columnName)
        {
            return ExtraFields.ContainsKey(columnName) ? ExtraFields[columnName] : null;
        }

        public virtual void SetField(string columnName, object value)
        {
            ExtraFields[columnName] = value;
        }
    }
}
