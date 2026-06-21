using Sgpa.Data;

namespace Sgpa.Web.Reporting.Predefinidos;

/// <summary>Datos del afiliado para la ficha (vista dbo.Rpt_Afiliado, por CI). Port de AfiliadoDato.rpt.</summary>
public sealed class AfiliadoDatosData
{
    private static readonly System.Globalization.CultureInfo EsUy = System.Globalization.CultureInfo.GetCultureInfo("es-UY");

    public long CI { get; set; }
    public string? Nombres { get; set; }
    public string? Apellido1 { get; set; }
    public string? Apellido2 { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string? Sexo { get; set; }                 // código: "1" masculino, "2" femenino
    public string? Telefono { get; set; }
    public string? Movil { get; set; }
    public string? EMail { get; set; }
    public int CodMutualista { get; set; }
    public string? DescMutualista { get; set; }
    public DateTime? FechaIngMutualista { get; set; }
    public string? NroSocioMutualista { get; set; }
    public string? DescRegimenJubilatorio { get; set; }
    public string? DescAfiliado { get; set; }
    public string? Direccion { get; set; }
    public string? CodDepartamento { get; set; }
    public string? DescDepartamento { get; set; }
    public bool PagaMutualista { get; set; }

    // ---- computados (presentación) ----
    public string NombreCompleto => string.IsNullOrWhiteSpace(DescAfiliado)
        ? $"{Apellido1} {Apellido2}, {Nombres}".Replace(" ,", ",").Replace("  ", " ").Trim().Trim(',').Trim()
        : DescAfiliado!.Trim();
    public string CIFormato => CI.ToString("#,#", EsUy);
    public string SexoTexto => Sexo?.Trim() switch { "1" => "Masculino", "2" => "Femenino", _ => "—" };
    public string FechaNacFmt => FechaNacimiento is { } f ? f.ToString("dd/MM/yyyy", EsUy) : "—";
    public string FechaIngMutFmt => FechaIngMutualista is { } f ? f.ToString("dd/MM/yyyy", EsUy) : "—";
    public string PagaMutualistaTexto => PagaMutualista ? "Sí" : "No";
    public string MutualistaTexto => string.IsNullOrWhiteSpace(DescMutualista) ? "—" : DescMutualista!.Trim();
    public string RegimenTexto => string.IsNullOrWhiteSpace(DescRegimenJubilatorio) ? "—" : DescRegimenJubilatorio!.Trim();
    public string DireccionTexto => string.IsNullOrWhiteSpace(Direccion) ? "—" : Direccion!.Trim();
    public string DepartamentoTexto => string.IsNullOrWhiteSpace(DescDepartamento) ? "—" : DescDepartamento!.Trim();
    public string TelefonoTexto => string.IsNullOrWhiteSpace(Telefono) ? "—" : Telefono!.Trim();
    public string MovilTexto => string.IsNullOrWhiteSpace(Movil) ? "—" : Movil!.Trim();
    public string EMailTexto => string.IsNullOrWhiteSpace(EMail) ? "—" : EMail!.Trim();
    public string NroSocioTexto => string.IsNullOrWhiteSpace(NroSocioMutualista) ? "—" : NroSocioMutualista!.Trim();
    public string FechaHoy => DateTime.Today.ToString("dd 'de' MMMM 'de' yyyy", EsUy);
}

/// <summary>Una línea de la lista de empleos del afiliado (vista dbo.Rs_Empleo, por CI). Port de EmpleoAfiliado.rpt.</summary>
public sealed class AfiliadoEmpleoLinea
{
    private static readonly System.Globalization.CultureInfo EsUy = System.Globalization.CultureInfo.GetCultureInfo("es-UY");

    public long CI { get; set; }
    public int CodEmpresa { get; set; }
    public string? DescEmpresa { get; set; }
    public DateTime? FechaIngreso { get; set; }
    public DateTime? FechaIngCasemed { get; set; }
    public DateTime? FechaBaja { get; set; }
    public int? CodBajaMotivo { get; set; }
    public string? NroFichaEmpresa { get; set; }
    public int IdTrabaja { get; set; }

