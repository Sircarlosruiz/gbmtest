using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace gbmtest.Models
{
    public class Producto
    {
        public Producto()
        {
            DetallesFacturas = new HashSet<DetalleFactura>();
        }
        // [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Descripcion { get; set; }
        public decimal PrecioCordobas { get; set; }
        public decimal PrecioDolares { get; set; }
        [Required]
        public string SKU { get; set; }

        [JsonIgnore]
        public ICollection<DetalleFactura> DetallesFacturas { get; set; }
    }
}