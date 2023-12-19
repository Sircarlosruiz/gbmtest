using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace gbmtest.Models
{
    public class DetalleFactura
    {
        // [Key]
        public Guid Id { get; set; }
        [ForeignKey("FacturaId")]
        public Guid FacturaId { get; set; }
        [ForeignKey("ProductoId")]
        public Guid ProductoId { get; set; }
        [Required]
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public Factura Factura { get; set; }
        public Producto Producto { get; set; }
    }
}