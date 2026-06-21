param(
    [string]$Mdb = 'C:\Personal\Gestion\CASEMED\VB6\Sgpa\Data\sgpaserv2k3.mdb',
    [string]$Password = '$($env:CASEMED_MDB_PWD)',
    [string]$SqlServer = 'localhost',
    [string]$SqlDb = 'NewSgpa2'
)
$ErrorActionPreference = 'Stop'

# --- Access: enumerar tablas TABLE (no queries, no MSys) y contar filas ---
$cs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=$Mdb;Jet OLEDB:Database Password=$Password;"
$cn = New-Object System.Data.OleDb.OleDbConnection $cs
$cn.Open()
$schema = $cn.GetOleDbSchemaTable([System.Data.OleDb.OleDbSchemaGuid]::Tables, @($null, $null, $null, 'TABLE'))
$access = @{}
foreach ($row in $schema.Rows) {
    $name = [string]$row['TABLE_NAME']
    if ($name -like 'MSys*') { continue }
    $cmd = $cn.CreateCommand(); $cmd.CommandText = "SELECT COUNT(*) FROM [$name]"
    try { $access[$name] = [int]$cmd.ExecuteScalar() } catch { $access[$name] = -1 }
}
$cn.Close()

# --- SQL Server: conteo de filas por tabla (dbo) ---
$sql = New-Object System.Data.SqlClient.SqlConnection("Server=$SqlServer;Database=$SqlDb;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;")
$sql.Open()
$cmd = $sql.CreateCommand()
$cmd.CommandText = @'
SELECT t.name AS n, SUM(p.rows) AS r
FROM sys.tables t
JOIN sys.partitions p ON p.object_id = t.object_id AND p.index_id IN (0,1)
WHERE SCHEMA_NAME(t.schema_id) = 'dbo'
GROUP BY t.name
'@
$rd = $cmd.ExecuteReader()
$sqlCounts = @{}
while ($rd.Read()) { $sqlCounts[[string]$rd['n']] = [long]$rd['r'] }
$rd.Close(); $sql.Close()

# Índice case-insensitive de tablas SQL
$sqlByLower = @{}
foreach ($k in $sqlCounts.Keys) { $sqlByLower[$k.ToLower()] = $k }

# --- Diff ---
$missing = @()   # en Access, no en SQL
$diff = @()      # en ambos, conteo distinto
$ok = 0
foreach ($t in ($access.Keys | Sort-Object)) {
    $aCnt = $access[$t]
    if ($sqlByLower.ContainsKey($t.ToLower())) {
        $sName = $sqlByLower[$t.ToLower()]
        $sCnt = $sqlCounts[$sName]
        if ($sCnt -ne $aCnt) { $diff += [pscustomobject]@{ Tabla = $t; Access = $aCnt; Sql = $sCnt; Delta = $aCnt - $sCnt } }
        else { $ok++ }
    }
    else {
        $missing += [pscustomobject]@{ Tabla = $t; Access = $aCnt }
    }
}

Write-Output "==================== RESUMEN ===================="
Write-Output ("Tablas en Access (TABLE): {0}" -f $access.Count)
Write-Output ("  coinciden exacto:       {0}" -f $ok)
Write-Output ("  faltan en SQL:          {0}" -f $missing.Count)
Write-Output ("  conteo difiere:         {0}" -f $diff.Count)
Write-Output ""
Write-Output "==================== FALTAN EN SQL (no migradas) ===================="
$missing | Sort-Object -Property @{Expression={$_.Access}; Descending=$true} | Format-Table -AutoSize | Out-String -Width 200 | Write-Output
Write-Output "==================== CONTEO DIFIERE ===================="
$diff | Sort-Object -Property @{Expression={[math]::Abs($_.Delta)}; Descending=$true} | Format-Table -AutoSize | Out-String -Width 200 | Write-Output
