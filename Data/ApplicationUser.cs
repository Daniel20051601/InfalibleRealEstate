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
    public virtual EstadoUsuarios EstadoUsuario { get; set; }

    public virtual Carritos? Carrito { get; set; }

    public virtual ICollection<Propiedades> PropiedadesPublicadas { get; set; } = new List<Propiedades>();
    public virtual ICollection<SuscripcionesNoticia> Suscripciones { get; set; } = new List<SuscripcionesNoticia>();
    public virtual ICollection<SolicitudesVenta> SolicitudesVenta { get; set; } = new List<SolicitudesVenta>();
    public virtual ICollection<SolicitudesUnirse> SolicitudesUnirse { get; set; } = new List<SolicitudesUnirse>();
    public virtual ICollection<Foros> Foros { get; set; } = new List<Foros>();
    public virtual ICollection<Citas> Citas { get; set; } = new List<Citas>();
}
