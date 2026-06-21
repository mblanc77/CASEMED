namespace Sgpa.Business.Subsidios;

/// <summary>
/// Acumulador por afiliado durante la liquidación de un subsidio. Port del tipo VB6
/// <c>tAfiliados</c> que va juntando importes a lo largo de ValorJornal / ProcesarCertificaciones
/// / ProcesarItems antes de volcarse a SubsidioCabezal.
/// </summary>
public sealed class LiquidacionAcumulador
{
    public long CI { get; set; }
    public decimal ImpNominal { get; set; }
    public decimal ImpAguinaldo { get; set; }
    public decimal ImpLiquido { get; set; }
    public decimal ValorJornal { get; set; }
    public int DiasCertif { get; set; }

    // Coeficientes de prorrateo del aporte jubilatorio cuando se llega al tope (régimen nuevo).
    // Los usa ProcesarItemsXEmpresa para repartir el aporte patronal por empresa.
    public decimal CoefAporteNominal { get; set; } = 1m;
    public decimal CoefAporteAguinaldo { get; set; } = 1m;
}
