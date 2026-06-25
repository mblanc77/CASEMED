using DevExpress.Data.Filtering;
using Sgpa.Data.Crud;

namespace Sgpa.Web.Components.Crud;

/// <summary>
/// Traduce el <see cref="CriteriaOperator"/> que arma el DxGrid / DxFilterBuilder (fila de filtros, menú de
/// filtro, caja de búsqueda, filtro avanzado) al árbol neutral <see cref="FilterNode"/> de la capa de datos.
/// Los casos no soportados se ignoran (devuelven null) para no romper la grilla.
/// <para>Soporta filtros sobre relaciones 1-N (EXISTS): vía el operando <c>Aggregate/Exists</c> del builder,
/// o vía campos con prefijo de colección (ej. <c>Empleos.CodEmpresa</c>) resueltos por <paramref name="relations"/>.</para>
/// </summary>
public static class SgpaCriteriaTranslator
{
    public static FilterNode? Translate(CriteriaOperator? op, Func<string, ExistsRelation?>? relations = null)
    {
        switch (op)
        {
            case null:
                return null;

            case GroupOperator g:
            {
                var nodes = new List<FilterNode>();
                foreach (var child in g.Operands)
                {
                    var n = Translate(child, relations);
                    if (n is not null) nodes.Add(n);
                }
                if (nodes.Count == 0) return null;
                return new FilterGroup(g.OperatorType == GroupOperatorType.And, nodes);
            }

            case UnaryOperator u when u.OperatorType == UnaryOperatorType.Not:
            {
                var inner = Translate(u.Operand, relations);
                return inner is null ? null : new FilterNot(inner);
            }

            case UnaryOperator u when u.OperatorType == UnaryOperatorType.IsNull:
            {
                var col = PropName(u.Operand);
                return Leaf(col, lc => new FilterNull(lc, IsNull: true), relations);
            }

            // Operando agregado del builder: [Coleccion][Exists(condición)] → FilterExists.
            case AggregateOperand agg:
            {
                var coll = agg.CollectionProperty is OperandProperty cp ? cp.PropertyName : null;
                if (coll is null || relations is null) return null;
                var rel = relations(coll);
                if (rel is null || agg.AggregateType != Aggregate.Exists) return null;   // sólo Exists por ahora
                var inner = Translate(agg.Condition, relations);                          // condición sobre la hija
                return new FilterExists(rel.Child, rel.ChildFkColumn, rel.ParentKeyColumn, inner);
            }

            case InOperator inOp:
            {
                var col = PropName(inOp.LeftOperand);
                if (col is null) return null;
                var values = inOp.Operands.Select(ValueOf).ToList();
                return Leaf(col, lc => new FilterIn(lc, values), relations);
            }

            case BetweenOperator bt:
            {
                var col = PropName(bt.TestExpression);
                if (col is null) return null;
                return Leaf(col, lc => new FilterGroup(And: true, new FilterNode[]
                {
                    new FilterCompare(lc, FilterOp.GreaterOrEqual, ValueOf(bt.BeginExpression)),
                    new FilterCompare(lc, FilterOp.LessOrEqual, ValueOf(bt.EndExpression)),
                }), relations);
            }

            case FunctionOperator f:
                return TranslateFunction(f, relations);

            case BinaryOperator b:
            {
                var fop = MapBinaryOp(b.OperatorType);
                // Comparación contra un agregado de colección (Certificaciones.Count() > 1, Imponibles.Sum(Importe) >= X).
                // El agregado puede estar a cualquier lado del comparador (si está a la derecha, se invierte el operador).
                if (fop is not null && b.LeftOperand is AggregateOperand aggL)
                    return AggregateCompare(aggL, fop.Value, ValueOf(b.RightOperand), relations);
                if (fop is not null && b.RightOperand is AggregateOperand aggR)
                    return AggregateCompare(aggR, Flip(fop.Value), ValueOf(b.LeftOperand), relations);

                // Comparación campo ↔ campo de la MISMA tabla (ambos operandos son columnas sin prefijo de relación):
                // ej. [FechaBaja] >= [FechaAlta]. Las rutas con punto (relación/padre) no se soportan por esta vía.
                if (fop is not null && b.LeftOperand is OperandProperty lp && b.RightOperand is OperandProperty rp
                    && lp.PropertyName.IndexOf('.') < 0 && rp.PropertyName.IndexOf('.') < 0)
                    return new FilterCompareColumns(lp.PropertyName, fop.Value, rp.PropertyName);

                var (col, value) = PropAndValue(b.LeftOperand, b.RightOperand);
                if (col is null) return null;
                return fop is null ? null : Leaf(col, lc => new FilterCompare(lc, fop.Value, value), relations);
            }

            default:
                return null; // operador no soportado → se ignora
        }
    }

