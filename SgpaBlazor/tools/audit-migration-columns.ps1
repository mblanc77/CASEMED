# Audita que NewSgpa2 incluya todas las columnas de cada tabla de las bases fuente del migrador
# (sgpaserv2k3.mdb / spserv2k3.mdb). Compara los dumps de spec (Migration/Access/*.mdb-specs.txt)
# contra el esquema vivo de NewSgpa2 y reporta columnas faltantes por tabla (casos tipo SP_Prestamo).
param(
  [string[]] $Specs = @(
    'C:\Personal\Gestion\CASEMED\NewSgpa\Migration\Access\sgpaserv.mdb-specs.txt',
    'C:\Personal\Gestion\CASEMED\NewSgpa\Migration\Access\spserv.mdb-specs.txt'
  ),
  [string] $ConnectionString = 'Data Source=localhost;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;Initial Catalog=NewSgpa2'
)

$reTable = [regex]'^TABLE:\s+(.+?)\s*$'
$reQuery = [regex]'^QUERY:'
$reCol   = [regex]'^\s+([^|]+?)\s*\|'

$specTables = @{}
foreach ($sf in $Specs) {
  $cur = $null
  foreach ($line in (Get-Content $sf -Encoding UTF8)) {
    $mt = $reTable.Match($line)
    if ($mt.Success) { $cur = $mt.Groups[1].Value; $specTables[$cur] = New-Object System.Collections.Generic.List[string]; continue }
    if ($reQuery.IsMatch($line)) { $cur = $null; continue }
    if ($null -ne $cur) { $mc = $reCol.Match($line); if ($mc.Success) { $specTables[$cur].Add($mc.Groups[1].Value.Trim()) } }
  }
}

$cn = New-Object System.Data.SqlClient.SqlConnection $ConnectionString
$cn.Open()
function LiveCols($t) {
  $c = $cn.CreateCommand(); $c.CommandText = 'SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME=@t'
  [void]$c.Parameters.AddWithValue('@t', $t); $r = $c.ExecuteReader(); $l = @(); while ($r.Read()) { $l += $r[0] }; $r.Close(); return $l
}
function TableExists($t) {
  $c = $cn.CreateCommand(); $c.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@t AND TABLE_TYPE='BASE TABLE'"
  [void]$c.Parameters.AddWithValue('@t', $t); return ([int]$c.ExecuteScalar() -gt 0)
}

$missing = @(); $notMigrated = @()
foreach ($t in ($specTables.Keys | Sort-Object)) {
  if (-not (TableExists $t)) { $notMigrated += $t; continue }
  $live = LiveCols $t
  $miss = @($specTables[$t] | Where-Object { $_ -and ($live -notcontains $_) })
  if ($miss.Count -gt 0) {
    $missing += [pscustomobject]@{ Tabla = $t; Spec = $specTables[$t].Count; Live = $live.Count; Faltan = ($miss -join ', ') }
  }
}
$cn.Close()

Write-Output '=== TABLAS MIGRADAS CON COLUMNAS FALTANTES ==='
if ($missing.Count -eq 0) { Write-Output '(ninguna)' } else { $missing | Format-Table -AutoSize -Wrap | Out-String -Width 220 | Write-Output }
Write-Output ''
Write-Output ('=== TABLAS DEL SPEC NO PRESENTES COMO BASE TABLE EN NewSgpa2 ({0}) ===' -f $notMigrated.Count)
Write-Output ($notMigrated -join ', ')
