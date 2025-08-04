using InfalibleRealEstate.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfalibleRealEstate.Models;

public class Carrito
{
    [Key]
    public int CarritoId { get; set; }

    public string? UsuarioId { get; set; }

    [ForeignKey("UsuarioId")]
    public virtual ApplicationUser? Usuario { get; set; }

    public virtual ICollection<CarritoItem> CarritoItems { get; set; } = new List<CarritoItem>();
   
}
