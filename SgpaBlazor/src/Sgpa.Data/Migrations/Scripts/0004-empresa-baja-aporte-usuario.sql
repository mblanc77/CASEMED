-- Empresa: baja lógica + aporte del afiliado.
--  · FechaBaja      — empresas que ya no están activas. NO se borran (rompería la visualización de datos
--                     históricos): en los combos de empresa se muestran al final y atenuadas. El flag "baja"
--                     se deriva en consulta (FechaBaja IS NOT NULL), no se persiste una columna bit aparte.
--  · AporteUsuario  — aporte del afiliado, idéntico a AporteCasemed (SQL real → C# float?).
-- Run-once (DbUp, journalizado en dbo.SchemaVersions). Guards por columna → re-aplicable sin daño.

IF COL_LENGTH('dbo.Empresa', 'FechaBaja') IS NULL
    ALTER TABLE dbo.Empresa ADD FechaBaja date NULL;
GO

IF COL_LENGTH('dbo.Empresa', 'AporteUsuario') IS NULL
    ALTER TABLE dbo.Empresa ADD AporteUsuario real NULL;
GO
