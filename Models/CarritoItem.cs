using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfalibleRealEstate.Models
{
    public class CarritoItem
    {
        [Key]
        public int CarritoItemId { get; set; }

        public int CarritoId { get; set; }
        [ForeignKey("CarritoId")]
        public virtual Carrito Carrito { get; set; }

        public int PropiedadId { get; set; }
        [ForeignKey("PropiedadId")]
        public virtual Propiedad Propiedad { get; set; }
    }
}
