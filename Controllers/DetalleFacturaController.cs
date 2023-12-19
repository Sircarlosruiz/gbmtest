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
    public class DetalleFactura : ControllerBase
    {
        private readonly ProyectContext _dbContext;

        public DetalleFactura(ProyectContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleFacturaDto>>> GetDetalleFacturas()
        {
            var detalleFacturas = await _dbContext.DetallesFactura.Include(df => df.Factura).Include(df => df.Producto).ToListAsync();
            
            var detalleFacturasDto = detalleFacturas.Select(detalleFactura => new DetalleFacturaDto
            {
                Id = detalleFactura.Id,
                FacturaId = detalleFactura.FacturaId,
                ProductoId = detalleFactura.ProductoId,
                Cantidad = detalleFactura.Cantidad,
                PrecioUnitario = detalleFactura.PrecioUnitario
            }).ToList();
            return Ok(detalleFacturas);
        }

        public class DetalleFacturaDto
        {
            public Guid Id { get; set; }
            public Guid FacturaId { get; set; }
            public Guid ProductoId { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult<DetalleFacturaDto>> CreateDetalleFactura([FromBody] gbmtest.Models.DetalleFactura detalleFactura)
        {
            detalleFactura.Id = Guid.NewGuid();
            await _dbContext.DetallesFactura.AddAsync(detalleFactura);
            await _dbContext.SaveChangesAsync();
            var detalleFacturaDto = new DetalleFacturaDto
            {
                Id = detalleFactura.Id,
                FacturaId = detalleFactura.FacturaId,
                ProductoId = detalleFactura.ProductoId,
                Cantidad = detalleFactura.Cantidad,
                PrecioUnitario = detalleFactura.PrecioUnitario
            };
            return Ok(detalleFacturaDto);
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<DetalleFacturaDto>> UpdateDetalleFactura([FromServices] ProyectContext dbContext, [FromBody] gbmtest.Models.DetalleFactura detalleFactura, Guid id)
        {
            var detalleFacturaToUpdate = await dbContext.DetallesFactura.FindAsync(id);
            if (detalleFacturaToUpdate == null)
            {
                return NotFound();
            }

            detalleFacturaToUpdate.FacturaId = detalleFactura.FacturaId;
            detalleFacturaToUpdate.ProductoId = detalleFactura.ProductoId;
            detalleFacturaToUpdate.Cantidad = detalleFactura.Cantidad;
            detalleFacturaToUpdate.PrecioUnitario = detalleFactura.PrecioUnitario;

            await dbContext.SaveChangesAsync();
            var detalleFacturaDto = new DetalleFacturaDto
            {
                Id = detalleFacturaToUpdate.Id,
                FacturaId = detalleFacturaToUpdate.FacturaId,
                ProductoId = detalleFacturaToUpdate.ProductoId,
                Cantidad = detalleFacturaToUpdate.Cantidad,
                PrecioUnitario = detalleFacturaToUpdate.PrecioUnitario
            };
            return Ok(detalleFacturaDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDetalleFactura([FromServices] ProyectContext dbContext, Guid id)
        {
            var detalleFacturaToDelete = await dbContext.DetallesFactura.FindAsync(id);
            if (detalleFacturaToDelete == null)
            {
                return NotFound();
            }

            dbContext.DetallesFactura.Remove(detalleFacturaToDelete);
            await dbContext.SaveChangesAsync();
            return Ok(detalleFacturaToDelete);
        }
    }
}