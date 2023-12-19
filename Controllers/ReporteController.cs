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
public class ReporteController : ControllerBase
{
    private readonly ProyectContext _dbContext;

    public ReporteController(ProyectContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("ventas-mensuales")]
    public async Task<ActionResult<IEnumerable<ReporteVentaMensualDTO>>> GetVentasMensuales(
        string? codigoCliente, string? nombreCliente, int? anio, int? mes, string? producto, string? sku)
    {
        var query = _dbContext.Facturas.AsQueryable();

        // Filtrar por parámetros si están presentes
        // ...

        var reporte = await query
            .Include(f => f.Cliente)
            .Include(f => f.DetallesFactura)
            .ThenInclude(d => d.Producto)
            .Where(f => (!anio.HasValue || f.Fecha.Year == anio) && 
                        (!mes.HasValue || f.Fecha.Month == mes) &&
                        (codigoCliente == null || (f.Cliente != null && f.Cliente.Codigo == codigoCliente)) &&
                        (nombreCliente == null || (f.Cliente != null && f.Cliente.Nombre == nombreCliente)) &&
                        (producto == null || f.DetallesFactura.Any(d => d.Producto.Descripcion == producto)) &&
                        (sku == null || f.DetallesFactura.Any(d => d.Producto.SKU == sku)))
            .GroupBy(f => new 
            {
                Codigo = f.Cliente != null ? f.Cliente.Codigo : null,
                Nombre = f.Cliente != null ? f.Cliente.Nombre : null,
                Año = f.Fecha.Year,
                Mes = f.Fecha.Month
            })
            .Select(g => new ReporteVentaMensualDTO
            {
                CodigoCliente = g.Key != null ? g.Key.Codigo : string.Empty,
                NombreCliente = g.Key != null ? g.Key.Nombre : string.Empty,
                Anio = g.Key.Año,
                Mes = g.Key.Mes,
                TotalDolares = g.Sum(f => f.DetallesFactura.Sum(d => d.Cantidad * d.Producto.PrecioDolares)),
                TotalCordobas = g.Sum(f => f.DetallesFactura.Sum(d => d.Cantidad * d.Producto.PrecioCordobas)),
                Producto = string.Join(", ", g.SelectMany(f => f.DetallesFactura.Select(d => d.Producto.Descripcion)).Distinct()),
                SKU = string.Join(", ", g.SelectMany(f => f.DetallesFactura.Select(d => d.Producto.SKU)).Distinct())
            })
            .ToListAsync();

        return Ok(reporte);
    }
}

}