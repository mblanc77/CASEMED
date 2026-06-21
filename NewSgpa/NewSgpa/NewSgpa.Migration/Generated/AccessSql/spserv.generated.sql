-- Auto-generated from spserv.mdb-specs.txt
-- Query count: 7
-- Ordered by dependencies (nested queries first)

-- ===== DATA OBJECT FOR QUERY: Cobros =====
CREATE OR ALTER VIEW [dbo].[acc_spserv_Cobros_q]
AS
SELECT SP_RetencionPago.IDPrestamo, Sum(SP_RetencionPago.Importe) AS Importe
FROM SP_RetencionPago
GROUP BY SP_RetencionPago.IDPrestamo;
GO

-- ===== DATA OBJECT FOR QUERY: envios =====
CREATE OR ALTER VIEW [dbo].[acc_spserv_envios_q]
AS
SELECT SP_Retencion.IDPrestamo, Sum(SP_Retencion.Importe) AS Importe
FROM SP_Retencion
GROUP BY SP_Retencion.IDPrestamo;
GO

-- ===== DATA OBJECT FOR QUERY: rsFacturaEstado =====
CREATE OR ALTER VIEW [dbo].[acc_spserv_rsFacturaEstado_q]
AS
SELECT SP_Factura.IdPrestamo, SP_Factura.IDFactura, LEFT(CONVERT(char(8), SP_Factura.FechaVencimiento, 112), 6) AS Mes, SP_Prestamo.CodMoneda, SP_Prestamo.Importe
FROM SP_PrestamoEstado INNER JOIN (SP_Prestamo INNER JOIN SP_Factura ON SP_Prestamo.IDPrestamo = SP_Factura.IdPrestamo) ON SP_PrestamoEstado.CodPrestamoEstado = SP_Prestamo.CodPrestamoEstado
WHERE (((SP_Factura.CodFacturaEstado)<>'anu') AND ((SP_PrestamoEstado.Fin)= 0) AND ((SP_PrestamoEstado.CodPrestamoEstado)<>'anu') AND ((SP_Factura.FechaVencimiento)>CONVERT(date, '2003-11-30')));
GO

-- ===== DATA OBJECT FOR QUERY: Rs_PrestamoInteresAmortizacion =====
CREATE OR ALTER VIEW [dbo].[acc_spserv_Rs_PrestamoInteresAmortizacion_q]
AS
SELECT SP_CuadroAmortizacion.IDPrestamo, Sum(SP_CuadroAmortizacion.Interes) AS Interes, Sum(SP_CuadroAmortizacion.Amortizacion) AS Amortizacion
FROM SP_CuadroAmortizacion
GROUP BY SP_CuadroAmortizacion.IDPrestamo;
GO

-- ===== DATA OBJECT FOR QUERY: rsCtaCteRet =====
-- DependsOn: envios, Cobros
CREATE OR ALTER VIEW [dbo].[acc_spserv_rsCtaCteRet_q]
AS
SELECT envios.IDPrestamo, envios.Importe, (CASE WHEN Cobros.Importe IS NULL THEN 0 ELSE Cobros.Importe END) AS cobros, (CASE WHEN ABS(envios.Importe-ISNULL(Cobros.Importe, 0))<1 THEN 0 ELSE envios.Importe-ISNULL(Cobros.Importe, 0) END) AS saldo
FROM [acc_spserv_envios_q] AS envios LEFT JOIN [acc_spserv_Cobros_q] AS Cobros ON envios.IDPrestamo = Cobros.IDPrestamo;
GO

-- ===== DATA OBJECT FOR QUERY: rsDatos =====
-- DependsOn: rsFacturaEstado
CREATE OR ALTER VIEW [dbo].[acc_spserv_rsDatos_q]
AS
SELECT rsFacturaEstado.Mes, Count(*) AS Cantidad, rsFacturaEstado.CodMoneda, Sum(rsFacturaEstado.Importe) AS Importe
FROM [acc_spserv_rsFacturaEstado_q] AS rsFacturaEstado
GROUP BY rsFacturaEstado.Mes, rsFacturaEstado.CodMoneda;
GO

