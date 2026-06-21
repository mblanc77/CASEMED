# Auditoría autoritativa: compara las columnas de cada tabla de las bases FUENTE reales que el
# migrador abre (sgpaserv2k3.mdb / spserv2k3.mdb, vía ACE.OLEDB) contra el esquema vivo de NewSgpa2.
# Reporta, por tabla migrada, las columnas del origen que NO quedaron en NewSgpa2 (casos SP_Prestamo).
param(
  [string[]] $Mdbs = @(
    'C:\Personal\Gestion\CASEMED\VB6\Sgpa\Data\sgpaserv2k3.mdb',
    'C:\Personal\Gestion\CASEMED\VB6\sp\Data\spserv2k3.mdb'
  ),
  [string] $ConnectionString = 'Data Source=localhost;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;Initial Catalog=NewSgpa2'
)

$sql = New-Object System.Data.SqlClient.SqlConnection $ConnectionString
$sql.Open()
function LiveCols($t) {
  $c = $sql.CreateCommand(); $c.CommandText = 'SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME=@t'
  [void]$c.Parameters.AddWithValue('@t', $t); $r = $c.ExecuteReader(); $l = @(); while ($r.Read()) { $l += [string]$r[0] }; $r.Close(); return $l
}
function TableExists($t) {
  $c = $sql.CreateCommand(); $c.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@t AND TABLE_TYPE='BASE TABLE'"
  [void]$c.Parameters.AddWithValue('@t', $t); return ([int]$c.ExecuteScalar() -gt 0)
}

$missing = @(); $notInDb = @()
foreach ($mdb in $Mdbs) {
  if (-not (Test-Path $mdb)) { Write-Output "FUENTE NO ENCONTRADA: $mdb"; continue }
  $cn = New-Object System.Data.OleDb.OleDbConnection ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=$mdb;")
  $cn.Open()
  $tbls = $cn.GetOleDbSchemaTable([System.Data.OleDb.OleDbSchemaGuid]::Tables, @($null, $null, $null, 'TABLE'))
  foreach ($row in $tbls.Rows) {
    $t = [string]$row.TABLE_NAME
    if ($t -like 'MSys*' -or $t -like '~*') { continue }
    if (-not (TableExists $t)) { $notInDb += $t; continue }
    $colsTbl = $cn.GetOleDbSchemaTable([System.Data.OleDb.OleDbSchemaGuid]::Columns, @($null, $null, $t, $null))
    $srcCols = @($colsTbl.Rows | ForEach-Object { [string]$_.COLUMN_NAME })
    $live = LiveCols $t
    $miss = @($srcCols | Where-Object { $live -notcontains $_ })
    if ($miss.Count -gt 0) {
      $missing += [pscustomobject]@{ Mdb = (Split-Path $mdb -Leaf); Tabla = $t; Src = $srcCols.Count; Live = $live.Count; Faltan = ($miss -join ', ') }
    }
  }
  $cn.Close()
}
$sql.Close()

Write-Output '=== TABLAS MIGRADAS CON COLUMNAS DEL ORIGEN FALTANTES EN NewSgpa2 ==='
if ($missing.Count -eq 0) { Write-Output '(ninguna)' } else { $missing | Sort-Object Mdb, Tabla | Format-Table -AutoSize -Wrap | Out-String -Width 220 | Write-Output }
Write-Output ''
Write-Output ('=== TABLAS DEL ORIGEN NO PRESENTES COMO BASE TABLE EN NewSgpa2 ({0}) ===' -f $notInDb.Count)
Write-Output (($notInDb | Sort-Object -Unique) -join ', ')
