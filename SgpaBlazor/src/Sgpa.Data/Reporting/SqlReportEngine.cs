using System.Globalization;
using System.Text.RegularExpressions;

namespace Sgpa.Data.Reporting;

/// <summary>
/// Motor de los reportes basados en SQL crudo. Responsabilidades:
/// <list type="bullet">
///   <item><see cref="EnsureReadOnly"/>: valida que la consulta sea una <b>única</b> sentencia de sólo lectura
///   (SELECT/WITH), sin palabras peligrosas ni múltiples sentencias.</item>
///   <item><see cref="DetectarTokens"/>: encuentra los tokens <c>@Nombre</c> (los parámetros del reporte).</item>
///   <item><see cref="Sustituir"/>: reemplaza cada token declarado por un <b>literal SQL tipado y escapado</b>
///   del valor ingresado (la query queda como una sola sentencia, sin parámetros de Dapper).</item>
///   <item><see cref="SustituirParaDescribir"/>: reemplaza los tokens por <c>CAST(NULL AS tipo)</c> para poder
///   validar la consulta y listar sus columnas con <c>sp_describe_first_result_set</c>.</item>
/// </list>
/// Como la sustitución produce literales (no parámetros), un token puede ir en cualquier parte del SQL; la
/// seguridad la dan el escapado por tipo + el guard de sólo lectura + que la creación es de administrador.
/// </summary>
public static class SqlReportEngine
{
    private static readonly Regex TokenRx = new(@"@([A-Za-z_][A-Za-z0-9_]*)", RegexOptions.Compiled);

