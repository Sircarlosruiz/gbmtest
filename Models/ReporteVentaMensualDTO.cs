using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gbmtest.Models
{
    public class ReporteVentaMensualDTO
    {
        public string CodigoCliente { get; set; }
        public string NombreCliente { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public decimal TotalDolares { get; set; }
        public decimal TotalCordobas { get; set; }
        public string Producto { get; set; }
        public string SKU { get; set; }
    }
}