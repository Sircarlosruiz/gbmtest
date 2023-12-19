using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gbmtest.Services
{
public class ConversionMonedaService
{
    private const decimal TasaCambio = 36;

    public decimal ConvertirCordobasADolares(decimal precioCordobas)
    {
        return precioCordobas / TasaCambio;
    }

    public decimal CalcularIVA(decimal precioCordobas)
    {
        const decimal tasaIVA = 0.15m;
        return precioCordobas * tasaIVA;
    }
}

}