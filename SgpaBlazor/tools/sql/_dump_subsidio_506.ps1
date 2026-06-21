$mdb = "C:\Personal\Gestion\CASEMED\VB6\Sgpa\sgpa.mdb"
$cn = New-Object System.Data.OleDb.OleDbConnection ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=$mdb;Jet OLEDB:Database Password=$($env:CASEMED_MDB_PWD);")
$cn.Open()
$want = @('506_Update_Liquidos','506_TotalLiquidoBPSCIMes','506_Insert_LiquidacionBPS','506_Rpt_LiquidacionBPS','506_Rpt_Subsidio','506_Export_SubsidioConBPS','506_Delete_Liquidacion_BPSXEntrega')
foreach ($guid in @([System.Data.OleDb.OleDbSchemaGuid]::Procedures, [System.Data.OleDb.OleDbSchemaGuid]::Views)) {
    $st = $cn.GetOleDbSchemaTable($guid, $null)
    $nameCol = if ($st.Columns.Contains("PROCEDURE_NAME")) { "PROCEDURE_NAME" } else { "TABLE_NAME" }
    $defCol  = if ($st.Columns.Contains("PROCEDURE_DEFINITION")) { "PROCEDURE_DEFINITION" } else { "VIEW_DEFINITION" }
    foreach ($row in $st.Rows) {
        $n = [string]$row[$nameCol]
        if ($want -contains $n) { Write-Output "===== $n ====="; Write-Output ([string]$row[$defCol]); Write-Output "" }
    }
}
$cn.Close()
