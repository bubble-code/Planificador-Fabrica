using System.ComponentModel.DataAnnotations;

namespace Planificador_Fabrica.Repositories
{
    public interface IEntity
    {
        object GetPrimaryKeyValue();
    }
}