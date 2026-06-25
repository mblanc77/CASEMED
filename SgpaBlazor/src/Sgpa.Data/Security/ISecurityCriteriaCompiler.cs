using Sgpa.Data.Crud;
using Sgpa.Domain.Metadata;
using Sgpa.Domain.Security;

namespace Sgpa.Data.Security;

/// <summary>
/// Compila un criterio de seguridad a nivel de registro (string <c>CriteriaOperator</c> de DevExpress) al árbol
/// neutral <see cref="FilterNode"/> que la capa de datos sabe traducir a SQL (<see cref="FilterSqlTranslator"/>).
/// La implementación real vive en la capa Web (depende de DevExpress + <c>SgpaCriteriaTranslator</c>); en tests y
/// consola se registra un no-op. Sustituye placeholders de usuario (ej. <c>CurrentUserLogin</c>) antes de traducir.
/// </summary>
public interface ISecurityCriteriaCompiler
{
    /// <summary>Compila <paramref name="criteria"/> para la entidad <paramref name="meta"/>; null si no se puede traducir.</summary>
    FilterNode? Compile(EntityMetadata meta, string criteria, ICurrentUser user);
}

/// <summary>No-op: no compila ningún criterio (no hay DevExpress). Para tests/consola sin la capa Web.</summary>
public sealed class NoopSecurityCriteriaCompiler : ISecurityCriteriaCompiler
{
    public FilterNode? Compile(EntityMetadata meta, string criteria, ICurrentUser user) => null;
}
