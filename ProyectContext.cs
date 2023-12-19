using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gbmtest.Models;
using Microsoft.EntityFrameworkCore;

namespace gbmtest
{
    public class ProyectContext: DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<DetalleFactura> DetallesFactura { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<TasaDeCambio> TasasDeCambio { get; set; }

        public ProyectContext(DbContextOptions<ProyectContext> options) : base(options)
        {
            
        }

    }
}