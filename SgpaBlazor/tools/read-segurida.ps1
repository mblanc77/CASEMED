param([string]$Mdb = 'C:\Personal\Gestion\CASEMED\VB6\Sgpa\segurida.mdb')
$ErrorActionPreference = 'Stop'
$cs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=$Mdb;Jet OLEDB:Database Password=$($env:CASEMED_MDB_PWD)s;"
$cn = New-Object System.Data.OleDb.OleDbConnection $cs
$cn.Open()
$tabs = $cn.GetOleDbSchemaTable([System.Data.OleDb.OleDbSchemaGuid]::Tables, $null)
Write-Output '=== ALL TABLE-LIKE OBJECTS ==='
$tabs | Select-Object TABLE_NAME, TABLE_TYPE | ForEach-Object { Write-Output ("{0,-30} {1}" -f $_.TABLE_NAME, $_.TABLE_TYPE) }

Write-Output ''
Write-Output '=== LINK TARGETS (MSysObjects) ==='
try {
    $da0 = New-Object System.Data.OleDb.OleDbDataAdapter("SELECT Name, Database, ForeignName FROM MSysObjects WHERE Type=6", $cn)
    $dt0 = New-Object System.Data.DataTable; [void]$da0.Fill($dt0)
    foreach ($r in $dt0.Rows) { Write-Output ("  {0,-20} -> {1} :: {2}" -f $r['Name'], $r['Database'], $r['ForeignName']) }
} catch { Write-Output ('  (no MSysObjects: ' + $_.Exception.Message + ')') }

$userTabs = $tabs | Where-Object { $_.TABLE_TYPE -in @('TABLE','LINK') } | Select-Object -ExpandProperty TABLE_NAME
foreach ($t in $userTabs) {
    Write-Output ''
    Write-Output ("===== TABLE: $t =====")
    try {
        $da = New-Object System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [$t]", $cn)
        $dt = New-Object System.Data.DataTable
        [void]$da.Fill($dt)
        Write-Output ('cols: ' + (($dt.Columns | ForEach-Object { $_.ColumnName + ':' + $_.DataType.Name }) -join ', '))
        Write-Output ('rows: ' + $dt.Rows.Count)
        $i = 0
        foreach ($r in $dt.Rows) {
            if ($i -ge 15) { Write-Output '  ...'; break }
            Write-Output ('  | ' + (($dt.Columns | ForEach-Object { [string]$r[$_.ColumnName] }) -join ' | '))
            $i++
        }
    } catch { Write-Output ('  ERROR: ' + $_.Exception.Message) }
}
$cn.Close()
