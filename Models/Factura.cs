using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace gbmtest.Models
{
    public class Factura
    {
        public Factura()
        {
            DetallesFactura = new HashSet<DetalleFactura>();
            Cliente = null;
        }
        // [Key]
        public Guid Id { get; set; }
        [ForeignKey("ClienteId")]
        public Guid ClienteId { get; set; }
        [Required]
        public DateTime Fecha { get; set; }

        [JsonIgnore]
        public Cliente? Cliente { get; set; }

        // [NotMapped]
        public decimal Iva { get; set; }

        [JsonIgnore]
        public ICollection<DetalleFactura> DetallesFactura{ get; set; }
    }

    public class FacturaDto
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Iva { get; set; }
    }
}