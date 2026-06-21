# (Re)crea el usuario de test interno 'qa' (admin) en seg.* de NewSgpa2 con clave conocida.
# Necesario tras regenerar la base (migrate-seguridad sólo trae los usuarios de seguserv.mdb).
# Uso: powershell -File create-qa-user.ps1   (correr en PS normal; no necesita 32-bit)
param(
    [string]$Login = 'qa',
    [string]$Password = 'Sgpa.QA.2026!',
    [string]$SqlServer = 'localhost',
    [string]$SqlDb = 'NewSgpa2'
)
$ErrorActionPreference = 'Stop'

# Hash PBKDF2-SHA256 en el formato de la app: v1$iter$base64(salt)$base64(hash)
$iter = 100000
$salt = New-Object byte[] 16
[System.Security.Cryptography.RandomNumberGenerator]::Create().GetBytes($salt)
$kdf = New-Object System.Security.Cryptography.Rfc2898DeriveBytes($Password, $salt, $iter, [System.Security.Cryptography.HashAlgorithmName]::SHA256)
$pass = 'v1$' + $iter + '$' + [Convert]::ToBase64String($salt) + '$' + [Convert]::ToBase64String($kdf.GetBytes(32))
$kdf.Dispose()

$sql = @"
SET NOCOUNT ON;
DECLARE @rid int = (SELECT TOP 1 Id FROM seg.Rol WHERE EsAdmin = 1 ORDER BY Id);
DELETE FROM seg.UsuarioRol WHERE Login = '$Login';
DELETE FROM seg.Usuario   WHERE Login = '$Login';
INSERT INTO seg.Usuario(Login, Pass, Nombre, Activo, Usr, Ts) VALUES('$Login', '$pass', 'QA Test', 1, 'migra', SYSDATETIME());
INSERT INTO seg.UsuarioRol(Login, RolId) VALUES('$Login', @rid);
SELECT 'usuario ' + '$Login' + ' (admin) listo' AS estado;
"@
sqlcmd -S $SqlServer -E -d $SqlDb -b -Q $sql
Write-Output "Login: $Login  /  Password: $Password"
