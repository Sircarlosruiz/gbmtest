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
        public async Task<ActionResult> GetDetalleFacturas()
        {
            var detalleFacturas = await _dbContext.DetallesFactura.Include(df => df.Factura).ToListAsync();
            return Ok(detalleFacturas);
        }

        [HttpPost]
        public async Task<ActionResult> CreateDetalleFactura([FromBody] gbmtest.Models.DetalleFactura detalleFactura)
        {
            detalleFactura.Id = Guid.NewGuid();
            await _dbContext.DetallesFactura.AddAsync(detalleFactura);
            await _dbContext.SaveChangesAsync();
            return Ok(detalleFactura);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDetalleFactura([FromServices] ProyectContext dbContext, [FromBody] gbmtest.Models.DetalleFactura detalleFactura, Guid id)
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
            return Ok(detalleFacturaToUpdate);
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