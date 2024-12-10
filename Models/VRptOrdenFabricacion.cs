namespace Planificador_Fabrica.Models
{
    public class VRptOrdenFabricacion:BaseEntity
    {
        public int IdOrden { get; set; }
        public string NOrden { get; set; }
        public string IDArticulo { get; set; }
        //public string IDCliente { get; set; }
        public DateTime FechaReq { get; set; }
        //public IDictionary<string, object> DatosExtras { get; set; } = new Dictionary<string, object>();
    }
}
