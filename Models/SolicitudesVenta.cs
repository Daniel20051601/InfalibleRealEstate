using InfalibleRealEstate.Data;
using System.ComponentModel.DataAnnotations;

namespace InfalibleRealEstate.Models;

public class SolicitudesVenta
{
    [Key]
    public int SolicitudVentaId { get; set; }

    [Required(ErrorMessage = "El nombre es requerido")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es requerido")]
    [StringLength(100, ErrorMessage = "El apellido no puede exceder los 100 caracteres")]
    public string Apellido { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo electrónico es requerido")]
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
    [StringLength(100, ErrorMessage = "El correo electrónico no puede exceder los 100 caracteres")]
    public string CorreoElectronico { get; set; } = string.Empty;

    [Required(ErrorMessage = "El número de teléfono es requerido")]
    [Phone(ErrorMessage = "El número de teléfono no es válido")]
    [StringLength(20, ErrorMessage = "El número de teléfono no puede exceder los 20 caracteres")]
    public string Telefono { get; set; } = string.Empty;

    [Required(ErrorMessage = "La categoria es Requerida")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría válida.")]
    public int CategoriaId { get; set; } 

    public virtual Categorias? Categoria { get; set; }

    [Required(ErrorMessage = "La descripción es requerida")]
    [StringLength(1000, ErrorMessage = "La descripción no puede exceder los 1000 caracteres")]
    public string Descripcion { get; set; } = string.Empty;

    public string? UsuarioId { get; set; }
    public virtual ApplicationUser? Usuario { get; set; }

    [Required]
    public DateTime FechaSolicitud { get; set; } = DateTime.Now;

    [Required]
    [StringLength(50)]
    public string Estado { get; set; } = "Pendiente";


}
