using Sgpa.Domain.Metadata;

namespace Sgpa.Domain.Entities;

/// <summary>
/// Campo calculado definido por el administrador para una tabla: una expresión (sintaxis CriteriaOperator de
/// DevExpress) reutilizable en cualquier consulta de esa tabla (reportes dinámicos, filtros, ListViews). Hecha a
/// mano (NO generada) para administrarse con el CRUD genérico en <c>/crud/CampoCalculado</c>. La traducción de la
/// expresión a SQL la hace <c>ScalarSqlTranslator</c> sobre el árbol neutral producido en la capa Web.
/// </summary>
[SgpaTable("CampoCalculado")]
public partial class CampoCalculado
{
    [SgpaColumn(Order = 1)]
    [SgpaKey(IsIdentity = true)]
    public int Id { get; set; }

    [SgpaColumn(Order = 2, Caption = "Tabla", Required = true, MaxLength = 128)]
    public string Tabla { get; set; } = string.Empty;

    [SgpaColumn(Order = 3, Caption = "Nombre del campo", Required = true, MaxLength = 128)]
    public string Nombre { get; set; } = string.Empty;

    [SgpaColumn(Order = 4, Caption = "Encabezado", MaxLength = 200)]
    public string? Caption { get; set; }

    [SgpaColumn(Order = 5, Caption = "Expresión", Required = true, VisibleInList = false)]
    public string Expr { get; set; } = string.Empty;

    [SgpaColumn(Order = 6, Caption = "Tipo de resultado", Required = true, MaxLength = 20)]
    public string TipoResultado { get; set; } = "decimal";

    [SgpaColumn(Order = 7, Caption = "Formato", MaxLength = 50)]
    public string? DisplayFormat { get; set; }

    [SgpaColumn(Order = 8, Caption = "Activo")]
    public bool Activo { get; set; } = true;

    [SgpaAudit(Kind = SgpaAuditKind.User)]
    public string? Usr { get; set; }

    [SgpaAudit(Kind = SgpaAuditKind.Timestamp)]
    public DateTime? Ts { get; set; }
}
