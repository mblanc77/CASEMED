# Corre las QueryDefs de Imponibles/haberes sobre la copia relinkeada (datos = NewSgpa2).
# Escenario: mes=202603, mesIni=202510, SMN=6864, empresa=0 (todas).
$copy = "C:\temp\sgpa_cmp.mdb"
$cs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=$copy;Jet OLEDB:Database Password=$($env:CASEMED_MDB_PWD);"
$cn = New-Object System.Data.OleDb.OleDbConnection $cs; $cn.Open()

function RunCount($name, $vals) {
    try {
        $cmd = New-Object System.Data.OleDb.OleDbCommand("[$name]", $cn)
        $cmd.CommandType = [System.Data.CommandType]::StoredProcedure
        foreach ($v in $vals) { [void]$cmd.Parameters.AddWithValue("p", $v) }
        $rd = $cmd.ExecuteReader(); $n = 0; while ($rd.Read()) { $n++ }; $rd.Close(); return $n
    } catch { return "ERR " + $_.Exception.Message.Split([Environment]::NewLine)[0] }
}
function RunSum($name, $vals, $col) {
    try {
        $cmd = New-Object System.Data.OleDb.OleDbCommand("[$name]", $cn)
        $cmd.CommandType = [System.Data.CommandType]::StoredProcedure
        foreach ($v in $vals) { [void]$cmd.Parameters.AddWithValue("p", $v) }
        $rd = $cmd.ExecuteReader(); $s = 0.0; while ($rd.Read()) { if ($rd[$col] -ne [DBNull]::Value) { $s += [double]$rd[$col] } }; $rd.Close(); return $s
    } catch { return "ERR " + $_.Exception.Message.Split([Environment]::NewLine)[0] }
}

$m = 202603; $mi = 202510; $smn = 6864.0; $emp = 0
"== Imponibles (Access, relinkeado) =="
"801_Promedio_Ult6 (Haberes0 prom6) = " + (RunCount "801_Promedio_Ult6" @($m,$mi,$emp))
"802_Ult6 (>1.25 prom6)            = " + (RunCount "802_Ult6" @($m,$mi,$smn,$emp))
"801_CI_Todos (total prom6)         = " + (RunCount "801_CI_Todos" @($m,$mi,$emp))
"801_Promedio_UltMes (Haberes0 mes) = " + (RunCount "801_Promedio_UltMes" @($m,$emp))
"802_UltMes (>1.25 mes)             = " + (RunCount "802_UltMes" @($m,$smn,$emp))
"803_Ult6 (>20 prom6)               = " + (RunCount "803_Ult6" @($m,$mi,$smn,$emp))
"802_>0_Ult6 (>0 prom6)             = " + (RunCount "802_>0_Ult6" @($m,$mi,$emp))
"804_Ult6 masa>20 prom6             = " + (RunSum "804_Ult6" @($m,$mi,$smn,$emp) "Promedio")
"804_>0_Ult6 masa>0 prom6           = " + (RunSum "804_>0_Ult6" @($m,$mi,$emp) "Promedio")
"811_<125_Ult6 (franjas prom6)      = " + (RunCount "811_Afiliado<125_Pct_Ult6" @($m,$mi,$smn,$emp))
"814_ImponibleFranja (mes)          = " + (RunCount "814_AfiliadoImponibleFranja" @($m,$smn,$emp))
$cn.Close()
