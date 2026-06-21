param(
    [string]$Mdb = 'C:\Personal\Gestion\CASEMED\VB6\Sgpa\sgpa.mdb',
    [string]$Password = '$($env:CASEMED_MDB_PWD)'
)
$ErrorActionPreference = 'Stop'
$names = @('300_AfiliadoDiasImporte', '300_AfiliadoValorJornalxEmpresa', '300_TrabajaActivo', '300_AfiliadoAporteOk')
$cs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=$Mdb;Jet OLEDB:Database Password=$Password;"
$cn = New-Object System.Data.OleDb.OleDbConnection $cs
$cn.Open()
# Las queries Access (SELECT) aparecen como VIEWS; las de acción como PROCEDURES.
foreach ($coll in @([System.Data.OleDb.OleDbSchemaGuid]::Views, [System.Data.OleDb.OleDbSchemaGuid]::Procedures)) {
    $dt = $cn.GetOleDbSchemaTable($coll, $null)
    foreach ($r in $dt.Rows) {
        $nm = $r[($dt.Columns | Where-Object { $_.ColumnName -match 'TABLE_NAME|PROCEDURE_NAME' } | Select-Object -First 1).ColumnName]
        if ($names -contains [string]$nm) {
            $defCol = ($dt.Columns | Where-Object { $_.ColumnName -match 'DEFINITION' } | Select-Object -First 1).ColumnName
            Write-Output "===== $nm ====="
            Write-Output ([string]$r[$defCol])
            Write-Output ""
        }
    }
}
$cn.Close()
