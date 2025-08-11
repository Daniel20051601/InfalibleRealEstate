using System.ComponentModel.DataAnnotations;

namespace InfalibleRealEstate.Models;

public class EstadoPropiedades
{
    [Key]
    public int EstadoPropiedadId { get; set; }

    [Required]
    [StringLength(50)]
    public string NombreEstado { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    public ICollection<Propiedades>? Propiedades { get; set; }
}
