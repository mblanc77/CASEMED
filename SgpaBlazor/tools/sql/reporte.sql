-- Reportes creados desde el sistema (DevExpress Reporting). Layout = REPX serializado del XtraReport.
-- TablaRoot asocia el reporte a su tabla madre (para listarlo desde su ListView). Idempotente.
IF OBJECT_ID('dbo.Reporte', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Reporte
    (
        Id        int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Reporte PRIMARY KEY,
        Nombre    nvarchar(200)  NOT NULL,
        TablaRoot nvarchar(128)  NULL,
        Layout    varbinary(max) NOT NULL,
        Fecha     datetime2      NOT NULL CONSTRAINT DF_Reporte_Fecha DEFAULT SYSDATETIME(),
        Login     nvarchar(50)   NULL
    );
    CREATE INDEX IX_Reporte_TablaRoot ON dbo.Reporte (TablaRoot);
END
GO
