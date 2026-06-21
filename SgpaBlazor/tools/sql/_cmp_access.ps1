# Compara las queries de Access (corridas sobre sgpaserv2k3.mdb, mismo snapshot que NewSgpa2)
# devolviendo conteo de filas + suma de Cantidad, para período 2025 y patología = todas.
$front = "C:\Personal\Gestion\CASEMED\VB6\Sgpa\sgpa.mdb"            # tiene las QueryDefs
$back  = "C:\Personal\Gestion\CASEMED\VB6\Sgpa\Data\sgpaserv2k3.mdb" # tiene los datos (= NewSgpa2)

$csFront = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=$front;Jet OLEDB:Database Password=$($env:CASEMED_MDB_PWD);"
$csBack  = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=$back;Jet OLEDB:Database Password=$($env:CASEMED_MDB_PWD);"

$cnF = New-Object System.Data.OleDb.OleDbConnection $csFront; $cnF.Open()
$cnB = New-Object System.Data.OleDb.OleDbConnection $csBack;  $cnB.Open()

# SQL de cada QueryDef
$defs = @{}
$st = $cnF.GetOleDbSchemaTable([System.Data.OleDb.OleDbSchemaGuid]::Procedures, $null)
foreach ($row in $st.Rows) { $defs[[string]$row["PROCEDURE_NAME"]] = [string]$row["PROCEDURE_DEFINITION"] }

$targets = @("808_CertificadosAfecciones","816_Certificados_GrupoAfeccion","817_Certificados_Patologia",
             "818_Certificados_Patologia","819_Certificados_AfeccionGrupo","820_Certificados_AfeccionTipo",
             "806_CertificadosSexo","807_CertificadosEspecialidad")

foreach ($t in $targets) {
    $sql = $defs[$t]
    if (-not $sql) { Write-Output ("{0,-34} (no encontrada)" -f $t); continue }
    # quitar PARAMETERS ...; (primera sentencia)
    $sql = [regex]::Replace($sql, '(?is)^\s*PARAMETERS.*?;', '')
    # inyectar período y patología = todas
    $sql = $sql.Replace('[pFechaIni]', '#2025/01/01#').Replace('[pFechaFin]', '#2025/12/31#').Replace('[pCodPatologia]', 'Null')
    $sql = $sql.Trim().TrimEnd(';').Trim()
    $wrap = "SELECT COUNT(*) AS Filas, Sum(q.Cantidad) AS Tot FROM ($sql) AS q"
    try {
        $cmd = $cnB.CreateCommand(); $cmd.CommandText = $wrap
        $rd = $cmd.ExecuteReader(); $rd.Read()
        Write-Output ("{0,-34} filas={1,-5} total={2}" -f $t, $rd[0], $rd[1])
        $rd.Close()
    } catch { Write-Output ("{0,-34} ERR {1}" -f $t, $_.Exception.Message.Split([Environment]::NewLine)[0]) }
}
$cnF.Close(); $cnB.Close()
