namespace Sgpa.Domain.Metadata;

/// <summary>
/// Heurística: ¿una tabla es candidata <b>por defecto</b> a estar disponible para reportes?
/// La base NewSgpa2 (migrada de Access/VB6) tiene muchas tablas temporales, vinculadas a archivos externos,
/// de XAF/seguridad o de trabajo. Esto pre-marca las que tienen pinta de entidad de negocio; es sólo el
/// DEFAULT — el admin lo override en "Configuración de tablas" (campo DisponibleReportes).
/// </summary>
public static class ReportableTables
{
    private static readonly string[] XafPrefixes = { "PermissionPolicy", "ModelDifference", "XP" };

    private static readonly HashSet<string> XafExact = new(StringComparer.OrdinalIgnoreCase)
    {
        "AuditData", "AuditEFCoreWeakReferences", "AuditedObjectWeakReference",
        "FileData", "DashboardData", "ReportDataV2", "HCategories",
    };

    public static bool IsDefault(EntityMetadata m)
    {
        var t = m.Table;
        var cn = m.EntityType.Name;

        if (m.Keys.Count == 0) return false;                                  // sin PK no sirve como root/relación
        if (cn.StartsWith('_') || (t.Length > 0 && char.IsDigit(t[0]))) return false;  // _NNN_ (reportes VB6)
        if (cn.Length > 0 && char.IsLower(cn[0])) return false;              // working tables (qCorregir, xLiq, zRs)
        if (t.Length > 1 && t.Where(char.IsLetter).All(char.IsUpper)) return false;    // imports TODO-MAYÚSCULAS (BROU, IMP)
        if (XafExact.Contains(t)) return false;
        if (XafPrefixes.Any(p => t.StartsWith(p, StringComparison.Ordinal))) return false;

        return t.IndexOf("tmp", StringComparison.OrdinalIgnoreCase) < 0
            && t.IndexOf("rpt", StringComparison.OrdinalIgnoreCase) < 0
            && t.IndexOf("temp", StringComparison.OrdinalIgnoreCase) < 0
            && t.IndexOf("backup", StringComparison.OrdinalIgnoreCase) < 0;
    }
}
