using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gbmtest.Models;
using gbmtest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gbmtest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly ProyectContext _dbContext;
        private readonly ConversionMonedaService _conversionService;

        public ProductoController(ProyectContext dbContext, ConversionMonedaService conversionService)
        {
            _dbContext = dbContext;
            _conversionService = conversionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetProductos()
        {
            var productos = await _dbContext.Productos.Include(producto => producto.DetallesFacturas).ToListAsync();
            var productosDto = productos.Select(producto => new ProductoDto
            {
                Id = producto.Id,
                SKU = producto.SKU,
                Descripcion = producto.Descripcion,
                PrecioCordobas = producto.PrecioCordobas,
                PrecioDolares = _conversionService.ConvertirCordobasADolares(producto.PrecioCordobas)
            }).ToList();
            return Ok(productosDto);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetProductoBySkuOrDescripcion(string? sku, string? descripcion)
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

            var tasaDeCambioDelDia = await _dbContext.TasasDeCambio.OrderByDescending(t => t.Fecha).FirstOrDefaultAsync();
            var productos = await query.ToListAsync();
            var productosDto = productos.Select(producto => new ProductoDto
            {
                Id = producto.Id,
                SKU = producto.SKU,
                Descripcion = producto.Descripcion,
                PrecioCordobas = producto.PrecioCordobas,
                PrecioDolares = _conversionService.ConvertirCordobasADolares(producto.PrecioCordobas)
            }).ToList();
            return Ok(productosDto);
        }

        public class ProductoCreationDto
        {
            public Guid Id { get; set; }
            public string SKU { get; set; }
            public string Descripcion { get; set; }
            public decimal PrecioCordobas { get; set; }
            public decimal PrecioDolares { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult<ProductoCreationDto>> PostProducto([FromBody] Producto producto)
        {
            producto.Id = Guid.NewGuid();
            await _dbContext.Productos.AddAsync(producto);
            await _dbContext.SaveChangesAsync();
            var productoDto = new ProductoCreationDto
            {
                Id = producto.Id,
                SKU = producto.SKU,
                Descripcion = producto.Descripcion,
                PrecioCordobas = producto.PrecioCordobas,
                PrecioDolares = _conversionService.ConvertirCordobasADolares(producto.PrecioCordobas)
            };
            return Ok(productoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductoCreationDto>> UpdateProducto([FromServices] ProyectContext dbContext, [FromBody] Producto producto, Guid id)
        {
            var productoToUpdate = await dbContext.Productos.FindAsync(id);
            if (productoToUpdate == null)
            {
                return NotFound();
            }

            productoToUpdate.SKU = producto.SKU;
            productoToUpdate.Descripcion = producto.Descripcion;
            productoToUpdate.PrecioCordobas = producto.PrecioCordobas;

            await dbContext.SaveChangesAsync();
            var productoDto = new ProductoCreationDto
            {
                Id = productoToUpdate.Id,
                SKU = productoToUpdate.SKU,
                Descripcion = productoToUpdate.Descripcion,
                PrecioCordobas = productoToUpdate.PrecioCordobas,
                PrecioDolares = _conversionService.ConvertirCordobasADolares(productoToUpdate.PrecioCordobas)
            };
            return Ok(productoDto);
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