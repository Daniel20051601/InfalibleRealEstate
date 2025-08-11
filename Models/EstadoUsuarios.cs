using InfalibleRealEstate.Data;
using System.ComponentModel.DataAnnotations;

namespace InfalibleRealEstate.Models;

public class EstadoUsuarios
{
    [Key]
    public int EstadoUsuarioId { get; set; } 
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; } = string.Empty; 

    public string? Descripcion { get; set; } = string.Empty;

    public ICollection<ApplicationUser>? Usuarios { get; set; } = new List<ApplicationUser>();

}
