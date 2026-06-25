namespace Sgpa.Data.Configuracion;

/// <summary>Settings dinámicos de una tabla, consolidados en un solo lugar (dbo.TablaConfig).</summary>
/// <param name="EdicionInline">Catálogo editable inline (EditRow) en vez de popup.</param>
/// <param name="ConfirmarBorrado">Eliminar exige tipear "ELIMINAR".</param>
/// <param name="Auditar">Auditar cambios por campo (alta/modificación/baja).</param>
/// <param name="Alias">Nombre amigable de la tabla para el usuario final (opcional).</param>
/// <param name="DisponibleReportes">¿Disponible para el motor de reportes? null = usar el default heurístico.</param>
public sealed record TablaConfig(bool EdicionInline, bool ConfirmarBorrado, bool Auditar, string? Alias = null, bool? DisponibleReportes = null)
{
    /// <summary>Default para tablas sin fila: no inline, confirma borrado, no audita, sin alias.</summary>
    public static readonly TablaConfig Default = new(EdicionInline: false, ConfirmarBorrado: true, Auditar: false);
}

/// <summary>
/// Configuración por tabla, cacheada en memoria para acceso síncrono (la usan el CRUD genérico en render
/// y el <c>DapperCrudService</c> en cada operación). Llamá <see cref="EnsureLoadedAsync"/> una vez antes de leer.
/// </summary>
public interface ITablaConfigService
{
    /// <summary>Carga la cache desde la base la primera vez (idempotente).</summary>
    Task EnsureLoadedAsync(CancellationToken ct = default);

    /// <summary>Config de una tabla (sync, desde cache). Default si no está configurada.</summary>
    TablaConfig Get(string tabla);

    /// <summary>Nombre amigable de la tabla: el alias configurado, o el nombre técnico sin el prefijo SP_.</summary>
    string DisplayName(string tabla);

    /// <summary>¿La tabla está disponible para reportes? Override explícito si existe; si no, default heurístico.</summary>
    bool DisponibleReportes(string tabla);

    /// <summary>Toda la config explícita (las filas guardadas).</summary>
    IReadOnlyDictionary<string, TablaConfig> All { get; }

    /// <summary>Guarda (upsert) la config de una tabla y refresca la cache.</summary>
    Task SetAsync(string tabla, TablaConfig config, CancellationToken ct = default);

    /// <summary>Guarda (upsert) varias tablas en una sola conexión/transacción y refresca la cache.</summary>
    Task SetManyAsync(IReadOnlyCollection<KeyValuePair<string, TablaConfig>> items, CancellationToken ct = default);
}
