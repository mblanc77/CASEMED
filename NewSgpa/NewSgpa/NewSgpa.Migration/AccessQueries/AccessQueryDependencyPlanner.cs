using System.Text.RegularExpressions;

namespace NewSgpa.Migration.AccessQueries;

public enum AccessSqlDataObjectKind
{
    None,
    View,
    InlineTableValuedFunction
}

public sealed class AccessQueryPlanItem
{
    public required AccessQueryDefinition Query { get; init; }
    public required IReadOnlyList<string> Dependencies { get; init; }
    public required AccessSqlDataObjectKind DataObjectKind { get; init; }
}

public sealed class AccessQueryPlan
{
    public required IReadOnlyList<AccessQueryPlanItem> OrderedItems { get; init; }
    public required IReadOnlyList<string> CycleQueryNames { get; init; }
}

public static class AccessQueryDependencyPlanner
{
    private static readonly Regex FromJoinRegex = new(@"\b(?:FROM|JOIN)\s+(?:\(+\s*)?(?<name>\[[^\]]+\]|[A-Za-z0-9_][A-Za-z0-9_]*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public static AccessQueryPlan BuildPlan(IReadOnlyList<AccessQueryDefinition> queries)
    {
        var byName = queries
            .GroupBy(x => x.Name, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);

        var nodes = new Dictionary<string, AccessQueryPlanItem>(StringComparer.OrdinalIgnoreCase);
        foreach (var q in queries)
        {
            var deps = ExtractDependencies(q.RawSql)
                .Where(d => byName.ContainsKey(d) && !d.Equals(q.Name, StringComparison.OrdinalIgnoreCase))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            nodes[q.Name] = new AccessQueryPlanItem
            {
                Query = q,
                Dependencies = deps,
                DataObjectKind = DetermineDataObjectKind(q)
            };
        }

        var inDegree = nodes.ToDictionary(k => k.Key, _ => 0, StringComparer.OrdinalIgnoreCase);
        var outgoing = nodes.ToDictionary(k => k.Key, _ => new List<string>(), StringComparer.OrdinalIgnoreCase);

        foreach (var node in nodes.Values)
        {
            foreach (var dep in node.Dependencies)
            {
                if (!nodes.ContainsKey(dep))
                    continue;
                inDegree[node.Query.Name]++;
                outgoing[dep].Add(node.Query.Name);
            }
        }

        var queue = new Queue<string>(inDegree.Where(kv => kv.Value == 0).Select(kv => kv.Key).OrderBy(x => x, StringComparer.OrdinalIgnoreCase));
        var ordered = new List<AccessQueryPlanItem>();

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            ordered.Add(nodes[current]);

            foreach (var next in outgoing[current])
            {
                inDegree[next]--;
                if (inDegree[next] == 0)
                    queue.Enqueue(next);
            }
        }

        var cycleNodes = inDegree.Where(x => x.Value > 0).Select(x => x.Key).OrderBy(x => x, StringComparer.OrdinalIgnoreCase).ToList();
        if (cycleNodes.Count > 0)
        {
            // Keep deterministic output even with cycles.
            foreach (var name in cycleNodes)
            {
                if (ordered.All(o => !o.Query.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                    ordered.Add(nodes[name]);
            }
        }

        return new AccessQueryPlan
        {
            OrderedItems = ordered,
            CycleQueryNames = cycleNodes
        };
    }

    private static IEnumerable<string> ExtractDependencies(string sql)
    {
        foreach (Match m in FromJoinRegex.Matches(sql))
        {
            var raw = m.Groups["name"].Value.Trim();
            if (raw.StartsWith("[", StringComparison.Ordinal) && raw.EndsWith("]", StringComparison.Ordinal))
                raw = raw[1..^1];

            if (raw.Contains('.'))
                continue;

            yield return raw;
        }
    }

    private static AccessSqlDataObjectKind DetermineDataObjectKind(AccessQueryDefinition query)
    {
        var sql = query.RawSql.TrimStart();

        if (sql.StartsWith("TRANSFORM", StringComparison.OrdinalIgnoreCase))
            return AccessSqlDataObjectKind.None;

        if (sql.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
            return query.Parameters.Count == 0
                ? AccessSqlDataObjectKind.View
                : AccessSqlDataObjectKind.InlineTableValuedFunction;

        return AccessSqlDataObjectKind.None;
    }
}
