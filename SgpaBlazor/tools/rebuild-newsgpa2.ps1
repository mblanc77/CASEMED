# Reconstruye NewSgpa2 de cero y re-aplica TODO el runbook post-migración en orden.
# El migrador (EnsureDeleted+EnsureCreated) sólo crea el esquema de negocio + importa datos; el resto
# (seguridad, QA, filtros guardados, fix de ValorJornal, valores de SubsidioItemCod) son pasos manuales
# que ESTE script orquesta. Pensado para correr varias veces. Algunos pasos usan Jet 4.0 (PS 32-bit).
#
# Uso:   powershell -ExecutionPolicy Bypass -File tools\rebuild-newsgpa2.ps1
#        -SkipMigrator   (re-aplica sólo los post-pasos, sin dropear/reimportar)
param(
    [switch]$SkipMigrator,
    [string]$SqlServer = 'localhost',
    [string]$SqlDb = 'NewSgpa2'
)
$ErrorActionPreference = 'Stop'
$tools   = 'C:\Personal\Gestion\CASEMED\SgpaBlazor\tools'
$migProj = 'C:\Personal\Gestion\CASEMED\NewSgpa\NewSgpa\NewSgpa.Migration\NewSgpa.Migration.csproj'
$ps32    = "$env:WINDIR\SysWOW64\WindowsPowerShell\v1.0\powershell.exe"
$cs      = "Server=$SqlServer;Database=$SqlDb;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;"

function Step($n) { Write-Host "`n==== $n ====" -ForegroundColor Cyan }
function ApplySqlFile($path) {
    $text = Get-Content $path -Raw -Encoding UTF8
    $cn = New-Object System.Data.SqlClient.SqlConnection $cs
    $cn.Open(); $cmd = $cn.CreateCommand(); $cmd.CommandTimeout = 600; $cmd.CommandText = $text
    [void]$cmd.ExecuteNonQuery(); $cn.Close()
    Write-Host "  aplicado: $(Split-Path $path -Leaf)"
}

if (-not $SkipMigrator) {
    Step "1/7  Migrador (drop + create + import desde sgpaserv2k3/spserv2k3) -> $SqlDb"
    Push-Location (Split-Path $migProj)
    # IMPORTANTE: pasar la base al migrador (--db) para que TODO el orquestador respete -SqlDb;
    # sin esto el migrador siempre apuntaba a su default (NewSgpa2) mientras los pasos 2-7 usaban -SqlDb.
    dotnet run --project $migProj -c Debug -- --db=$SqlDb
    if ($LASTEXITCODE -ne 0) { Pop-Location; throw "Migrador falló (exit $LASTEXITCODE)" }
    Pop-Location
} else { Step '1/7  Migrador OMITIDO (--SkipMigrator)' }

Step '2/7  Esquema de seguridad (seg.*)'
ApplySqlFile "$tools\sql\seg-schema.sql"

Step '3/7  Usuarios/roles desde seguserv.mdb (Jet 32-bit)'
& $ps32 -NoProfile -ExecutionPolicy Bypass -File "$tools\migrate-seguridad.ps1" -SqlServer $SqlServer -SqlDb $SqlDb

Step '4/7  Usuario QA (admin)'
& powershell -NoProfile -ExecutionPolicy Bypass -File "$tools\create-qa-user.ps1" -SqlServer $SqlServer -SqlDb $SqlDb

Step '5/7  Filtros guardados (SgpaFiltro + seed)'
ApplySqlFile "$tools\sql\sgpa-filtro.sql"
ApplySqlFile "$tools\sql\sgpa-filtro-seed.sql"

Step '6/7  Fix ValorJornal (acc_sgpa_300_AfiliadoDiasImporte_q)'
ApplySqlFile "$tools\sql\fix-diasimporte.sql"

Step '7/7  Valores de SubsidioItemCod desde sgpaserv.mdb (Jet 32-bit)'
& $ps32 -NoProfile -ExecutionPolicy Bypass -File "$tools\migrate-itemcod.ps1" -SqlServer $SqlServer -SqlDb $SqlDb

Write-Host "`n==== REBUILD COMPLETO ====" -ForegroundColor Green
# Nota: ya NO hace falta tools\sql\sp-prestamo-extend.sql — el migrador crea esas columnas.
