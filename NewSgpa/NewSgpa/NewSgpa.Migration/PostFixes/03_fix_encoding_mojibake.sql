-- Repara mojibake heredado de los .mdb: textos cargados con bytes DOS (CP850/CP437) que quedaron guardados
-- como Unicode (ej. 'í'->U+00A1, 'á'->U+00A0). La migración los copia fiel; esto los corrige en SQL.
--
-- SELECTIVO y seguro: sólo toca filas que contienen un marcador DOS INEQUÍVOCO (controles 129/130/144/154,
-- NBSP 160, ¢£¤¥ 162-165, ¨ 168, SHY 173, ¡ 161). El dato correcto en CP1252 (á=225, é=233, ñ=241, Í=205, …)
-- NO contiene esos marcadores, así que no se altera. Idempotente (tras reparar no quedan marcadores).
-- 100% ASCII (todo por NCHAR) para no depender de la codificación con que se lea este archivo.
-- Requerido por tablas con índices sobre columnas computadas / filtrados (si no, el UPDATE falla con Msg 1934).
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
SET NOCOUNT ON;

DECLARE @bin sysname = N'Latin1_General_BIN2';
-- Conjunto de marcadores DOS inequívocos para detectar una fila de origen DOS.
DECLARE @flag nvarchar(60) = N'%[' +
    NCHAR(129) + NCHAR(130) + NCHAR(144) + NCHAR(154) + NCHAR(160) + NCHAR(161) +
    NCHAR(162) + NCHAR(163) + NCHAR(164) + NCHAR(165) + NCHAR(168) + NCHAR(173) + N']%';

-- Mapeo byte-DOS (code point almacenado) -> code point correcto. El ORDEN importa: 233->Ú e 161->í van primero
-- porque sus SALIDAS (Ú, í) no deben re-procesarse, y 130->é (produce 233) y 173->¡ (produce 161) van al final.
DECLARE @map TABLE (ord int, cpf int, cpt int);
INSERT INTO @map (ord, cpf, cpt) VALUES
    (1, 233, 218),  -- Ú
    (2, 161, 237),  -- í
    (3, 160, 225),  -- á
    (4, 162, 243),  -- ó
    (5, 163, 250),  -- ú
    (6, 164, 241),  -- ñ
    (7, 165, 209),  -- Ñ
    (8, 144, 201),  -- É
    (9, 181, 193),  -- Á
    (10, 214, 205), -- Í
    (11, 224, 211), -- Ó
    (12, 129, 252), -- ü
    (13, 154, 220), -- Ü
    (14, 168, 191), -- ¿
    (15, 130, 233), -- é  (después de 233->Ú)
    (16, 173, 161); -- ¡  (después de 161->í)

DECLARE @sch sysname, @tbl sysname, @col sysname, @sql nvarchar(max), @expr nvarchar(max);

DECLARE cur CURSOR LOCAL FAST_FORWARD FOR
    SELECT s.name, t.name, c.name
    FROM sys.columns c
    JOIN sys.tables  t  ON t.object_id = c.object_id
    JOIN sys.schemas s  ON s.schema_id = t.schema_id
    JOIN sys.types   ty ON ty.user_type_id = c.user_type_id
    WHERE ty.name IN (N'nvarchar', N'nchar')
      AND c.is_computed = 0
      AND t.is_ms_shipped = 0;

OPEN cur;
FETCH NEXT FROM cur INTO @sch, @tbl, @col;
WHILE @@FETCH_STATUS = 0
BEGIN
    -- Cadena de REPLACE anidados (en orden de @map), toda en colación binaria para coincidencia exacta por code point.
    SET @expr = N'CAST(q.[' + @col + N'] AS nvarchar(max)) COLLATE ' + @bin;
    SELECT @expr = N'REPLACE(' + @expr + N',NCHAR(' + CAST(cpf AS varchar(5)) + N'),NCHAR(' + CAST(cpt AS varchar(5)) + N'))'
    FROM @map ORDER BY ord;

    SET @sql =
        N'UPDATE q SET [' + @col + N'] = ' + @expr +
        N' FROM [' + @sch + N'].[' + @tbl + N'] q' +
        N' WHERE q.[' + @col + N'] COLLATE ' + @bin + N' LIKE N''' + @flag + N''' COLLATE ' + @bin + N';';
    EXEC sys.sp_executesql @sql;

    FETCH NEXT FROM cur INTO @sch, @tbl, @col;
END
CLOSE cur;
DEALLOCATE cur;
