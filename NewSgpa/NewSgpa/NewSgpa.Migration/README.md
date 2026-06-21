# NewSgpa.Migration

Herramienta de migración que **reconstruye la base SQL `NewSgpa2`** a partir de los backends Access
(`sgpaserv2k3.mdb` de SGPA + `spserv2k3.mdb` de SP).

> ⚠️ **Por defecto es un rebuild total, NO incremental**: dropea la base destino (`EnsureDeleted`), recrea el
> esquema, aplica el SQL generado y recarga los datos. Todo lo que haya en `NewSgpa2` (incluidas ediciones de
> prueba de los usuarios y las tablas de la app como `PreferenciaVista`/`TablaConfig`/`AuditCambio`) se pierde
> y vuelve al estado de los Access de origen. Es **determinístico**: mismos Access + mismo código ⇒ misma base.

## Configuración (rutas, base SQL, proveedor)

Nada está hardcodeado: se resuelve con precedencia **argumento `--clave=valor` > variable de entorno > default**.

| Argumento | Variable de entorno | Default | Qué configura |
|---|---|---|---|
| `--sgpa-mdb=…` | `NEWSGPA_SGPA_MDB` | `C:\Personal\…\VB6\Sgpa\Data\sgpaserv2k3.mdb` | backend Access de SGPA |
| `--sp-mdb=…` | `NEWSGPA_SP_MDB` | `C:\Personal\…\VB6\sp\Data\spserv2k3.mdb` | backend Access de SP |
| `--sql="…"` | `NEWSGPA_SQL` | `Data Source=localhost;…;Initial Catalog=NewSgpa2` | cadena de conexión SQL completa |
| `--db=Nombre` | `NEWSGPA_DB` | — | atajo: cambia sólo el `Initial Catalog` de la cadena |
| `--oledb-provider=…` | `NEWSGPA_OLEDB_PROVIDER` | `Microsoft.ACE.OLEDB.12.0` | proveedor OLEDB para leer los `.mdb` |

Al arrancar imprime la config resuelta (con el password enmascarado).

### Password de las bases Access (`CASEMED_MDB_PWD`)

El password de los `.mdb` **no se versiona** (se quitó del repo por seguridad). Los scripts de
comparación/lectura Access↔SQL (`SgpaBlazor/tools/**/*.ps1`) lo leen de la variable de entorno
**`CASEMED_MDB_PWD`**; hay que setearla antes de correrlos:

```powershell
$env:CASEMED_MDB_PWD = "rdjcfm"      # clave del .mdb (sólo en tu sesión)
# o persistente para el usuario:
setx CASEMED_MDB_PWD "rdjcfm"
```

La herramienta de migración en sí abre los backends `*serv2k3.mdb` con ACE **sin password** (no están
protegidos) → no la necesita. Si algún día apuntás a un `.mdb` **protegido**, hay que agregar
`Jet OLEDB:Database Password=…` a la cadena OLEDB de `OleDbConn` (hoy no lo incluye).

### Flags de modo (ya existían)
- `--generate-access-sql-only` — sólo (re)genera los `.sql` de `Generated/AccessSql/` y termina.
- `--skip-sgpa` / `--skip-sp` — preserva la base (no dropea) y omite ese origen.
- `--backfill-only` — sólo rellena tablas vacías (verbatim), sin dropear.

## PostFixes (parches post-generación)

Los objetos `acc_sgpa_*` (funciones/SPs migradas de las queries Access) los **regenera** la migración con
`AccessQueries/AccessToSqlTranslator.cs`. Algunos arreglos no se pueden expresar en esa traducción y se
aplican como parche **después** de los artefactos, desde `PostFixes/*.sql` (orden alfabético), vía
`ApplyPostGenerationFixesAsync` en `Program.cs`. Deben ser **idempotentes** (`ALTER …`) y **sin `USE`**
(corren sobre la conexión configurada).

Actuales:
- `01_estadisticas_smn.sql` — castea `1.25*@pSMN` a `float` en `acc_sgpa_802_*`/`acc_sgpa_811_*`
  (overflow `numeric(3,2)`; Informes Estadísticos IdRpt 2, 3, 20, 21).
- `02_estadisticas_certificados_activos.sql` — agrega el filtro de afiliado activo en
  `acc_sgpa_805_CertificadosActivos_q` (IdRpt 10: el "Sin certificar" daba negativo).

**Para sumar un parche**: dejá un `.sql` nuevo en `PostFixes/` (ALTER idempotente, sin `USE`); se aplica solo.

## Cómo correrlo

### 64 bits (default — sirve para los `*serv2k3.mdb` actuales con ACE 12.0 de 64 bits instalado)
```powershell
# desde la carpeta del proyecto
dotnet run --project NewSgpa.Migration -- --db=NewSgpa2
# o apuntando a otra base/origen:
dotnet run --project NewSgpa.Migration -- --sgpa-mdb="D:\nuevo\sgpaserv2k3.mdb" --sp-mdb="D:\nuevo\spserv2k3.mdb" --db=NewSgpa3
```

### 32 bits (necesario sólo si el `.mdb` es formato Access 97 → proveedor **Jet 4.0**, que es 32-bit; o si tu ACE es 32-bit)
El proceso .NET debe ser x86 para cargar un proveedor OLEDB de 32 bits. Hay runtime **x86 de .NET 10 instalado**, así que alcanza con compilar para `win-x86`:

```powershell
# Publicar como x86 (framework-dependent) y ejecutar el .exe de 32 bits
dotnet publish NewSgpa.Migration\NewSgpa.Migration.csproj -c Release -r win-x86 --self-contained false -o publish-x86
.\publish-x86\NewSgpa.Migration.exe --oledb-provider="Microsoft.Jet.OLEDB.4.0" --sgpa-mdb="D:\viejo\sgpa.mdb" --sp-mdb="D:\viejo\sp.mdb" --db=NewSgpa3
```

Alternativa sin publicar (compila y corre x86 en un paso):
```powershell
dotnet run --project NewSgpa.Migration -r win-x86 -- --oledb-provider="Microsoft.Jet.OLEDB.4.0" --sgpa-mdb="…" --sp-mdb="…"
```

Si el server destino **no** tiene el runtime x86 de .NET 10, usar `--self-contained true` en el `publish`
(empaqueta el runtime; no requiere instalar nada):
```powershell
dotnet publish NewSgpa.Migration\NewSgpa.Migration.csproj -c Release -r win-x86 --self-contained true -o publish-x86
```

> **Regla práctica de proveedor/bitness**
> - `.mdb` formato 2000/2003 (los `*serv2k3.mdb`): **ACE 12.0** sirve. Si tenés ACE de 64 bits → corré 64-bit (default).
> - `.mdb` formato Access 97 (el `sgpa.mdb` front-end viejo): sólo **Jet 4.0** (32-bit) → publicá x86.
> - Si Office instalado es de 32 bits, tu ACE también es 32-bit → corré x86 aunque uses ACE.

## Requisitos
- SDK de .NET 10 (el x64 alcanza para compilar; para correr x86 hace falta el runtime/SDK x86, ya presente acá).
- Proveedor OLEDB acorde (ACE 12.0 o Jet 4.0) según formato del `.mdb`.
- Acceso al SQL Server destino (la cadena/credenciales del `--sql`). El rebuild dropea la base: correr contra
  un **restore/copia** o fuera de horario, nunca sobre producción en vivo.
