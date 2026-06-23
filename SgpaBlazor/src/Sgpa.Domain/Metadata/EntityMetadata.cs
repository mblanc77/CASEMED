using System.Collections.Concurrent;
using System.Reflection;

namespace Sgpa.Domain.Metadata;

/// <summary>Descriptor de una columna de la entidad (PK, auditoría, visibilidad, etc.).</summary>
public sealed class ColumnMetadata
{
    public required PropertyInfo Property { get; init; }
    public required string Name { get; init; }
    public required string Caption { get; init; }
    public int Order { get; init; }
    public bool IsKey { get; init; }
    public bool IsIdentity { get; init; }
    public SgpaAuditKind? Audit { get; init; }
    public bool VisibleInList { get; init; }
    public bool VisibleInDetail { get; init; }
    public bool ReadOnly { get; init; }
    /// <summary>Columna traída por el origen de lectura (vista/JOIN) que NO se persiste: fuera de INSERT/UPDATE.</summary>
    public bool Computed { get; init; }
    public string? DisplayFormat { get; init; }
    /// <summary>Columna NOT NULL no autogenerada → obligatoria al editar.</summary>
    public bool Required { get; init; }
    /// <summary>Largo máximo para texto (0 = sin límite / no aplica).</summary>
    public int MaxLength { get; init; }
    public Type ClrType { get; init; } = typeof(object);
    public Type UnderlyingType { get; init; } = typeof(object);

    public bool IsAudit => Audit is not null;

    public object? GetValue(object entity) => Property.GetValue(entity);
    public void SetValue(object entity, object? value) => Property.SetValue(entity, value);
}

/// <summary>
/// Metadata reflexiva y cacheada de una entidad: tabla, clave y columnas.
/// Fuente única para el repositorio genérico (Sgpa.Data) y los componentes CRUD (Sgpa.Web).
/// </summary>
public sealed class EntityMetadata
{
    private static readonly ConcurrentDictionary<Type, EntityMetadata> Cache = new();

    public required Type EntityType { get; init; }
    public required string Table { get; init; }
    public required string Schema { get; init; }
    /// <summary>Origen de lectura (vista) si difiere de la tabla; null = se lee de la propia tabla.</summary>
    public string? ReadSource { get; init; }
    /// <summary>Esquema del origen de lectura.</summary>
    public string ReadSchema { get; init; } = "dbo";
    public required IReadOnlyList<ColumnMetadata> Columns { get; init; }

    /// <summary>Columnas que forman la clave primaria (1 o más). Vacío si la tabla no tiene PK.</summary>
    public required IReadOnlyList<ColumnMetadata> Keys { get; init; }

    /// <summary>Primera columna clave. Conveniencia para entidades de clave simple.</summary>
    public ColumnMetadata Key => Keys.Count > 0
        ? Keys[0]
        : throw new InvalidOperationException($"La entidad {EntityType.Name} no tiene clave primaria.");

    public bool HasCompositeKey => Keys.Count > 1;

    public string QualifiedTable => $"[{Schema}].[{Table}]";

    /// <summary>Origen para las consultas de lectura: la vista declarada con <see cref="SgpaReadSourceAttribute"/>
    /// o, por defecto, la propia tabla. Las escrituras siempre usan <see cref="QualifiedTable"/>.</summary>
    public string QualifiedReadSource => ReadSource is null ? QualifiedTable : $"[{ReadSchema}].[{ReadSource}]";

    public IEnumerable<ColumnMetadata> ListColumns =>
        Columns.Where(c => c.VisibleInList && !c.IsAudit).OrderBy(c => c.Order);

    public IEnumerable<ColumnMetadata> DetailColumns =>
        Columns.Where(c => c.VisibleInDetail && !c.IsAudit).OrderBy(c => c.Order);

    public IEnumerable<ColumnMetadata> AuditColumns => Columns.Where(c => c.IsAudit);

    private ColumnMetadata? _displayColumn;
    private bool _displayResolved;

