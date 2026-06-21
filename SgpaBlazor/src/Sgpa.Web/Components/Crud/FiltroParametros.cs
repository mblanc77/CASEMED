using System.Text.Json;
using DevExpress.Data.Filtering;
using Sgpa.Domain.Metadata;

namespace Sgpa.Web.Components.Crud;

/// <summary>Definición persistida de un parámetro de filtro (se guarda como JSON junto al criterio).</summary>
public sealed record FiltroParametroDef(string Nombre, string Prompt, string ClrType, string? FkEntidad, string? DefaultJson, bool Requerido = true);

/// <summary>Una "hoja" del criterio que puede parametrizarse (un valor concreto), para la UI de autoría.</summary>
public sealed record FiltroLeaf(int Indice, string Caption, string ClrType, string? FkEntidad, string? ValorActual, string SugerenciaNombre);

/// <summary>
/// Convierte filtros guardados en filtros con <b>parámetros pedidos al ejecutar</b> (estilo "Request Parameters"
/// de XtraReports). Trabaja sobre el <see cref="CriteriaOperator"/> de DevExpress:
/// <list type="bullet">
///   <item><see cref="Enumerar"/>: lista los valores parametrizables (para tildar cuáles pedir).</item>
///   <item><see cref="Parametrizar"/>: reemplaza los valores elegidos por <c>OperandParameter</c> y devuelve sus defs.</item>
///   <item><see cref="Sustituir"/>: al ejecutar, reemplaza cada parámetro por el valor ingresado.</item>
/// </list>
/// El recorrido de enumerar/parametrizar es idéntico (mismo método, dos modos) → los índices coinciden.
/// </summary>
public static class FiltroParametros
{
    public static IReadOnlyList<FiltroLeaf> Enumerar(CriteriaOperator? crit, Type owner)
    {
        if (ReferenceEquals(crit, null)) return System.Array.Empty<FiltroLeaf>();
        var st = new State { OwnerType = owner };
        Process(crit, MetaOf(owner), st);
        return st.Leaves;
    }

    public static (CriteriaOperator? Criteria, IReadOnlyList<FiltroParametroDef> Defs) Parametrizar(
        CriteriaOperator? crit, Type owner, IReadOnlyDictionary<int, string> elegidos)
    {
        if (ReferenceEquals(crit, null)) return (crit, System.Array.Empty<FiltroParametroDef>());
        var st = new State { Rebuild = true, OwnerType = owner, Chosen = new Dictionary<int, string>(elegidos) };
        var c = Process(crit, MetaOf(owner), st);
        return (c, st.Defs);
    }

    /// <summary>Reemplaza cada <c>OperandParameter</c> por su valor (al ejecutar el filtro).</summary>
    public static CriteriaOperator? Sustituir(CriteriaOperator? crit, IReadOnlyDictionary<string, object?> valores)
        => ReferenceEquals(crit, null) ? crit : Sub(crit, valores);

    public static IReadOnlyList<string> Nombres(CriteriaOperator? crit)
    {
        var names = new List<string>();
        if (!ReferenceEquals(crit, null)) Collect(crit, names);
        return names.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
    }

    public static string SerializarDefs(IReadOnlyList<FiltroParametroDef> defs) => JsonSerializer.Serialize(defs);
    public static IReadOnlyList<FiltroParametroDef> DeserializarDefs(string? json)
        => string.IsNullOrWhiteSpace(json) ? System.Array.Empty<FiltroParametroDef>()
           : (JsonSerializer.Deserialize<List<FiltroParametroDef>>(json) ?? new());

    // ---------- recorrido dual (enumerar / reconstruir) ----------

    private sealed class State
    {
        public bool Rebuild;
        public int Counter;
        public Type OwnerType = default!;
        public Dictionary<int, string>? Chosen;     // índice -> etiqueta/prompt elegida por el usuario
        public List<FiltroLeaf> Leaves = new();
        public List<FiltroParametroDef> Defs = new();
        public HashSet<string> Used = new(StringComparer.OrdinalIgnoreCase);  // identificadores ?X ya usados
    }

