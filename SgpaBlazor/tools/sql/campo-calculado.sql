-- Campos calculados por tabla: expresión (sintaxis CriteriaOperator) reutilizable en cualquier consulta de esa
-- tabla (reportes dinámicos, filtros, ListViews). La traducción a SQL la hace ScalarSqlTranslator en la app.
-- Mantener en sintonía con NewSgpa.Migration/Program.cs:CreateInfrastructureTablesAsync.
IF OBJECT_ID('dbo.CampoCalculado','U') IS NULL
BEGIN
    CREATE TABLE dbo.CampoCalculado (
        Id            int IDENTITY(1,1) NOT NULL CONSTRAINT PK_CampoCalculado PRIMARY KEY,
        Tabla         nvarchar(128) NOT NULL,   -- entidad/tabla dueña del campo
        Nombre        nvarchar(128) NOT NULL,   -- nombre del campo calculado (único por tabla)
        Caption       nvarchar(200) NULL,       -- encabezado a mostrar
        Expr          nvarchar(max) NOT NULL,   -- expresión CriteriaOperator (ej. DateDiffDay([FechaIni],[FechaFin])+1)
        TipoResultado nvarchar(20)  NOT NULL CONSTRAINT DF_CampoCalculado_Tipo DEFAULT('decimal'), -- int/decimal/datetime/bool/string
        DisplayFormat nvarchar(50)  NULL,
        Activo        bit NOT NULL CONSTRAINT DF_CampoCalculado_Activo DEFAULT(1),
        Usr           nvarchar(16)  NULL,
        Ts            datetime2     NULL
    );
    CREATE UNIQUE INDEX UX_CampoCalculado_Tabla_Nombre ON dbo.CampoCalculado(Tabla, Nombre);
END
