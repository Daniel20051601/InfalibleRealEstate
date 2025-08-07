using InfalibleRealEstate.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfalibleRealEstate.Models;

public class Citas
{

    [Key]
    public int CitaId { get; set; }

    [Required]
    public string UsuarioId { get; set; } = string.Empty;

    [ForeignKey("UsuarioId")]
    public virtual ApplicationUser? Usuario { get; set; }

    [Required(ErrorMessage = "La fecha y hora de la cita son obligatorias.")]
    public DateTime FechaHora { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Costo { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un estado para la cita.")]
    public int EstadoCitaId { get; set; }

    [ForeignKey("EstadoCitaId")]
    public virtual EstadoCitas? EstadoCita { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    public virtual ICollection<CitasPropiedades> CitaPropiedades { get; set; } = new List<CitasPropiedades>();

}
