using InfalibleRealEstate.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfalibleRealEstate.Models;

public class Propiedades
{
    [Key]
    public int PropiedadId { get; set; }

    [Required(ErrorMessage = "El título es obligatorio.")]
    [MaxLength(255)]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Precio { get; set; } = 0;

    [Required(ErrorMessage = "La moneda es obligatoria.")]
    [MaxLength(10)]
    public string Moneda { get; set; } = string.Empty;

    [Required(ErrorMessage = "La ciudad es obligatoria.")]
    [MaxLength(100)]
    public string Ciudad { get; set; } = string.Empty;

    [MaxLength(100)]
    public string EstadoProvincia { get; set; } = string.Empty;

    [Required(ErrorMessage = "El tipo de transacción es obligatorio.")]
    [MaxLength(30)]
    public string TipoTransaccion { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe seleccionar una categoría.")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría válida.")]
    public int CategoriaId { get; set; } = 0;
    public Categorias? Categoria { get; set; }

    public string? AdministradorId { get; set; }

    public ApplicationUser? Administrador { get; set; }

    [Required(ErrorMessage = "La fecha de publicación es obligatoria.")]
    public DateTime FechaPublicacion { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "La fecha de actualización es obligatoria.")]
    public DateTime FechaActualizacion { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un estado para la propiedad.")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un estado válido.")]
    public int EstadoPropiedadId { get; set; } = 0;
    public EstadoPropiedades? EstadoPropiedad { get; set; }

    public PropiedadDetalle? Detalle { get; set; }
    public ICollection<ImagenPropiedad>? Imagenes { get; set; }

    public virtual ICollection<CitasPropiedades> CitaPropiedades { get; set; } = new List<CitasPropiedades>();

    public static Propiedades CrearNueva(int categoriaId = 1)
    {
        return new Propiedades
        {
            Titulo = "",
            Detalle = new PropiedadDetalle
            {
                Descripcion = "",
                Habitaciones = 0,
                Banos = 0,
                Parqueo = 0,
                MetrosCuadrados = 0
            },
            Moneda = "Dolar",
            TipoTransaccion = "",
            Precio = 0,
            Ciudad = "",
            EstadoProvincia = "",
            CategoriaId = categoriaId,
            FechaPublicacion = DateTime.UtcNow,
            FechaActualizacion = DateTime.UtcNow,
            EstadoPropiedadId = 1
        };
    }
}
