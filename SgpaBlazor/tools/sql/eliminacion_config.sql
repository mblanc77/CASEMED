-- Configuración por tabla de la confirmación de borrado por texto ("tipear ELIMINAR").
-- Default (sin fila): RequiereTexto = 1 (activado). Para desactivar en una tabla, se guarda una fila con 0.
-- Idempotente.
IF OBJECT_ID('dbo.EliminacionConfig', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.EliminacionConfig
    (
        Tabla         nvarchar(128) NOT NULL CONSTRAINT PK_EliminacionConfig PRIMARY KEY,
        RequiereTexto bit           NOT NULL CONSTRAINT DF_EliminacionConfig_RequiereTexto DEFAULT 1
    );
END
GO
