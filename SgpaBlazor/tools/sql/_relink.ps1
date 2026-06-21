# Copia sgpa.mdb y relinkea sus tablas vinculadas al backend sgpaserv2k3.mdb (= datos de NewSgpa2),
# para poder correr las QueryDefs originales sobre el mismo snapshot. Usa DAO (32-bit).
$src  = "C:\Personal\Gestion\CASEMED\VB6\Sgpa\sgpa.mdb"
$copy = "C:\temp\sgpa_cmp.mdb"
$back = "C:\Personal\Gestion\CASEMED\VB6\Sgpa\Data\sgpaserv2k3.mdb"

Copy-Item $src $copy -Force
$de = New-Object -ComObject DAO.DBEngine.36
$db = $de.OpenDatabase($copy, $false, $false, ";PWD=$($env:CASEMED_MDB_PWD)")
$relinked = 0; $afil = 0
foreach ($td in $db.TableDefs) {
    if ($td.Connect -ne "") {
        $td.Connect = ";DATABASE=$back;PWD=$($env:CASEMED_MDB_PWD)"
        $td.RefreshLink()
        $relinked++
    }
}
Write-Output ("Tablas relinkeadas: " + $relinked)
$rs = $db.OpenRecordset("SELECT COUNT(*) AS C FROM Afiliado")
Write-Output ("Afiliado (copia relinkeada) = " + $rs.Fields("C").Value)
$rs.Close()
$rs = $db.OpenRecordset("SELECT COUNT(*) AS C FROM Certificacion")
Write-Output ("Certificacion = " + $rs.Fields("C").Value)
$rs.Close()
$db.Close()
