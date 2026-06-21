-- Filtros guardados de los ListView (equivalente moderno a la tabla VB6 'Seleccion').
-- Criteria = string de DevExpress CriteriaOperator (round-trip con Parse/ToString).
IF OBJECT_ID('dbo.SgpaFiltro') IS NULL
BEGIN
    CREATE TABLE dbo.SgpaFiltro (
        Id        int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SgpaFiltro PRIMARY KEY,
        Entity    nvarchar(128) NOT NULL,   -- nombre de la entidad/tabla (ej. 'Afiliado')
        Nombre    nvarchar(128) NOT NULL,   -- nombre visible del filtro
        Criteria  nvarchar(max) NOT NULL,   -- CriteriaOperator.ToString()
        EsSistema bit NOT NULL CONSTRAINT DF_SgpaFiltro_Sys DEFAULT(0), -- 1 = compartido/predefinido
        Usr       nvarchar(16) NULL,
        Ts        datetime2 NULL
    );
    CREATE INDEX IX_SgpaFiltro_Entity ON dbo.SgpaFiltro(Entity);
END