    // Palabras que no pueden aparecer en una consulta de sólo lectura (se chequean sin comentarios, con límites de palabra).
    private static readonly Regex ForbiddenRx = new(
        @"\b(insert|update|delete|drop|alter|create|truncate|exec|execute|merge|grant|revoke|into|waitfor|shutdown|openrowset|opendatasource|openquery|bulk)\b|\bsp_|\bxp_",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    /// <summary>Devuelve null si la consulta es una única sentencia de sólo lectura; si no, el motivo del rechazo.</summary>
    public static string? EnsureReadOnly(string? sql)
    {
        if (string.IsNullOrWhiteSpace(sql)) return "La consulta está vacía.";
        var clean = StripComments(sql).Trim();
        if (clean.Length == 0) return "La consulta está vacía.";

        // Una sola sentencia: no puede haber ';' antes del final.
        var sinFinal = clean.TrimEnd(';', ' ', '\t', '\r', '\n');
        if (sinFinal.Contains(';')) return "La consulta debe ser una única sentencia (sin ';' intermedios).";

        if (!(StartsWithKeyword(sinFinal, "select") || StartsWithKeyword(sinFinal, "with")))
            return "La consulta debe empezar con SELECT (o WITH … SELECT).";

        var m = ForbiddenRx.Match(sinFinal);
        if (m.Success) return $"La consulta sólo puede leer datos; palabra no permitida: «{m.Value.Trim()}».";

        return null;
    }

    /// <summary>Nombres de los tokens <c>@Nombre</c> presentes en la consulta (sin repetir, en orden de aparición).</summary>
    public static IReadOnlyList<string> DetectarTokens(string? sql)
    {
        var res = new List<string>();
        if (string.IsNullOrWhiteSpace(sql)) return res;
        var vistos = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (Match m in TokenRx.Matches(sql))
        {
            var name = m.Groups[1].Value;
            if (vistos.Add(name)) res.Add(name);
        }
        return res;
    }

    /// <summary>Sustituye cada token declarado por el literal SQL del valor ingresado (NULL si falta).</summary>
    public static string Sustituir(string sql, IEnumerable<SqlParamDef> defs, IReadOnlyDictionary<string, object?> valores)
    {
        var lista = defs as IReadOnlyCollection<SqlParamDef> ?? defs.ToList();
        sql = QuitarComillasDeTokens(sql, lista);   // el motor entrecomilla por tipo; sacamos las que pusiera el usuario
        var porNombre = lista.ToDictionary(d => d.Nombre, StringComparer.OrdinalIgnoreCase);
        return TokenRx.Replace(sql, m =>
        {
            var name = m.Groups[1].Value;
            if (!porNombre.TryGetValue(name, out var def)) return m.Value;   // no declarado → variable SQL real, no se toca
            valores.TryGetValue(name, out var v);
            return Literal(def.Tipo, v);
        });
    }

    /// <summary>Sustituye los tokens declarados por <c>CAST(NULL AS tipo)</c> para validar/describir la consulta.</summary>
    public static string SustituirParaDescribir(string sql, IEnumerable<SqlParamDef> defs)
    {
        var lista = defs as IReadOnlyCollection<SqlParamDef> ?? defs.ToList();
        sql = QuitarComillasDeTokens(sql, lista);   // mismo criterio que al ejecutar (consistencia describe/ejecución)
        var porNombre = lista.ToDictionary(d => d.Nombre, StringComparer.OrdinalIgnoreCase);
        return TokenRx.Replace(sql, m =>
        {
            var name = m.Groups[1].Value;
            if (!porNombre.TryGetValue(name, out var def)) return m.Value;
            return $"CAST(NULL AS {SqlType(def.Tipo)})";
        });
    }

    /// <summary>Literal SQL tipado y escapado de un valor (para sustituir un token).</summary>
    public static string Literal(SqlParamTipo tipo, object? value)
    {
        if (value is null || (value is string s0 && s0.Length == 0)) return "NULL";
        switch (tipo)
        {
            case SqlParamTipo.Entero:
                return Convert.ToInt64(value, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
            case SqlParamTipo.Decimal:
                return Convert.ToDecimal(value, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
            case SqlParamTipo.Bool:
                return Convert.ToBoolean(value) ? "1" : "0";
            case SqlParamTipo.Fecha:
                var dt = value is DateTime d ? d : DateTime.Parse(value.ToString()!, CultureInfo.InvariantCulture);
                return "'" + dt.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture) + "'";
            default: // Texto: comillas + escapado de comilla simple, prefijo N para unicode.
                return "N'" + value.ToString()!.Replace("'", "''") + "'";
        }
    }

    /// <summary>Tipo SQL para el <c>CAST(NULL AS …)</c> al describir.</summary>
    public static string SqlType(SqlParamTipo tipo) => tipo switch
    {
        SqlParamTipo.Entero => "bigint",
        SqlParamTipo.Decimal => "decimal(38,10)",
        SqlParamTipo.Bool => "bit",
        SqlParamTipo.Fecha => "datetime2",
        _ => "nvarchar(4000)"
    };

    /// <summary>CLR (FullName) del tipo de parámetro, para <c>ParametrosPrompt</c>/filtros.</summary>
    public static string ClrType(SqlParamTipo tipo) => tipo switch
    {
        SqlParamTipo.Entero => typeof(long).FullName!,
        SqlParamTipo.Decimal => typeof(decimal).FullName!,
        SqlParamTipo.Bool => typeof(bool).FullName!,
        SqlParamTipo.Fecha => typeof(DateTime).FullName!,
        _ => typeof(string).FullName!
    };

    /// <summary>Mapea el <c>system_type_name</c> de SQL Server a un CLR aproximado (para la grilla/filtro avanzado).</summary>
    public static Type ClrFromSqlType(string? systemTypeName)
    {
        var t = (systemTypeName ?? string.Empty).ToLowerInvariant();
        if (t.StartsWith("bit")) return typeof(bool);
        if (t.StartsWith("tinyint") || t.StartsWith("smallint") || t.StartsWith("int")) return typeof(int);
        if (t.StartsWith("bigint")) return typeof(long);
        if (t.StartsWith("decimal") || t.StartsWith("numeric") || t.StartsWith("money") || t.StartsWith("smallmoney")
            || t.StartsWith("float") || t.StartsWith("real")) return typeof(decimal);
        if (t.StartsWith("date") || t.StartsWith("smalldatetime") || t.StartsWith("time")) return typeof(DateTime);
        if (t.StartsWith("uniqueidentifier")) return typeof(Guid);
        return typeof(string);
    }

    /// <summary>Convierte el texto de un default del editor al valor tipado (o null si no parsea).</summary>
    public static object? ParseDefault(SqlParamTipo tipo, string? text)
    {
        if (string.IsNullOrWhiteSpace(text)) return null;
        try
        {
            return tipo switch
            {
                SqlParamTipo.Entero => (object)long.Parse(text, CultureInfo.InvariantCulture),
                SqlParamTipo.Decimal => decimal.Parse(text, CultureInfo.InvariantCulture),
                SqlParamTipo.Bool => bool.Parse(text),
                SqlParamTipo.Fecha => DateTime.Parse(text, CultureInfo.InvariantCulture),
                _ => text
            };
        }
        catch { return null; }
    }

    // Si el usuario escribió el token entre comillas simples ('@Nombre'), las quita: el literal por tipo ya las aporta.
    // Así da igual escribir @Nombre o '@Nombre' (común para parámetros de texto/fecha).
    private static string QuitarComillasDeTokens(string sql, IEnumerable<SqlParamDef> defs)
    {
        foreach (var d in defs)
            sql = Regex.Replace(sql, $@"'\s*@{Regex.Escape(d.Nombre)}(?![A-Za-z0-9_])\s*'", "@" + d.Nombre);
        return sql;
    }

    private static bool StartsWithKeyword(string sql, string kw) =>
        sql.Length >= kw.Length
        && sql.StartsWith(kw, StringComparison.OrdinalIgnoreCase)
        && (sql.Length == kw.Length || !char.IsLetterOrDigit(sql[kw.Length]) && sql[kw.Length] != '_');

    // Quita comentarios -- … y /* … */ para que no puedan ocultar palabras prohibidas ni ';'. Sólo para validar.
    private static string StripComments(string sql)
    {
        sql = Regex.Replace(sql, @"/\*.*?\*/", " ", RegexOptions.Singleline);
        sql = Regex.Replace(sql, @"--[^\r\n]*", " ");
        return sql;
    }
}
