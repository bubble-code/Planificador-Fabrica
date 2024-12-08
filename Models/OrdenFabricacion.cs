using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Planificador_Fabrica.Models
{

    [Table("tbOrdenFabricacion")]
    public class OrdenFabricacion : BaseEntity
    {
        [Key]
        public int IDOrden { get; set; }

        [Required]
        [StringLength(25)]
        public string NOrden { get; set; }

        [StringLength(10)]
        public string? IDContador { get; set; }

        [Required]
        [StringLength(50)]
        public string IDArticulo { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        [StringLength(10)]
        public string IDCentroGestion { get; set; }

        [Required]
        [Column(TypeName = "numeric(23, 8)")]
        public decimal QFabricar { get; set; }

        [Required]
        [Column(TypeName = "numeric(23, 8)")]
        public decimal QIniciada { get; set; }

        [Required]
        [Column(TypeName = "numeric(23, 8)")]
        public decimal QFabricada { get; set; }

        [Required]
        [Column(TypeName = "numeric(23, 8)")]
        public decimal QRechazada { get; set; }

        [Required]
        public int Estado { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        [Required]
        [StringLength(10)]
        public string IDAlmacen { get; set; }

        [StringLength(10)]
        public string? IDEstructura { get; set; }

        [StringLength(10)]
        public string? IDTipoEstructura { get; set; }

        [StringLength(10)]
        public string? IDRuta { get; set; }

        [StringLength(10)]
        public string? IDTipoRuta { get; set; }

        public DateTime? FechaInicioReal { get; set; }
        public DateTime? FechaFinReal { get; set; }
        public DateTime? FechaInicioProg { get; set; }
        public DateTime? FechaFinProg { get; set; }
        public DateTime? FechaCreacionAudi { get; set; }
        public DateTime? FechaModificacionAudi { get; set; }

        [StringLength(75)]
        public string? UsuarioAudi { get; set; }

        [Required]
        public int ParamMaterial { get; set; }

        [Required]
        public int ParamTerminado { get; set; }

        public int? Prioridad { get; set; }

        [Required]
        [Column(TypeName = "numeric(23, 8)")]
        public decimal CosteMatA { get; set; }

        [Required]
        [Column(TypeName = "numeric(23, 8)")]
        public decimal CosteOpeA { get; set; }

        [Required]
        [Column(TypeName = "numeric(23, 8)")]
        public decimal CosteExtA { get; set; }

        [Required]
        [Column(TypeName = "numeric(23, 8)")]
        public decimal QDudosa { get; set; }

        [StringLength(100)]
        public string? NivelPlano { get; set; }

        [StringLength(25)]
        public string? IDCentroCritico { get; set; }

        [Required]
        public bool Reproceso { get; set; }

        [Column(TypeName = "numeric(23, 8)")]
        public decimal? OFOrigen { get; set; }

        [Required]
        [Column(TypeName = "numeric(23, 8)")]
        public decimal CosteVarA { get; set; }

        public string? Texto { get; set; }

        [StringLength(25)]
        public string? Lote { get; set; }

        [StringLength(25)]
        public string? IDUbicacion { get; set; }

        public string? Observaciones { get; set; }
        public int? OFSolapada { get; set; }
        public int? IDLineaPedidoOrigen { get; set; }
        public int? IDOrdenEstructura { get; set; }

        [StringLength(1000)]
        public string? RamaExplosionOF { get; set; }

        [StringLength(75)]
        public string? UsuarioCreacionAudi { get; set; }
        public bool? ReprocesoFavram { get; set; }
        public bool? Fabrica { get; set; }
    }
}
