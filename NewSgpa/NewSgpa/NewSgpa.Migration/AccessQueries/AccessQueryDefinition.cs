namespace NewSgpa.Migration.AccessQueries;

public sealed class AccessQueryDefinition
{
    public required string SourceFile { get; init; }
    public required string Name { get; init; }
    public required string RawSql { get; init; }
    public IReadOnlyList<AccessQueryParameter> Parameters { get; init; } = [];
}

public sealed class AccessQueryParameter
{
    public required string Name { get; init; }
    public required string AccessType { get; init; }
}
