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
    public class ReporteController : ControllerBase
    {
        private readonly ProyectContext _dbContext;
        private readonly ConversionMonedaService _conversionService;

        public ReporteController(ProyectContext dbContext, ConversionMonedaService conversionService)
        {
            _dbContext = dbContext;
            _conversionService = conversionService;
        }

        [HttpGet("ventas-mensuales")]
        public async Task<ActionResult<IEnumerable<ReporteVentaMensualDTO>>> GetVentasMensuales(
            string? codigoCliente, string? nombreCliente, int? anio, int? mes, string? producto, string? sku)
        {
            // Comienza la consulta por clientes
            var clientesQuery = _dbContext.Clientes.AsQueryable();

            // Filtra por código y nombre si están presentes
            if (!string.IsNullOrWhiteSpace(codigoCliente))
            {
                clientesQuery = clientesQuery.Where(c => c.Codigo == codigoCliente);
            }
            if (!string.IsNullOrWhiteSpace(nombreCliente))
            {
                clientesQuery = clientesQuery.Where(c => c.Nombre == nombreCliente);
            }

            // Incluye las facturas y detalles de factura y productos
            var clientesConFacturas = await clientesQuery
                .Select(c => new
                {
                    Cliente = c,
                    Facturas = c.Facturas
                        .Where(f => (!anio.HasValue || f.Fecha.Year == anio) &&
                                    (!mes.HasValue || f.Fecha.Month == mes) &&
                                    (producto == null || f.DetallesFactura.Any(d => d.Producto.Descripcion == producto)) &&
                                    (sku == null || f.DetallesFactura.Any(d => d.Producto.SKU == sku)))
                        .Select(f => new
                        {
                            f.Fecha,
                            Detalles = f.DetallesFactura.Select(d => new
                            {
                                d.Cantidad,
                                PrecioCordobas = d.PrecioUnitario,
                                PrecioDolares = _conversionService.ConvertirCordobasADolares(d.PrecioUnitario),
                                d.Producto.Descripcion,
                                d.Producto.SKU
                            })
                        })
                })
                .ToListAsync();


            // Construye el reporte
            var reporte = clientesConFacturas.Select(c => new ReporteVentaMensualDTO
            {
                CodigoCliente = c.Cliente.Codigo,
                NombreCliente = c.Cliente.Nombre,
                Anio = anio ?? 0,
                Mes = mes ?? 0,
                TotalDolares = c.Facturas.Sum(f => f.Detalles.Sum(d => d.Cantidad * d.PrecioDolares)),
                TotalCordobas = c.Facturas.Sum(f => f.Detalles.Sum(d => d.Cantidad * d.PrecioCordobas)),
                Producto = string.Join(", ", c.Facturas.SelectMany(f => f.Detalles.Select(d => d.Descripcion)).Distinct()),
                SKU = string.Join(", ", c.Facturas.SelectMany(f => f.Detalles.Select(d => d.SKU)).Distinct())
            }).ToList();


            return Ok(reporte);
        }

    }

}