    private static CriteriaOperator Process(CriteriaOperator op, EntityMetadata? ctx, State st)
    {
        switch (op)
        {
            case GroupOperator g:
            {
                var ops = g.Operands.Select(o => Process(o, ctx, st)).ToArray();
                return st.Rebuild ? new GroupOperator(g.OperatorType, ops) : op;
            }
            case UnaryOperator u:
            {
                var inner = Process(u.Operand, ctx, st);
                return st.Rebuild ? new UnaryOperator(u.OperatorType, inner) : op;
            }
            case AggregateOperand a:
            {
                var prefix = a.CollectionProperty is OperandProperty cp ? cp.PropertyName : null;
                var childCtx = prefix is not null ? EntityRelations.ByPrefix(st.OwnerType, prefix)?.Child : ctx;
                var cond = ReferenceEquals(a.Condition, null) ? null : Process(a.Condition, childCtx, st);
                return st.Rebuild
                    ? new AggregateOperand((OperandProperty)a.CollectionProperty, a.AggregatedExpression, a.AggregateType, cond)
                    : op;
            }
            case BetweenOperator bt:
            {
                var (caption, clr, fk) = ColInfo(PropName(bt.TestExpression), ctx, st.OwnerType);
                var begin = ValueSlot(caption, clr, fk, bt.BeginExpression, st, "desde");
                var end = ValueSlot(caption, clr, fk, bt.EndExpression, st, "hasta");
                return st.Rebuild ? new BetweenOperator(bt.TestExpression, begin, end) : op;
            }
            case FunctionOperator f
                when (f.OperatorType is FunctionOperatorType.StartsWith or FunctionOperatorType.EndsWith or FunctionOperatorType.Contains)
                     && f.Operands.Count >= 2 && PropName(f.Operands[0]) is { } fcol && f.Operands[1] is OperandValue:
            {
                var (caption, clr, fk) = ColInfo(fcol, ctx, st.OwnerType);
                var v = ValueSlot(caption, clr, fk, f.Operands[1], st, null);
                if (!st.Rebuild) return op;
                var ops = f.Operands.ToArray(); ops[1] = v;
                return new FunctionOperator(f.OperatorType, ops);
            }
            case BinaryOperator b:
            {
                // agregado <op> valor  (ej. Certificaciones.Count() > ?n): el agregado puede llevar params internos.
                if (b.LeftOperand is AggregateOperand aL && b.RightOperand is OperandValue)
                {
                    var left = Process(aL, ctx, st);
                    var (cap, clr) = AggInfo(aL, st.OwnerType);
                    var v = ValueSlot(cap, clr, null, b.RightOperand, st, null);
                    return st.Rebuild ? new BinaryOperator(left, v, b.OperatorType) : op;
                }
                // propiedad <op> valor
                if (PropName(b.LeftOperand) is { } lc && b.RightOperand is OperandValue)
                {
                    var (caption, clr, fk) = ColInfo(lc, ctx, st.OwnerType);
                    var v = ValueSlot(caption, clr, fk, b.RightOperand, st, null);
                    return st.Rebuild ? new BinaryOperator(b.LeftOperand, v, b.OperatorType) : op;
                }
                if (PropName(b.RightOperand) is { } rc && b.LeftOperand is OperandValue)
                {
                    var (caption, clr, fk) = ColInfo(rc, ctx, st.OwnerType);
                    var v = ValueSlot(caption, clr, fk, b.LeftOperand, st, null);
                    return st.Rebuild ? new BinaryOperator(v, b.RightOperand, b.OperatorType) : op;
                }
                var l = Process(b.LeftOperand, ctx, st);
                var r = Process(b.RightOperand, ctx, st);
                return st.Rebuild ? new BinaryOperator(l, r, b.OperatorType) : op;
            }
            default:
                return op; // OperandProperty, OperandValue, InOperator (listas no se parametrizan), etc.
        }
    }

    private static CriteriaOperator ValueSlot(string caption, Type clr, string? fk, CriteriaOperator valueOp, State st, string? suffix)
    {
        var idx = st.Counter++;
        var label = suffix is null ? caption : $"{caption} ({suffix})";
        var curVal = (valueOp as OperandValue)?.Value;
        if (!st.Rebuild)
        {
            st.Leaves.Add(new FiltroLeaf(idx, label, clr.Name, fk, curVal?.ToString(), Sugerir(caption, suffix)));
            return valueOp;
        }
        if (st.Chosen!.TryGetValue(idx, out var prompt))
        {
            // El texto del usuario es la ETIQUETA (prompt); el nombre del parámetro (token ?X del criterio) debe ser
            // un identificador sin espacios para que CriteriaOperator.Parse haga round-trip.
            var promptText = string.IsNullOrWhiteSpace(prompt) ? label : prompt;
            var nombre = UniqueId(promptText, st.Used);
            st.Defs.Add(new FiltroParametroDef(nombre, promptText,
                clr.AssemblyQualifiedName ?? clr.FullName ?? "System.String",
                fk, curVal is null ? null : JsonSerializer.Serialize(curVal)));
            return new OperandParameter(nombre);
        }
        return valueOp;
    }

    // Identificador seguro (sin espacios ni símbolos) y único dentro del filtro, derivado de la etiqueta.
    private static string UniqueId(string label, HashSet<string> used)
    {
        var id = new string(label.Where(char.IsLetterOrDigit).ToArray());
        if (id.Length == 0) id = "Param";
        if (char.IsDigit(id[0])) id = "p" + id;
        var baseId = id;
        var k = 1;
        while (!used.Add(id)) id = baseId + (++k);
        return id;
    }

    // ---------- sustitución (ejecución) ----------

