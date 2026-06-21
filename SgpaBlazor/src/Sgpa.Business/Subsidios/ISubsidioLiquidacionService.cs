namespace Sgpa.Business.Subsidios;

/// <summary>
/// Orquestador de la liquidación de subsidios (port de VB6 frmLiquidaSubsidio.LiquidarSubsidio).
/// Proceso central y más complejo del sistema. La implementación completa se construye por etapas;
/// se apoya en los SPs migrados acc_sgpa_300/490/500/505/506_* y en [[IIrpfCalculator]].
///
/// Pasos del algoritmo VB6 a portar:
///   1. VerificarConsistencia(mes anterior) y borrar SubsidioCabezal del período.
///   2. Seleccionar CIs con certificación vigente que trabajan en empresa con Liquidar=flag.
///   3. Por afiliado: GenerarCertificaciones -> por certificación temporal:
///        a. ValorJornal  (promedio de imponibles en ventana de -6 meses; control de aportes -12m).
///        b. ProcesarCertificaciones (días, descuentos: DescontarDia/DescontarDiasSeguro).
///        c. ProcesarItems / ProcesarItemsXEmpresa (items por empresa; IRPF vía VerificarIRP).
///        d. Calcular ImpNominal/ImpAguinaldo/ImpLiquido/ValorJornal/Dias (Rdo = 3 decimales).
///        e. InsertarBPS.
/// </summary>
public interface ISubsidioLiquidacionService
{
    /// <summary>
    /// Liquida (o simula, si <paramref name="liquidar"/> es false) los subsidios del período.
    /// Si <paramref name="ci"/> tiene valor, procesa sólo ese afiliado.
    /// Si <paramref name="genNroRecibo"/> es true, asigna nros de recibo correlativos a los cabezales
    /// generados (port del chkGenNroRecibo + GetProxNroRecibo del VB6).
    /// </summary>
    Task<LiquidacionResultado> LiquidarAsync(int anio, int mes, bool liquidar, long? ci = null,
        bool genNroRecibo = false, IProgress<LiquidacionProgreso>? progreso = null,
        CancellationToken cancellationToken = default);
}

public sealed record LiquidacionResultado(int AfiliadosProcesados, int SubsidiosGenerados, IReadOnlyList<string> Avisos);

/// <summary>Avance de la liquidación (afiliados procesados / total), para mostrar progreso en la UI.</summary>
public readonly record struct LiquidacionProgreso(int Procesados, int Total)
{
    public int Porcentaje => Total <= 0 ? 0 : (int)(Procesados * 100L / Total);
}
