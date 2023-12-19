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
        public async Task<ActionResult> GetTasasDeCambio()
        {
            var tasasDeCambio = await _dbContext.TasasDeCambio.ToListAsync();
            return Ok(tasasDeCambio);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTasaDeCambio([FromBody] TasaDeCambio tasaDeCambio)
        {
            tasaDeCambio.Id = Guid.NewGuid();
            tasaDeCambio.Fecha = DateTime.Now;
            await _dbContext.TasasDeCambio.AddAsync(tasaDeCambio);
            await _dbContext.SaveChangesAsync();
            return Ok(tasaDeCambio);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTasaDeCambio([FromServices] ProyectContext dbContext, [FromBody] TasaDeCambio tasaDeCambio, Guid id)
        {
            var tasaDeCambioToUpdate = await dbContext.TasasDeCambio.FindAsync(id);
            if (tasaDeCambioToUpdate == null)
            {
                return NotFound();
            }

            tasaDeCambioToUpdate.Valor = tasaDeCambio.Valor;
            tasaDeCambioToUpdate.Fecha = tasaDeCambio.Fecha;

            await dbContext.SaveChangesAsync();
            return Ok(tasaDeCambioToUpdate);
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