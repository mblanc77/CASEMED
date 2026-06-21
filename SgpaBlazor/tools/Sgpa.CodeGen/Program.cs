using System.Text;
using Microsoft.Data.SqlClient;

// Generador de entidades POCO para SgpaBlazor a partir del esquema de NewSgpa2.
// Emite una clase por tabla con los atributos de metadata (Sgpa.Domain.Metadata).
// Uso: dotnet run -- [connectionString] [outputDir]

string conn = args.Length > 0 && !string.IsNullOrWhiteSpace(args[0])
    ? args[0]
    : "Data Source=localhost;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;Initial Catalog=NewSgpa2";

string outDir = args.Length > 1 && !string.IsNullOrWhiteSpace(args[1])
    ? args[1]
    : @"C:\Personal\Gestion\CASEMED\SgpaBlazor\src\Sgpa.Domain\Entities\Generated";

const string ns = "Sgpa.Domain.Entities";

Directory.CreateDirectory(outDir);
// Limpia generación previa
foreach (var f in Directory.GetFiles(outDir, "*.cs")) File.Delete(f);

const string query = @"
SELECT s.name AS [Schema], t.name AS [Table], c.name AS [Column], c.column_id,
       ty.name AS SqlType, c.is_nullable, c.is_identity, c.max_length,
       CASE WHEN pkc.column_id IS NOT NULL THEN 1 ELSE 0 END AS IsPk,
       ISNULL(pk.cols, 0) AS PkCols
FROM sys.tables t
JOIN sys.schemas s ON s.schema_id = t.schema_id
JOIN sys.columns c ON c.object_id = t.object_id
JOIN sys.types ty ON ty.user_type_id = c.user_type_id
LEFT JOIN sys.indexes i ON i.object_id = t.object_id AND i.is_primary_key = 1
LEFT JOIN sys.index_columns pkc ON pkc.object_id = t.object_id AND pkc.index_id = i.index_id AND pkc.column_id = c.column_id
LEFT JOIN (SELECT i2.object_id, COUNT(*) cols FROM sys.indexes i2
           JOIN sys.index_columns ic2 ON ic2.object_id = i2.object_id AND ic2.index_id = i2.index_id
           WHERE i2.is_primary_key = 1 GROUP BY i2.object_id) pk ON pk.object_id = t.object_id
WHERE s.name = 'dbo'  -- sólo el esquema de negocio; seg.* tiene su propio modelo en Sgpa.Domain/Security
ORDER BY t.name, c.column_id;";

var tables = new Dictionary<string, TableInfo>();

await using (var cn = new SqlConnection(conn))
{
    await cn.OpenAsync();
    await using var cmd = new SqlCommand(query, cn);
    await using var rd = await cmd.ExecuteReaderAsync();
    while (await rd.ReadAsync())
    {
        var table = rd.GetString(1);
        if (!tables.TryGetValue(table, out var ti))
        {
            ti = new TableInfo(rd.GetString(0), table);
            tables[table] = ti;
        }
        ti.Columns.Add(new ColumnInfo(
            Name: rd.GetString(2),
            Ordinal: rd.GetInt32(3),
            SqlType: rd.GetString(4),
            IsNullable: rd.GetBoolean(5),
            IsIdentity: rd.GetBoolean(6),
            MaxLength: rd.GetInt16(7),
            IsPk: rd.GetInt32(8) == 1,
            PkCols: rd.GetInt32(9)));
    }
}

int simple = 0, composite = 0, none = 0;
foreach (var ti in tables.Values)
{
    var pkCols = ti.Columns.Where(c => c.IsPk).ToList();
    if (pkCols.Count == 1) simple++;
    else if (pkCols.Count > 1) composite++;
    else none++;

    File.WriteAllText(Path.Combine(outDir, Ident(ti.Table) + ".cs"), Emit(ti, pkCols), Encoding.UTF8);
}

Console.WriteLine($"Generadas {tables.Count} entidades en {outDir}");
Console.WriteLine($"  PK simple: {simple} | PK compuesta: {composite} | sin PK: {none}");

