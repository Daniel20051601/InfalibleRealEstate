using System.ComponentModel.DataAnnotations;

namespace InfalibleRealEstate.Models;

public class Categoria
{
    [Key]
    public int CategoriaId { get; set; }

    [Required]
    [StringLength(100)]
    public string NombreCategoria { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    public ICollection<Propiedad>? Propiedades { get; set; }
}
