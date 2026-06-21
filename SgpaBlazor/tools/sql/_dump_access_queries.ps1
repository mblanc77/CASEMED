param([string]$Out = "C:\temp\access_queries.txt")
$mdb = "C:\Personal\Gestion\CASEMED\VB6\Sgpa\sgpa.mdb"
$cs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=$mdb;Jet OLEDB:Database Password=$($env:CASEMED_MDB_PWD);"
$cn = New-Object System.Data.OleDb.OleDbConnection $cs
$cn.Open()
$sb = New-Object System.Text.StringBuilder
foreach ($guid in @([System.Data.OleDb.OleDbSchemaGuid]::Procedures, [System.Data.OleDb.OleDbSchemaGuid]::Views)) {
    $st = $cn.GetOleDbSchemaTable($guid, $null)
    $nameCol = if ($st.Columns.Contains("PROCEDURE_NAME")) { "PROCEDURE_NAME" } else { "TABLE_NAME" }
    $defCol  = if ($st.Columns.Contains("PROCEDURE_DEFINITION")) { "PROCEDURE_DEFINITION" } else { "VIEW_DEFINITION" }
    foreach ($row in $st.Rows) {
        $n = [string]$row[$nameCol]
        if ($n -match '^(80[0-9]|81[0-9]|82[0-9]|83[0-9])_') {
            [void]$sb.AppendLine("===== $n =====")
            [void]$sb.AppendLine([string]$row[$defCol])
            [void]$sb.AppendLine("")
        }
    }
}
$cn.Close()
Set-Content -Path $Out -Value $sb.ToString() -Encoding UTF8
Write-Output ("Escrito: " + $Out + "  (" + $sb.Length + " chars)")
