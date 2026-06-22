using DevExpress.Data.Filtering;
using Sgpa.Data.Crud;

namespace Sgpa.Web.Components.Crud;

/// <summary>
/// Traduce una expresión ESCALAR de DevExpress (<see cref="CriteriaOperator"/>, lo que escribe el admin en un campo
/// calculado) al árbol neutral <see cref="ScalarNode"/> que consume <see cref="ScalarSqlTranslator"/> en la capa de
/// datos. Mantiene a Sgpa.Data libre de DevExpress (igual que <see cref="SgpaCriteriaTranslator"/> para los filtros).
/// Lo no soportado lanza <see cref="NotSupportedException"/> para que la autoría lo detecte.
/// </summary>
public static class SgpaScalarTranslator
{
    public static ScalarNode Translate(CriteriaOperator? op) => op switch
    {
        OperandProperty p => ParseColumn(p.PropertyName),
        OperandValue v => new ScalarConst(v.Value),
        UnaryOperator u when u.OperatorType == UnaryOperatorType.Minus => new ScalarNegate(Translate(u.Operand)),
        BinaryOperator b => TranslateBinary(b),
        FunctionOperator f => TranslateFunction(f),
        null => throw new NotSupportedException("Expresión vacía."),
        _ => throw new NotSupportedException($"Operador no soportado en un campo calculado: {op.GetType().Name}.")
    };

    private static ScalarColumn ParseColumn(string name)
    {
        // v1: sólo columnas de la propia tabla (sin navegación por FK dentro de la fórmula).
        if (name.Contains('.'))
            throw new NotSupportedException($"Las referencias por relación ('{name}') no están soportadas en un campo calculado (v1).");
        return new ScalarColumn(name);
    }

    private static ScalarNode TranslateBinary(BinaryOperator b)
    {
        // Aritmética → ScalarBinary; comparación → ScalarCondition (válida como condición de Iif).
        var arith = b.OperatorType switch
        {
            BinaryOperatorType.Plus => (ScalarBinOp?)ScalarBinOp.Add,
            BinaryOperatorType.Minus => ScalarBinOp.Subtract,
            BinaryOperatorType.Multiply => ScalarBinOp.Multiply,
            BinaryOperatorType.Divide => ScalarBinOp.Divide,
            BinaryOperatorType.Modulo => ScalarBinOp.Modulo,
            _ => null
        };
        if (arith is not null)
            return new ScalarBinary(arith.Value, Translate(b.LeftOperand), Translate(b.RightOperand));

        var cmp = b.OperatorType switch
        {
            BinaryOperatorType.Equal => (ScalarCompareOp?)ScalarCompareOp.Equal,
            BinaryOperatorType.NotEqual => ScalarCompareOp.NotEqual,
            BinaryOperatorType.Greater => ScalarCompareOp.Greater,
            BinaryOperatorType.Less => ScalarCompareOp.Less,
            BinaryOperatorType.GreaterOrEqual => ScalarCompareOp.GreaterOrEqual,
            BinaryOperatorType.LessOrEqual => ScalarCompareOp.LessOrEqual,
            _ => null
        };
        if (cmp is not null)
            return new ScalarCondition(Translate(b.LeftOperand), cmp.Value, Translate(b.RightOperand));

        throw new NotSupportedException($"Operador binario no soportado: {b.OperatorType}.");
    }

    private static ScalarNode TranslateFunction(FunctionOperator f)
    {
        var args = f.Operands.Select(Translate).ToList();
        var fn = f.OperatorType switch
        {
            FunctionOperatorType.Iif => ScalarFn.Iif,
            FunctionOperatorType.Concat => ScalarFn.Concat,
            FunctionOperatorType.IsNull => ScalarFn.Coalesce,   // IsNull(a,b) → COALESCE
            FunctionOperatorType.Substring => ScalarFn.Substring,
            FunctionOperatorType.Len => ScalarFn.Len,
            FunctionOperatorType.Upper => ScalarFn.Upper,
            FunctionOperatorType.Lower => ScalarFn.Lower,
            FunctionOperatorType.Trim => ScalarFn.Trim,
            FunctionOperatorType.Abs => ScalarFn.Abs,
            FunctionOperatorType.Round => ScalarFn.Round,
            FunctionOperatorType.AddDays => ScalarFn.AddDays,
            FunctionOperatorType.AddMonths => ScalarFn.AddMonths,
            FunctionOperatorType.AddYears => ScalarFn.AddYears,
            FunctionOperatorType.DateDiffDay => ScalarFn.DateDiffDay,
            FunctionOperatorType.DateDiffMonth => ScalarFn.DateDiffMonth,
            FunctionOperatorType.DateDiffYear => ScalarFn.DateDiffYear,
            _ => throw new NotSupportedException($"Función no soportada en un campo calculado: {f.OperatorType}.")
        };
        // Coalesce de 2 argumentos: IsNull en DevExpress con 1 arg es chequeo de nulo (booleano), no aplica acá.
        if (fn == ScalarFn.Coalesce && args.Count < 2)
            throw new NotSupportedException("IsNull en un campo calculado requiere 2 argumentos: IsNull(valor, alternativo).");
        return new ScalarFunc(fn, args);
    }
}
