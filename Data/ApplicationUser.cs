using InfalibleRealEstate.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InfalibleRealEstate.Data;

public class ApplicationUser : IdentityUser
{
    [Required(ErrorMessage = "El nombre es requerido")]
    [PersonalData]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es requerido")]
    [PersonalData]
    [StringLength(100)]
    public string Apellido { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe agregar un estado para el usuario")]
    public int EstadoUsuarioId { get; set; } = 1; 
    public virtual EstadoUsuario EstadoUsuario { get; set; }

    public virtual ICollection<Propiedad> PropiedadesPublicadas { get; set; } = new List<Propiedad>();
    public virtual ICollection<SuscripcionNoticia> Suscripciones { get; set; } = new List<SuscripcionNoticia>();
}
