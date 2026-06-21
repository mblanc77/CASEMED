# Migra usuarios/roles/asignaciones desde seguserv.mdb (Access/Jet) a seg.* en NewSgpa2.
# Requiere ejecutarse en PowerShell de 32 bits (proveedor Jet 4.0).
param(
    [string]$Mdb = 'C:\Personal\Gestion\db\seguserv.mdb',
    [string]$SqlServer = 'localhost',
    [string]$SqlDb = 'NewSgpa2'
)
$ErrorActionPreference = 'Stop'

function New-PasswordHash([string]$plain) {
    if ([string]::IsNullOrWhiteSpace($plain)) { $plain = [Guid]::NewGuid().ToString('N') } # clave inusable
    $iter = 100000
    $salt = New-Object byte[] 16
    [System.Security.Cryptography.RandomNumberGenerator]::Create().GetBytes($salt)
    $kdf = New-Object System.Security.Cryptography.Rfc2898DeriveBytes($plain, $salt, $iter, [System.Security.Cryptography.HashAlgorithmName]::SHA256)
    $hash = $kdf.GetBytes(32); $kdf.Dispose()
    return 'v1$' + $iter + '$' + [Convert]::ToBase64String($salt) + '$' + [Convert]::ToBase64String($hash)
}

# --- Leer Access ---
$cs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=$Mdb;Jet OLEDB:Database Password=$($env:CASEMED_MDB_PWD)s;"
$ace = New-Object System.Data.OleDb.OleDbConnection $cs
$ace.Open()
function Read-Table($sql) { $da = New-Object System.Data.OleDb.OleDbDataAdapter($sql, $ace); $dt = New-Object System.Data.DataTable; [void]$da.Fill($dt); return ,$dt }
$usuarios = Read-Table 'SELECT login,pass,nombre,activo,ult_acceso,fechaclave,fechaexpiracion,duracionclave,DefPerfil FROM Usuario'
$perfiles = Read-Table 'SELECT id,nombre,Admin,Cod_Sistema FROM Perfil'
$perfUsr  = Read-Table 'SELECT Id_Perfil,Cod_Usuario FROM Perfil_Usuario'
$ace.Close()
Write-Output ("Access: {0} usuarios, {1} perfiles, {2} asignaciones" -f $usuarios.Rows.Count, $perfiles.Rows.Count, $perfUsr.Rows.Count)

# --- Escribir SQL ---
$sql = New-Object System.Data.SqlClient.SqlConnection("Server=$SqlServer;Database=$SqlDb;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;")
$sql.Open()
function Exec($text, [hashtable]$params) {
    $cmd = $sql.CreateCommand(); $cmd.CommandText = $text
    if ($params) { foreach ($k in $params.Keys) { $v = $params[$k]; if ($null -eq $v) { $v = [DBNull]::Value }; [void]$cmd.Parameters.AddWithValue($k, $v) } }
    return $cmd.ExecuteNonQuery()
}
function Val($row, $col) { if ($row.IsNull($col)) { return $null } else { return $row[$col] } }

# Limpiar (orden FK)
Exec 'DELETE FROM seg.RolPermisoTabla; DELETE FROM seg.UsuarioRol; DELETE FROM seg.Usuario; DELETE FROM seg.Rol;' $null | Out-Null

# Roles (preservar ids)
Exec 'SET IDENTITY_INSERT seg.Rol ON' $null | Out-Null
$rolIds = @{}
for ($i = 0; $i -lt $perfiles.Rows.Count; $i++) {
    $r = $perfiles.Rows[$i]
    $id = [int]$r['id']
    Exec 'INSERT INTO seg.Rol (Id,Nombre,EsAdmin,CodSistema,Usr,Ts) VALUES (@id,@n,@a,@s,@u,SYSDATETIME())' `
        @{ '@id' = $id; '@n' = (Val $r 'nombre'); '@a' = [bool]$r['Admin']; '@s' = (Val $r 'Cod_Sistema'); '@u' = 'migracion' } | Out-Null
    $rolIds[$id] = $true
}
Exec 'SET IDENTITY_INSERT seg.Rol OFF' $null | Out-Null
Write-Output ("Roles insertados: {0}" -f $perfiles.Rows.Count)

# Usuarios (hash de clave)
$logins = @{}
for ($i = 0; $i -lt $usuarios.Rows.Count; $i++) {
    $u = $usuarios.Rows[$i]
    $login = [string]$u['login']
    if ([string]::IsNullOrWhiteSpace($login)) { continue }
    $hash = New-PasswordHash ([string](Val $u 'pass'))
    Exec 'INSERT INTO seg.Usuario (Login,Pass,Nombre,Activo,UltAcceso,FechaClave,FechaExpiracion,DuracionClave,DefPerfil,Usr,Ts)
          VALUES (@l,@p,@n,@act,@ua,@fc,@fe,@dc,@dp,@u,SYSDATETIME())' `
        @{ '@l' = $login; '@p' = $hash; '@n' = (Val $u 'nombre'); '@act' = [bool](Val $u 'activo');
           '@ua' = (Val $u 'ult_acceso'); '@fc' = (Val $u 'fechaclave'); '@fe' = (Val $u 'fechaexpiracion');
           '@dc' = (Val $u 'duracionclave'); '@dp' = (Val $u 'DefPerfil'); '@u' = 'migracion' } | Out-Null
    $logins[$login.ToLower()] = $true
}
Write-Output ("Usuarios insertados: {0}" -f $logins.Count)

# Asignaciones usuario-rol (solo combinaciones válidas)
$asig = 0
for ($i = 0; $i -lt $perfUsr.Rows.Count; $i++) {
    $pu = $perfUsr.Rows[$i]
    $rid = [int]$pu['Id_Perfil']; $lg = [string]$pu['Cod_Usuario']
    if (-not $rolIds.ContainsKey($rid)) { continue }
    if ([string]::IsNullOrWhiteSpace($lg) -or -not $logins.ContainsKey($lg.ToLower())) { continue }
    try { Exec 'INSERT INTO seg.UsuarioRol (Login,RolId) VALUES (@l,@r)' @{ '@l' = $lg; '@r' = $rid } | Out-Null; $asig++ } catch {}
}
Write-Output ("Asignaciones insertadas: {0}" -f $asig)

# Seed permisos base: roles NO admin -> Read+Create+Write (=7) sobre todas las tablas dbo. Delete reservado a admin.
$seeded = Exec @'
INSERT INTO seg.RolPermisoTabla (RolId, Tabla, Acciones)
SELECT r.Id, t.name, 7
FROM seg.Rol r
CROSS JOIN sys.tables t
JOIN sys.schemas s ON s.schema_id = t.schema_id AND s.name = 'dbo'
WHERE r.EsAdmin = 0;
'@ $null
Write-Output ("Permisos base sembrados (roles no-admin x tablas): {0}" -f $seeded)
$sql.Close()
Write-Output 'Migracion de seguridad COMPLETA.'
