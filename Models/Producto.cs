using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gbmtest.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Descripcion { get; set; }
        public decimal PrecioCordobas { get; set; }
        public decimal PrecioDolares { get; set; }
        public string SKU { get; set; }

        public ICollection<DetalleFactura> DetallesFacturas { get; set; }
    }
}