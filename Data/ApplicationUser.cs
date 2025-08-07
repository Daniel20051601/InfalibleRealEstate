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

    public virtual Carrito? Carrito { get; set; }

    public virtual ICollection<Propiedad> PropiedadesPublicadas { get; set; } = new List<Propiedad>();
    public virtual ICollection<SuscripcionNoticia> Suscripciones { get; set; } = new List<SuscripcionNoticia>();
    public virtual ICollection<SolicitudVenta> SolicitudesVenta { get; set; } = new List<SolicitudVenta>();
    public virtual ICollection<SolicitudUnirse> SolicitudesUnirse { get; set; } = new List<SolicitudUnirse>();
    public virtual ICollection<Foros> Foros { get; set; } = new List<Foros>();
    public virtual ICollection<Citas> Citas { get; set; } = new List<Citas>();
}
