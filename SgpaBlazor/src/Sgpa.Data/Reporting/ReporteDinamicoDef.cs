using Sgpa.Data.Crud;

namespace Sgpa.Data.Reporting;

/// <summary>
/// Referencia a un campo de un reporte dinámico: la cadena de columnas FK desde la raíz (<see cref="Path"/>) más la
/// columna terminal (<see cref="Column"/>). <c>Path</c> vacío = columna de la tabla raíz; <c>["CodMutualista"]</c> =
/// <c>Mutualista.&lt;Column&gt;</c> (un nivel); soporta N niveles "hacia arriba" (relaciones N-1).
/// </summary>
/// <param name="Path">Columnas FK a recorrer desde la raíz (orden raíz→destino). Cada salto se resuelve por convención
/// con <c>EntityCatalog.LookupDisplayTargetFor</c>.</param>
/// <param name="Column">Columna terminal (en la entidad destino del último salto, o en la raíz si <c>Path</c> es vacío).</param>
public sealed record ReportFieldRef(
    string[] Path,
    string Column,
    string? Caption = null,
    string? DisplayFormat = null,
    int? Width = null)
{
    public string[] Path { get; init; } = Path ?? System.Array.Empty<string>();

    /// <summary>
    /// True si <see cref="Column"/> es el nombre de un <b>campo calculado</b> de la tabla (no una columna real).
    /// En v1 sólo se soportan campos calculados de la tabla raíz (<see cref="Path"/> vacío).
    /// </summary>
    public bool Calc { get; init; }
}

/// <summary>Subtotal/total de un reporte: el campo a agregar y la función (Sum/Count/Avg/Min/Max).</summary>
public sealed record ReportSummary(ReportFieldRef Field, AggKind Agg);

/// <summary>
/// Definición (serializable a JSON) de un reporte dinámico creado por el administrador. Se guarda en
/// <c>dbo.ReporteDinamico.DefJson</c>. El filtro reusa el modelo de <c>dbo.SgpaFiltro</c>: <b>un solo</b>
/// <see cref="Criteria"/> (DevExpress CriteriaOperator) donde las hojas no parametrizadas quedan fijas y las
/// parametrizadas (con <c>OperandParameter</c>) se piden al ejecutar — sus defs van en <see cref="ParametrosJson"/>
/// (formato de <c>FiltroParametros.SerializarDefs</c>, opaco para esta capa).
/// </summary>
public sealed class ReporteDinamicoDef
{
    /// <summary>Nombre de la tabla/entidad raíz (resoluble con <c>EntityCatalog.TryGet</c>).</summary>
    public string RootTable { get; set; } = string.Empty;

    /// <summary>Columnas seleccionadas, en orden de visualización.</summary>
    public List<ReportFieldRef> Columns { get; set; } = new();

    /// <summary>Criterio único (CriteriaOperator.ToString()); null/'' = sin filtro. Puede contener parámetros.</summary>
    public string? Criteria { get; set; }

    /// <summary>JSON de defs de parámetros (de <c>FiltroParametros.SerializarDefs</c>); null/'' = sin parámetros.</summary>
    public string? ParametrosJson { get; set; }

    /// <summary>Campos por los que agrupar (en orden); vacío = sin agrupamiento.</summary>
    public List<ReportFieldRef> Groups { get; set; } = new();

    /// <summary>Subtotales/totales a calcular.</summary>
    public List<ReportSummary> Summaries { get; set; } = new();
}
