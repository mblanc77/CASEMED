-- "Application Model" liviano por columna (estilo XAF): overrides de metadata por (Tabla, Columna).
-- Cada campo NULL = "usar el default del código/atributo". Tabla vacía = modelo de código puro; cada fila es un delta.
-- Run-once (DbUp, journalizado en dbo.SchemaVersions). El guard IF OBJECT_ID lo hace re-aplicable sin daño.
IF OBJECT_ID('dbo.ColumnaConfig', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ColumnaConfig
    (
        Tabla          nvarchar(128) NOT NULL,
        Columna        nvarchar(128) NOT NULL,
        -- Presentación
        Caption        nvarchar(200) NULL,   -- etiqueta (XAF Caption)
        DisplayFormat  nvarchar(100) NULL,   -- formato .NET/DevExpress (XAF DisplayFormat)
        Orden          int           NULL,   -- orden de columnas/campos (XAF Index)
        Ancho          nvarchar(20)  NULL,   -- ancho en grilla (XAF Width), ej. "120px"
        Alineacion     nvarchar(10)  NULL,   -- left | center | right (override del auto por tipo)
        -- Visibilidad
        VisibleLista   bit           NULL,   -- visible en grilla (XAF ListView member)
        VisibleDetalle bit           NULL,   -- visible en el formulario (XAF DetailView member)
        -- Comportamiento
        SoloLectura    bit           NULL,   -- no editable (XAF AllowEdit = false)
        CONSTRAINT PK_ColumnaConfig PRIMARY KEY (Tabla, Columna),
        CONSTRAINT CK_ColumnaConfig_Alineacion CHECK (Alineacion IS NULL OR Alineacion IN ('left','center','right'))
    );
END
GO
