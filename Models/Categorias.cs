using System.ComponentModel.DataAnnotations;

namespace InfalibleRealEstate.Models;

public class Categorias
{
    [Key]
    public int CategoriaId { get; set; }

    [Required]
    [StringLength(100)]
    public string NombreCategoria { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    public virtual ICollection<Propiedades>? Propiedades { get; set; }
    public virtual ICollection<SolicitudesVenta> SolicitudesVenta { get; set; } = new List<SolicitudesVenta>();
}
