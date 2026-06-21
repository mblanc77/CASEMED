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
        foreach (var col in meta.DetailColumns)
        {
            // No se valida lo que el usuario no puede editar (read-only, o claves no editables fuera del alta).
            if (col.IsAudit) continue;
            if (col.ReadOnly && !(col.IsKey && isNew)) continue;
            if (col.IsKey && col.IsIdentity) continue;

            var value = col.GetValue(model);

            if (col.Required && IsEmpty(value))
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

    private static bool IsEmpty(object? value) =>
        value is null || (value is string s && string.IsNullOrWhiteSpace(s));
}
