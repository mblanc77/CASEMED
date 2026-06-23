# Reconstruye NewSgpa2 de cero y re-aplica TODO el runbook post-migración en orden.
# Apunta a los backends Access 97 (Jet 3): sgpaserv.mdb / spserv.mdb / seguserv.mdb.
#   - El migrador (paso 1) corre en x86 con el proveedor Jet 4.0 (Access 97 NO lo abre ACE).
#     Se publica como win-x86 y se ejecuta el .exe (el runtime x86 de .NET debe estar instalado).
#   - Los .mdb 97 están protegidos: sgpaserv/spserv abren con $MdbPwd; seguserv con $MdbPwd + 's'
#     (ese sufijo final lo agrega internamente migrate-seguridad.ps1).
# El migrador (EnsureDeleted+EnsureCreated) sólo crea el esquema de negocio + importa datos; el resto
# (seguridad, QA, filtros guardados, fix de ValorJornal, valores de SubsidioItemCod) son pasos manuales
# que ESTE script orquesta. Pensado para correr varias veces. Varios pasos usan Jet 4.0 (PS 32-bit).
#
# Uso:   powershell -ExecutionPolicy Bypass -File tools\rebuild-newsgpa2.ps1
#        -SkipMigrator   (re-aplica sólo los post-pasos, sin dropear/reimportar)
#        -SgpaMdb/-SpMdb/-SeguMdb  para apuntar a otras ubicaciones de los .mdb 97
param(
    [switch]$SkipMigrator,
    [string]$SqlServer = 'localhost',
    [string]$SqlDb = 'NewSgpa2',
    # Backends Access 97 (Jet 3). Ajustá si los moviste.
    [string]$SgpaMdb = 'C:\Personal\Gestion\CASEMED\VB6\Sgpa\Data\sgpaserv.mdb',
    [string]$SpMdb   = 'C:\Personal\Gestion\CASEMED\VB6\SP\Data\spserv.mdb',
    # Vacío => auto: usa <carpeta de sgpaserv>\seguserv.mdb si existe; si no, C:\Personal\Gestion\db\seguserv.mdb.
    [string]$SeguMdb = '',
    [string]$MdbPwd  = 'rdjcfm',
    # Ubicaciones del código (parametrizables para otra máquina/layout).
    [string]$ToolsDir = $PSScriptRoot,   # por defecto, la carpeta donde vive este script (tools\)
    [string]$MigProj  = 'C:\Personal\Gestion\CASEMED\NewSgpa\NewSgpa\NewSgpa.Migration\NewSgpa.Migration.csproj'
)
$ErrorActionPreference = 'Stop'
$tools   = $ToolsDir
$migProj = $MigProj
$migDir  = Split-Path $migProj
$ps32    = "$env:WINDIR\SysWOW64\WindowsPowerShell\v1.0\powershell.exe"
$cs      = "Server=$SqlServer;Database=$SqlDb;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;"

# Password de los .mdb compartido por env (lo usan el migrador y los pasos Jet 32-bit;
# migrate-seguridad.ps1 le agrega la 's' final para seguserv.mdb).
$env:CASEMED_MDB_PWD = $MdbPwd

# seguserv.mdb: preferí el que esté junto a sgpaserv.mdb (si lo copiaste ahí); si no, el de C:\Personal\Gestion\db.
if (-not $SeguMdb) {
    $cand = Join-Path (Split-Path $SgpaMdb) 'seguserv.mdb'
    if (Test-Path $cand) { $SeguMdb = $cand } else { $SeguMdb = 'C:\Personal\Gestion\db\seguserv.mdb' }
}

function Step($n) { Write-Host "`n==== $n ====" -ForegroundColor Cyan }
function ApplySqlFile($path) {
    $text = Get-Content $path -Raw -Encoding UTF8
    $cn = New-Object System.Data.SqlClient.SqlConnection $cs
    $cn.Open(); $cmd = $cn.CreateCommand(); $cmd.CommandTimeout = 600; $cmd.CommandText = $text
    [void]$cmd.ExecuteNonQuery(); $cn.Close()
    Write-Host "  aplicado: $(Split-Path $path -Leaf)"
}

