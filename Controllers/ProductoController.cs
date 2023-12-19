using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gbmtest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gbmtest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly ProyectContext _dbContext;

        public ProductoController(ProyectContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetProductos()
        {
            var productos = await _dbContext.Productos.Include(producto => producto.DetallesFacturas).ToListAsync();
            return Ok(productos);
        }

        [HttpGet("search")]
        public async Task<ActionResult> GetProductoBySkuOrDescripcion(string? sku, string? descripcion)
        {
            var query = _dbContext.Productos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(sku))
            {
                query = query.Where(p => p.SKU.Contains(sku));
            }

            if (!string.IsNullOrWhiteSpace(descripcion))
            {
                query = query.Where(p => p.Descripcion.Contains(descripcion));
            }

            // Incluir la tasa de cambio del día en la respuesta
            var tasaDeCambioDelDia = await _dbContext.TasasDeCambio.OrderByDescending(t => t.Fecha).FirstOrDefaultAsync();
            var productos = await query.ToListAsync();

            // Añadir la tasa de cambio a cada producto si es necesario
            var productosConTasa = productos.Select(p => new
            {
                Producto = p,
                TasaDeCambio = tasaDeCambioDelDia
            });

            return Ok(productosConTasa);
        }

        [HttpPost]
        public async Task<ActionResult> PostProducto([FromBody] Producto producto)
        {
            producto.Id = Guid.NewGuid();
            await _dbContext.Productos.AddAsync(producto);
            await _dbContext.SaveChangesAsync();
            return Ok(producto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProducto([FromServices] ProyectContext dbContext, [FromBody] Producto producto, Guid id)
        {
            var productoToUpdate = await dbContext.Productos.FindAsync(id);
            if (productoToUpdate == null)
            {
                return NotFound();
            }

            productoToUpdate.SKU = producto.SKU;
            productoToUpdate.Descripcion = producto.Descripcion;
            productoToUpdate.PrecioCordobas = producto.PrecioCordobas;
            productoToUpdate.PrecioDolares = producto.PrecioDolares;

            await dbContext.SaveChangesAsync();
            return Ok(productoToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProducto([FromServices] ProyectContext dbContext, Guid id)
        {
            var productoToDelete = await dbContext.Productos.FindAsync(id);
            if (productoToDelete == null)
            {
                return NotFound();
            }

            dbContext.Productos.Remove(productoToDelete);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}