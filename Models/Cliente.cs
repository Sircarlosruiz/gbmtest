using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace gbmtest.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(20)]
        public string Codigo { get; set;}
        public string Direccion { get; set; }

        // [ForeignKey("ClienteId")]
        public ICollection<Factura> Facturas { get; set; }
    }
}