namespace Sgpa.Domain.Metadata;

/// <summary>Un error de validación sobre una columna concreta.</summary>
public readonly record struct ValidationError(ColumnMetadata Column, string Message);

/// <summary>
/// Validación genérica dirigida por metadata: requeridos (NOT NULL) y largo máximo de texto.
/// Pura y testeable; el componente <c>SgpaMetadataValidator</c> la conecta al EditContext de Blazor.
/// </summary>
public static class MetadataValidation
{
    /// <summary>Valida un modelo contra su metadata. <paramref name="isNew"/> habilita validar la PK manual.</summary>
    public static IReadOnlyList<ValidationError> Validate(EntityMetadata meta, object model, bool isNew = false)
    {
        var errors = new List<ValidationError>();
        var nameCol = meta.NameColumn;
        foreach (var col in meta.DetailColumns)
        {
            // No se valida lo que el usuario no puede editar (read-only, o claves no editables fuera del alta).
            if (col.IsAudit) continue;
            if (col.ReadOnly && !(col.IsKey && isNew)) continue;
            if (col.IsKey && col.IsIdentity) continue;

            var value = col.GetValue(model);

            // La columna nombre/descripción de las tablas catálogo se exige aunque el esquema legado la deje NULL
            // (paridad con el VB6, donde no tenía sentido un código sin descripción).
            var required = col.Required || ReferenceEquals(col, nameCol);

            // Una clave NATURAL compuesta (ej. Prestacion = CI+Fecha+Tipo) la elige siempre el usuario: un valor por
            // defecto (0 / 0001-01-01) significa "sin elegir". No se aplica a claves simples, que suelen ser códigos
            // autoasignados donde 0 puede ser legítimo durante el alta.
            var defaultIsEmpty = col.IsKey && meta.HasCompositeKey;

            if (required && IsEmpty(value, defaultIsEmpty))
            {
                errors.Add(new ValidationError(col, $"«{col.Caption}» es obligatorio."));
                continue; // sin valor no tiene sentido seguir validando esta columna
            }

            if (col.MaxLength > 0 && value is string s && s.Length > col.MaxLength)
                errors.Add(new ValidationError(col,
                    $"«{col.Caption}» no puede superar {col.MaxLength} caracteres (tiene {s.Length})."));
        }
        return errors;
    }

    private static bool IsEmpty(object? value, bool defaultIsEmpty = false)
    {
        if (value is null) return true;
        if (value is string s) return string.IsNullOrWhiteSpace(s);
        // Para claves naturales: un value-type en su valor por defecto (0, default(DateTime), etc.) = "sin elegir".
        return defaultIsEmpty && value.GetType().IsValueType
            && value.Equals(Activator.CreateInstance(value.GetType()));
    }
}
