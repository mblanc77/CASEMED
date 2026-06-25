-- Índice de apoyo para la pantalla de subsidios (filtra por período Mes/Año) y para el recálculo del
-- imponible emp900 por período (ImponibleSubsidioSync, camino ci=null de la liquidación):
-- antes, filtrar por (Anio, Mes) hacía scan de toda dbo.SubsidioCabezal (~70k filas) en cada
-- COUNT / sumario / page-fetch de la grilla y en el MERGE del período.
-- Clave (Anio, Mes); INCLUDE cubre el filtro Liquidar y los sumarios de plata server-side (sin key lookups).
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_SubsidioCabezal_AnioMes'
                                           AND object_id = OBJECT_ID('dbo.SubsidioCabezal'))
    CREATE NONCLUSTERED INDEX IX_SubsidioCabezal_AnioMes
        ON dbo.SubsidioCabezal (Anio, Mes)
        INCLUDE (CI, Liquidar, Dias, ImpNominal, ImpAguinaldo, ImpLiquido);
