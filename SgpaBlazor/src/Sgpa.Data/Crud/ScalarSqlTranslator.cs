using Dapper;

namespace Sgpa.Data.Crud;

/// <summary>
/// Traduce un <see cref="ScalarNode"/> (expresión escalar neutral de un campo calculado) a una expresión SQL
/// parametrizada. Las columnas se resuelven/validan a través de <paramref name="resolveColumn"/> que aporta el
/// consumidor (CRUD genérico: <c>[col]</c> desnudo; builder de reportes: <c>alias.[col]</c>), garantizando que el
/// nombre exista en la metadata (anti-inyección). Las constantes van como parámetros; las funciones, por lista blanca.
/// </summary>
public static class ScalarSqlTranslator
{
    /// <summary>Traduce <paramref name="node"/> a SQL; <paramref name="n"/> numera los parámetros (compartido con el WHERE).</summary>
    public static string Translate(ScalarNode node, Func<string, string> resolveColumn, DynamicParameters p, ref int n)
    {
        switch (node)
        {
            case ScalarColumn c:
                return resolveColumn(c.Name);   // el resolver valida contra metadata y aplica alias/quoting

            case ScalarConst k:
            {
                var pn = "@p" + n++;
                p.Add(pn, k.Value);
                return pn;
            }

            case ScalarNegate neg:
                return $"(-{Translate(neg.Operand, resolveColumn, p, ref n)})";

            case ScalarBinary b:
            {
                var l = Translate(b.Left, resolveColumn, p, ref n);
                var r = Translate(b.Right, resolveColumn, p, ref n);
                var op = b.Op switch
                {
                    ScalarBinOp.Add => "+",
                    ScalarBinOp.Subtract => "-",
                    ScalarBinOp.Multiply => "*",
                    ScalarBinOp.Divide => "/",
                    ScalarBinOp.Modulo => "%",
                    _ => throw new NotSupportedException($"Operador no soportado: {b.Op}")
                };
                return $"({l} {op} {r})";
            }

            case ScalarCondition cond:
            {
                var l = Translate(cond.Left, resolveColumn, p, ref n);
                var r = Translate(cond.Right, resolveColumn, p, ref n);
                var op = cond.Op switch
                {
                    ScalarCompareOp.Equal => "=",
                    ScalarCompareOp.NotEqual => "<>",
                    ScalarCompareOp.Greater => ">",
                    ScalarCompareOp.Less => "<",
                    ScalarCompareOp.GreaterOrEqual => ">=",
                    ScalarCompareOp.LessOrEqual => "<=",
                    _ => throw new NotSupportedException($"Comparación no soportada: {cond.Op}")
                };
                return $"({l} {op} {r})";
            }

            case ScalarFunc f:
                return TranslateFunc(f, resolveColumn, p, ref n);

            default:
                throw new NotSupportedException($"Nodo escalar no soportado: {node.GetType().Name}");
        }
    }

    private static string TranslateFunc(ScalarFunc f, Func<string, string> resolveColumn, DynamicParameters p, ref int n)
    {
        // Traduce cada argumento (en orden, para numerar parámetros de forma estable).
        var a = new string[f.Args.Count];
        for (int i = 0; i < f.Args.Count; i++)
            a[i] = Translate(f.Args[i], resolveColumn, p, ref n);

        void Need(int count)
        {
            if (a.Length != count)
                throw new ArgumentException($"La función {f.Fn} requiere {count} argumento(s); recibió {a.Length}.");
        }

        switch (f.Fn)
        {
            case ScalarFn.Concat:
                if (a.Length < 2) throw new ArgumentException("Concat requiere al menos 2 argumentos.");
                return $"CONCAT({string.Join(", ", a)})";
            case ScalarFn.Coalesce:
                if (a.Length < 2) throw new ArgumentException("Coalesce requiere al menos 2 argumentos.");
                return $"COALESCE({string.Join(", ", a)})";
            case ScalarFn.Iif:
                Need(3);
                return $"(CASE WHEN {a[0]} THEN {a[1]} ELSE {a[2]} END)";
            case ScalarFn.Substring:
                Need(3);
                return $"SUBSTRING({a[0]}, {a[1]}, {a[2]})";
            case ScalarFn.Len:
                Need(1); return $"LEN({a[0]})";
            case ScalarFn.Upper:
                Need(1); return $"UPPER({a[0]})";
            case ScalarFn.Lower:
                Need(1); return $"LOWER({a[0]})";
            case ScalarFn.Trim:
                Need(1); return $"LTRIM(RTRIM({a[0]}))";
            case ScalarFn.Abs:
                Need(1); return $"ABS({a[0]})";
            case ScalarFn.Round:
                Need(2); return $"ROUND({a[0]}, {a[1]})";
            case ScalarFn.DateDiffDay:
                Need(2); return $"DATEDIFF(day, {a[0]}, {a[1]})";
            case ScalarFn.DateDiffMonth:
                Need(2); return $"DATEDIFF(month, {a[0]}, {a[1]})";
            case ScalarFn.DateDiffYear:
                Need(2); return $"DATEDIFF(year, {a[0]}, {a[1]})";
            case ScalarFn.AddDays:
                Need(2); return $"DATEADD(day, {a[1]}, {a[0]})";
            case ScalarFn.AddMonths:
                Need(2); return $"DATEADD(month, {a[1]}, {a[0]})";
            case ScalarFn.AddYears:
                Need(2); return $"DATEADD(year, {a[1]}, {a[0]})";
            default:
                throw new NotSupportedException($"Función no soportada: {f.Fn}");
        }
    }
}
