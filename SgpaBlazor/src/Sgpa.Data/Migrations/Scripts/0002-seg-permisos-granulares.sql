-- Seguridad granular estilo XAF: restricciones por columna y filtros por registro, por rol.
-- Semántica SUSTRACTIVA (permitir por defecto): si el rol tiene permiso sobre la tabla
-- (seg.RolPermisoTabla), ve todos los campos/registros salvo donde haya una fila acá que reste acceso.
-- Run-once (DbUp, journalizado en dbo.SchemaVersions). Los guard IF OBJECT_ID lo hacen re-aplicable sin daño.
-- Asume que seg.Rol ya existe (tools/sql/seg-schema.sql).

-- Restricción por columna. Fila presente = restricción para ese (rol, tabla, columna);
-- ausencia de fila = acceso pleno (Leer = 1, Modificar = 1).
IF OBJECT_ID('seg.RolPermisoColumna', 'U') IS NULL
BEGIN
    CREATE TABLE seg.RolPermisoColumna
    (
        RolId     int           NOT NULL,
        Tabla     nvarchar(128) NOT NULL,
        Columna   nvarchar(128) NOT NULL,
        Leer      bit NOT NULL CONSTRAINT DF_seg_RPC_Leer DEFAULT(1),
        Modificar bit NOT NULL CONSTRAINT DF_seg_RPC_Mod  DEFAULT(1),
        CONSTRAINT PK_seg_RolPermisoColumna PRIMARY KEY (RolId, Tabla, Columna),
        CONSTRAINT FK_seg_RPC_Rol FOREIGN KEY (RolId) REFERENCES seg.Rol(Id) ON DELETE CASCADE
    );
END
GO

-- Filtro por registro. Acciones = flags PermissionAction acotado a Read(1) | Write(4) | Delete(8):
-- a qué operaciones aplica el criterio. Criteria = string CriteriaOperator (DevExpress), traducido a SQL.
IF OBJECT_ID('seg.RolPermisoRegistro', 'U') IS NULL
BEGIN
    CREATE TABLE seg.RolPermisoRegistro
    (
        Id        int IDENTITY(1,1) NOT NULL CONSTRAINT PK_seg_RolPermisoRegistro PRIMARY KEY,
        RolId     int           NOT NULL,
        Tabla     nvarchar(128) NOT NULL,
        Acciones  int           NOT NULL,
        Criteria  nvarchar(max) NOT NULL,
        CONSTRAINT FK_seg_RPR_Rol FOREIGN KEY (RolId) REFERENCES seg.Rol(Id) ON DELETE CASCADE
    );
    CREATE INDEX IX_seg_RPR_RolTabla ON seg.RolPermisoRegistro (RolId, Tabla);
END
GO
