using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace gbmtest.Models
{
    public class Factura
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ClienteId")]
        public int ClienteId { get; set; }
        [Required]
        public DateTime Fecha { get; set; }

        public Cliente Cliente { get; set; }
        public ICollection<DetalleFactura> Detalles{ get; set; }
    }
}