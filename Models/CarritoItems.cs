using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfalibleRealEstate.Models
{
    public class CarritoItems
    {
        [Key]
        public int CarritoItemId { get; set; }

        public int CarritoId { get; set; }
        [ForeignKey("CarritoId")]
        public virtual Carritos Carrito { get; set; }

        public int PropiedadId { get; set; }
        [ForeignKey("PropiedadId")]
        public virtual Propiedades Propiedad { get; set; }
    }
}
