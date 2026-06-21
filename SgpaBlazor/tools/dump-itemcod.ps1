param([string]$Mdb = 'C:\Personal\Gestion\db\sgpaserv.mdb', [string]$Password = '$($env:CASEMED_MDB_PWD)')
$ErrorActionPreference = 'Stop'
$cs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=$Mdb;Jet OLEDB:Database Password=$Password;"
$cn = New-Object System.Data.OleDb.OleDbConnection $cs
$cn.Open()
$da = New-Object System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [SubsidioItemCod] ORDER BY CodSubsidioItemCod", $cn)
$dt = New-Object System.Data.DataTable
[void]$da.Fill($dt)
$nn = ($dt.Rows | Where-Object { $_['Valor'] -ne [DBNull]::Value }).Count
Write-Output ("Filas=$($dt.Rows.Count) con_Valor_no_null=$nn")
Write-Output ("COLS: " + (($dt.Columns | ForEach-Object { $_.ColumnName }) -join ','))
foreach ($r in $dt.Rows) {
    function V($c) { if ($r[$c] -eq [DBNull]::Value) { '.' } else { [string]$r[$c] } }
    Write-Output ("{0,-5}|{1,-24}|Tipo={2}|VTipo={3}|Valor={4}|TComp={5}|Op={6}|Min={7}|Max={8}|Comparar={9}|CompContra={10}|ModNom={11}|Signo={12}" -f `
        (V 'CodSubsidioItemCod'), (V 'Descrip'), (V 'Tipo'), (V 'ValorTipo'), (V 'Valor'), (V 'TipoComp'), (V 'Operador'), (V 'ValorMin'), (V 'ValorMax'), (V 'Comparar'), (V 'CompararContra'), (V 'ModificaNominal'), (V 'Signo'))
}
$cn.Close()
