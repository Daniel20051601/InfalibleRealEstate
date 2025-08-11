using System.ComponentModel.DataAnnotations;

namespace InfalibleRealEstate.Models;

public class ImagenPropiedad
{
    [Key]
    public int ImagenId { get; set; }

    [Required]
    public int PropiedadId { get; set; }

    [Required]
    [StringLength(255)]
    public string UrlImagen { get; set; } = string.Empty;

    [Required]
    public int Orden { get; set; }

    public Propiedades? Propiedad { get; set; }
}
