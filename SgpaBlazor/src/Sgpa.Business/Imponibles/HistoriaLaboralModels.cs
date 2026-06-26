namespace Sgpa.Business.Imponibles;

/// <summary>Un archivo de nómina ATYR subido (nombre + líneas), tal cual llega del operador.</summary>
public sealed record AtyrArchivo(string FileName, IReadOnlyList<string> Lineas);

/// <summary>REGISTRO TIPO 1 — Empresa (datos del contribuyente declarante).</summary>
public sealed record AtyrEmpresa(
    string? TipoDeclaracion, string? Version, string? Aplicacion, string? NroEmpresa,
    string? NroContribuyente, int? TipoAportacion, string? Denominacion, string? Domicilio, string? Telefono);

/// <summary>REGISTRO TIPO 4 — Cabezal de nómina (define el período mmaaaa y los totales).</summary>
public sealed record AtyrCabezal(
    string? MesCargo, int? TipoContribuyente, double? Monto, int? FormaRealizacionObra, int? ActividadPrincipal);

/// <summary>REGISTRO TIPO 5 — Persona (identificación de la persona declarada).</summary>
public sealed record AtyrPersona(
    int? PaisDocumento, string? TipoDocumento, string? NroDocumento, long? CI,
    string? PrimerApellido, string? SegundoApellido, string? PrimerNombre, string? SegundoNombre,
    DateTime? FechaNacimiento, int? Sexo, int? Nacionalidad);

/// <summary>REGISTRO TIPO 6 — Actividad (relación empresa/persona del período).</summary>
public sealed record AtyrActividad(
    int? PaisDocumento, string? TipoDocumento, string? NroDocumento, long? CI,
    int? AcumulacionLaboral, DateTime? FechaIngreso, int? TipoRemuneracion, int? HorasSemanales,
    int? VinculoFuncional, int? CodExoneracion, int? ComputosEspeciales, int? Categoria, int? CajaActividad,
    string? AsignacionFamiliar, int? DiasTrabajados, int? HorasTrabajadas, int? SeguroSalud,
    int? CausalEgreso, DateTime? FechaEgreso);

/// <summary>REGISTRO TIPO 7 — Remuneración (conceptos y montos imponibles por persona).</summary>
public sealed record AtyrRemuneracion(
    int? PaisDocumento, string? TipoDocumento, string? NroDocumento, long? CI,
    int? AcumulacionLaboral, int? Concepto, double? Remuneracion, double? Jornal, double? OtrosHaberes);

/// <summary>
/// Resultado de parsear (y mergear) uno o más archivos ATYR V2 de una misma declaración. El período
/// (<see cref="Mes"/>/<see cref="Anio"/>) se deriva del cabezal (tipo 4). Los registros de cabecera
/// (empresa/cabezal) quedan deduplicados al primero; las personas/actividades/remuneraciones, completas.
/// </summary>
public sealed record ParsedAtyr(
    int Mes, int Anio,
    IReadOnlyList<AtyrEmpresa> Empresas,
    IReadOnlyList<AtyrCabezal> Cabezales,
    IReadOnlyList<AtyrPersona> Personas,
    IReadOnlyList<AtyrActividad> Actividades,
    IReadOnlyList<AtyrRemuneracion> Remuneraciones);

/// <summary>Conteos de lo cargado por una corrida de la Historia Laboral.</summary>
public sealed record HistoriaLaboralResult(
    int CodEmpresa, int Mes, int Anio,
    int Empresas, int Cabezales, int Personas, int Actividades, int Remuneraciones,
    int ImponiblesInsertados);
