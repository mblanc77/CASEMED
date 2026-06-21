# Afiliados/Edad/Sexo/Especialidad: corre las QueryDefs sobre la copia relinkeada (datos = NewSgpa2) vía DAO.
# Fecha de referencia fija = 2026-06-20 (para coincidir con la fecha que se pasará en SQL).
$copy = "C:\temp\sgpa_cmp.mdb"
$de = New-Object -ComObject DAO.DBEngine.36
$db = $de.OpenDatabase($copy, $false, $false, ";PWD=$($env:CASEMED_MDB_PWD)")
$f = [datetime]"2026-06-20"

function Scalar($name, $pairs) {
    $qd = $db.QueryDefs($name)
    foreach ($k in $pairs.Keys) { $qd.Parameters($k).Value = $pairs[$k] }
    $rs = $qd.OpenRecordset(4)
    $v = if ($rs.EOF) { 0 } else { $rs.Fields("Cantidad").Value }
    $rs.Close(); return $v
}
function SumCol($name, $pairs, $col) {
    $qd = $db.QueryDefs($name)
    foreach ($k in $pairs.Keys) { $qd.Parameters($k).Value = $pairs[$k] }
    $rs = $qd.OpenRecordset(4); $s = 0; $n = 0
    while (-not $rs.EOF) { $s += [double]$rs.Fields($col).Value; $n++; $rs.MoveNext() }
    $rs.Close(); return "$n filas, suma=$s"
}

"== Afiliados/Edad/Sexo (Access, fecha=$($f.ToString('yyyy-MM-dd'))) =="
"809 Activos (fecha)            = " + (Scalar "809_AfiliadoActivoFecha_Cantidad" @{ pFecha = $f })
"810 Sexo (fecha)              : " + (SumCol "810_AfiliadosSexo" @{ pFecha = $f } "Cantidad")
"810 Edad <30                   = " + (Scalar "810_AfiliadosMenores" @{ pAnioIni = [int16]29; pFecha = $f })
"810 Edad 30-39                 = " + (Scalar "810_AfiliadosEntre" @{ pAnioIni = [int16]30; pAnioFin = [int16]39; pFecha = $f })
"810 Edad 60+                   = " + (Scalar "810_AfiliadosMayores" @{ pAnioIni = [int16]60; pFecha = $f })
"812 Especialidad (fecha)      : " + (SumCol "812_AfiliadosEspecialidad" @{ pFecha = $f } "Cantidad")
"806 Cert edad <30 (sin per.)   = " + (Scalar "806_CertificadosMenores" @{ pAnioIni = [int16]29; pFechaIni = $null; pFechaFin = $null })
"806 Cert edad 30-39 (sin per.) = " + (Scalar "806_CertificadosEntre" @{ pAnioIni = [int16]30; pAnioFin = [int16]39; pFechaIni = $null; pFechaFin = $null })
$db.Close()
