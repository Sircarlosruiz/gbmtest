using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace gbmtest.Models
{
    public class Cliente
    {
        public Cliente()
        {
            Facturas = new HashSet<Factura>();
        }
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(20)]
        public string Codigo { get; set; }

        [Required]
        public string Direccion { get; set; }


        [JsonIgnore]
        public ICollection<Factura> Facturas { get; set; }
    }

    public class ClienteDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Direccion { get; set; }
    }
}