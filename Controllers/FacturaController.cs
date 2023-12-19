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
    public class FacturaController : ControllerBase
    {
        private readonly ProyectContext _dbContext;

        public FacturaController(ProyectContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetFacturas()
        {
            var facturas = await _dbContext.Facturas.Include(factura => factura.Cliente).ToListAsync();
            return Ok(facturas);
        }

        [HttpPost]
        public async Task<ActionResult> CreateFactura([FromBody] Factura factura)
        {
            factura.Id = Guid.NewGuid();
            factura.Fecha = DateTime.Now;
            await _dbContext.Facturas.AddAsync(factura);
            await _dbContext.SaveChangesAsync();
            return Ok(factura);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFactura([FromServices] ProyectContext dbContext, [FromBody] Factura factura, Guid id)
        {
            var facturaToUpdate = await dbContext.Facturas.FindAsync(id);
            if (facturaToUpdate == null)
            {
                return NotFound();
            }

            facturaToUpdate.ClienteId = factura.ClienteId;
            facturaToUpdate.Fecha = factura.Fecha;

            await dbContext.SaveChangesAsync();
            return Ok(facturaToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFactura([FromServices] ProyectContext dbContext, Guid id)
        {
            var facturaToDelete = await dbContext.Facturas.FindAsync(id);
            if (facturaToDelete == null)
            {
                return NotFound();
            }

            dbContext.Facturas.Remove(facturaToDelete);
            await dbContext.SaveChangesAsync();
            return Ok(facturaToDelete);
        }
    }
}