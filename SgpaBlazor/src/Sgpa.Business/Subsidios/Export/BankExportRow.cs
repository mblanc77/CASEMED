namespace Sgpa.Business.Subsidios.Export;

/// <summary>Fila de exportación bancaria (salida de acc_sgpa_Rs_Export_NBC / _BROU).</summary>
public sealed class BankExportRow
{
    public long CI { get; set; }
    public decimal ImpLiquido { get; set; }
    public decimal ImpNominal { get; set; }
    public decimal ImpAguinaldo { get; set; }
    public int? CodBanco { get; set; }
    public string? NroCuenta { get; set; }
    public DateTime Fecha { get; set; }
}
