param([long]$CI = 33539654, [int]$Desde = 202510, [int]$Hasta = 202603,
      [string]$Mdb = 'C:\Personal\Gestion\db\sgpaserv.mdb', [string]$Password = '$($env:CASEMED_MDB_PWD)')
$ErrorActionPreference = 'Stop'
$cs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=$Mdb;Jet OLEDB:Database Password=$Password;"
$cn = New-Object System.Data.OleDb.OleDbConnection $cs
$cn.Open()
$sql = "SELECT AnioMes, CodEmpresa, Concepto, DiasTrabajados, Importe FROM Imponible WHERE CI=$CI AND AnioMes>=$Desde AND AnioMes<=$Hasta ORDER BY CodEmpresa, AnioMes, Concepto"
$da = New-Object System.Data.OleDb.OleDbDataAdapter($sql, $cn)
$dt = New-Object System.Data.DataTable; [void]$da.Fill($dt)
Write-Output "ACCESS Imponible CI=$CI ventana $Desde-$Hasta (filas: $($dt.Rows.Count))"
foreach ($r in $dt.Rows) {
    Write-Output ("  {0} emp{1} c{2} dias={3} importe={4}" -f $r['AnioMes'], $r['CodEmpresa'], $r['Concepto'], $r['DiasTrabajados'], [math]::Round([double]$r['Importe'],2))
}
# Promedio por empresa (Concepto=1)
$g = $dt.Select("Concepto='1'") | Group-Object CodEmpresa
foreach ($grp in $g) {
    $imp = ($grp.Group | Measure-Object Importe -Sum).Sum
    $dias = ($grp.Group | Measure-Object DiasTrabajados -Sum).Sum
    if ($dias -gt 0) { Write-Output ("  PROM emp{0}: importe={1} dias={2} -> {3}" -f $grp.Name, [math]::Round($imp,2), $dias, [math]::Round($imp/$dias,2)) }
}
$cn.Close()
