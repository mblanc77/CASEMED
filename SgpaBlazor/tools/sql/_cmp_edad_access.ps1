$back = "C:\Personal\Gestion\CASEMED\VB6\Sgpa\Data\sgpaserv2k3.mdb"
$cn = New-Object System.Data.OleDb.OleDbConnection ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=$back;Jet OLEDB:Database Password=$($env:CASEMED_MDB_PWD);")
$cn.Open()
function Q($l, $sql) { $c = $cn.CreateCommand(); $c.CommandText = $sql; Write-Output ("{0,-30}= {1}" -f $l, $c.ExecuteScalar()) }
$act = "Afiliado.CI In (Select CI From Trabaja Where (FechaBaja Is Null Or FechaBaja > #2026/06/20#) And FechaIngreso <= #2026/06/20#)"
$cert = "Afiliado.CI In (Select Ci From Certificacion Where Efectiva=True)"
$ed = "DateDiff('yyyy',Afiliado.FechaNacimiento,Date())"
Q "810 Edad <30"        "SELECT Count(*) FROM Afiliado WHERE $act AND $ed <= 29"
Q "810 Edad 30-39"      "SELECT Count(*) FROM Afiliado WHERE $act AND $ed Between 30 And 39"
Q "810 Edad 60+"        "SELECT Count(*) FROM Afiliado WHERE $act AND $ed >= 60"
Q "806 Cert edad <30"   "SELECT Count(*) FROM Afiliado WHERE $cert AND $ed <= 29"
Q "806 Cert edad 30-39" "SELECT Count(*) FROM Afiliado WHERE $cert AND $ed Between 30 And 39"
$cn.Close()
