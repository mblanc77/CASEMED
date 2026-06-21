-- Configuración dinámica por tabla, consolidada: edición inline (port del set hardcodeado de
-- EntityCatalog), confirmación de borrado por texto (migrada de EliminacionConfig) y auditoría por campo.
-- Idempotente.
IF OBJECT_ID('dbo.TablaConfig', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.TablaConfig
    (
        Tabla            nvarchar(128) NOT NULL CONSTRAINT PK_TablaConfig PRIMARY KEY,
        EdicionInline    bit NOT NULL CONSTRAINT DF_TablaConfig_Inline DEFAULT 0,
        ConfirmarBorrado bit NOT NULL CONSTRAINT DF_TablaConfig_Conf   DEFAULT 1,
        Auditar          bit NOT NULL CONSTRAINT DF_TablaConfig_Aud    DEFAULT 0,
        Alias            nvarchar(200) NULL,
        -- Disponibilidad para el motor de reportes (ObjectDataSource). NULL = default heurístico (ReportableTables).
        DisponibleReportes bit NULL
    );

    -- Seed: edición inline (catálogos Gr*), antes hardcodeado en EntityCatalog.InlineEditTables.
    INSERT INTO dbo.TablaConfig (Tabla, EdicionInline)
    SELECT v, 1 FROM (VALUES
        ('Mutualista'),('Banco'),('Certificador'),('Patologia'),('AfeccionGrupo'),('AfeccionTipo'),
        ('SalidaTipo'),('BajaMotivo'),('FormaPago'),('AporteTipo'),('RegimenAporte'),('RegimenJubilatorio'),
        ('SituacionPago'),('SituacionMutual'),('Departamento'),('Especialidad'),('PrestacionTipo'),
        ('RecetaDistancia'),('IMS')
    ) t(v);

    -- Migrar confirmación de borrado desde EliminacionConfig (si existe).
    IF OBJECT_ID('dbo.EliminacionConfig', 'U') IS NOT NULL
        MERGE dbo.TablaConfig AS t
        USING (SELECT Tabla, RequiereTexto FROM dbo.EliminacionConfig) AS s ON t.Tabla = s.Tabla
        WHEN MATCHED THEN UPDATE SET ConfirmarBorrado = s.RequiereTexto
        WHEN NOT MATCHED THEN INSERT (Tabla, ConfirmarBorrado) VALUES (s.Tabla, s.RequiereTexto);
END
GO

-- Para bases ya creadas antes de agregar la columna (idempotente).
IF COL_LENGTH('dbo.TablaConfig', 'DisponibleReportes') IS NULL
    ALTER TABLE dbo.TablaConfig ADD DisponibleReportes bit NULL;
GO