string Emit(TableInfo ti, List<ColumnInfo> pkCols)
{
    var className = Ident(ti.Table);
    var sb = new StringBuilder();
    sb.AppendLine("using Sgpa.Domain.Metadata;");
    sb.AppendLine();
    sb.AppendLine($"namespace {ns};");
    sb.AppendLine();
    sb.AppendLine("/// <summary>Entidad generada desde NewSgpa2. NO editar a mano (regenerar con Sgpa.CodeGen).</summary>");

    if (pkCols.Count > 1)
        sb.AppendLine($"// Clave compuesta: {string.Join(", ", pkCols.Select(c => c.Name))}.");
    else if (pkCols.Count == 0)
        sb.AppendLine("// Sin clave primaria — POCO de sólo lectura/consulta (no apto para SgpaCrudView).");

    var schemaArg = ti.Schema == "dbo" ? "" : $", Schema = \"{ti.Schema}\"";
    sb.AppendLine($"[SgpaTable(\"{ti.Table}\"{schemaArg})]");
    sb.AppendLine($"public partial class {className}"); // partial: permite agregar propiedades de navegación para reportes (ObjectDataSource)
    sb.AppendLine("{");

    foreach (var c in ti.Columns)
    {
        var prop = PropName(c.Name);
        if (prop == className) prop += "_"; // una propiedad no puede llamarse igual que su clase
        var clr = ClrType(c.SqlType, c.IsNullable);
        var attrs = new List<string>();

        // Auditoría por convención de nombre (Usr / Ts)
        if (string.Equals(c.Name, "Usr", StringComparison.OrdinalIgnoreCase))
            attrs.Add("[SgpaAudit(Kind = SgpaAuditKind.User)]");
        else if (string.Equals(c.Name, "Ts", StringComparison.OrdinalIgnoreCase))
            attrs.Add("[SgpaAudit(Kind = SgpaAuditKind.Timestamp)]");
        else
        {
            var colArgs = new List<string> { $"Order = {c.Ordinal}" };
            if (!NameMatches(prop, c.Name)) colArgs.Insert(0, $"Column = \"{c.Name}\"");
            // NOT NULL no autogenerada → requerida (la PK identity la completa la BD).
            if (!c.IsNullable && !c.IsIdentity) colArgs.Add("Required = true");
            var maxLen = CharLength(c.SqlType, c.MaxLength);
            if (maxLen > 0) colArgs.Add($"MaxLength = {maxLen}");
            attrs.Add($"[SgpaColumn({string.Join(", ", colArgs)})]");
        }

        if (c.IsPk)
            attrs.Add(pkCols.Count == 1 && c.IsIdentity ? "[SgpaKey(IsIdentity = true)]" : "[SgpaKey]");

        foreach (var a in attrs) sb.AppendLine($"    {a}");
        sb.AppendLine($"    public {clr} {prop} {{ get; set; }}");
        sb.AppendLine();
    }

    sb.AppendLine("}");
    return sb.ToString();
}

// Largo en caracteres para tipos de texto acotados; 0 = sin límite o no aplica.
static int CharLength(string sqlType, short maxLength)
{
    switch (sqlType.ToLowerInvariant())
    {
        case "nvarchar":
        case "nchar":
            return maxLength > 0 ? maxLength / 2 : 0; // max_length viene en bytes; -1 = MAX
        case "varchar":
        case "char":
            return maxLength > 0 ? maxLength : 0;     // -1 = MAX
        default:
            return 0; // text/ntext/xml y no-texto: sin límite explícito
    }
}

static string ClrType(string sqlType, bool nullable)
{
    var (baseType, isValue) = sqlType.ToLowerInvariant() switch
    {
        "int" => ("int", true),
        "bigint" => ("long", true),
        "smallint" => ("short", true),
        "tinyint" => ("byte", true),
        "bit" => ("bool", true),
        "decimal" or "numeric" or "money" or "smallmoney" => ("decimal", true),
        "float" => ("double", true),
        "real" => ("float", true),
        "datetime" or "datetime2" or "date" or "smalldatetime" => ("DateTime", true),
        "datetimeoffset" => ("DateTimeOffset", true),
        "time" => ("TimeSpan", true),
        "uniqueidentifier" => ("Guid", true),
        "varbinary" or "binary" or "image" or "timestamp" or "rowversion" => ("byte[]", false),
        _ => ("string", false) // nvarchar, varchar, char, nchar, text, ntext, xml, sysname...
    };
    // Tipos por valor respetan nullability; referencias siempre anulables (evita CS8618).
    return isValue ? (nullable ? baseType + "?" : baseType) : baseType + "?";
}

static string PropName(string column)
{
    var id = Ident(column);
    return id;
}

static bool NameMatches(string prop, string column)
    => string.Equals(prop.TrimStart('@'), column, StringComparison.Ordinal);

static string Ident(string raw)
{
    var sb = new StringBuilder(raw.Length);
    foreach (var ch in raw)
        sb.Append(char.IsLetterOrDigit(ch) || ch == '_' ? ch : '_');
    var s = sb.ToString();
    if (s.Length == 0) s = "_";
    if (char.IsDigit(s[0])) s = "_" + s;
    return Kw.Set.Contains(s) ? "@" + s : s;
}

static class Kw
{
    public static readonly HashSet<string> Set = new(StringComparer.Ordinal)
    {
        "abstract","as","base","bool","break","byte","case","catch","char","checked","class","const","continue",
        "decimal","default","delegate","do","double","else","enum","event","explicit","extern","false","finally",
        "fixed","float","for","foreach","goto","if","implicit","in","int","interface","internal","is","lock","long",
        "namespace","new","null","object","operator","out","override","params","private","protected","public",
        "readonly","ref","return","sbyte","sealed","short","sizeof","stackalloc","static","string","struct","switch",
        "this","throw","true","try","typeof","uint","ulong","unchecked","unsafe","ushort","using","virtual","void",
        "volatile","while"
    };
}

record TableInfo(string Schema, string Table)
{
    public List<ColumnInfo> Columns { get; } = new();
}

record ColumnInfo(string Name, int Ordinal, string SqlType, bool IsNullable, bool IsIdentity, short MaxLength, bool IsPk, int PkCols);