    private static CriteriaOperator Sub(CriteriaOperator op, IReadOnlyDictionary<string, object?> vals) => op switch
    {
        OperandParameter p => new OperandValue(vals.TryGetValue(p.ParameterName, out var v) ? v : null),
        GroupOperator g => new GroupOperator(g.OperatorType, g.Operands.Select(o => Sub(o, vals)).ToArray()),
        UnaryOperator u => new UnaryOperator(u.OperatorType, Sub(u.Operand, vals)),
        BinaryOperator b => new BinaryOperator(Sub(b.LeftOperand, vals), Sub(b.RightOperand, vals), b.OperatorType),
        BetweenOperator bt => new BetweenOperator(Sub(bt.TestExpression, vals), Sub(bt.BeginExpression, vals), Sub(bt.EndExpression, vals)),
        InOperator i => new InOperator(Sub(i.LeftOperand, vals), i.Operands.Select(o => Sub(o, vals)).ToArray()),
        FunctionOperator f when f.OperatorType != FunctionOperatorType.Custom =>
            new FunctionOperator(f.OperatorType, f.Operands.Select(o => Sub(o, vals)).ToArray()),
        AggregateOperand a => new AggregateOperand((OperandProperty)a.CollectionProperty,
            ReferenceEquals(a.AggregatedExpression, null) ? null : Sub(a.AggregatedExpression, vals),
            a.AggregateType, ReferenceEquals(a.Condition, null) ? null : Sub(a.Condition, vals)),
        _ => op
    };

    private static void Collect(CriteriaOperator op, List<string> names)
    {
        switch (op)
        {
            case OperandParameter p: names.Add(p.ParameterName); break;
            case GroupOperator g: foreach (var o in g.Operands) Collect(o, names); break;
            case UnaryOperator u: Collect(u.Operand, names); break;
            case BinaryOperator b: Collect(b.LeftOperand, names); Collect(b.RightOperand, names); break;
            case BetweenOperator bt: Collect(bt.TestExpression, names); Collect(bt.BeginExpression, names); Collect(bt.EndExpression, names); break;
            case InOperator i: Collect(i.LeftOperand, names); foreach (var o in i.Operands) Collect(o, names); break;
            case FunctionOperator f: foreach (var o in f.Operands) Collect(o, names); break;
            case AggregateOperand a:
                if (!ReferenceEquals(a.AggregatedExpression, null)) Collect(a.AggregatedExpression, names);
                if (!ReferenceEquals(a.Condition, null)) Collect(a.Condition, names);
                break;
        }
    }

    // ---------- resolución de columnas/tipos ----------

    private static (string Caption, Type Clr, string? Fk) ColInfo(string? propName, EntityMetadata? ctx, Type owner)
    {
        if (propName is null) return ("valor", typeof(string), null);
        var dot = propName.IndexOf('.');
        if (dot > 0)
        {
            var rel = EntityRelations.ByPrefix(owner, propName[..dot]);
            if (rel is not null)
            {
                var local = propName[(dot + 1)..];
                var col = rel.Child.Columns.FirstOrDefault(c =>
                    c.Name.Equals(local, StringComparison.OrdinalIgnoreCase) || c.Property.Name.Equals(local, StringComparison.OrdinalIgnoreCase));
                if (col is not null) return ($"{rel.Label}.{col.Caption}", col.UnderlyingType, FkOf(col, rel.Child));
            }
        }
        if (ctx is not null)
        {
            var col = ctx.Columns.FirstOrDefault(c =>
                c.Name.Equals(propName, StringComparison.OrdinalIgnoreCase) || c.Property.Name.Equals(propName, StringComparison.OrdinalIgnoreCase));
            if (col is not null) return (col.Caption, col.UnderlyingType, FkOf(col, ctx));
        }
        return (propName, typeof(string), null);
    }

    private static (string Caption, Type Clr) AggInfo(AggregateOperand a, Type owner)
    {
        var prefix = a.CollectionProperty is OperandProperty cp ? cp.PropertyName : "?";
        var label = EntityRelations.ByPrefix(owner, prefix)?.Label ?? prefix;
        if (a.AggregateType == Aggregate.Count) return ($"{label}.Cantidad", typeof(int));
        var col = a.AggregatedExpression is OperandProperty ap ? ap.PropertyName : "valor";
        return ($"{label}.{a.AggregateType}({col})", typeof(double));
    }

    private static string? FkOf(ColumnMetadata col, EntityMetadata owner)
        => EntityCatalog.LookupTargetFor(col, owner)?.Table;

    private static EntityMetadata? MetaOf(Type t) => EntityCatalog.All.FirstOrDefault(m => m.EntityType == t);

    private static string? PropName(CriteriaOperator? op) => op is OperandProperty p ? p.PropertyName : null;

    private static string Sugerir(string caption, string? suffix)
    {
        var baseName = new string(caption.Where(char.IsLetterOrDigit).ToArray());
        if (string.IsNullOrEmpty(baseName)) baseName = "Param";
        return suffix is null ? baseName : baseName + char.ToUpperInvariant(suffix[0]) + suffix[1..];
    }
}
