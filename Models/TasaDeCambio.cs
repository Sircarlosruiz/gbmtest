using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gbmtest.Models
{
    public class TasaDeCambio
    {
        // [Key]
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Valor { get; set; }
    }

    public class TasaDeCambioDto
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Valor { get; set; }
    }
}