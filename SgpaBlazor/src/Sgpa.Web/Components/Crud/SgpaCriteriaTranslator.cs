using DevExpress.Data.Filtering;
using Sgpa.Data.Crud;

namespace Sgpa.Web.Components.Crud;

/// <summary>
/// Traduce el <see cref="CriteriaOperator"/> que arma el DxGrid (fila de filtros, menú de filtro,
/// caja de búsqueda) al árbol neutral <see cref="FilterNode"/> que entiende la capa de datos.
/// Los casos no soportados se ignoran (devuelven null) para no romper la grilla.
/// </summary>
public static class SgpaCriteriaTranslator
{
    public static FilterNode? Translate(CriteriaOperator? op)
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
                    var n = Translate(child);
                    if (n is not null) nodes.Add(n);
                }
                if (nodes.Count == 0) return null;
                return new FilterGroup(g.OperatorType == GroupOperatorType.And, nodes);
            }

            case UnaryOperator u when u.OperatorType == UnaryOperatorType.Not:
            {
                var inner = Translate(u.Operand);
                return inner is null ? null : new FilterNot(inner);
            }

            case UnaryOperator u when u.OperatorType == UnaryOperatorType.IsNull:
            {
                var col = PropName(u.Operand);
                return col is null ? null : new FilterNull(col, IsNull: true);
            }

            case InOperator inOp:
            {
                var col = PropName(inOp.LeftOperand);
                if (col is null) return null;
                var values = inOp.Operands.Select(ValueOf).ToList();
                return new FilterIn(col, values);
            }

            case BetweenOperator bt:
            {
                var col = PropName(bt.TestExpression);
                if (col is null) return null;
                return new FilterGroup(And: true, new FilterNode[]
                {
                    new FilterCompare(col, FilterOp.GreaterOrEqual, ValueOf(bt.BeginExpression)),
                    new FilterCompare(col, FilterOp.LessOrEqual, ValueOf(bt.EndExpression)),
                });
            }

            case FunctionOperator f:
                return TranslateFunction(f);

            case BinaryOperator b:
            {
                var (col, value) = PropAndValue(b.LeftOperand, b.RightOperand);
                if (col is null) return null;
#pragma warning disable CS0618 // BinaryOperatorType.Like obsoleto; el grid moderno usa funciones, pero lo mapeamos por compatibilidad.
                var fop = b.OperatorType switch
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
                return fop is null ? null : new FilterCompare(col, fop.Value, value);
            }

            default:
                return null; // operador no soportado → se ignora
        }
    }

    private static FilterNode? TranslateFunction(FunctionOperator f)
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
                return new FilterText(col, func, val);
            }
            case FunctionOperatorType.IsNullOrEmpty:
            {
                if (ops.Count < 1) return null;
                var col = PropName(ops[0]);
                if (col is null) return null;
                return new FilterGroup(And: false, new FilterNode[]
                {
                    new FilterNull(col, IsNull: true),
                    new FilterCompare(col, FilterOp.Equal, string.Empty),
                });
            }
            default:
                return null;
        }
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
