using System.Reflection;
using DbUp;
using DbUp.Engine;
using DbUp.Engine.Output;
using DbUp.Helpers;
using Microsoft.Extensions.Logging;

namespace Sgpa.Data.Migrations;

/// <summary>
/// Aplica las migraciones de esquema embebidas, al estilo de XAF (al arrancar la app se pone la base al día).
/// Dos pasadas:
///   1. <c>Migrations/Scripts</c>     — run-once, journaladas en <c>dbo.SchemaVersions</c> (tablas/columnas/índices).
///   2. <c>Migrations/ScriptsAlways</c> — run-always, sin journal (vistas/SPs/seeds idempotentes con CREATE OR ALTER).
/// Cada script corre en su propia transacción. NO crea la base (la base ya existe en prod y el app pool puede no
/// tener permiso de creación): sólo aplica cambios sobre la base existente.
/// </summary>
public static class DatabaseMigrator
{
    public static MigrationResult Upgrade(string connectionString, ILogger? logger = null)
    {
        var asm = typeof(DatabaseMigrator).Assembly;
        var log = new MigratorLog(logger);

        // 1) Estructura: una sola vez, registrada en dbo.SchemaVersions.
        var once = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(asm, Filter(".Migrations.Scripts."))
            .WithTransactionPerScript()
            .JournalToSqlTable("dbo", "SchemaVersions")
            .LogTo(log)
            .Build();

        var r1 = once.PerformUpgrade();
        if (!r1.Successful)
            return new MigrationResult(false, r1.Error, Array.Empty<string>());

        // 2) Vistas/SPs/seeds idempotentes: se re-aplican en cada arranque (NullJournal = sin registro).
        var always = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(asm, Filter(".Migrations.ScriptsAlways."))
            .WithTransactionPerScript()
            .JournalTo(new NullJournal())
            .LogTo(log)
            .Build();

        var r2 = always.PerformUpgrade();
        if (!r2.Successful)
            return new MigrationResult(false, r2.Error, Array.Empty<string>());

        var applied = r1.Scripts.Concat(r2.Scripts).Select(s => s.Name).ToArray();
        return new MigrationResult(true, null, applied);
    }

    // El nombre del recurso embebido es "<namespace>.Migrations.Scripts.<archivo>.sql"; filtramos por la carpeta.
    private static Func<string, bool> Filter(string folderToken)
        => name => name.Contains(folderToken, StringComparison.OrdinalIgnoreCase)
                   && name.EndsWith(".sql", StringComparison.OrdinalIgnoreCase);

    /// <summary>Puente entre el log interno de DbUp (<see cref="IUpgradeLog"/>) y el <see cref="ILogger"/> de la app.</summary>
    private sealed class MigratorLog : IUpgradeLog
    {
        private readonly ILogger? _logger;
        public MigratorLog(ILogger? logger) => _logger = logger;

        public void WriteInformation(string format, params object[] args) => _logger?.LogInformation(format, args);
        public void WriteError(string format, params object[] args) => _logger?.LogError(format, args);
        public void WriteWarning(string format, params object[] args) => _logger?.LogWarning(format, args);
    }
}

/// <summary>Resultado de una corrida de migraciones.</summary>
public sealed record MigrationResult(bool Successful, Exception? Error, IReadOnlyList<string> AppliedScripts);
