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
    public class ClienteController : ControllerBase
    {
        private readonly ProyectContext _dbContext;

        public ClienteController(ProyectContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetClientes()
        {
            var clientes = await _dbContext.Clientes.Include(cliente => cliente.Facturas).ToListAsync();
            return Ok(clientes);
        }

        [HttpGet("search")]
        public async Task<ActionResult> GetClienteByNombreOrCodigo(string? nombre, string? codigo)
        {
            var query = _dbContext.Clientes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                query = query.Where(c => c.Nombre.Contains(nombre));
            }

            if (!string.IsNullOrWhiteSpace(codigo))
            {
                query = query.Where(c => c.Codigo.Contains(codigo));
            }

            var clientes = await query.Include(cliente => cliente.Facturas).ToListAsync();
            return Ok(clientes);
        }

        [HttpPost]
        public async Task<ActionResult> AddCliente([FromBody] Cliente cliente)
        {
            cliente.Id = Guid.NewGuid();
            await _dbContext.Clientes.AddAsync(cliente);
            await _dbContext.SaveChangesAsync();
            return Ok(cliente);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCliente([FromServices] ProyectContext dbContext, [FromBody] Cliente cliente, Guid id)
        {
            var clienteToUpdate = await dbContext.Clientes.FindAsync(id);
            if (clienteToUpdate == null)
            {
                return NotFound();
            }

            clienteToUpdate.Nombre = cliente.Nombre;
            clienteToUpdate.Codigo = cliente.Codigo;
            clienteToUpdate.Direccion = cliente.Direccion;

            await dbContext.SaveChangesAsync();
            return Ok(clienteToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCliente([FromServices] ProyectContext dbContext, Guid id)
        {
            var clienteToDelete = await dbContext.Clientes.FindAsync(id);
            if (clienteToDelete == null)
            {
                return NotFound();
            }

            dbContext.Clientes.Remove(clienteToDelete);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}