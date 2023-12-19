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
    public class FacturaController : ControllerBase
    {
        private readonly ProyectContext _dbContext;
        private readonly ConversionMonedaService _conversionService;

        public FacturaController(ProyectContext dbContext, ConversionMonedaService conversionService)
        {
            _dbContext = dbContext;
            _conversionService = conversionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturaCreationDto>>> GetFacturas()
        {
            var facturas = await _dbContext.Facturas.Include(factura => factura.Cliente).ToListAsync();
            var facturasDto = facturas.Select(factura => new FacturaCreationDto
            {
                Id = factura.Id,
                ClienteId = factura.ClienteId,
                Fecha = factura.Fecha,
                Iva = factura.Iva,
                Cliente = factura.Cliente != null ? new ClienteCreationDto
                {
                    Id = factura.Cliente.Id,
                    Nombre = factura.Cliente.Nombre,
                    Codigo = factura.Cliente.Codigo,
                    Direccion = factura.Cliente.Direccion
                } : null
            }).ToList();

            return Ok(facturasDto);
        }



        public class FacturaCreationDto
        {
            public Guid Id { get; set; }
            public Guid ClienteId { get; set; }
            public DateTime Fecha { get; set; }
            public decimal Iva { get; set; }
            public ClienteCreationDto Cliente { get; set; }
        }

        public class ClienteCreationDto
        {
            public Guid Id { get; set; }
            public string Nombre { get; set; }
            public string Codigo { get; set; }
            public string Direccion { get; set; }
        }


        [HttpPost]
        public async Task<ActionResult<FacturaCreationDto>> CreateFactura([FromBody] Factura factura)
        {
            factura.Id = Guid.NewGuid();
            factura.Fecha = DateTime.Now;
            decimal totalIVA = 0;
            await _dbContext.Facturas.AddAsync(factura);
            foreach (var detalle in factura.DetallesFactura)
            {
                var producto = await _dbContext.Productos.FindAsync(detalle.ProductoId);
                if (producto != null)
                {
                    var ivaDetalle = _conversionService.CalcularIVA(detalle.PrecioUnitario);
                    totalIVA += ivaDetalle;
                }
            }
            factura.Iva = totalIVA;
            await _dbContext.Facturas.AddAsync(factura);
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