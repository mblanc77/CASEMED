$ErrorActionPreference = 'Stop'
$cs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Personal\Gestion\CASEMED\VB6\Sgpa\Data\sgpaserv2k3.mdb;Jet OLEDB:Database Password=$($env:CASEMED_MDB_PWD);"
$cn = New-Object System.Data.OleDb.OleDbConnection $cs
$cn.Open()
function Scalar($sql) { $c = $cn.CreateCommand(); $c.CommandText = $sql; return $c.ExecuteScalar() }
Write-Output ("SubsidioItemEmpresa total:            {0}" -f (Scalar "SELECT COUNT(*) FROM SubsidioItemEmpresa"))
Write-Output ("  con IdSubsidio NULL:                {0}" -f (Scalar "SELECT COUNT(*) FROM SubsidioItemEmpresa WHERE IdSubsidio IS NULL"))
Write-Output ("  IdSubsidio NO existe en Cabezal:    {0}" -f (Scalar "SELECT COUNT(*) FROM SubsidioItemEmpresa e WHERE e.IdSubsidio IS NOT NULL AND NOT EXISTS (SELECT 1 FROM SubsidioCabezal c WHERE c.IdSubsidio = e.IdSubsidio)"))
Write-Output ("  IdSubsidio=0:                       {0}" -f (Scalar "SELECT COUNT(*) FROM SubsidioItemEmpresa WHERE IdSubsidio = 0"))
Write-Output "  -- muestra de IdSubsidio huérfanos (top 10 por frecuencia) --"
$da = New-Object System.Data.OleDb.OleDbDataAdapter("SELECT TOP 10 e.IdSubsidio, COUNT(*) AS n FROM SubsidioItemEmpresa e WHERE e.IdSubsidio IS NOT NULL AND NOT EXISTS (SELECT 1 FROM SubsidioCabezal c WHERE c.IdSubsidio = e.IdSubsidio) GROUP BY e.IdSubsidio ORDER BY COUNT(*) DESC", $cn)
$dt = New-Object System.Data.DataTable; [void]$da.Fill($dt)
foreach ($r in $dt.Rows) { Write-Output ("     IdSubsidio={0}  filas={1}" -f $r['IdSubsidio'], $r['n']) }
$cn.Close()
