namespace Sgpa.Business.Subsidios;

/// <summary>Acceso a datos del proceso de liquidación de subsidios (sobre NewSgpa2).</summary>
public interface ISubsidioRepository
{
    /// <summary>
    /// CIs con certificación vigente en el período que trabajan en una empresa con el flag
    /// Liquidar indicado (y con ingreso/baja consistentes). Port del SELECT inicial de LiquidarSubsidio.
    /// </summary>
    Task<IReadOnlyList<long>> SeleccionarAfiliadosAsync(SubsidioPeriodo periodo, bool liquidar, long? ci = null,
        CancellationToken cancellationToken = default);

    /// <summary>Borra los SubsidioCabezal previos del período (mismo flag Liquidar). Devuelve filas borradas.</summary>
    Task<int> BorrarPeriodoAsync(SubsidioPeriodo periodo, bool liquidar, long? ci = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Genera los tramos de certificación del afiliado en el período, consolidando certificaciones
    /// consecutivas (FechaIni = FechaFin previa + 1 día). Port en memoria de GenerarCertificaciones.
    /// </summary>
    Task<IReadOnlyList<CertificacionSpan>> GenerarCertificacionesAsync(long ci, SubsidioPeriodo periodo,
        CancellationToken cancellationToken = default);

    /// <summary>Próximo IdSubsidio (MAX+1; SubsidioCabezal.IdSubsidio no es identity en la base migrada).</summary>
    Task<int> ProximoIdSubsidioAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// VerificarConsistencia (port): ¿hay cabezales del mes anterior sin su SubsidioEnfermedad?
    /// Síntoma de datos borrados/incompletos; si los hay no se debe liquidar.
    /// </summary>
    Task<bool> ExistenCabezalesSinEnfermedadAsync(int anio, int mes, CancellationToken cancellationToken = default);

    /// <summary>Inserta el cabezal del subsidio (montos en 0; se completan luego).</summary>
    Task InsertarCabezalAsync(int idSubsidio, long ci, SubsidioPeriodo periodo, bool liquidar, string usr,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asigna nros de recibo correlativos a los cabezales del período (alcance liquidar/ci),
    /// arrancando en MAX(NroRecibo)+1. Port de chkGenNroRecibo + GetProxNroRecibo.
    /// </summary>
    Task GenerarNrosReciboAsync(SubsidioPeriodo periodo, bool liquidar, long? ci,
        CancellationToken cancellationToken = default);

    /// <summary>Actualiza los montos calculados del cabezal.</summary>
    Task ActualizarCabezalMontosAsync(int idSubsidio, decimal valorJornal, int dias,
        decimal impNominal, decimal impAguinaldo, decimal impLiquido, string usr,
        CancellationToken cancellationToken = default);

    /// <summary>Inserta una fila de detalle por empresa (SubsidioCabezalEmpresa).</summary>
    Task InsertarCabezalEmpresaAsync(int idSubsidio, int codEmpresa, decimal valorJornal, int dias,
        decimal impNominal, decimal impAguinaldo, decimal impLiquido, string usr,
        CancellationToken cancellationToken = default);

    /// <summary>Empresas (con su ValorJornal) ya cargadas en el detalle del cabezal.</summary>
    Task<IReadOnlyList<CabezalEmpresa>> GetCabezalEmpresasAsync(int idSubsidio, CancellationToken cancellationToken = default);

    /// <summary>Empresas del cabezal con sus importes nominal/aguinaldo (para el reparto de ítems por empresa).</summary>
    Task<IReadOnlyList<CabezalEmpresaMontos>> GetCabezalEmpresasMontosAsync(int idSubsidio, CancellationToken cancellationToken = default);

    /// <summary>Inserta una línea de ítem por empresa (SubsidioItemEmpresa).</summary>
    Task InsertarItemEmpresaAsync(int idSubsidio, long codSubsidioItemCod, int codEmpresa, decimal importe, string usr,
        CancellationToken cancellationToken = default);

    /// <summary>Fija los importes finales de una fila de detalle por empresa.</summary>
    Task ActualizarCabezalEmpresaFinalAsync(int idSubsidio, int codEmpresa, decimal impNominal, decimal impAguinaldo,
        decimal impLiquido, int dias, string usr, CancellationToken cancellationToken = default);

    /// <summary>Actualiza los importes nominal/aguinaldo de una fila de detalle por empresa.</summary>
    Task ActualizarCabezalEmpresaMontosAsync(int idSubsidio, int codEmpresa, decimal impNominal, decimal impAguinaldo,
        string usr, CancellationToken cancellationToken = default);

    /// <summary>Inserta el período de enfermedad del subsidio (sólo fechas en el esquema migrado).</summary>
    Task InsertarEnfermedadAsync(int idSubsidio, DateTime fechaIni, DateTime fechaFin,
        DateTime fechaIniSubsidio, DateTime fechaFinSubsidio, string usr, CancellationToken cancellationToken = default);

    /// <summary>¿Hay una certificación del afiliado en la fecha indicada? (control del descuento del primer día).</summary>
    Task<bool> ExisteCertificacionEnFechaAsync(long ci, DateTime fecha, CancellationToken cancellationToken = default);

    /// <summary>Valores de configuración del proceso (Parametros: SMN, TopeJubilatorio, ...).</summary>
    Task<SubsidioParametros> GetParametrosAsync(CancellationToken cancellationToken = default);

    /// <summary>CodEmpresa de la línea "Casemed" (Subsidio por Enf.) desde xUsrParam(clave='CodCasemed'); 900 por defecto.</summary>
    Task<int> GetCodCasemedAsync(CancellationToken cancellationToken = default);

    /// <summary>Configuración de ítems aplicables al afiliado en el período (acc_sgpa_300_SubsidioItemCod_Full).</summary>
    Task<IReadOnlyList<ItemCodConfig>> GetItemsCodFullAsync(long ci, SubsidioPeriodo periodo, CancellationToken cancellationToken = default);

    /// <summary>Inserta una línea de ítem del subsidio (SubsidioItem).</summary>
    Task InsertarItemAsync(int idSubsidio, long codSubsidioItemCod, decimal importe, string usr, CancellationToken cancellationToken = default);

    /// <summary>Régimen jubilatorio del afiliado (0/NULL → régimen nuevo = 2).</summary>
    Task<int> GetRegimenJubilatorioAsync(long ci, CancellationToken cancellationToken = default);

    /// <summary>Valor (coeficiente) de un ítem por su código (acc_sgpa_300_SubsidioItemId); NULL → 0.</summary>
    Task<decimal> GetSubsidioItemValorAsync(long codSubsidioItemCod, CancellationToken cancellationToken = default);

    /// <summary>Importe de la franja anterior para controlar el IRP (acc_sgpa_300_SubsidioFranjaAnt); null si no hay.</summary>
    Task<decimal?> GetImporteFranjaAnteriorAsync(long codSubsidioItemCod, decimal importe, decimal smn, CancellationToken cancellationToken = default);

    // ---- BPS ----
    /// <summary>Fechas del período de enfermedad del subsidio.</summary>
    Task<EnfermedadFechas?> GetEnfermedadFechasAsync(int idSubsidio, CancellationToken cancellationToken = default);

    /// <summary>Tipo de salida de la certificación efectiva del afiliado que comienza en la fecha.</summary>
    Task<int?> GetCodSalidaTipoAsync(long ci, DateTime fechaIni, CancellationToken cancellationToken = default);

    /// <summary>Certificación efectiva anterior contigua (la que termina en <paramref name="fechaFin"/>).</summary>
    Task<CertFechas?> GetCertificacionPorFechaFinAsync(long ci, DateTime fechaFin, CancellationToken cancellationToken = default);

    /// <summary>Total de líquido BPS ya liquidado al afiliado en el mes (para no topear dos veces).</summary>
    Task<decimal> GetTotalLiquidoBpsAsync(long ci, int mes, int anio, CancellationToken cancellationToken = default);

    /// <summary>Inserta la fila BPS del subsidio (SubsidioCabezal_BPS).</summary>
    Task InsertarCabezalBpsAsync(int idSubsidio, int diasBps, decimal liquidoBps, decimal liquidoPagar, string usr,
        CancellationToken cancellationToken = default);
}

public sealed class EnfermedadFechas
{
    public DateTime FechaIni { get; set; }
    public DateTime FechaIniSubsidio { get; set; }
    public DateTime FechaFinSubsidio { get; set; }
}

public sealed class CertFechas
{
    public DateTime FechaIni { get; set; }
    public DateTime FechaFin { get; set; }
}

/// <summary>Configuración de un ítem de subsidio (todo anulable: la config puede no estar cargada).</summary>
public sealed class ItemCodConfig
{
    public long CodSubsidioItemCod { get; set; }
    public string? Tipo { get; set; }
    public string? ValorTipo { get; set; }
    public bool? Comparar { get; set; }
    public int? CompararContra { get; set; }
    public double? Valor { get; set; }
    public string? TipoComp { get; set; }
    public string? Operador { get; set; }
    public double? ValorMin { get; set; }
    public double? ValorMax { get; set; }
    public bool? ModificaNominal { get; set; }
    public int? Signo { get; set; }
}

/// <summary>Empresa del detalle del cabezal con su valor de jornal.</summary>
public sealed class CabezalEmpresa
{
    public int CodEmpresa { get; set; }
    public double ValorJornal { get; set; }
}

/// <summary>Empresa del detalle del cabezal con sus importes nominal/aguinaldo.</summary>
public sealed class CabezalEmpresaMontos
{
    public int CodEmpresa { get; set; }
    public double ImpNominal { get; set; }
    public double ImpAguinaldo { get; set; }
}

/// <summary>Configuración numérica del cálculo (fila única de dbo.Parametros).</summary>
public sealed class SubsidioParametros
{
    public decimal SMN { get; set; }
    public decimal TopeJubilatorio { get; set; }
    public decimal PctBPS { get; set; }          // C_PCTAPORTES (aportes sobre el líquido BPS)
    public decimal TopeLiquidoBPS { get; set; }  // tope del líquido que paga BPS
}
