using System.Text;
using System.Text.RegularExpressions;

namespace NewSgpa.Migration.AccessQueries;

public static class AccessToSqlTranslator
{
    // Access parameter placeholders are typically [pXxx]. Keep this strict to avoid matching table names like [Prestacion].
    private static readonly Regex ParamRegex = new(@"\[(?<name>p[A-Z]\w*)\]", RegexOptions.Compiled);
    private static readonly Regex DateLiteralRegex = new(@"#(?<m>\d{1,2})/(?<d>\d{1,2})/(?<y>\d{2,4})#", RegexOptions.Compiled);
    private static readonly Regex TransformRegex = new(@"^\s*TRANSFORM\s+Sum\((?<agg>.+?)\)\s+AS\s+.+?\s+SELECT\s+(?<select>.+?)\s+FROM\s+(?<from>.+?)\s+GROUP\s+BY\s+(?<group>.+?)\s+PIVOT\s+(?<pivot>.+?)\s*;?\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
    private static readonly Regex BareNumericIdentifierAfterClauseRegex = new(@"\b(?<kw>FROM|JOIN|INTO|UPDATE|DELETE\s+FROM)\s+(?<prefix>\(+\s*)?(?<name>\d[\w]*)\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public static string TranslateToSqlServer(string accessSql)
    {
        var sql = accessSql;

        sql = RewriteAccessUpdateJoin(sql);

        sql = NormalizeSelfAliasExpressions(sql);

        sql = NormalizeDeleteSyntax(sql);

        sql = ParamRegex.Replace(sql, m => "@" + NormalizeParameterName(m.Groups["name"].Value));
        sql = DateLiteralRegex.Replace(sql, m =>
        {
            var y = m.Groups["y"].Value;
            if (y.Length == 2) y = "20" + y;
            var month = int.Parse(m.Groups["m"].Value);
            var day = int.Parse(m.Groups["d"].Value);
            return $"CONVERT(date, '{y}-{month:00}-{day:00}')";
        });

        sql = Regex.Replace(sql, @"\biif\s*\(", "IIF(", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bDISTINCTROW\b", "DISTINCT", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bYear\s*\(", "YEAR(", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bMonth\s*\(", "MONTH(", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bDay\s*\(", "DAY(", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bLen\s*\(", "LEN(", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bInStr\s*\(", "CHARINDEX(", RegexOptions.IgnoreCase);
        // InStr(string, substring) -> CHARINDEX(substring, string)
        sql = Regex.Replace(
            sql,
            @"CHARINDEX\(\s*(?<str>[^,\)]+)\s*,\s*(?<find>'[^']*')\s*\)",
            m => $"CHARINDEX({m.Groups["find"].Value}, {m.Groups["str"].Value})",
            RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bMid\s*\(", "SUBSTRING(", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bLeft\s*\(", "LEFT(", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bRight\s*\(", "RIGHT(", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bFormatCI\s*\(", "CONVERT(nvarchar(max),", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bDateDiff\s*\(\s*\""d\""\s*,", "DATEDIFF(day,", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bDateDiff\s*\(\s*\""m\""\s*,", "DATEDIFF(month,", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bDateDiff\s*\(\s*\""yyyy\""\s*,", "DATEDIFF(year,", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bDateAdd\s*\(\s*\""d\""\s*,", "DATEADD(day,", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bDateAdd\s*\(\s*\""m\""\s*,", "DATEADD(month,", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bDateAdd\s*\(\s*\""yyyy\""\s*,", "DATEADD(year,", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bCDbl\s*\(", "TRY_CONVERT(float,", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bCLng\s*\(", "TRY_CONVERT(int,", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bVal\s*\(", "TRY_CONVERT(float,", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bCStr\s*\(", "CONVERT(nvarchar(max),", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bCDate\s*\(", "TRY_CONVERT(datetime2,", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bFormat\s*\(", "FORMAT(", RegexOptions.IgnoreCase);

        // Access string literals are commonly delimited with double quotes.
        sql = ConvertDoubleQuotedLiterals(sql);

        // Ensure CHARINDEX argument order is SQL Server-compatible: CHARINDEX(find, expression)
        sql = Regex.Replace(
            sql,
            @"CHARINDEX\(\s*(?<expr>[^,\)]+)\s*,\s*(?<find>'[^']*')\s*\)",
            m => $"CHARINDEX({m.Groups["find"].Value}, {m.Groups["expr"].Value})",
            RegexOptions.IgnoreCase);

        // Convert Access conditional helpers to robust SQL CASE expressions.
        sql = ConvertIifExpressions(sql);
        sql = ConvertSwitchExpressions(sql);
        sql = ConvertIsNullExpressions(sql);

        // Normalize string concatenation with parameters when adjacent to string literals.
        sql = Regex.Replace(
            sql,
            @"'(?<lit>[^']*)'\s*\+\s*@(?<p>[A-Za-z_][A-Za-z0-9_]*)",
            m => $"'{m.Groups["lit"].Value}' + CONVERT(nvarchar(50), @{m.Groups["p"].Value})",
            RegexOptions.IgnoreCase);
        sql = Regex.Replace(
            sql,
            @"@(?<p>[A-Za-z_][A-Za-z0-9_]*)\s*\+\s*'(?<lit>[^']*)'",
            m => $"CONVERT(nvarchar(50), @{m.Groups["p"].Value}) + '{m.Groups["lit"].Value}'",
            RegexOptions.IgnoreCase);

        // Targeted conversion for @param concatenation: + @pX + -> + CONVERT(nvarchar(50), @pX) +
        sql = Regex.Replace(sql, @"\+\s*@(?<p>[A-Za-z_][A-Za-z0-9_]*)\s*\+", m => $"+ CONVERT(nvarchar(50), @{m.Groups["p"].Value}) +");

        // Trailing concatenation before ) or , : + @pX) -> + CONVERT(nvarchar(50), @pX))
        sql = Regex.Replace(sql, @"\+\s*@(?<p>[A-Za-z_][A-Za-z0-9_]*)\s*(?=[\),])", m => $"+ CONVERT(nvarchar(50), @{m.Groups["p"].Value}) ");

        // Normalize common yyyymm numeric patterns to deterministic SQL arithmetic (avoid FORMAT->float conversions).
        sql = NormalizeYearMonthNumericExpressions(sql);

        // Generic FORMAT replacements for year-month and zero-padding cases used as numeric/string keys.
        sql = Regex.Replace(
            sql,
            @"FORMAT\(\s*(?<expr>[^,\)]+(?:\([^\)]*\))?)\s*,\s*'yyyymm'\s*\)",
            m => $"LEFT(CONVERT(char(8), {m.Groups["expr"].Value.Trim()}, 112), 6)",
            RegexOptions.IgnoreCase);
        sql = Regex.Replace(
            sql,
            @"FORMAT\(\s*(?<expr>[^,\)]+)\s*,\s*'00'\s*\)",
            m => $"RIGHT('00' + CONVERT(varchar(2), {m.Groups["expr"].Value.Trim()}), 2)",
            RegexOptions.IgnoreCase);

        // Date/Time and interval functions after quote normalization.
        sql = Regex.Replace(sql, @"\bDate\s*\(\s*\)", "CAST(GETDATE() AS date)", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bNow\s*\(\s*\)", "SYSDATETIME()", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bDateDiff\s*\(\s*'d'\s*,", "DATEDIFF(day,", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bDateDiff\s*\(\s*'m'\s*,", "DATEDIFF(month,", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bDateDiff\s*\(\s*'yyyy'\s*,", "DATEDIFF(year,", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bDateAdd\s*\(\s*'d'\s*,", "DATEADD(day,", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bDateAdd\s*\(\s*'m'\s*,", "DATEADD(month,", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bDateAdd\s*\(\s*'yyyy'\s*,", "DATEADD(year,", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bMod\b", "%", RegexOptions.IgnoreCase);

        // Access aggregate aliases unsupported in SQL Server.
        sql = Regex.Replace(sql, @"\bFirst\s*\(", "MIN(", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\bLast\s*\(", "MAX(", RegexOptions.IgnoreCase);

        // SQL Server requires quoted identifiers when names start with a digit.
        sql = NormalizeBareNumericIdentifiersInClauses(sql);

        // Known schema remaps from Access source names to SQL target names.
        sql = ApplyKnownColumnMappings(sql);

        // Access uses '&' for concatenation.
        sql = Regex.Replace(sql, @"\s*&\s*", " + ");

        // Re-run parameter concatenation normalization after '&' -> '+' conversion.
        sql = Regex.Replace(sql, @"\+\s*@(?<p>[A-Za-z_][A-Za-z0-9_]*)\s*\+", m => $"+ CONVERT(nvarchar(50), @{m.Groups["p"].Value}) +");
        sql = Regex.Replace(sql, @"\+\s*@(?<p>[A-Za-z_][A-Za-z0-9_]*)\s*(?=[\),])", m => $"+ CONVERT(nvarchar(50), @{m.Groups["p"].Value}) ");

        // Access boolean operator.
        sql = Regex.Replace(sql, @"\bNot\b", "NOT", RegexOptions.IgnoreCase);

        // Access comparisons against boolean False in predicates.
        sql = Regex.Replace(sql, @"<>\s*False\b", "= 1", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"=\s*False\b", "= 0", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\(\s*EXISTS\s*\((?<sub>[\s\S]*?)\)\s*\)\s*=\s*1", "EXISTS(${sub})", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\(\s*(?<pred>[^\(\)]*\bAND\b[^\(\)]*)\s*\)\s*=\s*1", "(${pred})", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\(\s*(?<pred>[^\(\)]*?<=\s*[^\(\)]*?\bAnd\b\s*[^\(\)]*?>=\s*[^\(\)]*?)\s*\)\s*=\s*1", "(${pred})", RegexOptions.IgnoreCase);

        // Simplify nested CASE boolean wrappers generated from IIF(IsNull(...), ...) patterns.
        // CASE WHEN (CASE WHEN X IS NULL THEN 1 ELSE 0 END) THEN A ELSE B END
        // -> CASE WHEN X IS NULL THEN A ELSE B END
        sql = Regex.Replace(
            sql,
            @"CASE\s+WHEN\s*\(\s*CASE\s+WHEN\s+(?<pred>[^\)]*?\s+IS\s+NULL)\s+THEN\s+1\s+ELSE\s+0\s+END\s*\)\s+THEN\s+(?<a>[\s\S]*?)\s+ELSE\s+(?<b>[\s\S]*?)\s+END",
            "CASE WHEN ${pred} THEN ${a} ELSE ${b} END",
            RegexOptions.IgnoreCase);

        // Access boolean literals in predicates.
        sql = Regex.Replace(sql, @"=\s*True\b", "= 1", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"=\s*False\b", "= 0", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\)\s*=\s*100\b", ") = 100", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\)(?=\s*100\b)", ") = ", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"\(\s*SubsidioItem\.CodSubsidioItemCod\s*\)\s*00\b", "(SubsidioItem.CodSubsidioItemCod)=100", RegexOptions.IgnoreCase);
        sql = Regex.Replace(sql, @"NOT\s*\(\s*CASE\s+WHEN\s+(?<expr>[^\)]*?)\s+IS\s+NULL\s+THEN\s+1\s+ELSE\s+0\s+END\s*\)", "${expr} IS NOT NULL", RegexOptions.IgnoreCase);

        return sql.Trim();
    }

    private static string PrefixBareParameterTokens(string sql, IReadOnlyList<AccessQueryParameter> parameters)
    {
        var rewritten = sql;
        foreach (var p in parameters)
        {
            var normalized = NormalizeParameterName(p.Name);
            // If parameter already appears as [pX], convert bracketed form first.
            rewritten = Regex.Replace(
                rewritten,
                $@"\[{Regex.Escape(p.Name)}\]",
                "@" + normalized,
                RegexOptions.IgnoreCase);
            rewritten = Regex.Replace(
                rewritten,
                $@"(?<![@\.\w]){Regex.Escape(p.Name)}\b",
                "@" + normalized,
                RegexOptions.IgnoreCase);
        }

        return rewritten;
    }

    private static string NormalizeSelfAliasExpressions(string sql)
    {
        // Access allows referring to SELECT aliases in the same projection, e.g.
        // Sum(EmpresaPago.Importe) AS Importe, ((Importe * 0.5) / 100) AS Tributo...
        // SQL Server does not. Rewrite this specific frequent pattern semantically.
        sql = Regex.Replace(
            sql,
            @"\(\(\s*Importe\s*\*\s*0\.5\s*\)\s*/\s*100\s*\)\s+AS\s+TributoTotImpMut",
            "((Sum(EmpresaPago.Importe) * 0.5) / 100) AS TributoTotImpMut",
            RegexOptions.IgnoreCase);

        // Access allows projection aliases (Dias/Importe) reused in same SELECT list.
        // Rewrite to aggregate expressions to avoid ambiguous names in SQL Server.
        sql = Regex.Replace(
            sql,
            @"\(\s*CASE\s+WHEN\s+Dias\s*>\s*0\s+THEN\s+Importe\s*/\s*Dias\s+ELSE\s+0\s+END\s*\)\s+AS\s+Promedio",
            "(CASE WHEN Sum(SubsidioImponible.Dias)>0 THEN Sum(SubsidioImponible.Importe)/Sum(SubsidioImponible.Dias) ELSE 0 END) AS Promedio",
            RegexOptions.IgnoreCase);
        sql = Regex.Replace(
            sql,
            @"\(\s*CASE\s+WHEN\s+Dias\s*>\s*0\s+THEN\s+Importe\s*/\s*Dias\s+ELSE\s+0\s+END\s*\)",
            "(CASE WHEN Sum(SubsidioImponible.Dias)>0 THEN Sum(SubsidioImponible.Importe)/Sum(SubsidioImponible.Dias) ELSE 0 END)",
            RegexOptions.IgnoreCase);
        sql = Regex.Replace(
            sql,
            @"\bImporte\s*/\s*180\b",
            "Sum(SubsidioImponible.Importe)/180",
            RegexOptions.IgnoreCase);

        // Access SELECT alias reuse inside same projection for iCol in Rs_Bps* queries.
        sql = Regex.Replace(
            sql,
            @"TRY_CONVERT\(float,\(CASE\s+WHEN\s+iCol>0\s+THEN\s+LEFT\((?<tbl>Bps\d)\.CI,iCol-1\)\s*\+\s*SUBSTRING\((?<tbl2>Bps\d)\.CI,iCol\+1,1\)\s+ELSE\s+(?<tbl3>Bps\d)\.CI\s+END\)\)",
            m => $"TRY_CONVERT(float,(CASE WHEN CHARINDEX('-', {m.Groups["tbl"].Value}.CI)>0 THEN LEFT({m.Groups["tbl"].Value}.CI,CHARINDEX('-', {m.Groups["tbl"].Value}.CI)-1) + SUBSTRING({m.Groups["tbl2"].Value}.CI,CHARINDEX('-', {m.Groups["tbl2"].Value}.CI)+1,1) ELSE {m.Groups["tbl3"].Value}.CI END))",
            RegexOptions.IgnoreCase);

        // Access-style date construction pattern used for computed birthday in current year.
        // Replace TRY_CONVERT(datetime2, CONVERT(nvarchar(max), dayExpr) + FORMAT(FechaNacimiento,'/mm/') + YEAR(GETDATE()))
        // with DATEFROMPARTS(YEAR(GETDATE()), MONTH(FechaNacimiento), dayExpr).
        sql = Regex.Replace(
            sql,
            @"TRY_CONVERT\(\s*datetime2\s*,\s*CONVERT\(\s*nvarchar\(max\)\s*,\s*(?<day>\([^\)]*\))\s*\)\s*\+\s*FORMAT\(\s*FechaNacimiento\s*,\s*'/mm/'\s*\)\s*\+\s*YEAR\(\s*CAST\(GETDATE\(\)\s+AS\s+date\)\s*\)\s*\)",
            m => $"DATEFROMPARTS(YEAR(CAST(GETDATE() AS date)), MONTH(FechaNacimiento), {m.Groups["day"].Value})",
            RegexOptions.IgnoreCase);

        return sql;
    }

    private static string ApplyKnownColumnMappings(string sql)
    {
        // Access table Mutualista field is Nombre; SQL target uses Descrip.
        sql = Regex.Replace(sql, @"\bMutualista\.Nombre\b", "Mutualista.Descrip", RegexOptions.IgnoreCase);

        // Access PrestacionTipo had PeriodoRenovacion in legacy query logic; target schema no longer has it.
        // Preserve query shape returning a constant neutral value.
        sql = Regex.Replace(sql, @"PrestacionTipo\.PeriodoRenovacion", "0", RegexOptions.IgnoreCase);

        // Legacy sgpa query joins Certificacion.NroLlamado to SubsidioEnfermedad.NroLlamado,
        // but SubsidioEnfermedad in target schema has no NroLlamado. Map to IdSubsidio relation.
        sql = Regex.Replace(
            sql,
            @"C\.NroLlamado\s*=\s*SubsidioEnfermedad\.NroLlamado",
            "C.NroLlamado = SubsidioEnfermedad.IdSubsidio",
            RegexOptions.IgnoreCase);

        // Legacy report uses Certificacion.Salir; target schema uses CodSalidaTipo.
        sql = Regex.Replace(sql, @"\bCertificacion\.Salir\b", "Certificacion.CodSalidaTipo", RegexOptions.IgnoreCase);

        return sql;
    }

    private static string NormalizeYearMonthNumericExpressions(string sql)
    {
        // TRY_CONVERT(float, CONVERT(nvarchar(max), anioExpr) + RIGHT('00'+CONVERT(varchar(2), mesExpr),2))
        sql = Regex.Replace(
            sql,
            @"TRY_CONVERT\(\s*float\s*,\s*CONVERT\(\s*nvarchar\(max\)\s*,\s*(?<anio>[^\)]+)\)\s*\+\s*RIGHT\(\s*'00'\s*\+\s*CONVERT\(\s*varchar\(2\)\s*,\s*(?<mes>[^\)]+)\)\s*,\s*2\s*\)\s*\)",
            m => $"((TRY_CONVERT(int,{m.Groups["anio"].Value.Trim()}) * 100) + TRY_CONVERT(int,{m.Groups["mes"].Value.Trim()}))",
            RegexOptions.IgnoreCase);

        // CONVERT(nvarchar(max), anioExpr) + RIGHT('00'+CONVERT(varchar(2), mesExpr),2)
        sql = Regex.Replace(
            sql,
            @"CONVERT\(\s*nvarchar\(max\)\s*,\s*(?<anio>[^\)]+)\)\s*\+\s*RIGHT\(\s*'00'\s*\+\s*CONVERT\(\s*varchar\(2\)\s*,\s*(?<mes>[^\)]+)\)\s*,\s*2\s*\)",
            m => $"((TRY_CONVERT(int,{m.Groups["anio"].Value.Trim()}) * 100) + TRY_CONVERT(int,{m.Groups["mes"].Value.Trim()}))",
            RegexOptions.IgnoreCase);

        // TRY_CONVERT(float, FORMAT(dateExpr,'yyyymm')) -> (YEAR(dateExpr) * 100 + MONTH(dateExpr))
        sql = Regex.Replace(
            sql,
            @"TRY_CONVERT\(\s*float\s*,\s*FORMAT\(\s*(?<expr>.+?)\s*,\s*'yyyymm'\s*\)\s*\)",
            m => $"(YEAR({m.Groups["expr"].Value.Trim()}) * 100 + MONTH({m.Groups["expr"].Value.Trim()}))",
            RegexOptions.IgnoreCase);

        // TRY_CONVERT(float, FORMAT(dateExpr,'yyyymm')) -> (YEAR(dateExpr) * 100 + MONTH(dateExpr))
        sql = Regex.Replace(
            sql,
            @"TRY_CONVERT\(\s*float\s*,\s*FORMAT\(\s*(?<expr>[^,\)]+(?:\([^\)]*\))?)\s*,\s*'yyyymm'\s*\)\s*\)",
            m => $"(YEAR({m.Groups["expr"].Value.Trim()}) * 100 + MONTH({m.Groups["expr"].Value.Trim()}))",
            RegexOptions.IgnoreCase);

        // TRY_CONVERT(float, FORMAT(dateExpr,'mm')) / ('yyyy')
        sql = Regex.Replace(
            sql,
            @"TRY_CONVERT\(\s*float\s*,\s*FORMAT\(\s*(?<expr>.+?)\s*,\s*'mm'\s*\)\s*\)",
            m => $"MONTH({m.Groups["expr"].Value.Trim()})",
            RegexOptions.IgnoreCase);
        sql = Regex.Replace(
            sql,
            @"TRY_CONVERT\(\s*float\s*,\s*FORMAT\(\s*(?<expr>.+?)\s*,\s*'yyyy'\s*\)\s*\)",
            m => $"YEAR({m.Groups["expr"].Value.Trim()})",
            RegexOptions.IgnoreCase);

        // TRY_CONVERT(float, FORMAT(expr)) -> TRY_CONVERT(float, expr)
        sql = Regex.Replace(
            sql,
            @"TRY_CONVERT\(\s*float\s*,\s*FORMAT\(\s*(?<expr>[^\)]+)\s*\)\s*\)",
            m => $"TRY_CONVERT(float,{m.Groups["expr"].Value.Trim()})",
            RegexOptions.IgnoreCase);

        // TRY_CONVERT(float, expr, '0') -> TRY_CONVERT(float, expr)
        // (style argument from Access Format("0") patterns is invalid for numeric conversions in SQL Server)
        sql = Regex.Replace(
            sql,
            @"TRY_CONVERT\(\s*float\s*,\s*(?<expr>[^,\)]+)\s*,\s*'0'\s*\)",
            m => $"TRY_CONVERT(float,{m.Groups["expr"].Value.Trim()})",
            RegexOptions.IgnoreCase);

        // TRY_CONVERT(float, <anio> + FORMAT(<mes>, '00')) -> ((anio * 100) + mes)
        sql = Regex.Replace(
            sql,
            @"TRY_CONVERT\(\s*float\s*,\s*(?<anio>[^\+\)]+?)\s*\+\s*FORMAT\(\s*(?<mes>[^,\)]+)\s*,\s*'00'\s*\)\s*\)",
            m => $"((TRY_CONVERT(int,{m.Groups["anio"].Value.Trim()}) * 100) + TRY_CONVERT(int,{m.Groups["mes"].Value.Trim()}))",
            RegexOptions.IgnoreCase);

        // TRY_CONVERT(float, FORMAT(anioExpr) + RIGHT('00' + CONVERT(varchar(2), mesExpr), 2))
        sql = Regex.Replace(
            sql,
            @"TRY_CONVERT\(\s*float\s*,\s*FORMAT\(\s*(?<anio>[^\)]+)\s*\)\s*\+\s*RIGHT\(\s*'00'\s*\+\s*CONVERT\(\s*varchar\(2\)\s*,\s*(?<mes>[^\)]+)\)\s*,\s*2\s*\)\s*\)",
            m => $"((TRY_CONVERT(int,{m.Groups["anio"].Value.Trim()}) * 100) + TRY_CONVERT(int,{m.Groups["mes"].Value.Trim()}))",
            RegexOptions.IgnoreCase);

        // Broader fallback for FORMAT(anio)+RIGHT('00'+CONVERT(varchar(2),mes),2) inside TRY_CONVERT(float,...)
        sql = Regex.Replace(
            sql,
            @"TRY_CONVERT\(\s*float\s*,\s*FORMAT\((?<anio>.*?)\)\s*\+\s*RIGHT\(\s*'00'\s*\+\s*CONVERT\(\s*varchar\(2\)\s*,\s*(?<mes>.*?)\)\s*,\s*2\s*\)\s*\)",
            m => $"((TRY_CONVERT(int,{m.Groups["anio"].Value.Trim()}) * 100) + TRY_CONVERT(int,{m.Groups["mes"].Value.Trim()}))",
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

        // Exact no-format-mask Access shape: TRY_CONVERT(float, FORMAT(anio) + RIGHT('00' + CONVERT(varchar(2), mes), 2))
        sql = Regex.Replace(
            sql,
            @"TRY_CONVERT\(float,FORMAT\((?<anio>[^\)]+)\)\s*\+\s*RIGHT\('00'\s*\+\s*CONVERT\(varchar\(2\),\s*(?<mes>[^\)]+)\),\s*2\)\)",
            m => $"((TRY_CONVERT(int,{m.Groups["anio"].Value.Trim()}) * 100) + TRY_CONVERT(int,{m.Groups["mes"].Value.Trim()}))",
            RegexOptions.IgnoreCase);

        // Final canonical matcher for the remaining Access shape with optional spaces.
        sql = Regex.Replace(
            sql,
            @"TRY_CONVERT\(\s*float\s*,\s*FORMAT\((?<anio>[^\)]*)\)\s*\+\s*RIGHT\(\s*'00'\s*\+\s*CONVERT\(\s*varchar\(2\)\s*,\s*(?<mes>[^\)]*)\)\s*,\s*2\s*\)\s*\)",
            m => $"((TRY_CONVERT(int,{m.Groups["anio"].Value.Trim()}) * 100) + TRY_CONVERT(int,{m.Groups["mes"].Value.Trim()}))",
            RegexOptions.IgnoreCase);

        // Direct string fallback for stubborn variants produced after previous rewrites.
        sql = Regex.Replace(
            sql,
            @"TRY_CONVERT\(float,FORMAT\((?<anio>.*?)\)\s*\+\s*RIGHT\('00'\s*\+\s*CONVERT\(varchar\(2\),\s*(?<mes>.*?)\),\s*2\)\)",
            m => $"((TRY_CONVERT(int,{m.Groups["anio"].Value.Trim()}) * 100) + TRY_CONVERT(int,{m.Groups["mes"].Value.Trim()}))",
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

        // Replace inner year+month concatenation regardless of TRY_CONVERT wrapper shape.
        sql = Regex.Replace(
            sql,
            @"FORMAT\((?<anio>[^\)]+)\)\s*\+\s*RIGHT\(\s*'00'\s*\+\s*CONVERT\(\s*varchar\(2\)\s*,\s*(?<mes>[^\)]+)\)\s*,\s*2\s*\)",
            m => $"((TRY_CONVERT(int,{m.Groups["anio"].Value.Trim()}) * 100) + TRY_CONVERT(int,{m.Groups["mes"].Value.Trim()}))",
            RegexOptions.IgnoreCase);

        // If previous replacement remains wrapped in TRY_CONVERT(float, <int_expr>) remove float cast.
        sql = Regex.Replace(
            sql,
            @"TRY_CONVERT\(\s*float\s*,\s*(?<expr>\(\(TRY_CONVERT\(int,[^\)]*\)\s*\*\s*100\)\s*\+\s*TRY_CONVERT\(int,[^\)]*\)\))\s*\)",
            m => m.Groups["expr"].Value,
            RegexOptions.IgnoreCase);

        return sql;
    }

    private static string ConvertIifExpressions(string sql)
    {
        return ConvertFunctionCalls(sql, "IIF", args =>
        {
            if (args.Count >= 3)
                return $"(CASE WHEN {args[0]} THEN {args[1]} ELSE {args[2]} END)";
            if (args.Count == 2)
                return $"(CASE WHEN {args[0]} THEN {args[1]} ELSE NULL END)";
            return null;
        });
    }

    private static string ConvertSwitchExpressions(string sql)
    {
        return ConvertFunctionCalls(sql, "Switch", args =>
        {
            if (args.Count < 2)
                return null;

            var sb = new StringBuilder("(CASE ");
            var i = 0;
            for (; i + 1 < args.Count; i += 2)
                sb.Append($"WHEN {args[i]} THEN {args[i + 1]} ");

            if (i < args.Count)
                sb.Append($"ELSE {args[i]} ");
            else
                sb.Append("ELSE NULL ");

            sb.Append("END)");
            return sb.ToString();
        });
    }

    private static string ConvertIsNullExpressions(string sql)
    {
        return ConvertFunctionCalls(sql, "IsNull", args =>
        {
            if (args.Count == 1)
                return $"(CASE WHEN {args[0]} IS NULL THEN 1 ELSE 0 END)";
            if (args.Count >= 2)
                return $"ISNULL({args[0]}, {args[1]})";
            return null;
        });
    }

    private static string ConvertFunctionCalls(string sql, string functionName, Func<List<string>, string?> converter)
    {
        var idx = 0;
        while (idx < sql.Length)
        {
            var hit = CultureInvariantIndexOfWord(sql, functionName, idx);
            if (hit < 0)
                break;

            var open = hit + functionName.Length;
            while (open < sql.Length && char.IsWhiteSpace(sql[open])) open++;
            if (open >= sql.Length || sql[open] != '(')
            {
                idx = hit + functionName.Length;
                continue;
            }

            var close = FindClosingParen(sql, open);
            if (close < 0)
                break;

            var inside = sql.Substring(open + 1, close - open - 1);
            var args = SplitTopLevelArgs(inside);
            var replacement = converter(args);
            if (string.IsNullOrWhiteSpace(replacement))
            {
                idx = close + 1;
                continue;
            }

            sql = sql[..hit] + replacement + sql[(close + 1)..];
            idx = hit + replacement.Length;
        }

        return sql;
    }

    private static int CultureInvariantIndexOfWord(string text, string word, int start)
    {
        for (var i = start; i <= text.Length - word.Length; i++)
        {
            if (!text.AsSpan(i, word.Length).Equals(word.AsSpan(), StringComparison.OrdinalIgnoreCase))
                continue;

            var beforeOk = i == 0 || !(char.IsLetterOrDigit(text[i - 1]) || text[i - 1] == '_');
            var afterPos = i + word.Length;
            var afterOk = afterPos >= text.Length || !(char.IsLetterOrDigit(text[afterPos]) || text[afterPos] == '_');
            if (beforeOk && afterOk)
                return i;
        }
        return -1;
    }

    private static int FindClosingParen(string text, int openPos)
    {
        var depth = 0;
        var inString = false;
        for (var i = openPos; i < text.Length; i++)
        {
            var ch = text[i];
            if (ch == '\'' && (i == 0 || text[i - 1] != '\\'))
            {
                inString = !inString;
                continue;
            }
            if (inString)
                continue;

            if (ch == '(') depth++;
            else if (ch == ')')
            {
                depth--;
                if (depth == 0)
                    return i;
            }
        }
        return -1;
    }

    private static List<string> SplitTopLevelArgs(string text)
    {
        var list = new List<string>();
        var depth = 0;
        var inString = false;
        var start = 0;
        for (var i = 0; i < text.Length; i++)
        {
            var ch = text[i];
            if (ch == '\'' && (i == 0 || text[i - 1] != '\\'))
            {
                inString = !inString;
                continue;
            }
            if (inString)
                continue;

            if (ch == '(') depth++;
            else if (ch == ')') depth--;
            else if (ch == ',' && depth == 0)
            {
                list.Add(text[start..i].Trim());
                start = i + 1;
            }
        }
        list.Add(text[start..].Trim());
        return list;
    }

    private static string ConvertDoubleQuotedLiterals(string sql)
    {
        return Regex.Replace(sql, "\"([^\"]*)\"", m =>
        {
            var inner = m.Groups[1].Value.Replace("'", "''");
            return $"'{inner}'";
        });
    }

    private static string NormalizeDeleteSyntax(string sql)
    {
        // Access allows syntaxes like:
        // DELETE * FROM Tabla ...
        // DELETE Campo1, Campo2 FROM Tabla ...
        // DELETE Tabla.* FROM Tabla ...
        // SQL Server expects DELETE FROM Tabla ... (or DELETE alias FROM ... for joins).

        sql = Regex.Replace(sql, @"^\s*DELETE\s+\*\s+FROM\s+", "DELETE FROM ", RegexOptions.IgnoreCase);

        var m = Regex.Match(sql, @"^\s*DELETE\s+(?<target>.+?)\s+FROM\s+(?<rest>.+)$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        if (!m.Success)
            return sql;

        var target = m.Groups["target"].Value.Trim();
        var rest = m.Groups["rest"].Value;

        var looksLikeAccessFieldList = target.Contains(',')
            || target.EndsWith(".*", StringComparison.Ordinal)
            || target.Contains(".", StringComparison.Ordinal) && target.Contains("[", StringComparison.Ordinal);

        if (!looksLikeAccessFieldList)
        {
            // Access style DELETE FROM T AS X -> SQL Server style DELETE X FROM T AS X
            sql = Regex.Replace(
                sql,
                @"^\s*DELETE\s+FROM\s+(?<tbl>\[[^\]]+\]|[A-Za-z_][A-Za-z0-9_]*)\s+AS\s+(?<alias>[A-Za-z_][A-Za-z0-9_]*)\b",
                "DELETE ${alias} FROM ${tbl} AS ${alias}",
                RegexOptions.IgnoreCase);
            return sql;
        }

        return $"DELETE FROM {rest}";
    }

    private static string RewriteAccessUpdateJoin(string sql)
    {
        // Convert Access UPDATE JOIN to SQL Server UPDATE ... SET ... FROM ...
        // Example:
        // UPDATE T1 INNER JOIN T2 ON ... SET T2.Col = ... WHERE ...
        // -> UPDATE T2 SET T2.Col = ... FROM T1 INNER JOIN T2 ON ... WHERE ...

        var m = Regex.Match(
            sql,
            @"^\s*UPDATE\s+(?<fromPart>.+?)\s+SET\s+(?<setPart>.+?)(?<wherePart>\s+WHERE\s+.+)?\s*;?\s*$",
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

        if (!m.Success)
            return sql;

        var fromPart = m.Groups["fromPart"].Value.Trim();
        var setPart = m.Groups["setPart"].Value.Trim();
        var wherePart = m.Groups["wherePart"].Success ? m.Groups["wherePart"].Value : string.Empty;

        // If it's not a JOIN-based UPDATE, keep original.
        if (!Regex.IsMatch(fromPart, @"\bJOIN\b", RegexOptions.IgnoreCase))
            return sql;

        // Determine update target as first left-hand alias/table in SET assignments.
        var firstSet = setPart.Split(',')[0];
        var targetMatch = Regex.Match(firstSet, @"^\s*(?<target>(?:\[[^\]]+\]|[A-Za-z_][A-Za-z0-9_]*)(?:\.(?:\[[^\]]+\]|[A-Za-z_][A-Za-z0-9_]*))?)\s*=", RegexOptions.IgnoreCase);
        if (!targetMatch.Success)
            return sql;

        var target = targetMatch.Groups["target"].Value;
        var targetRoot = target.Contains('.') ? target[..target.IndexOf('.')] : target;

        return $"UPDATE {targetRoot} SET {setPart} FROM {fromPart}{wherePart}";
    }

    private static string NormalizeBareNumericIdentifiersInClauses(string sql)
    {
        return BareNumericIdentifierAfterClauseRegex.Replace(sql, m =>
        {
            var kw = m.Groups["kw"].Value;
            var prefix = m.Groups["prefix"].Value;
            var name = m.Groups["name"].Value;
            return $"{kw} {prefix}[{name}]";
        });
    }

    public static string BuildStoredProcedureSql(AccessQueryDefinition query)
    {
        var transformSql = TryBuildTransformStoredProcedureSql(query);
        if (!string.IsNullOrWhiteSpace(transformSql))
            return transformSql;

        var sb = new StringBuilder();
        var procName = NormalizeProcedureName(query.SourceFile, query.Name);

        sb.AppendLine($"CREATE OR ALTER PROCEDURE [dbo].[{procName}]");
        var procParams = query.Parameters.Where(p => IsAccessParameterName(p.Name)).ToList();
        if (procParams.Count > 0)
        {
            for (var i = 0; i < procParams.Count; i++)
            {
                var p = procParams[i];
                var comma = i < procParams.Count - 1 ? "," : string.Empty;
                sb.AppendLine($"    @{NormalizeParameterName(p.Name)} {MapType(p.AccessType)}{comma}");
            }
        }
        sb.AppendLine("AS");
        sb.AppendLine("BEGIN");
        sb.AppendLine("    SET NOCOUNT ON;");

        var preprocessedSql = PrefixBareParameterTokens(query.RawSql, procParams);
        var sql = TranslateToSqlServer(preprocessedSql);
        foreach (var line in sql.Split('\n'))
        {
            sb.Append("    ");
            sb.AppendLine(line.TrimEnd('\r'));
        }

        sb.AppendLine("END;");
        return sb.ToString();
    }

    private static string? TryBuildTransformStoredProcedureSql(AccessQueryDefinition query)
    {
        var raw = query.RawSql.Trim();
        if (!raw.StartsWith("TRANSFORM", StringComparison.OrdinalIgnoreCase))
            return null;

        var m = TransformRegex.Match(raw);
        if (!m.Success)
            return null;

        var procName = NormalizeProcedureName(query.SourceFile, query.Name);
        var validParams = query.Parameters.Where(p => IsAccessParameterName(p.Name)).ToList();
        var aggExpr = TranslateToSqlServer(PrefixBareParameterTokens(m.Groups["agg"].Value.Trim(), validParams)).TrimEnd(';');
        var fromClause = TranslateToSqlServer(PrefixBareParameterTokens(m.Groups["from"].Value.Trim(), validParams)).TrimEnd(';');
        var groupExpr = TranslateToSqlServer(PrefixBareParameterTokens(m.Groups["group"].Value.Trim(), validParams)).TrimEnd(';');
        var pivotExpr = TranslateToSqlServer(PrefixBareParameterTokens(m.Groups["pivot"].Value.Trim(), validParams)).TrimEnd(';');
        var totalAlias = TryExtractSelectTotalAlias(m.Groups["select"].Value) ?? "Total";

        var sb = new StringBuilder();
        sb.AppendLine($"CREATE OR ALTER PROCEDURE [dbo].[{procName}]");
        if (validParams.Count > 0)
        {
            for (var i = 0; i < validParams.Count; i++)
            {
                var p = validParams[i];
                var comma = i < validParams.Count - 1 ? "," : string.Empty;
                sb.AppendLine($"    @{NormalizeParameterName(p.Name)} {MapType(p.AccessType)}{comma}");
            }
        }

        sb.AppendLine("AS");
        sb.AppendLine("BEGIN");
        sb.AppendLine("    SET NOCOUNT ON;");
        sb.AppendLine("    DECLARE @cols NVARCHAR(MAX);");
        sb.AppendLine("    DECLARE @sumCols NVARCHAR(MAX);");
        sb.AppendLine("    DECLARE @sql NVARCHAR(MAX);");
        sb.AppendLine();

        sb.AppendLine($"    SELECT @cols = STRING_AGG(QUOTENAME(CONVERT(nvarchar(128), pv)), ',')");
        sb.AppendLine($"    FROM (SELECT DISTINCT {pivotExpr} AS pv FROM {fromClause}) d;");
        sb.AppendLine();
        sb.AppendLine($"    SELECT @sumCols = STRING_AGG('ISNULL(' + QUOTENAME(CONVERT(nvarchar(128), pv)) + ',0)', ' + ')");
        sb.AppendLine($"    FROM (SELECT DISTINCT {pivotExpr} AS pv FROM {fromClause}) d;");
        sb.AppendLine();
        sb.AppendLine("    SET @sql = N'SELECT p.*' +");
        sb.AppendLine($"              CASE WHEN @sumCols IS NOT NULL THEN N', ' + @sumCols + N' AS {QuoteForDynamicSqlBracketedAlias(totalAlias)}' ELSE N'' END +");
        sb.AppendLine("              N' FROM (SELECT " + EscapeForSqlLiteral(groupExpr) + ", " + EscapeForSqlLiteral(pivotExpr) + " AS __PivotKey, " + EscapeForSqlLiteral(aggExpr) + " AS __PivotValue FROM " + EscapeForSqlLiteral(fromClause) + ") src '");
        sb.AppendLine("              + N' PIVOT (SUM(__PivotValue) FOR __PivotKey IN (' + ISNULL(@cols, N'') + N')) p';");
        sb.AppendLine();

        if (validParams.Count == 0)
        {
            sb.AppendLine("    EXEC sp_executesql @sql;");
        }
        else
        {
            var paramDef = string.Join(", ", validParams.Select(p => $"@{NormalizeParameterName(p.Name)} {MapType(p.AccessType)}"));
            var paramAssign = string.Join(", ", validParams.Select(p => $"@{NormalizeParameterName(p.Name)}=@{NormalizeParameterName(p.Name)}"));
            sb.AppendLine($"    EXEC sp_executesql @sql, N'{EscapeForSqlLiteral(paramDef)}', {paramAssign};");
        }

        sb.AppendLine("END;");
        return sb.ToString();
    }

    private static string EscapeForSqlLiteral(string s) => s.Replace("'", "''");

    private static string QuoteForDynamicSqlBracketedAlias(string alias)
    {
        var clean = alias.Trim();
        if (clean.StartsWith("[", StringComparison.Ordinal) && clean.EndsWith("]", StringComparison.Ordinal))
            return clean;

        clean = clean.Replace("]", "]]", StringComparison.Ordinal);
        return "[" + clean + "]";
    }

    private static string? TryExtractSelectTotalAlias(string selectClause)
    {
        var m = Regex.Match(selectClause, @"\bSum\s*\(.+?\)\s+AS\s+(?<alias>\[[^\]]+\]|[^,]+)$", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        if (!m.Success)
            return null;

        return m.Groups["alias"].Value.Trim();
    }

    public static string BuildDataObjectSql(AccessQueryDefinition query)
    {
        var validParamsForObject = query.Parameters.Where(p => IsAccessParameterName(p.Name)).ToList();
        var objectKind = validParamsForObject.Count == 0
            ? AccessSqlDataObjectKind.View
            : AccessSqlDataObjectKind.InlineTableValuedFunction;

        if (query.RawSql.TrimStart().StartsWith("TRANSFORM", StringComparison.OrdinalIgnoreCase))
            return string.Empty;

        if (!query.RawSql.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
            return string.Empty;

        var objectName = NormalizeDataObjectName(query.SourceFile, query.Name);
        var preprocessedSql = PrefixBareParameterTokens(query.RawSql, validParamsForObject);
        var sql = TranslateToSqlServer(preprocessedSql).Trim().TrimEnd(';');
        sql = RemoveTrailingOrderByForDataObject(sql);
        sql = RemoveDuplicateSelectColumnsForDataObject(sql);

        if (objectKind == AccessSqlDataObjectKind.View)
        {
            return $"CREATE OR ALTER VIEW [dbo].[{objectName}]\nAS\n{sql};";
        }
        var paramList = string.Join(", ", validParamsForObject.Select(p => $"@{NormalizeParameterName(p.Name)} {MapType(p.AccessType)}"));
        return $"CREATE OR ALTER FUNCTION [dbo].[{objectName}]({paramList})\nRETURNS TABLE\nAS\nRETURN\n(\n{sql}\n)";
    }

    public static string BuildCompatibilityObjectSql(AccessQueryDefinition query)
    {
        if (query.RawSql.TrimStart().StartsWith("TRANSFORM", StringComparison.OrdinalIgnoreCase))
            return string.Empty;

        if (!query.RawSql.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
            return string.Empty;

        var originalName = QuoteObjectName(query.Name);
        var dataObjectName = NormalizeDataObjectName(query.SourceFile, query.Name);
        var validParams = query.Parameters.Where(p => IsAccessParameterName(p.Name)).ToList();

        if (validParams.Count == 0)
        {
            var ddl = $"CREATE VIEW [dbo].[{originalName}] AS SELECT * FROM [dbo].[{dataObjectName}];";
            return $"IF OBJECT_ID('dbo.{EscapeForSqlLiteral(originalName)}') IS NULL EXEC('{EscapeForSqlLiteral(ddl)}')";
        }

        var paramList = string.Join(", ", validParams.Select(p => $"@{NormalizeParameterName(p.Name)} {MapType(p.AccessType)}"));
        var args = string.Join(", ", validParams.Select(p => $"@{NormalizeParameterName(p.Name)}"));
        var func = $"CREATE FUNCTION [dbo].[{originalName}]({paramList})\nRETURNS TABLE\nAS\nRETURN\n(\n    SELECT * FROM [dbo].[{dataObjectName}]({args})\n)";
        return $"IF OBJECT_ID('dbo.{EscapeForSqlLiteral(originalName)}') IS NULL EXEC('{EscapeForSqlLiteral(func)}')";
    }

    public static string ApplyDataObjectReferenceRewrites(string sql, string sourceFile, IReadOnlyList<AccessQueryDefinition> queries)
    {
        var rewritten = sql;
        var queryDefs = queries
            .GroupBy(q => q.Name, StringComparer.OrdinalIgnoreCase)
            .Select(g => g.First())
            .Where(q => q.Parameters.Count == 0)
            .OrderByDescending(q => q.Name.Length)
            .ToList();

        foreach (var q in queryDefs)
        {
            var target = BuildReferenceTarget(sourceFile, q);
            var nameEscaped = Regex.Escape(q.Name);

            // FROM <QueryName> / FROM [QueryName]
            rewritten = Regex.Replace(
                rewritten,
                $@"\bFROM\s+(?:\[)?{nameEscaped}(?:\])?(?=\s|\)|,|$)",
                $"FROM {target}",
                RegexOptions.IgnoreCase);
            rewritten = Regex.Replace(
                rewritten,
                $@"\bFROM\s+\[{nameEscaped}\](?=\s|\)|,|$)",
                $"FROM {target}",
                RegexOptions.IgnoreCase);

            // JOIN <QueryName> / JOIN [QueryName]
            rewritten = Regex.Replace(
                rewritten,
                $@"\bJOIN\s+(?:\[)?{nameEscaped}(?:\])?(?=\s|\)|,|$)",
                $"JOIN {target}",
                RegexOptions.IgnoreCase);
            rewritten = Regex.Replace(
                rewritten,
                $@"\bJOIN\s+\[{nameEscaped}\](?=\s|\)|,|$)",
                $"JOIN {target}",
                RegexOptions.IgnoreCase);

            // Replace remaining qualified column references <QueryName>.Column or [QueryName].Column
            var qualPattern = $@"(?:\[)?{nameEscaped}(?:\])?\.(?<col>\[[^\]]+\]|[A-Za-z_][A-Za-z0-9_]*)";
            var alias = NormalizeAliasFromQueryName(q.Name);
            rewritten = Regex.Replace(
                rewritten,
                qualPattern,
                m => $"{alias}.{m.Groups["col"].Value}",
                RegexOptions.IgnoreCase);

            // Ensure FROM/JOIN targets have deterministic aliases for rewritten column qualifiers.
            rewritten = Regex.Replace(
                rewritten,
                $@"\bFROM\s+{Regex.Escape(target)}(?!(\s+AS\s+|\s+[A-Za-z_][A-Za-z0-9_]*\b))",
                $"FROM {target} AS {alias}",
                RegexOptions.IgnoreCase);
            rewritten = Regex.Replace(
                rewritten,
                $@"\bJOIN\s+{Regex.Escape(target)}(?!(\s+AS\s+|\s+[A-Za-z_][A-Za-z0-9_]*\b))",
                $"JOIN {target} AS {alias}",
                RegexOptions.IgnoreCase);
        }

        return rewritten;
    }

    private static string QuoteObjectName(string name)
    {
        return name.Replace("]", "]]", StringComparison.Ordinal);
    }


    private static string RemoveDuplicateSelectColumnsForDataObject(string sql)
    {
        if (!sql.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
            return sql;

        var selectIdx = CultureInvariantIndexOfWord(sql, "SELECT", 0);
        if (selectIdx < 0)
            return sql;

        var fromIdx = FindTopLevelKeyword(sql, "FROM", selectIdx + 6);
        if (fromIdx < 0)
            return sql;

        var projection = sql[(selectIdx + 6)..fromIdx];
        var items = SplitTopLevelArgs(projection);
        if (items.Count <= 1)
            return sql;

        var kept = new List<string>();
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var item in items)
        {
            var key = GetSelectItemOutputName(item);
            if (seen.Contains(key))
                continue;

            seen.Add(key);
            kept.Add(item);
        }

        if (kept.Count == items.Count)
            return sql;

        var rebuilt = "SELECT " + string.Join(", ", kept.Select(x => x.Trim())) + " ";
        return rebuilt + sql[fromIdx..];
    }

    private static string GetSelectItemOutputName(string item)
    {
        var trimmed = item.Trim();

        var asMatch = Regex.Match(trimmed, @"\bAS\s+(?<alias>\[[^\]]+\]|[A-Za-z_][A-Za-z0-9_]*)\s*$", RegexOptions.IgnoreCase);
        if (asMatch.Success)
            return asMatch.Groups["alias"].Value.Trim('[', ']');

        var lastToken = Regex.Match(trimmed, @"(?:\.|\s)(?<name>\[[^\]]+\]|[A-Za-z_][A-Za-z0-9_]*)\s*$", RegexOptions.IgnoreCase);
        if (lastToken.Success)
            return lastToken.Groups["name"].Value.Trim('[', ']');

        return trimmed;
    }

    private static int FindTopLevelKeyword(string text, string keyword, int start)
    {
        var inString = false;
        var depth = 0;
        for (var i = start; i <= text.Length - keyword.Length; i++)
        {
            var ch = text[i];
            if (ch == '\'' && (i == 0 || text[i - 1] != '\\'))
            {
                inString = !inString;
                continue;
            }
            if (inString)
                continue;

            if (ch == '(') { depth++; continue; }
            if (ch == ')') { depth--; continue; }
            if (depth != 0)
                continue;

            if (!text.AsSpan(i, keyword.Length).Equals(keyword.AsSpan(), StringComparison.OrdinalIgnoreCase))
                continue;

            var beforeOk = i == 0 || !(char.IsLetterOrDigit(text[i - 1]) || text[i - 1] == '_');
            var afterPos = i + keyword.Length;
            var afterOk = afterPos >= text.Length || !(char.IsLetterOrDigit(text[afterPos]) || text[afterPos] == '_');
            if (beforeOk && afterOk)
                return i;
        }

        return -1;
    }

    private static string BuildReferenceTarget(string sourceFile, AccessQueryDefinition query)
    {
        var objectName = NormalizeDataObjectName(sourceFile, query.Name);
        return $"[{objectName}]";
    }

    private static string NormalizeAliasFromQueryName(string queryName)
    {
        var alias = Regex.Replace(queryName, @"[^A-Za-z0-9_]", "_");
        if (string.IsNullOrWhiteSpace(alias))
            alias = "q";
        if (!char.IsLetter(alias[0]) && alias[0] != '_')
            alias = "q_" + alias;
        return alias;
    }

    private static string RemoveTrailingOrderByForDataObject(string sql)
    {
        // SQL Server does not allow ORDER BY in views/iTVFs unless TOP/OFFSET/FOR XML are present.
        // Keep ordering behavior in wrapper procedures; strip it from data objects.
        return Regex.Replace(
            sql,
            @"\s+ORDER\s+BY\s+[\s\S]*$",
            string.Empty,
            RegexOptions.IgnoreCase);
    }

    private static string NormalizeProcedureName(string sourceFile, string queryName)
    {
        var db = Path.GetFileNameWithoutExtension(sourceFile)
            .Replace(".mdb-specs", string.Empty, StringComparison.OrdinalIgnoreCase)
            .Replace("(", string.Empty)
            .Replace(")", string.Empty)
            .Replace("-", "_")
            .Replace(" ", "_");

        var q = queryName
            .Replace(" ", "_")
            .Replace("-", "_")
            .Replace("/", "_")
            .Replace("\\", "_")
            .Replace(".", "_")
            .Replace("<", "_")
            .Replace(">", "_")
            .Replace("#", "_num_")
            .Replace("%", "pct")
            .Replace("á", "a", StringComparison.OrdinalIgnoreCase)
            .Replace("é", "e", StringComparison.OrdinalIgnoreCase)
            .Replace("í", "i", StringComparison.OrdinalIgnoreCase)
            .Replace("ó", "o", StringComparison.OrdinalIgnoreCase)
            .Replace("ú", "u", StringComparison.OrdinalIgnoreCase)
            .Replace("ñ", "n", StringComparison.OrdinalIgnoreCase);

        return $"acc_{db}_{q}";
    }

    private static string NormalizeParameterName(string parameterName)
    {
        var p = parameterName
            .Replace("á", "a", StringComparison.OrdinalIgnoreCase)
            .Replace("é", "e", StringComparison.OrdinalIgnoreCase)
            .Replace("í", "i", StringComparison.OrdinalIgnoreCase)
            .Replace("ó", "o", StringComparison.OrdinalIgnoreCase)
            .Replace("ú", "u", StringComparison.OrdinalIgnoreCase)
            .Replace("ñ", "n", StringComparison.OrdinalIgnoreCase);

        p = Regex.Replace(p, @"[^A-Za-z0-9_]", "_");
        if (string.IsNullOrWhiteSpace(p))
            p = "pParam";
        if (!char.IsLetter(p[0]) && p[0] != '_')
            p = "p_" + p;

        return p;
    }

    private static bool IsAccessParameterName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;
        return Regex.IsMatch(name, @"^p[A-Z]\w*$");
    }

    public static string NormalizeDataObjectName(string sourceFile, string queryName)
    {
        return NormalizeProcedureName(sourceFile, queryName) + "_q";
    }

    private static string MapType(string accessType)
    {
        return accessType.ToLowerInvariant() switch
        {
            "long" => "INT",
            "integer" => "SMALLINT",
            "byte" => "TINYINT",
            "single" => "REAL",
            "double" => "FLOAT",
            "datetime" => "DATETIME2(0)",
            "text" => "NVARCHAR(MAX)",
            _ => "NVARCHAR(MAX)"
        };
    }

    public static string NormalizeFinalScript(string script)
    {
        // Normalize common mojibake/accented identifier artifacts that break SQL parsing.
        script = script
            .Replace("AÃ±o", "Anio", StringComparison.OrdinalIgnoreCase)
            .Replace("Año", "Anio", StringComparison.OrdinalIgnoreCase)
            .Replace("Ã±", "n", StringComparison.OrdinalIgnoreCase)
            .Replace("á", "a", StringComparison.OrdinalIgnoreCase)
            .Replace("é", "e", StringComparison.OrdinalIgnoreCase)
            .Replace("í", "i", StringComparison.OrdinalIgnoreCase)
            .Replace("ó", "o", StringComparison.OrdinalIgnoreCase)
            .Replace("ú", "u", StringComparison.OrdinalIgnoreCase);

        // Final hardening pass for residual year/month numeric expressions.
        script = Regex.Replace(
            script,
            @"TRY_CONVERT\(float,FORMAT\((?<anio>[^\)]+)\)\s*\+\s*RIGHT\('00'\s*\+\s*CONVERT\(varchar\(2\),\s*(?<mes>[^\)]+)\),\s*2\)\)",
            m => $"((TRY_CONVERT(int,{m.Groups["anio"].Value.Trim()}) * 100) + TRY_CONVERT(int,{m.Groups["mes"].Value.Trim()}))",
            RegexOptions.IgnoreCase);

        // Targeted semantic rewrite: parameterized nested Access query 200_Imponible.
        script = Regex.Replace(script, @"FROM\s+\[200_Imponible\](?!\s*\()", "FROM [acc_sgpa_200_Imponible_q](@pMes) AS q_200_Imponible", RegexOptions.IgnoreCase);
        script = Regex.Replace(script, @"JOIN\s+\[200_Imponible\](?!\s*\()", "JOIN [acc_sgpa_200_Imponible_q](@pMes) AS q_200_Imponible", RegexOptions.IgnoreCase);
        script = Regex.Replace(script, @"\[200_Imponible\]\.", "q_200_Imponible.", RegexOptions.IgnoreCase);

        // Targeted semantic rewrite: nested parameterized queries used by 200_PagarMutualista and 201_PagarMutualista.
        script = Regex.Replace(script, @"FROM\s+\[200_Imponible_6_Meses\](?!\s*\()", "FROM [acc_sgpa_200_Imponible_6_Meses_q](@pMes, @pMesIni, @pSMN)", RegexOptions.IgnoreCase);
        script = Regex.Replace(script, @"JOIN\s+\[200_Imponible_6_Meses\](?!\s*\()", "JOIN [acc_sgpa_200_Imponible_6_Meses_q](@pMes, @pMesIni, @pSMN)", RegexOptions.IgnoreCase);
        script = Regex.Replace(script, @"FROM\s+\[200_Imponible_Ult_Mes\](?!\s*\()", "FROM [acc_sgpa_200_Imponible_Ult_Mes_q](@pMes, @pSMN)", RegexOptions.IgnoreCase);
        script = Regex.Replace(script, @"JOIN\s+\[200_Imponible_Ult_Mes\](?!\s*\()", "JOIN [acc_sgpa_200_Imponible_Ult_Mes_q](@pMes, @pSMN)", RegexOptions.IgnoreCase);

        // Targeted aggregate alias fixes in legacy SubsidioImponible summaries.
        script = script.Replace(
            "(CASE WHEN Dias>0 THEN Importe/Dias ELSE 0 END) AS Promedio",
            "(CASE WHEN Sum(SubsidioImponible.Dias)>0 THEN Sum(SubsidioImponible.Importe)/Sum(SubsidioImponible.Dias) ELSE 0 END) AS Promedio",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "Importe/180 AS Promedio",
            "Sum(SubsidioImponible.Importe)/180 AS Promedio",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "TRY_CONVERT(datetime2,CONVERT(nvarchar(max),(CASE WHEN FORMAT(FechaNacimiento,'dd/mm')='29/02' And YEAR(CAST(GETDATE() AS date)) % 4<>0 THEN 28 ELSE DAY(FechaNacimiento) END)) + FORMAT(FechaNacimiento,'/mm/') + YEAR(CAST(GETDATE() AS date)))",
            "DATEFROMPARTS(YEAR(CAST(GETDATE() AS date)), MONTH(FechaNacimiento), CASE WHEN FORMAT(FechaNacimiento,'dd/mm')='29/02' And YEAR(CAST(GETDATE() AS date)) % 4<>0 THEN 28 ELSE DAY(FechaNacimiento) END)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "DATEDIFF(year,FechaNacimiento,CAST(GETDATE() AS date))-(CASE WHEN FechaHoy<=CAST(GETDATE() AS date) THEN 0 ELSE 1 END)",
            "DATEDIFF(year,FechaNacimiento,CAST(GETDATE() AS date))-(CASE WHEN DATEFROMPARTS(YEAR(CAST(GETDATE() AS date)), MONTH(FechaNacimiento), CASE WHEN FORMAT(FechaNacimiento,'dd/mm')='29/02' And YEAR(CAST(GETDATE() AS date)) % 4<>0 THEN 28 ELSE DAY(FechaNacimiento) END)<=CAST(GETDATE() AS date) THEN 0 ELSE 1 END)",
            StringComparison.OrdinalIgnoreCase);

        // Fix TRY_CONVERT(float, DATEADD(month,n,<base>)) to yyyymm numeric expression.
        script = Regex.Replace(
            script,
            @"TRY_CONVERT\(\s*float\s*,\s*(?<expr>DATEADD\(\s*month\s*,\s*-?\d+\s*,\s*(?:CAST\(GETDATE\(\)\s+AS\s+date\)|@[A-Za-z_][A-Za-z0-9_]*)\s*\))\s*\)",
            m => $"(YEAR({m.Groups["expr"].Value}) * 100 + MONTH({m.Groups["expr"].Value}))",
            RegexOptions.IgnoreCase);

        // Targeted cleanup for Certificacion range predicate boolean artifact.
        script = Regex.Replace(
            script,
            @"\(\s*\[Certificacion\]\.\[FechaIni\]\s*<=\s*@pFechaFin\s+And\s+\[Certificacion\]\.\[FechaFin\]\s*>=\s*@pFechaIni\s*\)\s*=\s*1",
            "([Certificacion].[FechaIni]<=@pFechaFin And [Certificacion].[FechaFin]>=@pFechaIni)",
            RegexOptions.IgnoreCase);

        // Access Excel export syntax unsupported in SQL Server: SELECT ... INTO <table> IN 'file' [Excel ...]
        script = Regex.Replace(
            script,
            @"\s+IN\s+'[^']+'\s*\[[^\]]+\]",
            string.Empty,
            RegexOptions.IgnoreCase);

        // Remove legacy SELECT ... INTO patterns in reports to keep script parsable.
        script = Regex.Replace(
            script,
            @"\bSELECT\s+(?<cols>[\s\S]+?)\s+INTO\s+(?<dest>\w+)\b",
            "SELECT ${cols}",
            RegexOptions.IgnoreCase);

        // Inject parameters when numeric-named Access query references a TVF without arguments.
        script = Regex.Replace(
            script,
            @"FROM\s+\[220_AfiliadoImponibleMes\]\b",
            "FROM [220_AfiliadoImponibleMes](@pCI, @pMes, @pMesIni)",
            RegexOptions.IgnoreCase);
        script = script.Replace(
            "FROM [220_AfiliadoImponibleMes]",
            "FROM [220_AfiliadoImponibleMes](@pCI, @pMes, @pMesIni)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [250_ActivosXEmpresaAUnaFecha]",
            "FROM [250_ActivosXEmpresaAUnaFecha](@pFecha)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "FROM [250_AportantesAUnMes]",
            "FROM [250_AportantesAUnMes](@pAnioMes)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [250_AportantesAUnMes]",
            "JOIN [250_AportantesAUnMes](@pAnioMes)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [300_TrabajaActivo]",
            "FROM [300_TrabajaActivo](@pMes)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [300_TrabajaActivo]",
            "JOIN [300_TrabajaActivo](@pMes)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [300_AfiliadoAporteOk]",
            "FROM [300_AfiliadoAporteOk](@pMesIniImp)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [300_AfiliadoAporteOk]",
            "JOIN [300_AfiliadoAporteOk](@pMesIniImp, @pMesFin)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "FROM [300_AfiliadoAporteOk]",
            "FROM [300_AfiliadoAporteOk](@pMesIniImp, @pMesFin)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [300_AfiliadoDiasImporte]",
            "FROM [300_AfiliadoDiasImporte](@pCI, @pMesIni, @pMesFin, @pLiquidar, @pDias, @pMes, @pMesIniImp)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [300_AfiliadoDiasImporte]",
            "JOIN [300_AfiliadoDiasImporte](@pCI, @pMesIni, @pMesFin, @pLiquidar, @pDias, @pMes, @pMesIniImp)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [801_CI_Todos]",
            "FROM [801_CI_Todos](@pMes, @pMesIni, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [801_CI_Todos]",
            "JOIN [801_CI_Todos](@pMes, @pMesIni, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [801_Promedio_Ult6]",
            "FROM [801_Promedio_Ult6](@pMes, @pMesIni, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [801_Promedio_Ult6]",
            "JOIN [801_Promedio_Ult6](@pMes, @pMesIni, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [801_Promedio_UltMes]",
            "FROM [801_Promedio_UltMes](@pMes, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [801_Promedio_UltMes]",
            "JOIN [801_Promedio_UltMes](@pMes, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [802__0_Ult6]",
            "FROM [802__0_Ult6](@pMes, @pMesIni, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [802__0_Ult6]",
            "JOIN [802__0_Ult6](@pMes, @pMesIni, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [802_>0_Ult6]",
            "FROM [802_>0_Ult6](@pMes, @pMesIni, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [802_>0_Ult6]",
            "JOIN [802_>0_Ult6](@pMes, @pMesIni, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [802_>0_UltMes]",
            "FROM [802_>0_UltMes](@pMes, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [802_>0_UltMes]",
            "JOIN [802_>0_UltMes](@pMes, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [802_Ult6]",
            "FROM [802_Ult6](@pMes, @pMesIni, @pSMN, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [802_Ult6]",
            "JOIN [802_Ult6](@pMes, @pMesIni, @pSMN, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [802_UltMes]",
            "FROM [802_UltMes](@pMes, @pSMN, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [802_UltMes]",
            "JOIN [802_UltMes](@pMes, @pSMN, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [803_Ult6]",
            "FROM [803_Ult6](@pMes, @pMesIni, @pSMN, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [803_Ult6]",
            "JOIN [803_Ult6](@pMes, @pMesIni, @pSMN, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [803_UltMes]",
            "FROM [803_UltMes](@pMes, @pSMN, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [803_UltMes]",
            "JOIN [803_UltMes](@pMes, @pSMN, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [804_>0_Ult6]",
            "FROM [804_>0_Ult6](@pMes, @pMesIni, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [804_>0_Ult6]",
            "JOIN [804_>0_Ult6](@pMes, @pMesIni, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [804_>0_UltMes]",
            "FROM [804_>0_UltMes](@pMes, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [804_>0_UltMes]",
            "JOIN [804_>0_UltMes](@pMes, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [804_Ult6]",
            "FROM [804_Ult6](@pMes, @pMesIni, @pSMN, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [804_Ult6]",
            "JOIN [804_Ult6](@pMes, @pMesIni, @pSMN, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [804_UltMes]",
            "FROM [804_UltMes](@pMes, @pSMN, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [804_UltMes]",
            "JOIN [804_UltMes](@pMes, @pSMN, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [811_Afiliado_125_Pct_Ult6]",
            "FROM [811_Afiliado_125_Pct_Ult6](@pMes, @pMesIni, @pSMN, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [811_Afiliado_125_Pct_Ult6]",
            "JOIN [811_Afiliado_125_Pct_Ult6](@pMes, @pMesIni, @pSMN, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "FROM [811_Afiliado_125_Pct_UltMes]",
            "FROM [811_Afiliado_125_Pct_UltMes](@pMes, @pSMN, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [811_Afiliado_125_Pct_UltMes]",
            "JOIN [811_Afiliado_125_Pct_UltMes](@pMes, @pSMN, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [811_Afiliado<125_Pct_Ult6]",
            "FROM [811_Afiliado<125_Pct_Ult6](@pMes, @pMesIni, @pSMN, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [811_Afiliado<125_Pct_Ult6]",
            "JOIN [811_Afiliado<125_Pct_Ult6](@pMes, @pMesIni, @pSMN, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "FROM [811_Afiliado<125_Pct_UltMes]",
            "FROM [811_Afiliado<125_Pct_UltMes](@pMes, @pSMN, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [811_Afiliado<125_Pct_UltMes]",
            "JOIN [811_Afiliado<125_Pct_UltMes](@pMes, @pSMN, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [201_PagarMutualista]",
            "FROM [201_PagarMutualista](@pMes, @pMesIni, @pSMN)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [201_PagarMutualista]",
            "JOIN [201_PagarMutualista](@pMes, @pMesIni, @pSMN)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [805_CertificacionesxAnio]",
            "FROM [805_CertificacionesxAnio](@pFechaIni, @pFechaFin)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [805_CertificacionesxAnio]",
            "JOIN [805_CertificacionesxAnio](@pFechaIni, @pFechaFin)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [100_CargadosHL]",
            "FROM [100_CargadosHL](@pMes, @pAnio, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [100_CargadosHL]",
            "JOIN [100_CargadosHL](@pMes, @pAnio, @pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [101_ImponibleMes]",
            "FROM [101_ImponibleMes](@pMes, @pAno)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [101_ImponibleMes]",
            "JOIN [101_ImponibleMes](@pMes, @pAno)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace("FROM [210_TotTributo]", "FROM [210_TotTributo](@pMes, @pAnio, @pLiquidar)", StringComparison.OrdinalIgnoreCase);
        script = script.Replace("JOIN [210_TotTributo]", "JOIN [210_TotTributo](@pMes, @pAnio, @pLiquidar)", StringComparison.OrdinalIgnoreCase);
        script = script.Replace("FROM [210_TotImpEmp]", "FROM [210_TotImpEmp](@pMes, @pAnio)", StringComparison.OrdinalIgnoreCase);
        script = script.Replace("JOIN [210_TotImpEmp]", "JOIN [210_TotImpEmp](@pMes, @pAnio)", StringComparison.OrdinalIgnoreCase);
        script = script.Replace("[210_TotImpEmp] RIGHT JOIN", "[210_TotImpEmp](@pMes, @pAnio) RIGHT JOIN", StringComparison.OrdinalIgnoreCase);
        script = script.Replace("FROM [210_MontoGrabado]", "FROM [210_MontoGrabado](@pMes, @pAnio, @pLiquidar)", StringComparison.OrdinalIgnoreCase);
        script = script.Replace("JOIN [210_MontoGrabado]", "JOIN [210_MontoGrabado](@pMes, @pAnio, @pLiquidar)", StringComparison.OrdinalIgnoreCase);
        script = script.Replace("[210_MontoGrabado] RIGHT JOIN", "[210_MontoGrabado](@pMes, @pAnio, @pLiquidar) RIGHT JOIN", StringComparison.OrdinalIgnoreCase);
        script = script.Replace("FROM [210_ImpRetTotal]", "FROM [210_ImpRetTotal](@pMes, @pAnio, @pLiquidar)", StringComparison.OrdinalIgnoreCase);
        script = script.Replace("JOIN [210_ImpRetTotal]", "JOIN [210_ImpRetTotal](@pMes, @pAnio, @pLiquidar)", StringComparison.OrdinalIgnoreCase);
        script = script.Replace("[210_ImpRetTotal] RIGHT JOIN", "[210_ImpRetTotal](@pMes, @pAnio, @pLiquidar) RIGHT JOIN", StringComparison.OrdinalIgnoreCase);
        script = script.Replace("FROM [210_ImpRetPatronal]", "FROM [210_ImpRetPatronal](@pMes, @pAnio, @pLiquidar)", StringComparison.OrdinalIgnoreCase);
        script = script.Replace("JOIN [210_ImpRetPatronal]", "JOIN [210_ImpRetPatronal](@pMes, @pAnio, @pLiquidar)", StringComparison.OrdinalIgnoreCase);
        script = script.Replace("[210_ImpRetPatronal] RIGHT JOIN", "[210_ImpRetPatronal](@pMes, @pAnio, @pLiquidar) RIGHT JOIN", StringComparison.OrdinalIgnoreCase);
        script = script.Replace("FROM [210_ImpRetObrero]", "FROM [210_ImpRetObrero](@pMes, @pAnio, @pLiquidar)", StringComparison.OrdinalIgnoreCase);
        script = script.Replace("JOIN [210_ImpRetObrero]", "JOIN [210_ImpRetObrero](@pMes, @pAnio, @pLiquidar)", StringComparison.OrdinalIgnoreCase);
        script = script.Replace("[210_ImpRetObrero] RIGHT JOIN", "[210_ImpRetObrero](@pMes, @pAnio, @pLiquidar) RIGHT JOIN", StringComparison.OrdinalIgnoreCase);
        script = script.Replace("FROM [210_SubsidioCantidad]", "FROM [210_SubsidioCantidad](@pMes, @pAnio, @pLiquidar)", StringComparison.OrdinalIgnoreCase);
        script = script.Replace("JOIN [210_SubsidioCantidad]", "JOIN [210_SubsidioCantidad](@pMes, @pAnio, @pLiquidar)", StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [460_IMS_Actual]",
            "FROM [460_IMS_Actual](@pAnioMes)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            ",[460_IMS_Actual]",
            ",[460_IMS_Actual](@pAnioMes)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "q_q_460_Imponible.",
            "acc_sgpa_460_Imponible_q.",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [506_Rpt_LiquidacionBPS]",
            "FROM [506_Rpt_LiquidacionBPS](@pMes, @pAnio)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [506_Rpt_LiquidacionBPS]",
            "JOIN [506_Rpt_LiquidacionBPS](@pMes, @pAnio)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [765_CertificacionContinua]",
            "FROM [765_CertificacionContinua](@pFecha)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "FROM [765_CertificacionEmpalma]",
            "FROM [765_CertificacionEmpalma](@pFecha)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [800_AfiliadoImponible_Mes]",
            "FROM [800_AfiliadoImponible_Mes](@pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [800_AfiliadoImponible_Mes]",
            "JOIN [800_AfiliadoImponible_Mes](@pCodEmpresa)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [800_AfiliadoImponible_Mes_Fecha]",
            "FROM [800_AfiliadoImponible_Mes_Fecha](@pCodEmpresa, @pMes)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [800_AfiliadoImponible_Mes_Fecha]",
            "JOIN [800_AfiliadoImponible_Mes_Fecha](@pCodEmpresa, @pMes)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [813_CertificacionAfeccionDistintas]",
            "FROM [813_CertificacionAfeccionDistintas](@pFechaIni, @pFechaFin)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [813_CertificacionAfeccionDistintas]",
            "JOIN [813_CertificacionAfeccionDistintas](@pFechaIni, @pFechaFin)",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "FROM [830_CantidadPorPuesto]",
            "FROM [830_CantidadPorPuesto](@pFecha)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [830_CantidadPorPuesto]",
            "JOIN [830_CantidadPorPuesto](@pFecha)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "FROM [830_CantidadPorPuestoNo0]",
            "FROM [830_CantidadPorPuestoNo0](@pFecha, @pMes)",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "JOIN [830_CantidadPorPuestoNo0]",
            "JOIN [830_CantidadPorPuestoNo0](@pFecha, @pMes)",
            StringComparison.OrdinalIgnoreCase);

        // Keep SubsidioImponible-based Promedio for SubsidioEmpresaAnteriorVacia.
        // (Do not replace it with Imponible aggregates.)
        script = script.Replace(
            "SELECT Imponible.CI, Sum(Imponible.DiasTrabajados) AS Dias, Imponible.CodEmpresa, Sum(Imponible.Importe) AS Importe, (CASE WHEN Sum(SubsidioImponible.Dias)>0 THEN Sum(SubsidioImponible.Importe)/Sum(SubsidioImponible.Dias) ELSE 0 END) AS Promedio",
            "SELECT Imponible.CI, Sum(Imponible.DiasTrabajados) AS Dias, Imponible.CodEmpresa, Sum(Imponible.Importe) AS Importe, (CASE WHEN Sum(Imponible.DiasTrabajados)>0 THEN Sum(Imponible.Importe)/Sum(Imponible.DiasTrabajados) ELSE 0 END) AS Promedio",
            StringComparison.OrdinalIgnoreCase);

        // Access date arithmetic: FechaBaja - 1 -> DATEADD(day,-1,FechaBaja)
        script = Regex.Replace(
            script,
            @"\bFechaBaja\s*-\s*1\b",
            "DATEADD(day,-1,FechaBaja)",
            RegexOptions.IgnoreCase);

        // Keep generic INSERT handling untouched; specific legacy fixes are applied in Program.cs final rewrites.

        // Normalize DELETE FROM <table> AS <alias> syntax for SQL Server.
        script = Regex.Replace(
            script,
            @"DELETE\s+FROM\s+(?<tbl>\w+)\s+AS\s+(?<alias>\w+)",
            "DELETE ${alias} FROM ${tbl} AS ${alias}",
            RegexOptions.IgnoreCase);

        // Fix predicate artifact: (expr AND col)=(CASE ...) -> (expr) AND col=(CASE ...)
        script = Regex.Replace(
            script,
            @"\(\s*(?<expr>\(\(TRY_CONVERT\([^\)]*\)\s+Between\s+@pMesIni\s+And\s+@pMes\)\)\s+AND)\s+(?<col>Rs_TrabajaActivo\.CodEmpresa)\)\s*=\s*\(CASE\s+WHEN\s+@pCodEmpresa>0\s+THEN\s+@pCodEmpresa\s+ELSE\s+Rs_TrabajaActivo\.\[CodEmpresa\]\s+END\)",
            "(${expr} ${col}) AND ${col}=(CASE WHEN @pCodEmpresa>0 THEN @pCodEmpresa ELSE Rs_TrabajaActivo.[CodEmpresa] END)",
            RegexOptions.IgnoreCase);

        // Remove TRY_CONVERT wrapper in HAVING AVG(...) comparisons.
        script = Regex.Replace(
            script,
            @"TRY_CONVERT\(float,\s*Avg\((?<expr>[^\)]*)\)\)",
            "Avg(${expr})",
            RegexOptions.IgnoreCase);

        // Expand iCol alias-dependent expressions in Rs_Bps* views.
        script = Regex.Replace(
            script,
            @"CASE\s+WHEN\s+iCol>0\s+THEN\s+LEFT\((?<tbl>Bps\d)\.CI,iCol-1\)\s*\+\s*SUBSTRING\((?<tbl2>Bps\d)\.CI,iCol\+1,1\)\s+ELSE\s+(?<tbl3>Bps\d)\.CI\s+END",
            m => $"CASE WHEN CHARINDEX('-', {m.Groups["tbl"].Value}.CI)>0 THEN LEFT({m.Groups["tbl"].Value}.CI,CHARINDEX('-', {m.Groups["tbl"].Value}.CI)-1) + SUBSTRING({m.Groups["tbl2"].Value}.CI,CHARINDEX('-', {m.Groups["tbl2"].Value}.CI)+1,1) ELSE {m.Groups["tbl3"].Value}.CI END",
            RegexOptions.IgnoreCase);

        script = Regex.Replace(script, @"q_q_400_Suma_Importe\.", "acc_sgpa_400_Suma_Importe_q.", RegexOptions.IgnoreCase);
        script = Regex.Replace(script, @"q_q_400_Suma_Puestos\.", "acc_sgpa_400_Suma_Puestos_q.", RegexOptions.IgnoreCase);

        // Access date + integer (days) semantics -> DATEADD(day, ...)
        script = script.Replace(
            "[600_Afiliado_Certificado].F_Ult_Prorroga +[600_Afiliado_Certificado].DiasUltPro AS F_Ult_Prorroga",
            "DATEADD(day,[600_Afiliado_Certificado].DiasUltPro,[600_Afiliado_Certificado].F_Ult_Prorroga) AS F_Ult_Prorroga",
            StringComparison.OrdinalIgnoreCase);

        // Fix missing aliases for aggregated views without explicit AS.
        script = Regex.Replace(
            script,
            @"SELECT\s+q_q_400_Suma_Importe\.(?<col>Mes|Anio|Importe)",
            "SELECT acc_sgpa_400_Suma_Importe_q.${col}",
            RegexOptions.IgnoreCase);
        script = Regex.Replace(
            script,
            @"Avg\(q_q_400_Suma_Importe\.Importe\)",
            "Avg(acc_sgpa_400_Suma_Importe_q.Importe)",
            RegexOptions.IgnoreCase);
        script = Regex.Replace(
            script,
            @"GROUP BY q_q_400_Suma_Importe\.(Mes|Anio)",
            "GROUP BY acc_sgpa_400_Suma_Importe_q.$1",
            RegexOptions.IgnoreCase);

        script = Regex.Replace(
            script,
            @"SELECT\s+q_q_400_Suma_Puestos\.(?<col>Mes|Anio|Importe)",
            "SELECT acc_sgpa_400_Suma_Puestos_q.${col}",
            RegexOptions.IgnoreCase);
        script = Regex.Replace(
            script,
            @"Avg\(q_q_400_Suma_Puestos\.Importe\)",
            "Avg(acc_sgpa_400_Suma_Puestos_q.Importe)",
            RegexOptions.IgnoreCase);
        script = Regex.Replace(
            script,
            @"GROUP BY q_q_400_Suma_Puestos\.(Mes|Anio)",
            "GROUP BY acc_sgpa_400_Suma_Puestos_q.$1",
            RegexOptions.IgnoreCase);

        script = script.Replace(
            "q_q_400_Suma_Importe.",
            "acc_sgpa_400_Suma_Importe_q.",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "q_q_400_Suma_Puestos.",
            "acc_sgpa_400_Suma_Puestos_q.",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "SELECT q_q_400_Suma_Importe.Mes, q_q_400_Suma_Importe.Anio, Avg(q_q_400_Suma_Importe.Importe) AS Importe\r\nFROM [acc_sgpa_400_Suma_Importe_q]\r\nGROUP BY q_q_400_Suma_Importe.Mes, q_q_400_Suma_Importe.Anio;\r\n",
            "SELECT acc_sgpa_400_Suma_Importe_q.Mes, acc_sgpa_400_Suma_Importe_q.Anio, Avg(acc_sgpa_400_Suma_Importe_q.Importe) AS Importe\r\nFROM [acc_sgpa_400_Suma_Importe_q]\r\nGROUP BY acc_sgpa_400_Suma_Importe_q.Mes, acc_sgpa_400_Suma_Importe_q.Anio;\r\n",
            StringComparison.OrdinalIgnoreCase);
        script = script.Replace(
            "SELECT q_q_400_Suma_Puestos.Mes, q_q_400_Suma_Puestos.Anio, Avg(q_q_400_Suma_Puestos.Importe) AS Importe\r\nFROM [acc_sgpa_400_Suma_Puestos_q]\r\nGROUP BY q_q_400_Suma_Puestos.Mes, q_q_400_Suma_Puestos.Anio;\r\n",
            "SELECT acc_sgpa_400_Suma_Puestos_q.Mes, acc_sgpa_400_Suma_Puestos_q.Anio, Avg(acc_sgpa_400_Suma_Puestos_q.Importe) AS Importe\r\nFROM [acc_sgpa_400_Suma_Puestos_q]\r\nGROUP BY acc_sgpa_400_Suma_Puestos_q.Mes, acc_sgpa_400_Suma_Puestos_q.Anio;\r\n",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "CREATE OR ALTER VIEW [dbo].[acc_sgpa_400_Promedio_Mes_q]\r\nAS\r\nSELECT q_q_400_Suma_Importe.Mes, q_q_400_Suma_Importe.Anio, Avg(q_q_400_Suma_Importe.Importe) AS Importe\r\nFROM [acc_sgpa_400_Suma_Importe_q]\r\nGROUP BY q_q_400_Suma_Importe.Mes, q_q_400_Suma_Importe.Anio;\r\n",
            "CREATE OR ALTER VIEW [dbo].[acc_sgpa_400_Promedio_Mes_q]\r\nAS\r\nSELECT q_q_400_Suma_Importe.Mes, q_q_400_Suma_Importe.Anio, Avg(q_q_400_Suma_Importe.Importe) AS Importe\r\nFROM [acc_sgpa_400_Suma_Importe_q] AS q_q_400_Suma_Importe\r\nGROUP BY q_q_400_Suma_Importe.Mes, q_q_400_Suma_Importe.Anio;\r\n",
            StringComparison.OrdinalIgnoreCase);

        script = script.Replace(
            "CREATE OR ALTER VIEW [dbo].[acc_sgpa_400_Promedio_Mes_Puesto_q]\r\nAS\r\nSELECT q_q_400_Suma_Puestos.Mes, q_q_400_Suma_Puestos.Anio, Avg(q_q_400_Suma_Puestos.Importe) AS Importe\r\nFROM [acc_sgpa_400_Suma_Puestos_q]\r\nGROUP BY q_q_400_Suma_Puestos.Mes, q_q_400_Suma_Puestos.Anio;\r\n",
            "CREATE OR ALTER VIEW [dbo].[acc_sgpa_400_Promedio_Mes_Puesto_q]\r\nAS\r\nSELECT q_q_400_Suma_Puestos.Mes, q_q_400_Suma_Puestos.Anio, Avg(q_q_400_Suma_Puestos.Importe) AS Importe\r\nFROM [acc_sgpa_400_Suma_Puestos_q] AS q_q_400_Suma_Puestos\r\nGROUP BY q_q_400_Suma_Puestos.Mes, q_q_400_Suma_Puestos.Anio;\r\n",
            StringComparison.OrdinalIgnoreCase);

        return script;
    }
}
