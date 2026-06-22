-- Reportes dinámicos creados por el administrador (selección de tabla raíz + campos N-1 + filtro con
-- parámetros estilo Request Parameters). DefJson contiene la definición completa (ReporteDinamicoDef).
IF OBJECT_ID('dbo.ReporteDinamico') IS NULL
BEGIN
    CREATE TABLE dbo.ReporteDinamico (
        Id        int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ReporteDinamico PRIMARY KEY,
        Nombre    nvarchar(200) NOT NULL,                                   -- nombre visible (subítem de menú)
        RootTable nvarchar(128) NOT NULL,                                   -- tabla/entidad raíz (denormalizada)
        DefJson   nvarchar(max) NOT NULL,                                   -- ReporteDinamicoDef serializado
        Activo    bit NOT NULL CONSTRAINT DF_ReporteDinamico_Activo DEFAULT(1),  -- baja lógica
        Login     nvarchar(50) NULL,                                        -- autor
        Fecha     datetime2 NOT NULL CONSTRAINT DF_ReporteDinamico_Fecha DEFAULT SYSDATETIME()
    );
    CREATE INDEX IX_ReporteDinamico_RootTable ON dbo.ReporteDinamico(RootTable);
END
