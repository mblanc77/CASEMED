namespace Sgpa.Domain.Metadata;

/// <summary>Mapea una entidad a su tabla en NewSgpa2.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class SgpaTableAttribute : Attribute
{
    public SgpaTableAttribute(string name) => Name = name;
    public string Name { get; }
    public string Schema { get; set; } = "dbo";
}

/// <summary>Marca la propiedad como clave primaria.</summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class SgpaKeyAttribute : Attribute
{
    /// <summary>True si la BD genera el valor (IDENTITY). Banco usa clave manual → false.</summary>
    public bool IsIdentity { get; set; }
}

/// <summary>Configura cómo se muestra/edita la columna (estilo XAF model).</summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class SgpaColumnAttribute : Attribute
{
    /// <summary>Nombre físico de la columna en la BD si difiere del nombre de la propiedad.</summary>
    public string? Column { get; set; }
    public string? Caption { get; set; }
    public int Order { get; set; } = 1000;
    public bool VisibleInList { get; set; } = true;
    public bool VisibleInDetail { get; set; } = true;
    public bool ReadOnly { get; set; }
    public string? DisplayFormat { get; set; }
    /// <summary>Columna NOT NULL (sin contar identity/autogeneradas) → obligatoria en el alta/edición.</summary>
    public bool Required { get; set; }
    /// <summary>Largo máximo para columnas de texto (0 = sin límite / no aplica).</summary>
    public int MaxLength { get; set; }
}

/// <summary>
/// Marca una propiedad como auditoría auto-gestionada (usuario / timestamp).
/// Se completa sola al guardar y se oculta de la UI. Equivale a las columnas
/// Usr/Ts presentes en las tablas migradas.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class SgpaAuditAttribute : Attribute
{
    public SgpaAuditKind Kind { get; set; } = SgpaAuditKind.User;
}

public enum SgpaAuditKind
{
    User,
    Timestamp
}