    private static FilterNode? TranslateFunction(FunctionOperator f, Func<string, ExistsRelation?>? relations)
    {
        var ops = f.Operands;
        switch (f.OperatorType)
        {
            case FunctionOperatorType.StartsWith:
            case FunctionOperatorType.EndsWith:
            case FunctionOperatorType.Contains:
            {
                if (ops.Count < 2) return null;
                var col = PropName(ops[0]);
                var val = ValueOf(ops[1])?.ToString();
                if (col is null || val is null) return null;
                var func = f.OperatorType switch
                {
                    FunctionOperatorType.StartsWith => FilterFunc.StartsWith,
                    FunctionOperatorType.EndsWith => FilterFunc.EndsWith,
                    _ => FilterFunc.Contains
                };
                return Leaf(col, lc => new FilterText(lc, func, val), relations);
            }
            case FunctionOperatorType.IsNullOrEmpty:
            {
                if (ops.Count < 1) return null;
                var col = PropName(ops[0]);
                if (col is null) return null;
                return Leaf(col, lc => new FilterGroup(And: false, new FilterNode[]
                {
                    new FilterNull(lc, IsNull: true),
                    new FilterCompare(lc, FilterOp.Equal, string.Empty),
                }), relations);
            }
            default:
                return null;
        }
    }

    // Construye un nodo hoja a partir del nombre de propiedad. Si el nombre viene con prefijo de colección
    // conocido (ej. "Empleos.CodEmpresa"), envuelve la hoja (sobre la columna local "CodEmpresa") en un
    // FilterExists hacia la tabla hija; si no, la hoja se construye sobre la columna tal cual.
    private static FilterNode? Leaf(string? propName, Func<string, FilterNode?> build, Func<string, ExistsRelation?>? relations)
    {
        if (propName is null) return null;
        var dot = propName.IndexOf('.');
        if (dot > 0 && relations is not null)
        {
            var rel = relations(propName[..dot]);
            if (rel is not null)
            {
                var inner = build(propName[(dot + 1)..]);
                return inner is null ? null : new FilterExists(rel.Child, rel.ChildFkColumn, rel.ParentKeyColumn, inner);
            }
        }
        return build(propName);
    }

    // Comparación contra un agregado de colección → FilterAggregate. value = el otro lado del comparador.
    private static FilterNode? AggregateCompare(AggregateOperand agg, FilterOp op, object? value, Func<string, ExistsRelation?>? relations)
    {
        if (value is null) return null;
        var coll = agg.CollectionProperty is OperandProperty cp ? cp.PropertyName : null;
        if (coll is null || relations is null) return null;
        var rel = relations(coll);
        if (rel is null) return null;
        var kind = agg.AggregateType switch
        {
            Aggregate.Count => AggKind.Count,
            Aggregate.Sum => AggKind.Sum,
            Aggregate.Min => AggKind.Min,
            Aggregate.Max => AggKind.Max,
            Aggregate.Avg => AggKind.Avg,
            _ => (AggKind?)null   // Exists/Single/etc. no por esta vía
        };
        if (kind is null) return null;
        var aggCol = kind == AggKind.Count ? null
            : (agg.AggregatedExpression is OperandProperty ap ? ap.PropertyName : null);
        if (kind != AggKind.Count && aggCol is null) return null;   // Sum/Min/Max/Avg requieren columna
        var inner = Translate(agg.Condition, relations);
        return new FilterAggregate(rel.Child, rel.ChildFkColumn, rel.ParentKeyColumn, kind.Value, aggCol, inner, op, value);
    }

    private static FilterOp Flip(FilterOp op) => op switch
    {
        FilterOp.Greater => FilterOp.Less,
        FilterOp.Less => FilterOp.Greater,
        FilterOp.GreaterOrEqual => FilterOp.LessOrEqual,
        FilterOp.LessOrEqual => FilterOp.GreaterOrEqual,
        _ => op   // Equal / NotEqual / Like son simétricos
    };

    private static FilterOp? MapBinaryOp(BinaryOperatorType t)
    {
#pragma warning disable CS0618 // BinaryOperatorType.Like obsoleto; el grid moderno usa funciones, pero lo mapeamos por compatibilidad.
        return t switch
        {
            BinaryOperatorType.Equal => FilterOp.Equal,
            BinaryOperatorType.NotEqual => FilterOp.NotEqual,
            BinaryOperatorType.Greater => FilterOp.Greater,
            BinaryOperatorType.Less => FilterOp.Less,
            BinaryOperatorType.GreaterOrEqual => FilterOp.GreaterOrEqual,
            BinaryOperatorType.LessOrEqual => FilterOp.LessOrEqual,
            BinaryOperatorType.Like => FilterOp.Like,
            _ => (FilterOp?)null
        };
#pragma warning restore CS0618
    }

    private static string? PropName(CriteriaOperator? op) =>
        op is OperandProperty p ? p.PropertyName : null;

    private static object? ValueOf(CriteriaOperator? op) =>
        op is OperandValue v ? v.Value : null;

    // En un binario, la columna puede estar a izquierda o derecha; devuelve (columna, valor).
    private static (string? Column, object? Value) PropAndValue(CriteriaOperator left, CriteriaOperator right)
    {
        if (PropName(left) is { } lc) return (lc, ValueOf(right));
        if (PropName(right) is { } rc) return (rc, ValueOf(left));
        return (null, null);
    }
}
