using System.Text.Json;

namespace Sgpa.Data.Reporting;

/// <summary>Cabecera de un reporte SQL (para listar/menú).</summary>
public sealed record ReporteSqlInfo(int Id, string Nombre, bool SoloAdmin, DateTime Fecha, string? Login);

/// <summary>Reporte SQL completo: cabecera + definición.</summary>
public sealed record ReporteSql(int Id, string Nombre, ReporteSqlDef Def);

/// <summary>Una columna de salida descripta por SQL Server (nombre + tipo).</summary>
public sealed record SqlColumnInfo(string Name, string? SystemTypeName, Type ClrType);

/// <summary>Persistencia + validación/descripción de los reportes basados en SQL crudo (tabla <c>dbo.ReporteSql</c>).</summary>
public interface IReporteSqlService
{
    Task<IReadOnlyList<ReporteSqlInfo>> AllAsync(CancellationToken ct = default);
    Task<ReporteSql?> GetAsync(int id, CancellationToken ct = default);
    Task<int> CreateAsync(string nombre, ReporteSqlDef def, string login, CancellationToken ct = default);
    Task UpdateAsync(int id, string nombre, ReporteSqlDef def, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);

    /// <summary>
    /// Valida (sólo lectura) y describe la consulta: devuelve sus columnas de salida. Sustituye los tokens declarados
    /// por <c>CAST(NULL AS tipo)</c> para que SQL Server pueda analizarla sin ejecutarla. Lanza si el SQL es inválido.
    /// </summary>
    Task<IReadOnlyList<SqlColumnInfo>> DescribirAsync(string sql, IReadOnlyList<SqlParamDef> defs, CancellationToken ct = default);
}

public sealed class DapperReporteSqlService : IReporteSqlService
{
    private static readonly JsonSerializerOptions Json = new(JsonSerializerDefaults.Web)
    {
        Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
    };

    private readonly IDbExecutor _db;
    public DapperReporteSqlService(IDbExecutor db) => _db = db;

    public Task<IReadOnlyList<ReporteSqlInfo>> AllAsync(CancellationToken ct = default)
        => _db.QueryAsync<ReporteSqlInfo>(
            "SELECT Id, Nombre, SoloAdmin, Fecha, Login FROM dbo.ReporteSql WHERE Activo = 1 ORDER BY Nombre",
            cancellationToken: ct);

    public async Task<ReporteSql?> GetAsync(int id, CancellationToken ct = default)
    {
        var row = await _db.QuerySingleOrDefaultAsync<Row>(
            "SELECT Id, Nombre, DefJson FROM dbo.ReporteSql WHERE Id = @id AND Activo = 1",
            new { id }, cancellationToken: ct).ConfigureAwait(false);
        if (row is null) return null;
        var def = JsonSerializer.Deserialize<ReporteSqlDef>(row.DefJson, Json) ?? new ReporteSqlDef();
        return new ReporteSql(row.Id, row.Nombre, def);
    }

    public Task<int> CreateAsync(string nombre, ReporteSqlDef def, string login, CancellationToken ct = default)
        => _db.ExecuteScalarAsync<int>(
            @"INSERT INTO dbo.ReporteSql (Nombre, DefJson, SoloAdmin, Activo, Login, Fecha)
              VALUES (@nombre, @defJson, @soloAdmin, 1, @login, SYSDATETIME());
              SELECT CAST(SCOPE_IDENTITY() AS int);",
            new { nombre, defJson = JsonSerializer.Serialize(def, Json), soloAdmin = def.SoloAdmin, login },
            cancellationToken: ct);

    public Task UpdateAsync(int id, string nombre, ReporteSqlDef def, CancellationToken ct = default)
        => _db.ExecuteAsync(
            @"UPDATE dbo.ReporteSql
              SET Nombre = @nombre, DefJson = @defJson, SoloAdmin = @soloAdmin, Fecha = SYSDATETIME()
              WHERE Id = @id",
            new { id, nombre, defJson = JsonSerializer.Serialize(def, Json), soloAdmin = def.SoloAdmin },
            cancellationToken: ct);

    public Task DeleteAsync(int id, CancellationToken ct = default)
        => _db.ExecuteAsync("UPDATE dbo.ReporteSql SET Activo = 0 WHERE Id = @id", new { id }, cancellationToken: ct);

    public async Task<IReadOnlyList<SqlColumnInfo>> DescribirAsync(string sql, IReadOnlyList<SqlParamDef> defs, CancellationToken ct = default)
    {
        var error = SqlReportEngine.EnsureReadOnly(sql);
        if (error is not null) throw new InvalidOperationException(error);

        var tsql = SqlReportEngine.SustituirParaDescribir(sql, defs);
        // El DMV analiza la consulta sin ejecutarla y devuelve sus columnas; si el SQL es inválido, lanza.
        var rows = await _db.QueryAsync<DescribeRow>(
            @"SELECT name AS Name, system_type_name AS SystemTypeName, is_hidden AS IsHidden, column_ordinal AS Ordinal
              FROM sys.dm_exec_describe_first_result_set(@tsql, NULL, 0)
              ORDER BY column_ordinal",
            new { tsql }, cancellationToken: ct).ConfigureAwait(false);

        return rows
            .Where(r => !r.IsHidden && !string.IsNullOrEmpty(r.Name))
            .Select(r => new SqlColumnInfo(r.Name!, r.SystemTypeName, SqlReportEngine.ClrFromSqlType(r.SystemTypeName)))
            .ToList();
    }

    private sealed record Row(int Id, string Nombre, string DefJson);
    private sealed record DescribeRow(string? Name, string? SystemTypeName, bool IsHidden, int Ordinal);
}
