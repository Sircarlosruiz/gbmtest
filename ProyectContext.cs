using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gbmtest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(client => {
                client.ToTable("Cliente");
                client.HasKey(c => c.Id);
                client.Property(c => c.Nombre).HasMaxLength(100).IsRequired();
                client.Property(c => c.Codigo).HasMaxLength(20).IsRequired();
                client.Property(c => c.Direccion).HasMaxLength(200);
            });

            modelBuilder.Entity<Factura>(factura => {
                factura.ToTable("Factura");
                factura.HasKey(f => f.Id);
                factura.Property(f => f.Fecha).IsRequired();
                factura.HasOne(f => f.Cliente).WithMany(c => c.Facturas).HasForeignKey(f => f.Id);
            });

            modelBuilder.Entity<TasaDeCambio>(tasa => {
                tasa.ToTable("TasaDeCambio");
                tasa.HasKey(t => t.Id);
                tasa.Property(t => t.Fecha).IsRequired();
                tasa.Property(t => t.Valor).IsRequired();
            });

            modelBuilder.Entity<Producto>(producto => {
                producto.ToTable("Producto");
                producto.HasKey(p => p.Id);
                producto.Property(p => p.Descripcion).HasMaxLength(150).IsRequired();
                producto.Property(p => p.PrecioCordobas).IsRequired();
                producto.Property(p => p.PrecioDolares).IsRequired();
                producto.Property(p => p.SKU).HasMaxLength(50);
            });

            modelBuilder.Entity<DetalleFactura>(detalle => {
                detalle.ToTable("DetalleFactura");
                detalle.HasKey(d => d.Id);
                detalle.Property(d => d.Cantidad).IsRequired();
                detalle.Property(d => d.PrecioUnitario).IsRequired();
                detalle.HasOne(d => d.Factura).WithMany(f => f.DetallesFactura).HasForeignKey(d => d.Id);
                detalle.HasOne(d => d.Producto).WithMany(p => p.DetallesFacturas).HasForeignKey(d => d.Id);
            });
        }
    }
}