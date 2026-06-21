-- Esquema de seguridad nuevo para SgpaBlazor (reemplaza segurida.mdb / seguserv.mdb).
-- Roles por perfil (con bypass admin) + permisos CRUD por tabla.
SET NOCOUNT ON;

IF SCHEMA_ID('seg') IS NULL EXEC('CREATE SCHEMA seg');

IF OBJECT_ID('seg.RolPermisoTabla') IS NOT NULL DROP TABLE seg.RolPermisoTabla;
IF OBJECT_ID('seg.UsuarioRol')      IS NOT NULL DROP TABLE seg.UsuarioRol;
IF OBJECT_ID('seg.Usuario')         IS NOT NULL DROP TABLE seg.Usuario;
IF OBJECT_ID('seg.Rol')             IS NOT NULL DROP TABLE seg.Rol;

CREATE TABLE seg.Rol (
    Id          int IDENTITY(1,1) NOT NULL CONSTRAINT PK_seg_Rol PRIMARY KEY,
    Nombre      nvarchar(50)  NOT NULL,
    EsAdmin     bit           NOT NULL CONSTRAINT DF_seg_Rol_EsAdmin DEFAULT(0),
    CodSistema  nvarchar(20)  NULL,
    Usr         nvarchar(16)  NULL,
    Ts          datetime2     NULL
);

CREATE TABLE seg.Usuario (
    Login           nvarchar(50)  NOT NULL CONSTRAINT PK_seg_Usuario PRIMARY KEY,
    Pass            nvarchar(200) NOT NULL,            -- hash PBKDF2 (v1$iter$salt$hash)
    Nombre          nvarchar(100) NULL,
    Activo          bit           NOT NULL CONSTRAINT DF_seg_Usuario_Activo DEFAULT(1),
    UltAcceso       datetime2     NULL,
    FechaClave      datetime2     NULL,
    FechaExpiracion datetime2     NULL,
    DuracionClave   int           NULL,
    DefPerfil       int           NULL,
    Usr             nvarchar(16)  NULL,
    Ts              datetime2     NULL
);

CREATE TABLE seg.UsuarioRol (
    Login  nvarchar(50) NOT NULL,
    RolId  int          NOT NULL,
    CONSTRAINT PK_seg_UsuarioRol PRIMARY KEY (Login, RolId),
    CONSTRAINT FK_seg_UsuarioRol_Usuario FOREIGN KEY (Login) REFERENCES seg.Usuario(Login) ON DELETE CASCADE,
    CONSTRAINT FK_seg_UsuarioRol_Rol     FOREIGN KEY (RolId) REFERENCES seg.Rol(Id)        ON DELETE CASCADE
);

-- Permisos CRUD por tabla y rol. Acciones = flags PermissionAction (Read=1,Create=2,Write=4,Delete=8).
CREATE TABLE seg.RolPermisoTabla (
    RolId     int           NOT NULL,
    Tabla     nvarchar(128) NOT NULL,
    Acciones  int           NOT NULL CONSTRAINT DF_seg_RolPermisoTabla_Acc DEFAULT(0),
    CONSTRAINT PK_seg_RolPermisoTabla PRIMARY KEY (RolId, Tabla),
    CONSTRAINT FK_seg_RolPermisoTabla_Rol FOREIGN KEY (RolId) REFERENCES seg.Rol(Id) ON DELETE CASCADE
);

PRINT 'Esquema seg.* creado.';
