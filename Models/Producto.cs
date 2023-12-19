using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [Required]
        public string SKU { get; set; }

        [JsonIgnore]
        public ICollection<DetalleFactura> DetallesFacturas { get; set; }

    }

    public class ProductoDto
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioCordobas { get; set; }
        public decimal PrecioDolares { get; set; }
        public string SKU { get; set; }
    }

}