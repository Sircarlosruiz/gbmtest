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
    public class TasaDeCambioController : ControllerBase
    {
        private readonly ProyectContext _dbContext;

        public TasaDeCambioController(ProyectContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TasaDeCambioDto>>> GetTasasDeCambio()
        {
            var tasasDeCambio = await _dbContext.TasasDeCambio.ToListAsync();
            var tasasDeCambioDto = tasasDeCambio.Select(tasaDeCambio => new TasaDeCambioDto
            {
                Id = tasaDeCambio.Id,
                Valor = tasaDeCambio.Valor,
                Fecha = tasaDeCambio.Fecha
            }).ToList();
            return Ok(tasasDeCambioDto);
        }

        public class TasaDeCambioCreationDto
        {
            public Guid Id { get; set; }
            public decimal Valor { get; set; }
            public DateTime Fecha { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult<TasaDeCambioCreationDto>> CreateTasaDeCambio([FromBody] TasaDeCambio tasaDeCambio)
        {
            tasaDeCambio.Id = Guid.NewGuid();
            tasaDeCambio.Fecha = DateTime.Now;
            await _dbContext.TasasDeCambio.AddAsync(tasaDeCambio);
            await _dbContext.SaveChangesAsync();
            var tasaDeCambioDto = new TasaDeCambioCreationDto
            {
                Id = tasaDeCambio.Id,
                Valor = tasaDeCambio.Valor,
                Fecha = tasaDeCambio.Fecha
            };
            return Ok(tasaDeCambioDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TasaDeCambioCreationDto>> UpdateTasaDeCambio([FromServices] ProyectContext dbContext, [FromBody] TasaDeCambio tasaDeCambio, Guid id)
        {
            var tasaDeCambioToUpdate = await dbContext.TasasDeCambio.FindAsync(id);
            if (tasaDeCambioToUpdate == null)
            {
                return NotFound();
            }

            tasaDeCambioToUpdate.Valor = tasaDeCambio.Valor;
            tasaDeCambioToUpdate.Fecha = tasaDeCambio.Fecha;

            await dbContext.SaveChangesAsync();
            var tasaDeCambioDto = new TasaDeCambioCreationDto
            {
                Id = tasaDeCambioToUpdate.Id,
                Valor = tasaDeCambioToUpdate.Valor,
                Fecha = tasaDeCambioToUpdate.Fecha
            };
            return Ok(tasaDeCambioDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTasaDeCambio([FromServices] ProyectContext dbContext, Guid id)
        {
            var tasaDeCambioToDelete = await dbContext.TasasDeCambio.FindAsync(id);
            if (tasaDeCambioToDelete == null)
            {
                return NotFound();
            }

            dbContext.TasasDeCambio.Remove(tasaDeCambioToDelete);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}