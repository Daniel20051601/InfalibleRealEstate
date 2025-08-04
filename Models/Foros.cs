using InfalibleRealEstate.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfalibleRealEstate.Models;

public class Foros
{
    [Key]
    public int ForoId { get; set; }

    [Required(ErrorMessage = "El título es obligatorio.")]
    [MaxLength(255)]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción es obligatorio.")]
    [MaxLength(400)]
    public string Descripcion { get; set; } = string.Empty;

    [Required(ErrorMessage ="El link del foro es obligatorio.")]
    [Url(ErrorMessage = "El link del foro no es válido.")]
    [MaxLength(500)]
    public string LinkForo { get; set; } = string.Empty;

    public string? ImagenUrl { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public string? AdministradorId { get; set; }
    [ForeignKey("AdministradorId")]
    public virtual ApplicationUser? Administrador { get; set; }

}
