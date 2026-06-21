# Cuenta las filas de las QueryDefs de franjas (811/814) vía DAO sobre la copia relinkeada (datos = NewSgpa2).
# DAO tolera el Switch en GROUP BY (lo que OLEDB rechaza).
$copy = "C:\temp\sgpa_cmp.mdb"
$de = New-Object -ComObject DAO.DBEngine.36
$db = $de.OpenDatabase($copy, $false, $false, ";PWD=$($env:CASEMED_MDB_PWD)")

function CountQ($name, $pairs) {
    $qd = $db.QueryDefs($name)
    foreach ($k in $pairs.Keys) { $qd.Parameters($k).Value = $pairs[$k] }
    $rs = $qd.OpenRecordset(4)  # dbOpenSnapshot
    $n = 0
    if (-not $rs.EOF) { $rs.MoveLast(); $n = $rs.RecordCount }
    $rs.Close()
    return $n
}

"811_Afiliado<125_Pct_Ult6 (prom6) = " + (CountQ "811_Afiliado<125_Pct_Ult6" @{ pMes = 202603; pMesIni = 202510; pSMN = 6864; pCodEmpresa = 0 })
"811_Afiliado<125_Pct_UltMes (mes)  = " + (CountQ "811_Afiliado<125_Pct_UltMes" @{ pMes = 202603; pSMN = 6864; pCodEmpresa = 0 })
"814_AfiliadoImponibleFranja (mes)  = " + (CountQ "814_AfiliadoImponibleFranja" @{ pMes = 202603; pSMN = 6864; pCodEmpresa = 0 })
"815_AfiliadoImponible (mes)        = " + (CountQ "815_AfiliadoImponible" @{ pMes = 202603; pSMN = 6864; pCodEmpresa = 0 })
$db.Close()
