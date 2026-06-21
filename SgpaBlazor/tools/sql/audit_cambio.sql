-- Auditoría de cambios por campo (estilo XAF): una fila por campo cambiado, con valor anterior/nuevo.
-- Operacion: I=alta, U=modificación, D=baja. Idempotente.
IF OBJECT_ID('dbo.AuditCambio', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.AuditCambio
    (
        Id            bigint        IDENTITY(1,1) NOT NULL CONSTRAINT PK_AuditCambio PRIMARY KEY,
        Fecha         datetime2     NOT NULL CONSTRAINT DF_AuditCambio_Fecha DEFAULT SYSDATETIME(),
        Login         nvarchar(50)  NULL,
        Tabla         nvarchar(128) NOT NULL,
        Clave         nvarchar(400) NULL,   -- clave primaria del registro, serializada
        Operacion     char(1)       NOT NULL, -- I / U / D
        Campo         nvarchar(128) NULL,
        ValorAnterior nvarchar(max) NULL,
        ValorNuevo    nvarchar(max) NULL
    );
    CREATE INDEX IX_AuditCambio_Tabla_Clave ON dbo.AuditCambio (Tabla, Clave);
    CREATE INDEX IX_AuditCambio_Fecha       ON dbo.AuditCambio (Fecha DESC);
END
GO
