namespace Sgpa.Data.Crud;

/// <summary>
/// Campo calculado de una tabla, en forma NEUTRAL (lista para la capa de datos): su expresión ya traducida al árbol
/// <see cref="ScalarNode"/> (la conversión desde <c>CriteriaOperator</c> la hace la capa Web). Lo consumen el builder
/// de reportes y el CRUD genérico para emitir <c>(&lt;sql&gt;) AS [Nombre]</c> y para filtrar/ordenar por el cálculo.
/// </summary>
/// <param name="Tabla">Tabla/entidad dueña del campo.</param>
/// <param name="Nombre">Nombre del campo (alias de salida, único por tabla).</param>
/// <param name="Caption">Encabezado a mostrar.</param>
/// <param name="Expr">Expresión escalar neutral.</param>
/// <param name="ClrType">Tipo de resultado (para formato de grilla e inferencia de tipo de parámetro).</param>
/// <param name="DisplayFormat">Formato opcional.</param>
public sealed record CalculatedField(
    string Tabla, string Nombre, string Caption, ScalarNode Expr, Type ClrType, string? DisplayFormat);
