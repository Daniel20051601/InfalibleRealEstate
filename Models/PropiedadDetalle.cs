using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfalibleRealEstate.Models
{
    public class PropiedadDetalle
    {
        [Key]
        public int PropiedadId { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El número de habitaciones es obligatorio.")]
        [Range(0, 50, ErrorMessage = "Las habitaciones no pueden ser negativas.")]
        public int? Habitaciones { get; set; } = 0;

        [Required(ErrorMessage = "El número de baños es obligatorio.")]
        [Range(0.00, 50.00, ErrorMessage = "Los baños no pueden ser negativos.")]
        public decimal? Banos { get; set; } = 0;

        [Required(ErrorMessage = "El número de parqueos es obligatorio.")]
        [Range(0, 50, ErrorMessage = "Los parqueos no pueden ser negativos.")]
        public int? Parqueo { get; set; } = 0;

        [Required(ErrorMessage = "Los metros cuadrados son obligatorios.")]
        [Range(0, 200000, ErrorMessage = "Los metros cuadrados no pueden ser negativos.")]
        public decimal? MetrosCuadrados { get; set; } = 0;

        [ForeignKey("PropiedadId")]
        public Propiedades? Propiedad { get; set; }
    }
}
