using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Planificador_Fabrica.Models
{
    public abstract class BaseEntity
    {
        // Clave primaria común para todas las tablas
        //[Key]
        //public virtual int Id { get; set; }

        // Opcional: Columnas adicionales dinámicas para flexibilidad
        [NotMapped] // EF ignorará esta propiedad
        public Dictionary<string, object> ExtraFields { get; set; } = new();

        public object? GetField(string columnName)
        {
            return ExtraFields.ContainsKey(columnName) ? ExtraFields[columnName] : null;
        }

        public void SetField(string columnName, object value)
        {
            ExtraFields[columnName] = value;
        }
    }
}
