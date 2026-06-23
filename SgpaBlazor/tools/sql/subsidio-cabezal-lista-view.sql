-- Vista de LECTURA del listview de subsidios: SubsidioCabezal + cabezal BPS (1:1 por IdSubsidio).
-- La usa el CRUD genérico (entidad SubsidioCabezal con [SgpaReadSource("SubsidioCabezalLista")]) para que las
-- columnas BPS (DiasBPS, LiquidoBPS, LiquidoPagarBPS) sean ordenables/filtrables/agrupables y TOTALIZABLES
-- server-side. Las escrituras siguen yendo a dbo.SubsidioCabezal (esta vista es solo de lectura).
-- LEFT JOIN: los subsidios sin fila en SubsidioCabezal_BPS quedan con los valores BPS en NULL.
CREATE OR ALTER VIEW dbo.SubsidioCabezalLista
AS
    SELECT
        sc.*,
        bps.DiasBPS,
        bps.LiquidoBPS,
        bps.LiquidoPagar AS LiquidoPagarBPS
    FROM dbo.SubsidioCabezal sc
    LEFT JOIN dbo.SubsidioCabezal_BPS bps ON bps.IdSubsidio = sc.IdSubsidio;
