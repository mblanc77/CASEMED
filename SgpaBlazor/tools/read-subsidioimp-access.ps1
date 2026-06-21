param([long]$CI = 33539654, [int]$Anio = 2026, [int]$Mes = 4,
      [string]$Mdb = 'C:\Personal\Gestion\CASEMED\VB6\Sgpa\Data\sgpaserv2k3.mdb', [string]$Password = '$($env:CASEMED_MDB_PWD)')
$ErrorActionPreference = 'Stop'
$cn = New-Object System.Data.OleDb.OleDbConnection ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=$Mdb;Jet OLEDB:Database Password=$Password;")
$cn.Open()
function Q($sql) { $da = New-Object System.Data.OleDb.OleDbDataAdapter($sql, $cn); $dt = New-Object System.Data.DataTable; [void]$da.Fill($dt); return ,$dt }

$cab = Q "SELECT IdSubsidio, ValorJornal FROM SubsidioCabezal WHERE CI=$CI AND Anio=$Anio AND Mes=$Mes AND Liquidar=True"
Write-Output "Cabezales en Access para CI=$CI $Anio/$Mes :"
foreach ($r in $cab.Rows) { Write-Output ("  IdSubsidio=$($r['IdSubsidio'])  ValorJornal=$([math]::Round([double]$r['ValorJornal'],2))") }

foreach ($r in $cab.Rows) {
    $id = $r['IdSubsidio']
    $imp = Q "SELECT Mes, Anio, CodEmpresa, Dias, Importe FROM SubsidioImponible WHERE IdSubsidio=$id ORDER BY CodEmpresa, Anio, Mes"
    Write-Output "  -- SubsidioImponible del run (IdSubsidio=$id): $($imp.Rows.Count) filas --"
    foreach ($x in $imp.Rows) { Write-Output ("     {0}/{1} emp{2} dias={3} importe={4}" -f $x['Anio'], $x['Mes'], $x['CodEmpresa'], $x['Dias'], [math]::Round([double]$x['Importe'],2)) }
    $g = $imp.Rows | Group-Object CodEmpresa
    foreach ($grp in $g) {
        $si = ($grp.Group | Measure-Object Importe -Sum).Sum; $sd = ($grp.Group | Measure-Object Dias -Sum).Sum
        if ($sd -gt 0) { Write-Output ("     PROM emp{0}: {1}/{2} -> {3}" -f $grp.Name, [math]::Round($si,2), $sd, [math]::Round($si/$sd,2)) }
    }
}
$cn.Close()
