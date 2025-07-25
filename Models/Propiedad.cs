using InfalibleRealEstate.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfalibleRealEstate.Models;

public class Propiedad
{
    [Key]
    public int PropiedadId { get; set; }

    [Required(ErrorMessage = "El título es obligatorio.")]
    [MaxLength(255)]
    public string Titulo { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18, 2)")] 
    public decimal Precio { get; set; }

    [Required]
    [MaxLength(10)]
    public string Moneda { get; set; } = string.Empty;

    [Required(ErrorMessage = "La ciudad es obligatoria.")]
    [MaxLength(100)]
    public string Ciudad { get; set; } = string.Empty;

    [MaxLength(100)]
    public string EstadoProvincia { get; set; } = string.Empty;

    [Required(ErrorMessage = "El tipo de transacción es obligatorio.")]
    [MaxLength(10)]
    public string TipoTransaccion { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría.")]
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    public string? AdministradorId { get; set; }

    public ApplicationUser? Administrador { get; set; }

    [Required]
    public DateTime FechaPublicacion { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime FechaActualizacion { get; set; }

    [Required]
    public int EstadoPropiedadId { get; set; }
    public EstadoPropiedad? EstadoPropiedad { get; set; }

    public PropiedadDetalle? Detalle { get; set; }
    public ICollection<ImagenPropiedad>? Imagenes { get; set; }
}
