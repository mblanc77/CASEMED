using System.Text;
using System.Text.RegularExpressions;

namespace NewSgpa.Migration.AccessQueries;

public static class AccessQueryParser
{
    private static readonly Regex QueryHeaderRegex = new(@"^QUERY:\s*(.+?)\s*$", RegexOptions.Compiled);
    private static readonly Regex ParametersRegex = new(@"^PARAMETERS\s+(.+?);\s*", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
    private static readonly Regex ParamItemRegex = new(@"(?<name>\w+)\s+(?<type>[A-Za-z]+(?:Time)?)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex ParamTokenRegex = new(@"\[(?<name>p\w+)\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static IReadOnlyList<AccessQueryDefinition> ParseFile(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        var result = new List<AccessQueryDefinition>();

        var inQueriesSection = false;
        string? currentName = null;
        var sqlBuilder = new StringBuilder();

        void FlushCurrent()
        {
            if (string.IsNullOrWhiteSpace(currentName))
            {
                sqlBuilder.Clear();
                return;
            }

            var rawSql = sqlBuilder.ToString().Trim();
            if (!string.IsNullOrWhiteSpace(rawSql))
            {
                var parsed = ParseSqlBody(rawSql);
                result.Add(new AccessQueryDefinition
                {
                    SourceFile = Path.GetFileName(filePath),
                    Name = currentName!,
                    RawSql = parsed.Sql,
                    Parameters = parsed.Parameters
                });
            }

            currentName = null;
            sqlBuilder.Clear();
        }

        foreach (var rawLine in lines)
        {
            var line = rawLine;
            if (!inQueriesSection)
            {
                if (line.Trim().Equals("=== QUERIES ===", StringComparison.OrdinalIgnoreCase))
                    inQueriesSection = true;
                continue;
            }

            var headerMatch = QueryHeaderRegex.Match(line.Trim());
            if (headerMatch.Success)
            {
                FlushCurrent();
                currentName = headerMatch.Groups[1].Value.Trim();
                continue;
            }

            if (currentName is null)
                continue;

            var trimmed = line.TrimStart();
            if (trimmed.StartsWith("SQL:", StringComparison.OrdinalIgnoreCase))
            {
                sqlBuilder.AppendLine(trimmed[4..].TrimStart());
            }
            else
            {
                sqlBuilder.AppendLine(line);
            }
        }

        FlushCurrent();
        return result;
    }

    private static (string Sql, IReadOnlyList<AccessQueryParameter> Parameters) ParseSqlBody(string rawSql)
    {
        var sql = rawSql.Trim();
        var parameters = new List<AccessQueryParameter>();

        var m = ParametersRegex.Match(sql);
        if (m.Success)
        {
            var listText = m.Groups[1].Value;
            foreach (Match p in ParamItemRegex.Matches(listText))
            {
                parameters.Add(new AccessQueryParameter
                {
                    Name = p.Groups["name"].Value,
                    AccessType = p.Groups["type"].Value
                });
            }

            sql = sql[m.Length..].Trim();
        }

        // Access can reference [pX] parameters without a PARAMETERS declaration.
        var known = new HashSet<string>(parameters.Select(p => p.Name), StringComparer.OrdinalIgnoreCase);
        foreach (Match token in ParamTokenRegex.Matches(sql))
        {
            var name = token.Groups["name"].Value;
            if (known.Contains(name))
                continue;

            parameters.Add(new AccessQueryParameter
            {
                Name = name,
                AccessType = "Text"
            });
            known.Add(name);
        }

        return (sql, parameters);
    }
}
