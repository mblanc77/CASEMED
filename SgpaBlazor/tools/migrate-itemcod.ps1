# Completa la config de dbo.SubsidioItemCod en NewSgpa2 con los valores del backend Access
# (que la migración original dejó en NULL). Ejecutar en PowerShell 32-bit (proveedor Jet 4.0).
param(
    [string]$Mdb = 'C:\Personal\Gestion\db\sgpaserv.mdb',
    # La clave sale de $env:CASEMED_MDB_PWD (sgpaserv.mdb NO lleva la 's' final que sí usa seguserv.mdb).
    # (Antes estaba entre comillas SIMPLES -> no expandía y mandaba el literal, fallando con "Multiple-step OLE DB".)
    [string]$Password = "$($env:CASEMED_MDB_PWD)",
    [string]$SqlServer = 'localhost',
    [string]$SqlDb = 'NewSgpa2'
)
$ErrorActionPreference = 'Stop'

$ace = New-Object System.Data.OleDb.OleDbConnection ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=$Mdb;Jet OLEDB:Database Password=$Password;")
$ace.Open()
$da = New-Object System.Data.OleDb.OleDbDataAdapter("SELECT CodSubsidioItemCod,Tipo,ValorTipo,Valor,TipoComp,Operador,Comparar,CompararContra,ValorMin,ValorMax,ModificaNominal,Signo,Procesar FROM SubsidioItemCod", $ace)
$dt = New-Object System.Data.DataTable
[void]$da.Fill($dt)
$ace.Close()
Write-Output ("Leidas $($dt.Rows.Count) filas del Access")

$sql = New-Object System.Data.SqlClient.SqlConnection("Server=$SqlServer;Database=$SqlDb;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;")
$sql.Open()
$updated = 0
for ($i = 0; $i -lt $dt.Rows.Count; $i++) {
    $r = $dt.Rows[$i]
    $cmd = $sql.CreateCommand()
    $cmd.CommandText = @'
UPDATE dbo.SubsidioItemCod
SET Tipo=@Tipo, ValorTipo=@ValorTipo, Valor=@Valor, TipoComp=@TipoComp, Operador=@Operador,
    Comparar=@Comparar, CompararContra=@CompararContra, ValorMin=@ValorMin, ValorMax=@ValorMax,
    ModificaNominal=ISNULL(@ModificaNominal,0), Signo=ISNULL(@Signo,1), Procesar=ISNULL(@Procesar,1), Usr='migra', Ts=SYSDATETIME()
WHERE CodSubsidioItemCod=@Cod
'@
    foreach ($c in @('Tipo','ValorTipo','Valor','TipoComp','Operador','Comparar','CompararContra','ValorMin','ValorMax','ModificaNominal','Signo','Procesar')) {
        $v = $r[$c]; if ($v -eq [DBNull]::Value) { $v = [DBNull]::Value }
        [void]$cmd.Parameters.AddWithValue("@$c", $v)
    }
    [void]$cmd.Parameters.AddWithValue("@Cod", $r['CodSubsidioItemCod'])
    $updated += $cmd.ExecuteNonQuery()
}
$sql.Close()
Write-Output ("Filas actualizadas en NewSgpa2: $updated")
