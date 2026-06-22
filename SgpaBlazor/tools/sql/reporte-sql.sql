-- Reportes basados en SQL crudo: una sola consulta SELECT con tokens @param que se piden al abrir y se sustituyen
-- por literales tipados/escapados antes de ejecutar. DefJson contiene la definición completa (ReporteSqlDef:
-- Sql + parámetros + columnas + SoloAdmin + MaxFilas). Mantener en sintonía con NewSgpa.Migration/Program.cs.
IF OBJECT_ID('dbo.ReporteSql') IS NULL
BEGIN
    CREATE TABLE dbo.ReporteSql (
        Id        int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ReporteSql PRIMARY KEY,
        Nombre    nvarchar(200) NOT NULL,                                   -- nombre visible (subítem de menú)
        DefJson   nvarchar(max) NOT NULL,                                   -- ReporteSqlDef serializado
        SoloAdmin bit NOT NULL CONSTRAINT DF_ReporteSql_SoloAdmin DEFAULT(1),  -- ejecución sólo admin
        Activo    bit NOT NULL CONSTRAINT DF_ReporteSql_Activo DEFAULT(1),  -- baja lógica
        Login     nvarchar(50) NULL,                                        -- autor
        Fecha     datetime2 NOT NULL CONSTRAINT DF_ReporteSql_Fecha DEFAULT SYSDATETIME()
    );
END
