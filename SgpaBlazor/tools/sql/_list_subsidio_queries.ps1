$mdb = "C:\Personal\Gestion\CASEMED\VB6\Sgpa\sgpa.mdb"
$cn = New-Object System.Data.OleDb.OleDbConnection ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=$mdb;Jet OLEDB:Database Password=$($env:CASEMED_MDB_PWD);")
$cn.Open()
$pat = 'subsid|liquid|irpf|aguinaldo|retenc|prima|smn|bps'
foreach ($guid in @([System.Data.OleDb.OleDbSchemaGuid]::Procedures, [System.Data.OleDb.OleDbSchemaGuid]::Views)) {
    $st = $cn.GetOleDbSchemaTable($guid, $null)
    $nameCol = if ($st.Columns.Contains("PROCEDURE_NAME")) { "PROCEDURE_NAME" } else { "TABLE_NAME" }
    foreach ($row in $st.Rows) {
        $n = [string]$row[$nameCol]
        if ($n -imatch $pat) { Write-Output $n }
    }
}
$cn.Close()
