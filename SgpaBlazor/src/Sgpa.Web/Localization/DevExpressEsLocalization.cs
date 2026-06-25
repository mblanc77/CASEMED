using DevExpress.Blazor.Localization;
using DevExpress.Utils.Localization;

namespace Sgpa.Web.Localization;

/// <summary>
/// Completa al español las cadenas de DevExpress Blazor que el paquete satélite "es" deja SIN traducir.
/// El caso más visible es todo el <c>FilterBuilder</c> (operadores, grupos Y/O, "Agregar condición/grupo", etc.):
/// de las ~90 claves del generador de filtros, el satélite de la v25.2 no traduce ninguna, por lo que caen al
/// inglés neutro sin importar la cultura.
///
/// DevExpress resuelve estos textos vía <see cref="XtraLocalizer"/> (no un ResourceManager común). Su único punto
/// de extensión global y estable entre versiones es el evento <see cref="XtraLocalizer.QueryLocalizedString"/>:
/// se dispara en cada resolución (con caché posterior) y permite fijar <c>e.Value</c> para sobrescribir. Sólo
/// tocamos las claves de nuestro diccionario; el resto (incluidas las que el satélite SÍ traduce) queda intacto.
/// </summary>
public static class DevExpressEsLocalization
{
    private static int _installed;

    /// <summary>Engancha el override. Idempotente; basta llamarlo una vez al arranque.</summary>
    public static void Install()
    {
        if (System.Threading.Interlocked.Exchange(ref _installed, 1) == 1) return;
        XtraLocalizer.QueryLocalizedString += OnQueryLocalizedString;
        // CLAVE en Blazor Server: por defecto el evento sólo se dispara en el hilo que lo suscribió (el de arranque).
        // El render ocurre en hilos del circuito/pool; sin esto, el primer hilo que resuelva una cadena la cachea en
        // inglés para todos y el override nunca se aplica. Esto hace que el handler valga para TODOS los hilos.
        XtraLocalizer.HandleRequestsFromAllThreads();
    }

    private static void OnQueryLocalizedString(object? sender, XtraLocalizer.QueryLocalizedStringEventArgs e)
    {
        // Sólo cadenas de DevExpress.Blazor y sólo cuando la cultura pedida es español (la app corre es-UY).
        if (e.StringIDType != typeof(DxBlazorStringId)) return;
        if (!string.IsNullOrEmpty(e.Culture) && !e.Culture.StartsWith("es", StringComparison.OrdinalIgnoreCase)) return;
        if (e.StringID is DxBlazorStringId id && Traducciones.TryGetValue(id, out var es))
            e.Value = es;
    }

