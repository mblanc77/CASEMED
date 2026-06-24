using Sgpa.Data;

namespace Sgpa.Business.Subsidios.Export;

/// <summary>
/// Exports auxiliares de la liquidación de subsidios (port de frmLiquidaSubsidio):
/// archivo Discount (banco 2), planilla BROU (Excel) y planilla BPS. Devuelven <b>.xls</b> real
/// (SpreadsheetML 2003, ver <see cref="SubsidioXls"/>) — paridad con los .xls del VB6.
/// </summary>
public sealed class SubsidioExtraExporter
{
    // Bcpart.bas: PC_COD_BANCO_DISCOUNT = 2, PC_MONEDA_DISCOUNT = 0.
    private const int CodBancoDiscount = 2;
    private const int MonedaDiscount = 0;

    private readonly IDbExecutor _db;
    public SubsidioExtraExporter(IDbExecutor db) => _db = db;

    /// <summary>Archivo Discount: afiliados con CodBanco=2 e ImpLiquido&gt;0 del período.</summary>
    public async Task<string> GenerarDiscountAsync(int mes, int anio, DateTime fechaPago, bool liquidar,
        CancellationToken ct = default)
    {
        var rows = await _db.QueryAsync<DiscountRow>(
            @"SELECT a.CI, a.NroCuenta, ROUND(c.ImpLiquido, 0) AS Importe
              FROM dbo.SubsidioCabezal c INNER JOIN dbo.Afiliado a ON c.CI = a.CI
              WHERE c.Mes=@mes AND c.Anio=@anio AND c.Liquidar=@liquidar
                AND a.CodBanco=@banco AND c.ImpLiquido > 0
              ORDER BY a.CI",
            new { mes, anio, liquidar, banco = CodBancoDiscount }, cancellationToken: ct).ConfigureAwait(false);

        return SubsidioXls.Build("Discount",
            new[] { "NroFuncionario", "NroCuenta", "Fecha", "Importe", "Moneda" },
            rows.Select(r => (IReadOnlyList<object?>)new object?[]
            {
                r.CI, r.NroCuenta, fechaPago, (decimal)(r.Importe ?? 0d), MonedaDiscount
            }));
    }

    /// <summary>Planilla BROU (Excel): detalle de pagos a acreditar del período.</summary>
    public async Task<string> GenerarBrouExcelAsync(int mes, int anio, DateTime fechaPago, bool liquidar,
        CancellationToken ct = default)
    {
        var rows = await _db.QueryProcAsync<BankExportRow>("dbo.acc_sgpa_Rs_Export_BROU",
            new { pMes = mes.ToString(), pAnio = anio.ToString(), pLiquidar = liquidar ? "1" : "0", pFecha = fechaPago },
            ct).ConfigureAwait(false);

        return SubsidioXls.Build("BROU",
            new[] { "CI", "NroCuenta", "Fecha", "Importe" },
            rows.Select(r => (IReadOnlyList<object?>)new object?[]
            {
                r.CI, r.NroCuenta, fechaPago, SubsidioMath.Money2(r.ImpLiquido)
            }));
    }

