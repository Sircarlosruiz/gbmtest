using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace gbmtest.Services
{
    public class CurrencyExchangeService
    {
        private readonly ProyectContext _dbContext;

        public CurrencyExchangeService(ProyectContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<decimal> ObtenerTasaDeCambioActual()
        {
            var tasaDeCambio = await _dbContext.TasasDeCambio
                .OrderByDescending(t => t.Fecha)
                .FirstOrDefaultAsync();

            return tasaDeCambio?.Valor ?? 1;
        }

    }
}