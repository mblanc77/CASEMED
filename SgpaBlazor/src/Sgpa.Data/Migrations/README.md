# Migraciones de base de datos (embebidas)

Proceso de actualización de esquema **estilo XAF**: al arrancar, la app pone la base al día aplicando los scripts
pendientes. Lo ejecuta `DatabaseMigrator.Upgrade` desde `Program.cs`, controlado por el flag `Migrations:AutoApply`.

Este proyecto es **database-first** (las entidades se generan *desde* la base con `tools/Sgpa.CodeGen`), así que no hay
"diff de modelo" como en EF/XPO code-first. La evolución del esquema se versiona como scripts SQL ordenados.

## Dos carpetas

- **`Scripts/`** — *run-once*. Tablas, columnas, índices, constraints. Cada script se aplica **una sola vez** y queda
  registrado en `dbo.SchemaVersions`. El orden es **alfabético por nombre de archivo** → usá prefijo numérico.
- **`ScriptsAlways/`** — *run-always*. Vistas, stored procedures, funciones y seeds **idempotentes**
  (`CREATE OR ALTER ...`). Se re-aplican en **cada arranque** (no se journalizan), para que el objeto en la base
  siempre refleje el del repo.

Ambas se compilan como **recursos embebidos** (ver `Sgpa.Data.csproj`): viajan dentro del `.dll`, no hay `.sql`
sueltos que copiar al servidor.

## Convención de nombres

```
Scripts/0001-preferencia-vista.sql
Scripts/0002-agregar-columna-x.sql
ScriptsAlways/subsidio-cabezal-lista-view.sql
```

Numerá los run-once en orden creciente. Empezá la numeración por donde te quede cómodo; lo importante es que sea
monótona y única.

## Flujo de trabajo (de aquí en más)

1. Hacés el cambio de esquema y escribís el script (`Scripts/NNNN-...sql` o `ScriptsAlways/...sql`).
2. Lo aplicás a **dev** arrancando la app (o corriendo el migrador) y verificás.
3. Regenerás entidades: `tools/Sgpa.CodeGen` (y `tools/Sgpa.NavGen` si cambian relaciones).
4. Commiteás el script **junto con** las entidades regeneradas.
5. En el deploy, la app detecta el script pendiente y lo aplica solo a producción.

## Notas

- **Idempotencia**: en `Scripts/` conviene igual escribir defensivo (`IF OBJECT_ID(...) IS NULL`, `IF COL_LENGTH(...)`)
  por si la base ya tenía el objeto. En `ScriptsAlways/` es obligatorio (`CREATE OR ALTER`).
- **Batches**: se respeta el separador `GO` (igual que SSMS).
- **Baseline**: la base actual (dev y prod) ya tiene todo el esquema histórico de `tools/sql/`. Estas carpetas son
  para lo **nuevo de aquí en más**; no hace falta re-registrar lo viejo.
- **Una instancia**: la app corre en una sola instancia (IIS). Si algún día se escala a varias, agregar un lock
  (`sp_getapplock`) para que no migren dos a la vez.