    /// <summary>
    /// Columna "descriptiva" de la entidad (para mostrar en lookups de FK): prefiere
    /// Descrip/Descripcion/Nombre/Detalle; si no, la primera columna string no clave.
    /// </summary>
    public ColumnMetadata? DisplayColumn
    {
        get
        {
            if (_displayResolved) return _displayColumn;
            _displayColumn =
                Columns.FirstOrDefault(c => !c.IsKey && c.UnderlyingType == typeof(string)
                    && (c.Name.StartsWith("Descrip", StringComparison.OrdinalIgnoreCase)
                        || c.Name.Equals("Nombre", StringComparison.OrdinalIgnoreCase)
                        || c.Name.Equals("Detalle", StringComparison.OrdinalIgnoreCase)))
                ?? Columns.FirstOrDefault(c => !c.IsKey && !c.IsAudit && c.UnderlyingType == typeof(string));
            _displayResolved = true;
            return _displayColumn;
        }
    }

    public static EntityMetadata For<T>() => For(typeof(T));

    public static EntityMetadata For(Type type) => Cache.GetOrAdd(type, Build);

    private static EntityMetadata Build(Type type)
    {
        var table = type.GetCustomAttribute<SgpaTableAttribute>()
            ?? throw new InvalidOperationException($"La entidad {type.Name} no tiene [SgpaTable].");
        var readSource = type.GetCustomAttribute<SgpaReadSourceAttribute>();

        var columns = new List<ColumnMetadata>();

        foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (!prop.CanRead || !prop.CanWrite) continue;
            // Excluir las propiedades de navegación de reportes (referencia a otra entidad o colección):
            // NO son columnas de la tabla. Sin esto, el SELECT del CRUD pediría "Mutualista", "Trabajas", etc.
            if (IsNavigationProperty(prop.PropertyType)) continue;

            var col = prop.GetCustomAttribute<SgpaColumnAttribute>();
            var keyAttr = prop.GetCustomAttribute<SgpaKeyAttribute>();
            var auditAttr = prop.GetCustomAttribute<SgpaAuditAttribute>();
            var underlying = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

            var meta = new ColumnMetadata
            {
                Property = prop,
                Name = col?.Column ?? prop.Name,
                Caption = col?.Caption ?? Humanize(prop.Name),
                Order = col?.Order ?? 1000,
                IsKey = keyAttr is not null,
                IsIdentity = keyAttr?.IsIdentity ?? false,
                Audit = auditAttr?.Kind,
                VisibleInList = col?.VisibleInList ?? auditAttr is null,
                VisibleInDetail = col?.VisibleInDetail ?? auditAttr is null,
                ReadOnly = (col?.ReadOnly ?? false) || (col?.Computed ?? false) || keyAttr is not null,
                Computed = col?.Computed ?? false,
                DisplayFormat = col?.DisplayFormat,
                // NOT NULL detectado por el generador (Required) o por ser un tipo de valor no anulable
                // declarado a mano que no es clave identity.
                Required = (col?.Required ?? false)
                    || (auditAttr is null && (keyAttr?.IsIdentity ?? false) == false
                        && prop.PropertyType.IsValueType
                        && Nullable.GetUnderlyingType(prop.PropertyType) is null),
                MaxLength = col?.MaxLength ?? 0,
                ClrType = prop.PropertyType,
                UnderlyingType = underlying
            };

            columns.Add(meta);
        }

        // La clave puede ser simple o compuesta; se ordena por Order (refleja el orden de columnas).
        var keys = columns.Where(c => c.IsKey).OrderBy(c => c.Order).ToList();

        return new EntityMetadata
        {
            EntityType = type,
            Table = table.Name,
            Schema = table.Schema,
            ReadSource = readSource?.Name,
            ReadSchema = readSource?.Schema ?? "dbo",
            Columns = columns,
            Keys = keys
        };
    }

    // Una nav prop de reportes: colección genérica (List<Entidad>) o referencia a otra entidad ([SgpaTable]).
    private static bool IsNavigationProperty(Type t)
    {
        if (t != typeof(string) && t.IsGenericType
            && typeof(System.Collections.IEnumerable).IsAssignableFrom(t))
            return true;
        var underlying = Nullable.GetUnderlyingType(t) ?? t;
        return underlying.IsClass && underlying.GetCustomAttribute<SgpaTableAttribute>() is not null;
    }

    private static string Humanize(string name) => SpanishCaptions.Humanize(name);
}