-- ===== COMPAT OBJECT FOR QUERY: Cobros =====
IF OBJECT_ID('dbo.Cobros') IS NULL EXEC('CREATE VIEW [dbo].[Cobros] AS SELECT * FROM [dbo].[acc_spserv_Cobros_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: envios =====
IF OBJECT_ID('dbo.envios') IS NULL EXEC('CREATE VIEW [dbo].[envios] AS SELECT * FROM [dbo].[acc_spserv_envios_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: rsFacturaEstado =====
IF OBJECT_ID('dbo.rsFacturaEstado') IS NULL EXEC('CREATE VIEW [dbo].[rsFacturaEstado] AS SELECT * FROM [dbo].[acc_spserv_rsFacturaEstado_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: Rs_PrestamoInteresAmortizacion =====
IF OBJECT_ID('dbo.Rs_PrestamoInteresAmortizacion') IS NULL EXEC('CREATE VIEW [dbo].[Rs_PrestamoInteresAmortizacion] AS SELECT * FROM [dbo].[acc_spserv_Rs_PrestamoInteresAmortizacion_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: rsCtaCteRet =====
IF OBJECT_ID('dbo.rsCtaCteRet') IS NULL EXEC('CREATE VIEW [dbo].[rsCtaCteRet] AS SELECT * FROM [dbo].[acc_spserv_rsCtaCteRet_q];')
GO

-- ===== COMPAT OBJECT FOR QUERY: rsDatos =====
IF OBJECT_ID('dbo.rsDatos') IS NULL EXEC('CREATE VIEW [dbo].[rsDatos] AS SELECT * FROM [dbo].[acc_spserv_rsDatos_q];')
GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Cobros =====
CREATE OR ALTER PROCEDURE [dbo].[acc_spserv_Cobros]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_RetencionPago.IDPrestamo, Sum(SP_RetencionPago.Importe) AS Importe
    FROM SP_RetencionPago
    GROUP BY SP_RetencionPago.IDPrestamo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: envios =====
CREATE OR ALTER PROCEDURE [dbo].[acc_spserv_envios]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Retencion.IDPrestamo, Sum(SP_Retencion.Importe) AS Importe
    FROM SP_Retencion
    GROUP BY SP_Retencion.IDPrestamo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: rsFacturaEstado =====
CREATE OR ALTER PROCEDURE [dbo].[acc_spserv_rsFacturaEstado]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_Factura.IdPrestamo, SP_Factura.IDFactura, LEFT(CONVERT(char(8), SP_Factura.FechaVencimiento, 112), 6) AS Mes, SP_Prestamo.CodMoneda, SP_Prestamo.Importe
    FROM SP_PrestamoEstado INNER JOIN (SP_Prestamo INNER JOIN SP_Factura ON SP_Prestamo.IDPrestamo = SP_Factura.IdPrestamo) ON SP_PrestamoEstado.CodPrestamoEstado = SP_Prestamo.CodPrestamoEstado
    WHERE (((SP_Factura.CodFacturaEstado)<>'anu') AND ((SP_PrestamoEstado.Fin)= 0) AND ((SP_PrestamoEstado.CodPrestamoEstado)<>'anu') AND ((SP_Factura.FechaVencimiento)>CONVERT(date, '2003-11-30')));
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: Rs_PrestamoInteresAmortizacion =====
CREATE OR ALTER PROCEDURE [dbo].[acc_spserv_Rs_PrestamoInteresAmortizacion]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SP_CuadroAmortizacion.IDPrestamo, Sum(SP_CuadroAmortizacion.Interes) AS Interes, Sum(SP_CuadroAmortizacion.Amortizacion) AS Amortizacion
    FROM SP_CuadroAmortizacion
    GROUP BY SP_CuadroAmortizacion.IDPrestamo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: rsCtaCteRet =====
-- DependsOn: envios, Cobros
CREATE OR ALTER PROCEDURE [dbo].[acc_spserv_rsCtaCteRet]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT envios.IDPrestamo, envios.Importe, (CASE WHEN Cobros.Importe IS NULL THEN 0 ELSE Cobros.Importe END) AS cobros, (CASE WHEN ABS(envios.Importe-ISNULL(Cobros.Importe, 0))<1 THEN 0 ELSE envios.Importe-ISNULL(Cobros.Importe, 0) END) AS saldo
    FROM [acc_spserv_envios_q] AS envios LEFT JOIN [acc_spserv_Cobros_q] AS Cobros ON envios.IDPrestamo = Cobros.IDPrestamo;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: rsDatos =====
-- DependsOn: rsFacturaEstado
CREATE OR ALTER PROCEDURE [dbo].[acc_spserv_rsDatos]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT rsFacturaEstado.Mes, Count(*) AS Cantidad, rsFacturaEstado.CodMoneda, Sum(rsFacturaEstado.Importe) AS Importe
    FROM [acc_spserv_rsFacturaEstado_q] AS rsFacturaEstado
    GROUP BY rsFacturaEstado.Mes, rsFacturaEstado.CodMoneda;
END;

GO

-- ===== WRAPPER PROCEDURE FOR QUERY: rsDatos_Tabla de referencias cruzadas =====
-- DependsOn: rsDatos
CREATE OR ALTER PROCEDURE [dbo].[acc_spserv_rsDatos_Tabla_de_referencias_cruzadas]
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @cols NVARCHAR(MAX);
    DECLARE @sumCols NVARCHAR(MAX);
    DECLARE @sql NVARCHAR(MAX);

    SELECT @cols = STRING_AGG(QUOTENAME(CONVERT(nvarchar(128), pv)), ',')
    FROM (SELECT DISTINCT [Mes] AS pv FROM [acc_spserv_rsDatos_q] AS rsDatos) d;

    SELECT @sumCols = STRING_AGG('ISNULL(' + QUOTENAME(CONVERT(nvarchar(128), pv)) + ',0)', ' + ')
    FROM (SELECT DISTINCT [Mes] AS pv FROM [acc_spserv_rsDatos_q] AS rsDatos) d;

    SET @sql = N'SELECT p.*' +
              CASE WHEN @sumCols IS NOT NULL THEN N', ' + @sumCols + N' AS [Total de Cantidad]' ELSE N'' END +
              N' FROM (SELECT [IdPrestamo], [Mes] AS __PivotKey, [Cantidad] AS __PivotValue FROM [acc_spserv_rsDatos_q] AS rsDatos) src '
              + N' PIVOT (SUM(__PivotValue) FOR __PivotKey IN (' + ISNULL(@cols, N'') + N')) p';

    EXEC sp_executesql @sql;
END;

GO

