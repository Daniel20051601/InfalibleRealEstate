using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfalibleRealEstate.Models
{
    public class PropiedadDetalle
    {
        [Key]
        public int PropiedadId { get; set; }

        [Required]
        public string Descripcion { get; set; } = string.Empty;

        public int? Habitaciones { get; set; }

        public decimal? Banos { get; set; }

        public int? Parqueo { get; set; }

        public decimal? MetrosCuadrados { get; set; }

        [ForeignKey("PropiedadId")]
        public Propiedad? Propiedad { get; set; }
    }
}
