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
        public async Task<ActionResult<IEnumerable<ClienteDto>>> GetClientes()
        {
            var clientes = await _dbContext.Clientes.Include(cliente => cliente.Facturas).ToListAsync();
            var clientesDto = clientes.Select(cliente => new ClienteDto
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Codigo = cliente.Codigo,
                Direccion = cliente.Direccion
            }).ToList();

            return Ok(clientesDto);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> GetClienteByNombreOrCodigo(string? nombre, string? codigo)
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
            var clientesDto = clientes.Select(cliente => new ClienteDto
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Codigo = cliente.Codigo,
                Direccion = cliente.Direccion
            }).ToList();
            return Ok(clientesDto);
        }

        public class ClienteCreationDto
        {
            public string Nombre { get; set; }
            public string Codigo { get; set; }
            public string Direccion { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDto>> AddCliente([FromBody] Cliente cliente)
        {
            cliente.Id = Guid.NewGuid();
            await _dbContext.Clientes.AddAsync(cliente);
            await _dbContext.SaveChangesAsync();
            var clienteDto = new ClienteDto
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Codigo = cliente.Codigo,
                Direccion = cliente.Direccion
            };
            return Ok(clienteDto);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteDto>> UpdateCliente([FromServices] ProyectContext dbContext, [FromBody] Cliente cliente, Guid id)
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
            var clienteDto = new ClienteDto
            {
                Id = clienteToUpdate.Id,
                Nombre = clienteToUpdate.Nombre,
                Codigo = clienteToUpdate.Codigo,
                Direccion = clienteToUpdate.Direccion
            };
            return Ok(clienteDto);
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