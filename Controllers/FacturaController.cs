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
        public async Task<ActionResult<IEnumerable<FacturaDto>>> GetFacturas()
        {
            var facturas = await _dbContext.Facturas.Include(factura => factura.Cliente).ToListAsync();
            var facturasDto = facturas.Select(factura => new FacturaDto
            {
                Id = factura.Id,
                ClienteId = factura.ClienteId,
                Fecha = factura.Fecha,
                Iva = factura.Iva
            }).ToList();
            return Ok(facturas);
        }

        public class FacturaCreationDto
        {
            public Guid Id { get; set; }
            public Guid ClienteId { get; set; }
            public DateTime Fecha { get; set; }
            public decimal Iva { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult<FacturaCreationDto>> CreateFactura([FromBody] Factura factura)
        {
            factura.Id = Guid.NewGuid();
            factura.Fecha = DateTime.Now;
            const decimal tasaIva = 0.15m;
            await _dbContext.Facturas.AddAsync(factura);
            decimal totalPrecio = 0;
            foreach(var detalle in factura.DetallesFactura)
            {
                totalPrecio += detalle.Cantidad * detalle.PrecioUnitario;
            }

            factura.Iva = totalPrecio * tasaIva;
            await _dbContext.SaveChangesAsync();
            var facturaDto = new FacturaCreationDto
            {
                Id = factura.Id,
                ClienteId = factura.ClienteId,
                Fecha = factura.Fecha,
                Iva = factura.Iva
            };
            return Ok(facturaDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FacturaCreationDto>> UpdateFactura([FromServices] ProyectContext dbContext, [FromBody] Factura factura, Guid id)
        {
            var facturaToUpdate = await dbContext.Facturas.FindAsync(id);
            if (facturaToUpdate == null)
            {
                return NotFound();
            }

            facturaToUpdate.ClienteId = factura.ClienteId;
            facturaToUpdate.Fecha = factura.Fecha;

            await dbContext.SaveChangesAsync();
            var facturaDto = new FacturaCreationDto
            {
                Id = facturaToUpdate.Id,
                ClienteId = facturaToUpdate.ClienteId,
                Fecha = facturaToUpdate.Fecha,
                Iva = facturaToUpdate.Iva
            };
            return Ok(facturaDto);
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