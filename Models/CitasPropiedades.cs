using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfalibleRealEstate.Models;

public class CitasPropiedades
{
    [Key]
    public int CitaPropiedadId { get; set; }

    [Required]
    public int CitaId { get; set; }

    [ForeignKey("CitaId")]
    public virtual Citas? Cita { get; set; }

    [Required]
    public int PropiedadId { get; set; }

    [ForeignKey("PropiedadId")]
    public virtual Propiedad? Propiedad { get; set; }
}
