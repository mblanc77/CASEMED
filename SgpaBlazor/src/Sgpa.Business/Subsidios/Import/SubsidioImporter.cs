using System.Globalization;
using Sgpa.Business.Subsidios.Export;
using Sgpa.Data;

namespace Sgpa.Business.Subsidios.Import;

/// <summary>
/// Imports auxiliares de la liquidación (port de frmLiquidaSubsidio): carga de la liquidación BPS
/// (CargarLiquidacionBPS) y actualización de líquidos (ActualizarLiquidos). Aceptan CSV (separador
/// <c>;</c>, primera fila encabezados) — el operador guarda como CSV la planilla que antes era .xls.
/// </summary>
public sealed class SubsidioImporter
{
    private readonly IDbExecutor _db;
    public SubsidioImporter(IDbExecutor db) => _db = db;

    /// <summary>
    /// Carga la liquidación BPS desde la planilla (hoja DATOS del VB6). Reemplaza la entrega:
    /// borra Liquidacion_BPS de ese N° de entrega y vuelve a insertar las filas.
    /// </summary>
    public async Task<BpsImportResult> CargarBpsAsync(string csv, CancellationToken ct = default)
    {
        var (_, rows) = SubsidioCsv.Parse(csv);
        if (rows.Count == 0) return new BpsImportResult(0, null);

        var nroEntrega = GetInt(rows[0], "N_ ENTREGA", "N° ENTREGA", "N ENTREGA", "NRO ENTREGA", "ENTREGA");
        if (nroEntrega is null)
            throw new InvalidOperationException("La planilla no tiene la columna 'N° ENTREGA'.");

        await using var uow = await _db.BeginTransactionAsync(ct).ConfigureAwait(false);

        await uow.ExecuteAsync(
            "DELETE FROM dbo.Liquidacion_BPS WHERE [N_ ENTREGA] = @e",
            new { e = nroEntrega }, cancellationToken: ct).ConfigureAwait(false);

        int insertados = 0;
        foreach (var r in rows)
        {
            var ci = GetDouble(r, "CI");
            if (ci is null) continue; // filas vacías al final de la planilla
            await uow.ExecuteAsync(
                @"INSERT INTO dbo.Liquidacion_BPS
                    (CI, NOMBRE, APELLIDO, MONTO_TOTAL, MES_DE_CARGO, NOM_EMPRESA, PCT_POR_EMPRESA,
                     FECHA_PER_DESDE, FECHA_PER_HASTA, [N_ ENTREGA], FECHA_DE_ENTREGA, MES, ANIO, LIQUIDO)
                  VALUES (@ci, @nombre, @apellido, @monto, @mesCargo, @empresa, @pct,
                          @desde, @hasta, @entrega, @fechaEntrega, @mes, @anio, @liquido)",
                new
                {
                    ci = ci.Value,
                    nombre = GetStr(r, "NOMBRE"),
                    apellido = GetStr(r, "APELLIDO"),
                    monto = GetDouble(r, "MONTO_TOTAL"),
                    mesCargo = GetInt(r, "MES_DE_CARGO") ?? 0,
                    empresa = GetStr(r, "NOM_EMPRESA"),
                    pct = GetDouble(r, "PCT_POR_EMPRESA") ?? 0d,
                    desde = GetDate(r, "FECHA_PER_DESDE"),
                    hasta = GetDate(r, "FECHA_PER_HASTA"),
                    entrega = nroEntrega,
                    fechaEntrega = GetDate(r, "FECHA_DE_ENTREGA"),
                    mes = GetInt(r, "MES"),
                    anio = GetInt(r, "ANIO"),
                    liquido = GetDouble(r, "LIQUIDO")
                }, cancellationToken: ct).ConfigureAwait(false);
            insertados++;
        }

        await uow.CommitAsync(ct).ConfigureAwait(false);
        return new BpsImportResult(insertados, nroEntrega);
    }

    /// <summary>
    /// Actualiza los líquidos de los subsidios desde la planilla (round-trip del export BPS).
    /// Port fiel de la query Access <c>506_Update_Liquidos</c>:
    /// <c>UPDATE SubsidioCabezal SET ImpLiquido = [planilla].[LiquidoPagar] ON IdSubsidio</c>.
    /// La planilla debe traer las columnas <c>IdSubsidio</c> y <c>LiquidoPagar</c>.
    /// </summary>
    public async Task<int> ActualizarLiquidosAsync(string csv, CancellationToken ct = default)
    {
        var (_, rows) = SubsidioCsv.Parse(csv);
        if (rows.Count == 0) return 0;

        await using var uow = await _db.BeginTransactionAsync(ct).ConfigureAwait(false);
        int actualizados = 0;
        foreach (var r in rows)
        {
            var id = GetInt(r, "IdSubsidio", "Id");
            var liquido = GetDouble(r, "LiquidoPagar", "ImpLiquido", "Liquido", "Líquido");
            if (id is null || liquido is null) continue;
            actualizados += await uow.ExecuteAsync(
                "UPDATE dbo.SubsidioCabezal SET ImpLiquido = @liq WHERE IdSubsidio = @id",
                new { liq = liquido.Value, id = id.Value }, cancellationToken: ct).ConfigureAwait(false);
        }
        await uow.CommitAsync(ct).ConfigureAwait(false);
        return actualizados;
    }

    private static string? GetStr(IReadOnlyDictionary<string, string> row, params string[] keys)
    {
        foreach (var k in keys)
            if (row.TryGetValue(k, out var v) && !string.IsNullOrWhiteSpace(v))
                return v.Trim();
        return null;
    }

    private static int? GetInt(IReadOnlyDictionary<string, string> row, params string[] keys)
        => GetDouble(row, keys) is { } d ? (int)Math.Round(d) : null;

    private static double? GetDouble(IReadOnlyDictionary<string, string> row, params string[] keys)
    {
        var s = GetStr(row, keys);
        if (s is null) return null;
        s = s.Replace(".", "").Replace(',', '.'); // 1.234,56 → 1234.56
        if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var d)) return d;
        // Reintento: formato con punto decimal nativo
        if (double.TryParse(GetStr(row, keys), NumberStyles.Any, CultureInfo.InvariantCulture, out d)) return d;
        return null;
    }

    private static DateTime? GetDate(IReadOnlyDictionary<string, string> row, params string[] keys)
    {
        var s = GetStr(row, keys);
        if (s is null) return null;
        string[] fmts = { "dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd", "dd/MM/yyyy HH:mm:ss", "M/d/yyyy" };
        if (DateTime.TryParseExact(s, fmts, CultureInfo.InvariantCulture, DateTimeStyles.None, out var d)) return d;
        if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out d)) return d;
        return null;
    }
}

public sealed record BpsImportResult(int Insertados, int? NroEntrega);