    // Traducciones de las cadenas que el satélite "es" no cubre. Redactadas según el estilo del propio español de
    // DevExpress (clase FilterUIElementLocalizer, que sí viene traducida): "Es igual a", "Contiene", "Entre", etc.
    private static readonly IReadOnlyDictionary<DxBlazorStringId, string> Traducciones = new Dictionary<DxBlazorStringId, string>
    {
        // --- Estructura del generador de filtros ---
        [DxBlazorStringId.FilterBuilder_Add_Condition] = "Agregar condición",
        [DxBlazorStringId.FilterBuilder_Add_Group] = "Agregar grupo",
        [DxBlazorStringId.FilterBuilder_Enter_Value] = "Ingresar un valor",
        [DxBlazorStringId.FilterBuilder_Values_And] = "y",
        [DxBlazorStringId.FilterBuilder_Group_And] = "Y",
        [DxBlazorStringId.FilterBuilder_Group_Or] = "O",
        [DxBlazorStringId.FilterBuilder_Group_NotAnd] = "No Y",
        [DxBlazorStringId.FilterBuilder_Group_NotOr] = "No O",
        [DxBlazorStringId.FilterBuilder_OperatorsGroup_BasicComparison] = "Comparación básica",
        [DxBlazorStringId.FilterBuilder_OperatorsGroup_DateRanges] = "Rangos de fechas",
        [DxBlazorStringId.FilterBuilder_InvalidFieldError] = "Propiedad no válida '{0}'",
        [DxBlazorStringId.FilterBuilder_InvalidCustomFunctionError] = "Función no válida '{0}'",
        [DxBlazorStringId.FilterBuilder_DisplayExpressionAsTreeErrorTitle] = "No se puede mostrar el editor de filtros interactivo",
        [DxBlazorStringId.FilterBuilder_DisplayExpressionAsTreeErrorCaption] = "Se aplicó una condición de filtro no soportada o no válida",

        // --- Operadores: comparación / texto ---
        [DxBlazorStringId.FilterBuilder_OperatorType_Equals] = "Es igual a",
        [DxBlazorStringId.FilterBuilder_OperatorType_DoesNotEqual] = "No es igual a",
        [DxBlazorStringId.FilterBuilder_OperatorType_Greater] = "Es mayor que",
        [DxBlazorStringId.FilterBuilder_OperatorType_GreaterOrEqual] = "Es mayor o igual que",
        [DxBlazorStringId.FilterBuilder_OperatorType_Less] = "Es menor que",
        [DxBlazorStringId.FilterBuilder_OperatorType_LessOrEqual] = "Es menor o igual que",
        [DxBlazorStringId.FilterBuilder_OperatorType_Between] = "Está entre",
        [DxBlazorStringId.FilterBuilder_OperatorType_NotBetween] = "No está entre",
        [DxBlazorStringId.FilterBuilder_OperatorType_Contains] = "Contiene",
        [DxBlazorStringId.FilterBuilder_OperatorType_DoesNotContain] = "No contiene",
        [DxBlazorStringId.FilterBuilder_OperatorType_BeginsWith] = "Comienza con",
        [DxBlazorStringId.FilterBuilder_OperatorType_EndsWith] = "Termina con",
        [DxBlazorStringId.FilterBuilder_OperatorType_Like] = "Es como",
        [DxBlazorStringId.FilterBuilder_OperatorType_NotLike] = "No es como",
        [DxBlazorStringId.FilterBuilder_OperatorType_AnyOf] = "Es alguno de",
        [DxBlazorStringId.FilterBuilder_OperatorType_NoneOf] = "No es ninguno de",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsNull] = "Está vacío",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsNotNull] = "No está vacío",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsNullOrEmpty] = "Está vacío",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsNotNullOrEmpty] = "No está vacío",
        [DxBlazorStringId.FilterBuilder_OperatorType_Exists] = "Existe",

        // --- Operadores: agregados sobre colecciones ---
        [DxBlazorStringId.FilterBuilder_OperatorType_Count] = "Cantidad",
        [DxBlazorStringId.FilterBuilder_OperatorType_Sum] = "Suma",
        [DxBlazorStringId.FilterBuilder_OperatorType_Avg] = "Promedio",
        [DxBlazorStringId.FilterBuilder_OperatorType_Min] = "Mínimo",
        [DxBlazorStringId.FilterBuilder_OperatorType_Max] = "Máximo",

        // --- Operadores: rangos de fecha relativos ---
        [DxBlazorStringId.FilterBuilder_OperatorType_IsToday] = "Es hoy",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsYesterday] = "Es ayer",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsTomorrow] = "Es mañana",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsThisWeek] = "Es esta semana",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsThisMonth] = "Es este mes",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsThisYear] = "Es este año",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsLastWeek] = "Es la semana pasada",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsLastMonth] = "Es el mes pasado",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsLastYear] = "Es el año pasado",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsNextWeek] = "Es la próxima semana",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsNextMonth] = "Es el próximo mes",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsNextYear] = "Es el próximo año",
        [DxBlazorStringId.FilterBuilder_OperatorType_InDateRange] = "Está en el rango de fechas",
        [DxBlazorStringId.FilterBuilder_OperatorType_NotInDateRange] = "Está fuera del rango de fechas",
        [DxBlazorStringId.FilterBuilder_OperatorType_InTimeRange] = "Está en el rango de hora",
        [DxBlazorStringId.FilterBuilder_OperatorType_OutOfTimeRange] = "Está fuera del rango de hora",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsBeyondThisYear] = "Es posterior a este año",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsPriorThisYear] = "Es anterior a este año",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsYearToDate] = "Está en el período del año hasta la fecha",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsSameDay] = "Es la misma fecha que",

        // --- Operadores: meses ---
        [DxBlazorStringId.FilterBuilder_OperatorType_IsJanuary] = "Es enero",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsFebruary] = "Es febrero",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsMarch] = "Es marzo",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsApril] = "Es abril",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsMay] = "Es mayo",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsJune] = "Es junio",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsJuly] = "Es julio",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsAugust] = "Es agosto",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsSeptember] = "Es septiembre",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsOctober] = "Es octubre",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsNovember] = "Es noviembre",
        [DxBlazorStringId.FilterBuilder_OperatorType_IsDecember] = "Es diciembre",

        // --- Generador de filtros del grid (encabezado / botones) ---
        [DxBlazorStringId.Grid_FilterBuilder_HeaderText] = "Generador de filtros",
        [DxBlazorStringId.Grid_FilterBuilder_ApplyButton] = "Aplicar",
        [DxBlazorStringId.Grid_FilterBuilder_CancelButton] = "Cancelar",

        // --- Menú contextual del grid (clic derecho en encabezado) ---
        [DxBlazorStringId.Grid_ContextMenu_SortColumnAscending] = "Orden ascendente",
        [DxBlazorStringId.Grid_ContextMenu_SortColumnDescending] = "Orden descendente",
        [DxBlazorStringId.Grid_ContextMenu_ClearColumnSorting] = "Quitar ordenamiento",
        [DxBlazorStringId.Grid_ContextMenu_GroupByColumn] = "Agrupar por esta columna",
        [DxBlazorStringId.Grid_ContextMenu_UngroupColumn] = "Desagrupar columna",
        [DxBlazorStringId.Grid_ContextMenu_ClearGrouping] = "Quitar agrupamiento",
        [DxBlazorStringId.Grid_ContextMenu_ShowGroupPanel] = "Mostrar panel de agrupamiento",
        [DxBlazorStringId.Grid_ContextMenu_HideGroupPanel] = "Ocultar panel de agrupamiento",
        [DxBlazorStringId.Grid_ContextMenu_ShowColumnChooser] = "Selector de columnas",
        [DxBlazorStringId.Grid_ContextMenu_HideColumn] = "Ocultar columna",
        [DxBlazorStringId.Grid_ContextMenu_AutoFitAll] = "Autoajustar todas las columnas",
        [DxBlazorStringId.Grid_ContextMenu_ExpandAll] = "Expandir todo",
        [DxBlazorStringId.Grid_ContextMenu_CollapseAll] = "Contraer todo",
        [DxBlazorStringId.Grid_ContextMenu_ShowFilterBuilder] = "Generador de filtros",

        // --- Otros textos del grid sin traducir en el satélite ---
        [DxBlazorStringId.Grid_FilterPanel_CreateFilter] = "Crear filtro",
        [DxBlazorStringId.Grid_FilterPanel_Operators_Not] = "No",
        [DxBlazorStringId.Grid_Editing_NewItemRowText] = "Haga clic aquí para agregar una fila",
        [DxBlazorStringId.Grid_DragHint_RowsCount] = "{0} filas",
    };
}
