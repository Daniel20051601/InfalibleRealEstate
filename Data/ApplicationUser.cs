using InfalibleRealEstate.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InfalibleRealEstate.Data;

public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [PersonalData]
    [StringLength(100)]
    public string Apellido { get; set; } = string.Empty;

    public virtual ICollection<Propiedad> PropiedadesPublicadas { get; set; } = new List<Propiedad>();
    public virtual ICollection<SuscripcionNoticia> Suscripciones { get; set; } = new List<SuscripcionNoticia>();
}
