namespace Sgpa.Data.Reporting;

/// <summary>
/// Tipos de parámetro soportados por los reportes SQL. Determinan el control del prompt, el literal SQL al sustituir
/// y el CLR usado por <c>ParametrosPrompt</c>.
/// </summary>
public enum SqlParamTipo { Texto, Entero, Decimal, Fecha, Bool }

/// <summary>Un parámetro (token <c>@Nombre</c>) de un reporte SQL: cómo se pide y de qué tipo es.</summary>
public sealed record SqlParamDef(string Nombre, string? Caption, SqlParamTipo Tipo, string? DefaultText, bool Requerido = true);

/// <summary>Personalización de una columna de salida del SELECT (encabezado, formato, visibilidad).</summary>
public sealed record SqlColumnDef(string Nombre, string? Caption, string? DisplayFormat, bool Visible = true);

/// <summary>
/// Definición de un reporte basado en SQL crudo: una única consulta <c>SELECT</c> con tokens <c>@Nombre</c> que se
/// piden al abrir y se sustituyen por literales tipados/escapados antes de ejecutar. Se persiste como JSON en
/// <c>dbo.ReporteSql</c>.
/// </summary>
public sealed class ReporteSqlDef
{
    /// <summary>La consulta (una sola sentencia SELECT/WITH), con tokens <c>@Nombre</c> para los parámetros.</summary>
    public string Sql { get; set; } = string.Empty;

    /// <summary>Parámetros declarados (uno por token usado).</summary>
    public List<SqlParamDef> Parametros { get; set; } = new();

    /// <summary>Columnas de salida con su personalización (orden = orden de visualización).</summary>
    public List<SqlColumnDef> Columnas { get; set; } = new();

    /// <summary>Si es true, sólo administradores pueden ejecutarlo; si es false, cualquier usuario autenticado.</summary>
    public bool SoloAdmin { get; set; } = true;

    /// <summary>Tope de filas a traer (defensa ante consultas muy grandes).</summary>
    public int MaxFilas { get; set; } = 5000;
}
