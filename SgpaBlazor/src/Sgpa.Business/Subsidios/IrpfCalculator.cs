using Sgpa.Data;

namespace Sgpa.Business.Subsidios;

/// <summary>Cálculo del IRPF por franjas progresivas (port de la clase VB6 <c>cIRPF</c>).</summary>
public interface IIrpfCalculator
{
    Task<decimal> CalcularAsync(decimal importe, CancellationToken cancellationToken = default);
}

public sealed class IrpfCalculator : IIrpfCalculator
{
    private readonly IDbExecutor _db;

    public IrpfCalculator(IDbExecutor db) => _db = db;

    /// <summary>
    /// IRPF = suma sobre cada franja de (min(importe, Hasta·BCP) − Desde·BCP) · Porcentaje/100,
    /// cortando cuando Desde·BCP supera el importe. BCP = factor de <c>Parametros.BCP</c>.
    /// </summary>
    public async Task<decimal> CalcularAsync(decimal importe, CancellationToken cancellationToken = default)
    {
        var factor = await _db.ExecuteScalarAsync<decimal>(
            "SELECT TOP 1 CAST(BCP AS decimal(18,4)) FROM dbo.Parametros",
            cancellationToken: cancellationToken).ConfigureAwait(false);

        var franjas = await _db.QueryAsync<Franja>(
            @"SELECT CAST(Desde AS decimal(18,4)) AS Desde,
                     CAST(Hasta AS decimal(18,4)) AS Hasta,
                     CAST(Porcentaje AS decimal(18,4)) AS Porcentaje
              FROM dbo.FranjaIRPF ORDER BY Desde",
            cancellationToken: cancellationToken).ConfigureAwait(false);

        decimal total = 0m;
        foreach (var f in franjas)
        {
            var sustraendo = f.Desde * factor;
            if (sustraendo > importe) break;
            var minuendo = Math.Min(importe, f.Hasta * factor);
            total += (minuendo - sustraendo) * (f.Porcentaje / 100m);
        }
        return total;
    }

    private sealed record Franja(decimal Desde, decimal Hasta, decimal Porcentaje);
}
