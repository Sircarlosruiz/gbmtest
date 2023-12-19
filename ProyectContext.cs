using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gbmtest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySql.Data.MySqlClient.Memcached;

namespace gbmtest
{
    public class ProyectContext : DbContext
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
            List<Cliente> clientes = new List<Cliente>();
            clientes.Add(new Cliente() { Id = Guid.Parse("3d28b6ea-32e8-497b-9f52-07f211ac20ca"), Nombre = "Cliente 1", Codigo = "C1", Direccion = "Managua" });
            clientes.Add(new Cliente() { Id = Guid.Parse("3d28b6ea-32e8-497b-9f52-07f211ac20cb"), Nombre = "Cliente 2", Codigo = "B1", Direccion = "Granaga" });

            List<Factura> facturas = new List<Factura>();
            facturas.Add(new Factura() { Id = Guid.Parse("3d28b6ea-32e8-497b-9f52-07f211ac20cc"), Fecha = DateTime.Now, ClienteId = Guid.Parse("3d28b6ea-32e8-497b-9f52-07f211ac20ca") });
            facturas.Add(new Factura() { Id = Guid.Parse("3d28b6ea-32e8-497b-9f52-07f211ac20cb"), Fecha = DateTime.Now, ClienteId = Guid.Parse("3d28b6ea-32e8-497b-9f52-07f211ac20cb") });

            List<Producto> productos = new List<Producto>();
            productos.Add(new Producto() { Id = Guid.Parse("dc131c19-84af-4627-902a-08e3d5c98791"), Descripcion = "Producto 1", PrecioCordobas = 100, PrecioDolares = 10, SKU = "P1" });
            productos.Add(new Producto() { Id = Guid.Parse("dc131c19-84af-4627-902a-08e3d5c98792"), Descripcion = "Producto 2", PrecioCordobas = 200, PrecioDolares = 20, SKU = "P2" });

            List<TasaDeCambio> tasasDeCambio = new List<TasaDeCambio>();
            tasasDeCambio.Add(new TasaDeCambio() { Id = Guid.Parse("f789837c-ddd3-4451-be64-57996acf89ce"), Fecha = DateTime.Now, Valor = 36 });

            List<DetalleFactura> detallesFactura = new List<DetalleFactura>();
            detallesFactura.Add(new DetalleFactura() { Id = Guid.Parse("3d28b6ea-32e8-497b-9f52-07f211ac20cd"), FacturaId = Guid.Parse("3d28b6ea-32e8-497b-9f52-07f211ac20cc"), ProductoId = Guid.Parse("dc131c19-84af-4627-902a-08e3d5c98791"), Cantidad = 1, PrecioUnitario = 100 });
            detallesFactura.Add(new DetalleFactura() { Id = Guid.Parse("3d28b6ea-32e8-497b-9f52-07f211ac20ce"), FacturaId = Guid.Parse("3d28b6ea-32e8-497b-9f52-07f211ac20cc"), ProductoId = Guid.Parse("dc131c19-84af-4627-902a-08e3d5c98792"), Cantidad = 2, PrecioUnitario = 200 });

            modelBuilder.Entity<Cliente>(client =>
            {
                client.ToTable("Cliente");
                client.HasKey(c => c.Id);
                client.Property(c => c.Nombre).HasMaxLength(100).IsRequired();
                client.Property(c => c.Codigo).HasMaxLength(20).IsRequired();
                client.Property(c => c.Direccion).HasMaxLength(200);
                client.HasData(clientes);
            });

            modelBuilder.Entity<Factura>(factura =>
            {
                factura.ToTable("Factura");
                factura.HasKey(f => f.Id);
                factura.Property(f => f.Fecha).IsRequired();
                factura.HasOne(f => f.Cliente).WithMany(c => c.Facturas).HasForeignKey(f => f.ClienteId).OnDelete(DeleteBehavior.Cascade);
                factura.HasData(facturas);
            });

            modelBuilder.Entity<TasaDeCambio>(tasa =>
            {
                tasa.ToTable("TasaDeCambio");
                tasa.HasKey(t => t.Id);
                tasa.Property(t => t.Fecha).IsRequired();
                tasa.Property(t => t.Valor).IsRequired();
                tasa.HasData(tasasDeCambio);
            });

            modelBuilder.Entity<Producto>(producto =>
            {
                producto.ToTable("Producto");
                producto.HasKey(p => p.Id);
                producto.Property(p => p.Descripcion).HasMaxLength(150).IsRequired();
                producto.Property(p => p.PrecioCordobas).IsRequired();
                producto.Property(p => p.PrecioDolares).IsRequired();
                producto.Property(p => p.SKU).HasMaxLength(50);
                producto.HasData(productos);
            });

            modelBuilder.Entity<DetalleFactura>(detalle =>
            {
                detalle.ToTable("DetalleFactura");
                detalle.HasKey(d => d.Id);
                detalle.Property(d => d.Cantidad).IsRequired();
                detalle.Property(d => d.PrecioUnitario).IsRequired();
                detalle.HasOne(d => d.Factura).WithMany(f => f.DetallesFactura).HasForeignKey(d => d.FacturaId).OnDelete(DeleteBehavior.Cascade);
                detalle.HasOne(d => d.Producto).WithMany(p => p.DetallesFacturas).HasForeignKey(d => d.ProductoId).OnDelete(DeleteBehavior.Cascade);
                detalle.HasData(detallesFactura);
            });
        }
    }
}