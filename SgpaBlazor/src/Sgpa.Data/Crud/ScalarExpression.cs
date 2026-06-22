namespace Sgpa.Data.Crud;

/// <summary>
/// Árbol de expresión ESCALAR neutral (sin dependencia de DevExpress), análogo a <see cref="FilterNode"/> pero
/// para valores (no predicados). La capa Web traduce el <c>CriteriaOperator</c> de un campo calculado a este árbol;
/// <see cref="ScalarSqlTranslator"/> lo traduce a una expresión SQL parametrizada. Mantiene a Sgpa.Data libre de
/// DevExpress (igual que el resto del pipeline de filtros).
/// </summary>
public abstract record ScalarNode;

/// <summary>Referencia a una columna (su nombre se valida/resuelve contra la metadata en el traductor).</summary>
public sealed record ScalarColumn(string Name) : ScalarNode;

/// <summary>Constante (se emite como parámetro <c>@pN</c>).</summary>
public sealed record ScalarConst(object? Value) : ScalarNode;

public enum ScalarBinOp { Add, Subtract, Multiply, Divide, Modulo }

/// <summary>Operación aritmética binaria.</summary>
public sealed record ScalarBinary(ScalarBinOp Op, ScalarNode Left, ScalarNode Right) : ScalarNode;

/// <summary>Negación aritmética unaria (<c>-x</c>).</summary>
public sealed record ScalarNegate(ScalarNode Operand) : ScalarNode;

public enum ScalarCompareOp { Equal, NotEqual, Greater, Less, GreaterOrEqual, LessOrEqual }

/// <summary>Comparación booleana (sólo válida como condición de <see cref="ScalarFn.Iif"/>).</summary>
public sealed record ScalarCondition(ScalarNode Left, ScalarCompareOp Op, ScalarNode Right) : ScalarNode;

/// <summary>Funciones permitidas (lista blanca) que se mapean a T-SQL.</summary>
public enum ScalarFn
{
    Concat, Coalesce, Iif,
    Substring, Len, Upper, Lower, Trim,
    Abs, Round,
    DateDiffDay, DateDiffMonth, DateDiffYear,
    AddDays, AddMonths, AddYears
}

/// <summary>Llamada a una función de la lista blanca.</summary>
public sealed record ScalarFunc(ScalarFn Fn, IReadOnlyList<ScalarNode> Args) : ScalarNode;
