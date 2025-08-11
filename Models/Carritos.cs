using InfalibleRealEstate.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfalibleRealEstate.Models;

public class Carritos
{
    [Key]
    public int CarritoId { get; set; }

    public string? UsuarioId { get; set; }

    [ForeignKey("UsuarioId")]
    public virtual ApplicationUser? Usuario { get; set; }

    public virtual ICollection<CarritoItems> CarritoItems { get; set; } = new List<CarritoItems>();
   
}
