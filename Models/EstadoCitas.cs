using System.ComponentModel.DataAnnotations;

namespace InfalibleRealEstate.Models;

public class EstadoCitas
{
    [Key]
    public int EstadoCitaId { get; set; }

    [Required(ErrorMessage = "El nombre del estado es obligatorio.")]
    [MaxLength(50)]
    public string Nombre { get; set; } = string.Empty;

    public virtual ICollection<Citas> Citas { get; set; } = new List<Citas>();
}
