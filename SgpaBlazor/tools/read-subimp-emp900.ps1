$ErrorActionPreference = 'Stop'
$cs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Personal\Gestion\CASEMED\VB6\Sgpa\Data\sgpaserv2k3.mdb;Jet OLEDB:Database Password=$($env:CASEMED_MDB_PWD);"
$cn = New-Object System.Data.OleDb.OleDbConnection $cs
$cn.Open()
$ids = "114232,114239,114250,113952,113953,113728,113737,113752"
$sql = "SELECT IdSubsidio,Anio,Mes,CodEmpresa,Dias,Importe FROM SubsidioImponible WHERE IdSubsidio IN ($ids) AND CodEmpresa=900 ORDER BY IdSubsidio,Anio,Mes"
$da = New-Object System.Data.OleDb.OleDbDataAdapter($sql, $cn)
$dt = New-Object System.Data.DataTable
[void]$da.Fill($dt)
Write-Output "emp900 en SubsidioImponible (run snapshot Access) - filas: $($dt.Rows.Count)"
foreach ($r in $dt.Rows) {
    Write-Output ("  Id={0}  {1}{2:00} emp900 dias={3} importe={4}" -f $r['IdSubsidio'], $r['Anio'], $r['Mes'], $r['Dias'], [math]::Round([double]$r['Importe'], 2))
}
$cn.Close()