if (-not $SkipMigrator) {
    Step "1/8  Migrador 97 (x86/Jet4.0; drop+create+import desde sgpaserv.mdb/spserv.mdb) -> $SqlDb"
    foreach ($m in @($SgpaMdb, $SpMdb)) { if (-not (Test-Path $m)) { throw "No existe el backend Access: $m" } }
    # Access 97 = Jet 3: requiere proveedor Jet 4.0 (32-bit) => el proceso debe ser x86.
    $pub = Join-Path $migDir 'publish-x86'
    Write-Host "  publicando migrador como win-x86..."
    dotnet publish $migProj -c Release -r win-x86 --self-contained false -o $pub --nologo -v q
    if ($LASTEXITCODE -ne 0) { throw "publish x86 falló (exit $LASTEXITCODE)" }
    $exe = Join-Path $pub 'NewSgpa.Migration.exe'
    # Pasamos la cadena SQL COMPLETA (no sólo --db) para que el migrador respete $SqlServer en producción.
    $migConn = "Data Source=$SqlServer;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Command Timeout=300;Initial Catalog=$SqlDb"
    & $exe --sgpa-mdb="$SgpaMdb" --sp-mdb="$SpMdb" --oledb-provider="Microsoft.Jet.OLEDB.4.0" --mdb-pwd="$MdbPwd" --sql="$migConn"
    if ($LASTEXITCODE -ne 0) { throw "Migrador falló (exit $LASTEXITCODE)" }
} else { Step '1/8  Migrador OMITIDO (--SkipMigrator)' }

Step '2/8  Esquema de seguridad (seg.*)'
ApplySqlFile "$tools\sql\seg-schema.sql"

Step "3/8  Usuarios/roles desde seguserv.mdb 97 (Jet 32-bit): $SeguMdb"
if (-not (Test-Path $SeguMdb)) { throw "No existe seguserv.mdb: $SeguMdb" }
& $ps32 -NoProfile -ExecutionPolicy Bypass -File "$tools\migrate-seguridad.ps1" -Mdb $SeguMdb -SqlServer $SqlServer -SqlDb $SqlDb
if ($LASTEXITCODE -ne 0) { throw "migrate-seguridad.ps1 falló (exit $LASTEXITCODE)" }

Step '4/8  Usuario QA (admin)'
& powershell -NoProfile -ExecutionPolicy Bypass -File "$tools\create-qa-user.ps1" -SqlServer $SqlServer -SqlDb $SqlDb

Step '5/8  Filtros guardados (SgpaFiltro + seed)'
ApplySqlFile "$tools\sql\sgpa-filtro.sql"
ApplySqlFile "$tools\sql\sgpa-filtro-seed.sql"

Step '6/8  Fix ValorJornal (acc_sgpa_300_AfiliadoDiasImporte_q)'
ApplySqlFile "$tools\sql\fix-diasimporte.sql"

Step '7/8  Vista de lista de subsidios (SubsidioCabezal + cabezal BPS)'
# La entidad SubsidioCabezal lee de esta vista (columnas BPS totalizables); las escrituras van a la tabla.
ApplySqlFile "$tools\sql\subsidio-cabezal-lista-view.sql"

Step "8/8  Valores de SubsidioItemCod desde sgpaserv.mdb 97 (Jet 32-bit): $SgpaMdb"
& $ps32 -NoProfile -ExecutionPolicy Bypass -File "$tools\migrate-itemcod.ps1" -Mdb $SgpaMdb -Password $MdbPwd -SqlServer $SqlServer -SqlDb $SqlDb
if ($LASTEXITCODE -ne 0) { throw "migrate-itemcod.ps1 falló (exit $LASTEXITCODE)" }

Write-Host "`n==== REBUILD COMPLETO (backends Access 97) ====" -ForegroundColor Green
# Nota: ya NO hace falta tools\sql\sp-prestamo-extend.sql — el migrador crea esas columnas.