    public string EmpresaTexto => $"{CodEmpresa} — {DescEmpresa}".Trim().TrimEnd('—', ' ');
    public string FechaIngresoFmt => FechaIngreso is { } f ? f.ToString("dd/MM/yyyy", EsUy) : "";
    public string FechaIngCasemedFmt => FechaIngCasemed is { } f ? f.ToString("dd/MM/yyyy", EsUy) : "";
    public string FechaBajaFmt => FechaBaja is { } f ? f.ToString("dd/MM/yyyy", EsUy) : "";
    public string EstadoTexto => FechaBaja is null ? "Activo" : "Baja";
}

/// <summary>Una línea de los apuntes del afiliado (vista dbo.Rs_AfiliadoApunte, por CI). Port de AfApunte.rpt.</summary>
public sealed class AfiliadoApunteLinea
{
    private static readonly System.Globalization.CultureInfo EsUy = System.Globalization.CultureInfo.GetCultureInfo("es-UY");

    public long CI { get; set; }
    public DateTime? Fecha { get; set; }
    public string? Descrip { get; set; }

    public string FechaFmt => Fecha is { } f ? f.ToString("dd/MM/yyyy", EsUy) : "";
    public string DescripTexto => (Descrip ?? "").Trim();
}

/// <summary>Una línea de los imponibles del afiliado (vista dbo.Rs_Imponible, por CI). Port de Imponible.rpt.</summary>
public sealed class AfiliadoImponibleLinea
{
    private static readonly System.Globalization.CultureInfo EsUy = System.Globalization.CultureInfo.GetCultureInfo("es-UY");

    public long CI { get; set; }
    public int CodEmpresa { get; set; }
    public string? DescEmpresa { get; set; }
    public string? Concepto { get; set; }
    public int DiasTrabajados { get; set; }
    public double Importe { get; set; }
    public int Mes { get; set; }
    public int Anio { get; set; }

    public string PeriodoFmt => Mes >= 1 && Mes <= 12 ? $"{Mes:00}/{Anio}" : Anio.ToString();
    public string EmpresaTexto => (DescEmpresa ?? "").Trim();
    public string ConceptoTexto => (Concepto ?? "").Trim();
    public string ImporteFmt => ((decimal)Importe).ToString("N2", EsUy);
}

/// <summary>Provee los datos de los reportes predefinidos del afiliado desde NewSgpa2.</summary>
public interface IAfiliadoReporteData
{
    Task<AfiliadoDatosData?> GetDatosAsync(long ci, CancellationToken ct = default);
    Task<IReadOnlyList<AfiliadoEmpleoLinea>> GetEmpleosAsync(long ci, CancellationToken ct = default);
    Task<IReadOnlyList<AfiliadoApunteLinea>> GetApuntesAsync(long ci, CancellationToken ct = default);
    Task<IReadOnlyList<AfiliadoImponibleLinea>> GetImponiblesAsync(long ci, CancellationToken ct = default);
}

public sealed class AfiliadoReporteData(IDbExecutor db) : IAfiliadoReporteData
{
    public Task<AfiliadoDatosData?> GetDatosAsync(long ci, CancellationToken ct = default)
        => db.QuerySingleOrDefaultAsync<AfiliadoDatosData>(
            "SELECT TOP 1 * FROM dbo.Rpt_Afiliado WHERE CI = @ci",
            new { ci }, cancellationToken: ct);

    public Task<IReadOnlyList<AfiliadoEmpleoLinea>> GetEmpleosAsync(long ci, CancellationToken ct = default)
        => db.QueryAsync<AfiliadoEmpleoLinea>(
            "SELECT * FROM dbo.Rs_Empleo WHERE CI = @ci ORDER BY FechaIngreso DESC",
            new { ci }, cancellationToken: ct);

    public Task<IReadOnlyList<AfiliadoApunteLinea>> GetApuntesAsync(long ci, CancellationToken ct = default)
        => db.QueryAsync<AfiliadoApunteLinea>(
            "SELECT CI, Fecha, Descrip FROM dbo.Rs_AfiliadoApunte WHERE CI = @ci ORDER BY Fecha DESC",
            new { ci }, cancellationToken: ct);

    public Task<IReadOnlyList<AfiliadoImponibleLinea>> GetImponiblesAsync(long ci, CancellationToken ct = default)
        => db.QueryAsync<AfiliadoImponibleLinea>(
            "SELECT CI, CodEmpresa, DescEmpresa, Concepto, DiasTrabajados, Importe, Mes, Anio FROM dbo.Rs_Imponible WHERE CI = @ci ORDER BY Anio DESC, Mes DESC",
            new { ci }, cancellationToken: ct);
}
