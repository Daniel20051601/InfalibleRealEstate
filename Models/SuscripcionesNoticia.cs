using InfalibleRealEstate.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfalibleRealEstate.Models;

public class SuscripcionesNoticia
{
    [Key]
    public int SuscripcionId { get; set; }

    public string? UsuarioId { get; set; }

    [Required]
    [StringLength(255)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public DateTime FechaSuscripcion { get; set; }

    [Required]
    [Column("activa")]
    public bool Activa { get; set; }

    public ApplicationUser? Usuario { get; set; }
}