    /// <summary>Planilla BPS: subsidios del período cruzados con la liquidación BPS cargada.</summary>
    public async Task<string> GenerarBpsAsync(int mes, int anio, bool liquidar, CancellationToken ct = default)
    {
        // Pareo posicional subsidio <-> entrega BPS por CI: las fechas no siempre coinciden exacto, así que se
        // ordenan los subsidios por FechaIniSubsidio y las entregas de Liquidacion_BPS por FECHA_PER_DESDE y se
        // emparejan por orden (1º con 1º, 2º con 2º, ...). Evita el producto cartesiano del join por CI (que
        // duplicaba y cruzaba mal a quien tiene >1 subsidio y >1 entrega en el mes). 1 fila por subsidio.
        var rows = await _db.QueryAsync<BpsExportRow>(
            @"WITH sub AS (
                  SELECT c.IdSubsidio, c.CI, c.Dias, c.NroRecibo, c.ImpNominal, c.ImpAguinaldo, c.ImpLiquido,
                         ROW_NUMBER() OVER (PARTITION BY c.CI ORDER BY e.FechaIniSubsidio, c.IdSubsidio) AS rn
                  FROM dbo.SubsidioCabezal c
                  LEFT JOIN dbo.SubsidioEnfermedad e ON e.IdSubsidio = c.IdSubsidio
                  WHERE c.Mes=@mes AND c.Anio=@anio AND c.Liquidar=@liquidar
              ),
              bps AS (
                  SELECT lb.CI, lb.MONTO_TOTAL, lb.LIQUIDO, lb.MES_DE_CARGO, lb.NOM_EMPRESA, lb.PCT_POR_EMPRESA,
                         lb.FECHA_PER_DESDE, lb.FECHA_PER_HASTA, lb.[N_ ENTREGA] AS NroEntrega, lb.FECHA_DE_ENTREGA,
                         ROW_NUMBER() OVER (PARTITION BY lb.CI ORDER BY lb.FECHA_PER_DESDE, lb.Id) AS rn
                  FROM dbo.Liquidacion_BPS lb
                  WHERE lb.MES=@mes AND lb.ANIO=@anio
              )
              SELECT sub.IdSubsidio, sub.CI, a.Apellido1, a.Apellido2, a.Nombres, sub.Dias, sub.NroRecibo,
                     sub.ImpNominal, sub.ImpAguinaldo, sub.ImpLiquido,
                     b.DiasBPS, b.LiquidoBPS, b.LiquidoPagar,
                     a.CodBanco AS Banco, a.NroCuenta,
                     lb.MONTO_TOTAL, lb.LIQUIDO AS LiquidoBpsEmpresa, lb.MES_DE_CARGO, lb.NOM_EMPRESA,
                     lb.PCT_POR_EMPRESA, lb.FECHA_PER_DESDE, lb.FECHA_PER_HASTA,
                     lb.NroEntrega, lb.FECHA_DE_ENTREGA
              FROM sub
              INNER JOIN dbo.Afiliado a ON sub.CI = a.CI
              LEFT JOIN dbo.SubsidioCabezal_BPS b ON b.IdSubsidio = sub.IdSubsidio
              LEFT JOIN bps lb ON lb.CI = sub.CI AND lb.rn = sub.rn
              ORDER BY sub.CI, sub.IdSubsidio",
            new { mes, anio, liquidar }, cancellationToken: ct).ConfigureAwait(false);

        return SubsidioXls.Build("BPS",
            new[]
            {
                "IdSubsidio", "CI", "Apellido1", "Apellido2", "Nombres", "Dias", "NroRecibo",
                "ImpNominal", "ImpAguinaldo", "ImpLiquido", "DiasBPS", "LiquidoBPS", "LiquidoPagar",
                "Banco", "NroCuenta", "MONTO_TOTAL", "LIQUIDO", "MES_DE_CARGO", "NOM_EMPRESA",
                "PCT_POR_EMPRESA", "FECHA_PER_DESDE", "FECHA_PER_HASTA", "N_ ENTREGA", "FECHA_DE_ENTREGA"
            },
            rows.Select(r => (IReadOnlyList<object?>)new object?[]
            {
                r.IdSubsidio, r.CI, r.Apellido1, r.Apellido2, r.Nombres, r.Dias, r.NroRecibo,
                r.ImpNominal, r.ImpAguinaldo, r.ImpLiquido, r.DiasBPS, r.LiquidoBPS, r.LiquidoPagar,
                r.Banco, r.NroCuenta, r.MONTO_TOTAL, r.LiquidoBpsEmpresa, r.MES_DE_CARGO, r.NOM_EMPRESA,
                r.PCT_POR_EMPRESA, r.FECHA_PER_DESDE, r.FECHA_PER_HASTA, r.NroEntrega, r.FECHA_DE_ENTREGA
            }));
    }

    private sealed class DiscountRow
    {
        public long CI { get; set; }
        public string? NroCuenta { get; set; }
        public double? Importe { get; set; }
    }

    private sealed class BpsExportRow
    {
        public int IdSubsidio { get; set; }
        public long? CI { get; set; }
        public string? Apellido1 { get; set; }
        public string? Apellido2 { get; set; }
        public string? Nombres { get; set; }
        public int? Dias { get; set; }
        public int? NroRecibo { get; set; }
        public double? ImpNominal { get; set; }
        public double? ImpAguinaldo { get; set; }
        public double? ImpLiquido { get; set; }
        public int? DiasBPS { get; set; }
        public double? LiquidoBPS { get; set; }
        public double? LiquidoPagar { get; set; }
        public int? Banco { get; set; }
        public string? NroCuenta { get; set; }
        public double? MONTO_TOTAL { get; set; }
        public double? LiquidoBpsEmpresa { get; set; }
        public int? MES_DE_CARGO { get; set; }
        public string? NOM_EMPRESA { get; set; }
        public double? PCT_POR_EMPRESA { get; set; }
        public DateTime? FECHA_PER_DESDE { get; set; }
        public DateTime? FECHA_PER_HASTA { get; set; }
        public int? NroEntrega { get; set; }
        public DateTime? FECHA_DE_ENTREGA { get; set; }
    }
}
