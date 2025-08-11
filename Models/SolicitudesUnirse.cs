using InfalibleRealEstate.Data;
using System.ComponentModel.DataAnnotations;

namespace InfalibleRealEstate.Models;

public class SolicitudesUnirse
{
    [Key]
    public int SolicitudUnirseId { get; set; }

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

    [Required(ErrorMessage = "El nivel de estudios es requerido")]
    [StringLength(100, ErrorMessage = "El nivel de estudios no puede exceder los 100 caracteres")]
    public string NivelEstudios { get; set; } = string.Empty;

    [Required(ErrorMessage = "La experiencia es requerida")]
    [StringLength(300, ErrorMessage = "La experiencia no puede exceder los 300 caracteres")]
    public string Experiencia { get; set; } = string.Empty;

    [Required(ErrorMessage = "La profesión es requerida")]
    [StringLength(100, ErrorMessage = "La profesión no puede exceder los 100 caracteres")]
    public string Profesion { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este campo es requerido")]
    [StringLength(50, ErrorMessage = "Este campo no puede exceder los 50 caracteres")]
    public string TrabajaActualmente { get; set; } = string.Empty;

    [Required(ErrorMessage = "La provincia es requerida")]
    [StringLength(100, ErrorMessage = "La provincia no puede exceder los 100 caracteres")]
    public string Provincia { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
    public DateTime FechaNacimiento { get; set; }

    [Required(ErrorMessage = "La descripción es requerida")]
    [StringLength(1000, ErrorMessage = "La descripción no puede exceder los 1000 caracteres")]
    public string PorqueNosotros { get; set; } = string.Empty;

    public DateTime FechaSolicitud { get; set; } = DateTime.Now;

    public string? UsuarioId { get; set; }
    public virtual ApplicationUser? Usuario { get; set; }

    [Required]
    [StringLength(50)]
    public string Estado { get; set; } = "Pendiente";
}