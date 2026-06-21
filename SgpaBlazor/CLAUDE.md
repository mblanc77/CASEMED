# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

SGPA: a Blazor Server line-of-business app (social/health fund management — afiliados, subsidios, préstamos, pagos, retenciones, certificaciones) being **ported from a legacy VB6/Access system** to .NET 9 + DevExpress Blazor. It runs against an existing SQL Server database, **`NewSgpa2`**, whose schema is migrated, not designed here. The codebase, comments, and UI captions are in **Spanish** — match that when writing code and comments.

## Build / test / run

```bash
dotnet build Sgpa.slnx                              # build the solution (slnx format, not .sln)
dotnet test tests/Sgpa.Business.Tests               # xUnit suite (most of the tests)
dotnet test tests/Sgpa.Web.Tests                    # bUnit component tests
dotnet test tests/Sgpa.Business.Tests --filter "FullyQualifiedName~AfiliadoValidator"   # single test/class
```

- **Do not run the app** (`dotnet run`). The user launches it from Rider; just build to verify compilation.
- **Many `Sgpa.Business.Tests` hit the live `NewSgpa2` database** (Integrated Security against `localhost`). They are not pure unit tests — they need the DB up. Tests that count/mutate `dbo.Afiliado` are serialized via the `[Collection("AfiliadoDb")]` collection so they don't race.
- The test projects target **net10.0**; everything else targets **net9.0**. The SDK must support both.
- **DevExpress comes from a local offline NuGet feed** configured in `nuget.config` (`C:\Program Files\DevExpress 25.2\...`, pre-release `25.2.6-pre-26075`). A build will fail without that install present. These licensed packages bring the static web assets (`_content/*`) and transitive deps that loose DLLs would not.

## Architecture

Four source projects, strict layering (Domain ← Data ← Business ← Web):

- **`Sgpa.Domain`** — POCO entities + a metadata system + the security model (interfaces only).
- **`Sgpa.Data`** — Dapper + `Microsoft.Data.SqlClient` data access. No EF/ORM. Generic CRUD, lookups, auth, audit, per-table config, reporting graph loader.
- **`Sgpa.Business`** — domain services (subsidios, IRPF, préstamos, pagos, bank exporters/importers) + FluentValidation validators.
- **`Sgpa.Web`** — Blazor Server (`InteractiveServer` render mode) UI + DevExpress Reporting.

DI is wired through extension methods: `AddSgpaData(connectionString)` and `AddSgpaBusiness()`, called from `Program.cs`.

### The metadata-driven generic CRUD — the core idea

Most screens are **not hand-written per entity**. Instead:

1. **Entities are generated from the DB schema** by `tools/Sgpa.CodeGen` into `src/Sgpa.Domain/Entities/Generated/*.cs` (~160 files). They are `partial class`, decorated with attributes from `Sgpa.Domain/Metadata/SgpaMetadataAttributes.cs` (`[SgpaTable]`, `[SgpaKey]`, `[SgpaColumn]`, `[SgpaAudit]`). **Never edit files under `Generated/` by hand** — regenerate them. Put custom code in a separate partial-class file.
2. **`EntityCatalog`** (`Sgpa.Domain/Metadata`) reflects over the assembly at startup to find every `[SgpaTable]` type and build `EntityMetadata`. It also resolves **foreign keys by convention**: a column whose name matches the single-key column of another entity is treated as an FK (e.g. `Afiliado.CodMutualista` → `Mutualista`), driving lookup combos and grid descriptions.
3. **`DapperCrudService<T>`** (open generic, registered as `ISgpaCrudService<>`) builds SQL from the metadata for list/filter/page/insert/update/delete.
4. **`SgpaCrudView<T>`** + `SgpaDetailForm`/`SgpaPropertyEditor` (`Sgpa.Web/Components/Crud`) render the grid and edit form generically. The route **`/crud/{Entity}`** (`CrudDinamico.razor`) resolves the metadata and instantiates `SgpaCrudView<T>` via `DynamicComponent`. Hand-written pages under `Components/Pages/<Module>/` exist where a module needs more than generic CRUD.

So: to expose a new table, regenerate entities and it appears in the catalog/navigation; to customize, lean on attributes and per-table config before writing a bespoke page.

### Validation

Business rules are **`AbstractValidator<T>`** classes in `Sgpa.Business` (e.g. `AfiliadoValidator`). `AddValidatorsFromAssemblyContaining` registers them all as `IValidator<T>`, and the generic CRUD picks them up automatically — field-level errors render in red and gate saving. Add a validator class; no CRUD wiring needed.

### Runtime configuration knobs (admin-editable, not code)

- **`ITablaConfigService`** — per-table behavior (inline edit / confirm-delete / audit on/off / display name / `DisponibleReportes`), cached singleton. `ReportableTables.IsDefault` is only the heuristic default; the admin overrides it in "Configuración de tablas".
- **Per-user view preferences** — column layout/filters saved per user (`PreferenciaVista`, `IPreferenciaVistaStore`).
- **Saved filters** — `ISavedFilterService`.

### Security

Cookie auth (scheme `Sgpa.Auth`, 8h sliding, login/logout endpoints in `Program.cs`). `DapperSecurityService` authenticates against the DB (roles + per-table permissions); `WebCurrentUser` exposes the current `ICurrentUser` to the circuit. `PasswordHasher` handles credentials. Routes/components guard via authorization; unauthorized → `/denegado`.

### Reporting (DevExpress)

- **Ad-hoc reports**: the End-User Designer + Web Document Viewer, with reports stored in `dbo.Reporte` (`SgpaReportStorage`). Backed by MVC controllers (`AddControllers()` is required alongside Blazor for the viewer/designer to work). The data-source wizard only exposes the curated `NewSgpa2` connection.
- **Predefined reports**: `.repx` files under `Sgpa.Web/Reporting/Predefinidos` and ported Crystal `.rpt` → XtraReports C# under `Sgpa.Web/Reports/<Theme>/`.
- **PDFs render outside the Blazor circuit** via minimal-API endpoints (`/reportes/pdf/{id}`, `/reportes/prestamo/{id}/pdf`) so long renders don't trip the SignalR timeout (which is also raised to 3 min in `Program.cs`).
- Entities/DTOs bound by reports must be registered as DevExpress "trusted assemblies" (see `Program.cs`) or `ObjectDataSource` deserialization is blocked.

### Logging

Serilog → daily file (`logs/sgpa-*.log`, 31-day retention) **and** a custom sink to `dbo.Z_ErrorLog` so uncaught Blazor render/circuit exceptions also land in the DB. Handled errors go through `IErrorLog`. `UserLogCircuitHandler` enriches logs with the circuit's user.

## Tools (`tools/`)

Run-on-demand console apps; only `Sgpa.CodeGen` is in the solution.

- **`Sgpa.CodeGen`** — regenerates `Domain/Entities/Generated` from `NewSgpa2` (dbo schema only). `dotnet run -- [connectionString] [outputDir]`. Wipes and rewrites the folder.
- **`Sgpa.NavGen`** — reflects the metadata (no DB) to emit navigation-property partials (`EntityNavigation.g.cs`) and a relation map (`ReportNavMap.g.cs`) so the reporting `ObjectDataSource` shows the object graph (XPO/EF-like). Run after schema/entity changes that affect relations.
- **`UiVerify`** — Selenium/ChromeDriver smoke check of the server-side grids (render, sort, search) against a running instance. Requires the app to be up.
