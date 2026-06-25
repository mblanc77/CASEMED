using DevExpress.Data.Filtering;
using Sgpa.Data.Crud;
using Sgpa.Data.Security;
using Sgpa.Domain.Metadata;
using Sgpa.Domain.Security;
using Sgpa.Web.Components.Crud;

namespace Sgpa.Web.Security;

/// <summary>
/// Implementación real de <see cref="ISecurityCriteriaCompiler"/> (capa Web, con DevExpress): parsea el string
/// <c>CriteriaOperator</c> de un permiso por registro y lo traduce al <see cref="FilterNode"/> neutral con el mismo
/// pipeline que los filtros de la grilla (<see cref="SgpaCriteriaTranslator"/> + <see cref="EntityRelations"/>).
/// <para>Antes de parsear, sustituye los placeholders de usuario por el valor real (entre comillas), de modo que un
/// criterio como <c>[Usr] = '{CurrentUserLogin}'</c> limite las filas al login del usuario actual.</para>
/// </summary>
public sealed class SgpaSecurityCriteriaCompiler : ISecurityCriteriaCompiler
{
    public FilterNode? Compile(EntityMetadata meta, string criteria, ICurrentUser user)
    {
        if (string.IsNullOrWhiteSpace(criteria)) return null;

        var resolved = SustituirPlaceholders(criteria, user);
        CriteriaOperator op;
        try { op = CriteriaOperator.Parse(resolved); }
        catch { return null; }   // criterio mal formado → no se aplica (la capa de datos lo trata como fail-safe)

        return SgpaCriteriaTranslator.Translate(op, prefix => EntityRelations.ByPrefix(meta.EntityType, prefix));
    }

    // Reemplaza los placeholders soportados por el login actual, escapando la comilla simple para el literal.
    private static string SustituirPlaceholders(string criteria, ICurrentUser user)
    {
        var login = (user.UserName ?? string.Empty).Replace("'", "''");
        return criteria
            .Replace("{CurrentUserLogin}", login, StringComparison.OrdinalIgnoreCase)
            .Replace("{CurrentUser}", login, StringComparison.OrdinalIgnoreCase);
    }
}
