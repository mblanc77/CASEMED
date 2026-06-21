param(
    [Parameter(Mandatory = $true)][string]$Mdb,
    [Parameter(Mandatory = $true)][string]$Table,
    [string]$Password = ''
)
$ErrorActionPreference = 'Stop'
$pwdPart = if ($Password) { "Jet OLEDB:Database Password=$Password;" } else { '' }
foreach ($prov in @('Microsoft.Jet.OLEDB.4.0', 'Microsoft.ACE.OLEDB.12.0')) {
    $cs = "Provider=$prov;Data Source=$Mdb;$pwdPart"
    try {
        $cn = New-Object System.Data.OleDb.OleDbConnection $cs
        $cn.Open()
        $da = New-Object System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [$Table]", $cn)
        $dt = New-Object System.Data.DataTable
        [void]$da.Fill($dt)
        Write-Output ("PROVIDER=$prov ROWS=$($dt.Rows.Count)")
        Write-Output ('COLS: ' + (($dt.Columns | ForEach-Object { $_.ColumnName }) -join ','))
        $cn.Close()
        return $dt
    }
    catch {
        Write-Output ("FAIL[$prov]: " + $_.Exception.Message)
        if ($cn -and $cn.State -eq 'Open') { $cn.Close() }
    }
}
